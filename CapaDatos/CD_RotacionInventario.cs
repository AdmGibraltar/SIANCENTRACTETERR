using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_RotacionInventario
    {
        public void ProVentaNacional_Insertar(List<RotacionInventario> List, ref int Verificador, string Conexion)
        {
            try
            {
               

                string[] Parametros = {
                                          "@Id_Cd",
                                          "@Anio",
                                          "@Mes",
                                          "@Vn_VentaAAA"
                                      };
                foreach (RotacionInventario r in List)
                {
                    CD_Datos cd_datos = new CD_Datos(Conexion);
                    object[] Valores = {
                                           r.Id_Cd,
                                           r.Vn_Anio,
                                           r.Vn_Mes,
                                           r.Vn_VentaAAA
                                   };

                    SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProVentaNacional_Insertar", ref Verificador, Parametros, Valores);
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
