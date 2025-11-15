using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CXPro001.myclass;
using MyLib.SqlHelper;
namespace CXPro001.classes
{
  public  class My_CCD
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

        #region 高度参数
            public double Current1H, Current2H, Current3H, Current4H, Current5H, Current6H;
        #endregion

        #region 位置参数
        /// <summary>
        /// 位置度实测值1X 1Y---13X 13Y
        /// </summary>
        public double Current1X, Current1Y, Current2X, Current2Y, Current3X, Current3Y, Current4X, Current4Y, Current5X, Current5Y, Current6X, Current6Y,
            Current7X, Current7Y, Current8X, Current8Y, Current9X, Current9Y, Current10X, Current10Y, Current11X, Current11Y, Current12X, Current12Y, Current13X, Current13Y;
        /// <summary>
        ///  各Pin 位置度  计算得出
        /// </summary>
        public double PosP1, PosP2, PosP3, PosP4, PosP5, PosP6, PosP7, PosP8, PosP9, PosP10, PosP11, PosP12, PosP13;
        public double PosP1_y, PosP2_y, PosP3_y, PosP4_y, PosP5_y, PosP6_y, PosP7_y, PosP8_y, PosP9_y, PosP10_y, PosP11_y, PosP12_y, PosP13_y;
        #endregion





        /// <summary>
        /// 最终结果
        /// </summary>
        public bool ResEnd_H;

        public bool ResEnd_P;
        /// <summary>
        /// //////////////////
        /// </summary>
        NetWorkHelper.TCP.ITcpClient my_client;

        public bool isCon = false;
        /// <summary>
        /// 反馈数据
        /// </summary>
        public string data1;

        private int X_Model = 0;///1位置度1  2高度1    3位置度2  4高度2
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="m"></param>
        public void set(NetWorkHelper.TCP.ITcpClient m,int modle)
        {
            my_client=m;
            X_Model = modle;
        }
        /// <summary>
        /// 连接
        /// </summary>
        public void Conn()
        {
            if(my_client!=null)
            my_client.StartConnect();
        }

        /// <summary>
        /// 一次拍照
        /// </summary>
        /// <param name="product">产品</param>
        /// <param name="model_num">磨具号</param>
        /// <param name="lr">左右</param>
        public void CMD_CAM1(int product, int model_num, string lr)
        {
            string str = "T1," + product.ToString() + "," + model_num.ToString() + "," + lr;
            byte[] bb = Encoding.UTF8.GetBytes(str);
            if (my_client == null || my_client.Client == null)
                return;           
            my_client.SendData(bb);
            data1 = "";


        }

        /// <summary>
        /// 二次拍照
        /// </summary>
        /// <param name="model_num">磨具号</param>
        /// <param name="lr">左右</param>
        public void CMD_CAM2(int product, int model_num, string lr)
        {
            string str = "T2," + product.ToString() + "," + model_num.ToString() + "," + lr;
            byte[] bb = Encoding.UTF8.GetBytes(str);
            if (my_client == null || my_client.Client == null)
                return;
                my_client.SendData(bb);
            data1 = "";
        }

        /// <summary>
        /// 接受数据
        /// </summary>
        /// <param name="data"></param>
        public void onReceve(byte[] data)
        {
            string temp= System.Text.Encoding.UTF8.GetString(data);
            if (temp.Contains("abc"))
            {
                temp = "";
                return;
            }
            data1 = temp;
            string[] dataArray = data1.Split('\r', '\n');
            string[] var = dataArray[0].Split(',');          
        }

       
        public void OnStateInfo()
        {

        }
        public void Set_Sts(bool sts)
        {
            isCon = sts;
        }
        public bool Get_Sts()
        {
            return isCon;
        }

