using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_CatCliente
    {
        public void ConsultarClienteSigCentroDist(ref int verificador, int Id_Emp, int Id_Cd_Ver, string Conexion)
        {
            try
            {
                new CD_CatCliente().ConsultarClienteSigCentroDist(ref verificador, Id_Emp, Id_Cd_Ver, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarClientes(Clientes clientes, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.InsertarClientes(clientes, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarClientes(Clientes clientes, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.ModificarClientes(clientes, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClientes(ref Clientes cte, string Conexion)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.ConsultarCliente(cte, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClienteDet(ClienteDet clientedet, string Conexion, ref DataTable dt)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.ConsultarClienteDet(clientedet, Conexion, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void ConsultaExisteClienteDet(int Id_Ter, int Id_Seg, Sesion sesion, ref int validador)
        //{
        //    try
        //    {
        //        CD_CatCliente claseCapaDatos = new CD_CatCliente();
        //        claseCapaDatos.ConsultaExisteClienteDet(Id_Ter, Id_Seg, sesion, ref validador);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        public void InsertarClientesDet(Clientes clientes, DataTable dt, string Conexion)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.InsertarClienteDet(clientes, dt, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Recibe un id de cliente, sesion(id_emp,id_cd_ver) y devulve una lista de los territorios a los que pertenece 
        /// </summary>
        /// <param name="id_cliente"></param>
        /// <param name="sesion"></param>
        /// <param name="territorios"></param>
        public void ConsultaTerritoriosDelCliente(int id_cliente, Sesion sesion, ref List<Territorios> territorios)
        {
            try
            {
                new CD_CatCliente().ConsultaTerritoriosDelCliente(id_cliente, sesion, ref territorios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Recibe un id de cliente, sesion(id_emp,id_cd_ver) y devulve una lista de los territorios a los que pertenece 
        /// </summary>
        /// <param name="id_cliente"></param>
        /// <param name="sesion"></param>
        /// <param name="territorios"></param>
        public void ConsultaTodosTerritoriosDelCliente(int id_cliente, Sesion sesion, ref List<Territorios> territorios)
        {
            try
            {
                new CD_CatCliente().ConsultaTodosTerritoriosDelCliente(id_cliente, sesion, ref territorios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ReporteRentabilidad_ConsultarTotales(int Id_Emp, int Id_Cd_Ver, int Id_Cte, int? Id_Ter, string periodo, string ventas, ref DataTable dt, string Conexion)
        {
            try
            {
                new CD_CatCliente().ReporteRentabilidad_ConsultarTotales(Id_Emp, Id_Cd_Ver, Id_Cte, Id_Ter, periodo, ventas, ref dt, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Lista(Clientes cte, string Conexion, ref List<Clientes> List)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.Lista(cte, ref List, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClienteTerritorio(ref Clientes cte, string Conexion)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultaClienteTerritorio(ref cte, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPermisosUEN(ref DataTable dt, string Conexion)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.ConsultaPermisosUEN(ref dt, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaTipoCDC(int Id_Cd_ver, ref int Tipo_CDC, string Conexion)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.ConsultaTipoCDC(Id_Cd_ver, ref Tipo_CDC ,Conexion );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaClienteTieneCuentaNacional(ref Clientes cte, ref int TieneCuentaNacional, string Conexion)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.ConsultaClienteTieneCuentaNacional(ref cte, ref TieneCuentaNacional, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void EstructuraSegmento(ref DataSet dsEstructuraSegmento, Clientes cte, string Conexion)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.EstructuraSegmento(ref dsEstructuraSegmento, cte, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizaPotencial(Clientes cte, double NuevoVPObservado, string NuevoVPObservadoApp, ref int verificador, string Conexion)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ActualizaPotencial(cte, NuevoVPObservado, NuevoVPObservadoApp, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizaDimension(Clientes cte, int Dimension, double? VPTeorico, DateTime Fecha, ref int verificador, string Conexion)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ActualizaDimension(cte, Dimension, VPTeorico, Fecha, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaContactos(Clientes cte, ref DataSet dsContactosClientes, string Conexion)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultaContactos(cte, ref dsContactosClientes, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarContacto(Contacto cont, ref int verificador, string Conexion)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.EliminarContacto(cont, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void ConsultarAdenda(int Id_Emp, int Id_Cd_Ver, int Id_cte, string Tipo, ref List<AdendaDet> listCab, ref List<AdendaDet> listDet, ref List<AdendaDet> listCabR, string Conexion)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultarAdenda(Id_Emp, Id_Cd_Ver, Id_cte, Tipo, ref listCab, ref listDet, ref listCabR, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClientes(Clientes cte, string Conexion, ref List<Comun> List, object FiltroId, object FiltroDesc)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultaClientes(cte, Conexion, ref List, FiltroId, FiltroDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPrecios(Clientes cte, string Conexion, ref List<Comun> List, object FiltroId, object FiltroDesc)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultaPrecios(cte, Conexion, ref List, FiltroId, FiltroDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarClienteFormaPago(ref Clientes cte, string Conexion)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultarClienteFormaPago(ref cte, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarCteFormaPago(Clientes cte, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.InsertarCteFormaPago(cte, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaEstadistica(Clientes cte, string Conexion, ref List<Comun> List, object FiltroId, object FiltroDesc)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultaEstadistica(cte, Conexion, ref List, FiltroId, FiltroDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaIndicadores(Clientes cte, string Conexion, ref List<Producto> List, object FiltroId, object FiltroDesc)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultaIndicadores(cte, Conexion, ref List, FiltroId, FiltroDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClienteTransf(ref Clientes cte, string Conexion)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.ConsultarClienteTransf(cte, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //RBM
        //Cliente-Territorio
        //Inicio
        public void ConsultaSolicitudesClienteTerr(Sesion Sesion, ref List<ClienteTerritorio> lstSolicitud)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultaSolicitudesClienteTerr(Sesion, ref lstSolicitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSolicitudClienteTerr(Sesion Sesion, ref ClienteTerritorio solicitud)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultaSolicitudClienteTerr(Sesion, ref solicitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarSolClienteTerritorio(Sesion sesion, ClienteTerritorio ClienteTer, ref int Respuesta)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.GuardarSolClienteTerritorio(sesion, ClienteTer, ref Respuesta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizaSolClienteTerritorio(Sesion Sesion, ClienteTerritorio ClienteTer, string Estatus, ref int Respuesta)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ActualizaSolClienteTerritorio(Sesion, ClienteTer, Estatus, ref Respuesta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSucursal(Sesion Sesion, ref ClienteTerritorio ClienteTer)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultaSucursal(Sesion, ref ClienteTer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSolicitudClienteTerrCorreo(Sesion Sesion, ref ClienteTerritorio solicitud)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultaSolicitudClienteTerrCorreo(Sesion, ref solicitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Fin
    }
}
