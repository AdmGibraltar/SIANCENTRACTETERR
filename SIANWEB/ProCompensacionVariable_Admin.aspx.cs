using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;

namespace SIANWEB
{
    public partial class ProCompensacionVariable_Admin : System.Web.UI.Page
    {

        #region Variables

        private bool _PermisoGuardar
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                {
                    return false;
                }
                return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }

        private bool _PermisoModificar
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                {
                    return false;
                }
                return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }

        private bool _PermisoEliminar
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                {
                    return false;
                }
                return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }

        private bool _PermisoImprimir
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                {
                    return false;
                }
                return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        LlenarCombos();
                        CargarCentros();
                        CargarCDIS();
                        txtFecha1.DbSelectedDate = Sesion.CalendarioIni;
                        txtFecha2.DbSelectedDate = Sesion.CalendarioFin;

                        double ancho = 0;
                        foreach (GridColumn gc in rgPedido.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgPedido.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgPedido.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgPedido.Rebind();
                        if (!Sesion.Cu_Modif_Pass_Voluntario)
                        {
                            RAM1.ResponseScripts.Add("(function(){var f = function(){AbrirContrasenas(); return false;Sys.Application.remove_load(f);};Sys.Application.add_load(f);})()");
                        }
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Page_Load");
            }
        }

        protected void RAM1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            try
            {
                ErrorManager();
                GridItem gi = (Session["Gi" + Session.SessionID] as GridItem);
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":
                        if (Session["PreguntarImpresion" + Session.SessionID] != null)
                        {
                            RAM1.ResponseScripts.Add("return Confirma();");
                        }
                        rgPedido.Rebind();
                        break;
                    case "ok":
       
                        break;
                    case "no":
                        Session["PreguntarImpresion" + Session.SessionID] = null;
                        break;
                    default:
                        rgPedido.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }

        protected void rg_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
        }

        protected void rg_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgPedido.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rg_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            rgPedido.Rebind();
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
                txtFecha1.DbSelectedDate = sesion.CalendarioIni;
                txtFecha2.DbSelectedDate = sesion.CalendarioFin;
                rgPedido.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                rgPedido.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

      


        protected void rgPedido_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridItem item = e.Item;
                List<string> statusPosibles = new List<string>();
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                SistemaCompensacion sistemacompensacion = new SistemaCompensacion();

                int itemindex = e.Item.ItemIndex;

                sistemacompensacion.Id_Sistema = Int32.Parse(rgPedido.Items[itemindex]["Id_Sistema"].Text);
                sistemacompensacion.Id_Emp = Int32.Parse(rgPedido.Items[itemindex]["Id_Emp"].Text);
                sistemacompensacion.Id_Cd = Int32.Parse(rgPedido.Items[itemindex]["Id_Cd"].Text);

                //if (((RadTextBox)(rgPedido.Items[itemindex]["MotivoRechazo"].FindControl("TxtMotivoRechazo"))).Text != string.Empty)
                //{
                //    reciboremision.MotivoRechazo = Convert.ToString(((RadTextBox)(rgPedido.Items[itemindex]["MotivoRechazo"].FindControl("TxtMotivoRechazo"))).Text);
                //}


                switch (e.CommandName)
                {
                        ////JFCV todo
                    case "Copiar":
                      

                        int vId_Sistema = Convert.ToInt32((e.Item as GridDataItem)["Id_Sistema"].Text);
                        
                        CN_CatCompensacion clscompensacion = new CN_CatCompensacion();
                        SistemaCompensacion sistema = new SistemaCompensacion();
                        sistema.Id_Emp = Sesion.Id_Emp;
                        sistema.Id_Cd = Sesion.Id_Cd;
                        sistema.Id_Sistema = vId_Sistema;
                        int verificador = 0;
                        clscompensacion.CopiarConfiguracionSistemacompensacion(sistema, Sesion.Emp_Cnx, ref verificador);


                        if (verificador > 0)
                        {
                            Alerta("El sistema ha sido copiado , se genero el nuevo sistema. " + verificador.ToString());
                            return;
                        }
                        else
                        {
                            Alerta("Ocurrio un error al generar la copia."  );
                        }
                       break;
                    case "Delete":
                        #region Rechazar
                        //Sesion sessionr = new Sesion();
                        //sessionr = (Sesion)Session["Sesion" + Session.SessionID];
                     
                        //int vId_Sistemacancelar = Convert.ToInt32((e.Item as GridDataItem)["Id_Sistema"].Text);
                        
                        //CN_CatCompensacion clscompensacion2 = new CN_CatCompensacion();
                        //SistemaCompensacion sistema2 = new SistemaCompensacion();
                        //sistema2.Id_Emp = Sesion.Id_Emp;
                        //sistema2.Id_Cd = Sesion.Id_Cd;
                        //sistema2.Id_Sistema = vId_Sistemacancelar;
                        //int verificador2 = 0;
                        //clscompensacion2.CopiarConfiguracionSistemacompensacion(sistema2, Sesion.Emp_Cnx, ref verificador2);


                        //if (verificador2 > 0)
                        //{
                        //    Alerta("El sistema ha sido desactivado. " );
                        //    return;
                        //}
                        //else
                        //{
                        //    Alerta("Ocurrio un error al generar la copia."  );
                        //}

                        #endregion Rechazar
                        
                        break;

                    case "Soporte":

                        RAM1.ResponseScripts.Add("return AbrirVentana_ComponentesModificar(" + sistemacompensacion.Id_Sistema.ToString() + ")");
                         
                        break;

                    case "Simular":

                        RAM1.ResponseScripts.Add("return AbrirVentana_ComponentesImprimir(" + sistemacompensacion.Id_Sistema.ToString() + ")");

                        break;
                 
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rgPedido_ItemCommand");
            }
        }

        
        protected void rgPago_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                WebControl Button = default(WebControl);
                string clickHandler = "";

          
            }
        }
        protected void ImgExportar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //rgPedido.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), "ExcelML");
                //rgPedido.ExportSettings.IgnorePaging = true;
                //rgPedido.ExportSettings.ExportOnlyData = true;
                //rgPedido.ExportSettings.OpenInNewWindow = true;
                //rgPedido.ExportSettings.FileName = "Listado remisiones";
                //rgPedido.MasterTableView.ExportToExcel();

                //rgPedido.PageSize = RadGrid1.MasterTableView.VirtualItemCount;
                //rgPedido.ExportSettings.IgnorePaging = true;
                //rgPedido.ExportSettings.OpenInNewWindow = true;
                //rgPedido.ExportSettings.FileName = "Listado remisiones";
                //rgPedido.MasterTableView.ExportToExcel();
                GenerarExcel();
            }
            catch (Exception ex)
            {

                ErrorManager(ex, "ImgExportar_Click");
            }

        }
        

        #endregion

        #region Funciones

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

                    //boton toolbar "nuevo"     
                    rtb1.Items.FindItemByValue("new").Visible = true;
                    rtb1.Items.FindItemByValue("remisionPedido").Visible = false;

                    if (Permiso.PModificar)
                    {
                        //columna editar
                    }
                    //rgPedido.Columns.FindByUniqueName("Editar").Visible = Permiso.PModificar;

                    if (Permiso.PEliminar)
                    {
                        //columna borrar
                        //((RadToolBarItem)RadToolBar1.Items.FindItemByValue("delete")).Enabled = _PermisoEliminar;
                    }
                  
                    //rgPedido.Columns.FindByUniqueName("Autorizar").Visible = _PermisoGuardar;
                 
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

      

        private List<SistemaCompensacion> GetList()
        {
            try
            {
                List<SistemaCompensacion> remisiones = new List<SistemaCompensacion>();
                SistemaCompensacion remision = new SistemaCompensacion();
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatCompensacion cn_reciboremision = new CN_CatCompensacion();
                cn_reciboremision.ConsultarSistemaCompensacion(ref remisiones, ref remision, session,
                   txtNombre.Text,
                   txtFecha1.SelectedDate, txtFecha2.SelectedDate, cmbEstatus.SelectedValue, //<==
                   this.CmbCDI.SelectedValue == "-1" ? -1 : int.Parse(this.CmbCDI.SelectedValue));
                return remisiones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LlenarCombos()
        {

            cmbEstatus.Items.Insert(0, new RadComboBoxItem("Inactivo", "0"));
            cmbEstatus.Items.Insert(0, new RadComboBoxItem("Activo", "1"));
            cmbEstatus.Items.Insert(0, new RadComboBoxItem("-- Todos --", ""));
            
            //cmbEstatus.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", ""));
            this.cmbEstatus.Sort = RadComboBoxSort.Ascending;
            this.cmbEstatus.SortItems();
            cmbEstatus.SelectedIndex = 0;
        }
 
  
      
        private void MesNombre(int fechaMes, ref string mes)
        {
            if (fechaMes > 0)
            {
                switch (fechaMes)
                {
                    case 1:
                        mes = "Enero";
                        break;
                    case 2:
                        mes = "Febrero";
                        break;
                    case 3:
                        mes = "Marzo";
                        break;
                    case 4:
                        mes = "Abril";
                        break;
                    case 5:
                        mes = "Mayo";
                        break;
                    case 6:
                        mes = "Junio";
                        break;
                    case 7:
                        mes = "Julio";
                        break;
                    case 8:
                        mes = "Agosto";
                        break;
                    case 9:
                        mes = "Septiembre";
                        break;
                    case 10:
                        mes = "Octubre";
                        break;
                    case 11:
                        mes = "Noviembre";
                        break;
                    case 12:
                        mes = "Diciembre";
                        break;
                }
            }
        }

      
        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
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

         

        private bool Tm_ReqSpo
        {
            set
            {
                Session["Tm_ReqSpoREM" + Session.SessionID] = value;
            }
            get
            {
                return (bool)Session["Tm_ReqSpoREM" + Session.SessionID];
            }
        }
 
        private void GenerarExcel()
        {
            try
            {

                StringBuilder tabla = new StringBuilder();
                Funcion fn = new Funcion();
                tabla.Append("<html><head><meta http-equiv='Content-Type' content='text/html; charset=ISO-8859-1'></head><body><table style='width:700px'>");
                EscribeDetalle(ref tabla);
                tabla.Append("</table></body></html>");
                fn.ExportarExcel("Listado_Remisiones", tabla.ToString());

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void EscribeDetalle(ref StringBuilder Tabla)
        {
            try
            {
                String width;


                List<SistemaCompensacion> List = new List<SistemaCompensacion>();
                List = GetList();
                DataTable dt = new DataTable();

                dt = Funcion.Convertidor<SistemaCompensacion>.ListaToDatatable(List);

                Tabla.Append("<tr>");

                //lectura y escritura de columnas
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    if (dt.Columns[i].ColumnName == "Id_Cd")
                    {
                        width = (i == 0) ? "60px" : "90px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Sucursal");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Id_Cte")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Núm. Cte.");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "NombreSistema")
                    {
                        width = (i == 0) ? "180px" : "210px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Cliente");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Id_Rem")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Número Remisión");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Rec_Fecha_Remision")
                    {
                        width = (i == 0) ? "70px" : "90px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Fecha Remision");
                        Tabla.Append("</th>");
                    }
                    
                    else if (dt.Columns[i].ColumnName == "Rec_Importe")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Importe Soporte");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Rec_Importe_Remision")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Importe Remisión");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Rec_Observaciones")
                    {
                        width = (i == 0) ? "220px" : "240px";
                        Tabla.Append("<th  align = 'Left' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Observaciones");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Id_Recibo")
                    {
                        width = (i == 0) ? "130px" : "150px";
                        Tabla.Append("<th  align = 'Left' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Comprobante");
                        Tabla.Append("</th>");
                    }
                   
                    else if (dt.Columns[i].ColumnName == "Rec_NombreEstatus")
                    {
                        width = (i == 0) ? "70px" : "90px";
                        Tabla.Append("<th  align = 'Left' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Estatus");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Rec_Fecha_EnvioValidacion")
                    {
                        width = (i == 0) ? "70px" : "90px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Fecha Envio Validación");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "MotivoRechazo")
                    {
                        width = (i == 0) ? "180px" : "210px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("MotivoRechazo");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Rec_Validacion")
                    {
                        width = (i == 0) ? "180px" : "210px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Validado");
                        Tabla.Append("</th>");
                    }



                }
                Tabla.Append("</tr>");
                // lectura y escritura de filas
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    Tabla.Append("<tr>");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        if (dt.Columns[i].ColumnName == "Id_Cd")
                        {
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Id_Cte")
                        {
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "NombreSistema")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Id_Rem")
                        {
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Rec_Fecha_Remision")
                        {
                            DateTime datetime = Convert.ToDateTime(dt.Rows[j][i].ToString());
                            Tabla.Append("<td   style='text-align:center'>");
                            Tabla.Append(datetime.ToShortDateString());
                            Tabla.Append("</td>");
                        }

                        else if (dt.Columns[i].ColumnName == "Rec_Importe")
                        {
                            double valor = double.Parse(dt.Rows[j][i].ToString());
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(valor.ToString("N2"));
                            Tabla.Append("</td>");
                        }

                        else if (dt.Columns[i].ColumnName == "Rec_Importe_Remision")
                        {
                            double valor = double.Parse(dt.Rows[j][i].ToString());
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(valor.ToString("N2"));
                            Tabla.Append("</td>");
                        }   

                        else if (dt.Columns[i].ColumnName == "Rec_Observaciones")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Id_Recibo")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }

                        else if (dt.Columns[i].ColumnName == "Rec_NombreEstatus")
                        {
                            Tabla.Append("<td   style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Rec_Fecha_EnvioValidacion")
                        {
                            DateTime datetime = Convert.ToDateTime(dt.Rows[j][i].ToString());
                            Tabla.Append("<td   style='text-align:center'>");
                            Tabla.Append(datetime.ToShortDateString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "MotivoRechazo")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Rec_Validacion")
                        {
                            Tabla.Append("<td   style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                         
                       

                    }
                }
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td>");
                Tabla.Append("&nbsp; &nbsp;</td>");
                Tabla.Append("</tr>");



            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    this.lblMensaje.Text = string.Empty;
                    RadToolBarButton btn = e.Item as RadToolBarButton;
                    if (btn.CommandName == "new")
                    {
                        RAM1.ResponseScripts.Add("return AbrirVentana_CapturaComponentes('-1', '" + _PermisoGuardar + "', '" + _PermisoModificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "')");
                         
                    }
                    else if (btn.CommandName == "undo")
                    {
                        //Regresar()
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }



        #endregion

        #region ErrorManager

        private void RadConfirm(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radconfirm('" + mensaje + "', confirmCallBackFn);");
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
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
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
        //JFCV
        private void CargarCDIS()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun cn_comun = new CN__Comun();
                cn_comun.LlenaCombo(1, sesion.Emp_Cnx, "spCatCDI_ComboTodos", ref CmbCDI);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

           
    }
}