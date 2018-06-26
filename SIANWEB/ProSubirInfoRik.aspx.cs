using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
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
    public partial class ProSubirInfoRik : System.Web.UI.Page
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
                      
                    }
                }

            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }
        protected void Customvalidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                //args.IsValid = (RadUpload1.InvalidFiles.Count == 0);
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


                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<Representantes> List = new List<Representantes>();
                CN_CatRepresentantes  cn_re = new CN_CatRepresentantes();
                
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
                //lblMensaje.Text = "abriendo conexion";
                con.Close();
                con.Open();
                //lblMensaje.Text = "conexion abierta";
                DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string hoja = dt.Rows[1].ItemArray[2].ToString().Replace("'", "");
                //lblMensaje.Text = "hoja" + hoja;
                OleDbCommand cmd = new OleDbCommand("select * from [" + hoja + "]", con);
                OleDbDataAdapter dad = new OleDbDataAdapter();
                dad.SelectCommand = cmd;
                DataSet ds = new DataSet();
                dad.Fill(ds);

               Representantes  r;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    r = new Representantes();
                    if (dr[0].ToString() != "")
                    {
                        r.Id_Empl   = Convert.ToInt32(dr[0]);
                        r.Id_Rik    = Convert.ToInt32  (dr[1]);
                        r.RE_FechaA = dr[2].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr[2]);
                        r.RE_FechaB = dr[3].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr[3]);  
                        List.Add(r);
                    }

                }

                //lblMensaje.Text = "lleno dataset";
                con.Close();
                int Verificador = 0;
                cn_re.ActualizaDatosRik(List, ref Verificador, Sesion.Emp_Cnx);
                if (Verificador == -1)
                {
                    Alerta("Se actualizo la información de manera exitosa");
                    con.Close();
                }
                else
                {
                    Alerta("Ocurrio un error al tratar de actualizar la infromación");
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
                //this.DisplayMensajeAlerta(ex.Message);
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
                    if (btn.CommandName == "Descargar")
                    {
                        this.Descargar();
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
        #endregion
        #region Funciones
        private void Descargar()
        {
            try
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=FormatoRiks.xls");
                Response.Flush();
                Response.TransmitFile("~/Documentos/FormatoRiks.xlsx");
                Response.End();

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