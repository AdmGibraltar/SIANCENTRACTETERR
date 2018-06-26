using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Collections;
using System.Data;
using Telerik.Reporting.Processing;
using System.IO;

namespace SIANWEB
{
    public partial class Rep_VenRentabilidad : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoModificar { get { return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoGuardar { get { return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

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

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        if (Session["ReporteRentabilidadClientes" + Session.SessionID] != null)
                        {
                            if (Session["ReporteRentabilidadClientes" + Session.SessionID].ToString() == "SI")
                            {
                                this.Imprimir(true);
                                Session["ReporteRentabilidadClientes" + Session.SessionID] = null;
                                Session["ReporteValuacionProyecto" + Session.SessionID] = null;
                            }
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }

        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            string mensajeError = string.Empty;
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                if (btn.CommandName == "print")
                {
                    Imprimir(true);
                }
                else
                {
                    Imprimir(false);
                }
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, mensajeError);
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        protected void cmbCentrosDist_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
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

                //this.CargarCliente();
                txtCliente.Text = string.Empty;
                //cmbCliente.SelectedIndex = cmbCliente.FindItemIndexByValue("-1");

                List<Territorios> listaTerr = new List<Territorios>();
                Territorios ter = new Territorios();
                ter.Descripcion = "-- Seleccionar --";
                ter.Id_Ter = -1;
                listaTerr.Insert(0, ter);
                txtTerritorio.Text = string.Empty;

            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Cmb_CentroDistribucion_IndexChanging_error"));
            }
        }
        protected void cmbCliente_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (txtCliente.Value.Value.ToString() != "-1")
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    List<Territorios> listaTerritorios = new List<Territorios>();

