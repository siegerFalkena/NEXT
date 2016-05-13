using System;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;
using System.Reflection;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Serializer
{
    public class CustomContractResolver : DefaultContractResolver
    {
        Func<bool> _includeProperty { get; set; }

        public CustomContractResolver(Func<bool> includeProperty) : base()
        {
            _includeProperty = includeProperty;
        }


        protected override JsonProperty CreateProperty(
      MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            var shouldSerialize = property.ShouldSerialize;
            property.ShouldSerialize = obj => _includeProperty() &&
                                              (shouldSerialize == null ||
                                               shouldSerialize(obj));
            return property;
        }

    }
}
