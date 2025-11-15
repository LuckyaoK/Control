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

using MyLib.OldCtr;
using CXPro001.myclass;
using System.Threading;
using static GTN.mc;
namespace CXPro001.forms
{
    public partial class FormTest : Form
    {
        

         My_8740 TS8740 = null;//耐压8740
        /// <summary>
        /// /////////////////
        /// </summary>
        byte[] bDoData = new byte[2] { 0, 0 };       //定义500扩展模输出初值
        byte[] bDiData = new byte[4];           //定义500扩展模输入 
        Control[] SCANIex, button_DoEx,lbl_DI_Txt,lbl_DO_Txt;


        ///////////////////////



        /// <summary>
        /// /
        /// </summary>
        public FormTest()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            SCANIex = new Control[32] {SCANIex0, SCANIex1, SCANIex2, SCANIex3, SCANIex4, SCANIex5, SCANIex6,
                SCANIex7, SCANIex8, SCANIex9, SCANIex10, SCANIex11, SCANIex12, SCANIex13, SCANIex14, SCANIex15,
            SCANIex16,SCANIex17,SCANIex18,SCANIex19,SCANIex20,SCANIex21,SCANIex22,SCANIex23,SCANIex24,
            SCANIex25,SCANIex26,SCANIex27,SCANIex28,SCANIex29,SCANIex30,SCANIex31};

            button_DoEx = new Control[16] { button_DoEx0, button_DoEx1, button_DoEx2, button_DoEx3, button_DoEx4, button_DoEx5, button_DoEx6,
                button_DoEx7, button_DoEx8, button_DoEx9, button_DoEx10, button_DoEx11, button_DoEx12, button_DoEx13, button_DoEx14, button_DoEx15 };       //扩展模块通用输出

            lbl_DI_Txt = new Control[32] {PG5_Lbl_DI0, PG5_Lbl_DI1, PG5_Lbl_DI2, PG5_Lbl_DI3, PG5_Lbl_DI4, PG5_Lbl_DI5, PG5_Lbl_DI6,
                PG5_Lbl_DI7, PG5_Lbl_DI8, PG5_Lbl_DI9, PG5_Lbl_DI10, PG5_Lbl_DI11, PG5_Lbl_DI12, PG5_Lbl_DI13, PG5_Lbl_DI14, PG5_Lbl_DI15,
            PG5_Lbl_DI16,PG5_Lbl_DI17,PG5_Lbl_DI18,PG5_Lbl_DI19,PG5_Lbl_DI20,PG5_Lbl_DI21,PG5_Lbl_DI22,PG5_Lbl_DI23,PG5_Lbl_DI24,
            PG5_Lbl_DI25,PG5_Lbl_DI26,PG5_Lbl_DI27,PG5_Lbl_DI28,PG5_Lbl_DI29,PG5_Lbl_DI30,PG5_Lbl_DI31};

            lbl_DO_Txt = new Control[16] { PG5_Lbl_Do0, PG5_Lbl_Do1, PG5_Lbl_Do2, PG5_Lbl_Do3, PG5_Lbl_Do4, PG5_Lbl_Do5, PG5_Lbl_Do6,
                PG5_Lbl_Do7, PG5_Lbl_Do8, PG5_Lbl_Do9, PG5_Lbl_Do10, PG5_Lbl_Do11, PG5_Lbl_Do12, PG5_Lbl_Do13, PG5_Lbl_Do14, PG5_Lbl_Do15 };       //扩展模块通用输出


            show_di_do_name(0);
        }
   

        private void FormIO_Load(object sender, EventArgs e)
        {
            TS8740 = hardware.m_8740;

            //  keyplC_Control11.init();
            if (SysStatus.CurUser.Name == "管理员")
            {

                PG4_btn_save.Visible = true;
              
            }else
            {
                PG4_btn_save.Visible = false;
            }
            Set_PG4_PM_TXT(1);
            if (SysStatus.Shield_NO1)
                PG1_CHK_1.Checked = true;
            if (SysStatus.Shield_NO2)
                PG1_CHK_2.Checked = true;
            //if (SysStatus.Shield_NO3)
            //    PG1_CHK_3.Checked = true;
            if (SysStatus.Shield_NO4)
                PG1_CHK_4.Checked = true;
            if (SysStatus.Shield_NO5)
                PG1_CHK_5.Checked = true;
            //if (SysStatus.Shield_NO6)
            //    PG1_CHK_6.Checked = true;
            if (SysStatus.Shield_NO7)
                PG1_CHK_7.Checked = true;
            if (SysStatus.Shield_NO4_H)
                PG1_CHK_4_H.Checked = true;

            if (SysStatus.Shield_NO4_P)
                PG1_CHK_4_P.Checked = true;

            if (SysStatus.Shield_NO5_H)
                PG1_CHK_5_H.Checked = true;

            if (SysStatus.Shield_NO5_P)
                PG1_CHK_5_P.Checked = true;
            if (SysStatus.Shield_NO9)
                PG1_CHK_9.Checked = true;


            if (SysStatus.Shield_NO6)
            {
                cb_SaoDaSao.Checked = true;
            }
            else
            {
                cb_SaoDaSao.Checked = false;
            }
        }

        #region 手动触发       
        private void button1_Click(object sender, EventArgs e)
        {
            SysStatus.NO1 = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SysStatus.NO2 = true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            SysStatus.NO3 = true;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            SysStatus.NO4 = true;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            SysStatus.NO5 = true;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            //SysStatus.NO6 = true;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            SysStatus.NO7 = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SysStatus.NO9 = true;
        }
        #endregion




        private void cmb_sl_md_index_SelectedIndexChanged(object sender, EventArgs e)
        {
            int x = 0;
            try
            {
                x = int.Parse(cmb_sl_md_index.SelectedItem.ToString());
            }catch
            {
                MessageBox.Show("模块号错误");
                return;
            }
            if(x>=0 && x<5)
            show_di_do_name(x);
        }
        public void show_di_do_name(int module_index)
        {
           hardware.my_io.Init();
            for (int i = 0; i < 32; i++)
                lbl_DI_Txt[i].Text = hardware.my_io.str_di[module_index, i];
            for (int i = 0; i < 16; i++)
                lbl_DO_Txt[i].Text = hardware.my_io.str_do[module_index, i];
        }

        private void PG2_Btn_test_plc_con_Click(object sender, EventArgs e)
        {
            var result = hardware.plc.ConnectServer();
            if (result.IsSuccess)
            {
                // 读取操作，这里的M100可以替换成I100,Q100,DB20.100效果时一样的
                MessageBox.Show("ok");
            }
            else
            {
                MessageBox.Show("fail");
            }
        }

