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

namespace CXPro001.ShowControl
{
    /// <summary>
    /// 模号检测显示控件
    /// </summary>
    public partial class ModeNumShowCtr : UserControl
    {
        public ModeNumShowCtr()
        {
            InitializeComponent();
        }
              
        
        /// <summary>
        /// 显示第一栏内容--二维码
        /// </summary>
        /// <param name="cord"></param>
        public void ShowCord(string cord)
        {
            Invoke(new Action(() => {
                CordText.Text = cord;
            }));
        }
        /// <summary>
        /// 显示第二栏内容
        /// </summary>
        /// <param name="text1"></param>
        public void ShowPin1text(string text1)
        {
            Invoke(new Action(() => {
                labText1.Text = text1;
            }));
        }
        /// <summary>
        /// 显示第二栏内容
        /// </summary>
        /// <param name="text1"></param>
        public void ShowPin2text(string text1)
        {
            Invoke(new Action(() => {
                labText2.Text = text1;
            }));
        }
        /// <summary>
        /// 显示第二栏内容
        /// </summary>
        /// <param name="text1"></param>
        public void ShowPin3text(string text1)
        {
            Invoke(new Action(() => {
                labText3.Text = text1;
            }));
        }
        /// <summary>
        /// 显示第二栏内容
        /// </summary>
        /// <param name="text1"></param>
        public void ShowPin4text(string text1)
        {
            Invoke(new Action(() => {
                labText4.Text = text1;
            }));
        }
        /// <summary>
        /// 清除显示
        /// </summary>
        public void ClearAll()
        {
            Invoke(new Action(() => {
                CordText.Text = labText1.Text = labText2.Text = labText3.Text = labText4.Text = "";
                CordText.BackColor = labText1.BackColor = labText2.BackColor = labText3.BackColor = labText4.BackColor = Color.White;
            }));
        }
        #region 属性
        private int namewidth = 100;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("名称栏宽度")]
        public int NameWidth
        {
            get { return namewidth; }
            set
            {                
                tableLayout1.ColumnStyles[0].SizeType = SizeType.Absolute;
                tableLayout1.ColumnStyles[0].Width= namewidth = value;
            }
        }

        private string SHORTOPEN1 = "二维码";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("二维码名称")]
        public string label14Text
        {
            get { return SHORTOPEN1; }
            set
            {
                this.labCord.Text = SHORTOPEN1 = value;
            }
        }
        private string pin1text = "模号";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("Pin1名称")]
        public string Pin1Text
        {
            get { return pin1text; }
            set
            {
                this.labPin1.Text = pin1text = value;
            }
        }
        private string pin2text = "磁极";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("Pin2名称")]
        public string Pin2Text
        {
            get { return pin2text; }
            set
            {
                this.labPin2.Text = pin2text = value;
            }
        }
        private string pin3text = "金属丝";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("Pin3名称")]
        public string Pin3Text
        {
            get { return pin3text; }
            set
            {
                this.labPin3.Text = pin3text = value;
            }
        }
        private string pin4text = "金属丝";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("Pin4名称")]
        public string Pin4Text
        {
            get { return pin4text; }
            set
            {
                this.labPin4.Text = pin4text = value;
            }
        }
        private int pinCount = 0;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("测试PIN脚数量")]
        public int PinCount
        {
            get { return pinCount; }
            set
            {
                if (value > 4 || value < 1)
                {
                    return;
                }
                pinCount = value;
                this.tableLayout1.RowCount = pinCount + 1;
                switch (pinCount)
                {
                    case 1:
                        labCord.Visible = CordText.Visible  = true;
                        labPin1.Visible = labText1.Visible = true;
                        labPin2.Visible = labText2.Visible = false;
                        labPin3.Visible = labText3.Visible = false;
                        labPin4.Visible = labText4.Visible = false;
                        break;
                    case 2:
                        labCord.Visible = CordText.Visible = true;
                        labPin1.Visible = labText1.Visible = true;
                        labPin2.Visible = labText2.Visible = true;
                        labPin3.Visible = labText3.Visible = false;
                        labPin4.Visible = labText4.Visible = false;
                        break;
                    case 3:
                        labCord.Visible = CordText.Visible = true;
                        labPin1.Visible = labText1.Visible = true;
                        labPin2.Visible = labText2.Visible = true;
                        labPin3.Visible = labText3.Visible = true;
                        labPin4.Visible = labText4.Visible = false;
                        break;
                    case 4:
                        labCord.Visible = CordText.Visible = true;
                        labPin1.Visible = labText1.Visible = true;
                        labPin2.Visible = labText2.Visible = true;
                        labPin3.Visible = labText3.Visible = true;
                        labPin4.Visible = labText4.Visible = true;
                        break;

                    default:

                        break;
                }
            }
        }
        #endregion
    }
}
