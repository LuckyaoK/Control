using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLib.Files;

 
using System.IO;
using MyLib.Param;
 
using CXPro001.myclass;

namespace CXPro001.RunControl
{
    public partial class DatasSetup : UserControl
    {
        public DatasSetup()
        {
            InitializeComponent();
        }
        private string filename
        {
            get
            {
                return $"{Path.GetFullPath("..")}\\product\\{myclass.SysStatus.CurProductName.Trim()}\\DataSetup.ini";// 配置文件路径
            }
        }
        public void init()
        {
            LoadParameter("");
        }
        /// <summary>
        /// 检查平整度数据是否在范围内
        /// </summary>
        /// <param name="dat1"></param>
        /// <param name="dat2"></param>
        /// <param name="resu"></param>
        /// <returns></returns>
        public EmRes CheckNO1(string dat1,string dat2,out int resu)
        {
            resu = 0;
            return EmRes.Succeed;
        }
        /// <summary>
        /// 检查耐压仪的数据是否在范围内
        /// </summary>
        /// <param name="dat1"></param>
        /// <param name="dat2"></param>
        /// <param name="resu"></param>
        /// <returns></returns>
        public EmRes CheckNO2(string dat1, string dat2, out int resu)
        {
            resu = 0;
            return EmRes.Succeed;
        }
        /// <summary>
        /// 检查检查模号是否正确
        /// </summary>
        /// <param name="dat1"></param>
        /// <param name="resu"></param>
        /// <returns></returns>
        public EmRes CheckNO3(string dat1,  out int resu)
        {
            resu = 0;
            return EmRes.Succeed;
        }
        /// <summary>
        /// 检查检查螺帽是否有无
        /// </summary>
        /// <param name="dat1"></param>
        /// <param name="resu"></param>
        /// <returns></returns>
        public EmRes CheckNO4(string dat1, out int resu)
        {
            resu = 0;
            return EmRes.Succeed;
        }



