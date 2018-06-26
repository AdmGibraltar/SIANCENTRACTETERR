using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;

namespace SIANWEB
{
    public partial class ObtenerMaximo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string valor_retorno = "";
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            int Centro = Sesion.Id_Cd_Ver;
            int Empresa = Sesion.Id_Emp;
            string Catalogo = Request.Params["Catalogo"].ToString();
            string sp = Request.Params["sp"].ToString();
            string columna = Request.Params["columna"].ToString();
            string Conexion = Sesion.Emp_Cnx;
            int Naturaleza = int.Parse(Request.Params["Naturaleza"]);

            try
            {

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                if (sp == "spCatCompensacion_Maximo")
                {
                    CapaNegocios.CN_CatCompensacion CN_Compensacion = new CapaNegocios.CN_CatCompensacion();
                    valor_retorno =  CN_Compensacion.Maximo(Sesion.Emp_Cnx, "spCatCompensacion_Maximo");
                }
                else
                    if (sp == "spCatCentral_MaximoMov")
                    {
                        //int naturaleza = Convert.ToInt32(Convert.ToBoolean(Request.Params["Naturaleza"]));
                        valor_retorno = CN_Comun.Maximo(Empresa, Catalogo, Naturaleza, columna, Conexion, sp);
                    }
                    else
                        if (sp == "spCatLocal_Maximo")
                        {
                            //valor_retorno = CN_Comun.Maximo(Sesion.Id_Emp, "CatBanco", "Id_Ban", Sesion.Emp_Cnx, "spCatLocal_Maximo");
                            valor_retorno = CN_Comun.Maximo(Empresa, Centro, Catalogo, columna, Conexion, sp);
                        }
                        else
                        {
                            valor_retorno = CN_Comun.Maximo(Empresa, Catalogo, columna, Conexion, sp);
                        }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Response.Write(valor_retorno);
        }
    }
}