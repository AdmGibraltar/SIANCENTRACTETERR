using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.Xml.Serialization;
using System.Text;
using System.IO;

namespace SIANWEB
{
    public partial class CapAutClienteTerr : System.Web.UI.Page
    {
        #region Variables
        public string strEmp = System.Configuration.ConfigurationManager.AppSettings["VGEmpresa"];
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }


        private object _producto;
        public object producto
        {
            get { return _producto; }
            set { _producto = value; }
        }

        private int _Accion
        {
            get
            {
                return (int)Session["SesionAccion" + Session.SessionID];
            }
            set
            {
                Session["SesionAccion" + Session.SessionID] = value;
            }

        }

        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                sesion.URL = HttpContext.Current.Request.Url.Host;
                if (sesion == null)
                {
                    CerrarVentana();
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        //Valores desde URL
                        _Accion = Convert.ToInt32(Request.QueryString["Accion"]);
                        ClienteTerritorio solicitud = new ClienteTerritorio();
                        if (_Accion == 2)
                        {
                            solicitud.Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
                            solicitud.Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                            solicitud.Id_Cte = Convert.ToInt32(Page.Request.QueryString["Id_Cte"]);
                            solicitud.Id_Ter = Convert.ToInt32(Page.Request.QueryString["Id_Ter"]);
                            solicitud.Fec_Solicitud = Convert.ToDateTime(Page.Request.QueryString["Fec_Solicitud"]);

                            _PermisoGuardar = false;
                            _PermisoModificar = false;
                            _PermisoEliminar = false;
                            _PermisoImprimir = false;

                            BtnAutorizar.Visible = true;
                            BtnRechazar.Visible = true;

                            CargarFormaCorreo(solicitud);
                        }
                        else
                        {

                            solicitud.Id_Solicitud = Convert.ToInt32(Page.Request.QueryString["Id_Solicitud"]);
                            solicitud.Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                            solicitud.Id_Cte = Convert.ToInt32(Page.Request.QueryString["Id_Cte"]);
                            solicitud.Id_Ter = Convert.ToInt32(Page.Request.QueryString["Id_Ter"]);

                            CargarForma(solicitud);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }



        protected void BtnRechazar_Click(object sender, EventArgs e)
        {
            try
            {
                Guardar("RECHAZADO");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "ImageButton1_Click");
            }
        }

