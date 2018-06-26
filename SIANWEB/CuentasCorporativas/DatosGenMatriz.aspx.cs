using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CapaDatos;
using CapaNegocios;
using CapaEntidad;
using SIANWEB.Utilerias;

namespace SIANWEB.CuentasCorporativas
{
    public partial class DatosGenMatriz : System.Web.UI.Page
    {

        SIANCENTRAL_CCEntities1 model = new SIANCENTRAL_CCEntities1();

        private List<CatACYS_DirFiscales> listDirFiscales
        {
            get
            {
                return (List<CatACYS_DirFiscales>)Session["listDirFiscales"];
            }
            set
            {
                Session["listDirFiscales"] = value;
            }
        }

        private List<CatCNac_IntranetListaFranq> listFranquicias
        {
            get
            {
                return (List<CatCNac_IntranetListaFranq>)Session["listFranquicias"];
            }
            set
            {
                Session["listFranquicias"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            var permisos = new PermisosSesion(this.Page);
            permisos.ValidarSesion();

            if (!Page.IsPostBack)
            {

               // permisos.ValidarPermisos(this.rtb1);

                int id_ClienteMat = Int32.Parse(Request.QueryString["Id"]);

                CN_CatClienteMatriz cm_Matriz = new CN_CatClienteMatriz(model);
                Session["cm_Matriz"] = cm_Matriz;
                Session["model_Matriz"] = model;

                Session["Nuevo"] = false;


                cmbSoportes.DataSource = cm_Matriz.ComboSoportes();
                cmbSoportes.DataBind();

                CatCNac_Matriz matriz = cm_Matriz.ConsultarMatriz(id_ClienteMat);
           

                Session.Add("matrizOr", matriz);

                if (matriz.CatACYS_SIANCENTRAL != null)
                {
                    object objMatriz_SIANCENTRAL = matriz.CatACYS_SIANCENTRAL;
                    AsignacionCampos.AsignaCamposForma(ref objMatriz_SIANCENTRAL, "", this);

                    object objMatriz_IntranetFranq = matriz.CatCNac_IntranetFran;
                    AsignacionCampos.AsignaCamposForma(ref objMatriz_IntranetFranq, "", this);
                }
                else
                    Session["Nuevo"] = true;

                listDirFiscales = cm_Matriz.ConsutarDirFiscales(id_ClienteMat);
                //listProductos = cm_Matriz.ConsultarProductos(0);

                string[] soportes={""};
                if (matriz.CatCNac_IntranetFran!=null && matriz.CatCNac_IntranetFran.Soportes != null)
                        soportes = matriz.CatCNac_IntranetFran.Soportes.Split(',');

                foreach (string sop in soportes)
                {
                    if (sop.Trim() != "") 
                    cmbSoportes.Items.FindItemByValue(sop.Trim()).Checked=true;
                }

                cmbTipoMoneda.DataSource = cm_Matriz.ComboMoneda();
                cmbTipoMoneda.DataBind();

                cmbAddendaTipo.DataSource = cm_Matriz.ComboAddenda();
                cmbAddendaTipo.DataBind();

                cmbCatEspecial.DataSource = cm_Matriz.ComboCatEspecial();
                cmbCatEspecial.DataBind();

                cmbProdPermitidos.DataSource = cm_Matriz.ComboProdPermitidos();
                cmbProdPermitidos.DataBind();

                cmbTipoNotaCred.DataSource = cm_Matriz.ComboTipoNotaCred();
                cmbTipoNotaCred.DataBind();

                cmbMetodoPago.DataSource = cm_Matriz.ComboMetPago();
                cmbMetodoPago.DataBind();



                csbStrCondicionesClienteMac.DataSource = cm_Matriz.ConsultaCondiciones();
                csbStrCondicionesClienteMac.DataBind();

                //cmbStrTipoClienteMac.DataSource = cm_Matriz.ConsultaTipos();
                //cmbStrTipoClienteMac.DataBind();

                listFranquicias = cm_Matriz.ListaFranquicias(id_ClienteMat);

                this.cmbConvenioId.DataSource = this.ListNoUtilizados();
                this.cmbConvenioId.DataBind();

                
            }
        }

        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {

            RadToolBarButton btn = e.Item as RadToolBarButton;
            CN_CatClienteMatriz cm_Matriz = (CN_CatClienteMatriz)Session["cm_Matriz"];

            if (btn.CommandName == "save")
            {

                int idMatriz = Int32.Parse(Request.QueryString["Id"]);
                CatCNac_Matriz matriz = new CatCNac_Matriz();
                matriz.CatACYS_SIANCENTRAL = new CatACYS_SIANCENTRAL();
                matriz.CatCNac_IntranetFran= new CatCNac_IntranetFran();

                matriz.Id = idMatriz;
                matriz.CatACYS_SIANCENTRAL.Id = idMatriz;
                matriz.CatCNac_IntranetFran.Id= idMatriz;


                //LLena campos a partir del formulario

                object objMatriz_SIANCENTRAL = matriz.CatACYS_SIANCENTRAL;
                AsignacionCampos.AsignaCamposEntidad(ref objMatriz_SIANCENTRAL, "", this);

                object objMatriz_IntranetFran = matriz.CatCNac_IntranetFran;
                AsignacionCampos.AsignaCamposEntidad(ref objMatriz_IntranetFran, "", this);

                foreach (RadComboBoxItem item in cmbSoportes.CheckedItems)
                {
                     matriz.CatCNac_IntranetFran.Soportes+=item.Value.ToString() + ", ";
                }

           
                foreach (CatACYS_DirFiscales dir in listDirFiscales) matriz.CatACYS_DirFiscales.Add(dir);
                foreach (CatCNac_IntranetListaFranq fran in listFranquicias) matriz.CatCNac_IntranetListaFranq.Add(fran);


                

                matriz.CatACYS_SIANCENTRAL.ConvenioNombre = this.cmbConvenioId.Text;

                cm_Matriz.GuardarDatosGeneralesMat(matriz, (Boolean)Session["Nuevo"]);
                RAM1.ResponseScripts.Add("CloseAlert('Los datos se guardaron correctamente');");

            }

        }

        protected void rgDirFiscales_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {

                }
                this.rgDirFiscales.DataSource = listDirFiscales;
            }
            catch (Exception ex)
            {
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgDirFiscales_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgDirFiscales.EditItems.Count > 0)
                        {
                            //Alerta("Ya está editando un registro.");
                            //e.Canceled = true;
                        }
                        break;
                    case "PerformInsert":
                        PerformInsertDirFiscal(e);
                        break;
                    case "Update":
                        UpdateDirFiscal(e);
                        break;
                    case "Delete":
                        DeleteDirFiscal(e);
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }


