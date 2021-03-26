using System.Web.Mvc;

namespace Clinica.Areas.Profesionales
{
    public class ProfesionalAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Profesional";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Profesional_default",
                "Profesional/{controller}/{action}/{id}",
                new { controller = "Profesional", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}