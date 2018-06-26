namespace LibreriaReportes
{
    partial class DevParcial_Imprimir
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
            Telerik.Reporting.ReportParameter reportParameter16 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter17 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter18 = new Telerik.Reporting.ReportParameter();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.txtNCliente = new Telerik.Reporting.TextBox();
            this.txtDireccion = new Telerik.Reporting.TextBox();
            this.txtIdCliente = new Telerik.Reporting.TextBox();
            this.txtId_Dev = new Telerik.Reporting.TextBox();
            this.txtDevolucion = new Telerik.Reporting.TextBox();
            this.txtDFactura = new Telerik.Reporting.TextBox();
            this.txtId_Cd = new Telerik.Reporting.TextBox();
            this.txtId_Ter = new Telerik.Reporting.TextBox();
            this.txtId_Rik = new Telerik.Reporting.TextBox();
            this.txtFecha = new Telerik.Reporting.TextBox();
            this.txtTipoMov2 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.txtId_Prd = new Telerik.Reporting.TextBox();
            this.txtPrecio = new Telerik.Reporting.TextBox();
            this.txtCantidad = new Telerik.Reporting.TextBox();
            this.txtPrdDesc = new Telerik.Reporting.TextBox();
            this.txtPrd_Present = new Telerik.Reporting.TextBox();
            this.txtImporte = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.txtNca_Subtotal = new Telerik.Reporting.TextBox();
            this.txtNca_Iva = new Telerik.Reporting.TextBox();
            this.txtNca_Total = new Telerik.Reporting.TextBox();
            this.txtId_Nca = new Telerik.Reporting.TextBox();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.sqlConnection2 = new System.Data.SqlClient.SqlConnection();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
            this.group1 = new Telerik.Reporting.Group();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.textBox61 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = new Telerik.Reporting.Drawing.Unit(2.1000001430511475D, Telerik.Reporting.Drawing.UnitType.Inch);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtNCliente,
            this.txtDireccion,
            this.txtIdCliente,
            this.txtId_Dev,
            this.txtDevolucion,
            this.txtDFactura,
            this.txtId_Cd,
            this.txtId_Ter,
            this.txtId_Rik,
            this.txtFecha,
            this.txtTipoMov2});
            this.pageHeader.Name = "pageHeader";
            // 
            // txtNCliente
            // 
            this.txtNCliente.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.1000000610947609D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.800000011920929D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtNCliente.Name = "txtNCliente";
            this.txtNCliente.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.6000001430511475D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtNCliente.Value = "= Parameters.[Cliente]";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.1000000610947609D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.1000000238418579D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.4000003337860107D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtDireccion.Value = "= Parameters.[Cte_FacCalle1]";
            // 
            // txtIdCliente
            // 
            this.txtIdCliente.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.800000011920929D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtIdCliente.Name = "txtIdCliente";
            this.txtIdCliente.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.800000011920929D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.20000004768371582D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtIdCliente.Value = "= Parameters.[Num_Cliente]";
            // 
            // txtId_Dev
            // 
            this.txtId_Dev.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.800000011920929D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtId_Dev.Name = "txtId_Dev";
            this.txtId_Dev.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.30000051856040955D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtId_Dev.Value = "= Parameters.[Id_Nca2]";
            // 
            // txtDevolucion
            // 
            this.txtDevolucion.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.3999998569488525D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtDevolucion.Name = "txtDevolucion";
            this.txtDevolucion.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.0998423099517822D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.20000004768371582D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtDevolucion.Value = "= Parameters.[TipoMov]";
            // 
            // txtDFactura
            // 
            this.txtDFactura.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.7000002861022949D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtDFactura.Name = "txtDFactura";
            this.txtDFactura.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.80000019073486328D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.20000004768371582D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtDFactura.Value = "= Parameters.[DatoFactura]";
            // 
            // txtId_Cd
            // 
            this.txtId_Cd.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.699999988079071D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.4000000953674316D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtId_Cd.Name = "txtId_Cd";
            this.txtId_Cd.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.89999985694885254D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtId_Cd.Value = "= Parameters.[Id_Cd]";
            // 
            // txtId_Ter
            // 
            this.txtId_Ter.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.5D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.4000000953674316D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtId_Ter.Name = "txtId_Ter";
            this.txtId_Ter.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.3999999463558197D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtId_Ter.Value = "= Parameters.[Id_Ter]";
            // 
            // txtId_Rik
            // 
            this.txtId_Rik.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.3000001907348633D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(1.4000000953674316D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtId_Rik.Name = "txtId_Rik";
            this.txtId_Rik.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.599999725818634D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999997317790985D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtId_Rik.Value = "= Parameters.[Id_Rik]";
            // 
            // txtFecha
            // 
            this.txtFecha.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.5D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.0000003576278687D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.20000004768371582D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtFecha.Value = "= Parameters.[Fecha].ToString(\"dd/MM/yyyy\")";
            // 
            // txtTipoMov2
            // 
            this.txtTipoMov2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.4000000953674316D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.800000011920929D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtTipoMov2.Name = "txtTipoMov2";
            this.txtTipoMov2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.100001335144043D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.20000004768371582D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtTipoMov2.Value = "= Parameters.[TipoMov]";
            // 
            // detail
            // 
            this.detail.Height = new Telerik.Reporting.Drawing.Unit(0.20003955066204071D, Telerik.Reporting.Drawing.UnitType.Inch);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtId_Prd,
            this.txtPrecio,
            this.txtCantidad,
            this.txtPrdDesc,
            this.txtPrd_Present,
            this.txtImporte});
            this.detail.Name = "detail";
            // 
            // txtId_Prd
            // 
            this.txtId_Prd.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.30000004172325134D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(3.9418537198798731E-05D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtId_Prd.Name = "txtId_Prd";
            this.txtId_Prd.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.20000012218952179D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtId_Prd.Value = "=Fields.[Id_Prd]";
            // 
            // txtPrecio
            // 
            this.txtPrecio.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.5999999046325684D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(3.9418537198798731E-05D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.60000008344650269D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.20000012218952179D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtPrecio.Value = "=Fields.[Dev_Precio].ToString(\'##0.00\')";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.8000004291534424D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(3.9418537198798731E-05D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.599999725818634D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.20000012218952179D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtCantidad.Value = "=Fields.[Dev_Cant]";
            // 
            // txtPrdDesc
            // 
            this.txtPrdDesc.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.90000009536743164D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(3.9418537198798731E-05D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtPrdDesc.Name = "txtPrdDesc";
            this.txtPrdDesc.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.7999998331069946D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.20000012218952179D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtPrdDesc.Value = "=Fields.[Prd_Descripcion]";
            // 
            // txtPrd_Present
            // 
            this.txtPrd_Present.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.8998429775238037D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(3.9418537198798731E-05D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtPrd_Present.Name = "txtPrd_Present";
            this.txtPrd_Present.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.70015746355056763D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.20000012218952179D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtPrd_Present.Value = "=Fields.[Prd_Presentacion]";
            // 
            // txtImporte
            // 
            this.txtImporte.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.5D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(3.9418537198798731E-05D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.90000087022781372D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.20000012218952179D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtImporte.Value = "=Fields.Importe.ToString(\'##0.00\')";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = new Telerik.Reporting.Drawing.Unit(0.27558961510658264D, Telerik.Reporting.Drawing.UnitType.Inch);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox61});
            this.pageFooter.Name = "pageFooter";
            // 
            // txtNca_Subtotal
            // 
            this.txtNca_Subtotal.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.5D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.21885807812213898D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtNca_Subtotal.Name = "txtNca_Subtotal";
            this.txtNca_Subtotal.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.900000274181366D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.20000012218952179D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtNca_Subtotal.Value = "= Parameters.[Nca_Subtotal].ToString(\'##0.00\')";
            // 
            // txtNca_Iva
            // 
            this.txtNca_Iva.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.5D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.51885795593261719D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtNca_Iva.Name = "txtNca_Iva";
            this.txtNca_Iva.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.900000274181366D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.20000012218952179D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtNca_Iva.Value = "= Parameters.[Nca_Iva].ToString(\'##0.00\')";
            // 
            // txtNca_Total
            // 
            this.txtNca_Total.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.5D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.81881839036941528D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtNca_Total.Name = "txtNca_Total";
            this.txtNca_Total.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.900000274181366D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.19999980926513672D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtNca_Total.Value = "= Parameters.[Nca_Total].ToString(\'##0.00\')";
            // 
            // txtId_Nca
            // 
            this.txtId_Nca.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.31881842017173767D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtId_Nca.Name = "txtId_Nca";
            this.txtId_Nca.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.7999998927116394D, Telerik.Reporting.Drawing.UnitType.Inch), new Telerik.Reporting.Drawing.Unit(0.20000012218952179D, Telerik.Reporting.Drawing.UnitType.Inch));
            this.txtId_Nca.Value = "= Parameters.[Id_Nca]";
            // 
            // sqlConnection1
            // 
            this.sqlConnection1.ConnectionString = "Data Source=AXSDMS-02\\SQL2008;Initial Catalog=SIANWEB;Persist Security Info=True;" +
                "User ID=sa;Password=adminadmin";
            this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlConnection2
            // 
            this.sqlConnection2.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "dbo.spCapDevParcial_ConsultaImpresion";
            this.sqlSelectCommand1.CommandTimeout = 600;
            this.sqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlSelectCommand1.Connection = this.sqlConnection2;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, 0),
            new System.Data.SqlClient.SqlParameter("@Id_Emp", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, ""),
            new System.Data.SqlClient.SqlParameter("@Id_Cd", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, ""),
            new System.Data.SqlClient.SqlParameter("@Id_Dev", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, "")});
            // 
            // sqlDataAdapter1
            // 
            this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
            this.sqlDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "spCapDevParcial_ConsultaImpresion", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Id_Emp", "Id_Emp"),
                        new System.Data.Common.DataColumnMapping("Id_Cd", "Id_Cd"),
                        new System.Data.Common.DataColumnMapping("Id_Prd", "Id_Prd"),
                        new System.Data.Common.DataColumnMapping("Prd_Descripcion", "Prd_Descripcion"),
                        new System.Data.Common.DataColumnMapping("Prd_Presentacion", "Prd_Presentacion"),
                        new System.Data.Common.DataColumnMapping("Dev_Cant", "Dev_Cant"),
                        new System.Data.Common.DataColumnMapping("Dev_Precio", "Dev_Precio"),
                        new System.Data.Common.DataColumnMapping("Importe", "Importe")})});
            // 
            // group1
            // 
            //this.group1.Bookmark = null;
            this.group1.GroupFooter = this.groupFooterSection1;
            this.group1.GroupHeader = this.groupHeaderSection1;
            // 
            // groupFooterSection1
            // 
            this.groupFooterSection1.Height = new Telerik.Reporting.Drawing.Unit(3D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.groupFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtNca_Subtotal,
            this.txtNca_Iva,
            this.txtNca_Total,
            this.txtId_Nca});
            this.groupFooterSection1.Name = "groupFooterSection1";
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = new Telerik.Reporting.Drawing.Unit(0.5079994797706604D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.groupHeaderSection1.Name = "groupHeaderSection1";
            // 
            // textBox61
            // 
            this.textBox61.CanGrow = false;
            this.textBox61.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(4.3550014495849609D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.20000000298023224D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox61.Name = "textBox61";
            this.textBox61.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.8000001907348633D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999803304672241D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox61.Style.Font.Name = "Verdana";
            this.textBox61.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8D, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox61.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox61.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox61.TextWrap = false;
            this.textBox61.Value = "= \"Página \" + PageNumber + \" de \" + PageCount";
            // 
            // DevParcial_Imprimir
            // 
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            this.group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection1,
            this.groupFooterSection1,
            this.pageHeader,
            this.detail,
            this.pageFooter});
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = new Telerik.Reporting.Drawing.Unit(1D, Telerik.Reporting.Drawing.UnitType.Inch);
            this.PageSettings.Margins.Left = new Telerik.Reporting.Drawing.Unit(1D, Telerik.Reporting.Drawing.UnitType.Inch);
            this.PageSettings.Margins.Right = new Telerik.Reporting.Drawing.Unit(1D, Telerik.Reporting.Drawing.UnitType.Inch);
            this.PageSettings.Margins.Top = new Telerik.Reporting.Drawing.Unit(1D, Telerik.Reporting.Drawing.UnitType.Inch);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            reportParameter1.Name = "TipoMov";
            reportParameter2.Name = "Estatus";
            reportParameter3.Name = "Id_Nca";
            reportParameter4.Name = "Id_Nca2";
            reportParameter5.Name = "Id_Ter";
            reportParameter6.Name = "Id_Rik";
            reportParameter7.Name = "Fecha";
            reportParameter8.Name = "Factura";
            reportParameter9.Name = "Num_Cliente";
            reportParameter10.Name = "Cliente";
            reportParameter11.Name = "Cte_FacCalle1";
            reportParameter12.Name = "Nca_Subtotal";
            reportParameter13.Name = "Nca_Iva";
            reportParameter14.Name = "Nca_Total";
            reportParameter15.Name = "DatoFactura";
            reportParameter16.Name = "Id_Emp";
            reportParameter17.Name = "Id_Cd";
            reportParameter18.Name = "Emp_Cnx";
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
            this.ReportParameters.Add(reportParameter16);
            this.ReportParameters.Add(reportParameter17);
            this.ReportParameters.Add(reportParameter18);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = new Telerik.Reporting.Drawing.Unit(16.510002136230469D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.NeedDataSource += new System.EventHandler(this.DevParcial_Imprimir_NeedDataSource);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private System.Data.SqlClient.SqlConnection sqlConnection1;
        private System.Data.SqlClient.SqlConnection sqlConnection2;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
        private Telerik.Reporting.TextBox txtNCliente;
        private Telerik.Reporting.TextBox txtDireccion;
        private Telerik.Reporting.TextBox txtIdCliente;
        private Telerik.Reporting.TextBox txtId_Dev;
        private Telerik.Reporting.TextBox txtDevolucion;
        private Telerik.Reporting.TextBox txtDFactura;
        private Telerik.Reporting.TextBox txtId_Cd;
        private Telerik.Reporting.TextBox txtId_Ter;
        private Telerik.Reporting.TextBox txtId_Rik;
        private Telerik.Reporting.TextBox txtId_Prd;
        private Telerik.Reporting.TextBox txtPrecio;
        private Telerik.Reporting.TextBox txtCantidad;
        private Telerik.Reporting.TextBox txtNca_Subtotal;
        private Telerik.Reporting.TextBox txtNca_Iva;
        private Telerik.Reporting.TextBox txtNca_Total;
        private Telerik.Reporting.TextBox txtPrdDesc;
        private Telerik.Reporting.TextBox txtPrd_Present;
        private Telerik.Reporting.TextBox txtImporte;
        private Telerik.Reporting.TextBox txtFecha;
        private Telerik.Reporting.TextBox txtTipoMov2;
        private Telerik.Reporting.TextBox txtId_Nca;
        private Telerik.Reporting.Group group1;
        private Telerik.Reporting.GroupFooterSection groupFooterSection1;
        private Telerik.Reporting.GroupHeaderSection groupHeaderSection1;
        private Telerik.Reporting.TextBox textBox61;
    }
}