using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Movimientos
    {
        int _Id_Emp;
        int _Id;
        string _Nombre;
        int _Tipo;
        int _Naturaleza;
        int _Inverso;
        bool _AfeIVA;
        bool _AfeVta;
        bool _AfeOrdComp;
        int _Afecta;
        bool _Estatus;
        string _EstatusStr;
        int _NatMov;
        bool _ReqReferencia;
        bool _ReqSispropietario;

        public bool ReqSispropietario
        {
            get { return _ReqSispropietario; }
            set { _ReqSispropietario = value; }
        }

        public bool ReqReferencia
        {
            get { return _ReqReferencia; }
            set { _ReqReferencia = value; }
        }

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        public int Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        public int Naturaleza
        {
            get { return _Naturaleza; }
            set { _Naturaleza = value; }
        }
        public int Inverso
        {
            get { return _Inverso; }
            set { _Inverso = value; }
        }
        public bool AfeIVA
        {
            get { return _AfeIVA; }
            set { _AfeIVA = value; }
        }
        public bool AfeVta
        {
            get { return _AfeVta; }
            set { _AfeVta = value; }
        }
        public bool AfeOrdComp
        {
            get { return _AfeOrdComp; }
            set { _AfeOrdComp = value; }
        }
        public int Afecta
        {
            get { return _Afecta; }
            set { _Afecta = value; }
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
        public int NatMov
        {
            get { return _NatMov; }
            set { _NatMov = value; }
        }


        public int Id_TAc { get; set; }
        public int Id_Tm { get; set; }
        public bool TAc_NatMov { get; set; }
        public bool TAc_Naturaleza { get; set; }
        public string TAc_Cuenta { get; set; }
        public string TAc_Subcuenta { get; set; }
        public string TAc_Subsubcuenta { get; set; }
        public string TAc_CuentaA { get; set; }
        public string TAc_SubcuentaA { get; set; }
        public string TAc_SubsubcuentaA { get; set; }
        public string TAc_CuentaB { get; set; }
        public string TAc_SubcuentaB { get; set; }
        public string TAc_SubsubcuentaB { get; set; }
        public string TAc_CuentaStr { get; set; }
        public string TAc_SubcuentaStr { get; set; }
        public string TAc_Naturalezastr { get; set; }
        public string TAc_TipoStr { get; set; }
        public bool TAc_CC { get; set; }

    }
}
