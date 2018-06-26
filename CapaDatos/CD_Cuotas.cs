using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Cuotas
    {
        public void CatCuotasCRM_EliminarExistente(int Anio, int Mes, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                string[] Parametros = { "@Anio", "@Mes" };
                object[] Valores = { Anio, Mes };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatCuotasCrm_EliminaExistentes", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void GuardarCuotas(List<Cuotas> List, ref int Verificador, string Conexion)
        {
            try
            {
              //  CD_Datos cd_datos = new CD_Datos(Conexion);
                string[] Parametros = {     "@Id_Cd", 
                                            "@Id_Rik", 
                                            "@Cuo_Anio", 
                                            "@Cuo_Mes", 
                                            "@Cuo_MontoProy", 
                                            "@Cuo_MontoCierre", 
                                            "@Cuo_NumProy", 
                                            "@Cuo_NumProyCierre" };

               
                foreach (Cuotas c in List)
                {
                    CD_Datos cd_datos = new CD_Datos(Conexion);
                    SqlCommand sqlcmd = null;
                    object[] Valores = {c.Id_Cd,
                                        c.Id_Rik, 
                                        c.Anio, 
                                        c.Mes,
                                        c.Proyecto, 
                                        c.Cierre,
                                        c.NumProy, 
                                        c.NumCierre  };

                    sqlcmd = cd_datos.GenerarSqlCommand("spCatCuotasCrm_Insertar", ref Verificador, Parametros, Valores);
                    cd_datos.LimpiarSqlcommand(ref sqlcmd);

                }
               

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
