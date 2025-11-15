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
using CXPro001.classes;

namespace CXPro001.controls
{
    public partial class GeneralTest : UserControl
    {
        public GeneralTest()
        {
            InitializeComponent();
        }
        test7631 ts9803;
        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("通讯记录");
        }
        public void init()
        {
          //  ts9803 = hardware.test9803;
            timer1.Enabled = true;
          //  tianjia(ts9803);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
           if( hardware.test9803.IsConnected)
            {
                label1.Text = "连接正常";
                label1.BackColor = Color.Lime;                  
            }
           else
            {
                label1.Text = "连接异常";
                label1.BackColor = Color.Red;
            }
        }
        private void tianjia(test7631 ts7)
        {
            //ts7.chuan += jin;
        }
        private void jin(object sender, test8740.PaperC e)
        {
            Invoke(new Action(() => {
                listBox1.Items.Add($"{DateTime.Now.ToString()}接收：{e.Name}");
                textBox1.Text = e.Name;
            }));
        }

        private void button1_Click(object sender, EventArgs e)//启动
        {
            button1.Enabled = false;
            if(ts9803.sends("")==EmRes.Succeed)
            {
                listBox1.Items.Add($"{DateTime.Now.ToString()}发送：");
            }
            else
            {
                MessageBox.Show("发送指令失败");
            }
            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            if (ts9803.sends("") == EmRes.Succeed)
            {
                listBox1.Items.Add($"{DateTime.Now.ToString()}发送：");
            }
            else
            {
                MessageBox.Show("发送指令失败");
            }
            button2.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            if (ts9803.sends("") == EmRes.Succeed)
            {
                listBox1.Items.Add($"{DateTime.Now.ToString()}发送：");
            }
            else
            {
                MessageBox.Show("发送指令失败");
            }
            button3.Enabled = true;
        }
    }
}
