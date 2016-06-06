using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Resource
{
    public class ProductAttribute : AbstractResource
    {
        public int AttributeID { get; set; }
        public string AttributeName { get; set; } 
        public int ProductID { get; set; }
        public int LanguageID { get; set; }
        public int VendorID { get; set; }
        public string Value { get; set; }
        
    }
}
