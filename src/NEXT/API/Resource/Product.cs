﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Resource
{
    public class Product : AbstractResource
    {
        public int productID { get; set; }

        public DateTime? Created { get; set; }
        public int CreatedBy { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string ExternalProductIdentifier { get; set; }
        public DateTime? LastModified { get; set; }
        public int? LastModifiedBy { get; set; } = null;

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string SKU { get; set; }
        public string description { get; set; } = null;
        public int? ParentProductID { get; set; } = null;

        [Required]
        public int ProductTypeID { get; set; }
        [Required]
        public int BrandID { get; set; }

        public ProductType ProductType { get; set; } = null;
        public Brand brand { get; set; } = null;
        public Product ParentProduct { get; set; } = null;

        public ICollection<Channel> channel { get; set; } = null;
        public ICollection<Vendor> Vendor { get; set; }
        public ICollection<ProductAttribute> attributeValues { get; set; }
        public ICollection<ProductAttribute> attributeOptions { get; set; }
        public ICollection<Product> relatedProduct { get; set; }
        public ICollection<Product> relatedProductNavigation { get; set; }
        public ICollection<Product> children { get; set; }

        public override Dictionary<string, string> generateMeta(string relationship)
        {
            Dictionary<string, string> newMeta = new Dictionary<string, string>();
            newMeta.Add("ID", productID.ToString());
            newMeta.Add("link", "/api/product/" + productID);
            if (relationship != null) newMeta.Add("rel", relationship);
            return newMeta;
        }

    }
}
