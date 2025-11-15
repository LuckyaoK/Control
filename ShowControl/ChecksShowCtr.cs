using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CXPro001.ShowControl;
using MyLib.OldCtr;

namespace CXPro001.ShowControl
{
    public partial class ChecksShowCtr : UserControl
    {
        public ChecksShowCtr()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 显示二维码
        /// </summary>
        /// <param name="cord"></param>
        public void showcord(string cord)
        {
            showName1.Data_Text = cord;
        }
        /// <summary>
        /// 显示螺纹检测1结果
        /// </summary>
        /// <param name="res">判断结果</param>
        public void showluowen1(string res)
        {
            showName2.Data_Text = res;         
            if(res=="OK") showName2.DataBack_Color = ColorType1.Green;      
            else showName2.DataBack_Color = ColorType1.Red;      
        }
        /// <summary>
        /// 显示螺纹检测1结果
        /// </summary>
        /// <param name="res">判断结果</param>
        public void showluowen2(string res)
        {
            showName5.Data_Text = res;
            if (res == "OK") showName5.DataBack_Color = ColorType1.Green;
            else showName5.DataBack_Color = ColorType1.Red;
        }
        /// <summary>
        /// 显示螺纹检测1结果
        /// </summary>
        /// <param name="res">判断结果</param>
        public void showluowen3(string res)
        {
            showName4.Data_Text = res;
            if (res == "OK") showName4.DataBack_Color = ColorType1.Green;
            else showName4.DataBack_Color = ColorType1.Red;
        }
        /// <summary>
        /// 显示铜排检测结果
        /// </summary>
        /// <param name="res">判断结果</param>
        public void showCupai(string res)
        {
            showName3.Data_Text = res;
            if (res == "OK") showName3.DataBack_Color = ColorType1.Green;         
            else showName3.DataBack_Color = ColorType1.Red;
            
        }
        /// <summary>
        /// 显示铆点检测结果
        /// </summary>
        /// <param name="res">判断结果</param>
        public void showMaodian(string res)
        {
            showName6.Data_Text = res;
            if (res == "OK") showName6.DataBack_Color = ColorType1.Green;
            else showName6.DataBack_Color = ColorType1.Red;

        }
    }
}
