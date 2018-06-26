using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CatSolucion
    {
        public void Lista(Solucion area, string Conexion, ref List<Solucion> List)
        {
            try
            {
                CD_CatSolucion claseCapaDatos = new CD_CatSolucion();
                claseCapaDatos.Lista(area, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(Solucion area, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatSolucion claseCapaDatos = new CD_CatSolucion();
                claseCapaDatos.Insertar(area, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(Solucion area, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatSolucion claseCapaDatos = new CD_CatSolucion();
                claseCapaDatos.Modificar(area, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
