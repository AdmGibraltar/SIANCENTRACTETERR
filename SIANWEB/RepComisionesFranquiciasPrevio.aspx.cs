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
    public partial class RepComisionesFranquiciasPrevio : System.Web.UI.Page
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
                        this.CargarAnoMes();

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_Cve.Value = randObj.Next().ToString();

                         
                        CapaNegocios.CN__Comun CN_Comun2 = new CapaNegocios.CN__Comun();
                        CN_Comun2.LlenaCombo(3,Sesion.Emp_Cnx, "SpCatCdi_Combo", ref CmbId_Cd);
                         

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
        protected void CmbId_Cd_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];

            if ( CmbId_Cd.SelectedValue.ToString() == "-1") 
            {
                txtCdi_Id.Text = "";
            }
            else
            {
                txtCdi_Id.Text = CmbId_Cd.SelectedValue.ToString();
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
                            
                                this.GenerarExcel();
                                Alerta("Se genero el archivo correctamente");
 
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
       

        protected void CmbCentro_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
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
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged");
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
       
        private void GenerarExcel()
        {
            
            Random rnd = new Random();
            int numero = rnd.Next(1, 1433);

            string rutaynombre = "";
            String filename = "AnalisisVariablesComisiones" + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + numero.ToString();
            rutaynombre = System.Web.HttpContext.Current.Server.MapPath("~/Reportes") + "\\" + filename;

            ObtenerDetalle(rutaynombre);

            filename = filename+ ".xlsx";
            Response.ContentType = "application/vnd.ms-excel";
            Response.Redirect("~/Reportes/" + filename, false);
            Response.End();

            File.Delete("~/Reportes/" + filename);

        }
        
        private void ObtenerDetalle(string nombre)
        {
            try
            {
                List<ReporteComisiones> List = new List<ReporteComisiones>();
                List = GetListReporteDetalle(0);
                DataTable dtencabezado = new DataTable();
                dtencabezado = Funcion.Convertidor<ReporteComisiones>.ListaToDatatable(List);

                List<ReporteComisiones> ListDetCte = new List<ReporteComisiones>();
                ListDetCte = GetListReporteDetalle(1);
                DataTable dtDetCte = new DataTable();
                dtDetCte = Funcion.Convertidor<ReporteComisiones>.ListaToDatatable(ListDetCte);

                List<ReporteComisiones> ListDetFac = new List<ReporteComisiones>();
                ListDetFac = GetListReporteDetalle(2);
                DataTable dtdetfac = new DataTable();
                dtdetfac = Funcion.Convertidor<ReporteComisiones>.ListaToDatatable(ListDetFac);

                List<ReporteComisiones> ListDetProd = new List<ReporteComisiones>();
                ListDetProd = GetListReporteDetalle(3);
                DataTable dtDetProd = new DataTable();
                dtDetProd = Funcion.Convertidor<ReporteComisiones>.ListaToDatatable(ListDetProd);



                GeneraExcel(dtencabezado, dtDetCte, dtdetfac, dtDetProd, nombre);

                 
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

		static void GeneraExcel(DataTable tabla, DataTable tablaDetCte, DataTable tablaDetFac, DataTable tablaDetPrd, string NombreMes)
		{

 
			string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+ NombreMes + @".xlsx;Extended Properties=""Excel 12.0 Xml""";
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

					//command.CommandText = "CREATE TABLE [Resumen] (CDI number, Id Number, NombreRik char(150), NombreCD char(150),  MVI number ,  ComisionNeta number,Amortizacion number,TecSer number,Utrem number)";
                    command.CommandText = "CREATE TABLE [Resumen] (CDI number, NombreCD char(150), Año number, Mes number ,Rik Number, NombreRik char(150),   Amortizacion number, TecSer number,  Utrem number ,Comision  number ,MVI number  )";
					command.ExecuteNonQuery();


					for (int j = 0; j < tabla.Rows.Count; j++)
					{
						string comando = "";
                        //string descripcion = "";
						comando = "INSERT INTO [Resumen] VALUES(";


                        comando = comando + tabla.Rows[j]["Id_Cd"].ToString() + ",";
                        comando = comando + '\"' + tabla.Rows[j]["CdiNombre"].ToString() + '\"' + ",";
                        comando = comando + tabla.Rows[j]["Año"].ToString() + ",";
                        comando = comando + tabla.Rows[j]["Mes"].ToString() + ",";
                        comando = comando + tabla.Rows[j]["Id"].ToString() + ",";
                        comando = comando + '\"' + tabla.Rows[j]["Nombre_Empleado"].ToString() + '\"' + ",";
                        comando = comando + tabla.Rows[j]["Amortizacion"].ToString() + ",";
                        comando = comando + tabla.Rows[j]["TecSer"].ToString() + ",";
                        comando = comando + tabla.Rows[j]["UTrem"].ToString() + ",";
                        comando = comando + tabla.Rows[j]["ComisionNeta"].ToString() + ",";
                        comando = comando + tabla.Rows[j]["MVI"].ToString();
                        //descripcion = tabla.Rows[j][i].ToString();
                        //descripcion = descripcion.Replace("\"", " ");
                        //comando = comando + '\"' + descripcion + '\"' + ",";
                       
                       
                       
                        ////for (int i = 0; i < tabla.Columns.Count; i++)
                        ////{

                        ////    if (tabla.Columns[i].ColumnName == "Id_Cd")
                        ////    {
                        ////        comando = comando + tabla.Rows[j][i].ToString() + ",";
                        ////    }
                        ////    //if (tabla.Columns[i].ColumnName == "Mes")
                        ////    //{
                        ////    //    comando = comando + tabla.Rows[j][i].ToString() + ",";
                        ////    //}
                        ////    //if (tabla.Columns[i].ColumnName == "Año")
                        ////    //{
                        ////    //    comando = comando + tabla.Rows[j][i].ToString() + ",";
                        ////    //}

                        ////    if (tabla.Columns[i].ColumnName == "CdiNombre")
                        ////    {
                        ////        comando = comando + '\"' + tabla.Rows[j][i].ToString() + '\"' + ",";
                        ////    }
                        ////    if (tabla.Columns[i].ColumnName == "Id")
                        ////    {
                        ////        comando = comando + tabla.Rows[j][i].ToString() + ",";
                        ////    }
                        ////    if (tabla.Columns[i].ColumnName == "Nombre_Empleado")
                        ////    {
                        ////        descripcion = tabla.Rows[j][i].ToString();
                        ////        descripcion = descripcion.Replace("\"", " ");
                        ////        comando = comando + '\"' + descripcion + '\"' + ",";
                        ////    }
                        ////    if (tabla.Columns[i].ColumnName == "Amortizacion")
                        ////    {
                        ////        comando = comando + tabla.Rows[j][i].ToString() + ",";
                        ////    }
							
                        ////    if (tabla.Columns[i].ColumnName == "TecSer")
                        ////    {
                        ////        comando = comando + tabla.Rows[j][i].ToString() + ",";
                        ////    }
                        ////    if (tabla.Columns[i].ColumnName == "UTrem")
                        ////    {
                        ////        comando = comando + tabla.Rows[j][i].ToString()  ;
                        ////    }
                        ////    if (tabla.Columns[i].ColumnName == "ComisionNeta")
                        ////    {
                        ////        comando = comando + tabla.Rows[j][i].ToString() + ",";
                        ////    }
                        ////    if (tabla.Columns[i].ColumnName == "MVI")
                        ////    {
                        ////        comando = comando + tabla.Rows[j][i].ToString() + ",";
                        ////    }
							
                        ////}
						command.CommandText = comando + ")";
						command.ExecuteNonQuery();
					}
						#endregion OrdenCOmpra
				   
					#region Detalle Cliente

                    command.CommandText = "CREATE TABLE [Detalle por Cliente] (Id_Cd number, CdiNombre char(240),  Id_Cte number,[Nombre Cliente] char(130) , Rik number, [Importe Vta Cob] number, [Utilidad Prima]   number,Amortizacion number,GST number,[Utilidad Bruta] number, FPPP number,[Factor Rentabilidad] number,[Comisión Base] Number,Año number, Mes number)";
					command.ExecuteNonQuery();



					for (int jr = 0; jr < tablaDetCte.Rows.Count; jr++)
					{
                        string comando = "INSERT INTO [Detalle por Cliente] VALUES(";

						comando = comando + tablaDetCte.Rows[jr]["Id_Cd"].ToString() + ",";
						comando = comando + '\"' + tablaDetCte.Rows[jr]["CdiNombre"].ToString() + '\"' + ",";
						comando = comando + tablaDetCte.Rows[jr]["Id_Cte"].ToString() + ",";
						comando = comando + '\"' + tablaDetCte.Rows[jr]["Nombre_Empleado"].ToString() + '\"' + ",";

						comando = comando + tablaDetCte.Rows[jr]["Id"].ToString() + ",";
						comando = comando + tablaDetCte.Rows[jr]["VtaCob"].ToString() + ",";
						comando = comando + tablaDetCte.Rows[jr]["UP"].ToString() + ",";
						comando = comando + tablaDetCte.Rows[jr]["Amortizacion"].ToString() + ",";
						comando = comando + tablaDetCte.Rows[jr]["GST"].ToString() + ",";
						comando = comando + tablaDetCte.Rows[jr]["UB"].ToString() + ",";

						comando = comando + tablaDetCte.Rows[jr]["PPPA"].ToString() + ",";
						comando = comando + tablaDetCte.Rows[jr]["FR"].ToString() + ",";
						comando = comando + tablaDetCte.Rows[jr]["CB"].ToString() + ",";
						comando = comando + tablaDetCte.Rows[jr]["Año"].ToString() + ",";
						comando = comando + tablaDetCte.Rows[jr]["Mes"].ToString();
					 
						command.CommandText = comando + ")";
						command.ExecuteNonQuery();


					}
					 #endregion DetCliente


				#region Detalle Factura

                    command.CommandText = "CREATE TABLE [DetalleFacturas] (Id_Cd number, CdiNombre char(240), [Factura] char(40), Id_Cte number, [Cliente] char(80) , Territorio number, Rik number, [Fecha Venc.] char(40), [Fecha Pago] char(40),[Días] number,Importe number,[Utilidad Prima] number,[Mult Ajuste Cobranza] number, [Ajuste Cobranza] number,Año number, Mes number)";
					command.ExecuteNonQuery();



					for (int jr = 0; jr < tablaDetFac.Rows.Count; jr++)
					{
						string comando = "INSERT INTO [DetalleFacturas] VALUES(";

						comando = comando + tablaDetFac.Rows[jr]["Id_Cd"].ToString() + ",";
						comando = comando + '\"' + tablaDetFac.Rows[jr]["CdiNombre"].ToString() + '\"' + ",";
						comando = comando + tablaDetFac.Rows[jr]["Pag_referencia"].ToString() + ",";
						comando = comando + tablaDetFac.Rows[jr]["Id_Cte"].ToString() + ",";
						comando = comando + '\"' + tablaDetFac.Rows[jr]["Nombre_Empleado"].ToString() + '\"' + ",";
						comando = comando + tablaDetFac.Rows[jr]["Id_Territorio"].ToString() + ",";
						comando = comando + tablaDetFac.Rows[jr]["Id"].ToString() + ",";
						comando = comando + '\"' + tablaDetFac.Rows[jr]["FechaVencimiento"].ToString() + '\"' + ",";
						comando = comando + '\"' + tablaDetFac.Rows[jr]["FechaPago"].ToString() + '\"' + ",";
						comando = comando + tablaDetFac.Rows[jr]["Dias"].ToString() + ",";
						comando = comando + tablaDetFac.Rows[jr]["Importe"].ToString() + ",";
						comando = comando + tablaDetFac.Rows[jr]["UP"].ToString() + ",";
						comando = comando + tablaDetFac.Rows[jr]["Mult_Porc"].ToString() + ",";
						comando = comando + tablaDetFac.Rows[jr]["AjCobranza"].ToString() + ",";
						comando = comando + tablaDetFac.Rows[jr]["Año"].ToString() + ",";
						comando = comando + tablaDetFac.Rows[jr]["Mes"].ToString();
						
						command.CommandText = comando + ")";
						command.ExecuteNonQuery();


					}
					 #endregion Remision



                    #region Detalle Productos

                    command.CommandText = "CREATE TABLE [DetalleProductos] (Id_Cd number, CdiNombre char(240), [Factura] char(40), Id_Cte number,Cliente char(80) , Territorio number, Rik number, Producto int, Utilidad_Prima number,Año number, Mes number)";
                    command.ExecuteNonQuery();



                    for (int jr = 0; jr < tablaDetPrd.Rows.Count; jr++)
                    {
                        string comando = "INSERT INTO [DetalleProductos] VALUES(";

                        comando = comando + tablaDetPrd.Rows[jr]["Id_Cd"].ToString() + ",";
                        comando = comando + '\"' + tablaDetPrd.Rows[jr]["CdiNombre"].ToString() + '\"' + ",";
                        comando = comando + tablaDetPrd.Rows[jr]["Pag_referencia"].ToString() + ",";
                        comando = comando + tablaDetPrd.Rows[jr]["Id_Cte"].ToString() + ",";
                        comando = comando + '\"' + tablaDetPrd.Rows[jr]["Nombre_Empleado"].ToString() + '\"' + ",";
                        comando = comando + tablaDetPrd.Rows[jr]["Id_Territorio"].ToString() + ",";
                        comando = comando + tablaDetPrd.Rows[jr]["Id"].ToString() + ",";
                        comando = comando + tablaDetPrd.Rows[jr]["Id_TipoRepresentante"].ToString() + ",";
                        comando = comando + tablaDetPrd.Rows[jr]["UP"].ToString() + ",";
                        comando = comando + tablaDetPrd.Rows[jr]["Año"].ToString() + ",";
                        comando = comando + tablaDetPrd.Rows[jr]["Mes"].ToString();

                        command.CommandText = comando + ")";
                        command.ExecuteNonQuery();


                    }
                    #endregion Productos


                    connection.Close();

				}
			}
			}
			catch (Exception ex)
			{
				//DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
			}

		}

        private List<ReporteComisiones> GetListReporteDetalle(int detalle)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                CN_CatCompensacion clsCatCompensacion = new CN_CatCompensacion();

                SistemaCompensacionGetXML sistemaCompensacionGetXMLconcentrado = new SistemaCompensacionGetXML();

                sistemaCompensacionGetXMLconcentrado.Id_Emp = session.Id_Emp;
                sistemaCompensacionGetXMLconcentrado.Id_Cd = this.txtCdi_Id.Text == "" ? -1 : Convert.ToInt32(this.txtCdi_Id.Text);
                sistemaCompensacionGetXMLconcentrado.Id_Sistema = 19;
                sistemaCompensacionGetXMLconcentrado.Anio = Convert.ToInt32(this.cmbanio.SelectedValue);
                sistemaCompensacionGetXMLconcentrado.Mes = Convert.ToInt32(this.cmbmes.SelectedValue);
                sistemaCompensacionGetXMLconcentrado.MesTexto = this.cmbmes.Text;
                sistemaCompensacionGetXMLconcentrado.Id_TipoRepresentante = 3; // Convert.ToInt32(this.CmbTipo_Representante.SelectedValue);

                ReporteComisiones registrorik = new ReporteComisiones();
                List<ReporteComisiones> listaConcentrado = new List<ReporteComisiones>();

                string StrCnx = ConfigurationManager.AppSettings.Get("strConnection");
                session.Emp_Cnx = StrCnx + ";Connect Timeout=60000";

                if (detalle == 0)
                    clsCatCompensacion.ReporteConcentradoFranquicias(sistemaCompensacionGetXMLconcentrado, session.Emp_Cnx, ref  listaConcentrado);
                if (detalle == 1 )
                    clsCatCompensacion.ReporteConcentradoFranquiciasDetCliente(sistemaCompensacionGetXMLconcentrado, session.Emp_Cnx, ref  listaConcentrado);
                if (detalle == 2)
                    clsCatCompensacion.ReporteConcentradoFranquiciasDetFactura(sistemaCompensacionGetXMLconcentrado, session.Emp_Cnx, ref  listaConcentrado);
                if (detalle == 3)
                    clsCatCompensacion.ReporteConcentradoFranquiciasDetProducto(sistemaCompensacionGetXMLconcentrado, session.Emp_Cnx, ref  listaConcentrado);



                return listaConcentrado;

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
                cn_comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd, sesion.CalendarioIni.Year, sesion.Emp_Cnx, "spCatCalendarioMes_Combo", ref this.cmbmes);

                this.cmbanio.SelectedValue = DateTime.Now.Year.ToString();
                this.cmbmes.SelectedValue = (DateTime.Now.Month - 1).ToString();

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
