using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace NEXT.API.Resource
{
    public class User : IResource
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

        public override Dictionary<string, string> meta(string relationship)
        {
            throw new NotImplementedException();
        }

        public string resourceID; 
    }
}
