﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_SerRutaServicio
    {    
        public string ConsultaNombreEmpresa(Sesion sesion)
        {
            string empresa = string.Empty;
            try
            {
                CD_SerRutaServicio claseCapaDatos = new CD_SerRutaServicio();
                empresa = claseCapaDatos.ConsultaNombreEmpresa(sesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return empresa;
        }

        public string ConsultaNombreSucursal(Sesion sesion)
        {
            string sucursal = string.Empty;
            try
            {
                CD_SerRutaServicio claseCapaDatos = new CD_SerRutaServicio();
                sucursal = claseCapaDatos.ConsultaNombreSucursal(sesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sucursal;
        }

    }
}
