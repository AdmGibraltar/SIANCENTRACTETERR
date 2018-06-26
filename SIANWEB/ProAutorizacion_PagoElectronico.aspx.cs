using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Xml;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;

namespace SIANWEB
{
    public partial class ProAutorizacion_PagoElectronico : System.Web.UI.Page
    {
        private string CryptoPassIn = "k3y9u1m1c4";
        private string CryptoPassOut = "k3y9u1m1c4";
        private string MacolaKey
        {
            get
            {
                if (Session["TimeMacolaServer"] == null || Session["TimeMacolaServer"].ToString() == "")
                {
                    wsMacola.wsMacola wsMac = new wsMacola.wsMacola();
                    Session["TimeMacolaServer"] = (DateTime.Now);
                }

                DateTime TimeServer = (DateTime)Session["TimeMacolaServer"];

                string key = String.Format(
                    "{0}|&|{1}|&|{2}",
                    "SIANWEB",
                    TimeServer.ToString("yyyy-MM-dd HH:mm:ss"),
                    TimeServer.AddSeconds(60).ToString("yyyy-MM-dd HH:mm:ss")
                );

                //return CapaDatos.clsCrypto.BlowFish.Encrypt(key, CryptoPassOut);
                return key;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (Sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);

                //string str = Context.Items["href"].ToString();
                //string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);Context.Items.Add("href", pag[pag.Length-1]);Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);
            }
            else
            {
                if (!IsPostBack)
                {
                    ValidarPermisos();
                    CargarCentros();
                    //jfcv 24oct2016 punto 13 
                    //CargarAcreedores();
                    CargarCDI();

                    RadGrid rgPagoGastoViaje = RadPanelBar1.FindItemByValue("PanelGastoViaje").FindControl("rgPagoGastoViaje") as RadGrid;
                    RadGrid rgPagoCompAcreedor = RadPanelBar1.FindItemByValue("PanelCompAcreedor").FindControl("rgPagoCompAcreedor") as RadGrid;


                    rgPagoGastoViaje.Columns[6].Visible = true;  // Motivo
                    rgPagoGastoViaje.Columns[8].Visible = false;  // Nombre acreedor
                    rgPagoGastoViaje.Columns[9].Visible = false;  // Observaciones
                    rgPagoGastoViaje.Columns[10].Visible = false;  // fecha elaboracion
                    rgPagoGastoViaje.Columns[11].Visible = true;  // fecha entrada
                    rgPagoGastoViaje.Columns[12].Visible = true;  // fecha salida
                    rgPagoGastoViaje.Columns[13].Visible = true;  //Destino

                    rgPagoCompAcreedor.Columns[6].Visible = false;      // Motivo
                    rgPagoCompAcreedor.Columns[8].Visible = true;  // Nombre acreedor
                    rgPagoCompAcreedor.Columns[9].Visible = true;  // Observaciones
                    rgPagoCompAcreedor.Columns[10].Visible = true;  // fecha elaboracion
                    rgPagoCompAcreedor.Columns[11].Visible = false;  // fecha entrada
                    rgPagoCompAcreedor.Columns[12].Visible = false;  // fecha salida
                    rgPagoCompAcreedor.Columns[13].Visible = false;     //Destino


                    Inicializar();
                }
                //jfcv 24oct2016 punto 13 
                //CargarCtaGastos();
            }
        }

        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
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
                //rgAsignacion.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }


