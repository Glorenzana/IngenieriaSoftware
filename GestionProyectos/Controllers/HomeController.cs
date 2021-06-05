using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestionProyectos.Models;

namespace GestionProyectos.Controllers
{
    public class HomeController : Controller
    {
        private GestionProyectosEntitie db_gp;
        public ActionResult Index()
        {
            Session["idUs"] = string.Empty;
            Session["us"] = string.Empty;
            Session["rol"] = string.Empty;
            return View();
        }

        [HttpPost]
        public ActionResult Autenticar(LOGIN login)
        {
            db_gp = new GestionProyectosEntitie();
            LOGIN usuario = db_gp.LOGINS.FirstOrDefault(b => b.USUARIO == login.USUARIO && b.CONTRASEÑA == login.CONTRASEÑA);
            if (usuario is null)
            {
                return View("Denegado");
            }
            else
            {
                PLANILLA_RRHH perfil = db_gp.PLANILLA_RRHH.FirstOrDefault(p => p.ID_PLANILLA_RRHH == usuario.ID_PLANILLA_RRHH);
                Session["idUs"] = perfil.ID_PLANILLA_RRHH;
                Session["us"] = usuario.USUARIO;
                Session["rol"] = perfil.ID_CAT_ROL;
                return RedirectToAction("Index", "Dashboard", new { rol = perfil.ID_CAT_ROL });
            }

        }

    }
}