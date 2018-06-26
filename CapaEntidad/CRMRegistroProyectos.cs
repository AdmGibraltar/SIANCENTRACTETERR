using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class CRMRegistroProyectos
    {
        #region variables
        int uen;
        int segmento;
        int territorio;
        int area;
        int solucion;
        int aplicacion;
        int cliente;
        bool ventaNoRepetitiva;
        string comentarios;
        string productos;
        DateTime analisis;
        DateTime presentacion;
        DateTime negociacion;
        DateTime cancelacion;
        DateTime fechaCotizacion;
        double ventaPromedio;
        double valorPotencialO;
        double valorPotencialT;
        int estatus;
        int idMax;
        int idCausa;
        string competidor;
        string comentariosCancel;
        private int? _Id_Op;
        private DateTime _Cierre;
        private int _Id_Causa;
        private int _Id_Cam;
        

        public int Id_Causa
        {
            get { return _Id_Causa; }
            set { _Id_Causa = value; }
        }

         public int Id_Cam         
        {
            get { return _Id_Cam; }
            set { _Id_Cam = value; }
        }

        public DateTime Cierre
        {
            get { return _Cierre; }
            set { _Cierre = value; }
        }

        public int? Id_Op
        {
            get { return _Id_Op; }
            set { _Id_Op = value; }
        }
        #endregion

        #region factorizacion
        public int Uen
        {
            get { return uen; }
            set { uen = value; }
        }
        public int Segmento
        {
            get { return segmento; }
            set { segmento = value; }
        }
        public int Territorio
        {
            get { return territorio; }
            set { territorio = value; }
        }
        public int Area
        {
            get { return area; }
            set { area = value; }
        }
        public int Solucion
        {
            get { return solucion; }
            set { solucion = value; }
        }
        public int Aplicacion
        {
            get { return aplicacion; }
            set { aplicacion = value; }
        }
        public int Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }
        public bool VentaNoRepetitiva
        {
            get { return ventaNoRepetitiva; }
            set { ventaNoRepetitiva = value; }
        }
        public string Comentarios
        {
            get { return comentarios; }
            set { comentarios = value; }
        }
        public string Productos
        {
            get { return productos; }
            set { productos = value; }
        }
        public DateTime Analisis
        {
            get { return analisis; }
            set { analisis = value; }
        }
        public DateTime Presentacion
        {
            get { return presentacion; }
            set { presentacion = value; }
        }
        public DateTime Negociacion
        {
            get { return negociacion; }
            set { negociacion = value; }
        }
        public DateTime Cancelacion
        {
            get { return cancelacion; }
            set { cancelacion = value; }
        }
        public DateTime FechaCotizacion
        {
            get { return fechaCotizacion; }
            set { fechaCotizacion = value; }
        }
        public double VentaPromedio
        {
            get { return ventaPromedio; }
            set { ventaPromedio = value; }
        }
        public double ValorPotencialO
        {
            get { return valorPotencialO; }
            set { valorPotencialO = value; }
        }
        public double ValorPotencialT
        {
            get { return valorPotencialT; }
            set { valorPotencialT = value; }
        }
        public int Estatus
        {
            get { return estatus; }
            set { estatus = value; }
        }
        public int IdMax
        {
            get { return idMax; }
            set { idMax = value; }
        }
        public int IdCausa
        {
            get { return idCausa; }
            set { idCausa = value; }
        }
        public string Competidor
        {
            get { return competidor; }
            set { competidor = value; }
        }
        public string ComentariosCancel
        {
            get { return comentariosCancel; }
            set { comentariosCancel = value; }
        }
        #endregion
    }
}
