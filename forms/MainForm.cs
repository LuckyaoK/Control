using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SunnyLib.Sys;
 
using System.Threading;
using SunnyLib.Param;
using SunnyLib.Users;
using UpCom.myclass;
using UpCom.classes;
using UpCom.controls;

namespace UpCom.forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);//禁止擦除背景
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;//获取或设置一个值，该值指示此控件是否应使用辅助缓冲区重绘其图面，以减少或避免闪烁
            ShowStartTip();//启动小窗口
            Logger.Info($"程序启动{DateTime.Now.ToString("HH: mm:ss.fff")}");
           
            ParamAttribute.LoadFromDb(typeof(SysStatus), SysStatus.SysCfgPath);
            var s = SysStatus.CurProductName;//把产品名称执行出来
            Alarm.Init(label2);


            InitializeComponent();
           
            
        }
        #region 启动小窗口
        private Form _FormStartTip;
        private WaitStartTip _WaitStartTip;
        private void ShowStartTip()// 启动程序启动小窗口
        {
            _FormStartTip = new Form
            {
                Font = new Font(Font.FontFamily, 9),
                Size = new Size(530, 200),
                FormBorderStyle = FormBorderStyle.None,
                MaximizeBox = false,
                MinimizeBox = false,
                StartPosition = FormStartPosition.CenterScreen,
                //   TopMost = true,
                //Icon = Properties.Resources.Quick_Launch,
                //Icon = 071915390014_0gongsilog,
                Text = "Loading"
            };
            _WaitStartTip = new WaitStartTip
            {
                Dock = DockStyle.Fill,
                ElapsedTime = 30
            };
            _FormStartTip.Controls.Add(_WaitStartTip);
            Task tt = new Task(() => { _FormStartTip.ShowDialog(); });
            tt.Start();
            Thread.Sleep(50);
        }
        /// <summary>
        /// 关闭开始小窗口
        /// </summary>
        private void ColseStartTip()
        {
            _FormStartTip.BeginInvoke(new MethodInvoker(delegate
            {
                _WaitStartTip.Stop();
                // _WaitStartTip.Dispose();
                _FormStartTip.Close();
            }));
        }
        #endregion
       

        private void cTabControl2_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage.Name == "tabPage8")//扫码监控
            {
                barCordControl1.timeropen();
            }
            else barCordControl1.timerclose();

            if (e.TabPage.Name != "syssetup")//IO监控关闭
            {
                ioMonitor1.timerclose();
            }
            

            if (e.TabPage.Name == "tabPage4")
            {
                productList1.UpdateShow();
            }
           else if (e.TabPage.Name == "tabPage3")//参数界面，检查权限 syssetup
            {
                datasSetup1.init();
                EmRes ret = EmRes.Succeed;
                ret = ChekUser(ParamAttribute.Config.Engineer);//检测当前用户权限是否大于Operator
                if (ret != EmRes.Succeed)
                {
                    Logger.Error("无权限打开参数界面");
                    MessageBox.Show("无权限打开参数界面");
                    cTabControl1.SelectTab("tabPage1");
                    return;
                }
            }
            else if (e.TabPage.Name == "tabPage5")//用户界面
            {
                login1.ListUser = SysStatus.ListUser;
                login1.CurUser = SysStatus.CurUser;
            }
        }
        #region 用户权限检查
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
        #endregion
        #region 刷新曲线
        /// <summary>
        /// 刷新电流曲线
        /// </summary>
        /// <param name="y1"></param>
        private void refchart1(double y1)
        {

        }
        /// <summary>
        /// 刷新电压曲线
        /// </summary>
        /// <param name="y1"></param>
        private void refchart2(double y1)
        {

        }
        /// <summary>
        /// 刷新电组曲线
        /// </summary>
        /// <param name="y1"></param>
        private void refchart3(double y1)
        {

        }
        /// <summary>
        /// 刷新平整度曲线
        /// </summary>
        /// <param name="y1"></param>
        private void refchart4(double y1)
        {

        }
        private void button6_Click(object sender, EventArgs e)//清除曲线
        {
            if(((Button)sender).Name== "buttonA")
            {
                chart1.Series[0].Points.Clear();
            }
            if (((Button)sender).Name == "buttonV")
            {
                chart2.Series[0].Points.Clear();
            }
            if (((Button)sender).Name == "buttonO")
            {
                chart3.Series[0].Points.Clear();
            }
            if (((Button)sender).Name == "buttonT")
            {
                chart4.Series[0].Points.Clear();
            }
        }


        #endregion
        #region 启动 停止，状态显示
        private void button1_Click(object sender, EventArgs e)//启动按钮
        {
            #region 检测硬件连接及各种状态
            //检查硬件连接
            //if (!hardware.PLC_KEY.IsConnected)
            //{
            //    MessageBox.Show("基恩士PLC未连接", "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //if (!hardware.delijie1.IsConnected)
            //{
            //    MessageBox.Show("得利捷扫码枪未连接", "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //if (!hardware.test9803.IsConnected)
            //{
            //    MessageBox.Show("耐压仪未连接", "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //if (!hardware.viscam.IsConnected)
            //{
            //    MessageBox.Show("与视觉未连接", "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //if (!hardware.jienshi1.IsConnected)
            //{
            //    MessageBox.Show("基恩士读码器未连接", "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //if(SysStatus.CurProductName==""|SysStatus.CurProductName==null)
            //{
            //    MessageBox.Show("没有选择产品", "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //if(SysStatus.Status == SysStatus.EmSysSta.Run)
            //{
            //    MessageBox.Show("程序已经在执行中，请勿重复运行", "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            #endregion
            Form1 forms = new Form1();
            DialogResult dialogs = forms.ShowDialog();
            if (dialogs == DialogResult.Yes)  //接着继续
            {
                SysStatus.Status = SysStatus.EmSysSta.Run;
                OldRun();

            }
            if (dialogs == DialogResult.No)  //重新开始
            {            
                //清除状态和结果记录
                RunBuf.ClearSta();
                hardware.delijie1.ReceiveStringData = "";//把扫码枪扫到的码清零
                SysStatus.Status = SysStatus.EmSysSta.Run;
                label2.Text = "设备运行中……";
                label2.BackColor = Color.Lime;
                NewRun();
            }
            if (dialogs == DialogResult.Cancel) //取消返回
            {             
                button1.Text = "取消返回";
                return;
            }
        }
        #region 运行线程
        /// <summary>
        /// 重新开始线程
        /// </summary>
        Task  Run_task = null;
         
        public void NewRun()
        {
            if ( Run_task == null ||  Run_task != null &&  Run_task.IsCompleted)
            {
                Logger.Write(Logger.InfoType.Info, $" 创建重新运行线程");
                if ( Run_task != null)  Run_task.Dispose();
                 //Run_task = new Task(RunNew_th());
                 //Run_task.Start();
                Run_task = Task.Factory.StartNew(() => {               
                    RunNew_th();              
                }, TaskCreationOptions.None);
            }
            else
            {
                Logger.Error("Run_taskk线程未退出 ,无法创建!");
                MessageBox.Show("Run_taskk线程未退出 ,无法创建!");
            }
        }
        public void OldRun()
        {
            if (Run_task == null || Run_task != null && Run_task.IsCompleted)
            {
                Logger.Write(Logger.InfoType.Info, $" 创建重新运行线程");
                if (Run_task != null) Run_task.Dispose();
                //Run_task = new Task(RunOld_th());
                //Run_task.Start();
                Run_task = Task.Factory.StartNew(() => {                   
                    RunOld_th();                     
                }, TaskCreationOptions.None);
            }
            else
            {
                Logger.Error("Run_taskk线程未退出 ,无法创建!");
                MessageBox.Show("Run_taskk线程未退出 ,无法创建!");
            }
        }

        public void RunNew_th()
        {
            //try
            //{
                hardware.ComRun1.Quit = false;
                hardware.ComRun2.Quit = false;
                hardware.ComRun3.Quit = false;
                hardware.ComRun4.Quit = false;
                hardware.ComRun5.Quit = false;
                hardware.ComRun6.Quit = false;
                hardware.ComRun1.RunWaitCordTask();
                Logger.Debug($"工位1线程启动完成");
                int a = 0;
                
                while (SysStatus.Status == SysStatus.EmSysSta.Run & a <4)
                {
                    if (RunBuf.Station1 >= 3 & a==0)
                    {
                        a++;
                        hardware.ComRun2.RunWaitCordTask();
                        Logger.Debug($"工位2线程启动完成");
                    }
                    if (RunBuf.Station1 >= 7 & a == 1)
                    {
                        a++;
                        hardware.ComRun3.RunWaitCordTask();
                        Logger.Debug($"工位3线程启动完成");
                    }
                    if (RunBuf.Station1 >= 11 & a == 2)
                    {
                        a++;
                        hardware.ComRun4.RunWaitCordTask();
                        Logger.Debug($"工位4线程启动完成");
                    }
                    if (RunBuf.Station1 >= 15 & a == 3)
                    {
                        a++;
                        hardware.ComRun5.RunWaitCordTask();
                        Logger.Debug($"工位5线程启动完成");
                    }
                    if (RunBuf.Station1 >= 19 & a == 4)
                    {
                     
                        hardware.ComRun6.RunWaitCordTask();
                        Logger.Debug($"工位6线程启动完成");
                        a++;
                    }
                }             
            //}
            //catch (Exception ex)
            //{
            //    Logger.Debug($"系统运行线程出错!{ex.Message}");
            //    MessageBox.Show($"系统运行线程出错!{ex.Message}");
            //    SysStatus.Status = SysStatus.EmSysSta.Err;
            //    Run_task.Dispose();
            //    return;
            //}
        }
        public void RunOld_th()
        {
            try
            {
                hardware.ComRun1.Quit = false;
                hardware.ComRun2.Quit = false;
                hardware.ComRun3.Quit = false;
                hardware.ComRun4.Quit = false;
                hardware.ComRun5.Quit = false;
                hardware.ComRun6.Quit = false;
                int a = 0;
                if (RunBuf.Station1>0 & RunBuf.Station2>0 & RunBuf.Station3 > 0 & RunBuf.Station4 > 0 & RunBuf.Station5 > 0 & RunBuf.Station6 > 0)
                {
                    hardware.ComRun1.GoStation();
                    hardware.ComRun2.GoStation();
                    hardware.ComRun3.GoStation();
                    hardware.ComRun4.GoStation();
                    hardware.ComRun5.GoStation();
                    hardware.ComRun6.GoStation();
                }
                else if (RunBuf.Station1 > 0 & RunBuf.Station2 > 0 & RunBuf.Station3 > 0 & RunBuf.Station4 > 0 & RunBuf.Station5 > 0 & RunBuf.Station6 == 0)
                {
                    hardware.ComRun1.GoStation();
                    hardware.ComRun2.GoStation();
                    hardware.ComRun3.GoStation();
                    hardware.ComRun4.GoStation();
                    hardware.ComRun5.GoStation();
                    while (SysStatus.Status == SysStatus.EmSysSta.Run & a < 1)
                    {                         
                        if (RunBuf.Station1 >= 19 & a == 0)
                        {
                            hardware.ComRun6.GoStation();
                            a++;
                        }
                    }
                }
                else if (RunBuf.Station1 > 0 & RunBuf.Station2 > 0 & RunBuf.Station3 > 0 & RunBuf.Station4 > 0 & RunBuf.Station5 == 0 & RunBuf.Station6 == 0)
                {
                    hardware.ComRun1.GoStation();
                    hardware.ComRun2.GoStation();
                    hardware.ComRun3.GoStation();
                    hardware.ComRun4.GoStation();
                    while (SysStatus.Status == SysStatus.EmSysSta.Run & a < 2)
                    {
                        if (RunBuf.Station1 >= 15 & a == 0)
                        {
                            a++;
                            hardware.ComRun5.GoStation();
                        }
                        if (RunBuf.Station1 >= 19 & a == 1)
                        {

                            hardware.ComRun6.GoStation();
                            a++;
                        }
                    }                                                          
                }
                else if (RunBuf.Station1 > 0 & RunBuf.Station2 > 0 & RunBuf.Station3 > 0 & RunBuf.Station4 == 0 & RunBuf.Station5 == 0 & RunBuf.Station6 == 0)
                {
                    hardware.ComRun1.GoStation();
                    hardware.ComRun2.GoStation();
                    hardware.ComRun3.GoStation();                
                    while (SysStatus.Status == SysStatus.EmSysSta.Run & a < 3)
                    {
                        if (RunBuf.Station1 >= 11 & a == 0)
                        {
                            a++;
                            hardware.ComRun4.GoStation();
                        }
                        if (RunBuf.Station1 >= 15 & a == 1)
                        {
                            a++;
                            hardware.ComRun5.GoStation();
                        }
                        if (RunBuf.Station1 >= 19 & a == 2)
                        {

                            hardware.ComRun6.GoStation();
                            a++;
                        }
                    }
                }
                else if (RunBuf.Station1 > 0 & RunBuf.Station2 > 0 & RunBuf.Station3 == 0 & RunBuf.Station4 == 0 & RunBuf.Station5 == 0 & RunBuf.Station6 == 0)
                {
                    hardware.ComRun1.GoStation();
                    hardware.ComRun2.GoStation();
                    while (SysStatus.Status == SysStatus.EmSysSta.Run & a < 4)
                    {
                        if (RunBuf.Station1 >= 7 & a == 0)
                        {
                            a++;
                            hardware.ComRun3.GoStation();
                        }
                        if (RunBuf.Station1 >= 11 & a == 1)
                        {
                            a++;
                            hardware.ComRun4.GoStation();
                        }
                        if (RunBuf.Station1 >= 15 & a == 2)
                        {
                            a++;
                            hardware.ComRun5.GoStation();
                        }
                        if (RunBuf.Station1 >= 19 & a == 3)
                        {
                            hardware.ComRun6.GoStation();
                            a++;
                        }
                    }
                }
                else if (RunBuf.Station1 > 0 & RunBuf.Station2 == 0 & RunBuf.Station3 == 0 & RunBuf.Station4 == 0 & RunBuf.Station5 == 0 & RunBuf.Station6 == 0)
                {
                    hardware.ComRun1.GoStation();
                    while (SysStatus.Status == SysStatus.EmSysSta.Run & a < 5)
                    {
                        if (RunBuf.Station1 >= 3 & a == 0)
                        {
                            a++;
                            hardware.ComRun2.GoStation();
                        }
                        if (RunBuf.Station1 >= 7 & a == 1)
                        {
                            a++;
                            hardware.ComRun3.GoStation();
                        }
                        if (RunBuf.Station1 >= 11 & a == 2)
                        {
                            a++;
                            hardware.ComRun4.GoStation();
                        }
                        if (RunBuf.Station1 >= 15 & a == 3)
                        {
                            a++;
                            hardware.ComRun5.GoStation();
                        }
                        if (RunBuf.Station1 >= 19 & a == 4)
                        {
                            hardware.ComRun6.GoStation();
                            a++;
                        }
                    }
                }
                else if (RunBuf.Station1 == 0 & RunBuf.Station2 == 0 & RunBuf.Station3 == 0 & RunBuf.Station4 == 0 & RunBuf.Station5 == 0 & RunBuf.Station6 == 0)
                {
                    RunNew_th();
                }
                else
                {
                    hardware.ComRun1.GoStation();
                    hardware.ComRun2.GoStation();
                    hardware.ComRun3.GoStation();
                    hardware.ComRun4.GoStation();
                    hardware.ComRun5.GoStation();
                    hardware.ComRun6.GoStation();
                }

            }
            catch (Exception ex)
            {
                Logger.Debug($"系统运行线程出错!{ex.Message}");
                MessageBox.Show($"系统运行线程出错!{ex.Message}");
                SysStatus.Status = SysStatus.EmSysSta.Err;
                Run_task.Dispose();
                return;
            }
        }
        #endregion
        private void button2_Click(object sender, EventArgs e)//停止按钮
        {
            hardware.ComRun1.Quit = true;
            hardware.ComRun2.Quit = true;
            hardware.ComRun3.Quit = true;
            hardware.ComRun4.Quit = true;
            hardware.ComRun5.Quit = true;
            hardware.ComRun6.Quit = true;
            SysStatus.Status = SysStatus.EmSysSta.Standby;
            label2.Text = "系统已停止……";
            label2.BackColor = Color.Yellow;
            Logger.Info("系统已停止……button2_Click");
            //int aa = 0;
            //Task.Run(() =>
            //{
            //    //DialogResult ee = Alarm.ShowDialog(Color.Red, "报警:", "模号检测结果NG，是重新放一个测试，还是放弃？", "重新测", "放弃");
            //    //MessageBox.Show(ee.ToString());
            //    while (true)
            //    {
            //        RunBuf.Station1 = aa;
            //        RunBuf.Station2 = aa;
            //        RunBuf.Station3 = aa;
            //        RunBuf.Station4 = aa;
            //        RunBuf.Station5 = aa;
            //        RunBuf.Station6 = aa;
            //        Thread.Sleep(1000);
            //        Application.DoEvents();
            //        aa++;
            //        if (aa > 24) aa = 0;
            //    }
            //});
        }




        #endregion
       
        int xin = 0;
        private void timer1_Tick(object sender, EventArgs e)//心跳
        {
            if(hardware.PLC_KEY.IsConnected)
            {
                //if(xin==0)
                //{
                //    hardware.PLC_KEY.writebit("B1000", 1, "1");
                //    xin = 1;
                //}
                //else
                //{
                //    hardware.PLC_KEY.writebit("B1000", 1, "0");
                //    xin = 0;
                //}
                
            }
            timer1.Interval = 1000;
        }
        #region 扫码枪扫码接收事件
        //private bool delijierecei = false;
        //private barcode_delijie delijies = hardware.delijie1;
        //private barcode_jienshi jienshis =hardware.jienshi1;
        //#region 基恩士收发事件
        //private void SHUA(barcode_jienshi SSS)
        //{
        //    SSS.jieshou += myget;
            
        //}
        //private void myget(object sender, barcode_jienshi.PaperC e)
        //{
        //    Invoke(new Action(() => {
        //       label2.Text=($"基恩士扫码器扫码成功:{e.Name}");
                
        //    }));
        //}
       
        //#endregion
        //#region 得利捷收发事件
        //private void SHUA1(barcode_delijie SSS1)
        //{
        //    SSS1.jieshou1 += myget1;         
        //}
        //private void myget1(object sender, barcode_delijie.PaperC e)
        //{
        //    Invoke(new Action(() => {
        //        delijierecei = true;
        //        label2.Text = ($"得利捷扫码枪扫码成功:{e.Name}");
        //    }));
        //}
        //#endregion
        #endregion

        #region Form事件
        private void MainForm_Resize(object sender, EventArgs e)//窗口变化时改变字体
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                BeginInvoke(new Action(() => {
                    mainrRun1.textchangebig();
                }));          
            }
            if (this.WindowState == FormWindowState.Normal)
            {
                BeginInvoke(new Action(() => {
                    mainrRun1.textchangesmol();
                }));             
            }         
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            keyplC_Control11.init();
            barCordControl1.init();
            datetime1.strats();
            hardwarContr1.init();
            test7631Control1.init();
            visioncamControl1.init();
            datasSetup1.init();
            productSta1.init();
            mainrRun1.init();
            s7_12001.init();
            ioMonitor1.init();
            hanslaser1.init(null);
            Logger.Init(dataGridView1, Logger.InfoType.All);
            ColseStartTip();
            timer1.Enabled = true;
            //SHUA(jienshis);
            //SHUA1(delijies);
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)//关闭时保存当前状态及结果
        {
            RunBuf.SaveStaData("");
           
            mainrRun1.SaveText();
            //Dictionary<string, Hanslaser> aaa = new Dictionary<string, Hanslaser>();
            //aaa["aa"].mdescrible = "";
            ParamAttribute.SaveToDb(typeof(SysStatus), SysStatus.SysCfgPath);
            GC.Collect();
        }
        #endregion
    }
}
