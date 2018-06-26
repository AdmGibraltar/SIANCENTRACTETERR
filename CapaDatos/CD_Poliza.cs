using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;
using System.Collections;

namespace CapaDatos
{
   public class CD_Poliza
    {
       public void Poliza_ConsultaLista( int Ano, int Mes, ref List<Poliza> List, Sesion sesion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(sesion.Emp_Cnx);
               SqlDataReader dr = null;

               String[] Parametros = {"@Ano","@Mes" };
               Object[] Valores = { Ano, Mes };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapPoliza_ConsultaLista", ref dr, Parametros, Valores);

               Poliza p;

               while (dr.Read())
               {
                   p = new Poliza();
                   p.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                   p.Pol_Mes = Convert.ToInt32(dr["Pol_Mes"]);
                   p.Pol_Ano = Convert.ToInt32(dr["Pol_Ano"]);
                   p.Pol_Version = Convert.ToInt32(dr["Pol_Version"]);
                   p.Pol_Tipo = dr["Pol_Tipo"].ToString();
                   p.Pol_Cargo = Convert.ToDouble(dr["Pol_Cargo"]);
                   p.Pol_Abono = Convert.ToDouble(dr["Pol_Abono"]);

                   List.Add(p);
               }

               dr.Close();

               cd_datos.LimpiarSqlcommand(ref sqlcmd);



           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }

       public void PolizaRev_ConsultaLista(int ano, int mes, ref List<PolizaRev> List, Sesion sesion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(sesion.Emp_Cnx);
               SqlDataReader dr = null;

               String[] Parametros = { "@Ano", "@Mes" };
               Object[] Valores = { ano, mes };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapPolizaRev_ConsultaLista", ref dr, Parametros, Valores);

               PolizaRev p;

               while (dr.Read())
               {
                   p = new PolizaRev();
                   p.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                   p.Pol_Mes = Convert.ToInt32(dr["Pol_Mes"]);
                   p.Pol_Ano = Convert.ToInt32(dr["Pol_Ano"]);
                   p.Pol_Tipo = dr["Pol_Tipo"].ToString();
                   p.Pol_Cargo = Convert.ToDouble(dr["Pol_Cargo"]);
                   p.Pol_Abono = Convert.ToDouble(dr["Pol_Abono"]);

                   List.Add(p);
               }

               dr.Close();

               cd_datos.LimpiarSqlcommand(ref sqlcmd);



           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

       public void ProGeneraPolizaRev(int Id_Emp, int anio, int mes, ref int Verificador, string Emp_Cnx)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Emp_Cnx);
             
               String[] Parametros = {"@Id_Emp", "@Año", "@Mes" };
               Object[] Valores = {Id_Emp, anio, mes};

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("sp_generarevaluacioninventarios", ref Verificador, Parametros, Valores);

               cd_datos.LimpiarSqlcommand(ref sqlcmd);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
    }
}
