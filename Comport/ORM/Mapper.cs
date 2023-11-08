using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace Comport.ORM
{
    public class Mapper<T> where T : class, IMapping
    {
        private static SetContext INIT_CONTEXT = new SetContext
        {
            SkipPropertyWithoutSetter = true
        };

        public T Object { get; set; }

        public List<DataItem> Configs { get; set; }

        public BulkReader Reader { get; private set; }

        public Mapper(string tableName, BlockDivideStrategy readerStrategy, bool ignoreArea = false)
        {
            TableDataConfig tableDataConfig = new TableDataConfig();
            Configs = tableDataConfig.ReadDataItems(tableName);
            Reader = CreateReader(Configs, readerStrategy, ignoreArea);
        }

        public void InitObject()
        {
            InitObject(Object);
        }

        public void InitObject(T obj)
        {
            if (Configs == null)
            {
                throw new InvalidOperationException("Configs为null。");
            }

            foreach (DataItem config in Configs)
            {
                if (config != null)
                {
                    obj.SetValue(config.PropertyArr, config.Converter.DefaultValue, INIT_CONTEXT);
                }
            }
        }

        private BulkReader CreateReader(List<DataItem> Datas, BlockDivideStrategy readerStrategy, bool ignoreArea)
        {
            try
            {
                List<DataPoint> list = new List<DataPoint>();
                foreach (DataItem Data in Datas)
                {
                    if (Data == null)
                    {
                        continue;
                    }

                    if (!string.IsNullOrEmpty(Data.EnableAddress))
                    {
                        list.Add(DataPoint.Create<bool>(Data.EnableAddress, ignoreArea));
                    }

                    if (!string.IsNullOrEmpty(Data.Address))
                    {
                        if (Data.PLCValueType == typeof(string))
                        {
                            list.Add(DataPoint.Create(Data.Address, Data.PLCValueType, 1, Data.Length, ignoreArea));
                        }
                        else
                        {
                            list.Add(DataPoint.Create(Data.Address, Data.PLCValueType, Data.Length, 1, ignoreArea));
                        }
                    }
                }

                return new BulkReader(readerStrategy, list.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("创建批量读取对象失败：" + ex.Message);
            }
        }

        //public Tuple<bool, string> Read(IPLCClient plc, bool printLog = false)
        //{
        //    if (plc == null)
        //    {
        //        return new Tuple<bool, string>(item1: false, "plc为null。");
        //    }

        //    BulkData data;
        //    Tuple<bool, string> tuple = Reader.ReadBulk(plc.ReadDataBlock16, plc.ReadData16, out data);
        //    if (!tuple.Item1)
        //    {
        //        return tuple;
        //    }

        //    if (printLog)
        //    {
        //        StaticResources.Logger.Info("读取到数据：" + JsonConvert.SerializeObject(data));
        //    }

        //    try
        //    {
        //        Dictionary<string[], object> values = ConvertAddressToProperty(data);
        //        Object.SetValues(values);
        //        return new Tuple<bool, string>(item1: true, "");
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Tuple<bool, string>(item1: false, ex.Message);
        //    }
        //}

        public Tuple<bool, string> Load(DataRow row, bool shrinkObjectToOneRow = false)
        {
            return Load(row, Object, shrinkObjectToOneRow);
        }

        public Tuple<bool, string> Load(DataRow row, T obj, bool shrinkObjectToOneRow = false)
        {
            if (row == null)
            {
                return new Tuple<bool, string>(item1: false, "row为null。");
            }

            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (DataColumn column in row.Table.Columns)
            {
                dictionary[column.ColumnName] = row[column.ColumnName];
            }

            return Load(dictionary, obj, shrinkObjectToOneRow);
        }

        public Tuple<bool, string> Load(Dictionary<string, object> rowData, bool shrinkObjectToOneRow = false)
        {
            return Load(rowData, Object, shrinkObjectToOneRow);
        }

        private Tuple<bool, string> Load(Dictionary<string, object> rowData, T obj, bool shrinkObjectToOneRow = false)
        {
            if (rowData == null)
            {
                return new Tuple<bool, string>(item1: false, "row数据为null。");
            }

            try
            {
                Dictionary<string[], object> values = ConvertColumnToProperty(rowData);
                obj.SetValues(values);
                if (shrinkObjectToOneRow)
                {
                    return ShrinkObjectToOneRow(obj);
                }

                return new Tuple<bool, string>(item1: true, "");
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(item1: false, ex.Message);
            }
        }

        private Tuple<bool, string> ShrinkObjectToOneRow(T obj)
        {
            if (obj == null)
            {
                return new Tuple<bool, string>(item1: false, "object为null。");
            }

            try
            {
                IOrderedEnumerable<DataItem> orderedEnumerable = from i in Configs
                                                                 where i != null && i.Converter is IMultiRow
                                                                 orderby i.PropertyArr.Length
                                                                 select i;
                foreach (DataItem item in orderedEnumerable)
                {
                    if (obj.TryGetValue(item.PropertyArr, out var value))
                    {
                        List<Crotch> branches = (item.Converter as IMultiRow).GetBranches(value, "");
                        if (branches.Count > 1)
                        {
                            IList list = value as IList;
                            object value2 = list[0];
                            list.Clear();
                            list.Add(value2);
                        }
                    }
                }

                return new Tuple<bool, string>(item1: true, "");
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(item1: false, ex.Message);
            }
        }

        public Dictionary<string, object> ToRowData()
        {
            return ToRowData(Object);
        }

        public Dictionary<string, object> ToRowData(T obj)
        {
            if (obj == null)
            {
                return null;
            }

            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (DataItem config in Configs)
            {
                if (config != null && !config.DBIgnore)
                {
                    if (config.Converter == null)
                    {
                        dictionary[config.Column] = obj.GetValue(config.PropertyArr);
                    }
                    else
                    {
                        dictionary[config.Column] = config.Converter.ToRow(obj.GetValue(config.PropertyArr));
                    }
                }
            }

            return dictionary;
        }

        public Dictionary<string, object>[] ToRowDatas(bool skipInvalidAddress = false)
        {
            return ToRowDatas(Object, skipInvalidAddress);
        }

        public Dictionary<string, object>[] ToRowDatas(T obj, bool skipInvalidAddress = false)
        {
            if (obj == null)
            {
                return null;
            }

            List<Branch> list = new List<Branch>();
            list.Add(new Branch());
            List<Branch> list2 = list;
            foreach (DataItem item in Configs)
            {
                if (item == null)
                {
                    continue;
                }

                if (item.Converter is IMultiRow)
                {
                    object value2 = obj.GetValue(item.PropertyArr);
                    List<Crotch> branches = (item.Converter as IMultiRow).GetBranches(value2, item.Property);
                    Branch.GenerateBranch(list2, branches);
                }
                else
                {
                    if (item.DBIgnore)
                    {
                        continue;
                    }

                    object value;
                    if (skipInvalidAddress)
                    {
                        if (!obj.TryGetValue(item.PropertyArr, out value))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        value = obj.GetValue(item.PropertyArr);
                    }

                    if (item.Converter != null)
                    {
                        value = item.Converter.ToRow(value);
                    }

                    list2.ForEach(delegate (Branch b)
                    {
                        b.SetValue(item.PropertyArr, item.Column, value);
                    });
                }
            }

            return list2.Select((Branch b) => b.RowData).ToArray();
        }

        public string ToJson()
        {
            return ToJson(Object);
        }

        public string ToJson(T obj)
        {
            if (obj == null)
            {
                return null;
            }

            string value = JsonConvert.SerializeObject(obj);
            TmpClass tmpClass = JsonConvert.DeserializeObject<TmpClass>(value, new JsonConverter[1] { TmpConverter.INSTANCE });
            foreach (DataItem config in Configs)
            {
                if (config != null && config.Converter != null && obj.TryGetValue(config.PropertyArr, out var value2))
                {
                    value2 = config.Converter.ToRow(value2);
                    if (value2 != null && !(value2 is string) && !value2.GetType().IsPrimitive)
                    {
                        value2 = JsonConvert.DeserializeObject<object>(JsonConvert.SerializeObject(value2), new JsonConverter[1] { TmpConverter.INSTANCE });
                    }

                    tmpClass.SetValue(config.PropertyArr, value2);
                }
            }

            foreach (DataItem config2 in Configs)
            {
                if (config2 != null && config2.JsonIgnore)
                {
                    tmpClass.TryRemoveKey(config2.PropertyArr);
                }
            }

            return JsonConvert.SerializeObject(tmpClass);
        }

        private Dictionary<string[], object> ConvertAddressToProperty(BulkData dataDict)
        {
            if (Configs == null)
            {
                throw new InvalidOperationException("没有映射数据");
            }

            if (dataDict == null)
            {
                return null;
            }

            Dictionary<string[], object> dictionary = new Dictionary<string[], object>();
            foreach (DataItem config in Configs)
            {
                if (config == null || !dataDict.ContainsKey(config.Address))
                {
                    continue;
                }

                try
                {
                    bool flag = string.IsNullOrEmpty(config.EnableAddress) || (bool)dataDict[config.EnableAddress];
                    object plcValue = dataDict[config.Address].Get(config.PLCValueType);
                    if (flag)
                    {
                        dictionary[config.PropertyArr] = config.Converter.FromPLC(plcValue);
                    }
                    else
                    {
                        dictionary[config.PropertyArr] = Convert.ChangeType(config.Converter.NullValue.Object, config.Converter.ObjectType);
                    }
                }
                catch (Exception ex)
                {
                    object obj = ((dataDict != null && dataDict.ContainsKey(config.Address)) ? dataDict[config.Address] : null);
                    string message = $"转换PLC数据[{config.Address}]({obj})为[{config.Property}]时出错：{ex.Message}";
                    throw new Exception(message);
                }
            }

            return dictionary;
        }

        private Dictionary<string[], object> ConvertColumnToProperty(Dictionary<string, object> dataDict)
        {
            if (Configs == null)
            {
                throw new InvalidOperationException("没有映射数据");
            }

            if (dataDict == null)
            {
                return null;
            }

            Dictionary<string[], object> dictionary = new Dictionary<string[], object>();
            foreach (DataItem config in Configs)
            {
                if (config != null && !config.DBIgnore && dataDict.ContainsKey(config.Column))
                {
                    try
                    {
                        dictionary[config.PropertyArr] = config.Converter.FromRow((dataDict[config.Column] ?? "").ToString());
                    }
                    catch (Exception ex)
                    {
                        string message = $"转换数据库数据[{config.Column}]为[{config.Property}]时出错：{ex.Message}";
                        throw new Exception(message);
                    }
                }
            }

            return dictionary;
        }

        public string FindColumn(string property)
        {
            if (Configs == null)
            {
                throw new InvalidOperationException("没有映射数据");
            }

            foreach (DataItem config in Configs)
            {
                if (config != null && config.Property == property)
                {
                    return config.Column;
                }
            }

            return null;
        }

        public DataItem FindConfig(string property)
        {
            if (Configs == null)
            {
                throw new InvalidOperationException("没有映射数据");
            }

            foreach (DataItem config in Configs)
            {
                if (config != null && config.Property == property)
                {
                    return config;
                }
            }

            return null;
        }
    }
}
