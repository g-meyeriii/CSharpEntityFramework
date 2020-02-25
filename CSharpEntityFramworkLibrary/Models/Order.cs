using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSharpEntityFramworkLibrary.Models {
    public class Order {

        public int Id { get; set; }
        [StringLength(30)]
        [Required]
        public string Description { get; set; }
        public double Amount { get; set; }
        public int CustomerId { get; set; }
        public  virtual Customer Customer { get; set; }// Virtual tells EF not to make this property part of table

        public override string ToString() => $"{Id}|{Description}|{Amount}|{CustomerId}";
        public Order() { }
        
    }
}
