using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CapPago
    {
        public void ConsultarCantidadPagosCentroDist(ref int verificador, int Id_Cd, string Conexion)
        {
            try
            {
                new CD_CapPago().ConsultarCantidadPagosCentroDist(ref verificador, Id_Cd, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPago(Pago pago, string Conexion, ref List<Pago> List)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ConsultaPago(pago, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPagoFicha(ref Factura ficha, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ConsultaPagoFicha(ref ficha, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPagoNotaFicha(ref NotaCargo ficha, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ConsultaPagoNotaFicha(ref ficha, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void InsertarPago(Pago pago, List<Banco_Ficha> list_fichas, List<PagoDet> list_pagos, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.InsertarPago(pago, list_fichas, list_pagos, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarPago(Pago pago, List<Banco_Ficha> list_fichas, List<PagoDet> list_pagos, string Conexion, ref int verificador, ref List<int> centros)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ModificarPago(pago, list_fichas, list_pagos, Conexion, ref verificador, ref centros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPago(ref Pago pago, ref List<Banco_Ficha> list_fichas, ref List<PagoDet> list_pagos, string Conexion)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.ConsultaPago(ref pago, ref list_fichas, ref list_pagos, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Baja(Pago pag, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.Baja(pag, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Imprimir(Pago pag, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                claseCapaDatos.Imprimir(pag, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCuentaBanco(Sesion sesion, int banco, ref string cuenta)
        {
            try
            {
                CD_CapPago pago = new CD_CapPago();
                pago.ConsultaCuentaBanco(sesion, banco, ref cuenta); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PermitirExtemporaneo(Pago pag, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPago pago = new CD_CapPago();
                pago.PermitirExtemporaneo(pag, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CierreExtemporaneo(Pago pag, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPago pago = new CD_CapPago();
                pago.CierreExtemporaneo(pag, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarCentro(int Id_Emp, string serie, ref DbCentro centro, string ConexionCob, int Tipo_CDC )
        {
            try
            {
                CD_CapPago cppago = new CD_CapPago();
                cppago.ConsultarCentro(Id_Emp, serie,ref centro, ConexionCob,Tipo_CDC);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
