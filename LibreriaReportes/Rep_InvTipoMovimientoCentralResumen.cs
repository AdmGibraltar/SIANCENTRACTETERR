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
    public partial class Rep_InvTipoMovimientoCentralResumen : Telerik.Reporting.Report
    {
        public Rep_InvTipoMovimientoCentralResumen()
        {

            InitializeComponent();
            this.DataSource = null;
        }
        private void Rep_InvTipoMovimientoCentralResumen_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
   
                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();

                string[] arrayFechaInicial = this.ReportParameters["fecini"].Value.ToString().Split(new char[] { '/' });
                string[] arrayFechaFinal = this.ReportParameters["fecfin"].Value.ToString().Split(new char[] { '/' });
                string FIni = null;
                string FFin = null;
                if (arrayFechaInicial.Length == 3)
                {
                    FIni = string.Concat(arrayFechaInicial[2], ".", arrayFechaInicial[1], ".", arrayFechaInicial[0]) + " 00:00:00.000";
                }

                if (arrayFechaFinal.Length == 3)
                {
                    FFin = string.Concat(arrayFechaFinal[2], ".", arrayFechaFinal[1], ".", arrayFechaFinal[0]) + " 23:59:59.000";
                }

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Alm"].Value = this.ReportParameters["Id_Alm"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_TM"].Value = this.ReportParameters["Id_Tm"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_MovC"].Value = this.ReportParameters["Id_MovC"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaIn"].Value = FIni;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaFin"].Value = FFin;
                this.sqlDataAdapter1.SelectCommand.Parameters["@TipoPrecio"].Value = this.ReportParameters["Id_Tp"].Value.ToString();

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