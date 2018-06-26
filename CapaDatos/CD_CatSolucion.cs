using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
   public class CD_CatSolucion
    {
       public void Lista(Solucion solucion, string Conexion, ref List<Solucion> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp"};
                object[] Valores = { solucion.Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSolucion_Lista", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    solucion = new Solucion();
                    solucion.Id_Sol = (int)dr.GetValue(dr.GetOrdinal("Id_Sol"));
                    solucion.Id_Area = (int)dr.GetValue(dr.GetOrdinal("Id_Area"));
                    solucion.Sol_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Sol_Descripcion"));
                    solucion.Id_Seg = (int)dr.GetValue(dr.GetOrdinal("Id_Seg"));
                    solucion.Id_Uen = (int)dr.GetValue(dr.GetOrdinal("Id_Uen"));
                    solucion.Sol_Potencial = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Sol_Potencial")));
                    solucion.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Sol_Activo")));
                    if (Convert.ToBoolean(solucion.Estatus))
                    {
                        solucion.EstatusStr = "Activo";
                    }
                    else
                    {
                        solucion.EstatusStr = "Inactivo";
                    }
                    List.Add(solucion);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public void Insertar(Solucion solucion, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Sol",
                                          "@Id_Area", 
                                          "@Sol_Descripcion", 
                                          "@Sol_Potencial", 
                                          "@Estatus" 
                                      };
                object[] Valores = { 
                                       solucion.Id_Emp,
                                       solucion.Id_Sol,
                                       solucion.Id_Area,
                                       solucion.Sol_Descripcion,
                                       solucion.Sol_Potencial,
                                       solucion.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSolucion_Insertar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public void Modificar(Solucion solucion, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Sol",
                                          "@Id_Area", 
                                          "@Sol_Descripcion", 
                                          "@Sol_Potencial", 
                                          "@Estatus" 
                                      };
                object[] Valores = { 
                                       solucion.Id_Emp, 
                                       solucion.Id_Sol,
                                       solucion.Id_Area,
                                       solucion.Sol_Descripcion,
                                       solucion.Sol_Potencial,
                                       solucion.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSolucion_Modificar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
