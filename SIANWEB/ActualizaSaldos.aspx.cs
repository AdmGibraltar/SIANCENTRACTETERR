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
using System.Data.OleDb;
using Telerik.Web.UI.GridExcelBuilder;
using System.IO;
using System.Collections;
using System.Data.SqlClient;

namespace SIANWEB
{
    public partial class ActualizaSaldos : System.Web.UI.Page
    {
        #region variables de excel
        public string NombreArchivo;
        public string NombreHojaExcel;
        public int Id_Pvd;
        public int Prd_Inicial;
        public int Prd_Final;
        public bool chkTransito;

      
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
                    string mensaje = "";
                    RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow_Excel('", mensaje, "')"));
                }
                else
                {

                    if (!Page.IsPostBack)
                    {
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_Cve.Value = randObj.Next().ToString();
                        CargarAnoMes();
                    }
                }

            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
        protected void BtnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                GenerarSaldos();
            }
            catch (Exception ex)
            {

                Alerta(ex.Message);
            }
        }

 

        #endregion
        #region Funciones
        private void CargarAnoMes()
        {
            try
            {

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun cn_comun = new CN__Comun();
                cn_comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd, sesion.Emp_Cnx, "spCatCalendarioAnhio2_Combo", ref this.cmbanio);
                cn_comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd, sesion.CalendarioIni.Year, sesion.Emp_Cnx, "spCatCalendarioMes_Combo", ref this.cmbmes);

                this.cmbanio.SelectedValue = DateTime.Now.Year.ToString();
                this.cmbmes.SelectedValue =  DateTime.Now.Month.ToString();
 

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void GenerarSaldos()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (this.cmbanio.SelectedValue == "-1")
                {
                    Alerta("Seleccione un año");
                    return;
                }
                if (this.cmbmes.SelectedValue == "-1")
                {
                    Alerta("Seleccione un mes");
                    return;
                }

                CN_RotInventarios cn_ri = new CN_RotInventarios();
                int Verificador = 0;
                cn_ri.ProSaldoFinal_Inicializa(int.Parse(this.cmbanio.SelectedValue), int.Parse(this.cmbmes.SelectedValue), ref Verificador, sesion.Emp_Cnx);

                if (Verificador == -1)
                {
                    Alerta("Se han generado los saldos finales de manera exitosa");
 
                }
                else if (Verificador == -2)
                {
                    Alerta("Imposible generar saldos, ya ya se han generado anteriormente");
                }
                else if (Verificador == -3)
                {
                    Alerta("Imposible generar saldos, ya que no se ha generado una póliza para este periodo");
                }
                else if (Verificador == -4)
                {
                    Alerta("Imposible generar saldos, ya que no se ha subido los indicadores del mes");
                }
                else if (Verificador == -5)
                {
                    Alerta("Imposible generar saldos, ya que no se ha generado los saldos del mes anterior");
                }



            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        #endregion
        #region ErrorManager
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