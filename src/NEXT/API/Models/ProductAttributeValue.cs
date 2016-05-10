using System;
using System.Collections.Generic;

namespace NEXT.API.Models
{
    public partial class ProductAttributeValue
    {
        public int AttributeID { get; set; }
        public int ProductID { get; set; }
        public int LanguageID { get; set; }
        public int VendorID { get; set; }
        public string Value { get; set; }

        public virtual Attribute Attribute { get; set; }
        public virtual Language Language { get; set; }
        public virtual Product Product { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
