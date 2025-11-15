using MyLib.Param;
using MyLib.Sys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CXPro001.classes;
using CXPro001.myclass;
namespace CXPro001.controls
{
    public partial class BarCordControl : UserControl
    {
        public BarCordControl()
        {
            InitializeComponent();
        }
        private barcode_delijie delijies;
        private barcode_jienshi jienshis;
        Timer timer1;
        public enum bartype
        { 
            delijie=0,
            jienshi=1,
            huowell=2    
        }
        private bartype bartp;

        public void init()
        {
          //  delijies = hardware.delijie1;
        //    jienshis = hardware.jienshi1;
            bartp = bartype.delijie;
            radioButton1.Checked = true;
            timer1 = new Timer();
            timer1.Interval = 500;          
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            timer1.Enabled = true;
            SHUA(jienshis);
            SHUA1(delijies);
        }
        #region 基恩士收发事件
        private void SHUA(barcode_jienshi SSS)
        {
            SSS.jieshou += myget;
            SSS.fasong += myset;
        }
        private void myget(object sender, barcode_jienshi.PaperC e)
        {
            Invoke(new Action(() => {
                listBox1.Items.Add($"{DateTime.Now.ToString()}基恩士接收:{e.Name}");
                textBox1.Text = e.Name;
            }));
        }
        private void myset(object sender, barcode_jienshi.PaperC e)
        {
            Invoke(new Action(() => {
                listBox1.Items.Add($"{DateTime.Now.ToString()}基恩士发送:{e.Name1}");
            }));
        }
        #endregion
        #region 得利捷收发事件
        private void SHUA1(barcode_delijie SSS1)
        {
            SSS1.jieshou1 += myget1;
            SSS1.fasong1 += myset1;
        }
        private void myget1(object sender, barcode_delijie.PaperC e)
        {
            Invoke(new Action(() => {
                listBox1.Items.Add($"{DateTime.Now.ToString()}得利捷接收:{e.Name}");
                textBox1.Text = e.Name;
            }));
        }
        private void myset1(object sender, barcode_delijie.PaperC e)
        {
            Invoke(new Action(() => {
                listBox1.Items.Add($"{DateTime.Now.ToString()}得利捷发送:{e.Name1}");
            }));
        }
        #endregion
        public void timerclose()
        {
            timer1.Enabled = false;
        }
        public void timeropen()
        {
            timer1.Enabled = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 5000) listBox1.Items.Clear();     
            if(bartp == bartype.delijie &delijies.IsConnected)
            {

                label1.Text = "已连接";
                label1.BackColor = Color.Lime;
            }
            else if (bartp == bartype.jienshi & jienshis.IsConnected)
            {
                label1.Text = "已连接";
                label1.BackColor = Color.Lime;
            }
            else
            {
                label1.Text = "未连接";
                label1.BackColor = Color.Red;
            }
        }
        private bool barcheck(string bacord)//检查二维码是否重码
        {
           bool Checktrue = ParamAttribute.checkbar(bacord);
            if (!Checktrue)
            {
                label4.Text = "NG";
                label4.BackColor = Color.Red;
                listBox1.Items.Add($"{DateTime.Now.ToString()}对比失败：{bacord}");
                return false;
            }
            else
            {
                label4.Text = "OK";
                label4.BackColor = Color.Lime;
                listBox1.Items.Add($"{DateTime.Now.ToString()}对比成功：{bacord}");
                return true;
            }           
        }
        #region 按钮事件
        private void button1_Click(object sender, EventArgs e)//启动扫码
        {
            if(bartp==bartype.delijie)
            {
                MessageBox.Show("得利捷扫码枪无法发送指令，只能手动触发");
                return;
            }
            else if (bartp == bartype.jienshi)
            {
                jienshis.Sendb("LON\u000d");
            }
            else if(bartp == bartype.huowell)
            {
                jienshis.Sendb("LON\u000d");
            }    
        }
        private void button2_Click(object sender, EventArgs e)//停止扫码
        {
            if (bartp == bartype.delijie)
            {
                MessageBox.Show("得利捷扫码枪无法发送指令控制");
                return;
            }
            else if (bartp == bartype.jienshi)
            {
                jienshis.Sendb("LOFF\u000d");
            }
            else if (bartp == bartype.huowell)
            {
                jienshis.Sendb("LOFF\u000d");
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)//基恩士
        {
            bartp = bartype.jienshi;
            groupBox1.Text = "基恩士扫码枪";
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)//得利捷
        {
            bartp = bartype.delijie;
            groupBox1.Text = "得利捷扫码枪";
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)//霍尼韦尔
        {
            bartp = bartype.huowell;
            groupBox1.Text = "霍尼韦尔扫码枪";
        }

        private void button3_Click(object sender, EventArgs e)//清除记录
        {
             listBox1.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e)//重码比对
        {
            barcheck(textBox1.Text);
        }
        #endregion
    }
}
