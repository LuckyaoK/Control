using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CXPro001.classes;
using CXPro001.myclass;

namespace CXPro001.controls
{
    public partial class KEYPLC_Control1 : UserControl
    {
        public KEYPLC_Control1()
        {
            InitializeComponent();
        }
        PLC_jienshi plckey;
        public void init()
        {
            plckey = hardware.PLC_KEY;
            timer1.Enabled = true;
            SHUA(plckey);
        }
        private void SHUA(PLC_jienshi SSS)
        {
            SSS.jieshou += myget;
            SSS.fasong += myset;
        }
        private void myget(object sender,PLC_jienshi.PaperC e)
        {
            Invoke(new Action(() => {
                listBox3.Items.Add($"{DateTime.Now.ToString()}接收:{e.Name}");
            }));         
        }
        private void myset(object sender, PLC_jienshi.PaperC e)
        {
            Invoke(new Action(() => {
                listBox3.Items.Add($"{DateTime.Now.ToString()}发送:{e.Name1}");
            }));         
        }
        private void button1_Click(object sender, EventArgs e)//读取
        {
            button1.Enabled = false;
            button2.Enabled = false;
            string a = "";
            int a1 = 0;
            string sens =  comboBox1.Text + textBox1.Text;
            if (comboBox2.Text == "整数") a1 = 1;
           else if (comboBox2.Text .Contains("浮点数")) a1 = 2;
            else if (comboBox2.Text == "位") a1 = 3;
            if(plckey.RWKeyPlc(true,sens,ref a,a1)==EmRes.Succeed) 
            listBox1.Items.Add($"返回：{a}");           
            else MessageBox.Show ("读取失败！");
           
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)//写入
        {
            button1.Enabled = false;
            button2.Enabled = false;
            string a = "";
            int a1 = 0;
            string sens = comboBox4.Text + textBox4.Text;
            a = textBox5.Text;
            if (comboBox3.Text == "整数") a1 = 1;
            else if (comboBox3.Text.Contains("浮点数")) a1 = 2;
            else if (comboBox3.Text == "位") a1 = 3;
            if (plckey.RWKeyPlc(false, sens,ref a, a1) != EmRes.Succeed) MessageBox.Show("发送数据失败！");
            else
            {
                listBox2.Items.Add($"写入成功！");
            }


            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (plckey.IsConnected)
            {
                label10.Text = "已连接";
                label10.BackColor = Color.Lime;
            }
            else
            {
                label10.Text = "未连接";
                label10.BackColor = Color.Red;
            }

            if (listBox3.Items.Count > 5000) listBox3.Items.Clear();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!plckey.IsConnected) plckey.connect();
          
            listBox1.Items.Clear();
            listBox2.Items.Clear();
             
        }

        private void label11_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
    }
}
