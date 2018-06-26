using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

using System.Data;

namespace CapaDatos
{
    public class CD_CapValuacionProyecto
    {


        public void consultarParametrosActuales(ref ValuacionParametrosActual vp, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                verificador = 0;
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Vap" };
                object[] Valores = { vp.Id_Emp, vp.Id_Cd, vp.Id_Vap };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyecto_ConsultaParametros", ref dr, Parametros, Valores);
                //ValuacionProyectoDetalle vpd = default(ValuacionProyectoDetalle);
                if (dr.HasRows)
                {
                    dr.Read();
                    vp.txtCuentasPorCobrar = (double)dr.GetValue(dr.GetOrdinal("txtCuentasPorCobrar"));
                    vp.txtInventario = (double)dr.GetValue(dr.GetOrdinal("txtInventario"));
                    vp.txtGastosServirCliente = (double)dr.GetValue(dr.GetOrdinal("txtGastosServirCliente"));
                    vp.txtVigencia = (double)dr.GetValue(dr.GetOrdinal("txtVigencia"));
                    vp.txtFleteLocales = (double)dr.GetValue(dr.GetOrdinal("txtFleteLocales"));
                    vp.txtIsr = (double)dr.GetValue(dr.GetOrdinal("txtIsr"));
                    vp.txtCetes = (double)dr.GetValue(dr.GetOrdinal("txtCetes"));
                    vp.txtFinanciamientoproveedores = (double)dr.GetValue(dr.GetOrdinal("txtFinanciamientoproveedores"));
                    vp.txtInversionactivosfijos = (double)dr.GetValue(dr.GetOrdinal("txtInversionactivosfijos"));
                    vp.txtCostodecapital = (double)dr.GetValue(dr.GetOrdinal("txtCostodecapital"));
                    vp.txtManoObra = (double)dr.GetValue(dr.GetOrdinal("txtManoObra"));


                    verificador = 1;


                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarUltimaValuacionProyectoCentroDist(ref int verificador, int Id_Cd, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Cd" };
                object[] Valores = { Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(
                    "spCapValuacionProyectosCantidadEnCd_Consultar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaValuacionProyecto_Buscar(ValuacionProyecto valuacionProyecto, ref List<ValuacionProyecto> listaValuacionProyecto, string Conexion
            , int? Id_U
            , string Nombre
            , int? Id_Cte_inicio
            , int? Id_Cte_fin
            , DateTime? Vap_Fecha_inicio
            , DateTime? Vap_Fecha_fin)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_U"
                                          ,"@Nombre"
                                          ,"@Id_Cte_inicio"
                                          ,"@Id_Cte_fin"
                                          ,"@Vap_Fecha_inicio"
                                          ,"@Vap_Fecha_fin" 
                                          ,"@Vap_Estatus"
                                      };
                object[] Valores = { 
                                       valuacionProyecto.Id_Emp
                                       ,valuacionProyecto.Id_Cd
                                       ,Id_U
                                       ,Nombre
                                       ,Id_Cte_inicio
                                       ,Id_Cte_fin
                                       ,Vap_Fecha_inicio
                                       ,Vap_Fecha_fin
                                       ,valuacionProyecto.Vap_Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyecto_Buscar", ref dr, Parametros, Valores);
                listaValuacionProyecto = new List<ValuacionProyecto>();
                while (dr.Read())
                {
                    valuacionProyecto = new ValuacionProyecto();
                    valuacionProyecto.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    valuacionProyecto.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    valuacionProyecto.Id_Vap = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Vap")));
                    valuacionProyecto.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    valuacionProyecto.Vap_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Vap_Fecha")));
                    valuacionProyecto.Cte_NomComercial = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    valuacionProyecto.Vap_Nota = dr.IsDBNull(dr.GetOrdinal("Vap_Nota")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Vap_Nota")).ToString();
                    valuacionProyecto.Vap_Estatus = dr.GetValue(dr.GetOrdinal("Vap_Estatus")).ToString();
                    valuacionProyecto.Vap_Usuario = dr.GetValue(dr.GetOrdinal("U_Nombre")).ToString();
                    listaValuacionProyecto.Add(valuacionProyecto);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Vap"
                                      };
                object[] Valores = { 
                                       valuacionProyecto.Id_Emp
                                       ,valuacionProyecto.Id_Cd
                                       ,valuacionProyecto.Id_Vap
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyecto_Consultar", ref dr, Parametros, Valores);
                NotaCargo fac = new NotaCargo();
                if (dr.HasRows)
                {
                    dr.Read();
                    valuacionProyecto.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    valuacionProyecto.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    valuacionProyecto.Id_Vap = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Vap")));
                    valuacionProyecto.Vap_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Vap_Fecha")));
                    valuacionProyecto.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    valuacionProyecto.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    valuacionProyecto.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Vap_CteNombre")).ToString();
                    valuacionProyecto.Vap_Nota = dr.IsDBNull(dr.GetOrdinal("Vap_Nota")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Vap_Nota")).ToString();
                    valuacionProyecto.Vap_Estatus = dr.GetValue(dr.GetOrdinal("Vap_Estatus")).ToString();
                    valuacionProyecto.Vap_CteNombre = dr.GetValue(dr.GetOrdinal("Vap_CteNombre")).ToString();
                }

                dr.Close();
                valuacionProyecto.ListaProductosValuacionProyecto = new List<ValuacionProyectoDetalle>();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyectoDetalle_Consultar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    ValuacionProyectoDetalle valuacionProyectoDetalle = new ValuacionProyectoDetalle();
                    valuacionProyectoDetalle.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    valuacionProyectoDetalle.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    valuacionProyectoDetalle.Id_Vap = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Vap")));
                    valuacionProyectoDetalle.Id_VapDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_VapDet")));
                    valuacionProyectoDetalle.Vap_Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Tipo")));
                    valuacionProyectoDetalle.Vap_TipoStr = dr.GetValue(dr.GetOrdinal("Vap_TipoStr")).ToString();

                    valuacionProyectoDetalle.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    valuacionProyectoDetalle.Producto = new Producto();
                    valuacionProyectoDetalle.Producto.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    valuacionProyectoDetalle.Producto.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    valuacionProyectoDetalle.Producto.Prd_Presentacion = dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString();
                    if (dr.IsDBNull(dr.GetOrdinal("Prd_UniNs")))
                        valuacionProyectoDetalle.Producto.Prd_UniNs = null;
                    else
                        valuacionProyectoDetalle.Producto.Prd_UniNs = dr.GetValue(dr.GetOrdinal("Prd_UniNs")).ToString();

                    valuacionProyectoDetalle.Vap_Cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Cantidad")));
                    valuacionProyectoDetalle.Vap_Costo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Costo")));
                    valuacionProyectoDetalle.Vap_Precio = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Precio")));
                    valuacionProyectoDetalle.Estatus = dr.GetValue(dr.GetOrdinal("Det_Estatus")).ToString();
                    valuacionProyectoDetalle.Vap_PrecioEspecial = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_PrecioEspecial")));
                    valuacionProyecto.ListaProductosValuacionProyecto.Add(valuacionProyectoDetalle);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarValuacionProyecto_ReporteTotales(ref ValuacionProyecto valuacionProyecto, ref DataTable dt, string Conexion)
        {
            try
            {
                //SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Vap"
                                      };
                object[] Valores = { 
                                       valuacionProyecto.Id_Emp
                                       ,valuacionProyecto.Id_Cd
                                       ,valuacionProyecto.Id_Vap
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyectoDetalle_ConsultarReporteValuacionProy_Totales", "tabla", ref dt, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, ValuacionParametros vp, string Conexion, ref int verificador, ValuacionParametrosActual vpactual)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] Parametros = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Vap"		
                                        ,"@Vap_Fecha"		
                                        ,"@Id_U"			
                                        ,"@Id_Cte"		
                                        ,"@Vap_Nota"		
                                        ,"@Vap_Estatus"
                                        ,"@Vap_CteNombre"
                                      };
                object[] Valores = { 
                                        valuacionProyecto.Id_Emp			
                                        ,valuacionProyecto.Id_Cd			
                                        ,valuacionProyecto.Id_Vap		
                                        ,valuacionProyecto.Vap_Fecha		
                                        ,valuacionProyecto.Id_U			
                                        ,valuacionProyecto.Id_Cte		
                                        ,valuacionProyecto.Vap_Nota		
                                        ,valuacionProyecto.Vap_Estatus	
                                        ,valuacionProyecto.Vap_CteNombre
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyecto_Insertar", ref verificador, Parametros, Valores);
                valuacionProyecto.Id_Vap = verificador; //clave de val. proyecto

                // INSERTAR PARAMETROS
                Parametros = new string[] { 
                                            "@Id_Emp", 
                                            "@Id_Cd", 
                                            "@Id_Vap" , 
                                            "@Vap_Vigencia" ,  
                                            "@Vap_Participacion" , 
                                            "@Vap_Mano_Obra" , 
                                            "@Vap_Amortizacion" , 
                                            "@Vap_Numero_Entregas" , 
                                            "@Vap_Costo_Entregas" , 
                                            "@Vap_Comision_Factoraje" , 
                                            "@Vap_Comision_Anden" , 
                                            "@Vap_Gasto_Flete_Locales" , 
                                            "@Vap_IVA" , 
                                            "@Vap_Plazo_Pago_Cliente" , 
                                            "@Vap_Inventario_Key" , 
                                            "@Vap_Inventario_Consignacion" , 
                                            "@Vap_Inventario_Papel" , 
                                            "@Vap_Consignacion_Papel" , 
                                            "@Vap_Credito_Key" , 
                                            "@Vap_Credito_Papel" , 
                                            "@Vap_ISR" , 
                                            "@Vap_Ucs" , 
                                            "@Vap_Cetes" , 
                                            "@Vap_Adicional_Cetes" , 
                                            "@Vap_Costos_Fijos_No_Papel" , 
                                            "@Vap_Costos_Fijos_Papel" , 
                                            "@Vap_Gastos_Admin" , 
                                            "@Vap_Inversion_Activos" ,
                                            "@txtCuentasPorCobrar" , 
                                            "@txtInventario" , 
                                            "@txtGastosServirCliente" , 
                                            "@txtVigencia" , 
                                            "@txtFleteLocales" , 
                                            "@txtIsr" , 
                                            "@txtCetes" , 
                                            "@txtFinanciamientoproveedores" , 
                                            "@txtInversionactivosfijos" , 
                                            "@txtCostodecapital" , 
                                            "@txtManoObra" 
                                        };

                Valores = new object[] { 
                                            vp.Id_Emp,
                                            vp.Id_Cd,
                                            valuacionProyecto.Id_Vap,
                                            vp.Vap_Vigencia ,
                                            vp.Vap_Participacion ,
                                            vp.Vap_Mano_Obra ,
                                            vp.Vap_Amortizacion ,
                                            vp.Vap_Numero_Entregas ,
                                            vp.Vap_Costo_Entregas ,
                                            vp.Vap_Comision_Factoraje ,
                                            vp.Vap_Comision_Anden ,
                                            vp.Vap_Gasto_Flete_Locales ,
                                            vp.Vap_IVA ,
                                            vp.Vap_Plazo_Pago_Cliente ,
                                            vp.Vap_Inventario_Key ,
                                            vp.Vap_Inventario_Consignacion ,
                                            vp.Vap_Inventario_Papel ,
                                            vp.Vap_Consignacion_Papel ,
                                            vp.Vap_Credito_Key ,
                                            vp.Vap_Credito_Papel ,
                                            vp.Vap_ISR ,
                                            vp.Vap_Ucs ,
                                            vp.Vap_Cetes ,
                                            vp.Vap_Adicional_Cetes ,
                                            vp.Vap_Costos_Fijos_No_Papel ,
                                            vp.Vap_Costos_Fijos_Papel ,
                                            vp.Vap_Gastos_Admin ,
                                            vp.Vap_Inversion_Activos,
                                            vpactual.txtCuentasPorCobrar,
                                            vpactual.txtInventario,
                                            vpactual.txtGastosServirCliente, 
                                            vpactual.txtVigencia, 
                                            vpactual.txtFleteLocales, 
                                            vpactual.txtIsr, 
                                            vpactual.txtCetes, 
                                            vpactual.txtFinanciamientoproveedores, 
                                            vpactual.txtInversionactivosfijos, 
                                            vpactual.txtCostodecapital, 
                                            vpactual.txtManoObra 

                                          };
                verificador = 0;
                sqlcmd = CapaDatos.GenerarSqlCommand("CapValProyectoParams_Insertar", ref verificador, Parametros, Valores);


                // -----------------------------------------------------------------
                // Insertar detalle 
                // -----------------------------------------------------------------
                string[] ParametrosDet = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Vap"
                                        ,"@Id_VapDet"
                                        ,"@Vap_Tipo"
                                        ,"@Id_Prd"
                                        ,"@Vap_Cantidad"
                                        ,"@Vap_Costo"
                                        ,"@Vap_Precio"	
                                        ,"@Vap_PrecioLista"
                                      };
                int i = 1;
                foreach (ValuacionProyectoDetalle ValuacionProyectoDetalle in valuacionProyecto.ListaProductosValuacionProyecto)
                {
                    ValuacionProyectoDetalle.Id_VapDet = i;
                    object[] ValoresDet = { 
                                        ValuacionProyectoDetalle.Id_Emp
                                        ,ValuacionProyectoDetalle.Id_Cd
                                        ,ValuacionProyectoDetalle.Id_Vap
                                        ,ValuacionProyectoDetalle.Id_VapDet
                                        ,ValuacionProyectoDetalle.Vap_Tipo
                                        ,ValuacionProyectoDetalle.Id_Prd
                                        ,ValuacionProyectoDetalle.Vap_Cantidad
                                        ,ValuacionProyectoDetalle.Vap_Costo
                                        ,ValuacionProyectoDetalle.Vap_Precio
                                        ,ValuacionProyectoDetalle.Vap_PrecioEspecial
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyectoDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
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

        public void ModificarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, ValuacionParametros vp, string Conexion, ref int verificador, ValuacionParametrosActual vpactual)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] Parametros = { 
                                        "@Id_Emp"			
                                        ,"@Id_Cd"			
                                        ,"@Id_Vap"		
                                        ,"@Vap_Fecha"		
                                        ,"@Id_U"			
                                        ,"@Id_Cte"		
                                        ,"@Vap_Nota"		
                                        ,"@Vap_Estatus"
                                        ,"@txtCuentasPorCobrar"
                                        ,"@txtInventario"
                                        ,"@txtGastosServirCliente"
                                        ,"@txtVigencia"
                                        ,"@txtFleteLocales"
                                        ,"@txtIsr"
                                        ,"@txtCetes"
                                        ,"@txtFinanciamientoproveedores"
                                        ,"@txtInversionactivosfijos"
                                        ,"@txtCostodecapital"
                                        ,"@txtManoObra"


                                      };
                object[] Valores = { 
                                        valuacionProyecto.Id_Emp			
                                        ,valuacionProyecto.Id_Cd			
                                        ,valuacionProyecto.Id_Vap		
                                        ,valuacionProyecto.Vap_Fecha		
                                        ,valuacionProyecto.Id_U			
                                        ,valuacionProyecto.Id_Cte		
                                        ,valuacionProyecto.Vap_Nota		
                                        ,valuacionProyecto.Vap_Estatus	
                                        ,vpactual.txtCuentasPorCobrar
                                        ,vpactual.txtInventario
                                        ,vpactual.txtGastosServirCliente
                                        ,vpactual.txtVigencia
                                        ,vpactual.txtFleteLocales
                                        ,vpactual.txtIsr
                                        ,vpactual.txtCetes
                                        ,vpactual.txtFinanciamientoproveedores
                                        ,vpactual.txtInversionactivosfijos
                                        ,vpactual.txtCostodecapital
                                        ,vpactual.txtManoObra
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyecto_Modificar", ref verificador, Parametros, Valores);
                //valuacionProyecto.Id_Vap = verificador; //clave de val. proyecto

                // INSERTAR PARAMETROS
                Parametros = new string[] { 
                                            "@Id_Emp", 
                                            "@Id_Cd", 
                                            "@Id_Vap" , 
                                            "@Vap_Vigencia" ,  
                                            "@Vap_Participacion" , 
                                            "@Vap_Mano_Obra" , 
                                            "@Vap_Amortizacion" , 
                                            "@Vap_Numero_Entregas" , 
                                            "@Vap_Costo_Entregas" , 
                                            "@Vap_Comision_Factoraje" , 
                                            "@Vap_Comision_Anden" , 
                                            "@Vap_Gasto_Flete_Locales" , 
                                            "@Vap_IVA" , 
                                            "@Vap_Plazo_Pago_Cliente" , 
                                            "@Vap_Inventario_Key" , 
                                            "@Vap_Inventario_Consignacion" , 
                                            "@Vap_Inventario_Papel" , 
                                            "@Vap_Consignacion_Papel" , 
                                            "@Vap_Credito_Key" , 
                                            "@Vap_Credito_Papel" , 
                                            "@Vap_ISR" , 
                                            "@Vap_Ucs" , 
                                            "@Vap_Cetes" , 
                                            "@Vap_Adicional_Cetes" , 
                                            "@Vap_Costos_Fijos_No_Papel" , 
                                            "@Vap_Costos_Fijos_Papel" , 
                                            "@Vap_Gastos_Admin" , 
                                            "@Vap_Inversion_Activos" 
                                        };

                Valores = new object[] { 
                                            vp.Id_Emp,
                                            vp.Id_Cd,
                                            valuacionProyecto.Id_Vap,
                                            vp.Vap_Vigencia ,
                                            vp.Vap_Participacion ,
                                            vp.Vap_Mano_Obra ,
                                            vp.Vap_Amortizacion ,
                                            vp.Vap_Numero_Entregas ,
                                            vp.Vap_Costo_Entregas ,
                                            vp.Vap_Comision_Factoraje ,
                                            vp.Vap_Comision_Anden ,
                                            vp.Vap_Gasto_Flete_Locales ,
                                            vp.Vap_IVA ,
                                            vp.Vap_Plazo_Pago_Cliente ,
                                            vp.Vap_Inventario_Key ,
                                            vp.Vap_Inventario_Consignacion ,
                                            vp.Vap_Inventario_Papel ,
                                            vp.Vap_Consignacion_Papel ,
                                            vp.Vap_Credito_Key ,
                                            vp.Vap_Credito_Papel ,
                                            vp.Vap_ISR ,
                                            vp.Vap_Ucs ,
                                            vp.Vap_Cetes ,
                                            vp.Vap_Adicional_Cetes ,
                                            vp.Vap_Costos_Fijos_No_Papel ,
                                            vp.Vap_Costos_Fijos_Papel ,
                                            vp.Vap_Gastos_Admin ,
                                            vp.Vap_Inversion_Activos
                                          };
                verificador = 0;
                sqlcmd = CapaDatos.GenerarSqlCommand("CapValProyectoParams_Modificar", ref verificador, Parametros, Valores);

                // -----------------------------------------------------------------
                // Insertar detalle 
                // -----------------------------------------------------------------
                string[] ParametrosDet = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Vap"
                                        ,"@Id_VapDet"
                                        ,"@Vap_Tipo"
                                        ,"@Id_Prd"
                                        ,"@Vap_Cantidad"
                                        ,"@Vap_Costo"
                                        ,"@Vap_Precio"	
                                        ,"@Vap_PrecioLista"
                                      };
                int i = 1;
                foreach (ValuacionProyectoDetalle ValuacionProyectoDetalle in valuacionProyecto.ListaProductosValuacionProyecto)
                {
                    ValuacionProyectoDetalle.Id_VapDet = i;
                    object[] ValoresDet = { 
                                        ValuacionProyectoDetalle.Id_Emp
                                        ,ValuacionProyectoDetalle.Id_Cd
                                        ,ValuacionProyectoDetalle.Id_Vap
                                        ,ValuacionProyectoDetalle.Id_VapDet
                                        ,ValuacionProyectoDetalle.Vap_Tipo
                                        ,ValuacionProyectoDetalle.Id_Prd
                                        ,ValuacionProyectoDetalle.Vap_Cantidad
                                        ,ValuacionProyectoDetalle.Vap_Costo
                                        ,ValuacionProyectoDetalle.Vap_Precio
                                        ,ValuacionProyectoDetalle.Vap_PrecioEspecial//Vap_PrecioEspecial
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyectoDetalle_Insertar", ref verificador, ParametrosDet, ValoresDet);
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

        public void EliminarValuacionProyecto(ValuacionProyecto valuacionProyecto, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Vap"
                                      };
                object[] Valores = { 
                                       valuacionProyecto.Id_Emp
                                       ,valuacionProyecto.Id_Cd
                                       ,valuacionProyecto.Id_Vap
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapValProyecto_Eliminar", ref verificador, Parametros, Valores);
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

        public void ConsultaValuacionProyecto_Autorizacion(ref ValuacionProyecto VP, string Conexion, ref int verificador)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd",
                                        "@Id_Vap"
                                      };
                object[] Valores = { 
                                       VP.Id_Emp, 
                                       VP.Id_Cd,
                                       VP.Id_Vap == 0?(object)null: VP.Id_Vap,
                                       VP.Vap_Estatus == ""? (object)null : VP.Vap_Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spValProyectoAutorizacion_Consulta", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {//agregue procedure.. revisar si se necesita agregar a la tabla el campo fecha autorizacion.. y en que tabla?
                    //Id_Cd  Cd_Nombre	Id_U	U_Nombre	Id_Vap	Vap_Fecha	           Vap_Nota	Id_Emp	Vap_Estatus	Vap_Sustituida
                    dr.Read();
                    VP.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")).ToString());
                    VP.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")).ToString());
                    VP.Cd_Nombre = dr.GetValue(dr.GetOrdinal("Cd_Nombre")).ToString();
                    VP.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    VP.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("U_Nombre")).ToString();
                    VP.Id_Vap = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Vap")).ToString());
                    VP.Vap_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Vap_Fecha")).ToString());
                    VP.Vap_FechaStr = dr.IsDBNull(dr.GetOrdinal("Vap_Fecha")) ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Vap_Fecha"))).ToString("dd/MM/yyyy hh:mm:ss tt");
                    VP.Vap_Nota = dr.GetValue(dr.GetOrdinal("Vap_Nota")).ToString();
                    VP.Vap_Estatus = (dr.GetValue(dr.GetOrdinal("Vap_Estatus")).ToString());
                    VP.Vap_Sustituida = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Sustituida")).ToString());
                    VP.Vap_FechaAutStr = dr.IsDBNull(dr.GetOrdinal("Vap_FechaAut")) ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Vap_FechaAut"))).ToString("dd/MM/yyyy hh:mm:ss tt");
                    verificador = 1;
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaValuacionProyectoList(int Id_Emp, int Id_Cd, int Id_Val, string Conexion, ref List<ValuacionProyectoDetalle> List)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Val" };
                object[] Valores = { Id_Emp, Id_Cd, Id_Val };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spValuacionProyectoDet_Consultar", ref dr, Parametros, Valores);
                ValuacionProyectoDetalle vpd = default(ValuacionProyectoDetalle);
                while (dr.Read())
                {
                    vpd = new ValuacionProyectoDetalle();
                    vpd.Id_VapDet = dr.GetInt32(dr.GetOrdinal("Id_VapDet"));
                    vpd.Vap_Tipo = dr.GetInt32(dr.GetOrdinal("Vap_Tipo"));
                    vpd.Vap_TipoStr = dr.IsDBNull(dr.GetOrdinal("Tipo")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Tipo")));
                    vpd.Prd_Descripcion = dr.IsDBNull(dr.GetOrdinal("Prd_Descripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Descripcion")));
                    vpd.Prd_Presentacion = dr.IsDBNull(dr.GetOrdinal("Prd_Presentacion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Presentacion")));
                    vpd.Prd_UniNe = dr.IsDBNull(dr.GetOrdinal("Prd_UniNe")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_UniNe")));
                    vpd.Vap_Cantidad = dr.IsDBNull(dr.GetOrdinal("Vap_Cantidad")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Cantidad")));
                    vpd.Vap_Costo = dr.IsDBNull(dr.GetOrdinal("Vap_Costo")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Costo")));
                    vpd.Vap_Precio = dr.IsDBNull(dr.GetOrdinal("Vap_Precio")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Precio")));
                    vpd.Autorizado = dr.GetValue(dr.GetOrdinal("Det_Estatus")).ToString().ToUpper() == "A" ? true : false;
                    vpd.Rechazado = dr.GetValue(dr.GetOrdinal("Det_Estatus")).ToString().ToUpper() == "R" ? true : false;
                    vpd.Det_FecAut = dr.IsDBNull(dr.GetOrdinal("Det_FecAut")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Det_FecAut")));
                    vpd.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    vpd.Estatus = dr.IsDBNull(dr.GetOrdinal("Det_Estatus")) ? "" : dr.GetValue(dr.GetOrdinal("Det_Estatus")).ToString();
                    List.Add(vpd);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarValuacionProyectoDetalle(ValuacionProyectoDetalle cl, List<ValuacionProyectoDetalle> list, string Conexion, ref int verificador)
        {
            CD_Datos CapaDatos = default(CD_Datos);
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros;
                object[] Valores;
                int valor = 0;
                int idEmp = 0;
                int idCd = 0;
                int idVal = 0;
                int idUsu = 0;
                int cantidad = list.Count;
                string estatus = string.Empty;
                CapaDatos.StartTrans();
                //Guardar los Detalles
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Vap", "@Id_VapDet", "@Det_Estatus", "@Det_FecAut", "@Det_Autorizo" };
                foreach (ValuacionProyectoDetalle ValuacionProyecto in list)
                {
                    Valores = new object[] {
                                            cl.Id_Emp,
                                            cl.Id_Cd,
                                            cl.Id_Vap,
                                            ValuacionProyecto.Id_VapDet,
                                            ValuacionProyecto.Estatus,
                                            ValuacionProyecto.Det_FecAut==null ? (object)null: Convert.ToDateTime(ValuacionProyecto.Det_FecAut),                                           
                                            ValuacionProyecto.Id_U
                    };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spValProyectoDet_Modificar", ref verificador, Parametros, Valores);

                    if (ValuacionProyecto.Estatus == "A")
                        valor++;
                    idEmp = cl.Id_Emp;
                    idCd = cl.Id_Cd;
                    idVal = cl.Id_Vap;
                    idUsu = cl.Id_U;
                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                if (valor == cantidad)
                    estatus = "A";
                else
                    if (valor == 0)
                        estatus = "R";
                    else
                        estatus = "P";

                ModificarValuacionProyecto_Aut(idEmp, idCd, idVal, idUsu, estatus, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
        
        public void ModificarValuacionProyecto_Aut(int emp, int cd, int val, int usu, string estatus, string Conexion, ref int verificador)
        {
            CD_Datos CapaDatos = default(CD_Datos);
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros;
                object[] Valores;               
                CapaDatos.StartTrans();
                //Guardar los Detalles
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Vap", "@Det_Estatus", "@Det_Autorizo" };
               
                    Valores = new object[] {
                                            emp,
                                            cd,
                                            val,                                           
                                            estatus,                                         
                                            usu
                    };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spValProyecto_Modificar", ref verificador, Parametros, Valores);

                             
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);          
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void consultarParametros(ref ValuacionParametros vp, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Vap" };
                object[] Valores = { vp.Id_Emp, vp.Id_Cd, vp.Id_Vap };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spValuacionProyectoParametros_Consultar", ref dr, Parametros, Valores);
                //ValuacionProyectoDetalle vpd = default(ValuacionProyectoDetalle);
                if (dr.HasRows)
                {
                    dr.Read();
                    vp.Vap_Vigencia = (int)dr.GetValue(dr.GetOrdinal("Vap_Vigencia"));
                    vp.Vap_Participacion = (double)dr.GetValue(dr.GetOrdinal("Vap_Participacion"));
                    vp.Vap_Mano_Obra = (double)dr.GetValue(dr.GetOrdinal("Vap_Mano_Obra"));
                    vp.Vap_Amortizacion = (double)dr.GetValue(dr.GetOrdinal("Vap_Amortizacion"));
                    vp.Vap_Numero_Entregas = (int)dr.GetValue(dr.GetOrdinal("Vap_Numero_Entregas"));
                    vp.Vap_Costo_Entregas = (double)dr.GetValue(dr.GetOrdinal("Vap_Costo_Entregas"));
                    vp.Vap_Comision_Factoraje = (double)dr.GetValue(dr.GetOrdinal("Vap_Comision_Factoraje"));
                    vp.Vap_Comision_Anden = (double)dr.GetValue(dr.GetOrdinal("Vap_Comision_Anden"));
                    vp.Vap_Gasto_Flete_Locales = (double)dr.GetValue(dr.GetOrdinal("Vap_Gasto_Flete_Locales"));
                    vp.Vap_IVA = (double)dr.GetValue(dr.GetOrdinal("Vap_IVA"));
                    vp.Vap_Plazo_Pago_Cliente = (int)dr.GetValue(dr.GetOrdinal("Vap_Plazo_Pago_Cliente"));
                    vp.Vap_Inventario_Key = (int)dr.GetValue(dr.GetOrdinal("Vap_Inventario_Key"));
                    vp.Vap_Inventario_Consignacion = (int)dr.GetValue(dr.GetOrdinal("Vap_Inventario_Consignacion"));
                    vp.Vap_Inventario_Papel = (int)dr.GetValue(dr.GetOrdinal("Vap_Inventario_Papel"));
                    vp.Vap_Consignacion_Papel = (int)dr.GetValue(dr.GetOrdinal("Vap_Consignacion_Papel"));
                    vp.Vap_Credito_Key = (int)dr.GetValue(dr.GetOrdinal("Vap_Credito_Key"));
                    vp.Vap_Credito_Papel = (int)dr.GetValue(dr.GetOrdinal("Vap_Credito_Papel"));
                    vp.Vap_ISR = (double)dr.GetValue(dr.GetOrdinal("Vap_ISR"));
                    vp.Vap_Ucs = (double)dr.GetValue(dr.GetOrdinal("Vap_Ucs"));
                    vp.Vap_Cetes = (double)dr.GetValue(dr.GetOrdinal("Vap_Cetes"));
                    vp.Vap_Adicional_Cetes = (double)dr.GetValue(dr.GetOrdinal("Vap_Adicional_Cetes"));
                    vp.Vap_Costos_Fijos_No_Papel = (double)dr.GetValue(dr.GetOrdinal("Vap_Costos_Fijos_No_Papel"));
                    vp.Vap_Costos_Fijos_Papel = (double)dr.GetValue(dr.GetOrdinal("Vap_Costos_Fijos_Papel"));
                    vp.Vap_Gastos_Admin = (double)dr.GetValue(dr.GetOrdinal("Vap_Gastos_Admin"));
                    vp.Vap_Inversion_Activos = (int)dr.GetValue(dr.GetOrdinal("Vap_Inversion_Activos"));
                    verificador = 1;

                     
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void consultarCondicionesCentro(ref ValuacionParametros vp, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd"  };
                object[] Valores = { vp.Id_Emp, vp.Id_Cd  };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spValCondicionesCentro_Consultar", ref dr, Parametros, Valores);
                //ValuacionProyectoDetalle vpd = default(ValuacionProyectoDetalle);
                if (dr.HasRows)
                {
                    dr.Read();
                    vp.Vap_Vigencia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Vigencia")));
                    vp.Vap_Participacion = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Participacion")));
                    vp.Vap_Mano_Obra = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Mano_Obra")));
                    vp.Vap_Amortizacion = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Amortizacion")));
                    vp.Vap_Numero_Entregas = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Numero_Entregas")));
                    vp.Vap_Costo_Entregas = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Costo_Entregas")));
                    vp.Vap_Comision_Factoraje = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Comision_Factoraje")));
                    vp.Vap_Comision_Anden = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Comision_Anden")));
                    vp.Vap_Gasto_Flete_Locales = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Gasto_Flete_Locales")));
                    vp.Vap_IVA = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_IVA")));
                    vp.Vap_Plazo_Pago_Cliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Plazo_Pago_Cliente")));
                    vp.Vap_Inventario_Key = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Inventario_Key")));
                    vp.Vap_Inventario_Consignacion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Inventario_Consignacion")));
                    vp.Vap_Inventario_Papel = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Inventario_Key")));
                    vp.Vap_Consignacion_Papel = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Inventario_Consignacion")));
                    vp.Vap_Credito_Key = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Credito_Key")));
                    vp.Vap_Credito_Papel = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Credito_Papel")));
                    vp.Vap_ISR = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_ISR")));
                    vp.Vap_Ucs = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Ucs")));
                    vp.Vap_Cetes = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Cetes")));
                    vp.Vap_Adicional_Cetes = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Adicional_Cetes")));
                    vp.Vap_Costos_Fijos_No_Papel = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Costos_Fijos_No_Papel")));
                    vp.Vap_Costos_Fijos_Papel = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Costos_Fijos_Papel")));
                    vp.Vap_Gastos_Admin = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vap_Gastos_Admin")));
                    vp.Vap_Inversion_Activos = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Inversion_Activos")));
                    vp.Cd_ComisionRik = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_ComisionRik")));
                    vp.Cd_FactorConvActFijo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_FactorConvActFijo")));
                    vp.Cd_DiasFinanciaProv= Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_DiasFinanciaProv")));
                    vp.Cd_TasaIncCostoCapital = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_TasaIncCostoCapital")));
                    verificador = 1;
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
