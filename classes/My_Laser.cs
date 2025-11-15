using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPro001.classes
{
    /// <summary>
    /// 激光打标
    /// </summary>
  public  class My_Laser
    {
        NetWorkHelper.TCP.ITcpClient my_client;
        public string data1;
        public bool isCon = false;
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="m"></param>
        public void set(NetWorkHelper.TCP.ITcpClient m)
        {
            my_client = m;
        }

        /// <summary>
        /// 连接
        /// </summary>
        public void Conn()
        {
            if (my_client != null)
                my_client.StartConnect();
        }
        /// <summary>
        /// 断开连接
        /// </summary>
        public void Close()
        {
            my_client.StopConnect();
        }

        /// <summary>
        /// 设置连接状态
        /// </summary>
        /// <param name="sts"></param>
        public void Set_Sts(bool sts)
        {
            isCon = sts;
        }
        /// <summary>
        /// 获取连接状态
        /// </summary>
        /// <returns></returns>
        public bool Get_Sts()
        {
            return isCon;
        }

        /// <summary>
        /// 加载模板
        /// </summary>
        public void LoadFile(string s)
        {
            string str = "11;" + s;
            byte[] buf = System.Text.Encoding.Default.GetBytes(str);
            if (my_client != null)
                my_client.SendData(buf);
            
        }
        /// <summary>
        /// 启动打印
        /// </summary>
        public void Start()
        {
            string str = "15;12";
            byte[] buf = System.Text.Encoding.Default.GetBytes(str);
            if (my_client != null)
                my_client.SendData(buf);
        }

        //设置变量
        public void SetValue(string str1,string str2)
        {
            string cmd = "13;V1," + str1 + ";V2," + str2;
            byte[] buf = System.Text.Encoding.Default.GetBytes(cmd);
            if (my_client != null)
                my_client.SendData(buf);
        }
        /// <summary>
        /// 获取反馈
        /// </summary>
        /// <param name="data"></param>
        public void onReceve(byte[] data)
        {
             data1 = System.Text.Encoding.UTF8.GetString(data);
            string[] dataArray = data1.Split('\r', '\n');
            string[] var = dataArray[0].Split(',');
        }
    }
}
