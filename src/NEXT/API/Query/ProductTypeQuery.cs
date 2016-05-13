using System;
using System.Collections.Generic;
using System.Linq;

using System.Linq.Expressions;
using System.Threading.Tasks;
using NEXT.DB.Models;

namespace NEXT.API.Query
{
    public class ProductTypeQuery
    {

        //TODO code a predicatebuilder so not everything has to be passed
        //TODO refactor to query builder


        public Expression<Func<ProductType, bool>> asExpression()
        {
            Expression<Func<ProductType, bool>> query = (ProductType => true);

            return query;

        }
    }
}
