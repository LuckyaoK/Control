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
    public partial class IOMonitor : UserControl
    {
        public IOMonitor()
        {
            InitializeComponent();
        }
        PLC_jienshi jienshiplc;
        string AdressPLC = "";
        public void init()
        {
            jienshiplc = hardware.PLC_KEY;
            SHUA(jienshiplc);
        }
        private void SHUA(PLC_jienshi SSS)
        {
            SSS.jieshou += myget;
            SSS.fasong += myset;
        }
        private void myget(object sender, PLC_jienshi.PaperC e)//接收信息
        {
            if (!timer1.Enabled) return;
            Invoke(new Action(() => {
                listBox1.Items.Add($"{DateTime.Now.ToString()}接收:{e.Name}");
            }));
        }
        private void myset(object sender, PLC_jienshi.PaperC e)//发送信息
        {
            if (!timer1.Enabled) return;
            Invoke(new Action(() => {
                listBox1.Items.Add($"{DateTime.Now.ToString()}发送:{e.Name1}");
            }));
        }
        public void timerclose()
        {
            timer1.Enabled = false;
            button1.BackColor = Color.Transparent;
            button1.Text = "打开监控";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            button1.BackColor = Color.Lime;
            button1.Text = "监控中";
        }
        int SLEPP = 100;
        EmRes ret;
        string y;
        private void timer1_Tick(object sender, EventArgs e)
        {
            ret = EmRes.Succeed;
            if(listBox1.Items.Count>5000)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("通讯记录");
            }
            SLEPP = 100;
            label1.Text = "Station1:"+ RunBuf.Station1.ToString();
            label2.Text = "Station2:" + RunBuf.Station2.ToString();
            label3.Text = "Station3:" + RunBuf.Station3.ToString();
            label4.Text = "Station4:" + RunBuf.Station4.ToString();
            label5.Text = "Station5:" + RunBuf.Station5.ToString();
            label6.Text = "Station6:" + RunBuf.Station6.ToString();


            //读B1000
            if (!jienshiplc.IsConnected) return;
            AdressPLC = "B1000";
          







        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("通讯记录");
        }
    }
}
