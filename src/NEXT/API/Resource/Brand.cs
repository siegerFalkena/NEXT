using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Resource
{
    public class Brand : AbstractResource
    {
        public int brandID { get; set; }
        public string Name { get; set; } = null;

    }
}
