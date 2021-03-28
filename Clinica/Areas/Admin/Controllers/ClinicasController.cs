using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Clinica;
using Clinica.Filtros;

namespace Clinica.Areas.Admin.Controllers
{
    //[AdminFilters(Global.eTipoUsuario.SuperAdministrador)]
    public partial class ClinicasController : Controller
    {
        public static int? id_clinica = 0;
        private ClinicaEntidad db = new ClinicaEntidad();

        // GET: Admin/Clinicas
        public ActionResult Index()
        {
            return View(db.Clinica.ToList());
        }

        // GET: Admin/Clinicas/Details/5
        public ActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinica clinica = db.Clinica.Find(id);
            id_clinica = Convert.ToInt32(id);

            if (clinica == null)
            {
                return HttpNotFound();
            }

            ViewBag.Sucursales = db.Sucursal
                .Include("Localidad")
                .Include("Localidad.Provincia")
                .Where(s => s.ClinicaId == id)
                .ToList();
            ViewBag.Dias = new SelectList(db.DiasSemana, "id_dia", "Descripcion");
            ViewBag.Profesionales = db.spProfesionalenClinica(id).ToList();

            ViewBag.Provincias = new SelectList(db.Provincia, "ProvinciaId", "Descripcion");

            return View(clinica);
        }


        // GET: Admin/Clinicas/Create
        public ActionResult Crear()
        {
            return View();
        }
        [HttpPost]

        //[ValidateAntiForgeryToken]
        public ActionResult Crear(Clinica clinica, HttpPostedFileBase file)
        {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        var filename = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/imagenes/Clinica_logos"), filename);
                        file.SaveAs(path);  
                        clinica.Photopath = "/imagenes/Clinica_logos"+ filename;
                    }
                    else
                    {
                        clinica.Photopath = null;
                    }
                    clinica.FechaCreacion = DateTime.Now;
                    clinica.FechaModificacion = DateTime.Now;
                    db.Clinica.Add(clinica);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(clinica);
        }

        // GET: Admin/Clinicas/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinica clinica = db.Clinica.Find(id);
            if (clinica == null)
            {
                return HttpNotFound();
            }
            return View(clinica);
        }

        // POST: Admin/Clinicas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Editar(Clinica clinica,HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                if (File != null)
                {
                    if (clinica.Photopath != null)
                    {
                        var file = Path.Combine(Server.MapPath("~/imagenes/Clinica_logos/"), clinica.Photopath);
                        if (System.IO.File.Exists(file)) System.IO.File.Delete(file);
                    }
                    var filename = Path.GetFileName(File.FileName);
                    var path = Path.Combine(Server.MapPath("~/imagenes/Clinica_logos"), filename);
                    File.SaveAs(path);
                    clinica.Photopath = "/imagenes/Clinica_logos/" + filename;
                }
                else
                {
                    clinica.Photopath = clinica.Photopath;
                }
                //int id = clinica.ClinicaId;
                //Clinica clinicas = db.Clinica.Find(id);
                //clinica.FechaCreacion = clinicas.FechaCreacion;
                clinica.FechaModificacion = DateTime.Now;
                db.Entry(clinica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clinica);
        }

        // GET: Admin/Clinicas/Delete/5
        public ActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinica clinica = db.Clinica.Find(id);
            if (clinica == null)
            {
                return HttpNotFound();
            }
            return View(clinica);
        }

        // POST: Admin/Clinicas/Delete/5
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clinica clinica = db.Clinica.Find(id);
            db.Clinica.Remove(clinica);
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

        [HttpPost]
        public ActionResult CrearSucursal(int? id, Sucursal sucursal, HttpPostedFileBase File)
        {
            if (File != null)
            {
                if (sucursal.Photopath != null)
                {
                    var file = Path.Combine(Server.MapPath("~/imagenes/Clinica_logos/"), sucursal.Photopath);
                    if (System.IO.File.Exists(file)) System.IO.File.Delete(file);
                }
                var filename = Path.GetFileName(File.FileName);
                var path = Path.Combine(Server.MapPath("~/imagenes/Clinica_logos"), filename);
                File.SaveAs(path);
                sucursal.Photopath = "/imagenes/Clinica_logos/" + filename;
            }
            sucursal.ClinicaId = id;
            sucursal.SucursalId = null;
            db.Sucursal.Add(sucursal);
            db.SaveChanges();
            return View("Index");
        }
    }

}
