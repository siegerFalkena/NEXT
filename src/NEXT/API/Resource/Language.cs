using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Resource
{
    public class Language : AbstractResource
    {

        public int LanguageID { get; set; }
        public bool IsDefault { get; set; }
        public string ISOCode { get; set; }
        public string Name { get; set; }
        
    }
}
