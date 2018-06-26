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
    public partial class RepPolizaResumen : Telerik.Reporting.Report
    {
        public RepPolizaResumen()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void RepPolizaResumen_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                string[] arrayFechaInicial = this.ReportParameters["FechaIni"].Value.ToString().Split(new char[] { '/' });
                string[] arrayFechaFinal = this.ReportParameters["FechaFin"].Value.ToString().Split(new char[] { '/' });
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
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Alm"].Value = this.ReportParameters["Id_Alm"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Tm"].Value = this.ReportParameters["Id_Tm"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaIni"].Value = FIni;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaFin"].Value = FFin;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Tipo"].Value = this.ReportParameters["Tipo"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@VistaPrevia"].Value = this.ReportParameters["VistaPrevia"].Value;
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}