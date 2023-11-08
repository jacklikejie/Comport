using System;

namespace Comport.ORM
{
    internal class TimeConverter : IConverter
    {
        public NullValue NullValue { get; set; }

        public Type ObjectType => typeof(DateTime);

        public object DefaultValue => default(DateTime);

        public string Format { get; set; }

        public TimeConverter(string format)
        {
            Format = format;
        }

        public object FromPLC(object plcValue)
        {
            return DateTime.Parse(plcValue.ToString());
        }

        public object FromRow(string rowValue)
        {
            if (NullValue != null && NullValue.IsRowNull(rowValue))
            {
                return DateTime.Parse(NullValue.Object);
            }

            return DateTime.Parse(rowValue);
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

            return ((DateTime)objectValue).ToString(Format);
        }
    }
}