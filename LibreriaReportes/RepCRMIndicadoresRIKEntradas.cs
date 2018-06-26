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
    public partial class RepCRMIndicadoresEntradas : Telerik.Reporting.Report
    {
        public RepCRMIndicadoresEntradas()
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

        private void RepCRMIndicadoresEntradas_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                Telerik.Reporting.Processing.Report rptq = (Telerik.Reporting.Processing.Report)sender;
                this.textBox3.Value = "Entradas | " + rptq.Parameters["RikNombre"].Value + " " + rptq.Parameters["MesStr"].Value + " " + rptq.Parameters["Anio"].Value;


                this.sqlConnection1.ConnectionString = rptq.Parameters["Conexion"].Value.ToString();
                this.sqlDataAdapter1.SelectCommand.Parameters["@CDI"].Value = rptq.Parameters["CDI"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Anio"].Value = rptq.Parameters["Anio"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Mes"].Value = rptq.Parameters["Mes"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Rik"].Value = rptq.Parameters["Rik"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Tipo"].Value = rptq.Parameters["Tipo"].Value; 

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