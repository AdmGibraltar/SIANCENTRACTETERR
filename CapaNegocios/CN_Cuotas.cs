using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_Cuotas
    {
        public void CatCuotasCRM_EliminarExistente(int Anio, int Mes, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Cuotas cd_c = new CD_Cuotas();
                cd_c.CatCuotasCRM_EliminarExistente(Anio, Mes, ref  Verificador, Conexion);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void GuardarCuotas(List<Cuotas> List, ref int Verificador, string Conexion)
        {
            try
            {
                CD_Cuotas cd_c = new CD_Cuotas();
                cd_c.GuardarCuotas(List, ref  Verificador, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
