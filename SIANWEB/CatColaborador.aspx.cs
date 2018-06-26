using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Configuration;
using System.IO;

using Telerik.Web.UI;

using CapaEntidad;
using CapaNegocios;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.OleDb;
using System.Data;


namespace SIANWEB
{
    public partial class CatColaborador : System.Web.UI.Page
    {
        #region Variables
        public string NombreArchivo;
        public string NombreHojaExcel;
        private DataTable dtDet
        {
            //Se quita variable sesion para evitar que se borren las partidas de los pagos
            // 07/11/2016
            get
            {
                return (DataTable)Session["dtDetPagos"];
            }
            set
            {
                Session["dtDetPagos"] = value;
            }
        }
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        private List<ColaboradorObjetivo> listSource
        {
            get { return (List<ColaboradorObjetivo>)Session["listSource"]; }
            set { Session["listSource"] = value; }
        }

        private List<ConceptosNomina> listConceptosNomina
        {
            get { return (List<ConceptosNomina>)Session["listConceptosNomina"]; }
            set { Session["listConceptosNomina"] = value; }
        }


        public int IdProducto
        {
            get { return Convert.ToInt32(Session["_IdProducto"]); }
            set { Session["_IdProducto"] = value; }
        }

        public string Valor
        {
            get
            {
                //return MaximoId();
                return "1";
            }
            set { }
        }

        public string ValorEmpleado
        {
            get
            {
                //return MaximoId();
                return "1";
            }
            set { }
        }

        #endregion
        #region Eventos
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                CargarProductos();
                //this.LlenarComboProductosLista(string.Empty);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!IsPostBack)
                    {
                        Session["_IdProducto"] = 0;
                        Session["listSource"] = null;
                        Session["listConceptosNomina"] = null;
                        this.ValidarPermisos();
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("(function(){var f = function(){AbrirContrasenas(); return false;Sys.Application.remove_load(f);};Sys.Application.add_load(f);})()");
                            return;
                        }
                        txtId_Colaborador.Text = this.Valor;
                        txtId_Empleado.Text = this.ValorEmpleado;
                        Inicializar();

