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
    public partial class CatDiasRotacion : System.Web.UI.Page
    {
        #region variables de excel
        public string NombreArchivo;
        public string NombreHojaExcel;
        public int Id_Pvd;
        public int Prd_Inicial;
        public int Prd_Final;
        public bool chkTransito;
        #endregion

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
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (Page.IsValid)
                {
                        this.Descargar();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void Descargar()
        {
            try
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=DiasRotacion.xls");
                Response.Flush();
                Response.TransmitFile("~/Documentos/DiasRotacion.xls");
                Response.End();
            }
            catch (Exception ex)
            {

                throw ex;
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

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GuardarRotacion(List<DiasRotacion> List, ref int Verificador)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Verificador = -1;
                // Primero elimino las de mes 
                //CN_SegCobranza.EliminarExistente(int.Parse(this.cmbanio.SelectedValue), int.Parse(this.cmbmes.SelectedValue), ref Verificador, sesion.Emp_Cnx);
                //if ()
               // {
                    CN_SegCobranza.GuardarRotacion(List, ref Verificador, sesion.Emp_Cnx);
                //}
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
                cn_comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd, sesion.CalendarioIni.Year, sesion.Emp_Cnx, "spCatCalendarioMes_Combo", ref this.cmbmes);

                this.cmbanio.SelectedValue = DateTime.Now.Year.ToString();
                this.cmbmes.SelectedValue = DateTime.Now.Month.ToString();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void RadAsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            try
            {

                Label fileName = new Label();
                fileName.Text = e.File.FileName;
                NombreArchivo = e.File.GetNameWithoutExtension().ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + e.File.GetExtension();
                NombreHojaExcel = e.File.GetNameWithoutExtension().ToString();

                if (e.IsValid)
                {
                    ValidFiles.Visible = true;
                    ValidFiles.Controls.Add(fileName);


                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
        protected void btnImportar_Click(object sender, EventArgs e)
        {
            OleDbConnection con = default(OleDbConnection);
            try
            {
                if (this.cmbanio.SelectedValue == "-1")
                {
                    Alerta("Seleccione el año");
                    con.Close();
                    return;
                }
                if (this.cmbmes.SelectedValue == "-1")
                {
                    Alerta("Seleccione el mes");
                    con.Close();
                    return;
                }


                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<DiasRotacion> List = new List<DiasRotacion>();

                string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + NombreArchivo;
                foreach (UploadedFile f in RadUpload1.UploadedFiles)
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    f.SaveAs(path, true);
                }



                string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + path + "';Extended Properties=\"Excel 12.0 xml;HDR=YES;IMEX=1;\"";
                con = new OleDbConnection(strConn);

                con.Close();
                con.Open();

                DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string hoja = dt.Rows[0].ItemArray[2].ToString().Replace("'", "");
                //lblMensaje.Text = "hoja" + hoja;
                OleDbCommand cmd = new OleDbCommand("select * from [" + hoja + "]", con);
                OleDbDataAdapter dad = new OleDbDataAdapter();
                dad.SelectCommand = cmd;
                DataSet ds = new DataSet();
                dad.Fill(ds);

                DiasRotacion dias;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    dias = new DiasRotacion();
                    if (dr[0].ToString() != "")
                    {
                        dias.Id_Cd = Convert.ToInt32(dr[0]);
                        dias.Cartera = float.Parse(dr[1].ToString());
                        dias.Rotacion = Convert.ToInt32(dr[2]);
                        dias.Anio = int.Parse(this.cmbanio.SelectedValue);
                        dias.Mes = int.Parse(this.cmbmes.SelectedValue);
                        List.Add(dias);
                    }

                }

                //lblMensaje.Text = "lleno dataset";
                con.Close();
                int Verificador = 0;

                GuardarRotacion(List, ref Verificador);
                if (Verificador == -1)
                {
                    Alerta("Se actualizo la información de manera exitosa.");
                    con.Close();
                }
                else
                {
                    Alerta("Ocurrio un error al tratar de actualizar la información.");
                    con.Close();
                }

                try
                {
                    File.Delete(path);
                }
                catch
                {
                }
            }
            catch (Exception ex)
            {
                con.Close();
                Alerta(ex.Message.Replace("'", ""));
            }
        }
        private void BulkCopy(string path, string hoja)
        {
            try
            {
                //'Declare Variables - Edit these based on your particular situation
                String sSQLTable = "TempTableForExcelImport";



                //'Create our connection strings
                string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0 xml;HDR=YES\";";
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                String sSqlConnectionString = Sesion.Emp_Cnx;


                //'Series of commands to bulk copy data from the excel file into our SQL table
                OleDbConnection OleDbConn = new OleDbConnection(strConn);
                OleDbCommand OleDbCmd = new OleDbCommand(("SELECT * FROM [" + hoja + "]"), OleDbConn);
                OleDbConn.Open();
                OleDbDataReader dr = OleDbCmd.ExecuteReader();
                SqlBulkCopy bulkCopy = new SqlBulkCopy(sSqlConnectionString);
                bulkCopy.DestinationTableName = sSQLTable;
                bulkCopy.WriteToServer(dr);
                OleDbConn.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void Mensaje()
        {
        Alerta( "Especificaciones del documento a subir: 1.- Zona     -> Únicamente la clave de la zona. Ejemplo: 110  2.- Saldo    -> Cantidades en millones.         Ejemplo: 1456256.00          3.- Rotación -> Días de rotación deseados.      Ejemplo: 35 ");
        }
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