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
    public partial class CatConfigurarDias : System.Web.UI.Page
    {
        #region variables de excel
   
      
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
                        Inicializar();
                        rgDias.Rebind();
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
                    if (btn.CommandName == "Guardar")
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
        protected void rg_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    //
                    
                    this.rgDias.DataSource = GetList();
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
                this.rgDias.Rebind();
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void rg_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                Int32 item = default(Int32);
                item = e.Item.ItemIndex;

                switch (e.CommandName)
                {
                    case "Eliminar":
                        int Id_DF = Convert.ToInt32(rgDias.Items[item]["Id_DF"].Text);
                        CN_ConfiguracionDias cn_cd = new CN_ConfiguracionDias();
                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        int Verificador = 0;

                        cn_cd.CatConfiguracionDias_Eliminar(Id_DF, ref Verificador, sesion.Emp_Cnx);

                        if (Verificador == -1)
                        {
                            Alerta("Los datos se eliminaron correctamente");
                            rgDias.Rebind();
                        }

                        
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg_ItemCommand");
            }
        }
        protected void CmbNivel_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
              

                    List<Comun> List = new List<Comun>();
                    Comun c = new Comun();
                    c.Id = -1;
                    c.Descripcion = "--Todos--";
                    List.Add(c);

                if (this.CmbNivel.SelectedValue == "1")
                {

                    CmbCdi.DataSource = List;
                    CmbCdi.DataValueField = "Id";
                    CmbCdi.DataTextField = "Descripcion";
                    CmbCdi.DataBind();
                    CmbRepresentante.DataSource = List;
                    CmbRepresentante.DataValueField = "Id";
                    CmbRepresentante.DataTextField = "Descripcion";
                    CmbRepresentante.DataBind();
                    this.CmbCdi.Enabled = false;
                    this.CmbRepresentante.Enabled = false;
                }
                else if (this.CmbNivel.SelectedValue == "2" || this.CmbNivel.SelectedValue == "3")
                {
                    CargarCDI();
                    this.CmbCdi.Enabled = true;
                    CmbRepresentante.DataSource = List;
                    CmbRepresentante.DataValueField = "Id";
                    CmbRepresentante.DataTextField = "Descripcion";
                    CmbRepresentante.DataBind();
                    this.CmbRepresentante.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void CmbCDI_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
               
                if (this.CmbCdi.SelectedValue != "-1" && this.CmbNivel.SelectedValue == "3" )
                {
                    CargaRepresentantes();
                    this.CmbRepresentante.Enabled = true;
                
                }
               




            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        #endregion
        #region Funciones
        private void Inicializar()
        {
            try
            {
                List<Comun> List = new List<Comun>();
                Comun c = new Comun();
                c.Id = -1;
                c.Descripcion = "--Todos--";
                List.Add(c);
                CmbCdi.DataSource = List;
                CmbCdi.DataValueField = "Id";
                CmbCdi.DataTextField = "Descripcion";
                CmbCdi.DataBind();
                CmbRepresentante.DataSource = List;
                CmbRepresentante.DataValueField = "Id";
                CmbRepresentante.DataTextField = "Descripcion";
                CmbRepresentante.DataBind();

                this.CmbCdi.Enabled = false;
                this.CmbRepresentante.Enabled = false;
                this.CmbTipo.SelectedValue = "-1";
                this.rdFechaIni.SelectedDate = (DateTime?)null;
                this.rdFechaFin.SelectedDate = (DateTime?)null;
                this.TxtDF_Comentario.Text = string.Empty;

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

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun cn_comun = new CN__Comun();
                cn_comun.LlenaCombo(sesion.Emp_Cnx, "spCatCDI_ComboDias", ref this.CmbCdi);

              


            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void CargaRepresentantes()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun cn_comun = new CN__Comun();
                int id1 = int.Parse(this.CmbCdi.SelectedValue);
                cn_comun.LlenaCombo(id1, sesion.Emp_Cnx, "spCatCDI_ComboRepCentral", ref this.CmbRepresentante);

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
                ConfiguracionDias cf = new ConfiguracionDias();
                CN_ConfiguracionDias cn_cf = new CN_ConfiguracionDias();
                int Verificador = 0;

                if (this.CmbNivel.SelectedValue == "2")
                {
                    if (this.CmbCdi.SelectedValue == "-1")
                    {
                        Alerta("Seleccione el CDI");
                        return;
                    }
                }
                else if (this.CmbNivel.SelectedValue == "3")
                {
                    if (this.CmbCdi.SelectedValue == "-1")
                    {
                        Alerta("Seleccione el CDI");
                        return;
                    }

                    if (this.CmbRepresentante.SelectedValue == "-1")
                    {
                        Alerta("Seleccione el Representante");
                        return;
                    }
                }

                if (this.CmbTipo.SelectedValue == "-1")
                {
                    Alerta("Seleccione un tipo de justificación");
                    return;
                }

                if (this.rdFechaIni.SelectedDate == (DateTime?)null)
                {
                    Alerta("Ingrese la fecha inicial");
                    return;
                }
                if (this.rdFechaIni.SelectedDate == (DateTime?)null)
                {
                    Alerta("Ingrese la fecha final");
                    return;
                }

                if (this.rdFechaIni.SelectedDate > rdFechaFin.SelectedDate)
                {
                    Alerta("La fecha incial no puede ser mayor a la fecha final");
                    return;
                }

                cf.Id_Cd = int.Parse(this.CmbCdi.SelectedValue);
                cf.Id_Rik = int.Parse(this.CmbRepresentante.SelectedValue);
                cf.DF_RepNombre = this.CmbRepresentante.Text.Trim();
                cf.DF_FechaIni =Convert.ToDateTime (rdFechaIni.SelectedDate);
                cf.DF_FechaFin = Convert.ToDateTime(rdFechaFin.SelectedDate);
                cf.DF_Nivel = int.Parse(this.CmbNivel.SelectedValue);
                cf.DF_Tipo = int.Parse(this.CmbTipo.SelectedValue);
                cf.DF_Comentario = this.TxtDF_Comentario.Text.Trim();
                cf.Id_U =sesion.Id_U;

                cn_cf.CatConfiguracionDias_Insertar(cf, ref Verificador, sesion.Emp_Cnx);

                if (Verificador == -1)
                {
                    Alerta("Los datos se guardaron correctamente");
                    Inicializar();
                    rgDias.Rebind();
                }
              

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private List<ConfiguracionDias> GetList()
        {
            try
            {
          
            List<ConfiguracionDias> List = new List<ConfiguracionDias>();
            CN_ConfiguracionDias cn_cd = new CN_ConfiguracionDias();
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            cn_cd.CatConfiguracionDias_Lista(ref List, sesion.Emp_Cnx);

            return List;

            }
            catch (Exception)
            {

                throw;
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