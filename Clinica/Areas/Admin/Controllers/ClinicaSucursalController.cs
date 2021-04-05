using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Clinica;
using System.Data.Sql;
using System.Net;

namespace Clinica.Areas.Admin.Controllers
{
    public partial class ClinicasController : Controller
    {
        public static int? id_profesional = 0;
        // GET: Admin/ClinicaSucursal
        public ActionResult AsignarProfesional(int? id)
        {
            Clinica clinica = db.Clinica.Find(id);
            ViewBag.Profesionales = db.spProfesionalClinica(id).ToList();
            return View("AgregarProfesional", clinica);
        }
        public ActionResult Asignar(int? id, Clinica_profesionales clinica_Profesionales)
        {
            clinica_Profesionales.id_clinica = id_clinica;
            clinica_Profesionales.id_profesional = id;
            db.Clinica_profesionales.Add(clinica_Profesionales);
            db.SaveChanges();
            return RedirectToAction("Detalles/"+id_clinica);
        }
        public ActionResult Asignarespecialidad(int? id)
        {
            id_profesional = Convert.ToInt32(id);
            ViewBag.especialidades = db.spEspecialidadProfesional(id,id_clinica)
                .Where(e => e.Activo == true)
                .OrderBy(e=>e.Descripcion)
                .ToList();
            return View("AgregarEspecialidad");
        }
        public ActionResult Asignarespecialidades(int? id, Clinica_profesinales_especialidad clinica_Profesinales_Especialidad, Clinica_profesionales clinica_Profesionales)
        {
            ViewBag.CP = db.Clinica_profesionales.Where(cp => cp.id_profesional == id_profesional && cp.id_clinica == id_clinica)
                .ToList();
            int pc = 0;
            foreach (var item in (IList<Clinica_profesionales>)ViewBag.CP)
            {
                pc = item.id_PC;
            }
            clinica_Profesinales_Especialidad.id_cp = pc;
            clinica_Profesinales_Especialidad.id_especialidad = id;
            db.Clinica_profesinales_especialidad.Add(clinica_Profesinales_Especialidad);
            db.SaveChanges();
            Clinica clinica = db.Clinica.Find(id_clinica);
            return RedirectToAction("Index");
        }
        public ActionResult VistaHorario(int? id)
        {
            ViewBag.Sucursal = db.Sucursal
                .Include("Localidad")
                .Include("Localidad.Provincia")
                .Where(p=>p.SucursalId == id)
                .First();
            ViewBag.dia = new SelectList(db.spDiasAtencion(id), "id_dia", "Descripcion");
            return View("AsignarHorarioSucursal");
        }
        [HttpPost]
        public ActionResult AsignarHorarios(Atencion atencion, int? id_dia)
        {
            atencion.DiaId = id_dia;
            db.Atencion.Add(atencion);
            db.SaveChanges();
            return RedirectToAction("Detalles", id_clinica);
        }
        public ActionResult DetalleSucursal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.foto = db.Sucursal.Where(S => S.SucursalId == id).ToList();
            Sucursal sucursal = db.Sucursal.Find(id);
            if(sucursal==null)
            {
                return HttpNotFound();
            }
            return View("Sucursales/Detalles");
        }
        public ActionResult AsignarHorarioProfesional(int? id,int? clinicaID)
        {
            ViewBag.Dias = new SelectList(db.DiasSemana, "id_dia" , "Descripcion");

            ViewBag.Profesionales = db.spProfesionalConEspecialidad(clinicaID).ToList();
            var sucursal = db.Sucursal
                .Where(s => s.SucursalId == id)
                .SingleOrDefault();

            ViewBag.Provincias = new SelectList(db.Atencion.Where(c => c.SucursalId == id), "AtencionId", "Desde");
            ViewBag.Sucursal = sucursal;
            return View("AsignarHorarioProfesional");
        }
        [HttpPost]
        public ActionResult HorarioProfesionalok(int? ProfesionalID, sucursales_profesionales_especialidades spe,int? ClinicaID)
        {
            return View("Sucursales/Detalles");
        }

        public ActionResult EditarSucursal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.foto = db.Sucursal.Where(S => S.SucursalId == id).ToList();
            Sucursal sucursal = db.Sucursal.Find(id);
            if (sucursal == null)
            {
                return HttpNotFound();
            }
            return View("Sucursales/Editar");
        }

    }
}