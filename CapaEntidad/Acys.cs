using System;

namespace CapaEntidad
{
    public class Acys
    {
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_Ter;
        private int _Id_Cte;
        private int _Id_Rik;
        private int _Id_Acs;
        private string _Acs_Estatus;
        private string _Cte_Nombre;
        private DateTime _Acs_Fecha;
        private DateTime _Acs_FechaInicioDocumento;
        private DateTime _Acs_FechaFinDocumento;
        private string _Acs_EstatusStr;
        private DateTime? _Filtro_FecIni;
        private DateTime? _Filtro_FecFin;
        private string _Filtro_Estatus;
        private string _Filtro_FolIni;
        private string _Filtro_FolFin;
        private string _Filtro_usuario;
        private int _id_Rik;
        private string _Acs_Contacto;
        private string _Acs_Puesto;
        private int _Acs_Telefono;
        private string _Acs_Correo;
        private string _Acs_Contacto2;
        private int _Acs_Telefono2;
        private string _Acs_Correo2;
        private string _Acs_Contacto3;
        private int _Acs_Telefono3;
        private string _Acs_Correo3;
        private string _Acs_Contacto4;
        private int _Acs_Telefono4;
        private string _Acs_Correo4;
        private string _Acs_Contacto5;
        private string _ClienteDireccion;                
        private string _ClienteColonia;
        private string _ClienteMunicipio;
        private string _ClienteEstado;
        private string _ClienteRFC;
        private string _ClienteCodPost;        
        private bool _CuentaCorporativa;
        private bool _AddendaSI;

        private string _DireccionEntrega;
        private string _ClienteColoniaE;
        private string _ClienteMunicipioE;
        private string _ClienteEstadoE;
        private string _ClienteCPE;


        private string _Acs_PedidoEncargadoEnviar;
        private string _Acs_PedidoPuesto;
        private string _Acs_PedidoTelefono;
        private string _Acs_PedidoEmail;

        
        private bool _Acs_RecDocReposicion;
        private bool _Acs_RecDocFolio;
        private string _Acs_RecDocOtro;

        private bool _Cte_Contado;
        private bool Cte_TarjetaDebito;
        private bool CteTarjetaCredito;        
        private bool CteDeposito;

        private string _Acs_VisitaOtro;
        private bool _Acs_ReqServAsesoria;
        private bool _Acs_ReqServTecnicoRelleno;
        private bool _Acs_ReqServMantenimiento;
        
        private string _Acs_Notas;
              
        private int _Acs_ContactoRepVenta;
        private string _Acs_ContactoRepVentaTel;
        private string _Acs_ContactoRepVentaEmail;

        private int _Acs_ContactoRepServ;
        private string _Acs_ContactoRepServTel;
        private string _Acs_ContactoRepServEmail;

        private int _Acs_ContactoJefServ;
        private string _Acs_ContactoJefServTel;
        private string _Acs_ContactoJefServEmail;

        private int _Acs_ContactoAseServ;
        private string _Acs_ContactoAseServTel;
        private string _Acs_ContactoAseServEmail;


        private int _Acs_ContactoJefOper;
        private string _Acs_ContactoJefOperTel;
        private string _Acs_ContactoJefOperEmail;

        private int _Acs_ContactoCAlmRep;
        private string _Acs_ContactoCAlmRepTel;
        private string _Acs_ContactoCAlmRepEmail;

        private int _Acs_ContactoCServTec;
        private string _Acs_ContactoCServTecTel;
        private string _Acs_ContactoCServTecEmail;

        private int _Acs_ContactoCCreCob;
        private string _Acs_ContactoCCreCobTel;
        private string _Acs_ContactoCCreCobEmail;

        private string _Acs_Unique;
        private int _Acs_Solicitar;
        private int _Acs_Sustituye;
        private string _Acs_Vencido;

        public string Acs_Unique
        {
            get { return _Acs_Unique; }
            set { _Acs_Unique = value; }
        }

        public string Acs_Vencido
        {
            get { return _Acs_Vencido; }
            set { _Acs_Vencido = value; }
        }
        
        public int Acs_Solicitar
        {
            get { return _Acs_Solicitar; }
            set { _Acs_Solicitar = value; }
        }

        public int Acs_Sustituye
        {
            get { return _Acs_Sustituye; }
            set { _Acs_Sustituye = value; }
        }


