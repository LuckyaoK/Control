
namespace CXPro001.controls
{
    partial class S7_1200
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.txtBitBlock = new System.Windows.Forms.TextBox();
            this.Label56 = new System.Windows.Forms.Label();
            this.txtReBit = new System.Windows.Forms.TextBox();
            this.Label22 = new System.Windows.Forms.Label();
            this.cmbBit = new System.Windows.Forms.ComboBox();
            this.Label21 = new System.Windows.Forms.Label();
            this.txtBitAdd = new System.Windows.Forms.TextBox();
            this.Label20 = new System.Windows.Forms.Label();
            this.butBitRst = new System.Windows.Forms.Button();
            this.butBitSet = new System.Windows.Forms.Button();
            this.butBitTest = new System.Windows.Forms.Button();
            this.Label18 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_read_con = new System.Windows.Forms.Button();
            this.GroupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.txtBitBlock);
            this.GroupBox3.Controls.Add(this.Label56);
            this.GroupBox3.Controls.Add(this.txtReBit);
            this.GroupBox3.Controls.Add(this.Label22);
            this.GroupBox3.Controls.Add(this.cmbBit);
            this.GroupBox3.Controls.Add(this.Label21);
            this.GroupBox3.Controls.Add(this.txtBitAdd);
            this.GroupBox3.Controls.Add(this.Label20);
            this.GroupBox3.Controls.Add(this.butBitRst);
            this.GroupBox3.Controls.Add(this.butBitSet);
            this.GroupBox3.Controls.Add(this.butBitTest);
            this.GroupBox3.Controls.Add(this.Label18);
            this.GroupBox3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.GroupBox3.Location = new System.Drawing.Point(14, 50);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(614, 133);
            this.GroupBox3.TabIndex = 63;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "位操作";
            // 
            // txtBitBlock
            // 
            this.txtBitBlock.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBitBlock.Location = new System.Drawing.Point(116, 26);
            this.txtBitBlock.Name = "txtBitBlock";
            this.txtBitBlock.Size = new System.Drawing.Size(50, 23);
            this.txtBitBlock.TabIndex = 63;
            this.txtBitBlock.Text = "1";
            this.txtBitBlock.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Label56
            // 
            this.Label56.AutoSize = true;
            this.Label56.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label56.Location = new System.Drawing.Point(61, 30);
            this.Label56.Name = "Label56";
            this.Label56.Size = new System.Drawing.Size(49, 17);
            this.Label56.TabIndex = 64;
            this.Label56.Text = "DB块号";
            // 
            // txtReBit
            // 
            this.txtReBit.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtReBit.Location = new System.Drawing.Point(536, 24);
            this.txtReBit.Name = "txtReBit";
            this.txtReBit.ReadOnly = true;
            this.txtReBit.Size = new System.Drawing.Size(44, 23);
            this.txtReBit.TabIndex = 61;
            this.txtReBit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Label22
            // 
            this.Label22.AutoSize = true;
            this.Label22.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label22.Location = new System.Drawing.Point(494, 27);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(32, 17);
            this.Label22.TabIndex = 62;
            this.Label22.Text = "结果";
            // 
            // cmbBit
            // 
            this.cmbBit.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbBit.FormattingEnabled = true;
            this.cmbBit.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.cmbBit.Location = new System.Drawing.Point(424, 23);
            this.cmbBit.Name = "cmbBit";
            this.cmbBit.Size = new System.Drawing.Size(54, 25);
            this.cmbBit.TabIndex = 60;
            this.cmbBit.Text = "0";
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label21.Location = new System.Drawing.Point(400, 27);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(20, 17);
            this.Label21.TabIndex = 59;
            this.Label21.Text = "位";
            // 
            // txtBitAdd
            // 
            this.txtBitAdd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBitAdd.Location = new System.Drawing.Point(338, 24);
            this.txtBitAdd.Name = "txtBitAdd";
            this.txtBitAdd.Size = new System.Drawing.Size(50, 23);
            this.txtBitAdd.TabIndex = 57;
            this.txtBitAdd.Text = "0";
            this.txtBitAdd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label20.Location = new System.Drawing.Point(263, 27);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(56, 17);
            this.Label20.TabIndex = 58;
            this.Label20.Text = "字节地址";
            // 
            // butBitRst
            // 
            this.butBitRst.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butBitRst.Location = new System.Drawing.Point(369, 71);
            this.butBitRst.Name = "butBitRst";
            this.butBitRst.Size = new System.Drawing.Size(90, 32);
            this.butBitRst.TabIndex = 54;
            this.butBitRst.Text = "位复位";
            this.butBitRst.UseVisualStyleBackColor = true;
            this.butBitRst.Click += new System.EventHandler(this.butBitRst_Click);
            // 
            // butBitSet
            // 
            this.butBitSet.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butBitSet.Location = new System.Drawing.Point(225, 71);
            this.butBitSet.Name = "butBitSet";
            this.butBitSet.Size = new System.Drawing.Size(90, 32);
            this.butBitSet.TabIndex = 53;
            this.butBitSet.Text = "位置ON";
            this.butBitSet.UseVisualStyleBackColor = true;
            this.butBitSet.Click += new System.EventHandler(this.butBitSet_Click);
            // 
            // butBitTest
            // 
            this.butBitTest.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butBitTest.Location = new System.Drawing.Point(64, 71);
            this.butBitTest.Name = "butBitTest";
            this.butBitTest.Size = new System.Drawing.Size(90, 32);
            this.butBitTest.TabIndex = 52;
            this.butBitTest.Text = "位读取";
            this.butBitTest.UseVisualStyleBackColor = true;
            this.butBitTest.Click += new System.EventHandler(this.butBitTest_Click);
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label18.Location = new System.Drawing.Point(6, 26);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(0, 17);
            this.Label18.TabIndex = 45;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Location = new System.Drawing.Point(1149, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 22);
            this.button1.TabIndex = 120;
            this.button1.Text = "清除记录";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_read_con
            // 
            this.btn_read_con.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_read_con.Location = new System.Drawing.Point(147, 12);
            this.btn_read_con.Name = "btn_read_con";
            this.btn_read_con.Size = new System.Drawing.Size(90, 32);
            this.btn_read_con.TabIndex = 121;
            this.btn_read_con.Text = "读取连接状态";
            this.btn_read_con.UseVisualStyleBackColor = true;
            this.btn_read_con.Click += new System.EventHandler(this.btn_read_con_Click);
            // 
            // S7_1200
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_read_con);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.GroupBox3);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "S7_1200";
            this.Size = new System.Drawing.Size(1224, 495);
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.TextBox txtBitBlock;
        internal System.Windows.Forms.Label Label56;
        internal System.Windows.Forms.TextBox txtReBit;
        internal System.Windows.Forms.Label Label22;
        internal System.Windows.Forms.ComboBox cmbBit;
        internal System.Windows.Forms.Label Label21;
        internal System.Windows.Forms.TextBox txtBitAdd;
        internal System.Windows.Forms.Label Label20;
        internal System.Windows.Forms.Button butBitRst;
        internal System.Windows.Forms.Button butBitSet;
        internal System.Windows.Forms.Button butBitTest;
        internal System.Windows.Forms.Label Label18;
        private System.Windows.Forms.Button button1;
        internal System.Windows.Forms.Button btn_read_con;
    }
}
