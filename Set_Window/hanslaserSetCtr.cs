using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CXPro001.classes;
using CXPro001.myclass;

 
using MyLib.Utilitys;
using static MyLib.Sys.Logger;

namespace CXPro001.setups
{
    /// <summary>
    /// 刻字规则设置控件
    /// </summary>
    public partial class hanslaserSetCtr : UserControl
    {
        string CoderMain;
        int CoderLen;
        public hanslaserSetCtr()
        {
            InitializeComponent();
        }
        CordSet mycordset = null;
        public void init(CordSet mycordset1)
        {
            //dataGridView1.Rows.Add(mycordset.ProductName,"", mycordset.BarCord);
            //加载所有的二维码配置
            string filename = SysStatus.sys_dir_path + "\\product\\CordSET\\";              

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(filename);
            FileInfo[] allFiles = dir.GetFiles(".", SearchOption.AllDirectories);
          
            foreach (FileInfo file in allFiles)
            {
              if(Path.GetExtension(file.Name).Contains("ini"))
                {
                   
                    mycordset = new CordSet();
                    mycordset.LoadParameter(file.FullName);
                    dataGridView1.Rows.Add(mycordset.ProductName,mycordset.CordLevel, mycordset.CordLens.ToString(), mycordset.desr,mycordset.CordCount.ToString(), mycordset.BarCord);
                }
            }
            mycordset = null;

        }
        private    FileInfo[] GetAllFileInfo2(System.IO.DirectoryInfo dir)
        {
            return dir.GetFiles(".",  SearchOption.AllDirectories);
        }

