
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Data.SqlClient;
using System.IO;
using System.Data;

using CXPro001.classes;
using CXPro001.setups;
using CXPro001.ShowControl;

using MyLib.SqlHelper;
using MyLib.Files;
using MyLib.OldCtr;

namespace CXPro001.myclass
{
    /// <summary>
    /// 设备运行线程-单件流
    /// </summary>
    public class TaskCom
    {
        #region 实例化需要用到的参数和硬件设备类
        /// <summary>
        /// 铆点高度参数类  
        /// </summary>
        Highset highset = null;
        /// <summary>
        /// 铆点温度流通参数类
        /// </summary>
        ThermSet thermSet = null;
        /// <summary>
        /// 二维码规则类
        /// </summary>
        CordSet cordSet = null;
        /// <summary>
        /// 产品型号-从PLC读出来的
        /// </summary>
        int protype = 0;
        /// <summary>
        /// PLC
        /// </summary>
        s7com s71200 = null;
        /// <summary>
        /// 扫码器
        /// </summary>
        barcode_jienshi Scanjienshi = null;
        /// <summary>
        /// 刻字机
        /// </summary>
        Hanslaser hanslaser1 = null;
        /// <summary>
        /// 视觉主机
        /// </summary>
        visioncam viscam = null;
        #endregion
        #region 显示数据的控件
        /// <summary>
        /// 显示视觉检测结果的控件
        /// </summary>
        ChecksShowCtr checksShow = null;
        /// <summary>
        /// 显示温度的控件
        /// </summary>
        CXPro001.ShowControl.CCDHeightControls height_CCD_T = null;
        /// <summary>
        /// 显示流量的控件
        /// </summary>
        CXPro001.ShowControl.CCDHeightControls height_CCD_L = null;
        /// <summary>
        /// 显示铆点高度的控件
        /// </summary>
        CXPro001.ShowControl.CCDHeightControls height_CCD_H = null;
        /// <summary>
        /// 显示扫打扫的控件
        /// </summary>
        HanslerShowCtr hanslerShow1 = null;
        #endregion
        #region 个工序结果
        /// <summary>
        /// 螺纹检测结果
        /// </summary>
        bool result1 = false;
        /// <summary>
        /// 铜排检测结果
        /// </summary>
        bool result2 = false;
        /// <summary>
        /// 铆点检测结果
        /// </summary>
        bool result3 = false;
        /// <summary>
        /// 刻字结果
        /// </summary>
        bool result4 = false;
        /// <summary>
        /// 热铆温度
        /// </summary>
        bool result5 = false;
        /// <summary>
        /// 热铆风量
        /// </summary>
        bool result6 = false;
        /// <summary>
        /// 铆点位置检测结果
        /// </summary>
        bool result7 = false;
        /// <summary>
        /// 扫码结果
        /// </summary>
        bool result8 = false;
        /// <summary>
        /// 最终结果
        /// </summary>
        bool result9 = false;
        #endregion
        /// <summary>
        /// 要刻的二维码
        /// </summary>
        private string Cords = "";
        //构造
        public TaskCom(s7com S71200, visioncam Viscam, barcode_jienshi scanjienshi, Hanslaser Hanslaser1, Highset Highset1, ThermSet ThermSet1, CordSet CordSet1)
        {
          //  s71200 = hardware.s7_1200 ;
          //  viscam = hardware.viscam;
         //   Scanjienshi = hardware.jienshi1;
         //   hanslaser1 = hardware.hanslaser1;
            highset = Highset1;
            thermSet = ThermSet1;
            cordSet = CordSet1;
        }
        /// <summary>
        /// 导入控件
        /// </summary>
        /// <param name="checks">显示视觉检测结果的控件</param>
        /// <param name="height_CCDT">显示温度的控件</param>
        /// <param name="height_CCDL">显示流量的控件</param>
        /// <param name="height_CCDh">显示铆点高度的控件</param>
        /// <param name="hanslerShow">显示扫打扫的控件</param>
        public void showctrs(ChecksShowCtr checks,CXPro001.ShowControl.CCDHeightControls height_CCDT, CXPro001.ShowControl.CCDHeightControls height_CCDL, CXPro001.ShowControl.CCDHeightControls height_CCDh, HanslerShowCtr hanslerShow)
        {
            checksShow = checks;
            height_CCD_T = height_CCDT;
            height_CCD_L = height_CCDL;
            height_CCD_H = height_CCDh;
            hanslerShow1 = hanslerShow;
        }
        #region 运行线程
        Task run_task = null;
        public void run()
        {
            if (run_task == null || run_task != null && run_task.IsCompleted)
            {
                Logger.Write(Logger.InfoType.Info, "创建运行线程");
                if (run_task != null) run_task.Dispose();
                run_task = new Task(run_th);
                run_task.Start();
            }
        }
        private void run_th()
        {
            while(SysStatus.Status == SysStatus.EmSysSta.Run)
            {
                string D1 = "";
                EmRes ret = EmRes.Error;
                //螺纹检测1 DB10.X20.0
                ret = s71200.RWS71200(true, "DB", 10, "20.0", ref D1, 3);
                if (ret == EmRes.Succeed && D1 == "ON" || SysStatus.NO1) 
                {
                    StepNo1_1();
                    Thread.Sleep(100);
                }
                D1 = "";
                //螺纹检测2 DB10.X30.0
                ret = s71200.RWS71200(true, "DB", 10, "30.0", ref D1, 3);
                if (ret == EmRes.Succeed && D1 == "ON" || SysStatus.NO1)
                {
                    StepNo1_2();
                    Thread.Sleep(100);
                }
                D1 = "";
                //螺纹检测3 DB10.X40.0
                ret = s71200.RWS71200(true, "DB", 10, "40.0", ref D1, 3);
                if (ret == EmRes.Succeed && D1 == "ON" || SysStatus.NO1)
                {
                    StepNo1_3();
                    Thread.Sleep(100);
                }            
                //铜排检测 DB10.X50.0
                D1 = "";
                ret = s71200.RWS71200(true, "DB", 10, "50.0", ref D1, 3);
                if (ret == EmRes.Succeed && D1 == "ON" || SysStatus.NO2)
                {
                    StepNo2(); Thread.Sleep(500);
                }
                //刻字 DB10.X60.0
                D1 = "";
                ret = s71200.RWS71200(true, "DB", 10, "60.0", ref D1, 3);
                if (ret == EmRes.Succeed && D1 == "ON" || SysStatus.NO4)
                {
                    StepNo3(); Thread.Sleep(400);
                }
                //铆点温度检测 DB10.X70.0
                D1 = "";
                ret = s71200.RWS71200(true, "DB", 10, "70.0", ref D1, 3);
                if (ret == EmRes.Succeed && D1 == "ON" || SysStatus.NO5)
                {
                    StepNo4(); Thread.Sleep(500);
                }
                //铆点检测 DB10.X80.0
                D1 = "";
                ret = s71200.RWS71200(true, "DB", 10, "80.0", ref D1, 3);
                if (ret == EmRes.Succeed && D1 == "ON" || SysStatus.NO3)
                {
                    StepNo5(); Thread.Sleep(500);
                }
                             
                Thread.Sleep(50);
                ////铆点位置度检测 DB10.X70.0
                //D1 = "";
                //ret = s71200.RWS71200(true, "DB", 10, "70.0", ref D1, 3);
                //if (ret == EmRes.Succeed && D1 == "ON" || SysStatus.NO7)
                //{
                //    StepNo7(); Thread.Sleep(500);
                //}
                //Thread.Sleep(50);

                //扫码DB10.X90.0
                D1 = "";
                ret = s71200.RWS71200(true, "DB", 10, "90.0", ref D1, 3);
                if (ret == EmRes.Succeed && D1 == "ON" || SysStatus.NO8)
                {
                    StepNo8(); Thread.Sleep(100);
                }
                Thread.Sleep(50);
                //下料DB10.X100.0
                D1 = "";
                ret = s71200.RWS71200(true, "DB", 10, "100.0", ref D1, 3);
                if (ret == EmRes.Succeed && D1 == "ON" || SysStatus.NO9)
                {
                    StepNo9();
                    //存生产计数
                    UptataP("ST1", ST1CountOK, ST1CountNG);
                    UptataP("ST2", ST2CountOK, ST2CountNG);
                    UptataP("ST3", ST3CountOK, ST3CountNG);
                    UptataP("ST4", ST4CountOK, ST4CountNG);
                    UptataP("ST5", ST5CountOK, ST5CountNG);
                    UptataP("ST6", ST6CountOK, ST6CountNG);
                    UptataP("ST7", ST7CountOK, ST7CountNG);
                    UptataP("ST8", ST8CountOK, ST8CountNG);
                    Thread.Sleep(200);
                }

            }
        }
        #region 每个工位动作
        /// <summary>
        /// 第一步动作-螺纹检测1
        /// </summary>
        private void StepNo1_1()
        {
            EmRes ret = EmRes.Succeed;
            Cords = cordSet.GetCord();
            checksShow.showcord(Cords);
            string res = "";//返回的信息
            if (viscam.SendTs("T11+" + Cords + "+" + "1" + "\r\n", ref res) != EmRes.Succeed) //发送没有返回信息
            {
                result1 = false;
                //螺纹检测失败
                checksShow.showluowen1("NG");
                //发送结果DB11.INT18.0  DB11.X16.0
                string D1 = "2";
                if (ret != s71200.RWS71200(false, "DB", 11, "18.0", ref D1, 1))
                {
                  if(  ret != s71200.RWS71200(false, "DB", 11, "18.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT18.0=2 失败");
                    }

                }
                D1 = "ON";

               if( ret != s71200.RWS71200(false, "DB", 11, "16.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "16.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X16.0=ON 失败");
                    }
                }
                SaveLastSta("result1", result1.ToString());
                ST1CountNG++;
                return;
            }

            string[] aa = res.Split(',');
            if(aa[1]=="OK")
            {
                result1 = true;
                checksShow.showluowen1("OK");
                string D1 = "1";
                if (ret != s71200.RWS71200(false, "DB", 11, "18.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "18.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT18.0=1 失败");
                    }
                }
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "16.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "16.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X16.0=ON 失败");
                    }
                }
                SaveLastSta("result1", result1.ToString());
                ST1CountOK++;
                return;
            }
            else
            {
                result1 = false;
                checksShow.showluowen1("NG");
                string D1 = "2";
                if (ret != s71200.RWS71200(false, "DB", 11, "18.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "18.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT18.0=2 失败");
                    }
                }
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "16.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "16.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X16.0=ON 失败");
                    }
                }
                SaveLastSta("result1", result1.ToString());
                ST1CountNG++;
                return;
            }
        }
        /// <summary>
        /// 第一步动作-螺纹检测2
        /// </summary>
        private void StepNo1_2()
        {
            EmRes ret = EmRes.Succeed;
            Cords = cordSet.GetCord();
            checksShow.showcord(Cords);
            string res = "";//返回的信息
            if (viscam.SendTs("T11+" + Cords + "+" + "1" + "\r\n", ref res) != EmRes.Succeed) //发送没有返回信息
            {
                result1 = false;
                //螺纹检测失败
                checksShow.showluowen2("NG");
                //发送结果DB11.INT24.0  DB11.X22.0
                string D1 = "2";
                if (ret != s71200.RWS71200(false, "DB", 11, "24.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "24.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT24.0=2 失败");
                    }

                }
                D1 = "ON";

                if (ret != s71200.RWS71200(false, "DB", 11, "22.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "22.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X22.0=ON 失败");
                    }
                }
                SaveLastSta("result1", result1.ToString());
                ST1CountNG++;
                return;
            }

            string[] aa = res.Split(',');
            if (aa[1] == "OK")
            {
                result1 = true;
                checksShow.showluowen2("OK");
                string D1 = "1";
                if (ret != s71200.RWS71200(false, "DB", 11, "24.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "24.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT24.0=1 失败");
                    }
                }
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "22.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "22.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X22.0=ON 失败");
                    }
                }
                SaveLastSta("result1", result1.ToString());
                ST1CountOK++;
                return;
            }
            else
            {
                result1 = false;
                checksShow.showluowen2("NG");
                string D1 = "2";
                if (ret != s71200.RWS71200(false, "DB", 11, "24.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "24.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT24.0=2 失败");
                    }
                }
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "22.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "22.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X22.0=ON 失败");
                    }
                }
                SaveLastSta("result1", result1.ToString());
                ST1CountNG++;
                return;
            }
        }
        /// <summary>
        /// 第一步动作-螺纹检测3
        /// </summary>
        private void StepNo1_3()
        {
            EmRes ret = EmRes.Succeed;
            Cords = cordSet.GetCord();
            checksShow.showcord(Cords);
            string res = "";//返回的信息
            if (viscam.SendTs("T11+" + Cords + "+" + "1" + "\r\n", ref res) != EmRes.Succeed) //发送没有返回信息
            {
                result1 = false;
                //螺纹检测失败
                checksShow.showluowen3("NG");
                //发送结果DB11.INT30.0  DB11.X28.0
                string D1 = "2";
                if (ret != s71200.RWS71200(false, "DB", 11, "30.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "30.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT30.0=2 失败");
                    }

                }
                D1 = "ON";

                if (ret != s71200.RWS71200(false, "DB", 11, "28.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "28.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X28.0=ON 失败");
                    }
                }
                SaveLastSta("result1", result1.ToString());
                ST1CountNG++;
                return;
            }

            string[] aa = res.Split(',');
            if (aa[1] == "OK")
            {
                result1 = true;
                checksShow.showluowen3("OK");
                string D1 = "1";
                if (ret != s71200.RWS71200(false, "DB", 11, "30.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "30.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT30.0=1 失败");
                    }
                }
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "28.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "28.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X28.0=ON 失败");
                    }
                }
                SaveLastSta("result1", result1.ToString());
                ST1CountOK++;
                return;
            }
            else
            {
                result1 = false;
                checksShow.showluowen3("NG");
                string D1 = "2";
                if (ret != s71200.RWS71200(false, "DB", 11, "30.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "30.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT30.0=2 失败");
                    }
                }
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "28.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "28.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X28.0=ON 失败");
                    }
                }
                SaveLastSta("result1", result1.ToString());
                ST1CountNG++;
                return;
            }
        }

        /// <summary>
        /// 第二步动作-铜板检测
        /// </summary>
        private void StepNo2()
        {
            EmRes ret = EmRes.Succeed;
            string res = "";//返回的信息
            if (viscam.SendTs("T21+" + Cords + "+" + "1" + "\r\n", ref res) != EmRes.Succeed) //发送没有返回信息
            {
                result2 = false;
                //铜排检测失败
                checksShow.showCupai( "NG");
                //发送结果DB11.INT36.0  DB11.X34.0
                string D1 = "2";
                if (ret != s71200.RWS71200(false, "DB", 11, "36.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "36.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT36.0=2 失败");
                    }
                }
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "34.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "34.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X34.0=ON 失败");
                    }
                }
                SaveLastSta("result2", result2.ToString());
                ST2CountNG++;
                return;
            }

            string[] aa = res.Split(',');
            if (aa[1] == "OK")
            {
                result2 = true;
                checksShow.showCupai("OK");
                string D1 = "1";
                if (ret != s71200.RWS71200(false, "DB", 11, "36.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "36.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT24.0=1 失败");
                    }
                }
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "34.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "34.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X22.0=ON 失败");
                    }
                }
                SaveLastSta("result2", result2.ToString());
                ST2CountOK++;
                return;
            }
            else
            {
                result2 = false;
                checksShow.showCupai("NG");
                string D1 = "2";
                if (ret != s71200.RWS71200(false, "DB", 11, "36.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "36.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT24.0=2 失败");
                    }
                }
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "34.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "34.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X22.0=ON 失败");
                    }
                }
                SaveLastSta("result2", result2.ToString());
                ST2CountNG++;
                return;
            }
        }
        /// <summary>
        /// 铆点检测
        /// </summary>
        private void StepNo5()
        {
            EmRes ret = EmRes.Succeed;
            string res = "";//返回的信息
            if (viscam.SendTs("T31+" + Cords + "+" + "1" + "\r\n", ref res) != EmRes.Succeed) //发送没有返回信息
            {
                result3 = false;
                //铜排检测失败
                checksShow.showMaodian("NG");
                //发送结果DB11.INT54.0  DB11.X52.0
                string D1 = "2";
                if (ret != s71200.RWS71200(false, "DB", 11, "54.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "54.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT54.0=2 失败");
                    }
                }
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "52.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "52.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X54.0=ON 失败");
                    }
                }
                SaveLastSta("result3", result3.ToString());
                ST3CountNG++;
                return;
            }
            string[] aa = res.Split(',');
            if (aa[1] == "OK")
            {
                result3 = true;
                checksShow.showMaodian( "OK");
                string D1 = "1";
                if (ret != s71200.RWS71200(false, "DB", 11, "54.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "54.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT54.0=1 失败");
                    }
                }
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "52.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "52.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X52.0=ON 失败");
                    }
                }
                SaveLastSta("result3", result3.ToString());
                ST3CountOK++;
                return;
            }
            else
            {
                result3 = false;
                checksShow.showMaodian(  "NG");
                string D1 = "2";
                if (ret != s71200.RWS71200(false, "DB", 11, "54.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "54.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT54.0=2 失败");
                    }
                }
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "52.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "52.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X52.0=ON 失败");
                    }
                }
                SaveLastSta("result3", result3.ToString());
                ST3CountNG++;
                return;
            }
        }
        /// <summary>
        /// 打码
        /// </summary>
        private void StepNo3()
        {
            hanslerShow1.clearshow();
          //  hanslerShow1.Showtype(SysStatus.CurProductName, Cords);
            EmRes ret = EmRes.Succeed;
            //检查二维码重码
            if (SQLHelper.IsCordRepeat(Cords,SysStatus.CurProductName)) 
            {
                //重码
                result4 = false;
                //刻字失败
                hanslerShow1.Showmode("二维码重码:NG");
                //发送结果DB11.X42.0		DB11.INT40.0
                string D1 = "2";
                if (ret != s71200.RWS71200(false, "DB", 11, "42.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "42.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT42.0 = 2 失败");
                    }
                }
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "40.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "40.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X40.0 =ON 失败");
                    }
                }
                SaveLastSta("result4", result4.ToString());
                ST4CountNG++;
                return;
            }
            string initialize = "\u0002$Initialize_HS_" + SysStatus.CurProductName + "\u0003";
            string send = "\u0002$Data_" + Cords + "\u0003";
            string markStart = "\u0002$MarkStart_\u0003";          
            if (!hanslaser1.Sendmodel(initialize))
            {               
                result4 = false;
                //刻字失败
                hanslerShow1.Showmode("发送模板失败:NG");
                //发送结果DB11.X42.0		DB11.INT40.0
                string D1 = "2";
                if (ret != s71200.RWS71200(false, "DB", 11, "42.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "42.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT42.0 = 2 失败");
                    }
                }
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "40.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "40.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X40.0 =ON 失败");
                    }
                }
                SaveLastSta("result4", result4.ToString());
                ST4CountNG++;
                return;
            }
            Thread.Sleep(50);
            if (!hanslaser1.Sendmodel(send))
            {
                result4 = false;
                //刻字失败
                hanslerShow1.Showmode("发送刻字内容失败:NG");
                //发送结果DB11.X42.0		DB11.INT40.0
                string D1 = "2";
                if (ret != s71200.RWS71200(false, "DB", 11, "42.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "42.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT42.0 = 2 失败");
                    }
                }
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "40.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "40.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X40.0 =ON 失败");
                    }
                }
                SaveLastSta("result4", result4.ToString());
                ST4CountNG++;
                return;
            }
            Thread.Sleep(50);
            if (!hanslaser1.Sendmodel(markStart))
            {
                result4 = false;
                //刻字失败
                hanslerShow1.Showmode("启动刻字失败:NG");
                //发送结果DB11.X42.0		DB11.INT40.0
                string D1 = "2";
                if (ret != s71200.RWS71200(false, "DB", 11, "42.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "42.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT42.0 = 2 失败");
                    }
                }
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "40.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "40.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X40.0 =ON 失败");
                    }
                }
                SaveLastSta("result4", result4.ToString());
                ST4CountNG++;
                return;
            }
            result4 = true;
            //刻字成功
            hanslerShow1.Showmode("刻字正常完成:OK");
            //发送结果DB11.X42.0		DB11.INT40.0
            string D2 = "1";
            if (ret != s71200.RWS71200(false, "DB", 11, "42.0", ref D2, 1))
            {
                if (ret != s71200.RWS71200(false, "DB", 11, "42.0", ref D2, 1))
                {
                    Logger.Error("写入PLC地址DB11.INT36.0 = 2 失败");
                }
            }
            D2 = "ON";
            if (ret != s71200.RWS71200(false, "DB", 11, "40.0", ref D2, 3))
            {
                if (ret != s71200.RWS71200(false, "DB", 11, "40.0", ref D2, 3))
                {
                    Logger.Error("写入PLC地址 DB11.X40.0 =ON 失败");
                }
            }
            SaveLastSta("result4", result4.ToString());
            ST4CountOK++;
            return;
        }
        /// <summary>
        /// 铆点温度检测
        /// </summary>
        private void StepNo4()
        {
            result5 = true;
            height_CCD_T.clearshow();
            EmRes ret = EmRes.Succeed;
            string D1="", D2="", D3 = "";
            //返回的信息 DB10,Real 110
            if (ret != s71200.RWS71200(true, "DB", 10, "110.0", ref D1, 1))
            {
                if (ret != s71200.RWS71200(true, "DB", 10, "110.0", ref D1, 1))
                {
                    Logger.Error("读取PLC地址DB10,Real 110失败");
                }
            }
            //返回的信息 DB10,Real 114
            if (ret != s71200.RWS71200(true, "DB", 10, "114.0", ref D2, 1))
            {
                if (ret != s71200.RWS71200(true, "DB", 10, "114.0", ref D2, 1))
                {
                    Logger.Error("读取PLC地址DB10,Real 114失败");
                }
            }
            //返回的信息 DB10,Real 118
            if (ret != s71200.RWS71200(true, "DB", 10, "118.0", ref D3, 1))
            {
                if (ret != s71200.RWS71200(true, "DB", 10, "118.0", ref D3, 1))
                {
                    Logger.Error("读取PLC地址DB10,Real 118失败");
                }
            }
            string res = "OK" + "," + D1 + "," + D2 + "," + D3;
            //height_CCD_T.showres(res, 3);
            SaveLastSta("result5", result5.ToString());
            StepNo4_1();
        }
        /// <summary>
        /// 铆点风流量检测
        /// </summary>
        private void StepNo4_1()
        {
            result6 = true;
            height_CCD_L.clearshow();
            EmRes ret = EmRes.Succeed;
            string D1 = "", D2 = "", D3 = "";
            //返回的信息 DB10,Real 122
            if (ret != s71200.RWS71200(true, "DB", 10, "122.0", ref D1, 1))
            {
                if (ret != s71200.RWS71200(true, "DB", 10, "122.0", ref D1, 1))
                {
                    Logger.Error("读取PLC地址DB10,Real 122失败");
                }
            }
            //返回的信息 DB10,Real 126
            if (ret != s71200.RWS71200(true, "DB", 10, "126.0", ref D2, 1))
            {
                if (ret != s71200.RWS71200(true, "DB", 10, "126.0", ref D2, 1))
                {
                    Logger.Error("读取PLC地址DB10,Real 126失败");
                }
            }
            //返回的信息 DB10,Real 130
            if (ret != s71200.RWS71200(true, "DB", 10, "130.0", ref D3, 1))
            {
                if (ret != s71200.RWS71200(true, "DB", 10, "130.0", ref D3, 1))
                {
                    Logger.Error("读取PLC地址DB10,Real 130失败");
                }
            }

            string res = "OK" + "," + D1 + "," + D2 + "," + D3;
            SaveLastSta("result6", result6.ToString());
           // height_CCD_L.showres(res, 3);
            ST5CountOK ++;
            //发送结果DB11.X48.0		DB11.INT46.0
             D2 = "1";
            if (ret != s71200.RWS71200(false, "DB", 11, "46.0", ref D2, 1))
            {
                if (ret != s71200.RWS71200(false, "DB", 11, "46.0", ref D2, 1))
                {
                    Logger.Error("写入PLC地址DB11.INT46.0 = 2 失败");
                }
            }
            D2 = "ON";
            if (ret != s71200.RWS71200(false, "DB", 11, "48.0", ref D2, 3))
            {
                if (ret != s71200.RWS71200(false, "DB", 11, "48.0", ref D2, 3))
                {
                    Logger.Error("写入PLC地址 DB11.X48.0 =ON 失败");
                }
            }
        }
        /// <summary>
        /// 铆点高度/位置度检测
        /// </summary>
        private void StepNo7()
        {
            EmRes ret = EmRes.Succeed;
            height_CCD_H.clearshow();
            height_CCD_H.showtype(SysStatus.CurProductName);
           
            string res = "";//返回的信息
            if (viscam.SendTs("T41+" + Cords + "+" + "1" + "\r\n", ref res) != EmRes.Succeed) //发送没有返回信息
            {
                result7 = false;
                //铆点位置检测失败
             //   height_CCD_H.showres($"NG,NA1,NA2,NA3,NA4,NA5,NA6,NA7,NA8,", 8);
                //发送结果DB11.INT48.0  DB11.X46.0
                string D1 = "2";
                if (ret != s71200.RWS71200(false, "DB", 11, "48.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "48.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT48.0=2 失败");
                    }
                }
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "46.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "46.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X46.0=ON 失败");
                    }
                }
                SaveMaoDianHigh("NA", "NA", "NA", "NA", "NA", "NA", "NA", "NA");
                SaveLastSta("result7", result7.ToString());
                ST6CountNG++;
                return;
            }

            string[] aa = res.Split(',');
            if (aa[1] == "OK")
            {
                //判断上下限有没有超
                #region 上下限判断
                string resstrng = "";
                int resint = 0;
                double DM1 = Convert.ToDouble(aa[6]);
                double DM2 = Convert.ToDouble(aa[7]);
                double DM3 = Convert.ToDouble(aa[8]);
                double DM4 = Convert.ToDouble(aa[9]);
                double DM5 = Convert.ToDouble(aa[10]);
                double DM6 = Convert.ToDouble(aa[11]);
                double DM7 = Convert.ToDouble(aa[12]); 
                double DM8 = Convert.ToDouble(aa[13]);
                if (highset.DW1 < DM1 && DM1 < highset.UP1)                  
                {                    
                }
                else
                {
                    resint++;
                    resstrng += "NA1,";
                }
                if (highset.DW2 < DM2 && DM2 < highset.UP2)
                {
                }
                else
                {
                    resint++;
                    resstrng += "NA2,";
                }
                if (highset.DW3 < DM3 && DM3 < highset.UP3)
                {
                }
                else
                {
                    resint++;
                    resstrng += "NA3,";
                }
                if (highset.DW4 < DM4 && DM4 < highset.UP4)
                {
                }
                else
                {
                    resint++;
                    resstrng += "NA4,";
                }
                if (highset.DW5 < DM5 && DM5 < highset.UP5)
                {
                }
                else
                {
                    resint++;
                    resstrng += "NA5,";
                }
                if (highset.DW6 < DM6 && DM6 < highset.UP6)
                {
                }
                else
                {
                    resint++;
                    resstrng += "NA6,";
                }
                if (highset.DW7 < DM7 && DM7 < highset.UP7)
                {
                }
                else
                {
                    resint++;
                    resstrng += "NA7,";
                }
                if (highset.DW8 < DM8 && DM8 < highset.UP8)
                {
                }
                else
                {
                    resint++;
                    resstrng += "NA8,";
                }
                if (resint > 0)
                { result7 = false;
                    //height_CCD_H.showres($"NG,{aa[6]},{aa[7]},{aa[8]},{aa[9]},{aa[10]},{aa[11]},{aa[12]},{aa[13]},{resstrng}", 8);
                    ST6CountOK++;
                }
                else
                { result7 = true;
                    //.showres($"OK,{aa[6]},{aa[7]},{aa[8]},{aa[9]},{aa[10]},{aa[11]},{aa[12]},{aa[13]},{resstrng}", 8);
                    ST6CountNG++;
                }
                
                #endregion
                
                string D1 = "1";
                if (result7) D1 = "1";
                else D1 = "2";
                if (ret != s71200.RWS71200(false, "DB", 11, "48.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "48.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT48.0=1 失败");
                    }
                }
                int ret1=0;
                ret1 += (int)s71200.RWS71200(false, "DB", 11, "70.0", ref aa[6], 2);
                ret1 += (int)s71200.RWS71200(false, "DB", 11, "74.0", ref aa[7], 2);
                ret1 += (int)s71200.RWS71200(false, "DB", 11, "78.0", ref aa[8], 2);
                ret1 += (int)s71200.RWS71200(false, "DB", 11, "82.0", ref aa[9], 2);
                ret1 += (int)s71200.RWS71200(false, "DB", 11, "86.0", ref aa[10], 2);
                ret1 += (int)s71200.RWS71200(false, "DB", 11, "90.0", ref aa[11], 2);
                ret1 += (int)s71200.RWS71200(false, "DB", 11, "94.0", ref aa[12], 2);
                ret1 += (int)s71200.RWS71200(false, "DB", 11, "98.0", ref aa[13], 2);
                if (ret > 0) Logger.Error("写入PLC铆点高度值出错");
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "46.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "46.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X46.0=ON 失败");
                    }
                }
                SaveMaoDianHigh(aa[6], aa[7], aa[8], aa[9], aa[10], aa[11], aa[12], aa[13]);
                return;
            }
            else
            {
                result7 = false;
                //铆点位置检测失败
             //   height_CCD_H.showres($"NG,{aa[6]},{aa[7]},{aa[8]},{aa[9]},{aa[10]},{aa[11]},{aa[12]},{aa[13]},NA1,NA2,NA3,NA4,NA5,NA6,NA7,NA8,", 8);
                string D1 = "2";
                if (ret != s71200.RWS71200(false, "DB", 11, "48.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "48.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT48.0=2 失败");
                    }
                }
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "46.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "46.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X46.0=ON 失败");
                    }
                }
                SaveMaoDianHigh(aa[6], aa[7], aa[8], aa[9], aa[10], aa[11], aa[12], aa[13]);
                SaveLastSta("result7", result7.ToString());
                ST6CountNG++;
                return;
            }


        }
        /// <summary>
        /// 扫码
        /// </summary>
        private void StepNo8()
        {        
            EmRes ret = EmRes.Succeed;
            //启动扫码
            string cordsao = "";
            if (!Scanjienshi.SeconScan(ref cordsao, 1, 30))
            {
                result8 = false;
                hanslerShow1.ShowseconSao($"扫码失败{cordsao}，NG");
                //发送结果DB11.X58.0		DB11.INT60.0
                string D1 = "2";
                if (ret != s71200.RWS71200(false, "DB", 11, "60.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "60.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT60.0 = 2 失败");
                    }
                }
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "58.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "58.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X58.0 =ON 失败");
                    }
                }
                ST7CountNG++;
                return;
            }
            //检查二维码是否存在
            if (!SQLHelper.IsCordRepeat(cordsao,SysStatus.CurProductName))
            {
                //二维码不存在                           
               
                Logger.Warning($"扫到的码在数据库中不存在{cordsao}，NG");
                if(cordsao==Cords)
                {
                    result8 = true;
                    hanslerShow1.ShowseconSao($"扫刻一致{cordsao}，OK");
                    //发送结果DB11.X58.0		DB11.INT60.0
                    string D1 = "1";
                    if (ret != s71200.RWS71200(false, "DB", 11, "60.0", ref D1, 1))
                    {
                        if (ret != s71200.RWS71200(false, "DB", 11, "60.0", ref D1, 1))
                        {
                            Logger.Error("写入PLC地址DB11.INT60.0 = 2 失败");
                        }
                    }
                    D1 = "ON";
                    if (ret != s71200.RWS71200(false, "DB", 11, "58.0", ref D1, 3))
                    {
                        if (ret != s71200.RWS71200(false, "DB", 11, "58.0", ref D1, 3))
                        {
                            Logger.Error("写入PLC地址 DB11.X58.0 =ON 失败");
                        }
                    }
                    SaveLastSta("result8", result8.ToString());
                    ST7CountNG++;
                    return;
                }
                else
                {
                    result8 = false;
                    hanslerShow1.ShowseconSao($"扫刻不一致{cordsao}，NG");
                    //发送结果DB11.X58.0		DB11.INT60.0
                    string D1 = "2";
                    if (ret != s71200.RWS71200(false, "DB", 11, "T60.0", ref D1, 1))
                    {
                        if (ret != s71200.RWS71200(false, "DB", 11, "T60.0", ref D1, 1))
                        {
                            Logger.Error("写入PLC地址DB11.INT60.0 = 2 失败");
                        }
                    }
                    D1 = "ON";
                    if (ret != s71200.RWS71200(false, "DB", 11, "58.0", ref D1, 3))
                    {
                        if (ret != s71200.RWS71200(false, "DB", 11, "58.0", ref D1, 3))
                        {
                            Logger.Error("写入PLC地址 DB11.X58.0 =ON 失败");
                        }
                    }
                    SaveLastSta("result8", result8.ToString());
                    ST7CountNG++;
                    return;
                }
                
            }
            if (cordsao == Cords)
            {
                result8 = true;
                hanslerShow1.ShowseconSao($"扫刻一致{cordsao}，OK");
                //发送结果DB11.X58.0		DB11.INT60.0
                string D1 = "1";
                if (ret != s71200.RWS71200(false, "DB", 11, "60.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "60.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT60.0 = 2 失败");
                    }
                }
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "58.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "58.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X58.0 =ON 失败");
                    }
                }
                SaveLastSta("result8", result8.ToString());
                ST7CountOK++;
                return;
            }
            else
            {
                result8 = false;
                hanslerShow1.ShowseconSao($"扫刻不一致{cordsao}，NG");
                //发送结果DB11.X58.0		DB11.INT60.0
                string D1 = "2";
                if (ret != s71200.RWS71200(false, "DB", 11, "60.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "60.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT60.0 = 2 失败");
                    }
                }
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "58.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "58.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X58.0 =ON 失败");
                    }
                }
                SaveLastSta("result8", result8.ToString());
                ST7CountNG++;
                return;
            }
        }
        /// <summary>
        /// 下料
        /// </summary>
        private void StepNo9()
        {
            EmRes ret = EmRes.Succeed;
            if (result1 && result2 && result3 && result4  && result8)//&& result7
            {
                //合格
                result9 = true;
                //发送结果DB11.X64.0		DB11.INT66.0
                string D1 = "1";
                if (ret != s71200.RWS71200(false, "DB", 11, "66.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "66.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT66.0 = 2 失败");
                    }
                }
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "64.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "64.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X58.0 =ON 失败");
                    }
                }
                ST8CountOK++;
            }
            else
            {
                result9 = false;
                //发送结果DB11.X56.0		DB11.INT60.0
                string D1 = "2";
                if (ret != s71200.RWS71200(false, "DB", 11, "66.0", ref D1, 1))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "66.0", ref D1, 1))
                    {
                        Logger.Error("写入PLC地址DB11.INT66.0 = 2 失败");
                    }
                }
                D1 = "ON";
                if (ret != s71200.RWS71200(false, "DB", 11, "64.0", ref D1, 3))
                {
                    if (ret != s71200.RWS71200(false, "DB", 11, "64.0", ref D1, 3))
                    {
                        Logger.Error("写入PLC地址 DB11.X64.0 =ON 失败");
                    }
                }
                ST8CountNG++;
            }
            SaveData();
            ShowRes();
            ClearSta();
        }
        #endregion
        #endregion
        #region 显示最终结果组  存入数据库  铆点高度数据存入数据库
        ShowLED showres1 = null;
        ShowLED showres2 = null;
        ShowLED showres3 = null;
        ShowLED showres4 = null;
        ShowLED showres5 = null;
        ShowLED showres6 = null;
        ShowLED showres7 = null;
        ShowLED showres8 = null;
        ShowLED showres9 = null;
        public void ResCtrShow(ShowLED LED1, ShowLED LED2, ShowLED LED3, ShowLED LED4, ShowLED LED5, ShowLED LED6, ShowLED LED7, ShowLED LED8, ShowLED LED9)
        {
            showres1 = LED1;
            showres2 = LED2;
            showres3 = LED3;
            showres4 = LED4;
            showres5 = LED5;
            showres6 = LED6;
            showres7 = LED7;
            showres8 = LED8;
            showres9 = LED9;
        }
       /// <summary>
       /// 将最终的结果显示到LED控件上
       /// </summary>
        public void ShowRes()
        {
           
                if (result1) showres1.Color1 = Color.Green;
                else showres1.Color1 = Color.Red;
                if (result2) showres2.Color1 = Color.Green;
                else showres2.Color1 = Color.Red;
                if (result3) showres3.Color1 = Color.Green;
                else showres3.Color1 = Color.Red;
                if (result4) showres4.Color1 = Color.Green;
                else showres4.Color1 = Color.Red;
                if (result5) showres5.Color1 = Color.Green;
                else showres5.Color1 = Color.Red;
                if (result6) showres6.Color1 = Color.Green;
                else showres6.Color1 = Color.Red;
                //if (result7) showres7.Color1 = Color.Green;
                //else showres7.Color1 = Color.Red;
                if (result8) showres8.Color1 = Color.Green;
                else showres8.Color1 = Color.Red;
                if (result9) showres9.Color1 = Color.Green;
                else showres9.Color1 = Color.Red;
 
        }
        /// <summary>
        /// 将结果存入数据库
        /// </summary>
        /// <returns></returns>
        private bool SaveData()
        {
           
            string sqlTET = "insert into ResultData( InsertTime,ProductType,Cord,WhorlCheck,CuCheck,MaoDianCheck,Marking,MaoDianT,MaoDianHigh,ScanCord,ResultS)" +
                               " values( @InsertTime,@ProductType,@Cord,@WhorlCheck,@CuCheck,@MaoDianCheck,@Marking,@MaoDianT,@MaoDianHigh,@ScanCord,@ResultS)";
            SqlParameter[] param = new SqlParameter[]
                        { new SqlParameter("@InsertTime", DateTime.Now),
                          new SqlParameter("@ProductType",SysStatus.CurProductName),
                          new SqlParameter("@Cord", Cords),
                          new SqlParameter("@WhorlCheck", result1),
                          new SqlParameter("@CuCheck", result2),
                          new SqlParameter("@MaoDianCheck", result3),
                          new SqlParameter("@Marking", result4),
                          new SqlParameter("@MaoDianT", result5),
                          new SqlParameter("@MaoDianHigh", result7),
                          new SqlParameter("@ScanCord", result8),
                          new SqlParameter("@ResultS", result9),
                          
              };
            int a = SQLHelper.Update(sqlTET, param);
            if (a <= 0)
            {
                Logger.Error("最终结果组写入数据库失败！");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 铆点高度存入数据库
        /// </summary>
        /// <returns></returns>
        private bool SaveMaoDianHigh(string HA, string HB, string HC, string HD, string HE, string HF, string HG, string HH)
        {
             
            string sqlTET = "insert into MaoDianHigh( InsertTime,ProductType,Cord,HighA,HighB,HighC,HighD,HighE,HighF,HighG,HighH,ResultS)" +
                               " values( @InsertTime,@ProductType,@Cord,@HighA,@HighB,@HighC,@HighD,@HighE,@HighF,@HighG,@HighH,@ResultS)";
            SqlParameter[] param = new SqlParameter[]
                        { new SqlParameter("@InsertTime", DateTime.Now),
                          new SqlParameter("@ProductType",SysStatus.CurProductName),
                          new SqlParameter("@Cord", Cords),
                          new SqlParameter("@HighA",HA),
                          new SqlParameter("@HighB", HB),
                          new SqlParameter("@HighC", HC),
                          new SqlParameter("@HighD", HD),
                          new SqlParameter("@HighE", HE),
                          new SqlParameter("@HighF", HF),
                          new SqlParameter("@HighG", HG),
                          new SqlParameter("@HighH", HH),
                          new SqlParameter("@ResultS", result7),

              };
            int a = SQLHelper.Update(sqlTET, param);
            if (a <= 0)
            {
                Logger.Error("铆点高度检测结果数据组写入数据库失败！");
                return false;
            }
            return true;
        }

        #endregion
        #region 最后状态的存取
        /// <summary>
        /// 获取最后退出的状态
        /// </summary>
        public void LoadLastSta()
        {
           string filename = $"{Path.GetFullPath("..")}\\syscfg\\LastSta.ini";// 配置文件路径
            
            IniFile inf = new IniFile(filename);//确认路径是否存在，不存在则创建文件夹。
            try
            {
                string STEP = "Station";
                Cords = inf.ReadString(STEP, "Cords", Cords);
                result1 = inf.ReadBool(STEP, "result1", result1);
                result2 = inf.ReadBool(STEP, "result2", result2);
                result3 = inf.ReadBool(STEP, "result3", result3);
                result4 = inf.ReadBool(STEP, "result4", result4);
                result5 = inf.ReadBool(STEP, "result5", result5);
                result6 = inf.ReadBool(STEP, "result6", result6);
                result7 = inf.ReadBool(STEP, "result7", result7);
                result8 = inf.ReadBool(STEP, "result8", result8);
                result9 = inf.ReadBool(STEP, "result9", result9);
            }
            catch (Exception ex)
            {
                Logger.Error($"获取LastSta出错{ex.Message}");
            }            
        }
        /// <summary>
        /// 清除状态
        /// </summary>
        public void ClearSta()
        {
            string filename = $"{Path.GetFullPath("..")}\\syscfg\\LastSta.ini";// 配置文件路径
            IniFile inf = new IniFile(filename);//确认路径是否存在，不存在则创建文件夹。
            string STEP = "Station";

            inf.WriteString(STEP, "Cords", "");
            inf.WriteBool(STEP, "result1", false);
            inf.WriteBool(STEP, "result2", false);
            inf.WriteBool(STEP, "result3", false);
            inf.WriteBool(STEP, "result4", false);
            inf.WriteBool(STEP, "result5", false);
            inf.WriteBool(STEP, "result6", false);
            inf.WriteBool(STEP, "result7", false);
        }
        /// <summary>
        /// 保存状态，下一次启用时可以接着做
        /// </summary>
        /// <param name="staname"></param>
        /// <param name="values"></param>
        public void SaveLastSta(string staname,string values)
        {
            string filename = $"{Path.GetFullPath("..")}\\syscfg\\LastSta.ini";// 配置文件路径
            IniFile inf = new IniFile(filename);//确认路径是否存在，不存在则创建文件夹。
            string STEP = "Station";
            if(staname.Contains("Cord"))
            {
                inf.WriteString(STEP, $"{staname}", values);
            }
            else
            {
                bool res = values == "True" ? true : false;
                inf.WriteBool(STEP, $"{staname}", res);
            }

        }
        #endregion
        #region 生产数据
        private int ST1CountOK = 0;
        private int ST2CountOK = 0;
        private int ST3CountOK = 0;
        private int ST4CountOK = 0;
        private int ST5CountOK = 0;
        private int ST6CountOK = 0;
        private int ST7CountOK = 0;
        private int ST8CountOK = 0;
        private int ST9CountOK = 0;
        private int ST1CountNG = 0;
        private int ST2CountNG = 0;
        private int ST3CountNG = 0;
        private int ST4CountNG = 0;
        private int ST5CountNG = 0;
        private int ST6CountNG = 0;
        private int ST7CountNG = 0;
        private int ST8CountNG = 0;
        private int ST9CountNG = 0;



       /// <summary>
       /// 更新生产数据到数据库
       /// </summary>
       /// <param name="stname">站的名称</param>
       /// <param name="OKC">合格书</param>
       /// <param name="NGC">不合格数</param>
        private void UptataP(string stname,int OKC,int NGC)
        {
            string myday = DateTime.Now.ToString("yyyy-MM-dd");
            if(!SQLHelper.IsDaySavet(myday,SysStatus.CurProductName, stname))
            {
                string sql = "insert into ProductCount( InsertTime,ProductType,OKcount,NGcount,STN)" +
                               " values( @InsertTime,@ProductType,@OKcount,@NGcount,@STN)";
                SqlParameter[] param = new SqlParameter[]
                       { new SqlParameter("@InsertTime", myday),
                          new SqlParameter("@ProductType",SysStatus.CurProductName),
                          new SqlParameter("@OKcount", OKC),
                          new SqlParameter("@NGcount",NGC),
                          new SqlParameter("@STN", stname), };
                   int a = SQLHelper.Update(sql, param);
                if (a <= 0)
                {
                    Logger.Error($"{stname}生产计数写入数据库失败！");                 
                }
            }
            else
            {
                string sql = $"update ProductCount set OKcount = '{OKC}' where InsertTime = '{myday}' and ProductType = '{SysStatus.CurProductName}'  and STN = '{stname}' ";
                int a = SQLHelper.Update(sql);
                if (a <= 0)
                {
                    Logger.Error($"{stname}生产计数写入数据库失败！");
                }
                sql = $"update ProductCount set OKcount = '{NGC}' where InsertTime = '{myday}' and ProductType = '{SysStatus.CurProductName}'  and STN = '{stname}' ";
                a = SQLHelper.Update(sql);
                if (a <= 0)
                {
                    Logger.Error($"{stname}生产计数写入数据库失败！");
                }
            }

        }
        /// <summary>
        /// 获取当前生产数量
        /// </summary>
        public void LoadProC()
        {
            string aa = DateTime.Now.ToString("yyyy-MM-dd");
            // SysStatus.CurProductName = "123";
            string sql = $"select OKcount,NGcount from ProductCount where InsertTime = '{aa}' and ProductType = '{SysStatus.CurProductName}'  and STN = 'ST1'";
            DataTable aa1 = SQLHelper.GetDataTable(sql);
            if (aa1 != null && aa1.Rows.Count > 0)
            {
                ST1CountOK = (int)aa1.Rows[0][0];
                ST1CountNG = (int)aa1.Rows[0][1];                
            }
            sql = $"select OKcount,NGcount from ProductCount where InsertTime = '{aa}' and ProductType = '{SysStatus.CurProductName}'  and STN = 'ST2'";
            aa1 = SQLHelper.GetDataTable(sql);
            if (aa1 != null && aa1.Rows.Count > 0)
            {
                ST2CountOK = (int)aa1.Rows[0][0];
                ST2CountNG = (int)aa1.Rows[0][1];
            }
            sql = $"select OKcount,NGcount from ProductCount where InsertTime = '{aa}' and ProductType = '{SysStatus.CurProductName}'  and STN = 'ST3'";
            aa1 = SQLHelper.GetDataTable(sql);
            if (aa1 != null && aa1.Rows.Count > 0)
            {
                ST3CountOK = (int)aa1.Rows[0][0];
                ST3CountNG = (int)aa1.Rows[0][1];
            }
            sql = $"select OKcount,NGcount from ProductCount where InsertTime = '{aa}' and ProductType = '{SysStatus.CurProductName}'  and STN = 'ST4'";
            aa1 = SQLHelper.GetDataTable(sql);
            if (aa1 != null && aa1.Rows.Count > 0)
            {
                ST4CountOK = (int)aa1.Rows[0][0];
                ST4CountNG = (int)aa1.Rows[0][1];
            }
            sql = $"select OKcount,NGcount from ProductCount where InsertTime = '{aa}' and ProductType = '{SysStatus.CurProductName}'  and STN = 'ST5'";
            aa1 = SQLHelper.GetDataTable(sql);
            if (aa1 != null && aa1.Rows.Count > 0)
            {
                ST5CountOK = (int)aa1.Rows[0][0];
                ST5CountNG = (int)aa1.Rows[0][1];
            }
            sql = $"select OKcount,NGcount from ProductCount where InsertTime = '{aa}' and ProductType = '{SysStatus.CurProductName}'  and STN = 'ST6'";
            aa1 = SQLHelper.GetDataTable(sql);
            if (aa1 != null && aa1.Rows.Count > 0)
            {
                ST6CountOK = (int)aa1.Rows[0][0];
                ST6CountNG = (int)aa1.Rows[0][1];
            }
            sql = $"select OKcount,NGcount from ProductCount where InsertTime = '{aa}' and ProductType = '{SysStatus.CurProductName}'  and STN = 'ST7'";
            aa1 = SQLHelper.GetDataTable(sql);
            if (aa1 != null && aa1.Rows.Count > 0)
            {
                ST7CountOK = (int)aa1.Rows[0][0];
                ST7CountNG = (int)aa1.Rows[0][1];
            }
            sql = $"select OKcount,NGcount from ProductCount where InsertTime = '{aa}' and ProductType = '{SysStatus.CurProductName}'  and STN = 'ST8'";
            aa1 = SQLHelper.GetDataTable(sql);
            if (aa1 != null && aa1.Rows.Count > 0)
            {
                ST8CountOK = (int)aa1.Rows[0][0];
                ST8CountNG = (int)aa1.Rows[0][1];
            }
        }



        #endregion
    }
}
