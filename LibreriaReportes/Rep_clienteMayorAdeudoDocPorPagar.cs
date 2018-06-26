namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Rep_clienteMayorAdeudoDocPorPagar.
    /// </summary>
    public partial class Rep_clienteMayorAdeudoDocPorPagar : Telerik.Reporting.Report
    {
        public Rep_clienteMayorAdeudoDocPorPagar()
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

        private void Rep_clienteMayorAdeudoDocPorPagar_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                Telerik.Reporting.Processing.Report rptq = (Telerik.Reporting.Processing.Report)sender;
                this.sqlConnection1.ConnectionString = rptq.Parameters["Conexion"].Value.ToString();
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaCorte"].Value = this.ReportParameters["Fecha_Corte"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Dias"].Value = this.ReportParameters["Dias"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@DiasRevision"].Value = this.ReportParameters["Dias_Revision"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Plazo"].Value = 0;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Vencimiento"].Value = this.ReportParameters["Vencidos"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@TipoCte"].Value = 0;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Cd"].Value = this.ReportParameters["Cdi"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Cte"].Value = this.ReportParameters["Id_Cte"].Value;
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