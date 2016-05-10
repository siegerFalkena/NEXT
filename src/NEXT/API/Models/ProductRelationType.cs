using System;
using System.Collections.Generic;

namespace NEXT.API.Models
{
    public partial class ProductRelationType
    {
        public ProductRelationType()
        {
            RelatedProduct = new HashSet<RelatedProduct>();
        }

        public int RelatedProductTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RelatedProduct> RelatedProduct { get; set; }
    }
}
