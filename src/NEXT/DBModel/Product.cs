using System;
using System.Collections.Generic;

namespace NEXT.DB.Models
{
    public partial class Product
    {
        public Product()
        {
            ChannelProduct = new HashSet<ChannelProduct>();
            ProductAttributeOption = new HashSet<ProductAttributeOption>();
            ProductAttributeValue = new HashSet<ProductAttributeValue>();
            RelatedProduct = new HashSet<RelatedProduct>();
            RelatedProductNavigation = new HashSet<RelatedProduct>();
            VendorProduct = new HashSet<VendorProduct>();
        }

        public int ID { get; set; }
        public int BrandID { get; set; }
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public string ExternalProductIdentifier { get; set; }
        public DateTime LastModified { get; set; }
        public int? LastModifiedBy { get; set; } = null;
        public int? ParentProductID { get; set; } = null;
        public int ProductTypeID { get; set; }
        public string SKU { get; set; }


        public virtual ICollection<ChannelProduct> ChannelProduct { get; set; }
        public virtual ICollection<ProductAttributeOption> ProductAttributeOption { get; set; }
        public virtual ICollection<ProductAttributeValue> ProductAttributeValue { get; set; }
        public virtual ICollection<RelatedProduct> RelatedProduct { get; set; }
        public virtual ICollection<RelatedProduct> RelatedProductNavigation { get; set; }
        public virtual ICollection<VendorProduct> VendorProduct { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Product ParentProduct { get; set; }
        public virtual ICollection<Product> InverseParentProduct { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}
