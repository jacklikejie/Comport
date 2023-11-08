using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comport.ORM
{
    public class NullValue
    {
        public string Object { get; set; }

        public string DataFormat { get; set; }

        public string Row { get; set; }

        public bool IsObjectNull(object objectValue)
        {
            try
            {
                if (objectValue == null)
                {
                    return Object == null;
                }

                if (objectValue is DateTime)
                {
                    return ((DateTime)objectValue).ToString(DataFormat) == DateTime.Parse(Object).ToString(DataFormat);
                }

                if (objectValue is string)
                {
                    return Format(objectValue as string, DataFormat) == Format(Object, DataFormat);
                }

                return Convert.ToDecimal(objectValue).ToString(DataFormat) == Format(Object, DataFormat);
            }
            catch
            {
                return false;
            }
        }

        public bool IsRowNull(string rowValue)
        {
            return rowValue == Row;
        }

        private string Format(string value, string dataFormat)
        {
            if (decimal.TryParse(value, out var result))
            {
                return result.ToString(dataFormat);
            }

            if (DateTime.TryParse(value, out var result2))
            {
                return result2.ToString(dataFormat);
            }

            return value;
        }
    }
}
