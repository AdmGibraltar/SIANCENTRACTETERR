using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using System.Collections;
using System.Text;
using System.IO;
using CapaDatos;
using System.Xml;

namespace SIANWEB
{
    public partial class CapFacturaLegal : System.Web.UI.Page
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
        private DataTable dt;
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
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        CargarCDI();
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_Cve.Value = randObj.Next().ToString();
               
                        
    
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Page_Load");
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    //case "RebindGrid":
                    //    this.rgPolizas.Rebind();
                    //    break;
                    
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        //protected void BtnBuscar_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        if (_PermisoGuardar)
        //        {
        //            if (ValidarFecha())
        //            {

        //                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
        //                CN_CatMovimientos cn_movs = new CN_CatMovimientos();


        //                int Id_Alm = 0;
        //                DateTime FechaIni;
        //                DateTime FechaFin;
        //                int Verificador =0;

        //                if (this.TxtId_Alm.Text != "")
        //                {
        //                    Id_Alm = int.Parse(TxtId_Alm.Text);
        //                }
        //                else
        //                {
        //                    Id_Alm = 0;
        //                }

        //                FechaIni = DateTime.Parse(this.txtFecha1.SelectedDate.ToString());
        //                FechaFin = DateTime.Parse(this.txtFecha2.SelectedDate.ToString());

        //                cn_movs.ObtenerMovimientosEncabezado(Id_Alm, FechaIni, FechaFin, ref Verificador, sesion);

        //                if (Verificador == -1)
        //                {
        //                    cn_movs.ObtenerMovimientosDetalle(Id_Alm, FechaIni, FechaFin, ref Verificador, sesion);

        //                    if (Verificador == -1)
        //                    {
        //                        Alerta("Se obtuvo la información de manera exitosa");
        //                    }
        //                    else
        //                    {
        //                        Alerta("Error inesperado al tratar de obtener el detalle de movimientos, por favor vuelva a intentar");
        //                    }
        //                }
        //                else
        //                {
        //                    Alerta("Error inesperado al tratar de obtener los movimientos, por favor vuelva a intentar");
        //                }


        //            }
        //            else
        //            {
        //                Alerta("La fecha inicial no puede ser mayor a la fecha final");

        //            }
        //        }
        //        else
        //        {
        //            Alerta("No tiene permisos para realizar esta accion");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
        //    }

