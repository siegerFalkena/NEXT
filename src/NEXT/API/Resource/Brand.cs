﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Resource
{
    public class Brand : AbstractResource
    {
        public int brandID { get; set; }
        public string Name { get; set; } = null;

        public override Dictionary<string, string> generateMeta(string relationship)
        {
            Dictionary<string, string> newMeta = new Dictionary<string, string>();
            newMeta.Add("ID", brandID.ToString());
            newMeta.Add("link", "/api/brand/" + brandID);
            if (relationship != null) newMeta.Add("rel", relationship);
            return newMeta;
        }
    }
}
