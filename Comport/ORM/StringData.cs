using System.Linq;

namespace Comport.ORM
{
    internal class StringData : DataPoint
    {
        private int m_addressCount;

        public override int AddressCount => m_addressCount;

        public StringData(int length)
        {
            m_addressCount = length;
        }

        internal override object GetData(short[] rawData, DataFormat format)
        {
            byte[] bytes = GetBytes(rawData, 0);
            if (!format.ReverseString)
            {
                bytes = ReverseBytes(bytes);
            }

            string text = format.Encoding.GetString(bytes);
            if (format.EndWithZeroChar)
            {
                if (text.Contains('\0'))
                {
                    text = text.Substring(0, text.IndexOf('\0'));
                }
            }
            else
            {
                text = text.Replace("\0", "");
            }

            return text;
        }

        private byte[] ReverseBytes(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return bytes;
            }

            int num = bytes.Length + bytes.Length % 2;
            byte[] array = new byte[num];
            for (int i = 0; i < bytes.Length / 2; i++)
            {
                array[i * 2] = bytes[i * 2 + 1];
                array[i * 2 + 1] = bytes[i * 2];
            }

            if (bytes.Length % 2 == 1)
            {
                array[array.Length - 1] = bytes[bytes.Length - 1];
            }

            return array;
        }
    }
}