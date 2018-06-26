namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Rep_ContactosAcys.
    /// </summary>
    public partial class Rep_ContactosAcys : Telerik.Reporting.Report
    {
        public Rep_ContactosAcys()
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

        private void Rep_ContactosAcys_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                Telerik.Reporting.Processing.Report rptq = (Telerik.Reporting.Processing.Report)sender;
                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
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