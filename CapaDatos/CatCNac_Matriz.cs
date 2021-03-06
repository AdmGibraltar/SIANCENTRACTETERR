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
    
    public partial class CatCNac_Matriz
    {
        public CatCNac_Matriz()
        {
            this.CatCNac_ACYS = new HashSet<CatCNac_ACYS>();
            this.CatCNac_IntranetListaFranq = new HashSet<CatCNac_IntranetListaFranq>();
            this.CatDomicilioFiscal = new HashSet<CatDomicilioFiscal>();
            this.CatCNac_Estructura = new HashSet<CatCNac_Estructura>();
            this.CatCNac_Usuario = new HashSet<CatCNac_Usuario>();
            this.CatACYS_DirFiscales = new HashSet<CatACYS_DirFiscales>();
            this.CatCNac_Solicitudes = new HashSet<CatCNac_Solicitudes>();
        }
    
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Nullable<System.DateTime> FechaInicio { get; set; }
        public Nullable<System.DateTime> FechaFin { get; set; }
        public bool Estatus { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<bool> Presupuesto { get; set; }
        public Nullable<bool> ReqAutorizacion { get; set; }
        public Nullable<bool> Nivel_1 { get; set; }
        public string Desc_Nivel_1 { get; set; }
        public Nullable<bool> Nivel_2 { get; set; }
        public string Desc_Nivel_2 { get; set; }
        public Nullable<bool> Nivel_3 { get; set; }
        public string Desc_Nivel_3 { get; set; }
        public Nullable<bool> Nivel_4 { get; set; }
        public string Desc_Nivel_4 { get; set; }
        public byte[] Logo { get; set; }
    
        public virtual CatClienteMatriz_Permisos CatClienteMatriz_Permisos { get; set; }
        public virtual ICollection<CatCNac_ACYS> CatCNac_ACYS { get; set; }
        public virtual ICollection<CatCNac_IntranetListaFranq> CatCNac_IntranetListaFranq { get; set; }
        public virtual ICollection<CatDomicilioFiscal> CatDomicilioFiscal { get; set; }
        public virtual ICollection<CatCNac_Estructura> CatCNac_Estructura { get; set; }
        public virtual ICollection<CatCNac_Usuario> CatCNac_Usuario { get; set; }
        public virtual ICollection<CatACYS_DirFiscales> CatACYS_DirFiscales { get; set; }
        public virtual CatACYS_SIANCENTRAL CatACYS_SIANCENTRAL { get; set; }
        public virtual CatCNac_IntranetFran CatCNac_IntranetFran { get; set; }
        public virtual ICollection<CatCNac_Solicitudes> CatCNac_Solicitudes { get; set; }
    }
}
