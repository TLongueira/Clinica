//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Clinica
{
    using System;
    using System.Collections.Generic;
    
    public partial class Profesional
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Profesional()
        {
            this.Clinica_profesionales = new HashSet<Clinica_profesionales>();
        }
    
        public int ProfesionalId { get; set; }
        public int PersonaId { get; set; }
        public string MatriculaNacional { get; set; }
        public string MatriculaProvincial { get; set; }
        public System.DateTime Creado { get; set; }
        public System.DateTime Modificado { get; set; }
        public Nullable<bool> Activo { get; set; }
        public string Photopath { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Clinica_profesionales> Clinica_profesionales { get; set; }
        public virtual Persona Persona { get; set; }
    }
}