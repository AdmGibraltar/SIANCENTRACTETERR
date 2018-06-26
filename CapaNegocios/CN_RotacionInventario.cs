using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_RotacionInventario
    {
        public void ProVentaNacional_Insertar(List<RotacionInventario> List, ref int Verificador, string Conexion)
        {
            try
            {
                CD_RotacionInventario cd_ri = new CD_RotacionInventario();
                cd_ri.ProVentaNacional_Insertar(List, ref Verificador, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
