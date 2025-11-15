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

using CXPro001.myclass;
using CXPro001.classes;
namespace CXPro001.ShowControl
{
    /// <summary>
    /// 高度显示-设置控件
    /// </summary>
    public partial class CCDHeightControls : UserControl
    {
       
        public CCDHeightControls()
        {
            InitializeComponent();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        }
        #region 做显示控件来用
        /// <summary>
        /// 显示最后的结果
        /// </summary>
        /// <param name="highset"></param>
        public void ShowResAll(My_CCD highset)
        {
            Invoke(new Action(() => { 
               
                    label2.Text = highset.Cords;
                    lbl333.Text = highset.ResAll ? "OK" : "NG";
                    lbl_P1.Text = highset.Current1H.ToString();
                    lbl_P2.Text = highset.Current2H.ToString();
                    lbl_P3.Text = highset.Current3H.ToString();
                    lbl_P4.Text = highset.Current4H.ToString();
                    lbl_P5.Text = highset.Current5H.ToString();
                    lbl_P6.Text = highset.Current6H.ToString();
                  //  lbl_P7.Text = highset.Current7H.ToString();
                  //  lbl_P8.Text = highset.Current8H.ToString();
                    lbl333.BackColor = highset.ResAll ? Color.LimeGreen : Color.Red;
          
             
                
           
            }));
        }
        /// <summary>
        /// 清除当前测量值显示
        /// </summary>
        public void clearshow()
        {
            Invoke(new Action(() => {
                lbl333.Text = label2.Text = "-";
                lbl333.BackColor = Color.White;
                foreach (Control CTRS in this.tableLayoutPanel1.Controls)
                {
                    if (CTRS is Label container)
                    {
                        if (container.Tag != null && container.Tag.ToString() == "AA1")
                        {
                            container.Text = "0";
                            container.BackColor = Color.White;
                        }
                    }
                }
            }));
        }
      
        /// <summary>
        /// 显示理论值，上下限值
        /// </summary>
        /// <param name="thermSet"></param>
        public void showLUPDW(Highset thermSet)
        {
            Invoke(new Action(() => { 
            lbl_P1N.Text = thermSet.L1.ToString();
            lbl_P2N.Text = thermSet.L2.ToString();
            lbl_P3N.Text = thermSet.L3.ToString();
            lbl_P4N.Text = thermSet.L4.ToString();
            lbl_P5N.Text = thermSet.L5.ToString();
            lbl_P6N.Text = thermSet.L6.ToString();
            lbl_P7N.Text = thermSet.L7.ToString();
            lbl_P8N.Text = thermSet.L8.ToString();
            lbl_P1_Up.Text = thermSet.UP1.ToString();
            lbl_P2_Up.Text = thermSet.UP2.ToString();
            lbl_P3_Up.Text = thermSet.UP3.ToString();
            lbl_P4_Up.Text = thermSet.UP4.ToString();
            lbl_P5_Up.Text = thermSet.UP5.ToString();
            lbl_P6_Up.Text = thermSet.UP6.ToString();
            lbl_P7_Up.Text = thermSet.UP7.ToString();
            lbl_P8_Up.Text = thermSet.UP8.ToString();
            lbl_P1_Down.Text = thermSet.DW1.ToString();
            lbl_P2_Down.Text = thermSet.DW2.ToString();
            lbl_P3_Down.Text = thermSet.DW3.ToString();
            lbl_P4_Down.Text = thermSet.DW4.ToString();
            lbl_P5_Down.Text = thermSet.DW5.ToString();
            lbl_P6_Down.Text = thermSet.DW6.ToString();
            lbl_P7_Down.Text = thermSet.DW7.ToString();
            lbl_P8_Down.Text = thermSet.DW8.ToString();
            }));
        }
        
        /// <summary>
        /// 显示产品型号
        /// </summary>
        /// <param name="names"></param>
        public void showtype(string names)
        {
            Invoke(new Action(() => { lbl222.Text = names; }));
           
        }
        #endregion
        #region 做设置控件来用
       public void init(Highset highset)
        {
          
         //   lbl222.Text = SysStatus.CurProductName;
        }   
        /// <summary>
        /// 加载参数并显示
        /// </summary>
     