        protected void BtnAutorizar_Click(object sender, EventArgs e)
        {
            try
            {
                Guardar("AUTORIZADO");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "ImageButton1_Click");
            }
        }


        private void CargarFormaCorreo(ClienteTerritorio solicitud)
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                RadEditorComentarios.ToolsFile = "BasicTools.xml";
                CN_CatCliente CN = new CN_CatCliente();
                CN.ConsultaSolicitudClienteTerrCorreo(Sesion, ref solicitud);

                //Cambios solicitados
                //Datos Generales
                txtId_Cd.Text = solicitud.Id_Cd.ToString();
                txtId_Cliente.Text = solicitud.Id_Cte.ToString();
                txtIdSolicitud.Text = solicitud.Id_Solicitud.ToString();
                txtNom_Cliente.Text = solicitud.Nom_Cliente;

                //Datos a cambiar
                txtClave.Text = solicitud.Id_Ter.ToString();
                txtNom_Territorio.Text = solicitud.Nom_Territorio;
                txtDimension.Text = solicitud.Dimension.ToString();
                txtPesos.Text = solicitud.Pesos.ToString();
                txtPotencial.Text = solicitud.Potencial.ToString();
                txtManoObra.Text = solicitud.ManodeObra.ToString();
                txtGastos.Text = solicitud.GastosTerritorio.ToString();
                txtFletes.Text = solicitud.FletesPagadoCliente.ToString();
                txtPorcentaje.Text = solicitud.Porcentaje.ToString();
                RadEditorComentarios.Content = solicitud.Comentarios;
                RadEditorComentarios.Enabled = false;

                chkNuevo.Checked = solicitud.Activo;

                if (solicitud.Activo == true)
                {
                    chkActivar.Checked = true;
                    chkDesactivar.Checked = false;
                }
                else
                {
                    chkDesactivar.Checked = true;
                    chkActivar.Checked = false;
                }
                //Ultimo cambio Autorizado
                //Datos del cambio
                txtClave1.Text = solicitud.Id_Ter.ToString();
                txtNom_Territorio1.Text = solicitud.Nom_Territorio;
                txtDimension1.Text = solicitud.Dimension1.ToString();
                txtPesos1.Text = solicitud.Pesos1.ToString();
                txtPotencial1.Text = solicitud.Potencial1.ToString();
                txtManoObra1.Text = solicitud.ManodeObra1.ToString();
                txtGastos1.Text = solicitud.GastosTerritorio1.ToString();
                txtFletes1.Text = solicitud.FletesPagadoCliente1.ToString();
                txtPorcentaje1.Text = solicitud.Porcentaje1.ToString();

                chkNuevo1.Checked = solicitud.Activo1;

                if (solicitud.Activo1 == true)
                {
                    chkActivar1.Checked = true;
                    chkDesactivar1.Checked = false;
                }
                else
                {
                    chkActivar1.Checked = false;
                    chkDesactivar1.Checked = true;
                }

                if (solicitud.Estatus != "SOLICITADO")
                {
                    BtnAutorizar.Visible = false;
                    BtnRechazar.Visible = false;
                    Alerta("Esta solicitud ya ha sido atendida.");
                }

            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private void CargarForma(ClienteTerritorio solicitud)
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                RadEditorComentarios.ToolsFile = "BasicTools.xml";
                CN_CatCliente CN = new CN_CatCliente();
                CN.ConsultaSolicitudClienteTerr(Sesion, ref solicitud);

                //Cambios solicitados
                //Datos Generales
                txtId_Cd.Text = solicitud.Id_Cd.ToString();
                txtId_Cliente.Text = solicitud.Id_Cte.ToString();
                txtIdSolicitud.Text = solicitud.Id_Solicitud.ToString();
                txtNom_Cliente.Text = solicitud.Nom_Cliente;

                //Datos a cambiar
                txtClave.Text = solicitud.Id_Ter.ToString();
                txtNom_Territorio.Text = solicitud.Nom_Territorio;
                txtDimension.Text = solicitud.Dimension.ToString();
                txtPesos.Text = solicitud.Pesos.ToString();
                txtPotencial.Text = solicitud.Potencial.ToString();
                txtManoObra.Text = solicitud.ManodeObra.ToString();
                txtGastos.Text = solicitud.GastosTerritorio.ToString();
                txtFletes.Text = solicitud.FletesPagadoCliente.ToString();
                txtPorcentaje.Text = solicitud.Porcentaje.ToString();
                RadEditorComentarios.Content = solicitud.Comentarios;
                RadEditorComentarios.Enabled = false;

                chkNuevo.Checked = solicitud.Activo;

                if (solicitud.Activo == true)
                {
                    chkActivar.Checked = true;
                    chkDesactivar.Checked = false;
                }
                else
                {
                    chkDesactivar.Checked = true;
                    chkActivar.Checked = false;
                }
                //Ultimo cambio Autorizado
                //Datos del cambio
                txtClave1.Text = solicitud.Id_Ter.ToString();
                txtNom_Territorio1.Text = solicitud.Nom_Territorio;
                txtDimension1.Text = solicitud.Dimension1.ToString();
                txtPesos1.Text = solicitud.Pesos1.ToString();
                txtPotencial1.Text = solicitud.Potencial1.ToString();
                txtManoObra1.Text = solicitud.ManodeObra1.ToString();
                txtGastos1.Text = solicitud.GastosTerritorio1.ToString();
                txtFletes1.Text = solicitud.FletesPagadoCliente1.ToString();
                txtPorcentaje1.Text = solicitud.Porcentaje1.ToString();

                chkNuevo1.Checked = solicitud.Activo1;

                if (solicitud.Activo1 == true)
                {
                    chkActivar1.Checked = true;
                    chkDesactivar1.Checked = false;
                }
                else
                {
                    chkActivar1.Checked = false;
                    chkDesactivar1.Checked = true;
                }

            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private void CerrarVentana()
        {
            try
            {
                Session["ListaRemisionesFactura" + Session.SessionID] = null;
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

        protected void RadToolBar1_ButtonClick1(object sender, RadToolBarEventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            try
            {
                //RadToolBarButton btn = e.Item as RadToolBarButton;

                //switch (btn.CommandName)
                //{
                //    case "save":
                //        Guardar();
                //        break;
                //}
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private void Guardar(string Estatus)
        {
            Sesion Sesion = new CapaEntidad.Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            XmlSerializer serializar = new XmlSerializer(typeof(ClienteTerritorio));
            try
            {
                CN_CatCliente CN = new CN_CatCliente();
                ClienteTerritorio ClienteTer = new ClienteTerritorio();
                ClienteTer.Id_Solicitud = int.Parse(txtIdSolicitud.Text);
                ClienteTer.Id_Cd = int.Parse(txtId_Cd.Text);
                ClienteTer.Id_Cte = int.Parse(txtId_Cliente.Text);
                ClienteTer.Id_Ter = int.Parse(txtClave.Text);

                CN.ConsultaSolicitudClienteTerr(Sesion, ref ClienteTer);
                ClienteTer.Estatus = Estatus;

                CN.ActualizaSolClienteTerritorio(Sesion, ClienteTer, Estatus);

                Alerta("La solicitud acaba de ser atendida correctamente");

                BtnRechazar.Visible = false;
                BtnAutorizar.Visible = false;

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

                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion

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
