using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaDatos;
using CapaEntidad;
namespace CapaNegocios
{
    public class CN_BloqueoPeriodo
    {
        public void BloqueoPeriodo_Consultar(int Id_Cd, int Id_Cte, ref BloqueoPeriodo bp, ref  int Verificador,ref int Verificador2, string Conexion)
        {
            try
            {
                CD_BloqueoPeriodo cd_bp = new CD_BloqueoPeriodo();
                cd_bp.BloqueoPeriodo_Consultar( Id_Cd, Id_Cte, ref  bp, ref Verificador, ref Verificador2, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void BloqueoPeriodo_Insertar(BloqueoPeriodo bp, ref int Verificador, string Conexion)
        {
            try
            {
                 CD_BloqueoPeriodo cd_bp = new CD_BloqueoPeriodo();
                 cd_bp.BloqueoPeriodo_Insertar(bp, ref Verificador, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
