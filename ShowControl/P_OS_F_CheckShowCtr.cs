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
using MyLib.Sys;
using MyLib.Input;

namespace CXPro001.ShowControl
{
   
    /// <summary>
    /// 极性 开口 异物检测
    /// </summary>
    public partial class P_OS_F_CheckShowCtr : UserControl
    {
        OpenSet openSet2 = null;
        public P_OS_F_CheckShowCtr()
        {
            InitializeComponent();
        }
        #region 当显示控件用时
        /// <summary>
        /// 清除所有结果显示
        /// </summary>
        /// <param name="cord">二维码</param>
        public void ClearAll(string cord)
        {
            Invoke(new Action(() => {
                label2.Text = label6.Text = label10.Text = label14.Text = label16.Text = "-";
            PinRes1.Text = PinRes2.Text = PinRes3.Text = PinRes4.Text = PinRes5.Text = "-";
            label2.BackColor = label6.BackColor = label10.BackColor = label14.BackColor = label16.BackColor = Color.White;
            PinRes1.BackColor = PinRes2.BackColor = PinRes3.BackColor = PinRes4.BackColor = PinRes5.BackColor = Color.White;
            label2.Text = cord;
            }));
        }
        /// <summary>
        /// 显示极性检测结果
        /// </summary>
        /// <param name="res"></param>
        public void ShowJiXing(string res)
        {
            Invoke(new Action(() => {
                label6.Text = res;
                if (!res.Contains("OK")) label6.BackColor = Color.Red;
                else label6.BackColor = Color.LimeGreen;
            }));          
        }
        /// <summary>
        /// 显示异物检测结果
        /// </summary>
        /// <param name="res"></param>
        public void ShowYiWu(string res)
        {
            Invoke(new Action(() => {
              
            }));
        }

        /// <summary>
        /// 显示开口检测结果
        /// </summary>
        /// <param name="res"></param>
        public void ShowOpen(OpenSet openSet)
        {
            Invoke(new Action(() => {
                label2.Text = openSet.Cords;
                if (!openSet.Sheild) 
                {
                    label10.Text = openSet.ForeignRes ? "OK" : "NG";
                    label10.BackColor = openSet.ForeignRes ? Color.LimeGreen : Color.Red;
                    
                    PinRes1.Text = openSet.RealP1.ToString();
                    PinRes2.Text = openSet.RealP2.ToString();
                    PinRes3.Text = openSet.RealP3.ToString();
                    PinRes4.Text = openSet.RealP4.ToString();
                    PinRes5.Text = openSet.RealP5.ToString();
                    PinRes1.BackColor = openSet.ResP1 ? Color.LimeGreen : Color.Red;
                    PinRes2.BackColor = openSet.ResP2 ? Color.LimeGreen : Color.Red;
                    PinRes3.BackColor = openSet.ResP3 ? Color.LimeGreen : Color.Red;
                    PinRes4.BackColor = openSet.ResP4 ? Color.LimeGreen : Color.Red;
                    PinRes5.BackColor = openSet.ResP5 ? Color.LimeGreen : Color.Red;
                    label16.Text = openSet.ResEnd ? "OK" : "NG";
                    label16.BackColor = openSet.ResEnd ? Color.LimeGreen : Color.Red;
                }
                else
                {
                    PinRes1.Text = openSet.RealP1.ToString();
                    PinRes2.Text = openSet.RealP2.ToString();
                    PinRes3.Text = openSet.RealP3.ToString();
                    PinRes4.Text = openSet.RealP4.ToString();
                    PinRes5.Text = openSet.RealP5.ToString();
                    PinRes1.BackColor = PinRes2.BackColor = PinRes3.BackColor = PinRes4.BackColor = PinRes5.BackColor = Color.Yellow;


                    label10.Text = label16.Text = "屏蔽中";
                    label10.BackColor = label16.BackColor = Color.Yellow;
                }
               

            }));
        }

        /// <summary>
        /// 显示开口度上下限
        /// </summary>
        /// <param name="openSet1"></param>
        public void ShowLoad(OpenSet openSet1)
        {
            PinUP1.Text = openSet1.UP_P1.ToString();
            PinUP2.Text = openSet1.UP_P2.ToString();
            PinUP3.Text = openSet1.UP_P3.ToString();
            PinUP4.Text = openSet1.UP_P4.ToString();
            PinUP5.Text = openSet1.UP_P5.ToString();
            PinDW1.Text = openSet1.DW_P1.ToString();
            PinDW2.Text = openSet1.DW_P2.ToString();
            PinDW3.Text = openSet1.DW_P3.ToString();
            PinDW4.Text = openSet1.DW_P4.ToString();
            PinDW5.Text = openSet1.DW_P5.ToString();
        }
        #endregion
        #region 当设置参数控件用时
        /// <summary>
        /// 初始化--设置时
        /// </summary>
        /// <param name="openSet"></param>
        public void init(OpenSet openSet)
        {
            label14.Text = SysStatus.CurProductName;
            openSet2 = openSet;
        }
        
