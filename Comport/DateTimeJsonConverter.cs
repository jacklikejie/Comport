using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System;

namespace Comport
{
    class DateTimeJsonConverter : IsoDateTimeConverter
    {
        public DateTimeJsonConverter()
        {
            DateTimeFormat = "yyyy-MM-ddTHH:mm:ss";
        }

        public DateTimeJsonConverter(string format)
        {
            DateTimeFormat = format;
        }
    }

    class EnumJsonConverter : JsonConverter
    {
        private EnumJsonConverter.By type = By.Name;
        public EnumJsonConverter()
        {
        }
        public EnumJsonConverter(EnumJsonConverter.By type)
        {
            this.type = type;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsEnum;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (type)
            {
                case By.Value:
                    return Enum.ToObject(objectType, int.Parse(reader.Value.ToString()));
                case By.Name:
                    return Enum.Parse(objectType, reader.Value.ToString());
                default:
                    throw new ArgumentException("错误的ConvertBy值");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            switch (this.type)
            {
                case By.Value:
                    writer.WriteValue(Convert.ToInt32(value).ToString()); break;
                case By.Name:
                    writer.WriteValue(value.ToString()); break;
                default:
                    throw new ArgumentException("错误的ConvertBy值");
            }
        }

        public enum By
        {
            Value,
            Name,
        }
    }
}