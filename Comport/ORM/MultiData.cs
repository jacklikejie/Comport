using System.Linq;

namespace Comport.ORM
{
    internal class MultiData<T> : DataPoint
    {
        private DataPoint m_point;

        public int Length { get; set; }

        public override int AddressCount => m_point.AddressCount * Length;

        public MultiData(DataPoint elementPoint, int length)
        {
            m_point = elementPoint;
            Length = length;
        }

        internal override object GetData(short[] rawData, DataFormat format)
        {
            T[] array = new T[Length];
            for (int i = 0; i < Length; i++)
            {
                short[] rawData2 = rawData.Take(i * m_point.AddressCount, m_point.AddressCount);
                array[i] = (T)m_point.GetData(rawData2, format);
            }

            return array;
        }

        public override object Clone()
        {
            object obj = base.Clone();
            (obj as MultiData<T>).m_point = m_point.Clone() as DataPoint;
            return obj;
        }
    }
}