using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;

namespace SIANWEB
{
    public partial class CatConfiguraciones : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
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
                    if (Page.IsPostBack == false)
                    {
                        ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RadAjaxManager1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                        }
                        CargarCentros();
                        CargarZonaHoraria(Sesion.Emp_Cnx);
                        CargarConfiguracion(Sesion);

                        RadMultiPage1.SelectedIndex = 0;

                    }

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
                // ErrorManager()
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "save")
                {
                    Guardar();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadToolBar1_ButtonClick");
            }
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {


                CN__Comun comun = new CN__Comun();
                Sesion sesion2 = (Sesion)Session["Sesion" + Session.SessionID];
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion2);
                Session["Sesion" + Session.SessionID] = sesion2;
                CargarConfiguracion(sesion2);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        #endregion

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
        #region "Subs_y_Funciones"
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
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;

                    //If Permiso.PGrabar = False Then
                    //    Me.RadToolBar1.Items(6).Enabled = False
                    //End If
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                    {
                        this.RadToolBar1.Items[5].Visible = false;
                    }
                    //If Permiso.PEliminar = False Then
                    //    Me.RadToolBar1.Items(3).Enabled = False
                    //End If
                    //If Permiso.PImprimir = False Then
                    //    Me.RadToolBar1.Items(2).Enabled = False
                    //End If

                    //Nuevo
                    this.RadToolBar1.Items[6].Visible = false;
                    //Guardar
                    //Me.RadToolBar1.Items(5).Enabled = False
                    //Regresar
                    this.RadToolBar1.Items[4].Visible = false;
                    //Eliminar
                    this.RadToolBar1.Items[3].Visible = false;
                    //Imprimir
                    this.RadToolBar1.Items[2].Visible = false;
                    //Correo
                    this.RadToolBar1.Items[1].Visible = false;
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        private void CargarZonaHoraria(string Conexion)
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Conexion, "spSysTimeZoneInfoCombo", ref this.CmbHoraZona);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarConfiguracion(Sesion Sesion)
        {
            try
            {
                ConfiguracionGlobal Configuracion = new ConfiguracionGlobal();
                Configuracion.Id_Cd = Sesion.Id_Cd_Ver;
                Configuracion.Id_Emp = Sesion.Id_Emp;
                CapaNegocios.CN_Configuracion CN_Configuracion = new CapaNegocios.CN_Configuracion();
                CN_Configuracion.Consulta(ref Configuracion, Sesion.Emp_Cnx);

                //Zona horaria
                this.CmbHoraZona.SelectedValue = Configuracion.Hora_Zona;
                this.ChkHoraVerano.Checked = Configuracion.Hora_Verano;
                //Correo
                this.TxtMailServidor.Text = Configuracion.Mail_Servidor;
                this.TxtMailPuerto.Text = Configuracion.Mail_Puerto;
                this.TxtMailUsuario.Text = Configuracion.Mail_Usuario;
                this.TxtMailContraseña.Text = Configuracion.Mail_Contraseña;
                this.TxtMailRemitente.Text = Configuracion.Mail_Remitente;
                //Acceso
                this.TxtLoginIntentos.Text = Configuracion.Login_Intentos;
                this.TxtLoginTiempoBloqueo.Text = Configuracion.Login_Tiempo_Bloqueo;
                //Contraseñas
                this.TxtContTVida.Text = Configuracion.Contraseña_Tiempo_Vida;
                this.TxtContLong.Text = Configuracion.Contraseña_Long_Min;
                //Corres de autorización
                this.TxtMailBi.Text = Configuracion.Mail_BaseInstalada;
                this.TxtMailCompLocal.Text = Configuracion.Mail_CompLocal;
                this.TxtMailPrecioEsp.Text = Configuracion.Mail_PrecioEsp;
                this.TxtMailAcys.Text = Configuracion.Mail_Acys;
                //Acuerdos Comerciales

                TxtMailValuacion.Text = Configuracion.Mail_Valuacion;
                TxtMinValuacion.Value = Configuracion.Mail_MinValuacion;
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
                if (!_PermisoModificar)
                {
                    Alerta("No tiene permisos para modificar");
                    return;
                }

                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ConfiguracionGlobal Configuracion = new ConfiguracionGlobal();

                Configuracion.Id_Cd = Sesion.Id_Cd_Ver;
                Configuracion.Id_Emp = Sesion.Id_Emp;
                //Zona horaria
                Configuracion.Hora_Zona = this.CmbHoraZona.SelectedValue;
                Configuracion.Hora_Verano = Convert.ToBoolean(Convert.ToInt32(this.ChkHoraVerano.Checked) * -1);
                //Correo
                Configuracion.Mail_Servidor = this.TxtMailServidor.Text;
                Configuracion.Mail_Puerto = this.TxtMailPuerto.Text;
                Configuracion.Mail_Usuario = this.TxtMailUsuario.Text;
                Configuracion.Mail_Contraseña = this.TxtMailContraseña.Text;
                Configuracion.Mail_Remitente = this.TxtMailRemitente.Text;
                //Acceso
                Configuracion.Login_Intentos = this.TxtLoginIntentos.Text;
                Configuracion.Login_Tiempo_Bloqueo = this.TxtLoginTiempoBloqueo.Text;
                //Contraseñas
                Configuracion.Contraseña_Tiempo_Vida = this.TxtContTVida.Text;
                Configuracion.Contraseña_Long_Min = this.TxtContLong.Text;
                //Correos autorizacion
                Configuracion.Mail_CompLocal = this.TxtMailCompLocal.Text;
                Configuracion.Mail_PrecioEsp = this.TxtMailPrecioEsp.Text;
                Configuracion.Mail_BaseInstalada = this.TxtMailBi.Text;
                Configuracion.Mail_Valuacion = TxtMailValuacion.Text;
                Configuracion.Mail_MinValuacion = TxtMinValuacion.Value.HasValue ? TxtMinValuacion.Value.Value : 0;
                Configuracion.Mail_Acys = TxtMailAcys.Text;

                CapaNegocios.CN_Configuracion CN_Configuracion = new CapaNegocios.CN_Configuracion();
                CN_Configuracion.Modificar(ref Configuracion, Sesion.Emp_Cnx);

                Alerta("Los cambios se guardaron correctamente");

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
        #endregion
    }
}