//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CapaDatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class CatACYS_Productos_DatosGar_Fechas
    {
        public int Id { get; set; }
        public Nullable<int> Id_ACYS { get; set; }
        public Nullable<int> Id_TG { get; set; }
        public Nullable<int> Mes { get; set; }
        public Nullable<System.DateTime> FechaCorte { get; set; }
        public Nullable<bool> Facturado { get; set; }
    
        public virtual CatCNac_ACYS CatCNac_ACYS { get; set; }
    }
}