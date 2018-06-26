using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Data;
using System.Data.SqlClient;

namespace CapaNegocios
{
    public class CN_CatAgrupador
    {
        public void CatAgrupador_ConsultaLista(ref List<Agrupador> List, String Conexion)
        {
            try
            {
                CD_CatAgrupador cd_Ag = new CD_CatAgrupador();
                cd_Ag.CatAgrupador_ConsultaLista(ref List, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void CatAgrupador_Guardar(Agrupador ag, ref int Verificador, string Conexion)
        {
            try
            {
                CD_CatAgrupador cd_Ag = new CD_CatAgrupador();
                cd_Ag.CatAgrupador_Guardar( ag, ref Verificador,  Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void CatAgrupador_Modificar(Agrupador ag, ref int Verificador, string Conexion)
        {
            try
            {
                CD_CatAgrupador cd_Ag = new CD_CatAgrupador();
                cd_Ag.CatAgrupador_Modificar(ag, ref Verificador, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void CatAgrupador_CteConsultar(int Id_Cte, int Id_Cd,ref int Verificador,ref Agrupador Ag, string Conexion)
        {
            try
            {
                CD_CatAgrupador cd_ag = new CD_CatAgrupador();
                cd_ag.CatAgrupador_CteConsultar( Id_Cte,  Id_Cd, ref Verificador,ref Ag, Conexion);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void CatAgrupador_CteInsertar(Agrupador Ag, ref int Verificador, string Conexion)
        {
            try
            {
                CD_CatAgrupador cd_ag = new CD_CatAgrupador();
                cd_ag.CatAgrupador_CteInsertar(Ag, ref Verificador, Conexion);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void CapClienteBloque_Consultar(int Id_Cte, int Id_Cd, ref int Verificador,ref int Verificador2, ref Agrupador Ag, string Conexion)
        {
            try
            {
                CD_CatAgrupador cd_ag = new CD_CatAgrupador();
                cd_ag.CapClienteBloque_Consultar(Id_Cte, Id_Cd, ref Verificador, ref Verificador2,ref  Ag,  Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void CapClienteBloque_CteInsertar(Agrupador Ag, ref int Verificador, string Conexion)
        {
            try
            {
                 CD_CatAgrupador cd_ag = new CD_CatAgrupador();
                cd_ag.CapClienteBloque_CteInsertar(Ag, ref  Verificador, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
