using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinica.Areas.Paciente.Controllers
{
    public class PacientesController : Controller
    {
        ClinicaEntidad db = new ClinicaEntidad();
        // GET: Paciente/Pacientes
        public ActionResult Index()
        {
            return View();
        }

        // GET: Paciente/Pacientes/Details/5
        public ActionResult Perfil(int id)
        {
            Pacientes paciente = db.Pacientes.Find(id);
            return View(paciente);
        }


        // GET: Paciente/Pacientes/Edit/5
        public ActionResult Editar(int id)
        {
            return View();
        }

        // POST: Paciente/Pacientes/Edit/5
        [HttpPost]
        public ActionResult Editar(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Paciente/Pacientes/Delete/5
        public ActionResult Desactivar(int id)
        {
            return View();
        }

        // POST: Paciente/Pacientes/Delete/5
        [HttpPost]
        public ActionResult Desactivar(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult SacarTurno(int? id)
        {
            return View();
        }
    }
}
