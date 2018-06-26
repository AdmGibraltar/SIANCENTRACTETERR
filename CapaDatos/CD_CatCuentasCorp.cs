using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatCuentasCorp
    {
        public void ConsultaCuentasCorp(int Id_Emp, string Conexion, ref List<CuentasCorp> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCuentasCorp_Consulta", ref dr, Parametros, Valores);

                CuentasCorp segmento;
                while (dr.Read())
                {
                    segmento = new CuentasCorp();
                    segmento.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    segmento.Id_Cc = (int)dr.GetValue(dr.GetOrdinal("Id_Cc"));
                    segmento.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Cc_Descripcion"));
                    segmento.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cc_Activo")));
                    if (Convert.ToBoolean(segmento.Estatus))
                    {
                        segmento.EstatusStr = "Activo";
                    }
                    else
                    {
                        segmento.EstatusStr = "Inactivo";
                    }
                    List.Add(segmento);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarCuentasCorp(CuentasCorp CuentasCorp, string Conexion, ref int verificador)
        {
            try
            {
                
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp",
                                        "@Id_Cc",
	                                    "@Cc_Descripcion", 
                                        "@Cc_Activo"
                                      };
                object[] Valores = { 
                                        CuentasCorp.Id_Emp,
                                        CuentasCorp.Id_Cc,
                                        CuentasCorp.Descripcion,
                                        CuentasCorp.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCuentasCorp_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCuentasCorp(CuentasCorp CuentasCorp, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp",
                                        "@Id_Cc",
	                                    "@Cc_Descripcion", 
                                        "@Cc_Activo"
                                      };
                object[] Valores = { 
                                        CuentasCorp.Id_Emp,
                                        CuentasCorp.Id_Cc,
                                        CuentasCorp.Descripcion,
                                        CuentasCorp.Estatus
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCuentasCorp_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarApcontable(ref CuentasCorp CC, int Id_Emp, int Id_CC, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros= {"@Id_Emp", "@Id_CC"};
                Object[] Valores = { Id_Emp, Id_CC };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatCuentasCorp_ConsultaApContable", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    CC.CC_ApContable = dr["CC_ApContable"].ToString().Trim();
                }

                dr.Close();

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void ModificarApContable(int Id_Emp, int Id_CC, string ApContable, string Conexion, ref int Verificador)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                String[] Parametros = { "@Id_Emp", "@Id_CC", "@CC_ApContable" };
                object[] Valores = { Id_Emp, Id_CC, ApContable };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatCuentasCorp_ModificarApContable", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
