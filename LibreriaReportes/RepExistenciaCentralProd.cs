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
    public partial class RepExistenciaCentralProd : Telerik.Reporting.Report
    {
        public RepExistenciaCentralProd()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void RepExistenciaCentralProd_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();


                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cds"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Inv_Inicial"].Value = this.ReportParameters["AlmIni"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Inv_Final"].Value = this.ReportParameters["AlmFin"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Prd"].Value = this.ReportParameters["Id_Prd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@TipoPrecio"].Value = this.ReportParameters["Id_Tp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Actuales"].Value = this.ReportParameters["Actuales"].Value;
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