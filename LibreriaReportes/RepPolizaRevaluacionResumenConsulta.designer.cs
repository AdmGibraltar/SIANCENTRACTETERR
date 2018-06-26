namespace LibreriaReportes
{
    partial class RepPolizaRevaluacionResumenConsulta
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
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox37 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.sqlCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
            this.reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
            this.textBox33 = new Telerik.Reporting.TextBox();
            this.textBox34 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(2.8004248142242432D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox25,
            this.textBox1,
            this.textBox2,
            this.textBox3,
            this.textBox27,
            this.textBox37,
            this.textBox29,
            this.textBox30,
            this.textBox31,
            this.textBox32,
            this.textBox4,
            this.textBox18,
            this.textBox19,
            this.textBox20,
            this.textBox7,
            this.textBox6,
            this.textBox5,
            this.textBox21,
            this.textBox26,
            this.textBox28});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // textBox25
            // 
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(18.899999618530273D), Telerik.Reporting.Drawing.Unit.Cm(0.099999949336051941D));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.1997976303100586D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox25.Value = "=\"Usuario: \" + Parameters.U_Nombre";
            // 
            // textBox1
            // 
            this.textBox1.Format = "{0:g}";
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(18.899999618530273D), Telerik.Reporting.Drawing.Unit.Cm(0.57624995708465576D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.199796199798584D), Telerik.Reporting.Drawing.Unit.Cm(0.50000017881393433D));
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.Value = "=\"Fecha imp.: \" + Now()";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.29999944567680359D), Telerik.Reporting.Drawing.Unit.Cm(1.07895827293396D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7999998331069946D), Telerik.Reporting.Drawing.Unit.Cm(0.50000017881393433D));
            this.textBox2.Style.Font.Bold = false;
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.Value = "Fecha inicial:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.29999944567680359D), Telerik.Reporting.Drawing.Unit.Cm(1.5816665887832642D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7999998331069946D), Telerik.Reporting.Drawing.Unit.Cm(0.50000017881393433D));
            this.textBox3.Style.Font.Bold = false;
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.Value = "Fecha final:";
            // 
            // textBox27
            // 
            this.textBox27.Format = "{0:d}";
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.09999942779541D), Telerik.Reporting.Drawing.Unit.Cm(1.0764498710632324D));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.textBox27.Style.Font.Bold = false;
            this.textBox27.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox27.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox27.Value = "=Fields.FechaIni";
            // 
            // textBox37
            // 
            this.textBox37.Format = "{0:d}";
            this.textBox37.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.09999942779541D), Telerik.Reporting.Drawing.Unit.Cm(1.5816665887832642D));
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.textBox37.Style.Font.Bold = false;
            this.textBox37.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox37.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox37.Value = "=Fields.FechaFin";
            // 
            // textBox29
            // 
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.29999944567680359D), Telerik.Reporting.Drawing.Unit.Cm(0.099999949336051941D));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7999998331069946D), Telerik.Reporting.Drawing.Unit.Cm(0.49999997019767761D));
            this.textBox29.Style.Font.Bold = false;
            this.textBox29.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox29.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox29.Value = "Almacén:";
            // 
            // textBox30
            // 
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.09999942779541D), Telerik.Reporting.Drawing.Unit.Cm(0.099999949336051941D));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.textBox30.Style.Font.Bold = false;
            this.textBox30.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox30.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox30.Value = "Todos";
            // 
            // textBox31
            // 
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.29999944567680359D), Telerik.Reporting.Drawing.Unit.Cm(0.57624995708465576D));
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7999998331069946D), Telerik.Reporting.Drawing.Unit.Cm(0.50000017881393433D));
            this.textBox31.Style.Font.Bold = false;
            this.textBox31.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox31.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox31.Value = "Tipo mov.:";
            // 
            // textBox32
            // 
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.09999942779541D), Telerik.Reporting.Drawing.Unit.Cm(0.57624995708465576D));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D));
            this.textBox32.Style.Font.Bold = false;
            this.textBox32.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox32.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox32.Value = "Todos";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.005208969116211D), Telerik.Reporting.Drawing.Unit.Cm(1.3964582681655884D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7999997138977051D), Telerik.Reporting.Drawing.Unit.Cm(0.60000020265579224D));
            this.textBox4.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.Visible = true;
            this.textBox4.Value = "=Fields.MesNombre";
            // 
            // textBox18
            // 
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.5968756675720215D), Telerik.Reporting.Drawing.Unit.Cm(1.3964582681655884D));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.79979944229126D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox18.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox18.Style.Font.Bold = true;
            this.textBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox18.Style.Visible = true;
            this.textBox18.Value = "Póliza de Revaluación de Inventarios";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.29020881652832D), Telerik.Reporting.Drawing.Unit.Cm(0.682083249092102D));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.9000015258789062D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox19.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox19.Style.Font.Bold = true;
            this.textBox19.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox19.Style.Visible = true;
            this.textBox19.Value = "SIANWeb Central";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.29020881652832D), Telerik.Reporting.Drawing.Unit.Cm(0.099999949336051941D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.8999996185302734D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox20.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox20.Style.Font.Bold = true;
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox20.Style.Visible = true;
            this.textBox20.Value = "Key Química S.A. de C.V.";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.085624694824219D), Telerik.Reporting.Drawing.Unit.Cm(2.2000000476837158D));
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
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.12166690826416D), Telerik.Reporting.Drawing.Unit.Cm(2.2000000476837158D));
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
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.0999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(2.2000000476837158D));
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
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.10479173064231873D), Telerik.Reporting.Drawing.Unit.Cm(2.2000000476837158D));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.0130214691162109D), Telerik.Reporting.Drawing.Unit.Cm(0.60042470693588257D));
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
            this.textBox21.Value = "Cuenta";
            // 
            // textBox26
            // 
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.090415954589844D), Telerik.Reporting.Drawing.Unit.Cm(2.2000000476837158D));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.9997990131378174D), Telerik.Reporting.Drawing.Unit.Cm(0.60042470693588257D));
            this.textBox26.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.textBox26.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Inset;
            this.textBox26.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox26.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Inset;
            this.textBox26.Style.Font.Bold = true;
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox26.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox26.Style.Visible = true;
            this.textBox26.Value = "Almacén";
            // 
            // textBox28
            // 
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(20.080833435058594D), Telerik.Reporting.Drawing.Unit.Cm(2.2000000476837158D));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.0998988151550293D), Telerik.Reporting.Drawing.Unit.Cm(0.60042470693588257D));
            this.textBox28.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.textBox28.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Inset;
            this.textBox28.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox28.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Inset;
            this.textBox28.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox28.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox28.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox28.Style.Font.Bold = true;
            this.textBox28.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox28.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox28.Style.Visible = true;
            this.textBox28.Value = "Crédito";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.49999964237213135D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox22,
            this.textBox13,
            this.textBox12,
            this.textBox11,
            this.textBox10,
            this.textBox8});
            this.detail.Name = "detail";
            // 
            // textBox22
            // 
            this.textBox22.Format = "{0:N2}";
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(20.100000381469727D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.0998973846435547D), Telerik.Reporting.Drawing.Unit.Cm(0.47447481751441956D));
            this.textBox22.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox22.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox22.Style.Visible = true;
            this.textBox22.Value = "=Fields.Credito";
            // 
            // textBox13
            // 
            this.textBox13.Format = "{0:N2}";
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.104791641235352D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.9995999336242676D), Telerik.Reporting.Drawing.Unit.Cm(0.47447481751441956D));
            this.textBox13.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Style.Visible = true;
            this.textBox13.Value = "=Fields.Cargo";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.140833854675293D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.9764778614044189D), Telerik.Reporting.Drawing.Unit.Cm(0.47437471151351929D));
            this.textBox12.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Style.Visible = true;
            this.textBox12.Value = "=Fields.SSCTA";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.04812479019165D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.0099997520446777D), Telerik.Reporting.Drawing.Unit.Cm(0.47447481751441956D));
            this.textBox11.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.Style.Visible = true;
            this.textBox11.Value = "=Fields.SubCuenta";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.0130214691162109D), Telerik.Reporting.Drawing.Unit.Cm(0.47437471151351929D));
            this.textBox10.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.Style.Visible = true;
            this.textBox10.Value = "=Fields.Cuenta";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.109583854675293D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.9997990131378174D), Telerik.Reporting.Drawing.Unit.Cm(0.47447481751441956D));
            this.textBox8.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Style.Visible = true;
            this.textBox8.Value = "=Fields.Id_Cd";
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.500000536441803D);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox14,
            this.textBox23,
            this.textBox24});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.099999949336051941D), Telerik.Reporting.Drawing.Unit.Cm(4.0372211174144468E-07D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(16.000001907348633D), Telerik.Reporting.Drawing.Unit.Cm(0.47437471151351929D));
            this.textBox14.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0D);
            this.textBox14.Style.Font.Bold = true;
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox14.Style.Visible = true;
            this.textBox14.Value = "Total TM. 2 - COMPRAS";
            // 
            // textBox23
            // 
            this.textBox23.Format = "{0:N2}";
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(20.076040267944336D), Telerik.Reporting.Drawing.Unit.Cm(4.0372211174144468E-07D));
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
            // textBox24
            // 
            this.textBox24.Format = "{0:N2}";
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.080833435058594D), Telerik.Reporting.Drawing.Unit.Cm(4.0372211174144468E-07D));
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
            // sqlCommand1
            // 
            this.sqlCommand1.CommandText = "[sp_PolizaRevaluacionConsulta]";
            this.sqlCommand1.CommandTimeout = 600;
            this.sqlCommand1.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlCommand1.Connection = this.sqlConnection1;
            this.sqlCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("Return_value", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@Anio", System.Data.SqlDbType.Int),
            new System.Data.SqlClient.SqlParameter("@Mes", System.Data.SqlDbType.Int),
            new System.Data.SqlClient.SqlParameter("@Tipo", System.Data.SqlDbType.NVarChar)});
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
            // reportHeaderSection1
            // 
            this.reportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.4995749294757843D);
            this.reportHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox33,
            this.textBox34});
            this.reportHeaderSection1.Name = "reportHeaderSection1";
            // 
            // textBox33
            // 
            this.textBox33.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.04812479019165D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(20.086576461791992D), Telerik.Reporting.Drawing.Unit.Cm(0.47957545518875122D));
            this.textBox33.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.textBox33.Style.Font.Bold = true;
            this.textBox33.Value = "2. COMPRAS";
            // 
            // textBox34
            // 
            this.textBox34.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.0130214691162109D), Telerik.Reporting.Drawing.Unit.Cm(0.47957545518875122D));
            this.textBox34.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.textBox34.Style.Font.Bold = true;
            this.textBox34.Value = "Tipo de Movimiento\t\t\t\t\t";
            // 
            // RepPolizaRevaluacionResumenConsulta
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.reportFooterSection1,
            this.reportHeaderSection1});
            this.Name = "RepPolizaRevaluacionResumenConsulta";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(0.5D), Telerik.Reporting.Drawing.Unit.Mm(0.5D), Telerik.Reporting.Drawing.Unit.Mm(0.5D), Telerik.Reporting.Drawing.Unit.Mm(0.5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            reportParameter1.Name = "Conexion";
            reportParameter2.Name = "anio";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter3.Name = "mes";
            reportParameter3.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter4.Name = "Tipo";
            reportParameter5.Name = "U_Nombre";
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            this.ReportParameters.Add(reportParameter5);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(24.199897766113281D);
            this.NeedDataSource += new System.EventHandler(this.RepPolizaRevaluacionResumenConsulta_NeedDataSource);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.ReportFooterSection reportFooterSection1;
        private System.Data.SqlClient.SqlCommand sqlCommand1;
        private System.Data.SqlClient.SqlConnection sqlConnection1;
        private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox37;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.ReportHeaderSection reportHeaderSection1;
        private Telerik.Reporting.TextBox textBox33;
        private Telerik.Reporting.TextBox textBox34;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox24;
    }
}