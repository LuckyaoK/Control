using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using CXPro001.myclass;
using CXPro001.setups;
using CXPro001.classes;
using CXPro001.controls;

using MyLib.Param;
using MyLib.SqlHelper;
using MyLib.OldCtr;
using MyLib.Files;


namespace CXPro001.forms
{
    #region 委托事件
    /// <summary>
    /// 显示下料结果LED颜色
    /// </summary>
    /// <param name="c1">纤片检测</param>
    /// <param name="c2">螺帽检测</param>
    /// <param name="c3">耐压检测</param>
    /// <param name="c4">扫码</param>
    /// <param name="c5">下料</param>
    /// <param name="c6"></param>
    /// <param name="c7"></param>
    /// <param name="c8"></param>
    public delegate void ShowLEDColor(string cord, Color c1, Color c2, Color c3, Color c4, Color c5, Color c6, Color c7, Color c8);

    #endregion
    public partial class FormRun : Form
    {


        #region 实例化   
        private RunMany RunMany1 = null;//运行线程
        private int INit_flag = 1;
        private int door_time = 0;
        private int light_flag = 0;

        private int PLC_X1=0, PLC_X2=1;
        private int PLC_TIME = 0;
        #endregion

        #region 生产数据
        /// <summary>
        /// 显示数据频率
        /// </summary>
        public int sql_time;
        /// <summary>
        /// 各工序生产OK数量
        /// </summary>
        public int ST1CountOK, ST2CountOK, ST3CountOK, ST4CountOK, ST5CountOK, ST6CountOK, ST7CountOK, ST8CountOK, ST9CountOK, ST100CountOK;
        /// <summary>
        /// 各工序生产NG数量
        /// </summary>
        public int ST1CountNG, ST2CountNG, ST3CountNG, ST4CountNG, ST5CountNG, ST6CountNG, ST7CountNG, ST8CountNG, ST9CountNG, ST100CountNG;
        public List<int> STcount
        {
            get
            {
                List<int> aa = new List<int> { ST1CountOK, ST2CountOK, ST3CountOK, ST4CountOK, ST5CountOK, ST6CountOK, ST7CountOK, ST8CountOK, ST9CountOK, ST100CountOK,
                ST1CountNG, ST2CountNG, ST3CountNG, ST4CountNG, ST5CountNG, ST6CountNG, ST7CountNG, ST8CountNG, ST9CountNG, ST100CountNG };
                return aa;
            }
        }



        #endregion
        public FormRun()
        {
            InitializeComponent();//界面初始化

            loadini();//加载配置文件            
            SysStatus.LoadConfig(SysStatus.sys_dir_path);//设备信息                                                              
            SysStatus.CurUser = new User($"作业员, ,{User.Permission.Anonymous}");//写入用户名，密码，超级用户
           
        }

        private void FormRun_Load(object sender, EventArgs e)//初始化
        {
            Logger.Init(dataGridView1, Logger.InfoType.All);//日志与控件关联       

            Logger.Info($"程序启动完成！{DateTime.Now.ToString("HH: mm:ss.fff")}");//写入日志

            //  PLC_INI();
            //---------------hardware.Init(iTcpClient_XR1,iTcpClient_XR2,iTcpClient_LR,iTcpClient_CCD1,iTcpClient_CCD2,iTcpClient_CCD3,iTcpClient_CCD4);//初始化硬件
            //------------  Time_sys.Enabled = true;



        }
        /// <summary>
        /// 硬件初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Init_Tick(object sender, EventArgs e)
        {
            bool re = true;
            re = hardware.Init(INit_flag, iTcpClient_XR1, iTcpClient_XR2, iTcpClient_LR, iTcpClient_CCD1, iTcpClient_CCD2, iTcpClient_CCD3, iTcpClient_CCD4);//初始化硬件
            INit_flag++;

            if (re == false || INit_flag > 21)
            {
                Timer_Init.Enabled = false;

                if (INit_flag > 21)
                {
                    btn_SystemStart.Enabled = true;
                    button5.Enabled = true;
                    Time_sys.Enabled = true;
                    timer_protect.Enabled = true;
                    Time_code.Enabled = true;
                    ccD_Pos_ShowCtr1.Load_Postion(0);//加载上限值
                     ccD_High_ShowCtr1.Load_High(0);
                  ccD_Pos_ShowCtr2.Load_Postion(1);//加载上限值
                    ccD_High_ShowCtr2.Load_High(1);
                }
            }
        }


        /// <summary>
        /// 读取参数目录
        /// </summary>
        public void loadini()
        {
            string file = System.Windows.Forms.Application.StartupPath + @"\sys.ini";
            IniFile inf = new IniFile(file);//确认路径是否存在，不存在则创建文件夹。
            SysStatus.sys_dir_path = inf.ReadString("main", "1", "");
        }