        public string Acs_PedidoEncargadoEnviar
        {
            get { return _Acs_PedidoEncargadoEnviar; }
            set { _Acs_PedidoEncargadoEnviar = value; }
        }

        public string Acs_Notas
        {
            get { return _Acs_Notas; }
            set { _Acs_Notas = value; }
        }


        public string Acs_PedidoPuesto
        {
            get { return _Acs_PedidoPuesto; }
            set { _Acs_PedidoPuesto = value; }
        }
      

        public string Acs_PedidoEmail
        {
            get { return _Acs_PedidoEmail; }
            set { _Acs_PedidoEmail = value; }
        }



        public string Acs_PedidoTelefono
        {
            get { return _Acs_PedidoTelefono; }
            set { _Acs_PedidoTelefono = value; }
        }                     
        
        
        public bool Acs_RecDocReposicion
        {
            get { return _Acs_RecDocReposicion; }
            set { _Acs_RecDocReposicion = value; }
        }


        public bool Acs_RecDocFolio
        {
            get { return _Acs_RecDocFolio; }
            set { _Acs_RecDocFolio = value; }
        }


        public string Acs_RecDocOtro
        {
            get { return _Acs_RecDocOtro; }
            set { _Acs_RecDocOtro = value; }
        }
           


        

        public string Acs_VisitaOtro
        {
            get { return _Acs_VisitaOtro; }
            set { _Acs_VisitaOtro = value; }
        }

        public bool Acs_ReqServAsesoria
        {
            get { return _Acs_ReqServAsesoria; }
            set { _Acs_ReqServAsesoria = value; }
        }

        public bool Acs_ReqServTecnicoRelleno
        {
            get { return _Acs_ReqServTecnicoRelleno; }
            set { _Acs_ReqServTecnicoRelleno = value; }
        }

        public bool Acs_ReqServMantenimiento
        {
            get { return _Acs_ReqServMantenimiento; }
            set { _Acs_ReqServMantenimiento = value; }
        }


        public int Acs_ContactoRepVenta
        {
            get { return _Acs_ContactoRepVenta; }
            set { _Acs_ContactoRepVenta = value; }
        }

        public string Acs_ContactoRepVentaTel
        {
            get { return _Acs_ContactoRepVentaTel; }
            set { _Acs_ContactoRepVentaTel = value; }
        }

        public string Acs_ContactoRepVentaEmail
        {
            get { return _Acs_ContactoRepVentaEmail; }
            set { _Acs_ContactoRepVentaEmail = value; }
        }

        public int Acs_ContactoRepServ
        {
            get { return _Acs_ContactoRepServ; }
            set { _Acs_ContactoRepServ = value; }
        }

        public string Acs_ContactoRepServTel
        {
            get { return _Acs_ContactoRepServTel; }
            set { _Acs_ContactoRepServTel = value; }
        }

        public string Acs_ContactoRepServEmail
        {
            get { return _Acs_ContactoRepServEmail; }
            set { _Acs_ContactoRepServEmail = value; }
        }


        public int Acs_ContactoJefServ
        {
            get { return _Acs_ContactoJefServ; }
            set { _Acs_ContactoJefServ = value; }
        }

        public string Acs_ContactoJefServTel
        {
            get { return _Acs_ContactoJefServTel; }
            set { _Acs_ContactoJefServTel = value; }
        }

        public string Acs_ContactoJefServEmail
        {
            get { return _Acs_ContactoJefServEmail; }
            set { _Acs_ContactoJefServEmail = value; }
        }

        public int Acs_ContactoAseServ
        {
            get { return _Acs_ContactoAseServ; }
            set { _Acs_ContactoAseServ = value; }
        }

        public string Acs_ContactoAseServTel
        {
            get { return _Acs_ContactoAseServTel; }
            set { _Acs_ContactoAseServTel = value; }
        }

        public string Acs_ContactoAseServEmail
        {
            get { return _Acs_ContactoAseServEmail; }
            set { _Acs_ContactoAseServEmail = value; }
        }

        public int Acs_ContactoJefOper
        {
            get { return _Acs_ContactoJefOper; }
            set { _Acs_ContactoJefOper = value; }
        }

        public string Acs_ContactoJefOperTel
        {
            get { return _Acs_ContactoJefOperTel; }
            set { _Acs_ContactoJefOperTel = value; }
        }

        public string Acs_ContactoJefOperEmail
        {
            get { return _Acs_ContactoJefOperEmail; }
            set { _Acs_ContactoJefOperEmail = value; }
        }

