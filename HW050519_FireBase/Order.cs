//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HW050519_FireBase
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int ID { get; set; }
        public Nullable<int> Customer_ID { get; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<System.DateTime> Date { get; set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }
    }
}
