using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLib.Param;
using MyLib.Sys;
using MyLib.Utilitys;
using static MyLib.Sys.Logger;

namespace CXPro001.controls
{
    public partial class logdata : UserControl
    {
        public logdata()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            return;
            DateTime aa = monthCalendar1.SelectionStart;
            string aa1 = aa.ToString("yyyy-MM-dd");
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ParamAttribute.GelogTables(aa1);
            if(dataGridView1.Rows.Count>0)
            {
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

        }



        /// <summary>
        /// 右键菜单
        /// </summary>
        private readonly ContextMenuStrip Menu = new ContextMenuStrip();
        private void logdata_Load(object sender, EventArgs e)
        {
                                 
            foreach (int v in Enum.GetValues(typeof(InfoType)))
            {
                if (v == (int)InfoType.All || v == (int)InfoType.None) continue;
                ToolStripMenuItem tsm =
                    new ToolStripMenuItem(((InfoType)v).GetDescription())
                    {
                        CheckOnClick = false,
                        Checked = false,
                        Name = Enum.GetName(typeof(InfoType), v)
                    };
                tsm.Click += tsm_Click;
                Menu.Items.Add(tsm);
            }
            dataGridView1.ContextMenuStrip = Menu;

        }
        private void tsm_Click(object sender, EventArgs e)// 
        {
            ToolStripMenuItem AA = (ToolStripMenuItem)sender;
            string NAM = AA.Text;
            
            MyLib.Sys.Logger.Info(NAM);

            DateTime aa = monthCalendar1.SelectionStart;
            string aa1 = aa.ToString("yyyy-MM-dd");
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ParamAttribute.GelogTables(aa1,NAM);
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }






        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var txt = dataGridView1.Rows[e.RowIndex].Cells[1]?.Value?.ToString();
                switch (txt)
                {
                    case "错误":
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Pink;
                        break;
                    case "警告":
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Wheat;
                        break;
                    case "状态":
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCyan;
                        break;
                }

            }
        }
    }
}
