using MyLib.Input;
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

namespace CXPro001.ShowControl
{
    public partial class FlatnessShowCtr : UserControl
    {
        private FlatnessSet flatnessSet1 = null;
        public FlatnessShowCtr()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 初始化--当配置参数控件时使用
        /// </summary>
        /// <param name="flatnessSet"></param>
        public void init(FlatnessSet flatnessSet)
        {
            flatnessSet1 = flatnessSet;
            label2.Text = SysStatus.CurProductName;
        }
        #region 加载保存参数--设置界面专用
        /// <summary>
        /// 保存参数
        /// </summary>      
        public void SaveSetting()
        {
            flatnessSet1 = new FlatnessSet();
            flatnessSet1.area_A_Base = Convert.ToSingle(labelA1.Text);
            flatnessSet1.area_B_Base = Convert.ToSingle(labelA2.Text);
            flatnessSet1.area_C_Base = Convert.ToSingle(labelA3.Text);
            flatnessSet1.area_D_Base = Convert.ToSingle(labelA4.Text);
            flatnessSet1.area_E_Base = Convert.ToSingle(labelA5.Text);
            flatnessSet1.area_F_Base = Convert.ToSingle(labelA6.Text);

            flatnessSet1.area_A_Mode = Convert.ToSingle(labelB1.Text);
            flatnessSet1.area_B_Mode = Convert.ToSingle(labelB2.Text);
            flatnessSet1.area_C_Mode = Convert.ToSingle(labelB3.Text);
            flatnessSet1.area_D_Mode = Convert.ToSingle(labelB4.Text);
            flatnessSet1.area_E_Mode = Convert.ToSingle(labelB5.Text);
            flatnessSet1.area_F_Mode = Convert.ToSingle(labelB6.Text);

            flatnessSet1.ToleUp_A = Convert.ToSingle(labelC1.Text);
            flatnessSet1.ToleUp_B = Convert.ToSingle(labelC2.Text);
            flatnessSet1.ToleUp_C = Convert.ToSingle(labelC3.Text);
            flatnessSet1.ToleUp_D = Convert.ToSingle(labelC4.Text);
            flatnessSet1.ToleUp_E = Convert.ToSingle(labelC5.Text);
            flatnessSet1.ToleUp_F = Convert.ToSingle(labelC6.Text);

            string typename = label2.Text.Trim();
            flatnessSet1.SaveParam(typename);
        }
        /// <summary>
        /// 加载参数
        /// </summary>
        public void LoadSetting()
        {
            flatnessSet1 = new FlatnessSet();
            string typename = label2.Text.Trim();
            flatnessSet1.LoadParam(typename);
            labelA1.Text = flatnessSet1.area_A_Base.ToString();
            labelA2.Text = flatnessSet1.area_B_Base.ToString();
            labelA3.Text = flatnessSet1.area_C_Base.ToString();
            labelA4.Text = flatnessSet1.area_D_Base.ToString();
            labelA5.Text = flatnessSet1.area_E_Base.ToString();
            labelA6.Text = flatnessSet1.area_F_Base.ToString();

            labelB1.Text = flatnessSet1.area_A_Mode.ToString();
            labelB2.Text = flatnessSet1.area_B_Mode.ToString();
            labelB3.Text = flatnessSet1.area_C_Mode.ToString();
            labelB4.Text = flatnessSet1.area_D_Mode.ToString();
            labelB5.Text = flatnessSet1.area_E_Mode.ToString();
            labelB6.Text = flatnessSet1.area_F_Mode.ToString();

            labelC1.Text = flatnessSet1.ToleUp_A.ToString();
            labelC2.Text = flatnessSet1.ToleUp_B.ToString();
            labelC3.Text = flatnessSet1.ToleUp_C.ToString();
            labelC4.Text = flatnessSet1.ToleUp_D.ToString(); 
            labelC5.Text = flatnessSet1.ToleUp_E.ToString();
            labelC6.Text = flatnessSet1.ToleUp_F.ToString();


        }
        #endregion
        /// <summary>
        /// 显示所有
        /// </summary>
        /// <param name="flatnessSet"></param>
        public void ShowAllLabel(FlatnessSet flatnessSet)
        {
            Invoke(new Action(() => { 
            labelA1.Text = flatnessSet.area_A_P1.ToString();
            labelB1.Text = flatnessSet.area_A_P2.ToString();
            labelC1.Text = flatnessSet.area_A_P3.ToString();
            labelA2.Text = flatnessSet.area_B_P1.ToString();
            labelB2.Text = flatnessSet.area_B_P2.ToString();
            labelC2.Text = flatnessSet.area_B_P3.ToString();
            labelA3.Text = flatnessSet.area_C_P1.ToString();
            labelB3.Text = flatnessSet.area_C_P2.ToString();
            labelC3.Text = flatnessSet.area_C_P3.ToString();
            labelA4.Text = flatnessSet.area_D_P1.ToString();
            labelB4.Text = flatnessSet.area_D_P2.ToString();
            labelC4.Text = flatnessSet.area_D_P3.ToString();
            labelA5.Text = flatnessSet.area_E_P1.ToString();
            labelB5.Text = flatnessSet.area_E_P2.ToString();
            labelC5.Text = flatnessSet.area_E_P3.ToString();
            //labelA6.Text = flatnessSet.area_F_P1.ToString();
            //labelB6.Text = flatnessSet.area_F_P2.ToString();
            //labelC6.Text = flatnessSet.area_F_P3.ToString();
            //测量值 基准值 公差
            labelD1.Text = flatnessSet.area_A_AVG.ToString();
            labelE1.Text = flatnessSet.area_A_Base.ToString();
            labelF1.Text = flatnessSet.area_A_Tole.ToString();
            labelD2.Text = flatnessSet.area_B_AVG.ToString();
            labelE2.Text = flatnessSet.area_B_Base.ToString();
            labelF2.Text = flatnessSet.area_B_Tole.ToString();
            labelD3.Text = flatnessSet.area_C_AVG.ToString();
            labelE3.Text = flatnessSet.area_C_Base.ToString();
            labelF3.Text = flatnessSet.area_C_Tole.ToString();
            labelD4.Text = flatnessSet.area_D_AVG.ToString();
            labelE4.Text = flatnessSet.area_D_Base.ToString();
            labelF4.Text = flatnessSet.area_D_Tole.ToString();
            labelD5.Text = flatnessSet.area_E_AVG.ToString();
            labelE5.Text = flatnessSet.area_E_Base.ToString();
            labelF5.Text = flatnessSet.area_E_Tole.ToString();
            //labelD6.Text = flatnessSet.area_F_AVG.ToString();
            //labelE6.Text = flatnessSet.area_F_Base.ToString();
            //labelF6.Text = flatnessSet.area_F_Tole.ToString();
            //显示判断结果
            if(flatnessSet.ResA)
            {
                labelA1.BackColor = labelB1.BackColor = labelC1.BackColor = labelD1.BackColor = labelE1.BackColor = labelF1.BackColor = Color.LimeGreen;
            }
            else
            {
                labelA1.BackColor = labelB1.BackColor = labelC1.BackColor = labelD1.BackColor = labelE1.BackColor = labelF1.BackColor = Color.Red;
            }
            if (flatnessSet.ResB)
            {
                labelA2.BackColor = labelB2.BackColor = labelC2.BackColor = labelD2.BackColor = labelE2.BackColor = labelF2.BackColor = Color.LimeGreen;
            }
            else
            {
                labelA2.BackColor = labelB2.BackColor = labelC2.BackColor = labelD2.BackColor = labelE2.BackColor = labelF2.BackColor = Color.Red;
            }
            if (flatnessSet.ResC)
            {
                labelA3.BackColor = labelB3.BackColor = labelC3.BackColor = labelD3.BackColor = labelE3.BackColor = labelF3.BackColor = Color.LimeGreen;
            }
            else
            {
                labelA3.BackColor = labelB3.BackColor = labelC3.BackColor = labelD3.BackColor = labelE3.BackColor = labelF3.BackColor = Color.Red;
            }
            if (flatnessSet.ResD)
            {
                labelA4.BackColor = labelB4.BackColor = labelC4.BackColor = labelD4.BackColor = labelE4.BackColor = labelF4.BackColor = Color.LimeGreen;
            }
            else
            {
                labelA4.BackColor = labelB4.BackColor = labelC4.BackColor = labelD4.BackColor = labelE4.BackColor = labelF4.BackColor = Color.Red;
            }
            if (flatnessSet.ResE)
            {
                labelA5.BackColor = labelB5.BackColor = labelC5.BackColor = labelD5.BackColor = labelE5.BackColor = labelF5.BackColor = Color.LimeGreen;
            }
            else
            {
                labelA5.BackColor = labelB5.BackColor = labelC5.BackColor = labelD5.BackColor = labelE5.BackColor = labelF5.BackColor = Color.Red;
            }
            //if (flatnessSet.ResF)
            //{
            //    labelA6.BackColor = labelB6.BackColor = labelC6.BackColor = labelD6.BackColor = labelE6.BackColor = labelF6.BackColor = Color.LimeGreen;
            //}
            //else
            //{
            //    labelA6.BackColor = labelB6.BackColor = labelC6.BackColor = labelD6.BackColor = labelE6.BackColor = labelF6.BackColor = Color.Red;
            //}
            if (flatnessSet.ResAll)
            {
                label7.Text = "OK"; label7.BackColor = Color.LimeGreen;
            }
            else
            {
                label7.Text = "NG"; label7.BackColor = Color.Red;
            }
            }));
        }
        //清除结果显示
        public void ClearAll()
        {
            Invoke(new Action(() => {
                labelA1.Text = labelB1.Text = labelC1.Text = labelA2.Text = "-";           
            labelB2.Text = labelC2.Text = labelA3.Text = labelB3.Text = "-";           
            labelC3.Text = labelA4.Text = labelB4.Text = labelC4.Text = "-";             
            labelA5.Text = labelB5.Text = labelC5.Text = labelA6.Text = labelB6.Text = labelC6.Text = "-";

            //测量值 基准值 公差
            labelD1.Text = labelE1.Text = labelF1.Text = labelD2.Text = labelE2.Text = labelF2.Text = "-";
            labelD3.Text = labelE3.Text = labelF3.Text = labelD4.Text = labelE4.Text = labelF4.Text = "-";
            labelD5.Text = labelE5.Text = labelF5.Text = labelD6.Text = labelE6.Text = labelF6.Text = "-";
            //显示判断结果

            labelA1.BackColor = labelB1.BackColor = labelC1.BackColor = labelD1.BackColor = labelE1.BackColor = labelF1.BackColor = Color.White;
            labelA2.BackColor = labelB2.BackColor = labelC2.BackColor = labelD2.BackColor = labelE2.BackColor = labelF2.BackColor = Color.White;
            labelA3.BackColor = labelB3.BackColor = labelC3.BackColor = labelD3.BackColor = labelE3.BackColor = labelF3.BackColor = Color.White;
            labelA4.BackColor = labelB4.BackColor = labelC4.BackColor = labelD4.BackColor = labelE4.BackColor = labelF4.BackColor = Color.White;
            labelA5.BackColor = labelB5.BackColor = labelC5.BackColor = labelD5.BackColor = labelE5.BackColor = labelF5.BackColor = Color.White;
            labelA6.BackColor = labelB6.BackColor = labelC6.BackColor = labelD6.BackColor = labelE6.BackColor = labelF6.BackColor = Color.White;
            label7.Text = "-"; label7.BackColor = Color.White;

            }));
        }
        /// <summary>
        /// 将配置参数显示出来基准
        /// </summary>
        public void ShowSetting(FlatnessSet flatnessSet)
        {
            Invoke(new Action(() => {
                labelE1.Text = flatnessSet.area_A_Base.ToString();
                labelE2.Text = flatnessSet.area_B_Base.ToString();
                labelE3.Text = flatnessSet.area_C_Base.ToString();
                labelE4.Text = flatnessSet.area_D_Base.ToString();
                labelE5.Text = flatnessSet.area_E_Base.ToString();
                labelE6.Text = flatnessSet.area_F_Base.ToString();

                //labelF1.Text = flatnessSet.ToleUp_A.ToString();
                //labelF2.Text = flatnessSet.ToleUp_B.ToString();
                //labelF3.Text = flatnessSet.ToleUp_C.ToString();
                //labelF4.Text = flatnessSet.ToleUp_D.ToString();
                //labelF5.Text = flatnessSet.ToleUp_E.ToString();
                //labelF6.Text = flatnessSet.ToleUp_F.ToString();
            }));
        }
        #region 属性
        private string label1NAME = "二维码";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("二维码叫法名称")]
        public string label1NAME1
        {
            get { return label1NAME; }
            set
            {
                this.label1.Text = label1NAME = value;
            }
        }
        private string label15NAME = "面A";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("面A名称")]
        public string label15NAME1
        {
            get { return label15NAME; }
            set
            {
                this.label15.Text = label15NAME = value;
            }
        }
        private string label22NAME = "面B";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("面B名称")]
        public string label22NAME1
        {
            get { return label22NAME; }
            set
            {
                this.label22.Text = label22NAME = value;
            }
        }
        private string label29NAME = "面C";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("面C名称")]
        public string label29NAME1
        {
            get { return label29NAME; }
            set
            {
                this.label29.Text = label29NAME = value;
            }
        }
        private string label36NAME = "面D";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("面D名称")]
        public string label36NAME1
        {
            get { return label36NAME; }
            set
            {
                this.label36.Text = label36NAME = value;
            }
        }
        private string label43NAME = "面E";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("面E名称")]
        public string label43NAME1
        {
            get { return label43NAME; }
            set
            {
                this.label43.Text = label43NAME = value;
            }
        }

        private string label50NAME = "面F";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("面F名称")]
        public string label50NAME1
        {
            get { return label50NAME; }
            set
            {
                this.label50.Text = label50NAME = value;
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
        private string label6NAME = "结果";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("结果label")]
        public string label6NAME1
        {
            get { return label6NAME; }
            set
            {
                this.label6.Text = label6NAME = value;
            }
        }
        private string label9NAME = "P1";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P1")]
        public string label9NAME1
        {
            get { return label9NAME; }
            set
            {
                this.label9.Text = label9NAME = value;
            }
        }
        private string label10NAME = "P2";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P2")]
        public string label10NAME1
        {
            get { return label10NAME; }
            set
            {
                this.label10.Text = label10NAME = value;
            }
        }
        private string label11NAME = "P3";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P3")]
        public string label11NAME1
        {
            get { return label11NAME; }
            set
            {
                this.label11.Text = label11NAME = value;
            }
        }
        private string label12NAME = "测量值";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("测量值")]
        public string label12NAME1
        {
            get { return label12NAME; }
            set
            {
                this.label12.Text = label12NAME = value;
            }
        }
        private string label13NAME = "标准值";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("标准值")]
        public string label13NAME1
        {
            get { return label13NAME; }
            set
            {
                this.label13.Text = label13NAME = value;
            }
        }
        private string label14NAME = "公差";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("公差")]
        public string label14NAME1
        {
            get { return label14NAME; }
            set
            {
                this.label14.Text = label14NAME = value;
            }
        }
         
        #endregion
        private void label42_DoubleClick(object sender, EventArgs e)
        {
            if(ChangeText) NumPad.Show((Label)sender);

        }
    }
}
