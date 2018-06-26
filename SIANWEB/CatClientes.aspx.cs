using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;

namespace SIANWEB
{
    public partial class CatClientes : System.Web.UI.Page
    {
        #region Variables
        public string strEmp = System.Configuration.ConfigurationManager.AppSettings["VGEmpresa"];
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        //static DataTable dt;
        private DataTable dt { get { return (DataTable)Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public DataTable TablaPermisosUEN
        {
            get
            {
                return (Session["TablaPermisosUEN" + Session.SessionID] as DataTable);
            }
            set
            {
                Session["TablaPermisosUEN" + Session.SessionID] = value;
            }
        }
        bool terr = false;
        bool Seg = false;
        public string Valor
        {
            get
            {
                return MaximoId();
            }
            set { }
        }
        private string Emp_CnxCob
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCobranza"); }
        }
        #endregion
        #region Eventos
        protected void Page_Init(object sender, EventArgs e)
        {
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];
            if (session == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                Response.Redirect("login.aspx", false);
            }
            else
            {
                CargarClientes();
            }
        }
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
                        Inicializar();
                        CargarCuentaNacional();
                        int VGEmpresa = 0;
                        Int32.TryParse(strEmp, out VGEmpresa);
                        if (VGEmpresa == Sesion.Id_Emp)
                            trReferencia.Visible = true;
                        else
                            trReferencia.Visible = false;


                    }
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion(); sesion = (Sesion)Session["Sesion" + Session.SessionID]; if (sesion == null) { string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries); Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false); } CN__Comun comun = new CN__Comun(); comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                Inicializar();
                Nuevo();
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }

        protected void cmbRemisionElect_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                CargarCuentaNacional(Convert.ToInt32(cmbRemisionElect.SelectedValue));

            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
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
                    if (Page.IsValid)
                        PreGuardar();
                    //Guardar();
                }
                else if (btn.CommandName == "new")
                {
                    Nuevo();
                }
                else if (btn.CommandName == "undo")
                {
                    //Regresar()
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
        private void PreGuardar()
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                CN_ConfiguracionCobranza confCobranza = new CN_ConfiguracionCobranza();

                List<Acciones> list_accionesTemp = new List<Acciones>();
                List<Alertas> list_alertasTemp = new List<Alertas>();
                List<PeriodoGracia> list_graciaTemp = new List<PeriodoGracia>();
                Reglas reglas = new Reglas();

                confCobranza.Consultar(ref list_graciaTemp, ref list_accionesTemp, ref list_alertasTemp, session.Id_Emp, (new SqlConnectionStringBuilder(session.Emp_Cnx)).InitialCatalog, ref reglas, Emp_CnxCob);

                string id_tu = "";

                if (txtDias1.Value >= (double?)reglas.Val1 && txtDias1.Value <= (double?)reglas.Val2)
                {
                    id_tu = "," + reglas.Id_Tu1.ToString() + "," + reglas.Id_Tu2.ToString() + "," + reglas.Id_Tu3.ToString() + ",";
                }

                if (txtDias1.Value >= (double?)reglas.Val3 && txtDias1.Value <= (double?)reglas.Val4)
                {
                    id_tu = "," + reglas.Id_Tu3.ToString() + "," + reglas.Id_Tu2.ToString() + ",";
                }

                if (txtDias1.Value >= (double?)reglas.Val5 && txtDias1.Value <= (double?)reglas.Val6)
                {
                    id_tu = "," + reglas.Id_Tu3.ToString() + ",";
                }

                txtAutorizo.Text = "";

                if (id_tu != "")
                {
                    Session["Sesion" + Session.SessionID + "Id_Tu"] = id_tu;
                    AbrirVentana_Autorizacion();
                }
                else
                {
                    Guardar((int?)null, (int?)null);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void AbrirVentana_Autorizacion()
        {
            try
            {
                RAM1.ResponseScripts.Add("AbrirVentana_Autorizacion();");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void cmbCliente_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (cmbCliente.SelectedValue == "" && cmbCliente.Text != "")
                {
                    Alerta("El cliente no existe");
                    Nuevo();
                    cmbCliente.Focus();
                    return;
                }
                if (cmbCliente.SelectedValue != "" && Convert.ToInt32(cmbCliente.SelectedValue) > -1)
                {
                    CN__Comun.RemoverValidadores(Validators);
                    Sesion session = new Sesion();
                    session = (Sesion)Session["Sesion" + Session.SessionID];
                    Clientes cte = new Clientes();
                    cte.Id_Emp = session.Id_Emp;
                    cte.Id_Cd = session.Id_Cd_Ver;
                    cte.Id_Cte = Convert.ToInt32(cmbCliente.SelectedValue);
                    cte.Ignora_Inactivo = true;
                    CN_CatCliente clsCatClientes = new CN_CatCliente();
                    clsCatClientes.ConsultaClientes(ref cte, session.Emp_Cnx);

                    txtClave.Enabled = false;
                    txtClave.Text = cte.Id_Cte.ToString();
                    HF_ID.Value = cte.Id_Cte.ToString();
                    txtDescripcion.Text = cte.Cte_NomComercial;
                    txtNombreCorto.Text = cte.Cte_NomCorto;
                    txtFcalle.Text = cte.Cte_FacCalle;
                    txtFnumero.Text = cte.Cte_FacNumero;
                    txtReferencia.Text = cte.Cte_Referencia;
                    try
                    {
                        txtFcp.Text = cte.Cte_FacCp;
                    }
                    catch
                    {
                        //EL TELEFONO TIENE FORMATO INCORRECTO
                    }
                    txtFcolonia.Text = cte.Cte_FacColonia;
                    txtFmunicipio.Text = cte.Cte_FacMunicipio;
                    try
                    {
                        txtFtelefono.Text = cte.Cte_FacTel;
                        // txtFtelefono.Text = cte.Cte_FacTel.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "").Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    }
                    catch
                    {
                        //EL TELEFONO TIENE FORMATO INCORRECTO
                    }

                    txtFrfc.Text = cte.Cte_FacRfc;
                    txtFestado.Text = cte.Cte_FacEstado;
                    txtDcalle.Text = cte.Cte_Calle;
                    txtDnumero.Text = cte.Cte_Numero.ToString();
                    try
                    {
                        if (!string.IsNullOrEmpty(cte.Cte_Cp.Trim()))
                        {
                            txtDcp.Text = cte.Cte_Cp;
                        }
                    }
                    catch
                    {
                        //EL CP TIENE FORMATO INCORRECTO
                    }
                    txtDcolonia.Text = cte.Cte_Colonia;
                    txtDmunicipio.Text = cte.Cte_Municipio;
                    txtDestado.Text = cte.Cte_Estado;
                    try
                    {
                        txtDtelefono.Text = cte.Cte_Telefono;
                        // txtDtelefono.Text = cte.Cte_Telefono.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "").Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    }
                    catch
                    {
                        //EL TELEFONO TIENE FORMATO INCORRECTO
                    }
                    try
                    {
                        txtDrfc.Text = cte.Cte_DRfc;
                    }
                    catch
                    {
                        //EL RFC TIENE FORMATO INCORRECTO
                    }
                    try
                    {
                        txtDfax.Text = cte.Cte_Fax;
                        // txtDfax.Text = cte.Cte_Fax.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "").ToString();
                    }
                    catch
                    {
                        //EL FAX TIENE FORMATO INCORRECTO
                    }
                    txtDcontacto.Text = cte.Cte_Contacto;
                    txtmail.Text = cte.Cte_Email;
                    chkCredito.Checked = cte.Cte_Credito;
                    chkFacturar.Checked = cte.Cte_Facturacion;
                    cmbMoneda.SelectedIndex = cmbMoneda.FindItemIndexByValue(cte.Id_Mon.ToString());
                    cmbMoneda.Text = cmbMoneda.FindItemByValue(cte.Id_Mon.ToString()).Text;
                    txtCobranza.Text = cte.Cte_LimCobr.ToString();
                    if (cte.Cte_RHoraam1 == string.Empty)
                        txtRHoraam1.SelectedDate = null;
                    else
                        txtRHoraam1.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + cte.Cte_RHoraam1);

                    if (cte.Cte_RHoraam2 == string.Empty)
                        txtRHoraam2.SelectedDate = null;
                    else
                        txtRHoraam2.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + cte.Cte_RHoraam2);

                    if (cte.Cte_RHorapm1 == string.Empty)
                        txtRHorapm1.SelectedDate = null;
                    else
                        txtRHorapm1.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + cte.Cte_RHorapm1);

                    if (cte.Cte_RHorapm2 == string.Empty)
                        txtRHorapm2.SelectedDate = null;
                    else
                        txtRHorapm2.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + cte.Cte_RHorapm2);
                    chkRLunes.Checked = cte.Cte_RLunes;
                    chkRMartes.Checked = cte.Cte_RMartes;
                    chkRMiercoles.Checked = cte.Cte_RMiercoles;
                    chkRJueves.Checked = cte.Cte_RJueves;
                    chkRViernes.Checked = cte.Cte_RViernes;
                    chkRSabado.Checked = cte.Cte_RSabado;
                    chkRDomingo.Checked = cte.Cte_RDomingo;
                    txtDias1.Text = cte.Cte_CondPago.ToString();
                    chkPLunes.Checked = cte.Cte_CPLunes;
                    chkPMartes.Checked = cte.Cte_CPMartes;
                    chkPMiercoles.Checked = cte.Cte_CPMiercoles;
                    chkPJueves.Checked = cte.Cte_CPJueves;
                    chkPViernes.Checked = cte.Cte_CPViernes;
                    chkPSabado.Checked = cte.Cte_CPSabado;
                    chkPDomingo.Checked = cte.Cte_CPDomingo;

                    chkComisiones.Checked = cte.Cte_Comisiones;
                    chkDesglose.Checked = cte.Cte_DesgIva;
                    chkRetencion.Checked = cte.Cte_RetIva;
                    ChkPorcientoIVA.Checked = cte.BPorcientoIVA;

                    if (cte.Id_Corp <= 0)
                        numCorporativo.Text = "";
                    else
                        numCorporativo.Text = cte.Id_Corp.ToString();
                    cmbCorporativa.SelectedIndex = cmbCorporativa.FindItemByValue(cte.Id_Corp.ToString()) == null ? 0 : cmbCorporativa.FindItemIndexByValue(cte.Id_Corp.ToString());
                    cmbCorporativa.Text = cmbCorporativa.FindItemByValue(cte.Id_Corp.ToString()) == null ? cmbCorporativa.Items[0].Text : cmbCorporativa.FindItemByValue(cte.Id_Corp.ToString()).Text;

                    cmbSerie.SelectedIndex = cmbSerie.FindItemIndexByValue(cte.Id_Cfe.ToString());
                    cmbSerie.Text = cmbSerie.FindItemByValue(cte.Id_Cfe.ToString()) == null ? "" : cmbSerie.FindItemByValue(cte.Id_Cfe.ToString()).Text;

                    cmbAsignacion.SelectedIndex = cmbAsignacion.FindItemIndexByValue(cte.Cte_AsignacionPed.ToString());
                    cmbAsignacion.Text = cmbAsignacion.FindItemByValue(cte.Cte_AsignacionPed.ToString()) == null ? "" : cmbAsignacion.FindItemByValue(cte.Cte_AsignacionPed.ToString()).Text;

                    cmbAdenda.SelectedIndex = cmbAdenda.FindItemByValue(cte.Id_Ade.ToString()) == null ? 0 : cmbAdenda.FindItemIndexByValue(cte.Id_Ade.ToString());
                    cmbAdenda.Text = cmbAdenda.FindItemByValue(cte.Id_Ade.ToString()) == null ? cmbAdenda.Items[0].Text : cmbAdenda.FindItemByValue(cte.Id_Ade.ToString()).Text;

                    rdActivo.Checked = cte.Estatus;

                    cmbNCargo.SelectedIndex = cmbNCargo.FindItemByValue(cte.Cte_SerieNCa.ToString()) == null ? 0 : cmbNCargo.FindItemIndexByValue(cte.Cte_SerieNCa.ToString());
                    cmbNCargo.Text = cmbNCargo.FindItemByValue(cte.Cte_SerieNCa.ToString()) == null ? cmbNCargo.Items[0].Text : cmbNCargo.FindItemByValue(cte.Cte_SerieNCa.ToString()).Text;

                    cmbNCredito.SelectedIndex = cmbNCredito.FindItemByValue(cte.Cte_SerieNCre.ToString()) == null ? 0 : cmbNCredito.FindItemIndexByValue(cte.Cte_SerieNCre.ToString());
                    cmbNCredito.Text = cmbNCredito.FindItemByValue(cte.Cte_SerieNCre.ToString()) == null ? cmbNCredito.Items[0].Text : cmbNCredito.FindItemByValue(cte.Cte_SerieNCre.ToString()).Text;

                    chkCredSuspendido.Checked = cte.Cte_CreditoSuspendido;
                    if (cte.Cte_PHoraam1 == string.Empty)
                        txtPHoraam1.SelectedDate = null;
                    else
                        txtPHoraam1.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + cte.Cte_PHoraam1);

                    if (cte.Cte_PHoraam2 == string.Empty)
                        txtPHoraam2.SelectedDate = null;
                    else
                        txtPHoraam2.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + cte.Cte_PHoraam2);

                    if (cte.Cte_PHorapm1 == string.Empty)
                        txtPHorapm1.SelectedDate = null;
                    else
                        txtPHorapm1.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + cte.Cte_PHorapm1);

                    if (cte.Cte_PHorapm2 == string.Empty)
                        txtPHorapm2.SelectedDate = null;
                    else
                        txtPHorapm2.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + cte.Cte_PHorapm2);

                    txtSemana.Text = cte.Cte_SemRec.ToString();
                    txtSemanaRevision.Text = cte.Cte_SemRev.ToString();
                    txtSemanaPago.Text = cte.Cte_SemCob.ToString();
                    chkRecLunes.Checked = cte.Cte_RecLunes;
                    chkRecMartes.Checked = cte.Cte_RecMartes;
                    chkRecMiercoles.Checked = cte.Cte_RecMiercoles;
                    chkRecJueves.Checked = cte.Cte_RecJueves;
                    chkRecViernes.Checked = cte.Cte_RecViernes;
                    chkRecSabado.Checked = cte.Cte_RecSabado;
                    chkRecDomingo.Checked = cte.Cte_RecDomingo;
                    chkEfectivo.Checked = cte.Cte_Efectivo;
                    chkFactoraje.Checked = cte.Cte_Factoraje;
                    chkCheque.Checked = cte.Cte_Cheque;
                    chkTransferencia.Checked = cte.Cte_Transferencia;
                    chkOrdenCompra.Checked = cte.Cte_ReqOrdenCompra;
                    txtDocumentos.Text = cte.Cte_Documentos.Replace("'", "");
                    txtTelCobranza1.Text = cte.Cte_TelCobranza1;
                    txtTelCobranza2.Text = cte.Cte_TelCobranza2;

                    cmbRemisionElect.SelectedIndex = cmbRemisionElect.FindItemIndexByValue(cte.Cte_RemisionElectronica.ToString());
                    cmbRemisionElect.Text = cmbRemisionElect.FindItemByValue(cte.Cte_RemisionElectronica.ToString()).Text;

                    chkNotaCredFac.Checked = cte.BPorcNotaCredito;
                    txtPorcFacturar.Text = cte.PorcientoNotaCredito.ToString();
                    txtPorcRetencion.Text = cte.PorcientoRetencion.ToString();

                    txtPorcientoIVA.Text = cte.PorcientoIVA.ToString();
                    if (ChkPorcientoIVA.Checked == true) txtPorcientoIVA.Enabled = true; else txtPorcientoIVA.Enabled = false; 

                    txtUDigitos.Text = cte.Cte_UDigitos;
                    txtAutorizo.Text = cte.UPlazo;

                    clsCatClientes.ConsultarClienteFormaPago(ref cte, session.Emp_Cnx);
                    CargarFormaPago();
                    foreach (FormaPago fp in cte.FormasPago)
                    {
                        ListFPago.FindItemByValue(fp.Id_Fpa.ToString()).Checked = true;
                    }
                    GetListDet();
                    rgDetalles.Rebind();

                    CN_CatUsuario cn_catusuario = new CN_CatUsuario();
                    Usuario usu = new Usuario();
                    usu.Id_Emp = session.Id_Emp;
                    usu.Id_Cd = session.Id_Cd;
                    usu.Id_U = session.Id_U;
                    cn_catusuario.ConsultaUsuarios(ref usu, session.Emp_Cnx);

                    int dias = cte.Cte_DiasVencidos;
                    chkCredSuspendido.Enabled = usu.U_SusCredito && usu.U_DiasVencimiento >= dias;


                    txtCorreo1.Text = cte.Cte_CorreoEdoCuenta1;
                    txtCorreo2.Text = cte.Cte_CorreoEdoCuenta2;
                    txtCorreo3.Text = cte.Cte_CorreoEdoCuenta3;
                }
                else
                    Nuevo();
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
        protected void rgDetalles_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    double ancho = 0;
                    foreach (GridColumn gc in rgDetalles.Columns)
                    {
                        if (gc.Display)
                        {
                            ancho = ancho + gc.HeaderStyle.Width.Value;
                        }
                    }
                    int extra = 0;
                    if (dt.Rows.Count > 11)
                    {
                        extra = 20;
                    }
                    rgDetalles.Width = Unit.Pixel(Convert.ToInt32(ancho) + extra);
                    rgDetalles.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    rgDetalles.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
        protected void rgDetalles_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Edit":
                        Edit(e);
                        break;
                    case "InitInsert":
                        //Edit(e);
                        break;
                    case "PerformInsert":
                        PerformInsert(e);
                        break;
                    case "Update":
                        Update(e);
                        break;
                    case "Delete":
                        Delete(e);
                        break;
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
        protected void Territorio_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            RadComboBox RadCombo = (sender as RadComboBox);
            RadCombo.SelectedIndex = RadCombo.FindItemIndexByValue((RadCombo.Parent.FindControl("lblold1") as Label).Text);
        }
        protected void Segmento_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            RadComboBox RadCombo = (sender as RadComboBox);
            RadCombo.SelectedIndex = RadCombo.FindItemIndexByValue((RadCombo.Parent.FindControl("lblold2") as Label).Text);
        }
        protected void cmbTerritorio_DataBinding(object sender, EventArgs e)
        {
            try
            {
                if (terr)
                    return;
                terr = true;
                RadComboBox comboBox = ((RadComboBox)sender);// new RadComboBox() ;
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatTerritorio_Combo", ref comboBox);

                //comboBox.Items.Remove(0);
                //comboBox.SelectedIndex = 0;
                //comboBox.SelectedValue = comboBox.Items[0].Value;
                //comboBox.Text = comboBox.Items[0].Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cmbTerritorioDet_TextChanged(object sender, EventArgs e)
        {
            //if (((RadComboBox)sender).SelectedIndex > 0)
            //{
            CargarSegmentoDet(((sender as RadComboBox).Parent.Parent.FindControl("RadComboBox2") as RadComboBox));
            //}
            //else
            //{
            //    (sender as RadComboBox).Focus();
            //}
        }
        protected void cmbSegmento_TextChanged(object sender, EventArgs e)
        {
            
            
            RadComboBox cmb = (sender as RadComboBox);

            if ((Convert.ToInt32(cmb.SelectedValue)) > 0)
            {

                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatTerritorios cn_catter = new CN_CatTerritorios();
                Territorios terr = new Territorios();
                terr.Id_Emp = sesion.Id_Emp;
                terr.Id_Cd = sesion.Id_Cd_Ver;
                terr.Id_Ter = Convert.ToInt32((cmb.Parent.Parent.FindControl("RadComboBox1") as RadComboBox).SelectedValue);
                cn_catter.ConsultaTerritorios(ref terr, sesion.Emp_Cnx);
                //CargarSegmentoDet(((sender as RadComboBox).Parent.Parent.FindControl("RadComboBox2") as RadComboBox));
                (cmb.Parent.Parent.FindControl("lblUENDetEdit") as Label).Text = terr.Uen_Descripcion;
                (cmb.Parent.Parent.FindControl("lblIDUENEdit") as Label).Text = terr.Id_Uen.ToString();
                (cmb.Parent.Parent.FindControl("txtId_Uen") as RadNumericTextBox).Text = terr.Id_Uen.ToString();
                (cmb.Parent.Parent.FindControl("lblRikDetEdit") as Label).Text = terr.Rik_Nombre;
                (cmb.Parent.Parent.FindControl("LabelRikEdit") as Label).Text = terr.Id_Rik.ToString();

                CN_CatSegmentos cn_segmento = new CN_CatSegmentos();
                List<Segmentos> seg = new List<Segmentos>();

                cn_segmento.ConsultaSegmentos(sesion.Id_Emp, Convert.ToInt32(cmb.SelectedValue), sesion.Emp_Cnx, ref seg);
                (cmb.Parent.Parent.FindControl("RadTextBox3") as Label).Text = seg[0].Unidades;
                (cmb.Parent.Parent.FindControl("RadNumericTextBox5") as RadNumericTextBox).Value = seg[0].Valor;//RadTextBox4

                GetTablaPermisosUEN();

                List<int> uen_no_dimension = new List<int>();
                List<int> uen_modifica_potencial = new List<int>();

                foreach (DataRow dr in TablaPermisosUEN.Rows)
                {
                    if (!dr.IsNull("Id_UenDimension"))
                    {
                        int Id_UenPotencial = Convert.ToInt32(dr["Id_UenDimension"].ToString());
                        uen_no_dimension.Add(Id_UenPotencial);
                    }
                    if (!dr.IsNull("Id_UenPotencial"))
                    {
                        int Id_UenPotencial = Convert.ToInt32(dr["Id_UenPotencial"].ToString());
                        uen_modifica_potencial.Add(Id_UenPotencial);
                    }
                }

                //int[] uen_no_dimension = new int[] { 2, 3 };
                //int[] uen_modifica_potencial = new int[] { 2, 3 };

                (cmb.Parent.Parent.FindControl("RadTextBox4") as RadNumericTextBox).Enabled = uen_no_dimension.Contains(terr.Id_Uen);
                (cmb.Parent.Parent.FindControl("RadNumericTextBox6") as RadNumericTextBox).Enabled = uen_modifica_potencial.Contains(terr.Id_Uen);
            }
        }
        protected void cmbSegmento_DataBinding(object sender, EventArgs e)
        {
            try
            {
                RadComboBox cmb = sender as RadComboBox;
                CargarSegmentoDet(cmb);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            DataRow[] Ar_dr;
            Ar_dr = dt.Select("Id_CteDet='" + ((CheckBox)sender).ToolTip.Replace("Cons. ", "") + "'");
            if (Ar_dr.Length > 0)
            {
                Ar_dr[0].BeginEdit();
                Ar_dr[0]["Cte_Activo"] = ((CheckBox)sender).Checked;
                Ar_dr[0].AcceptChanges();
            }
        }
        protected void rdActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked && HF_ID.Value != "")
                if (!Deshabilitar())
                {
                    Alerta("El registro está siendo utilizado por otro componente");
                    ((CheckBox)sender).Checked = true;
                }
        }
        protected void chkActivoDet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox send = (sender as CheckBox);
                string activo = "";
                if (send.Checked)
                {
                    string uenStr = (send.Parent.FindControl("txtId_UenIT") as RadNumericTextBox).Text;
                    int coincidencias = dt.Select("Id_Uen='" + uenStr + "' and Cte_Activo=true").Length;
                    if (coincidencias > 0)
                    {
                        Alerta("No puede haber dos o mas territorios con el mismo UEN activos");
                        (sender as CheckBox).Checked = false;
                        return;
                    }
                    else
                    {
                        activo = "true";
                    }
                }
                else
                {
                    activo = "false";
                }

                DataRow[] Ar_dr;
                string Id_Ter = ((Label)send.Parent.FindControl("Label100")).Text;
                Ar_dr = dt.Select("Id_Ter='" + Id_Ter + "'");
                Ar_dr[0].BeginEdit();
                Ar_dr[0]["Cte_Activo"] = activo;
                Ar_dr[0].AcceptChanges();
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
        protected void chkRetencion_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRetencion.Checked == true)
                txtPorcRetencion.Enabled = true;
            else
                txtPorcRetencion.Enabled = false;
        }
        protected void chkPorcientoIVA_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkPorcientoIVA.Checked == true)
                txtPorcientoIVA.Enabled = true;
            else
                txtPorcientoIVA.Enabled = false;
        }
        #endregion
        #region Funciones
        private void Inicializar()
        {
            CargarCuentasCorporativas();
            CargarTmoneda();
            CargarConsecutivos();
            CargarAsignar();
            CargarAdenda();
            CargarNcre();
            CargarNca();
            CargarFormaPago();
            Nuevo();
            GetListDet();
            rgDetalles.Rebind();
            txtClave.Text = MaximoId();
            txtClave.Focus();

            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];

            CN_CatUsuario cn_catusuario = new CN_CatUsuario();
            Usuario usu = new Usuario();
            usu.Id_Emp = session.Id_Emp;
            usu.Id_Cd = session.Id_Cd;
            usu.Id_U = session.Id_U;
            cn_catusuario.ConsultaUsuarios(ref usu, session.Emp_Cnx);

            int dias = 0;
            chkCredSuspendido.Enabled = usu.U_SusCredito && usu.U_DiasVencimiento >= dias;

        }
        private void CargarSegmentoDet(RadComboBox comboBox)
        {
            if ((comboBox.Parent.Parent.FindControl("RadComboBox1") as RadComboBox).SelectedValue == "")
            {
                return;
            }

            if (Seg || Convert.ToInt32((comboBox.Parent.Parent.FindControl("RadComboBox1") as RadComboBox).SelectedValue) == -1)
                return;
            Seg = true;
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Convert.ToInt32((comboBox.Parent.Parent.FindControl("RadComboBox1") as RadComboBox).SelectedValue), Sesion.Emp_Cnx, "spCatSegmentosTer_Combo", ref comboBox);
            if (comboBox.Items.Count > 1)
            {
                comboBox.SelectedIndex = 1;
                comboBox.Text = comboBox.Items[1].Text;
                cmbSegmento_TextChanged(comboBox, null);

                //(comboBox.Parent.FindControl("LabelSeg_Descripcion") as Label).Text = comboBox.Items[1].Text;
                (comboBox.Parent.FindControl("lblId_Seg") as Label).Text = comboBox.Items[1].Value;

                if ((comboBox.Parent.FindControl("RadTextBox4") as RadNumericTextBox).Enabled)
                {
                    (comboBox.Parent.FindControl("RadTextBox4") as RadNumericTextBox).Focus();
                }
                else
                {
                    (comboBox.Parent.Parent.FindControl("RadNumericTextBox6") as RadNumericTextBox).Focus();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty((comboBox.Parent.Parent.FindControl("txtId_Ter") as RadNumericTextBox).Text))
                {
                    int ter = Convert.ToInt32((comboBox.Parent.Parent.FindControl("txtId_Ter") as RadNumericTextBox).Text);
                    if (ter > 0)
                    {
                        //(comboBox.Parent.FindControl("LabelSeg_Descripcion") as Label).Text = "";
                        (comboBox.Parent.FindControl("lblId_Seg") as Label).Text = "";
                        (comboBox.Parent.Parent.FindControl("lblUENDetEdit") as Label).Text = "";
                        (comboBox.Parent.Parent.FindControl("lblIDUENEdit") as Label).Text = "";
                        (comboBox.Parent.Parent.FindControl("txtId_Uen") as RadNumericTextBox).Text = "";
                        (comboBox.Parent.Parent.FindControl("lblRikDetEdit") as Label).Text = "";
                        (comboBox.Parent.Parent.FindControl("LabelRikEdit") as Label).Text = "";
                        (comboBox.Parent.Parent.FindControl("RadTextBox3") as Label).Text = "";
                        (comboBox.Parent.Parent.FindControl("RadNumericTextBox5") as RadNumericTextBox).Value = 0;
                        AlertaFocus("El territorio no tiene un segmento configurado", ((RadNumericTextBox)comboBox.Parent.Parent.FindControl("txtId_Ter")).ClientID);
                    }
                }
            }
            
            Sesion session2 = new Sesion();
            session2 = (Sesion)Session["Sesion" + Session.SessionID];
            int Tipo_CDC = 0;
            new CN_CatCliente().ConsultaTipoCDC(session2.Id_Cd_Ver, ref Tipo_CDC, session2.Emp_Cnx);            
            if (Tipo_CDC == 2)         
                comboBox.Enabled = true;         
            else
                comboBox.Enabled = false;
        }
        private void CargarFormaPago()
        {//ListFPago
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaListBox(Sesion.Id_Emp, -1, Sesion.Emp_Cnx, "spCatFormaPago_Combo", ref ListFPago);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarCuentasCorporativas()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatCuentaCorp_Combo", ref cmbCorporativa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarClientes()//Local
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_Cd_Ver, (int?)null, Sesion.Id_Rik == -1 ? (int?)null : (int?)Sesion.Id_Rik, Sesion.Emp_Cnx, "spCatCliente_Combo", ref cmbCliente);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarTmoneda() //Central
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatTmoneda_Combo", ref cmbMoneda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarConsecutivos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, 1, Sesion.Emp_Cnx, "spCatConsFactEle_Combo", ref cmbSerie);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarAdenda()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatAdenda_Combo", ref cmbAdenda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarNca()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, 2, Sesion.Emp_Cnx, "spCatConsFactEle_Combo", ref cmbNCargo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarNcre()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, 3, Sesion.Emp_Cnx, "spCatConsFactEle_Combo", ref cmbNCredito);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarAsignar()
        {
            cmbAsignacion.Items.Clear();
            DataTable Dt = new DataTable();
            Dt.Columns.Add("Descripcion");
            Dt.Columns.Add("Id");
            Dt.Rows.Add(new object[] { "-- Seleccionar --", "-1" });
            Dt.Rows.Add(new object[] { "Dependiendo de existencia", "0" });
            Dt.Rows.Add(new object[] { "Sólo partidas completas", "1" });

            cmbAsignacion.DataSource = Dt;
            cmbAsignacion.DataValueField = "Id";
            cmbAsignacion.DataTextField = "Descripcion";
            cmbAsignacion.DataBind();
        }


        private void CargarCuentaNacional(int IdCuenta  = 0)
        {
           
            WS_CuentaNacional.Service1 ws = new WS_CuentaNacional.Service1();
            ws.Url = ConfigurationManager.AppSettings["WS_CuentaNacional"].ToString();
          
          String respuesta = ws.RegresaCN(IdCuenta == 0? "": IdCuenta.ToString());
            XmlDocument Xml = new XmlDocument();
            Xml.LoadXml(respuesta);


            if (IdCuenta == 0)
            {
                cmbRemisionElect.Items.Clear();
            }
            DataTable Dt = new DataTable();
            Dt.Columns.Add("Descripcion");
            Dt.Columns.Add("Id");
            Dt.Rows.Add(new object[] { "-- Seleccionar --", "0" });

            int CuentaContableNacional = 0;
            XmlNodeList ListaCuentaNacionales;
            ListaCuentaNacionales = Xml.GetElementsByTagName("CuentaNacional");
            foreach (XmlElement nodo in ListaCuentaNacionales)
            {
                int i = 0;
                string nCliente = nodo.Attributes["CliNum"].Value.ToString();
                string nClienteNombre = nodo.Attributes["CliNom"].Value.ToString();                
                Dt.Rows.Add(new object[] { nClienteNombre, nCliente });
                if (IdCuenta > 0)
                {
                    CuentaContableNacional = Int32.Parse(nodo.Attributes["CtaCont"].Value);
                }
            }

            HiddenCteNumCuentaContNal.Value = CuentaContableNacional == 0 ? "" : CuentaContableNacional.ToString();

            if (IdCuenta == 0)
            {
                cmbRemisionElect.DataSource = Dt;
                cmbRemisionElect.DataValueField = "Id";
                cmbRemisionElect.DataTextField = "Descripcion";
                cmbRemisionElect.DataBind();
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
                    CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string MaximoId()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, "CatCliente", "Id_Cte", Sesion.Emp_Cnx, "spCatLocal_Maximo");
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
                    if (Permiso.PGrabar == false)
                        this.rtb1.Items[6].Visible = false;
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                        this.rtb1.Items[5].Visible = false;
                    //Regresar
                    this.rtb1.Items[4].Visible = false;
                    //Eliminar
                    this.rtb1.Items[3].Visible = false;
                    //Imprimir
                    this.rtb1.Items[2].Visible = false;
                    //Correo
                    this.rtb1.Items[1].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");
                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GetListDet()
        {
            try
            {
                dt = new DataTable();
                dt.Columns.Add("Id_CteDet", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_Ter", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Ter_Nombre", System.Type.GetType("System.String"));
                dt.Columns.Add("Id_Seg", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Seg_Descripcion", System.Type.GetType("System.String"));
                dt.Columns.Add("Cte_UnidadDim", System.Type.GetType("System.String"));
                dt.Columns.Add("Cte_Dimension", System.Type.GetType("System.String"));
                dt.Columns.Add("Cte_Pesos", System.Type.GetType("System.Double"));
                dt.Columns.Add("Cte_Potencial", System.Type.GetType("System.Double"));
                dt.Columns.Add("Cte_Activo", System.Type.GetType("System.Boolean"));
                dt.Columns.Add("Id_Rik", System.Type.GetType("System.String"));
                dt.Columns.Add("rik", System.Type.GetType("System.String"));

                dt.Columns.Add("uen", System.Type.GetType("System.String"));
                dt.Columns.Add("Cte_ManoObra", System.Type.GetType("System.Double"));
                dt.Columns.Add("Cte_GastoTerritorio", System.Type.GetType("System.Double"));
                dt.Columns.Add("Cte_FletePaga", System.Type.GetType("System.Double"));
                dt.Columns.Add("Cte_PorcComision", System.Type.GetType("System.Double"));
                dt.Columns.Add("Id_Uen", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Editar", System.Type.GetType("System.Boolean"));
                if (HF_ID.Value != "")
                {
                    CN_CatCliente clsCatCliente = new CN_CatCliente();
                    Sesion session2 = new Sesion();
                    session2 = (Sesion)Session["Sesion" + Session.SessionID];
                    ClienteDet clientedet = new ClienteDet();
                    clientedet.Id_Emp = session2.Id_Emp;
                    clientedet.Id_Cd = session2.Id_Cd_Ver;
                    clientedet.Id_Cte = Convert.ToInt32(HF_ID.Value);
                    DataTable dt2 = dt;
                    clsCatCliente.ConsultaClienteDet(clientedet, session2.Emp_Cnx, ref dt2);
                    dt = dt2;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void habilitarcomboSegmentos()
        {
            
           
         
        }

        private void GetTablaPermisosUEN()
        {
            try
            {
                TablaPermisosUEN = new DataTable();
                TablaPermisosUEN.Columns.Add("Id_UenPermiso", System.Type.GetType("System.Int32"));
                TablaPermisosUEN.Columns.Add("Id_UenPotencial", System.Type.GetType("System.Int32"));
                TablaPermisosUEN.Columns.Add("Id_UenDimension", System.Type.GetType("System.Int32"));

                CN_CatCliente clsCatCliente = new CN_CatCliente();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                DataTable dt2 = TablaPermisosUEN;
                clsCatCliente.ConsultaPermisosUEN(ref dt2, session2.Emp_Cnx);
                TablaPermisosUEN = dt2;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Nuevo()
        {
            cmbCliente.SelectedIndex = -1;
            cmbCliente.Text = "";
            HF_ID.Value = string.Empty;
            txtClave.Text = Valor;
            txtClave.Enabled = true;
            numCorporativo.Text = string.Empty;
            txtNombreCorto.Text = string.Empty;
            txtCobranza.Text = string.Empty;
            txtDcontacto.Text = string.Empty;
            txtDcalle.Text = string.Empty;
            txtDciudad.Text = string.Empty;
            txtDcolonia.Text = string.Empty;
            txtDcp.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtDestado.Text = string.Empty;
            txtDias1.Text = string.Empty;
            txtDmunicipio.Text = string.Empty;
            txtDnumero.Text = string.Empty;
            txtDrfc.Text = string.Empty;
            txtDtelefono.Text = string.Empty;
            txtDfax.Text = string.Empty;
            txtFcalle.Text = string.Empty;
            txtFciudad.Text = string.Empty;
            txtFcolonia.Text = string.Empty;
            txtFcp.Text = string.Empty;
            txtFestado.Text = string.Empty;
            txtFmunicipio.Text = string.Empty;
            txtFnumero.Text = string.Empty;
            txtFrfc.Text = string.Empty;
            txtFtelefono.Text = string.Empty;
            txtmail.Text = string.Empty;
            txtDocumentos.Text = string.Empty;

            txtReferencia.Text = string.Empty;
            txtUDigitos.Text = string.Empty;
            cmbRemisionElect.SelectedIndex = 0;
            cmbRemisionElect.Text = cmbAdenda.Items[0].Text;

            cmbAdenda.SelectedIndex = 0;
            cmbAdenda.Text = cmbAdenda.Items[0].Text;
            cmbAsignacion.SelectedIndex = 0;
            cmbAsignacion.Text = cmbAsignacion.Items[0].Text;
            cmbCorporativa.SelectedIndex = 0;
            cmbCorporativa.Text = cmbCorporativa.Items[0].Text;

            cmbCliente.SelectedIndex = 0;
            if (cmbCliente.FindItemByValue("-1") != null)
                cmbCliente.Text = cmbCliente.FindItemByValue("-1").Text;
            cmbMoneda.SelectedIndex = 0;
            cmbMoneda.Text = cmbMoneda.Items[0].Text;
            cmbSerie.SelectedIndex = 0;
            cmbSerie.Text = cmbSerie.Items[0].Text;
            cmbNCredito.SelectedIndex = 0;
            cmbNCredito.Text = cmbNCredito.Items[0].Text;
            cmbNCargo.SelectedIndex = 0;
            cmbNCargo.Text = cmbNCargo.Items[0].Text;

            rdActivo.Checked = true;
            chkComisiones.Checked = false;
            chkCredito.Checked = false;
            chkDesglose.Checked = false;
            chkFacturar.Checked = false;
            chkPDomingo.Checked = false;
            chkPJueves.Checked = false;
            chkPLunes.Checked = false;
            chkPMartes.Checked = false;
            chkPMiercoles.Checked = false;
            chkPSabado.Checked = false;
            chkPViernes.Checked = false;
            chkRDomingo.Checked = false;
            chkRetencion.Checked = false;
            ChkPorcientoIVA.Checked = false;
            chkRJueves.Checked = false;
            chkRLunes.Checked = false;
            chkRMartes.Checked = false;
            chkRMiercoles.Checked = false;
            chkRSabado.Checked = false;
            chkRViernes.Checked = false;
            chkEfectivo.Checked = false;
            chkFactoraje.Checked = false;
            chkTransferencia.Checked = false;
            chkCheque.Checked = false;
            chkOrdenCompra.Checked = false;
            chkCredSuspendido.Checked = false;

            txtRHoraam1.SelectedDate = null;
            txtRHoraam2.SelectedDate = null;
            txtRHorapm1.SelectedDate = null;
            txtRHorapm2.SelectedDate = null;

            txtPHoraam1.SelectedDate = null;
            txtPHoraam2.SelectedDate = null;
            txtPHorapm1.SelectedDate = null;
            txtPHorapm2.SelectedDate = null;
            chkNotaCredFac.Checked = false;
            txtPorcFacturar.Text = string.Empty;

            txtPorcRetencion.Text = string.Empty;
            txtPorcientoIVA.Text = string.Empty;

            txtCorreo1.Text = "";
            txtCorreo2.Text = "";
            txtCorreo3.Text = "";

            GetListDet();
            rgDetalles.Rebind();

            this.RadTabStripPrincipal.SelectedIndex = 0;
            RadMultiPagePrincipal.SelectedIndex = 0;


            foreach (RadListBoxItem rbli in ListFPago.Items)
            {
                rbli.Checked = false;

            }


            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];

            CN_CatUsuario cn_catusuario = new CN_CatUsuario();
            Usuario usu = new Usuario();
            usu.Id_Emp = session.Id_Emp;
            usu.Id_Cd = session.Id_Cd;
            usu.Id_U = session.Id_U;
            cn_catusuario.ConsultaUsuarios(ref usu, session.Emp_Cnx);

            int dias = 0;
            chkCredSuspendido.Enabled = usu.U_SusCredito && usu.U_DiasVencimiento >= dias;
        }
        private void Guardar(int? id_U, int? id_Cd)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];


                if (!chkRLunes.Checked && !chkRMartes.Checked && !chkRMiercoles.Checked && !chkRJueves.Checked && !chkRViernes.Checked && !chkRSabado.Checked && !chkRDomingo.Checked)
                {
                    Alerta("No se ha seleccionado ningún día de revisión");
                    RadTabStripPrincipal.Tabs[2].Selected = true;
                    RadPageViewParametros.Selected = true;
                    return;
                }
                if (!chkPLunes.Checked && !chkPMartes.Checked && !chkPMiercoles.Checked && !chkPJueves.Checked && !chkPViernes.Checked && !chkPSabado.Checked && !chkPDomingo.Checked)
                {
                    Alerta("No se ha seleccionado ningún día de pago");
                    RadTabStripPrincipal.Tabs[2].Selected = true;
                    RadPageViewParametros.Selected = true;
                    return;
                }
                if (!chkRecLunes.Checked && !chkRecMartes.Checked && !chkRecMiercoles.Checked && !chkRecJueves.Checked && !chkRecViernes.Checked && !chkRecSabado.Checked && !chkRecDomingo.Checked)
                {

                    Alerta("No se ha seleccionado ningún día de recepción");
                    RadTabStripPrincipal.Tabs[2].Selected = true;
                    RadPageViewParametros.Selected = true;

                    return;
                }
                if (ChkPorcientoIVA.Checked == true)
                {
                    if (txtPorcientoIVA.Value == 0 || txtPorcientoIVA.Value == null)
                    {
                        Alerta("El IVA del Cliente no puede ser CERO");
                        return;
                    }
                }
                if (!txtDias1.Value.HasValue)
                {
                    Alerta("No se ha capturado la condición de pago");
                    RadTabStripPrincipal.Tabs[2].Selected = true;
                    RadPageViewParametros.Selected = true;
                    return;
                }

                Clientes clientes = new Clientes();
                clientes.Id_Emp = session.Id_Emp;
                clientes.Id_Cd = session.Id_Cd_Ver;
                clientes.Id_Mon = Convert.ToInt32(cmbMoneda.SelectedValue);
                clientes.Id_Cfe = Convert.ToInt32(cmbSerie.SelectedValue);
                clientes.Cte_NomComercial = txtDescripcion.Text;
                clientes.Id_Corp = Convert.ToInt32(cmbCorporativa.SelectedValue);
                clientes.Cte_NomCorto = txtNombreCorto.Text;
                clientes.Cte_FacCalle = txtFcalle.Text;
                clientes.Cte_FacNumero = txtFnumero.Text;
                clientes.Cte_FacCp = txtFcp.Text;
                clientes.Cte_FacColonia = txtFcolonia.Text;
                clientes.Cte_FacMunicipio = txtFmunicipio.Text;
                clientes.Cte_FacTel = txtFtelefono.Text;
                clientes.Cte_FacRfc = txtFrfc.Text;
                clientes.Cte_FacEstado = txtFestado.Text;
                clientes.Cte_Calle = txtDcalle.Text;
                clientes.Cte_Numero = txtDnumero.Text;
                clientes.Cte_Cp = txtDcp.Text;
                clientes.Cte_Colonia = txtDcolonia.Text;
                clientes.Cte_Municipio = txtDmunicipio.Text;
                clientes.Cte_DRfc = txtDrfc.Text;
                clientes.Cte_Estado = txtDestado.Text;
                clientes.Cte_Telefono = txtDtelefono.Text;
                clientes.Cte_Fax = txtDfax.Text;
                clientes.Cte_Contacto = txtDcontacto.Text;
                clientes.Cte_Email = txtmail.Text;
                clientes.Cte_Referencia = txtReferencia.Text;
                clientes.Cte_Credito = chkCredito.Checked;
                clientes.Cte_Facturacion = chkFacturar.Checked;
                clientes.Cte_LimCobr = txtCobranza.Text == string.Empty ? 0 : Convert.ToDouble(txtCobranza.Text);
                clientes.Cte_RHoraam1 = txtRHoraam1.SelectedDate == null ? string.Empty : txtRHoraam1.SelectedDate.Value.ToString("HH:mm");
                clientes.Cte_RHoraam2 = txtRHoraam2.SelectedDate == null ? string.Empty : txtRHoraam2.SelectedDate.Value.ToString("HH:mm");
                clientes.Cte_RHorapm1 = txtRHorapm1.SelectedDate == null ? string.Empty : txtRHorapm1.SelectedDate.Value.ToString("HH:mm");
                clientes.Cte_RHorapm2 = txtRHorapm2.SelectedDate == null ? string.Empty : txtRHorapm2.SelectedDate.Value.ToString("HH:mm");

                clientes.Cte_RLunes = chkRLunes.Checked;
                clientes.Cte_RMartes = chkRMartes.Checked;
                clientes.Cte_RMiercoles = chkRMiercoles.Checked;
                clientes.Cte_RJueves = chkRJueves.Checked;
                clientes.Cte_RViernes = chkRViernes.Checked;
                clientes.Cte_RSabado = chkRSabado.Checked;
                clientes.Cte_RDomingo = chkRDomingo.Checked;
                clientes.Cte_CondPago = txtDias1.Text == string.Empty ? 0 : Convert.ToInt32(txtDias1.Text);
                clientes.Cte_CPLunes = chkPLunes.Checked;
                clientes.Cte_CPMartes = chkPMartes.Checked;
                clientes.Cte_CPMiercoles = chkPMiercoles.Checked;
                clientes.Cte_CPJueves = chkPJueves.Checked;
                clientes.Cte_CPViernes = chkPViernes.Checked;
                clientes.Cte_CPSabado = chkPSabado.Checked;
                clientes.Cte_CPDomingo = chkPDomingo.Checked;
                clientes.Cte_Comisiones = chkComisiones.Checked;
                clientes.Cte_DesgIva = chkDesglose.Checked;
                clientes.Cte_RetIva = chkRetencion.Checked;
                clientes.BPorcientoIVA = ChkPorcientoIVA.Checked;
                clientes.Cte_SerieNCre = Convert.ToInt32(cmbNCredito.SelectedValue);
                clientes.Cte_SerieNCa = Convert.ToInt32(cmbNCargo.SelectedValue);
                clientes.Cte_AsignacionPed = Convert.ToInt32(cmbAsignacion.SelectedValue);
                clientes.Id_Ade = Convert.ToInt32(cmbAdenda.SelectedValue);
                clientes.Estatus = rdActivo.Checked;
                clientes.Cte_NumCuentaContNacional = string.IsNullOrEmpty(HiddenCteNumCuentaContNal.Value)  ? 0 : Int32.Parse(HiddenCteNumCuentaContNal.Value);

                clientes.Cte_CreditoSuspendido = chkCredSuspendido.Checked;

                if (txtRHoraam1.SelectedDate > txtRHoraam2.SelectedDate)
                {
                    Alerta("La 1era. hora de revisión a.m. no debe ser mayor que la 2da. hora a.m");
                    return;
                }
                if (txtRHorapm1.SelectedDate > txtRHorapm2.SelectedDate)
                {
                    Alerta("La 1era. hora de revisión p.m. no debe ser mayor que la 2da. hora p.m.");
                    return;
                }
                if (txtPHoraam1.SelectedDate > txtPHoraam2.SelectedDate)
                {
                    Alerta("La 1era. hora de pago a.m. no debe ser mayor que la 2da. hora de pago a.m");
                    return;
                }
                if (txtPHorapm1.SelectedDate > txtPHorapm2.SelectedDate)
                {
                    Alerta("La 1era. hora de pago p.m. no debe ser mayor que la 2da. hora de pago p.m.");
                    return;
                }

                clientes.Cte_PHoraam1 = txtPHoraam1.SelectedDate == null ? string.Empty : txtPHoraam1.SelectedDate.Value.ToString("HH:mm");
                clientes.Cte_PHoraam2 = txtPHoraam2.SelectedDate == null ? string.Empty : txtPHoraam2.SelectedDate.Value.ToString("HH:mm");
                clientes.Cte_PHorapm1 = txtPHorapm1.SelectedDate == null ? string.Empty : txtPHorapm1.SelectedDate.Value.ToString("HH:mm");
                clientes.Cte_PHorapm2 = txtPHorapm2.SelectedDate == null ? string.Empty : txtPHorapm2.SelectedDate.Value.ToString("HH:mm");

                clientes.Cte_SemRec = txtSemana.Text == "" ? 0 : Convert.ToInt32(txtSemana.Text);
                clientes.Cte_SemRev = txtSemanaRevision.Text == "" ? 0 : Convert.ToInt32(txtSemanaRevision.Text);
                clientes.Cte_SemCob= txtSemanaPago.Text == "" ? 0 : Convert.ToInt32(txtSemanaPago.Text);
                clientes.Cte_RecLunes = chkRecLunes.Checked;
                clientes.Cte_RecMartes = chkRecMartes.Checked;
                clientes.Cte_RecMiercoles = chkRecMiercoles.Checked;
                clientes.Cte_RecJueves = chkRecJueves.Checked;
                clientes.Cte_RecViernes = chkRecViernes.Checked;
                clientes.Cte_RecSabado = chkRecSabado.Checked;
                clientes.Cte_RecDomingo = chkRecDomingo.Checked;
                clientes.Cte_Efectivo = chkEfectivo.Checked;
                clientes.Cte_Factoraje = chkFactoraje.Checked;
                clientes.Cte_Cheque = chkCheque.Checked;
                clientes.Cte_Transferencia = chkTransferencia.Checked;
                clientes.Cte_ReqOrdenCompra = chkOrdenCompra.Checked;
                clientes.Cte_Documentos = txtDocumentos.Text.Replace("'", "");
                clientes.Cte_TelCobranza1 = txtTelCobranza1.Text;
                clientes.Cte_TelCobranza2 = txtTelCobranza2.Text;
                clientes.Cte_RemisionElectronica = Convert.ToInt32(cmbRemisionElect.SelectedValue);
                clientes.BPorcNotaCredito = chkNotaCredFac.Checked;
                clientes.PorcientoNotaCredito = txtPorcFacturar.Value.HasValue ? txtPorcFacturar.Value.Value : 0;
                clientes.PorcientoRetencion = txtPorcRetencion.Value.HasValue ? txtPorcRetencion.Value.Value : 0;
                clientes.PorcientoIVA = txtPorcientoIVA.Value.HasValue ? Convert.ToInt32(txtPorcientoIVA.Value.Value) : 0;                
                clientes.Cte_UDigitos = txtUDigitos.Text;
                clientes.Id_U = session.Id_U;
                clientes.Id_UCd = session.Id_Cd;
                clientes.Db = (new SqlConnectionStringBuilder(session.Emp_Cnx)).InitialCatalog;
                clientes.Db_Cobranza = (new SqlConnectionStringBuilder(Emp_CnxCob)).InitialCatalog;

                clientes.Cte_AutorizaPlazo_IdCd = id_Cd;
                clientes.Cte_AutorizaPlazo_IdU = id_U;

                clientes.Cte_CorreoEdoCuenta1 = txtCorreo1.Text;
                clientes.Cte_CorreoEdoCuenta2 = txtCorreo2.Text;
                clientes.Cte_CorreoEdoCuenta3 = txtCorreo3.Text;

                List<FormaPago> listFP = new List<FormaPago>();
                FormaPago ItemFP;
                foreach (RadListBoxItem dr in ListFPago.Items)
                {
                    if (dr.Checked)
                    {
                        ItemFP = new FormaPago();
                        ItemFP.Id_Fpa = Convert.ToInt32(dr.Value);
                        listFP.Add(ItemFP);
                    }
                }
                clientes.FormasPago = listFP;

                bool continuar = false;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Id_Uen"] != DBNull.Value)
                    {
                        if (dt.Select("Id_Uen='" + dr["Id_Uen"] + "' and Cte_Activo=true").Length > 1)
                        {
                            Alerta("No puede haber dos o mas territorios con el mismo UEN activos");
                            return;
                        }
                        if (Convert.ToBoolean(dr["Cte_Activo"]))
                        {
                            continuar = true;
                        }
                    }
                    else
                    {
                        continuar = true;
                    }
                }

                if (!continuar)
                {
                    Alerta("No tiene territorios activos");
                    return;
                }

                CN_CatCliente clsCatClientes = new CN_CatCliente();
                int verificador = -1;
                if (HF_ID.Value == "")
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }
                    clientes.Id_Cte = Convert.ToInt32(txtClave.Text);
                    clsCatClientes.InsertarClientes(clientes, session.Emp_Cnx, ref verificador);
                    if (verificador == 1)
                    {
                        clsCatClientes.InsertarCteFormaPago(clientes, session.Emp_Cnx, ref verificador);
                        clsCatClientes.InsertarClientesDet(clientes, dt, session.Emp_Cnx);
                        Nuevo();
                        Alerta("Los datos se guardaron correctamente");
                    }
                    else
                        Alerta("La clave ya existe");
                    CargarClientes();
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }
                    clientes.Id_Cte = Convert.ToInt32(HF_ID.Value);
                    clsCatClientes.ModificarClientes(clientes, session.Emp_Cnx, ref verificador);
                    if (verificador == 1)
                    {
                        clsCatClientes.InsertarClientesDet(clientes, dt, session.Emp_Cnx);
                        Alerta("Los datos se modificaron correctamente");
                    }
                    else
                        Alerta("Ocurrió un error al intentar modificar los datos");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Update(GridCommandEventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            int Id_CteDet = 0;
            int Id_Ter = 0;
            string Ter_Nombre = "";
            int Id_Seg = 0;
            int uen = 0;
            string Seg_Descripcion = "";
            string Cte_UnidadDim = "";
            string Cte_Dimension = "";
            string Rik = "";
            string Uen = "";
            double Cte_Pesos = 0;
            double Cte_CarMP = 0;
            double Cte_GastoVarT = 0;
            double Cte_FletePaga = 0;
            double Cte_Potencial = 0;
            double Cte_PorcComision = 0;
            bool Cte_Activo = false;
            GridItem gi = null;

            DataRow[] Ar_dr;
            gi = e.Item;
            if (((RadComboBox)gi.FindControl("RadComboBox1")).Text == "" ||
              ((RadComboBox)gi.FindControl("RadComboBox2")).Text == "" ||
              ((Label)gi.FindControl("RadTextBox3")).Text == "" ||
              ((RadNumericTextBox)gi.FindControl("RadTextBox4")).Text == "" ||
              ((RadNumericTextBox)gi.FindControl("RadNumericTextBox5")).Text == "")
            {
                e.Canceled = true;
                Alerta("Todos los campos son requeridos");
                return;
            }

            Id_Ter = Convert.ToInt32(((RadComboBox)gi.FindControl("RadComboBox1")).SelectedValue);
            Ter_Nombre = ((RadComboBox)gi.FindControl("RadComboBox1")).Text;
            Id_Seg = Convert.ToInt32(((RadComboBox)gi.FindControl("RadComboBox2")).SelectedValue);
            Seg_Descripcion = ((RadComboBox)gi.FindControl("RadComboBox2")).Text;
            Cte_UnidadDim = ((Label)gi.FindControl("RadTextBox3")).Text;
            Cte_Dimension = ((RadNumericTextBox)gi.FindControl("RadTextBox4")).Text;
            Cte_Pesos = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox5")).Text);
            Cte_Potencial = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox6")).Text);
            Id_CteDet = Convert.ToInt32(((Label)gi.FindControl("Label0")).Text);
            Cte_Activo = ((CheckBox)gi.FindControl("chk8")).Checked;
            Cte_CarMP = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox7")).Text);
            Cte_GastoVarT = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox8")).Text);
            Cte_FletePaga = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox9")).Text);
            Cte_PorcComision = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtComision")).Text);
            uen = !string.IsNullOrEmpty(((Label)gi.FindControl("lblIDUENEdit")).Text) ? Convert.ToInt32(((Label)gi.FindControl("lblIDUENEdit")).Text) : 0;

            if (Cte_Potencial == 0)
            {
                AlertaFocus("El potencial no debe ser cero", ((RadNumericTextBox)gi.FindControl("RadNumericTextBox6")).ClientID);
                e.Canceled = true;
                return;
            }

            CN_CatTerritorios cn_catter = new CN_CatTerritorios();
            Territorios terr = new Territorios();
            terr.Id_Emp = sesion.Id_Emp;
            terr.Id_Cd = sesion.Id_Cd_Ver;
            terr.Id_Ter = Id_Ter;
            cn_catter.ConsultaTerritorios(ref terr, sesion.Emp_Cnx);
            Rik = terr.Rik_Nombre;
            Uen = terr.Uen_Descripcion;
            uen = terr.Id_Uen;

            Ar_dr = dt.Select("Id_CteDet='" + Id_CteDet + "'");
            if (Ar_dr.Length > 0)
            {
                DataRow dr;
                if (dt.Rows.Count > 0)
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        int UEN = !string.IsNullOrEmpty(dr["Id_Uen"].ToString()) ? Convert.ToInt32(dr["Id_Uen"].ToString()) : 0;
                        int id_ctedet = !string.IsNullOrEmpty(dr["Id_CteDet"].ToString()) ? Convert.ToInt32(dr["Id_CteDet"].ToString()) : 0;
                        bool activo = Convert.ToBoolean(dr["Cte_Activo"]);
                       /* if (Id_CteDet != id_ctedet)
                            if (UEN == uen && Cte_Activo)
                            {
                                e.Canceled = true;
                                Alerta("La UEN ya está registrada con otro territorio");
                                return;
                            }*/
                    }
                Ar_dr[0].BeginEdit();
                Ar_dr[0]["Id_Ter"] = Id_Ter;
                Ar_dr[0]["Ter_Nombre"] = Ter_Nombre;
                Ar_dr[0]["Id_Seg"] = Id_Seg;
                Ar_dr[0]["Seg_Descripcion"] = Seg_Descripcion;
                Ar_dr[0]["Cte_UnidadDim"] = Cte_UnidadDim;
                Ar_dr[0]["Cte_Dimension"] = Convert.ToDouble(Cte_Dimension).ToString("#,##0.00");
                Ar_dr[0]["Cte_Pesos"] = Cte_Pesos;
                Ar_dr[0]["Cte_Potencial"] = Cte_Potencial;
                Ar_dr[0]["Cte_Activo"] = Cte_Activo;
                Ar_dr[0]["rik"] = Rik;
                Ar_dr[0]["uen"] = Uen;
                Ar_dr[0]["Cte_ManoObra"] = Cte_CarMP;
                Ar_dr[0]["Cte_GastoTerritorio"] = Cte_GastoVarT;
                Ar_dr[0]["Cte_FletePaga"] = Cte_FletePaga;
                Ar_dr[0]["Cte_PorcComision"] = Cte_PorcComision;
                Ar_dr[0]["Id_Uen"] = uen;
                Ar_dr[0].AcceptChanges();
            }
        }
        private void PerformInsert(GridCommandEventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];

            int Id_CteDet = 0;
            int Id_Ter = 0;
            int Id_Seg = 0;
            int id_uen = 0;
            string Ter_Nombre = "";
            string Seg_Descripcion = "";
            string Cte_UnidadDim = "";
            string Cte_Dimension = "";
            int Id_Rik = 0;
            string Rik = "";
            string Uen = "";
            double Cte_Pesos = 0;
            double Cte_CarMP = 0;
            double Cte_GastoVarT = 0;
            double Cte_FletePaga = 0;
            double Cte_PorcComision = 0;
            double Cte_Potencial = 0;
            bool Cte_Activo = false;
            GridItem gi = null;
            gi = e.Item;
            if (((RadComboBox)gi.FindControl("RadComboBox1")).Text == "" ||
                ((RadComboBox)gi.FindControl("RadComboBox2")).Text == "" ||
                ((Label)gi.FindControl("RadTextBox3")).Text == "" ||
                ((RadNumericTextBox)gi.FindControl("RadTextBox4")).Text == "" ||
                ((RadNumericTextBox)gi.FindControl("RadNumericTextBox5")).Text == "")
            {
                e.Canceled = true;
                Alerta("Todos los campos son requeridos");
                return;
            }
            Id_Ter = Convert.ToInt32(((RadComboBox)gi.FindControl("RadComboBox1")).SelectedValue);
            Ter_Nombre = ((RadComboBox)gi.FindControl("RadComboBox1")).Text;
            Id_Seg = Convert.ToInt32(((RadComboBox)gi.FindControl("RadComboBox2")).SelectedValue);
            Seg_Descripcion = ((RadComboBox)gi.FindControl("RadComboBox2")).Text;
            Cte_UnidadDim = ((Label)gi.FindControl("RadTextBox3")).Text;
            Cte_Dimension = ((RadNumericTextBox)gi.FindControl("RadTextBox4")).Text;
            Cte_Pesos = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox5")).Text);
            Cte_Potencial = !string.IsNullOrEmpty(((RadNumericTextBox)gi.FindControl("RadNumericTextBox6")).Text) ? Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox6")).Text) : 0;
            Cte_Activo = ((CheckBox)gi.FindControl("chk8")).Checked;
            Cte_CarMP = !string.IsNullOrEmpty(((RadNumericTextBox)gi.FindControl("RadNumericTextBox7")).Text) ? Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox7")).Text) : 0;
            Cte_GastoVarT = !string.IsNullOrEmpty(((RadNumericTextBox)gi.FindControl("RadNumericTextBox8")).Text) ? Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox8")).Text) : 0;
            Cte_FletePaga = !string.IsNullOrEmpty(((RadNumericTextBox)gi.FindControl("RadNumericTextBox9")).Text) ? Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox9")).Text) : 0;
            Cte_PorcComision = !string.IsNullOrEmpty(((RadNumericTextBox)gi.FindControl("txtComision")).Text) ? Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtComision")).Text) : 0;

            if (Cte_Potencial == 0)
            {
                AlertaFocus("El potencial no debe ser cero", ((RadNumericTextBox)gi.FindControl("RadNumericTextBox6")).ClientID);
                e.Canceled = true;
                return;
            }

            CN_CatTerritorios cn_catterritorios = new CN_CatTerritorios();
            Territorios catterr = new Territorios();
            catterr.Id_Emp = sesion.Id_Emp;
            catterr.Id_Cd = sesion.Id_Cd_Ver;
            catterr.Id_Ter = Id_Ter;
            cn_catterritorios.ConsultaTerritorios(ref catterr, sesion.Emp_Cnx);
            Rik = catterr.Rik_Nombre;
            Id_Rik = catterr.Id_Rik;
            Uen = catterr.Uen_Descripcion;
            id_uen = catterr.Id_Uen;
            DataRow[] Ar_dr;
            DataRow dr;
            Ar_dr = dt.Select();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    int UEN = !string.IsNullOrEmpty(dr["Id_Uen"].ToString()) ? Convert.ToInt32(dr["Id_Uen"].ToString()) : 0;
                    bool activo = Convert.ToBoolean(dr["Cte_Activo"]);
                    if (UEN == id_uen && activo == Cte_Activo)
                    {
                        Cte_Activo = false;
                    }
                }
            }
            Id_CteDet = dt.Rows.Count + 1;
            dt.Rows.Add(new object[] {  
                                Id_CteDet, 
                                Id_Ter, 
                                Ter_Nombre,
                                Id_Seg,
                                Seg_Descripcion,
                                Cte_UnidadDim, 
                                Convert.ToDouble(Cte_Dimension).ToString("#,##0.00"),
                                Cte_Pesos,
                                Cte_Potencial,
                                Cte_Activo,
                                Id_Rik,
                                Rik,
                                Uen,
                                Cte_CarMP,
                                Cte_GastoVarT,
                                Cte_FletePaga,
                                Cte_PorcComision,
                                id_uen,
                                1
            });
        }
        private void Edit(GridCommandEventArgs e)
        {
            //GridItem gi = e.Item;
            //(gi.FindControl("RadComboBox1") as RadComboBox).Enabled = Convert.ToBoolean((gi.FindControl("lblEditable") as Label).Text);
        }
        private void Delete(GridCommandEventArgs e)
        {
            int Id_CteDet = 0;
            GridItem gi = null;
            DataRow[] Ar_dr;
            DataRow dr;
            gi = e.Item;

            if (((Label)gi.FindControl("lbleditar")).Text == "False")
            {
                Alerta("Imposible eliminar, existen documentos que utilizan esta relacion");
                return;
            }

            Id_CteDet = Convert.ToInt32(((Label)gi.FindControl("Label0")).Text);
            Ar_dr = dt.Select("Id_CteDet='" + Id_CteDet + "'");
            if (Ar_dr.Length > 0)
            {
                Ar_dr[0].Delete();
                dt.AcceptChanges();
            }
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                dr = dt.Rows[x];
                dr.BeginEdit();
                dr["Id_CteDet"] = x + 1;
                dr.AcceptChanges();
            }
        }
        private bool Deshabilitar()
        {
            try
            {
                bool verificador = false;
                if (HF_ID.Value != "")
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    Catalogo ct = new Catalogo();
                    ct.Id_Emp = Sesion.Id_Emp;
                    ct.Id_Cd = Sesion.Id_Cd_Ver;
                    ct.Id = Convert.ToInt32(HF_ID.Value);
                    ct.Tabla = "CatCliente";
                    ct.Columna = "Id_Cte";
                    CN_Comun.Deshabilitar(ct, Sesion.Emp_Cnx, ref verificador);
                }
                return verificador;
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
            string cmd = e.Argument.ToString();
            Guardar(Convert.ToInt32(cmd.Split(new string[] { "@" }, StringSplitOptions.None)[0]), Convert.ToInt32(cmd.Split(new string[] { "@" }, StringSplitOptions.None)[1]));
            txtAutorizo.Text = cmd.Split(new string[] { "@" }, StringSplitOptions.None)[2];
        }
    }
}
