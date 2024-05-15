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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Contexto _contexto;
        private readonly Data.Servicios.GeneralServicio _generalServicio;

        public HomeController(ILogger<HomeController> logger, Contexto contexto)
        {
            _logger = logger;
            _contexto = contexto;
            _generalServicio = new GeneralServicio(contexto);
        }

        public IActionResult Index()
        {
            ClaimsPrincipal c = HttpContext.User;

            if (c.Identity != null && c.Identity.IsAuthenticated)
            {
                // Verificar si el usuario tiene el claim de rol
                var roleClaim = c.FindFirst(ClaimTypes.Role);

                if (roleClaim != null)
                {
                    // Obtener el valor del claim de rol
                    string role = roleClaim.Value;

                    // Redirigir al index del controlador adecuado según el rol
                    switch (role)
                    {
                        case "Administrador":
                            return RedirectToAction("Index", "Administrador");
                        case "Encargado":
                            return RedirectToAction("Index", "Encargado");
                        case "Arbitro":
                            return RedirectToAction("Index", "Arbitro");
                        case "Contador":
                            return RedirectToAction("Index", "Contador");
                        case "Coach":
                            return RedirectToAction("Index", "Coach");
                        default:
                            return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }


        public IActionResult Login(string? Error)
        {
            ClaimsPrincipal c = HttpContext.User;
            if (c.Identity != null)
            {
                if (c.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Administrador");
            }
            ViewBag.Error = Error;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            try
            {
                using (SqlConnection con = new(_contexto.Conexion))
                {
                    using (SqlCommand cmd = new("ValidarUsuario", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", model.Username);
                        con.Open();
                        try
                        {
                            using (var dr = cmd.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    int numeroIntentos = (int)dr["intentosFallidos"];
                                    if (numeroIntentos <= 3)
                                    {
                                        bool passwordMatch = BCrypt.Net.BCrypt.Verify(model.LaPoderosa, dr["LaPoderosa"].ToString());
                                        if (passwordMatch)
                                        {

                                            int usuarioId = (int)dr["UsuarioId"];
                                            _generalServicio.LimpiarNumeroIntento(usuarioId);
                                            string? nombreusuario = (string)dr["Username"];
                                            int idUsuario = (int)dr["UsuarioId"];
                                            string? nombreCompleto = (string)dr["Nombre"];

                                            if (nombreusuario != null)
                                            {
                                                var claims = new List<Claim>()
                                            {
                                                new Claim(ClaimTypes.NameIdentifier, nombreusuario),
                                                new Claim(ClaimTypes.Name, nombreCompleto),
                                                new Claim(ClaimTypes.SerialNumber, idUsuario.ToString())
                                            };

                                                int perfilId = (int)dr["rolId"];
                                                string perfilNombre = (string)dr["RolNombre"];
                                                claims.Add(new Claim(ClaimTypes.Role, perfilNombre));

                                                var identify = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                                                var propiedades = new AuthenticationProperties
                                                {
                                                    AllowRefresh = true,
                                                    IsPersistent = true,
                                                    ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromHours(1)),
                                                };

                                                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identify), propiedades);

                                                switch (perfilId)
                                                {
                                                    case 1:
                                                        return RedirectToAction("Index", "Administrador");
                                                    case 2:
                                                        return RedirectToAction("Index", "Contador");
                                                    case 3:
                                                        return RedirectToAction("Index", "Arbitro");
                                                    case 4:
                                                        return RedirectToAction("Index", "Coach");
                                                    case 6:
                                                        return RedirectToAction("Index", "Encargado");
                                                }

                                            }

                                        }
                                        else
                                        {
                                            int usuarioId = (int)dr["UsuarioId"];
                                            _generalServicio.NumeroIntento(usuarioId);
                                            ViewBag.Error = "Contraseña Incorrecta";
                                            dr.Close();
                                        }
                                    }
                                    else
                                    {
                                        ViewBag.Error = "Cuenta Bloqueada por Exceso de Intentos";
                                        dr.Close();
                                    }
                                    
                                }
                                else
                                {
                                    ViewBag.Error = "Usuario no Registrado";
                                    dr.Close();
                                }
                            }
                        }
                        finally
                        {
                            if (cmd != null)
                                cmd.Dispose();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("Login", new { ViewBag.Error });
        }

        

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}