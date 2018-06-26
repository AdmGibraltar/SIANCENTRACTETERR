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
    public partial class CatClienteMatriz_Permisos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var permisos = new PermisosSesion(this.Page);
            permisos.ValidarSesion();


            //permisos.ValidarPermisos(this.rtb1);

            lblId.Text = Request.QueryString["Id"];
            lblNombre.Text = Request.QueryString["Nombre"];

            int id = Int32.Parse(Request.QueryString["Id"]);

            CN_CatClienteMatriz cm_Matriz = new CN_CatClienteMatriz();
            List<CapaDatos.CatClienteMatriz_Permisos> lista=cm_Matriz.ConsultarPermisos(id);

            if (lista.Count > 0)
            {
                chkAcuEconomico.Checked = lista[0].AcuerdoEcon.Value;
                this.chkAsignadoRIK.Checked = lista[0].AsigRIK.Value;
                this.chkDatosFiscales.Checked = lista[0].DatosFisc.Value;
                this.chkDiasCredito.Checked = lista[0].DiasCredito.Value;
                this.chkMOV80.Checked = lista[0].MOV80.Value;
            }
           

        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            CN_CatClienteMatriz cm_Matriz = new CN_CatClienteMatriz();
            CapaDatos.CatClienteMatriz_Permisos entPermisos = new CapaDatos.CatClienteMatriz_Permisos();

            entPermisos.Id = Int32.Parse(Request.QueryString["Id"]);
            entPermisos.DatosFisc = chkDatosFiscales.Checked;
            entPermisos.AcuerdoEcon = chkAcuEconomico.Checked;
            entPermisos.MOV80 = chkMOV80.Checked;
            entPermisos.DiasCredito = chkDiasCredito.Checked;
            entPermisos.AsigRIK = chkAsignadoRIK.Checked;

            cm_Matriz.Guardar_Permisos(entPermisos);


            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
            string funcion = "CloseWindow()";
            string script = "<script>" + funcion + "</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);

        }


    }
}