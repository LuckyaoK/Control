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
    public partial class BarTestShowCtrl : UserControl
    {
        public BarTestShowCtrl()
        {
            InitializeComponent();
        }
       
        /// <summary>
        /// 展现气密检测结果
        /// </summary>
        /// <param name="cord">二维码</param>
        /// <param name="proname">产品型号</param>
        /// <param name="firstsao">单位bar的泄漏值</param>
        /// <param name="secondsao">单位ml/min的泄漏值</param>
        /// <param name="lastres">最终结果OK:合格，NG：不合格的</param>
        public void ShowControl(string cord, string proname, string firstsao, string secondsao, string lastres)
        {
            labe1.Text = cord;
            labe2.Text = proname;
            label9.Text = firstsao;
            label13.Text = secondsao;          
            labe6.Text = lastres;
            //if (firstsao == "false") labe3.BackColor = Color.Red;
            //if (secondsao == "false") labe5.BackColor = Color.Red;
            if (lastres == "NG") labe6.BackColor = Color.Red;
            else labe6.BackColor = Color.Lime;
        }
        /// <summary>
        /// 显示上限值
        /// </summary>
        /// <param name="UP1">上限 bar</param>
        /// <param name="UP2">上限ml/min</param>
        public void ShowUP(string UP1,string UP2)
        {
            label15.Text = UP1;
            label17.Text = UP2;

        }
    }
}