            #region 编码规则-------------------------------------------------------------------------------------------------     
            /// <summary>
            /// 刷新类型-生产确认
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void button4_Click(object sender, EventArgs e)
        {
            if (this.txt_Product_mode.Text.Length > 0)
            {
                this.textBox4.Text = CordByAdd();
                
                textBox6.Text = (textBox4.Text.Trim()).Length.ToString();//长度
                this.panel4.Enabled = true;
            }
            else
            {
                MessageBox.Show("产品型号不能为空请填写型号", "参数设置");
            }
        }
        /// <summary>
        /// 将配方组合控件的值组合--生成确认按钮用  不存档
        /// </summary>
        /// <returns></returns>
        private string CordByAdd()
        {
            string str1 = GetCotorl(this.comboBox1.SelectedIndex, textB1);
            string str2 = GetCotorl(this.comboBox2.SelectedIndex, textB2);
            string str3 = GetCotorl(this.comboBox3.SelectedIndex, textB3);
            string str4 = GetCotorl(this.comboBox4.SelectedIndex, textB4);
            string str5 = GetCotorl(this.comboBox5.SelectedIndex, textB5);
            string str6 = GetCotorl(this.comboBox6.SelectedIndex, textB6);
            string str7 = GetCotorl(this.comboBox7.SelectedIndex, textB7);
            string str8 = GetCotorl(this.comboBox8.SelectedIndex, textB8);
            string str9 = GetCotorl(this.comboBox9.SelectedIndex, textB9);
            string str10 = GetCotorl(this.comboBox10.SelectedIndex, textB10);
            string str11 = GetCotorl(this.comboBox11.SelectedIndex, textB11);
            string str12 = GetCotorl(this.comboBox12.SelectedIndex, textB12);
            string str13 = GetCotorl(this.comboBox13.SelectedIndex, textB13);

            // CoderMain = str1 + str2 + str3 + str4 + str5 + str6 + str7 + str8;
            // CoderLen = str9.Length;

            CoderMain = "";
            if (str1.Length > 0)
                CoderMain += "@" + str1;
            if (str2.Length > 0)
                CoderMain += "@" + str2;
            if (str3.Length > 0)
                CoderMain += "@" + str3;
            if (str4.Length > 0)
                CoderMain += "@" + str4;
            if (str5.Length > 0)
                CoderMain += "@" + str5;

            CoderMain += "@" + str6 + str7 + str8;

            //   CoderMain = str1 + str2 + str3 + str4 + str5 + str6 + str7 + str8;

            CoderLen = str9.Length;

            return CoderMain + "@" + str9;

            //  return str1 + str2 + str3 + str4 + str5 + str6 + str7 + str8 + str9 + str10 + str11 + str12 + str13;




        }
        #region 不用
        /// <summary>
        /// 获取要刻的二维码
        /// </summary>
        /// <returns></returns>
        public string GetCord()
        {
            string str1 = GetCotorl1(Convert.ToInt32((mycordset.Rule1.Split(','))[0]), mycordset.Rule1.Split(',')[1]);
            string str2 = GetCotorl1(Convert.ToInt32((mycordset.Rule2.Split(','))[0]), mycordset.Rule2.Split(',')[1]);
            string str3 = GetCotorl1(Convert.ToInt32((mycordset.Rule3.Split(','))[0]), mycordset.Rule3.Split(',')[1]);
            string str4 = GetCotorl1(Convert.ToInt32((mycordset.Rule4.Split(','))[0]), mycordset.Rule4.Split(',')[1]);
            string str5 = GetCotorl1(Convert.ToInt32((mycordset.Rule5.Split(','))[0]), mycordset.Rule5.Split(',')[1]);
            string str6 = GetCotorl1(Convert.ToInt32((mycordset.Rule6.Split(','))[0]), mycordset.Rule6.Split(',')[1]);
            string str7 = GetCotorl1(Convert.ToInt32((mycordset.Rule7.Split(','))[0]), mycordset.Rule7.Split(',')[1]);
            string str8 = GetCotorl1(Convert.ToInt32((mycordset.Rule8.Split(','))[0]), mycordset.Rule8.Split(',')[1]);
            string str9 = GetCotorl1(Convert.ToInt32((mycordset.Rule9.Split(','))[0]), mycordset.Rule9.Split(',')[1]);
            string str10 = GetCotorl1(Convert.ToInt32((mycordset.Rule10.Split(','))[0]), mycordset.Rule10.Split(',')[1]);
            string str11 = GetCotorl1(Convert.ToInt32((mycordset.Rule11.Split(','))[0]), mycordset.Rule11.Split(',')[1]);
            string str12 = GetCotorl1(Convert.ToInt32((mycordset.Rule12.Split(','))[0]), mycordset.Rule12.Split(',')[1]);
            string str13 = GetCotorl1(Convert.ToInt32((mycordset.Rule13.Split(','))[0]), mycordset.Rule13.Split(',')[1]);
            return str1 + str2 + str3 + str4 + str5 + str6 + str7 + str8 + str9 + str10 + str11 + str12 + str13;
          
        }
        private string GetCotorl1(int result, string texs)
        {
            string str = result.ToString();
            string var = null;
            string rcv = null;//班次
            if (result > 0)
            {
                switch (str)
                {
                    case "1":

                    case "2":

                    case "3":

                    case "4":

                    case "5":
                        var = "string";
                        break;
                    case "6":
                        var = "year";
                        break;
                    case "7":
                        var = "month";
                        break;
                    case "8":
                        var = "day";
                        break;
                    case "9":
                        var = "hour";
                        break;
                    case "10":
                        var = "min";
                        break;
                    case "11":
                        var = "sec";
                        break;
                    case "12":
                        var = "yearday";
                        break;
                    case "13":
                        var = "cord";
                        break;
                    case "14":
                        var = "string";
                        break;
                    case "15":
                        var = "banci";
                        break;
                    default:
                        break;
                }
            }

            if (var == "banci")
            {
                string st1 = "08:00";
                string st2 = "20:00";
                DateTime dt1 = Convert.ToDateTime(st1);
                DateTime dt2 = Convert.ToDateTime(st2);
                DateTime dt3 = DateTime.Now;
                if (DateTime.Compare(dt3, dt1) > 0 && DateTime.Compare(dt3, dt2) < 0)
                {
                    rcv = "1";
                }
                else
                {
                    rcv = "2";
                }
            }

            if (var == "string")
            {
                rcv = texs;
            }
            else if (var == "year")
            {
                rcv = (DateTime.Now.Year - 2000).ToString();
            }
            else if (var == "month")
            {
                if (texs == "16#")
                {
                    if (DateTime.Now.Month == 10)
                    {
                        rcv = "A";
                    }
                    else if (DateTime.Now.Month == 11)
                    {
                        rcv = "B";
                    }
                    else if (DateTime.Now.Month == 12)
                    {
                        rcv = "C";
                    }
                    else
                    {
                        rcv = DateTime.Now.Month.ToString();
                    }
                }

                if (texs== "2#")
                {
                    int a = DateTime.Now.Month;
                    rcv = string.Format("{0:D2}", a);
                }
            }
            else if (var == "day")
            {
                int a = DateTime.Now.Day;
                rcv = string.Format("{0:D2}", a);
            }
            else if (var == "hour")
            {
                int a = DateTime.Now.Hour;
                rcv = string.Format("{0:D2}", a);
            }
            else if (var == "min")
            {
                int a = DateTime.Now.Minute;
                rcv = string.Format("{0:D2}", a);
            }
            else if (var == "sec")
            {
                int a = DateTime.Now.Second;
                rcv = string.Format("{0:D2}", a);
            }
            else if (var == "yearday")
            {
                int a = DateTime.Now.DayOfYear;
                rcv = string.Format("{0:D3}", a);
            }
            else if (var == "cord")
            {
                if (texs == "5")
                {
                    rcv = string.Format("{0:D5}", mycordset != null ? mycordset.CordNO : 1);
                }
                else
                if (texs == "6")
                {
                    rcv = string.Format("{0:D6}", mycordset != null ? mycordset.CordNO : 1);
                }
                else
                if (texs == "7")
                {
                    rcv = string.Format("{0:D7}", mycordset != null ? mycordset.CordNO : 1);
                }
                else
                if (texs == "8")
                {
                    rcv = string.Format("{0:D8}", mycordset != null ? mycordset.CordNO : 1);
                }
                else
                if (texs == "9")
                {
                    rcv = string.Format("{0:D9}", mycordset != null ? mycordset.CordNO : 1);
                }
                else
                if (texs == "10")
                {
                    rcv = string.Format("{0:D10}", mycordset != null ? mycordset.CordNO : 1);
                }
                else
                {
                    rcv = string.Format("{0:D5}", mycordset != null ? mycordset.CordNO : 1);
                }
                if (mycordset != null)
                {
                    mycordset.CordNO++;
                    mycordset.SaveParameter("");
                }
            }
            return rcv;
        }
        #endregion
        /// <summary>
        /// 获取配方组合控件的值，解析--确认组成用，不存档
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private string GetCotorl(int result,TextBox texs)
        {
            string str = result.ToString();
            string var = null;
            string rcv = null;//班次
            if (result > 0)
            {
                switch (str)
                {
                    case "1":

                    case "2":

                    case "3":

                    case "4":

                    case "5":
                        var = "string";
                        break;
                    case "6":
                        var = "year";
                        break;
                    case "7":
                        var = "month";
                        break;
                    case "8":
                        var = "day";
                        break;
                    case "9":
                        var = "hour";
                        break;
                    case "10":
                        var = "min";
                        break;
                    case "11":
                        var = "sec";
                        break;
                    case "12":
                        var = "yearday";
                        break;
                    case "13":
                        var = "cord";
                        break;
                    case "14":
                        var = "string";
                        break;
                    case "15":
                        var = "banci";
                        break;
                    default:
                        break;
                }
            }

            if (var == "banci")
            {
                string st1 = "08:00";
                string st2 = "20:00";
                DateTime dt1 = Convert.ToDateTime(st1);
                DateTime dt2 = Convert.ToDateTime(st2);
                DateTime dt3 = DateTime.Now;
                if (DateTime.Compare(dt3, dt1) > 0 && DateTime.Compare(dt3, dt2) < 0)
                {
                    rcv = "1";
                }
                else
                {
                    rcv = "2";
                }
            }

            if (var == "string")
            {
                rcv = texs.Text.Trim();
            }
            else if (var == "year")
            {
                rcv = (DateTime.Now.Year - 2000).ToString();
            }
            else if (var == "month")
            {
                if (texs.Text.Trim() == "16#")
                {
                    if (DateTime.Now.Month == 10)
                    {
                        rcv = "A";
                    }
                    else if (DateTime.Now.Month == 11)
                    {
                        rcv = "B";
                    }
                    else if (DateTime.Now.Month == 12)
                    {
                        rcv = "C";
                    }
                    else
                    {
                        rcv = DateTime.Now.Month.ToString();
                    }
                }

                if (texs.Text.Trim() == "2#")
                {
                    int a = DateTime.Now.Month;
                    rcv = string.Format("{0:D2}", a);
                }
            }
            else if (var == "day")
            {
                int a = DateTime.Now.Day;
                rcv = string.Format("{0:D2}", a);
            }
            else if (var == "hour")
            {
                int a = DateTime.Now.Hour;
                rcv = string.Format("{0:D2}", a);
            }
            else if (var == "min")
            {
                int a = DateTime.Now.Minute;
                rcv = string.Format("{0:D2}", a);
            }
            else if (var == "sec")
            {
                int a = DateTime.Now.Second;
                rcv = string.Format("{0:D2}", a);
            }
            else if (var == "yearday")
            {
                int a = DateTime.Now.DayOfYear;
                rcv = string.Format("{0:D3}", a);
            }
            else if (var == "cord")
            {
                if (texs.Text.Trim() == "5")
                {
                    rcv = string.Format("{0:D5}", mycordset != null ? mycordset.CordNO : 1);
                }
                else
                if (texs.Text.Trim() == "6")
                {
                    rcv = string.Format("{0:D6}", mycordset != null ? mycordset.CordNO : 1);
                }
                else
                if (texs.Text.Trim() == "7")
                {
                    rcv = string.Format("{0:D7}", mycordset != null ? mycordset.CordNO : 1);
                }
                else
                if (texs.Text.Trim() == "8")
                {
                    rcv = string.Format("{0:D8}", mycordset != null ? mycordset.CordNO : 1);
                }
                else
                if (texs.Text.Trim() == "9")
                {
                    rcv = string.Format("{0:D9}", mycordset != null ? mycordset.CordNO : 1);
                }
                else
                if (texs.Text.Trim() == "10")
                {
                    rcv = string.Format("{0:D10}", mycordset != null ? mycordset.CordNO : 1);
                }
                else
                {
                    rcv = string.Format("{0:D5}", mycordset != null ? mycordset.CordNO : 1);
                }

                //if (mycordset != null)
                //{
                //    mycordset.CordNO++;
                //    mycordset.SaveParameter("");
                //}  
               

            }
            return rcv;
        }
        #endregion
 
