using CXPro001.myclass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static GTN.mc;
namespace CXPro001.gl
{
    public class My_Motion
    {

  
        /// <summary>
        /// 反馈
        /// </summary>
        public short rtn;

        /// <summary>
        /// 模块数量
        /// </summary>
        public byte slavenum;
        /// <summary>
        /// 电机数量
        /// </summary>
        public short sMotionNum;
        /// <summary>
        /// 绝对编码器位置
        /// </summary>
        public int[] m_EcatEncPos = new int[10];
        /// <summary>
        /// 编码器位置
        /// </summary>
        public double[] m_EncPos = new double[10];
        /// <summary>
        /// 规划位置
        /// </summary>
        public double[] m_PrfPos = new double[10];
        /// <summary>
        /// 电机状态
        /// </summary>
        public int[] m_sts = new int[10];
        /// <summary>
        /// 电机正限位
        /// </summary>
        public bool[] m_z_limt = new bool[10];
        /// <summary>
        /// 电机负限位
        /// </summary>
        public bool[] m_f_limt = new bool[10];
        /// <summary>
        /// 伺服使能
        /// </summary>
        public bool[] m_serv_on = new bool[10];
        /// <summary>
        /// 伺服正在运行
        /// </summary>
      
        public bool[] m_serv_run = new bool[10];
        /// <summary>
        /// 伺服软限位负
        /// </summary>
        private int[] m_Soft_limt_f = new int[10];
        /// <summary>
        /// 伺服软限位正
        /// </summary>
        private int[] m_Soft_limt_z = new int[10];

        /// <summary>
        /// 单圈当量
        /// </summary>
        private const int single = 262144;//单圈脉冲
        /// <summary>
        /// 加速度
        /// </summary>
        private const double acc = single / 15;
        /// <summary>
        /// 减速度
        /// </summary>
        private const double dec = single / 6;

        /// <summary>
        /// 1号电机是否在保护区
        /// </summary>
        public bool is_protect_axis_1;
        /// <summary>
        /// 5号电机是否在保护区
        /// </summary>
        public bool is_protect_axis_5;

        /// <summary>
        /// 设置规划位置
        /// </summary>
        public int[] m_Set_PrfPos = new int[10];
        /// <summary>
        /// 反馈电机是否在运动
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>

        public bool Check_Run(short axis)
        {
            return m_serv_run[axis];

        }
        /// <summary>
        /// 判断是否到
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public bool Move_Finsh(short axis)
        {
            if (!m_serv_run[axis])
            {
                if (Math.Abs(m_EncPos[axis] - m_Set_PrfPos[axis]) < 300)
                    return true;
            }
            
                return false;
           
          
        }
        /// <summary>
        /// 返回电机在哪个位置点
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public int Check_Now_Pos(short axis)
        {
            int x=-1;
            int[] lpos = new int[6];
            if(axis==1)
            {
                lpos = SysStatus.Axis_1_Pos;
            }
            if (axis ==2)
            {
                lpos = SysStatus.Axis_2_Pos;
            }
            if (axis ==3)
            {
                lpos = SysStatus.Axis_3_Pos;
            }
            if (axis ==4)
            {
                lpos = SysStatus.Axis_4_Pos;
            }
            if (axis ==5)
            {
                lpos = SysStatus.Axis_5_Pos;
            }
            if (axis ==6)
            {
                lpos = SysStatus.Axis_6_Pos;
            }

            for (int i = 0; i < 6; i++)
            {
                if (Math.Abs(m_EcatEncPos[axis] - lpos[i]) < 300)
                {
                    x = i;
                    break;
                }
            }
           
            return x;

        }

        /// <summary>
        /// 走上次规划电机移动
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="index"></param>
        public void Move_Pro(short axis)
        {
            int pos = 0;
            double sp = SysStatus.Axis_SP[axis - 1];


            pos=m_Set_PrfPos[axis] ;//记录设定目标合位置
            P2P(axis, pos, sp);
        }

