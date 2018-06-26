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
    public partial class RepCrmIndicadoresDinamo : Telerik.Reporting.Report
    {
        public RepCrmIndicadoresDinamo()
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

        private void RepCrmIndicadoresDinamo_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                string TipoCd = this.ReportParameters["TipoCD"].Value.ToString() == "1" ? "CDI" : "CDC";  
                this.textBox3.Value = "INDICADORES DINAMO | " +TipoCd + " "+ this.ReportParameters["MesStr"].Value + " " + this.ReportParameters["Anio"].Value;

                if (this.ReportParameters["TipoRik"].Value == "2")
                {
                    this.textBox3.Value = this.textBox3.Value + " | Menor a 8 meses";
                }
                else if (this.ReportParameters["TipoRik"].Value == "3")
                {
                    this.textBox3.Value = this.textBox3.Value + " | Mayor o igual a 8 meses";
                }


                Telerik.Reporting.Processing.Report rptq = (Telerik.Reporting.Processing.Report)sender;

                this.sqlConnection1.ConnectionString = rptq.Parameters["Conexion"].Value.ToString();
                this.sqlDataAdapter1.SelectCommand.Parameters["@Anio"].Value = this.ReportParameters["Anio"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Mes"].Value = this.ReportParameters["Mes"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@TipoRik"].Value = this.ReportParameters["TipoRik"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@TipoCD"].Value = this.ReportParameters["TipoCD"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_U"].Value = this.ReportParameters["Id_U"].Value;
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