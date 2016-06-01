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

        public ICollection<Resource.Attribute> query(AttributeQuery query)
        { 
            IQueryable<DB.Models.Attribute> attributes = 
                context.Attribute.Include(att => att.AttributeType).Where(query.asExpression());
            attributes = query.getOrdering(attributes).Skip(query.page * query.results).Take(query.results);
            return mapper.Map<ICollection<DB.Models.Attribute>, ICollection<API.Resource.Attribute>>(attributes.ToList());
        }

        public API.Resource.Attribute getByID(int AttributeID) {
            DB.Models.Attribute attribute = context.Attribute.Where(a => a.ID == AttributeID).Include(a => a.AttributeType).SingleOrDefault();
            return mapper.Map<API.Resource.Attribute>(attribute);
        }
    }
}
