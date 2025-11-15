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
using CXPro001.classes;
using CXPro001.myclass;
namespace CXPro001.ShowControl
{
    /// <summary>
    /// 高度 位置度 显示、设置界面控件
    /// </summary>
    public partial class CCD_Pos_High_ShowCtr : UserControl
    {
        /// <summary>
        /// 位置参数
        /// </summary>
        //   My_CCD CCDPostion1 = new My_CCD();
        /// <summary>
        /// 位置参数
        /// </summary>
        //   My_CCD CCDPostion1 = new My_CCD();
        /// <summary>

        /// 位置参数
        /// </summary>
        CCDPostion_High C_POS = new CCDPostion_High();
        /// <summary>
        /// 高度参数
        /// </summary>
        CCDPostion_High C_HIGH = new CCDPostion_High();

        /// 位置参数
        /// </summary>
        CCDPostion_High N_POS = new CCDPostion_High();
        /// <summary>
        /// 高度参数
        /// </summary>
        CCDPostion_High N_HIGH = new CCDPostion_High();
        public CCD_Pos_High_ShowCtr()
        {
            InitializeComponent();
        }

        #region  做显示控件来用
        public void init_pos(CCDPostion_High CCDPostion)
        {
            //理论值
            labLX1.Text = CCDPostion.Theory1X.ToString(); labLY1.Text = CCDPostion.Theory1Y.ToString();
            labLX2.Text = CCDPostion.Theory2X.ToString(); labLY2.Text = CCDPostion.Theory2Y.ToString();
            labLX3.Text = CCDPostion.Theory3X.ToString(); labLY3.Text = CCDPostion.Theory3Y.ToString();
            labLX4.Text = CCDPostion.Theory4X.ToString(); labLY4.Text = CCDPostion.Theory4Y.ToString();
            labLX5.Text = CCDPostion.Theory5X.ToString(); labLY5.Text = CCDPostion.Theory5Y.ToString();
            labLX6.Text = CCDPostion.Theory6X.ToString(); labLY6.Text = CCDPostion.Theory6Y.ToString();
            //上限
            labHX1.Text = CCDPostion.Up1X.ToString(); labHY1.Text = CCDPostion.Up1Y.ToString();
            labHX2.Text = CCDPostion.Up2X.ToString(); labHY2.Text = CCDPostion.Up2Y.ToString();
            labHX3.Text = CCDPostion.Up3X.ToString(); labHY3.Text = CCDPostion.Up3Y.ToString();
            labHX4.Text = CCDPostion.Up4X.ToString(); labHY4.Text = CCDPostion.Up4Y.ToString();
            labHX5.Text = CCDPostion.Up5X.ToString(); labHY5.Text = CCDPostion.Up5Y.ToString();
            labHX6.Text = CCDPostion.Up6X.ToString(); labHY6.Text = CCDPostion.Up6Y.ToString();
            //下限
            labDX1.Text = CCDPostion.Down1X.ToString(); labDY1.Text = CCDPostion.Down1Y.ToString();
            labDX2.Text = CCDPostion.Down2X.ToString(); labDY2.Text = CCDPostion.Down2Y.ToString();
            labDX3.Text = CCDPostion.Down3X.ToString(); labDY3.Text = CCDPostion.Down3Y.ToString();
            labDX4.Text = CCDPostion.Down4X.ToString(); labDY4.Text = CCDPostion.Down4Y.ToString();
            labDX5.Text = CCDPostion.Down5X.ToString(); labDY5.Text = CCDPostion.Down5Y.ToString();
            labDX6.Text = CCDPostion.Down6X.ToString(); labDY6.Text = CCDPostion.Down6Y.ToString();

        }


        #region 显示上下限理论值 实测值
        /// <summary>
        ///显示实时值
        /// </summary>
        public void ShowParamPostion(My_CCD CCDPostion)
        {
            Invoke(new Action(() => {
                //理论值
                //     labelmode.Text = "";
                //    labLX1.Text = CCDPostion1.Theory1X.ToString(); labLY1.Text = CCDPostion1.Theory1Y.ToString();
                //     labLX2.Text = CCDPostion1.Theory2X.ToString(); labLY2.Text = CCDPostion1.Theory2Y.ToString();
                //      labLX3.Text = CCDPostion1.Theory3X.ToString(); labLY3.Text = CCDPostion1.Theory3Y.ToString();
                //         labLX4.Text = labLY4.Text = labLX5.Text = "0";
                label2.Text = CCDPostion.Cords;
                label7.Text = CCDPostion.ResAll ? "OK" : "NG";
                //labA1X.Text = CCDPostion.PosP1.ToString();//1
                //labA1Y.Text = CCDPostion.PosP1_y.ToString();

                //labA2X.Text = CCDPostion.PosP2.ToString();//2
                //labA2Y.Text = CCDPostion.PosP2_y.ToString();

                //labA3X.Text = CCDPostion.PosP3.ToString();//3
                //labA3Y.Text = CCDPostion.PosP3_y.ToString();

                //labA4X.Text = CCDPostion.PosP4.ToString();//4
                //labA4Y.Text = CCDPostion.PosP4_y.ToString();

                //labA5X.Text = CCDPostion.PosP5.ToString();//5
                //labA5Y.Text = CCDPostion.PosP5_y.ToString();

                //labA6X.Text = CCDPostion.PosP6.ToString();//6
                //labA6Y.Text = CCDPostion.PosP6_y.ToString();

                labA1X.Text = CCDPostion.Current1X.ToString();//1
                labA1Y.Text = CCDPostion.Current1Y.ToString();

                labA2X.Text = CCDPostion.Current2X.ToString();//2
                labA2Y.Text = CCDPostion.Current2Y.ToString();

                labA3X.Text = CCDPostion.Current3X.ToString();//3
                labA3Y.Text = CCDPostion.Current3Y.ToString();

                labA4X.Text = CCDPostion.Current4X.ToString();//4
                labA4Y.Text = CCDPostion.Current4Y.ToString();

                labA5X.Text = CCDPostion.Current5X.ToString();//5
                labA5Y.Text = CCDPostion.Current5Y.ToString();

                labA6X.Text = CCDPostion.Current6X.ToString();//6
                labA6Y.Text = CCDPostion.Current6Y.ToString();

                label7.BackColor = CCDPostion.ResAll ? Color.LimeGreen : Color.Red;
            }));
        }

