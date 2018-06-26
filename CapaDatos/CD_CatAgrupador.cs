using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
   public class CD_CatAgrupador
    {
       public void CatAgrupador_ConsultaLista(ref List<Agrupador> List, String Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               SqlDataReader dr = null;

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SpCatAgrupadorEspecial_Lista", ref dr);

               Agrupador ag;
               while (dr.Read())
               {
                   ag = new Agrupador();
                   ag.Id_Agp = Convert.ToInt32(dr["Id_Agp"]);
                   ag.Ag_Descripcion = dr["Ag_Descripcion"].ToString();

                   List.Add(ag);
               }

               dr.Close();

               cd_datos.LimpiarSqlcommand(ref sqlcmd);


           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void CatAgrupador_Guardar(Agrupador ag, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
                
               string[] Parametros ={"@AgDescripcion"};
               object[] Valores = { ag.Ag_Descripcion};

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SpCatAgrupadorEspecial_Insertar", ref Verificador, Parametros, Valores);

               cd_datos.LimpiarSqlcommand(ref sqlcmd);

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void CatAgrupador_Modificar(Agrupador ag, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);

               string[] Parametros = { "@Id_Agp","@AgDescripcion" };
               object[] Valores = { ag.Id_Agp, ag.Ag_Descripcion };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SpCatAgrupadorEspecial_Modificar", ref Verificador, Parametros, Valores);

               cd_datos.LimpiarSqlcommand(ref sqlcmd);

           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public void CatAgrupador_CteConsultar(int Id_Cte, int Id_Cd,ref int Verificador, ref Agrupador Ag, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               SqlDataReader dr = null;

               string[] Parametros = { "@Id_Cte", "@Id_Cd" };
               object[] Valores = { Id_Cte, Id_Cd};

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SpCatCliente_ApEspecialConsultar", ref dr, Parametros, Valores);

               if (dr.Read())
               {
                   Verificador = 1;
                   Ag.Id_Cte = Convert.ToInt32(dr["Id_Cte"]);
                   Ag.Cte_Nombre = dr["Cte_Nombre"].ToString();
                   Ag.Id_Agp = Convert.ToInt32(dr["Id_Agp"]);
                   Ag.Ag_Descripcion = dr["Ag_Descripcion"].ToString();
               }
               else
               {
                   Verificador = 0;
               }

               dr.Close();

               cd_datos.LimpiarSqlcommand(ref sqlcmd);


           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void CatAgrupador_CteInsertar(Agrupador Ag, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);

               string[] Parametros= {"@Id_Cd", "@Id_Cte", "@Id_Agp"};
               object[] Valores = { Ag.Id_Cd, Ag.Id_Cte, Ag.Id_Agp };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SpCatCliente_ApEspecialInsertar", ref Verificador, Parametros, Valores);

               cd_datos.LimpiarSqlcommand(ref sqlcmd);


           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void CapClienteBloque_Consultar(int Id_Cte, int Id_Cd,ref int Verificador, ref int Verificador2, ref Agrupador Ag, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);
               DataSet ds = null;

               string[] Parametros = { "@Id_Cd", "@Id_Cte" };
               object[] Valores = { Id_Cd, Id_Cte };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SpCatCliente_BloqueoConsultar", ref ds, Parametros, Valores);

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

                           Ag.Cte_Nombre = row["Cte_Nombre"].ToString();
                           Ag.Cte_NoBloquear = Convert.ToBoolean(row["Cte_NoBloquear"]);

                       }

                   }
                   else
                   {
                       Verificador2 = -2;
                   }

               }
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void CapClienteBloque_CteInsertar(Agrupador Ag, ref int Verificador, string Conexion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(Conexion);

               string[] Parametros = { "@Id_Cd", "@Id_Cte", "@Bloqueo"};
               object[] Valores = { Ag.Id_Cd, Ag.Id_Cte, Ag.Cte_NoBloquear };

               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("SpCatCliente_BloqueoInsertar", ref Verificador, Parametros, Valores);

               cd_datos.LimpiarSqlcommand(ref sqlcmd);

           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
   
    }
}
