using MyLib.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLib.Sys;
using System.IO;
using MyLib.Files;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

using System.Data;
using MyLib.SQLiteHelper;
 
namespace CXPro001.myclass
{
    /// <summary>
    /// 工位结果
    /// </summary>
    public class StaRes
    {
        /// <summary>
        /// 手持扫码枪扫的二维码
        /// </summary>
        public string ManualCord="";
        #region 模号检测结果
        /// <summary>
        /// 模号检测结果
        /// </summary>
        public bool sresult1=false;
        /// <summary>
        /// 模号
        /// </summary>
        public string MoNum=" ";
        #endregion
        #region 平整度检测结果
        /// <summary>
        /// 平整度检测结果
        /// </summary>
        public bool sresult2 = false;
        /// <summary>
        /// 平整度数据1
        /// </summary>
        public double Tdat1=0;
        /// <summary>
        /// 平整度数据2
        /// </summary>
        public double Tdat2=0;
        /// <summary>
        /// 平整度数据3
        /// </summary>
        public double Tdat3=0;
        /// <summary>
        /// 平整度数据4
        /// </summary>
        public double Tdat4 = 0;
        /// <summary>
        /// 平整度数据5
        /// </summary>
        public double Tdat5 = 0;
        /// <summary>
        /// 平整度数据6
        /// </summary>
        public double Tdat6 = 0;
        #endregion
        #region 耐压仪检测结果
        /// <summary>
        /// 耐压检测结果
        /// </summary>
        public bool sresult3=false;
        /// <summary>
        /// 耐压仪数据电流1
        /// </summary>
        public double Adat1 = 0;
        /// <summary>
        /// 耐压仪数据电流2
        /// </summary>
        public double Adat2 = 0;
        /// <summary>
        /// 耐压仪数据电压1
        /// </summary>
        public double Vdat1 = 0;
        /// <summary>
        ///  耐压仪数据电压2
        /// </summary>
        public double Vdat2 = 0;
        /// <summary>
        ///  耐压仪数据电阻1
        /// </summary>
        public double Odat1 = 0;
        /// <summary>
        /// 耐压仪数据电阻2
        /// </summary>
        public double Odat2 = 0;
        #endregion
        #region 螺帽检测结果
        /// <summary>
        /// 螺帽检测结果
        /// </summary>
        public bool sresult4=false;
        /// <summary>
        /// 螺帽数量
        /// </summary>
        public int LMcount=0;
        #endregion
        #region 扫码结果
        /// <summary>
        /// 扫码比对结果
        /// </summary>
        public bool sresult5=false;
        /// <summary>
        /// 固定扫码器扫的二维码
        /// </summary>
        public string AutoCode="";
        #endregion
        public void clear()
        {
            ManualCord = "";
            sresult1 = false;
            MoNum = "";
            sresult2 = false;
            Tdat1 = 0;
            Tdat2 = 0;
            Tdat3 = 0;
            Tdat4 = 0;
            Tdat5 = 0;
            Tdat6 = 0;
            sresult3 = false;
            Adat1 = 0;
            Adat2 = 0;
            Vdat1 = 0;
            Vdat2 = 0;
            Odat1 = 0;
            Odat2 = 0;
            sresult4 = false;
            LMcount = 0;
            sresult5 = false;
            AutoCode = "";
        }
    
    }
   
