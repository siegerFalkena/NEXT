using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace NEXT.API.Model
{
    public class Product
    {


        [ScaffoldColumn(true)]
        public int ID { get; set; }
        
        public string name { get; set; }

        [Display(Name = "Price")]
        public double price { get; set; }
        
        public string description { get; set; }

    }
}
