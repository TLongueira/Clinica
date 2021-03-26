using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinica.Controllers
{
    public class HomeController : Controller
    {
        ClinicaEntidad db = new ClinicaEntidad();
        public ActionResult Index()
        {
            ViewData["Pacientes"] = db.Pacientes.Count();
            ViewData["Profesionales"] = db.Profesional.Count();
            ViewData["Especialidad"] = db.Especialidad.Count();
            ViewData["Sucursales"] = db.Sucursal.Count();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ListadoProfesionales()
        {
            ViewBag.profesionales = db.spProfesionalClinicaEspecialidadInicio().ToList();
            return View("ListadoProfesionales");
        }
    }
}