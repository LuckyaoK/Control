using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CXPro001.myclass
{
    [Serializable]
    public class User
    {
        /// <summary>
        /// 用户类型-权限
        /// </summary>
        public enum Permission
        {
            /// <summary>
            /// 游客
            /// </summary>
            [Description("游客")] Anonymous,
            /// <summary>
            /// 作业员
            /// </summary>
            [Description("作业员")] Operator,
            /// <summary>
            /// 工程师
            /// </summary>
            [Description("工程师")] Engineer,
            /// <summary>
            /// 管理员
            /// </summary>
            [Description("管理员")] Admin,
            /// <summary>
            /// 超级用户
            /// </summary>
            [Description("超级用户")] Superuser
        }
        /// <summary>
        /// 用户名
        /// </summary>
        [Description("用户名")]
        public string Name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Description("密码")]
        public string Password { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        [Description("权限")]
        public Permission Permsn { get; set; }
        /// <summary>
        /// user
        /// </summary>
        [Description("user")]
        public User(){}
        /// <summary>
        /// 写入或更新用户名、密码、权限。
        /// </summary>
        /// <param name="str">用户名、密码、权限</param>
        public User(string str)//写入名称密码权限
        {
            FromString(str);
        }
        /// <summary>
        /// 返回用户名、密码、权限。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name},{Password},{Enum.GetName(typeof(Permission), Permsn)}";
        }
        /// <summary>
        /// 写入用户名、密码、权限。
        /// </summary>
        /// <param name="str">用户名、密码、权限</param>
        /// <returns></returns>
        public bool FromString(string str)//写入名称密码权限
        {
            if (str.Length < 3) return false;
            var strList = str.Trim().Split(',');
            if (strList.Length != 3) return false;
            Name = strList[0];
            Password = strList[1];
            Permsn = (Permission)Enum.Parse(typeof(Permission), strList[2], false);// 将一个或多个枚举常数的名称或数字值的字符串表示转换成等效的枚举对象。一个参数指定该操作是否不区分大小写。
            return true;
        }
        /// <summary>
        /// 返回与括号里内容比较结果，如果相同则=true.
        /// </summary>
        /// <param name="obj">要比较的内容</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var user = obj as User;
            return user != null && Name == user.Name && Password == user.Password && Permsn == user.Permsn;
        }

        protected bool Equals(User other)
        {
            return string.Equals(Name, other.Name) && string.Equals(Password, other.Password) && Permsn == other.Permsn;
        }
        /// <summary>
        /// 获取用户名密码权限的哈希值=（用户名*397)^密码）^权限
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked//发生算术溢出时，则不抛出异常
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);                       //用户名哈希值
                hashCode = (hashCode * 397) ^ (Password != null ? Password.GetHashCode() : 0);//密码哈希值
                hashCode = (hashCode * 397) ^ (int) Permsn;                                   //权限哈希值
                return hashCode;
            }
        }
    }
    public class UserCompare : IEqualityComparer<User>
    {
        /// <summary>
        /// 两个用户相比较-相同返回true
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(User x, User y)
        {
            return (x?.Name == y?.Name) && (x?.Password == y?.Password) && (x?.Permsn == y?.Permsn);
        }
        /// <summary>
        /// 获取用户哈希值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(User obj)
        {
            return obj.GetHashCode();

        }
    }
}
