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
    public partial class Form_Hand_Select : Form
    {
        public Form_Hand_Select()
        {
            InitializeComponent();
        }
 
        private void timer1_Tick(object sender, EventArgs e)
        {
        
        }

        private void Form_Hand_Select_Load(object sender, EventArgs e)
        {
            textBox1.Text = SysStatus.Hand_Select_mode_L.ToString();
            textBox2.Text = SysStatus.Hand_Select_mode_R.ToString();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            int a = 1;
            int b =2;
            try
            {
                  a = int.Parse(textBox1.Text);
                  b = int.Parse(textBox2.Text);
            }
            catch
            {
                MessageBox.Show("数值无效");
                return;
            }
            SysStatus.Hand_Select_mode_L =a;
            SysStatus.Hand_Select_mode_R = b;
            this.Close();
        }
    }
}
