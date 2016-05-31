using System;
using System.Collections.Generic;
using System.Linq;

using System.Linq.Expressions;
using System.Threading.Tasks;
using NEXT.DB.Models;

namespace NEXT.API.Query
{
    public class AttributeTypeQuery : AbstractQuery
    {

        public string Name { get; set; } = null;
        public bool ascending = true;
        public string orderBy = null;

        public Expression<Func<DB.Models.AttributeType, bool>> asExpression()
        {
            Expression<Func<DB.Models.AttributeType, bool>> query = (rootQuery => true);
            if (Name != null)
            {
                query = PredicateBuilder.And(query, (attributeType => attributeType.Name.Contains(Name)));
            }
            return query;
        }

        public IQueryable<DB.Models.AttributeType> getOrdering(IQueryable<DB.Models.AttributeType> queryable)
        {
            if (orderBy != null)
            {
                if (orderBy.Equals("Name")) return ascending ? queryable.OrderBy(attributeType => attributeType.Name) : queryable.OrderByDescending(attributeType => attributeType.Name);
            }
            return queryable;
        }
    }
}
