using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatSegmentos
    {
        public void ConsultaSegmentos(int Id_Emp, string Conexion, ref List<Segmentos> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSegmentos_Consulta", ref dr, Parametros, Valores);

                Segmentos segmento;
                while (dr.Read())
                {
                    segmento = new Segmentos();
                    segmento.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    segmento.Id_Seg = (int)dr.GetValue(dr.GetOrdinal("Id_Seg"));
                    segmento.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Seg_Descripcion"));
                    segmento.Id_UEN = (int)dr.GetValue(dr.GetOrdinal("Id_Uen"));
                    segmento.Unidades = (string)dr.GetValue(dr.GetOrdinal("Seg_Unidades"));
                    segmento.Valor = (double)dr.GetValue(dr.GetOrdinal("Seg_ValUniDim"));
                    segmento.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Seg_Activo")));
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

        public void InsertarSegmentos(Segmentos segmentos, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp",
                                        "@Id_Seg",
	                                    "@Seg_Descripcion", 
	                                    "@Id_Uen", 
	                                    "@Seg_Unidades",
                                        "@Seg_ValUniDim",
                                        "@Id_U",
                                        "@Seg_Activo"
                                      };
                object[] Valores = { 
                                        segmentos.Id_Emp,
                                        segmentos.Id_Seg,
                                        segmentos.Descripcion,
                                        segmentos.Id_UEN,
                                        segmentos.Unidades,
                                        segmentos.Valor,
                                        segmentos.Id_U,
                                        segmentos.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSegmentos_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarSegmentos(Segmentos segmentos, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp",
                                        "@Id_Seg",
                                        "@Id_Seg_Ant",
	                                    "@Seg_Descripcion", 
	                                    "@Id_Uen", 
	                                    "@Seg_Unidades",
                                        "@Seg_ValUniDim",
                                        "@Id_U",
                                        "@Seg_Activo"
                                      };
                object[] Valores = { 
                                        segmentos.Id_Emp,
                                        segmentos.Id_Seg,
                                        segmentos.Id_Seg_Ant,
                                        segmentos.Descripcion,
                                        segmentos.Id_UEN,
                                        segmentos.Unidades,
                                        segmentos.Valor,
                                        segmentos.Id_U,
                                        segmentos.Estatus
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSegmentos_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSegmentos(int Id_Emp, int Id_Seg, string Conexion, ref List<Segmentos> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_seg" };
                object[] Valores = { Id_Emp, Id_Seg };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSegmentos_Consulta", ref dr, Parametros, Valores);

                Segmentos segmento;
                while (dr.Read())
                {
                    segmento = new Segmentos();
                    segmento.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    segmento.Id_Seg = (int)dr.GetValue(dr.GetOrdinal("Id_Seg"));
                    segmento.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Seg_Descripcion"));
                    segmento.Id_UEN = (int)dr.GetValue(dr.GetOrdinal("Id_Uen"));
                    segmento.Unidades = (string)dr.GetValue(dr.GetOrdinal("Seg_Unidades"));
                    segmento.Valor = (double)dr.GetValue(dr.GetOrdinal("Seg_ValUniDim"));
                    segmento.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Seg_Activo")));
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

        public void ConsultaSegmento_Usuario(ref List<Segmentos> list, int Id_Emp, int? Id_U, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_U" };
                object[] Valores = { Id_Emp, Id_U };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSegmentosUsuario_Consultar", ref dr, Parametros, Valores);

                Segmentos segmento;
                while (dr.Read())
                {
                    segmento = new Segmentos();
                    segmento.Id_Seg  = (int)dr.GetValue(dr.GetOrdinal("Id_Seg"));
                    segmento.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Seg_Descripcion"));
                    segmento.Id_U = dr.IsDBNull(dr.GetOrdinal("Id_U")) ? (int?)null : dr.GetInt32(dr.GetOrdinal("Id_U"));
                    list.Add(segmento);
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