                        this.RadTabStripPrincipal.SelectedIndex = 0;
                        this.RadMultiPagePrincipal.SelectedIndex = 0;

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void cmbCentrosDist_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null) { string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries); Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false); } CN__Comun comun = new CN__Comun(); comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);

                CargarProductos();
                //preparar controles
                this.NuevoProducto();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #region grdPrecios Eventos
        protected void grdPrecios_PreRender(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                //foreach (GridDataItem item in rgPrecios.MasterTableView.Items)
                //{
                //    if (Convert.ToBoolean(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Estatus"]))
                //    {   //si es precio actual, se colorea de azul el Row                    
                //        foreach (TableCell cell in item.Cells)
                //        {
                //            cell.CssClass = "styleCellRowAGridPrecios";
                //        }
                //    }
                //    else //Se quita la capacidad de edición del precio                   
                //        item["EditCommandColumn"].Controls[0].Visible = false;
                //}
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void grdPrecios_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                GridEditFormItem editedItem = e.Item as GridEditFormItem;

                ColaboradorObjetivo productoPrecio = new ColaboradorObjetivo();
                productoPrecio.Id_Emp = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Emp"]);
                productoPrecio.Id_Cd = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Cd"]);
                productoPrecio.Id_Colaborador = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Colaborador"]);
                productoPrecio.Id_ColaboradorObjetivo = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_ColaboradorObjetivo"]);

                //productoPrecio.Estatus = Convert.ToBoolean(((Literal)editedItem["Estatus"].Controls[1]).Text);
                //productoPrecio.Prd_FechaInicio = Convert.ToDateTime((editedItem["Prd_FechaInicio"].FindControl("datePickerFechaInicio") as RadDatePicker).SelectedDate);
                //productoPrecio.Prd_FechaFin = Convert.ToDateTime((editedItem["Prd_FechaFin"].FindControl("datePickerFechaFin") as RadDatePicker).SelectedDate);
                productoPrecio.Anio = (editedItem["Anio"].FindControl("txtAnio") as RadTextBox).Text.Trim();
                productoPrecio.Mes = (editedItem["Mes"].FindControl("lblTipoPrecioEdit") as Label).Text.Trim();
                productoPrecio.Objetivo = Convert.ToSingle((editedItem["Objetivo"].FindControl("txtObjetivo") as RadNumericTextBox).Text);

                //Checar que es un rango de fechas correcto para SQL Server
                ////if (Convert.ToDateTime(productoPrecio.Prd_FechaFin).CompareTo(new DateTime(1753, 1, 1)) < 0 || Convert.ToDateTime(productoPrecio.Prd_FechaInicio).CompareTo(new DateTime(1753, 1, 1)) < 0)
                ////    throw new Exception("rgPrecios_FechasRango_incorrecto");

                List<ColaboradorObjetivo> listaProdPre = new List<ColaboradorObjetivo>();
                for (int i = 0; i < this.listSource.Count; i++)
                    listaProdPre.Add((ColaboradorObjetivo)this.ClonarPrecioProducto(this.listSource[i]));

                //for (int i = 0; i < this.listSource.Count / 2; i++)
                //{
                //    listaProdPre[i].Prd_FechaInicio = null;
                //    listaProdPre[i].Prd_FechaFin = null;
                //}
                //this.ValidarPeriodosPrecioProducto(ref productoPrecio, listaProdPre);

                //Agregar precio a la lista actual
                foreach (ColaboradorObjetivo p in this.listSource)
                {
                    if (p.Id_ColaboradorObjetivo == productoPrecio.Id_ColaboradorObjetivo) // && p.Estatus == productoPrecio.Estatus && p.Estatus == true)
                    {
                        List<ColaboradorObjetivo> listaPP = new List<ColaboradorObjetivo>(this.listSource);
                        int posicionFila = rgPrecios.CurrentPageIndex * rgPrecios.PageSize + e.Item.ItemIndex;
                        listaPP[posicionFila].Anio = productoPrecio.Anio;
                        listaPP[posicionFila].Mes = productoPrecio.Mes;
                        listaPP[posicionFila].Objetivo = productoPrecio.Objetivo;

                        this.listSource = listaPP;

                        //row["GVComprobante_Observaciones"] = comprobante.GVComprobante_Observaciones;

                        //row["PagElec_Numero"] = comprobante.GVComprobante_GV_Numero;
                        //row["PagElec_Cc"] = comprobante.GVComprobante_GV_Cc;
                        //row["PagElec_Cuenta"] = comprobante.GVComprobante_GV_Cuenta;
                        //row["PagElec_CuentaPago"] = comprobante.GVComprobante_GV_CuentaPago;
                        //row["PagElec_SubCuenta"] = comprobante.GVComprobante_GV_SubCuenta;
                        //row["PagElec_SubSubCuenta"] = comprobante.GVComprobante_GV_SubSubCuenta;
                        //row["PagElec_Id_PagElecCuenta"] = comprobante.GVComprobante_GV_PagElec_Id_PagElecCuenta;

                        break;
                    }
                }
                rgPrecios.Rebind();
            }
            catch (Exception ex)
            {  //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void grdPrecios_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgPrecios.DataSource = this.listSource;
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void grdPrecios_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                ErrorManager();
                if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
                {
                    GridEditFormItem editItem = (GridEditFormItem)e.Item;

                    //string datePickerFechaInicio = ((RadDatePicker)editItem.FindControl("datePickerFechaInicio")).ClientID.ToString();
                    //string datePickerFechaFin = ((RadDatePicker)editItem.FindControl("datePickerFechaFin")).ClientID.ToString();
                    //string txtObjetivo = ((RadNumericTextBox)editItem.FindControl("txtObjetivo")).ClientID.ToString();

                    //string jsControles = string.Concat(
                    //    "datePickerFechaInicioClientId='", datePickerFechaInicio, "';"
                    //    , "datePickerFechaFinClientId='", datePickerFechaFin, "';"
                    //    , "txtObjetivoClientId='", txtObjetivo, "';"
                    //    );

                    //ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                    //if (insertbtn != null)
                    //{
                    //    jsControles = string.Concat(
                    //        jsControles
                    //        , "return ValidaFormGridPrecioProductos(\"insertar\");");

                    //    insertbtn.Attributes.Add("onclick", jsControles);
                    //}

                    //ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    //if (updatebtn != null)
                    //{
                    //    jsControles = string.Concat(
                    //        jsControles
                    //        , "return ValidaFormGridPrecioProductos(\"actualizar\");");

                    //    updatebtn.Attributes.Add("onclick", jsControles);
                    //}
                }
            }
            catch (Exception ex)
            {   //RadGrid1.Controls.Add(new LiteralControl("No se pudo agregar el Usuario. " + ex.Message));
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void grdPrecios_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgPrecios.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }
        #endregion grdPrecios Eventos


        #region grdConceptoNomina Eventos
        protected void grdConceptoNomina_PreRender(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                //foreach (GridDataItem item in rgPrecios.MasterTableView.Items)
                //{
                //    if (Convert.ToBoolean(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Estatus"]))
                //    {   //si es precio actual, se colorea de azul el Row                    
                //        foreach (TableCell cell in item.Cells)
                //        {
                //            cell.CssClass = "styleCellRowAGridPrecios";
                //        }
                //    }
                //    else //Se quita la capacidad de edición del precio                   
                //        item["EditCommandColumn"].Controls[0].Visible = false;
                //}
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void grdConceptoNomina_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                GridEditFormItem editedItem = e.Item as GridEditFormItem;

                ConceptosNomina productoPrecio = new ConceptosNomina();
                productoPrecio.Id_Emp = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Emp"]);
                productoPrecio.Id_Cd = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Cd"]);
                productoPrecio.Id_Colaborador = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Colaborador"]);
                productoPrecio.Id_Compensacion = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Compensacion"]);
                productoPrecio.Id_Empleado = Convert.ToInt32(((Literal)editedItem["Id_empleado"].Controls[1]).Text);
                //productoPrecio.Compensacion_Estatus = Convert.ToInt32(((Literal)editedItem["Compensacion_Estatus"].Controls[1]).Text);
                productoPrecio.Id_Compensacion1 = Convert.ToInt32(((Literal)editedItem["Id_Compensacion1"].Controls[1]).Text);
                productoPrecio.Monto1 = Convert.ToSingle((editedItem["Monto1"].FindControl("txtMonto1") as RadNumericTextBox).Text);


                productoPrecio.Id_Compensacion2 = Convert.ToInt32(((Literal)editedItem["Id_Compensacion2"].Controls[1]).Text);
                productoPrecio.Monto2 = Convert.ToSingle((editedItem["Monto2"].FindControl("txtMonto2") as RadNumericTextBox).Text);

                productoPrecio.Id_Compensacion3 = Convert.ToInt32(((Literal)editedItem["Id_Compensacion3"].Controls[1]).Text);
                productoPrecio.Monto3 = Convert.ToSingle((editedItem["Monto3"].FindControl("txtMonto3") as RadNumericTextBox).Text);

                productoPrecio.Id_Compensacion4 = Convert.ToInt32(((Literal)editedItem["Id_Compensacion4"].Controls[1]).Text);
                productoPrecio.Monto4 = Convert.ToSingle((editedItem["Monto4"].FindControl("txtMonto4") as RadNumericTextBox).Text);

               
        

                List<ConceptosNomina> listaProdPre = new List<ConceptosNomina>();
                for (int i = 0; i < this.listConceptosNomina.Count; i++)
                    listaProdPre.Add((ConceptosNomina)this.ClonarPrecioProducto(this.listConceptosNomina[i]));

                //Agregar precio a la lista actual
                foreach (ConceptosNomina p in this.listConceptosNomina)
                {
                    if (p.Id_Compensacion_Monto == productoPrecio.Id_Compensacion_Monto) // && p.Estatus == productoPrecio.Estatus && p.Estatus == true)
                    {
                        List<ConceptosNomina> listaPP = new List<ConceptosNomina>(this.listConceptosNomina);
                        int posicionFila = grdConceptoNomina.CurrentPageIndex * grdConceptoNomina.PageSize + e.Item.ItemIndex;
                        listaPP[posicionFila].Monto1 = productoPrecio.Monto1;
                        listaPP[posicionFila].Monto2 = productoPrecio.Monto2;
                        listaPP[posicionFila].Monto3 = productoPrecio.Monto3;
                        listaPP[posicionFila].Monto4 = productoPrecio.Monto4;
                        listaPP[posicionFila].Id_Emp = productoPrecio.Id_Emp;
                        listaPP[posicionFila].Id_Cd = productoPrecio.Id_Cd;
                        listaPP[posicionFila].Id_Colaborador = productoPrecio.Id_Colaborador;
                        listaPP[posicionFila].Id_Compensacion = productoPrecio.Id_Compensacion;
                        listaPP[posicionFila].Id_Compensacion1 = productoPrecio.Id_Compensacion1;
                        listaPP[posicionFila].Id_Compensacion2 = productoPrecio.Id_Compensacion2;
                        listaPP[posicionFila].Id_Compensacion3 = productoPrecio.Id_Compensacion3;
                        listaPP[posicionFila].Id_Compensacion4 = productoPrecio.Id_Compensacion4;

                        listaPP[posicionFila].Compensacion_Estatus = productoPrecio.Compensacion_Estatus;
                        listaPP[posicionFila].Id_Compensacion_Monto = productoPrecio.Id_Compensacion_Monto;
                        listaPP[posicionFila].Id_Empleado = productoPrecio.Id_Empleado;
                        //listaPP[posicionFila].Compensacion_Descripcion = productoPrecio.Compensacion_Descripcion;

                        this.listConceptosNomina = listaPP;

                        break;
                    }
                }
                grdConceptoNomina.Rebind();
            }
            catch (Exception ex)
            {  //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void grdConceptoNomina_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    grdConceptoNomina.DataSource = this.listConceptosNomina;
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void grdConceptoNomina_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                ErrorManager();
                if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
                {
                    GridEditFormItem editItem = (GridEditFormItem)e.Item;

                    //string datePickerFechaInicio = ((RadDatePicker)editItem.FindControl("datePickerFechaInicio")).ClientID.ToString();
                    //string datePickerFechaFin = ((RadDatePicker)editItem.FindControl("datePickerFechaFin")).ClientID.ToString();
                    //string txtObjetivo = ((RadNumericTextBox)editItem.FindControl("txtObjetivo")).ClientID.ToString();

                    //string jsControles = string.Concat(
                    //    "datePickerFechaInicioClientId='", datePickerFechaInicio, "';"
                    //    , "datePickerFechaFinClientId='", datePickerFechaFin, "';"
                    //    , "txtObjetivoClientId='", txtObjetivo, "';"
                    //    );

                    //ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                    //if (insertbtn != null)
                    //{
                    //    jsControles = string.Concat(
                    //        jsControles
                    //        , "return ValidaFormGridPrecioProductos(\"insertar\");");

                    //    insertbtn.Attributes.Add("onclick", jsControles);
                    //}

                    //ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    //if (updatebtn != null)
                    //{
                    //    jsControles = string.Concat(
                    //        jsControles
                    //        , "return ValidaFormGridPrecioProductos(\"actualizar\");");

                    //    updatebtn.Attributes.Add("onclick", jsControles);
                    //}
                }
            }
            catch (Exception ex)
            {   //RadGrid1.Controls.Add(new LiteralControl("No se pudo agregar el Usuario. " + ex.Message));
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void grdConceptoNomina_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.grdConceptoNomina.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "grdConceptoNomina_PageIndexChanged"));
            }
        }
        #endregion grdConceptoNomina Eventos


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
                        this.cmbColaboradorsListaArreglaItem0();
                        this.LimpiarCampos();
                        txtId_Colaborador.Text = this.Valor;
                        txtId_Empleado.Text = this.ValorEmpleado;
                        this.NuevoProducto();
                        break;
                    case "save":
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbColaboradorsLista_DataBound(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbColaboradorsLista_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            try
            {
                ErrorManager();
                //if (e.Item.Value == "-1")
                //{
                //    Label lblAuxiliar = new Label();
                //    lblAuxiliar.Width = new Unit(80, UnitType.Pixel);
                //    e.Item.FindControl("liClave").Controls.Clear();
                //    e.Item.FindControl("liComprasLocales").Controls.Clear();
                //    e.Item.FindControl("liActivo").Controls.Clear();
                //    e.Item.FindControl("liDescripcion").Controls.AddAt(0, lblAuxiliar);
                //}
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbColaboradorsLista_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            try
            {
                ErrorManager();
                CargarProductos();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbColaboradorsLista_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                if (e.Value != string.Empty && e.Value != "-1")
                {
                    RadComboBoxItem item = ((RadComboBox)sender).FindItemByValue(e.Value);
                    int id_Cd_Prod = session.Id_Cd_Ver;

                    this.LlenarFormularioProducto(Convert.ToInt32(e.Value), id_Cd_Prod);
                    this.hiddenId.Value = e.Value;
                    txtId_Colaborador.Enabled = false;
                }
                if (e.Value == string.Empty || e.Value == "-1")
                {
                    this.LimpiarCampos();
                    cmbColaboradorsLista.Text = "-- Seleccionar --";
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void cmbColaboradorsListaArreglaItem0()
        {
            Label lblAuxiliar2 = new Label();
            lblAuxiliar2.Width = new Unit(85, UnitType.Pixel);
            Label lblAuxiliar = new Label();
            lblAuxiliar.Text = "-- Seleccionar --";

            if (cmbColaboradorsLista.Items.Count > 0)
            {
                cmbColaboradorsLista.Items[0].FindControl("liActivo").Controls.Clear();
                cmbColaboradorsLista.Items[0].FindControl("liComprasLocales").Controls.Clear();
                cmbColaboradorsLista.Items[0].FindControl("liDescripcion").Controls.Clear();
                cmbColaboradorsLista.Items[0].FindControl("liDescripcion").Controls.AddAt(0, lblAuxiliar);
                cmbColaboradorsLista.Items[0].FindControl("liDescripcion").Controls.AddAt(0, lblAuxiliar2);
            }
        }
        #endregion
        #region Funciones
        public object ClonarPrecioProducto(object obj)
        {
            object objResult = null;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Position = 0;
                objResult = bf.Deserialize(ms);
            }
            return objResult;
        }
        private void Inicializar()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            this.CargarCentros();
            this.CargarProductos();
            this.LlenarComboProductoFamilia();

            this.hiddenId.Value = string.Empty;

            System.Text.StringBuilder str = new System.Text.StringBuilder();
            List<SubFamProducto> listaSF = new List<SubFamProducto>();
            SubFamProducto subFamilia = new SubFamProducto();
            new CN_CatSubFamProducto().ConsultaSubFamProducto(subFamilia, sesion.Emp_Cnx, sesion.Id_Emp, ref listaSF);
            str.Append(string.Concat("arregloSubFamilias = new Array(3);"));
            str.Append(string.Concat("arregloSubFamilias[0] = new Array(", listaSF.Count, ");"));
            str.Append(string.Concat("arregloSubFamilias[1] = new Array(", listaSF.Count, ");"));
            str.Append(string.Concat("arregloSubFamilias[2] = new Array(", listaSF.Count, ");"));
            for (int i = 0; i < listaSF.Count; i++)
            {
                subFamilia = listaSF[i];
                str.Append(string.Concat("arregloSubFamilias[0][", i.ToString(), "] = '", subFamilia.Id_Fam, "';"));
                str.Append(string.Concat("arregloSubFamilias[1][", i.ToString(), "] = '", subFamilia.Id_Sub, "';"));
                str.Append(string.Concat("arregloSubFamilias[2][", i.ToString(), "] = '", subFamilia.Sub_Descripcion, "';"));
            }
            this.listSource = new List<ColaboradorObjetivo>();
            this.listConceptosNomina = new List<ConceptosNomina>();

            this.NuevoProducto();



            if (Session["IdLocal" + Session.SessionID] != null)
            {
                this.txtId_Colaborador.Text = Session["IdLocal" + Session.SessionID].ToString();
                txtId_Empleado.Text = Session["IdLocalEmpleado" + Session.SessionID].ToString();
                txtId_Colaborador.Enabled = false;

                Session["IdLocal" + Session.SessionID] = null;

                if (Session["IdTipoUsuario" + Session.SessionID] != null)
                {
                    int? index = CmbTipoUsuario.FindItemIndexByValue(Session["IdTipoUsuario" + Session.SessionID].ToString());
                    CmbTipoUsuario.SelectedIndex = index != null ? (int)index : 0;
                    CmbTipoUsuario.Text = CmbTipoUsuario.Items[CmbTipoUsuario.SelectedIndex].Text;
                    if (CmbTipoUsuario.SelectedIndex > 0)
                    {
                        txtCorreo.Text = Session["IdTipoUsuario" + Session.SessionID].ToString();
                    }
                }


            }

            lblTituloColaborador.Text = string.Concat(txtId_Colaborador.Text, " - ", txtNombreEmpleado.Text);

            RAM1.ResponseScripts.Add(string.Concat(@"", str.ToString()));
        }
        private void ValidarPeriodosPrecioProducto(ref ColaboradorObjetivo productoPrecio, List<ColaboradorObjetivo> listaPordPre)
        {
            try
            {
                //Checar que el rango de fechas del periodo actual no se empalma con el anterior o viceversa
                foreach (ColaboradorObjetivo p in listaPordPre)
                {
                    if (p.Id_ColaboradorObjetivo == productoPrecio.Id_ColaboradorObjetivo && !p.Estatus)
                    {
                        //Checar en este momento que los rangos no se empalmen

                        //if (Convert.ToDateTime(p.Prd_FechaInicio).CompareTo(productoPrecio.Prd_FechaInicio) >= 0 && Convert.ToDateTime(p.Prd_FechaInicio).CompareTo(productoPrecio.Prd_FechaFin) <= 0)
                        //    throw new Exception("rgPrecios_FechasRango_empalmado");

                        //if (Convert.ToDateTime(p.Prd_FechaFin).CompareTo(productoPrecio.Prd_FechaInicio) >= 0 && Convert.ToDateTime(p.Prd_FechaFin).CompareTo(productoPrecio.Prd_FechaFin) <= 0)
                        //    throw new Exception("rgPrecios_FechasRango_empalmado");

                        //if (Convert.ToDateTime(productoPrecio.Prd_FechaInicio).CompareTo(p.Prd_FechaInicio) >= 0 && Convert.ToDateTime(productoPrecio.Prd_FechaInicio).CompareTo(p.Prd_FechaFin) <= 0)
                        //    throw new Exception("rgPrecios_FechasRango_empalmado");

                        //if (Convert.ToDateTime(productoPrecio.Prd_FechaFin).CompareTo(p.Prd_FechaInicio) >= 0 && Convert.ToDateTime(productoPrecio.Prd_FechaFin).CompareTo(p.Prd_FechaFin) <= 0)
                        //    throw new Exception("rgPrecios_FechasRango_empalmado");

                        //Checar que no haya Dias entre periodos (el dia inicial del periodo nuevo debe ser el siguiente dia despues del dia final del periodo anterior)
                        //NOTA: el precio ke se esta agregando siempre es el actual, los precios anteriores no tienen la opción de Edición
                        ////if (p.Prd_FechaInicio != null && p.Prd_FechaFin != null)
                        ////{

                        ////    //    // Difference in days, hours, and minutes.
                        ////    //    TimeSpan ts = Convert.ToDateTime(productoPrecio.Prd_FechaInicio) - Convert.ToDateTime(p.Prd_FechaFin);

                        ////    //    if (ts.Days > 1)
                        ////    //        throw new Exception("rgPrecios_FechasRango_DiasEntrePeriodo");

                        ////    //    if (ts.Days < 0)
                        ////    //        throw new Exception("rgPrecios_FechasRango_PeridoNuevoAnterior");
                        ////}
                        break;
                    }
                }
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
                return CN_Comun.Maximo(sesion.Id_Emp, "CatProducto", "Id_Colaborador", sesion.Emp_Cnx, "spCatCentral_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool Deshabilitar()
        {
            try
            {
                bool verificador = false;
                if (hiddenId.Value != "") //Hidden Field BANDERA
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    Catalogo ct = new Catalogo();
                    ct.Id_Emp = sesion.Id_Emp; //Si no ocupa empresa su catalogo se pone -1
                    ct.Id_Cd = sesion.Id_Cd_Ver; //Si no ocupa Centro de distribución se pone -1
                    ct.Id = Convert.ToInt32(hiddenId.Value); //Si no ocupa Id se pone -1
                    ct.Tabla = "CatProducto"; //Nombre de la tabla del catalogo
                    ct.Columna = "Id_Colaborador"; //Nombre de la columna del ID del catalogo
                    CN_Comun.Deshabilitar(ct, sesion.Emp_Cnx, ref verificador);
                }
                return verificador;
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
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);

                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];

                else
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

                    if (Permiso.PGrabar == false)
                        this.RadToolBar1.Items[6].Visible = true;

                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                        this.RadToolBar1.Items[5].Visible = false;

                    //Regresar
                    this.RadToolBar1.Items[4].Visible = false;
                    //Eliminar
                    this.RadToolBar1.Items[3].Visible = false;
                    //Imprimir
                    this.RadToolBar1.Items[2].Visible = false;
                    //Correo
                    this.RadToolBar1.Items[1].Visible = false;

                    this.RadToolBar1.Items[6].Visible = false;

                }
                else
                    Response.Redirect("Inicio.aspx");

                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void NuevoProducto()
        {   //rgPrecios.
            this.listSource = this.ConsultarPorductoPrecios(0);
            rgPrecios.DataSource = this.listSource;
            rgPrecios.DataBind();

            this.listConceptosNomina = this.ConsultarConceptosColaborador(0);
            grdConceptoNomina.DataSource = this.listConceptosNomina;
            grdConceptoNomina.DataBind();



            this.hiddenId.Value = string.Empty;

            //Nuevo producto:
            //deshabilta controles de pestaña de compras locales


            txtId_Colaborador.Enabled = true;
            txtId_Colaborador.Focus();
        }

        private void Guardar()
        {
            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatColaborador clsCatColaborador = new CN_CatColaborador();
                int verificador = -1;
                Colaborador colaborador = this.LlenatObjetoColaborador();
                lblTituloColaborador.Text = string.Concat(colaborador.Num_Nomina.ToString(), " - ", colaborador.Nombre_Empleado);

                if (hiddenId.Value == string.Empty)
                {
                    if (!_PermisoGuardar)
                    {
                        DisplayMensajeAlerta("PermisoGuardarNo");
                        return;
                    }

                    clsCatColaborador.InsertarColaborador(colaborador, session.Emp_Cnx, ref verificador);
                    this.LimpiarCampos();
                    this.IdProducto = colaborador.Id_Colaborador;
                    txtId_Colaborador.Text = this.Valor;
                    txtId_Empleado.Text = this.ValorEmpleado;
                    this.DisplayMensajeAlerta("CatColaborador_Insert_Ok");
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }
                    clsCatColaborador.ModificarColaborador(colaborador, session.Emp_Cnx, ref verificador);
                    txtId_Colaborador.Enabled = false;
                    this.LimpiarCampos();
                    cmbColaboradorsLista.Text = "";
                    this.DisplayMensajeAlerta("CatColaborador_Update_Ok");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LimpiarCampos()
        {
            lblTituloColaborador.Text = string.Empty;
            txtId_Colaborador.Text = string.Empty;
            txtId_Empleado.Text = string.Empty;
            chkActivo.Text = "Activo";
            chkColaboradorNuevo.Checked = false;
            txtNum_Nomina.Text = string.Empty;
            txtNombreEmpleado.Text = string.Empty;

            txtIdSucursal.Text = string.Empty;

            txtEmpresa.Text = string.Empty;

            txtPuesto.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            CmbTipoUsuario.SelectedIndex = CmbTipoUsuario.FindItemIndexByValue("-1");


            this.txtFechaInicio.SelectedDate = null;
            cmbFam.SelectedIndex = cmbFam.FindItemIndexByValue("-1");
            if (cmbUEN.CheckedItems.Count() != 0)
            {
                foreach (RadComboBoxItem rlbi in cmbUEN.CheckedItems)
                {
                    rlbi.Checked = false;
                }

            }

            txtContribucion.Text = string.Empty;


            txtSueldoVariable.Text = string.Empty;




            this.listSource = this.ConsultarPorductoPrecios(0);
            rgPrecios.Rebind();
            this.listConceptosNomina = this.ConsultarConceptosColaborador(0);
            grdConceptoNomina.Rebind();

        }

        private void LlenarFormularioProducto(int id_colaborador, int id_Cd_Prod)
        {
            try
            {
                Colaborador colaborador = ConsultarColaboradorEmpleado(id_colaborador, id_Cd_Prod);

                txtId_Colaborador.Text = colaborador.Id_Colaborador.ToString();

                txtId_Empleado.Text = colaborador.Id_Empleado.ToString();
                txtNum_Nomina.Text = colaborador.Num_Nomina == 0 ? string.Empty : colaborador.Num_Nomina.ToString();
                chkActivo.Text = "Inactivo";
                if (colaborador.Colaborador_Estatus == 0)
                {
                    chkActivo.Text = "Activo";
                }

                chkColaboradorNuevo.Checked = false;
                txtNombreEmpleado.Text = colaborador.Nombre_Empleado;
                lblTituloColaborador.Text = string.Concat(txtNum_Nomina.Text, " - ", txtNombreEmpleado.Text);

                if (colaborador.Id_Emp == 1)
                    txtEmpresa.Text = "KEY QUIMICA";
                else
                    txtEmpresa.Text = "CNK";

                //TODO jfcv llenar bien el nombre de la sucursal y que no pueda cambiarse 
                txtIdSucursal.Text = colaborador.Id_Sucursal.ToString();

                txtIdSucursal.Text = "";
                txtIdSucursal.Text = colaborador.Emp_Sucursal;

                txtCorreo.Text = colaborador.Emp_Correo;
                CmbTipoUsuario.Text = "";
                CmbTipoUsuario.ClearSelection();
                if (colaborador.Id_TipoUsuario != 0)
                {
                    txtCorreo.Text = colaborador.Id_TipoUsuario.ToString();
                    CmbTipoUsuario.SelectedValue = colaborador.Id_TipoUsuario.ToString();
                }

                //txtFechaAlta.Text = Convert.ToString(colaborador.Emp_FechaAlta);
                DateTime fechaalt = Convert.ToDateTime(colaborador.Emp_FechaAlta);

                txtFechaAlta.Text = fechaalt.ToString("dd/MM/yyyy");
                this.txtFechaInicio.SelectedDate = colaborador.FechaInicioOperacion;
                cmbFam.Text = "";
                cmbFam.ClearSelection();
                //if (colaborador.Id_Fam != 0)
                //{
                //    txtFam.Text = colaborador.Id_Fam.ToString();
                //    cmbFam.SelectedValue = colaborador.Id_Fam.ToString();
                //}

                txtPuesto.Text = colaborador.Emp_Puesto.ToString();

                #region actualizar las UEN
                char[] delimiterChars = { ',' };
                cmbUEN.ClearSelection();
                ////cmbUEN.Items.Clear();
                if (cmbUEN.CheckedItems.Count() != 0)
                {
                    foreach (RadComboBoxItem rlbi in cmbUEN.CheckedItems)
                    {
                        rlbi.Checked = false;
                    }

                }

                string[] words = colaborador.Id_UEN.Split(delimiterChars);

                if (colaborador.Id_UEN != "0" & colaborador.Id_UEN != "" )
                {
                    foreach (string s in words)
                    {
                        cmbUEN.FindItemByValue(s).Checked = true;

                    }
                }

                #endregion

                //jfcv
                txtSueldoVariable.Text = colaborador.Sueldo_Variable.ToString();

                txtContribucion.Text = colaborador.Porcentaje_Contribucion.ToString();

                //JFCV TODO Consultar los objetivos del Usuario
                this.listSource = this.ConsultarPorductoPrecios(id_colaborador);
                this.listConceptosNomina = this.ConsultarConceptosColaborador(id_colaborador);





                //**********************************//
                //* Consultar Objetivos del Usuario  *//
                //**********************************//
                this.IdProducto = id_colaborador;
                rgPrecios.Enabled = true;
                rgPrecios.Rebind();
                grdConceptoNomina.Enabled = true;
                grdConceptoNomina.Rebind();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Colaborador LlenatObjetoColaborador()
        {
            Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
            Colaborador colaborador = new Colaborador();

            ///JFCV TODO agarrar la empresa de el combo 
            colaborador.Id_Emp = session.Id_Emp;
            //if(cmbEmpresa.SelectedIndex != 0 )
            //    colaborador.Id_Emp = Convert.ToInt32(cmbEmpresa.SelectedValue);
            colaborador.Id_Cd = session.Id_Cd_Ver;
            colaborador.Id_Colaborador = Convert.ToInt32(txtId_Colaborador.Text);
            //colaborador.Id_Emp = 1; //TODO  txtEmpresa.Text == string.Empty ? 0 : Convert.ToInt32(txtEmpresa.Text);
            //colaborador.Id_Sucursal = txtIdSucursal.Text ; //== string.Empty ? 0 : Convert.ToInt32(txtIdSucursal.Text);
            colaborador.Emp_Correo = txtCorreo.Text;
            //JFCV todo colaborador.Id_Fam = txtFam.Value.HasValue ? Convert.ToInt32(txtFam.Text) : 0;
            //JFCV this.txtFechaInicio.SelectedDate = null;
            colaborador.Id_Empleado = Convert.ToInt32(txtId_Empleado.Text);

            colaborador.Nombre_Empleado = txtNombreEmpleado.Text;

            colaborador.Emp_Puesto = txtPuesto.Text;


            colaborador.Porcentaje_Contribucion = txtContribucion.Text == string.Empty ? 0 : Convert.ToDouble(txtContribucion.Text);


            colaborador.Num_Nomina = txtNum_Nomina.Text == string.Empty ? 0 : Convert.ToInt32(txtNum_Nomina.Text);
            colaborador.Sueldo_Variable = txtSueldoVariable.Text == string.Empty ? 0 : Convert.ToSingle(txtSueldoVariable.Text);

            colaborador.Id_UEN = "";
            if (cmbUEN.CheckedItems.Count() != 0)
            {
                foreach (RadComboBoxItem rlbi in cmbUEN.CheckedItems)
                {
                    if (colaborador.Id_UEN == "")
                        colaborador.Id_UEN = rlbi.Value;
                    else
                        colaborador.Id_UEN = colaborador.Id_UEN + "," + rlbi.Value;
                }

            }

            if (chkActivo.Text == "Activo")
            {
                colaborador.Emp_Estatus = 1;
            }
            else
            {
                colaborador.Emp_Estatus = 0;
            }


            colaborador.Emp_FechaUltMod = DateTime.Now;
            colaborador.FechaInicioOperacion = txtFechaInicio.SelectedDate;

            colaborador.ListaColaboradorObjetivos = this.listSource;


            

                 foreach (ConceptosNomina p in this.listConceptosNomina)
                {

                    List<ConceptosNomina> listaPP = new List<ConceptosNomina>(this.listConceptosNomina);
                        //int posicionFila = grdConceptoNomina.CurrentPageIndex * grdConceptoNomina.PageSize + e.Item.ItemIndex;
                        p.Id_Compensacion = p.Id_Compensacion1;
                        p.Monto = p.Monto1;
                        colaborador.ListaConceptosNomina.Add(p);

                        p.Id_Compensacion = p.Id_Compensacion2;
                        p.Monto = p.Monto2;
                        colaborador.ListaConceptosNomina.Add(p);

                        p.Id_Compensacion = p.Id_Compensacion3;
                        p.Monto = p.Monto3;
                        colaborador.ListaConceptosNomina.Add(p);

                        p.Id_Compensacion = p.Id_Compensacion4;
                        p.Monto = p.Monto4;
                        colaborador.ListaConceptosNomina.Add(p);




                        //listaPP[posicionFila].Monto1 = productoPrecio.Monto1;
                        //listaPP[posicionFila].Monto2 = productoPrecio.Monto2;
                        //listaPP[posicionFila].Monto3 = productoPrecio.Monto3;
                        //listaPP[posicionFila].Monto4 = productoPrecio.Monto4;
                        //listaPP[posicionFila].Id_Emp = productoPrecio.Id_Emp;
                        //listaPP[posicionFila].Id_Cd = productoPrecio.Id_Cd;
                        //listaPP[posicionFila].Id_Colaborador = productoPrecio.Id_Colaborador;
                        //listaPP[posicionFila].Id_Compensacion = productoPrecio.Id_Compensacion;
                        //listaPP[posicionFila].Id_Compensacion1 = productoPrecio.Id_Compensacion1;
                        //listaPP[posicionFila].Id_Compensacion2 = productoPrecio.Id_Compensacion2;
                        //listaPP[posicionFila].Id_Compensacion3 = productoPrecio.Id_Compensacion3;
                        //listaPP[posicionFila].Id_Compensacion4 = productoPrecio.Id_Compensacion4;

                        //listaPP[posicionFila].Compensacion_Estatus = productoPrecio.Compensacion_Estatus;
                        //listaPP[posicionFila].Id_Compensacion_Monto = productoPrecio.Id_Compensacion_Monto;
                        //listaPP[posicionFila].Id_Empleado = productoPrecio.Id_Empleado;
                        ////listaPP[posicionFila].Compensacion_Descripcion = productoPrecio.Compensacion_Descripcion;

                        //this.listConceptosNomina = listaPP;


                       


                        break;
                    }

                      



            return colaborador;
        }

        private Colaborador ConsultarColaboradorEmpleado(int id_Colaborador, int id_Cd_Prod)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatColaborador clsCatProducto = new CN_CatColaborador();
                Colaborador colaborador = new Colaborador();
                clsCatProducto.ConsultaEmpleadoNomina(ref colaborador, sesion.Emp_Cnx, sesion.Id_Emp, id_Cd_Prod, sesion.Id_Cd_Ver, id_Colaborador, true);
                return colaborador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<ColaboradorObjetivo> ConsultarPorductoPrecios(int id_Colaborador)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<ColaboradorObjetivo> list = new List<ColaboradorObjetivo>();
                ColaboradorObjetivo objetivos = new ColaboradorObjetivo();
                objetivos.Id_Emp = sesion.Id_Emp;
                objetivos.Id_Cd = sesion.Id_Cd_Ver;
                objetivos.Id_Colaborador = id_Colaborador;

                new CN_CatColaborador().ConsultaListaObjetivos(objetivos, sesion.Emp_Cnx, ref list);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private List<ConceptosNomina> ConsultarConceptosColaborador(int id_Colaborador)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<ConceptosNomina> list = new List<ConceptosNomina>();
                ConceptosNomina objetivos = new ConceptosNomina();
                objetivos.Id_Emp = sesion.Id_Emp;
                objetivos.Id_Cd = sesion.Id_Cd_Ver;
                objetivos.Id_Colaborador = id_Colaborador;

                new CN_CatColaborador().ConsultaListaConceptosNomina(objetivos, sesion.Emp_Cnx, ref list);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                if (Sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;
                    this.TblEncabezado.Visible = false;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void cmbEmpresa_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                if (e.Value != string.Empty && e.Value != "-1")
                {
                    //RadComboBoxItem item = ((RadComboBox)sender).FindItemByValue(e.Value);
                    //int id_Cd_Prod = session.Id_Cd_Ver;
                    int empresa = Convert.ToInt32(e.Value);


                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    CN_Comun.LlenaCombo(0, empresa, session.Id_Cd_Ver, session.Emp_Cnx, "spCatEmpleadoNomina", ref cmbColaboradorsLista);

                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void CargarProductos()//Local
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                int empresa = Sesion.Id_Emp;
                //if (cmbEmpresa.SelectedIndex != 0)
                //    empresa = Convert.ToInt32(cmbEmpresa.SelectedValue);
                CN_Comun.LlenaCombo(0, empresa, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatEmpleadoNomina", ref cmbColaboradorsLista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LlenarComboProductosLista(string filtro)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Producto producto = new Producto();

                List<Producto> listaProducto = new List<Producto>();
                new CN_CatProducto().ConsultaListaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, filtro, ref listaProducto, null);

                producto = new Producto();
                producto.Id_Prd = -1;
                producto.Prd_Descripcion = "-- Seleccionar --";
                listaProducto.Insert(0, producto);
                cmbColaboradorsLista.DataSource = listaProducto;
                cmbColaboradorsLista.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void LlenarComboSucursal()
        //{

        //    try
        //    {

        //        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
        //        CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
        //        //CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatTipoProducto_Combo", ref cmbSucursal);

        //        if (sesion.U_MultiOfi == false)
        //        {
        //            CN_Comun.LlenaCombo(2, sesion.Id_Emp, sesion.Id_U, sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref cmbSucursal);
        //            this.CmbCentro.Visible = false;
        //            this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(sesion.Id_Cd_Ver.ToString()).Text;
        //        }
        //        else
        //        {
        //            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref cmbSucursal);
        //            this.CmbCentro.SelectedValue = sesion.Id_Cd_Ver.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }


        //}





        //private void LlenarComboProductoCategoria()
        //{
        //    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
        //    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
        //    CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatProductoCategoria_Combo", ref cmbCategoria);
        //    this.cmbCategoria.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
        //}

        private void LlenarComboProductoFamilia()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatFamProducto_Combo", ref cmbFam);
            this.cmbFam.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
        }




        private void LlenarComboUnidades(RadComboBox combo)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatUnidad_Combo", ref combo, true);
            combo.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
        }

        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("rgPrecios_FechasRango_incorrecto"))
                    Alerta("Favor de capturar un rango de fechas correcto");
                else
                    if (mensaje.Contains("rgPrecios_FechasRango_PeridoNuevoAnterior"))
                        Alerta("El periodo nuevo no debe ser menor al periodo actual");
                    else
                        if (mensaje.Contains("rgPrecios_FechasRango_DiasEntrePeriodo"))
                            Alerta("Rango de fechas no válido. Hay días entre el periodo anterior y el periodo nuevo. La fecha de inicio del periodo actual debe ser el siguiente día después de la fecha final del periodo anterior");
                        else
                            if (mensaje.Contains("rgPrecios_FechasRango_empalmado"))
                                Alerta("Rango de fechas empalmado.");
                            else
                                if (mensaje.Contains("cmbColaboradorsLista_ItemsDataBound"))
                                    Alerta("Error al llenar la lista de productos, combo cmbProductos");
                                else
                                    if (mensaje.Contains("cmbColaboradorsLista_UpdateCount"))
                                        Alerta("No se pudo actualizar el n&uacute;mero de registros de la lista de productos");
                                    else
                                        if (mensaje.Contains("cmbColaboradorsLista_ItemsRequested"))
                                            Alerta("No se pudo actualizar la lista de productos");
                                        else
                                            if (mensaje.Contains("CatProducto_fechaEmpalmada_error"))
                                                Alerta("Los datos del precio de producto no se guardaron.<br/> Rango de fechas empalmado");
                                            else
                                                if (mensaje.Contains("rgPrecios_ItemDataBound"))
                                                    Alerta("Error al colorear los precions actuales en el grid de precios de producto");
                                                else
                                                    if (mensaje.Contains("rgPrecios_NeedDataSource"))
                                                        Alerta("Error al cargar el Grid de tipos de costos");
                                                    else
                                                        if (mensaje.Contains("rgPrecios_ItemCommand"))
                                                            Alerta("Error al ejecutar un evento (rgPrecios_ItemCommand) al cargar el Grid de tipo de costos");
                                                        else
                                                            if (mensaje.Contains("rgPrecios_Actualizar_ok"))
                                                                Alerta("El precio del producto fue actualizado correctamente");
                                                            else
                                                                if (mensaje.Contains("rgPrecios_Actualizar_error"))
                                                                    Alerta("Error al actualizar el precio del producto");
                                                                else
                                                                    if (mensaje.Contains("Cmb_CentroDistribucion_IndexChanging_error"))
                                                                        Alerta("Error al cambiar de centro de distribución");
                                                                    else
                                                                        if (mensaje.Contains("radGrid_PageIndexChanged"))
                                                                            Alerta("Error al cambiar de página");
                                                                        else
                                                                            if (mensaje.Contains("PermisoGuardarNo"))
                                                                                Alerta("No tiene permisos para grabar");
                                                                            else
                                                                                if (mensaje.Contains("CatProductoIdRepetida_error"))
                                                                                    Alerta("La clave ya existe");
                                                                                else
                                                                                    if (mensaje.Contains("CatProductoDescripcionRepetida_error"))
                                                                                        Alerta("La descripción ya existe");
                                                                                    else
                                                                                        if (mensaje.Contains("PermisoModificarNo"))
                                                                                            Alerta("No tiene permisos para actualizar");
                                                                                        else
                                                                                            if (mensaje.Contains("ProductoBuscarNoExiste"))
                                                                                                Alerta(string.Concat("El producto con clave ", txtId_Colaborador.Text, " no se ha encontrado"));
                                                                                            else
                                                                                                if (mensaje.Contains("CatColaborador_Insert_Ok"))
                                                                                                    Alerta("Los datos se guardaron correctamente");
                                                                                                else
                                                                                                    if (mensaje.Contains("CatColaborador_Insert_error"))
                                                                                                        Alerta("No se pudo guardar los datos del tipo de precio");
                                                                                                    else
                                                                                                        if (mensaje.Contains("CatColaborador_Update_Ok"))
                                                                                                            Alerta("Los datos se modificaron correctamente");
                                                                                                        else
                                                                                                            if (mensaje.Contains("CatColaborador_Update_error"))
                                                                                                                Alerta("No se pudo actualizar los datos del tipo de precio");
                                                                                                            else
                                                                                                                Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }
        #endregion



        #region ErrorManager

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