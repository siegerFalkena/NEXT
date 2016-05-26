using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NEXT.DB.Models;
using AutoMapper;
using NEXT.API.Resource;
using NEXT.API.Query;
using NEXT.DB.Models;

namespace NEXT.API.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private NEXTContext context;
        private IMapper mapper;
        public BrandRepository(NEXTContext context, IMappingConfigProvider mapperConfigProvider)
        {
            this.context = context;
            mapper = mapperConfigProvider.getConfig().CreateMapper();

        }

        void IDisposable.Dispose()
        {
        }

        public void createBrand(Resource.Brand brand)
        {
        }

        public ICollection<Resource.Brand> query(BrandQuery query)
        { 
            IQueryable<DB.Models.Brand> brands =  context.Brand.Where(query.asExpression());
            brands = query.getOrdering(brands).Skip(query.page * query.results).Take(query.results);
            return mapper.Map<ICollection<DB.Models.Brand>, ICollection<API.Resource.Brand>>(brands.ToList());
        }
    }
}
