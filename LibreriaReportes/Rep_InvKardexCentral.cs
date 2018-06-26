namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using CapaNegocios;
    using CapaEntidad;

    /// <summary>
    /// Summary description for RepInvKardex.
    /// </summary>
    public partial class Rep_InvKardexCentral : Telerik.Reporting.Report
    {
        static double id_prd = 0;
        static int inventario = 0;

        public Rep_InvKardexCentral()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //

            //
            this.DataSource = null;

        }

        private void RepInvKardexCentral_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                inventario = 0;
                id_prd = 0;
                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();

                string[] arrayFechaInicial = this.ReportParameters["fecini"].Value.ToString().Split(new char[] { '/' });
                string[] arrayFechaFinal = this.ReportParameters["fecfin"].Value.ToString().Split(new char[] { '/' });
                string FIni = null;
                string FFin = null;
                if (arrayFechaInicial.Length == 3)
                {
                    FIni = string.Concat(arrayFechaInicial[2], ".", arrayFechaInicial[1], ".", arrayFechaInicial[0]) + " 00:00:00.000";
                }

                if (arrayFechaFinal.Length == 3)
                {
                    FFin = string.Concat(arrayFechaFinal[2], ".", arrayFechaFinal[1], ".", arrayFechaFinal[0]) + " 23:59:59.000";
                }

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cds"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Año"].Value = this.ReportParameters["Año"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Mes"].Value = this.ReportParameters["Mes"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Es_FecIni"].Value = FIni;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Es_FecFin"].Value = FFin;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@Es_FecIni"].Value = this.ReportParameters["fecini"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@Es_FecFin"].Value = this.ReportParameters["fecfin"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Inv_Inicial"].Value = this.ReportParameters["AlmIni"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Inv_Final"].Value = this.ReportParameters["AlmFin"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Prd"].Value = this.ReportParameters["Id_Prd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@TipoPrecio"].Value = this.ReportParameters["Id_Tp"].Value.ToString();

                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static double calculainventario(double producto, int inicial, int ent, int sal)
        {
            if (id_prd != producto)
            {
                inventario = inicial;
                id_prd = producto;
            }

            inventario += (ent - sal);
            return inventario;
        }
        public static int inventariofinal()
        {
            return inventario;
        }
        public static int inventarioexistencia()
        {
            return inventario;
        }



    }
}