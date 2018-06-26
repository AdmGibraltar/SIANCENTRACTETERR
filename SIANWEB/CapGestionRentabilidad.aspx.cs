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
using System.Xml;
using System.Diagnostics;
using System.Configuration;

namespace SIANWEB
{
    public partial class CapGestionRentabilidad : System.Web.UI.Page
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

            this.Inicializar();


            TxtPorCliente.Text = "30";
            TxtPorQuimicos.Text = "40";
            TxtPorPapelTradicional.Text = "15";
            TxtPorSistemaDiferenciado.Text = "25";
            txtPorJarcieria.Text = "15";
            txtPorAccesorios.Text = "15";
            txtPorBolsaBasura.Text = "15";



            CN_CatCalendario cn_calenda = new CN_CatCalendario();
            Calendario c = new Calendario();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            if (Convert.ToString(Request.QueryString["txtMesInicial"]) != null)
            {

                TxtAnioInicial.Text = Convert.ToString(Request.QueryString["TxtAnioInicial"]);
                TxtAnioFinal.Text = Convert.ToString(Request.QueryString["TxtAnioFinal"]);
                txtMesInicial.SelectedValue = Convert.ToString(Request.QueryString["txtMesInicial"]);
                txtMesFinal.SelectedValue = Convert.ToString(Request.QueryString["txtMesFinal"]);
                txtTerritorio.Text = Convert.ToString(Request.QueryString["StxtTerritorio"]);
                txtRepresentante.Text = Convert.ToString(Request.QueryString["StxtRepresentante"]) == "-1" ? "" : Convert.ToString(Request.QueryString["StxtRepresentante"]);
                TxtNumeroCliente.Text  = Convert.ToString(Request.QueryString["STxtNumeroCliente"]);
                TxtPorCliente.Text = Convert.ToString(Request.QueryString["STxtPorCliente"]);
                TxtPorQuimicos.Text = Convert.ToString(Request.QueryString["STxtPorQuimicos"]);
                TxtPorPapelTradicional.Text = Convert.ToString(Request.QueryString["STxtPorPapelTradicional"]);
                TxtPorSistemaDiferenciado.Text  = Convert.ToString(Request.QueryString["STxtPorSistemaDiferenciado"]);
                txtPorJarcieria.Text = Convert.ToString(Request.QueryString["StxtPorJarcieria"]);
                txtPorAccesorios.Text = Convert.ToString(Request.QueryString["StxtPorAccesorios"]);
                txtPorBolsaBasura.Text = Convert.ToString(Request.QueryString["StxtPorBolsaBasura"]);
                txtCategorias.Text = Convert.ToString(Request.QueryString["StxtCategorias"]);

            }
            else
            {

                cn_calenda.ConsultaCalendarioActual(ref c, Sesion);
                TxtAnioInicial.Text = c.Cal_Año.ToString();
                TxtAnioFinal.Text = c.Cal_Año.ToString();
                if (Convert.ToInt32(c.Cal_Mes.ToString()) <= 9)
                {
                    txtMesInicial.SelectedValue = "0" + c.Cal_Mes.ToString();
                    txtMesFinal.SelectedValue = "0" + c.Cal_Mes.ToString();
                }
                else
                {
                    txtMesInicial.SelectedValue = c.Cal_Mes.ToString();
                    txtMesFinal.SelectedValue = c.Cal_Mes.ToString();
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
        protected void rgGestionRentabilidad_ItemCommand(object source, GridCommandEventArgs e)
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
                    int Id_Emp = Convert.ToInt32(rgGestionRentabilidad.Items[item]["Id_Emp"].Text);
                    int Id_Cd = Convert.ToInt32(rgGestionRentabilidad.Items[item]["Id_Cd"].Text);
                    int Id_Cte = Convert.ToInt32(rgGestionRentabilidad.Items[item]["Id_Cte"].Text);
                    int Id_Ter = Convert.ToInt32(rgGestionRentabilidad.Items[item]["Id_Ter"].Text);
                    string Cte_NomComercial = rgGestionRentabilidad.Items[item]["Cte_NomComercial"].Text;

                    switch (e.CommandName.ToString())
                    {
                        case "Crear Proyecto":
                            RAM1.ResponseScripts.Add(string.Concat(@"AbrirProyecto('", Id_Emp, "','", Id_Cd, "','", Id_Ter, "','", Id_Cte, "','", Cte_NomComercial, "')"));
                            break;
                    }

                }
                if (e.CommandName.ToString().ToUpper().Contains("SORT"))
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<GestionRentabilidad> GetList()
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


                if (txtCategorias.SelectedValue == "0" || this.txtCategorias.SelectedValue == string.Empty)
                {
                    rgGestionRentabilidad.MasterTableView.Columns[8].Visible = false;
                    rgGestionRentabilidad.MasterTableView.Columns[9].Visible = false;
                    rgGestionRentabilidad.MasterTableView.Columns[10].Visible = false;
                    rgGestionRentabilidad.MasterTableView.Columns[11].Visible = false;
                    rgGestionRentabilidad.MasterTableView.Columns[12].Visible = false;
                    rgGestionRentabilidad.MasterTableView.Columns[13].Visible = false;
                    rgGestionRentabilidad.MasterTableView.Columns[14].Visible = false;
                    rgGestionRentabilidad.MasterTableView.Columns[15].Visible = false;
                    rgGestionRentabilidad.MasterTableView.Columns[16].Visible = false;
                    rgGestionRentabilidad.MasterTableView.Columns[17].Visible = false;
                    rgGestionRentabilidad.MasterTableView.Columns[18].Visible = false;
                    rgGestionRentabilidad.MasterTableView.Columns[19].Visible = false;

                }
                else
                {
                    rgGestionRentabilidad.MasterTableView.Columns[8].Visible = true;
                    rgGestionRentabilidad.MasterTableView.Columns[9].Visible = true;
                    rgGestionRentabilidad.MasterTableView.Columns[10].Visible = true;
                    rgGestionRentabilidad.MasterTableView.Columns[11].Visible = true;
                    rgGestionRentabilidad.MasterTableView.Columns[12].Visible = true;
                    rgGestionRentabilidad.MasterTableView.Columns[13].Visible = true;
                    rgGestionRentabilidad.MasterTableView.Columns[14].Visible = true;
                    rgGestionRentabilidad.MasterTableView.Columns[15].Visible = true;
                    rgGestionRentabilidad.MasterTableView.Columns[16].Visible = true;
                    rgGestionRentabilidad.MasterTableView.Columns[17].Visible = true;
                    rgGestionRentabilidad.MasterTableView.Columns[18].Visible = true;
                    rgGestionRentabilidad.MasterTableView.Columns[19].Visible = true;
                }





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

                string Territorio = "";
                string Representante = "";
                string NumeroCliente="";
                string PorCliente="";
                string PorQuimicos="";
                string PorPapelTradicional="";
                string PorSistemaDiferenciado="";
                string PorJarcieria="";
                string PorAccesorios="";
                string PorBolsaBasura="";
                string Categorias="";

                Territorio = txtTerritorio.Text;
                Representante = txtRepresentante.Text;
                NumeroCliente = TxtNumeroCliente.Text;
                PorCliente = TxtPorCliente.Text;
                PorQuimicos = TxtPorQuimicos.Text;
                PorPapelTradicional = TxtPorPapelTradicional.Text;
                PorSistemaDiferenciado = TxtPorSistemaDiferenciado.Text;
                PorJarcieria = txtPorJarcieria.Text;
                PorAccesorios = txtPorAccesorios.Text;
                PorBolsaBasura = txtPorBolsaBasura.Text;
                Categorias = txtCategorias.SelectedValue;



               if (txtMesInicial.Text != null && txtMesInicial.Text !="")
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


                        Territorio = Convert.ToString(Request.QueryString["StxtTerritorio"]);
                        Representante = Convert.ToString(Request.QueryString["StxtRepresentante"]);
                        NumeroCliente = Convert.ToString(Request.QueryString["STxtNumeroCliente"]);
                        PorCliente = Convert.ToString(Request.QueryString["STxtPorCliente"]);
                        PorQuimicos = Convert.ToString(Request.QueryString["STxtPorQuimicos"]);
                        PorPapelTradicional = Convert.ToString(Request.QueryString["STxtPorPapelTradicional"]);
                        PorSistemaDiferenciado = Convert.ToString(Request.QueryString["STxtPorSistemaDiferenciado"]);
                        PorJarcieria = Convert.ToString(Request.QueryString["StxtPorJarcieria"]);
                        PorAccesorios = Convert.ToString(Request.QueryString["StxtPorAccesorios"]);
                        PorBolsaBasura = Convert.ToString(Request.QueryString["StxtPorBolsaBasura"]);
                        Categorias = Convert.ToString(Request.QueryString["StxtCategorias"]);



                    } else {
                        cn_calenda.ConsultaCalendarioActual(ref c, Sesion);
                        AnioInicial = c.Cal_Año.ToString();
                        AnioFinal = c.Cal_Año.ToString();
                        if (Convert.ToInt32(c.Cal_Mes.ToString()) <= 9)
                        {
                            MesInicial = "0" + c.Cal_Mes.ToString();
                            MesFinal = "0" + c.Cal_Mes.ToString();
                        }
                        else
                        {
                            MesInicial = c.Cal_Mes.ToString();
                            MesFinal = c.Cal_Mes.ToString();
                        }
                    }

                }



                List<GestionRentabilidad> listGestionRentabilidad = new List<GestionRentabilidad>();
                GestionRentabilidad gestionRentabilidad = new GestionRentabilidad();
               



                new CN_GestionRentabilidad().ConsultaGestionRentabilidad_Buscar(gestionRentabilidad
                                    , sesion.Emp_Cnx
                                    , ref listGestionRentabilidad                                    
                                    , sesion.Id_Emp
                                    , sesion.Id_Cd_Ver
                                    , NumeroCliente == string.Empty ? "" : NumeroCliente
                                    , Territorio == string.Empty ? "" : Territorio
                                    , Representante == string.Empty ? -1 : Convert.ToInt32(Representante)
                                    , this.TxtNombreCliente.Text
                                    , Convert.ToInt32(MesInicial)
                                    , Convert.ToInt32(AnioInicial)
                                    , Convert.ToInt32(MesFinal)
                                    , Convert.ToInt32(AnioFinal)
                                    , sesion.Id_U
                                    , PorCliente == string.Empty ? 30 : Convert.ToInt32(PorCliente)
                                    , Categorias == string.Empty ? 0 : Convert.ToInt32(Categorias)
                                    , PorQuimicos == string.Empty ? 40 : Convert.ToInt32(PorQuimicos)
                                    , PorPapelTradicional == string.Empty ? 15 : Convert.ToInt32(PorPapelTradicional)
                                    , PorSistemaDiferenciado == string.Empty ? 25 : Convert.ToInt32(PorSistemaDiferenciado)
                                    , PorJarcieria == string.Empty ? 15 : Convert.ToInt32(PorJarcieria)
                                    , PorAccesorios == string.Empty ? 15 : Convert.ToInt32(PorAccesorios)
                                    , PorBolsaBasura == string.Empty ? 15 : Convert.ToInt32(PorBolsaBasura)
                                    );

                if (txtCategorias.SelectedValue == "0" || this.txtCategorias.SelectedValue == string.Empty)
                {
                    DivTotales.Visible = true;
                    TotalesCategorias.Visible = false;
                    Totventa.Text = Convert.ToString(listGestionRentabilidad.Sum(z => z.venta));
                    TotUtilidadBruta.Text = Convert.ToString(listGestionRentabilidad.Sum(z => (z.venta - z.Costo)));
                    TotCosto.Text = Convert.ToString(listGestionRentabilidad.Sum(z => z.Costo));
                    TotInversionSP.Text = Convert.ToString(listGestionRentabilidad.Sum(z => z.InversionSP));
                    TotInversionCT.Text = Convert.ToString(listGestionRentabilidad.Sum(z => z.InversionCT));
                } else {
                    DivTotales.Visible = false;
                    TotalesCategorias.Visible = true;
                    TotventaC.Text = Convert.ToString(listGestionRentabilidad.Sum(z => z.venta));
                    TotUtilidadBrutaC.Text = Convert.ToString(listGestionRentabilidad.Sum(z => (z.venta - z.Costo)));
                    TotCostoC.Text = Convert.ToString(listGestionRentabilidad.Sum(z => z.Costo));
                    TotInversionSPC.Text = Convert.ToString(listGestionRentabilidad.Sum(z => z.InversionSP));
                    TotInversionCTC.Text = Convert.ToString(listGestionRentabilidad.Sum(z => z.InversionCT));
                    TotVentasQuimicos.Text = Convert.ToString(listGestionRentabilidad.Sum(z => z.VentaQuimicos));
                    TotVentasPT.Text = Convert.ToString(listGestionRentabilidad.Sum(z => z.VentaPT));
                    TotVentasSD.Text = Convert.ToString(listGestionRentabilidad.Sum(z => z.VentaSD));
                    TotVentasJarcerias.Text = Convert.ToString(listGestionRentabilidad.Sum(z => z.VentaJarceria));
                    TotVentasAccesorios.Text = Convert.ToString(listGestionRentabilidad.Sum(z => z.VentaAccesorios));
                    TotVentasBB.Text = Convert.ToString(listGestionRentabilidad.Sum(z => z.VentaBB));

                }



               



                return listGestionRentabilidad;

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