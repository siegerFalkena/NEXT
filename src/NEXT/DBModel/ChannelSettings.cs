using System;
using System.Collections.Generic;

namespace NEXT.DB.Models
{
    public partial class ChannelSettings
    {
        public int ID { get; set; }
        public int ChannelID { get; set; }

        public virtual Channel Channel { get; set; }
    }
}
