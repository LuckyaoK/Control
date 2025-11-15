using CXPro001.myclass;
using CXPro001.setups;
using MyLib.Files;
using MyLib.SqlHelper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPro001.classes
{
    public class FlowMeter
    {
        public double flowMeterUp, flowMeterDown;

        public double leftFlowMeter,rightFlowMeter;

        public string leftResult,rightResult;



        public void SaveToDB_Pos(string cord,double _flowMeter,bool result)
        {
            
            if (cord.Length < 1)
            {
                Logger.Error($"流通量检测数据缺少二维码", 4);
                return;
            }//检测该二维码之前做过没有
            string sql2 = $"SELECT * from  FlowMeter  WHERE Cord = '{cord}'";

            int i2 = int.Parse(SQLHelper.ExecuteScalar(sql2));
            if (i2 > 0)
            {
                sql2 = $" delete FROM FlowMeter WHERE Cord = '{cord}'";
                int i3 = SQLHelper.Update(sql2);
                if (i3 > 0)
                {
                    Logger.Info($"从流通量检测数据库删除{cord}的数据成功", 4);
                }
                else
                {
                    Logger.Error($"从流通量检测数据库删除{cord}的数据失败", 4);
                }
            }

            string sql = "insert into FlowMeter( InsertTime,Cord,FlowMeter,Result)" +
                " values( @InsertTime,@Cord,@FlowMeter,@Result)";
               



            SqlParameter[] param = new SqlParameter[]
            {
                   new SqlParameter("@InsertTime", DateTime.Now),
                   new SqlParameter("@Cord",cord),
                   new SqlParameter("@FlowMeter", _flowMeter),
                   new SqlParameter("@Result", result)

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
        /// <summary>
        /// 导入参数-气密参数
        /// </summary>
        public FlowMeter LoadParmMeter()
        {
            string  filename = SysStatus.sys_dir_path + "\\product\\" + SysStatus.CurProductName + "\\FlowMeter.ini";// 配置文件路径;
            IniFile inf = new IniFile(filename);//确认路径是否存在，不存在则创建文件夹。
            //默认的是STEP1的参数
            string STEP = "上下限";
            flowMeterUp = inf.ReadDouble(STEP, "flowMeterUp", flowMeterUp);
            flowMeterDown = inf.ReadDouble(STEP, "flowMeterDown", flowMeterDown);

            return this;

        }
        /// <summary>
        /// 保存配置-高度参数
        /// </summary>
        public void SaveParmHigh(string up,string down)
        {
            string filename = SysStatus.sys_dir_path + "\\product\\" + SysStatus.CurProductName + "\\FlowMeter.ini";// 配置文件路径;
            IniFile inf = new IniFile(filename);//确认路径是否存在，不存在则创建文件夹。
            //默认的是STEP1的参数
            string STEP = "上下限";
            inf.WriteDouble(STEP, "flowMeterUp", StoD(up));
            inf.WriteDouble(STEP, "flowMeterDown", StoD(down));

        }
 
        private double StoD(string aa)
        {
            try
            {
                return Convert.ToDouble(aa);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return 999.9;
            }
        }

        public void init()
        {
            flowMeterUp=LoadParmMeter().flowMeterUp;
            flowMeterDown = LoadParmMeter().flowMeterDown;
        }
    }
}
