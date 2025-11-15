using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLib.Files;
using MyLib.SqlHelper;
using MyLib.Sys;
namespace CXPro001.setups
{
    /// <summary>
    ///  高度检测-参数设置-显示类-2022.0.23.陈宏
    /// </summary>
    public class Highset
    {
        /// <summary>
        /// 二维码
        /// </summary>
       public string Cords;
        /// <summary>
        /// 实测值
        /// </summary>
        public double Real1, Real2, Real3, Real4, Real5, Real6, Real7, Real8, Real9, Real10, Real11;
        
        /// <summary>
        /// 理论值
        /// </summary>
        public double L1, L2, L3, L4, L5, L6, L7, L8, L9, L10, L11;
 
        /// <summary>
        /// 上限值
        /// </summary>
        public double UP1, UP2, UP3, UP4, UP5, UP6, UP7, UP8, UP9, UP10, UP11;
 
        /// <summary>
        /// 下限值1
        /// </summary>
        public double DW1, DW2, DW3, DW4, DW5, DW6, DW7, DW8, DW9, DW10, DW11;
        /// <summary>
        /// 各PIN结果
        /// </summary>
        public bool ResP1, ResP2, ResP3, ResP4, ResP5, ResP6, ResP7, ResP8, ResP9, ResP10, ResP11;
        /// <summary>
        /// 最终结果
        /// </summary>
        public bool ResEnd;
        public string Status;
        /// <summary>
        /// 分析数据 提取数据 计算结果
        /// </summary>
        /// <param name="rcv"></param>
        public void AnalyseData(string rcv,string cord)
        {
            Cords = cord;
               rcv = rcv.Replace("\r\n", "");
            string[] das = rcv.Split(',');
            if(das.Length<1)
            {
                init();
            }
            int i = 0;
            Real1 = Convert.ToDouble(das[i + 0]); Real2 = Convert.ToDouble(das[i + 1]); Real3 = Convert.ToDouble(das[i + 2]);
            Real4 = Convert.ToDouble(das[i + 3]); Real5 = Convert.ToDouble(das[i + 4]); Real6 = Convert.ToDouble(das[i + 5]);
            Real7 = Convert.ToDouble(das[i + 6]); Real8 = Convert.ToDouble(das[i + 7]); Real9 = Convert.ToDouble(das[i + 8]);

            ResP1 = Real1 > (L1 + DW1) && Real1 < (L1 + UP1); ResP2 = Real2 > (L2 + DW2) && Real2 < (L2 + UP2);
            ResP3 = Real3 > (L3 + DW1) && Real3 < (L3 + UP3); ResP4 = Real4 > (L4 + DW4) && Real4 < (L4 + UP4);
            ResP5 = Real5 > (L5 + DW5) && Real5 < (L5 + UP5); ResP6 = Real6 > (L6 + DW6) && Real6 < (L6 + UP6);
            ResP7 = Real7 > (L7 + DW7) && Real7 < (L7 + UP7); ResP8 = Real8 > (L8 + DW8) && Real8 < (L8 + UP8);
            ResP9 = Real9 > (L9 + DW9) && Real9 < (L9 + UP9);
            if (ResP1 & ResP2 & ResP3 & ResP4 & ResP5 & ResP6 & ResP7 & ResP8 & ResP9)
            {
                ResEnd = true;
            }
            else
            {
                ResEnd = false;
            }
        }
        /// <summary>
        /// j将实测数据和结果初始化
        /// </summary>
        public void init()
        {
            Real1 = Real2 = Real3 = Real4 = Real5 = Real6 = Real7 = Real8 = Real9 = Real10 = Real11 = 999.9;
            ResEnd = ResP1 = ResP2 = ResP3 = ResP4 = ResP5 = ResP6 = ResP7 = ResP8 = ResP9 = ResP10 = ResP11 = false;
            Status = "";
        }
        public void Sheild()//屏蔽
        {
            Real1 = Real2 = Real3 = Real4 = Real5 = Real6 = Real7 = Real8 = Real9 = Real10 = Real11 = 999.9;
            ResEnd = ResP1 = ResP2 = ResP3 = ResP4 = ResP5 = ResP6 = ResP7 = ResP8 = ResP9 = ResP10 = ResP11 = true;
            Status = "屏蔽中";
        }
        /// <summary>
        /// 保存到数据库
        /// </summary>
        /// <param name="cord"></param>
        public void SaveToDB(string cord)
        {
            string sql = "insert into Height_CCD( InsertTime,Cord,VehicleNumber,ModelNumber,ProductType,Result,Status,Current1H,Current2H,Current3H,Current4H," +
              "Current5H,Current6H,Current7H,Current8H,Current9H,Current10H,Current11H" +
              ") values( @InsertTime,@Cord,@VehicleNumber,@ModelNumber," +
              "@ProductType,@Result,@Status,@Current1H,@Current2H, @Current3H,@Current4H,@Current5H,@Current6H,@Current7H,@Current8H," +
              "@Current9H,@Current10H,@Current11H)";
             
            SqlParameter[] param = new SqlParameter[]
          {
                   new SqlParameter("@InsertTime", DateTime.Now),
                   new SqlParameter("@Cord",cord),
                   new SqlParameter("@VehicleNumber", ""),
                   new SqlParameter("@ModelNumber",""),
                   new SqlParameter("@ProductType", SysStatus.CurProductName),
                   new SqlParameter("@Result", ResEnd),
                   new SqlParameter("@Status", Status),
                   new SqlParameter("@Current1H", Real1),
                    new SqlParameter("@Current2H",  Real2),
                   new SqlParameter("@Current3H" ,Real3),
                   new SqlParameter("@Current4H",  Real4),
                   new SqlParameter("@Current5H", Real5),
                   new SqlParameter("@Current6H",  Real6),
                   new SqlParameter("@Current7H", Real7),
                   new SqlParameter("@Current8H",  Real8),
                   new SqlParameter("@Current9H", Real9),
                   new SqlParameter("@Current10H", Real10),
                   new SqlParameter("@Current11H", Real11),
                   
                    
          };
            int i = SQLHelper.Update(sql, param);
            if (i > 0)
            {
                Logger.Info("位置度数据写入位置度数据库成功");
            }
            else
            {
                Logger.Error("位置度数据写入位置度数据库失败");
            }


        }
        /// <summary>
        /// 从数据库加载
        /// </summary>
        /// <param name="cord"></param>
        public void LoadFromDB(string cord)
        {
            string sql = $" select * from Position_CCD where Cord = '{cord}'";
            SqlDataReader reader = SQLHelper.GetReader(sql);
            while (reader.Read())
            {
                ResEnd = (bool)(reader["Result"]);
                Status = (reader["Status"]).ToString();
                Real1 = (double)(reader["Current1H"]);
                Real2 = (double)(reader["Current2H"]);
                Real3 = (double)(reader["Current3H"]);
                Real4 = (double)(reader["Current4H"]);
                Real5 = (double)(reader["Current5H"]);
                Real6 = (double)(reader["Current6H"]);
                Real7 = (double)(reader["Current7H"]);
                Real8 = (double)(reader["Current8H"]);
                Real9 = (double)(reader["Current9H"]);

            }
            reader.Close();

        }

