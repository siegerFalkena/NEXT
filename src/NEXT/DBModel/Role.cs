using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NEXT.DB.Models
{
    public class Role
    {
        public string description;
        public int userID;
        public int roleID;
    }
}
