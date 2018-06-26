using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CrmOportunidad
    {
        public void ComboSegmento(Sesion sesion, ref List<CrmOportunidades> list)
        {
            try
            {
                CD_CrmOportunidad claseCRM = new CD_CrmOportunidad();
                claseCRM.ComboSegmento(sesion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ComboArea(Sesion sesion, int segmento, ref List<CrmOportunidades> list)
        {
            try
            {
                CD_CrmOportunidad claseCRM = new CD_CrmOportunidad();
                claseCRM.ComboArea(sesion, segmento, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargarSolucion(Sesion sesion, int area, ref List<CrmOportunidades> list)
        {
            try
            {
                CD_CrmOportunidad claseCRM = new CD_CrmOportunidad();
                claseCRM.ConsultaSolucion(sesion, area, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaAplicacion(Sesion sesion, int solucion, ref List<CrmOportunidades> list)
        {
            try
            {
                CD_CrmOportunidad claseCRM = new CD_CrmOportunidad();
                claseCRM.ConsultaAplicacion(sesion, solucion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaVPotencial(Sesion sesion, CrmOportunidades registros, int tipo, ref double VPotencial)
        {
            try
            {
                CD_CrmOportunidad claseCRM = new CD_CrmOportunidad();
                claseCRM.ConsultaVPotencial(sesion, registros, tipo, ref VPotencial);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaVPotencialCliente(Sesion sesion, CrmOportunidades registros, ref double valorTeorico, ref double valorObservado, ref double? Teorico, ref double? Observado)
        {
            try
            {
                CD_CrmOportunidad claseCRM = new CD_CrmOportunidad();
                claseCRM.ConsultaVPotencialCliente(sesion, registros, ref valorTeorico, ref valorObservado, ref  Teorico, ref  Observado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaOportunidad(Sesion sesion, int cd, int idOportunidad, ref List<CrmOportunidades> list)
        {
            try
            {
                CD_CrmOportunidad claseCRM = new CD_CrmOportunidad();
                claseCRM.ConsultaOportunidad(sesion, cd, idOportunidad, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateOportunidad(Sesion sesion, CrmOportunidades registros, ref int validador)
        {
            try
            {
                CD_CrmOportunidad claseCRM = new CD_CrmOportunidad();
                claseCRM.UpdateOportunidad(sesion, registros, ref validador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void tipoUsuario(Sesion sesion, ref string tipoUsuario)
        {
            try
            {
                CD_CrmOportunidad claseCRM = new CD_CrmOportunidad();
                claseCRM.tipoUsuario(sesion, ref tipoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteOportunidad(int Id_emp, int Id_Cd, int Id_Op, string conexion)
        {
            try
            {
                CD_CrmOportunidad claseCRM = new CD_CrmOportunidad();
                claseCRM.DeleteOportunidad(Id_emp, Id_Cd, Id_Op, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
