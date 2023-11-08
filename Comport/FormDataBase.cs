using Comport.DataBase;
using Comport.ORM;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Comport.DataBase.TableData;

namespace Comport
{
    public partial class FormDataBase : Form
    {
        MySQLClient SQLClient = null;
        TableData tableData = null;
        private Mapper<OutboundParam> m_mapper = null;        //ORM对象
        DataTable table = new DataTable();
        public FormDataBase()
        {
            InitializeComponent();
            string mapperConfigTable = IniTool.ReadString("UploadExtrusion1", "MapperConfigTable", "", System.Windows.Forms.Application.StartupPath + "\\Setting.cfg");
            //this.m_mapper = new Mapper<OutboundParam>(mapperConfigTable, new BlockDivideStrategy(50, 120));
            //this.m_mapper.Reader.ReverseString = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLClient=new MySQLClient();
            SQLClient.ReadData("SELECT * from table_alarm_history", ref table);
            dataGridView1.DataSource = table;
            //var filter = new TableData.Filter(dtStartTime.Value, dtEndTime.Value) { Offset = offset, Limit = 200, CustomFilter = customFilter };
            tableData = new TableData("table_Extrusion_data1", Application.StartupPath + "\\Setting.cfg");
            //Mapper("", new BlockDivideStrategy(50, 120));
            //TableData.Query("table_Extrusion_data1", filter);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SQLiteHelper sqliteHelper = new SQLiteHelper();
            var list = sqliteHelper.Query<Stock>("select * from Stock");
            sqliteHelper.Add(new Valuation() { Price = 100, StockId = 1, Time = DateTime.Now });
            var list2 = sqliteHelper.Query<Valuation>("select *  from Valuation");
            var list3 = sqliteHelper.Query<Valuation>("select *   from Valuation INDEXED BY ValuationStockId2 WHERE StockId > 2");//使用索引ValuationStockId2查询
            try
            {
                sqliteHelper.Execute("drop index ValuationStockId");//删除索引，因为该索引已被删除，所以抛异常
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
