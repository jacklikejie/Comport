using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Collections;

namespace Comport.ORM
{
    internal class ListConverter : IConverter, IMultiRow
    {
        private string listJson = "";

        public NullValue NullValue { get; set; }

        public Type ObjectType { get; private set; }

        public bool MultiRow { get; set; }

        public object DefaultValue => JsonConvert.DeserializeObject(listJson, StaticResources.jsonSetting);

        public ListConverter(string argsJson)
        {
            string[] array = JsonConvert.DeserializeObject<string[]>(argsJson);
            int? initCapacity = ((array[0] == "null") ? null : new int?(int.Parse(array[0])));
            string className = array[1];
            bool flag = bool.Parse(array[2]);
            string elementJson = array[3];
            MultiRow = bool.Parse(array[4]);
            ObjectType = GetObjectType(className, flag);
            IList value = GenerateList(initCapacity, className, flag, elementJson);
            listJson = JsonConvert.SerializeObject(value, StaticResources.jsonSetting);
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

        private Type GetObjectType(string className, bool customDictType)
        {
            Type type = ((!customDictType) ? Type.GetType("System.Collections.Generic.List`1[[" + className + "]], mscorlib") : Type.GetType(className));
            if (type == null)
            {
                throw new ArgumentException("类名错误，" + className);
            }

            return type;
        }

        private IList GenerateList(int? initCapacity, string className, bool customListType, string elementJson)
        {
            Type objectType = GetObjectType(className, customListType);
            IList list = ((!initCapacity.HasValue) ? (Activator.CreateInstance(objectType) as IList) : ((!customListType) ? (Activator.CreateInstance(objectType, Activator.CreateInstance(Type.GetType(className).MakeArrayType(), initCapacity)) as IList) : (Activator.CreateInstance(objectType, initCapacity.Value) as IList)));
            if (elementJson != null)
            {
                Type listElementType = StaticResources.GetListElementType(list.GetType());
                if (listElementType == null)
                {
                    throw new ArgumentException("无法获取元素数据类型：" + list.GetType().FullName);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    list[i] = JsonConvert.DeserializeObject(elementJson, listElementType);
                }
            }

            return list;
        }

        public List<Crotch> GetBranches(object objectValue, string address)
        {
            IList list = objectValue as IList;
            if (list == null || !MultiRow)
            {
                return new List<Crotch>();
            }

            List<Crotch> list2 = new List<Crotch>();
            for (int i = 0; i < list.Count; i++)
            {
                list2.Add(new Crotch(address + "." + i));
            }

            return list2;
        }
    }
}