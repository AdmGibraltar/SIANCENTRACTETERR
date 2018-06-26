using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Collections;
using CapaDatos;
using System.Configuration;
using System.Xml;

namespace SIANWEB
{
    public partial class CapRemisiones : System.Web.UI.Page
    {
        #region Variables
        private object _producto;
        public object producto
        {
            get { return _producto; }
            set { _producto = value; }
        }
        public bool HabilitarGuardar
        {
            get
            {
                //DEVUELVE SI SE PUEDE O NO GUARDAR
                return RadToolBar1.Items[1].Visible;
            }
        }
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0] ]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0] ] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0] ]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0] ] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0] ]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0] ] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0] ]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0] ] = value; } }
        //private static bool Tm_ReqSpo;
        private bool Tm_ReqSpo
        {
            set { Session["Tm_ReqSpoREM" + Session.SessionID ] = value; }
            get { return (bool)Session["Tm_ReqSpoREM" + Session.SessionID ]; }
        }
        public string FechaEnable
        {
            get
            {
                return Convert.ToInt32(dpFecha.Enabled).ToString();// txtFecha.Enabled;
            }
        }
        public DataTable dt_detalles
        {
            get
            {
                return Session["dt_DetallesRem" + Session.SessionID ] as DataTable;
            }
            set
            {
                Session["dt_DetallesRem" + Session.SessionID ] = value;
            }
        }
        public DataTable dt_cuenta
        {
            get
            {
                return Session["dt_cuentaRem" + Session.SessionID ] as DataTable;
            }
            set
            {
                Session["dt_cuentaRem" + Session.SessionID ] = value;
            }
        }
        public DataTable dt_cuentaOriginal
        {
            get
            {
                return Session["dt_cuentaOriginalRem" + Session.SessionID ] as DataTable;
            }
            set
            {
                Session["dt_cuentaOriginalRem" + Session.SessionID ] = value;
            }
        }
        public DataTable dt_cuentaPedido
        {
            get
            {
                return Session["dt_cuentaPedidoRem" + Session.SessionID ] as DataTable;
            }
            set
            {
                Session["dt_cuentaPedidoRem" + Session.SessionID ] = value;
            }
        }

        //static int id_detalle = 0;
        private int id_detalle
        {
            set { Session["id_detalleREM" + Session.SessionID ] = value; }
            get { int? st = (int?)Session["id_detalleREM" + Session.SessionID ]; return st == null ? 0 : (int)st; }
        }
        //static int Id_RemDet_A = -1; //id de la partida que se va actualizar
        private int Id_RemDet_A
        {
            set { Session["Id_RemDet_AREM" + Session.SessionID ] = value; }
            get { int? st = (int?)Session["Id_RemDet_AREM" + Session.SessionID ]; return st == null ? -1 : (int)st; }
        }
        //static int cantidad_A = 0; //cantidad de la partida que se va actualizar
        private int cantidad_A
        {
            set { Session["cantidad_AREM" + Session.SessionID ] = value; }
            get { int? st = (int?)Session["cantidad_AREM" + Session.SessionID ]; return st == null ? 0 : (int)st; }
        }
        //static double costo_A = -1; //costo de la partida que se va actualizar
        private double costo_A
        {
            set { Session["costo_AREM" + Session.SessionID ] = value; }
            get { double? st = (double?)Session["costo_AREM" + Session.SessionID ]; return st == null ? -1 : (double)st; }
        }
        //static int Id_Prd_A;
        private int Id_Prd_A
        {
            set { Session["Id_Prd_AREM" + Session.SessionID ] = value; }
            get { return (int)Session["Id_Prd_AREM" + Session.SessionID ]; }
        }
        //static bool edicionRemisionDePedido;
        private bool edicionRemisionDePedido
        {
            set { Session["edicionRemisionDePedidoRem" + Session.SessionID ] = value; }
            get { return (bool)Session["edicionRemisionDePedidoRem" + Session.SessionID ]; }
        }
        /// <summary>
        /// 1 nuevo, 2 actualizacion, 3 RemisionDePedido    
        /// </summary>
        //static int tipoDeMovimiento = 0;
        private int tipoDeMovimiento
        {
            set { Session["tipoDeMovimientoREM" + Session.SessionID ] = value; }
            get { int? st = (int?)Session["tipoDeMovimientoREM" + Session.SessionID ]; return st == null ? 0 : (int)st; }
        }
        //static bool remisionDePedido;
        //static bool hayProductosNoSpo;
        private bool hayProductosNoSpo
        {
            set { Session["hayProductosNoSpoREM" + Session.SessionID ] = value; }
            get { return (bool)Session["hayProductosNoSpoREM" + Session.SessionID ]; }
        }
        //static int Id_Rem_Actualiza;
        private int Id_Rem_Actualiza
        {
            set { Session["Id_Rem_ActualizaREM" + Session.SessionID ] = value; }
            get { return (int)Session["Id_Rem_ActualizaREM" + Session.SessionID ]; }
        }


        private int Id_CuentaNacional
        {
            set { Session["Id_CuentaNacional" + Session.SessionID ] = value; }
            get { return (int)Session["Id_CuentaNacional" + Session.SessionID]; }
        }



        private int NumCuentaContNacional
        {
            set { Session["NumCuentaContNacional" + Session.SessionID] = value; }
            get { return (int)Session["NumCuentaContNacional" + Session.SessionID]; }
        }

        //Propiedad de lista de productos (partidas) de la Nota de cargo
        private List<RemisionDet> ListaProductosRemisionEspecial
        {
            get { return (List<RemisionDet>)Session["ListaProductosRemisionEspecial" ]; }
            set { Session["ListaProductosRemisionEspecial" ] = value; }
        }
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                sesion.URL = HttpContext.Current.Request.Url.Host;
                if (sesion == null)
                    CerrarVentana();
                else
                {
                    if (!Page.IsPostBack)
                    {
                        sesion.HoraInicio = DateTime.Now;
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();

                        //oculta campo si no es remision de pedido     
                        CN_Configuracion cn_configuracion = new CN_Configuracion();
                        ConfiguracionGlobal cg = new ConfiguracionGlobal();
                        cg.Id_Emp = sesion.Id_Emp;
                        cg.Id_Cd = sesion.Id_Cd_Ver;
                        cn_configuracion.Consulta(ref cg, sesion.Emp_Cnx);
                        HiddenField2.Value = cg.Mail_MinValuacion.ToString();

                        dpFecha.SelectedDate = DateTime.Now;
                        crearDT();
                        crear_dt_cuenta();
                        CargarTipoMovimiento();
                        cmbTipo.Items.Insert(0, new RadComboBoxItem("Remisiones", "0"));
                        Inicializar();
                        dpFecha.Focus();
                        rgDetalles.DataSource = dt_detalles;
                        rgDetalles.Rebind();

                        if (!sesion.Cu_Modif_Pass_Voluntario)
                            RadAjaxManager1.ResponseScripts.Add("(function(){var f = function(){AbrirContrasenas(); return false;Sys.Application.remove_load(f);};Sys.Application.add_load(f);})()");


                        if (!((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible)
                        {
                            deshabilitarcontroles(divGenerales.Controls);
                            deshabilitarcontroles(formularioTotales.Controls);
                            GridCommandItem cmdItem = (GridCommandItem)rgDetalles.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                            cmdItem.FindControl("AddNewRecordButton").Parent.Visible = false;// remove the image button                             
                            rgDetalles.MasterTableView.Columns[rgDetalles.MasterTableView.Columns.Count - 1].Display = false;
                            rgDetalles.MasterTableView.Columns[rgDetalles.MasterTableView.Columns.Count - 2].Display = false;
                        }

                        double ancho = 0;
                        foreach (GridColumn gc in rgDetalles.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgDetalles.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgDetalles.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void deshabilitarcontroles(System.Web.UI.ControlCollection controles_contenidos)
        {
            for (int x = 0; x < controles_contenidos.Count; x++)
            {
                string Type = controles_contenidos[x].GetType().FullName;
                if (Type.Contains("RadMultiPage") || Type.Contains("RadPageView") || Type.Contains("Panel"))
                {
                    deshabilitarcontroles(controles_contenidos[x].Controls);
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
                        break;
                    case "RadDateTimePicker":
                        (controles_contenidos[x] as RadDateTimePicker).Enabled = false;
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
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ErrorManager();
            rgDetalles.DataSource = dt_detalles;
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "new":
                        Nuevo();
                        break;
                    case "save":
                        Guardar(true);
                        break;
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.ToString());
                //Alerta(ex.ToString());
                //cacharMsgBaseDatos(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                throw ex;
            }
        }
        protected void cmbTipoMovimiento_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();

                ValidaClienteCuentaNacional();
                if (cmbTipoMovimiento.SelectedValue != "" && cmbTipoMovimiento.SelectedValue != "-1")
                {
                    Sesion sesion = new Sesion();
                    sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    bool Tm_ReqSpo2 = false;
                    new CN_CatMovimientos().ConsultarTmovimientoReqSpo(sesion, int.Parse(cmbTipoMovimiento.SelectedValue), ref Tm_ReqSpo2);
                    Tm_ReqSpo = Tm_ReqSpo2;
                
                    
                    if (tipoDeMovimiento == 3 && Tm_ReqSpo && hayProductosNoSpo)
                    {
                        Alerta("Este pedido contiene artículos que no son sistema de propietario, "
                                + "favor de seleccionar otro tipo de movimiento");
                        cmbTipoMovimiento.SelectedValue = "-1";
                        txtTipoId.Text = "";
                        return;
                    }
                    hf_spo.Value = Tm_ReqSpo.ToString();
                }

                if (tipoDeMovimiento == 1)
                {
                    crearDT();
                    crear_dt_cuenta();
                    rgDetalles.Rebind();
                    CalcularTotales();
                }
                int tipo = Convert.ToInt32(cmbTipoMovimiento.SelectedValue);
                if (tipo == 60 && txtTotal.Value >= Convert.ToDouble(HiddenField2.Value))
                {
                    valuacion.Visible = true;
                }
                else
                {
                    valuacion.Visible = false;
                }


                

                txtClienteId.Focus();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void ValidaClienteCuentaNacional()
        {

            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];

            Clientes cliente = new Clientes();
            cliente.Id_Emp = sesion.Id_Emp;
            cliente.Id_Cd = sesion.Id_Cd_Ver;
            cliente.Id_Cte = Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1);
            
            int TieneCuentaNacional = 0;
            if(int.Parse(cmbTipoMovimiento.SelectedValue) == 80) {
                new CN_CatCliente().ConsultaClienteTieneCuentaNacional(ref cliente, ref TieneCuentaNacional, sesion.Emp_Cnx);
                if (TieneCuentaNacional == -1)
                {
                    LimpiarCampos1();
                    txtClienteId.Text = "";
                    txtCliente.Text = "";
                    AlertaFocus("Este Cliente no Pertenece a cuenta Nacional", txtClienteId.ClientID);
                }
            }
            return;
            
        }

        protected void txtClienteId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                
                cmbCliente_indiceCambia();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbTerritorio_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (cmbTerritorio.SelectedValue == "-1")
                {
                    txtRepresentante.Text = "";
                    txtRepresentanteStr.Text = "";
                    txtTerritorioId.Focus();
                }
                else
                {
                    CN_CatTerritorios territorio = new CN_CatTerritorios();
                    Territorios ter = new Territorios();
                    ter.Id_Emp = sesion.Id_Emp;
                    ter.Id_Cd = sesion.Id_Cd_Ver;
                    ter.Id_Ter = Convert.ToInt32(cmbTerritorio.SelectedValue);
                    territorio.ConsultaTerritoriosCombo(ref ter, sesion.Emp_Cnx);
                    txtRepresentante.Text = ter.Id_Rik.ToString();
                    txtRepresentanteStr.Text = ter.Rik_Nombre;
                    txtCalle.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDetalles_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            ErrorManager();

            if (e.CommandName == "InitInsert")
            {
                cantidad_A = 0;
                Id_RemDet_A = 0;
                Id_Prd_A = 0;
                costo_A = 0;

                if (!validarCamposDetalle())
                {
                    e.Canceled = true;
                    return;
                }
                if (!validarFecha())
                {
                    e.Canceled = true;
                    return;
                }
            }
            if (e.CommandName == "Edit")
            {
                Id_RemDet_A = int.Parse(rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Id_RemDet"].Text);
                Id_Prd_A = int.Parse((rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Id_Prd"].FindControl("ProdLabel") as Label).Text);
                cantidad_A = int.Parse((rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Cantidad"].FindControl("CantidadLabel") as Label).Text);
                costo_A = double.Parse((rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Precio"].FindControl("PrecioLabel") as Label).Text);
                // RadNumericTextBox rad = (rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Id_Prd"].FindControl("RadNumericTextBox1") as RadNumericTextBox);               
                //rad.ClientEvents.OnLoad = "";
                //rad.ClientEvents.OnBlur = "";               
            }
        }
        protected void rgDetalles_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            ErrorManager();
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;

                RadComboBox RadComboBoxTerr = editItem.FindControl("RadComboBoxTerr") as RadComboBox;
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                new CN__Comun().LlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1), sesion.Emp_Cnx, "spCatTerritorioCte_Combo", ref RadComboBoxTerr);
                Label TerrIdDesc = (e.Item.FindControl("TerrIdDesc") as Label);

                Control insertbtn = (Control)editItem.FindControl("PerformInsertButton");
                if (insertbtn != null)
                { //se habilitan todos los controles
                    RadComboBoxTerr.Enabled = true;
                    (e.Item.FindControl("RadNumericTextBox1") as RadNumericTextBox).Enabled = true;
                    (editItem["importe"].Controls[0] as TextBox).Visible = false;
                }

                Control updatebtn = (Control)editItem.FindControl("UpdateButton");
                if (updatebtn != null)
                {
                    //se llenan y deshabilita cmb territorio y txtterritorio, cmb producto y txt producto.
                    //comboterritorio
                    (editItem.FindControl("RadComboBoxTerr") as RadComboBox).SelectedValue = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Terr"].ToString();//
                    (editItem.FindControl("txtter1") as RadNumericTextBox).Text = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Terr"].ToString();//txtterr
                    (editItem.FindControl("RadComboBoxTerr") as RadComboBox).Enabled = false;
                    (editItem.FindControl("txtter1") as RadNumericTextBox).Enabled = false;
                    //producto
                    (editItem.FindControl("RadNumericTextBox1") as RadNumericTextBox).Text = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Prd"].ToString();//txtproducto
                    (e.Item.FindControl("RadNumericTextBox1") as RadNumericTextBox).Enabled = false;//txtbox id del producto                    
                    editItem["importe"].Controls[1].Visible = false;
                }
            }

            //TODO: AGREGAR PARA PONER EL FOCUS
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem form = (GridEditableItem)e.Item;
                RadNumericTextBox dataField = (RadNumericTextBox)form["IdTerr"].FindControl("txtter1");
                if (!dataField.Enabled)
                {
                    dataField = (RadNumericTextBox)form["Cantidad"].FindControl("RadNumericTextBoxCantidad");
                }

                dataField.Focus();
            }
            //-----------------------------------------
        }
        protected void rgDetalles_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                
                GridEditableItem editedItem = e.Item as GridEditableItem;
                txtCantidad_TextChanged(editedItem["Cantidad"].FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox, e);
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                //RadComboBox comboboxProductos = (editedItem.FindControl("cmbProductosLista") as RadComboBox);
                RadNumericTextBox TxtProducto = editedItem.FindControl("RadNumericTextBox1") as RadNumericTextBox;
                //comprobar campos vacios
                if (
                        ((editedItem["Terr"].FindControl("RadComboBoxTerr") as RadComboBox).SelectedValue == "" || (editedItem["Terr"].FindControl("RadComboBoxTerr") as RadComboBox).SelectedValue == "-1")
                        || !TxtProducto.Value.HasValue
                        || (editedItem["Cantidad"].FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Text == ""
                        || (editedItem["Precio"].FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Text == ""
                    )
                {
                    Alerta("Todos los campos son requeridos");
                    e.Canceled = true;
                    return;
                }

                int territorio = int.Parse((editedItem["Terr"].FindControl("RadComboBoxTerr") as RadComboBox).SelectedValue);
                string DescrTer = (editedItem["Terr"].FindControl("RadComboBoxTerr") as RadComboBox).SelectedItem.Text;
                int Id_Prd = Convert.ToInt32(TxtProducto.Value);
                string descripcion = (editedItem["Descripcion"].FindControl("DescripcionTextBox") as RadTextBox).Text;
                int cantidad = Convert.ToInt32((editedItem["Cantidad"].FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Text);
                double precio = double.Parse((editedItem["Precio"].FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Text);
                double importe = cantidad * precio;

                //No se puede ingresar el mismo producto varias veces a menos ke tenga territorio distinto
                //msg Producto ya capturado
                if (dt_detalles.Select("Id_Prd='" + Id_Prd + "' and Terr='" + territorio + "'").Length > 0)
                {
                    Alerta("El producto ya ha sido agregado a la remisión");
                    e.Canceled = true;
                    return;
                }
                if (cantidad == 0)
                {
                    Alerta("No puede ingresar una partida con cantidad 0");
                    e.Canceled = true;
                    return;
                }
                if (precio == 0) //FRank: precio especial y precio publico se pueden modificar y no deben ser 0
                {
                    Alerta("No puede ingresar una partida con precio 0");
                    e.Canceled = true;
                    return;
                }


                if (Id_CuentaNacional > 0 ) 
                {

                    WS_Producto.Service1 ws = new WS_Producto.Service1();
                    ws.Url = ConfigurationManager.AppSettings["WS_Producto"].ToString();

                    string envio = ""+ Id_CuentaNacional + "|"+ Id_Prd+ "";
                    object respuesta = ws.TraeProductoCN(envio);
                    XmlDocument Xml = new XmlDocument();
                    Xml.LoadXml(respuesta.ToString());

                    XmlNode NodeError = Xml.SelectSingleNode("//Producto/MsgError/@Error");
                    XmlNode NodeValida = Xml.SelectSingleNode("//CuentaNacional/@ValidaCodEsp");
                    XmlNode NodeProductoID = Xml.SelectSingleNode("//Producto/@ProNum");
                    XmlNode NodeProductoDesc = Xml.SelectSingleNode("//Producto/@ProDesc");
                    XmlNode NodeProUM = Xml.SelectSingleNode("//Producto/@ProUM");
                    XmlNode NodeProPrecio = Xml.SelectSingleNode("//Producto/@ProPrecio"); 


                    if (!string.IsNullOrEmpty(NodeValida.InnerText))
                    {
                        if (NodeValida.InnerText != "N") {

                            if (!string.IsNullOrEmpty(NodeError.InnerText))
                            {
                                Alerta(NodeError.InnerText);
                                e.Canceled = true;
                                return;
                            }
                        }

                    }


                }


               

            

                //int cantidadEnDt_cuentaOriginal = 0;
                ////---si es REMISION DE PEDIDO, contar lo que ya se tenìa
                //if (tipoDeMovimiento == 3 && dt_cuentaOriginal.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                //{
                //    cantidadEnDt_cuentaOriginal = int.Parse(dt_cuentaOriginal.Select("Id_Prd='" + Id_Prd + "'")[0]["Cantidad"].ToString());
                //    if (dt_cuenta.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                //    {
                //        int cuentaActual = int.Parse(dt_cuenta.Select("Id_Prd='" + Id_Prd + "'")[0]["Cantidad"].ToString());
                //        if ((cantidad + cuentaActual) > cantidadEnDt_cuentaOriginal)
                //        {
                //            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                //            e.Canceled = true;
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        if (cantidad > cantidadEnDt_cuentaOriginal)
                //        {
                //            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                //            e.Canceled = true;
                //            return;
                //        }
                //    }
                //}

                ////************* SI es edicion de una remision de pedido, verificar que la cantidad no supere lo del pedido***************
                //if (edicionRemisionDePedido && dt_cuentaPedido.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                //{
                //    int cantidadEnPedido = int.Parse(dt_cuentaPedido.Select("Id_Prd='" + Id_Prd + "'")[0]["Cantidad"].ToString());
                //    if (dt_cuenta.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                //    {
                //        int cuentaActual = int.Parse(dt_cuenta.Select("Id_Prd='" + Id_Prd + "'")[0]["Cantidad"].ToString());
                //        cantidadEnDt_cuentaOriginal = int.Parse(dt_cuentaOriginal.Select("Id_Prd='" + Id_Prd + "'")[0]["Cantidad"].ToString());
                //        if ((cantidad + cuentaActual - cantidadEnDt_cuentaOriginal) > cantidadEnPedido)
                //        {
                //            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                //            e.Canceled = true;
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        if (cantidad > cantidadEnPedido)
                //        {
                //            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                //            e.Canceled = true;
                //            return;
                //        }
                //    }
                //}
                //----------------------------------------------------------------------------------------------------------------------

                //int disponible = 0;
                //int invFinal = 0;
                //int asignado = 0;
                //new CN_CapEntradaSalida().ConsultarDisponible(session, Id_Prd, ref disponible, ref invFinal, ref asignado);

                ////cuenta articulos
                //DataRow[] row_cuenta;
                //if (dt_cuenta.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                //{
                //    int original = tipoDeMovimiento == 2 ? int.Parse(dt_cuentaOriginal.Select("Id_Prd='" + Id_Prd + "'")[0]["Cantidad"].ToString()) : 0;
                //    row_cuenta = dt_cuenta.Select("Id_Prd='" + Id_Prd + "'");
                //    if (int.Parse(row_cuenta[0]["Cantidad"].ToString()) - original + cantidad > disponible)
                //    {// MSG asignado por antiguo sian
                //        Alerta("Producto <b>" + Id_Prd.ToString() + "</b> inventario disponible insuficiente, <br>inventario final: " + invFinal.ToString() +
                //            ",<br>asignado: " + asignado.ToString() + ",<br>disponible: " + disponible.ToString());
                //        e.Canceled = true;
                //        return;
                //    }
                //}
                //else
                //{
                //    if (cantidad > disponible)
                //    {// MSG asignado por antiguo sian
                //        Alerta("Producto <b>" + Id_Prd.ToString() + "</b> inventario disponible insuficiente,</br>inventario final: " + invFinal.ToString() + ",</br>asignado: " + asignado.ToString() + ",</br>disponible: " + disponible.ToString());
                //        e.Canceled = true;
                //        return;
                //    }
                //}

                //agregar al dt
                DataRow[] editable_dr;
                if (dt_cuenta.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                {
                    editable_dr = dt_cuenta.Select("Id_Prd='" + Id_Prd + "'");
                    editable_dr[0].BeginEdit();
                    editable_dr[0]["Cantidad"] = int.Parse(editable_dr[0]["Cantidad"].ToString()) + cantidad;
                    editable_dr[0].AcceptChanges();
                }
                else
                {
                    dt_cuenta.Rows.Add(new object[] { Id_Prd, cantidad });
                }

                double iva_cd = 0;
                new CN_CatCentroDistribucion().ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva_cd, session.Emp_Cnx);
                dt_detalles.Rows.Add(new object[] { ++id_detalle, territorio, Id_Prd, descripcion, cantidad, precio, importe, DescrTer });
                CalcularTotales();
                if (txtTipoId.Value == 60)
                {
                    double total = !string.IsNullOrEmpty(txtTotal.Text) ? Convert.ToDouble(txtTotal.Text) : 0;
                    if (total >= Convert.ToDouble(HiddenField2.Value))
                    {
                        valuacion.Visible = true;
                    }
                    else
                    {
                        valuacion.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            { //Alerta("No se pudieron guardar los datos. " + msgerror(ex)); //cambiar esto
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                e.Canceled = true;
            }
            finally
            {
                RadAjaxManager1.ResponseScripts.Add("showcolum();");
            }
        }
        protected void rgDetalles_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                GridEditableItem editedItem = e.Item as GridEditableItem;
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                txtCantidad_TextChanged(sender, e);
                RadNumericTextBox TxtProducto = editedItem.FindControl("RadNumericTextBox1") as RadNumericTextBox;
                //comprobar campos vacios
                if (
                        ((editedItem["Terr"].FindControl("RadComboBoxTerr") as RadComboBox).SelectedValue == "" || (editedItem["Terr"].FindControl("RadComboBoxTerr") as RadComboBox).SelectedValue == "-1")
                        || !TxtProducto.Value.HasValue
                        || (editedItem["Cantidad"].FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Text == ""
                        || (editedItem["Precio"].FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Text == ""
                    )
                {
                    Alerta("Todos los campos son requeridos");
                    e.Canceled = true;
                    return;
                }

                int Id_RemDet = int.Parse((editedItem["Id_RemDet"].Controls[0] as TextBox).Text);
                int territorio = int.Parse((editedItem["Terr"].FindControl("RadComboBoxTerr") as RadComboBox).SelectedValue);
                string DescrTer = (editedItem["Terr"].FindControl("RadComboBoxTerr") as RadComboBox).SelectedItem.Text;
                int Id_Prd = Convert.ToInt32(TxtProducto.Value);
                string descripcion = (editedItem["Descripcion"].FindControl("DescripcionTextBox") as RadTextBox).Text;
                int cantidad = Convert.ToInt32((editedItem["Cantidad"].FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Text);
                double precio = double.Parse((editedItem["Precio"].FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Text);
                double importe = cantidad * precio;

                if (cantidad == 0)
                {
                    Alerta("No puede ingresar una partida con cantidad 0");
                    e.Canceled = true;
                    return;
                }

                if (precio == 0) //FRank: precio especial y precio publico se pueden modificar y no deben ser 0
                {
                    Alerta("No puede ingresar una partida con precio 0");
                    e.Canceled = true;
                    return;
                }

                int dif = 0;
                if (cantidad > cantidad_A)
                {
                    dif = cantidad - cantidad_A;
                    //si es actualizacion, contar lo que ya se tenìa
                    int cantidadEnDt_cuentaOriginal = 0;
                    if (tipoDeMovimiento == 3 && dt_cuentaOriginal.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                    {
                        cantidadEnDt_cuentaOriginal = int.Parse(dt_cuentaOriginal.Select("Id_Prd='" + Id_Prd + "'")[0]["Cantidad"].ToString());
                        int cuentaActual = int.Parse(dt_cuenta.Select("Id_Prd='" + Id_Prd + "'")[0]["Cantidad"].ToString());
                        if ((dif + cuentaActual) > cantidadEnDt_cuentaOriginal)
                        {
                            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                            e.Canceled = true;
                            return;
                        }
                    }

                    //**************verificar que no pase lo que se ordenó ene l pedido*********
                    //if (edicionRemisionDePedido && dt_cuentaPedido.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                    //{
                    //    int cantidadEnPedido = int.Parse(dt_cuentaPedido.Select("Id_Prd='" + Id_Prd + "'")[0]["Cantidad"].ToString());
                    //    int cuentaActual = int.Parse(dt_cuenta.Select("Id_Prd='" + Id_Prd + "'")[0]["Cantidad"].ToString());
                    //    if ((dif + cuentaActual) > cantidadEnPedido)
                    //    {
                    //        Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                    //        e.Canceled = true;
                    //        return;
                    //    }
                    //}
                    //***************************************************************************
                    //sumar diferencia al dt_Cuenta
                    DataRow[] editable_drCuenta;
                    if (dt_cuenta.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                    {
                        editable_drCuenta = dt_cuenta.Select("Id_Prd='" + Id_Prd + "'");
                        editable_drCuenta[0].BeginEdit();
                        editable_drCuenta[0]["Cantidad"] = int.Parse(editable_drCuenta[0]["Cantidad"].ToString()) + dif;
                        editable_drCuenta[0].AcceptChanges();
                    }
                }
                else
                {
                    dif = cantidad_A - cantidad;

                    DataRow[] editable_drCuenta;
                    if (dt_cuenta.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                    {
                        editable_drCuenta = dt_cuenta.Select("Id_Prd='" + Id_Prd + "'");
                        editable_drCuenta[0].BeginEdit();
                        editable_drCuenta[0]["Cantidad"] = int.Parse(editable_drCuenta[0]["Cantidad"].ToString()) - dif;
                        editable_drCuenta[0].AcceptChanges();
                    }
                }







                //update dt_detalles
                DataRow[] editable_dr;
                editable_dr = null;

                if (dt_detalles.Select("Id_RemDet='" + Id_RemDet_A + "'").Length > 0)
                {
                    editable_dr = dt_detalles.Select("Id_RemDet='" + Id_RemDet_A + "'");

                    editable_dr[0].BeginEdit();
                    editable_dr[0]["cantidad"] = cantidad;
                    editable_dr[0]["Precio"] = precio;
                    editable_dr[0]["Importe"] = cantidad * precio;
                    editable_dr[0].AcceptChanges();
                }
                else {
                    throw new Exception("Error: No se pudo editar el detalle");
                }

                //se borran las cantidades anteriores del subtotal y se agregan las nuevas
                //subtotal -= costo_A * cantidad_A;
                CalcularTotales();
                //double iva_cd = 0;
                //new CN_CatCentroDistribucion().ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva_cd, session.Emp_Cnx);
                //subtotal += cantidad * precio;
                //IVA = double.Parse((subtotal * (iva_cd / 100)).ToString());
                //total = subtotal + IVA;
                //txtSub.Text = subtotal.ToString();
                //txtIva.Text = IVA.ToString();
                //txtTotal.Text = total.ToString();

                if (txtTipoId.Value == 60)
                {
                    double total = !string.IsNullOrEmpty(txtTotal.Text) ? Convert.ToDouble(txtTotal.Text) : 0;
                    if (total >= Convert.ToDouble(HiddenField2.Value))
                    {
                        valuacion.Visible = true;
                    }
                    else
                    {
                        valuacion.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                e.Canceled = true;
            }
            finally
            {
                RadAjaxManager1.ResponseScripts.Add("showcolum();");
            }
        }
        protected void rgDetalles_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                double iva_cd = 0;
                new CN_CatCentroDistribucion().ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva_cd, session.Emp_Cnx);

                string Id_RemDet = rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Id_RemDet"].Text;
                int cantidad = int.Parse((rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Cantidad"].FindControl("CantidadLabel") as Label).Text);
                double precio = double.Parse((rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Precio"].FindControl("PrecioLabel") as Label).Text);
                DataRow[] roww;
                roww = dt_detalles.Select("Id_RemDet='" + Id_RemDet  + "'");
                if (roww.Length != 1)
                {
                    throw new Exception(" ");
                    //return;
                }
                dt_detalles.Rows.Remove(roww[0]);

                CalcularTotales();
                //subtotal -= cantidad * precio;
                //IVA = float.Parse((subtotal * (iva_cd / 100)).ToString());
                //total = subtotal + IVA;
                //txtSub.Text = subtotal.ToString();
                //txtIva.Text = IVA.ToString();
                //txtTotal.Text = total.ToString();

                ///*QUITAR productos a la lista (dt_cuenta)*/
                DataRow[] editable_dr;
                int Id_Prd = int.Parse((rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Id_Prd"].FindControl("ProdLabel") as Label).Text);
                if (dt_cuenta.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                {
                    editable_dr = dt_cuenta.Select("Id_Prd='" + Id_Prd + "'");
                    editable_dr[0].BeginEdit();
                    editable_dr[0]["Cantidad"] = int.Parse(editable_dr[0]["Cantidad"].ToString()) - cantidad;
                    editable_dr[0].AcceptChanges();
                }
                else
                    throw new Exception(" ");
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                cacharMsgBaseDatos(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
            finally
            {
                RadAjaxManager1.ResponseScripts.Add("showcolum();");
            }
        }
        protected void cmbProductoDet_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                RadNumericTextBox estecombo = (sender as RadNumericTextBox);
                //if ((Telerik.Web.UI.GridDataInsertItem)estecombo.Parent.Parent != null)
                //{
                Telerik.Web.UI.GridDataInsertItem j = (Telerik.Web.UI.GridDataInsertItem)estecombo.Parent.Parent;

                int territorio = int.Parse((j["Terr"].FindControl("RadComboBoxTerr") as RadComboBox).SelectedValue);

                if (dt_detalles.Select("Id_Prd='" + estecombo.Text + "' and Terr='" + territorio + "'").Length > 0)
                {
                    AlertaFocus("El producto ya ha sido agregado a la remisión", estecombo.ClientID);
                    estecombo.Text = "";
                    (j.FindControl("DescripcionTextBox") as RadTextBox).Text = "";
                    (j.FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Text = "";
                    (j.FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Text = "";

                    return;
                }

                double precio_especial = -1;
                ClienteProd clienteProd = new ClienteProd();
                clienteProd.Id_Emp = session.Id_Emp;
                clienteProd.Id_Cd = session.Id_Cd_Ver;
                clienteProd.Id_Cte = Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1);
                clienteProd.Id_Prd = Convert.ToInt32(estecombo.Value.HasValue ? estecombo.Value : -1);

                RadNumericTextBox txtFac_Territorio = (RadNumericTextBox)estecombo.Parent.FindControl("txtter1");
                Producto prd = new Producto();
                //prd.Id_Ter = Convert.ToInt32(txtFac_Territorio.Value.HasValue ? txtFac_Territorio.Value : -1);
                prd.Id_Mov = (int?)txtTipoId.Value;
                try
                {
                    new CN_CatProducto().ConsultaProducto(ref prd, session.Emp_Cnx, session.Id_Emp, session.Id_Cd, Convert.ToInt32(estecombo.Value.HasValue ? estecombo.Value : -1));
                }
                catch (Exception ex)
                {
                    (j.FindControl("DescripcionTextBox") as RadTextBox).Text = "";
                    (j.FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Text = "";
                    (j.FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Text = "";
                    estecombo.Value = null;
                    AlertaFocus(ex.Message, estecombo.ClientID);
                    return;
                }
                new CN_CatClienteProd().ConsultaClienteProdPrecioEspecial(clienteProd, session.Emp_Cnx, ref precio_especial);

                (j.FindControl("DescripcionTextBox") as RadTextBox).Text = prd.Prd_Descripcion;

                //FRank: precio especial y precio publico se pueden modificar y no deben ser 0
                if (precio_especial != -1)
                {//rm precio especial no es modificable
                    (j.FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Text = precio_especial.ToString();//verificar control

                }
                else
                {
                    //no tiene precio especial el cliente-producto
                    if (!string.IsNullOrEmpty(prd.Prd_Precio))
                        (j.FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Text = prd.Prd_Precio /*precio_publico*/ == "-1" ? 0.ToString() : prd.Prd_Precio;//verificar control
                }
                (j.FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Focus();
                // }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void dpFecha_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                crearDT();
                crear_dt_cuenta();
                validarFecha();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadTabStrip1_TabClick(object sender, RadTabStripEventArgs e)
        {

        }
        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];


                RadNumericTextBox Txtcantidad = (sender as RadNumericTextBox);
                int cantidad = Txtcantidad.Value.HasValue ? (int)Txtcantidad.Value : 0;
                int prd = Convert.ToInt32((Txtcantidad.Parent.Parent.FindControl("RadNumericTextBox1") as RadNumericTextBox).Value);
                int ter = Convert.ToInt32((Txtcantidad.Parent.Parent.FindControl("txtter1") as RadNumericTextBox).Value);

                int disponible = 0;
                int invFinal = 0;
                int asignado = 0;
                int cantidad_B = 0;
                new CN_CapEntradaSalida().ConsultarDisponible(session, prd, ref disponible, ref invFinal, ref asignado);

                DataRow[] Dr = dt_detalles.Select("Id_Prd='" + prd + "' and Terr <> '" + ter + "'");

                Remision remision = new Remision();
                List<RemisionDet> detalle = new List<RemisionDet>();
                remision.Id_Emp = session.Id_Emp;
                remision.Id_Cd = session.Id_Cd_Ver;
                remision.Id_Rem = !string.IsNullOrEmpty(txtFolio.Text) ? Convert.ToInt32(txtFolio.Text) : -1;
                new CN_CapRemision().ConsultarRemisionesDetalle(session, remision, ref detalle);

                if (Dr.Length > 0)
                {
                    for (int i = 0; i < Dr.Length; i++)
                        cantidad_B += !string.IsNullOrEmpty(Dr[i]["Cantidad"].ToString()) ? Convert.ToInt32(Dr[i]["Cantidad"]) : 0;
                }
                int count = 0;
                foreach (RemisionDet rd in detalle)
                {
                    if (rd.Id_Prd == prd)
                        count += rd.Rem_Cant;
                }

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
                    dt2.Columns.Add("Prd_Presentacion"); dt2.Columns.Add("Prd_Unidad");
                    dt2.Columns.Add("Prd_Precio");
                    dt2.Columns.Add("Disponible");
                    dt2.Columns.Add("Prd_Importe");
                    dt2.Columns.Add("Id_Rem");
                    cappedido.ConsultaPedidoDetDisp(pedido, ref dt2, null, session.Emp_Cnx);

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

                //-----------------------------------------------------------
                int cantidadEnDt_cuentaOriginal = 0;
                //---si es REMISION DE PEDIDO, contar lo que ya se tenìa
                if (tipoDeMovimiento == 3 && dt_cuentaOriginal.Select("Id_Prd='" + prd + "'").Length > 0)
                {
                    cantidadEnDt_cuentaOriginal = int.Parse(dt_cuentaOriginal.Select("Id_Prd='" + prd + "'")[0]["Cantidad"].ToString());
                    if (dt_cuenta.Select("Id_Prd='" + prd + "'").Length > 0)
                    {
                        //int cuentaActual = int.Parse(dt_cuenta.Select("Id_Prd='" + prd + "'")[0]["Cantidad"].ToString());
                        if ((cantidad /*+ cuentaActual*/) > cantidadEnDt_cuentaOriginal)
                        {
                            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                            return;
                        }
                    }
                    else
                    {
                        if (cantidad > cantidadEnDt_cuentaOriginal)
                        {
                            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                            return;
                        }
                    }
                }

                //************* SI es edicion de una remision de pedido, verificar que la cantidad no supere lo del pedido***************
                if (edicionRemisionDePedido && dt_cuentaPedido.Select("Id_Prd='" + prd + "'").Length > 0)
                {
                    int cantidadEnPedido = int.Parse(dt_cuentaPedido.Select("Id_Prd='" + prd + "'")[0]["Cantidad"].ToString());
                    if (dt_cuenta.Select("Id_Prd='" + prd + "'").Length > 0)
                    {
                        int cuentaActual = int.Parse(dt_cuenta.Select("Id_Prd='" + prd + "'")[0]["Cantidad"].ToString());
                        cantidadEnDt_cuentaOriginal = int.Parse(dt_cuentaOriginal.Select("Id_Prd='" + prd + "'")[0]["Cantidad"].ToString());
                        if ((cantidad /*+ cuentaActual */ - cantidadEnDt_cuentaOriginal) > cantidadEnPedido)
                        {
                            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                            return;
                        }
                    }
                    else
                    {
                        if ((cantidad - cantidadEnDt_cuentaOriginal) > cantidadEnPedido)
                        {
                            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                            return;
                        }
                    }
                }

                //---------------------------------------------
                if (cantidad + cantidad_B > disponible + count + disponible_pedido)
                {
                    AlertaFocus("Producto <b>" + prd.ToString() + "</b> inventario disponible insuficiente,</br>inventario final: " + invFinal.ToString() + ",</br>asignado: " + asignado.ToString() + ",</br>disponible: " + (disponible + count + disponible_pedido).ToString(), Txtcantidad.ClientID);
                    Txtcantidad.Text = "";
                    return;
                }
                else
                {
                    (Txtcantidad.Parent.Parent.FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Focus();
                }

                if (tipoDeMovimiento == 2 && txtTipoId.Value == 60)
                {
                    int territorio = (int)((Txtcantidad.Parent.FindControl("txtter1") as RadNumericTextBox).Value.Value);
                    CN_CapEntradaSalida CNentrada = new CN_CapEntradaSalida();
                    int verificador = 0;
                    CNentrada.ConsultarSaldo(session.Id_Emp, session.Id_Cd_Ver, prd.ToString(), territorio.ToString(), txtClienteId.Text, session.Emp_Cnx, ref verificador, "60");

                    if (verificador - (cantidad_A - cantidad) < 0)
                    {
                        AlertaFocus("El producto " + prd.ToString() + " no cuenta con saldo suficiente", Txtcantidad.ClientID);
                        Txtcantidad.Text = "";
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                ErrorManager();
                switch (e.Argument.ToString())
                {
                    case "cliente":
                        txtClienteId.Text = Session["Id_Buscar" + Session.SessionID].ToString();
                        txtClienteId_TextChanged(null, null);
                        break;
                    case "precio":
                        (producto as RadNumericTextBox).DbValue = Session["Id_Buscar" + Session.SessionID];
                        cmbProductoDet_TextChanged(producto, null);
                        if ((producto as RadNumericTextBox).Value.HasValue)
                        {
                            ((producto as RadNumericTextBox).Parent.FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Focus();
                        }
                        break;
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 180);
                        RadPageViewDetalles.Height = altura;
                        RadPane1.Height = altura;
                        RadPane1.Width = RadPageViewDGenerales.Width;
                        RadSplitter1.Height = altura;
                        RadPageViewDGenerales.Height = altura;
                        break;
                    case "si":
                        Guardar(true);
                        break;
                    case "no":
                        Guardar(false);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ErrorManager();
                RadAjaxManager1.ResponseScripts.Add("popup();");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion 
        #region Funciones
        private bool validarFecha()
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                if (!((dpFecha.SelectedDate >= session.CalendarioIni) && (dpFecha.SelectedDate <= session.CalendarioFin)))
                {
                    Alerta("Fecha se encuentra fuera del periodo, favor de teclear la fecha correcta al periodo que se encuentra configurado el sistema");
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private string MaximoId()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, "CapRemision", "Id_Rem", Sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void llenar_cmbTipo()
        {
            cmbTipo.Items.Insert(1, new RadComboBoxItem("Remisiones", "0"));
        }
        private void CargarTipoMovimiento()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatMovimiento_ComboParaRemisiones", ref cmbTipoMovimiento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void cmbCliente_indiceCambia()
        {
            try
            {
                LimpiarCampos1();
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Clientes cliente = new Clientes();
                cliente.Id_Emp = sesion.Id_Emp;
                cliente.Id_Cd = sesion.Id_Cd_Ver;
                cliente.Id_Cte = Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1);
                cliente.Id_Rik = sesion.Id_Rik;

                ValidaClienteCuentaNacional();
                try
                {
                    new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);
                }
                catch (Exception ex)
                {
                    txtClienteId.Text = "";
                    txtCliente.Text = "";
                    AlertaFocus(ex.Message, txtClienteId.ClientID);
                    return;
                }
                txtCliente.Text = cliente.Cte_NomComercial;
                txtCalle.Text = cliente.Cte_Calle;
                txtNumero2.Text = cliente.Cte_Numero;
                txtCp.DbValue = (string.IsNullOrEmpty(cliente.Cte_Cp)) ? "" : cliente.Cte_Cp.ToString().Trim();
                txtColonia.Text = cliente.Cte_Colonia;
                txtMunicipio.Text = cliente.Cte_Municipio;
                txtEstado.Text = cliente.Cte_Estado;
                txtRfc.Text = cliente.Cte_DRfc;
                txtTelefono2.Text = cliente.Cte_Telefono;
                txtContacto.Text = cliente.Cte_Contacto;
                HiddenCteCuentaNacional.Value = cliente.Cte_RemisionElectronica.ToString();
                Id_CuentaNacional = cliente.Cte_RemisionElectronica;
                HiddenNumCuentaContNacional.Value = cliente.Cte_NumCuentaContNacional.ToString();               
                NumCuentaContNacional = cliente.Cte_NumCuentaContNacional == null ? 0:  cliente.Cte_NumCuentaContNacional;
               
                //List<Territorios> territorios = new List<Territorios>();
                //new CN_CatCliente().ConsultaTerritoriosDelCliente(cliente.Id_Cte.Value, sesion, ref territorios);
                cmbTerritorio_indiceCambiado();
                //CargarComboTerritorios(territorios);
                //new CN__Comun().LlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, cliente.Id_Cte.Value, sesion.Emp_Cnx, "spCatTerritorioCte_Combo", ref cmbTerritorio);
                if (cmbTerritorio.Items.Count < 2)
                    Alerta("El cliente no tiene territorios asociados");//verificarmensaje   
                if (IsPostBack)
                {
                    txtTerritorioId.Focus();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LimpiarCampos1()
        {
            txtCalle.Text = "";
            txtNumero2.Text = "";
            txtCp.Text = "";
            txtColonia.Text = "";
            txtMunicipio.Text = "";
            txtEstado.Text = "";
            txtRfc.Text = "";
            txtTelefono2.Text = "";
            txtContacto.Text = "";
            txtPedido.Text = "";
            txtConducto.Text = "";
            txtGuia2.Text = "";
            dtpFechaHora.SelectedDate = null;
            txtNota.Text = "";
            txtTerritorioId.Text = "";
            cmbTerritorio.Items.Clear();
            cmbTerritorio.Text = "";
            txtRepresentante.Text = "";
            txtRepresentanteStr.Text = "";
        }
        private void crearDT()
        {
            dt_detalles = new DataTable();
            dt_detalles.Columns.Add("Id_RemDet");
            dt_detalles.Columns.Add("Terr");
            dt_detalles.Columns.Add("Id_Prd");
            dt_detalles.Columns.Add("Descripcion");
            dt_detalles.Columns.Add("Cantidad", typeof(double));
            dt_detalles.Columns.Add("Precio", typeof(double));
            dt_detalles.Columns.Add("Importe", typeof(double));
            dt_detalles.Columns.Add("DescrTer");
        }
        private void CargarComboProducto(string Terr, ref RadComboBox cmbPrd, int Spo)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Terr == "" ? -1 : Convert.ToInt32(Terr), Spo, Sesion.Emp_Cnx, "spCatProductoTerr_Combo", ref cmbPrd);
        }
        private void Guardar(bool gen_contrato)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (!validarFecha())
                {
                    RadTabStrip1.SelectedIndex = 0;
                    RadMultiPage1.SelectedIndex = 0;
                    dpFecha.Focus();
                    return;
                }
                if (!validarCamposDetalle())
                {
                    RadTabStrip1.SelectedIndex = 0;
                    RadMultiPage1.SelectedIndex = 0;
                    return;
                }
                if (dt_detalles == null || dt_detalles.Rows.Count == 0)
                {
                    Alerta("Aún no se han capturado partidas");
                    return;
                }
                if (string.IsNullOrEmpty(txtSub.Text))
                {
                    Alerta("El total de la remisión no puede ser cero");
                    return;
                }
                foreach (DataRow row in dt_cuenta.Rows)
                { //solo se checa inventario si NO es Pedido
                    if (
                        tipoDeMovimiento == 1 ||
                        (tipoDeMovimiento == 2 && edicionRemisionDePedido == false) ||
                        (tipoDeMovimiento == 2 && edicionRemisionDePedido == true && dt_cuentaPedido.Select("Id_Prd='" + row["Id_Prd"].ToString() + "'").Length == 0) ||
                        (tipoDeMovimiento == 3 && dt_cuentaOriginal.Select("Id_Prd='" + row["Id_Prd"].ToString() + "'").Length == 0)
                       )
                    {
                        int disponible = -1;
                        int invFinal = -1;
                        int asignado = -1;
                        new CN_CapEntradaSalida().ConsultarDisponible(sesion, int.Parse(row["Id_Prd"].ToString()), ref disponible, ref invFinal, ref asignado);
                        // En caso que de sea una edicion y sea un producto que no era parte de un 
                        //pedido (no estaba asignado) verificar el disponible
                        int original = 0;
                        if (tipoDeMovimiento == 2 && dt_cuentaOriginal.Select("Id_Prd='" + row["Id_Prd"].ToString() + "'").Length > 0)
                            original = int.Parse(dt_cuentaOriginal.Select("Id_Prd='" + row["Id_Prd"].ToString() + "'")[0]["Cantidad"].ToString());

                        if (int.Parse(row["Cantidad"].ToString()) - original > disponible)
                        {// MSG asignado por antiguo sian
                            Alerta("Producto " + row["Id_Prd"].ToString() +
                                " inventario disponible insuficiente, inventario final: " + invFinal.ToString() +
                                ", asignado: " + asignado.ToString() + ", disponible: " + disponible.ToString());
                            return;
                        }
                    }
                    //************* SI es edicion de una remision de pedido, verificar que la cantidad no supere lo del pedido***************
                    if (edicionRemisionDePedido && dt_cuentaPedido.Select("Id_Prd='" + row["Id_Prd"].ToString() + "'").Length > 0)
                    {
                        int cantidadEnPedido = int.Parse(dt_cuentaPedido.Select("Id_Prd='" + row["Id_Prd"].ToString() + "'")[0]["Cantidad"].ToString());
                        int cantidadEnDt_cuentaOriginal = int.Parse(dt_cuentaOriginal.Select("Id_Prd='" + row["Id_Prd"].ToString() + "'")[0]["Cantidad"].ToString());
                        if (int.Parse(row["Cantidad"].ToString()) - cantidadEnDt_cuentaOriginal > cantidadEnPedido)
                        {
                            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                            return;
                        }
                    }
                    //----------------------------------------------------------------------------------------------------------------------
                }
                if (txtTipoId.Value == 60 && valuacion.Visible)
                {
                    if (txtValuacion.Value.HasValue)
                    {
                        List<ValuacionProyectoDetalle> list = new List<ValuacionProyectoDetalle>();
                        ValuacionProyecto valuacionProyecto = new ValuacionProyecto();
                        valuacionProyecto.Id_Emp = sesion.Id_Emp;
                        valuacionProyecto.Id_Cd = sesion.Id_Cd_Ver;
                        valuacionProyecto.Id_Vap = (int)txtValuacion.Value.Value;

                        new CN_CapValuacionProyecto().ConsultarValuacionProyecto(ref valuacionProyecto, sesion.Emp_Cnx);
                        list = valuacionProyecto.ListaProductosValuacionProyecto;

                        double total_valuacion = 0;
                        foreach (DataRow row in dt_detalles.Rows)
                        {
                            foreach (ValuacionProyectoDetalle vpd in list)
                            {
                                total_valuacion += vpd.Vap_Cantidad * vpd.Vap_Costo;
                                if (row["Id_Prd"].ToString() == vpd.Id_Prd.ToString())
                                {
                                    if (vpd.Estatus.ToLower() != "autorizado")
                                    {
                                        RadTabStrip1.Tabs[0].Selected = true;
                                        RadMultiPage1.SelectedIndex = 0;
                                        AlertaFocus("El producto " + row["Id_Prd"] + " no esta autorizado", txtValuacion.ClientID);
                                        return;
                                    }
                                }
                                else
                                {
                                    RadTabStrip1.Tabs[0].Selected = true;
                                    RadMultiPage1.SelectedIndex = 0;
                                    AlertaFocus("El producto no se encontro en la valuación de proyectos capturada", txtValuacion.ClientID);
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        RadTabStrip1.Tabs[0].Selected = true;
                        RadMultiPage1.SelectedIndex = 0;
                        AlertaFocus("Por favor capture el código de valuación de proyecto autorizado", txtValuacion.ClientID);
                        return;
                    }
                }

                Remision remision = new Remision();
                Funciones funcion = new Funciones();
                remision.Id_Emp = sesion.Id_Emp;
                remision.Id_Cd = sesion.Id_Cd_Ver;
                remision.Id_Rem = tipoDeMovimiento == 2 ? Id_Rem_Actualiza : -1;
                remision.Rem_ManAut = 1; // manual
                remision.Rem_Tipo = "3"; // 3=remision
                remision.Rem_Fecha = DateTime.Parse(dpFecha.SelectedDate.Value.ToString("dd/MM/yyyy") + " " + funcion.GetLocalDateTime(sesion.Minutos).ToString("HH:mm:ss"));
                remision.Id_Tm = int.Parse(cmbTipoMovimiento.SelectedValue);
                if (tipoDeMovimiento == 3 || (tipoDeMovimiento == 2 && edicionRemisionDePedido))
                    remision.Id_Ped = int.Parse(txtPedido.Text);
                else
                    remision.Id_Ped = -1;
                remision.Id_Cte = Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1);
                remision.Id_Ter = int.Parse(cmbTerritorio.SelectedValue);
                remision.Id_Rik = int.Parse(txtRepresentante.Text);
                remision.Id_U = sesion.Id_U;
                remision.Rem_Calle = txtCalle.Text;
                remision.Rem_Numero = txtNumero2.Text;
                remision.Rem_Cp = txtCp.Text;
                remision.Rem_Colonia = txtColonia.Text;
                remision.Rem_Municipio = txtMunicipio.Text;
                remision.Rem_Estado = txtEstado.Text;
                remision.Rem_Rfc = txtRfc.Text;
                remision.Rem_Telefono = txtTelefono2.Text;
                remision.Rem_Contacto = txtContacto.Text;
                remision.Rem_Conducto = txtConducto.Text;
                remision.Rem_Guia = txtGuia2.Text;
                remision.Rem_FechaEntrega = dtpFechaHora.SelectedDate;
                remision.Rem_Nota = txtNota.Text;
                remision.Rem_Subtotal = Convert.ToDouble(txtSub.Text);// subtotal;
                remision.Rem_Iva = Convert.ToDouble(txtIva.Text);// IVA;
                remision.Rem_Total = Convert.ToDouble(txtTotal.Text);// total;
                remision.Rem_Estatus = "C";
                remision.Rem_OrdenCompra =  txtOrdenCompra.Text;
                remision.Id_Vap = (int?)txtValuacion.Value;
                remision.Rem_CteCuentaNacional = Id_CuentaNacional;
                remision.Rem_CteCuentaContNacional = NumCuentaContNacional;
                List<RemisionDet> detalles = new List<RemisionDet>();
                RemisionDet remdetalle = new RemisionDet();
                foreach (DataRow row in dt_detalles.Rows)
                {
                    remdetalle = new RemisionDet();
                    remdetalle.Id_Emp = sesion.Id_Emp;
                    remdetalle.Id_Cd = sesion.Id_Cd_Ver;
                    remdetalle.Id_RemDet = int.Parse(row["Id_RemDet"].ToString());
                    remdetalle.Id_Ter = int.Parse(row["Terr"].ToString());
                    remdetalle.Id_Prd = int.Parse(row["Id_Prd"].ToString());
                    remdetalle.Rem_Cant = int.Parse(row["Cantidad"].ToString());
                    remdetalle.Rem_Precio = double.Parse(row["Precio"].ToString());
                    //si es edicion de remision de pedido
                    if (tipoDeMovimiento == 2 && edicionRemisionDePedido == true && dt_cuentaPedido.Select("Id_Prd='" + row["Id_Prd"].ToString() + "'").Length > 0)
                        remdetalle.Ped_Pertenece = true;

                    //si es remision de pedido
                    if (tipoDeMovimiento == 3 && dt_cuentaOriginal.Select("Id_Prd='" + row["Id_Prd"].ToString() + "'").Length > 0)
                        remdetalle.Ped_Pertenece = true;
                    detalles.Add(remdetalle);
                }

                // Evita que se guarde el documento si los totales no coinciden
                if (Session["ListaProductosRemisionEspecial" + Session.SessionID] != null)
                {
                    if (Session["RemEspecialGuardada" + Session.SessionID].ToString() == "1")
                    {
                        double totalEspecial = 0;
                        foreach (RemisionDet ncd in (List<RemisionDet>)Session["ListaProductosRemisionEspecial" + Session.SessionID])
                        {
                            totalEspecial += ncd.Rem_Importe;
                        }


                        // Se indico que solo podía haber diferecia de 90 centavos
                        double TE1 = Math.Round(totalEspecial, 2) + .90; // se suman 70 centavos al total especial -- modificar si se desea disminuir o aumentar el rango
                        double TE2 = Math.Round(totalEspecial, 2) - .90; // se restan 70 centavos al total especia


                        if (Math.Round(txtSub.Value.Value, 2) <= TE2 && Math.Round(txtSub.Value.Value, 2)  >= TE1 )
                        {
                            Alerta("El total del documento especial solo puede tener una diferencia de 90 centavos en total con repecto a el documento original");
                            return;
                        }
                    }
                }

                int verificador = -1;
                int Id_Rem = 0;
                bool tipoMovimento = false;
                string mensaje = "";
                try
                {
                    new CN_CapRemision().GuardarRemision(remision, detalles, sesion, ref verificador, tipoDeMovimiento == 2, gen_contrato, ref Id_Rem, ref tipoMovimento, ref mensaje);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("saldo_insuficiente") || ex.Message.Contains("error"))
                    {
                        Alerta(ex.Message.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)[0]);
                        return;
                    }
                    else
                    {
                        if (ex.Message.Contains("no cuenta con inventario suficiente"))
                        {
                            Alerta(ex.ToString());
                        }
                        else
                        {
                            throw ex;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(mensaje))
                {
                    Alerta(mensaje);
                    return;
                }
                else
                {//SI GUARDÓ BIEN LA REMISION:
                    //Guardar los datos de los productos de remision especial
                    if (Session["ListaProductosRemisionEspecial" + Session.SessionID] != null)
                    {
                        if (Session["RemEspecialGuardada" + Session.SessionID].ToString() == "1") //guarda solo si hizo clic en guardar en pantalla de RemisionEspecial.
                        {
                            FacturaEspecial facturaEsp = new FacturaEspecial();
                            facturaEsp.Id_Emp = remision.Id_Emp;
                            facturaEsp.Id_Cd = remision.Id_Cd;
                            facturaEsp.Id_Fac = verificador;
                            facturaEsp.Id_Ter = Convert.ToInt32(remision.Id_Ter);
                            facturaEsp.FacEsp_Fecha = remision.Rem_Fecha;
                            facturaEsp.FacEsp_Importe = Convert.ToDouble(remision.Rem_Total);
                            facturaEsp.FacEsp_SubTotal = Convert.ToDouble(remision.Rem_Subtotal);
                            facturaEsp.FacEsp_ImporteIva = Convert.ToDouble(remision.Rem_Iva);
                            facturaEsp.FacEsp_Total = Convert.ToDouble(remision.Rem_Total);
                            List<RemisionDet> listaProductosRemisionEspecial = (List<RemisionDet>)Session["ListaProductosRemisionEspecial" + Session.SessionID];
                            new CN_CatClienteProd().ModificarRemisionEspecial(ref facturaEsp, ref listaProductosRemisionEspecial, sesion.Emp_Cnx, ref verificador, string.IsNullOrEmpty(this.hiddenId.Value) ? 0 : 1);
                        }
                    }
                    //EL sp de insertar remision cambia el estatus del pedido en caso de que sea remision de pedido
                    if (tipoMovimento)
                    {
                        Session["PreguntarImpresion" + Session.SessionID] = Id_Rem;
                    }
                    new CN_Rendimientos().InsertarRendimientosRemisiones(sesion, sesion.Emp_Cnx, Session.SessionID, ref Id_Rem, ref verificador);
                    RadAjaxManager1.ResponseScripts.Add(string.Concat(@"CloseWindow('", "Los datos se guardaron correctamente", "')"));
                    Nuevo();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("deadlocked"))
                {
                    Alerta("El servidor esta tardando en responder, por favor de click en guardar nuevamente");
                }
                else
                {
                    throw ex;
                }
            }
        }
        private void cmbTerritorio_indiceCambiado()
        {
            try
            { //consultar representante del territorio seleccionado
                //if (int.Parse(cmbTerritorio.SelectedValue) > 0)
                //{
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<Territorios> listaTerritorios = new List<Territorios>();
                new CN_CatCliente().ConsultaTerritoriosDelCliente(Convert.ToInt32(txtClienteId.Value.Value), sesion, ref listaTerritorios);

                cmbTerritorio.DataTextField = "Descripcion";
                cmbTerritorio.DataValueField = "Id_Ter";
                cmbTerritorio.DataSource = listaTerritorios;
                cmbTerritorio.DataBind();
                if (cmbTerritorio.Items.Count > 1)
                {

                    cmbTerritorio.SelectedIndex = 1;
                    cmbTerritorio.Text = cmbTerritorio.Items[1].Text;
                    txtTerritorioId.Text = cmbTerritorio.Items[1].Value;

                    CN_CatTerritorios territorio = new CN_CatTerritorios();
                    Territorios ter = new Territorios();
                    ter.Id_Emp = sesion.Id_Emp;
                    ter.Id_Cd = sesion.Id_Cd_Ver;
                    ter.Id_Ter = Convert.ToInt32(cmbTerritorio.Items[1].Value);
                    territorio.ConsultaTerritoriosCombo(ref ter, sesion.Emp_Cnx);
                    txtRepresentante.Text = ter.Id_Rik.ToString();
                    txtRepresentanteStr.Text = ter.Rik_Nombre;
                }
                if (cmbTerritorio.Items.Count < 2)
                    Alerta("El cliente no tiene territorios asociados");//verificarmensaje   
                if (IsPostBack)
                {
                    txtTerritorioId.Focus();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void crear_dt_cuenta()
        {
            dt_cuenta = new DataTable();
            dt_cuenta.Columns.Add("Id_Prd");
            dt_cuenta.Columns.Add("Cantidad");
        }
        private void crear_dt_cuentaPedido()
        {
            dt_cuentaPedido = new DataTable();
            dt_cuentaPedido.Columns.Add("Id_Detalle");
            dt_cuentaPedido.Columns.Add("Id_Prd");
            dt_cuentaPedido.Columns.Add("Cantidad");
        }
        private void Nuevo()
        {
            LimpiarCampos1();
            ReiniciarVariables();
            txtTipoId.Text = "";
            cmbTipoMovimiento.SelectedValue = "-1";
            txtClienteId.Text = "";
            txtCliente.Text = "";
            rgDetalles.Rebind();
            txtFolio.Text = MaximoId();
            CalcularTotales();
            //txtSub.Text = subtotal.ToString();
            //txtIva.Text = IVA.ToString();
            //txtTotal.Text = total.ToString();
            RadTabStrip1.SelectedIndex = 0;
            RadMultiPage1.SelectedIndex = 0;
        }
        private bool validarCamposDetalle()
        {
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];

            if (cmbTipoMovimiento.SelectedValue == "" || cmbTipoMovimiento.SelectedValue == "-1")
            {
                Alerta("Favor de capturar todos los campos de la pestaña datos generales");
                return false;
            }
            if (!txtClienteId.Value.HasValue)
            {
                Alerta("Favor de capturar todos los campos de la pestaña datos generales");
                return false;
            }
            if (cmbTerritorio.SelectedValue == "" || cmbTerritorio.SelectedValue == "-1")
            {
                Alerta("Favor de capturar todos los campos de la pestaña datos generales");
                return false;
            }
            if (tipoDeMovimiento == 3 && txtPedido.Text == "")
            {
                Alerta("Favor de capturar todos los campos de la pestaña datos generales");
            }
            return true;
        }
        private void ReiniciarVariables()
        {
            crearDT();
            crear_dt_cuenta();
            //subtotal = 0;
            //IVA = 0;
            //total = 0;
            Tm_ReqSpo = false;
            id_detalle = 0;
            Id_Rem_Actualiza = -1;
            edicionRemisionDePedido = false;
        }
        private void cargarProductosSpo(ref RadComboBox combo_a_llenar)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatProducto_ConsultaSpoLista", ref combo_a_llenar);
                combo_a_llenar.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void cacharMsgBaseDatos(Exception exception, string metodoCachaExcepcion)
        {
            string[] Msgs =
            {     //spCapRemisionDet_Insertar
                    "No puede asignar una cantidad 0"
                    ,"No puede asignar precio 0"
                    ,"inventario disponible insuficiente, inventario final:"
                    ,"Se sobrepasa la cantidad disponible a remisionar para este pedido - producto"
                };
            bool msgConosido = false;
            foreach (string men in Msgs)
            {
                if (exception.Message.Contains(men))
                    msgConosido = true;
            }

            if (msgConosido)
            {
                Alerta(exception.Message);
            }
            else
            {
                ErrorManager(exception, metodoCachaExcepcion);
            }
        }
        private void Inicializar()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            Session["ListaProductosRemisionEspecial" + Session.SessionID] = null;
            Session["RemEspecialGuardada" + Session.SessionID] = "0";

            Nuevo();
            switch (Request.QueryString["tdm"])
            {
                case "1":
                    //NUEVO remision
                    tipoDeMovimiento = 1;
                    txtFolio.Text = MaximoId();
                    break;
                #region EDICION de remision
                case "2":
                    //EDICION de remision
                    tipoDeMovimiento = 2;
                    int Id_Rem = -1;
                    int.TryParse(Request.QueryString["Id_Rem"], out Id_Rem);
                    Remision remision = new Remision();
                    new CN_CapRemision().ConsultarEncabezadoImprimir(sesion, Id_Rem, ref remision, 0);

                    if (remision.Id_Tm == 60)
                    {
                        if (remision.Rem_Total >= Convert.ToDouble(HiddenField2.Value))
                            valuacion.Visible = true;
                        else
                            valuacion.Visible = false;
                    }
                    Id_Rem_Actualiza = remision.Id_Rem;
                    dpFecha.Enabled = false;
                    cmbTipoMovimiento.Enabled = false;
                    txtTipoId.Enabled = false;
                    txtCliente.Enabled = false;
                    txtClienteId.Enabled = false;
                    cmbTerritorio.Enabled = false;
                    txtTerritorioId.Enabled = false;

                    txtFolio.Text = remision.Id_Rem.ToString();
                    cmbTipoMovimiento.SelectedValue = remision.Id_Tm.ToString();
                    cmbTipoMovimiento.Text = cmbTipoMovimiento.FindItemByValue(remision.Id_Tm.ToString()).Text;
                    txtTipoId.Text = remision.Id_Tm.ToString();
                    txtCliente.Text = "";
                    txtClienteId.Text = remision.Id_Cte.ToString();
                    HiddenCteCuentaNacional.Value =  remision.Rem_CteCuentaNacional.ToString();
                    cmbCliente_indiceCambia();

                    cmbTerritorio_indiceCambiado();

                    cmbTerritorio.SelectedIndex = cmbTerritorio.FindItemIndexByValue(remision.Id_Ter.ToString());
                    cmbTerritorio.Text = cmbTerritorio.FindItemByValue(remision.Id_Ter.ToString()).Text;
                    txtTerritorioId.Text = remision.Id_Ter.ToString();

                    txtRepresentanteStr.Text = remision.Rik_Nombre;
                    txtRepresentante.Text = remision.Id_Rik.ToString();
                    txtValuacion.DbValue = remision.Id_Vap;
                    dpFecha.SelectedDate = remision.Rem_Fecha;
                    txtPedido.Text = remision.Id_Ped == -1 ? "" : remision.Id_Ped.ToString();
                    if (remision.Id_Ped > 0)
                    {
                        edicionRemisionDePedido = true;
                        crear_dt_cuentaPedido();
                        DataTable tabla = new DataTable();
                        tabla.Columns.Add("Id_PedDet");
                        tabla.Columns.Add("Id_Ter");
                        tabla.Columns.Add("Ter_Nombre");
                        tabla.Columns.Add("Id_Prd");
                        tabla.Columns.Add("Prd_Descripcion");
                        tabla.Columns.Add("Prd_Presentacion");
                        tabla.Columns.Add("Prd_Unidad");
                        tabla.Columns.Add("Prd_Precio");
                        tabla.Columns.Add("Prd_Cantidad");
                        tabla.Columns.Add("Prd_Importe");
                        tabla.Columns.Add("Id_Rem");
                        tabla.DefaultView.Sort = "Id_PedDet";
                        Pedido ped = new Pedido();
                        ped.Id_Emp = sesion.Id_Emp;
                        ped.Id_Cd = sesion.Id_Cd_Ver;
                        ped.Id_Ped = remision.Id_Ped;
                        new CN_CapPedido().ConsultaPedidoDetDisp(ped, ref tabla, null, sesion.Emp_Cnx);
                        ArrayList lista = new ArrayList();
                        foreach (DataRow row in tabla.Rows)
                        {
                            meterPedido_aDT_cuentaPedido(int.Parse(row["Id_PedDet"].ToString()), int.Parse(row["Id_Ter"].ToString()), int.Parse(row["Id_Prd"].ToString()),
                                                row["Prd_Descripcion"].ToString(), int.Parse(row["Prd_Cantidad"].ToString()), double.Parse(row["Prd_Precio"].ToString()),
                                                double.Parse(row["Prd_Importe"].ToString()));
                        }
                    }
                    txtCalle.Text = remision.Rem_Calle;
                    txtNumero2.Text = remision.Rem_Numero;
                    txtCp.Text = remision.Rem_Cp;
                    txtColonia.Text = remision.Rem_Colonia;
                    txtMunicipio.Text = remision.Rem_Municipio;
                    txtEstado.Text = remision.Rem_Estado;
                    txtRfc.Text = remision.Rem_Rfc;
                    txtTelefono2.Text = remision.Rem_Telefono;
                    txtContacto.Text = remision.Rem_Contacto;
                    txtConducto.Text = remision.Rem_Conducto;
                    txtGuia2.Text = remision.Rem_Guia;
                    dtpFechaHora.SelectedDate = remision.Rem_FechaEntrega;
                    txtNota.Text = remision.Rem_Nota;
                    txtOrdenCompra.Text =  remision.Rem_OrdenCompra.ToString();

                    List<RemisionDet> detalles = new List<RemisionDet>();
                    new CN_CapRemision().ConsultarRemisionesDetalle(sesion, remision, ref detalles);
                    foreach (RemisionDet detalle in detalles)
                    {
                        meterPedido_aDTs(detalle.Id_RemDet, detalle.Id_Ter == null ? -1 : int.Parse(detalle.Id_Ter.ToString()),
                                        detalle.Id_Prd, detalle.Prd_Descripcion, detalle.Rem_Cant, detalle.Rem_Precio,
                                        detalle.Rem_Cant * detalle.Rem_Precio, detalle.Ter_Nombre);
                    }
                    CalcularTotales();
                    //Sacar copia de los DT originales para compararlo y no pasar de esa cantidad                    
                    dt_cuentaOriginal = dt_cuenta.Clone();
                    dt_cuentaOriginal.Merge(dt_cuenta);

                    CargarEspecial(Id_Rem, sesion, remision.Id_Cte);
                    break;
                #endregion
                #region REMISION DE PEDIDO
                case "3":
                    //REMISION DE PEDIDO
                    tipoDeMovimiento = 3;
                    int PedRem = -1;
                    int.TryParse(Request.QueryString["PedRem"], out PedRem);
                    Pedido pedido = new Pedido();
                    pedido.Id_Emp = sesion.Id_Emp;
                    pedido.Id_Cd = sesion.Id_Cd_Ver;
                    pedido.Id_Ped = PedRem;
                    pedido.Filtro_Doc = "R";
                    new CN_CapPedido().ConsultaPedido(ref pedido, sesion.Emp_Cnx); //new CN_CapPedido().ConsultaPedidoDet(pedido, ref tabla, sesion.Emp_Cnx);
                    if (pedido.Id_Cte == 0 && pedido.Ped_Importe == 0 && pedido.Id_Ter == 0)
                    {  //cerrar ventana
                        RadAjaxManager1.ResponseScripts.Add(string.Concat(@"CloseWindow('", "No existe el pedido a remisionar", "')"));
                        return;
                    }
                    else
                    {//si existe pedido
                        txtCliente.Enabled = false;
                        txtClienteId.Enabled = false;
                        cmbTerritorio.Enabled = false;
                        txtTerritorioId.Enabled = false;
                        dtpFechaHora.Enabled = false;

                        txtFolio.Text = MaximoId();
                        txtCliente.Text = pedido.Cte_NomComercial;
                        txtClienteId.Text = pedido.Id_Cte.ToString();
                        cmbCliente_indiceCambia();
                        txtOrdenCompra.Text = pedido.Ped_OrdenEntrega;


                        cmbTerritorio_indiceCambiado();
                        try
                        {
                            cmbTerritorio.SelectedIndex = cmbTerritorio.FindItemIndexByValue(pedido.Id_Ter.ToString());
                            cmbTerritorio.Text = cmbTerritorio.FindItemByValue(pedido.Id_Ter.ToString()).Text;
                            txtTerritorioId.Text = pedido.Id_Ter.ToString();
                        }
                        catch
                        {


                        }

                        txtRepresentanteStr.Text = pedido.Rik_Nombre;
                        txtRepresentante.Text = pedido.Id_Rik.ToString();
                        txtConducto.Text = pedido.Ped_Solicito;
                        txtPedido.Text = PedRem.ToString();

                        txtCalle.Text = pedido.Ped_ConsignadoCalle;
                        txtNumero2.Text = pedido.Ped_ConsignadoNo;
                        txtCp.Text = pedido.Ped_ConsignadoCp;
                        txtMunicipio.Text = pedido.Ped_ConsignadoMunicipio;
                        txtEstado.Text = pedido.Ped_ConsignadoEstado;
                        txtColonia.Text = pedido.Ped_ConsignadoColonia;

                        //cargar detalles
                        DataTable tabla = new DataTable();
                        tabla.Columns.Add("Id_PedDet");
                        tabla.Columns.Add("Id_Ter");
                        tabla.Columns.Add("Ter_Nombre");
                        tabla.Columns.Add("Id_Prd");
                        tabla.Columns.Add("Prd_Descripcion");
                        tabla.Columns.Add("Prd_Presentacion");
                        tabla.Columns.Add("Prd_Unidad");
                        tabla.Columns.Add("Prd_Precio");
                        tabla.Columns.Add("Prd_Cantidad");//en lugar de la cantidad, cargar "disponible de remision"
                        tabla.Columns.Add("Prd_Importe");
                        tabla.Columns.Add("Id_Rem");
                        tabla.DefaultView.Sort = "Id_PedDet";
                        new CN_CapPedido().ConsultaPedidoDetDisp(pedido, ref tabla, null, sesion.Emp_Cnx);

                        foreach (DataRow row in tabla.Rows)
                        {
                            meterPedido_aDTs(int.Parse(row["Id_PedDet"].ToString()), int.Parse(row["Id_Ter"].ToString()), int.Parse(row["Id_Prd"].ToString()),
                                                row["Prd_Descripcion"].ToString(), int.Parse(row["Prd_Cantidad"].ToString()), double.Parse(row["Prd_Precio"].ToString()),
                                                double.Parse(row["Prd_Importe"].ToString()), row["Ter_Nombre"].ToString());
                        }
                        //Sacar copia de los DT originales para compararlo y no pasar de esa cantidad                        
                        dt_cuentaOriginal = dt_cuenta.Clone();
                        dt_cuentaOriginal.Merge(dt_cuenta);
                    }
                    CalcularTotales();
                    break;
                #endregion
                default:
                    break;
            }
            _PermisoGuardar = Convert.ToBoolean(Request.QueryString["PermisoGuardar"]);
            _PermisoModificar = Convert.ToBoolean(Request.QueryString["PermisoGuardar"]);
            _PermisoEliminar = Convert.ToBoolean(Request.QueryString["PermisoEliminar"]);
            _PermisoImprimir = Convert.ToBoolean(Request.QueryString["PermisoImprimir"]);
            botones_radtoolbar();//esconde o muestra los botones grabar , nuevo , imprimir , etc segun los permisos           
        }
        private void botones_radtoolbar()
        {
            ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible = _PermisoGuardar;//actualizacionDocumento ? _PermisoModificar : true;
            ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("new")).Visible = tipoDeMovimiento == 1;
        }
        private void meterPedido_aDTs(int Id_Detalle, int territorio, int Id_Prd, string descripcion, int cantidad, double precio, double importe, string Ter_Nombre)
        {
            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                //verificar si el articulo no es sistema de propietarios
                Producto producto = new Producto();
                producto.Id_Cte = txtClienteId.Value.HasValue ? Convert.ToInt32(txtClienteId.Value.Value) : 0;
                new CN_CatProducto().ConsultaProducto(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Id_Prd);

                if (!((bool)producto.Prd_AparatoSisProp))
                {//este articulo no es spo y no se puede seleccionar movimiento 60
                    hayProductosNoSpo = true;
                }

                //agregar al dt
                DataRow[] editable_dr;
                if (dt_cuenta.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                {
                    editable_dr = dt_cuenta.Select("Id_Prd='" + Id_Prd + "'");
                    editable_dr[0].BeginEdit();
                    editable_dr[0]["Cantidad"] = int.Parse(editable_dr[0]["Cantidad"].ToString()) + cantidad;
                    editable_dr[0].AcceptChanges();
                }
                else
                {
                    dt_cuenta.Rows.Add(new object[] { Id_Prd, cantidad });
                }

                if (cantidad > 0)
                {
                    double iva_cd = 0;
                    new CN_CatCentroDistribucion().ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva_cd, session.Emp_Cnx);
                    dt_detalles.Rows.Add(new object[] { Id_Detalle, territorio, Id_Prd, descripcion, cantidad, precio, importe, Ter_Nombre });
                    id_detalle = ++Id_Detalle;
                    //CalcularTotales();
                    //subtotal += cantidad * precio;
                    //IVA = double.Parse((subtotal * (iva_cd / 100)).ToString());
                    //total = subtotal + IVA;
                    //txtSub.Text = subtotal.ToString();
                    //txtIva.Text = IVA.ToString();
                    //txtTotal.Text = total.ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void CalcularTotales()
        {
            try
            {
                double importeTotal = 0;
                double iva_cd = 0;
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                new CN_CatCentroDistribucion().ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva_cd, session.Emp_Cnx);

                if (dt_detalles.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_detalles.Rows.Count; i++)
                    {
                        importeTotal += Convert.ToDouble(dt_detalles.Rows[i]["Importe"].ToString());
                    }
                }

                double subtotal = importeTotal;
                double IVA = double.Parse((subtotal * (iva_cd / 100)).ToString());
                double total = subtotal + IVA;
                txtSub.Text = subtotal.ToString();
                txtIva.Text = IVA.ToString();
                txtTotal.Text = total.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void meterPedido_aDT_cuentaPedido(int Id_Detalle, int territorio, int Id_Prd, string descripcion, int cantidad, double precio, double importe)
        {
            try
            { //agregar al dt
                DataRow[] editable_dr;
                if (dt_cuentaPedido.Select("Id_Prd='" + Id_Prd + "' and Id_Detalle='" + Id_Detalle + "'").Length > 0)
                {
                    editable_dr = dt_cuenta.Select("Id_Prd='" + Id_Prd + "'");
                    editable_dr[0].BeginEdit();
                    editable_dr[0]["Cantidad"] = int.Parse(editable_dr[0]["Cantidad"].ToString()) + cantidad;
                    editable_dr[0].AcceptChanges();
                }
                else
                    dt_cuentaPedido.Rows.Add(new object[] { Id_Detalle, Id_Prd, cantidad });
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void CerrarVentana()
        {
            try
            {
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
        private void CargarEspecial(int Id_Ncr, Sesion sesion, int Id_Cte)
        {
            //- Especial
            List<RemisionDet> listaProdRemisionEspecialFinal = new List<RemisionDet>();
            new CN_CapRemision().ConsultaRemisionEspecialDetalle(ref listaProdRemisionEspecialFinal
                , sesion.Emp_Cnx
                , sesion.Id_Emp
                , sesion.Id_Cd_Ver
                , Id_Ncr
                , Id_Cte);

            if (listaProdRemisionEspecialFinal.Count > 0)
            {
                Session["ListaProductosRemisionEspecial" + Session.SessionID] = listaProdRemisionEspecialFinal;
                Session["RemEspecialGuardada" + Session.SessionID] = 1;
            }
            //-
        }
        #endregion
        #region ErrorManager
        private void AlertaFocus(string mensaje, string rtb)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
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
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje + "', 340, 150);");
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
        protected void txtProducto_Load(object sender, EventArgs e)
        {
            producto = sender;
        }
    }
}