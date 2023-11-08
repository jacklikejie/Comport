using Comport.ORM;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comport.DataBase
{
    public class MySQLClient
    {
        MySqlConnection mySqlConnection = null;
        public MySQLClient()
        {
            Connect();
        }
        public bool Connected
        {
            get
            {
                if (mySqlConnection == null)
                {
                    return false;
                }

                return mySqlConnection.State != ConnectionState.Closed;
            }
        }

        public int ExecuteCmd(string deleteSQL)
        {
            int result = -1;
            if (string.IsNullOrEmpty(deleteSQL))
            {
                return result;
            }

            lock (mySqlConnection)
            {
                try
                {
                    if (!Connected && !Connect())
                    {
                        return result;
                    }

                    if (mySqlConnection.State == ConnectionState.Executing || mySqlConnection.State == ConnectionState.Fetching)
                    {
                        return result;
                    }

                    MySqlCommand mySqlCommand = new MySqlCommand(deleteSQL, mySqlConnection);
                    mySqlCommand.CommandTimeout = 5;
                    result = mySqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    result = -1;
                    MessageBox.Show(ex.Message);
                }
            }

            return result;
        }


        public int ReadData(string strSql, ref DataTable table)
        {
            int num = -1;
            DataSet dataSet = new DataSet();
            table = new DataTable();
            if (string.IsNullOrEmpty(strSql))
            {
                return -1;
            }

            lock (mySqlConnection)
            {
                try
                {

                    if (!Connected && !Connect())
                    {
                        return result;
                    }
                    // 打开连接(如果处于关闭状态才进行打开)
                    if (mySqlConnection.State == ConnectionState.Closed)
                    {
                        mySqlConnection.Open();
                    }
                    // 创建用于实现MySQL语句的对象
                    MySqlCommand mySqlCommand2 = new MySqlCommand(strSql, mySqlConnection);  // 参数一：SQL语句字符串 参数二：已经打开的数据库连接对象
                                                                                             // 执行MySQL语句，接收查询到的MySQL结果
                    MySqlDataReader mdr = mySqlCommand2.ExecuteReader();

                    table = mdr.GetSchemaTable();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message+ex.StackTrace);
                }
                finally
                {
                    // 关闭连接
                    mySqlConnection.Close();
                }
            }

            return num;
        }
        public int ReadTableData(string strSql, ref DataTable table)
        {
            int num = -1;
            DataSet dataSet = new DataSet();
            table = new DataTable();
            if (string.IsNullOrEmpty(strSql))
            {
                return -1;
            }

            lock (mySqlConnection)
            {
                try
                {

                    if (!Connected && !Connect())
                    {
                        return result;
                    }
                    // 打开连接(如果处于关闭状态才进行打开)
                    if (mySqlConnection.State == ConnectionState.Closed)
                    {
                        mySqlConnection.Open();
                    }
                    // 创建用于实现MySQL语句的对象
                    MySqlDataAdapter AD = new MySqlDataAdapter(strSql, mySqlConnection);// 参数一：SQL语句字符串 参数二：已经打开的数据库连接对象
                    DataSet DS = new DataSet();
                    AD.Fill(DS); // 执行MySQL语句，接收查询到的MySQL结果
                    mySqlConnection.Close();
                    table = DS.Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
                finally
                {
                    // 关闭连接
                    mySqlConnection.Close();
                }
            }

            return num;
        }
        public void Dispose()
        {
            Dispose(disposing: true);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (mySqlConnection != null)
                {
                    mySqlConnection.Dispose();
                }
            }
        }
        private bool Connect()
        {
            #region 连接数据库
            // 声明一个连接数据库的对象
            MySqlConnectionStringBuilder mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Database = "qingan_mes";   // 设置连接的数据库名
            mysqlCSB.Server = "127.0.0.1";  // 设置连接数据库的IP地址
            mysqlCSB.Port = 3306;           // MySql端口号
            mysqlCSB.UserID = "root";       // 设置登录数据库的账号
            mysqlCSB.Password = "root";     // 设置登录数据库的密码
                                            //string mysqlCSB = "Database=school;Data Source=127.0.0.1;port=3306;User Id=root;Password=yang;";

            // 创建连接
            mySqlConnection = new MySqlConnection(mysqlCSB.ToString());

            // 打开连接(如果处于关闭状态才进行打开)
            if (mySqlConnection.State == ConnectionState.Closed)
            {
                mySqlConnection.Open();
            }
            return mySqlConnection.State != ConnectionState.Closed;
            #endregion
        }
        string id = "0";
        string name = "0";
        // 保存数据库执行后受影响的行数
        int result = 0;
        private void sk()
        {

            #region MySQL插入（增）

            try
            {

                // 打开连接(如果处于关闭状态才进行打开)
                if (mySqlConnection.State == ConnectionState.Closed)
                {
                    mySqlConnection.Open();
                }


                // 创建要插入的MySQL语句
                String mysqlInsert = "insert into class1 values(" + id + ", '" + name + "')";

                // 创建用于实现MySQL语句的对象
                MySqlCommand mySqlCommand = new MySqlCommand(mysqlInsert, mySqlConnection);
                // 执行MySQL语句进行插入
                result = mySqlCommand.ExecuteNonQuery();

                Console.WriteLine("数据库中受影响的行数({0})\n", result);

            }
            catch (Exception)
            {

            }
            finally
            {
                // 关闭连接
                mySqlConnection.Close();
            }

            #endregion



            #region MySQL删除（删）

            try
            {

                // 打开连接(如果处于关闭状态才进行打开)
                if (mySqlConnection.State == ConnectionState.Closed)
                {
                    mySqlConnection.Open();
                }


                Console.WriteLine("请输入需要删除的id:");
                id = Console.ReadLine();


                // 创建要查询的MySQL语句
                String sqlDelete = "delete from class1 where id = " + id;

                // 创建用于实现MySQL语句的对象
                MySqlCommand mySqlCommand4 = new MySqlCommand(sqlDelete, mySqlConnection);
                // 执行MySQL语句进行删除
                result = mySqlCommand4.ExecuteNonQuery();

                Console.WriteLine("数据库中受影响的行数({0}\n)", result);

            }
            catch (Exception)
            {

            }
            finally
            {
                // 关闭连接
                mySqlConnection.Close();
            }

            #endregion

            #region MySQL修改（改）

            try
            {

                // 打开连接(如果处于关闭状态才进行打开)
                if (mySqlConnection.State == ConnectionState.Closed)
                {
                    mySqlConnection.Open();
                }

                Console.WriteLine("请输入需要修改的id:");
                id = Console.ReadLine();
                Console.WriteLine("请输入需要修改的name:");
                name = Console.ReadLine();

                // 创建要修改的MySQL语句
                String sqlUpdate = "update class1 set name = '" + name + "' where id = " + id;

                // 创建用于实现MySQL语句的对象
                MySqlCommand mySqlCommand3 = new MySqlCommand(sqlUpdate, mySqlConnection);
                // 执行MySQL语句进行修改
                result = mySqlCommand3.ExecuteNonQuery();

                Console.WriteLine("数据库中受影响的行数({0}\n)", result);

            }
            catch (Exception)
            {

            }
            finally
            {
                // 关闭连接
                mySqlConnection.Close();
            }

            #endregion
        }
    }
}
