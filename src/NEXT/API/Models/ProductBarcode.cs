using System;
using System.Collections.Generic;

namespace NEXT.API.Models
{
    public partial class ProductBarcode
    {
        public int BarcodeTypeID { get; set; }
        public string Barcode { get; set; }
        public int ProductID { get; set; }
        public int VendorID { get; set; }
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDefault { get; set; }
        public DateTime? LastModified { get; set; }
        public int? LastModifiedBy { get; set; }

        public virtual BarcodeType BarcodeType { get; set; }
        public virtual VendorProduct VendorProduct { get; set; }
    }
}
