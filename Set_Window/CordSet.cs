using CXPro001.myclass;
using MyLib.Files;
using MyLib.SqlHelper;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CXPro001.setups
{
    /// <summary>
    /// 二维码编码类
    /// </summary>
    public class CordSet
    {
        private string filename = "";// 配置文件路径
        /// <summary>
        /// 刻的二维码
        /// </summary>
        public string CurCord;
        /// <summary>
        /// 扫出来的二维码
        /// </summary>
        public string SaoCord;
        /// <summary>
        /// 扫打扫结果
        /// </summary>
        public bool ResEnd;
        /// <summary>
        /// 扫打扫结果信息
        /// </summary>
        public string CurResMess;
        public bool Sheild;
        public void init(bool pingbi = false)
        {           
            CurResMess = CurCord = SaoCord= "";
            Sheild = ResEnd = pingbi;
        }
        public void AnalyseData(string str1, string str2, string str3)
        {
            CurCord = str1.Trim('\0').Replace(" ","").Replace("\r","");
            SaoCord = str2.Trim('\0').Replace(" ", "").Replace("\r", "");
            ResEnd = str3 == "1";
            Logger.Info($"扫打扫字符识别结果解析完成:{str1};{str2};{str3}");
        }
        /// <summary>
        /// 二维码长度
        /// </summary>
        public int CordLens=0;
        /// <summary>
        /// 扫码等级
        /// </summary>
        public string CordLevel="";
        /// <summary>
        /// 范例
        /// </summary>
        public string BarCord = "";
        /// <summary>
        /// 递增的序列化
        /// </summary>
        public int CordNO { get; set; } = 1;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName {get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime GreateTime = new DateTime();
        /// <summary>
        /// 一次打码个数
        /// </summary>
        public int CordCount=1;
        /// <summary>
        /// 产品描述
        /// </summary>
        public string desr;
        #region 规则
        /// <summary>
        /// 条码规则1
        /// </summary>
        public string Rule1 { get; set; }
        /// <summary>
        /// 条码规则2
        /// </summary>
        public string Rule2 { get; set; }
        /// <summary>
        /// 条码规则3
        /// </summary>
        public string Rule3 { get; set; }
        /// <summary>
        /// 条码规则4
        /// </summary>
        public string Rule4 { get; set; }
        /// <summary>
        /// 条码规则5
        /// </summary>
        public string Rule5 { get; set; }
        /// <summary>
        /// 条码规则6
        /// </summary>
        public string Rule6 { get; set; }
        /// <summary>
        /// 条码规则7
        /// </summary>
        public string Rule7 { get; set; }
        /// <summary>
        /// 条码规则8
        /// </summary>
        public string Rule8 { get; set; }
        /// <summary>
        /// 条码规则9
        /// </summary>
        public string Rule9 { get; set; }
        /// <summary>
        /// 条码规则10
        /// </summary>
        public string Rule10 { get; set; }
        /// <summary>
        /// 条码规则11
        /// </summary>
        public string Rule11 { get; set; }
        /// <summary>
        /// 条码规则12
        /// </summary>
        public string Rule12 { get; set; }
        /// <summary>
        /// 条码规则13
        /// </summary>
        public string Rule13 { get; set; }
        /// <summary>
        /// 条码规则14
        /// </summary>
        public string Rule14 { get; set; }

        public string CoderMain { get; set; }
        public int CodeLen { get; set; }
        #endregion
        
        #region 导入/保存配置ini
        public EmRes LoadParameter(string proname)//加载配置
        {
            EmRes ret = EmRes.Succeed;

            filename = proname;


            IniFile inf = new IniFile(filename);//确认路径是否存在，不存在则创建文件夹。
                                                //
            string STEP = "基本信息";  
            ProductName = inf.ReadString(STEP, "ProductName", ProductName);//产品型号 
            CordCount=inf.ReadInteger(STEP, "CordCount", CordCount);//  
            BarCord = inf.ReadString(STEP, "范例", BarCord);//二维码序样例 
            desr = inf.ReadString(STEP, "描述", desr);
            GreateTime = Convert.ToDateTime(inf.ReadString(STEP, "GreateTime", GreateTime.ToString()));// 
            CordLevel= inf.ReadString(STEP, "CordLevel", CordLevel);

            STEP = "排序规则";
            Rule1= inf.ReadString(STEP, " Rule1", Rule1);//二维码序样例 
            Rule2 = inf.ReadString(STEP, " Rule2", Rule2);//二维码序样例 
            Rule3 = inf.ReadString(STEP, " Rule3", Rule3);//二维码序样例 
            Rule4 = inf.ReadString(STEP, " Rule4", Rule4);//二维码序样例 
            Rule5 = inf.ReadString(STEP, " Rule5", Rule5);//二维码序样例 
            Rule6 = inf.ReadString(STEP, " Rule6", Rule6);//二维码序样例 
            Rule7 = inf.ReadString(STEP, " Rule7", Rule7);//二维码序样例 
            Rule8 = inf.ReadString(STEP, " Rule8", Rule8);//二维码序样例 
            Rule9 = inf.ReadString(STEP, " Rule9", Rule9);//二维码序样例 
            Rule10 = inf.ReadString(STEP, " Rule10", Rule10);//二维码序样例 
            Rule11 = inf.ReadString(STEP, " Rule11", Rule11);//二维码序样例 
            Rule12 = inf.ReadString(STEP, " Rule12", Rule12);//二维码序样例 
            Rule13 = inf.ReadString(STEP, " Rule13", Rule13);//二维码序样例 

            STEP = "主体";
            CoderMain = inf.ReadString(STEP, " main", CoderMain);//二维码序样例 
            CodeLen = inf.ReadInteger(STEP, " len", 0);//二维码序样例 
          

            STEP = "最后一次";
            CordNO = inf.ReadInteger(STEP, "CordNO", CordNO);//二维码序列号
                                                       
         //   string date1 = inf.ReadString(STEP, "刷新时间", "");//最后写入日期 =2022/9/19 10:56:47
          //  if (date1 == "") date1 = "2020-10-10";
           // string date2 = DateTime.Now.ToString("yyyy-MM-dd");
          //  date1 = Convert.ToDateTime(date1).ToString("yyyy-MM-dd");

            if (CordNO == 0)
            {
                CordNO = 1;//从1开始
                SaveParameter("");
            }
            return ret;
        }
        public EmRes SaveParameter(string proname, bool news=false)//保存参数
        {
            EmRes ret = EmRes.Succeed;
            if (proname == "") proname = this.ProductName;
          
            filename = $"{SysStatus.sys_dir_path }\\product\\CordSET\\{proname.Trim()}.ini";// 配置文件路径
            IniFile inf = new IniFile(filename);//确认路径是否存在，不存在则创建文件夹。
            string STEP = "";
            if(news)
            {
                STEP = "基本信息";
                inf.WriteString(STEP, "ProductName", ProductName);
                inf.WriteInteger(STEP, "CordCount", CordCount);
                inf.WriteString(STEP, "最近修改时间", DateTime.Now.ToString());
                inf.WriteString(STEP, "范例", BarCord);
                inf.WriteString(STEP, "描述", desr);
                inf.WriteString(STEP, "CordLevel", CordLevel);
                STEP = "排序规则";
                inf.WriteString(STEP, "Rule1", Rule1);
                inf.WriteString(STEP, "Rule2", Rule2);
                inf.WriteString(STEP, "Rule3", Rule3);
                inf.WriteString(STEP, "Rule4", Rule4);
                inf.WriteString(STEP, "Rule5", Rule5);
                inf.WriteString(STEP, "Rule6", Rule6);
                inf.WriteString(STEP, "Rule7", Rule7);
                inf.WriteString(STEP, "Rule8", Rule8);
                inf.WriteString(STEP, "Rule9", Rule9);
                inf.WriteString(STEP, "Rule10", Rule10);
                inf.WriteString(STEP, "Rule11", Rule11);
                inf.WriteString(STEP, "Rule12", Rule12);
                inf.WriteString(STEP, "Rule13", Rule13);
                STEP = "主体";
                inf.WriteString(STEP, "main", CoderMain);
                inf.WriteInteger(STEP, "len", CodeLen);
              

            }
          
            STEP = "最后一次";
            inf.WriteInteger(STEP, "CordNO", CordNO);//二维码序列号
            inf.WriteString(STEP, "刷新时间", DateTime.Now.ToString());

            hardware.my_cord.Set_Coder(CoderMain);
            hardware.my_cord.Set_Coder_Len(CodeLen);
            hardware.my_cord.Set_Coder_num(CordNO);
            hardware.my_cord.SaveSTcord();

            return ret;
        }

        #endregion
        #region 生产二维码
        /// <summary> 
        /// 获取要刻的二维码
        /// </summary>
        /// <returns></returns>
        public string GetCord()
        {
            string str1 = GetCotorl1(Convert.ToInt32((this.Rule1.Split(','))[0]), this.Rule1.Split(',')[1]);
            string str2 = GetCotorl1(Convert.ToInt32((this.Rule2.Split(','))[0]), this.Rule2.Split(',')[1]);
            string str3 = GetCotorl1(Convert.ToInt32((this.Rule3.Split(','))[0]), this.Rule3.Split(',')[1]);
            string str4 = GetCotorl1(Convert.ToInt32((this.Rule4.Split(','))[0]), this.Rule4.Split(',')[1]);
            string str5 = GetCotorl1(Convert.ToInt32((this.Rule5.Split(','))[0]), this.Rule5.Split(',')[1]);
            string str6 = GetCotorl1(Convert.ToInt32((this.Rule6.Split(','))[0]), this.Rule6.Split(',')[1]);
            string str7 = GetCotorl1(Convert.ToInt32((this.Rule7.Split(','))[0]), this.Rule7.Split(',')[1]);
            string str8 = GetCotorl1(Convert.ToInt32((this.Rule8.Split(','))[0]), this.Rule8.Split(',')[1]);
            string str9 = GetCotorl1(Convert.ToInt32((this.Rule9.Split(','))[0]), this.Rule9.Split(',')[1]);
            string str10 = GetCotorl1(Convert.ToInt32((this.Rule10.Split(','))[0]), this.Rule10.Split(',')[1]);
            string str11 = GetCotorl1(Convert.ToInt32((this.Rule11.Split(','))[0]), this.Rule11.Split(',')[1]);
            string str12 = GetCotorl1(Convert.ToInt32((this.Rule12.Split(','))[0]), this.Rule12.Split(',')[1]);
            string str13 = GetCotorl1(Convert.ToInt32((this.Rule13.Split(','))[0]), this.Rule13.Split(',')[1]);
            //检查数据库重复
            return str1 + str2 + str3 + str4 + str5 + str6 + str7 + str8 + str9 + str10 + str11 + str12 + str13;
          
        }
        /// <summary>
        /// 编码规则
        /// </summary>
        /// <param name="result"></param>
        /// <param name="texs"></param>
        /// <returns></returns>
        private string GetCotorl1(int result, string texs)
        {
            string str = result.ToString();
            string var = null;
            string rcv = null;//班次
            if (result > 0)
            {
                switch (str)
                {
                    case "1":

                    case "2":

                    case "3":

                    case "4":

                    case "5":
                        var = "string";
                        break;
                    case "6":
                        var = "year";
                        break;
                    case "7":
                        var = "month";
                        break;
                    case "8":
                        var = "day";
                        break;
                    case "9":
                        var = "hour";
                        break;
                    case "10":
                        var = "min";
                        break;
                    case "11":
                        var = "sec";
                        break;
                    case "12":
                        var = "yearday";
                        break;
                    case "13":
                        var = "cord";
                        break;
                    case "14":
                        var = "string";
                        break;
                    case "15":
                        var = "banci";
                        break;
                    default:
                        break;
                }
            }

            if (var == "banci")
            {
                string st1 = "08:00";
                string st2 = "20:00";
                DateTime dt1 = Convert.ToDateTime(st1);
                DateTime dt2 = Convert.ToDateTime(st2);
                DateTime dt3 = DateTime.Now;
                if (DateTime.Compare(dt3, dt1) > 0 && DateTime.Compare(dt3, dt2) < 0)
                {
                    rcv = "1";
                }
                else
                {
                    rcv = "2";
                }
            }

            if (var == "string")
            {
                rcv = texs;
            }
            else if (var == "year")
            {
                rcv = (DateTime.Now.Year - 2000).ToString();
            }
            else if (var == "month")
            {
                if (texs == "16#")
                {
                    if (DateTime.Now.Month == 10)
                    {
                        rcv = "A";
                    }
                    else if (DateTime.Now.Month == 11)
                    {
                        rcv = "B";
                    }
                    else if (DateTime.Now.Month == 12)
                    {
                        rcv = "C";
                    }
                    else
                    {
                        rcv = DateTime.Now.Month.ToString();
                    }
                }

                if (texs == "2#")
                {
                    int a = DateTime.Now.Month;
                    rcv = string.Format("{0:D2}", a);
                }
            }
            else if (var == "day")
            {
                int a = DateTime.Now.Day;
                rcv = string.Format("{0:D2}", a);
            }
            else if (var == "hour")
            {
                int a = DateTime.Now.Hour;
                rcv = string.Format("{0:D2}", a);
            }
            else if (var == "min")
            {
                int a = DateTime.Now.Minute;
                rcv = string.Format("{0:D2}", a);
            }
            else if (var == "sec")
            {
                int a = DateTime.Now.Second;
                rcv = string.Format("{0:D2}", a);
            }
            else if (var == "yearday")
            {
                int a = DateTime.Now.DayOfYear;
                rcv = string.Format("{0:D3}", a);
            }
            else if (var == "cord")
            {
                if (texs == "5")
                {
                    if (this.CordNO >= 99998) CordNO = 1;
                     rcv = string.Format("{0:D5}", this.CordNO);
                }
                else
                if (texs == "6")
                {
                    if (this.CordNO >= 99999) CordNO = 1;
                    rcv = string.Format("{0:D6}", this.CordNO);
                }
                else
                if (texs == "7")
                {
                    if (this.CordNO >= 999999) CordNO = 1;
                    rcv = string.Format("{0:D7}", this.CordNO);
                }
                else
                if (texs == "8")
                {
                    if (this.CordNO >= 999999) CordNO = 1;
                    rcv = string.Format("{0:D8}", this.CordNO);
                }
                else
                if (texs == "9")
                {
                    if (this.CordNO >= 999999) CordNO = 1;
                    rcv = string.Format("{0:D9}", this.CordNO);
                }
                else
                if (texs == "10")
                {
                    if (this.CordNO >= 999999) CordNO = 1;
                    rcv = string.Format("{0:D10}", this.CordNO);
                }
                else
                {
                    if (this.CordNO >= 999999) CordNO = 1;
                    rcv = string.Format("{0:D5}", this.CordNO);
                }
                this.CordNO++;
                this.SaveParameter(this.ProductName);
            }
            return rcv;
        }

        #endregion

 


    }
}
