namespace LibreriaReportes
{
    partial class RepPolizaRevaluacionResumen
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
            Telerik.Reporting.ReportParameter reportParameter8 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter9 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.sqlCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox37 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.47957530617713928D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox8,
            this.textBox10,
            this.textBox11,
            this.textBox12,
            this.textBox13,
            this.textBox22});
            this.detail.Name = "detail";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.000200271606445D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.9997990131378174D), Telerik.Reporting.Drawing.Unit.Cm(0.47447481751441956D));
            this.textBox8.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Style.Visible = true;
            this.textBox8.Value = "=Fields.Id_Cd";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.0130214691162109D), Telerik.Reporting.Drawing.Unit.Cm(0.47437471151351929D));
            this.textBox10.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.Style.Visible = true;
            this.textBox10.Value = "=Fields.Cuenta";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.0133218765258789D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.0099997520446777D), Telerik.Reporting.Drawing.Unit.Cm(0.47447481751441956D));
            this.textBox11.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.Style.Visible = true;
            this.textBox11.Value = "=Fields.SubCuenta";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.02352237701416D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.9764778614044189D), Telerik.Reporting.Drawing.Unit.Cm(0.47437471151351929D));
            this.textBox12.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Style.Visible = true;
            this.textBox12.Value = "=Fields.SSCTA";
            // 
            // textBox13
            // 
            this.textBox13.Format = "{0:N2}";
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.000200271606445D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.9995999336242676D), Telerik.Reporting.Drawing.Unit.Cm(0.47447481751441956D));
            this.textBox13.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Style.Visible = true;
            this.textBox13.Value = "=Fields.Cargo";
            // 
            // textBox22
            // 
            this.textBox22.Format = "{0:N2}";
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(20D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.0998973846435547D), Telerik.Reporting.Drawing.Unit.Cm(0.47447481751441956D));
            this.textBox22.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox22.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox22.Style.Visible = true;
            this.textBox22.Value = "=Fields.Credito";
            // 
            // sqlConnection1
            // 
            this.sqlConnection1.ConnectionString = "Data Source=10.1.0.120;Initial Catalog=SIANCENTRAL;Persist Security Info=True;Use" +
                "r ID=sa;Password=sistemas";
            this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlCommand1
            // 
            this.sqlCommand1.CommandText = "SpPolizaCostos_Resumen";
            this.sqlCommand1.CommandTimeout = 600;
            this.sqlCommand1.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlCommand1.Connection = this.sqlConnection1;
            this.sqlCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Id_Emp", System.Data.SqlDbType.Int, 1, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, "3"),
            new System.Data.SqlClient.SqlParameter("@Id_Cd", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, "2016"),
            new System.Data.SqlClient.SqlParameter("@Id_Alm", System.Data.SqlDbType.Int),
            new System.Data.SqlClient.SqlParameter("@FechaIni", System.Data.SqlDbType.DateTime),
            new System.Data.SqlClient.SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime),
            new System.Data.SqlClient.SqlParameter("@VistaPrevia", System.Data.SqlDbType.Bit)});
            // 
            // sqlDataAdapter1
            // 
            this.sqlDataAdapter1.SelectCommand = this.sqlCommand1;
            this.sqlDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "SpPolizaCostos_Resumen", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ID_CD", "ID_CD"),
                        new System.Data.Common.DataColumnMapping("Column1", "Column1"),
                        new System.Data.Common.DataColumnMapping("Cuenta", "Cuenta"),
                        new System.Data.Common.DataColumnMapping("SubCuenta", "SubCuenta"),
                        new System.Data.Common.DataColumnMapping("SSCTA", "SSCTA"),
                        new System.Data.Common.DataColumnMapping("Descripcion", "Descripcion"),
                        new System.Data.Common.DataColumnMapping("Costo", "Costo")})});
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.49999979138374329D);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox24,
            this.textBox23,
            this.textBox14});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // textBox24
            // 
            this.textBox24.Format = "{0:N2}";
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.000200271606445D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.9995982646942139D), Telerik.Reporting.Drawing.Unit.Cm(0.47447481751441956D));
            this.textBox24.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Inset;
            this.textBox24.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Inset;
            this.textBox24.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox24.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox24.Style.Font.Bold = true;
            this.textBox24.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox24.Style.Visible = true;
            this.textBox24.Value = "=SUM(Fields.Cargo)";
            // 
            // textBox23
            // 
            this.textBox23.Format = "{0:N2}";
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(20D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.0998973846435547D), Telerik.Reporting.Drawing.Unit.Cm(0.49989962577819824D));
            this.textBox23.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Inset;
            this.textBox23.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Inset;
            this.textBox23.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox23.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox23.Style.Font.Bold = true;
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox23.Style.Visible = true;
            this.textBox23.Value = "=SUM(Fields.Credito)";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(16.000001907348633D), Telerik.Reporting.Drawing.Unit.Cm(0.47437471151351929D));
            this.textBox14.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox14.Style.Font.Bold = true;
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox14.Style.Visible = true;
            this.textBox14.Value = "Total TM. 2 - COMPRAS";
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(2.8000001907348633D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox20,
            this.textBox19,
            this.textBox18,
            this.textBox1,
            this.textBox21,
            this.textBox2,
            this.textBox4,
            this.textBox5,
            this.textBox6,
            this.textBox7,
            this.textBox32,
            this.textBox31,
            this.textBox30,
            this.textBox29,
            this.textBox37,
            this.textBox27,
            this.textBox15,
            this.textBox16,
            this.textBox17,
            this.textBox25});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.0002002716064453D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(13.89959716796875D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox20.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox20.Style.Font.Bold = true;
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox20.Style.Visible = true;
            this.textBox20.Value = "Key Química S.A. de C.V.";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.0002002716064453D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(13.89959716796875D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox19.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox19.Style.Font.Bold = true;
            this.textBox19.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox19.Style.Visible = true;
            this.textBox19.Value = "SIANWeb Central";
            // 
            // textBox18
            // 
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.0002002716064453D), Telerik.Reporting.Drawing.Unit.Cm(1.200200080871582D));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.3996000289917D), Telerik.Reporting.Drawing.Unit.Cm(0.80521661043167114D));
            this.textBox18.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox18.Style.Font.Bold = true;
            this.textBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox18.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.textBox18.Style.Visible = true;
            this.textBox18.Value = "Póliza de Revaluación de Inventarios";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.000000953674316D), Telerik.Reporting.Drawing.Unit.Cm(1.200200080871582D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(10.099793434143066D), Telerik.Reporting.Drawing.Unit.Cm(0.79960024356842041D));
            this.textBox1.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.textBox1.Style.Visible = true;
            this.textBox1.Value = "=Fields.MesNombre";
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(20D), Telerik.Reporting.Drawing.Unit.Cm(2D));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.0998988151550293D), Telerik.Reporting.Drawing.Unit.Cm(0.60042470693588257D));
            this.textBox21.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.textBox21.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Inset;
            this.textBox21.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox21.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Inset;
            this.textBox21.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox21.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox21.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox21.Style.Font.Bold = true;
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox21.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox21.Style.Visible = true;
            this.textBox21.Value = "Crédito";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.000200271606445D), Telerik.Reporting.Drawing.Unit.Cm(2.0000004768371582D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.9997990131378174D), Telerik.Reporting.Drawing.Unit.Cm(0.60042470693588257D));
            this.textBox2.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.textBox2.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Inset;
            this.textBox2.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox2.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Inset;
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Style.Visible = true;
            this.textBox2.Value = "Almacén";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(2D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.0130214691162109D), Telerik.Reporting.Drawing.Unit.Cm(0.60042470693588257D));
            this.textBox4.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.textBox4.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Inset;
            this.textBox4.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox4.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Inset;
            this.textBox4.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox4.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox4.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Style.Visible = true;
            this.textBox4.Value = "Cuenta";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.0133218765258789D), Telerik.Reporting.Drawing.Unit.Cm(2D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.0100002288818359D), Telerik.Reporting.Drawing.Unit.Cm(0.600000262260437D));
            this.textBox5.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.textBox5.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Inset;
            this.textBox5.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox5.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Inset;
            this.textBox5.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox5.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox5.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.Style.Visible = true;
            this.textBox5.Value = "SubCuenta";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.02352237701416D), Telerik.Reporting.Drawing.Unit.Cm(2D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.9764778614044189D), Telerik.Reporting.Drawing.Unit.Cm(0.60042470693588257D));
            this.textBox6.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.textBox6.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Inset;
            this.textBox6.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Inset;
            this.textBox6.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox6.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox6.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Style.Visible = true;
            this.textBox6.Value = "Subsubcuenta";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.000200271606445D), Telerik.Reporting.Drawing.Unit.Cm(2D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.9995999336242676D), Telerik.Reporting.Drawing.Unit.Cm(0.60042470693588257D));
            this.textBox7.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Inset;
            this.textBox7.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox7.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Inset;
            this.textBox7.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox7.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox7.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Style.Visible = true;
            this.textBox7.Value = "Cargo";
            // 
            // textBox32
            // 
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.7999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.49999994039535522D));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.textBox32.Style.Font.Bold = false;
            this.textBox32.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox32.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox32.Value = "Todos";
            // 
            // textBox31
            // 
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.49999994039535522D));
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7999998331069946D), Telerik.Reporting.Drawing.Unit.Cm(0.50000017881393433D));
            this.textBox31.Style.Font.Bold = false;
            this.textBox31.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox31.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox31.Value = "Tipo mov.:";
            // 
            // textBox30
            // 
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.7999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.023749962449073792D));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.textBox30.Style.Font.Bold = false;
            this.textBox30.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox30.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox30.Value = "=Parameters.Almacen";
            // 
            // textBox29
            // 
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7999998331069946D), Telerik.Reporting.Drawing.Unit.Cm(0.49999997019767761D));
            this.textBox29.Style.Font.Bold = false;
            this.textBox29.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox29.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox29.Value = "Almacén:";
            // 
            // textBox37
            // 
            this.textBox37.Format = "{0:d}";
            this.textBox37.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.7999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(1.5054166316986084D));
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.textBox37.Style.Font.Bold = false;
            this.textBox37.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox37.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox37.Value = "=Parameters.FechaFin";
            // 
            // textBox27
            // 
            this.textBox27.Format = "{0:d}";
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.7999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(1.0027083158493042D));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.textBox27.Style.Font.Bold = false;
            this.textBox27.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox27.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox27.Value = "=Parameters.FechaIni";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(1.5054166316986084D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7999998331069946D), Telerik.Reporting.Drawing.Unit.Cm(0.50000017881393433D));
            this.textBox15.Style.Font.Bold = false;
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox15.Value = "Fecha final:";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(1.0027083158493042D));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7999998331069946D), Telerik.Reporting.Drawing.Unit.Cm(0.50000017881393433D));
            this.textBox16.Style.Font.Bold = false;
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox16.Value = "Fecha inicial:";
            // 
            // textBox17
            // 
            this.textBox17.Format = "{0:g}";
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(18.899999618530273D), Telerik.Reporting.Drawing.Unit.Cm(0.49999994039535522D));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.199796199798584D), Telerik.Reporting.Drawing.Unit.Cm(0.70000028610229492D));
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox17.Value = "=\"Fecha imp.: \" + Now()";
            // 
            // textBox25
            // 
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(18.899999618530273D), Telerik.Reporting.Drawing.Unit.Cm(0.023749962449073792D));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.1997976303100586D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox25.Value = "=\"Usuario: \" + Parameters.U_Nombre";
            // 
            // reportHeaderSection1
            // 
            this.reportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.47957545518875122D);
            this.reportHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox3,
            this.textBox9});
            this.reportHeaderSection1.Name = "reportHeaderSection1";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.0130214691162109D), Telerik.Reporting.Drawing.Unit.Cm(0.47957545518875122D));
            this.textBox3.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Value = "Tipo de Movimiento\t\t\t\t\t";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.0133218765258789D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(20.086576461791992D), Telerik.Reporting.Drawing.Unit.Cm(0.47957545518875122D));
            this.textBox9.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.textBox9.Style.Font.Bold = true;
            this.textBox9.Value = "2. COMPRAS";
            // 
            // RepPolizaRevaluacionResumen
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.reportFooterSection1,
            this.reportHeaderSection1});
            this.Name = "RepPolizaCostosResumen";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(0.5D), Telerik.Reporting.Drawing.Unit.Mm(0.5D), Telerik.Reporting.Drawing.Unit.Mm(0.5D), Telerik.Reporting.Drawing.Unit.Mm(0.5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            reportParameter1.Name = "Conexion";
            reportParameter2.Name = "Id_Emp";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter3.Name = "Id_Cd";
            reportParameter3.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter4.Name = "Id_Alm";
            reportParameter4.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter5.Name = "FechaIni";
            reportParameter5.Type = Telerik.Reporting.ReportParameterType.DateTime;
            reportParameter6.Name = "FechaFin";
            reportParameter6.Type = Telerik.Reporting.ReportParameterType.DateTime;
            reportParameter7.Name = "U_Nombre";
            reportParameter8.Name = "Almacen";
            reportParameter9.Name = "VistaPrevia";
            reportParameter9.Type = Telerik.Reporting.ReportParameterType.Boolean;
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            this.ReportParameters.Add(reportParameter5);
            this.ReportParameters.Add(reportParameter6);
            this.ReportParameters.Add(reportParameter7);
            this.ReportParameters.Add(reportParameter8);
            this.ReportParameters.Add(reportParameter9);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(24.099897384643555D);
            this.NeedDataSource += new System.EventHandler(this.RepPolizaCostosResumen_NeedDataSource);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox13;
        private System.Data.SqlClient.SqlConnection sqlConnection1;
        private System.Data.SqlClient.SqlCommand sqlCommand1;
        private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox37;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.ReportHeaderSection reportHeaderSection1;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox9;
    }
}