        protected void rgFranquicias_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                this.rgFranquicias.DataSource = listFranquicias;
            }
            catch (Exception ex)
            {
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgFranquicias_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgFranquicias.EditItems.Count > 0)
                        {
                            //Alerta("Ya está editando un registro.");
                            //e.Canceled = true;
                        }
                        break;
                    case "PerformInsert":
                       this.PerformInsertFranqucias(e);
                        break;
                    case "Update":
                        UpdateFranqucias(e);
                        break;
                    case "Delete":
                        DeleteFranquicias(e);
                        break;
                }
            }
            catch (Exception ex)
            {
            }
        }


        protected void rgFranquicias_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item.IsInEditMode==true)
            {
                GridEditableItem insertItem = (GridEditableItem)e.Item;
                RadComboBox combo = (RadComboBox)insertItem["UsuarioIntranet"].FindControl("cmbUsuarioIntranet");

                CN_CatCNac_Matriz cm_Matriz = new CN_CatCNac_Matriz(model);
                int idMatriz = Int32.Parse(Request.QueryString["Id"]);

                combo.DataSource = cm_Matriz.ComboIntranetUsuarios();
                combo.DataBind();

                RadComboBox comboBox = (RadComboBox)insertItem["UsuarioIntranet"].FindControl("cmbMoneda");
                CN_CatClienteMatriz cm_ClienteMatriz = (CN_CatClienteMatriz)Session["cm_Matriz"];
               
                comboBox.DataSource = cm_ClienteMatriz.ComboMoneda();
                comboBox.DataBind();

                RadComboBox comboProductos = (RadComboBox)insertItem["UsuarioIntranet"].FindControl("cmbProductos");
                comboProductos.DataSource = cm_ClienteMatriz.ComboProdPermitidos();
                comboProductos.DataBind();

            }
        }


        protected void cmbUsuarioIntranet_DataBound(object sender, EventArgs e)
        {

            RadComboBox comboBox = ((RadComboBox)sender);
            string id = ((Label)comboBox.Parent.FindControl("lblUsuarioIntranetId")).Text;
            if (id != "")
                comboBox.SelectedIndex = comboBox.FindItemIndexByValue(id);
        }



        private void PerformInsertDirFiscal(GridCommandEventArgs e)
        {
            int idMatriz = Int32.Parse(Request.QueryString["Id"]);
            GridItem gi = e.Item;
            List<CatACYS_DirFiscales> dirFiscalesIns = this.listDirFiscales;
            var dirFiscal = new CatACYS_DirFiscales();
            dirFiscal.Id_ClienteMatriz = idMatriz;

            object objMatriz = dirFiscal;
            AsignacionCampos.AsignaCamposEntidad(ref objMatriz, "", gi, this);

            dirFiscalesIns.Add(dirFiscal);

        }


        private void PerformInsertFranqucias(GridCommandEventArgs e)
        {
            int idMatriz = Int32.Parse(Request.QueryString["Id"]);
            GridItem gi = e.Item;
            List<CatCNac_IntranetListaFranq> FranqIntranet = this.listFranquicias;
            var Franq = new CatCNac_IntranetListaFranq();
            Franq.Id_Matriz = idMatriz;

            object objMatriz = Franq;
            AsignacionCampos.AsignaCamposEntidad(ref objMatriz, "", gi, this);

            SIANCENTRAL_CCEntities1 model_Matriz = (SIANCENTRAL_CCEntities1)Session["model_Matriz"];
            Franq.CatCNac_IntranetUsuarios = model_Matriz.CatCNac_IntranetUsuarios.Where(x => x.Usu_IdUsuario == Franq.UsuarioIntranet).FirstOrDefault();
            Franq.CatCNac_ProductosPermitidos = model_Matriz.CatCNac_ProductosPermitidos.Where(x => x.Id == Franq.Productos).FirstOrDefault();
            Franq.CatTMoneda = model_Matriz.CatTMoneda.Where(x => x.Id_Mon == Franq.Moneda).FirstOrDefault();
            Franq.id_Emp = 1;

            FranqIntranet.Add(Franq);
        }



        private void UpdateFranqucias(GridCommandEventArgs e)
        {
            int idMatriz = Int32.Parse(Request.QueryString["Id"]);

            GridItem gi = e.Item;
            int idFran = Int32.Parse(((Telerik.Web.UI.GridDataItem)(e.Item)).GetDataKeyValue("Id").ToString());

            List<CatCNac_IntranetListaFranq> FranIns = this.listFranquicias;
            CatCNac_IntranetListaFranq Fran = FranIns.Where(x => x.Id == idFran).FirstOrDefault();

            Fran.Id_Matriz = idMatriz;

            object objMatriz = Fran;
            AsignacionCampos.AsignaCamposEntidad(ref objMatriz, "", gi, this);

            SIANCENTRAL_CCEntities1 model_Matriz = (SIANCENTRAL_CCEntities1)Session["model_Matriz"];

            Fran.CatCNac_IntranetUsuarios = model_Matriz.CatCNac_IntranetUsuarios.Where(x => x.Usu_IdUsuario == Fran.UsuarioIntranet).FirstOrDefault();
            Fran.CatCNac_ProductosPermitidos = model_Matriz.CatCNac_ProductosPermitidos.Where(x => x.Id == Fran.Productos).FirstOrDefault();
            Fran.CatTMoneda = model_Matriz.CatTMoneda.Where(x => x.Id_Mon == Fran.Moneda).FirstOrDefault();
        }

        
        private void UpdateDirFiscal(GridCommandEventArgs e)
        {
            int idMatriz = Int32.Parse(Request.QueryString["Id"]);

            GridItem gi = e.Item;
            int idDirFiscal = Int32.Parse(((Telerik.Web.UI.GridDataItem)(e.Item)).GetDataKeyValue("Id").ToString());

            List<CatACYS_DirFiscales> dirFiscalesIns = this.listDirFiscales;
            var dirFiscal = dirFiscalesIns.Where(x => x.Id == idDirFiscal).FirstOrDefault();

            dirFiscal.Id_ClienteMatriz = idMatriz;

            object objMatriz = dirFiscal;
            AsignacionCampos.AsignaCamposEntidad(ref objMatriz, "", gi, this);


        }

        private void DeleteDirFiscal(GridCommandEventArgs e)
        {
            GridItem gi = e.Item;
            int idDirFiscal = Int32.Parse(((Telerik.Web.UI.GridDataItem)(e.Item)).GetDataKeyValue("Id").ToString());

            List<CatACYS_DirFiscales> dirFiscalesIns = this.listDirFiscales;
            var dirFiscal = dirFiscalesIns.Where(x => x.Id == idDirFiscal).FirstOrDefault();
            dirFiscalesIns.Remove(dirFiscal);
        }


        private void DeleteFranquicias(GridCommandEventArgs e)
        {
            GridItem gi = e.Item;
            int idFran = Int32.Parse(((Telerik.Web.UI.GridDataItem)(e.Item)).GetDataKeyValue("Id").ToString());

            List<CatCNac_IntranetListaFranq> franquicias = this.listFranquicias;
            var fran = listFranquicias.Where(x => x.Id == idFran).FirstOrDefault();
            listFranquicias.Remove(fran);
        }

       




        protected void cmbMoneda_DataBound(object sender, EventArgs e)
        {
            RadComboBox comboBox = ((RadComboBox)sender);
            string id = ((Label)comboBox.Parent.FindControl("lblMoneda")).Text;
            if (id != "")
                comboBox.SelectedIndex = comboBox.FindItemIndexByValue(id);

        }


        protected void cmbProductos_DataBound(object sender, EventArgs e)
        {
            RadComboBox comboBox = ((RadComboBox)sender);
            string id = ((Label)comboBox.Parent.FindControl("lblProductos")).Text;
            if (id != "")
                comboBox.SelectedIndex = comboBox.FindItemIndexByValue(id);


        }

        protected void cmbSistemaPV_DataBinding(object sender, EventArgs e)
        {


        }



        private List<Convenio> ListNoUtilizados()
        {
            try
            {
                Convenio conv = new Convenio();
                CN_Convenio cn_conv = new CN_Convenio();
                List<Convenio> ListUtil = new List<Convenio>();
                List<Convenio> ListNoUtil = new List<Convenio>();
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                conv.Filtro_TipoFiltro = -1;
                conv.Filtro_Vencido = -1;
                conv.Filtro_Id_Cat = -1;
                conv.Filtro_Valor = "";
                conv.Filtro_Id_Cd = 100;

                cn_conv.ConsultaListaConvenios(conv, ref ListUtil, ref ListNoUtil, Conexion);

                return ListNoUtil;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }




    }
}