
namespace CXPro001.ShowControl
{
    partial class DaDianShowCtr
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
            this.showName1 = new MyLib.OldCtr.ShowName();
            this.showName2 = new MyLib.OldCtr.ShowName();
            this.SuspendLayout();
            // 
            // showName1
            // 
            this.showName1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.showName1.Data_Text = "0";
            this.showName1.DataBack_Color = MyLib.OldCtr.ColorType1.Normal;
            this.showName1.Location = new System.Drawing.Point(3, 3);
            this.showName1.Name = "showName1";
            this.showName1.Name_Text = "二维码";
            this.showName1.NameFont_Color = MyLib.OldCtr.ColorType.Normal;
            this.showName1.Size = new System.Drawing.Size(271, 19);
            this.showName1.TabIndex = 0;
            this.showName1.TextFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.showName1.VarName = null;
            // 
            // showName2
            // 
            this.showName2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.showName2.Data_Text = "0";
            this.showName2.DataBack_Color = MyLib.OldCtr.ColorType1.Normal;
            this.showName2.Location = new System.Drawing.Point(3, 31);
            this.showName2.Name = "showName2";
            this.showName2.Name_Text = "结果";
            this.showName2.NameFont_Color = MyLib.OldCtr.ColorType.Normal;
            this.showName2.Size = new System.Drawing.Size(271, 19);
            this.showName2.TabIndex = 1;
            this.showName2.TextFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.showName2.VarName = null;
            // 
            // DaDianShowCtr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.showName2);
            this.Controls.Add(this.showName1);
            this.Name = "DaDianShowCtr";
            this.Size = new System.Drawing.Size(277, 55);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib.OldCtr.ShowName showName1;
        private MyLib.OldCtr.ShowName showName2;
    }
}
