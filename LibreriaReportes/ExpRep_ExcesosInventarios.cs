﻿namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using CapaEntidad;
    using CapaNegocios;
   
    public partial class ExpRep_ExcesosInventarios : Telerik.Reporting.Report
    {
        public ExpRep_ExcesosInventarios()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //            
            //
        }

        private void Rep_ExcesosInventarios_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                //this.sqlConnection2.ConnectionString = this.ReportParameters["@Conexion"].Value.ToString();
                ////Transfer the ReportParameter value to the parameter of the select command
                //this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["@Id_Emp"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["@Id_Cd_Ver"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@Territorios"].Value = this.ReportParameters["@Territorios"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@Clientes"].Value = this.ReportParameters["@Clientes"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@Productos"].Value = this.ReportParameters["@Productos"].Value;

                ////Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                //Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                //report.DataSource = this.sqlDataAdapter1;




                //this.sqlConnection2.ConnectionString = this.ReportParameters["Emp_Cnx"].Value.ToString();
                ////Transfer the ReportParameter value to the parameter of the select command
                //this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.ReportParameters["Id_Emp"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd_Ver"].Value = this.ReportParameters["Id_Cd"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@proveedor"].Value = this.ReportParameters["Tipo_prov"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@ExcesoRota"].Value = this.ReportParameters["Exceso"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@Id_prd_RI"].Value = this.ReportParameters["Producto"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@Id_prd_RF"].Value = this.ReportParameters["Producto"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@Id_TipoProd"].Value = this.ReportParameters["Tipo"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@TipoFecha"].Value = this.ReportParameters["Mostrar"].Value;
                //this.sqlDataAdapter1.SelectCommand.Parameters["@Fecha"].Value = this.ReportParameters["Fecha"].Value;

                ////Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                //Telerik.Reporting.Processing.Report report2 = (Telerik.Reporting.Processing.Report)sender;
                //report2.DataSource = this.sqlDataAdapter1;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}