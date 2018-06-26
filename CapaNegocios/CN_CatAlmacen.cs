using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
   public  class CN_CatAlmacen
    {
       public void CatAlmacen_Insertar(CatAlmacen almacen, Sesion sesion, ref int Verificador)
       {
           try
           {
               CD_CatAlmacen cd_catalmacen = new CD_CatAlmacen();
               cd_catalmacen.CatAlmacen_Insertar(almacen, sesion, ref Verificador);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void CatAlmacen_Modificar(CatAlmacen almacen, Sesion sesion, ref int Verificador)
       {
           try
           {
               CD_CatAlmacen cd_catalmacen = new CD_CatAlmacen();
               cd_catalmacen.CatAlmacen_Modificar(almacen, sesion, ref Verificador);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public void CatAlmacen_Consulta(ref CatAlmacen almacen, int Alm_Clave, Sesion sesion)
       {
           try
           {
               CD_CatAlmacen cd_catalmacen = new CD_CatAlmacen();
               cd_catalmacen.CatAlmacen_Consulta(ref almacen, Alm_Clave, sesion);

           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public void CatAlmacen_ConsultaLista(ref List<CatAlmacen> List, Sesion sesion)
       {
           try
           {
               CD_CatAlmacen cd_catalmacen = new CD_CatAlmacen();
               cd_catalmacen.CatAlmacen_ConsultaLista(ref  List, sesion);

           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
    }
}
