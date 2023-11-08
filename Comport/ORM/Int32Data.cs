using System.Linq;
using System;

namespace Comport.ORM
{
    internal class Int32Data : DataPoint
    {
        public override int AddressCount => 2;

        internal override object GetData(short[] rawData, DataFormat format)
        {
            byte[] array = GetBytes(rawData, 0);
            switch (format.Order)
            {
                case DataOrder.ABCD:
                    array = new byte[4]
                    {
                    array[2],
                    array[3],
                    array[0],
                    array[1]
                    };
                    break;
                case DataOrder.DCBA:
                    array = new byte[4]
                    {
                    array[1],
                    array[0],
                    array[3],
                    array[2]
                    };
                    break;
                case DataOrder.BADC:
                    array = array.Reverse().ToArray();
                    break;
                default:
                    throw new ArgumentException("数据DataOrder类型错误:" + format.Order);
                case DataOrder.CDAB:
                    break;
            }

            return BitConverter.ToInt32(array, 0);
        }
    }
}