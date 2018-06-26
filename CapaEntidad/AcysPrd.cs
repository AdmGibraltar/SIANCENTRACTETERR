using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class AcysPrd
    {
        private int _Id_Prd;

        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }
        private string _Prd_Descripcion;

        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }
        private string _Prd_Presentacion;

        public string Prd_Presentacion
        {
            get { return _Prd_Presentacion; }
            set { _Prd_Presentacion = value; }
        }
        private string _Prd_UniNom;

        public string Prd_UniNom
        {
            get { return _Prd_UniNom; }
            set { _Prd_UniNom = value; }
        }
        private double _Prd_Precio;

        public double Prd_Precio
        {
            get { return _Prd_Precio; }
            set { _Prd_Precio = value; }
        }
        private int _Acys_Cantidad;

        public int Acys_Cantidad
        {
            get { return _Acys_Cantidad; }
            set { _Acys_Cantidad = value; }
        }
        private int _Acys_Frecuencia;

        public int Acys_Frecuencia
        {
            get { return _Acys_Frecuencia; }
            set { _Acys_Frecuencia = value; }
        }
        private bool _Acys_Lunes;

        public bool Acys_Lunes
        {
            get { return _Acys_Lunes; }
            set { _Acys_Lunes = value; }
        }
        private bool _Acys_Martes;

        public bool Acys_Martes
        {
            get { return _Acys_Martes; }
            set { _Acys_Martes = value; }
        }
        private bool _Acys_Miercoles;

        public bool Acys_Miercoles
        {
            get { return _Acys_Miercoles; }
            set { _Acys_Miercoles = value; }
        }
        private bool _Acys_Jueves;

        public bool Acys_Jueves
        {
            get { return _Acys_Jueves; }
            set { _Acys_Jueves = value; }
        }
        private bool _Acys_Viernes;

        public bool Acys_Viernes
        {
            get { return _Acys_Viernes; }
            set { _Acys_Viernes = value; }
        }
        private bool _Acys_Sabado;

        public bool Acys_Sabado
        {
            get { return _Acys_Sabado; }
            set { _Acys_Sabado = value; }
        }
        private string _Acs_Doc;
        private int _Acys_UltSCtp;

        public int Acys_UltSCtp
        {
            get { return _Acys_UltSCtp; }
            set { _Acys_UltSCtp = value; }
        }
        private int _Acys_UltACtp;

        public int Acys_UltACtp
        {
            get { return _Acys_UltACtp; }
            set { _Acys_UltACtp = value; }
        }

        public string Acs_Doc
        {
            get { return _Acs_Doc; }
            set { _Acs_Doc = value; }
        }


        private string _Acs_Modalidad;
        public string Acs_Modalidad
        {
            get { return _Acs_Modalidad; }
            set { _Acs_Modalidad = value; }
        }
    }
}