        /// <summary>
        /// 将配置class里面的规则显示到控件上
        /// </summary>
        public void LoadSets()
        {
            if(txt_Product_mode.Text==null || txt_Product_mode.Text==""|| txt_Product_mode.Text.Trim().Length<1)
            {
                MessageBox.Show("产品型号为空，无法加载");
                return;
            }
          //  mycordset.LoadParameter("textBox1.Text.Trim()");
            txt_Product_mode.Text = mycordset.ProductName;
            textBox4.Text = mycordset.BarCord;
            textBox5.Text = mycordset.CordCount.ToString();
            comboBox1.SelectedIndex = Convert.ToInt32((mycordset.Rule1.Split(','))[0]);
            comboBox2.SelectedIndex = Convert.ToInt32((mycordset.Rule2.Split(','))[0]);
            comboBox3.SelectedIndex = Convert.ToInt32((mycordset.Rule3.Split(','))[0]);
            comboBox4.SelectedIndex = Convert.ToInt32((mycordset.Rule4.Split(','))[0]);
            comboBox5.SelectedIndex = Convert.ToInt32((mycordset.Rule5.Split(','))[0]);
            comboBox6.SelectedIndex = Convert.ToInt32((mycordset.Rule6.Split(','))[0]);
            comboBox7.SelectedIndex = Convert.ToInt32((mycordset.Rule7.Split(','))[0]);
            comboBox8.SelectedIndex = Convert.ToInt32((mycordset.Rule8.Split(','))[0]);
            comboBox9.SelectedIndex = Convert.ToInt32((mycordset.Rule9.Split(','))[0]);
            comboBox10.SelectedIndex = Convert.ToInt32((mycordset.Rule10.Split(','))[0]);
            comboBox11.SelectedIndex = Convert.ToInt32((mycordset.Rule11.Split(','))[0]);
            comboBox12.SelectedIndex = Convert.ToInt32((mycordset.Rule12.Split(','))[0]);
            comboBox13.SelectedIndex = Convert.ToInt32((mycordset.Rule13.Split(','))[0]);
            textB1.Text = (mycordset.Rule1.Split(','))[1];
            textB2.Text = (mycordset.Rule2.Split(','))[1];
            textB3.Text = (mycordset.Rule3.Split(','))[1];
            textB4.Text = (mycordset.Rule4.Split(','))[1];
            textB5.Text = (mycordset.Rule5.Split(','))[1];
            textB6.Text = (mycordset.Rule6.Split(','))[1];
            textB7.Text = (mycordset.Rule7.Split(','))[1];
            textB8.Text = (mycordset.Rule8.Split(','))[1];
            textB9.Text = (mycordset.Rule9.Split(','))[1];
            textB10.Text = (mycordset.Rule10.Split(','))[1];
            textB11.Text = (mycordset.Rule11.Split(','))[1];
            textB12.Text = (mycordset.Rule12.Split(','))[1];
            textB13.Text = (mycordset.Rule13.Split(','))[1];
        }
        
