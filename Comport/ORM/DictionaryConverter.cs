using Newtonsoft.Json;
using System;

namespace Comport.ORM
{
    internal class DictionaryConverter : IConverter
    {
        public NullValue NullValue { get; set; }

        public string InitJson { get; private set; }

        public Type ObjectType { get; private set; }

        public object DefaultValue => JsonConvert.DeserializeObject(InitJson, ObjectType);

        public DictionaryConverter(string argsJson)
        {
            string[] array = JsonConvert.DeserializeObject<string[]>(argsJson);
            ObjectType = GetObjectType(array[0], array[1], bool.Parse(array[3]));
            InitJson = array[2];
        }

        public object FromPLC(object plcValue)
        {
            throw new InvalidOperationException("不支持从PLC读取");
        }

        public object FromRow(string rowValue)
        {
            return DefaultValue;
        }

        public object ToRow(object objectValue)
        {
            return this.NotConvert(objectValue);
        }

        private Type GetObjectType(string keyType, string type, bool customDictType)
        {
            string text = ((!customDictType) ? $"System.Collections.Generic.Dictionary`2[[{keyType}],[{type}]], mscorlib" : type);
            Type type2 = Type.GetType(text);
            if (type2 == null)
            {
                throw new ArgumentException("类名错误，" + text);
            }

            return type2;
        }
    }
}