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
    public partial class CapMonitoreoGestionRentabilidadRIK : System.Web.UI.Page
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
        public int id_rik, id_ter;
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
            
            this.Inicializar();
            txtRepresentante.Text = txtRepresentante.Text == "0" ? "0" : Convert.ToString(Request.QueryString["Id_Rik"]);
            txtTerritorio.Text = txtTerritorio.Text == "0" ? "0" : Convert.ToString(Request.QueryString["Id_Ter"]);
            id_rik = Convert.ToInt32(Request.QueryString["Id_Rik"] == "" ? "0" : Request.QueryString["Id_Rik"]);
            id_ter = Convert.ToInt32(Request.QueryString["Id_Ter"] == "" ? "0" : Request.QueryString["Id_Ter"]);


            CN_CatCalendario cn_calenda = new CN_CatCalendario();
            Calendario c = new Calendario();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            if (Convert.ToString(Request.QueryString["txtMesInicial"]) != null)
            {

                TxtAnioInicial.Text = Convert.ToString(Request.QueryString["TxtAnioInicial"]);
                TxtAnioFinal.Text = Convert.ToString(Request.QueryString["TxtAnioFinal"]);
                txtMesInicial.SelectedValue = Convert.ToInt32(Request.QueryString["txtMesInicial"]) < 10 ? ("0" + Convert.ToString(Convert.ToInt32(Convert.ToString(Request.QueryString["txtMesInicial"])))) : Convert.ToString(Request.QueryString["txtMesInicial"]);
                txtMesFinal.SelectedValue = Convert.ToInt32(Request.QueryString["txtMesFinal"]) < 10 ? ("0" + Convert.ToString(Convert.ToInt32(Convert.ToString(Request.QueryString["txtMesFinal"])))) : Convert.ToString(Request.QueryString["txtMesFinal"]);

            }
            else
            {

                cn_calenda.ConsultaCalendarioActual(ref c, Sesion);

                if (Convert.ToInt32(c.Cal_Mes.ToString()) == 1)
                {
                    txtMesFinal.SelectedValue = "12";
                    TxtAnioFinal.Text = Convert.ToString((Convert.ToInt32(c.Cal_Año.ToString()) - 1));
                }
                else
                {
                    txtMesFinal.SelectedValue = "0" + Convert.ToString((Convert.ToInt32(c.Cal_Mes.ToString()) - 1));
                    TxtAnioFinal.Text = c.Cal_Año.ToString();
                }


                if (Convert.ToInt32(c.Cal_Mes.ToString()) == 3)
                {
                    txtMesInicial.SelectedValue = "12";
                    TxtAnioInicial.Text = Convert.ToString((Convert.ToInt32(c.Cal_Año.ToString()) - 1));
                }
                else
                {
                    txtMesInicial.SelectedValue = "0" + Convert.ToString((Convert.ToInt32(c.Cal_Mes.ToString()) - 4));
                    TxtAnioInicial.Text = c.Cal_Año.ToString();
                }

            }



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


                Session["Sesion" + Session.SessionID] = sesion;


                rgGestionRentabilidad.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        #endregion
        #region Funciones

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        Session["ListaRemisionesFactura"] = new List<Remision>();// null;


                        break;

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void rgGestionRentabilidad_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {  //Llenar Grid
                    rgGestionRentabilidad.DataSource = this.GetList();


                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void rgGestionRentabilidad_CustomAggregate(object sender, GridCustomAggregateEventArgs e)
        {
            if (((Telerik.Web.UI.GridBoundColumn)e.Column).UniqueName == "UtilidadBrutaPorc")
            {
                // TODO: Filter duplicate check numbers
                Double Venta = 0;
                Double UtilidadBruta = 0;
                foreach (GridDataItem item in rgGestionRentabilidad.MasterTableView.Items)
                {
                    Venta += Convert.ToDouble(item["VentaImporte"].Text);
                    UtilidadBruta += Convert.ToDouble(item["UtilidadBrutaImporte"].Text);
                }


                e.Result = (UtilidadBruta / Venta) * 100;
            }
            if (((Telerik.Web.UI.GridBoundColumn)e.Column).UniqueName == "MetaUtilidadBrutaPorc")
            {
                // TODO: Filter duplicate check numbers
                Double Venta = 0;
                Double Meta = 0;
                foreach (GridDataItem item in rgGestionRentabilidad.MasterTableView.Items)
                {
                    Venta += Convert.ToDouble(item["VentaImporte"].Text);
                    Meta += Convert.ToDouble(item["MetaUtilidadBrutaImporte"].Text);
                }


                e.Result = (Meta / Venta) * 100;
            }

            if (((Telerik.Web.UI.GridBoundColumn)e.Column).UniqueName == "UtilidadBrutaProyectadaPorc")
            {
                // TODO: Filter duplicate check numbers
                Double Venta = 0;
                Double Proyectada = 0;
                foreach (GridDataItem item in rgGestionRentabilidad.MasterTableView.Items)
                {
                    Venta += Convert.ToDouble(item["VentaImporte"].Text);
                    Proyectada += Convert.ToDouble(item["UtilidadBrutaProyectadaImporte"].Text);
                }


                e.Result = (Proyectada / Venta) * 100;
            }
        }


        protected void rgGestionRentabilidad_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgGestionRentabilidad.Rebind();
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
        #region Funciones
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

        private void Inicializar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                this.CargarCentros();
                rgGestionRentabilidad.Rebind();

                //GraficaUtilidad.Text = "<div id='myNextDiv' ><!-- START Code Block for Chart myNext --><embed src=\"FusionCharts/Line.swf\" FlashVars=\"&chartWidth=400&chartHeight=200&debugMode=0&registerWithJS=0&DOMId=myNext&dataXML=<chart formatNumberScale='0' caption=' ' yAxisMaxValue='61' yaxisname='' bgColor='FFFFFF'  bgAlpha='100' baseFontColor='000000' canvasBgAlpha='0' canvasBorderColor='696969' divLineColor='696969' divLineAlpha='100' numVDivlines='10' vDivLineisDashed='1' showAlternateVGridColor='1' lineColor='399E38' anchorRadius='4' anchorBgColor='BBDA00' anchorBorderColor='696969' anchorBorderThickness='1' showValues='0'  toolTipBgColor='FFFFFF' toolTipBorderColor='406181' alternateHGridAlpha='5' labelDisplay='ROTATE' canvaspadding='6' showBorder='0'><set label='04/feb' value='1.55'  distance='6'/><set label='05/feb' value='1.55'  distance='6'/><set label='06/feb' value='1.56'  distance='6'/><set label='07/feb' value='1.52'  distance='6'/><set label='08/feb' value='1.53'  distance='6'/><set label='09/feb' value='0.00'  distance='6'/><set label='10/feb' value='0.00'  distance='6'/><set label='11/feb' value='0.00'  distance='6'/><set label='12/feb' value='0.00'  distance='6'/><set label='13/feb' value='0.00'  distance='6'/><set label='14/feb' value='0.00'  distance='6'/><set label='15/feb' value='0.00'  distance='6'/><set label='16/feb' value='0.00'  distance='6'/><set label='17/feb' value='0.00'  distance='6'/><set label='18/feb' value='0.00'  distance='6'/><set label='19/feb' value='0.00'  distance='6'/><set label='20/feb' value='0.00'  distance='6'/><set label='21/feb' value='0.00'  distance='6'/><set label='22/feb' value='0.00'  distance='6'/><set label='23/feb' value='0.00'  distance='6'/><set label='24/feb' value='0.00'  distance='6'/><set label='25/feb' value='0.00'  distance='6'/><set label='26/feb' value='0.00'  distance='6'/><set label='27/feb' value='49.05'  distance='6'/><set label='28/feb' value='0.00'  distance='6'/><set label='01/mar' value='0.00'  distance='6'/><set label='02/mar' value='0.00'  distance='6'/><set label='03/mar' value='0.00'  distance='6'/><styles><definition><style name='LineShadow' type='shadow' color='333333' distance='6'/></definition><application><apply toObject='DATAPLOT' styles='LineShadow' /></application></styles></chart>&scaleMode=noScale&lang=EN\" quality=\"high\" width=\"400\" height=\"200\" name=\"myNext\" id=\"myNext\" allowScriptAccess=\"always\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\"   /> <!-- END Code Block for Chart myNext --> </div>";



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<MonitoreoGestionRentabilidadRIK> GetList()
        {

            try
            {



                //foreach (GridColumn col in rgGestionRentabilidad.MasterTableView.DetailTables[0].Columns) 
                //   { 
                //       if (col.UniqueName == "Cte_NomComercial") 
                //       { 
                //           col.Visible = false; 
                //       } 
                //   }






                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CN_CatCalendario cn_calenda = new CN_CatCalendario();
                Calendario c = new Calendario();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];



                cn_calenda.ConsultaCalendarioActual(ref c, Sesion);


                string AnioInicial = "";
                string AnioFinal = "";
                string MesInicial = "";
                string MesFinal = "";
                string Grafica = "";

                if (txtMesInicial.Text != null && txtMesInicial.Text != "")
                {
                    AnioInicial = TxtAnioInicial.Text;
                    AnioFinal = TxtAnioFinal.Text;
                    MesInicial = txtMesInicial.SelectedValue;
                    MesFinal = txtMesFinal.SelectedValue;
                }
                else
                {

                    if (Convert.ToString(Request.QueryString["txtMesInicial"]) != null)
                    {

                        AnioInicial = Convert.ToString(Request.QueryString["TxtAnioInicial"]);
                        AnioFinal = Convert.ToString(Request.QueryString["TxtAnioFinal"]);
                        MesInicial = Convert.ToString(Request.QueryString["txtMesInicial"]);
                        MesFinal = Convert.ToString(Request.QueryString["txtMesFinal"]);


                    }
                    else
                    {
                        cn_calenda.ConsultaCalendarioActual(ref c, Sesion);

                        if (Convert.ToInt32(c.Cal_Mes.ToString()) == 1)
                        {
                            MesFinal = "12";
                            AnioFinal = Convert.ToString((Convert.ToInt32(c.Cal_Año.ToString()) - 1));
                        }
                        else
                        {
                            MesFinal = "0" + Convert.ToString((Convert.ToInt32(c.Cal_Mes.ToString()) - 1));
                            AnioFinal = c.Cal_Año.ToString();
                        }


                        if (Convert.ToInt32(c.Cal_Mes.ToString()) == 3)
                        {
                            MesInicial = "12";
                            AnioInicial = Convert.ToString((Convert.ToInt32(c.Cal_Año.ToString()) - 1));
                        }
                        else
                        {
                            MesInicial = "0" + Convert.ToString((Convert.ToInt32(c.Cal_Mes.ToString()) - 4));
                            AnioInicial = c.Cal_Año.ToString();
                        }
                    }

                }



                List<MonitoreoGestionRentabilidadRIK> listMonitoreoGestionRentabilidadRIK = new List<MonitoreoGestionRentabilidadRIK>();
                MonitoreoGestionRentabilidadRIK monitoreoGestionRentabilidadRIK = new MonitoreoGestionRentabilidadRIK();




                new CN_MonitoreoIndicadoresUtilidad().MonitoreoGestionRentabildiadRIK_Buscar(monitoreoGestionRentabilidadRIK
                                    , sesion.Emp_Cnx
                                    , ref listMonitoreoGestionRentabilidadRIK
                                    , sesion.Id_Emp
                                    , sesion.Id_Cd_Ver
                                    , Convert.ToInt32(MesInicial)
                                    , Convert.ToInt32(AnioInicial)
                                    , Convert.ToInt32(MesFinal)
                                    , Convert.ToInt32(AnioFinal)
                                    , sesion.Id_U
                                    , Convert.ToInt32(Convert.ToString(Request.QueryString["Id_Rik"]))
                                    , ref Grafica
                                    );


                GraficaUtilidad.Text = Grafica;
                GraficaUtilidad.Visible = false;
                GraficaUtilidad.Visible = true;


                return listMonitoreoGestionRentabilidadRIK;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                rgGestionRentabilidad.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        public decimal? x { get; set; }
    }
}