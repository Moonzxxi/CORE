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
    
    public partial class transactionTable
    {
        public string IDENTIFIER_ROOT { get; set; }
        public string IDENTIFIER_AFFECTED { get; set; }
        public string ACCOUNT_ROOT { get; set; }
        public string ACCOUNT_AFFECTED { get; set; }
        public string TYPE { get; set; }
        public System.DateTime TRANSDATE { get; set; }
        public string DESCRIPTION { get; set; }
        public decimal BALANCE { get; set; }
    }
}
