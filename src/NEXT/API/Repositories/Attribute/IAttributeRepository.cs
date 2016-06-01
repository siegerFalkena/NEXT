using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NEXT.DB.Models;
using NEXT.API.Query;

namespace NEXT.API.Repositories
{
    public interface IAttributeRepository : IDisposable
    {
        ICollection<Resource.Attribute> query(AttributeQuery query);
        Resource.Attribute getByID(int AttributeID);
    }
}
