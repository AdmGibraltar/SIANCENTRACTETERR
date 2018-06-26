namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Rep_ClienteMayorAdeudoDocDet.
    /// </summary>
    public partial class Rep_ClienteMayorAdeudoDocDet : Telerik.Reporting.Report
    {
        public Rep_ClienteMayorAdeudoDocDet()
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

        private void Rep_ClienteMayorAdeudoDocDet_NeedDataSource(object sender, EventArgs e)
        {
            //try
            //{
            //    Telerik.Reporting.Processing.Report rptq = (Telerik.Reporting.Processing.Report)sender;
            //    this.sqlConnection1.ConnectionString = rptq.Parameters["Conexion"].Value.ToString();
            //    this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = rptq.Parameters["Id_Cd"].Value;
            //    this.sqlDataAdapter1.SelectCommand.Parameters["@Serie"].Value = rptq.Parameters["Serie"].Value.ToString();
                
            //    Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
            //    report.DataSource = this.sqlDataAdapter1;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

    }
}