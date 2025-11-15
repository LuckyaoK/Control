
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
    /// <summary>
    /// 得利捷扫码枪
    /// </summary>
    public class barcode_delijie
    {
        /// <summary>
        ///  IP
        /// </summary>
        public string MIP;
        /// <summary>
        /// 端口
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
        ///  描述
        /// </summary>
        public string mdescrible;
        public string logs;
        Socket cliSocket;
        System.Net.IPAddress IP;
        private Byte[] MsgReceiveBuffer = new Byte[1024];//客户端所使用的套接字对象
        /// <summary>
        /// 接收到的信息
        /// </summary>
        public string ReceiveStringData = "";
        /// <summary>
        /// 连接状态，true:连接OK ,false:连接失败
        /// </summary>
        public bool IsConnected = false;
        /// <summary>
        /// 扫到码了
        /// </summary>
        public bool GetCordOK=false;
        /// <summary>
        /// 得利捷扫码枪构造
        /// </summary>
        /// <param name="mip">扫码枪IP</param>
        /// <param name="mport">扫码枪PORT</param>
        /// <param name="mLip">本地IP</param>
        /// <param name="mLport">本地PORT</param>
        /// <param name="mdes">描述</param>
        public barcode_delijie(string mip, int mport, string mLip, int mLport, string mdes,string loggs="")
        {
            MIP = mip;
            Mport = mport;
            localIP = mLip;
            LocalPort = mLport;
            mdescrible = mdes;
            logs = loggs;
            ssa = new PaperC();
            //connect();
        }
        /// <summary>
        /// 连接 
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
            try
            {
                cliSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                cliSocket.SendTimeout = 1000;
                cliSocket.ReceiveTimeout = 1000;
                /*    if (LocalIp != "")*/
               // cliSocket.Bind(new IPEndPoint(IPAddress.Any, LocalPort));//设置本地IPport
                //this.tcpclient.Connect(IPAddress.Parse(Ip), Port);
                var ServerInfo = new IPEndPoint(IP, Mport);
                cliSocket.Connect(ServerInfo);              
                IsConnected = true;
                run();
                Logger.Debug($"{mdescrible}连接OK");
            }
            catch (Exception ex)
            {
                IsConnected = false;
                Logger.Error($"{mdescrible}连接失败：{ex.Message}");
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
                        GetCordOK = true;
                        ssa.Name = ReceiveStringData;
                        jieshou1?.Invoke(this, ssa);

                    }
                    Thread.Sleep(20);
                }
                catch (Exception ex)
                {
                    IsConnected = false;
                    Logger.Debug($"与{mdescrible}通讯失败!{ex.Message}");
                    MessageBox.Show($"与扫码枪通讯失败!{ex.Message}");
                    run_task.Dispose();
                    return;
                }
            }
        }
        #endregion
         
        
        /// <summary>
        /// 发送信息，得利捷扫码枪没法发送信息
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public EmRes Sendb(string strs)
        {
            if (IsConnected)
            {
                cliSocket.Send(Encoding.UTF8.GetBytes(strs));
                ssa.Name1 = strs;
                fasong1?.Invoke(this, ssa);
            }
            else
            {
                Logger.Error($"{mdescrible}断开连接！");
                return EmRes.Error;
            }
            return EmRes.Succeed;
        }
        public event EventHandler<PaperC> jieshou1;
        public event EventHandler<PaperC> fasong1;
        PaperC ssa;
        public class PaperC : EventArgs
        {
            public string Name { get; set; }                    //用于存储数据，当事件被调用时，可利用其进行传递数据。
            public string Name1 { get; set; }                    //用于存储数据，当事件被调用时，可利用其进行传递数据。
        }

    }
}

