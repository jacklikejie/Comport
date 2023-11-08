namespace Comport.ORM
{
    internal class Int16Data : DataPoint
    {
        public override int AddressCount => 1;

        internal override object GetData(short[] rawData, DataFormat format)
        {
            return rawData[0];
        }
    }
}