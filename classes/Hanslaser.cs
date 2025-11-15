using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using xktComm.DataConvert;
using CXPro001.myclass;
namespace CXPro001.classes
{
    /// <summary>
    /// 大族激光刻字机- COM和网口通用
    /// </summary>
    public class Hanslaser
    {
        /// <summary>
        /// 与刻字机通讯的串口 
        /// </summary>
        public SerialPort serialPort;
        /// <summary>
        /// 串口号-IP
        /// </summary>
        public string MyPortName;
        /// <summary>
        /// 波特率-PORT
        /// </summary>
        public int MyBaudRate;
        /// <summary>
        /// 获取string类型数据
        /// </summary>
        public string ReceiveData8740;
        /// <summary>
        /// 监控线程的点
        /// </summary>
        public CancellationTokenSource cts;
        
        /// <summary>
        /// 是否连接成功
        /// </summary>
        public bool IsConnected = false;
        /// <summary>
        /// 打标机的描述
        /// </summary>
        public string mdescrible;
        public bool IsLAN = false;
        Socket cliSocket;
        System.Net.IPAddress IP;
        private Byte[] MsgReceiveBuffer = new Byte[1024];//客户端所使用的套接字对象
        /// <summary>
        /// 构造刻字机
        /// </summary>
        /// <param name="mPortName">串口号</param>
        /// <param name="mBaudRate">波特率</param>
        /// <param name="mdes">描述</param>
        public Hanslaser(string mPortName, int mBaudRate, string mdes,bool islan=false)
        {
            MyPortName = mPortName;
            MyBaudRate = mBaudRate;
            mdescrible = mdes;
            IsLAN = islan;
           
            //connect();
        }
       
        public EmRes connect()
        {
            if (IsLAN)
            {
                //检查参数
                if (MyPortName == "")
                {
                    Logger.Error($"{mdescrible}没实例化，无IP等参数");
                    return EmRes.Error;
                }
                IP = IPAddress.Parse(MyPortName);
                if (IsConnected) return EmRes.Succeed;

                try
                {
                    cliSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    cliSocket.SendTimeout = 1000;
                    cliSocket.ReceiveTimeout = 1000;
                    var ServerInfo = new IPEndPoint(IP, MyBaudRate);
                    cliSocket.Connect(ServerInfo);
                    IsConnected = true;
                    run();
                }
                catch (Exception ex)
                {
                    IsConnected = false;
                    Logger.Error($"{mdescrible}连接失败：{ex.Message}");
                    //MessageBox.Show($"{mdescrible}连接失败：{ex.Message}");
                    return EmRes.Error;
                }
            }
            else
            {
                serialPort = new SerialPort();
                //如果打开先关闭
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }
                //设置串口属性
                serialPort.PortName = MyPortName;
                serialPort.BaudRate = MyBaudRate;
                //serialPort.DataBits = 8;
                //serialPort.Parity = iParity;
                //serialPort.StopBits = iStopBits;
                //设置串口事件
                serialPort.ReceivedBytesThreshold = 1;//数据触发事件
                serialPort.DataReceived += SerialPort_DataReceived;

                try
                {
                    serialPort.Open();
                    Logger.Info($"{mdescrible}串口打开成功");
                    IsConnected = true;
                }
                catch (Exception ex)
                {
                    Logger.Error($"{mdescrible}串口打开失败:{ex.Message}");
                    IsConnected = false;
                    return EmRes.Error;
                }
            }
           

            return EmRes.Succeed;

        }

