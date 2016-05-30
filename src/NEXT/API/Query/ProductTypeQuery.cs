using System;
using System.Collections.Generic;
using System.Linq;

using System.Linq.Expressions;
using System.Threading.Tasks;
using NEXT.DB.Models;

namespace NEXT.API.Query
{
    public class ProductTypeQuery : AbstractQuery
    {

        //TODO code a predicatebuilder so not everything has to be passed
        //TODO refactor to query builder
        public string Name { get; set; } = null;
        public string orderBy { get; set; } = "Name";
        public bool ascending { get; set; } = true;


        public Expression<Func<ProductType, bool>> asExpression()
        {
            Expression<Func<ProductType, bool>> query = (ProductType => true);
            if (Name != null)
            {
                query = PredicateBuilder.And(query, (productType => productType.Name.Contains(Name)));
            }
            return query;

        }


        public IQueryable<ProductType> getOrdering(IQueryable<ProductType> queryable)
        {
            if (orderBy != null)
            {
                if (orderBy.Equals("Name")) return ascending ? queryable.OrderBy(productType => productType.Name) : queryable.OrderByDescending(productType => productType.Name);
            }
            return queryable;
        }
    }
}
