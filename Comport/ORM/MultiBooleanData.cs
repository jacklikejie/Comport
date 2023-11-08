using System.Linq;

namespace Comport.ORM
{
    internal class MultiBooleanData : DataPoint
    {
        public int Length { get; set; }

        public override int AddressCount
        {
            get
            {
                int num = base.Address.point + Length;
                return num / 16 + ((num % 16 != 0) ? 1 : 0);
            }
        }

        public MultiBooleanData(int length)
        {
            Length = length;
        }

        internal override object GetData(short[] rawData, DataFormat format)
        {
            bool[] array = new bool[Length];
            for (int i = 0; i < Length; i++)
            {
                int num = i + base.Address.point;
                short num2 = rawData.ElementAt(num / 16);
                num %= 16;
                array[i] = (num2 & (1 << num)) >> num == 1;
            }

            return array;
        }
    }
}