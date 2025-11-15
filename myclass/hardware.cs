using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CXPro001.classes;
using CXPro001.gl;
using HslCommunication;
using HslCommunication.Profinet.Siemens;
using Modbus.Device;
namespace CXPro001.myclass
{
    //所有外设的硬件初始化
    public static class hardware
    {
        /// <summary>
        /// 基恩士PLC
        /// </summary>
       // static public PLC_jienshi PLC_KEY = new PLC_jienshi("192.168.0.10", 8501, " ", 6000, "基恩士PLC");
        /// <summary>
        /// 大族激光刻字机
        /// </summary>
       // static public Hanslaser hanslaser1 = new Hanslaser("192.168.0.150", 9600, "大族激光刻字机",true);
        /// <summary>
        /// 耐压仪7631
        /// </summary>
      //  static public test7631 Tes7631 = new test7631("COM2", 9600, "耐压仪7631");
        /// <summary>
        /// 西门子PLC1200
        /// </summary>
      //  static public s7com s7_1200 = new s7com("192.168.0.10", 102, "西门子S7-1200");
        /// <summary>
        /// 得利捷扫码枪
        /// </summary>
      //  static public barcode_delijie delijie1 = new barcode_delijie("127.0.0.1", 8521, "", 6002, "得利捷扫码枪", "得利捷");
        /// <summary>
        /// 基恩士扫码枪
        /// </summary>
  //      static public barcode_jienshi jienshi1 = new barcode_jienshi("192.168.0.201", 8511, "", 6003, "基恩士扫码枪", "基恩士");
        /// <summary>
        /// 字符检测相机
        /// </summary>
       // static public visioncam viscam = new visioncam("127.0.0.1", 8080, "", 6001, "字符检测相机");
        ///// <summary>
        ///// 位置度相机
        ///// </summary>
        //static public visioncam viscam1 = new visioncam("127.0.0.1", 8080, "", 6002, "位置度相机");
        ///// <summary>
        ///// 字符检测相机
        ///// </summary>
        //static public visioncam viscam2 = new visioncam("127.0.0.1", 8080, "", 6003, "字符检测相机");
        /// <summary>
        /// 8740耐压仪
        /// </summary>
        public static My_8740 m_8740 = new My_8740();//8740
        public static My_io my_io = new My_io();  //IO
        public static My_Motion my_motion = new My_Motion();//电机控制
        public static SiemensS7Net plc = new SiemensS7Net(SiemensPLCS.S1200, "192.168.0.10");//西门子
        public static My_XR_100 my_cxr1 = new My_XR_100();//读码器
        public static My_XR_100 my_cxr2 = new My_XR_100();//读码器
        public static My_Laser my_laser = new My_Laser();//激光
        public static FlowMeter my_meter = new FlowMeter(); 
        public static SerialPort flowMeterPort = new SerialPort("COM11", 9600, Parity.None, 8, StopBits.One);

        public static ModbusSerialMaster _modbusSerialMaster;
        public static My_CCD my_ccd1 = new My_CCD();//位置
        public static My_CCD my_ccd2 = new My_CCD();//高度
        public static My_CCD my_ccd3 = new My_CCD();//位置
        public static My_CCD my_ccd4 = new My_CCD();//高度

        public static bool plc_isCon=false;//plc 是否链接
        public static bool Plc_Islive = false;//PLC心跳是正常

        public static My_STCords my_cord = new My_STCords();//生成二维码
        public static My_Take my_takes = new My_Take();

