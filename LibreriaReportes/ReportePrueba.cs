namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for ReportePrueba.
    /// </summary>
    public partial class ReportePrueba : Telerik.Reporting.Report
    {
        public ReportePrueba()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //

            //
            this.DataSource = null;
        }

        private void ReportePrueba_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                //this.sqlConnection2.ConnectionString = this.ReportParameters["@Conexion"].Value.ToString();
                ////Transfer the ReportParameter value to the parameter of the select command
                //this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["@Id_Emp"].Value;
                ////this.sqlDataAdapter1.SelectCommand.Parameters["@Responsable"].Value = this.ReportParameters["@IdUserCombo"].Value;
                ////this.sqlDataAdapter1.SelectCommand.Parameters["@Concepto"].Value = this.ReportParameters["@TipoCompromiso"].Value;
                ////this.sqlDataAdapter1.SelectCommand.Parameters["@FechaIni"].Value = this.ReportParameters["@FechaA"].Value;
                ////this.sqlDataAdapter1.SelectCommand.Parameters["@FechaFin"].Value = this.ReportParameters["@FechaB"].Value;

                ////Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                //Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                //report.DataSource = this.sqlDataAdapter1;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}