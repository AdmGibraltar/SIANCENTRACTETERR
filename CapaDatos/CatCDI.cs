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
    
    public partial class CatCDI
    {
        public CatCDI()
        {
            this.CatCNac_Solicitudes = new HashSet<CatCNac_Solicitudes>();
        }
    
        public int Id_Cd { get; set; }
        public string Cd_Nombre { get; set; }
        public string Cd_DescCorta { get; set; }
        public Nullable<int> Cd_Tipo { get; set; }
    
        public virtual ICollection<CatCNac_Solicitudes> CatCNac_Solicitudes { get; set; }
    }
}
