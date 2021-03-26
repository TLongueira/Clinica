using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Clinica.Global;
using Clinica;
using Clinica.Filtros;


namespace Clinica.Areas.Admin.Controllers
{
    [SesionFilters(Global.eTipoUsuario.SuperAdministrador)]
    public class UsuariosController : Controller
    {
        private ClinicaEntidad db = new ClinicaEntidad();

        // GET: Admin/Usuarios
        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Rol).Include(u => u.TipoUsuario);
            return View(usuarios.ToList());
        }

        // GET: Admin/Usuarios/Details/5
        public ActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Admin/Usuarios/Create
        public ActionResult Crear()
        {
            ViewBag.RolId = new SelectList(db.Rol, "RolId", "Descripcion");
            ViewBag.TipoUsuarioId = new SelectList(db.TipoUsuario, "TipoUsuarioId", "Descripcion");
            return View();
        }

        // POST: Admin/Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear([Bind(Include = "UsuarioId,NombreUsuario,Clave,TipoUsuarioId,RolId,Activo,Creado,Modificado")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                usuarios.Activo = true;
                usuarios.Creado = DateTime.Now;
                usuarios.Modificado = DateTime.Now;
                usuarios.Clave = Hasher.Hash(usuarios.Clave);
                usuarios.RolId = usuarios.TipoUsuarioId = 3;
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RolId = new SelectList(db.Rol, "RolId", "Descripcion", usuarios.RolId);
            ViewBag.TipoUsuarioId = new SelectList(db.TipoUsuario, "TipoUsuarioId", "Descripcion", usuarios.TipoUsuarioId);
            return View(usuarios);
        }

        // GET: Admin/Usuarios/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.RolId = new SelectList(db.Rol, "RolId", "Descripcion", usuarios.RolId);
            ViewBag.TipoUsuarioId = new SelectList(db.TipoUsuario, "TipoUsuarioId", "Descripcion", usuarios.TipoUsuarioId);
            return View(usuarios);
        }

        // POST: Admin/Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "UsuarioId,NombreUsuario,Clave,TipoUsuarioId,RolId,Activo,Creado,Modificado")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RolId = new SelectList(db.Rol, "RolId", "Descripcion", usuarios.RolId);
            ViewBag.TipoUsuarioId = new SelectList(db.TipoUsuario, "TipoUsuarioId", "Descripcion", usuarios.TipoUsuarioId);
            return View(usuarios);
        }

        // GET: Admin/Usuarios/Delete/5
        public ActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Admin/Usuarios/Delete/5
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarios);
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
