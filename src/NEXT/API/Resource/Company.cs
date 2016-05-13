using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Resource
{
    public class Company : IResource
    {

        public int ID { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }

        public ICollection<User> User { get; set; }
        public ICollection<Vendor> Vendor { get; set; }

        public string toJson()
        {
            throw new NotImplementedException();
        }
    }
}
