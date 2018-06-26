using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_CrmPromocion
    {
        public void ComboCds(Sesion sesion, ref List<CrmPromociones> list)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ComboCds(sesion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ComboRik(Sesion sesion, int cds, ref List<CrmPromociones> list)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ComboRik(sesion, cds, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ComboUen(Sesion sesion, ref List<CrmPromociones> list)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ComboUen(sesion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ComboSegmento(Sesion sesion, int cds, ref List<CrmPromociones> list)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ComboSegmento(sesion, cds, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ComboArea(Sesion sesion, int segmento, ref List<CrmPromociones> list)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ComboArea(sesion, segmento, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargarSolucion(Sesion sesion, int area, ref List<CrmPromociones> list)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ConsultaSolucion(sesion, area, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaAplicacion(Sesion sesion, int solucion, ref List<CrmPromociones> list)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ConsultaAplicacion(sesion, solucion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaCatPromocion(Sesion sesion, CrmPromociones promocion, ref List<CrmPromociones> list)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ConsultaCatPromocion(sesion, promocion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CancelarPromocion(Sesion sesion, int cd, int promocion, ref int validador)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.CancelarPromocion(sesion, cd, promocion, ref validador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaCatClientes(Sesion sesion, int Id_Ter, int Id_UEN, int Id_Rik, int id_Seg, int idCliente, string nombreCliente, ref List<CrmPromociones> List)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ConsultaCatClientes(sesion, Id_Ter, Id_UEN, Id_Rik, id_Seg, idCliente, nombreCliente, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertarOportunidad(Sesion sesion, CRMRegistroProyectos promocion, ref int validador, string aplicaciones)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.InsertarOportunidad(sesion,promocion, ref validador, aplicaciones);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EstructuraSegmento(ref DataSet dsEstructuraSegmento, CrmOportunidades registros, string Conexion)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.EstructuraSegmento(ref dsEstructuraSegmento, registros, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizaDimension(CrmOportunidades registros, string Cnx, ref int verificador)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ActualizaDimension(registros, Cnx, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