    /// <summary>
    /// 运行缓存，记录作业步骤，为重启接着干退出时动作
    /// </summary>
    static public class RunBuf
    {
        #region 各站的状态、结果、保存、加载
        /// <summary>
        /// 主线程运行中
        /// </summary>
        static public bool NewRunTask;
        /// <summary>
        /// 接着run的主线程启动中
        /// </summary>
        static public bool ResRunTask;
        /// <summary>
        /// 工位1的状态
        /// </summary>
        static public int Station1 = 0;
        /// <summary>
        /// 工位2的状态
        /// </summary>
        static public int Station2 = 0;
        /// <summary>
        /// 工位3的状态
        /// </summary>
        static public int Station3 = 0;
        /// <summary>
        /// 工位4的状态
        /// </summary>
        static public int Station4 = 0;
        /// <summary>
        /// 工位5的状态
        /// </summary>
        static public int Station5 = 0;
        /// <summary>
        /// 工位6的状态
        /// </summary>
        static public int Station6 = 0;
        /// <summary>
        /// 工位1的各站检测结果
        /// </summary>   
        static public StaRes StationRes1 =new StaRes() ;
        /// <summary>
        /// 工位2的各站检测结果
        /// </summary>
        static public StaRes StationRes2 = new StaRes();
        /// <summary>
        /// 工位3的各站检测结果
        /// </summary>
        static public StaRes StationRes3 = new StaRes();
        /// <summary>
        /// 工位4的各站检测结果
        /// </summary>
        static public StaRes StationRes4 = new StaRes();
        /// <summary>
        /// 工位5的各站检测结果
        /// </summary>
        static public StaRes StationRes5 = new StaRes();
        /// <summary>
        /// 工位6的各站检测结果
        /// </summary>
        static public StaRes StationRes6 = new StaRes();
        #endregion
        #region 保存 、加载状态参数
        static string filename; 
        /// <summary>
        /// 获取工位的当前状态string
        /// </summary>
        /// <param name="sta">状态数字</param>
        /// <returns></returns>
        static public string GetContent(int sta)
        {
            switch (sta)
            {
                case 0:
                    return "等待扫码上料";                  
                case 1:
                    return "扫码有产品";                   
                case 2:
                    return "前往模号检测位置中";                   
                case 3:
                   return "到达模号检测位置";                   
                case 4:
                    return "模号检测中";                   
                case 5:
                    return "模号检测完成";                   
                case 6:
                    return "前往平整度检测工位";                   
                case 7:
                    return "到达平整度检测工位";                   
                case 8:
                    return "平整度检测中";                    
                case 9:
                    return "平整度检测完成";
                case 10:
                    return "前往耐压检测工位中";                   
                case 11:
                    return "到达耐压检测工位";                   
                case 12:
                    return "耐压测试中";                  
                case 13:
                    return "耐压测试完成";                    
                case 14:
                    return "前往螺帽检测工位中";                   
                case 15:
                    return "到达螺帽检测工位";                   
                case 16:
                    return "螺帽检测中";                    
                case 17:
                    return "螺帽检测完成";                   
                case 18:
                    return "前往扫码比对工位中";                   
                case 19:
                    return "到达扫码工位";                   
                case 20:
                    return "开始扫码比对中";                   
                case 21:
                    return "扫码比对完成";                  
                case 22:
                    return "下料中";                    
                case 23:
                    return "下料完成，储存数据中";                   
                case 24:
                    return "储存数据完成，等待上料";
                default:
                    return "未知状态";
                   
            }
        }
        /// <summary>
        /// 保存静态参数
        /// </summary>
        /// <param name="path1"></param>
        /// <returns></returns>
        static public EmRes SaveStaData(string path1)
        {
            filename = $"{Path.GetFullPath("..")}\\product\\{SysStatus.CurProductName.Trim()}\\RunBufdata.ini";// 配置文件路径
            if (path1 == "") path1 = filename;
            IniFile inf = new IniFile(path1);//确认路径是否存在，不存在则创建文件夹。
            try
            {
                string STEP = "Station";
                inf.WriteString(STEP, "Station1", Station1.ToString());
                inf.WriteString(STEP, "Station2", Station2.ToString());
                inf.WriteString(STEP, "Station3", Station3.ToString());
                inf.WriteString(STEP, "Station4", Station4.ToString());
                inf.WriteString(STEP, "Station5", Station5.ToString());
                inf.WriteString(STEP, "Station6", Station6.ToString());
                STEP = "Station1Result";
                inf.WriteString(STEP, "StationResult1.1", StationRes1.ManualCord.ToString());//上料扫码
                inf.WriteString(STEP, "StationResult2.1", StationRes1.sresult1.ToString());//模号检测
                inf.WriteString(STEP, "StationResult2.2", StationRes1.MoNum.ToString());
                inf.WriteString(STEP, "StationResult3.1", StationRes1.sresult2.ToString());//平整度检测
                inf.WriteString(STEP, "StationResult3.2", StationRes1.Tdat1.ToString());
                inf.WriteString(STEP, "StationResult3.3", StationRes1.Tdat2.ToString());
                inf.WriteString(STEP, "StationResult3.4", StationRes1.Tdat3.ToString());
                inf.WriteString(STEP, "StationResult3.5", StationRes1.Tdat4.ToString());
                inf.WriteString(STEP, "StationResult3.6", StationRes1.Tdat5.ToString());
                inf.WriteString(STEP, "StationResult3.7", StationRes1.Tdat6.ToString());
                inf.WriteString(STEP, "StationResult4.1", StationRes1.sresult3.ToString());//耐压仪检测
                inf.WriteString(STEP, "StationResult4.2", StationRes1.Adat1.ToString());
                inf.WriteString(STEP, "StationResult4.3", StationRes1.Adat2.ToString());
                inf.WriteString(STEP, "StationResult4.4", StationRes1.Vdat1.ToString());
                inf.WriteString(STEP, "StationResult4.5", StationRes1.Vdat2.ToString());
                inf.WriteString(STEP, "StationResult4.6", StationRes1.Odat1.ToString());
                inf.WriteString(STEP, "StationResult4.7", StationRes1.Odat2.ToString());
                inf.WriteString(STEP, "StationResult5.1", StationRes1.sresult4.ToString());//螺帽检测
                inf.WriteString(STEP, "StationResult5.2", StationRes1.LMcount.ToString());
                inf.WriteString(STEP, "StationResult6.1", StationRes1.sresult5.ToString());//扫码比对
                inf.WriteString(STEP, "StationResult6.2", StationRes1.AutoCode.ToString());
                STEP = "Station2Result";
                inf.WriteString(STEP, "StationResult1.1", StationRes2.ManualCord.ToString());//上料扫码
                inf.WriteString(STEP, "StationResult2.1", StationRes2.sresult1.ToString());//模号检测
                inf.WriteString(STEP, "StationResult2.2", StationRes2.MoNum.ToString());
                inf.WriteString(STEP, "StationResult3.1", StationRes2.sresult2.ToString());//平整度检测
                inf.WriteString(STEP, "StationResult3.2", StationRes2.Tdat1.ToString());
                inf.WriteString(STEP, "StationResult3.3", StationRes2.Tdat2.ToString());
                inf.WriteString(STEP, "StationResult3.4", StationRes2.Tdat3.ToString());
                inf.WriteString(STEP, "StationResult3.5", StationRes2.Tdat4.ToString());
                inf.WriteString(STEP, "StationResult3.6", StationRes2.Tdat5.ToString());
                inf.WriteString(STEP, "StationResult3.7", StationRes2.Tdat6.ToString());
                inf.WriteString(STEP, "StationResult4.1", StationRes2.sresult3.ToString());//耐压仪检测
                inf.WriteString(STEP, "StationResult4.2", StationRes2.Adat1.ToString());
                inf.WriteString(STEP, "StationResult4.3", StationRes2.Adat2.ToString());
                inf.WriteString(STEP, "StationResult4.4", StationRes2.Vdat1.ToString());
                inf.WriteString(STEP, "StationResult4.5", StationRes2.Vdat2.ToString());
                inf.WriteString(STEP, "StationResult4.6", StationRes2.Odat1.ToString());
                inf.WriteString(STEP, "StationResult4.7", StationRes2.Odat2.ToString());
                inf.WriteString(STEP, "StationResult5.1", StationRes2.sresult4.ToString());//螺帽检测
                inf.WriteString(STEP, "StationResult5.2", StationRes2.LMcount.ToString());
                inf.WriteString(STEP, "StationResult6.1", StationRes2.sresult5.ToString());//扫码比对
                inf.WriteString(STEP, "StationResult6.2", StationRes2.AutoCode.ToString());
                STEP = "Station3Result";
                inf.WriteString(STEP, "StationResult1.1", StationRes3.ManualCord.ToString());//上料扫码
                inf.WriteString(STEP, "StationResult2.1", StationRes3.sresult1.ToString());//模号检测
                inf.WriteString(STEP, "StationResult2.2", StationRes3.MoNum.ToString());
                inf.WriteString(STEP, "StationResult3.1", StationRes3.sresult2.ToString());//平整度检测
                inf.WriteString(STEP, "StationResult3.2", StationRes3.Tdat1.ToString());
                inf.WriteString(STEP, "StationResult3.3", StationRes3.Tdat2.ToString());
                inf.WriteString(STEP, "StationResult3.4", StationRes3.Tdat3.ToString());
                inf.WriteString(STEP, "StationResult3.5", StationRes3.Tdat4.ToString());
                inf.WriteString(STEP, "StationResult3.6", StationRes3.Tdat5.ToString());
                inf.WriteString(STEP, "StationResult3.7", StationRes3.Tdat6.ToString());
                inf.WriteString(STEP, "StationResult4.1", StationRes3.sresult3.ToString());//耐压仪检测
                inf.WriteString(STEP, "StationResult4.2", StationRes3.Adat1.ToString());
                inf.WriteString(STEP, "StationResult4.3", StationRes3.Adat2.ToString());
                inf.WriteString(STEP, "StationResult4.4", StationRes3.Vdat1.ToString());
                inf.WriteString(STEP, "StationResult4.5", StationRes3.Vdat2.ToString());
                inf.WriteString(STEP, "StationResult4.6", StationRes3.Odat1.ToString());
                inf.WriteString(STEP, "StationResult4.7", StationRes3.Odat2.ToString());
                inf.WriteString(STEP, "StationResult5.1", StationRes3.sresult4.ToString());//螺帽检测
                inf.WriteString(STEP, "StationResult5.2", StationRes3.LMcount.ToString());
                inf.WriteString(STEP, "StationResult6.1", StationRes3.sresult5.ToString());//扫码比对
                inf.WriteString(STEP, "StationResult6.2", StationRes3.AutoCode.ToString());
                STEP = "Station4Result";
                inf.WriteString(STEP, "StationResult1.1", StationRes4.ManualCord.ToString());//上料扫码
                inf.WriteString(STEP, "StationResult2.1", StationRes4.sresult1.ToString());//模号检测
                inf.WriteString(STEP, "StationResult2.2", StationRes4.MoNum.ToString());
                inf.WriteString(STEP, "StationResult3.1", StationRes4.sresult2.ToString());//平整度检测
                inf.WriteString(STEP, "StationResult3.2", StationRes4.Tdat1.ToString());
                inf.WriteString(STEP, "StationResult3.3", StationRes4.Tdat2.ToString());
                inf.WriteString(STEP, "StationResult3.4", StationRes4.Tdat3.ToString());
                inf.WriteString(STEP, "StationResult3.5", StationRes4.Tdat4.ToString());
                inf.WriteString(STEP, "StationResult3.6", StationRes4.Tdat5.ToString());
                inf.WriteString(STEP, "StationResult3.7", StationRes4.Tdat6.ToString());
                inf.WriteString(STEP, "StationResult4.1", StationRes4.sresult3.ToString());//耐压仪检测
                inf.WriteString(STEP, "StationResult4.2", StationRes4.Adat1.ToString());
                inf.WriteString(STEP, "StationResult4.3", StationRes4.Adat2.ToString());
                inf.WriteString(STEP, "StationResult4.4", StationRes4.Vdat1.ToString());
                inf.WriteString(STEP, "StationResult4.5", StationRes4.Vdat2.ToString());
                inf.WriteString(STEP, "StationResult4.6", StationRes4.Odat1.ToString());
                inf.WriteString(STEP, "StationResult4.7", StationRes4.Odat2.ToString());
                inf.WriteString(STEP, "StationResult5.1", StationRes4.sresult4.ToString());//螺帽检测
                inf.WriteString(STEP, "StationResult5.2", StationRes4.LMcount.ToString());
                inf.WriteString(STEP, "StationResult6.1", StationRes4.sresult5.ToString());//扫码比对
                inf.WriteString(STEP, "StationResult6.2", StationRes4.AutoCode.ToString());
                STEP = "Station5Result";
                inf.WriteString(STEP, "StationResult1.1", StationRes5.ManualCord.ToString());//上料扫码
                inf.WriteString(STEP, "StationResult2.1", StationRes5.sresult1.ToString());//模号检测
                inf.WriteString(STEP, "StationResult2.2", StationRes5.MoNum.ToString());
                inf.WriteString(STEP, "StationResult3.1", StationRes5.sresult2.ToString());//平整度检测
                inf.WriteString(STEP, "StationResult3.2", StationRes5.Tdat1.ToString());
                inf.WriteString(STEP, "StationResult3.3", StationRes5.Tdat2.ToString());
                inf.WriteString(STEP, "StationResult3.4", StationRes5.Tdat3.ToString());
                inf.WriteString(STEP, "StationResult3.5", StationRes5.Tdat4.ToString());
                inf.WriteString(STEP, "StationResult3.6", StationRes5.Tdat5.ToString());
                inf.WriteString(STEP, "StationResult3.7", StationRes5.Tdat6.ToString());
                inf.WriteString(STEP, "StationResult4.1", StationRes5.sresult3.ToString());//耐压仪检测
                inf.WriteString(STEP, "StationResult4.2", StationRes5.Adat1.ToString());
                inf.WriteString(STEP, "StationResult4.3", StationRes5.Adat2.ToString());
                inf.WriteString(STEP, "StationResult4.4", StationRes5.Vdat1.ToString());
                inf.WriteString(STEP, "StationResult4.5", StationRes5.Vdat2.ToString());
                inf.WriteString(STEP, "StationResult4.6", StationRes5.Odat1.ToString());
                inf.WriteString(STEP, "StationResult4.7", StationRes5.Odat2.ToString());
                inf.WriteString(STEP, "StationResult5.1", StationRes5.sresult4.ToString());//螺帽检测
                inf.WriteString(STEP, "StationResult5.2", StationRes5.LMcount.ToString());
                inf.WriteString(STEP, "StationResult6.1", StationRes5.sresult5.ToString());//扫码比对
                inf.WriteString(STEP, "StationResult6.2", StationRes5.AutoCode.ToString());
                STEP = "Station6Result";
                inf.WriteString(STEP, "StationResult1.1", StationRes6.ManualCord.ToString());//上料扫码
                inf.WriteString(STEP, "StationResult2.1", StationRes6.sresult1.ToString());//模号检测
                inf.WriteString(STEP, "StationResult2.2", StationRes6.MoNum.ToString());
                inf.WriteString(STEP, "StationResult3.1", StationRes6.sresult2.ToString());//平整度检测
                inf.WriteString(STEP, "StationResult3.2", StationRes6.Tdat1.ToString());
                inf.WriteString(STEP, "StationResult3.3", StationRes6.Tdat2.ToString());
                inf.WriteString(STEP, "StationResult3.4", StationRes6.Tdat3.ToString());
                inf.WriteString(STEP, "StationResult3.5", StationRes6.Tdat4.ToString());
                inf.WriteString(STEP, "StationResult3.6", StationRes6.Tdat5.ToString());
                inf.WriteString(STEP, "StationResult3.7", StationRes6.Tdat6.ToString());
                inf.WriteString(STEP, "StationResult4.1", StationRes6.sresult3.ToString());//耐压仪检测
                inf.WriteString(STEP, "StationResult4.2", StationRes6.Adat1.ToString());
                inf.WriteString(STEP, "StationResult4.3", StationRes6.Adat2.ToString());
                inf.WriteString(STEP, "StationResult4.4", StationRes6.Vdat1.ToString());
                inf.WriteString(STEP, "StationResult4.5", StationRes6.Vdat2.ToString());
                inf.WriteString(STEP, "StationResult4.6", StationRes6.Odat1.ToString());
                inf.WriteString(STEP, "StationResult4.7", StationRes6.Odat2.ToString());
                inf.WriteString(STEP, "StationResult5.1", StationRes6.sresult4.ToString());//螺帽检测
                inf.WriteString(STEP, "StationResult5.2", StationRes6.LMcount.ToString());
                inf.WriteString(STEP, "StationResult6.1", StationRes6.sresult5.ToString());//扫码比对
                inf.WriteString(STEP, "StationResult6.2", StationRes6.AutoCode.ToString());
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
           
            return EmRes.Succeed;

        }
        /// <summary>
        /// 加载静态参数
        /// </summary>
        /// <param name="path1"></param>
        /// <returns></returns>
        static public EmRes LoadStaData(string path1)
        {
            filename = $"{Path.GetFullPath("..")}\\product\\{SysStatus.CurProductName.Trim()}\\RunBufdata.ini";// 配置文件路径
            if (path1 == "") path1 = filename;
            IniFile inf = new IniFile(path1);//确认路径是否存在，不存在则创建文件夹。
            try
            {
                string STEP = "Station";
                Station1 = inf.ReadInteger(STEP, "Station1", Station1);
                Station2 = inf.ReadInteger(STEP, "Station2", Station2);
                Station3 = inf.ReadInteger(STEP, "Station3", Station3);
                Station4 = inf.ReadInteger(STEP, "Station4", Station4);
                Station5 = inf.ReadInteger(STEP, "Station5", Station5);
                Station6 = inf.ReadInteger(STEP, "Station6", Station6);
                STEP = "Station1Result";
                StationRes1.ManualCord = inf.ReadString(STEP, "StationResult1.1", StationRes1.ManualCord);
                StationRes1.sresult1 = inf.ReadBool(STEP, "StationResult2.1", StationRes1.sresult1);
                StationRes1.MoNum = inf.ReadString(STEP, "StationResult2.2", StationRes1.MoNum);
                StationRes1.sresult2 = inf.ReadBool(STEP, "StationResult3.1", StationRes1.sresult2);
                StationRes1.Tdat1 = inf.ReadDouble(STEP, "StationResult3.2", StationRes1.Tdat1);
                StationRes1.Tdat2 = inf.ReadDouble(STEP, "StationResult3.3", StationRes1.Tdat2);
                StationRes1.Tdat3 = inf.ReadDouble(STEP, "StationResult3.4", StationRes1.Tdat3);
                StationRes1.Tdat4 = inf.ReadDouble(STEP, "StationResult3.5", StationRes1.Tdat4);
                StationRes1.Tdat5 = inf.ReadDouble(STEP, "StationResult3.6", StationRes1.Tdat5);
                StationRes1.Tdat6 = inf.ReadDouble(STEP, "StationResult3.7", StationRes1.Tdat6);
                StationRes1.sresult3 = inf.ReadBool(STEP, "StationResult4.1", StationRes1.sresult3);
                StationRes1.Adat1 = inf.ReadDouble(STEP, "StationResult4.2", StationRes1.Adat1);
                StationRes1.Adat2 = inf.ReadDouble(STEP, "StationResult4.3", StationRes1.Adat2);
                StationRes1.Vdat1 = inf.ReadDouble(STEP, "StationResult4.4", StationRes1.Vdat1);
                StationRes1.Vdat2 = inf.ReadDouble(STEP, "StationResult4.5", StationRes1.Vdat2);
                StationRes1.Odat1 = inf.ReadDouble(STEP, "StationResult4.6", StationRes1.Odat1);
                StationRes1.Odat2 = inf.ReadDouble(STEP, "StationResult4.7", StationRes1.Odat2);
                StationRes1.sresult4 = inf.ReadBool(STEP, "StationResult5.1", StationRes1.sresult4);
                StationRes1.LMcount = inf.ReadInteger(STEP, "StationResult5.2", StationRes1.LMcount);
                StationRes1.sresult5 = inf.ReadBool(STEP, "StationResult6.1", StationRes1.sresult5);
                StationRes1.AutoCode = inf.ReadString(STEP, "StationResult6.2", StationRes1.AutoCode);
                STEP = "Station2Result";
                StationRes2.ManualCord = inf.ReadString(STEP, "StationResult1.1", StationRes2.ManualCord);
                StationRes2.sresult1 = inf.ReadBool(STEP, "StationResult2.1", StationRes2.sresult1);
                StationRes2.MoNum = inf.ReadString(STEP, "StationResult2.2", StationRes2.MoNum);
                StationRes2.sresult2 = inf.ReadBool(STEP, "StationResult3.1", StationRes2.sresult2);
                StationRes2.Tdat1 = inf.ReadDouble(STEP, "StationResult3.2", StationRes2.Tdat1);
                StationRes2.Tdat2 = inf.ReadDouble(STEP, "StationResult3.3", StationRes2.Tdat2);
                StationRes2.Tdat3 = inf.ReadDouble(STEP, "StationResult3.4", StationRes2.Tdat3);
                StationRes2.Tdat4 = inf.ReadDouble(STEP, "StationResult3.5", StationRes2.Tdat4);
                StationRes2.Tdat5 = inf.ReadDouble(STEP, "StationResult3.6", StationRes2.Tdat5);
                StationRes2.Tdat6 = inf.ReadDouble(STEP, "StationResult3.7", StationRes2.Tdat6);
                StationRes2.sresult3 = inf.ReadBool(STEP, "StationResult4.1", StationRes2.sresult3);
                StationRes2.Adat1 = inf.ReadDouble(STEP, "StationResult4.2", StationRes2.Adat1);
                StationRes2.Adat2 = inf.ReadDouble(STEP, "StationResult4.3", StationRes2.Adat2);
                StationRes2.Vdat1 = inf.ReadDouble(STEP, "StationResult4.4", StationRes2.Vdat1);
                StationRes2.Vdat2 = inf.ReadDouble(STEP, "StationResult4.5", StationRes2.Vdat2);
                StationRes2.Odat1 = inf.ReadDouble(STEP, "StationResult4.6", StationRes2.Odat1);
                StationRes2.Odat2 = inf.ReadDouble(STEP, "StationResult4.7", StationRes2.Odat2);
                StationRes2.sresult4 = inf.ReadBool(STEP, "StationResult5.1", StationRes2.sresult4);
                StationRes2.LMcount = inf.ReadInteger(STEP, "StationResult5.2", StationRes2.LMcount);
                StationRes2.sresult5 = inf.ReadBool(STEP, "StationResult6.1", StationRes2.sresult5);
                StationRes2.AutoCode = inf.ReadString(STEP, "StationResult6.2", StationRes2.AutoCode);
                STEP = "Station3Result";
                StationRes3.ManualCord = inf.ReadString(STEP, "StationResult1.1", StationRes3.ManualCord);
                StationRes3.sresult1 = inf.ReadBool(STEP, "StationResult2.1", StationRes3.sresult1);
                StationRes3.MoNum = inf.ReadString(STEP, "StationResult2.2", StationRes3.MoNum);
                StationRes3.sresult2 = inf.ReadBool(STEP, "StationResult3.1", StationRes3.sresult2);
                StationRes3.Tdat1 = inf.ReadDouble(STEP, "StationResult3.2", StationRes3.Tdat1);
                StationRes3.Tdat2 = inf.ReadDouble(STEP, "StationResult3.3", StationRes3.Tdat2);
                StationRes3.Tdat3 = inf.ReadDouble(STEP, "StationResult3.4", StationRes3.Tdat3);
                StationRes3.Tdat4 = inf.ReadDouble(STEP, "StationResult3.5", StationRes3.Tdat4);
                StationRes3.Tdat5 = inf.ReadDouble(STEP, "StationResult3.6", StationRes3.Tdat5);
                StationRes3.Tdat6 = inf.ReadDouble(STEP, "StationResult3.7", StationRes3.Tdat6);
                StationRes3.sresult3 = inf.ReadBool(STEP, "StationResult4.1", StationRes3.sresult3);
                StationRes3.Adat1 = inf.ReadDouble(STEP, "StationResult4.2", StationRes3.Adat1);
                StationRes3.Adat2 = inf.ReadDouble(STEP, "StationResult4.3", StationRes3.Adat2);
                StationRes3.Vdat1 = inf.ReadDouble(STEP, "StationResult4.4", StationRes3.Vdat1);
                StationRes3.Vdat2 = inf.ReadDouble(STEP, "StationResult4.5", StationRes3.Vdat2);
                StationRes3.Odat1 = inf.ReadDouble(STEP, "StationResult4.6", StationRes3.Odat1);
                StationRes3.Odat2 = inf.ReadDouble(STEP, "StationResult4.7", StationRes3.Odat2);
                StationRes3.sresult4 = inf.ReadBool(STEP, "StationResult5.1", StationRes3.sresult4);
                StationRes3.LMcount = inf.ReadInteger(STEP, "StationResult5.2", StationRes3.LMcount);
                StationRes3.sresult5 = inf.ReadBool(STEP, "StationResult6.1", StationRes3.sresult5);
                StationRes3.AutoCode = inf.ReadString(STEP, "StationResult6.2", StationRes3.AutoCode);
                STEP = "Station4Result";
                StationRes4.ManualCord = inf.ReadString(STEP, "StationResult1.1", StationRes4.ManualCord);
                StationRes4.sresult1 = inf.ReadBool(STEP, "StationResult2.1", StationRes4.sresult1);
                StationRes4.MoNum = inf.ReadString(STEP, "StationResult2.2", StationRes4.MoNum);
                StationRes4.sresult2 = inf.ReadBool(STEP, "StationResult3.1", StationRes4.sresult2);
                StationRes4.Tdat1 = inf.ReadDouble(STEP, "StationResult3.2", StationRes4.Tdat1);
                StationRes4.Tdat2 = inf.ReadDouble(STEP, "StationResult3.3", StationRes4.Tdat2);
                StationRes4.Tdat3 = inf.ReadDouble(STEP, "StationResult3.4", StationRes4.Tdat3);
                StationRes4.Tdat4 = inf.ReadDouble(STEP, "StationResult3.5", StationRes4.Tdat4);
                StationRes4.Tdat5 = inf.ReadDouble(STEP, "StationResult3.6", StationRes4.Tdat5);
                StationRes4.Tdat6 = inf.ReadDouble(STEP, "StationResult3.7", StationRes4.Tdat6);
                StationRes4.sresult3 = inf.ReadBool(STEP, "StationResult4.1", StationRes4.sresult3);
                StationRes4.Adat1 = inf.ReadDouble(STEP, "StationResult4.2", StationRes4.Adat1);
                StationRes4.Adat2 = inf.ReadDouble(STEP, "StationResult4.3", StationRes4.Adat2);
                StationRes4.Vdat1 = inf.ReadDouble(STEP, "StationResult4.4", StationRes4.Vdat1);
                StationRes4.Vdat2 = inf.ReadDouble(STEP, "StationResult4.5", StationRes4.Vdat2);
                StationRes4.Odat1 = inf.ReadDouble(STEP, "StationResult4.6", StationRes4.Odat1);
                StationRes4.Odat2 = inf.ReadDouble(STEP, "StationResult4.7", StationRes4.Odat2);
                StationRes4.sresult4 = inf.ReadBool(STEP, "StationResult5.1", StationRes4.sresult4);
                StationRes4.LMcount = inf.ReadInteger(STEP, "StationResult5.2", StationRes4.LMcount);
                StationRes4.sresult5 = inf.ReadBool(STEP, "StationResult6.1", StationRes4.sresult5);
                StationRes4.AutoCode = inf.ReadString(STEP, "StationResult6.2", StationRes4.AutoCode);
                STEP = "Station5Result";
                StationRes5.ManualCord = inf.ReadString(STEP, "StationResult1.1", StationRes5.ManualCord);
                StationRes5.sresult1 = inf.ReadBool(STEP, "StationResult2.1", StationRes5.sresult1);
                StationRes5.MoNum = inf.ReadString(STEP, "StationResult2.2", StationRes5.MoNum);
                StationRes5.sresult2 = inf.ReadBool(STEP, "StationResult3.1", StationRes5.sresult2);
                StationRes5.Tdat1 = inf.ReadDouble(STEP, "StationResult3.2", StationRes5.Tdat1);
                StationRes5.Tdat2 = inf.ReadDouble(STEP, "StationResult3.3", StationRes5.Tdat2);
                StationRes5.Tdat3 = inf.ReadDouble(STEP, "StationResult3.4", StationRes5.Tdat3);
                StationRes5.Tdat4 = inf.ReadDouble(STEP, "StationResult3.5", StationRes5.Tdat4);
                StationRes5.Tdat5 = inf.ReadDouble(STEP, "StationResult3.6", StationRes5.Tdat5);
                StationRes5.Tdat6 = inf.ReadDouble(STEP, "StationResult3.7", StationRes5.Tdat6);
                StationRes5.sresult3 = inf.ReadBool(STEP, "StationResult4.1", StationRes5.sresult3);
                StationRes5.Adat1 = inf.ReadDouble(STEP, "StationResult4.2", StationRes5.Adat1);
                StationRes5.Adat2 = inf.ReadDouble(STEP, "StationResult4.3", StationRes5.Adat2);
                StationRes5.Vdat1 = inf.ReadDouble(STEP, "StationResult4.4", StationRes5.Vdat1);
                StationRes5.Vdat2 = inf.ReadDouble(STEP, "StationResult4.5", StationRes5.Vdat2);
                StationRes5.Odat1 = inf.ReadDouble(STEP, "StationResult4.6", StationRes5.Odat1);
                StationRes5.Odat2 = inf.ReadDouble(STEP, "StationResult4.7", StationRes5.Odat2);
                StationRes5.sresult4 = inf.ReadBool(STEP, "StationResult5.1", StationRes5.sresult4);
                StationRes5.LMcount = inf.ReadInteger(STEP, "StationResult5.2", StationRes5.LMcount);
                StationRes5.sresult5 = inf.ReadBool(STEP, "StationResult6.1", StationRes5.sresult5);
                StationRes5.AutoCode = inf.ReadString(STEP, "StationResult6.2", StationRes5.AutoCode);
                STEP = "Station6Result";
                StationRes6.ManualCord = inf.ReadString(STEP, "StationResult1.1", StationRes6.ManualCord);
                StationRes6.sresult1 = inf.ReadBool(STEP, "StationResult2.1", StationRes6.sresult1);
                StationRes6.MoNum = inf.ReadString(STEP, "StationResult2.2", StationRes6.MoNum);
                StationRes6.sresult2 = inf.ReadBool(STEP, "StationResult3.1", StationRes6.sresult2);
                StationRes6.Tdat1 = inf.ReadDouble(STEP, "StationResult3.2", StationRes6.Tdat1);
                StationRes6.Tdat2 = inf.ReadDouble(STEP, "StationResult3.3", StationRes6.Tdat2);
                StationRes6.Tdat3 = inf.ReadDouble(STEP, "StationResult3.4", StationRes6.Tdat3);
                StationRes6.Tdat4 = inf.ReadDouble(STEP, "StationResult3.5", StationRes6.Tdat4);
                StationRes6.Tdat5 = inf.ReadDouble(STEP, "StationResult3.6", StationRes6.Tdat5);
                StationRes6.Tdat6 = inf.ReadDouble(STEP, "StationResult3.7", StationRes6.Tdat6);
                StationRes6.sresult3 = inf.ReadBool(STEP, "StationResult4.1", StationRes6.sresult3);
                StationRes6.Adat1 = inf.ReadDouble(STEP, "StationResult4.2", StationRes6.Adat1);
                StationRes6.Adat2 = inf.ReadDouble(STEP, "StationResult4.3", StationRes6.Adat2);
                StationRes6.Vdat1 = inf.ReadDouble(STEP, "StationResult4.4", StationRes6.Vdat1);
                StationRes6.Vdat2 = inf.ReadDouble(STEP, "StationResult4.5", StationRes6.Vdat2);
                StationRes6.Odat1 = inf.ReadDouble(STEP, "StationResult4.6", StationRes6.Odat1);
                StationRes6.Odat2 = inf.ReadDouble(STEP, "StationResult4.7", StationRes6.Odat2);
                StationRes6.sresult4 = inf.ReadBool(STEP, "StationResult5.1", StationRes6.sresult4);
                StationRes6.LMcount = inf.ReadInteger(STEP, "StationResult5.2", StationRes6.LMcount);
                StationRes6.sresult5 = inf.ReadBool(STEP, "StationResult6.1", StationRes6.sresult5);
                StationRes6.AutoCode = inf.ReadString(STEP, "StationResult6.2", StationRes6.AutoCode);
               
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return EmRes.Succeed;
        }
        #endregion
        #region 功能函数
        /// <summary>
        /// 初始化所有工位状态和结果
        /// </summary>
        static public void ClearSta()
        {
            Station1 = 0; Station2 = 0; Station3 = 0; Station4 = 0; Station5 = 0; Station6 = 0;
            StationRes1.clear();
            StationRes2.clear();
            StationRes3.clear();
            StationRes4.clear();
            StationRes5.clear();
            StationRes6.clear();
        }
 
        /// <summary>
        /// 设置工位状态
        /// </summary>
        /// <param name="x">工位号</param>
        /// <param name="y">状态值</param>
        static public void Setsta(int x,int y)
        {
            if (x == 1) Station1 = y;
            else if (x == 2) Station2 = y;
            else if (x == 3) Station3 = y;
            else if (x == 4) Station4 = y;
            else if (x == 5) Station5 = y;
            else if (x == 6) Station6 = y;
        }
        /// <summary>
        /// 获取工位结果类
        /// </summary>
        /// <param name="x">工位号</param>
        /// <returns></returns>
        static public StaRes GetStaRes(int x)
        {
            if (x == 1) return StationRes1;
            else if (x == 2) return StationRes2;
            else if (x == 3) return StationRes3;
            else if (x == 4) return StationRes4;
            else if (x == 5) return StationRes5;
            else if (x == 6) return StationRes6;
            else return StationRes1;
        }
        /// <summary>
        /// 获取工位的状态值
        /// </summary>
        /// <param name="x">工位号</param>
        /// <returns></returns>
        static public int GetStaValue(int x)
        {
            if (x == 1) return Station1;
            else if (x == 2) return Station2;
            else if (x == 3) return Station3;
            else if (x == 4) return Station4;
            else if (x == 5) return Station5;
            else if (x == 6) return Station6;
            else   return Station1;
        }
        #endregion
        #region 保存数据到数据库
        /// <summary>
        /// 保持产品的生产数据
        /// </summary>
        /// <param name="x">工位号</param>
        /// <returns></returns>
      /*  static public EmRes SaveDB(int x)
        {
            StaRes stares = GetStaRes(x);
            string dbname = $"{SysStatus.CurProductName}.db";
            string SavePath1 = $@"E:\\DB\\{dbname}";

            if (!System.IO.Directory.Exists(@"E:\\DB\\")) System.IO.Directory.CreateDirectory(@"E:\\DB\\");//创建文件夹‪E:\DB

            using (var conn = new SQLiteConnection($@"data source={SavePath1}"))//没有就创建
            {
                using (var cmd = new SQLiteCommand())
                {
                    try
                    {
                        conn.Open();
                        cmd.Connection = conn;

                        if (conn.State == ConnectionState.Closed)
                        {
                            Logger.Error("barcode.db链接数据出错");
                        }
                        //开始事务
                        //  var trans = conn.BeginTransaction();
                        //chec;
                        if (SavePath1.Contains(SysStatus.CurProductName))
                        {
                            var cmd1 = new SQLiteCommand(conn)
                            {
                                CommandText = @"CREATE TABLE IF NOT EXISTS `prodata`(time TEXT,productname TEXT,ManualCord TEXT,sresult1 TEXT,MoNum TEXT,sresult2 TEXT,Tdat1 TEXT,Tdat2 TEXT,Tdat3 TEXT,Tdat4 TEXT,Tdat5 TEXT,Tdat6 TEXT,sresult3 TEXT,
Adat1 TEXT, Adat2 TEXT, Vdat1 TEXT, Vdat2 TEXT, Odat1 TEXT, Odat2 TEXT,sresult4 TEXT,LMcount  TEXT,sresult5 TEXT,AutoCode TEXT)"//不存在“param”表  就创建
                            };
                            int jie1 = cmd1.ExecuteNonQuery();
                        }
                        //提交事务

                        string dat = DateTime.Now.ToString();
                        var sql = new SqLiteHelper(cmd);
                        cmd.CommandText = @"INSERT INTO `prodata` (time ,productname ,ManualCord ,sresult1,MoNum,sresult2,Tdat1,Tdat2,Tdat3,Tdat4,Tdat5,Tdat6,sresult3,Adat1 , Adat2, Vdat1, Vdat2, Odat1, Odat2,sresult4,LMcount,sresult5,AutoCode)
VALUES (@time ,@productname ,@ManualCord ,@sresult1,@MoNum,@sresult2,@Tdat1,@Tdat2,@Tdat3,@Tdat4,@Tdat5,@Tdat6,@sresult3,@Adat1 , @Adat2, @Vdat1, @Vdat2, @Odat1, @Odat2,@sresult4,@LMcount,@sresult5,@AutoCode)";
                        cmd.Parameters.Clear();

                        cmd.Parameters.AddWithValue("@time", dat);
                        cmd.Parameters.AddWithValue("@productname", SysStatus.CurProductName);
                        cmd.Parameters.AddWithValue("@ManualCord", stares.ManualCord);
                        cmd.Parameters.AddWithValue("@sresult1", stares.sresult1.ToString());
                        cmd.Parameters.AddWithValue("@MoNum", stares.MoNum);
                        cmd.Parameters.AddWithValue("@sresult2", stares.sresult2.ToString());
                        cmd.Parameters.AddWithValue("@Tdat1", stares.Tdat1);
                        cmd.Parameters.AddWithValue("@Tdat2", stares.Tdat2);
                        cmd.Parameters.AddWithValue("@Tdat3", stares.Tdat3);
                        cmd.Parameters.AddWithValue("@Tdat4", stares.Tdat4);
                        cmd.Parameters.AddWithValue("@Tdat5", stares.Tdat5);
                        cmd.Parameters.AddWithValue("@Tdat6", stares.Tdat6);
                        cmd.Parameters.AddWithValue("@sresult3", stares.sresult3.ToString());
                        cmd.Parameters.AddWithValue("@Adat1", stares.Adat1);
                        cmd.Parameters.AddWithValue("@Adat2", stares.Adat2);
                        cmd.Parameters.AddWithValue("@Vdat1", stares.Vdat1);
                        cmd.Parameters.AddWithValue("@Vdat2", stares.Vdat2);
                        cmd.Parameters.AddWithValue("@Odat1", stares.Odat1);
                        cmd.Parameters.AddWithValue("@Odat2", stares.Odat2);
                        cmd.Parameters.AddWithValue("@sresult4", stares.sresult4.ToString());
                        cmd.Parameters.AddWithValue("@LMcount", stares.LMcount);
                        cmd.Parameters.AddWithValue("@sresult5", stares.sresult5.ToString());
                        cmd.Parameters.AddWithValue("@AutoCode", stares.AutoCode);


                        if (0 == cmd.ExecuteNonQuery())
                        {
                            Logger.Error($"ST{x}产品结果数据写入失败");
                            return EmRes.Error;
                        }
                        cmd.Dispose();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Logger.Error($"ST{x}产品结果数据写入失败:{ ex.Message}");
                        return EmRes.Error;
                    }
                }
            }
            return EmRes.Succeed;
        }*/
        #endregion
        /// <summary>
        /// 关闭所有线程
        /// </summary>
        
    }
      public class ComRun
    {
        public bool Quit = false;
       // public Task WaitCord = null;
        public int StaNumb = 0;
        /// <summary>
        /// 扫码上料等待转盘转到位线程  Task.Factory.StartNew(MyTask, cancelTokenSource.Token);
        /// </summary>
        /// <param name="StaNumb">工位号</param>
        public void RunWaitCordTask()
        {
           
                Task WaitCord = new Task(() =>
                {
                    Logger.Info("创建RunWaitCordTask线程!");
                    EmRes ret = EmRes.Succeed;
                    string Y = "";
                    //while (SysStatus.Status == SysStatus.EmSysSta.Run)
                    //{
                    //    if (RunBuf.GetStaValue(StaNumb) == 0)//等待扫码
                    //    {
                    //        //等待手持扫码枪有扫码
                    //        hardware.delijie1.ReceiveStringData = "";
                    //        hardware.delijie1.GetCordOK = false;
                    //        Logger.Info("ST1等待手持扫码枪扫码中……");
                    //        while (hardware.delijie1.ReceiveStringData.Length < 2)
                    //        {
                    //            Thread.Sleep(500);
                    //            Application.DoEvents();//等待扫码枪扫到码
                    //            if (Quit) return;
                    //        }
                    //        RunBuf.Setsta(StaNumb, 1);
                    //        Logger.Info($"扫码枪扫到码{hardware.delijie1.ReceiveStringData}");
                    //        RunBuf.GetStaRes(StaNumb).ManualCord = hardware.delijie1.ReceiveStringData;
                    //        hardware.delijie1.ReceiveStringData = "";//取完码就清掉
                    //        hardware.delijie1.GetCordOK = false;
                    //        Logger.Info($"ST{StaNumb}发送扫码结果给PLC:B1601、W1701");
                    //    STEP1:
                    //        if (ret != hardware.PLC_KEY.writebit("B1601", 1, "1"))
                    //        {
                    //            if (Quit) return;
                    //            Thread.Sleep(200);
                    //            goto STEP1;
                    //        }
                    //    STEP2:
                    //        if (ret != hardware.PLC_KEY.writebit("W1701", 1, "00001"))
                    //        {
                    //            if (Quit) return;
                    //            Thread.Sleep(100);
                    //            goto STEP2;
                    //        }
                    //        //等待转盘信号 读取触发状态B1002	B1003
                    //        Logger.Info($"ST{StaNumb}等待PLC转盘转动信号B1002");
                    //    STEP3:
                    //        if (ret != hardware.PLC_KEY.readRbit("B1002", 1, out Y))
                    //        {
                    //            if (Quit) return;
                    //            Thread.Sleep(100);
                    //            goto STEP3;
                    //        }
                    //        if (Y != "1")//转盘触发旋转 前往模号检测位置中
                    //        {
                    //            if (Quit) return;
                    //            Thread.Sleep(100);
                    //            goto STEP3;
                    //        }
                    //        RunBuf.Setsta(StaNumb, 2);
                    //        hardware.delijie1.ReceiveStringData = "";//清除掉扫码枪扫到的码
                           
                    //    STEP4:
                    //        //读取转盘转到位信号
                    //        Logger.Info($"ST{StaNumb}等待PLC转盘转到位信号B1003");
                    //        if (ret != hardware.PLC_KEY.readRbit("B1003", 1, out Y))
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(100);
                    //            goto STEP4;
                    //        }
                    //        if (Y != "1")
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(100);
                    //            goto STEP4;
                    //        }                           
                    //        //转盘转到位，到达模号检测位置
                    //        Logger.Info("转到旋转到位");
                    //        RunBuf.Setsta(StaNumb, 3);
                           
                    //    }
                    //    if (RunBuf.GetStaValue(StaNumb) == 3)//到达模号检测位置
                    //    {
                    //        //等待PLC的触发信号B1161
                    //        Logger.Info($"ST{StaNumb}等待 PLC:模号检测触发信号B1161");
                    //    STEP1:
                    //        if (ret != hardware.PLC_KEY.readRbit("B1161", ref Y)) 
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(100);
                    //            goto STEP1;
                    //        }
                    //        if (Y != "1")//模号检测触发信号
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(110);
                    //            goto STEP1;
                    //        }
                    //        //请求视觉检模号
                    //        Logger.Info($"ST{StaNumb}请求视觉检模号");
                    //        hardware.viscam.ReceiveStringData = "";
                    //    STEP2:
                    //        if (ret != hardware.viscam.Sendb("start1"))//启动视觉检测模号
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP2;//直到发送成功
                    //        }
                    //        RunBuf.Setsta(StaNumb, 4);
                    //        Logger.Info($"ST{StaNumb}模号检测中");
                    //        //等待模号检测结果
                    //        while (hardware.viscam.ReceiveStringData.Length < 2)
                    //        {
                    //            if (Quit) return;
                    //            Thread.Sleep(250);
                    //            Application.DoEvents();
                    //        }
                    //        if (hardware.viscam.ReceiveStringData != "OK1") //----------------------等待最终协议
                    //        {
                    //            (RunBuf.GetStaRes(StaNumb)).sresult1 = false;
                    //            (RunBuf.GetStaRes(StaNumb)).MoNum = hardware.viscam.ReceiveStringData;//----------------------等待最终协议
                    //            RunBuf.Setsta(StaNumb, 5);
                    //            Logger.Error($"ST{StaNumb}模号检测NG");
                    //            //发送模号检测NG的结果给PLC
                    //            Logger.Error($"ST{StaNumb}发送模号检测NG结果给PLC：B1611 W1761");
                    //        STEP3:
                    //            if (ret != hardware.PLC_KEY.writebit("B1661", 1, "1"))
                    //            {
                    //                if (Quit) break;
                    //                Thread.Sleep(250);
                    //                goto STEP3;
                    //            }
                    //        STEP4:
                    //            if (ret != hardware.PLC_KEY.writebit("W1761", 1, "00002"))
                    //            {
                    //                if (Quit) break;
                    //                Thread.Sleep(250);
                    //                goto STEP4;
                    //            }
                    //            hardware.delijie1.GetCordOK = false;
                    //            if (Alarm.ShowDialog(Color.Red, "报警", "模号检测结果NG，是重新放一个测试，还是放弃？重新换个产品测请扫下二维码", "重新测", "放弃") == DialogResult.OK)
                    //            {
                    //                //重新测
                    //                if (!hardware.delijie1.GetCordOK) Alarm.ShowDialog(Color.Yellow, "未检测到扫码枪扫码动作，请扫码后放置产品到模号检测工位上", "警告！");
                    //                if (hardware.delijie1.GetCordOK) RunBuf.GetStaRes(StaNumb).ManualCord = hardware.delijie1.ReceiveStringData;//码更新下
                    //                                                                                                                            //不扫码也不管了
                    //                hardware.viscam.ReceiveStringData = "";
                    //                goto STEP2;
                    //            }
                    //            else
                    //            {
                    //                //不重新测一个 就往下走了
                    //            }
                    //        }
                    //        else//模号检测OK
                    //        {
                    //            (RunBuf.GetStaRes(StaNumb)).sresult1 = true;
                    //            (RunBuf.GetStaRes(StaNumb)).MoNum = hardware.viscam.ReceiveStringData;//----------------------等待最终协议
                    //            RunBuf.Setsta(StaNumb, 5);
                    //            Logger.Error($"ST{StaNumb}模号检测OK");
                    //            //发送模号检测NG的结果给PLC
                    //            Logger.Error($"ST{StaNumb}发送模号检测OK结果给PLC：B1611 W1761");
                    //        STEP5:
                    //            if (ret != hardware.PLC_KEY.writebit("B1661", 1, "1"))
                    //            {
                    //                if (Quit) break;
                    //                Thread.Sleep(250);
                    //                goto STEP5;
                    //            }
                    //        STEP6:
                    //            if (ret != hardware.PLC_KEY.writebit("W1761", 1, "00001"))
                    //            {
                    //                if (Quit) break;
                    //                Thread.Sleep(250);
                    //                goto STEP6;
                    //            }

                    //        }
                           
                    //    }
                    //    if (RunBuf.GetStaValue(StaNumb) == 5)//模号检测完成
                    //    {
                    //        Logger.Error($"ST{StaNumb}等待PLC转盘信号触发B1002");  
                    //    STEP1:
                    //        if (ret != hardware.PLC_KEY.readRbit("B1002", 1, out Y))
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP1;
                    //        }
                    //        if (Y != "1")//转盘触发旋转 前往模号检测位置中
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP1;
                    //        }
                    //        RunBuf.Setsta(StaNumb, 6);

                    //    STEP2:
                    //        //读取转盘转到位信号
                    //        Logger.Error($"ST{StaNumb}等待PLC转盘到位信号触发B1003");
                    //        if (ret != hardware.PLC_KEY.readRbit("B1003", 1, out Y))
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP2;
                    //        }
                    //        if (Y != "1")
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP2;
                    //        }
                    //        //转盘转到位，到达平整度检测位置
                    //        Logger.Info($"ST{StaNumb}转到旋转到到达平整度检测位置");
                    //        RunBuf.Setsta(StaNumb, 7);

                    //        //等待PLC的平整度检测的结果
                    //        Logger.Info($"ST{StaNumb}等待平整度检测结果B1671 ");
                    //    STEP3:
                    //        if (ret != hardware.PLC_KEY.readRbit("B1671", 1, out Y))
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP3;
                    //        }
                    //        if (Y != "1")//检测完成了没有
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP3;
                    //        }
                    //        RunBuf.Setsta(StaNumb, 9);
                    //    STEP4:
                    //        Logger.Info($"ST{StaNumb}读取平整度检测结果W1771 ");
                    //        if (ret != hardware.PLC_KEY.readRbit("W1771", 1, out Y))
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP4;
                    //        }
                    //        if (Y != "00001")//检测OK
                    //        {
                    //            (RunBuf.GetStaRes(StaNumb)).sresult2 = true;

                    //        }
                    //        else
                    //        {
                    //            (RunBuf.GetStaRes(StaNumb)).sresult2 = false;
                    //        }
                    //    // 读取数据
                    //    STEP5:
                    //        if (ret != hardware.PLC_KEY.readRbit("W1161", 6, out Y))
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP5;
                    //        }
                    //        (RunBuf.GetStaRes(StaNumb)).Tdat1 = 0;//平整度数据，待具体分解-----------------------------------------未完








                           


                    //    }
                    //    if (RunBuf.GetStaValue(StaNumb) == 9)//平整度测完了
                    //    {
                    //        //等待转盘信号 读取触发状态B1002	B1003
                    //        Logger.Info($"ST{StaNumb}等待转盘信号 读取触发状态B1002");
                    //    STEP1:
                            
                    //        if (ret != hardware.PLC_KEY.readRbit("B1002", 1, out Y))
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP1;
                    //        }
                    //        if (Y != "1")//转盘触发旋转 前往耐压检测位置中
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP1;
                    //        }
                    //        RunBuf.Setsta(StaNumb, 10);
                    //        Logger.Info($"ST{StaNumb}等待转盘到位信号 读取信号B1003");
                    //    STEP2:
                    //        //读取转盘转到位信号
                           
                    //        if (ret != hardware.PLC_KEY.readRbit("B1003", 1, out Y))
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP2;
                    //        }
                    //        if (Y != "1")
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250); ;
                    //            goto STEP2;
                    //        }
                    //        //转盘转到位，到达模号检测位置
                    //        Logger.Info($"ST{StaNumb}转到旋转到耐压检测位");
                    //        RunBuf.Setsta(StaNumb, 11);

                    //        //等待PLC的触发信号B1121
                    //        Logger.Info($"ST{StaNumb}等待耐压检测触发信号 PLC:B1121");
                    //    STEP3:
                    //        if (ret != hardware.PLC_KEY.readRbit("B1121", 1, out Y))
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP3;
                    //        }
                    //        if (Y != "1")//模号检测触发信号
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP3;
                    //        }
                    //        //请求耐压仪启动检测
                    //        Logger.Info($"ST{StaNumb}请求耐压仪启动检测");
                    //        hardware.test9803.ReceiveStringData = "";
                    //    STEP4:
                    //        if (ret != hardware.test9803.sends("STRAT"))//启动耐压仪检测--------------------等待最终协议
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP4;//直到发送成功
                    //        }
                    //        RunBuf.Setsta(StaNumb, 12);
                    //        Logger.Info($"ST{StaNumb}耐压检测中");
                    //        //等待耐压检测结果
                    //        while (hardware.test9803.ReceiveStringData.Length < 2)
                    //        {
                    //            if (Quit) return;
                    //            Thread.Sleep(250);
                    //            Application.DoEvents();
                    //        }
                    //        if (hardware.viscam.ReceiveStringData != "OK1") //----------------------等待最终协议
                    //        {
                    //            (RunBuf.GetStaRes(StaNumb)).sresult3 = false;
                    //            (RunBuf.GetStaRes(StaNumb)).Vdat1 = 0;//----------------    ------等待最终协议
                    //            RunBuf.Setsta(StaNumb, 13);
                    //            Logger.Error($"ST{StaNumb}耐压检测NG");
                    //            //发送模号检测NG的结果给PLC
                    //            Logger.Error($"ST{StaNumb}发送耐压检测NG结果给PLC B1621");
                    //        STEP5:
                    //            if (ret != hardware.PLC_KEY.writebit("B1621", 1, "1"))
                    //            {
                    //                if (Quit) break;
                    //                Thread.Sleep(250);
                    //                goto STEP5;
                    //            }
                    //        STEP6:
                    //            Logger.Error($"ST{StaNumb}发送耐压检测NG结果给PLC W1721");
                    //            if (ret != hardware.PLC_KEY.writebit("W1721", 1, "00002"))
                    //            {
                    //                if (Quit) break;
                    //                Thread.Sleep(250);
                    //                goto STEP6;
                    //            }

                    //            if (Alarm.ShowDialog(Color.Red, "报警", "耐压检测结果NG，是重新测试一次，还是放弃？ ", "重新测", "放弃") == DialogResult.OK)
                    //            {
                    //                hardware.test9803.ReceiveStringData = "";
                    //                goto STEP4;
                    //            }
                    //            else
                    //            {
                    //                //不重新测一个 就往下走了
                    //            }
                    //        }
                    //        else//耐压检测OK
                    //        {
                    //            (RunBuf.GetStaRes(StaNumb)).sresult3 = true;
                    //            (RunBuf.GetStaRes(StaNumb)).Vdat1 = 0;//----------------    ------等待最终协议
                    //            RunBuf.Setsta(StaNumb, 13);
                    //            Logger.Error($"ST{StaNumb}耐压检测OK");
                    //            //发送模号检测NG的结果给PLC
                    //            Logger.Error($"ST{StaNumb}发送模号检测OK结果给PLC B1621");
                    //        STEP5:
                    //            if (ret != hardware.PLC_KEY.writebit("B1621", 1, "1"))
                    //            {
                    //                if (Quit) break;
                    //                Thread.Sleep(250);
                    //                goto STEP5;
                    //            }
                    //            Logger.Error($"ST{StaNumb}发送模号检测OK结果给PLC W1721");
                    //        STEP6:
                                
                    //            if (ret != hardware.PLC_KEY.writebit("W1721", 1, "00001"))
                    //            {
                    //                if (Quit) break;
                    //                Thread.Sleep(250);
                    //                goto STEP6;
                    //            }

                    //        }
                            
                    //    }
                    //    if (RunBuf.GetStaValue(StaNumb) == 13)//耐压测完了
                    //    {
                    //        //等待转盘信号 读取触发状态B1002	B1003
                    //        Logger.Error($"ST{StaNumb}等待转盘信号 读取触发状态B1002");
                    //    STEP1:
                          
                    //        if (ret != hardware.PLC_KEY.readRbit("B1002", 1, out Y))
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP1;
                    //        }
                    //        if (Y != "1")//转盘触发旋转 前往耐压检测位置中
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP1;
                    //        }
                    //        RunBuf.Setsta(StaNumb, 14);
                    //        Logger.Error($"ST{StaNumb}等待转盘到位信号 B1003");
                    //    STEP2:
                    //        //读取转盘转到位信号
                    //        if (ret != hardware.PLC_KEY.readRbit("B1003", 1, out Y))
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP2;
                    //        }
                    //        if (Y != "1")
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP2;
                    //        }
                    //        //转盘转到位，到达模号检测位置
                    //        Logger.Info($"ST{StaNumb}转到旋转到螺帽检测位");
                    //        RunBuf.Setsta(StaNumb, 15);

                    //        //等待PLC的触发信号B1131
                    //        Logger.Info($"ST{StaNumb}等待螺帽检测触发信号 PLC:B1131");
                    //    STEP3:
                    //        if (ret != hardware.PLC_KEY.readRbit("B1131", 1, out Y))
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP3;
                    //        }
                    //        if (Y != "1")//模号检测触发信号
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP3;
                    //        }
                    //        //请求耐压仪启动检测
                    //        Logger.Info($"ST{StaNumb}请求视觉检测螺帽");
                    //    STEP4:
                    //        if (ret != hardware.viscam.Sendb("start2"))//启动视觉检测模号------------------------------------------等待最终协议
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP4;//直到发送成功
                    //        }
                    //        RunBuf.Setsta(StaNumb, 16);
                    //        Logger.Info($"ST{StaNumb}螺帽检测中");
                    //        //等待模号检测结果
                    //        while (hardware.viscam.ReceiveStringData.Length < 2)
                    //        {
                    //            if (Quit) return;
                    //            Thread.Sleep(250);
                    //            Application.DoEvents();
                    //        }
                    //        if (hardware.viscam.ReceiveStringData != "OK1") //------------- ----------------------------------等待最终协议
                    //        {
                    //            (RunBuf.GetStaRes(StaNumb)).sresult4 = false;
                    //            (RunBuf.GetStaRes(StaNumb)).LMcount = 1;//-------   ---------------------------  ---------------等待最终协议
                    //            RunBuf.Setsta(StaNumb, 17);
                    //            Logger.Error($"ST{StaNumb}螺帽检测NG");
                    //            //发送模号检测NG的结果给PLC
                    //            Logger.Error($"ST{StaNumb}发送螺帽检测NG结果给PLC B1631");
                    //        STEP5:
                    //            if (ret != hardware.PLC_KEY.writebit("B1631", 1, "1"))
                    //            {
                    //                if (Quit) break;
                    //                Thread.Sleep(250);
                    //                goto STEP5;
                    //            }
                    //            Logger.Error($"ST{StaNumb}发送螺帽检测NG结果给PLC W1731");
                    //        STEP6:
                    //            if (ret != hardware.PLC_KEY.writebit("W1731", 1, "00002"))
                    //            {
                    //                if (Quit) break;
                    //                Thread.Sleep(250);
                    //                goto STEP6;
                    //            }

                    //            if (Alarm.ShowDialog(Color.Red, "报警", "螺帽检测结果NG，是重新测试一次，还是放弃？", "重新测", "放弃") == DialogResult.OK)
                    //            {
                    //                hardware.viscam.ReceiveStringData = "";
                    //                goto STEP4;
                    //            }
                    //            else
                    //            {
                    //                //不重新测一个 就往下走了
                    //            }
                    //        }
                    //        else//螺帽检测OK
                    //        {
                    //            (RunBuf.GetStaRes(StaNumb)).sresult4 = true;
                    //            (RunBuf.GetStaRes(StaNumb)).LMcount = 0;//-----------------------------------  ------等待最终协议
                    //            RunBuf.Setsta(StaNumb, 17);
                    //            Logger.Error($"ST{StaNumb}螺帽检测OK");
                    //            //发送模号检测NG的结果给PLC
                    //            Logger.Error($"ST{StaNumb}发送螺帽检测OK结果给PLC B1631");
                    //        STEP5:
                    //            if (ret != hardware.PLC_KEY.writebit("B1631", 1, "1"))
                    //            {
                    //                if (Quit) break;
                    //                Thread.Sleep(250);
                    //                goto STEP5;
                    //            }
                    //            Logger.Error($"ST{StaNumb}发送螺帽检测OK结果给PLC W1731");
                    //        STEP6:
                    //            if (ret != hardware.PLC_KEY.writebit("W1731", 1, "00001"))
                    //            {
                    //                if (Quit) break;
                    //                Thread.Sleep(250);
                    //                goto STEP6;
                    //            }
                    //        }
                            
                    //    }
                    //    if (RunBuf.GetStaValue(StaNumb) == 17)//螺帽测完了
                    //    {
                    //        //等待转盘信号 读取触发状态B1002	B1003
                    //        Logger.Info($"ST{StaNumb}等待转盘信号 读取触发状态B1002");
                    //    STEP1:
                    //        if (ret != hardware.PLC_KEY.readRbit("B1002", 1, out Y))
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP1;
                    //        }
                    //        if (Y != "1")//转盘触发旋转 前往下料置中
                    //        {
                    //            if(Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP1;
                    //        }
                    //        RunBuf.Setsta(StaNumb, 18);
                    //        Logger.Info($"ST{StaNumb}等待转盘到位信号 读取B1003");
                    //    STEP2:
                    //        //读取转盘转到位信号
                    //        if (ret != hardware.PLC_KEY.readRbit("B1003", 1, out Y))
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP2;
                    //        }
                    //        if (Y != "1")
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP2;
                    //        }
                    //        //转盘转到位，到达扫码下料位置
                    //        Logger.Info($"ST{StaNumb}旋转到扫码下料位");
                    //        RunBuf.Setsta(StaNumb, 19);

                    //        //等待PLC的触发信号B1010
                    //        Logger.Info($"ST{StaNumb}等待扫码下料触发信号 PLC:B1010");
                    //    STEP3:
                    //        if (ret != hardware.PLC_KEY.readRbit("B1010", 1, out Y))
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            goto STEP3;
                    //        }
                    //        if (Y != "1")//扫码比对触发信号
                    //        {
                    //            if (Quit) break;
                    //            Thread.Sleep(250);
                    //            Thread.Sleep(120);
                    //            goto STEP3;
                    //        }
                    //        //启动扫码器扫码
                    //        Logger.Info($"ST{StaNumb}启动扫码器扫码");
                    //        hardware.jienshi1.ReceiveStringData = "";
                    //    STEP4:
                    //        if (ret != hardware.jienshi1.Sendb("LON\u000d"))//启动扫码
                    //        {
                    //            goto STEP4;//直到发送成功
                    //        }
                    //        RunBuf.Setsta(StaNumb, 20);
                    //        Logger.Info($"ST{StaNumb}开始扫码中");
                    //        //等待扫码检测结果
                    //        while (hardware.jienshi1.ReceiveStringData.Length < 2)
                    //        {
                    //            Application.DoEvents();
                    //            if (Quit) return;
                    //            Thread.Sleep(250);
                    //        }
                    //        if (hardware.viscam.ReceiveStringData == "ERROR")
                    //        {
                    //            (RunBuf.GetStaRes(StaNumb)).sresult5 = false;
                    //            (RunBuf.GetStaRes(StaNumb)).AutoCode = hardware.jienshi1.ReceiveStringData;//----------------    ------等待最终协议
                    //            RunBuf.Setsta(StaNumb, 21);
                    //            Logger.Error($"ST{StaNumb}扫码比对NG，没有扫码到");
                    //            //发送模号检测NG的结果给PLC
                    //            Logger.Error($"ST{StaNumb}发送扫码比对NG结果给PLC B1510");
                    //        STEP5:
                    //            if (ret != hardware.PLC_KEY.writebit("B1510", 1, "1"))
                    //            {
                    //                if (Quit) break;
                    //                Thread.Sleep(250);
                    //                goto STEP5;
                    //            }
                    //            Logger.Error($"ST{StaNumb}发送扫码比对NG结果给PLC W1520");
                    //        STEP6:
                                
                    //            if (ret != hardware.PLC_KEY.writebit("W1520", 1, "00002"))
                    //            {
                    //                if (Quit) break;
                    //                Thread.Sleep(250);
                    //                goto STEP6;
                    //            }

                    //            if (Alarm.ShowDialog(Color.Red, "报警", "扫码器未扫到二维码，是重新测试一次，还是放弃？ ", "重新测", "放弃") == DialogResult.OK)
                    //            {
                    //                hardware.jienshi1.ReceiveStringData = "";
                    //                goto STEP4;
                    //            }
                    //            else
                    //            {
                    //                //不重新测一个 就往下走了
                    //            }
                    //        }
                    //        else//进行扫码比对，并存储信息
                    //        {
                    //            if (hardware.viscam.ReceiveStringData != (RunBuf.GetStaRes(StaNumb)).ManualCord)//比对失败
                    //            {
                    //                (RunBuf.GetStaRes(StaNumb)).sresult5 = false;
                    //                (RunBuf.GetStaRes(StaNumb)).AutoCode = hardware.jienshi1.ReceiveStringData;// 
                    //                RunBuf.Setsta(StaNumb, 21);
                    //                Logger.Error($"ST{StaNumb}扫码比对NG，上料扫码{(RunBuf.GetStaRes(StaNumb)).ManualCord}下料扫码{(RunBuf.GetStaRes(StaNumb)).AutoCode}");
                    //                //发送模号检测NG的结果给PLC
                    //                Logger.Error($"ST{StaNumb}发送扫码比对NG结果给PLC B1510");
                    //            STEP7:
                    //                if (ret != hardware.PLC_KEY.writebit("B1510", 1, "1"))
                    //                {
                    //                    goto STEP7;
                    //                }
                    //                Logger.Error($"ST{StaNumb}发送扫码比对NG结果给PLC W1520");
                    //            STEP8:
                    //                if (ret != hardware.PLC_KEY.writebit("W1520", 1, "00002"))
                    //                {
                    //                    goto STEP8;
                    //                }
                    //            }
                    //            else//比对成功
                    //            {
                    //                (RunBuf.GetStaRes(StaNumb)).sresult5 = true;
                    //                (RunBuf.GetStaRes(StaNumb)).AutoCode = hardware.viscam.ReceiveStringData;
                    //                RunBuf.Setsta(StaNumb, 13);
                    //                Logger.Error($"ST{StaNumb}耐压检测OK");
                    //                //发送模号检测NG的结果给PLC
                    //                Logger.Error($"ST{StaNumb}发送模号检测OK结果给PLC B1510");
                    //            STEP9:
                    //                if (ret != hardware.PLC_KEY.writebit("B1510", 1, "1"))
                    //                {
                    //                    goto STEP9;
                    //                }
                    //                Logger.Error($"ST{StaNumb}发送模号检测OK结果给PLC W1520");
                    //            STEP10:
                    //                if (ret != hardware.PLC_KEY.writebit("W1520", 1, "00001"))
                    //                {
                    //                    goto STEP10;
                    //                }
                    //            }
                    //        }
                    //        //存数据
                    //        RunBuf.SaveDB(StaNumb);
                    //        RunBuf.Setsta(StaNumb, 0);
                    //    }

                    //}

                }, TaskCreationOptions.None);
                WaitCord.Start();


            //}           
            //else
            //{
            //    Logger.Error($"ST{StaNumb}RunWaitCordTask线程未退出 ,无法创建!");
            //    MessageBox.Show($"ST{StaNumb}RunWaitCordTask线程未退出 ,无法创建!");
            //}
        }
        /// <summary>
        /// 继续运行
        /// </summary>
        /// <param name="x">工位号</param>
        public void GoStation()
        {
            EmRes ret = EmRes.Succeed;
            string Y = "";
        //STEP0:
        //    if (RunBuf.GetStaValue(StaNumb) == 0)
        //    {
        //        RunWaitCordTask();
        //    }
        //    else if (RunBuf.GetStaValue(StaNumb) == 1)

        //    {
        //        if (RunBuf.GetStaRes(StaNumb).ManualCord.Length > 1)//如果手动有码
        //        {
        //            Logger.Info($"ST{StaNumb}发送扫码结果给PLC:B1601 ");
        //        STEP1:
        //            if (ret != hardware.PLC_KEY.writebit("B1601", 1, "1"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(200);
        //                goto STEP1;
        //            }
        //            Logger.Info($"ST{StaNumb}发送扫码结果给PLC: W1701");
        //        STEP2:
        //            if (ret != hardware.PLC_KEY.writebit("W1701", 1, "00001"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(200);
        //                goto STEP2;
        //            }
        //            //等待转盘信号 读取触发状态B1002	B1003
        //            Logger.Info($"ST{StaNumb}等待转盘信号 读取触发状态B1002");
        //        STEP3:
        //            if (ret != hardware.PLC_KEY.readRbit("B1002", 1, out Y))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(200);
        //                goto STEP3;
        //            }
        //            if (Y != "1")//转盘触发旋转 前往模号检测位置中
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(200);
        //                goto STEP3;
        //            }
        //            RunBuf.Setsta(StaNumb, 2);
        //            hardware.delijie1.ReceiveStringData = "";//清除掉扫码枪扫到的码
        //            Logger.Info($"ST{StaNumb}等待转盘到位信号  B1003");
        //        STEP4:
        //            //读取转盘转到位信号
        //            if (ret != hardware.PLC_KEY.readRbit("B1003", 1, out Y))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(200);
        //                goto STEP4;
        //            }
        //            if (Y != "1")
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(200);
        //                goto STEP4;
        //            }                 
        //            //转盘转到位，到达模号检测位置
        //            Logger.Info("转到旋转到位");
        //            RunBuf.Setsta(StaNumb, 3);
        //             RunWaitCordTask();
        //            return;
        //        }
        //        else
        //        {
        //            Alarm.ShowDialog(Color.Yellow, "警告！", "上料位置未检测到二维码，请重新扫码");
        //            RunBuf.Setsta(StaNumb, 0);
        //            goto STEP0;
        //        }
        //    }
        //    else if (RunBuf.GetStaValue(StaNumb) == 2)
        //    {
        //        Logger.Info("读取转盘转到位信号中B1003……");
        //    STEP4:
        //        //读取转盘转到位信号
        //        if (ret != hardware.PLC_KEY.readRbit("B1003", 1, out Y))
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            goto STEP4;
        //        }
        //        if (Y != "1")
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            goto STEP4;
        //        }
        //        RunBuf.Setsta(StaNumb, 3);
        //        RunWaitCordTask();              
        //        //转盘转到位，到达模号检测位置
        //        Logger.Info("转盘转到旋转到位");              
        //    }
        //    else if (RunBuf.GetStaValue(StaNumb) == 3)
        //    {
        //        RunWaitCordTask();
        //    }
        //    else if (RunBuf.GetStaValue(StaNumb) == 4)
        //    {
        //        //请求视觉检模号
        //        Logger.Info($"ST{StaNumb}请求视觉检模号");
        //        hardware.viscam.ReceiveStringData = "";
        //    STEP2:
        //        if (ret != hardware.viscam.Sendb("start1"))//启动视觉检测模号
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            goto STEP2;//直到发送成功
        //        }
        //        Logger.Info($"ST1模号检测中");
        //        //等待模号检测结果
        //        while (hardware.viscam.ReceiveStringData.Length < 2)
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            Application.DoEvents();
        //        }
        //        if (hardware.viscam.ReceiveStringData != "OK1") //----------------------等待最终协议
        //        {
        //            (RunBuf.GetStaRes(StaNumb)).sresult1 = false;
        //            (RunBuf.GetStaRes(StaNumb)).MoNum = hardware.viscam.ReceiveStringData;//----------------------等待最终协议
        //            RunBuf.Setsta(StaNumb, 5);
        //            Logger.Error($"ST{StaNumb}模号检测NG");
        //            //发送模号检测NG的结果给PLC
        //            Logger.Error($"ST{StaNumb}发送模号检测NG结果给PLC B1661");
        //        STEP3:
        //            if (ret != hardware.PLC_KEY.writebit("B1661", 1, "1"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(200);
        //                goto STEP3;
        //            }
        //            Logger.Error($"ST{StaNumb}发送模号检测NG结果给PLC W1761");
        //        STEP4:
        //            if (ret != hardware.PLC_KEY.writebit("W1761", 1, "00002"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(200);
        //                goto STEP4;
        //            }
        //            hardware.delijie1.GetCordOK = false;
        //            if (Alarm.ShowDialog(Color.Red, "报警", "模号检测结果NG，是重新放一个测试，还是放弃？重新换个产品测请扫下二维码", "重新测", "放弃") == DialogResult.OK)
        //            {
        //                //重新测
        //                if (!hardware.delijie1.GetCordOK) Alarm.ShowDialog(Color.Yellow, "未检测到扫码枪扫码动作，请扫码后放置产品到模号检测工位上", "警告！");
        //                if (hardware.delijie1.GetCordOK) RunBuf.GetStaRes(StaNumb).ManualCord = hardware.delijie1.ReceiveStringData;//码更新下                                                                                                                           //不扫码也不管了
        //                hardware.viscam.ReceiveStringData = "";
        //                goto STEP2;
        //            }
        //            else
        //            {
        //                //不重新测一个 就往下走了
        //            }
        //        }
        //        else//模号检测OK
        //        {
        //            (RunBuf.GetStaRes(StaNumb)).sresult1 = true;
        //            (RunBuf.GetStaRes(StaNumb)).MoNum = hardware.viscam.ReceiveStringData;//----------------------等待最终协议
        //            RunBuf.Setsta(StaNumb, 5);
        //            Logger.Error($"ST{StaNumb}模号检测OK");
        //            //发送模号检测NG的结果给PLC
        //            Logger.Error($"ST{StaNumb}发送模号检测OK结果给PLC B1661");
        //        STEP5:
        //            if (ret != hardware.PLC_KEY.writebit("B1661", 1, "1"))
        //            {
        //                goto STEP5;
        //            }
        //            Logger.Error($"ST{StaNumb}发送模号检测OK结果给PLC W1761");
        //        STEP6:
        //            if (ret != hardware.PLC_KEY.writebit("W1761", 1, "00001"))
        //            {
        //                goto STEP6;
        //            }

        //        }
        //        RunWaitCordTask();
        //    }
        //    else if (RunBuf.GetStaValue(StaNumb) == 5)
        //    {
        //        if (RunBuf.GetStaRes(StaNumb).sresult1 == false)
        //        {
        //            if (Alarm.ShowDialog(Color.Red, "报警", "模号检测结果NG，是重新放一个测试，还是放弃？重新换个产品测请扫下二维码", "重新测", "放弃") == DialogResult.OK)
        //            {
        //                //重新测
        //                if (!hardware.delijie1.GetCordOK) Alarm.ShowDialog(Color.Yellow, "未检测到扫码枪扫码动作，请扫码后放置产品到模号检测工位上", "警告！");
        //                if (hardware.delijie1.GetCordOK) RunBuf.GetStaRes(StaNumb).ManualCord = hardware.delijie1.ReceiveStringData;//码更新下
        //                                                                                                                     //不扫码也不管了
        //                hardware.viscam.ReceiveStringData = "";
        //                RunBuf.Setsta(StaNumb, 4);
        //                goto STEP0;
        //            }
        //        }
        //        RunWaitCordTask();
        //    }
        //    else if (RunBuf.GetStaValue(StaNumb) == 6)
        //    {
        //        Logger.Info($"ST{StaNumb}读取转盘转到位信号B1003");
        //    STEP2:
        //        //读取转盘转到位信号
        //        if (ret != hardware.PLC_KEY.readRbit("B1003", 1, out Y))
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            goto STEP2;
        //        }
        //        if (Y != "1")
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            goto STEP2;
        //        }
        //        //转盘转到位，到达模号检测位置
        //        Logger.Info($"ST{StaNumb}转到旋转到位");
        //        RunBuf.Setsta(StaNumb, 7);
        //        //等待PLC的平整度检测的结果
        //        Logger.Info($"ST{StaNumb}等待平整度检测结果B1671");
        //    STEP3:
        //        if (ret != hardware.PLC_KEY.readRbit("B1671", 1, out Y))
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            goto STEP3;
        //        }
        //        if (Y != "1")//检测完成了没有
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            goto STEP3;
        //        }
        //        RunBuf.Setsta(StaNumb, 9);
        //        Logger.Info($"ST{StaNumb}等待平整度检测结果W1771");
        //    STEP4:
        //        if (ret != hardware.PLC_KEY.readRbit("W1771", 1, out Y))
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            goto STEP4;
        //        }
        //        if (Y != "00001")//检测OK
        //        {
        //            (RunBuf.GetStaRes(StaNumb)).sresult2 = true;

        //        }
        //        else
        //        {
        //            (RunBuf.GetStaRes(StaNumb)).sresult2 = false;
        //        }
        //        // 读取数据
        //        Logger.Info($"ST{StaNumb}读取平整度检测结果W1161");
        //    STEP5:
        //        if (ret != hardware.PLC_KEY.readRbit("W1161", 6, out Y))
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            goto STEP5;
        //        }
        //       (RunBuf.GetStaRes(StaNumb)).Tdat1 = 0;//平整度数据，待具体分解-----------------------------------------未完




        //       RunWaitCordTask();
               
        //    }
        //    else if (RunBuf.GetStaValue(StaNumb) == 7)
        //    {
        //        //等待PLC的平整度检测的结果

        //        Logger.Info($"ST{StaNumb}等待平整度检测结果B1671");
        //    STEP3:
        //        if (ret != hardware.PLC_KEY.readRbit("B1671", 1, out Y))
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            goto STEP3;
        //        }
        //        if (Y != "1")//检测完成了没有
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            goto STEP3;
        //        }
        //        RunBuf.Setsta(StaNumb, 9);
        //        Logger.Info($"ST{StaNumb}等待平整度检测结果W1771");
        //    STEP4:
        //        if (ret != hardware.PLC_KEY.readRbit("W1771", 1, out Y))
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            goto STEP4;
        //        }
        //        if (Y != "00001")//检测OK
        //        {
        //            (RunBuf.GetStaRes(StaNumb)).sresult2 = true;
        //        }
        //        else
        //        {
        //            (RunBuf.GetStaRes(StaNumb)).sresult2 = false;
        //        }
        //        // 读取数据
        //        Logger.Info($"ST{StaNumb}读取平整度检测结果W1161");
        //    STEP5:
        //        if (ret != hardware.PLC_KEY.readRbit("W1161", 6, out Y))
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            goto STEP5;
        //        }
        //       (RunBuf.GetStaRes(StaNumb)).Tdat1 = 0;//平整度数据，待具体分解-----------------------------------------未完




        //        RunWaitCordTask();             
        //    }
        //    else if (RunBuf.GetStaValue(StaNumb) == 9)
        //    {
        //       RunWaitCordTask();
        //    }
        //    else if (RunBuf.GetStaValue(StaNumb) == 10)
        //    {
        //        Logger.Info($"ST{StaNumb}读取读取转盘转到位信号B1003");
        //    STEP2:
        //        //读取转盘转到位信号
        //        if (ret != hardware.PLC_KEY.readRbit("B1003", 1, out Y))
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            goto STEP2;
        //        }
        //        if (Y != "1")
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            goto STEP2;
        //        }
        //        //转盘转到位，到达模号检测位置
        //        Logger.Info($"ST{StaNumb}转到旋转到耐压检测位");
        //        RunBuf.Setsta(StaNumb, 11);

        //        //等待PLC的触发信号B1121
        //        Logger.Info($"ST{StaNumb}等待耐压检测触发信号 PLC:B1121");
        //    STEP3:
        //        if (ret != hardware.PLC_KEY.readRbit("B1121", 1, out Y))
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            goto STEP3;
        //        }
        //        if (Y != "1")//模号检测触发信号
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200); 
        //            goto STEP3;
        //        }
        //        //请求耐压仪启动检测
        //        Logger.Info($"ST{StaNumb}请求耐压仪启动检测");
        //        hardware.test9803.ReceiveStringData = "";
        //    STEP4:
        //        if (ret != hardware.test9803.sends("STRAT"))//启动耐压仪检测--------------------等待最终协议
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            goto STEP4;//直到发送成功
        //        }
        //        RunBuf.Setsta(StaNumb, 12);
        //        Logger.Info($"ST{StaNumb}耐压检测中");
        //        //等待耐压检测结果
        //        while (hardware.test9803.ReceiveStringData.Length < 2)
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            Application.DoEvents();
        //        }
        //        if (hardware.viscam.ReceiveStringData != "OK1") //-----------------------------------等待最终协议
        //        {
        //            (RunBuf.GetStaRes(StaNumb)).sresult3 = false;
        //            (RunBuf.GetStaRes(StaNumb)).Vdat1 = 0;//----------------  --------------  ------等待最终协议

        //            RunBuf.Setsta(StaNumb, 13);
        //            Logger.Error($"ST{StaNumb}耐压检测NG");
        //            //发送模号检测NG的结果给PLC
        //            Logger.Error($"ST{StaNumb}发送耐压检测NG结果给PLC B1621");
        //        STEP5:
        //            if (ret != hardware.PLC_KEY.writebit("B1621", 1, "1"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(200);
        //                goto STEP5;
        //            }
        //            Logger.Error($"ST{StaNumb}发送耐压检测NG结果给PLC W1721");
        //        STEP6:
        //            if (ret != hardware.PLC_KEY.writebit("W1721", 1, "00002"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(200);
        //                goto STEP6;
        //            }

        //            if (Alarm.ShowDialog(Color.Red, "报警", "耐压检测结果NG，是重新测试一次，还是放弃？ ", "重新测", "放弃") == DialogResult.OK)
        //            {
        //                hardware.test9803.ReceiveStringData = "";
        //                goto STEP4;
        //            }
        //            else
        //            {
        //                //不重新测一个 就往下走了
        //            }
        //        }
        //        else//耐压检测OK
        //        {
        //            (RunBuf.GetStaRes(StaNumb)).sresult3 = true;
        //            (RunBuf.GetStaRes(StaNumb)).Vdat1 = 0;//----------------    ------------------------等待最终协议

        //            RunBuf.Setsta(StaNumb, 13);
        //            Logger.Error($"ST{StaNumb}耐压检测OK");
        //            //发送模号检测NG的结果给PLC
        //            Logger.Info($"ST{StaNumb}发送模号检测OK结果给PLC B1621");
        //        STEP5:
        //            if (ret != hardware.PLC_KEY.writebit("B1621", 1, "1"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(200);
        //                goto STEP5;
        //            }
        //            Logger.Info($"ST{StaNumb}发送模号检测OK结果给PLC W1721");
        //        STEP6:
        //            if (ret != hardware.PLC_KEY.writebit("W1721", 1, "00001"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(200);
        //                goto STEP6;
        //            }

        //        }

        //       RunWaitCordTask();            
        //    }
        //    else if (RunBuf.GetStaValue(StaNumb) == 11)
        //    {
        //        //等待PLC的触发信号B1121
        //        Logger.Info($"ST{StaNumb}等待耐压检测触发信号 PLC:B1121");
        //    STEP3:
        //        if (ret != hardware.PLC_KEY.readRbit("B1121", 1, out Y))
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            goto STEP3;
        //        }
        //        if (Y != "1")//模号检测触发信号
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            goto STEP3;
        //        }
        //        //请求耐压仪启动检测
        //        Logger.Info($"ST{StaNumb}请求耐压仪启动检测");
        //        hardware.test9803.ReceiveStringData = "";
        //    STEP4:
        //        if (ret != hardware.test9803.sends("STRAT"))//启动耐压仪检测--------------------等待最终协议
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            goto STEP4;//直到发送成功
        //        }
        //        RunBuf.Setsta(StaNumb, 12);
        //        Logger.Info($"ST{StaNumb}耐压检测中");
        //        //等待耐压检测结果
        //        while (hardware.test9803.ReceiveStringData.Length < 2)
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(500);
        //            Application.DoEvents();
        //        }
        //        if (hardware.viscam.ReceiveStringData != "OK1") //-----------------------------等待最终协议
        //        {
        //            (RunBuf.GetStaRes(StaNumb)).sresult3 = false;
        //            (RunBuf.GetStaRes(StaNumb)).Vdat1 = 0;//----------------  --------------- ------等待最终协议
        //            RunBuf.Setsta(StaNumb, 13);
        //            Logger.Error($"ST{StaNumb}耐压检测NG");
        //            //发送模号检测NG的结果给PLC
        //            Logger.Error($"ST{StaNumb}发送耐压检测NG结果给PLC B1621");
        //        STEP5:
        //            if (ret != hardware.PLC_KEY.writebit("B1621", 1, "1"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(200);
        //                goto STEP5;
        //            }
        //            Logger.Error($"ST{StaNumb}发送耐压检测NG结果给PLC W1721");
        //        STEP6:
        //            if (ret != hardware.PLC_KEY.writebit("W1721", 1, "00002"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(200);
        //                goto STEP6;
        //            }

        //            if (Alarm.ShowDialog(Color.Red, "报警", "耐压检测结果NG，是重新测试一次，还是放弃？ ", "重新测", "放弃") == DialogResult.OK)
        //            {
        //                hardware.test9803.ReceiveStringData = "";
        //                goto STEP4;
        //            }
        //            else
        //            {
        //                //不重新测一个 就往下走了
        //            }
        //        }
        //        else//耐压检测OK
        //        {
        //            (RunBuf.GetStaRes(StaNumb)).sresult3 = true;
        //            (RunBuf.GetStaRes(StaNumb)).Vdat1 = 0;//----------------    ------等待最终协议
        //            RunBuf.Setsta(StaNumb, 13);
        //            Logger.Info($"ST{StaNumb}耐压检测OK");
        //            //发送模号检测NG的结果给PLC
        //            Logger.Info($"ST{StaNumb}发送模号检测OK结果给PLC B1621");
        //        STEP5:
        //            if (ret != hardware.PLC_KEY.writebit("B1621", 1, "1"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(200);
        //                goto STEP5;
        //            }
        //            Logger.Info($"ST{StaNumb}发送模号检测OK结果给PLC W1721");
        //        STEP6:
        //            if (ret != hardware.PLC_KEY.writebit("W1721", 1, "00001"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(200);
        //                goto STEP6;
        //            }
        //        }
        //        RunWaitCordTask();
                
        //    }
        //    else if (RunBuf.GetStaValue(StaNumb) == 12)
        //    {
        //        Logger.Info($"ST{StaNumb}请求耐压仪启动检测");
        //        hardware.test9803.ReceiveStringData = "";
        //    STEP4:
        //        if (ret != hardware.test9803.sends("STRAT"))//启动耐压仪检测---------------------------------等待最终协议
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            goto STEP4;//直到发送成功
        //        }
        //        Logger.Info($"ST{StaNumb}耐压检测中");
        //        //等待耐压检测结果
        //        while (hardware.test9803.ReceiveStringData.Length < 2)
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(500);
        //            Application.DoEvents();
        //        }
        //        if (hardware.viscam.ReceiveStringData != "OK1") //-------------------------------------等待最终协议
        //        {
        //            (RunBuf.GetStaRes(StaNumb)).sresult3 = false;
        //            (RunBuf.GetStaRes(StaNumb)).Vdat1 = 0;//----------------  --------------  ------等待最终协议
        //            RunBuf.Setsta(StaNumb, 13);
        //            Logger.Error($"ST{StaNumb}耐压检测NG");
        //            //发送模号检测NG的结果给PLC
        //            Logger.Error($"ST{StaNumb}发送耐压检测NG结果给PLC B1621");
        //        STEP5:
        //            if (ret != hardware.PLC_KEY.writebit("B1621", 1, "1"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(200);
        //                goto STEP5;
        //            }
        //            Logger.Error($"ST{StaNumb}发送耐压检测NG结果给PLC W1721");
        //        STEP6:
        //            if (ret != hardware.PLC_KEY.writebit("W1721", 1, "00002"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(200);
        //                goto STEP6;
        //            }

        //            if (Alarm.ShowDialog(Color.Red, "报警", "耐压检测结果NG，是重新测试一次，还是放弃？ ", "重新测", "放弃") == DialogResult.OK)
        //            {
        //                hardware.test9803.ReceiveStringData = "";
        //                goto STEP4;
        //            }
        //            else
        //            {
        //                //不重新测一个 就往下走了
        //            }
        //        }
        //        else//耐压检测OK
        //        {
        //            (RunBuf.GetStaRes(StaNumb)).sresult3 = true;
        //            (RunBuf.GetStaRes(StaNumb)).Vdat1 = 0;//----------------    ------等待最终协议
        //            RunBuf.Setsta(StaNumb, 13);
        //            Logger.Info($"ST{StaNumb}耐压检测OK");
        //            //发送模号检测NG的结果给PLC
        //            Logger.Info($"ST{StaNumb}发送模号检测OK结果给PLC B1621");
        //        STEP5:
        //            if (ret != hardware.PLC_KEY.writebit("B1621", 1, "1"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(200);
        //                goto STEP5;
        //            }
        //            Logger.Info($"ST{StaNumb}发送模号检测OK结果给PLC W1721");
        //        STEP6:
        //            if (ret != hardware.PLC_KEY.writebit("W1721", 1, "00001"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(200);
        //                goto STEP6;
        //            }

        //        }

        //       RunWaitCordTask();
              
        //    }
        //    else if (RunBuf.GetStaValue(StaNumb) == 13)
        //    {
        //        if ((RunBuf.GetStaRes(StaNumb)).sresult3 == false)
        //        {
        //            if (Alarm.ShowDialog(Color.Red, "报警", "耐压检测结果NG，是重新测试一次，还是放弃？ ", "重新测", "放弃") == DialogResult.OK)
        //            {
        //                hardware.test9803.ReceiveStringData = "";
        //                RunBuf.Setsta(StaNumb, 12);
        //                goto STEP0;
        //            }
        //        }
        //        RunWaitCordTask();
             
        //    }
        //    else if (RunBuf.GetStaValue(StaNumb) == 14)
        //    {
        //        Logger.Info($"ST{StaNumb}读取转盘转到位信号PLC B1003");
        //    STEP2:
        //        //读取转盘转到位信号
        //        if (ret != hardware.PLC_KEY.readRbit("B1003", 1, out Y))
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(200);
        //            goto STEP2;
        //        }
        //        if (Y != "1")
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(210);
        //            goto STEP2;
        //        }
        //        //转盘转到位，到达模号检测位置
        //        Logger.Info($"ST{StaNumb}转到旋转到螺帽检测位");
        //        RunBuf.Setsta(StaNumb, 15);

        //        //等待PLC的触发信号B1131
        //        Logger.Info($"ST{StaNumb}等待螺帽检测触发信号 PLC:B1131");
        //    STEP3:
        //        if (ret != hardware.PLC_KEY.readRbit("B1131", 1, out Y))
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(210);
        //            goto STEP3;
        //        }
        //        if (Y != "1")//模号检测触发信号
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(210);
        //            goto STEP3;
        //        }
        //        //请求耐压仪启动检测
        //        Logger.Info($"ST{StaNumb}请求视觉检测螺帽");
        //    STEP4:
        //        if (ret != hardware.viscam.Sendb("start2"))//启动视觉检测模号------------------------------------------等待最终协议
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(210);
        //            goto STEP4;//直到发送成功
        //        }
        //        RunBuf.Setsta(StaNumb, 16);
        //        Logger.Info($"ST{StaNumb}螺帽检测中");
        //        //等待模号检测结果
        //        while (hardware.viscam.ReceiveStringData.Length < 2)
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(510);
        //            Application.DoEvents();
        //        }
        //        if (hardware.viscam.ReceiveStringData != "OK1") //------------- ----------------------------------等待最终协议
        //        {
        //            (RunBuf.GetStaRes(StaNumb)).sresult4 = false;
        //            (RunBuf.GetStaRes(StaNumb)).LMcount = 1;//-------   ---------------------------  ---------------等待最终协议
        //            RunBuf.Setsta(StaNumb, 17);
        //            Logger.Error($"ST{StaNumb}螺帽检测NG");
        //            //发送模号检测NG的结果给PLC
        //            Logger.Error($"ST{StaNumb}发送螺帽检测NG结果给PLC B1631");
        //        STEP5:
        //            if (ret != hardware.PLC_KEY.writebit("B1631", 1, "1"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(210);
        //                goto STEP5;
        //            }
        //            Logger.Error($"ST{StaNumb}发送螺帽检测NG结果给PLC W1731");
        //        STEP6:
        //            if (ret != hardware.PLC_KEY.writebit("W1731", 1, "00002"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(210);
        //                goto STEP6;
        //            }

        //            if (Alarm.ShowDialog(Color.Red, "报警", "螺帽检测结果NG，是重新测试一次，还是放弃？", "重新测", "放弃") == DialogResult.OK)
        //            {
        //                hardware.viscam.ReceiveStringData = "";
        //                goto STEP4;
        //            }
        //            else
        //            {
        //                //不重新测一个 就往下走了
        //            }
        //        }
        //        else//螺帽检测OK
        //        {
        //            (RunBuf.GetStaRes(StaNumb)).sresult4 = true;
        //            (RunBuf.GetStaRes(StaNumb)).LMcount = 0;//-----------------------------------  ------等待最终协议
        //            RunBuf.Setsta(StaNumb, 17);
        //            Logger.Info($"ST{StaNumb}螺帽检测OK");
        //            //发送模号检测NG的结果给PLC
        //            Logger.Info($"ST{StaNumb}发送螺帽检测OK结果给PLC B1631");
        //        STEP5:
        //            if (ret != hardware.PLC_KEY.writebit("B1631", 1, "1"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(210);
        //                goto STEP5;
        //            }
        //            Logger.Info($"ST{StaNumb}发送螺帽检测OK结果给PLC W1731");
        //        STEP6:
        //            if (ret != hardware.PLC_KEY.writebit("W1731", 1, "00001"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(210);
        //                goto STEP6;
        //            }
        //        }
        //        RunWaitCordTask();
                
        //    }
        //    else if (RunBuf.GetStaValue(StaNumb) == 15)
        //    {
        //        //等待PLC的触发信号B1131
        //        Logger.Info($"ST{StaNumb}等待螺帽检测触发信号 PLC:B1131");
        //    STEP3:
        //        if (ret != hardware.PLC_KEY.readRbit("B1131", 1, out Y))
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(210);
        //            goto STEP3;
        //        }
        //        if (Y != "1")//模号检测触发信号
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(210);
        //            goto STEP3;
        //        }
        //        //请求螺帽启动检测
        //        Logger.Info($"ST{StaNumb}请求视觉检测螺帽");
        //    STEP4:
        //        if (ret != hardware.viscam.Sendb("start2"))//启动视觉检测模号------------------------------------------等待最终协议
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(210);
        //            goto STEP4;//直到发送成功
        //        }
        //        RunBuf.Setsta(StaNumb, 16);
        //        Logger.Info($"ST{StaNumb}螺帽检测中");
        //        //等待模号检测结果
        //        while (hardware.viscam.ReceiveStringData.Length < 2)
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(510);
        //            Application.DoEvents();
        //        }
        //        if (hardware.viscam.ReceiveStringData != "OK1") //------------- ----------------------------------等待最终协议
        //        {
        //            (RunBuf.GetStaRes(StaNumb)).sresult4 = false;
        //            (RunBuf.GetStaRes(StaNumb)).LMcount = 1;//-------   ---------------------------  ---------------等待最终协议
        //            RunBuf.Setsta(StaNumb, 17);
        //            Logger.Error($"ST{StaNumb}螺帽检测NG");
        //            //发送模号检测NG的结果给PLC
        //            Logger.Error($"ST{StaNumb}发送螺帽检测NG结果给PLC B1631");
        //        STEP5:
        //            if (ret != hardware.PLC_KEY.writebit("B1631", 1, "1"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(210);
        //                goto STEP5;
        //            }
        //            Logger.Error($"ST{StaNumb}发送螺帽检测NG结果给PLC W1731");
        //        STEP6:
        //            if (ret != hardware.PLC_KEY.writebit("W1731", 1, "00002"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(210);
        //                goto STEP6;
        //            }

        //            if (Alarm.ShowDialog(Color.Red, "报警", "螺帽检测结果NG，是重新测试一次，还是放弃？", "重新测", "放弃") == DialogResult.OK)
        //            {
        //                hardware.viscam.ReceiveStringData = "";
        //                goto STEP4;
        //            }
        //            else
        //            {
        //                //不重新测一个 就往下走了
        //            }
        //        }
        //        else//螺帽检测OK
        //        {
        //            (RunBuf.GetStaRes(StaNumb)).sresult4 = true;
        //            (RunBuf.GetStaRes(StaNumb)).LMcount = 0;//-----------------------------------  ------等待最终协议
        //            RunBuf.Setsta(StaNumb, 17);
        //            Logger.Info($"ST{StaNumb}螺帽检测OK");
        //            //发送模号检测NG的结果给PLC
        //            Logger.Info($"ST{StaNumb}发送螺帽检测OK结果给PLC B1631 ");
        //        STEP5:
        //            if (ret != hardware.PLC_KEY.writebit("B1631", 1, "1"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(210);
        //                goto STEP5;
        //            }
        //            Logger.Info($"ST{StaNumb}发送螺帽检测OK结果给PLC W1731 ");
        //        STEP6:
        //            if (ret != hardware.PLC_KEY.writebit("W1731", 1, "00001"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(210);
        //                goto STEP6;
        //            }
        //        }
        //        RunWaitCordTask();             
        //    }
        //    else if (RunBuf.GetStaValue(StaNumb) == 16)
        //    {
        //        //等待PLC的触发信号B1131
        //        Logger.Info($"ST{StaNumb}等待螺帽检测触发信号 PLC:B1131");
        //    STEP3:
        //        if (ret != hardware.PLC_KEY.readRbit("B1131", 1, out Y))
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(210);
        //            goto STEP3;
        //        }
        //        if (Y != "1")//模号检测触发信号
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(210);
        //            goto STEP3;
        //        }
        //        //请求耐压仪启动检测
        //        Logger.Info($"ST{StaNumb}请求视觉检测螺帽");
        //    STEP4:
        //        if (ret != hardware.viscam.Sendb("start2"))//启动视觉检测模号------------------------------------------等待最终协议
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(210);
        //            goto STEP4;//直到发送成功
        //        }
        //        RunBuf.Setsta(StaNumb, 16);
        //        Logger.Info($"ST{StaNumb}螺帽检测中");
        //        //等待模号检测结果
        //        while (hardware.viscam.ReceiveStringData.Length < 2)
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(510);
        //            Application.DoEvents();
        //        }
        //        if (hardware.viscam.ReceiveStringData != "OK1") //------------- ----------------------------------等待最终协议
        //        {
        //            (RunBuf.GetStaRes(StaNumb)).sresult4 = false;
        //            (RunBuf.GetStaRes(StaNumb)).LMcount = 1;//-------   ---------------------------  ---------------等待最终协议
        //            RunBuf.Setsta(StaNumb, 17);
        //            Logger.Error($"ST{StaNumb}螺帽检测NG");
        //            //发送模号检测NG的结果给PLC
        //            Logger.Error($"ST{StaNumb}发送螺帽检测NG结果给PLC B1631");
        //        STEP5:
        //            if (ret != hardware.PLC_KEY.writebit("B1631", 1, "1"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(210);
        //                goto STEP5;
        //            }
        //            Logger.Error($"ST{StaNumb}发送螺帽检测NG结果给PLC W1731");
        //        STEP6:
        //            if (ret != hardware.PLC_KEY.writebit("W1731", 1, "00002"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(210);
        //                goto STEP6;
        //            }

        //            if (Alarm.ShowDialog(Color.Red, "报警", "螺帽检测结果NG，是重新测试一次，还是放弃？", "重新测", "放弃") == DialogResult.OK)
        //            {
        //                hardware.viscam.ReceiveStringData = "";
        //                goto STEP4;
        //            }
        //            else
        //            {
        //                //不重新测一个 就往下走了
        //            }
        //        }
        //        else//螺帽检测OK
        //        {
        //            (RunBuf.GetStaRes(StaNumb)).sresult4 = true;
        //            (RunBuf.GetStaRes(StaNumb)).LMcount = 0;//-----------------------------------  ------等待最终协议
        //            RunBuf.Setsta(StaNumb, 17);
        //            Logger.Info($"ST{StaNumb}螺帽检测OK");
        //            //发送模号检测NG的结果给PLC
        //            Logger.Info($"ST{StaNumb}发送螺帽检测OK结果给PLC B1631");
        //        STEP5:
        //            if (ret != hardware.PLC_KEY.writebit("B1631", 1, "1"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(210);
        //                goto STEP5;
        //            }
        //            Logger.Info($"ST{StaNumb}发送螺帽检测OK结果给PLC W1731");
        //        STEP6:              
        //            if (ret != hardware.PLC_KEY.writebit("W1731", 1, "00001"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(210);
        //                goto STEP6;
        //            }
        //        }
        //       RunWaitCordTask();
                
        //    }
        //    else if (RunBuf.GetStaValue(StaNumb) == 17)
        //    {
        //        if ((RunBuf.GetStaRes(StaNumb)).sresult4 == false)
        //        {
        //            if (Alarm.ShowDialog(Color.Red, "报警", "螺帽检测结果NG，是重新测试一次，还是放弃？", "重新测", "放弃") == DialogResult.OK)
        //            {
        //                RunBuf.Setsta(StaNumb, 16);
        //                goto STEP0;
        //            }
        //        }
        //       RunWaitCordTask();
                 
        //    }
        //    else if (RunBuf.GetStaValue(StaNumb) == 18)
        //    {
        //        //读取转盘转到位信号
        //        Logger.Info($"ST{StaNumb}读取转盘转到位信号B1003");
        //    STEP2:
        //        if (ret != hardware.PLC_KEY.readRbit("B1003", 1, out Y))
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(210);
        //            goto STEP2;
        //        }
        //        if (Y != "1")
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(210);
                    
        //            goto STEP2;
        //        }
        //        //转盘转到位，到达扫码下料位置
        //        Logger.Info($"ST{StaNumb}旋转到扫码下料位");
        //        RunBuf.Setsta(StaNumb, 19);

        //        //等待PLC的触发信号B1010
        //        Logger.Info($"ST{StaNumb}等待扫码下料触发信号 PLC:B1010");
        //    STEP3:
        //        if (ret != hardware.PLC_KEY.readRbit("B1010", 1, out Y))
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(210);
        //            goto STEP3;
        //        }
        //        if (Y != "1")//扫码比对触发信号
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(210);
        //            goto STEP3;
        //        }
        //        //启动扫码器扫码
        //        Logger.Info($"ST{StaNumb}启动扫码器扫码");
        //        hardware.jienshi1.ReceiveStringData = "";
        //    STEP4:
        //        if (ret != hardware.jienshi1.Sendb("LON\u000d"))//启动扫码
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(210);
        //            goto STEP4;//直到发送成功
        //        }
        //        RunBuf.Setsta(StaNumb, 20);
        //        Logger.Info($"ST{StaNumb}开始扫码中");
        //        //等待扫码检测结果
        //        while (hardware.jienshi1.ReceiveStringData.Length < 2)
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(310);
        //            Application.DoEvents();
        //        }
        //        if (hardware.viscam.ReceiveStringData == "ERROR")
        //        {
        //            (RunBuf.GetStaRes(StaNumb)).sresult5 = false;
        //            (RunBuf.GetStaRes(StaNumb)).AutoCode = hardware.jienshi1.ReceiveStringData;
        //            hardware.jienshi1.ReceiveStringData = "";
        //            RunBuf.Setsta(StaNumb, 21);
        //            Logger.Error($"ST{StaNumb}扫码比对NG，没有扫码到");
        //            //发送模号检测NG的结果给PLC
        //            Logger.Error($"ST{StaNumb}发送扫码比对NG结果给PLC B1510");
        //        STEP5:
        //            if (ret != hardware.PLC_KEY.writebit("B1510", 1, "1"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(310);
        //                goto STEP5;
        //            }
        //            Logger.Error($"ST{StaNumb}发送扫码比对NG结果给PLC W1520");
        //        STEP6:
        //            if (ret != hardware.PLC_KEY.writebit("W1520", 1, "00002"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(310);
        //                goto STEP6;
        //            }

        //            if (Alarm.ShowDialog(Color.Red, "报警", "扫码器未扫到二维码，是重新测试一次，还是放弃？ ", "重新测", "放弃") == DialogResult.OK)
        //            {
        //                hardware.jienshi1.ReceiveStringData = "";

        //                goto STEP4;
        //            }
        //            else
        //            {
        //                //不重新测一个 就往下走了
        //            }
        //        }
        //        else//进行扫码比对，并存储信息
        //        {
        //            if (hardware.viscam.ReceiveStringData != (RunBuf.GetStaRes(StaNumb)).ManualCord)//比对失败
        //            {
        //                (RunBuf.GetStaRes(StaNumb)).sresult5 = false;
        //                (RunBuf.GetStaRes(StaNumb)).AutoCode = hardware.jienshi1.ReceiveStringData;// 
        //                hardware.jienshi1.ReceiveStringData = "";
        //                RunBuf.Setsta(StaNumb, 21);
        //                Logger.Error($"ST{StaNumb}扫码比对NG，上料扫码{(RunBuf.GetStaRes(StaNumb)).ManualCord}下料扫码{(RunBuf.GetStaRes(StaNumb)).AutoCode}");
        //                //发送模号检测NG的结果给PLC
        //                Logger.Error($"ST{StaNumb}发送扫码比对NG结果给PLC B1510");
        //            STEP7:
        //                if (ret != hardware.PLC_KEY.writebit("B1510", 1, "1"))
        //                {
        //                    if (Quit) return;
        //                    Thread.Sleep(310);
        //                    goto STEP7;
        //                }
        //                Logger.Error($"ST{StaNumb}发送扫码比对NG结果给PLC W1520");
        //            STEP8:
        //                if (ret != hardware.PLC_KEY.writebit("W1520", 1, "00002"))
        //                {
        //                    if (Quit) return;
        //                    Thread.Sleep(310);
        //                    goto STEP8;
        //                }
        //            }
        //            else//比对成功
        //            {
        //                (RunBuf.GetStaRes(StaNumb)).sresult5 = true;
        //                (RunBuf.GetStaRes(StaNumb)).AutoCode = hardware.viscam.ReceiveStringData;
        //                hardware.jienshi1.ReceiveStringData = "";
        //                RunBuf.Setsta(StaNumb, 13);
        //                Logger.Info($"ST{StaNumb}耐压检测OK");
        //                //发送模号检测OK的结果给PLC
        //                Logger.Info($"ST{StaNumb}发送模号检测OK结果给PLC B1510");
        //            STEP9:
        //                if (ret != hardware.PLC_KEY.writebit("B1510", 1, "1"))
        //                {
        //                    if (Quit) return;
        //                    Thread.Sleep(310);
        //                    goto STEP9;
        //                }
        //                Logger.Info($"ST{StaNumb}发送模号检测OK结果给PLC W1520");
        //            STEP10:
        //                if (ret != hardware.PLC_KEY.writebit("W1520", 1, "00001"))
        //                {
        //                    if (Quit) return;
        //                    Thread.Sleep(310);
        //                    goto STEP10;
        //                }
        //            }
        //        }
        //        //存数据   
        //        RunBuf.SaveDB(StaNumb);
        //        RunBuf.Setsta(StaNumb, 0);
        //        RunWaitCordTask();               
        //    }
        //    else if (RunBuf.GetStaValue(StaNumb) == 19)
        //    {
        //        //等待PLC的触发信号B1010
        //        Logger.Info($"ST{StaNumb}等待扫码下料触发信号 PLC:B1010");
        //    STEP3:
        //        if (ret != hardware.PLC_KEY.readRbit("B1010", 1, out Y))
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(310);
        //            goto STEP3;
        //        }
        //        if (Y != "1")//扫码比对触发信号
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(310);
        //            goto STEP3;
        //        }
        //        //启动扫码器扫码
        //        Logger.Info($"ST{StaNumb}启动扫码器扫码");
        //        hardware.jienshi1.ReceiveStringData = "";
        //    STEP4:
        //        if (ret != hardware.jienshi1.Sendb("LON\u000d"))//启动扫码
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(310);
        //            goto STEP4;//直到发送成功
        //        }
        //        RunBuf.Setsta(StaNumb, 20);
        //        Logger.Info($"ST{StaNumb}开始扫码中");
        //        //等待扫码检测结果
        //        while (hardware.jienshi1.ReceiveStringData.Length < 2)
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(310);
        //            Application.DoEvents();
        //        }
        //        if (hardware.viscam.ReceiveStringData == "ERROR")
        //        {
        //            (RunBuf.GetStaRes(StaNumb)).sresult5 = false;
        //            (RunBuf.GetStaRes(StaNumb)).AutoCode = hardware.jienshi1.ReceiveStringData;
        //            hardware.jienshi1.ReceiveStringData = "";
        //            RunBuf.Setsta(StaNumb, 21);
        //            Logger.Error($"ST{StaNumb}扫码比对NG，没有扫码到");
        //            //发送模号检测NG的结果给PLC
        //            Logger.Error($"ST{StaNumb}发送扫码比对NG结果给PLC B1510");
        //        STEP5:
        //            if (ret != hardware.PLC_KEY.writebit("B1510", 1, "1"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(310);
        //                goto STEP5;
        //            }
        //            Logger.Error($"ST{StaNumb}发送扫码比对NG结果给PLC W1520");
        //        STEP6:
        //            if (ret != hardware.PLC_KEY.writebit("W1520", 1, "00002"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(310);
        //                goto STEP6;
        //            }

        //            if (Alarm.ShowDialog(Color.Red, "报警", "扫码器未扫到二维码，是重新测试一次，还是放弃？ ", "重新测", "放弃") == DialogResult.OK)
        //            {
        //                hardware.jienshi1.ReceiveStringData = "";

        //                goto STEP4;
        //            }
        //            else
        //            {
        //                //不重新测一个 就往下走了
        //            }
        //        }
        //        else//进行扫码比对，并存储信息
        //        {
        //            if (hardware.viscam.ReceiveStringData != (RunBuf.GetStaRes(StaNumb)).ManualCord)//比对失败
        //            {
        //                (RunBuf.GetStaRes(StaNumb)).sresult5 = false;
        //                (RunBuf.GetStaRes(StaNumb)).AutoCode = hardware.jienshi1.ReceiveStringData;// 
        //                hardware.jienshi1.ReceiveStringData = "";
        //                RunBuf.Setsta(StaNumb, 21);
        //                Logger.Error($"ST{StaNumb}扫码比对NG，上料扫码{(RunBuf.GetStaRes(StaNumb)).ManualCord}下料扫码{(RunBuf.GetStaRes(StaNumb)).AutoCode}");
        //                //发送模号检测NG的结果给PLC
        //                Logger.Error($"ST{StaNumb}发送扫码比对NG结果给PLC B1510");
        //            STEP7:
        //                if (ret != hardware.PLC_KEY.writebit("B1510", 1, "1"))
        //                {
        //                    if (Quit) return;
        //                    Thread.Sleep(310);
        //                    goto STEP7;
        //                }
        //                Logger.Error($"ST{StaNumb}发送扫码比对NG结果给PLC W1520");
        //            STEP8:
        //                if (ret != hardware.PLC_KEY.writebit("W1520", 1, "00002"))
        //                {
        //                    if (Quit) return;
        //                    Thread.Sleep(310);
        //                    goto STEP8;
        //                }
        //            }
        //            else//比对成功
        //            {
        //                (RunBuf.GetStaRes(StaNumb)).sresult5 = true;
        //                (RunBuf.GetStaRes(StaNumb)).AutoCode = hardware.viscam.ReceiveStringData;
        //                hardware.jienshi1.ReceiveStringData = "";
        //                RunBuf.Setsta(StaNumb, 13);
        //                Logger.Info($"ST{StaNumb}耐压检测OK");
        //                //发送模号检测OK的结果给PLC
        //                Logger.Info($"ST{StaNumb}发送模号检测OK结果给PLC B1510");
        //            STEP9:
        //                if (ret != hardware.PLC_KEY.writebit("B1510", 1, "1"))
        //                {
        //                    if (Quit) return;
        //                    Thread.Sleep(310);
        //                    goto STEP9;
        //                }
        //                Logger.Info($"ST{StaNumb}发送模号检测OK结果给PLC W1520");
        //            STEP10:
        //                if (ret != hardware.PLC_KEY.writebit("W1520", 1, "00001"))
        //                {
        //                    if (Quit) return;
        //                    Thread.Sleep(310);
        //                    goto STEP10;
        //                }
        //            }
        //        }
        //        //存数据   
        //        RunBuf.SaveDB(StaNumb);
        //        RunBuf.Setsta(StaNumb, 0);
        //        RunWaitCordTask();
               
        //    }
        //    else if (RunBuf.GetStaValue(StaNumb) == 20)
        //    {
        //        //启动扫码器扫码
        //        Logger.Info($"ST{StaNumb}启动扫码器扫码");
        //        hardware.jienshi1.ReceiveStringData = "";
        //    STEP4:
        //        if (ret != hardware.jienshi1.Sendb("LON\u000d"))//启动扫码
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(310);
        //            goto STEP4;//直到发送成功
        //        }
        //        RunBuf.Setsta(StaNumb, 20);
        //        Logger.Info($"ST{StaNumb}开始扫码中");
        //        //等待扫码检测结果
        //        while (hardware.jienshi1.ReceiveStringData.Length < 2)
        //        {
        //            if (Quit) return;
        //            Thread.Sleep(310);
        //            Application.DoEvents();
        //        }
        //        if (hardware.viscam.ReceiveStringData == "ERROR")
        //        {
        //            (RunBuf.GetStaRes(StaNumb)).sresult5 = false;
        //            (RunBuf.GetStaRes(StaNumb)).AutoCode = hardware.jienshi1.ReceiveStringData;
        //            hardware.jienshi1.ReceiveStringData = "";
        //            RunBuf.Setsta(StaNumb, 21);
        //            Logger.Error($"ST{StaNumb}扫码比对NG，没有扫码到");
        //            //发送模号检测NG的结果给PLC
        //            Logger.Error($"ST{StaNumb}发送扫码比对NG结果给PLC B1510");
        //        STEP5:
        //            if (ret != hardware.PLC_KEY.writebit("B1510", 1, "1"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(310);
        //                goto STEP5;
        //            }
        //            Logger.Error($"ST{StaNumb}发送扫码比对NG结果给PLC W1520");
        //        STEP6:
        //            if (ret != hardware.PLC_KEY.writebit("W1520", 1, "00002"))
        //            {
        //                if (Quit) return;
        //                Thread.Sleep(310);
        //                goto STEP6;
        //            }

        //            if (Alarm.ShowDialog(Color.Red, "报警", "扫码器未扫到二维码，是重新测试一次，还是放弃？ ", "重新测", "放弃") == DialogResult.OK)
        //            {
        //                hardware.jienshi1.ReceiveStringData = "";

        //                goto STEP4;
        //            }
        //            else
        //            {
        //                //不重新测一个 就往下走了
        //            }
        //        }
        //        else//进行扫码比对，并存储信息
        //        {
        //            if (hardware.viscam.ReceiveStringData != (RunBuf.GetStaRes(StaNumb)).ManualCord)//比对失败
        //            {
        //                (RunBuf.GetStaRes(StaNumb)).sresult5 = false;
        //                (RunBuf.GetStaRes(StaNumb)).AutoCode = hardware.jienshi1.ReceiveStringData;// 
        //                hardware.jienshi1.ReceiveStringData = "";
        //                RunBuf.Setsta(StaNumb, 21);
        //                Logger.Error($"ST{StaNumb}扫码比对NG，上料扫码{(RunBuf.GetStaRes(StaNumb)).ManualCord}下料扫码{(RunBuf.GetStaRes(StaNumb)).AutoCode}");
        //                //发送模号检测NG的结果给PLC
        //                Logger.Error($"ST{StaNumb}发送扫码比对NG结果给PLC B1510");
        //            STEP7:
        //                if (ret != hardware.PLC_KEY.writebit("B1510", 1, "1"))
        //                {
        //                    if (Quit) return;
        //                    Thread.Sleep(310);
        //                    goto STEP7;
        //                }
        //                Logger.Error($"ST{StaNumb}发送扫码比对NG结果给PLC W1520");
        //            STEP8:
        //                if (ret != hardware.PLC_KEY.writebit("W1520", 1, "00002"))
        //                {
        //                    if (Quit) return;
        //                    Thread.Sleep(310);
        //                    goto STEP8;
        //                }
        //            }
        //            else//比对成功
        //            {
        //                (RunBuf.GetStaRes(StaNumb)).sresult5 = true;
        //                (RunBuf.GetStaRes(StaNumb)).AutoCode = hardware.viscam.ReceiveStringData;
        //                hardware.jienshi1.ReceiveStringData = "";
        //                RunBuf.Setsta(StaNumb, 13);
        //                Logger.Info($"ST{StaNumb}耐压检测OK");
        //                //发送模号检测OK的结果给PLC
        //                Logger.Info($"ST{StaNumb}发送模号检测OK结果给PLC B1510");
        //            STEP9:
        //                if (ret != hardware.PLC_KEY.writebit("B1510", 1, "1"))
        //                {
        //                    if (Quit) return;
        //                    Thread.Sleep(310);
        //                    goto STEP9;
        //                }
        //                Logger.Info($"ST{StaNumb}发送模号检测OK结果给PLC W1520");
        //            STEP10:
        //                if (ret != hardware.PLC_KEY.writebit("W1520", 1, "00001"))
        //                {
        //                    if (Quit) return;
        //                    Thread.Sleep(310);
        //                    goto STEP10;
        //                }
        //            }
        //        }
        //        //存数据   
        //        RunBuf.SaveDB(StaNumb);
        //        RunBuf.Setsta(StaNumb, 0);
        //        RunWaitCordTask();
        //    }
        //    else if (RunBuf.GetStaValue(StaNumb) == 21)
        //    {
        //        if ((RunBuf.GetStaRes(StaNumb)).sresult5 == false)
        //        {

        //            if (Alarm.ShowDialog(Color.Red, "报警", "扫码器扫码比对失败，是重新测试一次，还是放弃？ ", "重新测", "放弃") == DialogResult.OK)
        //            {
        //                hardware.jienshi1.ReceiveStringData = "";
        //                RunBuf.Setsta(StaNumb, 20);
        //                goto STEP0;
        //            }
        //            RunBuf.SaveDB(StaNumb);
        //            RunBuf.Setsta(StaNumb, 0);
        //            RunWaitCordTask();
                    
        //        }

        //    }
        //    else if (RunBuf.GetStaValue(StaNumb) > 21)
        //    {
        //        RunBuf.Setsta(StaNumb, 0);
        //      RunWaitCordTask();
        //    }
        }
    }
   
    /// <summary>
    /// 生产数量统计
    /// </summary>
    static public class ProductCount
    {
        
        static public int ZOKCount;//总OK数
        static public int ZNGCount;//总NG数
        static public Double Ztimes;//单产品做完耗时
        static public Double Dtimes;//生产节拍
        static public int DOKCount;//当班OK数
        static public int DNGCount;//当班NG数

         
    }
}
