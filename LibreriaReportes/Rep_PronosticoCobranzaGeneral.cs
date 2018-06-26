namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

  

    /// <summary>
    /// Summary description for Rep_PronosticoCobranzaGeneral.
    /// </summary>
    public partial class Rep_PronosticoCobranzaGeneral : Telerik.Reporting.Report
    {
        public Rep_PronosticoCobranzaGeneral()
        {
            try
            {
                //
                // Required for telerik Reporting designer support
                //
                InitializeComponent();
                this.DataSource = null;
                //
                // TODO: Add any constructor code after InitializeComponent call
                //
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Rep_PronosticoCobranzaGeneral_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                Telerik.Reporting.Processing.Report rptq = (Telerik.Reporting.Processing.Report)sender;
                this.sqlConnection1.ConnectionString = rptq.Parameters["Conexion"].Value.ToString();

                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaCorte"].Value = this.ReportParameters["Fecha"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Meses"].Value = this.ReportParameters["Meses"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["cdi"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Rotacion"].Value = this.ReportParameters["rotacion"].Value;

                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                
                report.DataSource = sqlDataAdapter1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}