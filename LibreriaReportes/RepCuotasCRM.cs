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
    public partial class RepCuotasCRM : Telerik.Reporting.Report
    {
        public RepCuotasCRM()
        {
            try
            {
                InitializeComponent();

                //

                //
                this.DataSource = null;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
    
        }

        private void RepCuotasCRM_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.textBox3.Value = "Cuotas " + this.ReportParameters["MesStr"].Value + " " + this.ReportParameters["Anio"].Value;

                Telerik.Reporting.Processing.Report rptq = (Telerik.Reporting.Processing.Report)sender;

                this.sqlConnection1.ConnectionString = rptq.Parameters["Conexion"].Value.ToString();
                this.sqlDataAdapter1.SelectCommand.Parameters["@Anio"].Value = this.ReportParameters["Anio"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Mes"].Value = this.ReportParameters["Mes"].Value;

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