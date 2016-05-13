using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

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
}
