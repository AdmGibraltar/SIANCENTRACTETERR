using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;

namespace CapaDatos
{
    public class CD_CapAcys
    {
        public void ConsultarAcys_Lista(Acys acys, string Conexion, ref List<Acys> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Filtro_Estatus",
                                          "@Filtro_FolIni",
                                          "@Filtro_FolFin",
                                          "@Filtro_FecIni",
                                          "@Filtro_FecFin",
                                          "@Filtro_usuario",
                                          "@Id_Ter",
                                          "@Id_Rik",
                                          "@Id_Cte",
                                          "@Acs_Vencido"
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Filtro_Estatus == ""? (object)null: acys.Filtro_Estatus,
                                       acys.Filtro_FolIni == ""? (object)null: acys.Filtro_FolIni,
                                       acys.Filtro_FolFin == ""? (object)null: acys.Filtro_FolFin,
                                       acys.Filtro_FecIni,
                                       acys.Filtro_FecFin,
                                       acys.Filtro_usuario == ""? (object)null: acys.Filtro_usuario,
                                       acys.Id_Ter == -1 ? (object)null: acys.Id_Ter,
                                       acys.Id_Rik== -1 ? (object)null: acys.Id_Rik,
                                       acys.Id_Cte== -1 ? (object)null: acys.Id_Cte,
                                       acys.Acs_Vencido == ""? (object)null: acys.Acs_Vencido
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_Lista", ref dr, Parametros, Valores);

                Acys a;
                while (dr.Read())
                {
                    a = new Acys();
                    a.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    a.Acs_Estatus = dr.IsDBNull(dr.GetOrdinal("Acs_Estatus")) ? "" : dr.GetValue(dr.GetOrdinal("Acs_Estatus")).ToString();
                    a.Acs_EstatusStr = dr.IsDBNull(dr.GetOrdinal("Acs_Estatus")) ? "" : Estatus(dr.GetValue(dr.GetOrdinal("Acs_Estatus")).ToString());
                    a.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    a.Cte_Nombre = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    a.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    a.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    a.Acs_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_Fecha")));
                    a.Acs_FechaInicioDocumento = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_FechaInicioDocumento")));
                    a.Acs_FechaFinDocumento = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_FechaFinDocumento")));
                    a.Acs_Vencido = dr.IsDBNull(dr.GetOrdinal("Acs_Vencido")) ? "" : dr.GetValue(dr.GetOrdinal("Acs_Vencido")).ToString();

                    List.Add(a);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string Estatus(string p)
        {
            switch (p)
            {
                case "B":
                    return "Cancelado";
                case "C":
                    return "Capturado";
                case "I":
                    return "Impreso";
                case "A":
                    return "Autorizado";
                case "S":
                    return "Solicitado";
                case "R":
                    return "Rechazado";
                default:
                    return "";
            }
        }

        public void ConsultarAcys(Acys acys, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Ter",
                                          "@Id_Rik",
                                          "@Id_Cte",
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Id_Ter == -1 ? (object)null: acys.Id_Ter,
                                       acys.Id_Rik== -1 ? (object)null: acys.Id_Rik,
                                       acys.Id_Cte== -1 ? (object)null: acys.Id_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_Lista", ref verificador, Parametros, Valores);


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void actualizarEstatus(Acys acys, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Acs_Estatus"                                          
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Id_Acs,
                                       acys.Acs_Estatus                                      
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_ActualizarEstatus", ref verificador, Parametros, Valores);


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(Acys acys, List<AcysPrd> list, string Conexion, DataTable dt, ref int verificador, List<Asesoria> asesorias, List<Producto> servicios, List<Producto> serviciosMantenimiento)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);
            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();
                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Ter",
                                          "@Id_Rik",
                                          "@Id_Cte",
                                          "@Cte_Nombre",
                                          "@Acs_Fecha",
                                          "@Acs_Contacto",
                                          "@Acs_Puesto",
                                          "@Acs_Telefono",
                                          "@Acs_Correo",
                                          "@Acs_Contacto2",
                                          "@Acs_Telefono2",
                                          "@Acs_Correo2",
                                          "@Acs_Contacto3",
                                          "@Acs_Telefono3",
                                          "@Acs_Correo3",
                                          "@Acs_Contacto4",
                                          "@Acs_Telefono4",
                                          "@Acs_Correo4",
                                          "@Acs_Contacto5",
                                          "@Acs_Telefono5",
                                          "@Acs_Correo5",
                                          "@Acs_Contacto6",
                                          "@Acs_Telefono6",
                                          "@Acs_Correo6",
                                          "@Acs_Proveedor",
                                          "@Acs_RutaEntrega",
                                          "@Acs_RutaServicio",
                                          "@Acs_ReqOrdenCompra",
                                          "@Acs_VigenciaIni",
                                          "@Acs_Semana",
                                          "@Acs_ReqConfirmacion",
                                          "@Acs_RecPedCorreo",
                                          "@Acs_RecPedFax",
                                          "@Acs_RecPedTel",
                                          "@Acs_RecPedRep",
                                          "@Acs_RecPedOtro",
                                          "@Acs_RecPedOtroStr",
                                          "@Acs_PedidoEncargadoEnviar",
                                          "@Acs_PedidoPuesto",
                                          "@Acs_PedidoTelefono",
                                          "@Acs_PedidoEmail",
                                          "@Acs_RecDocReposicion",
                                          "@Acs_RecDocFolio",
                                          "@Acs_RecDocOtro",
                                          "@Acs_VisitaOtro",
                                          "@Acs_ReqServAsesoria",
                                          "@Acs_ReqServTecnicoRelleno",
                                          "@Acs_ReqServMantenimiento",
                                          "@Acs_Notas",
                                          "@Acs_ContactoRepVenta",
                                          "@Acs_ContactoRepVentaTel",
                                          "@Acs_ContactoRepVentaEmail",
                                          "@Acs_ContactoRepServ",
                                          "@Acs_ContactoRepServTel",
                                          "@Acs_ContactoRepServEmail",
                                          "@Acs_ContactoJefServ",
                                          "@Acs_ContactoJefServTel",
                                          "@Acs_ContactoJefServEmail",
                                          "@Acs_ContactoAseServ",
                                          "@Acs_ContactoAseServTel",
                                          "@Acs_ContactoAseServEmail",
                                          "@Acs_ContactoJefOper",
                                          "@Acs_ContactoJefOperTel",
                                          "@Acs_ContactoJefOperEmail",
                                          "@Acs_ContactoCAlmRep",
                                          "@Acs_ContactoCAlmRepTel",
                                          "@Acs_ContactoCAlmRepEmail",
                                          "@Acs_ContactoCServTec",
                                          "@Acs_ContactoCServTecTel",
                                          "@Acs_ContactoCServTecEmail",
                                          "@Acs_ContactoCCreCob",
                                          "@Acs_ContactoCCreCobTel",
                                          "@Acs_ContactoCCreCobEmail",
                                          "@Acs_FechaInicio",
                                          "@Acs_FechaFin",
                                          "@Id_U"
                                      };
                object[] Valores = {
                                       acys.Id_Emp,
                                       acys.Id_Cd ,
                                       acys.Id_Ter ,
                                       acys.Id_Rik ,
                                       acys.Id_Cte ,
                                       acys.Cte_Nombre,
                                       acys.Acs_Fecha,
                                       acys.Acs_Contacto,
                                       acys.Acs_Puesto,
                                       acys.Acs_Telefono,
                                       acys.Acs_Correo,
                                       acys.Acs_Contacto2,
                                       acys.Acs_Telefono2,
                                       acys.Acs_Correo2,
                                       acys.Acs_Contacto3,
                                       acys.Acs_Telefono3,
                                       acys.Acs_Correo3,
                                       acys.Acs_Contacto4,
                                       acys.Acs_Telefono4,
                                       acys.Acs_Correo4,
                                       acys.Acs_Contacto5,
                                       acys.Acs_Telefono5,
                                       acys.Acs_Correo5,
                                       acys.Acs_Contacto6,
                                       acys.Acs_Telefono6,
                                       acys.Acs_Correo6,
                                       acys.Acs_Proveedor,
                                       acys.Acs_RutaEntrega,
                                       acys.Acs_RutaServicio,
                                       acys.Acs_ReqOrdenCompra,
                                       acys.Acs_VigenciaIni,
                                       acys.Acs_Semana,
                                       acys.Acs_ReqConfirmacion,
                                       acys.Acs_RecPedCorreo,
                                       acys.Acs_RecPedFax,
                                       acys.Acs_RecPedTel,
                                       acys.Acs_RecPedRep,
                                       acys.Acs_RecPedOtro,
                                       acys.Acs_RecPedOtroStr,
                                       acys.Acs_PedidoEncargadoEnviar,
                                       acys.Acs_PedidoPuesto,
                                       acys.Acs_PedidoTelefono,
                                       acys.Acs_PedidoEmail,
                                       acys.Acs_RecDocReposicion,
                                       acys.Acs_RecDocFolio,
                                       acys.Acs_RecDocOtro,
                                       acys.Acs_VisitaOtro,
                                       acys.Acs_ReqServAsesoria,
                                       acys.Acs_ReqServTecnicoRelleno,
                                       acys.Acs_ReqServMantenimiento,
                                       acys.Acs_Notas,
                                       acys.Acs_ContactoRepVenta,
                                       acys.Acs_ContactoRepVentaTel,
                                       acys.Acs_ContactoRepVentaEmail,
                                       acys.Acs_ContactoRepServ,
                                       acys.Acs_ContactoRepServTel,
                                       acys.Acs_ContactoRepServEmail,
                                       acys.Acs_ContactoJefServ,
                                       acys.Acs_ContactoJefServTel,
                                       acys.Acs_ContactoJefServEmail,
                                       acys.Acs_ContactoAseServ,
                                       acys.Acs_ContactoAseServTel,
                                       acys.Acs_ContactoAseServEmail,
                                       acys.Acs_ContactoJefOper,
                                       acys.Acs_ContactoJefOperTel,
                                       acys.Acs_ContactoJefOperEmail,
                                       acys.Acs_ContactoCAlmRep,
                                       acys.Acs_ContactoCAlmRepTel,
                                       acys.Acs_ContactoCAlmRepEmail,
                                       acys.Acs_ContactoCServTec,
                                       acys.Acs_ContactoCServTecTel,
                                       acys.Acs_ContactoCServTecEmail,
                                       acys.Acs_ContactoCCreCob,
                                       acys.Acs_ContactoCCreCobTel,
                                       acys.Acs_ContactoCCreCobEmail,
                                       acys.Acs_FechaInicioDocumento,
                                       acys.Acs_FechaFinDocumento,
                                       acys.Id_U
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_Insertar", ref verificador, Parametros, Valores);

                acys.Id_Acs = verificador;

                Parametros = new string[] { 
                    		"@Id_Emp", 
		                    "@Id_Cd",  
		                    "@Id_AcsDet",  
		                    "@Id_Acs",  
		                    "@Id_Prd",  
		                    "@Acs_Cantidad",  
		                    "@Acs_Documento",  
		                    "@Acs_Sabado",  
		                    "@Acs_Viernes",  
		                    "@Acs_Jueves",  
		                    "@Acs_Miercoles", 
		                    "@Acs_Martes",  
		                    "@Acs_Lunes",  
		                    "@Acs_Frecuencia",
                            "@Acs_Precio",
                            "@Acs_Modalidad"
                    };
                for (int x = 0; x < list.Count; x++)
                {
                    if (verificador < 0)
                    {
                        break;
                    }
                    else
                    {
                        verificador = -1;
                    }

                    Valores = new object[] {                         
                            acys.Id_Emp,
                            acys.Id_Cd ,
		                    x, 
		                    acys.Id_Acs, 
		                    list[x].Id_Prd, 
		                    list[x].Acys_Cantidad, 
		                    list[x].Acs_Doc, 
		                    list[x].Acys_Sabado, 
		                    list[x].Acys_Viernes, 
		                    list[x].Acys_Jueves, 
		                    list[x].Acys_Miercoles, 
		                    list[x].Acys_Martes, 
		                    list[x].Acys_Lunes, 
		                    list[x].Acys_Frecuencia,
                            list[x].Prd_Precio,
                            list[x].Acs_Modalidad
                        };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysDet_Insertar", ref verificador, Parametros, Valores);
                }

                //ASESORIAS
                int verificador2 = 0;
                int modificar = 1;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "@Id_Ase", "@Mensual", "@FechaInicioMensual", "@Bimestral", "@FechaInicioBimestral", "@Trimestral", "@FechaInicioTrimestral", "@Modificar" };
                foreach (Asesoria a in asesorias)
                {
                    Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, a.Id_Ase, a.Ase_ServAsesoriaMensual, a.Ase_ServAsesoriaMensualfechaIni,a.Ase_ServAsesoriaBimestral, a.Ase_ServAsesoriaBimestralfechaIni, a.Ase_ServAsesoriaTrimestral, a.Ase_ServAsesoriaTrimestralfechaIni, modificar };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysAsesorias_Insertar", ref verificador2, Parametros, Valores);
                    modificar = 0;
                }

                //SERVICIOS RELLENO
                int verificador3 = 0;
                modificar = 1;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "@Id_Prd", "@Revision", "Bimestral", "FechaInicioBimestral", "Trimestral", "FechaInicioTrimestral", "@Modificar" };
                foreach (Producto a in servicios)
                {
                    Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, a.Id_Prd, a.Prd_InvInicial, a.ServTecnicoRellenoBimestral, a.ServTecnicoRellenoBimestralfechaIni, a.ServTecnicoRellenoTrimestral, a.ServTecnicoRellenoTrimestralfechaIni, modificar };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysServicios_Insertar", ref verificador3, Parametros, Valores);
                    modificar = 0;
                }


