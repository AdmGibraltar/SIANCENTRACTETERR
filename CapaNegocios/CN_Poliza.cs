using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
   public  class CN_Poliza
    {
       public void Poliza_ConsultaLista(int Ano, int Mes, ref List<Poliza> List, Sesion sesion)
       {
           try
           {
               CD_Poliza cd_poliza = new CD_Poliza();
               cd_poliza.Poliza_ConsultaLista( Ano, Mes,ref List, sesion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }

       public void PolizaRev_ConsultaLista(int ano, int mes, ref List<PolizaRev> List, Sesion sesion)
       {
           try
           {
               CD_Poliza cd_poliza = new CD_Poliza();
               cd_poliza.PolizaRev_ConsultaLista(ano, mes, ref List, sesion);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

       public void ProGeneraPolizaRev(int Id_Emp, int anio, int mes, ref int Verificador, string Emp_Cnx)
       {
           try
           {
               CD_Poliza cd_poliza = new CD_Poliza();
               cd_poliza.ProGeneraPolizaRev(Id_Emp, anio, mes, ref Verificador, Emp_Cnx);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
    }
}
