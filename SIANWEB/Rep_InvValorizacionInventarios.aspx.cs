﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Collections;
using Telerik.Reporting.Processing;
using System.IO;

namespace SIANWEB
{
    public partial class Rep_InvValorizacionInventarios : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public int PermisoGuardar { get { return _PermisoGuardar == true ? 1 : 0; } }
        public int PermisoModificar { get { return _PermisoModificar == true ? 1 : 0; } }
        public int PermisoEliminar { get { return _PermisoEliminar == true ? 1 : 0; } }
        public int PermisoImprimir { get { return _PermisoImprimir == true ? 1 : 0; } }
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
                    Response.Redirect("login.aspx", false);
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
                        this.Inicializar();
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
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
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            string mensajeError = string.Empty;
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;
                switch (btn.CommandName)
                {
                    case "print":
                        mensajeError = "Impresion_error";
                        this.Imprimir(true);
                        break;
                    case "excel":
                        mensajeError = "Impresion_error";
                        this.Imprimir(false);
                        break;
                    case "DwExcel":
                        List<Producto> List = new List<Producto>();
                        CN_Rep_InvValorizacionInventario capaNegocios = new CN_Rep_InvValorizacionInventario();
                        Sesion session2 = (Sesion)Session["Sesion" + Session.SessionID];
                        ValorizacionInventario valorizacion = new ValorizacionInventario();
                        valorizacion.Id_Emp = session2.Id_Emp;
                        valorizacion.Id_Cd = session2.Id_Cd_Ver;
                        valorizacion.Id_PrdStr = txtProducto.Text;
                        valorizacion.Id_Ptp = cmbTipoProducto.SelectedValue == "-1" ? null : cmbTipoProducto.SelectedValue;
                        valorizacion.Id_Spo = cmbPropietarios.SelectedValue == "-1" ? null : cmbPropietarios.SelectedValue;
                        valorizacion.TipoPrecioStr = cmbPrecio.SelectedValue == "-- Seleccionar --" ? null : cmbPropietarios.SelectedValue;                      
                        valorizacion.Orden = rbOrdenar.SelectedItem.Value;

                        capaNegocios.ConsultaValorizacion(valorizacion, session2.Emp_Cnx, ref List);
                        string ruta = Server.MapPath("Reportes") + "\\ValorizacionInventario.txt";
                        StreamWriter sw = new StreamWriter(ruta, false, Encoding.UTF8);
                        sw.WriteLine("<html>");
                        sw.WriteLine("<head>");
                        sw.WriteLine("</head>");
                        sw.WriteLine("<body>");
                        sw.WriteLine("<table border=1><font size=8pt>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Núm. Producto</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Descripción</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Presentación</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Unidades</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Inventario final</td>");
                        sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>Físico</td>");
                        sw.WriteLine("</tr>");

                        foreach (Producto p in List)
                        {
                            sw.WriteLine("<tr>");
                            sw.WriteLine("<td>" + p.Id_Prd + "</td>");
                            sw.WriteLine("<td>" + p.Prd_Descripcion + "</td>");
                            sw.WriteLine("<td>" + p.Prd_Presentacion + "</td>");
                            sw.WriteLine("<td>" + p.Prd_UniNs + "</td>");
                            sw.WriteLine("<td>" + p.Prd_InvFinal + "</td>");
                            sw.WriteLine("<td>" + p.Prd_Fisico + "</td>");
                            sw.WriteLine("</tr>");
                        }
                        sw.WriteLine("</font></table>");
                        sw.WriteLine("</body>");
                        sw.WriteLine("</html>");
                        sw.Close();
                        if (File.Exists(ruta))
                        {
                            string ruta2 = null;
                            ruta2 = Server.MapPath("Reportes") + "\\ValorizacionInventario.xls";
                            if (File.Exists(ruta2))
                            {
                                File.Delete(ruta2);
                            }
                            File.Move(ruta, Server.MapPath("Reportes") + "\\ValorizacionInventario.xls");
                            Response.Redirect("Reportes\\ValorizacionInventario.xls", false);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, mensajeError);
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        protected void cmbCentrosDist_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
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
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Cmb_CentroDistribucion_IndexChanging_error"));
            }
        }
        #endregion
        #region Funciones
        private void Imprimir(bool a_pantalla)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();
                //Consulta centro de distribución
                CentroDistribucion Cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                ALValorParametrosInternos.Add(sesion.Id_Emp);
                ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                ALValorParametrosInternos.Add(txtProducto.Text.Trim() == string.Empty ? "Todos" : txtProducto.Text.Trim());
                ALValorParametrosInternos.Add(txtProducto.Text);
                ALValorParametrosInternos.Add(sesion.Emp_Nombre);
                ALValorParametrosInternos.Add(sesion.Cd_Nombre);
                ALValorParametrosInternos.Add(sesion.U_Nombre);
                ALValorParametrosInternos.Add(string.Concat(DateTime.Now.ToString("dd/MM/yyyy"), " ", DateTime.Now.ToString("t")));
                ALValorParametrosInternos.Add(cmbTipoProducto.SelectedValue == "-1" ? null : cmbTipoProducto.SelectedValue);
                ALValorParametrosInternos.Add(cmbTipoProducto.SelectedValue == "-1" ? "Todos" : cmbTipoProducto.SelectedItem.Text);
                ALValorParametrosInternos.Add((cmbPropietarios.SelectedValue == "-1" || cmbPropietarios.SelectedValue == "Todos") ? (object)null : cmbPropietarios.SelectedValue);
                ALValorParametrosInternos.Add((cmbPropietarios.SelectedValue == "-1" || cmbPropietarios.SelectedValue == "Todos") ? "Todos" : cmbPropietarios.SelectedItem.Text);
                ALValorParametrosInternos.Add(cmbPrecio.SelectedValue);
                ALValorParametrosInternos.Add(cmbPrecio.SelectedItem.Text);
                ALValorParametrosInternos.Add(rbOrdenar.SelectedItem.Value);
                ALValorParametrosInternos.Add(rbOrdenar.SelectedItem.Text);
                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                Type instance = null;
                if (a_pantalla)
                {
                    instance = typeof(LibreriaReportes.Rep_InvValorizacionInventarios);
                }
                else
                {
                    instance = typeof(LibreriaReportes.ExpRep_InvValorizacionInventarios);
                }
                //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                if (_PermisoImprimir)
                {
                    if (a_pantalla)
                    {
                        Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                        Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;
                        RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
                    }
                    else
                    {
                        ImprimirXLS(ALValorParametrosInternos, instance);
                    }
                }
                else
                    Alerta("No tiene permiso para imprimir");
            }
            catch (Exception ex)
            {
                throw ex;
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
        private void Inicializar()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            this.CargarCentros();
            this.LlenarComboProductoTipo();
            this.LlenarComboSisPropietario();
        }
        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];

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
        private void LlenarComboProductoTipo()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatTipoProducto_Combo4", ref cmbTipoProducto);
        }
        private void LlenarComboSisPropietario()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(0, sesion.Id_Emp, sesion.Emp_Cnx, "spCatSisPropietarios_Combo", ref cmbPropietarios);
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