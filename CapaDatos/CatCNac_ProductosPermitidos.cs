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
    
    public partial class CatCNac_ProductosPermitidos
    {
        public CatCNac_ProductosPermitidos()
        {
            this.CatCNac_IntranetListaFranq = new HashSet<CatCNac_IntranetListaFranq>();
        }
    
        public int Id { get; set; }
        public string Descripcion { get; set; }
    
        public virtual ICollection<CatCNac_IntranetListaFranq> CatCNac_IntranetListaFranq { get; set; }
    }
}
