using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaDatos;

namespace CapaNegocios
{
    public class CD_PronosticoCobranza
    {
        public int ValidaDatos(int Mes, int Anio, int datos, string Conexion)
        {
            try 
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Mes","@Anio"};
                object[] Valores = { Mes,Anio};

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(
                    "spValidarDatosRotacion", ref datos, Parametros, Valores);

                return datos;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
