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
using MyLib.Sys;
using static CXPro001.myclass.RunMany;

namespace CXPro001.ShowControl
{
    /// <summary>
    /// 耐压仪8740 检测显示控件
    /// </summary>
    public partial class TS8740SshowCtr : UserControl
    {
        public TS8740SshowCtr()
        {
            InitializeComponent();
        }
        # region 自定义属性

        private string SHORTOPEN = "短/断路";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("短 / 断路名称")]
        public string label13Text
        {
            get { return SHORTOPEN; }
            set
            {
                this.label13.Text = SHORTOPEN = value;
            }
        }
        private string SHORTOPENVA = "导通阻抗";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("导通阻抗名称")]
        public string label17Text
        {
            get { return SHORTOPENVA; }
            set
            {
                this.label17.Text = SHORTOPENVA = value;
            }
        }
        private string VOLVA = "耐压值";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("耐压名称")]
        public string label4Text
        {
            get { return VOLVA; }
            set
            {
                this.label14.Text = VOLVA = value;
            }
        }
        private string labvol = "AC700V:50Hz";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("测试电压")]
        public string labvolText
        {
            get { return labvol; }
            set
            {
                labVol1.Text = labVol2.Text = labVol3.Text = labVol4.Text = labVol5.Text = labVol6.Text = labVol7.Text = labVol8.Text = labVol9.Text= labVol10.Text= labVol11.Text = labVol12.Text = labvol = value;
            }
        }
        private string labtimes = "2S";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("测试时间")]
        public string latimesText
        {
            get { return labtimes; }
            set
            {
                times1.Text = times2.Text = times3.Text = times4.Text = times5.Text = times6.Text = times7.Text = times8.Text = times9.Text = times10.Text = times11.Text = times12.Text = labtimes = value;
            }
        }
        private string p1text = "二维码";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("p1text名称")]
        public string P1Text
        {
            get { return p1text; }
            set
            {
                this.label4.Text = p1text = value;

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
                if (value > 12 || value < 0)
                {
                    return;
                }
                pinCount = value;
                this.tableLayout1.RowCount = pinCount + 3;
                switch (pinCount)
                {
                    case 2:
                        times1.Visible = lblName1.Visible = OS1.Visible = labVol1.Visible = labValue1.Visible = labRes1.Visible = true;
                        times2.Visible = lblName2.Visible = OS2.Visible = labVol2.Visible = labValue2.Visible = labRes2.Visible = true;
                        times3.Visible = lblName3.Visible = OS3.Visible = labVol3.Visible = labValue3.Visible = labRes3.Visible = false;
                        times4.Visible = lblName4.Visible = OS4.Visible = labVol4.Visible = labValue4.Visible = labRes4.Visible = false;
                        times5.Visible = lblName5.Visible = OS5.Visible = labVol5.Visible = labValue5.Visible = labRes5.Visible = false;
                        times6.Visible = lblName6.Visible = OS6.Visible = labVol6.Visible = labValue6.Visible = labRes6.Visible = false;
                        times7.Visible = lblName7.Visible = OS7.Visible = labVol7.Visible = labValue7.Visible = labRes7.Visible = false;
                        times8.Visible = lblName8.Visible = OS8.Visible = labVol8.Visible = labValue8.Visible = labRes8.Visible = false;
                        times9.Visible = lblName9.Visible = OS9.Visible = labVol9.Visible = labValue9.Visible = labRes9.Visible = false;
                        times10.Visible = lblName10.Visible = OS10.Visible = labVol10.Visible = labValue10.Visible = labRes10.Visible = false;
                        times11.Visible = lblName11.Visible = OS11.Visible = labVol11.Visible = labValue11.Visible = labRes11.Visible = false;
                        times12.Visible = lblName12.Visible = OS12.Visible = labVol12.Visible = labValue12.Visible = labRes12.Visible = false;

                        break;
                    case 3:
                        times1.Visible = lblName1.Visible = OS1.Visible = labVol1.Visible = labValue1.Visible = labRes1.Visible = true;
                        times2.Visible = lblName2.Visible = OS2.Visible = labVol2.Visible = labValue2.Visible = labRes2.Visible = true;
                        times3.Visible = lblName3.Visible = OS3.Visible = labVol3.Visible = labValue3.Visible = labRes3.Visible = true;
                        times4.Visible = lblName4.Visible = OS4.Visible = labVol4.Visible = labValue4.Visible = labRes4.Visible = false;
                        times5.Visible = lblName5.Visible = OS5.Visible = labVol5.Visible = labValue5.Visible = labRes5.Visible = false;
                        times6.Visible = lblName6.Visible = OS6.Visible = labVol6.Visible = labValue6.Visible = labRes6.Visible = false;
                        times7.Visible = lblName7.Visible = OS7.Visible = labVol7.Visible = labValue7.Visible = labRes7.Visible = false;
                        times8.Visible = lblName8.Visible = OS8.Visible = labVol8.Visible = labValue8.Visible = labRes8.Visible = false;
                        times9.Visible = lblName9.Visible = OS9.Visible = labVol9.Visible = labValue9.Visible = labRes9.Visible = false;
                        times10.Visible = lblName10.Visible = OS10.Visible = labVol10.Visible = labValue10.Visible = labRes10.Visible = false;
                        times11.Visible = lblName11.Visible = OS11.Visible = labVol11.Visible = labValue11.Visible = labRes11.Visible = false;
                        times12.Visible = lblName12.Visible = OS12.Visible = labVol12.Visible = labValue12.Visible = labRes12.Visible = false;
                        break;
                    case 4:
                        times1.Visible = lblName1.Visible = OS1.Visible = labVol1.Visible = labValue1.Visible = labRes1.Visible = true;
                        times2.Visible = lblName2.Visible = OS2.Visible = labVol2.Visible = labValue2.Visible = labRes2.Visible = true;
                        times3.Visible = lblName3.Visible = OS3.Visible = labVol3.Visible = labValue3.Visible = labRes3.Visible = true;
                        times4.Visible = lblName4.Visible = OS4.Visible = labVol4.Visible = labValue4.Visible = labRes4.Visible = true;
                        times5.Visible = lblName5.Visible = OS5.Visible = labVol5.Visible = labValue5.Visible = labRes5.Visible = false;
                        times6.Visible = lblName6.Visible = OS6.Visible = labVol6.Visible = labValue6.Visible = labRes6.Visible = false;
                        times7.Visible = lblName7.Visible = OS7.Visible = labVol7.Visible = labValue7.Visible = labRes7.Visible = false;
                        times8.Visible = lblName8.Visible = OS8.Visible = labVol8.Visible = labValue8.Visible = labRes8.Visible = false;
                        times9.Visible = lblName9.Visible = OS9.Visible = labVol9.Visible = labValue9.Visible = labRes9.Visible = false;
                        times10.Visible = lblName10.Visible = OS10.Visible = labVol10.Visible = labValue10.Visible = labRes10.Visible = false;
                        times11.Visible = lblName11.Visible = OS11.Visible = labVol11.Visible = labValue11.Visible = labRes11.Visible = false;
                        times12.Visible = lblName12.Visible = OS12.Visible = labVol12.Visible = labValue12.Visible = labRes12.Visible = false;
                        break;
                    case 5:
                        times1.Visible = lblName1.Visible = OS1.Visible = labVol1.Visible = labValue1.Visible = labRes1.Visible = true;
                        times2.Visible = lblName2.Visible = OS2.Visible = labVol2.Visible = labValue2.Visible = labRes2.Visible = true;
                        times3.Visible = lblName3.Visible = OS3.Visible = labVol3.Visible = labValue3.Visible = labRes3.Visible = true;
                        times4.Visible = lblName4.Visible = OS4.Visible = labVol4.Visible = labValue4.Visible = labRes4.Visible = true;
                        times5.Visible = lblName5.Visible = OS5.Visible = labVol5.Visible = labValue5.Visible = labRes5.Visible = true;
                        times6.Visible = lblName6.Visible = OS6.Visible = labVol6.Visible = labValue6.Visible = labRes6.Visible = false;
                        times7.Visible = lblName7.Visible = OS7.Visible = labVol7.Visible = labValue7.Visible = labRes7.Visible = false;
                        times8.Visible = lblName8.Visible = OS8.Visible = labVol8.Visible = labValue8.Visible = labRes8.Visible = false;
                        times9.Visible = lblName9.Visible = OS9.Visible = labVol9.Visible = labValue9.Visible = labRes9.Visible = false;
                        times10.Visible = lblName10.Visible = OS10.Visible = labVol10.Visible = labValue10.Visible = labRes10.Visible = false;
                        times11.Visible = lblName11.Visible = OS11.Visible = labVol11.Visible = labValue11.Visible = labRes11.Visible = false;
                        times12.Visible = lblName12.Visible = OS12.Visible = labVol12.Visible = labValue12.Visible = labRes12.Visible = false;
                        break;
                    case 6:
                        times1.Visible = lblName1.Visible = OS1.Visible = labVol1.Visible = labValue1.Visible = labRes1.Visible = true;
                        times2.Visible = lblName2.Visible = OS2.Visible = labVol2.Visible = labValue2.Visible = labRes2.Visible = true;
                        times3.Visible = lblName3.Visible = OS3.Visible = labVol3.Visible = labValue3.Visible = labRes3.Visible = true;
                        times4.Visible = lblName4.Visible = OS4.Visible = labVol4.Visible = labValue4.Visible = labRes4.Visible = true;
                        times5.Visible = lblName5.Visible = OS5.Visible = labVol5.Visible = labValue5.Visible = labRes5.Visible = true;
                        times6.Visible = lblName6.Visible = OS6.Visible = labVol6.Visible = labValue6.Visible = labRes6.Visible = true;
                        times7.Visible = lblName7.Visible = OS7.Visible = labVol7.Visible = labValue7.Visible = labRes7.Visible = false;
                        times8.Visible = lblName8.Visible = OS8.Visible = labVol8.Visible = labValue8.Visible = labRes8.Visible = false;
                        times9.Visible = lblName9.Visible = OS9.Visible = labVol9.Visible = labValue9.Visible = labRes9.Visible = false;
                        times10.Visible = lblName10.Visible = OS10.Visible = labVol10.Visible = labValue10.Visible = labRes10.Visible = false;
                        times11.Visible = lblName11.Visible = OS11.Visible = labVol11.Visible = labValue11.Visible = labRes11.Visible = false;
                        times12.Visible = lblName12.Visible = OS12.Visible = labVol12.Visible = labValue12.Visible = labRes12.Visible = false;
                        break;
                    case 7:
                        times1.Visible = lblName1.Visible = OS1.Visible = labVol1.Visible = labValue1.Visible = labRes1.Visible = true;
                        times2.Visible = lblName2.Visible = OS2.Visible = labVol2.Visible = labValue2.Visible = labRes2.Visible = true;
                        times3.Visible = lblName3.Visible = OS3.Visible = labVol3.Visible = labValue3.Visible = labRes3.Visible = true;
                        times4.Visible = lblName4.Visible = OS4.Visible = labVol4.Visible = labValue4.Visible = labRes4.Visible = true;
                        times5.Visible = lblName5.Visible = OS5.Visible = labVol5.Visible = labValue5.Visible = labRes5.Visible = true;
                        times6.Visible = lblName6.Visible = OS6.Visible = labVol6.Visible = labValue6.Visible = labRes6.Visible = true;
                        times7.Visible = lblName7.Visible = OS7.Visible = labVol7.Visible = labValue7.Visible = labRes7.Visible = true;
                        times8.Visible = lblName8.Visible = OS8.Visible = labVol8.Visible = labValue8.Visible = labRes8.Visible = false;
                        times9.Visible = lblName9.Visible = OS9.Visible = labVol9.Visible = labValue9.Visible = labRes9.Visible = false;
                        times10.Visible = lblName10.Visible = OS10.Visible = labVol10.Visible = labValue10.Visible = labRes10.Visible = false;
                        times11.Visible = lblName11.Visible = OS11.Visible = labVol11.Visible = labValue11.Visible = labRes11.Visible = false;
                        times12.Visible = lblName12.Visible = OS12.Visible = labVol12.Visible = labValue12.Visible = labRes12.Visible = false;
                        break;
                    case 8:
                        times1.Visible = lblName1.Visible = OS1.Visible = labVol1.Visible = labValue1.Visible = labRes1.Visible = true;
                        times2.Visible = lblName2.Visible = OS2.Visible = labVol2.Visible = labValue2.Visible = labRes2.Visible = true;
                        times3.Visible = lblName3.Visible = OS3.Visible = labVol3.Visible = labValue3.Visible = labRes3.Visible = true;
                        times4.Visible = lblName4.Visible = OS4.Visible = labVol4.Visible = labValue4.Visible = labRes4.Visible = true;
                        times5.Visible = lblName5.Visible = OS5.Visible = labVol5.Visible = labValue5.Visible = labRes5.Visible = true;
                        times6.Visible = lblName6.Visible = OS6.Visible = labVol6.Visible = labValue6.Visible = labRes6.Visible = true;
                        times7.Visible = lblName7.Visible = OS7.Visible = labVol7.Visible = labValue7.Visible = labRes7.Visible = true;
                        times8.Visible = lblName8.Visible = OS8.Visible = labVol8.Visible = labValue8.Visible = labRes8.Visible = true;
                        times9.Visible = lblName9.Visible = OS9.Visible = labVol9.Visible = labValue9.Visible = labRes9.Visible = false;
                        times10.Visible = lblName10.Visible = OS10.Visible = labVol10.Visible = labValue10.Visible = labRes10.Visible = false;
                        times11.Visible = lblName11.Visible = OS11.Visible = labVol11.Visible = labValue11.Visible = labRes11.Visible = false;
                        times12.Visible = lblName12.Visible = OS12.Visible = labVol12.Visible = labValue12.Visible = labRes12.Visible = false;
                        break;
                    case 9:
                        times1.Visible = lblName1.Visible = OS1.Visible = labVol1.Visible = labValue1.Visible = labRes1.Visible = true;
                        times2.Visible = lblName2.Visible = OS2.Visible = labVol2.Visible = labValue2.Visible = labRes2.Visible = true;
                        times3.Visible = lblName3.Visible = OS3.Visible = labVol3.Visible = labValue3.Visible = labRes3.Visible = true;
                        times4.Visible = lblName4.Visible = OS4.Visible = labVol4.Visible = labValue4.Visible = labRes4.Visible = true;
                        times5.Visible = lblName5.Visible = OS5.Visible = labVol5.Visible = labValue5.Visible = labRes5.Visible = true;
                        times6.Visible = lblName6.Visible = OS6.Visible = labVol6.Visible = labValue6.Visible = labRes6.Visible = true;
                        times7.Visible = lblName7.Visible = OS7.Visible = labVol7.Visible = labValue7.Visible = labRes7.Visible = true;
                        times8.Visible = lblName8.Visible = OS8.Visible = labVol8.Visible = labValue8.Visible = labRes8.Visible = true;
                        times9.Visible = lblName9.Visible = OS9.Visible = labVol9.Visible = labValue9.Visible = labRes9.Visible = true;
                        times10.Visible = lblName10.Visible = OS10.Visible = labVol10.Visible = labValue10.Visible = labRes10.Visible = false;
                        times11.Visible = lblName11.Visible = OS11.Visible = labVol11.Visible = labValue11.Visible = labRes11.Visible = false;
                        times12.Visible = lblName12.Visible = OS12.Visible = labVol12.Visible = labValue12.Visible = labRes12.Visible = false;
                        break;
                    case 10:
                        times1.Visible = lblName1.Visible = OS1.Visible = labVol1.Visible = labValue1.Visible = labRes1.Visible = true;
                        times2.Visible = lblName2.Visible = OS2.Visible = labVol2.Visible = labValue2.Visible = labRes2.Visible = true;
                        times3.Visible = lblName3.Visible = OS3.Visible = labVol3.Visible = labValue3.Visible = labRes3.Visible = true;
                        times4.Visible = lblName4.Visible = OS4.Visible = labVol4.Visible = labValue4.Visible = labRes4.Visible = true;
                        times5.Visible = lblName5.Visible = OS5.Visible = labVol5.Visible = labValue5.Visible = labRes5.Visible = true;
                        times6.Visible = lblName6.Visible = OS6.Visible = labVol6.Visible = labValue6.Visible = labRes6.Visible = true;
                        times7.Visible = lblName7.Visible = OS7.Visible = labVol7.Visible = labValue7.Visible = labRes7.Visible = true;
                        times8.Visible = lblName8.Visible = OS8.Visible = labVol8.Visible = labValue8.Visible = labRes8.Visible = true;
                        times9.Visible = lblName9.Visible = OS9.Visible = labVol9.Visible = labValue9.Visible = labRes9.Visible = true;
                        times10.Visible = lblName10.Visible = OS10.Visible = labVol10.Visible = labValue10.Visible = labRes10.Visible = true;
                        times11.Visible = lblName11.Visible = OS11.Visible = labVol11.Visible = labValue11.Visible = labRes11.Visible = false;
                        times12.Visible = lblName12.Visible = OS12.Visible = labVol12.Visible = labValue12.Visible = labRes12.Visible = false;
                        break;
                    case 11:
                        times1.Visible = lblName1.Visible = OS1.Visible = labVol1.Visible = labValue1.Visible = labRes1.Visible = true;
                        times2.Visible = lblName2.Visible = OS2.Visible = labVol2.Visible = labValue2.Visible = labRes2.Visible = true;
                        times3.Visible = lblName3.Visible = OS3.Visible = labVol3.Visible = labValue3.Visible = labRes3.Visible = true;
                        times4.Visible = lblName4.Visible = OS4.Visible = labVol4.Visible = labValue4.Visible = labRes4.Visible = true;
                        times5.Visible = lblName5.Visible = OS5.Visible = labVol5.Visible = labValue5.Visible = labRes5.Visible = true;
                        times6.Visible = lblName6.Visible = OS6.Visible = labVol6.Visible = labValue6.Visible = labRes6.Visible = true;
                        times7.Visible = lblName7.Visible = OS7.Visible = labVol7.Visible = labValue7.Visible = labRes7.Visible = true;
                        times8.Visible = lblName8.Visible = OS8.Visible = labVol8.Visible = labValue8.Visible = labRes8.Visible = true;
                        times9.Visible = lblName9.Visible = OS9.Visible = labVol9.Visible = labValue9.Visible = labRes9.Visible = true;
                        times10.Visible = lblName10.Visible = OS10.Visible = labVol10.Visible = labValue10.Visible = labRes10.Visible = true;
                        times11.Visible = lblName11.Visible = OS11.Visible = labVol11.Visible = labValue11.Visible = labRes11.Visible = true;
                        times12.Visible = lblName12.Visible = OS12.Visible = labVol12.Visible = labValue12.Visible = labRes12.Visible = false;
                        break;
                    case 12:
                        times1.Visible = lblName1.Visible = OS1.Visible = labVol1.Visible = labValue1.Visible = labRes1.Visible = true;
                        times2.Visible = lblName2.Visible = OS2.Visible = labVol2.Visible = labValue2.Visible = labRes2.Visible = true;
                        times3.Visible = lblName3.Visible = OS3.Visible = labVol3.Visible = labValue3.Visible = labRes3.Visible = true;
                        times4.Visible = lblName4.Visible = OS4.Visible = labVol4.Visible = labValue4.Visible = labRes4.Visible = true;
                        times5.Visible = lblName5.Visible = OS5.Visible = labVol5.Visible = labValue5.Visible = labRes5.Visible = true;
                        times6.Visible = lblName6.Visible = OS6.Visible = labVol6.Visible = labValue6.Visible = labRes6.Visible = true;
                        times7.Visible = lblName7.Visible = OS7.Visible = labVol7.Visible = labValue7.Visible = labRes7.Visible = true;
                        times8.Visible = lblName8.Visible = OS8.Visible = labVol8.Visible = labValue8.Visible = labRes8.Visible = true;
                        times9.Visible = lblName9.Visible = OS9.Visible = labVol9.Visible = labValue9.Visible = labRes9.Visible = true;
                        times10.Visible = lblName10.Visible = OS10.Visible = labVol10.Visible = labValue10.Visible = labRes10.Visible = true;
                        times11.Visible = lblName11.Visible = OS11.Visible = labVol11.Visible = labValue11.Visible = labRes11.Visible = true;
                        times12.Visible = lblName12.Visible = OS12.Visible = labVol12.Visible = labValue12.Visible = labRes12.Visible = true;
                        break;
                    default:

                        break;
                }
            }
        }
        #endregion
        
        
        /// <summary>
        /// 显示最终的结果
        /// </summary>
        /// <param name="res">OK NG</param>
        public void ShowRES(My_8740 voltageData)
        {
           
            BeginInvoke(new Action(() => {
                if (!voltageData.Shield) 
                {

                    labeCord.Text = voltageData.Cord_data_L;
                    ResChenTao.Text = voltageData.ResEnd ? "OK" : "NG";
                    ResChenTao.BackColor = voltageData.ResEnd ? Color.LimeGreen : Color.Red;

                    if (voltageData.SOresPin1 == 1)
                        OS1.Text = "OK";
                    else if (voltageData.SOresPin1 == 2)
                        OS1.Text = "断";
                    else
                        OS1.Text = "短";

                    OS1.BackColor = voltageData.SOresPin1 == 1 ? Color.LimeGreen : Color.Red;

                    //////////////////////////////////////////////////
                    if (voltageData.SOresPin2 == 1)
                        OS2.Text = "OK";
                    else if (voltageData.SOresPin2 == 2)
                        OS2.Text = "断";
                    else
                        OS2.Text = "短";

                    OS2.BackColor = voltageData.SOresPin2 == 1 ? Color.LimeGreen : Color.Red;
                    ////////////////////////////////////////////////
                    if (voltageData.SOresPin3 == 1)
                        OS3.Text = "OK";
                    else if (voltageData.SOresPin3 == 2)
                        OS3.Text = "断";
                    else
                        OS3.Text = "短";

                    OS3.BackColor = voltageData.SOresPin3 == 1 ? Color.LimeGreen : Color.Red;
                    //////////////////////////////////////////////
                    if (voltageData.SOresPin4 == 1)
                        OS4.Text = "OK";
                    else if (voltageData.SOresPin4 == 2)
                        OS4.Text = "断";
                    else
                        OS4.Text = "短";

                    OS4.BackColor = voltageData.SOresPin4 == 1 ? Color.LimeGreen : Color.Red;

                    //////////////////////////////////////////////
                    if (voltageData.SOresPin5 == 1)
                        OS5.Text = "OK";
                    else if (voltageData.SOresPin5 == 2)
                        OS5.Text = "断";
                    else
                        OS5.Text = "短";

                    OS5.BackColor = voltageData.SOresPin5 == 1 ? Color.LimeGreen : Color.Red;
                    ////////////////////////////////////////////////
                    if (voltageData.SOresPin6 == 1)
                        OS6.Text = "OK";
                    else if (voltageData.SOresPin6 == 2)
                        OS6.Text = "断";
                    else
                        OS6.Text = "短";

                    OS6.BackColor = voltageData.SOresPin6 == 1 ? Color.LimeGreen : Color.Red;
                    //////////////////////////////////////////////
                    if (voltageData.SOresPin7 == 1)
                        OS7.Text = "OK";
                    else if (voltageData.SOresPin7 == 2)
                        OS7.Text = "断";
                    else
                        OS7.Text = "短";

                    OS7.BackColor = voltageData.SOresPin7 == 1 ? Color.LimeGreen : Color.Red;

                    //////////////////////////////////////////////
                    if (voltageData.SOresPin8 == 1)
                        OS8.Text = "OK";
                    else if (voltageData.SOresPin8 == 2)
                        OS8.Text = "断";
                    else
                        OS8.Text = "短";

                    OS8.BackColor = voltageData.SOresPin8 == 1 ? Color.LimeGreen : Color.Red;

                    //////////////////////////////////////////////
                    if (voltageData.SOresPin9 == 1)
                        OS9.Text = "OK";
                    else if (voltageData.SOresPin9 == 2)
                        OS9.Text = "断";
                    else
                        OS9.Text = "短";

                    OS9.BackColor = voltageData.SOresPin9 == 1 ? Color.LimeGreen : Color.Red;

                    //////////////////////////////////////////////
                    if (voltageData.SOresPin10 == 1)
                        OS10.Text = "OK";
                    else if (voltageData.SOresPin10 == 2)
                        OS10.Text = "断";
                    else
                        OS10.Text = "短";

                    OS10.BackColor = voltageData.SOresPin10 == 1 ? Color.LimeGreen : Color.Red;

                    //////////////////////////////////////////////
                    if (voltageData.SOresPin11 == 1)
                        OS11.Text = "OK";
                    else if (voltageData.SOresPin11 == 2)
                        OS11.Text = "断";
                    else
                        OS11.Text = "短";

                    OS11.BackColor = voltageData.SOresPin11 == 1 ? Color.LimeGreen : Color.Red;

                    //////////////////////////////////////////////
                    if (voltageData.SOresPin12 == 1)
                        OS12.Text = "OK";
                    else if (voltageData.SOresPin12 == 2)
                        OS12.Text = "断";
                    else
                        OS12.Text = "短";
                    OS12.BackColor = voltageData.SOresPin12 == 1 ? Color.LimeGreen : Color.Red;

                    labValue1.Text = voltageData.VolvalPin1.ToString();
                    labValue2.Text = voltageData.VolvalPin2.ToString();
                    labValue3.Text = voltageData.VolvalPin3.ToString();
                    labValue4.Text = voltageData.VolvalPin4.ToString();
                    labValue5.Text = voltageData.VolvalPin5.ToString();
                    labValue6.Text = voltageData.VolvalPin6.ToString();
                    labValue7.Text = voltageData.VolvalPin7.ToString();
                    labValue8.Text = voltageData.VolvalPin8.ToString();
                    labValue9.Text = voltageData.VolvalPin9.ToString();
                    labValue10.Text = voltageData.VolvalPin10.ToString();
                    labValue11.Text = voltageData.VolvalPin11.ToString();
                    labValue12.Text = voltageData.VolvalPin12.ToString();

                    labRes1.Text = voltageData.ResPin1 ? "OK" : "NG";
                    labRes1.BackColor = voltageData.ResPin1 ? Color.LimeGreen : Color.Red;
                    labRes2.Text = voltageData.ResPin2 ? "OK" : "NG";
                    labRes2.BackColor = voltageData.ResPin2 ? Color.LimeGreen : Color.Red;
                    labRes3.Text = voltageData.ResPin3 ? "OK" : "NG";
                    labRes3.BackColor = voltageData.ResPin3 ? Color.LimeGreen : Color.Red;
                    labRes4.Text = voltageData.ResPin4 ? "OK" : "NG";
                    labRes4.BackColor = voltageData.ResPin4 ? Color.LimeGreen : Color.Red;
                    labRes5.Text = voltageData.ResPin5 ? "OK" : "NG";
                    labRes5.BackColor = voltageData.ResPin5 ? Color.LimeGreen : Color.Red;
                    labRes6.Text = voltageData.ResPin6 ? "OK" : "NG";
                    labRes6.BackColor = voltageData.ResPin6 ? Color.LimeGreen : Color.Red;
                    labRes7.Text = voltageData.ResPin7 ? "OK" : "NG";
                    labRes7.BackColor = voltageData.ResPin7 ? Color.LimeGreen : Color.Red;
                    labRes8.Text = voltageData.ResPin8 ? "OK" : "NG";
                    labRes8.BackColor = voltageData.ResPin8 ? Color.LimeGreen : Color.Red;
                    labRes9.Text = voltageData.ResPin9 ? "OK" : "NG";
                    labRes9.BackColor = voltageData.ResPin9 ? Color.LimeGreen : Color.Red;
                    labRes10.Text = voltageData.ResPin10 ? "OK" : "NG";
                    labRes10.BackColor = voltageData.ResPin10 ? Color.LimeGreen : Color.Red;
                    labRes11.Text = voltageData.ResPin11 ? "OK" : "NG";
                    labRes11.BackColor = voltageData.ResPin11 ? Color.LimeGreen : Color.Red;
                    labRes12.Text = voltageData.ResPin12 ? "OK" : "NG";
                    labRes12.BackColor = voltageData.ResPin12 ? Color.LimeGreen : Color.Red;
                    ResEnd.Text = voltageData.ResEnd ? "OK" : "NG";
                    ResEnd.BackColor = voltageData.ResEnd ? Color.LimeGreen : Color.Red;
                     
                }
                else
                {
                    labeCord.Text = voltageData.Cord_data_R;
                    ResChenTao.Text = OS1.Text = OS2.Text = OS3.Text = OS4.Text = OS5.Text = OS6.Text = OS7.Text = OS8.Text = OS9.Text = "屏蔽中";
                    OS1.BackColor = OS2.BackColor = OS3.BackColor = OS4.BackColor = OS5.BackColor = OS6.BackColor = OS7.BackColor = OS8.BackColor = OS9.BackColor = Color.Yellow;
                    labValue1.Text = voltageData.VolvalPin1.ToString();
                    labValue2.Text = voltageData.VolvalPin2.ToString();
                    labValue3.Text = voltageData.VolvalPin3.ToString();
                    labValue4.Text = voltageData.VolvalPin4.ToString();
                    labValue5.Text = voltageData.VolvalPin5.ToString();
                    labValue6.Text = voltageData.VolvalPin6.ToString();
                    labValue7.Text = voltageData.VolvalPin7.ToString();
                    labValue8.Text = voltageData.VolvalPin8.ToString();
                    labValue9.Text = voltageData.VolvalPin9.ToString();
                    labRes1.Text = labRes2.Text = labRes3.Text = labRes4.Text = labRes5.Text = "屏蔽中";
                    labRes6.Text = labRes7.Text = labRes8.Text = labRes9.Text = ResEnd.Text = "屏蔽中";
                    labRes1.BackColor = Color.Yellow;
                    labRes2.BackColor = Color.Yellow;
                    labRes3.BackColor = Color.Yellow;
                    labRes4.BackColor = Color.Yellow;
                    labRes5.BackColor = Color.Yellow;
                    labRes6.BackColor = Color.Yellow;                   
                    labRes7.BackColor = Color.Yellow;
                    labRes8.BackColor = Color.Yellow;
                    labRes9.BackColor = Color.Yellow;
                    ResEnd.BackColor = Color.Yellow;
                    ResChenTao.BackColor = Color.Yellow;
                }
            }));
        }
        /// <summary>
        /// 清除所有结果
        /// </summary>
        public void ClearAll()
        {
            BeginInvoke(new Action(() => {
                labeCord.Text = "";
            ResEnd.Text = "";
            OS1.Text = OS2.Text = OS3.Text = OS4.Text = OS5.Text = OS6.Text = OS7.Text = OS8.Text = OS9.Text = "";
            labVol1.Text = labVol2.Text = labVol3.Text = labVol4.Text = labVol5.Text = labVol6.Text = labVol7.Text =
                labVol8.Text = labVol9.Text = "";
            labValue1.Text = labValue2.Text = labValue3.Text = labValue4.Text = labValue5.Text = labValue6.Text = 
                labValue7.Text = labValue8.Text = labValue9.Text = "";
            labRes1.Text = labRes2.Text = labRes3.Text = labRes4.Text = labRes5.Text =
                labRes6.Text = labRes7.Text = labRes8.Text = labRes9.Text = "";


                ResEnd.BackColor = OS1.BackColor = OS2.BackColor = OS3.BackColor = OS4.BackColor = OS5.BackColor = OS6.BackColor = OS7.BackColor = OS8.BackColor = OS9.BackColor =
            labVol1.BackColor = labVol2.BackColor = labVol3.BackColor = labVol4.BackColor = labVol5.BackColor = labVol6.BackColor = labVol7.BackColor =
                labVol8.BackColor = labVol9.BackColor =
            labValue1.BackColor = labValue2.BackColor = labValue3.BackColor = labValue4.BackColor = labValue5.BackColor = labValue6.BackColor =
                labValue7.BackColor = labValue8.BackColor = labValue9.BackColor =
            labRes1.BackColor = labRes2.BackColor = labRes3.BackColor = labRes4.BackColor = labRes5.BackColor =
                labRes6.BackColor = labRes7.BackColor = labRes8.BackColor = labRes9.BackColor =
            OS1.BackColor = OS2.BackColor = OS3.BackColor = OS4.BackColor = OS5.BackColor = OS6.BackColor = OS7.BackColor = OS8.BackColor = OS9.BackColor = Color.White;

            }));


        }



    }
}
