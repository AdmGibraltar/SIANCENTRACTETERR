using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using CapaDatos;
using Telerik.Web.UI;
using CapaNegocios;

namespace SIANWEB
{
    public partial class CatAgrupador : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        static bool actualiza;

 

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
                    Context.Items.Add("href", pag[pag.Length - 1]);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);
                }
                else
                {
               
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        CargarCentros();
                       
                        rgAgrupador.Rebind();
                        if (!Sesion.Cu_Modif_Pass_Voluntario)
                        {
                            RadAjaxManager1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                        }


                        //Session["Head" + Session.SessionID] = "Region";

                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgAgrupador_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    this.rgAgrupador.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgAgrupador_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                this.HdId_Agp.Value = rgAgrupador.MasterTableView.Items[e.Item.ItemIndex]["Id_Agp"].Text;
                this.txtDescripcion2.Text = rgAgrupador.MasterTableView.Items[e.Item.ItemIndex]["Ag_Descripcion"].Text;
            }
        }
        protected void rgAgrupador_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            try
            {
                this.rgAgrupador.Rebind();
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }


        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "new":
                        LimpiarCampos();
                        break;

                    case "save":
                        Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                Alerta("Ha ocurrido un problema: " + ex.Message);
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
                        ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("new")).Visible = _PermisoGuardar;                  
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                         ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible = _PermisoGuardar;
                  
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("mail")).Visible = false;
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("print")).Visible = false;
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("delete")).Visible = false;
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("undo")).Visible = false;                   
                }
                else               
                    Response.Redirect("Inicio.aspx");               

                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
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
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<Agrupador> GetList()
        {
            try
            {
                List<Agrupador> List = new List<Agrupador>();
                CN_CatAgrupador cn_ag = new CN_CatAgrupador();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                cn_ag.CatAgrupador_ConsultaLista(ref List, sesion.Emp_Cnx);

                return List;
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
                this.HdId_Agp.Value = string.Empty;
                this.txtDescripcion2.Text = string.Empty;

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
                Agrupador ag = new Agrupador();
                CN_CatAgrupador cn_ag = new CN_CatAgrupador();
               Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
               int Verificador = 0;
               if (this.HdId_Agp.Value != "")
               {
                   ag.Id_Agp = int.Parse(this.HdId_Agp.Value);
                   ag.Ag_Descripcion = this.txtDescripcion2.Text.Trim();

                   cn_ag.CatAgrupador_Modificar(ag, ref Verificador, sesion.Emp_Cnx);


                   if (Verificador == -1)
                   {
                       Alerta("Los datos se modificaron correctamente");
                   }
                   else if (Verificador == -2)
                   {
                       Alerta("Ya existe un agrupador con la misma descripcion");
                   }
                   else
                   {
                       Alerta("Error inesperado al tratar de guardar el registro");
                   }
               }
               else
               {
                   ag.Ag_Descripcion = this.txtDescripcion2.Text.Trim();
                   cn_ag.CatAgrupador_Guardar(ag, ref Verificador, sesion.Emp_Cnx);

                   if (Verificador == -1)
                   {
                       Alerta("Los datos se guardaron correctamente");
                   }
                   else if (Verificador == -2)
                   {
                       Alerta("Ya existe un agrupador con la misma descripcion");
                   }
                   else
                   {
                       Alerta("Error inesperado al tratar de guardar el registro");
                   }
 
               }

               LimpiarCampos();
               rgAgrupador.Rebind();

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        #endregion

        #region ErrorManager
        private void Alerta(string mensaje)
        {
            try
            {
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje + "', 350, 150);");
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