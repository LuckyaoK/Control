using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace GoogolMotion
{
    public class CAxisPrm
    {
        public int cardnum = 0;                         //0开始
        public int axisnum = 0;                         //从0开始
    }
    public class CAxisMap
    {
        public int adress = 0;//在参数列表中的位置 0开始
        public int dataMap = 0;//在数据区中的位置 从0开始
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inadress">轴参数列表中的位置</param>
        public CAxisMap(int inadress)
        {
            adress = inadress;
        }
        public string Get_AxisName()
        {
            return "";
        }
        public string Get_Note()
        {
            return "";
        }
    }
    public interface IInitModel
    {
        bool Init();                     //初始化
        bool UnInit();                   //反初始化
    }
    public interface IMotionPart1
    {
        int MC_PowerOff(CAxisMap axis);
        int MC_Power(CAxisMap axis);    //开电
        //int MC_Reset(CAxisMap axis);    //复位
        //int MC_Home(CAxisMap axis);     //回零
        //int MC_MoveAbs(CAxisMap axis, double tpos, double beilv = 0.5);
        //int MC_MoveAdd(CAxisMap axis, double dist, double beilv = 0.5);
        //int MC_MoveJog(CAxisMap axis, double beilv = 0.5);
        //int MC_EStop(CAxisMap axis);
        //int MC_EStopAll();
        //int MC_SetPos(CAxisMap axis, double pos);
    } /// <summary>



    public class CGMotion : IMotionPart1  //IMotionPart2, IIoPart1, IInitModel, IStartStopModel, IFreshModel
    {

        public bool initOk = false;
        private System.Threading.Thread runThread = null;       //用来刷新IO的
        //私有变量
        private short cardNum = 0;
        public bool Init()
        {
            try
            {
                short rtn = 0;
                rtn += GTN.mc.GTN_Open(5, 2);
                rtn += GTN.mc.GTN_Reset(cardNum);
                rtn += GTN.mc.GTN_LoadConfig(cardNum, @"GTS400.cfg");
                rtn += GTN.mc.GTN_ClrSts(cardNum, 1, 4);
                rtn += GTN.mc.GTN_ZeroPos(cardNum, 1, 4);
                //if (rtn == 0)
                //{
                //    runThread = new System.Threading.Thread(Run);
                //    runThread.IsBackground = true;
                //    runThread.Start();
                //    return initOk = true;
                //}
                return initOk = false;
            }
            catch (Exception)
            {
                return initOk = false;
            }

        }
        public bool UnInit()
        {
            try
            {
                //停止刷新
                if (runThread != null && runThread.IsAlive)
                {
                    runThread.Abort();
                    Thread.Sleep(5);
                    runThread = null;
                }
                short rtn = 0;
                rtn += GTN.mc.GTN_Reset(cardNum);
                rtn += GTN.mc.GTN_Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        int IMotionPart1.MC_Power(CAxisMap axis)
        {
            return GTN.mc.GTN_Open((short)CAxisPrms.prms[axis.adress].cardnum, (short)CAxisPrms.prms[axis.adress].axisnum);
        }
        int IMotionPart1.MC_PowerOff(CAxisMap axis)
        {
            return GTN.mc.GTN_AxisOff((short)CAxisPrms.prms[axis.adress].cardnum, (short)CAxisPrms.prms[axis.adress].axisnum);
        }
        /// <summary>
        /// 所有轴的参数
        /// </summary>
        public static class CAxisPrms
        {
            public static bool readok = false;//读取成功
            public static List<CAxisPrm> prms = new List<CAxisPrm>();//原始数据中出现一个轴 就必须在这里有一个配置 
        }
       
    }

}