        /// <summary>
        /// 硬件初始化
        /// </summary>
        public static bool Init(int flag,NetWorkHelper.TCP.ITcpClient c1,NetWorkHelper.TCP.ITcpClient c2, NetWorkHelper.TCP.ITcpClient c3, NetWorkHelper.TCP.ITcpClient c4, NetWorkHelper.TCP.ITcpClient c5, NetWorkHelper.TCP.ITcpClient c6, NetWorkHelper.TCP.ITcpClient c7)
        {
            int sum = 0;
            if (flag == 1)
            {
                if (my_motion.Open() == 0)
                    Logger.Info($"打开卡成功！");//写入日志.
                else
                {
                    
                    Logger.Error($"打开失败！");
                    return false;
                }
            }
            if (flag == 2)
            {
                 sum = my_motion.Init_Mod();
                if (sum > 0)
                    Logger.Info($"模块连接成功数量:{sum.ToString()}");//写入日志.
                else
                {
                    Logger.Error($"模块连接失败:{sum.ToString()}");//写入日志.
                    return false; 
                }
                my_io.Set(sum);
            }
            if (flag == 3)
            {
                if (my_motion.LoadEnCatFile() == 0)
                    Logger.Info($"载入电机配置文件成功！");//写入日志.
                else
                {
                    Logger.Error($"载入电机配置文件失败！");//写入日志.
                    return false;
                }
            }
            
            if (flag == 15)
            {
                sum = my_motion.StartEcat();
                if (sum > 0)
                    Logger.Info($"电机在线数量:{sum.ToString()}");//写入日志.
                else
                {
                    Logger.Error($"电机通讯失败！ ");//写入日志.
                    return false;
                }
            }
            if (flag == 16)
            {
                //TCP 初始化
                my_cxr1.set(c1);//扫码枪1
                my_cxr2.set(c2);//扫码枪2
                my_laser.set(c3);

                my_ccd1.set(c4,1);//设置句柄
                my_ccd2.set(c5,2);//句柄
                my_ccd3.set(c6,3);
                my_ccd4.set(c7,4);
           
                my_cxr1.Conn();
                my_cxr2.Conn();
                my_laser.Conn();
                my_ccd1.Conn();
                my_ccd2.Conn();
                my_ccd3.Conn();
                my_ccd4.Conn();
            }
            ///////////////////////////////

            if (flag == 18)
            {
                //位置统一
                hardware.my_motion.En(1);
                hardware.my_motion.En(2);
                hardware.my_motion.En(3);
                hardware.my_motion.En(4);
                hardware.my_motion.En(5);
                hardware.my_motion.En(6);
            }
            //设置限位
            if (flag == 19)
            {
                my_motion.Set_SoftLimt(1, SysStatus.Axis_Soft_Limt_Z[0], SysStatus.Axis_Soft_Limt_F[0]);
                my_motion.Set_SoftLimt(2, SysStatus.Axis_Soft_Limt_Z[1], SysStatus.Axis_Soft_Limt_F[1]);
                my_motion.Set_SoftLimt(3, SysStatus.Axis_Soft_Limt_Z[2], SysStatus.Axis_Soft_Limt_F[2]);
                my_motion.Set_SoftLimt(4, SysStatus.Axis_Soft_Limt_Z[3], SysStatus.Axis_Soft_Limt_F[3]);
                my_motion.Set_SoftLimt(5, SysStatus.Axis_Soft_Limt_Z[4], SysStatus.Axis_Soft_Limt_F[4]);
                my_motion.Set_SoftLimt(6, SysStatus.Axis_Soft_Limt_Z[5], SysStatus.Axis_Soft_Limt_F[5]);
            }
            if (flag == 20)
            {
                    var result = plc.ConnectServer();
                     if (result.IsSuccess)
                     {
              
                        Logger.Info($"PLC通信成功");//写入日志.
                       plc_isCon = true;
                         Plc_Islive = true;
                     }
                     else
                      {
                       Logger.Error($"PLC通信失败！ ");//写入日志.
                        plc_isCon = false;
                    Plc_Islive = false;
                }

            }
            if(flag==21)
            {
                my_cord.LoadSTcord();//读取
                Logger.Info($"站点二维码载入！ ");//写入日志.
                m_8740.init();
                m_8740.connect();
                flowMeterPort.Open();
                _modbusSerialMaster = ModbusSerialMaster.CreateRtu(hardware.flowMeterPort);
                _modbusSerialMaster.Transport.ReadTimeout = 2000;
                my_meter.init();
                Logger.Info($"硬件初始化结束！ ");//写入日志.
            }

            return true;
            
        }
    }
}
