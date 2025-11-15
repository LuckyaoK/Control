using CXPro001.myclass;
using MyLib.SqlHelper;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using xktComm.DataConvert;

namespace CXPro001.classes
{
    /// <summary>
    /// 测压仪器8740
    /// </summary>
    public class My_8740
    {


        public string Cord_data_L, ModeNumber_L, Cord_data_R, ModeNumber_R;
        /// <summary>
        /// 1OK 2断 3短
        /// </summary> 
        public int SOresPin1, SOresPin2, SOresPin3, SOresPin4, SOresPin5, SOresPin6, SOresPin7, SOresPin8, SOresPin9, SOresPin10, SOresPin11, SOresPin12;

        /// <summary>
        /// 各pin耐压电流值--8740
        /// </summary>
        public double VolvalPin1, VolvalPin2, VolvalPin3, VolvalPin4, VolvalPin5, VolvalPin6, VolvalPin7, VolvalPin8, VolvalPin9, VolvalPin10, VolvalPin11, VolvalPin12;

        /// <summary>
        /// 各pin的测试结果--8740   TURE为通过
        /// </summary>
        public bool ResPin1, ResPin2, ResPin3, ResPin4, ResPin5, ResPin6, ResPin7, ResPin8, ResPin9, ResPin10, ResPin11, ResPin12;

        /// <summary>
        /// 最终结果--8740
        /// </summary>
        public bool ResEnd;
        public bool ResEnd_L;
        public bool ResEnd_R;
        public bool ResPLC;
        public bool ResChenTao;
        public bool Shield;
        public List<string> B0 = new List<string>();
        public List<string> B1 = new List<string>();//电流数据

        /// <summary>
        /// 左产品是否通过测试
        /// </summary>
        public bool M_Product_L=false;//左产品是否
        /// <summary>
        /// 右产品是否通过测试
        /// </summary>
        public bool M_Product_R=false;

        public void init( )
        {
            Cord_data_L = ModeNumber_L = Cord_data_R = ModeNumber_R = "";
            ResChenTao = ResPLC = Shield = false;
            ResEnd = false;
            SOresPin1 = SOresPin2 = SOresPin3 = SOresPin4 = SOresPin5 = SOresPin6 = SOresPin7 = SOresPin8 = SOresPin9 = SOresPin10= SOresPin11= SOresPin12=2;

            ResPin1 = ResPin2 = ResPin3 = ResPin4 = ResPin5 = ResPin6 = ResPin7 = ResPin8 = ResPin9 = false;

            VolvalPin1 = VolvalPin2 = VolvalPin3 = VolvalPin4 = VolvalPin5 = VolvalPin6 = 999;

            VolvalPin6 = VolvalPin7 = VolvalPin8 = VolvalPin9 = VolvalPin10= VolvalPin11= VolvalPin12=999;
        }