        /// <summary>
        /// 把上下限理论值显示出来--高度
        /// </summary>
        public void ShowParamHigh(My_CCD CCDPostion)
        {
            Invoke(new Action(() => {
                //理论值
                label2.Text = CCDPostion.Cords;
                label7.Text = CCDPostion.ResAll ? "OK" : "NG";
                labA1X.Text = CCDPostion.Current1H.ToString();
                labA1Y.Text = CCDPostion.Current2H.ToString();
                labA2X.Text = CCDPostion.Current3H.ToString();
                labA2Y.Text = CCDPostion.Current4H.ToString();
                labA3X.Text = CCDPostion.Current5H.ToString();
                labA3Y.Text = CCDPostion.Current6H.ToString();
                //  lbl_P7.Text = highset.Current7H.ToString();
                //  lbl_P8.Text = highset.Current8H.ToString();
                label7.BackColor = CCDPostion.ResAll ? Color.LimeGreen : Color.Red;


            }));

        }
        /// <summary>
        /// 显示实测值
        /// </summary>
        /// <param name="CCDHP">参数</param>
        /// <param name="STA">1：高度参数   2：位置度参数</param>
        private void ShowRealValue(CCDPostion_High CCDHP, int STA)
        {
            Invoke(new Action(() => {
                label2.Text = CCDHP.Cords;
                label4.Text = CCDHP.ModelNumber;
                label7.Text = CCDHP.ResAll ? "OK" : "NG";
                label7.BackColor = CCDHP.ResAll ? Color.LimeGreen : Color.Red;
                if (STA == 1)
                {
                    if (SysStatus.CurProductName == "Y23")
                    {
                        labA1X.Text = CCDHP.Current1H.ToString(); labA1Y.Text = CCDHP.Current2H.ToString();
                        labA2X.Text = CCDHP.Current3H.ToString(); labA2Y.Text = CCDHP.Current4H.ToString();
                        labA3X.Text = CCDHP.Current5H.ToString(); //labA3Y.Text = CCDHP.Current6H.ToString();
                        labA4X.Text = CCDHP.Current7H.ToString(); labA4Y.Text = CCDHP.Current8H.ToString();
                        labA5X.Text = CCDHP.Current9H.ToString(); //labA5Y.Text = CCDHP.Current10H.ToString();
                        //labA6X.Text = CCDHP.Current11H.ToString(); labA6Y.Text = CCDHP.Current12H.ToString();
                        //labA6Y.Text = CCDHP.Current13H.ToString();
                        labA1X.BackColor = CCDHP.ResH1 ? Color.Green : Color.Red;
                        labA1Y.BackColor = CCDHP.ResH2 ? Color.Green : Color.Red;
                        labA2X.BackColor = CCDHP.ResH3 ? Color.Green : Color.Red;
                        labA2Y.BackColor = CCDHP.ResH4 ? Color.Green : Color.Red;
                        labA3X.BackColor = CCDHP.ResH5 ? Color.Green : Color.Red;
                        // labA3Y.BackColor = CCDHP.ResH6 ? Color.Green : Color.Red;
                        labA4X.BackColor = CCDHP.ResH7 ? Color.Green : Color.Red;
                        labA4Y.BackColor = CCDHP.ResH8 ? Color.Green : Color.Red;
                        labA5X.BackColor = CCDHP.ResH9 ? Color.Green : Color.Red;
                        //labA5Y.BackColor = CCDHP.ResH10 ? Color.Green : Color.Red;
                        //labA6X.BackColor = CCDHP.ResH11 ? Color.Green : Color.Red;
                        //labA6Y.BackColor = CCDHP.ResH12 ? Color.Green : Color.Red;
                        //labA7X.BackColor = CCDHP.ResH13 ? Color.Green : Color.Red;
                    }
                    else
                    {
                        labA1X.Text = CCDHP.Current1H.ToString(); labA1Y.Text = CCDHP.Current2H.ToString();
                        labA2X.Text = CCDHP.Current3H.ToString(); labA2Y.Text = CCDHP.Current4H.ToString();
                        labA3X.Text = CCDHP.Current5H.ToString(); labA3Y.Text = CCDHP.Current6H.ToString();
                        labA4X.Text = CCDHP.Current7H.ToString(); labA4Y.Text = CCDHP.Current8H.ToString();
                        labA5X.Text = CCDHP.Current9H.ToString(); labA5Y.Text = CCDHP.Current10H.ToString();
                        labA6X.Text = CCDHP.Current11H.ToString(); labA6Y.Text = CCDHP.Current12H.ToString();
                        labA6Y.Text = CCDHP.Current13H.ToString();
                        labA1X.BackColor = CCDHP.ResH1 ? Color.LimeGreen : Color.Red;
                        labA1Y.BackColor = CCDHP.ResH2 ? Color.LimeGreen : Color.Red;
                        labA2X.BackColor = CCDHP.ResH3 ? Color.LimeGreen : Color.Red;
                        labA2Y.BackColor = CCDHP.ResH4 ? Color.LimeGreen : Color.Red;
                        labA3X.BackColor = CCDHP.ResH5 ? Color.LimeGreen : Color.Red;
                        labA3Y.BackColor = CCDHP.ResH6 ? Color.LimeGreen : Color.Red;
                        labA4X.BackColor = CCDHP.ResH7 ? Color.LimeGreen : Color.Red;
                        labA4Y.BackColor = CCDHP.ResH8 ? Color.LimeGreen : Color.Red;
                        labA5X.BackColor = CCDHP.ResH9 ? Color.LimeGreen : Color.Red;
                        labA5Y.BackColor = CCDHP.ResH10 ? Color.LimeGreen : Color.Red;
                        labA6X.BackColor = CCDHP.ResH11 ? Color.LimeGreen : Color.Red;
                        labA6Y.BackColor = CCDHP.ResH12 ? Color.LimeGreen : Color.Red;
                        labA7X.BackColor = CCDHP.ResH13 ? Color.LimeGreen : Color.Red;
                    }


                }
                else if (STA == 2)
                {
                    labA1X.Text = CCDHP.Current1X.ToString(); labA1Y.Text = CCDHP.Current1Y.ToString();
                    labA2X.Text = CCDHP.Current2X.ToString(); labA2Y.Text = CCDHP.Current2Y.ToString();
                    labA3X.Text = CCDHP.Current3X.ToString(); labA3Y.Text = CCDHP.Current3Y.ToString();
                    //labA4X.Text = CCDHP.Current4X.ToString(); labA4Y.Text = CCDHP.Current4Y.ToString();
                    labA4X.Text = CCDHP.PosP1.ToString(); labA4Y.Text = CCDHP.PosP2.ToString();
                    labA5X.Text = CCDHP.PosP3.ToString(); labA6Y.Text = CCDHP.PosP4.ToString();

                    //labA1X.BackColor = CCDHP.ResPinX1 ? Color.LimeGreen : Color.Red;
                    //labA1Y.BackColor = CCDHP.ResPinY1 ? Color.LimeGreen : Color.Red;
                    //labA2X.BackColor = CCDHP.ResPinX2 ? Color.LimeGreen : Color.Red;
                    //labA2Y.BackColor = CCDHP.ResPinY2 ? Color.LimeGreen : Color.Red;
                    //labA3X.BackColor = CCDHP.ResPinX3 ? Color.LimeGreen : Color.Red;
                    //labA3Y.BackColor = CCDHP.ResPinY3 ? Color.LimeGreen : Color.Red;
                    labA4X.BackColor = CCDHP.ResPin1 ? Color.LimeGreen : Color.Red;
                    labA4Y.BackColor = CCDHP.ResPin2 ? Color.LimeGreen : Color.Red;
                    labA5X.BackColor = CCDHP.ResPin3 ? Color.LimeGreen : Color.Red;
                    labA5Y.BackColor = CCDHP.ResPin2 ? Color.LimeGreen : Color.Red;
                    labA6X.BackColor = CCDHP.ResPin3 ? Color.LimeGreen : Color.Red;
                    labA6Y.BackColor = CCDHP.ResPin4 ? Color.LimeGreen : Color.Red;

                }
                if (CCDHP.Sheild)
                {
                    labA1X.BackColor = labA1Y.BackColor = labA2X.BackColor = labA2Y.BackColor = labA3X.BackColor = labA3Y.BackColor =
                    labA4X.BackColor = labA4Y.BackColor = labA5X.BackColor = labA5Y.BackColor = labA6X.BackColor = labA6Y.BackColor = labA7X.BackColor = Color.Yellow;
                    label7.Text = "屏蔽中";
                }

            }));
        }
        /// <summary>
        /// 擦除显示值
        /// </summary>
        private void Clearvalue()
        {
            Invoke(new Action(() => {
                labelmode.Text = "";
                label7.Text = label4.Text = label2.Text = //模号和二维码 结果
                labA1X.Text = labA1Y.Text =
            labA2X.Text = labA2Y.Text =
            labA3X.Text = labA3Y.Text =
            labA4X.Text = labA4Y.Text =
            labA5X.Text = labA5Y.Text =
            labA6X.Text = labA6Y.Text =
            labA7X.Text = labA7Y.Text =
            labA8X.Text = labA8Y.Text =
            labA9X.Text = labA9Y.Text =
            labA10X.Text = labA10Y.Text =
            labA11X.Text = labA11Y.Text =
            labA12X.Text = labA12Y.Text =
            labA13X.Text = labA13Y.Text = "-";
                label7.BackColor = labA1X.BackColor = labA1Y.BackColor =
               labA2X.BackColor = labA2Y.BackColor =
               labA3X.BackColor = labA3Y.BackColor =
               labA4X.BackColor = labA4Y.BackColor =
               labA5X.BackColor = labA5Y.BackColor =
               labA6X.BackColor = labA6Y.BackColor =
               labA7X.BackColor = labA7Y.BackColor =
               labA8X.BackColor = labA8Y.BackColor =
               labA9X.BackColor = labA9Y.BackColor =
               labA10X.BackColor = labA10Y.BackColor =
               labA11X.BackColor = labA11Y.BackColor =
               labA12X.BackColor = labA12Y.BackColor =
               labA13X.BackColor = labA13Y.BackColor = Color.White;
            }));

        }
        /// <summary>
        /// 显示产品型号
        /// </summary>
        private void Showtype(string typename)
        {

        }

