using System;
using System.Collections.Generic;

namespace NEXT.DB.Models
{
    public partial class Company
    {
        public Company()
        {
            User = new HashSet<User>();
            Vendor = new HashSet<Vendor>();
        }

        public int ID { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> User { get; set; }
        public virtual ICollection<Vendor> Vendor { get; set; }


    }
}
