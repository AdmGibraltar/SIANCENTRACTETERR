using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_CatTerritorios
    {
        public void ConsultaTerritorios(Territorios territorio, string Conexion, ref List<Territorios> List)
        {
            try
            {
                CD_CatTerritorios claseCapaDatos = new CD_CatTerritorios();
                claseCapaDatos.ConsultaTerritorios(territorio, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarTerritorios(Territorios territorio, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatTerritorios claseCapaDatos = new CD_CatTerritorios();
                claseCapaDatos.InsertarTerritorios(territorio, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarTerritorios(Territorios territorio, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatTerritorios claseCapaDatos = new CD_CatTerritorios();
                claseCapaDatos.ModificarTerritorios(territorio, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaTerritoriosDet(TerritorioDet territorio, string Conexion, ref DataTable dt)
        {
            try
            {
                CD_CatTerritorios claseCapaDatos = new CD_CatTerritorios();
                claseCapaDatos.ConsultaTerritoriosDet(territorio, Conexion, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarTerritoriosDet(Territorios territorio, DataTable dt, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatTerritorios claseCapaDatos = new CD_CatTerritorios();
                claseCapaDatos.InsertarTerritoriosDet(territorio, dt, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarTerritoriosDet(Territorios territorio, DataTable dt, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatTerritorios claseCapaDatos = new CD_CatTerritorios();
                claseCapaDatos.ModificarTerritoriosDet(territorio, dt, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaTerritorios(ref Territorios catterr, string Conexion)
        {
            try
            {
                CD_CatTerritorios claseCapaDatos = new CD_CatTerritorios();
                claseCapaDatos.ConsultaTerritorios(ref catterr, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaTerritoriosCombo(ref Territorios catterr, string Conexion)
        {
            try
            {
                CD_CatTerritorios claseCapaDatos = new CD_CatTerritorios();
                claseCapaDatos.ConsultaTerritoriosCombo(ref catterr, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaTerritorio(ref Territorios catterr, string Conexion)
        {
            try
            {
                CD_CatTerritorios claseCapaDatos = new CD_CatTerritorios();
                claseCapaDatos.ConsultaTerritorio(ref catterr, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerRepPendientesAct(string Conexion, int Id_Emp, int Id_Cd)
        {
            try
            {
                DataTable D = new DataTable();
                CD_CatTerritorios claseCapaDatos = new CD_CatTerritorios();

                claseCapaDatos.ObtenerRepPendientesAct(Conexion, Id_Emp, Id_Cd, ref D);
                return D;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public void ObtenerAutorizacionesPendientesCambioTerritorio(int IdCiudad, int IdEmpresa, string Conexion, ref List<ModelAutorizacionTerritorios> List)
        {
            CD_CatTerritorios claseCapaDatos = new CD_CatTerritorios();
            claseCapaDatos.ConsultaTerritoriosAutorizacionPendientes(IdCiudad, IdEmpresa, Conexion, ref List);
        }

        public void ObtenerAutorizacionesAprobadasCambioTerritorio(int IdCiudad, int IdEmpresa, string Conexion, ref List<ModelAutorizacionTerritorios> List)
        {
            CD_CatTerritorios claseCapaDatos = new CD_CatTerritorios();
            claseCapaDatos.ConsultaTerritoriosAutorizacionAprobadas(IdCiudad, IdEmpresa, Conexion, ref List);
        }

        public void ObtenerAutorizacionesRechazadasCambioTerritorio(int IdCiudad, int IdEmpresa, string Conexion, ref List<ModelAutorizacionTerritorios> List)
        {
            CD_CatTerritorios claseCapaDatos = new CD_CatTerritorios();
            claseCapaDatos.ConsultaTerritoriosAutorizacionRechazar(IdCiudad, IdEmpresa, Conexion, ref List);
        }

        public void AutorizarSolicitudCambioTerritorio(ModelAutorizacionTerritorios DatosSolcitud, int IdUsuario, string Conexion)
        {
            CD_CatTerritorios cd_CatTerritorios = new CD_CatTerritorios();

            ModelAutorizacionTerritorios Datos = new ModelAutorizacionTerritorios();
            Datos.IdAutorizacion = DatosSolcitud.IdAutorizacion;
            Datos.BdName = DatosSolcitud.BdName;
            Datos.IdUAutoriza = IdUsuario;
            int Respuesta = 0;
            cd_CatTerritorios.AutorizarCambioTerritorio(Datos, ref Respuesta, Conexion);
        }




        public void RechazarSolicitudCambioTerritorio(ModelAutorizacionTerritorios DatosAutorizacion, int IdUsuario, string Conexion)
        {
            CD_CatTerritorios cd_CatTerritorios = new CD_CatTerritorios();

            
            ModelAutorizacionTerritorios Datos = new ModelAutorizacionTerritorios();
            Datos.IdAutorizacion = DatosAutorizacion.IdAutorizacion;
            Datos.BdName = DatosAutorizacion.BdName;
            Datos.IdUAutoriza = IdUsuario;
            Datos.Comentario = DatosAutorizacion.Comentario;  
            int Respuesta = 0;
            cd_CatTerritorios.RechazarCambioTerritorio(Datos, ref Respuesta, Conexion);
        }
    }    
    
    
}
