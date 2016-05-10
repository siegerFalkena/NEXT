using System;
using System.Collections.Generic;

namespace NEXT.API.Models
{
    public partial class AttributeGroup
    {
        public AttributeGroup()
        {
            AttributeGroupAttribute = new HashSet<AttributeGroupAttribute>();
            AttributeGroupTranslation = new HashSet<AttributeGroupTranslation>();
        }

        public int AttributeGroupID { get; set; }
        public string AttributeGroupName { get; set; }

        public virtual ICollection<AttributeGroupAttribute> AttributeGroupAttribute { get; set; }
        public virtual ICollection<AttributeGroupTranslation> AttributeGroupTranslation { get; set; }
    }
}
