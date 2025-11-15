using MyLib.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

using CXPro001.ShowControl;
using System.IO;
using MyLib.Files;
using MyLib.SqlHelper;
using System.Data.SqlClient;
using MyLib.OldCtr;
using System.Data;
using CXPro001.controls;
using CXPro001.setups;
using CXPro001.classes;
using CXPro001.myclass;
using CXPro001.forms;
using Modbus.Device;

namespace CXPro001.myclass
{
    /// <summary>
    /// 多个工位同步作业类--例如转盘
    /// </summary>
    public class RunMany
    {


        #region 线程定义       
        Task task_1 = null;
        Task task_2 = null;
        Task task_3 = null;
        Task task_4 = null;
        Task task_5 = null;
        Task task_6 = null;
        Task task_7 = null;
        Task task_8 = null;
        Task task_9 = null;
        Task task_10 = null;
        Task task_11 = null;
        Task task_12 = null;
        #endregion


        #region  流程控制
        public struct TH
        {
            /// <summary>
            /// 流程
            /// </summary>
            public int Step;
            /// <summary>
            /// 流程备注
            /// </summary>
            public string Step_str;
            /// <summary>
            /// 超时时间
            /// </summary>
            public int Time;
        }
        private TH m_TH1;//取料
        private TH m_TH2;//横移
        private TH m_TH3;//耐压
        private TH m_TH4;//侧面
        private TH m_TH5;//顶部
        private TH m_TH6; //激光
        private TH m_TH7;//下料
        private TH m_TH8;//手工
        private TH m_TH9;//手工
        private TH m_TH10;//手工

        private TH m_TH12;//清料
        /// <summary>
        /// 线程状态  0 未开起  1运行 2 暂停 3 ERROR
        /// </summary>
        public int TH_Sts = 0;
        /// <summary>
        /// 自动1号  手动2号
        /// </summary>
        private int TH_Product_Num = 1;

        public int Clean_auto = 0;
        
        /// <summary>
        /// 线程同步锁对象
        /// </summary>
        private readonly object _lockObject = new object();
        
        /// <summary>
        /// 取料线程数据状态标志
        /// </summary>
        private bool _dataReady = false;
        
        /// <summary>
        /// 取料线程数据
        /// </summary>
        private struct PickupData
        {
            public bool get_1;
            public bool get_2;
            public int mode1_1;
            public int mode1_2;
            public int mode2_1;
            public int mode2_2;
            public bool dataValid;
            public DateTime timestamp; // 添加时间戳
        }
        
        /// <summary>
        /// 取料数据队列，支持多次取料数据存储
        /// </summary>
        private Queue<PickupData> _pickupDataQueue = new Queue<PickupData>();
        
        /// <summary>
        /// 取料线程是否正在运行
        /// </summary>
        private bool _pickupThreadRunning = false;
        #endregion

        /// <summary>
        /// 设置取料数据（线程安全，支持队列存储）
        /// </summary>
        private void SetPickupData(bool get_1, bool get_2, int mode1_1, int mode1_2, int mode2_1, int mode2_2)
        {
            lock (_lockObject)
            {
                PickupData newData = new PickupData
                {
                    get_1 = get_1,
                    get_2 = get_2,
                    mode1_1 = mode1_1,
                    mode1_2 = mode1_2,
                    mode2_1 = mode2_1,
                    mode2_2 = mode2_2,
                    dataValid = true,
                    timestamp = DateTime.Now
                };
                
                // 添加到队列中，支持多次取料
                _pickupDataQueue.Enqueue(newData);
                _dataReady = true;
            }
        }

        /// <summary>
        /// 获取取料数据（线程安全，从队列中获取）
        /// </summary>
        private bool GetPickupData(out bool get_1, out bool get_2, out int mode1_1, out int mode1_2, out int mode2_1, out int mode2_2)
        {
            lock (_lockObject)
            {
                if (_pickupDataQueue.Count > 0)
                {
                    PickupData data = _pickupDataQueue.Dequeue();
                    get_1 = data.get_1;
                    get_2 = data.get_2;
                    mode1_1 = data.mode1_1;
                    mode1_2 = data.mode1_2;
                    mode2_1 = data.mode2_1;
                    mode2_2 = data.mode2_2;
                    
                    // 如果队列为空，设置数据未就绪标志
                    if (_pickupDataQueue.Count == 0)
                        _dataReady = false;
                    
                    return true;
                }
                else
                {
                    get_1 = get_2 = false;
                    mode1_1 = mode1_2 = mode2_1 = mode2_2 = 1;
                    return false;
                }
            }
        }

        /// <summary>
        /// 清除取料数据（线程安全）
        /// </summary>
        private void ClearPickupData()
        {
            lock (_lockObject)
            {
                _pickupDataQueue.Clear();
                _dataReady = false;
            }
        }

        /// <summary>
        /// 获取队列中数据数量（线程安全）
        /// </summary>
        private int GetQueueCount()
        {
            lock (_lockObject)
            {
                return _pickupDataQueue.Count;
            }
        }

        /// <summary>
        /// 检查取料线程是否正在运行
        /// </summary>
        private bool IsPickupThreadRunning()
        {
            return _pickupThreadRunning;
        }




        public void Set_Error()
        {
            TH_Sts = 3;
            Stop_TH();
        }

        public void Clear_Error()
        {
            TH_Sts = 0;
        }
        /// <summary>
        /// 初始化线程标志位
        /// </summary>
        public void Run_Init(int flag)
        {

            m_TH1.Step = 0;//自动取料
            m_TH2.Step = 0;//横移
            m_TH3.Step = 0;//耐压
            m_TH4.Step = 0;//侧ccd
            m_TH5.Step = 0;//顶ccd
            m_TH6.Step = 0;//打码
            m_TH7.Step = 0;//下料
            m_TH8.Step = 0;//人工放料
            m_TH9.Step = 0;//人工放料
            if (flag == 1)
                m_TH1.Step = 1;//自动取料
            if (flag == 2)
                m_TH8.Step = 1;//自动取料
            if (flag == 3)
                m_TH12.Step = 1;//自动取料

        }
        /// <summary>
        /// 设置线程标志位  测试用
        /// </summary>
        /// <param name="th"></param>
        /// <param name="val"></param>
        public void Set_TH_Flag(int th, int val)
        {
            if (th == 1)
                m_TH1.Step = val;
            if (th == 2)
                m_TH2.Step = val;
            if (th == 3)
                m_TH3.Step = val;
            if (th == 4)
                m_TH4.Step = val;
            if (th == 5)
                m_TH5.Step = val;
            if (th == 6)
                m_TH6.Step = val;
            if (th == 7)
                m_TH7.Step = val;
            if (th == 9)
                m_TH9.Step = val;
        }
        /// <summary>
        /// 反馈线程流程步
        /// </summary>
        /// <param name="th"></param>
        /// <returns></returns>
        public string Get_TH_Flag(int th)
        {
            string re = "";
            if (th == 1)
                re = m_TH1.Step + m_TH1.Step_str;
            if (th == 2)
                re = m_TH2.Step + m_TH2.Step_str;
            if (th == 3)
                re = m_TH3.Step + m_TH3.Step_str;
            if (th == 4)
                re = m_TH4.Step + m_TH4.Step_str;
            if (th == 5)
                re = m_TH5.Step + m_TH5.Step_str;
            if (th == 6)
                re = m_TH6.Step + m_TH6.Step_str;
            if (th == 7)
                re = m_TH7.Step + m_TH7.Step_str;
            if (th == 8)
                re = m_TH8.Step + m_TH8.Step_str;
            if (th == 9)
                re = m_TH9.Step + m_TH9.Step_str;
            if (th == 12)
                re = m_TH12.Step + m_TH12.Step_str;
            return re;

        }

        /// <summary>
        /// 运行
        /// </summary>       
        public void RunNew()
        {
            InitStar();//变量标志位                    
            if (SysStatus.CurProductName == "M334")
                TH_Product_Num = 1;
            else
                TH_Product_Num = 2;

            if (SysStatus.Modle == 1)//人工
            {
                Run_Init(2);//流程标志位
                run_th("23456789AB");

            }
            if (SysStatus.Modle == 2)//自动
            {
                Run_Init(1);//流程标志位
                run_th("12345679AB");//自动

            }

            TH_Sts = 1;
        }

        /// <summary>
        /// 创建线程
        /// </summary>
        /// <param name="TaskSlect">不同功能不同号</param>
        private void run_th(string TaskSlect)
        {
            //1号模组取料
            if (TaskSlect.Contains("1"))
            {
                if (task_1 == null || task_1 != null && task_1.IsCompleted)
                {
                    Logger.Write(Logger.InfoType.Info, "1号取料线程");
                    if (task_1 != null) task_1.Dispose();
                    task_1 = new Task(Run_NO1);
                    task_1.Start();
                }
            }
            //2号横移线程
            if (TaskSlect.Contains("2"))
            {
                if (task_2 == null || task_2 != null && task_2.IsCompleted)
                {
                    Logger.Write(Logger.InfoType.Info, "2号横移放料线程");
                    if (task_2 != null) task_2.Dispose();
                    task_2 = new Task(Run_NO2);
                    task_2.Start();
                }
            }
            //耐压
            if (TaskSlect.Contains("3"))
            {
                if (task_3 == null || task_3 != null && task_3.IsCompleted)
                {
                    Logger.Write(Logger.InfoType.Info, "耐压检测线程");
                    if (task_3 != null) task_3.Dispose();
                    task_3 = new Task(Run_NO3);
                    task_3.Start();
                }
            }
            //侧面
            if (TaskSlect.Contains("4"))
            {
                if (task_4 == null || task_4 != null && task_4.IsCompleted)
                {
                    Logger.Write(Logger.InfoType.Info, "创建高度检测运行线程");
                    if (task_4 != null) task_4.Dispose();
                    task_4 = new Task(Run_NO4);
                    task_4.Start();
                }
            }
            //顶部
            if (TaskSlect.Contains("5"))
            {
                if (task_5 == null || task_5 != null && task_5.IsCompleted)
                {
                    Logger.Write(Logger.InfoType.Info, "创建位置度检测运行线程");
                    if (task_5 != null) task_5.Dispose();
                    task_5 = new Task(Run_NO5);
                    task_5.Start();
                }
            }
            //激光
            if (TaskSlect.Contains("6"))
            {
                if (task_6 == null || task_6 != null && task_6.IsCompleted)
                {
                    Logger.Write(Logger.InfoType.Info, "创建扫打扫运行线程");
                    if (task_6 != null) task_6.Dispose();
                    task_6 = new Task(Run_NO6);
                    task_6.Start();
                }
            }
            //下料
            if (TaskSlect.Contains("7"))
            {
                if (task_7 == null || task_7 != null && task_7.IsCompleted)
                {
                    Logger.Write(Logger.InfoType.Info, "创建下料运行线程");
                    if (task_7 != null) task_7.Dispose();
                    task_7 = new Task(Run_NO7);
                    task_7.Start();
                }
            }
            //手工上料
            if (TaskSlect.Contains("8"))
            {
                if (task_8 == null || task_8 != null && task_8.IsCompleted)
                {
                    Logger.Write(Logger.InfoType.Info, "创建人工放料的线程");
                    if (task_8 != null) task_8.Dispose();
                    task_8 = new Task(Run_N08);
                    task_8.Start();
                }
            }
            //气压
            if (TaskSlect.Contains("9"))
            {
                if (task_9 == null || task_9 != null && task_9.IsCompleted)
                {
                    Logger.Write(Logger.InfoType.Info, "创建气压监测线程 ");
                    if (task_9 != null) task_9.Dispose();
                    task_9 = new Task(Run_N09);
                    task_9.Start();
                }
            }

            //外部监听
            if (TaskSlect.Contains("A"))
            {
                if (task_10 == null || task_10 != null && task_10.IsCompleted)
                {
                    Logger.Write(Logger.InfoType.Info, "创建外设IO监听线程");
                    if (task_10 != null) task_10.Dispose();
                    task_10 = new Task(Run_NO10);
                    task_10.Start();
                }
            }
            //超时监听
            if (TaskSlect.Contains("B"))
            {
                if (task_11 == null || task_11 != null && task_11.IsCompleted)
                {
                    Logger.Write(Logger.InfoType.Info, "创建超时线程");
                    if (task_11 != null) task_11.Dispose();
                    task_11 = new Task(Run_NO11);
                    task_11.Start();
                }
            }
            //超时监听
            if (TaskSlect.Contains("C"))
            {
                if (task_12 == null || task_12 != null && task_12.IsCompleted)
                {
                    Logger.Write(Logger.InfoType.Info, "创建清料线程");
                    if (task_12 != null) task_12.Dispose();
                    task_12 = new Task(Run_NO12);
                    task_12.Start();
                }
            }
        }
        /// <summary>
        /// 恢复线程
        /// </summary>
        public void Rest_TH()
        {
            Station[1] = true;
            Station[2] = true;
            Station[3] = true;
            Station[4] = true;
            Station[5] = true;
            Station[6] = true;
            Station[7] = true;
            Station[8] = true;
            Station[9] = true;
            Station[10] = true;
            Station[11] = true;
            Station[12] = true;

            reback[1] = true;
            reback[2] = true;
            reback[3] = true;
            reback[4] = true;
            reback[5] = true;
            reback[6] = true;
            reback[7] = true;
            reback[8] = true;

            m_TH1.Time = 0;
            m_TH2.Time = 0;
            m_TH3.Time = 0;
            m_TH4.Time = 0;
            m_TH5.Time = 0;
            m_TH6.Time = 0;
            m_TH7.Time = 0;



            TH_Sts = 1;

            Logger.Write(Logger.InfoType.Info, "恢复线程");
        }

        /// <summary>
        /// 停止线程运行
        /// </summary>
        public void Stop_TH()
        {
            Station[1] = false;
            Station[2] = false;
            Station[3] = false;
            Station[4] = false;
            Station[5] = false;
            Station[6] = false;
            Station[7] = false;
            Station[8] = false;
            Station[9] = false;
            Station[10] = false;
            Station[11] = false;
            Station[12] = false;
            hardware.my_motion.Stop();//电机停止

            TH_Sts = 2;
        }

        public void Clean_Th()
        {
            Station[1] = false;
            Station[2] = false;
            Station[3] = false;
            Station[4] = false;
            Station[5] = false;
            Station[6] = false;
            Station[7] = false;
            Station[8] = false;
            Station[10] = false;
            Station[11] = false;
            Station[12] = false;
            hardware.my_motion.Stop();//电机停止

            TH_Sts = 0;
        }
        #region 线程     

