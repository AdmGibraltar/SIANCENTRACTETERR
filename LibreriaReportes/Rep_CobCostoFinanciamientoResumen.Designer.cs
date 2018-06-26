namespace LibreriaReportes
{
    partial class Rep_CobCostoFinanciamientoResumen
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter4 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter5 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter6 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter7 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.textBox35 = new Telerik.Reporting.TextBox();
            this.textBox34 = new Telerik.Reporting.TextBox();
            this.textBox33 = new Telerik.Reporting.TextBox();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlCommand1 = new System.Data.SqlClient.SqlCommand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(1.2000000476837158D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox2,
            this.textBox3,
            this.textBox4,
            this.textBox5,
            this.textBox6});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.0998001098632812D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox1.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.textBox1.Style.Color = System.Drawing.Color.White;
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "Promedio Trimestral";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.55281662940979D), Telerik.Reporting.Drawing.Unit.Cm(0.49960026144981384D));
            this.textBox2.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "Zona";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.5530164241790772D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.0183320045471191D), Telerik.Reporting.Drawing.Unit.Cm(0.49960026144981384D));
            this.textBox3.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "Sucursal";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.6000003814697266D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2806334495544434D), Telerik.Reporting.Drawing.Unit.Cm(0.49960026144981384D));
            this.textBox4.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "% Vencido\r\n";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.880833625793457D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.4729175567626953D), Telerik.Reporting.Drawing.Unit.Cm(0.49960026144981384D));
            this.textBox5.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.Value = "Costo Financiamiento";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.1000003814697266D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.2536501884460449D), Telerik.Reporting.Drawing.Unit.Cm(0.60000020265579224D));
            this.textBox6.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.textBox6.Style.Color = System.Drawing.Color.White;
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "=Fields.Trim";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.49989920854568481D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox26,
            this.textBox27,
            this.textBox29,
            this.textBox25});
            this.detail.Name = "detail";
            // 
            // textBox26
            // 
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(-5.6458848707308107E-10D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.5528165102005005D), Telerik.Reporting.Drawing.Unit.Cm(0.49989920854568481D));
            this.textBox26.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Double;
            this.textBox26.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Double;
            this.textBox26.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Double;
            this.textBox26.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Double;
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox26.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox26.Value = "=Fields.[Zona]";
            // 
            // textBox27
            // 
            this.textBox27.Format = "{0:N2}";
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.953749656677246D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.40000057220459D), Telerik.Reporting.Drawing.Unit.Cm(0.49989882111549377D));
            this.textBox27.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Double;
            this.textBox27.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Double;
            this.textBox27.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Double;
            this.textBox27.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Double;
            this.textBox27.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox27.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox27.Value = "=Fields.[CostoProm]";
            // 
            // textBox29
            // 
            this.textBox29.Format = "{0:P2}";
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.6000003814697266D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.3464586734771729D), Telerik.Reporting.Drawing.Unit.Cm(0.49989882111549377D));
            this.textBox29.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Double;
            this.textBox29.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Double;
            this.textBox29.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Double;
            this.textBox29.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Double;
            this.textBox29.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox29.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox29.Value = "=Fields.[VencidoProm]/100";
            // 
            // textBox25
            // 
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.5462589263916016D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.0535416603088379D), Telerik.Reporting.Drawing.Unit.Cm(0.49989882111549377D));
            this.textBox25.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Double;
            this.textBox25.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Double;
            this.textBox25.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Double;
            this.textBox25.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Double;
            this.textBox25.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox25.Value = "=Fields.[Sucursal]";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.59999990463256836D);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox35,
            this.textBox34,
            this.textBox33});
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // textBox35
            // 
            this.textBox35.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.1712503433227539D), Telerik.Reporting.Drawing.Unit.Cm(0.10010068118572235D));
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.4000988006591797D), Telerik.Reporting.Drawing.Unit.Cm(0.49989920854568481D));
            this.textBox35.Style.Font.Bold = true;
            this.textBox35.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox35.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox35.Value = "Totales";
            // 
            // textBox34
            // 
            this.textBox34.Format = "{0:N2}";
            this.textBox34.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.953749656677246D), Telerik.Reporting.Drawing.Unit.Cm(0.10010068118572235D));
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.4000015258789062D), Telerik.Reporting.Drawing.Unit.Cm(0.49989882111549377D));
            this.textBox34.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(204)))), ((int)(((byte)(228)))));
            this.textBox34.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox34.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox34.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox34.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox34.Style.Font.Bold = true;
            this.textBox34.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox34.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox34.Value = "=SUM(Fields.[CostoProm])";
            // 
            // textBox33
            // 
            this.textBox33.Format = "{0:P2}";
            this.textBox33.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.6000003814697266D), Telerik.Reporting.Drawing.Unit.Cm(0.10010068118572235D));
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.3535487651824951D), Telerik.Reporting.Drawing.Unit.Cm(0.49989882111549377D));
            this.textBox33.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(204)))), ((int)(((byte)(228)))));
            this.textBox33.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox33.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox33.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox33.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox33.Style.Font.Bold = true;
            this.textBox33.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox33.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox33.Value = "=(SUM(Fields.[VencidoProm])/Count(Fields.[VencidoProm]))/100";
            // 
            // sqlConnection1
            // 
            this.sqlConnection1.ConnectionString = "Data Source=10.1.0.120;Initial Catalog=SIANCENTRAL;Persist Security Info=True;Use" +
    "r ID=sa;Password=sistemas";
            this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlDataAdapter1
            // 
            this.sqlDataAdapter1.SelectCommand = this.sqlCommand1;
            // 
            // sqlCommand1
            // 
            this.sqlCommand1.CommandText = "sp_CobFacturasVencidas";
            this.sqlCommand1.CommandTimeout = 600;
            this.sqlCommand1.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlCommand1.Connection = this.sqlConnection1;
            this.sqlCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Cd", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@FechaCorte", System.Data.SqlDbType.DateTime, 8),
            new System.Data.SqlClient.SqlParameter("@Tasa", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, false, ((byte)(18)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@TipoReporte", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@Trimestre", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@Sub", System.Data.SqlDbType.Int, 4)});
            // 
            // Rep_CobCostoFinanciamientoResumen
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1});
            this.Name = "Rep_CobCostoFinanciamientoResumen";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.Name = "Conexion";
            reportParameter2.Name = "Cd";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter3.Name = "FechaCorte";
            reportParameter3.Type = Telerik.Reporting.ReportParameterType.DateTime;
            reportParameter4.Name = "Tasa";
            reportParameter4.Type = Telerik.Reporting.ReportParameterType.Float;
            reportParameter5.Name = "TipoReporte";
            reportParameter5.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter6.Name = "Trimestre";
            reportParameter6.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter7.Name = "Sub";
            reportParameter7.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter7.Value = "1";
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            this.ReportParameters.Add(reportParameter5);
            this.ReportParameters.Add(reportParameter6);
            this.ReportParameters.Add(reportParameter7);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(15.353750228881836D);
            this.NeedDataSource += new System.EventHandler(this.Rep_CobCostoFinanciamientoResumen_NeedDataSource);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private System.Data.SqlClient.SqlConnection sqlConnection1;
        private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
        private System.Data.SqlClient.SqlCommand sqlCommand1;
        private Telerik.Reporting.TextBox textBox35;
        private Telerik.Reporting.TextBox textBox34;
        private Telerik.Reporting.TextBox textBox33;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox6;
    }
}