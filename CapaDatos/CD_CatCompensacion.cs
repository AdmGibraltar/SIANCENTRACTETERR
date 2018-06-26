using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;

namespace CapaDatos
{
    public class CD_CatCompensacion
    {
        public void ConsultaCatCompensacion(string Conexion, ref List<CatCompensacion> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCompensacion_Consulta", ref dr);

                CatCompensacion compensacion;
                while (dr.Read())
                {
                    compensacion = new CatCompensacion();
                    compensacion.Id_Compensacion = (int)dr.GetValue(dr.GetOrdinal("Id_Compensacion"));
                    compensacion.Compensacion_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Compensacion_Descripcion"));
                    compensacion.Compensacion_Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Compensacion_Estatus")));
                    if (Convert.ToBoolean(compensacion.Compensacion_Estatus))
                    {
                        compensacion.EstatusStr = "Activo";
                    }
                    else
                    {
                        compensacion.EstatusStr = "Inactivo";
                    }
                    List.Add(compensacion);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarCatCompensacion(CatCompensacion compensacion, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Compensacion",
                                        "@Compensacion_Descripcion", 
	                                    "@Compensacion_Estatus"
                                      };
                object[] Valores = { 
                                        compensacion.Id_Compensacion,
                                        compensacion.Compensacion_Descripcion,
                                        compensacion.Compensacion_Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCompensacion_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCatCompensacion(CatCompensacion compensacion, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Compensacion",
                                        "@Compensacion_Descripcion", 
	                                    "@Compensacion_Estatus"
                                      };
                object[] Valores = { 
                                        compensacion.Id_Compensacion,
                                        compensacion.Compensacion_Descripcion,
                                        compensacion.Compensacion_Estatus
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCompensacion_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Maximo(string SP, string Conexion, ref string maximo)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(SP, ref dr);


                while (dr.Read())
                {
                    maximo = dr.GetValue(dr.GetOrdinal("Id")).ToString();
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarSistemaCompensacion(ref List<SistemaCompensacion> sistemac, ref SistemaCompensacion sistemacompensacion, CapaEntidad.Sesion sesion,
         string NombreSistema, DateTime? FechaIni, DateTime? FechaFin, string Estatus, int Cd)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);


                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",                                            
                                          "@NombreSistema",//NOMBRE sistema
                                          "@FechaIni",
                                          "@FechaFin",
                                          "@Estatus",
                                          "@Id_CdConsultar" 
                                      };

                object[] Valores = {
                                       sesion.Id_Emp,
                                       sesion.Id_Cd_Ver,
                                       NombreSistema==""?(object)null : NombreSistema, 
                                       FechaIni, 
                                       FechaFin,
                                       Estatus == "" ? (object)null : Estatus,
                                       Cd == -1 ? (object)null : Cd 
                                   };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapSistemaCompensacion_ListaConsulta", ref dr, parametros, Valores);

                while (dr.Read())
                {
                    SistemaCompensacion reciboremision = new SistemaCompensacion();
                    reciboremision.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    reciboremision.Id_Cd = dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    reciboremision.Id_Sistema = dr.GetInt32(dr.GetOrdinal("Id_Sistema"));
                    reciboremision.NombreSistema = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("NombreSistema"))) ? "" : dr.GetString(dr.GetOrdinal("NombreSistema"));
                    if (!Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FechaInicio"))))
                        reciboremision.FechaInicio = dr.GetDateTime(dr.GetOrdinal("FechaInicio"));
                    if (!Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FechaFin"))))
                        reciboremision.FechaFin = dr.GetDateTime(dr.GetOrdinal("FechaFin"));

                    if (!Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Estatus"))))
                        reciboremision.Estatus = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Estatus")));
                    reciboremision.NombreEstatus = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("NombreEstatus"))) ? "" : dr.GetString(dr.GetOrdinal("NombreEstatus"));
                    if (!Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FechaUltimaMod"))))
                        reciboremision.FechaUltimaMod = dr.GetDateTime(dr.GetOrdinal("FechaUltimaMod"));

                    sistemac.Add(reciboremision);

                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void InsertarConfiguracionSistemacompensacion(SistemaCompensacion confsistcompensacion, string Conexion, ref int verificador)
        {
            using (CapaDatos.dbAccess oDB = new CapaDatos.dbAccess(Conexion))
            {

                try
                {
                    verificador = (int)oDB.spExecScalar(
                        "spCapSistemaCompensacion_Insertar",
                        new SqlParameter("@Id_Emp", confsistcompensacion.Id_Emp),
                        new SqlParameter("@Id_Cd", confsistcompensacion.Id_Cd),
                        new SqlParameter("@Id_Sistema", confsistcompensacion.Id_Sistema),
                        new SqlParameter("@Id_Perfil", confsistcompensacion.Id_Perfil),

                        new SqlParameter("@NombreSistema", confsistcompensacion.NombreSistema),
                        new SqlParameter("@FechaInicio", confsistcompensacion.FechaInicio),
                        new SqlParameter("@FechaFin", confsistcompensacion.FechaFin),
                        new SqlParameter("@Estatus", confsistcompensacion.Estatus)
                    );

                    //JFCV para evitar inconsistencia, al mandar insertar y el id ya existe , genera un nuevo valor para el id 
                    // ese valor lo guardo para que lo tenga al insertar el detalle 
                    if (verificador != 0)
                    {
                        confsistcompensacion.Id_Sistema = verificador;
                    }

                    for (int CR = 0; CR <= (confsistcompensacion.listaConceptoVariables.Count - 1); CR++)
                    {
                        oDB.spExecNonQuery(
                            "spCapCapConceptoVariable_Insertar",
                            new SqlParameter("@Id_Emp", confsistcompensacion.Id_Emp),
                            new SqlParameter("@Id_Cd", confsistcompensacion.Id_Cd),
                            new SqlParameter("@Id_Sistema", confsistcompensacion.Id_Sistema),
                            new SqlParameter("@Id_ConceptoVariable", CR + 1),
                            new SqlParameter("@Concepto_Descripcion", confsistcompensacion.listaConceptoVariables[CR].Concepto_Descripcion),
                            new SqlParameter("@Concepto_Observaciones", confsistcompensacion.listaConceptoVariables[CR].Concepto_Observaciones),
                            new SqlParameter("@Operador", confsistcompensacion.listaConceptoVariables[CR].Operador),
                            new SqlParameter("@TipoVariable", confsistcompensacion.listaConceptoVariables[CR].TipoVariable),
                            new SqlParameter("@IdVariable", confsistcompensacion.listaConceptoVariables[CR].IdVariable),
                            new SqlParameter("@VariableDescripcion", confsistcompensacion.listaConceptoVariables[CR].VariableDescripcion)

                        );
                    }

                    for (int CRV = 0; CRV <= (confsistcompensacion.listaVariables.Count - 1); CRV++)
                    {
                        oDB.spExecNonQuery(
                            "spCapCapConceptoVariables_Insertar",
                            new SqlParameter("@Id_Emp", confsistcompensacion.Id_Emp),
                            new SqlParameter("@Id_Cd", confsistcompensacion.Id_Cd),
                            new SqlParameter("@Id_Sistema", confsistcompensacion.Id_Sistema),
                            new SqlParameter("@Id_VariableLocal", confsistcompensacion.listaVariables[CRV].Id_VariableLocal),
                            new SqlParameter("@sVariable_Local", confsistcompensacion.listaVariables[CRV].sVariable_Local),
                            new SqlParameter("@sVariable_Descripcion", confsistcompensacion.listaVariables[CRV].sVariable_Descripcion),
                            new SqlParameter("@sVariable_Comentarios", confsistcompensacion.listaVariables[CRV].sVariable_Comentarios),
                            new SqlParameter("@sVariable_Formula", confsistcompensacion.listaVariables[CRV].sVariable_Formula)
                             

                        );
                    }

                    for (int CRV = 0; CRV <= (confsistcompensacion.ListaConceptoVariableReporte.Count - 1); CRV++)
                    {
                        oDB.spExecNonQuery(
                            "spCapCapConceptoVariableReporte_Insertar",
                            new SqlParameter("@Id_Emp", confsistcompensacion.Id_Emp),
                            new SqlParameter("@Id_Cd", confsistcompensacion.Id_Cd),
                            new SqlParameter("@Id_Sistema", confsistcompensacion.Id_Sistema),
                            new SqlParameter("@Id_VariableReporte", confsistcompensacion.ListaConceptoVariableReporte[CRV].Id_VariableReporte),
                            new SqlParameter("@Id_Parent", confsistcompensacion.ListaConceptoVariableReporte[CRV].Id_Parent == null ? -1 : confsistcompensacion.ListaConceptoVariableReporte[CRV].Id_Parent),
                            new SqlParameter("@Id_VariableLocal", confsistcompensacion.ListaConceptoVariableReporte[CRV].Id_VariableLocal),
                            new SqlParameter("@EsBold", confsistcompensacion.ListaConceptoVariableReporte[CRV].EsBold),
                            new SqlParameter("@sVariable_Descripcion", confsistcompensacion.ListaConceptoVariableReporte[CRV].sVariable_Descripcion)
                        );
                    }

                 



                    //JFCV 05 abr 2016
                    //oDB.Commit();
                }
                catch (Exception ex)
                {
                    verificador = 0;
                    //JFCV 05 abr 2016
                    // oDB.RollBack();
                    throw ex;
                }
                finally
                {
                    oDB.Dispose();
                }
            }
        }

        public void ModificarConfiguracionSistemacompensacion(SistemaCompensacion confsistcompensacion, string Conexion, ref int verificador)
        {
            using (CapaDatos.dbAccess oDB = new CapaDatos.dbAccess(Conexion))
            {

                try
                {
                    verificador = (int)oDB.spExecScalar(
                    "spCapSistemaCompensacion_Modificar",
                    new SqlParameter("@Id_Emp", confsistcompensacion.Id_Emp),
                    new SqlParameter("@Id_Cd", confsistcompensacion.Id_Cd),
                    new SqlParameter("@Id_Sistema", confsistcompensacion.Id_Sistema),
                    new SqlParameter("@Id_Perfil", confsistcompensacion.Id_Perfil),

                    new SqlParameter("@NombreSistema", confsistcompensacion.NombreSistema),
                    new SqlParameter("@FechaInicio", confsistcompensacion.FechaInicio),
                    new SqlParameter("@FechaFin", confsistcompensacion.FechaFin),
                    new SqlParameter("@Estatus", confsistcompensacion.Estatus)
                );


                    if (verificador == 1)
                    {

                        verificador = (int)oDB.spExecScalar("spCapSistemaCompensacion_Eliminar",
                                         new SqlParameter("@Id_Emp", confsistcompensacion.Id_Emp),
                                         new SqlParameter("@Id_Cd", confsistcompensacion.Id_Cd),
                                         new SqlParameter("@Id_Sistema", confsistcompensacion.Id_Sistema)
                                 );
                        if (verificador == 1)
                        {
                            for (int CR = 0; CR <= (confsistcompensacion.listaConceptoVariables.Count - 1); CR++)
                            {
                                oDB.spExecNonQuery(
                                    "spCapCapConceptoVariable_Insertar",
                                    new SqlParameter("@Id_Emp", confsistcompensacion.Id_Emp),
                                    new SqlParameter("@Id_Cd", confsistcompensacion.Id_Cd),
                                    new SqlParameter("@Id_Sistema", confsistcompensacion.Id_Sistema),
                                    new SqlParameter("@Id_ConceptoVariable", CR + 1),
                                    new SqlParameter("@Concepto_Descripcion", confsistcompensacion.listaConceptoVariables[CR].Concepto_Descripcion),
                                    new SqlParameter("@Concepto_Observaciones", confsistcompensacion.listaConceptoVariables[CR].Concepto_Observaciones),
                                    new SqlParameter("@Operador", confsistcompensacion.listaConceptoVariables[CR].Operador),
                                    new SqlParameter("@TipoVariable", confsistcompensacion.listaConceptoVariables[CR].TipoVariable),
                                    new SqlParameter("@IdVariable", confsistcompensacion.listaConceptoVariables[CR].IdVariable),
                                    new SqlParameter("@VariableDescripcion", confsistcompensacion.listaConceptoVariables[CR].VariableDescripcion)

                                );
                            }

                            for (int CRV = 0; CRV <= (confsistcompensacion.listaVariables.Count - 1); CRV++)
                            {
                                oDB.spExecNonQuery(
                                    "spCapCapConceptoVariables_Insertar",
                                    new SqlParameter("@Id_Emp", confsistcompensacion.Id_Emp),
                                    new SqlParameter("@Id_Cd", confsistcompensacion.Id_Cd),
                                    new SqlParameter("@Id_Sistema", confsistcompensacion.Id_Sistema),
                                    new SqlParameter("@Id_VariableLocal", confsistcompensacion.listaVariables[CRV].Id_VariableLocal),
                                    new SqlParameter("@sVariable_Local", confsistcompensacion.listaVariables[CRV].sVariable_Local),
                                    new SqlParameter("@sVariable_Descripcion", confsistcompensacion.listaVariables[CRV].sVariable_Descripcion),
                                    new SqlParameter("@sVariable_Comentarios", confsistcompensacion.listaVariables[CRV].sVariable_Comentarios),
                                    new SqlParameter("@sVariable_Formula", confsistcompensacion.listaVariables[CRV].sVariable_Formula)
 

                                );
                            }

                            for (int CRV = 0; CRV <= (confsistcompensacion.ListaConceptoVariableReporte.Count - 1); CRV++)
                            {
                                oDB.spExecNonQuery(
                                    "spCapCapConceptoVariableReporte_Insertar",
                                    new SqlParameter("@Id_Emp", confsistcompensacion.Id_Emp),
                                    new SqlParameter("@Id_Cd", confsistcompensacion.Id_Cd),
                                    new SqlParameter("@Id_Sistema", confsistcompensacion.Id_Sistema),
                                    new SqlParameter("@Id_VariableReporte", confsistcompensacion.ListaConceptoVariableReporte[CRV].Id_VariableReporte),
                                    new SqlParameter("@Id_Parent", confsistcompensacion.ListaConceptoVariableReporte[CRV].Id_Parent == null ? -1 : confsistcompensacion.ListaConceptoVariableReporte[CRV].Id_Parent),
                                    new SqlParameter("@Id_VariableLocal", confsistcompensacion.ListaConceptoVariableReporte[CRV].Id_VariableLocal),
                                    new SqlParameter("@EsBold", confsistcompensacion.ListaConceptoVariableReporte[CRV].EsBold),
                                    new SqlParameter("@sVariable_Descripcion", confsistcompensacion.ListaConceptoVariableReporte[CRV].sVariable_Descripcion)
                                );
                            }

                        }
                    }
                    ////////////JFCV 22dic Transacciones  oDB.Commit();
                }
                catch (Exception ex)
                {
                    verificador = 0;
                    ////////////JFCV 22dic Transacciones  oDB.RollBack();
                    throw ex;
                }
                finally
                {
                    oDB.Dispose();
                }
            }
        }

        public void EliminarConfiguracionSistemacompensacion(SistemaCompensacion confsistcompensacion, string Conexion, ref int verificador)
        {
            using (CapaDatos.dbAccess oDB = new CapaDatos.dbAccess(Conexion))
            {
                ////////////JFCV 22dic Transacciones  oDB.BeginTransaction();
                try
                {
                    verificador = (int)oDB.spExecScalar("spCapSistemaCompensacion_Eliminar",
                                          new SqlParameter("@Id_Emp", confsistcompensacion.Id_Emp),
                                          new SqlParameter("@Id_Cd", confsistcompensacion.Id_Cd),
                                          new SqlParameter("@Id_Sistema", confsistcompensacion.Id_Sistema)
                );

                    ////////////JFCV 22dic Transacciones oDB.Commit();

                }
                catch (Exception ex)
                {
                    verificador = 0;
                    ////////////JFCV 22dic Transacciones  oDB.RollBack();
                    throw ex;
                }
                finally
                {
                    oDB.Dispose();
                }
            }
        }


        public void ConsultaConfiguracionSistemacompensacion(SistemaCompensacion confsistcompensacion, string Conexion)
        {
            using (dbAccess oDB = new dbAccess(Conexion))
            {
                try
                {
                    DataSet DS = oDB.spExecDataSet(
                        "spCapSistemaCompensacion_Consulta",
                        new SqlParameter("@Id_Emp", confsistcompensacion.Id_Emp),
                        new SqlParameter("@Id_Cd", confsistcompensacion.Id_Cd),
                        new SqlParameter("@Id_Sistema", confsistcompensacion.Id_Sistema)
                    );


                    foreach (DataRow DR in DS.Tables[0].Rows)
                    {
                        confsistcompensacion.Id_Emp = (int)DR["Id_Emp"];
                        confsistcompensacion.Id_Cd = (int)DR["Id_Cd"];
                        confsistcompensacion.Id_Sistema = (int)DR["Id_Sistema"];
                        confsistcompensacion.NombreSistema = (string)DR["NombreSistema"];
                        confsistcompensacion.FechaInicio = DateTime.Parse(DR["FechaInicio"].ToString());
                        confsistcompensacion.FechaFin = DateTime.Parse(DR["FechaFin"].ToString());
                        confsistcompensacion.FechaUltimaMod = DateTime.Parse(DR["FechaUltimaMod"].ToString());
                        confsistcompensacion.NombreEstatus = DR["NombreEstatus"].ToString();
                        confsistcompensacion.Estatus = (int)DR["Estatus"];

                        confsistcompensacion.Id_Perfil =  DR["Id_Perfil"] == System.DBNull.Value ? null : (string)(DR["Id_Perfil"]);
                        confsistcompensacion.ImpEdoConsolidado = DR["ImpEdoConsolidado"] == System.DBNull.Value ? null : (string)(DR["ImpEdoConsolidado"]);

                      
                         
                        //confsistcompensacion.Id_GV = DR["Id_GV"] == System.DBNull.Value ? 0 : (int)(DR["Id_GV"]);
                        

                        DataSet DSFile = oDB.spExecDataSet(
                            "spCapSistemaCompensacionConceptoVariable_Consulta",
                            new SqlParameter("@Id_Emp", confsistcompensacion.Id_Emp),
                            new SqlParameter("@Id_Cd", confsistcompensacion.Id_Cd),
                            new SqlParameter("@Id_Sistema", confsistcompensacion.Id_Sistema)
                        );
 
                        if (DSFile != null && DSFile.Tables.Count > 0 && DSFile.Tables[0].Rows.Count > 0)
                        {

                           List<ConceptoVariable> listaProdPre = new List<ConceptoVariable>();
 
 
                            foreach (DataRow row in DSFile.Tables[0].Rows)
                                {
                       
                                       ConceptoVariable concepto = new ConceptoVariable();
                                        concepto.Id_Cd = confsistcompensacion.Id_Cd;
                                        concepto.Id_Emp = confsistcompensacion.Id_Emp;
                                        concepto.Id_Sistema = confsistcompensacion.Id_Sistema;
                                        concepto.Id_ConceptoVariable = Convert.ToInt32(row["Id_ConceptoVariable"]);
                                        concepto.Concepto_Descripcion = (string)row["Concepto_Descripcion"];
                                        concepto.Concepto_Observaciones = (string)row["Concepto_Observaciones"];
                                        concepto.Operador = (string)row["Concepto_Operador"];
                                        concepto.TipoVariable = Convert.ToInt32(row["TipoVariable"]);
                                        concepto.IdVariable = Convert.ToInt32(row["IdVariable"]);
                                        concepto.VariableDescripcion = (string)row["VariableDescripcion"];
                                        concepto.FechaElaboracion = DateTime.Now;
                                        listaProdPre.Add(concepto);
                           
                                    }

                                confsistcompensacion.listaConceptoVariables = listaProdPre;

                        }

                        #region Variables 
                        DataSet DSFileVar = oDB.spExecDataSet(
                           "spCapSistemaCompensacionConceptoVariables_Consulta",
                           new SqlParameter("@Id_Emp", confsistcompensacion.Id_Emp),
                           new SqlParameter("@Id_Cd", confsistcompensacion.Id_Cd),
                           new SqlParameter("@Id_Sistema", confsistcompensacion.Id_Sistema)
                       );

                        if (DSFileVar != null && DSFileVar.Tables.Count > 0 && DSFileVar.Tables[0].Rows.Count > 0)
                        {

                            List<ConceptoVariables> listaVariables = new List<ConceptoVariables>();


                            foreach (DataRow row in DSFileVar.Tables[0].Rows)
                            {

                                ConceptoVariables variable = new ConceptoVariables();
                                variable.Id_Cd = confsistcompensacion.Id_Cd;
                                variable.Id_Emp = confsistcompensacion.Id_Emp;
                                variable.Id_Sistema = confsistcompensacion.Id_Sistema;
                                variable.Id_VariableLocal = Convert.ToInt32(row["Id_VariableLocal"]);
                                variable.sVariable_Local = (string)row["sVariable_Local"];
                                variable.sVariable_Descripcion = (string)row["sVariable_Descripcion"];
                                variable.sVariable_Comentarios = row["sVariable_Comentarios"].GetType().Name == "" ? (string)null  : (string)row["sVariable_Comentarios"];
                                variable.sVariable_Formula = row["sVariable_Formula"].GetType().Name == "" ? (string)null : (string)row["sVariable_Formula"];  
                                listaVariables.Add(variable);

                                

                            }

                            confsistcompensacion.listaVariables = listaVariables;
                            

                        }

                        #endregion Variables 

                        
                        #region Variables Reporte 
                        DataSet DSFileVarRep = oDB.spExecDataSet(
                           "spCapSistemaCompensacionConceptoVariableReporte_Consulta",
                           new SqlParameter("@Id_Emp", confsistcompensacion.Id_Emp),
                           new SqlParameter("@Id_Cd", confsistcompensacion.Id_Cd),
                           new SqlParameter("@Id_Sistema", confsistcompensacion.Id_Sistema)
                       );

                        if (DSFileVarRep != null && DSFileVarRep.Tables.Count > 0 && DSFileVarRep.Tables[0].Rows.Count > 0)
                        {

                            List<ConceptoVariableReporte> listaVariablesRep = new List<ConceptoVariableReporte>();


                            foreach (DataRow row in DSFileVarRep.Tables[0].Rows)
                            {

                                ConceptoVariableReporte reporte = new ConceptoVariableReporte();
                                reporte.Id_Cd = confsistcompensacion.Id_Cd;
                                reporte.Id_Emp = confsistcompensacion.Id_Emp;
                                reporte.Id_Sistema = confsistcompensacion.Id_Sistema;
                                reporte.Id_VariableReporte = Convert.ToInt32(row["Id_VariableReporte"]);
                                reporte.Id_Parent = Convert.ToInt32(row["Id_Parent"]);
                                reporte.Id_VariableLocal = Convert.ToInt32(row["Id_VariableLocal"]);
                                reporte.EsBold = (bool)row["EsBold"];
                                reporte.sVariable_Descripcion = (string)row["sVariable_Descripcion"];
                                if (reporte.Id_Parent == -1)
                                    reporte.Id_Parent = null;
                                listaVariablesRep.Add(reporte);
                            }

                            confsistcompensacion.ListaConceptoVariableReporte = listaVariablesRep;


                        }

                        #endregion Variables Reporte


                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    oDB.Dispose();
                }
            }
        }

        public void CopiarConfiguracionSistemacompensacion(SistemaCompensacion confsistcompensacion, string Conexion, ref int verificador)
        {
            using (CapaDatos.dbAccess oDB = new CapaDatos.dbAccess(Conexion))
            {

                try
                {
                    verificador = (int)oDB.spExecScalar(
                        "spCapSistemaCompensacion_Copiar",
                        new SqlParameter("@Id_Emp", confsistcompensacion.Id_Emp),
                        new SqlParameter("@Id_Cd", confsistcompensacion.Id_Cd),
                        new SqlParameter("@Id_Sistema", confsistcompensacion.Id_Sistema)
                    );
 
                    if (verificador != 0)
                    {
                        confsistcompensacion.Id_Sistema = verificador;
                    }
 
                }
                catch (Exception ex)
                {
                    verificador = 0;
   
                    throw ex;
                }
                finally
                {
                    oDB.Dispose();
                }
            }
        }



        public void ReporteComisionesGetXML(SistemaCompensacionGetXML confsistcompensacionxml, string Conexion)
        {
            using (dbAccess oDB = new dbAccess(Conexion,4200))
            {
                try
                {
                    
                    DataSet DS = oDB.spExecDataSet(
                        "spRepComisionesGetXML",
                        new SqlParameter("@Id_Emp", confsistcompensacionxml.Id_Emp),
                        new SqlParameter("@Id_Cd", confsistcompensacionxml.Id_Cd),
                        new SqlParameter("@Anio", confsistcompensacionxml.Anio),
                        new SqlParameter("@Mes", confsistcompensacionxml.Mes),
                        new SqlParameter("@Id_Rik", confsistcompensacionxml.Id_Rik),
                        new SqlParameter("@Id_Sistema", confsistcompensacionxml.Id_Sistema),
                        new SqlParameter("@Id_TipoRepresentante", confsistcompensacionxml.Id_TipoRepresentante),
                        new SqlParameter("@Id_Representante", confsistcompensacionxml.Id_Representante)
                    );


                    foreach (DataRow DR in DS.Tables[0].Rows)
                    {
                        confsistcompensacionxml.Id_Emp = (int)DR["Id_Emp"];
                        confsistcompensacionxml.Id_Cd = (int)DR["Id_Cd"];
                        confsistcompensacionxml.Id_Sistema = (int)DR["Id_Sistema"];
                        //confsistcompensacionxml.Parametros = (string)DR["Parametros"];
                        confsistcompensacionxml.Parametros = DR["Parametros"] == System.DBNull.Value ? null : (string)(DR["Parametros"]);
                        confsistcompensacionxml.Clientes = DR["Clientes"] == System.DBNull.Value ? null : (string)(DR["Clientes"]);
                        confsistcompensacionxml.Conceptos = DR["Conceptos"] == System.DBNull.Value ? null : (string)(DR["Conceptos"]);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    oDB.Dispose();
                }
            }
        }


        public void ConsultaRepresentantesListado(SistemaCompensacionGetXML sistemaCompensacionGetXML, string Conexion, ref List<ReporteComisiones> list)
        {
            
            try
            {
                if (sistemaCompensacionGetXML.Id_TipoRepresentante == 0)
                {
                    sistemaCompensacionGetXML.Id_TipoRepresentante = -1;
                }
                if (sistemaCompensacionGetXML.Id_Representante == 0)
                {
                    sistemaCompensacionGetXML.Id_Representante = -1;
                }

                SqlDataReader DR = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Tipo_representante", "@Id_Representante" , "@Id_TipoCDI"};
                object[] Valores = { sistemaCompensacionGetXML.Id_Emp, 
                                       sistemaCompensacionGetXML.Id_Cd == -1 ? (object)null : sistemaCompensacionGetXML.Id_Cd,
                                       sistemaCompensacionGetXML.Id_TipoRepresentante == -1 ? (object)null : sistemaCompensacionGetXML.Id_TipoRepresentante
                                   ,sistemaCompensacionGetXML.Id_Representante == -1 ? (object)null : sistemaCompensacionGetXML.Id_Representante
                                   ,sistemaCompensacionGetXML.Id_TipoCDI == -1 ? (object)null : sistemaCompensacionGetXML.Id_TipoCDI
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRikRepresentante_Lista", ref DR, Parametros, Valores);

                while (DR.Read())
                {

                    //foreach (DataRow DR in DS.Tables[0].Rows)
                    //{
                    ReporteComisiones  reportecomisiones = new ReporteComisiones();
                    reportecomisiones.Id_Emp = (int)DR["Id_Emp"];
                    reportecomisiones.Id_Cd = (int)DR["Id_Cd"];
                    reportecomisiones.Imprimir = (int)DR["Imprimir"];
                    reportecomisiones.Id_TipoRepresentante = (int)DR["Id_TipoRepresentante"];
                    reportecomisiones.Id = (int)DR["Id"];
                    reportecomisiones.Rik_Descripcion = DR["Rik_Descripcion"].ToString();
                    reportecomisiones.Nombre_Empleado = DR["Nombre_empleado"].ToString();
                    reportecomisiones.CdiNombre = DR["CdiNombre"].ToString();
                    reportecomisiones.Estatus = (int)DR["Estatus"];



                    list.Add(reportecomisiones);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ReporteConcentradoant(SistemaCompensacionGetXML rik, string Conexion, ref List<ReporteComisiones> registrorik)
        {

            try
            {
              

                SqlDataReader DR = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Anio","@Mes" ,"@Id_Rik","@Reporte"    };
                object[] Valores = { rik.Id_Emp, 
                                       rik.Id_Cd == -1 ? (object)null : rik.Id_Cd,
                                       rik.Anio, 
                                       rik.Mes, 
                                       rik.Id_Rik == -1 ? (object)null : rik.Id_Rik
                                   ,4   };
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.CommandTimeout =0;
                sqlcmd = CapaDatos.GenerarSqlCommand("spRepComisiones", ref DR, Parametros, Valores);
                
                while (DR.Read())
                {
                    

                        ReporteComisiones reportecomisiones = new ReporteComisiones();

                        reportecomisiones.Id_Emp = rik.Id_Emp;
                        reportecomisiones.Id_Cd = rik.Id_Cd;
                        reportecomisiones.Imprimir = 1;
                        reportecomisiones.Id_TipoRepresentante = rik.Id_TipoRepresentante;
                        reportecomisiones.Id = (int)DR["Id_Rik"];
                        reportecomisiones.Nombre_Empleado = (string)DR["Nombre_empleado"];
                        reportecomisiones.CdiNombre = (string)DR["CdiNombre"];
                        reportecomisiones.Estatus = 1;
                        reportecomisiones.VtaCob = (double)DR["VtaCob"];
                        reportecomisiones.UP = (double)DR["UP"];
                        reportecomisiones.UB = (double)DR["UB"];
                        reportecomisiones.CP = (double)DR["CP"];
                        reportecomisiones.MVI = (double)DR["MVI"];
                        reportecomisiones.ComBaseAjustada = (double)DR["ComBaseAjustada"];
                        reportecomisiones.GtoAdmin = (double)DR["GtoAdmin"];
                        reportecomisiones.ComisionNeta = (double)DR["ComisionNeta"];
                        //confsistcompensacionxml.Parametros = DR["Parametros"] == System.DBNull.Value ? null : (string)(DR["Parametros"]);
                        //confsistcompensacionxml.Clientes = DR["Clientes"] == System.DBNull.Value ? null : (string)(DR["Clientes"]);
                        //confsistcompensacionxml.Conceptos = DR["Conceptos"] == System.DBNull.Value ? null : (string)(DR["Conceptos"]);

                        registrorik.Add(reportecomisiones);
 

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void ReporteConcentrado(SistemaCompensacionGetXML rik, string Conexion, ref List<ReporteComisiones> registrorik)
        {


            using (dbAccess oDB = new dbAccess(Conexion, 800000))
            {
                try
                {

                    DataSet DS = oDB.spExecDataSet(
                        "spRepComisiones",
                        new SqlParameter("@Id_Emp", rik.Id_Emp),
                        new SqlParameter("@Id_Cd", rik.Id_Cd),
                        new SqlParameter("@Anio", rik.Anio),
                        new SqlParameter("@Mes", rik.Mes),
                        new SqlParameter("@Id_Rik", rik.Id_Rik),
                        new SqlParameter("@Reporte", 4) 
                       
                    );

                 
                       foreach (DataRow DR in DS.Tables[0].Rows)
                    {

                           ReporteComisiones reportecomisiones = new ReporteComisiones();

                           reportecomisiones.Id_Emp = rik.Id_Emp;
                           reportecomisiones.Id_Cd = (int)DR["Id_cd"];
                           reportecomisiones.Imprimir = 1;
                           reportecomisiones.Id_TipoRepresentante = rik.Id_TipoRepresentante;
                           reportecomisiones.Id = (int)DR["Id_Rik"];
                           reportecomisiones.Nombre_Empleado = (string)DR["Nombre_empleado"];
                           reportecomisiones.CdiNombre = (string)DR["CdiNombre"];
                           reportecomisiones.Estatus = 1;
                           reportecomisiones.VtaCob = (double)DR["VtaCob"];
                           reportecomisiones.UP = (double)DR["UP"];
                           reportecomisiones.Amortizacion = (double)DR["AN"];
                           reportecomisiones.GST = (double)DR["GST"];
                           reportecomisiones.UB = (double)DR["UB"];
                           reportecomisiones.UB = (double)DR["CB"];
                           reportecomisiones.AjCobranza = (double)DR["AjCobranza"];
                           reportecomisiones.CP = (double)DR["CP"];
                           reportecomisiones.CND = (double)DR["CND"];
                           reportecomisiones.MVI = (double)DR["MVI"];
                           reportecomisiones.ComBaseAjustada = (double)DR["ComBaseAjustada"];
                           reportecomisiones.GtoAdmin = (double)DR["GtoAdmin"];
                           reportecomisiones.ComisionNeta = (double)DR["ComisionNeta"];
                           
                           //confsistcompensacionxml.Parametros = DR["Parametros"] == System.DBNull.Value ? null : (string)(DR["Parametros"]);
                           //confsistcompensacionxml.Clientes = DR["Clientes"] == System.DBNull.Value ? null : (string)(DR["Clientes"]);
                           //confsistcompensacionxml.Conceptos = DR["Conceptos"] == System.DBNull.Value ? null : (string)(DR["Conceptos"]);

                           registrorik.Add(reportecomisiones);

                        }

                  
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    oDB.Dispose();
                }
            }

        }

        /// <summary>
        /// ReporteConcentradoFranquicias Encabezado
        /// </summary>
        /// <param name="rik"></param>
        /// <param name="Conexion"></param>
        /// <param name="registrorik"></param>
        public void ReporteConcentradoFranquicias(SistemaCompensacionGetXML rik, string Conexion, ref List<ReporteComisiones> registrorik)
        {

            using (dbAccess oDB = new dbAccess(Conexion, 800000))
            {
                try
                {

                    DataSet DS = oDB.spExecDataSet(
                        "spRepComisionesReporteFranquicias",
                        new SqlParameter("@Id_Emp", rik.Id_Emp),
                        new SqlParameter("@Anio", rik.Anio),
                        new SqlParameter("@Mes", rik.Mes),
                        new SqlParameter("@Id_TipoRepresentante", rik.Id_TipoRepresentante),
                        new SqlParameter("@Id_Cd", rik.Id_Cd),
                         new SqlParameter("@Id_Reporte", 0),
                        new SqlParameter("@Id_Sistema", rik.Id_Sistema)
                  
                    );


                    foreach (DataRow DR in DS.Tables[0].Rows)
                    {

                        ReporteComisiones reportecomisiones = new ReporteComisiones();

                        reportecomisiones.Id_Emp = rik.Id_Emp;
                        reportecomisiones.Id_Cd = (int)DR["CDI"];
                        reportecomisiones.Imprimir = 1;
                        reportecomisiones.Id_TipoRepresentante = rik.Id_TipoRepresentante;
                        reportecomisiones.Id = (int)DR["Rik"];
                        reportecomisiones.Nombre_Empleado = (string)DR["Nombre_Rik"];
                        reportecomisiones.CdiNombre = (string)DR["NombreCD"];
                        reportecomisiones.Estatus = 1;
                        reportecomisiones.Amortizacion = (double)DR["Amortizacion"];
                        reportecomisiones.TecSer = (double)DR["TecSer"];
                        reportecomisiones.UTrem = (int)DR["UTrem"];
                        reportecomisiones.ComisionNeta = (double)DR["ComisionNeta"];
                        reportecomisiones.MVI = (double)DR["MVI"];
                        reportecomisiones.Año = rik.Anio;
                        reportecomisiones.Mes = rik.Mes;
               
                        registrorik.Add(reportecomisiones);

                    }


                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    oDB.Dispose();
                }
            }

        }


        /// <summary>
        /// ReporteConcentradoFranquicias Detalle de Cliente  1
        /// </summary>
        /// <param name="rik"></param>
        /// <param name="Conexion"></param>
        /// <param name="registrorik"></param>
        public void ReporteConcentradoFranquiciasDetCliente(SistemaCompensacionGetXML rik, string Conexion, ref List<ReporteComisiones> registrorik)
        {


            using (dbAccess oDB = new dbAccess(Conexion, 8000))
            {
                try
                {

                    DataSet DS = oDB.spExecDataSet(
                        "spRepComisionesReporteFranquicias",
                        new SqlParameter("@Id_Emp", rik.Id_Emp),
                        new SqlParameter("@Anio", rik.Anio),
                        new SqlParameter("@Mes", rik.Mes),
                        new SqlParameter("@Id_TipoRepresentante", rik.Id_TipoRepresentante),
                        new SqlParameter("@Id_Cd", rik.Id_Cd),
                         new SqlParameter("@Id_Reporte", 1),
                        new SqlParameter("@Id_Sistema", rik.Id_Sistema)
                  
                    );


                    foreach (DataRow DR in DS.Tables[0].Rows)
                    {

                        ReporteComisiones reportecomisiones = new ReporteComisiones();

                        reportecomisiones.Id_Emp = rik.Id_Emp;
                        reportecomisiones.Id_Cd = (int)DR["Id_Cd"];
                        reportecomisiones.Imprimir = 1;
                        reportecomisiones.Id_TipoRepresentante = rik.Id_TipoRepresentante;
                        reportecomisiones.Id = (int)DR["Id_Rik"];
                        reportecomisiones.Id_Cte = (int)DR["Id_Cte"];
                        reportecomisiones.Nombre_Empleado = (string)DR["NombreCliente"];
                        reportecomisiones.CdiNombre = (string)DR["NombreCdi"];
                        reportecomisiones.Estatus = 1;
                        reportecomisiones.VtaCob = (double)DR["VtaCob"];
                        reportecomisiones.UP = (double)DR["UP"];
                        reportecomisiones.GST = (double)DR["GST"];
                        reportecomisiones.Amortizacion = (double)DR["Amortizacion"];
                        reportecomisiones.UB = (double)DR["UB"];
                        reportecomisiones.PPPA = (double)DR["PPPA"];
                        reportecomisiones.FR = (double)DR["FR"];
                        reportecomisiones.CB = (double)DR["CB"];
                        reportecomisiones.Año = rik.Anio;
                        reportecomisiones.Mes = rik.Mes;
                        
                        registrorik.Add(reportecomisiones);

                    }


                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    oDB.Dispose();
                }
            }

        }


        /// <summary>
        /// ReporteConcentradoFranquicias Detalle de Facturas  2
        /// </summary>
        /// <param name="rik"></param>
        /// <param name="Conexion"></param>
        /// <param name="registrorik"></param>
        public void ReporteConcentradoFranquiciasDetFactura(SistemaCompensacionGetXML rik, string Conexion, ref List<ReporteComisiones> registrorik)
        {


            using (dbAccess oDB = new dbAccess(Conexion, 800000))
            {
                try
                {

                    DataSet DS = oDB.spExecDataSet(
                        "spRepComisionesReporteFranquicias",
                        new SqlParameter("@Id_Emp", rik.Id_Emp),
                        new SqlParameter("@Anio", rik.Anio),
                        new SqlParameter("@Mes", rik.Mes),
                        new SqlParameter("@Id_TipoRepresentante", rik.Id_TipoRepresentante),
                        new SqlParameter("@Id_Cd", rik.Id_Cd),
                         new SqlParameter("@Id_Reporte", 2),
                        new SqlParameter("@Id_Sistema", rik.Id_Sistema)
                  
                    );


                    foreach (DataRow DR in DS.Tables[0].Rows)
                    {

                        ReporteComisiones reportecomisiones = new ReporteComisiones();

                        reportecomisiones.Id_Emp = rik.Id_Emp;
                        reportecomisiones.Id_Cd = (int)DR["Id_Cd"];
                        reportecomisiones.CdiNombre = (string)DR["NombreCdi"];
                        reportecomisiones.Imprimir = 1;
                        reportecomisiones.Id_TipoRepresentante = rik.Id_TipoRepresentante;
                        reportecomisiones.Id = (int)DR["Id_Rik"];
                        reportecomisiones.Id_Cte = (int)DR["Id_Cte"];
                        reportecomisiones.Nombre_Empleado = (string)DR["NombreCliente"];
                        reportecomisiones.Pag_referencia = (string)DR["Pag_Referencia"];
                        reportecomisiones.Id_Territorio = (int)DR["Id_Ter"];
                        reportecomisiones.FechaVencimiento = (DateTime)DR["Vencimiento"];
                        reportecomisiones.FechaPago = (DateTime)DR["FechaPago"];
                        reportecomisiones.Dias = (int)DR["Dias"];
                        reportecomisiones.Importe = (double)DR["Importe"];
                        reportecomisiones.UP = (double)DR["UP"];
                        reportecomisiones.Mult_Porc = (double)DR["Mult_Porc"];
                        reportecomisiones.AjCobranza = (double)DR["AjCobranza"];
                        reportecomisiones.Año = rik.Anio;
                        reportecomisiones.Mes = rik.Mes;


                        registrorik.Add(reportecomisiones);

                    }


                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    oDB.Dispose();
                }
            }

        }

        /// <summary>
        /// ReporteConcentradoFranquicias Detalle de Producto  3
        /// </summary>
        /// <param name="rik"></param>
        /// <param name="Conexion"></param>
        /// <param name="registrorik"></param>
        public void ReporteConcentradoFranquiciasDetProducto(SistemaCompensacionGetXML rik, string Conexion, ref List<ReporteComisiones> registrorik)
        {


            using (dbAccess oDB = new dbAccess(Conexion, 8000))
            {
                try
                {

                    DataSet DS = oDB.spExecDataSet(
                        "spRepComisionesReporteFranquicias",
                        new SqlParameter("@Id_Emp", rik.Id_Emp),
                        new SqlParameter("@Anio", rik.Anio),
                        new SqlParameter("@Mes", rik.Mes),
                        new SqlParameter("@Id_TipoRepresentante", rik.Id_TipoRepresentante),
                        new SqlParameter("@Id_Cd", rik.Id_Cd),
                         new SqlParameter("@Id_Reporte", 3),
                        new SqlParameter("@Id_Sistema", rik.Id_Sistema)
                  
                    );


                    foreach (DataRow DR in DS.Tables[0].Rows)
                    {

                        ReporteComisiones reportecomisiones = new ReporteComisiones();
                       
                        try 
                        {
                        reportecomisiones.Id_Emp = rik.Id_Emp;
                        reportecomisiones.Id_Cd = (int)DR["Id_CD"];
                        
                        //reportecomisiones.Id_TipoRepresentante = rik.Id_TipoRepresentante;
                        reportecomisiones.Id = (int)DR["Id_Rik"];
                             }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        try
                        {
                            reportecomisiones.Id_Cte = (int)DR["Id_Cte"];
                            reportecomisiones.Nombre_Empleado = (string)DR["NombreCliente"];
                            reportecomisiones.Pag_referencia = (string)DR["Pag_referencia"];
                            reportecomisiones.CdiNombre = (string)DR["NombreCdi"];
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        try 
                        {
                        //reportecomisiones.VtaCob = (double)DR["VtaCob"];
                         reportecomisiones.UP = (double)DR["UP"];
                            }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        try 
                        {
                        //reportecomisiones.GST = (double)DR["GST"];
                        reportecomisiones.Id_Territorio = (int)DR["Id_Ter"];
                            }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        try 
                        {
                        reportecomisiones.Id_TipoRepresentante = (int)DR["Id_Prd"];
                            }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        try 
                        {
                        //reportecomisiones.UB = (double)DR["UB"];
                        //reportecomisiones.PPPA = (double)DR["PPPA"];
                        //reportecomisiones.FR = (double)DR["FR"];
                        //reportecomisiones.CB = (double)DR["CB"];
                        reportecomisiones.Año = rik.Anio;
                        reportecomisiones.Mes = rik.Mes;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        try 
                        {
                        registrorik.Add(reportecomisiones);
                            }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                      
                    }


                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    oDB.Dispose();
                }
            }

        }

        /// Reporte de comisiones de Capital HUmano 
        /// 
 

        public void ReporteConcentradoComisiones(SistemaCompensacionGetXML rik, int idreporte ,string Conexion, ref List<ReporteComisiones> registrorik)
        {


            using (dbAccess oDB = new dbAccess(Conexion, 800000))
            {
                try
                {

                    DataSet DS = oDB.spExecDataSet(
                        "spRepComisiones",
                        new SqlParameter("@Id_Emp", rik.Id_Emp),
                        new SqlParameter("@Id_Cd", rik.Id_Cd),
                        new SqlParameter("@Anio", rik.Anio),
                        new SqlParameter("@Mes", rik.Mes),
                        new SqlParameter("@Id_Rik", rik.Id_Rik),
                        new SqlParameter("@Reporte", idreporte)

                    );

                    if (idreporte == 4)
                    {
                        foreach (DataRow DR in DS.Tables[0].Rows)
                        {

                            ReporteComisiones reportecomisiones = new ReporteComisiones();

                            reportecomisiones.Id_Emp = rik.Id_Emp;
                            reportecomisiones.Id_Cd = (int)DR["Id_cd"];
                            reportecomisiones.Imprimir = 1;
                            reportecomisiones.Id_TipoRepresentante = rik.Id_TipoRepresentante;
                            reportecomisiones.Id = (int)DR["Id_Rik"];
                            reportecomisiones.Nombre_Empleado = (string)DR["Nombre_empleado"];
                            reportecomisiones.CdiNombre = (string)DR["CdiNombre"];
                            reportecomisiones.Estatus = 1;
                            reportecomisiones.VtaCob = (double)DR["VtaCob"];
                            reportecomisiones.UP = (double)DR["UP"];
                            reportecomisiones.UB = (double)DR["UB"];
                            reportecomisiones.CP = (double)DR["CP"];
                            reportecomisiones.MVI = (double)DR["MVI"];
                            reportecomisiones.ComBaseAjustada = (double)DR["ComBaseAjustada"];
                            reportecomisiones.GtoAdmin = (double)DR["GtoAdmin"];
                            reportecomisiones.ComisionNeta = (double)DR["ComisionNeta"];
                            //confsistcompensacionxml.Parametros = DR["Parametros"] == System.DBNull.Value ? null : (string)(DR["Parametros"]);
                            //confsistcompensacionxml.Clientes = DR["Clientes"] == System.DBNull.Value ? null : (string)(DR["Clientes"]);
                            //confsistcompensacionxml.Conceptos = DR["Conceptos"] == System.DBNull.Value ? null : (string)(DR["Conceptos"]);

                            registrorik.Add(reportecomisiones);

                        }
                    }
                    //Si rpeorte = 0   encabezado
                    if (idreporte == 0)
                    {

                        foreach (DataRow DR in DS.Tables[0].Rows)
                        {

                            ReporteComisiones reportecomisiones = new ReporteComisiones();

                            reportecomisiones.Id_Emp = rik.Id_Emp;
                            reportecomisiones.Id_Cd = (int)DR["CDI"];
                            reportecomisiones.Imprimir = 1;
                            reportecomisiones.Id_TipoRepresentante = rik.Id_TipoRepresentante;
                            reportecomisiones.Id = (int)DR["Rik"];
                            reportecomisiones.Nombre_Empleado = (string)DR["Nombre_Rik"];
                            reportecomisiones.CdiNombre = (string)DR["NombreCD"];
                            reportecomisiones.Estatus = 1;
                            reportecomisiones.Amortizacion = (double)DR["Amortizacion"];
                            reportecomisiones.TecSer = (double)DR["TecSer"];
                            reportecomisiones.UTrem = (int)DR["UTrem"];
                            reportecomisiones.ComisionNeta = (double)DR["ComisionNeta"];
                            reportecomisiones.MVI = (double)DR["MVI"];
                            reportecomisiones.Año = rik.Anio;
                            reportecomisiones.Mes = rik.Mes;

                            registrorik.Add(reportecomisiones);

                        }
                    }

                    //Si rpeorte = 1  Detalle de Cliente  1
                    if (idreporte == 1)
                    {
                        foreach (DataRow DR in DS.Tables[0].Rows)
                        {

                            ReporteComisiones reportecomisiones = new ReporteComisiones();

                            reportecomisiones.Id_Emp = rik.Id_Emp;
                            reportecomisiones.Id_Cd = (int)DR["Id_Cd"];
                            reportecomisiones.Imprimir = 1;
                            reportecomisiones.Id_TipoRepresentante = rik.Id_TipoRepresentante;
                            reportecomisiones.Id = (int)DR["Id_Rik"];
                            reportecomisiones.Id_Cte = (int)DR["Id_Cte"];
                            reportecomisiones.Nombre_Empleado = (string)DR["NombreCliente"];
                            reportecomisiones.CdiNombre = (string)DR["NombreCdi"];
                            reportecomisiones.Estatus = 1;
                            reportecomisiones.VtaCob = (double)DR["VtaCob"];
                            reportecomisiones.UP = (double)DR["UP"];
                            reportecomisiones.GST = (double)DR["GST"];
                            reportecomisiones.Amortizacion = (double)DR["Amortizacion"];
                            reportecomisiones.UB = (double)DR["UB"];
                            reportecomisiones.PPPA = (double)DR["PPPA"];
                            reportecomisiones.FR = (double)DR["FR"];
                            reportecomisiones.CB = (double)DR["CB"];
                            reportecomisiones.Año = rik.Anio;
                            reportecomisiones.Mes = rik.Mes;

                            registrorik.Add(reportecomisiones);

                        }
                    }

                    //Si rpeorte = 2  Detalle de Facturas  2
                    if (idreporte == 2)
                    {
                        foreach (DataRow DR in DS.Tables[0].Rows)
                        {

                            ReporteComisiones reportecomisiones = new ReporteComisiones();

                            reportecomisiones.Id_Emp = rik.Id_Emp;
                            reportecomisiones.Id_Cd = (int)DR["Id_Cd"];
                            reportecomisiones.CdiNombre = (string)DR["NombreCdi"];
                            reportecomisiones.Imprimir = 1;
                            reportecomisiones.Id_TipoRepresentante = rik.Id_TipoRepresentante;
                            reportecomisiones.Id = (int)DR["Id_Rik"];
                            reportecomisiones.Id_Cte = (int)DR["Id_Cte"];
                            reportecomisiones.Nombre_Empleado = (string)DR["NombreCliente"];
                            reportecomisiones.Pag_referencia = (string)DR["Pag_Referencia"];
                            reportecomisiones.Id_Territorio = (int)DR["Id_Ter"];
                            reportecomisiones.FechaVencimiento = (DateTime)DR["Vencimiento"];
                            reportecomisiones.FechaPago = (DateTime)DR["FechaPago"];
                            reportecomisiones.Dias = (int)DR["Dias"];
                            reportecomisiones.Importe = (double)DR["Importe"];
                            reportecomisiones.UP = (double)DR["UP"];
                            reportecomisiones.Mult_Porc = (double)DR["Mult_Porc"];
                            reportecomisiones.AjCobranza = (double)DR["AjCobranza"];
                            reportecomisiones.Año = rik.Anio;
                            reportecomisiones.Mes = rik.Mes;


                            registrorik.Add(reportecomisiones);

                        }
                    }

                    //Si rpeorte = 3   Detalle de Producto  3
                    if (idreporte == 3)
                    {
                        foreach (DataRow DR in DS.Tables[0].Rows)
                        {

                            ReporteComisiones reportecomisiones = new ReporteComisiones();

                            try
                            {
                                reportecomisiones.Id_Emp = rik.Id_Emp;
                                reportecomisiones.Id_Cd = (int)DR["Id_CD"];

                                //reportecomisiones.Id_TipoRepresentante = rik.Id_TipoRepresentante;
                                reportecomisiones.Id = (int)DR["Id_Rik"];
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }

                            try
                            {
                                reportecomisiones.Id_Cte = (int)DR["Id_Cte"];
                                reportecomisiones.Nombre_Empleado = (string)DR["NombreCliente"];
                                reportecomisiones.Pag_referencia = (string)DR["Pag_referencia"];
                                reportecomisiones.CdiNombre = (string)DR["NombreCdi"];
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                            try
                            {
                                //reportecomisiones.VtaCob = (double)DR["VtaCob"];
                                reportecomisiones.UP = (double)DR["UP"];
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                            try
                            {
                                //reportecomisiones.GST = (double)DR["GST"];
                                reportecomisiones.Id_Territorio = (int)DR["Id_Ter"];
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                            try
                            {
                                reportecomisiones.Id_TipoRepresentante = (int)DR["Id_Prd"];
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                            try
                            {
                                //reportecomisiones.UB = (double)DR["UB"];
                                //reportecomisiones.PPPA = (double)DR["PPPA"];
                                //reportecomisiones.FR = (double)DR["FR"];
                                //reportecomisiones.CB = (double)DR["CB"];
                                reportecomisiones.Año = rik.Anio;
                                reportecomisiones.Mes = rik.Mes;
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                            try
                            {
                                registrorik.Add(reportecomisiones);
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }

                        }
                    }




                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    oDB.Dispose();
                }
            }

        }

        
  

    }
}