        /// <summary>
        ///t1--qu
        /// </summary>
        private void Run_NO1()
        {
            String str;

            bool get_1 = true, get_2 = true;
            ////
            bool p1_1 = false, p1_2 = false, p2_1 = false, p2_2 = false;
            int mode1_1 = 1, mode1_2 = 1, mode2_1 = 1, mode2_2 = 1;
            bool UP = true, DOWN;
            int heart_1 = 0, heart_2 = 0;
            
            // 设置取料线程运行标志
            _pickupThreadRunning = true;

            while (SysStatus.Status == SysStatus.EmSysSta.Run)//运行状态
            {

                double TM1_ = Environment.TickCount;//读取系统时间

                if ((Station[1] && !SysStatus.Shield_NO1) || SysStatus.NO1)//自动或手动
                {
                    switch (m_TH1.Step)
                    {
                        case 1://检测心跳
                            m_TH1.Step_str = "检测心跳是否正常";
                            m_TH1.Time = 0;
                            str = "DB100.0.0";//判断1-1
                            heart_1 = hardware.plc.ReadInt16(str).Content;  // 读取M100.0是否通断

                            m_TH1.Step++;
                            break;
                        case 2:
                            m_TH1.Step_str = "延时";
                            Thread.Sleep(500);
                            m_TH1.Step++;
                            break;
                        case 3:
                            str = "DB100.0.0";//判断1-1
                            heart_2 = hardware.plc.ReadInt16(str).Content;  // 读取M100.0是否通断
                            if (heart_1 == heart_2)//心跳失败                         
                                m_TH1.Step = 1;
                            else
                                m_TH1.Step++;
                            break;
                        ///////////////////////////////////////////////////////
                        case 4://打开气爪

                            str = "DB100.2.2";//不允许放料
                            hardware.plc.Write(str, false);

                            m_TH1.Step_str = "打开气爪";
                            hardware.my_io.Set_Do(0, 12, 0);
                            hardware.my_io.Set_Do(0, 13, 1);//
                            m_TH1.Step++;
                            break;
                        case 5://等待感应器到位
                            m_TH1.Step_str = "等待感应器到位";
                            if (hardware.my_io.m_input[0, 13] && hardware.my_io.m_input[0, 15])
                                m_TH1.Step++;
                            break;
                        case 6:// 关闭下降气缸
                            m_TH1.Step_str = "关闭下降气缸";
                            hardware.my_io.Set_Do(0, 8, 1);//升降关
                            hardware.my_io.Set_Do(0, 9, 0);
                            m_TH1.Step++;
                            break;
                        case 7://等待感应器到位
                            m_TH1.Step_str = "等待下降气缸关闭";
                            if (hardware.my_io.m_input[0, 8])
                                m_TH1.Step++;
                            break;
                        case 8://关闭
                            m_TH1.Step_str = "关闭旋转";
                            hardware.my_io.Set_Do(0, 10, 1);//旋转关掉
                            hardware.my_io.Set_Do(0, 11, 0);//旋转关掉

                            hardware.my_io.Set_Do(1, 0, 1);//放料台
                            hardware.my_io.Set_Do(1, 1, 0);//关闭收料 
                            m_TH1.Step++;
                            break;
                        case 9://感应器到位
                            m_TH1.Step_str = "等待旋转感应器到位";
                            if (hardware.my_io.m_input[0, 10] && hardware.my_io.m_input[1, 1])
                                m_TH1.Step++;
                            break;
                        case 10:
                            m_TH1.Step_str = "关闭加长气缸";
                            hardware.my_io.Set_Do(0, 14, 1);// 
                            hardware.my_io.Set_Do(0, 15, 0);// 
                            m_TH1.Step++;
                            break;
                        case 11:
                            m_TH1.Step_str = "等待加长气缸及放料台";
                            if (hardware.my_io.m_input[0, 12] && hardware.my_io.m_input[1, 1])
                                m_TH1.Step++;
                            break;
                        case 12://移动电机
                            m_TH1.Step_str = "电机移动到等待位";
                            hardware.my_motion.Move(SysStatus.Axis_1, 0);
                            m_TH1.Step++;
                            break;
                        case 13://等待电机到位
                            m_TH1.Step_str = "等待电机到等待位";
                            if (hardware.my_motion.Move_Finsh(SysStatus.Axis_1))
                            {
                                if (hardware.my_io.m_input[1, 2] && hardware.my_io.m_input[1, 3])//如果产品有料启动横移
                                {

                                    m_TH1.Step_str = "料台有料等待横移";
                                    if (SysStatus.Shield_NO9)
                                    {
                                        if (m_TH2.Step == 0)//横移
                                        {
                                            m_TH2.Step = 1;
                                        }
                                    }
                                    //else
                                    //{
                                    //    if (m_TH9.Step == 0)//启动气压
                                    //    {
                                            
                                    //        m_TH9.Step = 1;
                                    //    }
                                    //}

                                }

                                /////////////////清料模式////////////////////////////
                                if (Clean_auto > 0)
                                {

                               
                                    if (SysStatus.Shield_NO9)
                                    {
                                        if (m_TH2.Step == 0)//横移
                                        {
                                            m_TH2.Step = 1;
                                            Clean_auto--;
                                        }
                                    }
                                    else
                                    {
                                        if (m_TH9.Step == 0)//启动气压
                                        {
                                            m_TH9.Step = 1;
                                            Clean_auto--;
                                        }
                                    }

                                }
                                ////////////////////////////////////////
                                m_TH1.Step++;
                            }
                            else
                            {
                                if (reback[1])//中断恢复
                                    hardware.my_motion.Move_Pro(SysStatus.Axis_1);
                                reback[1] = false;
                            }
                            break;
                        case 14:
                            m_TH1.Step_str = "判断是否取";
                            str = "DB100.3.0";//判断1-1
                            UP = hardware.plc.ReadBool(str).Content;  // 上板
                            str = "DB100.3.5";//判断1-1
                            DOWN = hardware.plc.ReadBool(str).Content;  // 下板
                            p1_1 = false;
                            p1_2 = false;
                            p2_1 = false;
                            p2_2 = false;
                            if (UP)
                            {
                                str = "DB100.2.4";//判断1-1
                                p1_1 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
                                str = "DB100.2.5";//判断1-1
                                p1_2 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断

                                str = "DB100.2.6";//判断1-1
                                p2_1 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
                                str = "DB100.2.7";//判断1-1
                                p2_2 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
                            }
                            if (DOWN)
                            {
                                str = "DB100.3.1";//判断1-1
                                p1_1 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
                                str = "DB100.3.2";//判断1-1
                                p1_2 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断

                                str = "DB100.3.3";//判断1-1
                                p2_1 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
                                str = "DB100.3.4";//判断1-1
                                p2_2 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
                            }

                            if (p1_1 == false && p1_2 == false && p2_1 == false && p2_2 == false)
                            {
                                str = "DB100.2.2";//允许上料
                                hardware.plc.Write(str, true);

                                str = "DB100.2.3";//完成取料
                                hardware.plc.Write(str, true);
                            }
                            else
                            {
                                str = "DB100.2.2";// 禁止上料
                                hardware.plc.Write(str, false);
                                str = "DB100.2.3";//未完成取料
                                hardware.plc.Write(str, false);
                            }

                            m_TH1.Step++;
                            break;


                        case 15://等待PLC取料信号                      
                            m_TH1.Step_str = "等待取料信号";
                            str = "DB100.2.1";
                            bool M100_7 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
                            if (M100_7)
                            {
                                str = "DB100.2.3";//禁止上料
                                hardware.plc.Write(str, false);
                                m_TH1.Step++;

                            }
                            else
                                m_TH1.Step = 13;
                            break;
                        case 16:
                            m_TH1.Step_str = "进行取料";
                            str = "DB100.3.0";//判断1-1
                            UP = hardware.plc.ReadBool(str).Content;  // 上板
                            str = "DB100.3.5";//判断1-1
                            DOWN = hardware.plc.ReadBool(str).Content;  // 下板

                            if (UP)
                            {
                                str = "DB100.2.4";//判断1-1
                                p1_1 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
                                str = "DB100.2.5";//判断1-1
                                p1_2 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断

                                str = "DB100.2.6";//判断1-1
                                p2_1 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
                                str = "DB100.2.7";//判断1-1
                                p2_2 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
                            }
                            if (DOWN)
                            {
                                str = "DB100.3.1";//判断1-1
                                p1_1 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
                                str = "DB100.3.2";//判断1-1
                                p1_2 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断

                                str = "DB100.3.3";//判断1-1
                                p2_1 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
                                str = "DB100.3.4";//判断1-1
                                p2_2 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
                            }
                            //////////////////////磨具号/////////////////////////////////////
                            str = "DB100.4.0";//判断1-1
                            mode2_2 = hardware.plc.ReadInt16(str).Content;  // 读取M100.0是否通断
                            str = "DB100.6.0";//判断1-1
                            mode2_1 = hardware.plc.ReadInt16(str).Content;  // 读取M100.0是否通断

                            str = "DB100.8.0";//判断1-1
                            mode1_1 = hardware.plc.ReadInt16(str).Content;  // 读取M100.0是否通断
                            str = "DB100.10.0";//判断1-1
                            mode1_2 = hardware.plc.ReadInt16(str).Content;  // 读取M100.0是否通断

                            get_1 = false;
                            get_2 = false;

                            if (p2_1 || p2_2)
                            {
                                get_1 = true;
                                m_TH1.Step++;

                            }
                            else if (p1_1 || p1_2)
                            {
                                get_2 = true;

                                m_TH1.Step++;
                            }
                            else
                            {
                                m_TH1.Step = 13;
                            }
                            
                            // 设置共享数据，供其他线程使用
                            SetPickupData(get_1, get_2, mode1_1, mode1_2, mode2_1, mode2_2);

                            break;

                        case 17:

                            if (get_1)
                            {
                                m_TH1.Step_str = "移动到1号位";
                                if (UP)
                                    hardware.my_motion.Move(SysStatus.Axis_1, 1);
                                else
                                    hardware.my_motion.Move(SysStatus.Axis_1, 4);
                                m_TH1.Step++;
                            }
                            else if (get_2)
                            {
                                m_TH1.Step_str = "移动到2号位";
                                if (UP)
                                    hardware.my_motion.Move(SysStatus.Axis_1, 2);
                                else
                                    hardware.my_motion.Move(SysStatus.Axis_1, 5);
                                m_TH1.Step++;
                            }
                            else
                            {
                                m_TH1.Step--;
                            }


                            break;
                        case 18:
                            m_TH1.Step_str = "等待电机到位";

                            if (hardware.my_motion.Move_Finsh(SysStatus.Axis_1))
                                m_TH1.Step++;
                            else
                            {
                                if (hardware.my_motion.Check_Run(SysStatus.Axis_1) == false)
                                {
                                    if (reback[1])//中断恢复
                                        hardware.my_motion.Move_Pro(SysStatus.Axis_1);
                                    reback[1] = false;
                                }
                            }
                            break;
                        case 19:
                            m_TH1.Step_str = "打开加长";
                            hardware.my_io.Set_Do(0, 14, 0);//
                            hardware.my_io.Set_Do(0, 15, 1);//加长
                            m_TH1.Step++;
                            break;
                        case 20://判断是否到位
                            m_TH1.Step_str = "等待加长到位";
                            if (hardware.my_io.m_input[0, 14])
                                m_TH1.Step++;
                            break;
                        case 21://气缸下降
                            m_TH1.Step_str = "打开下降";
                            hardware.my_io.Set_Do(0, 8, 0);//
                            hardware.my_io.Set_Do(0, 9, 1);//升降开
                            m_TH1.Step++;
                            break;
                        case 22://判断是否到位
                            m_TH1.Step_str = "等待下降到位";
                            if (hardware.my_io.m_input[0, 9])
                                m_TH1.Step++;
                            break;
                        case 23:// 夹爪关闭
                            m_TH1.Step_str = "关闭气爪";
                            hardware.my_io.Set_Do(0, 12, 1);//气爪关掉
                            hardware.my_io.Set_Do(0, 13, 0);//气爪关掉
                            m_TH1.Step++;
                            break;
                        case 24://判断是否到位
                            m_TH1.Step_str = "气爪延时";
                            Thread.Sleep(500);
                            m_TH1.Step++;
                            break;
                        case 25://气缸上升
                            m_TH1.Step_str = "关闭下降";
                            hardware.my_io.Set_Do(0, 8, 1);//升降关
                            hardware.my_io.Set_Do(0, 9, 0);//升降关
                            m_TH1.Step++;
                            break;
                        case 26://判断是否到位
                            m_TH1.Step_str = "等待下降关闭到位";
                            if (hardware.my_io.m_input[0, 8])
                                m_TH1.Step++;
                            break;
                        case 27:
                            //判断是否需要旋转
                            if (get_1)  //1号位置需要旋转
                            {
                                m_TH1.Step_str = "等待旋转到位";
                                hardware.my_io.Set_Do(0, 10, 0);//旋转打开
                                hardware.my_io.Set_Do(0, 11, 1);//
                                if (hardware.my_io.m_input[0, 10])//等待旋转到位
                                {
                                    m_TH1.Step++;

                                }
                            }
                            else  //2号位置不需要旋转
                            {
                                m_TH1.Step++;
                            }

                            break;
                        case 28:
                            m_TH1.Step_str = "关闭加长";
                            hardware.my_io.Set_Do(0, 14, 1);//
                            hardware.my_io.Set_Do(0, 15, 0);//加长
                            m_TH1.Step++;
                            break;
                        case 29:
                            m_TH1.Step_str = "等待加长关闭到位";
                            if (hardware.my_io.m_input[0, 12])
                                m_TH1.Step++;
                            break;

                        case 30://电机到等待区  位置2
                            m_TH1.Step_str = "电机到达等待区";
                            hardware.my_motion.Move(SysStatus.Axis_1, 0);
                            m_TH1.Step++;

                            break;
                        case 31://判断电机是否到位
                            m_TH1.Step_str = "等待电机到达等待区";
                            if (hardware.my_motion.Move_Finsh(SysStatus.Axis_1))
                            {
                                if (hardware.my_io.m_input[1, 2] && hardware.my_io.m_input[1, 3])//如果产品有料启动横移
                                {
                                    m_TH1.Step_str = "料台有料等待横移";
                                    if (SysStatus.Shield_NO9)
                                    {
                                        if (m_TH2.Step == 0)//横移
                                        {
                                            m_TH2.Step = 1;
                                        }
                                    }
                                    //else
                                    //{
                                    //    if (m_TH9.Step == 0)//启动气压
                                    //    {
                                    //        m_TH9.Step = 1;
                                    //    }
                                    //}
                                }
                                else
                                {
                                    m_TH1.Step++;
                                }
                                m_TH9.Step = 1;
                            }
                            else
                            {
                                if (reback[1])//中断恢复
                                    hardware.my_motion.Move_Pro(SysStatus.Axis_1);
                                reback[1] = false;
                            }
                            break;
                        case 32:
                            str = "DB100.3.0";//判断1-1
                            UP = hardware.plc.ReadBool(str).Content;  // 上板
                            str = "DB100.3.5";//判断1-1
                            DOWN = hardware.plc.ReadBool(str).Content;  // 下板
                            p1_1 = false;
                            p1_2 = false;
                            p2_1 = false;
                            p2_2 = false;
                            if (UP)
                            {
                                str = "DB100.2.4";//判断1-1
                                p1_1 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
                                str = "DB100.2.5";//判断1-1
                                p1_2 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断

                                str = "DB100.2.6";//判断1-1
                                p2_1 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
                                str = "DB100.2.7";//判断1-1
                                p2_2 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
                            }
                            if (DOWN)
                            {
                                str = "DB100.3.1";//判断1-1
                                p1_1 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
                                str = "DB100.3.2";//判断1-1
                                p1_2 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断

                                str = "DB100.3.3";//判断1-1
                                p2_1 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
                                str = "DB100.3.4";//判断1-1
                                p2_2 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
                            }

                            if (p1_1 == false && p1_2 == false && p2_1 == false && p2_2 == false)
                            {
                                str = "DB100.2.2";//允许上料
                                hardware.plc.Write(str, true);

                                str = "DB100.2.3";//完成取料
                                hardware.plc.Write(str, true);
                            }
                            m_TH1.Step++;
                            break;

                        case 33://电机到放料区
                            m_TH1.Step_str = "等待横移结束及放料空";
                            if (hardware.my_io.m_input[1, 2] == false && hardware.my_io.m_input[1, 3] == false&&
                                hardware.my_io.m_input[3, 3]&& hardware.my_io.m_input[1, 6])
                            {
                                // 新码生成已移至气压线程，这里只执行电机移动
                                hardware.my_motion.Move(SysStatus.Axis_1, 3);
                                m_TH1.Step++;
                            }
                            break;
                        case 34://判断电机是否到位
                            m_TH1.Step_str = "等待电机到位";
                            if (hardware.my_motion.Move_Finsh(SysStatus.Axis_1))
                            {
                                m_TH1.Step++;
                            }
                            else
                            {
                                if (reback[1])//中断恢复
                                    hardware.my_motion.Move_Pro(SysStatus.Axis_1);
                                reback[1] = false;
                            }
                            break;

                        case 35://气缸下降
                            hardware.my_io.Set_Do(0, 8, 0);//
                            hardware.my_io.Set_Do(0, 9, 1);//
                            m_TH1.Step++;
                            break;
                        case 36://是否到位
                            m_TH1.Step_str = "等待下降到位";
                            if (hardware.my_io.m_input[0, 9])
                                m_TH1.Step = 1;
                            break;

                    }

                }

                Application.DoEvents();
                Thread.Sleep(60);
                TM2 = Environment.TickCount - TM1_;
            }
            
            // 清除取料线程运行标志
            _pickupThreadRunning = false;
            ClearPickupData();
            
            Logger.Error("模组取料停止");

        }

