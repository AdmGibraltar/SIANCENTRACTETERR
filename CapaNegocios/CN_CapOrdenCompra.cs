using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_CapOrdenCompra
    {
        public void ConsultaOrdenCompra(ref OrdenCompra ordenCompra, string Conexion)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.ConsultaOrdenCompra(ref ordenCompra, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaOrdenCompra_Lista(OrdenCompra ordenCompra, string Conexion, ref List<OrdenCompra> List
            , int Id_Ord_inicio
            , int Id_Ord_fin
            , DateTime Ord_Fecha_inicio
            , DateTime Ord_Fecha_fin
            , string Ord_Estatus)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.ConsultaOrdenCompra_Lista(ordenCompra, Conexion, ref List
                    , Id_Ord_inicio
                    , Id_Ord_fin
                    , Ord_Fecha_inicio
                    , Ord_Fecha_fin
                    , Ord_Estatus);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GeneraOrdenCompraAutomatica(string Conexion, ref DataTable dt, string nombreTabla, int Id_Emp, int Id_Cd_Ver, int proveedor, int Id_prd_RI, int Id_prd_RF, bool Prd_Transito_Aplica, int? validador)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.GeneraOrdenCompraAutomatica(Conexion, ref dt, nombreTabla, Id_Emp, Id_Cd_Ver, proveedor, Id_prd_RI, Id_prd_RF, Prd_Transito_Aplica, validador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void GeneraOrdenCompraAutomatica_Lista(OrdenCompraDet ordenCompra, Sesion sesion, int proveedor, int Id_prd_RI, int Id_prd_RF, bool Prd_Transito_Aplica, ref List<OrdenCompraDet> List)
        //{
        //    try
        //    {
        //        CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
        //        claseCapaDatos.GeneraOrdenCompraAutomatica_Lista(ordenCompra, sesion, proveedor, Id_prd_RI, Id_prd_RF, Prd_Transito_Aplica, ref List);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void InsertarOrdenCompra(ref OrdenCompra ordenCompra, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.InsertarOrdenCompra(ref ordenCompra, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarOrdenCompra(OrdenCompra ordenCompra, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.ModificarOrdenCompra(ordenCompra, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarOrdenCompra_Estatus(OrdenCompra ordenCompra, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.ModificarOrdenCompra_Estatus(ordenCompra, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarOrdenCompra_EstatusEmision(OrdenCompra ordenCompra, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.ModificarOrdenCompra_EstatusEmision(ordenCompra, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void EliminarOrdenCompra(OrdenCompra ordenCompra, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.EliminarOrdenCompra(ordenCompra, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
