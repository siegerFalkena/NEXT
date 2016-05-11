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

        public DateTime? min_Created { get; set; } = null;
        public DateTime? max_Created { get; set; } = null;

        public int? CreatedBy { get; set; } = null;
        public string ExternalProductIdentifier { get; set; } = null;
        public DateTime? min_LastModified { get; set; } = null;
        public DateTime? max_LastModified { get; set; } = null;

        //range
        public int? LastModifiedBy { get; set; } = null;

        public int? ParentProductID { get; set; } = null;
        public int? ProductTypeID { get; set; } = null;
        public string SKU { get; set; } = null;

        public Expression<Func<Product, bool>> asExpression()
        {
            Expression<Func<Product, bool>> query = (Product => true);
            if (ExternalProductIdentifier != null) {
                query = PredicateBuilder.And(query, (Product => Product.ExternalProductIdentifier.Contains(ExternalProductIdentifier)));
            }
            if (SKU != null)
            {
                query = PredicateBuilder.And(query, (Product => Product.SKU.Contains(SKU)));
            }
            if (ProductTypeID != null)
            {
                query = PredicateBuilder.And(query, (Product => Product.ProductTypeID == ProductTypeID));
            }
            if (ParentProductID != null)
            {
                query = PredicateBuilder.And(query, (Product => Product.ParentProductID == ParentProductID));
            }
            if (LastModifiedBy != null)
            {
                query = PredicateBuilder.And(query, (Product => Product.LastModifiedBy == LastModifiedBy));
            }
            if (min_LastModified != null)
            {
                query = PredicateBuilder.And(query, (Product => Product.LastModified >= min_LastModified));
            }
            if (max_LastModified != null)
            {
                query = PredicateBuilder.And(query, (Product => Product.LastModified <= max_LastModified));
            }
            if (min_LastModified != null)
            {
                query = PredicateBuilder.And(query, (Product => Product.LastModified >= min_LastModified));
            }
            if (min_Created != null)
            {
                query = PredicateBuilder.And(query, (Product => Product.Created <= min_Created));
            }
            if (max_Created != null)
            {
                query = PredicateBuilder.And(query, (Product => Product.Created >= max_Created));
            }
            if (CreatedBy != null) {
                query = PredicateBuilder.And(query, (Product => Product.CreatedBy == CreatedBy));
            }
            return query;

        }
    }
}
