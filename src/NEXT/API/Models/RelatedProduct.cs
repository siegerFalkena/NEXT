using System;
using System.Collections.Generic;

namespace NEXT.API.Models
{
    public partial class RelatedProduct
    {
        public int ProductID { get; set; }
        public int RelatedProductID { get; set; }
        public int RelatedProductTypeID { get; set; }

        public virtual Product Product { get; set; }
        public virtual Product RelatedProductNavigation { get; set; }
        public virtual ProductRelationType RelatedProductType { get; set; }
    }
}
