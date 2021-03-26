using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Clinica.Filtros;

namespace Clinica.Areas.Profesionales.Controllers
{
    [SesionFilters(Global.eTipoUsuario.Trabajador)]
    public class ProfesionalController : Controller
    {
        // GET: Profesionales/Profesional
        public ActionResult Index(int? UsuarioId)
        {
            return View();
        }

        // GET: Profesionales/Profesional/Details/5
        public ActionResult Detalles(int id)
        {
            return View();
        }

        // GET: Profesionales/Profesional/Create
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Profesionales/Profesional/Create
        [HttpPost]
        public ActionResult Crear(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Profesionales/Profesional/Edit/5
        public ActionResult Editar(int id)
        {
            return View();
        }

        // POST: Profesionales/Profesional/Edit/5
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

        // GET: Profesionales/Profesional/Delete/5
        public ActionResult Borrar(int id)
        {
            return View();
        }

        // POST: Profesionales/Profesional/Delete/5
        [HttpPost]
        public ActionResult Borrar(int id, FormCollection collection)
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
    }
}
