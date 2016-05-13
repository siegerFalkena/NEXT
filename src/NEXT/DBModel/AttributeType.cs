using System;
using System.Collections.Generic;

namespace NEXT.DB.Models
{
    public partial class AttributeType
    {
        public AttributeType()
        {
            Attribute = new HashSet<Attribute>();
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Attribute> Attribute { get; set; }
    }
}
