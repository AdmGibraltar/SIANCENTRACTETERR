using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;
namespace CapaDatos
{
    public class CD_CatAlmacen
    {
        public void CatAlmacen_Insertar(CatAlmacen almacen, Sesion sesion, ref int Verificador)
        {
            try
            {

                CD_Datos cd_datos = new CD_Datos(sesion.Emp_Cnx);

                String[] Parametros ={
                                        "@Alm_Clave",
                                        "@Alm_Nombre",
                                        "@Alm_Cuenta",
                                        "@Alm_Subcuenta",
                                        "@Alm_CtaCenCosto",
                                        "@Alm_SubCtaCenCosto",
                                        "@Alm_Activo",
                                        "@Id_U"
                                     };
                Object[] Valores = {
                                       almacen.Alm_Clave, 
                                       almacen.Alm_Nombre,
                                       almacen.Alm_CuentaStr, 
                                       almacen.Alm_SubcuentaStr, 
                                       almacen.Alm_CtaCenCosto,
                                       almacen.Alm_SubCtaCenCosto,
                                       almacen.Alm_Activo,
                                       sesion.Id_U 
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatAlmacen_Insertar", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void CatAlmacen_Modificar(CatAlmacen almacen, Sesion sesion, ref int Verificador)
        {
            try
            {

                CD_Datos cd_datos = new CD_Datos(sesion.Emp_Cnx);

                String[] Parametros ={
                                        "@Id_Alm",
                                        "@Alm_Clave",
                                        "@Alm_Nombre",
                                        "@Alm_Cuenta",
                                        "@Alm_Subcuenta",
                                        "@Alm_CtaCenCosto",
                                        "@Alm_SubCtaCenCosto",
                                        "@Alm_Activo",
                                 
                                     };
                Object[] Valores = {
                                       almacen.Id_Alm,
                                       almacen.Alm_Clave, 
                                       almacen.Alm_Nombre,
                                       almacen.Alm_CuentaStr, 
                                       almacen.Alm_SubcuentaStr,
                                       almacen.Alm_CtaCenCosto,
                                       almacen.Alm_SubCtaCenCosto,
                                       almacen.Alm_Activo,
                                       sesion.Id_U 
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatAlmacen_Modificar", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void CatAlmacen_Consulta(ref CatAlmacen almacen, int Alm_Clave,Sesion sesion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(sesion.Emp_Cnx);
                SqlDataReader dr = null;

                String[] Parametros = { "@Alm_Clave"};
                Object[] Valores = {Alm_Clave};

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatAlmacen_Consulta", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    almacen.Id_Alm = Convert.ToInt32(dr["Id_Alm"]);
                    almacen.Alm_Clave = Convert.ToInt32(dr["Alm_Clave"]);
                    almacen.Alm_Nombre = dr["Alm_Nombre"].ToString();
                    almacen.Alm_CuentaStr =dr["Alm_Cuenta"].ToString();
                    almacen.Alm_SubcuentaStr = dr["Alm_Subcuenta"].ToString();
                    almacen.Alm_CtaCenCosto = dr["Alm_CtaCenCosto"].ToString();
                    almacen.Alm_SubCtaCenCosto = dr["Alm_SubCtaCenCosto"].ToString();
                    almacen.Alm_Activo = Convert.ToBoolean(dr["Alm_Activo"]);
                }
                dr.Close();

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
 
        }
        public void CatAlmacen_ConsultaLista(ref List<CatAlmacen> List, Sesion sesion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(sesion.Emp_Cnx);
                SqlDataReader dr = null;
                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatAlmacen_ConsultaLista", ref dr);

                CatAlmacen a;

                while (dr.Read())
                {
                    a = new CatAlmacen();
                    a.Id_Alm  = Convert.ToInt32(dr["Id_Alm"]);
                    a.Alm_Clave = Convert.ToInt32(dr["Alm_Clave"]);
                    a.Alm_Nombre = dr["Alm_Nombre"].ToString();
                    a.Alm_CuentaStr = dr["Alm_Cuenta"].ToString();
                    a.Alm_SubcuentaStr = dr["Alm_Subcuenta"].ToString();
                    a.Alm_Activo = Convert.ToBoolean(dr["Alm_Activo"]);

                    List.Add(a);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
