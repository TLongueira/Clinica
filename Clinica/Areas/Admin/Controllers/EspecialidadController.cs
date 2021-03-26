using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Clinica.Extensions;
using Clinica.Filtros;


namespace Clinica.Areas.Admin.Controllers
{
   //[SesionFilters(Global.eTipoUsuario.SuperAdministrador)]

    public class EspecialidadController : Controller
    {
        private ClinicaEntidad db = new ClinicaEntidad();

        // GET: Admin/Especialidads
        public ActionResult Index(string orden, string filtro, int? pagina)
        {
            var especialidades = db.Especialidad
                .Where(E => E.Activo == true);

            var paginado = this.LoadPager(especialidades, orden, filtro, pagina, 15, "Descripcion");

            return View(paginado);
        }

        // GET: Admin/Especialidads/Details/5
        public ActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.especialidades = db.Clinica_profesinales_especialidad
               .Include("Especialidad")
               .Include("Clinica_profesionales")
               .Include("Clinica_profesionales.Profesional")
               .Include("Clinica_profesionales.Clinica")
               .Distinct()
               .Where(p => p.Especialidad.EspecialidadId == id)
               .OrderBy(c => c.Clinica_profesionales.Profesional.Persona.Nombre)
               .ToList();

            ViewBag.NombreClinica = db.Clinica.ToList();
            Especialidad especialidad = db.Especialidad.Find(id);
            if (especialidad == null)
            {
                return HttpNotFound();
            }
            return View(especialidad);
        }

        // GET: Admin/Especialidads/Create
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Admin/Especialidads/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear([Bind(Include = "EspecialidadId,Descripcion,Activo")] Especialidad especialidad)
        {
            if (ModelState.IsValid)
            {
                db.Especialidad.Add(especialidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(especialidad);
        }

        // GET: Admin/Especialidads/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidad especialidad = db.Especialidad.Find(id);
            if (especialidad == null)
            {
                return HttpNotFound();
            }
            return View(especialidad);
        }

        // POST: Admin/Especialidads/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "EspecialidadId,Descripcion,Activo")] Especialidad especialidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(especialidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(especialidad);
        }

        // GET: Admin/Especialidads/Delete/5
        public ActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidad especialidad = db.Especialidad.Find(id);
            if (especialidad == null)
            {
                return HttpNotFound();
            }
            return View(especialidad);
        }

        // POST: Admin/Especialidads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Especialidad especialidad = db.Especialidad.Find(id);
            db.Especialidad.Remove(especialidad);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult inactivo()
        {
            var especialidad = db.Especialidad;
            return View(especialidad.Where(E => E.Activo == false)
                .ToList());
        }
        public ActionResult Activar(int? id, Especialidad especialidad)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            especialidad = db.Especialidad.Find(id);
            especialidad.Activo = true;
            db.Entry(especialidad).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Desactivar(int? id, Especialidad especialidad)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            especialidad = db.Especialidad.Find(id);
            especialidad.Activo = false;
            db.Entry(especialidad).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
