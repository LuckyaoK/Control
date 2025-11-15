using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CXPro001.controls
{
    /// <summary>
    /// 实时显示 当前班别的生产数量
    /// </summary>
    public partial class ProCountDispl : UserControl
    {
        public ProCountDispl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 显示产品型号
        /// </summary>
        /// <param name="names"></param>
        public void ShowTypeName(string names)
        {
            labtype.Text = names;
            int aa = DateTime.Now.Hour;
            if (aa < 20 && aa > 7) label3.Text = "白班";
            else label3.Text = "夜班";
        }
        public void ShowBanBie(List<int> aa1)
        {
            BeginInvoke(new Action(() => {
                int aa = DateTime.Now.Hour;
                if (aa < 20 && aa > 7) label3.Text = "白班";
                else label3.Text = "夜班";

                STOK1.Text = aa1[0].ToString();
                STOK2.Text = aa1[1].ToString();
                STOK3.Text = aa1[2].ToString();
                STOK4.Text = aa1[3].ToString();
                STOK5.Text = aa1[4].ToString();
                STOK6.Text = aa1[5].ToString();
                STOK7.Text = aa1[6].ToString();
                STOK8.Text = aa1[7].ToString();
                STOK9.Text = aa1[8].ToString();
                STOK100.Text = aa1[9].ToString();

                STNG1.Text = aa1[10].ToString();
                STNG2.Text = aa1[11].ToString();
                STNG3.Text = aa1[12].ToString();
                STNG4.Text = aa1[13].ToString();
                STNG5.Text = aa1[14].ToString();
                STNG6.Text = aa1[15].ToString();
                STNG7.Text = aa1[16].ToString();
                STNG8.Text = aa1[17].ToString();
                STNG9.Text = aa1[18].ToString();
                STNG100.Text = aa1[19].ToString();

                STyield1.Text = (100*Convert.ToDouble(STOK1.Text) / (Convert.ToDouble(STOK1.Text) + Convert.ToDouble(STNG1.Text))).ToString("F2") + "%";
                STyield2.Text = (100 * Convert.ToDouble(STOK2.Text) / (Convert.ToDouble(STOK2.Text) + Convert.ToDouble(STNG2.Text))).ToString("F2") + "%";
                STyield3.Text = (100 * Convert.ToDouble(STOK3.Text) / (Convert.ToDouble(STOK3.Text) + Convert.ToDouble(STNG3.Text))).ToString("F2") + "%";
                STyield4.Text = (100 * Convert.ToDouble(STOK4.Text) / (Convert.ToDouble(STOK4.Text) + Convert.ToDouble(STNG4.Text))).ToString("F2") + "%";
                STyield5.Text = (100 * Convert.ToDouble(STOK5.Text) / (Convert.ToDouble(STOK5.Text) + Convert.ToDouble(STNG5.Text))).ToString("F2") + "%";
                STyield6.Text = (100 * Convert.ToDouble(STOK6.Text) / (Convert.ToDouble(STOK6.Text) + Convert.ToDouble(STNG6.Text))).ToString("F2") + "%";
                STyield7.Text = (100 * Convert.ToDouble(STOK7.Text) / (Convert.ToDouble(STOK7.Text) + Convert.ToDouble(STNG7.Text))).ToString("F2") + "%";
                STyield8.Text = (100 * Convert.ToDouble(STOK8.Text) / (Convert.ToDouble(STOK8.Text) + Convert.ToDouble(STNG8.Text))).ToString("F2") + "%";
                STyield9.Text = (100 * Convert.ToDouble(STOK9.Text) / (Convert.ToDouble(STOK9.Text) + Convert.ToDouble(STNG9.Text))).ToString("F2") + "%";
                STyield100.Text = (100 * Convert.ToDouble(STOK100.Text) / (Convert.ToDouble(STOK100.Text) + Convert.ToDouble(STNG100.Text))).ToString("F2") + "%";

            }));
        }
      



        #region 自定义属性
        private int cos = 10;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("测试PIN脚数量")]
        public int PinCount
        {
            get { return cos; }
            set
            {
                if (value > 10 || value < 2) return;               
                cos = value;
                this.tableLayoutPanel1.RowCount = cos + 2;
                switch (cos)
                {
                    case 2:
                        STname1.Visible = STOK1.Visible = STNG1.Visible = STyield1.Visible = true;
                        STname2.Visible = STOK2.Visible = STNG2.Visible = STyield2.Visible = true;
                        STname3.Visible = STOK3.Visible = STNG3.Visible = STyield3.Visible = false;
                        STname4.Visible = STOK4.Visible = STNG4.Visible = STyield4.Visible = false;
                        STname5.Visible = STOK5.Visible = STNG5.Visible = STyield5.Visible = false;
                        STname6.Visible = STOK6.Visible = STNG6.Visible = STyield6.Visible = false;
                        STname7.Visible = STOK7.Visible = STNG7.Visible = STyield7.Visible = false;
                        STname8.Visible = STOK8.Visible = STNG8.Visible = STyield8.Visible = false;
                        STname9.Visible = STOK9.Visible = STNG9.Visible = STyield9.Visible = false;
                        STname100.Visible = STOK100.Visible = STNG100.Visible = STyield100.Visible = false;
                        break;
                    case 3:
                        STname1.Visible = STOK1.Visible = STNG1.Visible = STyield1.Visible = true;
                        STname2.Visible = STOK2.Visible = STNG2.Visible = STyield2.Visible = true;
                        STname3.Visible = STOK3.Visible = STNG3.Visible = STyield3.Visible = true;
                        STname4.Visible = STOK4.Visible = STNG4.Visible = STyield4.Visible = false;
                        STname5.Visible = STOK5.Visible = STNG5.Visible = STyield5.Visible = false;
                        STname6.Visible = STOK6.Visible = STNG6.Visible = STyield6.Visible = false;
                        STname7.Visible = STOK7.Visible = STNG7.Visible = STyield7.Visible = false;
                        STname8.Visible = STOK8.Visible = STNG8.Visible = STyield8.Visible = false;
                        STname9.Visible = STOK9.Visible = STNG9.Visible = STyield9.Visible = false;
                        STname100.Visible = STOK100.Visible = STNG100.Visible = STyield100.Visible = false;
                        break;
                    case 4:
                        STname1.Visible = STOK1.Visible = STNG1.Visible = STyield1.Visible = true;
                        STname2.Visible = STOK2.Visible = STNG2.Visible = STyield2.Visible = true;
                        STname3.Visible = STOK3.Visible = STNG3.Visible = STyield3.Visible = true;
                        STname4.Visible = STOK4.Visible = STNG4.Visible = STyield4.Visible = true;
                        STname5.Visible = STOK5.Visible = STNG5.Visible = STyield5.Visible = false;
                        STname6.Visible = STOK6.Visible = STNG6.Visible = STyield6.Visible = false;
                        STname7.Visible = STOK7.Visible = STNG7.Visible = STyield7.Visible = false;
                        STname8.Visible = STOK8.Visible = STNG8.Visible = STyield8.Visible = false;
                        STname9.Visible = STOK9.Visible = STNG9.Visible = STyield9.Visible = false;
                        STname100.Visible = STOK100.Visible = STNG100.Visible = STyield100.Visible = false;
                        break;
                    case 5:
                        STname1.Visible = STOK1.Visible = STNG1.Visible = STyield1.Visible = true;
                        STname2.Visible = STOK2.Visible = STNG2.Visible = STyield2.Visible = true;
                        STname3.Visible = STOK3.Visible = STNG3.Visible = STyield3.Visible = true;
                        STname4.Visible = STOK4.Visible = STNG4.Visible = STyield4.Visible = true;
                        STname5.Visible = STOK5.Visible = STNG5.Visible = STyield5.Visible = true;
                        STname6.Visible = STOK6.Visible = STNG6.Visible = STyield6.Visible = false;
                        STname7.Visible = STOK7.Visible = STNG7.Visible = STyield7.Visible = false;
                        STname8.Visible = STOK8.Visible = STNG8.Visible = STyield8.Visible = false;
                        STname9.Visible = STOK9.Visible = STNG9.Visible = STyield9.Visible = false;
                        STname100.Visible = STOK100.Visible = STNG100.Visible = STyield100.Visible = false;
                        break;
                    case 6:
                        STname1.Visible = STOK1.Visible = STNG1.Visible = STyield1.Visible = true;
                        STname2.Visible = STOK2.Visible = STNG2.Visible = STyield2.Visible = true;
                        STname3.Visible = STOK3.Visible = STNG3.Visible = STyield3.Visible = true;
                        STname4.Visible = STOK4.Visible = STNG4.Visible = STyield4.Visible = true;
                        STname5.Visible = STOK5.Visible = STNG5.Visible = STyield5.Visible = true;
                        STname6.Visible = STOK6.Visible = STNG6.Visible = STyield6.Visible = true;
                        STname7.Visible = STOK7.Visible = STNG7.Visible = STyield7.Visible = false;
                        STname8.Visible = STOK8.Visible = STNG8.Visible = STyield8.Visible = false;
                        STname9.Visible = STOK9.Visible = STNG9.Visible = STyield9.Visible = false;
                        STname100.Visible = STOK100.Visible = STNG100.Visible = STyield100.Visible = false;
                        break;
                    case 7:
                        STname1.Visible = STOK1.Visible = STNG1.Visible = STyield1.Visible = true;
                        STname2.Visible = STOK2.Visible = STNG2.Visible = STyield2.Visible = true;
                        STname3.Visible = STOK3.Visible = STNG3.Visible = STyield3.Visible = true;
                        STname4.Visible = STOK4.Visible = STNG4.Visible = STyield4.Visible = true;
                        STname5.Visible = STOK5.Visible = STNG5.Visible = STyield5.Visible = true;
                        STname6.Visible = STOK6.Visible = STNG6.Visible = STyield6.Visible = true;
                        STname7.Visible = STOK7.Visible = STNG7.Visible = STyield7.Visible = true;
                        STname8.Visible = STOK8.Visible = STNG8.Visible = STyield8.Visible = false;
                        STname9.Visible = STOK9.Visible = STNG9.Visible = STyield9.Visible = false;
                        STname100.Visible = STOK100.Visible = STNG100.Visible = STyield100.Visible = false;
                        break;
                    case 8:
                        STname1.Visible = STOK1.Visible = STNG1.Visible = STyield1.Visible = true;
                        STname2.Visible = STOK2.Visible = STNG2.Visible = STyield2.Visible = true;
                        STname3.Visible = STOK3.Visible = STNG3.Visible = STyield3.Visible = true;
                        STname4.Visible = STOK4.Visible = STNG4.Visible = STyield4.Visible = true;
                        STname5.Visible = STOK5.Visible = STNG5.Visible = STyield5.Visible = true;
                        STname6.Visible = STOK6.Visible = STNG6.Visible = STyield6.Visible = true;
                        STname7.Visible = STOK7.Visible = STNG7.Visible = STyield7.Visible = true;
                        STname8.Visible = STOK8.Visible = STNG8.Visible = STyield8.Visible = true;
                        STname9.Visible = STOK9.Visible = STNG9.Visible = STyield9.Visible = false;
                        STname100.Visible = STOK100.Visible = STNG100.Visible = STyield100.Visible = false;
                        break;
                    case 9:
                        STname1.Visible = STOK1.Visible = STNG1.Visible = STyield1.Visible = true;
                        STname2.Visible = STOK2.Visible = STNG2.Visible = STyield2.Visible = true;
                        STname3.Visible = STOK3.Visible = STNG3.Visible = STyield3.Visible = true;
                        STname4.Visible = STOK4.Visible = STNG4.Visible = STyield4.Visible = true;
                        STname5.Visible = STOK5.Visible = STNG5.Visible = STyield5.Visible = true;
                        STname6.Visible = STOK6.Visible = STNG6.Visible = STyield6.Visible = true;
                        STname7.Visible = STOK7.Visible = STNG7.Visible = STyield7.Visible = true;
                        STname8.Visible = STOK8.Visible = STNG8.Visible = STyield8.Visible = true;
                        STname9.Visible = STOK9.Visible = STNG9.Visible = STyield9.Visible = true;
                        STname100.Visible = STOK100.Visible = STNG100.Visible = STyield100.Visible = false;
                        break;
                    case 10:
                        STname1.Visible = STOK1.Visible = STNG1.Visible = STyield1.Visible = true;
                        STname2.Visible = STOK2.Visible = STNG2.Visible = STyield2.Visible = true;
                        STname3.Visible = STOK3.Visible = STNG3.Visible = STyield3.Visible = true;
                        STname4.Visible = STOK4.Visible = STNG4.Visible = STyield4.Visible = true;
                        STname5.Visible = STOK5.Visible = STNG5.Visible = STyield5.Visible = true;
                        STname6.Visible = STOK6.Visible = STNG6.Visible = STyield6.Visible = true;
                        STname7.Visible = STOK7.Visible = STNG7.Visible = STyield7.Visible = true;
                        STname8.Visible = STOK8.Visible = STNG8.Visible = STyield8.Visible = true;
                        STname9.Visible = STOK9.Visible = STNG9.Visible = STyield9.Visible = true;
                        STname100.Visible = STOK100.Visible = STNG100.Visible = STyield100.Visible = true;
                        break;
                    default:

                        break;

                }
            }
        }

        private string p1text = "Pin1";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P1名称")]
        public string P1Text
        {
            get { return p1text; }
            set { this.STname1.Text = p1text = value; }             
        }

        private string p2text = "Pin2";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P2名称")]
        public string P2Text
        {
            get { return p2text; }
            set { this.STname2.Text = p2text = value; }
        }
        private string p3text = "Pin3";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P3名称")]
        public string P3Text
        {
            get { return p3text; }
            set { this.STname3.Text = p3text = value; }
        }
        private string p4text = "Pin4";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P4名称")]
        public string P4Text
        {
            get { return p4text; }
            set { this.STname4.Text = p4text = value; }
        }
        private string p5text = "Pin5";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P5名称")]
        public string P5Text
        {
            get { return p5text; }
            set { this.STname5.Text = p5text = value; }
        }
        private string p6text = "Pin6";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P6名称")]
        public string P6Text
        {
            get { return p6text; }
            set { this.STname6.Text = p6text = value; }

        }
        private string p7text = "Pin7";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P7名称")]
        public string P7Text
        {
            get { return p7text; }
            set { this.STname7.Text = p7text = value; }
        }
        private string p8text = "Pin8";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("ST8名称")]
        public string P8Text
        {
            get { return p8text; }
            set { this.STname8.Text = p8text = value; }
        }

        private string p9text = "Pin9";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("ST9名称")]
        public string P9text
        {
            get { return p9text; }
            set { this.STname9.Text = p9text = value; }
        }
        private string p100text = "总计";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("ST100名称")]
        public string P100text
        {
            get { return p100text; }
            set { this.STname100.Text = p100text = value; }
        }
        #endregion   
    }
}
