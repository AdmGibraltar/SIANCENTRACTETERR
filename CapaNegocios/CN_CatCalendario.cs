using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatCalendario
    {
        public void ConsultaCalendario(ref Calendario calendario, int año, CapaEntidad.Sesion sesion, ref List<Calendario> list)
        {
            try
            {
                CD_CatCalendario cal = new CD_CatCalendario();
                cal.ConsultaCalendario(ref calendario, año, sesion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCalendarioUltimaFecha(ref Calendario calendario, int año, CapaEntidad.Sesion sesion)
        {
            try
            {
                CD_CatCalendario cal = new CD_CatCalendario();
                cal.ConsultaCalendarioUltimaFecha(ref calendario, año, sesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarCalendario(ref List<Calendario> calendarios, string Conexion, ref int verificador, bool actualizar)
        {
            try
            {
                CD_CatCalendario cal = new CD_CatCalendario();
                cal.GuardarCalendario(ref calendarios, Conexion, ref verificador, actualizar);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarCalendario(int Id_Cal, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCalendario cd = new CD_CatCalendario();
                cd.EliminarCalendario(Id_Cal, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarCalendarioAño(int Cal_Año, Sesion session, ref int verificador)
        {
            try
            {
                CD_CatCalendario cd = new CD_CatCalendario();
                cd.EliminarCalendarioAño(Cal_Año, session, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void VerificaCalendario(ref Calendario calendario, int año,int Cal_Mes, Sesion sesion, ref List<Calendario> list)
        {
            try
            {
                CD_CatCalendario cal = new CD_CatCalendario();
                cal.VerificaCalendario(ref calendario, año,Cal_Mes, sesion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaCalendarioActual(ref Calendario calendario, Sesion sesion)
        {
            try
            {
                CD_CatCalendario cal = new CD_CatCalendario();
                cal.ConsultaCalendarioActual(ref calendario, sesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPeriodo(ref Calendario Cal, Sesion sesion)
        {
            try
            {
                CD_CatCalendario cd_Cal = new CD_CatCalendario();
                cd_Cal.ConsultaPeriodo(ref  Cal, sesion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

    }
}
