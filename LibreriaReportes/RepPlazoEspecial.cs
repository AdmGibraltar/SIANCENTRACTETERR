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
    public partial class RepPlazoEspecial : Telerik.Reporting.Report
    {
        public RepPlazoEspecial()
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

        private void RepCobPlazoEspecial_NeedDataSource(object sender, EventArgs e)
        {
            try
            {


                Telerik.Reporting.Processing.Report rptq = (Telerik.Reporting.Processing.Report)sender;

               

                this.TxtFiltros.Value ="Fecha de corte: " + Convert.ToDateTime(rptq.Parameters["FechaCorte"].Value.ToString()).ToString("dd/MM/yyy").ToString();
                this.TxtFiltros.Value = TxtFiltros.Value + "  Vtas. prom  < " + Convert.ToDouble(rptq.Parameters["SaldoMinimo"].Value).ToString("C2");
                this.TxtFiltros.Value = TxtFiltros.Value + "  Vta. prom últimos " + rptq.Parameters["Meses"].Value.ToString() +  " meses  " ;
                if (rptq.Parameters["Legal"].Value.ToString() == "1")
                {
                    this.TxtFiltros.Value = this.TxtFiltros.Value + "Inc. legal: No ";
                }
                else
                {
                    this.TxtFiltros.Value = this.TxtFiltros.Value + "Inc. legal: Si ";
                }

                if (rptq.Parameters["DesNacionales"].Value.ToString() == "1")
                {
                    this.TxtFiltros.Value = this.TxtFiltros.Value + "Ctas. Nacionales: No ";
                }
                else
                {
                    this.TxtFiltros.Value = this.TxtFiltros.Value + "Ctas. Nacionales: Si ";
                }




                this.sqlConnection1.ConnectionString = rptq.Parameters["Conexion"].Value.ToString();
                string fecha = Convert.ToDateTime(rptq.Parameters["FechaCorte"].Value.ToString()).ToString("dd/MM/yyy");
                string[] arrayFechaCorte = fecha.Split(new char[] { '/' });
                string FCorte = null;
                if (arrayFechaCorte.Length == 3)
                {
                    FCorte = string.Concat(arrayFechaCorte[2], ".", arrayFechaCorte[1], ".", arrayFechaCorte[0]) + " 23:59:59.000";
                }
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaCorte"].Value = FCorte;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Dias"].Value = rptq.Parameters["Dias"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@DiasRevision"].Value = rptq.Parameters["DiasRevision"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@SaldoMinimo"].Value = rptq.Parameters["SaldoMinimo"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Meses"].Value = rptq.Parameters["Meses"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Legal"].Value = rptq.Parameters["Legal"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@DesNacionales"].Value = rptq.Parameters["DesNacionales"].Value;

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