        /// <summary>
        /// 电机移动
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="index"></param>
        public void Move(short axis,int index)
        {
            int pos=0;
            double sp = SysStatus.Axis_SP[axis - 1];
            if (axis == 1)           
                pos = SysStatus.Axis_1_Pos[index];             
             if (axis == 2)
                pos = SysStatus.Axis_2_Pos[index];          
            if (axis == 3)
                pos = SysStatus.Axis_3_Pos[index];
            if (axis == 4)
                pos = SysStatus.Axis_4_Pos[index];
            if (axis == 5)
                pos = SysStatus.Axis_5_Pos[index];
            if (axis == 6)
                pos = SysStatus.Axis_6_Pos[index];

            m_Set_PrfPos[axis]=pos;//记录设定目标合位置
            P2P(axis,pos, sp);
        }
        /// <summary>
        /// 输出位 +偏移
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="index"></param>
        /// <param name="offiset"></param>
        public void Move(short axis, int index,int offiset)
        {
            int pos = 0;
            double sp = SysStatus.Axis_SP[axis - 1];
            if (axis == 1)
                pos = SysStatus.Axis_1_Pos[index];

            if (axis == 2)
                pos = SysStatus.Axis_2_Pos[index];

            if (axis == 3)
                pos = SysStatus.Axis_3_Pos[index];
            if (axis == 4)
                pos = SysStatus.Axis_4_Pos[index];
            if (axis == 5)
                pos = SysStatus.Axis_5_Pos[index];
            if (axis == 6)
                pos = SysStatus.Axis_6_Pos[index];
            pos = pos + offiset;

            m_Set_PrfPos[axis] = pos;//记录设定目标合位置
            P2P(axis, pos, sp);
        }
        


