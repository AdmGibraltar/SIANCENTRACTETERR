using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;
using System.Globalization;

namespace CapaDatos
{
    public class CD_CatTerritorios
    {
        public void ConsultaTerritorios(Territorios territorio, string Conexion, ref List<Territorios> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { territorio.Id_Emp, territorio.Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorio_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    territorio = new Territorios();
                    territorio.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    territorio.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    territorio.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    territorio.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Ter_Nombre"));
                    territorio.Id_Uen = (int)dr.GetValue(dr.GetOrdinal("Id_Uen"));
                    territorio.Id_Rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Rik"))) ? -1 : (int)dr.GetValue(dr.GetOrdinal("Id_Rik"));
                    territorio.Id_Seg = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Seg"))) ? -1 : (int)dr.GetValue(dr.GetOrdinal("Id_Seg"));
                    territorio.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ter_Activo")));
                    if (Convert.ToBoolean(territorio.Estatus))
                    {
                        territorio.EstatusStr = "Activo";
                    }
                    else
                    {
                        territorio.EstatusStr = "Inactivo";
                    }
                    List.Add(territorio);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ter", 
	                                    "@Ter_Nombre", 
	                                    "@Id_Uen", 
	                                    "@Id_Rik", 
	                                    "@Id_Seg", 
	                                    "@Ter_Activo"
                                      };
                object[] Valores = { 
                                        territorio.Id_Emp,
                                        territorio.Id_Cd,
                                        territorio.Id_Ter,
                                        territorio.Descripcion,
                                        territorio.Id_Uen,
                                        territorio.Id_Rik == -1 ? (object)null : territorio.Id_Rik,
                                        territorio.Id_Seg == -1 ? (object)null : territorio.Id_Seg,
                                        territorio.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorio_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ter", 
	                                    "@Ter_Nombre", 
	                                    "@Id_Uen", 
	                                    "@Id_Rik", 
	                                    "@Id_Seg", 
	                                    "@Ter_Activo"
                                      };
                object[] Valores = { 
                                        territorio.Id_Emp,
                                        territorio.Id_Cd,
                                        territorio.Id_Ter,
                                        territorio.Descripcion,
                                        territorio.Id_Uen,
                                        territorio.Id_Rik == -1 ? (object)null : territorio.Id_Rik,
                                        territorio.Id_Seg == -1 ? (object)null : territorio.Id_Seg,
                                        territorio.Estatus
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorio_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ter" };
                object[] Valores = { territorio.Id_Emp, territorio.Id_Cd, territorio.Id_Ter };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorioDet_Consulta", ref dr, Parametros, Valores);

                CultureInfo ci = CultureInfo.CurrentCulture;
                while (dr.Read())
                {
                    dt.Rows.Add(new object[] 
                    { 
                        dr.GetValue(dr.GetOrdinal("Det_Anyo")), 
                        dr.GetValue(dr.GetOrdinal("Det_Mes")),
                        ci.TextInfo.ToTitleCase(ci.DateTimeFormat.GetMonthName((int)dr.GetValue(dr.GetOrdinal("Det_Mes")))),
                        dr.GetValue(dr.GetOrdinal("Det_Presupuesto")) 
                    });
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarTerritoriosDet(Territorios territorio, DataTable dt, string Conexion, ref int verificador)
        {
            if (dt.Rows.Count == 0) return;

            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {

                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ter", 
                                        "@Id_TerDet",
	                                    "@Det_Anyo", 
	                                    "@Det_Mes", 
	                                    "@Det_Presupuesto",
                                        "@Accion"
                                      };

                object[] Valores = null;
                SqlCommand sqlcmd = null;

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    Valores = new object[] { 
                                        territorio.Id_Emp,
                                        territorio.Id_Cd,
                                        territorio.Id_Ter,
                                        x,
                                        dt.Rows[x]["Anyo"],
                                        dt.Rows[x]["Mes"],
                                        dt.Rows[x]["Presupuesto"],
                                        1
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorioDet_Insertar", ref verificador, Parametros, Valores);

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

        public void ModificarTerritoriosDet(Territorios territorio, DataTable dt, string Conexion, ref int verificador)
        {
            if (dt.Rows.Count == 0) return;

            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {

                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ter", 
                                        "@Id_TerDet",
	                                    "@Det_Anyo", 
	                                    "@Det_Mes", 
	                                    "@Det_Presupuesto", 
                                        "@Accion"
                                      };

                object[] Valores = null;
                SqlCommand sqlcmd = null; ;

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    Valores = new object[] { 
                                        territorio.Id_Emp,
                                        territorio.Id_Cd,
                                        territorio.Id_Ter,
                                        x,
                                        dt.Rows[x]["Anyo"],
                                        dt.Rows[x]["Mes"],
                                        dt.Rows[x]["Presupuesto"],
                                        x
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorioDet_Insertar", ref verificador, Parametros, Valores);

                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ter" };
                object[] Valores = { catterr.Id_Emp, catterr.Id_Cd, catterr.Id_Ter };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorio_Consultar", ref dr, Parametros, Valores);

                CultureInfo ci = CultureInfo.CurrentCulture;

                if (dr.HasRows)
                {
                    dr.Read();
                    catterr.Uen_Descripcion = dr.GetValue(dr.GetOrdinal("Uen_Descripcion")).ToString();
                    catterr.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    catterr.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    catterr.Id_Uen = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Uen"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Uen")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id1", "@Id2", "@Id3", "@Id4" };
                object[] Valores = { catterr.Id_Emp, catterr.Id_Cd, catterr.Id_Ter, 0 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRikTerr_Combo", ref dr, Parametros, Valores);

                CultureInfo ci = CultureInfo.CurrentCulture;
                if (dr.HasRows)
                {
                    dr.Read();
                    catterr.Id_Rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    catterr.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ter" };
                object[] Valores = { catterr.Id_Emp, catterr.Id_Cd, catterr.Id_Ter };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorio", ref dr, Parametros, Valores);

                CultureInfo ci = CultureInfo.CurrentCulture;
                catterr = new Territorios();
                if (dr.HasRows)
                {
                    dr.Read();
                    catterr.Id_Ter = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ter"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    catterr.Descripcion = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtenerRepPendientesAct(string Conexion, int Id_Emp, int Id_Cd, ref DataTable DT)
        {
            DataSet ds = new DataSet();
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp",
                                        "@Id_Cd"
                                      };
                object[] Valores = { 
                                            Id_Emp,
                                            Id_Cd
                                       };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("RepresentantePendientesActualizar", ref ds, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                ds.DataSetName = "RepresentantesDS";
                DataTable D = new DataTable();
                D = ds.Tables[0];
                D.TableName = "Representantes";
                DT = D;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void GuardarAutorizacionTerritorios(CapaEntidad.ModelAutorizacionTerritorios DatosAutorizacion, ref int Respuesta, string Conexion)
        {

            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {

                CapaDatos.StartTrans();
                string[] Parametros = { 
                                        "@IdAutorizacion",
                                        "@ClaveTerritorio",
	                                    "@IdRepresentante", 
	                                    "@Territorio", 
	                                    "@Activo", 
                                        "@IdUSolicita",
	                                    "@Accion"
                                      };

                if (DatosAutorizacion.IdAutorizacion == 0)
                {
                    //Nuevo


                    CapaEntidad.AutorizacionTerritorio NuevaSolicitud = new CapaEntidad.AutorizacionTerritorio();

                    NuevaSolicitud.IdRepresentante = DatosAutorizacion.IdRepresentante;
                    NuevaSolicitud.ClaveTerritorio = DatosAutorizacion.ClaveTerritorio;
                    NuevaSolicitud.Territorio = DatosAutorizacion.Territorio;
                    NuevaSolicitud.Activo = DatosAutorizacion.Activo;
                    //Estatus: 1 Pendiente | 2 Autorizado | 3 Rechazado
                    // NuevaSolicitud.Estatus = 1;
                    NuevaSolicitud.IdUSolicita = DatosAutorizacion.IdUSolicita;
                    NuevaSolicitud.FechaSolicitud = System.DateTime.Now;
                    object[] Valores = null;
                    Valores = new object[] { 
                                        NuevaSolicitud.IdAutorizacion,
                                        NuevaSolicitud.ClaveTerritorio,
                                        NuevaSolicitud.IdRepresentante,
                                        NuevaSolicitud.Territorio,
                                        NuevaSolicitud.Activo,
                                        NuevaSolicitud.IdUSolicita,
                                        1
                                   };
                    SqlCommand sqlcmd = null;

                    sqlcmd = CapaDatos.GenerarSqlCommand("SP_InsertSolicitudCambioTerritorio", ref Respuesta, Parametros, Valores);

                    CapaDatos.CommitTrans();
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                }
                else
                {
                    //Editar


                    CapaEntidad.AutorizacionTerritorio NuevaSolicitud = new CapaEntidad.AutorizacionTerritorio();

                    NuevaSolicitud.IdRepresentante = DatosAutorizacion.IdRepresentante;
                    NuevaSolicitud.Territorio = DatosAutorizacion.Territorio;
                    NuevaSolicitud.Activo = DatosAutorizacion.Activo;
                    //Estatus: 1 Pendiente | 2 Autorizado | 3 Rechazado
                    // NuevaSolicitud.Estatus = 1;
                    NuevaSolicitud.IdUSolicita = DatosAutorizacion.IdUSolicita;
                    NuevaSolicitud.FechaSolicitud = System.DateTime.Now;
                    object[] Valores = null;
                    Valores = new object[] { 
                                        NuevaSolicitud.IdAutorizacion,
                                        NuevaSolicitud.ClaveTerritorio,
                                        NuevaSolicitud.IdRepresentante,
                                        NuevaSolicitud.Territorio,
                                        NuevaSolicitud.Activo,
                                        2,
                                        NuevaSolicitud.IdUSolicita
                                   };
                    SqlCommand sqlcmd = null;

                    sqlcmd = CapaDatos.GenerarSqlCommand("SP_InsertSolicitudCambioTerritorio", ref Respuesta, Parametros, Valores);

                    CapaDatos.CommitTrans();
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                    Respuesta = 1;
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public void AutorizarRechazarTerritorios(CapaEntidad.ModelAutorizacionTerritorios DatosAutorizacion, ref int Respuesta)
        //{

        //    try
        //    {

        //        using (var Contexto = new CapaModelo.sianwebmty_gEntities())
        //        {
        //            Contexto.Configuration.LazyLoadingEnabled = false;

        //            var AutRechz = Contexto.AutorizacionTerritorios.Where(x => x.IdAutorizacion == DatosAutorizacion.IdAutorizacion).FirstOrDefault();

        //            AutRechz.Activo = DatosAutorizacion.Activo;
        //            //Estatus: 1 Pendiente | 2 Autorizado | 3 Rechazado
        //            AutRechz.Estatus = DatosAutorizacion.Estatus;
        //            AutRechz.IdUAutoriza = DatosAutorizacion.IdUAutoriza;
        //            AutRechz.FechaAutorizo = System.DateTime.Now;


        //            Contexto.SaveChanges();

        //            Respuesta = 1;

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void ConsultaAutorizacionPendienteTerritorio(int ClaveTerritorio, ref ModelAutorizacionTerritorios SolicitudDatos, string Conexion)
        {
            SqlDataReader dr = null;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {

                string[] Parametros = { "@IdAutorizacion", "@ClaveTerritorio ", "@IdRepresentante", "@Territorio", "@Activo", "@IdUSolicita", "@Accion" };
                object[] Valores = { 0, ClaveTerritorio, 0, "", false, 0, 3 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SP_InsertSolicitudCambioTerritorio", ref dr, Parametros, Valores);

                SolicitudDatos = new CapaEntidad.ModelAutorizacionTerritorios();

                if (dr.HasRows)
                {

                    //  dr.Read();

                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    SolicitudDatos.IdAutorizacion = Convert.IsDBNull(dt.Rows[0]["IdAutorizacion"]) ? 0 : Convert.ToInt64(dt.Rows[0]["IdAutorizacion"].ToString());
                    SolicitudDatos.IdRepresentante = Convert.IsDBNull(dt.Rows[0]["IdRepresentante"]) ? 0 : Convert.ToInt32(dt.Rows[0]["IdRepresentante"].ToString());
                    SolicitudDatos.Territorio = Convert.IsDBNull(dt.Rows[0]["Territorio"]) ? "" : dt.Rows[0]["Territorio"].ToString();
                    SolicitudDatos.Activo = bool.Parse(dt.Rows[0]["Activo"].ToString());
                }
                else
                {
                    SolicitudDatos.IdAutorizacion = 0;
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public void ConsultaTerritoriosAutorizacionPendientes(int IdCiudad, int IdEmpresa, string Conexion, ref List<ModelAutorizacionTerritorios> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                        "@BaseDeDatos",
                                        "@IdSolicitud",
	                                    "@Accion"
                                      };
                object[] Valores = { "", 0, 0 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("Sp_ConsultaSolicitudesExternas ", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    ModelAutorizacionTerritorios Solicitud = new ModelAutorizacionTerritorios();
                    Solicitud.IdAutorizacion = (long)dr.GetValue(dr.GetOrdinal("IdAutorizacion"));
                    Solicitud.ClaveTerritorio = (long)dr.GetValue(dr.GetOrdinal("ClaveTerritorio"));
                    Solicitud.FechaSolicitud = (DateTime)dr.GetValue(dr.GetOrdinal("FechaSolicitud"));
                    Solicitud.NombreRepresentanteActual = (string)dr.GetValue(dr.GetOrdinal("Rik_Nombre"));
                    Solicitud.Territorio = (string)dr.GetValue(dr.GetOrdinal("Territorio"));
                    Solicitud.NombreRepresentante = (string)dr.GetValue(dr.GetOrdinal("Representante"));
                    Solicitud.TerritorioCambio = (string)dr.GetValue(dr.GetOrdinal("T_Cambio"));
                    Solicitud.Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Activo")));
                    Solicitud.NombreSolicitante = (string)dr.GetValue(dr.GetOrdinal("Autorizo"));
                    Solicitud.BdName = (string)dr.GetValue(dr.GetOrdinal("DbName"));

                    if (List.Exists(e => e.IdAutorizacion == Solicitud.IdAutorizacion && e.FechaSolicitud == Solicitud.FechaSolicitud))
                    {
                        continue;
                    }

                    List.Add(Solicitud);
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaTerritoriosAutorizacionAprobadas(int IdCiudad, int IdEmpresa, string Conexion, ref List<ModelAutorizacionTerritorios> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                         "@BaseDeDatos",
                                        "@IdSolicitud",
	                                    "@Accion"
                                      };
                object[] Valores = { "", IdEmpresa, 4 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("Sp_ConsultaSolicitudesExternas ", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    ModelAutorizacionTerritorios Solicitud = new ModelAutorizacionTerritorios();
                    Solicitud.IdAutorizacion = (long)dr.GetValue(dr.GetOrdinal("IdAutorizacion"));
                    Solicitud.ClaveTerritorio = (long)dr.GetValue(dr.GetOrdinal("ClaveTerritorio"));
                    Solicitud.FechaSolicitud = (DateTime)dr.GetValue(dr.GetOrdinal("FechaSolicitud"));
                    Solicitud.NombreRepresentanteActual = (string)dr.GetValue(dr.GetOrdinal("Rik_Nombre"));
                    Solicitud.Territorio = (string)dr.GetValue(dr.GetOrdinal("Territorio"));
                    Solicitud.NombreRepresentante = (string)dr.GetValue(dr.GetOrdinal("Representante"));
                    Solicitud.TerritorioCambio = (string)dr.GetValue(dr.GetOrdinal("T_Cambio"));
                    Solicitud.Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Activo")));
                    Solicitud.NombreAprobador = (string)dr.GetValue(dr.GetOrdinal("Autorizo"));
                    Solicitud.BdName = (string)dr.GetValue(dr.GetOrdinal("DbName"));


                    if (List.Exists(e => e.IdAutorizacion == Solicitud.IdAutorizacion && e.FechaSolicitud == Solicitud.FechaSolicitud))
                    {
                        continue;
                    }

                    List.Add(Solicitud);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    public void ConsultaTerritoriosAutorizacionRechazar(int IdCiudad, int IdEmpresa, string Conexion, ref List<ModelAutorizacionTerritorios> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                         "@BaseDeDatos",
                                        "@IdSolicitud",
	                                    "@Accion"
                                      };
                object[] Valores = { "", IdEmpresa, 6 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("Sp_ConsultaSolicitudesExternas ", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    ModelAutorizacionTerritorios Solicitud = new ModelAutorizacionTerritorios();
                    Solicitud.IdAutorizacion = (long)dr.GetValue(dr.GetOrdinal("IdAutorizacion"));
                    Solicitud.ClaveTerritorio = (long)dr.GetValue(dr.GetOrdinal("ClaveTerritorio"));
                    Solicitud.FechaSolicitud = (DateTime)dr.GetValue(dr.GetOrdinal("FechaSolicitud"));
                    Solicitud.NombreRepresentanteActual = (string)dr.GetValue(dr.GetOrdinal("Rik_Nombre"));
                    Solicitud.Territorio = (string)dr.GetValue(dr.GetOrdinal("Territorio"));
                    Solicitud.NombreRepresentante = (string)dr.GetValue(dr.GetOrdinal("Representante"));
                    Solicitud.TerritorioCambio = (string)dr.GetValue(dr.GetOrdinal("T_Cambio"));
                    Solicitud.Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Activo")));
                    Solicitud.NombreAprobador = (string)dr.GetValue(dr.GetOrdinal("Autorizo"));
                    Solicitud.BdName = (string)dr.GetValue(dr.GetOrdinal("DbName"));
                    Solicitud.Comentario = (string)dr.GetValue(dr.GetOrdinal("Comentario"));  

                    if (List.Exists(e => e.IdAutorizacion == Solicitud.IdAutorizacion && e.FechaSolicitud == Solicitud.FechaSolicitud))
                    {
                        continue;
                    }

                    List.Add(Solicitud);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void AutorizarCambioTerritorio(CapaEntidad.ModelAutorizacionTerritorios DatosAutorizacion, ref int Respuesta, string Conexion)
        {

            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {

                CapaDatos.StartTrans();
                string[] Parametros = { 
                                        "@BaseDatos",
                                        "@IdSolicitud",
	                                    "@AprobarRechazar", 
	                                    "@IdUsuario", 
	                                    "@Comentario", 	                                    
                                      };


                object[] Valores = null;
                Valores = new object[] { 
                                        DatosAutorizacion.BdName,
                                        DatosAutorizacion.IdAutorizacion,
                                        2,
                                        DatosAutorizacion.IdUAutoriza
                                        ,""
                                   };
                SqlCommand sqlcmd = null;

                sqlcmd = CapaDatos.GenerarSqlCommand("SP_AprobarRechazarSolicitudesExternas", ref Respuesta, Parametros, Valores);

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);



            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void RechazarCambioTerritorio(CapaEntidad.ModelAutorizacionTerritorios DatosAutorizacion, ref int Respuesta, string Conexion)
        {

            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {

                CapaDatos.StartTrans();
                string[] Parametros = { 
                                        "@BaseDatos",
                                        "@IdSolicitud",
	                                    "@AprobarRechazar", 
	                                    "@IdUsuario",
                                        "@Comentario"
                                      };


                object[] Valores = null;
                Valores = new object[] { 
                                        DatosAutorizacion.BdName,
                                       DatosAutorizacion.IdAutorizacion,
                                        0,
                                        DatosAutorizacion.IdUAutoriza,  
                                        DatosAutorizacion.Comentario                                     
                                   };
                SqlCommand sqlcmd = null;

                sqlcmd = CapaDatos.GenerarSqlCommand("SP_AprobarRechazarSolicitudesExternas", ref Respuesta, Parametros, Valores);

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);



            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
