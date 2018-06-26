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
    public partial class RepProductos : Telerik.Reporting.Report
    {
        public RepProductos()
        {

            InitializeComponent();

            this.DataSource = null;
        }

        private void RepProductos_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Prd"].Value = this.ReportParameters["Id_Prd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Ptp"].Value = this.ReportParameters["Id_Ptp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Pvd"].Value = this.ReportParameters["Id_Pvd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cpr"].Value = this.ReportParameters["Id_Cpr"].Value;

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