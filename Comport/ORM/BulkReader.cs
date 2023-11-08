using System.Collections.Generic;
using System;
using System.Linq;

namespace Comport.ORM
{
    public class BulkReader
    {
        private List<SingleBulkReader> m_readers = null;

        private BlockDivideStrategy m_strategy;

        public DataOrder Order
        {
            set
            {
                if (m_readers != null)
                {
                    m_readers.ForEach(delegate (SingleBulkReader r)
                    {
                        r.Format.Order = value;
                    });
                }
            }
        }

        public bool ReverseString
        {
            set
            {
                if (m_readers != null)
                {
                    m_readers.ForEach(delegate (SingleBulkReader r)
                    {
                        r.Format.ReverseString = value;
                    });
                }
            }
        }

        public bool EndWithZeroChar
        {
            set
            {
                if (m_readers != null)
                {
                    m_readers.ForEach(delegate (SingleBulkReader r)
                    {
                        r.Format.EndWithZeroChar = value;
                    });
                }
            }
        }

        public int BlockCount
        {
            get
            {
                if (m_readers == null || m_readers.Any((SingleBulkReader r) => !r.IsBlockValid))
                {
                    return -1;
                }

                return m_readers.Count;
            }
        }

        public BulkReader(params DataPoint[] addrs)
        {
            m_strategy = new BlockDivideStrategy();
            List<BulkAddress> bulkAddress = m_strategy.GetBulkAddress(addrs);
            m_readers = GenerateReaders(bulkAddress);
        }

        public BulkReader(BlockDivideStrategy strategy, params DataPoint[] addrs)
        {
            m_strategy = strategy;
            List<BulkAddress> bulkAddress = m_strategy.GetBulkAddress(addrs);
            m_readers = GenerateReaders(bulkAddress);
        }

        public Tuple<bool, string> ReadBulk(ReadBlockHandler readBlock, ReadSingleHandler readSingle, out BulkData data)
        {
            data = new BulkData();
            if (m_readers == null || m_readers.Count == 0)
            {
                return new Tuple<bool, string>(item1: false, "读取器初始化失败");
            }

            foreach (SingleBulkReader reader in m_readers)
            {
                if (reader == null)
                {
                    return new Tuple<bool, string>(item1: false, "读取器为null");
                }

                BulkData data2;
                Tuple<bool, string> tuple = reader.ReadBulk(readBlock, readSingle, out data2);
                if (!tuple.Item1)
                {
                    return tuple;
                }

                foreach (KeyValuePair<string, DataBag> item in data2)
                {
                    if (!data.ContainsKey(item.Key))
                    {
                        data[item.Key] = item.Value;
                    }
                    else
                    {
                        data[item.Key].Combine(item.Value);
                    }
                }
            }

            return new Tuple<bool, string>(item1: true, "");
        }

        private List<SingleBulkReader> GenerateReaders(List<BulkAddress> blocks)
        {
            if (blocks == null)
            {
                return null;
            }

            List<SingleBulkReader> list = new List<SingleBulkReader>();
            foreach (BulkAddress block in blocks)
            {
                SingleBulkReader singleBulkReader = new SingleBulkReader(block.Points);
                singleBulkReader.MaxLength = m_strategy.MaxLength;
                SingleBulkReader item = singleBulkReader;
                list.Add(item);
            }

            return list;
        }
    }
}