        private void button2_Click(object sender, EventArgs e)//修改当前规则
        {
            if(txt_Product_mode.Text!=null & textBox4.Text != null && txt_Product_mode.Text.Length > 0)
            {
                mycordset.ProductName = txt_Product_mode.Text.Trim();
                mycordset.BarCord = textBox4.Text.Trim();
                mycordset.CordCount = Convert.ToInt32(textBox5.Text);
                mycordset.CordLevel= textBox2.Text.Trim();
                mycordset.CordLens = Convert.ToInt32(textBox6.Text);
                mycordset.Rule1 = comboBox1.SelectedIndex.ToString() + "," + textB1.Text.Trim();
                mycordset.Rule2 = comboBox2.SelectedIndex.ToString() + "," + textB2.Text.Trim();
                mycordset.Rule3 = comboBox3.SelectedIndex.ToString() + "," + textB3.Text.Trim();
                mycordset.Rule4 = comboBox4.SelectedIndex.ToString() + "," + textB4.Text.Trim();
                mycordset.Rule5 = comboBox5.SelectedIndex.ToString() + "," + textB5.Text.Trim();
                mycordset.Rule6 = comboBox6.SelectedIndex.ToString() + "," + textB6.Text.Trim();
                mycordset.Rule7 = comboBox7.SelectedIndex.ToString() + "," + textB7.Text.Trim();
                mycordset.Rule8 = comboBox8.SelectedIndex.ToString() + "," + textB8.Text.Trim();
                mycordset.Rule9 = comboBox9.SelectedIndex.ToString() + "," + textB9.Text.Trim();
                mycordset.Rule10 = comboBox10.SelectedIndex.ToString() + "," + textB10.Text.Trim();
                mycordset.Rule11 = comboBox11.SelectedIndex.ToString() + "," + textB11.Text.Trim();
                mycordset.Rule12 = comboBox12.SelectedIndex.ToString() + "," + textB12.Text.Trim();
                mycordset.Rule13 = comboBox13.SelectedIndex.ToString() + "," + textB13.Text.Trim();
            }
            else
            {
                MessageBox.Show("新规则未生产，不能点击保存！");
            }
        }

