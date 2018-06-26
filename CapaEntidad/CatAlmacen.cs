using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class CatAlmacen
    {
        public int Id_Alm { get; set; }
        public int Alm_Clave { get; set; }
        public string Alm_Nombre { get; set; }
        public int Alm_Cuenta { get; set; }
        public int Alm_Subcuenta { get; set; }
        public string Alm_CtaCenCosto { get; set; }
        public string Alm_SubCtaCenCosto { get; set; }
        public string  Alm_CuentaStr { get; set; }
        public string Alm_SubcuentaStr { get; set; }
        public bool Alm_Activo { get; set; }

    }
}
