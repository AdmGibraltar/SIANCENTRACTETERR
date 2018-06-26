using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_ClientesMayorAdeudo
    {
        public static void RegresaAdeudoCliente(DateTime FechaCierre, DateTime FechaCorte, int DiasRevision, int Id_Cte, string Conexion, ref double PorPagar, ref double Pagado)
        {
            CD_ClienteMayorAdeudo.RegresaAdeudoCliente(FechaCierre,FechaCorte, DiasRevision,Id_Cte, Conexion, ref PorPagar, ref Pagado);
        }

        public static void RegresaCuentasPagadas(DateTime FechaCierre, int DiasRevision, int Id_Cte, string Conexion, ref List<CobSaldosNiveles> Lista)
        {
            CD_ClienteMayorAdeudo.RegresaCuentasPagadas(FechaCierre, DiasRevision, Id_Cte, Conexion, ref Lista);
        }

        public static void RegresaCuentasPorPagadas(DateTime FechaCierre, int DiasRevision, int Id_Cte, string Conexion, ref List<CobSaldosNiveles> Lista)
        {
            CD_ClienteMayorAdeudo.RegresaCuentasPorPagadas(FechaCierre, DiasRevision, Id_Cte, Conexion, ref Lista);
        }
    }
}
