
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


using CXPro001.myclass;
namespace CXPro001.forms
{
    public partial class FrmLogin : Form
    {


        //声明UI更新时间变量
        public static bool loginSucceedFlag;
        public static DateTime dt;

       private int a = 0;
        public FrmLogin()
        {
            InitializeComponent();

            this.Load += FrmLogin_Load;

        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {


        }
        /// <summary>
        /// 倒计时登录默认窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>



        private void MainPanel_DoubleClick(object sender, EventArgs e)//关闭窗口
        {

        }
        User user = new User($"作业员,123,{User.Permission.Superuser}");
        private void btn_Login_Click(object sender, EventArgs e)//用户登录
        {
            string Uname = cmb_LoginName.Text;
            string pas = txt_LoginPwd.Text.Trim();

            if (Uname == "管理员")//用户名
            {
                if (pas.Length > 0 && pas == "123456")//密码
                {

                    user = new User($"管理员,{pas},{User.Permission.Superuser}");//写入用户名，密码，超级用户

                    Logger.Info($"登入用户成功{Uname}");
                    loginSucceedFlag = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(@"用户密码错误!", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                myclass.SysStatus.CurUser = user;//更新到系统状态             
                this.Close();
            }
            else if (Uname == "技术员")//用户名
            {
                if (pas.Length > 0 && pas == "123")//密码
                {

                    user = new User($"技术员,{pas},{User.Permission.Superuser}");//写入用户名，密码，超级用户

                    Logger.Info($"登入用户成功{Uname}");
                    loginSucceedFlag = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(@"用户密码错误!", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                SysStatus.CurUser = user;//更新到系统状态             
                this.Close();
            }
            else if (Uname == "作业员")//用户名
            {
                user = new User($"作业员,{pas},{User.Permission.Superuser}");//写入用户名，密码，超级用户

                Logger.Info($"登入用户成功{Uname}");
                loginSucceedFlag = true;
                SysStatus.CurUser = user;//更新到系统状态             
                this.Close();

            }

            if (loginSucceedFlag)
            {
                dt = DateTime.Now;
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }






        #region 窗体移动
        private Point mouseOff;//鼠标移动位置变量
        private bool leftFlag;//标签是否为左键

        private void FrmLogin_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }
        private void FrmLogin_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }
        private void FrmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        #endregion



    }
}
