//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BootstrapWebApplication.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class rokn_uppsetingprbrukara
    {
        public int Id { get; set; }
        public int upp_id { get; set; }
        public string username { get; set; }
        public Nullable<int> UsersId { get; set; }
        public byte[] rv { get; set; }
    
        public virtual rokn_uppseting rokn_uppseting { get; set; }
        public virtual users users { get; set; }
    }
}
