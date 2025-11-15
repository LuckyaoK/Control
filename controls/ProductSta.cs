using MyLib.Files;
using MyLib.Param;

using MyLib.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CXPro001.myclass;
using MyLib.SqlHelper;
namespace CXPro001.controls
{
    public partial class ProductSta : UserControl
    {
        public ProductSta()
        {
            InitializeComponent();
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
                if (value > 10 || value < 2)
                {
                    return;
                }
                cos = value;
                this.tableLayoutPanel1.RowCount = cos + 3;
                switch (cos)
                {

                    case 2:
                        ST1.Visible = ST1OK.Visible = ST1NG.Visible = ST1Lv.Visible = true;
                        ST2.Visible = ST2OK.Visible = ST2NG.Visible = ST2Lv.Visible = true;
                        ST2.Visible = ST3OK.Visible = ST3NG.Visible = ST3Lv.Visible = false;
                        ST4.Visible = ST4OK.Visible = ST4NG.Visible = ST4Lv.Visible = false;
                        ST5.Visible = ST5OK.Visible = ST5NG.Visible = ST5Lv.Visible = false;
                        ST6.Visible = ST6OK.Visible = ST6NG.Visible = ST6Lv.Visible = false;
                        ST7.Visible = ST7OK.Visible = ST7NG.Visible = ST7Lv.Visible = false;
                        ST8.Visible = ST8OK.Visible = ST8NG.Visible = ST8Lv.Visible = false;
                        ST9.Visible = ST9OK.Visible = ST9NG.Visible = ST9Lv.Visible = false;
                        ST100.Visible = ST100OK.Visible = ST100NG.Visible = ST100Lv.Visible = false;
                        break;
                    case 3:
                        ST1.Visible = ST1OK.Visible = ST1NG.Visible = ST1Lv.Visible = true;
                        ST2.Visible = ST2OK.Visible = ST2NG.Visible = ST2Lv.Visible = true;
                        ST2.Visible = ST3OK.Visible = ST3NG.Visible = ST3Lv.Visible = true;
                        ST4.Visible = ST4OK.Visible = ST4NG.Visible = ST4Lv.Visible = false;
                        ST5.Visible = ST5OK.Visible = ST5NG.Visible = ST5Lv.Visible = false;
                        ST6.Visible = ST6OK.Visible = ST6NG.Visible = ST6Lv.Visible = false;
                        ST7.Visible = ST7OK.Visible = ST7NG.Visible = ST7Lv.Visible = false;
                        ST8.Visible = ST8OK.Visible = ST8NG.Visible = ST8Lv.Visible = false;
                        ST9.Visible = ST9OK.Visible = ST9NG.Visible = ST9Lv.Visible = false;
                        ST100.Visible = ST100OK.Visible = ST100NG.Visible = ST100Lv.Visible = false;
                        break;
                    case 4:
                        ST1.Visible = ST1OK.Visible = ST1NG.Visible = ST1Lv.Visible = true;
                        ST2.Visible = ST2OK.Visible = ST2NG.Visible = ST2Lv.Visible = true;
                        ST2.Visible = ST3OK.Visible = ST3NG.Visible = ST3Lv.Visible = true;
                        ST4.Visible = ST4OK.Visible = ST4NG.Visible = ST4Lv.Visible = true;
                        ST5.Visible = ST5OK.Visible = ST5NG.Visible = ST5Lv.Visible = false;
                        ST6.Visible = ST6OK.Visible = ST6NG.Visible = ST6Lv.Visible = false;
                        ST7.Visible = ST7OK.Visible = ST7NG.Visible = ST7Lv.Visible = false;
                        ST8.Visible = ST8OK.Visible = ST8NG.Visible = ST8Lv.Visible = false;
                        ST9.Visible = ST9OK.Visible = ST9NG.Visible = ST9Lv.Visible = false;
                        ST100.Visible = ST100OK.Visible = ST100NG.Visible = ST100Lv.Visible = false;
                        break;
                    case 5:
                        ST1.Visible = ST1OK.Visible = ST1NG.Visible = ST1Lv.Visible = true;
                        ST2.Visible = ST2OK.Visible = ST2NG.Visible = ST2Lv.Visible = true;
                        ST2.Visible = ST3OK.Visible = ST3NG.Visible = ST3Lv.Visible = true;
                        ST4.Visible = ST4OK.Visible = ST4NG.Visible = ST4Lv.Visible = true;
                        ST5.Visible = ST5OK.Visible = ST5NG.Visible = ST5Lv.Visible = true;
                        ST6.Visible = ST6OK.Visible = ST6NG.Visible = ST6Lv.Visible = false;
                        ST7.Visible = ST7OK.Visible = ST7NG.Visible = ST7Lv.Visible = false;
                        ST8.Visible = ST8OK.Visible = ST8NG.Visible = ST8Lv.Visible = false;
                        ST9.Visible = ST9OK.Visible = ST9NG.Visible = ST9Lv.Visible = false;
                        ST100.Visible = ST100OK.Visible = ST100NG.Visible = ST100Lv.Visible = false;
                        break;
                    case 6:
                        ST1.Visible = ST1OK.Visible = ST1NG.Visible = ST1Lv.Visible = true;
                        ST2.Visible = ST2OK.Visible = ST2NG.Visible = ST2Lv.Visible = true;
                        ST2.Visible = ST3OK.Visible = ST3NG.Visible = ST3Lv.Visible = true;
                        ST4.Visible = ST4OK.Visible = ST4NG.Visible = ST4Lv.Visible = true;
                        ST5.Visible = ST5OK.Visible = ST5NG.Visible = ST5Lv.Visible = true;
                        ST6.Visible = ST6OK.Visible = ST6NG.Visible = ST6Lv.Visible = true;
                        ST7.Visible = ST7OK.Visible = ST7NG.Visible = ST7Lv.Visible = false;
                        ST8.Visible = ST8OK.Visible = ST8NG.Visible = ST8Lv.Visible = false;
                        ST9.Visible = ST9OK.Visible = ST9NG.Visible = ST9Lv.Visible = false;
                        ST100.Visible = ST100OK.Visible = ST100NG.Visible = ST100Lv.Visible = false;
                        break;
                    case 7:
                        ST1.Visible = ST1OK.Visible = ST1NG.Visible = ST1Lv.Visible = true;
                        ST2.Visible = ST2OK.Visible = ST2NG.Visible = ST2Lv.Visible = true;
                        ST2.Visible = ST3OK.Visible = ST3NG.Visible = ST3Lv.Visible = true;
                        ST4.Visible = ST4OK.Visible = ST4NG.Visible = ST4Lv.Visible = true;
                        ST5.Visible = ST5OK.Visible = ST5NG.Visible = ST5Lv.Visible = true;
                        ST6.Visible = ST6OK.Visible = ST6NG.Visible = ST6Lv.Visible = true;
                        ST7.Visible = ST7OK.Visible = ST7NG.Visible = ST7Lv.Visible = true;
                        ST8.Visible = ST8OK.Visible = ST8NG.Visible = ST8Lv.Visible = false;
                        ST9.Visible = ST9OK.Visible = ST9NG.Visible = ST9Lv.Visible = false;

                        ST9.Visible = ST9OK.Visible = ST9NG.Visible = ST9Lv.Visible = false;
                        ST100.Visible = ST100OK.Visible = ST100NG.Visible = ST100Lv.Visible = false;
                        break;
                    case 8:
                        ST1.Visible = ST1OK.Visible = ST1NG.Visible = ST1Lv.Visible = true;
                        ST2.Visible = ST2OK.Visible = ST2NG.Visible = ST2Lv.Visible = true;
                        ST2.Visible = ST3OK.Visible = ST3NG.Visible = ST3Lv.Visible = true;
                        ST4.Visible = ST4OK.Visible = ST4NG.Visible = ST4Lv.Visible = true;
                        ST5.Visible = ST5OK.Visible = ST5NG.Visible = ST5Lv.Visible = true;
                        ST6.Visible = ST6OK.Visible = ST6NG.Visible = ST6Lv.Visible = true;
                        ST7.Visible = ST7OK.Visible = ST7NG.Visible = ST7Lv.Visible = true;
                        ST8.Visible = ST8OK.Visible = ST8NG.Visible = ST8Lv.Visible = true;
                        ST9.Visible = ST9OK.Visible = ST9NG.Visible = ST9Lv.Visible = false;
                        ST100.Visible = ST100OK.Visible = ST100NG.Visible = ST100Lv.Visible = false;
                        break;
                    case 9:
                        ST1.Visible = ST1OK.Visible = ST1NG.Visible = ST1Lv.Visible = true;
                        ST2.Visible = ST2OK.Visible = ST2NG.Visible = ST2Lv.Visible = true;
                        ST2.Visible = ST3OK.Visible = ST3NG.Visible = ST3Lv.Visible = true;
                        ST4.Visible = ST4OK.Visible = ST4NG.Visible = ST4Lv.Visible = true;
                        ST5.Visible = ST5OK.Visible = ST5NG.Visible = ST5Lv.Visible = true;
                        ST6.Visible = ST6OK.Visible = ST6NG.Visible = ST6Lv.Visible = true;
                        ST7.Visible = ST7OK.Visible = ST7NG.Visible = ST7Lv.Visible = true;
                        ST8.Visible = ST8OK.Visible = ST8NG.Visible = ST8Lv.Visible = true;
                        ST9.Visible = ST9OK.Visible = ST9NG.Visible = ST9Lv.Visible = true;
                        ST100.Visible = ST100OK.Visible = ST100NG.Visible = ST100Lv.Visible = false;
                        break;
                    case 10:
                        ST1.Visible = ST1OK.Visible = ST1NG.Visible = ST1Lv.Visible = true;
                        ST2.Visible = ST2OK.Visible = ST2NG.Visible = ST2Lv.Visible = true;
                        ST2.Visible = ST3OK.Visible = ST3NG.Visible = ST3Lv.Visible = true;
                        ST4.Visible = ST4OK.Visible = ST4NG.Visible = ST4Lv.Visible = true;
                        ST5.Visible = ST5OK.Visible = ST5NG.Visible = ST5Lv.Visible = true;
                        ST6.Visible = ST6OK.Visible = ST6NG.Visible = ST6Lv.Visible = true;
                        ST7.Visible = ST7OK.Visible = ST7NG.Visible = ST7Lv.Visible = true;
                        ST8.Visible = ST8OK.Visible = ST8NG.Visible = ST8Lv.Visible = true;
                        ST9.Visible = ST9OK.Visible = ST9NG.Visible = ST9Lv.Visible = true;
                        ST100.Visible = ST100OK.Visible = ST100NG.Visible = ST100Lv.Visible = true;
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
            set
            {
                this.ST1.Text = p1text = value;

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
                this.ST2.Text = p2text = value;

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
                this.ST3.Text = p3text = value;

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
                this.ST4.Text = p4text = value;

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
                this.ST5.Text = p5text = value;

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
                this.ST6.Text = p6text = value;

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
                this.ST7.Text = p7text = value;

            }
        }
        private string p8text = "Pin8";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("ST8名称")]
        public string P8Text
        {
            get { return p8text; }
            set
            {
                this.ST8.Text = p8text = value;

            }
        }

