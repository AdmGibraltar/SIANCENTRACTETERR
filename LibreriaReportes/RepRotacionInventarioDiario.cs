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
    public partial class RepRotacionInventarioDiario : Telerik.Reporting.Report
    {
        public RepRotacionInventarioDiario()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void RepRotacionInventarioDiario_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                if (this.ReportParameters["TipoCd"].Value.ToString() == "1")
                {
                    this.textBox3.Value = "Rotación de inventarios diario CDI";
                }
                else
                {
                    this.textBox3.Value = "Rotación de inventarios diario CDC";
                }

                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@TipoRep"].Value = this.ReportParameters["TipoRep"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@TipoCd"].Value = this.ReportParameters["TipoCd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Anio"].Value = this.ReportParameters["Anio"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Mes"].Value = this.ReportParameters["Mes"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Fecha"].Value = this.ReportParameters["Fecha"].Value;
 
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