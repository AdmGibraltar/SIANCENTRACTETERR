using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using CapaNegocios;
using CapaDatos;
using System.Globalization;
using Telerik.Web.UI;
using System.Collections;

namespace SIANWEB
{
    public partial class CapAcys : System.Web.UI.Page
    {
        #region Variables
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
        private DataTable dtAcuerdos
        {
            get
            {
                return (DataTable)Session["dtAcuerdos" + Session.SessionID];
            }
            set
            {
                Session["dtAcuerdos" + Session.SessionID] = value;
            }
        }

        private int _Accion
        {
            get
            {
                return (int)Session["SesionAccion" + Session.SessionID];
            }
            set
            {
                Session["SesionAccion" + Session.SessionID] = value;
            }

        }
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        bool producto = false;
        private DataTable Seleccionados
        {
            get { return (DataTable)Session["SeleccionadosAcys" + Session.SessionID]; }
            set { Session["SeleccionadosAcys" + Session.SessionID] = value; }
        }
        private List<Producto> List_Productos
        {
            get { return (List<Producto>)Session["Servicios" + Session.SessionID]; }
            set { Session["Servicios" + Session.SessionID] = value; }
        }

        private List<Producto> List_ProductosMantenimiento
        {
            get { return (List<Producto>)Session["ServiciosMantenimiento" + Session.SessionID]; }
            set { Session["ServiciosMantenimiento" + Session.SessionID] = value; }
        }

        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (sesion == null)
                    CerrarVentana();
                else
                    if (!Page.IsPostBack)
                    {
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        Inicializar();


                        if (!((RadToolBarItem)rtb1.Items.FindItemByValue("save")).Visible)
                        {
                            deshabilitarcontroles(divGenerales.Controls);
                            deshabilitarcontroles(divAcuerdosE.Controls);
                            GridCommandItem cmdItem = (GridCommandItem)rgAcuerdos.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                            ((Button)cmdItem.FindControl("AddNewRecordButton")).Visible = false;// remove the image button 
                            ((LinkButton)cmdItem.FindControl("InitInsertButton")).Visible = false;//remove the link button  

                            rgAcuerdos.MasterTableView.Columns[rgAcuerdos.MasterTableView.Columns.Count - 1].Display = false;
                            rgAcuerdos.MasterTableView.Columns[rgAcuerdos.MasterTableView.Columns.Count - 2].Display = false;
                        }

                        double ancho = 0;
                        foreach (GridColumn gc in rgAcuerdos.Columns)
                        {
                            if (gc.Display && gc.Visible)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        if (rgAcuerdos.Items.Count > 6)
                        {
                            ancho += 17;
                        }
                        rgAcuerdos.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgAcuerdos.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));

