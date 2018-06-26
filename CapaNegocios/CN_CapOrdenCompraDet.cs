using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CapOrdenCompraDet
    {
        public void ConsultaOrdenCompraDetalle_Lista(OrdenCompraDet ordenCompraDet, string Conexion, ref List<OrdenCompraDet> List)
        {
            try
            {
                CD_CapOrdenCompraDet claseCapaDatos = new CD_CapOrdenCompraDet();
                claseCapaDatos.ConsultaOrdenCompraDetalle_Lista(ordenCompraDet, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
