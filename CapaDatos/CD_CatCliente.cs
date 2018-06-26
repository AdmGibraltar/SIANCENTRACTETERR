using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_CatCliente
    {
        public void ConsultarClienteSigCentroDist(ref int verificador, int Id_Emp, int Id_Cd_Ver, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { Id_Emp, Id_Cd_Ver };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteClaveSig_Consulta", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarClientes(Clientes clientes, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                //CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp",
		                                "@Id_Cd", 
                                        "@Id_Cte", 
                                        "@Id_Cfe", 
                                        "@Id_Corp",
		                                "@Cte_NomComercial", 
                                        "@Cte_NomCorto",
		                                "@Cte_FacCalle", 
		                                "@Cte_FacNumero", 
		                                "@Cte_FacCp", 
		                                "@Cte_FacColonia", 
		                                "@Cte_FacMunicipio", 
		                                "@Cte_FacTel", 
		                                "@Cte_FacRfc", 
		                                "@Cte_FacEstado", 
		                                "@Cte_Calle", 
		                                "@Cte_Numero", 
		                                "@Cte_Cp", 
		                                "@Cte_Colonia", 
		                                "@Cte_Municipio", 
		                                "@Cte_Estado", 
		                                "@Cte_Telefono", 
		                                "@Cte_Fax",
                                        "@Cte_Rfc",
		                                "@Cte_Contacto", 
		                                "@Cte_Tipo", 
		                                "@Cte_Email", 
		                                "@Cte_Credito", 
		                                "@Cte_Facturacion", 

		                                "@Id_Mon", 
		                                "@Cte_LimCobr", 
		                                "@Cte_RHoraam1", 
                                        "@Cte_RHoraam2", 
                                        "@Cte_RHorapm1", 
                                        "@Cte_RHorapm2", 
		                                "@Cte_RLunes", 
		                                "@Cte_RMartes", 
		                                "@Cte_RMiercoles", 
		                                "@Cte_RJueves", 
		                                "@Cte_RViernes", 
		                                "@Cte_RSabado", 
		                                "@Cte_RDomingo", 
		                                "@Cte_CondPago", 
		                                "@Cte_CPLunes", 
		                                "@Cte_CPMartes", 
		                                "@Cte_CPMiercoles", 
		                                "@Cte_CPJueves", 
		                                "@Cte_CPViernes", 
		                                "@Cte_CPSabado", 
		                                "@Cte_CPDomingo", 
		                                "@Cte_Comisiones", 
		                                "@Cte_DesgIva", 
		                                "@Cte_RetIva", 
		                                "@Cte_AsignacionPed", 
		                                "@Id_Ade", 
                                        "@Cte_SerieNcre",
                                        "@Cte_SerieNca",
		                                "@Cte_Activo",
                                        "@Cte_CreditoSuspendido",
                                        "@Cte_PHoraam1",
                                        "@Cte_PHoraam2",
                                        "@Cte_PHorapm1",
                                        "@Cte_PHorapm2",
                                        "@Cte_SemRec",
                                        "@Cte_RecLunes",
                                        "@Cte_RecMartes",
                                        "@Cte_RecMiercoles",
                                        "@Cte_RecJueves",
                                        "@Cte_RecViernes",
                                        "@Cte_RecSabado",
                                        "@Cte_RecDomingo",
                                        "@Cte_Efectivo",
                                        "@Cte_Factoraje",
                                        "@Cte_Cheque",
                                        "@Cte_Transferencia",
                                        "@Cte_ReqOrdenCompra",
                                        "@Cte_Documentos",
                                        "@Cte_TelCobranza1",
                                        "@Cte_TelCobranza2",
                                        "@Cte_RemisionElect",
                                        "@Cte_BPorcNotaCredito",
                                        "@Cte_PorcNotaCredito",
                                        "@Cte_PorcientoRetencion",
                                        "@Cte_BPorcientoIVA",                                       
                                        "@Cte_PorcientoIVA",
                                        "@Cte_UDigitos",
                                        "@Cte_Referencia",
                                        "@Cte_AutorizaPlazo_IdU",
                                        "@Cte_AutorizaPlazo_IdCd",
                                        "@Cte_Correo1",
                                        "@Cte_Correo2",
                                        "@Cte_Correo3",
                                        "@Cte_NumCuentaContNacional",
                                        "@Cte_SemRev",
                                         "@Cte_SemCob"
                                      };
                object[] Valores = { 
                                        clientes.Id_Emp,   
                                        clientes.Id_Cd, 
                                        clientes.Id_Cte, 
                                        clientes.Id_Cfe == -1 ? (object)null : clientes.Id_Cfe,
                                        clientes.Id_Corp == -1 ? (object)null : clientes.Id_Corp,
                                        clientes.Cte_NomComercial,
                                        clientes.Cte_NomCorto,
                                        clientes.Cte_FacCalle, 
                                        clientes.Cte_FacNumero, 
                                        clientes.Cte_FacCp, 
                                        clientes.Cte_FacColonia, 
                                        clientes.Cte_FacMunicipio, 
                                        clientes.Cte_FacTel, 
                                        clientes.Cte_FacRfc, 
                                        clientes.Cte_FacEstado, 
                                        clientes.Cte_Calle, 
                                        clientes.Cte_Numero, 
                                        clientes.Cte_Cp, 
                                        clientes.Cte_Colonia, 
                                        clientes.Cte_Municipio, 
                                        clientes.Cte_Estado, 
                                        clientes.Cte_Telefono, 
                                        clientes.Cte_Fax,
                                        clientes.Cte_DRfc,
                                        clientes.Cte_Contacto, 
                                        clientes.Cte_Tipo, 
                                        clientes.Cte_Email, 
                                        clientes.Cte_Credito, 
                                        clientes.Cte_Facturacion, 
                                    
                                        clientes.Id_Mon == -1 ? (object)null : clientes.Id_Mon,
                                        clientes.Cte_LimCobr, 
                                        clientes.Cte_RHoraam1, 
                                        clientes.Cte_RHoraam2, 
                                        clientes.Cte_RHorapm1, 
                                        clientes.Cte_RHorapm2, 
                                        clientes.Cte_RLunes, 
                                        clientes.Cte_RMartes, 
                                        clientes.Cte_RMiercoles, 
                                        clientes.Cte_RJueves, 
                                        clientes.Cte_RViernes, 
                                        clientes.Cte_RSabado, 
                                        clientes.Cte_RDomingo, 
                                        clientes.Cte_CondPago, 
                                        clientes.Cte_CPLunes, 
                                        clientes.Cte_CPMartes, 
                                        clientes.Cte_CPMiercoles, 
                                        clientes.Cte_CPJueves, 
                                        clientes.Cte_CPViernes, 
                                        clientes.Cte_CPSabado, 
                                        clientes.Cte_CPDomingo, 
                                        clientes.Cte_Comisiones, 
                                        clientes.Cte_DesgIva, 
                                        clientes.Cte_RetIva, 
                                        clientes.Cte_AsignacionPed == -1 ? (object)null : clientes.Cte_AsignacionPed,
                                        clientes.Id_Ade == -1 ? (object)null : clientes.Id_Ade,
                                        clientes.Cte_SerieNCre == -1 ? (object)null : clientes.Cte_SerieNCre,
                                        clientes.Cte_SerieNCa == -1 ? (object)null : clientes.Cte_SerieNCa,
                                        clientes.Estatus ,

                                        clientes.Cte_CreditoSuspendido ,
                                        clientes.Cte_PHoraam1,
                                        clientes.Cte_PHoraam2,
                                        clientes.Cte_PHorapm1,
                                        clientes.Cte_PHorapm2,
                                        clientes.Cte_SemRec ,
                                        clientes.Cte_RecLunes ,
                                        clientes.Cte_RecMartes ,
                                        clientes.Cte_RecMiercoles ,
                                        clientes.Cte_RecJueves,
                                        clientes.Cte_RecViernes,
                                        clientes.Cte_RecSabado,
                                        clientes.Cte_RecDomingo,
                                        clientes.Cte_Efectivo,
                                        clientes.Cte_Factoraje,
                                        clientes.Cte_Cheque,
                                        clientes.Cte_Transferencia,
                                        clientes.Cte_ReqOrdenCompra,
                                        clientes.Cte_Documentos,
                                        clientes.Cte_TelCobranza1,
                                        clientes.Cte_TelCobranza2,
                                        clientes.Cte_RemisionElectronica,
                                        clientes.BPorcNotaCredito,
                                        clientes.PorcientoNotaCredito,
                                        clientes.PorcientoRetencion,
                                        clientes.BPorcientoIVA,
                                        clientes.PorcientoIVA,
                                        clientes.Cte_UDigitos,
                                        clientes.Cte_Referencia,
                                        clientes.Cte_AutorizaPlazo_IdU,
                                        clientes.Cte_AutorizaPlazo_IdCd,

                                        clientes.Cte_CorreoEdoCuenta1,
                                        clientes.Cte_CorreoEdoCuenta2,
                                        clientes.Cte_CorreoEdoCuenta3,
                                        clientes.Cte_NumCuentaContNacional,
                                        clientes.Cte_SemRev,
                                        clientes.Cte_SemCob
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCliente_Insertar", ref verificador, Parametros, Valores);

                //CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ModificarClientes(Clientes clientes, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] Parametros = { 
	                                    "@Id_Emp",
		                                "@Id_Cd", 
		                                "@Id_Cte", 
                                        "@Id_Cfe", 
                                        "@Id_Corp",
		                                "@Cte_NomComercial", 
                                        "@Cte_NomCorto", 
		                                "@Cte_FacCalle", 
		                                "@Cte_FacNumero", 
		                                "@Cte_FacCp", 
		                                "@Cte_FacColonia", 
		                                "@Cte_FacMunicipio", 
		                                "@Cte_FacTel", 
		                                "@Cte_FacRfc", 
		                                "@Cte_FacEstado", 
		                                "@Cte_Calle", 
		                                "@Cte_Numero", 
		                                "@Cte_Cp", 
		                                "@Cte_Colonia", 
		                                "@Cte_Municipio", 
		                                "@Cte_Estado", 
		                                "@Cte_Telefono", 
		                                "@Cte_Fax",
                                        "@Cte_Rfc",
		                                "@Cte_Contacto", 
		                                "@Cte_Tipo", 
		                                "@Cte_Email", 
		                                "@Cte_Credito", 
		                                "@Cte_Facturacion", 
		                                "@Id_Mon", 
		                                "@Cte_LimCobr", 
		                                "@Cte_RHoraam1", 
                                        "@Cte_RHoraam2", 
                                        "@Cte_RHorapm1", 
                                        "@Cte_RHorapm2", 
		                                "@Cte_RLunes", 
		                                "@Cte_RMartes", 
		                                "@Cte_RMiercoles", 
		                                "@Cte_RJueves", 
		                                "@Cte_RViernes", 
		                                "@Cte_RSabado", 
		                                "@Cte_RDomingo", 
		                                "@Cte_CondPago", 
		                                "@Cte_CPLunes", 
		                                "@Cte_CPMartes", 
		                                "@Cte_CPMiercoles", 
		                                "@Cte_CPJueves", 
		                                "@Cte_CPViernes", 
		                                "@Cte_CPSabado", 
		                                "@Cte_CPDomingo", 
		                                "@Cte_Comisiones", 
		                                "@Cte_DesgIva", 
		                                "@Cte_RetIva", 
		                                "@Cte_AsignacionPed", 
		                                "@Id_Ade", 
                                        "@Cte_SerieNcre",
                                        "@Cte_SerieNca",
		                                "@Cte_Activo",
                                        "@Cte_CreditoSuspendido",
                                        "@Cte_PHoraam1",
                                        "@Cte_PHoraam2",
                                        "@Cte_PHorapm1",
                                        "@Cte_PHorapm2",
                                        "@Cte_SemRec",
                                        "@Cte_RecLunes",
                                        "@Cte_RecMartes",
                                        "@Cte_RecMiercoles",
                                        "@Cte_RecJueves",
                                        "@Cte_RecViernes",
                                        "@Cte_RecSabado",
                                        "@Cte_RecDomingo",
                                        "@Cte_Efectivo",
                                        "@Cte_Factoraje",
                                        "@Cte_Cheque",
                                        "@Cte_Transferencia",
                                        "@Cte_ReqOrdenCompra",
                                        "@Cte_Documentos",
                                        "@Cte_TelCobranza1",
                                        "@Cte_TelCobranza2",
                                        "@Cte_RemisionElect",
                                        "@Cte_BPorcNotaCredito",
                                        "@Cte_PorcNotaCredito",
                                        "@Cte_PorcientoRetencion",
                                        "@Cte_BPorcientoIVA",                                       
                                        "@Cte_PorcientoIVA",
                                        "@Cte_UDigitos",
                                        "@Cte_Referencia",
                                        "@Id_U",
                                        "@Id_UCd",
                                        "@Db",
                                        "@Db_Cobranza",
                                        "@Cte_AutorizaPlazo_IdU",
                                        "@Cte_AutorizaPlazo_IdCd",
                                        "@Cte_Correo1",
                                        "@Cte_Correo2",
                                        "@Cte_Correo3",
                                        "@Cte_NumCuentaContNacional",
                                         "@Cte_SemRev",
                                         "@Cte_SemCob"

                                      };
                object[] Valores = { 
                                        clientes.Id_Emp,   
                                        clientes.Id_Cd, 
                                        clientes.Id_Cte, 
                                        clientes.Id_Cfe == -1 ? (object)null : clientes.Id_Cfe,                                      
                                        clientes.Id_Corp == -1 ? (object)null : clientes.Id_Corp,
                                        clientes.Cte_NomComercial, 
                                        clientes.Cte_NomCorto, 
                                        clientes.Cte_FacCalle, 
                                        clientes.Cte_FacNumero, 
                                        clientes.Cte_FacCp, 
                                        clientes.Cte_FacColonia, 
                                        clientes.Cte_FacMunicipio, 
                                        clientes.Cte_FacTel, 
                                        clientes.Cte_FacRfc, 
                                        clientes.Cte_FacEstado, 
                                        clientes.Cte_Calle, 
                                        clientes.Cte_Numero, 
                                        clientes.Cte_Cp, 
                                        clientes.Cte_Colonia, 
                                        clientes.Cte_Municipio, 
                                        clientes.Cte_Estado, 
                                        clientes.Cte_Telefono, 
                                        clientes.Cte_Fax,
                                        clientes.Cte_DRfc,
                                        clientes.Cte_Contacto, 
                                        clientes.Cte_Tipo, 
                                        clientes.Cte_Email, 
                                        clientes.Cte_Credito, 
                                        clientes.Cte_Facturacion, 
                                        clientes.Id_Mon == -1 ? (object)null : clientes.Id_Mon,
                                        clientes.Cte_LimCobr, 
                                        clientes.Cte_RHoraam1, 
                                        clientes.Cte_RHoraam2,
                                        clientes.Cte_RHorapm1,
                                        clientes.Cte_RHorapm2,
                                        clientes.Cte_RLunes, 
                                        clientes.Cte_RMartes, 
                                        clientes.Cte_RMiercoles, 
                                        clientes.Cte_RJueves, 
                                        clientes.Cte_RViernes, 
                                        clientes.Cte_RSabado, 
                                        clientes.Cte_RDomingo, 
                                        clientes.Cte_CondPago, 
                                        clientes.Cte_CPLunes, 
                                        clientes.Cte_CPMartes, 
                                        clientes.Cte_CPMiercoles, 
                                        clientes.Cte_CPJueves, 
                                        clientes.Cte_CPViernes, 
                                        clientes.Cte_CPSabado, 
                                        clientes.Cte_CPDomingo, 
                                        clientes.Cte_Comisiones, 
                                        clientes.Cte_DesgIva, 
                                        clientes.Cte_RetIva, 
                                        clientes.Cte_AsignacionPed == -1 ? (object)null : clientes.Cte_AsignacionPed,
                                        clientes.Id_Ade == -1 ? (object)null : clientes.Id_Ade,
                                        clientes.Cte_SerieNCre == -1 ? (object)null : clientes.Cte_SerieNCre,
                                        clientes.Cte_SerieNCa == -1 ? (object)null : clientes.Cte_SerieNCa,
                                        clientes.Estatus,
                                        clientes.Cte_CreditoSuspendido ,
                                        clientes.Cte_PHoraam1 ,
                                        clientes.Cte_PHoraam2 ,
                                        clientes.Cte_PHorapm1 ,
                                        clientes.Cte_PHorapm2 ,
                                        clientes.Cte_SemRec ,
                                        clientes.Cte_RecLunes ,
                                        clientes.Cte_RecMartes ,
                                        clientes.Cte_RecMiercoles ,
                                        clientes.Cte_RecJueves,
                                        clientes.Cte_RecViernes,
                                        clientes.Cte_RecSabado,
                                        clientes.Cte_RecDomingo,
                                        clientes.Cte_Efectivo,
                                        clientes.Cte_Factoraje,
                                        clientes.Cte_Cheque,
                                        clientes.Cte_Transferencia,
                                        clientes.Cte_ReqOrdenCompra,
                                        clientes.Cte_Documentos,
                                        clientes.Cte_TelCobranza1,
                                        clientes.Cte_TelCobranza2,
                                        clientes.Cte_RemisionElectronica,
                                        clientes.BPorcNotaCredito,
                                        clientes.PorcientoNotaCredito,
                                        clientes.PorcientoRetencion,
                                        clientes.BPorcientoIVA,
                                        clientes.PorcientoIVA,
                                        clientes.Cte_UDigitos,
                                        clientes.Cte_Referencia,
                                        clientes.Id_U,
                                        clientes.Id_UCd,
                                        clientes.Db,
                                        clientes.Db_Cobranza,
                                        clientes.Cte_AutorizaPlazo_IdU,
                                        clientes.Cte_AutorizaPlazo_IdCd,
                                        clientes.Cte_CorreoEdoCuenta1,
                                        clientes.Cte_CorreoEdoCuenta2,
                                        clientes.Cte_CorreoEdoCuenta3,
                                        clientes.Cte_NumCuentaContNacional,
                                        clientes.Cte_SemRev,
                                        clientes.Cte_SemCob
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCliente_Modificar", ref verificador, Parametros, Valores);

                int verificador2 = 0;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Fpa", "@Contador" };
                int contador = 0;
                foreach (FormaPago dr in clientes.FormasPago)
                {
                    Valores = new object[] { clientes.Id_Emp, clientes.Id_Cd, clientes.Id_Cte, dr.Id_Fpa, contador };
                    sqlcmd = CapaDatos.GenerarSqlCommand("CatClienteFPago_Insertar", ref verificador2, Parametros, Valores);
                    contador = 1;
                }


                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ConsultarCliente(Clientes cte, string Conexion)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Rik", "@Id_Ter", "@Ignora_Activo" };
                object[] Valores = { 
                                       cte.Id_Emp, 
                                       cte.Id_Cd, 
                                       cte.Id_Cte, 
                                       cte.Id_Rik <= 0 ? (object)null : cte.Id_Rik, 
                                       cte.Id_Terr <= 0 ? (object)null : cte.Id_Terr,
                                       cte.Ignora_Inactivo
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCliente_Consulta", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    cte.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    cte.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    cte.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    cte.Id_Rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Rik"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    cte.Id_Cfe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cfe"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cfe")));
                    cte.Id_Corp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Corp"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Corp")));
                    cte.FacSerie = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FacSerie"))) ? "" :  dr.GetValue(dr.GetOrdinal("FacSerie")).ToString();
                    cte.Cte_NomComercial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_NomComercial"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    cte.Cte_NomCorto = dr.IsDBNull(dr.GetOrdinal("Cte_NomCorto")) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_NomCorto"));
                    cte.Cte_FacCalle = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacCalle"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacCalle"));
                    cte.Cte_FacNumero = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacNumero"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacNumero"));
                    cte.Cte_FacCp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacCp"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacCp"));
                    cte.Cte_FacColonia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacColonia"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacColonia"));
                    cte.Cte_FacMunicipio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacMunicipio"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacMunicipio"));
                    cte.Cte_FacTel = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacTel"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacTel"));
                    cte.Cte_FacRfc = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacRfc"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacRfc"));
                    cte.Cte_FacEstado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacEstado"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacEstado"));
                    cte.Cte_Calle = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Calle"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Calle"));
                    cte.Cte_Numero = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Numero"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Numero"));
                    string cp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Cp"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Cp"));
                    cte.Cte_Cp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Cp"))) ? "" : dr.GetValue(dr.GetOrdinal("Cte_Cp")).ToString();
                    cte.Cte_Colonia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Colonia"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Colonia"));
                    cte.Cte_Municipio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Municipio"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Municipio"));
                    cte.Cte_Estado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Estado"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Estado"));
                    cte.Cte_DRfc = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Rfc"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Rfc"));
                    cte.Cte_Telefono = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Telefono"))) ? "" : dr.GetValue(dr.GetOrdinal("Cte_Telefono")).ToString();
                    cte.Cte_Fax = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Fax"))) ? "" : dr.GetValue(dr.GetOrdinal("Cte_Fax")).ToString();
                    cte.Cte_Contacto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Contacto"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Contacto"));
                    cte.Cte_Tipo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Tipo"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_Tipo")));
                    cte.Cte_Email = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Email"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Email"));
                    cte.Cte_Credito = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Credito"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Credito"));
                    cte.Cte_Facturacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Facturacion"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Facturacion"));
                    cte.Id_Mon = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Mon"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Mon")));
                    cte.Cte_LimCobr = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_LimCobr"))) ? 0 : (double)dr.GetValue(dr.GetOrdinal("Cte_LimCobr"));
                    cte.Cte_RHoraam1 = dr.IsDBNull(dr.GetOrdinal("Cte_RHoraam1")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_RHoraam1"));
                    cte.Cte_RHoraam2 = dr.IsDBNull(dr.GetOrdinal("Cte_RHoraam2")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_RHoraam2"));
                    cte.Cte_RHorapm1 = dr.IsDBNull(dr.GetOrdinal("Cte_RHorapm1")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_RHorapm1"));
                    cte.Cte_RHorapm2 = dr.IsDBNull(dr.GetOrdinal("Cte_RHorapm2")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_RHorapm2"));
                    cte.Cte_RLunes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RLunes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RLunes"));
                    cte.Cte_RMartes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RMartes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RMartes"));
                    cte.Cte_RMiercoles = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RMiercoles"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RMiercoles"));
                    cte.Cte_RJueves = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RJueves"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RJueves"));
                    cte.Cte_RViernes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RViernes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RViernes"));
                    cte.Cte_RSabado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RSabado"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RSabado"));
                    cte.Cte_RDomingo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RDomingo"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RDomingo"));
                    cte.Cte_CondPago = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CondPago"))) ? 0 : (int)dr.GetValue(dr.GetOrdinal("Cte_CondPago"));
                    cte.Cte_CPLunes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CPLunes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CPLunes"));
                    cte.Cte_CPMartes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CPMartes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CPMartes"));
                    cte.Cte_CPMiercoles = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CPMiercoles"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CPMiercoles"));
                    cte.Cte_CPJueves = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CPJueves"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CPJueves"));
                    cte.Cte_CPViernes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CPViernes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CPViernes"));
                    cte.Cte_CPSabado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CPSabado"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CPSabado"));
                    cte.Cte_CPDomingo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CPDomingo"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CPDomingo"));
                    cte.Cte_Comisiones = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Comisiones"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Comisiones"));
                    cte.Cte_DesgIva = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_DesgIva"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_DesgIva"));
                    cte.Cte_RetIva = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RetIva"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RetIva"));
                    cte.Cte_AsignacionPed = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_AsignacionPed"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_AsignacionPed")));
                    cte.Id_Ade = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ade"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ade")));
                    cte.Cte_SerieNCre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_SerieNCre"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_SerieNCre")));
                    cte.Cte_SerieNCa = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_SerieNCa"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_SerieNCa")));
                    cte.Estatus = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Activo"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Activo"));

                    cte.Cte_CreditoSuspendido = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido"));
                    cte.Cte_PHoraam1 = dr.IsDBNull(dr.GetOrdinal("Cte_PHoraam1")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_PHoraam1"));
                    cte.Cte_PHoraam2 = dr.IsDBNull(dr.GetOrdinal("Cte_PHoraam2")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_PHoraam2"));
                    cte.Cte_PHorapm1 = dr.IsDBNull(dr.GetOrdinal("Cte_PHorapm1")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_PHorapm1"));
                    cte.Cte_PHorapm2 = dr.IsDBNull(dr.GetOrdinal("Cte_PHorapm2")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_PHorapm2"));
                    cte.Cte_SemRec = dr.IsDBNull(dr.GetOrdinal("Cte_SemRec")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_SemRec")));
                    cte.Cte_SemRev = dr.IsDBNull(dr.GetOrdinal("Cte_SemRev")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_SemRev")));
                    cte.Cte_SemCob = dr.IsDBNull(dr.GetOrdinal("Cte_SemCob")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_SemCob")));
                    cte.Cte_RecLunes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RecLunes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RecLunes"));
                    cte.Cte_RecMartes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RecMartes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RecMartes"));
                    cte.Cte_RecMiercoles = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RecMiercoles"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RecMiercoles"));
                    cte.Cte_RecJueves = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RecJueves"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RecJueves"));
                    cte.Cte_RecViernes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RecViernes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RecViernes"));
                    cte.Cte_RecSabado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RecSabado"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RecSabado"));
                    cte.Cte_RecDomingo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RecDomingo"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RecDomingo"));
                    cte.Cte_Efectivo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Efectivo"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Efectivo"));
                    cte.Cte_Factoraje = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Factoraje"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Factoraje"));
                    cte.Cte_Cheque = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Cheque"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Cheque"));
                    cte.Cte_Transferencia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Transferencia"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Transferencia"));
                    cte.Cte_ReqOrdenCompra = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_ReqOrdenCompra"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_ReqOrdenCompra"));
                    cte.Cte_Documentos = dr.IsDBNull(dr.GetOrdinal("Cte_Documentos")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_Documentos")).ToString();
                    cte.Ade_Nombre = dr.IsDBNull(dr.GetOrdinal("Ade_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("Ade_Nombre")).ToString();
                    cte.Cte_TelCobranza1 = dr.IsDBNull(dr.GetOrdinal("Cte_TelCobranza1")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_TelCobranza1")).ToString();
                    cte.Cte_TelCobranza2 = dr.IsDBNull(dr.GetOrdinal("Cte_TelCobranza2")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_TelCobranza2")).ToString();
                    cte.Cte_RemisionElectronica = dr.IsDBNull(dr.GetOrdinal("Cte_RemisionElectronica")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_RemisionElectronica")));
                    cte.Cte_NumCuentaContNacional = dr.IsDBNull(dr.GetOrdinal("Cte_NumCuentaContNacional")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_NumCuentaContNacional")));
                    cte.BPorcNotaCredito = dr.IsDBNull(dr.GetOrdinal("Cte_NCredito")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_NCredito")));
                    cte.PorcientoNotaCredito = dr.IsDBNull(dr.GetOrdinal("Cte_PorcNCredito")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cte_PorcNCredito")));
                    cte.PorcientoRetencion = dr.IsDBNull(dr.GetOrdinal("Cte_PorcRetencion")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cte_PorcRetencion")));
                    cte.BPorcientoIVA = dr.IsDBNull(dr.GetOrdinal("Cte_BPorcientoIVA")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_BPorcientoIVA")));
                    cte.PorcientoIVA = dr.IsDBNull(dr.GetOrdinal("Cte_PorcientoIVA")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_PorcientoIVA")));          
                    cte.Cte_UDigitos = dr.IsDBNull(dr.GetOrdinal("Cte_UDigitos")) ? "": dr.GetValue(dr.GetOrdinal("Cte_UDigitos")).ToString();
                    cte.Cte_Referencia = dr.IsDBNull(dr.GetOrdinal("Cte_Referencia")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_Referencia")).ToString();
                    //TODO: QUITAR COMENTARIOS
                    try { cte.Cte_DiasVencidos = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("DiasVencidos"))); }
                    catch { }

                    try { cte.Cte_MotCreditoSuspendido = dr.GetValue(dr.GetOrdinal("Cte_MotCreditoSuspendido")).ToString(); }
                    catch { }

                    try { cte.Cte_Referencia = dr.IsDBNull(dr.GetOrdinal("Cte_Referencia")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_Referencia")).ToString(); }
                    catch { }

                    try { cte.UPlazo = dr.IsDBNull(dr.GetOrdinal("UPlazo")) ? "" : dr.GetValue(dr.GetOrdinal("UPlazo")).ToString(); }
                    catch { }

                    if (Convert.ToBoolean(cte.Estatus))
                        cte.EstatusStr = "Activo";
                    else
                        cte.EstatusStr = "Inactivo";

                    cte.Cte_CorreoEdoCuenta1 = dr.GetValue(dr.GetOrdinal("Cte_Correo1")).ToString();
                    cte.Cte_CorreoEdoCuenta2 = dr.GetValue(dr.GetOrdinal("Cte_Correo2")).ToString();
                    cte.Cte_CorreoEdoCuenta3 = dr.GetValue(dr.GetOrdinal("Cte_Correo3")).ToString();
                }



                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Cte" };
                object[] Valores = new object[] { cte.Id_Emp, cte.Id_Cd, cte.Id_Cte };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("CatClienteFPago_Consultar", ref dr, Parametros, Valores);
                List<FormaPago> listFP = new List<FormaPago>();
                FormaPago FP;
                while (dr.Read())
                {
                    FP = new FormaPago();
                    FP.Id_Fpa = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fpa")));
                    listFP.Add(FP);
                }
                cte.FormasPago = listFP;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void ConsultarClienteDet(ClienteDet clientedet, string Conexion, ref DataTable dt)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte" };
                object[] Valores = { clientedet.Id_Emp, clientedet.Id_Cd, clientedet.Id_Cte };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteDet_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    dt.Rows.Add(new object[] {
                    dr.GetValue(dr.GetOrdinal("Id_CteDet")),
                    dr.GetValue(dr.GetOrdinal("Id_Ter")),
                    dr.GetValue(dr.GetOrdinal("Ter_Nombre")),
                    dr.GetValue(dr.GetOrdinal("Id_Seg")),
                    dr.GetValue(dr.GetOrdinal("Seg_Descripcion")),
                    dr.GetValue(dr.GetOrdinal("Cte_UnidadDim")),
                    Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cte_Dimension"))).ToString("#,##0.00"),
                    dr.GetValue(dr.GetOrdinal("Cte_Pesos")),
                    dr.GetValue(dr.GetOrdinal("Cte_Potencial")),
                    dr.GetValue(dr.GetOrdinal("Cte_Activo")),
                    dr.GetValue(dr.GetOrdinal("Id_Rik")),
                    dr.GetValue(dr.GetOrdinal("Rik_Nombre")),
                  
                    dr.GetValue(dr.GetOrdinal("Uen_Descripcion")),
                    dr.GetValue(dr.GetOrdinal("Cte_ManoObra")),
                    dr.GetValue(dr.GetOrdinal("Cte_GastoTerritorio")),
                    dr.GetValue(dr.GetOrdinal("Cte_FletePaga")),
                    dr.GetValue(dr.GetOrdinal("Cte_PorcComision")),
                    dr.GetValue(dr.GetOrdinal("Id_Uen")),
                    dr.GetValue(dr.GetOrdinal("Editable"))

                    });
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarClienteDet(Clientes clientes, DataTable dt, string Conexion)
        {
            if (dt.Rows.Count == 0) return;

            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            int verificador = 0;
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Cte", 
                                        "@Id_CteDet",
	                                    "@Id_Ter", 
	                                    "@Id_Seg", 
	                                    "@Cte_UnidadDim",
                                        "@Cte_Dimension",
                                        "@Cte_Pesos",
                                        "@Cte_Potencial",
                                        "@Cte_CarMP", 
		                                "@Cte_GasVarT", 
                                        "@Cte_FletePaga",
                                        "@Cte_PorcComision",
                                        "@Cte_Activo",
                                        "@Accion"
                                      };

                object[] Valores = null;
                SqlCommand sqlcmd = null;

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    Valores = new object[] { 
                                        clientes.Id_Emp,
                                        clientes.Id_Cd,
                                        clientes.Id_Cte,
                                        x,
                                        dt.Rows[x]["Id_Ter"],
                                        dt.Rows[x]["Id_Seg"],
                                        dt.Rows[x]["Cte_UnidadDim"],
                                        dt.Rows[x]["Cte_Dimension"],
                                        dt.Rows[x]["Cte_Pesos"],
                                        dt.Rows[x]["Cte_Potencial"],
                                        dt.Rows[x]["Cte_ManoObra"],
                                        dt.Rows[x]["Cte_GastoTerritorio"],
                                        dt.Rows[x]["Cte_FletePaga"],
                                        dt.Rows[x]["Cte_PorcComision"],
                                        dt.Rows[x]["Cte_Activo"],
                                        x
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteDet_Insertar", ref verificador, Parametros, Valores);
                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        /// <summary>
        /// Recibe un id de cliente, sesion(id_emp,id_cd_ver) y devulve una lista de los territorios a los que pertenece 
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="sesion"></param>
        /// <param name="territorios"></param>
        public void ConsultaTerritoriosDelCliente(int id_cliente, CapaEntidad.Sesion sesion, ref List<Territorios> territorios)
        {//RM
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] parametros = { 
                                          "@Id1",
                                          "@Id2",
                                          "@Id3"
                                      };
                string[] Valores = {
                                       sesion.Id_Emp.ToString(),
                                       sesion.Id_Cd_Ver.ToString(),
                                       id_cliente.ToString()
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorioCte_Combo", ref dr, parametros, Valores);

                Territorios territorio = new Territorios();
                while (dr.Read())
                {
                    territorio = new Territorios();
                    territorio.Id_Ter = dr.GetInt32(dr.GetOrdinal("Id"));
                    territorio.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    territorio.Id_Rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Rik"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    territorio.Rik_Nombre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rik_Nombre"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    territorios.Add(territorio);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Recibe un id de cliente, sesion(id_emp,id_cd_ver) y devulve una lista de los territorios a los que pertenece 
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="sesion"></param>
        /// <param name="territorios"></param>
        public void ConsultaTodosTerritoriosDelCliente(int id_cliente, CapaEntidad.Sesion sesion, ref List<Territorios> territorios)
        {//RM
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] parametros = { 
                                          "@Id1",
                                          "@Id2",
                                          "@Id3"
                                      };
                string[] Valores = {
                                       sesion.Id_Emp.ToString(),
                                       sesion.Id_Cd_Ver.ToString(),
                                       id_cliente.ToString()
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorioCteTodos_Combo", ref dr, parametros, Valores);

                Territorios territorio = new Territorios();
                while (dr.Read())
                {
                    territorio = new Territorios();
                    territorio.Id_Ter = dr.GetInt32(dr.GetOrdinal("Id"));
                    territorio.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    territorio.Id_Rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Rik"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    territorio.Rik_Nombre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rik_Nombre"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    territorios.Add(territorio);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd_Ver"
                                          ,"@Id_Cte"
                                          ,"@Id_Ter"
                                          ,"@periodo"
                                          ,"@ventas"
                                      };
                object[] Valores = { 
                                       Id_Emp
                                       ,Id_Cd_Ver
                                       ,Id_Cte
                                       ,Id_Ter
                                       ,periodo
                                       ,ventas
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRep_VenRentabilidad_Ventas", "tabla", ref dt, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Lista(Clientes cte, ref List<Clientes> List, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" ,
                                          "@Id_Cd",
                                          "@Id_Uen",
                                          "@Id_Seg",
                                          "@Id_Ter",
                                          "@Id_Rik",
                                          "@Id_Cte",
                                          "@Cte_Nombre"
                                      };
                object[] Valores = { 
                                       cte.Id_Emp,
                                       cte.Id_Cd,
                                       cte.Id_Uen,
                                       cte.Id_Seg,
                                       cte.Id_Terr,
                                       cte.Id_Rik,
                                       cte.Id_Cte,
                                       cte.Cte_NomComercial
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCliente_Lista", ref dr, Parametros, Valores);

                Clientes cliente;
                while (dr.Read())
                {
                    cliente = new Clientes();
                    cliente.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    cliente.Cte_NomComercial = Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")));
                    cliente.UnidadDimension = Convert.ToString(dr.GetValue(dr.GetOrdinal("UnidadDimension")));
                    cliente.Dimension = Convert.ToString(dr.GetValue(dr.GetOrdinal("Dimension")));
                    cliente.VPTeorico = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VPTeorico")));
                    cliente.VPObservado = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VPObservado")));
                    cliente.Id_Uen = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Uen")));
                    cliente.Id_Seg = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Seg")));
                    cliente.Id_Terr = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    List.Add(cliente);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Cte" ,
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Ter"
                                      };

                object[] Valores = { 
                                       cte.Id_Cte,
                                       cte.Id_Emp,
                                       cte.Id_Cd,
                                       cte.Id_Terr
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMClienteTerritorio", ref dr, Parametros, Valores);


                if (dr.HasRows)
                {
                    dr.Read();

                    cte.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    cte.Cte_NomComercial = Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")));
                    cte.Ter_Nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("Ter_Nombre")));
                    cte.Seg_Descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Seg_Descripcion")));
                    cte.Uen_Descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Uen_Descripcion")));
                    cte.Seg_Unidades = Convert.ToString(dr.GetValue(dr.GetOrdinal("Seg_Unidades")));
                    cte.Seg_ValUniDim = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Seg_ValUniDim")));
                    cte.Cte_Dimension = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cte_Dimension")));
                    cte.Id_Seg = (int?)dr.GetValue(dr.GetOrdinal("Id_Seg"));
                    cte.VPObservado = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VPObservado")));
                    cte.VPTeorico = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VPTeorico")));
                    dr.Close();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spConsulta_Permisos_UEN", ref dr);

                while (dr.Read())
                {
                    dt.Rows.Add(new object[] {
                    dr.GetValue(dr.GetOrdinal("Id_UenPermiso")),
                    dr.GetValue(dr.GetOrdinal("Id_UenPotencial")),                    
                    dr.GetValue(dr.GetOrdinal("Id_UenDimension"))
                    
                    });
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaTipoCDC(int Id_Cd_Ver,ref int Tipo_CDC, string Conexion  )
        {
            try
            {               
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Cd_Ver"};
                object[] valores = { Id_Cd_Ver };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spConsulta_TipoCDC", ref Tipo_CDC, Parametros, valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
               
                TieneCuentaNacional = 0;

                string[] Parametros = {"@Id_Emp", "@Id_Cd", "@Id_Cte" };
                object[] valores = { cte.Id_Emp, cte.Id_Cd, cte.Id_Cte };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spConsulta_ClienteCuentaNacional", ref TieneCuentaNacional, Parametros, valores);

              
                    cte.Cte_RemisionElectronica = Convert.IsDBNull(TieneCuentaNacional) ? -1 : Convert.ToInt32(TieneCuentaNacional);
                    if (cte.Cte_RemisionElectronica != -1)
                    {
                        TieneCuentaNacional = 1;
                    }                

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }







        public void ConsultaFactura_ConsecutivoFacElectronica(int Id_Emp, int Id_Cd, int Id_Cfe, int Cfe_TMov, ref int verificador, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cfe", "@Cfe_TMov" };
                object[] Valores = { Id_Emp, Id_Cd, Id_Cfe == -1 ? (int?)null : Id_Cfe, Cfe_TMov };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_ConsultarConsFacElectronica", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Seg", "@Id_Cte" };
                object[] Valores = { cte.Id_Emp, cte.Id_Cd, cte.Id_Seg, cte.Id_Cte };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMEstructuraSegmento", ref dr, Parametros, Valores);

                //creamos esquema
                DataTable estructura;

                //creamos tabla para guardar los datos
                DataTable dataTable;

                for (int x = 0; x < 4; x++)
                {
                    estructura = dr.GetSchemaTable();
                    dataTable = new DataTable();

                    //generemos la estructura de columnas
                    for (int i = 0; i <= estructura.Rows.Count - 1; i++)
                    {
                        DataRow dataRow = estructura.Rows[i];
                        string columnName = dataRow["ColumnName"].ToString();
                        DataColumn column = new DataColumn(columnName, (Type)dataRow["DataType"]);
                        dataTable.Columns.Add(column);
                    }
                    dsEstructuraSegmento.Tables.Add(dataTable);

                    while (dr.Read())
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (int i = 0; i <= dr.FieldCount - 1; i++)
                        {
                            dataRow[i] = dr.GetValue(i);
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                    if (!dr.NextResult())
                    {
                        break;
                    }
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Id_Seg", 
                                          "@Id_Ter",
                                          "@Id_Cte",
                                          "@Id_Apl",
                                          "@NuevoVPObservado",
                                          "@NuevoVPObservadoApp"
                                      };
                object[] Valores = { 
                                       cte.Id_Emp, 
                                       cte.Id_Cd, 
                                       cte.Id_Seg,
                                       cte.Id_Terr,
                                       cte.Id_Cte,
                                       cte.Id_Apl,
                                       NuevoVPObservado,
                                       NuevoVPObservadoApp
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMVPObservadoCliente_Modificar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Id_Ter",
                                          "@Id_Cte",
                                          "@Dimension",
                                          "@VPTeorico",
                                          "@Fecha"
                                      };
                object[] Valores = { 
                                       cte.Id_Emp, 
                                       cte.Id_Cd, 
                                       cte.Id_Terr,
                                       cte.Id_Cte,
                                       Dimension,
                                       VPTeorico,
                                       Fecha
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMDimensionCliente_Modificar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Seg", "@Id_Cte" };
                object[] Valores = { cte.Id_Emp, cte.Id_Cd, cte.Id_Seg, cte.Id_Cte };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCrmContactosCliente_Lista", ref dr, Parametros, Valores);

                //creamos esquema
                DataTable estructura; //= dr.GetSchemaTable();

                //creamos tabla para guardar los datos
                DataTable dataTable; //= new DataTable();

                for (int x = 0; x < 10; x++)
                {
                    estructura = dr.GetSchemaTable();
                    dataTable = new DataTable();
                    //generemos la estructura de columnas
                    for (int i = 0; i <= estructura.Rows.Count - 1; i++)
                    {
                        DataRow dataRow = estructura.Rows[i];
                        string columnName = dataRow["ColumnName"].ToString();
                        DataColumn column = new DataColumn(columnName, (Type)dataRow["DataType"]);
                        dataTable.Columns.Add(column);
                    }
                    dsContactosClientes.Tables.Add(dataTable);

                    while (dr.Read())
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (int i = 0; i <= dr.FieldCount - 1; i++)
                        {
                            dataRow[i] = dr.GetValue(i);
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                    if (!dr.NextResult())
                    {
                        break;
                    }
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Id_Con"
                                      };
                object[] Valores = { 
                                       
                                       cont.Id_Emp, 
                                       cont.Id_Cd, 
                                       cont.Id_Con,
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMContacto_Eliminar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" ,
                                          "@Id_Cd",
                                          "@Id_Cte",
                                          "@Ade_Tipo"
                                      };
                object[] Valores = { 
                                       Id_Emp,
                                       Id_Cd_Ver,
                                       Id_cte,
                                       Tipo,
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAdendaDet_Consultar", ref dr, Parametros, Valores);
                AdendaDet adendaDet;
                while (dr.Read())
                {
                    adendaDet = new AdendaDet();
                    adendaDet.Id_AdeDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AdeDet")));
                    adendaDet.Campo = Convert.ToString(dr.GetValue(dr.GetOrdinal("Ade_Campo")));
                    adendaDet.Nodo = Convert.ToString(dr.GetValue(dr.GetOrdinal("Ade_Nodo")));
                    adendaDet.Longitud = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ade_Longitud")));
                    adendaDet.Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ade_Tipo")));
                    adendaDet.Requerido = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ade_Requerido")));
                    switch (adendaDet.Tipo)
                    {
                        case 1:
                        case 3:
                        case 5:
                            listCab.Add(adendaDet);
                            break;
                        case 2:
                        case 4:
                        case 6:
                        case 8:
                            listDet.Add(adendaDet);
                            break;
                        case 7:
                            listCabR.Add(adendaDet);
                            break;
                        default:
                            break;
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

        public void ConsultaClientes(Clientes cte, string Conexion, ref List<Comun> List, object FiltroId, object FiltroDesc)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ter", "@FiltroId", "@FiltroDesc" };
                object[] Valores = { cte.Id_Emp, cte.Id_Cd, cte.Id_Terr, FiltroId, FiltroDesc };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCliente_Lista", ref dr, Parametros, Valores);
                Comun cliente;
                while (dr.Read())
                {
                    cliente = new Comun();
                    cliente.IdStr = dr.GetValue(dr.GetOrdinal("Id")).ToString();
                    cliente.Descripcion = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    List.Add(cliente);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@FiltroId", "@FiltroDesc" };
                object[] Valores = { cte.Id_Emp, cte.Id_Cd, cte.Id_Cte, FiltroId, FiltroDesc };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_UltimosPrecios", ref dr, Parametros, Valores);
                Comun cliente;
                while (dr.Read())
                {
                    cliente = new Comun();
                    cliente.IdStr = dr.GetValue(dr.GetOrdinal("Id")).ToString();
                    cliente.Descripcion = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    cliente.ValorDoble = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Precio")));
                    List.Add(cliente);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarCteFormaPago(Clientes clientes, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Fpa", "@Contador" };
                object[] Valores;
                SqlCommand sqlcmd = default(SqlCommand);
                int contador = 0;
                foreach (FormaPago dr in clientes.FormasPago)
                {
                    Valores = new object[] { clientes.Id_Emp, clientes.Id_Cd, clientes.Id_Cte, dr.Id_Fpa, contador };
                    sqlcmd = CapaDatos.GenerarSqlCommand("CatClienteFPago_Insertar", ref verificador, Parametros, Valores);
                    contador = 1;
                }
                CapaDatos.CommitTrans();
                if (sqlcmd != null)
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ConsultaEstadistica(Clientes cte, string Conexion, ref List<Comun> List, object FiltroId, object FiltroDesc)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@FiltroId", "@FiltroDesc" };
                object[] Valores = { cte.Id_Emp, cte.Id_Cd, cte.Id_Cte, FiltroId, FiltroDesc };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spEstVentaCliente_Ventana", ref dr, Parametros, Valores);
                Comun cliente;
                while (dr.Read())
                {
                    cliente = new Comun();
                    cliente.IdStr2 = dr.GetValue(dr.GetOrdinal("Id2")).ToString();
                    cliente.IdStr = dr.GetValue(dr.GetOrdinal("Id")).ToString();
                    cliente.Descripcion = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    cliente.ValorDoble = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Val1")));
                    cliente.ValorDoble2 = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Val2")));
                    cliente.ValorDoble3 = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Val3")));

                    cliente.ValorInt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Val4")));
                    cliente.ValorInt2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Val5")));
                    cliente.ValorInt3 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Val6")));

                    cliente.ValorStr = (dr.GetValue(dr.GetOrdinal("Col1"))).ToString();
                    cliente.ValorStr2 = (dr.GetValue(dr.GetOrdinal("Col2"))).ToString();
                    cliente.ValorStr3 = (dr.GetValue(dr.GetOrdinal("Col3"))).ToString();
                    List.Add(cliente);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@FiltroId", "@FiltroDesc" };
                object[] Valores = { cte.Id_Emp, cte.Id_Cd, cte.Id_Cte, FiltroId, FiltroDesc };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spClienteIndicadores_Ventana", ref dr, Parametros, Valores);
                Producto producto;
                while (dr.Read())
                {
                    producto = new Producto();
                    producto.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    producto.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    producto.Prd_Presentacion = dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString();
                    producto.Uni_Descripcion = dr.GetValue(dr.GetOrdinal("Uni_Descripcion")).ToString();
                    producto.Prd_InvInicial = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_InvInicial")));
                    producto.Prd_InvFinal = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_InvFinal")));

                    producto.Prd_Fisico = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Fisico")));
                    producto.Prd_Ordenado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Ordenado")));
                    producto.Prd_Asignado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Asignado")));

                    producto.Prd_Transito = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Transito")));


                    List.Add(producto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarClienteTransf(Clientes cte, string Conexion)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Rik", "@Id_Ter", "@Ignora_Activo" };
                object[] Valores = { 
                                       cte.Id_Emp, 
                                       cte.Id_Cd, 
                                       cte.Id_Cte, 
                                       cte.Id_Rik <= 0 ? (object)null : cte.Id_Rik, 
                                       cte.Id_Terr <= 0 ? (object)null : cte.Id_Terr,
                                       cte.Ignora_Inactivo
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCliente_ConsultaTransf", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    cte.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    cte.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    cte.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    cte.Id_Rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Rik"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    cte.Id_Cfe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cfe"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cfe")));
                    cte.Id_Corp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Corp"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Corp")));
                    cte.FacSerie = dr.GetValue(dr.GetOrdinal("FacSerie")).ToString();
                    cte.Cte_NomComercial = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    cte.Cte_NomCorto = dr.IsDBNull(dr.GetOrdinal("Cte_NomCorto")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_NomCorto"));
                    cte.Cte_FacCalle = (string)dr.GetValue(dr.GetOrdinal("Cte_FacCalle"));
                    cte.Cte_FacNumero = (string)dr.GetValue(dr.GetOrdinal("Cte_FacNumero"));
                    cte.Cte_FacCp = (string)dr.GetValue(dr.GetOrdinal("Cte_FacCp"));
                    cte.Cte_FacColonia = (string)dr.GetValue(dr.GetOrdinal("Cte_FacColonia"));
                    cte.Cte_FacMunicipio = (string)dr.GetValue(dr.GetOrdinal("Cte_FacMunicipio"));
                    cte.Cte_FacTel = (string)dr.GetValue(dr.GetOrdinal("Cte_FacTel"));
                    cte.Cte_FacRfc = (string)dr.GetValue(dr.GetOrdinal("Cte_FacRfc"));
                    cte.Cte_FacEstado = (string)dr.GetValue(dr.GetOrdinal("Cte_FacEstado"));
                    cte.Cte_Calle = (string)dr.GetValue(dr.GetOrdinal("Cte_Calle"));
                    cte.Cte_Numero = (string)dr.GetValue(dr.GetOrdinal("Cte_Numero"));
                    string cp = (string)dr.GetValue(dr.GetOrdinal("Cte_Cp"));
                    cte.Cte_Cp = dr.GetValue(dr.GetOrdinal("Cte_Cp")).ToString();
                    cte.Cte_Colonia = (string)dr.GetValue(dr.GetOrdinal("Cte_Colonia"));
                    cte.Cte_Municipio = (string)dr.GetValue(dr.GetOrdinal("Cte_Municipio"));
                    cte.Cte_Estado = (string)dr.GetValue(dr.GetOrdinal("Cte_Estado"));
                    cte.Cte_DRfc = (string)dr.GetValue(dr.GetOrdinal("Cte_Rfc"));
                    cte.Cte_Telefono = dr.GetValue(dr.GetOrdinal("Cte_Telefono")).ToString();
                    cte.Cte_Fax = dr.GetValue(dr.GetOrdinal("Cte_Fax")).ToString();
                    cte.Cte_Contacto = (string)dr.GetValue(dr.GetOrdinal("Cte_Contacto"));
                    cte.Cte_Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_Tipo")));
                    cte.Cte_Email = (string)dr.GetValue(dr.GetOrdinal("Cte_Email"));
                    cte.Cte_Credito = (bool)dr.GetValue(dr.GetOrdinal("Cte_Credito"));
                    cte.Cte_Facturacion = (bool)dr.GetValue(dr.GetOrdinal("Cte_Facturacion"));
                    cte.Id_Mon = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Mon"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Mon")));
                    cte.Cte_LimCobr = (double)dr.GetValue(dr.GetOrdinal("Cte_LimCobr"));
                    cte.Cte_RHoraam1 = dr.IsDBNull(dr.GetOrdinal("Cte_RHoraam1")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_RHoraam1"));
                    cte.Cte_RHoraam2 = dr.IsDBNull(dr.GetOrdinal("Cte_RHoraam2")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_RHoraam2"));
                    cte.Cte_RHorapm1 = dr.IsDBNull(dr.GetOrdinal("Cte_RHorapm1")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_RHorapm1"));
                    cte.Cte_RHorapm2 = dr.IsDBNull(dr.GetOrdinal("Cte_RHorapm2")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_RHorapm2"));
                    cte.Cte_RLunes = (bool)dr.GetValue(dr.GetOrdinal("Cte_RLunes"));
                    cte.Cte_RMartes = (bool)dr.GetValue(dr.GetOrdinal("Cte_RMartes"));
                    cte.Cte_RMiercoles = (bool)dr.GetValue(dr.GetOrdinal("Cte_RMiercoles"));
                    cte.Cte_RJueves = (bool)dr.GetValue(dr.GetOrdinal("Cte_RJueves"));
                    cte.Cte_RViernes = (bool)dr.GetValue(dr.GetOrdinal("Cte_RViernes"));
                    cte.Cte_RSabado = (bool)dr.GetValue(dr.GetOrdinal("Cte_RSabado"));
                    cte.Cte_RDomingo = (bool)dr.GetValue(dr.GetOrdinal("Cte_RDomingo"));
                    cte.Cte_CondPago = (int)dr.GetValue(dr.GetOrdinal("Cte_CondPago"));
                    cte.Cte_CPLunes = (bool)dr.GetValue(dr.GetOrdinal("Cte_CPLunes"));
                    cte.Cte_CPMartes = (bool)dr.GetValue(dr.GetOrdinal("Cte_CPMartes"));
                    cte.Cte_CPMiercoles = (bool)dr.GetValue(dr.GetOrdinal("Cte_CPMiercoles"));
                    cte.Cte_CPJueves = (bool)dr.GetValue(dr.GetOrdinal("Cte_CPJueves"));
                    cte.Cte_CPViernes = (bool)dr.GetValue(dr.GetOrdinal("Cte_CPViernes"));
                    cte.Cte_CPSabado = (bool)dr.GetValue(dr.GetOrdinal("Cte_CPSabado"));
                    cte.Cte_CPDomingo = (bool)dr.GetValue(dr.GetOrdinal("Cte_CPDomingo"));
                    cte.Cte_Comisiones = (bool)dr.GetValue(dr.GetOrdinal("Cte_Comisiones"));
                    cte.Cte_DesgIva = (bool)dr.GetValue(dr.GetOrdinal("Cte_DesgIva"));
                    cte.Cte_RetIva = (bool)dr.GetValue(dr.GetOrdinal("Cte_RetIva"));
                    cte.Cte_AsignacionPed = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_AsignacionPed"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_AsignacionPed")));
                    cte.Id_Ade = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ade"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ade")));
                    cte.Cte_SerieNCre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_SerieNCre"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_SerieNCre")));
                    cte.Cte_SerieNCa = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_SerieNCa"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_SerieNCa")));
                    cte.Estatus = (bool)dr.GetValue(dr.GetOrdinal("Cte_Activo"));

                    cte.Cte_CreditoSuspendido = (bool)dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido"));
                    cte.Cte_PHoraam1 = dr.IsDBNull(dr.GetOrdinal("Cte_PHoraam1")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_PHoraam1"));
                    cte.Cte_PHoraam2 = dr.IsDBNull(dr.GetOrdinal("Cte_PHoraam2")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_PHoraam2"));
                    cte.Cte_PHorapm1 = dr.IsDBNull(dr.GetOrdinal("Cte_PHorapm1")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_PHorapm1"));
                    cte.Cte_PHorapm2 = dr.IsDBNull(dr.GetOrdinal("Cte_PHorapm2")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_PHorapm2"));
                    cte.Cte_SemRec = dr.IsDBNull(dr.GetOrdinal("Cte_SemRec")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_SemRec")));
                    cte.Cte_RecLunes = (bool)dr.GetValue(dr.GetOrdinal("Cte_RecLunes"));
                    cte.Cte_RecMartes = (bool)dr.GetValue(dr.GetOrdinal("Cte_RecMartes"));
                    cte.Cte_RecMiercoles = (bool)dr.GetValue(dr.GetOrdinal("Cte_RecMiercoles"));
                    cte.Cte_RecJueves = (bool)dr.GetValue(dr.GetOrdinal("Cte_RecJueves"));
                    cte.Cte_RecViernes = (bool)dr.GetValue(dr.GetOrdinal("Cte_RecViernes"));
                    cte.Cte_RecSabado = (bool)dr.GetValue(dr.GetOrdinal("Cte_RecSabado"));
                    cte.Cte_RecDomingo = (bool)dr.GetValue(dr.GetOrdinal("Cte_RecDomingo"));
                    cte.Cte_Efectivo = (bool)dr.GetValue(dr.GetOrdinal("Cte_Efectivo"));
                    cte.Cte_Factoraje = (bool)dr.GetValue(dr.GetOrdinal("Cte_Factoraje"));
                    cte.Cte_Cheque = (bool)dr.GetValue(dr.GetOrdinal("Cte_Cheque"));
                    cte.Cte_Transferencia = (bool)dr.GetValue(dr.GetOrdinal("Cte_Transferencia"));
                    cte.Cte_ReqOrdenCompra = (bool)dr.GetValue(dr.GetOrdinal("Cte_ReqOrdenCompra"));
                    cte.Cte_Documentos = dr.IsDBNull(dr.GetOrdinal("Cte_Documentos")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_Documentos")).ToString();
                    cte.Ade_Nombre = dr.IsDBNull(dr.GetOrdinal("Ade_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("Ade_Nombre")).ToString();
                    cte.Cte_TelCobranza1 = dr.IsDBNull(dr.GetOrdinal("Cte_TelCobranza1")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_TelCobranza1")).ToString();
                    cte.Cte_TelCobranza2 = dr.IsDBNull(dr.GetOrdinal("Cte_TelCobranza2")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_TelCobranza2")).ToString();
                    cte.Cte_RemisionElectronica = dr.IsDBNull(dr.GetOrdinal("Cte_RemisionElectronica")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_RemisionElectronica")));
                    cte.BPorcNotaCredito = dr.IsDBNull(dr.GetOrdinal("Cte_NCredito")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_NCredito")));
                    cte.PorcientoNotaCredito = dr.IsDBNull(dr.GetOrdinal("Cte_PorcNCredito")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cte_PorcNCredito")));
                    cte.PorcientoRetencion = dr.IsDBNull(dr.GetOrdinal("Cte_PorcRetencion")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cte_PorcRetencion")));
                    cte.BPorcientoIVA = dr.IsDBNull(dr.GetOrdinal("Cte_BPorcientoIVA")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_BPorcientoIVA")));
                    cte.PorcientoIVA = dr.IsDBNull(dr.GetOrdinal("Cte_PorcientoIVA")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_PorcientoIVA")));         
                    cte.Cte_UDigitos = dr.IsDBNull(dr.GetOrdinal("Cte_UDigitos")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_UDigitos")));
                    if (Convert.ToBoolean(cte.Estatus))
                        cte.EstatusStr = "Activo";
                    else
                        cte.EstatusStr = "Inactivo";
                }



                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



    }
}
