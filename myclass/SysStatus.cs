using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

using MyLib.Param;
using MyLib.Users;
using MyLib.Files;

namespace CXPro001.myclass
{/// <summary>
/// 系统状态
/// </summary>
    public static class SysStatus
    {
        #region 手动控制线程
        /// <summary>
        /// 取料
        /// </summary>
        static public bool NO1 = false;
        /// <summary>
        /// 横移
        /// </summary>
        static public bool NO2 = false;
        /// <summary>
        /// 耐压
        /// </summary>
        static public bool NO3 = false;
        /// <summary>
        /// 插口相机
        /// </summary>
        static public bool NO4 = false;
        /// <summary>
        /// PCCB相机
        /// </summary>
        static public bool NO5 = false;
        /// <summary>
        /// 激光
        /// </summary>
        static public bool NO6 = false;
        /// <summary>
        /// 下料
        /// </summary>
        static public bool NO7 = false;
        /// <summary>
        /// 人工
        /// </summary>
        static public bool NO8 = false;
        /// <summary>
        /// 气压 
        /// </summary>
        static public bool NO9 = false;
        /// <summary>
        /// 传送带
        /// </summary>
        static public bool NO10 = false;
        /// <summary>
        /// 超时
        /// </summary>
        static public bool NO11 = false;
        /// <summary>
        /// 下料
        /// </summary>
        static public bool NO12 = false;
        /// <summary>
        /// 保留
        /// </summary>
        static public bool NO13 = false;
        #endregion
        #region 屏蔽线程
        /// <summary>
        /// 屏蔽1
        /// </summary>
        static public bool Shield_NO1 = false;
        /// <summary>
        /// 屏蔽2
        /// </summary>
        static public bool Shield_NO2 = false;
        /// <summary>
        /// 屏蔽3
        /// </summary>
        static public bool Shield_NO3 = false;
        /// <summary>
        /// 屏蔽4
        /// </summary>
        static public bool Shield_NO4 = false;
        /// <summary>
        /// 屏蔽4-P
        /// </summary>
        static public bool Shield_NO4_P = false;
        /// <summary>
        /// 屏蔽4-H
        /// </summary>
        static public bool Shield_NO4_H = false;
        /// <summary>
        /// 屏蔽5
        /// </summary>
        static public bool Shield_NO5 = false;
        /// <summary>
        /// 屏蔽5-P
        /// </summary>
        static public bool Shield_NO5_P = false;
        /// <summary>
        /// 屏蔽5-H
        /// </summary>
        static public bool Shield_NO5_H = false;
        /// <summary>
        /// 屏蔽6
        /// </summary>
        static public bool Shield_NO6 = false;
        /// <summary>
        /// 屏蔽7
        /// </summary>
        static public bool Shield_NO7 = false;
        /// <summary>
        /// 屏蔽8
        /// </summary>
        static public bool Shield_NO8 = false;
        /// <summary>
        /// 屏蔽9
        /// </summary>
        static public bool Shield_NO9 = false;
        /// <summary>
        /// 屏蔽10
        /// </summary>
        static public bool Shield_NO10 = false;
        /// <summary>
        /// 屏蔽11
        /// </summary>
        static public bool Shield_NO11 = false;
        /// <summary>
        /// 屏蔽12
        /// </summary>
        static public bool Shield_NO12 = false;
        /// <summary>
        /// 屏蔽13
        /// </summary>
        static public bool Shield_NO13 = false;
        #endregion
        #region 初始化线程
        /// <summary>
        /// 初始化1
        /// </summary>
        static public int TH_INI_NO1 = 0;
        /// <summary>
        /// 初始化2
        /// </summary>
        static public int TH_INI_NO2 = 0;
        /// <summary>
        /// 初始化3
        /// </summary>
        static public int TH_INI_NO3 = 0;
        /// <summary>
        /// 初始化4
        /// </summary>
        static public int TH_INI_NO4 = 0;
        /// <summary>
        /// 初始化5
        /// </summary>
        static public int TH_INI_NO5 = 0;
        /// <summary>
        /// 初始化6
        /// </summary>
        static public int TH_INI_NO6 = 0;
        /// <summary>
        /// 初始化7
        /// </summary>
        static public int TH_INI_NO7 = 0;
        /// <summary>
        /// 初始化8
        /// </summary>
        static public int TH_INI_NO8 = 0;
        /// <summary>
        /// 初始化9
        /// </summary>
        static public int TH_INI_NO9 = 0;
        /// <summary>
        /// 初始化10
        /// </summary>
        static public int TH_INI_NO10 = 0;
        /// <summary>
        /// 初始化11
        /// </summary>
        static public int TH_INI_NO11 = 0;
        /// <summary>
        /// 初始化12
        /// </summary>
        static public int TH_INI_NO12 = 0;
        /// <summary>
        /// 初始化13
        /// </summary>
        static public int TH_INI_NO13 = 0;
        #endregion

