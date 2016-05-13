using System;
using System.Collections.Generic;

namespace NEXT.DB.Models
{
    public partial class AttributeTranslation
    {
        public int AttributeID { get; set; }
        public int LanguageID { get; set; }
        public string Value { get; set; }

        public virtual Attribute Attribute { get; set; }
        public virtual Language Language { get; set; }
    }
}