        /// <summary>
        /// 数据切割成行
        /// </summary>
        /// <param name="datas"></param>
        private void data1(string datas)
        {
            datas = datas.Replace("\0", "");
            B0.Clear();
            B1.Clear();

            double ups = 0.3;//电流上限
            Logger.Info($"耐压返回数据{datas}", 2);
            string[] dataArray = datas.Split('\r', '\n');
            datas = "";
            for (int i = 0; i < dataArray.Length; i++)
            {
                if (dataArray[i].Contains("TestItem"))
                {
                    break;
                }
                if (dataArray[i] != "")
                {
                    B0.Add(dataArray[i]);
                }
                datas += dataArray[i];
            }

        }
        /// <summary>
        /// 解析电流数据
        /// </summary>
        /// <param name="datas"></param>
        private void data2(string datas)
        {
            string str;
            double ups = 0.3;//电流上限
            double X = 1.0;
            if (datas.Contains("uA")) X = 1;

            int index = 0;
            for (int i = 0; i < B0.Count; i++)
                if (B0[i].Contains("O/S"))
                    index = i;

            //提取电流值
            if (B0[index].Contains("O/S TEST"))//短断OK，数据OK
            {
                string pinres = "";

                //   if (B0[index + 1].Contains("uA")) ups = 0.3;
                //获取耐压值
                //   B1.Add(B0[index + 1].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());//1
                //  B1.Add(B0[index + 2].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());//2
                //  B1.Add(B0[index + 3].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());//3
                //   B1.Add(B0[index + 4].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());//4
                //    B1.Add(B0[index + 5].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());//5
                //   B1.Add(B0[index + 6].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());//6
                //   B1.Add(B0[index + 7].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());//7
                //    B1.Add(B0[index + 8].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());//8
                //    B1.Add(B0[index + 9].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());//9
                //     B1.Add(B0[index + 10].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());//10
                //    B1.Add(B0[index + 11].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());//11
                //     B1                str = B0[index + 1].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                string tempStr = B0[index + 1].Substring(13, 7).Trim();
                if (tempStr =="ARCING") str = "9999";
                else str = B0[index + 1].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                str = string_to_num(str);
                B1.Add(str);//1


                tempStr = B0[index + 2].Substring(13, 7).Trim();
                if (tempStr == "ARCING") str = "9999";
                else str = B0[index + 2].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                str = string_to_num(str);
                B1.Add(str);//1

                tempStr = B0[index + 3].Substring(13, 7).Trim();
                if (tempStr == "ARCING") str = "9999";
                else str = B0[index + 3].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                str = string_to_num(str);
                B1.Add(str);//1

              
                tempStr = B0[index + 4].Substring(13, 7).Trim();
                if (tempStr == "ARCING") str = "9999";
                else str = B0[index + 4].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                str = string_to_num(str);
                B1.Add(str);//1

                tempStr = B0[index + 5].Substring(13, 7).Trim();
                if (tempStr == "ARCING") str = "9999";
                else str = B0[index + 5].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                str = string_to_num(str);
                B1.Add(str);//1

                tempStr = B0[index + 6].Substring(13, 7).Trim();
                if (tempStr == "ARCING") str = "9999";
                else str = B0[index + 6].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                str = string_to_num(str);
                B1.Add(str);//1

                tempStr = B0[index + 7].Substring(13, 7).Trim();
                if (tempStr == "ARCING") str = "9999";
                else str = B0[index + 7].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                str = string_to_num(str);
                B1.Add(str);//1

                tempStr = B0[index + 8].Substring(13, 7).Trim();
                if (tempStr == "ARCING") str = "9999";
                else str = B0[index + 8].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                str = string_to_num(str);
                B1.Add(str);//1

                tempStr = B0[index + 9].Substring(13, 7).Trim();
                if (tempStr == "ARCING") str = "9999";
                else str = B0[index + 9].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                str = string_to_num(str);
                B1.Add(str);//1

                tempStr = B0[index + 10].Substring(13, 7).Trim();
                if (tempStr == "ARCING") str = "9999";
                else str = B0[index + 10].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                str = string_to_num(str);
                B1.Add(str);//1

                tempStr = B0[index + 11].Substring(13, 7).Trim();
                if (tempStr == "ARCING") str = "9999";
                else str = B0[index + 11].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                str = string_to_num(str);
                B1.Add(str);//1


                tempStr = B0[index + 12].Substring(13, 7).Trim();
                if (tempStr == "ARCING") str = "9999";
                else str = B0[index + 12].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                str = string_to_num(str);
                B1.Add(str);//1
                //str = B0[index + 2].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                //str = string_to_num(str);
                //B1.Add(str);//1

                //str = B0[index + 3].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                //str = string_to_num(str);
                //B1.Add(str);//1

                //str = B0[index + 4].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                //str = string_to_num(str);
                //B1.Add(str);//1

                //str = B0[index + 5].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                //str = string_to_num(str);
                //B1.Add(str);//1

                //str = B0[index + 6].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                //str = string_to_num(str);
                //B1.Add(str);//1

                //str = B0[index + 7].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                //str = string_to_num(str);
                //B1.Add(str);//1

                //str = B0[index + 8].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                //str = string_to_num(str);
                //B1.Add(str);//1

                //str = B0[index + 9].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                //str = string_to_num(str);
                //B1.Add(str);//1

                //str = B0[index + 10].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                //str = string_to_num(str);
                //B1.Add(str);//1

                //str = B0[index + 11].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                //str = string_to_num(str);
                //B1.Add(str);//1

                //str = B0[index + 12].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim();
                //str = string_to_num(str);
                B1.Add(str);//1.Add(B0[index + 12].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());//12

                for (int i = 0; i < B1.Count; i++)
                {
                    if (Convert.ToDouble(B1[i]) >= ups)//过电流
                    {
                        ResEnd = false;
                        if (i == 10)
                            pinres += "a" + ",";//未通过数量
                        else if (i == 11)
                            pinres += "b" + ",";//未通过数量                           
                        else
                            pinres += i.ToString() + ",";//未通过数量
                    }
                }
                ResPin1 = ResPin2 = ResPin3 = ResPin4 = ResPin5 = ResPin6 = ResPin7 = ResPin8 = ResPin9 = ResPin10 = ResPin11 = ResPin12 = true;
                //电流值
                if (B1.Count >= 1) VolvalPin1 = Convert.ToDouble(B1[0]) * X;
                if (B1.Count >= 2) VolvalPin2 = Convert.ToDouble(B1[1]) * X;
                if (B1.Count >= 3) VolvalPin3 = Convert.ToDouble(B1[2]) * X;
                if (B1.Count >= 4) VolvalPin4 = Convert.ToDouble(B1[3]) * X;
                if (B1.Count >= 5) VolvalPin5 = Convert.ToDouble(B1[4]) * X;
                if (B1.Count >= 6) VolvalPin6 = Convert.ToDouble(B1[5]) * X;

                if (B1.Count >= 7) VolvalPin7 = Convert.ToDouble(B1[6]) * X;
                if (B1.Count >= 8) VolvalPin8 = Convert.ToDouble(B1[7]) * X;
                if (B1.Count >= 9) VolvalPin9 = Convert.ToDouble(B1[8]) * X;
                if (B1.Count >= 10) VolvalPin10 = Convert.ToDouble(B1[9]) * X;
                if (B1.Count >= 11) VolvalPin11 = Convert.ToDouble(B1[10]) * X;
                if (B1.Count >= 12) VolvalPin12 = Convert.ToDouble(B1[11]) * X;

                //过电流的脚
                if (pinres.Contains("0") || VolvalPin1==0) ResPin1 = false;
                if (pinres.Contains("1") || VolvalPin2 == 0) ResPin2 = false;
                if (pinres.Contains("2") || VolvalPin3 == 0) ResPin3 = false;
                if (pinres.Contains("3") || VolvalPin4 == 0) ResPin4 = false;
                if (pinres.Contains("4") || VolvalPin5 == 0) ResPin5 = false;
                if (pinres.Contains("5") || VolvalPin6 == 0) ResPin6 = false;

                if (pinres.Contains("6") || VolvalPin7 == 0) ResPin7 = false;
                if (pinres.Contains("7") || VolvalPin8 == 0) ResPin8 = false;
                if (pinres.Contains("8") || VolvalPin9 == 0) ResPin9 = false;
                if (pinres.Contains("9") || VolvalPin10 == 0) ResPin10 = false;
                if (pinres.Contains("a") || VolvalPin11 == 0) ResPin11 = false;
                if (pinres.Contains("b") || VolvalPin12 == 0) ResPin12 = false;
            }
        }

