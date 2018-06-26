using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_CrmPromocion
    {
        public void ComboCds(Sesion sesion, ref List<CrmPromociones> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id1", "@Id2", "@Id3" };
                object[] Valores = { 2, sesion.Id_Emp, sesion.Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCentroDistribucion_Combo", ref dr, Parametros, Valores);

                CrmPromociones catPromociones;
                while (dr.Read())
                {
                    catPromociones = new CrmPromociones();
                    catPromociones.Id = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    catPromociones.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    list.Add(catPromociones);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ComboRik(Sesion sesion, int cds, ref List<CrmPromociones> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id1", "@Id2", "@Id3", "@Id4" };
                object[] Valores = { 1, sesion.Id_Emp, cds, sesion.Id_U };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRik_Combo", ref dr, Parametros, Valores);

                CrmPromociones catPromociones;
                while (dr.Read())
                {
                    catPromociones = new CrmPromociones();
                    catPromociones.Id = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    catPromociones.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    list.Add(catPromociones);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ComboUen(Sesion sesion, ref List<CrmPromociones> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id1", "@Id2" };
                object[] Valores = { 1, sesion.Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatUen_Combo", ref dr, Parametros, Valores);
                //spCatCRMUen_Combo
                CrmPromociones catPromociones;
                while (dr.Read())
                {
                    catPromociones = new CrmPromociones();
                    catPromociones.Id = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    catPromociones.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    list.Add(catPromociones);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ComboSegmento(Sesion sesion, int uen, ref List<CrmPromociones> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id1", "@Id2", "@Id3" };
                object[] Valores = { 1, sesion.Id_Emp, uen };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatSegmentosUen_Combo", ref dr, Parametros, Valores);

                CrmPromociones catPromociones;
                while (dr.Read())
                {
                    catPromociones = new CrmPromociones();
                    catPromociones.Id = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    catPromociones.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    list.Add(catPromociones);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ComboArea(Sesion sesion, int segmento, ref List<CrmPromociones> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id1", "@Id2", "@Id3" };
                object[] Valores = { 1, sesion.Id_Emp, segmento };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAreaSegmento_Combo", ref dr, Parametros, Valores);

                CrmPromociones catPromociones;
                while (dr.Read())
                {
                    catPromociones = new CrmPromociones();
                    catPromociones.Id = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    catPromociones.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    list.Add(catPromociones);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaSolucion(Sesion sesion, int area, ref List<CrmPromociones> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Area" };
                object[] Valores = { sesion.Id_Emp, area };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCatSoluciones_Combo", ref dr, Parametros, Valores);

                CrmPromociones catPromociones;
                while (dr.Read())
                {
                    catPromociones = new CrmPromociones();
                    catPromociones.Id = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    catPromociones.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    List.Add(catPromociones);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaAplicacion(Sesion sesion, int solucion, ref List<CrmPromociones> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Sol" };
                object[] Valores = { sesion.Id_Emp, solucion };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCatAplicacion_Combo", ref dr, Parametros, Valores);

                CrmPromociones catPromociones;
                while (dr.Read())
                {
                    catPromociones = new CrmPromociones();
                    catPromociones.Id = (int)dr.GetValue(dr.GetOrdinal("Id"));
                    catPromociones.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    List.Add(catPromociones);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaCatPromocion(Sesion sesion, CrmPromociones promocion, ref List<CrmPromociones> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd",                                   
                                         "@Id_Ter",
                                         "@Id_Seg",
                                         "@Id_Uen",
                                         "@Id_Area",
                                         "@Id_Sol",
                                         "@Id_U",
                                         "@Id_Apl",
                                         "@Estatus",
                                         "@Clientes",
                                         "@Id_Rik"
                                        
                                      };
                object[] Valores = { sesion.Id_Emp, 
                                     promocion.Cds,
                                     promocion.Territorio == - 1 ? (int?)null : promocion.Territorio,
                                     promocion.Segmento== - 1 ? (int?)null : promocion.Segmento,
                                     promocion.Uen== - 1 ? (int?)null : promocion.Uen,
                                     promocion.Area== - 1 ? (int?)null : promocion.Area,
                                     promocion.Solucion== - 1 ? (int?)null : promocion.Solucion,
                                     sesion.Id_U,
                                     promocion.Aplicacion== - 1 ? (int?)null : promocion.Aplicacion,
                                     promocion.Estatus,
                                     promocion.Cliente == 0 ? (int?)null: promocion.Cliente,
                                     promocion.Id_Rik == "-1" ? (object)null : promocion.Id_Rik, 
                                     
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCrmPromocion_Consulta", ref dr, Parametros, Valores);
                int Avances;
                CrmPromociones catPromociones;
                while (dr.Read())
                {
                    Avances = 0;
                    catPromociones = new CrmPromociones();
                    catPromociones.Ids = (int)dr.GetValue(dr.GetOrdinal("Id_Op"));
                    catPromociones.Id = (int)dr.GetValue(dr.GetOrdinal("Id_Op"));
                    catPromociones.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    catPromociones.Cds = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    catPromociones.Representante = (int)dr.GetValue(dr.GetOrdinal("Id_Rik"));
                    catPromociones.NombreCte = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    catPromociones.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    catPromociones.Segmento = (int)dr.GetValue(dr.GetOrdinal("Id_Seg"));
                    catPromociones.Cli_VPObservado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cli_VPObservado"))) ? 0.00 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cli_VPObservado")));
                    catPromociones.Descripcion = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    catPromociones.Analisis = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Analisis"))) ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Analisis"))).ToString("dd/MM/yyyy");
                    catPromociones.Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Presentacion"))) ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Presentacion"))).ToString("dd/MM/yyyy");
                    catPromociones.Negociacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Negociacion"))) ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Negociacion"))).ToString("dd/MM/yyyy");
                    catPromociones.Cierre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cierre"))) ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Cierre"))).ToString("dd/MM/yyyy");
                    catPromociones.Cancelacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelacion"))) ? " " : (string)dr.GetValue(dr.GetOrdinal("Cancelacion"));
                    catPromociones.FechaCancelacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FechaCancelacion"))) ? " " : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaCancelacion"))).ToString("dd/MM/yyyy");
                    catPromociones.Avances = (int)dr.GetValue(dr.GetOrdinal("Avances"));
                    catPromociones.Estatus = (int)dr.GetValue(dr.GetOrdinal("Estatus"));

                    Funciones funcion = new Funciones();
                    int mes_Actual = funcion.GetLocalDateTime(sesion.Minutos).Month;
                    int año_Actual = funcion.GetLocalDateTime(sesion.Minutos).Year;

                    //Analisis
                    if (catPromociones.Analisis != "")
                    {
                        if (((DateTime)(dr.GetValue(dr.GetOrdinal("Analisis")))).Month == mes_Actual && catPromociones.Avances != 5 && ((DateTime)(dr.GetValue(dr.GetOrdinal("Analisis")))).Year == año_Actual)
                        {
                            catPromociones.MesModificacion = "1";
                            Avances++;
                        }
                    }
                    //Presentacion
                    if (catPromociones.Presentacion != "")
                    {
                        if (((DateTime)(dr.GetValue(dr.GetOrdinal("Presentacion")))).Month == mes_Actual && catPromociones.Avances != 5 && ((DateTime)(dr.GetValue(dr.GetOrdinal("Presentacion")))).Year == año_Actual)
                        {
                            catPromociones.MesModificacion = "1";
                            Avances++;
                        }
                    }
                    //Negociacion
                    if (catPromociones.Negociacion != "")
                    {
                        if (((DateTime)(dr.GetValue(dr.GetOrdinal("Negociacion")))).Month == mes_Actual && catPromociones.Avances != 5 && ((DateTime)(dr.GetValue(dr.GetOrdinal("Negociacion")))).Year == año_Actual)
                        {
                            catPromociones.MesModificacion = "1";
                            Avances++;
                        }
                    }
                    //Cierre
                    if (catPromociones.Cierre != "")
                    {
                        if (((DateTime)(dr.GetValue(dr.GetOrdinal("Cierre")))).Month == mes_Actual && catPromociones.Avances != 5 && ((DateTime)(dr.GetValue(dr.GetOrdinal("Cierre")))).Year == año_Actual)
                        {
                            catPromociones.MesModificacion = "1";
                            Avances++;
                        }
                    }                  
                    catPromociones.MesModificacion = Avances == 0 ? "--" : Avances.ToString();

                    catPromociones.VentaMensual = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("VentaMensual"))) ? 0.00 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VentaMensual")));
                    List.Add(catPromociones);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CancelarPromocion(Sesion sesion, int cd, int promocion, ref int validador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Op" };
                object[] Valores = { sesion.Id_Emp, cd, promocion };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCrmPromocion_Cancelar", ref validador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaCatClientes(Sesion sesion, int Id_Ter, int Id_UEN, int Id_Rik, int id_Seg, int idCliente, string nombreCliente, ref List<CrmPromociones> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = {  "@Id_Emp", // saltillo - UEN, Segmento y territorio
                                         "@Id_Cd",  // mty - territorio y representante                          
                                         "@Cte_Nombre",
                                         "@Id_Ter",
                                         "@Id_UEN",//solo para saltillo
                                         "@Id_Seg",//solo para saltillo
                                         "@Id_Rik"
                                      };
                object[] Valores = {     sesion.Id_Emp,  
                                         sesion.Id_Cd_Ver,
                                         nombreCliente,
                                         Id_Ter,
                                         Id_UEN,//solo para saltillo
                                         id_Seg,//solo para saltillo
                                         Id_Rik
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCrmPromocion_ConsultaClientes", ref dr, Parametros, Valores);

                CrmPromociones catPromociones;
                while (dr.Read())
                {                  
                    catPromociones = new CrmPromociones();
                    catPromociones.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    catPromociones.NombreCte = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    catPromociones.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    catPromociones.Ter_Nombre = (string)dr.GetValue(dr.GetOrdinal("Ter_Nombre"));
                    catPromociones.Id_Uen = (int)dr.GetValue(dr.GetOrdinal("Id_Uen"));
                    catPromociones.Uen_Descrip = (string)dr.GetValue(dr.GetOrdinal("Uen_Descripcion"));
                    List.Add(catPromociones);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertarOportunidad(Sesion sesion, CRMRegistroProyectos promocion, ref int validador, string aplicaciones)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                DateTime date = new DateTime();
                date = Convert.ToDateTime("01/01/1980");
                string[] Parametros = {  
                                         "@Id_Emp", 
                                         "@Id_Cd",
                                         "@Id_Usu",
                                         "@Id_Op",       
                                         "@Id_UEN",          
                                         "@Id_Seg",           
                                         "@Id_Ter",            
                                         "@Id_Area",            
                                         "@Id_Sol", 
                                         "@List_Apl",
                                         "@Id_Apl",            
                                         "@Clientes",    
                                         "@Productos",
                                         "@VentaNoRepetitiva",
                                         "@Comentarios",
                                         "@Analisis",
                                         "@Presentacion",
                                         "@Negociacion",
                                         "@Cierre",                                         
                                         "@FechaCotizacion",
                                         "@VentaMensual",                                         
                                         "@MontoProyecto",  
                                         "@Estatus",
                                         "@Id_Op_Anterior",
                                         "@Cancelacion",
                                         "@Id_Causa",
                                         "@Competidor",
                                         "@ComentariosCancel",
                                         "@Id_Cam"
                                      };
                object[] Valores = {   
                                        sesion.Id_Emp,  
                                        sesion.Id_Cd_Ver,
                                        sesion.Id_Rik,
                                        promocion.IdMax,
                                        promocion.Uen,
                                        promocion.Segmento,
                                        promocion.Territorio,
                                        promocion.Area,
                                        promocion.Solucion,
                                        aplicaciones,
                                        promocion.Aplicacion,
                                        promocion.Cliente,
                                        promocion.Productos,
                                        promocion.VentaNoRepetitiva,
                                        promocion.Comentarios,
                                        (promocion.Analisis > date) ? promocion.Analisis : date,
                                        (promocion.Presentacion> date) ? promocion.Presentacion : date,
                                        (promocion.Negociacion> date) ? promocion.Negociacion : date,
                                        (promocion.Cierre> date) ? promocion.Cierre : date,
                                        promocion.FechaCotizacion,
                                        promocion.VentaPromedio,
                                        promocion.ValorPotencialO,
                                        promocion.Estatus,
                                        promocion.Id_Op,
                                        (promocion.Cancelacion > date) ? promocion.Cancelacion : date,
                                        promocion.Id_Causa,
                                        promocion.Competidor,
                                        promocion.ComentariosCancel,
                                        promocion.Id_Cam
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCrmPromocion_Insertar", ref validador, Parametros, Valores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EstructuraSegmento(ref DataSet dsEstructuraSegmento, CrmOportunidades registros, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Id_Seg", 
                                          //"@Id_Cte", 
                                          "@Id_Op", 
                                          "@Id_Sol" 
                                      };
                object[] Valores = { 
                                       registros.Id_Emp, 
                                       registros.Id_Cd, 
                                       registros.Id_Seg, 
                                       //registros.Id_Cte, 
                                       registros.Id_Op, 
                                       registros.Id_Sol 
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMEstructuraSegmentoProyecto", ref dr, Parametros, Valores);

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
                            dataRow[i] = dr.GetValue(i);
                        dataTable.Rows.Add(dataRow);
                    }
                    if (!dr.NextResult())
                        break;
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizaDimension(CrmOportunidades registros, string Cnx, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Cnx);
                string[] Parametros = {  
                                         "@Id_Emp", 
                                         "@Id_Cd",  
                                         "@Id_Estruc",
                                         "@Id_Ter",
                                         "@Id_Cte",
                                         "@Id_Area",
                                         "@Id_Seg", 
                                         "@Id_UEN",
                                         "@Id_Apl", 
                                         "@Id_Sol",            
                                         "@Porcentaje",
                                         "@Estatus",
                                         "@Id_Op"
                                      };
                object[] Valores = {   
                                        registros.Id_Emp,  
                                        registros.Id_Cd,
                                        registros.Id_Estruc,
                                        registros.Id_Ter,
                                        registros.Id_Cte,
                                        registros.ID_Area,
                                        registros.Id_Seg,
                                        registros.Id_Uen, 
                                        registros.Id_Apl,
                                        registros.Id_Sol,
                                        registros.Porcentaje,
                                        registros.Activo,
                                        registros.Id_Op
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMEstructuraSegmentoProyecto_Modificar", ref verificador, Parametros, Valores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
