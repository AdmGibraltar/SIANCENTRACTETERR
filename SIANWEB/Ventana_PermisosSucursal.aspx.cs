using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using System.Configuration;
using Telerik.Web.UI;

namespace SIANWEB
{ 
    public partial class Ventana_PermisosSucursal: System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    HFId_U.Value = Page.Request.QueryString["Id_U"].ToString();
                    this.LblPC_Nombre.Text = Page.Request.QueryString["U_Nombre"].ToString();
                    ConsultaLista();
                }
                
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        #region Eventos
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;

                if (Page.IsValid)
                {
                    if (btn.CommandName == "save")
                    {
                        Guardar();
                        
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
        private void ConsultaLista()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<Convenio> List = new List<Convenio>();
                int Id_U = int.Parse(HFId_U.Value);
                CN_Convenio cn_conv = new CN_Convenio();

                cn_conv.ConsultaPermisosSucursal (Id_U, ref List, sesion.Emp_Cnx);

                this.rgSucursal.DataSource = List;
                rgSucursal.DataBind();
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
                CN_Convenio cn_conv = new CN_Convenio();
                List<Convenio> List = new List<Convenio>();
                int Verificador = 0;

                LlenarLista(ref List);
                cn_conv.InsertarPermisosSucursal(List, ref Verificador, sesion.Emp_Cnx);

                if (Verificador == -1)
                {
                    AlertaCerrar("Los datos se guardaron correctamente");
                }
                else
                {
                    Alerta("Error inesperado al tratar de guardar los datos");
                }


            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void LlenarLista(ref List<Convenio> List)
        {
            try
            {
                Convenio c;
                foreach (GridDataItem grd in rgSucursal.Items)
                {
                    c = new Convenio();
                    if (int.Parse((grd.Controls[0].FindControl("LblId_CD") as Label).Text) != -1)
                    {

                        c.Id_U = int.Parse(grd["Id_U"].Text);
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
        private void RadConfirm(string mensaje)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("radconfirm('" + mensaje + "<br /><br />', confirmCallBackFn, 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }

        }
        private void AlertaFocus(string mensaje, string rtb)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
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
                RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void AlertaCerrar(string mensaje)
        {
            try
            {
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RadAjaxManager1.ResponseScripts.Add("CloseWindowA('" + mensaje + "');");
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
                this.LblMensaje.Text = "";
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
                this.LblMensaje.Text = Message;
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
                this.LblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                this.LblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion

      
    }
}