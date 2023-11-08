namespace Comport
{
    partial class FormualForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("52个电芯");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("50个电芯");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("48个电芯");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("电芯类型", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.CCSLocationlb1 = new System.Windows.Forms.Label();
            this.CCSLocationtb1 = new System.Windows.Forms.TextBox();
            this.CCSLocationlb3 = new System.Windows.Forms.Label();
            this.CCSLocationtb3 = new System.Windows.Forms.TextBox();
            this.CCSLocationlb2 = new System.Windows.Forms.Label();
            this.CCSLocationtb2 = new System.Windows.Forms.TextBox();
            this.CCSLocationlb4 = new System.Windows.Forms.Label();
            this.CCSLocationtb4 = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BatteriesTypelb = new System.Windows.Forms.Label();
            this.BatteriesTypetb = new System.Windows.Forms.TextBox();
            this.CCSlb1 = new System.Windows.Forms.Label();
            this.CCStb1 = new System.Windows.Forms.TextBox();
            this.CCSlb3 = new System.Windows.Forms.Label();
            this.CCStb3 = new System.Windows.Forms.TextBox();
            this.CCSlb2 = new System.Windows.Forms.Label();
            this.CCStb2 = new System.Windows.Forms.TextBox();
            this.CCSlb4 = new System.Windows.Forms.Label();
            this.CCStb4 = new System.Windows.Forms.TextBox();
            this.CCStree = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btndelete = new System.Windows.Forms.Button();
            this.btnadd = new System.Windows.Forms.Button();
            this.btnrefresh = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(740, 492);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.CCStree);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 60);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(740, 432);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(188, 21);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(549, 408);
            this.panel2.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.CCSLocationlb1);
            this.groupBox4.Controls.Add(this.CCSLocationtb1);
            this.groupBox4.Controls.Add(this.CCSLocationlb3);
            this.groupBox4.Controls.Add(this.CCSLocationtb3);
            this.groupBox4.Controls.Add(this.CCSLocationlb2);
            this.groupBox4.Controls.Add(this.CCSLocationtb2);
            this.groupBox4.Controls.Add(this.CCSLocationlb4);
            this.groupBox4.Controls.Add(this.CCSLocationtb4);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 202);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(549, 206);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "CCS类型位置设置";
            // 
            // CCSLocationlb1
            // 
            this.CCSLocationlb1.AutoSize = true;
            this.CCSLocationlb1.Location = new System.Drawing.Point(188, 22);
            this.CCSLocationlb1.Name = "CCSLocationlb1";
            this.CCSLocationlb1.Size = new System.Drawing.Size(129, 15);
            this.CCSLocationlb1.TabIndex = 0;
            this.CCSLocationlb1.Text = "CCS类型1对应位置";
            // 
            // CCSLocationtb1
            // 
            this.CCSLocationtb1.Location = new System.Drawing.Point(325, 22);
            this.CCSLocationtb1.Name = "CCSLocationtb1";
            this.CCSLocationtb1.Size = new System.Drawing.Size(100, 25);
            this.CCSLocationtb1.TabIndex = 1;
            // 
            // CCSLocationlb3
            // 
            this.CCSLocationlb3.AutoSize = true;
            this.CCSLocationlb3.Location = new System.Drawing.Point(188, 122);
            this.CCSLocationlb3.Name = "CCSLocationlb3";
            this.CCSLocationlb3.Size = new System.Drawing.Size(129, 15);
            this.CCSLocationlb3.TabIndex = 2;
            this.CCSLocationlb3.Text = "CCS类型3对应位置";
            // 
            // CCSLocationtb3
            // 
            this.CCSLocationtb3.Location = new System.Drawing.Point(325, 122);
            this.CCSLocationtb3.Name = "CCSLocationtb3";
            this.CCSLocationtb3.Size = new System.Drawing.Size(100, 25);
            this.CCSLocationtb3.TabIndex = 3;
            // 
            // CCSLocationlb2
            // 
            this.CCSLocationlb2.AutoSize = true;
            this.CCSLocationlb2.Location = new System.Drawing.Point(188, 72);
            this.CCSLocationlb2.Name = "CCSLocationlb2";
            this.CCSLocationlb2.Size = new System.Drawing.Size(129, 15);
            this.CCSLocationlb2.TabIndex = 4;
            this.CCSLocationlb2.Text = "CCS类型2对应位置";
            // 
            // CCSLocationtb2
            // 
            this.CCSLocationtb2.Location = new System.Drawing.Point(325, 72);
            this.CCSLocationtb2.Name = "CCSLocationtb2";
            this.CCSLocationtb2.Size = new System.Drawing.Size(100, 25);
            this.CCSLocationtb2.TabIndex = 5;
            // 
            // CCSLocationlb4
            // 
            this.CCSLocationlb4.AutoSize = true;
            this.CCSLocationlb4.Location = new System.Drawing.Point(188, 172);
            this.CCSLocationlb4.Name = "CCSLocationlb4";
            this.CCSLocationlb4.Size = new System.Drawing.Size(129, 15);
            this.CCSLocationlb4.TabIndex = 6;
            this.CCSLocationlb4.Text = "CCS类型4对应位置";
            // 
            // CCSLocationtb4
            // 
            this.CCSLocationtb4.Location = new System.Drawing.Point(325, 172);
            this.CCSLocationtb4.Name = "CCSLocationtb4";
            this.CCSLocationtb4.Size = new System.Drawing.Size(100, 25);
            this.CCSLocationtb4.TabIndex = 7;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BatteriesTypelb);
            this.groupBox3.Controls.Add(this.BatteriesTypetb);
            this.groupBox3.Controls.Add(this.CCSlb1);
            this.groupBox3.Controls.Add(this.CCStb1);
            this.groupBox3.Controls.Add(this.CCSlb3);
            this.groupBox3.Controls.Add(this.CCStb3);
            this.groupBox3.Controls.Add(this.CCSlb2);
            this.groupBox3.Controls.Add(this.CCStb2);
            this.groupBox3.Controls.Add(this.CCSlb4);
            this.groupBox3.Controls.Add(this.CCStb4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(549, 202);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "CCS类型设置";
            // 
            // BatteriesTypelb
            // 
            this.BatteriesTypelb.AutoSize = true;
            this.BatteriesTypelb.Location = new System.Drawing.Point(3, 72);
            this.BatteriesTypelb.Name = "BatteriesTypelb";
            this.BatteriesTypelb.Size = new System.Drawing.Size(67, 15);
            this.BatteriesTypelb.TabIndex = 8;
            this.BatteriesTypelb.Text = "电芯类型";
            // 
            // BatteriesTypetb
            // 
            this.BatteriesTypetb.Location = new System.Drawing.Point(93, 72);
            this.BatteriesTypetb.Name = "BatteriesTypetb";
            this.BatteriesTypetb.Size = new System.Drawing.Size(100, 25);
            this.BatteriesTypetb.TabIndex = 9;
            // 
            // CCSlb1
            // 
            this.CCSlb1.AutoSize = true;
            this.CCSlb1.Location = new System.Drawing.Point(221, 22);
            this.CCSlb1.Name = "CCSlb1";
            this.CCSlb1.Size = new System.Drawing.Size(69, 15);
            this.CCSlb1.TabIndex = 0;
            this.CCSlb1.Text = "CCS类型1";
            // 
            // CCStb1
            // 
            this.CCStb1.Location = new System.Drawing.Point(326, 22);
            this.CCStb1.Name = "CCStb1";
            this.CCStb1.Size = new System.Drawing.Size(100, 25);
            this.CCStb1.TabIndex = 1;
            // 
            // CCSlb3
            // 
            this.CCSlb3.AutoSize = true;
            this.CCSlb3.Location = new System.Drawing.Point(221, 122);
            this.CCSlb3.Name = "CCSlb3";
            this.CCSlb3.Size = new System.Drawing.Size(69, 15);
            this.CCSlb3.TabIndex = 2;
            this.CCSlb3.Text = "CCS类型3";
            // 
            // CCStb3
            // 
            this.CCStb3.Location = new System.Drawing.Point(326, 122);
            this.CCStb3.Name = "CCStb3";
            this.CCStb3.Size = new System.Drawing.Size(100, 25);
            this.CCStb3.TabIndex = 3;
            // 
            // CCSlb2
            // 
            this.CCSlb2.AutoSize = true;
            this.CCSlb2.Location = new System.Drawing.Point(221, 72);
            this.CCSlb2.Name = "CCSlb2";
            this.CCSlb2.Size = new System.Drawing.Size(69, 15);
            this.CCSlb2.TabIndex = 4;
            this.CCSlb2.Text = "CCS类型2";
            // 
            // CCStb2
            // 
            this.CCStb2.Location = new System.Drawing.Point(326, 72);
            this.CCStb2.Name = "CCStb2";
            this.CCStb2.Size = new System.Drawing.Size(100, 25);
            this.CCStb2.TabIndex = 5;
            // 
            // CCSlb4
            // 
            this.CCSlb4.AutoSize = true;
            this.CCSlb4.Location = new System.Drawing.Point(221, 172);
            this.CCSlb4.Name = "CCSlb4";
            this.CCSlb4.Size = new System.Drawing.Size(69, 15);
            this.CCSlb4.TabIndex = 6;
            this.CCSlb4.Text = "CCS类型4";
            // 
            // CCStb4
            // 
            this.CCStb4.Location = new System.Drawing.Point(326, 172);
            this.CCStb4.Name = "CCStb4";
            this.CCStb4.Size = new System.Drawing.Size(100, 25);
            this.CCStb4.TabIndex = 7;
            // 
            // CCStree
            // 
            this.CCStree.Dock = System.Windows.Forms.DockStyle.Left;
            this.CCStree.Location = new System.Drawing.Point(3, 21);
            this.CCStree.Name = "CCStree";
            treeNode1.Checked = true;
            treeNode1.Name = "节点0";
            treeNode1.Text = "52个电芯";
            treeNode2.Name = "节点1";
            treeNode2.Text = "50个电芯";
            treeNode3.Name = "节点2";
            treeNode3.Text = "48个电芯";
            treeNode4.Checked = true;
            treeNode4.Name = "节点0";
            treeNode4.Text = "电芯类型";
            this.CCStree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            this.CCStree.Size = new System.Drawing.Size(185, 408);
            this.CCStree.TabIndex = 0;
            this.CCStree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.CCStree_TreeNodeMouseClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btndelete);
            this.groupBox1.Controls.Add(this.btnadd);
            this.groupBox1.Controls.Add(this.btnrefresh);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(740, 60);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "电芯类型操作";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(628, 24);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 30);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btndelete
            // 
            this.btndelete.Location = new System.Drawing.Point(462, 24);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(86, 30);
            this.btndelete.TabIndex = 2;
            this.btndelete.Text = "删除";
            this.btndelete.UseVisualStyleBackColor = true;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            // 
            // btnadd
            // 
            this.btnadd.Location = new System.Drawing.Point(286, 24);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(86, 30);
            this.btnadd.TabIndex = 1;
            this.btnadd.Text = "新建";
            this.btnadd.UseVisualStyleBackColor = true;
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // btnrefresh
            // 
            this.btnrefresh.Location = new System.Drawing.Point(12, 24);
            this.btnrefresh.Name = "btnrefresh";
            this.btnrefresh.Size = new System.Drawing.Size(86, 30);
            this.btnrefresh.TabIndex = 0;
            this.btnrefresh.Text = "刷新";
            this.btnrefresh.UseVisualStyleBackColor = true;
            this.btnrefresh.Click += new System.EventHandler(this.btnrefresh_Click);
            // 
            // FormualForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 492);
            this.Controls.Add(this.panel1);
            this.Name = "FormualForm";
            this.Text = "FormualForm";
            this.Load += new System.EventHandler(this.FormualForm_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView CCStree;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.Button btnadd;
        private System.Windows.Forms.Button btnrefresh;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox CCStb4;
        private System.Windows.Forms.Label CCSlb4;
        private System.Windows.Forms.TextBox CCStb2;
        private System.Windows.Forms.Label CCSlb2;
        private System.Windows.Forms.TextBox CCStb3;
        private System.Windows.Forms.Label CCSlb3;
        private System.Windows.Forms.TextBox CCStb1;
        private System.Windows.Forms.Label CCSlb1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label CCSLocationlb1;
        private System.Windows.Forms.TextBox CCSLocationtb1;
        private System.Windows.Forms.Label CCSLocationlb3;
        private System.Windows.Forms.TextBox CCSLocationtb3;
        private System.Windows.Forms.Label CCSLocationlb2;
        private System.Windows.Forms.TextBox CCSLocationtb2;
        private System.Windows.Forms.Label CCSLocationlb4;
        private System.Windows.Forms.TextBox CCSLocationtb4;
        private System.Windows.Forms.Label BatteriesTypelb;
        private System.Windows.Forms.TextBox BatteriesTypetb;
    }
}