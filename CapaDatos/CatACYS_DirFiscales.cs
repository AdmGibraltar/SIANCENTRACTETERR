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
    
    public partial class CatACYS_DirFiscales
    {
        public int Id { get; set; }
        public Nullable<int> Id_ClienteMatriz { get; set; }
        public string ClienteDirFis { get; set; }
        public string DireccionDirFis { get; set; }
        public string EstadoDirFis { get; set; }
        public string ColoniaDirFis { get; set; }
        public string CPDirFis { get; set; }
        public string MunicipioDirFis { get; set; }
        public string RFCDirFis { get; set; }
        public string EmailFacturasDirFis { get; set; }
    
        public virtual CatCNac_Matriz CatCNac_Matriz { get; set; }
    }
}
