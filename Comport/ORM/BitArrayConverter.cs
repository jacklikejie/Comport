using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Comport.ORM
{
    internal class BitArrayConverter : IConverter
    {
        public NullValue NullValue { get; set; }

        public Type ObjectType => typeof(string);

        public object DefaultValue => null;

        public Dictionary<int, Tuple<string, string>> Mapping { get; set; }

        public string Default { get; set; }

        public string Spliter { get; set; }

        public BitArrayConverter(string argsJson)
        {
            object[] array = JsonConvert.DeserializeObject<object[]>(argsJson);
            Default = (array[0] as string) ?? "";
            Spliter = (array[1] as string) ?? "";
            Mapping = JsonConvert.DeserializeObject<Dictionary<int, Tuple<string, string>>>(array[2].ToString());
        }

        public object FromPLC(object plcValue)
        {
            bool[] array = (bool[])plcValue;
            List<string> list = new List<string>(array.Length);
            for (int i = 0; i < array.Length; i++)
            {
                if (!Mapping.ContainsKey(i))
                {
                    continue;
                }

                if (array[i])
                {
                    if (!string.IsNullOrEmpty(Mapping[i].Item1))
                    {
                        list.Add(Mapping[i].Item1);
                    }
                }
                else if (!string.IsNullOrEmpty(Mapping[i].Item2))
                {
                    list.Add(Mapping[i].Item2);
                }
            }

            if (list.Count == 0)
            {
                return Default;
            }

            return string.Join(Spliter, list.Distinct());
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