        private void button3_Click(object sender, EventArgs e)//保存当前规则
        {
            if (txt_Product_mode.Text != null & textBox4.Text != null && txt_Product_mode.Text.Length>0)
            {
                mycordset = new CordSet();
                mycordset.ProductName = txt_Product_mode.Text.Trim();
                mycordset.BarCord = textBox4.Text.Trim();
                mycordset.CordCount = Convert.ToInt32(textBox5.Text);
                mycordset.desr = textBox1.Text.Trim();
                mycordset.CordLevel = textBox2.Text.Trim();
                mycordset.Rule1 = comboBox1.SelectedIndex.ToString() + "," + textB1.Text.Trim();
                mycordset.Rule2 = comboBox2.SelectedIndex.ToString() + "," + textB2.Text.Trim();
                mycordset.Rule3 = comboBox3.SelectedIndex.ToString() + "," + textB3.Text.Trim();
                mycordset.Rule4 = comboBox4.SelectedIndex.ToString() + "," + textB4.Text.Trim();
                mycordset.Rule5 = comboBox5.SelectedIndex.ToString() + "," + textB5.Text.Trim();
                mycordset.Rule6 = comboBox6.SelectedIndex.ToString() + "," + textB6.Text.Trim();
                mycordset.Rule7 = comboBox7.SelectedIndex.ToString() + "," + textB7.Text.Trim();
                mycordset.Rule8 = comboBox8.SelectedIndex.ToString() + "," + textB8.Text.Trim();
                mycordset.Rule9 = comboBox9.SelectedIndex.ToString() + "," + textB9.Text.Trim();
                mycordset.Rule10 = comboBox10.SelectedIndex.ToString() + "," + textB10.Text.Trim();
                mycordset.Rule11 = comboBox11.SelectedIndex.ToString() + "," + textB11.Text.Trim();
                mycordset.Rule12 = comboBox12.SelectedIndex.ToString() + "," + textB12.Text.Trim();
                mycordset.Rule13 = comboBox13.SelectedIndex.ToString() + "," + textB13.Text.Trim();
                mycordset.CoderMain = CoderMain;
                mycordset.CodeLen = CoderLen;
                mycordset.SaveParameter(txt_Product_mode.Text.Trim(), true);
                MessageBox.Show(" 保存完成！");
            }
            else
            {
                MessageBox.Show("新规则未生产，不能点击保存！");
            }
              
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_Product_mode.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();//获取点击行
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();//获取点击行
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();//获取点击行
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();//获取点击行
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();//获取点击行
            mycordset = new CordSet();
            string path= SysStatus.sys_dir_path + "\\product\\CordSET\\"+ txt_Product_mode.Text+".ini";
            mycordset.LoadParameter(path);//加载配置
            LoadSets();
        }

