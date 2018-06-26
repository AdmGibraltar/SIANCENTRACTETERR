using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_Rep_Franquicia_VentaMensual
    {
            public void Rep_Franquicia_VentaMensual(int Anio, string Conexion, ref DataTable Dt)
            {
                try
                {
                    CD_Rep_Franquicia_VentaMensual CDRep_Franquicia_VentaMensual = new CD_Rep_Franquicia_VentaMensual();

                    CDRep_Franquicia_VentaMensual.Rep_Franquicia_VentaMensual(Anio, Conexion, ref Dt);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        
    }
}
