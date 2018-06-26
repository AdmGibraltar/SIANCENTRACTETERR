using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_SegCobranza
    {
        public static void GuardarRotacion(List<DiasRotacion> List, int Verificador, string Conexion)
        {
            try
            {
                string[] Parametros = {     "@Id_Cd", 
                                            "@Cartera", 
                                            "@Rotacion", 
                                            "@Anio", 
                                            "@Mes"};

                foreach (DiasRotacion d in List)
                {
                    CD_Datos cd_datos = new CD_Datos(Conexion);
                    SqlCommand sqlcmd = null;
                    object[] Valores = {
                                        d.Id_Cd,
                                        d.Cartera,
                                        d.Rotacion,
                                        d.Anio, 
                                        d.Mes };

                    sqlcmd = cd_datos.GenerarSqlCommand("spCatDiasRotacion_Insertar", ref Verificador, Parametros, Valores);
                    cd_datos.LimpiarSqlcommand(ref sqlcmd);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

          public static void EliminarExistente(int Anio, int Mes, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = { "@Anio", "@Mes" };
                object[] Valores = { Anio, Mes };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatDiasRotacion_EliminaExistentes", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
