using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_InvExcesoInventario
    {     
        public string ConsultaNombreEmpresa(Sesion sesion)
        {
            string empresa = string.Empty;
            try
            {
                CD_InvExcesoInventario claseCapaDatos = new CD_InvExcesoInventario();
                empresa = claseCapaDatos.ConsultaNombreEmpresa(sesion);
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
                CD_InvExcesoInventario claseCapaDatos = new CD_InvExcesoInventario();
                sucursal = claseCapaDatos.ConsultaNombreSucursal(sesion);
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
                CD_InvExcesoInventario claseCapaDatos = new CD_InvExcesoInventario();
                claseCapaDatos.ConsultaInventariosDiarios(conexion, ref lisInventarios);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaPedidosColocados(string conexion, ref List<ReportePedidosRemisiones> lisInventarios, int tiporeporte, int id_cd, DateTime? fecini, DateTime? fecfin, int id_prd, int id_Reporte)
        {
            try
            {
                CD_InvExcesoInventario claseCapaDatos = new CD_InvExcesoInventario();
                claseCapaDatos.ConsultaPedidosColocados(conexion, ref lisInventarios, tiporeporte, id_cd, fecini, fecfin, id_prd, id_Reporte);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
