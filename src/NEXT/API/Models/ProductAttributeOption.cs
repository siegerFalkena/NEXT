using System;
using System.Collections.Generic;

namespace NEXT.API.Models
{
    public partial class ProductAttributeOption
    {
        public int AttributeID { get; set; }
        public int ProductID { get; set; }
        public int VendorID { get; set; }
        public string Value { get; set; }

        public virtual Product Product { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual AttributeOption AttributeOption { get; set; }
    }
}
