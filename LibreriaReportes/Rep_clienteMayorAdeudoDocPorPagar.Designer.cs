namespace LibreriaReportes
{
    partial class Rep_clienteMayorAdeudoDocPorPagar
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
            this.rep_ClienteMayorAdeudoDocDet2 = new LibreriaReportes.Rep_ClienteMayorAdeudoDocDet();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.txtzona = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.txtSaldo = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.sqlCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.rep_ClienteMayorAdeudoDocDet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // rep_ClienteMayorAdeudoDocDet2
            // 
            this.rep_ClienteMayorAdeudoDocDet2.Name = "Rep_ClienteMayorAdeudoDocDet";
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(2.5999999046325684D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox2,
            this.textBox1,
            this.textBox15,
            this.textBox22,
            this.textBox21,
            this.textBox20,
            this.textBox18,
            this.textBox17,
            this.textBox16,
            this.textBox13,
            this.txtzona,
            this.textBox31,
            this.textBox30});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.694883346557617D), Telerik.Reporting.Drawing.Unit.Cm(1.2999999523162842D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.69990348815918D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(12D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "=Fields.Cte_Nombre";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.699999809265137D), Telerik.Reporting.Drawing.Unit.Cm(0.89999997615814209D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(11.099899291992188D), Telerik.Reporting.Drawing.Unit.Cm(0.4000001847743988D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(12D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "Documentos por pagar del cliente";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.592707633972168D), Telerik.Reporting.Drawing.Unit.Cm(2.0999002456665039D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.3959338665008545D), Telerik.Reporting.Drawing.Unit.Cm(0.39999997615814209D));
            this.textBox15.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox15.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox15.Style.Font.Bold = true;
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox15.Value = "Cliente";
            // 
            // textBox22
            // 
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(27.875417709350586D), Telerik.Reporting.Drawing.Unit.Cm(2.0999002456665039D));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.9999992847442627D), Telerik.Reporting.Drawing.Unit.Cm(0.39999997615814209D));
            this.textBox22.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox22.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox22.Style.Font.Bold = true;
            this.textBox22.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox22.Value = "Saldo";
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(24.991456985473633D), Telerik.Reporting.Drawing.Unit.Cm(2.0999002456665039D));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.900397777557373D), Telerik.Reporting.Drawing.Unit.Cm(0.39999997615814209D));
            this.textBox21.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox21.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox21.Style.Font.Bold = true;
            this.textBox21.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox21.Value = "Credito";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(21.975208282470703D), Telerik.Reporting.Drawing.Unit.Cm(2.0999002456665039D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.9913742542266846D), Telerik.Reporting.Drawing.Unit.Cm(0.39999997615814209D));
            this.textBox20.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox20.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox20.Style.Font.Bold = true;
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox20.Value = "Cargo";
            // 
            // textBox18
            // 
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(18.985416412353516D), Telerik.Reporting.Drawing.Unit.Cm(2.0999002456665039D));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.9999945163726807D), Telerik.Reporting.Drawing.Unit.Cm(0.39999997615814209D));
            this.textBox18.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox18.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox18.Style.Font.Bold = true;
            this.textBox18.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox18.Value = "Fecha Venc.";
            // 
            // textBox17
            // 
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.995622634887695D), Telerik.Reporting.Drawing.Unit.Cm(2.0999002456665039D));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.9982244968414307D), Telerik.Reporting.Drawing.Unit.Cm(0.39999997615814209D));
            this.textBox17.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox17.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox17.Style.Font.Bold = true;
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox17.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox17.Value = "Fecha";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.979374885559082D), Telerik.Reporting.Drawing.Unit.Cm(2.0999002456665039D));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.9961347579956055D), Telerik.Reporting.Drawing.Unit.Cm(0.39999997615814209D));
            this.textBox16.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox16.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox16.Style.Font.Bold = true;
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox16.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox16.Value = "Documento";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.8933333158493042D), Telerik.Reporting.Drawing.Unit.Cm(2.0999002456665039D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.6995997428894043D), Telerik.Reporting.Drawing.Unit.Cm(0.39999979734420776D));
            this.textBox13.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox13.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox13.Style.Font.Bold = true;
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Value = "Nombre Cdi";
            // 
            // txtzona
            // 
            this.txtzona.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.20000000298023224D), Telerik.Reporting.Drawing.Unit.Cm(2.0999002456665039D));
            this.txtzona.Name = "txtzona";
            this.txtzona.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.39999979734420776D));
            this.txtzona.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Groove;
            this.txtzona.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Groove;
            this.txtzona.Style.Font.Bold = true;
            this.txtzona.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtzona.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtzona.Value = "Zona";
            // 
            // textBox31
            // 
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.6999993324279785D), Telerik.Reporting.Drawing.Unit.Cm(0.099999949336051941D));
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(20.5D), Telerik.Reporting.Drawing.Unit.Cm(0.40000009536743164D));
            this.textBox31.Style.Font.Bold = true;
            this.textBox31.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(12D);
            this.textBox31.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox31.Value = "Key Química S.A. de C.V.";
            // 
            // textBox30
            // 
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.6999993324279785D), Telerik.Reporting.Drawing.Unit.Cm(0.49999994039535522D));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(20.5D), Telerik.Reporting.Drawing.Unit.Cm(0.39999991655349731D));
            this.textBox30.Style.Font.Bold = true;
            this.textBox30.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(12D);
            this.textBox30.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox30.Value = "SIANWeb Central";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.50010108947753906D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox10,
            this.textBox14,
            this.txtSaldo,
            this.textBox9,
            this.textBox8,
            this.textBox7,
            this.textBox5,
            this.textBox4,
            this.textBox3});
            this.detail.Name = "detail";
            // 
            // textBox10
            // 
            this.textBox10.Format = "{0:N2}";
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(24.885618209838867D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.0000009536743164D), Telerik.Reporting.Drawing.Unit.Cm(0.499900221824646D));
            this.textBox10.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox10.Style.Font.Bold = false;
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.Value = "=Fields.Pagado";
            // 
            // textBox14
            // 
            this.textBox14.Format = "{0:N2}";
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(21.975202560424805D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.4999004602432251D));
            this.textBox14.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox14.Style.Font.Bold = false;
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.Value = "=Fields.Total";
            // 
            // txtSaldo
            // 
            this.txtSaldo.Format = "{0:N2}";
            this.txtSaldo.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(27.981243133544922D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.txtSaldo.Name = "txtSaldo";
            this.txtSaldo.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.8941707611083984D), Telerik.Reporting.Drawing.Unit.Cm(0.49980032444000244D));
            this.txtSaldo.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.txtSaldo.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtSaldo.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtSaldo.Value = "=Fields.Saldo";
            // 
            // textBox9
            // 
            this.textBox9.Format = "{0:d}";
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(18.985410690307617D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.0000009536743164D), Telerik.Reporting.Drawing.Unit.Cm(0.49980011582374573D));
            this.textBox9.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox9.Style.Font.Bold = false;
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.Value = "=Fields.Ven_Fecha\r\n";
            // 
            // textBox8
            // 
            this.textBox8.Format = "{0:d}";
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.99561882019043D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.0000009536743164D), Telerik.Reporting.Drawing.Unit.Cm(0.49979948997497559D));
            this.textBox8.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.Font.Bold = false;
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "=Fields.doc_Fecha";
            // 
            // textBox7
            // 
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Conexion", "=Parameters.Conexion.Value"));
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Id_Cd", "=Parameters.cdi.Value"));
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Id_DocSerie", "=Fields.Id_DocSerie"));
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Fecha_Corte", "=Parameters.Fecha_Corte.Value"));
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Serie", "=Fields.Id_DocSerie"));
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("FechaCorte", "=Parameters.Fecha_Corte.Value"));
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Factura", "=Fields.Id_DocUnique"));
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Fecha", "=Fields.doc_Fecha"));
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Fecha_Ven", "=Fields.Ven_fecha"));
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Cargo", "=Fields.Total"));
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Credito", "=Fields.Pagado"));
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Saldo", "=Fields.Saldo"));
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("Fecha_Cierre", "=Parameters.Fecha_Cierre.Value"));
            instanceReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("FechaCierre", "=PArameters.Fecha_Cierre.Value"));
            instanceReportSource1.ReportDocument = this.rep_ClienteMayorAdeudoDocDet2;
            navigateToReportAction1.ReportSource = instanceReportSource1;
            this.textBox7.Action = navigateToReportAction1;
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.979369163513184D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.995232105255127D), Telerik.Reporting.Drawing.Unit.Cm(0.49990004301071167D));
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox7.Style.Color = System.Drawing.Color.Blue;
            this.textBox7.Style.Font.Underline = true;
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Value = "=Fields.Id_DocUnique";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.59270191192627D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.3959367275238037D), Telerik.Reporting.Drawing.Unit.Cm(0.49980011582374573D));
            this.textBox5.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox5.Style.Font.Bold = false;
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.Value = "=Fields.Id_Cte";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.8933274745941162D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.6995997428894043D), Telerik.Reporting.Drawing.Unit.Cm(0.499799907207489D));
            this.textBox4.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox4.Style.Font.Bold = false;
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "=Fields.Cdi";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.19999425113201141D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.6998999118804932D), Telerik.Reporting.Drawing.Unit.Cm(0.49980032444000244D));
            this.textBox3.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox3.Style.Font.Bold = false;
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "=Fields.Id_Cd";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.59999960660934448D);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox23,
            this.textBox11,
            this.textBox6});
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // textBox23
            // 
            this.textBox23.Format = "{0:N2}";
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(27.981243133544922D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.8941707611083984D), Telerik.Reporting.Drawing.Unit.Cm(0.60010021924972534D));
            this.textBox23.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox23.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox23.Style.Font.Bold = true;
            this.textBox23.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox23.Value = "=SUM(Fields.Saldo)";
            // 
            // textBox11
            // 
            this.textBox11.Format = "{0:N2}";
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(22.093957901000977D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.881242036819458D), Telerik.Reporting.Drawing.Unit.Cm(0.60010021924972534D));
            this.textBox11.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox11.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox11.Style.Font.Bold = true;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.Value = "= Sum(Fields.Total)";
            // 
            // textBox6
            // 
            this.textBox6.Format = "{0:N2}";
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(25.110208511352539D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7816486358642578D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox6.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox6.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "= Sum(Fields.Pagado)";
            // 
            // sqlConnection1
            // 
            this.sqlConnection1.ConnectionString = "Data Source=10.1.0.120;Initial Catalog=SIANCENTRAL;Persist Security Info=True;Use" +
    "r ID=sa;Password=sistemas";
            this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlCommand1
            // 
            this.sqlCommand1.CommandText = "Nivel5_SaldosClientePorPagar";
            this.sqlCommand1.CommandTimeout = 600;
            this.sqlCommand1.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlCommand1.Connection = this.sqlConnection1;
            this.sqlCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@FechaCorte", System.Data.SqlDbType.DateTime, 8),
            new System.Data.SqlClient.SqlParameter("@Dias", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@DiasRevision", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@Plazo", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@Vencimiento", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@TipoCte", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@Cd", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@Cte", System.Data.SqlDbType.Float, 8),
            new System.Data.SqlClient.SqlParameter("@Legal", System.Data.SqlDbType.Int, 4)});
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "Nivel5_SaldosClientePorPagar";
            this.sqlSelectCommand1.CommandTimeout = 600;
            this.sqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlSelectCommand1.Connection = this.sqlConnection1;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@FechaCorte", System.Data.SqlDbType.DateTime, 8),
            new System.Data.SqlClient.SqlParameter("@Dias", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@DiasRevision", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@Plazo", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@Vencimiento", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@TipoCte", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@Cd", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@Cte", System.Data.SqlDbType.Float, 8),
            new System.Data.SqlClient.SqlParameter("@Legal", System.Data.SqlDbType.Int, 4)});
            // 
            // sqlDataAdapter1
            // 
            this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
            // 
            // Rep_clienteMayorAdeudoDocPorPagar
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1});
            this.Name = "Rep_clienteMayorAdeudoDocPorPagar";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.Name = "Conexion";
            reportParameter2.Name = "Fecha_Corte";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.DateTime;
            reportParameter3.Name = "Fecha_Cierre";
            reportParameter3.Type = Telerik.Reporting.ReportParameterType.DateTime;
            reportParameter4.Name = "Dias";
            reportParameter4.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter5.Name = "Dias_Revision";
            reportParameter5.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter6.Name = "Vencidos";
            reportParameter6.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter7.Name = "cdi";
            reportParameter7.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter8.Name = "Id_Cte";
            reportParameter8.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter9.Name = "Legal";
            reportParameter9.Type = Telerik.Reporting.ReportParameterType.Integer;
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
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(30.87541389465332D);
            this.NeedDataSource += new System.EventHandler(this.Rep_clienteMayorAdeudoDocPorPagar_NeedDataSource);
            ((System.ComponentModel.ISupportInitialize)(this.rep_ClienteMayorAdeudoDocDet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox txtzona;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox txtSaldo;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox6;
        private System.Data.SqlClient.SqlConnection sqlConnection1;
        private System.Data.SqlClient.SqlCommand sqlCommand1;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
        private Rep_ClienteMayorAdeudoDocDet rep_ClienteMayorAdeudoDocDet2;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox30;
    }
}