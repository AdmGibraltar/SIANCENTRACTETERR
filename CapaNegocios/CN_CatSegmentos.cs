using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatSegmentos
    {
        public void ConsultaSegmentos(int Id_Emp, string Conexion, ref List<Segmentos> List)
        {
            try
            {
                CD_CatSegmentos claseCapaDatos = new CD_CatSegmentos();
                claseCapaDatos.ConsultaSegmentos(Id_Emp, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarSegmentos(Segmentos segmento, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatSegmentos claseCapaDatos = new CD_CatSegmentos();
                claseCapaDatos.InsertarSegmentos(segmento, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarSegmentos(Segmentos segmento, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatSegmentos claseCapaDatos = new CD_CatSegmentos();
                claseCapaDatos.ModificarSegmentos(segmento, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSegmentos(int Id_Emp, int Id_Seg, string Conexion, ref List<Segmentos> List)
        {
            try
            {
                CD_CatSegmentos claseCapaDatos = new CD_CatSegmentos();
                claseCapaDatos.ConsultaSegmentos(Id_Emp, Id_Seg, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSegmento_Usuario(ref List<Segmentos> list, int Id_Emp, int? Id_U, string Conexion)
        {
            try
            {
                CD_CatSegmentos claseCapaDatos = new CD_CatSegmentos();
                claseCapaDatos.ConsultaSegmento_Usuario(ref list, Id_Emp, Id_U, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
