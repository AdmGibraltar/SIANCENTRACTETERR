using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_Amortizacion
    {
        public void ConsultaAmortizacionCliente(Amortizacion amorizacion, string Conexion, ref List<Amortizacion> List)
        {
            try
            {
                CD_Amortizacion claseCapaDatos = new CD_Amortizacion();
                claseCapaDatos.ConsultaAmortizacionCliente(amorizacion, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
