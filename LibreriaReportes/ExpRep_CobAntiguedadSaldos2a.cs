﻿namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Rep_InvValorizacionInventarios.
    /// </summary>
    public partial class Rep_CobAntiguedadSaldos2a2 : Telerik.Reporting.Report
    {
        public Rep_CobAntiguedadSaldos2a2()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //            
            //
            this.DataSource = null;
        }

        private void Rep_CobAntiguedadSaldos2a2_NeedDataSource(object sender, EventArgs e)
        {
            this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();

            //Transfer the ReportParameter value to the parameter of the select command
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = Convert.ToInt32(this.ReportParameters["Id_Emp"].Value);
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = Convert.ToInt32(this.ReportParameters["Id_Cd"].Value);
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_CteStr"].Value = this.ReportParameters["Id_CteStrDesgloce"].Value.ToString() == string.Empty ? (object)null : this.ReportParameters["Id_CteStrDesgloce"].Value.ToString();
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_TerStr"].Value = this.ReportParameters["Id_TerStrDesgloce"].Value.ToString() == string.Empty ? (object)null : this.ReportParameters["Id_TerStrDesgloce"].Value.ToString();
            string[] arrayFecha = this.ReportParameters["fechaCorte"].Value.ToString().Split(new char[] { '/' });
            this.sqlDataAdapter1.SelectCommand.Parameters["@fechaCorte"].Value = new DateTime(Convert.ToInt32(arrayFecha[2]), Convert.ToInt32(arrayFecha[1]), Convert.ToInt32(arrayFecha[0]));

            //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
            Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
            report.DataSource = this.sqlDataAdapter1;
        }
    }
}