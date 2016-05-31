﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Resource
{
    public class Vendor : AbstractResource
    {
        public int VendorID { get; set; }
        public int CompanyID { get; set; }
        public string Name { get; set; }

        public override Dictionary<string, string> generateMeta(string relationship)
        {
            Dictionary<string, string> newMeta = new Dictionary<string, string>();
            newMeta.Add("ID", VendorID.ToString());
            newMeta.Add("link", "/api/vendor/" + VendorID);
            if (relationship != null) newMeta.Add("rel", relationship);
            return newMeta;
        }
    }
}
