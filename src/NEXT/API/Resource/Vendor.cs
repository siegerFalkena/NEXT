using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Resource
{
    public class Vendor : IResource
    {
        public int? vendorID { get; set; } = null;
        public string name { get; set; } = null;

        public override Dictionary<string, string> meta(string relationship)
        {
            throw new NotImplementedException();
        }
    }
}
