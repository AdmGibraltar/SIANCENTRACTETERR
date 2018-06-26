using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    [Serializable]
    public class Colaborador
    {
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_Empleado;

        private int _Num_Nomina;
        private string _Nombre_Empleado;
        private DateTime? _Emp_FechaAlta;
        private string _Emp_Puesto;
        private string _Emp_Correo;
        private int _Emp_Estatus;
        private DateTime? _Emp_FechaUltMod;

      

        private int _Id_Colaborador;
        private DateTime? _FechaInicioOperacion;
        private int _Id_Region;
        private int _Id_Sucursal;
        private int _Id_TipoUsuario;

        //CatUnicoColaborador
         
        private string _Id_UEN;
        private int _Colaborador_Estatus;
        private Double? _Sueldo_Variable;
        private Double? _Porcentaje_Contribucion;
        private string _Emp_Sucursal;


        private List<ColaboradorObjetivo> _listaColaboradorObjetivos;

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        public int Id_Empleado
        {
            get { return _Id_Empleado; }
            set { _Id_Empleado = value; }
        }

        public int Num_Nomina
        {
            get { return _Num_Nomina; }
            set { _Num_Nomina = value; }
        }


        public string Nombre_Empleado
        {
            get { return _Nombre_Empleado; }
            set { _Nombre_Empleado = value; }
        }

        public string Emp_Puesto
        {
            get { return _Emp_Puesto; }
            set { _Emp_Puesto = value; }
        }

        public string Emp_Correo
        {
            get { return _Emp_Correo; }
            set { _Emp_Correo = value; }
        }

        public int Emp_Estatus
        {
            get { return _Emp_Estatus; }
            set { _Emp_Estatus = value; }
        }

        public int Id_Colaborador
        {
            get { return _Id_Colaborador; }
            set { _Id_Colaborador = value; }
        }

        public int Id_Region
        {
            get { return _Id_Region; }
            set { _Id_Region = value; }
        }

        public int Id_Sucursal
        {
            get { return _Id_Sucursal; }
            set { _Id_Sucursal = value; }
        }

        public int Id_TipoUsuario
        {
            get { return _Id_TipoUsuario; }
            set { _Id_TipoUsuario = value; }
        }

        public string Id_UEN
        {
            get { return _Id_UEN; }
            set { _Id_UEN = value; }
        }

        public int Colaborador_Estatus
        {
            get { return _Colaborador_Estatus; }
            set { _Colaborador_Estatus = value; }
        }
      
        public Double? Sueldo_Variable
        {
            get { return _Sueldo_Variable; }
            set { _Sueldo_Variable = value; }
        }
        public Double? Porcentaje_Contribucion
        {
            get { return _Porcentaje_Contribucion; }
            set { _Porcentaje_Contribucion = value; }
        }

        public DateTime? Emp_FechaUltMod
        {
            get { return _Emp_FechaUltMod; }
            set { _Emp_FechaUltMod = value; }
        }
        public DateTime? FechaInicioOperacion
        {
            get { return _FechaInicioOperacion; }
            set { _FechaInicioOperacion = value; }
        }

        public DateTime? Emp_FechaAlta
        {
            get { return _Emp_FechaAlta; }
            set { _Emp_FechaAlta = value; }
        }


        public string Emp_Sucursal
        {
            get { return _Emp_Sucursal; }
            set { _Emp_Sucursal = value; }
        }


        public List<ColaboradorObjetivo> ListaColaboradorObjetivos
        {
            get { return _listaColaboradorObjetivos; }
            set { _listaColaboradorObjetivos = value; }
        }
      
    
        private List<Colaborador> _list_colaborador;

        public List<Colaborador> List_colaborador
        {
            get { return _list_colaborador; }
            set { _list_colaborador = value; }
        }


        private List<ConceptosNomina> _listaConceptosNomina;
        public List<ConceptosNomina> ListaConceptosNomina
        {
            get { return _listaConceptosNomina; }
            set { _listaConceptosNomina = value; }
        }
    }
}

