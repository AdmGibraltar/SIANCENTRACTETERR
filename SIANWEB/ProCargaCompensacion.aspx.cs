using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Configuration;
using System.IO;

using Telerik.Web.UI;

using CapaEntidad;
using CapaNegocios;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.OleDb;
using System.Data;


namespace SIANWEB
{
    public partial class ProCargaCompensacion : System.Web.UI.Page
    {
        #region Variables
        public string NombreArchivo;
        public string NombreHojaExcel;
        private DataTable dtDet
        {
            //Se quita variable sesion para evitar que se borren las partidas de los pagos
            // 07/11/2016
            get
            {
                return (DataTable)Session["dtDetPagos"];
            }
            set
            {
                Session["dtDetPagos"] = value;
            }
        }
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        private List<ColaboradorObjetivo> listSource
        {
            get { return (List<ColaboradorObjetivo>)Session["listSource"]; }
            set { Session["listSource"] = value; }
        }

        private List<ConceptosNomina> listConceptosNomina
        {
            get { return (List<ConceptosNomina>)Session["listConceptosNomina"]; }
            set { Session["listConceptosNomina"] = value; }
        }


        public int IdProducto
        {
            get { return Convert.ToInt32(Session["_IdProducto"]); }
            set { Session["_IdProducto"] = value; }
        }

        public string Valor
        {
            get
            {
                //return MaximoId();
                return "1";
            }
            set { }
        }

        public string ValorEmpleado
        {
            get
            {
                //return MaximoId();
                return "1";
            }
            set { }
        }

        #endregion
        #region Eventos
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!IsPostBack)
                    {
                        Session["_IdProducto"] = 0;
                        Session["listSource"] = null;
                        Session["listConceptosNomina"] = null;
                        this.ValidarPermisos();
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("(function(){var f = function(){AbrirContrasenas(); return false;Sys.Application.remove_load(f);};Sys.Application.add_load(f);})()");
                            return;
                        }
                       
                            Inicializar();
                       
                        
                        this.RadMultiPagePrincipal.SelectedIndex = 0;

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        #region RgDet Eventos

