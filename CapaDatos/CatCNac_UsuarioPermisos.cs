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
    
    public partial class CatCNac_UsuarioPermisos
    {
        public int Id { get; set; }
        public int Id_Usuario { get; set; }
        public int Id_Estructura { get; set; }
    
        public virtual CatCNac_Estructura CatCNac_Estructura { get; set; }
        public virtual CatCNac_Usuario CatCNac_Usuario { get; set; }
    }
}
