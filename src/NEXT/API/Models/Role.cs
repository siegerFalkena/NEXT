using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NEXT.API.Models
{
    public class Role
    {
        [ScaffoldColumn(false)]
        string rolename;

        int roleID;
    }
}
