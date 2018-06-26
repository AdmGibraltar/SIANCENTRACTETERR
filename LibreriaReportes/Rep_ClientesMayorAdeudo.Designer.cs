namespace LibreriaReportes
{
    partial class Rep_ClientesMayorAdeudo
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.NavigateToUrlAction navigateToUrlAction1 = new Telerik.Reporting.NavigateToUrlAction();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter4 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter5 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter6 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter7 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter8 = new Telerik.Reporting.ReportParameter();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(2.8999998569488525D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox2,
            this.textBox3,
            this.textBox4,
            this.textBox5,
            this.textBox6,
            this.textBox7});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            this.pageHeaderSection1.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Point(3D);
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.5D), Telerik.Reporting.Drawing.Unit.Cm(0.2000001072883606D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.3999004364013672D), Telerik.Reporting.Drawing.Unit.Cm(0.69999980926513672D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "Key Química S.A. de C.V.";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.5D), Telerik.Reporting.Drawing.Unit.Cm(0.9001997709274292D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.3999004364013672D), Telerik.Reporting.Drawing.Unit.Cm(0.59980005025863647D));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "SIANWeb Central";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.5D), Telerik.Reporting.Drawing.Unit.Cm(1.5001999139785767D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.3999004364013672D), Telerik.Reporting.Drawing.Unit.Cm(0.59960037469863892D));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "Clientes con mas Adeudo";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010006983939092606D), Telerik.Reporting.Drawing.Unit.Cm(2.2999999523162842D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.499699592590332D), Telerik.Reporting.Drawing.Unit.Cm(0.599899172782898D));
            this.textBox4.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox4.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox4.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox4.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox4.Style.Font.Bold = false;
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "Cliente";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.5D), Telerik.Reporting.Drawing.Unit.Cm(2.2999999523162842D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.5999996662139893D), Telerik.Reporting.Drawing.Unit.Cm(0.59989959001541138D));
            this.textBox5.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox5.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox5.Style.Font.Bold = false;
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.Value = "Saldo";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.100199699401856D), Telerik.Reporting.Drawing.Unit.Cm(2.2999999523162842D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.29979944229126D), Telerik.Reporting.Drawing.Unit.Cm(0.59989959001541138D));
            this.textBox6.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox6.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox6.Style.Font.Bold = false;
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "Plazo";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.400199890136719D), Telerik.Reporting.Drawing.Unit.Cm(2.2999999523162842D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(11.199700355529785D), Telerik.Reporting.Drawing.Unit.Cm(0.59989959001541138D));
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox7.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox7.Style.Font.Bold = false;
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Value = "Responsable";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.4000014066696167D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox10,
            this.textBox9,
            this.textBox8,
            this.textBox11});
            this.detail.Name = "detail";
            this.detail.Style.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.detail.Style.LineColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.100200653076172D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.2998003959655762D), Telerik.Reporting.Drawing.Unit.Cm(0.40000060200691223D));
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.Value = "=Fields.DiasCredito";
            // 
            // textBox9
            // 
            this.textBox9.Format = "{0:N2}";
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.5D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.5999996662139893D), Telerik.Reporting.Drawing.Unit.Cm(0.40000021457672119D));
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.Value = "=Fields.SaldoCartera";
            // 
            // textBox8
            // 
            navigateToUrlAction1.Url = "=Fields.Url";
            this.textBox8.Action = navigateToUrlAction1;
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010002215276472271D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.499699592590332D), Telerik.Reporting.Drawing.Unit.Cm(0.40000021457672119D));
            this.textBox8.Style.Color = System.Drawing.Color.DarkBlue;
            this.textBox8.Style.Font.Underline = true;
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "=Fields.Cte_Nombre";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.400199890136719D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(11.199700355529785D), Telerik.Reporting.Drawing.Unit.Cm(0.40000101923942566D));
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.Value = "=Fields.CDI";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.59999847412109375D);
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // sqlConnection1
            // 
            this.sqlConnection1.ConnectionString = "Data Source=10.1.0.120;Initial Catalog=SIANCENTRAL;Persist Security Info=True;Use" +
    "r ID=sa;Password=sistemas";
            this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "dbo.spRepClientesMayorAdeudo";
            this.sqlSelectCommand1.CommandTimeout = 600;
            this.sqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlSelectCommand1.Connection = this.sqlConnection1;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@FechaCorte", System.Data.SqlDbType.DateTime, 8),
            new System.Data.SqlClient.SqlParameter("@Dias", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@DiasRevision", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@Vencimiento", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@SaldoMinimo", System.Data.SqlDbType.Float, 8),
            new System.Data.SqlClient.SqlParameter("@Meses", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@Legal", System.Data.SqlDbType.Int, 4)});
            // 
            // sqlDataAdapter1
            // 
            this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
            // 
            // Rep_ClientesMayorAdeudo
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1});
            this.Name = "Rep_ClientesMayorAdeudo";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.Name = "Conexion";
            reportParameter2.Name = "Dias";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter3.Name = "DiasRevision";
            reportParameter3.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter4.Name = "FechaCorte";
            reportParameter4.Type = Telerik.Reporting.ReportParameterType.DateTime;
            reportParameter5.Name = "SaldoMinimo";
            reportParameter5.Type = Telerik.Reporting.ReportParameterType.Float;
            reportParameter6.Name = "Meses";
            reportParameter6.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter7.Name = "Vencimiento";
            reportParameter7.Type = Telerik.Reporting.ReportParameterType.Boolean;
            reportParameter8.Name = "Legal";
            reportParameter8.Type = Telerik.Reporting.ReportParameterType.Boolean;
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            this.ReportParameters.Add(reportParameter5);
            this.ReportParameters.Add(reportParameter6);
            this.ReportParameters.Add(reportParameter7);
            this.ReportParameters.Add(reportParameter8);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(27.89990234375D);
            this.NeedDataSource += new System.EventHandler(this.Rep_ClientesMayorAdeudo_NeedDataSource);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox7;
        private System.Data.SqlClient.SqlConnection sqlConnection1;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox11;

    }
}