using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_CapOrdenCompra
    {
        public void ConsultaOrdenCompra(ref OrdenCompra ordenCompra, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ord" };
                object[] Valores = { ordenCompra.Id_Emp, ordenCompra.Id_Cd, ordenCompra.Id_Ord };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapOrdCompra_Consulta", ref dr, Parametros, Valores);
                if (dr.Read())
                {
                    ordenCompra.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    ordenCompra.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    ordenCompra.Id_Ord = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ord")));
                    ordenCompra.Id_Pvd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Pvd"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pvd")));
                    ordenCompra.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    ordenCompra.Ord_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ord_Fecha")));
                    ordenCompra.Ord_Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ord_Tipo")));
                    ordenCompra.Ord_Notas = dr.GetValue(dr.GetOrdinal("Ord_Notas")).ToString();
                    ordenCompra.Ord_Estatus = dr.GetValue(dr.GetOrdinal("Ord_Estatus")).ToString();
                 

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaOrdenCompra_Lista(OrdenCompra ordenCompra, string Conexion, ref List<OrdenCompra> List
            , int Id_Ord_inicio
            , int Id_Ord_fin
            , DateTime Ord_Fecha_inicio
            , DateTime Ord_Fecha_fin
            , string Ord_Estatus)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                          "@Id_Emp"
                                          , "@Id_Cd"
                                          , "@Id_U" 
                                          , "@Id_Ord_inicio"
                                          , "@Id_Ord_fin"
                                          , "@Ord_Fecha_inicio"
                                          , "@Ord_Fecha_fin"
                                          , "@Ord_Estatus"
                                      };
                object[] Valores = { 
                                       ordenCompra.Id_Emp
                                       , ordenCompra.Id_Cd
                                       , ordenCompra.Id_U == -1 ? (object)null : ordenCompra.Id_U 
                                       , Id_Ord_inicio == -1 ? (object)null : Id_Ord_inicio
                                       , Id_Ord_fin == -1 ? (object)null : Id_Ord_fin
                                       , Ord_Fecha_inicio == DateTime.MinValue ? (object)null : Ord_Fecha_inicio
                                       , Ord_Fecha_fin == DateTime.MinValue ? (object)null : Ord_Fecha_fin
                                       , Ord_Estatus == "-1" || Ord_Estatus == string.Empty ? (object)null : Ord_Estatus
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapOrdCompra_Consulta", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    ordenCompra = new OrdenCompra();
                    ordenCompra.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    ordenCompra.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    ordenCompra.Id_Ord = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ord")));
                    ordenCompra.Id_Pvd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Pvd"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Pvd")));
                    ordenCompra.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    ordenCompra.Ord_Nombre_U = dr.GetValue(dr.GetOrdinal("U_Nombre")).ToString();
                    ordenCompra.Pvd_Descripcion = dr.GetValue(dr.GetOrdinal("Pvd_Descripcion")).ToString();
                    ordenCompra.Ord_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ord_Fecha")));
                    ordenCompra.Ord_Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ord_Tipo")));
                    ordenCompra.Ord_Notas = dr.GetValue(dr.GetOrdinal("Ord_Notas")).ToString();
                    ordenCompra.Ord_Estatus = dr.GetValue(dr.GetOrdinal("Ord_Estatus")).ToString();
                    ordenCompra.Ord_EstatusStr = dr.GetValue(dr.GetOrdinal("Ord_EstatusStr")).ToString();
                    ordenCompra.Ord_EstatusEmision =dr.IsDBNull(dr.GetOrdinal("Ord_EstatusEmision"))? -1:   Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ord_EstatusEmision")));
                    if (!dr.IsDBNull(dr.GetOrdinal("Ord_EstatusEmision")))
                    {
                        ordenCompra.Ord_FechaHoraEmision =  dr.GetValue(dr.GetOrdinal("Ord_FechaHoraEmision")).ToString();
                    }

                    List.Add(ordenCompra);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        public void GeneraOrdenCompraAutomatica(string Conexion, ref DataTable dt, string nombreTabla, int Id_Emp, int Id_Cd_Ver, int proveedor, int Id_prd_RI, int Id_prd_RF, bool Prd_Transito_Aplica, int? validador)
        {
            try
            {               
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Emp"
                                          ,"@Id_Cd_Ver"
                                          ,"@proveedor"
                                          ,"@Id_prd_RI"
                                          ,"@Id_prd_RF"
                                          ,"@Prd_Transito_Aplica" 
                                          ,"@Evaluacion"
                                      };
                object[] Valores = { Id_Emp
                                       ,Id_Cd_Ver
                                       ,proveedor
                                       ,Id_prd_RI <= 0 ? (object)null : Id_prd_RI
                                       ,Id_prd_RF <= 0 ? (object)null : Id_prd_RF
                                       ,Prd_Transito_Aplica 
                                       ,validador == null ? (object)null : validador
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProOrdCompra_GenMax_Partidas_Generar", "tabla", ref dt, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void GeneraOrdenCompraAutomatica_Lista(OrdenCompraDet ordenCompra, Sesion sesion, int proveedor, int Id_prd_RI, int Id_prd_RF, bool Prd_Transito_Aplica, ref List<OrdenCompraDet> List)        
        //{
        //    try
        //    {
        //        SqlDataReader dr = null;
        //        CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
        //       string[] Parametros = { "@Id_Emp"
        //                                  ,"@Id_Cd_Ver"
        //                                  ,"@proveedor"
        //                                  ,"@Id_prd_RI"
        //                                  ,"@Id_prd_RF"
        //                                  ,"@Prd_Transito_Aplica" 
        //                              };
        //        object[] Valores = { sesion.Id_Emp
        //                               ,sesion.Id_Cd_Ver
        //                               ,proveedor
        //                               ,Id_prd_RI <= 0 ? (object)null : Id_prd_RI
        //                               ,Id_prd_RF <= 0 ? (object)null : Id_prd_RF
        //                               ,Prd_Transito_Aplica 
        //                           };                                  
        //            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProOrdCompra_GenMax_Partidas_Generar", ref dr, Parametros, Valores);
        //        while (dr.Read())
        //        {
        //            ordenCompra = new OrdenCompraDet();
        //            ordenCompra.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
        //            ordenCompra.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
        //            ordenCompra.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
        //            ordenCompra.Ord_Cantidad = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniEmp"))) ? 1 : (float)(dr.GetValue(dr.GetOrdinal("Prd_UniEmp")));
                  
        //            List.Add(ordenCompra);
        //        }
        //        CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}        
        
        public void InsertarOrdenCompra(ref OrdenCompra ordenCompra, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
                                        "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ord", 
                                        "@Id_Pvd",
                                        "@Id_U",
                                        "@Ord_Fecha",
                                        "@Ord_Tipo",
                                        "@Ord_Notas",
                                        "@Ord_Estatus"
                                      };
                object[] Valores = { 
                                        ordenCompra.Id_Emp
                                        ,ordenCompra.Id_Cd
                                        ,ordenCompra.Id_Ord
                                        ,ordenCompra.Id_Pvd == -1 ? (object)null : ordenCompra.Id_Pvd
                                        ,ordenCompra.Id_U
                                        ,ordenCompra.Ord_Fecha
                                        ,ordenCompra.Ord_Tipo
                                        ,ordenCompra.Ord_Notas
                                        ,ordenCompra.Ord_Estatus
                                   };
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapOrdCompra_Insertar", ref verificador, Parametros, Valores);
                ordenCompra.Id_Ord = verificador; //clave de orden de compra

                string[] ParametrosDet = { 
                                        "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ord", 
                                        "@Id_OrdDet",
                                        "@Id_Prd",
                                        "@Ord_Cantidad",
                                        "@Ord_CantidadGen"
                                      };
                int i = 1;
                foreach (OrdenCompraDet ordenCompraDet in ordenCompra.ListOrdenCompra)
                {
                    ordenCompraDet.Id_OrdDet = i;
                    object[] ValoresDet = { 
                                        ordenCompraDet.Id_Emp
                                        ,ordenCompraDet.Id_Cd
                                        ,ordenCompra.Id_Ord //Id de orden de la tabla de encabezado
                                        ,ordenCompraDet.Id_OrdDet
                                        ,ordenCompraDet.Id_Prd
                                        ,ordenCompraDet.Ord_Cantidad
                                        ,ordenCompraDet.Ord_CantidadGen
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapOrdCompraDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
                    i += 1;
                }
                verificador = ordenCompra.Id_Ord;
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ModificarOrdenCompra(OrdenCompra ordenCompra, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
                                        "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ord", 
                                        "@Id_Pvd",
                                        "@Id_U",
                                        "@Ord_Fecha",
                                        "@Ord_Tipo",
                                        "@Ord_Notas",
                                        "@Ord_Estatus"
                                      };
                object[] Valores = { 
                                        ordenCompra.Id_Emp
                                        ,ordenCompra.Id_Cd
                                        ,ordenCompra.Id_Ord
                                        ,ordenCompra.Id_Pvd == -1 ? (object)null : ordenCompra.Id_Pvd
                                        ,ordenCompra.Id_U
                                        ,ordenCompra.Ord_Fecha
                                        ,ordenCompra.Ord_Tipo
                                        ,ordenCompra.Ord_Notas
                                        ,ordenCompra.Ord_Estatus
                                   };
                // -----------------------------------------------------------------
                // Actualizar encabezado de la orden de compra
                // -----------------------------------------------------------------
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapOrdCompra_Modificar", ref verificador, Parametros, Valores);                
                // -----------------------------------------------------------------
                // Eliminar el detalle de la orden de compra para insertar el nuevo
                // -----------------------------------------------------------------
                string[] ParametrosEliminar = { 
                                        "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ord", 
                                      };
                object[] ValoresEliminar = { 
                                        ordenCompra.Id_Emp
                                        ,ordenCompra.Id_Cd
                                        ,ordenCompra.Id_Ord
                                   };
                sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapOrdCompraDetalle_Eliminar", ref verificador, ParametrosEliminar, ValoresEliminar);
                  
                //parametros de detalle de la orden de compra
                string[] ParametrosDet = { 
                                        "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ord", 
                                        "@Id_OrdDet",
                                        "@Id_Prd",
                                        "@Ord_Cantidad",
                                        "@Ord_CantidadGen"
                                      };
                int i = 1;
                foreach (OrdenCompraDet ordenCompraDet in ordenCompra.ListOrdenCompra)
                {
                    ordenCompraDet.Id_OrdDet = i;
                    object[] ValoresDet = { 
                                        ordenCompraDet.Id_Emp
                                        ,ordenCompraDet.Id_Cd
                                        ,ordenCompra.Id_Ord //Id de orden de la tabla de encabezado
                                        ,ordenCompraDet.Id_OrdDet
                                        ,ordenCompraDet.Id_Prd
                                        ,ordenCompraDet.Ord_Cantidad
                                        ,ordenCompraDet.Ord_CantidadGen
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapOrdCompraDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
                    i += 1;
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

        public void ModificarOrdenCompra_Estatus(OrdenCompra ordenCompra, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
                                        "@Id_Emp" 
                                        ,"@Id_Cd"
	                                    ,"@Id_Ord"
                                        ,"@Ord_Estatus"
                                      };
                object[] Valores = { 
                                        ordenCompra.Id_Emp
                                        ,ordenCompra.Id_Cd
                                        ,ordenCompra.Id_Ord
                                        ,ordenCompra.Ord_Estatus
                                   };
                // -----------------------------------------------------------------
                // Actualizar encabezado de la orden de compra
                // -----------------------------------------------------------------
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapOrdCompra_ModificarEstatus", ref verificador, Parametros, Valores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ModificarOrdenCompra_EstatusEmision(OrdenCompra ordenCompra, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
                                        "@Id_Emp" 
                                        ,"@Id_Cd"
	                                    ,"@Id_Ord"
                                        ,"@Ord_EstatusEmision"
                                        ,"@Evento"
                                      };
                object[] Valores = { 
                                        ordenCompra.Id_Emp
                                        ,ordenCompra.Id_Cd
                                        ,ordenCompra.Id_Ord
                                        ,ordenCompra.Ord_EstatusEmision
                                        ,ordenCompra.Ord_EstatusEmisionStr
                                   };
                // -----------------------------------------------------------------
                // Actualizar encabezado de la orden de compra
                // -----------------------------------------------------------------
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapOrdCompra_ModificarEstatusEmision", ref verificador, Parametros, Valores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void EliminarOrdenCompra(OrdenCompra ordenCompra, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Ord"
                                      };
                object[] Valores = { 
                                       ordenCompra.Id_Emp
                                       ,ordenCompra.Id_Cd
                                       ,ordenCompra.Id_Ord
                                   };
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapOrdCompra_eliminar", ref verificador, Parametros, Valores);
                //ordenCompra.Id_Ord = verificador; //identity de orden de compra 
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
    }
}
