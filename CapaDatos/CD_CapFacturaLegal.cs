using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CapFacturaLegal
    {
        public void CapFacturaLegal_Consulta(ref CapFacturaLegal fac, ref int Verificador, int Id_Cd, int Id_Fac, int Tipo, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@Id_Cd", "@Id_Fac", "@Tipo" };
                object[] Valores = { Id_Cd, Id_Fac, Tipo };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapFacturaLegal_Consultar", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    Verificador = 1;
                    fac.Id_Cte = Convert.ToInt32(dr["Id_Cte"]);
                    fac.Cte_Nombre = dr["Cte_NomComercial"].ToString();
                    fac.Total = Convert.ToDouble(dr["Total"]);
                    fac.Saldo = Convert.ToDouble(dr["Saldo"]);
                    fac.Pagado = Convert.ToDouble(dr["Pagado"]);
                    fac.FacL_Legal = Convert.ToInt32(dr["FacL_Legal"]);
                    fac.FacL_Comentarios = dr["FacL_Comentarios"].ToString();
                }
                else
                {
                    Verificador = 0;
                }

                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void CapFacturaLegal_Guardar(ref CapFacturaLegal fac, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = { "@Id_Cd", "@Id_Fac", "@Id_Cte", "@FacL_Legal", "@FacL_Comentarios" , "@Tipo"};
                object[] Valores = { fac.Id_Cd, fac.Id_Fac, fac.Id_Cte, fac.FacL_Legal, fac.FacL_Comentarios , fac.Fac_Tipo};

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapFacturaLegal_Insertar", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