        //}
        //protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        //{
        //    try
        //    {
        //        RadToolBarButton btn = e.Item as RadToolBarButton;
        //        switch (btn.CommandName)
        //        {
        //            case "print":
        //                Imprimir(0);
        //                break;
        //            case "Generar":
        //                Imprimir(1);
        //                break;


        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
        //    }
        //}
        protected void rg_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    //this.rgPolizas.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            try
            {
                //this.rgPolizas.Rebind();
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
         
        }
        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.TxtId_Cd.Enabled = false;
                this.TxtId_Fac.Enabled = false;
                this.CmbId_Cd.Enabled = false;
                this.TxtId_Cte.Text = string.Empty;
                this.Txt_Nombre.Text = string.Empty;
                this.TxtTotal.Text = string.Empty;
                this.TxtPagado.Text = string.Empty;
                this.TxtSaldo.Text = string.Empty;
                this.ChkLegal.Checked = false;
                this.RblTipo.Enabled = false;
                ConsultarFactura();

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
                RadToolBarButton btn = e.Item as RadToolBarButton;
                switch (btn.CommandName)
                {
                    case "Nuevo":
                        LimpiarCampos();;
                        break;
                    case "Guardar":
                        Guardar();
                        break;
                    case "Imprimir":
                        Imprimir();
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
        private void CargarCDI()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Emp_Cnx, "SpCatCdi_Combo", ref CmbId_Cd);
               // CN_Comun.LlenaCombo(Sesion.Emp_Cnx, "spCatAlmacen_Combo", ref this.CmbId_Alm);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void LimpiarCampos()
        {
            try
            {
                this.TxtId_Fac.Enabled = true;
                this.TxtId_Cd.Enabled = true;
                this.CmbId_Cd.Enabled = true;
                this.RblTipo.Enabled = true;
                
                this.TxtId_Fac.Text = string.Empty;
                this.TxtId_Cd.Text = string.Empty;
                this.CmbId_Cd.Text = " -- Seleccionar -- ";
                this.CmbId_Cd.SelectedValue  = "-1";
                this.TxtId_Cte.Text = string.Empty;
                this.Txt_Nombre.Text = string.Empty;
                this.TxtTotal.Text = string.Empty;
                this.TxtPagado.Text = string.Empty;
                this.TxtSaldo.Text = string.Empty;
                this.ChkLegal.Checked = false;
                this.TxtFacL_Comentarios.Text = string.Empty;
                this.RblTipo.SelectedValue = "1";


            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void ConsultarFactura()
        {
            try
            {
                if (this.TxtId_Cd.Text == string.Empty)
                {
                    Alerta("Seleccione un centro de distribución");
                    return;
                }

                if (this.TxtId_Fac.Text == string.Empty)
                {
                    Alerta("Ingrese el número de factura");
                    return;
                }

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapFacturaLegal cn_fac = new CN_CapFacturaLegal();
                CapaEntidad.CapFacturaLegal fac = new CapaEntidad.CapFacturaLegal();
                int Verificador = 0;

                cn_fac.CapFacturaLegal_Consulta(ref fac, ref Verificador, int.Parse(this.TxtId_Cd.Text), int.Parse(this.TxtId_Fac.Text),int.Parse(this.RblTipo.SelectedValue), sesion.Emp_Cnx);

                if (Verificador == 1)
                {
                    this.TxtId_Cte.Text = fac.Id_Cte.ToString();
                    this.Txt_Nombre.Text = fac.Cte_Nombre;
                    this.TxtTotal.Text = fac.Total.ToString("C2");
                    this.TxtPagado.Text = fac.Pagado.ToString("C2");
                    this.TxtSaldo.Text = fac.Saldo.ToString("C2");
                    if (fac.FacL_Legal == 1)
                    {
                        this.ChkLegal.Checked = true;
                    }
                    else
                    {
                        this.ChkLegal.Checked = false;
                    }
                    this.TxtFacL_Comentarios.Text = fac.FacL_Comentarios;
                }
                else
                {
                    Alerta("No se enconto la factura, por favor verifique la información");
                    this.TxtId_Cd.Enabled = true;
                    this.TxtId_Fac.Enabled = true;
                    this.CmbId_Cd.Enabled = true;
                    this.RblTipo.Enabled = true;
                }

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
                if (this.TxtId_Cte.Text == string.Empty)
                {
                    Alerta("Debe seleccionar una factura");
                    return;
                }

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaEntidad.CapFacturaLegal fac = new CapaEntidad.CapFacturaLegal();
                CN_CapFacturaLegal cn_fac = new CN_CapFacturaLegal();
                int Verificador = 0;

                LlenarObjeto( ref fac);


                cn_fac.CapFacturaLegal_Guardar (ref fac, ref Verificador , sesion.Emp_Cnx);

                if (Verificador == -1)
                {
                    LimpiarCampos();
                    Alerta("Los datos se guardaron correctamente");
                    
                }
                else 
                {
                    Alerta("Error al intentar guardar la infromación");
                }


            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void LlenarObjeto(ref CapaEntidad.CapFacturaLegal fac)
        {
            try
            {
                fac.Id_Cd = int.Parse(this.TxtId_Cd.Text);
                fac.Id_Fac = int.Parse(this.TxtId_Fac.Text);
                fac.Id_Cte  = int.Parse(this.TxtId_Cte.Text);
                if (this.ChkLegal.Checked == true) { fac.FacL_Legal = 1;} else {fac.FacL_Legal = 0;  }
                fac.FacL_Comentarios = this.TxtFacL_Comentarios.Text;
                fac.Fac_Tipo = int.Parse(this.RblTipo.SelectedValue);

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


                Type instance = null;

                instance = typeof(LibreriaReportes.RepFacturaLegal);


                Session["InternParameter_Values" + Session.SessionID + HF_Cve.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_Cve.Value] = instance.AssemblyQualifiedName;
                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_Cve.Value + "');");


            }
            catch (Exception ex)
            {
                
                throw ex;
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

   

    }
}