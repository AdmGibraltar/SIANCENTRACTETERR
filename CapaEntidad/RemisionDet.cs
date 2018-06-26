using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class RemisionDet
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

        private int _Id_Rem;
        public int Id_Rem
        {
            get { return _Id_Rem; }
            set { _Id_Rem = value; }
        }

        private int _Id_RemDet;
        public int Id_RemDet
        {
            get { return _Id_RemDet; }
            set { _Id_RemDet = value; }
        }

        private int? _Id_Ter;
        public int? Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }

        private int _Id_Prd;
        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }

        private int _Rem_Cant;
        public int Rem_Cant
        {
            get { return _Rem_Cant; }
            set { _Rem_Cant = value; }
        }

        private int? _Rem_Asignar;
        public int? Rem_Asignar
        {
            get { return _Rem_Asignar; }
            set { _Rem_Asignar = value; }
        }

        private int? _Rem_CantE;
        public int? Rem_CantE
        {
            get { return _Rem_CantE; }
            set { _Rem_CantE = value; }
        }

        private int? _Rem_CantF;
        public int? Rem_CantF
        {
            get { return _Rem_CantF; }
            set { _Rem_CantF = value; }
        }

        private double _Rem_Precio;
        public double Rem_Precio
        {
            get { return _Rem_Precio; }
            set { _Rem_Precio = value; }
        }

        private bool _Ped_Pertenece;
        public bool Ped_Pertenece
        {
            get { return _Ped_Pertenece; }
            set { _Ped_Pertenece = value; }
        }

        private string _Prd_Descripcion;
        private string _Ter_Nombre;

        public string Ter_Nombre
        {
            get { return _Ter_Nombre; }
            set { _Ter_Nombre = value; }
        }
        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }

        private int _Id_CteExt;
        public int Id_CteExt
        {
            get { return _Id_CteExt; }
            set { _Id_CteExt = value; }
        }
        private Producto _producto;
        public Producto Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }
        private string _Clp_Release;
        public string Clp_Release
        {
            get { return _Clp_Release; }
            set { _Clp_Release = value; }
        }

        private double _Rem_Importe;
        public double Rem_Importe
        {
            get { return _Rem_Importe; }
            set { _Rem_Importe = value; }
        }
    }
}
