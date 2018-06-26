using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;
using CapaDatos;
using Telerik.Web.UI;
using SIANWEB.Utilerias;

namespace SIANWEB.CuentasCorporativas
{
    public partial class Solicitudes : System.Web.UI.Page
    {

        SIANCENTRAL_CCEntities1 model = new SIANCENTRAL_CCEntities1();

        protected void Page_Load(object sender, EventArgs e)
        {

            var permisos = new PermisosSesion(this.Page);
            permisos.ValidarSesion();

            if (!Page.IsPostBack)
            {
               // permisos.ValidarPermisos(this.rtb1);

                CN_CatCNac_Solicitudes cn = new CN_CatCNac_Solicitudes(model);

                this.dgSolicitudes.DataSource = cn.ConsultarTodos("",0);
                dgSolicitudes.DataBind();


                this.cmbEstatus.DataSource = cn.ConsultarEstatus();
                this.cmbEstatus.DataBind();
            }
        }



        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":

                CN_CatCNac_Solicitudes cn = new CN_CatCNac_Solicitudes(model);
                this.dgSolicitudes.DataSource = cn.ConsultarTodos("", 0);
                dgSolicitudes.DataBind();
                
                break;
                }
            }
            catch (Exception ex)
            {
                // ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CN_CatCNac_Solicitudes cn = new CN_CatCNac_Solicitudes(model);

            int idEstatus = 0;
            if (cmbEstatus.SelectedValue != "") idEstatus = Int32.Parse(cmbEstatus.SelectedValue);

            this.dgSolicitudes.DataSource = cn.ConsultarTodos(txtNombre.Text, idEstatus);
            dgSolicitudes.DataBind();
        }

    }
}