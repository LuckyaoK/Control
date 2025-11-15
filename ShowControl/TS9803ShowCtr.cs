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

namespace CXPro001.ShowControl
{
    public partial class TS9803ShowCtr : UserControl
    {
         
        public TS9803ShowCtr()
        {
            InitializeComponent();
        }
        public void ClearALL()
        {
            Invoke(new Action(() => { 
            label2.Text = label6.Text= label8.Text = label14.Text = label15.Text = label16.Text = "-";
            label18.Text = label19.Text = label20.Text = label22.Text = label23.Text = label24.Text = "-";
            label27.Text = label28.Text = label30.Text = label32.Text = label33.Text = label34.Text = "-";
            label36.Text = label37.Text = label38.Text = "-";
            label8.BackColor = label16.BackColor = label20.BackColor = label24.BackColor = Color.White;
            label34.BackColor = label38.BackColor = label28.BackColor = Color.White;
            }));
        }
        public void ShowAllLabel(My_9803 VoltageData1)
        {

            Invoke(new Action(() => {
                label2.Text = VoltageData1.Cords;
            label6.Text = VoltageData1.TestMode;
            label8.Text = VoltageData1.ResultAll == true ? "OK" : "NG";
            //绝缘
            label14.Text = VoltageData1.InsuVol.ToString();
            label18.Text = VoltageData1.InsuVol1.ToString();
            label22.Text = VoltageData1.InsuVol2.ToString();
            label15.Text = VoltageData1.InsuValue.ToString();
            label19.Text = VoltageData1.InsuValue1.ToString();
            label23.Text = VoltageData1.InsuValue2.ToString();
            label16.Text = VoltageData1.InsuRes.ToString();
            label20.Text = VoltageData1.InsuRes1.ToString();
            label24.Text = VoltageData1.InsuRes2.ToString();
            //耐压
            label32.Text = VoltageData1.VoltageVol.ToString();
            label36.Text = VoltageData1.VoltageVol1.ToString();
            label30.Text = VoltageData1.VoltageVol2.ToString();
            label33.Text = VoltageData1.VoltageValue.ToString();
            label37.Text = VoltageData1.VoltageValue1.ToString();
            label27.Text = VoltageData1.VoltageValue2.ToString();
            label34.Text = VoltageData1.VoltageRes.ToString();
            label38.Text = VoltageData1.VoltageRes1.ToString();
            label28.Text = VoltageData1.VoltageRes2.ToString();
            }));


        }
    }
}
