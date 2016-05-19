using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Resource
{
    public class Company : AbstractResource
    {

        public int ID { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }

        public ICollection<User> User { get; set; }
        public ICollection<Vendor> Vendor { get; set; }

        public override Dictionary<string, string> generateMeta(string relationship)
        {
            Dictionary<string, string> newMeta = new Dictionary<string, string>();
            newMeta.Add("ID", ID.ToString());
            newMeta.Add("link", "/api/company/" + ID);
            if (relationship != null) newMeta.Add("rel", relationship);
            return newMeta;
        }
    }
}
