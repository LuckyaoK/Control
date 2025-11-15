using CXPro001.myclass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GTN.mc;
namespace CXPro001.gl
{
    public  class My_io
    {
        //IO备注
       public   string[,] str_di = new string[5, 32];
       public  string[,] str_do = new string[5, 16];

        //IO状态
       public bool[,] m_input = new bool[5, 32];
       public bool[,] m_output = new bool[5, 16];
        /// <summary>
        /// 模块数量
        /// </summary>
        public int sNum;

        /// <summary>
        /// 设置模块数量
        /// </summary>
        /// <param name="x"></param>
        public void Set(int x)
        {
            sNum = x;
        }
        /// <summary>
        /// 初始化备注
        /// </summary>
       public  void  Init()
        {         
           // for (int i = 0; i < 5; i++)
           //     for (int j = 0; j < 32; j++)
             //       str_di[i, j] = "备用";
            
            #region 模块1
            str_di[0,0] = "启动";
            str_di[0,1] = "急停";
            str_di[0, 2] = "安全门";
            str_di[0, 3] = "安全光栅";
            str_di[0, 4] = "上料报警";
            str_di[0, 5] = "气压报警";
            str_di[0, 6] = "备用";
            str_di[0, 7] = "备用";

            str_di[0, 8] = "取料升降气缸1回";
            str_di[0, 9] = "取料升降气缸1出";
            str_di[0, 10] = "取料旋转位置1";
            str_di[0, 11] = "取料旋转位置2";
            str_di[0, 12] = "取料加长关";
            str_di[0, 13] = "取料气爪1开";
            str_di[0, 14] = "取料加长开";
            str_di[0, 15] = "取料气爪2开";
            //////////////////////////////////////
            str_do[0, 0] = "继电器-灯";
            str_do[0, 1] = "继电器-大风扇";
            str_do[0, 2] = "继电器-传送带";
            str_do[0, 3] = "继电器-报警";
            str_do[0, 4] = "备用";
            str_do[0, 5] = "继电器-安全门锁";
            str_do[0, 6] = "继电器-安全继电器";
            str_do[0, 7] = "备用--气压";


            str_do[0, 8] = "取料升降关";
            str_do[0, 9] = "取料升降开";
            str_do[0, 10] = "取料旋转关";
            str_do[0, 11] = "取料旋转开";
            str_do[0, 12] = "取料气爪关";
            str_do[0, 13] = "取料气爪开";
            str_do[0, 14] = "取料加长关";
            str_do[0, 15] = "取料加长开";
            #endregion
            #region 模块2
            str_di[1, 0] = "放料台气缸开";
            str_di[1, 1] = "放料台气缸关";
            str_di[1, 2] = "1-1#有料";
            str_di[1, 3] = "1-2#有料";
            str_di[1, 4] = "横移下降关";
            str_di[1, 5] = "横移下降开";
            str_di[1, 6] = "横移前进回";
            str_di[1, 7] = "横移前进出";

            str_di[1, 8] = "耐压后推气缸回";
            str_di[1, 9] = "耐压后推气缸出";
            str_di[1, 10] = "耐压下降气缸回";
            str_di[1, 11] = "耐压下降气缸出";
            str_di[1, 12] = "侧CCD下压气缸回";
            str_di[1, 13] = "侧CCD下压气缸出";
            str_di[1, 14] = "CCD1前进关";
            str_di[1, 15] = "CCD1前进开";
            //////////////////////////////////////
            str_do[1, 0] = "放料台气缸关";
            str_do[1, 1] = "放料台气缸开";
            str_do[1, 2] = "横移下降关";
            str_do[1, 3] = "横移下降开";
            str_do[1, 4] = "横移前进关";
            str_do[1, 5] = "横移前进开";
            str_do[1, 6] = "耐压后推气缸关";
            str_do[1, 7] = "耐压后推气缸开";

            str_do[1, 8] = "耐压下降气缸关";
            str_do[1, 9] = "耐压下降气缸开";
            str_do[1, 10] = "侧CCD下压气缸关";
            str_do[1, 11] = "侧CCD下压气缸开";
            str_do[1, 12] = "侧CCD位置气缸关";
            str_do[1, 13] = "侧CCD位置气缸开";
            str_do[1, 14] = "顶CCD下压气缸关";
            str_do[1, 15] = "顶CCD下压气缸开";
            #endregion
            #region 模块3
            str_di[2, 0] = "顶CCD下压气缸关";
            str_di[2, 1] = "顶CCD下压气缸开";
            str_di[2, 2] = "顶CCD后推气缸关";
            str_di[2, 3] = "顶CCD后推气缸开";
            str_di[2, 4] = "备用";
            str_di[2, 5] = "备用";
            str_di[2, 6] = "备用";
            str_di[2, 7] = "备用";

            str_di[2, 8] = "备用";
            str_di[2, 9] = "分料气爪1-1关";
            str_di[2, 10] = "分料气爪1-1开";
            str_di[2, 11] = "分料气爪1-2关";
            str_di[2, 12] = "分料气爪1-2开";
            str_di[2, 13] = "分料升降气缸关";
            str_di[2, 14] = "分料升降气缸开";
            str_di[2, 15] = "备用";
            /////////////////////////////
            str_do[2, 0] = "顶CCD后推气缸关";
            str_do[2, 1] = "顶CCD后推气缸开";
            str_do[2, 2] = "分料下降气缸关";
            str_do[2, 3] = "分料下降气缸开";
            str_do[2, 4] = "分料气爪1关";
            str_do[2, 5] = "分料气爪1开";
            str_do[2, 6] = "分料气爪2关";
            str_do[2, 7] = "分料气爪2开";

            str_do[2, 8] = "备用";
            str_do[2, 9] = "备用";
            str_do[2, 10] = "备用";
            str_do[2, 11] = "备用";
            str_do[2, 12] = "备用";
            str_do[2, 13] = "备用";
            str_do[2, 14] = "备用";
            str_do[2, 15] = "备用";
            #endregion
            #region 模块4
            str_di[3, 0] = "气压右下压开";
            str_di[3, 1] = "气压左下压开";
            str_di[3, 2] = "气压后推开";
            str_di[3, 3] = "气压后推关";
            str_di[3, 4] = "气压右OK";
            str_di[3, 5] = "气压左OK";
            str_di[3, 6] = "备用";
            str_di[3, 7] = "备用";

            str_di[3, 8] = "旋转位置关";
            str_di[3, 9] = "旋转位置开";
            str_di[3, 10] = "气爪1-1开";
            str_di[3, 11] = "气爪1-2开";
            str_di[3, 12] = "气爪2-1开";
            str_di[3, 13] = "气爪2-2开";
            str_di[3, 14] = "气爪3-1开";
            str_di[3, 15] = "气爪3-2开";
            //////////////////////////////////
            str_do[3, 0] = "旋转气缸2关";
            str_do[3, 1] = "旋转气缸2开";
            str_do[3, 2] = "气爪1 关";
            str_do[3, 3] = "气爪1 开";
            str_do[3, 4] = "气爪2-3关";
            str_do[3, 5] = "气爪2-3开";
            str_do[3, 6] = "气爪4-5 关";
            str_do[3, 7] = "气爪4-5 开";

            str_do[3, 8] = "气压后推开";
            str_do[3, 9] = "气压下压开";
            str_do[3, 10] = "通气1";
            str_do[3, 11] = "通气2";
            str_do[3, 12] = "气压后推关";
            str_do[3, 13] = "气压下压关";
            str_do[3, 14] = "备用";
            str_do[3, 15] = "备用";
            #endregion
            #region 模块5
            str_di[4, 0] = "气爪4-1开";
            str_di[4, 1] = "气爪4-2开";
            str_di[4, 2] = "气爪5-1开";
            str_di[4, 3] = "气爪5-2开";
            str_di[4, 4] = "备用";
            str_di[4, 5] = "备用";
            str_di[4, 6] = "备用";
            str_di[4, 7] = "备用";

            str_di[4, 8] = "1-1#有料  耐压";
            str_di[4, 9] = "1-2#有料";
            str_di[4, 10] = "2-1#有料";
            str_di[4, 11] = "2-2#有料";
            str_di[4, 12] = "3-1#有料";
            str_di[4, 13] = "3-2#有料";
            str_di[4, 14] = "4-1#有料";
            str_di[4, 15] = "4-2#有料";

            str_di[4, 16] = "5-1#有料";
            str_di[4, 17] = "5-2#有料";
            str_di[4, 18] = "传送带位置1";
            str_di[4, 19] = "传送带位置2";
            str_di[4, 20] = "备用";
            str_di[4, 21] = "备用";
            str_di[4, 22] = "备用";
            str_di[4, 23] = "备用";

         
            #endregion
        }

