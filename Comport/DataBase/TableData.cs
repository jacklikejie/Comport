using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comport.DataBase
{
    class TableData
    {
        public const string ID_COL = "ID";
        public const string TIME_COL = "Time";
        public const string UPLOAD_RESULT_COL = "UploadResult";
        public const string TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";
        public Dictionary<string, string> ColumnDict = null;
        private bool AddIDColumn = false;
        static MySQLClient MySqlDB = null;                            //用于存储数据

        public string TableName { get; private set; }

        public TableData(string tableName, string configFile)
        {
            this.TableName = tableName;
            this.ColumnDict = ReadColumnConfig(configFile);
        }

        public TableData(string tableName, Dictionary<string, string> columns, bool IDColumn = false)
        {
            this.TableName = tableName;
            this.ColumnDict = columns;
            this.AddIDColumn = IDColumn;
        }

        public bool CreateTable(string[] primaryKey = null, string[] indexCols = null, Dictionary<string, string> customColType = null)
        {
            try
            {
                List<string> colSQLs = new List<string>();
                if (AddIDColumn)
                {
                    colSQLs.Add("ID INTEGER PRIMARY KEY AUTO_INCREMENT");
                }

                foreach (var item in ColumnDict.Keys)
                {
                    string type = (customColType != null && customColType.ContainsKey(item)) ? customColType[item] : "VARCHAR(50)";
                    colSQLs.Add(item + " " + type);
                }
                if (primaryKey != null && primaryKey.Length > 0)
                {
                    colSQLs.Add(string.Format("PRIMARY KEY ({0})", string.Join(",", primaryKey)));
                }
                if (indexCols != null && indexCols.Length > 0)
                {
                    colSQLs.Add(string.Format("INDEX ({0})", string.Join(",", indexCols)));
                }
                string colSQL = string.Join(",", colSQLs);

                string createSql = string.Format("create TABLE if not exists {0} ({1});", TableName, colSQL);
                return MySqlDB.ExecuteCmd(createSql) >= 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("创建数据表时出现异常：" + ex.Message);
                return false;
            }
        }

        public bool ChangeColumnType(string column, string type)
        {
            try
            {
                string alterSql = string.Format("ALTER TABLE {0} MODIFY COLUMN `{1}` {2};", TableName, column, type);
                return MySqlDB.ExecuteCmd(alterSql) >= 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改数据表时出现异常：" + ex.Message);
                return false;
            }
        }

        private Dictionary<string, string> ReadColumnConfig(string configFile)
        {
            var dict = new Dictionary<string, string>();
            foreach (var item in IniTool.GetKeys(TableName, configFile))
            {
                dict[item] = IniTool.ReadString(TableName, item, null, configFile);
            }
            return dict;
        }

        internal bool Replace(Dictionary<string, string> dataDict)
        {
            if (dataDict == null || dataDict.Count == 0)
            {
                MessageBox.Show("插入数据失败，数据为空");
                return false;
            }

            var cols = this.ColumnDict.Keys.ToArray();
            var datas = new string[cols.Length];
            for (int i = 0; i < cols.Length; i++)
            {
                datas[i] = dataDict.ContainsKey(cols[i]) ? dataDict[cols[i]] : "";
            }

            string insertSQL = string.Format("replace into {0} (`{1}`) Values('{2}');",
                                    TableName,
                                    string.Join("`,`", cols),
                                    string.Join("','", SQL.SafeText(datas)));
            try
            {
                if (MySqlDB.ExecuteCmd(insertSQL) <= 0)
                {
                    MessageBox.Show("插入数据失败。SQL：" + insertSQL);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("插入数据时出现异常：{0},SQL:{1}", ex.Message+insertSQL);
                return false;
            }
            return true;
        }

        internal bool Insert(Dictionary<string, string> dataDict)
        {
            if (dataDict == null || dataDict.Count == 0)
            {
                MessageBox.Show("插入数据失败，数据为空");
                return false;
            }

            var cols = this.ColumnDict.Keys.ToArray();
            var datas = new string[cols.Length];
            for (int i = 0; i < cols.Length; i++)
            {
                datas[i] = dataDict.ContainsKey(cols[i]) ? dataDict[cols[i]] : "";
            }

            string insertSQL = string.Format("insert into {0} (`{1}`) Values('{2}');",
                                    TableName,
                                    string.Join("`,`", cols),
                                    string.Join("','", SQL.SafeText(datas)));
            try
            {
                if (MySqlDB.ExecuteCmd(insertSQL) <= 0)
                {
                    MessageBox.Show("插入数据失败。SQL：" + insertSQL);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("插入数据时出现异常：{0},SQL:{1}", ex.Message + insertSQL);
                return false;
            }
            return true;
        }

        #region 静态方法
        internal static bool Insert(string table, Dictionary<string, string> dataDict)
        {
            if (dataDict == null || dataDict.Count == 0)
            {
                MessageBox.Show("插入数据失败，数据为空");
                return false;
            }

            var cols = dataDict.Keys.ToArray();
            var datas = new string[cols.Length];
            for (int i = 0; i < cols.Length; i++)
            {
                datas[i] = dataDict[cols[i]];
            }

            string insertSQL = string.Format("insert into {0} (`{1}`) Values('{2}');",
                                    table,
                                    string.Join("`,`", cols),
                                    string.Join("','", SQL.SafeText(datas)));
            try
            {
                if (MySqlDB.ExecuteCmd(insertSQL) <= 0)
                {
                    MessageBox.Show("插入数据失败。SQL：" + insertSQL);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("插入数据时出现异常：{0},SQL:{1}", ex.Message + insertSQL);
                return false;
            }
            return true;
        }

        internal static Tuple<bool, string> ClearExpiredData(string table, DateTime time)
        {
            string deleteSQL = string.Format("delete from {0} where {1}<'{2}';",
                                    table, TIME_COL, time.ToString(TIME_FORMAT));
            try
            {
                if (MySqlDB.ExecuteCmd(deleteSQL) < 0)
                {
                    string msg = "删除数据失败。SQL：" + deleteSQL;
                    MessageBox.Show(msg);
                    return new Tuple<bool, string>(false, msg);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("删除数据时出现异常：{0},SQL:{1}", ex.Message, deleteSQL);
                MessageBox.Show(msg);
                return new Tuple<bool, string>(false, msg);
            }
            return new Tuple<bool, string>(true, "");
        }

        public static bool UpdateUploadResult(string table, string id, UploadResult uploadResult)
        {
            string sql = "";
            try
            {
                sql = string.Format("update {0} set {1}='{2}' where {3}={4};",
                               table, UPLOAD_RESULT_COL, uploadResult, ID_COL, SQL.SafeText(id));
                int result = MySqlDB.ExecuteCmd(sql);
                if (result < 0)
                    throw new Exception("更新出错");
                else
                    return result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("更新上传结果时出现异常：{0},SQL:{1}", ex.Message + sql);
                return false;
            }
        }

        public static int Count(string tableName, Filter filter)
        {
            string sql = "";
            try
            {
                sql = string.Format("SELECT count(*) from {0} WHERE {1};",
                                tableName,
                                filter.ToString());
                DataTable table = new DataTable();
                MySqlDB.ReadData(sql, ref table);
                if (table == null)
                    return -1;
                else
                    return int.Parse(table.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询数据时出现异常：{0},SQL:{1}", ex.Message + sql);
                return -1;
            }
        }

        public static DataTable Query(string tableName, UploadResult uploadResult, int limit = -1)
        {
            string sql = "";
            try
            {
                string limitStr = limit <= 0 ? "" : (" limit " + limit);
                sql = string.Format("select * from {0} where {1}='{2}' order by {3} asc {4};",
                               tableName, TableData.UPLOAD_RESULT_COL, uploadResult, TableData.TIME_COL, limitStr);
                DataTable table = new DataTable();
                if (MySqlDB.ReadData(sql, ref table) < 0)
                {
                    throw new Exception();
                }
                return table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询数据时出现异常：{0},SQL:{1}", ex.Message + sql);
                return null;
            }
        }

        public static DataTable Query(string tableName, string primaryKeyColumn, Filter filter)
        {
            string sql = "";
            try
            {
                sql = string.Format("SELECT t2.* FROM(SELECT `{0}` FROM {1} WHERE {2}) t1 LEFT JOIN {1} t2 ON t1.`{0}` = t2.`{0}` order by t1.`{0}`;",
                                primaryKeyColumn,
                                tableName,
                                filter.ToString());
                DataTable table = new DataTable();
                if (MySqlDB.ReadData(sql, ref table) < 0)
                {
                    throw new Exception();
                }
                return table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询数据时出现异常：{0},SQL:{1}", ex.Message + sql);
                return null;
            }
        }

        public static DataTable Query(string tableName, Filter filter)
        {
            string sql = "";
            try
            {
                sql = string.Format("SELECT * from {0} WHERE {1}",
                                tableName,
                                filter.ToString());
                DataTable table = new DataTable();
                if (MySqlDB.ReadData(sql, ref table) < 0)
                {
                    throw new Exception();
                }
                return table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询数据时出现异常：{0},SQL:{1}", ex.Message + sql);
                return null;
            }
        }

        public static DataRow Last(string tableName, string column, string value)
        {
            string sql = "";
            try
            {
                sql = string.Format("SELECT * from {0} WHERE `{1}`='{2}' order by {3} desc limit 1;",
                                tableName,
                                column,
                                SQL.SafeText(value),
                                TIME_COL);
                DataTable table = new DataTable();
                if (MySqlDB.ReadData(sql, ref table) < 0)
                {
                    throw new Exception();
                }
                return table.Rows.Count == 0 ? null : table.Rows[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询数据时出现异常：{0},SQL:{1}", ex.Message + sql);
                return null;
            }
        }
        #endregion

        public class Filter
        {
            public DateTime StartTime { get; private set; }
            public DateTime EndTime { get; private set; }
            public string CustomFilter { get; set; }
            public int Offset { get; set; }
            public int Limit { get; set; }

            public Filter(DateTime startTime, DateTime endTime)
            {
                this.StartTime = startTime;
                this.EndTime = endTime;
            }

            public override string ToString()
            {
                string customFilter = string.IsNullOrEmpty(CustomFilter) ? "" : ("AND " + CustomFilter);
                string limitStr = (Limit <= 0) ? "" : (string.Format("LIMIT {0} OFFSET {1}", Limit, Offset));

                return string.Format("`{0}` BETWEEN '{1}' AND '{2}' {3} {4}",
                                TIME_COL, StartTime.ToString(TIME_FORMAT), EndTime.ToString(TIME_FORMAT), customFilter, limitStr);
            }
        }
    }
}
