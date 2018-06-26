using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;
using System.Collections;

namespace CapaDatos
{
    public class CD_CatUsuario
    {
        public void ConsultaUsuarios(Usuario Usuario, string Conexion, ref List<Usuario> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Cd", "@Id_Emp" };
                object[] Valores = { Usuario.Id_Cd, Usuario.Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SPCatUsuario_Consulta", ref dr, Parametros, Valores);

                Usuario VarUsuario = default(Usuario);
                while (dr.Read())
                {
                    VarUsuario = new Usuario();
                    VarUsuario.Id_U = (int)dr.GetValue(dr.GetOrdinal("Id_U"));
                    VarUsuario.U_Nombre = (string)dr.GetValue(dr.GetOrdinal("U_Nombre"));
                    VarUsuario.U_FNac = dr.IsDBNull(dr.GetOrdinal("U_FNac")) ? Convert.ToDateTime("01/01/0001") : (DateTime)dr.GetValue(dr.GetOrdinal("U_FNac"));
                    VarUsuario.U_Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("U_Activo")));
                    VarUsuario.Id_TU = (int)dr.GetValue(dr.GetOrdinal("Id_TU"));
                    VarUsuario.U_VerTodo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("U_VerTodo")));
                    VarUsuario.U_MultiCentro = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("U_MultiOfi")));
                    VarUsuario.Tu_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Tu_Descripcion"));
                    VarUsuario.U_ActivoStr = (string)dr.GetValue(dr.GetOrdinal("Activo_String"));
                    VarUsuario.U_Correo = (string)dr.GetValue(dr.GetOrdinal("U_Correo"));
                    VarUsuario.Cu_User = (string)dr.GetValue(dr.GetOrdinal("Cu_User"));
                    VarUsuario.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    VarUsuario.Id_Id_U = (int)dr.GetValue(dr.GetOrdinal("Id_Id_U"));
                    VarUsuario.Ofi_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Cd_Nombre"));
                    VarUsuario.Id_Rik = (int)dr.GetValue(dr.GetOrdinal("Id_Rik"));
                    VarUsuario.U_SusCredito = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("U_SusCredito")));
                    VarUsuario.U_DiasVencimiento = dr.IsDBNull(dr.GetOrdinal("U_DiasVencimiento")) ? (Double?)null : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("U_DiasVencimiento")));
                    list.Add(VarUsuario);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertarUsuario(ref Usuario Usuario, string Conexion, ArrayList seleccionados, ref Int32 verificador, ref int Verificador2,List<RelacionGestor> list, string CobConexion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                verificador = 0;
                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@U_Nombre", 
                                          "@U_Correo", 
                                          "@U_FNac", 
                                          "@U_Activo", 
                                          "@Id_Id_U", 
                                          "@Id_Tu",  
                                          "@U_VerTodo", 
                                          "@U_MultiOfi", 
                                          "@Cu_User",
                                          "@Id_Rik",
							              "@U_SusCredito",
							              "@U_DiasVencimiento"
                                      };
                object[] Valores = {
                                       Usuario.Id_Emp, 
                                       Usuario.Id_Cd, 
                                       Usuario.U_Nombre, 
                                       Usuario.U_Correo,
                                       Usuario.U_FNac.ToString("MM/dd/yyyy") == "01/01/0001"? (string)null:Usuario.U_FNac.ToString("MM/dd/yyyy"), 
                                       Usuario.U_Activo, 
                                       Usuario.Id_Id_U, 
                                       Usuario.Id_TU,  
                                       Usuario.U_VerTodo, 
                                       Usuario.U_MultiCentro, 
                                       Usuario.Cu_User,
                                       Usuario.Id_Rik == -1 ? (int?)null: Usuario.Id_Rik,
							           Usuario.U_SusCredito,
							           Usuario.U_DiasVencimiento
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpCatUsuario_Insertar", ref verificador, Parametros, Valores);
                Usuario.Id_U = verificador;
                Verificador2 = verificador;
                bool limpiar = true;
                if (Usuario.Id_TU == 8)
                {
                    Parametros = new string[] { "@Id_Emp", "@Id_Uen", "@Id_U", "@Limpiar" };

                    foreach (object i in seleccionados)
                    {
                        Valores = new object[] { Usuario.Id_Emp, i, Usuario.Id_U, limpiar };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCatUen_Usuario", ref verificador, Parametros, Valores);
                        limpiar = false;
                    }
                }
                else if (Usuario.Id_TU == 7)
                {
                    limpiar = true;
                    Parametros = new string[] { "@Id_Emp", "@Id_Seg", "@Id_U", "@Limpiar" };
                    foreach (object i in seleccionados)
                    {
                        Valores = new object[] { Usuario.Id_Emp, i, Usuario.Id_U, limpiar };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCatSegmento_Usuario", ref verificador, Parametros, Valores);
                        limpiar = false;
                    }
                }
                else if (Usuario.Id_TU == 3)
                {
                    limpiar = true;
                    Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_U", "@Limpiar" };
                    foreach (object i in seleccionados)
                    {
                        Valores = new object[] { Usuario.Id_Emp, i, Usuario.Id_U, limpiar };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCatCd_Usuario", ref verificador, Parametros, Valores);
                        limpiar = false;
                    }
                }


                if (verificador != 1)
                {
                    Parametros = new string[] { "@Id_Emp", "@Id_U", "@Id_Cd", "@Id_Cd_Ver" };
                    foreach (object o in Usuario.Id_Centros)
                    {
                        Valores = new object[] { Usuario.Id_Emp, Usuario.Id_U, Usuario.Id_Cd, o };
                        sqlcmd = CapaDatos.GenerarSqlCommand("SpCatUsuarioCentro_Insertar", ref verificador, Parametros, Valores);
                    }
                }




                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                ActualizarRelacion(Usuario, CobConexion, list);

            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }

        }
        public void ModificarUsuario(Usuario Usuario, ref Int32 verificador, ArrayList seleccionados, string Conexion, List<RelacionGestor> list, string CobConexion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] Parametros = {
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Id_U", 
                                          "@U_Nombre", 
                                          "@U_Correo", 
                                          "@U_FNac", 
                                          "@U_Activo", 
                                          "@Id_Id_U", 
                                          "@Id_Tu", 
                                          "@U_VerTodo", 
                                          "@U_MultiOfi", 
                                          "@Cu_User",
                                          "@Id_Rik",
							              "@U_SusCredito",
							              "@U_DiasVencimiento"
                                      };
                object[] Valores = {
                                       Usuario.Id_Emp, 
                                       Usuario.Id_Cd, 
                                       Usuario.Id_U, 
                                       Usuario.U_Nombre, 
                                       Usuario.U_Correo, 
                                       Usuario.U_FNac.ToString("MM/dd/yyyy") == "01/01/0001"? (string)null: Usuario.U_FNac.ToString("dd/MM/yyyy")  , 
                                       Usuario.U_Activo, 
                                       Usuario.Id_Id_U, 
                                       Usuario.Id_TU, 
                                       Usuario.U_VerTodo, 
                                       Usuario.U_MultiCentro, 
                                       Usuario.Cu_User,
                                       Usuario.Id_Rik == -1 ? (int?)null : Usuario.Id_Rik,
							           Usuario.U_SusCredito,
							           Usuario.U_DiasVencimiento
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                if (verificador == 0)
                    sqlcmd = CapaDatos.GenerarSqlCommand("SpCatUsuario_Modificar", ref verificador, Parametros, Valores);
                else
                    sqlcmd = CapaDatos.GenerarSqlCommand("SpCatUsuario_ModificarTU", ref verificador, Parametros, Valores);

                bool limpiar = true;
                if (Usuario.Id_TU == 8)
                {
                    Parametros = new string[] { "@Id_Emp", "@Id_Uen", "@Id_U", "@Limpiar" };

                    foreach (object i in seleccionados)
                    {
                        Valores = new object[] { Usuario.Id_Emp, i, Usuario.Id_U, limpiar };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCatUen_Usuario", ref verificador, Parametros, Valores);
                        limpiar = false;
                    }
                }
                else if (Usuario.Id_TU == 7)
                {
                    limpiar = true;
                    Parametros = new string[] { "@Id_Emp", "@Id_Seg", "@Id_U", "@Limpiar" };
                    foreach (object i in seleccionados)
                    {
                        Valores = new object[] { Usuario.Id_Emp, i, Usuario.Id_U, limpiar };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCatSegmento_Usuario", ref verificador, Parametros, Valores);
                        limpiar = false;
                    }
                }
                else if (Usuario.Id_TU == 3)
                {
                    limpiar = true;
                    Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_U", "@Limpiar" };
                    foreach (object i in seleccionados)
                    {
                        Valores = new object[] { Usuario.Id_Emp, i, Usuario.Id_U, limpiar };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCatCd_Usuario", ref verificador, Parametros, Valores);
                        limpiar = false;
                    }
                }

                if (Usuario.U_MultiCentro)
                {
                    if (verificador != 1 && verificador != 2)
                    {

                        Parametros = new string[] { "@Id_Emp", "@Id_U" };
                        Valores = new object[] { Usuario.Id_Emp, Usuario.Id_U };
                        sqlcmd = CapaDatos.GenerarSqlCommand("SpCatUsuarioCentro_Eliminar", ref verificador, Parametros, Valores);


                        Parametros = new string[] { "@Id_Emp", "@Id_U", "@Id_Cd", "@Id_Cd_Ver" };
                        foreach (object o in Usuario.Id_Centros)
                        {

                            Valores = new object[] { Usuario.Id_Emp, Usuario.Id_U, Usuario.Id_Cd, o };
                            sqlcmd = CapaDatos.GenerarSqlCommand("SpCatUsuarioCentro_Insertar", ref verificador, Parametros, Valores);

                        }
                    }
                }
                CapaDatos.CommitTrans();

                ActualizarRelacion(Usuario, CobConexion, list);

            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        private void ActualizarRelacion(Usuario usu, string CobConexion, List<RelacionGestor> list)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(CobConexion);
            try
            {
                CapaDatos.StartTrans();

                SqlCommand sqlcmd = default(SqlCommand);

                int verificador = 0;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_U" };
                object[] Valores = { usu.Id_Emp, usu.Id_Cd, usu.Id_U };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCatRelacionGestor_Eliminar", ref verificador, Parametros, Valores);

                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_U", "@Id_Cd_Ver", "@Id_Cte", "@Id_Ter" };

                foreach (RelacionGestor rg in list)
                {
                    //if (usu.Id_Centros.Contains(rg.Id_Cd))
                    //{
                    Valores = new object[] { usu.Id_Emp, usu.Id_Cd, usu.Id_U, rg.Id_Cd, rg.Id_Cte, rg.Id_Ter };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatRelacionGestor_Insertar", ref verificador, Parametros, Valores);
                    //}
                }
                CapaDatos.CommitTrans();
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ModificarContraseñaUsuario(ref Usuario Usuario, string Conexion, ref Int32 verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Cd", "@Id_U", "@Cu_Pass" };
                object[] Valores = { Usuario.Id_Cd, Usuario.Id_U, Usuario.Cu_pass };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpCatUsuario_ModificarContraseña", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void BloqueoConsulta(Usuario Usuario, string Conexion, ref Int32 verificador)
        {
            try
            {
                SqlDataReader SqlDr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Cd", "@CU_User" };
                object[] Valores = { Usuario.Id_Cd, Usuario.Cu_User };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatUsuariosBloqueoConsulta", ref SqlDr, Parametros, Valores);

                if (SqlDr.HasRows == true)
                {
                    SqlDr.Read();
                    verificador = 1;
                    Usuario.Id_U = SqlDr.GetInt32(SqlDr.GetOrdinal("Id_U"));
                    Usuario.U_Nombre = SqlDr.GetString(SqlDr.GetOrdinal("U_Nombre"));
                    Usuario.Cu_FBloq = SqlDr.GetDateTime(SqlDr.GetOrdinal("Cu_FBloq"));
                    Usuario.Cu_Estatus = SqlDr.GetBoolean(SqlDr.GetOrdinal("Cu_Estatus"));
                }
                else
                    verificador = 2;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void BloqueoModificar(Usuario Usuario, string Conexion, ref Int32 verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Cd", "@Id_U", "@Cu_Estatus" };
                object[] Valores = { Usuario.Id_Cd, Usuario.Id_U, Usuario.Cu_Estatus };
                SqlCommand sqlcmd = default(SqlCommand);

                sqlcmd = CapaDatos.GenerarSqlCommand("spCatUsuariosBloqueoModificar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ConsultaDependencia(Sesion Sesion, string Conexion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

            string[] Parametros = { "@Id_Cd", "@Id_U", "@Id_OfiVer" };
            object[] Valores = { Sesion.Id_Cd, Sesion.Id_U, Sesion.Id_Cd_Ver };

            int Verificador = default(int);
            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpCatUsuario_ConsultaDependencia", ref Verificador, Parametros, Valores);

            CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            return Convert.ToBoolean(Verificador);

        }
        public void InsertaConfiguracionCorreo(Usuario usuario, string conexion, ref Int32 verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                string[] Parametros = { "@Id_Cd", "@Id_U", "@Cc_Correo", "@Cc_Pass", "@Cc_ServEntr", "@Cc_ServSal", "@Cc_PuertoEnt", "@Cc_PuertoSal" };
                object[] Valores = { usuario.Id_Cd, usuario.Id_U, usuario.Cc_Corrreo, usuario.Cc_Pass, usuario.Cc_ServEntr, usuario.Cc_ServSal, usuario.Cc_PuertoEnt, usuario.Cc_PuertoSal };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCuentaCorreo_Insertar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ModificarConfiguracionCorreo(Usuario usuario, string conexion, ref Int32 verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                string[] Parametros = { "@Id_Cd", "@Id_U", "@Id_CC", "@Cc_Correo", "@Cc_Pass", "@Cc_ServEntr", "@Cc_ServSal", "@Cc_PuertoEnt", "@Cc_PuertoSal" };
                object[] Valores = { usuario.Id_Cd, usuario.Id_U, usuario.Id_CC, usuario.Cc_Corrreo, usuario.Cc_Pass, usuario.Cc_ServEntr, usuario.Cc_ServSal, usuario.Cc_PuertoEnt, usuario.Cc_PuertoSal };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCuentaCorreo_ActualizaConfiguracion", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaConfiguracionCorreo(ref Usuario usuario, string conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                string[] Parametros = { "@Id_Cd", "@Id_U" };

                object[] Valores = { usuario.Id_Cd, usuario.Id_U };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCuentaCorreo_ConsultaConfiguracion", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    usuario.Id_CC = dr.GetInt32(dr.GetOrdinal("Id_CC"));
                    usuario.Cc_Corrreo = dr.GetString(dr.GetOrdinal("Cc_Correo"));
                    usuario.Cc_Pass = dr.GetString(dr.GetOrdinal("Cc_Pass"));
                    usuario.Cc_PuertoEnt = dr.GetInt32(dr.GetOrdinal("Cc_PuertoEnt"));
                    usuario.Cc_PuertoSal = dr.GetInt32(dr.GetOrdinal("Cc_PuertoSal"));
                    usuario.Cc_ServEntr = dr.GetString(dr.GetOrdinal("Cc_ServEntr"));
                    usuario.Cc_ServSal = dr.GetString(dr.GetOrdinal("Cc_ServSal"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertarConsecutivo(ref ConsecutivoFE FactElect, string conexion, ref int verificador)
        {
            throw new NotImplementedException();
        }
        public void ConsultaUsuarioCentro(int Id_Emp, int Id_Cd, string Id_U, string Conexion, ref System.Collections.ArrayList centros)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_U" };
                object[] Valores = { Id_Emp, Id_Cd, Id_U };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatUsuarioCentro_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    centros.Add(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaCorreoUsuario(Usuario usu, string Conexion, ref string Correo_Usuario)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_U" };

                object[] Valores = { usu.Id_Emp, usu.Id_Cd, usu.Id_U };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatUsuario_ConsultaCorreo", ref dr, Parametros, Valores);

                //Dim VarUsuario As Usuario
                while (dr.Read())
                {
                    Correo_Usuario = dr.GetString(dr.GetOrdinal("U_Correo"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaAutorizacionPrecio(Usuario usu, string Conexion, ref string Autorizacion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_U" };

                object[] Valores = { usu.Id_Emp, usu.Id_Cd, usu.Id_U };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatUsuario_ConsultaAutPreciosEspeciales", ref dr, Parametros, Valores);

                //Dim VarUsuario As Usuario
                while (dr.Read())
                {
                    Autorizacion = Convert.ToString ( dr.GetBoolean  (dr.GetOrdinal("U_AutorizaPrecio")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       


        public void ConsultaUsuarios(ref Usuario VarUsuario, string conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                string[] Parametros = { "@Id_Cd", "@Id_Emp", "@Id_U" };
                object[] Valores = { VarUsuario.Id_Cd, VarUsuario.Id_Emp, VarUsuario.Id_U };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SPCatUsuario_Consulta", ref dr, Parametros, Valores);

                
                if (dr.HasRows)
                {
                    dr.Read();
                    VarUsuario.U_Nombre = (string)dr.GetValue(dr.GetOrdinal("U_Nombre"));
                    VarUsuario.U_FNac = dr.IsDBNull(dr.GetOrdinal("U_FNac")) ? Convert.ToDateTime("01/01/0001") : (DateTime)dr.GetValue(dr.GetOrdinal("U_FNac"));
                    VarUsuario.U_Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("U_Activo")));
                    VarUsuario.Id_TU = (int)dr.GetValue(dr.GetOrdinal("Id_TU"));
                    VarUsuario.U_VerTodo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("U_VerTodo")));
                    VarUsuario.U_MultiCentro = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("U_MultiOfi")));
                    VarUsuario.Tu_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Tu_Descripcion"));
                    VarUsuario.U_ActivoStr = (string)dr.GetValue(dr.GetOrdinal("Activo_String"));
                    VarUsuario.U_Correo = (string)dr.GetValue(dr.GetOrdinal("U_Correo"));
                    VarUsuario.Cu_User = (string)dr.GetValue(dr.GetOrdinal("Cu_User"));
                    VarUsuario.Id_Id_U = (int)dr.GetValue(dr.GetOrdinal("Id_Id_U"));
                    VarUsuario.Ofi_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Cd_Nombre"));
                    VarUsuario.Id_Rik = (int)dr.GetValue(dr.GetOrdinal("Id_Rik"));
                    VarUsuario.U_SusCredito = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("U_SusCredito")));
                    VarUsuario.U_DiasVencimiento = dr.IsDBNull(dr.GetOrdinal("U_DiasVencimiento")) ? (Double?)null : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("U_DiasVencimiento")));
                    
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
