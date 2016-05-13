using System;
using System.Collections.Generic;

namespace NEXT.DB.Models
{
    public partial class AttributeGroupAttribute
    {
        public int AttributeGroupID { get; set; }
        public int AttributeID { get; set; }

        public virtual AttributeGroup AttributeGroup { get; set; }
        public virtual Attribute Attribute { get; set; }
    }
}
