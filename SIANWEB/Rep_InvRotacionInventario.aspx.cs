using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Collections;
using CapaDatos;
using Telerik.Reporting.Processing;
using System.IO;
using Telerik.Web.UI;
 
using System.Data;
using OfficeOpenXml;


namespace SIANWEB
{
    public partial class Rep_InvRotacionInventario : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
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
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (sesion == null)
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
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        CargarCentros();
                        Inicializar();
                        dpFecha.DbSelectedDate = sesion.CalendarioFin;

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
        protected void CmbCentro_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                Sesion sesion2 = sesion;
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion2);
                sesion = sesion2;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                string cadena = txtProducto.Text;
                StringBuilder condicion = new StringBuilder("");
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (!Page.IsValid)
                    return;
                if (_PermisoImprimir)
                {
                    if (btn.CommandName == "print")
                    {
                        this.Imprimir(cadena, true);
                    }
                    else
                    {
                        this.Imprimir(cadena, false);
                    }
                }
                else
                    Alerta("No tiene permiso para imprimir");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cmbTipo_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (e.Value == "4")
                {
                    chkDetalle.Visible = true;
                    RowCliente.Visible = true;
                    RowProducto.Visible = true;
                }
                else
                {
                    chkDetalle.Visible = false;
                    RowCliente.Visible = false;
                    RowProducto.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Funciones
        private void CargarCentros()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                if (sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, sesion.Id_Emp, sesion.Id_U, sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(sesion.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = sesion.Id_Cd_Ver.ToString();
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
                CargarTipo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarTipo()
        {
            cmbTipo.Items.Clear();
            cmbTipo.Items.Add(new Telerik.Web.UI.RadComboBoxItem("-- Seleccionar --", "-1"));
            cmbTipo.Items.Add(new Telerik.Web.UI.RadComboBoxItem("General", "1"));
            cmbTipo.Items.Add(new Telerik.Web.UI.RadComboBoxItem("Producto", "2"));
            cmbTipo.Items.Add(new Telerik.Web.UI.RadComboBoxItem("Sistemas propietarios", "3"));
            cmbTipo.Items.Add(new Telerik.Web.UI.RadComboBoxItem("Producto en consignación", "4"));
            cmbTipo.Items.Add(new Telerik.Web.UI.RadComboBoxItem("Papel", "5"));
            cmbTipo.Sort = Telerik.Web.UI.RadComboBoxSort.Ascending;
            cmbTipo.SortItems();
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
                CD_PermisosU CN_PermisosU = new CD_PermisosU();
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
        private void Imprimir(string CondicionStr, bool a_pantalla)
        {
            CN__Comun cn_comun = new CN__Comun();
            string resp = cn_comun.ValidarRango(txtProducto.Text);
            if (resp != "")
            {
                Alerta("El rango " + resp + " no es válido");
                return;
            }

            ArrayList ALValorParametrosInternos = new ArrayList();
            Funciones funcion = new Funciones();
            ALValorParametrosInternos.Add(sesion.Emp_Cnx);
            ALValorParametrosInternos.Add(sesion.Id_Emp);
            ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
            ALValorParametrosInternos.Add(CondicionStr == "" ? null : CondicionStr);
            ALValorParametrosInternos.Add(cmbTipo.SelectedItem.Text);
            ALValorParametrosInternos.Add(dpFecha.SelectedDate);
            ALValorParametrosInternos.Add(sesion.U_Nombre);
            ALValorParametrosInternos.Add(sesion.Emp_Nombre);
            ALValorParametrosInternos.Add(sesion.Cd_Nombre);
            ALValorParametrosInternos.Add(funcion.GetLocalDateTime(sesion.Minutos));

            Type instance = null;

            switch (cmbTipo.SelectedValue)
            {
                case "1":
                    if (a_pantalla)
                    {
                        instance = typeof(LibreriaReportes.Rep_InvRotacion);
                    }
                    else
                    {
                        instance = typeof(LibreriaReportes.ExpRep_InvRotacion);
                    }
                    break;
                case "2":
                    if (a_pantalla)
                    {
                        instance = typeof(LibreriaReportes.Rep_InvRotacion2);
                    }
                    else
                    {
                        instance = typeof(LibreriaReportes.ExpRep_InvRotacion2);
                    }
                    break;
                case "3":
                    if (a_pantalla)
                    {
                        instance = typeof(LibreriaReportes.Rep_InvRotacion3);
                    }
                    else
                    {
                        instance = typeof(LibreriaReportes.ExpRep_InvRotacion3);
                    }
                    break;
                case "4":
                    ALValorParametrosInternos.Add(txtCliente.Text);
                    if (chkDetalle.Checked)
                    {

                        if (a_pantalla)
                        {
                            instance = typeof(LibreriaReportes.Rep_InvRotacion4a);
                        }
                        else
                        {
                            instance = typeof(LibreriaReportes.ExpRep_InvRotacion4a);
                            instance = typeof(LibreriaReportes.ExpRep_InvRotacion4Consig);
                            
                        }
                    }
                    else
                    {
                        if (a_pantalla)
                        {
                            instance = typeof(LibreriaReportes.Rep_InvRotacion4b);
                        }
                        else
                        {
                            instance = typeof(LibreriaReportes.ExpRep_InvRotacion4b);
                            instance = typeof(LibreriaReportes.ExpRep_InvRotacion4Consig);
                        }
                    }
                    break;
                case "5":
                    if (a_pantalla)
                    {
                        instance = typeof(LibreriaReportes.Rep_InvRotacion5);
                    }
                    else
                    {
                        instance = typeof(LibreriaReportes.ExpRep_InvRotacion5);
                    }
                    break;
            }



            //NOTA: El estatus de impresión, lo pone cuando el reporte se carga

            if (a_pantalla)
            {
                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;
                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
            }
            else
            {
                ImprimirXLS(ALValorParametrosInternos, instance);
            }
            // RAM1.ResponseScripts.Add("refreshGrid();");
            //rgPago.Rebind();
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

        //JFCV 25 Abril del 2018 
        //probar con esta rutina para ver si se puede modificar el excell para agregar una pestaña con los datos 
        // del reporte de consignación 
        // también me serviría para el reporte general 

        //private void ExportarRotacionConsignacion()
        //{
        //    System.IO.StreamWriter sw = null;
        //    string ruta = null;
        //    Random rnd = new Random();

        //    int nro = rnd.Next(0, 8);
        //    string tipo = "RotacionInvConsignacion";
        //    ruta = Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xlsx";

        //    if (File.Exists(ruta))
        //        File.Delete(ruta);
        //    if (File.Exists(Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xls"))
        //        File.Delete(Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xls");



        //    List<InvRotacion> List = new List<InvRotacion>();
        //    InvRotacion InvRotacion = new InvRotacion();
        //    CN_InvRotacion CN_InvRotacion = new CN_InvRotacion();


        //    InvRotacion.Id_Emp = 1;
        //    InvRotacion.Ano = Int32.Parse(RadComboAno.SelectedValue);
        //    InvRotacion.Mes = Int32.Parse(RadComboMes.SelectedValue);


        //    int Antepenultimo = (InvRotacion.Mes - 2 == -1) ? 11 : InvRotacion.Mes - 2;
        //    int Penultimo = (InvRotacion.Mes - 1 == 0) ? 12 : InvRotacion.Mes - 1;
        //    int Ultimo = InvRotacion.Mes;

        //    CN_InvRotacion.Consulta(InvRotacion, Emp_CnxCen, ref  List);

        //    if ((List != null))
        //    {
        //        if (!(List.Count() == 0))
        //        {


        //            using (ExcelPackage p = new ExcelPackage())
        //            {

        //                //set the workbook properties and add a default sheet in it
        //                SetWorkbookProperties(p);
        //                //Create a sheet


        //                List<int> lCDI = List.Select(i => i.Id_Cd).Distinct().ToList();
        //                List<RemisionTotal> lCDTOTAL = new List<RemisionTotal>();
        //                int count = 1;

        //                foreach (int a in lCDI)
        //                {
        //                    DataTable dataTable = new DataTable();
        //                    DataColumn dcCDS = new DataColumn("Id_Cd", Type.GetType("System.Int32"));
        //                    DataColumn dcCte = new DataColumn("Id_Cte", Type.GetType("System.Int32"));
        //                    DataColumn dcCteNom = new DataColumn("Cte_NomComercial", Type.GetType("System.String"));
        //                    DataColumn dcIdPrd = new DataColumn("Id_Prd", Type.GetType("System.String"));
        //                    DataColumn dcDescripcion = new DataColumn("Prd_Descripcion", Type.GetType("System.String"));
        //                    DataColumn dcPresentacion = new DataColumn("Prd_Presentacion", Type.GetType("System.String"));
        //                    DataColumn dcUni = new DataColumn("Unidad", Type.GetType("System.String"));
        //                    DataColumn dcInvFinal = new DataColumn("Prd_InvFinal", Type.GetType("System.Int32"));
        //                    DataColumn dcPrecioAAA = new DataColumn("Prd_PrecioAAA", Type.GetType("System.Double"));
        //                    DataColumn dcImporteInventario = new DataColumn("ImporteInventario", Type.GetType("System.Double"));
        //                    DataColumn dcAntepenultimo = new DataColumn(getMonthName(Antepenultimo), Type.GetType("System.Int32"));
        //                    DataColumn dcPenultimo = new DataColumn(getMonthName(Penultimo), Type.GetType("System.Int32"));
        //                    DataColumn dcUltimo = new DataColumn(getMonthName(Ultimo), Type.GetType("System.Int32"));
        //                    DataColumn dcPromedio = new DataColumn("Promedio", Type.GetType("System.Double"));
        //                    DataColumn dcCostoPromedio = new DataColumn("CostoPromedio", Type.GetType("System.Double"));
        //                    DataColumn dcRotacion = new DataColumn("Rotacion", Type.GetType("System.String"));
        //                    DataColumn dcVigente = new DataColumn("Vigente", Type.GetType("System.Double"));
        //                    DataColumn dcVencido = new DataColumn("Vencido", Type.GetType("System.Double"));

        //                    dataTable.Columns.Add(dcCDS);
        //                    dataTable.Columns.Add(dcCte);
        //                    dataTable.Columns.Add(dcCteNom);
        //                    dataTable.Columns.Add(dcIdPrd);
        //                    dataTable.Columns.Add(dcDescripcion);
        //                    dataTable.Columns.Add(dcPresentacion);
        //                    dataTable.Columns.Add(dcUni);

        //                    dataTable.Columns.Add(dcInvFinal);
        //                    dataTable.Columns.Add(dcPrecioAAA);
        //                    dataTable.Columns.Add(dcImporteInventario);
        //                    dataTable.Columns.Add(dcAntepenultimo);
        //                    dataTable.Columns.Add(dcPenultimo);
        //                    dataTable.Columns.Add(dcUltimo);
        //                    dataTable.Columns.Add(dcPromedio);
        //                    dataTable.Columns.Add(dcCostoPromedio);
        //                    dataTable.Columns.Add(dcRotacion);

        //                    dataTable.Columns.Add(dcVigente);
        //                    dataTable.Columns.Add(dcVencido);

        //                    ExcelWorksheet ws = CreateSheet(p, getCDIName(a), count);

        //                    double TOTAL = 0; double TOTAL_VIGENTE = 0; double TOTAL_VENCIDO = 0; double TOTAL_IMPORTE = 0;
        //                    foreach (InvRotacion inv in List.FindAll(b => b.Id_Cd == a))
        //                    {
        //                        DataRow drFila = null;
        //                        drFila = dataTable.NewRow();
        //                        drFila["Id_Cd"] = inv.Id_Cd;
        //                        drFila["Id_Cte"] = inv.Id_Cte;
        //                        drFila["Cte_NomComercial"] = inv.Cte_NomComercial;
        //                        drFila["Id_Prd"] = inv.Id_Prd;
        //                        drFila["Prd_Descripcion"] = inv.Prd_Descripcion;
        //                        drFila["Prd_Presentacion"] = inv.Prd_Presentacion;
        //                        drFila["Unidad"] = inv.Id_Uni;
        //                        drFila["Prd_InvFinal"] = inv.Prd_InvFinal;
        //                        drFila["Prd_PrecioAAA"] = inv.Prd_PrecioAAA;
        //                        drFila["ImporteInventario"] = inv.ImporteInventario;
        //                        drFila[getMonthName(Antepenultimo)] = inv.Antepenultimo;
        //                        drFila[getMonthName(Penultimo)] = inv.Penultimo;
        //                        drFila[getMonthName(Ultimo)] = inv.Ultimo;
        //                        inv.Vigente = inv.Vigente > 0 ? inv.Vigente : 0;
        //                        inv.Vencido = inv.Vencido > 0 ? inv.Vencido : 0;
        //                        drFila["Promedio"] = inv.Promedio;
        //                        drFila["CostoPromedio"] = inv.CostoPromedio;
        //                        drFila["Rotacion"] = inv.Rotacion;
        //                        drFila["Vigente"] = inv.Vigente;
        //                        drFila["Vencido"] = inv.Vencido;


        //                        dataTable.Rows.Add(drFila);
        //                        dataTable.AcceptChanges();
        //                        TOTAL = TOTAL + inv.CostoPromedio;
        //                        TOTAL_IMPORTE = TOTAL_IMPORTE + inv.ImporteInventario;
        //                        TOTAL_VIGENTE = TOTAL_VIGENTE + inv.Vigente;
        //                        TOTAL_VENCIDO = TOTAL_VENCIDO + inv.Vencido;

        //                    }
        //                    RemisionTotal RemisionT = new RemisionTotal();
        //                    RemisionT.Id_Cd = a;
        //                    RemisionT.TotalImporte = TOTAL_IMPORTE;
        //                    RemisionT.Total = TOTAL;
        //                    RemisionT.TotalVigente = TOTAL_VIGENTE;
        //                    RemisionT.TotalVencido = TOTAL_VENCIDO;
        //                    lCDTOTAL.Add(RemisionT);



        //                    //Merging cells and create a center heading for out table
        //                    ws.Cells[1, 1].Value = "ROTACION DE INVENTARIOS DE PRODUCTO EN CONSIGNACION";
        //                    ws.Cells[1, 1, 1, dataTable.Columns.Count].Merge = true;
        //                    ws.Cells[1, 1, 1, dataTable.Columns.Count].Style.Font.Bold = true;
        //                    ws.Cells[1, 1, 1, dataTable.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



        //                    int rowIndex = 4;

        //                    CreateHeader(ws, ref rowIndex, dataTable);
        //                    CreateData(ws, ref rowIndex, dataTable);
        //                    // CreateFooter(ws, ref rowIndex, dt);*/

        //                    count++;
        //                }


        //                ExcelWorksheet wsGral = CreateSheet(p, "GENERAL", lCDTOTAL.Count() + 1);

        //                System.Data.DataTable dataTableGral = new DataTable();
        //                DataColumn dcCDSGral = new DataColumn("Id_Cd", Type.GetType("System.Int32"));
        //                DataColumn dcCteGral = new DataColumn("Id_Cte", Type.GetType("System.Int32"));
        //                DataColumn dcCteNomGral = new DataColumn("Cte_NomComercial", Type.GetType("System.String"));
        //                DataColumn dcIdPrdGral = new DataColumn("Id_Prd", Type.GetType("System.String"));
        //                DataColumn dcDescripcionGral = new DataColumn("Prd_Descripcion", Type.GetType("System.String"));
        //                DataColumn dcPresentacionGral = new DataColumn("Prd_Presentacion", Type.GetType("System.String"));
        //                DataColumn dcUniGral = new DataColumn("Unidad", Type.GetType("System.String"));
        //                DataColumn dcInvFinalGral = new DataColumn("Prd_InvFinal", Type.GetType("System.Int32"));
        //                DataColumn dcPrecioAAAGral = new DataColumn("Prd_PrecioAAA", Type.GetType("System.Double"));
        //                DataColumn dcImporteInventarioGral = new DataColumn("ImporteInventario", Type.GetType("System.Double"));
        //                DataColumn dcAntepenultimoGral = new DataColumn(getMonthName(Antepenultimo), Type.GetType("System.Int32"));
        //                DataColumn dcPenultimoGral = new DataColumn(getMonthName(Penultimo), Type.GetType("System.Int32"));
        //                DataColumn dcUltimoGral = new DataColumn(getMonthName(Ultimo), Type.GetType("System.Int32"));
        //                DataColumn dcPromedioGral = new DataColumn("Promedio", Type.GetType("System.Double"));
        //                DataColumn dcCostoPromedioGral = new DataColumn("CostoPromedio", Type.GetType("System.Double"));
        //                DataColumn dcRotacionGral = new DataColumn("Rotacion", Type.GetType("System.Double"));
        //                DataColumn dcVigenteGral = new DataColumn("Vigente", Type.GetType("System.Double"));
        //                DataColumn dcVencidoGral = new DataColumn("Vencido", Type.GetType("System.Double"));


        //                dataTableGral.Columns.Add(dcCDSGral);
        //                dataTableGral.Columns.Add(dcCteGral);
        //                dataTableGral.Columns.Add(dcCteNomGral);
        //                dataTableGral.Columns.Add(dcIdPrdGral);
        //                dataTableGral.Columns.Add(dcDescripcionGral);
        //                dataTableGral.Columns.Add(dcPresentacionGral);
        //                dataTableGral.Columns.Add(dcUniGral);
        //                dataTableGral.Columns.Add(dcInvFinalGral);
        //                dataTableGral.Columns.Add(dcPrecioAAAGral);
        //                dataTableGral.Columns.Add(dcImporteInventarioGral);
        //                dataTableGral.Columns.Add(dcAntepenultimoGral);
        //                dataTableGral.Columns.Add(dcPenultimoGral);
        //                dataTableGral.Columns.Add(dcUltimoGral);
        //                dataTableGral.Columns.Add(dcPromedioGral);
        //                dataTableGral.Columns.Add(dcCostoPromedioGral);
        //                dataTableGral.Columns.Add(dcRotacionGral);
        //                dataTableGral.Columns.Add(dcVigenteGral);
        //                dataTableGral.Columns.Add(dcVencidoGral);



        //                //Merging cells and create a center heading for out table
        //                wsGral.Cells[1, 1].Value = "ROTACION DE INVENTARIOS DE PRODUCTO EN CONSIGNACION";
        //                wsGral.Cells[1, 1, 1, 4].Merge = true;
        //                wsGral.Cells[1, 1, 1, 4].Style.Font.Bold = true;
        //                wsGral.Cells[1, 1, 1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


        //                foreach (InvRotacion inv in List)
        //                {
        //                    DataRow drFila = null;
        //                    drFila = dataTableGral.NewRow();
        //                    drFila["Id_Cd"] = inv.Id_Cd;
        //                    drFila["Id_Cte"] = inv.Id_Cte;
        //                    drFila["Cte_NomComercial"] = inv.Cte_NomComercial;
        //                    drFila["Id_Prd"] = inv.Id_Prd;
        //                    drFila["Prd_Descripcion"] = inv.Prd_Descripcion;
        //                    drFila["Prd_Presentacion"] = inv.Prd_Presentacion;
        //                    drFila["Unidad"] = inv.Id_Uni;
        //                    drFila["Prd_InvFinal"] = inv.Prd_InvFinal;
        //                    drFila["Prd_PrecioAAA"] = inv.Prd_PrecioAAA;
        //                    drFila["ImporteInventario"] = inv.ImporteInventario;
        //                    drFila[getMonthName(Antepenultimo)] = inv.Antepenultimo;
        //                    drFila[getMonthName(Penultimo)] = inv.Penultimo;
        //                    drFila[getMonthName(Ultimo)] = inv.Ultimo;
        //                    drFila["Promedio"] = inv.Promedio;
        //                    drFila["CostoPromedio"] = inv.CostoPromedio;
        //                    drFila["Rotacion"] = inv.Rotacion;
        //                    inv.Vigente = inv.Vigente > 0 ? inv.Vigente : 0;
        //                    inv.Vencido = inv.Vencido > 0 ? inv.Vencido : 0;
        //                    drFila["Vigente"] = inv.Vigente;
        //                    drFila["Vencido"] = inv.Vencido;


        //                    dataTableGral.Rows.Add(drFila);
        //                    dataTableGral.AcceptChanges();

        //                }

        //                int rowIndex1 = 4;
        //                CreateHeader(wsGral, ref rowIndex1, dataTableGral);
        //                CreateData(wsGral, ref rowIndex1, dataTableGral);


        //                ExcelWorksheet wsResumen = CreateSheet(p, "RESUMEN", lCDTOTAL.Count() + 2);



        //                wsGral.Cells[1, 1].Value = "ROTACION DE INVENTARIOS DE PRODUCTO EN CONSIGNACION";
        //                wsGral.Cells[1, 1, 1, 4].Merge = true;
        //                wsGral.Cells[1, 1, 1, 4].Style.Font.Bold = true;
        //                wsGral.Cells[1, 1, 1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



        //                DataTable dataTableResumen = new DataTable();
        //                DataColumn dcCDSResumen = new DataColumn("CDI", Type.GetType("System.String"));
        //                DataColumn dcTotalImporteResumen = new DataColumn("Total Importe del Inventario", Type.GetType("System.Double"));
        //                DataColumn dcTotalResumen = new DataColumn("Total Costo Promedio", Type.GetType("System.Double"));
        //                DataColumn dcVigenteResumen = new DataColumn("Total Vigente", Type.GetType("System.Double"));
        //                DataColumn dcVencidoResumen = new DataColumn("Total Vencido", Type.GetType("System.Double"));



        //                dataTableResumen.Columns.Add(dcCDSResumen);
        //                dataTableResumen.Columns.Add(dcTotalImporteResumen);
        //                dataTableResumen.Columns.Add(dcTotalResumen);
        //                dataTableResumen.Columns.Add(dcVigenteResumen);
        //                dataTableResumen.Columns.Add(dcVencidoResumen);


        //                int rowIndex2 = 4;
        //                CreateHeader(wsResumen, ref rowIndex2, dataTableResumen);


        //                wsResumen.Cells[5, 2].Value = "MONTERREY";
        //                wsResumen.Cells[5, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 110) > 0)
        //                {
        //                    wsResumen.Cells[5, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 110).TotalImporte;
        //                    wsResumen.Cells[5, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 110).Total;
        //                    wsResumen.Cells[5, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 110).TotalVigente;
        //                    wsResumen.Cells[5, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 110).TotalVencido;

        //                }

        //                wsResumen.Cells[6, 2].Value = "SALTILLO";
        //                wsResumen.Cells[6, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 150) > 0)
        //                {
        //                    wsResumen.Cells[6, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 150).TotalImporte;
        //                    wsResumen.Cells[6, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 150).Total;
        //                    wsResumen.Cells[6, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 150).TotalVigente;
        //                    wsResumen.Cells[6, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 150).TotalVencido;

        //                }


        //                wsResumen.Cells[7, 2].Value = "MATAMOROS";
        //                wsResumen.Cells[7, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 160) > 0)
        //                {
        //                    wsResumen.Cells[7, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 160).TotalImporte;
        //                    wsResumen.Cells[7, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 160).Total;
        //                    wsResumen.Cells[7, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 160).TotalVigente;
        //                    wsResumen.Cells[7, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 160).TotalVencido;

        //                }


        //                wsResumen.Cells[8, 2].Value = "TORREON";
        //                wsResumen.Cells[8, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 170) > 0)
        //                {
        //                    wsResumen.Cells[8, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 170).TotalImporte;
        //                    wsResumen.Cells[8, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 170).Total;
        //                    wsResumen.Cells[8, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 170).TotalVigente;
        //                    wsResumen.Cells[8, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 170).TotalVencido;
        //                }



        //                wsResumen.Cells[9, 2].Value = "LAREDO";
        //                wsResumen.Cells[9, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 180) > 0)
        //                {
        //                    wsResumen.Cells[9, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 180).TotalImporte;
        //                    wsResumen.Cells[9, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 180).Total;
        //                    wsResumen.Cells[9, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 180).TotalVigente;
        //                    wsResumen.Cells[9, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 180).TotalVencido;

        //                }



        //                wsResumen.Cells[10, 2].Value = "LEON";
        //                wsResumen.Cells[10, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 190) > 0)
        //                {
        //                    wsResumen.Cells[10, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 190).TotalImporte;
        //                    wsResumen.Cells[10, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 190).Total;
        //                    wsResumen.Cells[10, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 190).TotalVigente;
        //                    wsResumen.Cells[10, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 190).TotalVencido;

        //                }



        //                wsResumen.Cells[11, 2].Value = "TIJUANA";
        //                wsResumen.Cells[11, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 200) > 0)
        //                {
        //                    wsResumen.Cells[11, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 200).TotalImporte;
        //                    wsResumen.Cells[11, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 200).Total;
        //                    wsResumen.Cells[11, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 200).TotalVigente;
        //                    wsResumen.Cells[11, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 200).TotalVencido;

        //                }

        //                wsResumen.Cells[12, 2].Value = "CHIHUAHUA";
        //                wsResumen.Cells[12, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 210) > 0)
        //                {
        //                    wsResumen.Cells[12, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 210).TotalImporte;
        //                    wsResumen.Cells[12, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 210).Total;
        //                    wsResumen.Cells[12, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 210).TotalVigente;
        //                    wsResumen.Cells[12, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 210).TotalVencido;

        //                }



        //                wsResumen.Cells[13, 2].Value = "SAN LUIS";
        //                wsResumen.Cells[13, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 220) > 0)
        //                {
        //                    wsResumen.Cells[13, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 220).TotalImporte;
        //                    wsResumen.Cells[13, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 220).Total;
        //                    wsResumen.Cells[13, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 220).TotalVigente;
        //                    wsResumen.Cells[13, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 220).TotalVencido;

        //                }




        //                wsResumen.Cells[14, 2].Value = "JUAREZ";
        //                wsResumen.Cells[14, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 230) > 0)
        //                {
        //                    wsResumen.Cells[14, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 230).TotalImporte;
        //                    wsResumen.Cells[14, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 230).Total;
        //                    wsResumen.Cells[14, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 230).TotalVigente;
        //                    wsResumen.Cells[14, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 230).TotalVencido;
        //                }



        //                wsResumen.Cells[15, 2].Value = "AGUASCALIENTES";
        //                wsResumen.Cells[15, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 240) > 0)
        //                {
        //                    wsResumen.Cells[15, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 240).TotalImporte;
        //                    wsResumen.Cells[15, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 240).Total;
        //                    wsResumen.Cells[15, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 240).TotalVigente;
        //                    wsResumen.Cells[15, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 240).TotalVencido;
        //                }



        //                wsResumen.Cells[16, 2].Value = "MEXICO";
        //                wsResumen.Cells[16, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 310) > 0)
        //                {
        //                    wsResumen.Cells[16, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 310).TotalImporte;
        //                    wsResumen.Cells[16, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 310).Total;
        //                    wsResumen.Cells[16, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 310).TotalVigente;
        //                    wsResumen.Cells[16, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 310).TotalVencido;
        //                }



        //                wsResumen.Cells[17, 2].Value = "VERACRUZ";
        //                wsResumen.Cells[17, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 340) > 0)
        //                {
        //                    wsResumen.Cells[17, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 340).TotalImporte;
        //                    wsResumen.Cells[17, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 340).Total;
        //                    wsResumen.Cells[17, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 340).TotalVigente;
        //                    wsResumen.Cells[17, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 340).TotalVencido;
        //                }



        //                wsResumen.Cells[18, 2].Value = "CD CARMEN";
        //                wsResumen.Cells[18, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 350) > 0)
        //                {
        //                    wsResumen.Cells[18, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 350).TotalImporte;
        //                    wsResumen.Cells[18, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 350).Total;
        //                    wsResumen.Cells[18, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 350).TotalVigente;
        //                    wsResumen.Cells[18, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 350).TotalVencido;
        //                }



        //                wsResumen.Cells[19, 2].Value = "MERIDA";
        //                wsResumen.Cells[19, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 360) > 0)
        //                {
        //                    wsResumen.Cells[19, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 360).TotalImporte;
        //                    wsResumen.Cells[19, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 360).Total;
        //                    wsResumen.Cells[19, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 360).TotalVigente;
        //                    wsResumen.Cells[19, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 360).TotalVencido;
        //                }


        //                wsResumen.Cells[20, 2].Value = "CANCUN";
        //                wsResumen.Cells[20, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 370) > 0)
        //                {
        //                    wsResumen.Cells[20, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 370).TotalImporte;
        //                    wsResumen.Cells[20, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 370).Total;
        //                    wsResumen.Cells[20, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 370).TotalVigente;
        //                    wsResumen.Cells[20, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 370).TotalVencido;

        //                }

        //                wsResumen.Cells[21, 2].Value = "RIVIERA";
        //                wsResumen.Cells[21, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 380) > 0)
        //                {
        //                    wsResumen.Cells[21, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 380).TotalImporte;
        //                    wsResumen.Cells[21, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 380).Total;
        //                    wsResumen.Cells[21, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 380).TotalVigente;
        //                    wsResumen.Cells[21, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 380).TotalVencido;
        //                }



        //                wsResumen.Cells[22, 2].Value = "VALLARTA";
        //                wsResumen.Cells[22, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 390) > 0)
        //                {
        //                    wsResumen.Cells[22, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 390).TotalImporte;
        //                    wsResumen.Cells[22, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 390).Total;
        //                    wsResumen.Cells[22, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 390).TotalVigente;
        //                    wsResumen.Cells[22, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 390).TotalVencido;

        //                }



        //                wsResumen.Cells[23, 2].Value = "LOS CABOS";
        //                wsResumen.Cells[23, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 400) > 0)
        //                {
        //                    wsResumen.Cells[23, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 400).TotalImporte;
        //                    wsResumen.Cells[23, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 400).Total;
        //                    wsResumen.Cells[23, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 400).TotalVigente;
        //                    wsResumen.Cells[23, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 400).TotalVencido;
        //                }



        //                wsResumen.Cells[24, 2].Value = "QRO";
        //                wsResumen.Cells[24, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 410) > 0)
        //                {
        //                    wsResumen.Cells[24, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 410).TotalImporte;
        //                    wsResumen.Cells[24, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 410).Total;
        //                    wsResumen.Cells[24, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 410).TotalVigente;
        //                    wsResumen.Cells[24, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 410).TotalVencido;
        //                }



        //                wsResumen.Cells[25, 2].Value = "GDL";
        //                wsResumen.Cells[25, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 430) > 0)
        //                {
        //                    wsResumen.Cells[25, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 430).TotalImporte;
        //                    wsResumen.Cells[25, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 430).Total;
        //                    wsResumen.Cells[25, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 430).TotalVigente;
        //                    wsResumen.Cells[25, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 430).TotalVencido;
        //                }



        //                wsResumen.Cells[26, 2].Value = "PUEBLA";
        //                wsResumen.Cells[26, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 510) > 0)
        //                {
        //                    wsResumen.Cells[26, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 510).TotalImporte;
        //                    wsResumen.Cells[26, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 510).Total;
        //                    wsResumen.Cells[26, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 510).TotalVigente;
        //                    wsResumen.Cells[26, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 510).TotalVencido;

        //                }


        //                wsResumen.Cells[27, 2].Value = "COATZACOALCOS";
        //                wsResumen.Cells[27, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 610) > 0)
        //                {
        //                    wsResumen.Cells[27, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 610).TotalImporte;
        //                    wsResumen.Cells[27, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 610).Total;
        //                    wsResumen.Cells[27, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 610).TotalVigente;
        //                    wsResumen.Cells[27, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 610).TotalVencido;

        //                }


        //                wsResumen.Cells[28, 2].Value = "VILLAHERMOSA";
        //                wsResumen.Cells[28, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 620) > 0)
        //                {
        //                    wsResumen.Cells[28, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 620).TotalImporte;
        //                    wsResumen.Cells[28, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 620).Total;
        //                    wsResumen.Cells[28, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 620).TotalVigente;
        //                    wsResumen.Cells[28, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 620).TotalVencido;

        //                }


        //                wsResumen.Cells[29, 2].Value = "TOLUCA";
        //                wsResumen.Cells[29, 2].Style.Font.Bold = true;
        //                if (lCDTOTAL.Count(a => a.Id_Cd == 640) > 0)
        //                {
        //                    wsResumen.Cells[29, 3].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 640).TotalImporte;
        //                    wsResumen.Cells[29, 4].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 640).Total;
        //                    wsResumen.Cells[29, 5].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 640).TotalVigente;
        //                    wsResumen.Cells[29, 6].Value = lCDTOTAL.FirstOrDefault(a => a.Id_Cd == 640).TotalVencido;
        //                }



        //                ExcelWorksheet wsCDI = CreateSheet(p, "CDI'S", lCDTOTAL.Count() + 3);



        //                //Merging cells and create a center heading for out table
        //                wsCDI.Cells[1, 1].Value = "CENTROS DE DISTRIBUCION";
        //                wsCDI.Cells[1, 1, 1, 4].Merge = true;
        //                wsCDI.Cells[1, 1, 1, 4].Style.Font.Bold = true;
        //                wsCDI.Cells[1, 1, 1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



        //                DataTable dataTableCDI = new DataTable();
        //                DataColumn dcCDSCDI = new DataColumn("CODIGO", Type.GetType("System.String"));
        //                DataColumn dcExcesoCDI = new DataColumn("CDS", Type.GetType("System.String"));



        //                dataTableCDI.Columns.Add(dcCDSCDI);
        //                dataTableCDI.Columns.Add(dcExcesoCDI);


        //                int rowIndex3 = 4;
        //                CreateHeader(wsCDI, ref rowIndex3, dataTableCDI);

        //                wsCDI.Cells[5, 2].Value = "110";
        //                wsCDI.Cells[5, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[5, 3].Value = "MONTERREY";

        //                wsCDI.Cells[6, 2].Value = "150";
        //                wsCDI.Cells[6, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[6, 3].Value = "SALTILLO";

        //                wsCDI.Cells[7, 2].Value = "160";
        //                wsCDI.Cells[7, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[7, 3].Value = "MATAMOROS";

        //                wsCDI.Cells[8, 2].Value = "170";
        //                wsCDI.Cells[8, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[8, 3].Value = "TORREON";

        //                wsCDI.Cells[9, 2].Value = "180";
        //                wsCDI.Cells[9, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[9, 3].Value = "LAREDO";

        //                wsCDI.Cells[10, 2].Value = "190";
        //                wsCDI.Cells[10, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[10, 3].Value = "LEON";

        //                wsCDI.Cells[11, 2].Value = "200";
        //                wsCDI.Cells[11, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[11, 3].Value = "TIJUANA";

        //                wsCDI.Cells[12, 2].Value = "210";
        //                wsCDI.Cells[12, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[12, 3].Value = "CHIHUAHUA";

        //                wsCDI.Cells[13, 2].Value = "220";
        //                wsCDI.Cells[13, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[13, 3].Value = "SAN LUIS";

        //                wsCDI.Cells[14, 2].Value = "230";
        //                wsCDI.Cells[14, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[14, 3].Value = "JUAREZ";

        //                wsCDI.Cells[15, 2].Value = "240";
        //                wsCDI.Cells[15, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[15, 3].Value = "AGUASCALIENTES";

        //                wsCDI.Cells[16, 2].Value = "310";
        //                wsCDI.Cells[16, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[16, 3].Value = "MEXICO";

        //                wsCDI.Cells[17, 2].Value = "340";
        //                wsCDI.Cells[17, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[17, 3].Value = "VERACRUZ";

        //                wsCDI.Cells[18, 2].Value = "350";
        //                wsCDI.Cells[18, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[18, 3].Value = "CD CARMEN";

        //                wsCDI.Cells[19, 2].Value = "360";
        //                wsCDI.Cells[19, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[19, 3].Value = "MERIDA";

        //                wsCDI.Cells[20, 2].Value = "370";
        //                wsCDI.Cells[20, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[20, 3].Value = "CANCUN";

        //                wsCDI.Cells[21, 2].Value = "380";
        //                wsCDI.Cells[21, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[21, 3].Value = "RIVIERA";

        //                wsCDI.Cells[22, 2].Value = "390";
        //                wsCDI.Cells[22, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[22, 3].Value = "VALLARTA";

        //                wsCDI.Cells[23, 2].Value = "400";
        //                wsCDI.Cells[23, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[23, 3].Value = "LOS CABOS";


        //                wsCDI.Cells[24, 2].Value = "410";
        //                wsCDI.Cells[24, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[24, 3].Value = "QUERETARO";

        //                wsCDI.Cells[25, 2].Value = "430";
        //                wsCDI.Cells[25, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[25, 3].Value = "GUADALAJARA";

        //                wsCDI.Cells[26, 2].Value = "510";
        //                wsCDI.Cells[26, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[26, 3].Value = "PUEBLA";


        //                wsCDI.Cells[27, 2].Value = "610";
        //                wsCDI.Cells[27, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[27, 3].Value = "COATZACOALCOS";


        //                wsCDI.Cells[28, 2].Value = "620";
        //                wsCDI.Cells[28, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[28, 3].Value = "VILLAHERMOSA";

        //                wsCDI.Cells[29, 2].Value = "640";
        //                wsCDI.Cells[29, 3].Style.Font.Bold = true;
        //                wsCDI.Cells[29, 3].Value = "TOLUCA";



        //                Byte[] bin = p.GetAsByteArray();
        //                File.WriteAllBytes(ruta, bin);


        //                if (File.Exists(ruta))
        //                {
        //                    string ruta2 = null;
        //                    ruta2 = Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xlsx";
        //                    // File.Move(ruta, Server.MapPath("Reportes") + "\\Reporte" + tipo + nro + ".xls");
        //                    Response.Redirect("Reportes\\Reporte" + tipo + nro + ".xlsx", false);
        //                }

        //            }

        //        }

        //    }
        //}

        private static void SetWorkbookProperties(ExcelPackage p)
        {
            //Here setting some document properties
            p.Workbook.Properties.Author = "Rafael Mejía";
            p.Workbook.Properties.Title = "Reportes Centrales";


        }

        private string getCDIName(int CDI)
        {
            var name = "";
            switch (CDI)
            {
                case 110:
                    name = "MTY";
                    break;
                case 150:
                    name = "SAL";
                    break;
                case 170:
                    name = "TOR";
                    break;
                case 160:
                    name = "MAT";
                    break;
                case 180:
                    name = "LAR";
                    break;
                case 190:
                    name = "LEON";
                    break;
                case 200:
                    name = "TIJ";
                    break;
                case 210:
                    name = "CHI";
                    break;
                case 220:
                    name = "SLP";
                    break;
                case 230:
                    name = "JUA";
                    break;
                case 240:
                    name = "AGS";
                    break;
                case 310:
                    name = "MEX";
                    break;
                case 340:
                    name = "VER";
                    break;
                case 350:
                    name = "CAR";
                    break;
                case 360:
                    name = "MER";
                    break;
                case 370:
                    name = "CAN";
                    break;
                case 380:
                    name = "RIV";
                    break;
                case 390:
                    name = "VAL";
                    break;
                case 400:
                    name = "CAB";
                    break;
                case 410:
                    name = "QRO";
                    break;
                case 430:
                    name = "GDL";
                    break;
                case 510:
                    name = "PUE";
                    break;
                case 610:
                    name = "COA";
                    break;
                case 620:
                    name = "VIL";
                    break;
                case 640:
                    name = "TOL";
                    break;
                default:
                    name = "NO";
                    break;



            }
            return name;
        }




        private string getMonthName(int CDI)
        {
            var name = "";
            switch (CDI)
            {

                case 1:
                    name = "Ene";
                    break;
                case 2:
                    name = "Feb";
                    break;
                case 3:
                    name = "Mar";
                    break;
                case 4:
                    name = "Abr";
                    break;
                case 5:
                    name = "May";
                    break;
                case 6:
                    name = "Jun";
                    break;
                case 7:
                    name = "Jul";
                    break;
                case 8:
                    name = "Ago";
                    break;
                case 9:
                    name = "Sep";
                    break;
                case 10:
                    name = "Oct";
                    break;
                case 11:
                    name = "Nov";
                    break;
                case 12:
                    name = "Dic";
                    break;


            }
            return name;
        }

        private static ExcelWorksheet CreateSheet(ExcelPackage p, string sheetName, int index)
        {
            p.Workbook.Worksheets.Add(sheetName);
            ExcelWorksheet ws = p.Workbook.Worksheets[index];
            ws.Name = sheetName; //Setting Sheet's name
            ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
            ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet

            return ws;
        }

        //private static void CreateHeader(ExcelWorksheet ws, ref int rowIndex, DataTable dt)
        //{


        //    int colIndex = 2;
        //    foreach (DataColumn dc in dt.Columns) //Creating Headings
        //    {
        //        var cell = ws.Cells[rowIndex, colIndex];

        //        //Setting the background color of header cells to Gray
        //        var fill = cell.Style.Fill;
        //        fill.PatternType = ExcelFillStyle.Solid;
        //        fill.BackgroundColor.SetColor(Color.Gray);

        //        //Setting Top/left,right/bottom borders.
        //        var border = cell.Style.Border;
        //        border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

        //        //Setting Value in cell
        //        cell.Value = dc.ColumnName;

        //        colIndex++;
        //    }
        //}

        //private static void CreateData(ExcelWorksheet ws, ref int rowIndex, DataTable dt)
        //{
        //    int colIndex = 0;
        //    foreach (DataRow dr in dt.Rows) // Adding Data into rows
        //    {
        //        colIndex = 2;
        //        rowIndex++;

        //        foreach (DataColumn dc in dt.Columns)
        //        {
        //            var cell = ws.Cells[rowIndex, colIndex];

        //            //Setting Value in cell
        //            if (dc.DataType == typeof(System.Double))
        //            {
        //                cell.Value = (dr[dc.ColumnName].ToString());
        //                cell.Style.Numberformat.Format = "0.00";
        //            }
        //            else
        //            {
        //                cell.Value = dr[dc.ColumnName];
        //            }





        //            //Setting borders of cell
        //            var border = cell.Style.Border;
        //            border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
        //            colIndex++;
        //        }
        //    }
        //}

        //private static void CreateFooter(ExcelWorksheet ws, ref int rowIndex, DataTable dt)
        //{
        //    int colIndex = 0;
        //    foreach (DataColumn dc in dt.Columns) //Creating Formula in footers
        //    {
        //        colIndex++;
        //        var cell = ws.Cells[rowIndex, colIndex];

        //        //Setting Sum Formula
        //        cell.Formula = "Sum(" + ws.Cells[3, colIndex].Address + ":" + ws.Cells[rowIndex - 1, colIndex].Address + ")";

        //        //Setting Background fill color to Gray
        //        cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //        cell.Style.Fill.BackgroundColor.SetColor(Color.Gray);
        //    }
        //}




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