        /// <summary>
        /// 检查IO输出是否符合条件
        /// </summary>
        /// <param name="station"></param>
        /// <param name="num"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Check(short station, short num, byte value)
        {
            bool re = true;
            //////////////////////////////////////
            if (station == 0 && num == 0 && value == 1)
                if (m_input[1, 3])
                {
                    Logger.Write(Logger.InfoType.Info, "气缸位置干涉输出停止");
                    return false;
                   
                }
            if (station == 3 && num == 1 && value == 1)
                if (m_input[1, 7])
                {
                    Logger.Write(Logger.InfoType.Info, "旋转干涉");
                    return false;

                }
            /////////////////////////////////////////
            return re;
        }
        /// <summary>
        /// IO输出
        /// </summary>
        /// <param name="station">站号</param>
        /// <param name="num">IO号</param>
        /// <param name="value">只</param>
        public  void Set_Do(short station, short num, byte value)
        {
            if(Check(station,num,value))//符合条件进行输出
            GTN.glink.GT_SetGLinkDoBit(station, num, value);
            
        }
        /// <summary>
        /// 获取所以模块的IO状态
        /// </summary>
        public void Get_All()
        {
            //获取IO状态
            for(short i=0;i<sNum;i++)
            {
                bool[] di_b = Get_Di(i);
                for (int j= 0; j < 32; j++)
                {
                  
                      m_input[i,j]= di_b[j];                   
                }

                bool[] do_b = Get_Do(i);
                for (int j = 0; j < 16; j++)
                {
                    m_output[i, j] = do_b[j];                     
                }
            }

        }
        /// <summary>
        /// 获取输入
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        public  bool[] Get_Di(short station)
        {
            bool[] temp = new bool[32];
            short rtn;
            byte[] bDiData = new byte[4];
            rtn = GTN.glink.GT_GetGLinkDi(station, 0, out bDiData[0], 4);
            for (int i = 0; i < 8; i++)
            {
                temp[i] = bits(bDiData[0], i);
                temp[i + 8] = bits(bDiData[1], i);
                temp[i + 16] = bits(bDiData[2], i);
                temp[i + 24] = bits(bDiData[3], i);
            }
            return temp;

        }
        /// <summary>
        /// 获取输出点
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        public  bool[] Get_Do(short station)
        {
            bool[] temp = new bool[16];
            short rtn;
            byte[] bDoData = new byte[2];
            rtn = GTN.glink.GT_GetGLinkDo(station, 0, ref bDoData[0], 2);
            for (int i = 0; i < 8; i++)
            {
                temp[i] = bits(bDoData[0], i);
                temp[i + 8] = bits(bDoData[1], i);
               
            }
            return temp;

        }
        /// <summary>
        /// 位处理
        /// </summary>
        /// <param name="x"></param>
        /// <param name="num">0-7</param>
        /// <returns></returns>
        public  bool bits(byte x, int num)
        {
            int temp;

            if (num == 0)
            {
                temp = x & 1;
                return temp == 1 ? true : false;
            }
            if (num == 1)
            {
                temp = x & 2;
                return temp == 2 ? true : false;
            }
            if (num == 2)
            {
                temp = x & 4;
                return temp == 4 ? true : false;
            }
            if (num == 3)
            {
                temp = x & 8;
                return temp == 8 ? true : false;
            }
            if (num == 4)
            {
                temp = x & 16;
                return temp == 16 ? true : false;
            }
            if (num == 5)
            {
                temp = x & 32;
                return temp == 32 ? true : false;
            }
            if (num == 6)
            {
                temp = x & 64;
                return temp == 64 ? true : false;
            }
            if (num == 7)
            {
                temp = x & 128;
                return temp == 128 ? true : false;

            }
            if (num == 8)
            {
                temp = x & 256;
                return temp == 256 ? true : false;

            }
            if (num == 9)
            {
                temp = x & 512;
                return temp == 512 ? true : false;

            }

            if (num == 10)
            {
                temp = x & 1024;
                return temp == 1024 ? true : false;

            }
            if (num == 11)
            {
                temp = x & 2048;
                return temp == 2048 ? true : false;

            }
            return false;
        }
        /// <summary>
        /// 设置位
        /// </summary>
        /// <param name="x"></param>
        /// <param name="num"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public  byte setbyte(byte x, int num, bool val)
        {
            bool t = bits(x, num);
            if (t == val)
                return x;

            if (val)//+
            {
                switch (num)
                {
                    case 0:
                        x = (byte)(x + 1);
                        break;
                    case 1:
                        x = (byte)(x + 2);
                        break;
                    case 2:
                        x = (byte)(x + 4);
                        break;
                    case 3:

                        x = (byte)(x + 8);
                        break;
                    case 4:
                        x = (byte)(x + 16);
                        break;
                    case 5:
                        x = (byte)(x + 32);
                        break;
                    case 6:
                        x = (byte)(x + 64);
                        break;
                    case 7:
                        x = (byte)(x + 128);
                        break;
                }
            }
            else//-
            {
                switch (num)
                {
                    case 0:
                        x = (byte)(x - 1);
                        break;

                    case 1:
                        x = (byte)(x - 2);
                        break;
                    case 2:
                        x = (byte)(x - 4);
                        break;
                    case 3:

                        x = (byte)(x - 8);
                        break;
                    case 4:
                        x = (byte)(x - 16);
                        break;
                    case 5:
                        x = (byte)(x - 32);
                        break;
                    case 6:
                        x = (byte)(x - 64);
                        break;
                    case 7:
                        x = (byte)(x - 128);
                        break;
                }
            }
            return x;

        }
    }
}
