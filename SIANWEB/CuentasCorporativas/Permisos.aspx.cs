using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;
using CapaNegocios;
using SIANWEB.Utilerias;

namespace SIANWEB.CuentasCorporativas
{
    public partial class Permisos : System.Web.UI.Page
    {

        SIANCENTRAL_CCEntities1 model = new SIANCENTRAL_CCEntities1();
        protected void Page_Load(object sender, EventArgs e)
        {
            var permisos = new PermisosSesion(this.Page);
            permisos.ValidarSesion();

            if (!Page.IsPostBack)
            {
                // permisos.ValidarPermisos(this.rtb1);


                int id_ClienteMat = Int32.Parse(Request.QueryString["Id"]);
                CN_CatCNac_Usuario cm_Usuario = new CN_CatCNac_Usuario(model);

                dgUsuarios.DataSource = cm_Usuario.ConsultarTodos(id_ClienteMat);
                dgUsuarios.DataBind();
            }
            
        }


        protected void dgUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CN_CatCNac_Usuario cm_Usuario = new CN_CatCNac_Usuario(model);

            int id_ClienteMat = Int32.Parse(Request.QueryString["Id"]);

            if (e.CommandName == "Eliminar")
            {
                int id = Int32.Parse(e.CommandArgument.ToString());

                if (!cm_Usuario.Eliminar(id))
                {
                    RAM1.ResponseScripts.Add("alert('El usuario no se puede eliminar ya que tiene alguna solicitud asociada');");
                }

                else
                    if (dgUsuarios != null)
                    {
                        dgUsuarios.DataSource = cm_Usuario.ConsultarTodos(id_ClienteMat);
                        dgUsuarios.DataBind();
                    }

            }

        }


    }
}