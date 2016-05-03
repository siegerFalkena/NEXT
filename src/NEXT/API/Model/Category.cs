using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NEXT.API.Model
{
    public class Category
    {
        [ScaffoldColumn(false)]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        
    }
}
