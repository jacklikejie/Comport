using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace Comport.ORM
{
    internal class MappingConverter : IConverter
    {
        public NullValue NullValue { get; set; }

        public Type ObjectType => typeof(string);

        public object DefaultValue => null;

        public Dictionary<string, string> Mapping { get; set; }

        public string Default { get; set; }

        public MappingConverter(string argsJson)
        {
            object[] array = JsonConvert.DeserializeObject<object[]>(argsJson);
            Default = array[0] as string;
            Mapping = JsonConvert.DeserializeObject<Dictionary<string, string>>(array[1].ToString());
        }

        public object FromPLC(object plcValue)
        {
            if (Mapping.TryGetValue(plcValue.ToString(), out var value))
            {
                return value;
            }

            if (Default != null)
            {
                return Default;
            }

            return string.Concat("未知值(", plcValue, ")");
        }

        public object FromRow(string rowValue)
        {
            return this.NotConvert(rowValue);
        }

        public object ToRow(object objectValue)
        {
            return this.NotConvert(objectValue);
        }
    }
}