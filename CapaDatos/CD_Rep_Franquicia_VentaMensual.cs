using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Rep_Franquicia_VentaMensual
    {
        public void Rep_Franquicia_VentaMensual(int Anio, string Conexion, ref DataTable Dt)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Anio"};
                object[] Valores = { 
                                       Anio
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("sp_GeneraInfo_RptMensual_Franq", ref dr, Parametros, Valores);

                Dt.Load(dr);
                
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
