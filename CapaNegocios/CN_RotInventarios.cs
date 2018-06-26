using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_RotInventarios
    {
        public void ProSaldoFinal_Inicializa(int Anio, int Mes, ref int Verificador, string Conexion)
        {
            try
            {
                CD_RotInventarios cd_ri = new CD_RotInventarios();
                cd_ri.ProSaldoFinal_Inicializa(Anio, Mes, ref Verificador, Conexion);


            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public void ProIndicadoresMensuales_Insertar(List<ProIndicadoresMensuales> List, ref int Verificador, string Conexion)
        {
            try
            {
                CD_RotInventarios cd_ri = new CD_RotInventarios();
                cd_ri.ProIndicadoresMensuales_Insertar( List, ref Verificador,  Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void ProObjetivoRotacion_Consultar(int Id_Cd, ref ProIndicadoresMensuales pi, string Conexion)
        {
            try
            {
                CD_RotInventarios cd_ri = new CD_RotInventarios();
                cd_ri.ProObjetivoRotacion_Consultar(Id_Cd, ref pi, Conexion);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void ProObjetivoRotacionDiario_Consultar(int Id_Cd, ref ProIndicadoresMensuales pi, string Conexion)
        {
            try
            {
                CD_RotInventarios cd_ri = new CD_RotInventarios();
                cd_ri.ProObjetivoRotacionDiario_Consultar(Id_Cd, ref pi, Conexion);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ProObjetivoRotacion_Modificar(ProIndicadoresMensuales pi, ref int Verificador, string Conexion)
        {
            try
            {
                CD_RotInventarios cd_ri = new CD_RotInventarios();
                cd_ri.ProObjetivoRotacion_Modificar( pi, ref  Verificador,  Conexion);

            }
            catch (Exception ex)
            {

                throw ex;
            }
 
        }
        public void ProObjetivoRotacionDiario_Modificar(ProIndicadoresMensuales pi, ref int Verificador, string Conexion)
        {
            try
            {
                CD_RotInventarios cd_ri = new CD_RotInventarios();
                cd_ri.ProObjetivoRotacionDiario_Modificar(pi, ref  Verificador, Conexion);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void RotacionInventariosMensual_Valida(int Anio, int Mes, ref int GeneroPoliza, ref int SubioIndicadores, ref int GeneroSaldos, string Conexion)
        {

            try
            {
                CD_RotInventarios cd_ri = new CD_RotInventarios();
                cd_ri.RotacionInventariosMensual_Valida(Anio, Mes, ref GeneroPoliza, ref SubioIndicadores, ref GeneroSaldos, Conexion);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
