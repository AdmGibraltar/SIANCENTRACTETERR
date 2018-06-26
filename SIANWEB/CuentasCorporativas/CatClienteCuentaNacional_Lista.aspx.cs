using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;
using CapaDatos;
using SIANWEB.Utilerias;

namespace SIANWEB.CuentasCorporativas
{
    public partial class CatClienteCuentaNacional_Lista : System.Web.UI.Page
    {
        SIANCENTRAL_CCEntities1 model = new SIANCENTRAL_CCEntities1();

        protected void Page_Load(object sender, EventArgs e)
        {

            var permisos = new PermisosSesion(this.Page);
            permisos.ValidarSesion();
            if (!Page.IsPostBack)
            {
                if (repClientes != null)
                {
                    CN_CatCNac_Matriz cn_CNCliente = new CN_CatCNac_Matriz(model);
                    Session["CNCliente"] = cn_CNCliente;

                    this.repClientes.DataSource = cn_CNCliente.ConsultarTodos();
                    this.repClientes.DataBind();
                }
            }


        }

        protected void btnAgregarNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("CatClienteCuentaNacional_Item.aspx");
        }

        protected void repClientes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void repClientes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType== ListItemType.AlternatingItem)
            {


                byte[] imagem = (byte[])(((CapaDatos.CatCNac_Matriz)(e.Item.DataItem)).Logo);
                if (imagem != null)
                {
                    string base64String = Convert.ToBase64String(imagem);

                    Image Image1 = (Image)e.Item.FindControl("imgLogo");
                    Image1.ImageUrl = String.Format("data:image/jpg;base64,{0}", base64String);
                }

            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (repClientes != null)
            {
                CN_CatCNac_Matriz cn_CNCliente = new CN_CatCNac_Matriz(model);
                Session["CNCliente"] = cn_CNCliente;

                this.repClientes.DataSource = cn_CNCliente.ConsultarItem(txtNombre.Text);
                this.repClientes.DataBind();
            }
        }



    }
}