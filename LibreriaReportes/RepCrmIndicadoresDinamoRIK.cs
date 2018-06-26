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
    public partial class RepIndicadoresDinamoRIK : Telerik.Reporting.Report
    {
        public RepIndicadoresDinamoRIK()
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

        private void RepCrmIndicadoresDinamoRIK_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                Telerik.Reporting.Processing.Report rptq = (Telerik.Reporting.Processing.Report)sender;
                this.textBox3.Value = "INDICADORES DINAMO | " + rptq.Parameters["CDINombre"].Value + " " + rptq.Parameters["MesStr"].Value + " " + rptq.Parameters["Anio"].Value;

                if (this.ReportParameters["TipoRik"].Value == "2")
                {
                    this.textBox3.Value = this.textBox3.Value + " | Menor a 8 meses";
                }
                else if (this.ReportParameters["TipoRik"].Value == "3")
                {
                    this.textBox3.Value = this.textBox3.Value + " | Mayor o igual a 8 meses";
                }

                this.sqlConnection1.ConnectionString = rptq.Parameters["Conexion"].Value.ToString();
                this.sqlDataAdapter1.SelectCommand.Parameters["@CDI"].Value = rptq.Parameters["CDI"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Anio"].Value = rptq.Parameters["Anio"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Mes"].Value = rptq.Parameters["Mes"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@TipoRik"].Value = rptq.Parameters["TipoRik"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@TipoCD"].Value = rptq.Parameters["TipoCD"].Value;
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