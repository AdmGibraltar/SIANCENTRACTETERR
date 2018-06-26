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
    public partial class RepEntradasVirtuales : Telerik.Reporting.Report
    {

        public RepEntradasVirtuales()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //

            //
            this.DataSource = null;

        }

        private void RepEntradasVirtuales_NeedDataSource(object sender, EventArgs e)
        {
            try
            {

                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();

                string[] arrayFechaCorte = this.ReportParameters["FechaCorte"].Value.ToString().Split(new char[] { '/' });
                string FCorte = null;
                if (arrayFechaCorte.Length == 3)
                {
                    FCorte = string.Concat(arrayFechaCorte[2], ".", arrayFechaCorte[1], ".", arrayFechaCorte[0]) + " 23:59:59.000";
                }
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_AlmIni"].Value = this.ReportParameters["AlmIni"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_AlmFin"].Value = this.ReportParameters["AlmFin"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaCorte"].Value = FCorte;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Tipo"].Value = this.ReportParameters["Tipo"].Value;
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