                //SERVICIOS MANTENIMIENTO
                int verificador4 = 0;
                modificar = 1;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "@Id_Prd", "@Revision", "Mensual", "FechaInicioMensual",  "Bimestral", "FechaInicioBimestral", "Trimestral", "FechaInicioTrimestral", "@Modificar" };
                foreach (Producto a in serviciosMantenimiento)
                {
                    Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, a.Id_Prd, a.Prd_InvInicial, a.ServMantenimientoMensual, a.ServMantenimientoMensualfechaIni, a.ServMantenimientoBimestral, a.ServMantenimientoBimestralfechaIni, a.ServMantenimientoTrimestral, a.ServMantenimientoTrimestralfechaIni, modificar };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysServiciosMantenimiento_Insertar", ref verificador4, Parametros, Valores);
                    modificar = 0;
                }

               
                //
                int eliminar = 1;
                Parametros = new string[] { 
                    		"@Id_Emp", 
		                    "@Id_Cd",  
		                    "@Id_Acs",  
		                    "@Id_PrdOriginal",  
		                    "@Id_PrdEquivalente",  
		                    "@Eliminar"
                    };

                if (dt != null)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        if (Convert.ToBoolean(dt.Rows[x]["Seleccionado"]))
                        {
                            object[] ValoresEquivalencias = new object[] {                         
                            acys.Id_Emp,
                            acys.Id_Cd,
                            acys.Id_Acs,
                            dt.Rows[x]["Id_Original"],
                            dt.Rows[x]["Id_Similar"],
                            eliminar
                        };
                            sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysEquivalencias_Insertar", ref verificador, Parametros, ValoresEquivalencias);
                            eliminar++;
                        }
                    }
                }

                if (verificador < 0)
                {
                    CapaDatos.RollBackTrans();
                }
                else
                {
                    CapaDatos.CommitTrans();
                }
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
            finally
            {
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
        }

        public void Modificar(Acys acys, List<AcysPrd> list, string Conexion, DataTable dt, ref int verificador, List<Asesoria> asesorias, List<Producto> servicios, List<Producto> serviciosMantenimiento)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);
            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();
                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Id_Ter",
                                          "@Id_Rik",
                                          "@Id_Cte",
                                          "@Cte_Nombre",
                                          "@Acs_Fecha",
                                          "@Acs_Contacto",
                                          "@Acs_Puesto",
                                          "@Acs_Telefono",
                                          "@Acs_Correo",
                                          "@Acs_Contacto2",
                                          "@Acs_Telefono2",
                                          "@Acs_Correo2",
                                          "@Acs_Contacto3",
                                          "@Acs_Telefono3",
                                          "@Acs_Correo3",
                                          "@Acs_Contacto4",
                                          "@Acs_Telefono4",
                                          "@Acs_Correo4",
                                          "@Acs_Contacto5",
                                          "@Acs_Telefono5",
                                          "@Acs_Correo5",
                                          "@Acs_Contacto6",
                                          "@Acs_Telefono6",
                                          "@Acs_Correo6",
                                          "@Acs_Proveedor",
                                          "@Acs_RutaEntrega",
                                          "@Acs_RutaServicio",
                                          "@Acs_ReqOrdenCompra",
                                          "@Acs_VigenciaIni",
                                          "@Acs_Semana",
                                          "@Acs_ReqConfirmacion",
                                          "@Acs_RecPedCorreo",
                                          "@Acs_RecPedFax",
                                          "@Acs_RecPedTel",
                                          "@Acs_RecPedRep",
                                          "@Acs_RecPedOtro",
                                          "@Acs_RecPedOtroStr",
                                          "@Acs_PedidoEncargadoEnviar",
                                          "@Acs_PedidoPuesto",
                                          "@Acs_PedidoTelefono",
                                          "@Acs_PedidoEmail",
                                          "@Acs_RecDocReposicion",
                                          "@Acs_RecDocFolio",
                                          "@Acs_RecDocOtro",
                                          "@Acs_VisitaOtro",
                                          "@Acs_ReqServAsesoria",
                                          "@Acs_ReqServTecnicoRelleno",
                                          "@Acs_ReqServMantenimiento",
                                          "@Acs_Notas",
                                          "@Acs_ContactoRepVenta",
                                          "@Acs_ContactoRepVentaTel",
                                          "@Acs_ContactoRepVentaEmail",
                                          "@Acs_ContactoRepServ",
                                          "@Acs_ContactoRepServTel",
                                          "@Acs_ContactoRepServEmail",
                                          "@Acs_ContactoJefServ",
                                          "@Acs_ContactoJefServTel",
                                          "@Acs_ContactoJefServEmail",
                                          "@Acs_ContactoAseServ",
                                          "@Acs_ContactoAseServTel",
                                          "@Acs_ContactoAseServEmail",
                                          "@Acs_ContactoJefOper",
                                          "@Acs_ContactoJefOperTel",
                                          "@Acs_ContactoJefOperEmail",
                                          "@Acs_ContactoCAlmRep",
                                          "@Acs_ContactoCAlmRepTel",
                                          "@Acs_ContactoCAlmRepEmail",
                                          "@Acs_ContactoCServTec",
                                          "@Acs_ContactoCServTecTel",
                                          "@Acs_ContactoCServTecEmail",
                                          "@Acs_ContactoCCreCob",
                                          "@Acs_ContactoCCreCobTel",
                                          "@Acs_ContactoCCreCobEmail",
                                          "@Acs_FechaInicio",
                                          "@Acs_FechaFin",
                                          "@Id_U"
                                      };
                object[] Valores = {
                                       acys.Id_Emp,
                                       acys.Id_Cd ,
                                       acys.Id_Acs,
                                       acys.Id_Ter ,
                                       acys.Id_Rik ,
                                       acys.Id_Cte ,
                                       acys.Cte_Nombre,
                                       acys.Acs_Fecha,
                                       acys.Acs_Contacto,
                                       acys.Acs_Puesto,
                                       acys.Acs_Telefono,
                                       acys.Acs_Correo,
                                       acys.Acs_Contacto2,
                                       acys.Acs_Telefono2,
                                       acys.Acs_Correo2,
                                       acys.Acs_Contacto3,
                                       acys.Acs_Telefono3,
                                       acys.Acs_Correo3,
                                       acys.Acs_Contacto4,
                                       acys.Acs_Telefono4,
                                       acys.Acs_Correo4,
                                       acys.Acs_Contacto5,
                                       acys.Acs_Telefono5,
                                       acys.Acs_Correo5,
                                       acys.Acs_Contacto6,
                                       acys.Acs_Telefono6,
                                       acys.Acs_Correo6,
                                       acys.Acs_Proveedor,
                                       acys.Acs_RutaEntrega,
                                       acys.Acs_RutaServicio,
                                       acys.Acs_ReqOrdenCompra,
                                       acys.Acs_VigenciaIni,
                                       acys.Acs_Semana,
                                       acys.Acs_ReqConfirmacion,
                                       acys.Acs_RecPedCorreo,
                                       acys.Acs_RecPedFax,
                                       acys.Acs_RecPedTel,
                                       acys.Acs_RecPedRep,
                                       acys.Acs_RecPedOtro,
                                       acys.Acs_RecPedOtroStr,
                                        acys.Acs_PedidoEncargadoEnviar,
                                       acys.Acs_PedidoPuesto,
                                       acys.Acs_PedidoTelefono,
                                       acys.Acs_PedidoEmail,
                                       acys.Acs_RecDocReposicion,
                                       acys.Acs_RecDocFolio,
                                       acys.Acs_RecDocOtro,
                                       acys.Acs_VisitaOtro,
                                       acys.Acs_ReqServAsesoria,
                                       acys.Acs_ReqServTecnicoRelleno,
                                       acys.Acs_ReqServMantenimiento,
                                       acys.Acs_Notas,
                                       acys.Acs_ContactoRepVenta,
                                       acys.Acs_ContactoRepVentaTel,
                                       acys.Acs_ContactoRepVentaEmail,
                                       acys.Acs_ContactoRepServ,
                                       acys.Acs_ContactoRepServTel,
                                       acys.Acs_ContactoRepServEmail,
                                       acys.Acs_ContactoJefServ,
                                       acys.Acs_ContactoJefServTel,
                                       acys.Acs_ContactoJefServEmail,
                                       acys.Acs_ContactoAseServ,
                                       acys.Acs_ContactoAseServTel,
                                       acys.Acs_ContactoAseServEmail,
                                       acys.Acs_ContactoJefOper,
                                       acys.Acs_ContactoJefOperTel,
                                       acys.Acs_ContactoJefOperEmail,
                                       acys.Acs_ContactoCAlmRep,
                                       acys.Acs_ContactoCAlmRepTel,
                                       acys.Acs_ContactoCAlmRepEmail,
                                       acys.Acs_ContactoCServTec,
                                       acys.Acs_ContactoCServTecTel,
                                       acys.Acs_ContactoCServTecEmail,
                                       acys.Acs_ContactoCCreCob,
                                       acys.Acs_ContactoCCreCobTel,
                                       acys.Acs_ContactoCCreCobEmail,
                                       acys.Acs_FechaInicioDocumento,
                                       acys.Acs_FechaFinDocumento,
                                       acys.Id_U
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_Modificar", ref verificador, Parametros, Valores);

                Parametros = new string[] { 
                    		"@Id_Emp", 
		                    "@Id_Cd",  
		                    "@Id_AcsDet",  
		                    "@Id_Acs",  
		                    "@Id_Prd",  
		                    "@Acs_Cantidad",  
		                    "@Acs_Documento",  
		                    "@Acs_Sabado",  
		                    "@Acs_Viernes",  
		                    "@Acs_Jueves",  
		                    "@Acs_Miercoles", 
		                    "@Acs_Martes",  
		                    "@Acs_Lunes",  
		                    "@Acs_Frecuencia",
                            "@Acs_Precio",
                            "@Acs_UltSCpt",
                            "@Acs_UltACpt",
                            "@Acs_Modalidad"
                    };
                for (int x = 0; x < list.Count; x++)
                {
                    if (verificador < 0)
                    {
                        break;
                    }
                    else
                    {
                        verificador = -1;
                    }

                    Valores = new object[] {                         
                            acys.Id_Emp,
                            acys.Id_Cd ,
		                    x, 
		                    acys.Id_Acs, 
		                    list[x].Id_Prd, 
		                    list[x].Acys_Cantidad, 
		                    list[x].Acs_Doc, 
		                    list[x].Acys_Sabado, 
		                    list[x].Acys_Viernes, 
		                    list[x].Acys_Jueves, 
		                    list[x].Acys_Miercoles, 
		                    list[x].Acys_Martes, 
		                    list[x].Acys_Lunes, 
		                    list[x].Acys_Frecuencia,
                            list[x].Prd_Precio,
                            list[x].Acys_UltSCtp == -1 ? (int?)null : list[x].Acys_UltSCtp,
                            list[x].Acys_UltACtp == -1 ? (int?)null : list[x].Acys_UltACtp,
                            list[x].Acs_Modalidad
                        };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysDet_Insertar", ref verificador, Parametros, Valores);
                }


                //ASESORIAS
                int verificador2 = 0;
                int modificar = 1;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "@Id_Ase", "@Mensual", "@FechaInicioMensual", "@Bimestral", "@FechaInicioBimestral", "@Trimestral", "@FechaInicioTrimestral", "@Modificar" };
                foreach (Asesoria a in asesorias)
                {
                    Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, a.Id_Ase, a.Ase_ServAsesoriaMensual, a.Ase_ServAsesoriaMensualfechaIni, a.Ase_ServAsesoriaBimestral, a.Ase_ServAsesoriaBimestralfechaIni, a.Ase_ServAsesoriaTrimestral, a.Ase_ServAsesoriaTrimestralfechaIni, modificar };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysAsesorias_Insertar", ref verificador2, Parametros, Valores);
                    modificar = 0;
                }

                //SERVICIOS RELLENO
                int verificador3 = 0;
                modificar = 1;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "@Id_Prd", "@Revision", "Bimestral", "FechaInicioBimestral", "Trimestral", "FechaInicioTrimestral", "@Modificar" };
                foreach (Producto a in servicios)
                {
                    Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, a.Id_Prd, a.Prd_InvInicial, a.ServTecnicoRellenoBimestral, a.ServTecnicoRellenoBimestralfechaIni, a.ServTecnicoRellenoTrimestral, a.ServTecnicoRellenoTrimestralfechaIni, modificar };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysServicios_Insertar", ref verificador3, Parametros, Valores);
                    modificar = 0;
                }


                //SERVICIOS MANTENIMIENTO
                int verificador4 = 0;
                modificar = 1;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Acs", "@Id_Prd", "@Revision", "Mensual", "FechaInicioMensual", "Bimestral", "FechaInicioBimestral", "Trimestral", "FechaInicioTrimestral", "@Modificar" };
                foreach (Producto a in serviciosMantenimiento)
                {
                    Valores = new object[] { acys.Id_Emp, acys.Id_Cd, acys.Id_Acs, a.Id_Prd, a.Prd_InvInicial, a.ServMantenimientoMensual, a.ServMantenimientoMensualfechaIni, a.ServMantenimientoBimestral, a.ServMantenimientoBimestralfechaIni, a.ServMantenimientoTrimestral, a.ServMantenimientoTrimestralfechaIni, modificar };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysServiciosMantenimiento_Insertar", ref verificador4, Parametros, Valores);
                    modificar = 0;
                }


                //
                int eliminar = 1;
                Parametros = new string[] { 
                    		"@Id_Emp", 
		                    "@Id_Cd",  
		                    "@Id_Acs",  
		                    "@Id_PrdOriginal",  
		                    "@Id_PrdEquivalente",  
		                    "@Eliminar"
                    };
                if (dt != null)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        if (Convert.ToBoolean(dt.Rows[x]["Seleccionado"]))
                        {
                            object[] ValoresEquivalencias = new object[] {                         
                            acys.Id_Emp,
                            acys.Id_Cd,
                            acys.Id_Acs,
                            dt.Rows[x]["Id_Original"],
                            dt.Rows[x]["Id_Similar"],
                            eliminar
                        };
                            sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysEquivalencias_Insertar", ref verificador, Parametros, ValoresEquivalencias);
                            eliminar++;
                        }
                    }
                }
                if (verificador < 0)
                {
                    CapaDatos.RollBackTrans();
                }
                else
                {
                    CapaDatos.CommitTrans();
                }
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
            finally
            {
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
        }

        public void Cancelar(Acys acys, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);

            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Acs"
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Id_Acs
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_Baja", ref verificador, Parametros, Valores);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
        }

        public void Consultar(ref Acys acys, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Acs"
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Id_Acs
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_Consultar", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    acys.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    acys.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    acys.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));

                    acys.Cte_Nombre = dr.GetValue(dr.GetOrdinal("Acs_NomComercial")).ToString();
                    acys.Acs_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_Fecha")));
                    acys.Acs_FechaInicioDocumento = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_FechaInicioDocumento")));
                    acys.Acs_FechaFinDocumento = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_FechaFinDocumento")));


                    acys.Acs_Contacto = dr.GetValue(dr.GetOrdinal("Acs_Contacto")).ToString();
                    acys.Acs_Puesto = dr.GetValue(dr.GetOrdinal("Acs_Puesto")).ToString();
                    acys.Acs_Telefono = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Telefono")));
                    acys.Acs_Correo = dr.GetValue(dr.GetOrdinal("Acs_email")).ToString();

                    acys.ClienteDireccion = dr.GetValue(dr.GetOrdinal("ClienteDireccion")).ToString();
                    acys.ClienteColonia = dr.GetValue(dr.GetOrdinal("ClienteColonia")).ToString();
                    acys.ClienteMunicipio = dr.GetValue(dr.GetOrdinal("ClienteMunicipio")).ToString();
                    acys.ClienteEstado = dr.GetValue(dr.GetOrdinal("ClienteEstado")).ToString();
                    acys.ClienteRFC = dr.GetValue(dr.GetOrdinal("ClienteRFC")).ToString();
                    acys.ClienteCodPost = dr.GetValue(dr.GetOrdinal("ClienteCodPost")).ToString();
                    acys.CuentaCorporativa = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CuentaCorporativa"))) > 1 ? true: false;
                    acys.AddendaSI = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("AddendaSi"))) > 0 ? true : false;  

                    acys.DireccionEntrega = dr.GetValue(dr.GetOrdinal("DireccionEntrega")).ToString();
                    acys.ClienteColoniaE = dr.GetValue(dr.GetOrdinal("ClienteColoniaE")).ToString();
                    acys.ClienteMunicipioE = dr.GetValue(dr.GetOrdinal("ClienteMunicipioE")).ToString();
                    acys.ClienteCPE = dr.GetValue(dr.GetOrdinal("ClienteCPE")).ToString();
                    acys.ClienteEstadoE = dr.GetValue(dr.GetOrdinal("ClienteEstadoE")).ToString();
                      

                    acys.Acs_Contacto2 = dr.GetValue(dr.GetOrdinal("Acs_Contacto2")).ToString();
                    acys.Acs_Telefono2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Telefono2")));
                    acys.Acs_Correo2 = dr.GetValue(dr.GetOrdinal("Acs_Email2")).ToString();

                    acys.Acs_Contacto3 = dr.GetValue(dr.GetOrdinal("Acs_Contacto3")).ToString();
                    acys.Acs_Telefono3 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Telefono3")));
                    acys.Acs_Correo3 = dr.GetValue(dr.GetOrdinal("Acs_email3")).ToString();

                    acys.Acs_Contacto4 = dr.GetValue(dr.GetOrdinal("Acs_Contacto4")).ToString();
                    acys.Acs_Telefono4 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Telefono4")));
                    acys.Acs_Correo4 = dr.GetValue(dr.GetOrdinal("Acs_email4")).ToString();

                    acys.Acs_Contacto5 = dr.GetValue(dr.GetOrdinal("Acs_Contacto5")).ToString();
                    acys.Acs_Telefono5 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Telefono5")));
                    acys.Acs_Correo5 = dr.GetValue(dr.GetOrdinal("Acs_email5")).ToString();

                    acys.Acs_Contacto6 = dr.GetValue(dr.GetOrdinal("Acs_Contacto6")).ToString();
                    acys.Acs_Telefono6 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Telefono6")));
                    acys.Acs_Correo6 = dr.GetValue(dr.GetOrdinal("Acs_email6")).ToString();

                    acys.Acs_Proveedor = dr.GetValue(dr.GetOrdinal("Acs_NumPrv")).ToString();

                    acys.Acs_RutaServicio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Ruta1")));
                    acys.Acs_RutaEntrega = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Ruta2")));

                    acys.Acs_ReqOrdenCompra = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_ReqOrden")));
                    acys.Acs_VigenciaIni = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_VigenciaApartir")));
                    acys.Acs_Semana = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Semana")));
                    acys.Acs_ReqConfirmacion = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_ReqConfirmacion")));

                    acys.Acs_RecPedCorreo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecCorreo")));
                    acys.Acs_RecPedFax = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecFax")));
                    acys.Acs_RecPedTel = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecTelefono")));
                    acys.Acs_RecPedRep = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecRepresentante")));
                    acys.Acs_RecPedOtro = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecOtro")));
                    acys.Acs_RecPedOtroStr = dr.GetValue(dr.GetOrdinal("Acs_RecOtroDesc")).ToString();

                    acys.Acs_Estatus = dr.GetValue(dr.GetOrdinal("Acs_Estatus")).ToString();


                    //VISITAS
                    acys.Vis_Frecuencia = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vis_Frecuencia")));
                    acys.Vis_Lunes = dr.GetBoolean(dr.GetOrdinal("Vis_Lunes"));
                    acys.Vis_Martes = dr.GetBoolean(dr.GetOrdinal("Vis_Martes"));
                    acys.Vis_Miercoles = dr.GetBoolean(dr.GetOrdinal("Vis_Miercoles"));
                    acys.Vis_Jueves = dr.GetBoolean(dr.GetOrdinal("Vis_Jueves"));
                    acys.Vis_Viernes = dr.GetBoolean(dr.GetOrdinal("Vis_Viernes"));
                    acys.Vis_Sabado = dr.GetBoolean(dr.GetOrdinal("Vis_Sabado"));
                    acys.Vis_HrAm1 = dr.GetValue(dr.GetOrdinal("Vis_HrAm1")).ToString();
                    acys.Vis_HrAm2 = dr.GetValue(dr.GetOrdinal("Vis_HrAm2")).ToString();
                    acys.Vis_HrPm1 = dr.GetValue(dr.GetOrdinal("Vis_HrPm1")).ToString();
                    acys.Vis_HrPm2 = dr.GetValue(dr.GetOrdinal("Vis_HrPm2")).ToString();

                    //RECEPCION DE PEDIDOS
                    acys.Rec_Semanas = dr.GetValue(dr.GetOrdinal("Rec_Semanas")).ToString();

                    acys.Rec_Lunes = dr.GetBoolean(dr.GetOrdinal("Rec_Lunes"));
                    acys.Rec_Martes = dr.GetBoolean(dr.GetOrdinal("Rec_Martes"));
                    acys.Rec_Miercoles = dr.GetBoolean(dr.GetOrdinal("Rec_Miercoles"));
                    acys.Rec_Jueves = dr.GetBoolean(dr.GetOrdinal("Rec_Jueves"));
                    acys.Rec_Viernes = dr.GetBoolean(dr.GetOrdinal("Rec_Viernes"));
                    acys.Rec_Sabado = dr.GetBoolean(dr.GetOrdinal("Rec_Sabado"));

                    acys.Rec_Confirmacion = dr.GetBoolean(dr.GetOrdinal("Rec_Confirmacion"));
                    acys.Rec_Correo = dr.GetBoolean(dr.GetOrdinal("Rec_Correo"));
                    acys.Rec_Fax = dr.GetBoolean(dr.GetOrdinal("Rec_Fax"));
                    acys.Rec_Telefono = dr.GetBoolean(dr.GetOrdinal("Rec_Telefono"));
                    acys.Rec_Representante = dr.GetBoolean(dr.GetOrdinal("Rec_Representante"));
                    acys.Rec_Otro = dr.GetBoolean(dr.GetOrdinal("Rec_Otro"));

                    acys.Rec_OtroStr = dr.GetValue(dr.GetOrdinal("Rec_OtroStr")).ToString();


                    acys.Acs_PedidoEncargadoEnviar  = dr.GetValue(dr.GetOrdinal("Acs_PedidoEncargadoEnviar")).ToString();
                    acys.Acs_PedidoPuesto = dr.GetValue(dr.GetOrdinal("Acs_PedidoPuesto")).ToString();
                    acys.Acs_PedidoTelefono = dr.GetValue(dr.GetOrdinal("Acs_PedidoTelefono")).ToString();
                    acys.Acs_PedidoEmail = dr.GetValue(dr.GetOrdinal("Acs_PedidoEmail")).ToString();

        
                    acys.Acs_RecDocReposicion = dr.GetBoolean(dr.GetOrdinal("Acs_RecDocReposicion"));
                    acys.Acs_RecDocFolio = dr.GetBoolean(dr.GetOrdinal("Acs_RecDocFolio"));
                    acys.Acs_RecDocOtro  = dr.GetValue(dr.GetOrdinal("Acs_RecDocOtro")).ToString();
        

                    acys.Acs_VisitaOtro = dr.GetValue(dr.GetOrdinal("Acs_VisitaOtro")).ToString();
                    acys.Acs_ReqServAsesoria = dr.GetBoolean(dr.GetOrdinal("Acs_ReqServAsesoria"));
                    acys.Acs_ReqServTecnicoRelleno = dr.GetBoolean(dr.GetOrdinal("Acs_ReqServTecnicoRelleno"));
                    acys.Acs_ReqServMantenimiento = dr.GetBoolean(dr.GetOrdinal("Acs_ReqServMantenimiento"));
        
                    acys.Acs_Notas = dr.GetValue(dr.GetOrdinal("Acs_Notas")).ToString();
              
                    acys.Acs_ContactoRepVenta = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoRepVenta")));
                    acys.Acs_ContactoRepVentaTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoRepVentaTel")).ToString();
                    acys.Acs_ContactoRepVentaEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoRepVentaEmail")).ToString();

                    acys.Acs_ContactoRepServ = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoRepServ")));
                    acys.Acs_ContactoRepServTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoRepServTel")).ToString();
                    acys.Acs_ContactoRepServEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoRepServEmail")).ToString();

                    acys.Acs_ContactoJefServ = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoJefServ")));
                    acys.Acs_ContactoJefServTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoJefServTel")).ToString();
                    acys.Acs_ContactoJefServEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoJefServEmail")).ToString();

                    acys.Acs_ContactoAseServ = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoAseServ")));
                    acys.Acs_ContactoAseServTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoAseServTel")).ToString();
                    acys.Acs_ContactoAseServEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoAseServEmail")).ToString();


                    acys.Acs_ContactoJefOper = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoJefOper")));
                    acys.Acs_ContactoJefOperTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoJefOperTel")).ToString();
                    acys.Acs_ContactoJefOperEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoJefOperEmail")).ToString();

                    acys.Acs_ContactoCAlmRep = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoCAlmRep")));
                    acys.Acs_ContactoCAlmRepTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoCAlmRepTel")).ToString();
                    acys.Acs_ContactoCAlmRepEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoCAlmRepEmail")).ToString();

                    acys.Acs_ContactoCServTec = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoCServTec")));
                    acys.Acs_ContactoCServTecTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoCServTecTel")).ToString();
                    acys.Acs_ContactoCServTecEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoCServTecEmail")).ToString();

                    acys.Acs_ContactoCCreCob = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ContactoCCreCob")));
                    acys.Acs_ContactoCCreCobTel = dr.GetValue(dr.GetOrdinal("Acs_ContactoCCreCobTel")).ToString();
                    acys.Acs_ContactoCCreCobEmail = dr.GetValue(dr.GetOrdinal("Acs_ContactoCCreCobEmail")).ToString();
 
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarDet(Acys acys, ref System.Data.DataTable dtAcuerdos, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Acs"
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Id_Acs
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysDet_Consultar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    dtAcuerdos.Rows.Add(new object[] {
                        Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd"))), 
                        dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString(),
                        dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString(),
                        dr.GetValue(dr.GetOrdinal("Uni_Descripcion")).ToString(),
                        Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Acs_Precio"))), 
                        Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Cantidad"))), 
                        Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Frecuencia"))), 
                        Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_Lunes"))), 
                        Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_Martes"))), 
                        Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_Miercoles"))), 
                        Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_Jueves"))), 
                        Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_Viernes"))), 
                        Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_Sabado"))), 
                        dr.GetValue(dr.GetOrdinal("Acs_Documento")).ToString(),
                        Str(dr.GetValue(dr.GetOrdinal("Acs_Documento")).ToString()),  
                        dr.GetOrdinal("Acs_Modalidad").ToString(),
                        ObtieneNombreModalidad(dr.GetValue(dr.GetOrdinal("Acs_Modalidad")).ToString()),
                        Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_UltSCpt"))), 
                        Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_UltACpt"))),
                        
                       
                    });

                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string ObtieneNombreModalidad(string p)
        {
            if (p.Trim() == "A")
            {
                return "Visita del representante";
            }
            else if (p.Trim() == "B")
            {
                return "Confirmación Tel.";
            }
            else if (p.Trim() == "C")
            {
                return "Confirmación/Con consignación";
            }
            else if (p.Trim() == "D")
            {
                return "Orden Abierta/Con reposición";
            }
            else {
                return "";
            }
        }



        private string Str(string p)
        {
            if (p == "F")
                return "Factura";
            else
                return "Remisión";
        }

        public void Imprimir(Acys acys, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);
            try
            {
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Acs",
                                        "@Acs_Estatus"
                                      };
                object[] Valores = { 
                                        acys.Id_Emp,
                                        acys.Id_Cd,
                                        acys.Id_Acs,
                                        acys.Acs_Estatus
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_Imprimir", ref verificador, Parametros, Valores);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
        }

        public void ConsultarReemplazos(Acys acys, int Id_Prd, ref DataTable dt, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Acs",
                                          "@Id_Prd"
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Id_Acs,
                                       Id_Prd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysEquivalencias_ConsultarDisponible", ref dr, Parametros, Valores);
                Comun c;
                while (dr.Read())
                {
                    dt.Rows.Add(Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd_Equivalente"))),
                        dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString(),
                        Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Disponible"))),
                        0
                        );

                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ModificarEquivalencia(int Id_Prd, int Id_Prd_Original, string Id_Acys, int Id_Emp, int Id_Cd, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Acys",
                                          "@Id_PrdOriginal",
                                          "@Id_Prd"
                                      };
                object[] Valores = { 
                                       Id_Emp, 
                                       Id_Cd,
                                       Id_Acys,
                                       Id_Prd_Original,
                                       Id_Prd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysEquivalencias_Modificar", ref dr, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaAsesorias(Acys acys, string Conexion, ref List<Asesoria> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Acs"
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Id_Acs
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysAsesorias_Consultar", ref dr, Parametros, Valores);

                Asesoria a;
                while (dr.Read())
                {
                    a = new Asesoria();
                    a.Id_Ase = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ase")));
                    a.Ase_Descripcion = dr.GetValue(dr.GetOrdinal("Ase_Descripcion")).ToString();   
                    a.Ase_ServAsesoriaMensual = dr.GetBoolean(dr.GetOrdinal("Mensual"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FechaInicioMensual")))
                    {
                        a.Ase_ServAsesoriaMensualfechaIni = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaInicioMensual")));
                    }
                    a.Ase_ServAsesoriaBimestral = dr.GetBoolean(dr.GetOrdinal("Bimestral"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FechaInicioBimestral")))
                    {
                        a.Ase_ServAsesoriaBimestralfechaIni = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaInicioBimestral")));
                    }
                    a.Ase_ServAsesoriaTrimestral = dr.GetBoolean(dr.GetOrdinal("Trimestral"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FechaInicioTrimestral")))
                    {
                        a.Ase_ServAsesoriaTrimestralfechaIni = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaInicioTrimestral")));
                    }
                    List.Add(a);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaEstBi(Acys acys, string Conexion, ref List<Producto> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Cte",
                                          "@Id_Ter",
                                          "@Id_Acs"
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Id_Cte,
                                       acys.Id_Ter,
                                       acys.Id_Acs == 0 ? (int?)null:acys.Id_Acs 
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysEstBi_Consultar", ref dr, Parametros, Valores);

                Producto a;
                while (dr.Read())
                {
                    a = new Producto();
                    a.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    a.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    a.Prd_AgrupadoSpo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_AgrupadoSpo")));
                    a.Prd_InvFinal = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cantidad")));
                    a.Prd_InvInicial = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Revision")));
                    a.ServTecnicoRellenoBimestral =  dr.GetBoolean(dr.GetOrdinal("Bimestral"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FechaInicioBimestral")))
                    {
                        a.ServTecnicoRellenoBimestralfechaIni = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaInicioBimestral")));
                    }

                    a.ServTecnicoRellenoTrimestral =  dr.GetBoolean(dr.GetOrdinal("Trimestral"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FechaInicioTrimestral")))
                    {
                        a.ServTecnicoRellenoTrimestralfechaIni = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaInicioTrimestral")));
                    }
                    List.Add(a);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaEnvio(ref Acys Acs, string Conexion, ref int verificador)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Ape" 
                                          
                                      };

                object[] Valores = { 
                                       Acs.Id_Emp, 
                                       Acs.Id_Cd, 
                                       Acs.Id_Acs 
                                        
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spAcys_Envio", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Acs.Acs_Unique = dr.GetValue(dr.GetOrdinal("Acs_Unique")).ToString();
                    Acs.Acs_Solicitar = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Solicitar")));
                    if (dr.GetValue(dr.GetOrdinal("Acs_Sustituye")) != DBNull.Value)
                    {
                        Acs.Acs_Sustituye = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Sustituye")));
                    }
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaEstBiMantenimiento(Acys acys, string Conexion, ref List<Producto> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Cte",
                                          "@Id_Ter",
                                          "@Id_Acs"
                                      };
                object[] Valores = { 
                                       acys.Id_Emp, 
                                       acys.Id_Cd,
                                       acys.Id_Cte,
                                       acys.Id_Ter,
                                       acys.Id_Acs == 0 ? (int?)null:acys.Id_Acs 
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysEstBiMantenimiento_Consultar", ref dr, Parametros, Valores);

                Producto a;
                while (dr.Read())
                {
                    a = new Producto();
                    a.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    a.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    a.Prd_AgrupadoSpo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_AgrupadoSpo")));
                    a.Prd_InvFinal = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cantidad")));
                    a.Prd_InvInicial = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Revision")));
                    a.ServMantenimientoMensual = dr.GetBoolean(dr.GetOrdinal("Mensual"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FechaInicioMensual")))
                    {
                        a.ServMantenimientoMensualfechaIni = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaInicioMensual")));
                    }

                    a.ServMantenimientoBimestral = dr.GetBoolean(dr.GetOrdinal("Bimestral"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FechaInicioBimestral")))
                    {
                        a.ServMantenimientoBimestralfechaIni = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaInicioBimestral")));
                    }

                    a.ServMantenimientoTrimestral = dr.GetBoolean(dr.GetOrdinal("Trimestral"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FechaInicioTrimestral")))
                    {
                        a.ServMantenimientoTrimestralfechaIni = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaInicioTrimestral")));
                    }
                    List.Add(a);
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
