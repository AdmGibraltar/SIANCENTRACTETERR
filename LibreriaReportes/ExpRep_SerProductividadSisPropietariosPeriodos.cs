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
    public partial class ExpRep_SerProductividadSisPropietariosPeriodos : Telerik.Reporting.Report
    {
        public ExpRep_SerProductividadSisPropietariosPeriodos()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //
            this.DataSource = null;
        }

        private void Rep_InvValorizacionInventarios_NeedDataSource(object sender, EventArgs e)
        {
            this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();

            //Transfer the ReportParameter value to the parameter of the select command
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = Convert.ToInt32(this.ReportParameters["Id_Emp"].Value);
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = Convert.ToInt32(this.ReportParameters["Id_Cd"].Value);
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_SpoStr"].Value = this.ReportParameters["Id_SpoStrDesgloce"].Value.ToString() == string.Empty ? (object)null : this.ReportParameters["Id_SpoStrDesgloce"].Value.ToString();
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_RikStr"].Value = this.ReportParameters["Id_RikStrDesgloce"].Value.ToString() == string.Empty ? (object)null : this.ReportParameters["Id_RikStrDesgloce"].Value.ToString();
            this.sqlDataAdapter1.SelectCommand.Parameters["@Id_CteStr"].Value = this.ReportParameters["Id_CteStrDesgloce"].Value.ToString() == string.Empty ? (object)null : this.ReportParameters["Id_CteStrDesgloce"].Value.ToString();

            this.sqlDataAdapter1.SelectCommand.Parameters["@mes"].Value = Convert.ToInt32(this.ReportParameters["MesInicial"].Value);
            this.sqlDataAdapter1.SelectCommand.Parameters["@Año"].Value = Convert.ToInt32(this.ReportParameters["AnioInicial"].Value);
            this.sqlDataAdapter1.SelectCommand.Parameters["@orden"].Value = Convert.ToInt32(this.ReportParameters["Orden"].Value);
            this.sqlDataAdapter1.SelectCommand.Parameters["@Agrupado"].Value = 0;
            //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
            Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
            report.DataSource = this.sqlDataAdapter1;
        }
    }
}