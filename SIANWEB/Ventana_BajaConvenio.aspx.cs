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
    public partial class Ventana_BajaConvenio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    HFId_PC.Value = Page.Request.QueryString["Id_PC"].ToString();
                    this.LblPC_NoConvenio.Text = Page.Request.QueryString["PC_NoConvenio"].ToString();
                    this.LblPC_Nombre.Text = Page.Request.QueryString["PC_Nombre"].ToString();
                    this.LblId_CatStr.Text = Page.Request.QueryString["Id_CatStr"].ToString();

                }
                
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {

            try
            {
                BajaConvenio();
             
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                btnAceptar_Click(btnAceptar, null);

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        private void BajaConvenio()
        {
            try
            {
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_Convenio cn_conv = new CN_Convenio();
                int Id_PC = int.Parse(HFId_PC.Value);
                int Verificador = 0;
           
                cn_conv.BajaConvenio(Id_PC, ref Verificador, Conexion);

                if (Verificador == -1)
                {
                    cn_conv.ConvenioCancelo_EnviarCorreo(Id_PC, sesion.Emp_Cnx);
                    AlertaCerrar("Se dio de baja el convenio correctamente");
   
                }
                else
                {
                    Alerta("Error al tratar de dar de baja el convenio");
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        #region ErrorManager
        private void RadConfirm(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radconfirm('" + mensaje + "<br /><br />', confirmCallBackFn, 330, 150);");
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
                RAM1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
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
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
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
                RAM1.ResponseScripts.Add("CloseWindowA('" + mensaje + "');");
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