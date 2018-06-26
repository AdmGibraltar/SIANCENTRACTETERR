using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_ClienteMayorAdeudo
    {
    
       public static void RegresaAdeudoCliente(DateTime FechaCierre,DateTime FechaCorte, int DiasRevision,int Id_Cte, string Conexion,  ref double PorPagar, ref double Pagado)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@FechaCierre",
                                        "@FechaCorte", 
                                        "@DiasRevision",
                                        "@Id_Cte"
                                      };
                object[] Valores = { 
                                       FechaCierre,
                                       FechaCorte, 
                                       DiasRevision,
                                       Id_Cte,
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("Nivel3_SaldosCliente", ref dr, Parametros, Valores);
              
                while (dr.Read())
                {
                    PorPagar = (double)dr.GetValue(dr.GetOrdinal("Saldo"));
                    Pagado = (double)dr.GetValue(dr.GetOrdinal("Credito"));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public static void RegresaCuentasPagadas(DateTime FechaCierre, int DiasRevision, int Id_Cte, string Conexion, ref List<CobSaldosNiveles> Lista)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@FechaCierre", 
                                        "@DiasRevision",
                                        "@Id_Cte"
                                      };
                object[] Valores = { 
                                       FechaCierre, 
                                       DiasRevision,
                                       Id_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("Nivel4_SaldosClientePag", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    
                    CobSaldosNiveles Saldos = new CobSaldosNiveles();
                    if (float.Parse(dr.GetValue(dr.GetOrdinal("Credito")).ToString()) != 0)
                    {
                        Saldos.Cdi = dr.GetInt32(dr.GetOrdinal("CDI"));
                        Saldos.Cd_Nombre = dr.GetValue(dr.GetOrdinal("cd_Nombre")).ToString();
                        //Saldos.Id_Cte = dr.GetInt32(dr.GetOrdinal("Cliente"));
                        Saldos.Cartera = float.Parse(dr.GetValue(dr.GetOrdinal("Cargo")).ToString());
                        Saldos.Pagado = float.Parse(dr.GetValue(dr.GetOrdinal("Credito")).ToString());
                        Saldos.Restante = float.Parse(dr.GetValue(dr.GetOrdinal("Saldo")).ToString());
                        Lista.Add(Saldos);
                    }
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public static void RegresaCuentasPorPagadas(DateTime FechaCierre, int DiasRevision, int Id_Cte, string Conexion, ref List<CobSaldosNiveles> Lista)
       {
           try
           {
               SqlDataReader dr = null;
               CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

               string[] Parametros = { "@FechaCierre", 
                                        "@DiasRevision",
                                        "@Id_Cte"
                                      };
               object[] Valores = { 
                                       FechaCierre, 
                                       DiasRevision,
                                       Id_Cte
                                   };

               SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("Nivel4_SaldosClientePen", ref dr, Parametros, Valores);
               while (dr.Read())
               {
                  CobSaldosNiveles Saldos = new CobSaldosNiveles();
                   if (float.Parse(dr.GetValue(dr.GetOrdinal("Saldo")).ToString()) != 0)
                   {
                       Saldos.Cdi = dr.GetInt32(dr.GetOrdinal("CDI"));
                       Saldos.Cd_Nombre = dr.GetValue(dr.GetOrdinal("cd_Nombre")).ToString();
                       //Saldos.Id_Cte = dr.GetInt32(dr.GetOrdinal("Cliente"));
                       Saldos.Cartera = float.Parse(dr.GetValue(dr.GetOrdinal("Cargo")).ToString());
                       Saldos.Pagado = float.Parse(dr.GetValue(dr.GetOrdinal("Credito")).ToString());
                       Saldos.Restante = float.Parse(dr.GetValue(dr.GetOrdinal("Saldo")).ToString());
                       Lista.Add(Saldos);
                   }
               }
               CapaDatos.LimpiarSqlcommand(ref sqlcmd);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}