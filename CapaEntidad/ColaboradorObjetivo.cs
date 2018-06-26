using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    [Serializable]
    public class ColaboradorObjetivo
    {
        
        private int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        private int _Id_Cd;
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

        private int _Id_Colaborador;
        public int Id_Colaborador
        {
            get { return _Id_Colaborador; }
            set { _Id_Colaborador = value; }
        }

        private int _Id_ColaboradorObjetivo;
        public int Id_ColaboradorObjetivo
        {
            get { return _Id_ColaboradorObjetivo; }
            set { _Id_ColaboradorObjetivo = value; }
        }

        private bool _Estatus;
        public bool Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }

        private object _Anio;
        public object Anio
        {
            get { return _Anio; }
            set { _Anio = value; }
        }

        private object _Mes;
        public object Mes
        {
            get { return _Mes; }
            set { _Mes = value; }
        }

        private float _Objetivo;
        public float Objetivo
        {
            get { return _Objetivo; }
            set { _Objetivo = value; }
        }

        //private object _Prd_FechaFin;
        //public object Prd_FechaFin
        //{
        //    get { return _Prd_FechaFin; }
        //    set { _Prd_FechaFin = value; }
        //}

       
 
       

    }
}

