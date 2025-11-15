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
    /// 位置度检测类  参数设置  显示--2022.9.23 陈宏
    /// </summary>
    public class PostionSet
    {
        public string Cords;
        public string Status;//屏蔽
        #region 参数
        /// <summary>
        ///X各个Pin结果 
        /// </summary>
        public bool ResPinX1, ResPinX2, ResPinX3, ResPinX4, ResPinX5, ResPinX6, ResPinX7, ResPinX8, ResPinX9;
        /// <summary>
        ///Y各个Pin结果 
        /// </summary>
        public bool ResPinY1, ResPinY2, ResPinY3, ResPinY4, ResPinY5, ResPinY6, ResPinY7, ResPinY8, ResPinY9;
        /// <summary>
        /// 最终结果
        /// </summary>
        public bool ResEnd;
        /// <summary>
        /// X实测值
        /// </summary>
        public double RealX1, RealX2, RealX3, RealX4, RealX5, RealX6, RealX7, RealX8, RealX9;
        /// <summary>
        /// Y实测值
        /// </summary>
        public double RealY1, RealY2, RealY3, RealY4, RealY5, RealY6, RealY7, RealY8, RealY9;
        /// <summary>
        /// X理论值
        /// </summary>
        public double TheoryX1, TheoryX2, TheoryX3, TheoryX4, TheoryX5, TheoryX6, TheoryX7, TheoryX8, TheoryX9;
        /// <summary>
        /// Y理论值
        /// </summary>
        public double TheoryY1, TheoryY2, TheoryY3, TheoryY4, TheoryY5, TheoryY6, TheoryY7, TheoryY8, TheoryY9;
        /// <summary>
        /// X各pin上限值
        /// </summary>
        public double UpX1, UpX2, UpX3, UpX4, UpX5, UpX6, UpX7, UpX8, UpX9;
        /// <summary>
        /// Y各PIN上限
        /// </summary>
        public double UpY1, UpY2, UpY3, UpY4, UpY5, UpY6, UpY7, UpY8, UpY9;
        /// <summary>
        /// X各PIN下限
        /// </summary>
        public double DownX1, DownX2, DownX3, DownX4, DownX5, DownX6, DownX7, DownX8, DownX9;
        /// <summary>
        ///  Y各PIN下限
        /// </summary>
        public double DownY1, DownY2, DownY3, DownY4, DownY5, DownY6, DownY7, DownY8, DownY9;

        /// <summary>
        /// X补偿值
        /// </summary>
        public double CompensateX1, CompensateX2, CompensateX3, CompensateX4, CompensateX5, CompensateX6, CompensateX7, CompensateX8, CompensateX9;
        /// <summary>
        /// Y补偿值
        /// </summary>
        public double CompensateY1, CompensateY2, CompensateY3, CompensateY4, CompensateY5, CompensateY6, CompensateY7, CompensateY8, CompensateY9;
        #endregion
        /// <summary>
        /// 实测值和各pin结果初始化
        /// </summary>
        public void init(string cord)
        {
            Cords = cord;
            Status = "";
            //实测数据
            RealX1 = RealY1 = RealX2 = RealY2 = RealX3 = RealY3 = RealX4 = RealY4 = RealX5 = RealY5 = 999.9;
            RealX6 = RealY6 = RealX7 = RealY7 = RealX8 = RealY8 = RealX9 = RealY9 = 999.9;
            ResPinX1 = ResPinX2 = ResPinX3 = ResPinX4 = ResPinX5 = ResPinX6 = ResPinX7 = ResPinX8 = ResPinX9 = ResEnd =
            ResPinY1 = ResPinY2 = ResPinY3 = ResPinY4 = ResPinY5 = ResPinY6 = ResPinY7 = ResPinY8 = ResPinY9 = false;
        }
        public void Sheild()
        {
            Status = "屏蔽中";
            //实测数据
            RealX1 = RealY1 = RealX2 = RealY2 = RealX3 = RealY3 = RealX4 = RealY4 = RealX5 = RealY5 = 999.9;
            RealX6 = RealY6 = RealX7 = RealY7 = RealX8 = RealY8 = RealX9 = RealY9 = 999.9;
            ResEnd= ResPinX1 = ResPinX2 = ResPinX3 = ResPinX4 = ResPinX5 = ResPinX6 = ResPinX7 = ResPinX8 = ResPinX9 =
            ResPinY1 = ResPinY2 = ResPinY3 = ResPinY4 = ResPinY5 = ResPinY6 = ResPinY7 = ResPinY8 = ResPinY9 = true;

        }
        /// <summary>
        /// 解析收到的位置度数据 
        /// </summary>
        /// <param name="rcv">收到的数据</param>
        /// <param name="data_ref">返回的实际要显示的值</param>
        public void AnalyseData(string rcv)
        {
            string[] dataArray = rcv.Split('\r', '\n');
            string[] datarr = dataArray[0].Split(',');

            if (datarr.Length == 30)//根据实际情况来
            {
                Cords = datarr[2];//二维码
                //实测数据
                RealX1 = Convert.ToDouble(datarr[6]) + CompensateX1;
                RealY1 = Convert.ToDouble(datarr[7]) + CompensateY1;
                RealX2 = Convert.ToDouble(datarr[8]) + CompensateX2;
                RealY2 = Convert.ToDouble(datarr[9]) + CompensateY2;
                RealX3 = Convert.ToDouble(datarr[10]) + CompensateX3;
                RealY3 = Convert.ToDouble(datarr[11]) + CompensateY3;
                RealX4 = Convert.ToDouble(datarr[12]) + CompensateX4;
                RealY4 = Convert.ToDouble(datarr[13]) + CompensateY4;
                RealX5 = Convert.ToDouble(datarr[14]) + CompensateX5;
                RealY5 = Convert.ToDouble(datarr[15]) + CompensateY5;
                RealX6 = Convert.ToDouble(datarr[16]) + CompensateX6;
                RealY6 = Convert.ToDouble(datarr[17]) + CompensateY6;
                RealX7 = Convert.ToDouble(datarr[18]) + CompensateX7;
                RealY7 = Convert.ToDouble(datarr[19]) + CompensateY7;
                RealX8 = Convert.ToDouble(datarr[20]) + CompensateX8;
                RealY8 = Convert.ToDouble(datarr[21]) + CompensateY8;
                RealX9 = Convert.ToDouble(datarr[22]) + CompensateX9;
                RealY9 = Convert.ToDouble(datarr[23]) + CompensateY9;
                //公差计算
                //model.P4X = Convert.ToDouble((Math.Sqrt(Math.Pow(Math.Abs(model.P1X - positionType.P1XN), 2.0) + Math.Pow(Math.Abs(model.P1Y - positionType.P1YN), 2.0)) * 1.8).ToString("0.00"));
                //    model.P4Y = Convert.ToDouble((Math.Sqrt(Math.Pow(Math.Abs(model.P2X - positionType.P2XN), 2.0) + Math.Pow(Math.Abs(model.P2Y - positionType.P2YN), 2.0)) * 1.8).ToString("0.00"));
                //    model.P5X = Convert.ToDouble((Math.Sqrt(Math.Pow(Math.Abs(model.P3X - positionType.P3XN), 2.0) + Math.Pow(Math.Abs(model.P3Y - positionType.P3YN), 2.0)) * 1.8).ToString("0.00"));
               
                ResPinX1 = (RealX1) < (TheoryX1 + UpX1) && RealX1 > (TheoryX1 + DownX1);
                ResPinY1 = (RealY1) < (TheoryY1 + UpY1) && RealY1 > (TheoryY1 + DownY1);
                ResPinX2 = (RealX2) < (TheoryX2 + UpX1) && RealX2 > (TheoryX2 + DownX2);
                ResPinY2 = (RealY2) < (TheoryY2 + UpY1) && RealY2 > (TheoryY2 + DownY2);
                ResPinX3 = (RealX3) < (TheoryX3 + UpX1) && RealX3 > (TheoryX3 + DownX3);
                ResPinY3 = (RealY3) < (TheoryY3 + UpY1) && RealY3 > (TheoryY3 + DownY3);
                ResPinX4 = (RealX4) < (TheoryX4 + UpX1) && RealX4 > (TheoryX4 + DownX4);
                ResPinY4 = (RealY4) < (TheoryY4 + UpY1) && RealY4 > (TheoryY4 + DownY4);
                ResPinX5 = (RealX5) < (TheoryX5 + UpX1) && RealX5 > (TheoryX5 + DownX5);
                ResPinY5 = (RealY5) < (TheoryY5 + UpY1) && RealY5 > (TheoryY5 + DownY5);
                ResPinX6 = (RealX6) < (TheoryX6 + UpX1) && RealX6 > (TheoryX6 + DownX6);
                ResPinY6 = (RealY6) < (TheoryY6 + UpY1) && RealY6 > (TheoryY6 + DownY6);
                ResPinX7 = (RealX7) < (TheoryX7 + UpX1) && RealX7 > (TheoryX7 + DownX7);
                ResPinY7 = (RealY7) < (TheoryY7 + UpY1) && RealY7 > (TheoryY7 + DownY7);
                ResPinX8 = (RealX8) < (TheoryX8 + UpX1) && RealX8 > (TheoryX8 + DownX8);
                ResPinY8 = (RealY8) < (TheoryY8 + UpY1) && RealY8 > (TheoryY8 + DownY8);
                ResPinX9 = (RealX9) < (TheoryX9 + UpX1) && RealX9 > (TheoryX9 + DownX9);
                ResPinY9 = (RealY9) < (TheoryY9 + UpY1) && RealY9 > (TheoryY9 + DownY9);
                if (ResPinX1 && ResPinY1 && ResPinX2 && ResPinY2 && ResPinX3 && ResPinY3 && ResPinX4 && ResPinY4
                    && ResPinX5 && ResPinY5 && ResPinX6 && ResPinY6 && ResPinX7 && ResPinY7 && ResPinX8 && ResPinY8 && ResPinX9 && ResPinY9)
                    ResEnd = true;
                else
                    ResEnd = false;

                if (ResEnd) Logger.Info($"位置度数据解析完成，结果OK:{ rcv}", 4);        
                else Logger.Error($"位置度数据解析完成，结果NG:{ rcv}", 4);                
            }
            else
            {
                ResEnd = false;
                Logger.Error($"位置度数据解析失败，数据的长度不对:{ rcv}", 4);
            }
        }
        
        /// <summary>
        /// 位置度数据存入数据库
        /// </summary>
        public void SavePosToDB(string Cord_Pos)
        {
            //
            string sql = "insert into Position_CCD( InsertTime,Cord,VehicleNumber,ModelNumber,ProductType,Result,Status,RealX1,RealY1,RealX2,RealY2," +
                "RealX3,RealY3,RealX4,RealY4,RealX5,RealY5,RealX6,RealY6,RealX7,RealY7,RealX8,RealY8,RealX9,RealY9)" +
                " values( @InsertTime,@Cord,@VehicleNumber,@ModelNumber," +
                "@ProductType,@Result,@Status,@RealX1,@RealY1,@RealX2,@RealY2," +
                "@RealX3,@RealY3,@RealX4,@RealY4,@RealX5,@RealY5,@RealX6,@RealY6,@RealX7,@RealY7,@RealX8,@RealY8,@RealX9,@RealY9 )";
            SqlParameter[] param = new SqlParameter[]
          {
                   new SqlParameter("@InsertTime", DateTime.Now),
                   new SqlParameter("@Cord",Cord_Pos),
                   new SqlParameter("@VehicleNumber", ""),
                   new SqlParameter("@ModelNumber", ""),
                   new SqlParameter("@ProductType", SysStatus.CurProductName),
                   new SqlParameter("@Result", ResEnd),
                     new SqlParameter("@Status", Status),
                   new SqlParameter("@RealX1",RealX1),
                   new SqlParameter("@RealY1", RealY1),
                   new SqlParameter("@RealX2",RealX2),
                   new SqlParameter("@RealY2", RealY2),
                   new SqlParameter("@RealX3",RealX3),
                   new SqlParameter("@RealY3", RealY3),
                   new SqlParameter("@RealX4",RealX4),
                   new SqlParameter("@RealY4", RealY4),
                   new SqlParameter("@RealX5",RealX5),
                   new SqlParameter("@RealY5", RealY5),
                   new SqlParameter("@RealX6",RealX6),
                   new SqlParameter("@RealY6", RealY6),
                    new SqlParameter("@RealX7",RealX7),
                   new SqlParameter("@RealY7", RealY7),
                    new SqlParameter("@RealX8",RealX8),
                   new SqlParameter("@RealY8", RealY8),
                    new SqlParameter("@RealX9",RealX9),
                   new SqlParameter("@RealY9", RealY9),
                    
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
        /// 从数据库读取数据
        /// </summary>
        /// <param name="CORD">二维码</param>
        public void LoadFromDB(string CORD)
        {
            string sql = $" select * from Position_CCD where Cord = '{ CORD}'";
            SqlDataReader reader = SQLHelper.GetReader(sql);
            while (reader.Read())
            {
                ResEnd = (bool)(reader["Result"]);
                Status= (reader["Status"]).ToString();
                RealX1 = (double)(reader["RealX1"]);
                RealX2 = (double)(reader["RealX2"]);
                RealX3 = (double)(reader["RealX3"]);
                RealX4 = (double)(reader["RealX4"]);
                RealX5 = (double)(reader["RealX5"]);
                RealX6 = (double)(reader["RealX6"]);
                RealX7 = (double)(reader["RealX7"]);
                RealX8 = (double)(reader["RealX8"]);
                RealX9 = (double)(reader["RealX9"]);
                RealY1 = (double)(reader["RealY1"]);
                RealY2 = (double)(reader["RealY2"]);
                RealY3 = (double)(reader["RealY3"]);
                RealY4 = (double)(reader["RealY4"]);
                RealY5 = (double)(reader["RealY5"]);
                RealY6 = (double)(reader["RealY6"]);
                RealY7 = (double)(reader["RealY7"]);
                RealY8 = (double)(reader["RealY8"]);
                RealY9 = (double)(reader["RealY9"]);
            }
            reader.Close();
        }
         
        #region 加载-保存位置度参数
        /// <summary>
        /// 导入参数-位置度参数
        /// </summary>
        public void LoadParm(string typename="")
        {
            if (typename == "") typename = SysStatus.CurProductName.Trim();
            string filename = $"{Path.GetFullPath("..")}\\product\\{typename}\\CCDPostion.ini";// 配置文件路径
            IniFile inf = new IniFile(filename);//确认路径是否存在，不存在则创建文件夹。
            //默认的是STEP1的参数
            string STEP = "理论值";
            TheoryX1 = inf.ReadDouble(STEP, "TheoryX1", TheoryX1);
            TheoryY1 = inf.ReadDouble(STEP, "TheoryY1", TheoryY1);
            TheoryX2 = inf.ReadDouble(STEP, "TheoryX2", TheoryX2);
            TheoryY2 = inf.ReadDouble(STEP, "TheoryY2", TheoryY2);
            TheoryX3 = inf.ReadDouble(STEP, "TheoryX3", TheoryX3);
            TheoryY3 = inf.ReadDouble(STEP, "TheoryY3", TheoryY3);
            TheoryX4 = inf.ReadDouble(STEP, "TheoryX4", TheoryX4);
            TheoryY4 = inf.ReadDouble(STEP, "TheoryY4", TheoryY4);
            TheoryX5 = inf.ReadDouble(STEP, "TheoryX5", TheoryX5);
            TheoryY5 = inf.ReadDouble(STEP, "TheoryY5", TheoryY5);
            TheoryX6 = inf.ReadDouble(STEP, "TheoryX6", TheoryX6);
            TheoryY6 = inf.ReadDouble(STEP, "TheoryY6", TheoryY6);
            TheoryX7 = inf.ReadDouble(STEP, "TheoryX7", TheoryX7);
            TheoryY7 = inf.ReadDouble(STEP, "TheoryY7", TheoryY7);
            TheoryX8 = inf.ReadDouble(STEP, "TheoryX8", TheoryX8);
            TheoryY8 = inf.ReadDouble(STEP, "TheoryY8", TheoryY8);
            TheoryX9 = inf.ReadDouble(STEP, "TheoryX9", TheoryX9);
            TheoryY9 = inf.ReadDouble(STEP, "TheoryY9", TheoryY9);
            STEP = "上限";
            UpX1 = inf.ReadDouble(STEP, "UpX1", UpX1);
            UpY1 = inf.ReadDouble(STEP, "UpY1", UpY1);
            UpX2 = inf.ReadDouble(STEP, "UpX2", UpX2);
            UpY2 = inf.ReadDouble(STEP, "UpY2", UpY2);
            UpX3 = inf.ReadDouble(STEP, "UpX3", UpX3);
            UpY3 = inf.ReadDouble(STEP, "UpY3", UpY3);
            UpX4 = inf.ReadDouble(STEP, "UpX4", UpX4);
            UpY4 = inf.ReadDouble(STEP, "UpY4", UpY4);
            UpX5 = inf.ReadDouble(STEP, "UpX5", UpX5);
            UpY5 = inf.ReadDouble(STEP, "UpY5", UpY5);
            UpX6 = inf.ReadDouble(STEP, "UpX6", UpX6);
            UpY6 = inf.ReadDouble(STEP, "UpY6", UpY6);
            UpX7 = inf.ReadDouble(STEP, "UpX7", UpX7);
            UpY7 = inf.ReadDouble(STEP, "UpY7", UpY7);
            UpX8 = inf.ReadDouble(STEP, "UpX8", UpX8);
            UpY8 = inf.ReadDouble(STEP, "UpY8", UpY8);
            UpX9 = inf.ReadDouble(STEP, "UpX9", UpX9);
            UpY9 = inf.ReadDouble(STEP, "UpY9", UpY9);

            STEP = "下限";
            DownX1 = inf.ReadDouble(STEP, "DownX1", DownX1);
            DownY1 = inf.ReadDouble(STEP, "DownY1", DownY1);
            DownX2 = inf.ReadDouble(STEP, "DownX2", DownX2);
            DownY2 = inf.ReadDouble(STEP, "DownY2", DownY2);
            DownX3 = inf.ReadDouble(STEP, "DownX3", DownX3);
            DownY3 = inf.ReadDouble(STEP, "DownY3", DownY3);
            DownX4 = inf.ReadDouble(STEP, "DownX4", DownX4);
            DownY4 = inf.ReadDouble(STEP, "DownY4", DownY4);
            DownX5 = inf.ReadDouble(STEP, "DownX5", DownX5);
            DownY5 = inf.ReadDouble(STEP, "DownY5", DownY5);
            DownX6 = inf.ReadDouble(STEP, "DownX6", DownX6);
            DownY6 = inf.ReadDouble(STEP, "DownY6", DownY6);
            DownX7 = inf.ReadDouble(STEP, "DownX7", DownX7);
            DownY7 = inf.ReadDouble(STEP, "DownY7", DownY7);
            DownX8 = inf.ReadDouble(STEP, "DownX8", DownX8);
            DownY8 = inf.ReadDouble(STEP, "DownY8", DownY8);
            DownX9 = inf.ReadDouble(STEP, "DownX9", DownX9);
            DownY9 = inf.ReadDouble(STEP, "DownY9", DownY9);
            STEP = "补偿值";
            CompensateX1 = inf.ReadDouble(STEP, "CompensateX1", CompensateX1);
            CompensateY1 = inf.ReadDouble(STEP, "CompensateY1", CompensateY1);
            CompensateX2 = inf.ReadDouble(STEP, "CompensateX2", CompensateX2);
            CompensateY2 = inf.ReadDouble(STEP, "CompensateY2", CompensateY2);
            CompensateX3 = inf.ReadDouble(STEP, "CompensateX3", CompensateX3);
            CompensateY3 = inf.ReadDouble(STEP, "CompensateY3", CompensateY3);
            CompensateX4 = inf.ReadDouble(STEP, "CompensateX4", CompensateX4);
            CompensateY4 = inf.ReadDouble(STEP, "CompensateY4", CompensateY4);
            CompensateX5 = inf.ReadDouble(STEP, "CompensateX5", CompensateX5);
            CompensateY5 = inf.ReadDouble(STEP, "CompensateY5", CompensateY5);
            CompensateX6 = inf.ReadDouble(STEP, "CompensateX6", CompensateX6);
            CompensateY6 = inf.ReadDouble(STEP, "CompensateY6", CompensateY6);
            CompensateX7 = inf.ReadDouble(STEP, "CompensateX7", CompensateX7);
            CompensateY7 = inf.ReadDouble(STEP, "CompensateY7", CompensateY7);
            CompensateX8 = inf.ReadDouble(STEP, "CompensateX8", CompensateX8);
            CompensateY8 = inf.ReadDouble(STEP, "CompensateY8", CompensateY8);
            CompensateX9 = inf.ReadDouble(STEP, "CompensateX9", CompensateX9);
            CompensateY9 = inf.ReadDouble(STEP, "CompensateY9", CompensateY9);

        }
        /// <summary>
        /// 保存配置-位置度参数
        /// </summary>
        public void SaveParm(string typename="")
        {
            if (typename == "") typename = SysStatus.CurProductName.Trim();
            string filename = $"{Path.GetFullPath("..")}\\product\\{typename}\\CCDPostion.ini";// 配置文件路径
        IniFile inf = new IniFile(filename);//确认路径是否存在，不存在则创建文件夹。
                                                //默认的是STEP1的参数
            string STEP = "理论值";
            inf.WriteDouble(STEP, "TheoryX1", TheoryX1);
            inf.WriteDouble(STEP, "TheoryY1", TheoryY1);
            inf.WriteDouble(STEP, "TheoryX2", TheoryX2);
            inf.WriteDouble(STEP, "TheoryY2", TheoryY2);
            inf.WriteDouble(STEP, "TheoryX3", TheoryX3);
            inf.WriteDouble(STEP, "TheoryY3", TheoryY3);
            inf.WriteDouble(STEP, "TheoryX4", TheoryX4);
            inf.WriteDouble(STEP, "TheoryY4", TheoryY4);
            inf.WriteDouble(STEP, "TheoryX5", TheoryX5);
            inf.WriteDouble(STEP, "TheoryY5", TheoryY5);
            inf.WriteDouble(STEP, "TheoryX6", TheoryX6);
            inf.WriteDouble(STEP, "TheoryY6", TheoryY6);
            inf.WriteDouble(STEP, "TheoryX7", TheoryX7);
            inf.WriteDouble(STEP, "TheoryY7", TheoryY7);
            inf.WriteDouble(STEP, "TheoryX8", TheoryX8);
            inf.WriteDouble(STEP, "TheoryY8", TheoryY8);
            inf.WriteDouble(STEP, "TheoryX9", TheoryX9);
            inf.WriteDouble(STEP, "TheoryY9", TheoryY9);

            STEP = "上限";
            inf.WriteDouble(STEP, "UpX1", UpX1);
            inf.WriteDouble(STEP, "UpY1", UpY1);
            inf.WriteDouble(STEP, "UpX2", UpX2);
            inf.WriteDouble(STEP, "UpY2", UpY2);
            inf.WriteDouble(STEP, "UpX3", UpX3);
            inf.WriteDouble(STEP, "UpY3", UpY3);
            inf.WriteDouble(STEP, "UpX4", UpX4);
            inf.WriteDouble(STEP, "UpY4", UpY4);
            inf.WriteDouble(STEP, "UpX5", UpX5);
            inf.WriteDouble(STEP, "UpY5", UpY5);
            inf.WriteDouble(STEP, "UpX6", UpX6);
            inf.WriteDouble(STEP, "UpY6", UpY6);
            inf.WriteDouble(STEP, "UpX7", UpX7);
            inf.WriteDouble(STEP, "UpY7", UpY7);
            inf.WriteDouble(STEP, "UpX8", UpX8);
            inf.WriteDouble(STEP, "UpY8", UpY8);
            inf.WriteDouble(STEP, "UpX9", UpX9);
            inf.WriteDouble(STEP, "UpY9", UpY9);

            STEP = "下限";
            inf.WriteDouble(STEP, "DownX1", DownX1);
            inf.WriteDouble(STEP, "DownY1", DownY1);
            inf.WriteDouble(STEP, "DownX2", DownX2);
            inf.WriteDouble(STEP, "DownY2", DownY2);
            inf.WriteDouble(STEP, "DownX3", DownX3);
            inf.WriteDouble(STEP, "DownY3", DownY3);
            inf.WriteDouble(STEP, "DownX4", DownX4);
            inf.WriteDouble(STEP, "DownY4", DownY4);
            inf.WriteDouble(STEP, "DownX5", DownX5);
            inf.WriteDouble(STEP, "DownY5", DownY5);
            inf.WriteDouble(STEP, "DownX6", DownX6);
            inf.WriteDouble(STEP, "DownY6", DownY6);
            inf.WriteDouble(STEP, "DownX7", DownX7);
            inf.WriteDouble(STEP, "DownY7", DownY7);
            inf.WriteDouble(STEP, "DownX8", DownX8);
            inf.WriteDouble(STEP, "DownY8", DownY8);
            inf.WriteDouble(STEP, "DownX9", DownX9);
            inf.WriteDouble(STEP, "DownY9", DownY9);

            STEP = "补偿值";
            inf.WriteDouble(STEP, "CompensateX1", CompensateX1);
            inf.WriteDouble(STEP, "CompensateY1", CompensateY1);
            inf.WriteDouble(STEP, "CompensateX2", CompensateX2);
            inf.WriteDouble(STEP, "CompensateY2", CompensateY2);
            inf.WriteDouble(STEP, "CompensateX3", CompensateX3);
            inf.WriteDouble(STEP, "CompensateY3", CompensateY3);
            inf.WriteDouble(STEP, "CompensateX4", CompensateX4);
            inf.WriteDouble(STEP, "CompensateY4", CompensateY4);
            inf.WriteDouble(STEP, "CompensateX5", CompensateX5);
            inf.WriteDouble(STEP, "CompensateY5", CompensateY5);
            inf.WriteDouble(STEP, "CompensateX6", CompensateX6);
            inf.WriteDouble(STEP, "CompensateY6", CompensateY6);
            inf.WriteDouble(STEP, "CompensateX7", CompensateX7);
            inf.WriteDouble(STEP, "CompensateY7", CompensateY7);
            inf.WriteDouble(STEP, "CompensateX8", CompensateX8);
            inf.WriteDouble(STEP, "CompensateY8", CompensateY8);
            inf.WriteDouble(STEP, "CompensateX9", CompensateX9);
            inf.WriteDouble(STEP, "CompensateY9", CompensateY9);


        }
        #endregion



















    }
}
