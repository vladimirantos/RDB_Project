//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RDB_Project
{
    using System;
    using System.Collections.Generic;
    
    public partial class Device
    {
        public Device()
        {
            this.Measurements = new HashSet<Measurement>();
        }
    
        public string serialNumber { get; set; }
        public string description { get; set; }
    
        public virtual ICollection<Measurement> Measurements { get; set; }
    }
}
