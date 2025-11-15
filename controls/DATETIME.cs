using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLib.Users;
using MyLib.Sys;
namespace CXPro001.controls
{
    public partial class DATETIME : UserControl
    {
        public DATETIME()
        {
            InitializeComponent();
        }
       
        private void timer1_Tick(object sender, EventArgs e)
        {

            label1.Text= DateTime.Now.ToLocalTime().ToString();        // 2008-9-4 20:12:12
            string zhou= DateTime.Now.DayOfWeek.ToString(); //获取星期   // Thursday
            switch (zhou)
            {
                case "Monday":
                    label2.Text = "星期一";
                    break;
                case "Tuesday":
                    label2.Text = "星期二";
                    break;
                case "Wednesday":
                    label2.Text = "星期三";
                    break;
                case "Thursday":
                    label2.Text = "星期四";
                    break;
                case "Friday":
                    label2.Text = "星期五";
                    break;
                case "Saturday":
                    label2.Text = "星期六";
                    break;
                case "Sunday":
                    label2.Text = "星期天";
                    break;
                default:
                    break;
            }
            try
            {
                if (SysStatus.CurUser.Name != null)
                {
                    label3.Text = "当前用户" + '\u000d' + "\u000A" + SysStatus.CurUser.Name;
                }
                else
                {
                    label3.Text = "当前用户" + '\u000d' + "\u000A" + "无";
                }
                if (SysStatus.CurProductName != null & SysStatus.CurProductName != "")
                {
                    label4.Text = "当前生产产品" + '\u000d' + "\u000A" + SysStatus.CurProductName;
                }
                else
                {
                    label4.Text = "当前生产产品" + '\u000d' + "\u000A" + "XXXXXX";
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{this.Name}:{ex.Message}");
                
            }
            

        }

        private void DATETIME_Load(object sender, EventArgs e)
        {
          //  timer1.Enabled = true;
        }
        public void strats()
        {
            timer1.Enabled = true;
        }
    }
}
