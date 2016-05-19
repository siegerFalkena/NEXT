using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace NEXT.API.Resource
{
    public class User : AbstractResource
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

        public Company Company { get; set; }

        public override Dictionary<string, string> generateMeta(string relationship)
        {
            Dictionary<string, string> newMeta = new Dictionary<string, string>();
            newMeta.Add("ID", ID.ToString());
            newMeta.Add("link", "/api/brand/" + ID);
            if (relationship != null) newMeta.Add("rel", relationship);
            return newMeta;
        }

    }
}
