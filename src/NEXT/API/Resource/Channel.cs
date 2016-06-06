using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Resource
{
    public class Channel : AbstractResource
    {
        public int channelID { get; set; }
        public int parentChannelID { get; set; } 

        public string Name { get; set; } = null;
        
    }
}
