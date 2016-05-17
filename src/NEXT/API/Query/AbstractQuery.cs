using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Query
{
    public abstract class AbstractQuery
    {
        public int results { get; set; } = 25;
        public int page { get; set; } = 0;
    }
}
