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
    
    public partial class CatCNac_RolECommerce
    {
        public CatCNac_RolECommerce()
        {
            this.CatCNac_Usuario = new HashSet<CatCNac_Usuario>();
        }
    
        public int Id { get; set; }
        public string Nombre { get; set; }
    
        public virtual ICollection<CatCNac_Usuario> CatCNac_Usuario { get; set; }
    }
}