using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_Configuracion
    {
        public void Consulta(ref ConfiguracionGlobal Configuracion, string conexion)
        {
            try
            {
                CapaDatos.CD_Configuracion CD_Configuracion = new CapaDatos.CD_Configuracion();
                CD_Configuracion.Consulta(ref Configuracion, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Modificar(ref ConfiguracionGlobal Configuracion, string conexion)
        {
            try
            {
                CapaDatos.CD_Configuracion CD_Configuracion = new CapaDatos.CD_Configuracion();
                CD_Configuracion.Modificar(ref Configuracion, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
