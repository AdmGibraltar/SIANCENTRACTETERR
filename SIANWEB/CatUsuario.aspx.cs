﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using Telerik.Web.UI;
using System.Configuration;
using System.Net.Mail;
using CapaNegocios;
using CapaDatos;
using System.Collections;
using System.Net.Mime;
using System.Data.SqlClient;

namespace SIANWEB 
{
    public partial class CatUsuario : System.Web.UI.Page
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
        private List<Comun> CentrosSeleccionados
        {
            get { return (List<Comun>)Session["CentrosSeleccionados" + Session.SessionID]; }
            set { Session["CentrosSeleccionados" + Session.SessionID] = value; }
        }
        private List<RelacionGestor> list
        {
            get { return (List<RelacionGestor>)Session["ListaRelacionGestor" + Session.SessionID]; }
            set { Session["ListaRelacionGestor" + Session.SessionID] = value; }
        }
        private string Emp_CnxCob
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCobranza"); }
        }
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
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {

                        ValidarPermisos();
                        if (!Sesion.Cu_Modif_Pass_Voluntario)
                        {
                            RadAjaxManager1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                        }
                        CargarComboTipoU();
                        this.chkActivo.Checked = true;
                        this.txtNombre.Focus();
                        this.txtFechaNac.MaxDate = DateTime.Now.AddYears(-16).AddDays(1);
                        AddExpression();
                        rgSucursal.Rebind();
                        this.RadGrid1.Rebind();
                        CargarCentros();
                        List<Convenio> List = new List<Convenio>();
                       

                        list = new List<RelacionGestor>();

                        chkCredito.Attributes.Add("onclick", "javascript:Habilita(this);");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                switch (btn.CommandName)
                {
                    case "save":
                        Guardar();
                        break;
                    case "new":
                        Nuevo();
                        break;
                    case "config":
                        Abrir();
                        break;
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadToolBar1_ButtonClick");
            }

        }

        private void Abrir()
        {
            try
            {
                Comun cds;
                CentrosSeleccionados = new List<Comun>();
                foreach (RadListBoxItem item in RadListBox1.Items)
                {
                    if (item.Checked)
                    {
                        cds = new Comun();
                        cds.Id = Convert.ToInt32(item.Value);
                        cds.Descripcion = item.Text;
                        CentrosSeleccionados.Add(cds);
                    }
                }

                RadAjaxManager1.ResponseScripts.Add("AbrirConf();");
                //var oWnd = radopen(dir, "AbrirRelacionGestor");
                //oWnd.center();
                //oWnd.Maximize();
                //args.set_cancel(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    RadGrid1.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
            }

        }
        protected void rgSucursal_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgSucursal.DataSource  = GetListSucursales();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
            }

        }
        protected void RadGrid1_SortCommand(object source, GridSortCommandEventArgs e)
        {
            ErrorManager();
            try
            {
                this.RadGrid1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_SortCommand");
            }
        }
        protected void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            ErrorManager();
            try
            {
                this.RadGrid1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_PageIndexChanged");
            }
        }
        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.CommandName.ToString() == "Modificar")
                {
                    Int32 item = default(Int32);
                    item = e.Item.ItemIndex;
                    if (item >= 0)
                    {
                        CN__Comun.RemoverValidadores(Validators);

                        this.HiddenId_Ofi.Value = this.RadGrid1.Items[item]["Id_Cd"].Text;
                        this.HiddenId_U.Value = this.RadGrid1.Items[item]["Id_U"].Text;
                        this.txtNombre.Text = this.RadGrid1.Items[item]["U_Nombre"].Text;

                        rgSucursal.Rebind();
                        if (this.RadGrid1.Items[item]["U_FNac"].Text != "01/01/0001 12:00:00 a.m.")
                        {
                            this.txtFechaNac.SelectedDate = Convert.ToDateTime(Convert.ToDateTime(this.RadGrid1.Items[item]["U_FNac"].Text));
                        }
                        else
                        {
                            this.txtFechaNac.SelectedDate = null;
                        }
                        this.txtCorreo.Text = this.RadGrid1.Items[item]["U_Correo"].Text;
                        this.txtUsuario.Text = this.RadGrid1.Items[item]["Cu_User"].Text;
                        this.HiddenU_Usuario.Value = this.RadGrid1.Items[item]["Cu_User"].Text;
                        this.cboTipoUsuario.SelectedValue = this.RadGrid1.Items[item]["Id_Tu"].Text;
                        this.cboTipoUsuario_SelectedIndexChanged(null, null);
                        this.HiddenId_TU.Value = this.RadGrid1.Items[item]["Id_Tu"].Text;
                        this.chkActivo.Checked = Convert.ToBoolean(this.RadGrid1.Items[item]["U_Activo"].Text);
                        this.chkVerTodo.Checked = Convert.ToBoolean(this.RadGrid1.Items[item]["U_VerTodo"].Text);
                        this.chkMultiOficina.Checked = Convert.ToBoolean(this.RadGrid1.Items[item]["U_MultiCentro"].Text);
                        this.chkCredito.Checked = Convert.ToBoolean(this.RadGrid1.Items[item]["U_SusCredito"].Text);
                        txtDias.Value = this.RadGrid1.Items[item]["U_DiasVencimiento"].Text == "&nbsp;" ? (double?)null : Convert.ToDouble(this.RadGrid1.Items[item]["U_DiasVencimiento"].Text);

                        txtDias.Enabled = chkCredito.Checked;

                        TrRepresentante.Visible = false;
                        ListUen.Visible = false;
                        lblUen.Visible = false;
                        ListSegmento.Visible = false;
                        lblSegmento.Visible = false;
                        chkMultiOficina.Enabled = true;
                        RadToolBar1.FindItemByValue("config").Visible = !(cboTipoUsuario.SelectedValue == "2" || cboTipoUsuario.SelectedValue == "0");
                        if (this.RadGrid1.Items[item]["Id_Tu"].Text == "2")
                        {
                            CargarRik();
                            TrRepresentante.Visible = true;
                            this.cmbRepresentante.SelectedIndex = cmbRepresentante.FindItemIndexByValue(RadGrid1.Items[item]["Id_Rik"].Text);
                            if (cmbRepresentante.SelectedIndex != -1)
                            {
                                this.cmbRepresentante.Text = cmbRepresentante.FindItemByValue(RadGrid1.Items[item]["Id_Rik"].Text).Text;
                            }
                            chkMultiOficina.Enabled = false;
                        }


                        else if (this.RadGrid1.Items[item]["Id_Tu"].Text == "8")
                        {
                            CargarUen();

                            ListUen.Visible = true;
                            lblUen.Visible = true;

                        }
                        else if (this.RadGrid1.Items[item]["Id_Tu"].Text == "7")
                        {
                            CargarSegmentos();

                            ListSegmento.Visible = true;
                            lblSegmento.Visible = true;
                        }
                        RadListBox1.Enabled = chkMultiOficina.Checked;
                        if (chkMultiOficina.Checked)
                        {
                            SeleccionarCentros(RadGrid1.Items[item]["Id_Cd"].Text);
                        }
                        CargarRelGestor(Convert.ToInt32(HiddenId_U.Value));
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_ItemCommand");
            }
        }
        protected void cboTipoUsuario_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                if (this.cboTipoUsuario.SelectedValue != "0")
                {
                    //cargarComboJefe();
                    //this.CboJefeDirecto.Focus();
                }
                else
                {
                    CargarComboTipoU();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                CargarComboTipoU();
                Nuevo();
                RadGrid1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void chkMultiOficina_CheckedChanged(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            RadListBox1.Enabled = chkMultiOficina.Checked;

            foreach (RadListBoxItem rlbi in RadListBox1.Items)
            {
                if (rlbi.Enabled)
                {
                    rlbi.Checked = false;
                }
                else
                {
                    rlbi.Checked = true;
                    rlbi.Enabled = false;
                }
            }
        }
        protected void ChkSeleccionado_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CheckBox chkver = (sender as CheckBox);
                int Id_Tipo = Convert.ToInt32((chkver.Parent.Parent.FindControl("LblId_Tipo") as Label).Text);
                int Id_CD = Convert.ToInt32((chkver.Parent.Parent.FindControl("LblId_CD") as Label).Text);
                if (Id_CD == -1)
                {
                    foreach (GridDataItem grd in rgSucursal.Items)
                    {

                        int Id_Tipo2 = int.Parse((grd.Controls[0].FindControl("LblId_Tipo") as Label).Text);
                        if (Id_Tipo == Id_Tipo2)
                        {

                            CheckBox chkvr = grd.Controls[0].FindControl("ChkSeleccionado") as CheckBox;
                            chkvr.Checked = chkver.Checked;
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        #endregion
        #region Funciones
        private void CargarRelGestor(int Id_U)
        {
            try
            {
                List<RelacionGestor> list2 = new List<RelacionGestor>();
                CN_GestorCobranza cn_gestor = new CN_GestorCobranza();
                Cobranza cob = new Cobranza();
                cob.Id_Emp = sesion.Id_Emp;
                cob.Id_Cd = sesion.Id_Cd_Ver;
                cob.Id_U = Id_U;
                cob.DbName = (new SqlConnectionStringBuilder(sesion.Emp_Cnx)).InitialCatalog;
                cn_gestor.ConsultarRelaciones(cob, ref list2, Emp_CnxCob);

                list = list2.Where(RelacionGestor => RelacionGestor.Id_Cte != null).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private void AddExpression()
        {
            GridSortExpression expression2 = new GridSortExpression();
            expression2.FieldName = "U_Nombre";
            expression2.SetSortOrder("Ascending");
            this.RadGrid1.MasterTableView.SortExpressions.AddSortExpression(expression2);
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

                CD_PermisosU CN_PermisosU = new CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;
                    if (Permiso.PGrabar == false)
                        this.RadToolBar1.Items[6].Visible = false;
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
                }
                else
                    Response.Redirect("Inicio.aspx");
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
                list = new List<RelacionGestor>();
                this.HiddenId_Ofi.Value = string.Empty;
                this.HiddenId_U.Value = string.Empty;
                this.HiddenU_Usuario.Value = string.Empty;
                this.HiddenId_TU.Value = string.Empty;
                this.txtNombre.Text = string.Empty;
                this.txtCorreo.Text = string.Empty;
                this.txtFechaNac.SelectedDate = null;
                this.txtUsuario.Text = string.Empty;
                RadComboBoxItem item1 = new RadComboBoxItem();
                item1.Value = "0";
                item1.Text = "-- Seleccionar --";
                this.chkActivo.Checked = true;
                this.chkMultiOficina.Checked = false;
                this.chkVerTodo.Checked = false;
                this.RadGrid1.Rebind();
                rgSucursal.Rebind();

                RadListBox1.Enabled = false;
                foreach (RadListBoxItem rlbi in RadListBox1.Items)
                {
                    if (rlbi.Value == sesion.Id_Cd_Ver.ToString())
                    {
                        rlbi.Checked = true;
                        rlbi.Enabled = false;
                    }
                    else
                    {
                        rlbi.Checked = false;
                        rlbi.Enabled = true;
                    }
                }
                cboTipoUsuario.SelectedIndex = 0;
                cboTipoUsuario.Text = cboTipoUsuario.Items[0].Text;

                if (cmbRepresentante.Items.Count > 0)
                {
                    cmbRepresentante.SelectedIndex = 0;
                    cmbRepresentante.Text = cmbRepresentante.Items[0].Text;
                }
                TrRepresentante.Visible = false;

                ListUen.Items.Clear();
                ListSegmento.Items.Clear();

                ListUen.Visible = false;
                lblUen.Visible = false;

                ListSegmento.Visible = false;
                lblSegmento.Visible = false;

                chkCredito.Checked = false;
                txtDias.Value = null;

                RadToolBar1.FindItemByValue("config").Visible = !(cboTipoUsuario.SelectedValue == "2" || cboTipoUsuario.SelectedValue == "0");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Guardar()
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatUsuario clsCatUsuario = new CN_CatUsuario();
                Usuario usuario = new Usuario();
                usuario.Id_Emp = sesion.Id_Emp;
                usuario.U_Nombre = this.txtNombre.Text;

                usuario.U_Correo = this.txtCorreo.Text;
                usuario.Cu_User = this.txtUsuario.Text;
                if (txtFechaNac.SelectedDate.HasValue)
                    usuario.U_FNac = this.txtFechaNac.SelectedDate.Value;

                usuario.Id_Id_U = 1;
                usuario.Id_TU = Convert.ToInt32(this.cboTipoUsuario.SelectedValue);

                usuario.Id_Rik = usuario.Id_TU == 2 || usuario.Id_TU == 4 ? Convert.ToInt32(cmbRepresentante.SelectedValue) : -1;
                usuario.U_Activo = this.chkActivo.Checked;
                usuario.U_VerTodo = this.chkVerTodo.Checked;
                usuario.U_MultiCentro = this.chkMultiOficina.Checked;
                usuario.Id_Centros = ObtenerCentros();
                usuario.U_SusCredito = chkCredito.Checked;
                usuario.U_DiasVencimiento = txtDias.Value;
                //usuario.Id_Emp = sesion.Id_Emp;


                Int32 verificador = default(Int32);
                int Verificador2 = 0;

                if (this.HiddenId_TU.Value != this.cboTipoUsuario.SelectedValue)
                    verificador = 1;
                else
                    verificador = 0;

                 

                ArrayList seleccionados = new ArrayList();
                if (cboTipoUsuario.SelectedValue == "8")
                {
                    foreach (RadListBoxItem i in ListUen.Items)
                    {
                        if (i.Checked)
                        {
                            seleccionados.Add(i.Value);
                        }
                    }
                }
                else if (cboTipoUsuario.SelectedValue == "7")
                {
                    foreach (RadListBoxItem i in ListSegmento.Items)
                    {
                        if (i.Checked)
                        {
                            seleccionados.Add(i.Value);
                        }
                    }
                }
                //Nuevo usuario
                if (this.HiddenId_U.Value == string.Empty)
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }
                    usuario.Id_Cd = sesion.Id_Cd_Ver;
                    clsCatUsuario.InsertarUsuario(ref usuario, sesion.Emp_Cnx, seleccionados, ref verificador,ref Verificador2, list, Emp_CnxCob);
                    if (verificador == -1)
                    {
                        this.txtUsuario.Focus();
                        Alerta("El usuario introducido ya se encuentra registrado");
                    }
                    else if (verificador == -2)
                    {
                        this.cmbRepresentante.Focus();
                        Alerta("El usuario ya está asociado a otro representante");
                    }
                    else
                    {
                        InsertarPermisosSucursal(Verificador2);
                        DatosParaCorreo(ref usuario, verificador, sesion.Emp_Cnx);
                        RadGrid1.Rebind();
                        //Deja limpio para un nuevo
                        Nuevo();
                        rgSucursal.Rebind();
                        Alerta("Los datos se guardaron correctamente");
                    }//Modificación de usuario
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }
                    usuario.Id_Cd = Convert.ToInt32(this.HiddenId_Ofi.Value);
                    usuario.Id_U = Convert.ToInt32(this.HiddenId_U.Value);
                    clsCatUsuario.ModificarUsuario(usuario, sesion.Emp_Cnx, seleccionados, ref verificador, list, Emp_CnxCob);
                    if (verificador == 1)
                    {
                        this.txtUsuario.Focus();
                        Alerta("El usuario introducido ya se encuentra registrado");
                    }
                    else if (verificador == 2)
                    {
                        this.cmbRepresentante.Focus();
                        Alerta("El usuario ya está asociado a otro representante");
                    }
                    else
                    {
                        InsertarPermisosSucursal(Convert.ToInt32(this.HiddenId_U.Value));
                        RadGrid1.Rebind();
                        Nuevo();
                        Alerta("Los datos se modificaron correctamente");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private ArrayList ObtenerCentros()
        {
            ArrayList centros = new ArrayList();
            foreach (RadListBoxItem rlbi in RadListBox1.Items)
            {
                if (rlbi.Checked)
                {
                    if (!chkMultiOficina.Checked)
                    {
                        rlbi.Checked = false;
                    }
                    else
                    {
                        centros.Add(rlbi.Value);
                    }

                    if (list.Where(RelacionGestor => RelacionGestor.Id_Cd == rlbi.Value).ToList().Count == 0)
                    {
                        RelacionGestor rg = new RelacionGestor();
                        rg.Id_Emp = sesion.Id_Emp;
                        rg.Id_Cd = rlbi.Value;
                        rg.GUID = System.Guid.NewGuid().ToString();
                        list.Add(rg);
                    }
                }
            }
            return centros;
        }
        private void DatosParaCorreo(ref Usuario Usuario, Int32 Verificador, string conexion)
        {
            try
            {
                Int32 Id = default(Int32);
                ConfiguracionGlobal Configuracion = new ConfiguracionGlobal();
                CentroDistribucion Cdis = new CentroDistribucion();
                CapaNegocios.CN_Login CN_login = new CapaNegocios.CN_Login();
                Id = 0;
                CN_login.RecuperarContraseña(ref Usuario, ref Cdis, ref Configuracion, out Id, conexion);
                EnviaEmail(Usuario, Cdis, Configuracion, Verificador.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void EnviaEmail(Usuario Usuario, CentroDistribucion Cdis, ConfiguracionGlobal Configuracion, string Password)
        {
            try
            {
                //*****Saco el email ********
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                SmtpClient smtp = new SmtpClient();
                correo.From = new MailAddress(Configuracion.Mail_Remitente);
                correo.To.Add(Usuario.U_Correo);
                correo.Subject = ("Cuenta de acceso");
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString("" + "<div align='center'>" + " <table>" + "  <tr>" + "   <td><IMG SRC='cid:companylogo' ALIGN='left'></td>" + "   <td valign='middle' style='text-decoration: underline'><b><font face= 'Tahoma' size = '4'>Cuenta de acceso</font></b></td>" + "  </tr>" + "  <tr>" + "   <td colspan='2'><br><br><br></td>" + "  </tr>" + "  <tr>" + "   <td colspan='2'><b><font face= 'Tahoma' size = '2'>Anexo se encuentra el usuario y contraseña de su cuenta de acceso</font></b></td>" + "  </tr>" + "  <tr>" + "   <td colspan='2'><br><br></td>" + "  </tr>" + "  <tr>" + "   <td align='right'><b><font face= 'Tahoma' size = '2'>Usuario:</font></b></td>" + "   <td align='left'><b><font face= 'Tahoma' size = '2' color='#777777'>" + Usuario.Cu_User + "</font></b></td>" + "  </tr>" + "  <tr>" + "   <td align='right'><b><font face= 'Tahoma' size = '2'>Contraseña:</font></b></td><td align='left'><b><font face= 'Tahoma' size = '2' color='#777777'>" + Usuario.Cu_pass + "</font></b></td>" + "  </tr>" + "  <tr>" + "   <td colspan='2'><br><br></td>" + "  </tr>" + "  <tr>" + "   <td align ='center' colspan='2'><b><font face= 'Tahoma' size = '2'></b></td>" + "  </tr>" + "  <tr>" + "   <td align ='Left' colspan='2'><b><font face= 'Tahoma' size = '2'>" + Cdis.Cd_Descripcion + "</font></b></td>" + "  </tr>" + "  <tr>" + "  <tr>" + "   <td align ='Left' colspan='2'><b><font face= 'Tahoma' size = '2'>" + Cdis.Cd_Tel + "</font></b></td>" + "  </tr>" + "  <tr>" + "   <td align ='Left' colspan='2'><b><font face= 'Tahoma' size = '2'></font></b>" + "   </td>" + "  </tr>" + "  <tr>" + "   <td align ='Left' colspan='2'><b><font face= 'Tahoma' size = '2'>" + Cdis.Cd_CalleNo + "</font></b></td>" + "  </tr>" + " </table>" + "</div>", null, "text/html");

                try
                {
                    LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
                    logo.ContentId = "companylogo";
                    htmlView.LinkedResources.Add(logo);
                }
                catch (Exception)
                {
                }
                correo.AlternateViews.Add(htmlView);
                correo.IsBodyHtml = true;
                smtp.Host = Configuracion.Mail_Servidor;
                smtp.Port = Convert.ToInt32(Configuracion.Mail_Puerto);
                //Estoy hay que ponerlo cuando se ponga en un host para que si lo mande
                smtp.Credentials = new System.Net.NetworkCredential(Configuracion.Mail_Usuario, Configuracion.Mail_Contraseña);
                smtp.EnableSsl = false;
                smtp.Send(correo);
            }
            catch (Exception)
            {
                Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
            }
        }
        private List<Usuario> GetList()
        {
            try
            {
                List<Usuario> List = new List<Usuario>();
                CapaNegocios.CN_CatUsuario clsCatUsuario = new CapaNegocios.CN_CatUsuario();
                Usuario usuario = new Usuario();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                usuario.Id_Cd = session2.Id_Cd_Ver;
                usuario.Id_Emp = session2.Id_Emp;
                clsCatUsuario.ConsultaUsuarios(usuario, session2.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarComboTipoU()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "SpSysTipoUsuario_Combo", ref this.cboTipoUsuario);

                //Comentado porque ya no hay usuarios que dependan del administrador               
                Telerik.Web.UI.RadComboBoxItem item = new Telerik.Web.UI.RadComboBoxItem();
                item.Value = "0";
                item.Text = "-- Seleccionar --";
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
                CN_Comun.LlenaListBox(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref RadListBox1);
                RadListBox1.Items.Remove(RadListBox1.FindItemByValue("-1"));
                RadListBox1.Items.Remove(RadListBox1.FindItemByValue("0"));
                RadListBox1.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Enabled = false;
                RadListBox1.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Checked = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SeleccionarCentros(string cd)
        {
            ArrayList centros = new ArrayList();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_CatUsuario cn_usuario = new CN_CatUsuario();
            cn_usuario.ConsultaUsuarioCentro(Sesion.Id_Emp, sesion.Id_Cd, HiddenId_U.Value, Sesion.Emp_Cnx, ref centros);

            foreach (RadListBoxItem rlbi in RadListBox1.Items)
            {
                if (rlbi.Value == cd)
                {
                    rlbi.Checked = true;
                    rlbi.Enabled = false;
                }
                else
                {
                    rlbi.Checked = centros.Contains(Convert.ToInt32(rlbi.Value));
                    rlbi.Enabled = true;
                }
            }
        }
        private void CargarRik()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatRik_Combo", ref cmbRepresentante);
                cmbRepresentante.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cboTipoUsuario_SelectedIndexChanged1(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            TrRepresentante.Visible = false;

            ListUen.Visible = false;
            lblUen.Visible = false;
            ListSegmento.Visible = false;
            lblSegmento.Visible = false;
            cmbRepresentante.Items.Clear();
            chkMultiOficina.Enabled = true;
            ListUen.Items.Clear();
            ListSegmento.Items.Clear();

            RadToolBar1.FindItemByValue("config").Visible = !(cboTipoUsuario.SelectedValue == "2" || cboTipoUsuario.SelectedValue == "0");


            if (cboTipoUsuario.SelectedValue == "2")
            {
                chkMultiOficina.Enabled = false;
                chkMultiOficina.Checked = false;
                foreach (RadListBoxItem rlbi in RadListBox1.Items)
                {
                    rlbi.Checked = false;
                    rlbi.Enabled = true;
                }
                RadListBox1.Enabled = false;
                TrRepresentante.Visible = true;
                CargarRik();



            }
            else if (cboTipoUsuario.SelectedValue == "8")
            {
                CargarUen();

                ListUen.Visible = true;
                lblUen.Visible = true;
            }
            else if (cboTipoUsuario.SelectedValue == "7")
            {
                CargarSegmentos();
                ListSegmento.Visible = true;
                lblSegmento.Visible = true;
            }
        }
        private void CargarSegmentos()
        {
            try
            {
                CN_CatSegmentos cnSegmento = new CN_CatSegmentos();
                List<Segmentos> list = new List<Segmentos>();
                cnSegmento.ConsultaSegmento_Usuario(ref list, sesion.Id_Emp, HiddenId_U.Value == "" ? (int?)null : Convert.ToInt32(HiddenId_U.Value), sesion.Emp_Cnx);
                ListSegmento.Items.Clear();
                foreach (Segmentos u in list)
                {
                    RadListBoxItem lbi = new RadListBoxItem();
                    lbi.Text = u.Descripcion;
                    lbi.Value = u.Id_Seg.ToString();
                    lbi.Checked = u.Id_U == null ? false : true;
                    ListSegmento.Items.Add(lbi);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarUen()
        {
            try
            {
                CN_CatUen cnUen = new CN_CatUen();
                List<Uen> list = new List<Uen>();
                cnUen.ConsultaUen_Usuario(ref list, sesion.Id_Emp, HiddenId_U.Value == "" ? (int?)null : Convert.ToInt32(HiddenId_U.Value), sesion.Emp_Cnx);
                ListUen.Items.Clear();
                foreach (Uen u in list)
                {
                    RadListBoxItem lbi = new RadListBoxItem();
                    lbi.Text = u.Descripcion;
                    lbi.Value = u.Id.ToString();
                    lbi.Checked = u.Id_U == null ? false : true;
                    ListUen.Items.Add(lbi);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<Convenio> GetListSucursales()
        {
            try
            {
                List<Convenio> List = new List<Convenio>();
                int Id_U = HiddenId_U.Value == "" ? -1 : Convert.ToInt32 (HiddenId_U.Value);
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_Convenio cn_conv = new CN_Convenio();
                cn_conv.ConsultaPermisosSucursal(Id_U, ref List, sesion.Emp_Cnx);


                return List;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
  
        }
        private void InsertarPermisosSucursal(int Id_U)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_Convenio cn_conv = new CN_Convenio();
                List<Convenio> List = new List<Convenio>();
                int Verificador = 0;

                LlenarLista(ref List, Id_U);
                cn_conv.InsertarPermisosSucursal(List, ref Verificador, sesion.Emp_Cnx);

                if (Verificador != -1)
                {
                    Alerta("Error inesperado al tratar de guardar los datos");
                }


            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
         private void LlenarLista(ref List<Convenio> List, int Id_U)
         {
             try
             {
                 Convenio c;
                 foreach (GridDataItem grd in rgSucursal.Items)
                 {
                     c = new Convenio();
                     if (int.Parse((grd.Controls[0].FindControl("LblId_CD") as Label).Text) != -1)
                     {

                         c.Id_U =Id_U;
                         c.Id_Cd = int.Parse((grd.Controls[0].FindControl("LblId_CD") as Label).Text);
                         c.Seleccionado = (grd.Controls[0].FindControl("ChkSeleccionado") as CheckBox).Checked;
                         List.Add(c);
                     }


                 }

             }
             catch (Exception ex)
             {

                 throw ex;
             }

         }
        #endregion
        #region ErrorManager
        private void Alerta(string mensaje)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
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