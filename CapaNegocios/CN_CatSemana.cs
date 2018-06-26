using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatSemana
    {
        public void ConsultaSemana(ref Semana _semana, int Cal_Año, int Cal_Mes, Sesion sesion, ref List<Semana> list)
        {
            try
            {
                CD_CatSemana cd_catsemana = new CD_CatSemana();
                cd_catsemana.ConsultaSemana(ref _semana, Cal_Año, Cal_Mes, sesion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarSemana(ref List<Semana> semana, string Conexion, ref int verificador, bool actualizar)
        {
            try
            {
                CD_CatSemana cd_catSemana = new CD_CatSemana();
                cd_catSemana.GuardarSemana(ref semana, Conexion, ref verificador, actualizar);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarSemana(int Id_Cal, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatSemana cd_catSemana = new CD_CatSemana();
                cd_catSemana.EliminarSemana(Id_Cal, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaSemana(ref Semana semana, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatSemana cd_catSemana = new CD_CatSemana();
                cd_catSemana.ConsultaSemana(ref semana, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSemanaActual(ref Semana semana, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatSemana cd_catSemana = new CD_CatSemana();
                cd_catSemana.ConsultaSemanaActual(ref semana, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