        /// <summary>
        /// 判断是否要数值
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private string string_to_num(string str)
        {
            string re;
            try
            {
                re = str;
                double.Parse(str);
            }
            catch
            {
                re = "0";
            }

            return re;
        }
        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="mohao"></param>
        public bool AnalyseData(string datas)
        {
            //////////////
            try
            {
                M_Product_L = true;
                M_Product_R = true;
                ///////////////////
                double X = 1.0;

                data1(datas);//数据切割

                if (B0.Count < 3) return false;
                ///////////////数据拆分成行/////////////////////////////
                //提取电流值
                data2(datas);//提取电流

                //////开路检测///
                ResEnd = false;
                SOresPin1 = SOresPin2 = SOresPin3 = SOresPin4 = SOresPin5 = SOresPin6 = SOresPin7 = SOresPin8 = SOresPin9 = SOresPin10 = SOresPin11 = SOresPin12 = 1;
                for (int i = 0; i < B0.Count; i++)//找出断路 短路的脚
                {
                    if (B0[i].Contains("OPEN"))//开路
                    {
                        if (B0[i].Contains("A01") || B0[i].Contains("A02")) SOresPin1 = 2;
                        if (B0[i].Contains("A03") || B0[i].Contains("A04")) SOresPin2 = 2;
                        if (B0[i].Contains("A05") || B0[i].Contains("A06")) SOresPin3 = 2;
                        if (B0[i].Contains("A07") || B0[i].Contains("A08")) SOresPin4 = 2;
                        if (B0[i].Contains("A09") || B0[i].Contains("A10")) SOresPin5 = 2;
                        if (B0[i].Contains("A11") || B0[i].Contains("A12")) SOresPin6 = 2;

                        if (B0[i].Contains("A13") || B0[i].Contains("A14")) SOresPin7 = 2;
                        if (B0[i].Contains("A15") || B0[i].Contains("A16")) SOresPin8 = 2;
                        if (B0[i].Contains("A17") || B0[i].Contains("A18")) SOresPin9 = 2;
                        if (B0[i].Contains("A19") || B0[i].Contains("A20")) SOresPin10 = 2;
                        if (B0[i].Contains("A21") || B0[i].Contains("A22")) SOresPin11 = 2;
                        if (B0[i].Contains("A23") || B0[i].Contains("A24")) SOresPin12 = 2;
                    }
                    if (B0[i].Contains("SHORT"))//短路
                    {
                        if (B0[i].Contains("A01") || B0[i].Contains("A02")) SOresPin1 = 3;
                        if (B0[i].Contains("A03") || B0[i].Contains("A04")) SOresPin2 = 3;
                        if (B0[i].Contains("A05") || B0[i].Contains("A06")) SOresPin3 = 3;
                        if (B0[i].Contains("A07") || B0[i].Contains("A08")) SOresPin4 = 3;
                        if (B0[i].Contains("A09") || B0[i].Contains("A10")) SOresPin5 = 3;
                        if (B0[i].Contains("A11") || B0[i].Contains("A12")) SOresPin6 = 3;

                        if (B0[i].Contains("A13") || B0[i].Contains("A14")) SOresPin7 = 3;
                        if (B0[i].Contains("A15") || B0[i].Contains("A16")) SOresPin8 = 3;
                        if (B0[i].Contains("A17") || B0[i].Contains("A18")) SOresPin9 = 3;
                        if (B0[i].Contains("A19") || B0[i].Contains("A20")) SOresPin10 = 3;
                        if (B0[i].Contains("A21") || B0[i].Contains("A22")) SOresPin11 = 3;
                        if (B0[i].Contains("A23") || B0[i].Contains("A24")) SOresPin12 = 3;
                    }
                  //  string aa = B0[i];
                    /*  if (aa.Contains("XHIPOT"))
                      {
                          if (aa.Contains("A01"))
                          {
                              ResPin1 = false;
                              if (aa.Contains("LIMIT") || aa.Contains("ARCING")) VolvalPin1 = 999.9;
                              else VolvalPin1 = X * Convert.ToDouble(aa.Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());
                          }
                          if (aa.Contains("A03"))
                          {
                              ResPin2 = false;
                              if (aa.Contains("LIMIT") || aa.Contains("ARCING")) VolvalPin2 = 999.9;
                              else VolvalPin2 = X * Convert.ToDouble(aa.Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());
                          }
                          if (aa.Contains("A05"))
                          {
                              ResPin3 = false;
                              if (aa.Contains("LIMIT") || aa.Contains("ARCING")) VolvalPin3 = 999.9;
                              else VolvalPin3 = X * Convert.ToDouble(aa.Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());
                          }
                          if (aa.Contains("A07"))
                          {
                              ResPin4 = false;
                              if (aa.Contains("LIMIT") || aa.Contains("ARCING")) VolvalPin4 = 999.9;
                              else VolvalPin4 = X * Convert.ToDouble(aa.Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());
                          }
                          if (aa.Contains("A09"))
                          {
                              ResPin5 = false;
                              if (aa.Contains("LIMIT") || aa.Contains("ARCING")) VolvalPin5 = 999.9;
                              else VolvalPin5 = X * Convert.ToDouble(aa.Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());
                          }
                          if (aa.Contains("A11"))
                          {
                              ResPin6 = false;
                              if (aa.Contains("LIMIT") || aa.Contains("ARCING")) VolvalPin6 = 999.9;
                              else VolvalPin6 = X * Convert.ToDouble(aa.Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());
                          }

                      }
                      */

                }
                if (SOresPin1 > 1) ResPin1 = false;
                if (SOresPin2 > 1) ResPin2 = false;
                if (SOresPin3 > 1) ResPin3 = false;
                if (SOresPin4 > 1) ResPin4 = false;
                if (SOresPin5 > 1) ResPin5 = false;
                if (SOresPin6 > 1) ResPin6 = false;

                if (SOresPin7 > 1) ResPin7 = false;
                if (SOresPin8 > 1) ResPin8 = false;
                if (SOresPin9 > 1) ResPin9 = false;
                if (SOresPin10 > 1) ResPin10 = false;
                if (SOresPin11 > 1) ResPin11 = false;
                if (SOresPin12 > 1) ResPin12 = false;

                if (ResPin1 && ResPin2 && ResPin3 && ResPin4 && ResPin5 && ResPin6)
                    M_Product_L = true;
                else
                    M_Product_L = false;

                if (ResPin7 && ResPin8 && ResPin9 && ResPin10 && ResPin11 && ResPin12)
                    M_Product_R = true;
                else
                    M_Product_R = false;

                if (M_Product_L && M_Product_R)
                    ResEnd = true;
                return true;
            }catch(Exception ex)
            {
                Logger.Error($"N错误" + datas+ex.Message, 4);
                return false;
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="cords"></param>
        public void SaveToDB(string lr)
        {
            //检测该二维码之前做过没有
            string cords = Cord_data_R;
            string ModeNumber = ModeNumber_R;
            if (lr == "L")
            {
                cords = Cord_data_L;
                ModeNumber = ModeNumber_L;
            }
            else
            {
                cords = Cord_data_R;
                ModeNumber = ModeNumber_R;
            }

            if (cords.Length < 4)
            {
                Logger.Error("没有二维码无法写入数据库", 2);
                return;
            }

            if (cords == null || ModeNumber == null)
                return;

            string sql2 = $"SELECT count(*) from  VoltageData_8740 WHERE Cord = '{cords}'";
            int i2 = int.Parse(SQLHelper.ExecuteScalar(sql2));
            if (i2 > 0)
            {
                sql2 = $" delete FROM VoltageData_8740 WHERE Cord = '{cords}'";
                int i3 = SQLHelper.Update(sql2);
                if (i3 > 0)
                {
                    Logger.Info($"从耐压数据库删除{cords}的数据成功", 5);
                }
                else
                {
                    Logger.Error($"从耐压数据库删除{cords}的数据失败", 5);
                }
            }
            //存入数据库
            //      Cord_data = cords;
            string sql = "insert into VoltageData_8740( InsertTime,ProductType,ModeNumber,Cord,Result,ResultChenTao,ResultPLC,Shield," +
                "SOresPin1,SOresPin2,SOresPin3,SOresPin4,SOresPin5,SOresPin6," +
                "ResPin1,ResPin2,ResPin3,ResPin4,ResPin5,ResPin6," +
                "VolvalPin1,VolvalPin2,VolvalPin3,VolvalPin4,VolvalPin5,VolvalPin6)" +
                " values( @InsertTime,@ProductType,@ModeNumber,@Cord,@Result,@ResultChenTao,@ResultPLC,@Shield," +
                "@SOresPin1,@SOresPin2,@SOresPin3,@SOresPin4,@SOresPin5,@SOresPin6," +
                "@ResPin1,@ResPin2,@ResPin3,@ResPin4,@ResPin5,@ResPin6," +
                "@VolvalPin1,@VolvalPin2,@VolvalPin3,@VolvalPin4,@VolvalPin5,@VolvalPin6)";
            int i = 0;
            if (lr == "L")
            {
                SqlParameter[] param = new SqlParameter[]
                {
                   new SqlParameter("@InsertTime", DateTime.Now),//时间
                   new SqlParameter("@ProductType", SysStatus.CurProductName),//设备名称

                   new SqlParameter("@ModeNumber", ModeNumber),//磨具号
                   new SqlParameter("@Cord", cords),//二维码
                   new SqlParameter("@Result", M_Product_L),//测试结果
                   new SqlParameter("@ResultChenTao", ResChenTao),//衬套
                   new SqlParameter("@ResultPLC", ResPLC),
                   new SqlParameter("@Shield", Shield),

                   new SqlParameter("@SOresPin1",  SOresPin1 ),//开路
                   new SqlParameter("@SOresPin2",  SOresPin2 ),
                   new SqlParameter("@SOresPin3",  SOresPin3 ),
                   new SqlParameter("@SOresPin4",  SOresPin4 ),
                   new SqlParameter("@SOresPin5",  SOresPin5 ),
                   new SqlParameter("@SOresPin6",  SOresPin6 ),
                   new SqlParameter("@ResPin1",  ResPin1 ),//结果
                   new SqlParameter("@ResPin2",  ResPin2 ),
                   new SqlParameter("@ResPin3",  ResPin3 ),
                   new SqlParameter("@ResPin4",  ResPin4 ),
                   new SqlParameter("@ResPin5",  ResPin5 ),
                   new SqlParameter("@ResPin6",  ResPin6 ),

                   new SqlParameter("@VolvalPin1", VolvalPin1),//电流
                   new SqlParameter("@VolvalPin2", VolvalPin2),
                   new SqlParameter("@VolvalPin3", VolvalPin3),
                   new SqlParameter("@VolvalPin4", VolvalPin4),
                   new SqlParameter("@VolvalPin5", VolvalPin5),
                   new SqlParameter("@VolvalPin6", VolvalPin6),
                };
                i = SQLHelper.Update(sql, param);

                if (i > 0)                 
                    Logger.Info("左耐压数据写入耐压数据库成功", 2);                
                else                 
                    Logger.Error("左耐压数据写入耐压数据库失败", 2);
                
            }
            else
            {
                
                    SqlParameter[] param = new SqlParameter[]
                    {
                   new SqlParameter("@InsertTime", DateTime.Now),//时间
                   new SqlParameter("@ProductType", SysStatus.CurProductName),//设备名称

                   new SqlParameter("@ModeNumber", ModeNumber),//磨具号
                   new SqlParameter("@Cord", cords),//二维码
                   new SqlParameter("@Result", M_Product_R),//测试结果
                   new SqlParameter("@ResultChenTao", ResChenTao),//衬套
                   new SqlParameter("@ResultPLC", ResPLC),
                   new SqlParameter("@Shield", Shield),

                   new SqlParameter("@SOresPin1",  SOresPin7 ),//开路
                   new SqlParameter("@SOresPin2",  SOresPin8 ),
                   new SqlParameter("@SOresPin3",  SOresPin9 ),
                   new SqlParameter("@SOresPin4",  SOresPin10 ),
                   new SqlParameter("@SOresPin5",  SOresPin11 ),
                   new SqlParameter("@SOresPin6",  SOresPin12 ),
                   new SqlParameter("@ResPin1",  ResPin7 ),//结果
                   new SqlParameter("@ResPin2",  ResPin8 ),
                   new SqlParameter("@ResPin3",  ResPin9 ),
                   new SqlParameter("@ResPin4",  ResPin10 ),
                   new SqlParameter("@ResPin5",  ResPin11 ),
                   new SqlParameter("@ResPin6",  ResPin12 ),

                   new SqlParameter("@VolvalPin1", VolvalPin7),//电流
                   new SqlParameter("@VolvalPin2", VolvalPin8),
                   new SqlParameter("@VolvalPin3", VolvalPin9),
                   new SqlParameter("@VolvalPin4", VolvalPin10),
                   new SqlParameter("@VolvalPin5", VolvalPin11),
                   new SqlParameter("@VolvalPin6", VolvalPin12),
                    };
                    i = SQLHelper.Update(sql, param);

                    if (i > 0)
                    
                        Logger.Info("右耐压数据写入耐压数据库成功", 2);
                    
                    else
                    
                        Logger.Error("右耐压数据写入耐压数据库失败", 2);
                    
                
            }
         
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="CORD"></param>
        public void LoadFromDB(string CORD)
        {
            init();
            try
            {
                string sql = $" select * from VoltageData_8740 where Cord = '{ CORD}'";
                SqlDataReader reader = SQLHelper.GetReader(sql);
                while (reader.Read())
                {
                    ResEnd = (bool)(reader["Result"]);
                    ResChenTao = (bool)(reader["ResultChenTao"]);
                    ResPLC = (bool)(reader["ResultPLC"]);
                    Shield = (bool)reader["Shield"];
                    SOresPin1 = (int)reader["SOresPin1"];
                    SOresPin2 = (int)reader["SOresPin2"];
                    SOresPin3 = (int)reader["SOresPin3"];
                    SOresPin4 = (int)reader["SOresPin4"];
                    SOresPin5 = (int)reader["SOresPin5"];
                    SOresPin6 = (int)reader["SOresPin6"];

                    ResPin1 = (bool)(reader["ResPin1"]);
                    ResPin2 = (bool)(reader["ResPin2"]);
                    ResPin3 = (bool)(reader["ResPin3"]);
                    ResPin4 = (bool)(reader["ResPin4"]);
                    ResPin5 = (bool)(reader["ResPin5"]);
                    ResPin6 = (bool)(reader["ResPin6"]);

                    VolvalPin1 = (double)(reader["VolvalPin1"]);
                    VolvalPin2 = (double)(reader["VolvalPin2"]);
                    VolvalPin3 = (double)(reader["VolvalPin3"]);
                    VolvalPin4 = (double)(reader["VolvalPin4"]);
                    VolvalPin5 = (double)(reader["VolvalPin3"]);
                    VolvalPin6 = (double)(reader["VolvalPin4"]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                Logger.Error($"读取耐压数据库出错{ex.Message}", 2);
            }
        }
        /// <summary>
        /// 通讯的串口
        /// </summary>
        public SerialPort serialPort = new SerialPort();
        /// <summary>
        /// 串口号
        /// </summary>
        public string MyPortName;
        /// <summary>
        /// 波特率
        /// </summary>
        public int MyBaudRate;
        /// <summary>
        /// 获取string类型数据
        /// </summary>
        public string ReceiveData8740 = "";
        /// <summary>
        /// 获取的string值
        /// </summary>
        public string ReceiveStringData = "";
        /// <summary>
        /// 是否连接成功
        /// </summary>
        public bool IsConnected = false;
        /// <summary>
        /// 打标机的描述
        /// </summary>
        public string mdescrible;
        /// <summary>
        /// 构造刻字机
        /// </summary>
        /// <param name="mPortName">串口号</param>
        /// <param name="mBaudRate">波特率</param>
        /// <param name="mdes">描述</param>
        public My_8740()
        {

        }
        /// <summary>
        /// 连接com 或重新连接COM
        /// </summary>
        /// <returns></returns>
        public EmRes connect()
        {

            //如果打开先关闭
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
            serialPort = new SerialPort();
            //设置串口属性
            serialPort.PortName = "COM1";
            serialPort.BaudRate = 115200;

            //设置串口事件
            serialPort.ReceivedBytesThreshold = 1;//数据触发事件
            serialPort.DataReceived += SerialPort_DataReceived;

            try
            {
                serialPort.Open();
                Logger.Info($"{mdescrible}串口打开成功");
                IsConnected = true;
                ssa = new PaperC();
            }
            catch (Exception ex)
            {
                Logger.Error($"{mdescrible}串口打开失败:{ex.Message}");
                IsConnected = false;
                return EmRes.Error;
            }

            return EmRes.Succeed;

        }

        /// <summary>
        /// 串口关闭
        /// </summary>
        public void DisConnet()
        {
            if (serialPort != null)
            {
                serialPort.Close();
            }
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(150);
            //获取缓冲区的长度
            int ByteToRead = serialPort.BytesToRead;
            if (ByteToRead > 1)
            {
                try
                {
                    //定义一个数组
                    byte[] rec = new byte[ByteToRead];
                    //读取缓冲区的数据放到字节数组中
                    this.serialPort.Read(rec, 0, ByteToRead);
                    // ReceiveData = Encoding.ASCII.GetString(rec);
                    ReceiveData8740 = StringLib.GetStringFromByteArray(rec, 0, rec.Length);
                    ReceiveStringData = ReceiveData8740;
                    ssa.Name = ReceiveData8740;
                    chuan?.Invoke(this, ssa);
                }
                catch (Exception ex)
                {
                    Logger.Error($"{mdescrible}串口接收信息失败：{ex.Message}");
                    return;
                }
            }
        }
        /// <summary>
        /// 发送数据，*OPT?是否就绪
        /// </summary>
        /// <param name="mess">:STAR开始，:STOP停止，:RESU?请求结果，*OPC?询问完成没有？</param>
        /// <returns></returns>
        public EmRes sends(string mess)
        {
            if (IsConnected)
            {
                if (!serialPort.IsOpen)//如果断开连接就在连接一次
                {
                    serialPort.Open();
                }
                try
                {
                    ReceiveData8740 = "";
                    ReceiveStringData = "";
                    serialPort.Write(mess);
                    return EmRes.Succeed;
                }
                catch (Exception ex)
                {
                    Logger.Error($"{mdescrible}串口发送信息失败：{ex.Message}");
                    return EmRes.Error;
                }
            }
            else
            {
                Logger.Error($"{mdescrible}串口没有开打");
                return EmRes.Error;
            }
        }
        /// <summary>
        /// 开机点检
        /// </summary>
        /// <returns></returns>
        public EmRes Sendinit()
        {

            string type8740 = "*IDN?\r\n";
            string Exit = ":KEY EXIT\r\n";
            if (sends(Exit) != EmRes.Succeed) return EmRes.Error;
            Thread.Sleep(50);
            if (sends(type8740) != EmRes.Succeed) return EmRes.Error;
            Thread.Sleep(50);
            int a = 0;
            while (ReceiveData8740.Length < 1)
            {
                Thread.Sleep(50);
                a++;
                if (ReceiveData8740.Length > 1) break;
                if (a > 50) return EmRes.Error;
            }
            if (ReceiveData8740.Contains("8740"))
            {
                return EmRes.Succeed;
            }
            else return EmRes.Error;

        }
        /// <summary>
        /// 启动测量
        /// </summary>
        /// <param name="resdata"></param>
        /// <returns></returns>
        public EmRes SendStart(out string resdata)
        {
            string Exit = ":KEY EXIT\r\n";//退出
            string Test = ":KEY TEST\r\n";//测试
            resdata = "";
            sends(Exit);
            Thread.Sleep(50);
            sends(Test);
            Thread.Sleep(50);

            int a = 0;
            while (ReceiveData8740.Length < 1)
            {
                Thread.Sleep(100);
                a++;
                if (ReceiveData8740.Length > 1) break;
                if (a > 150) return EmRes.Error;
            }
            resdata = ReceiveData8740;
            return EmRes.Succeed;
        }
        /// <summary>
        /// 设置二维码及模号
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        public void SetData(string c1, string c2, int m1, int m2)
        {
            Cord_data_L = c1;
            ModeNumber_L = m1.ToString();
            Cord_data_R = c2;
            ModeNumber_R = m2.ToString();
        }
        /// <summary>
        /// 切换文件
        /// </summary>
        public void ChanageFile(string str)
        {
            sends(":KEY EXIT\r\n");
            Thread.Sleep(100);
            sends(":KEY FILE\r\n");
            Thread.Sleep(100);

            sends(":KEY " + str + "\r\n");
            Thread.Sleep(100);
            sends(":KEY RIGHT\r\n");
            Thread.Sleep(100);
            // sends(":KEY 4\r\n");
            // Thread.Sleep(100);
            // sends(":KEY RIGHT\r\n");
            //  Thread.Sleep(100);
            // sends(":KEY 6\r\n");
            // Thread.Sleep(100);
            sends(":KEY ENTER\r\n");
            Thread.Sleep(100);
            sends(":KEY ENTER\r\n");
        }

        #region 向外传递信息
        public event EventHandler<PaperC> chuan;
        PaperC ssa;
        public class PaperC : EventArgs
        {
            public string Name { get; set; }                    //用于存储数据，当事件被调用时，可利用其进行传递数据。
        }
        #endregion



    }
}
