namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using CapaEntidad;
    using System.Web;

    /// <summary>
    /// Summary description for RepPolizaCostosResumen.
    /// </summary>
    public partial class RepPolizaRevaluacionResumen : Telerik.Reporting.Report
    {
        public RepPolizaRevaluacionResumen()
        {
            try
            {
                InitializeComponent();
                this.DataSource = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void RepPolizaCostosResumen_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Alm"].Value = this.ReportParameters["Id_Alm"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaIni"].Value = this.ReportParameters["FechaIni"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaFin"].Value = this.ReportParameters["FechaFin"].Value;
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