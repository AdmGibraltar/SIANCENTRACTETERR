using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatAplicacion
    {
        public void Lista(Aplicacion aplicacion, string Conexion, ref List<Aplicacion> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { aplicacion.Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAplicacion_Lista", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    aplicacion = new Aplicacion();
                    aplicacion.Id_Apl = (int)dr.GetValue(dr.GetOrdinal("Id_Apl"));
                    aplicacion.Id_Sol = (int)dr.GetValue(dr.GetOrdinal("Id_Sol"));
                    aplicacion.Id_Area = (int)dr.GetValue(dr.GetOrdinal("Id_Area"));
                    aplicacion.Apl_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Apl_Descripcion"));
                    aplicacion.Id_Seg = (int)dr.GetValue(dr.GetOrdinal("Id_Seg"));
                    aplicacion.Id_Uen = (int)dr.GetValue(dr.GetOrdinal("Id_Uen"));
                    aplicacion.Apl_Potencial = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Apl_Potencial")));
                    aplicacion.Apl_Limpieza = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Apl_Limpieza")));
                    aplicacion.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Apl_Activo")));
                    if (Convert.ToBoolean(aplicacion.Estatus))
                    {
                        aplicacion.EstatusStr = "Activo";
                    }
                    else
                    {
                        aplicacion.EstatusStr = "Inactivo";
                    }
                    List.Add(aplicacion);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AplicacionesSegmento_Consultar(int Id_Emp, int Id_Seg, string Conexion, ref List<Aplicacion> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Seg" };
                object[] Valores = { Id_Emp, Id_Seg };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMSegmentoAplicaciones_Conssultar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Aplicacion aplicacion = new Aplicacion();
                    aplicacion.Id_Apl = (int)dr.GetValue(dr.GetOrdinal("Id_emp"));
                    aplicacion.Id_Apl = (int)dr.GetValue(dr.GetOrdinal("Id_Apl"));
                    aplicacion.Id_Sol = (int)dr.GetValue(dr.GetOrdinal("Id_Sol"));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Apl_Descripcion"))))
                        aplicacion.Apl_Descripcion = null;
                    else
                        aplicacion.Apl_Descripcion = dr.GetValue(dr.GetOrdinal("Apl_Descripcion")).ToString();

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Apl_Potencial"))))
                        aplicacion.Apl_Potencial = 0;
                    else
                        aplicacion.Apl_Potencial = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Apl_Potencial")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Apl_Limpieza"))))
                        aplicacion.Apl_Limpieza = false;
                    else
                        aplicacion.Apl_Limpieza = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Apl_Limpieza")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Apl_Activo"))))
                        aplicacion.Estatus = false;
                    else
                        aplicacion.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Apl_Activo")));

                    if (aplicacion.Estatus)
                    {
                        aplicacion.EstatusStr = "Activo";
                    }
                    else
                    {
                        aplicacion.EstatusStr = "Inactivo";
                    }
                    List.Add(aplicacion);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(Aplicacion aplicacion, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Apl",
                                          "@Id_Sol",
                                          "@Apl_Descripcion", 
                                          "@Apl_Potencial", 
                                          "@Apl_Limpieza",
                                          "@Estatus" 
                                      };
                object[] Valores = { 
                                       aplicacion.Id_Emp,
                                       aplicacion.Id_Apl,
                                       aplicacion.Id_Sol,
                                       aplicacion.Apl_Descripcion,
                                       aplicacion.Apl_Potencial,
                                       aplicacion.Apl_Limpieza,
                                       aplicacion.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAplicacion_Insertar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(Aplicacion aplicacion, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Apl",
                                          "@Id_Sol",
                                          "@Apl_Descripcion", 
                                          "@Apl_Potencial", 
                                          "@Apl_Limpieza",
                                          "@Estatus" 
                                      };
                object[] Valores = { 
                                       aplicacion.Id_Emp, 
                                       aplicacion.Id_Apl,
                                       aplicacion.Id_Sol,
                                       aplicacion.Apl_Descripcion,
                                       aplicacion.Apl_Potencial,
                                       aplicacion.Apl_Limpieza,
                                       aplicacion.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAplicacion_Modificar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
