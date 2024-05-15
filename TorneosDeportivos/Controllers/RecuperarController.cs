using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;
using TorneosDeportivos.Data;
using TorneosDeportivos.Data.Servicios;
using TorneosDeportivos.Models;
using TorneosDeportivos.Models.ViewModels;

namespace TorneosDeportivos.Controllers
{
    public class RecuperarController : Controller
    {
        private readonly ILogger<RecuperarController> _logger;
        private readonly Contexto _contexto;
        private readonly Data.Servicios.GeneralServicio _generalServicio;

        public RecuperarController(ILogger<RecuperarController> logger, Contexto contexto)
        {
            _logger = logger;
            _contexto = contexto;
            _generalServicio = new GeneralServicio(contexto);
        }




        [HttpPost]
        public IActionResult EnviarClave(string correo)
        {
            if (correo!=null)
            {
                bool usuarioExiste = false;
                int usuarioId = 0;
                using (SqlConnection con = new(_contexto.Conexion))
                {
                    using (SqlCommand cmd = new("ConsultarUsuarioPorCorreo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Correo", correo);
                        cmd.Parameters.Add("@Existe", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Direction = ParameterDirection.Output;

                        con.Open();
                        cmd.ExecuteNonQuery();

                        // Obtener los valores de retorno de los parámetros de salida
                        usuarioExiste = (bool)cmd.Parameters["@Existe"].Value;


                        if (usuarioExiste)
                        {
                            usuarioId = (int)cmd.Parameters["@UsuarioId"].Value;
                            con.Close();
                            bool creacionClave = Clave(usuarioId, correo);
                            if (creacionClave)
                            {
                                RecuperarViewModel model = new RecuperarViewModel
                                {
                                    Id = usuarioId,
                                    Status = true,
                                    Mensaje = "Te hemos enviado una clave a tu correo electrónico"
                                };
                                return Json(model);
                            }
                            else
                            {
                                RecuperarViewModel model = new RecuperarViewModel
                                {
                                    Id = usuarioId,
                                    Status = false,
                                    Mensaje = "Error al enviar Clave"
                                };
                                return Json(model);
                            }
                        }
                        else
                        {
                            RecuperarViewModel model = new RecuperarViewModel
                            {
                                Id = usuarioId,
                                Status = false,
                                Mensaje = "No se encontró el correo electrónico"
                            };
                            return Json(model);
                        }
                    }
                }
            }
            else
            {
                RecuperarViewModel model = new RecuperarViewModel
                {
                    Status = false,
                    Mensaje = "Por favor ingresa un correo electrónico"
                };
                return Json(model);
            }
            
        }

        public static string GenerateClave(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return result;
        }

        public bool Clave(int usuarioId, string correo)
        {
            using (var con = new SqlConnection(_contexto.Conexion))
            {
                string clave = GenerateClave(8);
                try
                {
                    using (SqlCommand cmd2 = new("actualizarClave", con))
                    {
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@Id", usuarioId);
                        cmd2.Parameters.AddWithValue("@Clave", clave);
                        con.Open();
                        cmd2.ExecuteNonQuery();
                        con.Close(); // Cerrar la conexión aquí
                        Email email = new();
                        email.Enviar(correo, clave);
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }



        [HttpPost]
        public IActionResult ComprobarClave(int id, string? clave)
        {
            bool ClaveConfirmada = false;
            try
            {
                
                if (clave != null)
                {
                    using (SqlConnection con = new(_contexto.Conexion))
                    {
                        using (SqlCommand cmd = new("ConsultarClave", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.Parameters.AddWithValue("@Clave", clave);
                            cmd.Parameters.Add("@Confirmar", SqlDbType.Bit).Direction = ParameterDirection.Output;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            ClaveConfirmada = (bool)cmd.Parameters["@Confirmar"].Value;
                            con.Close();
                            if (ClaveConfirmada)
                            {
                                RecuperarViewModel model = new RecuperarViewModel
                                {
                                    Id = id,
                                    Status = true,
                                    Mensaje = "Clave Verificada"
                                };
                                return Json(model);

                            }
                            else
                            {
                                RecuperarViewModel model = new RecuperarViewModel
                                {
                                    Id = id,
                                    Status = false,
                                    Mensaje = "Clave Incorrecta o Caducada"
                                };
                                return Json(model);

                            }

                        }
                    }
                }
                else
                {
                    RecuperarViewModel model = new RecuperarViewModel
                    {
                        Id = id,
                        Status = false,
                        Mensaje = "No Ingresaste Clave"
                    };
                    return Json(model);
                }
                      
            }
            catch (Exception ex)
            {
                RecuperarViewModel model = new RecuperarViewModel
                {
                    Id = id,
                    Status = false,
                    Mensaje = ex.Message
                };
                return Json(model);
            }
        }


        [HttpPost]
        public IActionResult NuevaContraseña(int id, string? laPoderosa)
        {
            if(laPoderosa != null)
            {
                try
                {
                    using (SqlConnection con = new(_contexto.Conexion))
                    {
                        using (SqlCommand cmd = new("ActualizarContraseña", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Id", id);
                            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(laPoderosa);
                            cmd.Parameters.AddWithValue("@LaPoderosa", hashedPassword);
                            con.Open();
                            cmd.ExecuteReader();
                            con.Close();
                            RecuperarViewModel model = new RecuperarViewModel
                            {
                                Status = true,
                                Mensaje = "Se ha actualizado tu contraseña exitosamente"
                            };
                            return Json(model);

                        }
                    }
                }
                catch (Exception ex)
                {
                    RecuperarViewModel model = new RecuperarViewModel
                    {
                        Status = false,
                        Mensaje = ex.Message
                    };
                    return Json(model);
                }
            }
            else
            {
                RecuperarViewModel model = new RecuperarViewModel
                {
                    Status = false,
                    Mensaje = "No Ingresaste Contraseña"
                };
                return Json(model);
            }
            
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}