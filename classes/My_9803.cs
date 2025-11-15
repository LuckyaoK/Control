using MyLib.SqlHelper;
using MyLib.Sys;
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
    public class My_9803
    {

    
        public string Cord_data, ModeNumber;
        /// <summary>
        /// 各pin通断结果
        /// </summary> 
        public bool SOresPin1, SOresPin2, SOresPin3, SOresPin4, SOresPin5, SOresPin6, SOresPin7, SOresPin8, SOresPin9;

        /// <summary>
        /// 各pin的短断电阻值--一般不用
        /// </summary>
        public double SOvalPin1, SOvalPin2, SOvalPin3, SOvalPin4, SOvalPin5, SOvalPin6, SOvalPin7, SOvalPin8, SOvalPin9;
        /// <summary>
        /// 各pin耐压电流值--8740
        /// </summary>
        public double VolvalPin1, VolvalPin2, VolvalPin3, VolvalPin4, VolvalPin5, VolvalPin6, VolvalPin7, VolvalPin8, VolvalPin9;

        /// <summary>
        /// 各pin的测试结果--8740
        /// </summary>
        public bool ResPin1, ResPin2, ResPin3, ResPin4, ResPin5, ResPin6, ResPin7, ResPin8, ResPin9;

        /// <summary>
        /// 最终结果--8740
        /// </summary>
        public bool ResEnd;
        public bool ResPLC;
        public bool ResChenTao;
        public bool Shield;

        public void init(bool pingbi = false)
        {
            Cord_data = "";
            ResChenTao = ResPLC = Shield = ResEnd = SOresPin1 = SOresPin2 = SOresPin3 = SOresPin4 = SOresPin5 = SOresPin6 = SOresPin7 = SOresPin8 = SOresPin9 = pingbi;
            ResPin1 = ResPin2 = ResPin3 = ResPin4 = ResPin5 = ResPin6 = ResPin7 = ResPin8 = ResPin9 = pingbi;
            SOvalPin1 = SOvalPin2 = SOvalPin3 = SOvalPin4 = SOvalPin5 = SOvalPin6 = SOvalPin7 = SOvalPin8 = SOvalPin9 = 999.9;
            VolvalPin1 = VolvalPin2 = VolvalPin3 = VolvalPin4 = VolvalPin5 = VolvalPin6 = VolvalPin7 = VolvalPin8 = VolvalPin9 = 999.9;
        }

        public void AnalyseData(string datas, string mohao)
        {

            try
            {
                ModeNumber = mohao;
                datas = datas.Replace("\0", "");
                List<string> B0 = new List<string>();
                List<string> B1 = new List<string>();//电流数据
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
                double X = 1.0;
                if (B0.Count < 3) return;
                if (datas.Contains("uA")) X = 1;
                if (B0[2] == " O/S TEST ................ PASS" && !datas.Contains("XHIPOT") && !datas.Contains("ARCING"))//短断OK，数据OK
                {
                    string pinres = "";
                    ResEnd = true;
                    if (B0[3].Contains("uA")) ups = 0.3;
                    B1.Add(B0[3].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());
                    B1.Add(B0[4].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());
                    B1.Add(B0[5].Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());
                    for (int i = 0; i < B1.Count; i++)
                    {

                        if (Convert.ToDouble(B1[i]) >= ups)
                        {
                            ResEnd = false;
                            pinres += (i + 1).ToString() + ",";
                        }
                    }
                    SOresPin1 = SOresPin2 = SOresPin3 = SOresPin4 = SOresPin5 = SOresPin6 = SOresPin7 = SOresPin8 = SOresPin9 = true;
                    ResPin1 = ResPin2 = ResPin3 = ResPin4 = ResPin5 = ResPin6 = ResPin7 = ResPin8 = ResPin9 = true;
                    if (B1.Count >= 1) VolvalPin1 = Convert.ToDouble(B1[0]) * X;
                    if (B1.Count >= 2) VolvalPin2 = Convert.ToDouble(B1[1]) * X;
                    if (B1.Count >= 3) VolvalPin3 = Convert.ToDouble(B1[2]) * X;
                    if (B1.Count >= 4) VolvalPin4 = Convert.ToDouble(B1[3]) * X;
                    if (B1.Count >= 5) VolvalPin5 = Convert.ToDouble(B1[4]) * X;
                    if (B1.Count >= 6) VolvalPin6 = Convert.ToDouble(B1[5]) * X;
                    if (B1.Count >= 7) VolvalPin7 = Convert.ToDouble(B1[6]) * X;
                    if (B1.Count >= 8) VolvalPin8 = Convert.ToDouble(B1[7]) * X;
                    if (B1.Count >= 9) VolvalPin9 = Convert.ToDouble(B1[8]) * X;
                    if (pinres.Contains("1")) ResPin1 = false;
                    if (pinres.Contains("2")) ResPin2 = false;
                    if (pinres.Contains("3")) ResPin3 = false;
                    if (pinres.Contains("4")) ResPin4 = false;
                    if (pinres.Contains("5")) ResPin5 = false;
                    if (pinres.Contains("6")) ResPin6 = false;
                    if (pinres.Contains("7")) ResPin7 = false;
                    if (pinres.Contains("8")) ResPin8 = false;
                    //VolvalPin4 = 0.0;


                }
                else
                {
                    ResEnd = false;
                    SOresPin1 = SOresPin2 = SOresPin3 = SOresPin4 = SOresPin5 = SOresPin6 = SOresPin7 = SOresPin8 = SOresPin9 = true;
                    ResPin1 = ResPin2 = ResPin3 = ResPin4 = ResPin5 = ResPin6 = ResPin7 = ResPin8 = ResPin9 = true;
                    for (int i = 0; i < B0.Count; i++)//找出断路 短路的脚
                    {
                        if (B0[i].Contains("OPEN") || B0[i].Contains("SHORT"))
                        {
                            if (B0[i].Contains("A01") || B0[i].Contains("A02")) SOresPin1 = false;
                            if (B0[i].Contains("A03") || B0[i].Contains("A04")) SOresPin2 = false;
                            if (B0[i].Contains("A05") || B0[i].Contains("A06")) SOresPin3 = false;
                            if (B0[i].Contains("A07") || B0[i].Contains("A08")) SOresPin4 = false;
                            if (B0[i].Contains("A09") || B0[i].Contains("A10")) SOresPin5 = false;
                            if (B0[i].Contains("A11") || B0[i].Contains("A12")) SOresPin6 = false;
                            //if (B0[i].Contains("A13") || B0[i].Contains("A14")) SOresPin7 = false;
                            //if (B0[i].Contains("A15") || B0[i].Contains("A16")) SOresPin8 = false;
                            //if (B0[i].Contains("A17") || B0[i].Contains("A18")) SOresPin9 = false;
                        }
                        string aa = B0[i];
                        if (aa.Contains("XHIPOT"))
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
                        if (aa.Contains(" HIPOT"))
                        {
                            if (aa.Contains("A01"))
                                VolvalPin1 = X * Convert.ToDouble(aa.Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());
                            if (aa.Contains("A03"))
                                VolvalPin2 = X * Convert.ToDouble(aa.Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());
                            if (aa.Contains("A05"))
                                VolvalPin3 = X * Convert.ToDouble(aa.Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());
                            if (aa.Contains("A07"))
                                VolvalPin4 = X * Convert.ToDouble(aa.Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());
                            if (aa.Contains("A09"))
                                VolvalPin5 = X * Convert.ToDouble(aa.Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());
                            if (aa.Contains("A11"))
                                VolvalPin6 = X * Convert.ToDouble(aa.Substring(13, 8).Replace("uA", "").Replace("mA", "").Trim());

                        }

                    }
                    if (!SOresPin1) ResPin1 = false;
                    if (!SOresPin2) ResPin2 = false;
                    if (!SOresPin3) ResPin3 = false;
                    if (!SOresPin4) ResPin4 = false;
                    if (!SOresPin5) ResPin5 = false;
                    if (!SOresPin6) ResPin6 = false;
                    if (!SOresPin7) ResPin7 = false;
                    if (!SOresPin8) ResPin8 = false;
                    if (!SOresPin9) ResPin9 = false;
                }

            }
            catch (Exception ex)
            {
                ResEnd = false;
                Logger.Error($"解析耐压数据出错{datas}{ex.Message}");
            }
            if (!ResChenTao || !ResPLC) ResEnd = false;
        }


        public void SaveToDB(string cords)
        {
            //检测该二维码之前做过没有
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
            Cord_data = cords;
            string sql = "insert into VoltageData_8740( InsertTime,ProductType,ModeNumber,Cord,Result,ResultChenTao,ResultPLC,Shield," +
                "SOresPin1,SOresPin2,SOresPin3,SOresPin4," +
                "ResPin1,ResPin2,ResPin3,ResPin4," +
                "VolvalPin1,VolvalPin2,VolvalPin3,VolvalPin4)" +
                " values( @InsertTime,@ProductType,@ModeNumber,@Cord,@Result,@ResultChenTao,@ResultPLC,@Shield," +
                "@SOresPin1,@SOresPin2,@SOresPin3,@SOresPin4," +
                "@ResPin1,@ResPin2,@ResPin3,@ResPin4," +
                "@VolvalPin1,@VolvalPin2,@VolvalPin3,@VolvalPin4)";
            SqlParameter[] param = new SqlParameter[]
            {
                   new SqlParameter("@InsertTime", DateTime.Now),
                   new SqlParameter("@ProductType", SysStatus.CurProductName),
                   new SqlParameter("@ModeNumber", ModeNumber),
                   new SqlParameter("@Cord", Cord_data),
                   new SqlParameter("@Result", ResEnd),
                   new SqlParameter("@ResultChenTao", ResChenTao),
                   new SqlParameter("@ResultPLC", ResPLC),
                   new SqlParameter("@Shield", Shield),

                   new SqlParameter("@SOresPin1",  SOresPin1 ),
                   new SqlParameter("@SOresPin2",  SOresPin2 ),
                   new SqlParameter("@SOresPin3",  SOresPin3 ),
                   new SqlParameter("@SOresPin4",  SOresPin4 ),
                   new SqlParameter("@ResPin1",  ResPin1 ),
                   new SqlParameter("@ResPin2",  ResPin2 ),
                   new SqlParameter("@ResPin3",  ResPin3 ),
                   new SqlParameter("@ResPin4",  ResPin4 ),

                   new SqlParameter("@VolvalPin1", VolvalPin1),
                   new SqlParameter("@VolvalPin2", VolvalPin2),
                   new SqlParameter("@VolvalPin3", VolvalPin3),
                   new SqlParameter("@VolvalPin4", VolvalPin4),

            };
            int i = SQLHelper.Update(sql, param);
            if (i > 0)
            {
                Logger.Info("耐压数据写入耐压数据库成功", 2);
            }
            else
            {
                Logger.Error("耐压数据写入耐压数据库失败", 2);
            }
        }
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
                    SOresPin1 = (bool)reader["SOresPin1"];
                    SOresPin2 = (bool)reader["SOresPin2"];
                    SOresPin3 = (bool)reader["SOresPin3"];
                    SOresPin4 = (bool)reader["SOresPin4"];

                    ResPin1 = (bool)(reader["ResPin1"]);
                    ResPin2 = (bool)(reader["ResPin2"]);
                    ResPin3 = (bool)(reader["ResPin3"]);
                    ResPin4 = (bool)(reader["ResPin4"]);

                    VolvalPin1 = (double)(reader["VolvalPin1"]);
                    VolvalPin2 = (double)(reader["VolvalPin2"]);
                    VolvalPin3 = (double)(reader["VolvalPin3"]);
                    VolvalPin4 = (double)(reader["VolvalPin4"]);

                }
                reader.Close();
            }
            catch (Exception ex)
            {

                Logger.Error($"读取耐压数据库出错{ex.Message}", 2);
            }
        }


        #region 9803耐压仪
        /// <summary>
        /// 模号
        /// </summary>
        public string TestMode;
        public string Cords;
        #region 绝缘测试
        /// <summary>
        /// 绝缘测试结果
        /// </summary>
        public bool InsuRes, InsuRes1, InsuRes2;

        /// <summary>
        /// 绝缘测试时的电压
        /// </summary>
        public string InsuVol, InsuVol1, InsuVol2;

        /// <summary>
        /// 绝缘测试返回的值 电阻ohm
        /// </summary>
        public string InsuValue, InsuValue1, InsuValue2;

        /// <summary>
        /// 绝缘测试时间
        /// </summary>
        public string InsuTime, InsuTime1, InsuTime2;
        #endregion
        #region 耐压测试
        /// <summary>
        /// 耐压测试结果
        /// </summary>
        public bool VoltageRes, VoltageRes1, VoltageRes2;
        /// <summary>
        /// 耐压测试时的电压
        /// </summary>
        public string VoltageVol, VoltageVol1, VoltageVol2;
        /// <summary>
        /// 耐压测试返回的值 电流mA
        /// </summary>
        public string VoltageValue, VoltageValue1, VoltageValue2;
        /// <summary>
        /// 耐压测试时间
        /// </summary>
        public string VoltageTime, VoltageTime1, VoltageTime2;
        #endregion
        /// <summary>
        /// 9803测试最终结果
        /// </summary>
        public bool ResultAll { get { return InsuRes && InsuRes1 && InsuRes2 && VoltageRes && VoltageRes1 && VoltageRes2 ? true : false; } }

        public void init_9803()
        {
            InsuValue = InsuValue1 = InsuValue2 = VoltageValue = VoltageValue1 = VoltageValue2 = "NA";
        }
        public void AnalyseData_9803(string resdata, int step, bool resNG = false)
        {
            //IR ,PASS ,0.050kV,9999M ohm,T=001.0S //ACW,PASS ,0.099kV,0.002 mA ,T=001.0S
            string[] dataar = resdata.Split(',');
            if (step == 1) //123绝缘456耐压
            {
                if (dataar.Length == 5 && dataar[0] == "IR")
                {
                    InsuVol = dataar[2]; InsuValue = dataar[3]; InsuTime = dataar[3].Substring(2, 6);
                    if (dataar[1].Contains("PASS") && Convert.ToDouble(InsuValue.Substring(0, 4)) > 20)
                    {
                        InsuRes = true;
                    }
                    else InsuRes = false;
                }
                else
                {
                    InsuVol = InsuValue = InsuTime = "NA";
                    InsuRes = false;
                }
                Logger.Info($"绝缘测试1数据分析完成{InsuRes.ToString()}:{resdata}", 3);
            }
            else if (step == 2)
            {
                if (dataar.Length == 5 && dataar[0] == "IR")
                {
                    InsuVol1 = dataar[2]; InsuValue1 = dataar[3]; InsuTime1 = dataar[3].Substring(2, 6);
                    if (dataar[1].Contains("PASS") && Convert.ToDouble(InsuValue1.Substring(0, 4)) > 20)
                    {
                        InsuRes1 = true;
                    }
                    else InsuRes1 = false;
                }
                else
                {
                    InsuVol1 = InsuValue1 = InsuTime1 = "NA";
                    InsuRes1 = false;
                }
                Logger.Info($"绝缘测试2数据分析完成{InsuRes1.ToString()}:{resdata}", 3);
            }
            else if (step == 3)
            {
                if (dataar.Length == 5 && dataar[0] == "IR")
                {
                    InsuVol2 = dataar[2]; InsuValue2 = dataar[3]; InsuTime2 = dataar[3].Substring(2, 6);
                    if (dataar[1].Contains("PASS") && Convert.ToDouble(InsuValue2.Substring(0, 4)) > 20)
                    {
                        InsuRes2 = true;
                    }
                    else InsuRes2 = false;
                }
                else
                {
                    InsuVol2 = InsuValue2 = InsuTime2 = "NA";
                    InsuRes2 = false;
                }
                Logger.Info($"绝缘测试3数据分析完成{InsuRes2.ToString()}:{resdata}", 3);
            }
            else if (step == 4)//123绝缘456耐压
            {
                if (dataar.Length == 5 && dataar[0] == "ACW")
                {
                    VoltageVol = dataar[2]; VoltageValue = dataar[3]; VoltageTime = dataar[3].Substring(2, 6);
                    if (dataar[1].Contains("PASS") && Convert.ToDouble(dataar[3].Substring(0, 4)) > 20)
                    {
                        VoltageRes = true;
                    }
                    else VoltageRes = false;
                }
                else
                {
                    VoltageVol = VoltageValue = VoltageTime = "NA";
                    VoltageRes = false;
                }
                Logger.Info($"耐压测试1数据分析完成{VoltageRes.ToString()}:{resdata}", 3);
            }
            else if (step == 5)//123绝缘456耐压
            {
                if (dataar.Length == 5 && dataar[0] == "ACW")
                {
                    VoltageVol1 = dataar[2]; VoltageValue1 = dataar[3]; VoltageTime1 = dataar[3].Substring(2, 6);
                    if (dataar[1].Contains("PASS") && Convert.ToDouble(dataar[3].Substring(0, 4)) > 20)
                    {
                        VoltageRes1 = true;
                    }
                    else VoltageRes1 = false;
                }
                else
                {
                    VoltageVol1 = VoltageValue1 = VoltageTime1 = "NA";
                    VoltageRes1 = false;
                }
                Logger.Info($"耐压测试2数据分析完成{VoltageRes1.ToString()}:{resdata}", 3);
            }
            else if (step == 6)//123绝缘456耐压
            {
                if (dataar.Length == 5 && dataar[0] == "ACW")
                {
                    VoltageVol2 = dataar[2]; VoltageValue2 = dataar[3]; VoltageTime2 = dataar[3].Substring(2, 6);
                    if (dataar[1].Contains("PASS") && Convert.ToDouble(dataar[3].Substring(0, 4)) > 20)
                    {
                        VoltageRes2 = true;
                    }
                    else VoltageRes2 = false;
                }
                else
                {
                    VoltageVol2 = VoltageValue2 = VoltageTime2 = "NA";
                    VoltageRes2 = false;
                }
                Logger.Info($"耐压测试3数据分析完成{VoltageRes2.ToString()}:{resdata}", 3);
            }

        }
        /// <summary>
        /// 存入数据库
        /// </summary>
        /// <param name="cords">二维码</param>
        public void SaveToDB_9803(string cords)
        {
            string sql = "insert into VoltageData_9803( InsertTime,Cord,Result,ProductType,InsuVol,InsuValue,InsuTime,InsuRes," +
                "InsuVol1,InsuValue1,InsuTime1,InsuRes1,InsuVol2,InsuValue2,InsuTime2,InsuRes2" +
                ",VoltageVol,VoltageValue,VoltageTime,VoltageRes,VoltageVol1,VoltageValue1,VoltageTime1,VoltageRes1" +
                ",VoltageVol2,VoltageValue2,VoltageTime2,VoltageRes2)" +
                " values( @InsertTime,@Cord,@Result,@ProductType,@InsuVol,@InsuValue,@InsuTime,@InsuRes" +
                ",@InsuVol1,@InsuValue1,@InsuTime1,@InsuRes1" +
                ",@InsuVol2,@InsuValue2,@InsuTime2,@InsuRes2" +
                ",@VoltageVol,@VoltageValue,@VoltageTime,@VoltageRes" +
                ",@VoltageVol1,@VoltageValue1,@VoltageTime1,@VoltageRes1" +
                ",@VoltageVol2,@VoltageValue2,@VoltageTime2,@VoltageRes2)";
            string res1 = ResultAll == true ? "OK" : "NG";
            SqlParameter[] param = new SqlParameter[]
            {
                   new SqlParameter("@InsertTime", DateTime.Now),
                   new SqlParameter("@Cord", cords),
                   new SqlParameter("@Result", res1),
                   new SqlParameter("@ProductType", SysStatus.CurProductName),
                   new SqlParameter("@InsuVol",  InsuVol ),
                   new SqlParameter("@InsuValue",  InsuValue ),
                   new SqlParameter("@InsuTime",  InsuTime ),
                   new SqlParameter("@InsuRes", InsuRes ),
                   new SqlParameter("@InsuVol1",  InsuVol1 ),
                   new SqlParameter("@InsuValue1",  InsuValue1 ),
                   new SqlParameter("@InsuTime1",  InsuTime1 ),
                   new SqlParameter("@InsuRes1", InsuRes1 ),
                   new SqlParameter("@InsuVol2",  InsuVol2 ),
                   new SqlParameter("@InsuValue2",  InsuValue2 ),
                   new SqlParameter("@InsuTime2",  InsuTime2 ),
                   new SqlParameter("@InsuRes2", InsuRes2 ),
                   new SqlParameter("@VoltageVol", VoltageVol ),
                   new SqlParameter("@VoltageValue", VoltageValue ),
                   new SqlParameter("@VoltageTime",  VoltageTime ),
                   new SqlParameter("@VoltageRes", VoltageRes ),
                   new SqlParameter("@VoltageVol1", VoltageVol1 ),
                   new SqlParameter("@VoltageValue1", VoltageValue1 ),
                   new SqlParameter("@VoltageTime1",  VoltageTime1 ),
                   new SqlParameter("@VoltageRes1", VoltageRes1 ),
                   new SqlParameter("@VoltageVol2", VoltageVol2 ),
                   new SqlParameter("@VoltageValue2", VoltageValue2 ),
                   new SqlParameter("@VoltageTime2",  VoltageTime2 ),
                   new SqlParameter("@VoltageRes2", VoltageRes2 ),

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
        public void LoadFromDB_9803(string CORD)
        {
            string sql = $" select * from VoltageData_9803 where Cord = '{ CORD}'";
            SqlDataReader reader = SQLHelper.GetReader(sql);
            while (reader.Read())
            {
                InsuRes = (bool)(reader["InsuRes"]);
                InsuRes1 = (bool)(reader["InsuRes1"]);
                InsuRes2 = (bool)(reader["InsuRes2"]);
                InsuValue = (reader["InsuValue"]).ToString();
                InsuValue1 = (reader["InsuValue1"]).ToString();
                InsuValue2 = (reader["InsuValue2"]).ToString();
                VoltageRes = (bool)(reader["VoltageRes"]);
                VoltageRes1 = (bool)(reader["VoltageRes1"]);
                VoltageRes2 = (bool)(reader["VoltageRes2"]);
                VoltageValue = (reader["VoltageValue"]).ToString();
                VoltageValue1 = (reader["VoltageValue1"]).ToString();
                VoltageValue2 = (reader["VoltageValue2"]).ToString();
            }
            reader.Close();

        }

        #endregion
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
        public My_9803()
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
            Thread.Sleep(100);
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
            while (ReceiveData8740.Length<1)
            {
                Thread.Sleep(50);
                a++;
                if (ReceiveData8740.Length > 1) break;
                if(a>50) return EmRes.Error;
            }
            if(ReceiveData8740.Contains("8740"))
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
        /// 切换文件
        /// </summary>
        public void ChanageFile()
        {
            sends(":KEY EXIT\r\n");
            Thread.Sleep(100);
            sends(":KEY FILE\r\n");
            Thread.Sleep(100);
            sends(":KEY 4\r\n");
            Thread.Sleep(100);
            sends(":KEY RIGHT\r\n");
            Thread.Sleep(100);
            sends(":KEY 4\r\n");
            Thread.Sleep(100);
            sends(":KEY RIGHT\r\n");
            Thread.Sleep(100);
            sends(":KEY 6\r\n");
            Thread.Sleep(100);
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
