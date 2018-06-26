using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Data;
using System.Configuration;
using System.Web.Configuration;
namespace SIANWEB
{
    public partial class VentanaAutorizacionCambiosTerritorios : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        DataTable dt { get { return (DataTable)Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        #endregion

        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                Sesion sesion = new Sesion(); sesion = (Sesion)Session["Sesion" + Session.SessionID]; if (sesion == null) { string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries); Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false); } CN__Comun comun = new CN__Comun(); comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);

                Inicializar();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }

        //Pendientes
        protected void rg1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rg1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_PageIndexChanged");
            }
        }
        protected void rg1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rg1.DataSource = GetListAutorizacionesPendientes();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_NeedDataSource");
            }
        }
        protected void rg1_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {

                ErrorManager();
                if (e.CommandName.ToString() == "Aprobar")
                {
                    Int32 item = default(Int32);
                    item = e.Item.ItemIndex;
                    if (item >= 0)
                    {
                        Sesion Sesion = new Sesion();
                        Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        long IdAutorizacion = 0;
                        IdAutorizacion = long.Parse(rg1.Items[item]["IdAutorizacion"].Text);
                        CN_CatTerritorios cn_CatTerritorios = new CN_CatTerritorios();
                        ModelAutorizacionTerritorios DatosSolicitud = new ModelAutorizacionTerritorios();
                        DatosSolicitud.IdAutorizacion = IdAutorizacion;
                        DatosSolicitud.BdName = rg1.Items[item]["BdName"].Text;
                        string ConexionWebCentral = WebConfigurationManager.AppSettings["strConnectionCentral"].ToString();
                        cn_CatTerritorios.AutorizarSolicitudCambioTerritorio(DatosSolicitud, Sesion.Id_U, ConexionWebCentral);
                        Inicializar();
                    }

                }

                if (e.CommandName.ToString() == "Rechazar")
                {
                    Int32 item = default(Int32);
                    item = e.Item.ItemIndex;
                    if (item >= 0)
                    {
                        Sesion Sesion = new Sesion();
                        Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        long IdAutorizacion = 0;
                        IdAutorizacion = long.Parse(rg1.Items[item]["IdAutorizacion"].Text);
                        CN_CatTerritorios cn_CatTerritorios = new CN_CatTerritorios();
                        ModelAutorizacionTerritorios DatosSolicitud = new ModelAutorizacionTerritorios();
                        DatosSolicitud.IdAutorizacion = IdAutorizacion;
                        DatosSolicitud.BdName = rg1.Items[item]["BdName"].Text;
                        DatosSolicitud.Comentario = "";

                        Session["SolicitudRechazar"] = DatosSolicitud;
                        //  string ConexionWebCentral = WebConfigurationManager.AppSettings["strConnectionCentral"].ToString();
                        //  cn_CatTerritorios.RechazarSolicitudCambioTerritorio(DatosSolicitud, Sesion.Id_U, ConexionWebCentral);

                        //    Inicializar();

                        // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openwin()", true);

                        RadWindow1.VisibleOnPageLoad = true;
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_ItemCommand");
            }
        }

        private List<ModelAutorizacionTerritorios> GetListAutorizacionesPendientes()
        {
            try
            {
                List<ModelAutorizacionTerritorios> List = new List<ModelAutorizacionTerritorios>();
                CN_CatTerritorios clsCatTerritorios = new CN_CatTerritorios();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Territorios territorio = new Territorios();
                territorio.Id_Emp = session2.Id_Emp;
                territorio.Id_Cd = session2.Id_Cd_Ver;
                string ConexionWebCentral = WebConfigurationManager.AppSettings["strConnectionCentral"].ToString();
                clsCatTerritorios.ObtenerAutorizacionesPendientesCambioTerritorio(territorio.Id_Cd, territorio.Id_Emp, ConexionWebCentral, ref List);

                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Aprobadas
        protected void rg2_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rg2.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg2_PageIndexChanged");
            }
        }
        protected void rg2_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rg2.DataSource = GetListAutorizacionesAprobadas();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_NeedDataSource");
            }
        }
        protected void rg2_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {



            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_ItemCommand");
            }
        }
        private List<ModelAutorizacionTerritorios> GetListAutorizacionesAprobadas()
        {
            try
            {
                List<ModelAutorizacionTerritorios> List = new List<ModelAutorizacionTerritorios>();
                CN_CatTerritorios clsCatTerritorios = new CN_CatTerritorios();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Territorios territorio = new Territorios();
                territorio.Id_Emp = session2.Id_Emp;
                territorio.Id_Cd = session2.Id_Cd_Ver;
                string ConexionWebCentral = WebConfigurationManager.AppSettings["strConnectionCentral"].ToString();
                clsCatTerritorios.ObtenerAutorizacionesAprobadasCambioTerritorio(territorio.Id_Cd, territorio.Id_Emp, ConexionWebCentral, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Rechazadas
        protected void rg3_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rg3.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg3_PageIndexChanged");
            }
        }
        protected void rg3_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rg3.DataSource = GetListAutorizacionesRechazadas();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg3_NeedDataSource");
            }
        }
        protected void rg3_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {



            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg3_ItemCommand");
            }
        }
        private List<ModelAutorizacionTerritorios> GetListAutorizacionesRechazadas()
        {
            try
            {
                List<ModelAutorizacionTerritorios> List = new List<ModelAutorizacionTerritorios>();
                CN_CatTerritorios clsCatTerritorios = new CN_CatTerritorios();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Territorios territorio = new Territorios();
                territorio.Id_Emp = session2.Id_Emp;
                territorio.Id_Cd = session2.Id_Cd_Ver;
                string ConexionWebCentral = WebConfigurationManager.AppSettings["strConnectionCentral"].ToString();
                clsCatTerritorios.ObtenerAutorizacionesRechazadasCambioTerritorio(territorio.Id_Cd, territorio.Id_Emp, ConexionWebCentral, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Inicializar()
        {


            rg1.Rebind();
            rg2.Rebind();
            rg3.Rebind();
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
                    Response.Redirect("Inicio.aspx");
                CN_Ctrl ctrl = new CN_Ctrl();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void RechazarSolicitudComentarios(object sender, EventArgs e)
        {


            ModelAutorizacionTerritorios DatosSolicitudRechaza = (ModelAutorizacionTerritorios)Session["SolicitudRechazar"];
            if (!string.IsNullOrEmpty(txtComentarioRechazo.Text))
            {
                DatosSolicitudRechaza.Comentario = txtComentarioRechazo.Text;
                string ConexionWebCentral = WebConfigurationManager.AppSettings["strConnectionCentral"].ToString();
                CN_CatTerritorios cn_CatTerritorios = new CN_CatTerritorios();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                cn_CatTerritorios.RechazarSolicitudCambioTerritorio(DatosSolicitudRechaza, Sesion.Id_U, ConexionWebCentral);
                RadWindow1.VisibleOnPageLoad = false;
                Inicializar();

            }
            else
            {
                Alerta("Debe agregar un comentario.");
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
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
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
                        ValidarPermisos();


                        CargarCentros();
                        Inicializar();
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        #region ErrorManager
        private void Alerta(string mensaje)
        {
            try
            {
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