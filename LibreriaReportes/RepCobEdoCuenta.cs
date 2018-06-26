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
    public partial class EstadoCuenta : Telerik.Reporting.Report
    {
        public EstadoCuenta()
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

        private void RepEdoCuenta_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                Telerik.Reporting.Processing.Report rptq = (Telerik.Reporting.Processing.Report)sender;

                this.TxtFiltros.Value = "Fecha de corte: " + Convert.ToDateTime(rptq.Parameters["FechaCorte"].Value.ToString()).ToString("dd/MM/yyy").ToString();


                if (rptq.Parameters["TipoCte"].Value.ToString() == "1")
                {
                    this.TxtFiltros.Value = this.TxtFiltros.Value + "   No Cte: " + rptq.Parameters["CteStr"].Value.ToString();

                }
                else if (rptq.Parameters["TipoCte"].Value.ToString() == "2")
                {
                    this.TxtFiltros.Value = this.TxtFiltros.Value + "   Cuenta nacional: " + rptq.Parameters["CteStr"].Value.ToString();
                }
                else
                {
                    this.TxtFiltros.Value = this.TxtFiltros.Value + "   Agrupador: " + rptq.Parameters["CteStr"].Value.ToString();
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
                this.sqlDataAdapter1.SelectCommand.Parameters["@TipoCte"].Value = rptq.Parameters["TipoCte"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Cd"].Value = rptq.Parameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Cte"].Value = rptq.Parameters["Cte"].Value;

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