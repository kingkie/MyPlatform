using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Yu3zx.Json
{
    public class CustomConverter<T>: JsonConverter
    {
        //private static readonly string ISCALAR_FULLNAME = typeof(Interfaces.IScalar).FullName;
        //private static readonly string IENTITY_FULLNAME = typeof(Interfaces.IEntity).FullName;

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(T));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<T>(reader);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Left as an exercise to the reader :)
            serializer.Serialize(writer, value);
        }
    }
}