        /// <summary>
        /// t2--heng
        /// </summary>
        private void Run_NO2()
        {

            while (SysStatus.Status == SysStatus.EmSysSta.Run)//运行状态
            {
                double TM1_ = Environment.TickCount;//读取系统时间

                if ((Station[2] && !SysStatus.Shield_NO2) || SysStatus.NO2)//自动或手动
                {

                    //     SysStatus.NO2 = false;
                    if (SysStatus.NO2 && m_TH2.Step == 0)//测试模式
                        m_TH2.Step = 1;
                    switch (m_TH2.Step)
                    {
                        case 1://判断下料位置是否有料
                            m_TH2.Step_str = "下料台下料";
                            if (hardware.my_io.m_input[4, 16] == false && hardware.my_io.m_input[4, 17] == false)
                                m_TH2.Step++;
                            break;
                        case 2:
                            //判断工位是否都已经完成
                            m_TH2.Step++;
                            break;
                        case 3://打开爪子
                            m_TH2.Time = 0;
                            m_TH2.Step_str = "打开气爪";
                            hardware.my_io.Set_Do(3, 2, 0);//1
                            hardware.my_io.Set_Do(3, 3, 1);//1
                            hardware.my_io.Set_Do(3, 4, 0);//2
                            hardware.my_io.Set_Do(3, 5, 1);//2
                            hardware.my_io.Set_Do(3, 6, 0);//3
                            hardware.my_io.Set_Do(3, 7, 1);//3

                            m_TH2.Step++;
                            break;
                        case 4:// 等待到位
                            m_TH2.Step_str = "等待气爪打开到位信号";
                            if (hardware.my_io.m_input[3, 10] && hardware.my_io.m_input[3, 11] && hardware.my_io.m_input[3, 12] && hardware.my_io.m_input[3, 13] && hardware.my_io.m_input[3, 14]
                                && hardware.my_io.m_input[3, 15] && hardware.my_io.m_input[4, 0] && hardware.my_io.m_input[4, 1] && hardware.my_io.m_input[4, 2] && hardware.my_io.m_input[4, 3])
                                m_TH2.Step++;
                            break;
                        case 5: //上升
                            m_TH2.Step_str = "横移上升";
                            hardware.my_io.Set_Do(1, 2, 1);
                            hardware.my_io.Set_Do(1, 3, 0);
                            m_TH2.Step++;
                            break;
                        case 6://等待到位                    
                            m_TH2.Step_str = "等待横移上升到位";
                            if (hardware.my_io.m_input[1, 4])
                                m_TH2.Step++;
                            break;
                        case 7://
                            m_TH2.Step_str = "关闭后推";
                            hardware.my_io.Set_Do(1, 4, 1);//1  
                            hardware.my_io.Set_Do(1, 5, 0);//1                         

                            m_TH2.Step++;
                            break;

                        case 8:// 等待到位
                            m_TH2.Step_str = "等待后推关闭 ";
                            if (hardware.my_io.m_input[1, 6])
                                m_TH2.Step++;
                            break;

                        case 9://    电机移动位置1
                            if (SysStatus.CurProductName != "M381")
                            {
                                m_TH2.Step_str = "关闭旋转";
                                hardware.my_io.Set_Do(3, 1, 0);//1
                                hardware.my_io.Set_Do(3, 0, 1);//1  
                            }
                            hardware.my_motion.Move(SysStatus.Axis_2, 1);
                            m_TH2.Step++;
                            break;

                        case 10:
                            if (SysStatus.CurProductName != "M381")
                            {
                                m_TH2.Step_str = "等待旋转到位";
                                if (hardware.my_io.m_input[3, 8])
                                    m_TH2.Step++;
                            }
                            else
                                m_TH2.Step++;
                            break;
                        case 11://等待到电机气缸到位位
                            m_TH2.Step_str = "等待电机到1位";
                            if (hardware.my_motion.Move_Finsh(SysStatus.Axis_2))
                            {
                                m_TH2.Step++;
                            }
                            else
                            {
                                if (hardware.my_motion.Check_Run(SysStatus.Axis_2) == false)
                                {
                                    if (reback[2])//中断恢复
                                        hardware.my_motion.Move_Pro(SysStatus.Axis_2);
                                    reback[2] = false;
                                }
                            }

                            break;
                        case 12://等待其他线程完成 
                            if (m_TH3.Step == 0 && m_TH4.Step == 0 && m_TH5.Step == 0 && m_TH6.Step == 0)
                            {
                                if (SysStatus.CurProductName != "M381")
                                    hardware.my_cord.RotateST();//号码转移
                                else
                                    hardware.my_cord.RotateST2();//号码转移
                                hardware.my_cord.SaveSTcord();

                                m_TH2.Step++;

                            }
                           
                            break;
                        case 13:
                            if (hardware.my_io.m_input[1, 10] && hardware.my_io.m_input[1, 12] && hardware.my_io.m_input[2, 0])  //检测台下降气缸到位
                                m_TH2.Step = 17;
                            break;

                        case 17://打开后推


                            m_TH2.Step_str = "横移后推打开";
                            hardware.my_io.Set_Do(1, 5, 1);//1
                            hardware.my_io.Set_Do(1, 4, 0);//1  
                            m_TH2.Step++;
                            break;
                        case 18://等待到位
                            m_TH2.Step_str = "等待横移后推到位";
                            if (hardware.my_io.m_input[1, 7])
                                m_TH2.Step++;
                            break;
                        case 19://气缸下降
                            m_TH2.Step_str = "横移下降";
                            hardware.my_io.Set_Do(1, 2, 0);//1
                            hardware.my_io.Set_Do(1, 3, 1);//1     
                            m_TH2.Step++;
                            break;
                        case 20: //等待到位                           
                            if (hardware.my_io.m_input[1, 5])
                                m_TH2.Step++;
                            break;
                        case 21://关闭爪子
                            hardware.my_io.Set_Do(3, 2, 1);//1
                            hardware.my_io.Set_Do(3, 3, 0);//1
                            hardware.my_io.Set_Do(3, 4, 1);//2
                            hardware.my_io.Set_Do(3, 5, 0);//2
                            hardware.my_io.Set_Do(3, 6, 1);//3
                            hardware.my_io.Set_Do(3, 7, 0);//3
                            m_TH2.Step++;
                            break;
                        case 22: //等待到位
                            m_TH2.Step_str = "等待爪子关闭到位";
                            Thread.Sleep(500);

                            m_TH2.Step++;
                            break;

                        case 23://气缸下降关闭
                            m_TH2.Step_str = "气缸上升";
                            hardware.my_io.Set_Do(1, 2, 1);//1
                            hardware.my_io.Set_Do(1, 3, 0);//1     
                            m_TH2.Step++;
                            break;
                        case 24://等待到位
                            m_TH2.Step_str = "等待上升到位";
                            if (hardware.my_io.m_input[1, 4])
                                m_TH2.Step++;
                            break;
                        case 25:
                            m_TH2.Step_str = "判断工位是否还有料";
                            //         if (hardware.my_io.m_input[1, 2] || hardware.my_io.m_input[1, 3] || hardware.my_io.m_input[4, 8] || hardware.my_io.m_input[4, 9] || hardware.my_io.m_input[4, 10] || hardware.my_io.m_input[4, 11] || hardware.my_io.m_input[4, 12])
                            //         {
                            //           Stop_TH();
                            //          Logger.Error($"夹取失败");//写入日志.   
                            //       }
                            //     else
                            m_TH2.Step++;
                            break;
                        case 26://后推气缸关闭
                            m_TH2.Step_str = "关闭后推";
                            hardware.my_io.Set_Do(1, 5, 0);//1
                            hardware.my_io.Set_Do(1, 4, 1);//1     
                            m_TH2.Step++;
                            break;
                        case 27://等待到位
                            m_TH2.Step_str = "等待后推关闭";
                            if (hardware.my_io.m_input[1, 6])
                                m_TH2.Step++;
                            break;
                        case 28://工位1旋转 +电机移动到位置0
                            if (SysStatus.CurProductName != "M381")
                            {
                                m_TH2.Step_str = "打卡旋转及移动电机到0位";
                                hardware.my_io.Set_Do(3, 1, 1);//1
                                hardware.my_io.Set_Do(3, 0, 0);//1     
                            }
                            hardware.my_motion.Move(SysStatus.Axis_2, 0);
                            m_TH2.Step++;
                            break;
                        case 29:
                            Thread.Sleep(200);
                            if (SysStatus.CurProductName != "M381")
                            {
                                if (hardware.my_io.m_input[3, 9])
                                {
                                    m_TH2.Step++;
                                }
                            }
                            else
                                m_TH2.Step++;
                            break;
                        case 30://等待到位
                            m_TH2.Step_str = "等待电机到位及旋转到位";
                            if (hardware.my_motion.Move_Finsh(SysStatus.Axis_2))
                            {
                                if (hardware.my_motion.Check_Now_Pos(SysStatus.Axis_2) == 0)
                                    m_TH2.Step++;
                                else
                                {
                                    if (reback[2])//中断恢复
                                        hardware.my_motion.Move_Pro(SysStatus.Axis_2);

                                }
                                reback[2] = false;
                            }
                            break;
                        case 31://后推打开
                            m_TH2.Step_str = "打开后推";
                            hardware.my_io.Set_Do(1, 4, 0);//1
                            hardware.my_io.Set_Do(1, 5, 1);//1     
                            m_TH2.Step++;
                            break;
                        case 32://等待到位
                            m_TH2.Step_str = "等待推到位";
                            if (hardware.my_io.m_input[1, 7])
                                m_TH2.Step++;
                            break;
                        case 33://下降打开
                            m_TH2.Step_str = "打开下降";
                            hardware.my_io.Set_Do(1, 2, 0);//1
                            hardware.my_io.Set_Do(1, 3, 1);//1     
                            m_TH2.Step++;
                            break;
                        case 34://等待到位                            
                            if (hardware.my_io.m_input[1, 5])
                            {
                                m_TH2.Step++;
                                // SysStatus.NO2 = false;
                            }
                            break;
                        case 35:
                            m_TH2.Step_str = "打开气爪";
                            hardware.my_io.Set_Do(3, 2, 0);//1
                            hardware.my_io.Set_Do(3, 3, 1);//1
                            hardware.my_io.Set_Do(3, 4, 0);//2
                            hardware.my_io.Set_Do(3, 5, 1);//2
                            hardware.my_io.Set_Do(3, 6, 0);//3
                            hardware.my_io.Set_Do(3, 7, 1);//3
                            m_TH2.Step++;
                            break;
                        case 36:
                            m_TH2.Step_str = "等待气爪打开到位信号";
                            if (hardware.my_io.m_input[3, 10] && hardware.my_io.m_input[3, 11] && hardware.my_io.m_input[3, 12] && hardware.my_io.m_input[3, 13] && hardware.my_io.m_input[3, 14]
                                && hardware.my_io.m_input[3, 15] && hardware.my_io.m_input[4, 0] && hardware.my_io.m_input[4, 1] && hardware.my_io.m_input[4, 2] && hardware.my_io.m_input[4, 3])
                                m_TH2.Step++;
                            break;
                        case 37: //上升
                            m_TH2.Step_str = "横移上升";
                            hardware.my_io.Set_Do(1, 2, 1);
                            hardware.my_io.Set_Do(1, 3, 0);
                            m_TH2.Step++;
                            break;
                        case 38://等待到位                    
                            m_TH2.Step_str = "等待横移上升到位";//
                            if (hardware.my_io.m_input[1, 4])
                                m_TH2.Step++;
                            break;
                        case 39://
                            m_TH2.Step_str = "关闭后推";
                            hardware.my_io.Set_Do(1, 4, 1);//1  
                            hardware.my_io.Set_Do(1, 5, 0);//1                         

                            m_TH2.Step++;
                            break;
                        case 40:
                            m_TH2.Step_str = "等待横移后推关闭";
                            if (hardware.my_io.m_input[1, 6])
                            {
                                hardware.my_io.Set_Do(3, 1, 0);//1
                                hardware.my_io.Set_Do(3, 0, 1);//1  
                                m_TH2.Step++;
                            }
                            break;
                        case 41:// 等待到位
                            m_TH2.Step_str = "等待旋转关闭 ";
                            if (hardware.my_io.m_input[3, 8])
                            {
                                Thread.Sleep(30);

                                if (SysStatus.NO2 == false)
                                {
                 
                                    Thread.Sleep(50);
                                    if (!SysStatus.Shield_NO3)
                                        m_TH3.Step = 1;
                                    if (!SysStatus.Shield_NO4)
                                        m_TH4.Step = 1;
                                    if (!SysStatus.Shield_NO5)
                                        m_TH5.Step = 1;
                                    if (!SysStatus.Shield_NO6)
                                        m_TH6.Step = 1;
                                    if (!SysStatus.Shield_NO7)
                                        m_TH7.Step = 1;

                                }
                                m_TH2.Step_str = "";
                                m_TH2.Step = 0;
                                hardware.my_motion.Move(SysStatus.Axis_2, 1);
                                SysStatus.NO2 = false;
                            }
                            break;
                    }

                }

                Application.DoEvents();
                Thread.Sleep(20);
                //   TM2 = Environment.TickCount - TM1_;
            }
            Logger.Error("横移线程停止");

        }

