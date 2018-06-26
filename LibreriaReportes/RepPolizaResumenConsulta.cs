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
    public partial class RepPolizaResumenConsulta : Telerik.Reporting.Report
    {
        public RepPolizaResumenConsulta()
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

                this.sqlDataAdapter1.SelectCommand.Parameters["@Pol_Ano"].Value = this.ReportParameters["Ano"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Pol_Mes"].Value = this.ReportParameters["Mes"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Pol_Version"].Value = this.ReportParameters["Version"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Pol_Tipo"].Value = this.ReportParameters["Tipo"].Value;
  
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