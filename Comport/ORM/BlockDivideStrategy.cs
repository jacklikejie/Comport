using System.Collections.Generic;
using System.Linq;
using System;

namespace Comport.ORM
{
    public class BlockDivideStrategy
    {
        private class LoadingBlock
        {
            private Address Start;

            private int BlankLength;

            private List<DataPoint> Points;

            public int Length { get; private set; }

            public LoadingBlock()
            {
            }

            public LoadingBlock(DataPoint startPoint)
            {
                InitBlock(startPoint);
            }

            internal bool TryAppend(DataPoint point, int maxBlank, int maxLength)
            {
                if (Points == null || Points.Count == 0)
                {
                    InitBlock(point);
                    return true;
                }

                if (point.Address.address < Points[Points.Count - 1].Address.address)
                {
                    return false;
                }

                int num = Math.Max(0, point.Address.address - Start.address - Length) + BlankLength;
                if (maxBlank >= 0 && num > maxBlank)
                {
                    return false;
                }

                int num2 = Math.Max(Length, point.Address.address - Start.address + point.AddressCount);
                if (maxLength >= 0 && num2 > maxLength)
                {
                    return false;
                }

                Points.Add(point);
                BlankLength = num;
                Length = num2;
                return true;
            }

            internal BulkAddress Pack()
            {
                return new BulkAddress(Start, Length, Points.ToArray());
            }

            private void InitBlock(DataPoint startPoint)
            {
                Start = startPoint.Address;
                Length = startPoint.AddressCount;
                Points = new List<DataPoint> { startPoint };
            }
        }

        public int MaxBlank;

        public int MaxLength;

        public int MaxDivideNum;

        public BlockDivideStrategy(int maxBlank = int.MaxValue, int maxLength = int.MaxValue, int maxDivideNum = int.MaxValue)
        {
            MaxBlank = maxBlank;
            MaxLength = maxLength;
            MaxDivideNum = maxDivideNum;
        }

        internal List<BulkAddress> GetBulkAddress(params DataPoint[] dataPoints)
        {
            if (dataPoints == null || dataPoints.Length == 0)
            {
                return new List<BulkAddress>();
            }

            IEnumerable<IGrouping<string, DataPoint>> enumerable = from p in dataPoints
                                                                   group p by p.Address.area;
            List<BulkAddress> list = new List<BulkAddress>();
            List<DataPoint> list2 = new List<DataPoint>();
            foreach (IGrouping<string, DataPoint> item in enumerable)
            {
                if (MaxDivideNum > 0 && list.Count >= MaxDivideNum)
                {
                    list2.AddRange(item);
                    continue;
                }

                IOrderedEnumerable<DataPoint> orderedEnumerable = item.OrderBy((DataPoint p) => p.Address.address);
                LoadingBlock loadingBlock = new LoadingBlock();
                foreach (DataPoint item2 in orderedEnumerable)
                {
                    if (item2 == null)
                    {
                        throw new ArgumentNullException("地址对象为null");
                    }

                    if (!item2.Address.IsValid)
                    {
                        throw new ArgumentException("地址不正确：" + item2.Address.RawString);
                    }

                    if (loadingBlock == null || item2.AddressCount > MaxLength)
                    {
                        list2.Add(item2);
                    }
                    else if (!loadingBlock.TryAppend(item2, MaxBlank, MaxLength))
                    {
                        list.Add(loadingBlock.Pack());
                        if (MaxDivideNum > 0 && list.Count >= MaxDivideNum)
                        {
                            list2.Add(item2);
                            loadingBlock = null;
                        }
                        else
                        {
                            loadingBlock = new LoadingBlock(item2);
                        }
                    }
                }

                if (loadingBlock != null && loadingBlock.Length > 0)
                {
                    list.Add(loadingBlock.Pack());
                }
            }

            if (list2.Count > 0)
            {
                list.Add(new BulkAddress
                {
                    Points = list2.ToArray()
                });
            }

            return list;
        }
    }
}