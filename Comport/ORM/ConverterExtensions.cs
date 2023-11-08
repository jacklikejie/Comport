using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comport.ORM
{
    internal static class ConverterExtensions
    {
        public static object NotConvert(this IConverter converter, string rowValue)
        {
            if (converter.NullValue != null && converter.NullValue.IsRowNull(rowValue))
            {
                return converter.NullValue.Object;
            }

            return rowValue;
        }

        public static object NotConvert(this IConverter converter, object objectValue)
        {
            if (converter.NullValue != null && converter.NullValue.IsObjectNull(objectValue))
            {
                return converter.NullValue.Row;
            }

            return objectValue;
        }

        public static object Default(this Type type)
        {
            if (type == null)
            {
                return null;
            }

            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
