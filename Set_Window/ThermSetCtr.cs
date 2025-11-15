using MyLib.Files;
using MyLib.Sys;
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

namespace CXPro001.setups
{
    public partial class ThermSetCtr : UserControl
    {
       
        ThermSet thermSet1 = null;
       
        public ThermSetCtr()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 写入产品型号
        /// </summary>
        /// <param name="names">型号</param>
        public void WritePro(string names, ThermSet thermSets)
        {
            textBox1.Text = names;
            thermSet1 = thermSets;
            button1_Click(null, null);
        }
        #region 读取 保存数据
        private void button1_Click(object sender, EventArgs e)//加载
        {
            string filename = $"{Path.GetFullPath("..")}\\product\\{SysStatus.CurProductName.Trim()}\\ThermSet.ini";// 配置文件路径
            if (textBox1.Text.Trim() != SysStatus.CurProductName.Trim())
            {
                filename = $"{Path.GetFullPath("..")}\\product\\{textBox1.Text.Trim()}\\HighSet.ini";
            }
            string path1 = filename;
            IniFile inf = new IniFile(path1);//确认路径是否存在，不存在则创建文件夹。                                             
            string STEP = "理论值";
            textL1.Text = inf.ReadString(STEP, "L1", textL1.Text);
            textL2.Text = inf.ReadString(STEP, "L2", textL2.Text);
            textL3.Text = inf.ReadString(STEP, "L3", textL3.Text);
            textL4.Text = inf.ReadString(STEP, "L4", textL4.Text);
            textL5.Text = inf.ReadString(STEP, "L5", textL5.Text);
            textL6.Text = inf.ReadString(STEP, "L6", textL6.Text);
            textL7.Text = inf.ReadString(STEP, "L7", textL7.Text);
            textL8.Text = inf.ReadString(STEP, "L8", textL8.Text);
            textL9.Text = inf.ReadString(STEP, "L9", textL9.Text);
            textL10.Text = inf.ReadString(STEP, "L10", textL10.Text);
            STEP = "上限值";
            textUP1.Text = inf.ReadString(STEP, "UP1", textUP1.Text);
            textUP2.Text = inf.ReadString(STEP, "UP2", textUP2.Text);
            textUP3.Text = inf.ReadString(STEP, "UP3", textUP3.Text);
            textUP4.Text = inf.ReadString(STEP, "UP4", textUP4.Text);
            textUP5.Text = inf.ReadString(STEP, "UP5", textUP5.Text);
            textUP6.Text = inf.ReadString(STEP, "UP6", textUP6.Text);
            textUP7.Text = inf.ReadString(STEP, "UP7", textUP7.Text);
            textUP8.Text = inf.ReadString(STEP, "UP8", textUP8.Text);
            textUP9.Text = inf.ReadString(STEP, "UP9", textUP9.Text);
            textUP10.Text = inf.ReadString(STEP, "UP10", textUP10.Text);
            STEP = "下限值";
            textDW1.Text = inf.ReadString(STEP, "DW1", textDW1.Text);
            textDW2.Text = inf.ReadString(STEP, "DW2", textDW2.Text);
            textDW3.Text = inf.ReadString(STEP, "DW3", textDW3.Text);
            textDW4.Text = inf.ReadString(STEP, "DW4", textDW4.Text);
            textDW5.Text = inf.ReadString(STEP, "DW5", textDW5.Text);
            textDW6.Text = inf.ReadString(STEP, "DW6", textDW6.Text);
            textDW7.Text = inf.ReadString(STEP, "DW7", textDW7.Text);
            textDW8.Text = inf.ReadString(STEP, "DW8", textDW8.Text);
            textDW9.Text = inf.ReadString(STEP, "DW9", textDW9.Text);
            textDW10.Text = inf.ReadString(STEP, "DW10", textDW10.Text);

        }

        private void button2_Click(object sender, EventArgs e)//保存参数
        {
            string filename = $"{Path.GetFullPath("..")}\\product\\{SysStatus.CurProductName.Trim()}\\ThermSet.ini";// 配置文件路径
            if (textBox1.Text.Trim() != SysStatus.CurProductName.Trim())
            {
                filename = $"{Path.GetFullPath("..")}\\product\\{textBox1.Text.Trim()}\\HighSet.ini";
            }
            string path1 = filename;
            IniFile inf = new IniFile(path1);//确认路径是否存在，不存在则创建文件夹。        
            string STEP = "理论值";
            inf.WriteString(STEP, "L1", textL1.Text);
            inf.WriteString(STEP, "L2", textL2.Text);
            inf.WriteString(STEP, "L3", textL3.Text);
            inf.WriteString(STEP, "L4", textL4.Text);
            inf.WriteString(STEP, "L5", textL5.Text);
            inf.WriteString(STEP, "L6", textL6.Text);
            inf.WriteString(STEP, "L7", textL7.Text);
            inf.WriteString(STEP, "L8", textL8.Text);
            inf.WriteString(STEP, "L9", textL9.Text);
            inf.WriteString(STEP, "L10", textL10.Text);
            STEP = "上限值";
            inf.WriteString(STEP, "UP1", textUP1.Text);
            inf.WriteString(STEP, "UP2", textUP2.Text);
            inf.WriteString(STEP, "UP3", textUP3.Text);
            inf.WriteString(STEP, "UP4", textUP4.Text);
            inf.WriteString(STEP, "UP5", textUP5.Text);
            inf.WriteString(STEP, "UP6", textUP6.Text);
            inf.WriteString(STEP, "UP7", textUP7.Text);
            inf.WriteString(STEP, "UP8", textUP8.Text);
            inf.WriteString(STEP, "UP9", textUP9.Text);
            inf.WriteString(STEP, "UP10", textUP10.Text);
            STEP = "下限值";
            inf.WriteString(STEP, "DW1", textDW1.Text);
            inf.WriteString(STEP, "DW2", textDW2.Text);
            inf.WriteString(STEP, "DW3", textDW3.Text);
            inf.WriteString(STEP, "DW4", textDW4.Text);
            inf.WriteString(STEP, "DW5", textDW5.Text);
            inf.WriteString(STEP, "DW6", textDW6.Text);
            inf.WriteString(STEP, "DW7", textDW7.Text);
            inf.WriteString(STEP, "DW8", textDW8.Text);
            inf.WriteString(STEP, "DW9", textDW9.Text);
            inf.WriteString(STEP, "DW10", textDW10.Text);
            thermSet1.LoadParam();
        }
        #endregion
    }
}
