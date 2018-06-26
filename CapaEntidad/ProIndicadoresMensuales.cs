using System;


namespace CapaEntidad
{
    public class ProIndicadoresMensuales
    {

        public int Id_Cd { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public double Ind_AmortNormal { get; set; }
        public double Ind_AmortAntic { get; set; }
        public double Ind_VentasCtasNac { get; set; }
        public double Ind_PolVariacion { get; set; }
        public double Ind_OtrosCostos { get; set; }
        public double Ind_1070 { get; set; }
        public double Ind_1073 { get; set; }
        public double Ind_1074 { get; set; }
        public double Ind_1075 { get; set; }
        public double Ind_1076 { get; set; }

        public int Ob_Dias { get; set; }
        public double Ob_Pesos {get;set;}

    }
}
