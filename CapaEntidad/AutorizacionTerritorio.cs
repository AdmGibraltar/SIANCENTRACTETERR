using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class AutorizacionTerritorio
    {
        public long IdAutorizacion { get; set; }
        public long ClaveTerritorio { get; set; }
        public int IdRepresentante { get; set; }
        public string Territorio { get; set; }
        public bool Activo { get; set; }
        public int Estatus { get; set; }
        public long IdUSolicita { get; set; }
        public System.DateTime FechaSolicitud { get; set; }
        public Nullable<long> IdUAutoriza { get; set; }
        public Nullable<System.DateTime> FechaAutorizo { get; set; }
    }
}
