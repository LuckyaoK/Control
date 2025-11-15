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
    public partial class DaDianShowCtr : UserControl
    {
        public DaDianShowCtr()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 清除显示
        /// </summary>
        public void ClearAll()
        {
            Invoke(new Action(() => {
                showName1.Data_Text = showName2.Data_Text = "--";
            showName1.DataBack_Color = showName2.DataBack_Color = MyLib.OldCtr.ColorType1.Normal;
            }));
        }
        /// <summary>
        /// 显示结果内容
        /// </summary>
        public void ShowAll(string cord,string res)
        {
            Invoke(new Action(() => {
                showName1.Data_Text = cord;
                showName2.Data_Text = res;
                if (res.Contains("NG")) showName2.DataBack_Color = MyLib.OldCtr.ColorType1.Red;
                else showName2.DataBack_Color = MyLib.OldCtr.ColorType1.Green;
                 
            }));
        }





        private string p1text = "结果";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("结果名称")]
        public string P1Text
        {
            get { return p1text; }
            set
            {
                this.showName2.Name_Text = p1text = value;

            }
        }
    }
}
