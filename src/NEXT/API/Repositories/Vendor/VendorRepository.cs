using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NEXT.DB.Models;
using AutoMapper;
using NEXT.API.Resource;
using NEXT.API.Query;

namespace NEXT.API.Repositories
{
    public class VendorRepository : IVendorRepository
    {
        private NEXTContext context;
        private IMapper mapper;
        public VendorRepository(NEXTContext context, IMappingConfigProvider mapperConfigProvider)
        {
            this.context = context;
            mapper = mapperConfigProvider.getConfig().CreateMapper();

        }

        void IDisposable.Dispose()
        {
        }

        public void createVendor(Resource.Vendor vendor)
        {
        }

        public ICollection<Resource.Vendor> query(VendorQuery query)
        {
            IQueryable<DB.Models.Vendor> vendors = context.Vendor.Where(query.asExpression());
            vendors = query.getOrdering(vendors).Skip(query.page * query.results).Take(query.results);
            return mapper.Map<ICollection<DB.Models.Vendor>, ICollection<API.Resource.Vendor>>(vendors.ToList());
        }

        public Resource.Vendor getByID(int VendorID) {
            DB.Models.Vendor vendor = context.Vendor.Where(v => v.ID == VendorID).SingleOrDefault();
            return mapper.Map<Resource.Vendor>(vendor);
        }
    }
}
