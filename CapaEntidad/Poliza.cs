using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Poliza
    {
        public int Id_Emp { get; set; }
        public int Pol_Mes { get; set; }
        public int Pol_Ano { get; set; }
        public int Pol_Version { get; set; }
        public string Pol_Tipo { get; set; }
        public double Pol_Cargo { get; set; }
        public double Pol_Abono { get; set; }

    }

    public class PolizaRev
    {
        public int Id_Emp { get; set; }
        public int Pol_Mes { get; set; }
        public int Pol_Ano { get; set; }
        public int Pol_Version { get; set; }
        public string Pol_Tipo { get; set; }
        public double Pol_Cargo { get; set; }
        public double Pol_Abono { get; set; }

    }
}
