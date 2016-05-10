using System;
using System.Collections.Generic;

namespace NEXT.API.Models
{
    public partial class AttributeGroupTranslation
    {
        public int LanguageID { get; set; }
        public int AttributeGroupID { get; set; }
        public string AttributeGroupTranslationValue { get; set; }

        public virtual AttributeGroup AttributeGroup { get; set; }
        public virtual Language Language { get; set; }
    }
}