        #region 系统状态

        /// <summary>
        /// 系统状态定义
        /// </summary>
        public enum EmSysSta
        {
            /// <summary>
            /// 未知
            /// </summary>
            [Description("未知")]
            Unkown,
            /// <summary>
            /// 就绪
            /// </summary>
            [Description("就绪")]
            Standby,
            /// <summary>
            /// 运行
            /// </summary>
            [Description("运行")]
            Run,
            /// <summary>
            /// 警告
            /// </summary>
            [Description("警告")]
            Warning,
            /// <summary>
            /// 错误
            /// </summary>
            [Description("错误")]
            Err,
            /// <summary>
            /// 暂停
            /// </summary>
            [Description("暂停")]
            Pause,
            /// <summary>
            /// 复位
            /// </summary>
            [Description("复位")]
            Reset,
            /// <summary>
            /// 急停
            /// </summary>
            [Description("急停")]
            Emg,
            /// <summary>
            /// 待料
            /// </summary>
            [Description("待料")]
            Wait,
            /// <summary>
            /// 维护
            /// </summary>
            [Description("维护")]
            Maintain
        }

        #endregion

   

        #region 系统信息

        /// <summary>
        /// 当前用户
        /// </summary>
        public static User CurUser= new User();

        /// <summary>
        /// 用户列表
        /// </summary>
        [Param("用户列表", "用户列表", "系统", ParamAttribute.Config.STbyAdminSys)]
        public static List<User> ListUser = new List<User>();

        /// <summary>
        /// 手持扫码枪扫到的二维码--扫到码就更新
        /// </summary>
        public static string CordGun;
        /// <summary>
        /// 手持扫码枪扫到的二维码--作业中不能更新
        /// </summary>
        public static string CordGun1;
        /// <summary>
        /// 手持扫码枪扫码才能启动
        /// </summary>
        public static bool StarEnb;

        /// <summary>
        /// 当前产品名
        /// </summary>
        [Param("当前产品", "当前产品", "系统")]
        public static string CurProductName
        { 
            get; 
            set; 
        }

        /// <summary>
        /// 取料轴
        /// </summary>
        public const short Axis_1 = 5;
        /// <summary>
        /// 横移轴
        /// </summary>
        public const short Axis_2 = 1;
        /// <summary>
        /// ccd1
        /// </summary>
        public const short Axis_3 = 4;
        /// <summary>
        /// CCD2
        /// </summary>
        public const short Axis_4 =3;
        /// <summary>
        /// 下料X
        /// </summary>
        public const short Axis_5 = 2;
        /// <summary>
        /// 下料Y
        /// </summary>
        public const short Axis_6 = 6;
        /// <summary>
        /// //电机正限
        /// </summary>
        public static int[] Axis_Soft_Limt_Z=new int[6];
        /// <summary>
        /// 电机负限
        /// </summary>
        public static int[] Axis_Soft_Limt_F = new int[6];
        /// <summary>
        /// 轴1
        /// </summary>
        public static int[] Axis_1_Pos = new int[6];
        /// <summary>
        /// 轴2
        /// </summary>
        public static int[] Axis_2_Pos = new int[6];
        /// <summary>
        /// 轴3
        /// </summary>
        public static int[] Axis_3_Pos = new int[6];
        /// <summary>
        /// 轴4
        /// </summary>
        public static int[] Axis_4_Pos = new int[6];
        /// <summary>
        ///轴5
        /// </summary>
        public static int[] Axis_5_Pos = new int[6];
        /// <summary>
        /// 轴6
        /// </summary>
        public static int[] Axis_6_Pos = new int[6];
        /// <summary>
        /// 速度
        /// </summary>
        public static long[] Axis_SP = new long[6];

