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
using System.Xml.Serialization;
using System.Text;
using System.IO;

namespace SIANWEB
{
    public partial class CapAutClienteTerr_Lista : System.Web.UI.Page
    {
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

        public List<ClienteTerritorio> lstSolicitud
        {
            get { return (List<ClienteTerritorio>)Session["lstSolicitud" + Session.SessionID]; }
            set { Session["lstSolicitud" + Session.SessionID] = value; }
        }

        public DataTable objdtLista { get; set; }

        protected DataTable objdtTabla { get { if (ViewState["objdtTabla"] != null) { return (DataTable)ViewState["objdtTabla"]; } else { return objdtLista; } } set { ViewState["objdtTabla"] = value; } }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        //InicializarTablaProductos();
                        //CargarEstatus();
                        rgFolios.DataSource = this.GetList();
                    }
                }
            }
            catch (Exception ex)
            {
                RAM.Alert("Error, " + ex.Message);
            }
        }

        private object GetList()
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<ClienteTerritorio> lstSolicitud = new List<ClienteTerritorio>();
                CN_CatCliente CN = new CN_CatCliente();

                CN.ConsultaSolicitudesClienteTerr(Sesion, ref lstSolicitud);
                return lstSolicitud;
            }
            catch (Exception ex)
            {
                RAM.Alert("Error, " + ex.Message);
                return lstSolicitud;
            }
        }

        protected void CmbSolicitud_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void CmbSolicitud_TextChanged(object sender, EventArgs e)
        {

        }

        protected void rgFolios_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                rgFolios.DataSource = this.GetList();
            }
            catch (Exception ex)
            {
                RAM.Alert("Error, " + ex.Message);
            }
        }

        protected void rgFolios_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            try
            {
                rgFolios.DataSource = this.GetList();
            }
            catch (Exception ex)
            {
                RAM.Alert("Error, " + ex.Message);
            }
        }

        protected void rgFolios_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Int32 item = default(Int32);
                if (e.Item == null) return;
                item = e.Item.ItemIndex;

                if (item >= 0)
                {
                    ClienteTerritorio ClienteTer = new ClienteTerritorio();
                    ClienteTer.Id_Solicitud = Convert.ToInt32(rgFolios.Items[item]["Id_Solicitud"].Text);
                    ClienteTer.Id_Cd = Convert.ToInt32(rgFolios.Items[item]["Id_Cd"].Text);
                    ClienteTer.Id_Cte = Convert.ToInt32(rgFolios.Items[item]["Id_Cte"].Text);
                    ClienteTer.Id_Ter = Convert.ToInt32(rgFolios.Items[item]["Id_Ter"].Text);
                    //ClienteTer.Dimension = Convert.ToDouble(rgFolios.Items[item]["Dimension"].Text);
                    //ClienteTer.Pesos = Convert.ToDouble(rgFolios.Items[item]["Pesos"].Text);
                    //ClienteTer.Potencial = Convert.ToDouble(rgFolios.Items[item]["Potencial"].Text);
                    //ClienteTer.ManodeObra = Convert.ToDouble(rgFolios.Items[item]["ManodeObra"].Text);
                    //ClienteTer.GastosTerritorio = Convert.ToDouble(rgFolios.Items[item]["GastosTerritorio"].Text);
                    //ClienteTer.FletesPagadoCliente = Convert.ToDouble(rgFolios.Items[item]["FletesPagadoCliente"].Text);
                    //ClienteTer.Porcentaje = Convert.ToDouble(rgFolios.Items[item]["Porcentaje"].Text);
                    //ClienteTer.Activo = ((CheckBox)e.Item.FindControl("chkActivo")).Checked;
                    //ClienteTer.Nuevo = ((CheckBox)e.Item.FindControl("chkNuevo")).Checked;

                    switch (e.CommandName.ToString())
                    {
                        case "Detalle":
                            RAM.ResponseScripts.Add(string.Concat(@"AbrirVentana_Solicitud('", ClienteTer.Id_Solicitud, "','", ClienteTer.Id_Cd, "','", ClienteTer.Id_Cte, "','", ClienteTer.Id_Ter, "')"));
                            break;
                        case "Autorizar":
                            Guardar(ClienteTer, "AUTORIZADO");
                            break;
                        case "Rechazar":
                            Guardar(ClienteTer, "RECHAZADO");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                RAM.Alert("Error, " + ex.Message);
            }
        }

        private void Guardar(ClienteTerritorio ClienteTer, string Estatus)
        {
            Sesion Sesion = new CapaEntidad.Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            XmlSerializer serializar = new XmlSerializer(typeof(ClienteTerritorio));
            try
            {
                CN_CatCliente CN = new CN_CatCliente();
                CN.ConsultaSolicitudClienteTerr(Sesion, ref ClienteTer);
                ClienteTer.Estatus = Estatus;

                CN.ActualizaSolClienteTerritorio(Sesion, ClienteTer, Estatus);

                CN.ConsultaSucursal(Sesion, ref ClienteTer);
                rgFolios.DataSource = this.GetList();

                #region Crear XML y consumir WsTerritorios

                StringBuilder sb = new StringBuilder();
                TextWriter tw = new StringWriter(sb);
                serializar.Serialize(tw, ClienteTer);
                tw.Close();
                string xmlClienteTer = sb.ToString();
                #endregion

                #region Llamar a webService

                wsClienteTerritorio.Service1 ws = new wsClienteTerritorio.Service1();
                ws.ActualizaAutClienteTerritorio(xmlClienteTer);

                #endregion



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void RadToolBar1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {

        }
    }
}