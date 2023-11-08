using Newtonsoft.Json;
using System;
using System.Collections;

namespace Comport.ORM
{
    internal class TmpConverter : JsonConverter
    {
        public static TmpConverter INSTANCE = new TmpConverter();

        public override bool CanConvert(Type objectType)
        {
            return !objectType.IsPrimitive && !objectType.IsEnum && objectType != typeof(TmpClass) && !typeof(IList).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value != null)
            {
                return serializer.Deserialize(reader);
            }

            object obj = serializer.Deserialize(reader);
            if (reader.TokenType == JsonToken.EndObject)
            {
                obj = JsonConvert.DeserializeObject<TmpClass>(obj.ToString(), new JsonConverter[1] { INSTANCE });
            }
            else if (reader.TokenType == JsonToken.EndArray)
            {
                obj = JsonConvert.DeserializeObject<object[]>(obj.ToString(), new JsonConverter[1] { INSTANCE });
            }

            return obj;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }
}