        private void lbl_P1N_DoubleClick(object sender, EventArgs e)
        {

       //     if (ChangeText) NumPad.Show((Label)sender);

        }

        #endregion


        #region 自定义属性
        private int pinCount = 0;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("测试PIN脚数量")]
        public int PinCount
        {
            get { return pinCount; }
            set
            {
                if (value > 8 || value < 0)
                {
                    return;
                }
                pinCount = value;
                this.tableLayoutPanel1.RowCount = pinCount + 2;
                switch (pinCount)
                {

                    case 2:

                        this.lbl_P1N.Visible = this.P1.Visible = this.lbl_P1.Visible = lbl_P1_Up.Visible = lbl_P1_Down.Visible = true;
                        this.lbl_P2N.Visible = this.P2.Visible = this.lbl_P2.Visible = lbl_P2_Up.Visible = lbl_P2_Down.Visible = true;
                        this.lbl_P3N.Visible = this.P3.Visible = this.lbl_P3.Visible = lbl_P3_Up.Visible = lbl_P3_Down.Visible = false;
                        this.lbl_P4N.Visible = this.P4.Visible = this.lbl_P4.Visible = lbl_P4_Up.Visible = lbl_P4_Down.Visible = false;
                        this.lbl_P5N.Visible = this.P5.Visible = this.lbl_P5.Visible = lbl_P5_Up.Visible = lbl_P5_Down.Visible = false;

                        this.lbl_P6N.Visible = this.P6.Visible = this.lbl_P6.Visible = lbl_P6_Up.Visible = lbl_P6_Down.Visible = false;
                        this.lbl_P7N.Visible = this.P7.Visible = this.lbl_P7.Visible = lbl_P7_Up.Visible = lbl_P7_Down.Visible = false;
                        this.lbl_P8N.Visible = this.P8.Visible = this.lbl_P8.Visible = lbl_P8_Up.Visible = lbl_P8_Down.Visible = false;

                        break;
                    case 3:
                        this.lbl_P1N.Visible = this.P1.Visible = this.lbl_P1.Visible = lbl_P1_Up.Visible = lbl_P1_Down.Visible = true;
                        this.lbl_P2N.Visible = this.P2.Visible = this.lbl_P2.Visible = lbl_P2_Up.Visible = lbl_P2_Down.Visible = true;
                        this.lbl_P3N.Visible = this.P3.Visible = this.lbl_P3.Visible = lbl_P3_Up.Visible = lbl_P3_Down.Visible = true;
                        this.lbl_P4N.Visible = this.P4.Visible = this.lbl_P4.Visible = lbl_P4_Up.Visible = lbl_P4_Down.Visible = false;
                        this.lbl_P5N.Visible = this.P5.Visible = this.lbl_P5.Visible = lbl_P5_Up.Visible = lbl_P5_Down.Visible = false;

                        this.lbl_P6N.Visible = this.P6.Visible = this.lbl_P6.Visible = lbl_P6_Up.Visible = lbl_P6_Down.Visible = false;
                        this.lbl_P7N.Visible = this.P7.Visible = this.lbl_P7.Visible = lbl_P7_Up.Visible = lbl_P7_Down.Visible = false;
                        this.lbl_P8N.Visible = this.P8.Visible = this.lbl_P8.Visible = lbl_P8_Up.Visible = lbl_P8_Down.Visible = false;

                        break;
                    case 4:
                        this.lbl_P1N.Visible = this.P1.Visible = this.lbl_P1.Visible = lbl_P1_Up.Visible = lbl_P1_Down.Visible = true;
                        this.lbl_P2N.Visible = this.P2.Visible = this.lbl_P2.Visible = lbl_P2_Up.Visible = lbl_P2_Down.Visible = true;
                        this.lbl_P3N.Visible = this.P3.Visible = this.lbl_P3.Visible = lbl_P3_Up.Visible = lbl_P3_Down.Visible = true;
                        this.lbl_P4N.Visible = this.P4.Visible = this.lbl_P4.Visible = lbl_P4_Up.Visible = lbl_P4_Down.Visible = true;
                        this.lbl_P5N.Visible = this.P5.Visible = this.lbl_P5.Visible = lbl_P5_Up.Visible = lbl_P5_Down.Visible = false;

                        this.lbl_P6N.Visible = this.P6.Visible = this.lbl_P6.Visible = lbl_P6_Up.Visible = lbl_P6_Down.Visible = false;
                        this.lbl_P7N.Visible = this.P7.Visible = this.lbl_P7.Visible = lbl_P7_Up.Visible = lbl_P7_Down.Visible = false;
                        this.lbl_P8N.Visible = this.P8.Visible = this.lbl_P8.Visible = lbl_P8_Up.Visible = lbl_P8_Down.Visible = false;

                        break;
                    case 5:
                        this.lbl_P1N.Visible = this.P1.Visible = this.lbl_P1.Visible = lbl_P1_Up.Visible = lbl_P1_Down.Visible = true;
                        this.lbl_P2N.Visible = this.P2.Visible = this.lbl_P2.Visible = lbl_P2_Up.Visible = lbl_P2_Down.Visible = true;
                        this.lbl_P3N.Visible = this.P3.Visible = this.lbl_P3.Visible = lbl_P3_Up.Visible = lbl_P3_Down.Visible = true;
                        this.lbl_P4N.Visible = this.P4.Visible = this.lbl_P4.Visible = lbl_P4_Up.Visible = lbl_P4_Down.Visible = true;
                        this.lbl_P5N.Visible = this.P5.Visible = this.lbl_P5.Visible = lbl_P5_Up.Visible = lbl_P5_Down.Visible = true;

                        this.lbl_P6N.Visible = this.P6.Visible = this.lbl_P6.Visible = lbl_P6_Up.Visible = lbl_P6_Down.Visible = false;
                        this.lbl_P7N.Visible = this.P7.Visible = this.lbl_P7.Visible = lbl_P7_Up.Visible = lbl_P7_Down.Visible = false;
                        this.lbl_P8N.Visible = this.P8.Visible = this.lbl_P8.Visible = lbl_P8_Up.Visible = lbl_P8_Down.Visible = false;

                        break;
                    case 6:
                        this.lbl_P1N.Visible = this.P1.Visible = this.lbl_P1.Visible = lbl_P1_Up.Visible = lbl_P1_Down.Visible = true;
                        this.lbl_P2N.Visible = this.P2.Visible = this.lbl_P2.Visible = lbl_P2_Up.Visible = lbl_P2_Down.Visible = true;
                        this.lbl_P3N.Visible = this.P3.Visible = this.lbl_P3.Visible = lbl_P3_Up.Visible = lbl_P3_Down.Visible = true;
                        this.lbl_P4N.Visible = this.P4.Visible = this.lbl_P4.Visible = lbl_P4_Up.Visible = lbl_P4_Down.Visible = true;
                        this.lbl_P5N.Visible = this.P5.Visible = this.lbl_P5.Visible = lbl_P5_Up.Visible = lbl_P5_Down.Visible = true;
                        this.lbl_P6N.Visible = this.P6.Visible = this.lbl_P6.Visible = lbl_P6_Up.Visible = lbl_P6_Down.Visible = true;

                        this.lbl_P7N.Visible = this.P7.Visible = this.lbl_P7.Visible = lbl_P7_Up.Visible = lbl_P7_Down.Visible = false;
                        this.lbl_P8N.Visible = this.P8.Visible = this.lbl_P8.Visible = lbl_P8_Up.Visible = lbl_P8_Down.Visible = false;

                        break;
                    case 7:
                        this.lbl_P1N.Visible = this.P1.Visible = this.lbl_P1.Visible = lbl_P1_Up.Visible = lbl_P1_Down.Visible = true;
                        this.lbl_P2N.Visible = this.P2.Visible = this.lbl_P2.Visible = lbl_P2_Up.Visible = lbl_P2_Down.Visible = true;
                        this.lbl_P3N.Visible = this.P3.Visible = this.lbl_P3.Visible = lbl_P3_Up.Visible = lbl_P3_Down.Visible = true;
                        this.lbl_P4N.Visible = this.P4.Visible = this.lbl_P4.Visible = lbl_P4_Up.Visible = lbl_P4_Down.Visible = true;
                        this.lbl_P5N.Visible = this.P5.Visible = this.lbl_P5.Visible = lbl_P5_Up.Visible = lbl_P5_Down.Visible = true;
                        this.lbl_P6N.Visible = this.P6.Visible = this.lbl_P6.Visible = lbl_P6_Up.Visible = lbl_P6_Down.Visible = true;
                        this.lbl_P7N.Visible = this.P7.Visible = this.lbl_P7.Visible = lbl_P7_Up.Visible = lbl_P7_Down.Visible = true;

                        this.lbl_P8N.Visible = this.P8.Visible = this.lbl_P8.Visible = lbl_P8_Up.Visible = lbl_P8_Down.Visible = false;

                        break;
                    case 8:
                        this.lbl_P1N.Visible = this.P1.Visible = this.lbl_P1.Visible = lbl_P1_Up.Visible = lbl_P1_Down.Visible = true;
                        this.lbl_P2N.Visible = this.P2.Visible = this.lbl_P2.Visible = lbl_P2_Up.Visible = lbl_P2_Down.Visible = true;
                        this.lbl_P3N.Visible = this.P3.Visible = this.lbl_P3.Visible = lbl_P3_Up.Visible = lbl_P3_Down.Visible = true;
                        this.lbl_P4N.Visible = this.P4.Visible = this.lbl_P4.Visible = lbl_P4_Up.Visible = lbl_P4_Down.Visible = true;
                        this.lbl_P5N.Visible = this.P5.Visible = this.lbl_P5.Visible = lbl_P5_Up.Visible = lbl_P5_Down.Visible = true;
                        this.lbl_P6N.Visible = this.P6.Visible = this.lbl_P6.Visible = lbl_P6_Up.Visible = lbl_P6_Down.Visible = true;
                        this.lbl_P7N.Visible = this.P7.Visible = this.lbl_P7.Visible = lbl_P7_Up.Visible = lbl_P7_Down.Visible = true;
                        this.lbl_P8N.Visible = this.P8.Visible = this.lbl_P8.Visible = lbl_P8_Up.Visible = lbl_P8_Down.Visible = true;


                        break;
                    case 9:
                        this.lbl_P1N.Visible = this.P1.Visible = this.lbl_P1.Visible = lbl_P1_Up.Visible = lbl_P1_Down.Visible = true;
                        this.lbl_P2N.Visible = this.P2.Visible = this.lbl_P2.Visible = lbl_P2_Up.Visible = lbl_P2_Down.Visible = true;
                        this.lbl_P3N.Visible = this.P3.Visible = this.lbl_P3.Visible = lbl_P3_Up.Visible = lbl_P3_Down.Visible = true;
                        this.lbl_P4N.Visible = this.P4.Visible = this.lbl_P4.Visible = lbl_P4_Up.Visible = lbl_P4_Down.Visible = true;
                        this.lbl_P5N.Visible = this.P5.Visible = this.lbl_P5.Visible = lbl_P5_Up.Visible = lbl_P5_Down.Visible = true;
                        this.lbl_P6N.Visible = this.P6.Visible = this.lbl_P6.Visible = lbl_P6_Up.Visible = lbl_P6_Down.Visible = true;
                        this.lbl_P7N.Visible = this.P7.Visible = this.lbl_P7.Visible = lbl_P7_Up.Visible = lbl_P7_Down.Visible = true;
                        this.lbl_P8N.Visible = this.P8.Visible = this.lbl_P8.Visible = lbl_P8_Up.Visible = lbl_P8_Down.Visible = true;

                        break;
                    case 10:
                        this.lbl_P1N.Visible = this.P1.Visible = this.lbl_P1.Visible = lbl_P1_Up.Visible = lbl_P1_Down.Visible = true;
                        this.lbl_P2N.Visible = this.P2.Visible = this.lbl_P2.Visible = lbl_P2_Up.Visible = lbl_P2_Down.Visible = true;
                        this.lbl_P3N.Visible = this.P3.Visible = this.lbl_P3.Visible = lbl_P3_Up.Visible = lbl_P3_Down.Visible = true;
                        this.lbl_P4N.Visible = this.P4.Visible = this.lbl_P4.Visible = lbl_P4_Up.Visible = lbl_P4_Down.Visible = true;
                        this.lbl_P5N.Visible = this.P5.Visible = this.lbl_P5.Visible = lbl_P5_Up.Visible = lbl_P5_Down.Visible = true;
                        this.lbl_P6N.Visible = this.P6.Visible = this.lbl_P6.Visible = lbl_P6_Up.Visible = lbl_P6_Down.Visible = true;
                        this.lbl_P7N.Visible = this.P7.Visible = this.lbl_P7.Visible = lbl_P7_Up.Visible = lbl_P7_Down.Visible = true;
                        this.lbl_P8N.Visible = this.P8.Visible = this.lbl_P8.Visible = lbl_P8_Up.Visible = lbl_P8_Down.Visible = true;

                        break;
                    case 11:
                        this.lbl_P1N.Visible = this.P1.Visible = this.lbl_P1.Visible = lbl_P1_Up.Visible = lbl_P1_Down.Visible = true;
                        this.lbl_P2N.Visible = this.P2.Visible = this.lbl_P2.Visible = lbl_P2_Up.Visible = lbl_P2_Down.Visible = true;
                        this.lbl_P3N.Visible = this.P3.Visible = this.lbl_P3.Visible = lbl_P3_Up.Visible = lbl_P3_Down.Visible = true;
                        this.lbl_P4N.Visible = this.P4.Visible = this.lbl_P4.Visible = lbl_P4_Up.Visible = lbl_P4_Down.Visible = true;
                        this.lbl_P5N.Visible = this.P5.Visible = this.lbl_P5.Visible = lbl_P5_Up.Visible = lbl_P5_Down.Visible = true;
                        this.lbl_P6N.Visible = this.P6.Visible = this.lbl_P6.Visible = lbl_P6_Up.Visible = lbl_P6_Down.Visible = true;
                        this.lbl_P7N.Visible = this.P7.Visible = this.lbl_P7.Visible = lbl_P7_Up.Visible = lbl_P7_Down.Visible = true;
                        this.lbl_P8N.Visible = this.P8.Visible = this.lbl_P8.Visible = lbl_P8_Up.Visible = lbl_P8_Down.Visible = true;

                        break;
                    case 12:
                        this.lbl_P1N.Visible = this.P1.Visible = this.lbl_P1.Visible = lbl_P1_Up.Visible = lbl_P1_Down.Visible = true;
                        this.lbl_P2N.Visible = this.P2.Visible = this.lbl_P2.Visible = lbl_P2_Up.Visible = lbl_P2_Down.Visible = true;
                        this.lbl_P3N.Visible = this.P3.Visible = this.lbl_P3.Visible = lbl_P3_Up.Visible = lbl_P3_Down.Visible = true;
                        this.lbl_P4N.Visible = this.P4.Visible = this.lbl_P4.Visible = lbl_P4_Up.Visible = lbl_P4_Down.Visible = true;
                        this.lbl_P5N.Visible = this.P5.Visible = this.lbl_P5.Visible = lbl_P5_Up.Visible = lbl_P5_Down.Visible = true;
                        this.lbl_P6N.Visible = this.P6.Visible = this.lbl_P6.Visible = lbl_P6_Up.Visible = lbl_P6_Down.Visible = true;
                        this.lbl_P7N.Visible = this.P7.Visible = this.lbl_P7.Visible = lbl_P7_Up.Visible = lbl_P7_Down.Visible = true;
                        this.lbl_P8N.Visible = this.P8.Visible = this.lbl_P8.Visible = lbl_P8_Up.Visible = lbl_P8_Down.Visible = true;
                        //this.lbl_P9N.Visible=this.P9.Visible = this.lbl_P9.Visible = lbl_P9_Up.Visible = lbl_P9_Down.Visible = true;
                        //this.lbl_P10N.Visible=this.P10.Visible = this.lbl_P10.Visible = lbl_P10_Up.Visible = lbl_P10_Down.Visible = true;
                        //this.lbl_P11N.Visible=this.P11.Visible = this.lbl_P11.Visible = lbl_P11_Up.Visible = lbl_P11_Down.Visible = true;
                        //this.lbl_P12N.Visible=this.P12.Visible = this.lbl_P12.Visible = lbl_P12_Up.Visible = lbl_P12_Down.Visible = true;
                        break;
                    default:

                        break;
                }
            }
        }