        private string p9text = "Pin9";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("ST9名称")]
        public string P9text
        {
            get { return p9text; }
            set
            {
                this.ST9.Text = p9text = value;

            }
        }
        private string p10text = "总计";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("ST100名称")]
        public string P100text
        {
            get { return p10text; }
            set
            {
                this.ST100.Text = p10text = value;

            }
        }
        #endregion   
        public void init()
        {
            Invoke(new Action(() => {

                ST1OK.Text = ST1NG.Text = ST1Lv.Text = ST2OK.Text = ST2NG.Text = ST2Lv.Text = ST3OK.Text = ST3NG.Text = "0";
                ST3Lv.Text = ST4OK.Text = ST4Lv.Text = ST5OK.Text = ST5Lv.Text = ST6OK.Text = ST6NG.Text = ST6Lv.Text = ST7OK.Text = ST7NG.Text = ST7Lv.Text = ST8OK.Text = "0";
                ST8NG.Text = ST8Lv.Text = ST9OK.Text = ST9NG.Text = ST9Lv.Text = ST100OK.Text = ST100NG.Text = ST100Lv.Text = "0";
                ST4NG.Text = ST5NG.Text = "0";
                comboBox1.Text = SysStatus.CurProductName;

            }));
        }
        private void ProductSta_Load(object sender, EventArgs e)
        {
            init();
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)//选定日期后触发
        {
            DateTime dt1 = (Convert.ToDateTime(dateTimePicker1.Text)).AddHours(8);
            DateTime dt2 = dt1.AddDays(1.0);
            if (comboBox2.Text == "全天")
            {
                dt1 = (Convert.ToDateTime(dateTimePicker1.Text)).AddHours(8);
                dt2 = dt1.AddDays(1.0);
            }
            else if (comboBox2.Text == "白班")
            {
                dt1 = (Convert.ToDateTime(dateTimePicker1.Text)).AddHours(8);
                dt2 = dt1.AddHours(12);
            }
            else if (comboBox2.Text == "夜班")
            {
                dt1 = (Convert.ToDateTime(dateTimePicker1.Text)).AddHours(20);
                dt2 = dt1.AddHours(12);
            }
            if (comboBox1.Text == null || comboBox1.Text == "" || comboBox1.Text.Length < 1)
            {
                MessageBox.Show("未选择产品型号");
                return;
            }
            string tpname = comboBox1.Text.Trim();
            if (SysStatus.CurProductName != comboBox1.Text.Trim()) tpname = comboBox1.Text.Trim();

            string sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultVoltage = '1'";
            int aa1 = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));
            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultVoltage = '0'";
            int aa2 = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));
            ST1OK.Text = aa1.ToString();
            ST1NG.Text = aa2.ToString();
            ST1Lv.Text = ((Convert.ToDouble(ST1OK.Text) / (Convert.ToDouble(ST1OK.Text) + Convert.ToDouble(ST1NG.Text))) * 100).ToString("f2") + "%";

            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultHigh1 = '1'";
            aa1 = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));
            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultHigh1 = '0'";
            aa2 = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));
            ST2OK.Text = aa1.ToString();
            ST2NG.Text = aa2.ToString();
            ST2Lv.Text = ((Convert.ToDouble(ST2OK.Text) / (Convert.ToDouble(ST2OK.Text) + Convert.ToDouble(ST2NG.Text))) * 100).ToString("f2") + "%";

            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultPostion1 = '1'";
            aa1 = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));
            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultPostion1 = '0'";
            aa2 = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));
            ST3OK.Text = aa1.ToString();
            ST3NG.Text = aa2.ToString();
            ST3Lv.Text = ((Convert.ToDouble(ST3OK.Text) / (Convert.ToDouble(ST3OK.Text) + Convert.ToDouble(ST3NG.Text))) * 100).ToString("f2") + "%";

            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultHigh2= '1'";
            aa1 = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));
            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultHigh2 = '0'";
            aa2 = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));
            ST4OK.Text = aa1.ToString();
            ST4NG.Text = aa2.ToString();
            ST4Lv.Text = ((Convert.ToDouble(ST4OK.Text) / (Convert.ToDouble(ST4OK.Text) + Convert.ToDouble(ST4NG.Text))) * 100).ToString("f2") + "%";

            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultPostion2 = '1'";
            aa1 = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));
            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultPostion2 = '0'";
            aa2 = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));
            ST5OK.Text = aa1.ToString();
            ST5NG.Text = aa2.ToString();
            ST5Lv.Text = ((Convert.ToDouble(ST5OK.Text) / (Convert.ToDouble(ST5OK.Text) + Convert.ToDouble(ST5NG.Text))) * 100).ToString("f2") + "%";

            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultLaser = '1'";
            aa1 = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));
            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultLaser = '0'";
            aa2 = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));
            ST6OK.Text = aa1.ToString();
            ST6NG.Text = aa2.ToString();
            ST6Lv.Text = ((Convert.ToDouble(ST6OK.Text) / (Convert.ToDouble(ST6OK.Text) + Convert.ToDouble(ST7NG.Text))) * 100).ToString("f2") + "%";

            //历史总计

            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultEnd = '1'";
            aa1 = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));
            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultEnd = '0'";
            aa2 = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));

            ST7OK.Text = aa1.ToString();
            ST7NG.Text = aa2.ToString();
            ST7Lv.Text = ((Convert.ToDouble(ST7OK.Text) / (Convert.ToDouble(ST7OK.Text) + Convert.ToDouble(ST7NG.Text))) * 100).ToString("f2") + "%";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            dateTimePicker1_CloseUp(null, null);
        }
    }
}
