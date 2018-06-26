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
   public  class CD_CapEntSalCentral
    {
       public void ConsultaLista (EntradasSalidasCentral Es, ref List<EntradasSalidasCentral> List, Sesion sesion)
       {
       try 
	    {	       
 
           CD_Datos cd_datos = new CD_Datos(sesion.Emp_Cnx);
           SqlDataReader dr = null;

           String[] Parametros = {
                                    "@Id_Alm",
                                    "@Id_Tm", 
                                    "@Referencia",
                                    "@Naturaleza",
                                    "@Movini",
                                    "@Movfin", 
                                    "@FechaIni",
                                    "@FechaFin"
                                 };
           Object[] Valores  = {
                                   Es.Alm, 
                                   Es.Tm,
                                   Es.Ref, 
                                   Es.Nat, 
                                   Es.MovIni, 
                                   Es.MovFin, 
                                   DateTime.Parse( Es.Fechaini),
                                   DateTime.Parse (Es.Fechafin) 
                               };

           SqlCommand sqlcmd = cd_datos.GenerarSqlCommandNulls("spCatMovimientosCentral_ConsultaLista", ref dr, Parametros, Valores);

           EntradasSalidasCentral e;

           while (dr.Read())
           {
               e = new EntradasSalidasCentral();
               e.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
               e.Id_Alm = Convert.ToInt32(dr["Id_Alm"]);
               e.Id_Tm = Convert.ToInt32(dr["Id_Tm"]);
               e.Id_MovC = Convert.ToInt32(dr["Id_MovC"]);
               e.MovC_Naturaleza = Convert.ToBoolean(dr["MovC_Naturaleza"]);
               e.Almacen  = dr["Almacen"].ToString();
               e.TipoMov = dr["TipoMov"].ToString();
               e.MovC_Fecha = Convert.ToDateTime(dr["MovC_Fecha"]);
               e.MovC_Referencia = dr["MovC_Referencia"].ToString();
               e.TotalFac = dr["TotalFac"].ToString();
               e.TotalCostoEst = dr["TotalCostoEst"].ToString();
               e.Variacion = dr["Variacion"].ToString();

               List.Add(e);
           }

           dr.Close();

           cd_datos.LimpiarSqlcommand(ref sqlcmd);

	    }
	    catch (Exception ex)
	    {
		    throw ex;
	    }
      }
       public void ConsultaEncabezado(int Id_Emp, int Id_Alm, int Id_MovC, int Id_Tm, bool MovC_Naturaleza, ref EntradasSalidasCentral es, Sesion sesion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(sesion.Emp_Cnx);
               SqlDataReader dr = null;

               String[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Alm", 
                                        "@Id_MovC",
                                        "@Id_Tm", 
                                        "@MovC_Naturaleza"
                                     };
               Object[] Valores = {
                                    Id_Emp, 
                                    Id_Alm,
                                    Id_MovC, 
                                    Id_Tm, 
                                    MovC_Naturaleza
                                  };
               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatMovimientosCentral_Consulta", ref dr, Parametros, Valores);

               if (dr.Read())
               {
                   es.Id_MovC = Convert.ToInt32(dr["Id_MovC"]);
                   es.MovC_Fecha = Convert.ToDateTime(dr["MovC_Fecha"]);
                   es.Alm = dr["Almacen"].ToString();
                   es.TipoMov = dr["TipoMov"].ToString();
                   es.MovC_Referencia = dr["MovC_Referencia"].ToString();
                   es.AplContable = dr["AplContable"].ToString();
                   es.Remitente = dr["Remitente"].ToString();
                   es.Destino = dr["Destino"].ToString();

               }

               dr.Close();

               cd_datos.LimpiarSqlcommand(ref sqlcmd);


           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConsultaDetalle(int Id_Emp, int Id_Alm, int Id_MovC, int Id_Tm, bool MovC_Naturaleza, ref List<EntradasSalidasCentralDet> List, Sesion sesion)
       {
           try
           {
               CD_Datos cd_datos = new CD_Datos(sesion.Emp_Cnx);
               SqlDataReader dr = null;

               String[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Alm", 
                                        "@Id_MovC",
                                        "@Id_Tm", 
                                        "@MovC_Naturaleza"
                                     };
               Object[] Valores = {
                                    Id_Emp, 
                                    Id_Alm,
                                    Id_MovC, 
                                    Id_Tm, 
                                    MovC_Naturaleza
                                  };
               SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatMovimientosCentral_ConsultaDet", ref dr, Parametros, Valores);

               EntradasSalidasCentralDet es;

               while  (dr.Read())
               {
                   es = new EntradasSalidasCentralDet();
                   es.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                   es.Id_Alm  = Convert.ToInt32(dr["Id_Alm"]);
                   es.Id_MovC = Convert.ToInt32(dr["Id_MovC"]);
                   es.Id_MovCDet = Convert.ToInt32(dr["Id_MovCDet"]);
                   es.Id_Tm = Convert.ToInt32(dr["Id_Tm"]);
                   es.MovC_Naturaleza = Convert.ToBoolean(dr["MovC_Naturaleza"]);
                   es.Id_Prd = Convert.ToInt32(dr["Id_Prd"]);
                   es.Prd_Descripcion = dr["Prd_Descripcion"].ToString();
                   es.Prd_Presentacion = dr["Prd_Presentacion"].ToString();
                   es.MovC_Cant = Convert.ToInt32(dr["MovC_Cant"]);
                   es.MovC_CostoFac = Convert.ToDouble(dr["MovC_CostoFac"]);
                   es.MovC_CostoEst = Convert.ToDouble(dr["MovC_CostoEst"]);
                   es.TotalFac = Convert.ToDouble(dr["TotalFac"]);
                   es.TotalEst = Convert.ToDouble(dr["TotalEst"]);
                   es.Variacion = Convert.ToDouble(dr["Variacion"]);

                   List.Add(es);

               }

               dr.Close();

               cd_datos.LimpiarSqlcommand(ref sqlcmd);


           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public void ModificarDetalle(List<EntradasSalidasCentralDet> List, Sesion sesion, ref int Verificador)
       {
           CD_Datos cd_datos = new CD_Datos(sesion.Emp_Cnx);
           try
           {
               cd_datos.StartTrans();
               SqlCommand sqlcmd = new SqlCommand();

               String[] Parametros = {
                                        "@Id_Emp", 
                                        "@Id_Alm",
                                        "@Id_MovC",
                                        "@Id_Tm",
                                        "@MovC_Naturaleza", 
                                        "@Id_Prd", 
                                        "@MovC_CostoFac"
                                     };

               foreach (EntradasSalidasCentralDet e in List)
               {
                   object[] Valores = {
                                          e.Id_Emp, 
                                          e.Id_Alm, 
                                          e.Id_MovC, 
                                          e.Id_Tm,
                                          e.MovC_Naturaleza,
                                          e.Id_Prd,
                                          e.MovC_CostoFac 
                                      };

                   sqlcmd = cd_datos.GenerarSqlCommand("spCatMovimientosCentralDet_Modificar", ref Verificador, Parametros, Valores);

               }
               cd_datos.CommitTrans();
               cd_datos.LimpiarSqlcommand(ref sqlcmd);


           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }

    }
}
