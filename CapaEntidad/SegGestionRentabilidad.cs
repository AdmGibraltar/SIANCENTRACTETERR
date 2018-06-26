using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
  public class SegGestionRentabilidad
    {
        public string Region { get; set; }
        public string CDI { get; set; }
        public string Representante { get; set; }
        public string Cliente { get; set; }
        public double MetaUBImporte { get; set; }
        public double MetaUBPorc { get; set; }
        public double ProyUBImporte { get; set; }
        public double ProyUBPorc { get; set; }
        public double RealUBImporte { get; set; }
        public double RealUBPorc { get; set; }
    }
}
