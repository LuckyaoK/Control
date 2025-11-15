using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLib.Input;

using CXPro001.classes;
using CXPro001.myclass;
using CXPro001.setups;

namespace CXPro001.ShowControl
{
    /// <summary>
    /// 位置度显示-设置控件
    /// </summary>
    public partial class CCDPosShowCtr : UserControl
    {
        private PostionSet postionSet2 = null;//位置度参数
        public CCDPosShowCtr()
        {
            InitializeComponent();
        }
        #region 做显示控件来用
        /// <summary>
        /// 清空显示
        /// </summary>
        public void ClearAll()
        {
            Invoke(new Action(() => {
                labe1.Text = "-";
                label14.Text = "-";
                lab1X.Text = "-";
                lab1Y.Text = "-";
                lab2X.Text = "-";
                lab2Y.Text = "-";
                lab3X.Text = "-";
                lab3Y.Text = "-";
                lab1H.Text = "-";
                lab2H.Text = "-";
                lab3H.Text = "-";
                label14.BackColor = Color.White;
                lab1X.BackColor = Color.White;
                lab1Y.BackColor = Color.White;
                lab2X.BackColor = Color.White;
                lab2Y.BackColor = Color.White;
                lab3X.BackColor = Color.White;
                lab3Y.BackColor = Color.White;
                lab1H.BackColor = Color.White;
                lab2H.BackColor = Color.White;
                lab3H.BackColor = Color.White;

            }));
        }
        /// <summary>
        /// 显示最终的结果
        /// </summary>
        /// <param name="res">测量值组</param>
        public void ShowResAll(PostionSet postionSet)
        {
            Invoke(new Action(() => {  
                if(postionSet.Status!="屏蔽中")
                {
                    labe1.Text = postionSet.Cords;
                    label14.Text = postionSet.ResEnd ? "OK" : "NG";
                    lab1X.Text = postionSet.RealX1.ToString();
                    lab1Y.Text = postionSet.RealY1.ToString();
                    lab2X.Text = postionSet.RealX2.ToString();
                    lab2Y.Text = postionSet.RealY2.ToString();
                    lab3X.Text = postionSet.RealX3.ToString();
                    lab3Y.Text = postionSet.RealY3.ToString();
                    lab1H.Text = postionSet.RealX4.ToString();
                    lab2H.Text = postionSet.RealY4.ToString();
                    lab3H.Text = postionSet.RealX5.ToString();
                    label14.BackColor = postionSet.ResEnd ? Color.LimeGreen : Color.Red;
                    lab1X.BackColor = postionSet.ResPinX1 ? Color.LimeGreen : Color.Red;
                    lab1Y.BackColor = postionSet.ResPinY1 ? Color.LimeGreen : Color.Red;
                    lab2X.BackColor = postionSet.ResPinX2 ? Color.LimeGreen : Color.Red;
                    lab2Y.BackColor = postionSet.ResPinY2 ? Color.LimeGreen : Color.Red;
                    lab3X.BackColor = postionSet.ResPinX3 ? Color.LimeGreen : Color.Red;
                    lab3Y.BackColor = postionSet.ResPinY3 ? Color.LimeGreen : Color.Red;
                    lab1H.BackColor = postionSet.ResPinX4 ? Color.LimeGreen : Color.Red;
                    lab2H.BackColor = postionSet.ResPinY4 ? Color.LimeGreen : Color.Red;
                    lab3H.BackColor = postionSet.ResPinX5 ? Color.LimeGreen : Color.Red;
                }
                else
                {
                    labe1.Text = postionSet.Cords;
                    label14.Text = "屏蔽中";
                    lab1X.Text = postionSet.RealX1.ToString();
                    lab1Y.Text = postionSet.RealY1.ToString();
                    lab2X.Text = postionSet.RealX2.ToString();
                    lab2Y.Text = postionSet.RealY2.ToString();
                    lab3X.Text = postionSet.RealX3.ToString();
                    lab3Y.Text = postionSet.RealY3.ToString();
                    lab1H.Text = postionSet.RealX4.ToString();
                    lab2H.Text = postionSet.RealY4.ToString();
                    lab3H.Text = postionSet.RealX5.ToString();
                    label14.BackColor = lab1X.BackColor = lab1Y.BackColor = lab2X.BackColor = lab2Y.BackColor = lab3X.BackColor =
                    lab3Y.BackColor = lab1H.BackColor = lab2H.BackColor = lab3H.BackColor = Color.Yellow;
                }
            
        }));
        }
        
