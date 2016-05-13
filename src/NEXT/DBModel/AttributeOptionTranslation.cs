using System;
using System.Collections.Generic;

namespace NEXT.DB.Models
{
    public partial class AttributeOptionTranslation
    {
        public int AttributeID { get; set; }
        public string Value { get; set; }
        public int LanguageID { get; set; }
        public string TranslationValue { get; set; }

        public virtual Language Language { get; set; }
        public virtual AttributeOption AttributeOption { get; set; }
    }
}
