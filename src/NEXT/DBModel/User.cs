using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NEXT.DB.Models
{
    public partial class User
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

        public virtual Company Company { get; set; }
    }
}