        /// <summary>
        /// 解析高度
        /// </summary>
        /// <param name="rcv"></param>
        /// <param name="step"></param>
        public bool AnalyseData_Pos()
        {
            bool re = false;

            if (data1.Length < 5)
                re = false;

            if (data1.Contains("OK"))
                re = true;

            Logger.Info($"相机返回位置度数据:{data1}");
            string rcv = data1;
            string[] dataArray = rcv.Split('\r', '\n');
            string[] datarr = dataArray[0].Split(',');

            try
            {

                ResEnd_P = datarr[4] == "OK" ? true : false;
                if (datarr[0] == "T2")
                {
                    if (datarr.Length > 27)
                    {
                        Current1X = double.Parse(datarr[5]);
                        Current1Y = double.Parse(datarr[6]);
                        PosP1 = double.Parse(datarr[7]);
                        PosP1_y = double.Parse(datarr[8]);

                        Current2X = double.Parse(datarr[9]);
                        Current2Y = double.Parse(datarr[10]);
                        PosP2 = double.Parse(datarr[11]);
                        PosP2_y = double.Parse(datarr[12]);

                        Current3X = double.Parse(datarr[13]);
                        Current3Y = double.Parse(datarr[14]);
                        PosP3 = double.Parse(datarr[15]);
                        PosP3_y = double.Parse(datarr[16]);

                        Current4X = double.Parse(datarr[17]);
                        Current4Y = double.Parse(datarr[18]);
                        PosP4 = double.Parse(datarr[19]);
                        PosP4_y = double.Parse(datarr[20]);

                        Current5X = double.Parse(datarr[21]);
                        Current5Y = double.Parse(datarr[22]);
                        PosP5 = double.Parse(datarr[23]);
                        PosP5_y = double.Parse(datarr[24]);

                        Current6X = double.Parse(datarr[25]);
                        Current6Y = double.Parse(datarr[26]);
                        PosP6 = double.Parse(datarr[27]);
                        PosP6_y = double.Parse(datarr[28]);
                    }else
                    {
                        Current1X = double.Parse(datarr[5]);
                        Current2X = double.Parse(datarr[6]);
                        Current3X = double.Parse(datarr[7]);
                        Current4X = double.Parse(datarr[8]);
                        Current5X = double.Parse(datarr[9]);
                        Current6X = double.Parse(datarr[10]);
                        Current1Y = double.Parse(datarr[11]);
                        //PosP1 = double.Parse(datarr[7]);
                        Current2Y = double.Parse(datarr[12]);
                        //PosP2 = double.Parse(datarr[10]);
                        Current3Y = double.Parse(datarr[13]);
                        //PosP3 = double.Parse(datarr[13]);
                        Current4Y = double.Parse(datarr[14]);
                        //PosP4 = double.Parse(datarr[16]);
                        Current5Y = double.Parse(datarr[15]);
                        //PosP5 = double.Parse(datarr[19]);
                        Current6Y = double.Parse(datarr[16]);
                        //PosP6 = double.Parse(datarr[22]);
                     
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Error($"p数据错误"+data1+ex.Message, 4);
                re = false;
            }
            return re;
        }

        /// <summary>
        /// gaodu
        /// </summary>
        /// <param name="rcv"></param>
        /// <param name="step"></param>
        public bool AnalyseData_High()
        {
            bool re = false;

            if (data1.Length < 5)
                re = false;

            if (data1.Contains("OK"))
                re = true;


            string rcv = data1;
            string[] dataArray = rcv.Split('\r', '\n');
            string[] datarr = dataArray[0].Split(',');

          
            try
            {
                ResEnd_H = datarr[4] == "OK" ? true : false;
                Current1H = double.Parse(datarr[5]);
                Current2H = double.Parse(datarr[6]);
                Current3H = double.Parse(datarr[7]);
                Current4H = double.Parse(datarr[8]);
                Current5H = double.Parse(datarr[9]);
                Current6H = double.Parse(datarr[10]);
            }catch
            {
                Logger.Error($"h数据错误"+ data1);
                re = false;
            }
            return re;
        }
        /// <summary>
        ///  数据存入数据库
        /// </summary>
        /// <summary>
        /// 保存高度数据到数据库
        /// </summary>
        /// <param name="cord">二维码</param>
        public void SaveToDB_High(string cord)
        {
            ResAll = ResEnd_H;
            //检测该二维码之前做过没有

            if (cord.Length < 1)
            {
                Logger.Error($"高度检测数据缺少二维码", 4);
                return;
            }
            string sql2 = $"SELECT * from  Height_CCD  WHERE Cord = '{cord}'";

            if(X_Model==4)
                sql2 = $"SELECT * from  Height_CCD2  WHERE Cord = '{cord}'";

            int i2 = int.Parse(SQLHelper.ExecuteScalar(sql2));
            if (i2 > 0)
            {
                sql2 = $" delete FROM Height_CCD WHERE Cord = '{cord}'";
                if (X_Model == 4)
                    sql2 = $" delete FROM Height_CCD2 WHERE Cord = '{cord}'";
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

            string sql = "insert into Height_CCD( InsertTime,Cord,VehicleNumber,ModelNumber,ProductType,Result," +
                "Current1H,Current2H,Current3H," +
               "Current4H,Current5H,Current6H" +
               ")" +
               " values( @InsertTime,@Cord,@VehicleNumber,@ModelNumber,@ProductType,@Result," +
               "@Current1H,@Current2H, @Current3H,@Current4H,@Current5H,@Current6H" +
              ")";
            if (X_Model == 4)
                sql = "insert into Height_CCD2( InsertTime,Cord,VehicleNumber,ModelNumber,ProductType,Result," +
                 "Current1H,Current2H,Current3H," +
                "Current4H,Current5H,Current6H" +
                ")" +
                " values( @InsertTime,@Cord,@VehicleNumber,@ModelNumber,@ProductType,@Result," +
                "@Current1H,@Current2H, @Current3H,@Current4H,@Current5H,@Current6H" +
               ")";

            SqlParameter[] param = new SqlParameter[]
            {
                   new SqlParameter("@InsertTime", DateTime.Now),//时间
                   new SqlParameter("@Cord",cord),//二维码
                   new SqlParameter("@VehicleNumber", VehicleNumber),//车辆编号
                   new SqlParameter("@ModelNumber", ModelNumber),//型号
                   new SqlParameter("@ProductType", SysStatus.CurProductName),//设备名称
                   new SqlParameter("@Result", ResAll),//总结果
                                                       
                   new SqlParameter("@Current1H",Current1H),
                   new SqlParameter("@Current2H", Current2H),
                   new SqlParameter("@Current3H",Current3H),
                   new SqlParameter("@Current4H", Current4H),
                   new SqlParameter("@Current5H",Current5H),
                   new SqlParameter("@Current6H", Current6H),
                  

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
        /// 位置度数据存入数据库
        /// </summary>
        public void SaveToDB_Pos(string cord)
        {
            ResAll = ResEnd_P;
            if (cord.Length < 1)
            {
                Logger.Error($"位置度检测数据缺少二维码", 4);
                return;
            }//检测该二维码之前做过没有
            string sql2 = $"SELECT * from  Position_CCD  WHERE Cord = '{cord}'";
            if(X_Model==3)
                sql2 = $"SELECT * from  Position_CCD2  WHERE Cord = '{cord}'";

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
                "Current1X,Current1Y,Current2X,Current2Y,Current3X,Current3Y,Current4X,Current4Y,Current5X,Current5Y,Current6X,Current6Y," +
                "PosP1,PosP2,PosP3,PosP4,PosP5,PosP6,PosP1_y,PosP2_y,PosP3_y,PosP4_y,PosP5_y,PosP6_y" +
                " )" +
                " values( @InsertTime,@Cord,@VehicleNumber,@ModelNumber,@ProductType,@Result,@ResultPLC,@Sheild," +
                "@Current1X,@Current1Y, @Current2X,@Current2Y,@Current3X,@Current3Y,@Current4X,@Current4Y,@Current5X,@Current5Y,@Current6X,@Current6Y," +
                 "@PosP1,@PosP2,@PosP3,@PosP4,@PosP5,@PosP6,@PosP1_y,@PosP2_y,@PosP3_y,@PosP4_y,@PosP5_y,@PosP6_y" +
                 ")";

            if (X_Model == 3)
                sql = "insert into Position_CCD2( InsertTime,Cord,VehicleNumber,ModelNumber,ProductType,Result,ResultPLC,Sheild," +
                "Current1X,Current1Y,Current2X,Current2Y,Current3X,Current3Y,Current4X,Current4Y,Current5X,Current5Y,Current6X,Current6Y," +
                "PosP1,PosP2,PosP3,PosP4,PosP5,PosP6,PosP1_y,PosP2_y,PosP3_y,PosP4_y,PosP5_y,PosP6_y" +
                " )" +
                " values( @InsertTime,@Cord,@VehicleNumber,@ModelNumber,@ProductType,@Result,@ResultPLC,@Sheild," +
                "@Current1X,@Current1Y, @Current2X,@Current2Y,@Current3X,@Current3Y,@Current4X,@Current4Y,@Current5X,@Current5Y,@Current6X,@Current6Y," +
                 "@PosP1,@PosP2,@PosP3,@PosP4,@PosP5,@PosP6,@PosP1_y,@PosP2_y,@PosP3_y,@PosP4_y,@PosP5_y,@PosP6_y" +
                 ")";

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
                   new SqlParameter("@Current5X",Current5X), new SqlParameter("@Current5Y", Current5Y),
                   new SqlParameter("@Current6X",Current6X), new SqlParameter("@Current6Y", Current6Y),
                   new SqlParameter("@PosP1",PosP1),
                   new SqlParameter("@PosP2",PosP2),
                   new SqlParameter("@PosP3",PosP3),
                   new SqlParameter("@PosP4",PosP4),
                   new SqlParameter("@PosP5",PosP5),
                   new SqlParameter("@PosP6",PosP6),
                   new SqlParameter("@PosP1_y",PosP1_y),
                   new SqlParameter("@PosP2_y",PosP2_y),
                   new SqlParameter("@PosP3_y",PosP3_y),
                   new SqlParameter("@PosP4_y",PosP4_y),
                   new SqlParameter("@PosP5_y",PosP5_y),
                   new SqlParameter("@PosP6_y",PosP6_y),
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
       
        
    }
}
