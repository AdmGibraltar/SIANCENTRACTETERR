using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using CapaEntidad;
using CapaDatos;


namespace CapaNegocios
{
    public class CN_CatColaborador
    {

        public void ConsultaEmpleadoNomina(ref Colaborador colaborador, string Conexion, int id_Emp, int id_Cd, int id_Cd_Ver, int id_Empleado, bool catalogo)
        {
            try
            {
                CD_CatColaborador claseCapaDatos = new CD_CatColaborador();
                claseCapaDatos.ConsultaEmpleadoNomina(ref colaborador, Conexion, id_Emp, id_Cd, id_Cd_Ver, id_Empleado, catalogo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaEmpleadoPorNumeroNomina(ref Colaborador colaborador, string Conexion, int id_Emp,  int num_nomina, bool catalogo)
        {
            try
            {
                CD_CatColaborador claseCapaDatos = new CD_CatColaborador();
                claseCapaDatos.ConsultaEmpleadoPorNumeroNomina(ref colaborador, Conexion, id_Emp,  num_nomina, catalogo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        


        public void ConsultaListaObjetivos(ColaboradorObjetivo listaObjetivos, string Conexion, ref List<ColaboradorObjetivo> List)
        {
            try
            {
                CD_CatColaborador claseCapaDatos = new CD_CatColaborador();
                claseCapaDatos.ConsultaListaObjetivos(listaObjetivos, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

         
        public void ConsultaListaConceptosNomina(ConceptosNomina conceptosNomina, string Conexion, ref List<ConceptosNomina> List)
        {
            try
            {
                CD_CatColaborador claseCapaDatos = new CD_CatColaborador();
                claseCapaDatos.ConsultaListaConceptosNomina(conceptosNomina, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarColaborador(Colaborador colaborador, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatColaborador claseCapaDatos = new CD_CatColaborador();
                claseCapaDatos.InsertarColaborador(colaborador, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarColaborador(Colaborador colaborador, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatColaborador claseCapaDatos = new CD_CatColaborador();
                claseCapaDatos.ModificarColaborador(colaborador, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GuardarCargaMasivaConceptosMonto(Colaborador colaborador, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatColaborador claseCapaDatos = new CD_CatColaborador();
                claseCapaDatos.GuardarCargaMasivaConceptosMonto(colaborador, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Carga de excepciones de las comisiones 
        /// </summary>
        /// <param name="colaborador"></param>
        /// <param name="Conexion"></param>
        /// <param name="verificador"></param>

        public void GuardarCargaMasivaExcepciones(ComisionesExcepcionesRentabilidad exepciones, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatColaborador claseCapaDatos = new CD_CatColaborador();
                claseCapaDatos.GuardarCargaMasivaExcepciones(exepciones, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina de excepciones de las comisiones 
        /// </summary>
        /// <param name="colaborador"></param>
        /// <param name="Conexion"></param>
        /// <param name="verificador"></param>

        public void EliminarCargaMasivaExcepciones( int id_emp , string Conexion, ref int verificador)
        {
            try
            {
                CD_CatColaborador claseCapaDatos = new CD_CatColaborador();
                claseCapaDatos.EliminarCargaMasivaExcepciones(  id_emp ,Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}