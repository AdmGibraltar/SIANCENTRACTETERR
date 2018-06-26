namespace LibreriaReportes
{
    partial class RepCifrasControl
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.NavigateToReportAction navigateToReportAction1 = new Telerik.Reporting.NavigateToReportAction();
            Telerik.Reporting.InstanceReportSource instanceReportSource1 = new Telerik.Reporting.InstanceReportSource();
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter4 = new Telerik.Reporting.ReportParameter();
            this.groupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.TxtFiltros = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.sqlConnection2 = new System.Data.SqlClient.SqlConnection();
            this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.repCifrasControlDet1 = new LibreriaReportes.RepCifrasControlDet();
            ((System.ComponentModel.ISupportInitialize)(this.repCifrasControlDet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // groupFooterSection
            // 
            this.groupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D);
            this.groupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox10,
            this.textBox11});
            this.groupFooterSection.Name = "groupFooterSection";
            // 
            // textBox10
            // 
            this.textBox10.Format = "{0:N2}";
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.600000381469727D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox10.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox10.Style.Font.Bold = true;
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.Value = "=SUM(Fields.Importe)";
            // 
            // textBox11
            // 
            this.textBox11.Format = "{0:N0}";
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.6102094650268555D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.9897918701171875D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox11.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox11.Style.Font.Bold = true;
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.Value = "=SUM(Fields.NUMERO_TRANSACCIONES)";
            // 
            // groupHeaderSection
            // 
            this.groupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.40000009536743164D);
            this.groupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1});
            this.groupHeaderSection.Name = "groupHeaderSection";
            this.groupHeaderSection.Style.BackgroundColor = System.Drawing.Color.LightGray;
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.6000003814697266D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "=Fields.Tipo";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(3.5D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox4,
            this.textBox2,
            this.textBox3,
            this.textBox17,
            this.textBox5,
            this.TxtFiltros,
            this.textBox6});
            this.pageHeader.Name = "pageHeader";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(-3.6557519678126482E-08D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(14.600000381469727D), Telerik.Reporting.Drawing.Unit.Cm(0.699999988079071D));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Value = "Key Química S.A. de C.V.";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(14.600000381469727D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.Value = "SIANWeb Central";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(1.3000000715255737D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(14.600000381469727D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Value = "Cifras control";
            // 
            // textBox17
            // 
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.59999942779541D), Telerik.Reporting.Drawing.Unit.Cm(2.6999998092651367D));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.0000002384185791D), Telerik.Reporting.Drawing.Unit.Cm(0.80000019073486328D));
            this.textBox17.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox17.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox17.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox17.Value = "No. Transacciones";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(2.6999998092651367D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.59999942779541D), Telerik.Reporting.Drawing.Unit.Cm(0.80000030994415283D));
            this.textBox5.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox5.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.Value = "CDI";
            // 
            // TxtFiltros
            // 
            this.TxtFiltros.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(1.8999998569488525D));
            this.TxtFiltros.Name = "TxtFiltros";
            this.TxtFiltros.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(14.600000381469727D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.TxtFiltros.Style.Font.Bold = true;
            this.TxtFiltros.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.TxtFiltros.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.TxtFiltros.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.TxtFiltros.Value = "";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.59999942779541D), Telerik.Reporting.Drawing.Unit.Cm(2.6999998092651367D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.0000002384185791D), Telerik.Reporting.Drawing.Unit.Cm(0.80000019073486328D));
            this.textBox6.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "Importe";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.40000021457672119D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox8,
            this.textBox7,
            this.textBox9});
            this.detail.Name = "detail";
            this.detail.Style.Font.Bold = false;
            // 
            // textBox8
            // 
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Conexion", "=Parameters.Conexion.Value"));
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Anio", "=Parameters.Anio.Value"));
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Mes", "=Parameters.Mes.Value"));
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("MesStr", "=Parameters.MesStr.Value"));
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("IdCd", "=Fields.CDI"));
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Tipo", "=Fields.Id_Tipo"));
            instanceReportSource1.ReportDocument = this.repCifrasControlDet1;
            navigateToReportAction1.ReportSource = instanceReportSource1;
            this.textBox8.Action = navigateToReportAction1;
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.6000003814697266D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox8.Style.Color = System.Drawing.Color.Blue;
            this.textBox8.Style.Font.Bold = false;
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "=Fields.Cd_Nombre";
            // 
            // textBox7
            // 
            this.textBox7.Format = "{0:#.}";
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.59999942779541D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox7.Style.Font.Bold = false;
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Value = "=Fields.NUMERO_TRANSACCIONES";
            // 
            // textBox9
            // 
            this.textBox9.Format = "{0:N2}";
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.600000381469727D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox9.Style.Font.Bold = false;
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.Value = "=Fields.Importe";
            // 
            // sqlConnection1
            // 
            this.sqlConnection1.ConnectionString = "Data Source=189.206.126.68;Initial Catalog=SIANCentral;User ID=sa";
            this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlConnection2
            // 
            this.sqlConnection2.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlDataAdapter1
            // 
            this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
            this.sqlDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "spCapGenerarPoliza_Imprimir", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("cuenta", "cuenta"),
                        new System.Data.Common.DataColumnMapping("Referencia", "Referencia"),
                        new System.Data.Common.DataColumnMapping("subcuenta", "subcuenta"),
                        new System.Data.Common.DataColumnMapping("subsubcuenta", "subsubcuenta"),
                        new System.Data.Common.DataColumnMapping("Almacen", "Almacen"),
                        new System.Data.Common.DataColumnMapping("TipoMov", "TipoMov"),
                        new System.Data.Common.DataColumnMapping("Cargo", "Cargo"),
                        new System.Data.Common.DataColumnMapping("Abono", "Abono")})});
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "spRepCifrasControl_Nivel1";
            this.sqlSelectCommand1.CommandTimeout = 600;
            this.sqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlSelectCommand1.Connection = this.sqlConnection1;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Anio", System.Data.SqlDbType.Int),
            new System.Data.SqlClient.SqlParameter("@Mes", System.Data.SqlDbType.Int)});
            // 
            // pageFooter
            // 
            this.pageFooter.Bindings.Add(new Telerik.Reporting.Binding("Visible", "=PageCount = PageNumber"));
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.49999985098838806D);
            this.pageFooter.Name = "pageFooter";
            this.pageFooter.PrintOnFirstPage = true;
            // 
            // repCifrasControlDet1
            // 
            this.repCifrasControlDet1.Name = "RepCifrasControl";
            // 
            // RepCifrasControl
            // 
            this.DataSource = this.sqlDataAdapter1;
            group1.GroupFooter = this.groupFooterSection;
            group1.GroupHeader = this.groupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("Fields.Id_Tipo"));
            group1.Name = "group";
            group1.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.Id_Tipo", Telerik.Reporting.SortDirection.Asc));
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection,
            this.groupFooterSection,
            this.pageHeader,
            this.detail,
            this.pageFooter});
            this.Name = "RepCifrasControl";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            reportParameter1.Name = "Conexion";
            reportParameter2.Name = "Anio";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter3.Name = "Mes";
            reportParameter4.Name = "MesStr";
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.Style.Font.Bold = true;
            this.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.Style.Visible = true;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(14.600003242492676D);
            this.NeedDataSource += new System.EventHandler(this.RepCifrasControl_NeedDataSource);
            ((System.ComponentModel.ISupportInitialize)(this.repCifrasControlDet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private System.Data.SqlClient.SqlConnection sqlConnection1;
        private System.Data.SqlClient.SqlConnection sqlConnection2;
        private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.Group group1;
        private Telerik.Reporting.TextBox textBox17;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox TxtFiltros;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.GroupHeaderSection groupHeaderSection;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.GroupFooterSection groupFooterSection;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
        private RepCifrasControlDet repCifrasControlDet1;
    }
}