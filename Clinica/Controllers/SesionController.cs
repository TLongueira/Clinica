using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Clinica.Models;
using Clinica.Global;
using System.Web.Security;

namespace Clinica.Controllers
{
    
    public class SesionController : Controller
    {
        public static int UsuarioId = 0;
        ClinicaEntidad db = new ClinicaEntidad();

        // GET: Sesion
        public ActionResult Iniciar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Iniciar(LoginModel login)
        {
            if(ModelState.IsValid)
            {

                var pass=Hasher.Hash(login.Clave);
                var usuario = db.Usuarios
                    .Where(u => u.NombreUsuario.Equals(login.NombreUsuario) &&
                    u.Clave.Equals(pass))
                    .FirstOrDefault();

                if(usuario !=null)
                {
                    FormsAuthentication.SetAuthCookie(usuario.NombreUsuario, false);

                    if (usuario.TipoUsuarioId == (int)Global.eTipoUsuario.SuperAdministrador)
                    {
                        return RedirectToAction("Index", "Default", new { Area = "Admin", UsuarioId = usuario.UsuarioId });
                    }
                    else if (usuario.TipoUsuarioId == (int)Global.eTipoUsuario.Paciente)
                    {
                        return RedirectToAction("Index", "Turnos", new { Area = "Paciente", UsuarioId = usuario.UsuarioId });
                    }
                    else if(usuario.TipoUsuarioId==(int)Global.eTipoUsuario.Trabajador)
                    {
                        return RedirectToAction("Index", "Profesional", new { Area = "Profesional", UsuarioId = usuario.UsuarioId });
                    }
                }
                else
                {
                    this.Request.Flash("danger", "Nombre de usuario y/o contraseña es incorrecto");
                }
            }

            return View();
        }

        public ActionResult  Salir()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Iniciar");
        }

        public ActionResult Registrarse()
        {
            ViewBag.ObraId = new SelectList(db.obra_social, "ObraId", "Descripcion");
            ViewBag.PersonId = new SelectList(db.Persona, "PersonaId", "Nombre");
            return View();
        }

        [HttpPost]
        public ActionResult Registrarse(Pacientes pacientes, string Contraseña, string Nick)
        {
            if (ModelState.IsValid)
            {
                Usuarios usuarios = new Usuarios();
                usuarios.Activo = true;
                usuarios.Clave = Hasher.Hash(Contraseña);
                usuarios.Modificado = DateTime.Now;
                usuarios.Creado = DateTime.Now;
                usuarios.NombreUsuario = Nick;
                usuarios.RolId = 2;
                usuarios.TipoUsuarioId = 2;
                db.Pacientes.Add(pacientes);
                db.SaveChanges();
                usuarios.id_persona = pacientes.Persona.PersonaId;
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Iniciar", "Sesion");
            }

            ViewBag.ObraId = new SelectList(db.obra_social, "ObraId", "Descripcion", pacientes.ObraId);
            ViewBag.PersonId = new SelectList(db.Persona, "PersonaId", "Nombre", pacientes.PersonId);
            return View(pacientes);
        }
    }
}