        #region 导入/保存配置ini
        private void button2_Click(object sender, EventArgs e)//保存参数
        {
            int t1, t2;
            t1 = Environment.TickCount;
            SaveParameter("");
            t2 = Environment.TickCount;
            int tt = t2 - t1;
            MessageBox.Show($"保存成功，耗时{tt}毫秒");
        }
        private void button1_Click(object sender, EventArgs e)//加载参数
        {
            int t1, t2;
            t1 = Environment.TickCount;
            LoadParameter("");
            t2 = Environment.TickCount;
            int tt = t2 - t1;
            MessageBox.Show($"保存成功，耗时{tt}毫秒");
        }
        public EmRes LoadParameter(string path1)//加载配置
        {
            EmRes ret = EmRes.Succeed;
            if (path1 == "") path1 = filename;
            IniFile inf = new IniFile(path1);//确认路径是否存在，不存在则创建文件夹。
            //默认的是STEP1的参数
            string STEP = "检平整度";
            myCheckBox1.Checked = inf.ReadBool(STEP, "ENBA", false);
            textBox1.Text = inf.ReadString(STEP, "PIN1", textBox1.Text);//PIN1
            textBox2.Text = inf.ReadString(STEP, "PIN1UP", textBox2.Text);
            textBox3.Text = inf.ReadString(STEP, "PIN1DOW", textBox3.Text);
            textBox6.Text = inf.ReadString(STEP, "PIN2", textBox6.Text);//PIN2
            textBox5.Text = inf.ReadString(STEP, "PIN2UP", textBox5.Text);
            textBox4.Text = inf.ReadString(STEP, "PIN2DOW", textBox4.Text);
            textBox9.Text = inf.ReadString(STEP, "PIN3", textBox9.Text);//PIN3
            textBox8.Text = inf.ReadString(STEP, "PIN3UP", textBox8.Text);
            textBox7.Text = inf.ReadString(STEP, "PIN3DOW", textBox7.Text);
            textBox12.Text = inf.ReadString(STEP, "PIN4", textBox12.Text);//PIN4
            textBox11.Text = inf.ReadString(STEP, "PIN4UP", textBox11.Text);
            textBox10.Text = inf.ReadString(STEP, "PIN4DOW", textBox10.Text);
            textBox15.Text = inf.ReadString(STEP, "PIN5", textBox15.Text);//PIN5
            textBox14.Text = inf.ReadString(STEP, "PIN5UP", textBox14.Text);
            textBox13.Text = inf.ReadString(STEP, "PIN5DOW", textBox13.Text);
            textBox18.Text = inf.ReadString(STEP, "PIN6", textBox18.Text);//PIN6
            textBox17.Text = inf.ReadString(STEP, "PIN6UP", textBox17.Text);
            textBox16.Text = inf.ReadString(STEP, "PIN6DOW", textBox16.Text);
            STEP = "耐压检测";
            myCheckBox2.Checked = inf.ReadBool(STEP, "ENBB", false);
            textBox36.Text = inf.ReadString(STEP, "parm1", textBox36.Text);//PARM1
            textBox35.Text = inf.ReadString(STEP, "parm1UP", textBox35.Text);
            textBox34.Text = inf.ReadString(STEP, "parm1DOW", textBox34.Text);
            textBox33.Text = inf.ReadString(STEP, "parm2", textBox33.Text);//PARM2
            textBox32.Text = inf.ReadString(STEP, "parm2UP", textBox32.Text);
            textBox31.Text = inf.ReadString(STEP, "parm2DOW", textBox31.Text);
            textBox30.Text = inf.ReadString(STEP, "parm3", textBox30.Text);//PARM3
            textBox29.Text = inf.ReadString(STEP, "parm3UP", textBox29.Text);
            textBox28.Text = inf.ReadString(STEP, "parm3DOW", textBox28.Text);
            textBox27.Text = inf.ReadString(STEP, "parm4", textBox27.Text);//PARM4
            textBox26.Text = inf.ReadString(STEP, "parm4UP", textBox26.Text);
            textBox25.Text = inf.ReadString(STEP, "parm4DOW", textBox25.Text);
            textBox24.Text = inf.ReadString(STEP, "parm5", textBox24.Text);//PARM5
            textBox23.Text = inf.ReadString(STEP, "parm5UP", textBox23.Text);
            textBox22.Text = inf.ReadString(STEP, "parm5DOW", textBox22.Text);
            textBox21.Text = inf.ReadString(STEP, "parm6", textBox21.Text);//PARM6
            textBox20.Text = inf.ReadString(STEP, "parm6UP", textBox20.Text);
            textBox19.Text = inf.ReadString(STEP, "parm6DOW", textBox19.Text);
            STEP = "模号检测";
            myCheckBox3.Checked = inf.ReadBool(STEP, "ENBC", false);
            textBox54.Text = inf.ReadString(STEP, "number", textBox54.Text);
            STEP = "螺帽检测";
            myCheckBox4.Checked = inf.ReadBool(STEP, "ENBD", false);
          
            return ret;
        }
        public EmRes SaveParameter(string path1)//保存参数
        {
            EmRes ret = EmRes.Succeed;
            ret = ChekUser(ParamAttribute.Config.Operator);//检测当前用户权限是否大于Operator
            if (ret != EmRes.Succeed)
            {
                Logger.Error("无权限进行操作");
                MessageBox.Show("无权限进行操作");
                return EmRes.Error;
            }
            if (path1 == "") path1 = filename;
            IniFile inf = new IniFile(path1);//确认路径是否存在，不存在则创建文件夹。
            string STEP = "检平整度";
            inf.WriteString(STEP, "ENBA", myCheckBox1.Checked.ToString());//ENB
            inf.WriteString(STEP, "PIN1", textBox1.Text);//PIN1
            inf.WriteString(STEP, "PIN1UP", textBox2.Text);
            inf.WriteString(STEP, "PIN1DOW", textBox3.Text);         
            inf.WriteString(STEP, "PIN2", textBox6.Text);//PIN2
            inf.WriteString(STEP, "PIN2UP", textBox5.Text);
            inf.WriteString(STEP, "PIN2DOW", textBox4.Text);
            inf.WriteString(STEP, "PIN3", textBox9.Text);//PIN3
            inf.WriteString(STEP, "PIN3UP", textBox8.Text);
            inf.WriteString(STEP, "PIN3DOW", textBox7.Text);
            inf.WriteString(STEP, "PIN4", textBox12.Text);//PIN4
            inf.WriteString(STEP, "PIN4UP", textBox11.Text);
            inf.WriteString(STEP, "PIN4DOW", textBox10.Text);
            inf.WriteString(STEP, "PIN5", textBox15.Text);//PIN5
            inf.WriteString(STEP, "PIN5UP", textBox14.Text);
            inf.WriteString(STEP, "PIN5DOW", textBox13.Text);
            inf.WriteString(STEP, "PIN3", textBox18.Text);//PIN6
            inf.WriteString(STEP, "PIN3UP", textBox17.Text);
            inf.WriteString(STEP, "PIN3DOW", textBox16.Text);
            STEP = "耐压检测";
            inf.WriteString(STEP, "ENBB", myCheckBox2.Checked.ToString());//ENB
            myCheckBox2.Checked = inf.ReadBool(STEP, "ENBB", false);
            inf.WriteString(STEP, "parm1", textBox36.Text);//parm1
            inf.WriteString(STEP, "parm1UP", textBox35.Text); 
            inf.WriteString(STEP, "parm1DOW", textBox34.Text);
            inf.WriteString(STEP, "parm2", textBox33.Text);//parm2
            inf.WriteString(STEP, "parm2UP", textBox32.Text);
            inf.WriteString(STEP, "parm2DOW", textBox31.Text);
            inf.WriteString(STEP, "parm3", textBox30.Text);//parm3
            inf.WriteString(STEP, "parm3UP", textBox29.Text);
            inf.WriteString(STEP, "parm3DOW", textBox28.Text);
            inf.WriteString(STEP, "parm4", textBox27.Text);//parm4
            inf.WriteString(STEP, "parm4UP", textBox26.Text);
            inf.WriteString(STEP, "parm4DOW", textBox25.Text);
            inf.WriteString(STEP, "parm5", textBox24.Text);//parm5
            inf.WriteString(STEP, "parm5UP", textBox23.Text);
            inf.WriteString(STEP, "parm5DOW", textBox22.Text);
            inf.WriteString(STEP, "parm6", textBox21.Text);//parm6
            inf.WriteString(STEP, "parm6UP", textBox20.Text);
            inf.WriteString(STEP, "parm6DOW", textBox19.Text);
            STEP = "模号检测";
            inf.WriteString(STEP, "ENBC", myCheckBox3.Checked.ToString());//ENB
            inf.WriteString(STEP, "number", textBox54.Text);
       
            STEP = "螺帽检测";
            inf.WriteString(STEP, "ENBD", myCheckBox4.Checked.ToString());//ENB
           
            return ret;
        }
        /// <summary>
        /// 用户权限检查
        /// </summary>
        /// <param name="par">最低操作权限</param>
        public EmRes ChekUser(ParamAttribute.Config par)
        {
            //check
            if (!ParamAttribute.CheckPermission(new ParamAttribute("", "", "", par)))//检查当前用户权限是否大于par
            {
            //    var login = new Login
             ////   {
                 //   ListUser =myclass.SysStatus.ListUser,
               //     Dock = DockStyle.Fill
              ///  };
                var fr = new Form
                {
                    Font = new Font(Font.FontFamily, 16),
                    Size = new Size(300, 180),
                    Text = @"用户登录",
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false,
                    StartPosition = FormStartPosition.CenterScreen
                };
            //    login.CurUser =myclass.SysStatus.CurUser;
             //   login.LogChanged += (sed, eAarg) => { fr.Close(); };
             //   fr.Controls.Clear();
            //    fr.Controls.Add(login);
            //    fr.ShowDialog();
                //recheck
                if (!ParamAttribute.CheckPermission(new ParamAttribute("", "", "", par))) return EmRes.Error;
            }
            return EmRes.Succeed;
        }

        #endregion

        

        
        
    }
}
