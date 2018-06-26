using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;
using System.Data;
namespace CapaNegocios
{
   public class CN_GestionRentabilidad
    {
        
        public void ConsultaGestionRentabilidad_Buscar(GestionRentabilidad gestionRentabilidad, string Conexion, ref List<GestionRentabilidad> List		    
            , int Id_Emp
            , int Id_Cd
            , string Id_Cte
            , string Id_Ter
            , int Id_Rik
            , string NombreCliente
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            , int UBPorCliente
            , int Categorias
            , int UBPorQuimicos
            , int UBPorPapelTradicional
            , int UBPorSistemaDiferenciado
            , int UBPorJarcieria
            , int UBPorAccesorios
            , int UBPorBolsaBasura
            )
        {  
            try
            {
                CD_GestionRentabilidad claseCapaDatos = new CD_GestionRentabilidad();

                claseCapaDatos.ConsultaGestionRentabilidad_Buscar(gestionRentabilidad, Conexion, ref List           
            , Id_Emp
            , Id_Cd
            , Id_Cte
            , Id_Ter
            , Id_Rik
            , NombreCliente
            , MesInicial
            , AnioInicial
            , MesFinal
            , AnioFinal
            , Id_U
            , UBPorCliente
            , Categorias
            , UBPorQuimicos
            , UBPorPapelTradicional
            , UBPorSistemaDiferenciado
            , UBPorJarcieria
            , UBPorAccesorios
            , UBPorBolsaBasura                        
            );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaGestionRentabilidadMonitoreo_Buscar(GestionRentabilidad gestionRentabilidad, string Conexion, ref List<GestionRentabilidad> List
            , int Id_Emp
            , int Id_Cd
            , string Id_Cte
            , string Id_Ter
            , int Id_Rik
            , string NombreCliente
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            )
        {
            try
            {
                CD_GestionRentabilidad claseCapaDatos = new CD_GestionRentabilidad();

                claseCapaDatos.ConsultaGestionRentabilidadMonitoreo_Buscar(gestionRentabilidad, Conexion, ref List
            , Id_Emp
            , Id_Cd
            , Id_Cte
            , Id_Ter
            , Id_Rik
            , NombreCliente
            , MesInicial
            , AnioInicial
            , MesFinal
            , AnioFinal
            , Id_U
            );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void MonitoreoUB_Central(int TipoCD, int MesIni, int AnioIni, int MesFin, int AnioFin, ref List<SegGestionRentabilidad> List, string Conexion)
        {
            try
            {
                CD_GestionRentabilidad cd_gr = new CD_GestionRentabilidad();
                cd_gr.MonitoreoUB_Central(TipoCD, MesIni, AnioIni, MesFin, AnioFin, ref List, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
           

       /*
        public void ConsultaGestionRentabilidad_Buscar(GestionRentabilidad gestionRentabilidad, string p, ref List<GestionRentabilidad> listGestionRentabilidad, int p_2, int p_3, Telerik.Web.UI.RadTextBox radTextBox, Telerik.Web.UI.RadTextBox radTextBox_2, Telerik.Web.UI.RadTextBox radTextBox_3)
        {
            throw new NotImplementedException();
        }*/
    }
}
