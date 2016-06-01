using System;
using System.Collections.Generic;
using System.Linq;

using System.Linq.Expressions;
using System.Threading.Tasks;
using NEXT.DB.Models;

namespace NEXT.API.Query
{
    public class AttributeQuery : AbstractQuery
    {
        
        public string Name { get; set; } = null;
        public string Code { get; set; } = null;
        public string TypeName { get; set; } = null;

        public bool ascending = true;
        public string orderBy = null;

        public Expression<Func<DB.Models.Attribute, bool>> asExpression()
        {
            Expression<Func<DB.Models.Attribute, bool>> query = (rootQuery => true);
            if (Code != null)
            {
                query = PredicateBuilder.And(query, (attribute => attribute.Code.Contains(Code)));
            }
            if (Name != null)
            {
                query = PredicateBuilder.And(query, (attribute => attribute.Name.Contains(Name)));
            }
            if (TypeName != null)
            {
                query = PredicateBuilder.And(query, (attribute => attribute.AttributeType.Name.Contains(TypeName)));
            }
            return query;
        }

        public IQueryable<DB.Models.Attribute> getOrdering(IQueryable<DB.Models.Attribute> queryable)
        {
            if (orderBy != null)
            {
                if (orderBy.Equals("Code")) return ascending ? queryable.OrderBy(attribute => attribute.Code) : queryable.OrderByDescending(attribute => attribute.Code);
                if (orderBy.Equals("TypeName")) return ascending ? queryable.OrderBy(attribute => attribute.AttributeType.Name) : queryable.OrderByDescending(attribute => attribute.AttributeType.Name);
                if (orderBy.Equals("Name")) return ascending ? queryable.OrderBy(attribute => attribute.Name) : queryable.OrderByDescending(attribute => attribute.Name);
            }
            return queryable;
        }
    }
}
