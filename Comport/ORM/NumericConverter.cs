using Newtonsoft.Json;
using System;

namespace Comport.ORM
{
    internal class NumericConverter : IConverter
    {
        public NullValue NullValue { get; set; }

        public PLCNullValue PLCNull { get; set; }

        public uint DecimalPlace { get; set; }

        public double Multiple { get; set; }

        public Type ObjectType { get; set; }

        public object DefaultValue => ObjectType.Default();

        public NumericConverter(string argsJson)
        {
            string[] array = JsonConvert.DeserializeObject<string[]>(argsJson);
            Multiple = double.Parse(array[0]);
            DecimalPlace = uint.Parse(array[1]);
            ObjectType = TableDataConfig.GetType(array[2]);
            if (array.Length >= 4)
            {
                PLCNull = JsonConvert.DeserializeObject<PLCNullValue>(array[3]);
            }

            if (ObjectType == typeof(int) || ObjectType == typeof(short))
            {
                DecimalPlace = 0u;
            }
        }

        public object FromPLC(object plcValue)
        {
            if (PLCNull != null && PLCNull.IsPlcNull(plcValue))
            {
                if (NullValue != null)
                {
                    return Convert.ChangeType(NullValue.Object, ObjectType);
                }

                return DefaultValue;
            }

            double num = Math.Round(Convert.ToDouble(plcValue) * Multiple, (int)DecimalPlace, MidpointRounding.AwayFromZero);
            if (ObjectType == typeof(string))
            {
                return num.ToString("f" + DecimalPlace);
            }

            return Convert.ChangeType(num, ObjectType);
        }

        public object FromRow(string rowValue)
        {
            if (NullValue != null && NullValue.IsRowNull(rowValue))
            {
                return Convert.ChangeType(NullValue.Object, ObjectType);
            }

            double num = Math.Round(double.Parse(rowValue), (int)DecimalPlace, MidpointRounding.AwayFromZero);
            if (ObjectType == typeof(string))
            {
                return num.ToString("f" + DecimalPlace);
            }

            return Convert.ChangeType(num, ObjectType);
        }

        public object ToRow(object objectValue)
        {
            if (NullValue != null && NullValue.IsObjectNull(objectValue))
            {
                return NullValue.Row;
            }

            if (objectValue == null)
            {
                return "";
            }

            double num = Math.Round(Convert.ToDouble(objectValue), (int)DecimalPlace, MidpointRounding.AwayFromZero);
            if (ObjectType == typeof(string))
            {
                return num.ToString("f" + DecimalPlace);
            }

            return Convert.ChangeType(num, ObjectType);
        }
    }
}