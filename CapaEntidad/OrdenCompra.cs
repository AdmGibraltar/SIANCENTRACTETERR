using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class OrdenCompra
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

        private int _Id_Ord;
        public int Id_Ord
        {
            get { return _Id_Ord; }
            set { _Id_Ord = value; }
        }


        private int _Id_Pvd;
        public int Id_Pvd
        {
            get { return _Id_Pvd; }
            set { _Id_Pvd = value; }
        }

        private int _Id_U;
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }

        private DateTime _Ord_Fecha;
        public DateTime Ord_Fecha
        {
            get { return _Ord_Fecha; }
            set { _Ord_Fecha = value; }
        }

        private string _Ord_FechaHoraEmision;
        public string Ord_FechaHoraEmision
        {
            get { return _Ord_FechaHoraEmision; }
            set { _Ord_FechaHoraEmision = value; }
        }

        private int _Ord_Tipo;
        public int Ord_Tipo
        {
            get { return _Ord_Tipo; }
            set { _Ord_Tipo = value; }
        }

        private string _Ord_Notas;
        public string Ord_Notas
        {
            get { return _Ord_Notas; }
            set { _Ord_Notas = value; }
        }

        private string _Ord_Estatus;
        public string Ord_Estatus
        {
            get { return _Ord_Estatus; }
            set { _Ord_Estatus = value; }
        }

        private string _Ord_EstatusStr;
        public string Ord_EstatusStr
        {
            get { return _Ord_EstatusStr; }
            set { _Ord_EstatusStr = value; }
        }


        private int _Ord_EstatusEmision;
        public int Ord_EstatusEmision
        {
            get { return _Ord_EstatusEmision; }
            set { _Ord_EstatusEmision = value; }
        }


        private string _Ord_EstatusEmisionStr;
        public string Ord_EstatusEmisionStr
        {
            get { return _Ord_EstatusEmisionStr; }
            set { _Ord_EstatusEmisionStr = value; }
        }

        private string _Ord_Nombre_U;
        public string Ord_Nombre_U
        {
            get { return _Ord_Nombre_U; }
            set { _Ord_Nombre_U = value; }
        }//
        private string _Pvd_Descripcion;
        public string Pvd_Descripcion
        {
            get { return _Pvd_Descripcion; }
            set { _Pvd_Descripcion = value; }
        }
        private List<OrdenCompraDet> _listOrdenCompra;
        public List<OrdenCompraDet> ListOrdenCompra
        {
            get { return _listOrdenCompra; }
            set { _listOrdenCompra = value; }
        }
    }
}
