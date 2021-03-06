namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Data;

    /// <summary>
    /// Summary description for SubSubTablaProductos.
    /// </summary>
    public partial class SubTablaProductosVP : Telerik.Reporting.Report
    {
        public SubTablaProductosVP()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //
            //this.DataSource = null;
            //
       }


        public SubTablaProductosVP(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }




        #region Propiedades
        public string Id_Emp
        {
            get { return this.ReportParameters["Id_Emp"].Value.ToString(); }
            set { this.ReportParameters["Id_Emp"].Value = value; }
        }

        public string Id_Cd
        {
            get { return this.ReportParameters["Id_Cd"].Value.ToString(); }
            set { this.ReportParameters["Id_Cd"].Value = value; }
        }

        public string Id_Vap
        {
            get { return this.ReportParameters["Id_Vap"].Value.ToString(); }
            set { this.ReportParameters["Id_Vap"].Value = value; }
        }

        public string Conexion
        {
            get { return this.ReportParameters[3].Value.ToString(); }
            set { this.ReportParameters[3].Value = value; }
        }
        #endregion

        private void SubTablaProductosVP_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection1.ConnectionString = this.Conexion;
                
                //Transfer the ReportParameter value to the parameter of the select command                    

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.Id_Emp;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.Id_Cd;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Vap"].Value = this.Id_Vap;

                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;
                //DataTable tabla = new DataTable();
                //this.sqlDataAdapter1.Fill(tabla);

                //report.DataSource = tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}