        #region 窗口点击按钮
        private FormSetting cordform = null;
        private FrmLogin frmAbout = null;
        private FormTest iO16Form1 = null;
        private FormSQL formsql = null;


        private async void btn_SystemStart_Click(object sender, EventArgs e)//启动按钮
        {
            /////////////////////////////////////

            if (hardware.my_ccd1.isCon == false)
            {
                MessageBox.Show("相机1未连接");
                return;
            }
            if (hardware.my_ccd1.isCon == false)
            {
                MessageBox.Show("相机2未连接");
                return;
            }
            if (hardware.my_ccd1.isCon == false)
            {
                MessageBox.Show("相机3未连接");
                return;
            }
            if (hardware.my_ccd1.isCon == false)
            {
                MessageBox.Show("相机4未连接");
                return;
            }
            if (hardware.my_io.m_input[1, 10] == false || hardware.my_io.m_input[1, 12] == false || hardware.my_io.m_input[2, 0] == false)  //检测台下降气缸到位
            {
                MessageBox.Show("耐压或这相机下压未退出");
                return;
            }
            if (SysStatus.Modle == 2)//自动
            {
                if(SysStatus.CurProductName=="M381")
                {
                    MessageBox.Show("m381产品只能工人上料");
                    return;
                }
            }

                if (SysStatus.Status == SysStatus.EmSysSta.Run)//运行改暂停
            {
                DialogResult result = MessageBox.Show("当前正在运行，暂停后无法继续", "确定继续", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    RunMany1.Clean_Th();
                    Logger.Info($"线程终止");//写入日志.
                    SysStatus.Status = SysStatus.EmSysSta.Standby;
                    Thread.Sleep(600);
                    return;
                }
                else
                {
                    return;
                }


            }

            await Task.Run(new Action(() =>
            {
                SysStatus.Status = SysStatus.EmSysSta.Run;//系统状态
                RunMany1 = new RunMany();//委托与类  
                RunMany1.RunNew();//启动线程

            }));


        }
  

        private void button5_Click(object sender, EventArgs e)//IO时序窗口
        {
            if (iO16Form1 == null)
            {
                iO16Form1 = new FormTest();
                iO16Form1.Show();
            }
            if (iO16Form1 != null && iO16Form1.Created)
            {
                iO16Form1.BringToFront();
            }
            if (iO16Form1 != null && !iO16Form1.Created)
            {
                iO16Form1.Dispose();
                iO16Form1 = new FormTest();
                iO16Form1.Show();
            }

        }

        private void button6_Click(object sender, EventArgs e)//数据查询窗口
        {
            if (formsql == null)
            {
                formsql = new FormSQL();
                formsql.Show();
            }
            if (formsql != null & !formsql.Created)
            {
                formsql.Dispose();
                formsql = new FormSQL();
                formsql.Show();

            }
            if (formsql != null & formsql.Created)
            {
                formsql.BringToFront();
            }
        }

        private void button10_Click(object sender, EventArgs e)//参数设置窗口
        {
            if (SysStatus.CurUser.Name != "管理员")
            {
                MessageBox.Show("用户权限不够");
                return;
            }

            if (cordform == null)
            {
                cordform = new FormSetting();
                cordform.Show();
            }
            if (cordform != null & !cordform.Created)
            {
                cordform.Dispose();
                cordform = new FormSetting();
                cordform.Show();

            }
            if (cordform != null & cordform.Created)
            {
                cordform.BringToFront();
            }
        }

        private void button1_Click(object sender, EventArgs e)//用户登录窗口
        {
            if (frmAbout == null)
            {
                frmAbout = new FrmLogin();
                frmAbout.Show();
            }
            if (frmAbout != null & !frmAbout.Created)
            {
                frmAbout.Dispose();
                frmAbout = new FrmLogin();
                frmAbout.Show();

            }
            if (frmAbout != null & frmAbout.Created)
            {
                frmAbout.BringToFront();
            }


        }

        private void button3_Click(object sender, EventArgs e)//退出窗口
        {
            if (RunMany1 != null)
            {
                if(RunMany1.TH_Sts==1)
                {
                    MessageBox.Show("线程已经打开，请先停止");
                    return;
                }

            }

            hardware.my_cord.SaveSTcord();
            SysStatus.SaveConfig(SysStatus.sys_dir_path);
            Close();
        }

