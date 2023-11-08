using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comport.ORM
{
    internal static class ArrayExtension
    {
        public static short[] Take(this short[] src, int startIndex, int length)
        {
            if (length < 0)
            {
                throw new ArgumentException("提取长度不能小于0");
            }

            if (startIndex < 0 || startIndex + length > src.Length)
            {
                throw new ArgumentOutOfRangeException("提取的数据超出数组范围");
            }

            short[] array = new short[length];
            Array.Copy(src, startIndex, array, 0, length);
            return array;
        }
    }
}
