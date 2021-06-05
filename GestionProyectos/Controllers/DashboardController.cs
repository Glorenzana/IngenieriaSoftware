using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestionProyectos.Models;

namespace GestionProyectos.Controllers
{
    public class DashboardController : Controller
    {
        private GestionProyectosEntitie db_gp;
        // GET: Dashboard
        public ActionResult Index(int rol = 0)
        {
            if(Session["us"] == null || Session["us"].ToString() == string.Empty && Session["rol"].ToString() == string.Empty)
            {
                return View("../Home/Denegado");
            }else
            {
                db_gp = new GestionProyectosEntitie();
                //ViewBag.EstadosTarea = db_gp.ESTADOes.Select(x => new SelectListItem { 
                //    Text = x.DESC_ESTADO,
                //    Value = x.ID_ESTADO.ToString()
                //});

                ViewBag.EstadosTarea = new SelectList(db_gp.ESTADOes.ToList(), "ID_ESTADO", "DESC_ESTADO");
                ViewBag.TipoTarea = new SelectList(db_gp.CAT_TIP_REQUERIMIENTO.ToList(), "ID_CAT_TIP_REQUERIMIENTO","DESC_REQUERIMIENTO");
                ViewBag.Prioridades = new SelectList(db_gp.CAT_PRIORIDAD.ToList(), "ID_CAT_PRIORIDAD","DESC_PRIORIDAD", 1);
                ViewBag.Proyectos = new SelectList(db_gp.PROYECTOes.ToList(), "ID_PROYECTO", "DESC_PROYECTO");


                var tec = (from a in db_gp.TECNICOes
                           join s in db_gp.PLANILLA_RRHH on a.PRH_ID_PLANILLA_RRHH equals s.ID_PLANILLA_RRHH
                           select new
                           {
                               idPerson = a.ID_TECNICO,
                               nombre = s.NOMBRES
                           }).ToList();

                ViewBag.Tecnicos = new SelectList(tec, "idPerson", "nombre");

                List<TAREA> conjuntoTareas = db_gp.TAREAs.ToList();
                List<PROYECTO> conjuntoProyectos = db_gp.PROYECTOes.ToList();
                List<REQUERIMIENTO> conjuntoRequerimientos = db_gp.REQUERIMIENTOes.ToList();

                Dashboard dash = new Dashboard();
                dash.TAREA = conjuntoTareas;
                dash.PROYECTO = conjuntoProyectos;
                dash.REQUERIMIENTO = conjuntoRequerimientos;

                return View(dash);
            }

        }

        [HttpPost]
        public ActionResult ActualizarEstadoTarea(TAREA tarea)
         {
            db_gp = new GestionProyectosEntitie();
            TAREA actualizaTarea = db_gp.TAREAs.FirstOrDefault(t => t.ID_TAREA == tarea.ID_TAREA);
            actualizaTarea.ID_ESTADO = tarea.ID_ESTADO;

            db_gp.Entry(actualizaTarea).State = System.Data.Entity.EntityState.Modified;
            db_gp.SaveChanges();


            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AgregarTarea(int TipoTarea, int Prioridad, string Descripcion)
        {
            db_gp = new GestionProyectosEntitie();
            TAREA nuevaTarea = new TAREA();
            nuevaTarea.ID_EMPLEADO = int.Parse(Session["idUs"].ToString());
            nuevaTarea.ID_TIP_REQUERIMIENTO = TipoTarea;
            nuevaTarea.DESC_TAREA = Descripcion;
            nuevaTarea.ID_PRIORIDAD = Prioridad;
            nuevaTarea.ID_ESTADO = 1;
            nuevaTarea.TIEMPO = DateTime.Now;

            db_gp.TAREAs.Add(nuevaTarea);
            db_gp.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AgregarProyecto(int Tecnico, string Descripcion)
        {
            db_gp = new GestionProyectosEntitie();
            PROYECTO nuevoProyecto = new PROYECTO();
            nuevoProyecto.DESC_PROYECTO = Descripcion;
            nuevoProyecto.TECNICO_ID_TECNICO = Tecnico;
            nuevoProyecto.ESTADO_ID_ESTADO = 1;
            nuevoProyecto.TIEMPO = DateTime.Now;
            db_gp.PROYECTOes.Add(nuevoProyecto);

            db_gp.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult AgregarRequerimiento(int Proyecto, string Descripcion)
        {
            db_gp = new GestionProyectosEntitie();
            REQUERIMIENTO nuevoRequerimiento = new REQUERIMIENTO();


            nuevoRequerimiento.PROYECTO_ID_PROYECTO = Proyecto;
            nuevoRequerimiento.DESC_TAREA = Descripcion;
            nuevoRequerimiento.ESTADO_ID_ESTADO = 1;
            db_gp.REQUERIMIENTOes.Add(nuevoRequerimiento);

            db_gp.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult actualizarProyecto(PROYECTO proyecto)
        {
            db_gp = new GestionProyectosEntitie();
            PROYECTO actualizaProyecto = db_gp.PROYECTOes.FirstOrDefault(t => t.ID_PROYECTO == proyecto.ID_PROYECTO);
            actualizaProyecto.ESTADO_ID_ESTADO = proyecto.ESTADO_ID_ESTADO;
            actualizaProyecto.TECNICO_ID_TECNICO = proyecto.TECNICO_ID_TECNICO;

            db_gp.Entry(actualizaProyecto).State = System.Data.Entity.EntityState.Modified;
            db_gp.SaveChanges();


            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult actualizarRequerimiento(REQUERIMIENTO requerimiento)
        {
            db_gp = new GestionProyectosEntitie();
            REQUERIMIENTO actualizaRequerimiento = db_gp.REQUERIMIENTOes.FirstOrDefault(t => t.ID_TAREA == requerimiento.ID_TAREA);
            actualizaRequerimiento.ESTADO_ID_ESTADO = requerimiento.ESTADO_ID_ESTADO;

            db_gp.Entry(actualizaRequerimiento).State = System.Data.Entity.EntityState.Modified;
            db_gp.SaveChanges();


            return RedirectToAction("Index");
        }
    }
}