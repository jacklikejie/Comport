using HymsonAutomation.CCS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comport
{
    public partial class FormualForm : Form
    {
        BatteriesType batteriesType;
        int index = 0;
        public FormualForm()
        {
            InitializeComponent();
            CCStree.Nodes[0].Nodes.Clear();
            RefreshTree();
        }
        private void btnadd_Click(object sender, EventArgs e)
        {
            CCStree.Nodes[0].Nodes.Add("子节点");
            batteriesType = new BatteriesType();
            FormualConfig.GetInstance().Batteries.Add(batteriesType);
        }
        private void TreeNode()
        {
            for (int i = 0; i < FormualConfig.GetInstance().Batteries.Count; i++)
            {
                CCStree.Nodes[0].Nodes.Add(FormualConfig.GetInstance().Batteries[i].BatteriesTypeNumber);
            }
        }
        private void RefreshTree()
        {
            FormualConfig.Init();
            CCStree.Nodes[0].Nodes.Clear();
            TreeNode();
            //CCStb1.Text = FormualConfig.GetInstance().CCSType1;
            //CCStb2.Text = FormualConfig.GetInstance().CCSType2;
            //CCStb3.Text = FormualConfig.GetInstance().CCSType3;
            //CCStb4.Text = FormualConfig.GetInstance().CCSType4;
            //BatteriesTypetb.Text= FormualConfig.GetInstance().BatteriesTypeNumber;
            //CCSLocationtb1.Text = FormualConfig.GetInstance().CCSTypeLocation1;
            //CCSLocationtb2.Text = FormualConfig.GetInstance().CCSTypeLocation2;
            //CCSLocationtb3.Text = FormualConfig.GetInstance().CCSTypeLocation3;
            //CCSLocationtb4.Text = FormualConfig.GetInstance().CCSTypeLocation4;
            //CCStb1.Text = FormualConfig.GetInstance().Batteries[0].CCSType1;
            //CCStb2.Text = FormualConfig.GetInstance().Batteries[0].CCSType2;
            //CCStb3.Text = FormualConfig.GetInstance().Batteries[0].CCSType3;
            //CCStb4.Text = FormualConfig.GetInstance().Batteries[0].CCSType4;
            //BatteriesTypetb.Text = FormualConfig.GetInstance().Batteries[0].BatteriesTypeNumber;
            //CCSLocationtb1.Text = FormualConfig.GetInstance().Batteries[0].CCSTypeLocation1;
            //CCSLocationtb2.Text = FormualConfig.GetInstance().Batteries[0].CCSTypeLocation2;
            //CCSLocationtb3.Text = FormualConfig.GetInstance().Batteries[0].CCSTypeLocation3;
            //CCSLocationtb4.Text = FormualConfig.GetInstance().Batteries[0].CCSTypeLocation4;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FormualConfig.GetInstance().Batteries[index].CCSType1 = CCStb1.Text;
            FormualConfig.GetInstance().Batteries[index].CCSType2 = CCStb2.Text;
            FormualConfig.GetInstance().Batteries[index].CCSType3 = CCStb3.Text;
            FormualConfig.GetInstance().Batteries[index].CCSType4 = CCStb4.Text;
            FormualConfig.GetInstance().Batteries[index].BatteriesTypeNumber = BatteriesTypetb.Text;
            FormualConfig.GetInstance().Batteries[index].CCSTypeLocation1 = CCSLocationtb1.Text;
            FormualConfig.GetInstance().Batteries[index].CCSTypeLocation2 = CCSLocationtb2.Text;
            FormualConfig.GetInstance().Batteries[index].CCSTypeLocation3 = CCSLocationtb3.Text;
            FormualConfig.GetInstance().Batteries[index].CCSTypeLocation4 = CCSLocationtb4.Text;
            FormualConfig.Save();
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            RefreshTree();
        }

        private void FormualForm_Load(object sender, EventArgs e)
        {
            //TreeNode();
        }

        private void CCStree_TreeNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            index = e.Node.Index;
            CCStb1.Text = FormualConfig.GetInstance().Batteries[index].CCSType1;
            CCStb2.Text = FormualConfig.GetInstance().Batteries[index].CCSType2;
            CCStb3.Text = FormualConfig.GetInstance().Batteries[index].CCSType3;
            CCStb4.Text = FormualConfig.GetInstance().Batteries[index].CCSType4;
            BatteriesTypetb.Text = FormualConfig.GetInstance().Batteries[index].BatteriesTypeNumber;
            CCSLocationtb1.Text = FormualConfig.GetInstance().Batteries[index].CCSTypeLocation1;
            CCSLocationtb2.Text = FormualConfig.GetInstance().Batteries[index].CCSTypeLocation2;
            CCSLocationtb3.Text = FormualConfig.GetInstance().Batteries[index].CCSTypeLocation3;
            CCSLocationtb4.Text = FormualConfig.GetInstance().Batteries[index].CCSTypeLocation4;
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            FormualConfig.GetInstance().Batteries.Remove(FormualConfig.GetInstance().Batteries[index]);
            FormualConfig.Save();
        }
    }
}
