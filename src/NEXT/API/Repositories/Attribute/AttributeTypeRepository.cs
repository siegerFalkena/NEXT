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
    public class AttributeTypeRepository : IAttributeTypeRepository
    {
        private NEXTContext context;
        private IMapper mapper;
        public AttributeTypeRepository(NEXTContext context, IMappingConfigProvider mapperConfigProvider)
        {
            this.context = context;
            mapper = mapperConfigProvider.getConfig().CreateMapper();

        }

        void IDisposable.Dispose()
        {
        }

        public void createAttributeType(Resource.AttributeType attributeType)
        {
        }

        public ICollection<Resource.AttributeType> query(AttributeTypeQuery query)
        { 
            IQueryable<DB.Models.AttributeType> attributeTypes =  context.AttributeType.Where(query.asExpression());
            attributeTypes = query.getOrdering(attributeTypes).Skip(query.page * query.results).Take(query.results);
            return mapper.Map<ICollection<DB.Models.AttributeType>, ICollection<API.Resource.AttributeType>>(attributeTypes.ToList());
        }
    }
}
