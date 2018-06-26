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
    public partial class Rep_VenEstadisticaVentas : System.Web.UI.Page
    {
        #region Variables
        public string strEmp = System.Configuration.ConfigurationManager.AppSettings["VGEmpresa"];
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
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        Inicializar();

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();

                        int VGEmpresa = 0;
                        Int32.TryParse(strEmp, out VGEmpresa);
                        if (VGEmpresa == Sesion.Id_Emp)
                        {
                            Ambos.Visible = true;
                            TrMes.Visible = true;
                        }
                        else
                        {
                            Ambos.Visible = false;
                            TrMes.Visible = false;
                        }
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
                if (!string.IsNullOrEmpty(cmbAño.SelectedValue))
                    if (cmbAño.SelectedValue != "-1")
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
                        Alerta("Ingresar un año válido");
                else
                    Alerta("Ingresar un año válido");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void rbTerritorio_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTerritorio.Checked)
            {
                //txtProducto.Enabled = true;
                Filtro_Producto.Visible = true;
                //txtTerritorio.Enabled = true;
                Filtro_Territorio.Visible = true;
                txtCliente.Text = string.Empty;
                //txtCliente.Enabled = false;
                Filtro_Cliente.Visible = false;
                //cbCliente.Enabled = true;
                Nivel_Cliente.Visible = true;
                //cbProducto.Enabled = true;
                Nivel_Producto.Visible = true;
                Nivel.Visible = true;
            }
        }
        protected void rbCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCliente.Checked)
            {
                //txtCliente.Enabled = true;
                Filtro_Cliente.Visible = true;
                cbCliente.Checked = false;
                //cbCliente.Enabled = false;
                Nivel_Cliente.Visible = false;
                //cbProducto.Enabled = true;
                Nivel_Producto.Visible = true;
                txtTerritorio.Text = string.Empty;
                //txtTerritorio.Enabled = false;
                Filtro_Territorio.Visible = false;
                txtProducto.Text = string.Empty;
                //txtProducto.Enabled = false;
                Filtro_Producto.Visible = false;
                Nivel.Visible = true;

            }
        }
        protected void rbProducto_CheckedChanged(object sender, EventArgs e)
        {
            if (rbProducto.Checked)
            {
                txtTerritorio.Text = string.Empty;
                //txtTerritorio.Enabled = false;
                Filtro_Territorio.Visible = false;
                txtCliente.Text = string.Empty;
                //txtCliente.Enabled = false;
                Filtro_Cliente.Visible = false;
                //txtProducto.Enabled = true;
                Filtro_Producto.Visible = true;
                cbCliente.Checked = false;
                //cbCliente.Enabled = false;
                Nivel_Cliente.Visible = false;
                cbProducto.Checked = false;
                //cbProducto.Enabled = false;
                Nivel_Producto.Visible = false;
                Nivel.Visible = false;
            }
        }
        protected void cmbCentrosDist_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
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
            CargarAño();
        }
        #endregion
        #region Funciones
        private void Inicializar()
        {
            try
            {
                CargarCentros();
                CargarAño();
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
        private void CargarAño()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatCalendarioAnhio2_Combo", ref cmbAño);

                if (cmbAño.FindItemIndexByValue(Sesion.CalendarioFin.Year.ToString()) != null)
                {
                    cmbAño.SelectedIndex = cmbAño.FindItemIndexByValue(Sesion.CalendarioFin.Year.ToString());
                }
                else
                {
                    cmbAño.SelectedIndex = cmbAño.Items.Count - 1;
                }
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
            #region Captura de valores
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            string nombreEmpresa = sesion.Emp_Nombre;
            string nombreSucursal = sesion.Cd_Nombre;
            DateTime Fechalocal = DateTime.Now;
            int error = 0;
            VenEstadisticaVentas ventas = new VenEstadisticaVentas();
            ventas.Id_Cd = sesion.Id_Cd_Ver;
            //radioButton Filtro-- 1
            if (rbTerritorio.Checked == true)
            {
                ventas.Filtro = 1;
                ventas.SFiltro = rbTerritorio.Text;
            }
            if (rbCliente.Checked == true)
            {
                ventas.Filtro = 2;
                ventas.SFiltro = rbCliente.Text;
            }
            if (rbProducto.Checked == true)
            {
                ventas.Filtro = 3;
                ventas.SFiltro = rbProducto.Text;
            }
            //txtTerritorio
            if (!string.IsNullOrEmpty(txtTerritorio.Text))
            {
                boton(txtTerritorio.Text, ref error);
                ventas.Territorio = txtTerritorio.Text;
                ventas.STerritorio = txtTerritorio.Text;
            }
            else
                ventas.STerritorio = "Todos";

            //txtClientes
            if (!string.IsNullOrEmpty(txtCliente.Text))
            {
                boton(txtCliente.Text, ref error);
                ventas.Cliente = txtCliente.Text;
                ventas.SCliente = txtCliente.Text;
            }
            else
                ventas.SCliente = "Todos";

            //txtProducto
            if (!string.IsNullOrEmpty(txtProducto.Text))
            {
                boton(txtProducto.Text, ref error);
                ventas.Producto = txtProducto.Text;
                ventas.SProducto = txtProducto.Text;
            }
            else
                ventas.SProducto = "Todos";

            //combo Año
            int año = -1;
            int.TryParse(cmbAño.SelectedValue, out año);
            if (año > 0)
            {
                ventas.Anio = año;
                ventas.SAnio = cmbAño.SelectedItem.Text;
            }
            //radioButton Mostrar
            if (rbPesos.Checked)
            {// X
                ventas.Mostrar = 1;
                ventas.SMostrar = rbPesos.Text;
            }
            if (rbUnidades.Checked)
            {// Y
                ventas.Mostrar = 2;
                ventas.SMostrar = rbUnidades.Text;
            }
            if (rbAmbos.Checked)
            {// Y
                ventas.Mostrar = 3;
                ventas.SMostrar = rbAmbos.Text;
            }
            //checkBox Cliente   
            ventas.Nivel = 0;
            ventas.Nivel2 = 0;
            if (cbCliente.Checked)
            {//A
                ventas.Nivel = 1;
                ventas.SNivel = cbCliente.Text;
            }
            else
                ventas.SNivel = "Todos";

            //checkBox Producto
            if (cbProducto.Checked)
            {//B
                ventas.Nivel2 = 1;
                if (ventas.SNivel == "Todos")
                    ventas.SNivel = cbProducto.Text;
                else
                    ventas.SNivel += " y " + cbProducto.Text;
            }
            else
                if (string.IsNullOrEmpty(ventas.SNivel))
                    ventas.SNivel = "Todos";

            #endregion
            #region valoresParametros
            #region datos de filtros
            if (ventas.Filtro == 1)
            {//Territorio      -- 1          
                if (ventas.Nivel == 0 && ventas.Nivel2 == 0)
                {//ni clientes,sin producto -- a - b
                    switch (ventas.Mostrar)
                    {
                        case 1://pesos - x 
                            ventas.Reporte = 1;
                            break;
                        case 2://unidades - y 
                            ventas.Reporte = 2;
                            break;
                        case 3: //ambos
                            ventas.Reporte = 15;
                            break;
                    }

                    ventas.Encabezado = "Núm.";
                    ventas.Encabezado1 = "Territorio";
                }
                if (ventas.Nivel == 1 && ventas.Nivel2 == 0)
                {//clientes - a
                    switch (ventas.Mostrar)
                    {
                        case 1://pesos - x 
                            ventas.Reporte = 3;
                            break;
                        case 2://unidades - y 
                            ventas.Reporte = 4;
                            break;
                        case 3: //ambos
                            ventas.Reporte = 16;
                            break;
                    }
                    ventas.Encabezado = "Núm.";
                    ventas.Encabezado1 = "Cliente";
                }

                if (ventas.Nivel == 0 && ventas.Nivel2 == 1)
                {//productos - b
                    switch (ventas.Mostrar)
                    {
                        case 1://pesos - x 
                            ventas.Reporte = 5;
                            break;
                        case 2://unidades - y 
                            ventas.Reporte = 6;
                            break;
                        case 3: //ambos
                            ventas.Reporte = 17;
                            break;
                    }
                    ventas.Encabezado = "Núm.";
                    ventas.Encabezado1 = "Producto";
                }
                if (ventas.Nivel == 1 && ventas.Nivel2 == 1)
                {//clientes - a , productos - b
                    switch (ventas.Mostrar)
                    {
                        case 1://pesos - x 
                            ventas.Reporte = 7;
                            break;
                        case 2://unidades - y 
                            ventas.Reporte = 8;
                            break;
                        case 3: //ambos
                            ventas.Reporte = 18;
                            break;
                    }
                    ventas.Encabezado = "Núm.";
                    ventas.Encabezado1 = "Producto";
                }
            }

            if (ventas.Filtro == 2)
            {//Cliente      -- 2
                if (ventas.Nivel == 0 && ventas.Nivel2 == 0)
                {//ni clientes,sin producto -- a - b
                    switch (ventas.Mostrar)
                    {
                        case 1://pesos - x 
                            ventas.Reporte = 9;
                            break;
                        case 2://unidades - y 
                            ventas.Reporte = 10;
                            break;
                        case 3: //ambos
                            ventas.Reporte = 19;
                            break;
                    }
                    ventas.Encabezado = "Núm.";
                    ventas.Encabezado1 = "Cliente";
                }
                if (ventas.Nivel == 0 && ventas.Nivel2 == 1)
                {//productos - b
                    switch (ventas.Mostrar)
                    {
                        case 1://pesos - x 
                            ventas.Reporte = 11;
                            break;
                        case 2://unidades - y 
                            ventas.Reporte = 12;
                            break;
                        case 3: //ambos
                            ventas.Reporte = 20;
                            break;
                    }
                    ventas.Encabezado = "Núm.";
                    ventas.Encabezado1 = "Producto";
                }
            }

            if (ventas.Filtro == 3)
            {//Productos -- 3
                if (ventas.Nivel == 0 && ventas.Nivel2 == 0)
                {//ni clientes,sin producto -- a - b
                    switch (ventas.Mostrar)
                    {
                        case 1://pesos - x 
                            ventas.Reporte = 13;
                            break;
                        case 2://unidades - y 
                            ventas.Reporte = 14;
                            break;
                        case 3: //ambos
                            ventas.Reporte = 21;
                            break;
                    }
                    ventas.Encabezado = "Núm.";
                    ventas.Encabezado1 = "Producto";
                }
            }
            #endregion

            ArrayList ALValorParametrosInternos = new ArrayList();
            ALValorParametrosInternos.Add(ventas.Filtro);
            ALValorParametrosInternos.Add(ventas.SFiltro);
            ALValorParametrosInternos.Add(ventas.Sucursal);
            ALValorParametrosInternos.Add(ventas.SSucursal);
            ALValorParametrosInternos.Add(ventas.Territorio);
            ALValorParametrosInternos.Add(ventas.STerritorio);
            ALValorParametrosInternos.Add(ventas.Cliente);
            ALValorParametrosInternos.Add(ventas.SCliente);
            ALValorParametrosInternos.Add(ventas.Producto);
            ALValorParametrosInternos.Add(ventas.SProducto);
            ALValorParametrosInternos.Add(ventas.Anio);
            ALValorParametrosInternos.Add(ventas.SAnio);
            ALValorParametrosInternos.Add(ventas.Mostrar);
            ALValorParametrosInternos.Add(ventas.SMostrar);
            ALValorParametrosInternos.Add(ventas.Nivel);
            ALValorParametrosInternos.Add(ventas.SNivel);
            ALValorParametrosInternos.Add(ventas.Nivel2);
            ALValorParametrosInternos.Add(ventas.Reporte);
            ALValorParametrosInternos.Add(ventas.Encabezado);
            ALValorParametrosInternos.Add(ventas.Encabezado1);
            ALValorParametrosInternos.Add(sesion.Id_U);
            //parametros para el cuerpo del reporte
            ALValorParametrosInternos.Add(sesion.Id_Emp);
            ALValorParametrosInternos.Add(ventas.Id_Cd);
            ALValorParametrosInternos.Add(sesion.U_Nombre);
            ALValorParametrosInternos.Add(Fechalocal);
            ALValorParametrosInternos.Add(nombreEmpresa);
            ALValorParametrosInternos.Add(nombreSucursal);
            if (!rbAmbos.Checked)
                ALValorParametrosInternos.Add(strEmp);
            //conexion
            ALValorParametrosInternos.Add(sesion.Emp_Cnx);
            ALValorParametrosInternos.Add(cmbMes.SelectedValue);
            #endregion
            Type instance = null;
            if (a_pantalla)
            {
                if (rbAmbos.Checked)
                {
                    instance = typeof(LibreriaReportes.Rep_VenEstadisticaVentasAmbos);
                }
                else
                {
                    instance = typeof(LibreriaReportes.Rep_VenEstadisticaVentas);
                }
            }
            else
            {
                if (rbAmbos.Checked)
                {
                    instance = typeof(LibreriaReportes.ExpRep_VenEstadisticaVentasAmbos);
                }
                else
                {
                    instance = typeof(LibreriaReportes.ExpRep_VenEstadisticaVentas);
                }

            }

            //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
            if (error == 0)
                if (_PermisoImprimir)
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
                else
                    Alerta("No tiene permiso para imprimir");
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
                string ruta = Server.MapPath("Reportes") + "\\" + instance.Name + "_" + report1.ReportParameters[1].Value + ".xls";

                if (File.Exists(ruta))
                    File.Delete(ruta);

                FileStream fs = new FileStream(ruta, FileMode.Create);
                fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);

                fs.Flush();
                fs.Close();

                RAM1.ResponseScripts.Add("startDownload('" + instance.Name + "_" + report1.ReportParameters[1].Value + ".xls');");
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

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }
    }
}