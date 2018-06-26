namespace LibreriaReportes
{
    partial class pruebafinal
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.GraphGroup graphGroup1 = new Telerik.Reporting.GraphGroup();
            Telerik.Reporting.GraphGroup graphGroup2 = new Telerik.Reporting.GraphGroup();
            Telerik.Reporting.GraphGroup graphGroup4 = new Telerik.Reporting.GraphGroup();
            Telerik.Reporting.GraphTitle graphTitle1 = new Telerik.Reporting.GraphTitle();
            Telerik.Reporting.NumericalScale numericalScale1 = new Telerik.Reporting.NumericalScale();
            Telerik.Reporting.CategoryScale categoryScale1 = new Telerik.Reporting.CategoryScale();
            Telerik.Reporting.GraphGroup graphGroup3 = new Telerik.Reporting.GraphGroup();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.graph1 = new Telerik.Reporting.Graph();
            this.objectDataSource1 = new Telerik.Reporting.ObjectDataSource();
            this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
            this.polarCoordinateSystem1 = new Telerik.Reporting.PolarCoordinateSystem();
            this.graphAxis1 = new Telerik.Reporting.GraphAxis();
            this.graphAxis2 = new Telerik.Reporting.GraphAxis();
            this.barSeries1 = new Telerik.Reporting.BarSeries();
            this.barSeries2 = new Telerik.Reporting.BarSeries();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(3D);
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(9D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.graph1});
            this.detail.Name = "detail";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(3D);
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // graph1
            // 
            graphGroup2.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.Restante"));
            graphGroup2.Name = "restanteGroup";
            graphGroup2.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.Restante", Telerik.Reporting.SortDirection.Asc));
            graphGroup1.ChildGroups.Add(graphGroup2);
            graphGroup1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.Pagado"));
            graphGroup1.Name = "pagadoGroup";
            graphGroup1.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.Pagado", Telerik.Reporting.SortDirection.Asc));
            this.graph1.CategoryGroups.Add(graphGroup1);
            this.graph1.CoordinateSystems.Add(this.polarCoordinateSystem1);
            this.graph1.DataSource = this.sqlDataSource1;
            this.graph1.Legend.Style.LineColor = System.Drawing.Color.LightGray;
            this.graph1.Legend.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            this.graph1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.5D), Telerik.Reporting.Drawing.Unit.Cm(0.299999862909317D));
            this.graph1.Name = "graph1";
            this.graph1.PlotAreaStyle.LineColor = System.Drawing.Color.LightGray;
            this.graph1.PlotAreaStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            this.graph1.Series.Add(this.barSeries1);
            this.graph1.Series.Add(this.barSeries2);
            graphGroup4.ChildGroups.Add(graphGroup3);
            graphGroup4.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.Pagado"));
            graphGroup4.Name = "pagadoGroup1";
            graphGroup4.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.Pagado", Telerik.Reporting.SortDirection.Asc));
            this.graph1.SeriesGroups.Add(graphGroup4);
            this.graph1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(13.199999809265137D), Telerik.Reporting.Drawing.Unit.Cm(7.90000057220459D));
            graphTitle1.Position = Telerik.Reporting.GraphItemPosition.TopCenter;
            graphTitle1.Style.LineColor = System.Drawing.Color.LightGray;
            graphTitle1.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            graphTitle1.Text = "graph1";
            this.graph1.Titles.Add(graphTitle1);
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionString = "Data Source=10.1.0.120;Initial Catalog=SIANCENTRAL;Persist Security Info=True;Use" +
    "r ID=sa;Password=sistemas";
            this.sqlDataSource1.Name = "sqlDataSource1";
            this.sqlDataSource1.Parameters.AddRange(new Telerik.Reporting.SqlDataSourceParameter[] {
            new Telerik.Reporting.SqlDataSourceParameter("@FechaCorte", System.Data.DbType.DateTime, null),
            new Telerik.Reporting.SqlDataSourceParameter("@DiasRevision", System.Data.DbType.Int32, null),
            new Telerik.Reporting.SqlDataSourceParameter("@Id_Cte", System.Data.DbType.Int32, null),
            new Telerik.Reporting.SqlDataSourceParameter("@Id_cd", System.Data.DbType.Int32, null)});
            this.sqlDataSource1.ProviderName = "System.Data.SqlClient";
            this.sqlDataSource1.SelectCommand = "dbo.spCobSaldosNivel2";
            this.sqlDataSource1.SelectCommandType = Telerik.Reporting.SqlDataSourceCommandType.StoredProcedure;
            // 
            // polarCoordinateSystem1
            // 
            this.polarCoordinateSystem1.AngularAxis = this.graphAxis1;
            this.polarCoordinateSystem1.InnerRadiusRatio = 0.3D;
            this.polarCoordinateSystem1.Name = "polarCoordinateSystem1";
            this.polarCoordinateSystem1.RadialAxis = this.graphAxis2;
            // 
            // graphAxis1
            // 
            this.graphAxis1.MajorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis1.MajorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis1.MajorGridLineStyle.Visible = false;
            this.graphAxis1.MinorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis1.MinorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis1.MinorGridLineStyle.Visible = false;
            this.graphAxis1.Name = "graphAxis1";
            this.graphAxis1.Scale = numericalScale1;
            this.graphAxis1.Style.Visible = false;
            // 
            // graphAxis2
            // 
            this.graphAxis2.MajorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis2.MajorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis2.MajorGridLineStyle.Visible = false;
            this.graphAxis2.MinorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis2.MinorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis2.MinorGridLineStyle.Visible = false;
            this.graphAxis2.Name = "graphAxis2";
            categoryScale1.PositionMode = Telerik.Reporting.AxisPositionMode.OnTicks;
            categoryScale1.SpacingSlotCount = 0D;
            this.graphAxis2.Scale = categoryScale1;
            this.graphAxis2.Style.Visible = false;
            // 
            // barSeries1
            // 
            this.barSeries1.ArrangeMode = Telerik.Reporting.GraphSeriesArrangeMode.Stacked100;
            this.barSeries1.CategoryGroup = graphGroup2;
            this.barSeries1.CoordinateSystem = this.polarCoordinateSystem1;
            this.barSeries1.DataPointLabel = "= Sum(Fields.Pagado) / CDbl(Exec(\'graph1\', Sum(Fields.Pagado)))";
            this.barSeries1.DataPointLabelFormat = "{0:P}";
            this.barSeries1.DataPointLabelStyle.Visible = true;
            this.barSeries1.DataPointStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            this.barSeries1.DataPointStyle.Visible = true;
            this.barSeries1.Legend = "=Fields.Pagado + \'/\' + Fields.Restante + \'/\' + \'Sum(Pagado)\'";
            this.barSeries1.LegendFormat = "";
            graphGroup3.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.Restante"));
            graphGroup3.Name = "restanteGroup1";
            graphGroup3.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.Restante", Telerik.Reporting.SortDirection.Asc));
            this.barSeries1.SeriesGroup = graphGroup3;
            this.barSeries1.X = "=Sum(Fields.Pagado)";
            // 
            // barSeries2
            // 
            this.barSeries2.ArrangeMode = Telerik.Reporting.GraphSeriesArrangeMode.Stacked100;
            this.barSeries2.CategoryGroup = graphGroup2;
            this.barSeries2.CoordinateSystem = this.polarCoordinateSystem1;
            this.barSeries2.DataPointLabel = "= Sum(Fields.Restante) / CDbl(Exec(\'graph1\', Sum(Fields.Restante)))";
            this.barSeries2.DataPointLabelFormat = "{0:P}";
            this.barSeries2.DataPointLabelStyle.Visible = true;
            this.barSeries2.DataPointStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            this.barSeries2.DataPointStyle.Visible = true;
            this.barSeries2.Legend = "=Fields.Pagado + \'/\' + Fields.Restante + \'/\' + \'Sum(Restante)\'";
            this.barSeries2.LegendFormat = "";
            this.barSeries2.SeriesGroup = graphGroup3;
            this.barSeries2.X = "=Sum(Fields.Restante)";
            // 
            // pruebafinal
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1});
            this.Name = "pruebafinal";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(16.100000381469727D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.Graph graph1;
        private Telerik.Reporting.PolarCoordinateSystem polarCoordinateSystem1;
        private Telerik.Reporting.GraphAxis graphAxis1;
        private Telerik.Reporting.GraphAxis graphAxis2;
        private Telerik.Reporting.SqlDataSource sqlDataSource1;
        private Telerik.Reporting.BarSeries barSeries1;
        private Telerik.Reporting.BarSeries barSeries2;
        private Telerik.Reporting.ObjectDataSource objectDataSource1;
    }
}