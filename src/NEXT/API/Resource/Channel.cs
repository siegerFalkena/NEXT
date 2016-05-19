using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Resource
{
    public class Channel : IResource
    {
        int? channelID { get; set; } = null;
        int? parentChannelID { get; set; } = null;

        string Name { get; set; } = null;

        public override Dictionary<string, string> meta(string relationship)
        {
            throw new NotImplementedException();
        }
    }
}
