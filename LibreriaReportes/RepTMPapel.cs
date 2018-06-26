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
    public partial class RepTMPapel : Telerik.Reporting.Report
    {
        public RepTMPapel()
        {

            InitializeComponent();

            this.DataSource = null;
        }
        
        private void RepTMPapel_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.textBox3.Value = "Reporte T.M. papel " + this.ReportParameters["MesStr"].Value + " " + this.ReportParameters["Anio"].Value;

                this.sqlConnection3.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
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