        /// <summary>
        /// t3--nai
        /// </summary>
        private void Run_NO3()
        {
            string str = "";
            bool b = false;//判断
            bool bTemp = false;
            while (SysStatus.Status == SysStatus.EmSysSta.Run)
            {
                double TM1_ = Environment.TickCount;//读取系统时间

                if ((Station[3] && !SysStatus.Shield_NO3) || SysStatus.NO3)//自动或手动
                {
                    if (SysStatus.NO3 && m_TH3.Step == 0)
                        m_TH3.Step = 1;

                    switch (m_TH3.Step)
                    {
                        case 1:  //横移下降 后推 都关闭情况进行耐压测试
                            m_TH3.Step_str = "等待横移在安全区";
                            m_TH3.Time = 0;//超时清零
                            if (hardware.my_io.m_input[1, 4] && hardware.my_io.m_input[1, 6])
                            {
                                SysStatus.NO3 = false;
                                m_TH3.Step++;
                            }
                            break;
                        case 2://有料
                            m_TH3.Step_str = "判断是否有产品";
                            if (hardware.my_io.m_input[4, 8] || hardware.my_io.m_input[4, 9])
                                m_TH3.Step++;
                            else
                                m_TH3.Step = 0;//关闭线程
                            break;
                        case 3:  //下压
                            m_TH3.Step_str = "打开下压";
                            hardware.my_io.Set_Do(1, 8, 0);//1
                            hardware.my_io.Set_Do(1, 9, 1);//1   
                            m_TH3.Step++;
                            break;
                        case 4:  //等待到位
                            m_TH3.Step_str = "等待下压到位";
                            if (hardware.my_io.m_input[1, 11])
                                m_TH3.Step++;
                            break;
                        case 5: //后推
                            m_TH3.Step_str = "打开后推";
                            hardware.my_io.Set_Do(1, 6, 0);//1
                            hardware.my_io.Set_Do(1, 7, 1);//1   
                            m_TH3.Step++;
                            break;
                        case 6://等待到位
                            m_TH3.Step_str = "等待后推到位";
                            if (hardware.my_io.m_input[1, 9])
                                m_TH3.Step++;
                            break;

                        case 7://发送开始
                            m_TH3.Step_str = "开始测试";
                            //传入相应数据
                            hardware.m_8740.SetData(hardware.my_cord.my_Pro[2].Cord_ST_L, hardware.my_cord.my_Pro[2].Cord_ST_R, hardware.my_cord.my_Pro[2].Model_Num_L, hardware.my_cord.my_Pro[2].Model_Num_R);
                            hardware.m_8740.SendStart(out str);//发送测试指令
                            m_TH3.Step++;
                            break;
                        case 8:
                            if (str.Length > 10)
                            {
                                Thread.Sleep(800);
                                m_TH3.Step++;
                            }
                            break;
                        case 9://解析等待数据                            
                            if (str.Length > 10)
                            {
                                bTemp = hardware.m_8740.AnalyseData(str);//解析数据

                                hardware.m_8740.SaveToDB("L");//通过
                                hardware.m_8740.SaveToDB("R");//通过
                                //if (bTemp)
                                //{
                                   
                                //}
                                if (hardware.m_8740.M_Product_L)//测试通过
                                    hardware.my_cord.Set_ST_STS(2, 2, "L", true);//未通过
                                else
                                    hardware.my_cord.Set_ST_STS(2, 2, "L", false);//未通过

                                if (hardware.m_8740.M_Product_R)
                                    hardware.my_cord.Set_ST_STS(2, 2, "R", true);//未通过
                                else
                                    hardware.my_cord.Set_ST_STS(2, 2, "R", false);//未通过
                                                                                  // hardware.my_cord.SaveSTcord();
                            }
                            m_TH3.Step++;

                            break;
                        case 10://关闭后推
                            hardware.my_io.Set_Do(1, 7, 0);//1
                            hardware.my_io.Set_Do(1, 6, 1);//1   
                            m_TH3.Step++;
                            break;
                        case 11://等待到位
                            m_TH3.Step_str = "等待后推关闭";
                            if (hardware.my_io.m_input[1, 8])
                                m_TH3.Step++;
                            break;
                        case 12://关闭下压
                            hardware.my_io.Set_Do(1, 9, 0);//1
                            hardware.my_io.Set_Do(1, 8, 1);//1   
                            m_TH3.Step++;
                            break;
                        case 13://下压关闭到位
                            m_TH3.Step_str = "等待下压关闭";
                            if (hardware.my_io.m_input[1, 10])
                            {
                                m_TH3.Step = 0;
                                SysStatus.NO3 = false;
                            }
                            break;

                    }

                }



                Application.DoEvents();
                Thread.Sleep(110);
                TM2 = Environment.TickCount - TM1_;
            }
            Logger.Error("耐压检测线程停止");
        }

