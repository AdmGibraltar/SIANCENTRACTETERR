using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_ProductoPrecios
    {
        public void ConsultaListaProductoPrecios(ProductoPrecios productoPrecio, string Conexion, ref List<ProductoPrecios> List)
        {
            try
            {
                CD_ProductoPrecios claseCapaDatos = new CD_ProductoPrecios();
                claseCapaDatos.ConsultaListaProductoPrecios(productoPrecio, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarProductoPrecios(ProductoPrecios productoPrecio, string Conexion, ref int verificador)
        {
            try
            {
                CD_ProductoPrecios claseCapaDatos = new CD_ProductoPrecios();
                claseCapaDatos.InsertarProductoPrecios(productoPrecio, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarProductoPrecios(ProductoPrecios productoPrecio, string Conexion, ref int verificador)
        {
            try
            {
                CD_ProductoPrecios claseCapaDatos = new CD_ProductoPrecios();
                claseCapaDatos.ModificarProductoPrecios(productoPrecio, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaListaProductoPrecioAAA(ref float precio, Sesion sesion, int Id_Prd, ref int Id_Pre)
        {
            try
            {
                CD_ProductoPrecios claseCapaDatos = new CD_ProductoPrecios();
                claseCapaDatos.ConsultaListaProductoPrecioAAA(ref precio, sesion, Id_Prd, ref Id_Pre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaListaProductoPrecioAAA(ref float precio, Sesion sesion, int Id_Prd)
        {
            try
            {
                CD_ProductoPrecios claseCapaDatos = new CD_ProductoPrecios();
                claseCapaDatos.ConsultaListaProductoPrecioAAA(ref precio, sesion, Id_Prd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaListaProductoPrecioAAA(ref float precio, int Id_Emp, int Id_Cd, int Id_Prd, string Conexion)
        {
            try
            {
                CD_ProductoPrecios claseCapaDatos = new CD_ProductoPrecios();
                claseCapaDatos.ConsultaListaProductoPrecioAAA(ref precio, Id_Emp, Id_Cd, Id_Prd, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaListaProductoPrecioPUBLICO(ref float precio, Sesion sesion, int Id_Prd)
        {
            try
            {
                CD_ProductoPrecios claseCapaDatos = new CD_ProductoPrecios();
                claseCapaDatos.ConsultaListaProductoPrecioPUBLICO(ref precio, sesion, Id_Prd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta el precio publico de un articulo. Recibe ProductoPrecios (Id_Emp, Id_Cd, Id_Prd)
        /// </summary>
        /// <param name="productoPrecios"></param>
        /// <param name="Conexion"></param>
        /// <param name="precio"></param>
        public void ConsultaProductoPrecio_Publico(ProductoPrecios productoPrecios, string Conexion, ref double precio, DateTime Fecha_Actual)
        {//rm Consulta el precio publico de un articulo
            try
            {
                new CD_ProductoPrecios().ConsultaProductoPrecio_Publico(productoPrecios, Conexion, ref precio, Fecha_Actual);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarPrecios(ref double precioAAA, ref double precioLista, Sesion sesion, int Id_Cte, int Id_Prd)
        {
            try
            {
                new CD_ProductoPrecios().ConsultarPrecios(ref precioAAA, ref  precioLista, sesion, Id_Cte, Id_Prd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
