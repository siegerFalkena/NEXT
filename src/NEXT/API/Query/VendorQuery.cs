using System;
using System.Collections.Generic;
using System.Linq;

using System.Linq.Expressions;
using System.Threading.Tasks;
using NEXT.DB.Models;

namespace NEXT.API.Query
{
    public class VendorQuery : AbstractQuery
    {
        public string Name { get; set; } = null;
        public bool ascending = true;
        public string orderBy = null;

        public Expression<Func<Vendor, bool>> asExpression()
        {
            Expression<Func<Vendor, bool>> query = (ProductType => true);
            if (Name != null)
            {
                query = PredicateBuilder.And(query, (vendor => vendor.Name.Contains(Name)));
            }
            return query;
        }

        public IQueryable<Vendor> getOrdering(IQueryable<Vendor> queryable)
        {
            if (orderBy != null)
            {
                if (orderBy.Equals("Name")) return ascending ? queryable.OrderBy(vendor => vendor.Name) : queryable.OrderByDescending(vendor => vendor.Name);
            }
            return queryable;
        }
    }
}
