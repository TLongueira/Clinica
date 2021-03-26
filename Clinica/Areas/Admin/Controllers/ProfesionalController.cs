using Clinica.Filtros;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Data.SqlClient;
using System.Web.Mvc;
using Clinica.Extensions;
namespace Clinica.Areas.Admin.Controllers
{
    [SesionFilters(Global.eTipoUsuario.SuperAdministrador)]
    public class ProfesionalController : Controller
    {
        private ClinicaEntidad db = new ClinicaEntidad();
        // GET: Admin/Profesionals
        public string ruta = "";
        public static int id_persona = 0;
        public ActionResult Index(string orden, string filtro, int? pagina)
        {
            var Profesional = db.Profesional.Include(p => p.Persona)
                .Where(p => p.Activo == true);

            var paginado = this.LoadPager(Profesional, orden, filtro, pagina, 15, "Persona.Apellido", "Persona.Nombre", "MatriculaProvincial", "MatriculaNacional", "Creado");

            return View(paginado);
        }

        // GET: Admin/Profesionals/Details/5
        public ActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesional profesional = db.Profesional.Find(id);
            if (profesional == null)
            {
                return HttpNotFound();
            }
            ViewBag.clinicas = db.Clinica_profesionales
                .Include("Clinica")
                .Where(c => c.id_profesional == id)
                .OrderBy(c=>c.Clinica.Nombre)
                .ToList();

            ViewBag.especialidades = db.Clinica_profesinales_especialidad
               .Include("Especialidad")
               .Include("Clinica_profesionales")
               .Where(p => p.Clinica_profesionales.id_profesional == id)
               .OrderBy(p=>p.Especialidad.Descripcion)
               .ToList();
            return View(profesional);
        }

        // GET: Admin/Profesionals/Create
        public ActionResult Crear()
        {
            ViewBag.PersonaId = new SelectList(db.Persona, "PersonaId", "Nombre");
            return View();
        }

        // POST: Admin/Profesionals/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Crear(Profesional profesional,HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                if (File != null)
                {
                    var filename = Path.GetFileName(File.FileName);
                    var path = Path.Combine(Server.MapPath("~/imagenes/Profesional_Fotos"), filename);
                    File.SaveAs(path);
                    profesional.Photopath = "/imagenes/Profesional_Fotos/"+filename;
                }
                else
                {
                    profesional.Photopath = null;
                }
                profesional.Activo = true;
                profesional.Creado = DateTime.Now;
                profesional.Modificado = DateTime.Now;
                db.Persona.Add(profesional.Persona);
                //db.SaveChanges();

                //profesional.PersonaId = profesional.Persona.PersonaId;

                db.Profesional.Add(profesional);
                db.SaveChanges();

                return RedirectToAction("Detalles", new { id= profesional.PersonaId - 1 });
            }

            ViewBag.PersonaId = new SelectList(db.Persona, "PersonaId", "Nombre", profesional.PersonaId);
            return View(profesional);
        }


        // GET: Admin/Profesionals/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesional profesional = db.Profesional.Find(id);
            if (profesional == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonaId = new SelectList(db.Persona, "PersonaId", "Nombre", profesional.PersonaId);
            id_persona = profesional.PersonaId;
            return View(profesional);
        }

        // POST: Admin/Profesionals/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Profesional profesional,Persona persona,HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                if (File != null)
                {
                    if(profesional.Photopath!=null)
                    {
                        var file = Path.Combine(Server.MapPath("~/imagenes/Profesional_Fotos/"), profesional.Photopath);
                        if (System.IO.File.Exists(file)) System.IO.File.Delete(file);
                    }
                    var filename = Path.GetFileName(File.FileName);
                    var path = Path.Combine(Server.MapPath("~/imagenes/Profesional_Fotos"), filename);
                    File.SaveAs(path);
                    profesional.Photopath = "/imagenes/Profesional_Fotos/" + filename;
                }
                else
                {
                    profesional.Photopath = profesional.Photopath;
                }
                profesional.Modificado = DateTime.Today;
                db.Entry(profesional.Persona).State = EntityState.Modified;
                profesional.PersonaId = profesional.Persona.PersonaId;
                db.Entry(profesional).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonaId = new SelectList(db.Persona, "PersonaId", "Nombre", profesional.PersonaId);
            return View(profesional);
        }

        // GET: Admin/Profesionals/Delete/5
        public ActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesional profesional = db.Profesional.Find(id);
            if (profesional == null)
            {
                return HttpNotFound();
            }
            ViewBag.clinicas = db.Clinica_profesionales
            .Include("Clinica")
            .Where(c => c.id_profesional == id)
            .ToList();
            ViewBag.especialidades = db.Especialidad
                .ToList();
            return View(profesional);
        }

        // POST: Admin/Profesionals/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Profesional profesional = db.Profesional.Find(id);
            profesional.Activo = false;
            db.Entry(profesional).State = EntityState.Modified;
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

        public ActionResult Inactivo()
        {
            var profesional = db.Profesional.Include(p => p.Persona);
            return View(profesional
                .Where(p=>p.Activo==false)
                .ToList());
        }
        public ActionResult Activar(int? id, Profesional profesional)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            profesional = db.Profesional.Find(id);
            profesional.Activo = true;
            db.Entry(profesional).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Desactivar(int? id, Profesional profesional)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            profesional = db.Profesional.Find(id);
            profesional.Activo = false;
            db.Entry(profesional).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult RedireccionarClinica(string Nombre)
        {
            int? ClinicaID = 0;
            var Clinica = db.Clinica.Where(C => C.Nombre == Nombre).ToList();
            foreach (var item in Clinica)
            {
                ClinicaID = item.ClinicaId;
            }
            return RedirectToAction("Detalles","Clinicas", new { id = ClinicaID });
        }
        public ActionResult RedireccionarEspecialidad(string nombre)
        {
            int? especialidadID = 0;
            var especialidad = db.Especialidad.Where(E => E.Descripcion == nombre).ToList();
            foreach(var item in especialidad)
            {
                especialidadID = item.EspecialidadId;
            }
            return RedirectToAction("Detalles", "Especialidad", new { id = especialidadID });
        }
    }
}