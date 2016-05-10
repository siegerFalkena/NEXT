using System;
using System.Collections.Generic;

namespace NEXT.API.Models
{
    public partial class ChannelProduct
    {
        public ChannelProduct()
        {
            ChannelProductStock = new HashSet<ChannelProductStock>();
            ChanneProductPrice = new HashSet<ChanneProductPrice>();
        }

        public int ChannelID { get; set; }
        public int ProductID { get; set; }
        public DateTime? EndDateTime { get; set; }
        public DateTime StartDateTime { get; set; }

        public virtual ICollection<ChannelProductStock> ChannelProductStock { get; set; }
        public virtual ICollection<ChanneProductPrice> ChanneProductPrice { get; set; }
        public virtual Channel Channel { get; set; }
        public virtual Product Product { get; set; }
    }
}
