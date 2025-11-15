using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MyLib.Utilitys;

namespace CXPro001.myclass
{
    #region 基本结构(结构体为值引用不需要实现Clone)

    public struct StXy
    {
        public double X;
        public double Y;

        public StXy(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }

        public StXy(string str)
        {
            X = 0;
            Y = 0;
            FrString(str);
        }

        public StXya ToXya(double a = 0)
        {
            StXya xya = new StXya
            {
                X = X,
                Y = Y,
                A = a
            };
            return xya;
        }

        public StXyz ToXyz(double z = 0)
        {
            StXyz xyz = new StXyz
            {
                X = X,
                Y = Y,
                Z = z
            };
            return xyz;
        }

        public StXyza ToXyza(double z = 0, double a = 0)
        {
            StXyza xyza = new StXyza
            {
                X = X,
                Y = Y,
                Z = z,
                A = a
            };
            return xyza;
        }

        public override string ToString()
        {
            return $" X{X:F3},Y{Y:F3} ";
        }

        public int FrString(string str)
        {
            int n = 0;
            if (Utility.GetDoubleFrStr(str, 'X', ref X)) n++;
            if (Utility.GetDoubleFrStr(str, 'Y', ref Y)) n++;
            return n;
        }

        public static StXy operator +(StXy lhs, StXy rhs)
        {
            return new StXy(lhs.X + rhs.X, lhs.Y + rhs.Y);
        }

        public static StXy operator -(StXy lhs, StXy rhs)
        {
            return new StXy(lhs.X - rhs.X, lhs.Y - rhs.Y);
        }

        public static StXy operator *(StXy lhs, StXy rhs)
        {
            return new StXy(lhs.X * rhs.X, lhs.Y * rhs.Y);
        }

        public static StXy operator /(StXy lhs, StXy rhs)
        {
            return new StXy(lhs.X / rhs.X, lhs.Y / rhs.Y);
        }

        public static bool operator ==(StXy lhs, StXy rhs)
        {
            return Math.Abs(lhs.X - rhs.X) < 0.000000001 && Math.Abs(lhs.Y - rhs.Y) < 0.000000001;
        }

