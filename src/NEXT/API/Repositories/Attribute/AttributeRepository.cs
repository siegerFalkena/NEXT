using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using NEXT.DB.Models;
using AutoMapper;
using NEXT.API.Resource;
using NEXT.API.Query;

namespace NEXT.API.Repositories
{
    public class AttributeRepository : IAttributeRepository
    {
        private NEXTContext context;
        private IMapper mapper;
        public AttributeRepository(NEXTContext context, IMappingConfigProvider mapperConfigProvider)
        {
            this.context = context;
            mapper = mapperConfigProvider.getConfig().CreateMapper();

        }

        void IDisposable.Dispose()
        {

        }

        public ICollection<Resource.ProductAttribute> query(ProductAttributeQuery query)
        { 
            IQueryable<DB.Models.ProductAttributeValue> attributes = 
                context.ProductAttributeValue.Include(pav => pav.Attribute).ThenInclude(at => at.AttributeType).Where(query.asExpression());
            attributes = query.getOrdering(attributes).Skip(query.page * query.results).Take(query.results);
            return mapper.Map<ICollection<DB.Models.ProductAttributeValue>, ICollection<API.Resource.ProductAttribute>>(attributes.ToList());
        }
    }
}
