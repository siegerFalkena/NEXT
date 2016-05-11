using System;
using System.Collections.Generic;
using System.Linq;

using System.Linq.Expressions;
using System.Threading.Tasks;
using NEXT.API.Models;

namespace NEXT.API.Query
{
    public class BrandQuery
    {

        //TODO code a predicatebuilder so not everything has to be passed
        //TODO refactor to query builder
        public string brandNameContains { get;set;} = null;
        public string orderBy { get; set; } = null;
        public bool ascending { get; set; } = true;
        

        public Expression<Func<Brand, bool>> asExpression()
        {
            Expression<Func<Brand, bool>> query = (Brand => true);
            if (brandNameContains != null) {
                query = PredicateBuilder.And(query, (brand => brand.Name.Contains(brandNameContains)));
            }
            return query;

        }

        public IQueryable<Brand> orderQuery(IQueryable<Brand> queryable) {
            if (orderBy != null) {
                if (orderBy.Equals("Name")) return ascending ? queryable.OrderBy(brand => brand.Name) : queryable.OrderByDescending(brand=> brand.Name);
                if (orderBy.Equals("ID")) return ascending ? queryable.OrderBy(brand => brand.ID) : queryable.OrderByDescending(brand => brand.ID);
                throw new KeyNotFoundException();
            }
            return queryable;
        }
    }
}
