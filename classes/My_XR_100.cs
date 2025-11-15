using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CXPro001.myclass;
using MyLib.SqlHelper;
using NetWorkHelper;
namespace CXPro001.classes
{
  public   class My_XR_100
    {
        NetWorkHelper.TCP.ITcpClient my_client;

        public string str;
        public bool isCon=false;


        string Cord_Pos, mohao;

        public string Get_data()
        {
            string xx;
            if(str.Length>2)
            xx = str.Substring(0,str.Length-2);
            return str;
        }
        /// <summary>
        /// 设置二维码 及模具号
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        public void Set_Cord_Num(string str1,string str2)
        {
            Cord_Pos = str1;
            mohao = str2;
        }
        /// <summary>
        /// 设置句柄
        /// </summary>
        /// <param name="m"></param>
        public  void set(NetWorkHelper.TCP.ITcpClient m)
        {
            my_client=m;
        }
        /// <summary>
        ///连接
        /// </summary>
        public void Conn()
        {
            my_client.StartConnect();
        }
        /// <summary>
        /// 断开连接
        /// </summary>
        public void Close()
        {
            my_client.StopConnect();
        }
        /// <summary>
        /// 设置连接状态
        /// </summary>
        /// <param name="sts"></param>
        public void Set_Sts(bool sts)
        {
            isCon = sts;
        }
        /// <summary>
        /// 获取连接状态
        /// </summary>
        /// <returns></returns>
        public bool Get_Sts()
        {
            return isCon;
        }
        public void Clr_str()
        {
            str = "";
        }

       ////////////////////////////////////////////
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="data"></param>
        public void onReceve(byte [] data)
        {
            str = Encoding.UTF8.GetString(data);
        }

        /// <summary>
        /// 开始触发
        /// </summary>
        public void Send_On()
        {
            byte[] byteArray = new byte[5];
            byteArray[0] = 0x4c;
            byteArray[1] = 0x4f;
            byteArray[2] = 0x4e;
            byteArray[3] = 0x0d;
            byteArray[4] = 0x0a;
            my_client.SendData(byteArray);
            str = "";

        }
        /// <summary>
        /// 结束触发
        /// </summary>
        public void Send_Off()
        {
            byte[] byteArray = new byte[6];
            byteArray[0] = 0x4c;
            byteArray[1] = 0x4f;    
            byteArray[2] = 0x46;
            byteArray[3] = 0x46;
            byteArray[4] = 0x0d;
            byteArray[5] = 0x0a;
            my_client.SendData(byteArray);
        }

        /// <summary>
        ///读取错误
        /// </summary>
        public void Send_error()
        {
            byte[] byteArray = new byte[6];
            byteArray[0] = 0x45;
            byteArray[1] = 0x52;
            byteArray[2] = 0x52;
            byteArray[3] = 0x4f;
            byteArray[4] = 0x52;
            byteArray[5] = 0x0d;
            byteArray[6] = 0x0a;
            my_client.SendData(byteArray);
        }

        /// <summary>
        /// 查询二维码是否存在
        /// </summary>
        /// <returns></returns>
        public int search(string Cord_Pos)
        {
            string sql2 = $"SELECT count(*) from  Result_Hansler  WHERE Cord = '{Cord_Pos}'";
            int i2 = int.Parse(SQLHelper.ExecuteScalar(sql2));
            return i2;
        }
        /// <summary>
        /// 保存打码数据
        /// </summary>
        /// <param name="Cord_Pos"></param>
        /// <param name="mohao"></param>
        public void SavePosToDB(bool ResEnd)
        {

            string sql2 = $"SELECT count(*) from  Result_Hansler  WHERE Cord = '{Cord_Pos}'";
            int i2 = int.Parse(SQLHelper.ExecuteScalar(sql2));
            if (i2 > 0)
            {
                sql2 = $" delete FROM VoltageData_8740 WHERE Cord = '{Cord_Pos}'";
                int i3 = SQLHelper.Update(sql2);
                if (i3 > 0)
                {
                    Logger.Info($"从打码数据库删除{Cord_Pos}的数据成功", 5);
                }
                else
                {
                    Logger.Error($"从打码数据库删除{Cord_Pos}的数据失败", 5);
                }
            }


            //
            string sql = "insert into Result_Hansler( InsertTime,Cord,VehicleNumber,ModelNumber,ProductType,Result,CurResMess)" +
                " values( @InsertTime,@Cord,@VehicleNumber,@ModelNumber,@ProductType,@Result,@CurResMess)";


            SqlParameter[] param = new SqlParameter[]
            {
                   new SqlParameter("@InsertTime", DateTime.Now),
                   new SqlParameter("@Cord",Cord_Pos),
                   new SqlParameter("@VehicleNumber", " "),
                   new SqlParameter("@ModelNumber", mohao),
                   new SqlParameter("@ProductType", SysStatus.CurProductName),
                   new SqlParameter("@Result", ResEnd),               
                   new SqlParameter("@CurResMess",Cord_Pos),
             };
            int i = SQLHelper.Update(sql, param);
            if (i > 0)
            {
                Logger.Info("扫打扫结果写入数据库成功", 5);
            }
            else
            {
                Logger.Error("扫打扫写入数据库失败", 5);
            }
        }

    }
}
