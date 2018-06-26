namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for RepCumplimientoVIConcen.
    /// </summary>
    public partial class RepCumplimientoVIConcen : Telerik.Reporting.Report
    {
        public RepCumplimientoVIConcen()
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

        private void RepCumplimientoVIConcen_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                this.sqlDataAdapter1.SelectCommand.Parameters["@Anio"].Value = this.ReportParameters["Anio"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Mes"].Value = this.ReportParameters["Mes"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Region"].Value = this.ReportParameters["Region"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_UEN"].Value = this.ReportParameters["Id_UEN"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Seg"].Value = this.ReportParameters["Id_Seg"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Rik"].Value = this.ReportParameters["Id_Rik"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Ter"].Value = this.ReportParameters["Id_Ter"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cte"].Value = this.ReportParameters["Id_Cte"].Value;
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