        public static int cycleTestFlag=0;
        /// <summary>
        /// t4-ccd1
        /// </summary>
        private void Run_NO4()
        {
            bool left = false, right = false;
            int temp = -1;
            int now_pos = -1;//1左  2右
            int cam_sum = 0;
            bool bTEMP1 = false, bTEMP2 = false;
            while (SysStatus.Status == SysStatus.EmSysSta.Run)
            {
                double TM1_ = Environment.TickCount;

                if ((Station[4] && !SysStatus.Shield_NO4) || SysStatus.NO4)
                {
                    if (SysStatus.NO4 && m_TH4.Step == 0)
                    {
                        m_TH4.Step = 1;
                        SysStatus.NO4 = false;
                    }
                    switch (m_TH4.Step)
                    {
                        case 1:  //横移下降 后推 都关闭情况进行耐压测试
                            cam_sum = 0;
                            now_pos = -1;
                            m_TH4.Time = 0;
                            m_TH4.Step_str = "判断横移是否在安全区";
                            if (hardware.my_io.m_input[1, 4] && hardware.my_io.m_input[1, 6])
                                m_TH4.Step++;
                            break;
                        case 2://有料
                            m_TH4.Step_str = "判断是否有料";
                            left = hardware.my_io.m_input[4, 10];
                            right = hardware.my_io.m_input[4, 11];

                            if (hardware.my_cord.my_Pro[3].STS_L_voltage == false)
                                left = false;
                            if (hardware.my_cord.my_Pro[3].STS_R_voltage == false)
                                right = false;




                            if (left == false && right == false)
                                m_TH4.Step = 0;//没有料直接退出本次测量

                            else
                            {
                                m_TH4.Step_str = "气缸下压及关闭位置";
                                hardware.my_io.Set_Do(1, 12, 1);//1  关闭位置
                                hardware.my_io.Set_Do(1, 13, 0);//1

                                hardware.my_io.Set_Do(1, 10, 0);//1  下压
                                hardware.my_io.Set_Do(1, 11, 1);//1          
                                m_TH4.Step++;//左侧有产品 
                            }
                            break;
                        case 3:
                            if (hardware.my_io.m_input[1, 13])//等待下压到位
                                m_TH4.Step++;
                            break;

                        case 4://判断当前位置
                            temp = hardware.my_motion.Check_Now_Pos(SysStatus.Axis_3);

                            if (temp == 0 && left) //当前在左侧并且有产品
                            {
                                now_pos = 1;
                                m_TH4.Step = 7;//左侧有产品
                            }
                            else if (temp == 1 && right) //当前在右侧且有产品
                            {
                                now_pos = 2;
                                m_TH4.Step = 7;//
                            }
                            else
                            {
                                now_pos = 1;
                                m_TH4.Step++;//不在指定位置 进行左移
                            }
                            break;
                        case 5://电机到位置1
                            m_TH4.Step_str = "电机移动到0位 ";
                            hardware.my_motion.Move(SysStatus.Axis_3, 0);//左

                            m_TH4.Step++;
                            break;
                        case 6://等待到位
                            m_TH4.Step_str = "等待电机到等待位";
                            if (hardware.my_motion.Move_Finsh(SysStatus.Axis_3))
                                m_TH4.Step++;
                            else
                            {
                                if (hardware.my_motion.Check_Run(SysStatus.Axis_3) == false)
                                {
                                    if (reback[4])//中断恢复
                                        hardware.my_motion.Move_Pro(SysStatus.Axis_3);
                                    reback[4] = false;
                                }
                            }

                            break;
                        case 7://发送测量指令
                            m_TH4.Step_str = "发送测量";
                            Thread.Sleep(1500);
                            if (now_pos == 1)//左
                            {
                                if (SysStatus.Shield_NO4_P == false)//屏蔽位置
                                    hardware.my_ccd1.CMD_CAM1(TH_Product_Num, hardware.my_cord.my_Pro[3].Model_Num_L, "L");//位置
                                if (SysStatus.Shield_NO4_H == false)//屏蔽高度
                                    hardware.my_ccd2.CMD_CAM1(TH_Product_Num, hardware.my_cord.my_Pro[3].Model_Num_L, "L");//高度

                            }
                            else//右
                            {
                                if (SysStatus.Shield_NO4_P == false)//屏蔽位置
                                    hardware.my_ccd1.CMD_CAM1(TH_Product_Num, hardware.my_cord.my_Pro[3].Model_Num_R, "R");//位置
                                if (SysStatus.Shield_NO4_H == false)//屏蔽高度
                                    hardware.my_ccd2.CMD_CAM1(TH_Product_Num, hardware.my_cord.my_Pro[3].Model_Num_R, "R");//高度
                            }
                            cam_sum++;
                            m_TH4.Step++;
                            break;
                        case 8://等待反馈指令
                            Thread.Sleep(300);
                            if (SysStatus.Shield_NO4_P)//屏蔽位置
                                m_TH4.Step = 13;
                            else
                            {
                                m_TH4.Step_str = "等待指令";
                                if (hardware.my_ccd1.data1.Length > 7)
                                    m_TH4.Step++;
                            }
                            break;
                        case 9://位置气缸打开
                            Thread.Sleep(200);
                            m_TH4.Step_str = "打开位置气缸";
                            hardware.my_io.Set_Do(1, 12, 0);//1
                            hardware.my_io.Set_Do(1, 13, 1);//1   
                            m_TH4.Step++;
                            break;
                        case 10://等待到位
                            Thread.Sleep(800);
                            m_TH4.Step++;
                            break;
                        case 11://发送测量指令2
                            m_TH4.Step_str = "相机2次拍摄";
                            if (SysStatus.Shield_NO4_P == false)//屏蔽位置
                            {
                                if (now_pos == 1)
                                    hardware.my_ccd1.CMD_CAM2(TH_Product_Num, hardware.my_cord.my_Pro[3].Model_Num_L, "L");//位置
                                else
                                    hardware.my_ccd1.CMD_CAM2(TH_Product_Num, hardware.my_cord.my_Pro[3].Model_Num_R, "R");//位置
                            }
                            m_TH4.Step++;
                            break;
                        case 12://等待指令
                            m_TH4.Step_str = "延时";
                            Thread.Sleep(300);
                            if (hardware.my_ccd1.data1.Length > 7)
                                m_TH4.Step++;
                            break;
                        case 13:
                            //解析指令
                            try
                            {
                                if (SysStatus.Shield_NO4_P == false)//屏蔽位置
                                    bTEMP1 = hardware.my_ccd1.AnalyseData_Pos();//解析位置数据

                                if (SysStatus.Shield_NO4_H == false)//屏蔽位置
                                    bTEMP2 = hardware.my_ccd2.AnalyseData_High();//解析高度数据


                                if (now_pos == 1)//左
                                {
                                    if (SysStatus.Shield_NO4_P == false)//屏蔽p
                                    {
                                        if (hardware.my_ccd1.data1.Contains("OK"))
                                            hardware.my_cord.Set_ST_STS(6, 3, "L", true);
                                        else
                                        {
                                            if (cycleTestFlag==2)  hardware.my_cord.Set_ST_STS(6, 3, "L", false);
                                            else
                                            {
                                                cycleTestFlag = 1;
                                                Logger.Debug($"{hardware.my_cord.my_Pro[3].Cord_ST_L}复测", 7);
                                            }
                                                
                                        }

                                    }
                                    if (SysStatus.Shield_NO4_H == false)//屏蔽h
                                    {
                                        if (hardware.my_ccd2.data1.Contains("OK"))
                                            hardware.my_cord.Set_ST_STS(3, 3, "L", true);
                                        else
                                        {
                                            if (cycleTestFlag == 2) hardware.my_cord.Set_ST_STS(3, 3, "L", false);
                                            else
                                            {
                                                cycleTestFlag = 1;
                                                Logger.Debug($"{hardware.my_cord.my_Pro[3].Cord_ST_L}复测", 7);
                                            }
                                        }

                                    }

                                    if (SysStatus.Shield_NO4_P == false)//屏蔽p
                                        hardware.my_ccd1.SaveToDB_Pos(hardware.my_cord.my_Pro[3].Cord_ST_L);
                                    if (SysStatus.Shield_NO4_H == false)//屏蔽h
                                        hardware.my_ccd2.SaveToDB_High(hardware.my_cord.my_Pro[3].Cord_ST_L);
                                }
                                else//右
                                {

                                    if (SysStatus.Shield_NO4_P == false)//屏蔽p
                                    {
                                        if (hardware.my_ccd1.data1.Contains("OK"))
                                            hardware.my_cord.Set_ST_STS(6, 3, "R", true);
                                        else
                                        {
                                            if (cycleTestFlag == 2) hardware.my_cord.Set_ST_STS(6, 3, "R", false);
                                            else
                                            {
                                                cycleTestFlag = 1;
                                                Logger.Debug($"{hardware.my_cord.my_Pro[3].Cord_ST_R}复测", 7);
                                            }
                                        }

                                    }
                                    if (SysStatus.Shield_NO4_H == false)//屏蔽h
                                    {
                                        if (hardware.my_ccd2.data1.Contains("OK"))
                                            hardware.my_cord.Set_ST_STS(3, 3, "R", true);
                                        else
                                        {
                                            if (cycleTestFlag==2) hardware.my_cord.Set_ST_STS(3, 3, "R", false);
                                            else
                                            {
                                                cycleTestFlag = 1;
                                                Logger.Debug($"{hardware.my_cord.my_Pro[3].Cord_ST_R}复测", 7);
                                            }

                                        }

                                    }
                                    if (SysStatus.Shield_NO4_P == false)//屏蔽位
                                        hardware.my_ccd1.SaveToDB_Pos(hardware.my_cord.my_Pro[3].Cord_ST_R);
                                    if (SysStatus.Shield_NO4_H == false)//屏蔽h
                                        hardware.my_ccd2.SaveToDB_High(hardware.my_cord.my_Pro[3].Cord_ST_R);
                                }

                                if (cycleTestFlag == 1)
                                {
                                   
                                    if (now_pos == 1)//左
                                    {
                                        if (SysStatus.Shield_NO4_P == false)//屏蔽位置
                                            hardware.my_ccd1.CMD_CAM1(TH_Product_Num, hardware.my_cord.my_Pro[3].Model_Num_L, "L");//位置
                                        if (SysStatus.Shield_NO4_H == false)//屏蔽高度
                                            hardware.my_ccd2.CMD_CAM1(TH_Product_Num, hardware.my_cord.my_Pro[3].Model_Num_L, "L");//高度

                                    }
                                    else//右
                                    {
                                        if (SysStatus.Shield_NO4_P == false)//屏蔽位置
                                            hardware.my_ccd1.CMD_CAM1(TH_Product_Num, hardware.my_cord.my_Pro[3].Model_Num_R, "R");//位置
                                        if (SysStatus.Shield_NO4_H == false)//屏蔽高度
                                            hardware.my_ccd2.CMD_CAM1(TH_Product_Num, hardware.my_cord.my_Pro[3].Model_Num_R, "R");//高度
                                    }

                                    if (SysStatus.Shield_NO4_P == false)//屏蔽位置
                                    {
                                        if (now_pos == 1)
                                            hardware.my_ccd1.CMD_CAM2(TH_Product_Num, hardware.my_cord.my_Pro[3].Model_Num_L, "L");//位置
                                        else
                                            hardware.my_ccd1.CMD_CAM2(TH_Product_Num, hardware.my_cord.my_Pro[3].Model_Num_R, "R");//位置
                                    }
                                    cycleTestFlag = 2;//复位
                                    Logger.Debug("复测完毕", 7);
                                }
                                else
                                {
                                    cycleTestFlag = 0;
                                    m_TH4.Step++;
                                }

                            }
                            catch (Exception eee)
                            {
                                MessageBox.Show(eee.ToString());
                                Logger.Error($"{eee.Message+eee.StackTrace}", 7);
                            }

                            break;
                        //case 13:
                        //    //解析指令
                        //    try
                        //    {
                        //        if (SysStatus.Shield_NO4_P == false)//屏蔽位置
                        //            bTEMP1 = hardware.my_ccd1.AnalyseData_Pos();//解析位置数据

                        //        if (SysStatus.Shield_NO4_H == false)//屏蔽位置
                        //            bTEMP2 = hardware.my_ccd2.AnalyseData_High();//解析高度数据

                        //        if (now_pos == 1)//左
                        //        {


                        //            if (SysStatus.Shield_NO4_P == false)//屏蔽p
                        //            {
                        //                if (hardware.my_ccd1.data1.Contains("OK"))
                        //                    hardware.my_cord.Set_ST_STS(6, 3, "L", true);
                        //                else
                        //                    hardware.my_cord.Set_ST_STS(6, 3, "L", false);

                        //            }
                        //            if (SysStatus.Shield_NO4_H == false)//屏蔽h
                        //            {
                        //                if (hardware.my_ccd2.data1.Contains("OK"))
                        //                    hardware.my_cord.Set_ST_STS(3, 3, "L", true);
                        //                else
                        //                    hardware.my_cord.Set_ST_STS(3, 3, "L", false);
                        //            }
                        //            if (SysStatus.Shield_NO4_P == false)//屏蔽p
                        //                hardware.my_ccd1.SaveToDB_Pos(hardware.my_cord.my_Pro[3].Cord_ST_L);
                        //            if (SysStatus.Shield_NO4_H == false)//屏蔽h
                        //                hardware.my_ccd2.SaveToDB_High(hardware.my_cord.my_Pro[3].Cord_ST_L);
                        //        }
                        //        else//右
                        //        {


                        //            if (SysStatus.Shield_NO4_P == false)//屏蔽p
                        //            {

                        //                if (hardware.my_ccd1.data1.Contains("OK"))
                        //                    hardware.my_cord.Set_ST_STS(6, 3, "R", true);
                        //                else
                        //                    hardware.my_cord.Set_ST_STS(6, 3, "R", false);
                        //            }
                        //            if (SysStatus.Shield_NO4_H == false)//屏蔽h
                        //            {

                        //                if (hardware.my_ccd2.data1.Contains("OK"))
                        //                    hardware.my_cord.Set_ST_STS(3, 3, "R", true);
                        //                else
                        //                    hardware.my_cord.Set_ST_STS(3, 3, "R", false);
                        //            }
                        //            if (SysStatus.Shield_NO4_P == false)//屏蔽位
                        //                hardware.my_ccd1.SaveToDB_Pos(hardware.my_cord.my_Pro[3].Cord_ST_R);
                        //            if (SysStatus.Shield_NO4_H == false)//屏蔽h
                        //                hardware.my_ccd2.SaveToDB_High(hardware.my_cord.my_Pro[3].Cord_ST_R);
                        //        }
                        //    }
                        //    catch (Exception eee)
                        //    {
                        //        MessageBox.Show(eee.ToString());
                        //    }
                        //    m_TH4.Step++;
                        //    break;
                        case 14://关闭位置气缸
                            m_TH4.Step_str = "关闭位置";
                            hardware.my_io.Set_Do(1, 13, 0);//1
                            hardware.my_io.Set_Do(1, 12, 1);//1   

                            m_TH4.Step++;
                            break;

                        case 15://移动到位置左
                            if (cam_sum == 2)
                                m_TH4.Step = 17;
                            else if (now_pos == 1 && right)//当前在左 移动到右
                            {
                                hardware.my_motion.Move(SysStatus.Axis_3, 1);
                                now_pos = 2;
                                m_TH4.Step++;
                            }
                            else if (now_pos == 2 && left)//当前在左 移动到右
                            {
                                hardware.my_motion.Move(SysStatus.Axis_3, 0);
                                now_pos = 1;
                                m_TH4.Step++;
                            }
                            else
                            {
                                m_TH4.Step = 17;
                            }
                            break;
                        case 16://等待到位
                            m_TH4.Step_str = "等待电机到位";
                            if (hardware.my_motion.Move_Finsh(SysStatus.Axis_3))
                            {
                                m_TH4.Step = 7;
                            }
                            else
                            {
                                if (hardware.my_motion.Check_Run(SysStatus.Axis_3) == false)
                                {
                                    if (reback[4])//中断恢复
                                        hardware.my_motion.Move_Pro(SysStatus.Axis_3);
                                    reback[4] = false;
                                }
                            }
                            break;
                        case 17:
                            hardware.my_io.Set_Do(1, 10, 1);//1  关闭下压
                            hardware.my_io.Set_Do(1, 11, 0);//1
                            m_TH4.Step_str = "";
                            m_TH4.Step = 0;
                            break;
                    }


                }

                Application.DoEvents();
                Thread.Sleep(30);
                TM3 = Environment.TickCount - TM1_;
            }
            Logger.Error("高度检测线程停止");
        }
        /// <summary>
        /// t5--ccd2
        /// </summary>
        private void Run_NO5()
        {
            bool left = false, right = false;
            int temp = -1;
            int now_pos = -1;//1左  2右
            int cam_sum = 0;
            bool bTemp1 = false, bTemp2 = false;
            while (SysStatus.Status == SysStatus.EmSysSta.Run)
            {
                double TM1_ = Environment.TickCount;

                //位置度检测                         
                if ((Station[5] && !SysStatus.Shield_NO5) || SysStatus.NO5)
                {
                    if (SysStatus.NO5 && m_TH5.Step == 0)
                        m_TH5.Step = 1;

                    switch (m_TH5.Step)
                    {
                        case 1:
                            cam_sum = 0;
                            now_pos = -1;
                            m_TH5.Time = 0;
                            m_TH5.Step_str = "判断横移是否在安全位置";
                            if (hardware.my_io.m_input[1, 4] && hardware.my_io.m_input[1, 6])
                                m_TH5.Step++;
                            break;
                        case 2:
                            m_TH5.Step_str = "判断工位是否有料";
                            left = hardware.my_io.m_input[4, 13];
                            right = hardware.my_io.m_input[4, 12];

                            if (hardware.my_cord.my_Pro[4].STS_L_voltage == false)
                                left = false;
                            if (hardware.my_cord.my_Pro[4].STS_R_voltage == false)
                                right = false;


                            if (hardware.my_cord.my_Pro[4].STS_L_CCD1_H == false)
                                left = false;
                            if (hardware.my_cord.my_Pro[4].STS_R_CCD1_H == false)
                                right = false;

                            if (hardware.my_cord.my_Pro[4].STS_L_CCD1_P == false)
                                left = false;
                            if (hardware.my_cord.my_Pro[4].STS_R_CCD1_P == false)
                                right = false;

                            if (left == false && right == false)//没有料结束线程
                                m_TH5.Step = 0;
                            else
                                m_TH5.Step++;
                            break;

                        case 3:
                            m_TH5.Step_str = "下压";
                            hardware.my_io.Set_Do(1, 14, 0);//1
                            hardware.my_io.Set_Do(1, 15, 1);//1   
                            m_TH5.Step++;
                            break;
                        case 4:
                            m_TH5.Step_str = "等待下压到位";
                            if (hardware.my_io.m_input[2, 1])
                                m_TH5.Step++;
                            break;
                        case 5:
                            m_TH5.Step_str = "气缸后推";
                            hardware.my_io.Set_Do(2, 0, 0);//1
                            hardware.my_io.Set_Do(2, 1, 1);//1   
                            m_TH5.Step++;
                            break;
                        case 6:
                            temp = hardware.my_motion.Check_Now_Pos(SysStatus.Axis_4);
                            if (temp == 0 && left) //当前在左侧并且有产品
                            {
                                now_pos = 1;
                                m_TH5.Step = 9;//左侧有产品
                            }
                            else if (temp == 1 && right) //当前在右侧且有产品
                            {
                                now_pos = 2;
                                m_TH5.Step = 9;//
                            }
                            else
                            {
                                now_pos = 1;
                                m_TH5.Step++;//不在指定位置 进行左移
                            }
                            break;
                        case 7:
                            hardware.my_motion.Move(SysStatus.Axis_4, 0);
                            m_TH5.Step++;
                            break;
                        case 8:
                            m_TH5.Step_str = "等待电机到等待位";
                            if (hardware.my_motion.Move_Finsh(SysStatus.Axis_4))
                            {
                                m_TH5.Step++;
                            }
                            else
                            {
                                if (hardware.my_motion.Check_Run(SysStatus.Axis_4) == false)
                                {
                                    if (reback[5])//中断恢复
                                        hardware.my_motion.Move_Pro(SysStatus.Axis_4);
                                    reback[5] = false;
                                }
                            }
                            break;
                        case 9://发送测量
                            m_TH5.Step_str = "发送测量指令";
                            Thread.Sleep(2000);
                            if (now_pos == 1)
                            {
                                if (SysStatus.Shield_NO5_P == false)//屏蔽位置
                                    hardware.my_ccd3.CMD_CAM1(TH_Product_Num, hardware.my_cord.my_Pro[4].Model_Num_L, "L");//位置
                                if (SysStatus.Shield_NO5_H == false)//屏蔽位置
                                    hardware.my_ccd4.CMD_CAM1(TH_Product_Num, hardware.my_cord.my_Pro[4].Model_Num_L, "L");//高度                             
                            }
                            else
                            {
                                if (SysStatus.Shield_NO5_P == false)//屏蔽位置
                                    hardware.my_ccd3.CMD_CAM1(TH_Product_Num, hardware.my_cord.my_Pro[4].Model_Num_R, "R");//位置
                                if (SysStatus.Shield_NO5_H == false)//屏蔽位置
                                    hardware.my_ccd4.CMD_CAM1(TH_Product_Num, hardware.my_cord.my_Pro[4].Model_Num_R, "R");//高度                     
                            }
                            m_TH5.Step++;
                            break;
                        case 10://等待指令
                            m_TH5.Step_str = "延时";
                            Thread.Sleep(500);
                            if (SysStatus.Shield_NO5_P)//屏蔽位置
                            {
                                Thread.Sleep(700);
                                m_TH5.Step = 13;//屏蔽跳转
                            }
                            else
                            {
                                if (hardware.my_ccd3.data1.Length > 7)
                                    m_TH5.Step++;
                            }
                            break;
                        case 11:
                            m_TH5.Step_str = "延时二次拍照";
                            Thread.Sleep(300);
                            if (SysStatus.Shield_NO5_P == false)//屏蔽位置
                            {
                                if (now_pos == 1)
                                {
                                    hardware.my_ccd3.CMD_CAM2(TH_Product_Num, hardware.my_cord.my_Pro[4].Model_Num_L, "L");//位置                             
                                }
                                else
                                {
                                    hardware.my_ccd3.CMD_CAM2(TH_Product_Num, hardware.my_cord.my_Pro[4].Model_Num_R, "R");//位置                        
                                }
                            }
                            m_TH5.Step++;
                            break;
                        case 12://等待指令
                            m_TH5.Step_str = "等待数据";
                            if (hardware.my_ccd3.data1.Length > 7)
                                m_TH5.Step++;
                            break;
                        case 13:


                            if (SysStatus.Shield_NO5_P == false)//屏蔽位置
                                bTemp1 = hardware.my_ccd3.AnalyseData_Pos();//解析位置数据
                            if (SysStatus.Shield_NO5_H == false)//屏蔽位置
                                bTemp2 = hardware.my_ccd4.AnalyseData_High();//解析高度数据

                            if (now_pos == 1)
                            {


                                if (SysStatus.Shield_NO5_P == false)//屏蔽位置
                                {


                                    if (hardware.my_ccd3.data1.Contains("OK"))
                                        hardware.my_cord.Set_ST_STS(7, 4, "L", true);
                                    else
                                        hardware.my_cord.Set_ST_STS(7, 4, "L", false);
                                }
                                if (SysStatus.Shield_NO5_H == false)//
                                {

                                    if (hardware.my_ccd4.data1.Contains("OK"))
                                        hardware.my_cord.Set_ST_STS(4, 4, "L", true);
                                    else
                                        hardware.my_cord.Set_ST_STS(4, 4, "L", false);
                                }
                                if (SysStatus.Shield_NO5_P == false)//屏蔽位置
                                    hardware.my_ccd3.SaveToDB_Pos(hardware.my_cord.my_Pro[4].Cord_ST_L);
                                if (SysStatus.Shield_NO5_H == false)//屏蔽gao
                                    hardware.my_ccd4.SaveToDB_High(hardware.my_cord.my_Pro[4].Cord_ST_L);
                            }
                            else
                            {

                                if (SysStatus.Shield_NO5_P == false)//屏蔽位置
                                {

                                    if (hardware.my_ccd3.data1.Contains("OK"))
                                        hardware.my_cord.Set_ST_STS(7, 4, "R", true);
                                    else
                                        hardware.my_cord.Set_ST_STS(7, 4, "R", false);
                                }
                                if (SysStatus.Shield_NO5_H == false)//屏蔽位置
                                {

                                    if (hardware.my_ccd4.data1.Contains("OK"))
                                        hardware.my_cord.Set_ST_STS(4, 4, "R", true);
                                    else
                                        hardware.my_cord.Set_ST_STS(4, 4, "R", false);

                                }
                                if (SysStatus.Shield_NO5_P == false)//屏蔽位置
                                    hardware.my_ccd3.SaveToDB_Pos(hardware.my_cord.my_Pro[4].Cord_ST_R);
                                if (SysStatus.Shield_NO5_H == false)//屏蔽位置
                                    hardware.my_ccd4.SaveToDB_High(hardware.my_cord.my_Pro[4].Cord_ST_R);
                            }

                            cam_sum++;
                            m_TH5.Step++;
                            break;
                        case 14:
                            m_TH5.Step_str = "电机移动";
                            if (cam_sum == 2)
                                m_TH5.Step = 16;
                            else if (now_pos == 1 && right)//当前在左 移动到右
                            {
                                hardware.my_motion.Move(SysStatus.Axis_4, 1);
                                now_pos = 2;
                                m_TH5.Step++;
                            }
                            else if (now_pos == 2 && left)//当前在右 移动到左
                            {
                                hardware.my_motion.Move(SysStatus.Axis_4, 0);
                                now_pos = 1;
                                m_TH5.Step++;
                            }
                            else
                            {
                                m_TH5.Step = 16;
                            }
                            break;
                        case 15://等待到位
                            m_TH5.Step_str = "等待电机到位";
                            if (hardware.my_motion.Move_Finsh(SysStatus.Axis_4))
                            {
                                m_TH5.Step = 9;
                            }
                            else
                            {
                                if (hardware.my_motion.Check_Run(SysStatus.Axis_4) == false)
                                {
                                    if (reback[5])//中断恢复
                                        hardware.my_motion.Move_Pro(SysStatus.Axis_4);
                                    reback[5] = false;
                                }
                            }
                            break;


                        case 16://关闭前推

                            hardware.my_io.Set_Do(2, 1, 0);//1
                            hardware.my_io.Set_Do(2, 0, 1);//1   
                            m_TH5.Step++;
                            break;
                        case 17://等待到位
                            m_TH5.Step_str = "等待前推关闭";
                            if (hardware.my_io.m_input[2, 2])
                                m_TH5.Step++;
                            break;
                        case 18://关闭下压
                            hardware.my_io.Set_Do(1, 15, 0);//1
                            hardware.my_io.Set_Do(1, 14, 1);//1   
                            m_TH5.Step++;
                            break;
                        case 19:
                            m_TH5.Step_str = "等待下压关闭";
                            if (hardware.my_io.m_input[2, 0])//等待到位
                            {
                                m_TH5.Step_str = "";
                                m_TH5.Step = 0;
                                SysStatus.NO5 = false;
                            }
                            break;
                    }

                }

                Application.DoEvents();
                Thread.Sleep(30);
                TM4 = Environment.TickCount - TM1_;
            }
            Logger.Error("位置度检测线程停止");

        }
        /// <summary>
        /// t6 ji
        /// </summary>
        private void Run_NO6()
        {
            bool left, right;
            string str1, str2;
            left = false;
            right = false;
            str2 = "";
            str1 = "";
            while (SysStatus.Status == SysStatus.EmSysSta.Run)
            {
                double TM1_ = Environment.TickCount;

                if ((Station[6] && !SysStatus.Shield_NO6) || SysStatus.NO6)
                {
                    if (SysStatus.NO6 && m_TH6.Step == 0)
                        m_TH6.Step = 1;
                    try
                    {
                        switch (m_TH6.Step)
                        {
                            case 1:  //横移下降 后推 都关闭情况进行耐压测试
                                m_TH6.Time = 0;
                                m_TH6.Step_str = "等待横移到安全位置";
                                if (hardware.my_io.m_input[1, 4] && hardware.my_io.m_input[1, 6])
                                    m_TH6.Step++;
                                break;
                            case 2://有料

                                left = hardware.my_io.m_input[4, 14];
                                right = hardware.my_io.m_input[4, 15];
                                str1 = "";
                                str2 = "";
                                m_TH6.Step++;
                                if (left == false && right == false)
                                {
                                    m_TH6.Step = 16;
                                    hardware.my_cord.Set_ST_STS(5, 5, "L", false);
                                    hardware.my_cord.Set_ST_STS(5, 5, "R", false);
                                }
                                //重码防呆
                                bool checkLeft = false, checkRight = false;
                                if (hardware.my_cxr1.search(hardware.my_cord.my_Pro[5].Cord_ST_L) > 0)
                                    checkLeft = true;
                                if (hardware.my_cxr2.search(hardware.my_cord.my_Pro[5].Cord_ST_R) > 0)
                                    checkRight = true;

                                if (checkLeft || checkRight)
                                {
                                    m_TH6.Step = 16;
                                    hardware.my_cord.Set_ST_STS(5, 5, "L", false);
                                    hardware.my_cord.Set_ST_STS(5, 5, "R", false);
                                }

                                Logger.Debug($"左工位待打码条码:【{hardware.my_cord.my_Pro[5].Cord_ST_L}】IO有料感应结果:【{left}】是否重码:【{checkLeft}】", 6);
                                Logger.Debug($"右工位待打码条码:【{hardware.my_cord.my_Pro[5].Cord_ST_R}】IO有料感应结果:【{right}】是否重码:【{checkRight}】", 6);

                                break;
                            case 3://发送扫码指令
                                m_TH6.Step_str = "开始扫码";
                                hardware.my_cxr1.Send_On();
                                hardware.my_cxr2.Send_On();
                                m_TH6.Step++;
                                break;
                            case 4:
                                m_TH6.Step++;
                                break;
                            case 5://延时300
                                m_TH6.Step_str = "延时";
                                Thread.Sleep(1000);
                                m_TH6.Step++;
                                break;
                            case 6://关闭扫码
                                   //   if (left)
                                m_TH6.Step_str = "关闭扫码";
                                hardware.my_cxr1.Send_Off();
                                //   if (right)
                                hardware.my_cxr2.Send_Off();
                                m_TH6.Step++;
                                break;
                            case 7:
                                m_TH6.Step_str = "延时";
                                Thread.Sleep(400);
                                m_TH6.Step++;
                                break;
                            case 8://是否有码

                                Logger.Debug($"左工位待打码条码:【{hardware.my_cord.my_Pro[5].Cord_ST_L}】一次扫码结果:【{ hardware.my_cxr1.str}】", 6);
                                Logger.Debug($"右工位待打码条码:【{hardware.my_cord.my_Pro[5].Cord_ST_R}】一次扫码结果:【{ hardware.my_cxr2.str}】", 6);
                                if (left && hardware.my_cxr1.str.Contains("ERROR") && hardware.my_cord.my_Pro[5].STS_L_voltage == true &&
                                    hardware.my_cord.my_Pro[5].STS_L_CCD1_H == true && hardware.my_cord.my_Pro[5].STS_L_CCD1_P == true &&
                                    hardware.my_cord.my_Pro[5].STS_L_CCD2_H == true && hardware.my_cord.my_Pro[5].STS_L_CCD2_P == true)
                                    str1 = hardware.my_cord.my_Pro[5].Cord_ST_L;
                                else
                                    str1 = "";
                                if (right && hardware.my_cxr2.str.Contains("ERROR") && hardware.my_cord.my_Pro[5].STS_R_voltage == true &&
                                    hardware.my_cord.my_Pro[5].STS_R_CCD1_H == true && hardware.my_cord.my_Pro[5].STS_R_CCD1_P == true &&
                                    hardware.my_cord.my_Pro[5].STS_R_CCD2_H == true && hardware.my_cord.my_Pro[5].STS_R_CCD2_P == true)
                                    str2 = hardware.my_cord.my_Pro[5].Cord_ST_R;
                                else
                                    str2 = "";

                                if (hardware.my_cxr1.search(str1) > 0)
                                    str1 = "";
                                if (hardware.my_cxr2.search(str2) > 0)
                                    str2 = "";


                                m_TH6.Step++;
                                break;
                            case 9://修改变量
                                if (str1.Length == 0 && str2.Length == 0)
                                {
                                    m_TH6.Step = 16;
                                    Logger.Debug($"左工位待打码条码:【{hardware.my_cord.my_Pro[5].Cord_ST_L}】预打码失败:【产品测试不合格】", 6);
                                    Logger.Debug($"右工位待打码条码:【{hardware.my_cord.my_Pro[5].Cord_ST_R}】预打码失败:【产品测试不合格】", 6);
                                    hardware.my_cord.Set_ST_STS(5, 5, "L", false);
                                    hardware.my_cord.Set_ST_STS(5, 5, "R", false);
                                }

                                else
                                {
                                    hardware.my_laser.SetValue(str1, str2);
                                    m_TH6.Step++;
                                    Logger.Debug($"左工位待打码条码:【{hardware.my_cord.my_Pro[5].Cord_ST_L}】发送打码内容:【{ str1}】", 6);
                                    Logger.Debug($"右工位待打码条码:【{hardware.my_cord.my_Pro[5].Cord_ST_R}】发送打码内容:【{ str2}】", 6);
                                }

                                break;
                            case 10://进行打码
                                m_TH6.Step_str = "开始大码";
                                hardware.my_laser.Start();
                                m_TH6.Step++;
                                break;
                            case 11://延时
                                m_TH6.Step_str = "激光延时";
                                Thread.Sleep(6000);
                                m_TH6.Step++;
                                break;
                            case 12://扫码
                                m_TH6.Step_str = "扫码";
                                if (left)
                                    hardware.my_cxr1.Send_On();
                                if (right)
                                    hardware.my_cxr2.Send_On();
                                m_TH6.Step++;
                                break;
                            case 13://延时
                                m_TH6.Step_str = "延时";
                                Thread.Sleep(1000);
                                m_TH6.Step++;
                                break;
                            case 14://关闭扫码
                                if (left)
                                    hardware.my_cxr1.Send_Off();
                                if (right)
                                    hardware.my_cxr2.Send_Off();
                                m_TH6.Step++;

                                Logger.Debug($"左工位待打码条码:【{hardware.my_cord.my_Pro[5].Cord_ST_L}】二次扫码结果:【{ hardware.my_cxr1.Get_data()}】", 6);
                                Logger.Debug($"右工位待打码条码:【{hardware.my_cord.my_Pro[5].Cord_ST_R}】二次扫码结果:【{ hardware.my_cxr2.Get_data()}】", 6);

                                hardware.my_cxr1.Set_Cord_Num(hardware.my_cord.my_Pro[5].Cord_ST_L, hardware.my_cord.my_Pro[5].Model_Num_L.ToString());
                                hardware.my_cxr2.Set_Cord_Num(hardware.my_cord.my_Pro[5].Cord_ST_R, hardware.my_cord.my_Pro[5].Model_Num_R.ToString());
                                break;
                            case 15://判断结果
                                if (left)
                                {
                                    string code = hardware.my_cxr1.Get_data();
                                    bool result = code.Contains(str1) && !string.IsNullOrEmpty(code)
                                         && !string.IsNullOrEmpty(str1); 
                                    if (result)
                                    {

                                        hardware.my_cord.Set_ST_STS(5, 5, "L", true);
                                        hardware.my_cxr1.SavePosToDB(true);
                                    }
                                    else
                                    {
                                        hardware.my_cord.Set_ST_STS(5, 5, "L", false);
                                        hardware.my_cxr1.SavePosToDB(false);
                                    }
                                    string str = "";
                                    str = result == true ? "成功" : "失败";
                                    Logger.Debug($"左工位待打码条码:【{hardware.my_cord.my_Pro[5].Cord_ST_L}】二次扫码结果:【{ hardware.my_cxr1.Get_data()}】比对{str}", 6);
                                }
                                if (right)
                                {
                                    string code = hardware.my_cxr2.Get_data();
                                    bool result = code.Contains(str2) && !string.IsNullOrEmpty(code)
                                         && !string.IsNullOrEmpty(str2);
                                    if (result)
                                    {

                                        hardware.my_cord.Set_ST_STS(5, 5, "R", true);
                                        hardware.my_cxr2.SavePosToDB(true);
                                    }
                                    else
                                    {
                                        hardware.my_cord.Set_ST_STS(5, 5, "R", false);
                                        hardware.my_cxr2.SavePosToDB(false);
                                    }
                                    string str = "";
                                    str = result == true ? "成功" : "失败";
                                    Logger.Debug($"右工位待打码条码:【{hardware.my_cord.my_Pro[5].Cord_ST_R}】二次扫码结果:【{ hardware.my_cxr2.Get_data()}】比对{str}", 6);
                                }
                                m_TH6.Step++;
                                break;
                            case 16:
                                m_TH6.Step_str = "";
                                m_TH6.Step = 0;
                                SysStatus.NO6 = false;
                                break;
                        }
                    }
                    catch(Exception ex)
                    {
                        //异常则判定打码工站NG
                        hardware.my_cord.Set_ST_STS(5, 5, "L", false);
                        hardware.my_cord.Set_ST_STS(5, 5, "R", false);
                        Logger.Debug($"{ex.Message}\n{ex.StackTrace}");

                    }
                    

                }


                Application.DoEvents();
                Thread.Sleep(80);
                TM5 = Environment.TickCount - TM1_;
            }
            Logger.Error("激光打码线程停止");
        }

