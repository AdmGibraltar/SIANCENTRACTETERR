using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Territorios
    {
        int _Id_Emp;
        int _Id_Cd;
        int _Id_Ter;
        string _Descripcion;
        int _Id_Uen;
        string _Uen_Descripcion;
        int _Id_Rik;
        string _Rik_Nombre;
        int _Id_Seg;
        bool _Estatus;
        string _EstatusStr;
         
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
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        public int Id_Uen
        {
            get { return _Id_Uen; }
            set { _Id_Uen = value; }
        }
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }
        public int Id_Seg
        {
            get { return _Id_Seg; }
            set { _Id_Seg = value; }
        }
        public bool Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }
        public string EstatusStr
        {
            get { return _EstatusStr; }
            set { _EstatusStr = value; }
        }
        public string Uen_Descripcion
        {
            get { return _Uen_Descripcion; }
            set { _Uen_Descripcion = value; }
        }
        public string Rik_Nombre
        {
            get { return _Rik_Nombre; }
            set { _Rik_Nombre = value; }
        }
    }
}
