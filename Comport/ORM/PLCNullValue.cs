using System;

namespace Comport.ORM
{
    public class PLCNullValue
    {
        public string PLC { get; set; }

        public uint PLCDecimalPlace { get; set; }

        public bool IsPlcNull(object plcValue)
        {
            if (plcValue == null || plcValue is string)
            {
                if ((string)plcValue == PLC)
                {
                    return true;
                }
            }
            else if (Convert.ToDecimal(plcValue).ToString("f" + PLCDecimalPlace) == Format(PLC, PLCDecimalPlace))
            {
                return true;
            }

            return false;
        }

        private string Format(string value, uint decimalPlace)
        {
            if (decimal.TryParse(value, out var result))
            {
                return result.ToString("f" + decimalPlace);
            }

            return value;
        }
    }
}