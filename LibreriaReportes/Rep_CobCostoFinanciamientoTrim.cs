namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Rep_CobCostoFinanciamientoTrim.
    /// </summary>
    public partial class Rep_CobCostoFinanciamientoTrim : Telerik.Reporting.Report
    {
        public Rep_CobCostoFinanciamientoTrim()
        {
            try
            {
                InitializeComponent();
                this.DataSource = null;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            
        }

        private void Rep_CobCostoFinanciamientoTrim_NeedDataSource(object sender, EventArgs e)
        {
            try 
            {

                Telerik.Reporting.Processing.Report rptq = (Telerik.Reporting.Processing.Report)sender;               
                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                this.sqlDataAdapter1.SelectCommand.Parameters["@Cd"].Value = this.ReportParameters["Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaCorte"].Value = this.ReportParameters["FechaCorte"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Tasa"].Value = this.ReportParameters["Tasa"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@TipoReporte"].Value = this.ReportParameters["TipoReporte"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Trimestre"].Value = this.ReportParameters["Trimestre"].Value;
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }
    }
}