        /// <summary>
        ///t7 xia
        /// </summary>
        private void Run_NO7()
        {

            int pos1_x = -1;
            int pos2_x = -1;

            ////////////////////////////
            int error_index = 0;
            int error_count = 0;

            while (SysStatus.Status == SysStatus.EmSysSta.Run)
            {

                double TM1_ = Environment.TickCount;
                bool left, right;


                if ((Station[7] && !SysStatus.Shield_NO7) || SysStatus.NO7)
                {
                    if (SysStatus.NO7 && m_TH7.Step == 0)
                        m_TH7.Step = 1;

                    switch (m_TH7.Step)
                    {
                        case 1://等待横移退出
                            m_TH7.Step_str = "等待横移到安全位";
                            m_TH7.Time = 0;
                            if (hardware.my_io.m_input[1, 4] && hardware.my_io.m_input[1, 6])
                                m_TH7.Step++;
                            break;
                        case 2://判断是否有产品
                            m_TH7.Step_str = "判断是否有料";
                            left = hardware.my_io.m_input[4, 16];
                            right = hardware.my_io.m_input[4, 17];
                            pos1_x = -1;
                            pos2_x = -1;
                            if (left || right)
                            {
                                ////左夹抓///////////////////////////////////
                                pos1_x = 5;

                                if (hardware.my_cord.my_Pro[6].STS_L_voltage == false)
                                    pos1_x = 1;

                                else if (hardware.my_cord.my_Pro[6].STS_L_CCD1_P == false || hardware.my_cord.my_Pro[6].STS_L_CCD2_P == false)
                                    pos1_x = 2;

                                else if (hardware.my_cord.my_Pro[6].STS_L_CCD1_H == false || hardware.my_cord.my_Pro[6].STS_L_CCD2_H == false)
                                    pos1_x = 3;

                                else if (hardware.my_cord.my_Pro[6].STS_L_LASER == false || hardware.my_cord.my_Pro[6].STS_L_AIR == false)
                                    pos1_x = 4;
                                /////右夹抓/////////////////////////////////////////

                                pos2_x = 5;

                                if (hardware.my_cord.my_Pro[6].STS_R_voltage == false)
                                    pos2_x = 1;

                                else if (hardware.my_cord.my_Pro[6].STS_R_CCD1_P == false || hardware.my_cord.my_Pro[6].STS_R_CCD2_P == false)
                                    pos2_x = 2;

                                else if (hardware.my_cord.my_Pro[6].STS_R_CCD1_H == false || hardware.my_cord.my_Pro[6].STS_R_CCD2_H == false)
                                    pos2_x = 3;

                                else if (hardware.my_cord.my_Pro[6].STS_R_LASER == false || hardware.my_cord.my_Pro[6].STS_R_AIR == false)
                                    pos2_x = 4;

                                /////////////////////////////////////////////////////
                                ///
                                if (hardware.my_cord.my_Pro[6].Cord_ST_L.Length > 5 && left)
                                {
                                    hardware.my_takes.Set_Cord(hardware.my_cord.my_Pro[6].Cord_ST_L);//写入二维码
                                    hardware.my_takes.Set_M(hardware.my_cord.my_Pro[6].Model_Num_L);//写入模号
                                    bool bb = hardware.my_cord.my_Pro[6].STS_L_voltage && hardware.my_cord.my_Pro[6].STS_L_CCD1_H && hardware.my_cord.my_Pro[6].STS_L_CCD1_P && hardware.my_cord.my_Pro[6].STS_L_CCD2_H && hardware.my_cord.my_Pro[6].STS_L_CCD2_P && hardware.my_cord.my_Pro[6].STS_L_LASER;
                                    hardware.my_takes.Set_ResEnd(bb, hardware.my_cord.my_Pro[6].STS_L_voltage, hardware.my_cord.my_Pro[6].STS_L_CCD1_H,
                                        hardware.my_cord.my_Pro[6].STS_L_CCD1_P, hardware.my_cord.my_Pro[6].STS_L_CCD2_H, hardware.my_cord.my_Pro[6].STS_L_CCD2_P,
                                        hardware.my_cord.my_Pro[6].STS_L_LASER, hardware.my_cord.my_Pro[6].STS_L_AIR
                                        );
                                    hardware.my_takes.SavePosToDB();
                                }

                                if (hardware.my_cord.my_Pro[6].Cord_ST_R.Length > 5 && right)
                                {
                                    hardware.my_takes.Set_Cord(hardware.my_cord.my_Pro[6].Cord_ST_R);//写入二维码
                                    hardware.my_takes.Set_M(hardware.my_cord.my_Pro[6].Model_Num_R);//写入模号
                                    bool bb = hardware.my_cord.my_Pro[6].STS_R_voltage && hardware.my_cord.my_Pro[6].STS_R_CCD1_H && hardware.my_cord.my_Pro[6].STS_R_CCD1_P && hardware.my_cord.my_Pro[6].STS_R_CCD2_H && hardware.my_cord.my_Pro[6].STS_R_CCD2_P && hardware.my_cord.my_Pro[6].STS_R_LASER;
                                    hardware.my_takes.Set_ResEnd(bb, hardware.my_cord.my_Pro[6].STS_R_voltage, hardware.my_cord.my_Pro[6].STS_R_CCD1_H,
                                        hardware.my_cord.my_Pro[6].STS_R_CCD1_P, hardware.my_cord.my_Pro[6].STS_R_CCD2_H, hardware.my_cord.my_Pro[6].STS_R_CCD2_P,
                                        hardware.my_cord.my_Pro[6].STS_R_LASER, hardware.my_cord.my_Pro[6].STS_R_AIR
                                        );
                                    hardware.my_takes.SavePosToDB();
                                }
                                /////////////////////////////////////////////
                                ///
                                 ///////////////////////////////////////////////
                                if (pos1_x < 5)
                                {
                                    if (error_index == pos1_x)
                                        error_count++;
                                    else
                                    {
                                        error_index = pos1_x;
                                        error_count = 0;
                                    }
                                }
                                if (pos2_x < 5)
                                {
                                    if (error_index == pos2_x)
                                        error_count++;
                                    else
                                    {
                                        error_index = pos2_x;
                                        error_count = 0;
                                    }
                                }
                                if (error_count >= 3)
                                {
                                    Stop_TH();//线程停止
                                    Logger.Error($"连续废料！");//写入日志.
                                    error_index = 0;
                                    error_count = 0;
                                }

                                m_TH7.Step++;
                            }
                            else
                                m_TH7.Step = 0;
                            break;
                        case 3://气爪打开
                            m_TH7.Step_str = "打开爪子及上升气缸";
                            hardware.my_io.Set_Do(2, 4, 0);//1
                            hardware.my_io.Set_Do(2, 5, 1);//1 
                            hardware.my_io.Set_Do(2, 6, 0);//1
                            hardware.my_io.Set_Do(2, 7, 1);//1   
                            //上升
                            hardware.my_io.Set_Do(2, 3, 0);//1
                            hardware.my_io.Set_Do(2, 2, 1);//1   
                            m_TH7.Step++;
                            break;
                        case 4://夹爪1 夹爪2 打开
                            m_TH7.Step_str = "等待爪子打开及上升气缸到位";
                            if (hardware.my_io.m_input[2, 10] && hardware.my_io.m_input[2, 12] && hardware.my_io.m_input[2, 13])
                                m_TH7.Step++;
                            break;
                        case 5://电机
                            m_TH7.Step_str = "电机移动到取料位";
                            hardware.my_motion.Move(SysStatus.Axis_5, 0);
                            hardware.my_motion.Move(SysStatus.Axis_6, 0);
                            m_TH7.Step++;
                            break;
                        case 6://等待电机到位
                            m_TH7.Step_str = "等待电机到位及旋转到位";
                            if (hardware.my_motion.Move_Finsh(SysStatus.Axis_5) && hardware.my_motion.Move_Finsh(SysStatus.Axis_6))
                                m_TH7.Step++;
                            else
                            {
                                if (hardware.my_motion.Check_Run(SysStatus.Axis_5) == false || hardware.my_motion.Check_Run(SysStatus.Axis_6) == false)
                                    if (reback[7])//中断恢复
                                        m_TH7.Step--;
                                reback[7] = false;
                            }


                            break;
                        case 7:
                            m_TH7.Step++;

                            break;
                        case 8://气缸下降
                            m_TH7.Step_str = "气缸下降";
                            hardware.my_io.Set_Do(2, 2, 0);//1
                            hardware.my_io.Set_Do(2, 3, 1);//1   
                            m_TH7.Step++;
                            break;
                        case 9://等待气缸到位
                            m_TH7.Step_str = "等待下降气缸到位";
                            if (hardware.my_io.m_input[2, 14])
                                m_TH7.Step++;
                            break;
                        case 10://关闭爪子
                            m_TH7.Step_str = "关闭爪子";
                            hardware.my_io.Set_Do(2, 4, 1);//1
                            hardware.my_io.Set_Do(2, 5, 0);//1   
                            hardware.my_io.Set_Do(2, 6, 1);//1
                            hardware.my_io.Set_Do(2, 7, 0);//1   

                            m_TH7.Step++;
                            break;
                        case 11://等待气爪关闭
                            m_TH7.Step_str = "等待爪子关闭";
                            if (hardware.my_io.m_input[2, 9] && hardware.my_io.m_input[2, 11])
                                m_TH7.Step++;
                            break;
                        case 12://上升
                            m_TH7.Step_str = "气缸上升";
                            hardware.my_io.Set_Do(2, 3, 0);//1
                            hardware.my_io.Set_Do(2, 2, 1);//1   
                            m_TH7.Step++;
                            break;
                        case 13://等待上升到位
                            m_TH7.Step_str = "等待电机运动完成";
                            if (hardware.my_io.m_input[2, 13])
                                m_TH7.Step++;
                            break;
                        case 14:
                            //左边无料进行跳过
                            if (pos1_x < 0)
                                m_TH7.Step = m_TH7.Step + 3;
                            else
                            {
                                //左侧电机定位
                                hardware.my_motion.Move(SysStatus.Axis_5, pos1_x, 788544);
                                if (pos1_x < 5)//异常位置---Y
                                    hardware.my_motion.Move(SysStatus.Axis_6, 1);
                                else//正常放料
                                    hardware.my_motion.Move(SysStatus.Axis_6, 2);
                                m_TH7.Step++;
                            }
                            break;
                        case 15://电机到位等待到位
                            m_TH7.Step_str = "等待电机到位,打开爪子";
                            if (hardware.my_motion.Move_Finsh(SysStatus.Axis_5) && hardware.my_motion.Move_Finsh(SysStatus.Axis_6))
                            {
                                hardware.my_io.Set_Do(2, 4, 0);//1  打开左边爪子
                                hardware.my_io.Set_Do(2, 5, 1);//1   
                                m_TH7.Step++;
                            }
                            else
                            {
                                if (hardware.my_motion.Check_Run(SysStatus.Axis_5) == false || hardware.my_motion.Check_Run(SysStatus.Axis_6) == false)
                                    if (reback[7])//中断恢复
                                        m_TH7.Step--;
                                reback[7] = false;
                            }
                            break;
                        case 16:
                            m_TH7.Step_str = "等待爪子打开到位";
                            if (hardware.my_io.m_input[2, 10])
                                m_TH7.Step++;
                            break;
                        case 17:
                            //右侧无料直接跳过
                            if (pos2_x < 0)
                            {
                                m_TH7.Step = m_TH7.Step + 2;
                            }
                            else
                            {
                                //右能进行定位。
                                hardware.my_motion.Move(SysStatus.Axis_5, pos2_x);
                                if (pos2_x < 5)
                                    hardware.my_motion.Move(SysStatus.Axis_6, 1);
                                else
                                    hardware.my_motion.Move(SysStatus.Axis_6, 2);

                                m_TH7.Step++;
                            }
                            break;
                        case 18://等待电机到位
                            m_TH7.Step_str = "等待电机到位";
                            if (hardware.my_motion.Move_Finsh(SysStatus.Axis_5) && hardware.my_motion.Move_Finsh(SysStatus.Axis_6))
                                m_TH7.Step++;
                            else
                            {
                                if (hardware.my_motion.Check_Run(SysStatus.Axis_5) == false || hardware.my_motion.Check_Run(SysStatus.Axis_6) == false)
                                    if (reback[7])//中断恢复
                                        m_TH7.Step--;
                                reback[7] = false;
                            }
                            break;
                        case 19:
                            m_TH7.Step_str = "打开右边爪子";
                            hardware.my_io.Set_Do(2, 6, 0);//1  打开右边爪子
                            hardware.my_io.Set_Do(2, 7, 1);//1   
                            m_TH7.Step++;
                            break;
                        case 20://右爪子
                            m_TH7.Step_str = "等待爪子到位";
                            if (hardware.my_io.m_input[2, 12])
                            {
                                m_TH7.Step_str = "";
                                m_TH7.Step = 0;
                                SysStatus.NO7 = false;
                            }
                            break;


                    }

                    Thread.Sleep(20);
                }

                Application.DoEvents();
                Thread.Sleep(200);
                TM7 = Environment.TickCount - TM1_;
            }
            Logger.Error("下料线程停止");
        }

