using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using CapaDatos;
using System.Globalization;
using System.Collections;
using System.Drawing;

namespace SIANWEB
{
    public partial class ProAdminCapPedido_VentInst : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        //public int PermisoGuardar { get { return _PermisoGuardar == true ? 1 : 0; } }
        //public int PermisoModificar { get { return _PermisoModificar == true ? 1 : 0; } }
        //public int PermisoEliminar { get { return _PermisoEliminar == true ? 1 : 0; } }
        //public int PermisoImprimir { get { return _PermisoImprimir == true ? 1 : 0; } }

        public Sesion session
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["session" + Session.SessionID] = value;

            }
        }
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (session == null)
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
                        if (session.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        CargarCentros();
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
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                CN__Comun comun = new CN__Comun();
                Sesion sesion2 = session;
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion2);
                session = sesion2;
                Inicializar();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "save")
                {
                  
                }
                else if (btn.CommandName == "new")
                {
                    VentaNueva();
                }
                else if (btn.CommandName == "print")
                {
                    Imprimir();
                }               
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    if (session == null)
                    {
                        string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                        Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                        Response.Redirect("login.aspx", false);
                    }
                    else
                    {
                        rg1.DataSource = GetList();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rg1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtCteIni.Value > txtCteFin.Value)
                {
                    Alerta("El número de cliente inicial no puede ser mayor al número de cliente final");
                    return;
                }
                if (txtTerIni.Value > txtTerFin.Value)
                {
                    Alerta("El número de territorio inicial no puede ser mayor al número de territorio final");
                    return;
                }
                rg1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":
                        rg1.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg1_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridItem gi = e.Item;
                switch (e.CommandName)
                {
                    case "captar":
                        Captar(ref e, gi);
                        break;
                    case "Cancelar":
                        cancelar(gi.Cells[3].Text, Convert.ToInt32(gi.Cells[9].Text), Convert.ToInt32(gi.Cells[8].Text));
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                ErrorManager();
                int semana = Convert.ToInt32(txtSem.Text);
                int anio = Convert.ToInt32(txtAnio.Text);
                if (e.Item is GridDataItem)
                {//solo falta campo de busqueda en filtro clientes                          
                    GridDataItem dataItem = (GridDataItem)e.Item;
                    int semanaAcs = !string.IsNullOrEmpty(dataItem["Acs_Semana"].Text) ? Convert.ToInt32(dataItem["Acs_Semana"].Text) : 0;
                    int anioAcs = !string.IsNullOrEmpty(dataItem["Acs_Anio"].Text) ? Convert.ToInt32(dataItem["Acs_Anio"].Text) : 0;

                    if (semana == 52)
                    {
                        if ((semanaAcs == 52 && anioAcs == anio) || (semanaAcs == 1 && anioAcs == (anio + 1)))
                        {
                            dataItem["Id_Cte"].ForeColor = Color.Red;
                            dataItem["Cte_Nom"].ForeColor = Color.Red;
                            dataItem["Id_Ter"].ForeColor = Color.Red;
                            dataItem["Acs_Cantidad"].ForeColor = Color.Red;
                            dataItem["Acs_Semana"].ForeColor = Color.Red;
                            dataItem["Acs_Anio"].ForeColor = Color.Red;

                            dataItem["Id_Cte"].Font.Bold = true;
                            dataItem["Cte_Nom"].Font.Bold = true;
                            dataItem["Id_Ter"].Font.Bold = true;
                            dataItem["Acs_Cantidad"].Font.Bold = true;
                            dataItem["Acs_Semana"].Font.Bold = true;
                            dataItem["Acs_Anio"].Font.Bold = true;
                        }
                    }
                    else
                    {
                        if ((semanaAcs == semana || semanaAcs == (semana + 1)) && anioAcs == anio)
                        {
                            dataItem["Id_Cte"].ForeColor = Color.Red;
                            dataItem["Cte_Nom"].ForeColor = Color.Red;
                            dataItem["Id_Ter"].ForeColor = Color.Red;
                            dataItem["Acs_Cantidad"].ForeColor = Color.Red;
                            dataItem["Acs_Semana"].ForeColor = Color.Red;
                            dataItem["Acs_Anio"].ForeColor = Color.Red;

                            dataItem["Id_Cte"].Font.Bold = true;
                            dataItem["Cte_Nom"].Font.Bold = true;
                            dataItem["Id_Ter"].Font.Bold = true;
                            dataItem["Acs_Cantidad"].Font.Bold = true;
                            dataItem["Acs_Semana"].Font.Bold = true;
                            dataItem["Acs_Anio"].Font.Bold = true;
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
        #region Funciones
        private void Inicializar()
        {
            cmbVencido.Items.Clear();
            cmbVencido.Items.Add(new RadComboBoxItem("-- Todos --", ""));
            cmbVencido.Items.Add(new RadComboBoxItem("Vencidos", "V"));
            cmbVencido.Items.Add(new RadComboBoxItem("Vigente", "C"));
            cmbVencido.Sort = RadComboBoxSort.Ascending;
            cmbVencido.SortItems();
            cmbVencido.SelectedValue = "C";

            Funciones funcion = new Funciones();
            txtAnio.Value = funcion.GetLocalDateTime(session.Minutos).Year;
            Semana semana = new Semana();
            semana.Sem_FechaAct = funcion.GetLocalDateTime(session.Minutos);
            semana.Id_Emp = session.Id_Emp;
            semana.Id_Cd = session.Id_Cd_Ver;
            CN_CatSemana cn_semana = new CN_CatSemana();
            int verificador = 0;
            cn_semana.ConsultaSemanaActual(ref semana, session.Emp_Cnx, ref verificador);
            if (verificador > 0 && semana.Id_Sem != 0)
            {
                txtSem.Value = semana.Id_Sem;
                rg1.Rebind();
            }
            else
                Alerta("Aun no se han configurado las semanas del periodo actual");
        }
        private void Imprimir()
        {
            try
            {
                CultureInfo cultura = CultureInfo.CurrentCulture;
                Funciones funcion = new Funciones();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();

                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                ALValorParametrosInternos.Add(sesion.Id_Emp);
                ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                ALValorParametrosInternos.Add(GetWeekNumber(funcion.GetLocalDateTime(session.Minutos)));
                ALValorParametrosInternos.Add(funcion.GetLocalDateTime(session.Minutos).Year);
                ALValorParametrosInternos.Add(funcion.GetLocalDateTime(session.Minutos));

                Type instance = null;
                instance = typeof(LibreriaReportes.PedidoProximosImpresion);
                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;

                //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
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
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                if (session.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, session.Id_Emp, session.Id_U, session.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(session.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, session.Id_Emp, session.Id_U, session.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = session.Id_Cd_Ver.ToString();
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
                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, session.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = session.Id_U;
                Permiso.Id_Cd = session.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, session.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;
                    this.rtb1.Items[6].Visible = false;
                    //Regresar
                    this.rtb1.Items[5].Visible = false;
                    //Eliminar
                    this.rtb1.Items[4].Visible = false;
                    //Correo
                    this.rtb1.Items[2].Visible = false;

                    if (session.Id_Rik != -1 || session.Id_TU == 2)
                    { //Captura de pedidos por parte del representante
                        CN_CatCentroDistribucion catcentro = new CN_CatCentroDistribucion();
                        CentroDistribucion cd = new CentroDistribucion();
                        catcentro.ConsultarCentroDistribucion(ref cd, session.Id_Cd_Ver, session.Id_Emp, session.Emp_Cnx);

                        if (!cd.Cd_ActivaCapPedRep)
                        {
                            this.rtb1.Items[7].Visible = false;
                            rg1.Columns[9].Visible = false;
                        }
                    }
                }
                else
                    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<PedidoVtaInst> GetList()
        {
            try
            {
                Funciones Funcion = new Funciones();
                List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                CN_CapPedidoVtaInst clsCatArea = new CN_CapPedidoVtaInst();
                PedidoVtaInst pedido = new PedidoVtaInst();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Acs_Fecha = Funcion.GetLocalDateTime(session.Minutos);
                pedido.Estatus = cmbVencido.SelectedValue == "" ? (string)null : cmbVencido.SelectedValue;
                double semana = 0;
                double anhio = 0;
                double.TryParse(txtSem.Value.ToString(), out semana);
                double.TryParse(txtAnio.Value.ToString(), out anhio);

                if (semana == 0)
                    pedido.Filtro_Sem = null;
                else
                    pedido.Filtro_Sem = txtSem.Value;

                if (anhio <= 1989)
                    pedido.Filtro_Anio = null;
                else
                    pedido.Filtro_Anio = txtAnio.Value;

                pedido.Filtro_CteIni = txtCteIni.Value.ToString();
                pedido.Filtro_CteFin = txtCteFin.Value.ToString();
                pedido.Filtro_TerIni = txtTerIni.Value;
                pedido.Filtro_TerFin = txtTerFin.Value;
                pedido.Filtro_usuario = session.Propia ? session.Id_U.ToString() : "";
                pedido.Id_U = session.Id_Rik == -1 ? (int?)null : session.Id_Rik;
                clsCatArea.Lista(pedido, session.Emp_Cnx, ref List);
                txtTotal.Value = List.Count;
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void cancelar(string Id_Acs, int Anio, int Semana)
        {
            try
            {
                int verificador = -1;
                CN_CapPedidoVtaInst clsPedidovi = new CN_CapPedidoVtaInst();
                PedidoVtaInst pedido = new PedidoVtaInst();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Acs = Convert.ToInt32(Id_Acs);
                pedido.Acs_Anio = Anio;
                pedido.Acs_Semana = Semana;
                clsPedidovi.Cancelar(pedido, session.Emp_Cnx, ref verificador);
                if (verificador == 1)
                {
                    rg1.Rebind();
                    Alerta("El registro fue cancelado");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void VentaNueva()
        {
            RAM1.ResponseScripts.Add("return AbrirVentana_ProPedidoVI('', '" + _PermisoGuardar + "', '" + _PermisoModificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "', '" + txtAnio.Text + "'," + txtSem.Text + ")");
        }      
        private void Captar(ref GridCommandEventArgs e, GridItem gi)
        {
            try
            {
                if (gi.Cells[2].Text.ToLower() != "false")
                {
                    Alerta("El cliente tiene crédito suspendido, favor de hacer las gestiones correspondientes para poder captar");
                    e.Canceled = true;
                }
                else
                {
                    CN_CapAcys cn_capacys = new CN_CapAcys();
                    Acys acys = new Acys();
                    acys.Id_Emp = session.Id_Emp;
                    acys.Id_Cd = session.Id_Cd_Ver;
                    acys.Id_Acs = Convert.ToInt32(gi.Cells[3].Text);
                    cn_capacys.Consultar(ref acys, session.Emp_Cnx);
                    if (acys.Acs_Estatus == "B")
                    {
                        Alerta("No se puede captar el pedido, el Acuerdo esta dado de baja");
                        rg1.Rebind();
                        return;
                    }
                    RAM1.ResponseScripts.Add("return AbrirVentana_ProPedidoVI('" + gi.Cells[3].Text + "', '" + _PermisoGuardar + "', '" + _PermisoModificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "'," + gi.Cells[9].Text + ", '" + gi.Cells[8].Text + "')");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetWeekNumber(DateTime dtPassed)
        {
            try
            {
                CultureInfo ciCurr = CultureInfo.CurrentCulture;
                int weekNum = ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                return weekNum.ToString();
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