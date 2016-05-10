using System;
using System.Collections.Generic;
using System.Linq;

using System.Linq.Expressions;
using System.Threading.Tasks;
using NEXT.API.Models;

namespace NEXT.API.Query
{
    public class ProductQuery
    {

        //TODO code a predicatebuilder so not everything has to be passed
        //TODO refactor to query builder


        public Expression<Func<Product, bool>> asExpression()
        {
            Expression<Func<Product, bool>> query = (Product => true);
            if (minPrice != null) {
                query = PredicateBuilder.And(query, (Product => Product.ID))

            }
            return query;

        }
    }
}