                        if (txtCliente.Text != "")
                        {
                            List_Productos = GetListServicios();
                            rgServicios.Rebind();
                            List_ProductosMantenimiento = GetListServiciosMantenimiento();
                            rgMantPrevRev.Rebind();
                        }
                        rgAsesoria.Rebind();
                    }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void deshabilitarcontroles(System.Web.UI.ControlCollection controles_contenidos)
        {
            for (int x = 0; x < controles_contenidos.Count; x++)
            {
                string Type = controles_contenidos[x].GetType().FullName;

                if (Type.Contains("RadMultiPage") || Type.Contains("RadPageView") || Type.Contains("Panel"))
                {
                    deshabilitarcontroles(controles_contenidos[x].Controls);
                }

                switch (Type.Replace("Telerik.Web.UI.", ""))
                {
                    case "RadNumericTextBox":
                        (controles_contenidos[x] as RadNumericTextBox).Enabled = false;
                        break;
                    case "RadTextBox":
                        (controles_contenidos[x] as RadTextBox).Enabled = false;
                        break;
                    case "RadComboBox":
                        (controles_contenidos[x] as RadComboBox).Enabled = false;
                        break;
                    case "RadDatePicker":
                        (controles_contenidos[x] as RadDatePicker).Enabled = false;
                        break;
                    case "RadDateTimePicker":
                        (controles_contenidos[x] as RadDateTimePicker).Enabled = false;
                        break;
                }
                if (Type.Contains("CheckBox"))
                {
                    (controles_contenidos[x] as CheckBox).Enabled = false;
                }

                if (Type.Contains("ImageButton"))
                {
                    (controles_contenidos[x] as ImageButton).Enabled = false;
                }
            }
        }
        protected void rgAcuerdos_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgAcuerdos.DataSource = dtAcuerdos;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "save")
                    if (Page.IsValid)
                        Guardar();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbRepresentante_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                AcysPrevio();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadDatePicker2_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                RadDatePicker objFecha = sender as RadDatePicker;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                DateTime fechaInicioPeriodo = sesion.CalendarioIni;

                if (fechaInicioPeriodo > Convert.ToDateTime(objFecha.SelectedDate))
                {
                    RAM1.ResponseScripts.Add("radalert('La fecha inicio de Vigencia no puede ser menor al periodo actual', 330, 150);");
                    objFecha.DbSelectedDate = null;
                    return;
                }      
                txtSemana.Text = GetWeekNumber(rdVigenciaIni.SelectedDate.Value);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbTer_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                AcysPrevio();
                CargarRik();
                //CargarClientes();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbProductoDet_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                RadNumericTextBox cmbProd = sender as RadNumericTextBox;
                Producto prd = new Producto();
                CN_CatProducto cnProducto = new CN_CatProducto();
                try
                {
                    cnProducto.ConsultaProducto(ref prd, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Id_Cd_Ver, Convert.ToInt32(cmbProd.Value.HasValue ? cmbProd.Value.Value : -1));
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, (sender as RadNumericTextBox).ClientID);
                    return;
                }
                (cmbProd.Parent.FindControl("txtProductoNombre") as RadTextBox).Text = prd.Prd_Descripcion;
                (cmbProd.Parent.FindControl("lblUniEd") as Label).Text = prd.Prd_UniNs;//(cmbProd.SelectedItem.FindControl("lblUni") as Label).Text;
                (cmbProd.Parent.FindControl("lblPresentacionEd") as Label).Text = prd.Prd_Presentacion;//(cmbProd.SelectedItem.FindControl("lblPre") as Label).Text;
                (cmbProd.Parent.FindControl("txtPrecio") as RadNumericTextBox).DbValue = prd.Prd_Precio; //(cmbProd.SelectedItem.FindControl("lblPrecio") as Label).Text;
                (cmbProd.Parent.FindControl("txtPrecio") as RadNumericTextBox).Focus();

                if (prd.Prd_Descripcion == null)
                {                   
                    
                    AlertaFocus("El producto no existe o esta deshabilitado", (sender as RadNumericTextBox).ClientID);
                    return;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void rgAcuerdos_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgAcuerdos.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        break;
                    case "PerformInsert":
                        PerformInsert(e);
                        break;
                    case "Update":
                        Update(e);
                        break;
                    case "Delete":
                        Delete(e);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void rgServicios_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgServicios.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        break;
                    case "PerformInsert":
                        PerformInsertService(e);
                        break;
                    case "Update":
                        UpdateService(e);
                        break;
                    case "Delete":
                        DeleteService(e);
                        break;
                 
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void rgMantPrevRev_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgMantPrevRev.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        break;
                    case "PerformInsert":
                        PerformInsertMantenimiento(e);
                        break;
                    case "Update":
                        UpdateMantenimiento(e);
                        break;
                    case "Delete":
                        DeleteMantenimiento(e);
                        break;

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void cmbProducto_DataBinding(object sender, EventArgs e)
        {
            try
            {
                if (producto)
                    return;
                producto = true;
                RadComboBox cmbProducto = sender as RadComboBox;
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatProducto cn_catproducto = new CN_CatProducto();
                List<Producto> list = new List<Producto>();
                Producto prd = new Producto();
                prd.Id_Cd = sesion.Id_Cd_Ver;
                prd.Id_Emp = sesion.Id_Emp;
                int Id_Acs = HF_ID.Value == "" ? -1 : Convert.ToInt32(HF_ID.Value);
                cn_catproducto.ConsultaProductoCte_Lista(prd, sesion.Emp_Cnx, Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1), Id_Acs, ref list);

                cmbProducto.DataSource = list;
                cmbProducto.DataValueField = "Id_Prd";
                cmbProducto.DataTextField = "Prd_Descripcion";
                cmbProducto.DataBind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AcysPrevio();
                //Limpiar();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Clientes cte = new Clientes();
                cte.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value : -1);
                cte.Id_Emp = sesion.Id_Emp;
                cte.Id_Cd = sesion.Id_Cd_Ver;
                cte.Id_Rik = txtRepresentante.Value.HasValue ? (int)txtRepresentante.Value.Value : (sesion.Id_Rik > 0 ? sesion.Id_Rik : 0);
                CN_CatCliente cnCliente = new CN_CatCliente();
                try
                {
                    cnCliente.ConsultaClientes(ref cte, sesion.Emp_Cnx);
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, txtCliente.ClientID);
                    txtClienteNombre.Text = "";
                    txtCliente.Text = "";
                    if (cmbTerritorio.Items.Count > 0)
                    {
                        cmbTerritorio.SelectedIndex = 0;
                        cmbTerritorio.Text = cmbTerritorio.Items[0].Text;

                    } txtTerritorio.Value = null;
                    Limpiar();
                    return;
                }
             
                txtClienteNombre.Text = cte.Cte_NomComercial;
                txtClienteDireccion.Text = cte.Cte_FacCalle;
                txtClienteColonia.Text = cte.Cte_FacColonia;
                txtClienteMunicipio.Text = cte.Cte_FacMunicipio;
                txtClienteEstado.Text = cte.Cte_FacEstado;
                txtClienteRFC.Text = cte.Cte_FacRfc;
                txtClienteCodPost.Text = cte.Cte_FacCp;
                
                CheckCuentaCorporativa.Checked = cte.Id_Corp > 1 ? true : false;
                ChkbAdendaSI.Checked = cte.Id_Ade > 0 ? true : false;


                //Información Comercial  
                txtComercial.Text = cte.Cte_NomComercial;
                txtDireccionEntrega.Text = cte.Cte_Calle;
                txtClienteColoniaE.Text = cte.Cte_Colonia;
                txtClienteMunicipioE.Text = cte.Cte_Municipio;
                txtClienteEstadoE.Text = cte.Cte_Estado;
                txtClienteCPE.Text = cte.Cte_Cp;
                //txtProveedor.Text = cte.;
                txtContacto.Text = cte.Cte_Contacto;
                //txtPuesto.Text = cte.ct;
                txtTelefono.Text = cte.Cte_Telefono;
                txtEmail.Text = cte.Cte_Email;


                CargarTerritorios();
                if (txtCliente.Text != "")
                {
                    CargarCondiciones();
                    List_Productos = GetListServicios();
                    rgServicios.Rebind();
                    List_ProductosMantenimiento = GetListServiciosMantenimiento();
                    rgMantPrevRev.Rebind();

                }


            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgAcuerdos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;
                ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                if (updatebtn != null)
                {
                    ((RadNumericTextBox)editItem.FindControl("RadNumericTextBox2")).Enabled = false;
                }
            }
        }
        protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RAM1.ResponseScripts.Add("popup();");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                ErrorManager();
                switch (e.Argument.ToString())
                {
                    case "cliente":
                        txtCliente.Text = Session["Id_Buscar" + Session.SessionID].ToString();
                        txtCliente_TextChanged(null, null);
                        break;
                    case "productos":
                        break;
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 90);


                        RadPageCliente.Height = altura;
                        RadSplitter1.Height = altura;
                        RadPane1.Height = altura;
                        RadPane1.Width = RadPageCliente.Width;


                        RPVRecepcionPedido.Height = altura;
                        RadSplitter2.Height = altura;
                        RadPane2.Height = altura;
                        RadPane2.Width = RPVRecepcionPedido.Width;

                        RPVAcuerdosEconomicos.Height = altura;
                        RadSplitter3.Height = altura;
                        RadPane3.Height = altura;
                        RadPane3.Width = RPVAcuerdosEconomicos.Width;
                       

                        RPVCondicionesPago.Height = altura;
                        RadSplitter4.Height = altura;
                        RadPane4.Height = altura;
                        RadPane4.Width = RPVCondicionesPago.Width;



                        RPVServicio.Height = altura;
                        RadSplitter5.Height = altura;                        
                        RadPane5.Height = altura;
                        RadPane5.Width = RPVServicio.Width;

                        RPVOtrosApoyos.Height = altura;
                        RadSplitter6.Height = altura;
                        RadPane6.Height = altura;
                        RadPane6.Width = RPVOtrosApoyos.Width;
                        

                        txtCliente.Focus();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgAcuerdos_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                try
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    LinkButton imgButton = (LinkButton)item.FindControl("hlEquivalencias");
                    string id_Prd = item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"].ToString();
                    string Id_Acs = txtFolio.Text == "" ? "-1" : txtFolio.Text;
                    imgButton.OnClientClick = "return popup2('" + id_Prd + "', '" + Id_Acs + "')";
                }
                catch (Exception)
                {

                }
            }
        }
        protected void rgAsesoria_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgAsesoria.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgServicios_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgServicios.DataSource = List_Productos;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgServicios_ItemDeleted(object source, GridDeletedEventArgs e)
        {

        }

        protected void rgMantPrevRev_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgMantPrevRev.DataSource = List_ProductosMantenimiento;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void cargarCboUsuarios()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoAseServ);
                this.ContactoAseServ.SelectedValue = "0";
                CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoCAlmRep);
                this.ContactoCAlmRep.SelectedValue = "0";
                CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoCCreCob);
                this.ContactoCCreCob.SelectedValue = "0";
                CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoCServTec);
                this.ContactoCServTec.SelectedValue = "0";
                CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoJefOper);
                this.ContactoJefOper.SelectedValue = "0";
                CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoJefServ);
                this.ContactoJefServ.SelectedValue = "0";
                CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoRepServ);
                this.ContactoRepServ.SelectedValue = "0";
                CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.ContactoRepVenta);
                this.ContactoRepVenta.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }  

        protected void cboUsuario_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
               ErrorManager();
               Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
               CN_CatUsuario clsCatUsuario = new CN_CatUsuario();
               string Correo = "";
               Usuario usuario = new Usuario();
               RadComboBox objUsuario = o as RadComboBox;               
               usuario.Id_Cd_Ver = sesion.Id_Cd_Ver;               
               usuario.Id_Cd = sesion.Id_Cd_Ver;
               usuario.Id_Emp = sesion.Id_Emp;

              RadTextBox emailTextBox =  (objUsuario.Parent.FindControl(objUsuario.ID + "Email") as RadTextBox);

              if (objUsuario.SelectedValue != string.Empty)
              {
                  if (Convert.ToInt32(objUsuario.SelectedValue) > 0)
                  {
                      usuario.Id_U =  Convert.ToInt32(objUsuario.SelectedValue);
                      clsCatUsuario.ConsultaCorreoUsuario(usuario, sesion.Emp_Cnx, ref Correo);
                    
                  }
              }
              emailTextBox.Text = Correo;
                
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }


        protected void Rb_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton rdb = sender as RadioButton;
                RadDatePicker dpGeneric = (rdb.Parent.FindControl(rdb.ID + "fechaIni") as RadDatePicker);
                dpGeneric.Enabled = true;
                string type = "";

                ControlCollection td = rdb.Parent.Controls as ControlCollection;

                foreach (Control control in td)
                {
                    if (control.GetType().ToString() == "Telerik.Web.UI.RadDatePicker")                   
                    {
                        if (control.ID != rdb.ID + "fechaIni")
                        {
                            dpGeneric = (rdb.Parent.FindControl(control.ID) as RadDatePicker);
                            dpGeneric.Enabled = false;
                            dpGeneric.DbSelectedDate = null;
                        }                        
                    }
                }             

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }



        protected void ChkServAsesoria_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AsesoriaListado.Visible = ChkServAsesoria.Checked;
                              
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void ChkServTecnicoRelleno_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                EquipoRellenoListado.Visible = ChkServTecnicoRelleno.Checked;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void ChkServMantenimiento_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                MantenimientoPreventivoListado.Visible = ChkServMantenimiento.Checked;


            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void ValidarFechaInicio_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e) 
        {

            RadDatePicker objFecha = sender as RadDatePicker;
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            DateTime fechaInicioPeriodo = sesion.CalendarioIni;

            if (fechaInicioPeriodo > Convert.ToDateTime(objFecha.SelectedDate))
            {                            
                    RAM1.ResponseScripts.Add("radalert('La fecha inicio no puede ser menor al periodo actual', 330, 150);");
                    objFecha.DbSelectedDate = null;
                    return;
            }           
            
        }


      
        protected void DeleteMantenimiento(GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            int Id_Prd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]);
            int Prd_AgrupadoSpo = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Prd_AgrupadoSpo"]);
            List_ProductosMantenimiento.Remove(List_ProductosMantenimiento.Where(Producto => Producto.Id_Prd == Id_Prd).ToArray()[0]);
        }

        protected void DeleteService(GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            int Id_Prd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]);
            int Prd_AgrupadoSpo = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Prd_AgrupadoSpo"]);
            List_Productos.Remove(List_Productos.Where(Producto => Producto.Id_Prd == Id_Prd).ToArray()[0]);
        }




        protected void PerformInsertService(GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            int Id_Prd = Convert.ToInt32((item.FindControl("txtCodigoEdit") as RadNumericTextBox).Value);
            int Prd_AgrupadoSpo = Convert.ToInt32((item.FindControl("txtAgrupadoSpoEdit") as RadNumericTextBox).Value);
            string Prd_Descripcion = (item.FindControl("txtProductoEdit") as RadTextBox).Text;
            int Cantidad = Convert.ToInt32((item.FindControl("txtCantidadEdit") as RadNumericTextBox).Value);            

            
            DateTime? FechaInicioBimestral;
            DateTime? FechaInicioTrimestral;


            bool Bimestral = Convert.ToBoolean((item.FindControl("ServTecnicoRellenoBimestralEdit") as RadioButton).Checked);
            FechaInicioBimestral = Convert.ToDateTime((item.FindControl("ServTecnicoRellenoBimestralEditfechaIni") as RadDatePicker).SelectedDate);
            RadDatePicker DateBimestral = (item.FindControl("ServTecnicoRellenoBimestralEditfechaIni") as RadDatePicker);

            bool Trimestral = Convert.ToBoolean((item.FindControl("ServTecnicoRellenoTrimestralEdit") as RadioButton).Checked);
            FechaInicioTrimestral = Convert.ToDateTime((item.FindControl("ServTecnicoRellenoTrimestralEditfechaIni") as RadDatePicker).SelectedDate);
            RadDatePicker DateTrimestral = (item.FindControl("ServTecnicoRellenoTrimestralEditfechaIni") as RadDatePicker);



            if(Cantidad < 1) {
                e.Canceled = true;
                this.Alerta("Favor de capturar un numero mayor a 0 en cantidad");
                return;
            }

            if (!DateBimestral.SelectedDate.HasValue && !DateTrimestral.SelectedDate.HasValue) {
                e.Canceled = true;
                this.Alerta("Favor de capturar una fecha");
                return;
            
            }

            if (List_Productos.Where(Producto => Producto.Id_Prd == Id_Prd).ToArray().Length > 0)
            {
                Alerta("El producto ya ha sido agregado");
                return;
            }
            else
            {
                Producto prd = new Producto();
                prd.Id_Prd = Id_Prd;
                prd.Prd_AgrupadoSpo = Prd_AgrupadoSpo;
                prd.Prd_Descripcion = Prd_Descripcion;
                prd.Prd_InvInicial = Cantidad;
                prd.ServTecnicoRellenoBimestral = Bimestral;
                prd.ServTecnicoRellenoTrimestral = Trimestral;

                if (Bimestral)
                {
                    DateTrimestral.DbSelectedDate = null;
                    prd.ServTecnicoRellenoBimestralfechaIni = FechaInicioBimestral;
                }


                if (Trimestral)
                {
                    DateBimestral.DbSelectedDate = null;
                    prd.ServTecnicoRellenoTrimestralfechaIni = FechaInicioTrimestral;
                }
                List_Productos.Add(prd);
            }

        }


        protected void PerformInsertMantenimiento(GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            int Id_Prd = Convert.ToInt32((item.FindControl("txtCodigoEdit") as RadNumericTextBox).Value);
            int Prd_AgrupadoSpo = Convert.ToInt32((item.FindControl("txtAgrupadoSpoEdit") as RadNumericTextBox).Value);
            string Prd_Descripcion = (item.FindControl("txtProductoEdit") as RadTextBox).Text;
            int Cantidad = Convert.ToInt32((item.FindControl("txtCantidadEdit") as RadNumericTextBox).Value);
            
            DateTime? FechaInicioMensual;
            DateTime? FechaInicioBimestral;
            DateTime? FechaInicioTrimestral;

            bool Mensual = Convert.ToBoolean((item.FindControl("ServMantenimientoMensualEdit") as RadioButton).Checked);
            FechaInicioMensual = Convert.ToDateTime((item.FindControl("ServMantenimientoMensualEditfechaIni") as RadDatePicker).SelectedDate);
            RadDatePicker DateMensual = (item.FindControl("ServMantenimientoMensualEditfechaIni") as RadDatePicker);
            bool Bimestral = Convert.ToBoolean((item.FindControl("ServMantenimientoBimestralEdit") as RadioButton).Checked);
            FechaInicioBimestral = Convert.ToDateTime((item.FindControl("ServMantenimientoBimestralEditfechaIni") as RadDatePicker).SelectedDate);
            RadDatePicker DateBimestral = (item.FindControl("ServMantenimientoBimestralEditfechaIni") as RadDatePicker);
            bool Trimestral = Convert.ToBoolean((item.FindControl("ServMantenimientoTrimestralEdit") as RadioButton).Checked);
            FechaInicioTrimestral = Convert.ToDateTime((item.FindControl("ServMantenimientoTrimestralEditfechaIni") as RadDatePicker).SelectedDate);
            RadDatePicker DateTrimestral = (item.FindControl("ServMantenimientoTrimestralEditfechaIni") as RadDatePicker);

            if (Cantidad < 1)
            {
                e.Canceled = true;
                this.Alerta("Favor de capturar un numero mayor a 0 en cantidad");
                return;
            }

            if (!DateMensual.SelectedDate.HasValue && !DateBimestral.SelectedDate.HasValue && !DateTrimestral.SelectedDate.HasValue)
            {
                e.Canceled = true;
                this.Alerta("Favor de capturar una fecha");
                return;
            }


            if (List_ProductosMantenimiento.Where(Producto => Producto.Id_Prd == Id_Prd).ToArray().Length > 0)
            {
                Alerta("El producto ya ha sido agregado");
                return;
            }
            else
            {
                Producto prd = new Producto();
                prd.Id_Prd = Id_Prd;
                prd.Prd_AgrupadoSpo = Prd_AgrupadoSpo;
                prd.Prd_Descripcion = Prd_Descripcion;
                prd.Prd_InvInicial = Cantidad;
                
             
                prd.ServMantenimientoMensual = Mensual;
                prd.ServMantenimientoBimestral = Bimestral;
                prd.ServMantenimientoTrimestral = Trimestral;
               

                if (Mensual)
                {
                    DateTrimestral.DbSelectedDate = null;
                    DateBimestral.DbSelectedDate = null;
                    prd.ServMantenimientoMensualfechaIni = FechaInicioMensual;
                }

                if (Bimestral)
                {
                    DateMensual.DbSelectedDate = null;
                    DateTrimestral.DbSelectedDate = null;
                    prd.ServMantenimientoBimestralfechaIni = FechaInicioBimestral;
                }


                if (Trimestral)
                {
                    DateMensual.DbSelectedDate = null;
                    DateBimestral.DbSelectedDate = null;
                    prd.ServMantenimientoTrimestralfechaIni = FechaInicioTrimestral;
                }

                List_ProductosMantenimiento.Add(prd);
            }

        }


        protected void UpdateMantenimiento(GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            int Id_Prd = Convert.ToInt32((item.FindControl("txtCodigoEdit") as RadNumericTextBox).Value);
            int Prd_AgrupadoSpo = Convert.ToInt32((item.FindControl("txtAgrupadoSpoEdit") as RadNumericTextBox).Value);
            string Prd_Descripcion = (item.FindControl("txtProductoEdit") as RadTextBox).Text;
            int Cantidad = Convert.ToInt32((item.FindControl("txtCantidadEdit") as RadNumericTextBox).Value);
            DateTime? FechaInicioMensual;
            DateTime? FechaInicioBimestral;
            DateTime? FechaInicioTrimestral;

            bool Mensual = Convert.ToBoolean((item.FindControl("ServMantenimientoMensualEdit") as RadioButton).Checked);
            FechaInicioMensual = Convert.ToDateTime((item.FindControl("ServMantenimientoMensualEditfechaIni") as RadDatePicker).SelectedDate);
            RadDatePicker DateMensual = (item.FindControl("ServMantenimientoMensualEditfechaIni") as RadDatePicker);
            bool Bimestral = Convert.ToBoolean((item.FindControl("ServMantenimientoBimestralEdit") as RadioButton).Checked);         
            FechaInicioBimestral = Convert.ToDateTime((item.FindControl("ServMantenimientoBimestralEditfechaIni") as RadDatePicker).SelectedDate);
            RadDatePicker DateBimestral = (item.FindControl("ServMantenimientoBimestralEditfechaIni") as RadDatePicker);
            
            bool Trimestral = Convert.ToBoolean((item.FindControl("ServMantenimientoTrimestralEdit") as RadioButton).Checked);           
            FechaInicioTrimestral = Convert.ToDateTime((item.FindControl("ServMantenimientoTrimestralEditfechaIni") as RadDatePicker).SelectedDate);
            RadDatePicker DateTrimestral = (item.FindControl("ServMantenimientoTrimestralEditfechaIni") as RadDatePicker);

            if (Cantidad < 1)
            {
                e.Canceled = true;
                this.Alerta("Favor de capturar un numero mayor a 0 en cantidad");
                return;
            }

            if (!DateMensual.SelectedDate.HasValue && !DateBimestral.SelectedDate.HasValue && !DateTrimestral.SelectedDate.HasValue)
            {
                e.Canceled = true;
                this.Alerta("Favor de capturar una fecha");
                return;
            }

            if (List_ProductosMantenimiento.Where(Producto => Producto.Id_Prd == Id_Prd).ToArray().Length > 0)
            {
                List_ProductosMantenimiento.Remove(List_ProductosMantenimiento.Where(Producto => Producto.Id_Prd == Id_Prd).ToArray()[0]);
            }

            Producto prd = new Producto();
            prd.Id_Prd = Id_Prd;
            prd.Prd_AgrupadoSpo = Prd_AgrupadoSpo;
            prd.Prd_Descripcion = Prd_Descripcion;
           
            prd.Prd_InvInicial = Cantidad;
            prd.ServMantenimientoMensual = Mensual;
            prd.ServMantenimientoBimestral = Bimestral;            
            prd.ServMantenimientoTrimestral = Trimestral;


            if (Mensual)
            {
                DateTrimestral.DbSelectedDate = null;
                DateBimestral.DbSelectedDate = null;
                prd.ServMantenimientoMensualfechaIni = FechaInicioMensual;
            }

            if (Bimestral)
            {
                DateMensual.DbSelectedDate = null;
                DateTrimestral.DbSelectedDate = null;
                prd.ServMantenimientoBimestralfechaIni = FechaInicioBimestral;
            }


            if (Trimestral)
            {
                DateMensual.DbSelectedDate = null;
                DateBimestral.DbSelectedDate = null;
                prd.ServMantenimientoTrimestralfechaIni = FechaInicioTrimestral;
            }

            List_ProductosMantenimiento.Add(prd);
        }




        protected void UpdateService(GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            int Id_Prd = Convert.ToInt32((item.FindControl("txtCodigoEdit") as RadNumericTextBox).Value);
            int Prd_AgrupadoSpo = Convert.ToInt32((item.FindControl("txtAgrupadoSpoEdit") as RadNumericTextBox).Value);
            string Prd_Descripcion = (item.FindControl("txtProductoEdit") as RadTextBox).Text;
            int Cantidad = Convert.ToInt32((item.FindControl("txtCantidadEdit") as RadNumericTextBox).Value);
            //int Revision = Convert.ToInt32((item.FindControl("txtRevisionEdit") as RadNumericTextBox).Value);


            DateTime? FechaInicioBimestral;
            DateTime? FechaInicioTrimestral;


            bool Bimestral = Convert.ToBoolean((item.FindControl("ServTecnicoRellenoBimestralEdit") as RadioButton).Checked);
            FechaInicioBimestral = Convert.ToDateTime((item.FindControl("ServTecnicoRellenoBimestralEditfechaIni") as RadDatePicker).SelectedDate);
            RadDatePicker  DateBimestral = (item.FindControl("ServTecnicoRellenoBimestralEditfechaIni") as RadDatePicker);


            bool Trimestral = Convert.ToBoolean((item.FindControl("ServTecnicoRellenoTrimestralEdit") as RadioButton).Checked);
            FechaInicioTrimestral = Convert.ToDateTime((item.FindControl("ServTecnicoRellenoTrimestralEditfechaIni") as RadDatePicker).SelectedDate);
            RadDatePicker DateTrimestral = (item.FindControl("ServTecnicoRellenoTrimestralEditfechaIni") as RadDatePicker);

            if (Cantidad < 1) {
                e.Canceled = true;
                this.Alerta("Favor de capturar un numero mayor a 0 en cantidad");
                return;
            }

            if (!DateBimestral.SelectedDate.HasValue && !DateTrimestral.SelectedDate.HasValue) {
                e.Canceled = true;
                this.Alerta("Favor de capturar una fecha");
                return;            
            }

            if (List_Productos.Where(Producto => Producto.Id_Prd == Id_Prd).ToArray().Length > 0)
            {
                List_Productos.Remove(List_Productos.Where(Producto => Producto.Id_Prd == Id_Prd).ToArray()[0]);
            }

            Producto prd = new Producto();
            prd.Id_Prd = Id_Prd;
            prd.Prd_AgrupadoSpo = Prd_AgrupadoSpo;
            prd.Prd_Descripcion = Prd_Descripcion;
            prd.Prd_InvInicial = Cantidad;        
            prd.ServTecnicoRellenoBimestral = Bimestral;
            prd.ServTecnicoRellenoTrimestral = Trimestral;

            if (Bimestral) { 
                DateTrimestral.DbSelectedDate = null;                
                prd.ServTecnicoRellenoBimestralfechaIni = FechaInicioBimestral;
            }


            if (Trimestral) { 
                DateBimestral.DbSelectedDate = null;                
                prd.ServTecnicoRellenoTrimestralfechaIni = FechaInicioTrimestral;
            }
            List_Productos.Add(prd);
        }
        protected void rgServicios_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;
                ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                if (updatebtn != null)
                {
                    ((RadNumericTextBox)editItem.FindControl("txtCodigoEdit")).Enabled = false;

                    bool Bimestral = Convert.ToBoolean((editItem.FindControl("ServTecnicoRellenoBimestralEdit") as RadioButton).Checked);
                    RadDatePicker DateBimestral = (editItem.FindControl("ServTecnicoRellenoBimestralEditfechaIni") as RadDatePicker);
                     bool Trimestral = Convert.ToBoolean((editItem.FindControl("ServTecnicoRellenoTrimestralEdit") as RadioButton).Checked);
                    RadDatePicker DateTrimestral = (editItem.FindControl("ServTecnicoRellenoTrimestralEditfechaIni") as RadDatePicker);

                    if (Bimestral)
                    {
                        DateBimestral.Enabled = true;
                        DateTrimestral.DbSelectedDate = null;

                    }

                   
                    if (Trimestral)
                    {
                        DateTrimestral.Enabled = true;
                        DateBimestral.DbSelectedDate = null;
                    }
        
                }
            }
        }


        protected void rgMantPrevRev_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;
                ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                if (updatebtn != null)
                {
                    ((RadNumericTextBox)editItem.FindControl("txtCodigoEdit")).Enabled = false;
                    bool Mensual = Convert.ToBoolean((editItem.FindControl("ServMantenimientoMensualEdit") as RadioButton).Checked);
                    RadDatePicker DateMensual = (editItem.FindControl("ServMantenimientoMensualEditfechaIni") as RadDatePicker);

                    bool Bimestral = Convert.ToBoolean((editItem.FindControl("ServMantenimientoBimestralEdit") as RadioButton).Checked);
                    RadDatePicker DateBimestral = (editItem.FindControl("ServMantenimientoBimestralEditfechaIni") as RadDatePicker);
                    bool Trimestral = Convert.ToBoolean((editItem.FindControl("ServMantenimientoTrimestralEdit") as RadioButton).Checked);
                    RadDatePicker DateTrimestral = (editItem.FindControl("ServMantenimientoTrimestralEditfechaIni") as RadDatePicker);

                    if (Mensual) 
                    {
                        DateMensual.Enabled = true;
                        DateBimestral.DbSelectedDate = null;
                        DateTrimestral.DbSelectedDate = null;                       
                    }

                    if (Bimestral)
                    {
                        DateBimestral.Enabled = true;
                        DateMensual.DbSelectedDate = null;
                        DateTrimestral.DbSelectedDate = null;
                    }


                    if (Trimestral)
                    {
                        DateTrimestral.Enabled = true;
                        DateBimestral.DbSelectedDate = null;
                        DateMensual.DbSelectedDate = null;
                    }

                }
            }
        }
        #endregion
        #region Funciones
        private List<Asesoria> GetList()
        {
            try
            {
                List<Asesoria> List = new List<Asesoria>();
                CN_CapAcys clsCapAcys = new CN_CapAcys();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Acys acys = new Acys();
                acys.Id_Emp = session2.Id_Emp;
                acys.Id_Cd = session2.Id_Cd_Ver;
                acys.Id_Acs = (int)txtFolio.Value.Value;
                clsCapAcys.ConsultaAsesorias(acys, session2.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<Producto> GetListServicios()
        {
            try
            {
                List<Producto> List = new List<Producto>();
                CN_CapAcys clsCapAcys = new CN_CapAcys();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Acys acys = new Acys();
                acys.Id_Emp = session2.Id_Emp;
                acys.Id_Cd = session2.Id_Cd_Ver;
                acys.Id_Cte = (int)txtCliente.Value.Value;
                acys.Id_Ter = (int)txtTerritorio.Value.Value;
                acys.Id_Acs = (int)txtFolio.Value.Value;
                clsCapAcys.ConsultaEstBi(acys, session2.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private List<Producto> GetListServiciosMantenimiento()
        {
            try
            {
                List<Producto> List = new List<Producto>();
                CN_CapAcys clsCapAcys = new CN_CapAcys();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Acys acys = new Acys();
                acys.Id_Emp = session2.Id_Emp;
                acys.Id_Cd = session2.Id_Cd_Ver;
                acys.Id_Cte = (int)txtCliente.Value.Value;
                acys.Id_Ter = (int)txtTerritorio.Value.Value;
                acys.Id_Acs = (int)txtFolio.Value.Value;
                clsCapAcys.ConsultaEstBiMantenimiento(acys, session2.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarCondiciones()
        {
            CN_CatCliente cn_catcliente = new CN_CatCliente();
            Clientes cte = new Clientes();
            cte.Id_Emp = sesion.Id_Emp;
            cte.Id_Cd = sesion.Id_Cd_Ver;
            cte.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1);
            try
            {
                cn_catcliente.ConsultaClientes(ref cte, sesion.Emp_Cnx);
               /* if (_PermisoGuardar == false)
                    this.rtb1.Items[5].Visible = false;
                else
                    this.rtb1.Items[5].Visible = true;
                if (_PermisoGuardar == false & _PermisoModificar == false)
                    this.rtb1.Items[5].Visible = false;
                else
                    this.rtb1.Items[5].Visible = true;*/
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
                this.rtb1.Items[5].Visible = false;
            }

            /** CONDICIONES DE PAGO *****/
            chkCredito.Checked = cte.Cte_Credito;
            txtDias.Text = cte.Cte_CondPago.ToString();
            txtLimite.Text = cte.Cte_LimCobr.ToString();
            chkContado.Checked = cte.Cte_Contado;


            //FORMAS DE PAGO
            chkEfectivo.Checked = cte.Cte_Efectivo;
            chkFactoraje.Checked = cte.Cte_Factoraje;
            chkTransferencia.Checked = cte.Cte_Transferencia;
            chkCheque.Checked = cte.Cte_Cheque;
            ChkTarjetaDebito.Checked = cte.Cte_TarjetaDebito;
            ChkTarjetaCredito.Checked = cte.Cte_TarjetaDebito;       
            ChkDeposito.Checked = cte.Cte_Deposito;


                //REVISION
            chkRevisionLunes.Checked = cte.Cte_RLunes;
            chkRevisionMartes.Checked = cte.Cte_RMartes;
            chkRevisionMiercoles.Checked = cte.Cte_RMiercoles;
            chkRevisionJueves.Checked = cte.Cte_RJueves;
            chkRevisionViernes.Checked = cte.Cte_RViernes;
            chkRevisionSabado.Checked = cte.Cte_RSabado;
            tpRevisionMañanaInicio.DbSelectedDate = cte.Cte_RHoraam1;
            tpRevisionMañanafin.DbSelectedDate = cte.Cte_RHoraam2;
            tpRevisionTardeinicio.DbSelectedDate = cte.Cte_RHorapm1;
            tpRevisionTardefin.DbSelectedDate = cte.Cte_RHorapm2;
            //PAGO
            chkPagoLunes.Checked = cte.Cte_CPLunes;
            chkPagoMartes.Checked = cte.Cte_CPMartes;
            chkPagoMiercoles.Checked = cte.Cte_CPMiercoles;
            chkPagoJueves.Checked = cte.Cte_CPJueves;
            chkPagoViernes.Checked = cte.Cte_CPViernes;
            chkPagoSabado.Checked = cte.Cte_CPSabado;
            tpPagoMañanaInicio.DbSelectedDate = cte.Cte_PHoraam1;
            tpPagoMañanafin.DbSelectedDate = cte.Cte_PHoraam2;
            tpPagoTardeinicio.DbSelectedDate = cte.Cte_PHorapm1;
            tpPagoTardefin.DbSelectedDate = cte.Cte_PHorapm2;
            
          
            
           // chkOrden.Checked = cte.Cte_ReqOrdenCompra;
           // txtDocumentos.Text = cte.Cte_Documentos;
        }
        private void Guardar()
        {
            try
            {
                if (dtAcuerdos.Rows.Count == 0)
                {
                    Alerta("Favor de capturar por lo menos un acuerdo económico");
                    return;
                }
                Acys acys = new Acys();
                acys.Id_Emp = sesion.Id_Emp;
                acys.Id_Cd = sesion.Id_Cd_Ver;
                acys.Id_Ter = Convert.ToInt32(cmbTerritorio.SelectedValue);
                acys.Id_Rik = Convert.ToInt32(cmbRepresentante.SelectedValue);
                acys.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1);
                acys.Cte_Nombre = txtComercial.Text;

                Funciones funcion = new Funciones();
                acys.Acs_Fecha = Convert.ToDateTime(rdFecha.SelectedDate.Value.ToShortDateString() + " " + funcion.GetLocalDateTime(sesion.Minutos).ToShortTimeString());

                acys.Acs_FechaInicioDocumento = Convert.ToDateTime(rdFechaInicioDocumento.SelectedDate.Value.ToShortDateString() + " " + funcion.GetLocalDateTime(sesion.Minutos).ToShortTimeString());

                acys.Acs_FechaFinDocumento = Convert.ToDateTime(rdFechaFinDocumento.SelectedDate.Value.ToShortDateString() + " " + funcion.GetLocalDateTime(sesion.Minutos).ToShortTimeString());
                                             


                acys.Acs_Proveedor = txtProveedor.Text;
                acys.Acs_RutaEntrega = !string.IsNullOrEmpty(cmbRutaEntrega.SelectedValue) ? Convert.ToInt32(cmbRutaEntrega.SelectedValue) : 0;
                acys.Acs_RutaServicio = !string.IsNullOrEmpty(cmbRutaServicio.SelectedValue) ? Convert.ToInt32(cmbRutaServicio.SelectedValue) : 0;
                
                acys.Acs_VigenciaIni = rdVigenciaIni.SelectedDate;
                acys.Acs_Semana = txtSemana.Value.HasValue ? Convert.ToInt32(txtSemana.Text) : 0;
               
                
                acys.Acs_RecPedCorreo = ChkbEmail.Checked;
                acys.Acs_RecPedFax = ChkbFax.Checked;
                acys.Acs_RecPedTel = ChkbTelefono.Checked;
                acys.Acs_RecPedRep = CheckRepVenta.Checked;                
                acys.Acs_RecPedOtroStr = txtPedidoOtro.Text;

                acys.Acs_PedidoEncargadoEnviar = txtPedidoEncargadoEnviar.Text;
                acys.Acs_PedidoPuesto = txtpedidoPuesto.Text;
                acys.Acs_PedidoTelefono = txtpedidotelefono.Text;
                acys.Acs_PedidoEmail  = txtpedidoEmail.Text;
                acys.Acs_RecDocReposicion = chkRecDocReposicion.Checked;
                acys.Acs_RecDocFolio = ChkRecDocFolio.Checked;
                acys.Acs_RecDocOtro = txtRecDocOtro.Text;

                acys.Id_U = sesion.Id_U;

                //VISITAS
                acys.Vis_Frecuencia = Vis_Frecuencia.Value.HasValue ? Vis_Frecuencia.Value : 0;
                acys.Acs_VisitaOtro =  txtVisitaOtro.Text;

                acys.Acs_ReqServAsesoria =  ChkServAsesoria.Checked;
                acys.Acs_ReqServTecnicoRelleno =  ChkServTecnicoRelleno.Checked;
                acys.Acs_ReqServMantenimiento = ChkServMantenimiento.Checked;
              

               

                //ASESORIAS
                List<Asesoria> asesorias = new List<Asesoria>();
                Asesoria asesoria;
                for (int i = 0; i < rgAsesoria.Items.Count; i++)
                {                    

                    bool ServAsesoriaMensual =  (rgAsesoria.Items[i].FindControl("ServAsesoriaMensual") as RadioButton).Checked;
                    DateTime? ServAsesoriaMensualfechaIni = (rgAsesoria.Items[i].FindControl("ServAsesoriaMensualfechaIni") as RadDatePicker).SelectedDate;

                    bool ServAsesoriaBimestral = (rgAsesoria.Items[i].FindControl("ServAsesoriaBimestral") as RadioButton).Checked;
                    DateTime? ServAsesoriaBimestralfechaIni = (rgAsesoria.Items[i].FindControl("ServAsesoriaBimestralfechaIni") as RadDatePicker).SelectedDate;

                    bool ServAsesoriaTrimestral = (rgAsesoria.Items[i].FindControl("ServAsesoriaTrimestral") as RadioButton).Checked;
                    DateTime? ServAsesoriaTrimestralfechaIni = (rgAsesoria.Items[i].FindControl("ServAsesoriaTrimestralfechaIni") as RadDatePicker).SelectedDate;

                    
                    asesoria = new Asesoria();
                    asesoria.Id_Ase = Convert.ToInt32(rgAsesoria.Items[i]["Id_Ase"].Text);
                    asesoria.Ase_ServAsesoriaMensual = ServAsesoriaMensual == null ? false : (bool)ServAsesoriaMensual;
                    asesoria.Ase_ServAsesoriaMensualfechaIni =  ServAsesoriaMensualfechaIni;

                    asesoria.Ase_ServAsesoriaBimestral = ServAsesoriaBimestral == null ? false : (bool)ServAsesoriaBimestral;
                    asesoria.Ase_ServAsesoriaBimestralfechaIni = ServAsesoriaBimestralfechaIni;

                    asesoria.Ase_ServAsesoriaTrimestral = ServAsesoriaTrimestral == null ? false : (bool)ServAsesoriaTrimestral;
                    asesoria.Ase_ServAsesoriaTrimestralfechaIni = ServAsesoriaTrimestralfechaIni;
                    asesorias.Add(asesoria);
                }

                //SERVICIO
                List<Producto> Lista_Producto = new List<Producto>();
                Producto prod;
                for (int i = 0; i < rgServicios.Items.Count; i++)
                {


                    bool ServTecnicoRellenoBimestral = (rgServicios.Items[i].FindControl("ServTecnicoRellenoBimestral") as RadioButton).Checked;
                    DateTime? ServTecnicoRellenoBimestralfechaIni = (rgServicios.Items[i].FindControl("ServTecnicoRellenoBimestralfechaIni") as RadDatePicker).SelectedDate;

                    bool ServTecnicoRellenoTrimestral = (rgServicios.Items[i].FindControl("ServTecnicoRellenoTrimestral") as RadioButton).Checked;
                    DateTime? ServTecnicoRellenoTrimestralfechaIni = (rgServicios.Items[i].FindControl("ServTecnicoRellenoTrimestralfechaIni") as RadDatePicker).SelectedDate;

                    prod = new Producto();
                    prod.Id_Prd = Convert.ToInt32((rgServicios.Items[i].FindControl("lblCodigo") as Label).Text);
                    //prod.Prd_InvFinal = (rgServicios.Items[i].FindControl("txtCantidad") as RadNumericTextBox).Value == null ? 0 : (int)(rgServicios.Items[i].FindControl("txtCantidad") as RadNumericTextBox).Value;
                    prod.Prd_InvInicial = (rgServicios.Items[i].FindControl("txtCantidad") as RadNumericTextBox).Value == null ? 0 : (int)(rgServicios.Items[i].FindControl("txtCantidad") as RadNumericTextBox).Value;
                    prod.ServTecnicoRellenoBimestral = ServTecnicoRellenoBimestral == null ? false : (bool)ServTecnicoRellenoBimestral;
                    prod.ServTecnicoRellenoBimestralfechaIni = ServTecnicoRellenoBimestralfechaIni;

                    prod.ServTecnicoRellenoTrimestral = ServTecnicoRellenoTrimestral == null ? false : (bool)ServTecnicoRellenoTrimestral;
                    prod.ServTecnicoRellenoTrimestralfechaIni = ServTecnicoRellenoTrimestralfechaIni;
                    Lista_Producto.Add(prod);
                }


                List<Producto> Lista_ProductosManteniento = new List<Producto>();
                Producto productoMantenimiento;
                for (int i = 0; i < rgMantPrevRev.Items.Count; i++)
                {


                    bool ServMantenimientoMensual = (rgMantPrevRev.Items[i].FindControl("ServMantenimientoMensual") as RadioButton).Checked;
                    DateTime? ServMantenimientoMensualfechaIni = (rgMantPrevRev.Items[i].FindControl("ServMantenimientoMensualfechaIni") as RadDatePicker).SelectedDate;

                    bool ServMantenimientoBimestral = (rgMantPrevRev.Items[i].FindControl("ServMantenimientoBimestral") as RadioButton).Checked;
                    DateTime? ServMantenimientoBimestralfechaIni = (rgMantPrevRev.Items[i].FindControl("ServMantenimientoBimestralfechaIni") as RadDatePicker).SelectedDate;

                    bool ServMantenimientoTrimestral = (rgMantPrevRev.Items[i].FindControl("ServMantenimientoTrimestral") as RadioButton).Checked;
                    DateTime? ServMantenimientoTrimestralfechaIni = (rgMantPrevRev.Items[i].FindControl("ServMantenimientoTrimestralfechaIni") as RadDatePicker).SelectedDate;

                    productoMantenimiento = new Producto();
                    productoMantenimiento.Id_Prd = Convert.ToInt32((rgMantPrevRev.Items[i].FindControl("lblCodigo") as Label).Text);
                    //prod.Prd_InvFinal = (rgServicios.Items[i].FindControl("txtCantidad") as RadNumericTextBox).Value == null ? 0 : (int)(rgServicios.Items[i].FindControl("txtCantidad") as RadNumericTextBox).Value;
                    productoMantenimiento.Prd_InvInicial = (rgMantPrevRev.Items[i].FindControl("txtCantidad") as RadNumericTextBox).Value == null ? 0 : (int)(rgServicios.Items[i].FindControl("txtCantidad") as RadNumericTextBox).Value;
                    productoMantenimiento.ServMantenimientoMensual = ServMantenimientoMensual == null ? false : (bool)ServMantenimientoMensual;
                    productoMantenimiento.ServMantenimientoMensualfechaIni = ServMantenimientoMensualfechaIni;

                    productoMantenimiento.ServMantenimientoBimestral = ServMantenimientoBimestral == null ? false : (bool)ServMantenimientoBimestral;
                    productoMantenimiento.ServMantenimientoBimestralfechaIni = ServMantenimientoBimestralfechaIni;

                    productoMantenimiento.ServMantenimientoTrimestral = ServMantenimientoTrimestral == null ? false : (bool)ServMantenimientoTrimestral;
                    productoMantenimiento.ServMantenimientoTrimestralfechaIni = ServMantenimientoTrimestralfechaIni;
                    Lista_ProductosManteniento.Add(productoMantenimiento);
                }

                //
                List<AcysPrd> list = new List<AcysPrd>();
                AcysPrd prd;
                for (int x = 0; x < dtAcuerdos.Rows.Count; x++)
                {
                    prd = new AcysPrd();
                    prd.Id_Prd = Convert.ToInt32(dtAcuerdos.Rows[x]["Id_Prd"]);
                    prd.Prd_Precio = Convert.ToDouble(dtAcuerdos.Rows[x]["Prd_Precio"]);
                    prd.Acys_Cantidad = Convert.ToInt32(dtAcuerdos.Rows[x]["Acys_Cantidad"]);
                    prd.Acys_Frecuencia = Convert.ToInt32(dtAcuerdos.Rows[x]["Acys_Frecuencia"]);
                    prd.Acys_Lunes = Convert.ToBoolean(dtAcuerdos.Rows[x]["Acys_Lunes"]);
                    prd.Acys_Martes = Convert.ToBoolean(dtAcuerdos.Rows[x]["Acys_Martes"]);
                    prd.Acys_Miercoles = Convert.ToBoolean(dtAcuerdos.Rows[x]["Acys_Miercoles"]);
                    prd.Acys_Jueves = Convert.ToBoolean(dtAcuerdos.Rows[x]["Acys_Jueves"]);
                    prd.Acys_Viernes = Convert.ToBoolean(dtAcuerdos.Rows[x]["Acys_Viernes"]);
                    prd.Acys_Sabado = Convert.ToBoolean(dtAcuerdos.Rows[x]["Acys_Sabado"]);
                    prd.Acs_Doc = dtAcuerdos.Rows[x]["Acs_Doc"].ToString();
                    prd.Acys_UltSCtp = dtAcuerdos.Rows[x]["Acys_UltSCtp"] == DBNull.Value ? -1 : Convert.ToInt32(dtAcuerdos.Rows[x]["Acys_UltSCtp"]);
                    prd.Acys_UltACtp = dtAcuerdos.Rows[x]["Acys_UltACtp"] == DBNull.Value ? -1 : Convert.ToInt32(dtAcuerdos.Rows[x]["Acys_UltACtp"]);
                    prd.Acs_Modalidad = dtAcuerdos.Rows[x]["Acs_Modalidad"].ToString();
                    list.Add(prd);
                }

               acys.Acs_Notas = txtNotas.Text;
                
               acys.Acs_ContactoRepVenta =   !string.IsNullOrEmpty(ContactoRepVenta.SelectedValue) ? Convert.ToInt32(ContactoRepVenta.SelectedValue) : 0;                          
               acys.Acs_ContactoRepVentaTel = ContactoRepVentaTel.Text;
               acys.Acs_ContactoRepVentaEmail = ContactoRepVentaEmail.Text;

               acys.Acs_ContactoRepServ =   !string.IsNullOrEmpty(ContactoRepServ.SelectedValue) ? Convert.ToInt32(ContactoRepServ.SelectedValue) : 0;
               acys.Acs_ContactoRepServTel = ContactoRepServTel.Text;
               acys.Acs_ContactoRepServEmail =  ContactoRepServEmail.Text;

                
               acys.Acs_ContactoJefServ = !string.IsNullOrEmpty(ContactoJefServ.SelectedValue) ? Convert.ToInt32(ContactoJefServ.SelectedValue) : 0;
               acys.Acs_ContactoJefServTel = ContactoJefServTel.Text;
               acys.Acs_ContactoJefServEmail = ContactoJefServEmail.Text;
            
                
               acys.Acs_ContactoAseServ = !string.IsNullOrEmpty(ContactoAseServ.SelectedValue) ? Convert.ToInt32(ContactoAseServ.SelectedValue) : 0;                 
               acys.Acs_ContactoAseServTel = ContactoAseServTel.Text;
               acys.Acs_ContactoAseServEmail =  ContactoAseServEmail.Text;

               acys.Acs_ContactoJefOper = !string.IsNullOrEmpty(ContactoJefOper.SelectedValue) ? Convert.ToInt32(ContactoJefOper.SelectedValue) : 0;
               acys.Acs_ContactoJefOperTel =  ContactoJefOperTel.Text;
               acys.Acs_ContactoJefOperEmail =  ContactoJefOperEmail.Text;


               acys.Acs_ContactoCAlmRep = !string.IsNullOrEmpty(ContactoCAlmRep.SelectedValue) ? Convert.ToInt32(ContactoCAlmRep.SelectedValue) : 0;              
               acys.Acs_ContactoCAlmRepTel = ContactoCAlmRepTel.Text;
               acys.Acs_ContactoCAlmRepEmail =  ContactoCAlmRepEmail.Text;

               acys.Acs_ContactoCServTec = !string.IsNullOrEmpty(ContactoCServTec.SelectedValue) ? Convert.ToInt32(ContactoCServTec.SelectedValue) : 0;
               acys.Acs_ContactoCServTecTel = ContactoCServTecTel.Text;
               acys.Acs_ContactoCServTecEmail =  ContactoCServTecEmail.Text;

               acys.Acs_ContactoCCreCob = !string.IsNullOrEmpty(ContactoCCreCob.SelectedValue) ? Convert.ToInt32(ContactoCCreCob.SelectedValue) : 0;
               acys.Acs_ContactoCCreCobTel = ContactoCCreCobTel.Text;
               acys.Acs_ContactoCCreCobEmail = ContactoCCreCobEmail.Text;


                acys.Acs_Contacto2 = txtContactoClientecompra.Text;
                acys.Acs_Telefono2= txtContactoClientecompraTel.Value.HasValue ? Convert.ToInt32(txtContactoClientecompraTel.Value) : 0;
                acys.Acs_Correo2 = txtContactoClientecompraEmail.Text;

                acys.Acs_Contacto3 = txtContactoClientealmacen.Text;
                acys.Acs_Telefono3 = txtContactoClientealmacenTel.Value.HasValue ? Convert.ToInt32(txtContactoClientealmacenTel.Value) : 0;
                acys.Acs_Correo3 = txtContactoClientealmacenEmail.Text;

                acys.Acs_Contacto4 =  txtContactoClienteMantenimiento.Text;
                acys.Acs_Telefono4 = txtContactoClienteMantenimientoTel.Value.HasValue ? Convert.ToInt32(txtContactoClienteMantenimientoTel.Value) : 0;
                acys.Acs_Correo4 = txtContactoClienteMantenimientoEmail.Text;

                acys.Acs_Contacto5 =  txtContactoClientePagos.Text;
                acys.Acs_Telefono5 = txtContactoClientePagosTel.Value.HasValue ? Convert.ToInt32(txtContactoClientePagosTel.Value) : 0;
                acys.Acs_Correo5 = txtContactoClientePagosEmail.Text;

                acys.Acs_Contacto6 = txtContactoClienteOtro.Text;
                acys.Acs_Telefono6 = txtContactoClienteOtroTel.Value.HasValue ? Convert.ToInt32(txtContactoClienteOtroTel.Value) : 0;
                acys.Acs_Correo6 = txtContactoClienteOtroEmail.Text;

                int verificador = -1;
                CN_CapAcys cn_capacys = new CN_CapAcys();

                if (HF_ID.Value == "" || HF_Sustituye.Value != "")
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }
                    cn_capacys.Insertar(acys, list, sesion.Emp_Cnx, Seleccionados, ref verificador, asesorias, Lista_Producto, Lista_ProductosManteniento);
                    if (verificador > 0)
                        RAM1.ResponseScripts.Add("CloseAlert('Los datos se guardaron correctamente');");
                    else
                        Alerta("Ocurrió un error al intentar guardar el acuerdo");
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }
                    acys.Id_Acs = Convert.ToInt32(HF_ID.Value);
                    cn_capacys.Modificar(acys, list, sesion.Emp_Cnx, Seleccionados, ref verificador, asesorias, Lista_Producto, Lista_ProductosManteniento);
                    if (verificador > 0)
                        RAM1.ResponseScripts.Add("CloseAlert('Los datos se guardaron correctamente');");
                    else
                        Alerta("Ocurrió un error al intentar guardar los cambios");
                }

                RadTabStrip1.Tabs[0].Selected = true;
                RadPageCliente.Selected = true;
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
                _PermisoGuardar = Convert.ToBoolean(Request.QueryString["PermisoGuardar"]);
                _PermisoModificar = Convert.ToBoolean(Request.QueryString["PermisoModificar"]);
                _PermisoEliminar = Convert.ToBoolean(Request.QueryString["PermisoEliminar"]);
                _PermisoImprimir = Convert.ToBoolean(Request.QueryString["PermisoImprimir"]);
                _Accion = Convert.ToInt32(Request.QueryString["Accion"]);

                ValidarPermisos();
                if (_Accion == 2) {
                    BtnAutorizar.Visible = true;
                    BtnRechazar.Visible = true;
                }

                this.cargarCboUsuarios();

                if (Request.QueryString["Id"].ToString() != "-1")
                {
                    if (Request.QueryString["Accion"].ToString() == "0")
                    {
                        txtFolio.Text = Request.QueryString["Id"].ToString();
                        HF_ID.Value = Request.QueryString["Id"].ToString();

                    }
                    else 
                    {
                        txtFolio.Text = MaximoId();
                        HF_ID.Value = Request.QueryString["Id"].ToString();
                        HF_Sustituye.Value = Request.QueryString["Id"].ToString();
                        
                    }
                    
                    txtTerritorio.Enabled = false;
                    cmbTerritorio.Enabled = false;
                    txtRepresentante.Enabled = false;
                    cmbRepresentante.Enabled = false;
                    CargarAcys();
                    CargarAcysDet();
                    CargarCondiciones();
                    //NO SE PUEDE MODIFICAR FECHA, TERRITORIO, REPRESENTANTE NI CLIENTE EN LA EDICION
                   
                }
                else
                {
                    txtFolio.Text = MaximoId();
                   
                }


                rdFecha.Enabled = false;
                
                //txtCliente.Enabled = false;
                txtClienteNombre.Enabled = false;
                txtComercial.Enabled = false;
                txtEmail.Enabled = false;
                CheckCuentaCorporativa.Enabled = false;
                txtDireccionEntrega.Enabled = false;
                txtClienteColoniaE.Enabled = false;
                txtClienteMunicipioE.Enabled = false;
                txtClienteEstadoE.Enabled = false;
                txtClienteCPE.Enabled = false;
                txtClienteMunicipio.Enabled = false;
                txtClienteColonia.Enabled = false;
                txtClienteDireccion.Enabled = false;
                txtClienteEstado.Enabled = false;
                txtClienteRFC.Enabled = false;
                txtRutaEntrega.Enabled = false;
                txtRutaServicio.Enabled = false;
                txtTelefono.Enabled = false;
                cmbRutaEntrega.Enabled = false;
                cmbRutaServicio.Enabled = false;
                txtPuesto.Enabled = false;
                txtContacto.Enabled = false;
                txtClienteCodPost.Enabled = false;
                ChkbAdendaSI.Enabled = false;
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarAcysDet()
        {
            try
            {
                GetListGrl();
                CN_CapAcys cn_capacys = new CN_CapAcys();
                Acys acys = new Acys();
                acys.Id_Emp = sesion.Id_Emp;
                acys.Id_Cd = sesion.Id_Cd_Ver;
                acys.Id_Acs = Convert.ToInt32(HF_ID.Value);
                DataTable dt = dtAcuerdos;
                cn_capacys.ConsultarDet(acys, ref dt, sesion.Emp_Cnx);
                dtAcuerdos = dt;
                rgAcuerdos.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarAcys()
        {
            try
            {
                CN_CapAcys cn_capacys = new CN_CapAcys();
                Acys acys = new Acys();
                acys.Id_Emp = sesion.Id_Emp;
                acys.Id_Cd = sesion.Id_Cd_Ver;
                acys.Id_Acs = Convert.ToInt32(HF_ID.Value);
                cn_capacys.Consultar(ref acys, sesion.Emp_Cnx);

                if (acys.Acs_Estatus != "C") {
                    this.rtb1.Items[5].Visible = false;
                }
                //Cabecera
                DateTime fec = Convert.ToDateTime("1900/01/01");
                rdFecha.DbSelectedDate = acys.Acs_Fecha;
                rdFechaFinDocumento.DbSelectedDate =  acys.Acs_FechaFinDocumento;
                rdFechaInicioDocumento.DbSelectedDate = acys.Acs_FechaInicioDocumento;

                /****CLIENTE ***/
                //Información Fiscal
                txtCliente.Text = acys.Id_Cte.ToString();
                txtClienteNombre.Text = acys.Cte_Nombre;
                txtClienteDireccion.Text = acys.ClienteDireccion;
                txtClienteColonia.Text = acys.ClienteColonia;
                txtClienteMunicipio.Text = acys.ClienteMunicipio;
                txtClienteEstado.Text = acys.ClienteEstado;
                txtClienteRFC.Text = acys.ClienteRFC;
                txtClienteCodPost.Text = acys.ClienteCodPost;
                txtEmail.Enabled = false; ;
                CheckCuentaCorporativa.Checked = acys.CuentaCorporativa;
                ChkbAdendaSI.Checked = acys.AddendaSI;


                //Información Comercial  
                txtComercial.Text = acys.Cte_Nombre;
                txtDireccionEntrega.Text= acys.DireccionEntrega;
                txtClienteColoniaE.Text = acys.ClienteColoniaE;
                txtClienteMunicipioE.Text = acys.ClienteMunicipioE;
                txtClienteEstadoE.Text = acys.ClienteEstadoE;
                txtClienteCPE.Text = acys.ClienteCPE;
                txtProveedor.Text = acys.Acs_Proveedor;
                txtContacto.Text = acys.Acs_Contacto;
                txtPuesto.Text = acys.Acs_Puesto;
                txtTelefono.Text = acys.Acs_Telefono.ToString();
                txtEmail.Text = acys.Acs_Correo;
                CargarTerritorios();
                txtTerritorio.Text = acys.Id_Ter.ToString();
                if (cmbTerritorio.FindItemIndexByValue(acys.Id_Ter.ToString()) > 0)
                {
                    cmbTerritorio.SelectedIndex = cmbTerritorio.FindItemIndexByValue(acys.Id_Ter.ToString());
                    cmbTerritorio.Text = cmbTerritorio.FindItemByValue(acys.Id_Ter.ToString()).Text;
                }


                if (cmbRutaServicio.FindItemIndexByValue(acys.Acs_RutaServicio.ToString()) > 0)
                {
                    txtRutaServicio.DbValue = acys.Acs_RutaServicio <= 0 ? (object)null : acys.Acs_RutaServicio;
                    cmbRutaServicio.SelectedIndex = cmbRutaServicio.FindItemIndexByValue(acys.Acs_RutaServicio.ToString());
                    cmbRutaServicio.Text = cmbRutaServicio.FindItemByValue(acys.Acs_RutaServicio.ToString()).Text;
                }

                
                CargarRik();
                //CargarClientes();
                if (cmbRepresentante.FindItemIndexByValue(acys.Id_Rik.ToString()) > 0)
                {
                    txtRepresentante.Text = acys.Id_Rik.ToString();
                    cmbRepresentante.SelectedIndex = cmbRepresentante.FindItemIndexByValue(acys.Id_Rik.ToString());
                    cmbRepresentante.Text = cmbRepresentante.FindItemByValue(acys.Id_Rik.ToString()).Text;
                }

                CargarRutaEntrega();


                if (cmbRutaEntrega.FindItemIndexByValue(acys.Acs_RutaEntrega.ToString()) > 0)
                {
                    txtRutaEntrega.DbValue = acys.Acs_RutaEntrega <= 0 ? (object)null : acys.Acs_RutaEntrega;
                    cmbRutaEntrega.SelectedIndex = cmbRutaEntrega.FindItemIndexByValue(acys.Acs_RutaEntrega.ToString());
                    cmbRutaEntrega.Text = cmbRutaEntrega.FindItemByValue(acys.Acs_RutaEntrega.ToString()).Text;
                }
                
               
                /*  RECEPCION DE PEDIDOS****/
                ChkbEmail.Checked = acys.Acs_RecPedCorreo;
                ChkbFax.Checked = acys.Acs_RecPedFax;
                ChkbTelefono.Checked = acys.Acs_RecPedTel;
                CheckRepVenta.Checked = acys.Acs_RecPedRep;                
                txtPedidoOtro.Text = acys.Acs_RecPedOtroStr;

                txtPedidoEncargadoEnviar.Text = acys.Acs_PedidoEncargadoEnviar;
                txtpedidoPuesto.Text = acys.Acs_PedidoPuesto;
                txtpedidotelefono.Text =  acys.Acs_PedidoTelefono;
                txtpedidoEmail.Text = acys.Acs_PedidoEmail;                
       
                chkRecDocOrdenCompra.Checked = acys.Acs_ReqOrdenCompra;
                chkRecDocReposicion.Checked = acys.Acs_RecDocReposicion;
                ChkRecDocFolio.Checked = acys.Acs_RecDocFolio;
                txtRecDocOtro.Text = acys.Acs_RecDocOtro;
               
                /************** ACUERDOS ENONOMICOS ******/
                rdVigenciaIni.DbSelectedDate = (acys.Acs_VigenciaIni == fec ? null : acys.Acs_VigenciaIni);
                txtSemana.DbValue = acys.Acs_Semana;
                
                /******* SERVICIOS DE VALOR ******/
                Vis_Frecuencia.DbValue = acys.Vis_Frecuencia;
                txtVisitaOtro.Text = acys.Acs_VisitaOtro;
                ChkServAsesoria.Checked = acys.Acs_ReqServAsesoria;
                ChkServTecnicoRelleno.Checked = acys.Acs_ReqServTecnicoRelleno;
                ChkServMantenimiento.Checked = acys.Acs_ReqServMantenimiento;              
                AsesoriaListado.Visible = acys.Acs_ReqServAsesoria;
                EquipoRellenoListado.Visible = acys.Acs_ReqServTecnicoRelleno;
                MantenimientoPreventivoListado.Visible = acys.Acs_ReqServMantenimiento;

                /**** OTROS APOYOS *******/


                txtNotas.Text = acys.Acs_Notas;

                // Personal de KEY
                               
                
                if (ContactoRepVenta.FindItemIndexByValue(acys.Acs_ContactoRepVenta.ToString()) > 0)
                {
                    ContactoRepVenta.SelectedIndex = ContactoRepVenta.FindItemIndexByValue(acys.Acs_ContactoRepVenta.ToString());
                    ContactoRepVenta.Text = ContactoRepVenta.FindItemByValue(acys.Acs_ContactoRepVenta.ToString()).Text;
                }
            
               ContactoRepVentaTel.Text = acys.Acs_ContactoRepVentaTel;
               ContactoRepVentaEmail.Text = acys.Acs_ContactoRepVentaEmail;

                if (ContactoRepServ.FindItemIndexByValue(acys.Acs_ContactoRepServ.ToString()) > 0)
                {
                    ContactoRepServ.SelectedIndex = ContactoRepServ.FindItemIndexByValue(acys.Acs_ContactoRepServ.ToString());
                    ContactoRepServ.Text = ContactoRepServ.FindItemByValue(acys.Acs_ContactoRepServ.ToString()).Text;
                }
            
               ContactoRepServTel.Text = acys.Acs_ContactoRepServTel;
               ContactoRepServEmail.Text = acys.Acs_ContactoRepServEmail;


                if (ContactoJefServ.FindItemIndexByValue(acys.Acs_ContactoJefServ.ToString()) > 0)
                {
                    ContactoJefServ.SelectedIndex = ContactoJefServ.FindItemIndexByValue(acys.Acs_ContactoJefServ.ToString());
                    ContactoJefServ.Text = ContactoJefServ.FindItemByValue(acys.Acs_ContactoJefServ.ToString()).Text;
                }
            
               ContactoJefServTel.Text = acys.Acs_ContactoRepServTel;
               ContactoJefServEmail.Text = acys.Acs_ContactoRepServEmail;


                if (ContactoAseServ.FindItemIndexByValue(acys.Acs_ContactoAseServ.ToString()) > 0)
                {
                    ContactoAseServ.SelectedIndex = ContactoAseServ.FindItemIndexByValue(acys.Acs_ContactoAseServ.ToString());
                    ContactoAseServ.Text = ContactoAseServ.FindItemByValue(acys.Acs_ContactoAseServ.ToString()).Text;
                }
            
               ContactoAseServTel.Text = acys.Acs_ContactoAseServTel;
               ContactoAseServEmail.Text = acys.Acs_ContactoAseServEmail;


                if (ContactoJefOper.FindItemIndexByValue(acys.Acs_ContactoJefOper.ToString()) > 0)
                {
                    ContactoJefOper.SelectedIndex = ContactoJefOper.FindItemIndexByValue(acys.Acs_ContactoJefOper.ToString());
                    ContactoJefOper.Text = ContactoJefOper.FindItemByValue(acys.Acs_ContactoJefOper.ToString()).Text;
                }
            
               ContactoJefOperTel.Text = acys.Acs_ContactoJefOperTel;
               ContactoJefOperEmail.Text = acys.Acs_ContactoJefOperEmail;

               if (ContactoCAlmRep.FindItemIndexByValue(acys.Acs_ContactoCAlmRep.ToString()) > 0)
                {
                    ContactoCAlmRep.SelectedIndex = ContactoCAlmRep.FindItemIndexByValue(acys.Acs_ContactoCAlmRep.ToString());
                    ContactoCAlmRep.Text = ContactoCAlmRep.FindItemByValue(acys.Acs_ContactoCAlmRep.ToString()).Text;
                }
            
               ContactoCAlmRepTel.Text = acys.Acs_ContactoCAlmRepTel;
               ContactoCAlmRepEmail.Text = acys.Acs_ContactoCAlmRepEmail;


                if (ContactoCServTec.FindItemIndexByValue(acys.Acs_ContactoCServTec.ToString()) > 0)
                {
                    ContactoCServTec.SelectedIndex = ContactoCServTec.FindItemIndexByValue(acys.Acs_ContactoCServTec.ToString());
                    ContactoCServTec.Text = ContactoCServTec.FindItemByValue(acys.Acs_ContactoCServTec.ToString()).Text;
                }
            
               ContactoCServTecTel.Text = acys.Acs_ContactoCServTecTel;
               ContactoCServTecEmail.Text = acys.Acs_ContactoCServTecEmail;


                if (ContactoCCreCob.FindItemIndexByValue(acys.Acs_ContactoCCreCob.ToString()) > 0)
                {
                    ContactoCCreCob.SelectedIndex = ContactoCCreCob.FindItemIndexByValue(acys.Acs_ContactoCCreCob.ToString());
                    ContactoCCreCob.Text = ContactoCCreCob.FindItemByValue(acys.Acs_ContactoCCreCob.ToString()).Text;
                }
            
               ContactoCCreCobTel.Text = acys.Acs_ContactoCCreCobTel;
               ContactoCCreCobEmail.Text = acys.Acs_ContactoCCreCobEmail;


                

                //  Contactos del Cliente

                txtContactoClientecompra.Text = acys.Acs_Contacto2;
                txtContactoClientecompraTel.DbValue = acys.Acs_Telefono2 <= 0 ? (object)null : acys.Acs_Telefono2;
                txtContactoClientecompraEmail.Text = acys.Acs_Correo2;

                txtContactoClientealmacen.Text = acys.Acs_Contacto3;
                txtContactoClientealmacenTel.DbValue = acys.Acs_Telefono3 <= 0 ? (object)null : acys.Acs_Telefono3;
                txtContactoClientealmacenEmail.Text = acys.Acs_Correo3;

                txtContactoClienteMantenimiento.Text = acys.Acs_Contacto4;
                txtContactoClienteMantenimientoTel.DbValue = acys.Acs_Telefono4 <= 0 ? (object)null : acys.Acs_Telefono4;
                txtContactoClienteMantenimientoEmail.Text = acys.Acs_Correo4;

                txtContactoClientePagos.Text = acys.Acs_Contacto5;
                txtContactoClientePagosTel.DbValue = acys.Acs_Telefono5 <= 0 ? (object)null : acys.Acs_Telefono5;
                txtContactoClientePagosEmail.Text = acys.Acs_Correo5;

                txtContactoClienteOtro.Text = acys.Acs_Contacto6;
                txtContactoClienteOtroTel.DbValue = acys.Acs_Telefono6 <= 0 ? (object)null : acys.Acs_Telefono6;
                txtContactoClienteOtroEmail.Text = acys.Acs_Correo6;



             


               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CerrarVentana()
        {
            try
            {
                string funcion;
                funcion = "CloseAndRebind()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarRik() //Local
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                int terr = !string.IsNullOrEmpty(cmbTerritorio.SelectedValue) ? Convert.ToInt32(cmbTerritorio.SelectedValue) : 0;
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, terr, 1, Sesion.Emp_Cnx, "spCatRikTerr_Combo", ref cmbRepresentante);
                if (cmbRepresentante.Items.Count > 1)
                {
                    cmbRepresentante.SelectedIndex = 1;
                    txtRepresentante.Text = cmbRepresentante.Items[1].Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarRutaEntrega() //Local
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatRutaEnt_Combo", ref cmbRutaEntrega);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarRutaServicio() //Local
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatRutaSer_Combo", ref cmbRutaServicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarTerritorios()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1), Sesion.Emp_Cnx, "spCatTerCte_Combo", ref cmbTerritorio);
                try
                {
                    if (cmbTerritorio.Items.Count > 1)
                    {
                        cmbTerritorio.SelectedIndex = 1;
                        txtTerritorio.Text = cmbTerritorio.Items[1].Value;
                        CargarRik();
                    }
                }
                catch (Exception)
                {
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string MaximoId()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, "CapAcys", "Id_Acs", Sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void AcysPrevio()
        {
            try
            {
                Nuevo();
                if (!txtCliente.Value.HasValue || cmbTerritorio.SelectedIndex == 0)
                    return;

                CN_CapAcys cn_capacys = new CN_CapAcys();
                Acys acys = new Acys();
                acys.Id_Ter = cmbTerritorio.SelectedValue != "" ? Convert.ToInt32(cmbTerritorio.SelectedValue) : -2;
                acys.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1);
                acys.Id_Rik = cmbRepresentante.SelectedValue != "" ? Convert.ToInt32(cmbRepresentante.SelectedValue) : -1;
                acys.Id_Emp = sesion.Id_Emp;
                acys.Id_Cd = sesion.Id_Cd_Ver;
                List<Acys> list = new List<Acys>();
                cn_capacys.ConsultarAcys_Lista(acys, sesion.Emp_Cnx, ref list);
                if (list.Count > 0)
                {
                    Alerta("Ya existe un acuerdo para el cliente <b>" + txtClienteNombre.Text + "</b> con el territorio <b>#" + cmbTerritorio.SelectedValue + "</b>");
                    txtTerritorio.Text = "";
                    cmbTerritorio.SelectedIndex = 0;
                    cmbTerritorio.Text = "";
                    txtCliente.Focus();
                }
                else
                {
                    if (Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1) != -1)
                    {
                        CN_CatCliente cn_catcliente = new CN_CatCliente();
                        Clientes cc = new Clientes();
                        cc.Id_Emp = sesion.Id_Emp;
                        cc.Id_Cd = sesion.Id_Cd_Ver;
                        cc.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1);
                        try
                        {
                            cn_catcliente.ConsultaClientes(ref cc, sesion.Emp_Cnx);
                            txtComercial.Text = cc.Cte_NomComercial;
                            txtContacto.Text = cc.Cte_Contacto;
                            txtEmail.Text = cc.Cte_Email;
                        }
                        catch (Exception ex)
                        {
                            AlertaFocus(ex.Message, txtCliente.ClientID);
                            txtComercial.Text = "";
                            txtContacto.Text = "";
                            txtEmail.Text = "";
                            return;
                        }
                        Funciones funcion = new Funciones();
                        rdFecha.SelectedDate = funcion.GetLocalDateTime(sesion.Minutos);
                        rdFecha.Focus();
                        GetListGrl();
                        rgAcuerdos.Rebind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Nuevo()
        {
            try
            {
                txtComercial.Text = string.Empty;
                txtContacto.Text = string.Empty;
                txtEmail.Text = string.Empty;
                HF_Sustituye.Value = "";
                HF_ID.Value = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Limpiar()
        {
            txtComercial.Text = string.Empty;
            txtContacto.Text = string.Empty;
            txtEmail.Text = string.Empty;

            if (cmbRepresentante.Items.Count > 0)
            {
                cmbRepresentante.SelectedIndex = 0;
                cmbRepresentante.Text = cmbRepresentante.Items[0].Text;

            }
            txtRepresentante.Value = null;
            //txtClienteNombre.Text = "";
            //txtCliente.Value = null;

            GetListGrl();
            rgAcuerdos.Rebind();
        }
        public string GetWeekNumber(DateTime dtPassed)
        {
            try
            {
                CultureInfo ciCurr = CultureInfo.CurrentCulture;
                int weekNum = ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                return weekNum.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ValidarPermisos()
        {
            try
            {
                if (_PermisoGuardar == false)
                    this.rtb1.Items[5].Visible = false;
                if (_PermisoGuardar == false & _PermisoModificar == false)
                    this.rtb1.Items[5].Visible = false;
                //Regresar
                this.rtb1.Items[4].Visible = false;
                //Eliminar
                this.rtb1.Items[3].Visible = false;
                //Imprimir
                this.rtb1.Items[2].Visible = false;
                //Correo
                this.rtb1.Items[1].Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GetListGrl()
        {
            try
            {
                dtAcuerdos = new DataTable();
                dtAcuerdos.Columns.Add("Id_Prd", System.Type.GetType("System.Int32"));
                dtAcuerdos.Columns.Add("Prd_Descripcion", System.Type.GetType("System.String"));
                dtAcuerdos.Columns.Add("Prd_Presentacion", System.Type.GetType("System.String"));
                dtAcuerdos.Columns.Add("Prd_UniNom", System.Type.GetType("System.String"));
                dtAcuerdos.Columns.Add("Prd_Precio", System.Type.GetType("System.Double"));
                dtAcuerdos.Columns.Add("Acys_Cantidad", System.Type.GetType("System.Int32"));
                dtAcuerdos.Columns.Add("Acys_Frecuencia", System.Type.GetType("System.Int32"));
                dtAcuerdos.Columns.Add("Acys_Lunes", System.Type.GetType("System.Boolean"));
                dtAcuerdos.Columns.Add("Acys_Martes", System.Type.GetType("System.Boolean"));
                dtAcuerdos.Columns.Add("Acys_Miercoles", System.Type.GetType("System.Boolean"));
                dtAcuerdos.Columns.Add("Acys_Jueves", System.Type.GetType("System.Boolean"));
                dtAcuerdos.Columns.Add("Acys_Viernes", System.Type.GetType("System.Boolean"));
                dtAcuerdos.Columns.Add("Acys_Sabado", System.Type.GetType("System.Boolean"));
                dtAcuerdos.Columns.Add("Acs_Doc", System.Type.GetType("System.String"));
                dtAcuerdos.Columns.Add("Acs_DocStr", System.Type.GetType("System.String"));                
                dtAcuerdos.Columns.Add("Acs_Modalidad", System.Type.GetType("System.String"));
                dtAcuerdos.Columns.Add("Acs_ModalidadStr", System.Type.GetType("System.String"));
                dtAcuerdos.Columns.Add("Acys_UltSCtp", System.Type.GetType("System.Int32"));
                dtAcuerdos.Columns.Add("Acys_UltACtp", System.Type.GetType("System.Int32"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Delete(GridCommandEventArgs e)
        {
            GridItem gi = e.Item;
            int Id_Prd = Convert.ToInt32(((Label)gi.FindControl("lblIdPrd")).Text);
            DataRow[] Ar_dr = dtAcuerdos.Select("Id_Prd='" + Id_Prd + "'");
            if (Ar_dr.Length > 0)
            {
                Ar_dr[0].Delete();
                dtAcuerdos.AcceptChanges();
            }
        }
        private void PerformInsert(GridCommandEventArgs e)
        {
            try
            {
                int Id_Prd = 0;
                string Prd_Descripcion = "";
                string Prd_Presentacion = "";
                string Prd_UniNom = "";
                double Prd_Precio = 0;
                int Acys_Cantidad = 0;
                int Acys_Frecuencia = 0;
                bool Acys_Lunes = false;
                bool Acys_Martes = false;
                bool Acys_Miercoles = false;
                bool Acys_Jueves = false;
                bool Acys_Viernes = false;
                bool Acys_Sabado = false;
                string Acs_Doc = "";
                string Acs_DocStr = "";
                string Acs_Modalidad = ""; 
                string Acs_ModalidadStr = ""; 
                GridItem gi = e.Item;

                if (
                    Convert.ToInt32(((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Value.HasValue ? ((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Value.Value : -1) == -1 ||
                    ((RadNumericTextBox)gi.FindControl("txtPrecio")).Text == "" ||
                    ((RadNumericTextBox)gi.FindControl("txtCantidad")).Text == "" ||
                    ((RadNumericTextBox)gi.FindControl("txtFrecuencia")).Text == "" ||
                    ((RadComboBox)gi.FindControl("txtDocumento")).Text == "" ||
                    ((RadComboBox)gi.FindControl("txtModalidad")).Text == "")
                {
                    e.Canceled = true;
                    this.Alerta("Todos los campos son requeridos");
                    return;
                }
                if (!(((CheckBox)gi.FindControl("chkLunes")).Checked ||
                    ((CheckBox)gi.FindControl("chkMartes")).Checked ||
                    ((CheckBox)gi.FindControl("chkMiercoles")).Checked ||
                    ((CheckBox)gi.FindControl("chkJueves")).Checked ||
                    ((CheckBox)gi.FindControl("chkViernes")).Checked ||
                    ((CheckBox)gi.FindControl("chkSabado")).Checked))
                {
                    e.Canceled = true;
                    this.Alerta("Seleccione por lo menos un día de entrega");
                    return;
                }

                Id_Prd = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Value.HasValue ? ((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Value.Value : -1);
                Prd_Descripcion = ((RadTextBox)gi.FindControl("txtProductoNombre")).Text;
                Prd_Presentacion = ((Label)gi.FindControl("lblPresentacionEd")).Text;
                Prd_UniNom = ((Label)gi.FindControl("lblUniEd")).Text;
                Prd_Precio = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtPrecio")).Text);
                Acys_Cantidad = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtCantidad")).Text);
                Acys_Frecuencia = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtFrecuencia")).Text);
                Acys_Lunes = ((CheckBox)gi.FindControl("chkLunes")).Checked;
                Acys_Martes = ((CheckBox)gi.FindControl("chkMartes")).Checked;
                Acys_Miercoles = ((CheckBox)gi.FindControl("chkMiercoles")).Checked;
                Acys_Jueves = ((CheckBox)gi.FindControl("chkJueves")).Checked;
                Acys_Viernes = ((CheckBox)gi.FindControl("chkViernes")).Checked;
                Acys_Sabado = ((CheckBox)gi.FindControl("chkSabado")).Checked;
                Acs_Doc = ((RadComboBox)gi.FindControl("txtDocumento")).SelectedValue;
                Acs_DocStr = ((RadComboBox)gi.FindControl("txtDocumento")).Text;
                Acs_Doc = ((RadComboBox)gi.FindControl("txtDocumento")).SelectedValue;
                Acs_DocStr = ((RadComboBox)gi.FindControl("txtDocumento")).Text;
                Acs_Modalidad = ((RadComboBox)gi.FindControl("txtModalidad")).SelectedValue;
                Acs_ModalidadStr = ((RadComboBox)gi.FindControl("txtModalidad")).Text;

                if (Prd_Precio <= 0)
                {
                    e.Canceled = true;
                    Alerta("El precio de venta debe ser mayor a cero");
                    return;
                }
                DataRow[] Ar_Dr = dtAcuerdos.Select("Id_Prd='" + Id_Prd + "'");
                if (Ar_Dr.Length > 0)
                {
                    e.Canceled = true;
                    this.Alerta("El producto ya fue incluido en este acuerdo");
                    return;
                }
                dtAcuerdos.Rows.Add(new object[] { 
                Id_Prd, 
                Prd_Descripcion, 
                Prd_Presentacion, 
                Prd_UniNom, 
                Prd_Precio, 
                Acys_Cantidad,
                Acys_Frecuencia, 
                Acys_Lunes,
                Acys_Martes,
                Acys_Miercoles,
                Acys_Jueves,
                Acys_Viernes,
                Acys_Sabado,
                Acs_Doc,
                Acs_DocStr,               
                Acs_Modalidad,
                Acs_ModalidadStr
            });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void rdFechaInicioDocumento_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                RadDatePicker dpIni = sender as RadDatePicker;
                RadDatePicker dpFin = (dpIni.Parent.FindControl("rdFechaFinDocumento") as RadDatePicker);
               

                if (dpIni.SelectedDate.HasValue && dpFin.SelectedDate.HasValue)
                {
                    if (dpIni.SelectedDate > dpFin.SelectedDate)
                    {
                        AlertaFocus("La fecha de fin de Vigencia debe ser mayor a la fecha de inicio", dpFin.DateInput.ClientID);
                        dpFin.DbSelectedDate = null;
                        return;
                    }
                    else {
                        dpFin.DateInput.Focus();
                    }
                   
                }
                else if (dpIni.SelectedDate.HasValue)
                {
                    if (dpIni.SelectedDate < Sesion.CalendarioIni)
                    {
                        AlertaFocus("La fecha de inicio debe ser mayor o igual a la fecha de inicio del periodo actual", dpIni.DateInput.ClientID);
                        dpIni.DbSelectedDate = null;
                        return;
                    }
                    else
                    {
                        dpFin.DateInput.Focus();
                    }
                }
                else
                {
                    dpIni.DateInput.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rdFechaFinDocumento_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                RadDatePicker dpFin = sender as RadDatePicker;
                RadDatePicker dpIni = (dpFin.Parent.FindControl("rdFechaInicioDocumento") as RadDatePicker);
                DateTime? oldDate;
                DateTime? newDate;

                if (dpIni.SelectedDate.HasValue && dpFin.SelectedDate.HasValue)
                {
                    if (dpIni.SelectedDate > dpFin.SelectedDate)
                    {
                        AlertaFocus("La fecha de fin de Vigencia no debe ser mayor a la fecha de inicio", dpFin.DateInput.ClientID);
                        dpFin.DbSelectedDate = null;
                        return;
                    }
                    
                    oldDate = dpIni.SelectedDate;
                    newDate = dpFin.SelectedDate;
                    if (oldDate != null && newDate != null)
                    {
                        DateTime FechaIni = oldDate.Value;
                        DateTime FechaFin = newDate.Value;

                        TimeSpan ts = FechaFin - FechaIni;
                    

                        int DiferenciaDias = ts.Days;                        

                        if (DiferenciaDias > 365)
                        {
                            AlertaFocus("La Vigencia del Acuerdo no puede ser mayor a un Año", dpFin.DateInput.ClientID);
                            dpFin.DbSelectedDate = null;
                            return;
                        }
                   }

                }
                else if (dpIni.SelectedDate.HasValue)
                {
                    dpFin.DateInput.Focus();
                }
                else
                {
                    dpIni.DateInput.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }



        protected void BtnAutorizar_Click(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                
                string mensaje = string.Empty;

                int verificador = 0;

               
                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }


                verificador = CambiarEstatus(Convert.ToInt32(HF_ID.Value), "A");
              
               

                if (verificador == 1)
                {
                    Alerta("El Acuerdo fueron autorizado exitosamente");
                }



            }
            catch (Exception ex)
            {
                ErrorManager(ex, "ImageButton1_Click");
            }
        }



        protected void BtnRechazar_Click(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                string mensaje = string.Empty;

                int verificador = 0;


                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }


                verificador = CambiarEstatus(Convert.ToInt32(HF_ID.Value), "R");



                if (verificador == 1)
                {
                    Alerta("El Acuerdo fueron autorizado exitosamente");
                }



            }
            catch (Exception ex)
            {
                ErrorManager(ex, "ImageButton1_Click");
            }
        }


        private void Update(GridCommandEventArgs e)
        {
            try
            {
                int Id_Prd = 0;
                string Prd_Descripcion = "";
                string Prd_Presentacion = "";
                string Prd_UniNom = "";
                double Prd_Precio = 0;
                int Acys_Cantidad = 0;
                int Acys_Frecuencia = 0;
                bool Acys_Lunes = false;
                bool Acys_Martes = false;
                bool Acys_Miercoles = false;
                bool Acys_Jueves = false;
                bool Acys_Viernes = false;
                bool Acys_Sabado = false;
                string Acs_Doc = "";
                string Acs_DocStr = "";
                string Acs_Modalidad = "";
                string Acs_ModalidadStr = ""; 

                GridItem gi = e.Item;
                if (
                   Convert.ToInt32(((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Value.HasValue ? ((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Value.Value : -1) == -1 ||
                    ((RadNumericTextBox)gi.FindControl("txtPrecio")).Text == "" ||
                    ((RadNumericTextBox)gi.FindControl("txtCantidad")).Text == "" ||
                    ((RadNumericTextBox)gi.FindControl("txtFrecuencia")).Text == "" ||
                    ((RadComboBox)gi.FindControl("txtDocumento")).Text == "" ||
                    ((RadComboBox)gi.FindControl("txtModalidad")).Text == "")
                {
                    e.Canceled = true;
                    this.Alerta("Todos los campos son requeridos");
                    return;
                }

                if (!(((CheckBox)gi.FindControl("chkLunes")).Checked ||
                    ((CheckBox)gi.FindControl("chkMartes")).Checked ||
                    ((CheckBox)gi.FindControl("chkMiercoles")).Checked ||
                    ((CheckBox)gi.FindControl("chkJueves")).Checked ||
                    ((CheckBox)gi.FindControl("chkViernes")).Checked ||
                    ((CheckBox)gi.FindControl("chkSabado")).Checked))
                {
                    e.Canceled = true;
                    this.Alerta("Seleccione por lo menos un día de entrega");
                    return;
                }

                Id_Prd = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Value.HasValue ? ((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Value.Value : -1);
                Prd_Descripcion = ((RadTextBox)gi.FindControl("txtProductoNombre")).Text;
                Prd_Presentacion = ((Label)gi.FindControl("lblPresentacionEd")).Text;
                Prd_UniNom = ((Label)gi.FindControl("lblUniEd")).Text;
                Prd_Precio = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtPrecio")).Text);
                Acys_Cantidad = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtCantidad")).Text);
                Acys_Frecuencia = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtFrecuencia")).Text);
                Acys_Lunes = ((CheckBox)gi.FindControl("chkLunes")).Checked;
                Acys_Martes = ((CheckBox)gi.FindControl("chkMartes")).Checked;
                Acys_Miercoles = ((CheckBox)gi.FindControl("chkMiercoles")).Checked;
                Acys_Jueves = ((CheckBox)gi.FindControl("chkJueves")).Checked;
                Acys_Viernes = ((CheckBox)gi.FindControl("chkViernes")).Checked;
                Acys_Sabado = ((CheckBox)gi.FindControl("chkSabado")).Checked;
                Acs_Doc = ((RadComboBox)gi.FindControl("txtDocumento")).SelectedValue;
                Acs_DocStr = ((RadComboBox)gi.FindControl("txtDocumento")).Text;
                Acs_Modalidad = ((RadComboBox)gi.FindControl("txtModalidad")).SelectedValue;
                Acs_ModalidadStr = ((RadComboBox)gi.FindControl("txtModalidad")).Text;

                if (Prd_Precio <= 0)
                {
                    e.Canceled = true;
                    Alerta("El precio de venta debe ser mayor a cero");
                    return;
                }

                DataRow[] Ar_dr = dtAcuerdos.Select("Id_Prd='" + Id_Prd + "'");
                if (Ar_dr.Length > 0)
                {
                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Id_Prd"] = Id_Prd;
                    Ar_dr[0]["Prd_Descripcion"] = Prd_Descripcion;
                    Ar_dr[0]["Prd_UniNom"] = Prd_UniNom;
                    Ar_dr[0]["Prd_Precio"] = Prd_Precio;
                    Ar_dr[0]["Acys_Cantidad"] = Acys_Cantidad;
                    Ar_dr[0]["Acys_Frecuencia"] = Acys_Frecuencia;
                    Ar_dr[0]["Acys_Lunes"] = Acys_Lunes;
                    Ar_dr[0]["Acys_Martes"] = Acys_Martes;
                    Ar_dr[0]["Acys_Miercoles"] = Acys_Miercoles;
                    Ar_dr[0]["Acys_Jueves"] = Acys_Jueves;
                    Ar_dr[0]["Acys_Viernes"] = Acys_Viernes;
                    Ar_dr[0]["Acys_Sabado"] = Acys_Sabado;
                    Ar_dr[0]["Acs_Doc"] = Acs_Doc;
                    Ar_dr[0]["Acs_DocStr"] = Acs_DocStr;
                    Ar_dr[0]["Acs_Modalidad"] = Acs_Modalidad;
                    Ar_dr[0]["Acs_ModalidadStr"] = Acs_ModalidadStr;
                    Ar_dr[0].AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int CambiarEstatus(int Id_Acs, string estatus)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapAcys cn_acys = new CN_CapAcys();
                Acys acys = new Acys();
                acys.Id_Emp = session.Id_Emp;
                acys.Id_Cd = session.Id_Cd_Ver;
                acys.Id_Acs = Id_Acs;
                acys.Acs_Estatus = estatus;
                int verificador = -1;
                cn_acys.actualizarEstatus(acys, session.Emp_Cnx, ref verificador);
                return verificador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void InitInsert(GridCommandEventArgs e)
        {

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
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "</br></br>', 330, 150);");
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