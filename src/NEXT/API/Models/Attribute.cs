using System;
using System.Collections.Generic;

namespace NEXT.API.Models
{
    public partial class Attribute
    {
        public Attribute()
        {
            AttributeGroupAttribute = new HashSet<AttributeGroupAttribute>();
            AttributeOption = new HashSet<AttributeOption>();
            AttributeTranslation = new HashSet<AttributeTranslation>();
            ProductAttributeValue = new HashSet<ProductAttributeValue>();
        }

        public int ID { get; set; }
        public int AttributeTypeID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AttributeGroupAttribute> AttributeGroupAttribute { get; set; }
        public virtual ICollection<AttributeOption> AttributeOption { get; set; }
        public virtual ICollection<AttributeTranslation> AttributeTranslation { get; set; }
        public virtual ICollection<ProductAttributeValue> ProductAttributeValue { get; set; }
        public virtual AttributeType AttributeType { get; set; }
    }
}
