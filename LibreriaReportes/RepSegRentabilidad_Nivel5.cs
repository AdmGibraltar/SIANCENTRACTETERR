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
    public partial class RepSegRentabilidad_Nivel5 : Telerik.Reporting.Report
    {
        public RepSegRentabilidad_Nivel5()
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

        private void RepSegRentabilidad_Nivel5_NeedDataSource(object sender, EventArgs e)
        {
            try
            {


                Telerik.Reporting.Processing.Report rptq = (Telerik.Reporting.Processing.Report)sender;
                this.TxtFiltros.Value = this.ReportParameters["Filtro"].Value.ToString();

                this.sqlConnection1.ConnectionString = rptq.Parameters["Conexion"].Value.ToString();
                this.sqlDataAdapter1.SelectCommand.Parameters["@TipoCd"].Value = this.ReportParameters["TipoCd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@MesInicial"].Value = this.ReportParameters["MesInicial"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@AnioInicial"].Value = this.ReportParameters["AnioInicial"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@MesFinal"].Value = this.ReportParameters["MesFinal"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@AnioFinal"].Value = this.ReportParameters["AnioFinal"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Nivel"].Value = this.ReportParameters["Nivel"].Value;
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