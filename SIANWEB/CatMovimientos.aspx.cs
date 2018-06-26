using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using Telerik.Web.UI;
using System.Collections;
using CapaNegocios;
using System.IO;

namespace SIANWEB
{
    public partial class CatMovimientos : System.Web.UI.Page
    {
        #region Variables
                private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public string Valor
        {
            get
            {
                return MaximoId();
            }
            set { }
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
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();

                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }

             
                        this.RadMultiPagePrincipal.SelectedIndex = 0;
                        this.RadTabStripPrincipal.SelectedIndex = 0;
                        if (Request.QueryString["ref"] != null)
                        {
                            if (Convert.ToInt32(Request.QueryString["ref"]) == 1)
                            {
                                rbCobranza.Checked = true;
                                rbInventario.Checked = false;
                                this.RbtnTAc_NatMov0.Checked = true;
                                this.RbtnTAc_NatMov1.Checked = false;
                                this.HFNatMov.Value = "0";


                            }
                            else
                            {
                                rbCobranza.Checked = false;
                                rbInventario.Checked = true;
                                this.RbtnTAc_NatMov0.Checked = false;
                                this.RbtnTAc_NatMov1.Checked = true;
                                this.HFNatMov.Value = "1";

                            }
                        }
                        Inicializar();



                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    this.rgMovimiento.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
            }
        }
        protected void GrdApContable_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    this.GrdApContable.DataSource = GetListApl();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "GrdApContable_NeedDataSource");
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
              
                    ErrorManager();
                    RadToolBarButton btn = e.Item as RadToolBarButton;
                    if (btn.CommandName == "save")
                    {
                        if (this.RadTabStripPrincipal.SelectedIndex == 0)
                        {
                            GuardarMovimiento();
                        }
                        else
                        {
                            GuardarAplicacion();
 
                        }
                    }
                    else if (btn.CommandName == "new")
                    {
                        Nuevo();
                    }
                    else if (btn.CommandName == "print")
                    {
                       Imprimir();
                    }
                //}
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadToolBar1_ButtonClick");
            }
        }
        protected void rgMovimiento_ItemCommand(object source, GridCommandEventArgs e)
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

                        txtNumero.Enabled = false;
                        HFId_Mov.Value = rgMovimiento.Items[item]["Id"].Text;
                        txtNumero.Text = rgMovimiento.Items[item]["Id"].Text;
                        cmbTipo.SelectedIndex = cmbTipo.FindItemIndexByValue(rgMovimiento.Items[item]["Tipo"].Text);
                        //trId.Visible = true;
                        txtNombre.Text = rgMovimiento.Items[item]["Nombre"].Text;
                        cmbNaturaleza.SelectedIndex = cmbNaturaleza.FindItemIndexByValue(rgMovimiento.Items[item]["Naturaleza"].Text);
                        txtInverso.Text = cmbInverso.FindItemByValue(rgMovimiento.Items[item]["Inverso"].Text) == null || rgMovimiento.Items[item]["Inverso"].Text == "-1" ? string.Empty : rgMovimiento.Items[item]["Inverso"].Text;
                        cmbInverso.SelectedIndex = cmbInverso.FindItemByValue(rgMovimiento.Items[item]["Inverso"].Text) == null ? 0 : cmbInverso.FindItemIndexByValue(rgMovimiento.Items[item]["Inverso"].Text);
                        cmbInverso.Text = cmbInverso.FindItemByValue(rgMovimiento.Items[item]["Inverso"].Text) == null ? cmbInverso.Items[0].Text : cmbInverso.FindItemByValue(rgMovimiento.Items[item]["Inverso"].Text).Text;
                        chkIva.Checked = Convert.ToBoolean(rgMovimiento.Items[item]["AfeIVA"].Text);
                        chkVenta.Checked = Convert.ToBoolean(rgMovimiento.Items[item]["AfeVta"].Text);
                        chkOrden.Checked = Convert.ToBoolean(rgMovimiento.Items[item]["AfeOrdComp"].Text);
                        chkReqRef.Checked = Convert.ToBoolean(rgMovimiento.Items[item]["ReqReferencia"].Text);
                        chkReqSpo.Checked = Convert.ToBoolean(rgMovimiento.Items[item]["ReqSispropietario"].Text);
                        chkActivo.Checked = Convert.ToBoolean(rgMovimiento.Items[item]["Estatus"].Text);
                        cmbAfecta.SelectedIndex = cmbAfecta.FindItemByValue(rgMovimiento.Items[item]["Afecta"].Text) == null ? 0 : cmbAfecta.FindItemIndexByValue(rgMovimiento.Items[item]["Afecta"].Text);
                        cmbAfecta.Text = cmbAfecta.FindItemByValue(rgMovimiento.Items[item]["Afecta"].Text) == null ? cmbAfecta.Items[0].Text : cmbAfecta.FindItemByValue(rgMovimiento.Items[item]["Afecta"].Text).Text;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_ItemCommand");
            }
        }
        protected void rb_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CargarTipo();
                CargarNaturaleza();
                CargarInverso();

                Nuevo();

                trCobranza.Visible = rbCobranza.Checked;
                //chkIva.Visible = rbCobranza.Checked;
                //chkVenta.Visible = rbCobranza.Checked;

                trInventario.Visible = rbInventario.Checked;
                //chkOrden.Visible = rbInventario.Checked;
                //chkReqRef.Visible = rbInventario.Checked;
                trAfecta.Visible = rbInventario.Checked;
                rgMovimiento.Rebind();

                txtNumero.Text = Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
                            Sesion sesion = new Sesion();                sesion = (Sesion)Session["Sesion" + Session.SessionID];                if (sesion == null)                {                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);                                        Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);                }                CN__Comun comun = new CN__Comun();                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
        }
        protected void rgMovimiento_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {

                rgMovimiento.Rebind();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void GrdApContable_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {

                GrdApContable.Rebind();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Buscar();
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        #endregion
        #region Funciones
        private void CargarAfecta()
        {
            cmbAfecta.Items.Clear();
            string[] Afecta = new string[] { "-- Seleccionar --", "Clientes", "Proveedores" };
            for (int i = 0; i < Afecta.Length; i++)
            {
                cmbAfecta.Items.Add(new RadComboBoxItem(Afecta[i].ToString(), (i - 1).ToString()));
            }
        }
        private void CargarInverso()
        {
            try
            {
                cmbInverso.Items.Clear();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                int NatMov = 1;
                if (rbCobranza.Checked)
                {
                    NatMov = 0;
                }
                CN_Comun.LlenaCombo(1, NatMov, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatMovimiento_Combo", ref cmbInverso);
                cmbInverso.DataValueField = "Id";
                cmbInverso.DataTextField = "Descripcion";
                cmbInverso.DataBind();
                //this.cmbInverso.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
                //txtInverso.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarNaturaleza()
        {
            cmbNaturaleza.Items.Clear();
            string[] tipoInventario = new string[] { "-- Seleccionar --", "Entrada", "Salida" };
            string[] tipoCobranza = new string[] { "-- Seleccionar --", "Abono", "Cargo" };

            if (rbCobranza.Checked)
            {
                for (int i = 0; i < tipoCobranza.Length; i++)
                {
                    cmbNaturaleza.Items.Add(new Telerik.Web.UI.RadComboBoxItem(tipoCobranza[i].ToString(), (i - 1).ToString()));
                    cmbNaturaleza2.Items.Add(new Telerik.Web.UI.RadComboBoxItem(tipoCobranza[i].ToString(), (i - 1).ToString()));
                }
            }
            else
            {
                for (int i = 0; i < tipoInventario.Length; i++)
                {
                    cmbNaturaleza.Items.Add(new Telerik.Web.UI.RadComboBoxItem(tipoInventario[i].ToString(), (i - 1).ToString()));
                    cmbNaturaleza2.Items.Add(new Telerik.Web.UI.RadComboBoxItem(tipoInventario[i].ToString(), (i - 1).ToString()));
                }
            }
        }
        private void CargarTipo()
        {
            cmbTipo.Items.Clear();
            string[] tipoInventario = new string[] { "-- Seleccionar --", "Entrada de almacén", "Salida de almacén", "Factura", "Remisiones", "Devoluciones parciales" };
            string[] tipoCobranza = new string[] { "-- Seleccionar --", "Factura", "Pago", "Nota de cargo", "Nota de crédito" };

            if (rbCobranza.Checked)
            {
                for (int i = 0; i < tipoCobranza.Length; i++)
                {
                    cmbTipo.Items.Add(new Telerik.Web.UI.RadComboBoxItem(tipoCobranza[i].ToString(), (i - 1).ToString()));
                }
            }
            else
            {
                for (int i = 0; i < tipoInventario.Length; i++)
                {
                    cmbTipo.Items.Add(new Telerik.Web.UI.RadComboBoxItem(tipoInventario[i].ToString(), (i - 1).ToString()));
                }
            }
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

                    if (Permiso.PGrabar == false)
                    {
                        this.RadToolBar1.Items[6].Visible = false;
                    }
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                    {
                        this.RadToolBar1.Items[5].Visible = false;
                    }

                    if (Permiso.PImprimir  == false)
                    {
                        this.RadToolBar1.Items[2].Visible = false;
                    }

                    //if (Permiso.PEliminar == false)
                    //{
                    //    this.RadToolBar1.Items[3].Visible = false;
                    //}
                    //if(Permiso.PImprimir == false)
                    //{
                    //    this.RadToolBar1.Items[2].Visible = false;
                    //}

                    //Nuevo
                    //Me.RadToolBar1.Items(6).Enabled = False
                    //Guardar
                    //Me.RadToolBar1.Items(5).Enabled = False
                    //Regresar
                    this.RadToolBar1.Items[4].Visible = false;
                    //Eliminar
                    this.RadToolBar1.Items[3].Visible = false;
                    //Imprimir
                    //this.RadToolBar1.Items[2].Visible = false;
                    //Correo
                    this.RadToolBar1.Items[1].Visible = false;
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }

                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Inicializar()
        {

            txtNumero.Text = Valor;
            CargarTipo();
            CargarNaturaleza();
            CargarInverso();
            CargarAfecta();
            CargarCentros();
            rgMovimiento.Rebind();
            GrdApContable.Rebind();

            trCobranza.Visible = rbCobranza.Checked;
            //chkIva.Visible = rbCobranza.Checked;
            //chkVenta.Visible = rbCobranza.Checked;

            trInventario.Visible = rbInventario.Checked;
            //chkOrden.Visible = rbInventario.Checked;
            //chkReqRef.Visible = rbInventario.Checked;
            trAfecta.Visible = rbInventario.Checked;
        }
        private string MaximoId()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, "CatTMovimiento", Convert.ToInt32(rbInventario.Checked), "Id_Tm", Sesion.Emp_Cnx, "spCatCentral_MaximoMov");
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
                    this.TblEncabezado.Visible = false;

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
        private List<Movimientos> GetList()
        {
            try
            {
                List<Movimientos> List = new List<Movimientos>();
                CN_CatMovimientos clsCatMovimientos = new CN_CatMovimientos();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                clsCatMovimientos.ConsultaMovimientos(rbCobranza.Checked, session2.Id_Emp, session2.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GuardarMovimiento()
        {
            if (this.cmbTipo.SelectedValue != "-1")
            {
                if (this.txtNombre.Text != "")
                {
                    if (this.txtNumero.Text != "")
                    {
                        if (this.cmbNaturaleza.SelectedValue != "-1")
                        {
                            if (this.cmbAfecta.SelectedValue != "-1")
                            {
                                Sesion session2 = new Sesion();
                                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                                Movimientos mv = new Movimientos();
                                mv.Id_Emp = session2.Id_Emp;
                                mv.Nombre = txtNombre.Text;
                                mv.Tipo = Convert.ToInt32(cmbTipo.SelectedValue);
                                mv.Naturaleza = Convert.ToInt32(cmbNaturaleza.SelectedValue);
                                mv.Inverso = Convert.ToInt32(cmbInverso.SelectedValue);
                                mv.Estatus = chkActivo.Checked;
                                mv.Id = Convert.ToInt32(txtNumero.Text);
                                mv.ReqReferencia = chkReqRef.Checked;
                                mv.Afecta = Convert.ToInt32(cmbAfecta.SelectedValue);
                                if (rbCobranza.Checked)
                                {
                                    mv.AfeVta = chkVenta.Checked;
                                    mv.AfeIVA = chkIva.Checked;
                                    mv.ReqSispropietario = false;
                                    mv.ReqReferencia = false;
                                    mv.AfeOrdComp = false;
                                    mv.NatMov = 0;
                                }
                                else
                                {
                                    mv.AfeVta = false;
                                    mv.AfeIVA = false;
                                    mv.AfeOrdComp = chkOrden.Checked;
                                    mv.ReqReferencia = chkReqRef.Checked;
                                    mv.ReqSispropietario = chkReqSpo.Checked;
                                    mv.NatMov = 1;
                                }

                                CN_CatMovimientos clsCatMovimientos = new CN_CatMovimientos();
                                int verificador = 0;
                                if (HFId_Mov.Value == "")
                                {
                                    if (!_PermisoGuardar)
                                    {
                                        Alerta("No tiene permisos para grabar");
                                        return;
                                    }
                                    clsCatMovimientos.InsertarMovimientos(mv, session2.Emp_Cnx, ref verificador);
                                    if (verificador == 1)
                                    {
                                        Inicializar();
                                        Nuevo();
                                        Alerta("Los datos se guardaron correctamente");
                                    }
                                    else
                                    {
                                        Alerta("La clave ya existe");
                                    }
                                }
                                else
                                {
                                    if (!_PermisoModificar)
                                    {
                                        Alerta("No tiene permisos para modificar");
                                        return;
                                    }
                                    //mv.Id = Convert.ToInt32(HFId_Mov.Value);
                                    clsCatMovimientos.ModificarMovimientos(mv, session2.Emp_Cnx, ref verificador);
                                    if (verificador == 1)
                                    {
                                        Alerta("Los datos se modificaron correctamente");
                                        CargarInverso();
                                    }
                                    else
                                    {
                                        Alerta("Ocurrió un error al intentar guardar los cambios");
                                    }
                                }
                                rgMovimiento.Rebind();
                            }
                            else
                            {
                                RfvAfecta.IsValid = false;
                            }
                        }
                        else
                        {
                            RfvNaturaleza.IsValid = false;
                        }
                    }
                    else
                    {
                        this.RfvClave.IsValid = false;
                    }
                }
                else
                {
                    this.RfvNombre.IsValid = false;
                }
            }
            else
            {
                this.RfvTipo.IsValid = false;
            }
           
        }
        private void GuardarAplicacion()
        {
            try
            {
                if (this.CmbAc_Naturaleza.SelectedValue != "-1" )
                {
                    if (this.TxtId_TmAc.Text != "")
                    {
                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        CN_CatMovimientos cn_mov = new CN_CatMovimientos();
                        Movimientos mov = new Movimientos();
                        int Verificador = 0;
                        LlenarObjetoMovimientos(ref mov);

                        cn_mov.InsertarMovApContable(mov, ref Verificador, sesion);

                        if (Verificador == -1)
                        {
                            Alerta("Los datos se guardaron correctamente");
                            InicializarApCont();
                            GrdApContable.Rebind();
                        }
                        else if (Verificador == -2)
                        {
                            Alerta("Los datos se modificaron correctamente");
                            InicializarApCont();
                            GrdApContable.Rebind();
                        }
                        else
                        {
                            Alerta("Error insesperado al tratar de guardar");
                        }
                    }
                    else
                    {
                        this.RfvTxtId_TmAc.IsValid  = false;

                    }
                }
                else
                {
                    this.RfvCmbAc_Nauraleza.IsValid = false;
 
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void LlenarObjetoMovimientos(ref Movimientos mov)
        {
            try
            {
                mov.Id_TAc = int.Parse(this.HdId_TAc.Value);
                mov.Id_Tm = int.Parse(this.TxtId_TmAc.Text);
                mov.TAc_Cuenta = this.TxtTAc_Cuenta.Text;
                mov.TAc_Subcuenta  = this.TxtAc_Subcuenta.Text;
                mov.TAc_Subsubcuenta = this.TxtAc_Subsubcuenta.Text;
                mov.TAc_CuentaA = this.TxtTAc_CuentaA.Text;
                mov.TAc_SubcuentaA = this.TxtAc_SubcuentaA.Text;
                mov.TAc_SubsubcuentaA = this.TxtAc_SubsubcuentaA.Text;
                mov.TAc_CuentaB = this.TxtTAc_CuentaB.Text;
                mov.TAc_SubcuentaB = this.TxtTAc_SubCuentaB.Text;
                mov.TAc_SubsubcuentaB = this.TxtTAc_SubsubCuentaB.Text;
                mov.TAc_CC = this.ChckApCC.Checked;

                mov.TAc_Naturaleza = Convert.ToBoolean(int.Parse(this.CmbAc_Naturaleza.SelectedValue));

                if (this.RbtnTAc_NatMov0.Checked == false)
                {
                    mov.TAc_NatMov = true;
                }
                else
                {
                    mov.TAc_NatMov = false;
                }
                

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void Nuevo()
        {
            txtNumero.Enabled = true;
            HFId_Mov.Value = "";
            txtNumero.Text = Valor;
            cmbTipo.SelectedIndex = 0;
             
            txtNombre.Text = "";
            cmbNaturaleza.SelectedIndex = 0;
            txtInverso.Text = "";
            cmbInverso.SelectedIndex = 0;
            cmbInverso.Text = cmbInverso.FindItemByValue("-1").Text;
            cmbAfecta.SelectedIndex = 0;
            chkIva.Checked = false;
            chkVenta.Checked = false;
            chkOrden.Checked = false;
            chkReqRef.Checked = false;
            chkReqSpo.Checked = false;
            chkActivo.Checked = true;
        }
        private void Buscar()
        {
            try
            {
                if (this.TxtId_TmAc.Text != "")
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    Movimientos movimientos = new Movimientos();
                    CN_CatMovimientos cn_movimientos = new CN_CatMovimientos();

                    int Id_Tm = int.Parse(this.TxtId_TmAc.Text);
                    bool Tm_NatMov = false;
                    bool Tm_Naturaleza = Convert.ToBoolean(int.Parse(this.cmbNaturaleza2.SelectedValue));


                    if (this.RbtnTAc_NatMov0.Checked == false)
                    {
                        Tm_NatMov = true;
                    }

                    cn_movimientos.ConsultaMApContable(ref movimientos, Id_Tm, Tm_NatMov,Tm_Naturaleza, sesion);
  
                    if (movimientos.Id_TAc != 0)
                    {
                        this.RfvTxtId_TmAc.Enabled = false;
                        this.HdId_TAc.Value = movimientos.Id_TAc.ToString();
                        this.TxtTm_Nombre.Text = movimientos.Nombre;
                        this.TxtTAc_Cuenta.Text = movimientos.TAc_Cuenta.ToString();
                        this.TxtAc_Subcuenta.Text = movimientos.TAc_Subcuenta.ToString();
                        this.TxtAc_Subsubcuenta.Text = movimientos.TAc_Subsubcuenta.ToString();
                        this.TxtTAc_CuentaA.Text = movimientos.TAc_CuentaA.ToString();
                        this.TxtAc_SubcuentaA.Text = movimientos.TAc_SubcuentaA.ToString();
                        this.TxtAc_SubsubcuentaA.Text = movimientos.TAc_SubsubcuentaA.ToString();
                        this.TxtTAc_CuentaB.Text = movimientos.TAc_CuentaB.ToString();
                        this.TxtTAc_SubCuentaB.Text = movimientos.TAc_SubcuentaB.ToString();
                        this.TxtTAc_SubsubCuentaB.Text = movimientos.TAc_SubsubcuentaB.ToString();


                        if (movimientos.Id_TAc == -1)
                        {
                            CmbAc_Naturaleza.SelectedValue = "-1";
                        }
                        else
                        {
                            CmbAc_Naturaleza.SelectedValue = Convert.ToString(Convert.ToInt32(movimientos.TAc_Naturaleza));
                        }

                        this.ChckApCC.Checked = movimientos.TAc_CC;


                    }
                    else if (movimientos.Id_TAc == 0)
                    {
                        Alerta("El movimiento no fue encontrado");
                        InicializarApCont();
                    }
                }
                else
                {
                    this.RfvTxtId_TmAc.IsValid = false;
 
                }

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void InicializarApCont()
        {
            try
            {

                this.HdId_TAc.Value = "";
                this.TxtId_TmAc.Text = string.Empty;
                this.TxtTm_Nombre.Text = string.Empty;
                this.TxtTAc_Cuenta.Text = string.Empty;
                this.TxtAc_Subcuenta.Text = string.Empty;
                this.TxtAc_Subsubcuenta.Text = string.Empty;
                this.TxtTAc_CuentaA.Text = string.Empty;
                this.TxtAc_SubcuentaA.Text = string.Empty;
                this.TxtAc_SubsubcuentaA.Text = string.Empty;
                this.TxtTAc_CuentaB.Text = string.Empty;
                this.TxtTAc_SubCuentaB.Text = string.Empty;
                this.TxtTAc_SubsubCuentaB.Text = string.Empty;
                this.CmbAc_Naturaleza.SelectedValue = "-1";
                this.RfvTxtId_TmAc.Enabled = true;
                this.ChckApCC.Checked = false;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private List<Movimientos> GetListApl()
        {
            try
            {
                List<Movimientos> List = new List<Movimientos>();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatMovimientos cn_mov = new CN_CatMovimientos();
                bool Tm_NatMov = false;

                if (this.RbtnTAc_NatMov0.Checked == false)
                {
                    Tm_NatMov = true;
                }
                else
                {
                    Tm_NatMov = false;
                }

                cn_mov.ConsultaListMovApContable(ref List, Tm_NatMov, sesion);

                return List;
               

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
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();

                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                ALValorParametrosInternos.Add(sesion.Id_Emp);
                ALValorParametrosInternos.Add(sesion.U_Nombre);



                Type instance = null;

                instance = typeof(LibreriaReportes.Reporte_Movimientos);


                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;
                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");

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