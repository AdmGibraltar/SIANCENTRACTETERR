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
    public partial class ACYS_Nuevo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            var permisos = new PermisosSesion(this.Page);
            permisos.ValidarSesion();

            if (!Page.IsPostBack)
            {
                //permisos.ValidarPermisos(this.rtb1);

                int idMatriz = Int32.Parse(Request.QueryString["IdMatriz"]);

                SIANCENTRAL_CCEntities1 model = new SIANCENTRAL_CCEntities1();
                CN_CatCNac_ACYS cnACYS = new CN_CatCNac_ACYS(model);

                Session["cnACYS"] = cnACYS;

                if (Request.QueryString["Id"] != "undefined")
                {
                    int id = Int32.Parse(Request.QueryString["Id"]);

                    //CN_CatCNac_ACYS cnACYS = (CN_CatCNac_ACYS)Session["cnACYS"];
                     var acys = cnACYS.ConsultarACYS_Item(id);

                     object objCN_ACYS = acys;
                     AsignacionCampos.AsignaCamposForma(ref objCN_ACYS, "", this);
                }

                cmbNivelAcys.DataSource = cnACYS.ComboNiveles(idMatriz);
                cmbNivelAcys.DataBind();

                cmbTipoCuenta.DataSource = cnACYS.ComboTipoCuenta();
                cmbTipoCuenta.DataBind();
            }

        }


        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {

            CN_CatCNac_ACYS negAcys = (CN_CatCNac_ACYS)Session["cnACYS"];
            CatCNac_ACYS Acys = new CatCNac_ACYS();


            object objCN_Acys = Acys;
            AsignacionCampos.AsignaCamposEntidad(ref objCN_Acys, "", this);

            int idMatriz = Int32.Parse(Request.QueryString["IdMatriz"]);
            Acys.Id_Matriz = idMatriz;
            Acys.FechaUltimaAct = DateTime.Now;
            Acys.FechaVencimiento = new DateTime(2017, 12, 31);
            Acys.Activo = true;
            

            if (Request.QueryString["Id"] == null || Request.QueryString["Id"] == "undefined") negAcys.Nuevo(Acys);
            else
            {
                int id = Int32.Parse(Request.QueryString["Id"]);
                Acys.Id = id;
                negAcys.Editar(Acys);
            }

            RAM1.ResponseScripts.Add("CloseAndRebind('Los datos se guardaron correctamente');");



        }

    }
}