        #endregion
        #endregion

        #region 当参数设置界面用时的保存和加载函数
        /// <summary>
        /// 保存高度参数 0 插口  1内部
        /// </summary>
        public void Save_High(int flag)
        {
            ///0 插口  1内部
            int a = Environment.TickCount;
            //基准值
            C_HIGH.BaseHigh = Convert.ToDouble(label7.Text);
            C_HIGH.BaseHigh1 = Convert.ToDouble(label2.Text);
            //补偿值
            C_HIGH.Compensate1H = Convert.ToDouble(labA1X.Text);
            C_HIGH.Compensate2H = Convert.ToDouble(labA1Y.Text);
            C_HIGH.Compensate3H = Convert.ToDouble(labA2X.Text);
            C_HIGH.Compensate4H = Convert.ToDouble(labA2Y.Text);
            C_HIGH.Compensate5H = Convert.ToDouble(labA3X.Text);
            C_HIGH.Compensate6H = Convert.ToDouble(labA3Y.Text);

            //理论值 
            C_HIGH.Theory1H = Convert.ToDouble(labLX1.Text);
            C_HIGH.Theory2H = Convert.ToDouble(labLY1.Text);
            C_HIGH.Theory3H = Convert.ToDouble(labLX2.Text);
            C_HIGH.Theory4H = Convert.ToDouble(labLY2.Text);
            C_HIGH.Theory5H = Convert.ToDouble(labLX3.Text);
            C_HIGH.Theory6H = Convert.ToDouble(labLY3.Text);

            //上限值labHX1
            C_HIGH.Up1H = Convert.ToDouble(labHX1.Text);
            C_HIGH.Up2H = Convert.ToDouble(labHY1.Text);
            C_HIGH.Up3H = Convert.ToDouble(labHX2.Text);
            C_HIGH.Up4H = Convert.ToDouble(labHY2.Text);
            C_HIGH.Up5H = Convert.ToDouble(labHX3.Text);
            C_HIGH.Up6H = Convert.ToDouble(labHY3.Text);

            //下限值
            C_HIGH.Down1H = Convert.ToDouble(labDX1.Text);
            C_HIGH.Down2H = Convert.ToDouble(labDY1.Text);
            C_HIGH.Down3H = Convert.ToDouble(labDX2.Text);
            C_HIGH.Down4H = Convert.ToDouble(labDY2.Text);
            C_HIGH.Down5H = Convert.ToDouble(labDX3.Text);
            C_HIGH.Down6H = Convert.ToDouble(labDY3.Text);

            C_HIGH.SaveParmHigh(label4.Text.Trim(), flag);
            a = Environment.TickCount - a;
            MessageBox.Show($"保存成功，耗时{a}ms");
        }
        /// <summary>
        /// 加载高度参数0 插口 1内部
        /// </summary>
        public void Load_High(int flag)
        {
            if (label4.Text == null || label4.Text == "") label4.Text = SysStatus.CurProductName;

            int a = Environment.TickCount;

            C_HIGH.LoadParmHigh(label4.Text.Trim(), flag);
            //插口PIN基准  PCB pin基准
            label7.Text = C_HIGH.BaseHigh.ToString();
            label2.Text = C_HIGH.BaseHigh1.ToString();
            //补偿值 
            labA1X.Text = C_HIGH.Compensate1H.ToString();
            labA1Y.Text = C_HIGH.Compensate2H.ToString();
            labA2X.Text = C_HIGH.Compensate3H.ToString();
            labA2Y.Text = C_HIGH.Compensate4H.ToString();
            labA3X.Text = C_HIGH.Compensate5H.ToString();
            labA3Y.Text = C_HIGH.Compensate6H.ToString();
            //   labA4X.Text = C_HIGH.Compensate7H.ToString();
            //   labA4Y.Text = C_HIGH.Compensate8H.ToString();
            //    labA5X.Text = C_HIGH.Compensate9H.ToString();
            //    labA5Y.Text = C_HIGH.Compensate10H.ToString();
            //    labA6X.Text = C_HIGH.Compensate11H.ToString();
            //    labA6Y.Text = C_HIGH.Compensate12H.ToString();
            //    labA7X.Text = C_HIGH.Compensate13H.ToString();

            //理论值 
            labLX1.Text = C_HIGH.Theory1H.ToString();
            labLY1.Text = C_HIGH.Theory2H.ToString();
            labLX2.Text = C_HIGH.Theory3H.ToString();
            labLY2.Text = C_HIGH.Theory4H.ToString();
            labLX3.Text = C_HIGH.Theory5H.ToString();
            labLY3.Text = C_HIGH.Theory6H.ToString();
            //  labLX4.Text = C_HIGH.Theory7H.ToString();
            //  labLY4.Text = C_HIGH.Theory8H.ToString();
            //  labLX5.Text = C_HIGH.Theory9H.ToString();
            //  labLY5.Text = C_HIGH.Theory10H.ToString();
            //   labLX6.Text = C_HIGH.Theory11H.ToString();
            //   labLY6.Text = C_HIGH.Theory12H.ToString();
            //   labLX7.Text = C_HIGH.Theory13H.ToString();

            //上限值labHX1
            labHX1.Text = C_HIGH.Up1H.ToString();
            labHY1.Text = C_HIGH.Up2H.ToString();
            labHX2.Text = C_HIGH.Up3H.ToString();
            labHY2.Text = C_HIGH.Up4H.ToString();
            labHX3.Text = C_HIGH.Up5H.ToString();
            labHY3.Text = C_HIGH.Up6H.ToString();
            //   labHX4.Text = C_HIGH.Up7H.ToString();
            //   labHY4.Text = C_HIGH.Up8H.ToString();
            //   labHX5.Text = C_HIGH.Up9H.ToString();
            //   labHY5.Text = C_HIGH.Up10H.ToString();
            //   labHX6.Text = C_HIGH.Up11H.ToString();
            //   labHY6.Text = C_HIGH.Up12H.ToString();
            //    labHX7.Text = C_HIGH.Up13H.ToString();

            //下限值
            labDX1.Text = C_HIGH.Down1H.ToString();
            labDY1.Text = C_HIGH.Down2H.ToString();
            labDX2.Text = C_HIGH.Down3H.ToString();
            labDY2.Text = C_HIGH.Down4H.ToString();
            labDX3.Text = C_HIGH.Down5H.ToString();
            labDY3.Text = C_HIGH.Down6H.ToString();
            //    labDX4.Text = C_HIGH.Down7H.ToString();
            //    labDY4.Text = C_HIGH.Down8H.ToString();
            //    labDX5.Text = C_HIGH.Down9H.ToString();
            //     labDY5.Text = C_HIGH.Down10H.ToString();
            //    labDX6.Text = C_HIGH.Down11H.ToString();
            //   labDY6.Text = C_HIGH.Down12H.ToString();
            //    labDX7.Text = C_HIGH.Down13H.ToString();

            a = Environment.TickCount - a;
            //   MessageBox.Show($"加载成功，耗时{a}ms");
        }
        /// <summary>
        /// 保存位置度参数0 插口 1内部
        /// </summary>
        public void Save_Postion(int flag)
        {
            int a = Environment.TickCount;
            //补偿值
            C_POS.CompensateX1 = Convert.ToDouble(labA1X.Text);
            C_POS.CompensateY1 = Convert.ToDouble(labA1Y.Text);
            C_POS.CompensateX2 = Convert.ToDouble(labA2X.Text);
            C_POS.CompensateY2 = Convert.ToDouble(labA2Y.Text);
            C_POS.CompensateX3 = Convert.ToDouble(labA3X.Text);
            C_POS.CompensateY3 = Convert.ToDouble(labA3Y.Text);
            C_POS.CompensateX4 = Convert.ToDouble(labA4X.Text);
            C_POS.CompensateY4 = Convert.ToDouble(labA4Y.Text);
            C_POS.CompensateX5 = Convert.ToDouble(labA5X.Text);
            C_POS.CompensateY5 = Convert.ToDouble(labA5Y.Text);
            C_POS.CompensateX6 = Convert.ToDouble(labA6X.Text);
            C_POS.CompensateY6 = Convert.ToDouble(labA6Y.Text);

            //理论值 
            C_POS.Theory1X = Convert.ToDouble(labLX1.Text);
            C_POS.Theory1Y = Convert.ToDouble(labLY1.Text);
            C_POS.Theory2X = Convert.ToDouble(labLX2.Text);
            C_POS.Theory2Y = Convert.ToDouble(labLY2.Text);
            C_POS.Theory3X = Convert.ToDouble(labLX3.Text);
            C_POS.Theory3Y = Convert.ToDouble(labLY3.Text);
            C_POS.Theory4X = Convert.ToDouble(labLX4.Text);
            C_POS.Theory4Y = Convert.ToDouble(labLY4.Text);
            C_POS.Theory5X = Convert.ToDouble(labLX5.Text);
            C_POS.Theory5Y = Convert.ToDouble(labLY5.Text);
            C_POS.Theory6X = Convert.ToDouble(labLX6.Text);
            C_POS.Theory6Y = Convert.ToDouble(labLY6.Text);

            //上限值labHX1
            C_POS.Up1X = Convert.ToDouble(labHX1.Text);
            C_POS.Up1Y = Convert.ToDouble(labHY1.Text);
            C_POS.Up2X = Convert.ToDouble(labHX2.Text);
            C_POS.Up2Y = Convert.ToDouble(labHY2.Text);
            C_POS.Up3X = Convert.ToDouble(labHX3.Text);
            C_POS.Up3Y = Convert.ToDouble(labHY3.Text);
            C_POS.Up4X = Convert.ToDouble(labHX4.Text);
            C_POS.Up4Y = Convert.ToDouble(labHY4.Text);
            C_POS.Up5X = Convert.ToDouble(labHX5.Text);
            C_POS.Up5Y = Convert.ToDouble(labHY5.Text);
            C_POS.Up6X = Convert.ToDouble(labHX6.Text);
            C_POS.Up6Y = Convert.ToDouble(labHY6.Text);

            //下限值
            C_POS.Down1X = Convert.ToDouble(labDX1.Text);
            C_POS.Down1Y = Convert.ToDouble(labDY1.Text);
            C_POS.Down2X = Convert.ToDouble(labDX2.Text);
            C_POS.Down2Y = Convert.ToDouble(labDY2.Text);
            C_POS.Down3X = Convert.ToDouble(labDX3.Text);
            C_POS.Down3Y = Convert.ToDouble(labDY3.Text);
            C_POS.Down4X = Convert.ToDouble(labDX4.Text);
            C_POS.Down4Y = Convert.ToDouble(labDY4.Text);
            C_POS.Down5X = Convert.ToDouble(labDX5.Text);
            C_POS.Down5Y = Convert.ToDouble(labDY5.Text);
            C_POS.Down6X = Convert.ToDouble(labDX6.Text);
            C_POS.Down6Y = Convert.ToDouble(labDY6.Text);

            C_POS.SaveParmPostion(label4.Text.Trim(), flag);
            a = Environment.TickCount - a;
            MessageBox.Show($"保存成功，耗时{a}ms");
        }
        /// <summary>
        /// 加载位置度参数0插口 1内部
        /// </summary>
        public void Load_Postion(int flag)
        {
            if (label4.Text == null || label4.Text == "") label4.Text = SysStatus.CurProductName;
            int a = Environment.TickCount;
            C_POS.LoadParmPostion(label4.Text.Trim(), flag);
            //补偿值
            labA1X.Text = C_POS.CompensateX1.ToString();
            labA1Y.Text = C_POS.CompensateY1.ToString();
            labA2X.Text = C_POS.CompensateX2.ToString();
            labA2Y.Text = C_POS.CompensateY2.ToString();
            labA3X.Text = C_POS.CompensateX3.ToString();
            labA3Y.Text = C_POS.CompensateY3.ToString();
            labA4X.Text = C_POS.CompensateX4.ToString();
            labA4Y.Text = C_POS.CompensateY4.ToString();
            labA5X.Text = C_POS.CompensateX5.ToString();
            labA5Y.Text = C_POS.CompensateY5.ToString();
            labA6X.Text = C_POS.CompensateX6.ToString();
            labA6Y.Text = C_POS.CompensateY6.ToString();

            //理论值 
            labLX1.Text = C_POS.Theory1X.ToString();
            labLY1.Text = C_POS.Theory1Y.ToString();
            labLX2.Text = C_POS.Theory2X.ToString();
            labLY2.Text = C_POS.Theory2Y.ToString();
            labLX3.Text = C_POS.Theory3X.ToString();
            labLY3.Text = C_POS.Theory3Y.ToString();
            labLX4.Text = C_POS.Theory4X.ToString();
            labLY4.Text = C_POS.Theory4Y.ToString();
            labLX5.Text = C_POS.Theory5X.ToString();
            labLY5.Text = C_POS.Theory5Y.ToString();
            labLX6.Text = C_POS.Theory6X.ToString();
            labLY6.Text = C_POS.Theory6Y.ToString();

            //上限值labHX1
            labHX1.Text = C_POS.Up1X.ToString();
            labHY1.Text = C_POS.Up1Y.ToString();
            labHX2.Text = C_POS.Up2X.ToString();
            labHY2.Text = C_POS.Up2Y.ToString();
            labHX3.Text = C_POS.Up3X.ToString();
            labHY3.Text = C_POS.Up3Y.ToString();
            labHX4.Text = C_POS.Up4X.ToString();
            labHY4.Text = C_POS.Up4Y.ToString();
            labHX5.Text = C_POS.Up5X.ToString();
            labHY5.Text = C_POS.Up5Y.ToString();
            labHX6.Text = C_POS.Up6X.ToString();
            labHY6.Text = C_POS.Up6Y.ToString();

            //下限值
            labDX1.Text = C_POS.Down1X.ToString();
            labDY1.Text = C_POS.Down1Y.ToString();
            labDX2.Text = C_POS.Down2X.ToString();
            labDY2.Text = C_POS.Down2Y.ToString();
            labDX3.Text = C_POS.Down3X.ToString();
            labDY3.Text = C_POS.Down3Y.ToString();
            labDX4.Text = C_POS.Down4X.ToString();
            labDY4.Text = C_POS.Down4Y.ToString();
            labDX5.Text = C_POS.Down5X.ToString();
            labDY5.Text = C_POS.Down5Y.ToString();
            labDX6.Text = C_POS.Down6X.ToString();
            labDY6.Text = C_POS.Down6Y.ToString();

            a = Environment.TickCount - a;
            //   MessageBox.Show($"加载成功，耗时{a}ms");
        }
        #endregion


