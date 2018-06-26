using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;
using SIANWEB.Utilerias;

namespace SIANWEB.CuentasCorporativas
{
    public partial class CatClienteMatriz_Afiliaciones : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {


            var permisos = new PermisosSesion(this.Page);
            permisos.ValidarSesion();

            //permisos.ValidarPermisos(this.rtb1);


            int id_ClienteMatriz = Int32.Parse(Request.QueryString["Id"]);

            CN_CatClienteMatriz cm_Matriz = new CN_CatClienteMatriz();
            dgAfiliaciones.DataSource = cm_Matriz.ConsultarAfiliaciones(id_ClienteMatriz);
            dgAfiliaciones.DataBind();

        }


    }
}