using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using CapaNegocios;
using CapaEntidad;
using System.Collections;

namespace SIANWEB
{
    public partial class CapGenerarPolizaCostos : System.Web.UI.Page
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
        #endregion

        #region Funciones
        private void LlenarComboAnio()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatCalendarioAnhio2_Combo", ref this.cmbAnio);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private void LlenarComboMes()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd, sesion.CalendarioIni.Year, sesion.Emp_Cnx, "spCatCalendarioMes_Combo", ref this.cmbMes);
                this.cmbMes.Enabled = true;

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

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                    _PermisoImprimir = Permiso.PImprimir;
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
        private bool ValidarFecha()
        {
            try
            {
                DateTime dt1 = DateTime.Parse(this.txtFecha1.SelectedDate.ToString());
                DateTime dt2 = DateTime.Parse(this.txtFecha2.SelectedDate.ToString());

                if (dt1 <= dt2)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {

                throw ex;
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

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        this.ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        ValidarPermisos();
                        CargarAlmacenes();
                        LlenarComboAnio();
                        LlenarComboMes();
                        this.rgPolizas.Rebind();

                        txtFecha1.DbSelectedDate = Sesion.CalendarioIni;
                        txtFecha2.DbSelectedDate = Sesion.CalendarioFin;
                        txtFecha1.Enabled = true;
                        txtFecha2.Enabled = true;

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                        
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        this.rgPolizas.Rebind();
                        break;

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void BtnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (_PermisoGuardar)
                {
                    if (ValidarFecha())
                    {

                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        CN_CatMovimientos cn_movs = new CN_CatMovimientos();


                        int Id_Alm = 0;
                        DateTime FechaIni;
                        DateTime FechaFin;
                        int Verificador = 0;

                        if (this.TxtId_Alm.Text != "")
                        {
                            Id_Alm = int.Parse(TxtId_Alm.Text);
                        }
                        else
                        {
                            Id_Alm = 0;
                        }

                        FechaIni = DateTime.Parse(this.txtFecha1.SelectedDate.ToString());
                        FechaFin = DateTime.Parse(this.txtFecha2.SelectedDate.ToString());

                        //cn_movs.ObtenerMovimientosEncabezado(Id_Alm, FechaIni, FechaFin, ref Verificador, sesion);

                        if (Verificador == -1)
                        {
                            //cn_movs.ObtenerMovimientosDetalle(Id_Alm, FechaIni, FechaFin, ref Verificador, sesion);

                            if (Verificador == -1)
                            {
                                Alerta("Se obtuvo la información de manera exitosa");
                            }
                            else
                            {
                                Alerta("Error inesperado al tratar de obtener el detalle de movimientos, por favor vuelva a intentar");
                            }
                        }
                        else
                        {
                            Alerta("Error inesperado al tratar de obtener los movimientos, por favor vuelva a intentar");
                        }


                    }
                    else
                    {
                        Alerta("La fecha inicial no puede ser mayor a la fecha final");

                    }
                }
                else
                {
                    Alerta("No tiene permisos para realizar esta accion");
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
                    case "print":
                        Imprimir(0);
                        break;
                    case "Generar":
                        Imprimir(1);
                        break;
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    this.rgPolizas.DataSource = GetList();
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
                this.rgPolizas.Rebind();
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
                List<string> statusPosibles = new List<string>();
                switch (e.CommandName)
                {


                    case "Imprimir":

                        int Ano = 0;
                        int Mes = 0;
                        string Tipo;
                        Ano = int.Parse(rgPolizas.MasterTableView.Items[e.Item.ItemIndex]["Pol_Ano"].Text);
                        Mes = int.Parse(rgPolizas.MasterTableView.Items[e.Item.ItemIndex]["Pol_Mes"].Text);
                        Tipo = rgPolizas.MasterTableView.Items[e.Item.ItemIndex]["Pol_Tipo"].Text.Trim();

                        ImprimirConsulta(Ano, Mes, Tipo);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg_ItemCommand");
            }
        }
        protected void BtnBuscar_Click1(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.rgPolizas.Rebind();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

       
        private void Imprimir(int VistaPrevia)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();
                ALValorParametrosInternos.Add(sesion.Emp_Cnx);

                ALValorParametrosInternos.Add(sesion.Id_Emp);
                ALValorParametrosInternos.Add(sesion.Id_Cd);
                ALValorParametrosInternos.Add(this.TxtId_Alm.Text == "" ? "0" : this.TxtId_Alm.Text);
                
                DateTime dt1 = Convert.ToDateTime (this.txtFecha1.SelectedDate.ToString());
                DateTime dt2 = Convert.ToDateTime(this.txtFecha2.SelectedDate.ToString());

                ALValorParametrosInternos.Add(dt1.ToShortDateString());
                ALValorParametrosInternos.Add(dt2.ToShortDateString());

                ALValorParametrosInternos.Add(sesion.U_Nombre);

                if (this.TxtId_Alm.Text == "")
                {
                    ALValorParametrosInternos.Add("Todos");
                }
                else
                {
                    ALValorParametrosInternos.Add(this.TxtId_Alm.Text);
                }
                ALValorParametrosInternos.Add(VistaPrevia);
                Type instance = null;
                
                if (this.RdblTipo.SelectedValue == "0")
                {
                    instance = typeof(LibreriaReportes.RepPolizaRevaluacionResumen);
                }
                else
                {
                    instance = typeof(LibreriaReportes.RepPolizaRevaluacionDet);
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

        private void ImprimirConsulta(int Ano, int Mes, string Tipo)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();


                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                ALValorParametrosInternos.Add(Ano);
                ALValorParametrosInternos.Add(Mes);
                ALValorParametrosInternos.Add(Tipo);
                ALValorParametrosInternos.Add(sesion.U_Nombre);

                Type instance = null;
                if (Tipo == "Resumen")
                {

                    instance = typeof(LibreriaReportes.RepPolizaRevaluacionResumenConsulta);
                }
                else
                {
                    instance = typeof(LibreriaReportes.RepPolizaRevaluacionDetConsulta);

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
        
        private List<PolizaRev> GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<PolizaRev> List = new List<PolizaRev>();
                CN_Poliza cn_poliza = new CN_Poliza();
                int ano = int.Parse(this.cmbAnio.SelectedValue);
                int mes = int.Parse(this.cmbMes.SelectedValue);
                cn_poliza.PolizaRev_ConsultaLista(ano, mes, ref List, sesion);

                return List;
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

        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("Impresion_error"))
                    Alerta("Error al momento de imprimir");
                else
                    Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }

        #endregion
         
    }
}