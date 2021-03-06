﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CrmOportunidad
    {
        public void ComboSegmento(Sesion sesion, ref List<CrmOportunidades> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id1", "@Id2" };
                object[] Valores = { 1, sesion.Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSegmentos_Combo", ref dr, Parametros, Valores);

                CrmOportunidades catOportunidad;
                while (dr.Read())
                {
                    catOportunidad = new CrmOportunidades();
                    catOportunidad.Id = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    catOportunidad.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    list.Add(catOportunidad);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ComboArea(Sesion sesion, int segmento, ref List<CrmOportunidades> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id1", "@Id2", "@Id3" };
                object[] Valores = { 1, sesion.Id_Emp, segmento };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAreaSegmento_Combo", ref dr, Parametros, Valores);

                CrmOportunidades catOportunidad;
                while (dr.Read())
                {
                    catOportunidad = new CrmOportunidades();
                    catOportunidad.Id = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    catOportunidad.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    list.Add(catOportunidad);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaSolucion(Sesion sesion, int area, ref List<CrmOportunidades> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Area" };
                object[] Valores = { sesion.Id_Emp, area };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCatSoluciones_Combo", ref dr, Parametros, Valores);

                CrmOportunidades catOportunidad;
                while (dr.Read())
                {
                    catOportunidad = new CrmOportunidades();
                    catOportunidad.Id = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    catOportunidad.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    List.Add(catOportunidad);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaAplicacion(Sesion sesion, int solucion, ref List<CrmOportunidades> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Sol" };
                object[] Valores = { sesion.Id_Emp, solucion };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCatAplicacion_Combo", ref dr, Parametros, Valores);

                CrmOportunidades catOportunidad;
                while (dr.Read())
                {
                    catOportunidad = new CrmOportunidades();
                    catOportunidad.Id = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    catOportunidad.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    List.Add(catOportunidad);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaVPotencial(Sesion sesion, CrmOportunidades registros, int tipo, ref double VPotencial)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Area", "@Id_Sol", "@Id_Apl", "@Tipo" };
                object[] Valores = { sesion.Id_Emp, registros.ID_Area, registros.Id_Sol, registros.Id_Apl, tipo };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSolucion_ValorPotencial", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    VPotencial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ValPotencial"))) ? 0.00 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ValPotencial")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaVPotencialCliente(Sesion sesion, CrmOportunidades registros, ref double valorTeorico, ref double valorObservado, ref double? Teorico, ref double? Observado)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Sol", "@Id_Apl" };
                object[] Valores = { sesion.Id_Emp, sesion.Id_Cd_Ver, registros.Id_Sol, registros.Id_Apl };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("sp_VPObservadoCliente", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    valorTeorico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("valorTeorico"))) ? 0.00 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("valorTeorico")));
                    //valorObservado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("valorObservado"))) ? 0.00 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("valorObservado")));
                    //Teorico = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Teorico"))) ? (double?)null : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Teorico")));
                    //Observado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Observado"))) ? (double?)null : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Observado")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaOportunidad(Sesion sesion, int cd, int idOportunidad, ref List<CrmOportunidades> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd",                                   
                                        "@Id_Op"
                                      };
                object[] Valores = { sesion.Id_Emp, 
                                     cd,
                                     idOportunidad
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCrmOportunidades_Consulta", ref dr, Parametros, Valores);
                CrmOportunidades catOportunidad;
                while (dr.Read())
                {
                    catOportunidad = new CrmOportunidades();
                    catOportunidad.Id_Op = (int)dr.GetValue(dr.GetOrdinal("Id_Op"));
                    catOportunidad.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    catOportunidad.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Cte_Nombre"));
                    catOportunidad.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    catOportunidad.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    catOportunidad.Id_Uen = (int)dr.GetValue(dr.GetOrdinal("Id_Uen"));
                    catOportunidad.Id_Seg = (int)dr.GetValue(dr.GetOrdinal("Id_Seg"));
                    catOportunidad.Id_Usu = (int)dr.GetValue(dr.GetOrdinal("Id_Usu"));
                    catOportunidad.ID_Area = (int)dr.GetValue(dr.GetOrdinal("ID_Area"));
                    catOportunidad.Id_Apl = (int)dr.GetValue(dr.GetOrdinal("Id_Apl"));
                    catOportunidad.Id_Sol = (int)dr.GetValue(dr.GetOrdinal("Id_Sol"));
                    catOportunidad.ValorPotencialT = (double)dr.GetValue(dr.GetOrdinal("Apl_Potencial"));
                    catOportunidad.VentaNoRepetitiva = (bool)dr.GetValue(dr.GetOrdinal("VentaNoRepetitiva"));
                    catOportunidad.Productos = (string)dr.GetValue(dr.GetOrdinal("Productos"));
                    catOportunidad.Comentarios = (string)dr.GetValue(dr.GetOrdinal("Comentarios"));
                    catOportunidad.Analisis = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Analisis"))) ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Analisis"))).ToString("dd/MM/yyyy");
                    catOportunidad.Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Presentacion"))) ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Presentacion"))).ToString("dd/MM/yyyy");
                    catOportunidad.Negociacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Negociacion"))) ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Negociacion"))).ToString("dd/MM/yyyy");
                    catOportunidad.Cierre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cierre"))) ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Cierre"))).ToString("dd/MM/yyyy");
                    catOportunidad.FechaCancelacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FechaCancelacion"))) ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaCancelacion"))).ToString("dd/MM/yyyy");
                    catOportunidad.FechaCotizacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FechaCotizacion"))) ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaCotizacion"))).ToString("dd/MM/yyyy");
                    catOportunidad.Cancelacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelacion"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cancelacion"));
                    catOportunidad.VentaMensual = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("VentaMensual"))) ? 0.00 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VentaMensual")));
                    catOportunidad.MontoProyecto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("MontoProyecto"))) ? 0.00 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("MontoProyecto")));
                    catOportunidad.Competidor = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Competidor"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Competidor"));
                    catOportunidad.Avances = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Avances"))) ? 0 : (int)dr.GetValue(dr.GetOrdinal("Avances"));
                    catOportunidad.Mes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Mes"))) ? 0 : (int)dr.GetValue(dr.GetOrdinal("Mes"));
                    catOportunidad.Año = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Año"))) ? 0 : (int)dr.GetValue(dr.GetOrdinal("Año"));
                    catOportunidad.Id_Causa = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Causa"))) ? 0 : (int)dr.GetValue(dr.GetOrdinal("Id_Causa"));
                    catOportunidad.Estatus = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Estatus"))) ? 0 : (int)dr.GetValue(dr.GetOrdinal("Estatus"));
                    catOportunidad.Id_Cam = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cam"))) ? 0 : (int)dr.GetValue(dr.GetOrdinal("Id_Cam"));
                    catOportunidad.Campania = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Campania"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Campania"));
                    List.Add(catOportunidad);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateOportunidad(Sesion sesion, CrmOportunidades registros, ref int validador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                int venta = 0;
                if (registros.VentaNoRepetitiva)
                    venta = 1;

                registros.DPresentacion = !string.IsNullOrEmpty(registros.Presentacion) ? Convert.ToDateTime(registros.Presentacion) : DateTime.MinValue;
                registros.DNegociacion = !string.IsNullOrEmpty(registros.Negociacion) ? Convert.ToDateTime(registros.Negociacion) : DateTime.MinValue;
                registros.DCierre = !string.IsNullOrEmpty(registros.Cierre) ? Convert.ToDateTime(registros.Cierre) : DateTime.MinValue;
                registros.DFechaCotizacion = !string.IsNullOrEmpty(registros.FechaCotizacion) ? Convert.ToDateTime(registros.FechaCotizacion) : DateTime.MinValue;
                registros.DCancelacion = !string.IsNullOrEmpty(registros.Cancelacion) ? Convert.ToDateTime(registros.Cancelacion) : DateTime.MinValue;

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd",                                   
                                        "@Id_Op",  
                                        "@Id_Apl",
                                        "@Productos",
                                        "@VentaNoRepetitiva",
                                        "@Comentarios",                                       
                                        "@Presentacion",
                                        "@Negociacion",
                                        "@Cierre",
                                        "@FechaCotizacion",
                                        "@VentaMensual",
                                        "@FechaCancelacion",
                                        "@Competidor",
                                        "@MontoProyecto",
                                        "@Avances",
                                        "@Id_Causa",
                                        "@Estatus",
                                        "@ComentarioCancel",
                                        "@Id_Cam"
                                      };
                object[] Valores = { sesion.Id_Emp,
                                     registros.Id_Cd,
                                     registros.Id_Op,   
                                     registros.Id_Apl,
                                     registros.Productos,
                                     venta,
                                     registros.Comentarios,                                     
                                     registros.DPresentacion == DateTime.MinValue? (object)null : registros.DPresentacion,                                
                                     registros.DNegociacion == DateTime.MinValue? (object)null : registros.DNegociacion,
                                     registros.DCierre == DateTime.MinValue? (object)null : registros.DCierre,
                                     registros.DFechaCotizacion == DateTime.MinValue? (object)null : registros.DFechaCotizacion,
                                     registros.VentaMensual,                                    
                                     registros.DCancelacion == DateTime.MinValue? (object)null : registros.DCancelacion,
                                     registros.Competidor,
                                     registros.MontoProyecto,
                                     registros.Avances,
                                     registros.Id_Causa,
                                     registros.Estatus,
                                     registros.ComentariosCancel,
                                     registros.Id_Cam
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCrmOportunidades_Update", ref validador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void tipoUsuario(Sesion sesion, ref string tipoUsuario)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp",                                
                                        "@Id_TU"
                                      };
                object[] Valores = { sesion.Id_Emp, 
                                     sesion.Id_TU
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCrmOportunidades_ConsultaTipoUsuario", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    CrmOportunidades catOportunidad = new CrmOportunidades();
                    tipoUsuario = (string)dr.GetValue(dr.GetOrdinal("Tu_Descripcion"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteOportunidad(int Id_Emp, int Id_Cd, int Id_Op, string conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                string[] Parametros = { "@Id_Emp",                                
                                        "@Id_Cd",
                                        "@Id_Op"
                                      };
                object[] Valores = {
                                     Id_Emp, 
                                     Id_Cd,
                                     Id_Op
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpCrmOportunidades_Eliminar", ref dr, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
