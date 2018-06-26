using System;


namespace CapaEntidad
{
   public class EntradasSalidasCentralDet
    {

        public int Id_Emp { get; set; }
        public int Id_Alm { get; set; }
        public int Id_Tm { get; set; }
        public int Id_MovC { get; set; }
        public int Id_MovCDet { get; set; }
        public bool MovC_Naturaleza { get; set; }
        public int Id_Prd { get; set; }
        public string Prd_Descripcion { get; set; }
        public string Prd_Presentacion { get; set; }
        public int MovC_Cant { get; set; }
        public double MovC_CostoFac { get; set; }
        public double MovC_CostoEst { get; set; }
        public double TotalFac { get; set; }
        public double TotalEst { get; set; }
        public double Variacion { get; set; }
    }
}
