namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Rep_cobCostoFinanciamientoResumen2.
    /// </summary>
    public partial class Rep_cobCostoFinanciamientoRes : Telerik.Reporting.Report
    {
        public Rep_cobCostoFinanciamientoRes()
        {
            try
            {
                InitializeComponent();
                this.DataSource = null;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        private void Rep_cobCostoFinanciamientoRes_NeedDataSource(object sender, EventArgs e)
        {
            try
            {

                Telerik.Reporting.Processing.Report rptq = (Telerik.Reporting.Processing.Report)sender;
                this.sqlConnection1.ConnectionString = rptq.Parameters["Conexion"].Value.ToString();

                string fecha = Convert.ToDateTime(rptq.Parameters["FechaCorte"].Value.ToString()).ToString("dd/MM/yyy");
                string[] arrayFechaCorte = fecha.Split(new char[] { '/' });
                string FCorte = null;
                if (arrayFechaCorte.Length == 3)
                {
                    FCorte = string.Concat(arrayFechaCorte[2], ".", arrayFechaCorte[1], ".", arrayFechaCorte[0]) + " 23:59:59.000";
                }
                this.sqlDataAdapter1.SelectCommand.Parameters["@Cd"].Value = rptq.Parameters["Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaCorte"].Value = rptq.Parameters["FechaCorte"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Tasa"].Value = rptq.Parameters["Tasa"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@TipoReporte"].Value = 3;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Trimestre"].Value = rptq.Parameters["Trimestre"].Value;

                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}