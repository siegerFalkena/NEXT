using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Resource
{
    public class Channel : AbstractResource
    {
        int? channelID { get; set; } = null;
        int? parentChannelID { get; set; } = null;

        string Name { get; set; } = null;

        public override Dictionary<string, string> generateMeta(string relationship)
        {
            throw new NotImplementedException();
        }
    }
}
