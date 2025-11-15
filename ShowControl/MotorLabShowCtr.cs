using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CXPro001.ShowControl
{
    public partial class MotorLabShowCtr : UserControl
    {
        public MotorLabShowCtr()
        {
            InitializeComponent();
        }

        public delegate void ButtonClickedEventHandler(object sender, EventArgs e);
        // 定义事件
        public event ButtonClickedEventHandler ButtonClicked;
        public event ButtonClickedEventHandler ButtonClicked2;


        private string name ;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("名称")]
        public string NameText​
        {
            get { return name; }
            set
            {
                label1.Text = value;
                name = value;
                Refresh();
            }
        }

        private string str2;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("内容")]      
        public string TxtText​
        {
            get { return txt_pos.Text; }
            set
            {
                txt_pos.Text = value;
                str2 = value;
                Refresh();
            }
        }

        private string unit;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("标签内容")]
        public string UnitText​
        {
            get { return unit; }
            set
            {
                label2.Text = value;
                unit = value;
                Refresh();
            }
        }

        
        private int Mode = 1;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("显示模式")]
        public int Btn_Mode
        {
            get { return Mode; }
            set
            {
                if (value > 6 || value < 1)
                {
                    return;
                }
                Mode = value;
              
                switch (Mode)
                {
                    case 1:
                        btn_getpos.Visible = true;
                        btn_setpos.Visible = true;
                        break;
                    case 2:
                        btn_getpos.Visible = false;
                        btn_setpos.Visible = false;
                        break;
                }
            }
        }


        private void btn_getpos_Click(object sender, EventArgs e)
        {
            if (ButtonClicked != null)
            {
                ButtonClicked(this, e);
            }
        }

        private void btn_setpos_Click(object sender, EventArgs e)
        {
            if (ButtonClicked2 != null)
            {
                ButtonClicked2(this, e);
            }
        }
    }
}
