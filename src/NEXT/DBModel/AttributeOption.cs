using System;
using System.Collections.Generic;

namespace NEXT.DB.Models
{
    public partial class AttributeOption
    {
        public AttributeOption()
        {
            AttributeOptionTranslation = new HashSet<AttributeOptionTranslation>();
            ProductAttributeOption = new HashSet<ProductAttributeOption>();
        }

        public int AttributeID { get; set; }
        public string Value { get; set; }

        public virtual ICollection<AttributeOptionTranslation> AttributeOptionTranslation { get; set; }
        public virtual ICollection<ProductAttributeOption> ProductAttributeOption { get; set; }
        public virtual Attribute Attribute { get; set; }
    }
}
