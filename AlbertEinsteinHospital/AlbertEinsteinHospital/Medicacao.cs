//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlbertEinsteinHospital
{
    using System;
    using System.Collections.Generic;
    
    public partial class Medicacao
    {
        public int Id { get; set; }
        public System.DateTime DataInicio { get; set; }
        public System.DateTime DataFim { get; set; }
        public string Medicamento { get; set; }
        public string Dosagem { get; set; }
    
        public virtual Paciente Paciente { get; set; }
    }
}
