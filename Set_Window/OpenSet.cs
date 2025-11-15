
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLib.Files;
using MyLib.SqlHelper;
 
using CXPro001.myclass;
namespace CXPro001.setups
{
    /// <summary>
    /// 开口检测 20220923
    /// </summary>
    public class OpenSet
    {
        /// <summary>
        /// 二维码
        /// </summary>
        public string Cords;
        /// <summary>
        /// 极性 检测结果
        /// </summary>
        public bool PolarityRes;
        /// <summary>
        /// 异物检测结果
        /// </summary>
        public bool ForeignRes;
        /// <summary>
        /// 模号
        /// </summary>
        public string Model;
        /// <summary>
        /// 各脚实测值
        /// </summary>
        public double RealP1, RealP2, RealP3, RealP4, RealP5;
        /// <summary>
        /// 上限值
        /// </summary>
        public double UP_P1, UP_P2, UP_P3, UP_P4, UP_P5;
        /// <summary>
        /// 下限值
        /// </summary>
        public double DW_P1, DW_P2, DW_P3, DW_P4, DW_P5;
        /// <summary>
        /// 各脚结果
        /// </summary>
        public bool ResP1, ResP2, ResP3, ResP4, ResP5;
        /// <summary>
        /// 最终结果
        /// </summary>
        public bool ResEnd;
        /// <summary>
        /// 是否屏蔽
        /// </summary>
        public bool Sheild;
        public void init(bool pingbi = false)
        {

            ResEnd = Sheild = PolarityRes = ForeignRes = pingbi;
            RealP1 = RealP2 = RealP3 = RealP4 = RealP5 = 999.9;
            ResP1 = ResP2 = ResP3 = ResP4 = ResP5 = pingbi;
        }

        /// <summary>
        /// 解析收到的位置度数据 
        /// </summary>
        /// <param name="rcv">收到的数据</param>
        /// <param name="data_ref">返回的实际要显示的值</param>
        public void AnalyseData(string rcv, int step = 1)
        {
            string[] dataArray = rcv.Split('\r', '\n');
            string[] datarr = dataArray[0].Split(',');

            if (datarr.Length == 30)//根据实际情况来
            {
              
                ForeignRes = datarr[3] == "OK" ? true : false;
                //实测数据
             if(step == 1)   RealP1 = Convert.ToDouble(datarr[6]);
                if (step == 2)
                {
                    RealP2 = Convert.ToDouble(datarr[6]);
                    ResP1 = RealP1 < UP_P1 && RealP1 > DW_P1;
                    ResP2 = RealP2 < UP_P2 && RealP2 > DW_P2;
                    ResEnd = ResP1 & ResP2;
                    PolarityRes = ForeignRes = ResEnd;
                    if (ResEnd) Logger.Info($"开口度数据解析完成，结果OK:{ rcv}", 5);
                    else Logger.Error($"开口度数据解析完成，结果NG:{ rcv}", 5);
                }
                   
               // RealP3 = Convert.ToDouble(datarr[8]);
               // RealP4 = Convert.ToDouble(datarr[9]);
               // RealP5 = Convert.ToDouble(datarr[10]);
               
              //  ResP3 = RealP3 < UP_P3 && RealP3 > DW_P3;
                //ResP4 = RealP4 < UP_P4 && RealP4 > DW_P4;
               // ResP5 = RealP5 < UP_P5 && RealP5 > DW_P5;

            
             
            }
            else
            {
                ResP1 = ResP2 = ResP3 = ResP4 = ResP5 = false;
                Logger.Error($"开口度数据解析失败，数据的长度不对:{ rcv}", 5);
            }
        }

