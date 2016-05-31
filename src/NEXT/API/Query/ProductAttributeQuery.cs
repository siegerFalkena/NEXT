using System;
using System.Collections.Generic;
using System.Linq;

using System.Linq.Expressions;
using System.Threading.Tasks;
using NEXT.DB.Models;

namespace NEXT.API.Query
{
    public class ProductAttributeQuery : AbstractQuery
    {
        
        public string Name { get; set; } = null;
        public string Value { get; set; } = null;
        public bool ascending = true;
        public string orderBy = null;

        public Expression<Func<DB.Models.ProductAttributeValue, bool>> asExpression()
        {
            Expression<Func<DB.Models.ProductAttributeValue, bool>> query = (rootQuery => true);
            if (Value != null)
            {
                query = PredicateBuilder.And(query, (attribute => attribute.Value.Contains(Value)));
            }
            return query;
        }

        public IQueryable<DB.Models.ProductAttributeValue> getOrdering(IQueryable<DB.Models.ProductAttributeValue> queryable)
        {
            if (orderBy != null)
            {
                if (orderBy.Equals("Value")) return ascending ? queryable.OrderBy(attribute => attribute.Value) : queryable.OrderByDescending(attribute => attribute.Value);
            }
            return queryable;
        }
    }
}
