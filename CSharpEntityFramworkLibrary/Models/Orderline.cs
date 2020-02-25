using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CSharpEntityFramworkLibrary.Models {
    
    public class Orderline {

    
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }


        [JsonIgnore] //tells the system to not get the order when reading an orderline, prevents the cylical error from happening
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public object Orderlines { get; set; }

        public override string ToString() => $"{Id}|{ProductId}|{OrderId}|{Quantity}";

        public Orderline() { 
            
        }
    }
}