        public int Acs_ContactoCAlmRep
        {
            get { return _Acs_ContactoCAlmRep; }
            set { _Acs_ContactoCAlmRep = value; }
        }

        public string Acs_ContactoCAlmRepTel
        {
            get { return _Acs_ContactoCAlmRepTel; }
            set { _Acs_ContactoCAlmRepTel = value; }
        }

        public string Acs_ContactoCAlmRepEmail
        {
            get { return _Acs_ContactoCAlmRepEmail; }
            set { _Acs_ContactoCAlmRepEmail = value; }
        }


        public int Acs_ContactoCServTec
        {
            get { return _Acs_ContactoCServTec; }
            set { _Acs_ContactoCServTec = value; }
        }

        public string Acs_ContactoCServTecTel
        {
            get { return _Acs_ContactoCServTecTel; }
            set { _Acs_ContactoCServTecTel = value; }
        }

        public string Acs_ContactoCServTecEmail
        {
            get { return _Acs_ContactoCServTecEmail; }
            set { _Acs_ContactoCServTecEmail = value; }
        }

        public int Acs_ContactoCCreCob
        {
            get { return _Acs_ContactoCCreCob; }
            set { _Acs_ContactoCCreCob = value; }
        }

        public string Acs_ContactoCCreCobTel
        {
            get { return _Acs_ContactoCCreCobTel; }
            set { _Acs_ContactoCCreCobTel = value; }
        }

        public string Acs_ContactoCCreCobEmail
        {
            get { return _Acs_ContactoCCreCobEmail; }
            set { _Acs_ContactoCCreCobEmail = value; }
        }
        
        public string DireccionEntrega
        {
            get { return _DireccionEntrega; }
            set { _DireccionEntrega= value; }
        }
        
         public string ClienteColoniaE
        {
            get { return _ClienteColoniaE; }
            set { _ClienteColoniaE = value; }
        }

         public string ClienteMunicipioE
        {
            get { return _ClienteMunicipioE; }
            set { _ClienteMunicipioE = value; }
        }

         public string ClienteEstadoE
         {
             get { return _ClienteEstadoE; }
             set { _ClienteEstadoE = value; }
         }


         public string ClienteCPE
         {
             get { return _ClienteCPE; }
             set { _ClienteCPE = value; }
         }




         public string ClienteDireccion
        {
            get { return _ClienteDireccion; }
            set { _ClienteDireccion = value; }
        }
        
         public string ClienteColonia
        {
            get { return _ClienteColonia; }
            set { _ClienteColonia = value; }
        }

         public string ClienteMunicipio
        {
            get { return _ClienteMunicipio; }
            set { _ClienteMunicipio = value; }
        }
        
         public string ClienteEstado
        {
            get { return _ClienteEstado; }
            set { _ClienteEstado = value; }
        }

        public string ClienteRFC
        {
            get { return _ClienteRFC; }
            set { _ClienteRFC = value; }
        }

        public string ClienteCodPost
        {
            get { return _ClienteEstado; }
            set { _ClienteEstado = value; }
        }

        public bool CuentaCorporativa
        {
            get { return _CuentaCorporativa; }
            set { _CuentaCorporativa = value; }
        }

        public bool AddendaSI
        {
            get { return _AddendaSI; }
            set { _AddendaSI = value; }
        }

       
        
         public string Acs_Contacto5
        {
            get { return _Acs_Contacto5; }
            set { _Acs_Contacto5 = value; }
        }



      
        private int _Acs_Telefono5;
        private string _Acs_Correo5;
        private string _Acs_Contacto6;
        private int _Acs_Telefono6;

        public int Acs_Telefono6
        {
            get { return _Acs_Telefono6; }
            set { _Acs_Telefono6 = value; }
        }
        private string _Acs_Correo6;
        private string _Acs_Proveedor;
        private int _Acs_RutaEntrega;
        private int _Acs_RutaServicio;
        private bool _Acs_ReqOrdenCompra;
        private DateTime? _Acs_VigenciaIni;
        private int _Acs_Semana;
        private bool _Acs_ReqConfirmacion;
        private bool _Acs_RecPedCorreo;
        private bool _Acs_RecPedFax;
        private bool _Acs_RecPedTel;
        private bool _Acs_RecPedRep;
        private bool _Acs_RecPedOtro;
        private string _Acs_RecPedOtroStr;
        private int _Id_U;
       

