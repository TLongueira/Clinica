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
    
    public partial class sucursales_profesionales_especialidades
    {
        public int id_spe { get; set; }
        public Nullable<int> id_cpe { get; set; }
        public Nullable<int> id_sucursal { get; set; }
        public int dia { get; set; }
        public System.TimeSpan Hora_inicio { get; set; }
        public System.TimeSpan Hora_fin { get; set; }
    
        public virtual Clinica_profesinales_especialidad Clinica_profesinales_especialidad { get; set; }
        public virtual DiasSemana DiasSemana { get; set; }
        public virtual Sucursal Sucursal { get; set; }
    }
}
