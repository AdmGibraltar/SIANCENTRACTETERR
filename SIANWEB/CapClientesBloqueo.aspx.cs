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
    public partial class CapClientesBloqueo : System.Web.UI.Page
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
                        CargarCDI();
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
        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                ConsultarClientes();

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
                    case "Nuevo":
                        LimpiarCampos();;
                        break;
                    case "Guardar":
                        Guardar();
                        break;
                    case "Imprimir":
                        Imprimir();
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
        private void CargarCDI()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Emp_Cnx, "SpCatCdi_Combo", ref CmbId_Cd);
               // CN_Comun.LlenaCombo(Sesion.Emp_Cnx, "spCatAlmacen_Combo", ref this.CmbId_Alm);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void ConsultarClientes()
        {
            try
            {
                if (this.TxtId_Cd.Text == string.Empty)
                {
                    Alerta("Seleccione un CDI");
                    return;
                }

                if (this.TxtId_Cte.Text == string.Empty)
                {
                    Alerta("Ingrese el numero de cliente");
                    return;
 
                }


                CN_CatAgrupador cn_ag = new CN_CatAgrupador();
                Agrupador Ag = new Agrupador();
                Sesion sesion=(Sesion)Session["Sesion" + Session.SessionID];
                int Verificador = 0;
                int Verificador2 = 0;

                cn_ag.CapClienteBloque_Consultar(int.Parse(this.TxtId_Cte.Text), int.Parse(this.TxtId_Cd.Text), ref Verificador,ref Verificador2 , ref Ag,sesion.Emp_Cnx);

                if (Verificador == -2)
                {
                    Alerta("El cliente <b>" + this.TxtId_Cte.Text + "</b> esta en la lista de desbloqueo por período, no se puede desbloquear permanentemente");
                    this.TxtId_Cte.Text = string.Empty;
                }
                else
                {
                    if (Verificador2 == -1)
                    {
                        this.TxtCte.Text = Ag.Cte_Nombre;
                        this.ChkBloquear.Checked = Ag.Cte_NoBloquear;

                        this.TxtId_Cd.Enabled = false;
                        this.CmbId_Cd.Enabled = false;
                        this.TxtId_Cte.Enabled = false;

                    }
                    else
                    {
                          Alerta("El cliente no existe, por favor revise la información");
                    }
                }

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void LimpiarCampos()
        {
            try
            {
                this.TxtId_Cd.Enabled = true;
                this.CmbId_Cd.Enabled = true;
                this.TxtId_Cte.Enabled = true;
                this.TxtId_Cd.Text = string.Empty;
                this.CmbId_Cd.Text = " -- Seleccionar -- ";
                this.TxtId_Cte.Text = string.Empty;
                this.CmbId_Cd.SelectedValue = "-1";

                this.TxtCte.Text= string.Empty;
                this.ChkBloquear.Checked = false;
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
                if (this.TxtId_Cd.Text == string.Empty)
                {
                    Alerta("Seleccione un CDI");
                    return;
                }

                if (this.TxtId_Cte.Text == string.Empty)
                {
                    Alerta("Ingrese el número de cliente");
                    return;

                }

                CN_CatAgrupador cn_ag = new CN_CatAgrupador();
                Agrupador Ag = new Agrupador();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int Verificador = 0;

                Ag.Id_Cd = int.Parse(this.TxtId_Cd.Text);
                Ag.Id_Cte = int.Parse(this.TxtId_Cte.Text);
                Ag.Cte_NoBloquear  = this.ChkBloquear.Checked;

                cn_ag.CapClienteBloque_CteInsertar(Ag, ref Verificador, sesion.Emp_Cnx);

                if (Verificador == -1)
                {
                    LimpiarCampos();
                    Alerta("Los datos se guardaron correctamente");
                    
                }
                else 
                {
                    Alerta("Error al intentar guardar la infromación");
                }


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


                Type instance = null;

                instance = typeof(LibreriaReportes.RepClientesBloq);


                Session["InternParameter_Values" + Session.SessionID + HF_Cve.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_Cve.Value] = instance.AssemblyQualifiedName;
                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_Cve.Value + "');");


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