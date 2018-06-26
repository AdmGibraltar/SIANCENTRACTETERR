using System;
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
using Telerik.Reporting.Processing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.html;
using System.Configuration;

 


namespace SIANWEB
{
    public partial class RepComisionesGeneraPDF : System.Web.UI.Page
    {
        #region Variables

        private int id_Sistema = 0;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

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
                        CargarComboEmpresas();
                        this.CargarCentros();
                        this.CargarCDI();
                       

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_Cve.Value = randObj.Next().ToString();
                        CargarAnoMes();
                        CargarRepresentantes();
                        this.TblEncabezado.Visible = false;
                        Session["Table"] = null;
                        //JFCV
                        rgRiks.Visible = false;
                        rgRiksPrevio.Visible = false;
                        
  
                    }

                    id_Sistema = Int32.Parse(Request.QueryString["Id"] == null ? "-1" : Request.QueryString["Id"]);
                    rgRiksPrevio.DataSource = Session["Table"]  ;
                    id_Sistema = 19;
                    Label2.Visible = false;
                    TxtId_Rik.Visible = false;
                    TxtTipo_Representante.Visible = false;
                    CmbTipo_Representante.Visible = false;
                    cmbRik.Visible = false;
                  
                    TxtId_Rik.Text = "";
                    TxtTipo_Representante.Text = "3";
                    


                }

            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }
        #region Eventos
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);


            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
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


                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                String filename = "";
                String stylesheet = "";
                string destino = "";
                string pdfdestino = "";
                int tiporeporte = 0;

                if (int.Parse(this.cmbanio.SelectedValue) > DateTime.Now.Year)
                {
                    Alerta("El año seleccionado no puede ser mayor al año actual");
                    return;
                }
                else if (int.Parse(this.cmbanio.SelectedValue) == DateTime.Now.Year && int.Parse(this.cmbmes.SelectedValue) > DateTime.Now.Month)
                {
                    Alerta("El periodo seleccionado no puede ser mayor al periodo actual");
                    return;

                }


             

               
   
                        ErrorManager();
                        id_Sistema = 19; //franquicias 
                        if (Convert.ToInt32(this.cmbEmpresa.SelectedValue) != 3)
                        {
                            id_Sistema = 18;
                        }


                        rgRiks.DataSource = GetList();
                        rgRiks.Rebind();

                        #region

                      


                        foreach (GridDataItem item in rgRiks.Items)//loops through each grid row
                        {

                                
                                int rik_seleccionado = Convert.ToInt32(item["Id"].Text);
                                int cdi_seleccionado = Convert.ToInt32(item["Id_Cd"].Text);
                                string rikNombre_seleccionado = (item["Nombre_Empleado"].Text);
                                string cdi_nombreseleccionado = (item["CdiNombre"].Text);
                                int tipoRik_seleccionado = Convert.ToInt32(item["id_tiporepresentante"].Text);
                                string rikseleccionado = rik_seleccionado.ToString();

                                if (rik_seleccionado != -1)
                                {

                                    destino = "rep" + cdi_seleccionado + rikseleccionado + "_" + this.cmbmes.SelectedValue + cmbanio.Text + id_Sistema.ToString() + ".html";

                                    //voy a generar diferente nombre para las francquicias , jfcv 6 ago 2017 
                                    //pdfdestino = "rep" + cdi_seleccionado + TxtId_Rik.Text + "_" + this.cmbmes.SelectedValue + cmbanio.Text + id_Sistema.ToString() + ".pdf";
                                    string mes = this.cmbmes.SelectedValue;
                                    mes = mes.PadLeft(2, '0');
                                    String Ceros = "000";
                                    String CerosA = "";
                                    try
                                    {
                                        if (rikseleccionado.Trim().Length <= 3)
                                        {
                                            CerosA = Ceros.ToString().Substring(1, 3 - rikseleccionado.Trim().Length);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                                    }

                                    pdfdestino = cdi_seleccionado + CerosA + rikseleccionado.Trim() + "_" + mes + cmbanio.Text + ".pdf";

                                    tiporeporte = 0;

                                    stylesheet = "xsl_html_reporteComisiones.xslt";

                                    filename = "xsl_html_Comisiones_Origen" + cmbanio.Text + cmbmes.Text + rikseleccionado + ".xml";
                                    //Si el tipo rep no es rik uso xslt alterno 08Feb2018
                                    if (tipoRik_seleccionado != 3)
                                    {
                                        stylesheet = "xsl_html_repComisionesRepresentante.xslt";
                                    }

                                    if (cmbTipoReporte.SelectedIndex == 1)
                                    {
                                        filename = "xsl_html_VentaIncremental_origen.xml";
                                        stylesheet = "xsl_Html_ReporteVentaIncremental.xslt";
                                        tiporeporte = 1;
                                    }

                                    if (!System.IO.File.Exists(Server.MapPath("~/Reportes") + "\\" + filename))
                                    {
                                    }
                                    if (!System.IO.File.Exists(Server.MapPath("~/Reportes") + "\\" + stylesheet))
                                    {
                                    }

                                    filename = Server.MapPath("~/Reportes") + "\\" + filename;
                                    stylesheet = Server.MapPath("~/Reportes") + "\\" + stylesheet;

                                    destino = Server.MapPath("~/Reportes") + "\\" + destino;



                                    SistemaCompensacion sistemacompensacion = new SistemaCompensacion();

                                    sistemacompensacion.Id_Emp = Sesion.Id_Emp;
                                    sistemacompensacion.Id_Cd = Sesion.Id_Cd_Ver;
                                    sistemacompensacion.Id_Sistema = id_Sistema;

                                    if (Convert.ToInt32(this.cmbEmpresa.SelectedValue) != 3)
                                    {
                                        if (tipoRik_seleccionado == 3)
                                        {
                                            id_Sistema = 18;
                                        }
                                        else
                                        {
                                            id_Sistema = 23;
                                        }
                                    }



                                    CN_CatCompensacion clsCapPagoElectronico = new CN_CatCompensacion();
                                    clsCapPagoElectronico.ConsultaConfiguracionSistemacompensacion(sistemacompensacion, Sesion.Emp_Cnx);

                                    string impEdoConsolidado = "";
                                    impEdoConsolidado = sistemacompensacion.ImpEdoConsolidado;

                                    SistemaCompensacionGetXML sistemaCompensacionGetXML = new SistemaCompensacionGetXML();

                                    sistemaCompensacionGetXML.Id_Emp = Sesion.Id_Emp;
                                    sistemaCompensacionGetXML.Id_Cd = Convert.ToInt32(cdi_seleccionado);
                                    sistemaCompensacionGetXML.Id_Sistema = id_Sistema;
                                    sistemaCompensacionGetXML.Anio = Convert.ToInt32(this.cmbanio.SelectedValue);
                                    sistemaCompensacionGetXML.Mes = Convert.ToInt32(this.cmbmes.SelectedValue);
                                    sistemaCompensacionGetXML.Id_Rik = Convert.ToInt32(rikseleccionado);
                                    sistemaCompensacionGetXML.MesTexto = this.cmbmes.Text;
                                    sistemaCompensacionGetXML.RikNombre = rikNombre_seleccionado;
                                    sistemaCompensacionGetXML.Id_Representante = Convert.ToInt32(rikseleccionado);
                                    sistemaCompensacionGetXML.Id_TipoRepresentante = tipoRik_seleccionado;
                                    //sistemaCompensacionGetXML.Id_TipoRepresentante = Convert.ToInt32(this.CmbTipo_Representante.SelectedValue);

                                     
                                    //if (tipoRik_seleccionado != 3)
                                    //{ //jfcv 2 mzo lo puse para que solo procese riks porque todavia no definen bien como quedaría 
                                    //// el de representantes 
                                    clsCapPagoElectronico.ReporteComisionesGetXML(sistemaCompensacionGetXML, Sesion.Emp_Cnx);

                                    if (sistemaCompensacionGetXML.Clientes != null)
                                        {
                                                                                                                                #region si trae datos

                                        try
                                        {
                                            string pclientesXML = "";
                                            pclientesXML = sistemaCompensacionGetXML.Parametros + sistemaCompensacionGetXML.Clientes + "<ConceptosPredefinidos>" + sistemaCompensacionGetXML.Conceptos + @"</ConceptosPredefinidos>";

                                            pclientesXML = pclientesXML.Replace("{1}", rik_seleccionado + " " + rikNombre_seleccionado);
                                            pclientesXML = pclientesXML.Replace("{2}", cdi_seleccionado + " " + cdi_nombreseleccionado);
                                            pclientesXML = pclientesXML.Replace("{5}", this.cmbmes.Text + ' ' + this.cmbanio.Text);

                                            Convertir_XML_XSL convertir_datos = new Convertir_XML_XSL(filename, stylesheet, impEdoConsolidado, pclientesXML, destino, tiporeporte);
                                            convertir_datos.convertir_html();

                                            // RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('Reportes\\" + "rep" + this.CmbId_Cd.SelectedValue.ToString() + TxtId_Rik.Text + "_" + this.cmbmes.SelectedValue + cmbanio.Text + id_Sistema.ToString() + ".html" + "')"));


                                            rikseleccionado = "";

                                            FileStream fsHTMLDocument = new FileStream(destino, FileMode.Open, FileAccess.Read);


                                            pdfWithCSS(fsHTMLDocument, pdfdestino);

                                            item["Estatus"].Text = "0";
                                        }
                                        catch (Exception ex)
                                        {
                                            ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                                        }

                                        //FINAL DE LA VERSION 
                                        #endregion si   trae datos
                                        }  //fin del if 

                                    //}
                                    //JFCV si genero el archivo que lo elimine
                                    #region Elimina archivos paso
                                    if (System.IO.File.Exists(filename))
                                    {
                                        System.IO.File.Delete(filename);
                                    }

                                    if (System.IO.File.Exists(destino))
                                    {
                                       System.IO.File.Delete(destino);
                                    }
                                     #endregion Elimina archivos paso 
                              
                                }  // si no esta checado no hago nada 

                        } //ciclo for

                        #endregion
                     
 
                   
                    //rgRiks.Visible = true;
                    rgRiksPrevio.Visible = false;
                    Session["Table"] = null;
                    rgRiksPrevio.DataSource = Session["Table"];
                    Alerta("Proceso de generación de PDF's terminado con éxito");
                    

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        public void pdfWithCSS(FileStream fsHTMLDocument, string pdfFileName)
        {


            try
            {
                    string strHtml = string.Empty;
                    Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream( Server.MapPath("~/Reportes") + "\\" + pdfFileName, FileMode.Create));
                    HtmlPipelineContext htmlContext = new HtmlPipelineContext(null);

 

                    htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());

                    //create a cssresolver to apply css
                    ICSSResolver cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false);

                    cssResolver.AddCss("div{color: red;}", true);
                    ////////cssResolver.AddCss("h1{color: green;}", true);
                    ////////cssResolver.AddCss("table{empty-cells: show; border-spacing: 0px; margin: 0px; padding: 0px;}", true);

                    //Create and attach pipline, without pipline parser will not work on css
                    IPipeline pipeline = new CssResolverPipeline(cssResolver, new HtmlPipeline(htmlContext, new PdfWriterPipeline(doc, writer)));

                    //Create XMLWorker and attach a parser to it
                    XMLWorker worker = new XMLWorker(pipeline, true);
                    XMLParser xmlParser = new XMLParser(worker);

                    //All is well open documnet and start writing.
                    doc.Open();

                    iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(Server.MapPath("~/images") + "\\" + "logokey.png");

                    pdfImage.ScaleToFit(100, 50);

                    pdfImage.Alignment = iTextSharp.text.Image.UNDERLYING; pdfImage.SetAbsolutePosition(30, 745);

                    doc.Add(pdfImage);

                    iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);

                   // FileStream fsHTMLDocument = new FileStream( Server.MapPath("~/Reportes") + "\\" +  htmlFileName , FileMode.Open, FileAccess.Read);
    
                    StreamReader srHTMLDocument = new StreamReader(fsHTMLDocument);
                    strHtml = srHTMLDocument.ReadToEnd();
                    srHTMLDocument.Close();
                    //en la version 5.5.1 ya no ocupe estas lineas 
                    ////////strHtml = strHtml.Replace("\r\n", "");
                    ////////strHtml = strHtml.Replace("\0", "");
      
                    xmlParser.Parse(new StringReader(strHtml));

                    //Done! close the documnet
                    doc.Close();

             }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }

        protected void CmbId_Cd_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CN__Comun cn_comun = new CN__Comun();
            //JFCV todo como cargo los de que sucursal? cn_comun.LlenaCombo(2, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatRik_ComboTodos", ref cmbRik);
            if (CmbId_Cd.SelectedIndex == -1)
            {
                cn_comun.LlenaCombo(2, 100,
                    Convert.ToInt32(CmbTipo_Representante.SelectedValue), sesion.Emp_Cnx, "spCatRik_ComboTodosRepresentante", ref cmbRik);
            }
            else
            {
                cn_comun.LlenaCombo(2, Convert.ToInt32(CmbId_Cd.SelectedValue), Convert.ToInt32(CmbTipo_Representante.SelectedValue), sesion.Emp_Cnx, "spCatRik_ComboTodosRepresentante", ref cmbRik);

            }
            TxtId_Rik.Text = "";
   
                rgRiks.Visible = false;
                rgRiksPrevio.Visible = false;
                RadAjaxLoadingPanel1.Visible = true;
  
        }

        protected void CmbTipo_Representante_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CN__Comun cn_comun = new CN__Comun();

            //JFCV cargo el tipo de representantes de acuerdo a como lo hayan elegido Asesor, Rik o RSC
            // 1 RSC
            // 2 ASESOR 
            // 3 RIK


            if (CmbId_Cd.SelectedIndex == -1)
            {
                cn_comun.LlenaCombo(2, 100, Convert.ToInt32(CmbTipo_Representante.SelectedValue), sesion.Emp_Cnx, "spCatRik_ComboTodosRepresentante", ref cmbRik);
            }
            else
            {
                cn_comun.LlenaCombo(2, Convert.ToInt32(CmbId_Cd.SelectedValue), Convert.ToInt32(CmbTipo_Representante.SelectedValue), sesion.Emp_Cnx, "spCatRik_ComboTodosRepresentante", ref cmbRik);

            }
            TxtId_Rik.Text = "";

        }

        protected void CmbEmpresa_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)

        //protected void CmbEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

            try
            {

                if (cmbEmpresa.SelectedValue == "3")
                {
                    CN_Comun.LlenaCombo(3, Sesion.Emp_Cnx, "SpCatCdi_Combo", ref CmbId_Cd);
                
                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Emp_Cnx, "SpCatCdi_Combo", ref CmbId_Cd);

                }
                CN_Comun.LlenaCombo(2, 100, Convert.ToInt32(CmbTipo_Representante.SelectedValue), sesion.Emp_Cnx, "spCatRik_ComboTodosRepresentante", ref cmbRik);
                //TxtId_Rik.Text = "";
                //TxtTipo_Representante.Text = "";
   
    
            }
            catch (Exception ex)
            {

                throw ex;
            }

           

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
                        CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                        this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            private void CargarAnoMes()
            {
                try
                {
                    this.cmbanio.SelectedValue = DateTime.Now.Year.ToString();
                    this.cmbmes.SelectedValue = (DateTime.Now.Month - 1).ToString();

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            private void CargarRepresentantes()
            {
                try
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                    CN__Comun cn_comun = new CN__Comun();
                    //JFCV todo como cargo los de que sucursal? cn_comun.LlenaCombo(2, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatRik_ComboTodos", ref cmbRik);
                    if (CmbId_Cd.SelectedIndex == -1)
                    {
                        cn_comun.LlenaCombo(2, 100, Convert.ToInt32(CmbTipo_Representante.SelectedValue), sesion.Emp_Cnx, "spCatRik_ComboTodosRepresentante", ref cmbRik);
                    }
                    else
                    {
                        cn_comun.LlenaCombo(2, Convert.ToInt32(CmbId_Cd.SelectedValue), Convert.ToInt32(CmbTipo_Representante.SelectedValue), sesion.Emp_Cnx, "spCatRik_ComboTodosRepresentante", ref cmbRik);

                    }

                    if (sesion.Id_TU == 2)
                    {
                        this.trRik.Visible = false;

                    }

                }
                catch (Exception ex)
                {

                    throw ex;
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
            private void CargarComboEmpresas()
            {
                //JFCV 14sep que no cargue las empresas de la BD , mejor qu elas cargue desde la forma 
                // porque agregue la sFranquicias 
                //Sesion Sesion = new Sesion();
                //Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                //CN_Comun.LlenaCombo(1, Sesion.Emp_Cnx, "spCatEmpresaCombo", ref cmbEmpresa);
            }
       
       
        #endregion Funciones

        #region Eventos Grid Riks 
            protected void rgRiks_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
            {
                try
                {
                    if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    {
                        //rgRiks.DataSource = GetList();
                        //int? tipo;
                        //int? cuenta;
                        //int? acreedor;
                        //int? estatus;
                        ////jfcv 20Oct2016 agregue el id pago ellecrónico en los filtros mejoras 2 punto 11
                        //int? id_pagoElectronico;

                        //if (CmbTipo.SelectedValue == "")
                        //{
                        //    tipo = null;
                        //}
                        //else
                        //{
                        //    tipo = Int32.Parse(CmbTipo.SelectedValue);
                        //}

                        rgRiks.DataSource = GetList();
                    }
                }
                catch (Exception ex)
                {
                    ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                }
            }

            protected void rgRiks_ItemCommand(object source, GridCommandEventArgs e)
            {
                try
                {
                    Int32 item = default(Int32);
                    item = e.Item.ItemIndex;
                    int Id = 0;
                    int estatus = 0;
                    int id_cd = 0;

                    if (item != -1)
                    {
                          Id = Convert.ToInt32(rgRiks.Items[item]["Id"].Text);
                          //estatus = Convert.ToInt32(rgRiks.Items[item]["Estatus"].Text);
                          id_cd = Convert.ToInt32(rgRiks.Items[item]["Id_Cd"].Text);
                    }
            

                    //JFCV 17 dic 2015 , que solo se puedan editar y cancelar Solicitudes que no estén autorizadas.

                    switch (e.CommandName.ToString())
                    {
                        case "XML":
                            //descargarXML(Id_PagElec);
                            break;
                        case "PDF":
                           // descargarPDF(Id_PagElec);

                            //rep150934_3201714.pdf
                            //voy a generar diferente nombre para las francquicias , jfcv 6 ago 2017 
                            //pdfdestino = "rep" + cdi_seleccionado + TxtId_Rik.Text + "_" + this.cmbmes.SelectedValue + cmbanio.Text + id_Sistema.ToString() + ".pdf";
                            string mes = this.cmbmes.SelectedValue;
                                    mes = mes.PadLeft(2, '0');
                                    String Ceros = "000";
                                    String CerosA = "";
                                    try
                                    {
                                        if (Id.ToString().Trim().Length <= 3)
                                        {
                                            CerosA = Ceros.ToString().Substring(1, 3 - Id.ToString().Trim().Length);
                                        }
                                        //cmbmes.SelectedValue.ToString()
                                    }
                                    catch (Exception ex)
                                    {
                                        ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                                    } 
                                    

                            //RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('Reportes\\" + "rep" + id_cd.ToString() + Id.ToString() + "_" + this.cmbmes.SelectedValue + cmbanio.Text + id_Sistema.ToString() + ".pdf" + "')"));
                                    RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('Reportes\\" + id_cd.ToString() + CerosA + Id.ToString() + "_" + mes + cmbanio.Text + ".pdf" + "')"));

                                // if (archivoPdf != null)
                                //{
                                //    if (archivoPdf.Length > 0)
                                //    {
                                //        string tempPDFname = string.Concat("GASTO_"
                                //                 , Sesion.Id_Emp.ToString()
                                //                 , "_", Sesion.Id_Cd.ToString()
                                //                 , "_", id_PagElec.ToString()
                                //                 , ".pdf");
                                //        string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                                //        string WebURLtempPDFGastos = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDFGastos"].ToString(), tempPDFname);



                            break;
                        case "Soporte":
                            
                             string mes2 = this.cmbmes.SelectedValue;
                            mes2 = mes2.PadLeft(2, '0');
                            //  RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('Reportes\\" + "rep" + id_cd.ToString() + Id.ToString() + "_" + this.cmbmes.SelectedValue + cmbanio.Text + id_Sistema.ToString() + ".pdf" + "')"));
                            RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('Reportes\\" + id_cd.ToString() + Id.ToString() + "_" + mes2 + cmbanio.Text + ".pdf" + "')"));


                          

                            break;
                        case "Comprobantes":

                                    mes = this.cmbmes.SelectedValue;
                                    mes = mes.PadLeft(2, '0');
                                    Ceros = "000";
                                    CerosA = "";
                                    if (Id.ToString().Trim().Length <= 3)
                                    {
                                        CerosA = Ceros.ToString().Substring(1, 3 - Id.ToString().Trim().Length);
                                    }
                          

                            //RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_LstComprobantes('", Id, "')"));
                            //RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('Reportes\\" + "rep" + id_cd.ToString() + Id.ToString() + "_" + this.cmbmes.SelectedValue + cmbanio.Text + id_Sistema.ToString() + ".pdf" + "')"));
                            RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('Reportes\\" + id_cd.ToString() + CerosA + Id.ToString() + "_" + mes + cmbanio.Text + ".pdf" + "')"));

                            break;
                        case "Delete":
                            if (estatus == 2 || estatus == 5 || estatus == 4)
                            {
                                Alerta("Solo se pueden Cancelar Solicitudes que estén en estatus creado o rechazado.");
                            }
                            else
                            {
                                //Cancelar(Id_PagElec);
                            }
                            break;
                        case "Modificar":
                            if (estatus == 2 || estatus == 5 || estatus == 4)
                            {
                                Alerta("Solo se pueden Editar Solicitudes que estén en estatus creado o rechazado.");
                            }
                            else
                            {
                                RAM1.ResponseScripts.Add("return AbrirVentana_GastosModificar(" + Id.ToString() + ")");
                            }
                            break;

                    }

                }
                catch (Exception ex)
                {
                    ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                }
            }

            protected void rgRiks_ItemDataBound(object sender, GridItemEventArgs e)
            {
                if (e.Item is GridDataItem)
                {
                    #region
                    ImageButton imgBtnSoporte = ((ImageButton)((Telerik.Web.UI.GridDataItem)(e.Item)).FindControl("imgSoporte"));
                    ImageButton imgBtnComprobantes = ((ImageButton)((Telerik.Web.UI.GridDataItem)(e.Item)).FindControl("imgComprobantes"));


                    GridDataItem dataItem = (GridDataItem)e.Item;

                    //if (Convert.ToInt32(dataItem["Id_PagElecTipo"].Text) == 2)
                    //{
                    //}



                    int Id = Convert.ToInt32(dataItem["Id"].Text);
                    int id_cd = Convert.ToInt32(dataItem["Id_Cd"].Text);


                    //JFCV 17 dic 2015 , que solo se puedan editar y cancelar Solicitudes que no estén autorizadas.
                    string mes2 = this.cmbmes.SelectedValue;
                    mes2 = mes2.PadLeft(2, '0');
        
                    String Ceros = "000";
                    String CerosA = "";
                    try
                    {

                        if (Id.ToString().Trim().Length <= 3)
                        {
                            CerosA = Ceros.ToString().Substring(1, 3 - Id.ToString().Trim().Length);
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                    }
                   

                    //string filename = "rep" + id_cd.ToString() + Id.ToString() + "_" + this.cmbmes.SelectedValue + cmbanio.Text + id_Sistema.ToString() + ".pdf";
                    string filename =  id_cd.ToString() + CerosA + Id.ToString() + "_" + mes2 + cmbanio.Text   + ".pdf";

                    if (System.IO.File.Exists(Server.MapPath("~/Reportes") + "\\" + filename))
                    {

                        imgBtnComprobantes.Visible = true;
                        imgBtnComprobantes.Enabled = true;
                        imgBtnComprobantes.Attributes["class"] = "edit";
                        imgBtnComprobantes.Attributes["title"] = "Comprobantes PDF y XML";

                    }
                    else
                    {
                        imgBtnComprobantes.Visible = false;
                        imgBtnComprobantes.Enabled = false;
                        imgBtnComprobantes.Attributes["class"] = "edit";
                        imgBtnComprobantes.Attributes["title"] = "Comprobantes PDF y XML";


                    }


                    //JFCV todo  buscar si tengo el pdf entonces lo habilito si no lo desabilito
                    //if (((PagoElectronico)(e.Item.DataItem)).PagElec_Soporte != null)
                    //{
                    //    imgBtnSoporte.Enabled = true;
                    //    imgBtnSoporte.Attributes["class"] = "edit";
                    //    imgBtnSoporte.Attributes["title"] = "Archivo de Soporte";
                    //}
                    //else
                    //{
                    //    //lo desabilito porque ahora siempre debe tener comprobantes 
                    //    //imgBtnSoporte.Enabled = false;
                    //    //imgBtnSoporte.Attributes.Remove("class");
                    //    //imgBtnSoporte.Attributes["title"] = "Sin Archivos";
                    //    //imgBtnComprobantes.Enabled = true;
                    //    //imgBtnComprobantes.Attributes["class"] = "edit";
                    //    //imgBtnComprobantes.Attributes["title"] = "Comprobantes PDF y XML";
                    //}
                    //lo desabilito porque ahora siempre debe tener comprobantes 
                    //if (((PagoElectronico)(e.Item.DataItem)).PagElecArchivo.Count > 0)
                    //{
                 
                    //////if (Convert.ToInt32(dataItem["Id_PagElecTipo"].Text) == 2)
                    //////    dataItem["Acr_Nombre"].Text = "[Varios]";

                    //imgBtnComprobantes.Visible = true;
                    //imgBtnComprobantes.Enabled = true;
                    //imgBtnComprobantes.Attributes["class"] = "edit";
                    //imgBtnComprobantes.Attributes["title"] = "Comprobantes PDF y XML";

                    //}
                    //    //JFCV si no tiene enonces el icono es no disponible
                    //else
                    //{
                    //    imgBtnComprobantes.Visible = false;
                    //    imgBtnComprobantes.Enabled = false;
                    //    imgBtnComprobantes.Attributes.Remove("class");
                    //    imgBtnSoporte.Attributes["title"] = "Sin Archivos";
                    //}
                    #endregion


                }
            }
        
            protected void rgRiks_PageIndexChanged(object source, GridPageChangedEventArgs e)
            {
                try
                {

                    rgRiks.Rebind();

                }
                catch (Exception ex)
                {
                    ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                }
            }

            
            private List<ReporteComisiones> GetList()
            {
                try
                {

                    Sesion session = new Sesion();
                    session = (Sesion)Session["Sesion" + Session.SessionID];

                    SistemaCompensacionGetXML sistemaCompensacionGetXML = new SistemaCompensacionGetXML();

                    sistemaCompensacionGetXML.Id_Emp = session.Id_Emp;
                    sistemaCompensacionGetXML.Id_Cd = Convert.ToInt32(this.CmbId_Cd.SelectedValue);
                    sistemaCompensacionGetXML.Id_Sistema = id_Sistema;
                    sistemaCompensacionGetXML.Anio = Convert.ToInt32(this.cmbanio.SelectedValue);
                    sistemaCompensacionGetXML.Mes = Convert.ToInt32(this.cmbmes.SelectedValue);
                    sistemaCompensacionGetXML.Id_TipoCDI = Convert.ToInt32(this.cmbEmpresa.SelectedValue); 
                    if (TxtId_Rik.Text != "")
                    {
                        sistemaCompensacionGetXML.Id_Rik = Convert.ToInt32(TxtId_Rik.Text);
                    }
                    sistemaCompensacionGetXML.MesTexto = this.cmbmes.Text;
                    //sistemaCompensacionGetXML.RikNombre = this.cmbRik.Text;
                    if (TxtId_Rik.Text != "")
                    {
                        sistemaCompensacionGetXML.Id_Representante = Convert.ToInt32(TxtId_Rik.Text);
                    }

                   
                    sistemaCompensacionGetXML.Id_TipoRepresentante = Convert.ToInt32(this.CmbTipo_Representante.SelectedValue);
                    //todo JFCV ver como autoamtirzarlo 
                    if (sistemaCompensacionGetXML.Id_TipoCDI == 3)
                    {
                        sistemaCompensacionGetXML.Id_TipoRepresentante = 3;  // cuando sea franquicias tiene que ser 3 
                    }

                    CN_CatCompensacion clsCapPagoElectronico = new CN_CatCompensacion();
                    List<ReporteComisiones> list = new List<ReporteComisiones>();
                    clsCapPagoElectronico.ConsultaRepresentantesListado(sistemaCompensacionGetXML, session.Emp_Cnx, ref list);
 
                    return list;

                    //list = list.Where(x => x.Id_PagElecEstatus != 4).ToList();

                    //if (CmbEstatus.SelectedValue == "")
                    //{
                    //    return list;
                    //}
                    //else
                    //{
                    //    return list.Where(x => x.Id_PagElecEstatus == Int32.Parse(CmbEstatus.SelectedValue)).ToList();
                    //}
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            protected void chkProcesarCabezera_CheckedChanged(object sender, EventArgs e)
            {

                try
                {
                    ErrorManager();
                    for (int x = 0; x < rgRiks.Items.Count; x++)
                        (rgRiks.Items[x].FindControl("chkEnviarMail") as System.Web.UI.WebControls.CheckBox).Checked = (sender as System.Web.UI.WebControls.CheckBox).Checked && (rgRiks.Items[x].FindControl("chkEnviarMail") as System.Web.UI.WebControls.CheckBox).Visible;
                }
                catch (Exception ex)
                {
                    ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                }

                
            } 
         
        #endregion eventos grid 

        #region Eventos Grid Concentrado

            protected void rgRiksPrevio_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
            {
                try
                {
                    if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    {

                        rgRiksPrevio.DataSource = GetListRiksPrevio();
                        Session["Table"] = rgRiksPrevio.DataSource;
                        
                    }
                }
                catch (Exception ex)
                {
                    ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                }
            }

            protected void rgRiksPrevio_ItemDataBound(object sender, GridItemEventArgs e)
            {
                if (e.Item is GridDataItem)
                {


                }
            }

            protected void rgRiksPrevio_PageIndexChanged(object source, GridPageChangedEventArgs e)
            {
                try
                {

                    rgRiksPrevio.Rebind();

                }
                catch (Exception ex)
                {
                    ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                }
            }


            protected void rgRiksPrevio_ItemCommand(object source, GridCommandEventArgs e)
            {
                try
                {
   

                }
                catch (Exception ex)
                {
                    ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                }
            }

        //JFCV 21 junio me marca errores de timeout y no puedo cachar el error en mty 
        // voy a modificar el concepto para que ejecute un solo Sp y ahi me traiga toda la información 

            //private List<ReporteComisiones> GetListRiksPrevio()
            //{
            //    try
            //    {

            //        Sesion session = new Sesion();
            //        session = (Sesion)Session["Sesion" + Session.SessionID];

            //        SistemaCompensacionGetXML sistemaCompensacionGetXML = new SistemaCompensacionGetXML();



            //        sistemaCompensacionGetXML.Id_Emp = session.Id_Emp;
            //        sistemaCompensacionGetXML.Id_Cd = Convert.ToInt32(this.CmbId_Cd.SelectedValue);
            //        sistemaCompensacionGetXML.Id_Sistema = id_Sistema;
            //        sistemaCompensacionGetXML.Anio = Convert.ToInt32(this.cmbanio.SelectedValue);
            //        sistemaCompensacionGetXML.Mes = Convert.ToInt32(this.cmbmes.SelectedValue);
            //        if (TxtId_Rik.Text != "")
            //        {
            //            sistemaCompensacionGetXML.Id_Rik = Convert.ToInt32(TxtId_Rik.Text);
            //        }
            //        sistemaCompensacionGetXML.MesTexto = this.cmbmes.Text;
            //        //sistemaCompensacionGetXML.RikNombre = this.cmbRik.Text;
            //        if (TxtId_Rik.Text != "")
            //        {
            //            sistemaCompensacionGetXML.Id_Representante = Convert.ToInt32(TxtId_Rik.Text);
            //        }


            //        sistemaCompensacionGetXML.Id_TipoRepresentante = Convert.ToInt32(this.CmbTipo_Representante.SelectedValue);

            //        //JFCV todo como el previo solo es para riks , ponerle aqui que el tiporepresentante sea siempre 3 
            //        sistemaCompensacionGetXML.Id_TipoRepresentante = 3;

            //        CN_CatCompensacion clsCapPagoElectronico = new CN_CatCompensacion();
            //        List<ReporteComisiones> list = new List<ReporteComisiones>();
            //        clsCapPagoElectronico.ConsultaRepresentantesListado(sistemaCompensacionGetXML, session.Emp_Cnx, ref list);

            //        //return list;


            //        //listado donde voy a ir guardando los resultados de los sps 
            //        List<ReporteComisiones> listaConcentrado = new List<ReporteComisiones>();

            //        foreach (ReporteComisiones item in list)//loops through each grid row
            //        {

            //            ////if (((item.FindControl("chkEnviarMail")) as System.Web.UI.WebControls.CheckBox).Checked == true)
            //            ////{

            //            int rik_seleccionado = item.Id;
            //            int cdi_seleccionado = item.Id_Cd;
            //            //string rikNombre_seleccionado = (item["Nombre_Empleado"].Text);
            //            //string cdi_nombreseleccionado = (item["Id_Cd"].Text);
            //            //int   tipoRik_seleccionado = Convert.ToInt32(item["id_tiporepresentante"].Text);

            //            if (rik_seleccionado != -1)
            //            {

            //                CN_CatCompensacion clsCatCompensacion = new CN_CatCompensacion();


            //                SistemaCompensacionGetXML sistemaCompensacionGetXMLconcentrado = new SistemaCompensacionGetXML();

            //                sistemaCompensacionGetXMLconcentrado.Id_Emp = session.Id_Emp;
            //                sistemaCompensacionGetXMLconcentrado.Id_Cd = cdi_seleccionado;
            //                sistemaCompensacionGetXMLconcentrado.Id_Sistema = id_Sistema;
            //                sistemaCompensacionGetXMLconcentrado.Anio = Convert.ToInt32(this.cmbanio.SelectedValue);
            //                sistemaCompensacionGetXMLconcentrado.Mes = Convert.ToInt32(this.cmbmes.SelectedValue);
            //                sistemaCompensacionGetXMLconcentrado.Id_Rik = rik_seleccionado;

            //                sistemaCompensacionGetXMLconcentrado.Id_TipoRepresentante = item.Id_TipoRepresentante;
            //                sistemaCompensacionGetXMLconcentrado.RikNombre = item.Nombre_Empleado;
            //                sistemaCompensacionGetXMLconcentrado.CdiNombre = item.CdiNombre;


            //                ReporteComisiones registrorik = new ReporteComisiones();
            //                clsCapPagoElectronico.ReporteConcentrado(sistemaCompensacionGetXMLconcentrado, session.Emp_Cnx, ref  registrorik);

            //                if (registrorik.Id_Cd != null   )
            //                {
            //                    if (registrorik.VtaCob != 0)
            //                    {
            //                        listaConcentrado.Add(registrorik);
            //                    }

            //                }  //fin del if 
            //            }
            //            ////}

            //        } // si no esta checado no hago nada 

            //        return listaConcentrado;

            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //}


            private List<ReporteComisiones> GetListRiksPrevio()
            {
                try
                {

                    Sesion session = new Sesion();
                    session = (Sesion)Session["Sesion" + Session.SessionID];

                    //SistemaCompensacionGetXML sistemaCompensacionGetXML = new SistemaCompensacionGetXML();


                     //listado donde voy a ir guardando los resultados de los sps 
                    
 
                     CN_CatCompensacion clsCatCompensacion = new CN_CatCompensacion();


                    SistemaCompensacionGetXML sistemaCompensacionGetXMLconcentrado = new SistemaCompensacionGetXML();

                    sistemaCompensacionGetXMLconcentrado.Id_Emp = session.Id_Emp;
                    sistemaCompensacionGetXMLconcentrado.Id_Cd = Convert.ToInt32(this.CmbId_Cd.SelectedValue);
                    sistemaCompensacionGetXMLconcentrado.Id_Sistema = id_Sistema;
                    sistemaCompensacionGetXMLconcentrado.Anio = Convert.ToInt32(this.cmbanio.SelectedValue);
                    sistemaCompensacionGetXMLconcentrado.Mes = Convert.ToInt32(this.cmbmes.SelectedValue);
                    if (TxtId_Rik.Text != "")
                    {
                        sistemaCompensacionGetXMLconcentrado.Id_Rik = Convert.ToInt32(TxtId_Rik.Text);
                    }
                    sistemaCompensacionGetXMLconcentrado.MesTexto = this.cmbmes.Text;
                    //sistemaCompensacionGetXML.RikNombre = this.cmbRik.Text;
                    if (TxtId_Rik.Text != "")
                    {
                        sistemaCompensacionGetXMLconcentrado.Id_Representante = Convert.ToInt32(TxtId_Rik.Text);
                    }


                    sistemaCompensacionGetXMLconcentrado.Id_TipoRepresentante = Convert.ToInt32(this.CmbTipo_Representante.SelectedValue);

                    //JFCV todo como el previo solo es para riks , ponerle aqui que el tiporepresentante sea siempre 3 
                    sistemaCompensacionGetXMLconcentrado.Id_TipoRepresentante = 3;
 

                            ReporteComisiones registrorik = new ReporteComisiones();
                             List<ReporteComisiones> listaConcentrado = new List<ReporteComisiones>();

                             string StrCnx = ConfigurationManager.AppSettings.Get("strConnection");
                            session.Emp_Cnx =  StrCnx + ";Connect Timeout=60000"; 
                            clsCatCompensacion.ReporteConcentrado(sistemaCompensacionGetXMLconcentrado, session.Emp_Cnx, ref  listaConcentrado);

                            //if (listaConcentrado  != null)
                            //{
                            //    if (listaConcentrado.Count> 0)
                            //    {
                            //        listaConcentrado.Add(listaConcentrado);
                            //    }

                            //}  //fin del if 
 

                    return listaConcentrado;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion  Eventos Grid Concentrado
        
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
                    this.lblMensajeError.Text = "";
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
                    this.lblMensajeError.Text = Message;
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
                    this.lblMensajeError.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

                }
                catch (Exception ex)
                {
                    this.lblMensajeError.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
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
