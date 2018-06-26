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
using System.Data.Sql;
using System.Data.SqlClient;
using System.Xml;


namespace SIANWEB
{
    public partial class Rep_Franquicias_VentaMensual : System.Web.UI.Page
    {
        #region Variables
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
                        this.CargarCentros();

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_Cve.Value = randObj.Next().ToString();
                        CargarAnoMes();
                        this.TblEncabezado.Visible = false;

                    }
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
                if (!_PermisoImprimir)
                {
                    this.Alerta("No tiene permisos para ver el reporte");
                    return;
                }
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (Page.IsValid)
                {
                    if (btn.CommandName == "print")
                    {
                        this.Imprimir();
                    }

                    if (btn.CommandName == "excel")
                    {
                        this.GenerarExcel();

                    }
                }


            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object sender, EventArgs e)
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
            //nuevo();
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

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun cn_comun = new CN__Comun();
                cn_comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd, sesion.Emp_Cnx, "spCatCalendarioAnhio2_Combo", ref this.cmbanio);
                //cn_comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd, sesion.CalendarioIni.Year, sesion.Emp_Cnx, "spCatCalendarioMes_Combo", ref this.cmbmes);

                this.cmbanio.SelectedValue = DateTime.Now.Year.ToString();
                //this.cmbmes.SelectedValue = DateTime.Now.Month.ToString();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void Imprimir()
        {
            try
            {
                if (this.cmbanio.SelectedValue == "-1")
                {
                    Alerta("Seleccione un año");
                    return;
                }
                //if (this.cmbmes.SelectedValue == "-1")
                //{
                //    Alerta("Seleccione un mes");
                //    return;
                //}

                Alerta("No se puede mostrar este reporte, solo se descarga a excel.");
                return;

                //int GeneroPoliza = 0;
                //int SubioIndicadores = 0;
                //int GeneroSaldos = 0;

                //Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //int anio = int.Parse(this.cmbanio.SelectedValue);
                ////int mes = int.Parse(this.cmbmes.SelectedValue);

                //CN_RotInventarios cn_ri = new CN_RotInventarios();
                ////cn_ri.RotacionInventariosMensual_Valida(anio, mes, ref GeneroPoliza, ref SubioIndicadores, ref GeneroSaldos, sesion.Emp_Cnx);

                //if (GeneroPoliza <= 0)
                //{
                //    Alerta("Imposible generar reporte, ya que aún no se ha generado la póliza para el mes de <b>" + cmbmes.Text + "<b/> del " + this.cmbanio.Text);
                //    return;
                //}
                //if (SubioIndicadores <= 0)
                //{
                //    Alerta("Imposible generar reporte, ya que aún no se han ingresado los indicadores para el mes <b>" + cmbmes.Text + "<b/> del " + this.cmbanio.Text);
                //    return;
                //}
                //if (GeneroSaldos  <= 0)
                //{
                //    Alerta("Imposible generar reporte, ya que aún no se han generado los saldos finales para el mes <b>" + cmbmes.Text + "<b/> del " + this.cmbanio.Text);
                //    return;
                //}



                //ArrayList ALValorParametrosInternos = new ArrayList();

                //ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                //ALValorParametrosInternos.Add(this.cmbanio.SelectedValue);
                //ALValorParametrosInternos.Add(this.cmbmes.SelectedValue);
                //ALValorParametrosInternos.Add(this.cmbmes.Text);




                //Type instance = null;

                //instance = typeof(LibreriaReportes.RepRotacionInventario);



                //Session["InternParameter_Values" + Session.SessionID + HF_Cve.Value] = ALValorParametrosInternos;
                //Session["assembly" + Session.SessionID + HF_Cve.Value] = instance.AssemblyQualifiedName;
                //RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_Cve.Value + "');");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private void GenerarExcel()
        {


            string XML_SUR = string.Empty;
            int anio = int.Parse(this.cmbanio.SelectedValue);
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            DataTable dt = new DataTable();

            dt.Columns.Add("IID");
            dt.Columns.Add("Zona");
            dt.Columns.Add("Num_Cliente");
            dt.Columns.Add("CD");
            dt.Columns.Add("Formato");
            dt.Columns.Add("Enero");
            dt.Columns.Add("Febrero");
            dt.Columns.Add("Marzo");
            dt.Columns.Add("Abril");
            dt.Columns.Add("Mayo");
            dt.Columns.Add("Junio");
            dt.Columns.Add("Julio");
            dt.Columns.Add("Agosto");
            dt.Columns.Add("Septiembre");
            dt.Columns.Add("Octubre");
            dt.Columns.Add("Noviembre");
            dt.Columns.Add("Diciembre");

            dt.TableName = "MasterTable";

            //Se Consume Webservice para realziar la Consulta Bennettss
            ConsultaVentaMensualBennetts.Reportes RP = new ConsultaVentaMensualBennetts.Reportes();

            ////Obtenemos XML de Bennet Sur
            XML_SUR = RP.ConsultaVentaMensual("1", 830, anio);

            ////Enviamos XML a Datatable
            StringReader theReader = new StringReader(XML_SUR);
            DataSet DS_XML = new DataSet();
            DS_XML.ReadXml(theReader);

            // Asignamos los Valores que Contiene Bennet Sur
            DS_XML.Tables[0].Rows[0][0] = "1";
            DS_XML.Tables[0].Rows[0][1] = "CLUSTER BENNETTS";
            DS_XML.Tables[0].Rows[0][2] = "30710";
            DS_XML.Tables[0].Rows[0][3] = "CDiK México (E.B.)";
            DS_XML.Tables[0].Rows[0][4] = "CDiK";
            DS_XML.Tables[0].AcceptChanges();

            //Copiamos los registros y los enviamos a la tabla que crea el formato Excel
            DataRow[] rowsToCopy;
            rowsToCopy = DS_XML.Tables[0].Select("");

            DataRow dr = dt.NewRow();
            dr["IID"] = rowsToCopy[0]["IID"];
            dr["Zona"] = rowsToCopy[0][1];
            dr["Num_Cliente"] = rowsToCopy[0][2];
            dr["CD"] = rowsToCopy[0][3];
            dr["Formato"] = rowsToCopy[0][4];
            dr["Enero"] = rowsToCopy[0][5];
            dr["Febrero"] = rowsToCopy[0][6];
            dr["Marzo"] = rowsToCopy[0][7];
            dr["Abril"] = rowsToCopy[0][8];
            dr["Mayo"] = rowsToCopy[0][9];
            dr["Junio"] = rowsToCopy[0][10];
            dr["Julio"] = rowsToCopy[0][11];
            dr["Agosto"] = rowsToCopy[0][12];
            dr["Septiembre"] = rowsToCopy[0][13];
            dr["Octubre"] = rowsToCopy[0][14];
            dr["Noviembre"] = rowsToCopy[0][15];
            dr["Diciembre"] = rowsToCopy[0][16];
            dt.Rows.Add(dr);


            //---Sesion.Emp_Cnx
            //string cnn = " Data Source=13.84.160.245; Initial catalog=sianwebcentral; user id=sa;Password=K3yQuimica10803!";
            //---Sesion.Emp_Cnx

            try
            {

            CN_Rep_Franquicia_VentaMensual CN_Rep_Franquicia_VentaMensual = new CN_Rep_Franquicia_VentaMensual();

            CN_Rep_Franquicia_VentaMensual.Rep_Franquicia_VentaMensual(anio, Sesion.Emp_Cnx, ref dt);


             string file = @"F:/APLICACIONES_IIS/SIANCENTRAL/Reportes/RptMensual_Franq.xls";

             string filename =  "RptMensual_Franq.xls";
            file = Server.MapPath("~/Reportes") + "\\" + filename;

            if (System.IO.File.Exists(Server.MapPath("~/Reportes") + "\\" + filename))
            {
                File.Delete(file);
            }

            //if (Directory.Exists(Path.GetDirectoryName(file)))
            //{
            //    File.Delete(file);
            //}


            //System.IO.StreamWriter sw = new System.IO.StreamWriter("F:/APLICACIONES_IIS/SIANCENTRAL/Reporte/RptMensual_Franq.xls", true);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(file, true);

            String Linea;
            string Encabezado = "N";
            decimal SubTotal_Enero = 0, SubTotal_Febrero = 0, SubTotal_Marzo = 0, SubTotal_Abril = 0, SubTotal_Mayo = 0, SubTotal_Junio = 0;
            decimal SubTotal_Julio = 0, SubTotal_Agosto = 0, SubTotal_Septiembre = 0, SubTotal_Octubre = 0, SubTotal_Noviembre = 0, SubTotal_Diciembre = 0;
            decimal Suma_SubTotal_Enero = 0, Suma_SubTotal_Febrero = 0, Suma_SubTotal_Marzo = 0, Suma_SubTotal_Abril = 0, Suma_SubTotal_Mayo = 0, Suma_SubTotal_Junio = 0;
            decimal Suma_SubTotal_Julio = 0, Suma_SubTotal_Agosto = 0, Suma_SubTotal_Septiembre = 0, Suma_SubTotal_Octubre = 0, Suma_SubTotal_Noviembre = 0, Suma_SubTotal_Diciembre = 0;
            decimal Suma_Todos_SubTotales = 0;
            decimal Suma_SubTotal_Meses = 0, Suma_SubTotales_Zona = 0;
            int i = 0;

            #region ciclo de registros 
            foreach (DataRow row in dt.Rows) // Loop over the rows.
            {
                Suma_SubTotal_Meses = 0;
                #region Encabezado
                if (Encabezado == "N")
                {
                    Linea = "<table >";
                    sw.WriteLine(Linea);

                    Linea = "<tr>";
                    sw.WriteLine(Linea);
                    Linea = "<td  colspan=3 align=\"left\" > <b>";
                    sw.WriteLine(Linea);
                    Linea = "Reporte de Ventas (SIAN WEB) ";
                    sw.WriteLine(Linea);
                    Linea = "</b></td>";
                    sw.WriteLine(Linea);
                    Linea = "</tr>";
                    sw.WriteLine(Linea);

                    Linea = "<tr></tr>";

                    Linea = "<tr></tr>";
                    sw.WriteLine(Linea);
                    Linea = "<td  colspan=6  align=\"left\">";
                    sw.WriteLine(Linea);
                    Linea = "CDiK / CDK  / CDC Propias y Concesiondas ";
                    sw.WriteLine(Linea);
                    Linea = "</td>";
                    sw.WriteLine(Linea);

                    Linea = "<tr></tr>";
                    sw.WriteLine(Linea);




                    //Linea = "<style> table {border-collapse: separate; width: 100%;  border: 1px solid black;} table.ex1 {table-layout: auto; }} </style>";
                    //sw.WriteLine(Linea);

                    Linea = "<table border=\"1\"  class=\"ex1\">";
                    sw.WriteLine(Linea);

                    Linea = "<tr  bgcolor=#d9d9d9  align=\"center\" style=\"vertical-align:middle\" >";
                    sw.WriteLine(Linea);

                    Linea = "<td width=\"60%\" height=\"70\" width=\"300\"><b>";
                    sw.WriteLine(Linea);
                    Linea = "ZONA";
                    sw.WriteLine(Linea);
                    Linea = "</b></td>";
                    sw.WriteLine(Linea);

                    Linea = "<td> <b>";
                    sw.WriteLine(Linea);
                    Linea = "# DE CLIENTE";
                    sw.WriteLine(Linea);
                    Linea = "</b> </td>";
                    sw.WriteLine(Linea);

                    Linea = "<td> <b>";
                    sw.WriteLine(Linea);
                    Linea = "CDiK/CDK/CDC";
                    sw.WriteLine(Linea);
                    Linea = "</b> </td>";
                    sw.WriteLine(Linea);

                    Linea = "<td> <b>";
                    sw.WriteLine(Linea);
                    Linea = "FORMATO";
                    sw.WriteLine(Linea);
                    Linea = "</b> </td>";
                    sw.WriteLine(Linea);

                    Linea = "<td bgcolor=#ffffff> <FONT COLOR=#ffffff>";
                    sw.WriteLine(Linea);
                    Linea = "_";
                    sw.WriteLine(Linea);
                    Linea = "</FONT></td>";
                    sw.WriteLine(Linea);

                    Linea = "<td>  <b>";
                    sw.WriteLine(Linea);
                    Linea = "ENE";
                    sw.WriteLine(Linea);
                    Linea = "</b> </td>";
                    sw.WriteLine(Linea);

                    Linea = "<td> <b>";
                    sw.WriteLine(Linea);
                    Linea = "FEB";
                    sw.WriteLine(Linea);
                    Linea = "</b> </td>";
                    sw.WriteLine(Linea);

                    Linea = "<td> <b>";
                    sw.WriteLine(Linea);
                    Linea = "MAR";
                    sw.WriteLine(Linea);
                    Linea = "</b> </td>";
                    sw.WriteLine(Linea);

                    Linea = "<td> <b>";
                    sw.WriteLine(Linea);
                    Linea = "ABR";
                    sw.WriteLine(Linea);
                    Linea = "</b> </td>";
                    sw.WriteLine(Linea);

                    Linea = "<td> <b>";
                    sw.WriteLine(Linea);
                    Linea = "MAY";
                    sw.WriteLine(Linea);
                    Linea = "</b> </td>";
                    sw.WriteLine(Linea);

                    Linea = "<td> <b>";
                    sw.WriteLine(Linea);
                    Linea = "JUN";
                    sw.WriteLine(Linea);
                    Linea = "</b> </td>";
                    sw.WriteLine(Linea);

                    Linea = "<td> <b>";
                    sw.WriteLine(Linea);
                    Linea = "JUL      ";
                    sw.WriteLine(Linea);
                    Linea = "</b> </td>";
                    sw.WriteLine(Linea);

                    Linea = "<td> <b>";
                    sw.WriteLine(Linea);
                    Linea = "AGO";
                    sw.WriteLine(Linea);
                    Linea = "</b> </td>";
                    sw.WriteLine(Linea);

                    Linea = "<td> <b>";
                    sw.WriteLine(Linea);
                    Linea = "SEP";
                    sw.WriteLine(Linea);
                    Linea = "</b> </td>";
                    sw.WriteLine(Linea);

                    Linea = "<td> <b>";
                    sw.WriteLine(Linea);
                    Linea = "OCT";
                    sw.WriteLine(Linea);
                    Linea = "</b> </td>";
                    sw.WriteLine(Linea);

                    Linea = "<td> <b>";
                    sw.WriteLine(Linea);
                    Linea = "NOV";
                    sw.WriteLine(Linea);
                    Linea = "</b></td>";
                    sw.WriteLine(Linea);

                    Linea = "<td> <b>";
                    sw.WriteLine(Linea);
                    Linea = "DIC";
                    sw.WriteLine(Linea);
                    Linea = "</b> </td>";
                    sw.WriteLine(Linea);

                    Linea = "<td bgcolor=#ffffff> <FONT COLOR=#ffffff>";
                    sw.WriteLine(Linea);
                    Linea = "_";
                    sw.WriteLine(Linea);
                    Linea = "</FONT></td>";
                    sw.WriteLine(Linea);

                    Linea = "<td> <b>";
                    sw.WriteLine(Linea);
                    Linea = "SUMA TOTAL";
                    sw.WriteLine(Linea);
                    Linea = "</b> </td>";
                    sw.WriteLine(Linea);

                    Linea = "</tr>";
                    sw.WriteLine(Linea);

                    Encabezado = "S";
                }//Fin de Encabezado
                #endregion

                if (i == 0)
                {
                    #region PrimerRegistro
                    Linea = "<tr align=\"center\" style=\"vertical-align:middle\">";

                    sw.WriteLine(Linea);
                    //---Gris Claro->f2f2f2, Gris Obscuro->d9d9d9
                    Linea = "<td colspan=4 bgcolor=#f2f2f2 align=\"left\"> <b>";
                    sw.WriteLine(Linea);
                    Linea = dt.Rows[i]["ZONA"].ToString();
                    sw.WriteLine(Linea);
                    Linea = "</b></td>";
                    sw.WriteLine(Linea);
                    Linea = "</tr>";
                    sw.WriteLine(Linea);

                    Linea = "<td></td>";
                    sw.WriteLine(Linea);

                    #region 1_CalculoSubtotales
                    //Inicio - Suma de Subtotales Enero
                    if (string.IsNullOrEmpty(dt.Rows[i]["Enero"].ToString()))
                    { SubTotal_Enero = 0 + SubTotal_Enero; }
                    else
                    {
                        SubTotal_Enero = (Convert.ToDecimal(dt.Rows[i]["Enero"].ToString())) + SubTotal_Enero;
                        Suma_SubTotal_Meses = Convert.ToDecimal(dt.Rows[i]["Enero"].ToString()) + Suma_SubTotal_Meses;
                        Suma_SubTotal_Enero = Suma_SubTotal_Enero + Convert.ToDecimal(dt.Rows[i]["Enero"].ToString());
                    }
                    //Fin   - Suma de Subtotales Enero

                    //Inicio - Suma de Subtotales Febrero
                    if (string.IsNullOrEmpty(dt.Rows[i]["Febrero"].ToString()))
                    { SubTotal_Febrero = 0 + SubTotal_Febrero; }
                    else
                    {
                        SubTotal_Febrero = (Convert.ToDecimal(dt.Rows[i]["Febrero"].ToString())) + SubTotal_Febrero;
                        Suma_SubTotal_Meses = Convert.ToDecimal(dt.Rows[i]["Febrero"].ToString()) + Suma_SubTotal_Meses;
                        Suma_SubTotal_Febrero = Suma_SubTotal_Febrero + (Convert.ToDecimal(dt.Rows[i]["Febrero"].ToString()));
                    }
                    //Fin   - Suma de Subtotales Febrero

                    //Inicio - Suma de Subtotales Marzo
                    if (string.IsNullOrEmpty(dt.Rows[i]["Marzo"].ToString()))
                    { SubTotal_Marzo = 0 + SubTotal_Marzo; }
                    else
                    {
                        SubTotal_Marzo = (Convert.ToDecimal(dt.Rows[i]["Marzo"].ToString())) + SubTotal_Marzo;
                        Suma_SubTotal_Meses = Convert.ToDecimal(dt.Rows[i]["Marzo"].ToString()) + Suma_SubTotal_Meses;
                        Suma_SubTotal_Marzo = Suma_SubTotal_Marzo + (Convert.ToDecimal(dt.Rows[i]["Marzo"].ToString()));
                    }
                    //Fin   - Suma de Subtotales Marzo

                    //Inicio - Suma de Subtotales Abril
                    if (string.IsNullOrEmpty(dt.Rows[i]["Abril"].ToString()))
                    { SubTotal_Abril = 0 + SubTotal_Abril; }
                    else
                    {
                        SubTotal_Abril = (Convert.ToDecimal(dt.Rows[i]["Abril"].ToString())) + SubTotal_Abril;
                        Suma_SubTotal_Meses = Convert.ToDecimal(dt.Rows[i]["Abril"].ToString()) + Suma_SubTotal_Meses;
                        Suma_SubTotal_Abril = Suma_SubTotal_Abril + (Convert.ToDecimal(dt.Rows[i]["Abril"].ToString()));
                    }
                    //Fin   - Suma de Subtotales Abril

                    //Inicio - Suma de Subtotales Mayo
                    if (string.IsNullOrEmpty(dt.Rows[i]["Mayo"].ToString()))
                    { SubTotal_Mayo = 0 + SubTotal_Mayo; }
                    else
                    {
                        SubTotal_Mayo = (Convert.ToDecimal(dt.Rows[i]["Mayo"].ToString())) + SubTotal_Mayo;
                        Suma_SubTotal_Meses = Convert.ToDecimal(dt.Rows[i]["Mayo"].ToString()) + Suma_SubTotal_Meses;
                        Suma_SubTotal_Mayo = Suma_SubTotal_Mayo + (Convert.ToDecimal(dt.Rows[i]["Mayo"].ToString()));
                    }
                    //Fin   - Suma de Subtotales Mayo

                    //Inicio - Suma de Subtotales Junio
                    if (string.IsNullOrEmpty(dt.Rows[i]["Junio"].ToString()))
                    { SubTotal_Junio = 0 + SubTotal_Junio; }
                    else
                    {
                        SubTotal_Junio = (Convert.ToDecimal(dt.Rows[i]["Junio"].ToString())) + SubTotal_Junio;
                        Suma_SubTotal_Meses = Convert.ToDecimal(dt.Rows[i]["Junio"].ToString()) + Suma_SubTotal_Meses;
                        Suma_SubTotal_Junio = Suma_SubTotal_Junio + (Convert.ToDecimal(dt.Rows[i]["Junio"].ToString()));
                    }
                    //Fin   - Suma de Subtotales Junio

                    //Inicio - Suma de Subtotales Julio
                    if (string.IsNullOrEmpty(dt.Rows[i]["Julio"].ToString()))
                    { SubTotal_Julio = 0 + SubTotal_Julio; }
                    else
                    {
                        SubTotal_Julio = (Convert.ToDecimal(dt.Rows[i]["Julio"].ToString())) + SubTotal_Julio;
                        Suma_SubTotal_Meses = Convert.ToDecimal(dt.Rows[i]["Julio"].ToString()) + Suma_SubTotal_Meses;
                        Suma_SubTotal_Julio = Suma_SubTotal_Julio + (Convert.ToDecimal(dt.Rows[i]["Julio"].ToString()));
                    }
                    //Fin   - Suma de Subtotales Julio

                    //Inicio - Suma de Subtotales Agosto
                    if (string.IsNullOrEmpty(dt.Rows[i]["Agosto"].ToString()))
                    { SubTotal_Agosto = 0 + SubTotal_Agosto; }
                    else
                    {
                        SubTotal_Agosto = (Convert.ToDecimal(dt.Rows[i]["Agosto"].ToString())) + SubTotal_Agosto;
                        Suma_SubTotal_Meses = Convert.ToDecimal(dt.Rows[i]["Agosto"].ToString()) + Suma_SubTotal_Meses;
                        Suma_SubTotal_Agosto = Suma_SubTotal_Agosto + (Convert.ToDecimal(dt.Rows[i]["Agosto"].ToString()));
                    }
                    //Fin   - Suma de Subtotales Agosto

                    //Inicio - Suma de Subtotales Septiembre
                    if (string.IsNullOrEmpty(dt.Rows[i]["Septiembre"].ToString()))
                    { SubTotal_Septiembre = 0 + SubTotal_Septiembre; }
                    else
                    {
                        SubTotal_Septiembre = (Convert.ToDecimal(dt.Rows[i]["Septiembre"].ToString())) + SubTotal_Septiembre;
                        Suma_SubTotal_Meses = Convert.ToDecimal(dt.Rows[i]["Septiembre"].ToString()) + Suma_SubTotal_Meses;
                        Suma_SubTotal_Septiembre = Suma_SubTotal_Septiembre + (Convert.ToDecimal(dt.Rows[i]["Septiembre"].ToString()));
                    }
                    //Fin   - Suma de Subtotales Septiembre

                    //Inicio - Suma de Subtotales Octubre
                    if (string.IsNullOrEmpty(dt.Rows[i]["Octubre"].ToString()))
                    { SubTotal_Octubre = 0 + SubTotal_Octubre; }
                    else
                    {
                        SubTotal_Octubre = (Convert.ToDecimal(dt.Rows[i]["Octubre"].ToString())) + SubTotal_Octubre;
                        Suma_SubTotal_Meses = Convert.ToDecimal(dt.Rows[i]["Octubre"].ToString()) + Suma_SubTotal_Meses;
                        Suma_SubTotal_Octubre = Suma_SubTotal_Octubre + (Convert.ToDecimal(dt.Rows[i]["Octubre"].ToString()));
                    }
                    //Fin   - Suma de Subtotales Octubre

                    //Inicio - Suma de Subtotales Noviembre
                    if (string.IsNullOrEmpty(dt.Rows[i]["Noviembre"].ToString()))
                    { SubTotal_Noviembre = 0 + SubTotal_Noviembre; }
                    else
                    {
                        SubTotal_Noviembre = (Convert.ToDecimal(dt.Rows[i]["Noviembre"].ToString())) + SubTotal_Noviembre;
                        Suma_SubTotal_Meses = Convert.ToDecimal(dt.Rows[i]["Noviembre"].ToString()) + Suma_SubTotal_Meses;
                        Suma_SubTotal_Noviembre = Suma_SubTotal_Noviembre + (Convert.ToDecimal(dt.Rows[i]["Noviembre"].ToString()));
                    }
                    //Fin   - Suma de Subtotales Noviembre

                    //Inicio - Suma de Subtotales Diciembre
                    if (string.IsNullOrEmpty(dt.Rows[i]["Diciembre"].ToString()))
                    { SubTotal_Diciembre = 0 + SubTotal_Diciembre; }
                    else
                    {
                        SubTotal_Diciembre = (Convert.ToDecimal(dt.Rows[i]["Diciembre"].ToString())) + SubTotal_Diciembre;
                        Suma_SubTotal_Meses = Convert.ToDecimal(dt.Rows[i]["Diciembre"].ToString()) + Suma_SubTotal_Meses;
                        Suma_SubTotal_Diciembre = Suma_SubTotal_Diciembre + (Convert.ToDecimal(dt.Rows[i]["Diciembre"].ToString()));
                    }
                    //Fin   - Suma de Subtotales Diciembre                        
                    #endregion

                    #endregion PrimerRegistro
                }

                if (i >= 1)
                {
                    #region SegundoRegistrooMas

                    #region 2_CalculoSubtotales
                    //Inicio - Suma de Subtotales Enero
                    if (string.IsNullOrEmpty(dt.Rows[i]["Enero"].ToString()))
                    { SubTotal_Enero = 0 + SubTotal_Enero; }
                    else
                    {
                        SubTotal_Enero = (Convert.ToDecimal(dt.Rows[i]["Enero"].ToString())) + SubTotal_Enero;
                        Suma_SubTotal_Meses = Suma_SubTotal_Meses + Convert.ToDecimal(dt.Rows[i]["Enero"].ToString());
                        Suma_SubTotal_Enero = Suma_SubTotal_Enero + Convert.ToDecimal(dt.Rows[i]["Enero"].ToString());
                    }
                    //Fin   - Suma de Subtotales Enero

                    //Inicio - Suma de Subtotales Febrero
                    if (string.IsNullOrEmpty(dt.Rows[i]["Febrero"].ToString()))
                    { SubTotal_Febrero = 0 + SubTotal_Febrero; }
                    else
                    {
                        SubTotal_Febrero = (Convert.ToDecimal(dt.Rows[i]["Febrero"].ToString())) + SubTotal_Febrero;
                        Suma_SubTotal_Meses = Suma_SubTotal_Meses + Convert.ToDecimal(dt.Rows[i]["Febrero"].ToString());
                        Suma_SubTotal_Febrero = Suma_SubTotal_Febrero + Convert.ToDecimal(dt.Rows[i]["Febrero"].ToString());
                    }
                    //Fin   - Suma de Subtotales Febrero

                    //Inicio - Suma de Subtotales Marzo
                    if (string.IsNullOrEmpty(dt.Rows[i]["Marzo"].ToString()))
                    { SubTotal_Marzo = 0 + SubTotal_Marzo; }
                    else
                    {
                        SubTotal_Marzo = (Convert.ToDecimal(dt.Rows[i]["Marzo"].ToString())) + SubTotal_Marzo;
                        Suma_SubTotal_Meses = Suma_SubTotal_Meses + Convert.ToDecimal(dt.Rows[i]["Marzo"].ToString());
                        Suma_SubTotal_Marzo = Suma_SubTotal_Marzo + Convert.ToDecimal(dt.Rows[i]["Marzo"].ToString());
                    }
                    //Fin   - Suma de Subtotales Marzo

                    //Inicio - Suma de Subtotales Abril
                    if (string.IsNullOrEmpty(dt.Rows[i]["Abril"].ToString()))
                    { SubTotal_Abril = 0 + SubTotal_Abril; }
                    else
                    {
                        SubTotal_Abril = (Convert.ToDecimal(dt.Rows[i]["Abril"].ToString())) + SubTotal_Abril;
                        Suma_SubTotal_Meses = Suma_SubTotal_Meses + Convert.ToDecimal(dt.Rows[i]["Abril"].ToString());
                        Suma_SubTotal_Abril = Suma_SubTotal_Abril + Convert.ToDecimal(dt.Rows[i]["Abril"].ToString());
                    }
                    //Fin   - Suma de Subtotales Abril

                    //Inicio - Suma de Subtotales Mayo
                    if (string.IsNullOrEmpty(dt.Rows[i]["Mayo"].ToString()))
                    { SubTotal_Mayo = 0 + SubTotal_Mayo; }
                    else
                    {
                        SubTotal_Mayo = (Convert.ToDecimal(dt.Rows[i]["Mayo"].ToString())) + SubTotal_Mayo;
                        Suma_SubTotal_Meses = Suma_SubTotal_Meses + Convert.ToDecimal(dt.Rows[i]["Mayo"].ToString());
                        Suma_SubTotal_Mayo = Suma_SubTotal_Mayo + Convert.ToDecimal(dt.Rows[i]["Mayo"].ToString());
                    }
                    //Fin   - Suma de Subtotales Mayo

                    //Inicio - Suma de Subtotales Junio
                    if (string.IsNullOrEmpty(dt.Rows[i]["Junio"].ToString()))
                    { SubTotal_Junio = 0 + SubTotal_Junio; }
                    else
                    {
                        SubTotal_Junio = (Convert.ToDecimal(dt.Rows[i]["Junio"].ToString())) + SubTotal_Junio;
                        Suma_SubTotal_Meses = Suma_SubTotal_Meses + Convert.ToDecimal(dt.Rows[i]["Junio"].ToString());
                        Suma_SubTotal_Junio = Suma_SubTotal_Junio + Convert.ToDecimal(dt.Rows[i]["Junio"].ToString());
                    }
                    //Fin   - Suma de Subtotales Junio

                    //Inicio - Suma de Subtotales Julio
                    if (string.IsNullOrEmpty(dt.Rows[i]["Julio"].ToString()))
                    { SubTotal_Julio = 0 + SubTotal_Julio; }
                    else
                    {
                        SubTotal_Julio = (Convert.ToDecimal(dt.Rows[i]["Julio"].ToString())) + SubTotal_Julio;
                        Suma_SubTotal_Meses = Suma_SubTotal_Meses + Convert.ToDecimal(dt.Rows[i]["Julio"].ToString());
                        Suma_SubTotal_Julio = Suma_SubTotal_Julio + Convert.ToDecimal(dt.Rows[i]["Julio"].ToString());
                    }
                    //Fin   - Suma de Subtotales Julio

                    //Inicio - Suma de Subtotales Agosto
                    if (string.IsNullOrEmpty(dt.Rows[i]["Agosto"].ToString()))
                    { SubTotal_Agosto = 0 + SubTotal_Agosto; }
                    else
                    {
                        SubTotal_Agosto = (Convert.ToDecimal(dt.Rows[i]["Agosto"].ToString())) + SubTotal_Agosto;
                        Suma_SubTotal_Meses = Suma_SubTotal_Meses + Convert.ToDecimal(dt.Rows[i]["Agosto"].ToString());
                        Suma_SubTotal_Agosto = Suma_SubTotal_Agosto + Convert.ToDecimal(dt.Rows[i]["Agosto"].ToString());
                    }
                    //Fin   - Suma de Subtotales Agosto

                    //Inicio - Suma de Subtotales Septiembre
                    if (string.IsNullOrEmpty(dt.Rows[i]["Septiembre"].ToString()))
                    { SubTotal_Septiembre = 0 + SubTotal_Septiembre; }
                    else
                    {
                        SubTotal_Septiembre = (Convert.ToDecimal(dt.Rows[i]["Septiembre"].ToString())) + SubTotal_Septiembre;
                        Suma_SubTotal_Meses = Suma_SubTotal_Meses + Convert.ToDecimal(dt.Rows[i]["Septiembre"].ToString());
                        Suma_SubTotal_Septiembre = Suma_SubTotal_Septiembre + Convert.ToDecimal(dt.Rows[i]["Septiembre"].ToString());
                    }
                    //Fin   - Suma de Subtotales Septiembre

                    //Inicio - Suma de Subtotales Octubre
                    if (string.IsNullOrEmpty(dt.Rows[i]["Octubre"].ToString()))
                    { SubTotal_Octubre = 0 + SubTotal_Octubre; }
                    else
                    {
                        SubTotal_Octubre = (Convert.ToDecimal(dt.Rows[i]["Octubre"].ToString())) + SubTotal_Octubre;
                        Suma_SubTotal_Meses = Suma_SubTotal_Meses + Convert.ToDecimal(dt.Rows[i]["Octubre"].ToString());
                        Suma_SubTotal_Octubre = Suma_SubTotal_Octubre + Convert.ToDecimal(dt.Rows[i]["Octubre"].ToString());
                    }
                    //Fin   - Suma de Subtotales Octubre

                    //Inicio - Suma de Subtotales Noviembre
                    if (string.IsNullOrEmpty(dt.Rows[i]["Noviembre"].ToString()))
                    { SubTotal_Noviembre = 0 + SubTotal_Noviembre; }
                    else
                    {
                        SubTotal_Noviembre = (Convert.ToDecimal(dt.Rows[i]["Noviembre"].ToString())) + SubTotal_Noviembre;
                        Suma_SubTotal_Meses = Suma_SubTotal_Meses + Convert.ToDecimal(dt.Rows[i]["Noviembre"].ToString());
                        Suma_SubTotal_Noviembre = Suma_SubTotal_Noviembre + Convert.ToDecimal(dt.Rows[i]["Noviembre"].ToString());
                    }
                    //Fin   - Suma de Subtotales Noviembre

                    //Inicio - Suma de Subtotales Diciembre
                    if (string.IsNullOrEmpty(dt.Rows[i]["Diciembre"].ToString()))
                    { SubTotal_Diciembre = 0 + SubTotal_Diciembre; }
                    else
                    {
                        SubTotal_Diciembre = (Convert.ToDecimal(dt.Rows[i]["Diciembre"].ToString())) + SubTotal_Diciembre;
                        Suma_SubTotal_Meses = Suma_SubTotal_Meses + Convert.ToDecimal(dt.Rows[i]["Diciembre"].ToString());
                        Suma_SubTotal_Diciembre = Suma_SubTotal_Diciembre + Convert.ToDecimal(dt.Rows[i]["Diciembre"].ToString());
                    }
                    //Fin   - Suma de Subtotales Diciembre

                    #endregion
                    if (dt.Rows[i]["ZONA"].ToString() == dt.Rows[i - 1]["ZONA"].ToString())
                    {
                        Linea = "<td></td>";
                        sw.WriteLine(Linea);
                    }
                    else
                    {
                        Linea = "<tr style=\"font-weight:bold\" align=\"center\" style=\"vertical-align:middle\" bgcolor=#ffffcc  >";
                        sw.WriteLine(Linea);
                        //Inicio - Subtotal#ffffcc 
                        Linea = "<td bgcolor=#ffffff></td><td ></td><td colspan=2  align=\"left\">";
                        sw.WriteLine(Linea);
                        Linea = "SubTotal";
                        sw.WriteLine(Linea);
                        Linea = "</td>";
                        sw.WriteLine(Linea);
                        Linea = "<td bgcolor=#ffffff></td>";
                        sw.WriteLine(Linea);
                        //Fin - Subtotal

                        #region 3_CalculoSubtotal
                        //Inicio - Subtotal - Cantidad - Enero
                        if (string.IsNullOrEmpty(dt.Rows[i]["Enero"].ToString()))
                        { SubTotal_Enero = 0 + SubTotal_Enero; }
                        else
                        {
                            SubTotal_Enero = SubTotal_Enero - (Convert.ToDecimal(dt.Rows[i]["Enero"].ToString()));
                            Suma_SubTotales_Zona = SubTotal_Enero + Suma_SubTotales_Zona;
                        }

                        Linea = "<td>";
                        sw.WriteLine(Linea);
                        Linea = String.Format("{0:0,0}", SubTotal_Enero);

                        sw.WriteLine(Linea);
                        Linea = "</td>";

                        sw.WriteLine(Linea);

                        SubTotal_Enero = 0;

                        if (string.IsNullOrEmpty(dt.Rows[i]["Enero"].ToString()))
                        { SubTotal_Enero = 0 + SubTotal_Enero; }
                        else
                        { SubTotal_Enero = (Convert.ToDecimal(dt.Rows[i]["Enero"].ToString())) + SubTotal_Enero; }
                        //Fin - Subtotal - Cantidad- Enero

                        //Inicio - Subtotal - Cantidad - Febrero
                        if (string.IsNullOrEmpty(dt.Rows[i]["Febrero"].ToString()))
                        { SubTotal_Febrero = 0 + SubTotal_Febrero; }
                        else
                        {
                            SubTotal_Febrero = SubTotal_Febrero - (Convert.ToDecimal(dt.Rows[i]["Febrero"].ToString()));
                            Suma_SubTotales_Zona = SubTotal_Febrero + Suma_SubTotales_Zona;
                        }

                        Linea = "<td>";
                        sw.WriteLine(Linea);
                        Linea = String.Format("{0:0,0}", SubTotal_Febrero);
                        sw.WriteLine(Linea);
                        Linea = "</td>";

                        sw.WriteLine(Linea);

                        SubTotal_Febrero = 0;

                        if (string.IsNullOrEmpty(dt.Rows[i]["Febrero"].ToString()))
                        { SubTotal_Febrero = 0 + SubTotal_Febrero; }
                        else
                        { SubTotal_Febrero = (Convert.ToDecimal(dt.Rows[i]["Febrero"].ToString())) + SubTotal_Febrero; }
                        //Fin - Subtotal - Cantidad- Febrero

                        //Inicio - Subtotal - Cantidad - Marzo
                        if (string.IsNullOrEmpty(dt.Rows[i]["Marzo"].ToString()))
                        { SubTotal_Marzo = 0 + SubTotal_Marzo; }
                        else
                        {
                            SubTotal_Marzo = SubTotal_Marzo - (Convert.ToDecimal(dt.Rows[i]["Marzo"].ToString()));
                            Suma_SubTotales_Zona = SubTotal_Marzo + Suma_SubTotales_Zona;
                        }


                        Linea = "<td>";
                        sw.WriteLine(Linea);
                        Linea = String.Format("{0:0,0}", SubTotal_Marzo);
                        sw.WriteLine(Linea);
                        Linea = "</td>";

                        sw.WriteLine(Linea);

                        SubTotal_Marzo = 0;

                        if (string.IsNullOrEmpty(dt.Rows[i]["Marzo"].ToString()))
                        { SubTotal_Marzo = 0 + SubTotal_Marzo; }
                        else
                        { SubTotal_Marzo = (Convert.ToDecimal(dt.Rows[i]["Marzo"].ToString())) + SubTotal_Marzo; }
                        //Fin - Subtotal - Cantidad- Marzo

                        //Inicio - Subtotal - Cantidad - Abril
                        if (string.IsNullOrEmpty(dt.Rows[i]["Abril"].ToString()))
                        { SubTotal_Abril = 0 + SubTotal_Abril; }
                        else
                        {
                            SubTotal_Abril = SubTotal_Abril - (Convert.ToDecimal(dt.Rows[i]["Abril"].ToString()));
                            Suma_SubTotales_Zona = SubTotal_Abril + Suma_SubTotales_Zona;
                        }


                        Linea = "<td>";
                        sw.WriteLine(Linea);
                        Linea = String.Format("{0:0,0}", SubTotal_Abril);
                        sw.WriteLine(Linea);
                        Linea = "</td>";

                        sw.WriteLine(Linea);

                        SubTotal_Abril = 0;

                        if (string.IsNullOrEmpty(dt.Rows[i]["Abril"].ToString()))
                        { SubTotal_Abril = 0 + SubTotal_Abril; }
                        else
                        { SubTotal_Abril = (Convert.ToDecimal(dt.Rows[i]["Abril"].ToString())) + SubTotal_Abril; }
                        //Fin - Subtotal - Cantidad- Abril

                        //Inicio - Subtotal - Cantidad - Mayo
                        if (string.IsNullOrEmpty(dt.Rows[i]["Mayo"].ToString()))
                        { SubTotal_Mayo = 0 + SubTotal_Mayo; }
                        else
                        {
                            SubTotal_Mayo = SubTotal_Mayo - (Convert.ToDecimal(dt.Rows[i]["Mayo"].ToString()));
                            Suma_SubTotales_Zona = SubTotal_Mayo + Suma_SubTotales_Zona;
                        }


                        Linea = "<td>";
                        sw.WriteLine(Linea);
                        Linea = String.Format("{0:0,0}", SubTotal_Mayo);
                        sw.WriteLine(Linea);
                        Linea = "</td>";

                        sw.WriteLine(Linea);

                        SubTotal_Mayo = 0;

                        if (string.IsNullOrEmpty(dt.Rows[i]["Mayo"].ToString()))
                        { SubTotal_Mayo = 0 + SubTotal_Mayo; }
                        else
                        { SubTotal_Mayo = (Convert.ToDecimal(dt.Rows[i]["Mayo"].ToString())) + SubTotal_Mayo; }
                        //Fin - Subtotal - Cantidad- Mayo

                        //Inicio - Subtotal - Cantidad - Junio
                        if (string.IsNullOrEmpty(dt.Rows[i]["Junio"].ToString()))
                        { SubTotal_Junio = 0 + SubTotal_Junio; }
                        else
                        {
                            SubTotal_Junio = SubTotal_Junio - (Convert.ToDecimal(dt.Rows[i]["Junio"].ToString()));
                            Suma_SubTotales_Zona = SubTotal_Junio + Suma_SubTotales_Zona;
                        }


                        Linea = "<td>";
                        sw.WriteLine(Linea);
                        Linea = String.Format("{0:0,0}", SubTotal_Junio);
                        sw.WriteLine(Linea);
                        Linea = "</td>";

                        sw.WriteLine(Linea);

                        SubTotal_Junio = 0;

                        if (string.IsNullOrEmpty(dt.Rows[i]["Junio"].ToString()))
                        { SubTotal_Junio = 0 + SubTotal_Junio; }
                        else
                        { SubTotal_Junio = (Convert.ToDecimal(dt.Rows[i]["Junio"].ToString())) + SubTotal_Junio; }
                        //Fin - Subtotal - Cantidad- Junio

                        //Inicio - Subtotal - Cantidad - Julio
                        if (string.IsNullOrEmpty(dt.Rows[i]["Julio"].ToString()))
                        { SubTotal_Julio = 0 + SubTotal_Julio; }
                        else
                        {
                            SubTotal_Julio = SubTotal_Julio - (Convert.ToDecimal(dt.Rows[i]["Julio"].ToString()));
                            Suma_SubTotales_Zona = SubTotal_Julio + Suma_SubTotales_Zona;
                        }


                        Linea = "<td>";
                        sw.WriteLine(Linea);
                        Linea = String.Format("{0:0,0}", SubTotal_Julio);
                        sw.WriteLine(Linea);
                        Linea = "</td>";

                        sw.WriteLine(Linea);

                        SubTotal_Julio = 0;

                        if (string.IsNullOrEmpty(dt.Rows[i]["Julio"].ToString()))
                        { SubTotal_Julio = 0 + SubTotal_Julio; }
                        else
                        { SubTotal_Julio = (Convert.ToDecimal(dt.Rows[i]["Julio"].ToString())) + SubTotal_Julio; }
                        //Fin - Subtotal - Cantidad- Julio

                        //Inicio - Subtotal - Cantidad - Agosto
                        if (string.IsNullOrEmpty(dt.Rows[i]["Agosto"].ToString()))
                        { SubTotal_Agosto = 0 + SubTotal_Agosto; }
                        else
                        {
                            SubTotal_Agosto = SubTotal_Agosto - (Convert.ToDecimal(dt.Rows[i]["Agosto"].ToString()));
                            Suma_SubTotales_Zona = SubTotal_Agosto + Suma_SubTotales_Zona;
                        }


                        Linea = "<td>";
                        sw.WriteLine(Linea);
                        Linea = String.Format("{0:0,0}", SubTotal_Agosto);
                        sw.WriteLine(Linea);
                        Linea = "</td>";

                        sw.WriteLine(Linea);

                        SubTotal_Agosto = 0;

                        if (string.IsNullOrEmpty(dt.Rows[i]["Agosto"].ToString()))
                        { SubTotal_Agosto = 0 + SubTotal_Agosto; }
                        else
                        { SubTotal_Agosto = (Convert.ToDecimal(dt.Rows[i]["Agosto"].ToString())) + SubTotal_Agosto; }
                        //Fin - Subtotal - Cantidad- Agosto

                        //Inicio - Subtotal - Cantidad - Septiembre
                        if (string.IsNullOrEmpty(dt.Rows[i]["Septiembre"].ToString()))
                        {
                            SubTotal_Septiembre = 0 + SubTotal_Septiembre;
                        }
                        else
                        {
                            SubTotal_Septiembre = SubTotal_Septiembre - (Convert.ToDecimal(dt.Rows[i]["Septiembre"].ToString()));
                            Suma_SubTotales_Zona = SubTotal_Septiembre + Suma_SubTotales_Zona;
                        }


                        Linea = "<td>";
                        sw.WriteLine(Linea);
                        Linea = String.Format("{0:0,0}", SubTotal_Septiembre);
                        sw.WriteLine(Linea);
                        Linea = "</td>";

                        sw.WriteLine(Linea);

                        SubTotal_Septiembre = 0;

                        if (string.IsNullOrEmpty(dt.Rows[i]["Septiembre"].ToString()))
                        { SubTotal_Septiembre = 0 + SubTotal_Septiembre; }
                        else
                        { SubTotal_Septiembre = (Convert.ToDecimal(dt.Rows[i]["Septiembre"].ToString())) + SubTotal_Septiembre; }
                        //Fin - Subtotal - Cantidad- Septiembre


                        //Inicio - Subtotal - Cantidad - Octubre
                        if (string.IsNullOrEmpty(dt.Rows[i]["Octubre"].ToString()))
                        { SubTotal_Octubre = 0 + SubTotal_Octubre; }
                        else
                        {
                            SubTotal_Octubre = SubTotal_Octubre - (Convert.ToDecimal(dt.Rows[i]["Octubre"].ToString()));
                            Suma_SubTotales_Zona = SubTotal_Octubre + Suma_SubTotales_Zona;
                        }


                        Linea = "<td>";
                        sw.WriteLine(Linea);
                        Linea = String.Format("{0:0,0}", SubTotal_Octubre);
                        sw.WriteLine(Linea);
                        Linea = "</td>";

                        sw.WriteLine(Linea);

                        SubTotal_Octubre = 0;

                        if (string.IsNullOrEmpty(dt.Rows[i]["Octubre"].ToString()))
                        { SubTotal_Octubre = 0 + SubTotal_Octubre; }
                        else
                        { SubTotal_Octubre = (Convert.ToDecimal(dt.Rows[i]["Octubre"].ToString())) + SubTotal_Octubre; }
                        //Fin - Subtotal - Cantidad- Octubre

                        //Inicio - Subtotal - Cantidad - Noviembre
                        if (string.IsNullOrEmpty(dt.Rows[i]["Noviembre"].ToString()))
                        { SubTotal_Noviembre = 0 + SubTotal_Noviembre; }
                        else
                        {
                            SubTotal_Noviembre = SubTotal_Noviembre - (Convert.ToDecimal(dt.Rows[i]["Noviembre"].ToString()));
                            Suma_SubTotales_Zona = SubTotal_Noviembre + Suma_SubTotales_Zona;
                        }


                        Linea = "<td>";
                        sw.WriteLine(Linea);
                        Linea = String.Format("{0:0,0}", SubTotal_Noviembre);
                        sw.WriteLine(Linea);
                        Linea = "</td>";

                        sw.WriteLine(Linea);

                        SubTotal_Noviembre = 0;

                        if (string.IsNullOrEmpty(dt.Rows[i]["Noviembre"].ToString()))
                        { SubTotal_Noviembre = 0 + SubTotal_Noviembre; }
                        else
                        { SubTotal_Noviembre = (Convert.ToDecimal(dt.Rows[i]["Noviembre"].ToString())) + SubTotal_Noviembre; }
                        //Fin - Subtotal - Cantidad- Noviembre


                        //Inicio - Subtotal - Cantidad - Diciembre
                        if (string.IsNullOrEmpty(dt.Rows[i]["Diciembre"].ToString()))
                        { SubTotal_Diciembre = 0 + SubTotal_Diciembre; }
                        else
                        {
                            SubTotal_Diciembre = SubTotal_Diciembre - (Convert.ToDecimal(dt.Rows[i]["Diciembre"].ToString()));
                            Suma_SubTotales_Zona = SubTotal_Diciembre + Suma_SubTotales_Zona;
                        }


                        Linea = "<td>";
                        sw.WriteLine(Linea);
                        Linea = String.Format("{0:0,0}", SubTotal_Diciembre);
                        sw.WriteLine(Linea);
                        Linea = "</td>";

                        sw.WriteLine(Linea);

                        SubTotal_Diciembre = 0;

                        if (string.IsNullOrEmpty(dt.Rows[i]["Diciembre"].ToString()))
                        { SubTotal_Diciembre = 0 + SubTotal_Diciembre; }
                        else
                        { SubTotal_Diciembre = (Convert.ToDecimal(dt.Rows[i]["Diciembre"].ToString())) + SubTotal_Diciembre; }
                        //Fin - Subtotal - Cantidad- Diciembre

                        Linea = "<td bgcolor=#ffffff></td>";
                        sw.WriteLine(Linea);

                        Linea = "<td>";
                        sw.WriteLine(Linea);
                        Linea = String.Format("{0:0,0}", Suma_SubTotales_Zona);
                        sw.WriteLine(Linea);
                        Linea = "</td>";

                        sw.WriteLine(Linea);
                        Suma_SubTotales_Zona = 0;
                        #endregion

                        Linea = "</tr>";
                        sw.WriteLine(Linea);


                        Linea = "<td colspan=4 bgcolor=#f2f2f2><b> <b>";
                        sw.WriteLine(Linea);
                        Linea = dt.Rows[i]["ZONA"].ToString();
                        sw.WriteLine(Linea);
                        Linea = "</b></td>";

                        sw.WriteLine(Linea);
                        Linea = "</b></td> </tr><td ></td>";
                        sw.WriteLine(Linea);

                    }
                    #endregion SegundoRegistrooMas
                }

                Linea = "<td style=\"font-size:10.5pt\" align=\"center\" > ";
                sw.WriteLine(Linea);
                Linea = dt.Rows[i]["Num_Cliente"].ToString();
                sw.WriteLine(Linea);
                Linea = "</font> </td>";
                sw.WriteLine(Linea);

                Linea = "<td style=\"font-size:10.5pt\" align=\"left\">";
                sw.WriteLine(Linea);
                Linea = dt.Rows[i]["CD"].ToString();
                sw.WriteLine(Linea);
                Linea = "</td>";
                sw.WriteLine(Linea);


                Linea = "<td  style=\"font-size:10.5pt\" align=\"center\">";
                sw.WriteLine(Linea);
                if (string.IsNullOrEmpty(dt.Rows[i]["Formato"].ToString()))
                { Linea = ""; }
                else
                { Linea = dt.Rows[i]["Formato"].ToString(); }
                sw.WriteLine(Linea);
                Linea = "</td>";
                sw.WriteLine(Linea);

                Linea = "<td>";
                sw.WriteLine(Linea);
                Linea = " ";
                sw.WriteLine(Linea);
                Linea = "</td>";
                sw.WriteLine(Linea);

                Linea = "<td style=\"font-size:10.5pt\" align=\"center\">";
                sw.WriteLine(Linea);
                if (string.IsNullOrEmpty(dt.Rows[i]["Enero"].ToString()))
                { Linea = "0"; }
                else
                {
                    Linea = String.Format("{0:0,0}", Convert.ToDecimal(dt.Rows[i]["Enero"]));
                }
                sw.WriteLine(Linea);
                Linea = "</td>";
                sw.WriteLine(Linea);

                Linea = "<td style= \"font-size:10.5pt\" align=\"center\">";
                sw.WriteLine(Linea);
                if (string.IsNullOrEmpty(dt.Rows[i]["Febrero"].ToString()))
                { Linea = "0"; }
                else
                {
                    Linea = String.Format("{0:0,0}", Convert.ToDecimal(dt.Rows[i]["Febrero"]));
                }
                sw.WriteLine(Linea);
                Linea = "</td>";
                sw.WriteLine(Linea);

                Linea = "<td style=\"font-size:10.5pt\"  align=\"center\">";
                sw.WriteLine(Linea);
                if (string.IsNullOrEmpty(dt.Rows[i]["Marzo"].ToString()))
                { Linea = "0"; }
                else
                {
                    Linea = String.Format("{0:0,0}", Convert.ToDecimal(dt.Rows[i]["Marzo"]));
                }
                sw.WriteLine(Linea);
                Linea = "</td>";
                sw.WriteLine(Linea);

                Linea = "<td style=\"font-size:10.5pt\"  align=\"center\">";
                sw.WriteLine(Linea);
                if (string.IsNullOrEmpty(dt.Rows[i]["Abril"].ToString()))
                { Linea = "0"; }
                else
                {
                    Linea = String.Format("{0:0,0}", Convert.ToDecimal(dt.Rows[i]["Abril"]));
                }
                sw.WriteLine(Linea);
                Linea = "</td > ";
                sw.WriteLine(Linea);

                Linea = "<td style=\"font-size:10.5pt\"  align=\"center\">";
                sw.WriteLine(Linea);
                if (string.IsNullOrEmpty(dt.Rows[i]["Mayo"].ToString()))
                { Linea = "0"; }
                else
                { Linea = String.Format("{0:0,0}", Convert.ToDecimal(dt.Rows[i]["Mayo"])); }
                sw.WriteLine(Linea);
                Linea = "</td>";
                sw.WriteLine(Linea);

                Linea = "<td style=\"font-size:10.5pt\"  align=\"center\">";
                sw.WriteLine(Linea);
                if (string.IsNullOrEmpty(dt.Rows[i]["Junio"].ToString()))
                { Linea = "0"; }
                else
                { Linea = String.Format("{0:0,0}", Convert.ToDecimal(dt.Rows[i]["Junio"])); }
                sw.WriteLine(Linea);
                Linea = "</td>";
                sw.WriteLine(Linea);

                Linea = "<td style=\"font-size:10.5pt\"  align=\"center\" width=\"55px\">";
                sw.WriteLine(Linea);
                if (string.IsNullOrEmpty(dt.Rows[i]["Julio"].ToString()))
                { Linea = "0"; }
                else
                { Linea = String.Format("{0:0,0}", Convert.ToDecimal(dt.Rows[i]["Julio"])); }
                sw.WriteLine(Linea);
                Linea = "</td>";
                sw.WriteLine(Linea);

                Linea = "<td style=\"font-size:10.5pt\"  align=\"center\">";
                sw.WriteLine(Linea);
                if (string.IsNullOrEmpty(dt.Rows[i]["Agosto"].ToString()))
                { Linea = "0"; }
                else
                { Linea = String.Format("{0:0,0}", Convert.ToDecimal(dt.Rows[i]["Agosto"])); }
                sw.WriteLine(Linea);
                Linea = "</td>";
                sw.WriteLine(Linea);

                Linea = "<td style=\"font-size:10.5pt\"  align=\"center\">";
                sw.WriteLine(Linea);
                if (string.IsNullOrEmpty(dt.Rows[i]["Septiembre"].ToString()))
                { Linea = "0"; }
                else
                { Linea = String.Format("{0:0,0}", Convert.ToDecimal(dt.Rows[i]["Septiembre"])); }
                sw.WriteLine(Linea);
                Linea = "</td>";
                sw.WriteLine(Linea);

                Linea = "<td style=\"font-size:10.5pt\" align=\"center\">";
                sw.WriteLine(Linea);
                if (string.IsNullOrEmpty(dt.Rows[i]["Octubre"].ToString()))
                { Linea = "0"; }
                else
                { Linea = String.Format("{0:0,0}", Convert.ToDecimal(dt.Rows[i]["Octubre"])); }
                sw.WriteLine(Linea);
                Linea = "</td>";
                sw.WriteLine(Linea);


                Linea = "<td style=\"font-size:10.5pt\"  align=\"center\">";
                sw.WriteLine(Linea);
                if (string.IsNullOrEmpty(dt.Rows[i]["Noviembre"].ToString()))
                { Linea = "0"; }
                else
                { Linea = String.Format("{0:0,0}", Convert.ToDecimal(dt.Rows[i]["Noviembre"])); }
                sw.WriteLine(Linea);
                Linea = "</td>";
                sw.WriteLine(Linea);


                Linea = "<td style=\"font-size:10.5pt\" align=\"center\">";
                sw.WriteLine(Linea);
                if (string.IsNullOrEmpty(dt.Rows[i]["Diciembre"].ToString()))
                { Linea = "0"; }
                else
                { Linea = String.Format("{0:0,0}", Convert.ToDecimal(dt.Rows[i]["Diciembre"])); }
                sw.WriteLine(Linea);
                Linea = "</td>";
                sw.WriteLine(Linea);

                Linea = "<td bgcolor=#ffffff></td>";
                sw.WriteLine(Linea);

                Linea = "<td align=\"center\">";
                sw.WriteLine(Linea);
                if (string.IsNullOrEmpty(Suma_SubTotal_Meses.ToString()))
                { Linea = "0"; }
                else
                { Linea = String.Format("{0:0,0}", Suma_SubTotal_Meses); }
                sw.WriteLine(Linea);
                Linea = "</td>";
                sw.WriteLine(Linea);


                Linea = "</tr>";
                sw.WriteLine(Linea);

                //Linea = "</tr>";
                //sw.WriteLine(Linea);
                //Linea = "</tr>";
                //sw.WriteLine(Linea);

                i = i + 1;
            }

            #endregion ciclo registros 

            //Inicio - Subtotal - Final
            Linea = "<tr style=\"font-weight:bold\"  bgcolor=#ffffcc align=\"center\" > ";
            sw.WriteLine(Linea);
            Linea = "<td bgcolor=#ffffff></td><td></td><td colspan=2 align=\"left\">";
            sw.WriteLine(Linea);
            Linea = "SubTotal";
            sw.WriteLine(Linea);
            Linea = "</b></td>";
            sw.WriteLine(Linea);
            //Fin - Subtotal - Final

            #region 4_CalculoSubtotal-Final


            Linea = "<td bgcolor=#ffffff></td><td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", SubTotal_Enero);
            //Linea = SubTotal_Enero.ToString();
            Suma_SubTotales_Zona = SubTotal_Enero + Suma_SubTotales_Zona;
            sw.WriteLine(Linea);
            Linea = "</td>";

            sw.WriteLine(Linea);

            SubTotal_Enero = 0;

            Linea = "<td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", SubTotal_Febrero);
            Suma_SubTotales_Zona = SubTotal_Febrero + Suma_SubTotales_Zona;
            sw.WriteLine(Linea);
            Linea = "</td>";

            sw.WriteLine(Linea);

            SubTotal_Febrero = 0;


            Linea = "<td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", SubTotal_Marzo);
            Suma_SubTotales_Zona = SubTotal_Marzo + Suma_SubTotales_Zona;
            sw.WriteLine(Linea);
            Linea = "</td>";

            sw.WriteLine(Linea);

            SubTotal_Marzo = 0;

            Linea = "<td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", SubTotal_Abril);
            Suma_SubTotales_Zona = SubTotal_Abril + Suma_SubTotales_Zona;
            sw.WriteLine(Linea);
            Linea = "</td>";

            sw.WriteLine(Linea);

            SubTotal_Abril = 0;


            Linea = "<td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", SubTotal_Mayo);
            Suma_SubTotales_Zona = SubTotal_Mayo + Suma_SubTotales_Zona;
            sw.WriteLine(Linea);
            Linea = "</td>";

            sw.WriteLine(Linea);

            SubTotal_Mayo = 0;


            Linea = "<td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", SubTotal_Junio);
            Suma_SubTotales_Zona = SubTotal_Junio + Suma_SubTotales_Zona;
            sw.WriteLine(Linea);
            Linea = "</td>";

            sw.WriteLine(Linea);

            SubTotal_Junio = 0;


            Linea = "<td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", SubTotal_Julio);
            Suma_SubTotales_Zona = SubTotal_Julio + Suma_SubTotales_Zona;
            sw.WriteLine(Linea);
            Linea = "</td>";

            sw.WriteLine(Linea);

            SubTotal_Julio = 0;


            Linea = "<td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", SubTotal_Agosto);
            Suma_SubTotales_Zona = SubTotal_Agosto + Suma_SubTotales_Zona;
            sw.WriteLine(Linea);
            Linea = "</td>";

            sw.WriteLine(Linea);

            SubTotal_Agosto = 0;

            Linea = "<td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", SubTotal_Septiembre);
            Suma_SubTotales_Zona = SubTotal_Septiembre + Suma_SubTotales_Zona;
            sw.WriteLine(Linea);
            Linea = "</td>";

            sw.WriteLine(Linea);

            SubTotal_Septiembre = 0;

            Linea = "<td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", SubTotal_Octubre);
            Suma_SubTotales_Zona = SubTotal_Octubre + Suma_SubTotales_Zona;
            sw.WriteLine(Linea);
            Linea = "</td>";

            sw.WriteLine(Linea);

            SubTotal_Octubre = 0;



            Linea = "<td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", SubTotal_Noviembre);
            Suma_SubTotales_Zona = SubTotal_Noviembre + Suma_SubTotales_Zona;
            sw.WriteLine(Linea);
            Linea = "</td>";

            sw.WriteLine(Linea);

            SubTotal_Noviembre = 0;


            Linea = "<td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", SubTotal_Diciembre);
            Suma_SubTotales_Zona = SubTotal_Diciembre + Suma_SubTotales_Zona;
            sw.WriteLine(Linea);
            Linea = "</td>";

            sw.WriteLine(Linea);

            SubTotal_Diciembre = 0;

            Linea = "<td></td><td bgcolor=#ffffcc >";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", Suma_SubTotales_Zona);
            sw.WriteLine(Linea);
            Linea = "</td>";

            sw.WriteLine(Linea);

            Suma_SubTotales_Zona = 0;
            Linea = "</tr>";
            sw.WriteLine(Linea);
            #endregion

            //Inicio - Subtotal - Final
            Linea = "<tr style=\"font-weight:bold;font-size:16px;\"  bgcolor=#FFFF00 align=\"center\" >   ";

            sw.WriteLine(Linea);
            Linea = "<td></td><td></td> <td colspan=2>";
            sw.WriteLine(Linea);
            Linea = "TOTAL";
            sw.WriteLine(Linea);
            Linea = " </td>";
            sw.WriteLine(Linea);

            //Fin - Subtotal - Final

            Linea = "<td bgcolor=#ffffff></td><td><b> ";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", Suma_SubTotal_Enero);
            sw.WriteLine(Linea);
            Linea = "</b></td>";
            sw.WriteLine(Linea);

            Linea = "<td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", Suma_SubTotal_Febrero);
            sw.WriteLine(Linea);
            Linea = "</td>";
            sw.WriteLine(Linea);

            Linea = "<td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", Suma_SubTotal_Marzo);
            sw.WriteLine(Linea);
            Linea = "</td>";
            sw.WriteLine(Linea);

            Linea = "<td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", Suma_SubTotal_Abril);
            sw.WriteLine(Linea);
            Linea = "</td>";
            sw.WriteLine(Linea);

            Linea = "<td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", Suma_SubTotal_Mayo);
            sw.WriteLine(Linea);
            Linea = "</td>";
            sw.WriteLine(Linea);


            Linea = "<td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", Suma_SubTotal_Junio);
            sw.WriteLine(Linea);
            Linea = "</td>";
            sw.WriteLine(Linea);

            Linea = "<td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", Suma_SubTotal_Julio);
            sw.WriteLine(Linea);
            Linea = "</td>";
            sw.WriteLine(Linea);

            Linea = "<td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", Suma_SubTotal_Agosto);
            sw.WriteLine(Linea);
            Linea = "</td>";
            sw.WriteLine(Linea);

            Linea = "<td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", Suma_SubTotal_Septiembre);
            sw.WriteLine(Linea);
            Linea = "</td>";
            sw.WriteLine(Linea);

            Linea = "<td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", Suma_SubTotal_Octubre);
            sw.WriteLine(Linea);
            Linea = "</td>";
            sw.WriteLine(Linea);

            Linea = "<td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", Suma_SubTotal_Noviembre);
            sw.WriteLine(Linea);
            Linea = "</td>";
            sw.WriteLine(Linea);

            Linea = "<td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", Suma_SubTotal_Diciembre);
            sw.WriteLine(Linea);
            Linea = "</td>";
            sw.WriteLine(Linea);

            Suma_Todos_SubTotales = Suma_SubTotal_Enero + Suma_SubTotal_Febrero + Suma_SubTotal_Marzo + Suma_SubTotal_Abril + Suma_SubTotal_Mayo + Suma_SubTotal_Junio + Suma_SubTotal_Julio + Suma_SubTotal_Agosto + Suma_SubTotal_Septiembre + Suma_SubTotal_Octubre + Suma_SubTotal_Noviembre + Suma_SubTotal_Diciembre;

            Linea = "<td bgcolor=#ffffff></td>";
            sw.WriteLine(Linea);

            Linea = "<td>";
            sw.WriteLine(Linea);
            Linea = String.Format("{0:0,0}", Suma_Todos_SubTotales);
            sw.WriteLine(Linea);
            Linea = "</td>";
            sw.WriteLine(Linea);

            Linea = "</tr>";
            sw.WriteLine(Linea);

            sw.Close();


            //FileInfo fi = new FileInfo("F:/APLICACIONES_IIS/SIANCENTRAL/Reporte/RptMensual_Franq.xls");
            FileInfo fi = new FileInfo(file);

           // if (fi.Exists) { Response.Redirect("Reporte/RptMensual_Franq.xls"); }
            if (fi.Exists) { Response.Redirect("http://40.84.229.61/siancentral/Reportes/RptMensual_Franq.xls", false); }
            else { }

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