using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using System.Net;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Collections;
using System.Text;
using System.Xml;
using CapaDatos;
using System.Globalization;
using System.Threading;


namespace SIANWEB
{
    public partial class CapFactura : System.Web.UI.Page
    {
        #region Variables
        public string strEmp = System.Configuration.ConfigurationManager.AppSettings["VGEmpresa"];
        public bool HabilitarGuardar
        {
            get
            {
                //DEVUELVE SI SE PUEDE O NO GUARDAR
                return RadToolBar1.Items[5].Visible;
            }
        }
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public string Valor
        {
            get
            {
                return MaximoId();
            }
            set { }
        }
        public List<AdendaDet> ListCab
        {
            get
            {
                return Session["CabeceraFacturacion" + Session.SessionID] as List<AdendaDet>;
            }
            set
            {
                Session["CabeceraFacturacion" + Session.SessionID] = value;
            }
        }
        public List<AdendaDet> ListCabRF
        {
            get
            {
                return Session["CabeceraReFacturacion" + Session.SessionID] as List<AdendaDet>;
            }
            set
            {
                Session["CabeceraReFacturacion" + Session.SessionID] = value;
            }
        }
        public List<AdendaDet> ListDet
        {
            get
            {
                return Session["DetalleFacturacion" + Session.SessionID] as List<AdendaDet>;
            }
            set
            {
                Session["DetalleFacturacion" + Session.SessionID] = value;
            }
        }
        public List<AdendaDet> ListDetRF
        {
            get
            {
                return Session["DetalleReFacturacion" + Session.SessionID] as List<AdendaDet>;
            }
            set
            {
                Session["DetalleReFacturacion" + Session.SessionID] = value;
            }
        }
        //public DataTable listaFacturaDet = new DataTable();//
        public string arrayRemisiones = string.Empty;
        public string ActualAnio { get { return DateTime.Now.Year.ToString(); } }
        public string ActualMes { get { return (DateTime.Now.Month - 1).ToString(); } }
        public string ActualDia { get { return DateTime.Now.Day.ToString(); } }

        //Variable de lista de productos para el combo del grid Editable
        private List<Producto> _listaProductos;
        public List<Producto> ListaProductos
        {
            get { return _listaProductos; }
            set { _listaProductos = value; }
        }

        //Variable de lista de territorios para el combo del grid Editable
        private List<Territorios> _listaTerritorios;
        public List<Territorios> ListaTerritorios
        {
            get { return _listaTerritorios; }
            set { _listaTerritorios = value; }
        }

        //Propiedad de lista de productos (partidas) de la factura
        public DataTable ListaProductosFactura
        {
            get
            {
                return (Session["ListaProductosFactura" + Session.SessionID] as DataTable);
            }
            set
            {
                Session["ListaProductosFactura" + Session.SessionID] = value;
            }
        }

        public DataTable ListaProductosFacturaAdenda
        {
            get
            {
                return (Session["ListaProductosFacturaAdenda" + Session.SessionID] as DataTable);
            }
            set
            {
                Session["ListaProductosFacturaAdenda" + Session.SessionID] = value;
            }
        }
       

        //Propiedad de lista de productos con amortizacion del cliente
        private List<Amortizacion> ListaProductosAmortizacion
        {
            get { return (List<Amortizacion>)Session["ListaAmortizaciones" + Session.SessionID]; }
            set { Session["ListaAmortizaciones" + Session.SessionID] = value; }
        }
        private int cantidad_A = 0;
        public double porcRetencion;
        private int Id_Rem_A = 0;
        private int Rem_Cant_A = 0;
        private string _Editable;
        public string FechaEnable
        {
            get
            {
                return _Editable;// txtFecha.Enabled;
            }
        }
        private string _reFactura;
        public string ReFactura
        {
            get
            {
                return _reFactura;
            }
        }
        private object _producto;
        public object producto
        {
            get { return _producto; }
            set { _producto = value; }
        }
        public bool EsRefactura
        {
            get { return Page.Request.QueryString["reFactura"] == null ? false : true; }

        }

        private FacturaEspecial FacturaEspecial
        {
            get { return (FacturaEspecial)Session["FacturaEspecial" + Session.SessionID]; }
            set { Session["FacturaEspecial" + Session.SessionID] = value; }
        }
        
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                sesion.URL = HttpContext.Current.Request.Url.Host;                
                if (sesion == null)
                    CerrarVentana();
                else
                {
                    
                    if (!Page.IsPostBack)
                    { //obtener valores desde la URL
                        int Id_Fac = Convert.ToInt32(Page.Request.QueryString["Id_Fac"]);
                        int Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                        int Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
                        string facModificable = Page.Request.QueryString["facModificable"].ToString();
                        string  Id_FacSerie = Page.Request.QueryString["Id_FacSerie"].ToString();
                        _Editable = facModificable;
                        if (Page.Request.QueryString["reFactura"] != null)
                        {
                            _reFactura = Page.Request.QueryString["reFactura"].ToString();
                        }
                        _PermisoGuardar = Convert.ToInt32(Page.Request.QueryString["permisoGuardar"]) == 1 ? true : false;
                        _PermisoModificar = Convert.ToInt32(Page.Request.QueryString["permisoModificar"]) == 1 ? true : false;
                        _PermisoEliminar = Convert.ToInt32(Page.Request.QueryString["permisoEliminar"]) == 1 ? true : false;
                        _PermisoImprimir = Convert.ToInt32(Page.Request.QueryString["permisoImprimir"]) == 1 ? true : false;
                        this.Inicializar(Id_Emp, Id_Cd, Id_Fac, Id_FacSerie, facModificable);
                        
                        sesion.HoraInicio = DateTime.Now;
                        
                        


                        if (Page.Request.QueryString["tipo"] != null)
                        {
                            rgFacturaDet.Columns.FindByUniqueName("Id_Ter").Display = false;
                            rgFacturaDet.Columns.FindByUniqueName("Id_TerN").HeaderText = "Ter.";
                            double width_Prd = rgFacturaDet.Columns.FindByUniqueName("Id_Prd").HeaderStyle.Width.Value;
                            double width_Ter = rgFacturaDet.Columns.FindByUniqueName("Id_Ter").HeaderStyle.Width.Value;

                            rgFacturaDet.Columns.FindByUniqueName("Id_Prd").HeaderStyle.Width = (Unit)(width_Prd + width_Ter - 42);
                        }

                        //rgFacturaDetAde
                        if (Page.Request.QueryString["tipo"] != null)
                        {                            
                            double width_Prd = rgFacturaDetAde.Columns.FindByUniqueName("Id_Prd").HeaderStyle.Width.Value; //DataField="Id_Prd"    PRODUCTO                            
                            rgFacturaDetAde.Columns.FindByUniqueName("Id_Prd").HeaderStyle.Width = (Unit)(width_Prd);

                        }
                        //rgFacturaDetAde

                        if (facModificable == "0" || !((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible || !string.IsNullOrEmpty(_reFactura))
                        {
                            deshabilitarcontroles(formularioDatosGenerales.Controls, _reFactura);
                            deshabilitarcontroles(formularioTotales.Controls, _reFactura);
                            if (!string.IsNullOrEmpty(_reFactura))
                                HabilitarColumnas(true);
                            else
                            {
                                if (facModificable == "0")
                                    HabilitarColumnas(true);
                                else
                                    HabilitarColumnas(false);
                            }
                            txtClienteNombre.Enabled = false;
                        }

                        double ancho = 0;
                        foreach (GridColumn gc in rgFacturaDet.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgFacturaDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgFacturaDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgFacturaDet.Rebind();


                        if (EsRefactura || facModificable == "0")
                        { ((GridCommandItem)rgFacturaDet.MasterTableView.GetItems(GridItemType.CommandItem)[0]).FindControl("AddNewRecordButton").Parent.Visible = false; }
                       

                        //rgFacturaDetAde
                        double ancho2 = 0;
                        foreach (GridColumn gc in rgFacturaDetAde.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho2 = ancho2 + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgFacturaDetAde.Width = Unit.Pixel(Convert.ToInt32(ancho2));
                        rgFacturaDetAde.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho2));
                        rgFacturaDetAde.Rebind();


                        if (facModificable == "0")
                            //if (EsRefactura || facModificable == "0")
                        {                            
                            ((GridCommandItem)rgFacturaDetAde.MasterTableView.GetItems(GridItemType.CommandItem)[0]).FindControl("AddNewRecordButton").Parent.Visible = false; 
                        }
                        
                    }

                   
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void deshabilitarcontroles(System.Web.UI.ControlCollection controles_contenidos, string _reFactura)
        {
            for (int x = 0; x < controles_contenidos.Count; x++)
            {
                string Type = controles_contenidos[x].GetType().FullName;

                if (Type.Contains("RadMultiPage") || Type.Contains("RadPageView") || Type.Contains("Panel"))
                {
                    deshabilitarcontroles(controles_contenidos[x].Controls, _reFactura);
                }

                switch (Type.Replace("Telerik.Web.UI.", ""))
                {
                    case "RadNumericTextBox":
                        (controles_contenidos[x] as RadNumericTextBox).Enabled = false;
                        break;
                    case "RadTextBox":
                        (controles_contenidos[x] as RadTextBox).Enabled = false;
                        break;
                    case "RadComboBox":
                        (controles_contenidos[x] as RadComboBox).Enabled = false;
                        break;
                    case "RadDatePicker":
                        (controles_contenidos[x] as RadDatePicker).Enabled = false;
                        if ((controles_contenidos[x] as RadDatePicker).ID == txtFecha.ID && !string.IsNullOrEmpty(_reFactura))
                        {
                            (controles_contenidos[x] as RadDatePicker).Enabled = true;
                            _Editable = "1";
                        }
                        break;
                }

                if (Type.Contains("CheckBox"))
                {
                    (controles_contenidos[x] as CheckBox).Enabled = false;
                }

                if (Type.Contains("ImageButton"))
                {
                    (controles_contenidos[x] as ImageButton).Enabled = false;
                }
            }
        }
        protected void cmbTerritorio_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if ((txtCliente.Text != string.Empty) && (this.cmbMov.SelectedValue != "-1" && this.cmbMov.SelectedValue != string.Empty)
                        && (cmbTerritorio.SelectedValue != "-1" && cmbTerritorio.SelectedValue != string.Empty))
                {
                    this.rgFacturaDet.Enabled = true;
                    this.btnFacturaEspecial.Enabled = true;
                }
                else
                {
                    this.rgFacturaDet.Enabled = false;
                    this.btnFacturaEspecial.Enabled = false;
                }

                CN_CatTerritorios territorio = new CN_CatTerritorios();
                Territorios ter = new Territorios();
                ter.Id_Emp = sesion.Id_Emp;
                ter.Id_Cd = sesion.Id_Cd_Ver;
                ter.Id_Ter = Convert.ToInt32(cmbTerritorio.SelectedValue);
                territorio.ConsultaTerritoriosCombo(ref ter, sesion.Emp_Cnx);
                txtRepresentante.Text = ter.Id_Rik.ToString();
                txtRepresentanteStr.Text = ter.Rik_Nombre;
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message));
            }
        }
        protected void cmbMov_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                if (Session["PedidoFacturacion" + Session.SessionID] != null)
                {
                    if (e.Value != "51" && e.Value != "52")
                    {
                        this.DisplayMensajeAlerta("MovFacturacionPedidoNoValido");
                        txtMov.Text = string.Empty;
                        cmbMov.SelectedIndex = cmbMov.FindItemIndexByValue("-1");
                        return;
                    }
                }

