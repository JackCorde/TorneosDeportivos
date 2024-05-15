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
using Microsoft.AspNetCore.Authorization;

namespace TorneosDeportivos.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Contexto _contexto;
        private readonly Data.Servicios.GeneralServicio _generalServicio;

        public ConsultasController(ILogger<HomeController> logger, Contexto contexto)
        {
            _logger = logger;
            _contexto = contexto;
            _generalServicio = new GeneralServicio(contexto);
        }

        // GET: /Consultas/ObtenerUsuarios
        [Authorize(Roles = "Administrador")]
        public IActionResult ObtenerUsuarios()
        {
            var model = new List<Usuario>();
            using (var connection = new SqlConnection(_contexto.Conexion))
            {
                connection.Open();
                using (SqlCommand cmd = new("ConsultarUsuarios", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        model.Add(new Usuario
                        {
                            UsuarioId = (int)reader["usuarioId"],
                            Nombre = (string)reader["Nombre"],
                            Username = (string)reader["Username"],
                            Email = (string)reader["Correo"],
                            RolId = (int)reader["rolId"],
                            RolName = (string)reader["RolNombre"]
                        });

                    }
                }
            }
            return Json(model);
        }

        [Authorize(Roles = "Administrador, Encargado")]
        public IActionResult ObtenerCanchas()
        {
            var model = new List<Cancha>();
            using (var connection = new SqlConnection(_contexto.Conexion))
            {
                connection.Open();
                using (SqlCommand cmd = new("ConsultarCanchas", connection))
                {
                    try {
                        cmd.CommandType = CommandType.StoredProcedure;

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            model.Add(new Cancha
                            {
                                CanchaId = (int)reader["canchaId"],
                                Descripcion = (string)reader["Descripcion"],
                                Deporte = (string)reader["Deporte"],
                                Activa = (bool)reader["activa"]
                            });

                        }
                    } catch (SqlException ex)
                    {
                        string Error = ex.Message;
                        return Json(model);
                    }
                    
                }
            }
            return Json(model);
        }

        [Authorize(Roles = "Administrador, Encargado")]
        public IActionResult ObtenerTorneos()
        {
            var model = new List<Torneo>();
            using (var connection = new SqlConnection(_contexto.Conexion))
            {
                connection.Open();
                using (SqlCommand cmd = new("ConsultarTorneos", connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            model.Add(new Torneo
                            {
                                TorneoId = (int)reader["torneoId"],
                                TorneoNombre = (string)reader["TorneoNombre"],
                                Deporte = (string)reader["Deporte"],
                                Categoria = (string)reader["Categoria"],
                                fechaInicio = (string)reader["FechaInicio"],
                                fechaFinal = (string)reader["FechaFinal"],
                                Ganador = reader["Ganador"] != DBNull.Value ? (string)reader["Ganador"] : "Aún no hay Ganador"
                            });

                        }
                    }
                    catch (SqlException ex)
                    {
                        string Error = ex.Message;
                    }

                }
            }
            return Json(model);
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult ObtenerEquipos()
        {
            var model = new List<Equipo>();
            using (var connection = new SqlConnection(_contexto.Conexion))
            {
                connection.Open();
                using (SqlCommand cmd = new("ConsultarEquipos", connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            model.Add(new Equipo
                            {
                                EquipoId = (int)reader["equipoId"],
                                EquipoNombre = (string)reader["EquipoNombre"],
                                Deporte = (string)reader["Deporte"],
                                Categoria = (string)reader["Categoria"],
                                CoachNombre = (string)reader["Coach"],
                                TorneoActual = reader["TorneoActual"] != DBNull.Value ? (string)reader["TorneoActual"] : "No se encuentra en un torneo"
                            });
                        }
                    }
                    catch (SqlException ex)
                    {
                        string Error = ex.Message;
                    }

                }
            }
            return Json(model);
        }

        [Authorize(Roles = "Administrador, Encargado")]
        public IActionResult ObtenerUsuariosPorRol(int rol)
        {
            var model = new List<UsuariosViewModel>();
            using (var connection = new SqlConnection(_contexto.Conexion))
            {
                connection.Open();
                using (SqlCommand cmd = new("ConsultarUsuariosPorRol", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RolId", rol);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        model.Add(new UsuariosViewModel
                        {
                            UsuarioId = (int)reader["usuarioId"],
                            Nombre = (string)reader["Nombre"],
                            Username = (string)reader["Username"],
                            Email = (string)reader["Correo"],
                            RolId = (int)reader["rolId"],
                            RolName = (string)reader["RolNombre"],
                            NumeroCuentas = reader["NumeroCuentas"] != DBNull.Value ? (int)reader["NumeroCuentas"] : null,
                            Deporte = reader["Deporte"]  != DBNull.Value ? (string)reader["Deporte"] : null,
                            NumeroPartidos = reader["NumeroPartidos"] != DBNull.Value ? (int)reader["NumeroPartidos"] : null,
                            Equipo = reader["Equipo"] != DBNull.Value ? (string)reader["Equipo"] : null,
                            Costo = reader["Costo"] != DBNull.Value ? (decimal)reader["Costo"] : null,
                        });

                    }
                }
            }
            return Json(model);
        }


        [Authorize(Roles = "Encargado")]
        public IActionResult ObtenerPartidos()
        {
            var model = new List<Partido>();
            using (var connection = new SqlConnection(_contexto.Conexion))
            {
                connection.Open();
                using (SqlCommand cmd = new("ConsultarPartidos", connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            model.Add(new Partido
                            {
                                PartidoId = (int)reader["partidoId"],
                                EquipoL = (string)reader["EquipoL"],
                                EquipoV = (string)reader["EquipoV"],
                                Torneo = (string)reader["Torneo"],
                                EquipoG = reader["EquipoG"] != DBNull.Value ? (string)reader["EquipoG"] : "Aún no hay equipo ganador",
                                EquipoP = reader["EquipoP"] != DBNull.Value ? (string)reader["EquipoP"] : "Aún no hay equipo perdedor",
                                Resultados = reader["Resultados"] != DBNull.Value ? (string)reader["Resultados"] : "Aún no se ha efectuado el partido",
                                Cancha = (string)reader["Cancha"],
                                fecha = (string)reader["Fecha"],
                                Hora = (int)reader["hora"],
                                Arbitro= (string)reader["Arbitro"],
                                CostoTotal = (decimal)reader["CostoTotal"],

                            });
                        }
                    }
                    catch (SqlException ex)
                    {
                        string Error = ex.Message;
                    }

                }
            }
            return Json(model);
        }


        [Authorize(Roles = "Arbitro")]
        public IActionResult ObtenerPartidosPorArbitro()
        {
            ClaimsPrincipal c = HttpContext.User;
            Claim? idClaim = c.FindFirst(ClaimTypes.SerialNumber);
            int id = int.Parse(idClaim.Value);
            var model = new List<Partido>();
            using (var connection = new SqlConnection(_contexto.Conexion))
            {
                connection.Open();
                using (SqlCommand cmd = new("ConsultarPartidosPorArbitro", connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);
                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            model.Add(new Partido
                            {
                                PartidoId = (int)reader["partidoId"],
                                EquipoL = (string)reader["EquipoL"],
                                EquipoV = (string)reader["EquipoV"],
                                Torneo = (string)reader["Torneo"],
                                EquipoG = reader["EquipoG"] != DBNull.Value ? (string)reader["EquipoG"] : "Aún no hay equipo ganador",
                                EquipoP = reader["EquipoP"] != DBNull.Value ? (string)reader["EquipoP"] : "Aún no hay equipo perdedor",
                                Resultados = reader["Resultados"] != DBNull.Value ? (string)reader["Resultados"] : "Aún no se ha efectuado el partido",
                                Cancha = (string)reader["Cancha"],
                                fecha = (string)reader["Fecha"],
                                Hora = (int)reader["hora"],
                                Arbitro = (string)reader["Arbitro"],
                                CostoTotal = (decimal)reader["CostoTotal"],
                                Status = reader["Resultados"] != DBNull.Value ? "Completado" : "Pendiente",
                            });
                        }
                    }
                    catch (SqlException ex)
                    {
                        string Error = ex.Message;
                    }

                }
            }
            return Json(model);
        }

        [Authorize(Roles = "Contador")]
        public IActionResult ObtenerCuentas()
        {
            var model = new List<Cuenta>();
            using (var connection = new SqlConnection(_contexto.Conexion))
            {
                connection.Open();
                using (SqlCommand cmd = new("ConsultarCuentas", connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            model.Add(new Cuenta
                            {
                                CuentaId = (int)reader["cuentaId"],
                                Torneo = (string)reader["Torneo"],
                                Contador = (string)reader["Contador"],
                                Retiros = (int)reader["Retiros"],
                                Depositos = (int)reader["Depositos"],
                                Total = reader["Total"] != DBNull.Value ? (decimal)reader["Total"] : 0,
                            });
                        }
                    }
                    catch (SqlException ex)
                    {
                        string Error = ex.Message;
                    }

                }
            }
            return Json(model);
        }

        [Authorize(Roles = "Contador")]
        public IActionResult ObtenerPagosPartido()
        {
            var model = new List<PagoPartido>();
            using (var connection = new SqlConnection(_contexto.Conexion))
            {
                connection.Open();
                using (SqlCommand cmd = new("ConsultarPagoPartido", connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            model.Add(new PagoPartido
                            {
                                PagoPartidoId = (int)reader["pagoPartidoId"],
                                Cantidad = (decimal)reader["cantidad"],
                                EquipoVisitante = (bool)reader["equipoVisitante"] ? "Pagado" : "Pendiente",
                                EquipoLocal = (bool)reader["equipoLocal"] ? "Pagado" : "Pendiente",
                                Torneo = (string)reader["Torneo"],
                                PartidoId = (int)reader["partidoId"],
                                fechaPago = reader["Fecha"] != DBNull.Value ? (string)reader["Fecha"] : "Aún no hay pagos",
                                cuentaId = (int)reader["cuentaId"]
                            }); 
                        }
                    }
                    catch (SqlException ex)
                    {
                        string Error = ex.Message;
                    }

                }
            }
            return Json(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}