        public static bool operator !=(StXy lhs, StXy rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(StXy other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is StXy && Equals((StXy) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }
    }

    public struct StXz
    {
        public double X;
        public double Z;

        public StXz(double x = 0, double z = 0)
        {
            Z = z;
            X = x;
        }

        public StXz(string str)
        {
            X = 0;
            Z = 0;
            FrString(str);
        }

        public override string ToString()
        {
            return $" X{X:F3},Z{Z:F3} ";
        }

        public int FrString(string str)
        {
            int n = 0;
            if (Utility.GetDoubleFrStr(str, 'X', ref X)) n++;
            if (Utility.GetDoubleFrStr(str, 'Z', ref Z)) n++;
            return n;
        }

        public static StXz operator +(StXz lhs, StXz rhs)
        {
            return new StXz(lhs.X + rhs.X, lhs.Z + rhs.Z);
        }

        public static StXz operator -(StXz lhs, StXz rhs)
        {
            return new StXz(lhs.X - rhs.X, lhs.Z - rhs.Z);
        }

        public static StXz operator *(StXz lhs, StXz rhs)
        {
            return new StXz(lhs.X * rhs.X, lhs.Z * rhs.Z);
        }

        public static StXz operator /(StXz lhs, StXz rhs)
        {
            return new StXz(lhs.X / rhs.X, lhs.Z / rhs.Z);
        }

        public static bool operator ==(StXz lhs, StXz rhs)
        {
            return Math.Abs(lhs.X - rhs.X) < 0.000000001 && Math.Abs(lhs.Z - rhs.Z) < 0.000000001;
        }

        public static bool operator !=(StXz lhs, StXz rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(StXz other)
        {
            return X.Equals(other.X) && Z.Equals(other.Z);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is StXz && Equals((StXz) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Z.GetHashCode();
            }
        }
    }

    public struct StYz
    {
        public double Y;
        public double Z;

        public StYz(double y = 0, double z = 0)
        {
            Z = z;
            Y = y;
        }

        public StYz(string str)
        {
            Z = 0;
            Y = 0;
            FrString(str);
        }

        public override string ToString()
        {
            return $" Y{Y:F3},Z{Z:F3} ";
        }

        public int FrString(string str)
        {
            int n = 0;
            if (Utility.GetDoubleFrStr(str, 'Z', ref Z)) n++;
            if (Utility.GetDoubleFrStr(str, 'Y', ref Y)) n++;
            return n;
        }

        public static StYz operator +(StYz lhs, StYz rhs)
        {
            return new StYz(lhs.Y + rhs.Y, lhs.Z + rhs.Z);
        }

        public static StYz operator -(StYz lhs, StYz rhs)
        {
            return new StYz(lhs.Y - rhs.Y, lhs.Z - rhs.Z);
        }

        public static StYz operator *(StYz lhs, StYz rhs)
        {
            return new StYz(lhs.Y * rhs.Y, lhs.Z * rhs.Z);
        }

        public static StYz operator /(StYz lhs, StYz rhs)
        {
            return new StYz(lhs.Y / rhs.Y, lhs.Z / rhs.Z);
        }

        public static bool operator ==(StYz lhs, StYz rhs)
        {
            return Math.Abs(lhs.Y - rhs.Y) < 0.000000001 && Math.Abs(lhs.Z - rhs.Z) < 0.000000001;
        }

        public static bool operator !=(StYz lhs, StYz rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(StYz other)
        {
            return Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is StYz && Equals((StYz) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Y.GetHashCode() * 397) ^ Z.GetHashCode();
            }
        }
    }

    public struct StXyz
    {
        public double X;
        public double Y;
        public double Z;

        public StXyz(double x = 0, double y = 0, double z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public StXyz(string str)
        {
            X = 0;
            Y = 0;
            Z = 0;
            FrString(str);
        }

        public StXy ToXy()
        {
            StXy xy = new StXy
            {
                X = X,
                Y = Y
            };
            return xy;
        }

        public StXya ToXya()
        {
            StXya xya = new StXya
            {
                X = X,
                Y = Y,
                A = Z
            };
            return xya;
        }

        public StXyza ToXyza(double a = 0)
        {
            StXyza xyza = new StXyza
            {
                X = X,
                Y = Y,
                Z = Z,
                A = a
            };
            return xyza;
        }

        public override string ToString()
        {
            return $" X{X:F3},Y{Y:F3},Z{Z:F3} ";
        }

        public int FrString(string str)
        {
            int n = 0;
            if (Utility.GetDoubleFrStr(str, 'X', ref X)) n++;
            if (Utility.GetDoubleFrStr(str, 'Y', ref Y)) n++;
            if (Utility.GetDoubleFrStr(str, 'Z', ref Z)) n++;
            return n;
        }

        public static StXyz operator +(StXyz lhs, StXyz rhs)
        {
            return new StXyz(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
        }

        public static StXyz operator -(StXyz lhs, StXyz rhs)
        {
            return new StXyz(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
        }

        public static StXyz operator *(StXyz lhs, StXyz rhs)
        {
            return new StXyz(lhs.X * rhs.X, lhs.Y * rhs.Y, lhs.Z * rhs.Z);
        }

        public static StXyz operator /(StXyz lhs, StXyz rhs)
        {
            return new StXyz(lhs.X / rhs.X, lhs.Y / rhs.Y, lhs.Z / rhs.Z);
        }

        public static bool operator ==(StXyz lhs, StXyz rhs)
        {
            return Math.Abs(lhs.X - rhs.X) < 0.000000001 && Math.Abs(lhs.Y - rhs.Y) < 0.000000001 &&
                   Math.Abs(lhs.Z - rhs.Z) < 0.000000001;
        }

        public static bool operator !=(StXyz lhs, StXyz rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(StXyz other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is StXyz && Equals((StXyz) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }
    }

    public struct StXya
    {
        public double X;
        public double Y;
        public double A;

        public StXya(double x = 0, double y = 0, double a = 0)
        {
            X = x;
            Y = y;
            A = a;
        }

        public StXya(string str)
        {
            X = 0;
            Y = 0;
            A = 0;
            FrString(str);
        }

        public StXy ToXy()
        {
            StXy xy = new StXy();
            xy.X = X;
            xy.Y = Y;
            return xy;
        }

        public StXyz ToXyz()
        {
            StXyz xyz = new StXyz
            {
                X = X,
                Y = Y,
                Z = A
            };
            return xyz;
        }

        public StXyza ToXyza(double z = 0)
        {
            StXyza xyza = new StXyza
            {
                X = X,
                Y = Y,
                Z = z,
                A = A
            };
            return xyza;
        }

        public override string ToString()
        {
            return $" X{X:F3},Y{Y:F3},A{A:F3} ";
        }

        public int FrString(string str)
        {
            int n = 0;
            if (Utility.GetDoubleFrStr(str, 'X', ref X)) n++;
            if (Utility.GetDoubleFrStr(str, 'Y', ref Y)) n++;
            if (Utility.GetDoubleFrStr(str, 'A', ref A)) n++;
            return n;
        }

        public static StXya operator +(StXya lhs, StXya rhs)
        {
            return new StXya(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.A + rhs.A);
        }

        public static StXya operator -(StXya lhs, StXya rhs)
        {
            return new StXya(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.A - rhs.A);
        }

        public static StXya operator *(StXya lhs, StXya rhs)
        {
            return new StXya(lhs.X * rhs.X, lhs.Y * rhs.Y, lhs.A * rhs.A);
        }

        public static StXya operator /(StXya lhs, StXya rhs)
        {
            return new StXya(lhs.X / rhs.X, lhs.Y / rhs.Y, lhs.A / rhs.A);
        }

        public static bool operator ==(StXya lhs, StXya rhs)
        {
            return Math.Abs(lhs.X - rhs.X) < 0.000000001 && Math.Abs(lhs.Y - rhs.Y) < 0.000000001 &&
                   Math.Abs(lhs.A - rhs.A) < 0.000000001;
        }

        public static bool operator !=(StXya lhs, StXya rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(StXya other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && A.Equals(other.A);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is StXya && Equals((StXya) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ A.GetHashCode();
                return hashCode;
            }
        }
    }

    public struct StXyza
    {
        public double X;
        public double Y;
        public double Z;
        public double A;

        public StXyza(double x = 0, double y = 0, double z = 0, double a = 0)//初始化=0
        {
            X = x;
            Y = y;
            Z = z;
            A = a;
        }

        public StXyza(string str)
        {
            X = 0;
            Y = 0;
            Z = 0;
            A = 0;
            FrString(str);
        }

        public StXy ToXy()
        {
            StXy xy = new StXy
            {
                X = X,
                Y = Y
            };
            return xy;
        }

        public StXyz ToXyz()
        {
            StXyz xyz = new StXyz
            {
                X = X,
                Y = Y,
                Z = Z
            };
            return xyz;
        }

        public StXya ToXya()
        {
            StXya xya = new StXya
            {
                X = X,
                Y = Y,
                A = A
            };
            return xya;
        }

        public override string ToString()
        {
            return $" X{X:F3},Y{Y:F3},Z{Z:F3},A{A:F3} ";//保留3位小数点
        }

        public int FrString(string str)
        {
            int n = 0;
            if (Utility.GetDoubleFrStr(str, 'X', ref X)) n++;//如果字符串str中X后面含数字则n++;
            if (Utility.GetDoubleFrStr(str, 'Y', ref Y)) n++;//如果字符串str中Y后面含数字则n++;
            if (Utility.GetDoubleFrStr(str, 'Z', ref Z)) n++;//如果字符串str中Z后面含数字则n++;
            if (Utility.GetDoubleFrStr(str, 'A', ref A)) n++;//如果字符串str中A后面含数字则n++;
            return n;
        }

        public static StXyza operator +(StXyza lhs, StXyza rhs)
        {
            return new StXyza(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z, lhs.A + rhs.A);
        }

        public static StXyza operator -(StXyza lhs, StXyza rhs)
        {
            return new StXyza(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z, lhs.A - rhs.A);
        }

        public static StXyza operator *(StXyza lhs, StXyza rhs)
        {
            return new StXyza(lhs.X * rhs.X, lhs.Y * rhs.Y, lhs.Z * rhs.Z, lhs.A * rhs.A);
        }

        public static StXyza operator /(StXyza lhs, StXyza rhs)
        {
            return new StXyza(lhs.X / rhs.X, lhs.Y / rhs.Y, lhs.Z / rhs.Z, lhs.A / rhs.A);
        }

        public static bool operator ==(StXyza lhs, StXyza rhs)
        {
            return Math.Abs(lhs.X - rhs.X) < 0.000000001 && Math.Abs(lhs.Y - rhs.Y) < 0.000000001 &&
                   Math.Abs(lhs.Z - rhs.Z) < 0.000000001 && Math.Abs(lhs.A - rhs.A) < 0.000000001;
        }

        public static bool operator !=(StXyza lhs, StXyza rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(StXyza other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && A.Equals(other.A);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is StXyza && Equals((StXyza) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                hashCode = (hashCode * 397) ^ A.GetHashCode();
                return hashCode;
            }
        }
    }

    /// <summary>
    /// 区间定义
    /// </summary>
    public class Limit
    {
        /// <summary>
        /// 区间最小值
        /// </summary>
        public double Min;

        /// <summary>
        /// 区间最大值
        /// </summary>
        public double Max;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="min">下限</param>
        /// <param name="max">上限</param>
        public Limit(double min = double.MinValue, double max = double.MaxValue)
        {
            Min = min;
            Max = max;
        }

        /// <summary>
        /// 根据字符串创建范围，格式[min,max]
        /// </summary>
        /// <param name="str"></param>
        public Limit(string str)
        {
            Min = 0;
            Max = 0;
            if (str.Length < 3) return;
            var idxStart = str.IndexOf("[", StringComparison.Ordinal);
            if(idxStart<0) idxStart = str.IndexOf("(", StringComparison.Ordinal);
            var idxEnd = str.IndexOf("]", StringComparison.Ordinal);
            if (idxEnd < 0) idxStart = str.IndexOf(")", StringComparison.Ordinal);
            if (idxStart < 0 || idxEnd < 0) return;
            var strLmt = str.Substring(idxStart, idxEnd - idxStart).Trim("[]()".ToCharArray()).Split(',');
            if (strLmt.Length != 2) return;
            if (strLmt[0].Contains("∞")) Min = double.MinValue;
            else double.TryParse(strLmt[0], out Min);
            if (strLmt[1].Contains("∞")) Max = double.MaxValue;
            else double.TryParse(strLmt[1], out Max);
        }

        public Limit()
        {
        }

        /// <summary>
        /// 输入值val是否在区间内
        /// </summary>
        /// <param name="val">输入值</param>
        /// <returns>在区间内返回true，否则返回false</returns>
        public bool IsInLimit(double val)
        {
            return val >= Min && val <= Max;
        }

        /// <summary>
        /// 输入值val是否在区间外
        /// </summary>
        /// <param name="val">输入值</param>
        /// <returns>在区间外返回true，否则返回false</returns>
        public bool IsOutLimit(double val)
        {
            return val < Min || val > Max;
        }

        /// <summary>
        /// 是否无上限
        /// </summary>
        public bool IsNoMaxLimit => Math.Abs(Max - double.MaxValue) < 1;

        /// <summary>
        /// 是否无下限
        /// </summary>
        public bool IsNoMinLimit => Math.Abs(Min - double.MinValue) < 1;

        /// <summary>
        /// 是否限
        /// </summary>
        public bool IsNoLimit => IsNoMinLimit && IsNoMaxLimit;

        /// <summary>
        /// 返回区间描述字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var strmin = Math.Abs(Min - double.MinValue) < 1 ? "-∞" : $"{Min}";
            var strmax = Math.Abs(Max - double.MaxValue) < 1 ? "∞" : $"{Max}";
            return $"[{strmin},{strmax}]";
        }

        public double Range => Math.Abs(Max - Min);

        /// <summary>
        /// 克隆副本
        /// </summary>
        /// <returns></returns>
        public Limit Clone()
        {
            return new Limit(Min, Max);
        }
    }

    /// <summary>
    /// 描述范围组
    /// </summary>
    public class Range
    {
        /// <summary>
        /// 区间组
        /// </summary>
        public List<Limit> Limits = new List<Limit>();

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="limits">区间数组</param>
        public Range(params Limit[] limits)
        {
            Limits.Clear();
            foreach (var lmt in limits)
            {
                Limits.Add(lmt.Clone());
            }
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="limits">区间列表</param>
        public Range(IEnumerable<Limit> limits)
        {
            Limits.Clear();
            foreach (var lmt in limits)
            {
                Limits.Add(lmt.Clone());
            }
        }

        /// <summary>
        /// 从字符串生成范围，格式[min,max][min,max]
        /// </summary>
        /// <param name="str"></param>
        public Range(string str)
        {
            if (str.Length < 3) return;
            var strLmt = str.Split(']');
            foreach (var s in strLmt)
            {
                if (s.Length < 3) continue;
                Limits.Add(new Limit(s + ']'));
            }
        }

        public Range()
        {
        }

        /// <summary>
        /// 返回区间组的描述字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Limits == null || Limits.Count == 0) return "[-∞,∞]";
            var str = "";
            foreach (var lt in Limits)
            {
                str += lt.ToString();
            }

            return str;
        }

        /// <summary>
        /// 测试输入值是否在区间组范围内
        /// </summary>
        /// <param name="val">输入值</param>
        /// <returns>在其中一个区间内时返回true,否则false</returns>
        public bool IsInRange(double val)
        {
            foreach (var lt in Limits)
            {
                if (lt.IsInLimit(val)) return true;
            }

            return false;
        }

        /// <summary>
        /// 返回指定区间的范围大小，找不到指定区间返回0
        /// </summary>
        /// <param name="limitIdx">指定区间编号</param>
        /// <returns>返回指定区间大小</returns>
        public double RangeDist(int limitIdx)
        {
            if (limitIdx >= Limits.Count) return 0;
            return Limits[limitIdx].Range;
        }

        /// <summary>
        /// 返回范围组最大距离
        /// </summary>
        public double MaxDist
        {
            get
            {
                if (Limits != null && Limits.Count > 0)
                    return Limits.Max(s => s.Range);
                return 0;
            }
        }

        /// <summary>
        /// 克隆副本
        /// </summary>
        /// <returns></returns>
        public Range Clone()
        {
            return new Range(Limits);
        }
    }

    #endregion

    #region 结果定义

    public enum EmRes
    {
        //RESEULT
        /// <summary>
        /// 结果-成功
        /// </summary>
        [Description("成功")] Ok = 0,
        /// <summary>
        /// 结果-成功
        /// </summary>
        [Description("成功")] Succeed = 0,
        /// <summary>
        /// 结果-错误
        /// </summary>
        [Description("错误")] Error,
        /// <summary>
        /// 结果-急停
        /// </summary>
        [Description("急停")] Emg,
        /// <summary>
        /// 结果-超时
        /// </summary>
        [Description("超时")] Timeout,
        /// <summary>
        /// 结果-取消
        /// </summary>
        [Description("取消")] Quit,
        /// <summary>
        /// 结果-退出
        /// </summary>
        [Description("退出")] Abort,
        /// <summary>
        /// 结果-未定义
        /// </summary>
        [Description("未定义")] Undefine,

        //FOLLOW
        /// <summary>
        /// 流程-忙
        /// </summary>
        [Description("BUSY")] Busy,
        /// <summary>
        /// 流程-等待
        /// </summary>
        [Description("WAIT")] Wait,
        /// <summary>
        /// 流程-下一个
        /// </summary>
        [Description("NEXT")] Next,
        /// <summary>
        /// 流程-重试
        /// </summary>
        [Description("RETRY")] Retry,
        /// <summary>
        /// 流程-结束
        /// </summary>
        [Description("END")] End,

        //INIT
        /// <summary>
        /// 参数-初始化错误
        /// </summary>
        [Description("初始化错误")]
        InitError,
        /// <summary>
        /// 初始化-未执行
        /// </summary>
        [Description("初始化未执行")]
        InitNotCompelet,
        /// <summary>
        /// 初始化-初始化参数错误
        /// </summary>
        [Description("初始化参数错误")]
        InitParamErr,

        //PARAM
        /// <summary>
        /// 参数-异常
        /// </summary>
        [Description("参数异常")] ParamError,
        /// <summary>
        /// 参数-超范围
        /// </summary>
        [Description("参数超范围")] ParamOutofRang,
        /// <summary>
        /// 参数-为空(NULL)
        /// </summary>
        [Description("参数为NULL")] ParamNull,
        /// <summary>
        /// 参数-无权限
        /// </summary>
        [Description("无权限")] PermissonErr,

        //File
        /// <summary>
        /// 文件-打开错误
        /// </summary>
        [Description("文件打开错误")] FileOpenErr,
        /// <summary>
        /// 文件-不存在
        /// </summary>
        [Description("文件不存在")] FileNotExist,

        //DataBase
        /// <summary>
        /// 数据库-链接错误
        /// </summary>
        [Description("数据库链接错误")] DbLinkErr,
        /// <summary>
        /// 数据库-写入错误
        /// </summary>
        [Description("数据库写入错误")] DbWriteErr,
        /// <summary>
        /// 数据库-查询错误
        /// </summary>
        [Description("数据库查询错误")] DbQueryErr,

        //CAM
        /// <summary>
        /// 相机-错误
        /// </summary>
        [Description("相机错误")] CamError,
        /// <summary>
        /// 相机-未初始化
        /// </summary>
        [Description("相机未初始化")] CamInitError,
        /// <summary>
        /// 相机-连接错误
        /// </summary>
        [Description("相机连接错误")] CamLinkError,
        /// <summary>
        /// 相机-参数错误
        /// </summary>
        [Description("相机参数错误")] CamParamError,
        /// <summary>
        /// 相机-任务加载错误
        /// </summary>
        [Description("相机任务加载错误")] CamTaskLoadError,
        /// <summary>
        /// 相机-重测错误
        /// </summary>
        [Description("相机重测错误")] CamRechkError,
        /// <summary>
        /// 相机-重定位错误
        /// </summary>
        [Description("相机重定位错误")] CamRemark,

        //FUCTION
        /// <summary>
        /// 功能-取料错误
        /// </summary>
        [Description("取料错误")] PickError,
        /// <summary>
        /// 功能-放料错误
        /// </summary>
        [Description("放料错误")] PlaceError,

        //PROTECT
        /// <summary>
        /// 保护-移动保护
        /// </summary>
        [Description("移动保护")] MoveProtect,
        /// <summary>
        /// 保护-传感器保护
        /// </summary>
        [Description("传感器保护")] SenProtect,
        /// <summary>
        /// 保护-位置保护
        /// </summary>
        [Description("位置保护")] PosProtect,
        /// <summary>
        /// 保护-GPIO保护
        /// </summary>
        [Description("GPIO保护")] GpioProtect,
        /// <summary>
        /// 保护-旋转保护
        /// </summary>
        [Description("旋转保护")] RolProtect,
        /// <summary>
        /// 保护-保养周期
        /// </summary>
        [Description("保养周期")] MaintenanceProtect,
        //XLing
        //STSTUS
        /// <summary>
        /// 结果-空
        /// 此位置九宫格已流转到下一位置，可以接受九宫格流转过来
        /// </summary>
        [Description("空")]
        Empty,
        /// <summary>
        /// 结果-待测
        /// 此位置九宫格已完成对应动作，待进入下一流程
        /// </summary>
        [Description("待测")]
        UnTest,
        /// <summary>
        /// 结果-忙
        /// 此位置九宫格正在进行动作
        /// </summary>
        [Description("忙")]
        OnGing,
        /// <summary>
        /// 结果-就绪
        /// 此位置九宫格未进行对应动作,等待当前位置操作
        /// </summary>
        [Description("就绪")]
        IsReady
        //XLing
    }

    #endregion

    //#region 视觉数据

    //public struct VsDat
    //{
    //    public int FlowNum; //对应流程编号
    //    public int Id; //目标编号
    //    public StXyza StCapPos; //拍照位置
    //    public StXya StPix; //像素坐标
    //    public StXya StMm; //mm坐标
    //    public StXy StCenterOfs; //与画面中心偏差
    //    public StXyza StTemp; //备用
    //    public string StrBarcode; //读取二维码数据
    //    public bool IsOk; //结果
    //    public bool IsUpdate; //更新标志

    //    public int CtMs; //CT时间
    //    //public ICogImage outputImage;//输出图像
    //    //public CogGraphicCollection GraphicCollection;//输出界面绘图
    //    //public CogCompositeShape GraphicsPMAlignTool;//输出PMAlignTool匹配图形
    //    //public List<CogPointMarker> ListTopCamera;
    //}

    //#endregion

}