using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class InventarioDiario
    {
        #region Atributos
        private int CDI;
        private Int64 SKU;  
        private int Cantidad;
        private int Total;     
        private int BackOrder;
        private int Transito;      
        #endregion

        #region Metodos
        public int CDI1
        {
            get { return CDI; }
            set { CDI = value; }
        }

        public Int64 SKU1
        {
            get { return SKU; }
            set { SKU = value; }
        }


        public int Cantidad1
        {
            get { return Cantidad; }
            set { Cantidad = value; }
        }


        public int Total1
        {
            get { return Total; }
            set { Total = value; }
        }

        
        public int BackOrder1
        {
            get { return BackOrder; }
            set { BackOrder = value; }
        }
        
        public int Transito1
        {
            get { return Transito; }
            set { Transito = value; }
        }

    
      

        #endregion
    }
}
