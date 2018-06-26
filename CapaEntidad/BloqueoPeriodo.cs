using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
   public class BloqueoPeriodo
    {

       public int Id_cte { get; set; }
       public int Id_cd { get; set; }
       public string Cte_nombre { get; set; }
       public DateTime Bp_FechaIni { get; set; }
       public DateTime Bp_FechaFin { get; set; }

    }
}
