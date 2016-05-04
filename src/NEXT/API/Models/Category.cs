using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NEXT.API.Models
{
    public class Category
    {
        [ScaffoldColumn(false)]
        public int categoryID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        
    }
}
