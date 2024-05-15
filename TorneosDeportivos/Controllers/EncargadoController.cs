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
using System.Reflection.Metadata;

namespace TorneosDeportivos.Controllers
{
    public class EncargadoController : Controller
    {
        private readonly ILogger<EncargadoController> _logger;
        private readonly Contexto _contexto;
        private readonly Data.Servicios.GeneralServicio _generalServicio;

        public EncargadoController(ILogger<EncargadoController> logger, Contexto contexto)
        {
            _logger = logger;
            _contexto = contexto;
            _generalServicio = new GeneralServicio(contexto);
        }

        [Authorize(Roles = "Encargado")]
        public IActionResult Index()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}