        /// <summary>
        ///  数据存入数据库
        /// </summary>
        public void SavePosToDB(string cord)
        {
            //检测该二维码之前做过没有
            string sql2 = $"SELECT * from  Open_CCD  WHERE Cord = '{cord}'";
            int i2 = int.Parse(SQLHelper.ExecuteScalar(sql2));
            if (i2 > 0)
            {
                sql2 = $" delete FROM Open_CCD WHERE Cord = '{cord}'";
                int i3 = SQLHelper.Update(sql2);
                if (i3 > 0)
                {
                    Logger.Info($"从开口度检测数据库删除{cord}的数据成功", 4);
                }
                else
                {
                    Logger.Error($"从开口度检测数据库删除{cord}的数据失败", 4);
                }
            }
            //存入数据库
            Cords = cord;
            string sql = "insert into Open_CCD( InsertTime,Cord,VehicleNumber,ModelNumber,ProductType,Result,Sheild,PolarityRes,ForeignRes," +
                "RealP1,RealP2,RealP3,RealP4," +
                "UP_P1,UP_P2,UP_P3,UP_P4," +
                "DW_P1,DW_P2,DW_P3,DW_P4)" +
                " values( @InsertTime,@Cord,@VehicleNumber,@ModelNumber,@ProductType,@Result,@Sheild,@PolarityRes,@ForeignRes," +
                "@RealP1,@RealP2,@RealP3,@RealP4," +
                "@UP_P1,@UP_P2,@UP_P3,@UP_P4,@" +
                "DW_P1,@DW_P2,@DW_P3,@DW_P4)";
             SqlParameter[] param = new SqlParameter[]
             {
                  new SqlParameter("@InsertTime", DateTime.Now),
                  new SqlParameter("@Cord",Cords),
                  new SqlParameter("@VehicleNumber", ""),
                  new SqlParameter("@ModelNumber", ""),
                  new SqlParameter("@ProductType", SysStatus.CurProductName),
                  new SqlParameter("@Result", ResEnd),
                  new SqlParameter("@Sheild", Sheild),
                  new SqlParameter("@PolarityRes",PolarityRes),
                  new SqlParameter("@ForeignRes",ForeignRes),
                  new SqlParameter("@RealP1",RealP1),
                  new SqlParameter("@RealP2",RealP2),
                  new SqlParameter("@RealP3",RealP3),
                  new SqlParameter("@RealP4",RealP4),
                  new SqlParameter("@UP_P1",UP_P1),
                  new SqlParameter("@UP_P2",UP_P2),
                  new SqlParameter("@UP_P3",UP_P3),
                  new SqlParameter("@UP_P4",UP_P4),
                  new SqlParameter("@DW_P1",DW_P1),
                  new SqlParameter("@DW_P2",DW_P2),
                  new SqlParameter("@DW_P3",DW_P3),
                  new SqlParameter("@DW_P4",DW_P4),                                   
             };
            int i = SQLHelper.Update(sql, param);
            if (i > 0)
            {
                Logger.Info("开口度数据写入数据库成功", 5);
            }
            else
            {
                Logger.Error("开口度数据写入数据库失败", 5);
            }
        }
        /// <summary>
        /// 从数据库读取数据
        /// </summary>
        /// <param name="CORD">二维码</param>
        public void LoadFromDB(string CORD)
        {
            init();
            try
            {
                string sql = $" select * from Open_CCD where Cord = '{ CORD}'";
                SqlDataReader reader = SQLHelper.GetReader(sql);
                while (reader.Read())
                {
                    PolarityRes = (bool)(reader["PolarityRes"]);
                    ForeignRes = (bool)(reader["ForeignRes"]);
                    ResEnd = (bool)(reader["Result"]);
                    Sheild = (bool)(reader["Sheild"]);
                    RealP1 = (double)(reader["RealP1"]);
                    RealP2 = (double)(reader["RealP2"]);
                    RealP3 = (double)(reader["RealP3"]);
                    RealP4 = (double)(reader["RealP4"]);
                    UP_P1 = (double)(reader["UP_P1"]);
                    UP_P2 = (double)(reader["UP_P2"]);
                    UP_P3 = (double)(reader["UP_P3"]);
                    UP_P4 = (double)(reader["UP_P4"]);
                    DW_P1 = (double)(reader["DW_P1"]);
                    DW_P2 = (double)(reader["DW_P2"]);
                    DW_P3 = (double)(reader["DW_P3"]);
                    DW_P4 = (double)(reader["DW_P4"]);

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Logger.Error($"读取开口度数据库出错{ex.Message}");
            }
        }

        #region 加载-保存位置度参数
        /// <summary>
        /// 导入参数-位置度参数
        /// </summary>
        public void LoadParm(string typename = "")
        {
            if (typename == "") typename = SysStatus.CurProductName.Trim();
            string filename = SysStatus.sys_dir_path+ "\\product\\"+ SysStatus.CurProductName + "\\OpenSet.ini";// 配置文件路径
            IniFile inf = new IniFile(filename);//确认路径是否存在，不存在则创建文件夹。
            //默认的是STEP1的参数
             
           string STEP = "上限";
            UP_P1 = inf.ReadDouble(STEP, "UP_P1", UP_P1);
            UP_P2 = inf.ReadDouble(STEP, "UP_P2", UP_P2);
            UP_P3 = inf.ReadDouble(STEP, "UP_P3", UP_P3);
            UP_P4 = inf.ReadDouble(STEP, "UP_P4", UP_P4);
            UP_P5 = inf.ReadDouble(STEP, "UP_P5", UP_P5);
            STEP = "下限";
            DW_P1 = inf.ReadDouble(STEP, "DW_P1", DW_P1);
            DW_P2 = inf.ReadDouble(STEP, "DW_P2", DW_P2);
            DW_P3 = inf.ReadDouble(STEP, "DW_P3", DW_P3);
            DW_P4 = inf.ReadDouble(STEP, "DW_P4", DW_P4);
            DW_P5 = inf.ReadDouble(STEP, "DW_P5", DW_P5);
            
        }
        /// <summary>
        /// 保存配置-位置度参数
        /// </summary>
        public void SaveParm(string typename = "")
        {
            if (typename == "") typename = SysStatus.CurProductName.Trim();
            string filename = SysStatus.sys_dir_path + "\\product\\" + SysStatus.CurProductName + "\\OpenSet.ini";// 配置文件路径

            IniFile inf = new IniFile(filename);//确认路径是否存在，不存在则创建文件夹。
            string STEP = "上限";
            inf.WriteDouble(STEP, "UP_P1", UP_P1);
            inf.WriteDouble(STEP, "UP_P2", UP_P2);
            inf.WriteDouble(STEP, "UP_P3", UP_P3);
            inf.WriteDouble(STEP, "UP_P4", UP_P4);
            inf.WriteDouble(STEP, "UP_P5", UP_P5);
            STEP = "下限";
            inf.WriteDouble(STEP, "DW_P1", DW_P1);
            inf.WriteDouble(STEP, "DW_P2", DW_P2);
            inf.WriteDouble(STEP, "DW_P3", DW_P3);
            inf.WriteDouble(STEP, "DW_P4", DW_P4);
            inf.WriteDouble(STEP, "DW_P5", DW_P5);

        }
        #endregion





    }
}
