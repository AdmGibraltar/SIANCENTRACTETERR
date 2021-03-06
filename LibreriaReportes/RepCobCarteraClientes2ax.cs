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
    /// Summary description for RepCobCarteraClientes1ax.
    /// </summary>
    public partial class RepCobCarteraClientes2ax : Telerik.Reporting.Report
    {
        static double saldo_anterior = 0;
        static int Id_cliente = 0;
        static int Id_territorio = 0;

        public RepCobCarteraClientes2ax()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void RepCobCarteraClientes2ax_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                saldo_anterior = 0;

                this.sqlConnection2.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();

                string[] arrayFCor = this.ReportParameters["FechaCorte"].Value.ToString().Split(new char[] { '/' });
                string FCor = null;
                if (arrayFCor.Length == 3)
                {
                    FCor = string.Concat(arrayFCor[2], ".", arrayFCor[1], ".", arrayFCor[0]);
                }

                //if (this.ReportParameters["DiaVenc"].Value == null)
                //{
                //    this.textBox10.Visible = false;
                //}

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cte"].Value = this.ReportParameters["Id_Cte"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@FechaCorte"].Value = FCor;
                this.sqlDataAdapter1.SelectCommand.Parameters["@DiasVencido"].Value = this.ReportParameters["DiasVencido"].Value;

                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


       
        public static double calcularSaldo(double cargo, int cliente, int territorio)
        {
            if (Id_cliente != cliente || Id_territorio != territorio)
            {
                saldo_anterior = 0;
                Id_cliente = cliente;
                Id_territorio = territorio;
            }

            saldo_anterior += cargo;
            return saldo_anterior;
        }
    }
}