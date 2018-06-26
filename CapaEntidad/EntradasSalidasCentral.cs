using System;
using System.Text;

namespace CapaEntidad
{
    public class EntradasSalidasCentral
    {
      public int  Id_Emp {get; set;}
      public int Id_Alm { get; set; }
      public int Id_Tm { get; set; }
      public int Id_MovC { get; set; }
      public bool MovC_Naturaleza { get; set; }
     
      public string TipoMov { get; set; }
      public DateTime MovC_Fecha { get; set; }
      public string MovC_Referencia { get; set; }
      public string   TotalFac { get; set; }
      public string   TotalCostoEst { get; set; }
      public string   Variacion { get; set; }
      public string AplContable { get; set; }
      public string Almacen { get; set; }
      public string Remitente { get; set; }
      public string Destino { get; set; }
      public string Alm { get; set; }
      public string Tm { get; set; }
      public string Ref { get; set; }
      public string Nat { get; set; }
      public string MovIni { get; set; }
      public string MovFin { get; set; }
      public string Fechaini { get; set; }
      public string Fechafin { get; set; }


    }
}