        /// <summary>
        /// 点位移动
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="pos"></param>
        /// <param name="sp"></param>
        public void P2P(short axis, int pos, double sp)
        {
          
            GTN_ClrSts(1, axis, 1);
            if (m_serv_on[axis] == false)
            {
                rtn = GTN_AxisOn(1, axis);
                Thread.Sleep(100);
            //  Pos_Uni(axis);
            
            }
            if (sp > 100)
                sp = 40;
            double ss = sp * single / 1000;//每秒转的脉冲量

            GTN.mc.TTrapPrm trap;
            rtn = GTN.mc.GTN_PrfTrap(1, axis);
            rtn = GTN.mc.GTN_GetTrapPrm(1, axis, out trap);
            trap.acc = acc;
            trap.dec = dec;
            trap.smoothTime = 50;
            rtn = GTN.mc.GTN_SetTrapPrm(1, axis, ref trap);
            rtn = GTN.mc.GTN_SetVel(1, axis, ss);
            rtn = GTN.mc.GTN_SetPos(1, axis, pos);
            rtn = GTN.mc.GTN_Update(1, 1 << (axis - 1));
        }
        /// <summary>
        /// 打开运动卡
        /// </summary>
        public short Open()
        {
            short rtn;
            rtn = GTN.mc.GTN_Open(5, 1);                //开卡
            return rtn;
        }
        /// <summary>
        /// 关闭卡
        /// </summary>
        public short Close()
        {
            short rtn;
            rtn = GTN.mc.GTN_Close();
            return rtn;
        }
        /// <summary>
        /// 初始化模块
        /// </summary>
        public short Init_Mod()
        {
            short rtn;

            rtn = GTN.glink.GT_GLinkInit(0);        //初始化模块
            if (rtn == 0)
            {
                rtn = GTN.glink.GT_GetGLinkOnlineSlaveNum(out slavenum);
                return slavenum;

            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// 导入电机配置文件
        /// </summary>
        /// <returns></returns>
        public short LoadEnCatFile()
        {
            rtn = GTN.mc.GTN_TerminateEcatComm(1);

            rtn = GTN.mc.GTN_InitEcatComm(1);
            return rtn;
        }
        /// <summary>
        /// 启动电机监听
        /// </summary>
        /// <returns></returns>
        public short StartEcat()
        {
            //   short status;
            //  do
            //  {
            //     rtn = GTN_IsEcatReady(1,out status);
            //  } while (status != 1 || rtn != 0);


            rtn = GTN.mc.GTN_StartEcatComm(1);
            rtn = GTN_Reset(1);
            if (rtn == 0)
                return GetEcatSlaves();
            else
                return rtn;


        }

        /// <summary>
        ///  获取电机数量
        /// </summary>
        private short GetEcatSlaves()
        {
            short sIONum;
            rtn = GTN.mc.GTN_GetEcatSlaves(1, out sMotionNum, out sIONum);

            return sMotionNum;

        }
        /// <summary>
        /// 电机使能
        /// </summary>
        /// <param name="axis"></param>
        public void En(short axis)
        {
            if (axis > sMotionNum)
                return;
           
            rtn = GTN_AxisOn(1, axis);
           Pos_Uni(axis);

        }
        /// <summary>
        /// 电机去使能
        /// </summary>
        /// <param name="axis"></param>
        public void NotEN(short axis)
        {
            if (axis > sMotionNum)
                return;
            GTN_AxisOff(1, axis);
        }

        /// <summary>
        /// 位置同一
        /// </summary>
        /// <param name="axis"></param>
        public void Pos_Uni(short axis)
        {
            if (axis > sMotionNum)
                return;
            int pos;

            rtn=GTN_GetEcatEncPos(1, axis, out pos);
            rtn = GT_SetPrfPos(axis, pos);
            rtn = GTN_SetEncPos(1, axis, pos);
           
            int sss = 1 << (axis - 1);
            rtn = GTN_SynchAxisPos(1, sss);//位置同步
        }
        /// <summary>
        /// 获取轴的位置及状态
        /// </summary>
        public void Get_Pos()
        {
            int pos;
            double dpos;
            UInt32 pClock;
            int lAxisStatus;
            for (int i = 1; i <= sMotionNum; i++)
            {
                short axis = short.Parse(i.ToString());
                rtn = GTN_GetEcatEncPos(1, axis, out pos);

                m_EcatEncPos[i] = pos;

                GTN_GetEncPos(1, axis, out dpos, 1, out pClock);
                m_EncPos[i] = dpos;
                GTN_GetPrfPos(1, axis, out dpos, 1, out pClock);
                m_PrfPos[i] = dpos;

                GTN_GetSts(1, axis, out lAxisStatus, 1, out pClock);
                m_sts[i] = lAxisStatus;

                if ((lAxisStatus & 0x20) > 0)
                    m_z_limt[i] = true;
                else
                    m_z_limt[i] = false;
                if ((lAxisStatus & 0x40) > 0)
                    m_f_limt[i] = true;
                else
                    m_f_limt[i] = false;

                if ((lAxisStatus & 0x200) > 0)
                    m_serv_on[i] = true;
                else
                    m_serv_on[i] = false;

                if ((lAxisStatus & 0x400) > 0)
                    m_serv_run[i] = true;
                else
                    m_serv_run[i] = false;
            }


            if (m_EcatEncPos[5] >= SysStatus.Axis_Protect_F[4] && m_EcatEncPos[5] <= SysStatus.Axis_Protect_Z[4])
                is_protect_axis_5 = true;            
            else
                is_protect_axis_5 = false;

            if (m_EcatEncPos[1] >= SysStatus.Axis_Protect_F[0] && m_EcatEncPos[1] <= SysStatus.Axis_Protect_Z[0])
                is_protect_axis_1 = true;
            else
                is_protect_axis_1 = false;
        }
        /// <summary>
        /// 设置软件限位
        /// </summary>
        /// <param name="a"></param>
        /// <param name="z"></param>
        /// <param name="f"></param>
        public void Set_SoftLimt(short axis, int z, int f)
        {
            if (axis > sMotionNum)
                return;
            rtn = GTN_SetSoftLimit(1, axis, z, f);

           rtn= GTN_SetSoftLimitMode(1, axis, 1);
        }

     
        /// <summary>
        /// 写入软限位值
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="z"></param>
        /// <param name="f"></param>
        public void Set_Limt(short axis, int z, int f)
        {
            m_Soft_limt_f[axis] = f;
            m_Soft_limt_z[axis] = z;
        }

        public void jog(short axis,double sp)
        {
            
            GTN_ClrSts(1, axis, 1);

            if (m_serv_on[axis] == false)
            {
                rtn = GTN_AxisOn(1, axis);
               
                Thread.Sleep(100);
                Pos_Uni(axis);
            }
            if (sp > 100)
                sp = 100;
            double ss = sp * single / 1000;//每秒转的脉冲量


            GTN.mc.TJogPrm jog;
            rtn = GTN.mc.GTN_PrfJog(1, axis);
            rtn = GTN.mc.GTN_GetJogPrm(1, axis, out jog);
            jog.acc = acc;
            jog.dec = dec;
            // 设置 Jog 运动参数
       
            rtn = GTN.mc.GTN_SetJogPrm(1, axis, ref jog);
            rtn = GTN.mc.GTN_SetVel(1, axis, ss);
            rtn = GTN.mc.GTN_Update(1, 1 << (axis - 1));
        }

        /// <summary>
        /// 停止所有电机
        /// </summary>
        public void Stop()
        {
            rtn = GTN.mc.GTN_Stop(1, 0xff, 0xff);
        }
        /// <summary>
        /// 停止单电机
        /// </summary>
        /// <param name="axis"></param>
        public void Stop(int axis)
        {
            rtn = GTN.mc.GTN_Stop(1, 1 << (axis - 1), 0xff);
        }
    }
}
