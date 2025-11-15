using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text; 
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
 
using MyLib.Param;
using CXPro001.myclass;

namespace CXPro001.classes
{
    /// <summary>
    /// 基恩士PLC
    /// </summary>
    public class PLC_jienshi
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
       public string ReceiveStringData="";
        /// <summary>
        /// 连接状态，true:连接OK ,false:连接失败
        /// </summary>
        public bool IsConnected = false;
        object lok1=new object();
        object lok2 = new object();
        public int stas = 0;
        /// <summary>
        /// 基恩士PLC构造
        /// </summary>
        /// <param name="mip">PLC IP</param>
        /// <param name="mport">PLC PORT</param>
        /// <param name="mLip">本地IP</param>
        /// <param name="mLport">本地port</param>
        /// <param name="mdes">PLC描述</param>
        public PLC_jienshi(string mip,int mport, string mLip, int mLport, string mdes)
        {
            MIP = mip;
            Mport = mport;
            localIP = mLip;
            LocalPort = mLport;
            mdescrible = mdes;
            ssa = new PaperC();
           // connect();
        }
        /// <summary>
        /// 连接PLC
        /// </summary>
        /// <returns></returns>
        public EmRes connect()
        {
            //检查参数
            if(MIP=="")
            {
                Logger.Error($"{mdescrible}没实例化，无IP等参数");
                return EmRes.Error;
            }
            IP= IPAddress.Parse(MIP);
            if (IsConnected) return EmRes.Succeed;

            try
            {
                cliSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                cliSocket.SendTimeout = 1000;
                cliSocket.ReceiveTimeout = 1000;             
                var ServerInfo = new IPEndPoint(IP, Mport);
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
                    if (cliSocket.Available > 1)
                    {
                        int countr = cliSocket.Receive(MsgReceiveBuffer);
                        ReceiveStringData = Encoding.UTF8.GetString(MsgReceiveBuffer, 0, countr);//接收IP信息，没有信息发过来就一直卡这里。
                        ReceiveStringData = ReceiveStringData.Replace("\u000d\u000a", "");
                        ssa.Name = ReceiveStringData;
                        jieshou?.Invoke(this, ssa);
                        stas = 0;
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
                Thread.Sleep(25);
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
                    ssa.Name1 = strs;
                    fasong?.Invoke(this, ssa);
                    stas = 1;
                    
                }
                catch (Exception ex)
                {
                    IsConnected = false;
                    Logger.Error($"发送数据{strs}到{mdescrible}失败！{ex.Message}");

                }
                return EmRes.Succeed;
            }
            else
            {
                Logger.Error($"{mdescrible}断开连接！");
                return EmRes.Error;
            }
              
        }
        #region 位读取
        /// <summary>
        /// 位读取，一次一个
        /// </summary>
        /// <param name="star">起始地址</param>
        /// <param name="values">读取返回的值</param>
        /// <returns></returns>
        private EmRes ReadBit(string star, ref string values)
        {
            values = "";
            string st1 = "RDS " + star + " 1" + '\u000d';
            if (Sendb(st1) != EmRes.Succeed) return EmRes.Error;
            Thread.Sleep(10);
            int a = 0;
            while (true)
            {
                if (ReceiveStringData != "") break;
                else
                {
                    Thread.Sleep(10);
                    a++;
                }
                if (a > 70)
                {
                    Logger.Error($"位读取{mdescrible}{star}失败：原因发送信息后等待回信超时700ms");
                    return EmRes.Error;
                }
            }
            //读取成功
            if (ReceiveStringData.Contains("E"))
            {
                Logger.Error($"位读取{mdescrible}{star}失败：原因返回信息中有E");
                return EmRes.Error;
            }
            string[] i = ReceiveStringData.Split('\u0020');
            if (i.Count() == 1)
            {
                values = i[0];
                return EmRes.Succeed;
            }
            else
            {
                Logger.Error($"位读取{mdescrible}{star}整数失败：原因{ReceiveStringData}返回数量不等于1");
                return EmRes.Error;
            }
        }
        /// <summary>
        /// 位写入，一次一个
        /// </summary>
        /// <param name="star">起始地址</param>
        /// <param name="values">写入的值</param>
        /// <returns></returns>
        private EmRes WriteBit(string star, ref string values)
        {

            string st1 = "WRS " + star + " 1 " + values + '\u000d';
            if (Sendb(st1) != EmRes.Succeed) return EmRes.Error;
            Thread.Sleep(10);
            int a = 0;
            while (true)//等待回信
            {
                if (ReceiveStringData != "") break;
                else
                {
                    Thread.Sleep(10);
                    a++;
                }
                if (a > 70)
                {
                    Logger.Error($"位写入{mdescrible}{star}内容{values}失败：原因发送信息后等待回信超时700ms");
                    return EmRes.Error;
                }
            }
            //回信完成
            if (ReceiveStringData.Contains("OK")) return EmRes.Succeed;
            else
            {
                Logger.Error($"位写入{mdescrible}{star}内容{values}完成，但返回的值不包含OK");
                return EmRes.Error;
            }
        }
        #endregion
        #region 读写整数
        /// <summary>
        /// 读取整数16位，一次一个
        /// </summary>
        /// <param name="star">起始地址</param>
        /// <param name="values">返回的值</param>
        /// <returns></returns>
        private EmRes ReadInt16(string star,  ref string values)
        {
            values = "";
            string st1 = "RDS " + star +   " 1" + '\u000d';//RDS R500 11,读取R500连续11个位
            if (Sendb(st1) != EmRes.Succeed) return EmRes.Error;
            Thread.Sleep(10);
            int a = 0;
            while (true)
            {
                if (ReceiveStringData != "") break;
                else
                {
                    Thread.Sleep(10);
                    a++;
                }
                if (a > 70)
                {
                    Logger.Error($"读取{mdescrible}{star}整数失败：原因发送信息后等待回信超时700ms");
                    return EmRes.Error;
                }
            }
            if (ReceiveStringData.Contains("E"))
            {
                Logger.Error($"读取{mdescrible}{star}整数失败：原因返回信息中有E");
                return EmRes.Error;
            }
            string[] i = ReceiveStringData.Split('\u0020');

            if (i.Count() == 1)
            {
                values = i[0];
                return EmRes.Succeed;
            }
            else
            {
                Logger.Error($"读取{mdescrible}{star}整数失败：原因{ReceiveStringData}返回数量不等于1");
                return EmRes.Error;
            }
        }
        /// <summary>
        /// 写入16位整数，一次一个,支持正负写入
        /// </summary>
        /// <param name="star">起始地址</param>
        /// <param name="values">要写入的值</param>
        /// <returns></returns>
        private EmRes WriteInt16(string star, ref string values)
        {
            short AA = Convert.ToInt16(values);
            string AA1 = AA.ToString("X2");
             
            string st1 = "WRS " + star+".H" + " 1 " + AA1 + '\u000d';
            if (Sendb(st1) != EmRes.Succeed) return EmRes.Error;
            Thread.Sleep(10);
            int a = 0;
            while (true)//等待回信
            {
                if (ReceiveStringData != "") break;
                else
                {
                    Thread.Sleep(10);
                    a++;
                }
                if (a > 50)
                {
                    Logger.Error($"写入{mdescrible}{star}整数失败：原因发送信息后等待回信超时700ms");
                    return EmRes.Error;
                }
            }
            //回信完成
            if (ReceiveStringData.Contains("OK")) return EmRes.Succeed;
            else
            {
                Logger.Error($"写入{mdescrible}{star}完成，但返回的值不包含OK");
                return EmRes.Error;
            }
        }
        #endregion
        #region 浮点数读写
        /// <summary>
        /// 读取32位浮点数,一次只能读取一个
        /// </summary>
        /// <param name="star">起始地址</param>
        /// <param name="values">返回的值</param>
        /// <returns></returns>
        private EmRes ReadReal(string star,ref string values)
        {
            values = "";
            string st1 = "RDS " + star + " 2" + '\u000d';
            if (Sendb(st1) != EmRes.Succeed) return EmRes.Error;
            Thread.Sleep(20);
            int a = 0;
            while (true)//等待回信
            {
                if (ReceiveStringData != "") break;
                else
                {
                    Thread.Sleep(10);
                    a++;
                }
                if (a > 70)
                {
                    Logger.Error($"读取{mdescrible}{star}浮点数失败：原因发送信息后等待回信超时700ms");
                    return EmRes.Error;
                }
            }
             
            //读取成功
            if(ReceiveStringData.Contains("E"))
            {
                Logger.Error($"读取{mdescrible}{star}浮点数失败：原因返回信息中有E");
                return EmRes.Error;
            }
            string[] i = ReceiveStringData.Split('\u0020');
            
            if (i.Count() == 2)
            {               
                int a1 = Convert.ToInt32(i[0]) & 0X0000FFFF;
                int a11 = (int)((Convert.ToInt32(i[1]) << 16) & 0XFFFF0000);
                int A12 = (int)(a1 + a11);
                byte[] a2 = BitConverter.GetBytes(A12);
                float a3 = BitConverter.ToSingle(a2, 0);
                values = a3.ToString();
                return EmRes.Succeed;
            }
            else
            {
                Logger.Error($"读取{mdescrible}{star}浮点数失败：原因返回数量不等于2");
                return EmRes.Error;
            }
 
        }
        /// <summary>
        /// 写入浮点数-一次一个
        /// </summary>
        /// <param name="star">起始地址</param>
        /// <param name="values">写入的值</param>
        /// <returns></returns>
        private EmRes WriteReal(string star, ref string values)
        {
            //225 106 69 68  27361 17477
            //FF 6A 45 44
            float D1 = Convert.ToSingle(values);
            byte[] D2 = BitConverter.GetBytes(D1);
            ushort D3 = BitConverter.ToUInt16(D2, 0);
            ushort D4 = BitConverter.ToUInt16(D2, 2);
            string st1 = "WRS " + star + " 2 " + D3.ToString() + " " +D4.ToString()+ '\u000d';
            if (Sendb(st1) != EmRes.Succeed) return EmRes.Error;
            Thread.Sleep(10);
            int a = 0;
            while (true)//等待回信
            {
                if (ReceiveStringData != "") break;
                else
                {
                    Thread.Sleep(10);
                    a++;
                }
                if (a > 70)
                {
                    Logger.Error($"写入{mdescrible}{star}浮点数失败：原因发送信息后等待回信超时700ms");
                    return EmRes.Error;
                }
            }
            //回信完成
            if (ReceiveStringData.Contains("OK")) return EmRes.Succeed;
            else
            {
                Logger.Error($"写入{mdescrible}{star}浮点数失败：原因:返回信息{ReceiveStringData}");
                return EmRes.Error;
            }

        }

