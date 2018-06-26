using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.Globalization;

namespace SIANWEB
{
    public partial class CatTerritorios : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        DataTable dt { get { return (DataTable)Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
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
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
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
                        CargarCentros();
                        Inicializar();

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion(); sesion = (Sesion)Session["Sesion" + Session.SessionID]; if (sesion == null) { string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries); Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false); } CN__Comun comun = new CN__Comun(); comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                Nuevo();
                Inicializar();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    ErrorManager();
                    RadToolBarButton btn = e.Item as RadToolBarButton;
                    if (btn.CommandName == "save")
                    {
                        Guardar();
                    }
                    else if (btn.CommandName == "new")
                    {
                        Nuevo();
                    }
                    else if (btn.CommandName == "undo")
                    {
                        //Regresar()
                    }
                }
                else
                {
                    RadTabStrip1.Tabs[0].Selected = true;
                    RadMultiPage1.PageViews[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }
        protected void rg1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rg1.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_NeedDataSource");
            }
        }
        protected void rg1_ItemCommand(object source, GridCommandEventArgs e)
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

                        txtClave.Enabled = false;
                        HF_ID.Value = rg1.Items[item]["Id_Ter"].Text;
                        txtClave.Text = rg1.Items[item]["Id_Ter"].Text;
                        txtDescripcion.Text = rg1.Items[item]["Descripcion"].Text;
                        txtUen.Text = rg1.Items[item]["Id_Uen"].Text;
                        if (cmbUen.FindItemIndexByValue(rg1.Items[item]["Id_Uen"].Text) > 0)
                        {
                            cmbUen.SelectedIndex = cmbUen.FindItemIndexByValue(rg1.Items[item]["Id_Uen"].Text);
                            cmbUen.Text = cmbUen.FindItemByValue(rg1.Items[item]["Id_Uen"].Text).Text;
                        }
                        else
                        {
                            cmbUen.SelectedIndex = 0;
                            cmbUen.Text = cmbUen.Items[0].Text;
                            txtUen.Text = "";
                        }
                        CargarRik();
                        CargarSeg();
                        txtRik.Text = rg1.Items[item]["Id_Rik"].Text == "-1" ? string.Empty : rg1.Items[item]["Id_Rik"].Text;
                        if (cmbRik.FindItemIndexByValue(rg1.Items[item]["Id_Rik"].Text) > 0)
                        {
                            cmbRik.SelectedIndex = cmbRik.FindItemIndexByValue(rg1.Items[item]["Id_Rik"].Text);
                            cmbRik.Text = cmbRik.FindItemByValue(rg1.Items[item]["Id_Rik"].Text).Text;
                        }
                        else
                        {
                            cmbRik.SelectedIndex = 0;
                            cmbRik.Text = cmbRik.Items[0].Text;
                            txtRik.Text = "";
                        }
                        txtSegmento.Text = rg1.Items[item]["Id_Seg"].Text == "-1" ? string.Empty : rg1.Items[item]["Id_Seg"].Text;
                        if (cmbSegmento.FindItemIndexByValue(rg1.Items[item]["Id_Seg"].Text) > 0)
                        {                          
                            cmbSegmento.SelectedIndex = cmbSegmento.FindItemIndexByValue(rg1.Items[item]["Id_Seg"].Text);
                            cmbSegmento.Text = cmbSegmento.FindItemByValue(rg1.Items[item]["Id_Seg"].Text).Text;
                        }
                        else
                        {
                            cmbSegmento.SelectedIndex = 0;
                            cmbSegmento.Text = cmbSegmento.Items[0].Text;
                            txtSegmento.Text = "";
                        }
                        chkActivo.Checked = Convert.ToBoolean(rg1.Items[item]["Estatus"].Text);
                        GetListDet();
                        rgDet.Rebind();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_ItemCommand");
            }
        }
        protected void rg1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rg1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_PageIndexChanged");
            }
        }
        protected void rgDet_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgDet.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDet_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                string anyo = "";
                string anyo_old = "";
                string mes = "";
                string mesStr = "";
                string mes_old = "";
                double presupuesto = 0;
                double presupuesto_old = 0;
                GridItem gi = null;
                DataRow[] Ar_dr;

                switch (e.CommandName)
                {
                    case "PerformInsert":
                        gi = e.Item;


                        if (((RadNumericTextBox)gi.FindControl("RadNumericTextBox1")).Text == "" ||
                            ((RadComboBox)gi.FindControl("RadComboBox1")).SelectedIndex == 0 ||
                            ((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Text == "")
                        {
                            Alerta("El año, mes y presupuesto son obligatorios");
                            e.Canceled = true;
                            break;
                        }


                        anyo = ((RadNumericTextBox)gi.FindControl("RadNumericTextBox1")).Text;
                        mes = ((RadComboBox)gi.FindControl("RadComboBox1")).SelectedValue;
                        mesStr = ((RadComboBox)gi.FindControl("RadComboBox1")).Text;
                        presupuesto = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Text);


                        Ar_dr = dt.Select("Anyo='" + anyo + "' and Mes='" + mes + "'");
                        if (Ar_dr.Length == 0)
                        {
                            dt.Rows.Add(new object[] { anyo, mes, mesStr, presupuesto });
                        }
                        else
                        {
                            Alerta("El presupuesto para ese periodo ya existe.");
                            e.Canceled = true;
                        }
                        break;
                    case "Update":
                        gi = e.Item;

                        if (((RadNumericTextBox)gi.FindControl("RadNumericTextBox1")).Text == "" ||
                            ((RadComboBox)gi.FindControl("RadComboBox1")).SelectedIndex == 0 ||
                            ((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Text == "")
                        {
                            Alerta("El año, mes y presupuesto son obligatorios");
                            e.Canceled = true;
                        }

                        anyo = ((RadNumericTextBox)gi.FindControl("RadNumericTextBox1")).Text;
                        mes = ((RadComboBox)gi.FindControl("RadComboBox1")).SelectedValue;
                        mesStr = ((RadComboBox)gi.FindControl("RadComboBox1")).Text;
                        presupuesto = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox2")).Text);
                        anyo_old = ((Label)gi.FindControl("lblold1")).Text;
                        mes_old = ((Label)gi.FindControl("lblold2")).Text;
                        presupuesto_old = Convert.ToDouble(((Label)gi.FindControl("lblold3")).Text);

                         Ar_dr = dt.Select("Anyo='" + anyo + "' and Mes='" + mes + "'");
                        if (Ar_dr.Length != 0)
                        {
                            Alerta("El presupuesto para ese periodo ya existe.");
                            e.Canceled = true;
                            return;
                        }
                        
                        Ar_dr = dt.Select("Anyo='" + anyo_old + "' and Mes='" + mes_old + "' and Presupuesto='" + presupuesto_old + "'");
                        if (Ar_dr.Length > 0)
                        {
                            Ar_dr[0].BeginEdit();
                            Ar_dr[0]["Anyo"] = anyo;
                            Ar_dr[0]["Mes"] = mes;
                            Ar_dr[0]["MesStr"] = mesStr;
                            Ar_dr[0]["Presupuesto"] = presupuesto;
                            Ar_dr[0].AcceptChanges();
                        }
                        break;
                    case "Delete":
                        gi = e.Item;
                        anyo_old = ((Label)gi.FindControl("label1")).Text;
                        mes_old = ((Label)gi.FindControl("Label4")).Text;
                        presupuesto_old = Convert.ToDouble(((Label)gi.FindControl("label3")).Text);
                        Ar_dr = dt.Select("Anyo='" + anyo_old + "' and Mes='" + mes_old + "' and Presupuesto='" + presupuesto_old + "'");
                        if (Ar_dr.Length > 0)
                        {
                            Ar_dr[0].Delete();
                            dt.AcceptChanges();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDet_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                //this.rgDet.Rebind();
                rgDet.DataSource = dt;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_PageIndexChanged");
            }
        }
        protected void RadComboBox_DataBinding(object sender, EventArgs e)
        {
            RadComboBox comboBox = ((RadComboBox)sender);
            comboBox.Items.Add(new RadComboBoxItem("-- Seleccionar --", "-1"));
            CultureInfo cultura = CultureInfo.CurrentCulture;

            for (int x = 1; x < 13; x++)
            {
                comboBox.Items.Add(new RadComboBoxItem(cultura.TextInfo.ToTitleCase(cultura.DateTimeFormat.GetMonthName(x)), x.ToString()));
            }
        }
        protected void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked && HF_ID.Value != "")
            {
                if (!Deshabilitar())
                {
                    Alerta("El registro está siendo utilizado por otro componente");
                    ((CheckBox)sender).Checked = true;
                }
            }
        }
        #endregion
        #region Funciones
        private void Inicializar()
        {
            txtClave.Text = Valor;
            rg1.Rebind();
            CargarUen();
            CargarRik();
            CargarSeg();
            GetListDet();
            rgDet.Rebind();
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
        private void CargarUen() //Central
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUen_Combo", ref cmbUen);
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
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Id_TU == 2 ? Sesion.Id_U : (int?)null, cmbUen.SelectedValue == "" ? -1 : Convert.ToInt32(cmbUen.SelectedValue), Sesion.Emp_Cnx, "spCatRik_Combo", ref cmbRik);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarSeg() //Central
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, cmbUen.SelectedValue == "" ? -1 : Convert.ToInt32(cmbUen.SelectedValue), Sesion.Emp_Cnx, "spCatSegmentos_Combo", ref cmbSegmento);
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
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;

                    if (Permiso.PGrabar == false)
                        this.rtb1.Items[6].Visible = false;
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
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
        private List<Territorios> GetList()
        {
            try
            {
                List<Territorios> List = new List<Territorios>();
                CN_CatTerritorios clsCatTerritorios = new CN_CatTerritorios();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Territorios territorio = new Territorios();
                territorio.Id_Emp = session2.Id_Emp;
                territorio.Id_Cd = session2.Id_Cd_Ver;
                clsCatTerritorios.ConsultaTerritorios(territorio, session2.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GetListDet()
        {
            dt = new DataTable();
            DataColumn dc = new DataColumn();
            dt.Columns.Add("Anyo", System.Type.GetType("System.Int32"));
            dt.Columns.Add("Mes", System.Type.GetType("System.Int32"));
            dt.Columns.Add("MesStr", System.Type.GetType("System.String"));
            dt.Columns.Add("Presupuesto", System.Type.GetType("System.Double"));
            if (HF_ID.Value != "")
            {
                CN_CatTerritorios clsCatTerritorios = new CN_CatTerritorios();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                TerritorioDet territorio = new TerritorioDet();
                territorio.Id_Emp = session2.Id_Emp;
                territorio.Id_Cd = session2.Id_Cd_Ver;
                territorio.Id_Ter = Convert.ToInt32(HF_ID.Value);
                DataTable dt2 = dt;
                clsCatTerritorios.ConsultaTerritoriosDet(territorio, session2.Emp_Cnx, ref dt2);
                dt = dt2;
            }
        }
        private void Nuevo()
        {

            txtClave.Text = Valor;
            txtClave.Enabled = true;
            txtDescripcion.Text = string.Empty;
            txtRik.Text = string.Empty;
            txtSegmento.Text = string.Empty;
            txtUen.Text = string.Empty;
            cmbRik.SelectedIndex = 0;
            cmbRik.Text = cmbRik.Items[0].Text;
            cmbSegmento.SelectedIndex = 0;
            cmbSegmento.Text = cmbSegmento.Items[0].Text;
            cmbUen.SelectedIndex = 0;
            cmbUen.Text = cmbUen.Items[0].Text;
            chkActivo.Checked = true;
            HF_ID.Value = string.Empty;
            dt.Rows.Clear();
            rgDet.Rebind();
        }
        private void Guardar()
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                Territorios territorio = new Territorios();
                territorio.Id_Emp = session.Id_Emp;
                territorio.Id_Cd = session.Id_Cd_Ver;
                territorio.Descripcion = txtDescripcion.Text;
                territorio.Id_Uen = Convert.ToInt32(cmbUen.SelectedValue);
                territorio.Id_Rik = Convert.ToInt32(cmbRik.SelectedValue);
                territorio.Id_Seg = Convert.ToInt32(cmbSegmento.SelectedValue);
                territorio.Estatus = chkActivo.Checked;
                CN_CatTerritorios clsCatSegmentos = new CN_CatTerritorios();
                int verificador = -1;
                if (HF_ID.Value == "")
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }
                    territorio.Id_Ter = Convert.ToInt32(txtClave.Text);
                    clsCatSegmentos.InsertarTerritorios(territorio, session.Emp_Cnx, ref verificador);

                    if (verificador == 1)
                    {
                        clsCatSegmentos.InsertarTerritoriosDet(territorio, dt, session.Emp_Cnx, ref verificador);
                        Nuevo();
                        Alerta("Los datos se guardaron correctamente");
                    }
                    else
                        Alerta("La clave ya existe");
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }
                    territorio.Id_Ter = Convert.ToInt32(HF_ID.Value);
                    clsCatSegmentos.ModificarTerritorios(territorio, session.Emp_Cnx, ref verificador);

                    if (verificador == 1)
                    {
                        clsCatSegmentos.ModificarTerritoriosDet(territorio, dt, session.Emp_Cnx, ref verificador);
                        Alerta("Los datos se modificaron correctamente");
                    }
                    else
                        Alerta("Ocurrió un error al intentar modificar los datos");
                }
                rg1.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool Deshabilitar()
        {
            try
            {
                bool verificador = false;
                if (HF_ID.Value != "")
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    Catalogo ct = new Catalogo();
                    ct.Id_Emp = Sesion.Id_Emp;
                    ct.Id_Cd = Sesion.Id_Cd_Ver;
                    ct.Id = Convert.ToInt32(HF_ID.Value);
                    ct.Tabla = "CatTerritorio";
                    ct.Columna = "Id_Ter";
                    CN_Comun.Deshabilitar(ct, Sesion.Emp_Cnx, ref verificador);
                }
                return verificador;
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
                if (Sesion == null)
                    return "";
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, "CatTerritorio", "Id_Ter", Sesion.Emp_Cnx, "spCatLocal_Maximo");
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

        protected void cmbUen_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                CargarRik();
                CargarSeg();
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
    }
}
