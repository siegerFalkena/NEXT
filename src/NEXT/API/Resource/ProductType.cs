using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Resource
{
    public class ProductType : IResource
    {
        public int? ID { get; set; }
        public string Name { get; set; }


        public override Dictionary<string, string> meta(string relationship)
        {
            throw new NotImplementedException();
        }
    }
}
