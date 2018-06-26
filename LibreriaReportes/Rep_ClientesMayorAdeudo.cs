namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Collections;
    using CapaEntidad;
    using System.Web;
    

    /// <summary>
    /// Summary description for Rep_ClientesMayorAdeudo.
    /// </summary>
    public partial class Rep_ClientesMayorAdeudo : Telerik.Reporting.Report
    {
         public Rep_ClientesMayorAdeudo()
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

        private void Rep_ClientesMayorAdeudo_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                Telerik.Reporting.Processing.Report rptq = (Telerik.Reporting.Processing.Report)sender;
                this.sqlConnection1.ConnectionString = rptq.Parameters["Conexion"].Value.ToString();
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaCorte"].Value = this.ReportParameters["FechaCorte"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Dias"].Value = this.ReportParameters["Dias"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@DiasRevision"].Value = this.ReportParameters["DiasRevision"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Vencimiento"].Value = this.ReportParameters["Vencimiento"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@SaldoMinimo"].Value = this.ReportParameters["SaldoMinimo"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Meses"].Value = this.ReportParameters["Meses"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Legal"].Value = this.ReportParameters["Legal"].Value;
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