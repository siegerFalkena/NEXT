using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Resource
{
    public abstract class AbstractResource
    {
        public Dictionary<string, string> metadata;
        public abstract Dictionary<string, string> generateMeta(string relationship);
    }
}
