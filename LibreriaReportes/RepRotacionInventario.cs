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
    public partial class RepRotacionInventario : Telerik.Reporting.Report
    {
        public RepRotacionInventario()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void RepRotacionInventario_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.textBox3.Value = "Rotación de inventarios " + this.ReportParameters["MesStr"].Value + " " + this.ReportParameters["Anio"].Value;

                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
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