                    //Consultar territorios relacionados con el cliente
                    new CN_CatCliente().ConsultaTerritoriosDelCliente(Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value : -1), sesion, ref listaTerritorios);

                }
                else
                {
                    List<Territorios> listaTerr = new List<Territorios>();
                    Territorios ter = new Territorios();
                    ter.Descripcion = "-- Seleccionar --";
                    ter.Id_Ter = -1;
                    listaTerr.Insert(0, ter);
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "cmbCliente_IndexChanging_error"));
            }
        }
        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Clientes cliente = new Clientes();
                cliente.Id_Emp = sesion.Id_Emp;
                cliente.Id_Cd = sesion.Id_Cd_Ver;
                cliente.Id_Cte = Convert.ToInt32(txtCliente.Value);
                cliente.Id_Rik = sesion.Id_Rik;
                try
                {
                    new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);
                    txtClienteNombre.Text = cliente.Cte_NomComercial;

                    //CargarComboTerritorios();
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, txtCliente.ClientID);
                    txtClienteNombre.Text = "";
                    txtCliente.Text = "";

                    return;
                }

                //CargarComboTerritorios();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Funciones
        private void Imprimir(bool a_pantalla)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();

                CentroDistribucion cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);

                //Obtener datos de encabezado de la Valuación de proyecto
                string Id_Cte = string.Empty, Cte_NomComercial = string.Empty, Vap_Nota = string.Empty;

                Id_Cte = Convert.ToString(txtCliente.Value.HasValue ? txtCliente.Value : -1); //cmbCliente.SelectedValue;
                Cte_NomComercial = txtClienteNombre.Text; //cmbCliente.SelectedItem.Text;
                int? territorio = null;
                if (txtTerritorio.Text != "")
                {
                    territorio = Convert.ToInt32(txtTerritorio.Text);
                }
                else
                {
                    territorio=0;
                }

                DataTable dtReporteTotales = new DataTable();
                new CN_CatCliente().ReporteRentabilidad_ConsultarTotales(
                    sesion.Id_Emp
                    , sesion.Id_Cd_Ver
                    , Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1)
                    , territorio
                    , cmbPeriodo.SelectedValue
                    , cmbVentas.SelectedValue
                    , ref dtReporteTotales
                    , sesion.Emp_Cnx);

                ALValorParametrosInternos.Add(sesion.Id_Emp);
                ALValorParametrosInternos.Add(sesion.Emp_Nombre);
                ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                ALValorParametrosInternos.Add(sesion.Cd_Nombre);
                ALValorParametrosInternos.Add(Id_Cte);
                ALValorParametrosInternos.Add(Cte_NomComercial);
                ALValorParametrosInternos.Add(txtTerritorio.Text);
                ALValorParametrosInternos.Add(txtTerritorio.Text != "-1" ? txtTerritorio.Text : "Todos");
                ALValorParametrosInternos.Add(cmbPeriodo.SelectedValue);
                ALValorParametrosInternos.Add(cmbVentas.SelectedValue);
                ALValorParametrosInternos.Add(DateTime.Now.ToString("dd/MM/yyyy"));
                ALValorParametrosInternos.Add(DateTime.Now.ToString("hh:MM:ss"));
                ALValorParametrosInternos.Add(sesion.Emp_Cnx);

                double VentaNeta = Convert.ToDouble(dtReporteTotales.Rows[0]["VentaNeta"]);
                double VentaNetaPapel = Convert.ToDouble(dtReporteTotales.Rows[0]["VentaNetaPapel"]);
                double VentaNetaOtros = Convert.ToDouble(dtReporteTotales.Rows[0]["VentaNetaOtros"]);
                double CostoMaterial = Convert.ToDouble(dtReporteTotales.Rows[0]["CostoMaterial"]);
                double AmortizacionTotal = Convert.ToDouble(dtReporteTotales.Rows[0]["AmortizacionTotal"]);
                double Prd_PesConTecnico = Convert.ToDouble(dtReporteTotales.Rows[0]["Prd_PesConTecnico"]);
                double UtilidadBruta = Convert.ToDouble(dtReporteTotales.Rows[0]["UtilidadBruta"]);

                double Cte_CarMP = Convert.ToDouble(dtReporteTotales.Rows[0]["Cte_CarMP"]);
                double Cte_GasVarT = Convert.ToDouble(dtReporteTotales.Rows[0]["Cte_GasVarT"]);
                double Cte_FletePaga = (VentaNeta) * 0.002; //Convert.ToDouble(dtReporteTotales.Rows[0]["Cte_FletePaga"]);
                double DiasRotacion = Convert.ToDouble(dtReporteTotales.Rows[0]["DiasRotacion"]);

                string listaClavesProd = dtReporteTotales.Rows[0]["Id_PrdStr"].ToString();

                // ------------------------------------------------
                // calcular amortizacion de cada producto
                // ------------------------------------------------
                if (listaClavesProd.Length > 0) listaClavesProd = listaClavesProd.Substring(0, listaClavesProd.Length - 1);
                string[] arrayId_Prd = listaClavesProd.Split(new char[] { ',' });
                //objeto amortizacion
                Amortizacion amortizacion = new Amortizacion();
                amortizacion.Id_Emp = sesion.Id_Emp;
                amortizacion.Id_Cd = sesion.Id_Cd_Ver;
                amortizacion.Id_Cte = Convert.ToInt32(Id_Cte);

                //obtener productos con amortización del producto
                int anioActual = DateTime.Now.Year;
                int mesActual = DateTime.Now.Month;
                List<Amortizacion> listAmortizacion = new List<Amortizacion>();
                new CN_Amortizacion().ConsultaAmortizacionCliente(amortizacion, sesion.Emp_Cnx, ref listAmortizacion);
                //foreach (string Id_Prd in arrayId_Prd)
                //{
                    float montoAmortizacion = 0;
                    foreach (Amortizacion amor in listAmortizacion)
                    {

                                //si el año y mes actual es mayor al año y mes de la amortizacion del producto
                                //la amortizacion se queda en 0
                                int mesesAmortizacion = ((amor.Amo_AnioFin - amor.Amo_AnioInicio) * 12) + (amor.Amo_MesFin - amor.Amo_MesInicio) - 1;

                                float importeTotalAmortizacion = amor.Amo_Cant * amor.Amo_Costo;
                                montoAmortizacion = importeTotalAmortizacion;   ///importeTotalAmortizacion / mesesAmortizacion;
                                AmortizacionTotal += montoAmortizacion;
                    }
                //}

                //calcular financiamiento de proveedores
                double financiamientoProveedores = (((((CostoMaterial / 30) * cd.Cd_Dias.Value) / cd.Cd_Dias.Value) * cd.Cd_DiasFinanciaProv) * (1 + (Convert.ToSingle(cd.Cd_Iva) / 100)));
                if (double.IsNaN(financiamientoProveedores) || double.IsInfinity(financiamientoProveedores))
                {
                    financiamientoProveedores = 0;
                }

                //calcular inversion total en activos
                double inversionTotalActivos
                    = (((VentaNeta / 30) * DiasRotacion) * (1 + (Convert.ToSingle(cd.Cd_Iva) / 100)))
                    + ((CostoMaterial / 30) * cd.Cd_Dias.Value)
                    + ((CostoMaterial / 30) * cd.Cd_DiasInv.Value)
                    + (CostoMaterial * Convert.ToSingle(cd.Cd_FactorInvComodato))
                    + ((VentaNeta / 30) * cd.Cd_FactorConvActFijo.Value);
                if (double.IsNaN(inversionTotalActivos) || double.IsInfinity(inversionTotalActivos))
                {
                    inversionTotalActivos = 0;
                }

                //calcular utilidad bruta
                UtilidadBruta =
                    VentaNeta
                    - CostoMaterial
                    - Cte_CarMP
                    - (CostoMaterial * (Convert.ToSingle(cd.Cd_Flete) / 100)) //flete
                    - AmortizacionTotal
                    - Prd_PesConTecnico;
                if (double.IsNaN(UtilidadBruta) || double.IsInfinity(UtilidadBruta))
                {
                    UtilidadBruta = 0;
                }
                //calcular utilidad marginal
                double UtilidadMarginal =
                    UtilidadBruta
                    - (UtilidadBruta * (Convert.ToSingle(cd.Cd_ComisionRik) / 100))
                    - Cte_GasVarT
                    //- (VentaNeta * (Convert.ToSingle(cd.Cd_OtrosGastosVar) / 100))
                    - Cte_FletePaga;
                if (double.IsNaN(UtilidadMarginal) || double.IsInfinity(UtilidadMarginal))
                {
                    UtilidadMarginal = 0;
                }

                //calcular Uafir mensual
                double UafirMensual =
                    UtilidadMarginal
                    - (VentaNetaOtros * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosOtros) / 100))
                   - (VentaNetaPapel * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosPapel) / 100))
                    - (VentaNeta * (Convert.ToSingle(cd.Cd_CargoUCS) / 100));
                if (double.IsNaN(UafirMensual) || double.IsInfinity(UafirMensual))
                {
                    UafirMensual = 0;
                }

                //calcular Costo de capital
                double CostoCapital = (Math.Round(inversionTotalActivos, 2) - financiamientoProveedores) * (cd.Cd_TasaCetes.Value + cd.Cd_TasaIncCostoCapital.Value) / 100;
                if (double.IsNaN(CostoCapital) || double.IsInfinity(CostoCapital))
                {
                    CostoCapital = 0;
                }
                //calcular Uafir después de impuestos
                double UafirDespuesImpuestos = (UafirMensual * 12) - ((UafirMensual * 12) * (Convert.ToSingle(cd.Cd_ISRyPTU) / 100));
                if (double.IsNaN(UafirDespuesImpuestos) || double.IsInfinity(UafirDespuesImpuestos))
                {
                    UafirDespuesImpuestos = 0;
                }


                //calcular porcentaje de utilidad remanente
                double UtilidadRemanentePorc = (UafirDespuesImpuestos / (inversionTotalActivos - financiamientoProveedores) * 100) - (cd.Cd_TasaCetes.Value + cd.Cd_TasaIncCostoCapital.Value);
                if (double.IsNaN(UtilidadRemanentePorc) || double.IsInfinity(UtilidadRemanentePorc))
                {
                    UtilidadRemanentePorc = 0;
                }

                ALValorParametrosInternos.Add(DiasRotacion); //txtCtaCobrarPorc
                ALValorParametrosInternos.Add(cd.Cd_Dias); //"txtInvDiasCant"
                ALValorParametrosInternos.Add(cd.Cd_DiasInv); //"txtInvConsigDiasCant"
                ALValorParametrosInternos.Add(UtilidadRemanentePorc / 100); //"txtUtilidadRemanentePorc"

                double ctaPorCobrar = ((VentaNeta / 30) * DiasRotacion) * (1 + (Convert.ToSingle(cd.Cd_Iva) / 100));
                if (double.IsNaN(ctaPorCobrar) || double.IsInfinity(ctaPorCobrar))
                {
                    ctaPorCobrar = 0;
                }
                ALValorParametrosInternos.Add(ctaPorCobrar); //"txtCtaCobrar"

                ALValorParametrosInternos.Add((CostoMaterial / 30) * cd.Cd_Dias); //"txtInvDias"
                ALValorParametrosInternos.Add(cd.Cd_FactorInvComodato); //"txtInvComodatoOtrosProdCant"
                ALValorParametrosInternos.Add(CostoMaterial * Convert.ToSingle(cd.Cd_FactorInvComodato)); //"txtInvComodatoOtrosProd"
                ALValorParametrosInternos.Add((CostoMaterial / 30) * cd.Cd_DiasInv); //"txtInvConsigDias"

                ALValorParametrosInternos.Add(financiamientoProveedores); //"txtFinanProv"

                ALValorParametrosInternos.Add(inversionTotalActivos - financiamientoProveedores); //"txtInvActivosNetos" OP'N
                ALValorParametrosInternos.Add(inversionTotalActivos); //"txtInvTotalActivos"
                ALValorParametrosInternos.Add((VentaNeta / 30) * cd.Cd_FactorConvActFijo); //"txtInvActivosFijos"
                ALValorParametrosInternos.Add(UtilidadRemanentePorc / 100 * (inversionTotalActivos - financiamientoProveedores)); //"txtUtilidadRemanente"

                double txtUafirActivos = UafirDespuesImpuestos / (inversionTotalActivos - financiamientoProveedores) * 100;
                if (double.IsNaN(txtUafirActivos) || double.IsInfinity(txtUafirActivos))
                {
                    txtUafirActivos = 0;
                }
                ALValorParametrosInternos.Add(txtUafirActivos / 100); //"txtUafirActivos"

                ALValorParametrosInternos.Add(Convert.ToSingle(cd.Cd_TasaCetes + cd.Cd_TasaIncCostoCapital) / 100); //"txtCostoCapital"
                ALValorParametrosInternos.Add(VentaNeta); //"txtVentaNetaMon"
                ALValorParametrosInternos.Add(CostoMaterial); //"txtCostoMaterialMon"
                ALValorParametrosInternos.Add(CostoMaterial * (Convert.ToSingle(cd.Cd_Flete) / 100)); //"txtFleteMon"
                ALValorParametrosInternos.Add(Cte_CarMP); //"txtManoObraMon"

                ALValorParametrosInternos.Add(UtilidadBruta); //"txtUtilidadMon"
                ALValorParametrosInternos.Add(Prd_PesConTecnico); //"txtCostoServEquipoMon"
                ALValorParametrosInternos.Add(AmortizacionTotal); //"txtAmortizacionMon"
                ALValorParametrosInternos.Add(UtilidadBruta * (Convert.ToSingle(cd.Cd_ComisionRik) / 100)); //"txtComisionRepMon"
                ALValorParametrosInternos.Add(VentaNetaOtros * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosOtros) / 100)); //"txtContribucionGastosFijosOtrosMon"
                ALValorParametrosInternos.Add(VentaNetaPapel * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosPapel) / 100)); //"txtContribucionGastosFijosPapelMon"
                ALValorParametrosInternos.Add(UafirMensual); //"txtUafirMensualMon"
                ALValorParametrosInternos.Add(VentaNeta * (Convert.ToSingle(cd.Cd_CargoUCS) / 100)); //"txtCargoUCSMon"
                ALValorParametrosInternos.Add(Cte_FletePaga); //"txtFletesPagadosMon"
                ALValorParametrosInternos.Add(0); //"txtOtrosGastosVariablesMon"  VentaNeta * (Convert.ToSingle(cd.Cd_OtrosGastosVar) / 100)
                ALValorParametrosInternos.Add(Cte_GasVarT); //"txtGastosVariablesMon"
                ALValorParametrosInternos.Add(UafirMensual * 12); //"txtUafirAnualMon"

                ALValorParametrosInternos.Add(CostoCapital); //"txtCostoCapitalMon"

                ALValorParametrosInternos.Add(UafirDespuesImpuestos - CostoCapital); //"txtUtilidadRemanenteMon"
                ALValorParametrosInternos.Add(UafirDespuesImpuestos); //"txtUafirDespuesImpMon"
                ALValorParametrosInternos.Add(cd.Cd_ISRyPTU); //"txtISRyPTU"
                double txtISRyPTUMon = (UafirMensual * 12) * (Convert.ToSingle(cd.Cd_ISRyPTU) / 100);
                if (double.IsNaN(txtISRyPTUMon) || double.IsInfinity(txtISRyPTUMon))
                {
                    txtISRyPTUMon = 0;
                }
                ALValorParametrosInternos.Add(txtISRyPTUMon); //"txtISRyPTUMon"

                //ALValorParametrosInternos.Add(-1); //"txtISRyPTUPorc"
                //ALValorParametrosInternos.Add(-1); //"txtUafirDespuesImpPorc"
                //ALValorParametrosInternos.Add(-1); //"txtUtilidadRemanentePorc2"
                //ALValorParametrosInternos.Add(-1); //"txtCostoCapitalPorc"
                //ALValorParametrosInternos.Add(-1); //"txtUafirAnualPorc"
                double txtGastosVariablesPorc = (Cte_GasVarT / VentaNeta) * 100;
                if (double.IsNaN(txtGastosVariablesPorc) || double.IsInfinity(txtGastosVariablesPorc))
                {
                    txtGastosVariablesPorc = 0;
                }
                ALValorParametrosInternos.Add(txtGastosVariablesPorc / 100); //"txtGastosVariablesPorc"
                double txtOtrosGastosVariablesPorc = 0; //((VentaNeta * (Convert.ToSingle(cd.Cd_OtrosGastosVar) / 100)) / VentaNeta) * 100;
                if (double.IsNaN(txtOtrosGastosVariablesPorc) || double.IsInfinity(txtOtrosGastosVariablesPorc))
                {
                    txtOtrosGastosVariablesPorc = 0;
                }
                ALValorParametrosInternos.Add(0); //"txtOtrosGastosVariablesPorc"  txtOtrosGastosVariablesPorc / 100)
                double txtFletesPagadosPorc = (Cte_FletePaga / VentaNeta) * 100;
                if (double.IsNaN(txtFletesPagadosPorc) || double.IsInfinity(txtFletesPagadosPorc))
                {
                    txtFletesPagadosPorc = 0;
                }
                ALValorParametrosInternos.Add(txtFletesPagadosPorc / 100); //"txtFletesPagadosPorc"
                double txtCargoUCSPorc = ((VentaNeta * (Convert.ToSingle(cd.Cd_CargoUCS) / 100)) / VentaNeta) * 100;
                if (double.IsNaN(txtCargoUCSPorc) || double.IsInfinity(txtCargoUCSPorc))
                {
                    txtCargoUCSPorc = 0;
                }
                ALValorParametrosInternos.Add(txtCargoUCSPorc / 100); //"txtCargoUCSPorc"
                double txtUafirMensualPorc = (UafirMensual / VentaNeta) * 100;
                if (double.IsNaN(txtUafirMensualPorc) || double.IsInfinity(txtUafirMensualPorc))
                {
                    txtUafirMensualPorc = 0;
                }
                ALValorParametrosInternos.Add(txtUafirMensualPorc / 100); //"txtUafirMensualPorc"
                double txtContribucionGastosFijosPapelPorc = ((VentaNetaPapel * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosPapel) / 100)) / VentaNeta) * 100;
                if (double.IsNaN(txtContribucionGastosFijosPapelPorc) || double.IsInfinity(txtContribucionGastosFijosPapelPorc))
                {
                    txtContribucionGastosFijosPapelPorc = 0;
                }
                ALValorParametrosInternos.Add(txtContribucionGastosFijosPapelPorc/100); //"txtContribucionGastosFijosPapelPorc"
                double txtContribucionGastosFijosOtrosPorc = ((VentaNetaOtros * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosOtros) / 100)) / VentaNeta) * 100;
                if (double.IsNaN(txtContribucionGastosFijosOtrosPorc) || double.IsInfinity(txtContribucionGastosFijosOtrosPorc))
                {
                    txtContribucionGastosFijosOtrosPorc = 0;
                }
                ALValorParametrosInternos.Add(txtContribucionGastosFijosOtrosPorc/100); //"txtContribucionGastosFijosOtrosPorc"
                double txtAmortizacionPorc = (AmortizacionTotal / VentaNeta) * 100;
                if (double.IsNaN(txtAmortizacionPorc) || double.IsInfinity(txtAmortizacionPorc))
                {
                    txtAmortizacionPorc = 0;
                }
                ALValorParametrosInternos.Add(txtAmortizacionPorc / 100); //"txtAmortizacionPorc"
                double txtCostoServEquipoPorc = (Prd_PesConTecnico / VentaNeta) * 100;
                if (double.IsNaN(txtCostoServEquipoPorc) || double.IsInfinity(txtCostoServEquipoPorc))
                {
                    txtCostoServEquipoPorc = 0;
                }
                ALValorParametrosInternos.Add(txtCostoServEquipoPorc / 100); //"txtCostoServEquipoPorc"
                double txtComisionRepPorc = ((UtilidadBruta * (Convert.ToSingle(cd.Cd_ComisionRik) / 100)) / VentaNeta) * 100;
                if (double.IsNaN(txtComisionRepPorc) || double.IsInfinity(txtComisionRepPorc))
                {
                    txtComisionRepPorc = 0;
                }
                ALValorParametrosInternos.Add(txtComisionRepPorc / 100); //"txtComisionRepPorc"
                double txtUtilidadPorc = (UtilidadBruta / VentaNeta) * 100;
                if (double.IsNaN(txtUtilidadPorc) || double.IsInfinity(txtUtilidadPorc))
                {
                    txtUtilidadPorc = 0;
                }
                ALValorParametrosInternos.Add(txtUtilidadPorc / 100); //"txtUtilidadPorc"
                double txtManoObraPorc = (Cte_CarMP / VentaNeta) * 100;
                if (double.IsNaN(txtManoObraPorc) || double.IsInfinity(txtManoObraPorc))
                {
                    txtManoObraPorc = 0;
                }
                ALValorParametrosInternos.Add(txtManoObraPorc / 100); //"txtManoObraPorc"

                ALValorParametrosInternos.Add(cd.Cd_Flete); //"txtFletePorc"

                double txtFletePorc2 = ((CostoMaterial * (Convert.ToSingle(cd.Cd_Flete) / 100)) / VentaNeta) * 100;
                if (double.IsNaN(txtFletePorc2) || double.IsInfinity(txtFletePorc2))
                {
                    txtFletePorc2 = 0;
                }
                ALValorParametrosInternos.Add(txtFletePorc2 / 100); //"txtFletePorc2"
                double txtCostoMaterialPorc = (CostoMaterial / VentaNeta) * 100;
                if (double.IsNaN(txtCostoMaterialPorc) || double.IsInfinity(txtCostoMaterialPorc))
                {
                    txtCostoMaterialPorc = 0;
                }
                ALValorParametrosInternos.Add(txtCostoMaterialPorc / 100); //"txtCostoMaterialPorc"


                ALValorParametrosInternos.Add(UtilidadMarginal); //"txtUtilidadMarginalMon"
                double txtUtilidadMarginalPorc = (UtilidadMarginal / VentaNeta) * 100;
                if (double.IsNaN(txtUtilidadMarginalPorc) || double.IsInfinity(txtUtilidadMarginalPorc))
                {
                    txtUtilidadMarginalPorc = 0;
                }
                ALValorParametrosInternos.Add(txtUtilidadMarginalPorc / 100); //"txtUtilidadMarginalPorc"

                Type instance = null;
                if (a_pantalla)
                {
                    instance = typeof(LibreriaReportes.Rep_RentabilidadClientes);
                }
                else
                {
                    instance = typeof(LibreriaReportes.ExpRep_RentabilidadClientes);
                }
                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;

                //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                if (a_pantalla)
                {
                    RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
                }
                else
                {
                    ImprimirXLS(ALValorParametrosInternos, instance);
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

                RAM1.ResponseScripts.Add("startDownload('" + instance.Name + ".xls');");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Inicializar()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            this.CargarCentros();
            List<Territorios> listaTerr = new List<Territorios>();
            Territorios ter = new Territorios();
            ter.Descripcion = "-- Seleccionar --";
            ter.Id_Ter = -1;
            listaTerr.Insert(0, ter);
            txtTerritorio.Text = string.Empty;

            //this.CargarComboTerritorios(listaTerr);
        }
        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];

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
        //private void CargarCliente()
        //{
        //    try
        //    {
        //        Sesion Sesion = new Sesion();
        //        Sesion = (Sesion)Session["Sesion" + Session.SessionID];
        //        CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
        //        CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatCliente_Combo", ref cmbCliente);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        
        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("Impresion_error"))
                    Alerta("Error al momento de imprimir");
                else
                    if (mensaje.Contains("cmbCliente_IndexChanging_error"))
                        Alerta("Error al momento de consultar los datos del cliente");
                    else
                        Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
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
    }
}