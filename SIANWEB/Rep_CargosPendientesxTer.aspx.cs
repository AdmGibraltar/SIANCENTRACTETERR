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
    public partial class Rep_CargosPendientesxTer : System.Web.UI.Page
    {
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
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
                        ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        Inicializar();

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;
                switch (btn.CommandName)
                {
                    case "Mostrar":
                        if (Page.IsValid)
                        {
                            this.Mostrar(true);//true
                        }
                        break;
                    case "excel":
                        if (Page.IsValid)
                        {
                            this.Mostrar(false);
                        }
                        break;
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
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion
        #region Funciones
        private void Inicializar()
        {
            try
            {
                CargarCentros();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CmbCentro.Items.Clear();

                if (Sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);
                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = Sesion.Id_U;
                Permiso.Id_Cd = Sesion.Id_Cd_Ver;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                //Imprimir
                if (!Permiso.PImprimir)
                    this.RadToolBar1.Items[0].Visible = false;

                _PermisoImprimir = Permiso.PImprimir;
                if (Permiso.PAccesar == true) //if(Sesion != null)
                {
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void Mostrar(bool a_pantalla)
        {
            try
            {
                #region captura de Variables y sesion
                int error = 0;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string strCliente = string.Empty;
                string strTerritorio = string.Empty;
                int agrupador = 0; //1- cliente, 2- territorio
                if (RBCliente.Checked)
                {
                    agrupador = 1;
                }
                if (RBTerritorio.Checked)
                {
                    agrupador = 2;
                }

                if (!string.IsNullOrEmpty(txtCte.Text))
                {
                    boton(txtCte.Text, ref error);
                    strCliente = txtCte.Text;
                }
                else
                    strCliente = "Todos";
                if (!string.IsNullOrEmpty(txtTerritorio.Text))
                {
                    boton(txtTerritorio.Text, ref error);
                    strTerritorio = txtTerritorio.Text;
                }
                else
                {
                    strTerritorio = "Todos";
                }

                if (_PermisoImprimir)
                {
                    if (error == 0)
                    {
                        ArrayList ALValorParametrosInternos = new ArrayList();
                        string nombreEmpresa = sesion.Emp_Nombre;
                        string nombreSucursal = sesion.Cd_Nombre;
                        DateTime Fechalocal = DateTime.Now;
                #endregion
                        //datos de filtros                             
                        ALValorParametrosInternos.Add(strCliente);
                        ALValorParametrosInternos.Add(strTerritorio);
                        ALValorParametrosInternos.Add(diasIni.Value);
                        ALValorParametrosInternos.Add(DiasFin.Value);
                        ALValorParametrosInternos.Add(agrupador);

                        Type instance = null;
                        if (a_pantalla)
                        {
                            if (agrupador == 1)//cliente
                                instance = typeof(LibreriaReportes.Rep_CargosPdtesxTer1);
                            else//territorio
                                instance = typeof(LibreriaReportes.Rep_CargosPdtesxTer);
                        }
                        else
                        {
                            if (agrupador == 1)//cliente
                                instance = typeof(LibreriaReportes.ExpRep_CargosPdtesxTer1);
                            else//territorio
                                instance = typeof(LibreriaReportes.Rep_CargosPdtesxTer2);
                        }                            

                        //parametros para el cuerpo del reporte
                        ALValorParametrosInternos.Add(sesion.Id_Emp);
                        ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                        ALValorParametrosInternos.Add(sesion.U_Nombre);
                        ALValorParametrosInternos.Add(Fechalocal);
                        ALValorParametrosInternos.Add(nombreEmpresa);
                        ALValorParametrosInternos.Add(nombreSucursal);
                        //conexion
                        ALValorParametrosInternos.Add(sesion.Emp_Cnx);

                        //NOTA: El estatus de impresión, lo pone cuando el reporte se carga    
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
                else
                {
                    Alerta("No tiene permiso para imprimir");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void boton(string cadena, ref int error)
        {
            try
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
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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