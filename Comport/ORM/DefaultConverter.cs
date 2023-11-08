using System;

namespace Comport.ORM
{
    internal class DefaultConverter : IConverter
    {
        public NullValue NullValue { get; set; }

        public Type ObjectType { get; private set; }

        public object DefaultValue => ObjectType.Default();

        public DefaultConverter(Type plcValueType)
        {
            ObjectType = plcValueType;
        }

        public object FromPLC(object plcValue)
        {
            return plcValue;
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