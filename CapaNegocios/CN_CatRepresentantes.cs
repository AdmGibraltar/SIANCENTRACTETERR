using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using Telerik.Web.UI;

namespace CapaNegocios
{
    public class CN_CatRepresentantes
    {
        public void ConsultarRepresentantes(Representantes representantes, string Conexion, ref List<Representantes> List)
        {
            try
            {
                CD_CatRepresentantes claseCapaDatos = new CD_CatRepresentantes();
                claseCapaDatos.ConsultarRepresentantes(representantes, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarRepresentantes(Representantes representantes, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatRepresentantes claseCapaDatos = new CD_CatRepresentantes();
                claseCapaDatos.InsertarRepresentantes(representantes, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarRepresentantes(Representantes representantes, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatRepresentantes claseCapaDatos = new CD_CatRepresentantes();
                claseCapaDatos.ModificarRepresentantes(representantes, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void InsertarRepresentantesDet(Representantes representante, List<Comun> lc, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatRepresentantes claseCapaDatos = new CD_CatRepresentantes();
                claseCapaDatos.InsertarRepresentantesDet(representante, lc, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarRepresentantesDet(RikUen representante, string Conexion, ref List<RikUen> lc)
        {
            try
            {
                CD_CatRepresentantes claseCapaDatos = new CD_CatRepresentantes();
                claseCapaDatos.ConsultarRepresentantesDet(representante, Conexion, ref lc);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ActualizaDatosRik(List<Representantes> List, ref int Verificador, string Conexion)
        {
            try
            {
                CD_CatRepresentantes cd_re = new CD_CatRepresentantes();
                cd_re.ActualizaDatosRik(List, ref Verificador, Conexion);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
