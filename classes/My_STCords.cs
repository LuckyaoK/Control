using CXPro001.myclass;
using MyLib.Files;
using MyLib.SqlHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPro001.classes
{
    public class My_STCords
    {
        #region 结构体
        /// <summary>
        /// 工位信息
        /// </summary>
        public struct Products
        {
            /// <summary>
            /// 左二维码
            /// </summary>
            public string Cord_ST_L;
            /// <summary>
            /// 右二维码
            /// </summary>
            public string Cord_ST_R;

            /// <summary>
            /// 左磨具号
            /// </summary>
            public int Model_Num_L;
            /// <summary>
            /// 右磨具号
            /// </summary>
            public int Model_Num_R;

            /// <summary>
            /// 左耐压
            /// </summary>
            public bool STS_L_voltage;
            /// <summary>
            /// 右耐压
            /// </summary>
            public bool STS_R_voltage;
            /// <summary>
            /// 左插口
            /// </summary>
            public bool STS_L_CCD1_H;
            /// <summary>
            /// 左插口
            /// </summary>
            public bool STS_L_CCD1_P;
            /// <summary>
            /// 右插口
            /// </summary>
            public bool STS_R_CCD1_H;
            /// <summary>
            /// 右插口
            /// </summary>
            public bool STS_R_CCD1_P;
            /// <summary>
            /// 左内部
            /// </summary>
            public bool STS_L_CCD2_H;
            /// <summary>
            /// 左内部
            /// </summary>
            public bool STS_L_CCD2_P;
            /// <summary>
            /// 右内部
            /// </summary>
            public bool STS_R_CCD2_H;
            /// <summary>
            /// 右内部
            /// </summary>
            public bool STS_R_CCD2_P;
            /// <summary>
            /// 左激光
            /// </summary>
            public bool STS_L_LASER;
            /// <summary>
            /// 右激光
            /// </summary>
            public bool STS_R_LASER;
            /// <summary>
            /// 左气压
            /// </summary>
            public bool STS_L_AIR;
            /// <summary>
            /// 右气压
            /// </summary>
            public bool STS_R_AIR;
        }

        /// <summary>
        /// 产量统计
        /// </summary>
        public struct Products_Count
        {
            /// <summary>
            /// 正常
            /// </summary>
            public int pass;

            /// <summary>
            /// 耐压
            /// </summary>
            public int voltage;

            /// <summary>
            /// 插口
            /// </summary>
            public int CCD1;

            /// <summary>
            /// 内部
            /// </summary>
            public bool CCD2;
            /// <summary>
            /// 打码
            /// </summary>
            public bool LASER;

        }

        #endregion

        #region 各站二维码、序号
        public Products[] my_Pro = new Products[8];

        public Products_Count my_Count;
        public string Corder_Main;
        public int Corder_len;
        public int Corder_s;

        #endregion



        /// <summary>
        /// 初始化
        /// </summary>
        public   My_STCords()
        {
           
        }
        public void InitStcord(int i)
        {
            
 

                my_Pro[i].STS_L_voltage = true;
                my_Pro[i].STS_R_voltage = true;

                my_Pro[i].STS_L_CCD1_H = true;
                my_Pro[i].STS_L_CCD1_P = true;
                my_Pro[i].STS_L_CCD2_H = true;
                my_Pro[i].STS_L_CCD2_P = true;

                my_Pro[i].STS_R_CCD1_H = true;
                my_Pro[i].STS_R_CCD1_P = true;
                my_Pro[i].STS_R_CCD2_H = true;
                my_Pro[i].STS_R_CCD2_P = true;

                my_Pro[i].STS_L_LASER = true;
                my_Pro[i].STS_R_LASER = true;
                my_Pro[i].STS_L_AIR = true;
                my_Pro[i].STS_R_AIR = true;
          
        }
        /// <summary>
        /// 设置编码主体
        /// </summary>
        /// <param name="str"></param>
        public void Set_Coder(string str)
        {
            Corder_Main = str;

        }
        /// <summary>
        /// 编号长度
        /// </summary>
        /// <param name="len"></param>
        public void Set_Coder_Len(int len)
        {
            Corder_len = len;
        }
        /// <summary>
        /// 编号当前计数
        /// </summary>
        /// <param name="s"></param>
        public void Set_Coder_num(int s)
        {
            Corder_s = s;
        }

        /// <summary>
        /// 设置工位状态  2耐压 3侧CCDH  4顶CCDH  5 打码 6侧P 7顶P
        /// </summary>
        /// <param name="x">1 站号</param>
        /// <param name="y">1左  2右</param>
        /// <param name="sts">1正常 2耐压 3侧CCD  4顶CCD  5 打码</param>
        public void Set_ST_STS(int num,int index,string lr,bool sts)
        {
            if (lr == "L")
            {
                if (num == 1)
                    my_Pro[index].STS_L_AIR = sts;
                if (num == 2)
                    my_Pro[index].STS_L_voltage = sts;
                if (num == 3)
                    my_Pro[index].STS_L_CCD1_H = sts;
                if (num == 4)
                    my_Pro[index].STS_L_CCD2_H = sts;
                if (num == 5)
                    my_Pro[index].STS_L_LASER = sts;
                if (num == 6)
                    my_Pro[index].STS_L_CCD1_P = sts;
                if (num == 7)
                    my_Pro[index].STS_L_CCD2_P = sts;

            }
            else
            {
                if (num == 1)
                    my_Pro[index].STS_R_AIR = sts;
                if (num == 2)
                    my_Pro[index].STS_R_voltage = sts;
                if (num == 3)
                    my_Pro[index].STS_R_CCD1_H = sts;
                if (num == 4)
                    my_Pro[index].STS_R_CCD2_H = sts;
                if (num == 5)
                    my_Pro[index].STS_R_LASER = sts;
                if (num == 6)
                    my_Pro[index].STS_R_CCD1_P = sts;
                if (num == 7)
                    my_Pro[index].STS_R_CCD2_P = sts;
            }

        }
        /// <summary>
        /// 产生一个新编码
        /// </summary>
        public string NewCord(String  lr)
        {
            /////////////////////////////////
            ///
            string rcv = (DateTime.Now.Year - 2000).ToString();//年
            int a = DateTime.Now.Month;
            string now = string.Format("{0:D2}", a);   //月
            rcv = rcv + now;
            a = DateTime.Now.Day;
            now = string.Format("{0:D2}", a);  //日
            rcv = rcv + now;

            int xxx = Corder_Main.IndexOf("X@");
            string sss = Corder_Main.Substring(xxx + 2);
            if (rcv.Contains(sss) == false)
            {
                Corder_Main = Corder_Main.Replace(sss, rcv);
                Corder_s = 0;
            }

            ////////////////////////////
            Corder_s++;
            string gs = "D" + Corder_len.ToString();//长度补0
            string str1;
            string main= Corder_Main;
            if (lr == "L")
                main = Corder_Main.Replace("@X", "@" + my_Pro[1].Model_Num_L.ToString("D2"));
            else
                main = Corder_Main.Replace("@X", "@" + my_Pro[1].Model_Num_R.ToString("D2"));

            str1 = main + "@"+Corder_s.ToString(gs);
            while (CheckDB(str1))
            {
                Corder_s++;
                str1 = main +"@"+ Corder_s.ToString(gs);
            }
            return str1;
          
        }
        /// <summary>
        /// 创建新二维码1工位
        /// </summary>
        /// <param name="lr">1左  2右</param>
        public void Set_Cord_ST(string str)
        {
            InitStcord(1);
            if (str == "L")
                my_Pro[1].Cord_ST_L = NewCord("L");
            else
                my_Pro[1].Cord_ST_R = NewCord("R");
        }
        /// <summary>
        /// 设置1工位   磨具号
        /// </summary>
        /// <param name="lr">1左  2右</param>
        public void Set_Modle(string str,int str2)
        {
            if (str == "L")
                my_Pro[1].Model_Num_L = str2;
            else
                my_Pro[1].Model_Num_R = str2;
        }

        /// <summary>
        /// 检查是否重复出现
        /// </summary>
        /// <param name="cords"></param>
        /// <returns></returns>
        public bool CheckDB(string cords)
        {
            string sql2 = $"SELECT count(*) from  VoltageData_8740 WHERE Cord = '{cords}'";
            int i2 = int.Parse(SQLHelper.ExecuteScalar(sql2));
            if (i2 > 0)
            {
                return true;
            }else

            return false;
        }
        /// <summary>
        /// 读取二维码记录
        /// </summary>
        public void LoadSTcord()
        {
            string cordpath = $"{SysStatus.sys_dir_path}\\product\\{SysStatus.CurProductName}\\STCord.ini";// 配置文件路径
            IniFile inf = new IniFile(cordpath);//确认路径是否存在，不存在则创建文件夹。
            string STEP = "主体";
            Corder_Main = inf.ReadString(STEP, "main", "");
            Corder_len = inf.ReadInteger(STEP, "len", 0);
            Corder_s = inf.ReadInteger(STEP, "s", 0);

            STEP = "各站二维码";
            my_Pro[1].Cord_ST_L = inf.ReadString(STEP, "Cord_ST1_1", "");
            my_Pro[1].Cord_ST_R = inf.ReadString(STEP, "Cord_ST1_2", "");
            my_Pro[2].Cord_ST_L = inf.ReadString(STEP, "Cord_ST2_1", "");
            my_Pro[2].Cord_ST_R = inf.ReadString(STEP, "Cord_ST2_2", "");
            my_Pro[3].Cord_ST_L = inf.ReadString(STEP, "Cord_ST3_1", "");
            my_Pro[3].Cord_ST_R = inf.ReadString(STEP, "Cord_ST3_2", "");
            my_Pro[4].Cord_ST_L = inf.ReadString(STEP, "Cord_ST4_1", "");
            my_Pro[4].Cord_ST_R = inf.ReadString(STEP, "Cord_ST4_2", "");
            my_Pro[5].Cord_ST_L = inf.ReadString(STEP, "Cord_ST5_1", "");
            my_Pro[5].Cord_ST_R = inf.ReadString(STEP, "Cord_ST5_2", "");
            my_Pro[6].Cord_ST_L = inf.ReadString(STEP, "Cord_ST6_1", "");
            my_Pro[6].Cord_ST_R = inf.ReadString(STEP, "Cord_ST6_2", "");

            STEP = "各站模号";
            my_Pro[1].Model_Num_L =int.Parse( inf.ReadString(STEP, "Model1", "1"));
            my_Pro[1].Model_Num_R = int.Parse(inf.ReadString(STEP, "Model2", "1"));
            my_Pro[2].Model_Num_L = int.Parse(inf.ReadString(STEP, "Model3", "1"));
            my_Pro[2].Model_Num_R = int.Parse(inf.ReadString(STEP, "Model4", "1"));
            my_Pro[3].Model_Num_L = int.Parse(inf.ReadString(STEP, "Model5", "1"));
            my_Pro[3].Model_Num_R = int.Parse(inf.ReadString(STEP, "Model6", "1"));
            my_Pro[4].Model_Num_L = int.Parse(inf.ReadString(STEP, "Model7", "1"));
            my_Pro[4].Model_Num_R = int.Parse(inf.ReadString(STEP, "Model8", "1"));
            my_Pro[5].Model_Num_L = int.Parse(inf.ReadString(STEP, "Model9", "1"));
            my_Pro[5].Model_Num_R = int.Parse(inf.ReadString(STEP, "Model10", "1"));
            my_Pro[6].Model_Num_L = int.Parse(inf.ReadString(STEP, "Model11", "1"));
            my_Pro[6].Model_Num_R = int.Parse(inf.ReadString(STEP, "Model12", "1"));

            STEP = "各站耐压";
            my_Pro[1].STS_L_voltage = bool.Parse(inf.ReadString(STEP, "voltage1", "true"));
            my_Pro[1].STS_R_voltage = bool.Parse(inf.ReadString(STEP, "voltage2", "true"));
            my_Pro[2].STS_L_voltage = bool.Parse(inf.ReadString(STEP, "voltage3", "true"));
            my_Pro[2].STS_R_voltage = bool.Parse(inf.ReadString(STEP, "voltage4", "true"));
            my_Pro[3].STS_L_voltage = bool.Parse(inf.ReadString(STEP, "voltage5", "true"));
            my_Pro[3].STS_R_voltage = bool.Parse(inf.ReadString(STEP, "voltage6", "true"));
            my_Pro[4].STS_L_voltage = bool.Parse(inf.ReadString(STEP, "voltage7", "true"));
            my_Pro[4].STS_R_voltage = bool.Parse(inf.ReadString(STEP, "voltage8", "true"));
            my_Pro[5].STS_L_voltage = bool.Parse(inf.ReadString(STEP, "voltage9", "true"));
            my_Pro[5].STS_R_voltage = bool.Parse(inf.ReadString(STEP, "voltage10", "true"));
            my_Pro[6].STS_L_voltage = bool.Parse(inf.ReadString(STEP, "voltage11", "true"));
            my_Pro[6].STS_R_voltage = bool.Parse(inf.ReadString(STEP, "voltage12", "true"));

            STEP = "各站插口";
            my_Pro[1].STS_L_CCD1_H = bool.Parse(inf.ReadString(STEP, "CCD1_1", "true"));
            my_Pro[1].STS_L_CCD1_P = bool.Parse(inf.ReadString(STEP, "CCD1_2", "true"));
            my_Pro[1].STS_R_CCD1_H = bool.Parse(inf.ReadString(STEP, "CCD1_3", "true"));
            my_Pro[1].STS_R_CCD1_P = bool.Parse(inf.ReadString(STEP, "CCD1_4", "true"));

            my_Pro[2].STS_L_CCD1_H = bool.Parse(inf.ReadString(STEP, "CCD2_1", "true"));
            my_Pro[2].STS_L_CCD1_P = bool.Parse(inf.ReadString(STEP, "CCD2_2", "true"));
            my_Pro[2].STS_R_CCD1_H = bool.Parse(inf.ReadString(STEP, "CCD2_3", "true"));
            my_Pro[2].STS_R_CCD1_P = bool.Parse(inf.ReadString(STEP, "CCD2_4", "true"));

            my_Pro[3].STS_L_CCD1_H = bool.Parse(inf.ReadString(STEP, "CCD3_1", "true"));
            my_Pro[3].STS_L_CCD1_P = bool.Parse(inf.ReadString(STEP, "CCD3_2", "true"));
            my_Pro[3].STS_R_CCD1_H = bool.Parse(inf.ReadString(STEP, "CCD3_3", "true"));
            my_Pro[3].STS_R_CCD1_P = bool.Parse(inf.ReadString(STEP, "CCD3_4", "true"));

            my_Pro[4].STS_L_CCD1_H = bool.Parse(inf.ReadString(STEP, "CCD4_1", "true"));
            my_Pro[4].STS_L_CCD1_P = bool.Parse(inf.ReadString(STEP, "CCD4_2", "true"));
            my_Pro[4].STS_R_CCD1_H = bool.Parse(inf.ReadString(STEP, "CCD4_3", "true"));
            my_Pro[4].STS_R_CCD1_P = bool.Parse(inf.ReadString(STEP, "CCD4_4", "true"));

            my_Pro[5].STS_L_CCD1_H = bool.Parse(inf.ReadString(STEP, "CCD5_1", "true"));
            my_Pro[5].STS_L_CCD1_P = bool.Parse(inf.ReadString(STEP, "CCD5_2", "true"));
            my_Pro[5].STS_R_CCD1_H = bool.Parse(inf.ReadString(STEP, "CCD5_3", "true"));
            my_Pro[5].STS_R_CCD1_P = bool.Parse(inf.ReadString(STEP, "CCD5_4", "true"));

            my_Pro[6].STS_L_CCD1_H = bool.Parse(inf.ReadString(STEP, "CCD6_1", "true"));
            my_Pro[6].STS_L_CCD1_P = bool.Parse(inf.ReadString(STEP, "CCD6_2", "true"));
            my_Pro[6].STS_R_CCD1_H = bool.Parse(inf.ReadString(STEP, "CCD6_3", "true"));
            my_Pro[6].STS_R_CCD1_P = bool.Parse(inf.ReadString(STEP, "CCD6_4", "true"));

            STEP = "各站内部";
            my_Pro[1].STS_L_CCD2_H = bool.Parse(inf.ReadString(STEP, "CCD1_1", "true"));
            my_Pro[1].STS_L_CCD2_P = bool.Parse(inf.ReadString(STEP, "CCD1_2", "true"));
            my_Pro[1].STS_R_CCD2_H = bool.Parse(inf.ReadString(STEP, "CCD1_3", "true"));
            my_Pro[1].STS_R_CCD2_P = bool.Parse(inf.ReadString(STEP, "CCD1_4", "true"));

            my_Pro[2].STS_L_CCD2_H = bool.Parse(inf.ReadString(STEP, "CCD2_1", "true"));
            my_Pro[2].STS_L_CCD2_P = bool.Parse(inf.ReadString(STEP, "CCD2_2", "true"));
            my_Pro[2].STS_R_CCD2_H = bool.Parse(inf.ReadString(STEP, "CCD2_3", "true"));
            my_Pro[2].STS_R_CCD2_P = bool.Parse(inf.ReadString(STEP, "CCD2_4", "true"));

            my_Pro[3].STS_L_CCD2_H = bool.Parse(inf.ReadString(STEP, "CCD3_1", "true"));
            my_Pro[3].STS_L_CCD2_P = bool.Parse(inf.ReadString(STEP, "CCD3_2", "true"));
            my_Pro[3].STS_R_CCD2_H = bool.Parse(inf.ReadString(STEP, "CCD3_3", "true"));
            my_Pro[3].STS_R_CCD2_P = bool.Parse(inf.ReadString(STEP, "CCD3_4", "true"));

            my_Pro[4].STS_L_CCD2_H = bool.Parse(inf.ReadString(STEP, "CCD4_1", "true"));
            my_Pro[4].STS_L_CCD2_P = bool.Parse(inf.ReadString(STEP, "CCD4_2", "true"));
            my_Pro[4].STS_R_CCD2_H = bool.Parse(inf.ReadString(STEP, "CCD4_3", "true"));
            my_Pro[4].STS_R_CCD2_P = bool.Parse(inf.ReadString(STEP, "CCD4_4", "true"));

            my_Pro[5].STS_L_CCD2_H = bool.Parse(inf.ReadString(STEP, "CCD5_1", "true"));
            my_Pro[5].STS_L_CCD2_P = bool.Parse(inf.ReadString(STEP, "CCD5_2", "true"));
            my_Pro[5].STS_R_CCD2_H = bool.Parse(inf.ReadString(STEP, "CCD5_3", "true"));
            my_Pro[5].STS_R_CCD2_P = bool.Parse(inf.ReadString(STEP, "CCD5_4", "true"));

            my_Pro[6].STS_L_CCD2_H = bool.Parse(inf.ReadString(STEP, "CCD6_1", "true"));
            my_Pro[6].STS_L_CCD2_P = bool.Parse(inf.ReadString(STEP, "CCD6_2", "true"));
            my_Pro[6].STS_R_CCD2_H = bool.Parse(inf.ReadString(STEP, "CCD6_3", "true"));
            my_Pro[6].STS_R_CCD2_P = bool.Parse(inf.ReadString(STEP, "CCD6_4", "true"));



            STEP = "各站激光";
            my_Pro[1].STS_L_LASER = bool.Parse(inf.ReadString(STEP, "LASER1", "true"));
            my_Pro[1].STS_R_LASER = bool.Parse(inf.ReadString(STEP, "LASER2", "true"));
            my_Pro[2].STS_L_LASER = bool.Parse(inf.ReadString(STEP, "LASER3", "true"));
            my_Pro[2].STS_R_LASER = bool.Parse(inf.ReadString(STEP, "LASER4", "true"));
            my_Pro[3].STS_L_LASER = bool.Parse(inf.ReadString(STEP, "LASER5", "true"));
            my_Pro[3].STS_R_LASER = bool.Parse(inf.ReadString(STEP, "LASER6", "true"));
            my_Pro[4].STS_L_LASER = bool.Parse(inf.ReadString(STEP, "LASER7", "true"));
            my_Pro[4].STS_R_LASER = bool.Parse(inf.ReadString(STEP, "LASER8", "true"));
            my_Pro[5].STS_L_LASER = bool.Parse(inf.ReadString(STEP, "LASER9", "true"));
            my_Pro[5].STS_R_LASER = bool.Parse(inf.ReadString(STEP, "LASER10", "true"));
            my_Pro[6].STS_L_LASER = bool.Parse(inf.ReadString(STEP, "LASER11", "true"));
            my_Pro[6].STS_R_LASER = bool.Parse(inf.ReadString(STEP, "LASER12", "true"));


            STEP = "气压";
            my_Pro[1].STS_L_AIR = bool.Parse(inf.ReadString(STEP, "AIR1", "true"));
            my_Pro[1].STS_R_AIR = bool.Parse(inf.ReadString(STEP, "AIR2", "true"));
            my_Pro[2].STS_L_AIR = bool.Parse(inf.ReadString(STEP, "AIR3", "true"));
            my_Pro[2].STS_R_AIR = bool.Parse(inf.ReadString(STEP, "AIR4", "true"));
            my_Pro[3].STS_L_AIR = bool.Parse(inf.ReadString(STEP, "AIR5", "true"));
            my_Pro[3].STS_R_AIR = bool.Parse(inf.ReadString(STEP, "AIR6", "true"));
            my_Pro[4].STS_L_AIR = bool.Parse(inf.ReadString(STEP, "AIR7", "true"));
            my_Pro[4].STS_R_AIR = bool.Parse(inf.ReadString(STEP, "AIR8", "true"));
            my_Pro[5].STS_L_AIR = bool.Parse(inf.ReadString(STEP, "AIR9", "true"));
            my_Pro[5].STS_R_AIR = bool.Parse(inf.ReadString(STEP, "AIR10", "true"));
            my_Pro[6].STS_L_AIR = bool.Parse(inf.ReadString(STEP, "AIR11", "true"));
            my_Pro[6].STS_R_AIR = bool.Parse(inf.ReadString(STEP, "AIR12", "true"));
        }

        /// <summary>
        /// 转一次保存一次
        /// </summary>
        public void SaveSTcord()
        {
            string cordpath = $"{SysStatus.sys_dir_path }\\product\\{SysStatus.CurProductName}\\STCord.ini";// 配置文件路径
            IniFile inf = new IniFile(cordpath);//确认路径是否存在，不存在则创建文件夹。             

            string STEP = "主体";
            inf.WriteString(STEP, "main", Corder_Main);
            inf.WriteInteger(STEP, "len", Corder_len);
            inf.WriteInteger(STEP, "s", Corder_s);

            STEP = "各站二维码";
            inf.WriteString(STEP, "Cord_ST1_1", my_Pro[1].Cord_ST_L);
            inf.WriteString(STEP, "Cord_ST1_2", my_Pro[1].Cord_ST_R);
            inf.WriteString(STEP, "Cord_ST2_1", my_Pro[2].Cord_ST_L);
            inf.WriteString(STEP, "Cord_ST2_2", my_Pro[2].Cord_ST_R);
            inf.WriteString(STEP, "Cord_ST3_1", my_Pro[3].Cord_ST_L);
            inf.WriteString(STEP, "Cord_ST3_2", my_Pro[3].Cord_ST_R);
            inf.WriteString(STEP, "Cord_ST4_1", my_Pro[4].Cord_ST_L);
            inf.WriteString(STEP, "Cord_ST4_2", my_Pro[4].Cord_ST_R);
            inf.WriteString(STEP, "Cord_ST5_1", my_Pro[5].Cord_ST_L);
            inf.WriteString(STEP, "Cord_ST5_2", my_Pro[5].Cord_ST_R);
            inf.WriteString(STEP, "Cord_ST6_1", my_Pro[6].Cord_ST_L);
            inf.WriteString(STEP, "Cord_ST6_2", my_Pro[6].Cord_ST_R);
            STEP = "各站模号";
            inf.WriteString(STEP, "Model1", my_Pro[1].Model_Num_L.ToString());
            inf.WriteString(STEP, "Model2", my_Pro[1].Model_Num_R.ToString());
            inf.WriteString(STEP, "Model3", my_Pro[2].Model_Num_L.ToString());
            inf.WriteString(STEP, "Model4", my_Pro[2].Model_Num_R.ToString());
            inf.WriteString(STEP, "Model5", my_Pro[3].Model_Num_L.ToString());
            inf.WriteString(STEP, "Model6", my_Pro[3].Model_Num_R.ToString());
            inf.WriteString(STEP, "Model7", my_Pro[4].Model_Num_L.ToString());
            inf.WriteString(STEP, "Model8", my_Pro[4].Model_Num_R.ToString());
            inf.WriteString(STEP, "Model9", my_Pro[5].Model_Num_L.ToString());
            inf.WriteString(STEP, "Model10", my_Pro[5].Model_Num_R.ToString());
            inf.WriteString(STEP, "Model11", my_Pro[6].Model_Num_L.ToString());
            inf.WriteString(STEP, "Model12", my_Pro[6].Model_Num_R.ToString());

            STEP = "各站耐压";
            inf.WriteString(STEP, "voltage1", my_Pro[1].STS_L_voltage.ToString());
            inf.WriteString(STEP, "voltage2", my_Pro[1].STS_R_voltage.ToString());
            inf.WriteString(STEP, "voltage3", my_Pro[2].STS_L_voltage.ToString());
            inf.WriteString(STEP, "voltage4", my_Pro[2].STS_R_voltage.ToString());
            inf.WriteString(STEP, "voltage5", my_Pro[3].STS_L_voltage.ToString());
            inf.WriteString(STEP, "voltage6", my_Pro[3].STS_R_voltage.ToString());
            inf.WriteString(STEP, "voltage7", my_Pro[4].STS_L_voltage.ToString());
            inf.WriteString(STEP, "voltage8", my_Pro[4].STS_R_voltage.ToString());
            inf.WriteString(STEP, "voltage9", my_Pro[5].STS_L_voltage.ToString());
            inf.WriteString(STEP, "voltage10", my_Pro[5].STS_R_voltage.ToString());
            inf.WriteString(STEP, "voltage11", my_Pro[6].STS_L_voltage.ToString());
            inf.WriteString(STEP, "voltage12", my_Pro[6].STS_R_voltage.ToString());

 
            STEP = "各站插口";
        
            inf.WriteString(STEP, "CCD1_1", my_Pro[1].STS_L_CCD1_H.ToString());
            inf.WriteString(STEP, "CCD1_2", my_Pro[1].STS_L_CCD1_P.ToString());
            inf.WriteString(STEP, "CCD1_3", my_Pro[1].STS_R_CCD1_H.ToString());
            inf.WriteString(STEP, "CCD1_4", my_Pro[1].STS_R_CCD1_P.ToString());

            inf.WriteString(STEP, "CCD2_1", my_Pro[2].STS_L_CCD1_H.ToString());
            inf.WriteString(STEP, "CCD2_2", my_Pro[2].STS_L_CCD1_P.ToString());
            inf.WriteString(STEP, "CCD2_3", my_Pro[2].STS_R_CCD1_H.ToString());
            inf.WriteString(STEP, "CCD2_4", my_Pro[2].STS_R_CCD1_P.ToString());

            inf.WriteString(STEP, "CCD3_1", my_Pro[3].STS_L_CCD1_H.ToString());
            inf.WriteString(STEP, "CCD3_2", my_Pro[3].STS_L_CCD1_P.ToString());
            inf.WriteString(STEP, "CCD3_3", my_Pro[3].STS_R_CCD1_H.ToString());
            inf.WriteString(STEP, "CCD3_4", my_Pro[3].STS_R_CCD1_P.ToString());

            inf.WriteString(STEP, "CCD4_1", my_Pro[4].STS_L_CCD1_H.ToString());
            inf.WriteString(STEP, "CCD4_2", my_Pro[4].STS_L_CCD1_P.ToString());
            inf.WriteString(STEP, "CCD4_3", my_Pro[4].STS_R_CCD1_H.ToString());
            inf.WriteString(STEP, "CCD4_4", my_Pro[4].STS_R_CCD1_P.ToString());

            inf.WriteString(STEP, "CCD5_1", my_Pro[5].STS_L_CCD1_H.ToString());
            inf.WriteString(STEP, "CCD5_2", my_Pro[5].STS_L_CCD1_P.ToString());
            inf.WriteString(STEP, "CCD5_3", my_Pro[5].STS_R_CCD1_H.ToString());
            inf.WriteString(STEP, "CCD5_4", my_Pro[5].STS_R_CCD1_P.ToString());

            inf.WriteString(STEP, "CCD6_1", my_Pro[6].STS_L_CCD1_H.ToString());
            inf.WriteString(STEP, "CCD6_2", my_Pro[6].STS_L_CCD1_P.ToString());
            inf.WriteString(STEP, "CCD6_3", my_Pro[6].STS_R_CCD1_H.ToString());
            inf.WriteString(STEP, "CCD6_4", my_Pro[6].STS_R_CCD1_P.ToString());


            STEP = "各站内部";
            inf.WriteString(STEP, "CCD1_1", my_Pro[1].STS_L_CCD2_H.ToString());
            inf.WriteString(STEP, "CCD1_2", my_Pro[1].STS_L_CCD2_P.ToString());
            inf.WriteString(STEP, "CCD1_3", my_Pro[1].STS_R_CCD2_H.ToString());
            inf.WriteString(STEP, "CCD1_4", my_Pro[1].STS_R_CCD2_P.ToString());

            inf.WriteString(STEP, "CCD2_1", my_Pro[2].STS_L_CCD2_H.ToString());
            inf.WriteString(STEP, "CCD2_2", my_Pro[2].STS_L_CCD2_P.ToString());
            inf.WriteString(STEP, "CCD2_3", my_Pro[2].STS_R_CCD2_H.ToString());
            inf.WriteString(STEP, "CCD2_4", my_Pro[2].STS_R_CCD2_P.ToString());

            inf.WriteString(STEP, "CCD3_1", my_Pro[3].STS_L_CCD2_H.ToString());
            inf.WriteString(STEP, "CCD3_2", my_Pro[3].STS_L_CCD2_P.ToString());
            inf.WriteString(STEP, "CCD3_3", my_Pro[3].STS_R_CCD2_H.ToString());
            inf.WriteString(STEP, "CCD3_4", my_Pro[3].STS_R_CCD2_P.ToString());

            inf.WriteString(STEP, "CCD4_1", my_Pro[4].STS_L_CCD2_H.ToString());
            inf.WriteString(STEP, "CCD4_2", my_Pro[4].STS_L_CCD2_P.ToString());
            inf.WriteString(STEP, "CCD4_3", my_Pro[4].STS_R_CCD2_H.ToString());
            inf.WriteString(STEP, "CCD4_4", my_Pro[4].STS_R_CCD2_P.ToString());

            inf.WriteString(STEP, "CCD5_1", my_Pro[5].STS_L_CCD2_H.ToString());
            inf.WriteString(STEP, "CCD5_2", my_Pro[5].STS_L_CCD2_P.ToString());
            inf.WriteString(STEP, "CCD5_3", my_Pro[5].STS_R_CCD2_H.ToString());
            inf.WriteString(STEP, "CCD5_4", my_Pro[5].STS_R_CCD2_P.ToString());

            inf.WriteString(STEP, "CCD6_1", my_Pro[6].STS_L_CCD2_H.ToString());
            inf.WriteString(STEP, "CCD6_2", my_Pro[6].STS_L_CCD2_P.ToString());
            inf.WriteString(STEP, "CCD6_3", my_Pro[6].STS_R_CCD2_H.ToString());
            inf.WriteString(STEP, "CCD6_4", my_Pro[6].STS_R_CCD2_P.ToString());



            STEP = "各站激光";
            inf.WriteString(STEP, "LASER1", my_Pro[1].STS_L_LASER.ToString());
            inf.WriteString(STEP, "LASER2", my_Pro[1].STS_R_LASER.ToString());
            inf.WriteString(STEP, "LASER3", my_Pro[2].STS_L_LASER.ToString());
            inf.WriteString(STEP, "LASER4", my_Pro[2].STS_R_LASER.ToString());
            inf.WriteString(STEP, "LASER5", my_Pro[3].STS_L_LASER.ToString());
            inf.WriteString(STEP, "LASER6", my_Pro[3].STS_R_LASER.ToString());
            inf.WriteString(STEP, "LASER7", my_Pro[4].STS_L_LASER.ToString());
            inf.WriteString(STEP, "LASER8", my_Pro[4].STS_R_LASER.ToString());
            inf.WriteString(STEP, "LASER9", my_Pro[5].STS_L_LASER.ToString());
            inf.WriteString(STEP, "LASER10", my_Pro[5].STS_R_LASER.ToString());
            inf.WriteString(STEP, "LASER11", my_Pro[6].STS_L_LASER.ToString());
            inf.WriteString(STEP, "LASER12", my_Pro[6].STS_R_LASER.ToString());

            STEP = "气压";
            inf.WriteString(STEP, "AIR1", my_Pro[1].STS_L_AIR.ToString());
            inf.WriteString(STEP, "AIR2", my_Pro[1].STS_R_AIR.ToString());
            inf.WriteString(STEP, "AIR3", my_Pro[2].STS_L_AIR.ToString());
            inf.WriteString(STEP, "AIR4", my_Pro[2].STS_R_AIR.ToString());
            inf.WriteString(STEP, "AIR5", my_Pro[3].STS_L_AIR.ToString());
            inf.WriteString(STEP, "AIR6", my_Pro[3].STS_R_AIR.ToString());
            inf.WriteString(STEP, "AIR7", my_Pro[4].STS_L_AIR.ToString());
            inf.WriteString(STEP, "AIR8", my_Pro[4].STS_R_AIR.ToString());
            inf.WriteString(STEP, "AIR9", my_Pro[5].STS_L_AIR.ToString());
            inf.WriteString(STEP, "AIR10", my_Pro[5].STS_R_AIR.ToString());
            inf.WriteString(STEP, "AIR11", my_Pro[6].STS_L_AIR.ToString());
            inf.WriteString(STEP, "AIR12", my_Pro[6].STS_R_AIR.ToString());
        }

        /// <summary>
        /// 产品运转一次。二维码迁移一次
        /// </summary>
        public void RotateST()
        {
            my_Pro[6] = my_Pro[5];
            my_Pro[5] = my_Pro[4];
            my_Pro[4] = my_Pro[3];
            my_Pro[3] = my_Pro[2];

            my_Pro[2] = my_Pro[1];

            my_Pro[2].Model_Num_L = my_Pro[1].Model_Num_R;
            my_Pro[2].Model_Num_R = my_Pro[1].Model_Num_L;

            my_Pro[2].Cord_ST_L = my_Pro[1].Cord_ST_R;
            my_Pro[2].Cord_ST_R = my_Pro[1].Cord_ST_L;

            my_Pro[1] = my_Pro[0];
           
            
            SaveSTcord();
        }

        /// <summary>
        /// 产品运转一次。二维码迁移一次
        /// </summary>
        public void RotateST2()
        {
            my_Pro[6] = my_Pro[5];
            my_Pro[5] = my_Pro[4];
            my_Pro[4] = my_Pro[3];
            my_Pro[3] = my_Pro[2];

            my_Pro[2] = my_Pro[1];

            my_Pro[2].Model_Num_L = my_Pro[1].Model_Num_L;
            my_Pro[2].Model_Num_R = my_Pro[1].Model_Num_R;

            my_Pro[2].Cord_ST_L = my_Pro[1].Cord_ST_L;
            my_Pro[2].Cord_ST_R = my_Pro[1].Cord_ST_R;

            my_Pro[1] = my_Pro[0];


            SaveSTcord();
        }

    }
}
