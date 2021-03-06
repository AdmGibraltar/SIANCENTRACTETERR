﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

using System.Data;

namespace CapaDatos
{
    public class CD_CatCentroDistribucion
    {
        public void ConsultaCentroDistribucionLista(string Conexion, ref List<CentroDistribucion> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCentroDistribucion_Consulta", ref dr);

                CentroDistribucion centroDistribucion = default(CentroDistribucion);
                while (dr.Read())
                {
                    centroDistribucion = new CentroDistribucion();
                    //Datos Generales de C. Dist.
                    centroDistribucion.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    centroDistribucion.Id_Reg = dr.GetInt32(dr.GetOrdinal("Id_Reg"));
                    centroDistribucion.Id_Cd = dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    centroDistribucion.Cd_Pais = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Pais"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Cd_Pais"));
                    centroDistribucion.Cd_Estado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Estado"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Cd_Estado"));
                    centroDistribucion.Cd_Ciudad = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Ciudad"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Cd_Ciudad"));
                    centroDistribucion.Cd_Municipio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Municipio"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Cd_Municipio"));
                    centroDistribucion.Cd_Colonia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Colonia"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Cd_Colonia"));
                    centroDistribucion.Cd_Descripcion = dr.GetString(dr.GetOrdinal("Cd_Nombre"));
                    centroDistribucion.Cd_Calle = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Calle"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Cd_Calle"));
                    centroDistribucion.Cd_Numero = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Numero"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Cd_Numero"));
                    centroDistribucion.Cd_CP = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Cp"))) ? string.Empty : dr.GetInt32(dr.GetOrdinal("Cd_Cp")).ToString();
                    centroDistribucion.Cd_Rfc = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Rfc"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Cd_Rfc"));
                    centroDistribucion.Cd_Tel = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Tel"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Cd_Tel"));
                    centroDistribucion.Id_TipoCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_TipoCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_TipoCD"));
                    //centroDistribucion.Cd_Formato = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Formato"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_Formato"));
                    centroDistribucion.Cd_Activo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Activo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Cd_Activo"));

                    //Datos de valuacion de proyectos de C. Dist.
                    centroDistribucion.Cd_TasaCetes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_TasaCetes"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_TasaCetes"));
                    centroDistribucion.Cd_DiasCuentasPorCobrar = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_DiasCuentasPorCobrar"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_DiasCuentasPorCobrar"));
                    centroDistribucion.Cd_Dias = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Dias"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_Dias"));
                    centroDistribucion.Cd_DiasInv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_DiasInv"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_DiasInv"));
                    centroDistribucion.Cd_FactorInvComodato = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_FactorInvComodato"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_FactorInvComodato")));
                    centroDistribucion.Cd_FactorConvActFijo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_FactorConvActFijo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_FactorConvActFijo"));
                    centroDistribucion.Cd_DiasFinanciaProv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_DiasFinanciaProv"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_DiasFinanciaProv"));
                    centroDistribucion.Cd_TasaIncCostoCapital = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_TasaIncCostoCapital"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_TasaIncCostoCapital"));
                    centroDistribucion.Cd_Iva = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Iva"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_Iva"));
                    centroDistribucion.Cd_Flete = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Flete"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_Flete"));
                    centroDistribucion.Cd_ComisionRik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_ComisionRik"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_ComisionRik"));
                    centroDistribucion.Cd_OtrosGastosVar = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_OtrosGastosVar"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_OtrosGastosVar"));
                    centroDistribucion.Cd_ContribucionGastosFijosOtros = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_ContribucionGastosFijosOtros"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_ContribucionGastosFijosOtros"));
                    centroDistribucion.Cd_ContribucionGastosFijosPapel = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_ContribucionGastosFijosPapel"))) ? 0 : dr.GetDouble(dr.GetOrdinal("Cd_ContribucionGastosFijosPapel"));
                    centroDistribucion.Cd_ISRyPTU = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_ISRyPTU"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_ISRyPTU"));
                    centroDistribucion.Cd_CargoUCS = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_CargoUCS"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_CargoUCS"));

                    //Datos de pedidos y facturación
                    //centroDistribucion.Cd_FacturasRangoInicio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_FacturasRangoInicio"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_FacturasRangoInicio"));
                    //centroDistribucion.Cd_PartidaFacturas = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_PartidaFacturas"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_PartidaFacturas"));
                    //centroDistribucion.Cd_FacturasRangoFin = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_FacturasRangoFin"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_FacturasRangoFin"));
                    centroDistribucion.Cd_PartidaPedidos = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_PartidaPedidos"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_PartidaPedidos"));
                    centroDistribucion.Cd_IvaPedidosFacturacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_IvaPedidosFacturacion"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_IvaPedidosFacturacion"));
                    //centroDistribucion.Cd_ClientesRangoInicio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_ClientesRangoInicio"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_ClientesRangoInicio"));
                    //centroDistribucion.Cd_ClientesRangoFin = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_ClientesRangoFin"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_ClientesRangoFin"));
                    //centroDistribucion.Cd_AjusteFromatoReng = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_AjusteFromatoReng"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_AjusteFromatoReng"));
                    centroDistribucion.Cd_MaximoTerritoriosSegmentos = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_MaximoTerritoriosSegmentos"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_MaximoTerritoriosSegmentos"));
                    centroDistribucion.Cd_FormatoFacturaRetIva = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_FormatoFacturaRetIva"))) ? false : dr.GetBoolean(dr.GetOrdinal("Cd_FormatoFacturaRetIva"));
                    centroDistribucion.Cd_DeshabilitarReglaCons = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_DeshabilitarReglaCons"))) ? false : dr.GetBoolean(dr.GetOrdinal("Cd_DeshabilitarReglaCons"));
                    centroDistribucion.Cd_ActivaCapPedRep = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_ActivaCapPedRep"))) ? false : dr.GetBoolean(dr.GetOrdinal("Cd_ActivaCapPedRep"));

                    //Datos de info de inventarios
                    centroDistribucion.Cd_PartidaRemisiones = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_PartidaRemisiones"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_PartidaRemisiones"));
                    centroDistribucion.Cd_PartidaEntradas = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_PartidaEntradas"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_PartidaEntradas"));
                    centroDistribucion.Cd_AjusteFromatoRengInventario = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_AjusteFormatoRengInventario"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_AjusteFormatoRengInventario"));

                    //Datos de informacion de Cobranza
                    centroDistribucion.Cd_InteresMoratorio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_InteresMoratorio"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_InteresMoratorio"));
                    centroDistribucion.Cd_ContribucionBruta = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_ContribucionBruta"))) ? 0 : dr.GetDecimal(dr.GetOrdinal("Cd_ContribucionBruta"));
                    centroDistribucion.Cd_Amortizacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Amortizacion"))) ? 0 : dr.GetDecimal(dr.GetOrdinal("Cd_Amortizacion"));
                    centroDistribucion.Cd_SaldosMenores = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_SaldosMenores"))) ? 0 : dr.GetDecimal(dr.GetOrdinal("Cd_SaldosMenores"));
                    centroDistribucion.Cd_CobranzaPersonaFormula = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_CobranzaPersonaFormula"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Cd_CobranzaPersonaFormula"));
                    centroDistribucion.Cd_CobranzaPersonaAutoriza = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_CobranzaPersonaAutoriza"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Cd_CobranzaPersonaAutoriza"));
                    //centroDistribucion.Cd_PartidaNotaCargo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_PartidaNotaCargo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_PartidaNotaCargo"));
                    //centroDistribucion.Cd_PartidaNotaCredito = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_PartidaNotaCredito"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_PartidaNotaCredito"));
                    centroDistribucion.Cd_RelacionCobranza = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_RelacionCobranza"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_RelacionCobranza"));

                    //Datos de informacion de Admion. de Inventarios
                    centroDistribucion.Cd_TiempoEntrega = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_TiempoEntrega"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_TiempoEntrega"));
                    centroDistribucion.Cd_TiempoTransportacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_TiempoTransportacion"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_TiempoTransportacion"));

                    //Datos de info de compras locales
                    centroDistribucion.Cd_ActualizaEntradaAuto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_ActualizaEntradaAuto"))) ? false : dr.GetBoolean(dr.GetOrdinal("Cd_ActualizaEntradaAuto"));
                    centroDistribucion.Cd_FactorCosto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_FactorCosto"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_FactorCosto"));

                    //Datos calculados
                    centroDistribucion.ConsecutivoValProyCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ConsecutivoValProyCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_TiempoEntrega"));
                    centroDistribucion.CantidadPedidosCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CantidadPedidosCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_TiempoEntrega"));
                    centroDistribucion.CantidadRemisionesCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CantidadRemisionesCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_TiempoEntrega"));
                    centroDistribucion.CantidadEntradasCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CantidadEntradasCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_TiempoEntrega"));
                    centroDistribucion.CantidadSalidasCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CantidadSalidasCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_TiempoEntrega"));
                    centroDistribucion.CantidadDevolucionesCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CantidadDevolucionesCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_TiempoEntrega"));
                    centroDistribucion.CantidadContratosComCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CantidadContratosComCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_TiempoEntrega"));
                    centroDistribucion.CantidadEmbarquesCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CantidadEmbarquesCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_TiempoEntrega"));
                    centroDistribucion.CantidadNotaCargoCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CantidadNotaCargoCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_TiempoEntrega"));
                    centroDistribucion.CantidadNotaCreditoCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CantidadNotaCreditoCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_TiempoEntrega"));
                    centroDistribucion.CantidadPagosCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CantidadPagosCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_TiempoEntrega"));
                    centroDistribucion.CantidadOrdenesCompraCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CantidadOrdenesCompraCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_TiempoEntrega"));

                    list.Add(centroDistribucion);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCentroDistribucion(ref CentroDistribucion centroDistribucion, int Id_Cd, int Id_Emp, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Cd", "@Id_Emp" };

                object[] Valores = { Id_Cd, Id_Emp };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCentroDistribucion_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    centroDistribucion = new CentroDistribucion();

                    centroDistribucion.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    centroDistribucion.Id_Reg = dr.GetInt32(dr.GetOrdinal("Id_Reg"));
                    centroDistribucion.Id_Cd = dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    centroDistribucion.Cd_Pais = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Pais"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Cd_Pais"));
                    centroDistribucion.Cd_Estado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Estado"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Cd_Estado"));
                    centroDistribucion.Cd_Ciudad = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Ciudad"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Cd_Ciudad"));
                    centroDistribucion.Cd_Municipio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Municipio"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Cd_Municipio"));
                    centroDistribucion.Cd_Colonia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Colonia"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Cd_Colonia"));
                    centroDistribucion.Cd_Descripcion = dr.GetString(dr.GetOrdinal("Cd_Nombre"));
                    centroDistribucion.Cd_Calle = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Calle"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Cd_Calle"));
                    centroDistribucion.Cd_Numero = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Numero"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Cd_Numero"));
                    centroDistribucion.Cd_CP = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Cp"))) ? string.Empty : dr.GetInt32(dr.GetOrdinal("Cd_Cp")).ToString();
                    centroDistribucion.Cd_Rfc = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Rfc"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Cd_Rfc"));
                    centroDistribucion.Cd_Tel = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Tel"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Cd_Tel"));
                    centroDistribucion.Id_TipoCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_TipoCD"))) ? -1 : dr.GetInt32(dr.GetOrdinal("Id_TipoCD"));
                    //centroDistribucion.Cd_Formato = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Formato"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_Formato"));
                    centroDistribucion.Cd_Activo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Activo"))) ? false : dr.GetBoolean(dr.GetOrdinal("Cd_Activo"));



                    //Datos de valuacion de proyectos de C. Dist.
                    centroDistribucion.Cd_TasaCetes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_TasaCetes"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_TasaCetes")));
                    centroDistribucion.Cd_DiasCuentasPorCobrar = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_DiasCuentasPorCobrar"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cd_DiasCuentasPorCobrar")));
                    centroDistribucion.Cd_Dias = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Dias"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cd_Dias")));
                    centroDistribucion.Cd_DiasInv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_DiasInv"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cd_DiasInv")));
                    centroDistribucion.Cd_FactorInvComodato = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_FactorInvComodato"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_FactorInvComodato")));
                    centroDistribucion.Cd_FactorConvActFijo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_FactorConvActFijo"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_FactorConvActFijo")));
                    centroDistribucion.Cd_DiasFinanciaProv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_DiasFinanciaProv"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cd_DiasFinanciaProv")));
                    centroDistribucion.Cd_TasaIncCostoCapital = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_TasaIncCostoCapital"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_TasaIncCostoCapital")));
                    centroDistribucion.Cd_Iva = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Iva"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_Iva")));
                    centroDistribucion.Cd_Flete = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Flete"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_Flete")));
                    centroDistribucion.Cd_ComisionRik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_ComisionRik"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_ComisionRik")));
                    centroDistribucion.Cd_OtrosGastosVar = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_OtrosGastosVar"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_OtrosGastosVar")));
                    centroDistribucion.Cd_ContribucionGastosFijosOtros = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_ContribucionGastosFijosOtros"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_ContribucionGastosFijosOtros")));
                    centroDistribucion.Cd_ContribucionGastosFijosPapel = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_ContribucionGastosFijosPapel"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_ContribucionGastosFijosPapel")));
                    centroDistribucion.Cd_ISRyPTU = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_ISRyPTU"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_ISRyPTU")));
                    centroDistribucion.Cd_CargoUCS = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_CargoUCS"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_CargoUCS")));
                    centroDistribucion.Cd_CreditoKey = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_CreditoKey")));
                    centroDistribucion.Cd_CreditoPapel = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_CreditoPapel")));

                    //Datos de pedidos y facturación
                    //centroDistribucion.Cd_FacturasRangoInicio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_FacturasRangoInicio"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_FacturasRangoInicio"));
                    //centroDistribucion.Cd_PartidaFacturas = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_PartidaFacturas"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_PartidaFacturas"));
                    //centroDistribucion.Cd_FacturasRangoFin = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_FacturasRangoFin"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_FacturasRangoFin"));
                    centroDistribucion.Cd_PartidaPedidos = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_PartidaPedidos"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_PartidaPedidos"));
                    centroDistribucion.Cd_IvaPedidosFacturacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_IvaPedidosFacturacion"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_IvaPedidosFacturacion"));
                    //centroDistribucion.Cd_ClientesRangoInicio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_ClientesRangoInicio"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_ClientesRangoInicio"));
                    //centroDistribucion.Cd_ClientesRangoFin = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_ClientesRangoFin"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_ClientesRangoFin"));
                    //centroDistribucion.Cd_AjusteFromatoReng = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_AjusteFromatoReng"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_AjusteFromatoReng"));
                    centroDistribucion.Cd_MaximoTerritoriosSegmentos = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_MaximoTerritoriosSegmentos"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_MaximoTerritoriosSegmentos"));
                    centroDistribucion.Cd_FormatoFacturaRetIva = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_FormatoFacturaRetIva"))) ? false : dr.GetBoolean(dr.GetOrdinal("Cd_FormatoFacturaRetIva"));
                    centroDistribucion.Cd_DeshabilitarReglaCons = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_DeshabilitarReglaCons"))) ? false : dr.GetBoolean(dr.GetOrdinal("Cd_DeshabilitarReglaCons"));
                    centroDistribucion.Cd_ActivaCapPedRep = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_ActivaCapPedRep"))) ? false : dr.GetBoolean(dr.GetOrdinal("Cd_ActivaCapPedRep"));

                    //Datos de info de inventarios
                    centroDistribucion.Cd_PartidaRemisiones = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_PartidaRemisiones"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_PartidaRemisiones"));
                    centroDistribucion.Cd_PartidaEntradas = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_PartidaEntradas"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_PartidaEntradas"));
                    centroDistribucion.Cd_AjusteFromatoRengInventario = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_AjusteFormatoRengInventario"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_AjusteFormatoRengInventario"));

                    //Datos de informacion de Cobranza
                    centroDistribucion.Cd_InteresMoratorio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_InteresMoratorio"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_InteresMoratorio"));
                    centroDistribucion.Cd_ContribucionBruta = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_ContribucionBruta"))) ? 0 : dr.GetDecimal(dr.GetOrdinal("Cd_ContribucionBruta"));
                    centroDistribucion.Cd_Amortizacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Amortizacion"))) ? 0 : dr.GetDecimal(dr.GetOrdinal("Cd_Amortizacion"));
                    centroDistribucion.Cd_SaldosMenores = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_SaldosMenores"))) ? 0 : dr.GetDecimal(dr.GetOrdinal("Cd_SaldosMenores"));
                    centroDistribucion.Cd_CobranzaPersonaFormula = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_CobranzaPersonaFormula"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Cd_CobranzaPersonaFormula"));
                    centroDistribucion.Cd_CobranzaPersonaAutoriza = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_CobranzaPersonaAutoriza"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Cd_CobranzaPersonaAutoriza"));
                    //centroDistribucion.Cd_PartidaNotaCargo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_PartidaNotaCargo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_PartidaNotaCargo"));
                    //centroDistribucion.Cd_PartidaNotaCredito = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_PartidaNotaCredito"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_PartidaNotaCredito"));
                    centroDistribucion.Cd_RelacionCobranza = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_RelacionCobranza"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_RelacionCobranza"));

                    //Datos de informacion de Admion. de Inventarios
                    centroDistribucion.Cd_TiempoEntrega = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_TiempoEntrega"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_TiempoEntrega"));
                    centroDistribucion.Cd_TiempoTransportacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_TiempoTransportacion"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_TiempoTransportacion"));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_NumMacola"))))
                        centroDistribucion.Cd_NumMacola = null;
                    else
                        centroDistribucion.Cd_NumMacola = dr.GetInt32(dr.GetOrdinal("Cd_NumMacola"));

                    //Datos de info de compras locales
                    centroDistribucion.Cd_ActualizaEntradaAuto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_ActualizaEntradaAuto"))) ? false : dr.GetBoolean(dr.GetOrdinal("Cd_ActualizaEntradaAuto"));
                    centroDistribucion.Cd_FactorCosto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_FactorCosto"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_FactorCosto"));

                    //Datos calculados
                    centroDistribucion.ConsecutivoValProyCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ConsecutivoValProyCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("ConsecutivoValProyCD"));
                    centroDistribucion.CantidadPedidosCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CantidadPedidosCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("CantidadPedidosCD"));
                    centroDistribucion.CantidadRemisionesCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CantidadRemisionesCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("CantidadRemisionesCD"));
                    centroDistribucion.CantidadEntradasCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CantidadEntradasCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("CantidadEntradasCD"));
                    centroDistribucion.CantidadSalidasCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CantidadSalidasCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("CantidadSalidasCD"));
                    centroDistribucion.CantidadDevolucionesCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CantidadDevolucionesCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("CantidadDevolucionesCD"));
                    centroDistribucion.CantidadContratosComCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CantidadContratosComCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("CantidadContratosComCD"));
                    centroDistribucion.CantidadEmbarquesCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CantidadEmbarquesCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("CantidadEmbarquesCD"));
                    centroDistribucion.CantidadNotaCargoCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CantidadNotaCargoCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("CantidadNotaCargoCD"));
                    centroDistribucion.CantidadNotaCreditoCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CantidadNotaCreditoCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("CantidadNotaCreditoCD"));
                    centroDistribucion.CantidadPagosCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CantidadPagosCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("CantidadPagosCD"));
                    centroDistribucion.CantidadOrdenesCompraCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CantidadOrdenesCompraCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("CantidadOrdenesCompraCD"));
                    centroDistribucion.CantidadReclamacionesCD = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CantidadReclamacionesCD"))) ? 0 : dr.GetInt32(dr.GetOrdinal("CantidadReclamacionesCD"));
                    centroDistribucion.Dias = dr["Cob_Dias"].ToString() == "" ? 0 : Convert.ToInt32(dr["Cob_Dias"]);
                    centroDistribucion.DiasRevision = dr["Cob_DiasRevision"].ToString() == "" ? 0 : Convert.ToInt32(dr["Cob_DiasRevision"]);
 
                    try
                    {
                        centroDistribucion.Cd_FactorCostoFinanciero = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_FactorCostoFinanciero")));
                    }
                    catch  
                    {
                        
                    }
                    
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCentroDistribucion_DatosValProyecto(ref CentroDistribucion centroDistribucion, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCentroDistribucion_DatosGen", ref dr);

                while (dr.Read())
                {
                    centroDistribucion = new CentroDistribucion();

                    //Datos de valuacion de proyectos de C. Dist.
                    centroDistribucion.Cd_TasaCetes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_TasaCetes"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_TasaCetes"));
                    centroDistribucion.Cd_DiasCuentasPorCobrar = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_DiasCuentasPorCobrar"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_DiasCuentasPorCobrar"));
                    centroDistribucion.Cd_Dias = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Dias"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_Dias"));
                    centroDistribucion.Cd_DiasInv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_DiasInv"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_DiasInv"));
                    centroDistribucion.Cd_FactorInvComodato = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_FactorInvComodato"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_FactorInvComodato")));
                    centroDistribucion.Cd_FactorConvActFijo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_FactorConvActFijo"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_FactorConvActFijo"));
                    centroDistribucion.Cd_DiasFinanciaProv = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_DiasFinanciaProv"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_DiasFinanciaProv"));
                    centroDistribucion.Cd_TasaIncCostoCapital = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_TasaIncCostoCapital"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_TasaIncCostoCapital"));
                    centroDistribucion.Cd_Iva = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Iva"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_Iva"));
                    centroDistribucion.Cd_Flete = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Flete"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_Flete"));
                    centroDistribucion.Cd_ComisionRik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_ComisionRik"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_ComisionRik"));
                    centroDistribucion.Cd_OtrosGastosVar = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_OtrosGastosVar"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_OtrosGastosVar"));
                    centroDistribucion.Cd_ContribucionGastosFijosOtros = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_ContribucionGastosFijosOtros"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_ContribucionGastosFijosOtros"));
                    centroDistribucion.Cd_ContribucionGastosFijosPapel = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_ContribucionGastosFijosPapel"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_ContribucionGastosFijosPapel")));
                    centroDistribucion.Cd_ISRyPTU = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_ISRyPTU"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_ISRyPTU"));
                    centroDistribucion.Cd_CargoUCS = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_CargoUCS"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cd_CargoUCS"));
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarCentroDistribucion_DatosValProyecto(ref CentroDistribucion centroDistribucion, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
			        "@Cd_TasaCetes"
                    ,"@Cd_DiasCuentasPorCobrar"
                    ,"@Cd_Dias"
                    ,"@Cd_DiasInv"
                    ,"@Cd_FactorInvComodato"
                    ,"@Cd_FactorConvActFijo"
                    ,"@Cd_DiasFinanciaProv"
                    ,"@Cd_TasaIncCostoCapital"
                    ,"@Cd_Iva"
                    ,"@Cd_Flete"
                    ,"@Cd_ComisionRik"
                    ,"@Cd_OtrosGastosVar"
                    ,"@Cd_ContribucionGastosFijosOtros"
                    ,"@Cd_ContribucionGastosFijosPapel"
                    ,"@Cd_ISRyPTU"
                    ,"@Cd_CargoUCS"
		        };

                object[] Valores = {
                    centroDistribucion.Cd_TasaCetes
                    ,centroDistribucion.Cd_DiasCuentasPorCobrar
                    ,centroDistribucion.Cd_Dias
			        ,centroDistribucion.Cd_DiasInv
                    ,centroDistribucion.Cd_FactorInvComodato
                    ,centroDistribucion.Cd_FactorConvActFijo
                    ,centroDistribucion.Cd_DiasFinanciaProv
                    ,centroDistribucion.Cd_TasaIncCostoCapital
                    ,centroDistribucion.Cd_Iva
			        ,centroDistribucion.Cd_Flete
                    ,centroDistribucion.Cd_ComisionRik
                    ,centroDistribucion.Cd_OtrosGastosVar
                    ,centroDistribucion.Cd_ContribucionGastosFijosOtros
                    ,centroDistribucion.Cd_ContribucionGastosFijosPapel
			        ,centroDistribucion.Cd_ISRyPTU
                    ,centroDistribucion.Cd_CargoUCS
		        };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(
                    "spCatCentroDistribucion_DatosGenActualiza", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarCentroDistribucion(ref CentroDistribucion centroDistribucion, string Conexion, ref int verificador, bool actualizar)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = {
			        "@Id_Emp"
                    ,"@Id_Cd"
				    ,"@Id_Reg"
				    ,"@Cd_Nombre"
				    ,"@Cd_Pais"
				    ,"@Cd_Estado"
				    ,"@Cd_Ciudad"
				    ,"@Cd_Municipio"
				    ,"@Cd_Colonia"
				    ,"@Cd_Calle"
				    ,"@Cd_Numero"
				    ,"@Cd_Cp"
				    ,"@Cd_Rfc"
				    ,"@Cd_Tel"
				    ,"@Id_TipoCD"
                    //,"@Cd_Formato"
				    ,"@Cd_Activo"

                    ,"@Cd_TasaCetes"
                    ,"@Cd_DiasCuentasPorCobrar"
                    ,"@Cd_Dias"
                    ,"@Cd_DiasInv"
                    ,"@Cd_FactorInvComodato"
                    ,"@Cd_FactorConvActFijo"
                    ,"@Cd_DiasFinanciaProv"
                    ,"@Cd_TasaIncCostoCapital"
                    ,"@Cd_Iva"
                    ,"@Cd_Flete"
                    ,"@Cd_ComisionRik"
                    ,"@Cd_OtrosGastosVar"
                    ,"@Cd_ContribucionGastosFijosOtros"
                    ,"@Cd_ContribucionGastosFijosPapel"
                    ,"@Cd_ISRyPTU"
                    ,"@Cd_CargoUCS"

                    //,"@Cd_FacturasRangoInicio"
                    //,"@Cd_PartidaFacturas"
                    //,"@Cd_FacturasRangoFin"
                    ,"@Cd_PartidaPedidos"
                    ,"@Cd_IvaPedidosFacturacion"
                    //,"@Cd_ClientesRangoInicio"
                    //,"@Cd_ClientesRangoFin"
                    //,"@Cd_AjusteFromatoReng"
                    ,"@Cd_MaximoTerritoriosSegmentos"
                    ,"@Cd_FormatoFacturaRetIva"
                    ,"@Cd_DeshabilitarReglaCons"
                    ,"@Cd_ActivaCapPedRep"

                    ,"@Cd_PartidaRemisiones"
                    ,"@Cd_PartidaEntradas"
                    ,"@Cd_AjusteFormatoRengInventario"

                    ,"@Cd_InteresMoratorio"
                    ,"@Cd_ContribucionBruta"
                    ,"@Cd_Amortizacion"
                    ,"@Cd_SaldosMenores"
                    ,"@Cd_CobranzaPersonaFormula"
                    ,"@Cd_CobranzaPersonaAutoriza"
                    //,"@Cd_PartidaNotaCargo"
                    //,"@Cd_PartidaNotaCredito"
                    ,"@Cd_RelacionCobranza"

                    ,"@Cd_TiempoEntrega"
                    ,"@Cd_TiempoTransportacion"
                    ,"@Cd_NumMacola"

                    ,"@Cd_ActualizaEntradaAuto"
				    ,"@Cd_FactorCosto"
                    ,"@Cd_CreditoKey"
                    ,"@Cd_CreditoPapel" 
                    ,"@Cd_FactorCostoFinanciero"
                    , "@Dias"
                    , "@DiasRevision"
		        };

                object[] Valores = {
                    centroDistribucion.Id_Emp
                    ,centroDistribucion.Id_Cd
                    ,centroDistribucion.Id_Reg
			        ,centroDistribucion.Cd_Descripcion
                    ,centroDistribucion.Cd_Pais
			        ,centroDistribucion.Cd_Estado
			        ,centroDistribucion.Cd_Ciudad
                    ,centroDistribucion.Cd_Municipio
			        ,centroDistribucion.Cd_Colonia
			        ,centroDistribucion.Cd_Calle
                    ,centroDistribucion.Cd_Numero
			        ,centroDistribucion.Cd_CP
                    ,centroDistribucion.Cd_Rfc
                    ,centroDistribucion.Cd_Tel
			        ,centroDistribucion.Id_TipoCD == -1 ? (object)null : centroDistribucion.Id_TipoCD
                    //,centroDistribucion.Cd_Formato
			        ,centroDistribucion.Cd_Activo

                    ,centroDistribucion.Cd_TasaCetes
                    ,centroDistribucion.Cd_DiasCuentasPorCobrar
                    ,centroDistribucion.Cd_Dias
			        ,centroDistribucion.Cd_DiasInv
                    ,centroDistribucion.Cd_FactorInvComodato
                    ,centroDistribucion.Cd_FactorConvActFijo
                    ,centroDistribucion.Cd_DiasFinanciaProv
                    ,centroDistribucion.Cd_TasaIncCostoCapital
                    ,centroDistribucion.Cd_Iva
			        ,centroDistribucion.Cd_Flete
                    ,centroDistribucion.Cd_ComisionRik
                    ,centroDistribucion.Cd_OtrosGastosVar
                    ,centroDistribucion.Cd_ContribucionGastosFijosOtros
                    ,centroDistribucion.Cd_ContribucionGastosFijosPapel
			        ,centroDistribucion.Cd_ISRyPTU
                    ,centroDistribucion.Cd_CargoUCS

                    //,centroDistribucion.Cd_FacturasRangoInicio
                    //,centroDistribucion.Cd_PartidaFacturas
                    //,centroDistribucion.Cd_FacturasRangoFin
                    ,centroDistribucion.Cd_PartidaPedidos
                    ,centroDistribucion.Cd_IvaPedidosFacturacion
                    //,centroDistribucion.Cd_ClientesRangoInicio
                    //,centroDistribucion.Cd_ClientesRangoFin
                    //,centroDistribucion.Cd_AjusteFromatoReng
                    ,centroDistribucion.Cd_MaximoTerritoriosSegmentos
                    ,centroDistribucion.Cd_FormatoFacturaRetIva
                    ,centroDistribucion.Cd_DeshabilitarReglaCons
                    ,centroDistribucion.Cd_ActivaCapPedRep

                    ,centroDistribucion.Cd_PartidaRemisiones
                    ,centroDistribucion.Cd_PartidaEntradas
                    ,centroDistribucion.Cd_AjusteFromatoRengInventario

                    ,centroDistribucion.Cd_InteresMoratorio
                    ,centroDistribucion.Cd_ContribucionBruta
                    ,centroDistribucion.Cd_Amortizacion
                    ,centroDistribucion.Cd_SaldosMenores
                    ,centroDistribucion.Cd_CobranzaPersonaFormula
                    ,centroDistribucion.Cd_CobranzaPersonaAutoriza
                    //,centroDistribucion.Cd_PartidaNotaCargo
                    //,centroDistribucion.Cd_PartidaNotaCredito
                    ,centroDistribucion.Cd_RelacionCobranza

                    ,centroDistribucion.Cd_TiempoEntrega
                    ,centroDistribucion.Cd_TiempoTransportacion
                    ,centroDistribucion.Cd_NumMacola

                    ,centroDistribucion.Cd_ActualizaEntradaAuto
                    ,centroDistribucion.Cd_FactorCosto
                    ,centroDistribucion.Cd_CreditoKey
                    ,centroDistribucion.Cd_CreditoPapel
                    ,centroDistribucion.Cd_FactorCostoFinanciero
                    ,centroDistribucion.Dias
                    ,centroDistribucion.DiasRevision

		        };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(
                    actualizar ? "spCatCentroDistribucion_Modificar" : "spCatCentroDistribucion_Insertar", ref verificador, Parametros, Valores);

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ModificarCentroDistribucion_DatosValuacionProyectos(ref CentroDistribucion centroDistribucion, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = {
			        "@Id_Emp"
                    ,"@Id_Cd"
				    
                    ,"@Cd_TasaCetes"
                    ,"@Cd_DiasCuentasPorCobrar"
                    ,"@Cd_Dias"
                    ,"@Cd_DiasInv"
                    ,"@Cd_FactorInvComodato"
                    ,"@Cd_FactorConvActFijo"
                    ,"@Cd_DiasFinanciaProv"
                    ,"@Cd_TasaIncCostoCapital"
                    ,"@Cd_Iva"
                    ,"@Cd_Flete"
                    ,"@Cd_ComisionRik"
                    ,"@Cd_OtrosGastosVar"
                    ,"@Cd_ContribucionGastosFijosOtros"
                    ,"@Cd_ContribucionGastosFijosPapel"
                    ,"@Cd_ISRyPTU"
                    ,"@Cd_CargoUCS"
		        };

                object[] Valores = {
                    centroDistribucion.Id_Emp
                    ,centroDistribucion.Id_Cd
                    
                    ,centroDistribucion.Cd_TasaCetes
                    ,centroDistribucion.Cd_DiasCuentasPorCobrar
                    ,centroDistribucion.Cd_Dias
			        ,centroDistribucion.Cd_DiasInv
                    ,centroDistribucion.Cd_FactorInvComodato
                    ,centroDistribucion.Cd_FactorConvActFijo
                    ,centroDistribucion.Cd_DiasFinanciaProv
                    ,centroDistribucion.Cd_TasaIncCostoCapital
                    ,centroDistribucion.Cd_Iva
			        ,centroDistribucion.Cd_Flete
                    ,centroDistribucion.Cd_ComisionRik
                    ,centroDistribucion.Cd_OtrosGastosVar
                    ,centroDistribucion.Cd_ContribucionGastosFijosOtros
                    ,centroDistribucion.Cd_ContribucionGastosFijosPapel
			        ,centroDistribucion.Cd_ISRyPTU
                    ,centroDistribucion.Cd_CargoUCS
		        };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCentroDistribucion_ModificarDatosValuacionProyectos", ref verificador, Parametros, Valores);

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void EliminarCentroDistribucion(int Id_Cd, string Conexion, ref Int32 verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Cd" };
                object[] Valores = { Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand_Nonquery("spCatCentroDistribucion_Eliminar", verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarIva(int Id_Emp, int Id_Cd, string Conexion, ref double Iva)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { Id_Emp, Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCentroDistribucionIva_Consultar", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    Iva = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cd_Iva")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarCobranzaCentroDistribucion(ref List<CentroDistribucion> ListaCentroDistribucion, int Id_Cd, int Id_Emp, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Cd", "@Id_Emp" };

                object[] Valores = { Id_Cd, Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCentroDistribucionCobranza_Consultar", ref dr, Parametros, Valores);
                CentroDistribucion centroDistribucion = new CentroDistribucion();
                while (dr.Read())
                {
                    centroDistribucion = new CentroDistribucion();
                    centroDistribucion.Id_Cob = dr.GetInt32(dr.GetOrdinal("Id_Cob"));
                    centroDistribucion.Cob_DiaInicio = dr.GetInt32(dr.GetOrdinal("Cob_DiaInicio"));
                    centroDistribucion.Cob_DiaLimite = dr.GetInt32(dr.GetOrdinal("Cob_DiaLimite"));
                    centroDistribucion.Cob_Multiplicador = dr.GetDouble(dr.GetOrdinal("Cob_Multiplicador"));
                    ListaCentroDistribucion.Add(centroDistribucion);
                }

                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCentroDistribucion_Cobranza(ref CentroDistribucion centroDistribucion, Sesion sesion, int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = {
			        "@Id_Emp"
                    ,"@Id_Cd"
                    ,"@Id_Cob"
                    ,"@Cob_DiaInicio"
                    ,"@Cob_DiaLimite"
                    ,"@Cob_Multiplicador"                    
                };
                object[] Valores = {
                    sesion.Id_Emp
                    ,sesion.Id_Cd_Ver                    
                    ,centroDistribucion.Id_Cob
                    ,centroDistribucion.Cob_DiaInicio
                    ,centroDistribucion.Cob_DiaLimite
			        ,centroDistribucion.Cob_Multiplicador
		        };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCentroDistribucion_ModificarCobranza", ref verificador, Parametros, Valores);

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex; 
            }
        }

        public void AgregarCentroDistribucion_Cobranza(List<CentroDistribucion> centroDistribucion, int Id_Cd, Sesion sesion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                EliminarCentroDistribucion_Cobranza(sesion.Id_Emp, Id_Cd, sesion.Emp_Cnx);
                string[] Parametros = {
			        "@Id_Emp"
                    ,"@Id_Cd"                    
                    ,"@Cob_DiaInicio"
                    ,"@Cob_DiaLimite"
                    ,"@Cob_Multiplicador" 
                };
                object[] Valores;
                SqlCommand sqlcmd = default(SqlCommand);
                int verificador = 0;

                for (int x = 0; x < centroDistribucion.Count; x++)
                {
                    Valores = new object[] {
                                            sesion.Id_Emp   
                                            ,Id_Cd         
                                            ,centroDistribucion[x].Cob_DiaInicio
                                            ,centroDistribucion[x].Cob_DiaLimite
			                                ,centroDistribucion[x].Cob_Multiplicador 
		                                    };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatCentroDistribucion_AgregarCobranza", ref verificador, Parametros, Valores);
                }
                CapaDatos.CommitTrans();
               // CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void EliminarCentroDistribucion_Cobranza(int Id_Emp, int Id_Cd, string Emp_Cnx)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = {
			        "@Id_Emp"
                    ,"@Id_Cd"                     
                };
                object[] Valores = {
                   Id_Emp
                    ,Id_Cd    
		        };
                int verificador = 0;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCentroDistribucion_EliminarCobranza", ref verificador, Parametros, Valores);

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }


        public void ConsultarRentabilidadCentroDistribucion(ref List<CentroDistribucion> ListaCentroDistribucion, int Id_Cd, int Id_Emp, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Cd", "@Id_Emp" };

                object[] Valores = { Id_Cd, Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCentroDistribucionRentabilidad_Consultar", ref dr, Parametros, Valores);
                CentroDistribucion centroDistribucion = new CentroDistribucion();
                while (dr.Read())
                {
                    centroDistribucion = new CentroDistribucion();
                    centroDistribucion.Id_Rent = dr.GetInt32(dr.GetOrdinal("Id_Rent"));
                    centroDistribucion.Rent_LInferior = dr.GetDouble(dr.GetOrdinal("Rent_LInferior"));
                    centroDistribucion.Rent_LSuperior = dr.GetDouble(dr.GetOrdinal("Rent_LSuperior"));
                    centroDistribucion.Rent_Multiplicador = dr.GetDouble(dr.GetOrdinal("Rent_Multiplicador"));
                    ListaCentroDistribucion.Add(centroDistribucion);
                }

                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCentroDistribucion_Rentabilidad(ref CentroDistribucion centroDistribucion, Sesion sesion, int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = {
			        "@Id_Emp"
                    ,"@Id_Cd"
                    ,"@Id_Rent"
                    ,"@Rent_LInferior"
                    ,"@Rent_LSuperior"
                    ,"@Rent_Multiplicador"                    
                };
                object[] Valores = {
                    sesion.Id_Emp
                    ,sesion.Id_Cd_Ver                    
                    ,centroDistribucion.Id_Rent
                    ,centroDistribucion.Rent_LInferior
                    ,centroDistribucion.Rent_LSuperior
			        ,centroDistribucion.Rent_Multiplicador
		        };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCentroDistribucion_ModificarRentabilidad", ref verificador, Parametros, Valores);

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void AgregarCentroDistribucion_Rentabilidad(List<CentroDistribucion> centroDistribucion, int Id_Cd, Sesion sesion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();

                EliminarCentroDistribucion_Rentabilidad(sesion.Id_Emp, Id_Cd, sesion.Emp_Cnx);

                string[] Parametros = {
			        "@Id_Emp"
                    ,"@Id_Cd"                    
                    ,"@Rent_LInferior"
                    ,"@Rent_LSuperior"
                    ,"@Rent_Multiplicador" 
                };

                object[] Valores;
                SqlCommand sqlcmd = default(SqlCommand);
                int verificador = 0;

                for (int x = 0; x < centroDistribucion.Count; x++)
                {
                    Valores = new object[] { 
                        sesion.Id_Emp, 
                        sesion.Id_Cd_Ver, 
                        centroDistribucion[x].Rent_LInferior, 
                        centroDistribucion[x].Rent_LSuperior,
                        centroDistribucion[x].Rent_Multiplicador 
                    };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatCentroDistribucion_AgregarRentabilidad", ref verificador, Parametros, Valores);
                }
                CapaDatos.CommitTrans();
                //CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void EliminarCentroDistribucion_Rentabilidad(int Id_Emp, int Id_Cd, string Emp_Cnx)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = {
			        "@Id_Emp"
                    ,"@Id_Cd"                  
                };
                object[] Valores = {
                    Id_Emp
                    ,Id_Cd
                                   };
                int verificador = 0;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCentroDistribucion_EliminarRentabilidad", ref verificador, Parametros, Valores);

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }


        public void ConsultaCd_Usuario(ref List<CentroDistribucion> list, int Id_Emp, int? Id_U, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_U" };

                object[] Valores = { Id_Emp, Id_U };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCdUsuario_Consultar", ref dr, Parametros, Valores);
                CentroDistribucion centroDistribucion = new CentroDistribucion();
                while (dr.Read())
                {
                    centroDistribucion = new CentroDistribucion();
                    centroDistribucion.Cd_Descripcion = dr.GetValue(dr.GetOrdinal("Cd_Nombre")).ToString();
                    centroDistribucion.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    centroDistribucion.Id_U = dr.IsDBNull(dr.GetOrdinal("Id_U")) ? (int?)null: dr.GetInt32(dr.GetOrdinal("Id_U"));
                    list.Add(centroDistribucion);
                }

                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void AgregarZona(string zonas, int Id_Cd, int Id_Emp, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd_Ver", "@Id_CdStr" };

                object[] Valores = { Id_Emp, Id_Cd, zonas };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatZonas_Modificar", ref dr, Parametros, Valores);
                
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
