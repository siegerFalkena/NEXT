using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Resource
{
    public class Vendor : AbstractResource
    {
        public int vendorID { get; set; }
        public int CompanyID { get; set; }
        public string Name { get; set; }

        public override Dictionary<string, string> generateMeta(string relationship)
        {
            Dictionary<string, string> newMeta = new Dictionary<string, string>();
            newMeta.Add("ID", vendorID.ToString());
            newMeta.Add("link", "/api/vendor/" + vendorID);
            if (relationship != null) newMeta.Add("rel", relationship);
            return newMeta;
        }
    }
}
