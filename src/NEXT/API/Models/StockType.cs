using System;
using System.Collections.Generic;

namespace NEXT.API.Models
{
    public partial class StockType
    {
        public StockType()
        {
            ChannelProductStock = new HashSet<ChannelProductStock>();
            VendorStock = new HashSet<VendorStock>();
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ChannelProductStock> ChannelProductStock { get; set; }
        public virtual ICollection<VendorStock> VendorStock { get; set; }
    }
}
