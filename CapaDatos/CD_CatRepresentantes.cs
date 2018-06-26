using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_CatRepresentantes
    {
        public void ConsultarRepresentantes(Representantes representante, string Conexion, ref List<Representantes> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { representante.Id_Emp, representante.Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRik_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    representante = new Representantes();
                    representante.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    representante.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    representante.Id_Rik = (int)dr.GetValue(dr.GetOrdinal("Id_Rik"));
                    representante.Nombre = (string)dr.GetValue(dr.GetOrdinal("Rik_Nombre"));
                    representante.Calle = (string)dr.GetValue(dr.GetOrdinal("Rik_Calle"));
                    try
                    {
                        representante.Numero = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rik_Numero")));
                    }
                    catch
                    { }
                    representante.Colonia = Convert.ToString(dr.GetValue(dr.GetOrdinal("Rik_Colonia")));
                    representante.Telefono = dr.GetValue(dr.GetOrdinal("Rik_Tel")).ToString();
                    representante.Fecha_Alta = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rik_FecAlta"))) ? DateTime.MinValue : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Rik_FecAlta")));
                    representante.Contribucion = (double)dr.GetValue(dr.GetOrdinal("Rik_Contribucion"));
                    representante.Compensacion = (double)dr.GetValue(dr.GetOrdinal("Rik_Compesacion"));
                    representante.Pertenece = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Rik_Pertenece")));
                    //representante.Gte = (int)dr.GetValue(dr.GetOrdinal("Rik_Gte"));
                    representante.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Rik_Activo")));
                    if (Convert.ToBoolean(representante.Estatus))
                    {
                        representante.EstatusStr = "Activo";
                    }
                    else
                    {
                        representante.EstatusStr = "Inactivo";
                    }
                    List.Add(representante);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertarRepresentantes(Representantes representante, string Conexion, ref int verificador)
        {
            try
            {
                 
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp", 
		                                "@Id_Cd", 
		                                "@Id_Rik", 
		                                "@Rik_Nombre", 
		                                "@Rik_Calle", 
		                                "@Rik_Numero", 
		                                "@Rik_Colonia", 
		                                "@Rik_Tel", 
		                                "@Rik_FecAlta", 
		                                "@Rik_Contribucion", 
		                                "@Rik_Compesacion", 
		                                "@Rik_Pertenece",
                                        //"@Rik_Gte",                    
		                                "@Rik_Activo"
                                      };
                object[] Valores = { 
                                        representante.Id_Emp,
                                        representante.Id_Cd,
                                        representante.Id_Rik,
                                        representante.Nombre,
                                        representante.Calle,
                                        representante.Numero,
                                        representante.Colonia,
                                        representante.Telefono,
                                        representante.Fecha_Alta == DateTime.MinValue ? (object)null : representante.Fecha_Alta,
                                        representante.Contribucion,
                                        representante.Compensacion,
                                        representante.Pertenece,
                                        //representante.Gte,
                                        representante.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRik_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ModificarRepresentantes(Representantes representante, string Conexion, ref int verificador)
        {
            try
            {
              
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp", 
		                                "@Id_Cd", 
		                                "@Id_Rik", 
		                                "@Rik_Nombre", 
		                                "@Rik_Calle", 
		                                "@Rik_Numero", 
		                                "@Rik_Colonia", 
		                                "@Rik_Tel", 
		                                "@Rik_FecAlta", 
		                                "@Rik_Contribucion", 
		                                "@Rik_Compesacion", 
		                                "@Rik_Pertenece",
                                        //"@Rik_Gte",                    
		                                "@Rik_Activo"
                                      };
                object[] Valores = { 
                                        representante.Id_Emp,
                                        representante.Id_Cd,
                                        representante.Id_Rik,
                                        representante.Nombre,
                                        representante.Calle,
                                        representante.Numero,
                                        representante.Colonia,
                                        representante.Telefono,
                                        representante.Fecha_Alta == DateTime.MinValue ? (object)null : representante.Fecha_Alta,
                                        representante.Contribucion,
                                        representante.Compensacion,
                                        representante.Pertenece,
                                        //representante.Gte,
                                        representante.Estatus
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRik_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertarRepresentantesDet(Representantes representante, List<Comun> lc, string Conexion, ref int verificador)
        {
            if (lc.Count == 0) return;

            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {

                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Rik", 
                                        "@Id_Uen",
                                        "@Accion"
                                      };

                object[] Valores = null;
                SqlCommand sqlcmd = null;

                for (int x = 0; x < lc.Count; x++)
                {
                    Valores = new object[] { 
                                        representante.Id_Emp,
                                        representante.Id_Cd,
                                        representante.Id_Rik,
                                        lc[x].Id,
                                        x
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatRikUen_Insertar", ref verificador, Parametros, Valores);

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
        public void ConsultarRepresentantesDet(RikUen representante, string Conexion, ref List<RikUen> lc)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Rik" };
                object[] Valores = { representante.Id_Emp, representante.Id_Cd, representante.Id_Rik };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRikUen_Consulta", ref dr, Parametros, Valores);
                RikUen comun = default(RikUen);
                while (dr.Read())
                {
                    comun = new RikUen();
                    comun.Id_Rik= (int)dr.GetValue(dr.GetOrdinal("RIK"));
                    comun.Id_Uen = (int)dr.GetValue(dr.GetOrdinal("UEN"));
                    comun.DescripcionUEN = (string)dr.GetValue(dr.GetOrdinal("Descripcion"));
                    lc.Add(comun);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizaDatosRik(List<Representantes> List, ref int Verificador, string Conexion)
        {
            try
            {
                string[] Parametros = { 
                                          "@Id_Empl",
                                          "@Id_Rik",
                                          "@RE_FechaI",
                                          "@RE_FechaB"
                                      };
                foreach (Representantes r in List)
                {
                    CD_Datos cd_datos = new CD_Datos(Conexion);
                    object[] Valores = {
                                           r.Id_Empl ,
                                           r.Id_Rik,
                                           r.RE_FechaA ,
                                           r.RE_FechaB
                                       };

                    SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatRikEmpleado_Actualizar", ref Verificador, Parametros, Valores);

                    cd_datos.LimpiarSqlcommand(ref sqlcmd);

                }

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
