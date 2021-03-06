﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatCalendario
    {
        public void ConsultaCalendario(ref Calendario Calen, int año, CapaEntidad.Sesion sesion, ref List<Calendario> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);


                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Cal_Año"
                                      };

                object[] Valores = {
                                       sesion.Id_Emp.ToString(),
                                       sesion.Id_Cd_Ver,
                                       año.ToString()
                                   };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCalendario_Consulta", ref dr, parametros, Valores);

                //Calendario _calendario = default(Calendario);
                while (dr.Read())
                {
                    Calen = new Calendario();
                    Calen.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    Calen.Id_Cal = dr.GetInt32(dr.GetOrdinal("Id_Cal"));
                    Calen.Cal_Año = dr.GetInt32(dr.GetOrdinal("Cal_Año"));
                    Calen.Cal_Mes = dr.GetInt32(dr.GetOrdinal("Cal_Mes"));
                    Calen.Cal_FechaIni = dr.GetDateTime(dr.GetOrdinal("Cal_FechaIni"));
                    Calen.Cal_FechaFin = dr.GetDateTime(dr.GetOrdinal("Cal_FechaFin"));
                    Calen.Cal_Actual = dr.GetBoolean(dr.GetOrdinal("Cal_Actual"));
                    Calen.Cal_Activo = dr.GetBoolean(dr.GetOrdinal("Cal_Activo"));
                    Calen.Cal_FechaExtemporaneo = dr.IsDBNull(dr.GetOrdinal("Cal_FechaExtemporaneo")) ? "" : dr.GetDateTime(dr.GetOrdinal("Cal_FechaExtemporaneo")).ToString("dd/MM/yyyy");
                    list.Add(Calen);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCalendarioUltimaFecha(ref Calendario Calen, int año, CapaEntidad.Sesion sesion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);


                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Cal_Año"
                                      };

                string[] Valores = {
                                       sesion.Id_Emp.ToString(),
                                       año.ToString()
                                   };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCalendario_ConsultaUltimoPeriodo", ref dr, parametros, Valores);

                //Calendario _calendario = default(Calendario);
                while (dr.Read())
                {
                    Calen = new Calendario();
                    Calen.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    Calen.Id_Cal = dr.GetInt32(dr.GetOrdinal("Id_Cal"));
                    Calen.Cal_Año = dr.GetInt32(dr.GetOrdinal("Cal_Año"));
                    Calen.Cal_Mes = dr.GetInt32(dr.GetOrdinal("Cal_Mes"));
                    Calen.Cal_FechaIni = dr.GetDateTime(dr.GetOrdinal("Cal_FechaIni"));
                    Calen.Cal_FechaFin = dr.GetDateTime(dr.GetOrdinal("Cal_FechaFin"));
                    Calen.Cal_Actual = dr.GetBoolean(dr.GetOrdinal("Cal_Actual"));
                    Calen.Cal_Activo = dr.GetBoolean(dr.GetOrdinal("Cal_Activo"));                    
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCalendarioExistente(ref Calendario Calen, int año, CapaEntidad.Sesion sesion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);


                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Cal_Año"
                                      };

                string[] Valores = {
                                       sesion.Id_Emp.ToString(),
                                       año.ToString()
                                   };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCalendario_ConsultaUltimoPeriodo", ref dr, parametros, Valores);

                //Calendario _calendario = default(Calendario);
                while (dr.Read())
                {
                    Calen = new Calendario();
                    Calen.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    Calen.Id_Cal = dr.GetInt32(dr.GetOrdinal("Id_Cal"));
                    Calen.Cal_Año = dr.GetInt32(dr.GetOrdinal("Cal_Año"));
                    Calen.Cal_Mes = dr.GetInt32(dr.GetOrdinal("Cal_Mes"));
                    Calen.Cal_FechaIni = dr.GetDateTime(dr.GetOrdinal("Cal_FechaIni"));
                    Calen.Cal_FechaFin = dr.GetDateTime(dr.GetOrdinal("Cal_FechaFin"));
                    Calen.Cal_Actual = dr.GetBoolean(dr.GetOrdinal("Cal_Actual"));
                    Calen.Cal_Activo = dr.GetBoolean(dr.GetOrdinal("Cal_Activo"));
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GuardarCalendario(ref List<Calendario> calendarios, string Conexion, ref int verificador, bool actualizar)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                
                CapaDatos.StartTrans();
                string[] Parametros = {
			        "@Id_Emp",
	                "@Id_Cal",
	                "@Cal_Año",
	                "@Cal_Mes",
	                "@Cal_FechaIni",
	                "@Cal_FechaFin",
	                "@Cal_Actual",
	                "@Cal_Activo"
		        };
                SqlCommand sqlcmd = new SqlCommand();
                foreach (Calendario calendario in calendarios)
                {


                    object[] Valores = {
                                        calendario.Id_Emp,
                                        calendario.Id_Cal,
                                        calendario.Cal_Año,
                                        calendario.Cal_Mes,
                                        calendario.Cal_FechaIni,
                                        calendario.Cal_FechaFin,
                                        calendario.Cal_Actual,
                                        calendario.Cal_Activo
		                            };

                    sqlcmd = CapaDatos.GenerarSqlCommand(actualizar ? "spCatCalendario_Modificar" : "spCatCalendario_Insertar", ref verificador, Parametros, Valores);
                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }


        public void EliminarCalendario(int Id_Cal, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Cal" };
                object[] Valores = { Id_Cal };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand_Nonquery("spCatCalendario_Eliminar", verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarCalendarioAño(int Cal_Año, Sesion session, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(session.Emp_Cnx);
            try
            {                
                CapaDatos.StartTrans();
                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Cal_Año"                                      
                                      };
                object[] Valores = { 
                                       session.Id_Emp,
                                       Cal_Año
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand_Nonquery("spCatCalendario_EliminarAño", verificador, Parametros, Valores);
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void VerificaCalendario(ref Calendario Calen, int Cal_Año, int Cal_Mes, Sesion sesion, ref List<Calendario> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);


                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Cal_Año",
                                          "@Cal_Mes",
                                          "@Id_Cd"
                                      };

                object[] Valores = {
                                       sesion.Id_Emp,
                                       Cal_Año,
                                       Cal_Mes,
                                       sesion.Id_Cd_Ver
                                   };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCalendario_Verifica", ref dr, parametros, Valores);
                
                while (dr.Read())
                {
                    Calen = new Calendario();
                    Calen.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    Calen.Id_Cal = dr.GetInt32(dr.GetOrdinal("Id_Cal"));
                    Calen.Cal_Año = dr.GetInt32(dr.GetOrdinal("Cal_Año"));
                    Calen.Cal_Mes = dr.GetInt32(dr.GetOrdinal("Cal_Mes"));
                    Calen.Cal_FechaIni = dr.GetDateTime(dr.GetOrdinal("Cal_FechaIni"));
                    Calen.Cal_FechaFin = dr.GetDateTime(dr.GetOrdinal("Cal_FechaFin"));
                    Calen.Cal_Actual = dr.GetBoolean(dr.GetOrdinal("Cal_Actual"));
                    Calen.Cal_Activo = dr.GetBoolean(dr.GetOrdinal("Cal_Activo"));
                    list.Add(Calen);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCalendarioActual(ref Calendario Calen, Sesion sesion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);


                string[] parametros = { 
                                          "@Id_Emp", "@Id_Cd"
                                      };

                object[] Valores = {
                                       sesion.Id_Emp,
                                       sesion.Id_Cd_Ver
                                   };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCalendario_ConsultaActual", ref dr, parametros, Valores);
                int contador = 0;              
                while (dr.Read())
                {
                    if (contador == 0)
                    {
                        Calen = new Calendario();
                        Calen.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                        Calen.Id_Cal = dr.GetInt32(dr.GetOrdinal("Id_Cal"));
                        Calen.Cal_Año = dr.GetInt32(dr.GetOrdinal("Cal_Año"));
                        Calen.Cal_Mes = dr.GetInt32(dr.GetOrdinal("Cal_Mes"));
                        Calen.Cal_FechaIni = dr.GetDateTime(dr.GetOrdinal("Cal_FechaIni"));
                        Calen.Cal_FechaFin = dr.GetDateTime(dr.GetOrdinal("Cal_FechaFin"));
                        Calen.Cal_Actual = dr.GetBoolean(dr.GetOrdinal("Cal_Actual"));
                        Calen.Cal_Activo = dr.GetBoolean(dr.GetOrdinal("Cal_Activo"));
                    }
                    else
                    {
                        throw new Exception("Existen mas de 1 calendario activos");
                    }
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPeriodo(ref Calendario Cal, Sesion sesion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(sesion.Emp_Cnx);
                SqlDataReader dr =  null;

                String[] Parametros = {"@Id_Emp", "@Id_Cd", "@Cal_Año", "@Cal_Mes" };
                Object[] Valores = { sesion.Id_Emp, sesion.Id_Cd, Cal.Cal_Año, Cal.Cal_Mes };

                 SqlCommand sqlcmd =   cd_datos.GenerarSqlCommand("spCatCalendario_ConsultaPeriodo", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    Cal.Cal_FechaIni = Convert.ToDateTime(dr["Cal_FechaIni"]);
                    Cal.Cal_FechaFin = Convert.ToDateTime(dr["Cal_FechaFin"]);

                }

                dr.Close();

                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
    }
}