        /// <summary>
        ///t8 shou
        /// </summary>
        private void Run_N08()
        {

            while (SysStatus.Status == SysStatus.EmSysSta.Run)
            {

                double TM1_ = Environment.TickCount;

                if ((Station[8] && !SysStatus.Shield_NO8) || SysStatus.NO8)
                {

                    switch (m_TH8.Step)
                    {
                        case 1://判断是否为空
                            m_TH8.Step_str = "判断是否1，2是否为空";

                            if (m_TH2.Step == 0 && m_TH9.Step == 0)
                            {
                                if (hardware.my_io.m_input[1, 2] == false && hardware.my_io.m_input[1, 3] == false)
                                    m_TH8.Step++;
                                else if (hardware.my_io.m_input[1, 2] || hardware.my_io.m_input[1, 3])
                                    if (hardware.my_io.m_input[1, 0] || hardware.my_io.m_input[1, 1])
                                        m_TH8.Step = 7;
                            }
                            break;
                        case 2:// 
                            m_TH8.Step_str = "判断横移是否在安全区域";
                            if (hardware.my_io.m_input[1, 4] && hardware.my_io.m_input[1, 6] && hardware.my_io.m_input[0, 3])
                                m_TH8.Step++;
                            else
                                m_TH8.Step--;
                            break;

                        case 3:
                            if (hardware.my_io.m_input[0, 3] == true)
                                m_TH8.Step++;
                            break;
                        case 4://                                
                            SysStatus.temp_shield_light = 1;
                            m_TH8.Step_str = "打开收料台";
                            hardware.my_io.Set_Do(1, 0, 0);//
                            hardware.my_io.Set_Do(1, 1, 1);//1  
                            m_TH8.Step++;
                            break;
                        case 5://
                            m_TH8.Step_str = "放料气缸是否到位";
                            if (hardware.my_io.m_input[1, 0])
                                m_TH8.Step++;
                            break;
                        case 6://产品到位

                            m_TH8.Step++;
                            break;
                        case 7://等待启动按钮
                            m_TH8.Step_str = "等待启动按钮及光幕退出";
                            if (hardware.my_io.m_input[0, 3] == true && hardware.my_io.m_input[0, 0])//启动
                            {
                                m_TH8.Step++;
                            }
                            if (hardware.my_io.m_input[1, 2] == false && hardware.my_io.m_input[1, 3] == false)
                            {
                                m_TH8.Step = 1;
                            }
                            break;
                        case 8://收料台前推送
                            m_TH8.Step_str = "放料台送到位";
                            hardware.my_io.Set_Do(1, 1, 0);//1
                            hardware.my_io.Set_Do(1, 0, 1);//1   
                            m_TH8.Step++;
                            break;
                        case 9://  
                            m_TH8.Step_str = "等待放料台到位";
                            if (hardware.my_io.m_input[1, 1])
                            {

                                if (SysStatus.CurProductName == "M334")
                                {

                                    hardware.my_cord.Set_Modle("L", SysStatus.Hand_Select_mode_R);
                                    hardware.my_cord.Set_Modle("R", SysStatus.Hand_Select_mode_L);

                                }
                                else
                                {


                                    hardware.my_cord.Set_Modle("L", SysStatus.Hand_Select_mode_L);

                                }
                                if (hardware.my_io.m_input[1, 2] || hardware.my_io.m_input[1, 3])//左
                                {

                                    hardware.my_cord.Set_Cord_ST("L");
                                    if (SysStatus.CurProductName != "M381")
                                        hardware.my_cord.Set_Cord_ST("R");
                                }
                                hardware.my_cord.SaveSTcord();//保存
                                if (SysStatus.Shield_NO9)
                                {
                                    if (m_TH2.Step == 0)//横移
                                    {
                                        m_TH2.Step = 1;
                                    }
                                }
                                else
                                {
                                    if (m_TH9.Step == 0)//启动气压
                                    {
                                        m_TH9.Step = 1;
                                    }
                                }

                                m_TH8.Step = 1;
                            }
                            break;

                    }

                }
                Application.DoEvents();
                Thread.Sleep(80);
                TM7 = Environment.TickCount - TM1_;
            }
            Logger.Error("下料线程停止");
        }