        /// <summary>
        /// 防护区负
        /// </summary>
        public static int[] Axis_Protect_F = new int[6];
        /// <summary>
        /// 防护区正
        /// </summary>
        public static int[] Axis_Protect_Z = new int[6];
        /// <summary>
        /// 人工/自动/清料 123
        /// </summary>
        public static int Modle;
        /// <summary>
        /// 超时时间
        /// </summary>
        public static int outTime;
        /// <summary>
        /// 碰撞保护
        /// </summary>
        public static int Protect;
        /// <summary>
        /// 屏蔽光幕
        /// </summary>
        public static int shield_light;
        /// <summary>
        /// 临时屏蔽光幕人工上料用
        /// </summary>
        public static int temp_shield_light;
        /// <summary>
        /// 屏蔽门
        /// </summary>
        public static int shield_door;
        /// <summary>
        /// 路径
        /// </summary>
        public static string sys_dir_path;
        /// <summary>
        /// 手动选择模号
        /// </summary>
        public static int Hand_Select_mode_L=1;
        /// <summary>
        /// 手动选择模号
        /// </summary>
        public static int Hand_Select_mode_R = 2;
        /// <summary>
        /// 使能蜂鸣器
        /// </summary>
        [Param("蜂鸣器使能", "是否启用蜂鸣器", "系统", ParamAttribute.Config.STbyEngineerSys)]
        public static bool BeepEnable;

        /// <summary>
        /// 蜂鸣器时间(ms)
        /// </summary>
        [Param("蜂鸣时间(ms)", "蜂鸣器蜂鸣时长(ms)", "系统", ParamAttribute.Config.STbyEngineerSys)]
        public static int BeepTimeMs;

        /// <summary>
        /// 使能触摸屏操作
        /// </summary>
        [Param("触摸屏使能", "是否启用触摸屏启动设备，禁用时只能通过按键启动", "系统", ParamAttribute.Config.STbyEngineerSys)]
        public static bool TouchEnable;

        /// <summary>
        /// 当前运行模式
        /// </summary>
        //public static EmSysMode Mode;

        /// <summary>
        /// 当前状态
        /// </summary>
        public static EmSysSta Status { get; set; }

        /// <summary>
        /// 退出运行标记/true:退出运行，false:不退出运行
        /// </summary>
        public static bool QuitFlag;

        /// <summary>
        /// 程序关闭标记
        /// </summary>
        public static bool CloseFlag;

        /// <summary>
        /// 程序暂停标记
        /// </summary>
        public static bool PauseFlag;

        /// <summary>
        /// 背光常亮
        /// </summary>
        public static bool BackLightAllOn;

