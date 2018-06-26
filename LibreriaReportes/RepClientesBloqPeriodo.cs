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
    public partial class RepClientesBloqPeriodo : Telerik.Reporting.Report
    {
        public RepClientesBloqPeriodo()
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

        private void RepClientesBloqPeriodo_NeedDataSource(object sender, EventArgs e)
        {
            try
            {


                Telerik.Reporting.Processing.Report rptq = (Telerik.Reporting.Processing.Report)sender;

                this.sqlConnection1.ConnectionString = rptq.Parameters["Conexion"].Value.ToString();

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