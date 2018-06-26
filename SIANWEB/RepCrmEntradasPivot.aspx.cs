using System;
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
using Telerik.Reporting.Processing;

namespace SIANWEB
{
    public partial class RepCrmEntradasPivot : System.Web.UI.Page
    {
        #region Variables
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
        #endregion Variables
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
                        this.ValidarPermisos();
                       
                        

                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        this.CargarCentros();

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_Cve.Value = randObj.Next().ToString();
                        CargarAnoMes();
                        this.TblEncabezado.Visible = false;

                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }
        #region Eventos
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);

           
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        //this.rgFacturaRuta.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                string Evento = Request.Form["__EVENTTARGET"].ToString();

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
                        Imprimir();
                    }

                }
            }
            catch (Exception ex)
            {
                Alerta("Imposible generar el reporte; aún no se han generado los respaldos del mes y año seleccionados");
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
            }
            CN__Comun comun = new CN__Comun();
            comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
            //nuevo();
        }
        #endregion Eventos
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
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                    _PermisoImprimir = Permiso.PImprimir;
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarAnoMes()
        {
            try
            {

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun cn_comun = new CN__Comun();
                cn_comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd, sesion.Emp_Cnx, "spCatCalendarioAnhio2_Combo", ref this.cmbanio);
                cn_comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd, sesion.CalendarioIni.Year, sesion.Emp_Cnx, "spCatCalendarioMes_Combo", ref this.cmbmes );

                this.cmbanio.SelectedValue = DateTime.Now.Year.ToString();
                this.cmbmes.SelectedValue = DateTime.Now.Month.ToString();

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

                StringBuilder tabla = new StringBuilder();
                tabla.Append("<html><head><meta http-equiv='Content-Type' content='text/html; charset=ISO-8859-1'></head><body><table style='width:700px'>");
                EscribeEncabezado(ref tabla);
                EscribeDetalle(ref tabla);
                tabla.Append("</table></body></html>");
                CN__Comun cn_comun = new CN__Comun();
                cn_comun.ExportarExcel("Reporte_EntradasCRM", tabla.ToString());

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void EscribeEncabezado(ref StringBuilder Tabla)
        {
            try
            {
                string TipoCd = this.RblTipoCd.SelectedValue == "1" ? "CDIs" : "CDCs";

                Sesion sesion = (Sesion) Session["Sesion" + Session.SessionID];
                Tabla.Append("<tr>");
                Tabla.Append("<td width='20px'><img src='http://" + HttpContext.Current.Request.Url.Authority + "/Imagenes/Logo.png'></td>");
                Tabla.Append("<td colspan ='19' style='width:400px; text-align:right; font-weight:bold'> Fecha impresión:  " + DateTime.Now.ToString() + " <br/> Usuario: " + sesion.U_Nombre  + " </td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td  colspan='20' style='width:400px; text-align:center; font-weight:bold'>&nbsp; Entradas CRM " + TipoCd + " " + this.cmbmes.Text + " " + this.cmbanio.Text  + "  </td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td>  </td><td>  </td><td style='text-align:left;font-weight:bold'  bgcolor='#0066FF' colspan='1'>  </td> <td style='text-align:left;font-weight:bold'> Domingo </td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td> </td><td>  </td><td style='text-align:left;font-weight:bold'  bgcolor='Red' colspan='1'>  </td> <td style='text-align:left;font-weight:bold'> Asueto </td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td>  </td><td>  </td><td style='text-align:left;font-weight:bold'  bgcolor='Orange' colspan='1'>  </td> <td style='text-align:left;font-weight:bold'> Curso </td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td>  </td><td>  </td><td style='text-align:left;font-weight:bold'  bgcolor='Green' colspan='1'>  </td> <td style='text-align:left;font-weight:bold'> Incapacidad </td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td>  </td><td>  </td><td style='text-align:left;font-weight:bold'  bgcolor='Silver' colspan='1'>  </td> <td style='text-align:left;font-weight:bold'> Permiso </td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td>  </td><td>  </td><td style='text-align:left;font-weight:bold'  bgcolor='Yellow' colspan='1'>  </td> <td style='text-align:left;font-weight:bold'> Incumplimiento </td>");
                Tabla.Append("</tr>");
       

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

                System.Data.DataTable dt = new System.Data.DataTable();
                CN_ConfiguracionDias cn_cfd = new CN_ConfiguracionDias();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                cn_cfd.RepEntradasCRM_Consulta(int.Parse(this.RblTipoCd.SelectedValue), int.Parse(this.cmbanio.SelectedValue), int.Parse(this.cmbmes.SelectedValue), int.Parse(this.RblTipoRik.SelectedValue),sesion.Id_U, ref dt);

           
                Tabla.Append("<tr>");

                //lectura y escritura de columnas
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    if (dt.Columns[i].ColumnName == "CDI")
                    {
                        width = (i == 0) ? "180px" : "220px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("CDI");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Region")
                    {
                        width = (i == 0) ? "90px" : "120px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Región");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Rik")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("No. Rik");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Representante")
                    {
                        width = (i == 0) ? "220px" : "270px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Representate");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Frecuencia")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Frecuencia");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Registros")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Registros");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Accesos")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Entradas");
                        Tabla.Append("</th>");
                    }
                    else
                    {
                         width = (i == 0) ? "30px" : "50px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append(dt.Columns[i].ColumnName);
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

                        if (dt.Columns[i].ColumnName == "CDI" || dt.Columns[i].ColumnName == "Region" || dt.Columns[i].ColumnName == "Rik" || dt.Columns[i].ColumnName == "Representante" || dt.Columns[i].ColumnName == "Frecuencia")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Rik" || dt.Columns[i].ColumnName == "Frecuencia" || dt.Columns[i].ColumnName == "Registros" || dt.Columns[i].ColumnName == "Accesos")
                        {
                            Tabla.Append("<td   style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else
                        {
                            //Domingos
                            if (Convert.ToInt32(dt.Rows[j][i].ToString()) == -99)
                            {
                                Tabla.Append("<td bgcolor='#0066FF' style='text-align:center'>");
                                Tabla.Append("0");
                                Tabla.Append("</td>");

                            }
                            //Incumplimiento
                            else if (Convert.ToInt32(dt.Rows[j][i].ToString()) == -98)
                            {
                                Tabla.Append("<td bgcolor='Yellow' style='text-align:center'>");
                                Tabla.Append("0");
                                Tabla.Append("</td>");
                            }
                             //Asueto
                            else if (Convert.ToInt32(dt.Rows[j][i].ToString()) == -1)
                            {
                                Tabla.Append("<td bgcolor='Red' style='text-align:center'>");
                                Tabla.Append("0");
                                Tabla.Append("</td>");
                            }
                            //Curso
                            else if (Convert.ToInt32(dt.Rows[j][i].ToString()) == -2)
                            {
                                Tabla.Append("<td bgcolor='Orange' style='text-align:center'>");
                                Tabla.Append("0");
                                Tabla.Append("</td>");
                            }
                             //Incapacidad
                            else if (Convert.ToInt32(dt.Rows[j][i].ToString()) == -3)
                            {
                                Tabla.Append("<td bgcolor='Green' style='text-align:center'>");
                                Tabla.Append("0");
                                Tabla.Append("</td>");
                            
                            } 
                                //Permiso
                            else if (Convert.ToInt32(dt.Rows[j][i].ToString()) == -4)
                            {
                                Tabla.Append("<td bgcolor='silver' style='text-align:center'>");
                                Tabla.Append("0");
                                Tabla.Append("</td>");
                            }
                        

                            else
                            {
                                Tabla.Append("<td   style='text-align:center'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");

                            }

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
     
        #endregion Funciones
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

        private void Alerta2(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 600, 150);");
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
        #endregion
    }
}