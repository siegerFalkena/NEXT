using System;
using System.Collections.Generic;

namespace NEXT.DB.Models
{
    public partial class VendorStock
    {
        public int ProductID { get; set; }
        public int VendorID { get; set; }
        public int StockTypeID { get; set; }
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public int? LastModifiedBy { get; set; }
        public int Quantity { get; set; }

        public virtual StockType StockType { get; set; }
        public virtual VendorProduct VendorProduct { get; set; }
    }
}