        #endregion
        #region  对外开放读取接口
        /// <summary>
        /// 读写基恩士PLC
        /// </summary>
        /// <param name="RorW">True:读，False:写</param>
        /// <param name="star">起始地址</param>
        /// <param name="values">读出或写入的值</param>
        /// <param name="function">1:整数，2:浮点数，3:bool</param>
        /// <returns></returns>
        public EmRes RWKeyPlc(bool RorW, string star, ref string values, int function = 1)
        {
            lock (lok1)
            {
                EmRes ret = EmRes.Error;
                if (RorW)//读取
                {
                    if (function == 1)//整数
                    {
                        ret = ReadInt16(star, ref values);
                    }
                    else if (function == 2)//浮点数
                    {
                        ret = ReadReal(star, ref values);
                    }
                    else if (function == 3)//bool
                    {
                        ret = ReadBit(star, ref values);
                    }
                }
                else//写入
                {
                    if (function == 1)//整数
                    {
                        ret = WriteInt16(star, ref values);
                    }
                    else if (function == 2)//浮点数
                    {
                        ret = WriteReal(star, ref values);
                    }
                    else if (function == 3)//bool
                    {
                        ret = WriteBit(star, ref values);
                    }

                }
                return ret;
            }
           
        }
        #endregion












        #region 向外传递数据
        public event EventHandler<PaperC> jieshou;
        public event EventHandler<PaperC> fasong;
        PaperC ssa;
        public class PaperC : EventArgs
        {
            public string Name { get; set; }                    //用于存储数据，当事件被调用时，可利用其进行传递数据。
            public string Name1 { get; set; }                    //用于存储数据，当事件被调用时，可利用其进行传递数据。
        }

        #endregion
    }
}
