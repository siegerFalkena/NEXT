using System;
using System.Collections.Generic;
using System.Linq;

using System.Linq.Expressions;
using System.Threading.Tasks;
using NEXT.DB.Models;

namespace NEXT.API.Query
{
    public class BrandQuery : AbstractQuery
    {

        public string Name { get; set; } = null;
        public bool ascending = true;
        public string orderBy = null;

        public Expression<Func<Brand, bool>> asExpression()
        {
            Expression<Func<Brand, bool>> query = (ProductType => true);
            if (Name != null)
            {
                query = PredicateBuilder.And(query, (brand => brand.Name.Contains(Name)));
            }
            return query;
        }

        public IQueryable<Brand> getOrdering(IQueryable<Brand> queryable)
        {
            if (orderBy != null)
            {
                if (orderBy.Equals("Name")) return ascending ? queryable.OrderBy(brand => brand.Name) : queryable.OrderByDescending(brand => brand.Name);
            }
            return queryable;
        }
    }
}