                if ((e.Value != "-1" && e.Value != string.Empty) && (txtCliente.Value.HasValue))
                {
                    this.rgFacturaDet.Enabled = true;
                    this.btnFacturaEspecial.Enabled = true;
                }
                else
                {
                    this.rgFacturaDet.Enabled = false;
                    this.btnFacturaEspecial.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void cmbConsFacEle_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                int valor = cmbConsFacEle.SelectedValue == "" ? -1 : Convert.ToInt32(cmbConsFacEle.SelectedValue);
                if (!this.ObtenerConsecutivoFactElectronica(valor))
                    Alerta("No hay consecutivo de facturación electrónica disponible para la serie seleccionada");
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "cmbConsFacEle_ObtenerConsFacElectFallo"));
            }
        }
        protected void cmbProducto_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            try
            {
                ErrorManager();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "cmbProductosLista_ItemsDataBound"));
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            string mensajeError = string.Empty;
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "save":
                        mensajeError = hiddenId.Value == string.Empty ? "CapFactura_insert_error" : "CapFactura_update_error";
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("No hay cantidad suficiente en el inventario para facturar el producto"))
                {
                    Alerta(ex.Message);
                    return;
                }
                else if (ex.Message.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).Length > 1)
                {
                    Alerta(ex.Message.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)[0]);
                    return;
                }

                else
                {
                    string mensaje = string.Concat(ex.Message, mensajeError);
                    this.DisplayMensajeAlerta(mensaje);
                    return;
                }
            }
            finally
            {
                RAM1.ResponseScripts.Add("HabilitarGuardar();");
            }

        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                ErrorManager();
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":
                        rgFacturaDet.Rebind();
                        //rgFacturaDetAde
                        rgFacturaDetAde.Rebind();
                        //rgFacturaDetAde
                        break;
                    case "cliente":
                        txtCliente.Text = Session["Id_Buscar" + Session.SessionID].ToString();
                        txtCliente_TextChanged(null, null);
                        break;
                    case "precio":
                        if ((producto as RadNumericTextBox).Enabled)
                        {
                            (producto as RadNumericTextBox).DbValue = Session["Id_Buscar" + Session.SessionID];
                            txtProducto_TextChanged(producto, null);
                            if ((producto as RadNumericTextBox).Value.HasValue)
                            {
                                ((producto as RadNumericTextBox).Parent.FindControl("txtFac_Cantidad") as RadNumericTextBox).Focus();
                            }
                        }
                        break;
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 150);
                        RadPageViewDetalles.Height = altura;
                        RadPane1.Height = altura;
                        RadPane1.Width = RadPageViewDGenerales.Width;
                        RadSplitter1.Height = altura;
                        RadPageViewDGenerales.Height = altura;
                        RadSplitter2.Height = altura;
                        RadPane2.Height = altura;
                        RadPane2.Width = RadPageViewDGenerales.Width;
                        break;
                    case "AjustarCentavos":
                        txtSubTotal.DbValue = FacturaEspecial.FacEsp_SubTotal;
                        txtIVA.DbValue = FacturaEspecial.FacEsp_ImporteIva;
                        txtTotal.DbValue = FacturaEspecial.FacEsp_Total;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "RAM1_AjaxRequest"));
            }
        }
        protected void rgFacturaDet_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                Session["CantidadEditar" + Session.SessionID] = 0;
                Session["CantidadRemision" + Session.SessionID] = 0;
                Session["Remision" + Session.SessionID] = 0;
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgFacturaDet.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        break;
                    case "Edit":
                        cantidad_A = int.Parse((rgFacturaDet.MasterTableView.Items[e.Item.ItemIndex]["Fac_Cant"].FindControl("lblOrd_Cantidad") as Label).Text);
                        Id_Rem_A = !string.IsNullOrEmpty(rgFacturaDet.MasterTableView.Items[e.Item.ItemIndex]["Id_Rem"].Text.Replace("&nbsp;", "")) ? int.Parse(rgFacturaDet.MasterTableView.Items[e.Item.ItemIndex]["Id_Rem"].Text) : 0;
                        Rem_Cant_A = !string.IsNullOrEmpty(rgFacturaDet.MasterTableView.Items[e.Item.ItemIndex]["Rem_Cant"].Text) ? Int32.TryParse(rgFacturaDet.MasterTableView.Items[e.Item.ItemIndex]["Rem_Cant"].Text, out Rem_Cant_A) ? Rem_Cant_A : 0 : 0;
                        Session["CantidadEditar" + Session.SessionID] = cantidad_A;//(rgFacturaDet.MasterTableView.Items[e.Item.RowIndex]["Fac_Cant"].FindControl("lblOrd_Cantidad") as Label).Text;
                        Session["CantidadRemision" + Session.SessionID] = Rem_Cant_A;
                        Session["Remision" + Session.SessionID] = Id_Rem_A;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void rgFacturaDet_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try

            {
                ErrorManager();
                this.rgFacturaDet.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }
        protected void rgFacturaDet_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                { //crear dataSource del combo de productos de cada registro del Grid
                    this.EstablecerDataSourceProductosLista(string.Empty);
                    //Llenar Grid
                    rgFacturaDet.DataSource = this.ListaProductosFactura;
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgFacturaDet_NeedDataSource"));
            }
        }
        protected void rgFacturaDet_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                ErrorManager();
                if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    GridEditableItem editItem = (GridEditableItem)e.Item;

                    //obtener nombres de los controles de formulario de inserción/edición de registro de grid.
                    RadNumericTextBox Ctrl_txtOrd_Cantidad = (RadNumericTextBox)editItem.FindControl("txtFac_Cantidad");
                    string lblFac_Cantidad = ((Label)editItem.FindControl("lblVal_txtFac_Cantidad")).ClientID.ToString();
                    string txtFac_Cantidad = Ctrl_txtOrd_Cantidad.ClientID.ToString();
                    string lbl_cmbProducto = ((Label)editItem.FindControl("lbl_cmbProducto")).ClientID.ToString();
                    string txtId_Prd = ((RadNumericTextBox)editItem.FindControl("txtId_Prd")).ClientID.ToString();
                    string lbltxtTerritorioPartida = ((Label)editItem.FindControl("lbltxtTerritorioPartida")).ClientID.ToString();
                    string txtTerritorioPartida = ((RadNumericTextBox)editItem.FindControl("txtTerritorioPartida")).ClientID.ToString();
                    string lblTxtClienteExterno = ((Label)editItem.FindControl("lblTxtClienteExterno")).ClientID.ToString();
                    string txtClienteExterno = ((RadNumericTextBox)editItem.FindControl("txtClienteExterno")).ClientID.ToString();

                    ////Llenar combo de productos
                    //RadComboBox comboProductoItem = (RadComboBox)editItem.FindControl("cmbProducto");
                    ////comboProductoItem.DataSource = this.ListaProductos;
                    ////comboProductoItem.DataBind();
                    ////
                    //CargarProductos(comboProductoItem);
                    //comboProductoItem.SelectedIndex = 0;

                    //Llenar combo de clientes, solo si es movimiento 70
                    //si no, el combo se oculta
                    //RadComboBox comboClientesItem = (RadComboBox)editItem.FindControl("cmbClienteExterno");
                    RadNumericTextBox txtClienteExternoItem = (RadNumericTextBox)editItem.FindControl("txtClienteExterno");
                    RadTextBox txtClienteExternoStr = (RadTextBox)editItem.FindControl("txtNombreCliente");
                    if (cmbMov.SelectedValue == "70")
                    {
                        //((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExt")).ReadOnly = false;
                        ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExt")).Display = true;
                        ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExtN")).Display = true;

                        //CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                        //CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatCliente_Combo", ref comboClientesItem);
                    }
                    else
                    {
                        ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExtN")).Display = false;
                        ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExt")).Display = false;

                        //editItem["Id_CteExt"].Controls.Clear();
                        //editItem["Id_CteExt"].Style.Add("display", "false");
                        //txtClienteExternoItem.Visible = false;
                        //comboClientesItem.Visible = false;

                        //txtClienteExternoItem.Parent.Visible = false;
                    }

                    //Llenar combo de territorios
                    RadComboBox comboTerritorioPartidaItem = (RadComboBox)editItem.FindControl("cmbTerritorioPartida");
                    List<Territorios> listaTerritorios = new List<Territorios>();
                    new CN_CatCliente().ConsultaTerritoriosDelCliente(Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1), sesion, ref listaTerritorios);
                    comboTerritorioPartidaItem.DataTextField = "Descripcion";
                    comboTerritorioPartidaItem.DataValueField = "Id_Ter";
                    comboTerritorioPartidaItem.DataSource = listaTerritorios;
                    comboTerritorioPartidaItem.DataBind();
                    //comboTerritorioPartidaItem.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

                    string jsControles = string.Concat(
                        "lblFac_CantidadClientId='", lblFac_Cantidad, "';"
                        , "txtFac_CantidadClientId='", txtFac_Cantidad, "';"
                        , "lbl_cmbProductoClientId='", lbl_cmbProducto, "';"
                        , "txtId_PrdClientId='", txtId_Prd, "';"
                        , "lbltxtTerritorioPartidaClientId='", lbltxtTerritorioPartida, "';"
                        , "txtTerritorioPartidaClientId='", txtTerritorioPartida, "';"
                        , "lblTxtClienteExternoClientId='", lblTxtClienteExterno, "';"
                        , "txtClienteExternoClientId='", txtClienteExterno, "';"
                        );

                    ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                    {
                        //cuando la edición se usa para inserción, se habilita el combo de producto
                        //((RadComboBox)editItem.FindControl("cmbProducto")).Enabled = true;
                        //es registro nuevo y se inhabilita el campo de cantidad (se habilita hasta ke elija un producto del combo)
                        Ctrl_txtOrd_Cantidad.Enabled = false;

                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormEdit(\"insertar\");");

                        insertbtn.Attributes.Add("onclick", jsControles);
                        if (txtClienteExternoItem.Parent.Visible == true)
                        {
                            ((RadNumericTextBox)editItem.FindControl("txtClienteExterno")).Focus();
                        }
                        else
                        {
                            ((RadNumericTextBox)editItem.FindControl("txtTerritorioPartida")).Focus();
                        }
                    }

                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {
                        //////establecer unidades de empaque
                        ////int claveOrden = Convert.ToInt32(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Ord"].ToString());
                        ////int claveProducto = Convert.ToInt32(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Prd"].ToString());
                        ////Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        ////Producto producto = null;
                        ////new CN_CatProducto().ConsultaProducto_OrdenCompra(ref producto, sesion.Emp_Cnx, claveOrden, claveProducto, sesion.Id_Emp, sesion.Id_Cd_Ver);
                        ////((HiddenField)editItem.FindControl("HD_Prd_UniEmp")).Value = producto.Prd_UniEmp.ToString();
                        //////-------------------------------


                        //cuando la edición se usa para actualización, se deshabilita el campo de texto y el combo de producto
                        ((RadNumericTextBox)editItem.FindControl("txtId_Prd")).Enabled = false;
                        ((RadNumericTextBox)editItem.FindControl("txtClienteExterno")).Enabled = false;
                        ((RadTextBox)editItem.FindControl("txtNombreCliente")).Enabled = false;
                        ((RadTextBox)editItem.FindControl("txtProductoNombre")).Enabled = false;
                        //((RadComboBox)editItem.FindControl("cmbProducto")).Enabled = false;
                        //es registro de edición, se habilita el campo de cantidad porque ya eligió producto y se estableció las unidades de empaque
                        Ctrl_txtOrd_Cantidad.Enabled = true;

                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormEdit(\"actualizar\");");

                        updatebtn.Attributes.Add("onclick", jsControles);

                        //cuando es actualización se selecciona el producto del combo
                        //comboProductoItem.SelectedValue = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Prd"].ToString();
                        //if (txtClienteExternoItem.Parent.Visible == true)
                        //{
                        //    if (editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_CteExt"] != null)
                        //        txtClienteExternoStr.Text = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_CteExtN"].ToString();
                        //}
                        comboTerritorioPartidaItem.SelectedValue = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Ter"].ToString();
                        Ctrl_txtOrd_Cantidad.Focus();
                    }
                }



                //TODO: AGREGAR PARA PONER EL FOCUS
                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {
                    GridEditableItem form = (GridEditableItem)e.Item;
                    RadNumericTextBox dataField = (RadNumericTextBox)form["Id_TerN"].FindControl("txtTerritorioPartida");
                    if (!dataField.Enabled)
                    {
                        dataField = (RadNumericTextBox)form["Fac_Cant"].FindControl("txtFac_Cantidad");
                    }

                    dataField.Focus();

                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    int VGEmpresa = 0;
                    Int32.TryParse(strEmp, out VGEmpresa);
                    if (VGEmpresa == Sesion.Id_Emp)
                    {
                        ((RadNumericTextBox)form.FindControl("txtFac_Precio")).Enabled = false;
                    }

                    ((RadNumericTextBox)form.FindControl("txtTerritorioPartida")).Enabled = !EsRefactura;
                    ((RadComboBox)form.FindControl("cmbTerritorioPartida")).Enabled = !EsRefactura;
                    ((RadTextBox)form.FindControl("txtPrd_Presentacion")).Enabled = !EsRefactura;
                    ((RadTextBox)form.FindControl("txtPrd_UniNe")).Enabled = !EsRefactura;
                    ((RadNumericTextBox)form.FindControl("txtFac_Cantidad")).Enabled = !EsRefactura;
                    ((RadNumericTextBox)form.FindControl("txtFac_Precio")).Enabled = !EsRefactura;
                }


                //-----------------------------------------
            }
            catch (Exception ex)
            {
                //RadGrid1.Controls.Add(new LiteralControl("No se pudo agregar el Usuario. " + ex.Message));
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgFacturaDet_ItemDataBound"));
            }
        }
        protected void rgFacturaDet_InsertCommand(object sender, GridCommandEventArgs e)
        {
            FacturaDet facturaDet = new FacturaDet();
            GridEditableItem insertedItem = (GridEditableItem)e.Item;
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                facturaDet.Id_Emp = sesion.Id_Emp;
                facturaDet.Id_Cd = sesion.Id_Cd_Ver;
                facturaDet.Id_Fac = Convert.ToInt32(txtId.Text); //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                facturaDet.Id_FacDet = 0;
                facturaDet.Id_Ter = Convert.ToInt32((insertedItem["Id_Ter"].FindControl("cmbTerritorioPartida") as RadComboBox).SelectedValue);
                facturaDet.Id_TerStr = (insertedItem["Id_Ter"].FindControl("cmbTerritorioPartida") as RadComboBox).Text;
                facturaDet.Id_Prd = Convert.ToInt32((insertedItem.FindControl("txtId_Prd") as RadNumericTextBox).Value); //Convert.ToInt32((insertedItem["Id_Prd"].FindControl("cmbProducto") as RadComboBox).SelectedValue);
                if (((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExt")).Display == true)
                {
                    facturaDet.Id_CteExt = Convert.ToInt32((insertedItem["Id_CteExtN"].FindControl("txtClienteExterno") as RadNumericTextBox).Value);
                    facturaDet.Id_CteExtStr = (insertedItem["Id_CteExt"].FindControl("txtNombreCliente") as RadTextBox).Text;
                }
                else
                {
                    facturaDet.Id_CteExt = 0;
                    facturaDet.Id_CteExtStr = string.Empty;
                }
                facturaDet.Fac_Cant = Convert.ToInt32((insertedItem["Fac_Cant"].FindControl("txtFac_Cantidad") as RadNumericTextBox).Text);
                double precioPartida = Convert.ToDouble((insertedItem["Fac_Precio"].FindControl("txtFac_Precio") as RadNumericTextBox).Text.Replace("$", string.Empty));
                double importe = precioPartida * Convert.ToDouble(facturaDet.Fac_Cant);
                if (facturaDet.Fac_Cant == 0)
                {
                    e.Canceled = true;
                    throw new Exception("rgFacturaDet_cantidadCero");
                }
                if (importe <= 0)
                {
                    e.Canceled = true;
                    throw new Exception("rgFacturaDet_cantidadImporteCero");
                }

                (insertedItem["Fac_Importe"].FindControl("lblFac_ImporteEdit") as Label).Text = importe.ToString();
                facturaDet.Fac_Precio = precioPartida;

                //datos del producto de la orden de compra
                facturaDet.Producto = new Producto();
                facturaDet.Producto.Id_Prd = Convert.ToInt32((insertedItem.FindControl("txtId_Prd") as RadNumericTextBox).Value); //Convert.ToInt32((insertedItem.FindControl("cmbProducto") as RadComboBox).SelectedValue);
                facturaDet.Producto.Id_Emp = sesion.Id_Emp;
                facturaDet.Producto.Id_Cd = sesion.Id_Cd_Ver;
                facturaDet.Producto.Prd_Descripcion = (insertedItem["Prd_Descripcion"].FindControl("txtPrd_Descripcion") as RadTextBox).Text;
                facturaDet.Producto.Prd_Presentacion = (insertedItem["Prd_Presentacion"].FindControl("txtPrd_Presentacion") as RadTextBox).Text;
                facturaDet.Producto.Prd_UniNe = (insertedItem["Prd_UniNe"].FindControl("txtPrd_UniNe") as RadTextBox).Text;

                if (ListaProductosFactura.Select("Id_Prd='" + facturaDet.Id_Prd.ToString() + "' and Id_Ter='" + facturaDet.Id_Ter.ToString() + "'").Length > 0)
                {
                    e.Canceled = true;
                    throw new Exception("rgFacturaDet_insert_repetida");
                }
                ArrayList al = new ArrayList();
                al.Add(facturaDet.Id_Fac);
                al.Add(facturaDet.Id_FacDet);
                al.Add(facturaDet.Id_Rem);
                al.Add(0);//id_tm_Rem
                al.Add(facturaDet.Id_CteExt);
                al.Add(facturaDet.Id_Ter);
                al.Add(facturaDet.Id_Prd);
                al.Add(facturaDet.Producto.Prd_Descripcion);
                al.Add(facturaDet.Producto.Prd_Presentacion);
                al.Add(facturaDet.Producto.Prd_UniNe);
                al.Add(facturaDet.Fac_Cant);
                al.Add(facturaDet.Rem_Cant);
                al.Add(facturaDet.Fac_Precio);
                al.Add(importe);
                al.Add(facturaDet.Id_TerStr);
                al.Add(facturaDet.Id_CteExtStr);
                al.Add(facturaDet.AmortizacionProducto);
                al.Add(facturaDet.Id_Emp);
                al.Add(facturaDet.Id_Cd);
                al.Add(facturaDet.Fac_Asignar);
                al.Add(facturaDet.Fac_Devolucion);
                al.Add(facturaDet.Producto.Prd_UniNs);
                
                //GUARDA LA LISTA DE PRODUCTOS AL ARREGLO
                ListaProductosFactura.Rows.Add(al.ToArray());
                    this.CalcularTotales();
                //}
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                if (ex.Message.Contains("rgFacturaDet_InvFinalInsuficiente"))
                {
                    this.AlertaFocus(string.Concat("Producto "
                            , facturaDet.Id_Prd.ToString()
                            , ", inventario disponible insuficiente.<br/>Inventario final: "
                            , HD_Prd_InvFinal.Value
                            , "<br/>Asignado: "
                            , HD_Prd_Asignado.Value
                            , "<br/>Disponible: "
                            , HD_Prd_Disponible.Value), (insertedItem["Fac_Cant"].FindControl("txtFac_Cantidad") as RadNumericTextBox).ClientID);
                    return;
                }
                else
                {
                    if (ex.Message.Contains("rgFacturaDet_cantidadCero"))
                    {
                        this.AlertaFocus("La cantidad del producto ingresado no puede ser cero", (insertedItem["Fac_Cant"].FindControl("txtFac_Cantidad") as RadNumericTextBox).ClientID);
                        return;
                    }
                    else
                        if (ex.Message.Contains("rgFacturaDet_cantidadImporteCero"))
                        {
                            this.AlertaFocus("El importe del producto ingresado no puede ser cero", (insertedItem["Fac_Precio"].FindControl("txtFac_Precio") as RadNumericTextBox).ClientID);
                            return;
                        }
                        else
                        {
                            Alerta(ex.Message);
                            return;
                        }
                }
            }
        }
        protected void rgFacturaDet_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            float cantidadResmisionada = 0;
            double disponible = 0;
            FacturaDet facturaDet = new FacturaDet();
            try
            {
                ErrorManager();
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                //siempre ke se edita un producto se calcula el dsponible porque puede ser un producto de facturacion pedido o remision
                this.ConsultaInventarioProducto(Convert.ToInt32((insertedItem.FindControl("txtId_Prd") as RadNumericTextBox).Value));

                facturaDet.Id_Emp = sesion.Id_Emp;
                facturaDet.Id_Cd = sesion.Id_Cd_Ver;
                facturaDet.Id_Fac = Convert.ToInt32(txtId.Text); //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual

                if (!Convert.IsDBNull(insertedItem.OwnerTableView.DataKeyValues[insertedItem.ItemIndex]["Id_FacDet"]))
                    facturaDet.Id_FacDet = Convert.ToInt32(insertedItem.OwnerTableView.DataKeyValues[insertedItem.ItemIndex]["Id_FacDet"]);
                else
                    facturaDet.Id_FacDet = 0;

                if (!Convert.IsDBNull(insertedItem.OwnerTableView.DataKeyValues[insertedItem.ItemIndex]["Id_Rem"]))
                    facturaDet.Id_Rem = Convert.ToInt32(insertedItem.OwnerTableView.DataKeyValues[insertedItem.ItemIndex]["Id_Rem"]);

                facturaDet.Id_Ter = Convert.ToInt32((insertedItem["Id_Ter"].FindControl("cmbTerritorioPartida") as RadComboBox).SelectedValue);
                facturaDet.Id_TerStr = (insertedItem["Id_Ter"].FindControl("cmbTerritorioPartida") as RadComboBox).Text;
                facturaDet.Id_Prd = Convert.ToInt32((insertedItem.FindControl("txtId_Prd") as RadNumericTextBox).Value);
                if (((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExt")).Display == true)
                {
                    facturaDet.Id_CteExt = Convert.ToInt32((insertedItem["Id_CteExtN"].FindControl("txtClienteExterno") as RadNumericTextBox).Value);
                    facturaDet.Id_CteExtStr = (insertedItem["Id_CteExt"].FindControl("txtNombreCliente") as RadTextBox).Text;
                    //facturaDet.Id_CteExt = Convert.ToInt32((insertedItem["Id_CteExt"].FindControl("cmbClienteExterno") as RadComboBox).SelectedValue);
                    //facturaDet.Id_CteExtStr = (insertedItem["Id_CteExt"].FindControl("cmbClienteExterno") as RadComboBox).Text;
                }
                else
                {
                    facturaDet.Id_CteExt = 0;
                    facturaDet.Id_CteExtStr = string.Empty;
                }
                facturaDet.Fac_Cant = Convert.ToInt32((insertedItem["Fac_Cant"].FindControl("txtFac_Cantidad") as RadNumericTextBox).Text);
                double precioPartida = Convert.ToDouble((insertedItem["Fac_Precio"].FindControl("txtFac_Precio") as RadNumericTextBox).Text.Replace("$", string.Empty));
                double importe = precioPartida * Convert.ToDouble(facturaDet.Fac_Cant);
                (insertedItem["Fac_Importe"].FindControl("lblFac_ImporteEdit") as Label).Text = importe.ToString();
                facturaDet.Fac_Precio = precioPartida;

                if (facturaDet.Fac_Cant == 0)
                {
                    e.Canceled = true;
                    throw new Exception("rgFacturaDet_cantidadCero");
                }

                if (importe <= 0)
                {
                    e.Canceled = true;
                    throw new Exception("rgFacturaDet_cantidadImporteCero");
                }

                //datos del producto de la orden de compra
                facturaDet.Producto = new Producto();
                facturaDet.Producto.Id_Prd = Convert.ToInt32((insertedItem.FindControl("txtId_Prd") as RadNumericTextBox).Value);  //Convert.ToInt32((insertedItem["Id_Prd"].FindControl("cmbProducto") as RadComboBox).SelectedValue);
                facturaDet.Producto.Id_Emp = sesion.Id_Emp;
                facturaDet.Producto.Id_Cd = sesion.Id_Cd_Ver;
                facturaDet.Producto.Prd_Descripcion = (insertedItem["Prd_Descripcion"].FindControl("txtPrd_Descripcion") as RadTextBox).Text;
                facturaDet.Producto.Prd_Presentacion = (insertedItem["Prd_Presentacion"].FindControl("txtPrd_Presentacion") as RadTextBox).Text;
                facturaDet.Producto.Prd_UniNe = (insertedItem["Prd_UniNe"].FindControl("txtPrd_UniNe") as RadTextBox).Text;

                string mensajeInventarioExcepcion = string.Empty;
                //validar cantidad de producto de partida contra Disponible
                //if (facturaDet.Fac_Cant > Convert.ToSingle(HD_Prd_Disponible.Value))
                //    mensajeInventarioExcepcion = "rgFacturaDet_InvFinalInsuficiente";
                //validar cantidad de producto de partida contra cantidad de remisión si es que la factura es de remisiones

                //if (!Convert.IsDBNull(insertedItem.OwnerTableView.DataKeyValues[insertedItem.ItemIndex]["Id_Rem"])) //es facturacion de resmision
                //{
                //    if (!Convert.IsDBNull(insertedItem.OwnerTableView.DataKeyValues[insertedItem.ItemIndex]["Rem_Cant"]))
                //        cantidadResmisionada = Convert.ToSingle(insertedItem.OwnerTableView.DataKeyValues[insertedItem.ItemIndex]["Rem_Cant"]);

                //    facturaDet.Id_Rem = Convert.ToInt32(insertedItem.OwnerTableView.DataKeyValues[insertedItem.ItemIndex]["Id_Rem"]);

                //    facturaDet.Rem_Cant = cantidadResmisionada;
                //    disponible = !string.IsNullOrEmpty(HD_Prd_Disponible.Value) ? Convert.ToInt32(HD_Prd_Disponible.Value) : 0;
                //    disponible += cantidadResmisionada;
                //    if (facturaDet.Id_Rem > 0)
                //        if (facturaDet.Fac_Cant > disponible)
                //            mensajeInventarioExcepcion = "rgFacturaDet_InvFinalRemisionInsuficiente";
                //}

                if (mensajeInventarioExcepcion != string.Empty)
                    throw new Exception(mensajeInventarioExcepcion);
                else //agregar producto de orden de compra a la lista
                {
                    DataRow[] Ar_dr;

                    Ar_dr = ListaProductosFactura.Select("Id_Ter='" + facturaDet.Id_Ter + "' and Id_Prd='" + facturaDet.Id_Prd + "'");
                    if (Ar_dr.Length > 0)
                    {
                        Ar_dr[0].BeginEdit();
                        Ar_dr[0]["Id_Fac"] = facturaDet.Id_Fac;
                        Ar_dr[0]["Id_FacDet"] = facturaDet.Id_FacDet;
                        Ar_dr[0]["Id_Rem"] = facturaDet.Id_Rem;
                        Ar_dr[0]["Id_CteExt"] = facturaDet.Id_CteExt;
                        Ar_dr[0]["Id_Ter"] = facturaDet.Id_Ter;
                        Ar_dr[0]["Id_Prd"] = facturaDet.Id_Prd;
                        Ar_dr[0]["Prd_Descripcion"] = facturaDet.Producto.Prd_Descripcion;
                        Ar_dr[0]["Prd_Presentacion"] = facturaDet.Producto.Prd_Presentacion;
                        Ar_dr[0]["Prd_UniNe"] = facturaDet.Producto.Prd_UniNe;
                        Ar_dr[0]["Fac_Cant"] = facturaDet.Fac_Cant;
                        Ar_dr[0]["Rem_Cant"] = facturaDet.Rem_Cant;
                        Ar_dr[0]["Fac_Precio"] = facturaDet.Fac_Precio;

                        Ar_dr[0]["Fac_Importe"] = importe;
                        Ar_dr[0]["Id_TerStr"] = facturaDet.Id_TerStr;
                        Ar_dr[0]["Id_CteExtStr"] = facturaDet.Id_CteExtStr;
                        Ar_dr[0]["AmortizacionProducto"] = facturaDet.AmortizacionProducto;
                        Ar_dr[0]["Id_Cd"] = facturaDet.Id_Cd;
                        Ar_dr[0]["Fac_Asignar"] = facturaDet.Fac_Asignar;
                        Ar_dr[0]["Fac_Devolucion"] = facturaDet.Fac_Devolucion;
                        Ar_dr[0]["Prd_UniNs"] = facturaDet.Producto.Prd_UniNs;
                        
                        Ar_dr[0].AcceptChanges();
                    }
                    CalcularTotales();
                }

                //GridSortExpression expression = new GridSortExpression();
                //expression.FieldName = "Id_Prd";
                //expression.SetSortOrder("Ascending");
                //this.rgFacturaDet.MasterTableView.SortExpressions.AddSortExpression(expression);
                //this.rgFacturaDet.MasterTableView.Rebind();
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                if (ex.Message.Contains("rgFacturaDet_InvFinalInsuficiente"))
                {
                    this.Alerta(string.Concat("Producto "
                        , facturaDet.Id_Prd.ToString()
                        , ", inventario disponible insuficiente.<br/>Inventario final: ", HD_Prd_InvFinal.Value
                        , "<br/>Asignado: ", HD_Prd_Asignado.Value
                        , "<br/>Disponible: ", HD_Prd_Disponible.Value));
                    return;
                }
                else
                {
                    if (ex.Message.Contains("rgFacturaDet_InvFinalRemisionInsuficiente"))
                    {
                        this.Alerta(string.Concat("Producto "
                        , facturaDet.Id_Prd.ToString()
                        , ", inventario disponible insuficiente.<br/>Remisionado: ", cantidadResmisionada.ToString()
                        , "<br/>Disponible: ", disponible.ToString()));
                        return;
                    }
                    if (ex.Message.Contains("rgFacturaDet_cantidadCero"))
                    {
                        this.Alerta("La cantidad del producto ingresado no puede ser cero");
                        return;
                    }
                    else
                        if (ex.Message.Contains("rgFacturaDet_cantidadImporteCero"))
                        {
                            this.Alerta("El importe del producto ingresado no puede ser cero");
                            return;
                        }
                        else
                        {
                            Alerta(ex.Message.Replace("'", ""));
                            return;
                        }
                }
            }
        }
        protected void rgFacturaDet_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();

                if (rgFacturaDet.EditItems.Count > 0)
                {
                    Alerta("Ya está editando un registro");
                    e.Canceled = true;
                }
                else
                {
                    GridDataItem itemAde = (GridDataItem)e.Item;
                    int Id_PrdFac = Convert.ToInt32(itemAde.OwnerTableView.DataKeyValues[itemAde.ItemIndex]["Id_Prd"]);
                    string eliminar = "SI";
                    if (rgFacturaDetAde.Items.Count > 1)
                    {
                        foreach (GridDataItem item2 in rgFacturaDetAde.Items)
                        {
                            //int IdProducto = Convert.ToInt32( item["Id_Prod"].Text);
                            int IdProducto = Convert.ToInt32(item2.OwnerTableView.DataKeyValues[item2.ItemIndex]["Id_Prd"]);

                            if (IdProducto == Id_PrdFac)
                            {
                                eliminar = "NO";                               
                            }
                        }
                    }

                    if (eliminar=="NO")
                    {
                        Alerta("No se Puede Eliminar este Producto, Existen Adendas Capturadas");                       
                    }
                    else
                    {
                        GridDataItem item = (GridDataItem)e.Item;
                        int id_Fac = Convert.ToInt32(txtId.Text);
                        int Id_FacDet = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_FacDet"]);
                        int Id_Prd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]);
                        int Id_Ter = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Ter"]);
                        //actualizar producto de orden de compra a la lista
                        this.ListaProductosFactura_EliminarProducto(Id_Prd, Id_Ter, id_Fac);                        
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgFacturaDet_ItemCreated(object sender, GridItemEventArgs e)
        {
            
        }

        //rgFacturaDetAde rgFacturaDetAde
        //rgFacturaDetAde rgFacturaDetAde


        protected void rgFacturaDetAde_ItemCommand(object source, GridCommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "InitInsert":
                    if (ListaProductosFactura.Rows.Count == 0)
                    {
                        Alerta("Debe agregar al menos un producto para llenar la Adenda");
                        e.Canceled = true;
                    }
                    else
                        if (rgFacturaDetAde.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        else
                        {
                            if (e.CommandName == RadGrid.InitInsertCommandName)
                            {
                                //Add new" button clicked
                                e.Canceled = true;

                                //Prepare an IDictionary with the predefined values
                                System.Collections.Specialized.ListDictionary newValues = new
                                System.Collections.Specialized.ListDictionary();
                                newValues["Id_Cons_AdeDet"] = generarGUI_IdAdeDet();
                                newValues["Id_Prd"] = string.Empty;
                                newValues["Prd_Descripcion"] = string.Empty;
                                //Insert the item and rebind
                                e.Item.OwnerTableView.InsertItem(newValues);
                            }
                        }
                  break;
            }       

        }
        protected void rgFacturaDetAde_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgFacturaDetAde.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }
        protected void rgFacturaDetAde_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                { 
                    //Llenar Grid
                    rgFacturaDetAde.DataSource = this.ListaProductosFacturaAdenda;                   
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgFacturaDetAde_NeedDataSource"));
            }
        }
        protected void rgFacturaDetAde_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                ErrorManager();
                if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    GridEditableItem editItem = (GridEditableItem)e.Item;           

                    //Llenar combo de Productos de Adenda                                     
                    RadComboBox comboproducto = (RadComboBox)editItem.FindControl("cmbProductoAde");
                                                                               
                    comboproducto.DataValueField = "Id_Prd";
                    comboproducto.DataTextField = "Prd_Descripcion";
                    comboproducto.DataSource = ListaProductosFactura;
                    comboproducto.DataBind();
                    comboproducto.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
                   
                    ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                    {                        
                        ((RadComboBox)editItem.FindControl("cmbProductoAde")).Enabled = true;
                    }

                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {                        
                        //cuando la edición se usa para actualización, se deshabilita el campo de texto y el combo de producto

                        

                        //((RadComboBox)editItem.FindControl("cmbProductoAde")).Enabled = false;
                        //((RadNumericTextBox)editItem.FindControl("txtId_PrdAde")).Enabled = false;
                        comboproducto.SelectedValue = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Prd"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {                
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgFacturaDetAde_ItemDataBound"));
            }
        }        
        protected void rgFacturaDetAde_InsertCommand(object sender, GridCommandEventArgs e)
        {
                        
            GridEditableItem insertedItem = (GridEditableItem)e.Item;
            try
            {

                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                string Id_Cons_AdeDet = "";
                int Id_Prd = 0;
                string Prd_Descripcion = "";

                Id_Cons_AdeDet = generarGUI_IdAdeDet();
                Id_Prd = Convert.ToInt32((insertedItem.FindControl("txtId_PrdAde") as RadNumericTextBox).Value); 
                Prd_Descripcion = (insertedItem["Id_Prd"].FindControl("cmbProductoAde") as RadComboBox).Text;
               
                if (Id_Prd == 0)
                {
                    Alerta("Elija un Producto Para poder Guardar");
                    e.Canceled = true;
                    return;
                }


                ArrayList al = new ArrayList();
                al.Add(Id_Cons_AdeDet); 
                al.Add(Id_Prd);
                al.Add(Prd_Descripcion);

                bool falta_adenda = false;
                TextBox Txtadenda = new TextBox();
                string valor_adenda = "";                
                ArrayList ok = new ArrayList();

                string adenda_faltante = "";
                foreach (AdendaDet det in ListDet)
                {
                    if (!ok.Contains(det.Id_AdeDet.ToString()))
                    {
                        ok.Add(det.Id_AdeDet.ToString());
                        Txtadenda = insertedItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox;
                        valor_adenda = Txtadenda.Text.Replace("'", "");                       

                        if (valor_adenda == "" && det.Requerido)
                        {
                            adenda_faltante = det.Campo;
                            falta_adenda = true;
                            break;
                        }
                        else
                        {
                            al.Add(valor_adenda);
                            
                        }
                    }
                }

                if (ListDetRF != null)
                {
                    foreach (AdendaDet det in ListDetRF)
                    {
                        if (!ok.Contains(det.Id_AdeDet.ToString()))
                        {
                            ok.Add(det.Id_AdeDet.ToString());
                            Txtadenda = insertedItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox;
                            valor_adenda = Txtadenda.Text;

                            if (valor_adenda == "" && det.Requerido)
                            {
                                adenda_faltante = det.Campo;
                                falta_adenda = true;
                                break;
                            }
                            else
                            {
                                al.Add(valor_adenda);
                            }
                        }
                    }
                }

                if (falta_adenda)
                {
                    AlertaFocus("El campo <b>" + adenda_faltante + "</b> de la addenda es requerido", Txtadenda.ClientID);
                    e.Canceled = true;
                }
                else
                {                    
                    ListaProductosFacturaAdenda.Rows.Add(al.ToArray());
                }
            }
            catch (Exception ex)
            {

                e.Canceled = true;               
                Alerta(ex.Message);
                return;
                
            }
        }
        protected void rgFacturaDetAde_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                string Id_Cons_AdeDet = "";
                int Id_Prd = 0;
                string Prd_Descripcion = "";


                Id_Cons_AdeDet = (insertedItem.FindControl("txtId_Cons_AdeDet") as RadTextBox).Text; 
                Id_Prd = Convert.ToInt32((insertedItem.FindControl("txtId_PrdAde") as RadNumericTextBox).Value);
                Prd_Descripcion = (insertedItem["Id_Prd"].FindControl("cmbProductoAde") as RadComboBox).Text;

               
                    DataRow[] Ar_dr;
                    //Ar_dr = ListaProductosFacturaAdenda.Select("Id_Prd='" + Id_Prd + "'");
                    Ar_dr = ListaProductosFacturaAdenda.Select("Id_Cons_AdeDet='" + Id_Cons_AdeDet + "'");
                    if (Ar_dr.Length > 0)
                    {
                        Ar_dr[0].BeginEdit();
                        Ar_dr[0]["Id_Cons_AdeDet"] = Id_Cons_AdeDet;          
                        Ar_dr[0]["Id_Prd"] = Id_Prd;
                        Ar_dr[0]["Prd_Descripcion"] = Prd_Descripcion;
                

                        bool falta_adenda = false;
                        string valor_adenda = "";
                        TextBox Txtadenda = new TextBox();
                        //ADENDA FACTURACION
                        ArrayList ok = new ArrayList();
                        string adenda_faltante = "";

                        foreach (AdendaDet det in ListDet)
                        {
                            if (!ok.Contains(det.Id_AdeDet.ToString()))
                            {
                                ok.Add(det.Id_AdeDet.ToString());
                                Txtadenda = insertedItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox;
                                valor_adenda = Txtadenda.Text.Replace("'", "");
                                bool addenda_Requerida = det.Requerido;//listDetT.Where(AdendaDet => AdendaDet.Id_AdeDet == det.Id_AdeDet).ToList()[0].Requerido;
                                if (valor_adenda == "" && addenda_Requerida)
                                {
                                    adenda_faltante = det.Campo;
                                    falta_adenda = true;
                                    (insertedItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox).Focus();
                                    break;
                                }
                                else
                                    Ar_dr[0][det.Id_AdeDet.ToString()] = valor_adenda;
                            }
                        }

                        //ADENDA REFACTURACION
                        if (ListDetRF != null && !falta_adenda)
                        {
                            foreach (AdendaDet det in ListDetRF)
                            {
                                if (!ok.Contains(det.Id_AdeDet.ToString()))
                                {
                                    ok.Add(det.Id_AdeDet.ToString());
                                    Txtadenda = insertedItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox;
                                    bool addenda_Requerida = det.Requerido;//listDetT.Where(AdendaDet => AdendaDet.Id_AdeDet == det.Id_AdeDet).ToList()[0].Requerido;
                                    valor_adenda = Txtadenda.Text;
                                    if (valor_adenda == "" && addenda_Requerida)
                                    {
                                        adenda_faltante = det.Campo;
                                        falta_adenda = true;
                                        (insertedItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox).Focus();
                                        break;
                                    }
                                    else
                                        Ar_dr[0][det.Id_AdeDet.ToString()] = valor_adenda;
                                }
                            }
                        }
                        if (falta_adenda)
                        {
                            AlertaFocus("El campo <b>" + adenda_faltante + "</b> de la addenda es requerido", Txtadenda.ClientID);
                            e.Canceled = true;
                        }
                        else
                        Ar_dr[0].AcceptChanges();
                    }                                                   
            }
            catch (Exception ex)
            {
                e.Canceled = true;               
                Alerta(ex.Message.Replace("'", ""));
                return;
               
            }
        }
        protected void rgFacturaDetAde_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();

                if (rgFacturaDetAde.EditItems.Count > 0)
                {
                    Alerta("Ya está editando un registro");
                    e.Canceled = true;
                }
                else
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    string Id_Cons_AdeDet = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Cons_AdeDet"]);                 
                    //int Id_Prd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]);                    
                    //actualizar producto de orden de compra a la lista
                    this.ListaProductosFacturaAdenda_EliminarProducto(Id_Cons_AdeDet);                                                           
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgFacturaDetAde_ItemCreated(object sender, GridItemEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.Item is GridDataItem)
                {
                    GridDataItem editItem = (GridDataItem)e.Item;
                    TextBox txt;
                    try
                    {
                        if (ListDet != null)
                        {
                            foreach (AdendaDet det in ListDet)
                            {
                                if (editItem[det.Id_AdeDet.ToString()] != null)
                                {
                                    if (editItem[det.Id_AdeDet.ToString()].Controls.Count > 0)
                                    {
                                        txt = editItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox;
                                        txt.Attributes.Add("onkeypress", "return SoloAlfanumerico(this)");
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                    try
                    {
                        if (ListDetRF != null)
                        {
                            foreach (AdendaDet det in ListDetRF)
                            {
                                if (editItem[det.Id_AdeDet.ToString()] != null)
                                {
                                    if (editItem[det.Id_AdeDet.ToString()].Controls.Count > 0)
                                    {
                                        txt = editItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox;
                                        txt.Attributes.Add("onkeypress", "return SoloAlfanumerico(this)");
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        //rgFacturaDetAde rgFacturaDetAde 
        //rgFacturaDetAde rgFacturaDetAde 

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            int prd = 0;
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                RadNumericTextBox Txtcantidad = (sender as RadNumericTextBox);
                int cantidad = (Txtcantidad.Parent.Parent.FindControl("txtFac_Cantidad") as RadNumericTextBox).Value.HasValue ? Convert.ToInt32((Txtcantidad.Parent.Parent.FindControl("txtFac_Cantidad") as RadNumericTextBox).Value.Value) : 0;
                prd = (Txtcantidad.Parent.Parent.FindControl("txtId_Prd") as RadNumericTextBox).Value.HasValue ? Convert.ToInt32((Txtcantidad.Parent.Parent.FindControl("txtId_Prd") as RadNumericTextBox).Value.Value) : 0;
                int ter = (Txtcantidad.Parent.Parent.FindControl("txtTerritorioPartida") as RadNumericTextBox).Value.HasValue ? Convert.ToInt32((Txtcantidad.Parent.Parent.FindControl("txtTerritorioPartida") as RadNumericTextBox).Value.Value) : 0;

                int disponible_pedido = 0;
                #region pedido
                if (txtPedido.Text != "")
                {
                    CN_CapPedido cappedido = new CN_CapPedido();
                    Pedido pedido = new Pedido();
                    pedido.Id_Emp = sesion.Id_Emp;
                    pedido.Id_Cd = sesion.Id_Cd_Ver;
                    pedido.Id_Ped = Convert.ToInt32(txtPedido.Text);

                    DataTable dt2 = new DataTable();
                    dt2.Columns.Add("Id_PedDet");
                    dt2.Columns.Add("Id_Ter");
                    dt2.Columns.Add("Ter_Nombre");
                    dt2.Columns.Add("Id_Prd");
                    dt2.Columns.Add("Prd_Descripcion");
                    dt2.Columns.Add("Prd_Presentacion");
                    dt2.Columns.Add("Prd_Unidad");
                    dt2.Columns.Add("Prd_Precio");
                    dt2.Columns.Add("Disponible");
                    dt2.Columns.Add("Prd_Importe");
                    dt2.Columns.Add("Id_Rem");
                    //cappedido.ConsultaPedidoDetDisp(pedido, ref dt2, 1, sesion.Emp_Cnx);

                    //DataRow[] dr = dt2.Select("Id_Prd='" + prd + "'");//"Id_Ter='" + ter + "' and 

                    //if (dr.Length > 0)
                    //{
                    //    for (int i = 0; i < dr.Length; i++)
                    //        disponible_pedido += !string.IsNullOrEmpty(dr[i]["Disponible"].ToString()) ? Convert.ToInt32(dr[i]["Disponible"]) : 0;
                    //}

                    cappedido.ConsultaPedidoDisp(pedido, prd, sesion.Emp_Cnx, ref disponible_pedido);

                    if (disponible_pedido < 0)
                        disponible_pedido = 0;
                }
                #endregion
                cantidad_A = Convert.ToInt32(Session["CantidadEditar" + Session.SessionID]);
                Id_Rem_A = Convert.ToInt32(Session["Remision" + Session.SessionID]);

                CN_CatProducto cn_producto = new CN_CatProducto();
                List<int> actuales = new List<int>();
                cn_producto.ConsultaProducto_Disponible(sesion.Id_Emp, sesion.Id_Cd_Ver, prd.ToString(), ref actuales, sesion.Emp_Cnx);

                int cantidad_B = 0;
                DataRow[] Dr2 = ListaProductosFactura.Select("Id_Prd='" + prd + "' and Id_Ter <> '" + ter + "'");
                if (Dr2.Length > 0)
                {
                    for (int i = 0; i < Dr2.Length; i++)
                        cantidad_B += !string.IsNullOrEmpty(Dr2[i]["Fac_Cant"].ToString()) ? Convert.ToInt32(Dr2[i]["Fac_Cant"]) : 0;
                }

                int cantRemision = 0;
                List<Remision> listaRemisiones;
                CN_CapRemision remision = new CN_CapRemision();
                listaRemisiones = new List<Remision>();
                remision.ConsultaRemisionesxFactura(sesion, Convert.ToInt32(txtId.Text), ref listaRemisiones);
                if (listaRemisiones.Count != 0)
                {
                    CN_CapFactura cn_fact = new CN_CapFactura();
                    int disponibleFacturar = 0;
                    Factura fac2 = new Factura();
                    fac2.Id_Fac = (int)txtId.Value;
                    fac2.Id_Rem = Id_Rem_A;
                    cn_fact.DisponibleFacturar(sesion, fac2, prd, ref disponibleFacturar);
                    cantRemision += disponibleFacturar;
                }
                else
                    if (Session["ListaRemisionesFactura" + Session.SessionID] != null)
                    {

                        listaRemisiones = (List<Remision>)Session["ListaRemisionesFactura" + Session.SessionID];
                        foreach (Remision rem in listaRemisiones)
                        {
                            arrayRemisiones = string.Concat(arrayRemisiones, rem.Id_Rem.ToString(), "|");
                        }
                        if (arrayRemisiones.Length > 1)
                            arrayRemisiones = arrayRemisiones.Substring(0, arrayRemisiones.Length - 1);

                        CN_CapRemision cr = new CN_CapRemision();
                        cantRemision = cr.ConsultaCantidadRemision(sesion, prd, arrayRemisiones);
                        if (cantRemision == 0)
                        {
                            CN_CapFactura cn_factura = new CN_CapFactura();
                            Factura fac = new Factura();
                            fac.Id_Emp = sesion.Id_Emp;
                            fac.Id_Cd = sesion.Id_Cd_Ver;
                            fac.Id_Fac = (int)txtId.Value;
                            fac.Id_FacSerie = this.cmbConsFacEle.Text + txtId.Text;
                            List<FacturaDet> list = new List<FacturaDet>();
                            cn_factura.ConsultaFactura(ref fac, ref list, sesion.Emp_Cnx);
                            int count = 0;
                            foreach (FacturaDet f in list)
                            {
                                if (f.Id_Prd == prd)
                                {
                                    count += f.Fac_Cant;
                                }
                            }
                            cantRemision = count - cantidad_A;
                        }
                    }
                    else
                    {
                        int fac1 = Convert.ToInt32(Page.Request.QueryString["Id_Fac"]);
                        if (fac1 != -1)
                        {
                            CN_CapFactura cn_factura = new CN_CapFactura();
                            Factura fac = new Factura();
                            fac.Id_Emp = sesion.Id_Emp;
                            fac.Id_Cd = sesion.Id_Cd_Ver;
                            fac.Id_Fac = (int)txtId.Value;
                            fac.Id_FacSerie = this.cmbConsFacEle.Text + txtId.Text;
                            List<FacturaDet> list = new List<FacturaDet>();
                            cn_factura.ConsultaFactura(ref fac, ref list, sesion.Emp_Cnx);
                            int count = 0;
                            foreach (FacturaDet f in list)
                            {
                                if (f.Id_Prd == prd)
                                {
                                    count += f.Fac_Cant;
                                }
                            }
                            cantRemision = count;
                        }
                    }
                if (Convert.ToInt32(txtMov.Text) != 70)
                {
                    if (cantidad + cantidad_B > actuales[2] + disponible_pedido + cantRemision)//cantidad_A)
                    {
                        this.AlertaFocus(string.Concat("Producto "
                                , prd.ToString()
                                , ", inventario disponible insuficiente.<br/>Inventario final: "
                                , actuales[0].ToString()
                                , "<br/>Asignado: "
                                , actuales[1].ToString()
                                , "<br/>Disponible: "
                                , (actuales[2] + disponible_pedido + cantRemision).ToString()), Txtcantidad.ClientID);//cantidad_A)
                        Txtcantidad.Text = "";
                        return;
                    }
                    else
                    {
                        (Txtcantidad.Parent.Parent.FindControl("txtFac_Precio") as RadNumericTextBox).Focus();
                    }
                }
                else
                {
                    int cte_ext = (Txtcantidad.Parent.Parent.FindControl("txtClienteExterno") as RadNumericTextBox).Value.HasValue ? Convert.ToInt32((Txtcantidad.Parent.Parent.FindControl("txtClienteExterno") as RadNumericTextBox).Value.Value) : 0;


                    CN_CapEntradaSalida cn_entsal = new CN_CapEntradaSalida();
                    int verificador = 0;
                    int disponible = 0;
                    cn_entsal.ConsultarSaldo(sesion.Id_Emp, sesion.Id_Cd_Ver, prd.ToString(), ter.ToString(), cte_ext.ToString(), sesion.Emp_Cnx, ref verificador, "14");
                    disponible = verificador;
                    verificador = 0;
                    cn_entsal.ConsultarSaldo(sesion.Id_Emp, sesion.Id_Cd_Ver, prd.ToString(), ter.ToString(), cte_ext.ToString(), sesion.Emp_Cnx, ref verificador, "");
                    disponible += verificador;

                    if (cantidad + cantidad_B > disponible)//cantidad_A)
                    {
                        this.AlertaFocus(string.Concat("Producto "
                                , prd.ToString()
                                , ", Saldo disponible: "
                                , (disponible).ToString()), Txtcantidad.ClientID);//cantidad_A)
                        Txtcantidad.Text = "";
                        return;
                    }
                    else
                    {
                        (Txtcantidad.Parent.Parent.FindControl("txtFac_Precio") as RadNumericTextBox).Focus();
                    }
                }
                string mensajeInventarioExcepcion = string.Empty;

                //validar cantidad de producto de partida contra cantidad de remisión si es que la factura es de remisiones               
                if (Id_Rem_A != 0) //es facturacion de remision
                {
                    if (cantidad > actuales[2] + disponible_pedido + Rem_Cant_A + cantRemision)// Rem_Cant_A)
                        mensajeInventarioExcepcion = "rgFacturaDet_InvFinalRemisionInsuficiente";
                }
                if (mensajeInventarioExcepcion != string.Empty)
                    throw new Exception(mensajeInventarioExcepcion);

                if (txtMov.Text == "51" && actuales[2] + disponible_pedido + Rem_Cant_A/*cantidad_A*/  + cantRemision > cantidad)
                {
                    int agrupado = -1;
                    int CantT = 0;

                    Producto prod = new Producto();
                    prod.Id_Cte = Convert.ToInt32(txtCliente.Value);
                    cn_producto.ConsultaProducto(ref prod, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, prd);
                    agrupado = prod.Prd_AgrupadoSpo;

                    if (agrupado != -1)
                    {
                        foreach (DataRow dr in ListaProductosFactura.Rows)
                        {
                            prod = new Producto();
                            cn_producto.ConsultaProducto(ref prod, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(dr["Id_Prd"]));

                            if (prod.Prd_AgrupadoSpo == agrupado)
                                CantT += Convert.ToInt32(dr["Fac_Cant"]);
                        }
                    }
                    CantT -= cantidad_A;
                    CantT += cantidad;
                    CN_CapFactura cn_factura = new CN_CapFactura();
                    Factura fac = new Factura();
                    fac.Id_Emp = sesion.Id_Emp;
                    fac.Id_Cd = sesion.Id_Cd_Ver;
                    fac.Id_Fac = (int)txtId.Value;
                    fac.Id_FacSerie = this.cmbConsFacEle.Text + txtId.Text;
                    List<FacturaDet> list = new List<FacturaDet>();
                    cn_factura.ConsultaFactura(ref fac, ref list, sesion.Emp_Cnx);

                    int CantOriginal = 0;
                    foreach (FacturaDet fd in list)
                    {
                        if (fd.Prd_Agrupador == agrupado)
                            CantOriginal += fd.Fac_Cant;
                    }
                    CN_CapEntradaSalida CNentrada = new CN_CapEntradaSalida();
                    int saldo = 0;
                    CNentrada.ConsultarSaldo(sesion.Id_Emp, sesion.Id_Cd_Ver, prd.ToString(), ter.ToString(), txtCliente.Text, sesion.Emp_Cnx, ref saldo, "14");
                    /*
                    if (saldo - (CantOriginal - CantT) < 0)
                    {
                        AlertaFocus("El producto " + prd.ToString() + " no cuenta con el saldo suficiente", Txtcantidad.ClientID);
                        Txtcantidad.Text = "";
                        return;
                    }*/
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("rgFacturaDet_InvFinalRemisionInsuficiente"))
                {
                    this.Alerta(string.Concat("Producto "
                    , prd.ToString()
                    , ", inventario disponible insuficiente.<br/>Remisionado: ", Rem_Cant_A.ToString()
                    , "<br/>Disponible: ", Rem_Cant_A.ToString()));
                    return;
                }
                else
                    ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                if (this.ConsultarDatosCliente(txtCliente.Text, false))
                {
                    CargarComboTerritorios();
                    rgFacturaDet.Rebind();
                    rgFacturaDetAde.Rebind();
                    rgAdendaFacturacion.Rebind();
                    Consultar_IVA_Cliente();
                    
                    txtTerritorio.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtDescuento1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                this.CalcularTotales();
                txtDescPorc1.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtDescuento2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                this.CalcularTotales();
                txtDescPorc2.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtProducto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                RadNumericTextBox combo = (RadNumericTextBox)sender;
                Telerik.Web.UI.GridTableCell tabla = (Telerik.Web.UI.GridTableCell)combo.Parent;
                RadNumericTextBox txtFac_Territorio = (RadNumericTextBox)tabla.FindControl("txtTerritorioPartida");
                RadTextBox txtPrdDescripcion = (RadTextBox)tabla.FindControl("txtProductoNombre");
                RadTextBox txtPrd_Descripcion = (RadTextBox)tabla.FindControl("txtPrd_Descripcion");
                RadTextBox txtPrd_Presentacion = (RadTextBox)tabla.FindControl("txtPrd_Presentacion");
                RadTextBox txtPrd_UniNe = (RadTextBox)tabla.FindControl("txtPrd_UniNe");
                RadNumericTextBox txtFac_Cantidad = (RadNumericTextBox)tabla.FindControl("txtFac_Cantidad");
                RadNumericTextBox txtFac_Precio = (RadNumericTextBox)tabla.FindControl("txtFac_Precio");

                if (ListaProductosFactura.Select("Id_Prd='" + combo.Value.ToString() + "' and Id_Ter='" + txtFac_Territorio.Value.ToString() + "'").Length > 0)
                {
                    AlertaFocus("El producto ya ha sido agregado a la factura", combo.ClientID);
                    combo.Text = "";
                    txtFac_Territorio.Text = "";
                    txtPrdDescripcion.Text = "";
                    txtPrd_Descripcion.Text = "";
                    txtPrd_Presentacion.Text = "";
                    txtPrd_UniNe.Text = "";
                    txtFac_Cantidad.Text = "";
                    txtFac_Precio.Text = "";
                    return;
                }

                //obtiene la tabla contenedora de los controles de edición de registro del Grid
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int id_Cd_Prod = sesion.Id_Cd_Ver;

                Producto producto = new Producto();
                if (combo.Value.HasValue)
                {
                    //esta variable guardará el precio de producto aceptado para la partida
                    //puede ser el precio publico del catalogo de producto
                    //o el precio publico del catalogo de cliente-producto
                    //o el precio AAA de una solicitud de precios especiales
                    double precioProductoAceptado = 0;

                    //obtener datos de producto
                    CN_CatProducto clsProducto = new CN_CatProducto();
                    producto.Id_Ter = Convert.ToInt32(txtFac_Territorio.Value.HasValue ? txtFac_Territorio.Value : -1);
                    try
                    {
                        producto.Id_Cte = Convert.ToInt32(txtCliente.Value);
                        producto.EmpBen = strEmp == "" ? (int?)null : Convert.ToInt32(strEmp);
                        clsProducto.ConsultaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, id_Cd_Prod, sesion.Id_Cd_Ver, Convert.ToInt32(combo.Value));
                    }
                    catch (Exception ex)
                    {
                        combo.Text = "";
                        txtPrdDescripcion.Text = "";
                        txtPrd_Descripcion.Text = "";
                        txtPrd_Presentacion.Text = "";
                        txtPrd_UniNe.Text = "";
                        txtFac_Cantidad.Text = "";
                        txtFac_Precio.Text = "";
                        AlertaFocus(ex.Message, combo.ClientID);
                        return;
                    }
                    //obtener precio de producto
                    float precioPublico = 0;
                    new CN_ProductoPrecios().ConsultaListaProductoPrecioPUBLICO(ref precioPublico, sesion, Convert.ToInt32(combo.Value));

                    //obtener precio especial de producto
                    //desde el catálogo CAT_CLIENTEPRODUCTO
                    float precioPublicoCAT_CLIENTEPRODUCTO = 0;
                    ClienteProd clienteProd = new ClienteProd();
                    clienteProd.Id_Emp = sesion.Id_Emp;
                    clienteProd.Id_Cd = sesion.Id_Cd_Ver;
                    clienteProd.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1);
                    clienteProd.Id_Prd = Convert.ToInt32(combo.Value);
                    new CN_CatClienteProd().ClienteProductoPrecioPublico_Consultar(ref clienteProd, sesion.Emp_Cnx, ref precioPublicoCAT_CLIENTEPRODUCTO);

                    precioProductoAceptado = precioPublicoCAT_CLIENTEPRODUCTO > 0 ? precioPublicoCAT_CLIENTEPRODUCTO : precioPublico;

                    //obtener SOLICITUDES DE PRECIOS ESPECIALES vencidas
                    List<VentanaPrecioEspecialPro> listaPrecioEspecial = new List<VentanaPrecioEspecialPro>();
                    new CN_PrecioEspecial().PrecioEspecialSolicitudesVencidas_Consulta(ref listaPrecioEspecial, sesion.Emp_Cnx, sesion.Id_Emp
                        , sesion.Id_Cd_Ver
                        , Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1)
                        , Convert.ToInt32(combo.Value)
                        /*, Convert.ToInt32(cmbMoneda.SelectedValue)*/);

                    //obtener precio especial del producto 
                    //para el cliente actual de la factura
                    //desde la CAPTURA de SOLICITUDES DE PRECIOS ESPECIALES
                    VentanaPrecioEspecialPro precioEspecialPro = null;
                    new CN_PrecioEspecial().PrecioEspecialProductoCliente_Consulta(ref precioEspecialPro, sesion.Emp_Cnx, sesion.Id_Emp
                        , sesion.Id_Cd_Ver
                        , Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1)
                        , Convert.ToInt32(combo.Value)
                        /* ,Convert.ToInt32(cmbMoneda.SelectedValue) */);

                    if (precioEspecialPro != null && precioEspecialPro.Ape_PreEsp > 0)
                    { //mensaje de vencimiento de solicitud de precio especial
                        string mensajePreciEspecialVencimiento = string.Concat("Faltan solo "
                            , ((TimeSpan)precioEspecialPro.Ape_FecFin.Subtract(precioEspecialPro.Ape_FecInicio)).Days.ToString()
                            , " día(s) para que venzan producto(s) con precio especial de la solicitud "
                            , precioEspecialPro.Id_Ape.ToString()
                            , " de precios especiales.<br/><br/>");
                        if (listaPrecioEspecial.Count > 0)
                        {
                            for (int i = 0; i < listaPrecioEspecial.Count; i++)
                            {
                                mensajePreciEspecialVencimiento = string.Concat(mensajePreciEspecialVencimiento
                                , "La solicitud de precios especiales ", listaPrecioEspecial[i].Id_Ape.ToString()
                                , " tiene productos con "
                                , ((TimeSpan)new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).Subtract(precioEspecialPro.Ape_FecInicio)).Days.ToString()
                                , " días vencidos.<br/>");
                            }
                        }
                        mensajePreciEspecialVencimiento = string.Concat(mensajePreciEspecialVencimiento
                            , "<br/>Los productos sin actualizar el Precio AAA Especial, impactan directamente en los cálculos de utilidad del CDI"
                            , " y por ende, en los sistemas de compensación de todo el personal.");

                        //validar precio de venta (de catClienteProducto) es diferente al precio especial de la solicitud de precios especiales
                        //se toma como precio aceptado al precio de catClienteProducto y  se manda mensaje
                        if (precioProductoAceptado != precioEspecialPro.Ape_PreVta)
                        {
                            mensajePreciEspecialVencimiento = string.Concat(mensajePreciEspecialVencimiento
                                , "<br/><br/>El precio especial autorizado para este producto en la solicitud "
                                , precioEspecialPro.Id_Ape
                                , " no se tomará en cuenta, ya que el precio de venta no es el mismo al convenido que es de "
                                , precioEspecialPro.Ape_PreVta);
                        }
                        else
                        {   /*
                             * NOTA: si el precio está en dólares u otro tipo de moneda, 
                             * se hace la conversión al tipo de moneda de la factura que se está capturando.
                             */
                            if (precioEspecialPro.Id_Mon != Convert.ToInt32(cmbMoneda.SelectedValue))
                            { //Consultar tipo de cambio
                                TipoMoneda tipoMoneda = new TipoMoneda();
                                List<TipoMoneda> listaTipoMoneda = new List<TipoMoneda>();
                                double tipoCambioFactura = 0, tipoCambioPrecioEspecial = 0;
                                new CN_CatTipoMoneda().ConsultaTipoMoneda(tipoMoneda, sesion.Id_Emp, sesion.Emp_Cnx, ref listaTipoMoneda);
                                foreach (TipoMoneda tm in listaTipoMoneda)
                                {
                                    if (tm.Id_Mon == Convert.ToInt32(cmbMoneda.SelectedValue))
                                        tipoCambioFactura = tm.Mon_TipCambio;
                                    if (tm.Id_Mon == precioEspecialPro.Id_Mon)
                                        tipoCambioPrecioEspecial = tm.Mon_TipCambio;
                                }
                                precioProductoAceptado = (precioEspecialPro.Ape_PreEsp * tipoCambioPrecioEspecial) / tipoCambioFactura;
                            }
                            else
                                precioProductoAceptado = precioEspecialPro.Ape_PreEsp;
                        }
                        this.AlertaFocus2(mensajePreciEspecialVencimiento, txtFac_Cantidad.ClientID);
                    }
                    //Finalmente introduce el precio de producto aceptado para la partida
                    txtFac_Precio.Text = precioProductoAceptado.ToString();
                }

                txtPrdDescripcion.Text = producto == null ? string.Empty : producto.Prd_Descripcion;
                txtPrd_Descripcion.Text = producto == null ? string.Empty : producto.Prd_Descripcion;
                txtPrd_Presentacion.Text = producto == null ? string.Empty : producto.Prd_Presentacion;
                txtPrd_UniNe.Text = producto == null ? string.Empty : producto.Prd_UniNe;
                //--------controles auxiliares--------
                //establecer unidades de empaque
                HD_Prd_UniEmp.Value = producto == null ? string.Empty : producto.Prd_UniEmp.ToString();
                HD_Prd_InvFinal.Value = producto == null ? string.Empty : producto.Prd_InvFinal.ToString();
                HD_Prd_Asignado.Value = producto == null ? string.Empty : producto.Prd_Asignado.ToString();
                HD_Prd_Disponible.Value = producto == null ? string.Empty : (producto.Prd_InvFinal - producto.Prd_Asignado).ToString();
                //    HD_Prd_Disponible.Value = producto == null ? string.Empty : ((producto.Prd_InvFinal - producto.Prd_Asignado) + producto.Prd_InvFinal).ToString();

                //este evento es porque se elige producto, por lo que 
                //se habilita el campo de cantidad porque ya eligió producto y se estableció las unidades de empaque
                txtFac_Cantidad.Enabled = true;
                txtFac_Cantidad.Text = string.Empty;
                if (combo.Value.HasValue)
                    txtFac_Cantidad.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtTerritorio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                RadNumericTextBox combo = (RadNumericTextBox)sender;
                if (txtMov.Text == "70")
                {
                    if (ListaProductosFactura.Select("Id_Ter<>'" + Convert.ToInt32(combo.Value.ToString()) + "'").Length > 0)
                    {
                        AlertaFocus("El territorio no puede ser diferente al ya capturado en el detalle", combo.ClientID);
                        combo.Text = "";
                        ((RadComboBox)combo.Parent.FindControl("cmbTerritorioPartida")).SelectedIndex = 0;
                        return;
                    }

                }
                RadNumericTextBox txtFac_Territorio = (RadNumericTextBox)combo.Parent.FindControl("txtId_Prd");
                if (combo.Value.HasValue)
                    txtFac_Territorio.Focus();
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtClienteExterno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();

                RadNumericTextBox combo = (RadNumericTextBox)sender;
                Telerik.Web.UI.GridTableCell tabla = (Telerik.Web.UI.GridTableCell)combo.Parent;

                RadTextBox nombre = combo.Parent.FindControl("txtNombreCliente") as RadTextBox;

                if (ListaProductosFactura.Select("Id_CteExt<>'" + Convert.ToInt32(combo.Value.ToString()) + "'").Length > 0)
                {
                    AlertaFocus("El cliente externo no puede ser diferente al ya capturado en el detalle", combo.ClientID);
                    combo.Text = "";
                    nombre.Text = "";
                    return;
                }

                if (combo.Value.ToString() == txtCliente.Text)
                {
                    AlertaFocus("El cliente externo no puede ser igual al capturado en los datos generales", combo.ClientID);
                    combo.Text = "";
                    nombre.Text = "";
                    return;
                }
                //obtiene la tabla contenedora de los controles de edición de registro del Grid
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Clientes cliente = new Clientes();
                if (combo.Value.HasValue)
                {
                    cliente.Id_Emp = sesion.Id_Emp;
                    cliente.Id_Cd = sesion.Id_Cd_Ver;
                    cliente.Id_Rik = sesion.Id_Rik;
                    cliente.Id_Cte = Convert.ToInt32(combo.Value.ToString());
                    try
                    {
                        new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);
                    }
                    catch (Exception ex)
                    {
                        AlertaFocus(ex.Message, combo.ClientID);
                        combo.Text = "";
                        return;
                    }

                    RadTextBox txtNombreCliente = (RadTextBox)tabla.FindControl("txtNombreCliente");

                    txtNombreCliente.Text = cliente.Cte_NomComercial;
                    RadNumericTextBox txtFac_Territorio = (RadNumericTextBox)tabla.FindControl("txtTerritorioPartida");
                    if (combo.Value.HasValue)
                        txtFac_Territorio.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void chkDesgloceIva_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                IVA.Visible = chkDesgloce.Checked;

                if (chkDesgloce.Checked)
                {
                    Consultar_IVA_Cliente();
                }
                else
                {
                    HD_IVAfacturacion.Value = "0";
                    txtIVA.Text = "0";
                }
                CalcularTotales();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void Consultar_IVA_Cliente()
        {
            string IVA_Cliente = "NO";
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (txtCliente.Text != string.Empty && txtCliente.Text != "-1")
            {
                Clientes cliente = new Clientes();
                cliente.Id_Emp = sesion.Id_Emp;
                cliente.Id_Cd = sesion.Id_Cd_Ver;
                cliente.Id_Rik = sesion.Id_Rik;
                cliente.Id_Cte = Convert.ToInt32(txtCliente.Text);
                new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);

                if (cliente.BPorcientoIVA == true)
                {
                    if (cliente.PorcientoIVA == 0 || cliente.PorcientoIVA == null)
                    {
                        Alerta("El porcentaje de IVA no está establecido, debe ser Mayor a Cero");
                        return;
                    }
                    else
                    {
                        HD_IVAfacturacion.Value = cliente.PorcientoIVA.ToString();
                        IVA_Cliente = "SI";
                    }
                }
            }

            if (IVA_Cliente == "NO")
            {
                // Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CentroDistribucion cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                HD_IVAfacturacion.Value = cd.Cd_IvaPedidosFacturacion.ToString();
            }
        }
        protected void consultarRetencion()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (txtCliente.Text != string.Empty && txtCliente.Text != "-1")
            { //Consultar clientes
                Clientes cliente = new Clientes();
                cliente.Id_Emp = sesion.Id_Emp;
                cliente.Id_Cd = sesion.Id_Cd_Ver;
                cliente.Id_Rik = sesion.Id_Rik;
                cliente.Id_Cte = Convert.ToInt32(txtCliente.Text);
                try
                {
                    new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);
                    txtPorcRetencion.Text = cliente.PorcientoRetencion.ToString();

                    if (cliente.PorcientoRetencion == 0 || cliente.PorcientoRetencion == null)
                    {
                        Alerta("El porcentaje de Retencion no está establecido, no se guardará el importe de Retención");
                        chkRetencion.Checked = false;
                        txtPorcRetencion.Visible = false;
                    }
                    else
                    {
                        txtPorcRetencion.Visible = true;
                        txtPorcRetencion.Text = cliente.PorcientoRetencion.ToString();
                    }
                }
                catch (Exception ex)
                {
                    ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                }
            }
        }
        protected void chkRetencion_CheckedChanged(object sender, EventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (txtCliente.Text != string.Empty && txtCliente.Text != "-1")
            {
                Clientes cliente = new Clientes();
                cliente.Id_Emp = sesion.Id_Emp;
                cliente.Id_Cd = sesion.Id_Cd_Ver;
                cliente.Id_Rik = sesion.Id_Rik;
                cliente.Id_Cte = Convert.ToInt32(txtCliente.Text);
                try
                {
                    new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);
                    txtPorcRetencion.Text = cliente.PorcientoRetencion.ToString();
                    if (chkRetencion.Checked == true)
                    {
                        if (cliente.PorcientoRetencion == 0 || cliente.PorcientoRetencion == null)
                        {
                            Alerta("El porcentaje de Retencion no está establecido, debe ser Mayor a Cero");
                            chkRetencion.Checked = false;
                            txtPorcRetencion.Visible = false;
                        }
                        else
                        {
                            consultarRetencion();
                        }
                    }
                    else
                    {
                        txtPorcRetencion.Visible = false;
                    }
                }
                catch (Exception ex)
                {

                    ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                }
            }
        }
        protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RAM1.ResponseScripts.Add("popup();");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtProducto_Load(object sender, EventArgs e)
        {
            producto = sender;
        }
        #endregion
        #region Funciones
        private void Inicializar(int Id_Emp, int Id_Cd, int Id_Fac,string Id_FacSerie, string facModificable)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Session["FacEspecialGuardada" + Session.SessionID] = "0";
                Session["ListaProductosFacturaEspecial" + Session.SessionID] = null;

                InicializarTablaProductos();
                chkDesgloce.Attributes.Add("onfocus", "return _ValidarFechaEnPeriodo()");
                chkRetencion.Attributes.Add("onfocus", "return _ValidarFechaEnPeriodo()");
                Consultar_IVA_Cliente();
                CentroDistribucion cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                //HD_IVAfacturacion.Value   = cd.Cd_IvaPedidosFacturacion.ToString();
                HF_VI.Value = "false";

                //llenar combos
                this.CargarConsFactElectronica();
                this.CargarComboTipoMovimientos();
                this.CargarComboTerritorios();
                this.CargarComboTipoModeda();
                this.HabilitaBotonesToolBar(false, true, false, false, false, false);

                //Variable de sesion para los productos con amortizaciones de un cliente
                Session["ListaAmortizaciones" + Session.SessionID] = new List<Amortizacion>();

                //establece valores de controles de inicio
                if (Id_Emp > 0 && Id_Cd > 0 && Id_Fac > 0)
                {
                    this.hiddenId.Value = Id_Fac.ToString();
                    this.HdId_FacSerie.Value = Id_FacSerie;
                    this.LLenarFormFactura(Id_Emp, Id_Cd, Id_Fac, Id_FacSerie); //EDICION de factura
                    this.rgFacturaDet.Enabled = true;
                    //rgFacturaDetAde
                    this.rgFacturaDetAde.Enabled = true;
                    this.rgAdendaFacturacion.Enabled = true;
                    
                    //rgFacturaDetAde
                    this.btnFacturaEspecial.Enabled = false;

                    if (facModificable == "0")
                        this.HabilitaBotonesToolBar(false, false, false, false, false, false);
                    if (_reFactura == "1")
                    {
                        this.HabilitaBotonesToolBar(false, true, false, false, false, false);
                        this.txtFecha.SelectedDate = DateTime.Now;

                    }
                    this.txtFecha.Focus();
                }
                else //FACTURA Nueva
                {
                    this.hiddenId.Value = string.Empty;
                    this.txtFecha.SelectedDate = DateTime.Now;

                    if (cmbConsFacEle.Items.Count > 1)
                    {
                        cmbConsFacEle.SelectedIndex = 1;
                        cmbConsFacEle.Text = cmbConsFacEle.Items[1].Text;
                        cmbConsFacEle_SelectedIndexChanged(null, null);
                    }

                    if (Session["PedidoFacturacion" + Session.SessionID] != null) //nueva factura con pedido previo                   
                    {
                        this.ConsultarDatosPedido();
                        Session["PedidoFacturacion" + Session.SessionID] = null;
                    }
                    else if (Session["ListaRemisionesFactura" + Session.SessionID] != null) //nueva factura de remisiones
                    {
                        this.ConsultarDatosRemisiones();
                    }
                    else
                    {
                        Session["ListaRemisionesFactura" + Session.SessionID] = null;
                        this.Nuevo();
                        this.rgFacturaDet.Enabled = false;
                        //rgFacturaDetAde
                        this.rgFacturaDetAde.Enabled = false;
                        //rgFacturaDetAde
                        this.btnFacturaEspecial.Enabled = false;
                    }
                    this.txtFecha.Focus();
                    this.HdId_FacSerie.Value = this.cmbConsFacEle.Text + this.txtId.Text;
                }
                rgAdendaFacturacion.Rebind();
                rgFacturaDet.Rebind();
                rgFacturaDetAde.Rebind();                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LLenarFormFactura(int Id_Emp, int Id_Cd, int Id_Fac, string Id_FacSerie)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Factura factura = new Factura();
                factura.Id_Emp = Id_Emp;
                factura.Id_Cd = Id_Cd;
                factura.Id_Fac = Id_Fac;
                factura.Id_FacSerie = Id_FacSerie;
                List<FacturaDet> listaFacturaDet = new List<FacturaDet>();
                //Consultar factura
                new CN_CapFactura().ConsultaFactura(ref factura, ref listaFacturaDet, sesion.Emp_Cnx);
                //Agregar Adendas
                InicializarTablaDetallesAdenda();
                List<AdendaDet> listCabT = new List<AdendaDet>();
                List<AdendaDet> listDetT = new List<AdendaDet>();
                List<AdendaDet> listCabR = new List<AdendaDet>();
                List<AdendaDet> listDetR = new List<AdendaDet>();

                if (rgFacturaDetAde.Columns.Count > 17)
                    for (int i = rgFacturaDetAde.Columns.Count; i > 17; i--)
                        rgFacturaDetAde.Columns.RemoveAt(rgFacturaDetAde.Columns.Count - 1);

                //if ((factura.Fac_Refactura != null && factura.Fac_Refactura != "") || EsRefactura)
                //{                    
                //    new CN_CapFactura().ConsultarAdenda(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Fac, "7", "8", ref listCabR, ref listDetR, sesion.Emp_Cnx);
                //    if (listCabR.Count > 0)
                //    {
                //        RadTabStrip1.Tabs[2].Visible = true;
                //        RadTabStrip1.Tabs[3].Visible = true;
                //        ListCabRF = listCabR;
                //        rgAdendaReFacturacion.Rebind();                        
                //    }
                //    ListCab = new List<AdendaDet>();
                //}
                //else               
                {
                    //new CN_CapFactura().ConsultarAdenda(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Fac,Id_FacSerie, "1", "2", ref listCabT, ref listDetT, sesion.Emp_Cnx);                    
                    if (listCabT.Count > 0)
                    {                        
                        //RadTabStrip1.Tabs[1].Visible = true;
                        RadTabStrip1.Tabs[2].Visible = true;
                        ListCab = listCabT;
                        rgAdendaFacturacion.Rebind();
                        
                    }
                    listCabR = new List<AdendaDet>();
                }

                if (Page.Request.QueryString["facModificable"].ToString() == "2")
                {
                   if (listCabR.Count == 0)
                   {                        
                       new CN_CatCliente().ConsultarAdenda(sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(factura.Id_Cte), "7,8", ref listCabR, ref listDetR, ref listCabR, sesion.Emp_Cnx);
                        if (listCabR.Count > 0)
                        {                
                            RadTabStrip1.Tabs[3].Visible = true;
                            ListCabRF = listCabR;                
                            rgAdendaReFacturacion.Rebind();   
                         
                       }
                   }
                   HiddenIdRF.Value = hiddenId.Value;
                   hiddenId.Value = "";
                }

                //if ((factura.Fac_Refactura != null && factura.Fac_Refactura != "") || EsRefactura)
                //{                    
                //    GridBoundColumn boundColumn2;
                    
                //    foreach (AdendaDet ad in listDetR)
                //    {
                //        if (!ListaProductosFacturaAdenda.Columns.Contains(ad.Id_AdeDet.ToString()))
                //        {
                //            boundColumn2 = new GridBoundColumn();
                //            rgFacturaDetAde.MasterTableView.Columns.Add(boundColumn2);
                //            boundColumn2.DataField = ad.Id_AdeDet.ToString();
                //            boundColumn2.UniqueName = ad.Id_AdeDet.ToString();
                //            boundColumn2.HeaderText = ad.Campo;
                //            boundColumn2.HeaderStyle.Width = Unit.Pixel(150);
                //            boundColumn2.MaxLength = ad.Longitud;
                //            boundColumn2.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                //            ListaProductosFacturaAdenda.Columns.Add(ad.Id_AdeDet.ToString());
                //        }
                //    }
                //    ListDetRF = listDetR;
                //    ListDet = new List<AdendaDet>();
                //}
                //else
                {
                    GridBoundColumn boundColumn3;                                      

                    foreach (AdendaDet ad in listDetT)
                    {                                             
                        if (!ListaProductosFacturaAdenda.Columns.Contains(ad.Id_AdeDet.ToString()))
                        {
                        boundColumn3 = new GridBoundColumn();
                        rgFacturaDetAde.MasterTableView.Columns.Add(boundColumn3);
                        boundColumn3.DataField = ad.Id_AdeDet.ToString();
                        boundColumn3.UniqueName = ad.Id_AdeDet.ToString();
                        boundColumn3.HeaderText = ad.Campo;
                        boundColumn3.HeaderStyle.Width = Unit.Pixel(150);
                        boundColumn3.MaxLength = ad.Longitud;
                        boundColumn3.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        ListaProductosFacturaAdenda.Columns.Add(ad.Id_AdeDet.ToString());
                        
                        }
                    }                   
                    ListDet = listDetT;
                    ListDetRF = new List<AdendaDet>();
                }

                //CREA BOTON DE EDITAR
                GridEditCommandColumn commandColumnAde = new GridEditCommandColumn();
                rgFacturaDetAde.MasterTableView.Columns.Add(commandColumnAde);

                commandColumnAde.ButtonType = GridButtonColumnType.ImageButton;
                commandColumnAde.UniqueName = "EditCommandColumn";
                commandColumnAde.EditText = "Editar";
                commandColumnAde.CancelText = "Cancelar";
                commandColumnAde.InsertText = "Aceptar";
                commandColumnAde.UpdateText = "Actualizar";
                commandColumnAde.HeaderStyle.Width = Unit.Pixel(60);
                commandColumnAde.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                commandColumnAde.ItemStyle.HorizontalAlign = HorizontalAlign.Center;


                //CREA BOTON ELIMINAR     
                GridButtonColumn deleteColumnAde = new GridButtonColumn();
                rgFacturaDetAde.MasterTableView.Columns.Add(deleteColumnAde);

                deleteColumnAde.ConfirmText = "¿Desea quitar este producto de la lista?";
                deleteColumnAde.ConfirmDialogHeight = Unit.Pixel(150);
                deleteColumnAde.ConfirmDialogWidth = Unit.Pixel(350);
                deleteColumnAde.ConfirmDialogType = GridConfirmDialogType.RadWindow;
                deleteColumnAde.ButtonType = GridButtonColumnType.ImageButton;
                deleteColumnAde.CommandName = "Delete";
                deleteColumnAde.Text = "Eliminar";
                deleteColumnAde.UniqueName = "DeleteColumn";
                deleteColumnAde.HeaderStyle.Width = Unit.Pixel(50);
                deleteColumnAde.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                deleteColumnAde.ItemStyle.HorizontalAlign = HorizontalAlign.Center;


                double ancho2 = 0;
                foreach (GridColumn gc in rgFacturaDetAde.Columns)
                {
                    if (gc.Display)
                    {
                        ancho2 = ancho2 + gc.HeaderStyle.Width.Value;
                    }
                }
                rgFacturaDetAde.Width = Unit.Pixel(Convert.ToInt32(ancho2));
                rgFacturaDetAde.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho2));


                ////rgFacturaDetAde 
                ////rgFacturaDetAde                


                //CREA BOTON DE EDITAR
                GridEditCommandColumn commandColumn = new GridEditCommandColumn();
                rgFacturaDet.MasterTableView.Columns.Add(commandColumn);
                commandColumn.ButtonType = GridButtonColumnType.ImageButton;
                commandColumn.UniqueName = "EditCommandColumn";
                commandColumn.EditText = "Editar";
                commandColumn.CancelText = "Cancelar";
                commandColumn.InsertText = "Aceptar";
                commandColumn.UpdateText = "Actualizar";
                commandColumn.HeaderStyle.Width = Unit.Pixel(60);
                commandColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                commandColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                //CREA BOTON ELIMINAR
                GridButtonColumn deleteColumn = new GridButtonColumn();
                rgFacturaDet.MasterTableView.Columns.Add(deleteColumn);
                deleteColumn.ConfirmText = "¿Desea quitar este producto de la lista?";
                deleteColumn.ConfirmDialogHeight = Unit.Pixel(150);
                deleteColumn.ConfirmDialogWidth = Unit.Pixel(350);
                deleteColumn.ConfirmDialogType = GridConfirmDialogType.RadWindow;
                deleteColumn.ButtonType = GridButtonColumnType.ImageButton;
                deleteColumn.CommandName = "Delete";
                deleteColumn.Text = "Eliminar";
                deleteColumn.UniqueName = "DeleteColumn";
                deleteColumn.HeaderStyle.Width = Unit.Pixel(50);
                deleteColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                deleteColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                double ancho = 0;
                foreach (GridColumn gc in rgFacturaDet.Columns)
                {
                    if (gc.Display)
                    {
                        ancho = ancho + gc.HeaderStyle.Width.Value;
                    }
                }
                rgFacturaDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
                rgFacturaDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));

                //agregar remisiones de factura a variable de sesion, silas trae
                List<Remision> listaRemisiones = new List<Remision>();
                //foreach (FacturaDet fd in listaFacturaDet)
                foreach (FacturaDet dr in listaFacturaDet)
                {
                    if (dr.Id_Rem != null && Convert.ToInt32(dr.Id_Rem) > 0)
                    {
                        Remision rem = new Remision();
                        rem.Id_Rem = Convert.ToInt32(dr.Id_Rem);
                        listaRemisiones.Add(rem);
                    }
                    //columan especial de cliente que no acepta null, por eso si cliente externo es null, se pasa a 0.
                    if (dr.Id_CteExt == null)
                    {
                        dr.Id_CteExt = 0;
                        dr.Id_CteExtStr = string.Empty;
                    }
                }
                if (listaRemisiones.Count > 0)
                    Session["ListaRemisionesFactura" + Session.SessionID] = listaRemisiones;

                txtId.Text = factura.Id_Fac.ToString();
                if (factura.Id_Cfe == null)
                    cmbConsFacEle.SelectedIndex = 0;
                else
                    cmbConsFacEle.SelectedIndex = cmbConsFacEle.FindItemIndexByValue(factura.Id_Cfe.ToString());

                if (Page.Request.QueryString["facModificable"].ToString() == "2")
                    ObtenerConsecutivoFactElectronica(Convert.ToInt32(cmbConsFacEle.SelectedValue));

                txtMov.Text = factura.Id_Tm.ToString();
                cmbMov.SelectedIndex = cmbMov.FindItemIndexByValue(factura.Id_Tm.ToString());
                //si es factura de aparatos inproductivos (mov. 70), se visualiza columna de Cliente Externo del grid
                if (factura.Id_Tm == 70)
                {
                    ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExt")).Display = true;
                    ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExtN")).Display = true;
                }
                else
                {
                    ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExt")).Display = false;
                    ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExtN")).Display = false;
                }
                txtFecha.SelectedDate = factura.Fac_Fecha;
                if (factura.Fac_PedNum == null) txtPedido.Text = string.Empty; else txtPedido.Text = factura.Fac_PedNum.ToString();
                if (factura.Fac_PedDesc == null) txtPedidoDesc.Text = string.Empty; else txtPedidoDesc.Text = factura.Fac_PedDesc.ToString();
                if (factura.Fac_Req == null) txtReq.Text = string.Empty; else txtReq.Text = factura.Fac_Req.ToString();

                txtCliente.Text = factura.Id_Cte.ToString();
                txtClienteNombre.Text = factura.Cte_NomComercial;

                CargarComboTerritorios();
                txtTerritorio.Text = factura.Id_Ter.ToString();
                cmbTerritorio.SelectedIndex = cmbTerritorio.FindItemIndexByValue(factura.Id_Ter.ToString());

                //no se permite modificar al cliente en modificacion de factura
                txtCliente.Enabled = false;
                txtClienteNombre.Enabled = false;

                txtContacto.Text = factura.Fac_Contacto;

                CargarFormaPago();
                cmbFormaPago.SelectedIndex = cmbFormaPago.FindItemIndexByValue(factura.Fac_FPago);
                txtUDigitos.Text = factura.Fac_UDigitos;


                //-----------------------------
                if (factura.Fac_DesgIva == null) chkDesgloce.Checked = false; else chkDesgloce.Checked = Convert.ToBoolean(factura.Fac_DesgIva);
                if (factura.Fac_RetIva == null) chkRetencion.Checked = false; else chkRetencion.Checked = Convert.ToBoolean(factura.Fac_RetIva);
                if (factura.Id_Mon == null) txtMoneda.Text = string.Empty; else txtMoneda.Text = factura.Id_Mon.ToString();
                if (factura.Id_Mon == null) cmbMoneda.SelectedIndex = -1; else cmbMoneda.SelectedIndex = cmbMoneda.FindItemIndexByValue(factura.Id_Mon.ToString());

                if (factura.Fac_CteCalle == null) txtCalle.Text = string.Empty; else txtCalle.Text = factura.Fac_CteCalle.ToString();
                if (factura.Fac_CteNumero == null) txtCalleNumero.Text = string.Empty; else txtCalleNumero.Text = factura.Fac_CteNumero.ToString();
                if (factura.Fac_CteCp == null) txtCP.Text = string.Empty; else txtCP.Text = factura.Fac_CteCp.ToString();
                if (factura.Fac_CteColonia == null) txtColonia.Text = string.Empty; else txtColonia.Text = factura.Fac_CteColonia.ToString();
                if (factura.Fac_CteMunicipio == null) txtMunicipio.Text = string.Empty; else txtMunicipio.Text = factura.Fac_CteMunicipio.ToString();
                if (factura.Fac_CteEstado == null) txtEstado.Text = string.Empty; else txtEstado.Text = factura.Fac_CteEstado.ToString();
                if (factura.Fac_CteRfc == null) txtRFC.Text = string.Empty; else txtRFC.Text = factura.Fac_CteRfc.ToString();
                if (factura.Fac_CteTel == null) txtTelefono.Text = string.Empty; else txtTelefono.Text = factura.Fac_CteTel.ToString();

                if (factura.Fac_CondEntrega == null) txtCondiciones.Text = string.Empty; else txtCondiciones.Text = factura.Fac_CondEntrega.ToString();
                if (factura.Fac_Conducto == null) txtConducto.Text = string.Empty; else txtConducto.Text = factura.Fac_Conducto.ToString();
                if (factura.Fac_NumEntrega == null) txtNumeroEntrega.Text = string.Empty; else txtNumeroEntrega.Text = factura.Fac_NumEntrega.ToString();
                if (factura.Fac_OrdEntrega == null) txtOrden.Text = string.Empty; else txtOrden.Text = factura.Fac_OrdEntrega.ToString();
                if (factura.Fac_NumeroGuia == null) txtNumeroGuia.Text = string.Empty; else txtNumeroGuia.Text = factura.Fac_NumeroGuia.ToString();
                if (factura.Fac_Notas == null) txtNotas.Text = string.Empty; else txtNotas.Text = factura.Fac_Notas.ToString();
                if (factura.Fac_DescPorcen1 == null) txtDescuento1.Text = string.Empty; else txtDescuento1.Text = factura.Fac_DescPorcen1.ToString();
                if (factura.Fac_DescPorcen2 == null) txtDescuento2.Text = string.Empty; else txtDescuento2.Text = factura.Fac_DescPorcen2.ToString();
                if (factura.Fac_Desc1 == null) txtDescPorc1.Text = string.Empty; else txtDescPorc1.Text = factura.Fac_Desc1.ToString();
                if (factura.Fac_Desc2 == null) txtDescPorc2.Text = string.Empty; else txtDescPorc2.Text = factura.Fac_Desc2.ToString();
                if ((factura.Fac_ImporteRetencion > 0) && (factura.Fac_RetIva == true)) { chkRetencion.Checked = true; consultarRetencion(); } else { chkRetencion.Checked = false; txtPorcRetencion.Visible = false; }

                if (txtCondiciones.Text.Trim() == "")
                {
                    CN_CatCliente Cn_cte = new CN_CatCliente();
                    Clientes cte = new Clientes();
                    cte.Id_Emp = sesion.Id_Emp;
                    cte.Id_Cd = sesion.Id_Cd_Ver;
                    cte.Id_Cte = factura.Id_Cte;
                    Cn_cte.ConsultaClientes(ref cte, sesion.Emp_Cnx);

                    txtCondiciones.Text = cte.Cte_CondPago.ToString();
                }

                if (factura.Id_Es == null) 
                    this.hiddenId_Es.Value = string.Empty; 
                else 
                    this.hiddenId_Es.Value = factura.Id_Es.ToString();
                
                ConvertiraDt(listaFacturaDet, listDetR, factura.Fac_Refactura);
                //this.CalcularTotales();

                txtSubTotal.DbValue = factura.Fac_SubTotal;
                txtIVA.DbValue = factura.Fac_ImporteIva;
                txtImporte.DbValue = factura.Fac_Importe;
                txtTotal.DbValue = factura.Fac_SubTotal + factura.Fac_ImporteIva;

                if ((factura.Id_Cte.ToString() != "-1" && factura.Id_Cte.ToString() != string.Empty)
                    && (this.cmbMov.SelectedValue != "-1" && this.cmbMov.SelectedValue != string.Empty)
                    && (this.cmbTerritorio.SelectedValue != "-1" && this.cmbTerritorio.SelectedValue != string.Empty))
                {
                    this.rgFacturaDet.Enabled = true;
                    this.rgFacturaDetAde.Enabled = true;
                    this.rgAdendaFacturacion.Enabled = true;
                    this.btnFacturaEspecial.Enabled = true;
                }
                else
                {
                    this.rgFacturaDet.Enabled = false;
                    this.rgAdendaFacturacion.Enabled = false;
                    this.rgFacturaDetAde.Enabled = false;
                    this.btnFacturaEspecial.Enabled = false;
                }

                CargarEspecial(Id_Fac,Id_FacSerie, sesion, factura.Id_Cte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarEspecial(int Id_Fac,string Id_FacSerie, Sesion sesion, int Id_Cte)
        {
            //- Especial
            List<FacturaDet> listaProdFacturaEspecialFinal = new List<FacturaDet>();
            new CN_CapFactura().ConsultaFacturaEspecialDetalle(ref listaProdFacturaEspecialFinal
                , sesion.Emp_Cnx
                , sesion.Id_Emp
                , sesion.Id_Cd_Ver
                , Id_Fac
                , Id_FacSerie
                , Id_Cte);

            if (listaProdFacturaEspecialFinal.Count > 0)
            {
                Session["ListaProductosFacturaEspecial" + Session.SessionID] = listaProdFacturaEspecialFinal;
                Session["FacEspecialGuardada" + Session.SessionID] = 1;
            }
            //-
        }
        private void CargarProductos(RadComboBox sender)
        {
            try
            {
                ErrorManager();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatProducto_Combo", ref sender);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ConvertiraDt(List<FacturaDet> listaFacturaDet, List<AdendaDet> listaReFacturaDet, object refactura)
        {
            
            try
            {
                ArrayList al;                
                foreach (FacturaDet fd in listaFacturaDet)
                {
                    al = new ArrayList();
                    al.Add(fd.Id_Fac);
                    al.Add(fd.Id_FacDet);
                    al.Add(fd.Id_Rem);
                    al.Add(0);//Id_Tm_Rem
                    al.Add(fd.Id_CteExt);
                    al.Add(fd.Id_Ter);
                    al.Add(fd.Id_Prd);
                    al.Add(fd.Producto.Prd_Descripcion);
                    al.Add(fd.Producto.Prd_Presentacion);
                    al.Add(fd.Producto.Prd_UniNe);
                    al.Add(fd.Fac_Cant);
                    al.Add(fd.Rem_Cant);
                    al.Add(fd.Fac_Precio);
                    al.Add(fd.Fac_Importe);
                    al.Add(fd.Id_TerStr);
                    al.Add(fd.Id_CteExtStr);
                    al.Add(fd.AmortizacionProducto);
                    al.Add(fd.Id_Emp);
                    al.Add(fd.Id_Cd);
                    al.Add(fd.Fac_Asignar);
                    al.Add(fd.Fac_Devolucion);
                    al.Add(fd.Producto.Prd_UniNs);
                    
                    ListaProductosFactura.Rows.Add(al.ToArray());
                }                
               
                //NUEVO METODO PARA OBTENER LOS VALORES DE LAS ADENDAS
                //NUEVO METODO PARA OBTENER LOS VALORES DE LAS ADENDAS

                ArrayList alAde;
                ArrayList nombreCampos;
                nombreCampos = new ArrayList();
                alAde = new ArrayList();
                string primercampo = "";
                int primerfila = 0;   

                foreach (AdendaDet ad1 in ListDet)
                {
                    if (primerfila == 0)
                    {
                        primercampo = ad1.Campo.ToString();
                        foreach (FacturaDet fd in listaFacturaDet)
                        {
                            if (fd.Id_Prd == ad1.Id_Prd)
                            {
                                alAde.Add(generarGUI_IdAdeDet());                                
                                alAde.Add(fd.Id_Prd);
                                alAde.Add(fd.Producto.Prd_Descripcion);
                            }
                        } 
                    }
                    nombreCampos.Add(ad1.Campo.ToString());

                    if (nombreCampos.Count > 1 && ad1.Campo == primercampo)
                    {                       
                            ListaProductosFacturaAdenda.Rows.Add(alAde.ToArray());
                            alAde = new ArrayList();
                            foreach (FacturaDet fd in listaFacturaDet)
                            {
                                if (fd.Id_Prd == ad1.Id_Prd)
                                {
                                    alAde.Add(generarGUI_IdAdeDet());
                                    alAde.Add(fd.Id_Prd);
                                    alAde.Add(fd.Producto.Prd_Descripcion);
                                }
                            }                       
                     }

                alAde.Add(ad1.Valor);

                    primerfila++;                                                        
                }
                ListaProductosFacturaAdenda.Rows.Add(alAde.ToArray());
            
        
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CalcularAdenda(out bool calcularAdendaTrue, out bool subtotalesIguales)
        {
            calcularAdendaTrue = false;
            subtotalesIguales = false;
            try
            {
                ErrorManager();

                string cantidadExiste = "NO", precioExiste = "NO";
                double subtotales = 0, subtotal = 0;
                string campoCant = "", campoPre = "";

                //SE BUSCA SI EXISTE CAMPO DE CANTIDAD Y DE PRECIO
                foreach (AdendaDet ad1 in ListDet)
                {
                    string campoTemp = ad1.Id_AdeDet.ToString();
                    if (ad1.Nodo.ToString() == "AddendaCantidad")
                    {
                        cantidadExiste = "SI";
                        campoCant = campoTemp;
                    }
                    if (ad1.Nodo.ToString() == "AddendaVU")
                    {
                        precioExiste = "SI";
                        campoPre = campoTemp;
                    }
                }

                if (cantidadExiste == "SI" && precioExiste == "SI") //VERIFICAMOS QUE EXISTAN LAS 2 COLUMNAS DE PRECIO Y CANTIDAD
                {
                    //CALCULAMOS EL SUBTOTAL
                    foreach (DataRow myRow in ListaProductosFacturaAdenda.Rows)
                    {
                        subtotales = Convert.ToDouble(myRow[campoCant]) * Convert.ToDouble(myRow[campoPre]);
                        subtotal += subtotales;
                    }
                    calcularAdendaTrue = true;
                }

                if (Math.Round(subtotal, 2) == Math.Round(Convert.ToDouble(txtSubTotal.Text), 2))
                {
                    subtotalesIguales = true;
                }
            }
            catch (Exception ex)
            {
                Alerta("Los Valores de CANTIDAD y PRECIO de la ADDENDA no se han llenado correctamente");
                throw ex;


            }
        }
        private void LlenarFactura(DataTable ListaProductosFactura, ref Factura factura)
        {
            try
            {
                Funciones func = new Funciones();
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];

                if (ListaProductosFactura.Rows.Count > 0)
                {
                    factura.Id_Tm_Rem = !string.IsNullOrEmpty(ListaProductosFactura.Rows[0]["Id_Tm"].ToString()) ? Convert.ToInt32(ListaProductosFactura.Rows[0]["Id_Tm"].ToString()) : 0;
                }
                factura.Id_Emp = session.Id_Emp;
                factura.Id_Cd = session.Id_Cd_Ver;
            
                factura.Id_Fac = Convert.ToInt32(txtId.Text); //cambia cuando se inserta la factura si es nueva, permanece si se modifica
                if (cmbConsFacEle.SelectedValue == "-1") factura.Id_Cfe = null; else factura.Id_Cfe = Convert.ToInt32(cmbConsFacEle.SelectedValue);
                if (cmbConsFacEle.SelectedValue == "-1") factura.Id_FacSerie = null; else factura.Id_FacSerie = string.Concat(cmbConsFacEle.Text);
                factura.Id_U = session.Id_U;
                factura.Id_Tm = Convert.ToInt32(cmbMov.SelectedValue);
                if (txtPedido.Text == string.Empty) factura.Fac_PedNum = null; else factura.Fac_PedNum = Convert.ToInt32(txtPedido.Text);
                if (txtPedidoDesc.Text == string.Empty) factura.Fac_PedDesc = null; else factura.Fac_PedDesc = txtPedidoDesc.Text;
                if (txtReq.Text == string.Empty) factura.Fac_Req = null; else factura.Fac_Req = txtReq.Text;
                factura.Fac_Fecha = Convert.ToDateTime(txtFecha.SelectedDate.Value.ToString("dd/MM/yyyy") + " " + func.GetLocalDateTime(session.Minutos).ToString("HH:mm:ss"));
                factura.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1);
                factura.Id_Ter = Convert.ToInt32(cmbTerritorio.SelectedValue);
                factura.Id_Rik = Convert.ToInt32(txtRepresentante.Text);
                factura.Id_Mon = Convert.ToInt32(cmbMoneda.SelectedValue);
                factura.Fac_DesgIva = chkDesgloce.Checked;
                factura.Fac_RetIva = chkRetencion.Checked;
                factura.Fac_Contacto = txtContacto.Text;
                factura.Fac_CteCalle = txtCalle.Text;
                factura.Fac_CteNumero = txtCalleNumero.Text;
                factura.Fac_CteCp = txtCP.Text;
                factura.Fac_CteColonia = txtColonia.Text;
                factura.Fac_CteMunicipio = txtMunicipio.Text;
                factura.Fac_CteEstado = txtEstado.Text;
                factura.Fac_CteRfc = txtRFC.Text;
                factura.Fac_CteTel = txtTelefono.Text;
                //  if (txtOrden.Text == string.Empty) factura.Fac_OrdEntrega = null; else factura.Fac_OrdEntrega = txtOrden.Text;
                factura.Fac_OrdEntrega = txtOrden.Text;
                factura.Fac_NumeroGuia = txtNumeroGuia.Text;
                factura.Fac_CondEntrega = txtCondiciones.Text;
                if (txtNumeroEntrega.Text == string.Empty) factura.Fac_NumEntrega = null; else factura.Fac_NumEntrega = Convert.ToInt32(txtNumeroEntrega.Text);
                factura.Fac_Notas = txtNotas.Text;
                if (txtDescuento1.Text == string.Empty) factura.Fac_DescPorcen1 = null; else factura.Fac_DescPorcen1 = Convert.ToDouble(txtDescuento1.Text);
                if (txtDescuento2.Text == string.Empty) factura.Fac_DescPorcen2 = null; else factura.Fac_DescPorcen2 = Convert.ToDouble(txtDescuento2.Text);
                factura.Fac_Desc1 = txtDescPorc1.Text;
                factura.Fac_Desc2 = txtDescPorc2.Text;
                factura.Fac_Tipo = "VN";
                factura.Fac_Conducto = txtConducto.Text;
                factura.Fac_CanNum = null; //????
                factura.Fac_FecCan = null;
                factura.Fac_Pagado = 0;
                factura.Fac_FecPag = DateTime.Now;
                factura.Fac_Importe = Convert.ToDouble(txtImporte.Value.Value);
                factura.Fac_SubTotal = Convert.ToDouble(txtSubTotal.Text);
                factura.Fac_ImporteIva = Convert.ToDouble(txtIVA.Text);
                if (factura.Fac_RetIva == true)
                {
                    factura.Fac_ImporteRetencion = Convert.ToDouble(txtPorcRetencion.Text) != 0 ? ((Convert.ToDouble(txtSubTotal.Text)) * (Convert.ToDouble(txtPorcRetencion.Text) / 100)) : 0;
                }
                else
                {
                    factura.Fac_ImporteRetencion = 0;
                }
                factura.Fac_Estatus = "C";

                factura.Fac_FPago = cmbFormaPago.SelectedValue;
                factura.Fac_UDigitos = txtUDigitos.Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Nuevo()
        {
            try
            {
                this.HabilitaControles(true);
                txtNotas.Text = string.Empty;
                //COMENTARIADO POR OSCAR PARA CAMBIO DE LISTA A DT
                //rgFacturaDet.DataSource = this.ListaProductosFactura = new List<FacturaDet>();
                //rgFacturaDet.DataBind();
                txtFecha.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Guardar()
        {
            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                if (rgFacturaDet.Items.Count == 0)
                {
                    Alerta("Capture al menos un producto para guardar la factura");
                    return;
                }
                

                if (string.IsNullOrEmpty(cmbTerritorio.SelectedValue) || cmbTerritorio.SelectedValue == "-1")
                {
                    Alerta("Seleccione un territorio válido");
                    return;
                }

                if (HF_VI.Value == "true")
                { //validacion para facturaVI campo requisicion
                    bool requisicion = false;
                    Factura fac2 = new Factura();
                    fac2.Id_Cte = Convert.ToInt32(txtCliente.Text);
                    fac2.Id_Ter = Convert.ToInt32(txtTerritorio.Text);
                    new CN_CapFactura().FacturaVI_ValidadorRequisicion(session, fac2, ref requisicion);
                    if (requisicion)
                    {
                        if (string.IsNullOrEmpty(txtReq.Text))
                        {
                            AlertaFocus("El campo Requisición es obligatorio", txtReq.ClientID);
                            return;
                        }
                    }
                }

                EntradaSalida entSal = new EntradaSalida();
                List<EntradaSalidaDetalle> listaEntSal = new List<EntradaSalidaDetalle>();
                List<EntradaSalida> listaEntSalRemisiones = new List<EntradaSalida>();
                string productosFactura = string.Empty; //para las notas de la entrada-salida
                float montoAmortizacionTotal = 0;

                //llenar datos de factura
                Factura factura = new Factura();
                this.LlenarFactura(ListaProductosFactura, ref factura);
                int error = 0;
                for (int m = 0; m < rgFacturaDet.Items.Count; m++)
                {
                    if ((rgFacturaDet.Items[m].FindControl("lblid_prdnum") as Label) == null)
                    {
                        Alerta("Capture al menos un producto para guardar la factura");
                        error = 1;
                        break;
                    }
                }
                if (error == 1)
                    return;

                for (int k = 0; k < rgFacturaDet.Items.Count; k++)
                {
                    int prd = Convert.ToInt32((rgFacturaDet.Items[k].FindControl("lblid_prdnum") as Label).Text);//rgFacturaDet.Items[k]["Id_Prd"].ToString());
                    int ter = Convert.ToInt32((rgFacturaDet.Items[k].FindControl("LblTerritorioPartidaNum") as Label).Text);//rgFacturaDet.Items[k]["Id_Ter"].ToString());
                    int cantidad = Convert.ToInt32((rgFacturaDet.Items[k].FindControl("lblord_cantidad") as Label).Text);//rgFacturaDet.Items[k]["Cantidad"].ToString());

                    List<int> actuales = new List<int>();
                    CN_CatProducto cn_producto = new CN_CatProducto();
                    cn_producto.ConsultaProducto_Disponible(session.Id_Emp, session.Id_Cd_Ver, prd.ToString(), ref actuales, session.Emp_Cnx);

                    int cantidad_B = 0;
                    int disponible_pedido = 0;
                    #region pedido
                    if (txtPedido.Text != "")
                    {
                        CN_CapPedido cappedido = new CN_CapPedido();
                        Pedido pedido = new Pedido();
                        pedido.Id_Emp = session.Id_Emp;
                        pedido.Id_Cd = session.Id_Cd_Ver;
                        pedido.Id_Ped = Convert.ToInt32(txtPedido.Text);

                        DataTable dt2 = new DataTable();
                        dt2.Columns.Add("Id_PedDet");
                        dt2.Columns.Add("Id_Ter");
                        dt2.Columns.Add("Ter_Nombre");
                        dt2.Columns.Add("Id_Prd");
                        dt2.Columns.Add("Prd_Descripcion");
                        dt2.Columns.Add("Prd_Presentacion");
                        dt2.Columns.Add("Prd_Unidad");
                        dt2.Columns.Add("Prd_Precio");
                        dt2.Columns.Add("Disponible");
                        dt2.Columns.Add("Prd_Importe");
                        dt2.Columns.Add("Id_Rem");
                        cappedido.ConsultaPedidoDetDisp(pedido, ref dt2, 1, session.Emp_Cnx);

                        DataRow[] dr = dt2.Select("Id_Prd='" + prd + "'");

                        if (dr.Length > 0)
                        {
                            for (int i = 0; i < dr.Length; i++)
                                disponible_pedido += !string.IsNullOrEmpty(dr[i]["Disponible"].ToString()) ? Convert.ToInt32(dr[i]["Disponible"]) : 0;
                        }
                        if (disponible_pedido < 0)
                            disponible_pedido = 0;
                    }
                    #endregion

                    DataRow[] Dr2 = ListaProductosFactura.Select("Id_Prd='" + prd + "' and Id_Ter <> '" + ter + "'");
                    if (Dr2.Length > 0)
                    {
                        for (int i = 0; i < Dr2.Length; i++)
                            cantidad_B += !string.IsNullOrEmpty(Dr2[i]["Fac_Cant"].ToString()) ? Convert.ToInt32(Dr2[i]["Fac_Cant"]) : 0;
                    }

                    int cantRemision = 0;
                    if (Session["ListaRemisionesFactura" + Session.SessionID] != null)
                    {
                        List<Remision> listaRemisiones = (List<Remision>)Session["ListaRemisionesFactura" + Session.SessionID];

                        arrayRemisiones = "";
                        foreach (Remision rem in listaRemisiones)
                        {
                            arrayRemisiones = string.Concat(arrayRemisiones, rem.Id_Rem.ToString(), "|");
                        }
                        if (arrayRemisiones.Length > 1)
                            arrayRemisiones = arrayRemisiones.Substring(0, arrayRemisiones.Length - 1);

                        CN_CapRemision cr = new CN_CapRemision();
                        cantRemision = cr.ConsultaCantidadRemision(session, prd, arrayRemisiones);
                    }
                    else
                    {
                        int fac = Convert.ToInt32(Page.Request.QueryString["Id_Fac"]);
                        if (fac != -1)
                            if (string.IsNullOrEmpty(txtPedido.Text))
                            {
                                CN_CapFactura fact = new CN_CapFactura();
                                fact.ConsultarCantidadProdFactura(session, prd, fac, ter, ref cantRemision);
                            }
                            else
                                cantRemision = cantidad_A;
                    }
                }
                /*
                * Si el tipo de movimiento fue 70 (caso de aparatos inproductivo):
                * Calcula la amortización . (Checar formulas para el cálculo de la amortización). 
                * Se genera un movimiento en el almacén (CapEntSal antes entsal,  CapEntSalDet antes entsal1). 
                * Se genera un movimiento 16 de tipo automático, en las referencia se captura el número de factura y hace referencia a la factura.
                */
                #region txtMov = 70
                if (txtMov.Text == "70")
                {
                    Amortizacion amortizacion = new Amortizacion();
                    amortizacion.Id_Emp = session.Id_Emp;
                    amortizacion.Id_Cd = session.Id_Cd_Ver;
                    amortizacion.Id_Cte = Convert.ToInt32(txtCliente.Text);
                    List<Amortizacion> listAmortizacion = new List<Amortizacion>();

                    //obtener productos con amortización del cliente
                    new CN_Amortizacion().ConsultaAmortizacionCliente(amortizacion, session.Emp_Cnx, ref listAmortizacion);
                    this.ListaProductosAmortizacion = listAmortizacion;

                    //calcula amortizacion de cada producto
                    int anioActual = DateTime.Now.Year;
                    int mesActual = DateTime.Now.Month;
                    //foreach (FacturaDet facturaDet in this.ListaProductosFactura)
                    foreach (DataRow facturaDet in ListaProductosFactura.Rows)
                    {
                        //validar que todas las partidas tengan capturado el cliente externo
                        if (facturaDet["Id_CteExt"] == null)
                        {
                            RadMultiPage1.SelectedIndex = 1;
                            RadTabStrip1.SelectedIndex = RadTabStrip1.FindTabByValue("Detalles").Index;
                            throw new Exception("FacturacionClienteExtNoEnPartida");
                        }
                        productosFactura = string.Concat(productosFactura, "Prod ", facturaDet["Id_Prd"], ": ", facturaDet["Fac_Cant"], ", ");
                        float montoAmortizacion = 0;
                        foreach (Amortizacion amor in this.ListaProductosAmortizacion)
                        {
                            if (Convert.ToInt32(facturaDet["Id_Prd"]) == amor.Id_Prd)
                            {
                                //si el año y mes actual es mayor al año y mes de la amortizacion del producto
                                //la amortizacion se queda en 0
                                DateTime fechaFinAmortizacion = new DateTime(amor.Amo_AnioFin, amor.Amo_MesFin, 1);
                                DateTime fechaActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                                int mesesAmortizacion = 0;
                                if (((TimeSpan)(fechaFinAmortizacion.Subtract(fechaActual))).Ticks > 0)
                                {
                                    //calcula meses de amortizacion
                                    //al final al mes actual se le resta 1 porque aun no se acaba el mes actual
                                    mesesAmortizacion = (((anioActual - amor.Amo_AnioInicio) * 12) - amor.Amo_MesInicio) + (mesActual - 1);
                                }
                                float importeTotalAmortizacion = amor.Amo_Cant * amor.Amo_Costo;
                                montoAmortizacion = importeTotalAmortizacion / mesesAmortizacion;

                                facturaDet["AmortizacionProducto"] = montoAmortizacion;
                                montoAmortizacionTotal += montoAmortizacion;
                                break;
                            }
                        }

                        //Crear item de lista de entrada-salida mov. 16
                        EntradaSalidaDetalle entSalDetalle = new EntradaSalidaDetalle();
                        entSalDetalle.Id_Emp = session.Id_Emp;
                        entSalDetalle.Id_Cd = session.Id_Cd_Ver;
                        entSalDetalle.Id_Es = 0;//se reasigna al insertar la entSal de encabezado
                        entSalDetalle.Id_EsDet = 0;//se reasigna al insertar la entSalDetalle
                        entSalDetalle.Id_Ter = Convert.ToInt32(facturaDet["Id_Ter"]);//txtTerritorio.Text);
                        entSalDetalle.Id_Prd = Convert.ToInt32(facturaDet["Id_Prd"]);
                        entSalDetalle.EsDet_Naturaleza = 0; //entrada
                        entSalDetalle.Es_Cantidad = Convert.ToInt32(facturaDet["Fac_Cant"]);
                        entSalDetalle.Es_Costo = Convert.ToInt32(facturaDet["Fac_Precio"]);
                        entSalDetalle.Es_BuenEstado = true;
                        entSalDetalle.Afct_OrdCompra = false;
                        listaEntSal.Add(entSalDetalle);
                    }
                    productosFactura = (productosFactura.Length > 0) ? (productosFactura.Substring(0, productosFactura.Length - 2)) : productosFactura;

                    //llenar objeto de entrada-salida
                    entSal.Id_Emp = session.Id_Emp;
                    entSal.Id_Cd = session.Id_Cd_Ver;
                    entSal.Id_U = session.Id_U;
                    entSal.Id_Tm = 16; //mov. de entrada por aparatos inproductivos

                    try
                    {
                        entSal.Id_Cte = Convert.ToInt32(ListaProductosFactura.Rows[0]["Id_CteExt"].ToString()); //Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1);
                    }
                    catch
                    {

                    }
                    entSal.Id_Pvd = -1;
                    try
                    {
                        entSal.Id_Ter = Convert.ToInt32(ListaProductosFactura.Rows[0]["Id_Ter"].ToString()); //Convert.ToInt32(txtTerritorio.Text);
                    }
                    catch
                    {

                    }


                    entSal.Es_Naturaleza = 0;//entrada
                    entSal.Es_Fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    entSal.Es_Referencia = "0"; //ref. de factura o remision
                    entSal.Es_Notas = string.Concat("Factura: #Id_Fac# Aparatos improductivos/Ter: ", entSal.Id_Ter, ", Cte: ", entSal.Id_Cte, " ", productosFactura);
                    entSal.Es_SubTotal = Convert.ToDouble(txtSubTotal.Text);
                    entSal.Es_Iva = Convert.ToDouble(txtIVA.Text);
                    entSal.Es_Total = Convert.ToDouble(txtTotal.Text);
                    entSal.Es_Estatus = "C";
                }
                #endregion
                factura.Fac_SubTotal += montoAmortizacionTotal;
                // ------------------------------------------------------------------------------------------
                // checar si se esta facturando remisiones y obtener contrapartidas (ENTRADAS) de remisiones 
                // ------------------------------------------------------------------------------------------
                #region remisiones
                //if (Session["PedidoRemisionado" + Session.SessionID] != null)
                //{
                //    if (txtPedido.Text != "")
                //    {
                //        CN_CapRemision remision = new CN_CapRemision();
                //        List<Remision> listaRemisiones = new List<Remision>();
                //        remision.ConsultaRemisionesxPedido(session, Convert.ToInt32(txtPedido.Text), ref listaRemisiones);
                //        foreach (Remision rem in listaRemisiones)
                //        {
                //            arrayRemisiones = string.Concat(arrayRemisiones, rem.Id_Rem.ToString(), "|");
                //        }
                //        if (arrayRemisiones.Length > 1)
                //        {
                //            arrayRemisiones = arrayRemisiones.Substring(0, arrayRemisiones.Length - 1);
                //            Session["ListaRemisionesFactura" + Session.SessionID] = listaRemisiones;
                //        }
                //    }
                //}
                List<Remision> listaRem = (List<Remision>)Session["ListaRemisionesFactura" + Session.SessionID];
                if (listaRem == null)
                {
                    if (!string.IsNullOrEmpty(txtPedido.Text))
                    {
                        CN_CapRemision remision = new CN_CapRemision();
                        listaRem = new List<Remision>();
                        remision.ConsultaRemisionesxPedido(session, Convert.ToInt32(txtPedido.Text), ref listaRem);
                    }
                    else
                    {
                        CN_CapRemision remision = new CN_CapRemision();
                        listaRem = new List<Remision>();
                        remision.ConsultaRemisionesxFactura(session, Convert.ToInt32(txtId.Text), ref listaRem);
                    }
                }
                //agregado para facturar un pedido-remision
                if (listaRem == null || Session["PedidoRemisionado" + Session.SessionID] != null || listaRem.Count == 0)
                {
                    if (!string.IsNullOrEmpty(txtPedido.Text))
                    {
                        CN_CapRemision remision = new CN_CapRemision();
                        listaRem = new List<Remision>();
                        remision.ConsultaRemisionesxPedido(session, Convert.ToInt32(txtPedido.Text), ref listaRem);
                    }
                }
                if (listaRem != null) //nueva factura de remisiones
                {
                    foreach (Remision rem in listaRem)
                    {
                        EntradaSalida entRem = new EntradaSalida();
                        //llenar objeto de entrada-salida
                        entRem.Id_Emp = session.Id_Emp;
                        entRem.Id_Cd = session.Id_Cd_Ver;
                        entRem.Id_U = session.Id_U;
                        entRem.Id_Ter = Convert.ToInt32(txtTerritorio.Text);
                        entRem.Id_Tm = 0; //mov. de entrada por mov. inverso de facturacion de remision
                        entRem.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1);
                        entRem.Id_Pvd = -1;
                        entRem.Es_Naturaleza = 0;//entrada
                        entRem.Es_Fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        entRem.Es_Referencia = rem.Id_Rem.ToString(); //ref. de factura o remision
                        entRem.Es_Notas = string.Empty;//string.Concat("Remisión ", rem.Id_Rem.ToString());//, ", #producto-cantidad#");
                        entRem.Es_SubTotal = 0;
                        entRem.Es_Iva = 0;
                        entRem.Es_Total = 0;
                        entRem.Es_Estatus = "C";
                        entRem.Id_Rem = rem.Id_Rem;
                        entRem.ListaDetalle = new List<EntradaSalidaDetalle>();
                        listaEntSalRemisiones.Add(entRem);
                    }
                }
                #endregion
                //foreach (FacturaDet facturaDet in this.ListaProductosFactura)
                #region ListaProductosFactura
                foreach (DataRow facturaDet in ListaProductosFactura.Rows)
                {
                    //esta validacion es por la columna especial de cliente externo (que en el Grid no puede ser null se maneja como 0), 
                    // --> si es 0 se guarda como null en base de datos
                    //if (facturaDet.Id_CteExt == 0) COMENTARIADO POR OSCAR
                    int? id_cteext = !Convert.IsDBNull(facturaDet["Id_CteExt"]) ? Convert.ToInt32(facturaDet["Id_CteExt"]) == 0 ? (int?)null : Convert.ToInt32(facturaDet["Id_CteExt"]) : null;

                    facturaDet["Id_CteExt"] = id_cteext;
                    //validar si es producto de remisión
                    //if (facturaDet.Id_Rem != null && facturaDet.Id_Rem > 0) COMENTARIADO POR OSCAR
                    if (!Convert.IsDBNull(facturaDet["Id_Rem"]))
                        if (Convert.ToInt32(facturaDet["Id_Rem"]) > 0)
                        {//Crear item de lista de entrada-salida mov. 16
                            EntradaSalidaDetalle entSalDetalleRem = new EntradaSalidaDetalle();
                            entSalDetalleRem.Id_Emp = session.Id_Emp;
                            entSalDetalleRem.Id_Cd = session.Id_Cd_Ver;
                            entSalDetalleRem.Id_Es = 0;//se reasigna al insertar la entSal de encabezado
                            entSalDetalleRem.Id_EsDet = 0;//se reasigna al insertar la entSalDetalle
                            entSalDetalleRem.Id_Ter = Convert.ToInt32(facturaDet["Id_Ter"]);//txtTerritorio.Text);
                            entSalDetalleRem.Id_Prd = Convert.ToInt32(facturaDet["Id_Prd"]); //facturaDet.Id_Prd;
                            entSalDetalleRem.EsDet_Naturaleza = 0; //entrada
                            entSalDetalleRem.Es_Cantidad = Convert.ToInt32(facturaDet["Fac_Cant"]); //facturaDet.Fac_Cant;
                            entSalDetalleRem.Es_Costo = Convert.ToDouble(facturaDet["Fac_Precio"]); //facturaDet.Fac_Precio;
                            entSalDetalleRem.Es_BuenEstado = true;
                            entSalDetalleRem.Afct_OrdCompra = false;
                            entSalDetalleRem.Id_Rem = Convert.ToInt32(facturaDet["Id_Rem"]);
                            entSalDetalleRem.Es_CantidadRem = 0;// Convert.ToInt32(facturaDet["Rem_Cant"]);

                            foreach (EntradaSalida entRem in listaEntSalRemisiones)
                            {
                                if (entRem.Es_Notas.Contains(string.Concat("Remisión ", facturaDet["Id_Rem"].ToString()))) //si las notas tienen la referencia a la remisión
                                {
                                    //entRem.Es_Notas = entRem.Es_Notas.Replace("#producto-cantidad#",
                                    //    string.Concat("producto ", facturaDet["Id_Prd"].ToString(), " cantidad ", facturaDet["Fac_Cant"], ", #producto-cantidad#"));
                                    entRem.ListaDetalle.Add(entSalDetalleRem);
                                    break;
                                }
                            }
                        }
                }
                #endregion
                //calcular totales de movimientos de ENTRADA inversos por facturacion de remisiones
                #region totales de movimientos
                //if (listaEntSalRemisiones.Count > 0)
                //    for (int i = 0; i < listaEntSalRemisiones.Count; i++)
                //    {
                //        EntradaSalida entRem = listaEntSalRemisiones[i];
                //        this.CalcularTotalesEntradaRemision(ref entRem);
                //        entRem.Es_Notas = entRem.Es_Notas.Replace(", #producto-cantidad#", string.Empty);
                //    }
                #endregion

                #region Adenda
                List<AdendaDet> listAdendaCabecera = new List<AdendaDet>();
                AdendaDet ad;
                RadTextBox txtAdenda = new RadTextBox();

                List<AdendaDet> listCabT = new List<AdendaDet>();
                List<AdendaDet> listDetT = new List<AdendaDet>();
                List<AdendaDet> listCabR = new List<AdendaDet>();
                               
                for (int i = 0; i < rgAdendaFacturacion.MasterTableView.Items.Count; i++)
                {
                    ad = new AdendaDet();
                    ad.Tipo = 1;
                    ad.Id_AdeDet = Convert.ToInt32(rgAdendaFacturacion.MasterTableView.Items[i]["Id_AdeDet"].Text);
                    ad.Campo = rgAdendaFacturacion.MasterTableView.Items[i]["campo"].Text;
                    ad.Nodo = rgAdendaFacturacion.MasterTableView.Items[i]["nodo"].Text;
                    txtAdenda = rgAdendaFacturacion.MasterTableView.Items[i]["valor"].FindControl("RadTextBox1") as RadTextBox;
                    ad.Valor = txtAdenda.Text.Replace("'", "").Trim();
                    bool addenda_Requerida = ListCab.Where(AdendaDet => AdendaDet.Id_AdeDet == ad.Id_AdeDet).ToList()[0].Requerido;
                    if (ad.Valor == "" && addenda_Requerida)
                    {
                        AlertaFocus("El campo <b>" + ad.Campo + "</b> de la addenda es requerido", txtAdenda.ClientID);
                        //RadTabStrip1.Tabs[1].Selected = true;
                        RadTabStrip1.Tabs[2].Selected = true;
                        rpvAdendaFacturacion.Selected = true;
                        return;
                    }
                    else
                        listAdendaCabecera.Add(ad);
                }

                for (int i = 0; i < rgAdendaReFacturacion.MasterTableView.Items.Count; i++)
                {
                    ad = new AdendaDet();
                    ad.Tipo = 7;
                    ad.Id_AdeDet = Convert.ToInt32(rgAdendaReFacturacion.MasterTableView.Items[i]["Id_AdeDet"].Text);
                    ad.Campo = rgAdendaReFacturacion.MasterTableView.Items[i]["campo"].Text;
                    ad.Nodo = rgAdendaReFacturacion.MasterTableView.Items[i]["nodo"].Text;
                    txtAdenda = rgAdendaReFacturacion.MasterTableView.Items[i]["valor"].FindControl("RadTextBox1") as RadTextBox;
                    ad.Valor = txtAdenda.Text.Replace("'", "").Trim();
                    bool addenda_Requerida = ListCabRF.Where(AdendaDet => AdendaDet.Id_AdeDet == ad.Id_AdeDet).ToList()[0].Requerido;
                    if (ad.Valor == "" && addenda_Requerida)
                    {
                        AlertaFocus("El campo <b>" + ad.Campo + "</b> de la addenda es requerido", txtAdenda.ClientID);
                        //RadTabStrip1.Tabs[2].Selected = true;
                        RadTabStrip1.Tabs[3].Selected = true;
                        rpvAdendaRefacturacion.Selected = true;
                        return;
                    }
                    else
                        listAdendaCabecera.Add(ad);
                }
                     
                //VALIDA SI HAY CAMPOS REQUERIDOS DE DETALLES GUARDAR AL MENOS UN DETALLE DE ADENDA
                string requeridodetalle = "NO";
                if (ListDet.Count > 0 || ListDetRF.Count > 0)
                {
                        foreach (AdendaDet det in ListDet)
                        {
                            if (det.Requerido)
                            { requeridodetalle = "SI"; }
                        }
                        if (ListDetRF != null)
                        {
                            foreach (AdendaDet det in ListDetRF)
                            {
                                if (det.Requerido)
                                { requeridodetalle = "SI"; }
                            }
                        }

                        if (requeridodetalle == "SI")
                        {
                            if (rgFacturaDetAde.Items.Count == 0)
                            {
                                Alerta("Capture al menos un Detalle de Adenda para guardar la factura");
                                RadTabStrip1.Tabs[2].Selected = true;
                                rpvAdendaFacturacion.Selected = true;
                                return;
                            }
                        }                       
                    }
                    
                foreach (DataRow dr in ListaProductosFacturaAdenda.Rows)
                {
                    foreach (AdendaDet det in ListDet)
                    {                        
                        if (dr[det.Id_AdeDet.ToString()] != null && dr[det.Id_AdeDet.ToString()].ToString().Trim() == "" && det.Requerido)
                        {
                            Alerta("El campo <b>" + det.Campo + "</b> de la addenda es requerido");
                            //RadTabStrip1.Tabs[3].Selected = true;
                            RadTabStrip1.Tabs[2].Selected = true;
                            RadPageViewDetalles.Selected = true;
                            return;
                        }
                    }
                    if (ListDetRF != null)
                    {
                        foreach (AdendaDet det in ListDetRF)
                        {
                            if (dr[det.Id_AdeDet.ToString()] != null && dr[det.Id_AdeDet.ToString()].ToString().Trim() == "" && det.Requerido)
                            {
                                Alerta("El campo <b>" + det.Campo + "</b> de la addenda es requerido");
                                //RadTabStrip1.Tabs[3].Selected = true;
                                RadTabStrip1.Tabs[2].Selected = true;
                                RadPageViewDetalles.Selected = true;
                                return;
                            }
                        }
                    }
                }

                //VALIDAMOS EL SUBTOTAL DE LA FACTURA CON LA ADENDA
                bool calcularAdendaTrue2 = false;
                bool subtotalesIguales2 = false;
                CalcularAdenda(out calcularAdendaTrue2, out subtotalesIguales2);

                if (calcularAdendaTrue2 == true)
                {
                    if (subtotalesIguales2 != true)
                    {
                        Alerta("El Subtotal de la Addenda no coincide con el subtotal de la Factura!");
                        RadTabStrip1.Tabs[2].Selected = true;
                        rpvAdendaFacturacion.Selected = true;
                        return;
                    }
                }

                #endregion
                // Evita que se guarde el documento si los totales no coinciden
                #region validacion
                if (Session["ListaProductosFacturaEspecial" + Session.SessionID] != null)
                {
                    if (Session["FacEspecialGuardada" + Session.SessionID].ToString() == "1")
                    {
                        double totalEspecial = 0;
                        foreach (FacturaDet ncd in (List<FacturaDet>)Session["ListaProductosFacturaEspecial" + Session.SessionID])
                        {
                            totalEspecial += ncd.Fac_Importe;
                        }

                        // se agregaron las variables TE1 y TE2 para para definir la diferencia maximma y la diferencia minima que puede tener la factura
                        // Se indico que solo podía haber diferecia de 70 centavos
                        double TE1 = Math.Round(totalEspecial, 2) + .90; // se suman 70 centavos al total especial -- modificar si se desea disminuir o aumentar el rango
                        double TE2 = Math.Round(totalEspecial, 2) - .90; // se restan 70 centavos al total especia


                        // se cambia la validacion ahora solo se valida que el total de la factura este dentro del rango permitido

                        if (Math.Round(txtImporte.Value.Value, 2) <= TE2 && Math.Round(txtImporte.Value.Value, 2) >= TE1)
                        {
                            Alerta("El total del documento especial no es igual al total del documento original");// si la diferencia es de mas menos 70 centavos muestra el mensaje
                            return;
                        }
                    }
                }

                #endregion

                #region arrayRemisiones
                if (string.IsNullOrEmpty(arrayRemisiones))
                {
                    List<Remision> listaRemisiones;
                    if (Session["ListaRemisionesFactura" + Session.SessionID] != null)
                    {
                        listaRemisiones = (List<Remision>)Session["ListaRemisionesFactura" + Session.SessionID];
                    }
                    else
                    {
                        CN_CapRemision remision = new CN_CapRemision();
                        listaRemisiones = new List<Remision>();
                        remision.ConsultaRemisionesxFactura(session, Convert.ToInt32(txtId.Text), ref listaRemisiones);
                    }
                    foreach (Remision rem in listaRemisiones)
                    {
                        arrayRemisiones = string.Concat(arrayRemisiones, rem.Id_Rem.ToString(), "|");
                    }
                    if (arrayRemisiones.Length > 1)
                        arrayRemisiones = arrayRemisiones.Substring(0, arrayRemisiones.Length - 1);
                    Session["ListaRemisionesFactura" + Session.SessionID] = null;
                }

                //-- facturar pedido que tiene remision auto
                if (string.IsNullOrEmpty(arrayRemisiones))
                {
                    if (txtPedido.Text != "")
                    {
                        CN_CapRemision remision = new CN_CapRemision();
                        List<Remision> listaRemisiones = new List<Remision>();
                        remision.ConsultaRemisionesxPedido(session, Convert.ToInt32(txtPedido.Text), ref listaRemisiones);
                        foreach (Remision rem in listaRemisiones)
                        {
                            arrayRemisiones = string.Concat(arrayRemisiones, rem.Id_Rem.ToString(), "|");
                        }
                        if (arrayRemisiones.Length > 1)
                            arrayRemisiones = arrayRemisiones.Substring(0, arrayRemisiones.Length - 1);
                    }
                }
                #endregion
                //***--------------------------------------***
                //***          GUARDAR FACTURA             ***
                //***--------------------------------------***
                int verificador = 0;
                string mensaje = string.Empty;
                //List<FacturaDet> listaFacturaDetalle = this.ListaProductosFactura;
                DataTable listaFacturaDetalle = ListaProductosFactura;
                DataTable listaFacturaDetalleAdenda = ListaProductosFacturaAdenda;
                //----checar si se esta facturando un pedido-----
                int? pedidoPrevio = null;
                if (Session["PedidoRemisionado" + Session.SessionID] != null)
                    Session["PedidoRemisionado" + Session.SessionID] = null;
                else
                {
                    if (txtPedido.Text != string.Empty)
                        pedidoPrevio = Convert.ToInt32(txtPedido.Text);
                }
                //int columnas_RF = (ListDetRF != null ? ListDetRF.Count : 0) / (ListaProductosFactura != null ? ListaProductosFactura.Rows.Count : 1);

                int dividendo = 0;
                int divisor = 0;

                if (ListDetRF != null)
                {
                    dividendo = ListDetRF.Count;
                }

                if (ListaProductosFactura != null)
                {
                    divisor = ListaProductosFactura.Rows.Count;
                }
                else
                {
                    divisor = 1;
                }

                int columnas_RF = divisor == 0 ? 0 : dividendo / divisor;

                if (_PermisoGuardar)
                {// NUEVA FACTURA    



                    #region Factura Especial
                    //Guardar los datos de los productos de factura especial
                    //en catálogo de Cli ente-Producto                    

                    List<FacturaDet> listaProductosFacturaEspecial = new List<FacturaDet>();
                    FacturaEspecial facturaEsp = null;

                    if (Session["ListaProductosFacturaEspecial" + Session.SessionID] != null)
                    {
                        if (Session["FacEspecialGuardada" + Session.SessionID].ToString() == "1") //guarda solo si hizo clic en guardar en pantalla de facturaEspecial.
                        {
                            facturaEsp = new FacturaEspecial();
                            facturaEsp.Id_Emp = factura.Id_Emp;
                            facturaEsp.Id_Cd = factura.Id_Cd;
                            facturaEsp.Id_Fac = factura.Id_Fac;
                            facturaEsp.Id_Ter = Convert.ToInt32(factura.Id_Ter);
                            facturaEsp.FacEsp_Fecha = factura.Fac_Fecha;
                            facturaEsp.FacEsp_Importe = Convert.ToDouble(factura.Fac_Importe);
                            facturaEsp.FacEsp_SubTotal = Convert.ToDouble(factura.Fac_SubTotal);
                            facturaEsp.FacEsp_ImporteIva = Convert.ToDouble(factura.Fac_ImporteIva);
                            facturaEsp.FacEsp_Total = Convert.ToDouble(factura.Fac_Importe);

                           listaProductosFacturaEspecial = (List<FacturaDet>)Session["ListaProductosFacturaEspecial" + Session.SessionID];
                           // new CN_CapFactura().ModificarFacturaEspecial(ref facturaEsp, ref listaProductosFacturaEspecial, session.Emp_Cnx, ref verificador, string.IsNullOrEmpty(this.hiddenId.Value) ? false : true);
                        }
                    }
                    #endregion


                    #region nueva factura
                    if (this.hiddenId.Value == string.Empty)
                    {
                        new CN_CapFactura().InsertarFactura(session, ref factura, ref listaFacturaDetalle, ref listaFacturaDetalleAdenda, columnas_RF, session.Emp_Cnx, ref verificador, ref pedidoPrevio, ref listaEntSalRemisiones, listAdendaCabecera, HiddenIdRF.Value, arrayRemisiones, ref entSal, ref listaEntSal, ref facturaEsp, ref listaProductosFacturaEspecial, string.IsNullOrEmpty(this.hiddenId.Value) ? false : true);
                        if (verificador == -2)
                        {
                            Alerta("No se pudo insertar la factura, favor de revisar el tipo de remisión a facturar porque no tiene tipo de movimiento inverso");
                            return;
                        }
                        new CN_Rendimientos().InsertarRendimientos(session, session.Emp_Cnx, Session.SessionID, ref factura, ref verificador);
                        mensaje = "Se creó correctamente la factura <b>#" + factura.Id_Fac.ToString() + "</b>";
                    }
                    #endregion
                    // ACTUALIZAR FACTURA                   
                    #region Actualizar factura
                    else
                    {
                        if (txtMov.Text == "70")
                        {
                            entSal.Es_Referencia = factura.Id_Fac.ToString(); //referencia a la factura
                            entSal.Es_Notas = entSal.Es_Notas.Replace("#Id_Fac#", factura.Id_Fac.ToString());
                            if (this.hiddenId_Es.Value != string.Empty)
                                entSal.Id_Es = Convert.ToInt32(this.hiddenId_Es.Value);
                            else
                                throw new Exception("CapFactura_Id_Es_NoEncontrado");
                        }
                        new CN_CapFactura().ModificarFactura(session, ref factura
                            , ref listaFacturaDetalle
                            , ref listaFacturaDetalleAdenda
                            , columnas_RF
                            , session.Emp_Cnx
                            , ref verificador
                            , ref pedidoPrevio
                            , ref listaEntSalRemisiones,
                            listAdendaCabecera,
                            arrayRemisiones, ref entSal, ref listaEntSal, ref facturaEsp, ref listaProductosFacturaEspecial, string.IsNullOrEmpty(this.hiddenId.Value) ? false : true);

                        
                        new CN_Rendimientos().InsertarRendimientos(session, session.Emp_Cnx, Session.SessionID, ref factura, ref verificador);
                        
                        mensaje = "Los datos se modificaron correctamente";
                    }
                    #endregion
                }
                //SI GUARDÓ BIEN LA FACTURA:

                           



                if (HF_VI.Value == "true")
                {
                    Session["PreguntarImpresionVI" + Session.SessionID] = factura.Id_Fac;
                    Session["PreguntarImpresionVISerie" + Session.SessionID] = factura.Id_FacSerie + factura.Id_Fac ;
                }
                RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void validarDetalleInventario(DataTable listaFacturaDetalle)
        {
            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                foreach (DataRow dr in listaFacturaDetalle.Rows)
                {
                    if (txtPedido.Text != "")
                    {
                        int validador = 0;
                        int cantidadDetalle = Convert.ToInt32(dr["Fac_Cant"]);
                        int producto = Convert.ToInt32(dr["Id_Prd"].ToString());
                        int pedido = !string.IsNullOrEmpty(txtPedido.Text) ? Convert.ToInt32(txtPedido.Text) : -1;
                        new CN_CapFactura().ValidarDisponibilidadInventario(session, cantidadDetalle, producto, pedido, ref validador);

                        if (validador == 0)
                        {
                            throw new Exception("No hay cantidad suficiente en el inventario para facturar el producto # " + producto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ConsultarDatosRemisiones()
        {
            try
            {
                if (Session["ListaRemisionesFactura" + Session.SessionID] != null)
                {
                    Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                    List<Remision> listaRemisiones = (List<Remision>)Session["ListaRemisionesFactura" + Session.SessionID];
                    //----------------------------
                    // llenar datos de factura
                    //----------------------------              
                    if (listaRemisiones.Count > 0)
                    {
                        if (this.ConsultarDatosCliente(listaRemisiones[0].Id_Cte.ToString(), false)) //si la consulta de datos del cliente es correcta
                        {
                            txtCliente.Text = listaRemisiones[0].Id_Cte.ToString();
                            if (!string.IsNullOrEmpty(listaRemisiones[0].Cte_NomComercial))
                                txtClienteNombre.Text = listaRemisiones[0].Cte_NomComercial;
                            else
                                txtClienteNombre.Text = listaRemisiones[0].NombreCliente;
                        }
                        //---------------------------------------------------------------------------------------
                        // Consulta partidas de la remisones y las pasa a partidas de detalle de factura
                        //---------------------------------------------------------------------------------------
                        DataTable listaFacturaDet = new DataTable();
                        InicializarTablaProductosRemisiones(ref listaFacturaDet);
                        //List<FacturaDet> listaFactura = new List<FacturaDet>();
                        Remision remision = new Remision();

                        this.ListaProductosFactura.Clear();
                        //obtener string de remisiones
                        foreach (Remision rem in listaRemisiones)
                        {
                            arrayRemisiones = string.Concat(arrayRemisiones, rem.Id_Rem.ToString(), "|");
                        }
                        if (arrayRemisiones.Length > 1)
                            arrayRemisiones = arrayRemisiones.Substring(0, arrayRemisiones.Length - 1);
                        remision.Id_Emp = session.Id_Emp;
                        remision.Id_Cd = session.Id_Cd_Ver;

                        new CN_CapRemision().ConsultaRemisionDetalleFacturacion(ref remision, ref listaFacturaDet, arrayRemisiones, session.Emp_Cnx);
                        this.ListaProductosFactura = listaFacturaDet;
                        rgFacturaDet.Rebind();
                        this.CalcularTotales();

                        if ((txtCliente.Text != string.Empty)
                            && (this.cmbMov.SelectedValue != "-1" && this.cmbMov.SelectedValue != string.Empty)
                            && (this.cmbTerritorio.SelectedValue != "-1" && this.cmbTerritorio.SelectedValue != string.Empty))
                        {
                            this.rgFacturaDet.Enabled = true;
                            this.rgFacturaDetAde.Enabled = true;
                            this.btnFacturaEspecial.Enabled = true;
                            this.rgAdendaFacturacion.Enabled = true;
                        }
                        else
                        {
                            this.rgFacturaDet.Enabled = false;
                            this.rgFacturaDetAde.Enabled = false;
                            this.rgAdendaFacturacion.Enabled = false;
                            this.btnFacturaEspecial.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConsultarDatosPedido()
        {
            try
            {
                if (Session["PedidoFacturacion" + Session.SessionID] != null)
                {
                    if (Session["PedidoVI" + Session.SessionID] != null)
                    {
                        Session["PedidoVI" + Session.SessionID] = null;
                        HF_VI.Value = "true";
                    }
                    Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                    DataTable dtPedido = new DataTable();
                    Pedido pedido = new Pedido();
                    List<Pedido> listaPedido = new List<Pedido>();
                    int pedidoFacturacion = Convert.ToInt32(Session["PedidoFacturacion" + Session.SessionID]);
                    pedido.Filtro_Doc = "F";
                    pedido.Id_Emp = session.Id_Emp;
                    pedido.Id_Cd = session.Id_Cd_Ver;
                    pedido.Id_Ped = pedidoFacturacion;
                    new CN_CapPedido().ConsultaPedido(ref pedido, session.Emp_Cnx);

                    txtPedido.Text = pedido.Id_Ped.ToString();
                    if (pedido.Ped_Tipo == 3 || pedido.Ped_Tipo == 4)//si es pedido captado
                        if (!string.IsNullOrEmpty(pedido.Requisicion))
                            txtReq.Text = pedido.Requisicion;
                        else
                            txtReq.Text = pedido.Ped_OrdenCompra.ToString();
                    else//si pedido es normal, va en orden de entrega
                    {
                        txtOrden.Text = pedido.Ped_OrdenEntrega;
                        txtReq.Text = pedido.Requisicion;
                    }
                    txtNotas.Text = pedido.Ped_Comentarios;
                    //txtReq.Text = pedido.Ped_OrdenCompra.ToString();
                    txtDescuento1.Text = pedido.Ped_DescPorcen1.ToString();
                    txtDescuento2.Text = pedido.Ped_DescPorcen2.ToString();
                    txtDescPorc1.Text = pedido.Ped_Desc1 == string.Empty ? "descto" : pedido.Ped_Desc1.ToString();
                    txtDescPorc2.Text = pedido.Ped_Desc2 == string.Empty ? "descto" : pedido.Ped_Desc2.ToString();
                    txtCondiciones.Text = pedido.Ped_CondEntrega.ToString();
                    txtConducto.Text = pedido.Ped_Solicito;


                    if (this.ConsultarDatosCliente(pedido.Id_Cte.ToString(), false))
                    {//si la consulta de datos del cliente es correcta
                        txtCliente.Text = pedido.Id_Cte.ToString();
                        txtClienteNombre.Text = pedido.Cte_NomComercial;
                    }

                    if (pedido.Ped_Tipo == 3 || pedido.Ped_Tipo == 4)
                    {
                        txtCalle.Text = pedido.Ped_ConsignadoCalle;
                        txtCalleNumero.Text = pedido.Ped_ConsignadoNo;
                        txtCP.Text = pedido.Ped_ConsignadoCp;
                        txtColonia.Text = pedido.Ped_ConsignadoColonia;
                        txtMunicipio.Text = pedido.Ped_ConsignadoMunicipio;
                        txtEstado.Text = pedido.Ped_ConsignadoEstado;
                    }

                    txtNotas.Text = pedido.Ped_Comentarios;
                    cmbTerritorio.SelectedIndex = cmbTerritorio.FindItemIndexByValue(pedido.Id_Ter.ToString());

                    txtTerritorio.Text = pedido.Id_Ter.ToString();
                    cmbTerritorio_SelectedIndexChanged(cmbTerritorio, null);
                    //-------------------------------
                    // Consulta partidas del pedido
                    //-------------------------------
                    dtPedido.Columns.Add("Id_PedDet", typeof(int));
                    dtPedido.Columns.Add("Id_Ter", typeof(int));
                    dtPedido.Columns.Add("Ter_Nombre", typeof(string));
                    dtPedido.Columns.Add("Id_Prd", typeof(int));
                    dtPedido.Columns.Add("Prd_Descripcion", typeof(string));
                    dtPedido.Columns.Add("Prd_Presentacion", typeof(string));
                    dtPedido.Columns.Add("Prd_Unidad", typeof(string));
                    dtPedido.Columns.Add("Prd_Precio", typeof(double));
                    dtPedido.Columns.Add("Disponible", typeof(int));
                    dtPedido.Columns.Add("Prd_Importe", typeof(double));
                    dtPedido.Columns.Add("Id_Rem", typeof(int));
                    new CN_CapPedido().ConsultaPedidoDetDisp(pedido, ref dtPedido, 1, session.Emp_Cnx);

                    DataRowCollection colRow = dtPedido.Rows;
                    int z = 0;
                    foreach (DataRow row in colRow)
                    {

                        int Id_Ter = 0;
                        string Id_TerStr = string.Empty;
                        int Id_Prd = 0;
                        string Prd_Descripcion = string.Empty;
                        string Prd_Presentacion = string.Empty;
                        string Prd_UniNe = string.Empty;
                        int Fac_Cant = 0;
                        int Rem_Cant = 0;
                        int id_Rem = 0;
                        double Fac_Precio = 0;
                        int Id_CteExt = 0;
                        string Id_CteExtStr = string.Empty;

                        if (row["Id_Ter"] != null)
                            Id_Ter = row["Id_Ter"].ToString() == string.Empty ? -1 : Convert.ToInt32(row["Id_Ter"]);

                        Id_TerStr = Convert.IsDBNull(row["Ter_Nombre"]) ? string.Empty : row["Ter_Nombre"].ToString();

                        if (row["Id_Prd"] != null)
                            Id_Prd = row["Id_Prd"].ToString() == string.Empty ? -1 : Convert.ToInt32(row["Id_Prd"]);

                        if (row["Prd_Descripcion"] != null)
                            Prd_Descripcion = row["Prd_Descripcion"].ToString() == string.Empty ? string.Empty : row["Prd_Descripcion"].ToString();

                        if (row["Prd_Presentacion"] != null)
                            Prd_Presentacion = row["Prd_Presentacion"].ToString() == string.Empty ? string.Empty : row["Prd_Presentacion"].ToString();

                        if (row["Prd_Unidad"] != null)
                            Prd_UniNe = row["Prd_Unidad"].ToString() == string.Empty ? string.Empty : row["Prd_Unidad"].ToString();

                        if (row["Prd_Precio"] != null)
                            Fac_Precio = row["Prd_Precio"].ToString() == string.Empty ? 0 : Convert.ToDouble(row["Prd_Precio"]);

                        if (row["Disponible"] != null)
                            Fac_Cant = row["Disponible"].ToString() == string.Empty ? 0 : Convert.ToInt32(row["Disponible"]);

                        if (row["Id_Rem"] != null)
                            id_Rem = row["Id_Rem"].ToString() == string.Empty ? 0 : Convert.ToInt32(row["Id_Rem"]);

                        Id_CteExt = 0;
                        Id_CteExtStr = string.Empty;
                        ListaProductosFactura.Rows.Add(new object[] { null, z, id_Rem, null, Id_CteExt, Id_Ter, Id_Prd, Prd_Descripcion, Prd_Presentacion, Prd_UniNe, Fac_Cant, Rem_Cant, Fac_Precio, Fac_Precio * (Fac_Cant + Rem_Cant), Id_TerStr, Id_CteExtStr, null, session.Id_Emp, session.Id_Cd_Ver });
                        z++;
                    }
                    this.rgFacturaDet.Enabled = true;

                    this.CalcularTotales();
                    //deshabilitar campos que no se deben de editar en una facturación de pedido
                    this.HabilitarCamposPedido(false);

                    if ((txtCliente.Text != string.Empty)
                            && (this.cmbMov.SelectedValue != "-1" && this.cmbMov.SelectedValue != string.Empty)
                            && (this.cmbTerritorio.SelectedValue != "-1" && this.cmbTerritorio.SelectedValue != string.Empty))
                    {
                        this.rgFacturaDet.Enabled = true;
                        this.btnFacturaEspecial.Enabled = true;
                        this.rgFacturaDet.Enabled = true;
                        this.rgAdendaFacturacion.Enabled = true;
                    }
                    else
                    {
                        this.rgFacturaDet.Enabled = false;
                        this.btnFacturaEspecial.Enabled = false;
                        this.rgFacturaDetAde.Enabled = false;
                        this.rgAdendaFacturacion.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool ObtenerConsecutivoFactElectronica(int id_Cfe)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int consecutivo = 0;

                txtId.Text = string.Empty;
                if (cmbConsFacEle.SelectedValue != "-1")
                {
                    new CN__Comun().ConsultaFactura_ConsecutivoFacElectronica(
                        sesion.Id_Emp
                        , sesion.Id_Cd_Ver
                        , id_Cfe
                        , 1 // 1 = factura, 2 = nota de cargo, 3 = nota de credito
                        , ref consecutivo
                        , sesion.Emp_Cnx);
                    txtId.Text = consecutivo.ToString();
                }
                return true;
            }
            catch (Exception ex)
            {
                txtId.Text = string.Empty;
                cmbConsFacEle.SelectedIndex = 0;
                cmbConsFacEle.SelectedValue = "-1";
                cmbConsFacEle.Text = "-- Seleccionar --";
                return false;
                throw ex;
            }
        }
        private void InicializarTablaProductos()
        {
            try
            {
                ListaProductosFactura = new DataTable();
                ListaProductosFactura.Columns.Add("Id_Fac");
                ListaProductosFactura.Columns.Add("Id_FacDet");
                ListaProductosFactura.Columns.Add("Id_Rem");
                ListaProductosFactura.Columns.Add("Id_Tm");
                ListaProductosFactura.Columns.Add("Id_CteExt");
                ListaProductosFactura.Columns.Add("Id_Ter");
                ListaProductosFactura.Columns.Add("Id_Prd", typeof(System.Int32));
                ListaProductosFactura.Columns.Add("Prd_Descripcion");
                ListaProductosFactura.Columns.Add("Prd_Presentacion");
                ListaProductosFactura.Columns.Add("Prd_UniNe");
                ListaProductosFactura.Columns.Add("Fac_Cant");
                ListaProductosFactura.Columns.Add("Rem_Cant");
                ListaProductosFactura.Columns.Add("Fac_Precio", typeof(System.Double));
                ListaProductosFactura.Columns.Add("Fac_Importe", typeof(System.Double));
                ListaProductosFactura.Columns.Add("Id_TerStr");
                ListaProductosFactura.Columns.Add("Id_CteExtStr");
                ListaProductosFactura.Columns.Add("AmortizacionProducto");
                ListaProductosFactura.Columns.Add("Id_Emp");
                ListaProductosFactura.Columns.Add("Id_Cd");
                ListaProductosFactura.Columns.Add("Fac_Asignar");
                ListaProductosFactura.Columns.Add("Fac_Devolucion");
                ListaProductosFactura.Columns.Add("Prd_UniNs");

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InicializarTablaDetallesAdenda()
        {
            try
            {               
                ListaProductosFacturaAdenda = new DataTable();
                ListaProductosFacturaAdenda.Columns.Add("Id_Cons_AdeDet");
                ListaProductosFacturaAdenda.Columns.Add("Id_Prd");
                ListaProductosFacturaAdenda.Columns.Add("Prd_Descripcion");


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InicializarTablaProductosRemisiones(ref DataTable remisiones)
        {
            try
            {
                remisiones = new DataTable();
                remisiones.Columns.Add("Id_Fac");
                remisiones.Columns.Add("Id_FacDet");
                remisiones.Columns.Add("Id_Rem");
                remisiones.Columns.Add("Id_Tm");
                remisiones.Columns.Add("Id_CteExt");
                remisiones.Columns.Add("Id_Ter");
                remisiones.Columns.Add("Id_Prd");
                remisiones.Columns.Add("Prd_Descripcion");
                remisiones.Columns.Add("Prd_Presentacion");
                remisiones.Columns.Add("Prd_UniNe");
                remisiones.Columns.Add("Fac_Cant");
                remisiones.Columns.Add("Rem_Cant");
                remisiones.Columns.Add("Fac_Precio", typeof(System.Double));
                remisiones.Columns.Add("Fac_Importe", typeof(System.Double));
                remisiones.Columns.Add("Id_TerStr");
                remisiones.Columns.Add("Id_CteExtStr");
                remisiones.Columns.Add("AmortizacionProducto");
                remisiones.Columns.Add("Id_Emp");
                remisiones.Columns.Add("Id_Cd");
                remisiones.Columns.Add("Fac_Asignar");
                remisiones.Columns.Add("Fac_Devolucion");
                remisiones.Columns.Add("Prd_UniNs");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool ConsultarDatosCliente(string idCliente, bool modificar)
        {
            try
            {
                bool datosClienteEstablecidos = false;
                bool proveedorNoSeleccionado = false;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (idCliente != string.Empty && idCliente != "-1")
                { //Consultar clientes
                    Clientes cliente = new Clientes();
                    cliente.Id_Emp = sesion.Id_Emp;
                    cliente.Id_Cd = sesion.Id_Cd_Ver;
                    cliente.Id_Rik = sesion.Id_Rik;
                    cliente.Id_Cte = Convert.ToInt32(idCliente);
                    try
                    {
                        bool facVI = false;
                        facVI = !string.IsNullOrEmpty(HF_VI.Value) ? Convert.ToBoolean(HF_VI.Value) : false;
                        new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);
                        CN_PrecioEspecial clsCN_CapPrecioEspecial = new CN_PrecioEspecial();
                         PrecioEspecial PrecioEspecial = new PrecioEspecial();
                         PrecioEspecial.Id_Emp = sesion.Id_Emp;
                         PrecioEspecial.Id_Cd = sesion.Id_Cd_Ver;
                         PrecioEspecial.Id_Cte = cliente.Id_Cte;
                        int valor = 0;
                        int result = 0;
                        clsCN_CapPrecioEspecial.ConsultaProveedorSeleccionado(PrecioEspecial, sesion.Emp_Cnx, ref valor, ref proveedorNoSeleccionado);

                    }
                    catch (Exception ex)
                    {
                        InicializarTablaProductos();
                        AlertaFocus(ex.Message, txtCliente.ClientID);
                        txtCliente.Text = "";
                        txtClienteNombre.Text = "";
                        txtRepresentante.Text = "";
                        CargarComboTerritorios();
                        txtTerritorio.Text = "";
                        txtContacto.Text = string.Empty;
                        txtCalle.Text = string.Empty;
                        txtCalleNumero.Text = string.Empty;
                        txtCP.Text = string.Empty;
                        txtColonia.Text = string.Empty;
                        txtMunicipio.Text = string.Empty;
                        txtEstado.Text = string.Empty;
                        txtRFC.Text = string.Empty;
                        txtTelefono.Text = string.Empty;
                        chkDesgloce.Checked = false;
                        chkRetencion.Checked = false;
                        txtCondiciones.Text = string.Empty;
                        txtMoneda.Text = string.Empty;
                        txtPorcRetencion.Text = string.Empty;
                        txtUDigitos.Text = "";
                        cmbMoneda.SelectedIndex = cmbMoneda.FindItemIndexByValue("-1");
                        return false;
                    }

                    if (cliente.Cte_CreditoSuspendido ||  proveedorNoSeleccionado)
                    {
                        InicializarTablaProductos();

                        txtCliente.Text = "";
                        txtClienteNombre.Text = "";
                        txtRepresentante.Text = "";
                        CargarComboTerritorios();
                        txtTerritorio.Text = "";
                        txtContacto.Text = string.Empty;
                        txtCalle.Text = string.Empty;
                        txtCalleNumero.Text = string.Empty;
                        txtCP.Text = string.Empty;
                        txtColonia.Text = string.Empty;
                        txtMunicipio.Text = string.Empty;
                        txtEstado.Text = string.Empty;
                        txtRFC.Text = string.Empty;
                        txtTelefono.Text = string.Empty;
                        chkDesgloce.Checked = false;
                        chkRetencion.Checked = false;
                        txtCondiciones.Text = string.Empty;
                        txtMoneda.Text = string.Empty;
                        txtPorcRetencion.Text = string.Empty;
                        txtUDigitos.Text = "";
                        cmbMoneda.SelectedIndex = cmbMoneda.FindItemIndexByValue("-1");

                        if(!proveedorNoSeleccionado)
                        {
                            if (cliente.Cte_CreditoSuspendido)
                            {
                                AlertaFocus(generar_motivo(cliente.Cte_MotCreditoSuspendido), txtCliente.ClientID);
                            }
                        } else{
                            AlertaFocus("Existen Solicitudes de Precios Especiales para este cliente sin un Proveedor seleccionado ó No se ha registrado el Número de Usuario para el proveedor Georgia Pacific, favor ir al módulo de solicitud de precios especiales  seleccionar un proveedor y registrar numero de usuario según sea el caso para cada solicitud autorizada no vencida", txtCliente.ClientID);
                           // Response.Redirect("PrecioEspecial_Admin.aspx?&Id_Cte=" + cliente.Id_Cte );
                        }
                        return false;
                    }

                    txtUDigitos.Text = cliente.Cte_UDigitos;
                    txtClienteNombre.Text = cliente.Cte_NomComercial;
                    txtCliente.Value = cliente.Id_Cte == null ? 0 : cliente.Id_Cte;


                    //Consultar territorios relacionados con el cliente
                    List<Territorios> listaTerritorios = new List<Territorios>();
                    this.CargarComboTerritorios();
                    CargarFormaPago();
                    if (!modificar)
                    {
                        if (HF_VI.Value != "true" && Session["PedidoFacturacion" + Session.SessionID] != null)
                        {
                            txtCalle.Text = txtCalle.Text == "" ? cliente.Cte_Calle : txtCalle.Text;
                            txtCalleNumero.Text = txtCalleNumero.Text == "" ? cliente.Cte_Numero : txtCalleNumero.Text;
                            txtCP.Text = txtCP.Text == "" ? cliente.Cte_Cp : txtCP.Text;
                            txtColonia.Text = txtColonia.Text == "" ? cliente.Cte_Colonia : txtColonia.Text;
                            txtMunicipio.Text = txtMunicipio.Text == "" ? cliente.Cte_Municipio : txtMunicipio.Text;
                            txtEstado.Text = txtEstado.Text == "" ? cliente.Cte_Estado : txtEstado.Text;
                        }
                        InicializarTablaProductos();
                        InicializarTablaDetallesAdenda();
                        List<AdendaDet> listCabT = new List<AdendaDet>();
                        List<AdendaDet> listDetT = new List<AdendaDet>();
                        List<AdendaDet> listCabR = new List<AdendaDet>();
                        listCabR = new List<AdendaDet>();
                        ListCabRF = new List<AdendaDet>();
                        ListDet = new List<AdendaDet>();
                        ListDetRF = new List<AdendaDet>();

                        //if (rgFacturaDetAde.Columns.Count > 17)
                        //    for (int i = rgFacturaDetAde.Columns.Count; i > 17; i--)
                        //        rgFacturaDetAde.Columns.RemoveAt(rgFacturaDetAde.Columns.Count - 1);

                        new CN_CatCliente().ConsultarAdenda(sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(idCliente), "1,2", ref listCabT, ref listDetT, ref listCabR, sesion.Emp_Cnx);


                        //new CN_CapFactura().ConsultarAdenda(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Fac, "7", "8", ref listCabR, ref listDetR, sesion.Emp_Cnx);

                        if (listCabT.Count > 0)
                        {
                            RadTabStrip1.Tabs[2].Visible = true;
                            ListCab = listCabT;                           
                            rgAdendaFacturacion.Rebind();
                            //rgFacturaDetAde.Rebind();
                        }
                        else
                        {
                            RadTabStrip1.Tabs[2].Visible = false;
                            ListCab = listCabT;
                            rgAdendaFacturacion.Rebind();
                            //rgFacturaDetAde.Rebind();
                        }

                        if (listCabR.Count > 0)
                        {
                            RadTabStrip1.Tabs[3].Visible = true;
                            ListCabRF = listCabR;
                            rgAdendaReFacturacion.Rebind();
                           
                        }
                        else
                        {
                            RadTabStrip1.Tabs[3].Visible = false;
                            rgAdendaReFacturacion.Rebind();
                            //rgFacturaDetAde.Rebind();
                            //rgFacturaDet.Rebind();
                        }

                        GridBoundColumn boundColumn1;                       
                                                
                        foreach (AdendaDet ad in listDetT)
                        {
                            boundColumn1 = new GridBoundColumn();
                            rgFacturaDetAde.MasterTableView.Columns.Add(boundColumn1);
                            boundColumn1.DataField = ad.Id_AdeDet.ToString();
                            boundColumn1.UniqueName = ad.Id_AdeDet.ToString();
                            boundColumn1.HeaderText = ad.Campo;
                            boundColumn1.HeaderStyle.Width = Unit.Pixel(150);
                            boundColumn1.MaxLength = ad.Longitud;
                            ListaProductosFacturaAdenda.Columns.Add(ad.Id_AdeDet.ToString());
                        }
                        ListDet = listDetT;


                        //CREA BOTON DE EDITAR

                        //CREA BOTON DE EDITAR
                        try
                        {
                            rgFacturaDetAde.Columns.Remove(rgFacturaDetAde.Columns.FindByUniqueName("EditCommandColumn"));
                        }
                        catch (Exception)
                        {

                        }

                        GridEditCommandColumn commandColumnAde = new GridEditCommandColumn();
                        rgFacturaDetAde.MasterTableView.Columns.Add(commandColumnAde);

                        commandColumnAde.ButtonType = GridButtonColumnType.ImageButton;
                        commandColumnAde.UniqueName = "EditCommandColumn";
                        commandColumnAde.EditText = "Editar";
                        commandColumnAde.CancelText = "Cancelar";
                        commandColumnAde.InsertText = "Aceptar";
                        commandColumnAde.UpdateText = "Actualizar";
                        commandColumnAde.HeaderStyle.Width = Unit.Pixel(60);
                        commandColumnAde.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        commandColumnAde.ItemStyle.HorizontalAlign = HorizontalAlign.Center;


                        //CREA BOTON ELIMINAR     
                        //CREA BOTON ELIMINAR
                        try
                        {
                            rgFacturaDetAde.Columns.Remove(rgFacturaDetAde.Columns.FindByUniqueName("DeleteColumn"));
                        }
                        catch (Exception)
                        {

                        }
                        GridButtonColumn deleteColumnAde = new GridButtonColumn();
                        rgFacturaDetAde.MasterTableView.Columns.Add(deleteColumnAde);

                        deleteColumnAde.ConfirmText = "¿Desea quitar este producto de la lista?";
                        deleteColumnAde.ConfirmDialogHeight = Unit.Pixel(150);
                        deleteColumnAde.ConfirmDialogWidth = Unit.Pixel(350);
                        deleteColumnAde.ConfirmDialogType = GridConfirmDialogType.RadWindow;
                        deleteColumnAde.ButtonType = GridButtonColumnType.ImageButton;
                        deleteColumnAde.CommandName = "Delete";
                        deleteColumnAde.Text = "Eliminar";
                        deleteColumnAde.UniqueName = "DeleteColumn";
                        deleteColumnAde.HeaderStyle.Width = Unit.Pixel(50);
                        deleteColumnAde.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        deleteColumnAde.ItemStyle.HorizontalAlign = HorizontalAlign.Center;


                        double ancho2 = 0;
                        foreach (GridColumn gc in rgFacturaDetAde.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho2 = ancho2 + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgFacturaDetAde.Width = Unit.Pixel(Convert.ToInt32(ancho2));
                        rgFacturaDetAde.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho2));


                        //CREA BOTON DE EDITAR
                        try
                        {
                            rgFacturaDet.Columns.Remove(rgFacturaDet.Columns.FindByUniqueName("EditCommandColumn"));
                        }
                        catch (Exception)
                        {

                        }


                        GridEditCommandColumn commandColumn = new GridEditCommandColumn();
                        rgFacturaDet.MasterTableView.Columns.Add(commandColumn);
                        commandColumn.ButtonType = GridButtonColumnType.ImageButton;
                        commandColumn.UniqueName = "EditCommandColumn";
                        commandColumn.EditText = "Editar";
                        commandColumn.CancelText = "Cancelar";
                        commandColumn.InsertText = "Aceptar";
                        commandColumn.UpdateText = "Actualizar";
                        commandColumn.HeaderStyle.Width = Unit.Pixel(60);
                        commandColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        commandColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                        //CREA BOTON ELIMINAR
                        try
                        {
                            rgFacturaDet.Columns.Remove(rgFacturaDet.Columns.FindByUniqueName("DeleteColumn"));
                        }
                        catch (Exception)
                        {

                        }


                        GridButtonColumn deleteColumn = new GridButtonColumn();
                        rgFacturaDet.MasterTableView.Columns.Add(deleteColumn);
                        deleteColumn.ConfirmText = "¿Desea quitar este producto de la lista?";
                        deleteColumn.ConfirmDialogHeight = Unit.Pixel(150);
                        deleteColumn.ConfirmDialogWidth = Unit.Pixel(350);
                        deleteColumn.ConfirmDialogType = GridConfirmDialogType.RadWindow;
                        deleteColumn.ButtonType = GridButtonColumnType.ImageButton;
                        deleteColumn.CommandName = "Delete";
                        deleteColumn.Text = "Eliminar";
                        deleteColumn.UniqueName = "DeleteColumn";
                        deleteColumn.HeaderStyle.Width = Unit.Pixel(50);
                        deleteColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        deleteColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                        double ancho = 0;
                        foreach (GridColumn gc in rgFacturaDet.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgFacturaDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgFacturaDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    }
                    //Si el cliente no se le permite facturacion, manda mensaje y no permite continuar
                    if (!cliente.Cte_Facturacion)
                    {
                        this.DisplayMensajeAlerta("Cliente_NoPermiteFacturacion");
                        txtId.Text = string.Empty;
                        txtCliente.Text = string.Empty;
                        txtClienteNombre.Text = string.Empty;
                        cmbConsFacEle.SelectedIndex = cmbConsFacEle.FindItemIndexByValue("-1");
                        txtRepresentante.Text = string.Empty;
                        txtRepresentanteStr.Text = string.Empty;
                        txtTerritorio.Text = string.Empty;
                        cmbTerritorio.SelectedIndex = cmbTerritorio.FindItemIndexByValue("-1");

                        txtContacto.Text = string.Empty;
                        txtCalle.Text = string.Empty;
                        txtCalleNumero.Text = string.Empty;
                        txtCP.Text = string.Empty;
                        txtColonia.Text = string.Empty;
                        txtMunicipio.Text = string.Empty;
                        txtEstado.Text = string.Empty;
                        txtRFC.Text = string.Empty;
                        txtTelefono.Text = string.Empty;
                        chkDesgloce.Checked = false;
                        chkRetencion.Checked = false;
                        txtCondiciones.Text = string.Empty;
                        txtMoneda.Text = string.Empty;
                        txtPorcRetencion.Text = string.Empty;
                        cmbMoneda.SelectedIndex = cmbMoneda.FindItemIndexByValue("-1");
                    }
                    else
                    {//muestra datos del cliente, los de CONSIGNACION, si no existen, muestra los de FACTURACION
                        if (cliente.Id_Cfe != -1)
                        {
                            cmbConsFacEle.SelectedIndex = cmbConsFacEle.FindItemIndexByValue(cliente.Id_Cfe.ToString());
                            cmbConsFacEle.Text = cmbConsFacEle.FindItemByValue(cliente.Id_Cfe.ToString()).Text;
                            if (!modificar) //Trae el cosecutivo si no es una modificación de documento                            
                            {
                                if (!ObtenerConsecutivoFactElectronica(cliente.Id_Cfe))
                                {
                                    Alerta("No hay consecutivo de facturación electrónica disponible para la serie seleccionada");
                                }
                            }
                        }
                        else
                        { //se limpia solo si es nueva factura, si no le pone el "ConsFacEle" que trae el cliente en la Factura
                            if (this.hiddenId.Value != string.Empty)
                            {
                                txtId.Text = string.Empty;
                                cmbConsFacEle.SelectedIndex = cmbConsFacEle.FindItemIndexByValue("-1");
                            }
                        }
                        txtContacto.Text = cliente.Cte_Contacto;

                        if (string.IsNullOrEmpty(cliente.Cte_Calle) && string.IsNullOrEmpty(cliente.Cte_Numero) &&
                            string.IsNullOrEmpty(cliente.Cte_Colonia) && string.IsNullOrEmpty(cliente.Cte_Municipio) &&
                            string.IsNullOrEmpty(cliente.Cte_Estado) && string.IsNullOrEmpty(cliente.Cte_Cp))
                        {
                            txtCalle.Text = cliente.Cte_FacCalle;
                            txtCalleNumero.Text = cliente.Cte_FacNumero;
                            txtCP.Text = cliente.Cte_FacCp;
                            txtColonia.Text = cliente.Cte_FacColonia;
                            txtMunicipio.Text = cliente.Cte_FacMunicipio;
                            txtEstado.Text = cliente.Cte_FacEstado;
                        }
                        else
                        {

                            //if (txtCalle.Text == "" && txtCalleNumero.Text == "" && txtCP.Text == "" && txtColonia.Text == "" && txtMunicipio.Text == "" && txtEstado.Text == "")
                            //{
                            txtCalle.Text = cliente.Cte_Calle;
                            txtCalleNumero.Text = cliente.Cte_Numero;
                            txtCP.Text = cliente.Cte_Cp;
                            txtColonia.Text = cliente.Cte_Colonia;
                            txtMunicipio.Text = cliente.Cte_Municipio;
                            txtEstado.Text = cliente.Cte_Estado;
                            //}
                        }

                        txtRFC.Text = cliente.Cte_FacRfc;
                        txtTelefono.Text = cliente.Cte_Telefono;
                        chkDesgloce.Checked = cliente.Cte_DesgIva;
                        chkRetencion.Checked = cliente.Cte_RetIva;
                        txtPorcRetencion.Text = cliente.PorcientoRetencion.ToString();
                        txtCondiciones.Text = cliente.Cte_CondPago.ToString();
                        if ((chkRetencion.Checked == true))
                        {
                            txtPorcRetencion.Visible = true;
                        }
                        else
                        {
                            txtPorcRetencion.Visible = false;
                        }
                        if (cliente.Id_Mon != -1)
                        {
                            txtMoneda.Text = cliente.Id_Mon.ToString();
                            cmbMoneda.SelectedIndex = cmbMoneda.FindItemIndexByValue(cliente.Id_Mon.ToString());
                        }
                        else
                        {
                            txtMoneda.Text = string.Empty;
                            cmbMoneda.SelectedIndex = cmbMoneda.FindItemIndexByValue("-1");
                        }

                        if ((idCliente != "-1" && idCliente != string.Empty)
                            && (this.cmbMov.SelectedValue != "-1" && this.cmbMov.SelectedValue != string.Empty)
                            && (this.cmbTerritorio.SelectedValue != "-1" && this.cmbTerritorio.SelectedValue != string.Empty))
                        {
                            this.rgFacturaDet.Enabled = true;
                            this.rgFacturaDetAde.Enabled = true;
                            this.rgAdendaFacturacion.Enabled = true;
                            this.btnFacturaEspecial.Enabled = true;
                        }
                        else
                        {
                            this.rgFacturaDet.Enabled = false;
                            this.rgFacturaDetAde.Enabled = false;
                            this.btnFacturaEspecial.Enabled = false;
                        }
                        datosClienteEstablecidos = true;
                    }
                }
                return datosClienteEstablecidos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string generar_motivo(string motivo)
        {
            string motivo_texto = "El cliente tiene el crédito suspendido";

            if (motivo != "")
            {
                motivo = "<table>" + motivo + "</table>";
                motivo = motivo.Replace("LIM", "<tr><td>-&nbsp;</td><td>excedió el límite de crédito</tr></td>");
                motivo = motivo.Replace("ACC", "<tr><td>-&nbsp;</td><td>tiene acciones pendientes</tr></td>");
                motivo = motivo.Replace("VEN", "<tr><td>-&nbsp;</td><td>tiene facturas vencidas</tr></td>");
                motivo = motivo.Replace("REC", "<tr><td>-&nbsp;</td><td>faltan días de recepción</tr></td>");
                motivo = motivo.Replace("REV", "<tr><td>-&nbsp;</td><td>faltan días de revisión</tr></td>");
                motivo = motivo.Replace("PAG", "<tr><td>-&nbsp;</td><td>faltan días de pago</tr></td>");
                motivo = motivo.Replace("CON", "<tr><td>-&nbsp;</td><td>faltan condiciones de pago</tr></td>");
                motivo = motivo.Replace(",", "");

                motivo = motivo[0].ToString().ToUpper() + motivo.Substring(1, motivo.Length - 1);

                motivo_texto = motivo_texto + ", causas:" + motivo;
            }
            return motivo_texto;
        }
        private void ConsultaInventarioProducto(int Id_Prd)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Producto producto = null;
                //obtener datos de producto
                CN_CatProducto clsProducto = new CN_CatProducto();
                clsProducto.ConsultaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, 0, sesion.Id_Cd_Ver, Id_Prd);

                HD_Prd_UniEmp.Value = producto == null ? string.Empty : producto.Prd_UniEmp.ToString();
                HD_Prd_InvFinal.Value = producto == null ? string.Empty : producto.Prd_InvFinal.ToString();
                HD_Prd_Asignado.Value = producto == null ? string.Empty : producto.Prd_Asignado.ToString();
                HD_Prd_Disponible.Value = producto == null ? string.Empty : (producto.Prd_InvFinal - producto.Prd_Asignado).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void HabilitarCamposPedido(bool habilitar)
        {
            try
            {
                txtCondiciones.Enabled = habilitar;
                txtMoneda.Enabled = habilitar;
                cmbMoneda.Enabled = habilitar;
                txtConducto.Enabled = habilitar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void HabilitaBotonesToolBar(bool nuevo, bool guardar, bool regresar, bool eliminar, bool imprimir, bool correo)
        {
            try
            {
                this.RadToolBar1.Items[6].Visible = nuevo;
                this.RadToolBar1.Items[5].Visible = guardar;
                if (guardar)
                    if (_PermisoGuardar == false & _PermisoModificar == false)
                        this.RadToolBar1.Items[5].Visible = false;
                //Regresar
                this.RadToolBar1.Items[4].Visible = regresar;
                //Eliminar
                this.RadToolBar1.Items[3].Visible = eliminar;
                //Imprimir
                this.RadToolBar1.Items[2].Visible = imprimir;
                //Correo
                this.RadToolBar1.Items[1].Visible = correo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string MaximoId()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(sesion.Id_Emp, sesion.Id_Cd_Ver, "CapFactura", "Id_Fac", sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string generarGUI_IdAdeDet()
        {
            string guidIdentifier = System.Guid.NewGuid().ToString();
            guidIdentifier = guidIdentifier.Replace("-", string.Empty);
            guidIdentifier.ToUpper();
            return guidIdentifier;
        }
        private void HabilitaControles(bool habilitar)
        {
            try
            {
                txtNotas.Enabled = habilitar;
                rgFacturaDet.Enabled = habilitar;
                ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible = habilitar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void EstablecerDataSourceProductosLista(string filtro)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Producto producto = new Producto();

                List<Producto> listaProducto = new List<Producto>();
                if (cmbTerritorio.SelectedValue != string.Empty)
                    new CN_CatProducto().ConsultaListaProductoFacturacion(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(cmbTerritorio.SelectedValue), filtro, ref listaProducto, 1);

                producto = new Producto();
                producto.Id_Prd = -1;
                producto.Prd_Descripcion = "-- Seleccionar --";
                listaProducto.Insert(0, producto);

                this.ListaProductos = listaProducto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void EstablecerDataSourceTerritoriosClienteLista()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<Territorios> listaTerritorios = new List<Territorios>();
                //if (cmbCliente.SelectedValue != "-1")
                if (txtCliente.Value.HasValue)
                {
                    new CN_CatCliente().ConsultaTerritoriosDelCliente(Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1), sesion, ref listaTerritorios);
                }
                this.ListaTerritorios = listaTerritorios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarConsFactElectronica()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver, 1, sesion.Emp_Cnx, "spCatConsFactEle_Combo", ref cmbConsFacEle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarComboTipoMovimientos()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, 1, sesion.Id_Emp, 2, sesion.Emp_Cnx, "spCatMovimiento_Combo", ref cmbMov);
                this.cmbMov.SelectedValue = "51";
                this.cmbMov.Text = (this.cmbMov.FindItemByValue("51")).Text;
                this.txtMov.Text = this.cmbMov.SelectedValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarComboTerritorios()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                int cliente = !string.IsNullOrEmpty(txtCliente.Value.ToString()) ? Convert.ToInt32(txtCliente.Value.ToString()) : -1;

                List<Territorios> listaTerritorios = new List<Territorios>();
                new CN_CatCliente().ConsultaTerritoriosDelCliente(cliente, sesion, ref listaTerritorios);
                cmbTerritorio.DataTextField = "Descripcion";
                cmbTerritorio.DataValueField = "Id_Ter";
                cmbTerritorio.DataSource = listaTerritorios;
                cmbTerritorio.DataBind();

                if (cmbTerritorio.Items.Count > 1)
                {
                    cmbTerritorio.SelectedIndex = 1;
                    cmbTerritorio.Text = cmbTerritorio.Items[1].Text;
                    txtTerritorio.Text = cmbTerritorio.Items[1].Value;

                    CN_CatTerritorios territorio = new CN_CatTerritorios();
                    Territorios ter = new Territorios();
                    ter.Id_Emp = sesion.Id_Emp;
                    ter.Id_Cd = sesion.Id_Cd_Ver;
                    ter.Id_Ter = Convert.ToInt32(cmbTerritorio.Items[1].Value);
                    territorio.ConsultaTerritoriosCombo(ref ter, sesion.Emp_Cnx);
                    txtRepresentante.Text = ter.Id_Rik.ToString();
                    txtRepresentanteStr.Text = ter.Rik_Nombre;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarFormaPago()
        {
            try
            {
                int? cliente;
                if (!_PermisoGuardar)
                {
                    cliente = null;
                }
                else
                {
                    cliente = txtCliente.Value.HasValue ? (int)txtCliente.Value.Value : -1;
                }
                cmbFormaPago.Items.Clear();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, cliente, sesion.Emp_Cnx, "spCatClienteFormaPago_Combo", ref cmbFormaPago);
                //this.cmbFormaPago.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

                if (cmbFormaPago.Items.Count > 0)
                {
                    cmbFormaPago.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarComboTipoModeda()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatTipoMoneda_Combo", ref cmbMoneda);
                this.cmbMoneda.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void rgAdendaFacturacion_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgAdendaFacturacion.DataSource = ListCab;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgAdendaReFacturacion_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgAdendaReFacturacion.DataSource = ListCabRF;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("ConsFacElectronica_ExcedeRango"))
                Alerta("El consecutivo de facturación electrónica no está en el rango de consecutivos (folio inicial y folio final) de la serie seleccionada");
            else
                if (mensaje.Contains("cmbConsFacEle_ObtenerConsFacElectFallo"))
                    Alerta("Error al momento de obtener el consecutivo de facturación electrónica");
                else
                    if (mensaje.Contains("MovFacturacionPedidoNoValido"))
                        Alerta("El tipo de movimiento no es válido");
                    else
                        if (mensaje.Contains("FacturacionClienteExtNoEnPartida"))
                            Alerta("Favor de capturar el cliente externo en todas las partidas");
                        else
                            if (mensaje.Contains("FacturacionPedidoNoReferenciaFac"))
                                Alerta("El pedido no hace referencia a ninguna factura");
                            else
                                if (mensaje.Contains("rgFacturaDet_delete_error_cancelacion"))
                                    Alerta("Esta orden de compra ya esta cancelada");
                                else
                                    if (mensaje.Contains("CapOrdCompra_print_error"))
                                        Alerta("Error al momento de generar el reporte de impresi&oacute;n de orden de compra");
                                    else
                                        if (mensaje.Contains("CapOrdCompra_delete_ok"))
                                            Alerta("La orden de compra se ha dado de baja (estatus 'B') correctamente");
                                        else
                                            if (mensaje.Contains("CapOrdCompra_delete_error"))
                                                Alerta("Error al momento de dar de baja la orden de compra");
                                            else
                                                if (mensaje.Contains("rgFacturaDet_insert_repetida"))
                                                    Alerta("Este producto ya ha sido capturado");
                                                else
                                                    if (mensaje.Contains("rgFacturaDet_delete_item_error"))
                                                        Alerta("Error al momento de eliminar el producto a la lista de productos de la factura");
                                                    else
                                                        if (mensaje.Contains("rgFacturaDet_insert_error"))
                                                            Alerta("Error al momento de agregar el producto a la lista de productos de la factura");
                                                        else
                                                            if (mensaje.Contains("Cliente_NoPermiteFacturacion"))
                                                                Alerta("CUIDADO. Este cliente se encuentra bloqueado por parte de cobranza, favor de aclarar su situación de créditos");
                                                            else
                                                                if (mensaje.Contains("cmbCliente_IndexChanging_error"))
                                                                    Alerta("Error al consultar los datos del cliente");
                                                                else
                                                                    if (mensaje.Contains("cmbProducto_IndexChanging_error"))
                                                                        Alerta("Error al consultar los datos de producto");
                                                                    else
                                                                        if (mensaje.Contains("rgFacturaDet_ItemDataBound"))
                                                                            Alerta("Error al momento de preparar un registro para edici&oacute;n");
                                                                        else
                                                                            if (mensaje.Contains("rgFacturaDetAde_ItemDataBound"))
                                                                                Alerta("Error al momento de preparar un registro para edici&oacute;n");
                                                                        else
                                                                            if (mensaje.Contains("CapOrdCompraDetalle_consulta_error"))
                                                                                Alerta("Error al consultar el detalle de la orden de compra");
                                                                            else
                                                                                if (mensaje.Contains("CapOrdCompraDetalle_insert_error"))
                                                                                    Alerta("Error al guardar el detalle de la orden de compra");
                                                                                else
                                                                                    if (mensaje.Contains("rgFacturaDet_NeedDataSource"))
                                                                                        Alerta("Error al cargar el grid de detalle de factura");
                                                                                    else
                                                                                        if (mensaje.Contains("rgFacturaDetAde_NeedDataSource"))
                                                                                            Alerta("Error al cargar el grid de detalle de factura");
                                                                                        else
                                                                                        if (mensaje.Contains("rgFacturaDet_ItemCommand"))
                                                                                            Alerta("Error al ejecutar un evento (rgOrdCompra_ItemCommand) al cargar el grid de detalle de factura");
                                                                                        else
                                                                                            if (mensaje.Contains("rgFacturaDet_Actualizar_ok"))
                                                                                                Alerta("El producto de la orden de compra fue actualizado correctamente");
                                                                                            else
                                                                                                if (mensaje.Contains("rgFacturaDet_Actualizar_error"))
                                                                                                    Alerta("Error al actualizar el producto de la orden de compra");
                                                                                                else
                                                                                                    if (mensaje.Contains("Cmb_CentroDistribucion_IndexChanging_error"))
                                                                                                        Alerta("Error al cambiar de centro de distribución");
                                                                                                    else
                                                                                                        if (mensaje.Contains("radGrid_PageIndexChanged"))
                                                                                                            Alerta("Error al cambiar de página");
                                                                                                        else
                                                                                                            if (mensaje.Contains("CapFactura_LlenarForm_error"))
                                                                                                                Alerta("Error al momento de consultar los datos de la factura");
                                                                                                            else
                                                                                                                if (mensaje.Contains("PermisoGuardarNo"))
                                                                                                                    Alerta("No tiene permisos para grabar");
                                                                                                                else
                                                                                                                    if (mensaje.Contains("PermisoModificarNo"))
                                                                                                                        Alerta("No tiene permisos para actualizar");
                                                                                                                    else
                                                                                                                        if (mensaje.Contains("CapOrdCompraDetalle_delete_error"))
                                                                                                                            Alerta("Error al momento de eliminar el detalle de la orden de compra");
                                                                                                                        else
                                                                                                                            if (mensaje.Contains("cmbProducto_IndexChanging_error"))
                                                                                                                                Alerta("Error al consultar los datos del producto");
                                                                                                                            else
                                                                                                                                if (mensaje.Contains("CapFactura_Id_Es_NoEncontrado"))
                                                                                                                                    Alerta("No se pudo actualizar los datos de la factura. No se encontró el movimiento de entrada para esta factura hecha a partir de aparatos inproductivos. (tipo de movimiento 16)");
                                                                                                                                else
                                                                                                                                    if (mensaje.Contains("CapFactura_insert_ok"))
                                                                                                                                        Alerta("Los datos se guardaron correctamente");
                                                                                                                                    else
                                                                                                                                        if (mensaje.Contains("CapFactura_insert_error"))
                                                                                                                                            Alerta(string.Concat("No se pudo guardar la factura. ", mensaje.Replace("'", "\"")));
                                                                                                                                        else
                                                                                                                                            if (mensaje.Contains("CapFactura_update_ok"))
                                                                                                                                                Alerta("Los datos se modificaron correctamente");
                                                                                                                                            else
                                                                                                                                                if (mensaje.Contains("CapFactura_update_error"))
                                                                                                                                                    Alerta(string.Concat("No se pudo actualizar los datos de la factura. ", mensaje.Replace("'", "\"")));
                                                                                                                                                else
                                                                                                                                                    if (mensaje.Contains("Page_Load_error"))
                                                                                                                                                        Alerta("Error al cargar la página");
                                                                                                                                                    else
                                                                                                                                                        Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }
        private void CerrarVentana()
        {
            try
            {
                Session["ListaRemisionesFactura" + Session.SessionID] = null;
                string funcion;
                funcion = "CloseAndRebind()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region "Métodos para obtrener desde objetos los valores para los controles durante la inserción/actualización de un Grid editable"

        protected string ObtenerDescripcion(object oc)
        {
            if (oc is FacturaDet) { return ((FacturaDet)oc).Producto.Prd_Descripcion; } else { return string.Empty; }
        }

        protected string ObtenerDescripcionTerritorio(object oc)
        {
            if (oc is FacturaDet) { return ((FacturaDet)oc).Id_TerStr; } else { return string.Empty; }
        }

        protected string ObtenerDescripcionCliente(object oc)
        {
            if (oc is FacturaDet) { return ((FacturaDet)oc).Id_CteExtStr; } else { return string.Empty; }
        }

        protected string ObtenerPresentacion(object oc)
        {
            if (oc is FacturaDet) { return ((FacturaDet)oc).Producto.Prd_Presentacion; } else { return string.Empty; }
        }

        protected string ObtenerUnidades(object oc)
        {
            if (oc is FacturaDet) { return ((FacturaDet)oc).Producto.Prd_UniNe; } else { return string.Empty; }
        }

        protected int ObtenerInvFinal(object oc)
        {
            if (oc is FacturaDet) { return ((FacturaDet)oc).Producto.Prd_InvFinal; } else { return 0; }
        }

        #endregion
        private void HabilitarColumnas(bool habilitar)
        {
            GridCommandItem cmdItem = (GridCommandItem)rgFacturaDet.MasterTableView.GetItems(GridItemType.CommandItem)[0];
            cmdItem.FindControl("AddNewRecordButton").Parent.Visible = !habilitar;

            GridCommandItem cmdItem2 = (GridCommandItem)rgFacturaDetAde.MasterTableView.GetItems(GridItemType.CommandItem)[0];                            
            cmdItem2.FindControl("AddNewRecordButton").Parent.Visible = !habilitar;            

            try
            {
                rgFacturaDet.Columns.FindByUniqueName("EditCommandColumn").Display = !habilitar;
                rgFacturaDetAde.Columns.FindByUniqueName("EditCommandColumn").Display = !habilitar;
            }
            catch
            { }
            try
            {
                rgFacturaDet.Columns.FindByUniqueName("DeleteColumn").Display = !habilitar;
                rgFacturaDetAde.Columns.FindByUniqueName("DeleteColumn").Display = !habilitar;
            }
            catch
            {
            }


            if (Page.Request.QueryString["reFactura"] != null)
            {
                cmdItem.FindControl("AddNewRecordButton").Parent.Visible = false;
                cmdItem2.FindControl("AddNewRecordButton").Parent.Visible = false;
                rgAdendaReFacturacion.Rebind();
                try
                {

                    //rgFacturaDet.Columns.FindByUniqueName("EditCommandColumn").Display = true;
                    rgFacturaDetAde.Columns.FindByUniqueName("EditCommandColumn").Display = true;
                    rgFacturaDetAde.Columns.FindByUniqueName("DeleteColumn").Display = true;
                }
                catch
                { }
            }
        }
        #region "Métodos para manejar la lista dinámica de Productos de la factura"
        //protected void ListaProductosFactura_AgregarProducto(FacturaDet factura_prod)
        //{
        //List<FacturaDet> lista = this.ListaProductosFactura;


        ////buscar producto de factura en la lista para ver si ya existe
        //for (int i = 0; i < lista.Count; i++)
        //{
        //    FacturaDet factura = lista[i];
        //    if (factura.Id_Prd == factura_prod.Id_Prd)//si el producto es el mismo
        //    {
        //        if (factura.Id_Ter == factura_prod.Id_Ter)//y si el territorio es el mismo
        //        {
        //            throw new Exception("rgFacturaDet_insert_repetida");
        //        }
        //    }
        //}
        //lista.Add(factura_prod);

        //}

        //protected void ListaProductosFactura_ModificarProducto(FacturaDet factura_prod)
        //{
        //    //List<FacturaDet> lista = this.ListaProductosFactura;

        //    ////buscar producto de factura en la lista
        //    //for (int i = 0; i < lista.Count; i++)
        //    //{
        //    //    FacturaDet factura = lista[i];
        //    //    if (factura.Id_Prd == factura_prod.Id_Prd)
        //    //    {
        //    //        lista[i] = factura_prod;
        //    //        break;
        //    //    }
        //    //}
        //    //this.ListaProductosFactura = lista;
        //    //this.CalcularTotales();


        //    try
        //    {
        //        int Id_Fac = factura_prod.Id_Fac;
        //        int Id_FacDet = factura_prod.Id_FacDet;
        //        int? Id_Rem = factura_prod.Id_Rem;
        //        int? Id_CteExt = factura_prod.Id_CteExt;
        //        int Id_Ter = factura_prod.Id_Ter;
        //        int? Id_Prd = factura_prod.Id_Prd;
        //        string Prd_Descripcion = factura_prod.Producto.Prd_Descripcion;
        //        string Prd_Presentacion = factura_prod.Producto.Prd_Presentacion;
        //        string Prd_UniNe = factura_prod.Producto.Prd_UniNe;
        //        int Fac_Cant = factura_prod.Fac_Cant;
        //        float? Rem_Cant = factura_prod.Rem_Cant;
        //        float Fac_Precio = factura_prod.Fac_Precio;
        //        float Fac_Importe = factura_prod.Fac_Importe;

        //        DataRow[] Ar_dr;

        //        Ar_dr = ListaProductosFactura.Select("Id_Prd='" + Id_Prd + "'");
        //        if (Ar_dr.Length > 0)
        //        {
        //            Ar_dr[0].BeginEdit();
        //            Ar_dr[0]["Id_Fac"] = Id_Fac;
        //            Ar_dr[0]["Id_FacDet"] = Id_FacDet;
        //            Ar_dr[0]["Id_CteExt"] = Id_CteExt;
        //            Ar_dr[0]["Id_Ter"] = Id_Ter;
        //            Ar_dr[0]["Id_Prd"] = Id_Prd;
        //            Ar_dr[0]["Prd_Descripcion"] = Prd_Descripcion;
        //            Ar_dr[0]["Prd_Presentacion"] = Prd_Presentacion;
        //            Ar_dr[0]["Prd_UniNe"] = Prd_UniNe;
        //            Ar_dr[0]["Fac_Cant"] = Fac_Cant;
        //            Ar_dr[0]["Rem_Cant"] = Rem_Cant;
        //            Ar_dr[0]["Fac_Precio"] = Fac_Precio;
        //            Ar_dr[0]["Fac_Importe"] = Fac_Importe;
        //            Ar_dr[0].AcceptChanges();
        //        }

        //        CalcularTotales();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        protected void ListaProductosFactura_EliminarProducto(int Id_Prd, int Id_Ter, int id_Fac)
        {
            try
            {
                DataRow[] Ar_dr;

                Ar_dr = ListaProductosFactura.Select("Id_Prd='" + Id_Prd + "' and Id_Ter='" + Id_Ter + "'");
                if (Ar_dr.Length > 0)
                {
                    if (this.hiddenId.Value != string.Empty)
                    {
                        int verificador = 0;
                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        CN_CapFactura factura = new CN_CapFactura();
                        string Id_FacSerie = this.cmbConsFacEle.SelectedItem.Text + this.txtId.Text;
                        factura.revisionEspeciales(sesion, Id_Prd, Id_Ter, id_Fac,Id_FacSerie,  ref verificador);
                        if (verificador != 0)
                        {
                            Ar_dr[0].Delete();
                            ListaProductosFactura.AcceptChanges();
                        }
                        else
                        {
                            Alerta("no se puede eliminar el producto " + Id_Prd + " por que tiene relación con facturas especiales");
                        }
                    }
                    else
                    {
                        Ar_dr[0].Delete();
                        ListaProductosFactura.AcceptChanges();
                    }
                }
                this.CalcularTotales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ListaProductosFacturaAdenda_EliminarProducto(string Id_Cons_AdeDet)
        {
            try
            {
                DataRow[] Ar_dr;
                
//                Ar_dr = ListaProductosFacturaAdenda.Select("Id_Prd='" + Id_Prd +  "'");
                Ar_dr = ListaProductosFacturaAdenda.Select("Id_Cons_AdeDet='" + Id_Cons_AdeDet + "'");
                if (Ar_dr.Length > 0)
                {                                                            
                        Ar_dr[0].Delete();
                        ListaProductosFacturaAdenda.AcceptChanges();                    
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CalcularTotales()
        {
            try
            {
                double importeTotal = 0;
                double porcDescuento1 = txtDescuento1.Text != string.Empty ? Convert.ToDouble(txtDescuento1.Text) : 0;
                double porcDescuento2 = txtDescuento2.Text != string.Empty ? Convert.ToDouble(txtDescuento2.Text) : 0;

                for (int i = 0; i < ListaProductosFactura.Rows.Count; i++)
                {
                    importeTotal += Convert.ToDouble(ListaProductosFactura.Rows[i]["Fac_Importe"]);
                }
                txtImporte.Text = importeTotal.ToString();
                importeTotal = porcDescuento1 > 0 ? (importeTotal - (importeTotal * (porcDescuento1 / 100))) : importeTotal;
                importeTotal = porcDescuento2 > 0 ? (importeTotal - (importeTotal * (porcDescuento2 / 100))) : importeTotal;
                txtSubTotal.Text = importeTotal.ToString();
                txtIVA.Text = HD_IVAfacturacion.Value.Trim() != string.Empty ? (importeTotal * (Convert.ToDouble(HD_IVAfacturacion.Value.Trim()) / 100)).ToString() : "0";
                txtTotal.Text = (Convert.ToDouble(txtSubTotal.Text) + Convert.ToDouble(txtIVA.Text)).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CalcularTotalesEntradaRemision(ref EntradaSalida entrada)
        {
            try
            {
                double importeTotal = 0;
                double porcDescuento1 = txtDescuento1.Text != string.Empty ? Convert.ToInt32(txtDescuento1.Text) : 0;
                double porcDescuento2 = txtDescuento2.Text != string.Empty ? Convert.ToInt32(txtDescuento2.Text) : 0;

                foreach (EntradaSalidaDetalle entradaDet in entrada.ListaDetalle)
                {
                    importeTotal += (entradaDet.Es_Cantidad * entradaDet.Es_Costo);
                }
                importeTotal = porcDescuento1 > 0 ? (importeTotal - (importeTotal * (porcDescuento1 / 100))) : importeTotal;
                importeTotal = porcDescuento2 > 0 ? (importeTotal - (importeTotal * (porcDescuento2 / 100))) : importeTotal;
                entrada.Es_SubTotal = importeTotal;
                entrada.Es_Iva = HD_IVAfacturacion.Value.Trim() != string.Empty ? (importeTotal * (Convert.ToDouble(HD_IVAfacturacion.Value.Trim()) / 100)) : 0;
                entrada.Es_Total = entrada.Es_SubTotal + entrada.Es_Iva;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void HabilitarControlesTotales(bool habilitar)
        {
            try
            {
                txtImporte.Enabled = habilitar;
                txtSubTotal.Enabled = habilitar;
                txtIVA.Enabled = habilitar;
                txtTotal.Enabled = habilitar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion
        #region ErrorManager
        private void AlertaFocus(string mensaje, string rtb)
        {
            try
            {
                RAM1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void AlertaFocus2(string mensaje, string rtb)
        {
            try
            {
                RAM1.ResponseScripts.Add("AlertaFocus2('" + mensaje + "','" + rtb + "');");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 340, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void Alerta2(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 600, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void ErrorManager()
        {
            try
            {
                this.lblMensaje.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ErrorManager(string Message)
        {
            try
            {
                this.lblMensaje.Text = Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ErrorManager(Exception eme, string NombreFuncion)
        {
            try
            {
                this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion
    }
}