using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Sesion
    {
        //Datos Cliente-------------------------------------------
        private Int32 _Id_Cte;
        private DateTime _Fecha_Corte;
        private Int32 _Dias_Revision;
        private Int32 _Dias;
        private float _SaldoMinimo;
        private Int32 _Meses;
        private Int32 _Legal;
        private Int32 _Vencidos;
        private DateTime _Fecha_Cierre;
        private string _Cte_Nombre;

        public string Cte_Nombre
        {
            get { return _Cte_Nombre; }
            set { _Cte_Nombre = value; }
        }

           
        //Datos de la empresa------------------------------------
        private Int32 _Id_Emp;
        private string _Emp_Pref;
        private string _Emp_Cnx;
        //Datos de la oficina------------------------------------
        private Int32 _Id_Cd;
        private Int32 _Id_Cd_Ver;
        //Datos de la cuenta-------------------------------------
        private string _Cu_User;
        private string _Cu_Pass;
        private bool _Cu_Modif_Pass_Voluntario;
        //Datos de configuración---------------------------------
        
        private Int32 _Minutos;
        private bool _U_VerTodo;
        private bool _U_MultiOfi;
        private bool _U_ConfigPermitirCliSinCompromisos;

        private Int32 _U_ConfigCliSinCompromisos;
        //Datos del usuario--------------------------------------
        private Int32 _Id_U;
        private Int32 _Id_TU;
        private string _U_Nombre;
        private string _U_Correo;
        private bool _Dependientes;
        private DateTime _HoraInicio;
        private string _URL;
        public int MesActual { get; set; }
        public int AnioActual { get; set; }
        
        //Datos del Cliente -----------------------------------------
        public Int32 Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }
        public DateTime Fecha_Corte
        {
            get { return _Fecha_Corte; }
            set { _Fecha_Corte = value; }
        }
        public Int32 Dias_Revision
        {
            get { return _Dias_Revision; }
            set { _Dias_Revision = value; }
        }

        public Int32 Dias
        {
            get { return _Dias; }
            set { _Dias = value; }
        }

        public float SaldoMinimo
        {
            get { return _SaldoMinimo; }
            set { _SaldoMinimo = value; }
        }

        public Int32 Meses
        {
            get { return _Meses; }
            set { _Meses = value; }
        }
        public int Legal
        {
            get { return _Legal; }
            set { _Legal = value; }
        }

        public Int32 Vencidos
        {
            get { return _Vencidos; }
            set { _Vencidos = value; }
        }

        public DateTime Fecha_Cierre
        {
            get { return _Fecha_Cierre; }
            set { _Fecha_Cierre = value; }
        }
        
        //Datos de la empresa------------------------------------
        public Int32 Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public string Emp_Pref
        {
            get { return _Emp_Pref.Trim(); }
            set { _Emp_Pref = value.Trim(); }
        }
        public string Emp_Cnx
        {
            get { return _Emp_Cnx.Trim(); }
            set { _Emp_Cnx = value.Trim(); }
        }
        private DateTime _CalendarioIni;
        private DateTime _CalendarioFin;
        private bool _Propia;
        private string _Emp_Nombre;
        private string _Cd_Nombre;
        private int _Id_Rik;
        

        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }

        public string Cd_Nombre
        {
            get { return _Cd_Nombre; }
            set { _Cd_Nombre = value; }
        }

        public string Emp_Nombre
        {
            get { return _Emp_Nombre; }
            set { _Emp_Nombre = value; }
        }

        public bool Propia
        {
            get { return _Propia; }
            set { _Propia = value; }
        }

        //Datos de la oficina------------------------------------
        public Int32 Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        public Int32 Id_Cd_Ver
        {
            get { return _Id_Cd_Ver; }
            set { _Id_Cd_Ver = value; }
        }
        //Datos de la cuenta-------------------------------------
        public string Cu_User
        {
            get { return _Cu_User.Trim(); }
            set { _Cu_User = value.Trim(); }
        }
        public string Cu_Pass
        {
            get { return _Cu_Pass.Trim(); }
            set { _Cu_Pass = value.Trim(); }
        }
        public bool Cu_Modif_Pass_Voluntario
        {
            get { return _Cu_Modif_Pass_Voluntario; }
            set { _Cu_Modif_Pass_Voluntario = value; }
        }
        //Datos de configuración---------------------------------
         
        public Int32 Minutos
        {
            get { return _Minutos; }
            set { _Minutos = value; }
        }
        public bool U_VerTodo
        {
            get { return _U_VerTodo; }
            set { _U_VerTodo = value; }
        }
        public bool U_MultiOfi
        {
            get { return _U_MultiOfi; }
            set { _U_MultiOfi = value; }
        }
        public bool U_ConfigPermitirCliSinCompromisos
        {
            get { return _U_ConfigPermitirCliSinCompromisos; }
            set { _U_ConfigPermitirCliSinCompromisos = value; }
        }
        public Int32 U_ConfigCliSinCompromisos
        {
            get { return _U_ConfigCliSinCompromisos; }
            set { _U_ConfigCliSinCompromisos = value; }
        }

        //Datos del usuario--------------------------------------
        public Int32 Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }
        public Int32 Id_TU
        {
            get { return _Id_TU; }
            set { _Id_TU = value; }
        }
        public string U_Nombre
        {
            get { return _U_Nombre.Trim(); }
            set { _U_Nombre = value.Trim(); }
        }
        public string U_Correo
        {
            get { return _U_Correo.Trim(); }
            set { _U_Correo = value.Trim(); }
        }
        public bool Dependientes
        {
            get { return _Dependientes; }
            set { _Dependientes = value; }
        }
 
        public DateTime CalendarioIni
        {
            get { return _CalendarioIni; }
            set { _CalendarioIni = value; }
        }
        public DateTime CalendarioFin
        {
            get { return _CalendarioFin; }
            set { _CalendarioFin = value; }
        }

        public DateTime HoraInicio
        {
            get { return _HoraInicio; }
            set { _HoraInicio = value; }
        }

        public string URL
        {
            get { return _URL; }
            set { _URL = value; }
        }


      
    }
}
