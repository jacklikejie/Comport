using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Net;
using System;

namespace Comport.ORM
{
    public abstract class DataPoint : ICloneable
    {
        public Address Address { get; set; }

        public abstract int AddressCount { get; }

        public static DataPoint Create<T>(string address, bool ignoreArea = false)
        {
            Type typeFromHandle = typeof(T);
            return Create(address, typeFromHandle, 1, 1, ignoreArea);
        }

        public static DataPoint Create<T>(string address, int length, bool ignoreArea = false) where T : IEnumerable
        {
            Type typeFromHandle = typeof(T);
            if (!typeFromHandle.IsArray && typeFromHandle != typeof(string))
            {
                throw new ArgumentException("数据类型错误，必须为数组或者字符串");
            }

            if (typeFromHandle == typeof(string))
            {
                return Create(address, typeFromHandle, 1, length, ignoreArea);
            }

            return Create(address, typeFromHandle, length, 1, ignoreArea);
        }

        public static DataPoint Create<T>(string address, int length, int elementLength, bool ignoreArea = false) where T : ICloneable, IList<string>, IStructuralComparable, IStructuralEquatable
        {
            Type typeFromHandle = typeof(T);
            if (typeFromHandle != typeof(string[]))
            {
                throw new ArgumentException("数据类型错误，类型必须为string[]");
            }

            return Create(address, typeFromHandle, length, elementLength, ignoreArea);
        }

        public static DataPoint Create(string address, Type type, int length = 1, int elementLength = 1, bool ignoreArea = false)
        {
            Address address2 = new Address(address, ignoreArea);
            if (type == null)
            {
                throw new ArgumentException("数据类型错误");
            }

            if (!address2.IsValid)
            {
                throw new ArgumentException("地址格式错误");
            }

            if (elementLength < 1)
            {
                throw new ArgumentException("长度必须大等于1");
            }

            if (length < 1)
            {
                throw new ArgumentException("个数必须大等于1");
            }

            Type type2 = (type.IsArray ? type.GetElementType() : type);
            DataPoint dataPoint;
            if (type2 == typeof(short))
            {
                dataPoint = new Int16Data();
            }
            else if (type2 == typeof(int))
            {
                dataPoint = new Int32Data();
            }
            else if (type2 == typeof(float))
            {
                dataPoint = new FloatData();
            }
            else if (type2 == typeof(double))
            {
                dataPoint = new DoubleData();
            }
            else if (type2 == typeof(string))
            {
                dataPoint = new StringData(elementLength);
            }
            else
            {
                if (!(type2 == typeof(bool)))
                {
                    throw new NotSupportedException("不支持的数据类型");
                }

                dataPoint = new BooleanData();
            }

            if (type.IsArray)
            {
                if (type2 == typeof(bool))
                {
                    dataPoint = new MultiBooleanData(length);
                }
                else
                {
                    Type type3 = typeof(MultiData<>).MakeGenericType(type2);
                    dataPoint = Activator.CreateInstance(type3, dataPoint, length) as DataPoint;
                }
            }

            dataPoint.Address = address2;
            return dataPoint;
        }

        public bool TryGetData(short[] rawData, Address startAddress, DataFormat format, out object data)
        {
            int index = GetIndex(startAddress);
            if (rawData == null || index < 0 || index > rawData.Length - AddressCount)
            {
                data = null;
                return false;
            }

            try
            {
                data = GetData(rawData.Take(index, AddressCount), format);
                return true;
            }
            catch
            {
                data = null;
                return false;
            }
        }

        internal abstract object GetData(short[] rawData, DataFormat format);

        private int GetIndex(Address startAddress)
        {
            if (!startAddress.IsValid)
            {
                return -1;
            }

            if (!Address.IsValid)
            {
                return -1;
            }

            if (Address.IgnoreArea != startAddress.IgnoreArea)
            {
                return -1;
            }

            if (Address.area != startAddress.area)
            {
                return -1;
            }

            return Address.address - startAddress.address;
        }

        protected byte[] GetBytes(short[] dataArr, int startIndex)
        {
            byte[] array = new byte[dataArr.Length * 2];
            for (int i = 0; i < dataArr.Length; i++)
            {
                byte[] bytes = BitConverter.GetBytes(dataArr[startIndex + i]);
                bytes.CopyTo(array, i * 2);
            }

            return array;
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }
    }
}