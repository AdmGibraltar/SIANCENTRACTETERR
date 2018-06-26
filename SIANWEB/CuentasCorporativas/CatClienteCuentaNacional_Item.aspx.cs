using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;
using CapaDatos;
using System.IO;
using Telerik.Web.UI;
using SIANWEB.Utilerias;

namespace SIANWEB.CuentasCorporativas
{
    public partial class CatClienteCuentaNacional_Item : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var permisos = new PermisosSesion(this.Page);
            permisos.ValidarSesion();


            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Id"] != "undefined")
                {

                    //permisos.ValidarPermisos(this.rtb1);

                    int id = Int32.Parse(Request.QueryString["Id"]);

                    CN_CatCNac_Matriz negCliente = (CN_CatCNac_Matriz)Session["CNCliente"];
                    var cliente = negCliente.ConsultarItem(id);

                    object objCN_Cliente = cliente;
                    AsignacionCampos.AsignaCamposForma(ref objCN_Cliente, "", this);
                }
            }

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            CN_CatCNac_Matriz negCliente = (CN_CatCNac_Matriz)Session["CNCliente"];
            CatCNac_Matriz cliente = new CatCNac_Matriz();


            if (Logo.PostedFile != null)
            {
                MemoryStream target = new MemoryStream();
                Logo.PostedFile.InputStream.CopyTo(target);
                cliente.Logo = target.ToArray();
            }

            object objCN_Cliente = cliente;
            AsignacionCampos.AsignaCamposEntidad(ref objCN_Cliente, "", this);

            if (Request.QueryString["Id"] == null || Request.QueryString["Id"] == "undefined")
            {
                cliente.Id = negCliente.ConsultarMax()+1;
                negCliente.Nuevo(cliente);
            }
            else
            {
                int id = Int32.Parse(Request.QueryString["Id"]);
                cliente.Id = id;
                negCliente.Editar(cliente);

            }

            RAM1.ResponseScripts.Add("CloseAlert('Los datos se guardaron correctamente');");

        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
        }
    }


}