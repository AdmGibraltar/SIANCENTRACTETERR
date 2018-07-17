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
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Collections;

namespace SIANWEB
{
    public partial class CapAutClienteTerr_Lista : System.Web.UI.Page
    {
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

        public List<ClienteTerritorio> lstSolicitud
        {
            get { return (List<ClienteTerritorio>)Session["lstSolicitud" + Session.SessionID]; }
            set { Session["lstSolicitud" + Session.SessionID] = value; }
        }

        public DataTable objdtListaSolicitudes { get; set; }

        protected DataTable objdtTablaSolicitudes { get { if (ViewState["objdtTablaSolicitudes"] != null) { return (DataTable)ViewState["objdtTablaSolicitudes"]; } else { return objdtListaSolicitudes; } } set { ViewState["objdtTablaSolicitudes"] = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        InicializarTablaSolicitudes();
                        GetList();
                    }
                }
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
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

                //if (Permiso.PAccesar == true)
                //{
                //    _PermisoGuardar = Permiso.PGrabar;
                //    _PermisoModificar = Permiso.PModificar;
                //    _PermisoEliminar = Permiso.PEliminar;
                //    _PermisoImprimir = Permiso.PImprimir;

                //}
                //else
                //{
                //    Response.Redirect("Inicio.aspx");
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InicializarTablaSolicitudes()
        {
            try
            {
                objdtListaSolicitudes = new DataTable();
                objdtTablaSolicitudes.Columns.Add("Id_Solicitud");
                objdtTablaSolicitudes.Columns.Add("Id_Cd");
                objdtTablaSolicitudes.Columns.Add("Nom_Sucursal");
                objdtTablaSolicitudes.Columns.Add("Id_Cte");
                objdtTablaSolicitudes.Columns.Add("Nom_Cliente");
                objdtTablaSolicitudes.Columns.Add("Id_Ter");
                objdtTablaSolicitudes.Columns.Add("Nom_Territorio");
                objdtTablaSolicitudes.Columns.Add("Activo");
                objdtTablaSolicitudes.Columns.Add("Nuevo");
                objdtTablaSolicitudes.Columns.Add("Estatus");
                objdtTablaSolicitudes.Columns.Add("Comentarios");
                objdtTablaSolicitudes.Columns.Add("Fec_Solicitud");
                objdtTablaSolicitudes.Columns.Add("Fec_Atendido");


                objdtTablaSolicitudes = objdtListaSolicitudes;
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }

        }

        private void GetList()
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<ClienteTerritorio> lstSolicitud = new List<ClienteTerritorio>();
                CN_CatCliente CN = new CN_CatCliente();
                rgPendientes.DataSource = null;
                rgAutorizados.DataSource = null;
                rgRechazados.DataSource = null;

                objdtTablaSolicitudes.Rows.Clear();
                CN.ConsultaSolicitudesClienteTerr(Sesion, ref lstSolicitud);
                CargarSolicitudes(lstSolicitud);

