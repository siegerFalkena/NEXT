using System;
using System.Collections.Generic;

namespace NEXT.API.Models
{
    public partial class Language
    {
        public Language()
        {
            AttributeGroupTranslation = new HashSet<AttributeGroupTranslation>();
            AttributeOptionTranslation = new HashSet<AttributeOptionTranslation>();
            AttributeTranslation = new HashSet<AttributeTranslation>();
            ProductAttributeValue = new HashSet<ProductAttributeValue>();
        }

        public int ID { get; set; }
        public bool IsDefault { get; set; }
        public string ISOCode { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AttributeGroupTranslation> AttributeGroupTranslation { get; set; }
        public virtual ICollection<AttributeOptionTranslation> AttributeOptionTranslation { get; set; }
        public virtual ICollection<AttributeTranslation> AttributeTranslation { get; set; }
        public virtual ICollection<ProductAttributeValue> ProductAttributeValue { get; set; }
    }
}
