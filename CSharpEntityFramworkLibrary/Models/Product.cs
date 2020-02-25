using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSharpEntityFramworkLibrary.Models {
    public class Product {
        
        public int Id { get; set; }
        
        public string Code { get; set; }
        //[StringLength(30)] attributes can't do everthing do, done in fluid api in the DbContext table
       // [Required]
        public string Name { get; set; }
        public double Price { get; set; }

        public override string ToString() => $"{Id}|{Code}|{Name}|{Price}";
        public Product() { }
    }
}
