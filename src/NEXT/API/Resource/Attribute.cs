using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Resource
{
    public class Attribute
    {
        public int attributeID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int attributeTypeID { get; set; }
        public string attributeTypeName { get; set; }
    }
}
