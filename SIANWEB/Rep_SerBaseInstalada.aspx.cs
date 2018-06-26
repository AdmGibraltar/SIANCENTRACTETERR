using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Collections;
using Telerik.Reporting.Processing;
using System.IO;

namespace SIANWEB
{
    public partial class Rep_SerBaseInstalada : System.Web.UI.Page
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
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        llenarCombo();
                        CargarCentros();
                        if (!Sesion.Cu_Modif_Pass_Voluntario)
                            RadAjaxManager1.ResponseScripts.Add("(function(){var f = function(){AbrirContrasenas(); return false;Sys.Application.remove_load(f);};Sys.Application.add_load(f);})()");
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                    }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Page_Load");
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
                    case "print":
                        Abrir_Reporte(true);
                        break;
                    case "excel":
                        Abrir_Reporte(false);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            Sesion.Id_Cd_Ver = Convert.ToInt32(CmbCentro.SelectedItem.Value);
        }
        #endregion

        #region Metodos
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
        private void Abrir_Reporte(bool a_pantalla)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();

                CN__Comun cn_comun = new CN__Comun();
                string resp = cn_comun.ValidarRango(txtGrupo.Text);
                if (resp != "")
                {
                    Alerta("El rango " + resp + ", en el campo \"Grupo\" no es válido");
                    return;
                }
                resp = cn_comun.ValidarRango(txtTerritorio.Text);
                if (resp != "")
                {
                    Alerta("El rango " + resp + ", en el campo \"Territorio\" no es válido");
                    return;
                }
                resp = cn_comun.ValidarRango(txtCliente.Text);
                if (resp != "")
                {
                    Alerta("El rango " + resp + ", en el campo \"Cliente\" no es válido");
                    return;
                }
                resp = cn_comun.ValidarRango(txtEquipo.Text);
                if (resp != "")
                {
                    Alerta("El rango " + resp + ", en el campo \"Equipos\" no es válido");
                    return;
                }
                resp = cn_comun.ValidarRango(txtRepresentante.Text);
                if (resp != "")
                {
                    Alerta("El rango " + resp + ", en el campo \"Representante\" no es válido");
                    return;
                }
                //Consulta centro de distribución
                string Emp_Nombre = "";
                string Cd_Nombre = "";
                string U_Nombre = "";
                new CN_CapPedido().ConsultarEncabezado_RepFacPedidosPendientes(sesion, ref Emp_Nombre, ref Cd_Nombre, ref U_Nombre);
                //parametros cabecera       
                ALValorParametrosInternos.Add(txtGrupo.Text == "" ? "Todos" : txtGrupo.Text);
                ALValorParametrosInternos.Add(txtTerritorio.Text == "" ? "Todos" : txtTerritorio.Text);//Territorio
                ALValorParametrosInternos.Add(txtCliente.Text == "" ? "Todos" : txtCliente.Text);//Cliente
                ALValorParametrosInternos.Add(txtEquipo.Text == "" ? "Todos" : txtEquipo.Text);
                ALValorParametrosInternos.Add(txtRepresentante.Text == "" ? "Todos" : txtRepresentante.Text);
                ALValorParametrosInternos.Add(cmbAño.SelectedValue);
                ALValorParametrosInternos.Add(rblDetallado.SelectedItem.Text);
                ALValorParametrosInternos.Add(this.RblNuevo.SelectedValue);
                ALValorParametrosInternos.Add(this.RblNuevo.SelectedItem.Text );
                //ALValorParametrosInternos.Add(cmbAgrupar.SelectedValue);
                //if (cmbAgrupar.SelectedValue == "0")
                //    ALValorParametrosInternos.Add("Todos");
                //else
                //    ALValorParametrosInternos.Add(cmbAgrupar.SelectedItem.Text);

                ALValorParametrosInternos.Add(Emp_Nombre);//nombre empresa
                ALValorParametrosInternos.Add(Cd_Nombre);//nombre sucursal
                ALValorParametrosInternos.Add(sesion.U_Nombre);//usuario
                ALValorParametrosInternos.Add(DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToShortTimeString());//fecha

                //parametros para el cuerpo del reporte
                ALValorParametrosInternos.Add(sesion.Id_Emp);
                ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                ALValorParametrosInternos.Add(txtGrupo.Text == "" ? (object)null : txtGrupo.Text);//new CN__Comun().DesgloceRangoProductos(txtGrupo.Text.Trim()));
                ALValorParametrosInternos.Add(txtTerritorio.Text == "" ? (object)null : txtTerritorio.Text);//(object)null : new CN__Comun().DesgloceRangoProductos(txtTerritorio.Text.Trim()));//Territorio
                ALValorParametrosInternos.Add(txtCliente.Text == "" ? (object)null : txtCliente.Text);//new CN__Comun().DesgloceRangoProductos(txtCliente.Text.Trim()));//Cliente
                ALValorParametrosInternos.Add(txtEquipo.Text == "" ? (object)null : txtEquipo.Text);// new CN__Comun().DesgloceRangoProductos(txtEquipo.Text.Trim()));//Producto
                ALValorParametrosInternos.Add(txtRepresentante.Text == "" ? (object)null : txtRepresentante.Text);// new CN__Comun().DesgloceRangoProductos(txtRepresentante.Text.Trim()));
                ALValorParametrosInternos.Add(cmbAño.SelectedValue);
                ALValorParametrosInternos.Add(rblDetallado.SelectedValue);
                //conexion
                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                Type instance = null;
                if (a_pantalla)
                {
                    instance = typeof(LibreriaReportes.Rep_SerBaseInstalada);
                }
                else
                {
                    instance = typeof(LibreriaReportes.ExpRep_SerBaseInstalada);
                }


                if (_PermisoImprimir)
                {
                    if (a_pantalla)
                    {
                        Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = null;
                        Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                        Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;
                        RadAjaxManager1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
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

                RadAjaxManager1.ResponseScripts.Add("startDownload('" + instance.Name + ".xls');");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void llenarCombo()
        {
            cmbAño.Items.Insert(0, new RadComboBoxItem(DateTime.Now.AddYears(-3).Year.ToString(), DateTime.Now.AddYears(-3).Year.ToString()));
            cmbAño.Items.Insert(0, new RadComboBoxItem(DateTime.Now.AddYears(-2).Year.ToString(), DateTime.Now.AddYears(-2).Year.ToString()));
            cmbAño.Items.Insert(0, new RadComboBoxItem(DateTime.Now.AddYears(-1).Year.ToString(), DateTime.Now.AddYears(-1).Year.ToString()));
            cmbAño.Items.Insert(0, new RadComboBoxItem(DateTime.Now.AddYears(0).Year.ToString(), DateTime.Now.AddYears(0).Year.ToString()));
            cmbAño.Items.Insert(0, new RadComboBoxItem(DateTime.Now.AddYears(1).Year.ToString(), DateTime.Now.AddYears(1).Year.ToString()));
            cmbAño.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", ""));
            this.cmbAño.Sort = RadComboBoxSort.Ascending;
            this.cmbAño.SortItems();


            //cmbAgrupar.Items.Insert(0, new RadComboBoxItem("Nuevo", "1"));
            //cmbAgrupar.Items.Insert(0, new RadComboBoxItem("Usado", "2"));
            //cmbAgrupar.Items.Insert(0, new RadComboBoxItem("-- Todos --", "0"));
            //this.cmbAgrupar.Sort = RadComboBoxSort.Ascending;
            //this.cmbAgrupar.SortItems();
            //this.cmbAgrupar.SelectedValue = "0";
        }
        #endregion

        #region ErrorManager
        private void Alerta(string mensaje)
        {
            try
            {
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
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