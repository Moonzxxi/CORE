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
    
    public partial class clientTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public clientTable()
        {
            this.accountTable = new HashSet<accountTable>();
            this.accountTable1 = new HashSet<accountTable>();
        }
    
        public string IDENTIFIER { get; set; }
        public string NAME { get; set; }
        public string LAST { get; set; }
        public int ACCOUNTS { get; set; }
        public string PASSWORD { get; set; }
        public string PIN { get; set; }
        public string DIRECTION { get; set; }
        public string EMAIL { get; set; }
        public string STATE { get; set; }
        public System.DateTime REGDATE { get; set; }
        public Nullable<System.DateTime> LOGDATE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<accountTable> accountTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<accountTable> accountTable1 { get; set; }
    }
}