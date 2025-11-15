using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CXPro001.myclass;
namespace CXPro001.forms
{
    public partial class Frm_Set_Modle : Form
    {
        public Frm_Set_Modle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SysStatus.Modle =2;
            SysStatus.SaveConfig(SysStatus.sys_dir_path);
            Logger.Info($"系统参数修改成功");//写入日志.
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SysStatus.Modle = 1;
            SysStatus.SaveConfig(SysStatus.sys_dir_path);
            Logger.Info($"系统参数修改成功");//写入日志.
            this.Close();
        }

        private void Frm_Set_Modle_Load(object sender, EventArgs e)
        {
            if(SysStatus.Modle==1)
            {
                button2.BackColor = Color.Red;
            }else
            {
                button1.BackColor = Color.Red;
            }
        }
    }
}
