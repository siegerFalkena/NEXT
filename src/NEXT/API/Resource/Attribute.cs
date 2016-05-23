using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Resource
{
    public class Attribute : AbstractResource
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string AttributeType { get; set; }
        public int AttributeID { get; set; }
        public int LanguageID { get; set; }
        public int VendorID { get; set; }

        public override Dictionary<string, string> generateMeta(string relationship)
        {
            Dictionary<string, string> newMeta = new Dictionary<string, string>();
            newMeta.Add("ID", ID.ToString());
            newMeta.Add("link", "/api/attribute/" + ID);
            if (relationship != null) newMeta.Add("rel", relationship);
            return newMeta;
        }
    }
}
