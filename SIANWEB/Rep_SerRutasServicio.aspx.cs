using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaDatos;
using CapaNegocios;
using System.IO;
using System.Runtime.Remoting;
using System.Text;
using Telerik.Reporting.Processing;

namespace SIANWEB
{
    public partial class Rep_SerRutasServicio : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        #endregion
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
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        CargarCentros();
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();


                        txtFechaini.DbSelectedDate = Sesion.CalendarioIni;
                        txtFechafin.DbSelectedDate = Sesion.CalendarioFin;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbCentrosDist_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);

                txtFechaini.DbSelectedDate = sesion.CalendarioIni;
                txtFechafin.DbSelectedDate = sesion.CalendarioFin;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (_PermisoImprimir)
                    switch (btn.CommandName)
                    {
                        case "Mostrar":
                            Mostrar(true);
                            break;
                        case "excel":
                            Mostrar(false);
                            break;
                    }
                else
                    Alerta("No tiene permiso para imprimir");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
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
                }
                else
                    Response.Redirect("Inicio.aspx");
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
        private void Mostrar(bool a_pantalla)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            SerRutaServicio rutaServicio = new SerRutaServicio();
            int error = 0;

            rutaServicio.Id_Cd = sesion.Id_Cd_Ver;
            if (string.IsNullOrEmpty(txtFechaini.SelectedDate.Value.ToString()))
                error = 1;
            if (string.IsNullOrEmpty(txtFechafin.SelectedDate.Value.ToString()))
                error = 1;
            if (txtFechaini.SelectedDate > txtFechafin.SelectedDate)
            {
                Alerta("La fecha inicial no debe ser mayor a la fecha final");
                return;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtFechaini.SelectedDate.ToString()))
                {
                    rutaServicio.FechaInicial = txtFechaini.SelectedDate.Value;
                    rutaServicio.SfechaInicial = txtFechaini.SelectedDate.Value.ToString("dd/MM/yyy");
                }
                if (!string.IsNullOrEmpty(txtFechafin.SelectedDate.ToString()))
                {
                    rutaServicio.FechaFinal = txtFechafin.SelectedDate.Value;
                    rutaServicio.SfechaFinal = txtFechafin.SelectedDate.Value.ToString("dd/MM/yyy");
                }
                else
                    rutaServicio.SfechaFinal = "Actual";
            }
            if (!string.IsNullOrEmpty(txtRuta.Text))
            {
                boton(txtRuta.Text, ref error);
                rutaServicio.Ruta = txtRuta.Text;
                rutaServicio.Sruta = txtRuta.Text;
            }
            else
                rutaServicio.Sruta = "Todas";

            if (!string.IsNullOrEmpty(txtTerr.Text))
            {
                boton(txtTerr.Text, ref error);
                rutaServicio.Territorio = txtTerr.Text;
                rutaServicio.Sterritorio = txtTerr.Text;
            }
            else
                rutaServicio.Sterritorio = "Todos";

            if (!string.IsNullOrEmpty(txtCliente.Text))
            {
                boton(txtCliente.Text, ref error);
                rutaServicio.Cliente = txtCliente.Text;
                rutaServicio.Scliente = txtCliente.Text;
            }
            else
                rutaServicio.Scliente = "Todos";

            rutaServicio.Orden = Convert.ToInt32(cmbOrden.SelectedValue);
            rutaServicio.Sorden = cmbOrden.SelectedItem.Text;
            rutaServicio.Detalle = chkDetalle.Checked;
            if (rutaServicio.Detalle)
                rutaServicio.Reporte1 = 1;
            else
                rutaServicio.Reporte1 = 2;

            ArrayList ALValorParametrosInternos = new ArrayList();

            CN_SerRutaServicio serRuta = new CN_SerRutaServicio();
            string nombreEmpresa = sesion.Emp_Nombre;
            string nombreSucursal = sesion.Cd_Nombre;

            DateTime Fechalocal = DateTime.Now;

            //datos de filtros
            ALValorParametrosInternos.Add(rutaServicio.FechaInicial);
            ALValorParametrosInternos.Add(rutaServicio.FechaFinal);
            ALValorParametrosInternos.Add(rutaServicio.Ruta);
            ALValorParametrosInternos.Add(rutaServicio.Sruta);
            ALValorParametrosInternos.Add(rutaServicio.Territorio);
            ALValorParametrosInternos.Add(rutaServicio.Sterritorio);
            ALValorParametrosInternos.Add(rutaServicio.Cliente);
            ALValorParametrosInternos.Add(rutaServicio.Scliente);
            ALValorParametrosInternos.Add(rutaServicio.Orden);
            ALValorParametrosInternos.Add(rutaServicio.Sorden);
            ALValorParametrosInternos.Add(rutaServicio.SfechaInicial);
            ALValorParametrosInternos.Add(rutaServicio.SfechaFinal);
            ALValorParametrosInternos.Add(rutaServicio.Reporte1);
            //parametros para el cuerpo del reporte
            ALValorParametrosInternos.Add(sesion.Id_Emp);
            ALValorParametrosInternos.Add(rutaServicio.Id_Cd);
            ALValorParametrosInternos.Add(sesion.U_Nombre);
            ALValorParametrosInternos.Add(Fechalocal);
            ALValorParametrosInternos.Add(nombreEmpresa);
            ALValorParametrosInternos.Add(nombreSucursal);
            //conexion
            ALValorParametrosInternos.Add(sesion.Emp_Cnx);
            Type instance = null;
            if (rutaServicio.Detalle)
            {//formato1
                if (a_pantalla)
                {
                    instance = typeof(LibreriaReportes.Rep_SerRutaServicio1);
                }
                else
                {
                    instance = typeof(LibreriaReportes.ExpRep_SerRutaServicio1);
                }
            }
            else
            {//formato2
                if (a_pantalla)
                {
                    instance = typeof(LibreriaReportes.Rep_SerRutaServicio2);
                }
                else
                {
                    instance = typeof(LibreriaReportes.ExpRep_SerRutaServicio2);
                }
            }
            //NOTA: El estatus de impresión, lo pone cuando el reporte se carga

            if (error == 0)
            {

                if (a_pantalla)
                {
                    Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = null;
                    Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                    Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;
                    RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
                }
                else
                {
                    ImprimirXLS(ALValorParametrosInternos, instance);
                }
            }
        }
        private void ImprimirXLS(ArrayList ALValorParametrosInternos, Type instance)
        {
            try
            {
                Telerik.Reporting.Report report1 = (Telerik.Reporting.Report)Activator.CreateInstance(instance);
                for (int i = 0; i <= ALValorParametrosInternos.Count - 1; i++)
                {
                    report1.ReportParameters[i].Value = ALValorParametrosInternos[i];
                }
                ReportProcessor reportProcessor = new ReportProcessor();
                RenderingResult result = reportProcessor.RenderReport("XLS", report1, null);
                string ruta = Server.MapPath("Reportes") + "\\" + instance.Name + ".xls";
                if (File.Exists(ruta))
                    File.Delete(ruta);
                FileStream fs = new FileStream(ruta, FileMode.Create);
                fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                 
                fs.Flush();
                fs.Close();

                RAM1.ResponseScripts.Add("startDownload('" + instance.Name + ".xls');");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void boton(string cadena, ref int error)
        {
            if (!string.IsNullOrEmpty(cadena))
            {
                string[] split = cadena.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string[] split2;
                foreach (string a in split)
                {
                    if (a.Contains("-"))
                    {
                        split2 = a.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                        if (split2.Length != 2)
                        {
                            Alerta("El rango " + a.ToString() + " no es válido");
                            error = 1;
                        }
                        if (split2.Length == 2)
                            if (Convert.ToInt32(split2[0]) > Convert.ToInt32(split2[1]))
                            {
                                Alerta("El rango " + a.ToString() + " no es válido");
                                error = 1;
                            }
                    }
                }
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