        /// <summary>
        ///气压
        /// </summary>
        private void Run_N09()
        {
            m_TH9.Step = 3;
            while (SysStatus.Status == SysStatus.EmSysSta.Run)
            {

                double TM1_ = Environment.TickCount;


                if ((Station[9] && !SysStatus.Shield_NO9) || SysStatus.NO9)
                {
                    switch (m_TH9.Step)
                    {
                        case 1://判断是否为空并生成新码
                            m_TH9.Step_str = "判断是否1，2是否为空并生成新码";

                            if (hardware.my_io.m_input[0, 13]&& hardware.my_io.m_input[0, 15]&& hardware.my_io.m_input[0, 8])//夹爪打开，取料升降气缸回，
                            {
                                if (hardware.my_io.m_input[1, 2] || hardware.my_io.m_input[1, 3] || Clean_auto>0)
                                {
                                    // 等待取料线程数据准备就绪
                                    if (IsPickupThreadRunning() && GetQueueCount() > 0)
                                    {
                                        // 获取取料线程的数据
                                        bool get_1, get_2;
                                        int mode1_1, mode1_2, mode2_1, mode2_2;
                                        
                                        if (GetPickupData(out get_1, out get_2, out mode1_1, out mode1_2, out mode2_1, out mode2_2))
                                        {
                                            // 生成新码
                                            if (get_1)
                                            {
                                                hardware.my_cord.Set_Modle("L", mode1_1);
                                                hardware.my_cord.Set_Cord_ST("L");//产生新码

                                                hardware.my_cord.Set_Modle("R", mode1_2);
                                                hardware.my_cord.Set_Cord_ST("R");//产生新码

                                                hardware.my_cord.SaveSTcord();
                                            }
                                            else if (get_2)
                                            {
                                                hardware.my_cord.Set_Modle("L", mode2_1);
                                                hardware.my_cord.Set_Cord_ST("L");//产生新码

                                                hardware.my_cord.Set_Modle("R", mode2_2);
                                                hardware.my_cord.Set_Cord_ST("R");//产生新码

                                                hardware.my_cord.SaveSTcord();
                                            }
                                            
                                            // 清除已使用的数据，防止重复使用
                                            ClearPickupData();
                                        }
                                    }
                                    else
                                    {
                                        // 如果取料线程未运行或数据未准备就绪，等待一下再检查
                                        m_TH9.Step_str = "等待取料线程数据准备就绪";
                                        return; // 保持当前步骤，等待下次循环
                                    }
                                    
                                    m_TH9.Step++;
                                }
                                else
                                    m_TH9.Step = 0;

                            }
                            break;
                        case 2:// 
                            m_TH9.Step_str = "判断横移是否在安全区域";
                            if (hardware.my_io.m_input[1, 4] && hardware.my_io.m_input[1, 6] && hardware.my_io.m_input[0, 3])
                                m_TH9.Step++;
                            else
                                m_TH9.Step--;
                            break;
                        case 3://
                            m_TH9.Step_str = "打开后推";
                            hardware.my_io.Set_Do(3, 8, 1);// 
                            hardware.my_io.Set_Do(3, 12, 0);//
                            m_TH9.Step++;
                            break;
                        case 4://
                            m_TH9.Step_str = "等待后推到位";
                            if (hardware.my_io.m_input[3, 2])
                                m_TH9.Step++;

                            break;
                        case 5:
                            m_TH9.Step_str = "打开下压";
                            hardware.my_io.Set_Do(3, 9, 1);//打开下压
                            hardware.my_io.Set_Do(3, 13, 0);//
                            m_TH9.Step++;
                            break;
                        case 6://
                            Thread.Sleep(1100);
                            //  m_TH9.Step_str = "等待下压到位";
                            //  if (!hardware.my_io.m_input[3, 0])
                            //  {
                            m_TH9.Step++;
                            // }
                            break;

                        case 7:
                            m_TH9.Step_str = "打开气压";
                            hardware.my_io.Set_Do(3, 10, 1);//
                            hardware.my_io.Set_Do(3, 11, 1);//   
                            m_TH9.Step++;
                            break;
                        case 8://收料台前推送
                            Thread.Sleep(2000);
                            m_TH9.Step++;
                            break;
                        case 9://  
                            m_TH9.Step_str = "判断流量计";
                            //m_TH9.Step++;
                            Logger.Debug("流量计开始");
                            int leftFlowMeter, rightFlowMeter;
                            // 2. 创建Modbus RTU主站
                            //var adapter = new SerialPortAdapter(port);

                            // 3. 设置设备从站地址（示例地址为1）
                            byte slaveId1 = 1;
                            byte slaveId2 = 2;
                            ushort startAddress = 0x0016; // 高32位起始地址
                            //ushort startAddressLow = 0x0017;  // 低32位起始地址
                            ushort numRegisters = 2;          // 每个部分需要2个16位寄存器(每一个寄存器对应两个字节)

                            try
                            {
                                ushort[] registers1 = hardware._modbusSerialMaster.ReadHoldingRegisters(slaveId1, startAddress, numRegisters);
                                ushort[] registers2 = hardware._modbusSerialMaster.ReadHoldingRegisters(slaveId2, startAddress, numRegisters);

                                //// 5. 转换为32位无符号整数
                                //byte[] bytes = new byte[4];
                                //Buffer.BlockCopy(registers, 0, bytes, 0, 4);
                                //Array.Reverse(bytes); // 处理大端序
                                leftFlowMeter =  (registers1[0] << 16 | registers1[1])/100;
                                rightFlowMeter = (registers2[0] << 16 | registers2[1])/100;
                                hardware.my_meter.leftFlowMeter = leftFlowMeter;
                                hardware.my_meter.rightFlowMeter = rightFlowMeter;
                                Logger.Debug($"左工位流量：{leftFlowMeter};右工位流量：{rightFlowMeter}");

                                if(leftFlowMeter<hardware.my_meter.flowMeterUp&& leftFlowMeter >
                                    hardware.my_meter.flowMeterDown)
                                {
                                    hardware.my_meter.leftResult = "OK";
                                    hardware.my_cord.Set_ST_STS(1, 1, "L", true);
                                }
                                else
                                {
                                    hardware.my_meter.leftResult = "NG";
                                    hardware.my_cord.Set_ST_STS(1, 1, "L", false);
                                    Logger.Error($"左流量未通过！");
                                    m_TH9.Step = 20;
                                }
                                bool leftResultFlag = hardware.my_meter.leftResult == "OK" ? true : false;
                              
                                hardware.my_meter.SaveToDB_Pos(hardware.my_cord.my_Pro[1].Cord_ST_L, leftFlowMeter, leftResultFlag);

                                if (rightFlowMeter < hardware.my_meter.flowMeterUp && rightFlowMeter >
                                    hardware.my_meter.flowMeterDown)
                                {
                                    hardware.my_meter.rightResult = "OK";
                                    hardware.my_cord.Set_ST_STS(1, 1, "R", true);

                                }
                                else
                                {
                                    hardware.my_meter.rightResult = "NG";
                                    hardware.my_cord.Set_ST_STS(1, 1, "R", false);
                                    Logger.Error($"右流量未通过！");
                                    m_TH9.Step = 20;
                                }

                                bool rightResultFlag = hardware.my_meter.rightResult == "OK" ? true : false;
                                hardware.my_meter.SaveToDB_Pos(hardware.my_cord.my_Pro[1].Cord_ST_R, rightFlowMeter, rightResultFlag);
                                m_TH9.Step++;
                                Logger.Debug("流量计结束");
                                break;

                            }
                            catch (Exception ex)
                            {

                                Logger.Error($"错误：{ex.Message+ex.StackTrace}");
                                hardware.my_cord.Set_ST_STS(1, 1, "L", false);
                                hardware.my_cord.Set_ST_STS(1, 1, "R", false);
                                m_TH9.Step = 20;
                                break;
                            }

                            
                        //if (hardware.my_io.m_input[3, 4])
                        //    hardware.my_cord.Set_ST_STS(1, 1, "L", true);
                        //else
                        //{
                        //    hardware.my_cord.Set_ST_STS(1, 1, "L", false);

                        //    Logger.Error($"左流量未通过！");//写入日志.
                        //    m_TH9.Step = 20;
                        //}
                        //if (hardware.my_io.m_input[3, 5])
                        //    hardware.my_cord.Set_ST_STS(1, 1, "R", true);
                        //else
                        //{
                        //    hardware.my_cord.Set_ST_STS(1, 1, "R", false);

                        //    Logger.Error($"右流量未通过！");//写入日志.
                        //    m_TH9.Step = 20;
                        //}


                        case 10:
                            m_TH9.Step_str = "关闭气压";
                            hardware.my_io.Set_Do(3, 10, 0);// 
                            hardware.my_io.Set_Do(3, 11, 0);//
                            m_TH9.Step++;
                            break;
                        case 11:
                            Thread.Sleep(300);
                            m_TH9.Step++;
                            break;
                        case 12:
                            m_TH9.Step_str = "关闭后推";
                            hardware.my_io.Set_Do(3, 12, 1);// 
                            hardware.my_io.Set_Do(3, 8, 0);//
                            m_TH9.Step++;
                            break;
                        case 13:
                            m_TH9.Step_str = "等待后推关";
                            if (hardware.my_io.m_input[3, 3])
                                m_TH9.Step++;
                            break;
                        case 14:
                            m_TH9.Step_str = "关闭侧压";
                            hardware.my_io.Set_Do(3, 9, 0);// 
                            hardware.my_io.Set_Do(3, 13, 1);//
                            m_TH9.Step++;
                            break;

                        case 15:

                            m_TH9.Step_str = "等待侧压关闭";
                            Thread.Sleep(500);

                            m_TH9.Step++;
                            break;



                        case 16:
                            if (m_TH2.Step == 0)//启动横移
                            {
                                if (!SysStatus.Shield_NO2)
                                    m_TH2.Step = 1;
                                m_TH9.Step = 0;
                            }
                            break;

                        case 20:
                            hardware.my_io.Set_Do(3, 10, 0);// 
                            hardware.my_io.Set_Do(3, 11, 0);//
                            Thread.Sleep(100);
                            hardware.my_io.Set_Do(3, 12, 1);// 
                            hardware.my_io.Set_Do(3, 8, 0);//
                            Thread.Sleep(100);
                            hardware.my_io.Set_Do(3, 9, 0);// 
                            hardware.my_io.Set_Do(3, 13, 1);//
                            m_TH9.Step = 0;
                            Stop_TH();//线程停止
                            break;


                    }

                }
                Application.DoEvents();
                Thread.Sleep(80);
                TM9 = Environment.TickCount - TM1_;
            }
            Logger.Error("下料线程停止");
        }

        /// <summary>
        /// 传送带
        /// </summary>
        private void Run_NO10()
        {
            int time_count = 0;
            int time_free = 0;
            while (SysStatus.Status == SysStatus.EmSysSta.Run)
            {
                double TM1_ = Environment.TickCount;
                if ((Station[10] && !SysStatus.Shield_NO10))
                {

                    if (hardware.my_io.m_input[4, 19])
                    {
                        hardware.my_io.Set_Do(0, 2, 1);

                        time_count = 0;
                        time_free = 0;
                    }
                    else
                    {
                        time_free++;
                    }

                    if (hardware.my_io.m_input[4, 18])
                    {
                        time_count++;
                    }
                    else
                    {
                        time_count = 0;

                    }
                    if (time_count > 30)
                    {
                        m_TH7.Step_str = "传送带物料异常 ";
                        hardware.my_io.Set_Do(0, 2, 0);
                        Stop_TH();
                        time_count = 0;
                    }

                    if (time_free > 40)
                    {
                        hardware.my_io.Set_Do(0, 2, 0);
                        time_free = 15;
                    }
                }
                Thread.Sleep(200);
                Application.DoEvents();
                TM10 = Environment.TickCount - TM1_;
            }
        }
        /// <summary>
        ///error
        /// </summary>
        private void Run_NO11()
        {

            while (SysStatus.Status == SysStatus.EmSysSta.Run)
            {

                if ((Station[11] && !SysStatus.Shield_NO11))
                {
                    //    if (SysStatus.outTime < 5 || TH_Error > 0)//超时或有错误 直接关闭

                    double TM1_ = Environment.TickCount;

                    if (task_1 != null && m_TH1.Time <= SysStatus.outTime && SysStatus.Shield_NO1 == false)
                        m_TH1.Time++;//取料
                    if (task_2 != null && m_TH2.Time <= SysStatus.outTime && SysStatus.Shield_NO2 == false)
                        m_TH2.Time++;//横移
                    if (task_3 != null && m_TH3.Time <= SysStatus.outTime && SysStatus.Shield_NO3 == false)
                        m_TH3.Time++;//耐压
                    if (task_4 != null && m_TH4.Time <= SysStatus.outTime && SysStatus.Shield_NO4 == false)
                        m_TH4.Time++;//侧面
                    if (task_5 != null && m_TH5.Time <= SysStatus.outTime && SysStatus.Shield_NO5 == false)
                        m_TH5.Time++;//顶部
                    if (task_6 != null && m_TH6.Time <= SysStatus.outTime && SysStatus.Shield_NO6 == false)
                        m_TH6.Time++;//激光
                    if (task_7 != null && m_TH7.Time <= SysStatus.outTime && SysStatus.Shield_NO7 == false)
                        m_TH7.Time++;//下料

                    if (m_TH1.Time > SysStatus.outTime)
                    {
                        Stop_TH();//线程停止
                        Logger.Error($"取料线程超时");//写入日志.

                    }
                    if (m_TH2.Time > SysStatus.outTime)
                    {
                        Stop_TH();//线程停止
                        Logger.Error($"横移线程超时");//写入日志.

                    }
                    if (m_TH3.Time > SysStatus.outTime)
                    {
                        Stop_TH();//线程停止
                        Logger.Error($"耐压线程超时");//写入日志.

                    }

                    if (m_TH4.Time > SysStatus.outTime)
                    {
                        Stop_TH();//线程停止
                        Logger.Error($"侧CCD检测超时");//写入日志.

                    }


                    if (m_TH5.Time > SysStatus.outTime)
                    {
                        Stop_TH();//线程停止
                        Logger.Error($"顶CCD检测超时");//写入日志.

                    }


                    if (m_TH6.Time > SysStatus.outTime)
                    {
                        Stop_TH();//线程停止
                        Logger.Error($"打码线程超时");//写入日志.

                    }


                    if (m_TH7.Time > SysStatus.outTime)
                    {
                        Stop_TH();//线程停止
                        Logger.Error($"下料线程超时");//写入日志.

                    }



                    Thread.Sleep(1000);
                    Application.DoEvents();

                    TM11 = Environment.TickCount - TM1_;
                }
            }
        }
        #endregion
        private void Run_NO12()
        {
            int sum = 6;
            while (SysStatus.Status == SysStatus.EmSysSta.Run)
            {
                double TM1_ = Environment.TickCount;
                if (Station[12])
                {

                    switch (m_TH12.Step)
                    {
                        case 1://抓料 复位
                            sum = 6;
                            m_TH12.Step++;
                            break;
                        case 2:
                            if (sum > 0 && m_TH8.Step == 7)
                                m_TH12.Step++;
                            break;
                        case 3:
                            if (m_TH8.Step == 7)
                            {
                                m_TH12.Step++;
                                sum--;

                            }
                            break;

                    }

                }
                Application.DoEvents();
                Thread.Sleep(80);
                TM7 = Environment.TickCount - TM1_;
            }
            Logger.Error("下料线程停止");
        }


        public void Set_Auto_Clean(int flag)
        {
            Clean_auto = flag;

        }
        #region 各工站结果  触发状态  屏蔽状态 模号
        public double TM1, TM2, TM3, TM4, TM5, TM6, TM7, TM8, TM9, TM10, TM11, TM12, TM20;
        /// <summary>
        /// 状态初始化
        /// </summary>
        public void InitStar()
        {
            Station[1] = Station[2] = Station[3] = Station[4] = Station[5] = Station[6] = Station[7] = Station[8] = Station[9] = Station[10] = Station[12] = true;
            SysStatus.NO1 = SysStatus.NO2 = SysStatus.NO3 = SysStatus.NO4 = SysStatus.NO5 = false;
            SysStatus.NO6 = SysStatus.NO7 = SysStatus.NO8 = SysStatus.NO9 = false;

        }

        /// <summary>
        /// 线程
        /// </summary>
        private bool[] Station = new bool[15];
        /// <summary>
        /// 屏蔽
        /// </summary>
        private bool[] Shield = new bool[15];
        /// <summary>
        /// 中断恢复
        /// </summary>
        private bool[] reback = new bool[15];

        #endregion



    }
}




