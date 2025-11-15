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
    public partial class in8out8Control1 : UserControl
    {
        in8out8 in8Out81;
        byte[] AA;
        public in8out8Control1()
        {
            InitializeComponent();
        }
        public void init()
        {
            in8Out81 = hardware.in8Out1;
            AA = new byte[9] { 0x00, 0x5A, 0x56, 0x00, 0x07, 0x00, 0x00, 0x00, 0xB7 };
            SHUA(in8Out81);
        }

        private void SHUA(in8out8 SSS)
        {
            SSS.jieshou += myget;
            SSS.fasong += myset;
        }
        private void myget(object sender, in8out8.PaperC e)
        {
            Invoke(new Action(() => {
                for(int i=0;i<e.Name.Length;i++)
                {
                    listBox1.Items.Add($"{DateTime.Now.ToString()}接收:{e.Name[i]}");
                }
               
            }));
        }
        private void myset(object sender, in8out8.PaperC e)
        {
            Invoke(new Action(() => {
                for (int i = 0; i < e.Name1.Length; i++)
                {
                    listBox1.Items.Add($"{DateTime.Now.ToString()}发送:{e.Name1[i]}");
                }

            }));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 读入命令(十六进制): 00 5A 56 00 07 00 00 00 B7           
            AA = new byte[9] { 0x00, 0x5A, 0x56, 0x00, 0x07, 0x00, 0x00, 0x00, 0xB7 };
        
            in8Out81.serialPort.Write(AA,0,9);
            Thread.Sleep(120);
            if(in8Out81.ReceiveByte.Length==9)
            {
                string str = "";
                for (int i = 0; i < 8; i++)
                {
                    //右移 与1相与 从右向左一位一位取    目的数.ToString
                    var t = ((in8Out81.ReceiveByte[7] >> (7 - i)) & 1).ToString();
                    //字符串拼接
                    str += t;
                }
                if (str.Substring(7, 1) == "1") checkBox1.Checked = true;
                else checkBox1.Checked = false;
                if (str.Substring(6, 1) == "1") checkBox2.Checked = true;
                else checkBox2.Checked = false;
                if (str.Substring(5, 1) == "1") checkBox3.Checked = true;
                else checkBox3.Checked = false;
                if (str.Substring(4, 1) == "1") checkBox4.Checked = true;
                else checkBox4.Checked = false;
                if (str.Substring(3, 1) == "1") checkBox5.Checked = true;
                else checkBox5.Checked = false;
                if (str.Substring(2, 1) == "1") checkBox6.Checked = true;
                else checkBox6.Checked = false;
                if (str.Substring(1, 1) == "1") checkBox7.Checked = true;
                else checkBox7.Checked = false;
                if (str.Substring(0, 1) == "1") checkBox8.Checked = true;
                else checkBox8.Checked = false;
                str = "";
                for (int i = 0; i < 8; i++)
                {
                    //右移 与1相与 从右向左一位一位取    目的数.ToString
                    var t = ((in8Out81.ReceiveByte[6] >> (7 - i)) & 1).ToString();
                    //字符串拼接
                    str += t;
                }
                if (str.Substring(7, 1) == "1") checkBY0.Checked = true;
                else checkBY0.Checked = false;
                if (str.Substring(6, 1) == "1") checkBY1.Checked = true;
                else checkBY1.Checked = false;
                if (str.Substring(5, 1) == "1") checkBY2.Checked = true;
                else checkBY2.Checked = false;
                if (str.Substring(4, 1) == "1") checkBY3.Checked = true;
                else checkBY3.Checked = false;
                if (str.Substring(3, 1) == "1") checkBY4.Checked = true;
                else checkBY4.Checked = false;
                if (str.Substring(2, 1) == "1") checkBY5.Checked = true;
                else checkBY5.Checked = false;
                if (str.Substring(1, 1) == "1") checkBY6.Checked = true;
                else checkBY6.Checked = false;
                if (str.Substring(0, 1) == "1") checkBY7.Checked = true;
                else checkBY7.Checked = false;
                str = "";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void checkY0_CheckedChanged(object sender, EventArgs e)//打开或关闭Y
        {
            if(((CheckBox)sender).Checked)
            {
                switch (((CheckBox)sender).Name)
                {
                    case "checkY0":
                        AA = new byte[9] { 0x00, 0x5A, 0x56, 0x00, 0x01, 0x01, 0x00, 0x00, 0xB2 };//00 5A 56 00 01 04 00 00 B5
                        in8Out81.serialPort.Write(AA, 0, 9);
                        break;
                    case "checkY1":
                        AA = new byte[9] { 0x00, 0x5A, 0x56, 0x00, 0x01, 0x02, 0x00, 0x00, 0xB3 };//00 5A 56 00 01 04 00 00 B5
                        in8Out81.serialPort.Write(AA, 0, 9);
                        break;
                    case "checkY2":
                        AA = new byte[9] { 0x00, 0x5A, 0x56, 0x00, 0x01, 0x03, 0x00, 0x00, 0xB4 };//00 5A 56 00 01 04 00 00 B5
                        in8Out81.serialPort.Write(AA, 0, 9);
                        break;
                    case "checkY3":
                        AA = new byte[9] { 0x00, 0x5A, 0x56, 0x00, 0x01, 0x04, 0x00, 0x00, 0xB5 };//00 5A 56 00 01 04 00 00 B5
                        in8Out81.serialPort.Write(AA, 0, 9);
                        break;
                    case "checkY4":
                        AA = new byte[9] { 0x00, 0x5A, 0x56, 0x00, 0x01, 0x05, 0x00, 0x00, 0xB6 };//00 5A 56 00 01 04 00 00 B5
                        in8Out81.serialPort.Write(AA, 0, 9);
                        break;
                    case "checkY5":
                        AA = new byte[9] { 0x00, 0x5A, 0x56, 0x00, 0x01, 0x06, 0x00, 0x00, 0xB7 };//00 5A 56 00 01 04 00 00 B5
                        in8Out81.serialPort.Write(AA, 0, 9);
                        break;
                    case "checkY6":
                        AA = new byte[9] { 0x00, 0x5A, 0x56, 0x00, 0x01, 0x07, 0x00, 0x00, 0xB8 };//00 5A 56 00 01 04 00 00 B5
                        in8Out81.serialPort.Write(AA, 0, 9);
                        break;
                    case "checkY7":
                        AA = new byte[9] { 0x00, 0x5A, 0x56, 0x00, 0x01, 0x08, 0x00, 0x00, 0xB9 };//00 5A 56 00 01 04 00 00 B5
                        in8Out81.serialPort.Write(AA, 0, 9);
                        break;
                }
            }
            else
            {
                switch (((CheckBox)sender).Name)
                {
                    case "checkY0":
                        AA = new byte[9] { 0x00, 0x5A, 0x56, 0x00, 0x02, 0x01, 0x00, 0x00, 0xB3 };//00 5A 56 00 01 04 00 00 B5
                        in8Out81.serialPort.Write(AA, 0, 9);
                        break;
                    case "checkY1":
                        AA = new byte[9] { 0x00, 0x5A, 0x56, 0x00, 0x02, 0x02, 0x00, 0x00, 0xB4 };//00 5A 56 00 01 04 00 00 B5
                        in8Out81.serialPort.Write(AA, 0, 9);
                        break;
                    case "checkY2":
                        AA = new byte[9] { 0x00, 0x5A, 0x56, 0x00, 0x02, 0x03, 0x00, 0x00, 0xB5 };//00 5A 56 00 01 04 00 00 B5
                        in8Out81.serialPort.Write(AA, 0, 9);
                        break;
                    case "checkY3":
                        AA = new byte[9] { 0x00, 0x5A, 0x56, 0x00, 0x02, 0x04, 0x00, 0x00, 0xB6 };//00 5A 56 00 01 04 00 00 B5
                        in8Out81.serialPort.Write(AA, 0, 9);
                        break;
                    case "checkY4":
                        AA = new byte[9] { 0x00, 0x5A, 0x56, 0x00, 0x02, 0x05, 0x00, 0x00, 0xB7 };//00 5A 56 00 01 04 00 00 B5
                        in8Out81.serialPort.Write(AA, 0, 9);
                        break;
                    case "checkY5":
                        AA = new byte[9] { 0x00, 0x5A, 0x56, 0x00, 0x02, 0x06, 0x00, 0x00, 0xB8 };//00 5A 56 00 01 04 00 00 B5
                        in8Out81.serialPort.Write(AA, 0, 9);
                        break;
                    case "checkY6":
                        AA = new byte[9] { 0x00, 0x5A, 0x56, 0x00, 0x02, 0x07, 0x00, 0x00, 0xB9 };//00 5A 56 00 01 04 00 00 B5
                        in8Out81.serialPort.Write(AA, 0, 9);
                        break;
                    case "checkY7":
                        AA = new byte[9] { 0x00, 0x5A, 0x56, 0x00, 0x02, 0x08, 0x00, 0x00, 0xBA };//00 5A 56 00 01 04 00 00 B5
                        in8Out81.serialPort.Write(AA, 0, 9);
                        break;
                }
            }
           

                
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AA = new byte[9] { 0x00, 0x5A, 0x56, 0x00, 0x04, 0x00, 0x00, 0x00, 0xB4 };//00 5A 56 00 04 00 00 00 B4
            in8Out81.serialPort.Write(AA, 0, 9);
        }
    }
}
