using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ClienteTerritorio
    {
        #region Cliente-Territorio
        private int folio;

        public int Folio
        {
            get { return folio; }
            set { folio = value; }
        }
        private int id_Solicitud;

        public int Id_Solicitud
        {
            get { return id_Solicitud; }
            set { id_Solicitud = value; }
        }
        private int id_Emp;

        public int Id_Emp
        {
            get { return id_Emp; }
            set { id_Emp = value; }
        }
        private int id_Cd;

        public int Id_Cd
        {
            get { return id_Cd; }
            set { id_Cd = value; }
        }

        private string nom_Sucursal;

        public string Nom_Sucursal
        {
            get { return nom_Sucursal; }
            set { nom_Sucursal = value; }
        }

        private int id_Cte;

        public int Id_Cte
        {
            get { return id_Cte; }
            set { id_Cte = value; }
        }
        private string nom_Cliente;

        public string Nom_Cliente
        {
            get { return nom_Cliente; }
            set { nom_Cliente = value; }
        }
        private int id_Ter;

        public int Id_Ter
        {
            get { return id_Ter; }
            set { id_Ter = value; }
        }
        private string nom_Territorio;

        public string Nom_Territorio
        {
            get { return nom_Territorio; }
            set { nom_Territorio = value; }
        }
        private double? dimension;

        public double? Dimension
        {
            get { return dimension; }
            set { dimension = value; }
        }
        private double? pesos;

        public double? Pesos
        {
            get { return pesos; }
            set { pesos = value; }
        }
        private double? potencial;

        public double? Potencial
        {
            get { return potencial; }
            set { potencial = value; }
        }
        private double? manodeObra;

        public double? ManodeObra
        {
            get { return manodeObra; }
            set { manodeObra = value; }
        }
        private double? gastosTerritorio;

        public double? GastosTerritorio
        {
            get { return gastosTerritorio; }
            set { gastosTerritorio = value; }
        }
        private double? fletesPagadoCliente;

        public double? FletesPagadoCliente
        {
            get { return fletesPagadoCliente; }
            set { fletesPagadoCliente = value; }
        }
        private double? porcentaje;

        public double? Porcentaje
        {
            get { return porcentaje; }
            set { porcentaje = value; }
        }
        private bool activo;

        public bool Activo
        {
            get { return activo; }
            set { activo = value; }
        }
        private bool nuevo;

        public bool Nuevo
        {
            get { return nuevo; }
            set { nuevo = value; }
        }
        private string comentarios;

        public string Comentarios
        {
            get { return comentarios; }
            set { comentarios = value; }
        }
        private DateTime? fec_Solicitud;

        public DateTime? Fec_Solicitud
        {
            get { return fec_Solicitud; }
            set { fec_Solicitud = value; }
        }
        private DateTime? fec_Atendida;

        public DateTime? Fec_Atendida
        {
            get { return fec_Atendida; }
            set { fec_Atendida = value; }
        }

        private int id_Version;

        public int Id_Version
        {
            get { return id_Version; }
            set { id_Version = value; }
        }
        private string estatus;

        public string Estatus
        {
            get { return estatus; }
            set { estatus = value; }
        }
        #endregion

        #region Cliente-Territorio Anterior
        //Cliente-Territorio Anterior 
        private int? id_Ter1;

        public int? Id_Ter1
        {
            get { return id_Ter1; }
            set { id_Ter1 = value; }
        }
        private string nom_Territorio1;

        public string Nom_Territorio1
        {
            get { return nom_Territorio1; }
            set { nom_Territorio1 = value; }
        }
        private double? dimension1;

        public double? Dimension1
        {
            get { return dimension1; }
            set { dimension1 = value; }
        }
        private double? pesos1;

        public double? Pesos1
        {
            get { return pesos1; }
            set { pesos1 = value; }
        }
        private double? potencial1;

        public double? Potencial1
        {
            get { return potencial1; }
            set { potencial1 = value; }
        }
        private double? manodeObra1;

        public double? ManodeObra1
        {
            get { return manodeObra1; }
            set { manodeObra1 = value; }
        }
        private double? gastosTerritorio1;

        public double? GastosTerritorio1
        {
            get { return gastosTerritorio1; }
            set { gastosTerritorio1 = value; }
        }
        private double? fletesPagadoCliente1;

        public double? FletesPagadoCliente1
        {
            get { return fletesPagadoCliente1; }
            set { fletesPagadoCliente1 = value; }
        }
        private double? porcentaje1;

        public double? Porcentaje1
        {
            get { return porcentaje1; }
            set { porcentaje1 = value; }
        }

        private bool nuevo1;

        public bool Nuevo1
        {
            get { return nuevo1; }
            set { nuevo1 = value; }
        }

        private bool activo1;

        public bool Activo1
        {
            get { return activo1; }
            set { activo1 = value; }
        }

        #endregion
    }
}
