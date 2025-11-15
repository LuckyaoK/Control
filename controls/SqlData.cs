
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLib.Param;
using MyLib.SqlHelper;
using MyLib.Sys;

namespace CXPro001.controls
{
    public partial class SqlData : UserControl
    {
        /// <summary>
        /// 当前数据表单
        /// </summary>
        public DataTable CurDatatTable;

        public SqlData()
        {
            InitializeComponent();
           
             
        }
        /// <summary>
        /// 写入产品型号
        /// </summary>
        /// <param name="NAME"></param>
        public void WriteProduct(string NAME)
        {
            txt_pro_name.Text = NAME;
        }
        private void button1_Click(object sender, EventArgs e)//根据时间查询
        {
            DateTime start = Convert.ToDateTime(this.txt_StartTime.Text);
            DateTime end = Convert.ToDateTime(this.txt_EndTime.Text);
            if (start == end) end = start.AddDays(1.0);
            dgv_dataX.DataSource = null;

            if (comboBox1.Text.Trim() == "全部")
            {
                DataSet aa = SQLHelper.GetDataSetT(start, end, txt_pro_name.Text);
                if (aa!=null &&aa.Tables.Count > 0)
                {
                    dgv_dataX.DataSource = aa.Tables[0].DefaultView;
                    dgv_dataX.ColumnHeadersVisible = true;
                }
            }
             
            else if (comboBox1.Text.Trim() == "耐压")
            {
                string sql = $" select ID,InsertTime AS 时间,ModeNumber as 模号,Cord as 二维码,Result as 结果,SOresPin1 as 通断1," +
                    $"SOresPin2 as 通断2,SOresPin3 as 通断3,SOresPin4 as 通断4,SOresPin5 as 通断5,SOresPin6 as 通断6,VolvalPin1 as 电流1,VolvalPin2 as 电流2,VolvalPin3 as 电流3,VolvalPin4 as 电流4,VolvalPin5 as 电流5,VolvalPin6 as 电流6 from VoltageData_8740 where InsertTime between '{start}' and '{end}' and ProductType = '{txt_pro_name.Text}'";
                DataSet aa = SQLHelper.GetDataSet(sql);
                if (aa.Tables.Count > 0)
                {
                    dgv_dataX.DataSource = aa.Tables[0].DefaultView;
                    dgv_dataX.ColumnHeadersVisible = true;
                }
            }            
            else if (comboBox1.Text.Trim() == "插口位置")
            {
                string sql = $" select * from Position_CCD where InsertTime between '{start}' and '{end}' and ProductType = '{txt_pro_name.Text}' ORDER BY ID DESC";
                DataSet aa = SQLHelper.GetDataSet(sql);
                if (aa.Tables.Count > 0)
                {
                    dgv_dataX.DataSource = aa.Tables[0].DefaultView;
                    dgv_dataX.ColumnHeadersVisible = true;
                }
            }
            else if (comboBox1.Text.Trim() == "插口高度")
            {
                string sql = $" select * from Height_CCD where InsertTime between '{start}' and '{end}' and ProductType = '{txt_pro_name.Text}' ORDER BY ID DESC";
                DataSet aa = SQLHelper.GetDataSet(sql);
                if (aa.Tables.Count > 0)
                {
                    dgv_dataX.DataSource = aa.Tables[0].DefaultView;
                    dgv_dataX.ColumnHeadersVisible = true;
                }
            }
            else if (comboBox1.Text.Trim() == "内部位置")
            {
                string sql = $" select * from Position_CCD2 where InsertTime between '{start}' and '{end}' and ProductType = '{txt_pro_name.Text}' ORDER BY ID DESC";
                DataSet aa = SQLHelper.GetDataSet(sql);
                if (aa.Tables.Count > 0)
                {
                    dgv_dataX.DataSource = aa.Tables[0].DefaultView;
                    dgv_dataX.ColumnHeadersVisible = true;
                }
            }
            else if (comboBox1.Text.Trim() == "内部高度")
            {
                string sql = $" select * from Height_CCD2 where InsertTime between '{start}' and '{end}' and ProductType = '{txt_pro_name.Text}' ORDER BY ID DESC";
                DataSet aa = SQLHelper.GetDataSet(sql);
                if (aa.Tables.Count > 0)
                {
                    dgv_dataX.DataSource = aa.Tables[0].DefaultView;
                    dgv_dataX.ColumnHeadersVisible = true;
                }
            }
           lbl_sql_sum.Text= dgv_dataX.RowCount.ToString();

        }
        private void button2_Click(object sender, EventArgs e)//根据二维码查询
        {
            if(textBox1.Text!=null &textBox1.Text.Length>1)
            {
               
                if (comboBox1.Text.Trim() == "全部")
                {
                    dgv_dataX.DataSource = null;
                    DataSet aa = SQLHelper.GetDataSetS(textBox1.Text);
                    if (aa.Tables.Count > 0)
                    {
                        dgv_dataX.DataSource = aa.Tables[0].DefaultView;
                        dgv_dataX.ColumnHeadersVisible = true;
                    }
                }
               
                else if (comboBox1.Text.Trim() == "耐压")
                {
                    string sql = $" select * from VoltageData_8740 where Cord = '{textBox1.Text}'";
                    DataSet aa = SQLHelper.GetDataSet(sql);
                    if (aa.Tables.Count > 0)
                    {
                        dgv_dataX.DataSource = aa.Tables[0].DefaultView;
                        dgv_dataX.ColumnHeadersVisible = true;
                    }
                }
                else if (comboBox1.Text.Trim() == "插口位置")
                {
                    string sql = $" select * from Position_CCD where Cord = '{textBox1.Text}'";
                    DataSet aa = SQLHelper.GetDataSet(sql);
                    if (aa.Tables.Count > 0)
                    {
                        dgv_dataX.DataSource = aa.Tables[0].DefaultView;
                        dgv_dataX.ColumnHeadersVisible = true;
                    }
                }
                else if (comboBox1.Text.Trim() == "插口高度")
                {
                    string sql = $" select * from Height_CCD where Cord = '{textBox1.Text}'";
                    DataSet aa = SQLHelper.GetDataSet(sql);
                    if (aa.Tables.Count > 0)
                    {
                        dgv_dataX.DataSource = aa.Tables[0].DefaultView;
                        dgv_dataX.ColumnHeadersVisible = true;
                    }
                }
                else if (comboBox1.Text.Trim() == "内部位置")
                {
                    string sql = $" select * from Position_CCD2 where Cord = '{textBox1.Text}'";
                    DataSet aa = SQLHelper.GetDataSet(sql);
                    if (aa.Tables.Count > 0)
                    {
                        dgv_dataX.DataSource = aa.Tables[0].DefaultView;
                        dgv_dataX.ColumnHeadersVisible = true;
                    }
                }
                else if (comboBox1.Text.Trim() == "内部高度")
                {
                    string sql = $" select * from Height_CCD2 where Cord = '{textBox1.Text}'";
                    DataSet aa = SQLHelper.GetDataSet(sql);
                    if (aa.Tables.Count > 0)
                    {
                        dgv_dataX.DataSource = aa.Tables[0].DefaultView;
                        dgv_dataX.ColumnHeadersVisible = true;
                    }
                }
                 

            }
            else
            {
                MessageBox.Show("没有二维码无法查询，请输入二维码或者按时间查询");
            }          
        }

