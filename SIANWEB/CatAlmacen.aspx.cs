using System;
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
    public partial class CatAlmacen : System.Web.UI.Page
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

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        } 
        #region Eventos
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
                    case "print":
                        Imprimir();
                        break;
                    case "details":
                        VerDetalles();
                        break;

                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadToolBar1_ButtonClick");
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
                
                   ErrorManager(ex, "imgBuscar_Click");
            }

        }
        protected void GrdAlmacen_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    this.GrdAlmacen.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "GrdApContable_NeedDataSource");
            }
        }
        protected void GrdAlmacen_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            try
            {

                this.GrdAlmacen.Rebind();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }

        #endregion
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

                CD_PermisosU CN_PermisosU = new CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;
                    if (Permiso.PGrabar == false)
                        this.RadToolBar1.Items[3].Visible = false;
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                        this.RadToolBar1.Items[3].Visible = false;
                    if (Permiso.PImprimir == false)
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
                this.TxtAlm_Clave.Text = string.Empty;
                this.TxtAlm_Cuenta.Text = string.Empty;
                this.TxtAlm_Nombre.Text = string.Empty;
                this.TxtAlm_SubCuenta.Text = string.Empty;
                this.TxtAlm_CtaCenCosto.Text = string.Empty;
                this.TxtAlm_SubCtaCenCosto.Text = string.Empty;
                this.HdId_Alm.Value = "";
                this.TblDetalles.Visible = false;
                this.ChkAlm_Activo.Checked = true;
                this.TxtAlm_Clave.Enabled = true;

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
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatAlmacen cn_catalmacen = new CN_CatAlmacen();
                CapaEntidad.CatAlmacen alm = new CapaEntidad.CatAlmacen();
                int Verificador = 0;

                LlenarObjetoAlmacen(ref alm);

                if (this.HdId_Alm.Value == "")
                {
                    cn_catalmacen.CatAlmacen_Insertar(alm, sesion, ref Verificador);

                    if (Verificador == -1)
                    {
                        Alerta("Los datos se guardaron correctamente");
                        Nuevo();
                    }
                    else if (Verificador == -2)
                    {
                        Alerta("Ya existe un almacén con la misma clave");
                    }
                    else
                    {
                        Alerta("Error inesperado al guardar almacén");
                    }


                }
                else
                {
                    cn_catalmacen.CatAlmacen_Modificar(alm, sesion, ref Verificador);


                    if (Verificador == -1)
                    {
                        Alerta("Los datos se modificaron correctamente");
                        Nuevo();
                    }
                    else
                    {
                        Alerta("Error inesperado al guardar almacén");
                    }

                }

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void Buscar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatAlmacen cn_catalmacen = new CN_CatAlmacen();
                CapaEntidad.CatAlmacen alm = new CapaEntidad.CatAlmacen();
                if (this.TxtAlm_Clave.Text == "")
                {
                    this.RfvTxtAlm_Clave.IsValid = false;
                }
                else
                {
                    int Alm_Clave = int.Parse (this.TxtAlm_Clave.Text);

                    cn_catalmacen.CatAlmacen_Consulta(ref alm, Alm_Clave, sesion);

                    if (alm.Id_Alm != 0)
                    {

                        this.HdId_Alm.Value = alm.Id_Alm.ToString();
                        this.TxtAlm_Nombre.Text = alm.Alm_Nombre;
                        this.TxtAlm_Cuenta.Text = alm.Alm_CuentaStr.Trim();
                        this.TxtAlm_SubCuenta.Text = alm.Alm_SubcuentaStr.Trim();
                        this.TxtAlm_CtaCenCosto.Text = alm.Alm_CtaCenCosto.Trim();
                        this.TxtAlm_SubCtaCenCosto.Text = alm.Alm_SubCtaCenCosto.Trim();
                        this.ChkAlm_Activo.Checked = alm.Alm_Activo;
                        this.TxtAlm_Clave.Enabled = false;
                    }
                    else
                    {
                        Alerta("No existe ningun almacén con la clave proporcionada");
                    }

                    

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void LlenarObjetoAlmacen(ref CapaEntidad.CatAlmacen alm)
        {
            try
            {
                if (this.HdId_Alm.Value != "")
                {
                alm.Id_Alm = int.Parse (this.HdId_Alm.Value);
                }
                else
                {
                alm.Id_Alm = 0;
                }
                alm.Alm_Clave = int.Parse(this.TxtAlm_Clave.Text);
                alm.Alm_Nombre = this.TxtAlm_Nombre.Text;
                alm.Alm_CuentaStr = this.TxtAlm_Cuenta.Text.Trim();
                alm.Alm_SubcuentaStr = this.TxtAlm_SubCuenta.Text.Trim();
                alm.Alm_CtaCenCosto = this.TxtAlm_CtaCenCosto.Text.Trim();
                alm.Alm_SubCtaCenCosto = this.TxtAlm_SubCtaCenCosto.Text.Trim();
                alm.Alm_Activo = this.ChkAlm_Activo.Checked;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private List<CapaEntidad.CatAlmacen> GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatAlmacen cn_catalmacen = new CN_CatAlmacen();
                List<CapaEntidad.CatAlmacen> list = new List<CapaEntidad.CatAlmacen>();

                cn_catalmacen.CatAlmacen_ConsultaLista(ref list, sesion);

                return list;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void VerDetalles()
        {
            try
            {
                this.GrdAlmacen.Rebind();
                this.TblDetalles.Visible = true;

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
                ALValorParametrosInternos.Add(sesion.U_Nombre);

                Type instance = null;
       
                 instance = typeof(LibreriaReportes.RepAlmacenes);
 


                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;
                RadAjaxManager1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        //private void Imprimir()
        //{
        //    try
        //    {

        //        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
        //        ArrayList ALValorParametrosInternos = new ArrayList();

        //        ALValorParametrosInternos.Add(sesion.U_Nombre);
        //        ALValorParametrosInternos.Add(sesion.Emp_Cnx );

        //        Type instance = null;
        //        instance = typeof(LibreriaReportes.RepCatAlmacen);

        //        Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = null;
        //        Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
        //        Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;
        //        RadAjaxManager1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");



        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

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