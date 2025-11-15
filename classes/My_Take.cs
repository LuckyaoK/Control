using CXPro001.myclass;
using MyLib.SqlHelper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPro001.classes
{
    public class My_Take
    {
        public string Cord_Pos="1";
        public int mohao;
        public bool ResEnd, rv, rh1, rp1, rh2, rp2, rl, ra;

        /// <summary>
        /// 设置二维码
        /// </summary>
        /// <param name="str1"></param>
        public void Set_Cord(string str1)
        {
            Cord_Pos = str1;

        }
        /// <summary>
        /// 设置模号
        /// </summary>
        /// <param name="str1"></param>
        public void Set_M(int str1)
        {
            mohao = str1;

        }
        /// <summary>
        /// 设置结果 1结果2耐压 3高度4位置5高度6位置7打码
        /// </summary>
        /// <param name="str1"></param>
        public void Set_ResEnd(bool b1, bool b2, bool b3, bool b4, bool b5, bool b6, bool b7, bool b8)
        {
            ResEnd = b1;
            rv = b2;
            rh1 = b3;
            rp1 = b4;
            rh2 = b5;
            rp2 = b6;
            rl = b7;
            ra = b8;
        }

        public void SavePosToDB( )
        {

            string sql2 = $"SELECT count(*) from  EndResultDB  WHERE Cord = '{Cord_Pos}'";
            int i2 = int.Parse(SQLHelper.ExecuteScalar(sql2));
            if (i2 > 0)
            {
                sql2 = $" delete FROM EndResultDB WHERE Cord = '{Cord_Pos}'";
                int i3 = SQLHelper.Update(sql2);
                if (i3 > 0)
                {
                    Logger.Info($"从总结删除{Cord_Pos}的数据成功", 5);
                }
                else
                {
                    Logger.Error($"从总结删除{Cord_Pos}的数据失败", 5);
                }
            }

            //
            string sql = "insert into EndResultDB( InsertTime,Cord,ModelNumber,ProductType,ResultEnd,ResultVoltage,ResultHigh1,ResultPostion1,ResultHigh2,ResultPostion2,ResultLaser,ResultAir)" +
                " values( @InsertTime,@Cord,@ModelNumber,@ProductType,@ResultEnd,@ResultVoltage,@ResultHigh1,@ResultPostion1,@ResultHigh2,@ResultPostion2,@ResultLaser,@ResultAir)";


            SqlParameter[] param = new SqlParameter[]
            {
                   new SqlParameter("@InsertTime", DateTime.Now),
                   new SqlParameter("@Cord",Cord_Pos),
                   new SqlParameter("@ModelNumber",mohao),
                   new SqlParameter("@ProductType", SysStatus.CurProductName),
                   new SqlParameter("@ResultEnd", ResEnd),
                   new SqlParameter("@ResultVoltage",rv),
                   new SqlParameter("@ResultHigh1", rh1),
                   new SqlParameter("@ResultPostion1",rp1),
                   new SqlParameter("@ResultHigh2", rh2),
                   new SqlParameter("@ResultPostion2",rp2),
                   new SqlParameter("@ResultLaser", rl),
                    new SqlParameter("@ResultAir", ra),

             };
            int i = SQLHelper.Update(sql, param);
            if (i > 0)
            {
                Logger.Info("结果写入数据库成功", 5);
            }
            else
            {
                Logger.Error("结果写入数据库失败", 5);
            }
        }
    }
}
