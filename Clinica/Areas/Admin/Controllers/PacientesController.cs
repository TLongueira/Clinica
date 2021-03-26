using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Clinica;
using Clinica.Filtros;
using Clinica.Global;

namespace Clinica.Areas.Admin.Controllers
{
    public class PacientesController : Controller
    {
        private ClinicaEntidad db = new ClinicaEntidad();

        // GET: Admin/Pacientes
        public ActionResult Index()
        {
            var pacientes = db.Pacientes.Include(p => p.obra_social).Include(p => p.Persona);
            return View(pacientes.ToList());
        }

        // GET: Admin/Pacientes/Details/5
        public ActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacientes pacientes = db.Pacientes.Find(id);
            if (pacientes == null)
            {
                return HttpNotFound();
            }
            return View(pacientes);
        }

        // GET: Admin/Pacientes/Create
        public ActionResult Crear()
        {
            ViewBag.ObraId = new SelectList(db.obra_social, "ObraId", "Descripcion");
            ViewBag.PersonId = new SelectList(db.Persona, "PersonaId", "Nombre");
            return View();
        }

        // POST: Admin/Pacientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult CrearPaciente(Pacientes pacientes,string Contraseña, Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                ////usuarios.Activo = true;
                ////usuarios.Clave = Hasher.Hash(Contraseña);
                ////usuarios.Modificado = DateTime.Now;
                ////usuarios.Creado = DateTime.Now;
                ////usuarios.NombreUsuario = pacientes.Email;
                ////usuarios.RolId = 2;
                ////usuarios.TipoUsuarioId = 2;
                ////db.Usuarios.Add(usuarios);
                ////db.SaveChanges();
                ////db.Pacientes.Add(pacientes);
                ////db.SaveChanges();
                return RedirectToAction("~/~/Iniciar", "Sesion");
            }

            ViewBag.ObraId = new SelectList(db.obra_social, "ObraId", "Descripcion", pacientes.ObraId);
            ViewBag.PersonId = new SelectList(db.Persona, "PersonaId", "Nombre", pacientes.PersonId);
            return View(pacientes);
        }

        // GET: Admin/Pacientes/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacientes pacientes = db.Pacientes.Find(id);
            if (pacientes == null)
            {
                return HttpNotFound();
            }
            ViewBag.ObraId = new SelectList(db.obra_social, "ObraId", "Descripcion", pacientes.ObraId);
            ViewBag.PersonId = new SelectList(db.Persona, "PersonaId", "Nombre", pacientes.PersonId);
            return View(pacientes);
        }

        // POST: Admin/Pacientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "PacienteID,PersonId,Email,Numero_credencial,ObraId")] Pacientes pacientes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pacientes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ObraId = new SelectList(db.obra_social, "ObraId", "Descripcion", pacientes.ObraId);
            ViewBag.PersonId = new SelectList(db.Persona, "PersonaId", "Nombre", pacientes.PersonId);
            return View(pacientes);
        }

        // GET: Admin/Pacientes/Delete/5
        public ActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacientes pacientes = db.Pacientes.Find(id);
            if (pacientes == null)
            {
                return HttpNotFound();
            }
            return View(pacientes);
        }

        // POST: Admin/Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pacientes pacientes = db.Pacientes.Find(id);
            db.Pacientes.Remove(pacientes);
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
    }
}
