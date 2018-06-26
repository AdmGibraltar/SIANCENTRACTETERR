using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using System.Collections;
using Telerik.Web.UI;
using CapaDatos;
using Telerik.Reporting.Processing;
using System.IO;

namespace SIANWEB
{
    public partial class Rep_InvControlRemisiones3 : System.Web.UI.Page
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
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        CargarCentros();
                        if (!Sesion.Cu_Modif_Pass_Voluntario)
                        {
                            RadAjaxManager1.ResponseScripts.Add("(function(){var f = function(){AbrirContrasenas(); return false;Sys.Application.remove_load(f);};Sys.Application.add_load(f);})()");
                        }
                        CargarRemisiones();
                        CargarTipoRem();

                        dpFechaini.DbSelectedDate = Sesion.CalendarioIni;
                        dpFechafin.DbSelectedDate = Sesion.CalendarioFin;

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Page_Load");
            }
        }
        private void CargarTipoRem()
        {
            try
            {
                cmbTipoRem.Items.Add(new RadComboBoxItem("-- Seleccionar --", "-1"));
                cmbTipoRem.Items.Add(new RadComboBoxItem("Consignación", "62"));
                cmbTipoRem.Items.Add(new RadComboBoxItem("Prueba", "64"));
                cmbTipoRem.Items.Add(new RadComboBoxItem("Pendiente por facturar", "63"));
                cmbTipoRem.Items.Add(new RadComboBoxItem("Equipo arrendado", "72"));
                cmbTipoRem.Items.Add(new RadComboBoxItem("Producto no conforme", "65"));
                cmbTipoRem.Sort = RadComboBoxSort.Ascending;
                cmbTipoRem.SortItems();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void CargarRemisiones()
        {
            try
            {
                cmbRemisiones.Items.Add(new RadComboBoxItem("-- Seleccionar --", "-1"));
                cmbRemisiones.Items.Add(new RadComboBoxItem("Producto", "P"));
                cmbRemisiones.Items.Add(new RadComboBoxItem("Estadística", "E"));
                cmbRemisiones.Items.Add(new RadComboBoxItem("Kardex", "K"));
                cmbRemisiones.Items.Add(new RadComboBoxItem("Listado", "L"));
                cmbRemisiones.Sort = RadComboBoxSort.Ascending;
                cmbRemisiones.SortItems();
            }
            catch (Exception ex)
            {

                throw ex;
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
        protected void rblRemisiones_SelectedIndexChanged(object sender, EventArgs e)
        {


            //if (rblRemisiones.SelectedValue == "1")
            //{
            //    rblDetalle.Enabled = true;
            //}
            //else
            //{
            //    rblDetalle.Enabled = false;
            //}
        }
        protected void cmbRemisiones_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                dpFechaini.DbSelectedDate = sesion.CalendarioIni;
                dpFechafin.DbSelectedDate = sesion.CalendarioFin;

                FechaIni.Visible = false;
                FechaFin.Visible = false;
                txtAnyo.Visible = false;
                txtRik.Visible = false;
                txtTerritorio.Visible = false;
                txtCliente.Visible = false;
                txtProducto.Visible = false;
                Detalle.Visible = false;
                MostrarEnc.Visible = false;
                MostrarDet.Visible = false;
                FiltroEnc.Visible = false;

                dpFechafin.Enabled = false;
                dpFechaini.Enabled = false;
                Estatus.Visible = false;

                Estatus2Enc.Visible = false;
                estatus2Det.Visible = false;

                TrNivelEnc.Visible = false;
                TrNivelDet.Visible = false;

                if (cmbRemisiones.SelectedValue == "P")
                {
                    FechaIni.Visible = true;
                    FechaFin.Visible = true;

                    txtRik.Visible = true;
                    txtTerritorio.Visible = true;
                    txtCliente.Visible = true;
                    txtProducto.Visible = true;

                    Detalle.Visible = true;

                    FiltroEnc.Visible = true;
                    Estatus.Visible = true;
                }
                else if (cmbRemisiones.SelectedValue == "K")
                {
                    FechaIni.Visible = true;
                    FechaFin.Visible = true;

                    dpFechafin.Enabled = true;
                    dpFechaini.Enabled = true;

                    txtRik.Visible = true;
                    txtTerritorio.Visible = true;
                    txtCliente.Visible = true;
                    txtProducto.Visible = true;

                    FiltroEnc.Visible = true;
                    Estatus.Visible = true;

                }
                else if (cmbRemisiones.SelectedValue == "E")
                {
                    txtAnyo.Visible = true;
                    txtRik.Visible = true;
                    txtTerritorio.Visible = true;
                    txtCliente.Visible = true;
                    txtProducto.Visible = true;
                    MostrarEnc.Visible = true;
                    MostrarDet.Visible = true;
                }
                else if (cmbRemisiones.SelectedValue == "L")
                {
                    FechaIni.Visible = true;
                    FechaFin.Visible = true;

                    dpFechafin.Enabled = true;
                    dpFechaini.Enabled = true;

                    txtTerritorio.Visible = true;
                    txtCliente.Visible = true;

                    Estatus2Enc.Visible = true;
                    estatus2Det.Visible = true;

                    TrNivelEnc.Visible = true;
                    TrNivelDet.Visible = true;
                }


            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void cmbTipoRem_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (cmbTipoRem.SelectedValue == "62" || cmbTipoRem.SelectedValue == "72" || cmbTipoRem.SelectedValue == "65")
                {
                   rblEstatus.Visible = false;
                   lblFiltroEstatus.Visible = false;
                   lblFiltroEstatus0.Visible = true;
                   rblEstatus0.Visible = true;
                   if (cmbTipoRem.SelectedValue == "62")
                   {
                       lblFiltroEstatus0.Visible = false;
                       rblEstatus0.Visible = false;
                   }

                }
                else
                {
                    rblEstatus.Visible = true;
                    lblFiltroEstatus.Visible = true;
                    lblFiltroEstatus0.Visible = true;
                    rblEstatus0.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
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

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("print")).Visible = _PermisoImprimir;
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }

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

                string resp = cn_comun.ValidarRango(RadTextBoxTerritorio.Text);
                if (resp != "")
                {
                    Alerta("El rango " + resp + ", en el campo \"Territorio\" no es válido");
                    return;
                }

                resp = cn_comun.ValidarRango(RadTextBoxCliente.Text);
                if (resp != "")
                {
                    Alerta("El rango " + resp + ", en el campo \"Cliente\" no es válido");
                    return;
                }

                resp = cn_comun.ValidarRango(RadTextBoxProducto.Text);
                if (resp != "")
                {
                    Alerta("El rango " + resp + ", en el campo \"Producto\" no es válido");
                    return;
                }

                resp = cn_comun.ValidarRango(RadTextBoxRik.Text);
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
                ALValorParametrosInternos.Add(cmbRemisiones.SelectedItem.Text); //remisiones
                ALValorParametrosInternos.Add(cmbTipoRem.SelectedItem.Text.Replace("-", "").Trim()); //tipo remision
                if (dpFechaini.SelectedDate == null)
                {
                    ALValorParametrosInternos.Add("Todas");//fecha inicial
                }
                else
                {
                    ALValorParametrosInternos.Add(dpFechaini.SelectedDate);//fecha inicial
                }

                if (dpFechafin.SelectedDate == null)
                {
                    ALValorParametrosInternos.Add("Todas");//fecha inicial
                }
                else
                {
                    ALValorParametrosInternos.Add(dpFechafin.SelectedDate);//fecha final
                }
                ALValorParametrosInternos.Add(RadTextBoxRik.Text == "" ? "Todos" : RadTextBoxRik.Text);//RIK
                ALValorParametrosInternos.Add(RadTextBoxTerritorio.Text == "" ? "Todos" : RadTextBoxTerritorio.Text);//Territorio
                ALValorParametrosInternos.Add(RadTextBoxCliente.Text == "" ? "Todos" : RadTextBoxCliente.Text);//Cliente
                ALValorParametrosInternos.Add(RadTextBoxProducto.Text == "" ? "Todos" : RadTextBoxProducto.Text);//Producto
                ALValorParametrosInternos.Add(rblEstatus.SelectedItem.Text);//Estatus
                ALValorParametrosInternos.Add(chkDetalle.Checked ? "A detalle" : "Sin detalle");//Detalle

                ALValorParametrosInternos.Add(Emp_Nombre);//nombre empresa
                ALValorParametrosInternos.Add(Cd_Nombre);//nombre sucursal
                ALValorParametrosInternos.Add(sesion.U_Nombre);//usuario
                ALValorParametrosInternos.Add(DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToShortTimeString());//fecha

                //parametros para el cuerpo del reporte
                ALValorParametrosInternos.Add(sesion.Id_Emp);
                ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                ALValorParametrosInternos.Add(cmbTipoRem.SelectedValue == "-1" ? (object)null : cmbTipoRem.SelectedValue);
                ALValorParametrosInternos.Add(dpFechaini.SelectedDate);//fecha inicial
                ALValorParametrosInternos.Add(dpFechafin.SelectedDate);//fecha final
                ALValorParametrosInternos.Add(RadTextBoxRik.Text == "" ? (object)null : new CN__Comun().DesgloceRangoProductos(RadTextBoxRik.Text.Trim()));//RIK
                ALValorParametrosInternos.Add(RadTextBoxTerritorio.Text == "" ? (object)null : new CN__Comun().DesgloceRangoProductos(RadTextBoxTerritorio.Text.Trim()));//Territorio
                ALValorParametrosInternos.Add(RadTextBoxCliente.Text == "" ? (object)null : new CN__Comun().DesgloceRangoProductos(RadTextBoxCliente.Text.Trim()));//Cliente
                ALValorParametrosInternos.Add(RadTextBoxProducto.Text == "" ? (object)null : new CN__Comun().DesgloceRangoProductos(RadTextBoxProducto.Text.Trim()));//Producto

                if (cmbRemisiones.SelectedValue == "L")
                {
                    ALValorParametrosInternos.Add(rblEstatus0.SelectedItem.Value);//Estatus
                }
                else
                {
                    ALValorParametrosInternos.Add(rblEstatus.SelectedItem.Text == "Vencidas" ? 1 : 0);//Estatus
                }
                //conexion
                ALValorParametrosInternos.Add(sesion.Emp_Cnx);

                Funciones funcion = new Funciones();
                ALValorParametrosInternos.Add(funcion.GetLocalDateTime(sesion.Minutos));//FECHA ACTUAL
                if (cmbRemisiones.SelectedValue == "E")
                {
                    ALValorParametrosInternos.Add(NtxtAnyo.Text);
                }
                Type instance = null;
                switch (cmbRemisiones.SelectedValue)
                {
                    case "P"://Producto
                        if (!chkDetalle.Checked)//caso 1a.
                        {//sin Detalle
                            if (a_pantalla)
                            {
                                instance = typeof(LibreriaReportes.Rep_InvControlRemisiones1A);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.ExpRep_InvControlRemisiones1A);
                            }
                        }
                        else
                        {//con Detalle
                            if (a_pantalla)
                            {
                                instance = typeof(LibreriaReportes.Rep_InvControlRemisiones1B);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.ExpRep_InvControlRemisiones1B);
                            }
                        }
                        break;
                    case "E"://Estadistica
                        switch (rblMostrar.SelectedValue)
                        {
                            case "a":
                                if (a_pantalla)
                                {
                                    instance = typeof(LibreriaReportes.Rep_InvControlRemisiones2a);//unidades
                                }
                                else
                                {
                                    instance = typeof(LibreriaReportes.ExpRep_InvControlRemisiones2a);//unidades
                                }
                                break;
                            case "b":
                                if (a_pantalla)
                                {
                                    instance = typeof(LibreriaReportes.Rep_InvControlRemisiones2b);//pesos
                                }
                                else
                                {
                                    instance = typeof(LibreriaReportes.ExpRep_InvControlRemisiones2b);//pesos
                                }
                                break;
                            case "x":
                                if (a_pantalla)
                                {
                                    instance = typeof(LibreriaReportes.Rep_InvControlRemisiones2);//ambos
                                }
                                else
                                {
                                    instance = typeof(LibreriaReportes.ExpRep_InvControlRemisiones2);//ambos
                                }
                                break;
                        }
                        break;
                    case "K"://Kardex
                        if (a_pantalla)
                        {
                            instance = typeof(LibreriaReportes.Rep_InvControlRemisiones3);
                        }
                        else
                        {
                            instance = typeof(LibreriaReportes.ExpRep_InvControlRemisiones3);
                        }
                        break;
                    case "L"://Listado
                        if (a_pantalla)
                        {
                            if (rblNivel.SelectedValue == "D")
                            {
                                instance = typeof(LibreriaReportes.Rep_RemisionVencidaA);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.Rep_RemisionVencidaB);
                            }
                        }
                        else
                        {
                            if (rblNivel.SelectedValue == "D")
                            {
                                instance = typeof(LibreriaReportes.ExpRep_RemisionVencidaA);
                            }
                            else
                            {
                                instance = typeof(LibreriaReportes.ExpRep_RemisionVencidaB);
                            }
                        }
                        break;
                    default:
                        break;
                }

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
                    report1.ReportParameters[i].AllowNull = true;
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
