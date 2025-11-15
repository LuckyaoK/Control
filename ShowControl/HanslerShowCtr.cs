using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CXPro001.setups;

namespace CXPro001.ShowControl
{
    public partial class HanslerShowCtr : UserControl
    {
        public HanslerShowCtr()
        {
            InitializeComponent();
        }
         
        /// <summary>
        /// 显示要刻的二维码
        /// </summary>
        /// <param name="cord"></param>
        public void ShowCord(string cord1,string cord2,string read1,string read2,string leftCord="", string rigthCord = "")
        {
            Invoke(new Action(() =>{ 
                labe1.Text = cord1;
                labe2.Text = cord2;
                labe3.Text = leftCord;
                labe4.Text = rigthCord;

                if (read1 == "NG")
                {
                    labe1.BackColor = Color.Red;
                }
                else if(read1 == "OK")
                {
                    labe1.BackColor = Color.Green;
                }
                if (read2 == "NG")
                {
                    labe2.BackColor = Color.Red;
                }
                else if (read2 == "OK")
                {
                    labe2.BackColor = Color.Green;
                }
            }));           
        }
        
     
        public void clearshow()
        {
            Invoke(new Action(() => {
                labe1.Text = "";
                labe2.Text = "";
                labe3.Text = "";
                labe4.Text = "";
                labe5.Text = "";
                labe1.BackColor = Color.White;
                labe2.BackColor = Color.White;
                labe3.BackColor = Color.White;
                labe4.BackColor = Color.White;
                labe5.BackColor = Color.White;
            }));
        }



        #region -----------------自定义属性------------------------

        private string nameP1 = "二维码";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("Pin1名称")]
        public string P1name
        {
            get { return nameP1; }
            set
            {
                nameP1 = labelP1.Text = value;
            }
        }
        private string nameP2 = "一扫";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("Pin2名称")]
        public string P2name
        {
            get { return nameP2; }
            set
            {
                nameP2 = labelP2.Text = value;
            }
        }
        private string nameP3 = "打码";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("Pin3名称")]
        public string P3name
        {
            get { return nameP3; }
            set
            {
                nameP3 = labelP3.Text = value;
            }
        }
        private string nameP4 = "二扫";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("Pin4名称")]
        public string P4name
        {
            get { return nameP4; }
            set
            {
                nameP4 = labelP4.Text = value;
            }
        }
        private string nameP5 = "结果";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("Pin5名称")]
        public string P5name
        {
            get { return nameP5; }
            set
            {
                nameP5 = labelP5.Text = value;
            }
        }


        private string nameP6 = "结果";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("Pin6名称")]
        public string P6name
        {
            get { return nameP6; }
            set
            {
                nameP6 = labelP6.Text = value;
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
                if (value > 6 || value < 1)
                {
                    return;
                }
                pinCount = value;
                this.tableLayoutPanel2.RowCount = pinCount;
                switch (pinCount)
                {
                    case 2:
                        labelP1.Visible = labe1.Visible = true;
                        labelP2.Visible = labe2.Visible = true;
                        labelP3.Visible = labe3.Visible = false;
                        labelP4.Visible = labe4.Visible = false;
                        labelP5.Visible = labe5.Visible = false;
                        labelP6.Visible = labe6.Visible = false;
                        break;
                    case 3:
                        labelP1.Visible = labe1.Visible = true;
                        labelP2.Visible = labe2.Visible = true;
                        labelP3.Visible = labe3.Visible = true;
                        labelP4.Visible = labe4.Visible = false;
                        labelP5.Visible = labe5.Visible = false;
                        labelP6.Visible = labe6.Visible = false;
                        break;
                    case 4:
                        labelP1.Visible = labe1.Visible = true;
                        labelP2.Visible = labe2.Visible = true;
                        labelP3.Visible = labe3.Visible = true;
                        labelP4.Visible = labe4.Visible = true;
                        labelP5.Visible = labe5.Visible = false;
                        labelP6.Visible = labe6.Visible = false;
                        break;
                    case 5:
                        labelP1.Visible = labe1.Visible = true;
                        labelP2.Visible = labe2.Visible = true;
                        labelP3.Visible = labe3.Visible = true;
                        labelP4.Visible = labe4.Visible = true;
                        labelP5.Visible = labe5.Visible = true;
                        labelP6.Visible = labe6.Visible = false;
                        break;
                    case 6:
                        labelP1.Visible = labe1.Visible = true;
                        labelP2.Visible = labe2.Visible = true;
                        labelP3.Visible = labe3.Visible = true;
                        labelP4.Visible = labe4.Visible = true;
                        labelP5.Visible = labe5.Visible = true;
                        labelP6.Visible = labe6.Visible = true;
                        break;
                    default:

                        break;
                }
            }
        }



        private int namewith = 70;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("名称栏宽度")]
        public int NameWith
        {
            get { return namewith; }
            set
            {
                tableLayoutPanel2.ColumnStyles[0].SizeType = SizeType.Absolute;
                tableLayoutPanel2.ColumnStyles[0].Width = namewith = value;
            }
        }
        #endregion







    }

}
