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
    public partial class SolicitudesItem : System.Web.UI.Page
    {

        SIANCENTRAL_CCEntities1 model = new SIANCENTRAL_CCEntities1();

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Int32.Parse(Request.QueryString["Id"]);
            int sucursal = Int32.Parse(Request.QueryString["Sucursal"]);


            var permisos = new PermisosSesion(this.Page);
            permisos.ValidarSesion();

            if (!Page.IsPostBack)
            {

               // permisos.ValidarPermisos(this.rtb1);
                CN_CatCNac_Solicitudes cn = new CN_CatCNac_Solicitudes(model);
                var solic = cn.ConsultarItem(id, sucursal);

                object objMatriz_Solic = solic;
                AsignacionCampos.AsignaCamposForma(ref objMatriz_Solic, "", this);

                object objMatriz_SolicDirFis = solic.CatCNac_Solicitudes_DirFiscal_1;
                AsignacionCampos.AsignaCamposForma(ref objMatriz_SolicDirFis, "", this);


                

                if (solic.Estatus == 1 || solic.Estatus == 5)
                {
                    btnAceptar.Enabled = true;
                    btnRechazar.Enabled = true;
                }
                else
                {
                    btnAceptar.Enabled = false;
                    btnRechazar.Enabled = false;
                }



                //this.cmbAsesorId.DataSource = cn.ComboAsesores(solic.Id_Matriz.Value);
                //this.cmbAsesorId.DataBind();

            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            CN_CatCNac_Solicitudes cn = new CN_CatCNac_Solicitudes(model);
            int id = Int32.Parse(Request.QueryString["Id"]);
            int sucursal = Int32.Parse(Request.QueryString["Sucursal"]);

            int estatus = 2;

            cn.ActualizaSolicitud(id,sucursal, estatus, txtComentariosAdministrador.Text);

            RAM1.ResponseScripts.Add("CloseAlert('La solicitud ha sido aceptada');");

        }

        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            CN_CatCNac_Solicitudes cn = new CN_CatCNac_Solicitudes(model);
            int id = Int32.Parse(Request.QueryString["Id"]);
            int sucursal = Int32.Parse(Request.QueryString["Sucursal"]);

            int estatus = 3;

            cn.ActualizaSolicitud(id,sucursal, estatus, txtComentariosAdministrador.Text);

            RAM1.ResponseScripts.Add("CloseAlert('La solicitud ha sido rechazada');");
        }



    }
}