using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Clinica.Filtros;

namespace Clinica.Areas.Admin.Controllers
{

    [SesionFilters(Global.eTipoUsuario.SuperAdministrador)]
    public class DefaultController : Controller
    {
        // GET: Admin/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}