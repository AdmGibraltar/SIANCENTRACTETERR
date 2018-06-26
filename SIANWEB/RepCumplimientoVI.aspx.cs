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
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using Telerik.Reporting.Processing;

namespace SIANWEB
{
    public partial class RepCumplimientoVI : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private Sesion sesion
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["Sesion" + Session.SessionID] = value;
            }
        }
        #endregion Variables
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
                        this.CargarCentros();

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_Cve.Value = randObj.Next().ToString();
                        CargarCombos();
                      
                        this.TblEncabezado.Visible = false;

                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }
        #region Eventos
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);

           
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        //this.rgFacturaRuta.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                string Evento = Request.Form["__EVENTTARGET"].ToString();

                if (!_PermisoImprimir)
                {
                    this.Alerta("No tiene permisos para ver el reporte");
                    return;
                }
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (Page.IsValid)
                {
                    if (btn.CommandName == "print")
                    {
                        Imprimir();
                    }

                }
            }
            catch (Exception ex)
            {
                Alerta("Imposible generar el reporte; aún no se han generado los respaldos del mes y año seleccionados");
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
            }
            CN__Comun comun = new CN__Comun();
            comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
            //nuevo();
        }
        protected void CmbCDI_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun cn_comun = new CN__Comun();

                cn_comun.LlenaCombo(2, int.Parse(cmbCDI.SelectedValue), sesion.Emp_Cnx, "spCatRikVI_Combo", ref this.cmbRIK);
                cn_comun.LlenaCombo(2, int.Parse(cmbCDI.SelectedValue), sesion.Emp_Cnx, "spCatTerritorioVI_Combo", ref this.cmbTer);
                cn_comun.LlenaCombo(2, int.Parse(cmbCDI.SelectedValue), sesion.Emp_Cnx, "spCatClienteVI_Combo", ref this.cmbCte);

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        
        }
        protected void RblTipoCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun cn_comun = new CN__Comun();
                cn_comun.LlenaCombo( int.Parse(this.RblTipoCd.SelectedValue), sesion.Emp_Cnx, "spCatCDI_ComboTodos", ref cmbCDI);
            }
            catch (Exception ex)
            {
               ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        #endregion Eventos
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
                    _PermisoImprimir = Permiso.PImprimir;
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
                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarCombos()
        {
            try
            {

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun cn_comun = new CN__Comun();
                cn_comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd, sesion.Emp_Cnx, "spCatCalendarioAnhio2_Combo", ref this.cmbanio);
                cn_comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd, sesion.CalendarioIni.Year, sesion.Emp_Cnx, "spCatCalendarioMes_Combo", ref this.cmbmes );
                cn_comun.LlenaCombo(sesion.Emp_Cnx, "spCatCDIRegion_Combo", ref cmbRegion);
                cn_comun.LlenaCombo( int.Parse(this.RblTipoCd.SelectedValue), sesion.Emp_Cnx, "spCatCDI_ComboTodos", ref cmbCDI);
                cn_comun.LlenaCombo(2, sesion.Emp_Cnx, "spCatUENVI_Combo", ref cmbUEN);
                cn_comun.LlenaCombo(2, sesion.Emp_Cnx, "spCatSegmentoVI_Combo", ref cmbSegmento);
                cn_comun.LlenaCombo(2,-1, sesion.Emp_Cnx, "spCatRikVI_Combo", ref this.cmbRIK);
                cn_comun.LlenaCombo(2, -1, sesion.Emp_Cnx, "spCatTerritorioVI_Combo", ref this.cmbTer);
                cn_comun.LlenaCombo(2, -1, sesion.Emp_Cnx, "spCatClienteVI_Combo", ref this.cmbCte);

                this.cmbanio.SelectedValue = DateTime.Now.Year.ToString();
                this.cmbmes.SelectedValue = DateTime.Now.Month.ToString();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void Imprimir()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();

                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                ALValorParametrosInternos.Add(this.cmbanio.SelectedValue);
                ALValorParametrosInternos.Add(this.cmbmes.SelectedValue);
                ALValorParametrosInternos.Add(this.cmbRegion.SelectedValue == "-1"  ? null : this.cmbRegion.SelectedValue);
                if (cmbCDI.SelectedValue == "-1" && RblTipo.SelectedValue == "1" || RblTipo.SelectedValue == "2")
                {
                    DisplayMensajeAlerta("Para este tipo de reporte se debe seleccionar una sucursal en el filtro CD.");
                    return;
                }
                else{
                    ALValorParametrosInternos.Add(this.cmbCDI.SelectedValue == "-1" ? null : this.cmbCDI.SelectedValue);}
                ALValorParametrosInternos.Add(this.cmbUEN.SelectedValue == "-1" ? null : this.cmbUEN.SelectedValue);
                ALValorParametrosInternos.Add(this.cmbSegmento.SelectedValue == "-1" ? null : this.cmbSegmento.SelectedValue);
                ALValorParametrosInternos.Add(this.cmbRIK.SelectedValue == "-1" || this.cmbRIK.SelectedValue == "" ? null : this.cmbRIK.SelectedValue);
                ALValorParametrosInternos.Add(this.cmbTer.SelectedValue == "-1" || this.cmbTer.SelectedValue == "" ? null : this.cmbTer.SelectedValue);
                ALValorParametrosInternos.Add(this.cmbCte.SelectedValue == "-1" || this.cmbCte.SelectedValue == "" ? null : this.cmbCte.SelectedValue);
                ALValorParametrosInternos.Add(this.RblTipo.SelectedValue);
                ALValorParametrosInternos.Add(sesion.U_Nombre);
                ALValorParametrosInternos.Add(this.cmbRegion.Text);
                ALValorParametrosInternos.Add(this.cmbCDI.Text);
                ALValorParametrosInternos.Add(this.cmbUEN.Text);
                ALValorParametrosInternos.Add(this.cmbSegmento.Text);
                ALValorParametrosInternos.Add(this.cmbRIK.Text);
                ALValorParametrosInternos.Add(this.cmbTer.Text);
                ALValorParametrosInternos.Add(this.cmbCte.Text);
                ALValorParametrosInternos.Add(this.cmbmes.Text);

                Type instance = null;

                if (RblTipo.SelectedValue == "1")
                {
                    instance = typeof(LibreriaReportes.RepCumplimientoVIGnral);
                }
                else if (RblTipo.SelectedValue == "2")
                {
                    instance = typeof(LibreriaReportes.RepCumplimientoVIDet);
                }
                else 
                {
                    instance = typeof(LibreriaReportes.RepCumplimientoVIConcen);
                }

                Session["InternParameter_Values" + Session.SessionID + HF_Cve.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_Cve.Value] = instance.AssemblyQualifiedName;
                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_Cve.Value + "');");
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        #endregion Funciones
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