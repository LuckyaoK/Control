using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using xktComm.DataConvert;

using CXPro001.myclass;
namespace CXPro001.classes
{
    /// <summary>
    /// 耐压仪7631
    /// </summary>
    public  class test7631
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
        public string ReceiveData8740="";
        
        /// <summary>
        /// 是否连接成功
        /// </summary>
        public bool IsConnected = false;
        /// <summary>
        /// 打标机的描述
        /// </summary>
        public string mdescrible;
        /// <summary>
        /// 构造刻字机
        /// </summary>
        /// <param name="mPortName">串口号</param>
        /// <param name="mBaudRate">波特率</param>
        /// <param name="mdes">描述</param>
        public test7631(string mPortName, int mBaudRate, string mdes)
        {
            MyPortName = mPortName;
            MyBaudRate = mBaudRate;
            mdescrible = mdes;
          //  connect();
        }
        /// <summary>
        /// 连接com 或重新连接COM
        /// </summary>
        /// <returns></returns>
        public EmRes connect()
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
                ssa = new PaperC();
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
                    string a = serialPort.ReadExisting();
                    if (a.Contains("OK") || a.Contains("0,No Error"))
                    {
                        serialPort.Write("MEAS?\r\n");
                        return;
                    }
                    ReceiveData8740 = a;
                    ReceiveData8740.Replace("\r\n", "");
                        //ssa.Name = ReceiveData8740;
                        //chuan?.Invoke(this, ssa);

                }
                catch (Exception ex)
                {
                    Logger.Error($"{mdescrible}串口接收信息失败：{ex.Message}");
                    return;
                }
            }
        }
        /// <summary>
        /// 发送数据，*OPT?是否就绪
        /// </summary>
        /// <param name="mess">:STAR开始，:STOP停止，:RESU?请求结果，*OPC?询问完成没有？</param>
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
            else
            {
                Logger.Error($"{mdescrible}串口没有开打");
                return EmRes.Error;
            }
        }
        /// <summary>
        /// 启动9803耐压仪测试
        /// </summary>
        /// <param name="mess">要发送的指令</param>
        /// <param name="revdata">返回的耐压数据</param>
        /// <returns></returns>
        public EmRes StartTest9803(string mess, ref string revdata)
        {
            string mes = "";
            if (mess == "IR") mes = "MANU:STEP 10";//绝缘测试 MAIN:FUNC MANU
            else if(mess == "ACW") mes = "MANU:STEP 11";//耐压测试

            if (sends("MAIN:FUNC MANU\r\n") != EmRes.Succeed) return EmRes.Error;//手动                  
            if (sends(mes) != EmRes.Succeed) return EmRes.Error;//设置测试step
           
            if (sends("FUNC:TEST ON\r\n") != EmRes.Succeed) return EmRes.Error;//启动测试
            Thread.Sleep(100);
            int a = 0;
            while (ReceiveData8740.Length < 1)
            {
                Thread.Sleep(50); a++;
                if (ReceiveData8740.Length > 1) break;
                if (a > 100) return EmRes.Error;
            }
            revdata = ReceiveData8740;
            return EmRes.Succeed;
        }
        /// <summary>
        /// 9803运行前检查---待验证
        /// </summary>
        /// <returns></returns>
        public EmRes Sendinit9803(string filename)
        {
            
            if (sends("MAIN:FUNC MANU\r\n") != EmRes.Succeed) return EmRes.Error;
            if(sends("TEST:RET ON\r\n") != EmRes.Succeed) return EmRes.Error;//当测试已停止（通过/失败或停止）时，允许在远程 终端上显示“OK”。
            Thread.Sleep(100);
            int a = 0;
            while (ReceiveData8740.Length < 1)
            {
                Thread.Sleep(50); a++;
                if (ReceiveData8740.Length > 1) break;
                if (a > 100) return EmRes.Error;
            }
            if(filename!= ReceiveData8740) return EmRes.Error;


            return EmRes.Succeed;

        }



        public event EventHandler<PaperC> chuan;
        PaperC ssa;     
        public class PaperC : EventArgs
        {
            public string Name { get; set; }                    //用于存储数据，当事件被调用时，可利用其进行传递数据。
        }









    }
}
