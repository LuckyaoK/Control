using MyLib.SqlHelper;
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
namespace CXPro001.controls
{
    public partial class SQL_Test : UserControl
    {
        public SQL_Test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sqlconn = textBox1.Text.Trim();
            string sqlcmd = textBox2.Text.Trim();
            var aa = SQLHelper.ExecuteScalar(sqlconn, sqlcmd);
            if(aa!=null)
            textBox3.Text = aa.ToString();
            else
                Logger.Error($"数据库操作失败 ", 0);



        }
    }
}
