using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CXPro001.ShowControl
{
    public partial class TS7631ShowCtr : UserControl
    {
        public TS7631ShowCtr()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 清除所有显示，产品型号除外
        /// </summary>
        public void clearshow()
        {
            cords.Text = "";
            label18.Text = "";
            labA1.Text = labA2.Text = labA3.Text = labA4.Text = labA5.Text = labA6.Text = "";
            labB1.Text = labB2.Text = labB3.Text = labB4.Text = labB5.Text = labB6.Text = "";
            labC1.Text = labC2.Text = labC3.Text = labC4.Text = labC5.Text = labC6.Text = "";
            labD1.Text = labD2.Text = labD3.Text = labD4.Text = labD5.Text = labD6.Text = "";
        }
        /// <summary>
        /// 显示二维码
        /// </summary>
        /// <param name="cord"></param>
        public void Showcord(string cord)
        {
            Invoke(new Action(() => { cords.Text = cord; }));
           
        }
        /// <summary>
        /// 显示产品型号 和上限电流
        /// </summary>
        /// <param name="tupe"></param>
        public void Showtype(string tupe,string upa)
        {
            Invoke(new Action(() => {
                label5.Text = tupe;
                label181.Text = upa;
            }));

        }
        /// <summary>
        /// 显示各针脚测试数据
        /// </summary>
        /// <param name="pinres"></param>
        public void ShowPins(string pinres)
        {
            Invoke(new Action(() => {
                pinres = pinres.Replace("\u000d\u000a", ".");
                string[] aa = pinres.Split('.');
                for(int i=0;i<aa.Length;i++)
                {
                    if(aa[i].Contains("01"))
                    {
                        string[] aa1 = aa[i].Split(',');
                        labA1.Text = aa1[1];//模式
                        labB1.Text = (Convert.ToDouble(aa1[2])*1000).ToString("F2");//测试电压 Convert.ToDouble(B0[4]) * 1000;//获取PIN1测量值
                        labC1.Text = (Convert.ToDouble(aa1[3]) * 1000).ToString("F2");//结果电流值
                        labD1.Text = aa1[4];
                        if (aa1[4].Contains("PASS")) labD1.BackColor = Color.Green;
                        else labD1.BackColor = Color.Red;
                    }
                    if (aa[i].Contains("02"))
                    {
                        string[] aa1 = aa[i].Split(',');
                        labA2.Text = aa1[1];//模式
                        labB2.Text = (Convert.ToDouble(aa1[2]) * 1000).ToString("F2");//测试电压 Convert.ToDouble(B0[4]) * 1000;//获取PIN1测量值
                        labC2.Text = (Convert.ToDouble(aa1[3]) * 1000).ToString("F2");//结果电流值
                        labD2.Text = aa1[4];
                        if (aa1[4].Contains("PASS")) labD2.BackColor = Color.Green;
                        else labD2.BackColor = Color.Red;
                    }
                    if (aa[i].Contains("03"))
                    {
                        string[] aa1 = aa[i].Split(',');
                        labA3.Text = aa1[1];//模式
                        labB3.Text = (Convert.ToDouble(aa1[2]) * 1000).ToString("F2");//测试电压 Convert.ToDouble(B0[4]) * 1000;//获取PIN1测量值
                        labC3.Text = (Convert.ToDouble(aa1[3]) * 1000).ToString("F2");//结果电流值
                        labD3.Text = aa1[4];
                        if (aa1[4].Contains("PASS")) labD3.BackColor = Color.Green;
                        else labD3.BackColor = Color.Red;
                    }
                    if (aa[i].Contains("04"))
                    {
                        string[] aa1 = aa[i].Split(',');
                        labA4.Text = aa1[1];//模式
                        labB4.Text = (Convert.ToDouble(aa1[2]) * 1000).ToString("F2");//测试电压 Convert.ToDouble(B0[4]) * 1000;//获取PIN1测量值
                        labC4.Text = (Convert.ToDouble(aa1[3]) * 1000).ToString("F2");//结果电流值
                        labD4.Text = aa1[4];
                        if (aa1[4].Contains("PASS")) labD4.BackColor = Color.Green;
                        else labD4.BackColor = Color.Red;
                    }
                    if (aa[i].Contains("05"))
                    {
                        string[] aa1 = aa[i].Split(',');
                        labA5.Text = aa1[1];//模式
                        labB5.Text = (Convert.ToDouble(aa1[2]) * 1000).ToString("F2");//测试电压 Convert.ToDouble(B0[4]) * 1000;//获取PIN1测量值
                        labC5.Text = (Convert.ToDouble(aa1[3]) * 1000).ToString("F2");//结果电流值
                        labD5.Text = aa1[4];
                        if (aa1[4].Contains("PASS")) labD5.BackColor = Color.Green;
                        else labD5.BackColor = Color.Red;
                    }
                    if (aa[i].Contains("06"))
                    {
                        string[] aa1 = aa[i].Split(',');
                        labA6.Text = aa1[1];//模式
                        labB6.Text = (Convert.ToDouble(aa1[2]) * 1000).ToString("F2");//测试电压 Convert.ToDouble(B0[4]) * 1000;//获取PIN1测量值
                        labC6.Text = (Convert.ToDouble(aa1[3]) * 1000).ToString("F2");//结果电流值
                        labD6.Text = aa1[4];
                        if (aa1[4].Contains("PASS")) labD6.BackColor = Color.Green;
                        else labD6.BackColor = Color.Red;
                    }
                }
                                                                                                                                        
              }));

        }
        /// <summary>
        /// 显示最终结果
        /// </summary>
        /// <param name="res"></param>
        public void Showres(string res)
        {
            label18.Text = res;
            if (res.Contains("OK")) label18.BackColor = Color.Green;
            else label18.BackColor = Color.Red;
        }
    }
}
