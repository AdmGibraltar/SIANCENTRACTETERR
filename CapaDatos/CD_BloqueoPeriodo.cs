using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data;
using System.Data.SqlClient;
namespace CapaDatos
{
   public class CD_BloqueoPeriodo
    {
       public void BloqueoPeriodo_Consultar(int Id_Cd, int Id_Cte, ref BloqueoPeriodo bp, ref  int Verificador, ref int Verificador2,  string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               DataSet ds = null;

               string[] Parametros = {"@Id_Cd", "@Id_Cte" };
               object[] Valores = { Id_Cd, Id_Cte };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapBloquePeriodo_Consulta", ref ds, Parametros, Valores);

               DataTable dt = ds.Tables[0];

               foreach (DataRow row in dt.Rows)
               {

                   Verificador = Convert.ToInt32(row["Verificador"]);

               }

               if (Verificador != -2)
               {
                   DataTable dt2 = ds.Tables[1];

                   if (dt2.Rows.Count > 0)
                   {
                       Verificador2 = -1;
                       foreach (DataRow row in dt2.Rows)
                       {

                           bp.Cte_nombre  = row["Cte_NomComercial"].ToString();
                           if (row["bp_FechaIni"].ToString() != "") { bp.Bp_FechaIni = Convert.ToDateTime(row["bp_FechaIni"]);}
                           if (row["bp_FechaFin"].ToString() != "") {bp.Bp_FechaFin = Convert.ToDateTime(row["bp_FechaFin"]);}

                       }

                   }
                   else
                   {
                       Verificador2 = -2;
                   }

               }


               cd_datos.LimpiarSqlcommand(ref sqlcmd);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void BloqueoPeriodo_Insertar(BloqueoPeriodo bp, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);

               string[] Parametros = { "@Id_Cd", "@Id_Cte", "@FechaIni", "@FechaFin" };
               object[] Valores = { bp.Id_cd, bp.Id_cte,  bp.Bp_FechaIni, bp.Bp_FechaFin };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapBloquePeriodo_Insertar", ref Verificador, Parametros, Valores);

               cd_datos.LimpiarSqlcommand(ref sqlcmd);

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
          
       }
    }
}
