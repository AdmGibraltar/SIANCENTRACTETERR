using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_InvExcesoInventario
    {
        public string ConsultaNombreEmpresa(Sesion sesion)
        {
            string empresa = string.Empty;
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp" };
                object[] Valores = { sesion.Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spNombreEmpresa", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    empresa = (string)dr.GetValue(dr.GetOrdinal("Emp_Nombre"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return empresa;
        }

        public string ConsultaNombreSucursal(Sesion sesion)
        {
            string sucursal = string.Empty;
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { sesion.Id_Emp, sesion.Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spNombreSucursal", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    sucursal = (string)dr.GetValue(dr.GetOrdinal("Cd_Nombre"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sucursal;
        }

        public void ConsultaInventariosDiarios(string conexion, ref List<InventarioDiario> lisInventarios)
        {
            try
            {
                SqlDataReader dr = null;

                InventarioDiario Inventarios;
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);
                string[] Parametros = { };
                object[] Valores = { };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("sp_ExtraeInvyTransito_Temp", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Inventarios = new InventarioDiario();
                    Inventarios.CDI1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CDI")));
                    Inventarios.SKU1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("SKU")));
                    Inventarios.Cantidad1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cantidad")));
                    Inventarios.Total1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Total")));
                    Inventarios.BackOrder1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("BackOrder")));
                    Inventarios.Transito1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Transito")));

                    lisInventarios.Add(Inventarios);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidosColocados(string conexion, ref List<ReportePedidosRemisiones> lisInventarios, int tiporeporte, int id_cd, DateTime? fecini, DateTime? fecfin, int id_prd, int id_Reporte )
        {
            try
            {
                SqlDataReader dr = null;

                ReportePedidosRemisiones Inventarios;
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);
                string[] Parametros = { "@Id_Cd", "@Es_FecIni", "@Es_FecFin", "@Id_Prd", "@Id_Reporte" };
                object[] Valores = {  id_cd, fecini, fecfin,id_prd, id_Reporte};

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepOrdenCompraColocada", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Inventarios = new ReportePedidosRemisiones();
                    Inventarios.FechaOrdendeCompra1 = Convert.ToString(dr.GetValue(dr.GetOrdinal("fecha")));
                    Inventarios.Remision1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Remision")));
                    Inventarios.CodigoCDI1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CDI")));
                    Inventarios.NombreCDI1 = Convert.ToString(dr.GetValue(dr.GetOrdinal("NombreCDI")));
                    Inventarios.Codigo_SKU1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Codigo_SKU")));
                    Inventarios.Descripcion_SKU1 = Convert.ToString(dr.GetValue(dr.GetOrdinal("Descripcion_SKU")));
                    Inventarios.Presentacion1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Presentacion")));
                    Inventarios.Unidad1 = Convert.ToString(dr.GetValue(dr.GetOrdinal("Unidad")));
                    Inventarios.Cantidad1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cantidad_Ordenada")));
                    Inventarios.Costo_AAA1 = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PrecioAAA")));
                    Inventarios.Importe_Total_AAA1 = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("ImporteAAA")));
                    Inventarios.Num_OrdendeCompra1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Num_OrdendeCompra")));


                    lisInventarios.Add(Inventarios);
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
