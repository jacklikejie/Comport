using System.Collections.Generic;
using System.Data;
using System;
using Newtonsoft.Json;

namespace Comport.ORM
{
    internal class TableDataConfig
    {
        public const string DB_PATH = "tableSchema.db";

        private SQLiteClient m_db = new SQLiteClient("tableSchema.db");

        public void CreateTable(string tableName)
        {
            try
            {
                List<string> list = new List<string>();
                list.Add("`ID` INTEGER PRIMARY KEY AUTOINCREMENT");
                list.Add("`Name` VARCHAR(50) NOT NULL");
                list.Add("`Property` VARCHAR(50) NOT NULL");
                list.Add("`Column` VARCHAR(50) NOT NULL");
                list.Add("`Address` VARCHAR(10)");
                list.Add("`EnableAddress` VARCHAR(10)");
                list.Add("`NullValue` VARCHAR(500)");
                list.Add("`PLCValueType` VARCHAR(10)");
                list.Add("`Length` VARCHAR(10)");
                list.Add("`Converter` VARCHAR(10)");
                list.Add("`ConvertArgs` VARCHAR(500)");
                list.Add("`JsonIgnore` INTEGER NOT NULL DEFAULT 0");
                string arg = string.Join(",", list);
                string sql = $"create TABLE if not exists {tableName} ({arg});";
                m_db.Excute(sql);
            }
            catch (Exception ex)
            {
                StaticResources.Logger.Error("创建数据表时出现异常：" + ex.Message);
            }
        }

        public List<DataItem> ReadDataItems(string tableName)
        {
            string sql = $"SELECT * from `{tableName}` order by ID asc;";
            DataTable dataTable = m_db.Query(sql);
            List<DataItem> list = new List<DataItem>();
            foreach (DataRow row in dataTable.Rows)
            {
                Type plcValueType = ((row["PLCValueType"] is DBNull) ? null : GetType(row["PLCValueType"].ToString()));
                NullValue nullValue = JsonConvert.DeserializeObject<NullValue>(row["NullValue"].ToString());
                IConverter converter = null;
                if (!string.IsNullOrEmpty(row["Converter"].ToString()))
                {
                    converter = ConvertFactory.Create(row["Converter"].ToString(), row["ConvertArgs"].ToString());
                    converter.NullValue = nullValue;
                }
                else
                {
                    DefaultConverter defaultConverter = new DefaultConverter(plcValueType);
                    defaultConverter.NullValue = nullValue;
                    converter = defaultConverter;
                }

                list.Add(new DataItem(plcValueType, converter)
                {
                    Property = row["Property"].ToString(),
                    EnableAddress = row["EnableAddress"].ToString(),
                    Address = row["Address"].ToString(),
                    Column = row["Column"].ToString(),
                    Name = row["Name"].ToString(),
                    Length = (string.IsNullOrEmpty(row["Length"].ToString()) ? 1 : int.Parse(row["Length"].ToString())),
                    JsonIgnore = (row["JsonIgnore"].ToString() == "1")
                });
            }

            return list;
        }

        public static Type GetType(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                return null;
            }
            switch (typeName.ToLower())
            {
                case "bool":
                    return typeof(bool);
                case "numeric":
                    return typeof(bool[]);
                case "int":
                    return typeof(int);
                case "int[]":
                    return typeof(int[]);
                case "short":
                    return typeof(short);
                case "short[]":
                    return typeof(short[]);
                case "string":
                    return typeof(string);
                case "string[]":
                    return typeof(string[]);
                case "double":
                    return typeof(double);
                case "double[]":
                    return typeof(double[]);
                case "float":
                    return typeof(float);
                case "float[]":
                    return typeof(float[]);
                default:
                    throw new ArgumentException("不支持的Converter类型：" + typeName);
            }
            //return typeName.ToLower() switch
            //{
            //    "bool" => typeof(bool),
            //    "bool[]" => typeof(bool[]),
            //    "int" => typeof(int),
            //    "int[]" => typeof(int[]),
            //    "short" => typeof(short),
            //    "short[]" => typeof(short[]),
            //    "string" => typeof(string),
            //    "string[]" => typeof(string[]),
            //    "double" => typeof(double),
            //    "double[]" => typeof(double[]),
            //    "float" => typeof(float),
            //    "float[]" => typeof(float[]),
            //    _ => throw new NotImplementedException("不支持的类型:" + typeName),
            //};
        }
    }
}