using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CXPro001.classes;
using CXPro001.myclass;

using System.Threading;
using System.IO;
using MyLib.Files;
using MyLib.Param;
using MyLib.Users;

namespace CXPro001.controls
{
    public partial class Test7631Control : UserControl
    {
        public Test7631Control()
        {
            InitializeComponent();
        }
        private string filename
        {
            get
            {
                return $"{Path.GetFullPath("..")}\\product\\{SysStatus.CurProductName.Trim()}\\TEST7631.ini";// 配置文件路径
            }
        }
        test7631 TS76311 = null;
        Task task1 = null;
        private void tianjia(test7631 ts7)
        {
            ts7.chuan += jin;
        }
        private void jin(object sender,test7631.PaperC e)
        {
            Invoke(new Action(() => { 
                listBox1.Items.Add($"{DateTime.Now.ToString()}接收：{e.Name}");
                textBox1.Text = e.Name;
            }));
        }
        public void init()
        {
            TS76311 = hardware.Tes7631;
            tianjia(TS76311);
            timer1.Enabled = true;
        }
        #region 写入配置参数
        private async void button9_Click(object sender, EventArgs e)//写入当前设置
        {
            if (MessageBox.Show("确定要写入参数配置吗？","提示",MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            //检查用户权限
            EmRes ret = EmRes.Succeed;
            ret = ChekUser(ParamAttribute.Config.Operator);//检测当前用户权限是否大于Operator
            if (ret != EmRes.Succeed)
            {
                Logger.Error("无权限进行操作");
                MessageBox.Show("无权限进行操作");
                return;
            }
            button9.Enabled = false;
            if (TS76311 == null)
            {
                Logger.Error("耐压仪7631未初始化");
                MessageBox.Show("耐压仪7631未初始化");
                button9.Enabled = true;
                return;
            }
            if (!TS76311.IsConnected)
            {
                Logger.Error("耐压仪7631未连接");
                MessageBox.Show("耐压仪7631未连接");
                button9.Enabled = true;
                return;
            }
            //1.写入STEP EDIT: STEP 1
            listBox1.Items.Add($"发送信息:{DateTime.Now.ToString()}：EDIT: STEP {comboBox1.Text.Trim()}");
            TS76311.sends("EDIT:STEP " + (comboBox1.Text.Trim()));
            task1 = new Task(RUN);
            task1.Start();
            await task1;

            //2.写入模式 EDIT: FUNC ACW
            listBox1.Items.Add($"发送信息:{DateTime.Now.ToString()}：EDIT: FUNC {comboBox2.Text.Trim()}");
            TS76311.sends("EDIT:FUNC " + (comboBox2.Text.Trim()));
            task1 = new Task(RUN);
            task1.Start();
            await task1;
            //3.写入测试电压EDIT:VOLT 1kV
            listBox1.Items.Add($"发送信息:{DateTime.Now.ToString()}：EDIT: VOLT {textBox2.Text.Trim()}");
            TS76311.sends("EDIT:VOLT " + (textBox2.Text.Trim()));
            task1 = new Task(RUN);
            task1.Start();
            await task1;
            //4.写入测试频率 EDIT:FREQ 50HZ
            listBox1.Items.Add($"发送信息:{DateTime.Now.ToString()}：EDIT: FREQ {comboBox3.Text.Trim()}");
            TS76311.sends("EDIT:FREQ " + (comboBox3.Text.Trim()));
            task1 = new Task(RUN);
            task1.Start();
            await task1;
            //4.写入上限报警值 EDIT:HILI 1mA
            listBox1.Items.Add($"发送信息:{DateTime.Now.ToString()}：EDIT: HILI {textBox3.Text.Trim()}");
            TS76311.sends("EDIT:HILI " + (textBox3.Text.Trim()));
            task1 = new Task(RUN);
            task1.Start();
            await task1;
            //5.写入下限报警值 EDIT:LOLI 0.5mA
            listBox1.Items.Add($"发送信息:{DateTime.Now.ToString()}：EDIT:LOLI {textBox4.Text.Trim()}");
            TS76311.sends("EDIT:LOLI " + (textBox4.Text.Trim()));
            task1 = new Task(RUN);
            task1.Start();
            await task1;
            //6.写入电压上升时间  EDIT:RAMP 0.5s
            listBox1.Items.Add($"发送信息:{DateTime.Now.ToString()}：EDIT:RAMP {textBox5.Text.Trim()}");
            TS76311.sends("EDIT:RAMP " + (textBox5.Text.Trim()));
            task1 = new Task(RUN);
            task1.Start();
            await task1;
            //7.写入测试时间 EDIT:DWEL 1s
            listBox1.Items.Add($"发送信息:{DateTime.Now.ToString()}：EDIT:DWEL {textBox6.Text.Trim()}");
            TS76311.sends("EDIT:DWEL " + (textBox6.Text.Trim()));
            task1 = new Task(RUN);
            task1.Start();
            await task1;
            //8.偵測電弧敏感度的設定  EDIT: ARC 5
            listBox1.Items.Add($"发送信息:{DateTime.Now.ToString()}：EDIT:ARC {textBox7.Text.Trim()}");
            TS76311.sends("EDIT:ARC " + (textBox7.Text.Trim()));
            task1 = new Task(RUN);
            task1.Start();
            await task1;
            //9.定虛部電流的歸零扣除值  EDIT: OFFS IMAG 0.002mA
            listBox1.Items.Add($"发送信息:{DateTime.Now.ToString()}：EDIT:OFFS IMAG {textBox8.Text.Trim()}");
            TS76311.sends("EDIT:OFFS IMAG " + (textBox8.Text.Trim()));
            task1 = new Task(RUN);
            task1.Start();
            await task1;
            //10.實部電流的歸零扣除值 EDIT: OFFS REAL 0.002mA
            listBox1.Items.Add($"发送信息:{DateTime.Now.ToString()}：EDIT:OFFS REAL {textBox9.Text.Trim()}");
            TS76311.sends("EDIT:OFFS REAL " + (textBox9.Text.Trim()));
            task1 = new Task(RUN);
            task1.Start();
            await task1;
            //11.設定開路偵測敏感度 EDIT: OPEN 200
            listBox1.Items.Add($"发送信息:{DateTime.Now.ToString()}：EDIT:OPEN {textBox10.Text.Trim()}");
            TS76311.sends("EDIT:OPEN " + (textBox10.Text.Trim()));
            task1 = new Task(RUN);
            task1.Start();
            await task1;
            //12.設定 IR 的延遲判斷時間 EDIT:IR:DELA 0.1s
            listBox1.Items.Add($"发送信息:{DateTime.Now.ToString()}：EDIT:IR:DELA {textBox11.Text.Trim()}");
            TS76311.sends("EDIT:IR:DELA " + (textBox11.Text.Trim()));
            task1 = new Task(RUN);
            task1.Start();
            await task1;
            button9.Enabled = true;

        }
        private  void RUN()//获取返回信息
        {
            Thread.Sleep(100);
            int a = 0;
            while(true)
            {             
                if(TS76311.ReceiveData8740.Length < 1)
                {
                    Thread.Sleep(50);
                    a++;
                    if (a > 30)
                    {
                        Invoke(new Action(() =>
                        {
                            listBox1.Items.Add($"接收信息超时！{DateTime.Now.ToString()}");
                        }));                     
                        break;
                    }
                }
                else
                {
                    Invoke(new Action(() =>
                    {
                        listBox1.Items.Add($"接收信息:{DateTime.Now.ToString()}：{TS76311.ReceiveData8740}");
                    }));               
                }
            }         
        }
        #endregion
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (TS76311 == null) return;
            if(TS76311.IsConnected)
            {
                label1.Text = "连接正常";
                label1.BackColor = Color.Lime;
            }
            else
            {
                label1.Text = "连接异常";
                label1.BackColor = Color.Red;
            }
            if (listBox1.Items.Count > 5000) listBox1.Items.Clear();
        }
        private void button10_Click(object sender, EventArgs e)//清楚listbox
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("通讯记录");
        }
        #region 手动操作
        private void button3_Click(object sender, EventArgs e)//检查仪器是否就绪
        {
            button3.Enabled = false;
            if (TS76311 == null)
            {
                Logger.Error("耐压仪7631未初始化");
                MessageBox.Show("耐压仪7631未初始化");
                button3.Enabled = true;
                return;
            }
            if (!TS76311.IsConnected)
            {
                Logger.Error("耐压仪7631未连接");
                MessageBox.Show("耐压仪7631未连接");
                button3.Enabled = true;
                return;
            }
           if(TS76311.sends("*OPT?") == EmRes.Succeed)//发送成功
            {
                //Thread.Sleep(150);
                //if(TS76311.ReceiveData8740.Length>0) textBox1.Text = $"返回信息：{TS76311.ReceiveData8740}";
                //else textBox1.Text = $"未收到仪器返回信息";
            }
            else MessageBox.Show("发送信息失败", "警告");
            button3.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)//启动测试
        {
            button1.Enabled = false;
            if (TS76311 == null)
            {
                Logger.Error("耐压仪7631未初始化");
                MessageBox.Show("耐压仪7631未初始化");
                button1.Enabled = true;
                return;
            }
            if (!TS76311.IsConnected)
            {
                Logger.Error("耐压仪7631未连接");
                MessageBox.Show("耐压仪7631未连接");
                button1.Enabled = true;
                return;
            }
            if (TS76311.sends(":STAR") == EmRes.Succeed)//发送成功
            {
                //Thread.Sleep(150);
                //if (TS76311.ReceiveData8740.Length > 0) textBox1.Text = $"返回信息：{TS76311.ReceiveData8740}";
                //else textBox1.Text = $"未收到仪器返回信息";
            }
            else
            {
                MessageBox.Show("发送信息失败", "警告");
            }
            button1.Enabled = true;
        }
        private void button4_Click(object sender, EventArgs e)//检查是否完成
        {
            button4.Enabled = false;
            if (TS76311 == null)
            {
                Logger.Error("耐压仪7631未初始化");
                MessageBox.Show("耐压仪7631未初始化");
                button4.Enabled = true;
                return;
            }
            if (!TS76311.IsConnected)
            {
                Logger.Error("耐压仪7631未连接");
                MessageBox.Show("耐压仪7631未连接");
                button4.Enabled = true;
                return;
            }
            if (TS76311.sends("*OPC?") == EmRes.Succeed)//发送成功
            {
                //Thread.Sleep(150);
                //if (TS76311.ReceiveData8740.Length > 0) textBox1.Text = $"返回信息：{TS76311.ReceiveData8740}";
                //else textBox1.Text = $"未收到仪器返回信息";
            }
            else MessageBox.Show("发送信息失败", "警告");
            button4.Enabled = true;
        }
        private void button2_Click(object sender, EventArgs e)//停止测试
        {
            button2.Enabled = false;
            if (TS76311 == null)
            {
                Logger.Error("耐压仪7631未初始化");
                MessageBox.Show("耐压仪7631未初始化");
                button2.Enabled = true;
                return;
            }
            if (!TS76311.IsConnected)
            {
                Logger.Error("耐压仪7631未连接");
                MessageBox.Show("耐压仪7631未连接");
                button2.Enabled = true;
                return;
            }
            if (TS76311.sends(":STOP") == EmRes.Succeed)//发送成功
            {
                //Thread.Sleep(150);
                //if (TS76311.ReceiveData8740.Length > 0) textBox1.Text = $"返回信息：{TS76311.ReceiveData8740}";              
                //else textBox1.Text = $"未收到仪器返回信息";             
            }
            else MessageBox.Show("发送信息失败", "警告");
            button2.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)//获取结果
        {
            button5.Enabled = false;
            if (TS76311 == null)
            {
                Logger.Error("耐压仪7631未初始化");
                MessageBox.Show("耐压仪7631未初始化");
                button5.Enabled = true;
                return;
            }
            if (!TS76311.IsConnected)
            {
                Logger.Error("耐压仪7631未连接");
                MessageBox.Show("耐压仪7631未连接");
                button5.Enabled = true;
                return;
            }
            if (TS76311.sends(":RESUlt?") == EmRes.Succeed)//发送成功
            {
                //Thread.Sleep(150);
                //if (TS76311.ReceiveData8740.Length > 0) textBox1.Text = $"返回信息：{TS76311.ReceiveData8740}";
                //else textBox1.Text = $"未收到仪器返回信息";
            }
            else MessageBox.Show("发送信息失败", "警告");
            button5.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)//设置仪器结果自动发送开启
        {
            button6.Enabled = false;
            if (TS76311 == null)
            {
                Logger.Error("耐压仪7631未初始化");
                MessageBox.Show("耐压仪7631未初始化");
                button6.Enabled = true;
                return;
            }
            if (!TS76311.IsConnected)
            {
                Logger.Error("耐压仪7631未连接");
                MessageBox.Show("耐压仪7631未连接");
                button6.Enabled = true;
                return;
            }
            if (TS76311.sends("SYST:AURE ON") == EmRes.Succeed)//发送成功
            {
                //Thread.Sleep(150);
                //if (TS76311.ReceiveData8740.Length > 0) textBox1.Text = $"返回信息：{TS76311.ReceiveData8740}";
                //else textBox1.Text = $"未收到仪器返回信息";
            }
            else MessageBox.Show("发送信息失败", "警告");
            button6.Enabled = true;
        }
        private void button7_Click(object sender, EventArgs e)//启动键盘锁
        {
            button7.Enabled = false;
            if (TS76311 == null)
            {
                Logger.Error("耐压仪7631未初始化");
                MessageBox.Show("耐压仪7631未初始化");
                button7.Enabled = true;
                return;
            }
            if (!TS76311.IsConnected)
            {
                Logger.Error("耐压仪7631未连接");
                MessageBox.Show("耐压仪7631未连接");
                button7.Enabled = true;
                return;
            }
            if (TS76311.sends("SYST: KYSO ON") == EmRes.Succeed)//发送成功
            {
                //Thread.Sleep(150);
                //if (TS76311.ReceiveData8740.Length > 0) textBox1.Text = $"返回信息：{TS76311.ReceiveData8740}";
                //else textBox1.Text = $"未收到仪器返回信息";
            }
            else MessageBox.Show("发送信息失败", "警告");
            button7.Enabled = true;
        }
        private void button8_Click(object sender, EventArgs e)//关闭键盘锁
        {
            button7.Enabled = false;
            if (TS76311 == null)
            {
                Logger.Error("耐压仪7631未初始化");
                MessageBox.Show("耐压仪7631未初始化");
                button7.Enabled = true;
                return;
            }
            if (!TS76311.IsConnected)
            {
                Logger.Error("耐压仪7631未连接");
                MessageBox.Show("耐压仪7631未连接");
                button7.Enabled = true;
                return;
            }
            if (TS76311.sends("SYST: KYSO OFF") == EmRes.Succeed)//发送成功
            {
                //Thread.Sleep(150);
                //if (TS76311.ReceiveData8740.Length > 0) textBox1.Text = $"返回信息：{TS76311.ReceiveData8740}";
                //else textBox1.Text = $"未收到仪器返回信息";
            }
            else MessageBox.Show("发送信息失败", "警告");
            button7.Enabled = true;
        }
        #endregion
        #region 导入/保存配置ini
        public EmRes LoadParameter(string path1)//加载配置
        {
            EmRes ret = EmRes.Succeed;
            if (path1 == "") path1 = filename;
            IniFile inf = new IniFile(path1);//确认路径是否存在，不存在则创建文件夹。
            //默认的是STEP1的参数
            string STEP = "";
            string STEP1 = comboBox1.Text.Trim();
            if (STEP1 == "1" | STEP1 == "2"| STEP1 == "3"| STEP1 == "4" | STEP1=="5" | STEP1 == "6" | STEP1 == "7" | STEP1 == "8")
            {
                STEP = $"STEP{STEP1}";
                comboBox2.Text = inf.ReadString(STEP, "MODE:", comboBox2.Text);//模式
                textBox2.Text = inf.ReadString(STEP, "VOLT:", textBox2.Text);//电压
                comboBox3.Text = inf.ReadString(STEP, "FREQ:", comboBox3.Text);//频率
                textBox3.Text = inf.ReadString(STEP, "HI:", textBox3.Text);//上限
                textBox4.Text = inf.ReadString(STEP, "LO:", textBox4.Text);//下限
                textBox5.Text = inf.ReadString(STEP, "Ramp:", textBox5.Text);//上升时间
                textBox6.Text = inf.ReadString(STEP, "DWEL:", textBox6.Text);//测试时间
                textBox7.Text = inf.ReadString(STEP, "ARC:", textBox7.Text);//电弧敏感度
                textBox8.Text = inf.ReadString(STEP, "OFFS IMAG:", textBox8.Text);//治具误差-虚
                textBox9.Text = inf.ReadString(STEP, "OFFS REAL:", textBox9.Text);//治具误差-实
                textBox10.Text = inf.ReadString(STEP, "OPEN:", textBox10.Text);//侦测敏感度
                textBox11.Text = inf.ReadString(STEP, "DLAY:", textBox11.Text);//延迟时间
            }
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
            string STEP = "";
            string STEP1 = comboBox1.Text.Trim();
            if (STEP1 == "1" | STEP1 == "2" | STEP1 == "3" | STEP1 == "4" | STEP1 == "5" | STEP1 == "6" | STEP1 == "7" | STEP1 == "8")
            {
                STEP = $"STEP{STEP1}";
                inf.WriteString(STEP, "MODE:", comboBox2.Text);//模式
                inf.WriteString(STEP, "VOLT:", textBox2.Text);//电压
                inf.WriteString(STEP, "FREQ:", comboBox3.Text);//频率
                inf.WriteString(STEP, "HI:", textBox3.Text);//上限
                inf.WriteString(STEP, "LO:", textBox4.Text);//下限
                inf.WriteString(STEP, "Ramp:", textBox5.Text);//上升时间
                inf.WriteString(STEP, "DWEL:", textBox6.Text);//测试时间
                inf.WriteString(STEP, "ARC:", textBox7.Text);//电弧敏感度
                inf.WriteString(STEP, "OFFS IMAG:", textBox8.Text);//治具误差-虚
                inf.WriteString(STEP, "OFFS REAL:", textBox9.Text);//治具误差-实
                inf.WriteString(STEP, "OPEN:", textBox10.Text);//侦测敏感度
                inf.WriteString(STEP, "DLAY:", textBox11.Text);//延迟时间
            } 

            return ret;
        }

        #endregion

        private void button11_Click(object sender, EventArgs e)//加载参数
        {
            LoadParameter(filename);
            listBox1.Items.Add($"加载参数完成！{DateTime.Now.ToString()}");
        }

        private void button12_Click(object sender, EventArgs e)//保存设置
        {
            SaveParameter(filename);
            listBox1.Items.Add($"保存参数完成！{DateTime.Now.ToString()}");
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
                var login = new Login
                {
                    ListUser = SysStatus.ListUser,
                    Dock = DockStyle.Fill
                };
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
                login.CurUser = SysStatus.CurUser;
                login.LogChanged += (sed, eAarg) => { fr.Close(); };
                fr.Controls.Clear();
                fr.Controls.Add(login);
                fr.ShowDialog();
                //recheck
                if (!ParamAttribute.CheckPermission(new ParamAttribute("", "", "", par))) return EmRes.Error;
            }
            return EmRes.Succeed;
        }
    }
}
