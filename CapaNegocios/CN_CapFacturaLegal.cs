using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CapFacturaLegal
    {
        public void CapFacturaLegal_Consulta(ref CapFacturaLegal fac, ref int Verificador, int Id_Cd, int Id_Fac, int Tipo,string Conexion)
        {
            try
            {
                CD_CapFacturaLegal cd_fac = new CD_CapFacturaLegal();
                cd_fac.CapFacturaLegal_Consulta(ref  fac, ref  Verificador,  Id_Cd, Id_Fac,Tipo, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void CapFacturaLegal_Guardar(ref CapFacturaLegal fac, ref int Verificador, string Conexion)
        {
            try
            {
                CD_CapFacturaLegal cd_fac = new CD_CapFacturaLegal();
                cd_fac.CapFacturaLegal_Guardar(ref fac, ref Verificador, Conexion);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

    }
}
