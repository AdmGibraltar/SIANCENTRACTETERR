using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_RotInventarios
    {
        public void ProSaldoFinal_Inicializa(int Anio, int Mes, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = { "@Anio", "@Mes" };
                object[] Valores = { Anio, Mes };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProSaldoFinal_Inicializa", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void ProIndicadoresMensuales_Insertar(List<ProIndicadoresMensuales > List, ref int Verificador, string Conexion)
        {
            try
            {
                string[] Parametros = { 
                                        "@Id_Cd",
                                        "@Mes",
                                        "@Anio",
                                        "@Ind_AmortNormal",
                                        "@Ind_AmortAntic",
                                        "@Ind_VentasCtasNac",
                                        "@Ind_PolVariacion",
                                        "@Ind_OtrosCostos",
                                        "@Ind_1070",
                                        "@Ind_1073",
                                        "@Ind_1074",
                                        "@Ind_1075",
                                        "@Ind_1076" 
                                      
                                      };
                foreach (ProIndicadoresMensuales p in List)
                {
                    CD_Datos cd_datos = new CD_Datos(Conexion);
                    SqlCommand sqlcmd = null;
                    object[] Valores = {
                                          p.Id_Cd, 
                                          p.Mes, 
                                          p.Anio, 
                                          p.Ind_AmortNormal,
                                          p.Ind_AmortAntic,
                                          p.Ind_VentasCtasNac,
                                          p.Ind_PolVariacion,
                                          p.Ind_OtrosCostos,
                                          p.Ind_1070 ,
                                          p.Ind_1073,
                                          p.Ind_1074,
                                          p.Ind_1075,
                                          p.Ind_1076 
                                      };

                    sqlcmd = cd_datos.GenerarSqlCommand("spProIndicadoresMensuales_Insertar", ref Verificador, Parametros, Valores);
                    cd_datos.LimpiarSqlcommand(ref sqlcmd);
                }

          

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void ProObjetivoRotacion_Consultar( int Id_Cd, ref ProIndicadoresMensuales pi, string Conexion)
        {
            try
            {  
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Cd"};
                object[] Valores = { Id_Cd};

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProObjetivoRotacion_Consultar", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    pi.Ob_Dias = Convert.ToInt32(dr["Ob_Dias"]);
                    pi.Ob_Pesos = Convert.ToInt32(dr["Ob_Pesos"]);

                }

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void ProObjetivoRotacionDiario_Consultar(int Id_Cd, ref ProIndicadoresMensuales pi, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Cd" };
                object[] Valores = { Id_Cd };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProObjetivoRotacionDiario_Consultar", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    pi.Ob_Dias = Convert.ToInt32(dr["Obj_Dias"]);

                }

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ProObjetivoRotacion_Modificar( ProIndicadoresMensuales pi,ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = { "@Id_Cd", "@Ob_Dias", "@Ob_Pesos" };
                object[] Valores = { pi.Id_Cd ,pi.Ob_Dias, pi.Ob_Pesos  };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProObjetivoRotacion_Modificar", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void ProObjetivoRotacionDiario_Modificar(ProIndicadoresMensuales pi, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = { "@Id_Cd", "@Obj_Dias" };
                object[] Valores = { pi.Id_Cd, pi.Ob_Dias};

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spProObjetivoRotacionDiario_Modificar", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void RotacionInventariosMensual_Valida(int Anio, int Mes,ref int GeneroPoliza, ref int SubioIndicadores, ref int GeneroSaldos, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = { "@Anio", "@Mes" };
                object[] Valores = { Anio, Mes };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spRotacionInventariosMensual_Valida", ref dr, Parametros, Valores);
                
                if(dr.Read())
                {
                    GeneroPoliza = Convert.ToInt32(dr["GenPoliza"]);
                    SubioIndicadores=  Convert.ToInt32(dr["SubioIndicadores"]);
                    GeneroSaldos = Convert.ToInt32(dr["GenSaldos"]);
                }

                cd_datos.LimpiarSqlcommand (ref sqlcmd);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
