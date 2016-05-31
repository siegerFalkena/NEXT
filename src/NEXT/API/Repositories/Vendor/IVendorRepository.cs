using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NEXT.API.Query;
using NEXT.API.Resource;

namespace NEXT.API.Repositories
{
    public interface IVendorRepository :IDisposable
    {
        ICollection<Resource.Vendor> query(VendorQuery query);
    }
}
