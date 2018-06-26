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
    public partial class CapEntSalCentral : System.Web.UI.Page
    {
        #region Variables
   
        private Sesion sesion
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
        }
   
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        private List<EntradasSalidasCentralDet> _List_EsDet;
        public List<EntradasSalidasCentralDet> List_EsDet
        {
            get { return _List_EsDet; }
            set { _List_EsDet = value; }
        }
        public List<EntradasSalidasCentralDet> List_Es
        {
            get
            {
                return (Session["DetallesMovsCentral" + Session.SessionID] as List<EntradasSalidasCentralDet>);
            }
            set
            {
                Session["DetallesMovsCentral" + Session.SessionID] = value;
            }
 
        }
        double TotalFac = 0;
        double TotalEst = 0;

        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (sesion == null)
                {
                    CerrarVentana();

                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        Inicializar();

                   
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void RAM1_AjaxRequest1(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "panel":
                        //Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 180);
                        //RadPageViewDetalles.Height = altura;
                        //RadPane1.Height = altura;
                        //RadPane1.Width = RadPageViewDGenerales.Width;
                        //RadSplitter1.Height = altura;
                        //RadPageViewDGenerales.Height = altura;
                        //RadSplitter2.Height = altura;
                        //RadPane2.Height = altura;
                        //RadPane2.Width = RadPageViewDGenerales.Width;
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
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;
                switch (btn.CommandName)
                {
                    case "save":
                        Guardar();
                        break;
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        //protected void dpFecha_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e) { }

        protected void rg_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    this.rgMovimentosDet.DataSource = List_Es;
                }
             
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg_ItemDataBound(object sender, GridItemEventArgs e)
        {
            ErrorManager();
            if (e.Item is GridEditableItem && !e.Item.IsInEditMode)
            {
                GridDataItem item = e.Item as GridDataItem;
                Label txtfac = (Label)item.FindControl("LblTotalFac");
                TotalFac = TotalFac + double.Parse(txtfac.Text);
                Label txtest = (Label)item.FindControl("LblTotalEst");
                TotalEst = TotalEst + double.Parse(txtest.Text);
                double fac = 0;
                double est = 0;
                fac = double.Parse(txtfac.Text);
                est = double.Parse(txtest.Text);

                Label txtvar = (Label)item.FindControl("LblVariacion");
                if (fac > est)
                {
                    txtvar.ForeColor = System.Drawing.Color.Red;
                }
                else if (fac < est)
                {
                    txtvar.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    txtvar.ForeColor = System.Drawing.Color.Black;
                }



                this.TxtTotal.Text = TotalFac.ToString("N2");
                this.TxtAcumRecepciones.Text = TotalEst.ToString("N2");

                if (TotalFac > TotalEst)
                {
                    this.TxtVariacion.Text = (TotalFac - TotalEst).ToString("N2");
                    this.TxtVariacion.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    this.TxtVariacion.Text = (TotalEst - TotalFac).ToString("N2");
                    this.TxtVariacion.ForeColor = System.Drawing.Color.Black;
                }
                
            }

            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridDataItem item = e.Item as GridDataItem;

                RadNumericTextBox  txtfac = (RadNumericTextBox )item.FindControl("TxtTotalFac");
                TotalFac = TotalFac + double.Parse(txtfac.Text);
                RadNumericTextBox  txtest = (RadNumericTextBox)item.FindControl("TxtTotalEst");
                TotalEst = TotalEst + double.Parse(txtest.Text);


                double fac = 0;
                double est = 0;
                fac = double.Parse(txtfac.Text);
                est = double.Parse(txtest.Text);

                RadNumericTextBox txtvar =(RadNumericTextBox) item.FindControl ("TxtVariación");
                if (fac > est)
                {
                    txtvar.ForeColor = System.Drawing.Color.Red;
                }
                else if (fac < est)
                {
                    txtvar.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    txtvar.ForeColor = System.Drawing.Color.Black;
                }
                
                this.TxtTotal.Text = TotalFac.ToString("N2");
                this.TxtAcumRecepciones.Text = TotalEst.ToString("N2");

                if (TotalFac > TotalEst)
                {
                    this.TxtVariacion.Text = (TotalFac - TotalEst).ToString("N2");
                    this.TxtVariacion.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    this.TxtVariacion.Text = (TotalEst - TotalFac).ToString("N2");
                    this.TxtVariacion.ForeColor = System.Drawing.Color.Black;
                }
            }
        }
        protected void rg_ItemCommand(object source, GridCommandEventArgs e)
        {
            //try
            //{
            //    ErrorManager();
            //    string estatus_registro = "0";
            //    int cantidad_registro = 0;
            //    if (e.CommandName == "InitInsert")
            //    {
            //        estatus_registro = "2";
            //    }
            //    else if (e.CommandName == "Edit")
            //    {
            //        int cantidad_A = int.Parse((rgEntradaSalida.MasterTableView.Items[e.Item.ItemIndex]["cantidad"].FindControl("CantidadLabel") as Label).Text);
            //        estatus_registro = "1"; //1=Edit
            //        cantidad_registro = cantidad_A;
            //    }
            //    else
            //    {
            //        Session["CantidadEdicion" + Session.SessionID + HF_ClvPag.Value] = int.Parse((rgEntradaSalida.MasterTableView.Items[e.Item.ItemIndex]["cantidad"].FindControl("CantidadLabel") as Label).Text);
            //        int prd_ = int.Parse((rgEntradaSalida.MasterTableView.Items[e.Item.ItemIndex]["Id_Prd"].FindControl("ProdLabel") as Label).Text);
            //        int ter_ = int.Parse((rgEntradaSalida.MasterTableView.Items[e.Item.ItemIndex]["Id_Ter"].FindControl("TerLabel") as Label).Text);
            //        int can_ = 0;
            //        string nat_ = cmbNaturaleza.SelectedValue;
            //        string ref_ = txtReferencia.Text;
            //        string cte_ = txtClienteId.Text;
            //        string es_ = txtFolio.Text;
            //        string mov_ = txtTipoId.Text;
            //        int _gpo;
            //        switch (Convert.ToInt32(txtTipoId.Text))
            //        {
            //            case 2:
            //            case 4:
            //                _gpo = 1;
            //                break;
            //            case 6:
            //            case 15:
            //            case 16:
            //                _gpo = 2;
            //                break;
            //            case 7:
            //            case 11:
            //            case 12:
            //            case 13:
            //                _gpo = 3;
            //                break;
            //            case 14:
            //                _gpo = 4;
            //                break;
            //            default:
            //                _gpo = 0;
            //                break;
            //        }
            //        string valor_retorno = "";
            //        Producto producto = new Producto();

            //        valor_retorno = Producto_Cantidad(sesion, valor_retorno.ToString(), nat_ == "" ? "-1" : nat_, _gpo.ToString() == "" ? "-1" : _gpo.ToString(), Convert.ToInt32(prd_), ref_ == "" ? "-1" : ref_, Convert.ToInt32(es_), Convert.ToInt32(ter_), can_, mov_ == "" ? "-1" : mov_, cte_ == "" ? "-1" : cte_, producto);
            //        string[] valores = valor_retorno.Split(new string[] { "@@" }, StringSplitOptions.None);
            //        if (valores.Length > 1)
            //        {
            //            Alerta(valores[1]);
            //            e.Canceled = true;
            //            return;
            //        }


            //        estatus_registro = "3";
            //    }
            //    Session["estatus" + Session.SessionID + HF_ClvPag.Value] = estatus_registro;
            //    Session["CantidadEdicion" + Session.SessionID + HF_ClvPag.Value] = cantidad_registro;
            //}
            //catch (Exception ex)
            //{
            //    ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            //}
        }
        protected void rg_InsertCommand(object source, GridCommandEventArgs e)
        {
            //try
            //{
            //    ErrorManager();
            //    GridEditableItem editedItem = e.Item as GridEditableItem;
            //    EntradaSalidaDetalle Es_Det = new EntradaSalidaDetalle();
            //    Es_Det.Id_EsDetStr = Guid.NewGuid().ToString();
            //    if ((editedItem.FindControl("cmbTerritorio") as RadComboBox).SelectedIndex < 1 && rgEntradaSalida.Columns[rgEntradaSalida.Columns.FindByUniqueName("territorio").OrderIndex - 2].Display)
            //    {
            //        Alerta("No se ha seleccionado un territorio");
            //        e.Canceled = true; return;
            //    }

            //    GenerarDetalle(editedItem, ref Es_Det);

            //    if (list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_Prd == Es_Det.Id_Prd).ToList().Count > 0)
            //    {
            //        Alerta("El producto ya fue capturado");
            //        e.Canceled = true; return;
            //    }

            //    list_Es.Add(Es_Det);
            //    rgEntradaSalida.Rebind();
            //    CalcularTotales();
            //}
            //catch (Exception ex)
            //{
            //    ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            //}
        }
        protected void rg_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {

           
                ErrorManager();
                GridEditableItem Item = (GridEditableItem)e.Item;
                EntradasSalidasCentralDet es = new EntradasSalidasCentralDet();
      


                es.Id_Emp = Convert.ToInt32(Item.OwnerTableView.DataKeyValues[Item.ItemIndex]["Id_Emp"]);
                es.Id_Alm = Convert.ToInt32(Item.OwnerTableView.DataKeyValues[Item.ItemIndex]["Id_Alm"]);
                es.Id_MovC = Convert.ToInt32(Item.OwnerTableView.DataKeyValues[Item.ItemIndex]["Id_MovC"]);
                es.Id_MovCDet = Convert.ToInt32(Item.OwnerTableView.DataKeyValues[Item.ItemIndex]["Id_MovCDet"]);
                es.Id_Tm = Convert.ToInt32(Item.OwnerTableView.DataKeyValues[Item.ItemIndex]["Id_Tm"]);
                es.Id_Prd = Convert.ToInt32((Item["Id_Prd"].FindControl("TxtId_Prd") as RadNumericTextBox).Value);
                es.Prd_Descripcion = (Item["Prd_Descripcion"].FindControl("TxtPrd_Descripcion") as RadTextBox).Text;
                es.Prd_Presentacion = (Item["Prd_Presentacion"].FindControl("TxtPrd_Presentacion") as RadTextBox).Text;
                es.MovC_Cant = Convert.ToInt32((Item["MovC_Cant"].FindControl("TxtMovC_Cant") as RadNumericTextBox).Value);
                es.MovC_CostoEst = Convert.ToDouble((Item["CostoEst"].FindControl("TxtCostoEst") as RadNumericTextBox).Value);
                es.MovC_CostoFac = Convert.ToDouble((Item["CostoFac"].FindControl("TxtCostoFac") as RadNumericTextBox).Value);
                es.TotalEst = es.MovC_Cant * es.MovC_CostoEst;
                es.TotalFac = es.MovC_Cant * es.MovC_CostoFac;
           

                if (es.TotalFac > es.TotalEst)
                {
                    es.Variacion = es.TotalFac - es.TotalEst;
                }
                else if (es.TotalFac < es.TotalEst)
                {
                    es.Variacion = es.TotalEst - es.TotalFac;
                }
                else
                {
                    es.Variacion = 0;
                }
                DataRow[] dr;

                CN__Comun cn_c = new CN__Comun();

                DataTable dtprods = new DataTable();

                dtprods = CN__Comun.Convertidor<EntradasSalidasCentralDet>.ListaToDatatable(List_Es);

                dr = dtprods.Select("Id_MovC='" + es.Id_MovC    + "' and Id_Prd='" + es.Id_Prd + "'");
                if (dr.Length > 0)
                {
                    dr[0].BeginEdit();
             
                    dr[0]["MovC_CostoEst"] = es.MovC_CostoEst;
                    dr[0]["MovC_CostoFac"] = es.MovC_CostoFac;
                    dr[0]["TotalFac"] = es.TotalFac;
                    dr[0]["TotalEst"] = es.TotalEst;
                    dr[0]["Variacion"] = es.Variacion;
                    dr[0].AcceptChanges();
                }

                List<EntradasSalidasCentralDet> Lst = new List<EntradasSalidasCentralDet>();

                Lst = CN__Comun.Convertidor<EntradasSalidasCentralDet>.DataTableToLista(dtprods);

                List_Es = Lst;

                //CalcularTotales();
                //FormatoGrid();
     
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg_DeleteCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                //GridEditableItem editedItem = e.Item as GridEditableItem;
                //EntradaSalidaDetalle Es_Det = new EntradaSalidaDetalle();
                //Es_Det.Id_EsDetStr = (editedItem["Id_EsDetStr"].FindControl("lblDet_Item") as Label).Text;
                //list_Es.Remove(list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_EsDetStr == Es_Det.Id_EsDetStr).ToList()[0]);

                //rgEntradaSalida.Rebind();
                //CalcularTotales();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = e.Item as GridEditableItem;

            }
        }
        protected void TxtTotalFac_TextChanged(object sender, EventArgs e)
        {
            try
            {
                RadNumericTextBox txt = (sender) as RadNumericTextBox;
                string val = txt.Text;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        #endregion
        #region Funciones
        private void Inicializar()
        {
            try
            {
                Random randObj = new Random(DateTime.Now.Millisecond);
                HF_ClvPag.Value = randObj.Next().ToString();
                int Id_Tm = int.Parse(Request.QueryString["Id_Tm"]);
                if (Id_Tm != 2)
                {
                    this.rgMovimentosDet.Columns[11].Visible = false;
                    this.rgMovimentosDet.Columns[13].Visible = false;
                    this.rgMovimentosDet.Columns[14].Visible = false;
                    this.rgMovimentosDet.Columns[10].HeaderText = "Costo";
                    this.rgMovimentosDet.Columns[12].HeaderText = "Total";
                    trTotal.Visible = false;
                    trVariacion.Visible = false;
                    this.LblAcumRecepciones.Text = "Total";

                }
                ValidarPermisos();
                CargarEncabezado();
                CargarDetalles();
                this.rgMovimentosDet.Rebind();


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private void CargarEncabezado()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapEntradasSalidasCentral cn_es = new CN_CapEntradasSalidasCentral();
                EntradasSalidasCentral es = new EntradasSalidasCentral();

                int Id_Emp = int.Parse(Request.QueryString["Id_Emp"]);
                int Id_Alm = int.Parse(Request.QueryString["Id_Alm"]);
                int Id_MovC = int.Parse(Request.QueryString["Id_MovC"]);
                int Id_Tm = int.Parse(Request.QueryString["Id_Tm"]);
                bool MovC_Naturaleza = bool.Parse(Request.QueryString["Nat"]);

                cn_es.ConsultaEncabezado(Id_Emp, Id_Alm, Id_MovC, Id_Tm, MovC_Naturaleza, ref es, sesion);

                if (MovC_Naturaleza == false)
                {
                    this.TxtId_MovC.Text = "E-" + es.Id_MovC.ToString();
                }
                else 
                {
                    this.TxtId_MovC.Text = "S-" + es.Id_MovC.ToString();
                }
                this.txtMovC_Fecha.Text  = es.MovC_Fecha.ToShortDateString();
                this.TxtAlmacen.Text = es.Alm;
                this.TxtTipoMov.Text = es.TipoMov;
                this.TxtMovC_Referencia.Text = es.MovC_Referencia;
                this.TxtAplContable.Text = es.AplContable;
                this.TxtRemitente.Text  = es.Remitente;
                this.TxtDestinatario.Text  = es.Destino;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void CargarDetalles()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapEntradasSalidasCentral cn_es = new CN_CapEntradasSalidasCentral();
                List<EntradasSalidasCentralDet> List = new List<EntradasSalidasCentralDet>();

                int Id_Emp = int.Parse(Request.QueryString["Id_Emp"]);
                int Id_Alm = int.Parse(Request.QueryString["Id_Alm"]);
                int Id_MovC = int.Parse(Request.QueryString["Id_MovC"]);
                int Id_Tm = int.Parse(Request.QueryString["Id_Tm"]);
                bool MovC_Naturaleza = bool.Parse(Request.QueryString["Nat"]);

                cn_es.ConsultaDetalle(Id_Emp, Id_Alm, Id_MovC, Id_Tm, MovC_Naturaleza, ref List, sesion);

                this.List_Es = List;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void cargarMovimientoEntSal()
        {
            ////aqui se va traer la info del documento a editar             
            EntradaSalida entradaSalida = new EntradaSalida();
            try
            {
            //    int Id_Es = Convert.ToInt32(Request.QueryString["id"]);
            //    int Es_Naturaleza = Convert.ToInt32(Request.QueryString["Es_Naturaleza"]);

            //    CN_CapEntradaSalida cn_entsal = new CN_CapEntradaSalida();
            //    cn_entsal.ConsultarEntradaSalida(sesion, sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Es, Es_Naturaleza, ref entradaSalida);
            //    cmbNaturaleza.SelectedValue = entradaSalida.Es_Naturaleza.ToString();
            //    CargarTipoMovimiento(entradaSalida.Es_Naturaleza);

            //    txtFolio.Text = entradaSalida.Id_Es.ToString();
            //    dpFecha.SelectedDate = entradaSalida.Es_Fecha;
            //    txtTipoId.Text = entradaSalida.Id_Tm.ToString();
            //    cmbTipoMovimento.SelectedIndex = cmbTipoMovimento.FindItemIndexByValue(entradaSalida.Id_Tm.ToString());
            //    cmbTipoMovimento.Text = cmbTipoMovimento.FindItemByValue(entradaSalida.Id_Tm.ToString()).Text;
            //    cmbTipoMovimento_SelectedIndexChanged(cmbTipoMovimento, null);
            //    txtClienteId.DbValue = entradaSalida.Id_Cte == -1 ? (int?)null : entradaSalida.Id_Cte;
            //    txtClienteNombre.Text = entradaSalida.Cte_NomComercial;
            //    HiddenCteCuentaNacional.Value = entradaSalida.Es_CteCuentaNacional.ToString();
            //    HiddenNumCuentaContNacional.Value = entradaSalida.Es_CteCuentaContNacional.ToString();
            //    if (entradaSalida.Id_Tm == 26)
            //    {
            //        this.CmbProveedorF.SelectedIndex = this.CmbProveedorF.FindItemIndexByValue(entradaSalida.Id_Pvd.ToString());
            //        this.CmbProveedorF.Text = this.CmbProveedorF.FindItemByValue(entradaSalida.Id_Pvd.ToString()).Text;
            //        this.txtProveedorFId.DbValue = entradaSalida.Id_Pvd == -1 ? (int?)null : entradaSalida.Id_Pvd;
            //    }
            //    else 
            //    {
            //        cmbProveedor.SelectedIndex = cmbProveedor.FindItemIndexByValue(entradaSalida.Id_Pvd.ToString());
            //        cmbProveedor.Text = cmbProveedor.FindItemByValue(entradaSalida.Id_Pvd.ToString()).Text;
            //        txtProveedorId.DbValue = entradaSalida.Id_Pvd == -1 ? (int?)null : entradaSalida.Id_Pvd;
            //    }
              
               
            //    txtReferencia.Text = entradaSalida.Es_Referencia;
            //    txtNotas.Text = entradaSalida.Es_Notas;
            //    txtterritorio.DbValue = entradaSalida.Id_Ter;
            //    txtTerritorioNombre.Text = entradaSalida.Ter_Nombre;
            //    List<EntradaSalidaDetalle> detalles = new List<EntradaSalidaDetalle>();
            //    ////DataTable dt = new DataTable();
            //    new CN_CapEntradaSalida().ConsultarEntradaSalidaDetalles(sesion, entradaSalida, ref detalles);//, ref dt);
            //    list_Es = detalles;
            //    rgEntradaSalida.DataSource = list_Es;
            //    rgEntradaSalida.Rebind();

            //    CalcularTotales();

            //    habilitarDeshabilitar();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();

      //JMM: Le pongo la url fija para que tome los permisos de la lista
                pagina.Url = "CapEntradasSalidasCentral_Lista.aspx";
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

                    if (_PermisoGuardar == false)
                    {
                        this.rtb1.Items[1].Visible = false;
 
                    }

                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
        private void Guardar()
        {
            try
            {

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapEntradasSalidasCentral cn_es = new CN_CapEntradasSalidasCentral();
                int Verificador = 0;

                cn_es.ModificarDetalle(List_Es, sesion, ref Verificador);

                if (Verificador == -1)
                {
                    AlertaCerrar("Los datos se guardaron correctamente");
                }
                else
                {
                    Alerta("Ha ocurrido un error al guardar la infromación");
                }

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        //private void CalcularTotales()
        //{
        //    try
        //    {
        //         double TotalFac = 0;
        //         double TotalEst = 0;
        //        foreach (GridDataItem item in rgMovimentosDet.MasterTableView.Items)
        //        {

        //            Label txtfac = (Label)item.FindControl("LblTotalFac");
        //            TotalFac = TotalFac + double.Parse(txtfac.Text);

        //            Label txtest = (Label)item.FindControl("LblTotalEst");

        //            TotalEst = TotalEst + double.Parse(txtest.Text);
             

        //        }

        //        this.TxtTotal.Text = TotalFac.ToString("N2");
        //        this.TxtAcumRecepciones.Text = TotalEst.ToString("N2");

        //        if (TotalFac > TotalEst)
        //        {
        //            this.TxtVariacion.Text = (TotalFac - TotalEst).ToString("N2");
        //            this.TxtVariacion.ForeColor = System.Drawing.Color.Red;
        //        }
        //        else
        //        {
        //            this.TxtVariacion.Text = (TotalEst - TotalFac).ToString("N2");
        //            this.TxtVariacion.ForeColor = System.Drawing.Color.Black;
        //        }

             


        //    }
        //    catch (Exception ex)
        //    {
                
        //        throw ex;
        //    }
        //}
        //private void FormatoGrid()
        //{
        //    try
        //    {
        //        double TotalFac = 0;
        //        double TotalEst = 0;
        //        foreach (GridDataItem item in rgMovimentosDet.MasterTableView.Items)
        //        {

        //            Label txtfac = (Label)item.FindControl("LblTotalFac");
        //            TotalFac =  double.Parse(txtfac.Text);

        //            Label txtest = (Label)item.FindControl("LblTotalEst");
        //            Label variacion = (Label)item.FindControl("LblVariacion");

        //            TotalEst =  double.Parse(txtest.Text);

        //            if (TotalFac > TotalEst)
        //            {
        //                variacion.Text = (TotalFac - TotalEst).ToString("N2");
        //                variacion.ForeColor = System.Drawing.Color.Red;
        //            }
        //            else
        //            {
        //                variacion.Text = (TotalEst - TotalFac).ToString("N2");
        //                variacion.ForeColor = System.Drawing.Color.Black;
        //            }


        //        }

        //    }
        //    catch (Exception ex)
        //    {
                
        //        throw ex;
        //    }
        //}
        //private void Guardar()
        //{
        //    try
        //    {


        //        if (Request.QueryString["id"] != "-1" && !_PermisoModificar) // EDICION
        //        {
        //            Alerta("No tiene permisos para modificar");
        //            return;
        //        }

        //        if (Request.QueryString["id"] == "-1" && !_PermisoGuardar) //NUEVO
        //        {
        //            Alerta("No tiene permisos para grabar");
        //            return;
        //        }



        //        if (list_Es.Count == 0)
        //        {
        //            RadTabStrip1.Tabs[1].Selected = true;
        //            RadPageViewDetalles.Selected = true;
        //            Alerta("Aún no se han capturado partidas");
        //            return;
        //        }
        //        else
        //        {
        //            RadTabStrip1.Tabs[0].Selected = true;
        //            RadPageViewDGenerales.Selected = true;
        //        }

        //        RadTabStrip1.Enabled = false;
        //        RadMultiPage1.Enabled = false;
        //        CN_CapEntradaSalida cn_capEntradaSalida = new CN_CapEntradaSalida();

        //        EntradaSalida entsal = new EntradaSalida();
        //        entsal.Id_Emp = sesion.Id_Emp;
        //        entsal.Id_Cd = sesion.Id_Cd_Ver;
        //        entsal.Id_U = sesion.Id_U;
        //        entsal.Id_Es = int.Parse(txtFolio.Text);
        //        entsal.Es_Naturaleza = int.Parse(cmbNaturaleza.SelectedValue);
        //        entsal.Es_Fecha = Convert.ToDateTime(dpFecha.SelectedDate);
        //        entsal.Id_Tm = int.Parse(cmbTipoMovimento.SelectedValue);
        //        entsal.Id_Cte = Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1);
               

        //        // De acuerdo al tipo de mov se toma de un combo u otro el valor
        //        if (Convert.ToInt32(this.txtTipoId.Text) == 26)
        //        {
        //            entsal.Id_Pvd = int.Parse(this.CmbProveedorF.SelectedValue);
        //        }
        //        else
        //        {
        //            entsal.Id_Pvd = int.Parse(cmbProveedor.SelectedValue);
        //        }
               
        //        entsal.Es_Referencia = txtReferencia.Text;
        //        entsal.Es_Notas = txtNotas.Text;
        //        entsal.Es_SubTotal = RadNumericTextBoxSubTotal.Value.Value;
        //        entsal.Es_Iva = RadNumericTextBoxIVA.Value.Value;
        //        entsal.Es_Total = RadNumericTextBoxTotal.Value.Value;
        //        entsal.Es_Estatus = "C";
        //        entsal.Id_Ter = txtterritorio.Value.HasValue ? (int)txtterritorio.Value.Value : -1;
        //        entsal.Es_CteCuentaNacional = string.IsNullOrEmpty(HiddenCteCuentaNacional.Value) ? -1 : Convert.ToInt32(HiddenCteCuentaNacional.Value);
        //        entsal.Es_CteCuentaContNacional = string.IsNullOrEmpty(HiddenNumCuentaContNacional.Value) ? 0 : Convert.ToInt32(HiddenNumCuentaContNacional.Value);
        //        List<EntradaSalidaDetalle> listaDetalle = list_Es;

        //        string verificadorStr = "";
        //        if (Request.QueryString["id"] == "-1" || Request.QueryString["id"] == null)
        //        {
        //            cn_capEntradaSalida.GuardarEntradaSalida(entsal, listaDetalle, ref verificadorStr, strEmp, sesion.Emp_Cnx);
        //        }
        //        else
        //        {
        //            cn_capEntradaSalida.EdicionEntradaSalida(entsal, listaDetalle, ref verificadorStr, strEmp, sesion.Emp_Cnx);
        //        }

        //        this.rtb1.Items[5].Enabled = false;
        //        if (verificadorStr.Trim() != "")
        //        {
        //            verificadorStr = verificadorStr + "<br";
        //        }

        //        AlertaCerrar(verificadorStr + "Los datos se guardaron correctamente");

        //    }
        //    catch (Exception ex)
        //    {
        //        this.rtb1.Items[5].Enabled = true;
        //        RadTabStrip1.Enabled = true;
        //        RadMultiPage1.Enabled = true;
        //        Alerta(ex.Message);
        //    }
        //}
        //private void Nuevo()
        //{
        //    try
        //    {
        //        RadTabStrip1.Tabs[0].Selected = true;
        //        RadPageViewDGenerales.Selected = true;
        //        txtTipoId.Text = "";
        //        LimpiarCombo(cmbNaturaleza);
        //        LimpiarCombo(cmbTipoMovimento);
        //        txtFolio.Text = "";
        //        cmbTipoMovimento_SelectedIndexChanged(cmbTipoMovimento, null);
        //        LimpiarClienteProducto();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //private void LimpiarCombo(RadComboBox rcb)
        //{
        //    try
        //    {
        //        rcb.SelectedIndex = 0;
        //        rcb.Text = rcb.Items[0].Text;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //private void LimpiarClienteProducto()
        //{
        //    try
        //    {
        //        txtClienteId.Text = "";
        //        txtClienteNombre.Text = "";
        //        cmbProveedor.SelectedIndex = 0;
        //        cmbProveedor.Text = cmbProveedor.Items[0].Text;
        //        CmbProveedorF.SelectedIndex = 0;
        //        CmbProveedorF.Text = cmbProveedor.Items[0].Text;

        //        txtProveedorId.Text = "";
        //        this.txtProveedorFId.Text = "";
        //        txtReferencia.Text = "";
        //        txtNotas.Text = "";


        //        LabelTerritorio.Visible = false;
        //        txtTerritorioNombre.Visible = false;
        //        txtterritorio.Text = "";
        //        txtTerritorioNombre.Text = "";

        //        list_Es = new List<EntradaSalidaDetalle>();
        //        rgEntradaSalida.Rebind();

        //        CalcularTotales();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //private void CargarTipoMovimiento(int tipo_movimiento) //Central
        //{
        //    try
        //    {
        //        CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
        //        CN_Comun.LlenaCombo(sesion.Id_Emp, tipo_movimiento, sesion.Emp_Cnx, "spCatMovimientoFiltro_Combo", ref cmbTipoMovimento);
        //        if (cmbNaturaleza.SelectedValue == "0")
        //        {
        //            RemoverItem(new int[] { 18, 51, 78, 81, 82 });
        //        }
        //        else
        //        {
        //            RemoverItem(new int[] { 17, 51, 53, 54, 60, 62, 63, 64, 65, 70, 72, 73, 74, 75 });
        //        }

        //        cmbTipoMovimento.Enabled = !(tipo_movimiento == -1);
        //        txtTipoId.Enabled = !(tipo_movimiento == -1);
        //        cmbTipoMovimento.Text = cmbTipoMovimento.Items[0].Text;
        //        cmbTipoMovimento.SelectedIndex = 0;
        //        txtTipoId.DbValue = cmbTipoMovimento.Items[0].Value == "-1" ? null : cmbTipoMovimento.Items[0].Value;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //private void RemoverItem(int[] NoVisibles)
        //{
        //    foreach (int tm in NoVisibles)
        //    {
        //        RadComboBoxItem bi = cmbTipoMovimento.FindItemByValue(tm.ToString());
        //        if (bi != null)
        //            cmbTipoMovimento.Items.Remove(bi);
        //    }
        //}
        //private int consultarConsecutivo(int Naturaleza_movimiento)
        //{
        //    try
        //    {
        //        CN_CapEntradaSalida cn_entradasal = new CN_CapEntradaSalida();

        //        int naturalela = Convert.ToInt32(cmbNaturaleza.SelectedValue);
        //        int consecutivo = 0;
        //        cn_entradasal.ConsultarConsecutivo(sesion, naturalela, ref consecutivo);
        //        return consecutivo;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}
        //private List<Movimientos> ObtenerMovimiento(string TipoMovimento)
        //{
        //    try
        //    {
        //        List<Movimientos> List = new List<Movimientos>();
        //        CN_CatMovimientos clsCatMovimientos = new CN_CatMovimientos();

        //        clsCatMovimientos.ConsultaMovimientos(false, sesion.Id_Emp, sesion.Emp_Cnx, ref List);
        //        return List.Where(Movimientos => Movimientos.Id.ToString() == TipoMovimento).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



        //private EntradaSalidaDetalle GenerarDetalle(GridEditableItem editedItem, ref EntradaSalidaDetalle Es_Det)
        //{
        //    try
        //    {


        //        Es_Det.Id_Emp = sesion.Id_Emp;
        //        Es_Det.Id_Cd = sesion.Id_Cd_Ver;
        //        Es_Det.Id_Ter = (editedItem.FindControl("txtTerritorio") as RadNumericTextBox).Value.HasValue ? (int)(editedItem.FindControl("txtTerritorio") as RadNumericTextBox).Value : 0;
        //        Es_Det.Ter_Nombre = (editedItem.FindControl("cmbTerritorio") as RadComboBox).Text;
        //        Es_Det.Id_Prd = (int)(editedItem.FindControl("txtId_Prd") as RadNumericTextBox).Value;
        //        Es_Det.Prd_Descripcion = (editedItem.FindControl("DescripcionTextBox") as RadTextBox).Text;
        //        Es_Det.Presentacion = (editedItem.FindControl("PresenTextBox") as RadTextBox).Text;
        //        Es_Det.Es_Cantidad = (int)(editedItem.FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Value;
        //        Es_Det.Es_Costo = (double)((editedItem.FindControl("RadNumericTextBoxCosto") as RadNumericTextBox).Value);
        //        Es_Det.Importe = Es_Det.Es_Cantidad * Es_Det.Es_Costo;
        //        Es_Det.Afecta = (editedItem["afecta"].Controls[0] as CheckBox).Checked;
        //        Es_Det.Es_BuenEstado = (editedItem["buenEstado"].Controls[0] as CheckBox).Checked;
        //        Es_Det.Prd_AgrupadoSpo = (int)(editedItem.FindControl("AgrupadorTextBox") as RadNumericTextBox).Value;
        //        return Es_Det;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}
        //private string Producto_Cantidad(Sesion sesion, string valor_retorno, string nat_, string gpo_, int id_prd, string ref_, int es_, int ter_, int can_, string mov_, string cte_, Producto producto)
        //{
        //    try
        //    {
        //        if (nat_ == "1")
        //        {
        //            int cantidadB = 0;
        //            foreach (EntradaSalidaDetalle dr in list_Es)
        //            {
        //                if (dr.Id_Prd.ToString() == id_prd.ToString())
        //                {
        //                    cantidadB = cantidadB + Convert.ToInt32(dr.Es_Cantidad);
        //                }
        //            }
        //            if (Session["estatus" + Session.SessionID + HF_ClvPag.Value].ToString() == "1")
        //            {
        //                cantidadB = cantidadB - Convert.ToInt32(Session["CantidadEdicion" + Session.SessionID + HF_ClvPag.Value]);
        //            }

        //            CN_CapRemision rem = new CN_CapRemision();
        //            int cantidadES2 = 0;
        //            if (actualizacionDocumento)
        //            {
        //                rem.ConsultarRemisionesCantidadRemCantidad(sesion.Id_Emp, sesion.Id_Cd_Ver, es_, id_prd, nat_, ref cantidadES2, sesion.Emp_Cnx);
        //            }


        //            if (producto.Prd_InvFinal - producto.Prd_Asignado + cantidadES2 < can_ + cantidadB)
        //            {
        //                return "-1@@" + "No hay producto suficiente";
        //            }


        //        }
        //        else if (gpo_ == "0")
        //        {
        //            int edicion = Convert.ToInt32(Session["CantidadEdicion" + Session.SessionID + HF_ClvPag.Value]);
        //            if (producto.Prd_InvFinal - producto.Prd_Asignado - (edicion - can_) < 0)
        //            {
        //                return "-1@@" + "Producto " + producto.Id_Prd.ToString() + " inventario disponible insuficiente, inventario final: " + producto.Prd_InvFinal.ToString() + ", asignado: " + producto.Prd_Asignado.ToString() + ", disponible:" + (producto.Prd_InvFinal - producto.Prd_Asignado).ToString();
        //            }
        //        }

        //        if (gpo_ == "4" || gpo_ == "2")
        //        {

        //            CN_CapEntradaSalida CNentrada = new CN_CapEntradaSalida();
        //            int verificador = 0;
        //            CNentrada.ConsultarSaldo(sesion.Id_Emp, sesion.Id_Cd_Ver, id_prd.ToString(), ter_.ToString(), cte_, sesion.Emp_Cnx, ref verificador, mov_);
        //            int Prd_AgrupadoSpo = producto.Prd_AgrupadoSpo;

        //            int cantidadEnDt = 0;
        //            foreach (EntradaSalidaDetalle dr in list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Prd_AgrupadoSpo == Prd_AgrupadoSpo && EntradaSalidaDetalle.Id_Ter == ter_ && EntradaSalidaDetalle.Id_Prd != id_prd).ToList())
        //            {
        //                cantidadEnDt += dr.Es_Cantidad;
        //            }

        //            CN_CapRemision rem = new CN_CapRemision();
        //            int cantidadES2 = 0;
        //            if (actualizacionDocumento)
        //            {
        //                rem.ConsultarRemisionesCantidadRemCantidad(sesion.Id_Emp, sesion.Id_Cd_Ver, es_, Prd_AgrupadoSpo, nat_, ref cantidadES2, sesion.Emp_Cnx);
        //                verificador += cantidadES2;
        //            }


        //            if (cantidadEnDt + can_ > verificador)
        //            {
        //                return "-1@@" + "Los artículos sobrepasan lo disponible";
        //            }

        //        }
        //        else if (gpo_ == "3")
        //        {
        //            CN_CapRemision rem = new CN_CapRemision();


        //            int cantidadES = 0;

        //            int cantidadEnDttemp_original = 0;
        //            if (Session["estatus" + Session.SessionID + HF_ClvPag.Value].ToString() != "1")
        //            {
        //                cantidadEnDttemp_original = 0;
        //            }
        //            else
        //            {
        //                cantidadEnDttemp_original = Convert.ToInt32(Session["CantidadEdicion" + Session.SessionID + HF_ClvPag.Value]);
        //            }

        //            int cantidadB = 0;
        //            foreach (EntradaSalidaDetalle dr in list_Es)
        //            {
        //                if (dr.Id_Prd.ToString() == id_prd.ToString())
        //                {
        //                    cantidadB += dr.Es_Cantidad;

        //                }
        //            }


        //            //rem.ConsultarRemisionesCantidad(session.Id_Emp, session.Id_Cd_Ver, refe, id_prd, ref cantidadES, session.Emp_Cnx);
        //            rem.ConsultarRemisionesCantidadRem(sesion.Id_Emp, sesion.Id_Cd_Ver, ref_, id_prd, ref cantidadES, sesion.Emp_Cnx);
        //            int cantidadES2 = 0;
        //            if (actualizacionDocumento)
        //            {
        //                rem.ConsultarRemisionesCantidadRemCantidad(sesion.Id_Emp, sesion.Id_Cd_Ver, es_, id_prd, nat_, ref cantidadES2, sesion.Emp_Cnx);
        //                cantidadES += cantidadES2;
        //            }




        //            if (cantidadES < cantidadB - cantidadEnDttemp_original + can_)
        //            //if (cantidadES < can_)
        //            {
        //                return "-1@@" + "Los artículos sobrepasan el disponible";

        //            }

        //            if (producto.Prd_InvFinal - producto.Prd_Asignado - (cantidadEnDttemp_original - can_) < 0)
        //            {
        //                return "-1@@" + "Producto " + producto.Id_Prd.ToString() + " inventario disponible insuficiente, inventario final: " + producto.Prd_InvFinal.ToString() + ", asignado: " + producto.Prd_Asignado.ToString() + ", disponible:" + (producto.Prd_InvFinal - producto.Prd_Asignado).ToString();

        //            }
        //        }
        //        else if (gpo_ == "1")
        //        {
        //            if (actualizacionDocumento)
        //            {
        //                CN_CapRemision rem = new CN_CapRemision();
        //                int cantidadES2 = 0;
        //                rem.ConsultarRemisionesCantidadRemCantidad(sesion.Id_Emp, sesion.Id_Cd_Ver, es_, id_prd, nat_, ref cantidadES2, sesion.Emp_Cnx);

        //                Producto cp = new Producto();
        //                CN_CatProducto cn_catproducto = new CN_CatProducto();
        //                cn_catproducto.ConsultaProducto(ref cp, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, id_prd);

        //                int cantidadB = 0;
        //                foreach (EntradaSalidaDetalle dr in list_Es)
        //                {
        //                    if (dr.Id_Prd.ToString() == id_prd.ToString())
        //                    {
        //                        cantidadB += dr.Es_Cantidad;
        //                    }
        //                }

        //                cantidadB = cantidadB - Convert.ToInt32(Session["CantidadEdicion" + Session.SessionID + HF_ClvPag.Value]) + (int)can_;
        //                if (cantidadB < cantidadES2 && (cantidadES2 - cantidadB) > (cp.Prd_InvFinal - cp.Prd_Asignado))
        //                {
        //                    return "-1@@" + "Producto " + id_prd.ToString() + " inventario disponible insuficiente, inventario final: " + cp.Prd_InvFinal.ToString() + ", asignado: " + cp.Prd_Asignado.ToString() + " , disponible: " + (cp.Prd_InvFinal - cp.Prd_Asignado).ToString() + "";
        //                }
        //            }
        //        }
        //        return "1";
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

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
        private void AlertaCerrar(string mensaje)
        {
            try
            {
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RadAjaxManager1.ResponseScripts.Add("CloseWindowA('" + mensaje + "');");
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
                RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
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