using System;
using System.Collections.Generic;

namespace NEXT.DB.Models
{
    public partial class BarcodeType
    {
        public BarcodeType()
        {
            ProductBarcode = new HashSet<ProductBarcode>();
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductBarcode> ProductBarcode { get; set; }
    }
}
