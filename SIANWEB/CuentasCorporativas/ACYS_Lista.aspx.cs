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
    public partial class ACYS_Lista : System.Web.UI.Page
    {

        SIANCENTRAL_CCEntities1 model = new SIANCENTRAL_CCEntities1();



        protected void Page_Load(object sender, EventArgs e)
        {

            int id = Int32.Parse(Request.QueryString["Id"]);


            var permisos = new PermisosSesion(this.Page);
            permisos.ValidarSesion();

            if (!Page.IsPostBack)
            {
                 //permisos.ValidarPermisos(this.rtb1);
                if (dgACYS != null)
                {
                    CN_CatCNac_ACYS cnACYS = new CN_CatCNac_ACYS(model);
                    dgACYS.DataSource = cnACYS.ConsultarACYS(id);
                    dgACYS.DataBind();

                }

            }
        }


        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {

        }

        protected void dgACYS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CN_CatCNac_ACYS cnACYS = new CN_CatCNac_ACYS(model);

            int idAcys = Int32.Parse(Request.QueryString["Id"]);

            if (e.CommandName == "Deshabilitar")
            {
                int id =  Int32.Parse(e.CommandArgument.ToString());
                cnACYS.Deshabilitar(id);



                if (dgACYS != null)
                {
                    dgACYS.DataSource = cnACYS.ConsultarACYS(idAcys);
                    dgACYS.DataBind();
                }

            }

            if (e.CommandName == "Duplicar")
            {
                int id = Int32.Parse(e.CommandArgument.ToString());
                cnACYS.DuplicarACYS(id);

                if (dgACYS != null)
                {
                    dgACYS.DataSource = cnACYS.ConsultarACYS(idAcys);
                    dgACYS.DataBind();
                }
            }


            if (e.CommandName == "Eliminar")
            {
                int id = Int32.Parse(e.CommandArgument.ToString());

                if (!cnACYS.Eliminar(id))
                {
                    RAM1.ResponseScripts.Add("alert('El acys no se puede eliminar ya que tiene un cliente asociado');");
                }

                else
                    if (dgACYS != null)
                    {
                        dgACYS.DataSource = cnACYS.ConsultarACYS(idAcys);
                        dgACYS.DataBind();
                    }

            }

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(Request.QueryString["Id"]);

                if (dgACYS != null)
                {
                    CN_CatCNac_ACYS cnACYS = new CN_CatCNac_ACYS(model);
                    dgACYS.DataSource = cnACYS.ConsultarACYS_Item(id,txtNombre.Text);
                    dgACYS.DataBind();

                }

        }
    }
}