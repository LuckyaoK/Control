using NVRCsharpDemo;
using MyLib.Sys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CXPro001.controls
{
    public partial class Hikvision : UserControl
    {
        private bool m_bInitSDK = false;
        private bool m_bRecord = false;
        private uint iLastErr = 0;
        private Int32 m_lUserID = -1;
        private Int32 m_lRealHandle = -1;
        private string str1;
        private string str2;
        private Int32 i = 0;
        private Int32 m_lTree = 0;
        private string str;
        private long iSelIndex = 0;
        private uint dwAChanTotalNum = 0;
        private uint dwDChanTotalNum = 0;
        private Int32 m_lPort = -1;
        private IntPtr m_ptrRealHandle;
        private int[] iIPDevID = new int[96];
        private int[] iChannelNum = new int[96];

        private CHCNetSDK.REALDATACALLBACK RealData = null;
        public CHCNetSDK.NET_DVR_DEVICEINFO_V30 DeviceInfo;
        public CHCNetSDK.NET_DVR_IPPARACFG_V40 m_struIpParaCfgV40;
        public CHCNetSDK.NET_DVR_STREAM_MODE m_struStreamMode;
        public CHCNetSDK.NET_DVR_IPCHANINFO m_struChanInfo;
        public CHCNetSDK.NET_DVR_PU_STREAM_URL m_struStreamURL;
        public CHCNetSDK.NET_DVR_IPCHANINFO_V40 m_struChanInfoV40;
        private PlayCtrl.DECCBFUN m_fDisplayFun = null;
        public delegate void MyDebugInfo(string str);
        public Hikvision()
        {
            InitializeComponent();
           
        }
        public void init()
        {
            m_bInitSDK = CHCNetSDK.NET_DVR_Init();
            if (m_bInitSDK == false)
            {
                MessageBox.Show("NET_DVR_Init error!");
                return;
            }
            else
            {
                //保存SDK日志 To save the SDK log
                CHCNetSDK.NET_DVR_SetLogToFile(3, "C:\\SdkLog\\", true);
                timer1.Enabled = true;


                for (int i = 0; i < 64; i++)
                {
                    iIPDevID[i] = -1;
                    iChannelNum[i] = -1;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)//登录
        {
            button1.Enabled = false;
            button2.Enabled = false;
            if (m_lUserID < 0)
            {
                string DVRIPAddress = "192.168.1.168"; //设备IP地址或者域名 Device IP
                Int16 DVRPortNumber = 8000;//设备服务端口号 Device Port
                string DVRUserName = "admin";//设备登录用户名 User name to login
                string DVRPassword = "qaz12345";//设备登录密码 Password to login

             //   m_bInitSDK = CHCNetSDK.NET_DVR_Init();

                //登录设备 Login the device
                m_lUserID = CHCNetSDK.NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref DeviceInfo);
                if (m_lUserID < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "登录海康监控失败, error code= " + iLastErr; //登录失败，输出错误号 Failed to login and output the error code
                    Logger.Error(str);
                    return;
                }
                else
                {
                    //登录成功
                    Logger.Info("登录海康监控成功");
                    button1.Text = "注销";
                    DM1 = true;
                    dwAChanTotalNum = (uint)DeviceInfo.byChanNum;
                    dwDChanTotalNum = (uint)DeviceInfo.byIPChanNum + 256 * (uint)DeviceInfo.byHighDChanNum;
                    if (dwDChanTotalNum > 0)
                    {
                        InfoIPChannel();
                    }
                    else
                    {
                        for (i = 0; i < dwAChanTotalNum; i++)
                        {
                            ListAnalogChannel(i + 1, 1);
                            iChannelNum[i] = i + (int)DeviceInfo.byStartChan;
                        }

                        //comboBoxView.SelectedItem = 1;
                        // MessageBox.Show("This device has no IP channel!");
                    }
                }

            }
            else
            {
                //注销登录 Logout the device
                if (m_lRealHandle >= 0)
                {
                    MessageBox.Show("先停止监控才能退出登录"); ; //登出前先停止预览 Stop live view before logout
                    return;
                }

                if (!CHCNetSDK.NET_DVR_Logout(m_lUserID))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "退出海康监控失败, error code= " + iLastErr;
                    Logger.Error(str);
                    return;
                }
               
                m_lUserID = -1;
                button1.Text = "登录";
            }
            button1.Enabled = true;
            button2.Enabled = true;
        }
        public void InfoIPChannel()
        {
            uint dwSize = (uint)Marshal.SizeOf(m_struIpParaCfgV40);

            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(m_struIpParaCfgV40, ptrIpParaCfgV40, false);

            uint dwReturn = 0;
            int iGroupNo = 0;  //该Demo仅获取第一组64个通道，如果设备IP通道大于64路，需要按组号0~i多次调用NET_DVR_GET_IPPARACFG_V40获取

            if (!CHCNetSDK.NET_DVR_GetDVRConfig(m_lUserID, CHCNetSDK.NET_DVR_GET_IPPARACFG_V40, iGroupNo, ptrIpParaCfgV40, dwSize, ref dwReturn))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "获取IP资源配置信息失败，输出错误号= " + iLastErr;
                //获取IP资源配置信息失败，输出错误号 Failed to get configuration of IP channels and output the error code
                Logger.Error(str);
            }
            else
            {
                Logger.Info("NET_DVR_GET_IPPARACFG_V40 succ!");

                m_struIpParaCfgV40 = (CHCNetSDK.NET_DVR_IPPARACFG_V40)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(CHCNetSDK.NET_DVR_IPPARACFG_V40));

                for (i = 0; i < dwAChanTotalNum; i++)
                {
                    ListAnalogChannel(i + 1, m_struIpParaCfgV40.byAnalogChanEnable[i]);
                    iChannelNum[i] = i + (int)DeviceInfo.byStartChan;
                }

                byte byStreamType = 0;
                uint iDChanNum = 64;

                if (dwDChanTotalNum < 64)
                {
                    iDChanNum = dwDChanTotalNum; //如果设备IP通道小于64路，按实际路数获取
                }

                for (i = 0; i < iDChanNum; i++)
                {
                    iChannelNum[i + dwAChanTotalNum] = i + (int)m_struIpParaCfgV40.dwStartDChan;
                    byStreamType = m_struIpParaCfgV40.struStreamMode[i].byGetStreamType;

                    dwSize = (uint)Marshal.SizeOf(m_struIpParaCfgV40.struStreamMode[i].uGetStream);
                    switch (byStreamType)
                    {
                        //目前NVR仅支持直接从设备取流 NVR supports only the mode: get stream from device directly
                        case 0:
                            IntPtr ptrChanInfo = Marshal.AllocHGlobal((Int32)dwSize);
                            Marshal.StructureToPtr(m_struIpParaCfgV40.struStreamMode[i].uGetStream, ptrChanInfo, false);
                            m_struChanInfo = (CHCNetSDK.NET_DVR_IPCHANINFO)Marshal.PtrToStructure(ptrChanInfo, typeof(CHCNetSDK.NET_DVR_IPCHANINFO));

                            //列出IP通道 List the IP channel
                            ListIPChannel(i + 1, m_struChanInfo.byEnable, m_struChanInfo.byIPID);
                            iIPDevID[i] = m_struChanInfo.byIPID + m_struChanInfo.byIPIDHigh * 256 - iGroupNo * 64 - 1;

                            Marshal.FreeHGlobal(ptrChanInfo);
                            break;
                        case 4:
                            IntPtr ptrStreamURL = Marshal.AllocHGlobal((Int32)dwSize);
                            Marshal.StructureToPtr(m_struIpParaCfgV40.struStreamMode[i].uGetStream, ptrStreamURL, false);
                            m_struStreamURL = (CHCNetSDK.NET_DVR_PU_STREAM_URL)Marshal.PtrToStructure(ptrStreamURL, typeof(CHCNetSDK.NET_DVR_PU_STREAM_URL));

                            //列出IP通道 List the IP channel
                            ListIPChannel(i + 1, m_struStreamURL.byEnable, m_struStreamURL.wIPID);
                            iIPDevID[i] = m_struStreamURL.wIPID - iGroupNo * 64 - 1;

                            Marshal.FreeHGlobal(ptrStreamURL);
                            break;
                        case 6:
                            IntPtr ptrChanInfoV40 = Marshal.AllocHGlobal((Int32)dwSize);
                            Marshal.StructureToPtr(m_struIpParaCfgV40.struStreamMode[i].uGetStream, ptrChanInfoV40, false);
                            m_struChanInfoV40 = (CHCNetSDK.NET_DVR_IPCHANINFO_V40)Marshal.PtrToStructure(ptrChanInfoV40, typeof(CHCNetSDK.NET_DVR_IPCHANINFO_V40));

                            //列出IP通道 List the IP channel
                            ListIPChannel(i + 1, m_struChanInfoV40.byEnable, m_struChanInfoV40.wIPID);
                            iIPDevID[i] = m_struChanInfoV40.wIPID - iGroupNo * 64 - 1;

                            Marshal.FreeHGlobal(ptrChanInfoV40);
                            break;
                        default:
                            break;
                    }
                }
            }
            Marshal.FreeHGlobal(ptrIpParaCfgV40);

        }
        public void ListAnalogChannel(Int32 iChanNo, byte byEnable)
        {
            str1 = String.Format("Camera {0}", iChanNo);
            m_lTree++;

            if (byEnable == 0)
            {
                str2 = "Disabled"; //通道已被禁用 This channel has been disabled               
            }
            else
            {
                str2 = "Enabled"; //通道处于启用状态 This channel has been enabled
            }

        }
        public void ListIPChannel(Int32 iChanNo, byte byOnline, int byIPID)
        {
            str1 = String.Format("IPCamera {0}", iChanNo);
            m_lTree++;

            if (byIPID == 0)
            {
                str2 = "X"; //通道空闲，没有添加前端设备 the channel is idle                  
            }
            else
            {
                if (byOnline == 0)
                {
                    str2 = "offline"; //通道不在线 the channel is off-line
                }
                else
                    str2 = "online"; //通道在线 The channel is on-line
            }
 
        }

        private void button2_Click(object sender, EventArgs e)//预览
        {
            button1.Enabled = false;
            button2.Enabled = false;
            Invoke(new Action(() => {
                if (m_lUserID < 0)
                {
                    MessageBox.Show("请先登录");
                    return;
                }

                if (m_bRecord)
                {
                    MessageBox.Show("Please stop recording firstly!");
                    return;
                }

                if (m_lRealHandle < 0)
                {
                //    var aa1 = RealPlayWnd;
                //STEP1:
                    CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
                    lpPreviewInfo.hPlayWnd = RealPlayWnd.Handle;//预览窗口 live view window
                    iSelIndex = Convert.ToInt32(textBox1.Text);
                    lpPreviewInfo.lChannel = iChannelNum[(int)iSelIndex];//预览的设备通道 the device channel number
                    lpPreviewInfo.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                    lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
                    lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
                    lpPreviewInfo.dwDisplayBufNum = 15; //播放库显示缓冲区最大帧数

                    IntPtr pUser = IntPtr.Zero;//用户数据 user data 

                    if (true)
                    {
                        //打开预览 Start live view 
                        m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, null/*RealData*/, pUser);
                    }
                    else
                    {
                        //lpPreviewInfo.hPlayWnd = IntPtr.Zero;//预览窗口 live view window
                        //m_ptrRealHandle = RealPlayWnd.Handle;
                        //RealData = new CHCNetSDK.REALDATACALLBACK(RealDataCallBack);//预览实时流回调函数 real-time stream callback function 
                        //m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, RealData, pUser);
                    }
                    Application.DoEvents();
                    Thread.Sleep(100);
                   
                    //var aa = RealPlayWnd.Image;
                    //if (aa == null)
                    //{
                    //    goto STEP1;
                    //}
                    if (m_lRealHandle < 0)
                    {
                        iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                        str = "实时预览监控, error code= " + iLastErr; //预览失败，输出错误号 failed to start live view, and output the error code.
                        Logger.Error(str);
                        return;
                    }
                    else
                    {
                        //预览成功
                        Logger.Info("打开监控预览OK");
                        button2.Text = "停止预览";
                    }
                    
                }
                else
                {
                    ////停止预览 Stop live view 
                    //if (!CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle))
                    //{
                    //    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    //    str = "停止预览监控 failed, error code= " + iLastErr;
                    //    Logger.Error(str);
                    //    return;
                    //}
                    //RealPlayWnd.Invalidate();//刷新窗口 refresh the window


                    Logger.Info("停止监控预览完成");


                    m_lRealHandle = -1;
                    button2.Text = "监控预览";
                   
                }

               
            }));
           
            button1.Enabled = true;
            button2.Enabled = true;
        }
        bool DM1 = false;
        int DM2 = 0;
        int DM3 = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!DM1 & DM3 < 2) 
            {
                DM3++;
                button1_Click(null, null);
            }
            else
            {
                button1.Enabled = true;
            }
            if (DM2 < 2) 
            {
                DM2++;
                button2_Click(null, null);
            }
            else
            {
                button2.Enabled = true;
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("msedge.EXE", "http://192.168.1.168/");//IE浏览器 IEXPLORE.EXE

        }
    }
}
