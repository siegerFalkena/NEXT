using System;
using System.Collections.Generic;

namespace NEXT.API.Models
{
    public partial class VendorProduct
    {
        public VendorProduct()
        {
            ProductBarcode = new HashSet<ProductBarcode>();
            VendorProductPrice = new HashSet<VendorProductPrice>();
            VendorStock = new HashSet<VendorStock>();
        }

        public int ProductID { get; set; }
        public int VendorID { get; set; }
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public string Identifier { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastModified { get; set; }
        public int? LastModifiedBy { get; set; }

        public virtual ICollection<ProductBarcode> ProductBarcode { get; set; }
        public virtual ICollection<VendorProductPrice> VendorProductPrice { get; set; }
        public virtual ICollection<VendorStock> VendorStock { get; set; }
        public virtual Product Product { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
