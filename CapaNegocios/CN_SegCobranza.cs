using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_SegCobranza
    {
        public static void GuardarRotacion(List<CapaEntidad.DiasRotacion> List, ref int Verificador, string Conexion)
        {
            try
            {
                CD_SegCobranza.GuardarRotacion(List, Verificador, Conexion);
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        public static void EliminarExistente(int Anio, int Mes, ref int Verificador, string Conexion)
        {
            try
            {
                CD_SegCobranza.EliminarExistente(Anio, Mes, ref Verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
