using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NEXT.API.Models
{
    public class User
    {
        [ScaffoldColumn(false)]
        public int userID { get; set; }

        public string name { get; set; }
        
        public string email { get; set; }

    }
}
