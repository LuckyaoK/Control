using MyLib.Files;
using MyLib.Sys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXPro001.setups
{
    /// <summary>
    /// 铆点温度流量类
    /// </summary>
    public class ThermSet
    {
        #region 理论值
        /// <summary>
        /// 理论温度值1
        /// </summary>
        public double L1;
        /// <summary>
        /// 理论温度值2
        /// </summary>
        public double L2;
        /// <summary>
        /// 理论温度值3
        /// </summary>
        public double L3;
        /// <summary>
        /// 理论温度值4
        /// </summary>
        public double L4;
        /// <summary>
        /// 理论温度值5
        /// </summary>
        public double L5;
        /// <summary>
        /// 理论流量值1
        /// </summary>
        public double L6;
        /// <summary>
        /// 理论流量值2
        /// </summary>
        public double L7;
        /// <summary>
        /// 理论流量值3
        /// </summary>
        public double L8;
        /// <summary>
        /// 理论流量值4
        /// </summary>
        public double L9;
        /// <summary>
        /// 理论流量值5
        /// </summary>
        public double L10;
        #endregion
        #region 上限值
        /// <summary>
        /// 上限值1
        /// </summary>
        public double UP1;
        /// <summary>
        /// 上限值2
        /// </summary>
        public double UP2;
        /// <summary>
        /// 上限值3
        /// </summary>
        public double UP3;
        /// <summary>
        /// 上限值4
        /// </summary>
        public double UP4;
        /// <summary>
        /// 上限值5
        /// </summary>
        public double UP5;
        /// <summary>
        /// 上限值6
        /// </summary>
        public double UP6;
        /// <summary>
        /// 上限值7
        /// </summary>
        public double UP7;
        /// <summary>
        /// 上限值8
        /// </summary>
        public double UP8;
        /// <summary>
        /// 上限值9
        /// </summary>
        public double UP9;
        /// <summary>
        /// 上限值10
        /// </summary>
        public double UP10;
        #endregion
        #region 下限值
        /// <summary>
        /// 下限值1
        /// </summary>
        public double DW1;
        /// <summary>
        /// 下限值2
        /// </summary>
        public double DW2;
        /// <summary>
        /// 下限值3
        /// </summary>
        public double DW3;
        /// <summary>
        /// 下限值4
        /// </summary>
        public double DW4;
        /// <summary>
        /// 下限值5
        /// </summary>
        public double DW5;
        /// <summary>
        /// 下限值6
        /// </summary>
        public double DW6;
        /// <summary>
        /// 下限值7
        /// </summary>
        public double DW7;
        /// <summary>
        /// 下限值8
        /// </summary>
        public double DW8;
        /// <summary>
        /// 下限值9
        /// </summary>
        public double DW9;
        /// <summary>
        /// 下限值10
        /// </summary>
        public double DW10;
        #endregion
        /// <summary>
        /// 加载参数
        /// </summary>
        public void LoadParam()
        {
            string path1 = $"{Path.GetFullPath("..")}\\product\\{SysStatus.CurProductName.Trim()}\\ThermSet.ini";// 配置文件路径
            IniFile inf = new IniFile(path1);//确认路径是否存在，不存在则创建文件夹。                                             
            string STEP = "理论值";
            L1 = inf.ReadDouble(STEP, "L1", L1);
            L2 = inf.ReadDouble(STEP, "L2", L2);
            L3 = inf.ReadDouble(STEP, "L3", L3);
            L4 = inf.ReadDouble(STEP, "L4", L4);
            L5 = inf.ReadDouble(STEP, "L5", L5);
            L6 = inf.ReadDouble(STEP, "L6", L6);
            L7 = inf.ReadDouble(STEP, "L7", L7);
            L8 = inf.ReadDouble(STEP, "L8", L8);
            L9 = inf.ReadDouble(STEP, "L9", L9);
            L10 = inf.ReadDouble(STEP, "L1", L10);
            STEP = "上限值";
            UP1 = inf.ReadDouble(STEP, "UP1", UP1);
            UP2 = inf.ReadDouble(STEP, "UP2", UP2);
            UP3 = inf.ReadDouble(STEP, "UP3", UP3);
            UP4 = inf.ReadDouble(STEP, "UP4", UP4);
            UP5 = inf.ReadDouble(STEP, "UP5", UP5);
            UP6 = inf.ReadDouble(STEP, "UP6", UP6);
            UP7 = inf.ReadDouble(STEP, "UP7", UP7);
            UP8 = inf.ReadDouble(STEP, "UP8", UP8);
            UP9 = inf.ReadDouble(STEP, "UP9", UP9);
            UP10 = inf.ReadDouble(STEP, "UP1", UP10);
            STEP = "下限值";
            DW1 = inf.ReadDouble(STEP, "DW1", DW1);
            DW2 = inf.ReadDouble(STEP, "DW2", DW2);
            DW3 = inf.ReadDouble(STEP, "DW3", DW3);
            DW4 = inf.ReadDouble(STEP, "DW4", DW4);
            DW5 = inf.ReadDouble(STEP, "DW5", DW5);
            DW6 = inf.ReadDouble(STEP, "DW6", DW6);
            DW7 = inf.ReadDouble(STEP, "DW7", DW7);
            DW8 = inf.ReadDouble(STEP, "DW8", DW8);
            DW9 = inf.ReadDouble(STEP, "DW9", DW9);
            DW10 = inf.ReadDouble(STEP, "DW1", DW10);
        }
        /// <summary>
        /// 保存参数
        /// </summary>
        public void SaveParam()
        {
            string path1 = $"{Path.GetFullPath("..")}\\product\\{SysStatus.CurProductName.Trim()}\\ThermSet.ini";// 配置文件路径
            IniFile inf = new IniFile(path1);//确认路径是否存在，不存在则创建文件夹。                                             
            string STEP = "理论值";
            inf.WriteDouble(STEP, "L1", L1);
            inf.WriteDouble(STEP, "L2", L2);
            inf.WriteDouble(STEP, "L3", L3);
            inf.WriteDouble(STEP, "L4", L4);
            inf.WriteDouble(STEP, "L5", L5);
            inf.WriteDouble(STEP, "L6", L6);
            inf.WriteDouble(STEP, "L7", L7);
            inf.WriteDouble(STEP, "L8", L8);
            inf.WriteDouble(STEP, "L9", L9);
            inf.WriteDouble(STEP, "L10", L10);
            STEP = "上限值";
            inf.WriteDouble(STEP, "UP1", UP1);
            inf.WriteDouble(STEP, "UP2", UP2);
            inf.WriteDouble(STEP, "UP3", UP3);
            inf.WriteDouble(STEP, "UP4", UP4);
            inf.WriteDouble(STEP, "UP5", UP5);
            inf.WriteDouble(STEP, "UP6", UP6);
            inf.WriteDouble(STEP, "UP7", UP7);
            inf.WriteDouble(STEP, "UP8", UP8);
            inf.WriteDouble(STEP, "UP9", UP9);
            inf.WriteDouble(STEP, "UP10", UP10);
            STEP = "下限值";
            inf.WriteDouble(STEP, "DW1", DW1);
            inf.WriteDouble(STEP, "DW2", DW2);
            inf.WriteDouble(STEP, "DW3", DW3);
            inf.WriteDouble(STEP, "DW4", DW4);
            inf.WriteDouble(STEP, "DW5", DW5);
            inf.WriteDouble(STEP, "DW6", DW6);
            inf.WriteDouble(STEP, "DW7", DW7);
            inf.WriteDouble(STEP, "DW8", DW8);
            inf.WriteDouble(STEP, "DW9", DW9);
            inf.WriteDouble(STEP, "DW10", DW10);

        }
    }
}
