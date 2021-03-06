﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using CapaDatos;
using System.Text;

namespace SIANWEB
{
    public partial class ProCierre : System.Web.UI.Page
    {
        #region Varibles
        private Sesion sesion
        {
            get { return (Sesion)Session["Sesion" + Session.SessionID]; }
            set { Session["Sesion" + Session.SessionID] = value; }
        }
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {

                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        CargarCentros();
                        Inicializar();
                    }
                }

                this.TblEncabezado.Visible = false;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int verificador = -1;
                CN_ProCierreMes cn_procierremes = new CN_ProCierreMes();
                cn_procierremes.Cierre(sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Emp_Cnx, ref verificador);
                if (verificador == 3)
                {
                    Alerta("No se ha dado de alta el periodo siguiente");
                    return;
                }
                else
                    if (verificador == 1)
                    {
                        Alerta("Cierre de mes ejecutado");
                        CN_CatCalendario cn_catcalendario = new CN_CatCalendario();
                        Calendario calendario = new Calendario();
                        cn_catcalendario.ConsultaCalendarioActual(ref calendario, sesion);
                        dpFecha1.SelectedDate = calendario.Cal_FechaIni;
                        dpFecha2.SelectedDate = calendario.Cal_FechaFin;

                        sesion.CalendarioIni = calendario.Cal_FechaIni;
                        sesion.CalendarioFin = calendario.Cal_FechaFin;
                    }
                    else
                    {
                        Alerta("Ocurrio un error durante el cierre de mes");
                    }
            }
            catch (Exception ex)
            {
                throw ex;
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
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun(); comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                dpFecha1.SelectedDate = sesion.CalendarioIni;
                dpFecha2.SelectedDate = sesion.CalendarioFin;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion
        #region Funciones
        private void Inicializar()
        {
            try
            {
                dpFecha1.SelectedDate = sesion.CalendarioIni;
                dpFecha2.SelectedDate = sesion.CalendarioFin;
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

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();


                if (sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, sesion.Id_Emp, sesion.Id_U, sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(sesion.Id_Cd_Ver.ToString()).Text;

                }
                else
                {
                    CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = sesion.Id_Cd_Ver.ToString();
                }
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
                CapaNegocio.PaginaConsultar(ref pagina, sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = sesion.Id_U;
                Permiso.Id_Cd = sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CD_PermisosU CN_PermisosU = new CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    //    _PermisoGuardar = Permiso.PGrabar;
                    //    _PermisoModificar = Permiso.PModificar;
                    //    _PermisoEliminar = Permiso.PEliminar;
                    //    _PermisoImprimir = Permiso.PImprimir;

                    //    if (Sesion.U_MultiOfi)
                    //    {
                    //        this.RadToolBar1.Items[6].Enabled = _PermisoGuardar; //new
                    //        if (_PermisoGuardar || _PermisoModificar)
                    //        {
                    //            this.RadToolBar1.Items[5].Enabled = true; //save
                    //        }
                    //        this.RadToolBar1.Items[4].Visible = false; //Regresar
                    //        this.RadToolBar1.Items[3].Enabled = _PermisoEliminar; //Eliminar
                    //        this.RadToolBar1.Items[2].Visible = false; //Imprimir
                    //        this.RadToolBar1.Items[1].Visible = false; //Correo
                    //    }
                    //    else //usuario No multi-CentroDistribucion
                    //    {
                    //        this.RadToolBar1.Items[6].Visible = false; //new
                    //        if (_PermisoGuardar || _PermisoModificar)
                    //        {
                    //            this.RadToolBar1.Items[5].Enabled = true; //save
                    //        }
                    //        this.RadToolBar1.Items[4].Visible = false; //Regresar
                    //        this.RadToolBar1.Items[3].Enabled = _PermisoEliminar; //Eliminar
                    //        this.RadToolBar1.Items[2].Visible = false; //Imprimir
                    //        this.RadToolBar1.Items[1].Visible = false; //Correo
                    //    }
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
                //CN_Ctrl ctrl = new CN_Ctrl();
                //ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                //ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
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