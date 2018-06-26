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
    public partial class RepAlmacenes : Telerik.Reporting.Report
    {
        public RepAlmacenes()
        {

            InitializeComponent();

            this.DataSource = null;
        }
        
        private void RepAlmacenes_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();

       
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