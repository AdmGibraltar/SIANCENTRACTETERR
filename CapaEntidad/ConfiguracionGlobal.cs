using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ConfiguracionGlobal
    {
        private Int32 _Id_Cd = 0;
        private bool _Solicitud_Prospecto = false;
        private string _Hora_Zona = "";
        private bool _Hora_Verano = false;
        private string _Mail_Servidor = "";
        private string _Mail_Usuario = "";
        private string _Mail_Contraseña = "";
        private string _Mail_Puerto = "";
        private string _Mail_Remitente = "";
        private string _Login_Intentos = "";
        private string _Login_Tiempo_Bloqueo = "";
        private string _Contraseña_Tiempo_Vida = "";
        private string _Contraseña_Long_Min = "";
        private string _Mail_CompLocal = "";
        private string _Mail_PrecioEsp;
        private string _Mail_BaseInstalada;
        private string _Mail_Acys;
        private Int32 _Id_Conf;
        private int _Id_Emp;
        private string _Mail_Valuacion;
        private double _Mail_MinValuacion;
        private bool _Remisiones_Especiales = false;


        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        public Int32 Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        public bool Solicitud_Prospecto
        {
            get { return _Solicitud_Prospecto; }
            set { _Solicitud_Prospecto = value; }
        }
        public string Hora_Zona
        {
            get { return _Hora_Zona.Trim(); }
            set { _Hora_Zona = value.Trim(); }
        }
        public bool Hora_Verano
        {
            get { return _Hora_Verano; }
            set { _Hora_Verano = value; }
        }
        public string Mail_Servidor
        {
            get { return _Mail_Servidor.Trim(); }
            set { _Mail_Servidor = value.Trim(); }
        }
        public string Mail_Usuario
        {
            get { return _Mail_Usuario.Trim(); }
            set { _Mail_Usuario = value.Trim(); }
        }
        public string Mail_Contraseña
        {
            get { return _Mail_Contraseña.Trim(); }
            set { _Mail_Contraseña = value.Trim(); }
        }
        public string Mail_Puerto
        {
            get { return _Mail_Puerto.Trim(); }
            set { _Mail_Puerto = value.Trim(); }
        }
        public string Mail_Remitente
        {
            get { return _Mail_Remitente.Trim(); }
            set { _Mail_Remitente = value.Trim(); }
        }
        public string Login_Intentos
        {
            get { return _Login_Intentos.Trim(); }
            set { _Login_Intentos = value.Trim(); }
        }
        public string Login_Tiempo_Bloqueo
        {
            get { return _Login_Tiempo_Bloqueo.Trim(); }
            set { _Login_Tiempo_Bloqueo = value.Trim(); }
        }
        public string Contraseña_Tiempo_Vida
        {
            get { return _Contraseña_Tiempo_Vida.Trim(); }
            set { _Contraseña_Tiempo_Vida = value.Trim(); }
        }
        public string Contraseña_Long_Min
        {
            get { return _Contraseña_Long_Min.Trim(); }
            set { _Contraseña_Long_Min = value.Trim(); }
        }
        public Int32 Id_Conf
        {
            get { return _Id_Conf; }
            set { _Id_Conf = value; }
        }
        public string Mail_CompLocal
        {
            get { return _Mail_CompLocal; }
            set { _Mail_CompLocal = value; }
        }
        public string Mail_PrecioEsp
        {
            get { return _Mail_PrecioEsp; }
            set { _Mail_PrecioEsp = value; }
        }


        public string Mail_BaseInstalada
        {
            get { return _Mail_BaseInstalada; }
            set { _Mail_BaseInstalada = value; }
        }

        public string Mail_Acys
        {
            get { return _Mail_Acys; }
            set { _Mail_Acys = value; }
        }


        public string Mail_Valuacion
        {
            get { return _Mail_Valuacion; }
            set { _Mail_Valuacion = value; }
        }

        public double Mail_MinValuacion
        {
            get { return _Mail_MinValuacion; }
            set { _Mail_MinValuacion = value; }
        }

        //jfcv 26oct2016 agregar configuración para remisiones especiales
        public bool Remisiones_Especiales
        {
            get { return _Remisiones_Especiales; }
            set { _Remisiones_Especiales = value; }
        }

        public string Mail_GastosProveedores { get; set; }
        public string Mail_GastosAcreedores { get; set; }
        public string Mail_GastosComprasLocales { get; set; }
        public string Mail_GastosFletes { get; set; }
        public string Mail_GastosNoInventariables { get; set; }
        public string Mail_GastosPagoServicios { get; set; }
        public string Mail_GastosOtrosPagos { get; set; }
        public string Mail_GastosReposicionCaja { get; set; }
        public string Mail_GastosCuentaGastos { get; set; }
        public string Mail_GastosComprobacion { get; set; }
        public string Mail_GastosAvisoGerente { get; set; }
        public string Mail_GastosAvisoUsuario { get; set; }
        //jfcv 12 sep 2016
        public string Mail_GastosHonorarios { get; set; }
        public string Mail_GastosArrendamientos { get; set; }
        //jfcv 14 oct 2016
        public string Mail_AutorizaReFacturas { get; set; }
        public string Mail_ResponsableReFacturas { get; set; }
        //jfcv 2 ene 2016 
        public string RutaSistemaGastos { get; set; }
                

    }
}
