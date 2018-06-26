using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_PronosticoCobranza
    {
        public int ValidaDatos(int Mes, int Anio, ref int datos,string Conexion)
        {
            CD_PronosticoCobranza Pron = new CD_PronosticoCobranza();
            return Pron.ValidaDatos(Mes, Anio, datos, Conexion);
        }
    }
}
