using System.Web.Mvc;

namespace Clinica.Areas.Paciente
{
    public class PacienteAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Paciente";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Paciente_default",
                "Paciente/{controller}/{action}/{id}",
                new { controller = "Turnos", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}