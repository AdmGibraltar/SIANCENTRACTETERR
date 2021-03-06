﻿using Telerik.Web.UI;
using System;
using CapaEntidad;
using CapaNegocios;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web.UI.WebControls;
using System.Text;
using System.Net;
using CapaDatos;
using System.Web.UI;

namespace SIANWEB
{
    public partial class CapValProyectos : System.Web.UI.Page
    {
        #region Variables
        public string FechaEnable
        {
            get
            {
                return _Editable;
            }
        }
        private int Id_Vap = 0;
        private int Id_Cd = 0;
        private int Id_Emp = 0;
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
        private string _Editable;
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

        //Propiedad de lista de productos (partidas) de la Nota de cargo
        private List<ValuacionProyectoDetalle> ListaProductosValProyecto
        {
            get { return (List<ValuacionProyectoDetalle>)Session["ListaProductosValProyecto"]; }
            set { Session["ListaProductosValProyecto"] = value; }
        }

        //Propiedad de lista de tipo de Moneda
        private List<TipoMoneda> ListaTipoMoneda
        {
            get { return (List<TipoMoneda>)Session["ListaTipoMoneda"]; }
            set { Session["ListaTipoMoneda"] = value; }
        }

        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    RAM1.ResponseScripts.Add("RefreshParentPage()");
                }
                else
                    if (!Page.IsPostBack)
                    { //obtener valores desde la URL
                        parametros();
                        this.Inicializar(Id_Emp, Id_Cd, Id_Vap);
                        double ancho = 0;
                        foreach (GridColumn gc in rgDetalle.Columns)
                        {
                            if (gc.Display)
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                        }
                        rgDetalle.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgDetalle.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        txtFecha.Focus();
                    }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!Page.IsPostBack)
                deshabilitaControles();
        }

        private void deshabilitaControles()
        {
            Sesion vSesion = (Sesion)Session["Sesion" + Session.SessionID];
            List<TipoUsuario> listTipoUsuario = new List<TipoUsuario>();
            List<Control> listControls = new List<Control>();

            List<Type> vCommonTypes = new List<Type>() { typeof(RadNumericTextBox),
                                                     typeof(RadTextBox),
                                                     typeof(RadDatePicker),
                                                     typeof(RadComboBox),
                                                     typeof(CheckBox),
                                                     typeof(RadGrid),
                                                     typeof(ImageButton),
                                                     typeof(Button)
            };

            List<string> vControles = new List<string>() { "txtCuentasPorCobrar", "txtInventario", "txtInversionactivosfijos", "txtFinanciamientoproveedores", "txtCostodecapital", "txtVigencia", "txtFleteLocales", "txtGastosServirCliente", "txtIsr", "txtCetes", "txtManoObra" };

            Dictionary<string, string> vPerfilMapped = new Dictionary<string, string>()
            {
                {"Precios especiales", "Directivo"},
                {"Mercadotecnia", "Directivo"},
                {"Administracion", "Directivo"},
                {"Administrador", "Directivo"},
                {"Gerente de Desarrollo de Negocios", "Directivo"},
                {"Gerente de Sucursal", "Gerencial"},
                {"Representante de Ventas", "RIK"}
            };

            Dictionary<string, List<string>> vPerfiles = new Dictionary<string, List<string>>() {
               {"Directivo", new List<string>() { "txtCuentasPorCobrar", "txtInventario", "txtInversionactivosfijos", "txtFinanciamientoproveedores", "txtCostodecapital", "txtVigencia", "txtFleteLocales", "txtGastosServirCliente", "txtIsr", "txtCetes", "txtManoObra"}},
               {"Gerencial", new List<string>() {"txtCuentasPorCobrar", "txtInventario", "txtVigencia", "txtGastosServirCliente", "txtManoObra"}},
               {"RIK", new List<string>() {"txtCuentasPorCobrar", "txtVigencia", "txtManoObra"}}
            };

            CN_CatUsuario cnCatUsuario = new CN_CatUsuario();
            Usuario paramUsuario = new Usuario()
            {
                Id_Emp = vSesion.Id_Emp,
                Id_Cd = vSesion.Id_Cd,
                Id_U = vSesion.Id_U
            };

            cnCatUsuario.ConsultaUsuarios(ref paramUsuario, vSesion.Emp_Cnx);

            CN_CatTiposUsuario cnCatTipoUsuario = new CN_CatTiposUsuario();
            TipoUsuario paramTipoUsuario = new TipoUsuario()
            {
                Id_Emp = vSesion.Id_Emp
            };
            cnCatTipoUsuario.ConsultaTiposDeUsuario(paramTipoUsuario, vSesion.Emp_Cnx, ref listTipoUsuario);

            listControls = ObtieneControles(RadPane3).Where(ctrl => vCommonTypes.Contains(ctrl.GetType()) && vControles.Contains(ctrl.ID))
                                                     .ToList();

            listControls.ForEach(ctrl => ((WebControl)ctrl).Enabled = false);

            if (listTipoUsuario.Any(x => x.Id_TU == paramUsuario.Id_TU && vPerfilMapped.Keys.Contains(x.TU_Descripcion)))
            {
                listTipoUsuario.Where(tu => tu.Id_TU == paramUsuario.Id_TU)
                               .Join(vPerfilMapped, tu => new { Perfil = tu.TU_Descripcion.ToUpper() }, pm => new { Perfil = pm.Key.ToUpper() }, (tu, pm) => new { TU = tu, PM = pm })
                               .Join(vPerfiles, tupm => new { PerfilGen = tupm.PM.Value.ToUpper() }, p => new { PerfilGen = p.Key.ToUpper() }, (tupm, p) => new { TUPM = tupm, P = p })
                               .FirstOrDefault()
                               .P.Value
                               .Join(listControls, cd => new { CId = cd }, lc => new { CId = lc.ID }, (cd, lc) => new { Controles = lc })
                               .ToList()
                               .ForEach(ctrl => ((WebControl)ctrl.Controles).Enabled = true);
            }
        }

        #region Manejo de Controles
        public void DeshabilitaControles(WebControl pContenedor)
        {
            List<Type> vCommonTypes = new List<Type>() { typeof(RadNumericTextBox),
                                                     typeof(RadTextBox),
                                                     typeof(RadDatePicker),
                                                     typeof(RadComboBox),
                                                     typeof(CheckBox),
                                                     typeof(RadGrid),
                                                     typeof(ImageButton),
                                                     typeof(Button)
            };

            ObtieneControles(pContenedor).Where(ctrl => vCommonTypes.Contains(ctrl.GetType()))
                                         .ToList()
                                         .ForEach(ctrl => ((WebControl)ctrl).Enabled = false);
        }

        public IEnumerable<Control> ObtieneControles(Control pContenedor)
        {
            foreach (Control ctrl in pContenedor.Controls)
            {
                yield return ctrl;

                foreach (Control childCtrl in ObtieneControles(ctrl))
                {
                    yield return childCtrl;
                }
            }
        }
        #endregion

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            string mensajeError = string.Empty;
            try
            {
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":
                        rgDetalle.Rebind();
                        break;                    
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 70);
                        RadPageViewDetalles.Height = altura;
                        RadPane1.Height = altura;
                        RadPane1.Width = RadPageViewDGenerales.Width;
                        RadSplitter1.Height = altura;
                        RadPageViewDGenerales.Height = altura;
                        RadSplitter2.Height = altura;
                        RadPane2.Height = altura;
                        RadPane2.Width = RadPageViewDGenerales.Width;
                        RadSplitter3.Height = altura;
                        RadPane3.Height = altura;
                        RadPane3.Width = RadPageViewDGenerales.Width;
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            string mensajeError = string.Empty;
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "edit":
                        divResumen.InnerHtml = GeneraReporteVP();
                        break;
                    case "excel":
                        System.IO.StreamWriter sw = new System.IO.StreamWriter("E:/APLICACIONES_IIS/SIANCENTRAL/Vp.xls");
                        /*System.IO.StreamWriter sw = new System.IO.StreamWriter("Vp.xls");*/
                        sw.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />Folio : " + txtFolio.Text+ "<br>" + txtClienteNombre.Text + "<br>" + GeneraReporteVP());
                        sw.Close();
                        Response.Redirect("http://10.1.0.120/siancentral/Vp.xls");
                        /*Response.Redirect("Vp.xls");*/
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            string mensajeError = string.Empty;
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "save":
                        mensajeError = hiddenId.Value == string.Empty ? "CapValProyecto_insert_error" : "CapValProyecto_update_error";
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDetalle_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgDetalle.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDetalle_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgDetalle.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDetalle_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    //crear dataSource del combo de productos de cada registro del Grid
                    this.EstablecerDataSourceProductosLista(string.Empty);
                    //Llenar Grid
                    rgDetalle.DataSource = this.ListaProductosValProyecto;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDetalle_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    GridEditableItem editItem = (GridEditableItem)e.Item;

                    string cmbTipo = ((RadComboBox)editItem.FindControl("cmbTipo")).ClientID.ToString();
                    string lblVal_cmbTipo = ((Label)editItem.FindControl("lblVal_cmbTipo")).ClientID.ToString();
                    string txtId_Prd = ((RadNumericTextBox)editItem.FindControl("txtId_Prd")).ClientID.ToString();
                    string lbl_cmbProducto = ((Label)editItem.FindControl("lbl_cmbProducto")).ClientID.ToString();
                    string txtVap_Cantidad = ((RadNumericTextBox)editItem.FindControl("txtVap_Cantidad")).ClientID.ToString();
                    string lblVal_txtVap_Cantidad = ((Label)editItem.FindControl("lblVal_txtVap_Cantidad")).ClientID.ToString();
                    string txtVap_Precio = ((RadNumericTextBox)editItem.FindControl("txtVap_Precio")).ClientID.ToString();
                    string lblVal_txtVap_Precio = ((Label)editItem.FindControl("lblVal_txtVap_Precio")).ClientID.ToString();
                    string txtVap_CostoEditClientID = ((RadNumericTextBox)editItem.FindControl("txtVap_Costo")).ClientID.ToString();

                    ////Llenar combo de productos
                    //RadComboBox comboProductoItem = (RadComboBox)editItem.FindControl("cmbProducto");
                    ////comboProductoItem.DataSource = this.ListaProductos;
                    ////comboProductoItem.DataBind();
                    ////comboProductoItem.SelectedIndex = 0;
                    //CargarProductos(comboProductoItem);

                    RadComboBox cmbTipoCombo = (RadComboBox)editItem.FindControl("cmbTipo");

                    string jsControles = string.Concat(
                        "cmbTipoClientID='", cmbTipo, "';"
                        , "lblVal_cmbTipoClientID='", lblVal_cmbTipo, "';"
                        , "txtId_PrdClientID='", txtId_Prd, "';"
                        , "lbl_cmbProductoClientID='", lbl_cmbProducto, "';"
                        , "txtVap_CantidadClientID='", txtVap_Cantidad, "';"
                        , "lblVal_txtVap_CantidadClientID='", lblVal_txtVap_Cantidad, "';"
                        , "txtVap_PrecioClientID='", txtVap_Precio, "';"
                        , "lblVal_txtVap_PrecioClientID='", lblVal_txtVap_Precio, "';"
                        , "txtVap_CostoEditClientID='", txtVap_CostoEditClientID, "';"
                        );

                    ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                    {
                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormEdit(\"insertar\");");

                        ((RadNumericTextBox)editItem.FindControl("txtId_Prd")).Enabled = true;
                        ((RadTextBox)editItem.FindControl("txtProductoNombre")).Enabled = true;
                        //comboProductoItem.Enabled = true;

                        insertbtn.Attributes.Add("onclick", jsControles);

                        cmbTipoCombo.SelectedIndex = 1;
                    }

                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {

                        int Id_Prd = Convert.ToInt32(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Prd"]);
                        foreach (ValuacionProyectoDetalle vpd in this.ListaProductosValProyecto)
                        {
                            if (vpd.Id_Prd == Id_Prd)
                            {
                                cmbTipoCombo.SelectedIndex = cmbTipoCombo.FindItemIndexByValue(vpd.Vap_Tipo.ToString());
                                cmbTipoCombo.Text = cmbTipoCombo.FindItemByValue(vpd.Vap_Tipo.ToString()).Text;

                                if (vpd.Vap_Tipo == 2)
                                {
                                    ((RadNumericTextBox)editItem.FindControl("txtVap_Precio")).Enabled = false;
                                }
                                ((RadNumericTextBox)editItem.FindControl("txtId_Prd")).Enabled = false;
                                ((RadTextBox)editItem.FindControl("txtProductoNombre")).Enabled = false;
                                ((RadComboBox)editItem.FindControl("cmbTipo")).Enabled = false;                              
                            }
                        }
                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormEdit(\"actualizar\");");
                        updatebtn.Attributes.Add("onclick", jsControles);
                    }
                    cmbTipoCombo.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDetalle_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ValuacionProyectoDetalle valuacionProyectoDetalle = new ValuacionProyectoDetalle();

                valuacionProyectoDetalle.Id_Emp = sesion.Id_Emp;
                valuacionProyectoDetalle.Id_Cd = sesion.Id_Cd_Ver;
                valuacionProyectoDetalle.Id_Vap = Convert.ToInt32(txtFolio.Text); //se debe volver asignar cuando se guarda la valuacion de proyecto, cuando actualiza queda igual
                valuacionProyectoDetalle.Id_VapDet = 0;

                valuacionProyectoDetalle.Vap_Tipo = Convert.ToInt32((insertedItem.FindControl("cmbTipo") as RadComboBox).SelectedValue);
                valuacionProyectoDetalle.Vap_TipoStr = (insertedItem.FindControl("cmbTipo") as RadComboBox).SelectedItem.Text;
                RadNumericTextBox comboProducto = (insertedItem.FindControl("txtId_Prd") as RadNumericTextBox);
                valuacionProyectoDetalle.Id_Prd = Convert.ToInt32(comboProducto.Value);
                valuacionProyectoDetalle.Producto = new Producto();
                valuacionProyectoDetalle.Producto.Id_Prd = Convert.ToInt32(comboProducto.Value);
                valuacionProyectoDetalle.Producto.Prd_Descripcion = (insertedItem.FindControl("txtProductoNombre") as RadTextBox).Text;
                valuacionProyectoDetalle.Producto.Prd_Presentacion = (insertedItem.FindControl("lblPrd_PresentacionEdit") as Label).Text;
                valuacionProyectoDetalle.Producto.Prd_UniNs = (insertedItem.FindControl("lblPrd_UniNsEdit") as Label).Text;

                valuacionProyectoDetalle.Vap_Cantidad = Convert.ToInt32((insertedItem.FindControl("txtVap_Cantidad") as RadNumericTextBox).Text);
                valuacionProyectoDetalle.Vap_Costo = (insertedItem.FindControl("txtVap_Costo") as RadNumericTextBox).Text == "" ? 0 :  Convert.ToDouble((insertedItem.FindControl("txtVap_Costo") as RadNumericTextBox).Text);
                valuacionProyectoDetalle.Vap_Precio = Convert.ToDouble((insertedItem.FindControl("txtVap_Precio") as RadNumericTextBox).Text);
                valuacionProyectoDetalle.Vap_PrecioEspecial = Convert.ToDouble((insertedItem.FindControl("lblVap_ListaEdit") as Label).Text);//lblVap_PrecioEspecialEdit") as Label).Text);

                if (valuacionProyectoDetalle.Vap_Costo > 0 && (insertedItem.FindControl("txtVap_Precio") as RadNumericTextBox).Enabled)
                    if (valuacionProyectoDetalle.Vap_Costo > valuacionProyectoDetalle.Vap_Precio)
                    {
                        Alerta("El precio de venta no puede ser menor que el precio AAA");
                        return;
                    }
                //agregar producto de nota de cargo a la lista
                List<ValuacionProyectoDetalle> lista = this.ListaProductosValProyecto;

                //buscar producto de factura en la lista para ver si ya existe
                for (int i = 0; i < lista.Count; i++)
                {
                    ValuacionProyectoDetalle valProyectoDet = lista[i];
                    if (valProyectoDet.Id_Prd == valuacionProyectoDetalle.Id_Prd)//si el producto es el mismo
                    {
                        e.Canceled = true;
                        AlertaFocus("Producto ya capturado", comboProducto.ClientID);
                        return;
                    }
                }
                lista.Add(valuacionProyectoDetalle);
                this.ListaProductosValProyecto = lista;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDetalle_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ValuacionProyectoDetalle valuacionProyectoDetalle = new ValuacionProyectoDetalle();

                valuacionProyectoDetalle.Id_Emp = sesion.Id_Emp;
                valuacionProyectoDetalle.Id_Cd = sesion.Id_Cd_Ver;
                valuacionProyectoDetalle.Id_Vap = Convert.ToInt32(txtFolio.Text); //se debe volver asignar cuando se guarda la valuacion de proyecto, cuando actualiza queda igual
                valuacionProyectoDetalle.Id_VapDet = 0;

                valuacionProyectoDetalle.Vap_Tipo = Convert.ToInt32((insertedItem.FindControl("cmbTipo") as RadComboBox).SelectedValue);
                valuacionProyectoDetalle.Vap_TipoStr = (insertedItem.FindControl("cmbTipo") as RadComboBox).SelectedItem.Text;
                RadNumericTextBox comboProducto = (insertedItem.FindControl("txtId_Prd") as RadNumericTextBox);
                valuacionProyectoDetalle.Id_Prd = Convert.ToInt32(comboProducto.Value);
                valuacionProyectoDetalle.Producto = new Producto();
                valuacionProyectoDetalle.Producto.Id_Prd = Convert.ToInt32(comboProducto.Value);
                valuacionProyectoDetalle.Producto.Prd_Descripcion = (insertedItem.FindControl("txtProductoNombre") as RadTextBox).Text;
                valuacionProyectoDetalle.Producto.Prd_Presentacion = (insertedItem.FindControl("lblPrd_PresentacionEdit") as Label).Text;
                valuacionProyectoDetalle.Producto.Prd_UniNs = (insertedItem.FindControl("lblPrd_UniNsEdit") as Label).Text;
                valuacionProyectoDetalle.Vap_Cantidad = Convert.ToInt32((insertedItem.FindControl("txtVap_Cantidad") as RadNumericTextBox).Text);
                valuacionProyectoDetalle.Vap_Costo = (insertedItem.FindControl("txtVap_Costo") as RadNumericTextBox).Text == "" ? 0 : Convert.ToDouble((insertedItem.FindControl("txtVap_Costo") as RadNumericTextBox).Text);
                valuacionProyectoDetalle.Vap_Precio = Convert.ToDouble((insertedItem.FindControl("txtVap_Precio") as RadNumericTextBox).Text);
                valuacionProyectoDetalle.Vap_PrecioEspecial = Convert.ToDouble((insertedItem.FindControl("lblVap_ListaEdit") as Label).Text);//lblVap_PrecioEspecialEdit") as Label).Text);
                
                if (valuacionProyectoDetalle.Vap_Costo > 0)
                    if (valuacionProyectoDetalle.Vap_Costo > valuacionProyectoDetalle.Vap_Precio)
                    {
                        return;
                    }
                List<ValuacionProyectoDetalle> lista = this.ListaProductosValProyecto;
                //buscar producto de factura en la lista
                for (int i = 0; i < lista.Count; i++)
                {
                    ValuacionProyectoDetalle valProyectoDet = lista[i];
                    if (valProyectoDet.Id_Prd == valuacionProyectoDetalle.Id_Prd)
                    {
                        lista[i] = valuacionProyectoDetalle;
                        break;
                    }
                }
                this.ListaProductosValProyecto = lista;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDetalle_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridDataItem item = (GridDataItem)e.Item;
                int id_Prd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]);
                //eliminar producto de nota de cargo a la lista
                this.ListaProductosValProyecto_EliminarProducto(id_Prd);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                RadComboBox rcb = (sender as RadComboBox);
                RadNumericTextBox rtb = rcb.Parent.Parent.FindControl("txtVap_Precio") as RadNumericTextBox;
                (rcb.Parent.Parent.FindControl("lblPrd_PresentacionEdit") as Label).Text = "";
                (rcb.Parent.Parent.FindControl("lblPrd_UniNsEdit") as Label).Text = "";
                (rcb.Parent.Parent.FindControl("txtVap_Costo") as RadNumericTextBox).Text = "";
               // (rcb.Parent.Parent.FindControl("lblVap_CostoEdit") as Label).Text = "";
                (rcb.Parent.Parent.FindControl("lblVap_ListaEdit") as Label).Text = "";// as Label).Text = "";
                (rcb.Parent.Parent.FindControl("txtVap_Cantidad") as RadNumericTextBox).Text = "";
                (rcb.Parent.Parent.FindControl("hdSisProp") as HiddenField).Value = "";
                (rcb.Parent.Parent.FindControl("txtProductoNombre") as RadTextBox).Text = "";
                (rcb.Parent.Parent.FindControl("txtVap_Precio") as RadNumericTextBox).Text = "";
                (rcb.Parent.Parent.FindControl("txtId_Prd") as RadNumericTextBox).Text = "";
                if (rcb.SelectedValue == "2")
                {
                    rtb.Value = 0;
                    rtb.Enabled = false;
                }
                else
                {
                    rtb.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbProducto_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                RadComboBox combo = (RadComboBox)sender;
                //obtiene la tabla contenedora de los controles de edición de registro del Grid
                Telerik.Web.UI.GridTableCell tabla = (Telerik.Web.UI.GridTableCell)combo.Parent;

                Label lblPrd_PresentacionEdit = (Label)tabla.FindControl("lblPrd_PresentacionEdit");
                Label lblPrd_UniNsEdit = (Label)tabla.FindControl("lblPrd_UniNsEdit");
                Label lblVap_CostoEdit = (Label)tabla.FindControl("lblVap_CostoEdit"); 
                RadNumericTextBox txt_Cantidad = (RadNumericTextBox)tabla.FindControl("txtVap_Cantidad");
                Label txtVap_PrecioEspecialEdit = (Label)tabla.FindControl("lblVap_ListaEdit");
                RadComboBoxItem item = ((RadComboBox)sender).FindItemByValue(e.Value);
                int id_Cd_Prod = sesion.Id_Cd_Ver;
                Producto producto = null;
                if (e.Value != string.Empty && e.Value != "-1")
                {
                    CN_CatProducto clsProducto = new CN_CatProducto();
                    clsProducto.ConsultaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, id_Cd_Prod, sesion.Id_Cd_Ver, Convert.ToInt32(e.Value));
                }

                lblPrd_PresentacionEdit.Text = producto == null ? string.Empty : producto.Prd_Presentacion;
                lblPrd_UniNsEdit.Text = producto == null ? string.Empty : producto.Prd_UniNs;

                //obtener precio de producto
                double precioAAA = 0;               
                double precioLista = 0;
                new CN_ProductoPrecios().ConsultarPrecios(ref precioAAA, ref precioLista, sesion, 0, Convert.ToInt32(e.Value));

                lblVap_CostoEdit.Text = Math.Round(this.PartidasCalcularPrecioAAA(sesion.Id_Emp, sesion.Id_Cd_Ver, 0, producto.Id_Prd, sesion.Emp_Cnx), 2).ToString();                
                txtVap_PrecioEspecialEdit.Text = producto.Prd_Precio;
                //Limpiar controles de compras locales
                //combo.Items[0].FindControl("liComprasLocales").Controls.Clear();
                txt_Cantidad.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbProductoDet_TextChanged(object sender, EventArgs e)
        {
            try
            {
                RadNumericTextBox txtProducto = (RadNumericTextBox)sender;
                Telerik.Web.UI.GridTableCell tabla = (Telerik.Web.UI.GridTableCell)txtProducto.Parent;
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                Label lblPrd_PresentacionEdit = (Label)tabla.FindControl("lblPrd_PresentacionEdit");
                Label lblPrd_UniNsEdit = (Label)tabla.FindControl("lblPrd_UniNsEdit");
                RadNumericTextBox txtVap_CostoEdit = (RadNumericTextBox)tabla.FindControl("txtVap_Costo");
                Label txtVap_PrecioEspecialEdit = (Label)tabla.FindControl("lblVap_ListaEdit");
                RadNumericTextBox txt_Cantidad = (RadNumericTextBox)tabla.FindControl("txtVap_Cantidad");

                List<ValuacionProyectoDetalle> lista = this.ListaProductosValProyecto;
                //buscar producto de factura en la lista para ver si ya existe
                for (int i = 0; i < lista.Count; i++)
                {
                    ValuacionProyectoDetalle valProyectoDet = lista[i];
                    if (valProyectoDet.Id_Prd.ToString() == txtProducto.Text)//si el producto es el mismo
                    {
                        AlertaFocus("Producto ya capturado", txtProducto.ClientID);
                        txtProducto.Text = "";
                        return;
                    }
                }
                Producto prd = new Producto();
                prd.Prd_AparatoSisProp = (txtProducto.Parent.Parent.FindControl("cmbTipo") as RadComboBox).SelectedValue == "2" ? true : false;
                try
                {
                    new CN_CatProducto().ConsultaProducto(ref prd, session.Emp_Cnx, session.Id_Emp, session.Id_Cd, Convert.ToInt32(txtProducto.Value.HasValue ? (sender as RadNumericTextBox).Value : -1));
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, txtProducto.ClientID);
                    txtProducto.Text = "";
                    (tabla.FindControl("txtProductoNombre") as RadTextBox).Text = "";
                    lblPrd_PresentacionEdit.Text = "";
                    lblPrd_UniNsEdit.Text = "";
                    txtVap_CostoEdit.Text = "";
                    txtVap_PrecioEspecialEdit.Text = "";
                    return;
                }
                (tabla.FindControl("hdSisProp") as HiddenField).Value = prd.Prd_AparatoSisProp.ToString();
                (tabla.FindControl("txtProductoNombre") as RadTextBox).Text = prd.Prd_Descripcion;

                int id_Cd_Prod = session.Id_Cd_Ver; 
                lblPrd_PresentacionEdit.Text = prd == null ? string.Empty : prd.Prd_Presentacion;
                lblPrd_UniNsEdit.Text = prd == null ? string.Empty : prd.Prd_UniNs;

                //obtener precio de producto
                float precioAAA = 0;
                float precioLista = 0;
                int Id_Pre = 0;

                new CN_ProductoPrecios().ConsultaListaProductoPrecioAAA(ref precioAAA, session, Convert.ToInt32(txtProducto.Value), ref Id_Pre);
                new CN_ProductoPrecios().ConsultaListaProductoPrecioPUBLICO(ref precioLista, session, Convert.ToInt32(txtProducto.Value));
                
                //new CN_ProductoPrecios().ConsultarPrecios(ref precioAAA, ref precioLista, session, Convert.ToInt32(txtCliente.Text), Convert.ToInt32(txtProducto.Value));
                txtVap_CostoEdit.Text = precioAAA.ToString("N");
                txtVap_PrecioEspecialEdit.Text = precioLista.ToString("N");
                txt_Cantidad.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        
        protected void cmbProducto_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            try
            {             
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void imgAceptar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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
        #endregion
        #region Funciones
        private void Inicializar(int Id_Emp, int Id_Cd, int Id_Vap)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            TipoMoneda tipoMoneda = new TipoMoneda();
            List<TipoMoneda> lista = new List<TipoMoneda>();
            new CN_CatTipoMoneda().ConsultaTipoMoneda(tipoMoneda, Sesion.Id_Emp
                , Sesion.Emp_Cnx, ref lista);
            this.ListaTipoMoneda = lista;

            //nueva variable para controlar tabla dinamica de productos de nota de cargo
            Session["ListaProductosValProyecto"] = new List<ValuacionProyectoDetalle>();

            //establece valores de controles de inicio
            if (Id_Emp > 0 && Id_Cd > 0 && Id_Vap > 0)
            {
                this.LLenarFormValProyecto(Id_Emp, Id_Cd, Id_Vap); //EDICION 
                this.hiddenId.Value = Id_Vap.ToString();
                LlenarParametros(Id_Vap);
            }
            else //Nueva
            {
                LlenarParametros(null);
                Id_Vap = !string.IsNullOrEmpty(Page.Request.QueryString["Id"]) ? Convert.ToInt32(Page.Request.QueryString["Id"]) : 0;
                _Editable = "1";
                if (Id_Vap == 0)
                {
                    this.hiddenId.Value = string.Empty;
                    this.txtFecha.SelectedDate = DateTime.Now;
                    this.txtFolio.Text = this.Valor;
                }
                else
                {
                    this.LLenarFormValProyecto(Sesion.Id_Emp, Sesion.Id_Cd, Id_Vap);
                    this.hiddenId.Value = Id_Vap.ToString();
                    LlenarParametros(Id_Vap);
                }
            }
            this.rgDetalle.Rebind();
            this.txtFecha.Focus();
        }
        protected string ObtenerDescripcionProducto(object oc)
        {
            if (oc is ValuacionProyectoDetalle) { return ((ValuacionProyectoDetalle)oc).Producto.Prd_Descripcion; } else { return string.Empty; }
        }
        protected string ObtenerPresentacionProducto(object oc)
        {
            if (oc is ValuacionProyectoDetalle) { return ((ValuacionProyectoDetalle)oc).Producto.Prd_Presentacion; } else { return string.Empty; }
        }
        protected string ObtenerUnidadesProducto(object oc)
        {
            if (oc is ValuacionProyectoDetalle) { return ((ValuacionProyectoDetalle)oc).Producto.Prd_UniNs; } else { return string.Empty; }
        }
        protected int ObtenerInvFinal(object oc)
        {
            if (oc is ValuacionProyectoDetalle) { return ((ValuacionProyectoDetalle)oc).Producto.Prd_InvFinal; } else { return 0; }
        }
        protected void ListaProductosValProyecto_EliminarProducto(int id_Prd)
        {
            List<ValuacionProyectoDetalle> lista = this.ListaProductosValProyecto;

            //buscar producto de factura en la lista
            for (int i = 0; i < lista.Count; i++)
            {
                ValuacionProyectoDetalle valProyectoDet = lista[i];
                if (valProyectoDet.Id_Prd == id_Prd)
                {
                    lista.RemoveAt(i);
                    break;
                }
            }
            this.ListaProductosValProyecto = lista;
        }
        private double PartidasCalcularPrecioLista(int Id_Emp, int Id_Cd, int Id_Cte, int Id_Prd, string Conexion)
        {
            double precioProductoAceptado = 0;
            //obtener precio especial del producto 
            //para el cliente actual de la factura
            //desde la CAPTURA de SOLICITUDES DE PRECIOS ESPECIALES
            VentanaPrecioEspecialPro precioEspecialPro = null;
            new CN_PrecioEspecial().PrecioEspecialProductoCliente_Consulta(ref precioEspecialPro, Conexion
                , Id_Emp, Id_Cd, Id_Cte, Id_Prd /* , Convert.ToInt32(cmbMoneda.SelectedValue) */);
            if (precioEspecialPro != null && precioEspecialPro.Ape_PreEsp > 0)
            {
                /*
                    * NOTA: si el precio está en dólares u otro tipo de moneda, 
                    * se hace la conversión al tipo de moneda de la Valuacion de proyectos
                    */
                if (precioEspecialPro.Id_Mon != 1) // MONEDA = PESO (1) siempre en captura de valuacion proyectos
                { //Consultar tipo de cambio
                    double tipoCambioFactura = 1; // MONEDA = PESO (1) siempre en captura de valuacion proyectos
                    double tipoCambioPrecioEspecial = 0;
                    foreach (TipoMoneda tm in ListaTipoMoneda)
                    {
                        if (tm.Id_Mon == precioEspecialPro.Id_Mon)
                            tipoCambioPrecioEspecial = tm.Mon_TipCambio;
                    }
                    precioProductoAceptado = (precioEspecialPro.Ape_PreEsp * tipoCambioPrecioEspecial) / tipoCambioFactura;
                }
                else
                    precioProductoAceptado = precioEspecialPro.Ape_PreEsp;
            }
            else
            {
                //Si no hay un precio especial en SOLICITUD DE PRECIOS ESPECIALES
                //va por el precio del catalogo CLIENTE-PRODUCTO, si no hay toma el precio AAA normal del producto
                //obtener precio AAA
                float precioAAA = 0;
                new CN_ProductoPrecios().ConsultaListaProductoPrecioAAA(ref precioAAA, Id_Emp, Id_Cd, Id_Prd, Conexion);

                //obtener precio especial de producto
                //desde el catálogo CAT_CLIENTEPRODUCTO
                float precioPublicoCAT_CLIENTEPRODUCTO = 0;
                ClienteProd clienteProd = new ClienteProd();
                clienteProd.Id_Emp = Id_Emp;
                clienteProd.Id_Cd = Id_Cd;
                clienteProd.Id_Cte = Id_Cte;
                clienteProd.Id_Prd = Id_Prd;
                clienteProd.Id_Vap = Id_Vap;
                new CN_CatClienteProd().ClienteProductoPrecioPublico_Consultar(ref clienteProd, Conexion, ref precioPublicoCAT_CLIENTEPRODUCTO);

                precioProductoAceptado = precioPublicoCAT_CLIENTEPRODUCTO > 0 ? precioPublicoCAT_CLIENTEPRODUCTO : precioAAA;
            }
            return precioProductoAceptado;
        }
        private double PartidasCalcularPrecioAAA(int Id_Emp, int Id_Cd, int Id_Cte, int Id_Prd, string Conexion)
        {                                    
            //Si no hay un precio especial en SOLICITUD DE PRECIOS ESPECIALES
            //va por el precio del catalogo CLIENTE-PRODUCTO, si no hay toma el precio AAA normal del producto
            //obtener precio AAA
            float precioAAA = 0;
            new CN_ProductoPrecios().ConsultaListaProductoPrecioAAA(ref precioAAA, Id_Emp, Id_Cd, Id_Prd, Conexion);                        

            return precioAAA;
        }
        private void LlenarParametros(int? Id_Vap)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Id_Vap == null)
                {
                    CN_CapValuacionProyecto cn_valuacion = new CN_CapValuacionProyecto();
                    ValuacionParametros vp = new ValuacionParametros();
                    vp.Id_Emp = sesion.Id_Emp;
                    vp.Id_Cd = sesion.Id_Cd_Ver;

                    int verificador = 0;
                    cn_valuacion.consultarCondicionesCentro(ref vp, sesion.Emp_Cnx, ref verificador);

                    txtCuentasPorCobrar.DbValue = 0;
                    lblCuentasPorCobrar.DbValue = 0;

                    txtInventario.DbValue = vp.Vap_Inventario_Key;
                    lblInventario.DbValue = vp.Vap_Inventario_Key;

                    txtGastosServirCliente.DbValue = vp.Cd_ComisionRik;
                    lblGastosServirCliente.DbValue = vp.Cd_ComisionRik;

                    txtVigencia.DbValue = vp.Vap_Vigencia;
                    lblVigencia.DbValue = vp.Vap_Vigencia;

                    txtFleteLocales.DbValue = vp.Vap_Gasto_Flete_Locales;
                    lblFleteLocales.DbValue = vp.Vap_Gasto_Flete_Locales;

                    txtIsr.DbValue = vp.Vap_ISR;
                    lblIsr.DbValue = vp.Vap_ISR;

                    txtCetes.DbValue = vp.Vap_Cetes;
                    lblCetes.DbValue = vp.Vap_Cetes;

                    txtFinanciamientoproveedores.DbValue = vp.Cd_DiasFinanciaProv;
                    lblFinanciamientoproveedores.DbValue = vp.Cd_DiasFinanciaProv;

                    txtInversionactivosfijos.DbValue = vp.Cd_FactorConvActFijo;
                    lblInversionactivosfijos.DbValue = vp.Cd_FactorConvActFijo;

                    txtCostodecapital.DbValue = vp.Cd_TasaIncCostoCapital;
                    lblCostodecapital.DbValue = vp.Cd_TasaIncCostoCapital;

                    txtManoObra.DbValue = 0;
                    lblManoObra.DbValue = 0;
                }
                else
                {

                        txtCuentasPorCobrar.Text = Convert.ToString(0);
                        lblCuentasPorCobrar.Text = Convert.ToString(0);


                    CN_CapValuacionProyecto cn_valuacion = new CN_CapValuacionProyecto();
                    ValuacionParametros vp = new ValuacionParametros();
                    vp.Id_Emp = sesion.Id_Emp;
                    vp.Id_Cd = sesion.Id_Cd_Ver;

                    int verificador = 0;
                    cn_valuacion.consultarCondicionesCentro(ref vp, sesion.Emp_Cnx, ref  verificador);

                    ValuacionParametrosActual vpactuales = new ValuacionParametrosActual();
                    vpactuales.Id_Emp = sesion.Id_Emp;
                    vpactuales.Id_Cd = sesion.Id_Cd_Ver;
                    vpactuales.Id_Vap = Convert.ToInt32(Id_Vap);
                    cn_valuacion.consultarCondicionesActuales(ref vpactuales, sesion.Emp_Cnx, ref  verificador);


                    if (verificador != 0)
                        txtCuentasPorCobrar.Text = Convert.ToString(vpactuales.txtCuentasPorCobrar);



                    if (verificador != 0)
                    {
                        txtInventario.DbValue = vpactuales.txtInventario;
                    }
                    else
                    {
                        txtInventario.DbValue = vp.Vap_Inventario_Key;
                    }

                    lblInventario.DbValue = vp.Vap_Inventario_Key;

                    if (verificador != 0)
                    {
                        txtGastosServirCliente.DbValue = vpactuales.txtGastosServirCliente;
                    }
                    else
                    {
                        txtGastosServirCliente.DbValue = vp.Cd_ComisionRik;
                    }
                    lblGastosServirCliente.DbValue = vp.Cd_ComisionRik;



                    if (verificador != 0)
                    {
                        txtVigencia.DbValue = vpactuales.txtVigencia;
                    }
                    else
                    {
                        txtVigencia.DbValue = vp.Vap_Vigencia;
                    }
                    lblVigencia.DbValue = vp.Vap_Vigencia;

                    if (verificador != 0)
                    {
                        txtFleteLocales.DbValue = vpactuales.txtFleteLocales;
                    }
                    else
                    {
                        txtFleteLocales.DbValue = vp.Vap_Gasto_Flete_Locales;
                    }
                    lblFleteLocales.DbValue = vp.Vap_Gasto_Flete_Locales;

                    if (verificador != 0)
                    {
                        txtIsr.DbValue = vpactuales.txtIsr;
                    }
                    else
                    {
                        txtIsr.DbValue = vp.Vap_ISR;
                    }
                    lblIsr.DbValue = vp.Vap_ISR;

                    if (verificador != 0)
                    {
                        txtCetes.DbValue = vpactuales.txtCetes;
                    }
                    else
                    {
                        txtCetes.DbValue = vp.Vap_Cetes;
                    }
                    lblCetes.DbValue = vp.Vap_Cetes;

                    if (verificador != 0)
                    {
                        txtFinanciamientoproveedores.DbValue = vpactuales.txtFinanciamientoproveedores;
                    }
                    else
                    {
                        txtFinanciamientoproveedores.DbValue = vp.Cd_DiasFinanciaProv;
                    }
                    lblFinanciamientoproveedores.DbValue = vp.Cd_DiasFinanciaProv;

                    if (verificador != 0)
                    {
                        txtInversionactivosfijos.DbValue = vpactuales.txtInversionactivosfijos;
                    }
                    else
                    {
                        txtInversionactivosfijos.DbValue = vp.Cd_FactorConvActFijo;
                    }
                    lblInversionactivosfijos.DbValue = vp.Cd_FactorConvActFijo;

                    if (verificador != 0)
                    {
                        txtCostodecapital.DbValue = vpactuales.txtCostodecapital;
                    }
                    else
                    {
                        txtCostodecapital.DbValue = vp.Cd_TasaIncCostoCapital;
                    }
                    lblCostodecapital.DbValue = vp.Cd_TasaIncCostoCapital;

                    if (verificador != 0)
                    {
                        txtManoObra.DbValue = vpactuales.txtManoObra;
                    }
                    else
                    {
                        txtManoObra.DbValue = 0;
                    }
                    lblManoObra.DbValue = 0;

                }
            }
            catch    (Exception ex)
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
                new CN_CatProducto().ConsultaListaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, filtro, ref listaProducto, 1);

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
        private void LLenarFormValProyecto(int Id_Emp, int Id_Cd, int Id_Vap)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            ValuacionProyecto valuacionProyecto = new ValuacionProyecto();
            valuacionProyecto.Id_Emp = Id_Emp;
            valuacionProyecto.Id_Cd = Id_Cd;
            valuacionProyecto.Id_Vap = Id_Vap;

            new CN_CapValuacionProyecto().ConsultarValuacionProyecto(ref valuacionProyecto, sesion.Emp_Cnx);
            txtFolio.Text = valuacionProyecto.Id_Vap.ToString();
            txtFecha.SelectedDate = valuacionProyecto.Vap_Fecha;            
            txtClienteNombre.Text = valuacionProyecto.Vap_CteNombre;
            txtNota.Text = valuacionProyecto.Vap_Nota;
            //calcular precio de lista de cada producto de la partida
            //foreach (ValuacionProyectoDetalle vd in valuacionProyecto.ListaProductosValuacionProyecto)
            //{
            //    vd.Vap_PrecioEspecial = this.PartidasCalcularPrecioLista(vd.Id_Emp, vd.Id_Cd, valuacionProyecto.Id_Cte, vd.Id_Prd, sesion.Emp_Cnx);                     
            //}
            rgDetalle.MasterTableView.Columns[11].Display = true;
            this.ListaProductosValProyecto = valuacionProyecto.ListaProductosValuacionProyecto;

            double ancho = 0;
            foreach (GridColumn gc in rgDetalle.Columns)
            {
                if (gc.Display)
                {
                    ancho = ancho + gc.HeaderStyle.Width.Value;
                }
            }
            rgDetalle.Width = Unit.Pixel(Convert.ToInt32(ancho));
            rgDetalle.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));

            rgDetalle.Rebind();
        }
        private ValuacionProyecto LlenarObjetoValProyecto()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            ValuacionProyecto valuacionProyecto = new ValuacionProyecto();
            valuacionProyecto.Id_Emp = sesion.Id_Emp;
            valuacionProyecto.Id_Cd = sesion.Id_Cd_Ver;
            valuacionProyecto.Id_Vap = Convert.ToInt32(txtFolio.Text);

            valuacionProyecto.Id_U = sesion.Id_U;
            valuacionProyecto.Id_Cte = 0;
            Funciones funcion = new Funciones();
            valuacionProyecto.Vap_Fecha = Convert.ToDateTime(txtFecha.SelectedDate.Value.ToShortDateString() + funcion.GetLocalDateTime(sesion.Minutos).ToString(" HH:mm:ss"));
            valuacionProyecto.Vap_Nota = txtNota.Text;
            valuacionProyecto.Vap_Estatus = "C";
            valuacionProyecto.Vap_CteNombre = txtClienteNombre.Text;
            valuacionProyecto.ListaProductosValuacionProyecto = this.ListaProductosValProyecto;
            return valuacionProyecto;
        }
        private string GeneraReporteVP()
        {

            Double VentaNeta = 0;
            Double VentaNetaPapel = 0;
            Double VentaNetaOtros = 0;
            Double CostoMaterial = 0;
            Double CostoMaterialNOPapel = 0;
            Double AmortizacionTotal = 0;
            Double Prd_PesConTecnico = 0;
            Double UtilidadBruta = 0;

            String EsPapel ="";
            Int32 Prd_Mes = 0;
            Double Prd_PesosAAA = 0;
            Double Prd_PesosConTecnico = 0;
            Int32 MaxMeses = 0;

            double TotalInversionComodatos = 0;

            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            List<ValuacionProyectoDetalle> lista = this.ListaProductosValProyecto;


            for (int i = 0; i < lista.Count; i++)
            {

                EsPapel ="";
                Prd_PesosConTecnico = 0;
                Prd_Mes = 0;
                Prd_PesosAAA = 0;

                ValuacionProyectoDetalle valProyectoDet = lista[i];

                CN_CatProducto clsProducto = new CN_CatProducto();
                clsProducto.CatProducto_Informacion_VP(valProyectoDet.Id_Prd, sesion.Emp_Cnx, ref EsPapel, ref Prd_PesosConTecnico, ref Prd_Mes, ref Prd_PesosAAA);


                if (valProyectoDet.Vap_Tipo == 1)   //Si es Consumible
                {

                    VentaNeta = VentaNeta + (valProyectoDet.Vap_Cantidad * valProyectoDet.Vap_Precio);
                    CostoMaterial = CostoMaterial + (valProyectoDet.Vap_Cantidad * valProyectoDet.Vap_Costo);

                    if (EsPapel == "S")   //Si Es Papel
                    {
                        VentaNetaPapel = VentaNetaPapel + (valProyectoDet.Vap_Cantidad * valProyectoDet.Vap_Precio);
                    }
                    else  //Si es Diferente de Papel
                    {
                        CostoMaterialNOPapel = CostoMaterialNOPapel + (valProyectoDet.Vap_Cantidad * valProyectoDet.Vap_Costo);
                        VentaNetaOtros = VentaNetaOtros + (valProyectoDet.Vap_Cantidad * valProyectoDet.Vap_Precio);
                    }
                }
                else // Si es Sistemas Propietarios
                {
                    if (Prd_Mes == 1)
                    {
                        Prd_Mes = 24;
                    }

                    if (MaxMeses < Prd_Mes)
                    {
                        MaxMeses = Prd_Mes;
                    }
                    Prd_PesConTecnico = Prd_PesConTecnico + (valProyectoDet.Vap_Cantidad * Prd_PesosConTecnico);
                    AmortizacionTotal = AmortizacionTotal + ((valProyectoDet.Vap_Cantidad * Prd_PesosAAA)/Prd_Mes);
                    TotalInversionComodatos = TotalInversionComodatos + (valProyectoDet.Vap_Cantidad * Prd_PesosAAA);
                }
            }

            UtilidadBruta = VentaNeta - CostoMaterial;

            CentroDistribucion cd = new CentroDistribucion();
            new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);

            double FactorFijos = 0;
            double FactorUCS = 0;

            if (VentaNeta < 5000) FactorFijos = 17.5;
            if (VentaNeta >= 5000 && VentaNeta < 10000) FactorFijos = 16.84;
            if (VentaNeta >= 10000 && VentaNeta < 15000) FactorFijos = 16.18;
            if (VentaNeta >= 15000 && VentaNeta < 20000) FactorFijos = 15.53;
            if (VentaNeta >= 20000 && VentaNeta < 25000) FactorFijos = 14.87;
            if (VentaNeta >= 25000 && VentaNeta < 30000) FactorFijos = 14.21;
            if (VentaNeta >= 30000 && VentaNeta < 35000) FactorFijos = 13.55;
            if (VentaNeta >= 35000 && VentaNeta < 40000) FactorFijos = 12.89;
            if (VentaNeta >= 40000 && VentaNeta < 45000) FactorFijos = 12.24;
            if (VentaNeta >= 45000 && VentaNeta < 50000) FactorFijos = 11.58;
            if (VentaNeta >= 50000 && VentaNeta < 55000) FactorFijos = 10.92;
            if (VentaNeta >= 55000 && VentaNeta < 60000) FactorFijos = 10.26;
            if (VentaNeta >= 60000 && VentaNeta < 65000) FactorFijos = 9.61;
            if (VentaNeta >= 65000 && VentaNeta < 70000) FactorFijos = 8.95;
            if (VentaNeta >= 70000 && VentaNeta < 75000) FactorFijos = 8.29;
            if (VentaNeta >= 75000 && VentaNeta < 80000) FactorFijos = 7.63;
            if (VentaNeta >= 80000 && VentaNeta < 85000) FactorFijos = 6.97;
            if (VentaNeta >= 85000 && VentaNeta < 90000) FactorFijos = 6.32;
            if (VentaNeta >= 90000 && VentaNeta < 100000) FactorFijos = 5.66;
            if (VentaNeta >= 100000) FactorFijos = 5.0;

            if (VentaNeta < 5000) FactorUCS = 3.5;
            if (VentaNeta >= 5000 && VentaNeta < 10000) FactorUCS = 3.0;
            if (VentaNeta >= 10000 && VentaNeta < 25000) FactorUCS = 2.5;
            if (VentaNeta >= 25000 && VentaNeta < 50000) FactorUCS = 2;
            if (VentaNeta >= 50000 && VentaNeta < 100000) FactorUCS = 1.5;
            if (VentaNeta >= 100000) FactorUCS = 1;

            double Cte_CarMP = Convert.ToDouble(txtManoObra.Text);
            double Cte_GasVarT = 0;
            double Cte_FletePaga = 0; //Convert.ToDouble(dtReporteTotales.Rows[0]["Cte_FletePaga"]);


            double DiasRotacion = 0;


            //CN_CatCliente clsCliente = new CN_CatCliente();
            //clsCliente.CatClienteCondPago(sesion.Id_Emp,sesion.Id_Cd_Ver,Convert.ToInt32(txtCliente.Text),ref DiasRotacion, sesion.Emp_Cnx);

            Double.TryParse(txtCuentasPorCobrar.Text, out DiasRotacion);           

            //calcular financiamiento de proveedores
            //double financiamientoProveedores = (((((CostoMaterial / 30) * cd.Cd_Dias.Value) / cd.Cd_Dias.Value) * cd.Cd_DiasFinanciaProv) * (1 + (Convert.ToSingle(cd.Cd_Iva) / 100)));
            double financiamientoProveedores = (((((CostoMaterial / 30) * Convert.ToDouble(txtInventario.Text)) / Convert.ToDouble(txtInventario.Text)) * Convert.ToDouble(txtFinanciamientoproveedores.Text)) * (1 + (Convert.ToSingle(cd.Cd_Iva) / 100)));
            if (double.IsNaN(financiamientoProveedores) || double.IsInfinity(financiamientoProveedores))
            {
                financiamientoProveedores = 0;
            }

            //calcular inversion total en activos
            //double inversionTotalActivos
            //    = (((VentaNeta / 30) * DiasRotacion) * (1 + (Convert.ToSingle(cd.Cd_Iva) / 100)))
            //    + ((CostoMaterial / 30) * cd.Cd_Dias.Value)
            //    + ((CostoMaterial / 30) * cd.Cd_DiasInv.Value)
            //    //+ (TotalInversionComodatos * Convert.ToSingle(cd.Cd_FactorInvComodato))
            //    + ((VentaNeta / 30) * cd.Cd_FactorConvActFijo.Value);





            double inversionTotalActivos
                = (((VentaNeta / 30) * DiasRotacion) * (1 + (Convert.ToSingle(cd.Cd_Iva) / 100)))
                + ((CostoMaterial / 30) * Convert.ToDouble(txtInventario.Text))
                //+ (TotalInversionComodatos * Convert.ToSingle(cd.Cd_FactorInvComodato))
                + ((VentaNeta / 30) * Convert.ToDouble(txtInversionactivosfijos.Text));



            if (double.IsNaN(inversionTotalActivos) || double.IsInfinity(inversionTotalActivos))
            {
                inversionTotalActivos = 0;
            }

            //calcular utilidad bruta
            //UtilidadBruta =
            //    VentaNeta
            //    - CostoMaterial
            //    - Cte_CarMP
            //    - (/*CostoMaterial*/ CostoMaterialNOPapel * (Convert.ToSingle(cd.Cd_Flete) / 100)) //flete
            //    - AmortizacionTotal
            //    - Prd_PesConTecnico;


            //calcular utilidad bruta
            UtilidadBruta =
                VentaNeta
                - CostoMaterial
                - Cte_CarMP
                - (/*CostoMaterial*/ CostoMaterialNOPapel * (Convert.ToSingle(txtFleteLocales.DbValue) / 100)) //flete
                - AmortizacionTotal
                - Prd_PesConTecnico;



            if (double.IsNaN(UtilidadBruta) || double.IsInfinity(UtilidadBruta))
            {
                UtilidadBruta = 0;
            }
            //calcular utilidad marginal
            //double UtilidadMarginal =
            //    UtilidadBruta
            //    - (UtilidadBruta * (Convert.ToSingle(cd.Cd_ComisionRik) / 100))
            //    - Cte_GasVarT
            //    //- (VentaNeta * (Convert.ToSingle(cd.Cd_OtrosGastosVar) / 100))
            //    - Cte_FletePaga;


            double UtilidadMarginal =
                UtilidadBruta
                - (UtilidadBruta * (Convert.ToSingle(txtGastosServirCliente.DbValue) / 100))
                - Cte_GasVarT
                //- (VentaNeta * (Convert.ToSingle(cd.Cd_OtrosGastosVar) / 100))
                - Cte_FletePaga;

            if (double.IsNaN(UtilidadMarginal) || double.IsInfinity(UtilidadMarginal))
            {
                UtilidadMarginal = 0;
            }

            //calcular Uafir mensual
            double UafirMensual =
                UtilidadMarginal
                /*                        - (VentaNetaOtros * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosOtros) / 100))
                                          - (VentaNetaPapel * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosPapel) / 100))   
                                          - (VentaNeta * (Convert.ToSingle(cd.Cd_CargoUCS) / 100)); */
                - (VentaNeta * (Convert.ToSingle(FactorFijos) / 100))
                - (VentaNeta * (Convert.ToSingle(FactorUCS) / 100));
            if (double.IsNaN(UafirMensual) || double.IsInfinity(UafirMensual))
            {
                UafirMensual = 0;
            }
            //calcular Costo de capital
            //double CostoCapital = (Math.Round(inversionTotalActivos, 2) - financiamientoProveedores) * (cd.Cd_TasaCetes.Value + cd.Cd_TasaIncCostoCapital.Value) / 100;

            double CostoCapital = (Math.Round(inversionTotalActivos, 2) - financiamientoProveedores) * (Convert.ToDouble(txtCetes.Text) + Convert.ToDouble(txtCostodecapital.Text)) / 100;



            if (double.IsNaN(CostoCapital) || double.IsInfinity(CostoCapital))
            {
                CostoCapital = 0;
            }
            //calcular Uafir después de impuestos

            //double UafirDespuesImpuestos = (UafirMensual * 12) - ((UafirMensual * 12) * (Convert.ToSingle(cd.Cd_ISRyPTU) / 100));

            double UafirDespuesImpuestos = (UafirMensual * 12) - ((UafirMensual * 12) * (Convert.ToSingle(txtIsr.DbValue) / 100));


            if (double.IsNaN(UafirDespuesImpuestos) || double.IsInfinity(UafirDespuesImpuestos))
            {
                UafirDespuesImpuestos = 0;
            }
            //calcular porcentaje de utilidad remanente
            //double UtilidadRemanentePorc = (UafirDespuesImpuestos / (inversionTotalActivos - financiamientoProveedores) * 100) - (cd.Cd_TasaCetes.Value + cd.Cd_TasaIncCostoCapital.Value);

            double UtilidadRemanentePorc = (UafirDespuesImpuestos / (inversionTotalActivos - financiamientoProveedores) * 100) - (Convert.ToDouble(txtCetes.Text) + Convert.ToDouble(txtCostodecapital.Text));


            if (double.IsNaN(UtilidadRemanentePorc) || double.IsInfinity(UtilidadRemanentePorc))
            {
                UtilidadRemanentePorc = 0;
            }


            //double ctaPorCobrar = ((VentaNeta / 30) * DiasRotacion) * (1 + (Convert.ToSingle(cd.Cd_Iva) / 100));
            double ctaPorCobrar = ((VentaNeta / 30) * DiasRotacion) * (1 + (Convert.ToSingle(cd.Cd_Iva) / 100));

            if (double.IsNaN(ctaPorCobrar) || double.IsInfinity(ctaPorCobrar))
            {
                ctaPorCobrar = 0;
            }

            double txtUafirActivos = UafirDespuesImpuestos / (inversionTotalActivos - financiamientoProveedores) * 100;
            if (double.IsNaN(txtUafirActivos) || double.IsInfinity(txtUafirActivos))
            {
                txtUafirActivos = 0;
            }

            //double txtISRyPTUMon = (UafirMensual * 12) * (Convert.ToSingle(cd.Cd_ISRyPTU) / 100);
            double txtISRyPTUMon = (UafirMensual * 12) * (Convert.ToSingle(txtIsr.DbValue) / 100);

            if (double.IsNaN(txtISRyPTUMon) || double.IsInfinity(txtISRyPTUMon))
            {
                txtISRyPTUMon = 0;
            }

            double txtGastosVariablesPorc = (Cte_GasVarT / VentaNeta) * 100;

            if (double.IsNaN(txtGastosVariablesPorc) || double.IsInfinity(txtGastosVariablesPorc))
            {
                txtGastosVariablesPorc = 0;
            }
            double txtOtrosGastosVariablesPorc = 0; //((VentaNeta * (Convert.ToSingle(cd.Cd_OtrosGastosVar) / 100)) / VentaNeta) * 100;
            if (double.IsNaN(txtOtrosGastosVariablesPorc) || double.IsInfinity(txtOtrosGastosVariablesPorc))
            {
                txtOtrosGastosVariablesPorc = 0;
            }
            double txtFletesPagadosPorc = (Cte_FletePaga / VentaNeta) * 100;
            if (double.IsNaN(txtFletesPagadosPorc) || double.IsInfinity(txtFletesPagadosPorc))
            {
                txtFletesPagadosPorc = 0;
            }

            double txtCargoUCSPorc = ((VentaNeta * (Convert.ToSingle(cd.Cd_CargoUCS) / 100)) / VentaNeta) * 100;
            
            if (double.IsNaN(txtCargoUCSPorc) || double.IsInfinity(txtCargoUCSPorc))
            {
                txtCargoUCSPorc = 0;
            }
            double txtUafirMensualPorc = (UafirMensual / VentaNeta) * 100;
            if (double.IsNaN(txtUafirMensualPorc) || double.IsInfinity(txtUafirMensualPorc))
            {
                txtUafirMensualPorc = 0;
            }
            double txtContribucionGastosFijosPapelPorc = (/*(VentaNetaPapel * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosPapel) / 100))*/(VentaNeta * (Convert.ToSingle(FactorFijos) / 100)) / VentaNeta) * 100;
            if (double.IsNaN(txtContribucionGastosFijosPapelPorc) || double.IsInfinity(txtContribucionGastosFijosPapelPorc))
            {
                txtContribucionGastosFijosPapelPorc = 0;
            }

            double txtContribucionGastosFijosOtrosPorc = ((VentaNetaOtros * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosOtros) / 100)) / VentaNeta) * 100;

            if (double.IsNaN(txtContribucionGastosFijosOtrosPorc) || double.IsInfinity(txtContribucionGastosFijosOtrosPorc))
            {
                txtContribucionGastosFijosOtrosPorc = 0;
            }
            double txtAmortizacionPorc = (AmortizacionTotal / VentaNeta) * 100;
            if (double.IsNaN(txtAmortizacionPorc) || double.IsInfinity(txtAmortizacionPorc))
            {
                txtAmortizacionPorc = 0;
            }
            double txtCostoServEquipoPorc = (Prd_PesConTecnico / VentaNeta) * 100;
            if (double.IsNaN(txtCostoServEquipoPorc) || double.IsInfinity(txtCostoServEquipoPorc))
            {
                txtCostoServEquipoPorc = 0;
            }
            //double txtComisionRepPorc = ((UtilidadBruta * (Convert.ToSingle(cd.Cd_ComisionRik) / 100)) / VentaNeta) * 100;
            double txtComisionRepPorc = ((UtilidadBruta * (Convert.ToSingle(txtGastosServirCliente.DbValue) / 100)) / VentaNeta) * 100;

            if (double.IsNaN(txtComisionRepPorc) || double.IsInfinity(txtComisionRepPorc))
            {
                txtComisionRepPorc = 0;
            }
            double txtUtilidadPorc = (UtilidadBruta / VentaNeta) * 100;
            if (double.IsNaN(txtUtilidadPorc) || double.IsInfinity(txtUtilidadPorc))
            {
                txtUtilidadPorc = 0;
            }
            //double txtManoObraPorc = (Cte_CarMP / VentaNeta) * 100;
            double txtManoObraPorc = (Convert.ToDouble(txtManoObra.Text) / VentaNeta) * 100;

            if (double.IsNaN(txtManoObraPorc) || double.IsInfinity(txtManoObraPorc))
            {
                txtManoObraPorc = 0;
            }

            //double txtFletePorc2 = ((/*CostoMaterial*/CostoMaterialNOPapel * (Convert.ToSingle(cd.Cd_Flete) / 100)) / VentaNeta) * 100;
            double txtFletePorc2 = ((/*CostoMaterial*/CostoMaterialNOPapel * (Convert.ToSingle(txtFleteLocales.DbValue) / 100)) / VentaNeta) * 100;


            if (double.IsNaN(txtFletePorc2) || double.IsInfinity(txtFletePorc2))
            {
                txtFletePorc2 = 0;
            }
            double txtCostoMaterialPorc = (CostoMaterial / VentaNeta) * 100;
            if (double.IsNaN(txtCostoMaterialPorc) || double.IsInfinity(txtCostoMaterialPorc))
            {
                txtCostoMaterialPorc = 0;
            }
            double txtUtilidadMarginalPorc = (UtilidadMarginal / VentaNeta) * 100;
            if (double.IsNaN(txtUtilidadMarginalPorc) || double.IsInfinity(txtUtilidadMarginalPorc))
            {
                txtUtilidadMarginalPorc = 0;
            }

            //Formular HTML
            String CadenaHtml;


            CadenaHtml = "";

            CadenaHtml = CadenaHtml + "        <table width=\"100%\">";
            CadenaHtml = CadenaHtml + "            <tr>";
            CadenaHtml = CadenaHtml + "                <td colspan=\"4\" align=\"center\" style=\"font-family: Verdana; font-size: 12pt;background-color: #A9BCF5\"><b>Determinación de la inversión en activos netos de la operación</b></td>";
            CadenaHtml = CadenaHtml + "                <td colspan=\"5\" align=\"center\" style=\"font-family: Verdana; font-size: 12pt;background-color: #A9BCF5\"><b>Cálculo del Uafir anual después de impuestos</b></td>";
            CadenaHtml = CadenaHtml + "            </tr>";
            CadenaHtml = CadenaHtml + "            <tr>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Son los Días de Crédito que tiene el Cliente Autorizado en el Sistema\">Cuentas por cobrar [" + String.Format("{0:N}", DiasRotacion) + "]</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", ctaPorCobrar) + "</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Formula :  (Facturas Notas de Cargo-Cancelaciones de Facturas Operadas – Notas de Crédito) Promedio Calculada del Periodo Seleccionado\">Venta neta</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\"><a href=\"#\" title=\"Click Ver Detalle de Resultado\" onclick=\"popup()\">$" + String.Format("{0:N}", VentaNeta) + "</a></td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">100.00%</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "            </tr>";
            CadenaHtml = CadenaHtml + "            <tr>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default de 20 Días;Formula : (Costo de Material/30)* 25\">Inventario (Días) [" + String.Format("{0:N}", Convert.ToDouble(txtInventario.Text)) + "]</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", Convert.ToDouble(((CostoMaterial / 30) * Convert.ToDouble(txtInventario.Text)))) + "</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Formula : Son las Ventas Promedio del Periodo Seleccionado evaluadas al Costo AAA.\">Costo de material</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\"><a href=\"#\" title=\"Click Ver Detalle de Resultado\" onclick=\"popup()\">$" + String.Format("{0:N}", CostoMaterial) + "</a></td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">" + String.Format("{0:N}", ((CostoMaterial / VentaNeta) * 100)) + "%</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "            </tr>";
            CadenaHtml = CadenaHtml + "            <tr>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"En Construcción\">Inventario en consignación (Días) [0.00]</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$0.00</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Formula : Venta Neta - Costo Material\">Utilidad Prima</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\"><a href=\"#\" title=\"Click Ver Detalle de Resultado\" onclick=\"popup()\">$" + String.Format("{0:N}", (VentaNeta - CostoMaterial)) + "</a></td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">" + String.Format("{0:N}", (((VentaNeta - CostoMaterial) / VentaNeta) * 100)) + "%</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "            </tr>";
            CadenaHtml = CadenaHtml + "            <tr>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            //CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default de 0.5;(Costo de los Equipos en Comodato)* 0.5\">Inversión en Equipo Comodato  (costo*" + String.Format("{0:N}", cd.Cd_FactorInvComodato) + ")</td>";
            //CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\"><a href=\"#\" title=\"Click Ver Detalle de Resultado\" onclick=\"popup()\">$" + String.Format("{0:N}", (TotalInversionComodatos * Convert.ToSingle(cd.Cd_FactorInvComodato))) + "</a></td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default 3.1;Formula : (Venta Neta del Periodo/30)*3.1\">Inversión en activos fijos [" + String.Format("{0:N}", Convert.ToDouble(txtInversionactivosfijos.Text)) + "]</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"border-bottom: 1px solid #333333;font-family: Verdana; font-size: 10pt;\">" + String.Format("{0:N}", ((VentaNeta / 30) * Convert.ToDouble(txtInversionactivosfijos.Text))) + "</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Formula : Importe identificado por Cliente y Territorio de Mano de Obra en Proyectos.\">Mano de obra en proyectos</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", Cte_CarMP) + "</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "            </tr>";
            CadenaHtml = CadenaHtml + "            <tr>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : Cuentas por Cobrar+ Inventario(Días)+ Inventario en Consignación (Días)+ Inversión en Activos Fijos\"><b>Inversión total en activos</b></td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", inversionTotalActivos) + "</b></td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default 3.0;Formula : (Costo de Material de los Productos que NO sin Papel * (3.0)/100)\">Flete al CD [" + String.Format("{0:N}", Convert.ToDouble(txtFleteLocales.Text)) + "]</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", (/*CostoMaterial*/CostoMaterialNOPapel * (Convert.ToSingle(Convert.ToDouble(txtFleteLocales.Text)) / 100))) + "</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "            </tr>";
            CadenaHtml = CadenaHtml + "            <tr>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default 45;(Costo de Material/30)* 45 Más IVA del CD.\">Financiamiento de proveedores (" + String.Format("{0:N}", Convert.ToDouble(txtFinanciamientoproveedores.Text)) + " Días)</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"border-bottom: 1px solid #333333;font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", financiamientoProveedores) + "</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Importe de Amortización del Territorio y Cliente Seleccionado a la Fecha generada el reporte\">Amortización en Equipos</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\"><a href=\"#\" title=\"Click Ver Detalle de Resultado\" onclick=\"popup()\">$" + String.Format("{0:N}", AmortizacionTotal) + "</a></td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "            </tr>";
            CadenaHtml = CadenaHtml + "";
            CadenaHtml = CadenaHtml + "            <tr>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default Cetes : 5.00 y Tasa Incremental al Costo Capital : 15.00;Formula : (Inversión en activos netos op'n - Financiamiento de Proveedores)  * ( (5.00)+ (15.00) )/100\">Costo de capital [" + String.Format("{0:N}", (Convert.ToDouble(txtCetes.Text) + Convert.ToDouble(txtCostodecapital.Text))) + "%]</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", CostoCapital) + "</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Estadística de Base Instalada del Territorio y Cliente Seleccionado a la Fecha generada el reporte evaluada a los Pesos por Equipo.\">Costo Servicio a Equipos</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"border-bottom: 1px solid #333333;font-family: Verdana; font-size: 10pt;\"><a href=\"#\" title=\"Click Ver Detalle de Resultado\" onclick=\"popup()\">$" + String.Format("{0:N}", Prd_PesConTecnico) + "</a></td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "            </tr>";
            CadenaHtml = CadenaHtml + "";

            CadenaHtml = CadenaHtml + "            <tr>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"center\" colspan=\"2\" rowspan=\"6\">";


            Int32 AnioIndice = 0;
            Double Flujo = 0;
            Double VPFlujo = 0;
            Double TotalVPFlujo = 0;


            CadenaHtml = CadenaHtml + "        <table  border=\"1\" spacing=\"0\">";
            CadenaHtml = CadenaHtml + "            <tr>";
            CadenaHtml = CadenaHtml + "                <td align=\"center\" style=\"font-family: Verdana; font-size: 8pt;background-color: #A9BCF5\"><b>Año</b></td>";
            CadenaHtml = CadenaHtml + "                <td align=\"center\" style=\"font-family: Verdana; font-size: 8pt;background-color: #A9BCF5\"><b>Flujo anuales</b></td>";
            CadenaHtml = CadenaHtml + "                <td align=\"center\" style=\"font-family: Verdana; font-size: 8pt;background-color: #A9BCF5\"><b>VP Flujos</b></td>";
            CadenaHtml = CadenaHtml + "            </tr>";

            if (MaxMeses == 0)
            {
                MaxMeses = 1;
            }
            MaxMeses = Convert.ToInt32(txtVigencia.Text)*12;

            while (AnioIndice < Convert.ToInt32(MaxMeses / 12) + 1)
            {
                if (AnioIndice == 0)
                {
                    Flujo = ((inversionTotalActivos + TotalInversionComodatos) - financiamientoProveedores) * -1;
                    VPFlujo = ((inversionTotalActivos + TotalInversionComodatos) - financiamientoProveedores) * -1;
                }
                else
                {


                    if (AnioIndice == Convert.ToInt32(MaxMeses / 12) )
                    {
                        Flujo = ((UafirMensual + AmortizacionTotal) * 12) + ((inversionTotalActivos-((VentaNeta / 30) * Convert.ToDouble(txtInversionactivosfijos.Text))) - financiamientoProveedores);
                    }
                    else
                    {
                        Flujo = ((UafirMensual + AmortizacionTotal) * 12);
                    }



                    //((inversionTotalActivos + TotalInversionComodatos) - financiamientoProveedores)


                    VPFlujo = Flujo / Math.Pow((((cd.Cd_TasaCetes.Value + cd.Cd_TasaIncCostoCapital.Value) / 100) + 1), AnioIndice);
                } 
                TotalVPFlujo = TotalVPFlujo + VPFlujo;

                CadenaHtml = CadenaHtml + "            <tr>";
                CadenaHtml = CadenaHtml + "                <td align=\"center\" style=\"font-family: Verdana; font-size: 10pt;\">" + Convert.ToString(AnioIndice).Trim() + "</td>";
                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", Flujo) + "</td>";
                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", VPFlujo) + "</td>";
                CadenaHtml = CadenaHtml + "            </tr>";

                AnioIndice++;
            }


            CadenaHtml = CadenaHtml + "            <tr>";
            CadenaHtml = CadenaHtml + "                <td align=\"center\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", TotalVPFlujo) + "</td>";
            CadenaHtml = CadenaHtml + "            </tr>";
            CadenaHtml = CadenaHtml + "</table></td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : Venta Neta – (Costo de Material+ Mano de Obra en Proyectos+ Flete+ Amortización en Equipos+ Costo Servicio Equipos)\"><b>Utilidad bruta</b></td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", UtilidadBruta) + "<b></td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">" + String.Format("{0:N}", ((UtilidadBruta / VentaNeta) * 100)) + "%</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "            </tr>";
            CadenaHtml = CadenaHtml + "";
            CadenaHtml = CadenaHtml + "            <tr>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se definio fijo por default 20;Formula : (Utilidad Bruta* (20/100))\">Gastos de Servir al Cliente [" + String.Format("{0:N}", Convert.ToDouble(txtGastosServirCliente.Text)) + "]</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", (UtilidadBruta * (Convert.ToSingle(Convert.ToDouble(txtGastosServirCliente.Text)) / 100))) + "</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "            </tr>";
            CadenaHtml = CadenaHtml + "";
            CadenaHtml = CadenaHtml + "";
            CadenaHtml = CadenaHtml + "            <tr>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Formula : (Venta Neta * (Factor de Otros Gastos Variables Aplicados al Terr /100)) \">Gastos var. aplicados al terr</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", (Cte_GasVarT / VentaNeta)) + "</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "            </tr>";
            CadenaHtml = CadenaHtml + "";
            CadenaHtml = CadenaHtml + "";
            CadenaHtml = CadenaHtml + "            <tr>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"(Venta Neta * (Fletes Pagados al Cliente /100))\">Fletes pagados al cte</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"border-bottom: 1px solid #333333;font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", Cte_FletePaga) + "</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "            </tr>";
            CadenaHtml = CadenaHtml + "";
            CadenaHtml = CadenaHtml + "            <tr>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : Utilidad Bruta- Gastos de Servir al Cliente - Gastos Variables Aplicados al Terr.- Otros Gastos Variables- Fletes Pagados al Cliente\"><b>Utilidad marginal</b></td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", UtilidadMarginal) + "</b></td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">" + String.Format("{0:N}", ((UtilidadMarginal / VentaNeta) * 100)) + "%</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "            </tr>";
            CadenaHtml = CadenaHtml + "";
            /*                    CadenaHtml = CadenaHtml + "            <tr>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default 12.00;Formula :  (Venta Neta NO Papel * (12.00 /100))\">Contrib. a gastos fijos a otros [" + String.Format("{0:N}", cd.Cd_ContribucionGastosFijosOtros) + "]</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", (VentaNetaOtros * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosOtros) / 100))) + "</td>";
                                CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
                                CadenaHtml = CadenaHtml + "            </tr>"; */
            CadenaHtml = CadenaHtml + "";
            CadenaHtml = CadenaHtml + "            <tr>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó evaluar la venta en una ecuación \">Contribución a gastos fijos[" + String.Format("{0:N}", FactorFijos) + "]</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", (VentaNeta * (Convert.ToSingle(FactorFijos) / 100))) + "</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "            </tr>";
            CadenaHtml = CadenaHtml + "";
            CadenaHtml = CadenaHtml + "";
            CadenaHtml = CadenaHtml + "            <tr>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó evaluar la venta en una ecuación \">Cargo UCS's [" + String.Format("{0:N}", FactorUCS) + "]</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"border-bottom: 1px solid #333333;font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", (VentaNeta * (Convert.ToSingle(FactorUCS) / 100))) + "</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "            </tr>";
            CadenaHtml = CadenaHtml + "";
            CadenaHtml = CadenaHtml + "            <tr>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : Utilidad Marginal- Contrib. a Gastos Fijos Otros- Contrib. a Gastos Fijos Papel- Cargos UCS’s\"><b>Uafir mensual</b></td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", UafirMensual) + "</b></td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">" + String.Format("{0:N}", ((UafirMensual / VentaNeta) * 100)) + "%</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "            </tr>";
            CadenaHtml = CadenaHtml + "";
            CadenaHtml = CadenaHtml + "            <tr>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : Uafir Mensual*12\"><b>Uafir anual</b></td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", (UafirMensual * 12)) + "</b></td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "            </tr>";
            CadenaHtml = CadenaHtml + "";
            CadenaHtml = CadenaHtml + "";
            CadenaHtml = CadenaHtml + "            <tr>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Formula : Uafir Anual*(40.00/100)\">ISR y PTU (	" + String.Format("{0:N}", Convert.ToDouble(txtIsr.Text)) + "%	)</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"border-bottom: 1px solid #333333;font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", txtISRyPTUMon) + "</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "            </tr>";
            CadenaHtml = CadenaHtml + "";
            CadenaHtml = CadenaHtml + "";
            CadenaHtml = CadenaHtml + "            <tr>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : Uafir Anual- ISR Y PTU\"><b>Uafir después de impuestos</b></td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", UafirDespuesImpuestos) + "</b></td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "            </tr>";
            CadenaHtml = CadenaHtml + "";
            CadenaHtml = CadenaHtml + "";
            CadenaHtml = CadenaHtml + "            <tr>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"font-family: Verdana; font-size: 10pt;\" title=\"Se determinó dejar fijo por default Cetes : 5.00 y Tasa Incremental al Costo Capital : 15.00;Formula : Inversión en activos netos op'n   * ( (5.00)+ (15.00) )/100\">Costo de capital [" + String.Format("{0:N}", (Convert.ToDouble(txtCetes.Text) + Convert.ToDouble(txtCostodecapital.Text))) + "]</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"border-bottom: 1px solid #333333;font-family: Verdana; font-size: 10pt;\">$" + String.Format("{0:N}", CostoCapital) + "</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "            </tr>";
            CadenaHtml = CadenaHtml + "";
            CadenaHtml = CadenaHtml + "            <tr>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td align=\"left\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\" title=\"Formula : UAFIR Después de Impuestos – Costo Capital\"><b>Utilidad remanente</b></td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"background-color: #E6E6E6;font-family: Verdana; font-size: 10pt;\"><b>$" + String.Format("{0:N}", (UafirDespuesImpuestos - CostoCapital)) + "</b></td>";
            CadenaHtml = CadenaHtml + "                <td align=\"right\" style=\"font-family: Verdana; font-size: 10pt;\">&nbsp;</td>";
            CadenaHtml = CadenaHtml + "                <td>&nbsp;</td>";
            CadenaHtml = CadenaHtml + "            </tr>";
            CadenaHtml = CadenaHtml + "</table>";

            
            
            
            return CadenaHtml;
        }
        private void Guardar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ValuacionProyecto valuacionProyecto = this.LlenarObjetoValProyecto();
                string mensaje = string.Empty;

                ValuacionParametrosActual vpactual = new ValuacionParametrosActual();
                vpactual.Id_Emp = sesion.Id_Emp;
                vpactual.Id_Cd = sesion.Id_Cd_Ver;
                vpactual.Id_Vap = Convert.ToInt32(txtFolio.Text);
                vpactual.txtCuentasPorCobrar = Convert.ToDouble(txtCuentasPorCobrar.Text);
                vpactual.txtInventario = Convert.ToDouble(txtInventario.Text);
                vpactual.txtGastosServirCliente = Convert.ToDouble(txtGastosServirCliente.Text);
                vpactual.txtVigencia = Convert.ToDouble(txtVigencia.Text);
                vpactual.txtFleteLocales = Convert.ToDouble(txtFleteLocales.Text);
                vpactual.txtIsr = Convert.ToDouble(txtIsr.Text);
                vpactual.txtCetes = Convert.ToDouble(txtCetes.Text);
                vpactual.txtFinanciamientoproveedores = Convert.ToDouble(txtFinanciamientoproveedores.Text);
                vpactual.txtInversionactivosfijos = Convert.ToDouble(txtInversionactivosfijos.Text);
                vpactual.txtCostodecapital = Convert.ToDouble(txtCostodecapital.Text);
                vpactual.txtManoObra = Convert.ToDouble(txtManoObra.Text);




                ValuacionParametros vp = new ValuacionParametros();
                vp.Id_Cd = sesion.Id_Cd_Ver;

                vp.Id_Emp = sesion.Id_Emp;
                vp.Vap_Vigencia = txtVigencia.Value.HasValue ? (int)txtVigencia.Value.Value : 0;
                //vp.Vap_Participacion = txtComision.Value.HasValue ? txtComision.Value.Value : 0;
                vp.Vap_Mano_Obra = txtManoObra.Value.HasValue ? txtManoObra.Value.Value : 0;
                //vp.Vap_Amortizacion = txtAmortizacion.Value.HasValue ? txtAmortizacion.Value.Value : 0;
                //vp.Vap_Numero_Entregas = txtNumEntregas.Value.HasValue ? (int)txtNumEntregas.Value.Value : 0;
                //vp.Vap_Costo_Entregas = txtCostoEntregas.Value.HasValue ? txtCostoEntregas.Value.Value : 0;
                //vp.Vap_Comision_Factoraje = txtComisionFactoraje.Value.HasValue ? txtComisionFactoraje.Value.Value : 0;
                //vp.Vap_Comision_Anden = txtComisionCruce.Value.HasValue ? txtComisionCruce.Value.Value : 0;
                vp.Vap_Gasto_Flete_Locales = txtFleteLocales.Value.HasValue ? txtFleteLocales.Value.Value : 0;
                //vp.Vap_IVA = txtIva.Value.HasValue ? txtIva.Value.Value : 0;
                //vp.Vap_Plazo_Pago_Cliente = txtPlazoPago.Value.HasValue ? txtPlazoPago.Value.Value : 0;
                //vp.Vap_Inventario_Key = txtInventarioKey.Value.HasValue ? (int)txtInventarioKey.Value.Value : 0;
                //vp.Vap_Inventario_Consignacion = txtInventarioKeyConsignacion.Value.HasValue ? (int)txtInventarioKeyConsignacion.Value.Value : 0;
                //vp.Vap_Credito_Key = txtCreditoProveedor.Value.HasValue ? (int)txtCreditoProveedor.Value.Value : 0;
                //vp.Vap_Credito_Papel = txtCreditoProveedorPapel.Value.HasValue ? (int)txtCreditoProveedorPapel.Value.Value : 0;
                vp.Vap_ISR = txtIsr.Value.HasValue ? txtIsr.Value.Value : 0;
                //vp.Vap_Ucs = txtUcs.Value.HasValue ? txtUcs.Value.Value : 0;
                vp.Vap_Cetes = txtCetes.Value.HasValue ? txtCetes.Value.Value : 0;
                //vp.Vap_Adicional_Cetes = txtAdicionalCetes.Value.HasValue ? txtAdicionalCetes.Value.Value : 0;
                //vp.Vap_Costos_Fijos_No_Papel = txtCostosFijosNoPapel.Value.HasValue ? txtCostosFijosNoPapel.Value.Value : 0;
                //vp.Vap_Costos_Fijos_Papel = txtCostosFijosPapel.Value.HasValue ? txtCostosFijosPapel.Value.Value : 0;
                //vp.Vap_Gastos_Admin = txtGAdmitivos.Value.HasValue ? txtGAdmitivos.Value.Value : 0;
                //vp.Vap_Inversion_Activos = txtInversionActivosFijos.Value.HasValue ? (int)txtInversionActivosFijos.Value.Value : 0;
                int verificador = 0;
                if (this.hiddenId.Value == string.Empty) //nueva valuación de proyecto
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }
                    if (valuacionProyecto.ListaProductosValuacionProyecto.Count > 0)
                    {
                        new CN_CapValuacionProyecto().InsertarValuacionProyecto(ref valuacionProyecto, vp, sesion.Emp_Cnx, ref verificador, vpactual);
                        EnviaEmail(Convert.ToInt32(txtFolio.Text));
                        mensaje = "Los datos se guardaron correctamente";
                        RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')")); //cerrar ventana radWindow de factura
                    }
                    else
                        Alerta("Ingrese por lo menos un producto en el detalle");
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }
                    if (valuacionProyecto.ListaProductosValuacionProyecto.Count > 0)
                    {
                        new CN_CapValuacionProyecto().ModificarValuacionProyecto(ref valuacionProyecto, vp, sesion.Emp_Cnx, ref verificador, vpactual);
                        EnviaEmail(Convert.ToInt32(txtFolio.Text));
                        mensaje = "Los datos se modificaron correctamente";
                        RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')")); //cerrar ventana radWindow de factura
                    }
                    else
                        Alerta("Ingrese por lo menos un producto en el detalle");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = Sesion.Id_U;
                Permiso.Id_Cd = Sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;

                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                        this.RadToolBar1.Items[1].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void EnviaEmail(int solicitud)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd_Ver;
                configuracion.Id_Emp = session.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);
                if (configuracion.Mail_Valuacion.Length == 0)
                {
                    Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                    return;
                }
                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table style='font-family: verdana; font-size:9pt'><tr><td>");
                cuerpo_correo.Append("<img src=\"cid:companylogo\"></td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'>");
                cuerpo_correo.Append("Se ha colocado una solicitud de autorización de valuación de proyectos con el número de folio <b>" + solicitud.ToString() + "</b>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                cuerpo_correo.Append("Centro de distribución: <b>" + session.Id_Cd_Ver + " - " + session.Cd_Nombre + "</b>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                cuerpo_correo.Append("Solicitó: <b>" + session.Id_U + " - " + session.U_Nombre + "</b>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'><br>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");

                string[] url = Request.Url.ToString().Split(new char[] { '/' });

                cuerpo_correo.Append("<center><br>");
                cuerpo_correo.Append("<a href='" + Request.Url.ToString().Replace(url[url.Length - 1], "") + "CapValProyectos_Autorizacion.aspx?Id1=" + solicitud.ToString() + "&id2=" + session.Id_Emp + "&id3=" + session.Id_Cd_Ver + "'>Solicitud de autorización de valuación de proyectos</a></font></center>");

                // cuerpo_correo.Append("<center><br><a href='http://" + url + "/CapValProyectos_Autorizacion.aspx?id1=" + solicitud.ToString() + "&id2=" + session.Id_Emp + "&id3=" + session.Id_Cd_Ver + "'>Solicitud de autorización de valuación de proyectos</a></font></center>");
                // cuerpo_correo.Append("<center><br><a href='" + Request.Url.ToString().Replace((new FileInfo(Request.Url.AbsolutePath)).Name, "") + "CapValProyectos.aspx?id1=" + solicitud.ToString() + "&id2=" + session.Id_Emp + "&id3=" + session.Id_Cd_Ver + "'>Solicitud de autorización de valuación de proyectos</a></font></center>");
                cuerpo_correo.Append("</td></tr></table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);

                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);

                string To = configuracion.Mail_Valuacion;//cambiar al hacerlo jalar por Mail_ValProyectos
                for (int x = 0; x < To.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).Length; x++)
                    m.To.Add(new MailAddress(To.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)[x]));

                m.Subject = "Solicitud de autorización de valuación de proyectos";
                m.IsBodyHtml = true;
                string body = cuerpo_correo.ToString();
                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande
                try
                {
                    LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
                    logo.ContentId = "companylogo";
                    vistaHtml.LinkedResources.Add(logo);
                }
                catch (Exception)
                {
                }
                m.AlternateViews.Add(vistaHtml);
                sm.Send(m);
            }
            catch (Exception)
            {
                Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
            }
        }
        private void parametros()
        {
            try
            {
                Id_Vap = Convert.ToInt32(Page.Request.QueryString["Id_Vap"]);
                Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
                _PermisoGuardar = Convert.ToInt32(Page.Request.QueryString["permisoGuardar"]) == 1 ? true : false;
                _PermisoModificar = Convert.ToInt32(Page.Request.QueryString["permisoModificar"]) == 1 ? true : false;
                _PermisoEliminar = Convert.ToInt32(Page.Request.QueryString["permisoEliminar"]) == 1 ? true : false;
                _PermisoImprimir = Convert.ToInt32(Page.Request.QueryString["permisoImprimir"]) == 1 ? true : false;
                _Editable = Page.Request.QueryString["modificable"];
                if (_Editable == "0")
                    RadToolBar1.FindItemByValue("save").Visible = false;
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
                return CN_Comun.Maximo(sesion.Id_Emp, sesion.Id_Cd_Ver, "CapValProyecto", "Id_Vap", sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarProductos(RadComboBox sender)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatProducto_Combo", ref sender);
                sender.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //private void CargarComboCliente()
        //{
        //    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
        //    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
        //    CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatCliente_Combo", ref cmbCliente);
        //}
        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("rgValProyectoDet_insert_repetida"))
                    Alerta("Este producto ya ha sido capturado");
                else
                    if (mensaje.Contains("PermisoGuardarNo"))
                        Alerta("No tiene permisos para grabar");
                    else
                        if (mensaje.Contains("PermisoModificarNo"))
                            Alerta("No tiene permisos para actualizar");
                        else
                            if (mensaje.Contains("cmbProducto_IndexChanging_error"))
                                Alerta("Error al consultar los datos del producto");
                            else
                                if (mensaje.Contains("CapNotaCredito_insert_ok"))
                                    Alerta("Los datos se guardaron correctamente");
                                else
                                    if (mensaje.Contains("CapValProyecto_insert_error"))
                                        Alerta("No se pudo guardar la valuación de proyecto");
                                    else
                                        if (mensaje.Contains("CapNotaCredito_update_ok"))
                                            Alerta("Los datos se modificaron correctamente");
                                        else
                                            if (mensaje.Contains("CapValProyecto_update_error"))
                                                Alerta("No se pudo actualizar la valuación de proyecto");
                                            else
                                                if (mensaje.Contains("rgDetalleDet_NeedDataSource"))
                                                    Alerta("Error al cargar el grid de detalle de la valuación de proyecto");
                                                else
                                                    if (mensaje.Contains("rgDetalle_ItemDataBound"))
                                                        Alerta("Error al momento de preparar un registro para edición");
                                                    else
                                                        if (mensaje.Contains("rgValProyectoDet_insert_error"))
                                                            Alerta("Error al momento de agregar la partida a la lista de partidas de la valuación de proyecto");
                                                        else
                                                            if (mensaje.Contains("rgNotaCreditoDet_delete_error"))
                                                                Alerta("Error al momento de eliminar el producto a la lista de productos de la nota de crédito");
                                                            else
                                                                Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }
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
        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
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