        private void dgv_dataX_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)//另存为
        {
            SaveFileDialog aa = new SaveFileDialog();
            aa.Title = "另存为CSV文件";
            aa.Filter = "CSV文件(*.CSV)|*.CSV|所有文件|*.*";//    "XLS文件(*.xls)|*.xls|所有文件|*.*";//设置保存文件的类型
            aa.InitialDirectory = @"C:\Users\Administrator\Documents";
            aa.RestoreDirectory = true;
            aa.CreatePrompt = true;
            DataGridViewToExcel(dgv_dataX);
            return;
            
        
        }
        public void DataGridViewToExcel(DataGridView dgv)
        {
            //程序实例化SaveFileDialog控件，并对该控件相关参数进行设置
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Execl files (*.csv)|*.csv";
            dlg.FilterIndex = 0;
            dlg.InitialDirectory = @"C:\Users\Administrator\Documents";
            dlg.RestoreDirectory = true;
            dlg.CreatePrompt = true;
            dlg.Title = "保存为csv文件";
            //以上过程也可以通过添加控件，再设置控件属性完成，此处用程序编写出来了，在移植时就可摆脱控件的限制

            if (dlg.ShowDialog() == DialogResult.OK)//打开SaveFileDialog控件，判断返回值结果
            {
                Stream myStream;//流变量
                myStream = dlg.OpenFile();//返回SaveFileDialog控件打开的文件，并将所选择的文件转化成流
                StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));//将选择的文件流生成写入流
                string columnTitle = "";
                try
                {
                    //写入列标题    
                    for (int i = 0; i < dgv.ColumnCount; i++)
                    {
                        if (i > 0)
                        {
                            columnTitle += ",";
                        }
                        columnTitle += dgv.Columns[i].HeaderText;//符号 ， 的添加，在保存为Excel时就以 ， 分成不同的列了
                    }

                    sw.WriteLine(columnTitle);//将内容写入文件流中

                    //写入列内容    
                    for (int j = 0; j < dgv.Rows.Count; j++)
                    {
                        string columnValue = "";
                        for (int k = 0; k < dgv.Columns.Count; k++)
                        {
                            if (k > 0)
                            {
                                columnValue += ",";
                            }
                            if (dgv.Rows[j].Cells[k].Value == null)
                                columnValue += "";
                            else if (dgv.Rows[j].Cells[k].Value.ToString().Contains(","))
                            {
                                columnValue += "\"" + dgv.Rows[j].Cells[k].Value.ToString().Trim() + "\"";//将单元格中的，号转义成文本
                            }
                            else
                            {
                                columnValue += dgv.Rows[j].Cells[k].Value.ToString().Trim() + "\t";//\t 横向跳格
                            }
                        }//获得写入到列中的值
                        sw.WriteLine(columnValue);//将内容写入文件流中
                    }
                    sw.Close();//关闭写入流
                    myStream.Close();//关闭流变量
                    MessageBox.Show("导出表格成功！");
                }
                catch (Exception e)
                {
                    MessageBox.Show("导出表格失败！");
                }
                finally
                {
                    sw.Close();
                    myStream.Close();
                }
            }
            else
            {
                MessageBox.Show("取消导出表格操作!");
            }
        }
 
    }
}
