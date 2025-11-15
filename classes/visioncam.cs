
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CXPro001.myclass;
namespace CXPro001.classes
{
    public delegate EmRes ReceiveC1(string input, out string output);

    /// <summary>
    /// 视觉主机通讯
    /// </summary>
    public class visioncam
    {
        /// <summary>
        /// PLC的IP
        /// </summary>
        public string MIP;
        /// <summary>
        /// PLC的端口
        /// </summary>
        public int Mport;
        /// <summary>
        /// 本地IP
        /// </summary>
        public string localIP;
        /// <summary>
        /// 本地端口
        /// </summary>
        public int LocalPort;
        /// <summary>
        /// PLC的描述
        /// </summary>
        public string mdescrible;
        Socket cliSocket;
        System.Net.IPAddress IP;
        private Byte[] MsgReceiveBuffer = new Byte[1024];//客户端所使用的套接字对象
        /// <summary>
        /// 收到的信息
        /// </summary>
        public string ReceiveStringData = "";
        /// <summary>
        /// 连接状态，true:连接OK ,false:连接失败
        /// </summary>
        public bool IsConnected = false;
        //ReceiveC1 receiveclick1 = null;
        object locks = new object();
        /// <summary>
        /// 基恩士PLC构造
        /// </summary>
        /// <param name="mip">PLC IP</param>
        /// <param name="mport">PLC PORT</param>
        /// <param name="mLip">本地IP</param>
        /// <param name="mLport">本地port</param>
        /// <param name="mdes">PLC描述</param>
        public visioncam(string mip, int mport, string mLip, int mLport, string mdes)
        {
            MIP = mip;
            Mport = mport;
            localIP = mLip;
            LocalPort = mLport;
            mdescrible = mdes;
            //connect();
        }
        /// <summary>
        /// 连接PLC
        /// </summary>
        /// <returns></returns>
        public EmRes connect()
        {
            //检查参数
            if (MIP == "")
            {
                Logger.Error($"{mdescrible}没实例化，无IP等参数");
                return EmRes.Error;
            }
            IP = IPAddress.Parse(MIP);
            if (IsConnected) return EmRes.Succeed;

            try
           {
               
                cliSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                cliSocket.SendTimeout = 1000;
                cliSocket.ReceiveTimeout = 1000;
                                            
               // cliSocket.Bind(new IPEndPoint(IPAddress.Any, LocalPort));
                IPEndPoint ServerInfo = new IPEndPoint(IP, Mport);
                cliSocket.Connect(ServerInfo);
                IsConnected = true;
               
                //paperContent = new PaperContentEventArgs();
                Logger.Info($"{mdescrible}连接OK");
                run();
            }
            catch (Exception ex)
            {
                IsConnected = false;
                Logger.Error($"{mdescrible}连接失败：{ex.Message}");
                //MessageBox.Show($"{mdescrible}连接失败：{ex.Message}");
                return EmRes.Error;
            }

            return EmRes.Succeed;
        }
        public void DisConnet()
        {
            IsConnected = false;
            cliSocket.Close();
            cliSocket.Dispose();
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
                    if (cliSocket.Available > 0)
                    {
                        int countr = cliSocket.Receive(MsgReceiveBuffer);
                        ReceiveStringData = Encoding.UTF8.GetString(MsgReceiveBuffer, 0, countr);//接收IP信息，没有信息发过来就一直卡这里。                     
                        ReceiveStringData = ReceiveStringData.Replace("\u000d", "");
                        ReceiveStringData = ReceiveStringData.Replace("\u000a", "");


                        //if (StartPublishPaper != null)
                        //{
                        //    paperContent.Name = ReceiveStringData;
                        //    StartPublishPaper(this, paperContent);
                        //}
                    }
                    
                }
                catch (Exception ex)
                {
                    IsConnected = false;
                    Logger.Debug($"与{mdescrible}通讯失败!{ex.Message}");
                    MessageBox.Show($"与扫码枪通讯失败!{ex.Message}");
                    run_task.Dispose();
                    return;
                }
                Thread.Sleep(50);
                Application.DoEvents();
            }
        }

        #endregion
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public EmRes Sendb(string strs)
        {
            if (IsConnected)
            {
                try
                {
                    ReceiveStringData = "";
                    cliSocket.Send(Encoding.UTF8.GetBytes(strs));
                }
                catch (Exception ex)
                {
                    IsConnected = false;
                    Logger.Error($"发送数据到{mdescrible}失败！{ex.Message}");
                    return EmRes.Error;
                }
            }
            else
            {
                Logger.Error($"{mdescrible}断开连接！");
                return EmRes.Error;
            }
            return EmRes.Succeed;
        }
        /// <summary>
        /// 发送模板
        /// </summary>
        /// <param name="sens"></param>
        /// <returns></returns>
        public EmRes SendMode(string sens)
        {
            if (Sendb(sens) != EmRes.Succeed) return EmRes.Error;
            Thread.Sleep(400);
            int a = 0;
            while (ReceiveStringData.Length < 2)
            {
                if (ReceiveStringData.Length > 2) break;
                a++;
                if(a>20) break;
                Thread.Sleep(100);
            }
            if (ReceiveStringData !="" && ReceiveStringData.Length > 0)
            {
                string[] result = ReceiveStringData.Split(',', '\r', '\n');
                if (result[1] == "OK")
                {
                    Logger.Info("发送视觉模板成功");
                    return EmRes.Succeed;
                }
                else
                {
                    Logger.Error("发送视觉模板失败，请确认产品型号名称是否正确");
                    return EmRes.Error;
                }
            }
            else
            {
                Logger.Error("发送视觉模板等待接收返回数据失败");
                return EmRes.Error;
            }
           
        }
        /// <summary>
        /// 发送指令信息，等待返回值
        /// </summary>
        /// <param name="sens">发送的信息</param>
        /// <param name="res">接收的信息</param>
        /// <returns></returns>
        public EmRes SendTs(string sens,ref string res)
        {
            lock(locks)
            {
                res = "";
                //if (!IsConnected) connect();
                if (Sendb(sens) != EmRes.Succeed) return EmRes.Error;
                int a = 0;
                Thread.Sleep(50);
                while (ReceiveStringData.Length < 2)
                {
                    Thread.Sleep(50);
                    a++;
                    if (ReceiveStringData.Length > 1) break;
                    if (a > 30)
                    {
                        Logger.Error($"等待视觉主机返回信息超时，指令{sens}");
                        
                        return EmRes.Error;
                    }
                }
                res = ReceiveStringData;
                return EmRes.Succeed;
            }          
        }
        /// <summary>
        /// 对接收的信息进行分析
        /// </summary>
        /// <param name="dats1">待分析的数据</param>
        /// <param name="dats2">分析完返回的数据</param>
        /// <returns></returns>
        public bool AnalyseData1(string dats1,ref string dats2)
        {
            string[] dataArray = dats1.Split('\r', '\n');
            string[] var = dataArray[0].Split(',');
            if(var[1]=="OK") return true;


            return true;
        }



        #region 传递函数 不用
        //1、创建事件并发布
        public event EventHandler<PaperContentEventArgs> StartPublishPaper;
        PaperContentEventArgs paperContent;
        
        /// <summary>
        /// 派生自EventArgs的类，用于传递数据
        /// </summary>
        public class PaperContentEventArgs : EventArgs
        {
            public string Name { get; set; }                    //用于存储数据，当事件被调用时，可利用其进行传递数据。
        }
        #endregion
    }
}
