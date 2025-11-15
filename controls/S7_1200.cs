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
using MyLib.Sys;

namespace CXPro001.controls
{
    public partial class S7_1200 : UserControl
    {
        public S7_1200()
        {
            InitializeComponent();
        }

   

        public void init()
        {
           
        
           
        }
    
        private void myget(object sender, s7com.PaperC e)
        {
           /* Invoke(new Action(() => {
                listBox1.Items.Add(DateTime.Now.ToString());
                for (int i = 0; i < e.jiecout; i++)
                {
                    listBox1.Items.Add($"接收:{e.receive[i].ToString("X")}");
                }


            }));*/
        }
        private void myset(object sender, s7com.PaperC e)
        {
          /*  Invoke(new Action(() => {
                listBox1.Items.Add(DateTime.Now.ToString());
                for (int i = 0; i < e.facout; i++)
                {
                    listBox1.Items.Add($"发送:{e.fabyte[i].ToString("X")}");
                }
            }));*/
        }
      
        #region 按钮事件
        private void button1_Click(object sender, EventArgs e)
        {
        
        }
        private void butBitTest_Click(object sender, EventArgs e)//位读取
        {
         
            
        }
        private void butBitSet_Click(object sender, EventArgs e)//位置ON
        {
         
        }
        private void butBitRst_Click(object sender, EventArgs e)//位复位
        {
        
        }
    
      

        #endregion

        private void btn_read_con_Click(object sender, EventArgs e)
        {
         
            var result = CXPro001.forms.FormRun.plc.ConnectServer();
            if (result.IsSuccess)
            {
                // 读取操作，这里的M100可以替换成I100,Q100,DB20.100效果时一样的
                MessageBox.Show("ok");
            }
            else
            {
                MessageBox.Show("fail");
            }
        }
    }
}