        protected void RgDet_PageIndexChanged_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.RgDet.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "RgDet_PageIndexChanged"));
            }
        }
        #endregion grdPrecios Eventos

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            //rgPagoElectronico.Rebind();
        }


        #region grdConceptoNomina Eventos
        protected void grdConceptoNomina_PreRender(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                //foreach (GridDataItem item in rgPrecios.MasterTableView.Items)
                //{
                //    if (Convert.ToBoolean(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Estatus"]))
                //    {   //si es precio actual, se colorea de azul el Row                    
                //        foreach (TableCell cell in item.Cells)
                //        {
                //            cell.CssClass = "styleCellRowAGridPrecios";
                //        }
                //    }
                //    else //Se quita la capacidad de edición del precio                   
                //        item["EditCommandColumn"].Controls[0].Visible = false;
                //}
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void grdConceptoNomina_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                GridEditFormItem editedItem = e.Item as GridEditFormItem;

                ConceptosNomina productoPrecio = new ConceptosNomina();
                productoPrecio.Id_Emp = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Emp"]);
                productoPrecio.Id_Cd = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Cd"]);
                productoPrecio.Id_Colaborador = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Colaborador"]);
                productoPrecio.Id_Compensacion = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Compensacion"]);

                //productoPrecio.Compensacion_Estatus = Convert.ToInt32(((Literal)editedItem["Compensacion_Estatus"].Controls[1]).Text);
                productoPrecio.Id_Compensacion_Monto = Convert.ToInt32(((Literal)editedItem["Id_Compensacion_Monto"].Controls[1]).Text);
                productoPrecio.Id_Empleado = Convert.ToInt32(((Literal)editedItem["Id_empleado"].Controls[1]).Text);
                productoPrecio.Monto = Convert.ToSingle((editedItem["Monto"].FindControl("txtMonto") as RadNumericTextBox).Text);
                

                List<ConceptosNomina> listaProdPre = new List<ConceptosNomina>();
                for (int i = 0; i < this.listConceptosNomina.Count; i++)
                    listaProdPre.Add((ConceptosNomina)this.ClonarPrecioProducto(this.listConceptosNomina[i]));
   
                //Agregar precio a la lista actual
                foreach (ConceptosNomina p in this.listConceptosNomina)
                {
                    if (p.Id_Compensacion_Monto == productoPrecio.Id_Compensacion_Monto) // && p.Estatus == productoPrecio.Estatus && p.Estatus == true)
                    {
                        List<ConceptosNomina> listaPP = new List<ConceptosNomina>(this.listConceptosNomina);
                        //int posicionFila = grdConceptoNomina.CurrentPageIndex * grdConceptoNomina.PageSize + e.Item.ItemIndex;
                        //listaPP[posicionFila].Monto = productoPrecio.Monto;
                        //listaPP[posicionFila].Id_Emp = productoPrecio.Id_Emp;
                        //listaPP[posicionFila].Id_Cd = productoPrecio.Id_Cd;
                        //listaPP[posicionFila].Id_Colaborador = productoPrecio.Id_Colaborador;
                        //listaPP[posicionFila].Id_Compensacion = productoPrecio.Id_Compensacion;

                        //listaPP[posicionFila].Compensacion_Estatus = productoPrecio.Compensacion_Estatus;
                        //listaPP[posicionFila].Id_Compensacion_Monto = productoPrecio.Id_Compensacion_Monto;
                        //listaPP[posicionFila].Id_Empleado = productoPrecio.Id_Empleado;
                        //listaPP[posicionFila].Compensacion_Descripcion = productoPrecio.Compensacion_Descripcion;

                        this.listConceptosNomina = listaPP;
               
                        break;
                    }
                }
                
            }
            catch (Exception ex)
            {  //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        //protected void grdConceptoNomina_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{
        //    try
        //    {
        //        ErrorManager();
        //        if (e.RebindReason == GridRebindReason.ExplicitRebind)
        //            //grdConceptoNomina.DataSource = this.listConceptosNomina;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.DisplayMensajeAlerta(ex.Message);
        //    }
        //}
        //protected void grdConceptoNomina_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    try
        //    {
        //        ErrorManager();
        //        if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
        //        {
        //            GridEditFormItem editItem = (GridEditFormItem)e.Item;

        //            //string datePickerFechaInicio = ((RadDatePicker)editItem.FindControl("datePickerFechaInicio")).ClientID.ToString();
        //            //string datePickerFechaFin = ((RadDatePicker)editItem.FindControl("datePickerFechaFin")).ClientID.ToString();
        //            //string txtObjetivo = ((RadNumericTextBox)editItem.FindControl("txtObjetivo")).ClientID.ToString();

        //            //string jsControles = string.Concat(
        //            //    "datePickerFechaInicioClientId='", datePickerFechaInicio, "';"
        //            //    , "datePickerFechaFinClientId='", datePickerFechaFin, "';"
        //            //    , "txtObjetivoClientId='", txtObjetivo, "';"
        //            //    );

        //            //ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
        //            //if (insertbtn != null)
        //            //{
        //            //    jsControles = string.Concat(
        //            //        jsControles
        //            //        , "return ValidaFormGridPrecioProductos(\"insertar\");");

        //            //    insertbtn.Attributes.Add("onclick", jsControles);
        //            //}

        //            //ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
        //            //if (updatebtn != null)
        //            //{
        //            //    jsControles = string.Concat(
        //            //        jsControles
        //            //        , "return ValidaFormGridPrecioProductos(\"actualizar\");");

        //            //    updatebtn.Attributes.Add("onclick", jsControles);
        //            //}
        //        }
        //    }
        //    catch (Exception ex)
        //    {   //RadGrid1.Controls.Add(new LiteralControl("No se pudo agregar el Usuario. " + ex.Message));
        //        //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
        //        this.DisplayMensajeAlerta(ex.Message);
        //    }
        //}
        //protected void grdConceptoNomina_PageIndexChanged(object source, GridPageChangedEventArgs e)
        //{
        //    try
        //    {
        //        ErrorManager();
        //        this.grdConceptoNomina.Rebind();
        //    }
        //    catch (Exception ex)
        //    {
        //        DisplayMensajeAlerta(string.Concat(ex.Message, "grdConceptoNomina_PageIndexChanged"));
        //    }
        //}
        #endregion grdConceptoNomina Eventos


        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "new":
                        this.LimpiarCampos();
                       
                        this.NuevoProducto();
                        break;
                    case "save":
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
      
       
       
        #endregion
        #region Funciones
        public object ClonarPrecioProducto(object obj)
        {
            object objResult = null;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Position = 0;
                objResult = bf.Deserialize(ms);
            }
            return objResult;
        }
        private void Inicializar()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
 
            this.hiddenId.Value = string.Empty;

            System.Text.StringBuilder str = new System.Text.StringBuilder();
            List<SubFamProducto> listaSF = new List<SubFamProducto>();
            SubFamProducto subFamilia = new SubFamProducto();
            new CN_CatSubFamProducto().ConsultaSubFamProducto(subFamilia, sesion.Emp_Cnx, sesion.Id_Emp, ref listaSF);
            str.Append(string.Concat("arregloSubFamilias = new Array(3);"));
            str.Append(string.Concat("arregloSubFamilias[0] = new Array(", listaSF.Count, ");"));
            str.Append(string.Concat("arregloSubFamilias[1] = new Array(", listaSF.Count, ");"));
            str.Append(string.Concat("arregloSubFamilias[2] = new Array(", listaSF.Count, ");"));
            for (int i = 0; i < listaSF.Count; i++)
            {
                subFamilia = listaSF[i];
                str.Append(string.Concat("arregloSubFamilias[0][", i.ToString(), "] = '", subFamilia.Id_Fam, "';"));
                str.Append(string.Concat("arregloSubFamilias[1][", i.ToString(), "] = '", subFamilia.Id_Sub, "';"));
                str.Append(string.Concat("arregloSubFamilias[2][", i.ToString(), "] = '", subFamilia.Sub_Descripcion, "';"));
            }
            this.listSource = new List<ColaboradorObjetivo>();
            this.listConceptosNomina = new List<ConceptosNomina>();

            this.NuevoProducto();

            GetListDet();

            if (Session["IdLocal" + Session.SessionID] != null)
            {
               
                Session["IdLocal" + Session.SessionID] = null;

               
            }
          
            RAM1.ResponseScripts.Add(string.Concat(@"", str.ToString()));
        }
       

      

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
                        this.RadToolBar1.Items[6].Visible = true;

                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                        this.RadToolBar1.Items[5].Visible = false;

                    //Regresar
                    this.RadToolBar1.Items[4].Visible = false;
                    //Eliminar
                    this.RadToolBar1.Items[3].Visible = false;
                    //Imprimir
                    this.RadToolBar1.Items[2].Visible = false;
                    //Correo
                    this.RadToolBar1.Items[1].Visible = false;

                    this.RadToolBar1.Items[6].Visible = false;

                }
                else
                    Response.Redirect("Inicio.aspx");

                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void NuevoProducto()
        {   //rgPrecios.
            this.listSource = this.ConsultarPorductoPrecios(0);
            //rgPrecios.DataSource = this.listSource;
            //rgPrecios.DataBind();

            //this.listConceptosNomina = this.ConsultarConceptosColaborador(0);
            //grdConceptoNomina.DataSource = this.listConceptosNomina;
            //grdConceptoNomina.DataBind();

            

            this.hiddenId.Value = string.Empty;

            //Nuevo producto:
            //deshabilta controles de pestaña de compras locales
         
        }

        private void Guardar()
        {
            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatColaborador clsCatColaborador = new CN_CatColaborador();
                int verificador = -1;

                //aqui hago un ciclo en el datasource y por cada registro inserto un valor 
                // pero no seria en insertar y tampoco en ModificarColaborador 
                // si no que creo otro metodo donde solo le mando los conceptos 

                //Colaborador colaborador = this.LlenatObjetoColaborador();
                Colaborador colaborador = new Colaborador();
                colaborador.Id_Emp = session.Id_Emp;

                colaborador.Id_Cd = session.Id_Cd_Ver;


                //colaborador.Id_Emp = 1; //TODO  txtEmpresa.Text == string.Empty ? 0 : Convert.ToInt32(txtEmpresa.Text);
                //colaborador.Id_Sucursal = txtIdSucursal.Text ; //== string.Empty ? 0 : Convert.ToInt32(txtIdSucursal.Text);

                colaborador.ListaColaboradorObjetivos = this.listSource;
                colaborador.ListaConceptosNomina = this.listConceptosNomina;
                //colaborador.ListaConceptosNomina.Clear();
                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permisos para guardar");
                    return;
                }


                foreach (DataRow item in dtDet.Rows)
                {
                    string nomina = item["Nomina"].ToString();
                    int numnomina = Convert.ToInt32(nomina);

                    colaborador.Id_Empleado = numnomina;

                    //Limpiar los conceptos porque esta duplicandolos 
                    colaborador.ListaConceptosNomina.Clear();

                    for (int i = 0; i < 4; i++)
                    {
                        
                        ConceptosNomina conceptomonto = new ConceptosNomina();
                        conceptomonto.Id_Empleado = colaborador.Id_Empleado;
                        conceptomonto.EsEditable = 1;
                        conceptomonto.Id_Compensacion = i+1;
                        if ( i+1 == 1 )
                            conceptomonto.Monto = Convert.ToSingle(item["Sueldo"]);
                        if ( i+1 == 2 )
                            conceptomonto.Monto = Convert.ToSingle(item["CuentadeGastos"]);
                        if (i+1 == 3)
                        conceptomonto.Monto = Convert.ToSingle(item["Despensa"]);
                        if (i+1 == 4) 
                            conceptomonto.Monto = Convert.ToSingle(item["Gasolina"]);
                        //conceptomonto.Monto5 = Convert.ToSingle(item["TOTAL"].Text);
                        colaborador.ListaConceptosNomina.Add(conceptomonto);
                    }
               
 
                    //List<ConceptosNomina> listaProdPre = new List<ConceptosNomina>();
                    //for (int i = 0; i < this.listConceptosNomina.Count; i++)
                    //    listaProdPre.Add((ConceptosNomina)this.ClonarPrecioProducto(this.listConceptosNomina[i]));

 
              
                    clsCatColaborador.GuardarCargaMasivaConceptosMonto(colaborador, session.Emp_Cnx, ref verificador);

                }

                    this.LimpiarCampos();
                    this.DisplayMensajeAlerta("CatColaborador_Update_Ok");
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LimpiarCampos()
        {
            
      
            this.listSource = this.ConsultarPorductoPrecios(0);
            //this.listConceptosNomina = this.ConsultarConceptosColaborador(0);
        
        }

        private void LlenarFormularioProducto(int id_colaborador, int id_Cd_Prod)
        {
            try
            {
                Colaborador colaborador = ConsultarColaboradorEmpleado(id_colaborador, id_Cd_Prod);
          
                //JFCV TODO Consultar los objetivos del Usuario
                this.listSource = this.ConsultarPorductoPrecios(id_colaborador);
                this.listConceptosNomina = this.ConsultarConceptosColaborador(id_colaborador);
                
          
                //**********************************//
                //* Consultar Objetivos del Usuario  *//
                //**********************************//
                this.IdProducto = id_colaborador;
     
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Colaborador LlenatObjetoColaborador()
        {
            Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
            Colaborador colaborador = new Colaborador();

            ///JFCV TODO agarrar la empresa de el combo 
            colaborador.Id_Emp = session.Id_Emp;
            
            colaborador.Id_Cd = session.Id_Cd_Ver;
          
            //colaborador.Id_Emp = 1; //TODO  txtEmpresa.Text == string.Empty ? 0 : Convert.ToInt32(txtEmpresa.Text);
            //colaborador.Id_Sucursal = txtIdSucursal.Text ; //== string.Empty ? 0 : Convert.ToInt32(txtIdSucursal.Text);
       
            colaborador.ListaColaboradorObjetivos = this.listSource;
            colaborador.ListaConceptosNomina = this.listConceptosNomina;

            return colaborador;
        }

        private Colaborador ConsultarColaboradorEmpleado(int num_nomina,   int id_emp_carga)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatColaborador clsCatProducto = new CN_CatColaborador();
                Colaborador colaborador = new Colaborador();
                clsCatProducto.ConsultaEmpleadoPorNumeroNomina(ref colaborador, sesion.Emp_Cnx, id_emp_carga,   num_nomina, true);
                return colaborador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<ColaboradorObjetivo> ConsultarPorductoPrecios(int id_Colaborador)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<ColaboradorObjetivo> list = new List<ColaboradorObjetivo>();
                ColaboradorObjetivo objetivos = new ColaboradorObjetivo();
                objetivos.Id_Emp = sesion.Id_Emp;
                objetivos.Id_Cd = sesion.Id_Cd_Ver;
                objetivos.Id_Colaborador = id_Colaborador;

                new CN_CatColaborador().ConsultaListaObjetivos(objetivos, sesion.Emp_Cnx, ref list);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private List<ConceptosNomina> ConsultarConceptosColaborador(int id_Colaborador)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<ConceptosNomina> list = new List<ConceptosNomina>();
                ConceptosNomina objetivos = new ConceptosNomina();
                objetivos.Id_Emp = sesion.Id_Emp;
                objetivos.Id_Cd = sesion.Id_Cd_Ver;
                objetivos.Id_Colaborador = id_Colaborador;

                new CN_CatColaborador().ConsultaListaConceptosNomina(objetivos, sesion.Emp_Cnx, ref list);
                return list;
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
            else if (mensaje.Contains("CatColaborador_Update_Ok"))
                Alerta(string.Concat("Se actualizo la información en la Base de Datos.<br/>", ""));
            else
                Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }
        #endregion



        #region Subir Archivo 

        protected void RadAsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            try
            {
                Label fileName = new Label();
                fileName.Text = e.File.FileName;
                NombreArchivo = fileName.Text;
                NombreHojaExcel = e.File.GetNameWithoutExtension().ToString();
                if (e.IsValid)
                {
                    ValidFiles.Visible = true;
                    ValidFiles.Controls.Add(fileName);
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }

        protected void btnImportar_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + NombreArchivo;

                if (NombreArchivo != null)
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                    foreach (UploadedFile f in RadUpload1.UploadedFiles)
                    {
                        f.SaveAs(path, true);
                    }

                    OleDbConnection con = default(OleDbConnection);

                    string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0 xml;HDR=YES;IMEX=1;\"";
                    con = new OleDbConnection(strConn);
                    con.Open();
                    DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string hoja = dt.Rows[0].ItemArray[2].ToString().Replace("'", "");
                    OleDbCommand cmd = new OleDbCommand("select * from [" + hoja + "]", con);
                    OleDbDataAdapter dad = new OleDbDataAdapter();
                    dad.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    try
                    {
                        dad.Fill(ds);
                    }
                    catch (Exception ex)
                    {
                        Alerta(ex.Message);

                    }
                    con.Close();


                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                    string EMPRESA = "";
                    string Nomina = "";
                    string NOMBRE = "";
                    string PUESTO = "";
                    Double? Sueldo = 0;
                    Double CuentadeGastos = 0;
                    Double Despensa = 0;
                    Double Gasolina = 0;
                    Double TOTAL = 0;
                    int SeCargaron = 0;
                    int NoSeCargaron = 0;

                    dtDet.Clear();

                    int x = 0;



                    foreach (DataRow row in ds.Tables[0].Rows)
                    {

                        EMPRESA = "";
                        Nomina = "";
                        NOMBRE = "";
                        PUESTO = "";
                        Sueldo = 0;
                        CuentadeGastos = 0;
                        Despensa = 0;
                        Gasolina = 0;
                        TOTAL = 0;



                        //INICIA CONVERSION
                        EMPRESA = Convert.ToString(row[0]);
                        Nomina = Convert.ToString(row[1]);
                        NOMBRE = Convert.ToString(row[2]);
                        PUESTO = Convert.ToString(row[3]);
                        Sueldo = Convert.ToDouble(row[4]);
                        CuentadeGastos = Convert.ToDouble(row[5]);
                        Despensa = Convert.ToDouble(row[6]);

                        Gasolina = Convert.ToDouble(row[7]);

                        TOTAL = Convert.ToDouble(row[8]);



                        #region validar

                        //if (Convert.ToString(row[1]) == "1")
                        //{
                        int id_empresa = 0;

                        if (EMPRESA == "KEY")
                            id_empresa = 1;
                        else
                            id_empresa = 2;



                        ////if (Doc_Estatus == "B")
                        ////{
                        ////    Alerta("Movimiento se encuentra cancelado; imposible aplicarle un pago " + MovStr + " " + Convert.ToString(row[2]) + " " + Convert.ToString(row[3]));
                        ////    return;
                        ////}
                        ////else if (Doc_Estatus == "C")
                        ////{
                        ////    Alerta("Movimiento se encuentra en status capturado; imposible aplicarle un pago " + MovStr + " " + Convert.ToString(row[2]) + " " + Convert.ToString(row[3]));
                        ////    return;
                        ////}

                        ////if (Math.Round(Convert.ToDouble(Doc_Importe) - Convert.ToDouble(Doc_Pagado), 2) <= 0)
                        ////{
                        ////    Alerta("Movimiento no tiene saldo positivo; imposible aplicarle un pago " + MovStr + " " + Convert.ToString(row[2]) + " " + Convert.ToString(row[3]));
                        ////    return;
                        ////}


                        #endregion validar


                        Colaborador colaborador = ConsultarColaboradorEmpleado(Convert.ToInt32(Nomina), id_empresa);
                        if (colaborador.Id_Empleado != 0)
                        {
                            //jfcv el sueldo no lo multiplico por el 30.4
                            //Sueldo = colaborador.Sueldo_Variable * 30.4;
                            Sueldo = colaborador.Sueldo_Variable ;
                            ///que busque el numero de nomina y vea si coincide el nombre con el de la bd 
                            ///que busqeu el puesto y mande warnings 
                            ///
                            x++;
                            dtDet.Rows.Add(
                                x,
                                EMPRESA,      
                                Nomina,                         
                                NOMBRE, 
                                PUESTO,               
                                Sueldo,                      
                                CuentadeGastos,      
                                Despensa,                    
                                Gasolina,                      
                                TOTAL
                                );


                            SeCargaron++;
                        }
                        else
                        {
                            NoSeCargaron++;
                        }
                    }
                    RgDet.Rebind();
                    ///AQUI
                    RgDet.DataSource = null;
                    RgDet.Rebind();
                    RgDet.DataSource = dtDet;
                    RgDet.Rebind();
                    if (NoSeCargaron > 0)
                        Alerta("Se Importaron Registros : " + SeCargaron.ToString() + "<BR></BR>" + "Empleados no encontrados : " + NoSeCargaron.ToString());
                    else
                        Alerta("Se Importaron Registros : " + SeCargaron.ToString());

                    ////txtTotal.Text = Total.ToString();
                }
                else
                {
                    Alerta("Seleccione la plantilla a cargar. ");
                }

            }

            catch (Exception ex)
            {
                Alerta(ex.Message.Replace("'", ""));
            }
        }
        private void GetListDet()
        {
            try
            {
                dtDet = new DataTable();
                dtDet.Columns.Add("RgDId", System.Type.GetType("System.Int32"));
                dtDet.Columns.Add("EMPRESA", System.Type.GetType("System.String"));
                dtDet.Columns.Add("Nomina", System.Type.GetType("System.String"));
                dtDet.Columns.Add("NOMBRE", System.Type.GetType("System.String"));
                dtDet.Columns.Add("PUESTO", System.Type.GetType("System.String"));
                //dtDet.Columns.Add("Id_Terr", System.Type.GetType("System.Int32"));
                
                //dtDet.Columns.Add("Doc_Fecha", System.Type.GetType("System.DateTime"));
                dtDet.Columns.Add("Sueldo", System.Type.GetType("System.Double"));
                dtDet.Columns.Add("CuentadeGastos", System.Type.GetType("System.String"));
                dtDet.Columns.Add("Despensa", System.Type.GetType("System.Double"));
                dtDet.Columns.Add("Gasolina", System.Type.GetType("System.Double"));
                dtDet.Columns.Add("TOTAL", System.Type.GetType("System.Double"));
 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion Subir archivo 
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