        protected void CmbTipo_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //CargarConceptos();
        }
        protected void CmbSubTipo_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //CargarConceptos();
        }
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {

            RadPanelItem item = RadPanelBar1.Items[0];
            RadGrid rgPago = RadPanelBar1.FindItemByValue("PanelItem1").FindControl("rgPago") as RadGrid;
            rgPago.Rebind();

            RadGrid rgPagoGastoViaje = RadPanelBar1.FindItemByValue("PanelGastoViaje").FindControl("rgPagoGastoViaje") as RadGrid;
            rgPagoGastoViaje.Rebind();


            RadGrid rgPagoCompAcreedor = RadPanelBar1.FindItemByValue("PanelCompAcreedor").FindControl("rgPagoCompAcreedor") as RadGrid;
            rgPagoCompAcreedor.Rebind();

            RadGrid rgPagoAcreedor = RadPanelBar1.FindItemByValue("PanelAcreedor").FindControl("rgAcreedor") as RadGrid;
            rgPagoAcreedor.Rebind();

        }


        private DataTable ObtenerDatos(byte[] xmlObject)
        {
            DataTable dtable = new DataTable();
            dtable.Columns.Add("rfc", typeof(string));
            dtable.Columns.Add("fecha", typeof(DateTime));
            dtable.Columns.Add("serie", typeof(string));
            dtable.Columns.Add("folio", typeof(string));
            dtable.Columns.Add("importe", typeof(string));
            dtable.Columns.Add("UUID", typeof(string));
            //jfcv 25nov2015 Agregar 3 valores de impuestos agregar iva
            dtable.Columns.Add("subtotal", typeof(string));
            dtable.Columns.Add("iva", typeof(string));
            dtable.Columns.Add("impuestoretenido", typeof(string));
            dtable.Columns.Add("ivaretenido", typeof(string));




            if (xmlObject != null && xmlObject.Length > 0)
            {
                try
                {
                    MemoryStream xmlStream = new MemoryStream(xmlObject);
                    string rfc = null;
                    string serie = null;
                    string folio = null;
                    DateTime? fecha = null;
                    decimal? importe = null;
                    string UUID = null;
                    //jfcv 24nov2015 Agregar 3 valores de impuestos
                    decimal? subtotal = null;
                    decimal? ivaretenido = null;
                    decimal? impuestoretenido = null;
                    decimal? iva = null;
                    //JFCV 17 junio agregue ieps e ish
                    decimal? ieps = null;
                    decimal? ish = null;
                    //JFCV 31oct2016 agregar validación para campo descuento
                    decimal? descuento = null;

                    XmlDocument xmldoc = new XmlDocument();
                    XmlNodeList xmlnode;

                    xmldoc.Load(xmlStream);
                    Session["xml"] = xmlStream;
                    xmlnode = xmldoc.GetElementsByTagName("cfdi:Comprobante");
                    try
                    {
                        serie = xmlnode[0].Attributes["serie"].Value;
                    }
                    catch
                    {
                        try
                        {
                            serie = xmlnode[0].Attributes["Serie"].Value;
                        }
                        catch
                        {
                            serie = "";
                        }

                         
                    }
                 
                    //JFCV 04 ene 2016 algunas facturas pueden no traer serie ni folio
                    try
                    {
                        folio = xmlnode[0].Attributes["folio"].Value;
                    }
                    catch
                    {
                        try
                        {
                            folio = xmlnode[0].Attributes["Folio"].Value;
                        }
                        catch
                        {
                            folio = "";
                        }
                    }

                    try
                    {
                        fecha = Convert.ToDateTime(xmlnode[0].Attributes["fecha"].Value);
                        #region es anterior a 3.3
                        importe = decimal.Parse("0" + xmlnode[0].Attributes["total"].Value);

                                //jfcv 24nov2015 Agregar 3 valores de impuestos
                                subtotal = decimal.Parse("0" + xmlnode[0].Attributes["subTotal"].Value);
                                //jfcv fin
                                //JFCV  obtener el valor de descuento en caso de que tenga 08 JUNIO 2016
                                //original subtotal = subtotal - decimal.Parse("0" + xmlnode[0].Attributes["descuento"].Value);

                                //JFCV 31oct2016 agregar validación por si no trae descuento
                                descuento = 0;
                                try
                                {
                                    descuento = decimal.Parse("0" + xmlnode[0].Attributes["descuento"].Value);
                                }
                                catch
                                {
                                    descuento = 0;
                                }

                                subtotal = subtotal - descuento;

                                xmlnode = xmldoc.GetElementsByTagName("cfdi:Emisor");

                                try
                                {
                                    rfc = xmlnode[0].Attributes["rfc"].Value;
                                }
                                catch
                                {
                                    rfc = xmlnode[0].Attributes["Rfc"].Value;
                                }
                                 

                                xmlnode = xmldoc.GetElementsByTagName("tfd:TimbreFiscalDigital");
                                UUID = xmlnode[0].Attributes["UUID"].Value;
                                if (folio == "")
                                {
                                    folio = UUID.Substring(0, 10);
                                }
                                //jfcv 24nov2015 Agregar 3 valores de impuestos
                                ////try
                                ////{
                                ////    xmlnode = xmldoc.GetElementsByTagName("cfdi:Impuestos");
                                ////    iva = decimal.Parse("0" + xmlnode[0].Attributes["totalImpuestosTrasladados"].Value);
                                ////}
                                ////catch
                                ////{
                                ////    iva = 0;
                                ////}

                                iva = 0;

                                try
                                {

                                    xmlnode = xmldoc.GetElementsByTagName("cfdi:Traslados");
                                    if (xmlnode[0].ChildNodes.Count > 0)
                                    {
                                        if (xmlnode[0].ChildNodes.Count == 1)
                                        {
                                            if (xmlnode[0].ChildNodes[0].Attributes["impuesto"].Value == "IVA")
                                                iva = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);
                                        }
                                        else
                                        {
                                            if (xmlnode[0].ChildNodes[0].Attributes["impuesto"].Value == "IVA")
                                                iva = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);

                                            if (xmlnode[0].ChildNodes[1].Attributes["impuesto"].Value == "IVA")
                                                iva = iva + decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["importe"].Value);

                                        }
                                    }

                                }
                                catch
                                {

                                }

                                ieps = 0;
                                try
                                {
                                    xmlnode = xmldoc.GetElementsByTagName("cfdi:Traslados");
                                    if (xmlnode[0].ChildNodes.Count > 0)
                                    {
                                        if (xmlnode[0].ChildNodes.Count == 1)
                                        {
                                            if (xmlnode[0].ChildNodes[0].Attributes["impuesto"].Value == "IEPS")
                                                ieps = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);
                                        }
                                        else
                                        {
                                            if (xmlnode[0].ChildNodes[0].Attributes["impuesto"].Value == "IEPS")
                                                ieps = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);
                                            if (xmlnode[0].ChildNodes[1].Attributes["impuesto"].Value == "IEPS")
                                                ieps = ieps + decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["importe"].Value);
                                        }
                                    }
                                }
                                catch
                                {
                                }
                                ish = 0;
                                try
                                {
                                    xmlnode = xmldoc.GetElementsByTagName("implocal:ImpuestosLocales");
                                    if (xmlnode[0].ChildNodes.Count > 0)
                                    {
                                        if (xmlnode[0].ChildNodes.Count == 1)
                                        {
                                            if (xmlnode[0].ChildNodes[0].Attributes["ImpLocTrasladado"].Value == "ISH")
                                                ish = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);
                                        }
                                        else
                                        {
                                            if (xmlnode[0].ChildNodes[0].Attributes["ImpLocTrasladado"].Value == "ISH")
                                                ish = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);
                                            if (xmlnode[0].ChildNodes[1].Attributes["ImpLocTrasladado"].Value == "ISH")
                                                ish = ish + decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["importe"].Value);
                                        }
                                    }
                                }
                                catch
                                {
                                }
                                ivaretenido = 0;
                                impuestoretenido = 0;

                                try
                                {

                                    xmlnode = xmldoc.GetElementsByTagName("cfdi:Retenciones");
                                    if (xmlnode[0].ChildNodes.Count > 0)
                                    {

                                        if (xmlnode[0].ChildNodes.Count == 1)
                                        {
                                            if (xmlnode[0].ChildNodes[0].Attributes["impuesto"].Value == "IVA")
                                                ivaretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);
                                            else
                                                impuestoretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);
                                        }
                                        else
                                        {
                                            if (xmlnode[0].ChildNodes[0].Attributes["impuesto"].Value == "IVA")
                                                ivaretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);
                                            else
                                                impuestoretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);

                                            if (xmlnode[0].ChildNodes[1].Attributes["impuesto"].Value == "IVA")
                                                ivaretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["importe"].Value);
                                            else
                                                impuestoretenido = decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["importe"].Value);

                                        }
                                    }

                                }
                                catch
                                {

                                }
                        #endregion anterior a 3.3

                    }
                    catch
                    {

                        #region es 3.3
                        fecha = Convert.ToDateTime(xmlnode[0].Attributes["Fecha"].Value);
                        importe = decimal.Parse("0" + xmlnode[0].Attributes["Total"].Value);

                        //jfcv 24nov2015 Agregar 3 valores de impuestos
                        subtotal = decimal.Parse("0" + xmlnode[0].Attributes["SubTotal"].Value);
                        //jfcv fin
                        //JFCV  obtener el valor de descuento en caso de que tenga 08 JUNIO 2016
                        //original subtotal = subtotal - decimal.Parse("0" + xmlnode[0].Attributes["descuento"].Value);

                        //JFCV 31oct2016 agregar validación por si no trae descuento
                        descuento = 0;
                        try
                        {
                            descuento = decimal.Parse("0" + xmlnode[0].Attributes["Descuento"].Value);
                        }
                        catch
                        {
                            descuento = 0;
                        }

                        subtotal = subtotal - descuento;

                        xmlnode = xmldoc.GetElementsByTagName("cfdi:Emisor");
                        
                        try
                        {
                            rfc = xmlnode[0].Attributes["rfc"].Value;
                        }
                        catch
                        {
                            rfc = xmlnode[0].Attributes["Rfc"].Value;
                        }


                        xmlnode = xmldoc.GetElementsByTagName("tfd:TimbreFiscalDigital");
                        UUID = xmlnode[0].Attributes["UUID"].Value;
                        if (folio == "")
                        {
                            folio = UUID.Substring(0, 10);
                        }
                      

                        iva = 0;
                        ivaretenido = 0;
                        impuestoretenido = 0;

                        try
                        {

                            //navegamos hasta el nodo de Traslado
                            XmlNodeList comprobantes = xmldoc.GetElementsByTagName("cfdi:Comprobante");
                            XmlNodeList impuest =
                                ((XmlElement)comprobantes[0]).GetElementsByTagName("cfdi:Impuestos");
                            //ME ESTA TRAYENDO TODOS LOS IMPUESTOS INCLUYENDO LOS DE CONCEPTOS ASI QUE VOY A MODIFICARLO
                            XmlNodeList TRASLADOTOTAL =
                               ((XmlElement)comprobantes[0]).GetElementsByTagName("cfdi:Traslado");
                            foreach (XmlElement nodo in TRASLADOTOTAL)
                            {
                                if (!nodo.HasAttribute("Base"))
                                {
                                    if (nodo.Attributes.GetNamedItem("Impuesto").Value == "002")
                                        iva = decimal.Parse("0" + nodo.Attributes.GetNamedItem("Importe").Value);
                                }
                            }

                            //calcular los impuestos retenidos en caso de que tenga 

                            XmlNodeList imptRetenciones =
                              ((XmlElement)impuest[0]).GetElementsByTagName("cfdi:Retenciones");
                            XmlNodeList retenido =
                              ((XmlElement)imptRetenciones[0]).GetElementsByTagName("cfdi:Retencion");

                            foreach (XmlElement nodo in retenido)
                            {

                                try
                                {
                                    if (nodo.Attributes["Impuesto"].Value == "002") //iva retenido
                                        ivaretenido = decimal.Parse("0" + nodo.Attributes.GetNamedItem("Importe").Value);
                                    if (nodo.Attributes.GetNamedItem("Impuesto").Value == "001")
                                        impuestoretenido = decimal.Parse("0" + nodo.Attributes.GetNamedItem("Importe").Value);
                                }
                                catch
                                {
                                    if (nodo.Attributes["impuesto"].Value == "002") //iva retenido
                                        ivaretenido = decimal.Parse("0" + nodo.Attributes.GetNamedItem("Importe").Value);
                                    if (nodo.Attributes.GetNamedItem("impuesto").Value == "001")
                                        impuestoretenido = decimal.Parse("0" + nodo.Attributes.GetNamedItem("Importe").Value);
                                }

                            }


                        }
                        catch
                        {

                        }

                        ieps = 0;
                        try
                        {
                            xmlnode = xmldoc.GetElementsByTagName("cfdi:Traslados");
                            if (xmlnode[0].ChildNodes.Count > 0)
                            {
                                if (xmlnode[0].ChildNodes.Count == 1)
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["Impuesto"].Value == "IEPS")
                                        ieps = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["Importe"].Value);
                                }
                                else
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["Impuesto"].Value == "IEPS")
                                        ieps = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["Importe"].Value);
                                    if (xmlnode[0].ChildNodes[1].Attributes["Impuesto"].Value == "IEPS")
                                        ieps = ieps + decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["Importe"].Value);
                                }
                            }
                        }
                        catch
                        {
                        }
                        ish = 0;
                        try
                        {
                            xmlnode = xmldoc.GetElementsByTagName("implocal:ImpuestosLocales");
                            if (xmlnode[0].ChildNodes.Count > 0)
                            {
                                if (xmlnode[0].ChildNodes.Count == 1)
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["ImpLocTrasladado"].Value == "ISH")
                                        ish = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["importe"].Value);
                                }
                                else
                                {
                                    if (xmlnode[0].ChildNodes[0].Attributes["ImpLocTrasladado"].Value == "ISH")
                                        ish = decimal.Parse("0" + xmlnode[0].ChildNodes[0].Attributes["Importe"].Value);
                                    if (xmlnode[0].ChildNodes[1].Attributes["ImpLocTrasladado"].Value == "ISH")
                                        ish = ish + decimal.Parse("0" + xmlnode[0].ChildNodes[1].Attributes["Importe"].Value);
                                }
                            }
                        }
                        catch
                        {
                        }
               
                        #endregion es 3.3


                    }
                    
                    //jfcv fin 24nov2015 Agregar 3 valores de impuestos

                    //jfcv 24nov2015 Agregar 3 valores de impuestos
                    //dtable.Rows.Add(rfc, fecha, serie, folio, importe, UUID);
                    subtotal = subtotal + ish + ieps;

                    dtable.Rows.Add(rfc, fecha, serie, folio, importe, UUID, subtotal, iva, impuestoretenido, ivaretenido);

                }
                catch
                {
                    dtable = null;
                }
            }
            else
            {
                dtable = null;
            }


            return dtable != null && dtable.Rows.Count > 0 ? dtable : null;
        }

        private string CerosDerecha(string Valor, int Len)
        {
            string Result = "";

            Result = string.Concat(Valor.Trim(), String.Join("", Enumerable.Repeat("0", Len - Valor.Trim().Length)));

            return Result;
        }


        #region Autorizacion de Gastos

        protected void rgPago_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    //jfcv 24 oct 2016 que pueda filtrar por diferentes condiciones 
                    //rgPagoElectronico.DataSource = GetList();
                    int? tipo;
                    int? id_PagElecSubTipo;
                    int? cuenta;
                    int? acreedor;
                    int? estatus;
                    //jfcv 20Oct2016 agregue el id pago ellecrónico en los filtros mejoras 2 punto 11
                    int? id_pagoElectronico;

                    if (CmbTipo.SelectedValue == "")
                    {
                        tipo = null;
                    }
                    else
                    {
                        tipo = Int32.Parse(CmbTipo.SelectedValue);
                    }

                    if (CmbSubTipo.SelectedValue == "")
                    {
                        id_PagElecSubTipo = null;
                    }
                    else
                    {
                        id_PagElecSubTipo = Int32.Parse(CmbSubTipo.SelectedValue);
                    }

                    if (CmbAcreedor.SelectedValue == "")
                    {
                        acreedor = null;
                    }
                    else
                    {
                        acreedor = Int32.Parse(CmbAcreedor.SelectedValue);
                    }

                    if (cmdCtaGastos.SelectedValue == "")
                    {
                        cuenta = null;
                    }
                    else
                    {
                        cuenta = Int32.Parse(cmdCtaGastos.SelectedValue);
                    }



                    if (txtidPagoElectronico.Text == "")
                    {
                        id_pagoElectronico = -1;
                    }
                    else
                    {
                        id_pagoElectronico = Int32.Parse(txtidPagoElectronico.Text);
                    }

                    //RadPanelItem item = RadPanelBar1.Items[0];
                    //RadGrid rgPago = item.Header.FindControl("rgPago") as RadGrid;

                    RadGrid rgPago = RadPanelBar1.FindItemByValue("PanelItem1").FindControl("rgPago") as RadGrid;
                    //Dim InvoicesGrid As RadGrid = DirectCast(RadPanelBar1.FindItemByValue("PanelItem2").FindControl("RadGrid2"), RadGrid)  


                    rgPago.DataSource = GetList(tipo, acreedor, cuenta, id_pagoElectronico, id_PagElecSubTipo);

                    int contador = (((System.Collections.Generic.List<CapaEntidad.PagoElectronico>)(rgPago.DataSource))).Count;
                    RadPanelBar1.Items[0].Text = "Autorización de Gastos   -  Solicitudes Pendientes de Autorizar " + contador.ToString();
 
                     
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgPago_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                PagoElectronico pago = new PagoElectronico();

                if (e.CommandName.ToString() != "Page")
                {
                    int item = e.Item.ItemIndex;
                    //RadPanelItem panel = RadPanelBar1.Items[0];
                    //RadGrid rgpagos = RadPanelBar1.FindItemByValue("PanelItem1").FindControl("rgPago") as RadGrid; 
                    //RadGrid rgPago = panel.Header.FindControl("rgPago") as RadGrid;
                    RadGrid rgPago = RadPanelBar1.FindItemByValue("PanelItem1").FindControl("rgPago") as RadGrid;

                    pago.Id_PagElec = Int32.Parse(rgPago.Items[item]["Id_PagElec"].Text);
                    pago.Id_Cd = Int32.Parse(rgPago.Items[item]["Id_CD"].Text);



                    pago.PagElec_NumeroReferencia = "";

                    //JFCV cambiar numerico por textbox
                    //if (((RadNumericTextBox)(rgPago.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text != string.Empty)
                    //{
                    //    pago.PagElec_NumeroReferencia = Int32.Parse(((RadNumericTextBox)(rgPago.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text);
                    //}
                    if (((RadTextBox)(rgPago.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text != string.Empty)
                    {
                        pago.PagElec_NumeroReferencia = Convert.ToString(((RadTextBox)(rgPago.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text);
                    }
                    if (((RadTextBox)(rgPago.Items[item]["Acr_MotivoRechazo"].FindControl("TxtMotivoRechazo"))).Text != string.Empty)
                    {
                        pago.PagElec_MotivoRechazo = Convert.ToString(((RadTextBox)(rgPago.Items[item]["Acr_MotivoRechazo"].FindControl("TxtMotivoRechazo"))).Text);
                    }
                    switch (e.CommandName.ToString())
                    {
                        case "XML":
                            descargarXML(pago.Id_PagElec);
                            break;
                        case "PDF":
                            descargarPDF(pago.Id_PagElec);
                            break;

                        case "Soporte":
                            descargarSOPORTE(pago.Id_PagElec);
                            break;
                        case "Comprobantes":
                            RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_LstComprobantes('", pago.Id_PagElec, "','", pago.Id_Cd, "')"));
                            break;
                        //JFCV 18 dic 2015 agregue la funcionalidad de rechazar 
                        case "Delete":
                            #region Rechazar
                            Sesion sessionr = new Sesion();
                            sessionr = (Sesion)Session["Sesion" + Session.SessionID];

                            pago.Id_Emp = Int32.Parse(rgPago.Items[item]["Id_Emp"].Text);
                            pago.Id_Cd = Int32.Parse(rgPago.Items[item]["Id_Cd"].Text);

                            if (pago.PagElec_MotivoRechazo != "" && pago.PagElec_MotivoRechazo != null)
                            {
                                int verificador = -1;

                                CapaDatos.dbAccess oDB = new CapaDatos.dbAccess(sessionr.Emp_Cnx);
                                //using ())
                                //{
                                ////////////JFCV 22dic Transacciones  oDB.BeginTransaction();
                                try
                                {
                                    CN_CapPagoElectronico clsPago = new CN_CapPagoElectronico();

                                    clsPago.RechazarPagoElectronico(pago, ref verificador, ref oDB);

                                    if (verificador == 1)
                                    {
                                        ////////////JFCV 22dic Transacciones  oDB.Commit();

                                        try
                                        {
                                            EnviarCorreo(pago, 0);
                                        }
                                        catch (Exception ex)
                                        {
                                            Alerta("La solicitud fue rechazada pero el correo no pudo ser enviado, error: " + ex.Message);
                                        }
                                    }
                                    else //no paso la validación
                                    {
                                        ////////////JFCV 22dic Transacciones  oDB.RollBack();
                                        Alerta("No se pudo rechazar la solicitud");
                                    }
                                    rgPago.Rebind();

                                }
                                catch (Exception ex)
                                {
                                    ////////////JFCV 22dic Transacciones oDB.RollBack();
                                    throw ex;
                                }
                                finally
                                {
                                    oDB.Dispose();
                                }

                            }
                            else
                            {
                                Alerta("Capture el Motivo de Rechazo.");
                            }
                            #endregion Rechazar
                            break;
                        case "Autorizar":
                            #region Autorizar
                            Sesion session = new Sesion();
                            session = (Sesion)Session["Sesion" + Session.SessionID];

                            if (pago.PagElec_NumeroReferencia != "")
                            {
                                int verificador = -1;


                                pago.Id_Emp = Int32.Parse(rgPago.Items[item]["Id_Emp"].Text);
                                pago.Id_Cd = Int32.Parse(rgPago.Items[item]["Id_Cd"].Text);



                                CapaDatos.dbAccess oDB = new CapaDatos.dbAccess(session.Emp_Cnx);
                                //using ())
                                //{
                                ////////////JFCV 22dic Transacciones oDB.BeginTransaction();
                                try
                                {
                                    CN_CapPagoElectronico clsPago = new CN_CapPagoElectronico();

                                    clsPago.AutorizarPagoElectronico(pago, ref verificador, ref oDB);



                                    if (verificador == 1)
                                    {
                                        wsMacola.wsMacola wsMac = new wsMacola.wsMacola();

                                        PagoElectronico PartidasGastos = new PagoElectronico();
                                        PartidasGastos.Id_Emp = Int32.Parse(rgPago.Items[item]["Id_Emp"].Text);
                                        PartidasGastos.Id_Cd = Int32.Parse(rgPago.Items[item]["Id_Cd"].Text);
                                        PartidasGastos.Id_PagElec = Int32.Parse(rgPago.Items[item]["Id_PagElec"].Text);
                                        clsPago.ConsultaPagoElectronicoAutorizacion(PartidasGastos, session.Emp_Cnx);

                                        //JFCV obtengo el importe de la solicitud 
                                        decimal? importeapagar = Convert.ToDecimal(rgPago.Items[item]["PagElec_ImporteSumar"].Text);
                                        decimal? importesoporte = importeapagar;

                                        List<wsMacola.Gasto> lstGasto = new List<wsMacola.Gasto>();
                                        if (rgPago.Items[item]["Id_PagElecTipo"].Text == "3" && PartidasGastos.PagElecArchivo.Count == 0 && rgPago.Items[item]["PagElec_Soporte"].Text.Length <= 8)
                                        {
                                            //convertir el valor de string to byte 
                                            wsMacola.Gasto gasto = new wsMacola.Gasto();



                                            gasto.id_trx = Int32.Parse(rgPago.Items[item]["Id_PagElec"].Text);
                                            gasto.cus_type_cd = rgPago.Items[item]["Id_Cd"].Text;
                                            //JFCV 29 sep 2015 el valor de vend_no truncarlo a 12 caracteres porque esa es la longitud en la tabla 
                                            if (rgPago.Items[item]["Acr_NumeroGenerado"].Text.Length > 12)
                                                gasto.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text.Substring(0, 12);
                                            else
                                                gasto.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text;
                                            //JFCV agregar el numero de referencia en el web service
                                            gasto.no_ref = Convert.ToString(((RadTextBox)(rgPago.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text);


                                            gasto.inv_no = "";
                                            gasto.amt = importesoporte;
                                            gasto.trx_dt = Convert.ToDateTime(rgPago.Items[item]["PagElec_FechaRequiere"].Text);

                                            //JFCV agregar retenciones si es una factura de soporte el iva y el impuesto retenido es cero
                                            gasto.sub_amt = importesoporte;
                                            gasto.tax_amt = 0;
                                            gasto.ret_amt = 0;
                                            gasto.iva_ret = 0;

                                            gasto.dwn = null; //null
                                            gasto.dwn_dt = null; //null
                                            gasto.vchr_no = null; //null
                                            gasto.mov_type = null; //null
                                            //jfcv 0909 2016 agregar columan de subtipo para enviarselo a macola 
                                            gasto.mov_type = rgPago.Items[item]["Id_PagElecSubTipo"].Text;
                                            if (Convert.ToInt32(gasto.mov_type) == -1)
                                                gasto.mov_type = null;
                                            //fin jfcv 09092016

                                            //JFCV 13 ene 2016 agregar cuenta y banco 
                                            gasto.bank_name = null;
                                            gasto.bank_account = null;



                                            gasto.account_no = String.Concat(
                                                CerosDerecha(rgPago.Items[item]["PagElec_Cc"].Text, 8),
                                                CerosDerecha(rgPago.Items[item]["PagElec_SubCuenta"].Text, 8),
                                                CerosDerecha(rgPago.Items[item]["PagElec_SubSubCuenta"].Text, 8)
                                            );
                                            gasto.SAT_no = "";
                                            try
                                            {
                                                gasto.flg_test = Convert.ToBoolean(ConfigurationManager.AppSettings["insGastoMacola"].ToString());
                                            }
                                            catch
                                            {
                                                gasto.flg_test = false;
                                            }
                                            //JFCV 22 ago 2016 agregar el mail del usuario 
                                            gasto.mail_usu = session.U_Correo;
                                            lstGasto.Add(gasto);

                                        }
                                        else
                                        {
                                            foreach (PagoElectronicoArchivo factura in PartidasGastos.PagElecArchivo)
                                            {
                                                wsMacola.Gasto gasto = new wsMacola.Gasto();

                                                gasto.id_trx = Int32.Parse(rgPago.Items[item]["Id_PagElec"].Text);
                                                gasto.cus_type_cd = rgPago.Items[item]["Id_Cd"].Text;
                                                //JFCV 29 sep 2015 el valor de vend_no truncarlo a 12 caracteres porque esa es la longitud en la tabla 
                                                if (rgPago.Items[item]["Acr_NumeroGenerado"].Text.Length > 12)
                                                    gasto.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text.Substring(0, 12);
                                                else
                                                    gasto.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text;
                                                //JFCV agregar el numero de referencia en el web service
                                                gasto.no_ref = Convert.ToString(((RadTextBox)(rgPago.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text);

                                                DataTable DT = ObtenerDatos(factura.PagElec_XMLStream.Length > 0 ? factura.PagElec_XMLStream : null);
                                                gasto.inv_no = string.Concat((string)DT.Rows[0]["serie"], (string)DT.Rows[0]["folio"]);
                                                //15junio2018 que trunque el folio a 14 caracteres porque macola solo acepta eso
                                                int longitud = gasto.inv_no.Length;
                                                if (longitud > 14)
                                                    gasto.inv_no = gasto.inv_no.Substring(longitud-14,14);
                                                
                                                gasto.amt = Convert.ToDecimal("0" + (string)DT.Rows[0]["importe"]);
                                                gasto.trx_dt = (DateTime)DT.Rows[0]["fecha"];

                                                //JFCV agregar retenciones
                                                gasto.sub_amt = Convert.ToDecimal("0" + (string)DT.Rows[0]["subtotal"]);
                                                gasto.tax_amt = Convert.ToDecimal("0" + (string)DT.Rows[0]["iva"]);
                                                gasto.ret_amt = Convert.ToDecimal("0" + (string)DT.Rows[0]["impuestoretenido"]);
                                                gasto.iva_ret = Convert.ToDecimal("0" + (string)DT.Rows[0]["ivaretenido"]);

                                                //JFCV calcular el monto de la suma de los comprobantes para restarlo al total y obtener el monto de lo que es soporte 
                                                importesoporte = importesoporte - gasto.amt;


                                                gasto.dwn = null; //null
                                                gasto.dwn_dt = null; //null
                                                gasto.vchr_no = null; //null
                                                gasto.mov_type = null; //null
                                                //jfcv 0909 2016 agregar columan de subtipo para enviarselo a macola 
                                                gasto.mov_type = rgPago.Items[item]["Id_PagElecSubTipo"].Text;
                                                if (Convert.ToInt32(gasto.mov_type) == -1)
                                                    gasto.mov_type = null;
                                                //fin jfcv 09092016

                                                gasto.account_no = String.Concat(
                                                    CerosDerecha(factura.PagElec_Cc, 8),
                                                    CerosDerecha(factura.PagElec_SubCuenta, 8),
                                                    CerosDerecha(factura.PagElec_SubSubCuenta, 8)
                                                );
                                                gasto.SAT_no = (string)DT.Rows[0]["UUID"];
                                                try
                                                {
                                                    gasto.flg_test = Convert.ToBoolean(ConfigurationManager.AppSettings["insGastoMacola"].ToString());
                                                }
                                                catch
                                                {
                                                    gasto.flg_test = false;
                                                }
                                                //JFCV 22 ago 2016 agregar el mail del usuario 
                                                gasto.mail_usu = session.U_Correo;
                                                lstGasto.Add(gasto);
                                            }


                                            // me falta obtener el importe  
                                            //JFCV si el soporte tiene un valor genero un registro en lstGasto
                                            // if (rgPago.Items[item]["PagElec_Soporte"].Text != "")
                                            if (rgPago.Items[item]["PagElec_Soporte"].Text != null && rgPago.Items[item]["PagElec_Soporte"].Text.Length > 8)
                                            {
                                                //convertir el valor de string to byte 
                                                wsMacola.Gasto gasto = new wsMacola.Gasto();

                                                gasto.id_trx = Int32.Parse(rgPago.Items[item]["Id_PagElec"].Text);
                                                gasto.cus_type_cd = rgPago.Items[item]["Id_Cd"].Text;
                                                //JFCV 29 sep 2015 el valor de vend_no truncarlo a 12 caracteres porque esa es la longitud en la tabla 
                                                if (rgPago.Items[item]["Acr_NumeroGenerado"].Text.Length > 12)
                                                    gasto.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text.Substring(0, 12);
                                                else
                                                    gasto.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text;
                                                //JFCV agregar el numero de referencia en el web service
                                                gasto.no_ref = Convert.ToString(((RadTextBox)(rgPago.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text);


                                                gasto.inv_no = "";
                                                gasto.amt = importesoporte;
                                                gasto.trx_dt = Convert.ToDateTime(rgPago.Items[item]["PagElec_FechaRequiere"].Text);

                                                //JFCV agregar retenciones si es una factura de soporte el iva y el impuesto retenido es cero
                                                gasto.sub_amt = importesoporte;
                                                gasto.tax_amt = 0;
                                                gasto.ret_amt = 0;
                                                gasto.iva_ret = 0;

                                                gasto.dwn = null; //null
                                                gasto.dwn_dt = null; //null
                                                gasto.vchr_no = null; //null
                                                gasto.mov_type = null; //null
                                                //jfcv 0909 2016 agregar columan de subtipo para enviarselo a macola 
                                                gasto.mov_type = rgPago.Items[item]["Id_PagElecSubTipo"].Text;
                                                if (Convert.ToInt32(gasto.mov_type) == -1)
                                                    gasto.mov_type = null;
                                                //fin jfcv 09092016

                                                //JFCV 13 ene 2016 agregar cuenta y banco 
                                                gasto.bank_name = null;
                                                gasto.bank_account = null;



                                                gasto.account_no = String.Concat(
                                                    CerosDerecha(rgPago.Items[item]["PagElec_Cc"].Text, 8),
                                                    CerosDerecha(rgPago.Items[item]["PagElec_SubCuenta"].Text, 8),
                                                    CerosDerecha(rgPago.Items[item]["PagElec_SubSubCuenta"].Text, 8)
                                                );
                                                gasto.SAT_no = "";
                                                try
                                                {
                                                    gasto.flg_test = Convert.ToBoolean(ConfigurationManager.AppSettings["insGastoMacola"].ToString());
                                                }
                                                catch
                                                {
                                                    gasto.flg_test = false;
                                                }
                                                //JFCV 22 ago 2016 agregar el mail del usuario 
                                                gasto.mail_usu = session.U_Correo;
                                                lstGasto.Add(gasto);

                                            }

                                        }
                                        //JFCV poner aqui a ver como queda para que valide si truena 
                                        // y validar soprte de otra manera porque no funciona asi como esta 
                                        //try
                                        //{
                                        //    if (wsMac.insGasto(MacolaKey, lstGasto.ToArray()))
                                        //        oDB.Commit();
                                        //    else
                                        //        oDB.RollBack();

                                        //}
                                        //catch (Exception ex)
                                        //{
                                        //    Alerta(ex.Message);
                                        //}


                                        //if (wsMac.insGasto(MacolaKey, lstGasto.ToArray()))
                                        string resultado = wsMac.insGasto(MacolaKey, lstGasto.ToArray());
                                        if (resultado == "ok")

                                        //wsMac.insGasto(MacolaKey, lstGasto.ToArray());
                                        {
                                            ////////////JFCV 22dic Transacciones oDB.Commit();

                                            try
                                            {
                                                EnviarCorreo(pago, 1);
                                            }
                                            catch (Exception ex)
                                            {
                                               
                                                Alerta(ex.Message);
                                            }
                                        }
                                        else //no paso la validación
                                        {
                                            ////////////JFCV 22dic Transacciones oDB.RollBack();
                                            EnviarCorreoError("Error al enviar a macola folio:" + pago.Id_PagElec, "Autorizacioncentral");
                                            Alerta("No se pudo Autorizar la solicitud: " + resultado);
                                        }
                                    }
                                    else
                                    {
                                        EnviarCorreoError("Error al enviar a macola folio:"  , "Autorizacioncentral_Else");
                                        ////////////JFCV 22dic Transacciones oDB.RollBack();
                                    }

                                    rgPago.Rebind();
                                }
                                catch (Exception ex)
                                {
                                    ////////////JFCV 22dic Transacciones oDB.RollBack();
                                    EnviarCorreoError("Ocurrio un error al Autorizar:", "Autorizacioncentral_Else_Else");
                                    throw ex;
                                }
                                finally
                                {
                                    oDB.Dispose();
                                }
                                //}
                            }
                            else
                            {
                                Alerta("Capture el Número de Referencia.");
                            }
                            #endregion Autorizar
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void rgPago_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                RadPanelItem item = RadPanelBar1.Items[0];
                //RadGrid rgPago = item.Header.FindControl("rgPago") as RadGrid;
                RadGrid rgPago = RadPanelBar1.FindItemByValue("PanelItem1").FindControl("rgPago") as RadGrid;

                rgPago.Rebind();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion

        #region Autorizacion de Gastos de Viaje
        protected void rgPagoGastoViaje_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    RadGrid rgPago = RadPanelBar1.FindItemByValue("PanelGastoViaje").FindControl("rgPagoGastoViaje") as RadGrid;
                    rgPago.DataSource = GetList(1);
                    int contador = (((System.Collections.Generic.List<CapaEntidad.GastoViaje>)(rgPago.DataSource))).Count;
                    RadPanelBar1.Items[1].Text = "Autorización de Gastos de Viaje  -  Solicitudes Pendientes de Autorizar " + contador.ToString();
                    
                 
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgPagoGastoViaje_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GastoViaje gastoViaje = new GastoViaje();

                int item = e.Item.ItemIndex;

                RadGrid rgPago = RadPanelBar1.FindItemByValue("PanelGastoViaje").FindControl("rgPagoGastoViaje") as RadGrid;
                gastoViaje.Id_GV = Int32.Parse(rgPago.Items[item]["Id_GV"].Text);
                gastoViaje.Id_Emp = Int32.Parse(rgPago.Items[item]["Id_Emp"].Text);
                gastoViaje.Id_Cd = Int32.Parse(rgPago.Items[item]["Id_Cd"].Text);

                if (((RadTextBox)(rgPago.Items[item]["Acr_MotivoRechazo"].FindControl("TxtMotivoRechazo"))).Text != string.Empty)
                {
                    gastoViaje.GV_MotivoRechazo = Convert.ToString(((RadTextBox)(rgPago.Items[item]["Acr_MotivoRechazo"].FindControl("TxtMotivoRechazo"))).Text);
                }
                //diferencia 
                decimal diferencia = Decimal.Parse(rgPago.Items[item]["GV_Saldo_Comprobar"].Text);

                switch (e.CommandName.ToString())
                {
                    case "XML":
                        //descargarXML(pago.Id_PagElec);
                        break;
                    case "PDF":
                        //descargarPDF(pago.Id_PagElec);
                        break;
                    //JFCV 24 feb 2016 agregue la funcionalidad de abrir comprobantes 
                    case "Comprobantes":
                        RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_LstComprobantesGV('", gastoViaje.Id_GV, "' ,'", gastoViaje.Id_Emp, "' ,'", gastoViaje.Id_Cd, "')"));


                        break;
                    //JFCV 18 dic 2015 agregue la funcionalidad de rechazar 
                    case "Delete":
                        #region Rechazar
                        Sesion sessionr = new Sesion();
                        sessionr = (Sesion)Session["Sesion" + Session.SessionID];
                        gastoViaje.UsuarioMod = sessionr.Id_U;

                        if (gastoViaje.GV_MotivoRechazo != "" && gastoViaje.GV_MotivoRechazo != null)
                        {
                            int verificador = -1;

                            CapaDatos.dbAccess oDB = new CapaDatos.dbAccess(sessionr.Emp_Cnx);
                            //using ())
                            //{

                            try
                            {

                                CN_CapGastoViaje clsGastoViaje = new CN_CapGastoViaje();
                                clsGastoViaje.RechazarGastoViaje(gastoViaje, sessionr.Emp_Cnx, ref verificador);

                                if (verificador == 1)
                                {

                                    try
                                    {
                                        PagoElectronico pagoElec = new PagoElectronico();
                                        pagoElec.Id_PagElec = Int32.Parse(rgPago.Items[item]["Id_PagElec"].Text);
                                        pagoElec.PagElec_MotivoRechazo = gastoViaje.GV_MotivoRechazo;
                                        EnviarCorreo(pagoElec, 0);
                                    }
                                    catch (Exception ex)
                                    {
                                        this.lblMensaje.Text = "El Gasto de Viaje fue rechazado pero el correo no pudo ser enviado, error: " + ex.Message;
                                    }
                                }
                                else //no paso la validación
                                {
                                    this.lblMensaje.Text = "No se pudo rechazar el Gasto de Viaje ";
                                }


                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                            finally
                            {

                            }

                        }
                        else
                        {
                            this.lblMensaje.Text = "Capture el Motivo de Rechazo.";
                        }
                        #endregion Rechazar
                        break;
                    case "Autorizar":

                        #region Autorizar COmprobante
                        Sesion session = new Sesion();
                        session = (Sesion)Session["Sesion" + Session.SessionID];
                        gastoViaje.UsuarioMod = session.Id_U;


                        int verificadorins = -1;

                        ///JFCV TODO Definir rango de tolerancia 
                        if (diferencia >= -1)  // si la diferencia no es 0 o a favor no hace nada 
                        {


                            CN_CapGastoViaje clsGastoViajeAut = new CN_CapGastoViaje();

                            CapaDatos.dbAccess oDBaut = new CapaDatos.dbAccess(session.Emp_Cnx);

                            try
                            {

                                #region Autorizar

                                CN_CapPagoElectronico clsPago = new CN_CapPagoElectronico();

                                CN_CapGastoViajeComprobante clsGastoViajeComprobante = new CN_CapGastoViajeComprobante();
                                List<GastoViajeComprobante> list = new List<GastoViajeComprobante>();

                                GastoViajeComprobante gastoViajeComprobante = new GastoViajeComprobante();
                                gastoViajeComprobante.Id_Emp = Int32.Parse(rgPago.Items[item]["Id_Emp"].Text);
                                gastoViajeComprobante.Id_Cd = Int32.Parse(rgPago.Items[item]["Id_Cd"].Text);
                                gastoViajeComprobante.Id_GV = Int32.Parse(rgPago.Items[item]["Id_GV"].Text);

                                clsGastoViajeComprobante.ConsultaGastoViajeComprobante(gastoViajeComprobante, session.Emp_Cnx, ref list);

                                PagoElectronico pagoelectronico = new PagoElectronico();
                                pagoelectronico.Id_Emp = Int32.Parse(rgPago.Items[item]["Id_Emp"].Text);
                                pagoelectronico.Id_Cd = Int32.Parse(rgPago.Items[item]["Id_Cd"].Text);
                                pagoelectronico.Id_GV = Int32.Parse(rgPago.Items[item]["Id_GV"].Text);
                                pagoelectronico.Id_PagElec = Int32.Parse(rgPago.Items[item]["Id_PagElec"].Text);
                                CN_CapPagoElectronico clsPagoElectronico = new CN_CapPagoElectronico();
                                clsPagoElectronico.ConsultaPagoElectronicoAutorizacion(pagoelectronico, session.Emp_Cnx);

                                wsMacola.wsMacola wsMac = new wsMacola.wsMacola();


                                List<wsMacola.Gasto> lstGasto = new List<wsMacola.Gasto>();
                                List<wsMacola.Gasto> lstGastoDiferencia = new List<wsMacola.Gasto>();


                                #region foreach de comprobantes
                                foreach (GastoViajeComprobante comprobante in list)
                                {
                                    wsMacola.Gasto gasto = new wsMacola.Gasto();


                                    if (comprobante.GVComprobante_ConComprobante)  //Inserto los que tienen comprobante 
                                    {
                                        #region Con Comprobante
                                        gasto.id_trx = Int32.Parse(rgPago.Items[item]["Id_PagElec"].Text);
                                        gasto.cus_type_cd = rgPago.Items[item]["Id_Cd"].Text;
                                        //JFCV 29 sep 2015 el valor de vend_no truncarlo a 12 caracteres porque esa es la longitud en la tabla 
                                        if (rgPago.Items[item]["Acr_NumeroGenerado"].Text.Length > 12)
                                            gasto.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text.Substring(0, 12);
                                        else
                                            gasto.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text;
                                        //JFCV agregar el numero de referencia en el web service
                                        gasto.no_ref = Convert.ToString(((RadTextBox)(rgPago.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text);

                                        DataTable DT = ObtenerDatos(comprobante.GVComprobante_XmlStream.Length > 0 ? comprobante.GVComprobante_XmlStream : null);

                                        gasto.inv_no = string.Concat((string)DT.Rows[0]["serie"], (string)DT.Rows[0]["folio"]);
                                        //15junio2018 que trunque el folio a 14 caracteres porque macola solo acepta eso 
                                        int longitud = gasto.inv_no.Length;
                                        if (longitud > 14)
                                            gasto.inv_no = gasto.inv_no.Substring(longitud - 14, 14);

                                        gasto.amt = Convert.ToDecimal("0" + (string)DT.Rows[0]["importe"]);
                                        gasto.trx_dt = (DateTime)DT.Rows[0]["fecha"];

                                        //JFCV agregar retenciones
                                        gasto.sub_amt = Convert.ToDecimal("0" + (string)DT.Rows[0]["subtotal"]);
                                        gasto.tax_amt = Convert.ToDecimal("0" + (string)DT.Rows[0]["iva"]);
                                        gasto.ret_amt = Convert.ToDecimal("0" + (string)DT.Rows[0]["impuestoretenido"]);
                                        gasto.iva_ret = Convert.ToDecimal("0" + (string)DT.Rows[0]["ivaretenido"]);

                                        gasto.dwn = null; //null
                                        gasto.dwn_dt = null; //null
                                        gasto.vchr_no = null; //null
                                        gasto.mov_type = null; //null
                                        //jfcv 0909 2016 agregar columan de subtipo para enviarselo a macola 
                                        gasto.mov_type = pagoelectronico.Id_PagElecSubTipo.ToString();   //rgPago.Items[item]["Id_PagElecSubTipo"].Text;
                                        if (Convert.ToInt32(gasto.mov_type) == -1)
                                            gasto.mov_type = null;
                                        //fin jfcv 09092016

                                        gasto.account_no = String.Concat(
                                            CerosDerecha(comprobante.GVComprobante_GV_Cc, 8),
                                            CerosDerecha(comprobante.GVComprobante_GV_SubCuenta, 8),
                                            CerosDerecha(comprobante.GVComprobante_GV_SubSubCuenta, 8)
                                        );
                                        gasto.SAT_no = (string)DT.Rows[0]["UUID"];
                                        try
                                        {
                                            gasto.flg_test = Convert.ToBoolean(ConfigurationManager.AppSettings["insGastoMacola"].ToString());
                                        }
                                        catch
                                        {
                                            gasto.flg_test = false;
                                        }
                                        //JFCV 22 ago 2016 agregar el mail del usuario 
                                        gasto.mail_usu = session.U_Correo;
                                        lstGasto.Add(gasto);
                                        #endregion con comprobante
                                    }
                                    else
                                    {
                                        #region Enviar si tiene archivo de soporte

                                        gasto.id_trx = Int32.Parse(rgPago.Items[item]["Id_PagElec"].Text);
                                        gasto.cus_type_cd = rgPago.Items[item]["Id_Cd"].Text;
                                        //JFCV 29 sep 2015 el valor de vend_no truncarlo a 12 caracteres porque esa es la longitud en la tabla 
                                        if (rgPago.Items[item]["Acr_NumeroGenerado"].Text.Length > 12)
                                            gasto.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text.Substring(0, 12);
                                        else
                                            gasto.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text;
                                        //JFCV agregar el numero de referencia en el web service
                                        gasto.no_ref = Convert.ToString(((RadTextBox)(rgPago.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text);


                                        gasto.inv_no = "";
                                        gasto.amt = comprobante.GVComprobante_Importe;
                                        gasto.trx_dt = comprobante.GVComprobante_Fecha;

                                        //JFCV agregar retenciones si es una factura de soporte el iva y el impuesto retenido es cero
                                        gasto.sub_amt = comprobante.GVComprobante_Importe;
                                        gasto.tax_amt = 0;
                                        gasto.ret_amt = 0;
                                        gasto.iva_ret = 0;

                                        gasto.dwn = null; //null
                                        gasto.dwn_dt = null; //null
                                        gasto.vchr_no = null; //null
                                        gasto.mov_type = null; //null
                                        //jfcv 0909 2016 agregar columan de subtipo para enviarselo a macola 
                                        gasto.mov_type = pagoelectronico.Id_PagElecSubTipo.ToString();
                                        if (Convert.ToInt32(gasto.mov_type) == -1)
                                            gasto.mov_type = null;
                                        //fin jfcv 09092016

                                        gasto.bank_name = null;
                                        gasto.bank_account = null;
                                        gasto.account_no = String.Concat(
                                           CerosDerecha(comprobante.GVComprobante_GV_Cc, 8),
                                           CerosDerecha(comprobante.GVComprobante_GV_SubCuenta, 8),
                                           CerosDerecha(comprobante.GVComprobante_GV_SubSubCuenta, 8)
                                        );

                                        gasto.SAT_no = "";
                                        try
                                        {
                                            gasto.flg_test = Convert.ToBoolean(ConfigurationManager.AppSettings["insGastoMacola"].ToString());
                                        }
                                        catch
                                        {
                                            gasto.flg_test = false;
                                        }

                                        //JFCV 22 ago 2016 agregar el mail del usuario 
                                        gasto.mail_usu = session.U_Correo;

                                        lstGasto.Add(gasto);


                                        #endregion Enviar si tiene archivo de soporte
                                    }

                                }

                                #endregion foreach de comprobantes

                                #region Enviar a GL el monto original en saldo negativo

                                wsMacola.Gasto gastoGL = new wsMacola.Gasto();
                                gastoGL.id_trx = Int32.Parse(rgPago.Items[item]["Id_PagElec"].Text);
                                gastoGL.cus_type_cd = rgPago.Items[item]["Id_Cd"].Text;
                                if (rgPago.Items[item]["Acr_NumeroGenerado"].Text.Length > 12)
                                    gastoGL.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text.Substring(0, 12);
                                else
                                    gastoGL.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text;
                                gastoGL.no_ref = Convert.ToString(((RadTextBox)(rgPago.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text);
                                gastoGL.inv_no = "";
                                gastoGL.amt = Convert.ToDecimal(rgPago.Items[item]["GV_importe"].Text) * -1;
                                gastoGL.trx_dt = Convert.ToDateTime(DateTime.Now);
                                gastoGL.sub_amt = gastoGL.amt;
                                gastoGL.tax_amt = 0;
                                gastoGL.ret_amt = 0;
                                gastoGL.iva_ret = 0;
                                gastoGL.dwn = null; //null
                                gastoGL.dwn_dt = null; //null
                                gastoGL.vchr_no = null; //null
                                gastoGL.mov_type = null; //null
                                //jfcv 0909 2016 agregar columan de subtipo para enviarselo a macola 
                                gastoGL.mov_type = pagoelectronico.Id_PagElecSubTipo.ToString();
                                if (Convert.ToInt32(gastoGL.mov_type) == -1)
                                    gastoGL.mov_type = null;
                                //fin jfcv 09092016

                                gastoGL.bank_name = null;
                                gastoGL.bank_account = null;
                                gastoGL.SAT_no = "";
                                gastoGL.flg_test = false;
                                gastoGL.account_no = String.Concat(
                                 CerosDerecha(pagoelectronico.PagElec_Cc, 8),
                                 CerosDerecha(pagoelectronico.PagElec_SubCuenta, 8),
                                 CerosDerecha(pagoelectronico.PagElec_SubSubCuenta, 8)
                                );
                                //JFCV 22 ago 2016 agregar el mail del usuario 
                                gastoGL.mail_usu = session.U_Correo;
                                lstGasto.Add(gastoGL);
                                #endregion Enviar a GL el monto original en saldo negativo
                                // si la diferencia es  en contra  no inserto nada y mando mensaje 
                                // si la diferencia es a favor inserto y aparte inserto en insGasto la diferencia 
                                decimal montominimocheque = Convert.ToDecimal(ConfigurationManager.AppSettings["montominimocheque"].ToString());
                                if (diferencia > 0 && diferencia >= montominimocheque)  //Si la diferencia es mayor a cero inserto otro movimiento en macola 
                                {
                                    #region Enviar a GL el monto de la diferencia pero en negativo ( contrario a lo que traigo )
                                    wsMacola.Gasto gastoGLDiferencia = new wsMacola.Gasto();
                                    gastoGLDiferencia.id_trx = Int32.Parse(rgPago.Items[item]["Id_PagElec"].Text);
                                    gastoGLDiferencia.cus_type_cd = rgPago.Items[item]["Id_Cd"].Text;
                                    if (rgPago.Items[item]["Acr_NumeroGenerado"].Text.Length > 12)
                                        gastoGLDiferencia.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text.Substring(0, 12);
                                    else
                                        gastoGLDiferencia.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text;
                                    // JFCV TODO ver de cuanto es el margen para insertar diferencias 
                                    gastoGLDiferencia.no_ref = Convert.ToString(((RadTextBox)(rgPago.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text);

                                    gastoGLDiferencia.inv_no = "";
                                    gastoGLDiferencia.amt = diferencia * -1;
                                    gastoGLDiferencia.trx_dt = Convert.ToDateTime(DateTime.Now);

                                    gastoGLDiferencia.sub_amt = gastoGLDiferencia.amt;
                                    gastoGLDiferencia.tax_amt = 0;
                                    gastoGLDiferencia.ret_amt = 0;
                                    gastoGLDiferencia.iva_ret = 0;
                                    gastoGLDiferencia.dwn = null; //null
                                    gastoGLDiferencia.dwn_dt = null; //null
                                    gastoGLDiferencia.vchr_no = null; //null
                                    gastoGLDiferencia.mov_type = null; //null
                                    //jfcv 0909 2016 agregar columan de subtipo para enviarselo a macola 
                                    gastoGLDiferencia.mov_type = pagoelectronico.Id_PagElecSubTipo.ToString();
                                    if (Convert.ToInt32(gastoGLDiferencia.mov_type) == -1)
                                        gastoGLDiferencia.mov_type = null;
                                    //fin jfcv 09092016

                                    gastoGLDiferencia.bank_name = null;
                                    gastoGLDiferencia.bank_account = null;
                                    gastoGLDiferencia.SAT_no = "";
                                    gastoGLDiferencia.flg_test = false;
                                    gastoGLDiferencia.account_no = String.Concat(
                                     CerosDerecha(pagoelectronico.PagElec_Cc, 8),
                                     CerosDerecha(pagoelectronico.PagElec_SubCuenta, 8),
                                     CerosDerecha(pagoelectronico.PagElec_SubSubCuenta, 8)
                                    );
                                    //JFCV 22 ago 2016 agregar el mail del usuario 
                                    gastoGLDiferencia.mail_usu = session.U_Correo;
                                    lstGasto.Add(gastoGLDiferencia);
                                    #endregion Enviar a GL el monto de la diferencia pero en negativo ( contrario a lo que traigo )
                                }
                                ////////////JFCV 22dic Transacciones  oDBaut.BeginTransaction();

                                string resultado = wsMac.insGastoViaje(MacolaKey, lstGasto.ToArray());


                                if (resultado == "ok")
                                {

                                    //JFCV obtengo el monto minimo por el que puedo generar un cheque , 
                                    // si la diferencia es menor a ese monto , entonces no genero el segundo movimiento 
                                    // a la tabla de CXP



                                    if (diferencia > 0 && diferencia >= montominimocheque)  //Si la diferencia es mayor a cero inserto otro movimiento en macola 
                                    {
                                        wsMacola.Gasto gastodferencia = new wsMacola.Gasto();

                                        #region Enviar el gasto en caso de que tenga una diferencia a favor


                                        gastodferencia.id_trx = Int32.Parse(rgPago.Items[item]["Id_PagElec"].Text);
                                        gastodferencia.cus_type_cd = rgPago.Items[item]["Id_Cd"].Text;
                                        //JFCV 29 sep 2015 el valor de vend_no truncarlo a 12 caracteres porque esa es la longitud en la tabla 
                                        if (rgPago.Items[item]["Acr_NumeroGenerado"].Text.Length > 12)
                                            gastodferencia.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text.Substring(0, 12);
                                        else
                                            gastodferencia.vend_no = rgPago.Items[item]["Acr_NumeroGenerado"].Text;
                                        //JFCV agregar el numero de referencia en el web service
                                        gastodferencia.no_ref = Convert.ToString(((RadTextBox)(rgPago.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text);


                                        gastodferencia.inv_no = "";
                                        gastodferencia.amt = diferencia;
                                        // gastodferencia.trx_dt = Convert.ToDateTime(rgPago.Items[item]["GV_FechaSalida"].Text);
                                        gastodferencia.trx_dt = Convert.ToDateTime(DateTime.Now);

                                        //JFCV agregar retenciones si es una factura de soporte el iva y el impuesto retenido es cero
                                        gastodferencia.sub_amt = diferencia;
                                        gastodferencia.tax_amt = 0;
                                        gastodferencia.ret_amt = 0;
                                        gastodferencia.iva_ret = 0;

                                        gastodferencia.dwn = null; //null
                                        gastodferencia.dwn_dt = null; //null
                                        gastodferencia.vchr_no = null; //null
                                        gastodferencia.mov_type = null; //null
                                        //jfcv 0909 2016 agregar columan de subtipo para enviarselo a macola 
                                        gastodferencia.mov_type = pagoelectronico.Id_PagElecSubTipo.ToString();
                                        if (Convert.ToInt32(gastodferencia.mov_type) == -1)
                                            gastodferencia.mov_type = null;
                                        //fin jfcv 09092016
                                        gastodferencia.bank_name = null;
                                        gastodferencia.bank_account = null;
                                        gastodferencia.SAT_no = "";
                                        gastodferencia.flg_test = false;

                                        //JFCV para el movimiento de diferencia en CXP debo insertar la cuenta del pago electronico 
                                        // y no la cuenta de los comprobantes 
                                        //gastodferencia.account_no = String.Concat(
                                        // CerosDerecha(rgPago.Items[item]["GV_Cc"].Text, 8),
                                        // CerosDerecha(rgPago.Items[item]["GV_SubCuenta"].Text, 8),
                                        // CerosDerecha(rgPago.Items[item]["GV_SubSubCuenta"].Text, 8) 
                                        //);

                                        gastodferencia.account_no = String.Concat(
                                         CerosDerecha(pagoelectronico.PagElec_Cc, 8),
                                         CerosDerecha(pagoelectronico.PagElec_SubCuenta, 8),
                                         CerosDerecha(pagoelectronico.PagElec_SubSubCuenta, 8)
                                        );

                                        //JFCV 22 ago 2016 agregar el mail del usuario 
                                        gastodferencia.mail_usu = session.U_Correo;
                                        lstGastoDiferencia.Add(gastodferencia);

                                        #endregion Enviar si tiene archivo de soporte

                                        string resultadodiferencia = wsMac.insGasto(MacolaKey, lstGastoDiferencia.ToArray());
                                        if (resultadodiferencia != "ok")
                                        {
                                            ////////////JFCV 22dic Transacciones oDBaut.RollBack();
                                            Alerta("No se pudo realizar la autorización." + resultadodiferencia);
                                            return;
                                        }


                                    }

                                    clsGastoViajeAut.AutorizarGastoViaje(gastoViaje, session.Emp_Cnx, ref verificadorins);
                                    if (verificadorins == 1)
                                    {
                                        ////////////JFCV 22dic Transacciones  oDBaut.Commit();
                                    }
                                    else
                                    {
                                        ////////////JFCV 22dic Transacciones oDBaut.RollBack();
                                        Alerta("Ocurrio un error al realizar la autorización.");
                                    }


                                    try
                                    {
                                        PagoElectronico pagoElec2 = new PagoElectronico();
                                        pagoElec2.Id_PagElec = Int32.Parse(rgPago.Items[item]["Id_PagElec"].Text);

                                        EnviarCorreo(pagoElec2, 1);
                                    }
                                    catch (Exception ex)
                                    {
                                        Alerta(ex.Message);
                                    }

                                }
                                else
                                {
                                    ////////////JFCV 22dic Transacciones oDBaut.RollBack();
                                    Alerta("No se pudo realizar la autorización.");
                                }


                                #endregion Autorizar


                            }

                            catch (Exception ex)
                            {
                                ////////////JFCV 22dic Transacciones oDBaut.RollBack();
                                throw ex;
                            }
                            finally
                            {
                                oDBaut.Dispose();
                            }
                        }
                        else  // si la diferencia no es 0 o a favor no hace nada 
                        {
                            Alerta("Se cuenta con Saldo en contra para enviar a autorización debe comprobar el saldo solicitado.");
                        }

                        rgPago.Rebind();

                        break;

                        #endregion Autorizar COmprobante
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgPagoGastoViaje_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                RadGrid rgPago = RadPanelBar1.FindItemByValue("PanelGastoViaje").FindControl("rgPagoGastoViaje") as RadGrid;
                rgPago.Rebind();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        #endregion Autorizacion de Gastos de Viaje

        #region Autorizacion de Comprobación de Acreedores
        protected void rgPagoCompAcreedor_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    RadGrid rgPagoCompAcreedor = RadPanelBar1.FindItemByValue("PanelCompAcreedor").FindControl("rgPagoCompAcreedor") as RadGrid;
                    rgPagoCompAcreedor.DataSource = GetList(2);
                    int contador = (((System.Collections.Generic.List<CapaEntidad.GastoViaje>)(rgPagoCompAcreedor.DataSource))).Count;
                     RadPanelBar1.Items[2].Text = "Autorización de Comp. de Acreedores  -  Solicitudes Pendientes de Autorizar " + contador.ToString();
 
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgPagoCompAcreedor_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GastoViaje gastoViaje = new GastoViaje();

                int item = e.Item.ItemIndex;

                RadGrid rgPagoCompAcreedor = RadPanelBar1.FindItemByValue("PanelCompAcreedor").FindControl("rgPagoCompAcreedor") as RadGrid;
                gastoViaje.Id_GV = Int32.Parse(rgPagoCompAcreedor.Items[item]["Id_GV"].Text);
                gastoViaje.Id_Emp = Int32.Parse(rgPagoCompAcreedor.Items[item]["Id_Emp"].Text);
                gastoViaje.Id_Cd = Int32.Parse(rgPagoCompAcreedor.Items[item]["Id_Cd"].Text);

                if (((RadTextBox)(rgPagoCompAcreedor.Items[item]["Acr_MotivoRechazo"].FindControl("TxtMotivoRechazo"))).Text != string.Empty)
                {
                    gastoViaje.GV_MotivoRechazo = Convert.ToString(((RadTextBox)(rgPagoCompAcreedor.Items[item]["Acr_MotivoRechazo"].FindControl("TxtMotivoRechazo"))).Text);
                }
                //diferencia 
                decimal diferencia = Decimal.Parse(rgPagoCompAcreedor.Items[item]["GV_Saldo_Comprobar"].Text);

                switch (e.CommandName.ToString())
                {
                    case "XML":
                        //descargarXML(pago.Id_PagElec);
                        break;
                    case "PDF":
                        //descargarPDF(pago.Id_PagElec);
                        break;
                    //JFCV 24 feb 2016 agregue la funcionalidad de abrir comprobantes 
                    case "Comprobantes":
                        RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_LstComprobantesGV('", gastoViaje.Id_GV, "' ,'", gastoViaje.Id_Emp, "' ,'", gastoViaje.Id_Cd, "')"));


                        break;
                    //JFCV 18 dic 2015 agregue la funcionalidad de rechazar 
                    case "Delete":
                        #region Rechazar
                        Sesion sessionr = new Sesion();
                        sessionr = (Sesion)Session["Sesion" + Session.SessionID];
                        gastoViaje.UsuarioMod = sessionr.Id_U;

                        if (gastoViaje.GV_MotivoRechazo != "" && gastoViaje.GV_MotivoRechazo != null)
                        {
                            int verificador = -1;

                            CapaDatos.dbAccess oDB = new CapaDatos.dbAccess(sessionr.Emp_Cnx);
                            //using ())
                            //{

                            try
                            {

                                CN_CapGastoViaje clsGastoViaje = new CN_CapGastoViaje();
                                clsGastoViaje.RechazarGastoViaje(gastoViaje, sessionr.Emp_Cnx, ref verificador);

                                if (verificador == 1)
                                {

                                    try
                                    {
                                        PagoElectronico pagoElec = new PagoElectronico();
                                        pagoElec.Id_PagElec = Int32.Parse(rgPagoCompAcreedor.Items[item]["Id_PagElec"].Text);
                                        pagoElec.PagElec_MotivoRechazo = gastoViaje.GV_MotivoRechazo;
                                        EnviarCorreo(pagoElec, 0);
                                    }
                                    catch (Exception ex)
                                    {
                                        this.lblMensaje.Text = "El Gasto de Viaje fue rechazado pero el correo no pudo ser enviado, error: " + ex.Message;
                                    }
                                }
                                else //no paso la validación
                                {
                                    this.lblMensaje.Text = "No se pudo rechazar el Gasto de Viaje ";
                                }


                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                            finally
                            {

                            }

                        }
                        else
                        {
                            this.lblMensaje.Text = "Capture el Motivo de Rechazo.";
                        }
                        #endregion Rechazar
                        break;
                    case "Autorizar":

                        #region Autorizar COmprobante
                        Sesion session = new Sesion();
                        session = (Sesion)Session["Sesion" + Session.SessionID];
                        gastoViaje.UsuarioMod = session.Id_U;


                        int verificadorins = -1;

                        ///JFCV TODO Definir rango de tolerancia 
                        if (diferencia >= -1)  // si la diferencia no es 0 o a favor no hace nada 
                        {


                            CN_CapGastoViaje clsGastoViajeAut = new CN_CapGastoViaje();

                            CapaDatos.dbAccess oDBaut = new CapaDatos.dbAccess(session.Emp_Cnx);

                            try
                            {

                                #region Autorizar

                                CN_CapPagoElectronico clsPago = new CN_CapPagoElectronico();

                                CN_CapGastoViajeComprobante clsGastoViajeComprobante = new CN_CapGastoViajeComprobante();
                                List<GastoViajeComprobante> list = new List<GastoViajeComprobante>();

                                GastoViajeComprobante gastoViajeComprobante = new GastoViajeComprobante();
                                gastoViajeComprobante.Id_Emp = Int32.Parse(rgPagoCompAcreedor.Items[item]["Id_Emp"].Text);
                                gastoViajeComprobante.Id_Cd = Int32.Parse(rgPagoCompAcreedor.Items[item]["Id_Cd"].Text);
                                gastoViajeComprobante.Id_GV = Int32.Parse(rgPagoCompAcreedor.Items[item]["Id_GV"].Text);

                                clsGastoViajeComprobante.ConsultaGastoViajeComprobante(gastoViajeComprobante, session.Emp_Cnx, ref list);

                                PagoElectronico pagoelectronico = new PagoElectronico();
                                pagoelectronico.Id_Emp = Int32.Parse(rgPagoCompAcreedor.Items[item]["Id_Emp"].Text);
                                pagoelectronico.Id_Cd = Int32.Parse(rgPagoCompAcreedor.Items[item]["Id_Cd"].Text);
                                pagoelectronico.Id_GV = Int32.Parse(rgPagoCompAcreedor.Items[item]["Id_GV"].Text);
                                pagoelectronico.Id_PagElec = Int32.Parse(rgPagoCompAcreedor.Items[item]["Id_PagElec"].Text);
                                CN_CapPagoElectronico clsPagoElectronico = new CN_CapPagoElectronico();
                                clsPagoElectronico.ConsultaPagoElectronicoAutorizacion(pagoelectronico, session.Emp_Cnx);

                                wsMacola.wsMacola wsMac = new wsMacola.wsMacola();


                                List<wsMacola.Gasto> lstGasto = new List<wsMacola.Gasto>();
                                List<wsMacola.Gasto> lstGastoDiferencia = new List<wsMacola.Gasto>();


                                #region foreach de comprobantes
                                foreach (GastoViajeComprobante comprobante in list)
                                {
                                    wsMacola.Gasto gasto = new wsMacola.Gasto();


                                    if (comprobante.GVComprobante_ConComprobante)  //Inserto los que tienen comprobante 
                                    {
                                        #region Con Comprobante
                                        gasto.id_trx = Int32.Parse(rgPagoCompAcreedor.Items[item]["Id_PagElec"].Text);
                                        gasto.cus_type_cd = rgPagoCompAcreedor.Items[item]["Id_Cd"].Text;
                                        //JFCV 29 sep 2015 el valor de vend_no truncarlo a 12 caracteres porque esa es la longitud en la tabla 
                                        if (rgPagoCompAcreedor.Items[item]["Acr_NumeroGenerado"].Text.Length > 12)
                                            gasto.vend_no = rgPagoCompAcreedor.Items[item]["Acr_NumeroGenerado"].Text.Substring(0, 12);
                                        else
                                            gasto.vend_no = rgPagoCompAcreedor.Items[item]["Acr_NumeroGenerado"].Text;
                                        //JFCV agregar el numero de referencia en el web service
                                        gasto.no_ref = Convert.ToString(((RadTextBox)(rgPagoCompAcreedor.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text);

                                        DataTable DT = ObtenerDatos(comprobante.GVComprobante_XmlStream.Length > 0 ? comprobante.GVComprobante_XmlStream : null);

                                        gasto.inv_no = string.Concat((string)DT.Rows[0]["serie"], (string)DT.Rows[0]["folio"]);
                                        //15junio2018 que trunque el folio a 14 caracteres porque macola solo acepta eso 
                                        int longitud = gasto.inv_no.Length;
                                        if (longitud > 14)
                                            gasto.inv_no = gasto.inv_no.Substring(longitud - 14, 14);

                                        gasto.amt = Convert.ToDecimal("0" + (string)DT.Rows[0]["importe"]);
                                        gasto.trx_dt = (DateTime)DT.Rows[0]["fecha"];

                                        //JFCV agregar retenciones
                                        gasto.sub_amt = Convert.ToDecimal("0" + (string)DT.Rows[0]["subtotal"]);
                                        gasto.tax_amt = Convert.ToDecimal("0" + (string)DT.Rows[0]["iva"]);
                                        gasto.ret_amt = Convert.ToDecimal("0" + (string)DT.Rows[0]["impuestoretenido"]);
                                        gasto.iva_ret = Convert.ToDecimal("0" + (string)DT.Rows[0]["ivaretenido"]);

                                        gasto.dwn = null; //null
                                        gasto.dwn_dt = null; //null
                                        gasto.vchr_no = null; //null
                                        gasto.mov_type = null; //null
                                        //jfcv 0909 2016 agregar columan de subtipo para enviarselo a macola 
                                        gasto.mov_type = pagoelectronico.Id_PagElecSubTipo.ToString();   //rgPagoCompAcreedor.Items[item]["Id_PagElecSubTipo"].Text;
                                        if (Convert.ToInt32(gasto.mov_type) == -1)
                                            gasto.mov_type = null;
                                        //fin jfcv 09092016

                                        gasto.account_no = String.Concat(
                                            CerosDerecha(comprobante.GVComprobante_GV_Cc, 8),
                                            CerosDerecha(comprobante.GVComprobante_GV_SubCuenta, 8),
                                            CerosDerecha(comprobante.GVComprobante_GV_SubSubCuenta, 8)
                                        );
                                        gasto.SAT_no = (string)DT.Rows[0]["UUID"];
                                        try
                                        {
                                            gasto.flg_test = Convert.ToBoolean(ConfigurationManager.AppSettings["insGastoMacola"].ToString());
                                        }
                                        catch
                                        {
                                            gasto.flg_test = false;
                                        }
                                        //JFCV 22 ago 2016 agregar el mail del usuario 
                                        gasto.mail_usu = session.U_Correo;
                                        lstGasto.Add(gasto);
                                        #endregion con comprobante
                                    }
                                    else
                                    {
                                        #region Enviar si tiene archivo de soporte

                                        gasto.id_trx = Int32.Parse(rgPagoCompAcreedor.Items[item]["Id_PagElec"].Text);
                                        gasto.cus_type_cd = rgPagoCompAcreedor.Items[item]["Id_Cd"].Text;
                                        //JFCV 29 sep 2015 el valor de vend_no truncarlo a 12 caracteres porque esa es la longitud en la tabla 
                                        if (rgPagoCompAcreedor.Items[item]["Acr_NumeroGenerado"].Text.Length > 12)
                                            gasto.vend_no = rgPagoCompAcreedor.Items[item]["Acr_NumeroGenerado"].Text.Substring(0, 12);
                                        else
                                            gasto.vend_no = rgPagoCompAcreedor.Items[item]["Acr_NumeroGenerado"].Text;
                                        //JFCV agregar el numero de referencia en el web service
                                        gasto.no_ref = Convert.ToString(((RadTextBox)(rgPagoCompAcreedor.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text);


                                        gasto.inv_no = "";
                                        gasto.amt = comprobante.GVComprobante_Importe;
                                        gasto.trx_dt = comprobante.GVComprobante_Fecha;

                                        //JFCV agregar retenciones si es una factura de soporte el iva y el impuesto retenido es cero
                                        gasto.sub_amt = comprobante.GVComprobante_Importe;
                                        gasto.tax_amt = 0;
                                        gasto.ret_amt = 0;
                                        gasto.iva_ret = 0;

                                        gasto.dwn = null; //null
                                        gasto.dwn_dt = null; //null
                                        gasto.vchr_no = null; //null
                                        gasto.mov_type = null; //null
                                        //jfcv 0909 2016 agregar columan de subtipo para enviarselo a macola 
                                        gasto.mov_type = pagoelectronico.Id_PagElecSubTipo.ToString();
                                        if (Convert.ToInt32(gasto.mov_type) == -1)
                                            gasto.mov_type = null;
                                        //fin jfcv 09092016

                                        gasto.bank_name = null;
                                        gasto.bank_account = null;
                                        gasto.account_no = String.Concat(
                                           CerosDerecha(comprobante.GVComprobante_GV_Cc, 8),
                                           CerosDerecha(comprobante.GVComprobante_GV_SubCuenta, 8),
                                           CerosDerecha(comprobante.GVComprobante_GV_SubSubCuenta, 8)
                                        );

                                        gasto.SAT_no = "";
                                        try
                                        {
                                            gasto.flg_test = Convert.ToBoolean(ConfigurationManager.AppSettings["insGastoMacola"].ToString());
                                        }
                                        catch
                                        {
                                            gasto.flg_test = false;
                                        }

                                        //JFCV 22 ago 2016 agregar el mail del usuario 
                                        gasto.mail_usu = session.U_Correo;

                                        lstGasto.Add(gasto);


                                        #endregion Enviar si tiene archivo de soporte
                                    }

                                }

                                #endregion foreach de comprobantes

                                #region Enviar a GL el monto original en saldo negativo

                                wsMacola.Gasto gastoGL = new wsMacola.Gasto();
                                gastoGL.id_trx = Int32.Parse(rgPagoCompAcreedor.Items[item]["Id_PagElec"].Text);
                                gastoGL.cus_type_cd = rgPagoCompAcreedor.Items[item]["Id_Cd"].Text;
                                if (rgPagoCompAcreedor.Items[item]["Acr_NumeroGenerado"].Text.Length > 12)
                                    gastoGL.vend_no = rgPagoCompAcreedor.Items[item]["Acr_NumeroGenerado"].Text.Substring(0, 12);
                                else
                                    gastoGL.vend_no = rgPagoCompAcreedor.Items[item]["Acr_NumeroGenerado"].Text;
                                gastoGL.no_ref = Convert.ToString(((RadTextBox)(rgPagoCompAcreedor.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text);
                                gastoGL.inv_no = "";
                                gastoGL.amt = Convert.ToDecimal(rgPagoCompAcreedor.Items[item]["GV_importe"].Text) * -1;
                                gastoGL.trx_dt = Convert.ToDateTime(DateTime.Now);
                                gastoGL.sub_amt = gastoGL.amt;
                                gastoGL.tax_amt = 0;
                                gastoGL.ret_amt = 0;
                                gastoGL.iva_ret = 0;
                                gastoGL.dwn = null; //null
                                gastoGL.dwn_dt = null; //null
                                gastoGL.vchr_no = null; //null
                                gastoGL.mov_type = null; //null
                                //jfcv 0909 2016 agregar columan de subtipo para enviarselo a macola 
                                gastoGL.mov_type = pagoelectronico.Id_PagElecSubTipo.ToString();
                                if (Convert.ToInt32(gastoGL.mov_type) == -1)
                                    gastoGL.mov_type = null;
                                //fin jfcv 09092016

                                gastoGL.bank_name = null;
                                gastoGL.bank_account = null;
                                gastoGL.SAT_no = "";
                                gastoGL.flg_test = false;
                                gastoGL.account_no = String.Concat(
                                 CerosDerecha(pagoelectronico.PagElec_Cc, 8),
                                 CerosDerecha(pagoelectronico.PagElec_SubCuenta, 8),
                                 CerosDerecha(pagoelectronico.PagElec_SubSubCuenta, 8)
                                );
                                //JFCV 22 ago 2016 agregar el mail del usuario 
                                gastoGL.mail_usu = session.U_Correo;
                                lstGasto.Add(gastoGL);
                                #endregion Enviar a GL el monto original en saldo negativo
                                // si la diferencia es  en contra  no inserto nada y mando mensaje 
                                // si la diferencia es a favor inserto y aparte inserto en insGasto la diferencia 
                                decimal montominimocheque = Convert.ToDecimal(ConfigurationManager.AppSettings["montominimocheque"].ToString());
                                if (diferencia > 0 && diferencia >= montominimocheque)  //Si la diferencia es mayor a cero inserto otro movimiento en macola 
                                {
                                    #region Enviar a GL el monto de la diferencia pero en negativo ( contrario a lo que traigo )
                                    wsMacola.Gasto gastoGLDiferencia = new wsMacola.Gasto();
                                    gastoGLDiferencia.id_trx = Int32.Parse(rgPagoCompAcreedor.Items[item]["Id_PagElec"].Text);
                                    gastoGLDiferencia.cus_type_cd = rgPagoCompAcreedor.Items[item]["Id_Cd"].Text;
                                    if (rgPagoCompAcreedor.Items[item]["Acr_NumeroGenerado"].Text.Length > 12)
                                        gastoGLDiferencia.vend_no = rgPagoCompAcreedor.Items[item]["Acr_NumeroGenerado"].Text.Substring(0, 12);
                                    else
                                        gastoGLDiferencia.vend_no = rgPagoCompAcreedor.Items[item]["Acr_NumeroGenerado"].Text;
                                    // JFCV TODO ver de cuanto es el margen para insertar diferencias 
                                    gastoGLDiferencia.no_ref = Convert.ToString(((RadTextBox)(rgPagoCompAcreedor.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text);

                                    gastoGLDiferencia.inv_no = "";
                                    gastoGLDiferencia.amt = diferencia * -1;
                                    gastoGLDiferencia.trx_dt = Convert.ToDateTime(DateTime.Now);

                                    gastoGLDiferencia.sub_amt = gastoGLDiferencia.amt;
                                    gastoGLDiferencia.tax_amt = 0;
                                    gastoGLDiferencia.ret_amt = 0;
                                    gastoGLDiferencia.iva_ret = 0;
                                    gastoGLDiferencia.dwn = null; //null
                                    gastoGLDiferencia.dwn_dt = null; //null
                                    gastoGLDiferencia.vchr_no = null; //null
                                    gastoGLDiferencia.mov_type = null; //null
                                    //jfcv 0909 2016 agregar columan de subtipo para enviarselo a macola 
                                    gastoGLDiferencia.mov_type = pagoelectronico.Id_PagElecSubTipo.ToString();
                                    if (Convert.ToInt32(gastoGLDiferencia.mov_type) == -1)
                                        gastoGLDiferencia.mov_type = null;
                                    //fin jfcv 09092016

                                    gastoGLDiferencia.bank_name = null;
                                    gastoGLDiferencia.bank_account = null;
                                    gastoGLDiferencia.SAT_no = "";
                                    gastoGLDiferencia.flg_test = false;
                                    gastoGLDiferencia.account_no = String.Concat(
                                     CerosDerecha(pagoelectronico.PagElec_Cc, 8),
                                     CerosDerecha(pagoelectronico.PagElec_SubCuenta, 8),
                                     CerosDerecha(pagoelectronico.PagElec_SubSubCuenta, 8)
                                    );
                                    //JFCV 22 ago 2016 agregar el mail del usuario 
                                    gastoGLDiferencia.mail_usu = session.U_Correo;
                                    lstGasto.Add(gastoGLDiferencia);
                                    #endregion Enviar a GL el monto de la diferencia pero en negativo ( contrario a lo que traigo )
                                }
                                ////////////JFCV 22dic Transacciones  oDBaut.BeginTransaction();

                                string resultado = wsMac.insGastoViaje(MacolaKey, lstGasto.ToArray());


                                if (resultado == "ok")
                                {

                                    //JFCV obtengo el monto minimo por el que puedo generar un cheque , 
                                    // si la diferencia es menor a ese monto , entonces no genero el segundo movimiento 
                                    // a la tabla de CXP



                                    if (diferencia > 0 && diferencia >= montominimocheque)  //Si la diferencia es mayor a cero inserto otro movimiento en macola 
                                    {
                                        wsMacola.Gasto gastodferencia = new wsMacola.Gasto();

                                        #region Enviar el gasto en caso de que tenga una diferencia a favor


                                        gastodferencia.id_trx = Int32.Parse(rgPagoCompAcreedor.Items[item]["Id_PagElec"].Text);
                                        gastodferencia.cus_type_cd = rgPagoCompAcreedor.Items[item]["Id_Cd"].Text;
                                        //JFCV 29 sep 2015 el valor de vend_no truncarlo a 12 caracteres porque esa es la longitud en la tabla 
                                        if (rgPagoCompAcreedor.Items[item]["Acr_NumeroGenerado"].Text.Length > 12)
                                            gastodferencia.vend_no = rgPagoCompAcreedor.Items[item]["Acr_NumeroGenerado"].Text.Substring(0, 12);
                                        else
                                            gastodferencia.vend_no = rgPagoCompAcreedor.Items[item]["Acr_NumeroGenerado"].Text;
                                        //JFCV agregar el numero de referencia en el web service
                                        gastodferencia.no_ref = Convert.ToString(((RadTextBox)(rgPagoCompAcreedor.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text);


                                        gastodferencia.inv_no = "";
                                        gastodferencia.amt = diferencia;
                                        // gastodferencia.trx_dt = Convert.ToDateTime(rgPagoCompAcreedor.Items[item]["GV_FechaSalida"].Text);
                                        gastodferencia.trx_dt = Convert.ToDateTime(DateTime.Now);

                                        //JFCV agregar retenciones si es una factura de soporte el iva y el impuesto retenido es cero
                                        gastodferencia.sub_amt = diferencia;
                                        gastodferencia.tax_amt = 0;
                                        gastodferencia.ret_amt = 0;
                                        gastodferencia.iva_ret = 0;

                                        gastodferencia.dwn = null; //null
                                        gastodferencia.dwn_dt = null; //null
                                        gastodferencia.vchr_no = null; //null
                                        gastodferencia.mov_type = null; //null
                                        //jfcv 0909 2016 agregar columan de subtipo para enviarselo a macola 
                                        gastodferencia.mov_type = pagoelectronico.Id_PagElecSubTipo.ToString();
                                        if (Convert.ToInt32(gastodferencia.mov_type) == -1)
                                            gastodferencia.mov_type = null;
                                        //fin jfcv 09092016
                                        gastodferencia.bank_name = null;
                                        gastodferencia.bank_account = null;
                                        gastodferencia.SAT_no = "";
                                        gastodferencia.flg_test = false;

                                        //JFCV para el movimiento de diferencia en CXP debo insertar la cuenta del pago electronico 
                                        // y no la cuenta de los comprobantes 
                                        //gastodferencia.account_no = String.Concat(
                                        // CerosDerecha(rgPagoCompAcreedor.Items[item]["GV_Cc"].Text, 8),
                                        // CerosDerecha(rgPagoCompAcreedor.Items[item]["GV_SubCuenta"].Text, 8),
                                        // CerosDerecha(rgPagoCompAcreedor.Items[item]["GV_SubSubCuenta"].Text, 8) 
                                        //);

                                        gastodferencia.account_no = String.Concat(
                                         CerosDerecha(pagoelectronico.PagElec_Cc, 8),
                                         CerosDerecha(pagoelectronico.PagElec_SubCuenta, 8),
                                         CerosDerecha(pagoelectronico.PagElec_SubSubCuenta, 8)
                                        );

                                        //JFCV 22 ago 2016 agregar el mail del usuario 
                                        gastodferencia.mail_usu = session.U_Correo;
                                        lstGastoDiferencia.Add(gastodferencia);

                                        #endregion Enviar si tiene archivo de soporte

                                        string resultadodiferencia = wsMac.insGasto(MacolaKey, lstGastoDiferencia.ToArray());
                                        if (resultadodiferencia != "ok")
                                        {
                                            ////////////JFCV 22dic Transacciones oDBaut.RollBack();
                                            Alerta("No se pudo realizar la autorización." + resultadodiferencia);
                                            return;
                                        }


                                    }

                                    clsGastoViajeAut.AutorizarGastoViaje(gastoViaje, session.Emp_Cnx, ref verificadorins);
                                    if (verificadorins == 1)
                                    {
                                       ////////////JFCV 22dic Transacciones  oDBaut.Commit();
                                    }
                                    else
                                    {
                                        ////////////JFCV 22dic Transacciones  oDBaut.RollBack();
                                        Alerta("Ocurrio un error al realizar la autorización.");
                                    }


                                    try
                                    {
                                        PagoElectronico pagoElec2 = new PagoElectronico();
                                        pagoElec2.Id_PagElec = Int32.Parse(rgPagoCompAcreedor.Items[item]["Id_PagElec"].Text);

                                        EnviarCorreo(pagoElec2, 1);
                                    }
                                    catch (Exception ex)
                                    {
                                        Alerta(ex.Message);
                                    }

                                }
                                else
                                {
                                    ////////////JFCV 22dic Transacciones oDBaut.RollBack();
                                    Alerta("No se pudo realizar la autorización.");
                                }


                                #endregion Autorizar


                            }

                            catch (Exception ex)
                            {
                                ////////////JFCV 22dic Transacciones oDBaut.RollBack();
                                throw ex;
                            }
                            finally
                            {
                                oDBaut.Dispose();
                            }
                        }
                        else  // si la diferencia no es 0 o a favor no hace nada 
                        {
                            Alerta("Se cuenta con Saldo en contra para enviar a autorización debe comprobar el saldo solicitado.");
                        }

                        rgPagoCompAcreedor.Rebind();

                        break;

                        #endregion Autorizar COmprobante
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgPagoCompAcreedor_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                RadGrid rgPago = RadPanelBar1.FindItemByValue("PanelGastoViaje").FindControl("rgPagoGastoViaje") as RadGrid;
                rgPago.Rebind();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        #endregion

        private List<GastoViaje> GetList(int tipoGasto) // 1 Gtos de viaje 2 es aut comp acreedores  
        {
            try
            {
                CN_CapGastoViaje clsGastoViaje = new CN_CapGastoViaje();
                List<GastoViaje> list = new List<GastoViaje>();

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                GastoViaje gastoViaje = new GastoViaje();
                gastoViaje.Id_Emp = session.Id_Emp;
                if (CmbId_Cd.SelectedIndex == -1)
                {
                    gastoViaje.Id_Cd = session.Id_Cd_Ver;
                }
                else
                {
                    gastoViaje.Id_Cd = Convert.ToInt32(CmbId_Cd.SelectedValue);
                }

                if (txtidPagoElectronico.Text != "")
                {
                    gastoViaje.Id_PagElec = Convert.ToInt32(txtidPagoElectronico.Text);
                }

                gastoViaje.Id_PagElecTipo = Convert.ToInt32(CmbTipo.SelectedValue);
 
                clsGastoViaje.ConsultaGastoViaje(gastoViaje, session.Emp_Cnx, ref list);
                //JFCV 02 feb regresa solo los reg que coincidan con la referencia 1 Gtos de viaje 2 es aut comp acreedores 

                //jfcv 23 junio 2017 si el tipo de gasto es 2 puede traer tanto los 2 como los 4 
                // 2 comprobación acreedores y 4 comproación compras o proveedores 
                if (tipoGasto == 1)
                {
                    return list.Where(x => (x.Id_GVEst == 2) && (tipoGasto == x.GV_TipoGasto)).ToList();

                }
                else
                {
                    return list.Where(x => (x.Id_GVEst == 2) && (x.GV_TipoGasto == 2 || x.GV_TipoGasto == 4)).ToList();
                }

                //return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region Autorización de acreedores

        protected void rgAcreedor_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {

                    RadGrid rgAcreedor = RadPanelBar1.FindItemByValue("PanelAcreedor").FindControl("rgAcreedor") as RadGrid;
                    rgAcreedor.DataSource = GetList();
                    int contador = (((System.Collections.Generic.List<CapaEntidad.Acreedor>)(rgAcreedor.DataSource))).Count;
                    RadPanelBar1.Items[3].Text = "Autorización de Nuevos Acreedores  -  Acreedores Pendientes de Autorizar " + contador.ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgAcreedor_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToString() == "Autorizar")
                {
                    Sesion session = new Sesion();
                    session = (Sesion)Session["Sesion" + Session.SessionID];

                    Acreedor acreedor = new Acreedor();

                    int item = e.Item.ItemIndex;

                    RadGrid rgAcreedor = RadPanelBar1.FindItemByValue("PanelAcreedor").FindControl("rgAcreedor") as RadGrid;

                    if (((RadTextBox)(rgAcreedor.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text != string.Empty)
                    {
                        acreedor.Acr_NumeroGenerado = ((RadTextBox)(rgAcreedor.Items[item]["Acr_NumeroGenerado"].FindControl("TxtNumeroAcreedor"))).Text.Trim().ToUpper();
                        acreedor.Id_Acr = Int32.Parse(rgAcreedor.Items[item]["Id_Acr"].Text);
                        acreedor.Id_Emp = Int32.Parse(rgAcreedor.Items[item]["Id_Emp"].Text);
                        acreedor.Id_Cd = Int32.Parse(rgAcreedor.Items[item]["Id_Cd"].Text);

                        int verificador = -1;

                        CN_CatAcreedor clsAcreedor = new CN_CatAcreedor();

                        clsAcreedor.AutorizarAcreedor(acreedor, session.Emp_Cnx, ref verificador);

                        if (verificador == 1)
                        {
                            rgAcreedor.Rebind();
                        }
                    }
                    else
                    {
                        Alerta("Capture el Número del Acreedor");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgAcreedor_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                RadGrid rgAcreedor = RadPanelBar1.FindItemByValue("PanelAcreedor").FindControl("rgAcreedor") as RadGrid;
                rgAcreedor.Rebind();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private List<Acreedor> GetList()
        {
            try
            {
                CN_CatAcreedor clsAcreedor = new CN_CatAcreedor();
                List<Acreedor> list = new List<Acreedor>();

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                Acreedor acreedor = new Acreedor();
                acreedor.Id_Emp = session.Id_Emp;
                acreedor.Id_Cd = session.Id_Cd_Ver;

                if (CmbId_Cd.SelectedIndex == -1)
                {
                    acreedor.Id_Cd = session.Id_Cd_Ver;
                }
                else
                {
                    acreedor.Id_Cd = Convert.ToInt32(CmbId_Cd.SelectedValue);
                }

                clsAcreedor.ConsultaAcreedor(acreedor, session.Emp_Cnx, ref list);

                return list.Where(x => x.Acr_Autorizado == false).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        #endregion Autorización de acreedores

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            RadPanelItem item = RadPanelBar1.Items[0];
            //RadGrid rgPago = item.Header.FindControl("rgPago") as RadGrid;
            RadGrid rgPago = RadPanelBar1.FindItemByValue("PanelItem1").FindControl("rgPago") as RadGrid;
            rgPago.Rebind();
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
                //throw ex;
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
                    //_PermisoGuardar = Permiso.PGrabar;
                    //_PermisoModificar = Permiso.PModificar;
                    //_PermisoEliminar = Permiso.PEliminar;
                    //_PermisoImprimir = Permiso.PImprimir;

                    //if (Permiso.PGrabar == false)
                    //{
                    //    this.rtb1.Items[6].Visible = false;
                    //}
                    //if (Permiso.PGrabar == false && Permiso.PModificar == false)
                    //{
                    //    this.rtb1.Items[5].Visible = false;
                    //}
                    //this.rtb1.Items[4].Visible = false;
                    ////Eliminar
                    //this.rtb1.Items[3].Visible = false;
                    ////Imprimir
                    //this.rtb1.Items[2].Visible = false;
                    ////Correo
                    //this.rtb1.Items[1].Visible = false;
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                //throw ex;
            }
        }

        private void Inicializar()
        {
            RadPanelItem item = RadPanelBar1.Items[0];
            //RadGrid rgPago = item.Header.FindControl("rgPago") as RadGrid; 
            RadGrid rgPago = RadPanelBar1.FindItemByValue("PanelItem1").FindControl("rgPago") as RadGrid;
            rgPago.Rebind();

            RadGrid rgPagoGastoViaje = RadPanelBar1.FindItemByValue("PanelGastoViaje").FindControl("rgPagoGastoViaje") as RadGrid;
            rgPagoGastoViaje.Rebind();
            RadGrid rgPagoCompAcreedor = RadPanelBar1.FindItemByValue("PanelCompAcreedor").FindControl("rgPagoCompAcreedor") as RadGrid;
            rgPagoCompAcreedor.Rebind();
            RadGrid rgPagoAcreedor = RadPanelBar1.FindItemByValue("PanelAcreedor").FindControl("rgAcreedor") as RadGrid;
            rgPagoAcreedor.Rebind();

        }

        //jfcv 24oct2016  punto 13
        //protected void CargarCtaGastos()
        //{
        //    Sesion Sesion = new Sesion();
        //    Sesion = (Sesion)Session["Sesion" + Session.SessionID];

        //    List<PagoElectronicoCuenta> CtaGastos = new List<PagoElectronicoCuenta>();

        //    (new CN_CatPagoElectronicoCuenta()).ConsultaCuenta(
        //        new PagoElectronicoCuenta() { Id_Emp = Sesion.Id_Emp, Id_Cd = Sesion.Id_Cd },
        //        Sesion.Emp_Cnx,
        //        ref CtaGastos
        //    );

        //    cmdCtaGastos.Items.Clear();
        //    if (CtaGastos.Count > 0)
        //    {
        //        //JFCV 18 noviembre 2016 que se pueda buscar por la subcuenta  punto 5
        //        var datasource = from x in CtaGastos
        //                         select new
        //                         {
        //                             x.Id_Emp,
        //                             x.Id_Cd,
        //                             x.Id_PagElecCuenta,
        //                             x.PagElecCuenta_CC,
        //                             x.PagElecCuenta_CuentaPago,
        //                             x.PagElecCuenta_Descripcion,
        //                             x.PagElecCuenta_Numero,
        //                             x.PagElecCuenta_SubCuenta,
        //                             x.PagElecCuenta_SubSubCuenta,
        //                             DisplayField = String.Format("{0} ({1})", x.PagElecCuenta_Descripcion, x.PagElecCuenta_SubCuenta)
        //                         };

        //        cmdCtaGastos.DataSource = datasource;
        //        cmdCtaGastos.DataValueField = "Id_PagElecCuenta";
        //        cmdCtaGastos.DataTextField = "DisplayField";
        //        //cmdCtaGastos.DataTextField = "PagElecCuenta_Descripcion";
        //        cmdCtaGastos.DataBind();
        //    }
        //}

        protected void CargarAcreedores()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatAcreedor_Combo", ref CmbAcreedor);
        }


        //jfcv 24Oct2016 agregue el id pago ellecrónico en los filtros mejoras 2 punto 13
        private List<PagoElectronico> GetList(int? tipo, int? acreedor, int? cuenta, int? id_pagoelectronico, int? id_PagElecSubTipo)
        {
            try
            {
                CN_CapPagoElectronico clsPago = new CN_CapPagoElectronico();
                List<PagoElectronico> list = new List<PagoElectronico>();

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                PagoElectronico pago = new PagoElectronico();
                pago.Id_Emp = session.Id_Emp;
            
                //jfcv 24Oct2016 agregue el id pago ellecrónico en los filtros mejoras 2 punto 11
                pago.Id_Emp = session.Id_Emp;
                if (CmbId_Cd.SelectedIndex == -1)
                {
                    pago.Id_Cd = session.Id_Cd_Ver;
                }
                else
                {
                    pago.Id_Cd = Convert.ToInt32(CmbId_Cd.SelectedValue);
                }

                pago.Id_Acr_Filtro = acreedor;
                pago.Id_PagElecTipo_Filtro = tipo;
                pago.Id_PagElecCuenta_Filtro = cuenta;

                pago.Id_PagElec = Convert.ToInt32(id_pagoelectronico);
                pago.Id_PagElecSubTipo = Convert.ToInt32(id_PagElecSubTipo);

                clsPago.ConsultaPagoElectronico(pago, session.Emp_Cnx, ref list);
                //JFCV 14 Ene 2016 Autorización que solo muestre los que tienen estatus solicitado , (5) 
                return list.Where(x => x.Id_PagElecEstatus == 5).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void descargarXML(int id_PagElec)
        {
            PagoElectronico pagoElectronico = new PagoElectronico();

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            pagoElectronico.Id_Emp = Sesion.Id_Emp;
            pagoElectronico.Id_Cd = Sesion.Id_Cd_Ver;
            pagoElectronico.Id_PagElec = id_PagElec;

            CN_CapPagoElectronico clsPagoElectronico = new CN_CapPagoElectronico();
            clsPagoElectronico.ConsultaPagoElectronico(pagoElectronico, Sesion.Emp_Cnx);

            string ruta = null;
            System.IO.StreamWriter sw = null;
            //JFCV 15 Oct 2015 en el servidor no se leia el xml , cambiar la ruta 
            //  Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
            //ruta = Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + id_PagElec.ToString() + ".txt";

            //if (File.Exists(ruta))
            //    File.Delete(ruta);
            //if (File.Exists(Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + id_PagElec.ToString() + ".xml"))
            //    File.Delete(Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + id_PagElec.ToString() + ".xml");
            //sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
            //sw.WriteLine(pagoElectronico.PagElec_Xml.ToString());
            //sw.Close();
            //File.Move(ruta, Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + id_PagElec.ToString() + ".xml");
            //RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('Reportes\\archivoXml" + Sesion.Id_U.ToString() + "PagElec", id_PagElec.ToString(), ".xml')"));

            string rutadestino = ConfigurationManager.AppSettings["URLtempPDF"].ToString();
            ruta = Server.MapPath(string.Concat(rutadestino, Sesion.Id_U.ToString() + "PagElec" + id_PagElec.ToString() + ".txt"));

            if (File.Exists(ruta))
                File.Delete(ruta);
            if (File.Exists(rutadestino + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + id_PagElec.ToString() + ".xml"))
                File.Delete(rutadestino + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + id_PagElec.ToString() + ".xml");
            sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
            sw.WriteLine(pagoElectronico.PagElec_Xml.ToString());
            sw.Close();
            File.Move(ruta, rutadestino + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec" + id_PagElec.ToString() + ".xml");
            RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('" + rutadestino + "\\archivoXml" + Sesion.Id_U.ToString() + "PagElec", id_PagElec.ToString(), ".xml')"));

        }

        private void descargarPDF(int id_PagElec)
        {
            PagoElectronico pagoElectronico = new PagoElectronico();

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            pagoElectronico.Id_Emp = Sesion.Id_Emp;
            pagoElectronico.Id_Cd = Sesion.Id_Cd_Ver;
            pagoElectronico.Id_PagElec = id_PagElec;

            CN_CapPagoElectronico clsPagoElectronico = new CN_CapPagoElectronico();
            clsPagoElectronico.ConsultaPagoElectronico(pagoElectronico, Sesion.Emp_Cnx);

            byte[] archivoPdf = pagoElectronico.PagElec_Pdf;

            if (archivoPdf != null)
            {
                if (archivoPdf.Length > 0)
                {
                    string tempPDFname = string.Concat("GASTO_"
                             , Sesion.Id_Emp.ToString()
                             , "_", Sesion.Id_Cd.ToString()
                             , "_", id_PagElec.ToString()
                             , ".pdf");
                    string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                    string WebURLtempPDFGastos = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDFGastos"].ToString(), tempPDFname);
                    this.ByteToTempPDF(URLtempPDF, archivoPdf);
                    // ------------------------------------------------------------------------------------------------
                    // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                    // ------------------------------------------------------------------------------------------------
                    RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", WebURLtempPDFGastos, "')"));
                }
            }
        }

        private void descargarSOPORTE(int id_PagElec)
        {
            PagoElectronico pagoElectronico = new PagoElectronico();

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            pagoElectronico.Id_Emp = Sesion.Id_Emp;
            pagoElectronico.Id_Cd = Sesion.Id_Cd_Ver;
            pagoElectronico.Id_PagElec = id_PagElec;

            CN_CapPagoElectronico clsPagoElectronico = new CN_CapPagoElectronico();
            clsPagoElectronico.ConsultaPagoElectronico(pagoElectronico, Sesion.Emp_Cnx);

            byte[] archivoSoporte = pagoElectronico.PagElec_Soporte;

            if (archivoSoporte != null)
            {
                if (archivoSoporte.Length > 0)
                {
                    string tempPDFname = //pagoElectronico.PagElec_Soporte_Nombre; 
                    string.Concat(
                        "GASTO_Soporte_",
                        Sesion.Id_Emp.ToString(),
                        "_", Sesion.Id_Cd.ToString(),
                        "_", id_PagElec.ToString(),
                        "_", pagoElectronico.PagElec_Soporte_Nombre + ".pdf"
                    );
                    string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                    string WebURLtempPDFGastos = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDFGastos"].ToString(), tempPDFname);
                    this.ByteToTempJPG(URLtempPDF, archivoSoporte);
                    // ------------------------------------------------------------------------------------------------
                    // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                    // ------------------------------------------------------------------------------------------------
                    RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", WebURLtempPDFGastos, "')"));
                }
            }
            else
            {
                Alerta("El registro no tiene un archivo de Soporte.");
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

        private void ByteToTempJPG(string tempPath, byte[] filebytes)
        {
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }

            //System.Drawing.Bitmap myBitmap = new System.Drawing.Bitmap(new MemoryStream(filebytes));

            //myBitmap.Save(tempPath, System.Drawing.Imaging.ImageFormat.Jpeg); 

            FileStream fs = new FileStream(
                tempPath,
                FileMode.CreateNew,
                FileAccess.Write
            );
            fs.Write(filebytes, 0, filebytes.Length);
            fs.Close();
        }

        private void EnviarCorreo(PagoElectronico pagoElectronico, int tipotransaccion)
        {
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];

            try
            {

                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd_Ver;
                configuracion.Id_Emp = session.Id_Emp;
                //TODO JFCV configuración  fija 
                //mientras reviso porque los datos servidor de la cd 100 no funcionan
                configuracion.Id_Cd = pagoElectronico.Id_Cd;

                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);

                Usuario usu = new Usuario();
                CN_CatUsuario cn_catusuario = new CN_CatUsuario();
                usu.Id_Emp = session.Id_Emp;
                usu.Id_Cd = session.Id_Cd_Ver;
                usu.Id_U = session.Id_U;
                string Correo_Usuario = "";
                cn_catusuario.ConsultaCorreoUsuario(usu, session.Emp_Cnx, ref Correo_Usuario);

                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<table>");
                cuerpo_correo.Append("<tr>");
                //cuerpo_correo.Append("<td>Le informamos que su solicitud de gastos ha sido autorizada, pago al acreedor {Acr_Nombre} por el monto de $" + pagoElectronico.PagElec_Importe + "</td>");
                if (tipotransaccion == 1) //es una autorización 
                {
                    cuerpo_correo.Append("<td>Le informamos que su solicitud de gastos ha sido autorizada. </td>");
                    cuerpo_correo.Append("</tr>");
                    cuerpo_correo.Append("<tr>");
                    cuerpo_correo.Append("<td>Número de Solicitud : {id_pagElectronico}</td>");
                    cuerpo_correo.Append("</tr>");
                }
                else
                {
                    cuerpo_correo.Append("<td>Le informamos que su solicitud de gastos ha sido rechazada. </td>");
                    cuerpo_correo.Append("</tr>");
                    cuerpo_correo.Append("<tr>");
                    cuerpo_correo.Append("<td>Número de Solicitud : {id_pagElectronico}</td>");
                    cuerpo_correo.Append("</tr>");
                    cuerpo_correo.Append("<tr>");
                    cuerpo_correo.Append("<td>Motivo Rechazo : {pagElecMotivoRechazo}</td>");
                    cuerpo_correo.Append("</tr>");
                }
                cuerpo_correo.Append("</tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td><br></td>");
                cuerpo_correo.Append("</tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td>Accesar a : <a href='{href}'>Rastreo de Solicitud</a></td>");
                cuerpo_correo.Append("</tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td>&nbsp;</td>");
                cuerpo_correo.Append("</tr>");
                cuerpo_correo.Append("</table>");

               

               // string strUrl = (HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/").Replace("//", "/").Replace("http:/", "http://");
                string strUrl = "http://189.206.126.67/sianweb/";
                if (configuracion.RutaSistemaGastos != "" && configuracion.RutaSistemaGastos != null)
                {
                    strUrl = configuracion.RutaSistemaGastos + "/";
                }

                string txtCuerpoMail = cuerpo_correo.ToString();
                txtCuerpoMail = txtCuerpoMail.Replace("{pagElecMotivoRechazo}", pagoElectronico.PagElec_MotivoRechazo);
                txtCuerpoMail = txtCuerpoMail.Replace("{id_pagElectronico}", pagoElectronico.Id_PagElec.ToString());
                if (tipotransaccion == 1) //es una autorización 
                {
                    txtCuerpoMail = txtCuerpoMail.Replace("{href}", strUrl + "RepPagoElectronico.aspx");
                }
                else
                {   // cuando es un rechazo
                    txtCuerpoMail = txtCuerpoMail.Replace("{href}", strUrl + "CapPagosElectronicos_Admin.aspx");
                }

                SmtpClient smtp = new SmtpClient();
                //smtp.EnableSsl = true;
                smtp.Host = configuracion.Mail_Servidor;
                smtp.Port = Convert.ToInt32(configuracion.Mail_Puerto);
                smtp.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                MailAddress from = new MailAddress(configuracion.Mail_Remitente);
                MailMessage mail = new MailMessage();
                mail.From = from;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = System.Text.Encoding.UTF8;

                if (tipotransaccion == 1) //es una autorización 
                {
                    mail.Subject = "(SIANWEB) Notificación de Gasto Autorizado.";
                }
                else
                {   // cuando es un rechazo
                    mail.Subject = "(SIANWEB) Notificación de Gasto Rechazado.";
                }
                if (Correo_Usuario != "")
                    mail.To.Add(new MailAddress(Correo_Usuario));
                if (configuracion.Mail_GastosAvisoGerente != "" && configuracion.Mail_GastosAvisoGerente != null)
                {
                    mail.To.Add(new MailAddress(configuracion.Mail_GastosAvisoGerente));
                }
                if (configuracion.Mail_GastosAvisoUsuario != "" && configuracion.Mail_GastosAvisoUsuario != null)
                {
                    mail.To.Add(new MailAddress(configuracion.Mail_GastosAvisoUsuario));
                }

               

                mail.Body = txtCuerpoMail;
                smtp.Send(mail);


            }
            catch (Exception ex)
            {
                Alerta("El correo no pudo ser enviado. Error: " + ex.Message);
            }

        }

        private void EnviarCorreoError(string mensaje, string pantalla_evento)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd_Ver;
                configuracion.Id_Emp = session.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);

                Usuario usu = new Usuario();
                CN_CatUsuario cn_catusuario = new CN_CatUsuario();
                usu.Id_Emp = session.Id_Emp;
                usu.Id_Cd = session.Id_Cd_Ver;
                usu.Id_U = session.Id_U;
                string Correo_Usuario = "";
                cn_catusuario.ConsultaCorreoUsuario(usu, session.Emp_Cnx, ref Correo_Usuario);


                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<table>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td>{mensajeerror}  usuario {Acr_Nombre} en: " + pantalla_evento + " </td>");
                // cuerpo_correo.Append("<td>{PagElec_Solicitante} solicita el pago al acreedor {Acr_Nombre} por el monto de $" + pagoElectronico.PagElec_Importe + "</td>");
                cuerpo_correo.Append("</tr>");
                cuerpo_correo.Append("<tr>");
                //cuerpo_correo.Append("<td>Cuenta : {pagElecCuenta_Descripcion}</td>");
                cuerpo_correo.Append("</tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td><br></td>");
                cuerpo_correo.Append("</tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td>Accesar a : <a href='{href}'>Autorización de Gastos</a></td>");
                cuerpo_correo.Append("</tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td>&nbsp;</td>");
                cuerpo_correo.Append("</tr>");
                cuerpo_correo.Append("</table>");

                //string strUrl = (HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/").Replace("//", "/").Replace("http:/", "http://");
                string txtCuerpoMail = cuerpo_correo.ToString();
                txtCuerpoMail = txtCuerpoMail.Replace("{mensajeerror}", mensaje);
                txtCuerpoMail = txtCuerpoMail.Replace("{Acr_Nombre}", session.Emp_Nombre);

                string strUrl = "http://40.84.229.61/siancentral/";
                if (configuracion.RutaSistemaGastos != "" && configuracion.RutaSistemaGastos != null)
                {
                    strUrl = configuracion.RutaSistemaGastos + "/";
                }

                // txtCuerpoMail = txtCuerpoMail.Replace("{PagElec_Solicitante}", pagoElectronico.PagElec_Solicitante);
                txtCuerpoMail = txtCuerpoMail.Replace("{href}", "ProAutorizacion_PagoElectronico.aspx");
               ///// txtCuerpoMail = txtCuerpoMail.Replace("{href}", strUrl + "ProAutorizacion_PagoElectronico.aspx");


                SmtpClient smtp = new SmtpClient();
                //smtp.EnableSsl = true;
                smtp.Host = configuracion.Mail_Servidor;
                smtp.Port = Convert.ToInt32(configuracion.Mail_Puerto);
                smtp.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);

                MailAddress from = new MailAddress(configuracion.Mail_Remitente);
                MailMessage mail = new MailMessage();

                mail.From = from;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.Subject = "Error Generado en gastos ";
                mail.To.Add(new MailAddress("francisco.cepeda@gibraltar.com.mx"));
                if (Correo_Usuario != "")
                    mail.To.Add(new MailAddress(Correo_Usuario));

                mail.Body = txtCuerpoMail;
                smtp.Send(mail);

            }
            catch (Exception ex)
            {
                Alerta("El correo de Error no pudo ser enviado." + mensaje + " " + ex.Message);
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

        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "</br></br>', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }

        private void CargarCDI()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Emp_Cnx, "SpCatCdi_Combo", ref CmbId_Cd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //JFCV corre proceso de importación de Pagos electrónicos y Gastos de viaje
        protected void imgBoton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

              

                Sesion sessionr = new Sesion();
                sessionr = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapPagoElectronico clspagoelectronico = new CN_CapPagoElectronico();
                int verificador = 0;

                clspagoelectronico.ImportarPagoElectronico(sessionr.Emp_Cnx, ref verificador);


                CN_CatAcreedor clscatacreedor = new CN_CatAcreedor();
                
                clscatacreedor.ImportarAcreedor(sessionr.Emp_Cnx, sessionr.Id_Emp, ref verificador);

               

                BtnBuscar_Click(sender, e);
                  
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        


    }
}
