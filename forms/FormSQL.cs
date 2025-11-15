using CXPro001.myclass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CXPro001.forms
{
    public partial class FormSQL : Form
    {
        public FormSQL()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 写入产品型号
        /// </summary>
        /// <param name="namep"></param>
        public void writepro(string namep)
        {
            sqlData1.WriteProduct(namep);
        }

        private void FormSQL_Load(object sender, EventArgs e)
        {
            sqlData1.WriteProduct(SysStatus.CurProductName);
        }
    }
}
