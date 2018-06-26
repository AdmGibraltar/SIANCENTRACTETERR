using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class CapFacturaLegal
    {

        public int Id_Cd { get; set; }
        public int Id_Fac { get; set; }
        public int Id_Cte { get; set; }
        public string Cte_Nombre { get; set; }
        public double Saldo { get; set; }
        public double Total { get; set; }
        public double Pagado { get; set; }
        public int FacL_Legal { get; set; }
        public string FacL_Comentarios { get; set; }
        public int Fac_Tipo { get; set; }
    }
}