        public bool Acs_RecPedRep
        {
            get { return _Acs_RecPedRep; }
            set { _Acs_RecPedRep = value; }
        }
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
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
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }
        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }
        public int Id_Acs
        {
            get { return _Id_Acs; }
            set { _Id_Acs = value; }
        }
        public string Acs_Estatus
        {
            get { return _Acs_Estatus; }
            set { _Acs_Estatus = value; }
        }
        public string Cte_Nombre
        {
            get { return _Cte_Nombre; }
            set { _Cte_Nombre = value; }
        }
        public DateTime Acs_Fecha
        {
            get { return _Acs_Fecha; }
            set { _Acs_Fecha = value; }
        }

        public DateTime Acs_FechaInicioDocumento
        {
            get { return _Acs_FechaInicioDocumento; }
            set { _Acs_FechaInicioDocumento = value; }
        }

        public DateTime Acs_FechaFinDocumento
        {
            get { return _Acs_FechaFinDocumento; }
            set { _Acs_FechaFinDocumento = value; }
        }


        public string Acs_EstatusStr
        {
            get { return _Acs_EstatusStr; }
            set { _Acs_EstatusStr = value; }
        }
        public DateTime? Filtro_FecIni
        {
            get { return _Filtro_FecIni; }
            set { _Filtro_FecIni = value; }
        }
        public DateTime? Filtro_FecFin
        {
            get { return _Filtro_FecFin; }
            set { _Filtro_FecFin = value; }
        }
        public string Filtro_Estatus
        {
            get { return _Filtro_Estatus; }
            set { _Filtro_Estatus = value; }
        }
        public string Filtro_FolIni
        {
            get { return _Filtro_FolIni; }
            set { _Filtro_FolIni = value; }
        }
        public string Filtro_FolFin
        {
            get { return _Filtro_FolFin; }
            set { _Filtro_FolFin = value; }
        }
        public string Filtro_usuario
        {
            get { return _Filtro_usuario; }
            set { _Filtro_usuario = value; }
        }
        public int Id_Rik1
        {
            get { return _id_Rik; }
            set { _id_Rik = value; }
        }
        public string Acs_Contacto
        {
            get { return _Acs_Contacto; }
            set { _Acs_Contacto = value; }
        }
        public string Acs_Puesto
        {
            get { return _Acs_Puesto; }
            set { _Acs_Puesto = value; }
        }
        public int Acs_Telefono
        {
            get { return _Acs_Telefono; }
            set { _Acs_Telefono = value; }
        }
        public string Acs_Correo
        {
            get { return _Acs_Correo; }
            set { _Acs_Correo = value; }
        }
        public string Acs_Contacto2
        {
            get { return _Acs_Contacto2; }
            set { _Acs_Contacto2 = value; }
        }
        public int Acs_Telefono2
        {
            get { return _Acs_Telefono2; }
            set { _Acs_Telefono2 = value; }
        }
        public string Acs_Correo2
        {
            get { return _Acs_Correo2; }
            set { _Acs_Correo2 = value; }
        }
        public string Acs_Contacto3
        {
            get { return _Acs_Contacto3; }
            set { _Acs_Contacto3 = value; }
        }
        public int Acs_Telefono3
        {
            get { return _Acs_Telefono3; }
            set { _Acs_Telefono3 = value; }
        }
        public string Acs_Correo31
        {
            get { return _Acs_Correo3; }
            set { _Acs_Correo3 = value; }
        }
        public string Acs_Correo3
        {
            get { return Acs_Correo31; }
            set { Acs_Correo31 = value; }
        }
        public string Acs_Contacto4
        {
            get { return _Acs_Contacto4; }
            set { _Acs_Contacto4 = value; }
        }
        public int Acs_Telefono4
        {
            get { return _Acs_Telefono4; }
            set { _Acs_Telefono4 = value; }
        }
        public string Acs_Correo4
        {
            get { return _Acs_Correo4; }
            set { _Acs_Correo4 = value; }
        }
        public int Acs_Telefono5
        {
            get { return _Acs_Telefono5; }
            set { _Acs_Telefono5 = value; }
        }
        public string Acs_Correo5
        {
            get { return _Acs_Correo5; }
            set { _Acs_Correo5 = value; }
        }
        public string Acs_Contacto6
        {
            get { return _Acs_Contacto6; }
            set { _Acs_Contacto6 = value; }
        }
        public string Acs_Correo6
        {
            get { return _Acs_Correo6; }
            set { _Acs_Correo6 = value; }
        }
        public string Acs_Proveedor
        {
            get { return _Acs_Proveedor; }
            set { _Acs_Proveedor = value; }
        }
        public int Acs_RutaEntrega
        {
            get { return _Acs_RutaEntrega; }
            set { _Acs_RutaEntrega = value; }
        }
        public int Acs_RutaServicio
        {
            get { return _Acs_RutaServicio; }
            set { _Acs_RutaServicio = value; }
        }
        public bool Acs_ReqOrdenCompra
        {
            get { return _Acs_ReqOrdenCompra; }
            set { _Acs_ReqOrdenCompra = value; }
        }
        public DateTime? Acs_VigenciaIni
        {
            get { return _Acs_VigenciaIni; }
            set { _Acs_VigenciaIni = value; }
        }
        public int Acs_Semana
        {
            get { return _Acs_Semana; }
            set { _Acs_Semana = value; }
        }
        public bool Acs_ReqConfirmacion
        {
            get { return _Acs_ReqConfirmacion; }
            set { _Acs_ReqConfirmacion = value; }
        }
        public bool Acs_RecPedCorreo
        {
            get { return _Acs_RecPedCorreo; }
            set { _Acs_RecPedCorreo = value; }
        }
        public bool Acs_RecPedFax
        {
            get { return _Acs_RecPedFax; }
            set { _Acs_RecPedFax = value; }
        }
        public bool Acs_RecPedTel
        {
            get { return _Acs_RecPedTel; }
            set { _Acs_RecPedTel = value; }
        }
        public bool Acs_RecPedOtro
        {
            get { return _Acs_RecPedOtro; }
            set { _Acs_RecPedOtro = value; }
        }
        public string Acs_RecPedOtroStr
        {
            get { return _Acs_RecPedOtroStr; }
            set { _Acs_RecPedOtroStr = value; }
        }

        //VISITA
        private double? _Vis_Frecuencia;

        public double? Vis_Frecuencia
        {
            get { return _Vis_Frecuencia; }
            set { _Vis_Frecuencia = value; }
        }
        private bool _Vis_Lunes;

        public bool Vis_Lunes
        {
            get { return _Vis_Lunes; }
            set { _Vis_Lunes = value; }
        }
        private bool _Vis_Martes;

        public bool Vis_Martes
        {
            get { return _Vis_Martes; }
            set { _Vis_Martes = value; }
        }
        private bool _Vis_Miercoles;

        public bool Vis_Miercoles
        {
            get { return _Vis_Miercoles; }
            set { _Vis_Miercoles = value; }
        }
        private bool _Vis_Jueves;

        public bool Vis_Jueves
        {
            get { return _Vis_Jueves; }
            set { _Vis_Jueves = value; }
        }
        private bool _Vis_Viernes;

        public bool Vis_Viernes
        {
            get { return _Vis_Viernes; }
            set { _Vis_Viernes = value; }
        }
        private bool _Vis_Sabado;

        public bool Vis_Sabado
        {
            get { return _Vis_Sabado; }
            set { _Vis_Sabado = value; }
        }
        private string _Vis_HrAm1;

        public string Vis_HrAm1
        {
            get { return _Vis_HrAm1; }
            set { _Vis_HrAm1 = value; }
        }
        private string _Vis_HrAm2;

        public string Vis_HrAm2
        {
            get { return _Vis_HrAm2; }
            set { _Vis_HrAm2 = value; }
        }
        private string _Vis_HrPm1;

        public string Vis_HrPm1
        {
            get { return _Vis_HrPm1; }
            set { _Vis_HrPm1 = value; }
        }
        private string _Vis_HrPm2;
        public string Rec_Semanas;
        public bool Rec_Lunes;
        public bool Rec_Martes;
        public bool Rec_Miercoles;
        public bool Rec_Jueves;
        public bool Rec_Viernes;
        public bool Rec_Sabado;
        public bool Rec_Confirmacion;
        public bool Rec_Correo;
        public bool Rec_Fax;
        public bool Rec_Telefono;
        public bool Rec_Representante;
        public bool Rec_Otro;
        public string Rec_OtroStr;
        

        public string Vis_HrPm2
        {
            get { return _Vis_HrPm2; }
            set { _Vis_HrPm2 = value; }
        }
    }
}
