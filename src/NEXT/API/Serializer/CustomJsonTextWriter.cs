using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using System.Threading.Tasks;

namespace NEXT.API.Serializer
{
    public class CustomJsonTextWriter : JsonTextWriter
    {
        public CustomJsonTextWriter(TextWriter textWriter) : base(textWriter) { }

        public int CurrentDepth { get; private set; }

        public override void WriteStartObject()
        {
            CurrentDepth++;
            base.WriteStartObject();
        }

        public override void WriteEndObject()
        {
            CurrentDepth--;
            base.WriteEndObject();
        }
    }

    public class CustomContractResolver : DefaultContractResolver
    {
        private readonly Func<bool> _includeProperty;

        public CustomContractResolver(Func<bool> includeProperty)
        {
            _includeProperty = includeProperty;
        }

        protected override JsonProperty CreateProperty(
            MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            var shouldSerialize = property.ShouldSerialize;
            if (property.PropertyName == "_meta") {
            };
            property.ShouldSerialize = obj => _includeProperty() &&
                                              (shouldSerialize == null ||
                                               shouldSerialize(obj));
            return property;
        }
    }

    public class serializeResource
    {
        public static string SerializeObject(object obj, int maxDepth)
        {
            PropertyInfo info = obj.GetType().GetRuntimeProperty("_meta");
            using (var strWriter = new StringWriter())
            {
                using (var jsonWriter = new CustomJsonTextWriter(strWriter))
                {
                    Func<bool> include = () => jsonWriter.CurrentDepth <= maxDepth;
                    var resolver = new CustomContractResolver(include);
                    var serializer = new JsonSerializer { ContractResolver = resolver };
                    serializer.Serialize(jsonWriter, obj);
                }
                return strWriter.ToString();
            }
        }
    }
}
