using System.Linq;
using System.Net;
using System;

namespace Comport.ORM
{
    public class SingleBulkReader
    {
        private DataPoint[] m_dataPoints;
        private Address m_startAddr = default(Address);

        public string StartAddr => m_startAddr.RawString;

        public int Length { get; private set; }

        public int MaxLength { get; set; }

        public bool IsBlockValid => m_startAddr.IsValid && m_dataPoints != null && (m_dataPoints.Length <= 1 || MaxLength >= Length);

        public DataFormat Format { get; private set; }

        public SingleBulkReader(params DataPoint[] addrs)
        {
            MaxLength = int.MaxValue;
            Tuple<Address, int> bulkAddress = GetBulkAddress(addrs);
            if (bulkAddress != null)
            {
                m_startAddr = bulkAddress.Item1;
                Length = bulkAddress.Item2;
            }

            m_dataPoints = ((addrs == null) ? new DataPoint[0] : (from DataPoint a in addrs
                                                                  select a.Clone() as DataPoint).ToArray());
            Format = new DataFormat();
        }

        private Tuple<Address, int> GetBulkAddress(params DataPoint[] dataPoints)
        {
            if (dataPoints == null || dataPoints.Length == 0)
            {
                return null;
            }

            Address item = dataPoints[0].Address;
            int num = 0;
            for (int i = 0; i < dataPoints.Length; i++)
            {
                if (dataPoints[i] == null)
                {
                    throw new ArgumentNullException("地址对象为null");
                }

                Address address = dataPoints[i].Address;
                if (!address.IsValid)
                {
                    throw new ArgumentException("地址不正确：" + address.RawString);
                }

                if (address.IgnoreArea != item.IgnoreArea)
                {
                    return null;
                }

                if (address.area != item.area)
                {
                    return null;
                }

                if (address.address < item.address)
                {
                    num += item.address - address.address;
                    item = address;
                }

                num = Math.Max(num, address.address - item.address + dataPoints[i].AddressCount);
            }

            return new Tuple<Address, int>(item, num);
        }

        public Tuple<bool, string> ReadBulk(ReadBlockHandler readBlock, ReadSingleHandler readSingle, out BulkData data)
        {
            try
            {
                return ReadBulkPrivate(readBlock, readSingle, out data);
            }
            catch (Exception ex)
            {
                data = new BulkData();
                return new Tuple<bool, string>(item1: false, ex.Message);
            }
        }

        private Tuple<bool, string> ReadBulkPrivate(ReadBlockHandler readBlock, ReadSingleHandler readSingle, out BulkData data)
        {
            data = new BulkData();
            if (IsBlockValid)
            {
                short[] array = ReadRawData(m_startAddr, Length, readBlock, readSingle);
                if (array == null)
                {
                    string item = $"读取失败，块起始地址[{StartAddr}]，长度:{Length}。";
                    return new Tuple<bool, string>(item1: false, item);
                }

                return AnalyzeData(ref data, array, m_startAddr, m_dataPoints);
            }

            DataPoint[] dataPoints = m_dataPoints;
            foreach (DataPoint dataPoint in dataPoints)
            {
                string addressWithoutPoint = dataPoint.Address.AddressWithoutPoint;
                short[] array = ReadRawData(dataPoint.Address, dataPoint.AddressCount, readBlock, readSingle);
                if (array == null)
                {
                    string item = $"读取失败，块起始地址[{addressWithoutPoint}]，长度:{dataPoint.AddressCount}。";
                    return new Tuple<bool, string>(item1: false, item);
                }

                Tuple<bool, string> tuple = AnalyzeData(ref data, array, dataPoint.Address, dataPoint);
                if (!tuple.Item1)
                {
                    return tuple;
                }
            }

            return new Tuple<bool, string>(item1: true, "");
        }

        private short[] ReadRawData(Address startAddr, int length, ReadBlockHandler readBlock, ReadSingleHandler readSingle)
        {
            try
            {
                if (length == 1)
                {
                    if (readSingle == null)
                    {
                        return null;
                    }

                    if (!readSingle(startAddr.RawString, out var data))
                    {
                        return null;
                    }

                    return new short[1] { data };
                }

                if (readBlock == null)
                {
                    return null;
                }

                short[] arrData;
                if (MaxLength > length)
                {
                    if (!readBlock(startAddr.RawString, length, out arrData))
                    {
                        return null;
                    }

                    return arrData;
                }

                arrData = new short[length];
                int i = startAddr.address;
                int num2;
                for (int num = i + length - 1; i <= num; i += num2)
                {
                    num2 = Math.Max(1, Math.Min(MaxLength, num - i + 1));
                    string address = ((!startAddr.IgnoreArea) ? startAddr.area : "") + i;
                    if (!readBlock(address, (ushort)num2, out var arrData2) || arrData2.Length != num2)
                    {
                        return null;
                    }

                    arrData2.CopyTo(arrData, i - startAddr.address);
                }

                return arrData;
            }
            catch
            {
                return null;
            }
        }

        private Tuple<bool, string> AnalyzeData(ref BulkData data, short[] rawData, Address startAddress, params DataPoint[] dataPoints)
        {
            foreach (DataPoint dataPoint in dataPoints)
            {
                if (!dataPoint.TryGetData(rawData, startAddress, Format, out var data2))
                {
                    return new Tuple<bool, string>(item1: false, $"提取地址[{dataPoint.Address.RawString}]数据失败，块起始地址[{StartAddr}]，长度:{Length}。");
                }

                string rawString = dataPoint.Address.RawString;
                if (!data.ContainsKey(rawString))
                {
                    data[rawString] = new DataBag();
                }

                data[rawString][data2.GetType()] = data2;
            }

            return new Tuple<bool, string>(item1: true, "");
        }
    }
}