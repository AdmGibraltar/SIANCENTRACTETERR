﻿namespace LibreriaReportes
{
    partial class Rep_FacRegistroPedidos
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
            Telerik.Reporting.ReportParameter reportParameter10 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter11 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter12 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter13 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter14 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter15 = new Telerik.Reporting.ReportParameter();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox43 = new Telerik.Reporting.TextBox();
            this.textBox40 = new Telerik.Reporting.TextBox();
            this.textBox39 = new Telerik.Reporting.TextBox();
            this.textBox38 = new Telerik.Reporting.TextBox();
            this.textBox37 = new Telerik.Reporting.TextBox();
            this.textBox34 = new Telerik.Reporting.TextBox();
            this.textBox33 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.sqlConnection2 = new System.Data.SqlClient.SqlConnection();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = new Telerik.Reporting.Drawing.Unit(4.599799633026123D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox43,
            this.textBox40,
            this.textBox39,
            this.textBox38,
            this.textBox37,
            this.textBox34,
            this.textBox33,
            this.textBox32,
            this.textBox31,
            this.textBox30,
            this.textBox29,
            this.textBox28,
            this.textBox27,
            this.textBox26,
            this.textBox15,
            this.textBox16,
            this.textBox17,
            this.textBox18,
            this.textBox19,
            this.textBox20,
            this.textBox21,
            this.textBox22});
            this.pageHeader.Name = "pageHeader";
            // 
            // textBox43
            // 
            this.textBox43.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(21.378332138061523D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox43.Name = "textBox43";
            this.textBox43.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.5999984741210938D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000008344650269D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox43.Style.Font.Name = "Verdana";
            this.textBox43.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(7D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox43.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox43.Value = "= \"Página \" + PageNumber + \" de \" + PageCount";
            // 
            // textBox40
            // 
            this.textBox40.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.89958328008651733D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.1000001430511475D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox40.Name = "textBox40";
            this.textBox40.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.8004169464111328D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000008344650269D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox40.Style.Font.Bold = true;
            this.textBox40.Style.Font.Name = "Verdana";
            this.textBox40.Value = "Estatus: ";
            // 
            // textBox39
            // 
            this.textBox39.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.89958328008651733D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.5D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox39.Name = "textBox39";
            this.textBox39.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.8004169464111328D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.59999990463256836D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox39.Style.Font.Bold = true;
            this.textBox39.Style.Font.Name = "Verdana";
            this.textBox39.Value = "Pedidos a:";
            // 
            // textBox38
            // 
            this.textBox38.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.89958328008651733D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.89999997615814209D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox38.Name = "textBox38";
            this.textBox38.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.8004169464111328D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000008344650269D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox38.Style.Font.Bold = true;
            this.textBox38.Style.Font.Name = "Verdana";
            this.textBox38.Value = "Fecha final:";
            // 
            // textBox37
            // 
            this.textBox37.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.89999997615814209D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.30000001192092896D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.8004169464111328D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.59999990463256836D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox37.Style.Font.Bold = true;
            this.textBox37.Style.Font.Name = "Verdana";
            this.textBox37.Value = "Fecha inicial:";
            // 
            // textBox34
            // 
            this.textBox34.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.9200000762939453D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.30000001192092896D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.0533990859985352D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000002384185791D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox34.Style.Font.Bold = true;
            this.textBox34.Style.Font.Name = "Verdana";
            this.textBox34.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(12D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox34.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox34.Value = "= Parameters.nombreEmpresa";
            // 
            // textBox33
            // 
            this.textBox33.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.9200000762939453D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.89999997615814209D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.0533990859985352D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000008344650269D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox33.Style.Font.Bold = true;
            this.textBox33.Style.Font.Name = "Verdana";
            this.textBox33.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(12D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox33.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox33.Value = "= Parameters.nombreSucursal";
            // 
            // textBox32
            // 
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.9200000762939453D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.5D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.0480575561523438D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.59999990463256836D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox32.Style.Font.Bold = true;
            this.textBox32.Style.Font.Name = "Verdana";
            this.textBox32.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(12D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox32.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox32.Value = "Registro de pedidos";
            // 
            // textBox31
            // 
            this.textBox31.CanGrow = false;
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(17.600000381469727D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.89999997615814209D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(9.09000015258789D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000002384185791D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox31.Style.Font.Name = "Verdana";
            this.textBox31.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox31.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox31.Value = "= \"Usuario: \" + Parameters.usuario";
            // 
            // textBox30
            // 
            this.textBox30.CanGrow = false;
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(17.600000381469727D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.5D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(9.09000015258789D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000002384185791D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox30.Style.Font.Name = "Verdana";
            this.textBox30.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox30.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox30.Value = "= Parameters.fecha";
            // 
            // textBox29
            // 
            this.textBox29.Format = "{0:d}";
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.8000001907348633D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.30000004172325134D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.9000000953674316D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.59999990463256836D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox29.Style.Font.Bold = true;
            this.textBox29.Style.Font.Name = "Verdana";
            this.textBox29.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox29.Value = "= Parameters.[fecha_inicial]";
            // 
            // textBox28
            // 
            this.textBox28.Format = "{0:d}";
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.8000001907348633D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.89999997615814209D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.9000000953674316D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000008344650269D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox28.Style.Font.Bold = true;
            this.textBox28.Style.Font.Name = "Verdana";
            this.textBox28.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox28.Value = "= Parameters.[fecha_final]";
            // 
            // textBox27
            // 
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.8000001907348633D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.5D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.9000000953674316D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.599999725818634D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox27.Style.Font.Bold = true;
            this.textBox27.Style.Font.Name = "Verdana";
            this.textBox27.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox27.Value = "= Parameters.[pedidos_a]";
            // 
            // textBox26
            // 
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.8000001907348633D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.1000001430511475D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.9000000953674316D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000008344650269D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox26.Style.Font.Bold = true;
            this.textBox26.Style.Font.Name = "Verdana";
            this.textBox26.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox26.Value = "= Parameters.estatus";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(6.5000004768371582D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.4492499828338623D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(8.40000057220459D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.59999990463256836D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox15.Style.Font.Bold = true;
            this.textBox15.Style.Font.Name = "Verdana";
            this.textBox15.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox15.Value = "Nombre cliente";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(15.19999885559082D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.4492499828338623D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.5000002384185791D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.59999990463256836D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox16.Style.Font.Bold = true;
            this.textBox16.Style.Font.Name = "Verdana";
            this.textBox16.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox16.Value = "Estatus";
            // 
            // textBox17
            // 
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(18.999998092651367D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.4492495059967041D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.3999979496002197D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.59999990463256836D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox17.Style.Font.Bold = true;
            this.textBox17.Style.Font.Name = "Verdana";
            this.textBox17.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox17.Value = "Subtotal";
            // 
            // textBox18
            // 
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.2000002861022949D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.4492499828338623D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.8999996185302734D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.59999990463256836D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox18.Style.Font.Bold = true;
            this.textBox18.Style.Font.Name = "Verdana";
            this.textBox18.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox18.Value = "Fecha";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.40000057220459D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.4492499828338623D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.8000001907348633D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.860416829586029D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox19.Style.Font.Bold = true;
            this.textBox19.Style.Font.Name = "Verdana";
            this.textBox19.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox19.Value = "Código cliente";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.00010001297778217122D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.4492499828338623D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.8999000787734985D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.59999990463256836D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox20.Style.Font.Bold = true;
            this.textBox20.Style.Font.Name = "Verdana";
            this.textBox20.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox20.Value = "Pedido";
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(21.700002670288086D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.4492499828338623D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.3999979496002197D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.59999990463256836D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox21.Style.Font.Bold = true;
            this.textBox21.Style.Font.Name = "Verdana";
            this.textBox21.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox21.Value = "I.V.A.";
            // 
            // textBox22
            // 
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(24.400003433227539D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.4395828247070312D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.3999979496002197D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.59999990463256836D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox22.Style.Font.Bold = true;
            this.textBox22.Style.Font.Name = "Verdana";
            this.textBox22.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox22.Value = "Total\r\n";
            // 
            // detail
            // 
            this.detail.Height = new Telerik.Reporting.Drawing.Unit(0.40010008215904236D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox2,
            this.textBox3,
            this.textBox5,
            this.textBox7,
            this.textBox8,
            this.textBox6,
            this.textBox4});
            this.detail.Name = "detail";
            // 
            // textBox1
            // 
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.8999999761581421D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox1.Style.Font.Name = "Verdana";
            this.textBox1.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox1.Value = "=Fields.[Id_Ped]";
            // 
            // textBox2
            // 
            this.textBox2.Format = "{0:d}";
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.2000000476837158D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.00010012308484874666D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.8999999761581421D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox2.Style.Font.Name = "Verdana";
            this.textBox2.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox2.Value = "=Fields.[Ped_Fecha]";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.4000000953674316D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.00010012308484874666D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.7999999523162842D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox3.Style.Font.Name = "Verdana";
            this.textBox3.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox3.Value = "=Fields.[Id_Cte]";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(15.19999885559082D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.00010012308484874666D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.5D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox5.Style.Font.Name = "Verdana";
            this.textBox5.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox5.Value = "=Fields.[Ped_Estatus]";
            // 
            // textBox7
            // 
            this.textBox7.Format = "{0:C2}";
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(21.69999885559082D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.00010012308484874666D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.4000000953674316D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox7.Style.Font.Name = "Verdana";
            this.textBox7.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox7.Value = "=Fields.[Ped_Iva]";
            // 
            // textBox8
            // 
            this.textBox8.Format = "{0:C2}";
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(24.400001525878906D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.00010012308484874666D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.4000000953674316D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox8.Style.Font.Name = "Verdana";
            this.textBox8.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox8.Value = "=Fields.[Ped_Total]";
            // 
            // textBox6
            // 
            this.textBox6.Format = "{0:C2}";
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(18.999998092651367D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.4000000953674316D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox6.Style.Font.Name = "Verdana";
            this.textBox6.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox6.Value = "=Fields.[Ped_Subtotal]";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(6.5000004768371582D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(8.3999996185302734D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox4.Style.Font.Name = "Verdana";
            this.textBox4.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox4.Value = "=Fields.[Cte_NomComercial]";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = new Telerik.Reporting.Drawing.Unit(0.2999001145362854D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.pageFooter.Name = "pageFooter";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(14.69999885559082D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.59989988803863525D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.0000002384185791D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000008344650269D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox9.Style.Font.Name = "Verdana";
            this.textBox9.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox9.Value = "Total:";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(14.69999885559082D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.4999001026153565D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.8000001907348633D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.599999725818634D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox10.Style.Font.Name = "Verdana";
            this.textBox10.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox10.Value = "Total de pedidos:";
            // 
            // textBox11
            // 
            this.textBox11.Format = "{0:C2}";
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(18.999998092651367D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.59989988803863525D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.3999989032745361D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000008344650269D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox11.Style.Font.Name = "Verdana";
            this.textBox11.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox11.Value = "= Sum(Fields.[Ped_Subtotal])";
            // 
            // textBox12
            // 
            this.textBox12.Format = "{0:C2}";
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(21.69999885559082D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.59989988803863525D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.4000000953674316D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000002384185791D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox12.Style.Font.Name = "Verdana";
            this.textBox12.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox12.Value = "= Sum(Fields.[Ped_Iva])";
            // 
            // textBox13
            // 
            this.textBox13.Format = "{0:C2}";
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(24.400003433227539D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.59989988803863525D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.3999972343444824D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000008344650269D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox13.Style.Font.Name = "Verdana";
            this.textBox13.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox13.Value = "= Sum(Fields.[Ped_Total])";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(18.999998092651367D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.4999001026153565D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.4000003337860107D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.599999725818634D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox14.Style.Font.Name = "Verdana";
            this.textBox14.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox14.Value = "= Count(Fields.[Id_Ped])";
            // 
            // sqlConnection1
            // 
            this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlConnection2
            // 
            this.sqlConnection2.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "dbo.spRep_FacRegistroPedidos";
            this.sqlSelectCommand1.CommandTimeout = 600;
            this.sqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlSelectCommand1.Connection = this.sqlConnection1;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, 0),
            new System.Data.SqlClient.SqlParameter("@Id_Emp", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, ""),
            new System.Data.SqlClient.SqlParameter("@Id_Cd", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, ""),
            new System.Data.SqlClient.SqlParameter("@Fecha_inicial", System.Data.SqlDbType.DateTime, 8),
            new System.Data.SqlClient.SqlParameter("@Fecha_final", System.Data.SqlDbType.DateTime, 8),
            new System.Data.SqlClient.SqlParameter("@Pedidos_a", System.Data.SqlDbType.NVarChar, 4),
            new System.Data.SqlClient.SqlParameter("@Estatus", System.Data.SqlDbType.NVarChar, 1)});
            // 
            // sqlDataAdapter1
            // 
            this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
            this.sqlDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "spRep_FacRegistroPedidos", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Id_Ped", "Id_Ped"),
                        new System.Data.Common.DataColumnMapping("Ped_Fecha", "Ped_Fecha"),
                        new System.Data.Common.DataColumnMapping("Id_Cte", "Id_Cte"),
                        new System.Data.Common.DataColumnMapping("Cte_NomComercial", "Cte_NomComercial"),
                        new System.Data.Common.DataColumnMapping("Ped_Estatus", "Ped_Estatus"),
                        new System.Data.Common.DataColumnMapping("Ped_Subtotal", "Ped_Subtotal"),
                        new System.Data.Common.DataColumnMapping("Ped_Iva", "Ped_Iva"),
                        new System.Data.Common.DataColumnMapping("Ped_Total", "Ped_Total")})});
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = new Telerik.Reporting.Drawing.Unit(2.3999001979827881D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox9,
            this.textBox10,
            this.textBox11,
            this.textBox12,
            this.textBox13,
            this.textBox14});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // Rep_FacRegistroPedidos
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail,
            this.pageFooter,
            this.reportFooterSection1});
            this.PageSettings.Landscape = true;
            this.PageSettings.Margins.Bottom = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Left = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Right = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Top = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            reportParameter1.Name = "fecha_inicial";
            reportParameter2.Name = "fecha_final";
            reportParameter3.Name = "pedidos_a";
            reportParameter4.Name = "estatus";
            reportParameter5.Name = "nombreEmpresa";
            reportParameter6.Name = "nombreSucursal";
            reportParameter7.Name = "usuario";
            reportParameter8.Name = "fecha";
            reportParameter9.Name = "@Id_Emp";
            reportParameter10.Name = "@Id_Cd_Ver";
            reportParameter11.Name = "@Fecha_inicial";
            reportParameter12.Name = "@Fecha_final";
            reportParameter13.Name = "@Pedidos_a";
            reportParameter14.Name = "@Estatus";
            reportParameter15.Name = "@Conexion";
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            this.ReportParameters.Add(reportParameter5);
            this.ReportParameters.Add(reportParameter6);
            this.ReportParameters.Add(reportParameter7);
            this.ReportParameters.Add(reportParameter8);
            this.ReportParameters.Add(reportParameter9);
            this.ReportParameters.Add(reportParameter10);
            this.ReportParameters.Add(reportParameter11);
            this.ReportParameters.Add(reportParameter12);
            this.ReportParameters.Add(reportParameter13);
            this.ReportParameters.Add(reportParameter14);
            this.ReportParameters.Add(reportParameter15);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = new Telerik.Reporting.Drawing.Unit(26.899999618530273D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.NeedDataSource += new System.EventHandler(this.Rep_FacRegistroPedidos_NeedDataSource);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox textBox43;
        private Telerik.Reporting.TextBox textBox40;
        private Telerik.Reporting.TextBox textBox39;
        private Telerik.Reporting.TextBox textBox38;
        private Telerik.Reporting.TextBox textBox37;
        private Telerik.Reporting.TextBox textBox34;
        private Telerik.Reporting.TextBox textBox33;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox26;
        private System.Data.SqlClient.SqlConnection sqlConnection1;
        private System.Data.SqlClient.SqlConnection sqlConnection2;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox22;
    }
}