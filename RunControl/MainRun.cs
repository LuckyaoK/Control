using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLib.Files;

using CXPro001.myclass;
namespace CXPro001.RunControl
{
    public partial class MainRun : UserControl
    {
        
        public MainRun()
        {
            InitializeComponent();
           

        }
        System.Windows.Forms.Timer timer1;
        /// <summary>
        /// 刷新控件上显示的内容-所有工位
        /// </summary>
        /// <returns></returns>
        public EmRes DpdataDontrol1()
        {
            try
            {
                label37.Text = RunBuf.Station1.ToString();
                label38.Text = RunBuf.Station2.ToString();
                label39.Text = RunBuf.Station3.ToString();
                label40.Text = RunBuf.Station4.ToString();
                label41.Text = RunBuf.Station5.ToString();
                label42.Text = RunBuf.Station6.ToString();
                switch (RunBuf.Station1)
                {
                    case 0:
                        textBoxA1.Text = "等待扫码上料";
                        break;
                    case 1:
                        textBoxA1.Text = "扫码有产品"; textBoxA1.Text = RunBuf.StationRes1.ManualCord;
                        break;
                    case 2:
                        textBoxA2.Text = "前往模号检测位置中"; textBoxA2.BackColor = Color.White;
                        break;
                    case 3:
                        textBoxA2.Text = "到达模号检测位置"; textBoxA2.BackColor = Color.White;
                        break;
                    case 4:
                        textBoxA2.Text = "模号检测中"; textBoxA2.BackColor = Color.White;
                        break;
                    case 5:
                        textBoxA2.Text = "模号检测完成"; textBoxA2.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult1)
                        { textBoxA2.Text += "结果OK"; textBoxA2.BackColor = Color.Lime; }
                        else
                        { textBoxA2.Text += "结果NG"; textBoxA2.BackColor = Color.Red; }
                        break;
                    case 6:
                        textBoxA3.Text = "前往平整度检测工位"; textBoxA3.BackColor = Color.White;
                        break;
                    case 7:
                        textBoxA3.Text = "到达平整度检测工位"; textBoxA3.BackColor = Color.White;
                        break;
                    case 8:
                        textBoxA3.Text = "平整度检测中"; textBoxA3.BackColor = Color.White;
                        break;
                    case 9:
                        textBoxA3.Text = "平整度检测完成"; textBoxA3.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult2)
                        { textBoxA3.Text += "结果OK"; textBoxA3.BackColor = Color.Lime; }
                        else
                        { textBoxA3.Text += "结果NG"; textBoxA3.BackColor = Color.Red; }
                        break;
                    case 10:
                        textBoxA4.Text = "前往耐压检测工位中"; textBoxA4.BackColor = Color.White;
                        break;
                    case 11:
                        textBoxA4.Text = "到达耐压检测工位"; textBoxA4.BackColor = Color.White;
                        break;
                    case 12:
                        textBoxA4.Text = "耐压测试中"; textBoxA4.BackColor = Color.White;
                        break;
                    case 13:
                        textBoxA4.Text = "耐压测试完成"; textBoxA4.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult3)
                        { textBoxA4.Text += "结果OK"; textBoxA4.BackColor = Color.Lime; }
                        else
                        { textBoxA4.Text += "结果NG"; textBoxA4.BackColor = Color.Red; }
                        break;
                    case 14:
                        textBoxA5.Text = "前往螺帽检测工位中"; textBoxA5.BackColor = Color.White;
                        break;
                    case 15:
                        textBoxA5.Text = "到达螺帽检测工位"; textBoxA5.BackColor = Color.White;
                        break;
                    case 16:
                        textBoxA5.Text = "螺帽检测中"; textBoxA5.BackColor = Color.White;
                        break;
                    case 17:
                        textBoxA5.Text = "螺帽检测完成"; textBoxA5.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult4)
                        { textBoxA5.Text += "结果OK"; textBoxA5.BackColor = Color.Lime; }
                        else
                        { textBoxA5.Text += "结果NG"; textBoxA5.BackColor = Color.Red; }
                        break;
                    case 18:
                        textBoxA6.Text = "前往扫码比对工位中"; textBoxA6.BackColor = Color.White;
                        break;
                    case 19:
                        textBoxA6.Text = "到达扫码工位"; textBoxA6.BackColor = Color.White;
                        break;
                    case 20:
                        textBoxA6.Text = "开始扫码比对中"; textBoxA6.BackColor = Color.White;
                        break;
                    case 21:
                        textBoxA6.Text = "扫码比对完成"; textBoxA6.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult5)
                        { textBoxA6.Text += "结果OK"; textBoxA6.BackColor = Color.Lime; }
                        else
                        { textBoxA6.Text += "结果NG"; textBoxA6.BackColor = Color.Red; }
                        break;
                    case 22:
                        //return "下料中";
                        break;
                    case 23:
                        //  return "下料完成，等待扫码上料";
                        break;
                    case 24:
                        // return "储存数据完成，等待上料";
                        textBoxA1.Text = "Waitting"; textBoxA1.BackColor = Color.White;
                        textBoxA2.Text = "Waitting"; textBoxA2.BackColor = Color.White;
                        textBoxA3.Text = "Waitting"; textBoxA3.BackColor = Color.White;
                        textBoxA4.Text = "Waitting"; textBoxA4.BackColor = Color.White;
                        textBoxA5.Text = "Waitting"; textBoxA5.BackColor = Color.White;
                        textBoxA6.Text = "Waitting"; textBoxA6.BackColor = Color.White;
                        RunBuf.Station1 = 0;
                        break;
                    default:
                        // return "未知状态";
                        break;
                }
                switch (RunBuf.Station2)
                {
                    case 0:
                        textBoxB1.Text = "等待扫码上料";
                        break;
                    case 1:
                        textBoxB1.Text = "扫码有产品"; textBoxB1.Text = RunBuf.StationRes2.ManualCord;
                        break;
                    case 2:
                        textBoxB2.Text = "前往模号检测位置中"; textBoxB2.BackColor = Color.White;
                        break;
                    case 3:
                        textBoxB2.Text = "到达模号检测位置"; textBoxB2.BackColor = Color.White;
                        break;
                    case 4:
                        textBoxB2.Text = "模号检测中"; textBoxB2.BackColor = Color.White;
                        break;
                    case 5:
                        textBoxB2.Text = "模号检测完成"; textBoxB2.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult1)
                        { textBoxB2.Text += "结果OK"; textBoxB2.BackColor = Color.Lime; }
                        else
                        { textBoxB2.Text += "结果NG"; textBoxB2.BackColor = Color.Red; }
                        break;
                    case 6:
                        textBoxB3.Text = "前往平整度检测工位"; textBoxB3.BackColor = Color.White;
                        break;
                    case 7:
                        textBoxB3.Text = "到达平整度检测工位"; textBoxB3.BackColor = Color.White;
                        break;
                    case 8:
                        textBoxB3.Text = "平整度检测中"; textBoxB3.BackColor = Color.White;
                        break;
                    case 9:
                        textBoxB3.Text = "平整度检测完成"; textBoxB3.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult2)
                        { textBoxB3.Text += "结果OK"; textBoxB3.BackColor = Color.Lime; }
                        else
                        { textBoxB3.Text += "结果NG"; textBoxB3.BackColor = Color.Red; }
                        break;
                    case 10:
                        textBoxB4.Text = "前往耐压检测工位中"; textBoxB4.BackColor = Color.White;
                        break;
                    case 11:
                        textBoxB4.Text = "到达耐压检测工位"; textBoxB4.BackColor = Color.White;
                        break;
                    case 12:
                        textBoxB4.Text = "耐压测试中"; textBoxB4.BackColor = Color.White;
                        break;
                    case 13:
                        textBoxB4.Text = "耐压测试完成"; textBoxB4.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult3)
                        { textBoxB4.Text += "结果OK"; textBoxB4.BackColor = Color.Lime; }
                        else
                        { textBoxB4.Text += "结果NG"; textBoxB4.BackColor = Color.Red; }
                        break;
                    case 14:
                        textBoxB5.Text = "前往螺帽检测工位中"; textBoxB5.BackColor = Color.White;
                        break;
                    case 15:
                        textBoxB5.Text = "到达螺帽检测工位"; textBoxB5.BackColor = Color.White;
                        break;
                    case 16:
                        textBoxB5.Text = "螺帽检测中"; textBoxB5.BackColor = Color.White;
                        break;
                    case 17:
                        textBoxB5.Text = "螺帽检测完成"; textBoxB5.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult4)
                        { textBoxB5.Text += "结果OK"; textBoxB5.BackColor = Color.Lime; }
                        else
                        { textBoxB5.Text += "结果NG"; textBoxB5.BackColor = Color.Red; }
                        break;
                    case 18:
                        textBoxB6.Text = "前往扫码比对工位中"; textBoxB6.BackColor = Color.White;
                        break;
                    case 19:
                        textBoxB6.Text = "到达扫码工位"; textBoxB6.BackColor = Color.White;
                        break;
                    case 20:
                        textBoxB6.Text = "开始扫码比对中"; textBoxB6.BackColor = Color.White;
                        break;
                    case 21:
                        textBoxB6.Text = "扫码比对完成"; textBoxB6.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult5)
                        { textBoxB6.Text += "结果OK"; textBoxB6.BackColor = Color.Lime; }
                        else
                        { textBoxB6.Text += "结果NG"; textBoxB6.BackColor = Color.Red; }
                        break;
                    case 22:
                        //return "下料中";
                        break;
                    case 23:
                        //  return "下料完成，等待扫码上料";
                        break;
                    case 24:
                        // return "储存数据完成，等待上料";
                        textBoxB1.Text = "Waitting"; textBoxB1.BackColor = Color.White;
                        textBoxB2.Text = "Waitting"; textBoxB2.BackColor = Color.White;
                        textBoxB3.Text = "Waitting"; textBoxB3.BackColor = Color.White;
                        textBoxB4.Text = "Waitting"; textBoxB4.BackColor = Color.White;
                        textBoxB5.Text = "Waitting"; textBoxB5.BackColor = Color.White;
                        textBoxB6.Text = "Waitting"; textBoxB6.BackColor = Color.White;
                        RunBuf.Station2 = 0;
                        break;
                    default:
                        // return "未知状态";
                        break;
                }
                switch (RunBuf.Station3)
                {
                    case 0:
                        textBoxC1.Text = "等待扫码上料";
                        break;
                    case 1:
                        textBoxC1.Text = "扫码有产品"; textBoxC1.Text = RunBuf.StationRes2.ManualCord;
                        break;
                    case 2:
                        textBoxC2.Text = "前往模号检测位置中"; textBoxC2.BackColor = Color.White;
                        break;
                    case 3:
                        textBoxC2.Text = "到达模号检测位置"; textBoxC2.BackColor = Color.White;
                        break;
                    case 4:
                        textBoxC2.Text = "模号检测中"; textBoxC2.BackColor = Color.White;
                        break;
                    case 5:
                        textBoxC2.Text = "模号检测完成"; textBoxC2.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult1)
                        { textBoxC2.Text += "结果OK"; textBoxC2.BackColor = Color.Lime; }
                        else
                        { textBoxC2.Text += "结果NG"; textBoxC2.BackColor = Color.Red; }
                        break;
                    case 6:
                        textBoxC3.Text = "前往平整度检测工位"; textBoxC3.BackColor = Color.White;
                        break;
                    case 7:
                        textBoxC3.Text = "到达平整度检测工位"; textBoxC3.BackColor = Color.White;
                        break;
                    case 8:
                        textBoxC3.Text = "平整度检测中"; textBoxC3.BackColor = Color.White;
                        break;
                    case 9:
                        textBoxC3.Text = "平整度检测完成"; textBoxC3.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult2)
                        { textBoxC3.Text += "结果OK"; textBoxC3.BackColor = Color.Lime; }
                        else
                        { textBoxC3.Text += "结果NG"; textBoxC3.BackColor = Color.Red; }
                        break;
                    case 10:
                        textBoxC4.Text = "前往耐压检测工位中"; textBoxC4.BackColor = Color.White;
                        break;
                    case 11:
                        textBoxC4.Text = "到达耐压检测工位"; textBoxC4.BackColor = Color.White;
                        break;
                    case 12:
                        textBoxC4.Text = "耐压测试中"; textBoxC4.BackColor = Color.White;
                        break;
                    case 13:
                        textBoxC4.Text = "耐压测试完成"; textBoxC4.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult3)
                        { textBoxC4.Text += "结果OK"; textBoxC4.BackColor = Color.Lime; }
                        else
                        { textBoxC4.Text += "结果NG"; textBoxC4.BackColor = Color.Red; }
                        break;
                    case 14:
                        textBoxC5.Text = "前往螺帽检测工位中"; textBoxC5.BackColor = Color.White;
                        break;
                    case 15:
                        textBoxC5.Text = "到达螺帽检测工位"; textBoxC5.BackColor = Color.White;
                        break;
                    case 16:
                        textBoxC5.Text = "螺帽检测中"; textBoxC5.BackColor = Color.White;
                        break;
                    case 17:
                        textBoxC5.Text = "螺帽检测完成"; textBoxC5.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult4)
                        { textBoxC5.Text += "结果OK"; textBoxC5.BackColor = Color.Lime; }
                        else
                        { textBoxC5.Text += "结果NG"; textBoxC5.BackColor = Color.Red; }
                        break;
                    case 18:
                        textBoxC6.Text = "前往扫码比对工位中"; textBoxC6.BackColor = Color.White;
                        break;
                    case 19:
                        textBoxC6.Text = "到达扫码工位"; textBoxC6.BackColor = Color.White;
                        break;
                    case 20:
                        textBoxC6.Text = "开始扫码比对中"; textBoxC6.BackColor = Color.White;
                        break;
                    case 21:
                        textBoxC6.Text = "扫码比对完成"; textBoxC6.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult5)
                        { textBoxC6.Text += "结果OK"; textBoxC6.BackColor = Color.Lime; }
                        else
                        { textBoxC6.Text += "结果NG"; textBoxC6.BackColor = Color.Red; }
                        break;
                    case 22:
                        //return "下料中";
                        break;
                    case 23:
                        //  return "下料完成，等待扫码上料";
                        break;
                    case 24:
                        // return "储存数据完成，等待上料";
                        textBoxC1.Text = "Waitting"; textBoxC1.BackColor = Color.White;
                        textBoxC2.Text = "Waitting"; textBoxC2.BackColor = Color.White;
                        textBoxC3.Text = "Waitting"; textBoxC3.BackColor = Color.White;
                        textBoxC4.Text = "Waitting"; textBoxC4.BackColor = Color.White;
                        textBoxC5.Text = "Waitting"; textBoxC5.BackColor = Color.White;
                        textBoxC6.Text = "Waitting"; textBoxC6.BackColor = Color.White;
                        RunBuf.Station3 = 0;
                        break;
                    default:
                        // return "未知状态";
                        break;
                }
                switch (RunBuf.Station4)
                {
                    case 0:
                        textBoxD1.Text = "等待扫码上料";
                        break;
                    case 1:
                        textBoxD1.Text = "扫码有产品"; textBoxD1.Text = RunBuf.StationRes2.ManualCord;
                        break;
                    case 2:
                        textBoxD2.Text = "前往模号检测位置中"; textBoxD2.BackColor = Color.White;
                        break;
                    case 3:
                        textBoxD2.Text = "到达模号检测位置"; textBoxD2.BackColor = Color.White;
                        break;
                    case 4:
                        textBoxD2.Text = "模号检测中"; textBoxD2.BackColor = Color.White;
                        break;
                    case 5:
                        textBoxD2.Text = "模号检测完成"; textBoxD2.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult1)
                        { textBoxD2.Text += "结果OK"; textBoxD2.BackColor = Color.Lime; }
                        else
                        { textBoxD2.Text += "结果NG"; textBoxD2.BackColor = Color.Red; }
                        break;
                    case 6:
                        textBoxD3.Text = "前往平整度检测工位"; textBoxD3.BackColor = Color.White;
                        break;
                    case 7:
                        textBoxD3.Text = "到达平整度检测工位"; textBoxD3.BackColor = Color.White;
                        break;
                    case 8:
                        textBoxD3.Text = "平整度检测中"; textBoxD3.BackColor = Color.White;
                        break;
                    case 9:
                        textBoxD3.Text = "平整度检测完成"; textBoxD3.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult2)
                        { textBoxD3.Text += "结果OK"; textBoxD3.BackColor = Color.Lime; }
                        else
                        { textBoxD3.Text += "结果NG"; textBoxD3.BackColor = Color.Red; }
                        break;
                    case 10:
                        textBoxD4.Text = "前往耐压检测工位中"; textBoxD4.BackColor = Color.White;
                        break;
                    case 11:
                        textBoxD4.Text = "到达耐压检测工位"; textBoxD4.BackColor = Color.White;
                        break;
                    case 12:
                        textBoxD4.Text = "耐压测试中"; textBoxD4.BackColor = Color.White;
                        break;
                    case 13:
                        textBoxD4.Text = "耐压测试完成"; textBoxD4.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult3)
                        { textBoxD4.Text += "结果OK"; textBoxD4.BackColor = Color.Lime; }
                        else
                        { textBoxD4.Text += "结果NG"; textBoxD4.BackColor = Color.Red; }
                        break;
                    case 14:
                        textBoxD5.Text = "前往螺帽检测工位中"; textBoxD5.BackColor = Color.White;
                        break;
                    case 15:
                        textBoxD5.Text = "到达螺帽检测工位"; textBoxD5.BackColor = Color.White;
                        break;
                    case 16:
                        textBoxD5.Text = "螺帽检测中"; textBoxD5.BackColor = Color.White;
                        break;
                    case 17:
                        textBoxD5.Text = "螺帽检测完成"; textBoxD5.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult4)
                        { textBoxD5.Text += "结果OK"; textBoxD5.BackColor = Color.Lime; }
                        else
                        { textBoxD5.Text += "结果NG"; textBoxD5.BackColor = Color.Red; }
                        break;
                    case 18:
                        textBoxD6.Text = "前往扫码比对工位中"; textBoxD6.BackColor = Color.White;
                        break;
                    case 19:
                        textBoxD6.Text = "到达扫码工位"; textBoxD6.BackColor = Color.White;
                        break;
                    case 20:
                        textBoxD6.Text = "开始扫码比对中"; textBoxD6.BackColor = Color.White;
                        break;
                    case 21:
                        textBoxD6.Text = "扫码比对完成"; textBoxD6.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult5)
                        { textBoxD6.Text += "结果OK"; textBoxD6.BackColor = Color.Lime; }
                        else
                        { textBoxD6.Text += "结果NG"; textBoxD6.BackColor = Color.Red; }
                        break;
                    case 22:
                        //return "下料中";
                        break;
                    case 23:
                        //  return "下料完成，等待扫码上料";
                        break;
                    case 24:
                        // return "储存数据完成，等待上料";
                        textBoxD1.Text = "Waitting"; textBoxD1.BackColor = Color.White;
                        textBoxD2.Text = "Waitting"; textBoxD2.BackColor = Color.White;
                        textBoxD3.Text = "Waitting"; textBoxD3.BackColor = Color.White;
                        textBoxD4.Text = "Waitting"; textBoxD4.BackColor = Color.White;
                        textBoxD5.Text = "Waitting"; textBoxD5.BackColor = Color.White;
                        textBoxD6.Text = "Waitting"; textBoxD6.BackColor = Color.White;
                        RunBuf.Station4 = 0;
                        break;
                    default:
                        // return "未知状态";
                        break;
                }
                switch (RunBuf.Station5)
                {
                    case 0:
                        textBoxE1.Text = "等待扫码上料";
                        break;
                    case 1:
                        textBoxE1.Text = "扫码有产品"; textBoxE1.Text = RunBuf.StationRes2.ManualCord;
                        break;
                    case 2:
                        textBoxE2.Text = "前往模号检测位置中"; textBoxE2.BackColor = Color.White;
                        break;
                    case 3:
                        textBoxE2.Text = "到达模号检测位置"; textBoxE2.BackColor = Color.White;
                        break;
                    case 4:
                        textBoxE2.Text = "模号检测中"; textBoxE2.BackColor = Color.White;
                        break;
                    case 5:
                        textBoxE2.Text = "模号检测完成"; textBoxE2.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult1)
                        { textBoxE2.Text += "结果OK"; textBoxE2.BackColor = Color.Lime; }
                        else
                        { textBoxE2.Text += "结果NG"; textBoxE2.BackColor = Color.Red; }
                        break;
                    case 6:
                        textBoxE3.Text = "前往平整度检测工位"; textBoxE3.BackColor = Color.White;
                        break;
                    case 7:
                        textBoxE3.Text = "到达平整度检测工位"; textBoxE3.BackColor = Color.White;
                        break;
                    case 8:
                        textBoxE3.Text = "平整度检测中"; textBoxE3.BackColor = Color.White;
                        break;
                    case 9:
                        textBoxE3.Text = "平整度检测完成"; textBoxE3.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult2)
                        { textBoxE3.Text += "结果OK"; textBoxE3.BackColor = Color.Lime; }
                        else
                        { textBoxE3.Text += "结果NG"; textBoxE3.BackColor = Color.Red; }
                        break;
                    case 10:
                        textBoxE4.Text = "前往耐压检测工位中"; textBoxE4.BackColor = Color.White;
                        break;
                    case 11:
                        textBoxE4.Text = "到达耐压检测工位"; textBoxE4.BackColor = Color.White;
                        break;
                    case 12:
                        textBoxE4.Text = "耐压测试中"; textBoxE4.BackColor = Color.White;
                        break;
                    case 13:
                        textBoxE4.Text = "耐压测试完成"; textBoxE4.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult3)
                        { textBoxE4.Text += "结果OK"; textBoxE4.BackColor = Color.Lime; }
                        else
                        { textBoxE4.Text += "结果NG"; textBoxE4.BackColor = Color.Red; }
                        break;
                    case 14:
                        textBoxE5.Text = "前往螺帽检测工位中"; textBoxE5.BackColor = Color.White;
                        break;
                    case 15:
                        textBoxE5.Text = "到达螺帽检测工位"; textBoxE5.BackColor = Color.White;
                        break;
                    case 16:
                        textBoxE5.Text = "螺帽检测中"; textBoxE5.BackColor = Color.White;
                        break;
                    case 17:
                        textBoxE5.Text = "螺帽检测完成"; textBoxE5.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult4)
                        { textBoxE5.Text += "结果OK"; textBoxE5.BackColor = Color.Lime; }
                        else
                        { textBoxE5.Text += "结果NG"; textBoxE5.BackColor = Color.Red; }
                        break;
                    case 18:
                        textBoxE6.Text = "前往扫码比对工位中"; textBoxE6.BackColor = Color.White;
                        break;
                    case 19:
                        textBoxE6.Text = "到达扫码工位"; textBoxE6.BackColor = Color.White;
                        break;
                    case 20:
                        textBoxE6.Text = "开始扫码比对中"; textBoxE6.BackColor = Color.White;
                        break;
                    case 21:
                        textBoxE6.Text = "扫码比对完成"; textBoxE6.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult5)
                        { textBoxE6.Text += "结果OK"; textBoxE6.BackColor = Color.Lime; }
                        else
                        { textBoxE6.Text += "结果NG"; textBoxE6.BackColor = Color.Red; }
                        break;
                    case 22:
                        //return "下料中";
                        break;
                    case 23:
                        //  return "下料完成，等待扫码上料";
                        break;
                    case 24:
                        // return "储存数据完成，等待上料";
                        textBoxE1.Text = "Waitting"; textBoxE1.BackColor = Color.White;
                        textBoxE2.Text = "Waitting"; textBoxE2.BackColor = Color.White;
                        textBoxE3.Text = "Waitting"; textBoxE3.BackColor = Color.White;
                        textBoxE4.Text = "Waitting"; textBoxE4.BackColor = Color.White;
                        textBoxE5.Text = "Waitting"; textBoxE5.BackColor = Color.White;
                        textBoxE6.Text = "Waitting"; textBoxE6.BackColor = Color.White;
                        RunBuf.Station5 = 0;
                        break;
                    default:
                        // return "未知状态";
                        break;
                }
                switch (RunBuf.Station6)
                {
                    case 0:
                        textBoxF1.Text = "等待扫码上料";
                        break;
                    case 1:
                        textBoxF1.Text = "扫码有产品"; textBoxF1.Text = RunBuf.StationRes2.ManualCord;
                        break;
                    case 2:
                        textBoxF2.Text = "前往模号检测位置中"; textBoxF2.BackColor = Color.White;
                        break;
                    case 3:
                        textBoxF2.Text = "到达模号检测位置"; textBoxF2.BackColor = Color.White;
                        break;
                    case 4:
                        textBoxF2.Text = "模号检测中"; textBoxF2.BackColor = Color.White;
                        break;
                    case 5:
                        textBoxF2.Text = "模号检测完成"; textBoxF2.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult1)
                        { textBoxF2.Text += "结果OK"; textBoxF2.BackColor = Color.Lime; }
                        else
                        { textBoxF2.Text += "结果NG"; textBoxF2.BackColor = Color.Red; }
                        break;
                    case 6:
                        textBoxF3.Text = "前往平整度检测工位"; textBoxF3.BackColor = Color.White;
                        break;
                    case 7:
                        textBoxF3.Text = "到达平整度检测工位"; textBoxF3.BackColor = Color.White;
                        break;
                    case 8:
                        textBoxF3.Text = "平整度检测中"; textBoxF3.BackColor = Color.White;
                        break;
                    case 9:
                        textBoxF3.Text = "平整度检测完成"; textBoxF3.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult2)
                        { textBoxF3.Text += "结果OK"; textBoxF3.BackColor = Color.Lime; }
                        else
                        { textBoxF3.Text += "结果NG"; textBoxF3.BackColor = Color.Red; }
                        break;
                    case 10:
                        textBoxF4.Text = "前往耐压检测工位中"; textBoxF4.BackColor = Color.White;
                        break;
                    case 11:
                        textBoxF4.Text = "到达耐压检测工位"; textBoxF4.BackColor = Color.White;
                        break;
                    case 12:
                        textBoxF4.Text = "耐压测试中"; textBoxF4.BackColor = Color.White;
                        break;
                    case 13:
                        textBoxF4.Text = "耐压测试完成"; textBoxF4.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult3)
                        { textBoxF4.Text += "结果OK"; textBoxF4.BackColor = Color.Lime; }
                        else
                        { textBoxF4.Text += "结果NG"; textBoxF4.BackColor = Color.Red; }
                        break;
                    case 14:
                        textBoxF5.Text = "前往螺帽检测工位中"; textBoxF5.BackColor = Color.White;
                        break;
                    case 15:
                        textBoxF5.Text = "到达螺帽检测工位"; textBoxF5.BackColor = Color.White;
                        break;
                    case 16:
                        textBoxF5.Text = "螺帽检测中"; textBoxF5.BackColor = Color.White;
                        break;
                    case 17:
                        textBoxF5.Text = "螺帽检测完成"; textBoxF5.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult4)
                        { textBoxF5.Text += "结果OK"; textBoxF5.BackColor = Color.Lime; }
                        else
                        { textBoxF5.Text += "结果NG"; textBoxF5.BackColor = Color.Red; }
                        break;
                    case 18:
                        textBoxF6.Text = "前往扫码比对工位中"; textBoxF6.BackColor = Color.White;
                        break;
                    case 19:
                        textBoxF6.Text = "到达扫码工位"; textBoxF6.BackColor = Color.White;
                        break;
                    case 20:
                        textBoxF6.Text = "开始扫码比对中"; textBoxF6.BackColor = Color.White;
                        break;
                    case 21:
                        textBoxF6.Text = "扫码比对完成"; textBoxF6.BackColor = Color.White;
                        if (RunBuf.StationRes1.sresult5)
                        { textBoxF6.Text += "结果OK"; textBoxF6.BackColor = Color.Lime; }
                        else
                        { textBoxF6.Text += "结果NG"; textBoxF6.BackColor = Color.Red; }
                        break;
                    case 22:
                        //return "下料中";
                        break;
                    case 23:
                        //  return "下料完成，等待扫码上料";
                        break;
                    case 24:
                        // return "储存数据完成，等待上料";
                        textBoxF1.Text = "Waitting"; textBoxF1.BackColor = Color.White;
                        textBoxF2.Text = "Waitting"; textBoxF2.BackColor = Color.White;
                        textBoxF3.Text = "Waitting"; textBoxF3.BackColor = Color.White;
                        textBoxF4.Text = "Waitting"; textBoxF4.BackColor = Color.White;
                        textBoxF5.Text = "Waitting"; textBoxF5.BackColor = Color.White;
                        textBoxF6.Text = "Waitting"; textBoxF6.BackColor = Color.White;
                        RunBuf.Station6 = 0;
                        break;
                    default:
                        // return "未知状态";
                        break;
                }
                return EmRes.Succeed;
            }
            catch (Exception ex )
            {
                Logger.Error($"{this.Name}控件内容刷新出错{ex.Message}");
                return EmRes.Error;
            }
           
        }

        public void init()
        {
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 500;
            timer1.Tick += Timer1_Tick;
            timer1.Enabled = true;
            RunBuf.LoadStaData("");
            LoadText();
        }
        
      
        private void Timer1_Tick(object sender, EventArgs e) 
        {
            BeginInvoke(new Action(() => {
                DpdataDontrol1();
            }));
          
        }
        #region 保存、加载text
        /// <summary>
        /// 保存空间上的内容
        /// </summary>
        public void SaveText()
        {
            string filename = $"{Path.GetFullPath("..")}\\product\\{SysStatus.CurProductName.Trim()}\\controltext.ini";// 配置文件路径           
            IniFile inf = new IniFile(filename);//确认路径是否存在，不存在则创建文件夹。          
            string STEP = "Station1";
            inf.WriteString(STEP, "textBoxA1", textBoxA1.Text);
            inf.WriteString(STEP, "textBoxA2", textBoxA2.Text);
            inf.WriteString(STEP, "textBoxA3", textBoxA3.Text);
            inf.WriteString(STEP, "textBoxA4", textBoxA4.Text);
            inf.WriteString(STEP, "textBoxA5", textBoxA5.Text);
            inf.WriteString(STEP, "textBoxA6", textBoxA6.Text);
            STEP = "Station2";
            inf.WriteString(STEP, "textBoxB1", textBoxB1.Text);
            inf.WriteString(STEP, "textBoxB2", textBoxB2.Text);
            inf.WriteString(STEP, "textBoxB3", textBoxB3.Text);
            inf.WriteString(STEP, "textBoxB4", textBoxB4.Text);
            inf.WriteString(STEP, "textBoxB5", textBoxB5.Text);
            inf.WriteString(STEP, "textBoxB6", textBoxB6.Text);
            STEP = "Station3";
            inf.WriteString(STEP, "textBoxC1", textBoxC1.Text);
            inf.WriteString(STEP, "textBoxC2", textBoxC2.Text);
            inf.WriteString(STEP, "textBoxC3", textBoxC3.Text);
            inf.WriteString(STEP, "textBoxC4", textBoxC4.Text);
            inf.WriteString(STEP, "textBoxC5", textBoxC5.Text);
            inf.WriteString(STEP, "textBoxC6", textBoxC6.Text);
            STEP = "Station4";
            inf.WriteString(STEP, "textBoxD1", textBoxD1.Text);
            inf.WriteString(STEP, "textBoxD2", textBoxD2.Text);
            inf.WriteString(STEP, "textBoxD3", textBoxD3.Text);
            inf.WriteString(STEP, "textBoxD4", textBoxD4.Text);
            inf.WriteString(STEP, "textBoxD5", textBoxD5.Text);
            inf.WriteString(STEP, "textBoxD6", textBoxD6.Text);
            STEP = "Station5";
            inf.WriteString(STEP, "textBoxE1", textBoxE1.Text);
            inf.WriteString(STEP, "textBoxE2", textBoxE2.Text);
            inf.WriteString(STEP, "textBoxE3", textBoxE3.Text);
            inf.WriteString(STEP, "textBoxE4", textBoxE4.Text);
            inf.WriteString(STEP, "textBoxE5", textBoxE5.Text);
            inf.WriteString(STEP, "textBoxE6", textBoxE6.Text);
            STEP = "Station6";
            inf.WriteString(STEP, "textBoxF1", textBoxF1.Text);
            inf.WriteString(STEP, "textBoxF2", textBoxF2.Text);
            inf.WriteString(STEP, "textBoxF3", textBoxF3.Text);
            inf.WriteString(STEP, "textBoxF4", textBoxF4.Text);
            inf.WriteString(STEP, "textBoxF5", textBoxF5.Text);
            inf.WriteString(STEP, "textBoxF6", textBoxF6.Text);

        }
        /// <summary>
        /// 加载空间上的内容
        /// </summary>
        public void LoadText()
        {
            string filename = $"{Path.GetFullPath("..")}\\product\\{SysStatus.CurProductName.Trim()}\\controltext.ini";// 配置文件路径           
            IniFile inf = new IniFile(filename);//确认路径是否存在，不存在则创建文件夹。          
                string STEP = "Station1";
                textBoxA1.Text = inf.ReadString(STEP, "textBoxA1", "");
                textBoxA2.Text = inf.ReadString(STEP, "textBoxA2", "");
                textBoxA3.Text = inf.ReadString(STEP, "textBoxA3", "");
                textBoxA4.Text = inf.ReadString(STEP, "textBoxA4", "");
                textBoxA5.Text = inf.ReadString(STEP, "textBoxA5", "");
                textBoxA6.Text = inf.ReadString(STEP, "textBoxA6", "");
                 STEP = "Station2";
                textBoxB1.Text = inf.ReadString(STEP, "textBoxB1", "");
                textBoxB2.Text = inf.ReadString(STEP, "textBoxB2", "");
                textBoxB3.Text = inf.ReadString(STEP, "textBoxB3", "");
                textBoxB4.Text = inf.ReadString(STEP, "textBoxB4", "");
                textBoxB5.Text = inf.ReadString(STEP, "textBoxB5", "");
                textBoxB6.Text = inf.ReadString(STEP, "textBoxB6", "");
                STEP = "Station3";
                textBoxC1.Text = inf.ReadString(STEP, "textBoxC1", "");
                textBoxC2.Text = inf.ReadString(STEP, "textBoxC2", "");
                textBoxC3.Text = inf.ReadString(STEP, "textBoxC3", "");
                textBoxC4.Text = inf.ReadString(STEP, "textBoxC4", "");
                textBoxC5.Text = inf.ReadString(STEP, "textBoxC5", "");
                textBoxC6.Text = inf.ReadString(STEP, "textBoxC6", "");
                STEP = "Station4";
                textBoxD1.Text = inf.ReadString(STEP, "textBoxD1", "");
                textBoxD2.Text = inf.ReadString(STEP, "textBoxD2", "");
                textBoxD3.Text = inf.ReadString(STEP, "textBoxD3", "");
                textBoxD4.Text = inf.ReadString(STEP, "textBoxD4", "");
                textBoxD5.Text = inf.ReadString(STEP, "textBoxD5", "");
                textBoxD6.Text = inf.ReadString(STEP, "textBoxD6", "");
                STEP = "Station5";
                textBoxE1.Text = inf.ReadString(STEP, "textBoxE1", "");
                textBoxE2.Text = inf.ReadString(STEP, "textBoxE2", "");
                textBoxE3.Text = inf.ReadString(STEP, "textBoxE3", "");
                textBoxE4.Text = inf.ReadString(STEP, "textBoxE4", "");
                textBoxE5.Text = inf.ReadString(STEP, "textBoxE5", "");
                textBoxE6.Text = inf.ReadString(STEP, "textBoxE6", "");
                STEP = "Station5";
                textBoxF1.Text = inf.ReadString(STEP, "textBoxF1", "");
                textBoxF2.Text = inf.ReadString(STEP, "textBoxF2", "");
                textBoxF3.Text = inf.ReadString(STEP, "textBoxF3", "");
                textBoxF4.Text = inf.ReadString(STEP, "textBoxF4", "");
                textBoxF5.Text = inf.ReadString(STEP, "textBoxF5", "");
                textBoxF6.Text = inf.ReadString(STEP, "textBoxF6", "");

         }
        /// <summary>
        /// 清空显示
        /// </summary>
        public void ClearText()
        {
            textBoxA1.Text = "";
            textBoxA2.Text = "";
            textBoxA3.Text = "";
            textBoxA4.Text = "";
            textBoxA5.Text = "";
            textBoxA6.Text = "";

            textBoxB1.Text = "";
            textBoxB2.Text = "";
            textBoxB3.Text = "";
            textBoxB4.Text = "";
            textBoxB5.Text = "";
            textBoxB6.Text = "";
            
            textBoxC1.Text = "";
            textBoxC2.Text = "";
            textBoxC3.Text = "";
            textBoxC4.Text = "";
            textBoxC5.Text = "";
            textBoxC6.Text = "";
           
            textBoxD1.Text = "";
            textBoxD2.Text = "";
            textBoxD3.Text = "";
            textBoxD4.Text = "";
            textBoxD5.Text = "";
            textBoxD6.Text = "";
          
            textBoxE1.Text = "";
            textBoxE2.Text = "";
            textBoxE3.Text = "";
            textBoxE4.Text = "";
            textBoxE5.Text = "";
            textBoxE6.Text = "";
           
            textBoxF1.Text = "";
            textBoxF2.Text = "";
            textBoxF3.Text = "";
            textBoxF4.Text = "";
            textBoxF5.Text = "";
            textBoxF6.Text = "";
        }
        #endregion
        public void textchangebig()
        {
            timer1.Enabled = false;
            Thread.Sleep(100);
            textBoxA1.Font = new Font("宋体", 14);
            textBoxA2.Font = new Font("宋体", 14);
            textBoxA3.Font = new Font("宋体", 14);
            textBoxA4.Font = new Font("宋体", 14);
            textBoxA5.Font = new Font("宋体", 14);
            textBoxA6.Font = new Font("宋体", 14);
            textBoxB1.Font = new Font("宋体", 14);
            textBoxB2.Font = new Font("宋体", 14);
            textBoxB3.Font = new Font("宋体", 14);
            textBoxB4.Font = new Font("宋体", 14);
            textBoxB5.Font = new Font("宋体", 14);
            textBoxB6.Font = new Font("宋体", 14);
            textBoxC1.Font = new Font("宋体", 14);
            textBoxC2.Font = new Font("宋体", 14);
            textBoxC3.Font = new Font("宋体", 14);
            textBoxC4.Font = new Font("宋体", 14);
            textBoxC5.Font = new Font("宋体", 14);
            textBoxC6.Font = new Font("宋体", 14);
            textBoxD1.Font = new Font("宋体", 14);
            textBoxD2.Font = new Font("宋体", 14);
            textBoxD3.Font = new Font("宋体", 14);
            textBoxD4.Font = new Font("宋体", 14);
            textBoxD5.Font = new Font("宋体", 14);
            textBoxD6.Font = new Font("宋体", 14);
            textBoxE1.Font = new Font("宋体", 14);
            textBoxE2.Font = new Font("宋体", 14);
            textBoxE3.Font = new Font("宋体", 14);
            textBoxE4.Font = new Font("宋体", 14);
            textBoxE5.Font = new Font("宋体", 14);
            textBoxE6.Font = new Font("宋体", 14);
            textBoxF1.Font = new Font("宋体", 14);
            textBoxF2.Font = new Font("宋体", 14);
            textBoxF3.Font = new Font("宋体", 14);
            textBoxF4.Font = new Font("宋体", 14);
            textBoxF5.Font = new Font("宋体", 14);
            textBoxF6.Font = new Font("宋体", 14);
            timer1.Enabled = true;
        }
        public void textchangesmol()
        {
            timer1.Enabled = false;
            Thread.Sleep(100);
            textBoxA1.Font = new Font("宋体", 9);
            textBoxA2.Font = new Font("宋体", 9);
            textBoxA3.Font = new Font("宋体", 9);
            textBoxA4.Font = new Font("宋体", 9);
            textBoxA5.Font = new Font("宋体", 9);
            textBoxA6.Font = new Font("宋体", 9);
            textBoxB1.Font = new Font("宋体", 9);
            textBoxB2.Font = new Font("宋体", 9);
            textBoxB3.Font = new Font("宋体", 9);
            textBoxB4.Font = new Font("宋体", 9);
            textBoxB5.Font = new Font("宋体", 9);
            textBoxB6.Font = new Font("宋体", 9);
            textBoxC1.Font = new Font("宋体", 9);
            textBoxC2.Font = new Font("宋体", 9);
            textBoxC3.Font = new Font("宋体", 9);
            textBoxC4.Font = new Font("宋体", 9);
            textBoxC5.Font = new Font("宋体", 9);
            textBoxC6.Font = new Font("宋体", 9);
            textBoxD1.Font = new Font("宋体", 9);
            textBoxD2.Font = new Font("宋体", 9);
            textBoxD3.Font = new Font("宋体", 9);
            textBoxD4.Font = new Font("宋体", 9);
            textBoxD5.Font = new Font("宋体", 9);
            textBoxD6.Font = new Font("宋体", 9);
            textBoxE1.Font = new Font("宋体", 9);
            textBoxE2.Font = new Font("宋体", 9);
            textBoxE3.Font = new Font("宋体", 9);
            textBoxE4.Font = new Font("宋体", 9);
            textBoxE5.Font = new Font("宋体", 9);
            textBoxE6.Font = new Font("宋体", 9);
            textBoxF1.Font = new Font("宋体", 9);
            textBoxF2.Font = new Font("宋体", 9);
            textBoxF3.Font = new Font("宋体", 9);
            textBoxF4.Font = new Font("宋体", 9);
            textBoxF5.Font = new Font("宋体", 9);
            textBoxF6.Font = new Font("宋体", 9);
            timer1.Enabled = true;
        }
    }
}
