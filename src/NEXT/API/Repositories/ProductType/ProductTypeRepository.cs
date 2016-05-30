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
    public class ProductTypeRepository : IProductTypeRepository
    {
        private NEXTContext context;
        private IMapper mapper;
        public ProductTypeRepository(NEXTContext context, IMappingConfigProvider mapperConfigProvider)
        {
            this.context = context;
            mapper = mapperConfigProvider.getConfig().CreateMapper();

        }

        void IDisposable.Dispose()
        {
        }

        public void createProductType(Resource.ProductType brand)
        {
        }

        public ICollection<Resource.ProductType> query(ProductTypeQuery query)
        {
            IQueryable<DB.Models.ProductType> brands =  context.ProductType.Where(query.asExpression());
            brands = query.getOrdering(brands).Skip(query.page * query.results).Take(query.results);
            return mapper.Map<ICollection<DB.Models.ProductType>, ICollection<API.Resource.ProductType>>(brands.ToList());
        }
    }
}
