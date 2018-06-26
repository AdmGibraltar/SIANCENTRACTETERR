using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_CapAcys
    {
        public void ConsultarAcys_Lista(Acys acys, string Conexion, ref List<Acys> List)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.ConsultarAcys_Lista(acys, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(Acys acys, List<AcysPrd> list, string Conexion, DataTable seleccionados, ref int verificador, List<Asesoria> asesorias, List<Producto> servicios,  List<Producto> serviciosMantenimiento)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.Insertar(acys, list, Conexion, seleccionados, ref verificador, asesorias, servicios, serviciosMantenimiento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(Acys acys, List<AcysPrd> list, string Conexion, DataTable dt, ref int verificador, List<Asesoria> asesorias, List<Producto> servicios, List<Producto> serviciosMantenimiento)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.Modificar(acys, list, Conexion, dt, ref verificador, asesorias, servicios,  serviciosMantenimiento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Cancelar(Acys acys, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.Cancelar(acys, Conexion, ref verificador);
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
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.actualizarEstatus(acys, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaEnvio(ref Acys acys, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.ConsultaEnvio(ref acys, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Consultar(ref Acys acys, string Conexion)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.Consultar(ref acys, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarDet(Acys acys, ref  DataTable dtAcuerdos, string Conexion)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.ConsultarDet(acys, ref dtAcuerdos, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Imprimir(Acys acys, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.Imprimir(acys, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarReemplazos(Acys acys, int Id_Prd, ref DataTable list2, string Conexion)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.ConsultarReemplazos(acys, Id_Prd, ref list2, Conexion);
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
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.ModificarEquivalencia(Id_Prd, Id_Prd_Original, Id_Acys, Id_Emp, Id_Cd, Conexion);
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
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.ConsultaAsesorias(acys, Conexion, ref List);
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
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.ConsultaEstBi(acys, Conexion, ref List);
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
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.ConsultaEstBiMantenimiento(acys, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
