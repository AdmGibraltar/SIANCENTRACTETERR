using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CapaEntidad
{
    public class Pedido
    {
        private int _Id_Emp;
        private int _Id_Cd;
        int _Id_Ped;
        DateTime _Ped_Fecha;
        int _Id_Cte;
        string _Cte_NomComercial;
        int _Id_Ter;
        int _Id_Rik;
        int? _Id_Fac;
        string _Pedido_del;
        string _Requisicion;
        string _Ped_Solicito;
        string _Ped_Flete;
        string _Ped_OrdenEntrega;
        int _Ped_CondEntrega;
        //bool _Ped_ABC;
        DateTime _Ped_FechaEntrega;
        string _Ped_Observaciones;
        double _Ped_DescPorcen1;
        string _Ped_Desc1;
        double _Ped_DescPorcen2;
        string _Ped_Desc2;
        string _Ped_Comentarios;
        double _Ped_Importe;
        double _Ped_Subtotal;
        double _Ped_Iva;
        double _Ped_Total;
        string _Estatus;
        int _Id_U;
        string _U_Nombre;
        int _Ped_Tipo;
        int cant_Facturada;
        private string _Ped_TipoStr;
        private bool _Facturacion;
        private string _EstatusStr;
        private bool _Credito;
        private string _CreditoStr;
        private DateTime _ped_FechaAut;
        public string Filtro_Nombre;
        private string _Filtro_CteIni;
        
        public string U_Nombre
        {
            get { return _U_Nombre; }
            set { _U_Nombre = value; }
        }
        public string Filtro_CteIni
        {
            get { return _Filtro_CteIni; }
            set { _Filtro_CteIni = value; }
        }
        private string _Filtro_CteFin;

        public string Filtro_CteFin
        {
            get { return _Filtro_CteFin; }
            set { _Filtro_CteFin = value; }
        }
        private DateTime? _Filtro_FecIni;

        public DateTime? Filtro_FecIni
        {
            get { return _Filtro_FecIni; }
            set { _Filtro_FecIni = value; }
        }
        private DateTime? _Filtro_FecFin;

        public DateTime? Filtro_FecFin
        {
            get { return _Filtro_FecFin; }
            set { _Filtro_FecFin = value; }
        }
        private double? _Filtro_PedIni;

        public double? Filtro_PedIni
        {
            get { return _Filtro_PedIni; }
            set { _Filtro_PedIni = value; }
        }
        private double? _Filtro_PedFin;
        private DateTime _FechaAsignacion;
        private string _Filtro_usuario;
        private string _Filtro_Tipo;

        public string Filtro_Tipo
        {
            get { return _Filtro_Tipo; }
            set { _Filtro_Tipo = value; }
        }
        private string _Filtro_Estatus;
        public string Rik_Nombre;
        public string Ter_Nombre;
        public string Ped_SolicitoTel;
        public string Ped_SolicitoEmail;
        public string Ped_SolicitoPuesto;
        public string Ped_ConsignadoCalle;
        public string Ped_ConsignadoNo;
        public string Ped_ConsignadoCp;
        public string Ped_ConsignadoMunicipio;
        public string Ped_ConsignadoEstado;
        public string Ped_ConsignadoColonia;
        public bool Ped_ReqOrden;
        public string Ped_OrdenCompra;
        public int Ped_AcysSemana;
        public int Ped_AcysAnio;
        public int Id_Acs;
        
        private bool _Ped_Captacion = false;
        public string Filtro_Doc;

        public bool Ped_Captacion
        {
            get { return _Ped_Captacion; }
            set { _Ped_Captacion = value; }
        }

        public string Filtro_Estatus
        {
            get { return _Filtro_Estatus; }
            set { _Filtro_Estatus = value; }
        }
        

        public string Filtro_usuario
        {
            get { return _Filtro_usuario; }
            set { _Filtro_usuario = value; }
        }

        public DateTime FechaAsignacion
        {
            get { return _FechaAsignacion; }
            set { _FechaAsignacion = value; }
        }

        public double? Filtro_PedFin
        {
            get { return _Filtro_PedFin; }
            set { _Filtro_PedFin = value; }
        }


        public string EstatusStr
        {
            get { return _EstatusStr; }
            set { _EstatusStr = value; }
        }
        public bool Facturacion
        {
            get { return _Facturacion; }
            set { _Facturacion = value; }
        }
        public string Ped_TipoStr
        {
            get { return _Ped_TipoStr; }
            set { _Ped_TipoStr = value; }
        }
        public int Ped_Tipo
        {
            get { return _Ped_Tipo; }
            set { _Ped_Tipo = value; }
        }

        public int Cant_Facturada
        {
            get { return cant_Facturada; }
            set { cant_Facturada = value; }
        }
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }
        public int Id_Ped
        {
            get { return _Id_Ped; }
            set { _Id_Ped = value; }
        }
        public DateTime Ped_Fecha
        {
            get { return _Ped_Fecha; }
            set { _Ped_Fecha = value; }
        }
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }
        public string Cte_NomComercial
        {
            get { return _Cte_NomComercial; }
            set { _Cte_NomComercial = value; }
        }
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }
        public int? Id_Fac
        {
            get { return _Id_Fac; }
            set { _Id_Fac = value; }
        }
        public string Pedido_del
        {
            get { return _Pedido_del; }
            set { _Pedido_del = value; }
        }
        public string Requisicion
        {
            get { return _Requisicion; }
            set { _Requisicion = value; }
        }
        public string Ped_Solicito
        {
            get { return _Ped_Solicito; }
            set { _Ped_Solicito = value; }
        }
        public string Ped_Flete
        {
            get { return _Ped_Flete; }
            set { _Ped_Flete = value; }
        }
        public string Ped_OrdenEntrega
        {
            get { return _Ped_OrdenEntrega; }
            set { _Ped_OrdenEntrega = value; }
        }
        public int Ped_CondEntrega
        {
            get { return _Ped_CondEntrega; }
            set { _Ped_CondEntrega = value; }
        }
        public DateTime Ped_FechaEntrega
        {
            get { return _Ped_FechaEntrega; }
            set { _Ped_FechaEntrega = value; }
        }
        public string Ped_Observaciones
        {
            get { return _Ped_Observaciones; }
            set { _Ped_Observaciones = value; }
        }
        public double Ped_DescPorcen1
        {
            get { return _Ped_DescPorcen1; }
            set { _Ped_DescPorcen1 = value; }
        }
        public string Ped_Desc1
        {
            get { return _Ped_Desc1; }
            set { _Ped_Desc1 = value; }
        }
        public double Ped_DescPorcen2
        {
            get { return _Ped_DescPorcen2; }
            set { _Ped_DescPorcen2 = value; }
        }
        public string Ped_Desc2
        {
            get { return _Ped_Desc2; }
            set { _Ped_Desc2 = value; }
        }
        public string Ped_Comentarios
        {
            get { return _Ped_Comentarios; }
            set { _Ped_Comentarios = value; }
        }
        public double Ped_Importe
        {
            get { return _Ped_Importe; }
            set { _Ped_Importe = value; }
        }
        public double Ped_Subtotal
        {
            get { return _Ped_Subtotal; }
            set { _Ped_Subtotal = value; }
        }
        public double Ped_Iva
        {
            get { return _Ped_Iva; }
            set { _Ped_Iva = value; }
        }
        public double Ped_Total
        {
            get { return _Ped_Total; }
            set { _Ped_Total = value; }
        }
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
        public string Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }
        public bool Credito
        {
            get { return _Credito; }
            set { _Credito = value; }
        }
        public string CreditoStr
        {
            get { return _CreditoStr; }
            set { _CreditoStr = value; }
        }

        public DateTime Ped_FechaAut
        {
            get { return _ped_FechaAut; }
            set { _ped_FechaAut = value; }
        }


    }
         
}
