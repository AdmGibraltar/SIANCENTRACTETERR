using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatArea
    {

        public void Lista(Area area, string Conexion, ref List<Area> List)
        {
            try
            {
                CD_CatArea claseCapaDatos = new CD_CatArea();
                claseCapaDatos.Lista(area, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(Area area, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatArea claseCapaDatos = new CD_CatArea();
                claseCapaDatos.Insertar(area, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(Area area, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatArea claseCapaDatos = new CD_CatArea();
                claseCapaDatos.Modificar(area, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void Borrar(Area area, ref int verificador, string Conexion)
        {
            try
            {
                CD_CatArea claseCapaDatos = new CD_CatArea();
                claseCapaDatos.Borrar(area, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