        /// <summary>
        /// 显示理论值，上下限 产品型号
        /// </summary>
        public void LaodShow(PostionSet postionSet1)
        {
            Invoke(new Action(() => {
                label11.Text = SysStatus.CurProductName.Trim();
            //理论值
            labL1.Text = postionSet1.TheoryX1.ToString(); 
            labL2.Text = postionSet1.TheoryY1.ToString();
            labL3.Text = postionSet1.TheoryX2.ToString();
            labL4.Text = postionSet1.TheoryY2.ToString();
            labL5.Text = postionSet1.TheoryX3.ToString();
            labL6.Text = postionSet1.TheoryY3.ToString();
            labL7.Text = postionSet1.TheoryX4.ToString();
            labL8.Text = postionSet1.TheoryY4.ToString();
            labL9.Text = postionSet1.TheoryX5.ToString();
            //上限
            labUP1.Text = postionSet1.UpX1.ToString();
            labUP2.Text = postionSet1.UpY1.ToString();
            labUP3.Text = postionSet1.UpX2.ToString();
            labUP4.Text = postionSet1.UpY2.ToString();
            labUP5.Text = postionSet1.UpX3.ToString();
            labUP6.Text = postionSet1.UpY3.ToString();
            labUP7.Text = postionSet1.UpX4.ToString();
            labUP8.Text = postionSet1.UpY4.ToString();
            labUP9.Text = postionSet1.UpX5.ToString();
            //下限
            labDW1.Text = postionSet1.DownX1.ToString();
            labDW2.Text = postionSet1.DownY1.ToString();
            labDW3.Text = postionSet1.DownX2.ToString();
            labDW4.Text = postionSet1.DownY2.ToString();
            labDW5.Text = postionSet1.DownX3.ToString();
            labDW6.Text = postionSet1.DownY3.ToString();
            labDW7.Text = postionSet1.DownX4.ToString();
            labDW8.Text = postionSet1.DownY4.ToString();
            labDW9.Text = postionSet1.DownX5.ToString();
            }));
        }
        #endregion
        #region  做设置参数控件来用
        /// <summary>
        /// 初始化--设置控件时使用
        /// </summary>
        /// <param name="postionSet"></param>
        public void init(PostionSet postionSet)
        {
            postionSet2 = postionSet;
            label11.Text = SysStatus.CurProductName;
        }
        /// <summary>
        /// 加载参数
        /// </summary>
        public void LoadSet()
        {
            postionSet2.LoadParm(label11.Text.Trim());
            LaodShow(postionSet2);         
        }
        /// <summary>
        /// 保存参数
        /// </summary>
        public void SaveSet()
        {
            try
            {
                //理论值
                postionSet2.TheoryX1 = Convert.ToDouble(labL1.Text);
                postionSet2.TheoryY1 = Convert.ToDouble(labL2.Text);
                postionSet2.TheoryX2 = Convert.ToDouble(labL3.Text);
                postionSet2.TheoryY2 = Convert.ToDouble(labL4.Text);
                postionSet2.TheoryX3 = Convert.ToDouble(labL5.Text);
                postionSet2.TheoryY3 = Convert.ToDouble(labL6.Text);
                postionSet2.TheoryX4 = Convert.ToDouble(labL7.Text);
                postionSet2.TheoryY4 = Convert.ToDouble(labL8.Text);
                postionSet2.TheoryX5 = Convert.ToDouble(labL9.Text);
                //上限
                postionSet2.UpX1 = Convert.ToDouble(labUP1.Text);
                postionSet2.UpY1 = Convert.ToDouble(labUP2.Text);
                postionSet2.UpX2 = Convert.ToDouble(labUP3.Text);
                postionSet2.UpY2 = Convert.ToDouble(labUP4.Text);
                postionSet2.UpX3 = Convert.ToDouble(labUP5.Text);
                postionSet2.UpY3 = Convert.ToDouble(labUP6.Text);
                postionSet2.UpX4 = Convert.ToDouble(labUP7.Text);
                postionSet2.UpY4 = Convert.ToDouble(labUP8.Text);
                postionSet2.UpX5 = Convert.ToDouble(labUP9.Text);
                //下限
                postionSet2.DownX1 = Convert.ToDouble(labDW1.Text);
                postionSet2.DownY1 = Convert.ToDouble(labDW2.Text);
                postionSet2.DownX2 = Convert.ToDouble(labDW3.Text);
                postionSet2.DownY2 = Convert.ToDouble(labDW4.Text);
                postionSet2.DownX3 = Convert.ToDouble(labDW5.Text);
                postionSet2.DownY3 = Convert.ToDouble(labDW6.Text);
                postionSet2.DownX4 = Convert.ToDouble(labDW7.Text);
                postionSet2.DownY4 = Convert.ToDouble(labDW8.Text);
                postionSet2.DownX5 = Convert.ToDouble(labDW9.Text);

                postionSet2.SaveParm(label11.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }
        private void labL1_DoubleClick(object sender, EventArgs e)
        {
            if (ChangeText) NumPad.Show((Label)sender);
        }

        #endregion
        #region 自定义属性
        private bool CHANGETEXT = false;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("修改内容权限")]
        public bool ChangeText
        {
            get { return CHANGETEXT; }
            set
            {
                CHANGETEXT = value;
            }
        }
        private string p1text = "Pin1";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P1名称")]
        public string P1Text
        {
            get { return p1text; }
            set
            {
                this.label8.Text = p1text = value;

            }
        }
        private string p2text = "Pin2";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P2名称")]
        public string P2Text
        {
            get { return p2text; }
            set
            {
                this.label10.Text = p2text = value;

            }
        }

        private string p3text = "Pin3";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P3名称")]
        public string P3Text
        {
            get { return p3text; }
            set
            {
                this.label12.Text = p3text = value;

            }
        }

        private string p4text = "Pin4";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P4名称")]
        public string P4Text
        {
            get { return p4text; }
            set
            {
                this.label26.Text = p4text = value;

            }
        }

        private string p5text = "Pin5";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P5名称")]
        public string P5Text
        {
            get { return p5text; }
            set
            {
                this.label28.Text = p5text = value;

            }
        }

        private string p6text = "Pin6";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P6名称")]
        public string P6Text
        {
            get { return p6text; }
            set
            {
                this.label30.Text = p6text = value;
            }
        }
        private string p7text = "Pin7";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P7名称")]
        public string P7Text
        {
            get { return p7text; }
            set
            {
                this.label32.Text = p7text = value;
            }
        }
        private string p8text = "Pin8";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P8名称")]
        public string P8Text
        {
            get { return p8text; }
            set
            {
                this.label34.Text = p8text = value;
            }
        }

        private string p9text = "Pin9";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P9名称")]
        public string P9Text
        {
            get { return p9text; }
            set
            {
                this.label36.Text = p9text = value;
            }
        }

        private string p10text = "理论值";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P10名称")]
        public string P10Text
        {
            get { return p10text; }
            set
            {
                this.label3.Text = p10text = value;
            }
        }

        private string p11text = "实测值";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P11名称")]
        public string P11Text
        {
            get { return p11text; }
            set
            {
                this.label5.Text = p11text = value;
            }
        }
        private string p12text = "上限";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P12名称")]
        public string P12Text
        {
            get { return p12text; }
            set
            {
                this.label7.Text = p12text = value;
            }
        }
        private string p13text = "下限值";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P13名称")]
        public string P13Text
        {
            get { return p13text; }
            set
            {
                this.label9.Text = p13text = value;
            }
        }
        #endregion

       
    }
}
