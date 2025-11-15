using MyLib.Files;
using MyLib.SqlHelper;
using MyLib.Sys;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPro001.setups
{
    /// <summary>
    /// 平整度设置及参数类
    /// </summary>
    public class FlatnessSet
    {
        #region 平面度个点的值---PLC里面读取
        public float area_A_P1;
        public float area_A_P2;
        public float area_A_P3;
        public float area_B_P1;
        public float area_B_P2;
        public float area_B_P3;
        public float area_C_P1;
        public float area_C_P2;
        public float area_C_P3;
        public float area_D_P1;
        public float area_D_P2;
        public float area_D_P3;
        public float area_E_P1;
        public float area_E_P2;
        public float area_E_P3;
        public float area_F_P1;
        public float area_F_P2;
        public float area_F_P3;
        #endregion
        #region 测量值 = 平均值-基准值
        public float area_A_AVG { get { return ((area_A_P1 + area_A_P2 + area_A_P3) / 3)- area_A_Base; } }
        public float area_B_AVG { get { return ((area_B_P1 + area_B_P2 + area_B_P3) / 3)-area_B_Base; } }
        public float area_C_AVG { get { return ((area_C_P1 + area_C_P2 + area_C_P3) / 3)-area_C_Base; } }
        public float area_D_AVG { get { return ((area_D_P1 + area_D_P2 + area_D_P3) / 3)-area_D_Base; } }
        public float area_E_AVG { get { return ((area_E_P1 + area_E_P2 + area_E_P3) / 3)-area_E_Base; } }
        public float area_F_AVG { get { return ((area_F_P1 + area_F_P2 + area_F_P3) / 3)-area_F_Base; } }
        #endregion
        #region  基准值---设置的
        public float area_A_Base;
        public float area_B_Base;
        public float area_C_Base;
        public float area_D_Base;
        public float area_E_Base;
        public float area_F_Base;
        #endregion
        #region  标准值---设置的
        public float area_A_Mode;
        public float area_B_Mode;
        public float area_C_Mode;
        public float area_D_Mode;
        public float area_E_Mode;
        public float area_F_Mode;
        #endregion
        #region 误差上限--设置的
        public float ToleUp_A;
        public float ToleUp_B;
        public float ToleUp_C;
        public float ToleUp_D;
        public float ToleUp_E;
        public float ToleUp_F;
        #endregion
        #region  公差 = 测量值-基准值-标准值
        public float area_A_Tole { get { return Math.Abs(area_A_AVG - area_A_Base - area_A_Mode); } }
        public float area_B_Tole { get { return Math.Abs(area_B_AVG - area_B_Base - area_B_Mode); } }
        public float area_C_Tole { get { return Math.Abs(area_C_AVG - area_C_Base - area_C_Mode); } }
        public float area_D_Tole { get { return Math.Abs(area_D_AVG - area_D_Base - area_D_Mode); } }
        public float area_E_Tole { get { return Math.Abs(area_E_AVG - area_E_Base - area_E_Mode); } }
        public float area_F_Tole { get { return Math.Abs(area_F_AVG - area_F_Base - area_F_Mode); } }
        #endregion
        #region  显示结果
        public bool ResA { get { return area_A_Tole <= ToleUp_A ? true : false; } }
        public bool ResB { get { return area_B_Tole <= ToleUp_B ? true : false; } }
        public bool ResC { get { return area_C_Tole <= ToleUp_C ? true : false; } }
        public bool ResD { get { return area_D_Tole <= ToleUp_D ? true : false; } }
        public bool ResE { get { return area_E_Tole <= ToleUp_E ? true : false; } }
        public bool ResF { get { return area_F_Tole <= ToleUp_F ? true : false; } }
        public bool ResAll { get {
                return area_A_Tole <= ToleUp_A && area_B_Tole <= ToleUp_B && area_C_Tole <= ToleUp_C
 && area_D_Tole <= ToleUp_D && area_E_Tole <= ToleUp_E ? true : false; } }
        #endregion

        public void init()
        {
            area_A_P1 = area_A_P2 = area_A_P3 = area_B_P1 = area_B_P2 = area_B_P3 = 0.00F;
            area_C_P1 = area_C_P2 = area_C_P3 = area_D_P1 = area_D_P2 = area_D_P3 = 0.00F;
            area_E_P1 = area_E_P2 = area_E_P3 = area_F_P1 = area_F_P2 = area_F_P3 = 0.00F;
        }
        /// <summary>
        /// 数据存入数据库
        /// </summary>
        public void SaveToDB(string cord)
        {         
            string sql = "insert into FlatnessDB( InsertTime,Cord,Result,ProductType,area_A_P1,area_A_P2,area_A_P3,area_B_P1,area_B_P2,area_B_P3," +
                "area_C_P1,area_C_P2,area_C_P3,area_D_P1,area_D_P2,area_D_P3,area_E_P1,area_E_P2,area_E_P3," +
                "area_A_AVG,area_B_AVG,area_C_AVG,area_D_AVG,area_E_AVG," +
                "area_A_Tole,area_B_Tole,area_C_Tole,area_D_Tole,area_E_Tole," +
                "area_A_Base,area_B_Base,area_C_Base,area_D_Base,area_E_Base," +
                "area_A_Mode,area_B_Mode,area_C_Mode,area_D_Mode,area_E_Mode," +
                "ToleUp_A,ToleUp_B,ToleUp_C,ToleUp_D,ToleUp_E)" +
                " values( @InsertTime,@Cord,@Result,@ProductType,@area_A_P1,@area_A_P2,@area_A_P3,@area_B_P1,@area_B_P2,@area_B_P3,@" +
                "area_C_P1,@area_C_P2,@area_C_P3,@area_D_P1,@area_D_P2,@area_D_P3,@area_E_P1,@area_E_P2,@area_E_P3,@" +
                "area_A_AVG,@area_B_AVG,@area_C_AVG,@area_D_AVG,@area_E_AVG,@" +
                "area_A_Tole,@area_B_Tole,@area_C_Tole,@area_D_Tole,@area_E_Tole,@" +
                "area_A_Base,@area_B_Base,@area_C_Base,@area_D_Base,@area_E_Base,@" +
                "area_A_Mode,@area_B_Mode,@area_C_Mode,@area_D_Mode,@area_E_Mode,@" +
                "ToleUp_A,@ToleUp_B,@ToleUp_C,@ToleUp_D,@ToleUp_E)";
            string RES1 = ResAll == true ? "OK" : "NG";
            SqlParameter[] param = new SqlParameter[]
            {
                   new SqlParameter("@InsertTime", DateTime.Now),
                   new SqlParameter("@Cord", cord),
                   new SqlParameter("@Result", RES1),
                   new SqlParameter("@ProductType", SysStatus.CurProductName),
                   new SqlParameter("@area_A_P1",  area_A_P1 ), new SqlParameter("@area_A_P2",  area_A_P2 ), new SqlParameter("@area_A_P3",  area_A_P3 ),
                   new SqlParameter("@area_B_P1",  area_B_P1 ), new SqlParameter("@area_B_P2",  area_B_P2 ), new SqlParameter("@area_B_P3",  area_B_P3 ),
                   new SqlParameter("@area_C_P1",  area_C_P1 ), new SqlParameter("@area_C_P2",  area_C_P2 ), new SqlParameter("@area_C_P3",  area_C_P3 ),
                   new SqlParameter("@area_D_P1",  area_D_P1 ), new SqlParameter("@area_D_P2",  area_D_P2 ), new SqlParameter("@area_D_P3",  area_D_P3 ),
                   new SqlParameter("@area_E_P1",  area_E_P1 ), new SqlParameter("@area_E_P2",  area_E_P2 ), new SqlParameter("@area_E_P3",  area_E_P3 ),
                   new SqlParameter("@area_A_AVG",  area_A_AVG ), new SqlParameter("@area_B_AVG",  area_B_AVG ), new SqlParameter("@area_C_AVG",  area_C_AVG ),
                   new SqlParameter("@area_D_AVG",  area_D_AVG ), new SqlParameter("@area_E_AVG",  area_E_AVG ),
                   new SqlParameter("@area_A_Tole",  area_A_Tole ),new SqlParameter("@area_B_Tole",  area_B_Tole ),new SqlParameter("@area_C_Tole",  area_C_Tole ),
                   new SqlParameter("@area_D_Tole",  area_D_Tole ), new SqlParameter("@area_E_Tole",  area_E_Tole ),
                   new SqlParameter("@area_A_Base",  area_A_Base ), new SqlParameter("@area_B_Base",  area_B_Base ), new SqlParameter("@area_C_Base",  area_C_Base ),
                   new SqlParameter("@area_D_Base",  area_D_Base ), new SqlParameter("@area_E_Base",  area_E_Base ),
                   new SqlParameter("@area_A_Mode",  area_A_Mode ), new SqlParameter("@area_B_Mode",  area_B_Mode ), new SqlParameter("@area_C_Mode",  area_C_Mode ),
                   new SqlParameter("@area_D_Mode",  area_D_Mode ), new SqlParameter("@area_E_Mode",  area_E_Mode ),
                   new SqlParameter("@ToleUp_A",  ToleUp_A), new SqlParameter("@ToleUp_B",  ToleUp_B), new SqlParameter("@ToleUp_C",  ToleUp_C),
                   new SqlParameter("@ToleUp_D",  ToleUp_D), new SqlParameter("@ToleUp_E",  ToleUp_E),
            };
            int i = SQLHelper.Update(sql, param);
            if (i > 0)
            {
                Logger.Info("耐压数据写入耐压数据库成功");
            }
            else
            {
                Logger.Error("耐压数据写入耐压数据库失败");
            }
        }

        public void LoadFromDB(string CORD)
        {
            string sql = $" select * from FlatnessDB  where Cord = '{ CORD}'";
            SqlDataReader reader = SQLHelper.GetReader(sql);
            while (reader.Read())
            {            
                area_A_P1 = (float)(reader["area_A_P1"]);
                area_A_P2 = (float)(reader["area_A_P2"]);
                area_A_P3 = (float)(reader["area_A_P3"]);
                area_B_P1 = (float)(reader["area_B_P1"]);
                area_B_P2 = (float)(reader["area_B_P2"]);
                area_B_P3 = (float)(reader["area_B_P3"]);
                area_C_P1 = (float)(reader["area_C_P1"]);
                area_C_P2 = (float)(reader["area_C_P2"]);
                area_C_P3 = (float)(reader["area_C_P3"]);
                area_D_P1 = (float)(reader["area_D_P1"]);
                area_D_P2 = (float)(reader["area_D_P2"]);
                area_D_P3 = (float)(reader["area_D_P3"]);
                area_E_P1 = (float)(reader["area_E_P1"]);
                area_E_P2 = (float)(reader["area_E_P2"]);
                area_E_P3 = (float)(reader["area_E_P3"]);
            }
            reader.Close();

        }


        /// <summary>
        /// 加载参数
        /// </summary>
        public void LoadParam(string typename)
        {
            if (typename == "") typename = SysStatus.CurProductName.Trim();
             string path1 = $"{Path.GetFullPath("..")}\\product\\{typename}\\FlatnessSet.ini";// 配置文件路径
            IniFile inf = new IniFile(path1);//确认路径是否存在，不存在则创建文件夹。                                             
            string STEP = "基准值";
            area_A_Base = (float)inf.ReadDouble(STEP, "area_A_Base", area_A_Base);
            area_B_Base = (float)inf.ReadDouble(STEP, "area_B_Base", area_B_Base);
            area_C_Base = (float)inf.ReadDouble(STEP, "area_C_Base", area_C_Base);
            area_D_Base = (float)inf.ReadDouble(STEP, "area_D_Base", area_D_Base);
            area_E_Base = (float)inf.ReadDouble(STEP, "area_E_Base", area_E_Base);
            area_F_Base = (float)inf.ReadDouble(STEP, "area_F_Base", area_F_Base);
            STEP = "标准值";
            area_A_Mode = (float)inf.ReadDouble(STEP, "area_A_Mode", area_A_Mode);
            area_B_Mode = (float)inf.ReadDouble(STEP, "area_B_Mode", area_B_Mode);
            area_C_Mode = (float)inf.ReadDouble(STEP, "area_C_Mode", area_C_Mode);
            area_D_Mode = (float)inf.ReadDouble(STEP, "area_D_Mode", area_D_Mode);
            area_E_Mode = (float)inf.ReadDouble(STEP, "area_E_Mode", area_E_Mode);
            area_F_Mode = (float)inf.ReadDouble(STEP, "area_F_Mode", area_F_Mode);
            STEP = "公差上限";
            ToleUp_A= (float)inf.ReadDouble(STEP, "ToleUp_A", ToleUp_A);
            ToleUp_B = (float)inf.ReadDouble(STEP, "ToleUp_B", ToleUp_B);
            ToleUp_C = (float)inf.ReadDouble(STEP, "ToleUp_C", ToleUp_C);
            ToleUp_D = (float)inf.ReadDouble(STEP, "ToleUp_D", ToleUp_D);
            ToleUp_E = (float)inf.ReadDouble(STEP, "ToleUp_E", ToleUp_E);
            ToleUp_F = (float)inf.ReadDouble(STEP, "ToleUp_F", ToleUp_F);
        }
        /// <summary>
        /// 保存参数
        /// </summary>
        public void SaveParam(string typename)
        {
            if (typename == "") typename = SysStatus.CurProductName.Trim();
            string path1 = $"{Path.GetFullPath("..")}\\product\\{typename}\\FlatnessSet.ini";// 配置文件路径
            IniFile inf = new IniFile(path1);//确认路径是否存在，不存在则创建文件夹。                                             
            string STEP = "基准值";
            inf.WriteDouble(STEP, "area_A_Base", (double)area_A_Base);
            inf.WriteDouble(STEP, "area_B_Base", (double)area_B_Base);
            inf.WriteDouble(STEP, "area_C_Base", (double)area_C_Base);
            inf.WriteDouble(STEP, "area_D_Base", (double)area_D_Base);
            inf.WriteDouble(STEP, "area_E_Base", (double)area_E_Base);
            inf.WriteDouble(STEP, "area_F_Base", (double)area_F_Base);
            STEP = "标准值";
            inf.WriteDouble(STEP, "area_A_Mode", (double)area_A_Mode);
            inf.WriteDouble(STEP, "area_B_Mode", (double)area_B_Mode);
            inf.WriteDouble(STEP, "area_C_Mode", (double)area_C_Mode);
            inf.WriteDouble(STEP, "area_D_Mode", (double)area_D_Mode);
            inf.WriteDouble(STEP, "area_E_Mode", (double)area_E_Mode);
            inf.WriteDouble(STEP, "area_F_Mode", (double)area_F_Mode);
            STEP = "公差上限";
            inf.WriteDouble(STEP, " ToleUp_A", (double)ToleUp_A);
            inf.WriteDouble(STEP, " ToleUp_B", (double)ToleUp_B);
            inf.WriteDouble(STEP, " ToleUp_C", (double)ToleUp_C);
            inf.WriteDouble(STEP, " ToleUp_D", (double)ToleUp_D);
            inf.WriteDouble(STEP, " ToleUp_E", (double)ToleUp_E);
            inf.WriteDouble(STEP, " ToleUp_F", (double)ToleUp_F);
        }















    }
}