        private void PG2_BTN_read_bool_Click(object sender, EventArgs e)
        {
            String str = PG2_TXT_adress.Text;
            bool M100_7 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
            MessageBox.Show(M100_7.ToString());
        }

        private void PG2_BTN_write_true_Click(object sender, EventArgs e)
        {
            String str = PG2_TXT_adress.Text;
            hardware.plc.Write(str, true);
        }

        private void PG2_BTN_write_false_Click(object sender, EventArgs e)
        {
            String str = PG2_TXT_adress.Text;
            hardware.plc.Write(str, false);
        }

        private void PG2_BTN_read_int_Click(object sender, EventArgs e)
        {
            String str = PG2_TXT_adress.Text;
            int M100_7 = hardware.plc.ReadInt16(str).Content;  // 读取M100.0是否通断
            MessageBox.Show(M100_7.ToString());
        }

        private void PG2_BTN_write_int_Click(object sender, EventArgs e)
        {
            short x = short.Parse(PG2_TXT_value.Text);
            String str = PG2_TXT_adress.Text;
            hardware.plc.Write(str, x);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                if(SysStatus.CurUser.Name != "管理员")
                {
                    MessageBox.Show("用户权限不够");
                    tabControl1.SelectedIndex = 0;
                    tabControl1_SelectedIndexChanged(sender, e);
                    return;
                }
            }
                if (tabControl1.SelectedIndex ==3)
                PG4_TIME.Enabled = true;
            else
                PG4_TIME.Enabled = false;
          /*  if (tabControl1.SelectedIndex == 4)
                PG5_TIME.Enabled = true;
            else
                PG5_TIME.Enabled = false;*/
        }

        private void PG4_TIME_Tick(object sender, EventArgs e)
        {
            label_MotionNum.Text = hardware.my_motion.sMotionNum.ToString();
         //   hardware.my_motion.Get_Pos();

            if (hardware.my_motion.m_serv_on[1])
                pg4_btn__m_en1.BackColor = Color.Red;
            else
                pg4_btn__m_en1.BackColor = Color.Silver;
          
            lbl_pg4_sts_1.Text = hardware.my_motion.m_sts[1].ToString();

            if (hardware.my_motion.m_serv_on[2])
                pg4_btn__m_en2.BackColor = Color.Red;
            else
                pg4_btn__m_en2.BackColor = Color.Silver;
            if (hardware.my_motion.m_serv_on[3])
                pg4_btn__m_en3.BackColor = Color.Red;
            else
                pg4_btn__m_en3.BackColor = Color.Silver;

            if (hardware.my_motion.m_serv_on[4])
                pg4_btn__m_en4.BackColor = Color.Red;
            else
                pg4_btn__m_en4.BackColor = Color.Silver;

            if (hardware.my_motion.m_serv_on[5])
                pg4_btn__m_en5.BackColor = Color.Red;
            else
                pg4_btn__m_en5.BackColor = Color.Silver;

            if (hardware.my_motion.m_serv_on[6])
                pg4_btn__m_en6.BackColor = Color.Red;
            else
                pg4_btn__m_en6.BackColor = Color.Silver;
            ///////1////////////


            lbl_pg4_abs_pos1.Text = hardware.my_motion.m_EcatEncPos[1].ToString();            
            lbl_pg4_enc_pos1.Text = hardware.my_motion.m_EncPos[1].ToString();           
            lbl_pg4_pro_pos1.Text = hardware.my_motion.m_PrfPos[1].ToString();
            ///////2////////////
            lbl_pg4_abs_pos2.Text = hardware.my_motion.m_EcatEncPos[2].ToString();
            lbl_pg4_enc_pos2.Text = hardware.my_motion.m_EncPos[2].ToString();
            lbl_pg4_pro_pos2.Text = hardware.my_motion.m_PrfPos[2].ToString();
            ////////////////////////
            lbl_pg4_abs_pos3.Text = hardware.my_motion.m_EcatEncPos[3].ToString();            
            lbl_pg4_enc_pos3.Text = hardware.my_motion.m_EncPos[3].ToString();           
            lbl_pg4_pro_pos3.Text = hardware.my_motion.m_PrfPos[3].ToString();

            lbl_pg4_abs_pos4.Text = hardware.my_motion.m_EcatEncPos[4].ToString();
            lbl_pg4_enc_pos4.Text = hardware.my_motion.m_EncPos[4].ToString();
            lbl_pg4_pro_pos4.Text = hardware.my_motion.m_PrfPos[4].ToString();

            lbl_pg4_abs_pos5.Text = hardware.my_motion.m_EcatEncPos[5].ToString();
            lbl_pg4_enc_pos5.Text = hardware.my_motion.m_EncPos[5].ToString();
            lbl_pg4_pro_pos5.Text = hardware.my_motion.m_PrfPos[5].ToString();

            lbl_pg4_abs_pos6.Text = hardware.my_motion.m_EcatEncPos[6].ToString();
            lbl_pg4_enc_pos6.Text = hardware.my_motion.m_EncPos[6].ToString();
            lbl_pg4_pro_pos6.Text = hardware.my_motion.m_PrfPos[6].ToString();
        }

        private void pg4_btn_m_start_Click(object sender, EventArgs e)
        {
            short axis =0 ;
            int pos =0 ;
            double sp =0  ;
            try
            {
                  axis = Convert.ToInt16(PG4_cmb_AxisNum.Text);
                  pos = int.Parse(pg4_txt_set_pos.Text);
                  sp = double.Parse(PG4_TXT_M_SP.Text);
            }
            catch
            {
                MessageBox.Show("数值错误");
                return;
            }

            if (sp > 50 || sp <=0)
                return;
            if (axis > 6 || axis <=0)
                return;

            hardware.my_motion.P2P(axis, pos,sp);
        }
  
  
 
   

        private void PG4_btn_save_Click(object sender, EventArgs e)
        {

            int x = int.Parse(PG4_cmb_AxisNum.SelectedItem.ToString()) ;
            if (x >= 1 && x <= 6)
            {

                int temp = int.Parse(PG4_M1.TxtText);
                SysStatus.Axis_Soft_Limt_F[x-1] = temp;//负限位
                temp = int.Parse(PG4_M2.TxtText);
                SysStatus.Axis_Soft_Limt_Z[x-1] = temp;//正限位

                temp = int.Parse(PG4_M9.TxtText); //速度
                SysStatus.Axis_SP[x-1] = temp;

                if (x == 1)//1轴
                {
                    temp = int.Parse(PG4_M3.TxtText);
                    SysStatus.Axis_1_Pos[0] = temp;
                    temp = int.Parse(PG4_M4.TxtText);
                    SysStatus.Axis_1_Pos[1] = temp;

                    temp = int.Parse(PG4_M5.TxtText);
                    SysStatus.Axis_Protect_F[0] = temp;
                    temp = int.Parse(PG4_M6.TxtText);
                    SysStatus.Axis_Protect_Z[0] = temp;

                   
                    Logger.Info($"电机1参数更新");//写入日志.
                }

                if (x == 2)
                {
                    temp = int.Parse(PG4_M3.TxtText);
                    SysStatus.Axis_2_Pos[0] = temp;
                    temp = int.Parse(PG4_M4.TxtText);
                    SysStatus.Axis_2_Pos[1] = temp;
                    temp = int.Parse(PG4_M5.TxtText);
                    SysStatus.Axis_2_Pos[2] = temp;
                    temp = int.Parse(PG4_M6.TxtText);
                    SysStatus.Axis_2_Pos[3] = temp;
                    temp = int.Parse(PG4_M7.TxtText);
                    SysStatus.Axis_2_Pos[4] = temp;
                    temp = int.Parse(PG4_M8.TxtText);
                    SysStatus.Axis_2_Pos[5] = temp;
                    Logger.Info($"电机2参数更新");//写入日志.
                }
                if (x == 3)
                {
                    temp = int.Parse(PG4_M3.TxtText);
                    SysStatus.Axis_3_Pos[0] = temp;
                    temp = int.Parse(PG4_M4.TxtText);
                    SysStatus.Axis_3_Pos[1] = temp;
                    temp = int.Parse(PG4_M5.TxtText);
                    SysStatus.Axis_3_Pos[2] = temp;
                    temp = int.Parse(PG4_M6.TxtText);
                    SysStatus.Axis_3_Pos[3] = temp;
                    temp = int.Parse(PG4_M7.TxtText);
                    SysStatus.Axis_3_Pos[4] = temp;
                    temp = int.Parse(PG4_M8.TxtText);
                    SysStatus.Axis_3_Pos[5] = temp;
                    Logger.Info($"电机3参数更新");//写入日志.
                }
                if (x == 4)
                {
                    temp = int.Parse(PG4_M3.TxtText);
                    SysStatus.Axis_4_Pos[0] = temp;
                    temp = int.Parse(PG4_M4.TxtText);
                    SysStatus.Axis_4_Pos[1] = temp;
                    temp = int.Parse(PG4_M5.TxtText);
                    SysStatus.Axis_4_Pos[2] = temp;
                    temp = int.Parse(PG4_M6.TxtText);
                    SysStatus.Axis_4_Pos[3] = temp;
                    temp = int.Parse(PG4_M7.TxtText);
                    SysStatus.Axis_4_Pos[4] = temp;
                    temp = int.Parse(PG4_M8.TxtText);
                    SysStatus.Axis_4_Pos[5] = temp;
                    Logger.Info($"电机4参数更新");//写入日志.
                }
                if (x ==5)
                {
                    temp = int.Parse(PG4_M3.TxtText);
                    SysStatus.Axis_5_Pos[0] = temp;
                    temp = int.Parse(PG4_M4.TxtText);
                    SysStatus.Axis_5_Pos[1] = temp;
                    temp = int.Parse(PG4_M5.TxtText);
                    SysStatus.Axis_5_Pos[2] = temp;
                    temp = int.Parse(PG4_M6.TxtText);
                    SysStatus.Axis_5_Pos[3] = temp;

                    temp = int.Parse(PG4_M7.TxtText);
                    SysStatus.Axis_5_Pos[4] = temp;
                    temp = int.Parse(PG4_M8.TxtText);
                    SysStatus.Axis_5_Pos[5] = temp;
                    Logger.Info($"电机5参数更新");//写入日志.
                }
                if (x == 6)
                {
                    temp = int.Parse(PG4_M3.TxtText);
                    SysStatus.Axis_6_Pos[0] = temp;
                    temp = int.Parse(PG4_M4.TxtText);
                    SysStatus.Axis_6_Pos[1] = temp;
                    temp = int.Parse(PG4_M5.TxtText);
                    SysStatus.Axis_6_Pos[2] = temp;
                    temp = int.Parse(PG4_M6.TxtText);
                    SysStatus.Axis_6_Pos[3] = temp;
                    temp = int.Parse(PG4_M7.TxtText);
                    SysStatus.Axis_6_Pos[4] = temp;
                    temp = int.Parse(PG4_M8.TxtText);
                    SysStatus.Axis_6_Pos[5] = temp;
                    Logger.Info($"电机6参数更新");//写入日志.
                }

            }
            SysStatus.SaveConfig(SysStatus.sys_dir_path);
            MessageBox.Show("ok");
            
        }
        /// <summary>
        /// 不同电机不同参数
        /// </summary>
        /// <param name="index"></param>
        private void Set_PG4_PM_TXT(int index)
        {

            PG4_M1.TxtText = SysStatus.Axis_Soft_Limt_F[index-1].ToString();//负限位
            PG4_M2.TxtText = SysStatus.Axis_Soft_Limt_Z[index-1].ToString();//正限位
            PG4_M9.TxtText = SysStatus.Axis_SP[index-1].ToString();//速度

            if (index == 1)//横移
            {
                PG4_M3.TxtText = SysStatus.Axis_1_Pos[0].ToString();
                PG4_M3.NameText = "放料位";
                PG4_M4.TxtText = SysStatus.Axis_1_Pos[1].ToString();
                PG4_M4.NameText = "抓料位";

                PG4_M5.TxtText = SysStatus.Axis_Protect_F[0].ToString();
                PG4_M5.NameText = "防护区负";
                PG4_M6.TxtText = SysStatus.Axis_Protect_Z[0].ToString();
                PG4_M6.NameText = "防护区正";

                PG4_M3.Visible = true;
                PG4_M4.Visible = true;
                PG4_M5.Visible = true;
                PG4_M6.Visible = true;
                PG4_M7.Visible = false;
                PG4_M8.Visible = false;

            }
            if (index == 2)//下料X
            {

                PG4_M3.TxtText = SysStatus.Axis_2_Pos[0].ToString();
                PG4_M4.TxtText = SysStatus.Axis_2_Pos[1].ToString();
                PG4_M5.TxtText = SysStatus.Axis_2_Pos[2].ToString();
                PG4_M6.TxtText = SysStatus.Axis_2_Pos[3].ToString();
                PG4_M7.TxtText = SysStatus.Axis_2_Pos[4].ToString();
                PG4_M8.TxtText = SysStatus.Axis_2_Pos[5].ToString();
                PG4_M3.NameText = "取料位";
                PG4_M4.NameText = "异常1";
                PG4_M5.NameText = "异常2";
                PG4_M6.NameText = "异常3";
                PG4_M7.NameText = "异常4";
                PG4_M8.NameText = "放料位";

                PG4_M3.Visible = true;
                PG4_M4.Visible = true;
                PG4_M5.Visible = true;
                PG4_M6.Visible = true;
                PG4_M7.Visible = true;
                PG4_M8.Visible = true;



            }
            if (index == 3)//CCD2
            {
                PG4_M3.TxtText = SysStatus.Axis_3_Pos[0].ToString();
                PG4_M4.TxtText = SysStatus.Axis_3_Pos[1].ToString();
                PG4_M3.NameText = "位置1";
                PG4_M4.NameText = "位置2";
            
                PG4_M3.Visible = true;
                PG4_M4.Visible = true;
                PG4_M5.Visible = false;
                PG4_M6.Visible = false;
                PG4_M7.Visible = false;
                PG4_M8.Visible = false;

            }
            if (index == 4)//CCD1
            {
                PG4_M3.TxtText = SysStatus.Axis_4_Pos[0].ToString();
                PG4_M4.TxtText = SysStatus.Axis_4_Pos[1].ToString();
                PG4_M3.NameText = "位置1";
                PG4_M4.NameText = "位置2";

                PG4_M3.Visible = true;
                PG4_M4.Visible = true;
                PG4_M5.Visible = false;
                PG4_M6.Visible = false;
                PG4_M7.Visible = false;
                PG4_M8.Visible = false;

            }
            if (index == 5)//取料
            {
                PG4_M3.TxtText = SysStatus.Axis_5_Pos[0].ToString();
                PG4_M4.TxtText = SysStatus.Axis_5_Pos[1].ToString();
                PG4_M5.TxtText = SysStatus.Axis_5_Pos[2].ToString();
                PG4_M6.TxtText = SysStatus.Axis_5_Pos[3].ToString();
                PG4_M7.TxtText = SysStatus.Axis_5_Pos[4].ToString();
                PG4_M8.TxtText = SysStatus.Axis_5_Pos[5].ToString();

                PG4_M3.NameText = "等待位";
                PG4_M4.NameText = "上-取料位1";
                PG4_M5.NameText = "上-取料位2";
                PG4_M6.NameText = "放料位";
                PG4_M7.NameText = "下-取料位1";
                PG4_M8.NameText = "下_取料位2";
                PG4_M3.Visible = true;
                PG4_M4.Visible = true;
                PG4_M5.Visible = true;
                PG4_M6.Visible = true;
                PG4_M7.Visible = true;
                PG4_M8.Visible = true;

            }
            if (index == 6)//下料Y
            {
                PG4_M3.TxtText = SysStatus.Axis_6_Pos[0].ToString();
                PG4_M4.TxtText = SysStatus.Axis_6_Pos[1].ToString();
                PG4_M5.TxtText = SysStatus.Axis_6_Pos[2].ToString();
                PG4_M3.NameText = "取料位";
                PG4_M4.NameText = "异常位";
                PG4_M5.NameText = "放料位";

                PG4_M3.Visible = true;
                PG4_M4.Visible = true;
                PG4_M5.Visible = true;
                PG4_M6.Visible = false;
                PG4_M7.Visible = false;
                PG4_M8.Visible = false;

            }
        }

      

        private void pg4_btn__m_en1_Click(object sender, EventArgs e)
        {
            if(pg4_btn__m_en1.BackColor==Color.Red)
                hardware.my_motion.NotEN(1);
            else
                hardware.my_motion.En(1);
        }
     
        private void pg4_btn_JOG_F_MouseDown(object sender, MouseEventArgs e)
        {
            short axis = Convert.ToInt16(PG4_cmb_AxisNum.Text);
            int pos = int.Parse(pg4_txt_set_pos.Text);
            double sp = double.Parse(PG4_TXT_M_SP.Text);
            hardware.my_motion.jog(axis,-1*sp);
        }

        private void pg4_btn_JOG_F_MouseUp(object sender, MouseEventArgs e)
        {
            hardware.my_motion.Stop();
        }

        private void pg4_btn_JOG_Z_MouseDown(object sender, MouseEventArgs e)
        {
            short axis = Convert.ToInt16(PG4_cmb_AxisNum.Text);
            int pos = int.Parse(pg4_txt_set_pos.Text);
            double sp = double.Parse(PG4_TXT_M_SP.Text);
            hardware.my_motion.jog(axis, sp);
        }

        private void pg4_btn_JOG_Z_MouseUp(object sender, MouseEventArgs e)
        {
            hardware.my_motion.Stop();
        }

        private void pg4_btn__m_en2_Click(object sender, EventArgs e)
        {
            if (pg4_btn__m_en2.BackColor == Color.Red)
                hardware.my_motion.NotEN(2);
            else
                hardware.my_motion.En(2);
        
    }

        private void pg4_btn__m_en3_Click(object sender, EventArgs e)
        {
            if (pg4_btn__m_en3.BackColor == Color.Red)
                hardware.my_motion.NotEN(3);
            else
                hardware.my_motion.En(3);
        }

        private void pg4_btn__m_en4_Click(object sender, EventArgs e)
        {
            if (pg4_btn__m_en4.BackColor == Color.Red)
                hardware.my_motion.NotEN(4);
            else
                hardware.my_motion.En(4);
        }

        private void pg4_btn__m_en5_Click(object sender, EventArgs e)
        {
            if (pg4_btn__m_en5.BackColor == Color.Red)
                hardware.my_motion.NotEN(5);
            else
                hardware.my_motion.En(5);
        }

        private void pg4_btn__m_en6_Click(object sender, EventArgs e)
        {
            if (pg4_btn__m_en6.BackColor == Color.Red)
                hardware.my_motion.NotEN(6);
            else
                hardware.my_motion.En(6);
        }

        private void pg4_btn_m_stop_Click(object sender, EventArgs e)
        {
            hardware.my_motion.Stop();
        }

        private void PG6_BTN_READ1_Click(object sender, EventArgs e)
        {
            hardware.my_cxr1.Send_On();
        }

        private void PG6_BTN_END1_Click(object sender, EventArgs e)
        {
            hardware.my_cxr1.Send_Off();
        }

        private void PG6_BTN_READ2_Click(object sender, EventArgs e)
        {
            hardware.my_cxr2.Send_On();
        }

        private void PG6_BTN_END2_Click(object sender, EventArgs e)
        {
            hardware.my_cxr2.Send_Off();
        }

        private void PG6_BTN_LOAD_Click(object sender, EventArgs e)
        {
            String str=PG6_TXT_1.Text.Trim();
            hardware.my_laser.LoadFile(str);
        }

        private void PG6_BTN_EDIT_Click(object sender, EventArgs e)
        {
            string str1 = PG6_TXT_2.Text;
            string str2 = PG6_TXT_3.Text;
            hardware.my_laser.SetValue(str1, str2);

        }

        private void PG6_BTN_START_Click(object sender, EventArgs e)
        {
            hardware.my_laser.Start();
        }

        private void PG7_BTN_SEND_Click(object sender, EventArgs e)
        {
            int index = int.Parse(PG7_CMB_CAM.Text);
            int model_num = int.Parse(PG8_txt_Model_num.Text);
            string lr = PG8_txt_pos.Text;
            int pro=1;
            if(index==1)
            {
                if (PG8_Rbtn_1.Checked)
                    hardware.my_ccd1.CMD_CAM1(pro,model_num, lr);
                if (PG8_Rbtn_2.Checked)
                    hardware.my_ccd1.CMD_CAM2(pro,model_num, lr);

            }

            if (index == 2)
            {
                if (PG8_Rbtn_1.Checked)
                    hardware.my_ccd2.CMD_CAM1(pro, model_num, lr);
                if (PG8_Rbtn_2.Checked)
                    hardware.my_ccd2.CMD_CAM2(pro, model_num, lr);

            }
            if (index == 3)
            {
                if (PG8_Rbtn_1.Checked)
                    hardware.my_ccd3.CMD_CAM1(pro, model_num, lr);
                if (PG8_Rbtn_2.Checked)
                    hardware.my_ccd3.CMD_CAM2(pro, model_num, lr);

            }

            if (index == 4)
            {
                if (PG8_Rbtn_1.Checked)
                    hardware.my_ccd4.CMD_CAM1(pro, model_num, lr);
                if (PG8_Rbtn_2.Checked)
                    hardware.my_ccd4.CMD_CAM2(pro, model_num, lr);

            }
        }

        private void PG10_BTN_SEND_Click(object sender, EventArgs e)
        {
            String str;
            hardware.m_8740.SendStart(out str);
        }

        private void PG4_M1_ButtonClicked(object sender, EventArgs e)
        {
            int x = int.Parse(PG4_cmb_AxisNum.SelectedItem.ToString());
            PG4_M1.TxtText = hardware.my_motion.m_EcatEncPos[x].ToString();
        }

        private void PG4_M1_ButtonClicked2(object sender, EventArgs e)
        {
            pg4_txt_set_pos.Text = PG4_M1.TxtText;
        }

        private void PG4_M2_ButtonClicked(object sender, EventArgs e)
        {
            int x = int.Parse(PG4_cmb_AxisNum.SelectedItem.ToString());
            PG4_M2.TxtText = hardware.my_motion.m_EcatEncPos[x].ToString();
        }

        private void PG4_M2_ButtonClicked2(object sender, EventArgs e)
        {
            pg4_txt_set_pos.Text = PG4_M2.TxtText;
        }

        private void PG4_M3_ButtonClicked(object sender, EventArgs e)
        {
            int x = int.Parse(PG4_cmb_AxisNum.SelectedItem.ToString());
            PG4_M3.TxtText = hardware.my_motion.m_EcatEncPos[x].ToString();
        }

        private void PG4_M3_ButtonClicked2(object sender, EventArgs e)
        {
            pg4_txt_set_pos.Text = PG4_M3.TxtText;
        }

        private void PG4_M4_ButtonClicked(object sender, EventArgs e)
        {
            int x = int.Parse(PG4_cmb_AxisNum.SelectedItem.ToString());
            PG4_M4.TxtText = hardware.my_motion.m_EcatEncPos[x].ToString();
        }

        private void PG4_M4_ButtonClicked2(object sender, EventArgs e)
        {
            pg4_txt_set_pos.Text = PG4_M4.TxtText;
        }

        private void PG4_M5_ButtonClicked(object sender, EventArgs e)
        {
            int x = int.Parse(PG4_cmb_AxisNum.SelectedItem.ToString());
            PG4_M5.TxtText = hardware.my_motion.m_EcatEncPos[x].ToString();
        }

        private void PG4_M5_ButtonClicked2(object sender, EventArgs e)
        {
            pg4_txt_set_pos.Text = PG4_M5.TxtText;
        }

        private void PG4_M6_ButtonClicked(object sender, EventArgs e)
        {
            int x = int.Parse(PG4_cmb_AxisNum.SelectedItem.ToString());
            PG4_M6.TxtText = hardware.my_motion.m_EcatEncPos[x].ToString();
        }

        private void PG4_M6_ButtonClicked2(object sender, EventArgs e)
        {
            pg4_txt_set_pos.Text = PG4_M6.TxtText;
        }

        private void PG4_M7_ButtonClicked(object sender, EventArgs e)
        {
            int x = int.Parse(PG4_cmb_AxisNum.SelectedItem.ToString());
            PG4_M7.TxtText = hardware.my_motion.m_EcatEncPos[x].ToString();
        }

        private void PG4_M7_ButtonClicked2(object sender, EventArgs e)
        {
            pg4_txt_set_pos.Text = PG4_M7.TxtText;
        }

        private void PG4_M8_ButtonClicked(object sender, EventArgs e)
        {
            int x = int.Parse(PG4_cmb_AxisNum.SelectedItem.ToString());
            PG4_M8.TxtText = hardware.my_motion.m_EcatEncPos[x].ToString();
        }

        private void PG4_M8_ButtonClicked2(object sender, EventArgs e)
        {
            pg4_txt_set_pos.Text = PG4_M8.TxtText;
        }
  

        private void PG1_TIME_Tick(object sender, EventArgs e)
        {

            if (PG1_CHK_1.Checked)
                SysStatus.Shield_NO1 = true;
            else
                SysStatus.Shield_NO1 = false;
            if (PG1_CHK_2.Checked)
                SysStatus.Shield_NO2 = true;
            else
                SysStatus.Shield_NO2 = false;
            //if (PG1_CHK_3.Checked)
            //    SysStatus.Shield_NO3 = true;
            //else
            //    SysStatus.Shield_NO3 = false;
            if (PG1_CHK_4.Checked)
                SysStatus.Shield_NO4 = true;
            else
                SysStatus.Shield_NO4 = false;
            if (PG1_CHK_5.Checked)
                SysStatus.Shield_NO5 = true;
            else
                SysStatus.Shield_NO5 = false;
            //if (PG1_CHK_6.Checked)
            //    SysStatus.Shield_NO6 = true;
            //else
            //    SysStatus.Shield_NO6 = false;

            if (PG1_CHK_7.Checked)
                SysStatus.Shield_NO7 = true;
            else
                SysStatus.Shield_NO7 = false;


            if (PG1_CHK_9.Checked)
                SysStatus.Shield_NO9 = true;
            else
                SysStatus.Shield_NO9 = false;


            if (PG1_CHK_4_P.Checked)
                SysStatus.Shield_NO4_P = true;
            else
                SysStatus.Shield_NO4_P = false;

            if (PG1_CHK_4_H.Checked)
                SysStatus.Shield_NO4_H = true;
            else
                SysStatus.Shield_NO4_H = false;

            if (PG1_CHK_5_P.Checked)
                SysStatus.Shield_NO5_P = true;
            else
                SysStatus.Shield_NO5_P = false;

            if (PG1_CHK_5_H.Checked)
                SysStatus.Shield_NO5_H = true;
            else
                SysStatus.Shield_NO5_H = false;


            if (cb_SaoDaSao.Checked)
            {
                SysStatus.Shield_NO6 = true;
            }
            else
            {
                SysStatus.Shield_NO6 = false;
            }

        }

        private void PG6_BTN_CLEAR1_Click(object sender, EventArgs e)
        {
            hardware.my_cxr1.Clr_str();
        }

        private void PG6_BTN_CLEAR2_Click(object sender, EventArgs e)
        {
            hardware.my_cxr2.Clr_str();
        }

        private void PG1_BTN_close_all_Click(object sender, EventArgs e)
        {
            PG1_CHK_1.Checked = true;
            PG1_CHK_2.Checked = true;
            //PG1_CHK_3.Checked = true;
            PG1_CHK_4.Checked = true;
            PG1_CHK_5.Checked = true;
            //PG1_CHK_6.Checked = true;
            PG1_CHK_7.Checked = true;
            PG1_CHK_9.Checked = true;

        }
        /// <summary>
        /// 显示工位的二维码 磨具号 状态
        /// </summary>
        /// <param name="index"></param>
        public void Show_Station_cord(int index)
        {
            PG1_LBL_STATION_NUM.Text = index.ToString();

            PG1_TXT_CODE_L.Text = hardware.my_cord.my_Pro[index].Cord_ST_L;
            PG1_TXT_MOJU_L.Text = hardware.my_cord.my_Pro[index].Model_Num_L.ToString();
            if (hardware.my_cord.my_Pro[index].STS_L_voltage)
                PG1_CHK_L1.Checked = true;
            else
                PG1_CHK_L1.Checked = false;

            if (hardware.my_cord.my_Pro[index].STS_L_CCD1_H)
                PG1_CHK_L2.Checked = true;
            else
                PG1_CHK_L2.Checked = false;

            if (hardware.my_cord.my_Pro[index].STS_L_CCD1_P)
                PG1_CHK_L22.Checked = true;
            else
                PG1_CHK_L22.Checked = false;


            if (hardware.my_cord.my_Pro[index].STS_L_CCD2_H)
                PG1_CHK_L3.Checked = true;
            else
                PG1_CHK_L3.Checked = false;

            if (hardware.my_cord.my_Pro[index].STS_L_CCD2_P)
                PG1_CHK_L33.Checked = true;
            else
                PG1_CHK_L33.Checked = false;

            if (hardware.my_cord.my_Pro[index].STS_L_LASER)
                PG1_CHK_L4.Checked = true;
            else
                PG1_CHK_L4.Checked = false;


            if (hardware.my_cord.my_Pro[index].STS_L_AIR)
                PG1_CHK_L5.Checked = true;
            else
                PG1_CHK_L5.Checked = false;


            PG1_TXT_CODE_R.Text = hardware.my_cord.my_Pro[index].Cord_ST_R;
            PG1_TXT_MOJU_R.Text = hardware.my_cord.my_Pro[index].Model_Num_R.ToString();


            if (hardware.my_cord.my_Pro[index].STS_R_voltage)
                PG1_CHK_R1.Checked = true;
            else
                PG1_CHK_R1.Checked = false;

            if (hardware.my_cord.my_Pro[index].STS_R_CCD1_H)
                PG1_CHK_R2.Checked = true;
            else
                PG1_CHK_R2.Checked = false;

            if (hardware.my_cord.my_Pro[index].STS_R_CCD1_P)
                PG1_CHK_R22.Checked = true;
            else
                PG1_CHK_R22.Checked = false;

            if (hardware.my_cord.my_Pro[index].STS_R_CCD2_H)
                PG1_CHK_R3.Checked = true;
            else
                PG1_CHK_R3.Checked = false;

            if (hardware.my_cord.my_Pro[index].STS_R_CCD2_P)
                PG1_CHK_R33.Checked = true;
            else
                PG1_CHK_R33.Checked = false;

            if (hardware.my_cord.my_Pro[index].STS_R_LASER)
                PG1_CHK_R4.Checked = true;
            else
                PG1_CHK_R4.Checked = false;



            if (hardware.my_cord.my_Pro[index].STS_R_AIR)
                PG1_CHK_R5.Checked = true;
            else
                PG1_CHK_R5.Checked = false;

            Set_PG1_BTN_COLOR(index);
        }
        private void PG1_BTN_STATION1_Click(object sender, EventArgs e)
        {
            Show_Station_cord(1);

        }

        private void PG1_BTN_STATION2_Click(object sender, EventArgs e)
        {
            Show_Station_cord(2);
        }

        private void PG1_BTN_STATION3_Click(object sender, EventArgs e)
        {
            Show_Station_cord(3);
        }

        private void PG1_BTN_STATION4_Click(object sender, EventArgs e)
        {
            Show_Station_cord(4);
        }

        private void PG1_BTN_STATION5_Click(object sender, EventArgs e)
        {
            Show_Station_cord(5);
        }

        private void PG1_BTN_STATION6_Click(object sender, EventArgs e)
        {
            Show_Station_cord(6);
        }
        /// <summary>
        /// 设置按钮颜色
        /// </summary>
        /// <param name="flg"></param>
        public void Set_PG1_BTN_COLOR(int flg)
        {
       
                PG1_BTN_STATION1.BackColor = Color.Transparent;
     
                PG1_BTN_STATION2.BackColor = Color.Transparent;
    
                PG1_BTN_STATION3.BackColor = Color.Transparent;
 
                PG1_BTN_STATION4.BackColor = Color.Transparent;
  
                PG1_BTN_STATION5.BackColor = Color.Transparent;
 
                PG1_BTN_STATION6.BackColor = Color.Transparent;

            if (flg==1)
                PG1_BTN_STATION1.BackColor = Color.Red;
            if (flg == 2)
                PG1_BTN_STATION2.BackColor = Color.Red;
            if (flg == 3)
                PG1_BTN_STATION3.BackColor = Color.Red;
            if (flg == 4)
                PG1_BTN_STATION4.BackColor = Color.Red;
            if (flg == 5)
                PG1_BTN_STATION5.BackColor = Color.Red;
            if (flg == 6)
                PG1_BTN_STATION6.BackColor = Color.Red;
        }
        private void PG1_BTN_WRITE_Click(object sender, EventArgs e)
        {
            int x = int.Parse(PG1_LBL_STATION_NUM.Text);

            if (x == 0)
                return;

            hardware.my_cord.my_Pro[x].Cord_ST_L = PG1_TXT_CODE_L.Text;
            hardware.my_cord.my_Pro[x].Model_Num_L = int.Parse(PG1_TXT_MOJU_L.Text);

            hardware.my_cord.my_Pro[x].STS_L_voltage = false;
            hardware.my_cord.my_Pro[x].STS_L_CCD1_H = false;
            hardware.my_cord.my_Pro[x].STS_L_CCD1_P = false;
            hardware.my_cord.my_Pro[x].STS_L_CCD2_H = false;
            hardware.my_cord.my_Pro[x].STS_L_CCD2_P = false;
            hardware.my_cord.my_Pro[x].STS_L_LASER = false;
            if (PG1_CHK_L1.Checked)
                hardware.my_cord.my_Pro[x].STS_L_voltage = true;

            if (PG1_CHK_L2.Checked)
                hardware.my_cord.my_Pro[x].STS_L_CCD1_H = true;
            if (PG1_CHK_L22.Checked)
                hardware.my_cord.my_Pro[x].STS_L_CCD1_P = true;

            if (PG1_CHK_L3.Checked)
                hardware.my_cord.my_Pro[x].STS_L_CCD2_H = true;
            if (PG1_CHK_L33.Checked)
                hardware.my_cord.my_Pro[x].STS_L_CCD2_P = true;

            if (PG1_CHK_L4.Checked)
                hardware.my_cord.my_Pro[x].STS_L_LASER = true;
            if (PG1_CHK_L5.Checked)
                hardware.my_cord.my_Pro[x].STS_L_AIR = true;
           
            /////////////////////////////////////////////////////////////////////
            hardware.my_cord.my_Pro[x].Cord_ST_R = PG1_TXT_CODE_R.Text;
            hardware.my_cord.my_Pro[x].Model_Num_R = int.Parse(PG1_TXT_MOJU_R.Text);

            hardware.my_cord.my_Pro[x].STS_R_voltage = false;
            hardware.my_cord.my_Pro[x].STS_R_CCD1_H = false;
            hardware.my_cord.my_Pro[x].STS_R_CCD1_P = false;
            hardware.my_cord.my_Pro[x].STS_R_CCD2_H = false;
            hardware.my_cord.my_Pro[x].STS_R_CCD2_P = false;
            hardware.my_cord.my_Pro[x].STS_R_LASER = false;
            if (PG1_CHK_R1.Checked)
                hardware.my_cord.my_Pro[x].STS_R_voltage = true;
            if (PG1_CHK_R2.Checked)
                hardware.my_cord.my_Pro[x].STS_R_CCD1_H = true;

            if (PG1_CHK_R22.Checked)
                hardware.my_cord.my_Pro[x].STS_R_CCD1_P = true;

            if (PG1_CHK_R3.Checked)
                hardware.my_cord.my_Pro[x].STS_R_CCD2_H = true;
            if (PG1_CHK_R33.Checked)
                hardware.my_cord.my_Pro[x].STS_R_CCD2_P = true;

            if (PG1_CHK_R4.Checked)
                hardware.my_cord.my_Pro[x].STS_R_LASER = true;
            if (PG1_CHK_R5.Checked)
                hardware.my_cord.my_Pro[x].STS_R_AIR = true;
            hardware.my_cord.SaveSTcord();
        }

        private void PG1_BTN_STOP_Click(object sender, EventArgs e)
        {
            hardware.my_motion.Stop();
        }

        private void PG2_TIME_Tick(object sender, EventArgs e)
        {
            if (hardware.plc_isCon == false)
                return;

            if (PG2_CHK_CHECK.Checked == false)
                return;
            string str;
            bool UP, DOWN, p1_1, p1_2, p1_3, p1_4;
            bool p2_1, p2_2, p2_3, p2_4;

            int mode1_1, mode1_2, mode2_1, mode2_2;
            str = "DB100.3.0";//判断1-1
            UP = hardware.plc.ReadBool(str).Content;  // 上板
            str = "DB100.3.5";//判断1-1
            DOWN = hardware.plc.ReadBool(str).Content;  // 下板

            PG2_lbl_plc_up.Text = UP.ToString();
            PG2_lbl_plc_down.Text = DOWN.ToString();

            str = "DB100.2.4";//判断1-1
                p1_1 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
                str = "DB100.2.5";//判断1-1
                p1_2 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断

                str = "DB100.2.6";//判断1-1
            p1_3 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
                str = "DB100.2.7";//判断1-1
            p1_4 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
             
                str = "DB100.3.1";//判断1-1
            p2_1 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
                str = "DB100.3.2";//判断1-1
            p2_2 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断

                str = "DB100.3.3";//判断1-1
            p2_3 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
                str = "DB100.3.4";//判断1-1
            p2_4 = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断


            PG2_lbl_plc_1_1.Text = p1_1.ToString();
            PG2_lbl_plc_1_2.Text = p1_2.ToString();
            PG2_lbl_plc_1_3.Text = p1_3.ToString();
            PG2_lbl_plc_1_4.Text = p1_4.ToString();
            PG2_lbl_plc_2_1.Text = p2_1.ToString();
            PG2_lbl_plc_2_2.Text = p2_2.ToString();
            PG2_lbl_plc_2_3.Text = p2_3.ToString();
            PG2_lbl_plc_2_4.Text = p2_4.ToString();
          
            //////////////////////磨具号/////////////////////////////////////
            str = "DB100.4.0";//判断1-1
            mode2_1 = hardware.plc.ReadInt16(str).Content;  // 读取M100.0是否通断
            str = "DB100.6.0";//判断1-1
            mode2_2 = hardware.plc.ReadInt16(str).Content;  // 读取M100.0是否通断

            str = "DB100.8.0";//判断1-1
            mode1_1 = hardware.plc.ReadInt16(str).Content;  // 读取M100.0是否通断
            str = "DB100.10.0";//判断1-1
            mode1_2 = hardware.plc.ReadInt16(str).Content;  // 读取M100.0是否通断

            PG2_lbl_plc_m_1.Text = mode1_1.ToString();
            PG2_lbl_plc_m_2.Text = mode1_2.ToString();
            PG2_lbl_plc_m_3.Text = mode2_1.ToString();
            PG2_lbl_plc_m_4.Text = mode2_2.ToString();

            str = "DB100.2.1";//判断1-1
            bool X = hardware.plc.ReadBool(str).Content;  // 读取M100.0是否通断
            PG2_lbl_plc_GRAP.Text = X.ToString();
        }

        private void PG2_BTN_PLC_PUT_Click(object sender, EventArgs e)
        {
         string   str = "DB100.2.2";//允许上料
            hardware.plc.Write(str, true);
        }
        private void PG2_BTN_PLC_PUT_f_Click(object sender, EventArgs e)
        {
            string str = "DB100.2.2";//允许上料
            hardware.plc.Write(str, false);
        }
        private void PG2_BTN_PLC_GRAP_Click(object sender, EventArgs e)
        {
            string str = "DB100.2.3";//允许上料
            hardware.plc.Write(str, true);
        }

        private void PG2_BTN_PLC_GRAP_F_Click(object sender, EventArgs e)
        {
            string str = "DB100.2.3";//允许上料
            hardware.plc.Write(str, false);
        }

        private void PG1_CHK_3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void PG4_cmb_AxisNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PG4_cmb_AxisNum.SelectedItem.ToString() == "1")
            {
                groupBox3.BackColor = Color.Silver;
                Set_PG4_PM_TXT(1);
            }
            else
                groupBox3.BackColor = Color.Transparent;

            if (PG4_cmb_AxisNum.SelectedItem.ToString() == "2")
            {
                groupBox4.BackColor = Color.Silver;
                Set_PG4_PM_TXT(2);
            }
            else
                groupBox4.BackColor = Color.Transparent;

            if (PG4_cmb_AxisNum.SelectedItem.ToString() == "3")
            {
                groupBox5.BackColor = Color.Silver;
                Set_PG4_PM_TXT(3);
            }
            else
                groupBox5.BackColor = Color.Transparent;

            if (PG4_cmb_AxisNum.SelectedItem.ToString() == "4")
            {
                groupBox6.BackColor = Color.Silver;
                Set_PG4_PM_TXT(4);
            }
            else
                groupBox6.BackColor = Color.Transparent;

            if (PG4_cmb_AxisNum.SelectedItem.ToString() == "5")
            {
                groupBox7.BackColor = Color.Silver;
                Set_PG4_PM_TXT(5);
            }
            else
                groupBox7.BackColor = Color.Transparent;

            if (PG4_cmb_AxisNum.SelectedItem.ToString() == "6")
            {
                groupBox8.BackColor = Color.Silver;
                Set_PG4_PM_TXT(6);
            }
            else
                groupBox8.BackColor = Color.Transparent;
        }


        private void button_DoEx0_Click(object sender, EventArgs e)
        {

            short station = 0;
            try
            {
                station = short.Parse(cmb_sl_md_index.Text);
            }
            catch
            {
                MessageBox.Show("模块站号错误");
            }
            if (station < 0 || station > 3)
                return;

            System.Windows.Forms.Button tempbutton = sender as System.Windows.Forms.Button;
            string str = tempbutton.Text;
            char x = ' ';
            short index = short.Parse(str.Substring(str.IndexOf(x)));
            index =(short) (index - station * 16);
            if (tempbutton.BackColor == Color.Red)
               hardware.my_io.Set_Do(station, index, 0);
            else
                hardware.my_io.Set_Do(station, index, 1);
        }

        private void PG5_TIME_Tick(object sender, EventArgs e)
        {
            
            short station=0;  //站号
            try
            {
                station = short.Parse(cmb_sl_md_index.Text);
            }catch
            {
                return;
            }
            if (station < 0 || station > 4)
                return;

            int sum = 32;
            if (station < 4)
                sum = 16;

            int input_txt = 0;
           
            if (station == 1)
                input_txt = 16;                
            if (station == 2)
                input_txt = 32;
            if (station == 3)
                input_txt = 48;
            if (station == 4)
                input_txt = 64;

            ///输入状态
            for (int i = 0; i < 32; i++)
            {
                if (i < sum)
                {
                    if (hardware.my_io.m_input[station, i])
                        SCANIex[i].BackColor = Color.Red;
                    else
                        SCANIex[i].BackColor = Color.LightGray;
                    SCANIex[i].Text = "X" + (input_txt + i).ToString();

                    SCANIex[i].Visible = true;
                }
                else
                    SCANIex[i].Visible = false;
            }

            //输出状态
            for (int i = 0; i < 16; i++)
            {
                if (hardware.my_io.m_output[station, i])
                    button_DoEx[i].BackColor = Color.Red;
                else
                    button_DoEx[i].BackColor = Color.LightGray;

                button_DoEx[i].Text = "Q " + (input_txt + i).ToString();
            }
            ///////读码//////////////
            PG6_TXT_STS1.Text = hardware.my_cxr1.str;
            PG6_TXT_STS2.Text = hardware.my_cxr2.str;
            ///////////CCD/////////////
            PG7_TXT_CCD1.Text = hardware.my_ccd1.data1;
            PG7_TXT_CCD2.Text = hardware.my_ccd2.data1;
            PG7_TXT_CCD3.Text = hardware.my_ccd3.data1;
            PG7_TXT_CCD4.Text = hardware.my_ccd4.data1;

            PG6_TXT_la.Text = hardware.my_laser.data1;
            /////////
            PG10_TXT_1.Text = hardware.m_8740.ReceiveData8740;

        }
    }
}