        /// <summary>
        /// 系统配置文件路径
        /// </summary>
        [Param("系统配置路径", "系统配置的存取相对路径", "系统", ParamAttribute.Config.STbySuperuserSys)]
        public static string SysCfgPath
        {
            get { return _sysCfgPath; }
            set
            {
                _sysCfgPath = value;
                //目录不存在则创建
                var path = Path.GetDirectoryName(_sysCfgPath);
                if (path?.Length > 0 && !Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
        }
        public static string SysCfgFullPath => Path.GetFullPath(SysCfgPath);
        private static string _sysCfgPath= @"../syscfg/syscfg.db";

        /// <summary>
        /// 产品路径
        /// </summary>
        [Param("产品配置路径", "产品配置的存取相对路径", "系统", ParamAttribute.Config.STbySuperuserSys)]
        public static string ProductPath 
        {
            get { return _productPath; }
            set
            {
                _productPath = value;
                //目录不存在则创建
                var path = Path.GetDirectoryName(_productPath);
                if (path?.Length > 0 && !Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
        }
        public static string ProductFullPath => Path.GetFullPath(ProductPath);
        private static string _productPath = @"../product/";

        /// <summary>
        /// 当前产品路径
        /// </summary>
        public static string CurProductPath => $@"{_productPath}{CurProductName}/";


        /// <summary>
        /// 当前产品配置文件路径
        /// </summary>
        public static string CurProductCfgPath => $@"{CurProductPath}productCfg.db";

        #region 系统设置
        /// <summary>
        /// 读取设备配置信息
        /// </summary>
        /// <param name="filename"></param>
        public static void LoadConfig(string filename = "")
        {
           string  path = filename+"\\syscfg\\SysStatus.ini";// 配置文件路径

            IniFile inf = new IniFile(path);//确认路径是否存在，不存在则创建文件夹。             
            string STEP = "基本信息";
            CurProductName = inf.ReadString(STEP, "CurProductName", CurProductName);//产品型号 

            STEP = "正限";
            Axis_Soft_Limt_Z[0] = inf.ReadInteger(STEP, "AXIS_SOFT_LIMT_Z_1",0);//限位 
            Axis_Soft_Limt_Z[1] = inf.ReadInteger(STEP, "AXIS_SOFT_LIMT_Z_2", 0);//限位 
            Axis_Soft_Limt_Z[2] = inf.ReadInteger(STEP, "AXIS_SOFT_LIMT_Z_3", 0);//限位 
            Axis_Soft_Limt_Z[3] = inf.ReadInteger(STEP, "AXIS_SOFT_LIMT_Z_4", 0);//限位 
            Axis_Soft_Limt_Z[4] = inf.ReadInteger(STEP, "AXIS_SOFT_LIMT_Z_5", 0);//限位 
            Axis_Soft_Limt_Z[5] = inf.ReadInteger(STEP, "AXIS_SOFT_LIMT_Z_6", 0);//限位 
            STEP = "负限";
            Axis_Soft_Limt_F[0] = inf.ReadInteger(STEP, "AXIS_SOFT_LIMT_F_1", 0);//限位 
            Axis_Soft_Limt_F[1] = inf.ReadInteger(STEP, "AXIS_SOFT_LIMT_F_2", 0);//限位 
            Axis_Soft_Limt_F[2] = inf.ReadInteger(STEP, "AXIS_SOFT_LIMT_F_3", 0);//限位 
            Axis_Soft_Limt_F[3] = inf.ReadInteger(STEP, "AXIS_SOFT_LIMT_F_4", 0);//限位 
            Axis_Soft_Limt_F[4] = inf.ReadInteger(STEP, "AXIS_SOFT_LIMT_F_5", 0);//限位 
            Axis_Soft_Limt_F[5] = inf.ReadInteger(STEP, "AXIS_SOFT_LIMT_F_6", 0);//限位 
            STEP = "轴1";
            Axis_1_Pos[0]= inf.ReadInteger(STEP, "AXIS_1_POS_1", 0);//位置1 
            Axis_1_Pos[1] = inf.ReadInteger(STEP, "AXIS_1_POS_2", 0);//位置1 
            Axis_1_Pos[2] = inf.ReadInteger(STEP, "AXIS_1_POS_3", 0);//位置1 
            Axis_1_Pos[3] = inf.ReadInteger(STEP, "AXIS_1_POS_4", 0);//位置1 
            Axis_1_Pos[4] = inf.ReadInteger(STEP, "AXIS_1_POS_5", 0);//位置1 
            Axis_1_Pos[5] = inf.ReadInteger(STEP, "AXIS_1_POS_6", 0);//位置1 

            STEP = "轴2";
            Axis_2_Pos[0] = inf.ReadInteger(STEP, "AXIS_2_POS_1", 0);//位置2
            Axis_2_Pos[1] = inf.ReadInteger(STEP, "AXIS_2_POS_2", 0);//位置2 
            Axis_2_Pos[2] = inf.ReadInteger(STEP, "AXIS_2_POS_3", 0);//位置2 
            Axis_2_Pos[3] = inf.ReadInteger(STEP, "AXIS_2_POS_4", 0);//位置2 
            Axis_2_Pos[4] = inf.ReadInteger(STEP, "AXIS_2_POS_5", 0);//位置2 
            Axis_2_Pos[5] = inf.ReadInteger(STEP, "AXIS_2_POS_6", 0);//位置2
            STEP = "轴3";
            Axis_3_Pos[0] = inf.ReadInteger(STEP, "AXIS_3_POS_1", 0);//位置3
            Axis_3_Pos[1] = inf.ReadInteger(STEP, "AXIS_3_POS_2", 0);//位置3 
            Axis_3_Pos[2] = inf.ReadInteger(STEP, "AXIS_3_POS_3", 0);//位置3 
            Axis_3_Pos[3] = inf.ReadInteger(STEP, "AXIS_3_POS_4", 0);//位置3 
            Axis_3_Pos[4] = inf.ReadInteger(STEP, "AXIS_3_POS_5", 0);//位置3 
            Axis_3_Pos[5] = inf.ReadInteger(STEP, "AXIS_3_POS_6", 0);//位置3

            STEP = "轴4";
            Axis_4_Pos[0] = inf.ReadInteger(STEP, "AXIS_4_POS_1", 0);//位置4
            Axis_4_Pos[1] = inf.ReadInteger(STEP, "AXIS_4_POS_2", 0);//位置4 
            Axis_4_Pos[2] = inf.ReadInteger(STEP, "AXIS_4_POS_3", 0);//位置4 
            Axis_4_Pos[3] = inf.ReadInteger(STEP, "AXIS_4_POS_4", 0);//位置4 
            Axis_4_Pos[4] = inf.ReadInteger(STEP, "AXIS_4_POS_5", 0);//位置4 
            Axis_4_Pos[5] = inf.ReadInteger(STEP, "AXIS_4_POS_6", 0);//位置4

            STEP = "轴5";
            Axis_5_Pos[0] = inf.ReadInteger(STEP, "AXIS_5_POS_1", 0);//位置5
            Axis_5_Pos[1] = inf.ReadInteger(STEP, "AXIS_5_POS_2", 0);//位置5 
            Axis_5_Pos[2] = inf.ReadInteger(STEP, "AXIS_5_POS_3", 0);//位置5 
            Axis_5_Pos[3] = inf.ReadInteger(STEP, "AXIS_5_POS_4", 0);//位置5 
            Axis_5_Pos[4] = inf.ReadInteger(STEP, "AXIS_5_POS_5", 0);//位置5 
            Axis_5_Pos[5] = inf.ReadInteger(STEP, "AXIS_5_POS_6", 0);//位置5

            STEP = "轴6";
            Axis_6_Pos[0] = inf.ReadInteger(STEP, "AXIS_6_POS_1", 0);//位置5
            Axis_6_Pos[1] = inf.ReadInteger(STEP, "AXIS_6_POS_2", 0);//位置5 
            Axis_6_Pos[2] = inf.ReadInteger(STEP, "AXIS_6_POS_3", 0);//位置5 
            Axis_6_Pos[3] = inf.ReadInteger(STEP, "AXIS_6_POS_4", 0);//位置5 
            Axis_6_Pos[4] = inf.ReadInteger(STEP, "AXIS_6_POS_5", 0);//位置5 
            Axis_6_Pos[5] = inf.ReadInteger(STEP, "AXIS_6_POS_6", 0);//位置5
            STEP = "速度";
            Axis_SP[0] = inf.ReadInteger(STEP, "AXIS_1_SP", 0);//速度1
            Axis_SP[1] = inf.ReadInteger(STEP, "AXIS_2_SP", 0);//速度1
            Axis_SP[2] = inf.ReadInteger(STEP, "AXIS_3_SP", 0);//速度1
            Axis_SP[3] = inf.ReadInteger(STEP, "AXIS_4_SP", 0);//速度1
            Axis_SP[4] = inf.ReadInteger(STEP, "AXIS_5_SP", 0);//速度1 
            Axis_SP[5] = inf.ReadInteger(STEP, "AXIS_6_SP", 0);//速度1

            STEP = "防护区F";
            Axis_Protect_F[0] = inf.ReadInteger(STEP, "AXIS_1_PROTECT", 0);//防护区
            Axis_Protect_F[1] = inf.ReadInteger(STEP, "AXIS_2_PROTECT", 0);//防护区
            Axis_Protect_F[2] = inf.ReadInteger(STEP, "AXIS_3_PROTECT", 0);//防护区
            Axis_Protect_F[3] = inf.ReadInteger(STEP, "AXIS_4_PROTECT", 0);//防护区
            Axis_Protect_F[4] = inf.ReadInteger(STEP, "AXIS_5_PROTECT", 0);//防护区
            Axis_Protect_F[5] = inf.ReadInteger(STEP, "AXIS_6_PROTECT", 0);//防护区

            STEP = "防护区Z";
            Axis_Protect_Z[0] = inf.ReadInteger(STEP, "AXIS_1_PROTECT", 0);//防护区
            Axis_Protect_Z[1] = inf.ReadInteger(STEP, "AXIS_2_PROTECT", 0);//防护区
            Axis_Protect_Z[2] = inf.ReadInteger(STEP, "AXIS_3_PROTECT", 0);//防护区
            Axis_Protect_Z[3] = inf.ReadInteger(STEP, "AXIS_4_PROTECT", 0);//防护区
            Axis_Protect_Z[4] = inf.ReadInteger(STEP, "AXIS_5_PROTECT", 0);//防护区
            Axis_Protect_Z[5] = inf.ReadInteger(STEP, "AXIS_6_PROTECT", 0);//防护区

            STEP = "参数";
            Modle = inf.ReadInteger(STEP, "hand", 0);//人工上料
            outTime = inf.ReadInteger(STEP, "outTime", 120);//超时时间
            Protect = inf.ReadInteger(STEP,"protect",0);
            shield_light = inf.ReadInteger(STEP, "shield_light", 0);
            shield_door = inf.ReadInteger(STEP, "shield_door", 0);
        }

        /// <summary>
        /// 保存设备名称
        /// </summary>
        /// <param name="filename"></param>
        public static void SaveConfig(string filename)
        {

            string path = filename+"\\syscfg\\SysStatus.ini";// 配置文件路径
            IniFile inf = new IniFile(path);//确认路径是否存在，不存在则创建文件夹。            
            string STEP = "基本信息";
            inf.WriteString(STEP, "CurProductName", CurProductName);
            ////////////
            STEP = "正限";
            inf.WriteInteger(STEP, "AXIS_SOFT_LIMT_Z_1", Axis_Soft_Limt_Z[0]);//限位 
            inf.WriteInteger(STEP, "AXIS_SOFT_LIMT_Z_2", Axis_Soft_Limt_Z[1]);//限位 
            inf.WriteInteger(STEP, "AXIS_SOFT_LIMT_Z_3", Axis_Soft_Limt_Z[2]);//限位 
            inf.WriteInteger(STEP, "AXIS_SOFT_LIMT_Z_4", Axis_Soft_Limt_Z[3]);//限位 
            inf.WriteInteger(STEP, "AXIS_SOFT_LIMT_Z_5", Axis_Soft_Limt_Z[4]);//限位 
            inf.WriteInteger(STEP, "AXIS_SOFT_LIMT_Z_6", Axis_Soft_Limt_Z[5]);//限位 
            STEP = "负限";
            inf.WriteInteger(STEP, "AXIS_SOFT_LIMT_F_1", Axis_Soft_Limt_F[0]);//限位 
            inf.WriteInteger(STEP, "AXIS_SOFT_LIMT_F_2", Axis_Soft_Limt_F[1]);//限位 
            inf.WriteInteger(STEP, "AXIS_SOFT_LIMT_F_3", Axis_Soft_Limt_F[2]);//限位 
            inf.WriteInteger(STEP, "AXIS_SOFT_LIMT_F_4", Axis_Soft_Limt_F[3]);//限位 
            inf.WriteInteger(STEP, "AXIS_SOFT_LIMT_F_5", Axis_Soft_Limt_F[4]);//限位 
            inf.WriteInteger(STEP, "AXIS_SOFT_LIMT_F_6", Axis_Soft_Limt_F[5]);//限位 
            STEP = "轴1";
            inf.WriteInteger(STEP, "AXIS_1_POS_1", Axis_1_Pos[0]);//位置1 
            inf.WriteInteger(STEP, "AXIS_1_POS_2", Axis_1_Pos[1]);//位置1 
            inf.WriteInteger(STEP, "AXIS_1_POS_3", Axis_1_Pos[2]);//位置1 
            inf.WriteInteger(STEP, "AXIS_1_POS_4", Axis_1_Pos[3]);//位置1 
            inf.WriteInteger(STEP, "AXIS_1_POS_5", Axis_1_Pos[4]);//位置1 
            inf.WriteInteger(STEP, "AXIS_1_POS_6", Axis_1_Pos[5]);//位置1 
            STEP = "轴2";
            inf.WriteInteger(STEP, "AXIS_2_POS_1", Axis_2_Pos[0]);//位置2
            inf.WriteInteger(STEP, "AXIS_2_POS_2", Axis_2_Pos[1]);//位置2 
            inf.WriteInteger(STEP, "AXIS_2_POS_3", Axis_2_Pos[2]);//位置2 
            inf.WriteInteger(STEP, "AXIS_2_POS_4", Axis_2_Pos[3]);//位置2 
            inf.WriteInteger(STEP, "AXIS_2_POS_5", Axis_2_Pos[4]);//位置2 
            inf.WriteInteger(STEP, "AXIS_2_POS_6", Axis_2_Pos[5]);//位置2
            STEP = "轴3";
            inf.WriteInteger(STEP, "AXIS_3_POS_1", Axis_3_Pos[0]);//位置3
            inf.WriteInteger(STEP, "AXIS_3_POS_2", Axis_3_Pos[1]);//位置3 
            inf.WriteInteger(STEP, "AXIS_3_POS_3", Axis_3_Pos[2]);//位置3 
            inf.WriteInteger(STEP, "AXIS_3_POS_4", Axis_3_Pos[3]);//位置3 
            inf.WriteInteger(STEP, "AXIS_3_POS_5", Axis_3_Pos[4]);//位置3 
            inf.WriteInteger(STEP, "AXIS_3_POS_6", Axis_3_Pos[5]);//位置3 

            STEP = "轴4";
            inf.WriteInteger(STEP, "AXIS_4_POS_1", Axis_4_Pos[0]);//位置3
            inf.WriteInteger(STEP, "AXIS_4_POS_2", Axis_4_Pos[1]);//位置3 
            inf.WriteInteger(STEP, "AXIS_4_POS_3", Axis_4_Pos[2]);//位置3 
            inf.WriteInteger(STEP, "AXIS_4_POS_4", Axis_4_Pos[3]);//位置3 
            inf.WriteInteger(STEP, "AXIS_4_POS_5", Axis_4_Pos[4]);//位置3 
            inf.WriteInteger(STEP, "AXIS_4_POS_6", Axis_4_Pos[5]);//位置3 

            STEP = "轴5";
            inf.WriteInteger(STEP, "AXIS_5_POS_1", Axis_5_Pos[0]);//位置3
            inf.WriteInteger(STEP, "AXIS_5_POS_2", Axis_5_Pos[1]);//位置3 
            inf.WriteInteger(STEP, "AXIS_5_POS_3", Axis_5_Pos[2]);//位置3 
            inf.WriteInteger(STEP, "AXIS_5_POS_4", Axis_5_Pos[3]);//位置3 
            inf.WriteInteger(STEP, "AXIS_5_POS_5", Axis_5_Pos[4]);//位置3 
            inf.WriteInteger(STEP, "AXIS_5_POS_6", Axis_5_Pos[5]);//位置3 

            STEP = "轴6";
            inf.WriteInteger(STEP, "AXIS_6_POS_1", Axis_6_Pos[0]);//位置3
            inf.WriteInteger(STEP, "AXIS_6_POS_2", Axis_6_Pos[1]);//位置3 
            inf.WriteInteger(STEP, "AXIS_6_POS_3", Axis_6_Pos[2]);//位置3 
            inf.WriteInteger(STEP, "AXIS_6_POS_4", Axis_6_Pos[3]);//位置3 
            inf.WriteInteger(STEP, "AXIS_6_POS_5", Axis_6_Pos[4]);//位置3 
            inf.WriteInteger(STEP, "AXIS_6_POS_6", Axis_6_Pos[5]);//位置3 

            STEP = "速度";
            inf.WriteInteger(STEP, "AXIS_1_SP", Axis_SP[0]);//速度
            inf.WriteInteger(STEP, "AXIS_2_SP", Axis_SP[1]);//速度 
            inf.WriteInteger(STEP, "AXIS_3_SP", Axis_SP[2]);//速度 
            inf.WriteInteger(STEP, "AXIS_4_SP", Axis_SP[3]);// 
            inf.WriteInteger(STEP, "AXIS_5_SP", Axis_SP[4]);// 
            inf.WriteInteger(STEP, "AXIS_6_SP", Axis_SP[5]);// 

            STEP = "防护区F";
            inf.WriteInteger(STEP, "AXIS_1_PROTECT", Axis_Protect_F[0]);//防护区
            inf.WriteInteger(STEP, "AXIS_2_PROTECT", Axis_Protect_F[1]);//防护区 
            inf.WriteInteger(STEP, "AXIS_3_PROTECT", Axis_Protect_F[2]);//防护区 
            inf.WriteInteger(STEP, "AXIS_4_PROTECT", Axis_Protect_F[3]);// 
            inf.WriteInteger(STEP, "AXIS_5_PROTECT", Axis_Protect_F[4]);// 
            inf.WriteInteger(STEP, "AXIS_6_PROTECT", Axis_Protect_F[5]);// 

            STEP = "防护区Z";
            inf.WriteInteger(STEP, "AXIS_1_PROTECT", Axis_Protect_Z[0]);//防护区
            inf.WriteInteger(STEP, "AXIS_2_PROTECT", Axis_Protect_Z[1]);//防护区 
            inf.WriteInteger(STEP, "AXIS_3_PROTECT", Axis_Protect_Z[2]);//防护区 
            inf.WriteInteger(STEP, "AXIS_4_PROTECT", Axis_Protect_Z[3]);// 
            inf.WriteInteger(STEP, "AXIS_5_PROTECT", Axis_Protect_Z[4]);// 
            inf.WriteInteger(STEP, "AXIS_6_PROTECT", Axis_Protect_Z[5]);// 

            STEP = "参数"; 
            inf.WriteInteger(STEP, "hand", Modle);//人工模式
            inf.WriteInteger(STEP, "outTime", outTime);//超时时间
            inf.WriteInteger(STEP, "protect", Protect);//碰撞保护 
            inf.WriteInteger(STEP, "shield_light", shield_light);//碰撞保护 
            inf.WriteInteger(STEP, "shield_door", shield_door);//碰撞保护 
        }

        #endregion

        #endregion

       

    }
}
