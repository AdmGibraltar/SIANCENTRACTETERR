using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using System.Collections;
using System.Text;
using System.IO;
using CapaDatos;
using System.Xml;

namespace SIANWEB
{
    public partial class CapEntradasSalidasCentral_Lista : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                {
                    return false;
                }
                return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }
        private bool _PermisoModificar
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                {
                    return false;
                }
                return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }
        private bool _PermisoEliminar
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                {
                    return false;
                }
                return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }
        private bool _PermisoImprimir
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                {
                    return false;
                }
                return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }
        private DataTable dt;
        public List<EntradasSalidasCentral> List_Es
        {
            get
            {
                return (Session["DetallesMovsCentral" + Session.SessionID] as List<EntradasSalidasCentral>);
            }
            set
            {
                Session["DetallesMovsCentral" + Session.SessionID] = value;
            }

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
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    if (!Page.IsPostBack)
                    {

                        txtFecha1.DbSelectedDate = Sesion.CalendarioIni;
                        txtFecha2.DbSelectedDate = Sesion.CalendarioFin;

                        ValidarPermisos();
                   
                        CargarTipoMovimiento();
                        CargarAlmacenes();
                        CargarLista();
                       
                      
                        
                        double ancho = 0;
                        foreach (GridColumn gc in this.rgMovimientos.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgMovimientos.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgMovimientos.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgMovimientos.Rebind();
                      
                  
                        if (!Sesion.Cu_Modif_Pass_Voluntario)
                        {
                            RAM1.ResponseScripts.Add("(function(){var f = function(){AbrirContrasenas(); return false;Sys.Application.remove_load(f);};Sys.Application.add_load(f);})()");
                        }
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Page_Load");
            }
        }
        protected void RAM1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            try
            {
                ErrorManager();
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        CargarLista();
                        rgMovimientos.Rebind();
    
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void rg_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    this.rgMovimientos.DataSource = List_Es;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            try
            {
                rgMovimientos.Rebind();

            }
            catch (Exception ex)
            {
                
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
            //rgPedido.Rebind();
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridItem item = e.Item;
                int Id_Emp = 0;
                int Id_Alm = 0;
                int Id_MovC = 0;
                int Id_Tm = 0;
                bool MovC_Naturaleza = false;
                string Referencia;
                Referencia = rgMovimientos.MasterTableView.Items[e.Item.ItemIndex]["MovC_Referencia"].Text;
                Id_Emp = int.Parse(rgMovimientos.MasterTableView.Items[e.Item.ItemIndex]["Id_Emp"].Text);
                Id_Alm = int.Parse(rgMovimientos.MasterTableView.Items[e.Item.ItemIndex]["Id_Alm"].Text);
                Id_MovC = int.Parse(rgMovimientos.MasterTableView.Items[e.Item.ItemIndex]["Id_MovC"].Text);
                Id_Tm = int.Parse(rgMovimientos.MasterTableView.Items[e.Item.ItemIndex]["Id_Tm"].Text);
                MovC_Naturaleza = bool.Parse(rgMovimientos.MasterTableView.Items[e.Item.ItemIndex]["MovC_Naturaleza"].Text);

                switch (e.CommandName)
                {
                    case "Editar":
                    RAM1.ResponseScripts.Add("return OpenWindow('" + Id_Emp  + "','" + Id_Alm + "','" + Id_MovC  + "','" + Id_Tm  + "','"+ MovC_Naturaleza +  "')");
                    break;
               case "Imprimir":
                  ImprimirDetalle(Id_Emp, Id_Alm, Id_MovC, Id_Tm, MovC_Naturaleza);
                    break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rgPedido_ItemCommand");
            }
        }
        protected void BtnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                CargarLista();
                rgMovimientos.Rebind();
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void rgMovimientos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem dataItem = e.Item as GridDataItem;

                    double totalfac = 0;
                    double totalest = 0;
                    double variacion = 0;

                    if (dataItem["TotalFac"].Text != "&nbsp;")
                    {
                        totalfac = double.Parse(dataItem["TotalFac"].Text);
                        dataItem["TotalFac"].Text = totalfac.ToString("N2");
                    }
                    else
                    {
                        totalfac = 0;
                    }


                    if (dataItem["TotalCostoEst"].Text != "&nbsp;")
                    {
                        totalest = double.Parse(dataItem["TotalCostoEst"].Text);
                        dataItem["TotalCostoEst"].Text = totalest.ToString("N2");
                    }
                    else
                    {
                        totalest = 0;
                    }

                    if (dataItem["Variacion"].Text != "&nbsp;")
                    {
                        variacion = double.Parse(dataItem["Variacion"].Text);

                        if (variacion < 0)
                        {
                            variacion = variacion * -1;
                            dataItem["Variacion"].Text = variacion.ToString("N2");
                        }
                        else
                        {
                            dataItem["Variacion"].Text = variacion.ToString("N2");
                        }
                    }


                    if (totalfac > totalest)
                    {
                        dataItem["Variacion"].ForeColor = System.Drawing.Color.Red;


                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;
                switch (btn.CommandName)
                {
                    case "print":
                        ImprimirResumen();
                        break;
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion
        #region Funciones
        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                
                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                }
                else
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                }
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

                    
                    if (Permiso.PImprimir== false)
                    {
                        ((RadToolBarItem)rtb1.Items.FindItemByValue("print")).Visible = false;
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
        private void CargarLista()
        {
            try
            {
                List_Es = GetList();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private List<EntradasSalidasCentral> GetList()
        {
            try
            {
                List<EntradasSalidasCentral> List = new List<EntradasSalidasCentral>();
                EntradasSalidasCentral  es = new EntradasSalidasCentral ();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapEntradasSalidasCentral cn_es = new CN_CapEntradasSalidasCentral();

                LlenarObjetoEs(ref es);

                cn_es.ConsultaLista(es, ref List, sesion);

                dt = CN__Comun.Convertidor<EntradasSalidasCentral>.ListaToDatatable(List);
                CalcularTotales();
                return List;
 
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LlenarObjetoEs(ref EntradasSalidasCentral es)
        {
            try
            {

                if (this.TxtId_Alm.Text != "")
                {
                    es.Alm = this.TxtId_Alm.Text;
                }
                else
                {
                    es.Alm = null;
                }

                if (this.TxtId_Tm.Text != "")
                {
                    es.Tm = this.TxtId_Tm.Text;
                }
                else
                {
                    es.Tm = null;
                }

                if (this.TxtReferencia.Text != "")
                {
                    es.Ref = this.TxtReferencia.Text;
                }
                else
                {
                    es.Ref = null;
                }

                if (this.CmbMovC_Naturaleza.SelectedValue == "-1")
                {
                    es.Nat = null;
                }
                else 
                {
                    es.Nat = this.CmbMovC_Naturaleza.SelectedValue;
                }

                if (this.TxtId_MovCIni.Text != "")
                {
                    es.MovIni = this.TxtId_MovCIni.Text;
                }
                else
                {
                    es.MovIni  = null;
                }

                if (this.TxtId_MovCIni.Text != "")
                {
                    es.MovFin = this.TxtId_MovCFin.Text;
                }
                else
                {
                    es.MovFin = null;
                }

                if (this.txtFecha1.SelectedDate.ToString() != "")
                {
                    es.Fechaini = this.txtFecha1.SelectedDate.ToString();
                }
                else
                {
                    es.Fechaini = null;
                }

                if (this.txtFecha2.SelectedDate.ToString() != "")
                {
                    es.Fechafin = this.txtFecha2.SelectedDate.ToString();
                }
                else
                {
                    es.Fechafin = null;
                }

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void CargarTipoMovimiento()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatMovimiento_ComboCentral", ref cmbTipoMovimiento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarAlmacenes()
        {
            try
            {
                    Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Emp_Cnx, "spCatAlmacen_Combo", ref this.CmbId_Alm);

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
                double totalfac = 0;
                double totalest = 0;
                double variacion = 0;


                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["TotalFac"].ToString() != "")
                    {
                        totalfac += Convert.ToDouble(dr["TotalFac"]);
                    }

                    if (dr["TotalCostoEst"].ToString() != "")
                    {
                        totalest += Convert.ToDouble(dr["TotalCostoEst"]);
                    }

                    if (dr["TotalFac"].ToString() != "" && dr["TotalCostoEst"].ToString() != "")
                    {
                        variacion += (Convert.ToDouble(dr["TotalCostoEst"]) - Convert.ToDouble(dr["TotalFac"]));
                    }

               
                }

                this.TxtTotalFac.Text = totalfac.ToString("N2");
                this.TxtTotalEst.Text = totalest.ToString("N2");

                if (variacion >= 0)
                {
                    this.TxtTotVariacion.Text = variacion.ToString("N2");
                    this.TxtTotVariacion.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    variacion = variacion * -1;
                    this.TxtTotVariacion.Text = variacion.ToString("N2");
                    this.TxtTotVariacion.ForeColor = System.Drawing.Color.Red;

                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void ImprimirResumen()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();

                ALValorParametrosInternos.Add(sesion.Emp_Cnx);

                ALValorParametrosInternos.Add(this.TxtId_Alm.Text == "" ? null: this.TxtId_Alm.Text);
                if (this.TxtId_Alm.Text == "")
                {
                    ALValorParametrosInternos.Add("Todos");
                }
                else
                {
                    ALValorParametrosInternos.Add(this.TxtId_Alm.Text + "-" + this.CmbId_Alm.Text);
                }

                ALValorParametrosInternos.Add(this.TxtId_Tm.Text == "" ? null : this.TxtId_Tm.Text);
                if (this.TxtId_Tm.Text == "")
                {
                    ALValorParametrosInternos.Add("Todos");
                }
                else
                {
                    ALValorParametrosInternos.Add(this.TxtId_Tm.Text + "-" + this.cmbTipoMovimiento.Text);
                }

                ALValorParametrosInternos.Add(this.TxtReferencia.Text == "" ? null : this.TxtReferencia.Text);

                if (this.CmbMovC_Naturaleza.SelectedValue == "-1")
                {
                    ALValorParametrosInternos.Add(null);
                }
                else
                {
                    ALValorParametrosInternos.Add(this.CmbMovC_Naturaleza.SelectedValue);
 
                }

                ALValorParametrosInternos.Add(this.TxtId_MovCIni.Text == "" ? null : this.TxtId_MovCIni.Text);
                ALValorParametrosInternos.Add(this.TxtId_MovCFin.Text == "" ? null : this.TxtId_MovCFin.Text);

                DateTime dt1 = Convert.ToDateTime(this.txtFecha1.SelectedDate.ToString());
                DateTime dt2 = Convert.ToDateTime(this.txtFecha2.SelectedDate.ToString());
                ALValorParametrosInternos.Add(dt1.ToShortDateString());
                ALValorParametrosInternos.Add(dt2.ToShortDateString());
                ALValorParametrosInternos.Add(sesion.U_Nombre);

                Type instance = null;
                instance = typeof(LibreriaReportes.RepMovimientosResumen);

                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;
                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");



               

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void ImprimirDetalle( int Id_Emp, int Id_Alm, int Id_Movc, int Id_Tm, bool MovC_Naturaleza)
        {
            try
            {

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();

                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                ALValorParametrosInternos.Add(Id_Emp);
                ALValorParametrosInternos.Add(Id_Alm);
                ALValorParametrosInternos.Add(Id_Movc);
                ALValorParametrosInternos.Add(Id_Tm);
                ALValorParametrosInternos.Add(MovC_Naturaleza);
                ALValorParametrosInternos.Add(sesion.U_Nombre);

                Type instance = null;

                if (Id_Tm == 2)
                {
                    instance = typeof(LibreriaReportes.RepMovimientosDetalleTM2);
                }
                else
                {
                    instance = typeof(LibreriaReportes.RepMovimientosDetalle);
                }

                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;
                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");



            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        #endregion
        #region ErrorManager
                
        private void RadConfirm(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radconfirm('" + mensaje + "', confirmCallBackFn);");
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