        /// <summary>
        /// 串口关闭
        /// </summary>
        public void DisConnet()
        {
            
            if(IsLAN)
            {
                IsConnected = false;
                cliSocket.Close();
                cliSocket.Dispose();
            }
            else
            {
                if (serialPort != null)
                {
                    serialPort.Close();
                }
            }
            

        }
        #region 接收线程
        Task run_task = null;
        public void run()
        {
            if (run_task == null || run_task != null && run_task.IsCompleted)
            {
                Logger.Write(Logger.InfoType.Info, $"{mdescrible}创建运行线程");
                if (run_task != null) run_task.Dispose();
                run_task = new Task(run_th);
                run_task.Start();
            }
        }
        public void run_th()
        {
            while (IsConnected)
            {
                try
                {
                    if (cliSocket.Available > 1)
                    {
                        int countr = cliSocket.Receive(MsgReceiveBuffer);
                        ReceiveData8740 = Encoding.UTF8.GetString(MsgReceiveBuffer, 0, countr);//接收IP信息，没有信息发过来就一直卡这里。
                        //ReceiveData8740 = ReceiveStringData.Replace("\u000d\u000a", "");
                         
                    }
                   
                }
                catch (Exception ex)
                {
                    IsConnected = false;
                    Logger.Error($"与{mdescrible}通讯失败!{ex.Message}");
                    MessageBox.Show($"与扫码枪通讯失败!{ex.Message}");
                    run_task.Dispose();
                    return;
                }
                Thread.Sleep(100);
                Application.DoEvents();
            }
        }
        #endregion
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(100);
            //获取缓冲区的长度
            int ByteToRead = serialPort.BytesToRead;
            if (ByteToRead > 1)
            {
                try
                {
                    //定义一个数组
                    byte[] rec = new byte[ByteToRead];
                    //读取缓冲区的数据放到字节数组中
                    this.serialPort.Read(rec, 0, ByteToRead);
                    // ReceiveData = Encoding.ASCII.GetString(rec);
                    ReceiveData8740 = StringLib.GetStringFromByteArray(rec, 0, rec.Length);
                }
                catch (Exception ex)
                {
                    Logger.Error($"{mdescrible}串口接收信息失败：{ex.Message}");
                    return;
                }
            }
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="mess"></param>
        /// <returns></returns>
        public EmRes sends(string mess)
        {
            if(IsConnected)
            {
                if(IsLAN)
                {
                  
                    try
                    {
                        ReceiveData8740 = "";
                        cliSocket.Send(Encoding.UTF8.GetBytes(mess));
                        return EmRes.Succeed;

                    }
                    catch (Exception ex)
                    {
                        Logger.Error($"{mdescrible}发送信息{mess}失败:{ex.Message}");
                        IsConnected = false;
                        return EmRes.Error;
                    }
                }
                else
                {
                    if (!serialPort.IsOpen)//如果断开连接就在连接一次
                    {
                        serialPort.Open();
                    }
                    try
                    {
                        ReceiveData8740 = "";
                        serialPort.Write(mess);
                        return EmRes.Succeed;
                    }
                    catch (Exception ex)
                    {
                        Logger.Error($"{mdescrible}串口发送信息失败：{ex.Message}");
                        return EmRes.Error;
                    }
                }
                
            }
            else
            {
                Logger.Error($"{mdescrible}串口没有开打");
                return EmRes.Error;
            }          
        }
        /// <summary>
        /// 发送数据并判断返回结果如果
        /// </summary>
        /// <param name="modes"></param>
        /// <returns>true:OK,false:NG</returns>
        public bool Sendmodel(string modes)
        {
            int a = 0;
            Thread.Sleep(50);
            sends(modes);
            Thread.Sleep(100);
            while (true)
            {
                if (ReceiveData8740 == "")
                {
                    Thread.Sleep(50);
                    a = a + 1;
                }
                if (ReceiveData8740 != "")
                {
                    Thread.Sleep(10);
                    break;
                }
                if (a > 100)
                {
                    Logger.Error($"{mdescrible}接收数据超时");
                    return false;               
                }
            }
            string rcv = ReceiveData8740;
            if (rcv != "" && rcv.Length > 0)
            {
                switch (rcv)
                {
                    case "\u0002$SysNoReady\u0003":  //初始化失败时，打标窗口未打开时
                        Logger.Error($"{mdescrible}初始化失败时，打标窗口未打开",1);
                        return false;
                    case "\u0002$Initialize_OK\u0003"://模板调用OK
                        Logger.Info($"{mdescrible}模板调用OK{modes}", 1);
                        return true;                      
                    case "\u0002$Initialize_FALSE\u0003"://模板调用失败
                        Logger.Error($"{mdescrible}打码机初始化失败，调用打码模板不存在", 1);
                        return false;
                    case "\u0002$Receive_OK\u0003"://数据接收OK                        
                        Logger.Info($"{mdescrible}打码数据接收成功:{rcv}", 1);
                        return true;                     
                    case "\u0002$Receive_Error\u0003"://数据接收失败 
                        Logger.Error($"{mdescrible}打码机数据接收失败，请检查打标可变文本设置:{rcv}", 1);
                        return false;
                    case "\u0002$MarkStart_OK\u0003"://启动成功 
                        Logger.Info($"{mdescrible}打码启动完成OK:{rcv}", 1);
                        return true;
                    case "\u0002$MarkStart_ERROR\u0003"://启动失败 
                        Logger.Error($"{mdescrible}打码启动失败:{rcv}", 1);
                        return false;
                    default:
                        Logger.Error($"{mdescrible}接收数据异常：{rcv}", 1);
                        return false;                        
                }
            }
            else
            {
                Logger.Error($"{mdescrible}接收数据为空：{rcv}", 1);
                return false;
            }
 
        }

    }
}
