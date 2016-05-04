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
        public string minPrice { get; set; } = null;
        public string maxPrice { get; set; } = null;
        public string nameContains { get; set; } = null;
        public string nameExact { get; set; } = null;
        public string descriptionContains { get; set; } = null ;


        public Expression<Func<Product, bool>> asExpression()
        {
            Expression<Func<Product, bool>> baseExpression = (product => true);
            if (maxPrice != null) {
                Expression<Func<Product, bool>> maxPriceExpression = (product => product.price <= decimal.Parse(maxPrice));
                baseExpression = PredicateBuilder.And(baseExpression, maxPriceExpression);
            }
            if (minPrice != null)
            {
                Expression<Func<Product, bool>> minPriceExpression = (product => product.price >= decimal.Parse(minPrice));
                baseExpression = PredicateBuilder.And(baseExpression, minPriceExpression);
            }
            if (nameContains != null)
            {
                Expression<Func<Product, bool>> namecontains = (product => product.name.Contains(nameContains));
                baseExpression = PredicateBuilder.And(baseExpression, namecontains);
            }
            if (maxPrice != null)
            {
                Expression<Func<Product, bool>> maxPriceExpression = (product => product.price <= decimal.Parse(maxPrice));
                baseExpression = PredicateBuilder.And(baseExpression, maxPriceExpression);
            }
            if (descriptionContains != null) {
                Expression<Func<Product, bool>> descriptionExpression = (product => product.description.Contains(descriptionContains));
                baseExpression = PredicateBuilder.And(baseExpression, descriptionExpression);
            }
            
            return baseExpression;
            
        }
    }
}
