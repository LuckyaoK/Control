using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


using System.Drawing;
using System.Windows.Forms;
using MyLib.Utilitys;
using CXPro001.myclass;
namespace CXPro001.classes
{
    /// <summary>
    /// S7-1200PLC通讯
    /// </summary>
    public  class s7com
    {
        #region 属性
        /// <summary>
        /// 连接用的IP地址
        /// </summary>
        public IPAddress ip;
        /// <summary>
        /// IP
        /// </summary>
        public string MIP;
        /// <summary>
        /// 端口
        /// </summary>
        public int Mport;
        /// <summary>
        /// 客户端
        /// </summary>
        Socket cliSocket;
        /// <summary>
        /// 连接OK
        /// </summary>
        public bool IsConnected = false;
        /// <summary>
        /// plc描述
        /// </summary>
        public string mdescrible;
        /// <summary>
        /// 发送用的通用指令
        /// </summary>
        byte[] AB =new byte[100]; 
        /// <summary>
        /// 用了装接收的字节组
        /// </summary>
        byte[] MsgReceiveBuffer = new byte[1024];
        
        /// <summary>
        /// 接收到的字节数
        /// </summary>
        public int countr = 0;
        /// <summary>
        /// 接收到的转成的字符串
        /// </summary>
        string ReceiveStringData = "";
        object loks = new object();
        #endregion
        #region 构造连接断开
        /// <summary>
        /// 构造1200
        /// </summary>
        /// <param name="mip"></param>
        /// <param name="mport"></param>
        /// <param name="des"></param>
        public s7com(string mip,int mport,string des)
        {
            MIP = mip;
            ip = IPAddress.Parse(mip);
            Mport = mport;
            mdescrible = des;
            connect();
          

        }
        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public EmRes connect()
        {
            if (ip==null)
            {
                Logger.Error($"{mdescrible}连接失败");
                return EmRes.Error;
            }
            var remoteEP = new IPEndPoint(ip, Mport); //'将网络端点表示为ip地址和端口号（IPAssress提供网际协议IP地址）
            cliSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //'确定接口的类型，寻址方案传输类型，支持的协议
            try
            {
                cliSocket.Connect(remoteEP);
                byte[] B = new byte[22] { 0x3, 0x0, 0x0, 0x16, 0x11, 0xE0, 0x0, 0x0, 0x0, 0x1, 0x0, 0xC0, 0x1, 0x9, 0xc1, 0x2, 0x4b, 0x54, 0xc2, 0x2, 0x2, 0x1 };//   COTP '
                 //c                      &H03 00  00  16   11   E0  00  00  00   01  00  C0   01  09   C1  02  4B   54   C2   02  02  01
                cliSocket.Send(B); // '将数据发送到接口上
                Thread.Sleep(50);
                B = new byte[25] { 0x03, 0x00, 0x00, 0x19, 0x02, 0xf0, 0x80, 0x32, 0x01, 0x00, 0x00, 0x01, 0x24, 0x00, 0x08, 0x00, 0x00, 0xf0, 0x00, 0x00, 0x01, 0x00, 0x01, 0x00, 0xf0 };
                // '03 00 00 19 02 F0 80 32 01 00 00 01 24 00 08 00 00 F0 00 00 01 00 01 00 F0 
                cliSocket.Send(B); // '将数据发送到接口上
                IsConnected = true;
                run();
                Logger.Info($"{mdescrible}连接成功！");
                #region 发送的指令
                AB = new byte[41];
                AB[0] = 0x03;//版本不变
                AB[1] = 0x00;//备用不变
                AB[2] = 0x00;//长度高8位
                AB[3] = 0x00;//长度低8位
                AB[4] = 0x02;//不变
                AB[5] = 0xF0;//不变
                AB[6] = 0x80;//不变
                AB[7] = 0x32;//S7类型不变
                AB[8] = 0x01;//job01
                AB[9] = 0x00;//不变
                AB[10] = 0x00;//不变
                AB[11] = 0x00;//每次加1不变
                AB[12] = 0x00;//每次加1不变
                AB[13] = 0x00;//DATA(ITEM)r部分总长度,算好
                AB[14] = 0x0E; //DATA(ITEM)r部分总长度,算好
                AB[15] = 0x00;//读的时候为0000返回时候才有数据
                AB[16] = 0x00;//读的时候为0000返回时候才有数据
                AB[17] = 0x04;//读04 写05
                AB[18] = 0x01;//组的数量
                AB[19] = 0x12;//不变
                AB[20] = 0x0A;//本Item其余部分的长度,不变
                AB[21] = 0x10;//不变
                AB[22] = 0x01;//确定变量的类型和长度1：bit 2：byte
                AB[23] = 0x00;//请求的数据长度高8位
                AB[24] = 0x01;//请求的数据长度低8位
                AB[25] = 0x00;//DB模块的编号，如果访问的不是DB区域，此处为0×0000
                AB[26] = 0x00;//DB模块的编号，如果访问的不是DB区域，此处为0×0000
                AB[27] = 0x84;//区域类型0x84= DB; 0X82= Q; 0x81=I; 0x83= M; 0x1d= S7 timers(T); 0x1c= S7counters(C);
                AB[28] = 0x00;// Address低3位表示位的  其余表示字
                AB[29] = 0x00;// Address低3位表示位的  其余表示字
                AB[30] = 0x00;// Address低3位表示位的  其余表示字
                AB[31] = 0x00;//data（item1）写入时才有，发送是设00
                AB[32] = 0x03;//data（item1）写入时才有，数据单位04byte 03bit
                AB[33] = 0x00;//ata（item1）写入时才有，写入数据的长度单位是位
                AB[34] = 0x00;//ata（item1）写入时才有，写入数据的长度单位是位
                AB[35] = 0x00;//ata（item1）写入时才有，要写入的数据
                AB[36] = 0x00;//ata（item1）写入时才有，要写入的数据
                AB[37] = 0x00;//ata（item1）写入时才有，要写入的数据
                AB[38] = 0x00;//ata（item1）写入时才有，要写入的数据
                AB[39] = 0x00;//ata（item1）写入时才有，要写入的数据
                AB[40] = 0x00;//ata（item1）写入时才有，要写入的数据
                #endregion
                return EmRes.Succeed;
            }
            catch (Exception ex)
            {
                Logger.Error($"{mdescrible}连接失败:{ex.Message}");
                IsConnected = false;
                return EmRes.Error;
               
            }
           
        }
        public void DisConnet()
        {
            IsConnected = false;
            cliSocket.Close();
            cliSocket.Dispose();
        }
        #endregion
        #region 接收线程 和发送
        Task run_task = null;
        public void run()
        {
            if (run_task == null || run_task != null && run_task.IsCompleted)
            {
                Logger.Write(Logger.InfoType.Info, $"{mdescrible}创建接收运行线程");
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
                        countr = cliSocket.Receive(MsgReceiveBuffer);
                        ReceiveStringData = Encoding.UTF8.GetString(MsgReceiveBuffer, 0, countr);//接收IP信息，没有信息发过来就一直卡这里。                        
                        //ssa.jiecout = countr;
                        //ssa.jiebyte = MsgReceiveBuffer;
                        //jieshou?.Invoke(this, ssa);

                    }
                    Thread.Sleep(10);
                    Application.DoEvents();
                }
                catch (Exception ex)
                {
                    IsConnected = false;
                    Logger.Debug($"与{mdescrible}通讯失败!{ex.Message}");
                  //  MessageBox.Show($"与扫码枪通讯失败!{ex.Message}");
                    run_task.Dispose();
                    return;
                }
            }
        }
        public void SendByte(byte[] mess)
        {
            if(IsConnected)
            {
                try
                {
                    countr = 0;
                    MsgReceiveBuffer = new byte[1024];
                    cliSocket.Send(mess);
                }
                catch (Exception ex)
                {
                    IsConnected = false;
                    Logger.Error($"发送信息给{mdescrible}失败{ex.Message}");
                    return;
                }
            }
            else
            {
                Logger.Error($"{mdescrible}未连接");
            }

        }
        #endregion
        #region 读写PLC地址 
        #region 读写bit 单个读 
        /// <summary>
        /// 写位,一次写一个
        /// </summary>
        /// <param name="mArea">地址区域</param>
        /// <param name="DBnumber">DB块号</param>
        /// <param name="star">起始地址</param>
        /// <param name="valu">on or off</param>     
        /// <returns></returns>
        public EmRes WriteBit(string mArea, int DBnumber, string star, string valu)//,int lens,string bitvalue="")
        {
            #region 数据格式
            AB = new byte[36];
            AB[0] = 0x03;//版本不变
            AB[1] = 0x00;//备用不变
            AB[2] = 0x00;//OPTIONS区域的长度高8位
            AB[3] = 0x24;//OPTIONS区域的长度低8位
            AB[4] = 0x02;//不变
            AB[5] = 0xF0;//不变
            AB[6] = 0x80;//不变
            AB[7] = 0x32;//S7类型不变
            AB[8] = 0x01;//job01不变
            AB[9] = 0x00;//不变
            AB[10] = 0x00;//不变
            AB[11] = 0x00;//每次加1不变
            AB[12] = 0x00;//每次加1不变
            AB[13] = 0x00;//DATA(ITEM)r部分总长度,算好----------------读时不变
            AB[14] = 0x0E; //DATA(ITEM)r部分总长度,算好----------------读时不变
            AB[15] = 0x00;//读的时候为0000  返回时候才有数据
            AB[16] = 0x05;//读的时候为0000  返回时候才有数据
            AB[17] = 0x05;//读04 写05---------------------?
            AB[18] = 0x01;//组的数量
            AB[19] = 0x12;//不变
            AB[20] = 0x0A;//本Item其余部分的长度,不变
            AB[21] = 0x10;//不变
            AB[22] = 0x01;//确定变量的类型和长度1：bit 2：byte----------------?
            AB[23] = 0x00;//请求的数据长度高8位 --------------------------?
            AB[24] = 0x01;//请求的数据长度低8位 ----------------------------?
            AB[25] = 0x00;//DB模块的编号，如果访问的不是DB区域，此处为0×0000 -------------------------?
            AB[26] = 0x00;//DB模块的编号，如果访问的不是DB区域，此处为0×0000 -------------------------?
            AB[27] = 0x84;//区域类型0x84= DB; 0X82= Q; 0x81=I; 0x83= M; 0x1d= S7 timers(T); 0x1c= S7counters(C);-------------------------------?
            AB[28] = 0x00;// Address低3位表示位的  其余表示字 --------------------------?
            AB[29] = 0x00;// Address低3位表示位的  其余表示字 ---------------------------?
            AB[30] = 0x00;// Address低3位表示位的  其余表示字 ---------------------------?
            AB[31] = 0x00;//data（item1）写入时才有，发送是设00
            AB[32] = 0x03;//data（item1）写入时才有，数据单位04byte 03bit
            AB[33] = 0x00;//ata（item1）数据的长度,单位是位
            AB[34] = 0x01;//ata（item1）数据的长度,单位是位
            AB[35] = 0x00;//ata（item1）写入时才有，要写入的数据
            //AB[36] = 0x00;//ata（item1）写入时才有，要写入的数据
            //AB[37] = 0x00;//ata（item1）写入时才有，要写入的数据
            //AB[38] = 0x00;//ata（item1）写入时才有，要写入的数据
            //AB[39] = 0x00;//ata（item1）写入时才有，要写入的数据
            //AB[40] = 0x00;//ata（item1）写入时才有，要写入的数据
            #endregion
            if (!IsConnected) return EmRes.Error;
            switch (mArea)
            {
                case "M":
                    AB[27] = 0X83;
                    break;
                case "I":
                    AB[27] = 0X81;
                    break;
                case "Q":
                    AB[27] = 0X82;
                    break;
                case "DB":
                    AB[27] = 0X84;
                    AB[25] = (byte)((DBnumber >> 8) & 0xFF);
                    AB[26] = (byte)(DBnumber & 0xFF);
                    break;
                case "T":
                    AB[27] = 0X1D;
                    break;
            }          
            string[] sta = star.Split(new char[] {'.'});
            if (sta.Count()<2)
            {
                Logger.Error($"{mdescrible}位读或写的位地址不对：{star}");
                return EmRes.Error;
            }
            Int32 j0 = Convert.ToInt32(sta[0]);
            Int32 j1 = Convert.ToInt32(sta[1]);
            Int32 j2 = (j0 << 3) + j1;
            byte[] j3 = new byte[4];//数据地址
            ConvertIntToByteArray(j2, ref j3);
            AB[28] = j3[2];
            AB[29] = j3[1];
            AB[30] = j3[0];
            if (valu=="ON") AB[35] = 0x01;
            else AB[35] = 0x00;
            try
            {
                countr = 0;
                MsgReceiveBuffer = new byte[1024];
                SendByte(AB);               
                Thread.Sleep(5);
                int a = 0;
                while(countr<1)
                {
                    Thread.Sleep(10);
                    a++;
                    if(a>50)
                    {
                        Logger.Error($"位写入操作失败,返回信息超时：{mdescrible}");
                        return EmRes.Error;
                    }
                }
                byte[] bufs = new byte[1024];
                bufs = MsgReceiveBuffer;
                if (bufs[21] == 0xFF)
                {                   
                    //Logger.Info($"位写入操作成功：{mdescrible}");
                    return EmRes.Succeed;
                }
                else
                {
                    //Logger.Error($"位写入操作失败：{mdescrible}");
                    return EmRes.Error;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return EmRes.Error;
            }
        }
        /// <summary>
        /// 读位--西门子读位只能单个读
        /// </summary>
        /// <param name="mArea">地址区域</param>
        /// <param name="DBnumber">DB块号</param>
        /// <param name="star">起始地址</param>
        /// <param name="valu">写入的值</param>   
        /// <param name="COUNTS">读取的个数</param>   
        /// <returns></returns>
        public EmRes ReadBit(string mArea,int DBnumber, string star, ref string valu,int COUNTS=1)
        {
            #region 数据格式
            AB = new byte[31];
            AB[0] = 0x03;//版本不变
            AB[1] = 0x00;//备用不变
            AB[2] = 0x00;//OPTIONS区域的长度高8位
            AB[3] = 0x1F;//OPTIONS区域的长度低8位
            AB[4] = 0x02;//不变
            AB[5] = 0xF0;//不变
            AB[6] = 0x80;//不变
            AB[7] = 0x32;//S7类型不变
            AB[8] = 0x01;//job01不变
            AB[9] = 0x00;//不变
            AB[10] = 0x00;//不变
            AB[11] = 0x00;//每次加1不变
            AB[12] = 0x00;//每次加1不变
            AB[13] = 0x00;//DATA(ITEM)r部分总长度,算好----------------读时不变
            AB[14] = 0x0E; //DATA(ITEM)r部分总长度,算好----------------读时不变
            AB[15] = 0x00;//读的时候为0000  返回时候才有数据
            AB[16] = 0x00;//读的时候为0000  返回时候才有数据
            AB[17] = 0x04;//读04 写05---------------------?
            AB[18] = 0x01;//组的数量
            AB[19] = 0x12;//不变
            AB[20] = 0x0A;//本Item其余部分的长度,不变
            AB[21] = 0x10;//不变
            AB[22] = 0x01;//确定变量的类型和长度1：bit 2：byte----------------?
            AB[23] = 0x00;//请求的数据长度高8位 --------------------------?
            AB[24] = 0x01;//请求的数据长度低8位 ----------------------------?
            AB[25] = 0x00;//DB模块的编号，如果访问的不是DB区域，此处为0×0000 -------------------------?
            AB[26] = 0x00;//DB模块的编号，如果访问的不是DB区域，此处为0×0000 -------------------------?
            AB[27] = 0x84;//区域类型0x84= DB; 0X82= Q; 0x81=I; 0x83= M; 0x1d= S7 timers(T); 0x1c= S7counters(C);-------------------------------?
            AB[28] = 0x00;// Address低3位表示位的  其余表示字 --------------------------?
            AB[29] = 0x00;// Address低3位表示位的  其余表示字 ---------------------------?
            AB[30] = 0x00;// Address低3位表示位的  其余表示字 ---------------------------?
                          
            #endregion
            if (!IsConnected) return EmRes.Error;
            switch (mArea)
            {
                case "M":
                    AB[27] = 0X83;
                    break;
                case "I":
                    AB[27] = 0X81;
                    break;
                case "Q":
                    AB[27] = 0X82;
                    break;
                case "DB":
                    AB[27] = 0X84;
                    AB[25] = (byte)((DBnumber>>8) & 0xFF);
                    AB[26] = (byte)(DBnumber & 0xFF);
                    break;
                case "T":
                    AB[27] = 0X1D;
                    break;
            }
            AB[24] = (byte)(COUNTS & 0xFF); 
            string[] sta = star.Split('.');
            if (sta.Count() < 2)
            {
                Logger.Error($"{mdescrible}位读或写的位地址不对：{star}");
                return EmRes.Error;
            }
            Int32 j0 = Convert.ToInt32(sta[0]);
            Int32 j1 = Convert.ToInt32(sta[1]);
            Int32 j2 = (j0 << 3) + j1;
            byte[] j3 = new byte[4];//数据地址
            ConvertIntToByteArray(j2, ref j3);
            AB[28] = j3[2];
            AB[29] = j3[1];
            AB[30] = j3[0];
            try
            {
                countr = 0;
                MsgReceiveBuffer = new byte[1024];
                SendByte(AB);
                Thread.Sleep(5);
                int a = 0;
                while (countr < 1)
                {
                    Thread.Sleep(10);
                    a++;
                    if (a > 50)
                    {
                        Logger.Error($"位写入操作失败,返回信息超时：{mdescrible}");
                        return EmRes.Error;
                    }
                }
                byte[] bufs = new byte[1024];
                bufs = MsgReceiveBuffer;                             
                if (bufs[21] == 0xFF)
                {
                    if (bufs[25] == 0x01) valu = "ON";
                    else valu = "OFF";
                   // Logger.Info($"位写入操作成功：{mdescrible}");
                    return EmRes.Succeed;
                }
                else
                {
                    //Logger.Error($"位写入操作失败：{mdescrible}");
                    return EmRes.Error;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return EmRes.Error;
            }
        }
        #endregion
        #region 读 PLC 字节
        /// <summary>
        /// 将int32转换成字节
        /// </summary>
        /// <param name="m"></param>
        /// <param name="arry"></param>
        /// <returns></returns>
        private bool ConvertIntToByteArray(Int32 m, ref byte[] arry)
        {
            if (arry == null) return false;
            if (arry.Length < 4) return false;
            arry[0] = (byte)(m & 0xFF);
            arry[1] = (byte)((m & 0xFF00) >> 8);
            arry[2] = (byte)((m & 0xFF0000) >> 16);
            arry[3] = (byte)((m >> 24) & 0xFF);
            return true;
        }
        /// <summary>
        /// 读字节--可读多个字节
        /// </summary>
        /// <param name="mArea">区域</param>
        /// <param name="DBnumber">DB块号</param>
        /// <param name="star">起始地址</param>
        /// <param name="lens">长度,单位：字节</param>
        /// <param name="valu">读取的值</param>
        /// <returns></returns>
        public EmRes ReadByte(string mArea, int DBnumber, string star,int lens,ref byte[] valu)
        {
            #region 数据格式
            AB = new byte[31];
            AB[0] = 0x03;//版本不变
            AB[1] = 0x00;//备用不变
            AB[2] = 0x00;//OPTIONS区域的长度高8位
            AB[3] = 0x1F;//OPTIONS区域的长度低8位
            AB[4] = 0x02;//不变
            AB[5] = 0xF0;//不变
            AB[6] = 0x80;//不变
            AB[7] = 0x32;//S7类型不变
            AB[8] = 0x01;//job01不变
            AB[9] = 0x00;//不变
            AB[10] = 0x00;//不变
            AB[11] = 0x00;//每次加1不变
            AB[12] = 0x00;//每次加1不变
            AB[13] = 0x00;//DATA(ITEM)r部分总长度,算好----------------读时不变
            AB[14] = 0x0E; //DATA(ITEM)r部分总长度,算好----------------读时不变
            AB[15] = 0x00;//读的时候为0000  返回时候才有数据
            AB[16] = 0x00;//读的时候为0000  返回时候才有数据
            AB[17] = 0x04;//读04 写05---------------------?
            AB[18] = 0x01;//组的数量
            AB[19] = 0x12;//不变
            AB[20] = 0x0A;//本Item其余部分的长度,不变
            AB[21] = 0x10;//不变
            AB[22] = 0x02;//确定变量的类型和长度1：bit 2：byte----------------?
            AB[23] = 0x00;//请求的数据长度高8位,单位字节  --------------------------?
            AB[24] = 0x02;//请求的数据长度低8位,单位字节 ----------------------------?
            AB[25] = 0x00;//DB模块的编号，如果访问的不是DB区域，此处为0×0000 -------------------------?
            AB[26] = 0x00;//DB模块的编号，如果访问的不是DB区域，此处为0×0000 -------------------------?
            AB[27] = 0x84;//区域类型0x84= DB; 0X82= Q; 0x81=I; 0x83= M; 0x1d= S7 timers(T); 0x1c= S7counters(C);-------------------------------?
            AB[28] = 0x00;// Address低3位表示位的  其余表示字 --------------------------?
            AB[29] = 0x00;// Address低3位表示位的  其余表示字 ---------------------------?
            AB[30] = 0x00;// Address低3位表示位的  其余表示字 ---------------------------?
                        
            #endregion
            if (!IsConnected) return EmRes.Error;
            switch (mArea)
            {
                case "M":
                    AB[27] = 0X83;
                    break;
                case "I":
                    AB[27] = 0X81;
                    break;
                case "Q":
                    AB[27] = 0X82;
                    break;
                case "DB":
                    AB[27] = 0X84;
                    AB[25] = (byte)((DBnumber >> 8) & 0xFF);
                    AB[26] = (byte)(DBnumber & 0xFF);
                    break;
                case "T":
                    AB[27] = 0X1D;
                    break;
            }
            if (!star.Contains(".")) star += ".0";
            string[] sta = star.Split('.');
            if (sta.Count() < 2)
            {
                Logger.Error($"{mdescrible}字节读的位地址不对：{star}");
                return EmRes.Error;
            }
            AB[23] = (byte)((lens >> 8) & 0xFF);
            AB[24] = (byte)(lens & 0xFF);
            Int32 j0 = Convert.ToInt32(sta[0]);
            Int32 j1 = Convert.ToInt32(sta[1]);
            Int32 j2 = (j0 << 3) + j1;
            byte[] j3 = new byte[4];//数据地址
            ConvertIntToByteArray(j2, ref j3);
            AB[28] = j3[2];
            AB[29] = j3[1];
            AB[30] = j3[0];
            try
            {
                countr = 0;
                MsgReceiveBuffer = new byte[1024];
                SendByte(AB);
                Thread.Sleep(5);
                int a = 0;
                while (countr < 1)
                {
                    Thread.Sleep(10);
                    a++;
                    if (a > 50)
                    {
                        Logger.Error($"字节读操作失败,返回信息超时：{mdescrible}");
                        return EmRes.Error;
                    }
                }
                byte[] bufs = new byte[1024];
                bufs = MsgReceiveBuffer;
               
                int c1 = countr;
                if (bufs[21] == 0xFF)
                {
                    valu = new byte[c1 - 25];
                   for(int i=25;i<c1;i++)
                    {
                        valu[i - 25] = bufs[i];
                    }
                    //Logger.Info($"位写入操作成功：{mdescrible}");
                    return EmRes.Succeed;
                }
                else
                {
                    Logger.Error($"字节读取操作失败：{mdescrible}");
                    return EmRes.Error;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return EmRes.Error;
            }          
        }
        #endregion
        #region 读写字符串-可多个读取
        /// <summary>
        /// 写入字符串--支持中文
        /// </summary>
        /// <param name="mArea">区域</param>
        /// <param name="DBnumber">DB块号</param>
        /// <param name="star">起始地址</param>
        /// <param name="lens">字节长度</param>
        /// <param name="valu">写入的值</param>
        /// <returns></returns>
        public EmRes WriteString(string mArea, int DBnumber, string star, int lens, string valu)
        {
            int bytecouts = Encoding.GetEncoding("gb2312").GetBytes(valu).Length;
            lens = bytecouts;
            #region 数据格式
            AB = new byte[35+lens];
            AB[0] = 0x03;//版本不变
            AB[1] = 0x00;//备用不变
            AB[2] = 0x00;//OPTIONS区域的长度高8位
            AB[3] = 0x1F;//OPTIONS区域的长度低8位
            AB[4] = 0x02;//不变
            AB[5] = 0xF0;//不变
            AB[6] = 0x80;//不变
            AB[7] = 0x32;//S7类型不变
            AB[8] = 0x01;//job01不变
            AB[9] = 0x00;//不变
            AB[10] = 0x00;//不变
            AB[11] = 0x00;//每次加1不变
            AB[12] = 0x00;//每次加1不变
            AB[13] = 0x00;//Parameter部分总长度,算好----------------读时不变
            AB[14] = 0x0E; //Parameter部分总长度,算好----------------读时不变
            AB[15] = 0x00;//data（item1）部分的长度---------------?
            AB[16] = 0x00;//data（item1）部分的长度---------------?
            AB[17] = 0x05;//读04 写05---------------------?
            AB[18] = 0x01;//组的数量
            AB[19] = 0x12;//不变
            AB[20] = 0x0A;//本Item其余部分的长度,不变
            AB[21] = 0x10;//不变
            AB[22] = 0x02;//确定变量的类型和长度1：bit 2：byte----------------?
            AB[23] = 0x00;//请求的数据长度高8位,单位字节  --------------------------?
            AB[24] = 0x01;//请求的数据长度低8位,单位字节 ----------------------------?
            AB[25] = 0x00;//DB模块的编号，如果访问的不是DB区域，此处为0×0000 -------------------------?
            AB[26] = 0x00;//DB模块的编号，如果访问的不是DB区域，此处为0×0000 -------------------------?
            AB[27] = 0x84;//区域类型0x84= DB; 0X82= Q; 0x81=I; 0x83= M; 0x1d= S7 timers(T); 0x1c= S7counters(C);-------------------------------?
            AB[28] = 0x00;// Address低3位表示位的  其余表示字 --------------------------?
            AB[29] = 0x00;// Address低3位表示位的  其余表示字 ---------------------------?
            AB[30] = 0x00;// Address低3位表示位的  其余表示字 ---------------------------?
            AB[31] = 0x00;//不变
            AB[32] = 0x04;//data（item1）写入时才有，数据单位04byte 03bit
            AB[33] = 0x00;//ata（item1）数据的长度,单位是位
            AB[34] = 0x01;//ata（item1）数据的长度,单位是位
            //AB[35] = 0x00;//  要写入的数据
            //AB[36] = 0x00;//  要写入的数据
          
            #endregion
            if (!IsConnected) return EmRes.Error;
            switch (mArea)
            {
                case "M":
                    AB[27] = 0X83;
                    break;
                case "I":
                    AB[27] = 0X81;
                    break;
                case "Q":
                    AB[27] = 0X82;
                    break;
                case "DB":
                    AB[27] = 0X84;
                    AB[25] = (byte)((DBnumber >> 8) & 0xFF);
                    AB[26] = (byte)(DBnumber & 0xFF);
                    break;
                case "T":
                    AB[27] = 0X1D;
                    break;
            }
            if (!star.Contains(".")) star += ".0";
            string[] sta = star.Split('.');
            if (sta.Count() < 2)
            {
                Logger.Error($"{mdescrible}写入字符串的位地址不对：{star}");
                return EmRes.Error;
            }
            AB[23] = (byte)((lens >> 8) & 0xFF);
            AB[24] = (byte)(lens & 0xFF);
            AB[2] = (byte)(((lens+35) >> 8) & 0xFF); //OPTIONS区域的长度高8位
            AB[3] = (byte)((lens+35) & 0xFF); ;//OPTIONS区域的长度低8位
            Int32 j0 = Convert.ToInt32(sta[0]);
            Int32 j1 = Convert.ToInt32(sta[1]);
            Int32 j2 = (j0 << 3) + j1;
            byte[] j3 = new byte[4];//数据地址
            ConvertIntToByteArray(j2, ref j3);

            AB[15] = (byte)(((lens + 4) >> 8) & 0xFF);//data（item1）部分的长度---------------?
            AB[16] = (byte)((lens + 4) & 0xFF); //data（item1）部分的长度---------------?


            AB[28] = j3[2];
            AB[29] = j3[1];
            AB[30] = j3[0];
            AB[33] = (byte)(((lens*8) >> 8) & 0xFF);//ata（item1）数据的长度,单位是位
            AB[34] = (byte)((lens*8) & 0xFF);//ata（item1）数据的长度,单位是位
            var aa4 = Encoding.GetEncoding("gb2312").GetBytes(valu);//这个最后能够兼容汉子
            for (int i = 35; i <35+lens; i++)
            {
                
                //var aa2 = Encoding.UTF8.GetBytes(valu);//不兼容汉子
                //var aa3 = Encoding.UTF32.GetBytes(valu);//获得的都是4倍


                AB[i] = aa4[i-35];
            }
            try
            {
                countr = 0;
                MsgReceiveBuffer = new byte[1024];
                SendByte(AB);
                Thread.Sleep(10);
                int a = 0;
                while (countr < 1)
                {
                    Thread.Sleep(10);
                    a++;
                    if (a > 100)
                    {
                        Logger.Error($"写入字符串操作失败,返回信息超时：{mdescrible}");
                        return EmRes.Error;
                    }
                }
                byte[] bufs = new byte[1024];
                bufs = MsgReceiveBuffer;
                if (bufs[21] == 0xFF)
                {                  
                    Logger.Info($"写入字符串操作成功：{mdescrible}");
                    return EmRes.Succeed;
                }
                else
                {
                    Logger.Error($"写入字符串操作失败：{mdescrible}");
                    return EmRes.Error;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return EmRes.Error;
            }
        }
        /// <summary>
        /// 读字符串--可读多个 -支持中文
        /// </summary>
        /// <param name="mArea">区域</param>
        /// <param name="DBnumber">DB块号</param>
        /// <param name="star">起始地址</param>
        /// <param name="lens">长度,单位：字节</param>
        /// <param name="valu">读取的值</param>
        /// <returns></returns>
        public EmRes ReadString(string mArea, int DBnumber, string star, int lens, ref string valu)
        {
            #region 数据格式
            AB = new byte[31];
         AB[0] = 0x03;//版本不变
            AB[1] = 0x00;//备用不变
            AB[2] = 0x00;//OPTIONS区域的长度高8位
            AB[3] = 0x1F;//OPTIONS区域的长度低8位
            AB[4] = 0x02;//不变
            AB[5] = 0xF0;//不变
            AB[6] = 0x80;//不变
            AB[7] = 0x32;//S7类型不变
            AB[8] = 0x01;//job01不变
            AB[9] = 0x00;//不变
            AB[10] = 0x00;//不变
            AB[11] = 0x00;//每次加1不变
            AB[12] = 0x00;//每次加1不变
            AB[13] = 0x00;//DATA(ITEM)r部分总长度,算好----------------读时不变
            AB[14] = 0x0E; //DATA(ITEM)r部分总长度,算好----------------读时不变
            AB[15] = 0x00;//读的时候为0000  返回时候才有数据
            AB[16] = 0x00;//读的时候为0000  返回时候才有数据
            AB[17] = 0x04;//读04 写05---------------------?
            AB[18] = 0x01;//组的数量
            AB[19] = 0x12;//不变
            AB[20] = 0x0A;//本Item其余部分的长度,不变
            AB[21] = 0x10;//不变
            AB[22] = 0x02;//确定变量的类型和长度1：bit 2：byte----------------?
            AB[23] = 0x00;//请求的数据长度高8位,单位字节  --------------------------?
            AB[24] = 0x02;//请求的数据长度低8位,单位字节 ----------------------------?
            AB[25] = 0x00;//DB模块的编号，如果访问的不是DB区域，此处为0×0000 -------------------------?
            AB[26] = 0x00;//DB模块的编号，如果访问的不是DB区域，此处为0×0000 -------------------------?
            AB[27] = 0x84;//区域类型0x84= DB; 0X82= Q; 0x81=I; 0x83= M; 0x1d= S7 timers(T); 0x1c= S7counters(C);-------------------------------?
            AB[28] = 0x00;// Address低3位表示位的  其余表示字 --------------------------?
            AB[29] = 0x00;// Address低3位表示位的  其余表示字 ---------------------------?
            AB[30] = 0x00;// Address低3位表示位的  其余表示字 ---------------------------?

            #endregion
            if (!IsConnected) return EmRes.Error;
            switch (mArea)
            {
                case "M":
                    AB[27] = 0X83;
                    break;
                case "I":
                    AB[27] = 0X81;
                    break;
                case "Q":
                    AB[27] = 0X82;
                    break;
                case "DB":
                    AB[27] = 0X84;
                    AB[25] = (byte)((DBnumber >> 8) & 0xFF);
                    AB[26] = (byte)(DBnumber & 0xFF);
                    break;
                case "T":
                    AB[27] = 0X1D;
                    break;
            }
            if (!star.Contains(".")) star += ".0";
            string[] sta = star.Split('.');
            if (sta.Count() < 2)
            {
                Logger.Error($"{mdescrible}读字符串地址不对：{star}");
                return EmRes.Error;
            }
            AB[23] = (byte)((lens >> 8) & 0xFF);
            AB[24] = (byte)(lens & 0xFF);
            Int32 j0 = Convert.ToInt32(sta[0]);
            Int32 j1 = Convert.ToInt32(sta[1]);
            Int32 j2 = (j0 << 3) + j1;
            byte[] j3 = new byte[4];//数据地址
            ConvertIntToByteArray(j2, ref j3);
            AB[28] = j3[2];
            AB[29] = j3[1];
            AB[30] = j3[0];
            try
            {
                countr = 0;
                MsgReceiveBuffer = new byte[1024];
                SendByte(AB);
                Thread.Sleep(5);
                int a = 0;
                while (countr < 1)
                {
                    Thread.Sleep(10);
                    a++;
                    if (a > 50)
                    {
                        Logger.Error($"读字符串操作失败,返回信息超时：{mdescrible}");
                        return EmRes.Error;
                    }
                }
                byte[] bufs = new byte[1024];
                bufs = MsgReceiveBuffer;

                int c1 = countr;
                if (bufs[21] == 0xFF)
                {
                    valu = Encoding.Default.GetString(bufs,25,c1-25);
                    var aa4 = Encoding.GetEncoding("gb2312").GetString(bufs, 25, c1 - 25);
                    //byte[] valu1 = new byte[c1 - 25];
                    // for (int i = 25; i < c1; i++)
                    // {
                    //     valu1[i - 25] = bufs[i];
                    // }
                    // valu = Encoding.Default.GetString(valu1);
                    return EmRes.Succeed;
                }
                else
                {                   
                    return EmRes.Error;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return EmRes.Error;
            }
        }
        #endregion
        #region 读取 写入整数值
        /// <summary>
        /// 读取整数,一次读取一个16位的
        /// </summary>
        /// <param name="mArea">区域</param>
        /// <param name="DBnumber">DB块号</param>
        /// <param name="star">起始地址</param>
        /// <param name="lens">字节长度</param>
        /// <param name="valu">读取的值</param>
        /// <returns></returns>
        public EmRes ReadInt16(string mArea, int DBnumber, string star,  ref string valu, int floatcous = 1)
        {
            #region 数据格式
            AB = new byte[31];
            AB[0] = 0x03;//版本不变
            AB[1] = 0x00;//备用不变
            AB[2] = 0x00;//OPTIONS区域的长度高8位
            AB[3] = 0x1F;//OPTIONS区域的长度低8位
            AB[4] = 0x02;//不变
            AB[5] = 0xF0;//不变
            AB[6] = 0x80;//不变
            AB[7] = 0x32;//S7类型不变
            AB[8] = 0x01;//job01不变
            AB[9] = 0x00;//不变
            AB[10] = 0x00;//不变
            AB[11] = 0x00;//每次加1不变
            AB[12] = 0x00;//每次加1不变
            AB[13] = 0x00;//DATA(ITEM)r部分总长度,算好----------------读时不变
            AB[14] = 0x0E; //DATA(ITEM)r部分总长度,算好----------------读时不变
            AB[15] = 0x00;//读的时候为0000  返回时候才有数据
            AB[16] = 0x00;//读的时候为0000  返回时候才有数据
            AB[17] = 0x04;//读04 写05---------------------?
            AB[18] = 0x01;//组的数量
            AB[19] = 0x12;//不变
            AB[20] = 0x0A;//本Item其余部分的长度,不变
            AB[21] = 0x10;//不变
            AB[22] = 0x02;//确定变量的类型和长度1：bit 2：byte----------------?
            AB[23] = 0x00;//请求的数据长度高8位,单位字节  --------------------------?
            AB[24] = 0x02;//请求的数据长度低8位,单位字节 ----------------------------?
            AB[25] = 0x00;//DB模块的编号，如果访问的不是DB区域，此处为0×0000 -------------------------?
            AB[26] = 0x00;//DB模块的编号，如果访问的不是DB区域，此处为0×0000 -------------------------?
            AB[27] = 0x84;//区域类型0x84= DB; 0X82= Q; 0x81=I; 0x83= M; 0x1d= S7 timers(T); 0x1c= S7counters(C);-------------------------------?
            AB[28] = 0x00;// Address低3位表示位的  其余表示字 --------------------------?
            AB[29] = 0x00;// Address低3位表示位的  其余表示字 ---------------------------?
            AB[30] = 0x00;// Address低3位表示位的  其余表示字 ---------------------------?

            #endregion
            if (!IsConnected) return EmRes.Error;
            switch (mArea)
            {
                case "M":
                    AB[27] = 0X83;
                    break;
                case "I":
                    AB[27] = 0X81;
                    break;
                case "Q":
                    AB[27] = 0X82;
                    break;
                case "DB":
                    AB[27] = 0X84;
                    AB[25] = (byte)((DBnumber >> 8) & 0xFF);
                    AB[26] = (byte)(DBnumber & 0xFF);
                    break;
                case "T":
                    AB[27] = 0X1D;
                    break;
            }
            AB[24] = (byte)((floatcous * 2) & 0xFF);
            if (!star.Contains(".")) star += ".0";
            string[] sta = star.Split('.');
            if (sta.Count() < 2)
            {
                Logger.Error($"{mdescrible}读取整数的地址不对：{star}");
                return EmRes.Error;
            }
            //AB[23] = (byte)((lens >> 8) & 0xFF);
            //AB[24] = (byte)(lens & 0xFF);
            Int32 j0 = Convert.ToInt32(sta[0]);
            Int32 j1 = Convert.ToInt32(sta[1]);
            Int32 j2 = (j0 << 3) + j1;
            byte[] j3 = new byte[4];//数据地址
            ConvertIntToByteArray(j2, ref j3);
            AB[28] = j3[2];
            AB[29] = j3[1];
            AB[30] = j3[0];
            try
            {
                countr = 0;
                MsgReceiveBuffer = new byte[1024];
                SendByte(AB);
                Thread.Sleep(5);
                int a = 0;
                while (countr < 1)
                {
                    Thread.Sleep(10);
                    a++;
                    if (a > 50)
                    {
                        Logger.Error($"读取整数操作失败,返回信息超时：{mdescrible}");
                        return EmRes.Error;
                    }
                }
                byte[] bufs = new byte[1024];
                bufs = MsgReceiveBuffer;

                int c1 = countr;
                if (bufs[21] == 0xFF)
                {
                    byte[] valu1 = new byte[c1 - 25];
                    for (int i = 25; i < c1; i++)
                    {
                        valu1[i - 25] = bufs[c1-1-i+25];
                    }
                    for (int i = 0; i < floatcous; i++)
                    {
                        if(i== floatcous-1)
                        {
                            valu += (BitConverter.ToInt16(valu1, i * 2)).ToString();
                        }
                        else
                        {
                            valu += (BitConverter.ToInt16(valu1, i * 2)).ToString() + ",";
                        }                     
                    }                
                    return EmRes.Succeed;
                }
                else
                {                   
                    return EmRes.Error;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return EmRes.Error;
            }
        }
        /// <summary>
        /// 写入整数,一次写入一个16位的
        /// </summary>
        /// <param name="mArea">区域</param>
        /// <param name="DBnumber">DB块号</param>
        /// <param name="star">起始地址</param>
        /// <param name="lens">字节长度</param>
        /// <param name="valu">写入的值</param>
        /// <returns></returns>
        public EmRes WriteInt16(string mArea, int DBnumber, string star,  string valu)
        {
            #region 数据格式
            AB = new byte[37];
            AB[0] = 0x03;//版本不变
            AB[1] = 0x00;//备用不变
            AB[2] = 0x00;//OPTIONS区域的长度高8位
            AB[3] = 0x25;//OPTIONS区域的长度低8位
            AB[4] = 0x02;//不变
            AB[5] = 0xF0;//不变
            AB[6] = 0x80;//不变
            AB[7] = 0x32;//S7类型不变
            AB[8] = 0x01;//job01不变
            AB[9] = 0x00;//不变
            AB[10] = 0x00;//不变
            AB[11] = 0x00;//每次加1不变
            AB[12] = 0x00;//每次加1不变
            AB[13] = 0x00;//Parameter部分总长度,算好----------------读时不变
            AB[14] = 0x0E; //Parameter部分总长度,算好----------------读时不变
            AB[15] = 0x00;//data（item1）部分的长度---------------?
            AB[16] = 0x06;//data（item1）部分的长度---------------?
            AB[17] = 0x05;//读04 写05---------------------?
            AB[18] = 0x01;//组的数量
            AB[19] = 0x12;//不变
            AB[20] = 0x0A;//本Item其余部分的长度,不变
            AB[21] = 0x10;//不变
            AB[22] = 0x02;//确定变量的类型和长度1：bit 2：byte----------------?
            AB[23] = 0x00;//请求的数据长度高8位,单位字节  --------------------------?
            AB[24] = 0x02;//请求的数据长度低8位,单位字节 ----------------------------?
            AB[25] = 0x00;//DB模块的编号，如果访问的不是DB区域，此处为0×0000 -------------------------?
            AB[26] = 0x00;//DB模块的编号，如果访问的不是DB区域，此处为0×0000 -------------------------?
            AB[27] = 0x84;//区域类型0x84= DB; 0X82= Q; 0x81=I; 0x83= M; 0x1d= S7 timers(T); 0x1c= S7counters(C);-------------------------------?
            AB[28] = 0x00;// Address低3位表示位的  其余表示字 --------------------------?
            AB[29] = 0x00;// Address低3位表示位的  其余表示字 ---------------------------?
            AB[30] = 0x00;// Address低3位表示位的  其余表示字 ---------------------------?
            AB[31] = 0x00;//不变
            AB[32] = 0x04;//data（item1）写入时才有，数据单位04byte 03bit
            AB[33] = 0x00;//ata（item1）数据的长度,单位是位
            AB[34] = 0x10;//ata（item1）数据的长度,单位是位
            AB[35] = 0x00;//  要写入的数据
            AB[36] = 0x00;//  要写入的数据

            #endregion
            if (!IsConnected) return EmRes.Error;
            switch (mArea)
            {
                case "M":
                    AB[27] = 0X83;
                    break;
                case "I":
                    AB[27] = 0X81;
                    break;
                case "Q":
                    AB[27] = 0X82;
                    break;
                case "DB":
                    AB[27] = 0X84;
                    AB[25] = (byte)((DBnumber >> 8) & 0xFF);
                    AB[26] = (byte)(DBnumber & 0xFF);
                    break;
                case "T":
                    AB[27] = 0X1D;
                    break;
            }
            if (!star.Contains(".")) star += ".0";
            string[] sta = star.Split('.');
            if (sta.Count() < 2)
            {
                Logger.Error($"{mdescrible}写入整数的位地址不对：{star}");
                return EmRes.Error;
            }
            //AB[23] = (byte)((lens >> 8) & 0xFF);
            //AB[24] = (byte)(lens & 0xFF);
            Int32 j0 = Convert.ToInt32(sta[0]);
            Int32 j1 = Convert.ToInt32(sta[1]);
            Int32 j2 = (j0 << 3) + j1;
            byte[] j3 = new byte[4];//数据地址
            ConvertIntToByteArray(j2, ref j3);
            AB[28] = j3[2];
            AB[29] = j3[1];
            AB[30] = j3[0];
            //AB[33] = (byte)(((lens * 8) >> 8) & 0xFF);//ata（item1）数据的长度,单位是位
            //AB[34] = (byte)((lens * 8) & 0xFF);//ata（item1）数据的长度,单位是位
            var aa = Convert.ToInt16(valu);//将字符串值转换成整数
            AB[35] = (byte)((aa >> 8) & 0xFF);
            AB[36] = (byte)(aa & 0xFF);
            try
            {
                countr = 0;
                MsgReceiveBuffer = new byte[1024];
                SendByte(AB);
                Thread.Sleep(5);
                int a = 0;
                while (countr < 1)
                {
                    Thread.Sleep(10);
                    a++;
                    if (a > 50)
                    {
                        Logger.Error($"写入整数操作失败,返回信息超时：{mdescrible}");
                        return EmRes.Error;
                    }
                }
                byte[] bufs = new byte[1024];
                bufs = MsgReceiveBuffer;
                if (bufs[21] == 0xFF)
                {
                    //Logger.Info($"int16写入操作成功：{mdescrible}");
                    return EmRes.Succeed;
                }
                else
                {
                    //Logger.Error($"位写入操作失败：{mdescrible}");
                    return EmRes.Error;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return EmRes.Error;
            }
        }
        #endregion
        #region 读取写入浮点数
        /// <summary>
        /// 读取浮点数,一次读取一个32位的 可读取多个
        /// </summary>
        /// <param name="mArea">区域</param>
        /// <param name="DBnumber">DB块号</param>
        /// <param name="star">起始地址</param>
        /// <param name="floatcous">读取个数</param>
        /// <param name="valu">读取的值</param>
        /// <returns></returns>
        public EmRes ReadReal(string mArea, int DBnumber, string star,  ref string valu,int floatcous=1)
        {
            #region 数据格式
            AB = new byte[31];
            AB[0] = 0x03;//版本不变
            AB[1] = 0x00;//备用不变
            AB[2] = 0x00;//OPTIONS区域的长度高8位
            AB[3] = 0x1F;//OPTIONS区域的长度低8位
            AB[4] = 0x02;//不变
            AB[5] = 0xF0;//不变
            AB[6] = 0x80;//不变
            AB[7] = 0x32;//S7类型不变
            AB[8] = 0x01;//job01不变
            AB[9] = 0x00;//不变
            AB[10] = 0x00;//不变
            AB[11] = 0x00;//每次加1不变
            AB[12] = 0x00;//每次加1不变
            AB[13] = 0x00;//DATA(ITEM)r部分总长度,算好----------------读时不变
            AB[14] = 0x0E; //DATA(ITEM)r部分总长度,算好----------------读时不变
            AB[15] = 0x00;//读的时候为0000  返回时候才有数据
            AB[16] = 0x00;//读的时候为0000  返回时候才有数据
            AB[17] = 0x04;//读04 写05---------------------?
            AB[18] = 0x01;//组的数量
            AB[19] = 0x12;//不变
            AB[20] = 0x0A;//本Item其余部分的长度,不变
            AB[21] = 0x10;//不变
            AB[22] = 0x02;//确定变量的类型和长度1：bit 2：byte----------------?
            AB[23] = 0x00;//请求的数据长度高8位,单位字节  --------------------------?
            AB[24] = 0x04;//请求的数据长度低8位,单位字节 ----------------------------?
            AB[25] = 0x00;//DB模块的编号，如果访问的不是DB区域，此处为0×0000 -------------------------?
            AB[26] = 0x00;//DB模块的编号，如果访问的不是DB区域，此处为0×0000 -------------------------?
            AB[27] = 0x84;//区域类型0x84= DB; 0X82= Q; 0x81=I; 0x83= M; 0x1d= S7 timers(T); 0x1c= S7counters(C);-------------------------------?
            AB[28] = 0x00;// Address低3位表示位的  其余表示字 --------------------------?
            AB[29] = 0x00;// Address低3位表示位的  其余表示字 ---------------------------?
            AB[30] = 0x00;// Address低3位表示位的  其余表示字 ---------------------------?

            #endregion
            if (!IsConnected) return EmRes.Error;
            switch (mArea)
            {
                case "M":
                    AB[27] = 0X83;
                    break;
                case "I":
                    AB[27] = 0X81;
                    break;
                case "Q":
                    AB[27] = 0X82;
                    break;
                case "DB":
                    AB[27] = 0X84;
                    AB[25] = (byte)((DBnumber >> 8) & 0xFF);
                    AB[26] = (byte)(DBnumber & 0xFF);
                    break;
                case "T":
                    AB[27] = 0X1D;
                    break;
            }
            if (!star.Contains(".")) star += ".0";
            string[] sta = star.Split('.');
            if (sta.Count() < 2)
            {
                Logger.Error($"{mdescrible}读取浮点数的地址不对：{star}");
                return EmRes.Error;
            }
            int lens = floatcous * 4;
            AB[23] = (byte)((lens >> 8) & 0xFF);
            AB[24] = (byte)(lens & 0xFF);
            Int32 j0 = Convert.ToInt32(sta[0]);
            Int32 j1 = Convert.ToInt32(sta[1]);
            Int32 j2 = (j0 << 3) + j1;
            byte[] j3 = new byte[4];//数据地址
            ConvertIntToByteArray(j2, ref j3);
            AB[28] = j3[2];
            AB[29] = j3[1];
            AB[30] = j3[0];
            try
            {
                countr = 0;
                MsgReceiveBuffer = new byte[1024];
                SendByte(AB);
                Thread.Sleep(5);
                int a = 0;
                while (countr < 1)
                {
                    Thread.Sleep(10);
                    a++;
                    if (a > 50)
                    {
                        Logger.Error($"读取浮点数操作失败,返回信息超时：{mdescrible}");
                        return EmRes.Error;
                    }
                }
                byte[] bufs = new byte[1024];
                bufs = MsgReceiveBuffer;

                int c1 = countr;
                if (bufs[21] == 0xFF)
                {
                    int DM1 = c1 - 25;//接收到的字节数据的个数
                    int DM2 = DM1 / 4;//多少个浮点数
                    if(DM2!= floatcous) return EmRes.Error;
                    float DM3 = 0.0f;//浮点数
                    byte[] valu1 = new byte[4];
                    for (int i = 0; i < DM2; i++)
                    {
                        for (int j = 25+(i*4); j < 29 + (i * 4); j++)
                        {
                            valu1[(j - (25 + (i * 4)))] = bufs[(29 + (i * 4)-1)- (j - (25 + (i * 4)))];                                                                          
                        }
                        DM3 = BitConverter.ToSingle(valu1, 0);
                        if (i == DM2 - 1) valu += DM3.ToString();
                        else valu += DM3.ToString() + ",";
                    }                   
                    return EmRes.Succeed;
                }
                else
                {
                    return EmRes.Error;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return EmRes.Error;
            }
        }
        /// <summary>
        /// 写入浮点数,一次写入一个32位的
        /// </summary>
        /// <param name="mArea">区域</param>
        /// <param name="DBnumber">DB块号</param>
        /// <param name="star">起始地址</param>
        /// <param name="lens">字节长度</param>
        /// <param name="valu">写入的值</param>
        /// <returns></returns>
        public EmRes WriteReal(string mArea, int DBnumber, string star, string valu)
        {           
            #region 数据格式
            AB = new byte[39];
            AB[0] = 0x03;//版本不变
            AB[1] = 0x00;//备用不变
            AB[2] = 0x00;//OPTIONS区域的长度高8位
            AB[3] = 0x27;//OPTIONS区域的长度低8位
            AB[4] = 0x02;//不变
            AB[5] = 0xF0;//不变
            AB[6] = 0x80;//不变
            AB[7] = 0x32;//S7类型不变
            AB[8] = 0x01;//job01不变
            AB[9] = 0x00;//不变
            AB[10] = 0x00;//不变
            AB[11] = 0x00;//每次加1不变
            AB[12] = 0x00;//每次加1不变
            AB[13] = 0x00;//Parameter部分总长度,算好----------------读时不变
            AB[14] = 0x0E; //Parameter部分总长度,算好----------------读时不变
            AB[15] = 0x00;//data（item1）部分的长度---------------?
            AB[16] = 0x08;//data（item1）部分的长度---------------?
            AB[17] = 0x05;//读04 写05---------------------?
            AB[18] = 0x01;//组的数量
            AB[19] = 0x12;//不变
            AB[20] = 0x0A;//本Item其余部分的长度,不变
            AB[21] = 0x10;//不变
            AB[22] = 0x02;//确定变量的类型和长度1：bit 2：byte----------------?
            AB[23] = 0x00;//请求的数据长度高8位,单位字节  --------------------------?
            AB[24] = 0x04;//请求的数据长度低8位,单位字节 ----------------------------?
            AB[25] = 0x00;//DB模块的编号，如果访问的不是DB区域，此处为0×0000 -------------------------?
            AB[26] = 0x00;//DB模块的编号，如果访问的不是DB区域，此处为0×0000 -------------------------?
            AB[27] = 0x84;//区域类型0x84= DB; 0X82= Q; 0x81=I; 0x83= M; 0x1d= S7 timers(T); 0x1c= S7counters(C);-------------------------------?
            AB[28] = 0x00;// Address低3位表示位的  其余表示字 --------------------------?
            AB[29] = 0x00;// Address低3位表示位的  其余表示字 ---------------------------?
            AB[30] = 0x00;// Address低3位表示位的  其余表示字 ---------------------------?
            AB[31] = 0x00;//不变
            AB[32] = 0x04;//data（item1）写入时才有，数据单位04byte 03bit
            AB[33] = 0x00;//ata（item1）数据的长度,单位是位
            AB[34] = 0x20;//ata（item1）数据的长度,单位是位
            AB[35] = 0x00;//  要写入的数据
            AB[36] = 0x00;//  要写入的数据
            AB[37] = 0x00;//  要写入的数据
            AB[38] = 0x00;//  要写入的数据
            #endregion
            if (!IsConnected) return EmRes.Error;
            switch (mArea)
            {
                case "M":
                    AB[27] = 0X83;
                    break;
                case "I":
                    AB[27] = 0X81;
                    break;
                case "Q":
                    AB[27] = 0X82;
                    break;
                case "DB":
                    AB[27] = 0X84;
                    AB[25] = (byte)((DBnumber >> 8) & 0xFF);
                    AB[26] = (byte)(DBnumber & 0xFF);
                    break;
                case "T":
                    AB[27] = 0X1D;
                    break;
            }
            if (!star.Contains(".")) star += ".0";
            string[] sta = star.Split('.');
            if (sta.Count() < 2)
            {
                Logger.Error($"{mdescrible}写入浮点数地址不对：{star}");
                return EmRes.Error;
            }
            //AB[23] = (byte)((lens >> 8) & 0xFF);
            //AB[24] = (byte)(lens & 0xFF);
            Int32 j0 = Convert.ToInt32(sta[0]);
            Int32 j1 = Convert.ToInt32(sta[1]);
            Int32 j2 = (j0 << 3) + j1;
            byte[] j3 = new byte[4];//数据地址
            ConvertIntToByteArray(j2, ref j3);
            AB[28] = j3[2];
            AB[29] = j3[1];
            AB[30] = j3[0];
            //AB[33] = (byte)(((lens * 8) >> 8) & 0xFF);//ata（item1）数据的长度,单位是位
            //AB[34] = (byte)((lens * 8) & 0xFF);//ata（item1）数据的长度,单位是位
            //将值转换成字节                                  
            float aa1 = Convert.ToSingle(valu);//字符串转浮点
            var aa = BitConverter.GetBytes(aa1);//浮点转字节
            AB[35] = aa[3];
            AB[36] = aa[2];
            AB[37] = aa[1];
            AB[38] = aa[0];

            try
            {
                countr = 0;
                MsgReceiveBuffer = new byte[1024];
                SendByte(AB);
                Thread.Sleep(5);
                int a = 0;
                while (countr < 1)
                {
                    Thread.Sleep(10);
                    a++;
                    if (a > 50)
                    {
                        Logger.Error($"写入浮点数操作失败,返回信息超时：{mdescrible}");
                        return EmRes.Error;
                    }
                }
                byte[] bufs = new byte[1024];
                bufs = MsgReceiveBuffer;
                if (bufs[21] == 0xFF)
                {
                   
                    return EmRes.Succeed;
                }
                else
                {                   
                    return EmRes.Error;
                }
            }
            catch (Exception ex)
            {
                Logger.Error( mdescrible+ ex.Message);
                return EmRes.Error;
            }
        }
        #endregion
        #endregion
        /// <summary>
        /// 读取二维码或流水号
        /// </summary>
        /// <param name="mArea">区域</param>
        /// <param name="DBnumber">DB块号</param>
        /// <param name="star">起始地址</param>
        /// <param name="valu">返回的值</param>
        /// <returns></returns>
        public EmRes ReadSN(string mArea, int DBnumber, string star, ref string valu, int couta = 1)
        {
            EmRes ret = EmRes.Succeed;
            string sn = "";
          //40-52-64-76-88
            byte[] valu2 = new byte[couta * 12];
            int lens = couta * 12;
            ret = ReadByte(mArea, DBnumber, star, lens, ref valu2);
            string aa1= Encoding.ASCII.GetString(valu2);
            sn = aa1.Replace("\u0000", ",");
            sn = sn.TrimEnd(',');
            //for (int i = 0; i < couta; i++)
            //{              
            //    if (i == (couta-1))
            //    {                     
            //        sn +=Encoding.ASCII.GetString(valu2, i * 12, 11);//12 25 36
            //    }
            //    else
            //    {                   
            //        sn += Encoding.ASCII.GetString(valu2, i * 12, 11) + ",";
            //    }
            //}
            valu = sn;
            return ret;
        }

        #region 对外接口

        /// <summary>
        /// 读或者写 S7-1200的地址
        /// </summary>
        /// <param name="RW">true:读，false:写</param>
        /// <param name="mArea">区域：DB M I Q等</param>
        /// <param name="DBnumber">DB块号</param>
        /// <param name="star">起始地址</param>
        /// <param name="valu">写入或读取的值， 必须是string类型</param>
        /// <param name="inttype"> 1：整数，2：浮点数，3：bool，4，字符串，5，二维码</param>
        /// <param name="floatcous">长度</param>
        /// <returns></returns>
        public EmRes RWS71200(bool RW, string mArea, int DBnumber, string star, ref string valu, int inttype, int floatcous = 1)
        {
            lock (loks)
            {
                if(!IsConnected) return EmRes.Error;
                EmRes ret = EmRes.Error;
                if (RW)//读
                {
                    if (inttype == 1)//读取整数
                    {
                        string i = "";
                        ret = ReadInt16(mArea, DBnumber, star, ref i, floatcous);
                        valu = i;
                    }
                    else if (inttype == 2)//读取浮点数
                    {
                        string i = "";
                        ret = ReadReal(mArea, DBnumber, star, ref i, floatcous);
                        valu = i;
                    }
                    else if (inttype == 3)//读取bool
                    {
                        string i = "";
                        ret = ReadBit(mArea, DBnumber, star, ref i);
                        valu = i;
                    }
                    else if (inttype == 4)//读取字符串
                    {
                        string i = "";
                        ret = ReadString(mArea, DBnumber, star, floatcous, ref i);
                        valu = i;
                    }
                    else if (inttype == 5)//读流水号 二维码
                    {
                        string i = "";
                        ret = ReadSN(mArea, DBnumber, star, ref i, floatcous);
                        valu = i;
                    }
                    return ret;
                }
                else//写
                {
                    if (inttype == 1)//写入整数
                    {
                        ret = WriteInt16(mArea, DBnumber, star, valu);
                    }
                    else if (inttype == 2) //浮点数
                    {
                        ret = WriteReal(mArea, DBnumber, star, valu);
                    }
                    else if (inttype == 3)//bool 
                    {
                        if (valu == "ON")
                        {
                            ret = WriteBit(mArea, DBnumber, star, "ON");
                        }
                        if (valu == "OFF")
                        {
                            ret = WriteBit(mArea, DBnumber, star, "OFF");
                        }
                    }
                    else if (inttype == 4) //字符串
                    {
                        ret = WriteString(mArea, DBnumber, star, floatcous, valu);
                    }
                    return ret;
                }
            }
        }


        #endregion


        /* 西门子整数转位注意：贼恶心
         * 16位整数INT0.0:   0 0 0 0 0 0 0 0                            0 0 0 0 0 0 0 0
         * 对应的位          0.7 0.6  0.5   0.4   0.3  0.2 0.1  0.0    1.7 1.6 1.5 1.4 1.3 1.2 1.1  1.0
         * 
         * 
         */



        #region  事件向外输送信息--不用了
        public event EventHandler<PaperC> jieshou;
        public event EventHandler<PaperC> fasong;
        PaperC ssa=new PaperC();
        public class PaperC : EventArgs
        {
            public int jiecout;
            public int facout;
            public byte[] jiebyte = new byte[1024];
            /// <summary>
            /// 最终要外发初速的真正发送的字节组
            /// </summary>
            public byte[] fabyte = new byte[1024];
            /// <summary>
            /// 最终要外发出去的接收到的真正字节组
            /// </summary>
            public byte[] receive
            {
                get
                {
                    _receive = new byte[jiecout];
                    for (int i=0;i<jiecout;i++)
                    {
                        _receive[i] = jiebyte[i];
                    }
                    return _receive;
                }
            }
            private byte[] _receive;
          
            public string Name { get; set; }               
            public string Name1 { get; set; }         
        }
        #endregion

















    }
}
