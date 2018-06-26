﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using System.Xml;
using System.Diagnostics;
using System.Configuration;

namespace SIANWEB
{
    public partial class CapFactura_Lista : System.Web.UI.Page
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
                        Session["PedidoFacturacion" + Session.SessionID] = null;
                    }
                }
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
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        Session["ListaRemisionesFactura"] = new List<Remision>();// null;
                        rgFactura.Rebind();

                        break;
                    case "FacturaDepuracion":
                        Session["ListaRemisionesFactura"] = new List<Remision>();// null;
                        rgFactura.Rebind();

                        break;
                    case "FacturacionVarialesSesionDestruir":
                        Session["ListaRemisionesFactura" + Session.SessionID] = null;
                        Session["ListaRemisionesFactura"] = new List<Remision>();// null;
                        break;

                    case "FacturaRemisiones":
                        //Abre la ventana de facturacion, cuando esta se carga debe detectar por medio de la variable de sesion
                        //si se esta facturando remisiones.
                        if (this.HD_GridRebind_FacturaRemisiones.Value == "1")
                        {
                            this.HD_GridRebind_FacturaRemisiones.Value = "0";
                            if (((List<Remision>)Session["ListaRemisionesFactura" + Session.SessionID]).Count > 0)
                                RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_Factura_Edicion('0','0','0','1')"));
                            else
                                Session["ListaRemisionesFactura" + Session.SessionID] = new List<Remision>();// null;
                        }
                        else
                            Session["ListaRemisionesFactura" + Session.SessionID] = new List<Remision>();// null;
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void rgFactura_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {  //Llenar Grid
                    rgFactura.DataSource = this.GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgFactura_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgFactura.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgFactura_ItemCommand(object source, GridCommandEventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            string mensajeError = string.Empty;
            try
            {
                ErrorManager();
                Int32 item = default(Int32);
                if (e.Item == null) return;
                item = e.Item.ItemIndex;
                if (item >= 0)
                {
                    int Id_Emp = Convert.ToInt32(rgFactura.Items[item]["Id_Emp"].Text);
                    int Id_Cd = Convert.ToInt32(rgFactura.Items[item]["Id_Cd"].Text);
                    int Id_Fac = Convert.ToInt32(rgFactura.Items[item]["Id_Fac"].Text);
                    string Id_FacSerie = rgFactura.Items[item]["Id_FacSerie"].Text.Trim();
                    bool tienePDF = Convert.ToBoolean(rgFactura.Items[item]["FPDF"].Text);
                    bool tieneXML = Convert.ToBoolean(rgFactura.Items[item]["FXML"].Text);
                    string estatus = rgFactura.Items[item]["Fac_EstatusStr"].Text;
                    string depuracion = rgFactura.Items[item]["Fac_DepuracionStr"].Text;
                    string[] datePart = rgFactura.Items[item]["Fac_Fecha"].Text.Split(new char[] { '/' });
                    DateTime fechaFactura = new DateTime(Convert.ToInt32(datePart[2]), Convert.ToInt32(datePart[1]), Convert.ToInt32(datePart[0]));
                    string facModificable;
                    switch (e.CommandName.ToString())
                    {
                        case "Eliminar":
                            mensajeError = "CapFactura_delete_error";
                            if (Convert.ToDouble(rgFactura.Items[item]["Fac_Saldo"].Text) < Convert.ToDouble(rgFactura.Items[item]["Fac_Importe"].Text))
                                this.DisplayMensajeAlerta("CapFactura_TienePagos");
                            else
                            {
                                if (_PermisoEliminar)
                                    this.CancelarFactura(Id_Emp, Id_Cd, Id_Fac, Id_FacSerie );
                                else
                                    this.DisplayMensajeAlerta("PermisoEliminarDenegado");
                            }
                            break;
                        case "Refacturar":
                            //Definir si la factura es modificable o no
                            facModificable = "1";
                            if (!estatus.ToUpper().Contains("BAJA") && !estatus.ToUpper().Contains("CAPTURADO") && !Convert.ToBoolean(rgFactura.Items[item]["TienePagos"].Text))
                                facModificable = "2";
                            else
                            {
                                Alerta("Imposible refacturar el documento");
                                return;
                            }
                            RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_Factura_EdicionRefacturar('", Id_Emp, "','", Id_Cd, "','", Id_Fac, "','",Id_FacSerie ,"','", facModificable, "')"));
                            break;
                        case "Modificar":
                            //Definir si la factura es modificable o no
                            facModificable = "1";
                            if (!estatus.ToUpper().Contains("CAPTURA") || fechaFactura < sesion.CalendarioIni || fechaFactura > sesion.CalendarioFin)
                                facModificable = "0";

                            if (_PermisoModificar)
                                if (facModificable == "0")
                                    RAM1.ResponseScripts.Add("OpenAlert('Imposible modificar el documento','" + Id_Emp + "','" + Id_Cd + "','" + Id_Fac + "','"+ Id_FacSerie+ "','" + facModificable + "')");                              
                                else
                                    RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_Factura_Edicion('", Id_Emp, "','", Id_Cd, "','", Id_Fac, "','",Id_FacSerie ,"','" ,facModificable, "')"));
                            else
                                this.DisplayMensajeAlerta("PermisoModificarDenegado");
                            break;
                        case "Imprimir":
                            mensajeError = "CapFactura_print_error";
                            if (estatus.Contains("Capturado") || estatus.Contains("Impreso") || estatus.Contains("Embarque") || estatus.Contains("Entregado"))
                            {
                                if (_PermisoImprimir)
                                    this.ImprimirFactura(Id_Emp, Id_Cd, Id_Fac,Id_FacSerie, "FACTURA", "");
                                else
                                    this.DisplayMensajeAlerta("PermisoImprimirDenegado");
                            }
                            else
                                this.DisplayMensajeAlerta("CapFactura_Imprimir_Denegado");
                            break;
                        case "PDF":
                            if (tienePDF)
                                descargarPDF(Id_Fac, Id_FacSerie );
                            else
                                Alerta("Esta factura aún no cuenta con un archivo PDF");
                            
                                break;
                        case "XML":
                            if (tieneXML)
                                descargarXML(Id_Fac, Id_FacSerie );
                            else
                                Alerta("Esta factura aún no cuenta con un archivo XML");
                            break;
                        case "depurar":
                            if (_PermisoGuardar )
                                RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_FacturaDepurar('", Id_Emp, "','", Id_Cd, "','", Id_Fac,"','",Id_FacSerie, "')"));
                            else
                            Alerta("No tiene permisos para guardar");
                         break;

                    }
                }
                if (e.CommandName.ToString().ToUpper().Contains("SORT"))
                    this.rgFactura.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbCentrosDist_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun comun = new CN__Comun();

                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                if (sesion.CalendarioIni >= txtFecha1.MinDate && sesion.CalendarioIni <= txtFecha1.MaxDate)
                {
                    txtFecha1.DbSelectedDate = sesion.CalendarioIni;
                }
                if (sesion.CalendarioFin >= txtFecha2.MinDate && sesion.CalendarioFin <= txtFecha2.MaxDate)
                {
                    txtFecha2.DbSelectedDate = sesion.CalendarioFin;
                }

                Session["Sesion" + Session.SessionID] = sesion;

                txtCliente1.Text = string.Empty;
                txtCliente2.Text = string.Empty;
                this.CargarUsuarios();
                rgFactura.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                rgFactura.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion
        #region Funciones
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

                    if (Permiso.PGrabar == false)
                        this.RadToolBar1.Items[6].Visible = false;
                    if (Permiso.PGrabar == false || Permiso.PModificar == false)
                        this.RadToolBar1.Items[5].Visible = false;
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
                    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Inicializar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                this.CargarCentros();
                this.CargarUsuarios();
                if (sesion.Propia)
                {
                    this.cmbUsuario.Enabled = false;
                    rowUsuario.Visible = false;
                }
                else
                {
                    this.cmbUsuario.Enabled = true;
                    rowUsuario.Visible = true;
                }

                this.cmbEstatus.Sort = RadComboBoxSort.Ascending;
                this.cmbEstatus.SortItems();

                //Cargar grid de ordenes de compra
                if (sesion.CalendarioIni >= txtFecha1.MinDate && sesion.CalendarioIni <= txtFecha1.MaxDate)
                {
                    txtFecha1.DbSelectedDate = sesion.CalendarioIni;
                }
                if (sesion.CalendarioFin >= txtFecha2.MinDate && sesion.CalendarioFin <= txtFecha2.MaxDate)
                {
                    txtFecha2.DbSelectedDate = sesion.CalendarioFin;
                }

                double ancho = 0;
                foreach (GridColumn gc in rgFactura.Columns)
                {
                    if (gc.Display)
                    {
                        ancho = ancho + gc.HeaderStyle.Width.Value;
                    }
                }
                rgFactura.Width = Unit.Pixel(Convert.ToInt32(ancho));
                rgFactura.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));

                rgFactura.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<Factura> GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<Factura> listFactura = new List<Factura>();
                Factura factura = new Factura();

                bool? acuse = null;
                switch (cmbAcuse.SelectedValue)
                {
                    case "-1":
                        acuse = null;
                        break;
                    case "0":
                        acuse = false;
                        break;
                    case "1":
                        acuse = true;
                        break;
                }

                bool? depuracion = null;
                switch (CmbDepuracion.SelectedValue )
                {
                    case "-1":
                        depuracion = null;
                        break;
                    case "0":
                        depuracion = false;
                        break;
                    case "1":
                        depuracion = true;
                        break;
                }

                new CN_CapFactura().ConsultaFactura_Buscar(factura, sesion.Emp_Cnx, ref listFactura
                    , sesion.Id_Emp
                    , sesion.Id_Cd_Ver
                    , this.txtNombre.Text
                    , this.txtCliente1.Text == string.Empty ? -1 : Convert.ToInt32(this.txtCliente1.Text)
                    , this.txtCliente2.Text == string.Empty ? -1 : Convert.ToInt32(this.txtCliente2.Text)
                    , cmbTipo.SelectedValue
                    , cmbEstatus.SelectedValue
                    , this.txtFecha1.SelectedDate == null ? DateTime.MinValue : Convert.ToDateTime(this.txtFecha1.SelectedDate)
                    , this.txtFecha2.SelectedDate == null ? DateTime.MinValue : Convert.ToDateTime(this.txtFecha2.SelectedDate)
                    , this.txtFactura1.Text == string.Empty ? -1 : Convert.ToInt32(this.txtFactura1.Text)
                    , this.txtFactura2.Text == string.Empty ? -1 : Convert.ToInt32(this.txtFactura2.Text)
                    , this.txtPedido1.Text == string.Empty ? -1 : Convert.ToInt32(this.txtPedido1.Text)
                    , this.txtPedido2.Text == string.Empty ? -1 : Convert.ToInt32(this.txtPedido2.Text)
                    , acuse
                    , depuracion
                    , sesion.Propia ? sesion.Id_U : Convert.ToInt32(cmbUsuario.SelectedValue));
                    
                return listFactura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void descargarXML(int Id_Fac, string Id_FacSerie)
        {
            Factura fac = new Factura();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            fac.Id_Emp = Sesion.Id_Emp;
            fac.Id_Cd = Sesion.Id_Cd_Ver;
            fac.Id_Fac = Id_Fac;
            fac.Id_FacSerie = Id_FacSerie;
            CN_CapFactura factura = new CN_CapFactura();
            factura.ArchivoPdf_Xml(ref fac, Sesion.Emp_Cnx);
            string ruta = null;
            System.IO.StreamWriter sw = null;
            ruta = Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "Fac" + Id_Fac.ToString() + ".txt";

            if (File.Exists(ruta))
                File.Delete(ruta);
            if (File.Exists(Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "Fac" + Id_Fac.ToString() + ".xml"))
                File.Delete(Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "Fac" + Id_Fac.ToString() + ".xml");
            sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
            sw.WriteLine(fac.Fac_Xml.ToString());
            sw.Close();
            File.Move(ruta, Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "Fac" + Id_Fac.ToString() + ".xml");
            RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('Reportes\\archivoXml" + Sesion.Id_U.ToString() + "Fac", Id_Fac.ToString(), ".xml')"));
        }
        private void descargarPDF(int Id_Fac, string Id_FacSerie)
        {
            object resultado = null;
            Factura fac = new Factura();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            fac.Id_Emp = Sesion.Id_Emp;
            fac.Id_Cd = Sesion.Id_Cd_Ver;
            fac.Id_Fac = Id_Fac;
            fac.Id_FacSerie = Id_FacSerie;
            CN_CapFactura factura = new CN_CapFactura();
            factura.ConsultarFacturaSAT(ref fac, Sesion.Emp_Cnx, ref resultado);
            byte[] archivoPdf = (byte[])resultado;
            if (archivoPdf.Length > 0)
            {
                string tempPDFname = string.Concat("FACTURA_"
                         , Sesion.Id_Emp.ToString()
                         , "_", Sesion.Id_Cd.ToString()
                         , "_", Id_Fac.ToString()
                         , ".pdf");
                string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);
                this.ByteToTempPDF(URLtempPDF, archivoPdf);
                // ------------------------------------------------------------------------------------------------
                // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                // ------------------------------------------------------------------------------------------------
                RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", WebURLtempPDF, "')"));
            }
        }
        private void ImprimirFactura(int Id_Emp, int Id_Cd, int Id_Fac, string Id_FacSerie, string movimiento,string agregado_nota_cancelacion)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            try
            {
                int verificador = 0;

                List<FacturaDet> listaFacturaDet = new List<FacturaDet>();
                CN_CapFactura cn_factura = new CN_CapFactura();
                Factura factura = new Factura();
                factura.Id_Emp = sesion.Id_Emp;
                factura.Id_Cd = sesion.Id_Cd_Ver;
                factura.Id_Fac = Id_Fac;
                factura.Id_FacSerie = Id_FacSerie;
                cn_factura.ConsultaFactura(ref factura, ref listaFacturaDet, sesion.Emp_Cnx);

                List<AdendaDet> listCabT = new List<AdendaDet>();
                List<AdendaDet> listDetT = new List<AdendaDet>();
                List<AdendaDet> listCabR = new List<AdendaDet>();
                List<AdendaDet> listDetR = new List<AdendaDet>();
                new CN_CapFactura().ConsultarAdenda(Id_Emp, Id_Cd, Id_Fac,Id_FacSerie , "1", "2", ref listCabT, ref listDetT, sesion.Emp_Cnx);
                new CN_CapFactura().ConsultarAdenda(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Fac,Id_FacSerie, "7", "8", ref listCabR, ref listDetR, sesion.Emp_Cnx);

                // -------------------------------------------------------------------------------------------
                // Consulta productos de factura especial de la tabla 'CapFacturaEspecialDet' si esque la factura especial existe
                // esto es si es una actualización de factura --> si el parametro Folio trae un Id de factura
                // -------------------------------------------------------------------------------------------
                List<FacturaDet> listaProdFacturaEspecialFinal = new List<FacturaDet>();
                new CN_CapFactura().ConsultaFacturaEspecialDetalle(ref listaProdFacturaEspecialFinal
                    , sesion.Emp_Cnx
                    , Id_Emp
                    , Id_Cd
                    , Id_Fac
                    , Id_FacSerie
                    , factura.Id_Cte);
                // -------------------------------------------------------------------------------------------

                #region variable XML a enviar
                StringBuilder XML_Enviar = new StringBuilder();
                XML_Enviar.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                XML_Enviar.Append("<Comprobante");
                XML_Enviar.Append(" serie=\"\"");
                XML_Enviar.Append(" folio=\"\"");
                XML_Enviar.Append(" fecha=\"\"");
                XML_Enviar.Append(" formaDePago=\"\"");
                XML_Enviar.Append(" subTotal=\"\"");
                XML_Enviar.Append(" total=\"\"");

                XML_Enviar.Append(" tipoDeComprobante=\"\"");
                XML_Enviar.Append(" Sustituye=\"\"");
                XML_Enviar.Append(" tipoMovimiento=\"\""); //FACTURA,NOTA DE CARGO, NOTA DE CEDITO ,CANCELACION FACTURA,CANCELACION NOTA DE CARGO
                XML_Enviar.Append(" tipoMoneda=\"\""); //MN= MONEDA NACIONAL, MA = MONEDA AMERICANA depende del catalogo del SIAN
                XML_Enviar.Append(" tipoCambio=\"\""); //IMPORTE VIGENTE DEL CAMBIO DEPENDIENDO DEL TIPO DE MONEDA
                XML_Enviar.Append(" leyendaFacturaEspecial=\"\""); //LEYENDA DE FACTURA ESPECIAL: LOS DATOS DEL DETALLE REAL DE LA FACTURA PERO DELIMITADOS POR /
                XML_Enviar.Append(" movimientoacancelar=\"\""); //SI ES CANCELACION FACTURA HAY QUE INDICAR QUE FACTURA ESTA CANCELANDO APLICA LO MISMO PARA LA NOTA DE CARGO
                XML_Enviar.Append(" ConceptoDescuento1=\"\"");
                XML_Enviar.Append(" TasaDescuento1=\"\"");
                XML_Enviar.Append(" ConceptoDescuento2=\"\"");
                XML_Enviar.Append(" TasaDescuento2=\"\"");
                XML_Enviar.Append(" Notas=\"\"");
                XML_Enviar.Append(" Correo=\"\"");
                XML_Enviar.Append(" CliNum=\"\"");

                XML_Enviar.Append(" MetodoPago=\"\"");
                XML_Enviar.Append(" CuentaBancaria=\"\"");
                XML_Enviar.Append(" Referencia=\"\"");
                XML_Enviar.Append(">");
                XML_Enviar.Append(" <Emisor");
                XML_Enviar.Append(" rfc=\"\"");
                XML_Enviar.Append(" numero=\"\" />");
                XML_Enviar.Append(" <Receptor");
                XML_Enviar.Append(" rfc=\"\"");
                XML_Enviar.Append(" nombre=\"\">");
                XML_Enviar.Append(" <Domicilio");
                XML_Enviar.Append(" calle=\"\"");
                XML_Enviar.Append(" noExterior=\"\"");
                XML_Enviar.Append(" colonia=\"\"");
                XML_Enviar.Append(" municipio=\"\"");
                XML_Enviar.Append(" estado=\"\"");
                XML_Enviar.Append(" pais=\"\"");
                XML_Enviar.Append(" codigoPostal=\"\" />");
                XML_Enviar.Append(" </Receptor>");
                XML_Enviar.Append(" <Conceptos>");
                XML_Enviar.Append(" <Concepto");
                XML_Enviar.Append(" cantidad=\"\"");
                XML_Enviar.Append(" noIdentificacion=\"\"");
                XML_Enviar.Append(" descripcion=\"\"");
                XML_Enviar.Append(" valorUnitario=\"\"");
                XML_Enviar.Append(" importe=\"\" />");
                XML_Enviar.Append(" </Conceptos>");
                XML_Enviar.Append(" <Impuestos");
                XML_Enviar.Append(" totalImpuestosTrasladados=\"\">");
                XML_Enviar.Append(" <Traslados>");
                XML_Enviar.Append(" <Traslado");
                XML_Enviar.Append(" impuesto=\"\"");
                XML_Enviar.Append(" tasa=\"\"");
                XML_Enviar.Append(" importe=\"\" />");
                XML_Enviar.Append(" </Traslados>");

                if ((factura.Fac_RetIva == true) && (factura.Fac_ImporteRetencion > 0))
                {
                    XML_Enviar.Append(" <Retenidos>");
                    XML_Enviar.Append(" <Retenido");
                    XML_Enviar.Append(" importe=\"\"");
                    XML_Enviar.Append(" impuesto=\"\" />");
                    XML_Enviar.Append(" </Retenidos>");
                }
                XML_Enviar.Append(" </Impuestos>");                

                XML_Enviar.Append(" <Addenda>");

                //ADENDA CABECERA
                XML_Enviar.Append(" <cabecera");
                XML_Enviar.Append(" Pedido=\"\"");
                XML_Enviar.Append(" Requisicion=\"\"");
                XML_Enviar.Append(" consignarRenglon1=\"\"");
                XML_Enviar.Append(" consignarRenglon2=\"\"");
                XML_Enviar.Append(" consignarRenglon3=\"\"");
                XML_Enviar.Append(" consignarRenglon4=\"\"");
                XML_Enviar.Append(" consignarRenglon5=\"\"");
                XML_Enviar.Append(" Conducto=\"\"");
                XML_Enviar.Append(" CondicionesPago=\"\"");
                XML_Enviar.Append(" NumeroGuia=\"\"");
                XML_Enviar.Append(" ControlPedido=\"\"");
                XML_Enviar.Append(" OrdenEmbarque=\"\"");
                XML_Enviar.Append(" Zona=\"\"");
                XML_Enviar.Append(" Territorio=\"\"");
                XML_Enviar.Append(" Agente=\"\"");
                XML_Enviar.Append(" NumeroDocumentoAduanero=\"\"");
                XML_Enviar.Append(" Formulo=\"\"");
                XML_Enviar.Append(" Autorizo=\"\"");

                XML_Enviar.Append(" NombreAddenda=\"\"");
                foreach (AdendaDet det in listCabT)
                {
                    XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                }
                foreach (AdendaDet det in listCabR)
                {
                    XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                }
                XML_Enviar.Append("/>");

              
                //ADENDA DETALLE
                if (listaProdFacturaEspecialFinal.Count > 0)
                {
                    foreach (FacturaDet fd in listaProdFacturaEspecialFinal)
                    {
                        XML_Enviar.Append(" <Detalle");
                        XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                        XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                        XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");

                        string primerNodo = "";
                        int primerfila = 0;
                        foreach (AdendaDet det in listDetT)
                        {

                            if (fd.Id_Prd == det.Id_Prd)
                            {
                                if (primerfila == 0)
                                { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                    primerNodo = det.Nodo;
                                }
                                if (primerfila > 0 && det.Nodo == primerNodo)
                                {
                                    XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                    // ABRIMOS UNA NUEVA ADENDA
                                    XML_Enviar.Append(" <Detalle");
                                    XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                    XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                    XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                }

                                XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                primerfila++;
                            }
                        }

                        primerNodo = "";
                        primerfila = 0;
                        foreach (AdendaDet det in listDetR)
                        {

                            if (fd.Id_Prd == det.Id_Prd)
                            {
                                if (primerfila == 0)
                                { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                    primerNodo = det.Nodo;
                                }
                                if (primerfila > 0 && det.Nodo == primerNodo)
                                {
                                    XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                    // ABRIMOS UNA NUEVA ADENDA
                                    XML_Enviar.Append(" <Detalle");
                                    XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                    XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                    XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                }

                                XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                primerfila++;
                            }
                        }

                        XML_Enviar.Append("/>");
                    }
                }
                else
                {
                    //NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                    //NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                    foreach (FacturaDet fd in listaFacturaDet)
                    {
                        XML_Enviar.Append(" <Detalle");
                        XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                        XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                        XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");

                        string primerNodo = "";
                        int primerfila = 0;
                        foreach (AdendaDet det in listDetT)
                        {

                            if (fd.Id_Prd == det.Id_Prd)
                            {
                                if (primerfila == 0)
                                { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                    primerNodo = det.Nodo;
                                }
                                if (primerfila > 0 && det.Nodo == primerNodo)
                                {
                                    XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                    // ABRIMOS UNA NUEVA ADENDA
                                    XML_Enviar.Append(" <Detalle");
                                    XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                    XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                    XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                }

                                XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                primerfila++;
                            }
                        }

                        primerNodo = "";
                        primerfila = 0;
                        foreach (AdendaDet det in listDetR)
                        {

                            if (fd.Id_Prd == det.Id_Prd)
                            {
                                if (primerfila == 0)
                                { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                    primerNodo = det.Nodo;
                                }
                                if (primerfila > 0 && det.Nodo == primerNodo)
                                {
                                    XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                    // ABRIMOS UNA NUEVA ADENDA
                                    XML_Enviar.Append(" <Detalle");
                                    XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                    XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                    XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                }

                                XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                primerfila++;
                            }
                        }

                        XML_Enviar.Append("/>");
                    }

                }
                XML_Enviar.Append(" </Addenda>");
                XML_Enviar.Append(" </Comprobante>");

                    //TERMINA NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                    //TERMINA NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA

                    //foreach (FacturaDet fd in listaFacturaDet)
                    //{
                    //    XML_Enviar.Append(" <Detalle");
                    //    XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                    //    XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                    //    XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\""); 
                    //    foreach (AdendaDet det in listDetT)
                    //    {
                    //        if (fd.Id_Prd == det.Id_Prd)
                    //        {
                    //            XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                    //        }
                    //    }
                    //    foreach (AdendaDet det in listDetR)
                    //    {
                    //        if (fd.Id_Prd == det.Id_Prd)
                    //        {
                    //            XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                    //        }
                    //    }
                    //    XML_Enviar.Append("/>");
                    //}


                    

                

                #endregion

                #region Codigo pruebas

                //PruebaServicio.Service1 servicio = new PruebaServicio.Service1();
                //float suma = servicio.Suma(Convert.ToSingle(txtNumero1.Text), Convert.ToSingle(txtNumero2.Text));
                //this.Alerta(suma.ToString());

                //Uri objURI = new Uri("");
                //WebRequest objWebRequest = WebRequest.Create(objURI);
                //WebResponse objWebResponse = objWebRequest.GetResponse();
                //Stream objStream = objWebResponse.GetResponseStream();
                //StreamReader objStreamReader = new StreamReader(objStream);
                //string responseText = objStreamReader.ReadToEnd();

                #endregion

                // --------------------------------------
                // Consulta centro de distribución
                // --------------------------------------
                CentroDistribucion Cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                // --------------------------------------------------------------------
                // Consulta detalle de factura para generar lista de productos
                // --------------------------------------------------------------------
                //if (factura.Fac_Sello != "" && factura.Fac_Sello != null && movimiento == "FACTURA")
                //{
                //    //Abre el XML y carga el PDF de la factura
                //    object resultado = null;
                //    cn_factura.ConsultarFacturaSAT(ref factura, sesion.Emp_Cnx, ref resultado);
                //    byte[] archivoPdf = (byte[])resultado;
                //    if (archivoPdf.Length > 0)
                //    {
                //        string tempPDFname = string.Concat("FACTURA_"
                //                 , factura.Id_Emp.ToString()
                //                 , "_", factura.Id_Cd.ToString()
                //                 , "_", factura.Id_U.ToString()
                //                 , ".pdf");
                //        string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                //        string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);

                //        this.ByteToTempPDF(URLtempPDF, archivoPdf);
                //        // ------------------------------------------------------------------------------------------------
                //        // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                //        // ------------------------------------------------------------------------------------------------

                //        RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", WebURLtempPDF, "')"));
                //    }
                //    else
                //        this.DisplayMensajeAlerta("TempPDFNoData");
                //}
                //else
                //{
                // --------------------------------------
                // cargar xml de factura que se envia a SAT
                // --------------------------------------
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(XML_Enviar.ToString());

                // --------------------------------------//
                // --------------------------------------//
                //         LLENAR DATOS DEL XML          //
                // --------------------------------------//
                // --------------------------------------//
                #region Llenar datos factura a Enviar
                //encabezado
                XmlNode Comprobante = xml.SelectSingleNode("Comprobante");
                Clientes cliente = new Clientes();
                cliente.Id_Emp = factura.Id_Emp;
                cliente.Id_Cd = factura.Id_Cd;
                cliente.Id_Cte = factura.Id_Cte;
                new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);

                Comprobante.Attributes["serie"].Value = factura.Serie;
                Comprobante.Attributes["folio"].Value = factura.Folio_cancelacion > 0 ? factura.Folio_cancelacion.ToString() : factura.Id_Fac.ToString();
                Comprobante.Attributes["fecha"].Value = string.Format("{0:s}", factura.Fac_Fecha);
                Comprobante.Attributes["formaDePago"].Value = "PAGO EN UNA SOLA EXHIBICION";
                Comprobante.Attributes["subTotal"].Value = factura.Fac_SubTotal == null ? "0" : Math.Round((double)factura.Fac_SubTotal, 2).ToString();
                Comprobante.Attributes["total"].Value = (Math.Round((double)factura.Fac_SubTotal, 2) + Math.Round((double)factura.Fac_ImporteIva, 2)).ToString();
                Comprobante.Attributes["tipoDeComprobante"].Value = "ingreso";
                Comprobante.Attributes["Sustituye"].Value = factura.Fac_Refactura;
                Comprobante.Attributes["tipoMovimiento"].Value = movimiento;
                Comprobante.Attributes["tipoMoneda"].Value = factura.Mon_Unidad;
                Comprobante.Attributes["tipoCambio"].Value = factura.Mon_TipCambio.ToString();
                Comprobante.Attributes["leyendaFacturaEspecial"].Value = ""; //
                Comprobante.Attributes["movimientoacancelar"].Value = ""; //

                Comprobante.Attributes["ConceptoDescuento1"].Value = factura.Fac_Desc1;
                Comprobante.Attributes["TasaDescuento1"].Value = factura.Fac_DescPorcen1 == null ? string.Empty : factura.Fac_DescPorcen1.ToString();
                Comprobante.Attributes["ConceptoDescuento2"].Value = factura.Fac_Desc2;
                Comprobante.Attributes["TasaDescuento2"].Value = factura.Fac_DescPorcen2 == null ? string.Empty : factura.Fac_DescPorcen2.ToString();
                Comprobante.Attributes["Correo"].Value = factura.Cte_Email;
                Comprobante.Attributes["CliNum"].Value = factura.Id_Cte.ToString();

                Comprobante.Attributes["MetodoPago"].Value = FormaPagoNombre(factura.Fac_FPago);
                Comprobante.Attributes["CuentaBancaria"].Value = factura.Fac_UDigitos.ToString();
                Comprobante.Attributes["Referencia"].Value = cliente.Cte_Referencia;

                XmlNode Emisor = Comprobante.SelectSingleNode("Emisor");
                Emisor.Attributes["rfc"].Value = Cd.Cd_Rfc;
                Emisor.Attributes["numero"].Value = Cd.Cd_Numero ;

                //receptor
                XmlNode Receptor = Comprobante.SelectSingleNode("Receptor");
                Receptor.Attributes["rfc"].Value = factura.Fac_CteRfc ;
                Receptor.Attributes["nombre"].Value =  factura.Cte_NomComercial;

                //Domicilio
                XmlNode Domicilio = Receptor.SelectSingleNode("Domicilio");
                Domicilio.Attributes["calle"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacCalle); // factura.Fac_CteCalle.Replace("\"", "");
                Domicilio.Attributes["noExterior"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacNumero);// factura.Fac_CteNumero;
                Domicilio.Attributes["colonia"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacColonia);// factura.Fac_CteColonia;
                Domicilio.Attributes["municipio"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacMunicipio);// factura.Fac_CteMunicipio;
                Domicilio.Attributes["estado"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacEstado);// factura.Fac_CteEstado;
                Domicilio.Attributes["pais"].Value = "México";
                Domicilio.Attributes["codigoPostal"].Value = cliente.Cte_FacCp;// factura.Fac_CteCp;
                // ---------------------
                // Conceptos --> partidas = producto
                // Detalle --> productoDetalle
                // ---------------------         
                XmlNode Conceptos = Comprobante.SelectSingleNode("Conceptos");
                XmlNode producto = Conceptos.SelectSingleNode("Concepto");
                XmlNode Addenda = Comprobante.SelectSingleNode("Addenda");

                //Si existe una factura especial, en los nodos de conceptos del producto se pone
                //los productos de la factura especial
                //si no, se pone los datos de productos de la factura original
                StringBuilder NotaProductosOriginales = new StringBuilder();
                if (listaProdFacturaEspecialFinal.Count > 0)
                {
                    foreach (FacturaDet facturaDet in listaProdFacturaEspecialFinal)
                    {
                        XmlNode prd = producto.Clone();
                        prd.Attributes["noIdentificacion"].Value = facturaDet.Producto.Id_PrdEsp;
                        prd.Attributes["descripcion"].Value = facturaDet.Producto.Prd_DescripcionEspecial.Replace("\"", "");
                        prd.Attributes["cantidad"].Value = facturaDet.Fac_Cant.ToString();
                        prd.Attributes["valorUnitario"].Value = Math.Round(facturaDet.Fac_Precio, 2).ToString();
                        prd.Attributes["importe"].Value = Math.Round(facturaDet.Fac_Importe, 2).ToString();
                        producto.ParentNode.AppendChild(prd);
                    }

                    foreach (FacturaDet facturaDet in listaFacturaDet)
                    {
                        NotaProductosOriginales.Append("/");
                        NotaProductosOriginales.Append(facturaDet.Id_Prd.ToString());
                        NotaProductosOriginales.Append("/");
                        NotaProductosOriginales.Append(Math.Round(facturaDet.Fac_Precio, 2).ToString());
                        NotaProductosOriginales.Append("/");
                        NotaProductosOriginales.Append(facturaDet.Fac_Cant.ToString());
                    }
                }
                else
                {
                    foreach (FacturaDet facturaDet in listaFacturaDet)
                    {
                        XmlNode prd = producto.Clone();
                        prd.Attributes["noIdentificacion"].Value = facturaDet.Id_Prd.ToString();
                        prd.Attributes["descripcion"].Value = facturaDet.Producto.Prd_Descripcion.Replace("\"", "");
                        prd.Attributes["cantidad"].Value = facturaDet.Fac_Cant.ToString();
                        prd.Attributes["valorUnitario"].Value = Math.Round(facturaDet.Fac_Precio, 2).ToString();
                        prd.Attributes["importe"].Value = Math.Round(facturaDet.Fac_Importe, 2).ToString();
                        producto.ParentNode.AppendChild(prd);
                    }
                }
                producto.ParentNode.RemoveChild(xml.SelectNodes("//Concepto").Item(0));

                //Impuestos
                XmlNode Impuestos = Comprobante.SelectSingleNode("Impuestos");
                Impuestos.Attributes["totalImpuestosTrasladados"].Value = factura.Fac_ImporteIva == null ? "0" : factura.Fac_ImporteIva.ToString();

                //Traslado (impuestos desgloce)
                XmlNode Traslados = Impuestos.SelectSingleNode("Traslados");
                XmlNode Traslado = Traslados.SelectSingleNode("Traslado");
                Traslado.Attributes["impuesto"].Value = "IVA";
                if (cliente.BPorcientoIVA == true)
                    Traslado.Attributes["tasa"].Value = cliente.PorcientoIVA.ToString();
                else
                    Traslado.Attributes["tasa"].Value = Cd.Cd_IvaPedidosFacturacion.ToString();                
                Traslado.Attributes["importe"].Value = factura.Fac_ImporteIva == null ? "0" : Math.Round((double)factura.Fac_ImporteIva, 2).ToString();

                if ((factura.Fac_RetIva == true) && (factura.Fac_ImporteRetencion > 0))
                {
                    XmlNode Retenidos = Impuestos.SelectSingleNode("Retenidos");
                    XmlNode Retenido = Retenidos.SelectSingleNode("Retenido");
                    Retenido.Attributes["importe"].Value = factura.Fac_ImporteRetencion == null ? "0" : Math.Round((double)factura.Fac_ImporteRetencion, 2).ToString();
                    Retenido.Attributes["impuesto"].Value = "IVA";
                }

                //Addenda
                XmlNode cabecera = Addenda.SelectSingleNode("cabecera");
                cabecera.Attributes["Pedido"].Value = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                cabecera.Attributes["Requisicion"].Value = factura.Fac_Req == null ? string.Empty : factura.Fac_Req.ToString();
                //consulta datos cliente                 
                cabecera.Attributes["consignarRenglon1"].Value = factura.Fac_Contacto;
                cabecera.Attributes["consignarRenglon2"].Value = string.Concat(factura.Fac_CteCalle.Replace("\"", ""), " ", factura.Fac_CteNumero);
                cabecera.Attributes["consignarRenglon3"].Value = factura.Fac_CteColonia;
                cabecera.Attributes["consignarRenglon4"].Value = string.Concat(factura.Fac_CteMunicipio, " ", factura.Fac_CteEstado, " ", factura.Fac_CteCp);
                cabecera.Attributes["consignarRenglon5"].Value = "México";
                cabecera.Attributes["Conducto"].Value = factura.Fac_Conducto;
                cabecera.Attributes["CondicionesPago"].Value = factura.Fac_CondEntrega;
                cabecera.Attributes["NumeroGuia"].Value = factura.Fac_NumeroGuia;
                cabecera.Attributes["ControlPedido"].Value = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                cabecera.Attributes["OrdenEmbarque"].Value = factura.Id_Emb == null ? string.Empty : factura.Id_Emb.ToString();
                cabecera.Attributes["Zona"].Value = factura.Id_Cd.ToString(); //Cd.Cd_Descripcion;
                cabecera.Attributes["Territorio"].Value = factura.Id_Ter.ToString(); //factura.Ter_Nombre == null ? string.Empty : factura.Ter_Nombre;
                cabecera.Attributes["Agente"].Value = factura.Id_Rik == null ? string.Empty : factura.Id_Rik.ToString();
                cabecera.Attributes["NumeroDocumentoAduanero"].Value = factura.Fac_Req == null ? string.Empty : factura.Fac_Req.ToString();
                cabecera.Attributes["Formulo"].Value = Cd.Cd_CobranzaPersonaFormula;
                cabecera.Attributes["Autorizo"].Value = Cd.Cd_CobranzaPersonaAutoriza;
                cabecera.Attributes["NombreAddenda"].Value = cliente.Ade_Nombre;

                Factura factura_remision = new Factura();
                factura_remision.Id_Emp = factura.Id_Emp;
                factura_remision.Id_Cd = factura.Id_Cd;
                factura_remision.Id_Fac = factura.Id_Fac;
                factura_remision.Id_FacSerie = factura.Id_FacSerie;
                string agregado_nota = "";
                cn_factura.FacturaRemision_Nota(factura_remision, sesion.Emp_Cnx, ref agregado_nota);
                StringBuilder NotaCompleta = new StringBuilder();

                NotaCompleta.Append(agregado_nota + "//");
                NotaCompleta.Append(NotaProductosOriginales.ToString() + "//");
                NotaCompleta.Append(factura.Fac_Notas + "//");
                NotaCompleta.Append(agregado_nota_cancelacion);
                Comprobante.Attributes["Notas"].Value = NotaCompleta.ToString();

                /*
                if (!ValidaImpresionFactura(xml)) 
                {
                    Alerta("No se puede Imprimir Documento: Detalle de factura no coincide con total, Revise factura");
                    return;
                    
                }*/
                
                #endregion
                // --------------------------------------
                // convertir XML a string
                // --------------------------------------
                StringWriter sw = new StringWriter();
                XmlTextWriter tx = new XmlTextWriter(sw);
                xml.WriteTo(tx);
                string xmlString = sw.ToString();
                // ------------------------------------------------------   
                // ENVIAR XML al servicio de la aplicacion de KEY
                // -------- ----------------------------------------------
                XmlDocument xmlSAT = new XmlDocument();
                //sian_cfd.Service1 sianFacturacionElectronica = new sian_cfd.Service1();

                WebReference.Service1 sianFacturacionElectronica = new WebReference.Service1();


                //Alerta("Conectandose a:" + sianFacturacionElectronica.Url);
                //Alerta("Conectandose a:" + sianFacturacionElectronica.Url);
                
                object sianFacturacionElectronicaResult = sianFacturacionElectronica.ObtieneCFD(xmlString);

                xmlSAT.LoadXml(sianFacturacionElectronicaResult.ToString());



         


               
               

                //*********************************************//
                //* Procesar XML recibido de servicio de SAT  *//
                //*********************************************//
                string stringPDF = string.Empty;
                string selloSAT = string.Empty;
                string errorNum = string.Empty;
                string errorText = string.Empty;

                foreach (XmlNode nodo in xmlSAT.ChildNodes)
                {
                    if (nodo.Name == "Comprobante")
                    {
                        selloSAT = nodo.Attributes["sello"].Value;

                        foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                        {
                            if (Nodo_nivel2.Name == "AddendaKey")
                            {
                                foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                {
                                    if (Nodo_nivel3.Name == "PDF")
                                        stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                    if (Nodo_nivel3.Name == "ERROR")
                                    {
                                        errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                        errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                    }
                                }

                                nodo.RemoveChild(Nodo_nivel2);
                            }

                           
                        }
                    }
                }


                
                if (errorNum != "0")
                {
                    this.Alerta(string.Concat(errorText.Replace("'", "\"")));

                   /* factura.Fac_Sello = selloSAT;
                    System.Data.SqlTypes.SqlXml sqlXml
                        = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.OuterXml, XmlNodeType.Document, null));
                    factura.Fac_Xml = sqlXml;
                    factura.Fac_Pdf = this.Base64ToByte(stringPDF);

                    verificador = 0;

                    new CN_CapFactura().ModificarFacturaSAT(factura, sesion.Emp_Cnx, ref verificador);*/
                }
                else
                {
                    //ComprobanteSAT.RemoveChild(AddendaSAT);
                    factura.Fac_Sello = selloSAT;
                    System.Data.SqlTypes.SqlXml sqlXml
                        = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.OuterXml, XmlNodeType.Document, null));


                    factura.Fac_Xml = sqlXml;
                    factura.Fac_Pdf = this.Base64ToByte(stringPDF);


                    #region reporte factura

                    //// --------------------------------------
                    //// Agregar parámetros del reporte
                    //// --------------------------------------
                    //ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                    //ALValorParametrosInternos.Add(sesion.Id_Emp);
                    //ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                    //ALValorParametrosInternos.Add(Id_Fac);
                    //ALValorParametrosInternos.Add(xmlString);
                    //ALValorParametrosInternos.Add(arrayProductoUnidades);
                    //ALValorParametrosInternos.Add(Cd.Cd_Calle);
                    //ALValorParametrosInternos.Add(Cd.Cd_CalleNo);
                    //ALValorParametrosInternos.Add(Cd.Cd_CP);
                    //ALValorParametrosInternos.Add(Cd.Cd_Ciudad);
                    //ALValorParametrosInternos.Add(Cd.Cd_Estado);

                    //Type instance = null;
                    //instance = typeof(LibreriaReportes.FacturaImpresion);
                    //Session["InternParameter_Values" + Session.SessionID] = ALValorParametrosInternos;
                    //Session["assembly" + Session.SessionID] = instance.AssemblyQualifiedName;

                    ////NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                    //RAM1.ResponseScripts.Add("AbrirReporte()");

                    #endregion

                    // ---------------------------------------------------------------------------------------------
                    // Se actualiza el estatus de la factura a Impreso (I)
                    // ---------------------------------------------------------------------------------------------
                    if (movimiento != "CANCELACION")
                    {
                        factura.Fac_Estatus = "I";
                    }
                    else
                    {
                        factura.Fac_Estatus = "B";
                    }
                    verificador = 0;

                    new CN_CapFactura().ModificarFacturaSAT(factura, sesion.Emp_Cnx, ref verificador);

                    // -----------------------
                    // Abrir PDF de factura
                    // -----------------------
                    string tempPDFname = string.Concat("FACTURA_", factura.Id_Emp.ToString(), "_", factura.Id_Cd.ToString(), "_", factura.Id_Fac.ToString(), ".pdf");
                    string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                    string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);

                    this.ByteToTempPDF(URLtempPDF, this.Base64ToByte(stringPDF));
                    // ------------------------------------------------------------------------------------------------
                    // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                    // ------------------------------------------------------------------------------------------------
                    RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", WebURLtempPDF, "')"));
                }
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private bool ValidaImpresionFactura(XmlDocument xml)
        {
           
               
            XmlNodeList nodeList = xml.SelectNodes("//Concepto");

            XmlNode Comprobante = xml.SelectSingleNode("Comprobante");

            decimal subtotal =  decimal.Parse(Comprobante.Attributes["subTotal"].Value); 

            decimal totalDetalle = 0;

            decimal descuento1 = decimal.Parse(Comprobante.Attributes["TasaDescuento1"].Value);
            decimal descuento2 = decimal.Parse(Comprobante.Attributes["TasaDescuento2"].Value);

          
            foreach (XmlNode concepto in nodeList) {

                totalDetalle +=  decimal.Parse(concepto.Attributes["importe"].Value);  
            }


            if (descuento1 > 0)
            {
                totalDetalle = totalDetalle - (totalDetalle * (descuento1/100));
            }

            if (descuento2 > 0)
            {
                totalDetalle = totalDetalle - (totalDetalle * (descuento2/100));
            }


            if (Math.Round(subtotal, 2) != Math.Round(totalDetalle, 2, MidpointRounding.AwayFromZero))
            {
                return false;
            }          



            return true;
        }
        private string FormaPagoNombre(string Id_Fpa)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatFormaPago cncatformapago = new CN_CatFormaPago();
                FormaPago fpago = new FormaPago();
                fpago.Id_Emp = sesion.Id_Emp;
                if (Id_Fpa != "")
                {
                    fpago.Id_Fpa = Convert.ToInt32(Id_Fpa == "" ? "1" : Id_Fpa);
                    cncatformapago.ConsultaFormaPago(ref fpago, sesion.Emp_Cnx);
                }
                else
                {
                    fpago.Descripcion = "";
                }
                return fpago.Descripcion;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void CancelarFactura(int Id_Emp, int Id_Cd, int Id_Fac, string Id_FacSerie)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Factura factura = new Factura();
                factura.Id_Emp = Id_Emp;
                factura.Id_Cd = Id_Cd;
                factura.Id_Fac = Id_Fac;
                factura.Id_FacSerie = Id_FacSerie;
                factura.Id_U = sesion.Id_U;
                List<FacturaDet> listaFacturaDet = new List<FacturaDet>();
                EntradaSalida entSal = new EntradaSalida();
                List<EntradaSalidaDetalle> listaEntSal = new List<EntradaSalidaDetalle>();
                double importeTotalFactura = 0;
                double importeTotalFacturaIVA = 0;
                double importeTotalFactura_ProdNoDevolucion = 0;
                double importeTotalFacturaIVA_ProdNoDevolucion = 0;

                //Consultar factura
                new CN_CapFactura().ConsultaFactura(ref factura, ref listaFacturaDet, sesion.Emp_Cnx);

                if (factura.Fac_Estatus != "B")
                {
                    //crear objeto entrada-salida y su detalle
                    foreach (FacturaDet facturaDet in listaFacturaDet)
                    {
                        if (factura.Id_Tm == 51)
                        {
                            CN_CapEntradaSalida CNentrada = new CN_CapEntradaSalida();
                            int saldo = 0;
                            CN_CatProducto cn_producto = new CN_CatProducto();
                            Producto prod = new Producto();
                            cn_producto.ConsultaProducto(ref prod, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, facturaDet.Id_Prd);

                            if (((bool)prod.Prd_AparatoSisProp) && prod.Id_Ptp != 2)
                            {
                                CNentrada.ConsultarSaldo(sesion.Id_Emp, sesion.Id_Cd_Ver, facturaDet.Id_Prd.ToString(), facturaDet.Id_Ter.ToString(), factura.Id_Cte.ToString(), sesion.Emp_Cnx, ref saldo, "14");
                                /*
                                if (saldo - facturaDet.Fac_Cant < 0)
                                {
                                    Alerta("El producto " + facturaDet.Id_Prd.ToString() + " no cuenta con el saldo suficiente");
                                    return;
                                }*/
                            }
                        }

                        if (!facturaDet.Fac_Devolucion)
                        { //Crear item de lista de entrada-salida mov. 7
                            EntradaSalidaDetalle entSalDetalle = new EntradaSalidaDetalle();
                            entSalDetalle.Id_Emp = Id_Emp;
                            entSalDetalle.Id_Cd = Id_Cd;
                            entSalDetalle.Id_Es = 0;//se reasigna al insertar la entSal de encabezado
                            entSalDetalle.Id_EsDet = 0;//se reasigna al insertar la entSalDetalle
                            entSalDetalle.Id_Ter = facturaDet.Id_Ter;
                            entSalDetalle.Id_Prd = facturaDet.Id_Prd;
                            entSalDetalle.EsDet_Naturaleza = 0; //entrada
                            entSalDetalle.Es_Cantidad = facturaDet.Fac_Cant;
                            entSalDetalle.Es_Costo = facturaDet.Fac_Precio;
                            entSalDetalle.Es_BuenEstado = true;
                            entSalDetalle.Afct_OrdCompra = false;
                            listaEntSal.Add(entSalDetalle);
                            // ir sumando la cantidad de importe de factura por productos que no se les
                            // a aplicado una devolución
                            importeTotalFactura_ProdNoDevolucion += facturaDet.Fac_Importe;
                        }
                    }
                    /*
                     * Calcular cantidad de Iva en base al porcentaje que representa el importe de la factura a 
                     * cancelar calculado de los productos a los que no se ha aplicado una devolución
                     */
                    importeTotalFactura = factura.Fac_SubTotal != null ? Convert.ToSingle(factura.Fac_SubTotal) : 0;
                    importeTotalFacturaIVA = factura.Fac_ImporteIva != null ? Convert.ToSingle(factura.Fac_ImporteIva) : 0;
                    double porcentaje = 0;
                    if (importeTotalFactura > 0)
                        porcentaje = importeTotalFactura_ProdNoDevolucion / importeTotalFactura;
                    if (porcentaje > 0 && importeTotalFacturaIVA > 0)
                        importeTotalFacturaIVA_ProdNoDevolucion = importeTotalFacturaIVA * porcentaje;

                    CapaDatos.Funciones funciones = new CapaDatos.Funciones();

                    //llenar objeto de entrada-salida, movimiento 7 (cancelación de factura)
                    entSal.Id_Emp = Id_Emp;
                    entSal.Id_Cd = Id_Cd;
                    entSal.Id_U = sesion.Id_U;
                    entSal.Id_Tm = 8; //mov. de entrada por cancelacion de factura, el prod. vuvlve al almacén de la sucursal
                    entSal.Id_Cte = factura.Id_Cte;
                    entSal.Id_Pvd = -1;
                    entSal.Es_Naturaleza = 0;//entrada
                    entSal.Es_Fecha = funciones.GetLocalDateTime(sesion.Minutos);
                    entSal.Es_Referencia = string.Concat("Canc. F-", factura.Id_Fac.ToString());
                    entSal.Es_Notas = string.Concat("Movimiento automático generado por cancelación de factura ", factura.Id_Fac.ToString());
                    entSal.Es_SubTotal = importeTotalFactura_ProdNoDevolucion;
                    entSal.Es_Iva = importeTotalFacturaIVA_ProdNoDevolucion;
                    entSal.Es_Total = importeTotalFactura_ProdNoDevolucion + importeTotalFacturaIVA_ProdNoDevolucion;
                    entSal.Es_Estatus = "I";
                    int verificador = 0;
                    new CN_CapFactura().EliminarFactura(ref factura, sesion.Emp_Cnx, ref verificador, ref entSal, ref listaEntSal);//, ref notaCredito, ref listaNotaCreditoDetalle);
                }

                ImprimirFactura(sesion.Id_Emp, sesion.Id_Cd, factura.Id_Fac,Id_FacSerie, "CANCELACION", string.Concat("Canc. F-", factura.Id_Fac.ToString()));
                if (factura.Fac_Estatus != "B")
                { }
                rgFactura.Rebind();
                Alerta("Factura cancelada exitosamente");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private byte[] Base64ToByte(string data)
        {
            byte[] filebytes = null;
            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    filebytes = Convert.FromBase64String(data);
                }
                return filebytes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ByteToTempPDF(string tempPath, byte[] filebytes)
        {
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
            FileStream fs = new FileStream(tempPath,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None);
            fs.Write(filebytes, 0, filebytes.Length);
            fs.Close();
        }
        private void ShowTempPDF(string tempPath_archivoPDF)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            Process proc = new Process();
            proc.StartInfo = new ProcessStartInfo(tempPath_archivoPDF);
            //new CN_CapFactura().LogError_Insertar("17.1", "carga info de proceso a ejecutar, iniciando proc.Start", sesion.Emp_Cnx);
            proc.Start();
            //proc.Start("IExplore.exe", "C:\\myPath\\myFile.asp");


            //new CN_CapFactura().LogError_Insertar("17.2", "proc.Start iniciado correctamente", sesion.Emp_Cnx);
            while (!proc.HasExited)
            {
                System.Threading.Thread.Sleep(200);
            }
            //new CN_CapFactura().LogError_Insertar("17.3", "finalizó proceso para mostrar impresion", sesion.Emp_Cnx);
            //File.Delete(tempPath_archivoPDF);
            //new CN_CapFactura().LogError_Insertar("17.3", "Borró archivo temporal", sesion.Emp_Cnx);
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
        private void CargarUsuarios()
        {
            try
            {

                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.cmbUsuario);
                //cmbTipo.Items[0].Text = "Todos";

                //cmbUsuario.Items.Remove(0);
                //cmbUsuario.Items.Insert(0, new RadComboBoxItem("-- Todos --", "0"));
                this.cmbUsuario.SelectedValue = "-1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarCliente(ref RadComboBox combo)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatCliente_Combo", ref combo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("TempPDFNoData"))
                    Alerta("El archivo PDF no contiene datos.");
                else
                    if (mensaje.Contains("TempPDFNoEncontrado"))
                        Alerta("No se encontró el archivo PDF de la factura");
                    else
                        if (mensaje.Contains("CapFactura_EsBaja"))
                            Alerta("La factura ya está dada de baja");
                        else
                            if (mensaje.Contains("CapFactura_EstatusIncorrecto"))
                                Alerta("No se puede dar de baja la factura. Estatus incorrecto");
                            else
                                if (mensaje.Contains("PermisoModificarDenegado"))
                                    Alerta("Operación denegada, no tiene permisos para modificar facturas");
                                else
                                    if (mensaje.Contains("PermisoEliminarDenegado"))
                                        Alerta("Operación denegada, no tiene permisos para dar de baja facturas");
                                    else
                                        if (mensaje.Contains("PermisoImprimirDenegado"))
                                            Alerta("Operación denegada, no tiene permisos para imprimir facturas");
                                        else
                                            if (mensaje.Contains("CapFactura_Imprimir_Denegado"))
                                                Alerta("Imposible imprimir el documento");
                                            else
                                                if (mensaje.Contains("CapFactura_Modificar_Denegado"))
                                                    Alerta("Imposible modificar el documento");
                                                else
                                                    if (mensaje.Contains("CapFactura_print_error"))
                                                        Alerta(string.Concat("Error al imprimir la factura. ", mensaje.Replace("'", "\"")));
                                                    else
                                                        if (mensaje.Contains("CapFactura_delete_ok"))
                                                            Alerta("La factura se ha dado de baja (estatus \"B\") correctamente");
                                                        else
                                                            if (mensaje.Contains("CapFactura_TienePagos"))
                                                                Alerta("La factura no puede ser cancelada; tiene pagos aplicados");
                                                            else
                                                                if (mensaje.Contains("btnBuscar_error"))
                                                                    Alerta("Error al momento de filtrar la información");
                                                                else
                                                                    if (mensaje.Contains("RAM1_AjaxRequest"))
                                                                        Alerta("Error al momento de actualizar el grid de factura");
                                                                    else
                                                                        if (mensaje.Contains("rgFactura_NeedDataSource"))
                                                                            Alerta("Error al cargar el grid de detalle de factura");
                                                                        else
                                                                            if (mensaje.Contains("rgFactura_ItemCommand"))
                                                                                Alerta("Error al ejecutar un evento (ItemCommand) al cargar el grid de factura");
                                                                            else
                                                                                if (mensaje.Contains("CapFactura_delete_error"))
                                                                                    Alerta("Error al momento de dar de baja la factura");
                                                                                else
                                                                                    if (mensaje.Contains("radGrid_PageIndexChanged"))
                                                                                        Alerta("Error al cambiar de página");
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