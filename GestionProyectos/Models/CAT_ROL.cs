//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestionProyectos.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CAT_ROL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAT_ROL()
        {
            this.PLANILLA_RRHH = new HashSet<PLANILLA_RRHH>();
            this.TECNICOes = new HashSet<TECNICO>();
        }
    
        public int ID_CAT_ROL { get; set; }
        public string DESC_ROL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PLANILLA_RRHH> PLANILLA_RRHH { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TECNICO> TECNICOes { get; set; }
    }
}
