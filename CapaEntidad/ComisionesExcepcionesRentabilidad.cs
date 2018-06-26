using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    [Serializable]
    public class ComisionesExcepcionesRentabilidad
    {
        private int _Id_Emp;
        private int _Id_Cd;
        private int? _Id_Rik;
        private int? _Id_Cte;
        private int _Estatus_Rentabilidad;
        private Double? _Rentabilidad;

        private int _Id_Usuario;
        private DateTime? _Emp_FechaUltMod;
      
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
        public int? Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }

        public int? Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }


        public int Estatus_Rentabilidad
        {
            get { return _Estatus_Rentabilidad; }
            set { _Estatus_Rentabilidad = value; }
        }


        public Double? Rentabilidad
        {
            get { return _Rentabilidad; }
            set { _Rentabilidad = value; }
        }
       

        public DateTime? Emp_FechaUltMod
        {
            get { return _Emp_FechaUltMod; }
            set { _Emp_FechaUltMod = value; }
        }

        public int Id_Usuario
        {
            get { return _Id_Usuario; }
            set { _Id_Usuario = value; }
        }

        
    
    }
}