        private void button8_Click(object sender, EventArgs e)//产品点检窗口
        {
            //  MessageBox.Show("设备未连网，暂时无法打开");
            if (RunMany1==null || RunMany1.TH_Sts == 0)
            {
                Frm_Set_Modle aa = new Frm_Set_Modle();
                aa.ShowDialog();
            }else
            {
                MessageBox.Show("请先终止线程");
            }
        }
        private void button7_Click(object sender, EventArgs e)//异常上报窗口
        {
            if (SysStatus.Modle != 2)
            {
                MessageBox.Show("自动模式下有效");
                return;
            }
             
            if (RunMany1 != null)
            {
                if (RunMany1.Clean_auto > 0)
                {
                    RunMany1.Set_Auto_Clean(0);
                    Logger.Info($"取消清料！");//写入日志.
                }
                else
                {
                    RunMany1.Set_Auto_Clean(5);
                    Logger.Info($"清料中！");//写入日志.
                }
            }
            else
            {
                MessageBox.Show("线程未启动");
            }
           


        }
        //---------------------以上菜单---------------------------------------

        #endregion
        #region 窗体移动
        private Point mouseOff;//鼠标移动位置变量
        private bool leftFlag;//标签是否为左键
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }
        #endregion

        #region timer  事件
        /// <summary>
        /// 刷新外设连接状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            #region 面板显示  异步
            BeginInvoke(new Action(() =>
            {

                hardware.my_io.Get_All();
                hardware.my_motion.Get_Pos();


            }));
            #endregion


        }


        /// <summary>
        /// 从数据计算当前班别的生产数量
        /// </summary>
        public void LoadDay()
        {
            string banbie = "";
            int aa = DateTime.Now.Hour;
            if (aa <= 20 && aa >= 8) banbie = "白班";
            DateTime dt1  = (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))).AddHours(8);
             
            DateTime dt2 = dt1.AddDays(1.0);
            if (banbie == "白班")
            {
                dt1 = (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))).AddHours(8); //8
              
                dt2 = dt1.AddHours(12);
            }
          //  else if (aa >= 0 && aa < 8)//后夜班
          //  {
           //     dt1 = (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")).AddHours(-4));
           //     dt2 = dt1.AddHours(12);
         //   }
            else //if (aa >= 20 && aa < 24)//前夜班
            {
                dt1 = (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")).AddHours(20));
                dt2 = dt1.AddHours(12);
            }
            string tpname = SysStatus.CurProductName;//  "ResultVoltage,ResultHigh,ResultPostion,ResultSaoMark,ModelNumber," +



            string sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultVoltage = '1'";
            ST1CountOK = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));

            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultVoltage = '0'";
            ST1CountNG = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));



            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultHigh1 = '1'";
            ST2CountOK = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));

            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultHigh1 = '0'";
            ST2CountNG = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));


            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultPostion1 = '1'";
            ST3CountOK = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));

            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultPostion1 = '0'";
            ST3CountNG = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));


            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultHigh2 = '1'";
            ST4CountOK = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));

            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultHigh2 = '0'";
            ST4CountNG = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));

            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultPostion2 = '1'";
            ST5CountOK = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));

            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultPostion2 = '0'";
            ST5CountNG = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));


            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultLaser = '1'";
            ST6CountOK = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));

            sql = $"select  count(*) from EndResultDB where InsertTime between '{dt1}' and '{dt2}' and ProductType = '{tpname}'  and ResultLaser = '0'";
            ST6CountNG = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));




            sql = $"select  count(*) from EndResultDB where InsertTime  between '{dt1}' and '{dt2}' and  ProductType = '{tpname}'  and ResultEnd = '1'";
            ST7CountOK = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));

            sql = $"select  count(*) from EndResultDB where InsertTime  between '{dt1}' and '{dt2}' and  ProductType = '{tpname}'  and ResultEnd = '0'";
            ST7CountNG = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));

           
            //    ST7Lv.Text = ((Convert.ToDouble(ST7OK.Text) / (Convert.ToDouble(ST7OK.Text) + Convert.ToDouble(ST7NG.Text))) * 100).ToString("f2") + "%";


            sql = $"select  count(*) from EndResultDB where   ProductType = '{tpname}'  and ResultEnd = '1'";
            ST8CountOK = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));
            sql = $"select  count(*) from EndResultDB where    ProductType = '{tpname}'  and ResultEnd = '0'";
            ST8CountNG = Convert.ToInt32(SQLHelper.ExecuteScalar(sql));

            proCountDispl1.ShowBanBie(STcount);
            //////////////////////////////////////////////////////////

            String str;

            str = "DB100.12.0";
            hardware.plc.Write(str, ST7CountOK + ST7CountNG);//耐压NG
            str = "DB100.16.0";
            hardware.plc.Write(str, ST7CountOK);//插口高度NG
            str = "DB100.20.0";
            hardware.plc.Write(str, ST7CountNG);//插口位置


            str = "DB100.24.0";
            hardware.plc.Write(str, ST8CountOK+ ST8CountNG);//耐压NG
            str = "DB100.28.0";
            hardware.plc.Write(str, ST8CountOK);//插口高度NG
            str = "DB100.32.0";
            hardware.plc.Write(str, ST8CountNG);//插口位置


            str ="DB100.40.0";
            hardware.plc.Write(str, ST1CountNG);//耐压NG
             str = "DB100.44.0";
            hardware.plc.Write(str, ST2CountNG);//插口高度NG
             str = "DB100.48.0";
            hardware.plc.Write(str, ST3CountNG);//插口位置
             str = "DB100.52.0";
            hardware.plc.Write(str, ST4CountNG);//内部高度
             str = "DB100.56.0";
            hardware.plc.Write(str, ST5CountNG);//内部位置
             str = "DB100.69.0";
            hardware.plc.Write(str, ST6CountNG);//扫码
             
        }

        private void timer2_Tick(object sender, EventArgs e)//读扫码枪用
        {
            #region 实时显示
            BeginInvoke(new Action(() =>
            {


                //////////////////////////////////////////////////////
                //  if (SysStatus.Status == SysStatus.EmSysSta.Run  )
                // {
                try
                {
                    DateTime date1 = DateTime.Now;
                    if (FrmLogin.loginSucceedFlag)
                    {
                        DateTime date2 = DateTime.Now;
                        TimeSpan ts = date2 - FrmLogin.dt;
                        if (ts.TotalMinutes > 5)
                        {
                            User user = new User($"作业员,123,{User.Permission.Superuser}");
                            SysStatus.CurUser.Name = "无权限";
                            FrmLogin.loginSucceedFlag = false;
                            Logger.Info($"5分钟权限自动注销!");
                        }

                    }



                    lbl_LoginName.Text = SysStatus.CurUser.Name;
                    lbl_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    lbl_Time.Text = DateTime.Now.ToString("HH:mm:ss");
                    label10.Text = $"当前产品:" + SysStatus.CurProductName;
                    //  var result =hardware.plc.ConnectServer();
                    if (SysStatus.Modle == 1)
                        pic_hand.Image = CXPro001.Properties.Resources.hand;
                    if (SysStatus.Modle == 2)
                        pic_hand.Image = CXPro001.Properties.Resources.auto;
                    if (SysStatus.Modle == 3)
                        pic_hand.Image = CXPro001.Properties.Resources.clean;

                    if (SysStatus.Protect == 0)
                        pic_protect.Visible = true;
                    else
                        pic_protect.Visible = false;

                    //  var result =hardware.plc.ConnectServer();
                    //外设链接状态

                    if (hardware.plc_isCon) P1_Show_name1.DataBack_Color = ColorType1.Green; else P1_Show_name1.DataBack_Color = ColorType1.Normal;  //PLC状态灯
                    if (hardware.m_8740.IsConnected) P1_Show_name2.DataBack_Color = ColorType1.Green; else P1_Show_name2.DataBack_Color = ColorType1.Normal;                                                                                                                             //
                    if (hardware.my_cxr1.isCon) P1_Show_name3.DataBack_Color = ColorType1.Green; else P1_Show_name3.DataBack_Color = ColorType1.Normal;
                    if (hardware.my_cxr2.isCon) P1_Show_name4.DataBack_Color = ColorType1.Green; else P1_Show_name4.DataBack_Color = ColorType1.Normal;
                    if (hardware.my_laser.isCon) P1_Show_name5.DataBack_Color = ColorType1.Green; else P1_Show_name5.DataBack_Color = ColorType1.Normal;
                    if (hardware.my_ccd1.isCon) P1_Show_name6.DataBack_Color = ColorType1.Green; else P1_Show_name6.DataBack_Color = ColorType1.Normal;
                    if (hardware.my_ccd2.isCon) P1_Show_name7.DataBack_Color = ColorType1.Green; else P1_Show_name7.DataBack_Color = ColorType1.Normal;
                    if (hardware.my_ccd3.isCon) P1_Show_name8.DataBack_Color = ColorType1.Green; else P1_Show_name8.DataBack_Color = ColorType1.Normal;
                    if (hardware.my_ccd4.isCon) P1_Show_name9.DataBack_Color = ColorType1.Green; else P1_Show_name9.DataBack_Color = ColorType1.Normal;

                    //二维码
                    montionShowCtr1.ShowCoder(1, 1, hardware.my_cord.my_Pro[1].Cord_ST_L);
                    montionShowCtr1.ShowCoder(1, 2, hardware.my_cord.my_Pro[1].Cord_ST_R);
                    montionShowCtr1.ShowCoder(2, 1, hardware.my_cord.my_Pro[2].Cord_ST_L);
                    montionShowCtr1.ShowCoder(2, 2, hardware.my_cord.my_Pro[2].Cord_ST_R);
                    montionShowCtr1.ShowCoder(3, 1, hardware.my_cord.my_Pro[3].Cord_ST_L);
                    montionShowCtr1.ShowCoder(3, 2, hardware.my_cord.my_Pro[3].Cord_ST_R);
                    montionShowCtr1.ShowCoder(4, 1, hardware.my_cord.my_Pro[4].Cord_ST_L);
                    montionShowCtr1.ShowCoder(4, 2, hardware.my_cord.my_Pro[4].Cord_ST_R);
                    montionShowCtr1.ShowCoder(5, 1, hardware.my_cord.my_Pro[5].Cord_ST_L);
                    montionShowCtr1.ShowCoder(5, 2, hardware.my_cord.my_Pro[5].Cord_ST_R);
                    montionShowCtr1.ShowCoder(6, 1, hardware.my_cord.my_Pro[6].Cord_ST_L);
                    montionShowCtr1.ShowCoder(6, 2, hardware.my_cord.my_Pro[6].Cord_ST_R);
                    //坐标
                    montionShowCtr1.ShowPos(1, hardware.my_motion.m_EcatEncPos[1].ToString());
                    montionShowCtr1.ShowPos(2, hardware.my_motion.m_EcatEncPos[2].ToString());
                    montionShowCtr1.ShowPos(3, hardware.my_motion.m_EcatEncPos[3].ToString());
                    montionShowCtr1.ShowPos(4, hardware.my_motion.m_EcatEncPos[4].ToString());
                    montionShowCtr1.ShowPos(5, hardware.my_motion.m_EcatEncPos[5].ToString());
                    montionShowCtr1.ShowPos(6, hardware.my_motion.m_EcatEncPos[6].ToString());
                    //显示流程步

                    if (RunMany1 != null)
                    {
                        montionShowCtr1.ShowStep(1, RunMany1.Get_TH_Flag(1));
                        montionShowCtr1.ShowStep(2, RunMany1.Get_TH_Flag(2));
                        montionShowCtr1.ShowStep(3, RunMany1.Get_TH_Flag(3));
                        montionShowCtr1.ShowStep(4, RunMany1.Get_TH_Flag(4));
                        montionShowCtr1.ShowStep(5, RunMany1.Get_TH_Flag(5));
                        montionShowCtr1.ShowStep(6, RunMany1.Get_TH_Flag(6));
                        montionShowCtr1.ShowStep(7, RunMany1.Get_TH_Flag(7));
                        montionShowCtr1.ShowStep(8, RunMany1.Get_TH_Flag(8));
                        montionShowCtr1.ShowStep(9, RunMany1.Get_TH_Flag(9));

                    }
                    //////磨具号///

                    montionShowCtr1.ShowModel(1, 1, hardware.my_cord.my_Pro[1].Model_Num_L.ToString());
                    montionShowCtr1.ShowModel(1, 2, hardware.my_cord.my_Pro[1].Model_Num_R.ToString());

                    montionShowCtr1.ShowModel(2, 1, hardware.my_cord.my_Pro[2].Model_Num_L.ToString());
                    montionShowCtr1.ShowModel(2, 2, hardware.my_cord.my_Pro[2].Model_Num_R.ToString());

                    montionShowCtr1.ShowModel(3, 1, hardware.my_cord.my_Pro[3].Model_Num_L.ToString());
                    montionShowCtr1.ShowModel(3, 2, hardware.my_cord.my_Pro[3].Model_Num_R.ToString());

                    montionShowCtr1.ShowModel(4, 1, hardware.my_cord.my_Pro[4].Model_Num_L.ToString());
                    montionShowCtr1.ShowModel(4, 2, hardware.my_cord.my_Pro[4].Model_Num_R.ToString());

                    montionShowCtr1.ShowModel(5, 1, hardware.my_cord.my_Pro[5].Model_Num_L.ToString());
                    montionShowCtr1.ShowModel(5, 2, hardware.my_cord.my_Pro[5].Model_Num_R.ToString());

                    montionShowCtr1.ShowModel(6, 1, hardware.my_cord.my_Pro[6].Model_Num_L.ToString());
                    montionShowCtr1.ShowModel(6, 2, hardware.my_cord.my_Pro[6].Model_Num_R.ToString());

                    /////耐压值
                    tS8740SshowCtr1.ShowRES(hardware.m_8740);

                    //////////////CCD//////////////////////
                    ccD_Pos_ShowCtr1.ShowParamPostion(hardware.my_ccd1);
                    ccD_High_ShowCtr1.ShowParamHigh(hardware.my_ccd4);

                    ccD_Pos_ShowCtr2.ShowParamPostion(hardware.my_ccd3);
                    ccD_High_ShowCtr2.ShowParamHigh(hardware.my_ccd2);
                    ///////////扫码///////////////////////
                    hanslerShowCtr1.ShowCord(hardware.my_cxr1.str, hardware.my_cxr2.str, null, null);
                    flowMeterShowCtr.ShowCord(hardware.my_meter.leftFlowMeter.ToString(), hardware.my_meter.rightFlowMeter.ToString(),
                        hardware.my_meter.leftResult, hardware.my_meter.rightResult,
                         hardware.my_cord.my_Pro[1].Cord_ST_L, hardware.my_cord.my_Pro[1].Cord_ST_R);
                    ///////////////滚动条///////////////////////
                    if (SysStatus.Status == SysStatus.EmSysSta.Run)//滚动条显示系统运行
                    {
                        xScrollingText1.Text = "系统运行中……";
                        xScrollingText1.ForeColor = Color.LimeGreen;
                        btn_SystemStart.BackColor = Color.LimeGreen;

                    }
                    else
                    {
                        xScrollingText1.Text = "系统未启动!";
                        xScrollingText1.ForeColor = Color.Red;
                        btn_SystemStart.BackColor = Color.Red;

                    }

                    PLC_TIME++;
                    if(PLC_TIME>10)
                    {
                        PLC_TIME = 0;
                        if (hardware.plc_isCon)
                        {
                            string str = "DB100.0.0";//判断1-1
                            int plcx = hardware.plc.ReadInt16(str).Content;  // 读取M100.0是否通断
                            ST0_PLC_HART.Text = plcx.ToString();
                            PLC_X2 = PLC_X1;
                            PLC_X1 = plcx;

                            if (PLC_X1 == PLC_X2)
                                hardware.Plc_Islive = false;
                        }
                    }
                    ///////////////界面数量统计/////////////////////////
                    
                    sql_time++;
                    if (sql_time > 50)
                    {
                        LoadDay();
                        sql_time = 0;
                    }
                    proCountDispl1.ShowTypeName(SysStatus.CurProductName);

                    ///////////////btn_y_reset_th//////////////////////////////////////
                    if (RunMany1 != null)
                    {
                        if (RunMany1.TH_Sts == 0)
                            btn_y_reset_th.Text = "程序未启动";
                        if (RunMany1.TH_Sts == 1)
                        {
                            btn_y_reset_th.Text = "当前运行中";
                            montionShowCtr1.ShowSts(1, 1);
                        }
                        if (RunMany1.TH_Sts == 2)
                        {
                            btn_y_reset_th.Text = "当前暂停中";
                            montionShowCtr1.ShowSts(1, 3);
                        }
                    }
                   

                    //////////////////关闭安全门////////////////
                    if (hardware.my_io.m_output[0, 5] == false)
                    {
                        door_time = 0;
                    }
                    else
                    {
                        door_time++;

                    }
                    if (door_time > 5)
                        hardware.my_io.Set_Do(0, 5, 0);
                    btn_y_open_door.Text = "开门" + door_time.ToString();


                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message);
                }
                // }
            }));
            #endregion
        }

        //初始化

        #endregion



        #region 委托实例
        /// <summary>
        /// 显示视觉图片
        /// </summary>
        /// <param name="res">结果</param>
        /// <param name="stas">工站</param>
        /// <param name="cord">二维码</param>
        public void ShowPictrue(bool res, int stas, string cord)//
        {
            Invoke(new Action(() =>
            {
            }));
        }

        private void pic_hand_DoubleClick(object sender, EventArgs e)
        {
            Form_Hand_Select aa = new Form_Hand_Select();
            aa.Show();
        }

        /// <summary>
        /// 下料二维码 及颜色显示
        /// </summary>
        /// <param name="cord"></param>
        /// <param name="CL1"></param>
        /// <param name="CL2"></param>
        /// <param name="CL3"></param>
        /// <param name="CL4"></param>
        /// <param name="CL5"></param>
        /// <param name="CL6"></param>
        /// <param name="CL7"></param>
        /// <param name="CL8"></param>
        /*  public void ShowDownRes(string cord, Color CL1, Color CL2, Color CL3, Color CL4, Color CL5, Color CL6, Color CL7, Color CL8)//显示下料结果
          {
              Invoke(new Action(() => {
              //    showName55.ShowDataRes(cord, MyLib.OldCtr.ColorType1.Normal);//二维码
                  showLED1.Color1 = CL1; showLED2.Color1 = CL2; showLED3.Color1 = CL3; showLED4.Color1 = CL4;
                  showLED5.Color1 = CL5;
                  //showLED6.Color1 = CL6; showLED7.Color1 = CL7; 
                  showLED8.Color1 = CL6;
              }));


          }*/
        #endregion

        #region TCP 接收
        //XR-1
        private void iTcpClient1_OnRecevice(object sender, NetWorkHelper.ICommond.TcpClientReceviceEventArgs e)
        {
            hardware.my_cxr1.onReceve(e.Data);
        }

        private void iTcpClient1_OnStateInfo(object sender, NetWorkHelper.ICommond.TcpClientStateEventArgs e)
        {
            if (e.State == NetWorkHelper.SocketState.Connected)
            {
                //连接
                hardware.my_cxr1.Set_Sts(true);
            }
            else
            {
                hardware.my_cxr1.Set_Sts(false);
            }
        }
        //XR-2
        private void iTcpClient2_OnRecevice(object sender, NetWorkHelper.ICommond.TcpClientReceviceEventArgs e)
        {
            hardware.my_cxr2.onReceve(e.Data);
        }

        private void iTcpClient2_OnStateInfo(object sender, NetWorkHelper.ICommond.TcpClientStateEventArgs e)
        {
            if (e.State == NetWorkHelper.SocketState.Connected)
            {
                //连接
                hardware.my_cxr2.Set_Sts(true);
            }
            else
            {
                hardware.my_cxr2.Set_Sts(false);
            }
        }
        //激光
        private void iTcpClient3_OnRecevice(object sender, NetWorkHelper.ICommond.TcpClientReceviceEventArgs e)
        {
            byte[] bb = e.Data;
            hardware.my_laser.onReceve(bb);
        }

        private void iTcpClient3_OnStateInfo(object sender, NetWorkHelper.ICommond.TcpClientStateEventArgs e)
        {
            if (e.State == NetWorkHelper.SocketState.Connected)
            {
                //连接  
                hardware.my_laser.Set_Sts(true);
            }
            else
            {
                hardware.my_laser.Set_Sts(false);
            }
        }


        //CCD1
        private void iTcpClient_CCD1_OnRecevice(object sender, NetWorkHelper.ICommond.TcpClientReceviceEventArgs e)
        {
            byte[] bb = e.Data;
            hardware.my_ccd1.onReceve(bb);
        }


        private void iTcpClient_CCD1_OnStateInfo(object sender, NetWorkHelper.ICommond.TcpClientStateEventArgs e)
        {
            if (e.State == NetWorkHelper.SocketState.Connected)
                hardware.my_ccd1.Set_Sts(true);
            else
                hardware.my_ccd1.Set_Sts(false);
        }

        //CCD2
        private void iTcpClient_CCD2_OnRecevice(object sender, NetWorkHelper.ICommond.TcpClientReceviceEventArgs e)
        {
            byte[] bb = e.Data;
            hardware.my_ccd2.onReceve(bb);
        }

        private void iTcpClient_CCD2_OnStateInfo(object sender, NetWorkHelper.ICommond.TcpClientStateEventArgs e)
        {
            if (e.State == NetWorkHelper.SocketState.Connected)
                hardware.my_ccd2.Set_Sts(true);
            else
                hardware.my_ccd2.Set_Sts(false);
        }
        //CCD3
        private void iTcpClient_CCD3_OnRecevice(object sender, NetWorkHelper.ICommond.TcpClientReceviceEventArgs e)
        {
            byte[] bb = e.Data;
            hardware.my_ccd3.onReceve(bb);
        }

        private void iTcpClient_CCD3_OnStateInfo(object sender, NetWorkHelper.ICommond.TcpClientStateEventArgs e)
        {
            if (e.State == NetWorkHelper.SocketState.Connected)
                hardware.my_ccd3.Set_Sts(true);
            else
                hardware.my_ccd3.Set_Sts(false);
        }
        //CCD4
        private void iTcpClient_CCD4_OnRecevice(object sender, NetWorkHelper.ICommond.TcpClientReceviceEventArgs e)
        {
            byte[] bb = e.Data;
            hardware.my_ccd4.onReceve(bb);
        }

        private void iTcpClient_CCD4_OnStateInfo(object sender, NetWorkHelper.ICommond.TcpClientStateEventArgs e)
        {
            if (e.State == NetWorkHelper.SocketState.Connected)
                hardware.my_ccd4.Set_Sts(true);
            else
                hardware.my_ccd4.Set_Sts(false);
        }
        #endregion



        #region 窗口2 链接按钮
        private void btn_y_reset_th_Click(object sender, EventArgs e)
        {
            if (RunMany1 == null)
                return;
            else if (RunMany1.TH_Sts == 0)
            {
                MessageBox.Show("线程未启动");
            }
            else if (RunMany1.TH_Sts == 1)
            {
                RunMany1.Stop_TH();
                Logger.Info($"所有线程停止！");//写入日志.
            }
            else if (RunMany1.TH_Sts == 2)
            {
                RunMany1.Clear_Error();
                RunMany1.Rest_TH();
                Logger.Info($"所有线程恢复！");//写入日志.
            }
            
        }

        private void btn_y_open_clam_Click(object sender, EventArgs e)
        {
            if (hardware.my_io.m_output[0, 0] == false)
                hardware.my_io.Set_Do(0, 0, 1);
            else
                hardware.my_io.Set_Do(0, 0, 0);
        }

        private void btn_y_open_fan_Click(object sender, EventArgs e)
        {
            if (hardware.my_io.m_output[0, 1] == false)
                hardware.my_io.Set_Do(0, 1, 1);
            else
                hardware.my_io.Set_Do(0, 1, 0);
        }

        private void btn_y_open_door_Click(object sender, EventArgs e)
        {
            if (hardware.my_io.m_output[0, 5] == false)
                hardware.my_io.Set_Do(0, 5, 1);
        }
        #endregion
        private void timer_protect_Tick(object sender, EventArgs e)
        {
            //碰撞保护线程
            if (SysStatus.Protect == 0)
            {
                //1轴在防护区
                /*  if (hardware.my_motion.is_protect_axis_1)
                  {
                      if (hardware.my_motion.m_PrfPos[5] <= SysStatus.Axis_Protect_Z[4])
                      {
                          if (hardware.my_motion.m_serv_run[5])
                          {
                              if (RunMany1 != null)
                                  RunMany1.Stop_TH();
                              hardware.my_motion.Stop(5);
                              Logger.Error($"横移电机在防护区，取料无法移动");//写入日志.
                          }
                      }
                  }
                  //5轴在防护区
                  if (hardware.my_motion.is_protect_axis_5)
                  {
                      if (hardware.my_motion.m_serv_run[1])
                      {
                          if (RunMany1 != null)
                              RunMany1.Stop_TH();
                          hardware.my_motion.Stop(1);
                          Logger.Error($"取料电机在防护区，横移无法移动");//写入日志.
                      }

                  }
                */
                if (hardware.my_motion.m_serv_run[1])//横移电机
                {
                    if (!hardware.my_io.m_input[1, 4])
                    {
                        if (RunMany1 != null)
                            RunMany1.Stop_TH();
                     
                        Logger.Error($"横移升降感应器未检测，无法移动");//写入日志.
                    }
                    if (!hardware.my_io.m_input[1, 6])
                    {
                        if (RunMany1 != null)
                            RunMany1.Stop_TH();
                       
                        Logger.Error($"横移前进感应器未检测，无法移动");//写入日志.
                    }
                }

                if (hardware.my_motion.m_serv_run[5])//取料
                {
                    if (!hardware.my_io.m_input[0, 12])
                    {
                        if (RunMany1 != null)
                            RunMany1.Stop_TH();
                      
                        Logger.Error($"取料加长未关闭，电机无法移动");//写入日志.
                    }
                    if (!hardware.my_io.m_input[0, 8])
                    {
                        if (RunMany1 != null)
                            RunMany1.Stop_TH();
                        
                        Logger.Error($"取料升降未关闭，电机无法移动");//写入日志.
                    }
                }

                if (hardware.my_motion.m_serv_run[2])//下料
                {
                    if (!hardware.my_io.m_input[2, 13])
                    {
                        if (RunMany1 != null)
                            RunMany1.Stop_TH();
                      
                        Logger.Error($"分料下降气缸未关闭，电机无法移动");//写入日志.
                    }
                }
            }

            if (hardware.my_io.m_input[0, 1])//急停
            {

                if (RunMany1 != null)
                    RunMany1.Stop_TH();

                Logger.Error($"急停触发，电机停止");//写入日志.

                 
             //   string   str = "DB100.2.2";//允许上料
            //    hardware.plc.Write(str, true);
          
            } 

            if (SysStatus.shield_light == 0)
            {
                if (hardware.my_io.m_input[0, 3] == false)
                {
                    if (RunMany1 != null && RunMany1.TH_Sts == 1)
                    {

                        RunMany1.Stop_TH();
                        light_flag = 1;

                    }


                    Logger.Error($"安全光栅触发，电机停止");//写入日志.
                }
                else
                {
                    if (RunMany1 != null && light_flag == 1)
                    {
                        light_flag = 0;
                        if (RunMany1.TH_Sts == 2)
                        {
                            RunMany1.Rest_TH();
                        }
                    }
                }
            }
            if (hardware.my_io.m_input[0, 2] && SysStatus.shield_door == 0)
            {
                if (RunMany1 != null)
                    RunMany1.Stop_TH();

                Logger.Error($"安全门未关闭，电机停止");//写入日志.
            }



        }


    }
}
