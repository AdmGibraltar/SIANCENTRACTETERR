using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_RemisionesEntrega
    {
        public void ConsultaProRemisionEntrega(int Id_Emp, int Id_Cd, string Conexion, RemisionesEntrega remisionfiltro, ref List<RemisionesEntrega> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Filtro_Nombre",
                                          "@Filtro_CteIni",
                                          "@Filtro_CteFin",
                                          "@Filtro_FecIni",
                                          "@Filtro_FecFin"
                                      };
                object[] Valores = { 
                                       Id_Emp, 
                                       Id_Cd,
                                       remisionfiltro.Filtro_Nombre  == "" ? (object)null : remisionfiltro.Filtro_Nombre ,
                                       remisionfiltro.Filtro_Id_Cte  == "" ? (object)null : remisionfiltro.Filtro_Id_Cte ,
                                       remisionfiltro.Filtro_Id_Cte2  == "" ? (object)null : remisionfiltro.Filtro_Id_Cte2,
                                       remisionfiltro.Filtro_FecIni  == "" ? (object)null : remisionfiltro.Filtro_FecIni ,
                                       remisionfiltro.Filtro_FecFin  == "" ? (object)null : remisionfiltro.Filtro_FecFin 
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProRemisionesEntrega_Consulta", ref dr, Parametros, Valores);

                RemisionesEntrega remisionesEntrega;
                while (dr.Read())
                {
                    remisionesEntrega = new RemisionesEntrega();
                    remisionesEntrega.Id_Rem = (int)dr.GetValue(dr.GetOrdinal("Id_Rem"));
                    remisionesEntrega.Tipo = (string)dr.GetValue(dr.GetOrdinal("Rem_Tipo"));
                    remisionesEntrega.Estatus = (string)dr.GetValue(dr.GetOrdinal("Rem_Estatus"));
                    remisionesEntrega.Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("Rem_Fecha"));
                    remisionesEntrega.Numero = (int)dr.GetValue(dr.GetOrdinal("Id_Rem"));
                    remisionesEntrega.Pedido = (int)dr.GetValue(dr.GetOrdinal("Id_Ped"));
                    remisionesEntrega.Cliente = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    remisionesEntrega.Num_Cliente = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    remisionesEntrega.Fecha2 = !string.IsNullOrEmpty(dr.GetValue(dr.GetOrdinal("Rem_FechaEntrega")).ToString()) ? (DateTime)dr.GetValue(dr.GetOrdinal("Rem_FechaEntrega")) : DateTime.Now;
                    List.Add(remisionesEntrega);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarProRemisionEntrega(int Id_Emp, int Id_Cd, int Id_U, RemisionesEntrega remision, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                         "@Id_Emp",        
                                         "@Id_Cd",   
                                         "@Id_U",
                                         "@Id_Rem",
                                         "@Id_Ped"
                                      };
                object[] Valores = {                                       
                                       Id_Emp,
                                       Id_Cd,       
                                       Id_U,
                                       remision.Id_Rem,
                                       remision.Pedido                                                                        
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProRemisionesEntrega_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }            
    }
}
