using MyLib.Sys;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using xktComm.DataConvert;

namespace CXPro001.classes
{
    /// <summary>
    /// 8进8出控制器
    /// </summary>
    public class in8out8
    {
        /// <summary>
        /// 与刻字机通讯的串口
        /// </summary>
        public SerialPort serialPort;
        /// <summary>
        /// 串口号
        /// </summary>
        public string MyPortName;
        /// <summary>
        /// 波特率
        /// </summary>
        public int MyBaudRate;
        /// <summary>
        /// 获取string类型数据
        /// </summary>
        public string ReceiveData8740;
        /// <summary>
        /// 接收的字节数据
        /// </summary>
        public byte[] ReceiveByte=new byte[1024];
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
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mcom"></param>
        /// <param name="mbits"></param>
        /// <param name="mdes"></param>
        public in8out8(string mcom,int mbits,string mdes)
        {
            MyPortName = mcom;
            MyBaudRate = mbits;
            mdescrible = mdes;
            ssa = new PaperC();
         //  Connect();
        }
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <returns></returns>
        public EmRes Connect()
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

            return EmRes.Succeed;
        }
        /// <summary>
        /// 串口关闭
        /// </summary>
        public void DisConnet()
        {
            if (serialPort != null)
            {
                serialPort.Close();
            }
        }
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
                    ReceiveByte = rec;
                    // ReceiveData = Encoding.ASCII.GetString(rec);
                    ReceiveData8740 = StringLib.GetStringFromByteArray(rec, 0, rec.Length);
                    ssa.Name = ReceiveByte;
                    jieshou?.Invoke(this, ssa);
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
            if (IsConnected)
            {
                if (!serialPort.IsOpen)//如果断开连接就在连接一次
                {
                    serialPort.Open();
                }
                try
                {
                    serialPort.Write(mess);
                    return EmRes.Succeed;
                }
                catch (Exception ex)
                {
                    Logger.Error($"{mdescrible}串口发送信息失败：{ex.Message}");
                    return EmRes.Error;
                }
            }
            else
            {
                Logger.Error($"{mdescrible}串口没有开打");
                return EmRes.Error;
            }
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="mess"></param>
        /// <returns></returns>
        public EmRes sendbytes(byte[] mess)
        {
            if (IsConnected)
            {
                if (!serialPort.IsOpen)//如果断开连接就在连接一次
                {
                    serialPort.Open();
                }
                try
                {
                    serialPort.Write(mess,0,mess.Length);
                    ssa.Name1 = mess;
                    fasong?.Invoke(this, ssa);
                    return EmRes.Succeed;
                }
                catch (Exception ex)
                {
                    Logger.Error($"{mdescrible}串口发送信息失败：{ex.Message}");
                    return EmRes.Error;
                }
            }
            else
            {
                Logger.Error($"{mdescrible}串口没有开打");
                return EmRes.Error;
            }
        }
        public event EventHandler<PaperC> jieshou;
        public event EventHandler<PaperC> fasong;
        PaperC ssa;
        public class PaperC : EventArgs
        {
            public byte[] Name = new byte[1024];                  //用于存储数据，当事件被调用时，可利用其进行传递数据。
            public byte[] Name1 = new byte[1024];                   //用于存储数据，当事件被调用时，可利用其进行传递数据。
        }
    }
   
}
