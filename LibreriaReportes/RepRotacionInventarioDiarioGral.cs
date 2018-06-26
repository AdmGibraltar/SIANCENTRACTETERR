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
    public partial class RepRotacionInventarioDiarioGral : Telerik.Reporting.Report
    {
        public RepRotacionInventarioDiarioGral()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void RepRotacionInventarioDiarioGral_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                
                Telerik.Reporting.Processing.Report rptq = (Telerik.Reporting.Processing.Report)sender;



                if (rptq.Parameters["TipoCd"].Value.ToString() == "1")
                {
                    this.textBox3.Value = "Resultados generales de la rotación de inventarios promedio CDI";
                }
                else
                {
                    this.textBox3.Value = "Resultados generales de la rotación de inventarios promedio CDC";
                }

                this.sqlConnection1.ConnectionString = rptq.Parameters["Conexion"].Value.ToString();

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value   = rptq.Parameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@TipoRep"].Value = rptq.Parameters["TipoRep"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@TipoCd"].Value  = rptq.Parameters["TipoCd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Anio"].Value    = rptq.Parameters["Anio"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Mes"].Value     = rptq.Parameters["Mes"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Fecha"].Value   = rptq.Parameters["Fecha"].Value;
            
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