        #region 加载和保存配置
        /// <summary>
        /// 加载参数
        /// </summary>
        public void LoadParam(string typename="")
        {
            if (typename == "") typename = SysStatus.CurProductName.Trim();
            string path1 = $"{Path.GetFullPath("..")}\\product\\{typename}\\HighSet.ini";// 配置文件路径
            IniFile inf = new IniFile(path1);//确认路径是否存在，不存在则创建文件夹。                                             
            string STEP = "理论值";
            L1 = inf.ReadDouble(STEP, "L1", L1);
            L2 = inf.ReadDouble(STEP, "L2", L2);
            L3 = inf.ReadDouble(STEP, "L3", L3);
            L4 = inf.ReadDouble(STEP, "L4", L4);
            L5 = inf.ReadDouble(STEP, "L5", L5);
            L6 = inf.ReadDouble(STEP, "L6", L6);
            L7 = inf.ReadDouble(STEP, "L7", L7);
            L8 = inf.ReadDouble(STEP, "L8", L8);
            L9 = inf.ReadDouble(STEP, "L9", L9);
            L10 = inf.ReadDouble(STEP, "L1", L10);
            STEP = "上限值";
            UP1 = inf.ReadDouble(STEP, "UP1", UP1);
            UP2 = inf.ReadDouble(STEP, "UP2", UP2);
            UP3 = inf.ReadDouble(STEP, "UP3", UP3);
            UP4 = inf.ReadDouble(STEP, "UP4", UP4);
            UP5 = inf.ReadDouble(STEP, "UP5", UP5);
            UP6 = inf.ReadDouble(STEP, "UP6", UP6);
            UP7 = inf.ReadDouble(STEP, "UP7", UP7);
            UP8 = inf.ReadDouble(STEP, "UP8", UP8);
            UP9 = inf.ReadDouble(STEP, "UP9", UP9);
            UP10 = inf.ReadDouble(STEP, "UP1", UP10);
            STEP = "下限值";
            DW1 = inf.ReadDouble(STEP, "DW1", DW1);
            DW2 = inf.ReadDouble(STEP, "DW2", DW2);
            DW3 = inf.ReadDouble(STEP, "DW3", DW3);
            DW4 = inf.ReadDouble(STEP, "DW4", DW4);
            DW5 = inf.ReadDouble(STEP, "DW5", DW5);
            DW6 = inf.ReadDouble(STEP, "DW6", DW6);
            DW7 = inf.ReadDouble(STEP, "DW7", DW7);
            DW8 = inf.ReadDouble(STEP, "DW8", DW8);
            DW9 = inf.ReadDouble(STEP, "DW9", DW9);
            DW10 = inf.ReadDouble(STEP, "DW1", DW10);
        }
        /// <summary>
        /// 保存参数
        /// </summary>
        public void SaveParam(string typename = "")
        {
            if (typename == "") typename = SysStatus.CurProductName.Trim();
            string path1 = $"{Path.GetFullPath("..")}\\product\\{typename}\\HighSet.ini";// 配置文件路径
            IniFile inf = new IniFile(path1);//确认路径是否存在，不存在则创建文件夹。                                             
            string STEP = "理论值";
            inf.WriteDouble(STEP, "L1", L1);
            inf.WriteDouble(STEP, "L2", L2);
            inf.WriteDouble(STEP, "L3", L3);
            inf.WriteDouble(STEP, "L4", L4);
            inf.WriteDouble(STEP, "L5", L5);
            inf.WriteDouble(STEP, "L6", L6);
            inf.WriteDouble(STEP, "L7", L7);
            inf.WriteDouble(STEP, "L8", L8);
            inf.WriteDouble(STEP, "L9", L9);
            inf.WriteDouble(STEP, "L10", L10);
            STEP = "上限值";
            inf.WriteDouble(STEP, "UP1", UP1);
            inf.WriteDouble(STEP, "UP2", UP2);
            inf.WriteDouble(STEP, "UP3", UP3);
            inf.WriteDouble(STEP, "UP4", UP4);
            inf.WriteDouble(STEP, "UP5", UP5);
            inf.WriteDouble(STEP, "UP6", UP6);
            inf.WriteDouble(STEP, "UP7", UP7);
            inf.WriteDouble(STEP, "UP8", UP8);
            inf.WriteDouble(STEP, "UP9", UP9);
            inf.WriteDouble(STEP, "UP10", UP10);
            STEP = "下限值";
            inf.WriteDouble(STEP, "DW1", DW1);
            inf.WriteDouble(STEP, "DW2", DW2);
            inf.WriteDouble(STEP, "DW3", DW3);
            inf.WriteDouble(STEP, "DW4", DW4);
            inf.WriteDouble(STEP, "DW5", DW5);
            inf.WriteDouble(STEP, "DW6", DW6);
            inf.WriteDouble(STEP, "DW7", DW7);
            inf.WriteDouble(STEP, "DW8", DW8);
            inf.WriteDouble(STEP, "DW9", DW9);
            inf.WriteDouble(STEP, "DW10", DW10);

        }
        #endregion



    }
}
