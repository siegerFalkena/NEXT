using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Resource
{
    public class Brand : IResource
    {
        public int? brandID { get; set; } = null;
        public string Name { get; set; } = null;


        public string toJson()
        {
            throw new NotImplementedException();
        }
    }
}
