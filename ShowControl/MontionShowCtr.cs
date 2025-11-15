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
    public partial class MontionShowCtr : UserControl
    {
        public MontionShowCtr()
        {
            InitializeComponent();
        }


        private string p1text = "10000";

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("电机1位置")]
        public string P1Text
        {
            get { return p1text; }
            set
            {
                this.lbl_Pos1.Text = p1text = value;

            }
        }

        private string p2text = "10000";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("电机2位置")]
        public string P2Text
        {
            get { return p2text; }
            set
            {
                this.lbl_Pos2.Text = p2text = value;

            }
        }

        private string p3text = "10000";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("电机3位置")]
        public string P3Text
        {
            get { return p3text; }
            set
            {
                this.lbl_Pos3.Text = p3text = value;

            }
        }

        private string p4text = "10000";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("电机4位置")]
        public string P4Text
        {
            get { return p4text; }
            set
            {
                this.lbl_Pos4.Text = p4text = value;

            }
        }

        private string p5text = "10000";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("电机5位置")]
        public string P5Text
        {
            get { return p5text; }
            set
            {
                this.lbl_Pos5.Text = p5text = value;

            }
        }

        private string p6text = "10000";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("电机6位置")]
        public string P6Text
        {
            get { return p6text; }
            set
            {
                this.lbl_Pos6.Text = p6text = value;

            }
        }

        private string step1text = "1";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("流程1")]
        public string Step1text
        {
            get { return step1text; }
            set
            {
                this.lbl_sys_step1.Text = step1text = value;

            }
        }

        private string step2text = "2";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("流程2")]
        public string Step2text
        {
            get { return step2text; }
            set
            {
                this.lbl_sys_step2.Text = step2text = value;

            }
        }


        private string step3text = "2";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("流程3")]
        public string Step3text
        {
            get { return step3text; }
            set
            {
                this.lbl_sys_step3.Text = step3text = value;

            }
        }


        private string step4text = "2";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("流程4")]
        public string Step4text
        {
            get { return step4text; }
            set
            {
                this.lbl_sys_step4.Text = step4text = value;

            }
        }

        private string step5text = "2";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("流程5")]
        public string Step5text
        {
            get { return step5text; }
            set
            {
                this.lbl_sys_step5.Text = step5text = value;

            }
        }
        private string step6text = "2";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("流程6")]
        public string Step6text
        {
            get { return step6text; }
            set
            {
                this.lbl_sys_step6.Text = step6text = value;

            }
        }
        private string step7text = "2";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("流程7")]
        public string Step7text
        {
            get { return step7text; }
            set
            {
                this.lbl_sys_step7.Text = step7text = value;

            }
        }

        private string step8text = "2";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("流程8")]
        public string Step8text
        {
            get { return step8text; }
            set
            {
                this.lbl_sys_step8.Text = step8text = value;

            }
        }

        private string step9text = "2";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("流程9")]
        public string Step9text
        {
            get { return step9text; }
            set
            {
                this.lbl_sys_step9.Text = step9text = value;

            }
        }
        /// <summary>
        /// 显示最终的结果
        /// </summary>
        /// <param name="res">OK NG</param>
        public void ShowPos(int index,string str)
        {
            BeginInvoke(new Action(() =>
            {
                if (index == 1)
                    lbl_Pos1.Text = str;
                else if (index == 2)
                    lbl_Pos2.Text = str;
                else if (index == 3)
                    lbl_Pos3.Text = str;
                else if (index == 4)
                    lbl_Pos4.Text = str;
                else if (index == 5)
                    lbl_Pos5.Text = str;
                else if (index == 6)
                    lbl_Pos6.Text = str;
 
            }));


        }

        /// <summary>
        /// 显示最终的结果
        /// </summary>
        /// <param name="res">OK NG</param>
        public void ShowStep(int index,string str)
        {
            BeginInvoke(new Action(() =>
            {
                if(index==1)
                lbl_sys_step1.Text = str;
                else if(index==2)
                    lbl_sys_step2.Text = str;
                else if (index == 3)
                    lbl_sys_step3.Text = str;
                else if (index == 4)
                    lbl_sys_step4.Text = str;
                else if (index == 5)
                    lbl_sys_step5.Text = str;
                else if (index == 6)
                    lbl_sys_step6.Text = str;
                else if (index == 7)
                    lbl_sys_step7.Text = str;
                else if (index == 8)
                    lbl_sys_step8.Text = str;
                else if (index == 9)
                    lbl_sys_step9.Text = str;
            }));

        }
        public void ShowError(int index, string str)
        {
            BeginInvoke(new Action(() =>
            {
              /*  if (index == 1)
                    lbl_sts_1.Text = str;
                else if (index == 2)
                    lbl_sts_2.Text = str;
                else if (index == 3)
                    lbl_sts_3.Text = str;
                else if (index == 4)
                    lbl_sts_4.Text = str;
                else if (index == 5)
                    lbl_sts_5.Text = str;
                else if (index == 6)
                    lbl_sts_6.Text = str;*/
            }));
        }
        /// <summary>
        /// 显示对应站的二维码
        /// </summary>
        /// <param name="index"></param>
        /// <param name="str"></param>
        public void ShowCoder(int station, int lr, string coder)
        {
            BeginInvoke(new Action(() =>
            {
                if (station == 1)
                    if (lr == 1)
                        lbl_coder_1_1.Text = coder;
                    else
                        lbl_coder_1_2.Text = coder;

                if (station == 2)
                    if (lr == 1)
                        lbl_coder_3_1.Text = coder;
                    else
                        lbl_coder_3_2.Text = coder;
                if (station == 3)
                    if (lr == 1)
                        lbl_coder_4_1.Text = coder;
                    else
                        lbl_coder_4_2.Text = coder;
                if (station == 4)
                    if (lr == 1)
                        lbl_coder_5_1.Text = coder;
                    else
                        lbl_coder_5_2.Text = coder;
                if (station == 5)
                    if (lr == 1)
                        lbl_coder_6_1.Text = coder;
                    else
                        lbl_coder_6_2.Text = coder;
                if (station == 6)
                    if (lr == 1)
                        lbl_coder_7_1.Text = coder;
                    else
                        lbl_coder_7_2.Text = coder;
            }));
        }


        /// <summary>
        /// 显示模号
        /// </summary>
        /// <param name="index"></param>
        /// <param name="str"></param>
        public void ShowModel(int station, int lr, string num)
        {
            BeginInvoke(new Action(() =>
            {
                if (station == 1)
                    if (lr == 1)
                        lbl_model_1_1.Text = num;
                    else
                        lbl_model_1_2.Text = num;
                if (station == 2)
                    if (lr == 1)
                        lbl_model_3_1.Text = num;
                    else
                        lbl_model_3_2.Text = num;
                if (station == 3)
                    if (lr == 1)
                        lbl_model_4_1.Text = num;
                    else
                        lbl_model_4_2.Text = num;
                if (station == 4)
                    if (lr == 1)
                        lbl_model_5_1.Text = num;
                    else
                        lbl_model_5_2.Text = num;
                if (station == 5)
                    if (lr == 1)
                        lbl_model_6_1.Text = num;
                    else
                        lbl_model_6_2.Text = num;
                if (station == 6)
                    if (lr == 1)
                        lbl_model_7_1.Text = num;
                    else
                        lbl_model_7_2.Text = num;
            }));
        }




        /// <summary>
        /// 设置显示状态  颜色
        /// </summary>
        /// <param name="index"></param>
        /// <param name="b">1 正常运行  0 不显示  2报警</param>
        public void ShowSts(int index,int b)
        {
            Color cr;
            BeginInvoke(new Action(() =>
            {
                if (b == 1)
                    cr = Color.Green;
                else if (b == 3)
                    cr = Color.Red;
                else if (b == 2)
                    cr = Color.Brown;
                else
                    cr = SystemColors.Control;

                if (index == 1)
                    panel1.BackColor = cr;
                else if (index == 2)
                    panel2.BackColor = cr;
                else if (index == 3)
                    panel3.BackColor = cr;
                else if (index == 4)
                    panel4.BackColor = cr;
                else if (index == 5)
                    panel5.BackColor = cr;
                else if (index == 6)
                    panel6.BackColor = cr;
                else if (index ==7)
                    panel7.BackColor = cr;
            }));
        }
    }
}
