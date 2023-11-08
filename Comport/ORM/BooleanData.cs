using System.Linq;

namespace Comport.ORM
{
    internal class BooleanData : DataPoint
    {
        public override int AddressCount => 1;

        internal override object GetData(short[] rawData, DataFormat format)
        {
            return (rawData.First() & (1 << base.Address.point)) >> base.Address.point == 1;
        }
    }
}