﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using System.Globalization;
using CapaDatos;
using Telerik.Reporting.Processing;

namespace SIANWEB
{
    public partial class Rep_VenVentas : System.Web.UI.Page
    {
        #region Variables
        public static bool _PermisoImprimir;
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
        #endregion Variables

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
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        this.ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        this.CargarCentros();
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                        Funciones f = new Funciones();
                        CN_CatSemana cn_semana = new CN_CatSemana();
                        Semana semana = new Semana();
                        semana.Id_Emp = sesion.Id_Emp;
                        semana.Id_Cd = sesion.Id_Cd_Ver;
                        semana.Sem_FechaAct = f.GetLocalDateTime(Sesion.Minutos);
                        int verificador = 0;
                        cn_semana.ConsultaSemanaActual(ref semana, Sesion.Emp_Cnx, ref verificador);

                        rdFechaIni.DbSelectedDate = semana.Sem_FechaIni;
                        rdFechaFin.DbSelectedDate = semana.Sem_FechaFin;
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
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
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                if (!_PermisoImprimir)
                {
                    this.Alerta("No tiene permisos para ver el reporte");
                    return;
                }
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (Page.IsValid)
                {
                    if (btn.CommandName == "print")
                    {
                        Imprimir(true);
                    }
                    else
                    {
                        Imprimir(false);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion Eventos

        #region Metodos
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
                    _PermisoImprimir = Permiso.PImprimir;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void Imprimir(bool a_pantalla)
        {
            try
            {
                string paramRel = "";
                int error = 0;
                if (!string.IsNullOrEmpty(this.txtRelacion.Text))
                {
                    boton(this.txtRelacion.Text, ref error);
                    paramRel = this.txtRelacion.Text;
                }

                if (error == 0)
                {
                    ArrayList ALValorParametrosInternos = new ArrayList();

                    ALValorParametrosInternos.Add(this.sesion.Emp_Nombre); // NOMBRE DE LA EMPRESA
                    ALValorParametrosInternos.Add(this.sesion.Cd_Nombre); // NOMBRE DEL CENTRO
                    ALValorParametrosInternos.Add(this.sesion.U_Nombre); // NOMBRE DEL USUARIO

                    /*   PARAMETROS QUE LLENAN EL ENCABEZADO   */
                    ALValorParametrosInternos.Add("De " + rdFechaIni.SelectedDate.Value.ToShortDateString() + " a " + rdFechaFin.SelectedDate.Value.ToShortDateString());
                    ALValorParametrosInternos.Add(this.txtRelacion.Text == "" ? "Todos" : this.txtRelacion.Text); // OPCION DE SELECCION POR RELACIONES

                    /*   PARAMETROS OBLIGATORIOS PARA EL REPORTE   */
                    ALValorParametrosInternos.Add(this.sesion.Id_Emp); // ID DE LA EMPRESA
                    ALValorParametrosInternos.Add(this.sesion.Id_Cd_Ver); //ID DEL CENTRO DE DISTRIBUCION
                    ALValorParametrosInternos.Add(this.txtRelacion.Text == "" ? "" : this.txtRelacion.Text); // OPCION DE SELECCION POR RELACIONES

                    ALValorParametrosInternos.Add(rdFechaIni.SelectedDate.Value);
                    ALValorParametrosInternos.Add(rdFechaFin.SelectedDate.Value);
                    ALValorParametrosInternos.Add(sesion.Id_U);
                    ALValorParametrosInternos.Add(sesion.Emp_Cnx); // CADENA DE CONEXION A LA BASE DE DATOS

                    Type instance = null;
                    if (RadioButton2.Checked)
                    {
                        if (a_pantalla)
                        {
                            instance = typeof(LibreriaReportes.Rep_VenVentasU);
                        }
                        else
                        {
                            instance = typeof(LibreriaReportes.ExpRep_VenVentasU);
                        }
                    }
                    else
                    {
                        if (a_pantalla)
                        {
                            instance = typeof(LibreriaReportes.Rep_VenVentas);
                        }
                        else
                        {
                            instance = typeof(LibreriaReportes.ExpRep_VenVentas);
                        }
                    }

                    if (a_pantalla)
                    {
                        Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                        Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;
                        RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
                    }
                    else
                    {
                        ImprimirXLS(ALValorParametrosInternos, instance);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ImprimirXLS(ArrayList ALValorParametrosInternos, Type instance)
        {
            try
            {
                Telerik.Reporting.Report report1 = (Telerik.Reporting.Report)Activator.CreateInstance(instance);
                for (int i = 0; i <= ALValorParametrosInternos.Count - 1; i++)
                {
                    report1.ReportParameters[i].Value = ALValorParametrosInternos[i];
                }
                ReportProcessor reportProcessor = new ReportProcessor();
                RenderingResult result = reportProcessor.RenderReport("XLS", report1, null);
                string ruta = Server.MapPath("Reportes") + "\\" + instance.Name + ".xls";
                if (File.Exists(ruta))
                    File.Delete(ruta);
                FileStream fs = new FileStream(ruta, FileMode.Create);
                fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                 
                fs.Flush();
                fs.Close();


                RAM1.ResponseScripts.Add("startDownload('" + instance.Name + ".xls');");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Metodo para separar una cadena dada y generar otra de ids
        /// <summary>
        /// Funcion que separa una cadena dada para determinar los parametros de busqueda por
        /// ID de la tabla
        /// </summary>
        /// <param name="cadena">Cadena de caracteres que recibe para generar la cadena de parametros</param>
        /// <returns>Regresa la cadena de parametros en tipo string</returns>
        private void boton(string cadena, ref int error)
        {
            if (!string.IsNullOrEmpty(cadena))
            {
                string[] split = cadena.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string[] split2;
                foreach (string a in split)
                {
                    if (a.Contains("-"))
                    {
                        split2 = a.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                        if (split2.Length != 2)
                        {
                            Alerta("El rango " + a.ToString() + " no es válido");
                            error = 1;
                        }
                        if (split2.Length == 2)
                            if (Convert.ToInt32(split2[0]) > Convert.ToInt32(split2[1]))
                            {
                                Alerta("El rango " + a.ToString() + " no es válido");
                                error = 1;
                            }
                    }
                }
            }
        }
        #endregion Metodo para separar una cadena dada y generar otra de ids

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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Metodos

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

        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("Impresion_error"))
                    Alerta("Error al momento de imprimir");
                else
                    Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }
        #endregion ErrorManager
    }
}