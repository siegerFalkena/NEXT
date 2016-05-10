using System;
using System.Collections.Generic;

namespace NEXT.API.Models
{
    public partial class Channel
    {
        public Channel()
        {
            ChannelProduct = new HashSet<ChannelProduct>();
            ChannelSettings = new HashSet<ChannelSettings>();
        }

        public int ChannelID { get; set; }
        public string Name { get; set; }
        public int? ParentChannelID { get; set; }

        public virtual ICollection<ChannelProduct> ChannelProduct { get; set; }
        public virtual ICollection<ChannelSettings> ChannelSettings { get; set; }
        public virtual Channel ParentChannel { get; set; }
        public virtual ICollection<Channel> InverseParentChannel { get; set; }
    }
}
