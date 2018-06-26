using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using iTextSharp.text;
using Telerik.Web.UI;
using System.Configuration;



namespace SIANWEB
{
    public partial class RepSOP_PedidosRemisiones : System.Web.UI.Page
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
        #region Page_Load
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

                     
                        //this.ValidarPermisos();
                        //if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        //{
                        //    RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                        //    return;
                        //}
                        this.CargarCentros();

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_Cve.Value = randObj.Next().ToString();
                        
                        this.dpFecha1.SelectedDate = DateTime.Now.AddDays(-1);
                        this.dpFechaFin.SelectedDate = DateTime.Now;
                        this.TblEncabezado.Visible = false;

                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }
        #endregion Page_Load
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

                    ErrorManager();
                    RadToolBarButton btn = e.Item as RadToolBarButton;
                    if (Page.IsValid)
                    {

                        if (btn.CommandName == "print")
                        {
                            if (ValidarFecha())
                            {
                                this.GenerarExcel();
                                Alerta("Se genero el archivo correctamente");

                            }
                        }
                    }


                    if (!_PermisoImprimir)
                    {
                        this.Alerta("No tiene permisos para ver el reporte");
                        return;
                    }
                    ErrorManager();
                //RadToolBarButton btn = e.Item as RadToolBarButton;
                //if (Page.IsValid)
                //{
                //    if (btn.CommandName == "print")
                //    {
                //        this.Imprimir();
                //    }

                //}
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
       
        private void GenerarExcel()
        {
            
            Random rnd = new Random();
            int numero = rnd.Next(1, 14334);

            string rutaynombre = "";
            String filename = "ReportePedidos_Remisiones" + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + numero.ToString();
            rutaynombre = System.Web.HttpContext.Current.Server.MapPath("~/Reportes") + "\\" + filename;

            ObtenerDetalle(rutaynombre);

            filename = filename+ ".xls";
            Response.ContentType = "application/vnd.ms-excel";
            Response.Redirect("~/Reportes/" + filename, false);
            Response.End();

            File.Delete("~/Reportes/" + filename);

        }
        
        private void ObtenerDetalle(string nombre)
        {
            try
            {
                List<ReportePedidosRemisiones> List = new List<ReportePedidosRemisiones>();
                List = GetList();
                DataTable dt = new DataTable();
                dt = Funcion.Convertidor<ReportePedidosRemisiones>.ListaToDatatable(List);

                List<ReportePedidosRemisiones> Listrem = new List<ReportePedidosRemisiones>();
                Listrem = GetListRemisiones();
                DataTable dtrem = new DataTable();
                dtrem = Funcion.Convertidor<ReportePedidosRemisiones>.ListaToDatatable(Listrem);

                GeneraExcel(dt,dtrem , nombre);

                 
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        
        static void GeneraExcel(DataTable tabla, DataTable tablarem, string NombreMes)
        {
            //string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Temp\ReportePedidos_Remisiones" + NombreMes + @".xls;Extended Properties=""Excel 8.0;HDR=NO;""";
          
            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + NombreMes + @".xls;Extended Properties=""Excel 8.0;HDR=NO;""";

            

            //connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Temp\ReportePedidos_RemisionesNombreMes.xls;Extended Properties=""Excel 8.0;HDR=NO;""";
            DbProviderFactory factory =
              DbProviderFactories.GetFactory("System.Data.OleDb");

            try{

            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                using (DbCommand command = connection.CreateCommand())
                {
                    connection.Open();  //open the connection

                    //use the '$' notation after the sheet name to indicate that this is
                    // an existing sheet and not to actually create it.  This basically defines
                    // the metadata for the insert statements that will follow.
                    // If the '$' notation is removed, then a new sheet is created named 'Sheet1'.

                    #region OrdenCOmpra

                    command.CommandText = "CREATE TABLE [Ordencompra] (FechaOrdendeCompra char(40),	CodigoCDI number,NombreCDI char(150), Codigo_SKU number, Descripcion_SKU char(150), Presentacion number,Unidad char(5),Cantidad number, ordenada number,Costo_AAA number, Importe_Total_AAA number, Num_OrdendeCompra number)";
                    command.ExecuteNonQuery();


                    for (int j = 0; j < tabla.Rows.Count; j++)
                    {
                        string comando = "";
                        string descripcion = "";
                        comando = "INSERT INTO [ordencompra] VALUES(";

                        for (int i = 0; i < tabla.Columns.Count; i++)
                        {
                            if (tabla.Columns[i].ColumnName == "FechaOrdendeCompra1")
                            {
                                comando = comando + '\"' + tabla.Rows[j][i].ToString() + '\"' + ",";
                            }

                            if (tabla.Columns[i].ColumnName == "CodigoCDI1")
                            {
                                comando = comando + tabla.Rows[j][i].ToString() + ",";
                            }
                            if (tabla.Columns[i].ColumnName == "NombreCDI1")
                            {
                                comando = comando + '\"' + tabla.Rows[j][i].ToString() + '\"' + ",";
                            }
                            if (tabla.Columns[i].ColumnName == "Codigo_SKU1")
                            {
                                comando = comando + tabla.Rows[j][i].ToString() + ",";
                            }
                            if (tabla.Columns[i].ColumnName == "Descripcion_SKU1")
                            {
                                descripcion = tabla.Rows[j][i].ToString();
                                descripcion = descripcion.Replace("\"", " ");
                                comando = comando + '\"' + descripcion + '\"' + ",";
                            }
                            if (tabla.Columns[i].ColumnName == "Presentacion1")
                            {
                                comando = comando + tabla.Rows[j][i].ToString() + ",";
                            }
                            if (tabla.Columns[i].ColumnName == "Unidad1")
                            {
                                comando = comando + '\"' + tabla.Rows[j][i].ToString() + '\"' + ",";
                            }
                            if (tabla.Columns[i].ColumnName == "Cantidad1")
                            {
                                comando = comando + tabla.Rows[j][i].ToString() + ",";
                            }
                            if (tabla.Columns[i].ColumnName == "ordenada1")
                            {
                                comando = comando + tabla.Rows[j][i].ToString() + ",";
                            }
                            if (tabla.Columns[i].ColumnName == "Costo_AAA1")
                            {
                                comando = comando + tabla.Rows[j][i].ToString() + ",";
                            }
                            if (tabla.Columns[i].ColumnName == "Importe_Total_AAA1")
                            {
                                comando = comando + tabla.Rows[j][i].ToString() + ",";
                            }
                            if (tabla.Columns[i].ColumnName == "Num_OrdendeCompra1")
                            {
                                comando = comando + tabla.Rows[j][i].ToString();
                            }
                        }
                        command.CommandText = comando + ")";
                        command.ExecuteNonQuery();
                    }
                        #endregion OrdenCOmpra


                #region Remision

                    command.CommandText = "CREATE TABLE [Remisiones] (FechaOrdendeCompra char(40),	Remision number,CodigoCDI number,NombreCDI char(150), Codigo_SKU number, Descripcion_SKU char(150), Presentacion number,Unidad char(5),Cantidad number, ordenada number,Costo_AAA number, Importe_Total_AAA number, Num_OrdendeCompra number)";
                    command.ExecuteNonQuery();

                    string descripcionrem = "";
                    for (int jr = 0; jr < tablarem.Rows.Count; jr++)
                    {
                        string comando = "INSERT INTO [Remisiones] VALUES(";

                        for (int ir = 0; ir < tablarem.Columns.Count; ir++)
                        {
                            if (tablarem.Columns[ir].ColumnName == "FechaOrdendeCompra1")
                            {
                                comando = comando + '\"' + tablarem.Rows[jr][ir].ToString() + '\"' + ",";
                            }
                            if (tablarem.Columns[ir].ColumnName == "Remision1")
                            {
                                comando = comando + tablarem.Rows[jr][ir].ToString() + ",";
                            }
                            if (tablarem.Columns[ir].ColumnName == "CodigoCDI1")
                            {
                                comando = comando + tablarem.Rows[jr][ir].ToString() + ",";
                            }
                            if (tablarem.Columns[ir].ColumnName == "NombreCDI1")
                            {
                                comando = comando + '\"' + tablarem.Rows[jr][ir].ToString() + '\"' + ",";
                            }
                            if (tablarem.Columns[ir].ColumnName == "Codigo_SKU1")
                            {
                                comando = comando + tablarem.Rows[jr][ir].ToString() + ",";
                            }
                            if (tablarem.Columns[ir].ColumnName == "Descripcion_SKU1")
                            {
                                descripcionrem = tablarem.Rows[jr][ir].ToString();
                                descripcionrem = descripcionrem.Replace("\"", " ");
                                comando = comando + '\"' + descripcionrem + '\"' + ",";
                                //comando = comando + '\"' +  tablarem.Rows[jr][ir].ToString() + '\"' + ",";
                            }
                            if (tablarem.Columns[ir].ColumnName == "Presentacion1")
                            {
                                comando = comando + tablarem.Rows[jr][ir].ToString() + ",";
                            }
                            if (tablarem.Columns[ir].ColumnName == "Unidad1")
                            {
                                comando = comando + '\"' + tablarem.Rows[jr][ir].ToString() + '\"' + ",";
                            }
                            if (tablarem.Columns[ir].ColumnName == "Cantidad1")
                            {
                                comando = comando + tablarem.Rows[jr][ir].ToString() + ",";
                            }
                            if (tablarem.Columns[ir].ColumnName == "ordenada1")
                            {
                                comando = comando + tablarem.Rows[jr][ir].ToString() + ",";
                            }
                            if (tablarem.Columns[ir].ColumnName == "Costo_AAA1")
                            {
                                comando = comando + tablarem.Rows[jr][ir].ToString() + ",";
                            }
                            if (tablarem.Columns[ir].ColumnName == "Importe_Total_AAA1")
                            {
                                comando = comando + tablarem.Rows[jr][ir].ToString() + ",";
                            }
                            if (tablarem.Columns[ir].ColumnName == "Num_OrdendeCompra1")
                            {
                                comando = comando + tablarem.Rows[jr][ir].ToString();
                            }
                        }
                        command.CommandText = comando + ")";  
                        command.ExecuteNonQuery();

                       


                    }
                     #endregion Remision
                    connection.Close();

                }
            }
            }
            catch (Exception ex)
            {
                //DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }

        }
        
        private List<ReportePedidosRemisiones> GetList()
        {
            try
            {
                int tipodereporte = 0;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<ReportePedidosRemisiones> lisInventarios = new List<ReportePedidosRemisiones>();
                ReportePedidosRemisiones semanal = new ReportePedidosRemisiones();
                //if (cmb.SelectedValue == "")
                //{
                //    tipodereporte = 0;
                //}
                //else
                //{
                //    tipodereporte = 1;
                //}
                int id_cd;
                DateTime? fecini;
                DateTime? fecfin;
                int id_prd;

                id_cd = this.txtAlmacen.Text == "" ? -1 : Convert.ToInt32(this.txtAlmacen.Text);
                fecini = dpFecha1.SelectedDate;
                fecfin = dpFechaFin.SelectedDate;
                id_prd = this.TxtId_Prd.Text == "" ? -1 : Convert.ToInt32(this.TxtId_Prd.Text);

                string cnn = sesion.Emp_Cnx;
                new CN_InvExcesoInventario().ConsultaPedidosColocados(cnn, ref lisInventarios, tipodereporte,  id_cd,  fecini,  fecfin,  id_prd,1);
                return lisInventarios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<ReportePedidosRemisiones> GetListRemisiones()
        {
            try
            {
                int tipodereporte = 0;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<ReportePedidosRemisiones> lisInventarios = new List<ReportePedidosRemisiones>();
                ReportePedidosRemisiones semanal = new ReportePedidosRemisiones();
                //if (cmb.SelectedValue == "")
                //{
                //    tipodereporte = 0;
                //}
                //else
                //{
                //    tipodereporte = 1;
                //}
                int id_cd;
                DateTime? fecini;
                DateTime? fecfin;
                int id_prd;

                id_cd = this.txtAlmacen.Text == "" ? -1 : Convert.ToInt32(this.txtAlmacen.Text);
                fecini = dpFecha1.SelectedDate;
                fecfin = dpFechaFin.SelectedDate;
                id_prd = this.TxtId_Prd.Text == "" ? -1 : Convert.ToInt32(this.TxtId_Prd.Text);

                string cnn = sesion.Emp_Cnx;
                new CN_InvExcesoInventario().ConsultaPedidosColocados(cnn, ref lisInventarios, tipodereporte, id_cd, fecini, fecfin, id_prd, 2);
                return lisInventarios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private bool ValidarFecha()
        {
            try
            {
                DateTime dt1 = DateTime.Parse(this.dpFecha1.SelectedDate.ToString());
                DateTime dt2 = DateTime.Parse(this.dpFechaFin.SelectedDate.ToString());

                if (dt1 <= dt2)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        #endregion Funciones
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