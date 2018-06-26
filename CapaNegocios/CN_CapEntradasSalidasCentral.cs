using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;
namespace CapaNegocios
{
   public  class CN_CapEntradasSalidasCentral
    {
       public void ConsultaLista(EntradasSalidasCentral Es, ref List<EntradasSalidasCentral> List, Sesion sesion)
       {
           try
           {
               CD_CapEntSalCentral cd_es = new CD_CapEntSalCentral();
               cd_es.ConsultaLista(Es, ref  List, sesion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ConsultaEncabezado(int Id_Emp, int Id_Alm, int Id_MovC, int Id_Tm, bool MovC_Naturaleza, ref EntradasSalidasCentral es, Sesion sesion)
       {
           try
           {
               CD_CapEntSalCentral cd_es = new CD_CapEntSalCentral();
               cd_es.ConsultaEncabezado(Id_Emp,Id_Alm, Id_MovC, Id_Tm,MovC_Naturaleza, ref es, sesion);
           }
           catch (Exception ex)
           {

               throw ex;
                   
           }
       }
       public void ConsultaDetalle(int Id_Emp, int Id_Alm, int Id_MovC, int Id_Tm, bool MovC_Naturaleza, ref List<EntradasSalidasCentralDet> List, Sesion sesion)
       {
           try
           {
               CD_CapEntSalCentral cd_es = new CD_CapEntSalCentral();
               cd_es.ConsultaDetalle( Id_Emp, Id_Alm, Id_MovC, Id_Tm,  MovC_Naturaleza, ref  List, sesion);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void ModificarDetalle(List<EntradasSalidasCentralDet> List, Sesion sesion, ref int Verificador)
       {
           try
           {
               CD_CapEntSalCentral cd_es = new CD_CapEntSalCentral();
               cd_es.ModificarDetalle(List,  sesion, ref Verificador);

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
    }
}
