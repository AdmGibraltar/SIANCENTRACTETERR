using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Configuracion
    {
        public void Consulta(ref ConfiguracionGlobal Configuracion, string Conexion)
        {
            try
            {
                SqlDataReader SqlDr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Cd", "@Id_Emp" };

                object[] Valores = { Configuracion.Id_Cd, Configuracion.Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSysConfiguracion_Consulta", ref SqlDr, Parametros, Valores);

                if (SqlDr.HasRows == true)
                {
                    SqlDr.Read();
                    Configuracion.Solicitud_Prospecto = Convert.ToBoolean(Convert.ToInt32(SqlDr.GetValue(SqlDr.GetOrdinal("Solicitud_Prospecto"))));
                    Configuracion.Hora_Zona = (String)SqlDr.GetValue(SqlDr.GetOrdinal("Hora_Zona"));
                    Configuracion.Hora_Verano = Convert.ToBoolean(Convert.ToInt32(SqlDr.GetValue(SqlDr.GetOrdinal("Hora_Verano"))));
                    Configuracion.Mail_Servidor = (String)SqlDr.GetValue(SqlDr.GetOrdinal("Mail_Servidor"));
                    Configuracion.Mail_Usuario = (String)SqlDr.GetValue(SqlDr.GetOrdinal("Mail_Usuario"));
                    Configuracion.Mail_Contraseña = (String)SqlDr.GetValue(SqlDr.GetOrdinal("Mail_Contraseña"));
                    Configuracion.Mail_Puerto = (String)SqlDr.GetValue(SqlDr.GetOrdinal("Mail_Puerto"));
                    Configuracion.Mail_Remitente = (String)SqlDr.GetValue(SqlDr.GetOrdinal("Mail_Remitente"));
                    Configuracion.Login_Intentos = (String)SqlDr.GetValue(SqlDr.GetOrdinal("Login_Intentos"));
                    Configuracion.Login_Tiempo_Bloqueo = (String)SqlDr.GetValue(SqlDr.GetOrdinal("Login_Tiempo_Bloqueo"));
                    Configuracion.Contraseña_Tiempo_Vida = (String)SqlDr.GetValue(SqlDr.GetOrdinal("Contraseña_Tiempo_Vida"));
                    Configuracion.Contraseña_Long_Min = (String)SqlDr.GetValue(SqlDr.GetOrdinal("Contraseña_Long_Min"));
                    Configuracion.Mail_CompLocal = SqlDr.IsDBNull(SqlDr.GetOrdinal("Mail_CompLocal")) ? "" : (String)SqlDr.GetValue(SqlDr.GetOrdinal("Mail_CompLocal"));
                    Configuracion.Mail_PrecioEsp = SqlDr.IsDBNull(SqlDr.GetOrdinal("Mail_PrecioEsp")) ? "" : (String)SqlDr.GetValue(SqlDr.GetOrdinal("Mail_PrecioEsp"));
                    Configuracion.Mail_BaseInstalada = SqlDr.IsDBNull(SqlDr.GetOrdinal("Mail_Bi")) ? "" : (String)SqlDr.GetValue(SqlDr.GetOrdinal("Mail_Bi"));
                    Configuracion.Mail_Acys = SqlDr.IsDBNull(SqlDr.GetOrdinal("Mail_Acys")) ? "" : (String)SqlDr.GetValue(SqlDr.GetOrdinal("Mail_Acys"));
                    Configuracion.Mail_Valuacion = SqlDr.IsDBNull(SqlDr.GetOrdinal("Mail_CorreoValuacion")) ? "" : (String)SqlDr.GetValue(SqlDr.GetOrdinal("Mail_CorreoValuacion"));
                    Configuracion.Mail_MinValuacion = SqlDr.IsDBNull(SqlDr.GetOrdinal("Mail_MinValuacion")) ? 0 : Convert.ToDouble(SqlDr.GetValue(SqlDr.GetOrdinal("Mail_MinValuacion")));

                    Configuracion.Mail_GastosAvisoGerente = SqlDr.IsDBNull(SqlDr.GetOrdinal("Mail_GastosAvisoGerente")) ? "" : (String)SqlDr.GetValue(SqlDr.GetOrdinal("Mail_GastosAvisoGerente"));
                    Configuracion.Mail_GastosAvisoUsuario = SqlDr.IsDBNull(SqlDr.GetOrdinal("Mail_GastosAvisoUsuario")) ? "" : (String)SqlDr.GetValue(SqlDr.GetOrdinal("Mail_GastosAvisoUsuario"));
                    Configuracion.RutaSistemaGastos = SqlDr.IsDBNull(SqlDr.GetOrdinal("RutaSistemaGastos")) ? "" : (String)SqlDr.GetValue(SqlDr.GetOrdinal("RutaSistemaGastos"));

                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Modificar(ref ConfiguracionGlobal Configuracion, string Conexion)
        {
            try
            {
                //SqlDataReader SqlDr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
			"@Id_Cd",
            "@Id_Emp",
			"@Solicitud_Prosp",
			"@Hora_Zona",
			"@Hora_Verano",
			"@Mail_Servidor",
			"@Mail_Usuario",
			"@Mail_Contraseña",
			"@Mail_Puerto",
			"@Mail_Remitente",
			"@Login_Intentos",
			"@Login_Tiempo_Bloqueo",
			"@Contraseña_Tiempo_Vida",
			"@Contraseña_Long_Min",
            "@Mail_CompLocal",
            "@Mail_PrecioEsp",
            "@Mail_Bi",
            "@Mail_CorreoValuacion",
            "@Mail_MinValuacion",
            "@Mail_Acys"
		};

                object[] Valores = {
			Configuracion.Id_Cd,
            Configuracion.Id_Emp,
			Configuracion.Solicitud_Prospecto,
			Configuracion.Hora_Zona,
			Configuracion.Hora_Verano,
			Configuracion.Mail_Servidor,
			Configuracion.Mail_Usuario,
			Configuracion.Mail_Contraseña,
			Configuracion.Mail_Puerto,
			Configuracion.Mail_Remitente,
			Configuracion.Login_Intentos,
			Configuracion.Login_Tiempo_Bloqueo,
			Configuracion.Contraseña_Tiempo_Vida,
			Configuracion.Contraseña_Long_Min,
            Configuracion.Mail_CompLocal,
            Configuracion.Mail_PrecioEsp,
            Configuracion.Mail_BaseInstalada,
            Configuracion.Mail_Valuacion,
            Configuracion.Mail_MinValuacion,
            Configuracion.Mail_Acys
		};

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand_Nonquery("spSysConfiguracion_Modificar", Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
