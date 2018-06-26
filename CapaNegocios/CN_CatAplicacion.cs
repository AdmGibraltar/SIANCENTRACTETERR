using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatAplicacion
    {
        public void Lista(Aplicacion aplicacion, string Conexion, ref List<Aplicacion> List)
        {
            try
            {
                CD_CatAplicacion claseCapaDatos = new CD_CatAplicacion();
                claseCapaDatos.Lista(aplicacion, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AplicacionesSegmento_Consultar(int Id_Emp, int Id_Seg, string Conexion, ref List<Aplicacion> List)
        {
            try
            {
                CD_CatAplicacion claseCapaDatos = new CD_CatAplicacion();
                claseCapaDatos.AplicacionesSegmento_Consultar(Id_Emp, Id_Seg, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(Aplicacion aplicacion, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatAplicacion claseCapaDatos = new CD_CatAplicacion();
                claseCapaDatos.Insertar(aplicacion, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(Aplicacion aplicacion, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatAplicacion claseCapaDatos = new CD_CatAplicacion();
                claseCapaDatos.Modificar(aplicacion, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