        /// <summary>
        /// 加载参数
        /// </summary>
        public void LoadSet()
        {
            openSet2.LoadParm(label14.Text);
            ShowLoad(openSet2);
        }
        /// <summary>
        /// 保存参数
        /// </summary>
        public void SaveSet()
        {
            try
            {
                openSet2.UP_P1 = Convert.ToDouble(PinUP1.Text);
                openSet2.UP_P2 = Convert.ToDouble(PinUP2.Text);
                openSet2.UP_P3 = Convert.ToDouble(PinUP3.Text);
                openSet2.UP_P4 = Convert.ToDouble(PinUP4.Text);
                openSet2.UP_P5 = Convert.ToDouble(PinUP5.Text);
                openSet2.DW_P1 = Convert.ToDouble(PinDW1.Text);
                openSet2.DW_P2 = Convert.ToDouble(PinDW2.Text);
                openSet2.DW_P3 = Convert.ToDouble(PinDW3.Text);
                openSet2.DW_P4 = Convert.ToDouble(PinDW4.Text);
                openSet2.DW_P5 = Convert.ToDouble(PinDW5.Text);
                openSet2.SaveParm(label14.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           

        }
        
        private void PinUP1_DoubleClick(object sender, EventArgs e)
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
        private int pinCount = 5;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("测试PIN脚数量")]
        public int PinCount
        {
            get
            {
                pinCount = tableLayoutPanel1.RowCount - 5;
                return pinCount;
            }
            set
            {
                if (value > 5 || value < 1)
                {
                    return;
                }
                pinCount = value;
                this.tableLayoutPanel1.RowCount = pinCount + 5;
                switch (pinCount)
                {

                    case 1:
                        Pin1.Visible = PinRes1.Visible = PinUP1.Visible = PinDW1.Visible = true;
                        Pin2.Visible = PinRes2.Visible = PinUP2.Visible = PinDW2.Visible = false;
                        Pin3.Visible = PinRes3.Visible = PinUP3.Visible = PinDW3.Visible = false;
                        Pin4.Visible = PinRes4.Visible = PinUP4.Visible = PinDW4.Visible = false;
                        Pin5.Visible = PinRes5.Visible = PinUP5.Visible = PinDW5.Visible = false;

                        break;
                    case 2:
                        Pin1.Visible = PinRes1.Visible = PinUP1.Visible = PinDW1.Visible = true;
                        Pin2.Visible = PinRes2.Visible = PinUP2.Visible = PinDW2.Visible = true;
                        Pin3.Visible = PinRes3.Visible = PinUP3.Visible = PinDW3.Visible = false;
                        Pin4.Visible = PinRes4.Visible = PinUP4.Visible = PinDW4.Visible = false;
                        Pin5.Visible = PinRes5.Visible = PinUP5.Visible = PinDW5.Visible = false;
                        break;
                    case 3:
                        Pin1.Visible = PinRes1.Visible = PinUP1.Visible = PinDW1.Visible = true;
                        Pin2.Visible = PinRes2.Visible = PinUP2.Visible = PinDW2.Visible = true;
                        Pin3.Visible = PinRes3.Visible = PinUP3.Visible = PinDW3.Visible = true;
                        Pin4.Visible = PinRes4.Visible = PinUP4.Visible = PinDW4.Visible = false;
                        Pin5.Visible = PinRes5.Visible = PinUP5.Visible = PinDW5.Visible = false;
                        break;
                    case 4:
                        Pin1.Visible = PinRes1.Visible = PinUP1.Visible = PinDW1.Visible = true;
                        Pin2.Visible = PinRes2.Visible = PinUP2.Visible = PinDW2.Visible = true;
                        Pin3.Visible = PinRes3.Visible = PinUP3.Visible = PinDW3.Visible = true;
                        Pin4.Visible = PinRes4.Visible = PinUP4.Visible = PinDW4.Visible = true;
                        Pin5.Visible = PinRes5.Visible = PinUP5.Visible = PinDW5.Visible = false;
                        break;
                    case 5:
                        Pin1.Visible = PinRes1.Visible = PinUP1.Visible = PinDW1.Visible = true;
                        Pin2.Visible = PinRes2.Visible = PinUP2.Visible = PinDW2.Visible = true;
                        Pin3.Visible = PinRes3.Visible = PinUP3.Visible = PinDW3.Visible = true;
                        Pin4.Visible = PinRes4.Visible = PinUP4.Visible = PinDW4.Visible = true;
                        Pin5.Visible = PinRes5.Visible = PinUP5.Visible = PinDW5.Visible = true;
                        break;
                    default:

                        break;
                }
            }
        }

        private string UPNAME = "上限";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("上限叫法")]
        public string Upname
        {
            get
            {
                return UPNAME;
            }
            set
            {
                label19.Text = UPNAME = value;
            }

        }
        private string DWNAME = "下限";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("下限叫法")]
        public string DWname
        {
            get
            {
                return DWNAME;
            }
            set
            {
                label20.Text = DWNAME = value;
            }

        }







        #endregion

       
    }
}
