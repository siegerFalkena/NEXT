using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace NEXT.API.Resource
{
    public class Vendor : AbstractResource
    {
        public int VendorID { get; set; }
        public int CompanyID { get; set; }
        public string Name { get; set; }
        
    }
}
