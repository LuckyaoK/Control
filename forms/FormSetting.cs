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
using CXPro001.setups;
using CXPro001.classes;
using CXPro001.myclass;
namespace CXPro001.forms
{
    public partial class FormSetting : Form
    {
 
 
 
         private CordSet CordSet1 = new CordSet();
        private FlowMeter flowMeter = new FlowMeter();
        public FormSetting()
        {
            InitializeComponent();

            this.Load += FormSetting_load;
        }
        private void FormSetting_load(object sender, EventArgs e)
        {
           
            hanslaserSetCtr1.init(CordSet1);//二维码参数            
           
         

            

            pg2_txt_outtime.Text = SysStatus.outTime.ToString();

            if (SysStatus.Protect == 1)
                pg2_chk_protect.Checked = true;
            else
                pg2_chk_protect.Checked = false;


            if (SysStatus.shield_light == 1)
                PG2_CHK_shield1.Checked = true;
            else
                PG2_CHK_shield1.Checked = false;

            if (SysStatus.shield_door == 1)
                PG2_CHK_shield2.Checked = true;
            else
                PG2_CHK_shield2.Checked = false;

            if(SysStatus.CurProductName=="M334")
            {
                pg2_pd_m334.Checked = true;
            }else
            {
                pg2_pd_m381.Checked = true;
            }
        }
      
       

        private void pg5_btn_save_Click(object sender, EventArgs e)
        {
          //  SysStatus.hand = 0;
            SysStatus.Protect = 0;
            SysStatus.shield_light = 0;
            SysStatus.shield_door = 0;
        //    if (pg2_chk_hand.Checked)//手动
         //       SysStatus.hand = 1;
            if (pg2_chk_protect.Checked)//碰撞
                SysStatus.Protect = 1;
            if (PG2_CHK_shield1.Checked)//屏蔽光幕
                SysStatus.shield_light = 1;
            if (PG2_CHK_shield2.Checked)//屏蔽光幕
                SysStatus.shield_door = 1;
            int s = 120;
            try
            {
                s = int.Parse(pg2_txt_outtime.Text);
            }
            catch
            {
                MessageBox.Show("值错误");
                return;
            }

            if (pg2_pd_m334.Checked)
                SysStatus.CurProductName = "M334";
            else
                SysStatus.CurProductName = "M381";

            SysStatus.outTime = s;
            SysStatus.SaveConfig(SysStatus.sys_dir_path);
            Logger.Info($"系统参数修改成功");//写入日志.
            MessageBox.Show("ok");
        }

        private void PG4_BTN_LOAD_Click(object sender, EventArgs e)
        {
            PG4_CCD_C_POS.Load_Postion(0);
        }

        private void PG4_BTN_SAVE_Click(object sender, EventArgs e)
        {
            PG4_CCD_C_POS.Save_Postion(0);
        }

        private void PG7_BTN_LOAD_Click(object sender, EventArgs e)
        {
            PG7_CCD_C_POS.Load_Postion(1);
        }

        private void PG7_BTN_SAVE_Click(object sender, EventArgs e)
        {
            PG7_CCD_C_POS.Save_Postion(1);
        }

        private void PG3_BTN_LOAD_Click(object sender, EventArgs e)
        {
            PG3_CCD_C_HIGH.Load_High(0);
        }

        private void PG3_BTN_SAVE_Click(object sender, EventArgs e)
        {
            PG3_CCD_C_HIGH.Save_High(0);
        }

        private void PG6_BTN_LOAD_Click(object sender, EventArgs e)
        {
            PG6_CCD_C_HIGH.Load_High(1);
        }

        private void PG6_BTN_SAVE_Click(object sender, EventArgs e)
        {
            PG6_CCD_C_HIGH.Save_High(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FlowMeter flow= flowMeter.LoadParmMeter();
            tb_FlowMeterUp.Text = flow.flowMeterUp.ToString();
            tb_FlowMeterDown.Text = flow.flowMeterDown.ToString();

          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flowMeter.SaveParmHigh(tb_FlowMeterUp.Text,tb_FlowMeterDown.Text) ;
            MessageBox.Show("设置成功!");
        }
    }
}
