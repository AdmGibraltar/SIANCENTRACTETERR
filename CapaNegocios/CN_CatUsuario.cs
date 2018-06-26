using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Collections;

namespace CapaNegocios
{
    public class CN_CatUsuario
    {
        public CN_CatUsuario()
        { }

        public void ConsultaUsuarios(Usuario usuario, string conexion, ref List<Usuario> List)
        {
            try
            {
                CapaDatos.CD_CatUsuario claseCapaDatos = new CapaDatos.CD_CatUsuario();
                claseCapaDatos.ConsultaUsuarios(usuario, conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertarUsuario(ref Usuario Usuario, string Conexion, ArrayList seleccionados, ref int Verificador,ref int Verificador2, List<RelacionGestor> list, string CobConexion)
        {
            try
            {
                CapaDatos.CD_CatUsuario claseCapaDatos = new CapaDatos.CD_CatUsuario();
                claseCapaDatos.InsertarUsuario(ref Usuario, Conexion, seleccionados, ref Verificador,ref Verificador2 , list, CobConexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ModificarUsuario(Usuario Usuario, string conexion, ArrayList seleccionados, ref int Verificador, List<RelacionGestor> list, string CobConexion)
        {
            try
            {
                CapaDatos.CD_CatUsuario claseCapaDatos = new CapaDatos.CD_CatUsuario();
                claseCapaDatos.ModificarUsuario(Usuario, ref Verificador, seleccionados, conexion, list, CobConexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ModificarContraseñaUsuario(ref Usuario usuario, string conexion, ref int Verificador)
        {
            try
            {
                CapaDatos.CD_CatUsuario claseCapaDatos = new CapaDatos.CD_CatUsuario();
                claseCapaDatos.ModificarContraseñaUsuario(ref usuario, conexion, ref Verificador);
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
                CapaDatos.CD_CatUsuario claseCapaDatos = new CapaDatos.CD_CatUsuario();
                claseCapaDatos.BloqueoConsulta(Usuario, Conexion, ref verificador);
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
                CapaDatos.CD_CatUsuario claseCapaDatos = new CapaDatos.CD_CatUsuario();
                claseCapaDatos.BloqueoModificar(Usuario, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ConsultaDependencia(Sesion Sesion, string Conexion)
        {
            try
            {
                CapaDatos.CD_CatUsuario claseCapaDatos = new CapaDatos.CD_CatUsuario();
                return claseCapaDatos.ConsultaDependencia(Sesion, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public void ConsultaCorreoUsuario(Int32 Id_User, string Conexion, ref string Correo_Usuario)
        //{
        //    try
        //    {
        //        CapaDatos.CD_CatUsuario claseCapaDatos = new CapaDatos.CD_CatUsuario();
        //        claseCapaDatos.ConsultaCorreoUsuario(Id_User, Conexion, ref Correo_Usuario);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public void InsertaConfiguracionCorreo(Usuario usuario, string conexion, ref Int32 verificador)
        {
            try
            {
                CD_CatUsuario CD = new CD_CatUsuario();
                CD.InsertaConfiguracionCorreo(usuario, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ModificaronfiguracionCorreo(Usuario usuario, string conexion, ref Int32 verificador)
        {
            try
            {
                CD_CatUsuario CD = new CD_CatUsuario();
                CD.ModificarConfiguracionCorreo(usuario, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public void ModificarConfiguracionExpCli(Usuario usuario, string conexion, ref Int32 verificador)
        //{
        //    try
        //    {
        //        CD_CatUsuario CD = new CD_CatUsuario();
        //        CD.ModificarConfiguracionExpCli(usuario, conexion,ref verificador);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public void ConsultaConfiguracionCorreo(ref Usuario usuario, string conecion)
        {
            try
            {
                CD_CatUsuario CD = new CD_CatUsuario();
                CD.ConsultaConfiguracionCorreo(ref usuario, conecion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaUsuarioCentro(int Id_Emp, int Id_Cd, string Id_U, string Conexion, ref System.Collections.ArrayList centros)
        {
            try
            {
                CapaDatos.CD_CatUsuario claseCapaDatos = new CapaDatos.CD_CatUsuario();
                claseCapaDatos.ConsultaUsuarioCentro(Id_Emp,Id_Cd, Id_U, Conexion, ref centros);
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
                CapaDatos.CD_CatUsuario claseCapaDatos = new CapaDatos.CD_CatUsuario();
                claseCapaDatos.ConsultaCorreoUsuario(usu, Conexion, ref Correo_Usuario);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaAutorizacionPrecio(Usuario usu, string Conexion, ref string  Autorizacion)
        {
            try
            {
                CapaDatos.CD_CatUsuario claseCapaDatos = new CapaDatos.CD_CatUsuario();
                claseCapaDatos.ConsultaAutorizacionPrecio(usu, Conexion, ref Autorizacion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


     

        public void ConsultaUsuarios(ref Usuario usu, string conexion)
        {
            try
            {
                CapaDatos.CD_CatUsuario claseCapaDatos = new CapaDatos.CD_CatUsuario();
                claseCapaDatos.ConsultaUsuarios(ref usu, conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
