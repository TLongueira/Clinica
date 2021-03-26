using Clinica.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinica.Areas.Admin.Controllers
{
    public class AjaxController : Controller
    {
        private ClinicaEntidad db = new ClinicaEntidad();

        // GET: Admin/Ajax
        public JsonResult getLocalidad(int? id)
        {
            var localidades = db.Localidad
                .Where(l => l.ProvinciaId == id)
                .ToList();

            var result = LocalidadesModel.getData(localidades);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}