using System;
using System.Collections.Generic;

namespace NEXT.API.Models
{
    public partial class Vendor
    {
        public Vendor()
        {
            ProductAttributeOption = new HashSet<ProductAttributeOption>();
            ProductAttributeValue = new HashSet<ProductAttributeValue>();
            VendorProduct = new HashSet<VendorProduct>();
        }

        public int ID { get; set; }
        public int CompanyID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductAttributeOption> ProductAttributeOption { get; set; }
        public virtual ICollection<ProductAttributeValue> ProductAttributeValue { get; set; }
        public virtual ICollection<VendorProduct> VendorProduct { get; set; }
        public virtual Company Company { get; set; }
    }
}
