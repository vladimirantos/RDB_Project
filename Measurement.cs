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
    
    public partial class Measurement
    {
        public Measurement()
        {
            this.Points = new HashSet<Point>();
        }
    
        public int id_measurement { get; set; }
        public string device { get; set; }
        public short mtype { get; set; }
        public string description { get; set; }
        public System.DateTime date { get; set; }
    
        public virtual Device Device1 { get; set; }
        public virtual ICollection<Point> Points { get; set; }
        public virtual MType MType1 { get; set; }
    }
}
