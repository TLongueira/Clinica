using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Clinica.Filtros;

namespace Clinica.Areas.Paciente.Controllers
{
    [SesionFilters(Global.eTipoUsuario.Paciente)]
    public class TurnosController : Controller
    {
        // GET: Paciente/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}