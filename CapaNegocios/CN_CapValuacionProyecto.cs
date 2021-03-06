﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_CapValuacionProyecto
    {
        public void ConsultarUltimaValuacionProyectoCentroDist(ref int verificador, int Id_Cd, string Conexion)
        {
            try
            {
                new CD_CapValuacionProyecto().ConsultarUltimaValuacionProyectoCentroDist(ref verificador, Id_Cd, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaValuacionProyecto_Buscar(ValuacionProyecto valuacionProyecto, ref List<ValuacionProyecto> listaValuacionProyecto, string Conexion
            , int? Id_U
            , string Nombre
            , int? Id_Cte_inicio
            , int? Id_Cte_fin
            , DateTime? Vap_Fecha_inicio
            , DateTime? Vap_Fecha_fin)
        {
            try
            {
                new CD_CapValuacionProyecto().ConsultaValuacionProyecto_Buscar(valuacionProyecto, ref listaValuacionProyecto, Conexion
                    , Id_U
                    , Nombre
                    , Id_Cte_inicio
                    , Id_Cte_fin
                    , Vap_Fecha_inicio
                    , Vap_Fecha_fin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, string Conexion)
        {
            try
            {
                new CD_CapValuacionProyecto().ConsultarValuacionProyecto(ref valuacionProyecto, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarValuacionProyecto_ReporteTotales(ref ValuacionProyecto valuacionProyecto, ref DataTable dt, string Conexion)
        {
            try
            {
                new CD_CapValuacionProyecto().ConsultarValuacionProyecto_ReporteTotales(ref valuacionProyecto, ref dt, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, ValuacionParametros vp, string Conexion, ref int verificador, ValuacionParametrosActual vpactual)
        {
            try
            {
                new CD_CapValuacionProyecto().InsertarValuacionProyecto(ref valuacionProyecto, vp, Conexion, ref verificador, vpactual);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, ValuacionParametros vp, string Conexion, ref int verificador, ValuacionParametrosActual vpactual)
        {
            try
            {
                new CD_CapValuacionProyecto().ModificarValuacionProyecto(ref valuacionProyecto, vp, Conexion, ref verificador, vpactual);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void consultarCondicionesActuales(ref ValuacionParametrosActual vp, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapValuacionProyecto claseCapaDatos = new CD_CapValuacionProyecto();
                claseCapaDatos.consultarParametrosActuales(ref vp, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarValuacionProyecto(ValuacionProyecto valuacionProyecto, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapValuacionProyecto().EliminarValuacionProyecto(valuacionProyecto, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaValuacionProyecto_Autorizacion(ref ValuacionProyecto VP, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapValuacionProyecto claseCapaDatos = new CD_CapValuacionProyecto();
                claseCapaDatos.ConsultaValuacionProyecto_Autorizacion(ref VP, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaValuacionProyectoList(int Id_Emp, int Id_Cd, int Id_Val, string Conexion, ref List<ValuacionProyectoDetalle> List)
        {
            try
            {
                CD_CapValuacionProyecto claseCapaDatos = new CD_CapValuacionProyecto();
                claseCapaDatos.ConsultaValuacionProyectoList(Id_Emp, Id_Cd, Id_Val, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ModificarValuacionProyectoDetalle(ValuacionProyectoDetalle cl, List<ValuacionProyectoDetalle> list, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapValuacionProyecto claseCapaDatos = new CD_CapValuacionProyecto();
                claseCapaDatos.ModificarValuacionProyectoDetalle(cl, list, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void consultarCondiciones(ref ValuacionParametros vp, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapValuacionProyecto claseCapaDatos = new CD_CapValuacionProyecto();
                claseCapaDatos.consultarParametros(ref vp, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void consultarCondicionesCentro(ref ValuacionParametros vp, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapValuacionProyecto claseCapaDatos = new CD_CapValuacionProyecto();
                claseCapaDatos.consultarCondicionesCentro(ref vp, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
