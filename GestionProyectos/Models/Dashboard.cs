using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionProyectos.Models
{
    public class Dashboard
    {
        public List<PROYECTO> PROYECTO { get; set; }
        public List<REQUERIMIENTO> REQUERIMIENTO  { get; set; }
        public List<TAREA> TAREA { get; set; }
    }
}