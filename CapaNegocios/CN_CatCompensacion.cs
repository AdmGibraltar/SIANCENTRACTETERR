using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatCompensacion
    {
        public void ConsultaConceptos( string Conexion, ref List<CatCompensacion> List)
        {
            try
            {
                CD_CatCompensacion claseCapaDatos = new CD_CatCompensacion();
                claseCapaDatos.ConsultaCatCompensacion(Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarConceptos(CatCompensacion Conceptos, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCompensacion claseCapaDatos = new CD_CatCompensacion();
                claseCapaDatos.InsertarCatCompensacion(Conceptos, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarConceptos(CatCompensacion Conceptos, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCompensacion claseCapaDatos = new CD_CatCompensacion();
                claseCapaDatos.ModificarCatCompensacion(Conceptos, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Maximo(string Conexion, string SP)
        {
            try
            {
                string maximo = "";
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD_CatCompensacion claseCapaDatos = new CapaDatos.CD_CatCompensacion();
                claseCapaDatos.Maximo(SP, Conexion, ref maximo);
                return maximo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarSistemaCompensacion(ref List<SistemaCompensacion> listsistemacompensaciones, ref SistemaCompensacion compensacion, CapaEntidad.Sesion sesion,
          string NombreSistema,  DateTime? FechaIni, DateTime? FechaFin, string Estatus,  int Cd )
        {
            try
            {
                new CD_CatCompensacion().ConsultarSistemaCompensacion(ref listsistemacompensaciones, ref compensacion, sesion, NombreSistema,
                                                         FechaIni, FechaFin, Estatus, Cd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void InsertarConfiguracionSistemacompensacion(SistemaCompensacion confsistcompensacion, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CatCompensacion().InsertarConfiguracionSistemacompensacion(confsistcompensacion, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarConfiguracionSistemacompensacion(SistemaCompensacion confsistcompensacion, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CatCompensacion().ModificarConfiguracionSistemacompensacion(confsistcompensacion, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaConfiguracionSistemacompensacion(SistemaCompensacion confsistcompensacion, string Conexion)
        {
            try
            {
                new CD_CatCompensacion().ConsultaConfiguracionSistemacompensacion(confsistcompensacion, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void CopiarConfiguracionSistemacompensacion(SistemaCompensacion confsistcompensacion, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CatCompensacion().CopiarConfiguracionSistemacompensacion(confsistcompensacion, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        
         public void ReporteComisionesGetXML(SistemaCompensacionGetXML confsistcompensacion, string Conexion)
                {
                    try
                    {
                        new CD_CatCompensacion().ReporteComisionesGetXML(confsistcompensacion, Conexion);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }




         public void ConsultaRepresentantesListado(SistemaCompensacionGetXML confsistcompensacion, string Conexion, ref List<ReporteComisiones> list)
         {
             try
             {
                 new CD_CatCompensacion().ConsultaRepresentantesListado(confsistcompensacion, Conexion, ref list);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

         public void ReporteConcentrado(SistemaCompensacionGetXML confsistcompensacion, string Conexion, ref List<ReporteComisiones> registrorik)
         {
             try
             {
                 new CD_CatCompensacion().ReporteConcentrado(confsistcompensacion, Conexion, ref registrorik);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

         public void ReporteConcentradoFranquicias(SistemaCompensacionGetXML confsistcompensacion, string Conexion, ref List<ReporteComisiones> registrorik)
         {
             try
             {
                 new CD_CatCompensacion().ReporteConcentradoFranquicias(confsistcompensacion, Conexion, ref registrorik);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

         public void ReporteConcentradoComisiones(SistemaCompensacionGetXML confsistcompensacion, int idreporte ,string Conexion, ref List<ReporteComisiones> registrorik)
         {
             try
             {
                 new CD_CatCompensacion().ReporteConcentradoComisiones(confsistcompensacion, idreporte, Conexion, ref registrorik);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

        


         public void ReporteConcentradoFranquiciasDetCliente(SistemaCompensacionGetXML confsistcompensacion, string Conexion, ref List<ReporteComisiones> registrorik)
         {
             try
             {
                 new CD_CatCompensacion().ReporteConcentradoFranquiciasDetCliente(confsistcompensacion, Conexion, ref registrorik);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

         public void ReporteConcentradoFranquiciasDetFactura(SistemaCompensacionGetXML confsistcompensacion, string Conexion, ref List<ReporteComisiones> registrorik)
         {
             try
             {
                 new CD_CatCompensacion().ReporteConcentradoFranquiciasDetFactura(confsistcompensacion, Conexion, ref registrorik);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

         public void ReporteConcentradoFranquiciasDetProducto(SistemaCompensacionGetXML confsistcompensacion, string Conexion, ref List<ReporteComisiones> registrorik)
         {
             try
             {
                 new CD_CatCompensacion().ReporteConcentradoFranquiciasDetProducto(confsistcompensacion, Conexion, ref registrorik);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }




    }
}