        private void button5_Click(object sender, EventArgs e)//新建规则
        {
            if (txt_Product_mode.Text == null || txt_Product_mode.Text == "")
            {
                MessageBox.Show("没输入“产品型号”，无法创建");
                return;
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (txt_Product_mode.Text.Trim() == dataGridView1.Rows[i].Cells[0].Value.ToString())
                {
                    MessageBox.Show("当前产品型号存在，无法新增");
                    return;

                }
            }
            dataGridView1.Rows.Add(txt_Product_mode.Text.Trim(), textBox2.Text.Trim(), textBox6.Text.Trim(), textBox1.Text.Trim(), textBox5.Text.Trim(), textBox4.Text.Trim());


            MessageBox.Show("新增完成，需要点击保存才能保存到本地");






        }






        /// <summary>
        /// 右键菜单
        /// </summary>
        private  readonly ContextMenuStrip Menu = new ContextMenuStrip();
        private void hanslaserSetCtr_Load(object sender, EventArgs e)
        {
            //给dataview添加右击菜单
            ToolStripMenuItem tsmfilter =
                   new ToolStripMenuItem("删除选择行")
                   {
                       CheckOnClick = false,
                       Name = "filter"
                   };
            tsmfilter.Click += tsmfilter_Click;
            Menu.Items.Add(tsmfilter);

            ToolStripMenuItem tsmall = new ToolStripMenuItem("返回")
            {
                CheckOnClick = false,
                Name =  "asd"
            };           
            Menu.Items.Add(tsmall);        
            dataGridView1.ContextMenuStrip = Menu;
        }


        private   void tsmfilter_Click(object sender, EventArgs e)//删除选择行
        {
          if(  MessageBox.Show("确定要删除选中的行的对应的产品二维码参数吗？" ,"警告！", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)== DialogResult.Yes)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                   
                    DataGridViewRow aa = dataGridView1.SelectedRows[0] as DataGridViewRow;
                    string proname = aa.Cells[0].Value.ToString();
                    string path = $"{Path.GetFullPath("..")}\\product\\CordSET\\{proname.Trim()}.ini";// 配置文件路径
                    File.Delete(path);
                    dataGridView1.Rows.Remove(aa);
                    Logger.Info("删除选择行成功");
                }              
            }



        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
