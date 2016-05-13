using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Resource
{
    public class ProductAttribute
    {
        public string type { get; set; } = null;
        public int? typeID { get; set; } = null;

        public string value { get; set; } = null;
        public int? valueID { get; set; } = null;

        public string contentType { get; set; } = null;


    }
}
