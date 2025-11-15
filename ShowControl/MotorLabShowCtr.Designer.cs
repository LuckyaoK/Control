
namespace CXPro001.ShowControl
{
    partial class MotorLabShowCtr
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
            this.btn_setpos = new System.Windows.Forms.Button();
            this.btn_getpos = new System.Windows.Forms.Button();
            this.txt_pos = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_setpos
            // 
            this.btn_setpos.Location = new System.Drawing.Point(298, 3);
            this.btn_setpos.Name = "btn_setpos";
            this.btn_setpos.Size = new System.Drawing.Size(75, 23);
            this.btn_setpos.TabIndex = 127;
            this.btn_setpos.Text = "定位";
            this.btn_setpos.UseVisualStyleBackColor = true;
            this.btn_setpos.Click += new System.EventHandler(this.btn_setpos_Click);
            // 
            // btn_getpos
            // 
            this.btn_getpos.Location = new System.Drawing.Point(217, 2);
            this.btn_getpos.Name = "btn_getpos";
            this.btn_getpos.Size = new System.Drawing.Size(75, 23);
            this.btn_getpos.TabIndex = 126;
            this.btn_getpos.Text = "获取";
            this.btn_getpos.UseVisualStyleBackColor = true;
            this.btn_getpos.Click += new System.EventHandler(this.btn_getpos_Click);
            // 
            // txt_pos
            // 
            this.txt_pos.Location = new System.Drawing.Point(80, 3);
            this.txt_pos.Name = "txt_pos";
            this.txt_pos.Size = new System.Drawing.Size(67, 21);
            this.txt_pos.TabIndex = 125;
            this.txt_pos.Text = "10000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 124;
            this.label1.Text = "软限位置负";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_setpos);
            this.panel1.Controls.Add(this.txt_pos);
            this.panel1.Controls.Add(this.btn_getpos);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(378, 30);
            this.panel1.TabIndex = 128;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 128;
            this.label2.Text = "/p";
            // 
            // MotorLabShowCtr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "MotorLabShowCtr";
            this.Size = new System.Drawing.Size(384, 36);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_setpos;
        private System.Windows.Forms.Button btn_getpos;
        private System.Windows.Forms.TextBox txt_pos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
    }
}
