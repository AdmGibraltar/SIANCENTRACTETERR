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
    public partial class RepFinIncremental : Telerik.Reporting.Report
    {
        public RepFinIncremental()
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

        private void  RepConFinIncremental_NeedDataSource(object sender, EventArgs e)
        {
            try
            {

                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                string fecha = Convert.ToDateTime(this.ReportParameters["FechaCorte"].Value.ToString()).ToString("dd/MM/yyy");
                string[] arrayFechaCorte = fecha.Split(new char[] { '/' });
                string FCorte = null;
                if (arrayFechaCorte.Length == 3)
                {
                    FCorte = string.Concat(arrayFechaCorte[2], ".", arrayFechaCorte[1], ".", arrayFechaCorte[0]) + " 23:59:59.000";
                }
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaCorte"].Value = FCorte;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Dias"].Value = this.ReportParameters["Dias"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@DiasRevision"].Value = this.ReportParameters["DiasRevision"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Vencimiento"].Value = this.ReportParameters["Vencimiento"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@SaldoMinimo"].Value = this.ReportParameters["SaldoMinimo"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Meses"].Value = this.ReportParameters["Meses"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Legal"].Value = this.ReportParameters["Legal"].Value;

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