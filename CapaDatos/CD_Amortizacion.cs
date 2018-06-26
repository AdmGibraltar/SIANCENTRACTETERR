using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Amortizacion
    {
        public void ConsultaAmortizacionCliente(Amortizacion amorizacion, string Conexion, ref List<Amortizacion> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte" };
                object[] Valores = { amorizacion.Id_Emp, amorizacion.Id_Cd, amorizacion.Id_Cte };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spAmortizacionCliente_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    amorizacion = new Amortizacion();
                    amorizacion.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    amorizacion.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    amorizacion.Id_Amo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Amo")));
                    amorizacion.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    amorizacion.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    amorizacion.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    amorizacion.Amo_AnioInicio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Amo_AnioInicio")));
                    amorizacion.Amo_AnioFin = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Amo_AnioFin")));
                    amorizacion.Amo_MesInicio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Amo_MesInicio")));
                    amorizacion.Amo_MesFin = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Amo_MesFin")));
                    amorizacion.Amo_MesesAmortiza = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Amo_MesesAmortiza")));
                    amorizacion.Amo_Costo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Amo_Costo")));
                    amorizacion.Amo_Cant = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Amo_Cant")));
                    List.Add(amorizacion);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