                rgPendientes.DataSource = objdtTablaSolicitudes.Select("Estatus = '" + "Solicitado" + "'");
                rgAutorizados.DataSource = objdtTablaSolicitudes.Select("Estatus = '" + "Autorizado" + "'");
                rgRechazados.DataSource = objdtTablaSolicitudes.Select("Estatus = '" + "Rechazado" + "'");
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private void CargarSolicitudes(List<ClienteTerritorio> lstSolicitud)
        {
            try
            {
                foreach (ClienteTerritorio sol in lstSolicitud)
                {
                    ArrayList ArraySolicitudes = new ArrayList();
                    ArraySolicitudes.Add(sol.Id_Solicitud);
                    ArraySolicitudes.Add(sol.Id_Cd);
                    ArraySolicitudes.Add(sol.Nom_Sucursal);
                    ArraySolicitudes.Add(sol.Id_Cte);
                    ArraySolicitudes.Add(sol.Nom_Cliente);
                    ArraySolicitudes.Add(sol.Id_Ter);
                    ArraySolicitudes.Add(sol.Nom_Territorio);
                    ArraySolicitudes.Add(sol.Activo);
                    ArraySolicitudes.Add(sol.Nuevo);
                    ArraySolicitudes.Add(sol.Estatus);
                    ArraySolicitudes.Add(sol.Comentarios);
                    ArraySolicitudes.Add(Convert.ToDateTime(sol.Fec_Solicitud).ToShortDateString());
                    ArraySolicitudes.Add(Convert.ToDateTime(sol.Fec_Atendida).ToShortDateString());

                    objdtTablaSolicitudes.Rows.Add(ArraySolicitudes.ToArray());
                }
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        protected void CmbSolicitud_TextChanged(object sender, EventArgs e)
        {

        }

        protected void rgPendientes_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {  //Llenar Grid
                    GetList();
                    rgPendientes.Rebind();
                    rgAutorizados.Rebind();
                    rgRechazados.Rebind();

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void rgPendientes_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            try
            {
                rgPendientes.DataSource = objdtTablaSolicitudes.Select("Estatus = '" + "Solicitado" + "'");
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        protected void rgAutorizados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {  //Llenar Grid
                    GetList();
                    rgPendientes.Rebind();
                    rgAutorizados.Rebind();
                    rgRechazados.Rebind();

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void rgAutorizados_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            try
            {
                rgAutorizados.DataSource = objdtTablaSolicitudes.Select("Estatus = '" + "Autorizado" + "'");
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        protected void rgRechazados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {  //Llenar Grid
                    GetList();
                    rgPendientes.Rebind();
                    rgAutorizados.Rebind();
                    rgRechazados.Rebind();
                }
            }
            catch
            {
                // Alerta("Error, " + ex.Message);
            }
        }

        protected void rgRechazados_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            try
            {
                rgRechazados.DataSource = objdtTablaSolicitudes.Select("Estatus = '" + "Rechazado" + "'");
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        protected void rgPendientes_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Int32 item = default(Int32);
                if (e.Item == null) return;
                item = e.Item.ItemIndex;

                if (item >= 0)
                {
                    ClienteTerritorio ClienteTer = new ClienteTerritorio();
                    ClienteTer.Id_Solicitud = Convert.ToInt32(rgPendientes.Items[item]["Id_Solicitud"].Text);
                    ClienteTer.Id_Cd = Convert.ToInt32(rgPendientes.Items[item]["Id_Cd"].Text);
                    ClienteTer.Id_Cte = Convert.ToInt32(rgPendientes.Items[item]["Id_Cte"].Text);
                    ClienteTer.Id_Ter = Convert.ToInt32(rgPendientes.Items[item]["Id_Ter"].Text);

                    switch (e.CommandName.ToString())
                    {
                        case "Detalle":
                            RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_Solicitud('", ClienteTer.Id_Solicitud, "','", ClienteTer.Id_Cd, "','", ClienteTer.Id_Cte, "','", ClienteTer.Id_Ter, "')"));
                            break;
                        case "Autorizar":
                            Guardar(ClienteTer, "AUTORIZADO");
                            break;
                        case "Rechazar":
                            Guardar(ClienteTer, "RECHAZADO");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        protected void rgAutorizados_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Int32 item = default(Int32);
                if (e.Item == null) return;
                item = e.Item.ItemIndex;

                if (item >= 0)
                {
                    ClienteTerritorio ClienteTer = new ClienteTerritorio();
                    ClienteTer.Id_Solicitud = Convert.ToInt32(rgAutorizados.Items[item]["Id_Solicitud"].Text);
                    ClienteTer.Id_Cd = Convert.ToInt32(rgAutorizados.Items[item]["Id_Cd"].Text);
                    ClienteTer.Id_Cte = Convert.ToInt32(rgAutorizados.Items[item]["Id_Cte"].Text);
                    ClienteTer.Id_Ter = Convert.ToInt32(rgAutorizados.Items[item]["Id_Ter"].Text);

                    switch (e.CommandName.ToString())
                    {
                        case "Detalle":
                            RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_Solicitud('", ClienteTer.Id_Solicitud, "','", ClienteTer.Id_Cd, "','", ClienteTer.Id_Cte, "','", ClienteTer.Id_Ter, "')"));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        protected void rgRechazados_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Int32 item = default(Int32);
                if (e.Item == null) return;
                item = e.Item.ItemIndex;

                if (item >= 0)
                {
                    ClienteTerritorio ClienteTer = new ClienteTerritorio();
                    ClienteTer.Id_Solicitud = Convert.ToInt32(rgRechazados.Items[item]["Id_Solicitud"].Text);
                    ClienteTer.Id_Cd = Convert.ToInt32(rgRechazados.Items[item]["Id_Cd"].Text);
                    ClienteTer.Id_Cte = Convert.ToInt32(rgRechazados.Items[item]["Id_Cte"].Text);
                    ClienteTer.Id_Ter = Convert.ToInt32(rgRechazados.Items[item]["Id_Ter"].Text);

                    switch (e.CommandName.ToString())
                    {
                        case "Detalle":
                            RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_Solicitud('", ClienteTer.Id_Solicitud, "','", ClienteTer.Id_Cd, "','", ClienteTer.Id_Cte, "','", ClienteTer.Id_Ter, "')"));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }


        private void Guardar(ClienteTerritorio ClienteTer, string Estatus)
        {
            Sesion Sesion = new CapaEntidad.Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            int Respuesta = 0;
            XmlSerializer serializar = new XmlSerializer(typeof(ClienteTerritorio));
            try
            {
                CN_CatCliente CN = new CN_CatCliente();
                CN.ConsultaSolicitudClienteTerr(Sesion, ref ClienteTer);
                ClienteTer.Estatus = Estatus;

                CN.ActualizaSolClienteTerritorio(Sesion, ClienteTer, Estatus, ref Respuesta);
                if (Respuesta == 1)
                {
                    CN.ConsultaSucursal(Sesion, ref ClienteTer);
                    #region Crear XML y consumir WsTerritorios

                    StringBuilder sb = new StringBuilder();
                    TextWriter tw = new StringWriter(sb);
                    serializar.Serialize(tw, ClienteTer);
                    tw.Close();
                    string xmlClienteTer = sb.ToString();
                    #endregion

                    #region Llamar a webService

                    wsClienteTerritorio.Service1 ws = new wsClienteTerritorio.Service1();
                    ws.ActualizaAutClienteTerritorio(xmlClienteTer);
                    RAM1.ResponseScripts.Add("CloseAlert('La solicitud fue atendida correctamente.');");
                }
                else
                {
                    RAM1.ResponseScripts.Add("CloseAlert('Error al intentar guardar el registro, favor  de intentar de nuevo.');");
                }

                    #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        GetList();
                        rgPendientes.Rebind();
                        rgAutorizados.Rebind();
                        rgRechazados.Rebind();

                        break;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region ErrorManager
        private void AlertaFocus(string mensaje, string rtb)
        {
            try
            {
                RAM1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void AlertaFocus2(string mensaje, string rtb)
        {
            try
            {
                RAM1.ResponseScripts.Add("AlertaFocus2('" + mensaje + "','" + rtb + "');");
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
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 340, 150);");
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
        #endregion
    }
}