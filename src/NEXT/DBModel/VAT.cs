using System;
using System.Collections.Generic;

namespace NEXT.DB.Models
{
    public partial class VAT
    {
        public VAT()
        {
            ChanneProductPrice = new HashSet<ChanneProductPrice>();
            VendorProductPrice = new HashSet<VendorProductPrice>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Percentage { get; set; }

        public virtual ICollection<ChanneProductPrice> ChanneProductPrice { get; set; }
        public virtual ICollection<VendorProductPrice> VendorProductPrice { get; set; }
    }
}
