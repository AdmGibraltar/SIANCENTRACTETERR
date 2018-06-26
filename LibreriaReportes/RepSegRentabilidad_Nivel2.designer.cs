namespace LibreriaReportes
{
    partial class RepSegRentabilidad_Nivel2
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter4 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter5 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter6 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter7 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter8 = new Telerik.Reporting.ReportParameter();
            this.groupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox27A = new Telerik.Reporting.TextBox();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.TxtFiltros = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox12A = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox15X = new Telerik.Reporting.TextBox();
            this.textBox15A = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.sqlConnection2 = new System.Data.SqlClient.SqlConnection();
            this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.textBox33 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // groupFooterSection
            // 
            this.groupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.49999964237213135D);
            this.groupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox22,
            this.textBox23,
            this.textBox24,
            this.textBox25,
            this.textBox26,
            this.textBox27,
            this.textBox27A,
            this.textBox28,
            this.textBox30,
            this.textBox32,
            this.textBox33});
            this.groupFooterSection.Name = "groupFooterSection";
            this.groupFooterSection.Style.BackgroundColor = System.Drawing.Color.LightGray;
            // 
            // textBox22
            // 
            this.textBox22.Format = "{0:N2}";
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox22.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox22.Style.Font.Bold = false;
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox22.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox22.Value = "=(SUM(Fields.UtilidadBrutaObjetivo)/SUM(Fields.Venta)*100) - (SUM(Fields.Utilidad" +
    "Bruta)/SUM(Fields.Venta)*100)";
            // 
            // textBox23
            // 
            this.textBox23.Format = "{0:N2}";
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.5D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox23.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox23.Style.Font.Bold = false;
            this.textBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox23.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox23.Value = "=SUM(Fields.UtilidadBrutaObjetivo)/SUM(Fields.Venta)*100";
            // 
            // textBox24
            // 
            this.textBox24.Format = "{0:N2}";
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox24.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox24.Style.Font.Bold = false;
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox24.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox24.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox24.Value = "=SUM(Fields.UtilidadBruta)/SUM(Fields.Venta)*100";
            // 
            // textBox25
            // 
            this.textBox25.Format = "{0:N2}";
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.5000004768371582D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox25.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox25.Style.Font.Bold = false;
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox25.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox25.Value = "=SUM(Fields.UtilidadBruta)";
            // 
            // textBox26
            // 
            this.textBox26.Format = "{0:N2}";
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox26.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox26.Style.Font.Bold = false;
            this.textBox26.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox26.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox26.Value = "=SUM(Fields.Venta)";
            // 
            // textBox27
            // 
            this.textBox27.Format = "{0:N2}";
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(21.5D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox27.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox27.Style.Font.Bold = false;
            this.textBox27.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox27.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox27.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox27.Value = "=AVG(Fields.CumplimientoProyectada)";
            // 
            // textBox27A
            // 
            this.textBox27A.Format = "{0:N2}";
            this.textBox27A.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(26.5D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox27A.Name = "textBox27A";
            this.textBox27A.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox27A.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox27A.Style.Font.Bold = false;
            this.textBox27A.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox27A.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox27A.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox27A.Value = "=AVG(Fields.PorcUtilidadBrutaPP)";
            // 
            // textBox28
            // 
            this.textBox28.Action = null;
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.9999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox28.Style.Color = System.Drawing.Color.Black;
            this.textBox28.Style.Font.Bold = false;
            this.textBox28.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox28.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox28.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox28.Value = "= \"Total \" + Fields.Region ";
            // 
            // textBox30
            // 
            this.textBox30.Format = "{0:N2}";
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.5D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox30.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox30.Style.Font.Bold = false;
            this.textBox30.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox30.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox30.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox30.Value = "=SUM(Fields.UtilidadBrutaP)";
            // 
            // textBox32
            // 
            this.textBox32.Format = "{0:N2}";
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(19D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox32.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox32.Style.Font.Bold = false;
            this.textBox32.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox32.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox32.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox32.Value = "=SUM(Fields.NumeroProyectos)";
            // 
            // groupHeaderSection
            // 
            this.groupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.3999997079372406D);
            this.groupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox21});
            this.groupHeaderSection.Name = "groupHeaderSection";
            this.groupHeaderSection.Style.BackgroundColor = System.Drawing.Color.LightGray;
            // 
            // textBox21
            // 
            this.textBox21.Action = null;
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox21.Style.Color = System.Drawing.Color.Black;
            this.textBox21.Style.Font.Bold = true;
            this.textBox21.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox21.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox21.Value = "=Fields.Region";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(3.1000001430511475D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox4,
            this.textBox2,
            this.textBox3,
            this.textBox5,
            this.TxtFiltros,
            this.textBox6,
            this.textBox1,
            this.textBox7,
            this.textBox13,
            this.textBox16,
            this.textBox18,
            this.textBox19,
            this.textBox20,
            this.textBox12,
            this.textBox12A});
            this.pageHeader.Name = "pageHeader";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(-3.6557519678126482E-08D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(29D), Telerik.Reporting.Drawing.Unit.Cm(0.699999988079071D));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Value = "Key Química S.A. de C.V.";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(29D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.Value = "SIANWeb Central";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(1.3000000715255737D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(29D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Value = "Gestión de rentabilidad";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(2.6999998092651367D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.0000004768371582D), Telerik.Reporting.Drawing.Unit.Cm(0.40000024437904358D));
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
            this.TxtFiltros.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(29D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.TxtFiltros.Style.Font.Bold = true;
            this.TxtFiltros.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.TxtFiltros.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.TxtFiltros.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.TxtFiltros.Value = "";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4D), Telerik.Reporting.Drawing.Unit.Cm(2.7000000476837158D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999995231628418D), Telerik.Reporting.Drawing.Unit.Cm(0.40000012516975403D));
            this.textBox6.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "Importe";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.5000004768371582D), Telerik.Reporting.Drawing.Unit.Cm(2.7000000476837158D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999995231628418D), Telerik.Reporting.Drawing.Unit.Cm(0.40000012516975403D));
            this.textBox1.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox1.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "UP$";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.9999990463256836D), Telerik.Reporting.Drawing.Unit.Cm(2.7000000476837158D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999995231628418D), Telerik.Reporting.Drawing.Unit.Cm(0.40000012516975403D));
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Value = "UP%";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.5D), Telerik.Reporting.Drawing.Unit.Cm(2.6999998092651367D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999995231628418D), Telerik.Reporting.Drawing.Unit.Cm(0.40000012516975403D));
            this.textBox13.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox13.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Value = "UP Objetivo%";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(2.6999998092651367D));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999995231628418D), Telerik.Reporting.Drawing.Unit.Cm(0.40000012516975403D));
            this.textBox16.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox16.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox16.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox16.Value = "GAP";
            // 
            // textBox18
            // 
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(19D), Telerik.Reporting.Drawing.Unit.Cm(2.6999998092651367D));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999995231628418D), Telerik.Reporting.Drawing.Unit.Cm(0.40000012516975403D));
            this.textBox18.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox18.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox18.Value = "Proyectos #";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.5D), Telerik.Reporting.Drawing.Unit.Cm(2.6999998092651367D));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999995231628418D), Telerik.Reporting.Drawing.Unit.Cm(0.40000012516975403D));
            this.textBox19.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox19.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox19.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox19.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox19.Value = "Proyectos $";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(21.5D), Telerik.Reporting.Drawing.Unit.Cm(2.6999998092651367D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999995231628418D), Telerik.Reporting.Drawing.Unit.Cm(0.40000012516975403D));
            this.textBox20.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox20.Value = "% Cump proy";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(24D), Telerik.Reporting.Drawing.Unit.Cm(2.6999998092651367D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999995231628418D), Telerik.Reporting.Drawing.Unit.Cm(0.40000012516975403D));
            this.textBox12.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox12.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Value = "UP Proy $";
            // 
            // textBox12A
            // 
            this.textBox12A.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(26.5D), Telerik.Reporting.Drawing.Unit.Cm(2.6999998092651367D));
            this.textBox12A.Name = "textBox12A";
            this.textBox12A.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999995231628418D), Telerik.Reporting.Drawing.Unit.Cm(0.40000012516975403D));
            this.textBox12A.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox12A.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox12A.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox12A.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox12A.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12A.Value = "UP Proy %";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.40000021457672119D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox8,
            this.textBox9,
            this.textBox10,
            this.textBox11,
            this.textBox14,
            this.textBox17,
            this.textBox15,
            this.textBox15X,
            this.textBox15A,
            this.textBox29,
            this.textBox31});
            this.detail.Name = "detail";
            this.detail.Style.Font.Bold = false;
            // 
            // textBox8
            // 
            this.textBox8.Action = null;
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.0000004768371582D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox8.Style.Color = System.Drawing.Color.Black;
            this.textBox8.Style.Font.Bold = false;
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "=Fields.CDI";
            // 
            // textBox9
            // 
            this.textBox9.Format = "{0:N2}";
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox9.Style.Font.Bold = false;
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.Value = "=Fields.Venta";
            // 
            // textBox10
            // 
            this.textBox10.Format = "{0:N2}";
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.5000004768371582D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox10.Style.Font.Bold = false;
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.Value = "=Fields.UtilidadBruta";
            // 
            // textBox11
            // 
            this.textBox11.Format = "{0:N2}";
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.9999990463256836D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox11.Style.Font.Bold = false;
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.Value = "=Fields.PorcUtilidadBruta";
            // 
            // textBox14
            // 
            this.textBox14.Format = "{0:N2}";
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.5D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox14.Style.Font.Bold = false;
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.Value = "=Fields.PorcUtilidadBrutaObjetivo";
            // 
            // textBox17
            // 
            this.textBox17.Format = "{0:N2}";
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox17.Style.Font.Bold = false;
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox17.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox17.Value = "=Fields.GAP";
            // 
            // textBox15
            // 
            this.textBox15.Format = "{0:N2}";
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(24D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox15.Style.Font.Bold = false;
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox15.Value = "=Fields.UtilidadBrutaPP";
            // 
            // textBox15X
            // 
            this.textBox15X.Format = "{0:N2}";
            this.textBox15X.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(21.5D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox15X.Name = "textBox15X";
            this.textBox15X.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox15X.Style.Font.Bold = false;
            this.textBox15X.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox15X.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox15X.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox15X.Value = "=Fields.CumplimientoProyectada";
            // 
            // textBox15A
            // 
            this.textBox15A.Format = "{0:N2}";
            this.textBox15A.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(26.5D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox15A.Name = "textBox15A";
            this.textBox15A.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox15A.Style.Font.Bold = false;
            this.textBox15A.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox15A.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox15A.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox15A.Value = "=Fields.PorcUtilidadBrutaPP";
            // 
            // textBox29
            // 
            this.textBox29.Format = "{0:N2}";
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.5D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox29.Style.Font.Bold = false;
            this.textBox29.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox29.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox29.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox29.Value = "=Fields.UtilidadBrutaP";
            // 
            // textBox31
            // 
            this.textBox31.Format = "{0:N2}";
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(19D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox31.Style.Font.Bold = false;
            this.textBox31.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox31.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox31.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox31.Value = "=Fields.NumeroProyectos";
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
            this.sqlSelectCommand1.CommandText = "SP_Gestion_Rentabilidad";
            this.sqlSelectCommand1.CommandTimeout = 600;
            this.sqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlSelectCommand1.Connection = this.sqlConnection1;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@TipoCd", System.Data.SqlDbType.Int),
            new System.Data.SqlClient.SqlParameter("@MesInicial", System.Data.SqlDbType.Int),
            new System.Data.SqlClient.SqlParameter("@AnioInicial", System.Data.SqlDbType.Int),
            new System.Data.SqlClient.SqlParameter("@MesFinal", System.Data.SqlDbType.Int),
            new System.Data.SqlClient.SqlParameter("@AnioFinal", System.Data.SqlDbType.Int),
            new System.Data.SqlClient.SqlParameter("@Nivel", System.Data.SqlDbType.Int)});
            // 
            // pageFooter
            // 
            this.pageFooter.Bindings.Add(new Telerik.Reporting.Binding("Visible", "=PageCount = PageNumber"));
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.49999985098838806D);
            this.pageFooter.Name = "pageFooter";
            this.pageFooter.PrintOnFirstPage = true;
            // 
            // textBox33
            // 
            this.textBox33.Format = "{0:N2}";
            this.textBox33.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(24D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.4999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox33.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox33.Style.Font.Bold = false;
            this.textBox33.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox33.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox33.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox33.Value = "=SUM(Fields.UtilidadBrutaPP)";
            // 
            // RepSegRentabilidad_Nivel2
            // 
            this.DataSource = this.sqlDataAdapter1;
            group1.GroupFooter = this.groupFooterSection;
            group1.GroupHeader = this.groupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("Fields.Id_Region"));
            group1.Name = "group";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection,
            this.groupFooterSection,
            this.pageHeader,
            this.detail,
            this.pageFooter});
            this.Name = "RepCifrasControl";
            this.PageSettings.Landscape = true;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            reportParameter1.Name = "Conexion";
            reportParameter2.Name = "TipoCd";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter3.Name = "MesInicial";
            reportParameter3.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter4.Name = "AnioInicial";
            reportParameter4.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter5.Name = "MesFinal";
            reportParameter5.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter6.Name = "AnioFinal";
            reportParameter6.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter7.Name = "Nivel";
            reportParameter7.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter8.Name = "Filtro";
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            this.ReportParameters.Add(reportParameter5);
            this.ReportParameters.Add(reportParameter6);
            this.ReportParameters.Add(reportParameter7);
            this.ReportParameters.Add(reportParameter8);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.Style.Font.Bold = true;
            this.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.Style.Visible = true;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(29D);
            this.NeedDataSource += new System.EventHandler(this.RepSegRentabilidad_Nivel2_NeedDataSource);
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
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox TxtFiltros;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox12A;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox15X;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox15A;
//        private Telerik.Reporting.TextBox textBox15B;
        private Telerik.Reporting.GroupHeaderSection groupHeaderSection;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.GroupFooterSection groupFooterSection;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox27A;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox textBox33;
    }
}