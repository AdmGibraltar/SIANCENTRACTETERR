﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_FacturasEntrega
    {
        public void ConsultaFacturasEntrega(int Id_Emp, int Id_Cd, string Conexion, FacturaEntrega facturafiltro, ref List<FacturaEntrega> List)
        {
            try
            {
                CD_FacturasEntrega factura = new CD_FacturasEntrega();
                factura.ConsultaProFacturaEntrega(Id_Emp, Id_Cd, Conexion, facturafiltro, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarFacturasEntrega(int Id_Emp, int Id_Cd, int Id_U, FacturaEntrega facturaEntrega, string Conexion, ref int verificador)
        {
            try
            {
                CD_FacturasEntrega factura = new CD_FacturasEntrega();
                factura.ModificarProFacturaEntrega(Id_Emp, Id_Cd, Id_U, facturaEntrega, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarFacturasEntregaCob(int Id_Emp, int Id_Cd, int Id_U, FacturaAlmacenCobro facturaEntrega, string Conexion, ref int verificador, string dbname)
        {
            try
            {
                CD_FacturasEntrega factura = new CD_FacturasEntrega();
                factura.ModificarProFacturaEntregaCob(Id_Emp, Id_Cd, Id_U, facturaEntrega, Conexion, ref verificador, dbname);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
