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
    public partial class RepCobAntiguedadSaldosDet : Telerik.Reporting.Report
    {
        public RepCobAntiguedadSaldosDet()
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
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
    
        }

        private void RepCobAntiguedadSaldosDet_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                Telerik.Reporting.Processing.Report rptq = (Telerik.Reporting.Processing.Report)sender;

                this.textBox3.Value = "Antiguedad de saldos  " + rptq.Parameters["CdiNombre"].Value.ToString();

                this.TxtFiltros.Value = "Fecha de corte: " + Convert.ToDateTime(rptq.Parameters["FechaCorte"].Value.ToString()).ToString("dd/MM/yyy").ToString();

                if (rptq.Parameters["Legal"].Value.ToString() == "1")
                {
                    this.TxtFiltros.Value = this.TxtFiltros.Value + "  Inc. legal: No ";
                }
                else
                {
                    this.TxtFiltros.Value = this.TxtFiltros.Value + "  Inc. legal: Si ";
                }

                this.TxtFiltros.Value = this.TxtFiltros.Value + " Cte. No. " + rptq.Parameters["Cte"].Value.ToString(); ;

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
                this.sqlDataAdapter1.SelectCommand.Parameters["@Cdi"].Value = rptq.Parameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Cte"].Value = rptq.Parameters["Cte"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Legal"].Value = rptq.Parameters["Legal"].Value;

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