//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBCORE
{
    using System;
    using System.Collections.Generic;
    
    public partial class accountTable
    {
        public string IDENTIFIER { get; set; }
        public string ACCOUNT_NAME { get; set; }
        public string ACCOUNT_TYPE { get; set; }
        public string ACCOUNT_STATE { get; set; }
        public decimal BALANCE { get; set; }
        public System.DateTime OPENDATE { get; set; }
    
        public virtual clientTable clientTable { get; set; }
        public virtual clientTable clientTable1 { get; set; }
    }
}