        #region 自定义属性

        #region 定义Pin 名称
        private string label3text = "模号";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("模号名称")]
        public string label3text1
        {
            get { return label3text; }
            set
            {
                this.label3.Text = label3text = value;
            }
        }
        private bool textenb = false;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("二维码叫法名称")]
        public bool TextEnb
        {
            get { return textenb; }
            set
            {
                textenb = value;
            }
        }
        private string CORDNAME = "二维码";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("二维码叫法名称")]
        public string CORDNAME1
        {
            get { return CORDNAME; }
            set
            {
                this.label1.Text = CORDNAME = value;
            }
        }
        private string Pin1X = "Pin1X";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P1X名称")]
        public string P1XText
        {
            get { return Pin1X; }
            set
            {
                this.label13.Text = Pin1X = value;
            }
        }
        private string Pin1Y = "Pin1Y";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P1Y名称")]
        public string P1YText
        {
            get { return Pin1Y; }
            set
            {
                this.label14.Text = Pin1Y = value;
            }
        }

        private string Pin2X = "Pin2X";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P2X名称")]
        public string P2XText
        {
            get { return Pin2X; }
            set
            {
                this.label15.Text = Pin2X = value;
            }
        }
        private string Pin2Y = "Pin2Y";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P2Y名称")]
        public string P2YText
        {
            get { return Pin2Y; }
            set
            {
                this.label16.Text = Pin2Y = value;
            }
        }

        private string Pin3X = "Pin3X";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P3X名称")]
        public string P3XText
        {
            get { return Pin3X; }
            set
            {
                this.label17.Text = Pin3X = value;
            }
        }
        private string Pin3Y = "Pin3Y";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P3Y名称")]
        public string P3YText
        {
            get { return Pin3Y; }
            set
            {
                this.label18.Text = Pin3Y = value;
            }
        }

        private string Pin4X = "Pin4X";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P4X名称")]
        public string P4XText
        {
            get { return Pin4X; }
            set
            {
                this.label19.Text = Pin4X = value;
            }
        }
        private string Pin4Y = "Pin4Y";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P4Y名称")]
        public string P4YText
        {
            get { return Pin4Y; }
            set
            {
                this.label20.Text = Pin4Y = value;
            }
        }


        private string Pin5X = "Pin5X";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P5X名称")]
        public string P5XText
        {
            get { return Pin5X; }
            set
            {
                this.label21.Text = Pin5X = value;
            }
        }
        private string Pin5Y = "Pin5Y";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P5Y名称")]
        public string P5YText
        {
            get { return Pin5Y; }
            set
            {
                this.label22.Text = Pin5Y = value;
            }
        }
        private string Pin6X = "Pin6X";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P6X名称")]
        public string P6XText
        {
            get { return Pin6X; }
            set
            {
                this.label23.Text = Pin6X = value;
            }
        }
        private string Pin6Y = "Pin6Y";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P6Y名称")]
        public string P6YText
        {
            get { return Pin6Y; }
            set
            {
                this.label24.Text = Pin6Y = value;
            }
        }

        private string Pin7X = "Pin7X";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P7X名称")]
        public string P7XText
        {
            get { return Pin7X; }
            set
            {
                this.label25.Text = Pin7X = value;
            }
        }
        private string Pin7Y = "Pin7Y";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P7Y名称")]
        public string P7YText
        {
            get { return Pin7Y; }
            set
            {
                this.label26.Text = Pin7Y = value;
            }
        }

        private string Pin8X = "Pin8X";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P8X名称")]
        public string P8XText
        {
            get { return Pin8X; }
            set
            {
                this.label27.Text = Pin8X = value;
            }
        }
        private string Pin8Y = "Pin8Y";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P8Y名称")]
        public string P8YText
        {
            get { return Pin8Y; }
            set
            {
                this.label28.Text = Pin8Y = value;
            }
        }

        private string Pin9X = "Pin9X";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P9X名称")]
        public string P9XText
        {
            get { return Pin9X; }
            set
            {
                this.label29.Text = Pin9X = value;
            }
        }
        private string Pin9Y = "Pin9Y";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P9Y名称")]
        public string P9YText
        {
            get { return Pin9Y; }
            set
            {
                this.label30.Text = Pin9Y = value;
            }
        }

        private string Pin10X = "Pin10X";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P10X名称")]
        public string P10XText
        {
            get { return Pin10X; }
            set
            {
                this.label31.Text = Pin10X = value;
            }
        }
        private string Pin10Y = "Pin10Y";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P10Y名称")]
        public string P10YText
        {
            get { return Pin10Y; }
            set
            {
                this.label32.Text = Pin10Y = value;
            }
        }

        private string Pin11X = "Pin11X";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P11X名称")]
        public string P11XText
        {
            get { return Pin11X; }
            set
            {
                this.label33.Text = Pin11X = value;
            }
        }
        private string Pin11Y = "Pin11Y";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P11Y名称")]
        public string P11YText
        {
            get { return Pin11Y; }
            set
            {
                this.label34.Text = Pin11Y = value;
            }
        }

        private string Pin12X = "Pin12X";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P12X名称")]
        public string P12XText
        {
            get { return Pin12X; }
            set
            {
                this.label35.Text = Pin12X = value;
            }
        }
        private string Pin12Y = "Pin12Y";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P12Y名称")]
        public string P12YText
        {
            get { return Pin12Y; }
            set
            {
                this.label36.Text = Pin12Y = value;
            }
        }


        private string Pin13X = "Pin13X";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P13X名称")]
        public string P13XText
        {
            get { return Pin13X; }
            set
            {
                this.label37.Text = Pin13X = value;
            }
        }
        private string Pin13Y = "Pin13Y";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("P13Y名称")]
        public string P13YText
        {
            get { return Pin13Y; }
            set
            {
                this.label38.Text = Pin13Y = value;
            }
        }


        //label9

        private string label9na = "实测值";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("实测值label")]
        public string label9Text
        {
            get { return label9na; }
            set
            {
                this.label9.Text = label9na = value;
            }
        }
        //label6

        private string label6na = "结果";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("结果label")]
        public string label6Text
        {
            get { return label6na; }
            set
            {
                this.label6.Text = label6na = value;
            }
        }




        #endregion

        #region 行数定制

        private int pinCount = 0;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("测试PIN脚数量")]
        public int PinCount
        {
            get {
                pinCount = tableLayoutPanel1.RowCount-3;
                return pinCount; }
            set
            {
                if (value > 38 || value < 1)
                {
                    return;
                }
                pinCount = value;
                this.tableLayoutPanel1.RowCount = pinCount + 3;
                switch (pinCount)
                {

                    case 2:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = false;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = false;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = false;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = false;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = false;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = false;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = false;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = false;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = false;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = false;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = false;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = false;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = false;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = false;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = false;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = false;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = false;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = false;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = false;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = false;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = false;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = false;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = false;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;

                        break;
                    case 3:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = false;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = false;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = false;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = false;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = false;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = false;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = false;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = false;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = false;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = false;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = false;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = false;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = false;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = false;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = false;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = false;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = false;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = false;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = false;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = false;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = false;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = false;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;
                        break;
                    case 4:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = false;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = false;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = false;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = false;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = false;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = false;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = false;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = false;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = false;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = false;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = false;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = false;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = false;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = false;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = false;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = false;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = false;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = false;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = false;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = false;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = false;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;
                        break;
                    case 5:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = true;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = false;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = false;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = false;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = false;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = false;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = false;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = false;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = false;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = false;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = false;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = false;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = false;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = false;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = false;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = false;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = false;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = false;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = false;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = false;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = false;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;
                        break;
                    case 6:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = true;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = true;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = false;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = false;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = false;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = false;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = false;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = false;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = false;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = false;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = false;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = false;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = false;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = false;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = false;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = false;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = false;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = false;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = false;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = false;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = false;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;
                        break;
                    case 7:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = true;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = true;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = true;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = false;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = false;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = false;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = false;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = false;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = false;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = false;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = false;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = false;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = false;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = false;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = false;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = false;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = false;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = false;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = false;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = false;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = false;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;
                        break;
                    case 8:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = true;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = true;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = true;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = true;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = false;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = false;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = false;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = false;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = false;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = false;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = false;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = false;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = false;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = false;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = false;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = false;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = false;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = false;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = false;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = false;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = false;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;
                        break;
                    case 9:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = true;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = true;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = true;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = true;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = true;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = false;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = false;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = false;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = false;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = false;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = false;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = false;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = false;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = false;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = false;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = false;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = false;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = false;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = false;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = false;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = false;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;
                        break;
                    case 10:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = true;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = true;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = true;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = true;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = true;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = true;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = false;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = false;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = false;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = false;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = false;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = false;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = false;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = false;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = false;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = false;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = false;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = false;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = false;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = false;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = false;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;
                        break;
                    case 11:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = true;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = true;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = true;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = true;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = true;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = true;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = true;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = false;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = false;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = false;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = false;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = false;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = false;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = false;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = false;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = false;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = false;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = false;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = false;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = false;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = false;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;
                        break;
                    case 12:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = true;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = true;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = true;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = true;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = true;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = true;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = true;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = true;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = false;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = false;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = false;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = false;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = false;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = false;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = false;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = false;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = false;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = false;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = false;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = false;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = false;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;
                        break;
                    case 13:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = true;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = true;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = true;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = true;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = true;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = true;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = true;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = true;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = true;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = false;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = false;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = false;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = false;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = false;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = false;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = false;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = false;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = false;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = false;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = false;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = false;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;
                        break;
                    case 14:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = true;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = true;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = true;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = true;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = true;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = true;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = true;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = true;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = true;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = true;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = false;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = false;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = false;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = false;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = false;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = false;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = false;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = false;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = false;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = false;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = false;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;
                        break;
                    case 15:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = true;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = true;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = true;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = true;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = true;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = true;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = true;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = true;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = true;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = true;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = true;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = false;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = false;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = false;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = false;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = false;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = false;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = false;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = false;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = false;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = false;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;
                        break;
                    case 16:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = true;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = true;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = true;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = true;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = true;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = true;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = true;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = true;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = true;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = true;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = true;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = true;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = false;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = false;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = false;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = false;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = false;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = false;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = false;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = false;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = false;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;
                        break;
                    case 17:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = true;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = true;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = true;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = true;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = true;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = true;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = true;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = true;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = true;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = true;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = true;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = true;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = true;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = false;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = false;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = false;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = false;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = false;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = false;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = false;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = false;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;
                        break;
                    case 18:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = true;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = true;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = true;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = true;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = true;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = true;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = true;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = true;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = true;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = true;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = true;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = true;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = true;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = true;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = false;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = false;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = false;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = false;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = false;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = false;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = false;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;
                        break;
                    case 19:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = true;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = true;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = true;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = true;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = true;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = true;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = true;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = true;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = true;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = true;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = true;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = true;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = true;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = true;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = true;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = false;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = false;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = false;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = false;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = false;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = false;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;
                        break;
                    case 20:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = true;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = true;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = true;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = true;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = true;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = true;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = true;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = true;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = true;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = true;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = true;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = true;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = true;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = true;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = true;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = true;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = false;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = false;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = false;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = false;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = false;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;
                        break;
                    case 21:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = true;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = true;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = true;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = true;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = true;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = true;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = true;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = true;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = true;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = true;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = true;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = true;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = true;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = true;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = true;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = true;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = true;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = false;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = false;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = false;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = false;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;
                        break;
                    case 22:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = true;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = true;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = true;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = true;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = true;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = true;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = true;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = true;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = true;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = true;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = true;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = true;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = true;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = true;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = true;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = true;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = true;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = true;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = false;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = false;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = false;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;
                        break;
                    case 23:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = true;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = true;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = true;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = true;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = true;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = true;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = true;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = true;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = true;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = true;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = true;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = true;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = true;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = true;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = true;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = true;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = true;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = true;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = true;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = false;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = false;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;
                        break;
                    case 24:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = true;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = true;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = true;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = true;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = true;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = true;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = true;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = true;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = true;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = true;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = true;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = true;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = true;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = true;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = true;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = true;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = true;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = true;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = true;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = true;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = false;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;
                        break;
                    case 25:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = true;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = true;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = true;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = true;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = true;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = true;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = true;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = true;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = true;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = true;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = true;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = true;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = true;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = true;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = true;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = true;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = true;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = true;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = true;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = true;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = true;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = false;
                        break;
                    case 26:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = true;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = true;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = true;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = true;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = true;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = true;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = true;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = true;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = true;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = true;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = true;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = true;
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = true;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = true;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = true;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = true;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = true;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = true;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = true;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = true;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = true;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = true;
                        break;
                    case 27:
                        label13.Visible = labA1X.Visible = labLX1.Visible = labHX1.Visible = labDX1.Visible = true;
                        label14.Visible = labA1Y.Visible = labLY1.Visible = labHY1.Visible = labDY1.Visible = true;
                        label15.Visible = labA2X.Visible = labLX2.Visible = labHX2.Visible = labDX2.Visible = true;
                        label16.Visible = labA2Y.Visible = labLY2.Visible = labHY2.Visible = labDY2.Visible = true;
                        label17.Visible = labA3X.Visible = labLX3.Visible = labHX3.Visible = labDX3.Visible = true;
                        label18.Visible = labA3Y.Visible = labLY3.Visible = labHY3.Visible = labDY3.Visible = true;
                        label19.Visible = labA4X.Visible = labLX4.Visible = labHX4.Visible = labDX4.Visible = true;
                        label20.Visible = labA4Y.Visible = labLY4.Visible = labHY4.Visible = labDY4.Visible = true;
                        label21.Visible = labA5X.Visible = labLX5.Visible = labHX5.Visible = labDX5.Visible = true;
                        label22.Visible = labA5Y.Visible = labLY5.Visible = labHY5.Visible = labDY5.Visible = true;
                        label23.Visible = labA6X.Visible = labLX6.Visible = labHX6.Visible = labDX6.Visible = true;
                        label24.Visible = labA6Y.Visible = labLY6.Visible = labHY6.Visible = labDY6.Visible = true;
                        label25.Visible = labA7X.Visible = labLX7.Visible = labHX7.Visible = labDX7.Visible = true;
                        label26.Visible = labA7Y.Visible = labLY7.Visible = labHY7.Visible = labDY7.Visible = true;
                        label27.Visible = labA8X.Visible = labLX8.Visible = labHX8.Visible = labDX8.Visible = true;
                        label28.Visible = labA8Y.Visible = labLY8.Visible = labHY8.Visible = labDY8.Visible = true;  
                        label29.Visible = labA9X.Visible = labLX9.Visible = labHX9.Visible = labDX9.Visible = true;
                        label30.Visible = labA9Y.Visible = labLY9.Visible = labHY9.Visible = labDY9.Visible = true;
                        label31.Visible = labA10X.Visible = labLX10.Visible = labHX10.Visible = labDX10.Visible = true;
                        label32.Visible = labA10Y.Visible = labLY10.Visible = labHY10.Visible = labDY10.Visible = true;
                        label33.Visible = labA11X.Visible = labLX11.Visible = labHX11.Visible = labDX11.Visible = true;
                        label34.Visible = labA11Y.Visible = labLY11.Visible = labHY11.Visible = labDY11.Visible = true;
                        label35.Visible = labA12X.Visible = labLX12.Visible = labHX12.Visible = labDX12.Visible = true;
                        label36.Visible = labA12Y.Visible = labLY12.Visible = labHY12.Visible = labDY12.Visible = true;
                        label37.Visible = labA13X.Visible = labLX13.Visible = labHX13.Visible = labDX13.Visible = true;
                        label38.Visible = labA13Y.Visible = labLY13.Visible = labHY13.Visible = labDY13.Visible = true;
                        break;
                    default:

                        break;
                }
            }
        }



        #endregion
        #endregion

     
        private void labA1X_Click(object sender, EventArgs e)//点击即可输出数字
        {
          if(!TextEnb)  NumPad.Show((Label)sender);   
                
        }
    }
}
