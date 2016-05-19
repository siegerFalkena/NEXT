using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Resource
{
    public abstract class IResource
    {
      public abstract Dictionary<string, string> meta(string relationship);
    }
}
