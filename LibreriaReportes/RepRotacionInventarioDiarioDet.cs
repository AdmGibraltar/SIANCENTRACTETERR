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
    public partial class RepRotacionInventarioDiarioDet : Telerik.Reporting.Report
    {
        public RepRotacionInventarioDiarioDet()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void RepRotacionInventarioDiarioDet_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                
                Telerik.Reporting.Processing.Report rptq = (Telerik.Reporting.Processing.Report)sender;

                this.textBox3.Value = "Rotación de inventarios diario " + rptq.Parameters["CDNombre"].Value;

          


                this.sqlConnection1.ConnectionString = rptq.Parameters["Conexion"].Value.ToString();

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value   = rptq.Parameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@TipoRep"].Value = rptq.Parameters["TipoRep"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@TipoCd"].Value  = rptq.Parameters["TipoCd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Anio"].Value    = rptq.Parameters["Anio"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Mes"].Value     = rptq.Parameters["Mes"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Fecha"].Value   = rptq.Parameters["Fecha"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@CDK"].Value     = rptq.Parameters["CDK"].Value;
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