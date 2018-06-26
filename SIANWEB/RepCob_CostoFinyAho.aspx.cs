using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Collections;

namespace SIANWEB
{
    public partial class RepCob_CostoFinyAho : System.Web.UI.Page
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

        #region Metodos
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
                        CargarCentros();
                        rblTrimestres.SelectedValue = "0";
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_Cve.Value = randObj.Next().ToString();
                        this.dpFecha.SelectedDate = DateTime.Today;
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
                        //this.rgFacturaRuta.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
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
        private void Imprimir()
        {
            try
            {
                if (this.txtTasa.Text == string.Empty)
                {
                    Alerta("Debe especificar la tasa que se utilizará para el reporte.");
                    return;
                }

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();
                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                ALValorParametrosInternos.Add(CmbCentro.SelectedValue);
                DateTime Cierre = Convert.ToDateTime(this.dpFecha.SelectedDate.ToString());
                ALValorParametrosInternos.Add(Cierre.ToShortDateString());
                ALValorParametrosInternos.Add(this.txtTasa.Text);
                ALValorParametrosInternos.Add(this.rblReportes.SelectedValue);
                if (this.rblReportes.SelectedValue == "1")
                    ALValorParametrosInternos.Add("0");
                else
                    ALValorParametrosInternos.Add(this.rblTrimestres.SelectedValue);

                Type instance = null;
                if (rblReportes.SelectedValue == "1")
                    instance = typeof(LibreriaReportes.Rep_CobCostoFinanciamientoGral);
                else if (rblReportes.SelectedValue == "2")
                    instance = typeof(LibreriaReportes.Rep_CobCostoFinanciamientoTrim);
            
                Session["InternParameter_Values" + Session.SessionID + HF_Cve.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_Cve.Value] = instance.AssemblyQualifiedName;
                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_Cve.Value + "');");
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
                CN_Comun.LlenaCombo(1, Sesion.Emp_Cnx, "spCDI_Combo ", ref CmbCentro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Eventos
       
        protected void Index_Changed(object sender, EventArgs e) 
        {
            if (rblReportes.SelectedValue == "2")
            {
                rblTrimestres.Visible = true;
                lbltrimestre.Visible = true;
                dpFecha.Enabled = false;

            }
            else
            { 
                rblTrimestres.Visible = false;
                lbltrimestre.Visible = false;
                dpFecha.Enabled = true;
            }
        }

        protected void rtb_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (Page.IsValid)
                {
                    if (btn.CommandName == "print")
                    {
                        this.Imprimir();
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
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