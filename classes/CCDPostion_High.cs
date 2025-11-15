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
namespace CXPro001.classes
{
    /// <summary>
    /// 高度 位置度（最多13组）  配置参数-显示类 多功能 陈宏2022.10.21
    /// </summary>
    public class CCDPostion_High
    {
        #region 公共参数
        /// <summary>
        /// 绑定的二维码或序号
        /// </summary>
        public string Cords = "";
        /// <summary>
        /// 产品型号
        /// </summary>
        public string ProductType { get { return SysStatus.CurProductName; } }
        /// <summary>
        /// 模号
        /// </summary>
        public string ModelNumber = "0";
        /// <summary>
        /// 载具号
        /// </summary>
        public string VehicleNumber = "0";
        /// <summary>
        /// 总结果
        /// </summary>
        public bool ResAll;
        public bool ResPLC;
        /// <summary>
        /// 屏蔽
        /// </summary>
        public bool Sheild = false;
        #endregion

        #region 位置度

        #region 参数
        /// <summary>
        /// 位置度各个Pin位置度结果 X Y结果
        /// </summary>
        public bool ResPin1, ResPin2, ResPin3, ResPin4, ResPin5, ResPin6, ResPin7, ResPin8, ResPin9, ResPin10, ResPin11, ResPin12, ResPin13,
             ResPinX1, ResPinX2, ResPinX3, ResPinX4, ResPinX5, ResPinX6, ResPinX7, ResPinX8, ResPinX9, ResPinX10, ResPinX11, ResPinX12, ResPinX13,
             ResPinY1, ResPinY2, ResPinY3, ResPinY4, ResPinY5, ResPinY6, ResPinY7, ResPinY8, ResPinY9, ResPinY10, ResPinY11, ResPinY12, ResPinY13;
        /// <summary>
        /// 位置度实测值1X 1Y---13X 13Y
        /// </summary>
        public double Current1X, Current1Y, Current2X, Current2Y, Current3X, Current3Y, Current4X, Current4Y, Current5X, Current5Y, Current6X, Current6Y,
            Current7X, Current7Y, Current8X, Current8Y, Current9X, Current9Y, Current10X, Current10Y, Current11X, Current11Y, Current12X, Current12Y, Current13X, Current13Y;
        /// <summary>
        /// 位置度理论值1X 1Y---13X 13Y
        /// </summary>
        public double Theory1X, Theory1Y, Theory2X, Theory2Y, Theory3X, Theory3Y, Theory4X, Theory4Y, Theory5X, Theory5Y, Theory6X, Theory6Y, Theory7X, Theory7Y,
            Theory8X, Theory8Y, Theory9X, Theory9Y, Theory10X, Theory10Y, Theory11X, Theory11Y, Theory12X, Theory12Y, Theory13X, Theory13Y;
        /// <summary>
        /// 位置度各Pin上限1X 1Y---13X 13Y
        /// </summary>
        public double Up1X, Up1Y, Up2X, Up2Y, Up3X, Up3Y, Up4X, Up4Y, Up5X, Up5Y, Up6X, Up6Y, Up7X, Up7Y, Up8X, Up8Y, Up9X, Up9Y, Up10X, Up10Y, Up11X, Up11Y, Up12X, Up12Y,
            Up13X, Up13Y, UpP1, UpP2, UpP3, UpP4, UpP5, UpP6, UpP7, UpP8, UpP9, UpP10, UpP11, UpP12, UpP13;
        /// <summary>
        /// 位置度各Pin下限1X 1Y---13X 13Y
        /// </summary>
        public double Down1X, Down1Y, Down2X, Down2Y, Down3X, Down3Y, Down4X, Down4Y, Down5X, Down5Y, Down6X, Down6Y, Down7X, Down7Y, Down8X, Down8Y,
            Down9X, Down9Y, Down10X, Down10Y, Down11X, Down11Y, Down12X, Down12Y, Down13X, Down13Y,
        DownP1, DownP2, DownP3, DownP4, DownP5, DownP6, DownP7, DownP8, DownP9, DownP10, DownP11, DownP12, DownP13;
        /// <summary>
        /// 位置度各Pin 补偿值
        /// </summary>
        public double CompensateX1, CompensateY1, CompensateX2, CompensateY2, CompensateX3, CompensateY3, CompensateX4, CompensateY4, CompensateX5, CompensateY5,
            CompensateX6, CompensateY6, CompensateX7, CompensateY7, CompensateX8, CompensateY8, CompensateX9, CompensateY9, CompensateX10, CompensateY10,
            CompensateX11, CompensateY11, CompensateX12, CompensateY12, CompensateX13, CompensateY13;
        /// <summary>
        ///  各Pin 位置度  计算得出
        /// </summary>
        public double PosP1, PosP2, PosP3, PosP4, PosP5, PosP6, PosP7, PosP8, PosP9, PosP10, PosP11, PosP12, PosP13;
        #endregion
        public void init_Postion(bool pingbi = false)
        {
            Sheild = ResAll = ResPin1 = ResPin2 = ResPin3 = ResPin4 = ResPin5 = ResPin6 = ResPin7 = ResPin8 = ResPin9 = ResPin10 = ResPin11 = ResPin12 = ResPin13 =
               ResPinX1 = ResPinX2 = ResPinX3 = ResPinX4 = ResPinX5 = ResPinX6 = ResPinX7 = ResPinX8 = ResPinX9 = ResPinX10 = ResPinX11 = ResPinX12 = ResPinX13 =
               ResPinY1 = ResPinY2 = ResPinY3 = ResPinY4 = ResPinY5 = ResPinY6 = ResPinY7 = ResPinY8 = ResPinY9 = ResPinY10 = ResPinY11 = ResPinY12 = ResPinY13 = pingbi;
            Current1X = Current1Y = Current2X = Current2Y = Current3X = Current3Y =
            Current4X = Current4Y = Current5X = Current5Y = Current6X = Current6Y =
            Current7X = Current7Y = Current8X = Current8Y = Current9X = Current9Y =
            Current10X = Current10Y = Current11X = Current11Y = Current12X = Current12Y =
            Current13X = Current13Y = 999.9;
        }
        /// <summary>
        /// 解析收到的位置度数据—第一个位置拍的
        /// </summary>
        /// <param name="rcv">收到的数据</param>
        /// <param name="data_ref">返回的实际要显示的值</param>
        public void AnalyseData_Pos(string rcv, string modes,string mohao)
        {
            ResPLC = modes == "ON";            
            string[] datarr = rcv.Split(',');
            try
            {
                ModelNumber = mohao;
                if (datarr.Length >= 18)//根据实际情况来
                {
                    //Theory1X = StoD(datarr[0]);
                    //Theory2X = StoD(datarr[1]);
                    //Theory3X = StoD(datarr[2]);
                    //Theory4X = StoD(datarr[3]);
                    //Theory5X = StoD(datarr[4]);
                    //Theory6X = StoD(datarr[5]);
                    //Theory1Y = StoD(datarr[6]);
                    //Theory2Y = StoD(datarr[7]);
                    //Theory3Y = StoD(datarr[8]);
                    //Theory4Y = StoD(datarr[9]);
                    //Theory5Y = StoD(datarr[10]);
                    //Theory6Y = StoD(datarr[11]);
                    Current1X = StoD(datarr[0]);
                    Current2X = StoD(datarr[1]);
                    Current3X = StoD(datarr[2]);
                    //Current4X = StoD(datarr[15]);
                    //Current5X = StoD(datarr[16]);
                    //Current6X = StoD(datarr[17]);
                    Current1Y = StoD(datarr[6]);
                    Current2Y = StoD(datarr[7]);
                    Current3Y = StoD(datarr[8]);
                    //Current4Y = StoD(datarr[21]);
                    //Current5Y = StoD(datarr[22]);
                    //Current6Y = StoD(datarr[23]);
                    PosP1 = StoD(datarr[12]);
                    PosP2 = StoD(datarr[13]);
                    PosP3 = StoD(datarr[14]);
                    //PosP4 = StoD(datarr[27]);
                    //PosP5 = StoD(datarr[28]);
                    //PosP6 = StoD(datarr[29]);
                    ////公差计算
                    //PosP1 = Math.Round(Math.Sqrt(Math.Pow(Math.Abs(Current1X - Theory1X), 2.0) + Math.Pow(Math.Abs(Current1Y - Theory1Y), 2.0)) * 1.8, 3);
                    //PosP2 = Math.Round(Math.Sqrt(Math.Pow(Math.Abs(Current2X - Theory2X), 2.0) + Math.Pow(Math.Abs(Current2Y - Theory2Y), 2.0)) * 1.8, 3);
                    //PosP3 = Math.Round(Math.Sqrt(Math.Pow(Math.Abs(Current3X - Theory3X), 2.0) + Math.Pow(Math.Abs(Current3Y - Theory3Y), 2.0)) * 1.8, 3);
                    ////Up1X = StoD(datarr[30]); Up1Y = StoD(datarr[36]);
                    //Up2X = StoD(datarr[31]); Up2Y = StoD(datarr[37]);
                    //Up3X = StoD(datarr[32]); Up3Y = StoD(datarr[38]);
                    //Up4X = StoD(datarr[33]); Up4Y = StoD(datarr[39]);
                    //Up5X = StoD(datarr[34]); Up5Y = StoD(datarr[40]);
                    //Up6X = StoD(datarr[35]); Up6Y = StoD(datarr[41]);
                    //UpP1= StoD(datarr[42]);
                    //UpP2 = StoD(datarr[43]);
                    //UpP3 = StoD(datarr[44]);
                    //UpP4 = StoD(datarr[45]);
                    //UpP5 = StoD(datarr[46]);
                    //UpP6 = StoD(datarr[47]);
                    //Down1X = StoD(datarr[48]); Down1Y = StoD(datarr[54]);
                    //Down2X = StoD(datarr[49]); Down2Y = StoD(datarr[55]);
                    //Down3X = StoD(datarr[50]); Down3Y = StoD(datarr[56]);
                    //Down4X = StoD(datarr[51]); Down4Y = StoD(datarr[57]);
                    //Down5X = StoD(datarr[52]); Down5Y = StoD(datarr[58]);
                    //Down6X = StoD(datarr[53]); Down6Y = StoD(datarr[59]);
                    //DownP1 = StoD(datarr[60]);
                    //DownP2 = StoD(datarr[61]);
                    //DownP3 = StoD(datarr[62]);
                    //DownP4 = StoD(datarr[63]);
                    //DownP5 = StoD(datarr[64]);
                    //DownP6 = StoD(datarr[65]);

                    //公差计算
                    //PosP1 = Math.Round(Math.Sqrt(Math.Pow(Math.Abs(Current1X - Theory1X), 2.0) + Math.Pow(Math.Abs(Current1Y - Theory1Y), 2.0)) * 1.8, 3);
                    //PosP2 = Math.Round(Math.Sqrt(Math.Pow(Math.Abs(Current2X - Theory2X), 2.0) + Math.Pow(Math.Abs(Current2Y - Theory2Y), 2.0)) * 1.8, 3);
                    //PosP3 = Math.Round(Math.Sqrt(Math.Pow(Math.Abs(Current3X - Theory3X), 2.0) + Math.Pow(Math.Abs(Current3Y - Theory3Y), 2.0)) * 1.8, 3);
                    //PosP4 = Math.Round(Math.Sqrt(Math.Pow(Math.Abs(Current4X - Theory4X), 2.0) + Math.Pow(Math.Abs(Current4Y - Theory4Y), 2.0)) * 1.8, 3);
                    //PosP5 = Math.Round(Math.Sqrt(Math.Pow(Math.Abs(Current5X - Theory5X), 2.0) + Math.Pow(Math.Abs(Current5Y - Theory5Y), 2.0)) * 1.8, 3);
                    //PosP6 = Math.Round(Math.Sqrt(Math.Pow(Math.Abs(Current6X - Theory6X), 2.0) + Math.Pow(Math.Abs(Current6Y - Theory6Y), 2.0)) * 1.8, 3);
                    //PosP7 = Math.Round(Math.Sqrt(Math.Pow(Math.Abs(Current7X - Theory7X), 2.0) + Math.Pow(Math.Abs(Current7Y - Theory7Y), 2.0)) * 1.8, 3);
                    //PosP8 = Math.Round(Math.Sqrt(Math.Pow(Math.Abs(Current8X - Theory8X), 2.0) + Math.Pow(Math.Abs(Current8Y - Theory8Y), 2.0)) * 1.8, 3);
                    //PosP9 = Math.Round(Math.Sqrt(Math.Pow(Math.Abs(Current9X - Theory9X), 2.0) + Math.Pow(Math.Abs(Current9Y - Theory9Y), 2.0)) * 1.8, 3);
                    //PosP10 = Math.Round(Math.Sqrt(Math.Pow(Math.Abs(Current10X - Theory10X), 2.0) + Math.Pow(Math.Abs(Current10Y - Theory10Y), 2.0)) * 1.8, 3);
                    //PosP11 = Math.Round(Math.Sqrt(Math.Pow(Math.Abs(Current11X - Theory11X), 2.0) + Math.Pow(Math.Abs(Current11Y - Theory11Y), 2.0)) * 1.8, 3);
                    //PosP12 = Math.Round(Math.Sqrt(Math.Pow(Math.Abs(Current12X - Theory12X), 2.0) + Math.Pow(Math.Abs(Current12Y - Theory12Y), 2.0)) * 1.8, 3);
                    //PosP13 = Math.Round(Math.Sqrt(Math.Pow(Math.Abs(Current13X - Theory13X), 2.0) + Math.Pow(Math.Abs(Current13Y - Theory13Y), 2.0)) * 1.8, 3);
                    //判断结果
                    //ResPinX1 = Current1X < Theory1X + Up1X && Current1X > Theory1X + Down1X;
                    //ResPinY1 = Current1Y < Theory1Y + Up1Y && Current1Y > Theory1Y + Down1Y;
                    //ResPinX2 = Current2X < Theory2X + Up2X && Current2X > Theory2X + Down2X;
                    //ResPinY2 = Current2Y < Theory2Y + Up2Y && Current2Y > Theory2Y + Down2Y;
                    //ResPinX3 = Current3X < Theory3X + Up3X && Current3X > Theory3X + Down3X;
                    //ResPinY3 = Current3Y < Theory3Y + Up3Y && Current3Y > Theory3Y + Down3Y;
                    //ResPinX4 = Current4X < Theory4X + Up4X && Current4X > Theory4X + Down4X;
                    //ResPinY4 = Current4Y < Theory4Y + Up4Y && Current4Y > Theory4Y + Down4Y;
                    //ResPinX5 = Current5X < Theory5X + Up5X && Current5X > Theory5X + Down5X;
                    //ResPinY5 = Current5Y < Theory5Y + Up5Y && Current5Y > Theory5Y + Down5Y;
                    //ResPinX6 = Current6X < Theory6X + Up6X && Current6X > Theory6X + Down6X;
                    //ResPinY6 = Current6Y < Theory6Y + Up6Y && Current6Y > Theory6Y + Down6Y;
                    //ResPinX7 = Current7X < Theory7X + Up7X && Current7X > Theory7X + Down7X;
                    //ResPinY7 = Current7Y < Theory7Y + Up7Y && Current7Y > Theory7Y + Down7Y;
                    //ResPinX8 = Current8X < Theory8X + Up8X && Current8X > Theory8X + Down8X;
                    //ResPinY8 = Current8Y < Theory8Y + Up8Y && Current8Y > Theory8Y + Down8Y;
                    //ResPinX9 = Current9X < Theory9X + Up9X && Current9X > Theory9X + Down9X;
                    //ResPinY9 = Current9Y < Theory9Y + Up9Y && Current9Y > Theory9Y + Down9Y;
                    //ResPinX10 = Current10X < Theory10X + Up10X && Current10X > Theory10X + Down10X;
                    //ResPinY10 = Current10Y < Theory10Y + Up10Y && Current10Y > Theory10Y + Down10Y;
                    //ResPinX11 = Current11X < Theory11X + Up11X && Current11X > Theory11X + Down11X;
                    //ResPinY11 = Current11Y < Theory11Y + Up11Y && Current11Y > Theory11Y + Down11Y;
                    //ResPinX12 = Current12X < Theory12X + Up12X && Current12X > Theory12X + Down12X;
                    //ResPinY12 = Current12Y < Theory12Y + Up12Y && Current12Y > Theory12Y + Down12Y;
                    //ResPinX13 = Current13X < Theory13X + Up13X && Current13X > Theory13X + Down13X;
                    //ResPinY13 = Current13Y < Theory13Y + Up13Y && Current13Y > Theory13Y + Down13Y;
                    ResPin1 = PosP1 <= Up4X;
                    ResPin2 = PosP2 <= Up4Y;
                    ResPin3 = PosP3 <= Up5X; 
                    //ResPin4 = PosP4 <= UpP4;
                    //ResPin5 = PosP5 < UpP5; ResPin6 = PosP6 < UpP6; ResPin7 = PosP7 < UpP7; ResPin8 = PosP8 < UpP8;
                    //ResPin9 = PosP9 < UpP9; ResPin10 = PosP10 < UpP10; ResPin11 = PosP11 < UpP11; ResPin12 = PosP12 < UpP12; ResPin13 = PosP13 < UpP13;
                    if (/*ResPinX1 & ResPinY1 & ResPinX2 & ResPinY2 & ResPinX3 & ResPinY3 &*/ ResPin1 & ResPin2 & ResPin3)
                    {
                        ResAll = true;
                        Logger.Info($"位置度数据解析完成，结果OK:{ rcv}", 4);
                    }                   
                    else
                    {
                        ResAll = false;
                        Logger.Error($"位置度数据解析完成，结果NG:{ rcv}");
                    }
                    //if (ResAll != ResPLC)
                    //{
                    //    ResAll = false; 
                    //    Logger.Error($"位置度数据解析完成，结果与PLC不一致");
                    //}
                }
                else
                {
                    ResAll = false;
                    Logger.Error($"位置度数据解析失败，数据的长度不对:{ rcv}", 4);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"位置度数据解析失败{ex.Message}，数据有错误:{rcv}", 4);
                 
            }
           
        }      
        /// <summary>
        /// 位置度数据存入数据库
        /// </summary>
        public void SaveToDB_Pos(string cord)
        {
            //检测该二维码之前做过没有
            string sql2 = $"SELECT * from  Position_CCD  WHERE Cord = '{cord}'";
            int i2 = int.Parse(SQLHelper.ExecuteScalar(sql2));
            if (i2 > 0)
            {
                sql2 = $" delete FROM Position_CCD WHERE Cord = '{cord}'";
                int i3 = SQLHelper.Update(sql2);
                if (i3 > 0)
                {
                    Logger.Info($"从位置度检测数据库删除{cord}的数据成功", 4);
                }
                else
                {
                    Logger.Error($"从位置度检测数据库删除{cord}的数据失败", 4);
                }
            }
            //存入数据库
            Cords = cord;
            string sql = "insert into Position_CCD( InsertTime,Cord,VehicleNumber,ModelNumber,ProductType,Result,ResultPLC,Sheild," +
                "Current1X,Current1Y,Current2X,Current2Y,Current3X,Current3Y,Current4X,Current4Y," +
                "PosP1,PosP2,PosP3,PosP4," +
                "Theory1X,Theory1Y,Theory2X,Theory2Y,Theory3X,Theory3Y,Theory4X,Theory4Y," +
                "Up1X,Up1Y,Up2X,Up2Y,Up3X,Up3Y,Up4X,Up4Y,UpP1,UpP2,UpP3,UpP4," +
                "Down1X,Down1Y,Down2X,Down2Y,Down3X,Down3Y,Down4X,Down4Y)" +
                " values( @InsertTime,@Cord,@VehicleNumber,@ModelNumber,@ProductType,@Result,@ResultPLC,@Sheild," +
                "@Current1X,@Current1Y, @Current2X,@Current2Y,@Current3X,@Current3Y,@Current4X,@Current4Y," +
                 "@PosP1,@PosP2,@PosP3,@PosP4,@" +
                 "Theory1X,@Theory1Y,@Theory2X,@Theory2Y,@Theory3X,@Theory3Y,@Theory4X,@Theory4Y,@" +
                 "Up1X,@Up1Y,@Up2X,@Up2Y,@Up3X,@Up3Y,@Up4X,@Up4Y,@UpP1,@UpP2,@UpP3,@UpP4,@" +
                 "Down1X,@Down1Y,@Down2X,@Down2Y,@Down3X,@Down3Y,@Down4X,@Down4Y)";
            SqlParameter[] param = new SqlParameter[]
            {
                   new SqlParameter("@InsertTime", DateTime.Now),
                   new SqlParameter("@Cord",Cords),
                   new SqlParameter("@VehicleNumber", VehicleNumber),
                   new SqlParameter("@ModelNumber", ModelNumber),
                   new SqlParameter("@ProductType", ProductType),
                   new SqlParameter("@Result", ResAll),
                   new SqlParameter("@ResultPLC", ResPLC),
                    new SqlParameter("@Sheild",  Sheild),
                   new SqlParameter("@Current1X",Current1X), new SqlParameter("@Current1Y", Current1Y),
                   new SqlParameter("@Current2X",Current2X), new SqlParameter("@Current2Y", Current2Y),
                   new SqlParameter("@Current3X",Current3X), new SqlParameter("@Current3Y", Current3Y),
                   new SqlParameter("@Current4X",Current4X), new SqlParameter("@Current4Y", Current4Y),
                   new SqlParameter("@PosP1",PosP1),
                   new SqlParameter("@PosP2",PosP2),
                   new SqlParameter("@PosP3",PosP3),
                   new SqlParameter("@PosP4",PosP4),
                   new SqlParameter("@Theory1X", Theory1X), new SqlParameter("@Theory1Y", Theory1Y),
                   new SqlParameter("@Theory2X", Theory2X), new SqlParameter("@Theory2Y", Theory2Y),
                   new SqlParameter("@Theory3X", Theory3X), new SqlParameter("@Theory3Y", Theory3Y),
                   new SqlParameter("@Theory4X", Theory4X), new SqlParameter("@Theory4Y", Theory4Y),
                   new SqlParameter("@Up1X", Up1X), new SqlParameter("@Up1Y", Up1Y),
                   new SqlParameter("@Up2X", Up2X), new SqlParameter("@Up2Y", Up2Y),
                   new SqlParameter("@Up3X", Up3X), new SqlParameter("@Up3Y", Up3Y),
                   new SqlParameter("@Up4X", Up4X), new SqlParameter("@Up4Y", Up4Y),
                   new SqlParameter("@UpP1", UpP1), new SqlParameter("@UpP2", UpP2),
                   new SqlParameter("@UpP3", UpP3), new SqlParameter("@UpP4", UpP4),
                   new SqlParameter("@Down1X", Down1X), new SqlParameter("@Down1Y", Down1Y),
                   new SqlParameter("@Down2X", Down2X), new SqlParameter("@Down2Y", Down2Y),
                   new SqlParameter("@Down3X", Down3X), new SqlParameter("@Down3Y", Down3Y),
                   new SqlParameter("@Down4X", Down4X), new SqlParameter("@Down4Y", Down4Y),

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
        /// 从数据库读取位置度数据
        /// </summary>
        /// <param name="CORD">二维码</param>
        public void LoadFromDB_Pos(string CORD)
        {
            init_Postion();
            try
            {
                string sql = $" select * from Position_CCD where Cord = '{CORD}'";
                SqlDataReader reader = SQLHelper.GetReader(sql);
                while (reader.Read())
                {
                    ResAll = (bool)(reader["Result"]);
                    ResPLC = (bool)(reader["ResultPLC"]);
                    Sheild = (bool)(reader["Sheild"]);
                    Current1X = (double)(reader["Current1X"]); Current1Y = (double)(reader["Current1Y"]);
                    Current2X = (double)(reader["Current2X"]); Current2Y = (double)(reader["Current2Y"]);
                    Current3X = (double)(reader["Current3X"]); Current3Y = (double)(reader["Current3Y"]);
                    Current4X = (double)(reader["Current4X"]); Current4Y = (double)(reader["Current4Y"]);
                    PosP1 = (double)(reader["PosP1"]); PosP2 = (double)(reader["PosP2"]);
                    PosP3 = (double)(reader["PosP3"]); PosP4 = (double)(reader["PosP4"]);
                    Theory1X = (double)(reader["Theory1X"]); Theory1Y = (double)(reader["Theory1Y"]);
                    Theory2X = (double)(reader["Theory2X"]); Theory2Y = (double)(reader["Theory2Y"]);
                    Theory3X = (double)(reader["Theory3X"]); Theory3Y = (double)(reader["Theory3Y"]);
                    Theory4X = (double)(reader["Theory4X"]); Theory1Y = (double)(reader["Theory4Y"]);
                    Up1X = (double)(reader["Up1X"]); Up1Y = (double)(reader["Up1Y"]);
                    Up2X = (double)(reader["Up2X"]); Up2Y = (double)(reader["Up2Y"]);
                    Up3X = (double)(reader["Up3X"]); Up3Y = (double)(reader["Up3Y"]);
                    Up4X = (double)(reader["Up4X"]); Up1Y = (double)(reader["Up4Y"]);
                    UpP1 = (double)(reader["UpP1"]); UpP2 = (double)(reader["UpP2"]);
                    UpP3 = (double)(reader["UpP3"]); UpP4 = (double)(reader["UpP4"]);

                    Down1X = (double)(reader["Down1X"]); Down1Y = (double)(reader["Down1Y"]);
                    Down2X = (double)(reader["Down2X"]); Down2Y = (double)(reader["Down2Y"]);
                    Down3X = (double)(reader["Down3X"]); Down3Y = (double)(reader["Down3Y"]);
                    Down4X = (double)(reader["Down4X"]); Down1Y = (double)(reader["Down4Y"]);
 

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Logger.Error($"读取位置度数据库出错{ex.Message}");
            }
        }
        #endregion

        #region 高度
        #region 属性
        /// <summary>
        /// 基准值:插口PIN基准   PCB pin基准
        /// </summary>
        public double BaseHigh, BaseHigh1;        
        /// <summary>
        /// 各个PiN高度实测值
        /// </summary>
        public double Current1H, Current2H, Current3H, Current4H, Current5H, Current6H, Current7H, Current8H, Current9H, Current10H, Current11H, Current12H, Current13H;
        /// <summary>
        /// 各个PiN高度理论值
        /// </summary>
        public double Theory1H, Theory2H, Theory3H, Theory4H, Theory5H, Theory6H, Theory7H, Theory8H, Theory9H, Theory10H, Theory11H, Theory12H, Theory13H;
        /// <summary>
        /// 各个PiN高度上限值
        /// </summary>
        public double Up1H, Up2H, Up3H, Up4H, Up5H, Up6H, Up7H, Up8H, Up9H, Up10H, Up11H, Up12H, Up13H;
        /// <summary>
        /// 各个PiN高度下限值
        /// </summary>
        public double Down1H, Down2H, Down3H, Down4H, Down5H, Down6H, Down7H, Down8H, Down9H, Down10H, Down11H, Down12H, Down13H;
        /// <summary>
        /// 各个PiN高度补偿值
        /// </summary>
        public double Compensate1H, Compensate2H, Compensate3H, Compensate4H, Compensate5H, Compensate6H, Compensate7H, Compensate8H, Compensate9H, Compensate10H,
            Compensate11H, Compensate12H, Compensate13H;
        /// <summary>
        ///  各个PiN高度结果
        /// </summary>
        public bool ResH1, ResH2, ResH3, ResH4, ResH5, ResH6, ResH7, ResH8, ResH9, ResH10, ResH11, ResH12, ResH13;
        #endregion
        public void init_High(bool pingbi=false)
        {
            Sheild = ResAll = pingbi;
            Current1H = Current2H = Current3H = Current4H = Current5H =
            Current6H = Current7H = Current8H = Current9H = Current10H =
            Current11H = Current12H = Current13H = 999.9;
            ResH1 = ResH2 = ResH3 = ResH4 = ResH5 = ResH6 = ResH7 = ResH8 = ResH9 = ResH10 = ResH11 = ResH12 = ResH13 = pingbi;
        }
        /// <summary>
        /// 解析数据得出结果  高度
        /// </summary>
        /// <param name="rcv">带解析数据</param>
        /// <param name="data_ref">返回的实测数据</param>
        /// <param name="pincount"></param>
        public void AnalyseData_High(string rcv, int pincount,string modes,string mode1)
        {
            try
            {
                ModelNumber = mode1;
                ResPLC = modes == "ON";
                string[] datastr = rcv.Split(',');
                if (datastr.Length == 24)
                {
                    Theory1H =
                    Theory2H =
                    Theory3H =
                    Theory4H =
                    Theory5H =
                    Theory6H = 7.2;
                    Current1H = StoD(datastr[6]);
                    Current2H = StoD(datastr[7]);
                    Current3H = StoD(datastr[8]);
                    Current4H = StoD(datastr[9]);
                    Current5H = StoD(datastr[10]);
                    Current6H = StoD(datastr[11]);
                    Up1H = StoD(datastr[12]);
                    Up2H = StoD(datastr[13]);
                    Up3H = StoD(datastr[14]);
                    Up4H = StoD(datastr[15]);
                    Up5H = StoD(datastr[16]);
                    Up6H = StoD(datastr[17]);
                    Down1H = StoD(datastr[18]);
                    Down2H = StoD(datastr[19]);
                    Down3H = StoD(datastr[20]);
                    Down4H = StoD(datastr[21]);
                    Down5H = StoD(datastr[22]);
                    Down6H = StoD(datastr[23]);
                    ResH1 = Current1H < ( Up1H) && Current1H > ( Down1H);
                    ResH2 = Current2H < ( Up2H) && Current2H > ( Down2H);
                    ResH3 = Current3H < ( Up3H) && Current3H > ( Down3H);
                    ResH4 = Current4H < (Theory4H + Up4H) && Current4H > (Theory4H + Down4H);
                    ResH5 = Current5H < (Theory5H + Up5H) && Current5H > (Theory5H + Down5H);
                    ResH6 = Current6H < (Theory6H + Up6H) && Current6H > (Theory6H + Down6H);
                    if (ResH1 & ResH2 & ResH3)
                    {
                        ResAll = true; Logger.Info($"高度数据解析完成，结果OK:{ rcv}", 3);
                    }
                    else
                    {
                        ResAll = false;
                        Logger.Error($"高度数据解析完成，结果NG:{ rcv}", 3);
                    }
                    if (ResAll != ResPLC)
                    {
                        Logger.Error($"高度数据解析完成PLC结果和上位机结果不一致");
                        ResAll = false;
                    }
                }
                else
                {
                    Logger.Error($"高度数据解析出错,读出的数量不对{rcv}");
                }                                                                                          
            }
            catch (Exception ex)
            {
                Logger.Error($"高度数据解析出错{rcv}，{ex}");
            }                            
        }
        /// <summary>
        /// 保存高度数据到数据库
        /// </summary>
        /// <param name="cord">二维码</param>
        public void SaveToDB_High(string cord)
        {
            //检测该二维码之前做过没有
            string sql2 = $"SELECT * from  Height_CCD  WHERE Cord = '{cord}'";
            int i2 = int.Parse(SQLHelper.ExecuteScalar(sql2));
            if (i2 > 0)
            {
                sql2 = $" delete FROM Height_CCD WHERE Cord = '{cord}'";
                int i3 = SQLHelper.Update(sql2);
                if (i3 > 0)
                {
                    Logger.Info($"从高度检测数据库删除{cord}的数据成功", 4);
                }
                else
                {
                    Logger.Error($"从高度检测数据库删除{cord}的数据失败", 4);
                }
            }
            //存入数据库
            Cords = cord;
            string sql = "insert into Height_CCD( InsertTime,Cord,VehicleNumber,ModelNumber,ProductType,Result,ResultPLC,Sheild," +
                "Current1H,Current2H,Current3H," +
               "Theory1H,Theory2H,Theory3H," +
               "Up1H,Up2H,Up3H," +
               "Down1H,Down2H,Down3H)" +
               " values( @InsertTime,@Cord,@VehicleNumber,@ModelNumber,@ProductType,@Result,@ResultPLC,@Sheild," +
               "@Current1H,@Current2H, @Current3H," +
              "@Theory1H,@Theory2H,@Theory3H,@" +
               "Up1H,@Up2H,@Up3H,@" +
               "Down1H,@Down2H,@Down3H)";
            SqlParameter[] param = new SqlParameter[]
            {
                   new SqlParameter("@InsertTime", DateTime.Now),
                   new SqlParameter("@Cord",cord),
                   new SqlParameter("@VehicleNumber", VehicleNumber),
                   new SqlParameter("@ModelNumber", ModelNumber),
                   new SqlParameter("@ProductType", SysStatus.CurProductName),
                   new SqlParameter("@Result", ResAll),
                   new SqlParameter("@ResultPLC", ResPLC),
                   new SqlParameter("@Sheild", Sheild),
                   new SqlParameter("@Current1H",Current1H),
                   new SqlParameter("@Current2H", Current2H),
                   new SqlParameter("@Current3H",Current3H),
                   //new SqlParameter("@Current4H", Current4H),
                   //new SqlParameter("@Current5H",Current5H),
                   //new SqlParameter("@Current6H", Current6H),
                   //new SqlParameter("@Current7H",Current7H),
                   //new SqlParameter("@Current8H", Current8H),
                   //new SqlParameter("@Current9H",Current9H),
                   //new SqlParameter("@Current10H", Current10H),
                   new SqlParameter("@Theory1H",Theory1H),
                   new SqlParameter("@Theory2H",Theory2H),
                   new SqlParameter("@Theory3H",Theory3H),
                   //new SqlParameter("@Theory4H",Theory4H),
                   //new SqlParameter("@Theory5H",Theory5H),
                   //new SqlParameter("@Theory6H",Theory6H),
                   //new SqlParameter("@Theory7H",Theory7H),
                   //new SqlParameter("@Theory8H",Theory8H),
                   //new SqlParameter("@Theory9H",Theory9H),
                   //new SqlParameter("@Theory10H",Theory10H),
                   new SqlParameter("@Up1H", Up1H),
                   new SqlParameter("@Up2H", Up2H),
                   new SqlParameter("@Up3H", Up3H),
                   //new SqlParameter("@Up4H", Up4H),
                   //new SqlParameter("@Up5H", Up5H),
                   //new SqlParameter("@Up6H", Up6H),
                   //new SqlParameter("@Up7H", Up7H),
                   //new SqlParameter("@Up8H", Up8H),
                   //new SqlParameter("@Up9H", Up9H),
                   //new SqlParameter("@Up10H", Up10H),
                   new SqlParameter("@Down1H", Down1H),
                   new SqlParameter("@Down2H", Down2H),
                   new SqlParameter("@Down3H", Down3H),
                   //new SqlParameter("@Down4H", Down4H),
                   //new SqlParameter("@Down5H", Down5H),
                   //new SqlParameter("@Down6H", Down6H),
                   //new SqlParameter("@Down7H", Down7H),
                   //new SqlParameter("@Down8H", Down8H),
                   //new SqlParameter("@Down9H", Down9H),
                   //new SqlParameter("@Down10H", Down10H),

            };
            int i = SQLHelper.Update(sql, param);
            if (i > 0)
            {
                Logger.Info("高度数据写入位置度数据库成功");
            }
            else
            {
                Logger.Error("高度数据写入位置度数据库失败");
            }
        }
        /// <summary>
        /// 从数据库加载  高度
        /// </summary>
        /// <param name="cord"></param>
        public void LoadFromDB_High(string cord)
        {
            init_High();
            try
            {
                string sql = $" select * from Height_CCD where Cord = '{cord}'";
                SqlDataReader reader = SQLHelper.GetReader(sql);
                while (reader.Read())
                {
                    ResAll = (bool)(reader["Result"]);
                    ResPLC = (bool)(reader["ResultPLC"]);
                    Sheild = (bool)(reader["Sheild"]);
                    Current1H = (double)(reader["Current1H"]);
                    Current2H = (double)(reader["Current2H"]);
                    Current3H = (double)(reader["Current3H"]);
                    //Current4H = (double)(reader["Current4H"]);
                    //Current5H = (double)(reader["Current5H"]);
                    //Current6H = (double)(reader["Current6H"]);
                    //Current7H = (double)(reader["Current7H"]);
                    //Current8H = (double)(reader["Current8H"]);
                    //Current9H = (double)(reader["Current9H"]);
                    //Current10H = (double)(reader["Current10H"]);
                    Theory1H = (double)(reader["Theory1H"]);
                    Theory2H = (double)(reader["Theory2H"]);
                    Theory3H = (double)(reader["Theory3H"]);
                    //Theory4H = (double)(reader["Theory4H"]);
                    //Theory5H = (double)(reader["Theory5H"]);
                    //Theory6H = (double)(reader["Theory6H"]);
                    //Theory7H = (double)(reader["Theory7H"]);
                    //Theory8H = (double)(reader["Theory8H"]);
                    //Theory9H = (double)(reader["Theory9H"]);
                    //Theory10H = (double)(reader["Theory10H"]);
                    Up1H = (double)(reader["Up1H"]);
                    Up2H = (double)(reader["Up2H"]);
                    Up3H = (double)(reader["Up3H"]);
                    //Up4H = (double)(reader["Up4H"]);
                    //Up5H = (double)(reader["Up5H"]);
                    //Up6H = (double)(reader["Up6H"]);
                    //Up7H = (double)(reader["Up7H"]);
                    //Up8H = (double)(reader["Up8H"]);
                    //Up9H = (double)(reader["Up9H"]);
                    //Up10H = (double)(reader["Up10H"]);
                    Down1H = (double)(reader["Down1H"]);
                    Down2H = (double)(reader["Down2H"]);
                    Down3H = (double)(reader["Down3H"]);
                    //Down4H = (double)(reader["Down4H"]);
                    //Down5H = (double)(reader["Down5H"]);
                    //Down6H = (double)(reader["Down6H"]);
                    //Down7H = (double)(reader["Down7H"]);
                    //Down8H = (double)(reader["Down8H"]);
                    //Down9H = (double)(reader["Down9H"]);
                    //Down10H = (double)(reader["Down10H"]);

                }
                reader.Close();
            }
            catch (Exception ex)
            {
              
                Logger.Error($"读取高度数据库出错{ex.Message}");
            }
}
        #endregion

        #region 加载-保存位置度参数
        /// <summary>
        /// 导入参数-位置度参数
        /// </summary>
     
            public void LoadParmPostion(string filename1, int flag)
        {
            string filename = "";
            if (flag == 0)
                filename = SysStatus.sys_dir_path + "\\product\\" + SysStatus.CurProductName + "\\CPostion.ini";// 配置文件路径
            else
                filename = SysStatus.sys_dir_path + "\\product\\" + SysStatus.CurProductName + "\\NPostion.ini";// 配置文件路径


            IniFile inf = new IniFile(filename);//确认路径是否存在，不存在则创建文件夹。
            //默认的是STEP1的参数
            string STEP = "理论值";
            Theory1X = inf.ReadDouble(STEP, "Theory1X", Theory1X);
            Theory1Y = inf.ReadDouble(STEP, "Theory1Y", Theory1Y);
            Theory2X = inf.ReadDouble(STEP, "Theory2X", Theory2X);
            Theory2Y = inf.ReadDouble(STEP, "Theory2Y", Theory2Y);
            Theory3X = inf.ReadDouble(STEP, "Theory3X", Theory3X);
            Theory3Y = inf.ReadDouble(STEP, "Theory3Y", Theory3Y);
            Theory4X = inf.ReadDouble(STEP, "Theory4X", Theory4X);
            Theory4Y = inf.ReadDouble(STEP, "Theory4Y", Theory4Y);
            Theory5X = inf.ReadDouble(STEP, "Theory5X", Theory5X);
            Theory5Y = inf.ReadDouble(STEP, "Theory5Y", Theory5Y);
            Theory6X = inf.ReadDouble(STEP, "Theory6X", Theory6X);
            Theory6Y = inf.ReadDouble(STEP, "Theory6Y", Theory6Y);
            Theory7X = inf.ReadDouble(STEP, "Theory7X", Theory7X);
            Theory7Y = inf.ReadDouble(STEP, "Theory7Y", Theory7Y);
            Theory8X = inf.ReadDouble(STEP, "Theory8X", Theory8X);
            Theory8Y = inf.ReadDouble(STEP, "Theory8Y", Theory8Y);
            Theory9X = inf.ReadDouble(STEP, "Theory9X", Theory9X);
            Theory9Y = inf.ReadDouble(STEP, "Theory9Y", Theory9Y);
            Theory10X = inf.ReadDouble(STEP, "Theory10X", Theory10X);
            Theory10Y = inf.ReadDouble(STEP, "Theory10Y", Theory10Y);
            Theory11X = inf.ReadDouble(STEP, "Theory11X", Theory11X);
            Theory11Y = inf.ReadDouble(STEP, "Theory11Y", Theory11Y);
            Theory12X = inf.ReadDouble(STEP, "Theory12X", Theory12X);
            Theory12Y = inf.ReadDouble(STEP, "Theory12Y", Theory12Y);
            Theory13X = inf.ReadDouble(STEP, "Theory13X", Theory13X);
            Theory13Y = inf.ReadDouble(STEP, "Theory13Y", Theory13Y);
            STEP = "上限";
            Up1X = inf.ReadDouble(STEP, "Up1X", Up1X);
            Up1Y = inf.ReadDouble(STEP, "Up1Y", Up1Y);
            Up2X = inf.ReadDouble(STEP, "Up2X", Up2X);
            Up2Y = inf.ReadDouble(STEP, "Up2Y", Up2Y);
            Up3X = inf.ReadDouble(STEP, "Up3X", Up3X);
            Up3Y = inf.ReadDouble(STEP, "Up3Y", Up3Y);
            Up4X = inf.ReadDouble(STEP, "Up4X", Up4X);
            Up4Y = inf.ReadDouble(STEP, "Up4Y", Up4Y);
            
            UpP1 = Up5X = inf.ReadDouble(STEP, "Up5X", Up5X);
            UpP2 = Up5Y = inf.ReadDouble(STEP, "Up5Y", Up5Y);
            UpP3 = Up6X = inf.ReadDouble(STEP, "Up6X", Up6X);
            UpP4 = Up6Y = inf.ReadDouble(STEP, "Up6Y", Up6Y);
            Up7X = inf.ReadDouble(STEP, "Up7X", Up7X);
            Up7Y = inf.ReadDouble(STEP, "Up7Y", Up7Y);
            Up8X = inf.ReadDouble(STEP, "Up8X", Up8X);
            Up8Y = inf.ReadDouble(STEP, "Up8Y", Up8Y);
            Up9X = inf.ReadDouble(STEP, "Up9X", Up9X);
            Up9Y = inf.ReadDouble(STEP, "Up9Y", Up9Y);
            Up10X = inf.ReadDouble(STEP, "Up10X", Up10X);
            Up10Y = inf.ReadDouble(STEP, "Up10Y", Up10Y);
            Up11X = inf.ReadDouble(STEP, "Up11X", Up11X);
            Up11Y = inf.ReadDouble(STEP, "Up11Y", Up11Y);
            Up12X = inf.ReadDouble(STEP, "Up12X", Up12X);
            Up12Y = inf.ReadDouble(STEP, "Up12Y", Up12Y);
            Up13X = inf.ReadDouble(STEP, "Up13X", Up13X);
            Up13Y = inf.ReadDouble(STEP, "Up13Y", Up13Y);

            STEP = "下限";
            Down1X = inf.ReadDouble(STEP, "Down1X", Down1X);
            Down1Y = inf.ReadDouble(STEP, "Down1Y", Down1Y);
            Down2X = inf.ReadDouble(STEP, "Down2X", Down2X);
            Down2Y = inf.ReadDouble(STEP, "Down2Y", Down2Y);
            Down3X = inf.ReadDouble(STEP, "Down3X", Down3X);
            Down3Y = inf.ReadDouble(STEP, "Down3Y", Down3Y);
            Down4X = inf.ReadDouble(STEP, "Down4X", Down4X);
            Down4Y = inf.ReadDouble(STEP, "Down4Y", Down4Y);
            DownP1 = Down5X = inf.ReadDouble(STEP, "Down5X", Down5X);
            DownP2 = Down5Y = inf.ReadDouble(STEP, "Down5Y", Down5Y);
            DownP3 = Down6X = inf.ReadDouble(STEP, "Down6X", Down6X);
            DownP4 = Down6Y = inf.ReadDouble(STEP, "Down6Y", Down6Y);
            Down7X = inf.ReadDouble(STEP, "Down7X", Down7X);
            Down7Y = inf.ReadDouble(STEP, "Down7Y", Down7Y);
            Down8X = inf.ReadDouble(STEP, "Down8X", Down8X);
            Down8Y = inf.ReadDouble(STEP, "Down8Y", Down8Y);
            Down9X = inf.ReadDouble(STEP, "Down9X", Down9X);
            Down9Y = inf.ReadDouble(STEP, "Down9Y", Down9Y);
            Down10X = inf.ReadDouble(STEP, "Down10X", Down10X);
            Down10Y = inf.ReadDouble(STEP, "Down10Y", Down10Y);
            Down11X = inf.ReadDouble(STEP, "Down11X", Down11X);
            Down11Y = inf.ReadDouble(STEP, "Down11Y", Down11Y);
            Down12X = inf.ReadDouble(STEP, "Down12X", Down12X);
            Down12Y = inf.ReadDouble(STEP, "Down12Y", Down12Y);
            Down13X = inf.ReadDouble(STEP, "Down13X", Down13X);
            Down13Y = inf.ReadDouble(STEP, "Down13Y", Down13Y);
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
            CompensateX10 = inf.ReadDouble(STEP, "CompensateX10", CompensateX10);
            CompensateY10 = inf.ReadDouble(STEP, "CompensateY10", CompensateY10);
            CompensateX11 = inf.ReadDouble(STEP, "CompensateX11", CompensateX11);
            CompensateY11 = inf.ReadDouble(STEP, "CompensateY11", CompensateY11);
            CompensateX12 = inf.ReadDouble(STEP, "CompensateX12", CompensateX12);
            CompensateY12 = inf.ReadDouble(STEP, "CompensateY12", CompensateY12);
            CompensateX13 = inf.ReadDouble(STEP, "CompensateX13", CompensateX13);
            CompensateY13 = inf.ReadDouble(STEP, "CompensateY13", CompensateY13);

        }
        /// <summary>
        /// 保存配置-位置度参数
        /// </summary>
        public void SaveParmPostion(string filename1, int flag)
        {
            string filename = "";

            if (flag == 0)
                filename = SysStatus.sys_dir_path + "\\product\\" + SysStatus.CurProductName + "\\CPostion.ini";// 配置文件路径
            else
                filename = SysStatus.sys_dir_path + "\\product\\" + SysStatus.CurProductName + "\\NPostion.ini";// 配置文件路径
            IniFile inf = new IniFile(filename);//确认路径是否存在，不存在则创建文件夹。
                                                //默认的是STEP1的参数
            string STEP = "理论值";
            inf.WriteDouble(STEP, "Theory1X", Theory1X);
            inf.WriteDouble(STEP, "Theory1X", Theory1X);
            inf.WriteDouble(STEP, "Theory1Y", Theory1Y);
            inf.WriteDouble(STEP, "Theory2X", Theory2X);
            inf.WriteDouble(STEP, "Theory2Y", Theory2Y);
            inf.WriteDouble(STEP, "Theory3X", Theory3X);
            inf.WriteDouble(STEP, "Theory3Y", Theory3Y);
            inf.WriteDouble(STEP, "Theory4X", Theory4X);
            inf.WriteDouble(STEP, "Theory4Y", Theory4Y);
            inf.WriteDouble(STEP, "Theory5X", Theory5X);
            inf.WriteDouble(STEP, "Theory5Y", Theory5Y);
            inf.WriteDouble(STEP, "Theory6X", Theory6X);
            inf.WriteDouble(STEP, "Theory6Y", Theory6Y);
            inf.WriteDouble(STEP, "Theory7X", Theory7X);
            inf.WriteDouble(STEP, "Theory7Y", Theory7Y);
            inf.WriteDouble(STEP, "Theory8X", Theory8X);
            inf.WriteDouble(STEP, "Theory8Y", Theory8Y);
            inf.WriteDouble(STEP, "Theory9X", Theory9X);
            inf.WriteDouble(STEP, "Theory9Y", Theory9Y);
            inf.WriteDouble(STEP, "Theory10X", Theory10X);
            inf.WriteDouble(STEP, "Theory10Y", Theory10Y);
            inf.WriteDouble(STEP, "Theory11X", Theory11X);
            inf.WriteDouble(STEP, "Theory11Y", Theory11Y);
            inf.WriteDouble(STEP, "Theory12X", Theory12X);
            inf.WriteDouble(STEP, "Theory12Y", Theory12Y);
            inf.WriteDouble(STEP, "Theory13X", Theory13X);
            inf.WriteDouble(STEP, "Theory13Y", Theory13Y);
            STEP = "上限";
            inf.WriteDouble(STEP, "Up1X", Up1X);
            inf.WriteDouble(STEP, "Up1Y", Up1Y);
            inf.WriteDouble(STEP, "Up2X", Up2X);
            inf.WriteDouble(STEP, "Up2Y", Up2Y);
            inf.WriteDouble(STEP, "Up3X", Up3X);
            inf.WriteDouble(STEP, "Up3Y", Up3Y);
            inf.WriteDouble(STEP, "Up4X", Up4X);
            inf.WriteDouble(STEP, "Up4Y", Up4Y);
            inf.WriteDouble(STEP, "Up5X", Up5X);
            inf.WriteDouble(STEP, "Up5Y", Up5Y);
            inf.WriteDouble(STEP, "Up6X", Up6X);
            inf.WriteDouble(STEP, "Up6Y", Up6Y);
            inf.WriteDouble(STEP, "Up7X", Up7X);
            inf.WriteDouble(STEP, "Up7Y", Up7Y);
            inf.WriteDouble(STEP, "Up8X", Up8X);
            inf.WriteDouble(STEP, "Up8Y", Up8Y);
            inf.WriteDouble(STEP, "Up9X", Up9X);
            inf.WriteDouble(STEP, "Up9Y", Up9Y);
            inf.WriteDouble(STEP, "Up10X", Up10X);
            inf.WriteDouble(STEP, "Up10Y", Up10Y);
            inf.WriteDouble(STEP, "Up11X", Up11X);
            inf.WriteDouble(STEP, "Up11Y", Up11Y);
            inf.WriteDouble(STEP, "Up12X", Up12X);
            inf.WriteDouble(STEP, "Up12Y", Up12Y);
            inf.WriteDouble(STEP, "Up13X", Up13X);
            inf.WriteDouble(STEP, "Up13Y", Up13Y);
            STEP = "下限";
            inf.WriteDouble(STEP, "Down1X", Down1X);
            inf.WriteDouble(STEP, "Down1Y", Down1Y);
            inf.WriteDouble(STEP, "Down2X", Down2X);
            inf.WriteDouble(STEP, "Down2Y", Down2Y);
            inf.WriteDouble(STEP, "Down3X", Down3X);
            inf.WriteDouble(STEP, "Down3Y", Down3Y);
            inf.WriteDouble(STEP, "Down4X", Down4X);
            inf.WriteDouble(STEP, "Down4Y", Down4Y);
            inf.WriteDouble(STEP, "Down5X", Down5X);
            inf.WriteDouble(STEP, "Down5Y", Down5Y);
            inf.WriteDouble(STEP, "Down6X", Down6X);
            inf.WriteDouble(STEP, "Down6Y", Down6Y);
            inf.WriteDouble(STEP, "Down7X", Down7X);
            inf.WriteDouble(STEP, "Down7Y", Down7Y);
            inf.WriteDouble(STEP, "Down8X", Down8X);
            inf.WriteDouble(STEP, "Down8Y", Down8Y);
            inf.WriteDouble(STEP, "Down9X", Down9X);
            inf.WriteDouble(STEP, "Down9Y", Down9Y);
            inf.WriteDouble(STEP, "Down10X", Down10X);
            inf.WriteDouble(STEP, "Down10Y", Down10Y);
            inf.WriteDouble(STEP, "Down11X", Down11X);
            inf.WriteDouble(STEP, "Down11Y", Down11Y);
            inf.WriteDouble(STEP, "Down12X", Down12X);
            inf.WriteDouble(STEP, "Down12Y", Down12Y);
            inf.WriteDouble(STEP, "Down13X", Down13X);
            inf.WriteDouble(STEP, "Down13Y", Down13Y);
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
            inf.WriteDouble(STEP, "CompensateX10", CompensateX10);
            inf.WriteDouble(STEP, "CompensateY10", CompensateY10);
            inf.WriteDouble(STEP, "CompensateX11", CompensateX11);
            inf.WriteDouble(STEP, "CompensateY11", CompensateY11);
            inf.WriteDouble(STEP, "CompensateX12", CompensateX12);
            inf.WriteDouble(STEP, "CompensateY12", CompensateY12);
            inf.WriteDouble(STEP, "CompensateX13", CompensateX13);
            inf.WriteDouble(STEP, "CompensateY13", CompensateY13);

        }
        #endregion
        #region 加载-保存高度参数
        /// <summary>
        /// 导入参数-高度参数
        /// </summary>
        public void LoadParmHigh(string filename1, int flag)
        {
            string filename = "";

            if (flag == 0)
                filename = SysStatus.sys_dir_path + "\\product\\" + SysStatus.CurProductName + "\\CHigh.ini";// 配置文件路径
            else
                filename = SysStatus.sys_dir_path + "\\product\\" + SysStatus.CurProductName + "\\NHigh.ini";// 配置文件路径                 

            IniFile inf = new IniFile(filename);//确认路径是否存在，不存在则创建文件夹。
            //默认的是STEP1的参数
            string STEP = "基准值";
            BaseHigh = inf.ReadDouble(STEP, "BaseHigh", BaseHigh);
            BaseHigh1 = inf.ReadDouble(STEP, "BaseHigh1", BaseHigh1);
            STEP = "理论值";
            Theory1H = inf.ReadDouble(STEP, "Theory1H", Theory1H);
            Theory2H = inf.ReadDouble(STEP, "Theory2H", Theory2H);
            Theory3H = inf.ReadDouble(STEP, "Theory3H", Theory3H);
            Theory4H = inf.ReadDouble(STEP, "Theory4H", Theory4H);
            Theory5H = inf.ReadDouble(STEP, "Theory5H", Theory5H);
            Theory6H = inf.ReadDouble(STEP, "Theory6H", Theory6H);
            Theory7H = inf.ReadDouble(STEP, "Theory7H", Theory7H);
            Theory8H = inf.ReadDouble(STEP, "Theory8H", Theory8H);
            Theory9H = inf.ReadDouble(STEP, "Theory9H", Theory9H);
            Theory10H = inf.ReadDouble(STEP, "Theory10H", Theory10H);
            Theory11H = inf.ReadDouble(STEP, "Theory11H", Theory11H);
            Theory12H = inf.ReadDouble(STEP, "Theory12H", Theory12H);
            Theory13H = inf.ReadDouble(STEP, "Theory13H", Theory13H);
            
            STEP = "上限";
            Up1H = inf.ReadDouble(STEP, "Up1H", Up1H);
            Up2H = inf.ReadDouble(STEP, "Up2H", Up2H);
            Up3H = inf.ReadDouble(STEP, "Up3H", Up3H);
            Up4H = inf.ReadDouble(STEP, "Up4H", Up4H);
            Up5H = inf.ReadDouble(STEP, "Up5H", Up5H);
            Up6H = inf.ReadDouble(STEP, "Up6H", Up6H);
            Up7H = inf.ReadDouble(STEP, "Up7H", Up7H);
            Up8H = inf.ReadDouble(STEP, "Up8H", Up8H);
            Up9H = inf.ReadDouble(STEP, "Up9H", Up9H);
            Up10H = inf.ReadDouble(STEP, "Up10H", Up10H);
            Up11H = inf.ReadDouble(STEP, "Up11H", Up11H);
            Up12H = inf.ReadDouble(STEP, "Up12H", Up12H);
            Up13H = inf.ReadDouble(STEP, "Up13H", Up13H);
           
            STEP = "下限";
            Down1H = inf.ReadDouble(STEP, "Down1H", Down1H);
            Down2H = inf.ReadDouble(STEP, "Down2H", Down2H);
            Down3H = inf.ReadDouble(STEP, "Down3H", Down3H);
            Down4H = inf.ReadDouble(STEP, "Down4H", Down4H);
            Down5H = inf.ReadDouble(STEP, "Down5H", Down5H);
            Down6H = inf.ReadDouble(STEP, "Down6H", Down6H);
            Down7H = inf.ReadDouble(STEP, "Down7H", Down7H);
            Down8H = inf.ReadDouble(STEP, "Down8H", Down8H);
            Down9H = inf.ReadDouble(STEP, "Down9H", Down9H);
            Down10H = inf.ReadDouble(STEP, "Down10H", Down10H);
            Down11H = inf.ReadDouble(STEP, "Down11H", Down11H);
            Down12H = inf.ReadDouble(STEP, "Down12H", Down12H);
            Down13H = inf.ReadDouble(STEP, "Down13H", Down13H);
           
            STEP = "补偿值";
            Compensate1H = inf.ReadDouble(STEP, "Compensate1H", Compensate1H);
            Compensate2H = inf.ReadDouble(STEP, "Compensate2H", Compensate2H);
            Compensate3H = inf.ReadDouble(STEP, "Compensate3H", Compensate3H);
            Compensate4H = inf.ReadDouble(STEP, "Compensate4H", Compensate4H);
            Compensate5H = inf.ReadDouble(STEP, "Compensate5H", Compensate5H);
            Compensate6H = inf.ReadDouble(STEP, "Compensate6H", Compensate6H);
            Compensate7H = inf.ReadDouble(STEP, "Compensate7H", Compensate7H);
            Compensate8H = inf.ReadDouble(STEP, "Compensate8H", Compensate8H);
            Compensate9H = inf.ReadDouble(STEP, "Compensate9H", Compensate9H);
            Compensate10H = inf.ReadDouble(STEP, "Compensate10H", Compensate10H);
            Compensate11H = inf.ReadDouble(STEP, "Compensate11H", Compensate11H);
            Compensate12H = inf.ReadDouble(STEP, "Compensate12H", Compensate12H);
            Compensate13H = inf.ReadDouble(STEP, "Compensate13H", Compensate13H);
           

        }
        /// <summary>
        /// 保存配置-高度参数
        /// </summary>
        public void SaveParmHigh(string filename1, int flag)
        {
            string filename = "";

            if (flag == 0)
                filename = SysStatus.sys_dir_path + "\\product\\" + SysStatus.CurProductName + "\\CHigh.ini";// 配置文件路径
            else
                filename = SysStatus.sys_dir_path + "\\product\\" + SysStatus.CurProductName + "\\NHigh.ini";// 配置文件路径


            IniFile inf = new IniFile(filename);//确认路径是否存在，不存在则创建文件夹。
            string STEP = "基准值";
            inf.WriteDouble(STEP, "BaseHigh", BaseHigh);
            inf.WriteDouble(STEP, "BaseHigh1", BaseHigh1);
            STEP = "理论值";
            inf.WriteDouble(STEP, "Theory1H", Theory1H);
            inf.WriteDouble(STEP, "Theory2H", Theory2H);
            inf.WriteDouble(STEP, "Theory3H", Theory3H);
            inf.WriteDouble(STEP, "Theory4H", Theory4H);
            inf.WriteDouble(STEP, "Theory5H", Theory5H);
            inf.WriteDouble(STEP, "Theory6H", Theory6H);
            inf.WriteDouble(STEP, "Theory7H", Theory7H);
            inf.WriteDouble(STEP, "Theory8H", Theory8H);
            inf.WriteDouble(STEP, "Theory9H", Theory9H);
            inf.WriteDouble(STEP, "Theory10H", Theory10H);
            inf.WriteDouble(STEP, "Theory11H", Theory11H);
            inf.WriteDouble(STEP, "Theory12H", Theory12H);
            inf.WriteDouble(STEP, "Theory13H", Theory13H);
            STEP = "上限";
            inf.WriteDouble(STEP, "Up1H", Up1H);
            inf.WriteDouble(STEP, "Up2H", Up2H);
            inf.WriteDouble(STEP, "Up3H", Up3H);
            inf.WriteDouble(STEP, "Up4H", Up4H);
            inf.WriteDouble(STEP, "Up5H", Up5H);
            inf.WriteDouble(STEP, "Up6H", Up6H);
            inf.WriteDouble(STEP, "Up7H", Up7H);
            inf.WriteDouble(STEP, "Up8H", Up8H);
            inf.WriteDouble(STEP, "Up9H", Up9H);
            inf.WriteDouble(STEP, "Up10H", Up10H);
            inf.WriteDouble(STEP, "Up11H", Up11H);
            inf.WriteDouble(STEP, "Up12H", Up12H);
            inf.WriteDouble(STEP, "Up13H", Up13H);
            STEP = "下限";
            inf.WriteDouble(STEP, "Down1H", Down1H);
            inf.WriteDouble(STEP, "Down2H", Down2H);
            inf.WriteDouble(STEP, "Down3H", Down3H);
            inf.WriteDouble(STEP, "Down4H", Down4H);
            inf.WriteDouble(STEP, "Down5H", Down5H);
            inf.WriteDouble(STEP, "Down6H", Down6H);
            inf.WriteDouble(STEP, "Down7H", Down7H);
            inf.WriteDouble(STEP, "Down8H", Down8H);
            inf.WriteDouble(STEP, "Down9H", Down9H);
            inf.WriteDouble(STEP, "Down10H", Down10H);
            inf.WriteDouble(STEP, "Down11H", Down11H);
            inf.WriteDouble(STEP, "Down12H", Down12H);
            inf.WriteDouble(STEP, "Down13H", Down13H);
            STEP = "补偿值";
            inf.WriteDouble(STEP, "Compensate1H", Compensate1H);
            inf.WriteDouble(STEP, "Compensate2H", Compensate2H);
            inf.WriteDouble(STEP, "Compensate3H", Compensate3H);
            inf.WriteDouble(STEP, "Compensate4H", Compensate4H);
            inf.WriteDouble(STEP, "Compensate5H", Compensate5H);
            inf.WriteDouble(STEP, "Compensate6H", Compensate6H);
            inf.WriteDouble(STEP, "Compensate7H", Compensate7H);
            inf.WriteDouble(STEP, "Compensate8H", Compensate8H);
            inf.WriteDouble(STEP, "Compensate9H", Compensate9H);
            inf.WriteDouble(STEP, "Compensate10H", Compensate10H);
            inf.WriteDouble(STEP, "Compensate11H", Compensate11H);
            inf.WriteDouble(STEP, "Compensate12H", Compensate12H);
            inf.WriteDouble(STEP, "Compensate13H", Compensate13H);


        }
        #endregion
        private double StoD(string aa)
        {
            try
            {
                return Convert.ToDouble(aa);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return 999.9;
            }           
        }
    }
}
