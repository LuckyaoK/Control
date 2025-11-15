using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MyLib.SqlHelper;
using CXPro001.myclass;
namespace CXPro001.controls
{
    public partial class ProCountslist : UserControl
    {
        public ProCountslist()
        {
            InitializeComponent();
        }
 

        /// </summary>
        #region 生产计数
        List<int> counsOK = new List<int>();
        List<int> counsNG = new List<int>();
        #endregion
        private void button1_Click(object sender, EventArgs e)//最近一月
        {
            if (comboBox1.Text == null || comboBox1.Text == "" || comboBox1.Text.Length < 1)
            {
                MessageBox.Show("未选择产品型号");
                return;
            }
            string tpname = comboBox1.Text.Trim();
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();        
            DateTime dt1 = (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")));
            DateTime dtstar;
            DateTime dtend;
            for (int i = 0; i < 30; i++)
            {
                dtstar = dt1.AddDays(-(29-i));
                dtend = dtstar.AddDays(1.0);
                string sql1 = "select COUNT(*) from Result_Hansler t1 inner join Height_CCD t2 on t1.Cord = t2.Cord  inner  join Height_CCD2 t3 on t2.Cord = t3.Cord " +
                  "inner join Position_CCD t4 on t3.Cord = t4.Cord inner join Position_CCD2 t5 on t4.Cord = t5.Cord inner join VoltageData_8740 t6 on t5.Cord = t6.Cord " +
                  $" where  DATEDIFF(D,t1.InsertTime,'{dtstar}')=0 and(t1.Result = 1 and t2.Result = 1 and t3.Result = 1 and t3.Result = 1 and t4.Result =1 and t5.Result = 1 and t6.Result = 1)";
                int aa1 = Convert.ToInt32(SQLHelper.ExecuteScalar(sql1));
                
                chart1.Series[0].Points.AddXY(dtstar, aa1);
                sql1 = "select COUNT(*) from Result_Hansler t1 inner join Height_CCD t2 on t1.Cord = t2.Cord  inner  join Height_CCD2 t3 on t2.Cord = t3.Cord " +
                 "inner join Position_CCD t4 on t3.Cord = t4.Cord inner join Position_CCD2 t5 on t4.Cord = t5.Cord inner join VoltageData_8740 t6 on t5.Cord = t6.Cord " +
                 $" where DATEDIFF(D,t1.InsertTime,'{dtstar}')=0 and(t1.Result = 0 or t2.Result = 0 or t3.Result = 0 or t3.Result = 0 or t4.Result = 0 or t5.Result = 0 or t6.Result = 0)";
                aa1 = Convert.ToInt32(SQLHelper.ExecuteScalar(sql1));
                chart1.Series[1].Points.AddXY(dtstar, aa1);

            }
           
        }

        private void button2_Click(object sender, EventArgs e)//最近一周
        {
            if (comboBox1.Text == null || comboBox1.Text == "" || comboBox1.Text.Length < 1)
            {
                MessageBox.Show("未选择产品型号");
                return;
            }
            chart1.Series[0].XValueType = ChartValueType.Date;
            chart1.Series[1].XValueType = ChartValueType.Date;
            string tpname = comboBox1.Text.Trim();
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            DateTime dt1 = (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")));
            DateTime dtstar;
            DateTime dtend;
            for (int i = 0; i < 7; i++)
            {
                dtstar = dt1.AddDays(-(6 - i));
                dtend = dtstar.AddDays(1.0);

            //    string sql1 = $"select  count(*) from EndResultDB where InsertTime between '{dtstar}' and '{dtend}' and ProductType = '{tpname}'  and ResultEnd = '1' ";
                string sql1 = "select COUNT(*) from Result_Hansler t1 inner join Height_CCD t2 on t1.Cord = t2.Cord  inner  join Height_CCD2 t3 on t2.Cord = t3.Cord " +
               "inner join Position_CCD t4 on t3.Cord = t4.Cord inner join Position_CCD2 t5 on t4.Cord = t5.Cord inner join VoltageData_8740 t6 on t5.Cord = t6.Cord " +
               $" where  DATEDIFF(D,t1.InsertTime,'{dtstar}')=0 and(t1.Result = 1 and t2.Result = 1 and t3.Result = 1 and t3.Result = 1 and t4.Result =1 and t5.Result = 1 and t6.Result = 1)";

                double aa1 = Convert.ToDouble(SQLHelper.ExecuteScalar(sql1));
                 
                chart1.Series[0].Points.AddXY( dtstar,aa1);


                //不合格
                sql1 = "select COUNT(*) from Result_Hansler t1 inner join Height_CCD t2 on t1.Cord = t2.Cord  inner  join Height_CCD2 t3 on t2.Cord = t3.Cord " +
                "inner join Position_CCD t4 on t3.Cord = t4.Cord inner join Position_CCD2 t5 on t4.Cord = t5.Cord inner join VoltageData_8740 t6 on t5.Cord = t6.Cord " +
                $" where DATEDIFF(D,t1.InsertTime,'{dtstar}')=0 and(t1.Result = 0 or t2.Result = 0 or t3.Result = 0 or t3.Result = 0 or t4.Result = 0 or t5.Result = 0 or t6.Result = 0)";
             
                aa1 = Convert.ToDouble(SQLHelper.ExecuteScalar(sql1));
                chart1.Series[1].Points.AddXY(dtstar, aa1);
                var aaa = dtstar.ToOADate();

            }

        }

        private void ProCountslist_Load(object sender, EventArgs e)
        {
            chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "yyyy-MM-dd";
            comboBox1.Text = SysStatus.CurProductName;
            init();
        }
        public void init()
        {
            chart1.ChartAreas[0].InnerPlotPosition.Auto = false;
            chart1.ChartAreas[0].InnerPlotPosition.X = 3;
            chart1.ChartAreas[0].InnerPlotPosition.Y = 1;
            //chart1.ChartAreas[0].InnerPlotPosition.Width = this.Width - 5;
            //chart1.ChartAreas[0].InnerPlotPosition.Height = this.Height - 35;
        }
        #region 自定义属性
        private bool autoEnb = false;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("InnerPlotPosition.Auto")]
        public bool AutoEnb
        {
            get { return autoEnb; }
            set
            {
                this.chart1.ChartAreas[0].InnerPlotPosition.Auto = autoEnb = value;
            }
        }
        private int POSTIONWidth = 63;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("InnerPlotPosition.Width")]
        public int Pos_Width
        {
            get { return POSTIONWidth; }
            set
            {
                this.chart1.ChartAreas[0].InnerPlotPosition.Width = POSTIONWidth = value;
            }
        }
        private int POSTIONHeight = 63;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("InnerPlotPosition.Height")]
        public int Pos_Height
        {
            get { return POSTIONHeight; }
            set
            {
                this.chart1.ChartAreas[0].InnerPlotPosition.Height = POSTIONHeight = value;
            }
        }
        private float POSTIONx = 3;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("InnerPlotPosition.X")]
        public float Pos_X
        {
            get { return POSTIONx; }
            set
            {
                this.chart1.ChartAreas[0].InnerPlotPosition.X = POSTIONx = value;
            }
        }
        private float POSTIONy = 3;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("InnerPlotPosition.Y")]
        public float Pos_Y
        {
            get { return POSTIONy; }
            set
            {
                this.chart1.ChartAreas[0].InnerPlotPosition.Y = POSTIONy = value;
            }
        }

        #endregion
    }
}
