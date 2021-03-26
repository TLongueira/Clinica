using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security;
using Clinica.Global;
using Clinica;

namespace Clinica.Filtros
{
   
    public class SesionFilters: AuthorizeAttribute
    {
        public eTipoUsuario UsuarioTipo { get; set; }
        public SesionFilters(eTipoUsuario tipo)
        {
            this.UsuarioTipo = tipo;

        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string nombre = filterContext.HttpContext.User.Identity.Name;
            if(nombre!="")
            {
                ClinicaEntidad db = new ClinicaEntidad();
                var usuario = db.Usuarios
                    .Where(user => user.NombreUsuario.Equals(nombre))
                    .FirstOrDefault();

                

                if(UsuarioTipo!= eTipoUsuario.Ninguno&& usuario.TipoUsuarioId != (int)UsuarioTipo)
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                    filterContext.HttpContext.Request.Flash("warning", "No tiene acceso al area");
                    
                }
                filterContext.Controller.ViewBag.Usuario = usuario;

            }

            filterContext.Controller.ViewBag.Controller = filterContext.RouteData.Values["controller"];
            filterContext.Controller.ViewBag.Action = filterContext.RouteData.Values["action"];



            if (UsuarioTipo!=eTipoUsuario.Ninguno)
            base.OnAuthorization(filterContext);

           

        }




    }
}