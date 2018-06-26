using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ReportePedidosRemisiones
    {
        #region Atributos
 
        private string FechaOrdendeCompra ;
        private int Remision;
        private int CodigoCDI ;
        private string NombreCDI;
        private int Codigo_SKU;
        private string Descripcion_SKU;
        private int Presentacion;
        private string Unidad;
        private int Cantidad;
        private int ordenada;
        private decimal Costo_AAA;
        private decimal Importe_Total_AAA;
        private int Num_OrdendeCompra;


        #endregion

        #region Metodos
        public string FechaOrdendeCompra1
        {
            get { return FechaOrdendeCompra; }
            set { FechaOrdendeCompra = value; }
        }

        public int Remision1
        {
            get { return Remision; }
            set { Remision = value; }
        }


        public int CodigoCDI1
        {
            get { return CodigoCDI; }
            set { CodigoCDI = value; }
        }

        public string NombreCDI1
        {
            get { return NombreCDI; }
            set { NombreCDI = value; }
        }

        public int Codigo_SKU1
        {
            get { return Codigo_SKU; }
            set { Codigo_SKU = value; }
        }

        public string Descripcion_SKU1
        {
            get { return Descripcion_SKU; }
            set { Descripcion_SKU = value; }
        }

        public int Presentacion1
        {
            get { return Presentacion; }
            set { Presentacion = value; }
        }


        public string Unidad1
        {
            get { return Unidad; }
            set { Unidad = value; }
        }

        public int Cantidad1
        {
            get { return Cantidad; }
            set { Cantidad = value; }
        }

        public int ordenada1
        {
            get { return ordenada; }
            set { ordenada = value; }
        }
        public decimal Costo_AAA1
        {
            get { return Costo_AAA; }
            set { Costo_AAA = value; }
        }
        public decimal Importe_Total_AAA1
        {
            get { return Importe_Total_AAA; }
            set { Importe_Total_AAA = value; }
        }
        public int Num_OrdendeCompra1
        {
            get { return Num_OrdendeCompra; }
            set { Num_OrdendeCompra = value; }
        }
        #endregion
    }
}

