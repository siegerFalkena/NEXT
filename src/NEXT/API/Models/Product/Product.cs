using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace NEXT.API.Models
{
    public class Product
    {


        [ScaffoldColumn(true)]
        public int productID { get; set; }
        
        public string name { get; set; }

        [Display(Name = "Price")]
        public decimal price { get; set; }
        
        public string description { get; set; }

    }
}
