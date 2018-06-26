using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatColaborador
    {

        /// <summary>
        /// Consulta un Colaborador  según la orden de compra donde esta registrado.
        /// </summary>
        public void ConsultaEmpleadoNomina(ref Colaborador colaborador, string Conexion, int id_Emp, int id_Cd, int id_Cd_Ver, int id_Empleado, bool catalogo)
        {
            try
            {

                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cd_Ver", "@Id_Empleado" };
                object[] Valores = { id_Emp, 
                                       id_Cd <= 0 ? (object)null : id_Cd,
                                       id_Cd_Ver, 
                                       id_Empleado
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatColaborador_ConsultaCatalogo", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    colaborador = new Colaborador();

                    colaborador.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    colaborador.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    colaborador.Id_Empleado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Empleado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Empleado"));
                    colaborador.Num_Nomina = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Num_Nomina"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Num_Nomina"));
                    colaborador.Nombre_Empleado = dr.GetString(dr.GetOrdinal("Nombre_Empleado"));
                    colaborador.Emp_FechaAlta = dr.IsDBNull(dr.GetOrdinal("Emp_FechaAlta")) ? Convert.ToDateTime("01/01/2001") : (DateTime)dr.GetValue(dr.GetOrdinal("Emp_FechaAlta"));
                    colaborador.Emp_Puesto = dr.GetString(dr.GetOrdinal("Emp_Puesto"));
                    colaborador.Emp_Correo = dr.GetString(dr.GetOrdinal("Emp_Correo"));
                    colaborador.Emp_Estatus = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Emp_Estatus"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Emp_Estatus"));
                    
                    colaborador.Id_Colaborador = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Colaborador"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Colaborador"));
                    //colaborador. = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("id_Empleado_Colaborador"))) ? 0 : dr.GetInt32(dr.GetOrdinal("id_Empleado_Colaborador"));
                    colaborador.FechaInicioOperacion = dr.IsDBNull(dr.GetOrdinal("FechaInicioOperacion")) ? Convert.ToDateTime("01/01/2001") : (DateTime)dr.GetValue(dr.GetOrdinal("FechaInicioOperacion"));
                    colaborador.Id_Region = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Region"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Region"));


                    colaborador.Id_Sucursal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Sucursal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Sucursal"));
                    colaborador.Id_TipoUsuario = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_TipoUsuario"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_TipoUsuario"));
                    //colaborador.Id_UEN = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_UEN"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_UEN"));
                    colaborador.Id_UEN = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_UEN"))) ? "" : dr.GetString(dr.GetOrdinal("Id_UEN"));
                    colaborador.Sueldo_Variable = dr.IsDBNull(dr.GetOrdinal("Sueldo_Variable")) ? (Double?)null : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Sueldo_Variable")));
                    colaborador.Porcentaje_Contribucion = dr.IsDBNull(dr.GetOrdinal("Porcentaje_Contribucion")) ? (Double?)null : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Porcentaje_Contribucion")));
                    colaborador.Emp_Sucursal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Emp_Sucursal"))) ? "" : dr.GetString(dr.GetOrdinal("Emp_Sucursal"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaEmpleadoPorNumeroNomina(ref Colaborador colaborador, string Conexion, int id_Emp, int id_NumNomina, bool catalogo)
        {
            try
            {

                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Num_Nomina" };
                object[] Valores = { id_Emp, 
                                       id_NumNomina
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatColaboradorNomina_ConsultaCatalogo", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    colaborador = new Colaborador();

                    colaborador.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    colaborador.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    colaborador.Id_Empleado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Empleado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Empleado"));
                    colaborador.Num_Nomina = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Num_Nomina"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Num_Nomina"));
                    colaborador.Nombre_Empleado = dr.GetString(dr.GetOrdinal("Nombre_Empleado"));
                    colaborador.Emp_FechaAlta = dr.IsDBNull(dr.GetOrdinal("Emp_FechaAlta")) ? Convert.ToDateTime("01/01/2001") : (DateTime)dr.GetValue(dr.GetOrdinal("Emp_FechaAlta"));
                    colaborador.Emp_Puesto = dr.GetString(dr.GetOrdinal("Emp_Puesto"));
                    colaborador.Emp_Correo = dr.GetString(dr.GetOrdinal("Emp_Correo"));
                    colaborador.Emp_Estatus = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Emp_Estatus"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Emp_Estatus"));

                    colaborador.Id_Colaborador = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Colaborador"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Colaborador"));
                    //colaborador. = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("id_Empleado_Colaborador"))) ? 0 : dr.GetInt32(dr.GetOrdinal("id_Empleado_Colaborador"));
                    colaborador.FechaInicioOperacion = dr.IsDBNull(dr.GetOrdinal("FechaInicioOperacion")) ? Convert.ToDateTime("01/01/2001") : (DateTime)dr.GetValue(dr.GetOrdinal("FechaInicioOperacion"));
                    colaborador.Id_Region = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Region"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Region"));


                    colaborador.Id_Sucursal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Sucursal"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Sucursal"));
                    colaborador.Id_TipoUsuario = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_TipoUsuario"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_TipoUsuario"));
                    //colaborador.Id_UEN = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_UEN"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_UEN"));
                    colaborador.Id_UEN = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_UEN"))) ? "" : dr.GetString(dr.GetOrdinal("Id_UEN"));
                    colaborador.Sueldo_Variable = dr.IsDBNull(dr.GetOrdinal("Sueldo_Variable")) ? (Double?)null : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Sueldo_Variable")));
                    colaborador.Porcentaje_Contribucion = dr.IsDBNull(dr.GetOrdinal("Porcentaje_Contribucion")) ? (Double?)null : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Porcentaje_Contribucion")));
                    colaborador.Emp_Sucursal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Emp_Sucursal"))) ? "" : dr.GetString(dr.GetOrdinal("Emp_Sucursal"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaListaObjetivos(ColaboradorObjetivo objetivosColaborador, string Conexion, ref List<ColaboradorObjetivo> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Colaborador" };
                object[] Valores = { objetivosColaborador.Id_Emp, objetivosColaborador.Id_Cd, objetivosColaborador.Id_Colaborador };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spColaboradorObjetivos_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    objetivosColaborador = new ColaboradorObjetivo();
                    objetivosColaborador.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    objetivosColaborador.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    objetivosColaborador.Id_Colaborador = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Colaborador")));
                    objetivosColaborador.Id_ColaboradorObjetivo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_ColaboradorObjetivo")));
                    objetivosColaborador.Estatus = true;
                    objetivosColaborador.Anio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Anio")));
                    objetivosColaborador.Mes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Mes")));    // Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Mes"))) ? string.Empty : dr.GetString(dr.GetOrdinal("Mes"));
                    objetivosColaborador.Objetivo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Objetivo"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Objetivo")));

                    List.Add(objetivosColaborador);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void InsertarColaborador(Colaborador colaborador, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

               
                string[] Parametros = { 
	                                  "@Id_Emp" 
                                        ,"@Id_Cd"
                                        ,"@Id_Empleado"
                                        ,"@Num_Nomina"
                                        ,"@Nombre_Empleado"
                                        ,"@Emp_FechaAlta"
                                        ,"@Emp_Puesto"
                                        ,"@Emp_Correo"
                                        ,"@Emp_Estatus"
                                        ,"@Id_Colaborador"
                                        ,"@FechaInicioOperacion"
                                        ,"@Id_Region"
                                        ,"@Id_Sucursal"
                                        ,"@Id_TipoUsuario"
                                        ,"@Id_UEN"
                                        ,"@Sueldo_Variable"
                                        ,"@Porcentaje_Contribucion"
                                        ,"@Colaborador_Estatus"
                                      };
                object[] Valores = { 
                                         colaborador.Id_Emp
                                        ,colaborador.Id_Cd 
                                        ,colaborador.Id_Empleado
                                        ,colaborador.Num_Nomina
                                        ,colaborador.Nombre_Empleado
                                        ,colaborador.Emp_FechaAlta
                                        ,colaborador.Emp_Puesto
                                        ,colaborador.Emp_Correo
                                        ,colaborador.Emp_Estatus
                                        ,colaborador.FechaInicioOperacion
                                        ,colaborador.Id_Region  
                                        ,colaborador.Id_Sucursal
                                        ,colaborador.Id_TipoUsuario
                                        ,colaborador.Id_UEN
                                        ,colaborador.Sueldo_Variable
                                        ,colaborador.Porcentaje_Contribucion
                                        ,colaborador.Colaborador_Estatus
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatColaborador_Insertar", ref verificador, Parametros, Valores);

                // ------------------------------------------------------------
                // Eliminar datos de precio de Objetivos  del Colaborador actual
                // ------------------------------------------------------------
                string[] ParametrosDelete = { 
                                        "@Id_Emp", 
                                        "@Id_Cd", 
                                        "@Id_Prd", 
                                      };
                object[] ValoresDelete = { 
                                        colaborador.Id_Emp
                                        ,colaborador.Id_Cd
                                        ,colaborador.Id_Empleado 
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCatColaborador_Eliminar", ref verificador, ParametrosDelete, ValoresDelete);


                // ------------------------------------------------------------
                // Insertar datos de objetivos de Colaboradores
                // ------------------------------------------------------------
                foreach (ColaboradorObjetivo colaboradorobjetivos in colaborador.ListaColaboradorObjetivos)
                {
                    string[] ParametrosInsert = { 
                                        "@Id_ColaboradorObjetivo", 
                                        "@Id_Colaborador",
                                        "@Año",
                                        "@Mes",
                                        "@Objetivo"
                                      };
                    object[] ValoresInsert = { 
                                         colaboradorobjetivos.Id_Colaborador
                                        ,colaboradorobjetivos.Anio
                                        ,colaboradorobjetivos.Mes
                                        ,colaboradorobjetivos.Objetivo
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spColaboradorObjetivos_Insertar", ref verificador, ParametrosInsert, ValoresInsert);
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

        public void ModificarColaborador(Colaborador colaborador, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                

                string[] Parametros = { 
                                        "@Id_Emp" 
                                        ,"@Id_Cd"
                                        ,"@Id_Empleado"
                                        ,"@Num_Nomina"
                                        ,"@Nombre_Empleado"
                                        //,"@Emp_FechaAlta"
                                        ,"@Emp_Puesto"
                                        ,"@Emp_Correo"
                                        ,"@Emp_Estatus"
                                        ,"@Id_Colaborador"
                                        ,"@FechaInicioOperacion"
                                        ,"@Id_Region"
                                        ,"@Id_Sucursal"
                                        ,"@Id_TipoUsuario"
                                        ,"@Id_UEN"
                                        ,"@Sueldo_Variable"
                                        ,"@Porcentaje_Contribucion"
                                        ,"@Colaborador_Estatus"
                                     
                                      };
                object[] Valores = { 
                                        colaborador.Id_Emp
                                        ,colaborador.Id_Cd 
                                        ,colaborador.Id_Empleado
                                        ,colaborador.Num_Nomina
                                        ,colaborador.Nombre_Empleado
                                        //,colaborador.Emp_FechaAlta
                                        ,colaborador.Emp_Puesto
                                        ,colaborador.Emp_Correo
                                        ,colaborador.Emp_Estatus
                                        ,colaborador.Id_Colaborador
                                        ,colaborador.FechaInicioOperacion
                                        ,colaborador.Id_Region  
                                        ,colaborador.Id_Sucursal
                                        ,colaborador.Id_TipoUsuario
                                        ,colaborador.Id_UEN
                                        ,colaborador.Sueldo_Variable
                                        ,colaborador.Porcentaje_Contribucion
                                        ,colaborador.Colaborador_Estatus
                                       
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatColaborador_Modificar", ref verificador, Parametros, Valores);

                // ------------------------------------------------------------
                // Eliminar Objetivos del Colaborador 
                // ------------------------------------------------------------
                //string[] ParametrosDelete = { 
                //                        "@Id_Emp", 
                //                        "@Id_Cd", 
                //                        "@Id_Empleado", 
                //                      };
                //object[] ValoresDelete = { 
                //                        colaborador.Id_Emp
                //                        ,colaborador.Id_Cd
                //                        ,colaborador.Id_Empleado 
                //                   };
                //sqlcmd = CapaDatos.GenerarSqlCommand("spCatColaborador_Eliminar", ref verificador, ParametrosDelete, ValoresDelete);

                //foreach (ColaboradorObjetivo colaboradorobjetivos in colaborador.ListaColaboradorObjetivos)
                //{
                //    string[] ParametrosInsert = { 
                //                        "@Id_Colaborador",
                //                        "@Año",
                //                        "@Mes",
                //                        "@Objetivo"
                //                      };
                //    object[] ValoresInsert = { 
                //                         colaboradorobjetivos.Id_Colaborador
                //                        ,colaboradorobjetivos.Anio
                //                        ,colaboradorobjetivos.Mes
                //                        ,colaboradorobjetivos.Objetivo
                //                   };
                //    sqlcmd = CapaDatos.GenerarSqlCommand("spColaboradorObjetivos_Insertar", ref verificador, ParametrosInsert, ValoresInsert);
                //}


                // ------------------------------------------------------------
                // Eliminar Los Conceptos anteriores e inserta los nuevos 
                // ------------------------------------------------------------
                string[] ParametrosDeleteconceptos = { 
                                        "@Id_Emp", 
                                        "@Id_Cd", 
                                        "@Id_Empleado", 
                                      };
                object[] ValoresDeleteconceptos = { 
                                        colaborador.Id_Emp
                                        ,colaborador.Id_Cd
                                        ,colaborador.Id_Empleado 
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapConceptos_Monto_Eliminar", ref verificador, ParametrosDeleteconceptos, ValoresDeleteconceptos);

                foreach (ConceptosNomina conceptosNomina in colaborador.ListaConceptosNomina)
                {
                    string[] ParametrosInsert = { 
                                        "@Id_Compensacion_Monto",
                                        "@Id_Compensacion",
                                        "@Id_Empleado",
                                        "@Monto"
                                      };
                    object[] ValoresInsert = { 
                                         conceptosNomina.Id_Compensacion_Monto
                                        ,conceptosNomina.Id_Compensacion
                                        ,colaborador.Id_Empleado 
                                        ,conceptosNomina.Monto
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapConceptos_Monto_Insertar", ref verificador, ParametrosInsert, ValoresInsert);
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

        //public void ConsultaListaConceptosNomina(ConceptosNomina conceptosNomina, string Conexion, ref List<ConceptosNomina> List)
        //{
        //    try
        //    {
        //        SqlDataReader dr = null;
        //        CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

        //        string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Colaborador" };
        //        object[] Valores = { conceptosNomina.Id_Emp, conceptosNomina.Id_Cd, conceptosNomina.Id_Colaborador };

        //        SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spColaboradorConceptosNomina_Consulta", ref dr, Parametros, Valores);


        //        while (dr.Read())
        //        {
        //            conceptosNomina = new ConceptosNomina();
        //            conceptosNomina.Id_Emp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Emp"))) ? conceptosNomina.Id_Emp : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
        //            conceptosNomina.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? conceptosNomina.Id_Cd : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
        //            conceptosNomina.Id_Empleado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Empleado"))) ? conceptosNomina.Id_Empleado : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Empleado")));
        //            conceptosNomina.Id_Colaborador = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Colaborador"))) ? conceptosNomina.Id_Colaborador : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Colaborador")));
        //            conceptosNomina.Id_Compensacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Compensacion"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Compensacion")));
        //            conceptosNomina.Compensacion_Descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Compensacion_Descripcion")));
        //            conceptosNomina.Id_Compensacion_Monto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Compensacion_Monto"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Compensacion_Monto")));
        //            conceptosNomina.Monto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Monto"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Monto")));
        //            conceptosNomina.EsEditable = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("EsEditable"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EsEditable")));
                     
                   

        //            List.Add(conceptosNomina);
        //        }

        //        CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void ConsultaListaConceptosNomina(ConceptosNomina conceptosNomina, string Conexion, ref List<ConceptosNomina> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Colaborador" };
                object[] Valores = { conceptosNomina.Id_Emp, conceptosNomina.Id_Cd, conceptosNomina.Id_Colaborador };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spColaboradorConceptosNomina_Consulta2", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    conceptosNomina = new ConceptosNomina();
                    conceptosNomina.Id_Emp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Emp"))) ? conceptosNomina.Id_Emp : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    conceptosNomina.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? conceptosNomina.Id_Cd : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    conceptosNomina.Id_Empleado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Empleado"))) ? conceptosNomina.Id_Empleado : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Empleado")));
                    conceptosNomina.Id_Colaborador = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Colaborador"))) ? conceptosNomina.Id_Colaborador : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Colaborador")));
                    conceptosNomina.Id_Compensacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Compensacion"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Compensacion")));
                    conceptosNomina.Compensacion_Descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Compensacion_Descripcion")));
                    conceptosNomina.Id_Compensacion_Monto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Compensacion_Monto"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Compensacion_Monto")));
                    conceptosNomina.Monto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Monto"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Monto")));
                    conceptosNomina.EsEditable = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("EsEditable"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EsEditable")));


                    conceptosNomina.Id_Compensacion1 = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Compensacion1"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Compensacion1")));
                    conceptosNomina.Monto1 = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Monto1"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Monto1")));
                    conceptosNomina.Id_Compensacion2 = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Compensacion2"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Compensacion2")));
                    conceptosNomina.Monto2 = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Monto2"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Monto2")));
                    conceptosNomina.Id_Compensacion3 = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Compensacion3"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Compensacion3")));
                    conceptosNomina.Monto3 = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Monto3"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Monto3")));
                    conceptosNomina.Id_Compensacion4 = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Compensacion4"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Compensacion4")));
                    conceptosNomina.Monto4 = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Monto4"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Monto4")));
                    //conceptosNomina.Id_Compensacion5 = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Compensacion5"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Compensacion5")));
                    //conceptosNomina.Monto5 = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Monto5"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Monto5")));
                    //conceptosNomina.Id_Compensacion6 = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Compensacion6"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Compensacion6")));
                    //conceptosNomina.Monto6 = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Monto6"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Monto6")));
                    //conceptosNomina.Id_Compensacion7 = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Compensacion7"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Compensacion7")));
                    //conceptosNomina.Monto7 = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Monto7"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Monto7")));



                    List.Add(conceptosNomina);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //inserto los montos de los conceptos por empleado
        public void GuardarCargaMasivaConceptosMonto(Colaborador colaborador, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();


                SqlCommand sqlcmd = new SqlCommand();

              
                // ------------------------------------------------------------
                // Eliminar Los Conceptos anteriores e inserta los nuevos 
                // ------------------------------------------------------------
                string[] ParametrosDeleteconceptos = { 
                                        "@Id_Emp", 
                                        "@Id_Cd", 
                                        "@Id_Empleado", 
                                      };
                object[] ValoresDeleteconceptos = { 
                                        colaborador.Id_Emp
                                        ,colaborador.Id_Cd
                                        ,colaborador.Id_Empleado 
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapConceptos_Monto_Eliminar", ref verificador, ParametrosDeleteconceptos, ValoresDeleteconceptos);

                //Inserto los conceptos del empleado 
                foreach (ConceptosNomina conceptosNomina in colaborador.ListaConceptosNomina)
                {
                    string[] ParametrosInsert = { 
                                        "@Id_Compensacion_Monto",
                                        "@Id_Compensacion",
                                        "@Id_Empleado",
                                        "@Monto",
                                        "@Id_Emp"
                                      };
                    object[] ValoresInsert = { 
                                         conceptosNomina.Id_Compensacion_Monto
                                        ,conceptosNomina.Id_Compensacion
                                        ,colaborador.Id_Empleado 
                                        ,conceptosNomina.Monto
                                        ,colaborador.Id_Emp
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapConceptos_Monto_Insertar", ref verificador, ParametrosInsert, ValoresInsert);
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


        //inserto las exepciones para carga de comisiones 
        public void GuardarCargaMasivaExcepciones(ComisionesExcepcionesRentabilidad colaborador, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
               // CapaDatos.StartTrans();


                SqlCommand sqlcmd = new SqlCommand();
 
                
                    string[] ParametrosInsert = { 
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Rik",
                                        "@Id_Cte",
                                        "@EstatusRentabilidad",
                                        "@Rentabilidad",
                                        "@Id_Usuario"
                                      };
                    object[] ValoresInsert = { 
                                         colaborador.Id_Emp
                                        ,colaborador.Id_Cd
                                        ,colaborador.Id_Rik
                                        ,colaborador.Id_Cte 
                                        ,colaborador.Estatus_Rentabilidad
                                        ,colaborador.Rentabilidad
                                        ,colaborador.Id_Usuario
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatCalcularRentabilidadExcepcion_Insertar", ref verificador, ParametrosInsert, ValoresInsert);
                
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        //inserto las exepciones para carga de comisiones 
        public void EliminarCargaMasivaExcepciones( int id_Emp , string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
 
                SqlCommand sqlcmd = new SqlCommand();


                string[] ParametrosInsert = { 
                                        "@Id_Emp"
                                      };
                object[] ValoresInsert = { 
                                         id_Emp
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCatCalcularRentabilidadExcepcion_Eliminar", ref verificador, ParametrosInsert, ValoresInsert);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }


        
    }
}
