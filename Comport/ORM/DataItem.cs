using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Resources.ResXFileRef;

namespace Comport.ORM
{
    public class DataItem
    {
        private string m_property = null;

        internal string[] PropertyArr = null;

        public string Name { get; set; }

        public string Property
        {
            get
            {
                return m_property;
            }
            set
            {
                m_property = value;
                PropertyArr = AnalyzeAddress(value);
            }
        }

        public string Column { get; set; }

        public bool DBIgnore => string.IsNullOrEmpty(Column);

        public IConverter Converter { get; set; }

        public bool JsonIgnore { get; set; }

        public string EnableAddress { get; set; }

        public string Address { get; set; }

        public Type PLCValueType { get; private set; }
        public int Length { get; set; }

        internal DataItem(Type plcValueType, IConverter converter)
        {
            PLCValueType = plcValueType;
            Converter = converter;
        }

        internal static string[] AnalyzeAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                return null;
            }

            return address.Split('.');
        }
    }
}
