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
    public partial class Rep_Inventariosdiarios : System.Web.UI.Page
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

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                txtFecha.Text = Convert.ToString(DateTime.Now);

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

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                        this.TblEncabezado.Visible = false;

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

        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (Page.IsValid)
                {
                    if (btn.CommandName == "excel")
                    {
                        this.GenerarExcel();
                        Alerta("Se genero el archivo correctamente");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        
        protected void CmbCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
            }
            CN__Comun comun = new CN__Comun();
        }

        #endregion Eventos

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
                    _PermisoImprimir = Permiso.PImprimir;
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
        

        private void GenerarExcel()
        {
                StringBuilder tabla = new StringBuilder();
                Funcion fn = new Funcion();
                tabla.Append("<html><head><meta http-equiv='Content-Type' content='text/html; charset=ISO-8859-1'></head><body><table style='width:1200px'>");
                EscribeDetalle(ref tabla);
                tabla.Append("</table></body></html>");
                fn.ExportarExcel("RepInvDiarios_" + DateTime.Now , tabla.ToString());     
        }


        private void EscribeEncabezado(ref StringBuilder Tabla)
        {
            try
            {

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Tabla.Append("<tr>");
                Tabla.Append("<th  style='width:50px; text-align:Left; font-weight:bold'>CDI</th>");
                Tabla.Append("<th  style='width:60px; text-align:Left; font-weight:bold'> SKU </td>");
                Tabla.Append("<th  style='width:70px; text-align:Left; font-weight:bold'>Inventario</td>");
                Tabla.Append("<th  style='width:70px; text-align:Left; font-weight:bold'>BO + Trans</td>");
                Tabla.Append("<th  style='width:70px; text-align:Left; font-weight:bold'>BackOrder</td>");
                Tabla.Append("<th  style='width:60px; text-align:Left; font-weight:bold'>Transito</td>");
                Tabla.Append("</tr>");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void EscribeDetalle(ref StringBuilder Tabla)
        {
            try
            {
                List<InventarioDiario> List = new List<InventarioDiario>();
                List = GetList();
                DataTable dt = new DataTable();
                dt = Funcion.Convertidor<InventarioDiario>.ListaToDatatable(List);
                EscribeEncabezado(ref Tabla);
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    Tabla.Append("<tr>");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (dt.Columns[i].ColumnName == "CDI1")
                        {
                            Tabla.Append("<td style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        if (dt.Columns[i].ColumnName == "SKU1")
                        {
                            Tabla.Append("<td style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        if (dt.Columns[i].ColumnName == "Cantidad1")
                        {
                            Tabla.Append("<td style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }

                        if (dt.Columns[i].ColumnName == "Total1")
                        {
                            Tabla.Append("<td style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        if (dt.Columns[i].ColumnName == "BackOrder1")
                        {
                            Tabla.Append("<td style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        if (dt.Columns[i].ColumnName == "Transito1")
                        {
                            Tabla.Append("<td style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                    }
                    Tabla.Append("</tr>");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private List<InventarioDiario> GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<InventarioDiario> lisInventarios = new List<InventarioDiario>();
                InventarioDiario semanal = new InventarioDiario();

                string cnn = System.Configuration.ConfigurationManager.AppSettings["Solmex"].ToString();
                new CN_InvExcesoInventario().ConsultaInventariosDiarios(cnn, ref lisInventarios);
                return lisInventarios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
#endregion

        #region Errores
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