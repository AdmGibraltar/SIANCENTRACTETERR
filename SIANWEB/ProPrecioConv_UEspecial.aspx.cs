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
    public partial class ProPrecioConv_UEspecial : System.Web.UI.Page
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
                        ValidarPermisos();
                        CargarUsuarios();
                        CargarPermisosEspeciales();
                       
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_Cve.Value = randObj.Next().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Page_Load");
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    //case "RebindGrid":
                    //    this.rgPolizas.Rebind();
                    //    break;
                    
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
                
                    case "Guardar":
                        Guardar();
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
        private void CargarUsuarios()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun cn_comun = new CN__Comun();
                cn_comun.LlenaCombo(sesion.Id_Cd_Ver, 1, 1, sesion.Emp_Cnx, "spCatUsuario_Combo", ref CmbId_UAdmin1 );
                cn_comun.LlenaCombo(sesion.Id_Cd_Ver, 1, 1, sesion.Emp_Cnx, "spCatUsuario_Combo", ref CmbId_UAdmin2);
                cn_comun.LlenaCombo(sesion.Id_Cd_Ver, 1, 1, sesion.Emp_Cnx, "spCatUsuario_Combo", ref CmbId_UCteIntranet);
                cn_comun.LlenaCombo(sesion.Id_Cd_Ver, 1, 1, sesion.Emp_Cnx, "spCatUsuario_Combo", ref CmbId_UCteMacola);
                cn_comun.LlenaCombo(sesion.Id_Cd_Ver, 1, 1, sesion.Emp_Cnx, "spCatUsuario_Combo", ref CmbId_UComodato);
                cn_comun.LlenaCombo(sesion.Id_Cd_Ver, 1, 1, sesion.Emp_Cnx, "spCatUsuario_Combo", ref CmbId_UTodo);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void CargarPermisosEspeciales()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Convenio conv = new Convenio();
                CN_Convenio cn_conv = new CN_Convenio();

                cn_conv.ConsultaUsuariosEspeciales(ref conv, sesion.Emp_Cnx);

                this.CmbId_UAdmin1.SelectedValue = conv.Pue_Admin1.ToString();
                this.CmbId_UAdmin2.SelectedValue = conv.Pue_Admin2.ToString();
                this.CmbId_UCteIntranet.SelectedValue = conv.Pue_CteIntranet.ToString();
                this.CmbId_UCteMacola.SelectedValue = conv.Pue_CteMacola.ToString();
                this.CmbId_UComodato.SelectedValue = conv.Pue_EqComodato.ToString();
                this.CmbId_UTodo.SelectedValue = conv.Pue_VerTodo.ToString();

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
                CN_Convenio cn_conv = new CN_Convenio();
                Convenio conv = new Convenio();
                int Verificador = 0;

                conv.Id_Cd = sesion.Id_Cd_Ver;
                conv.Pue_Admin1 = int.Parse(CmbId_UAdmin1.SelectedValue);
                conv.Pue_Admin2 = int.Parse(CmbId_UAdmin2.SelectedValue);
                conv.Pue_CteMacola = int.Parse(CmbId_UCteMacola.SelectedValue);
                conv.Pue_CteIntranet = int.Parse(CmbId_UCteIntranet.SelectedValue);
                conv.Pue_EqComodato = int.Parse(CmbId_UComodato.SelectedValue);
                conv.Pue_VerTodo = int.Parse(CmbId_UTodo.SelectedValue);


                cn_conv.ModificaUsuariosEspeciales(conv, ref Verificador, sesion.Emp_Cnx);

                if (Verificador == -1)
                {
                    Alerta("Los datos se han guardado correctamente");
                }
                else
                {
                    Alerta("Error al tratar de guardar usuarios especiales");
                }

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