        private Color backColor = Color.Black;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("字体色")]
        public Color BackForeColor
        {
            get { return backColor; }
            set
            {
                lbl1.ForeColor = lbl2.ForeColor = lbl3.ForeColor = lbl4.ForeColor = lbl5.ForeColor = lbl6.ForeColor = this.lbl222.ForeColor = this.lbl333.ForeColor = this.lbl_P1.ForeColor =
                              this.lbl_P2.ForeColor = this.lbl_P3.ForeColor = this.lbl_P4.ForeColor = this.lbl_P5.ForeColor = this.lbl_P6.ForeColor = this.lbl_P7.ForeColor = this.lbl_P8.ForeColor =
                   this.P1.ForeColor = this.P2.ForeColor = this.P3.ForeColor = this.P4.ForeColor = this.P5.ForeColor = this.P6.ForeColor = this.P7.ForeColor = this.P8.ForeColor =
                   this.lbl_P1N.ForeColor = this.lbl_P2N.ForeColor = this.lbl_P3N.ForeColor = this.lbl_P4N.ForeColor = this.lbl_P5N.ForeColor = this.lbl_P6N.ForeColor = this.lbl_P7N.ForeColor = this.lbl_P8N.ForeColor =
                   this.lbl_P1_Up.ForeColor = this.lbl_P2_Up.ForeColor = this.lbl_P3_Up.ForeColor = this.lbl_P4_Up.ForeColor = this.lbl_P5_Up.ForeColor = this.lbl_P6_Up.ForeColor = this.lbl_P7_Up.ForeColor = this.lbl_P8_Up.ForeColor =
                   this.lbl_P1_Down.ForeColor = this.lbl_P2_Down.ForeColor = this.lbl_P3_Down.ForeColor = this.lbl_P4_Down.ForeColor = this.lbl_P5_Down.ForeColor = this.lbl_P6_Down.ForeColor = this.lbl_P7_Down.ForeColor = this.lbl_P8_Down.ForeColor
                        = backColor = value;

            }
        }

        private string label1text = "二维码";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("label名称")]
        public string Label1Text
        {
            get { return label1text; }
            set
            {
                this.label1.Text = label1text = value;

            }
        }
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
                this.P1.Text = p1text = value;

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
                this.P2.Text = p2text = value;

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
                this.P3.Text = p3text = value;

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
                this.P4.Text = p4text = value;

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
                this.P5.Text = p5text = value;

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
                this.P6.Text = p6text = value;

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
                this.P7.Text = p7text = value;

            }
        }
        private string p8text = "Pin8";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P7名称")]
        public string P8Text
        {
            get { return p8text; }
            set
            {
                this.P8.Text = p8text = value;

            }
        }

        private string lbl5text = "上限值mm";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P12名称")]
        public string LblUPtext
        {
            get { return lbl5text; }
            set
            {
                this.lbl5.Text = lbl5text = value;

            }
        }
        private string lbl6text = "下限值mm";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P12名称")]
        public string LblDowntext
        {
            get { return lbl6text; }
            set
            {
                this.lbl6.Text = lbl6text = value;

            }
        }

        private string lbl7text = "理论值mm";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("理论值名称")]
        public string Lbllilun
        {
            get { return lbl7text; }
            set
            {
                this.lbl3.Text = lbl7text = value;

            }
        }

        private string lbl8text = "实测值mm";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("实测值名称")]
        public string Lblshice
        {
            get { return lbl8text; }
            set
            {
                this.lbl4.Text = lbl8text = value;

            }
        }





        #endregion

       
    }
}
