using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
   public class Agrupador
    {
       public int Id_Agp { get; set; }
       public int Id_Cte {get;set;}
       public int Id_Cd { get; set; }
       public string Cte_Nombre { get; set; }
       public string Ag_Descripcion { get; set; }
       public bool Cte_NoBloquear { get; set; }
   }
}
