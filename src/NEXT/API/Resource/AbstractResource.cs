using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Resource
{
    public abstract class AbstractResource
    {

        public Meta _meta = new Meta();
        public virtual void setMeta()
        {
            this._meta.link = "<<abstract meta>>";
            this._meta.type = "<<abstract meta>>";
            this._meta.rel = "self";
        }

    }
}
