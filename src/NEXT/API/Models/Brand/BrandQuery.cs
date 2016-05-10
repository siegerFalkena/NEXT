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
        string brandNameContains { get;set;} = null;

        public Expression<Func<Brand, bool>> asExpression()
        {
            Expression<Func<Brand, bool>> query = (Brand => true);
            if (brandNameContains != null) {
                query = PredicateBuilder.And(query, (brand => brand.Name.Contains(brandNameContains)));
            }

            return query;

        }
    }
}
