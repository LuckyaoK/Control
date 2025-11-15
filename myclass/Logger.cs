using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using MyLib.Param;
using MyLib.SQLiteHelper;
using MyLib.Utilitys;

namespace CXPro001.myclass
{
    public static class Logger
    {
        /// <summary>
        /// Log保存路径
        /// </summary>
        [Param("保存路径", "Log文件的保存路径", "日志")]
        public static string LogSavePath
        {
            get { return _logSavePath; }
            set
            {
                _logSavePath = value;
                if (_logSavePath == "")
                {
                    _logSavePath = $@"D:\log\";
                }
                //文件夹不存在则创建
                if (!Directory.Exists(_logSavePath)) Directory.CreateDirectory(_logSavePath);
            }
        }
        private static string _logSavePath = $@"D:\log\";// $@"D:\log\logtxt\{sta.ToString()}\";

        /// <summary>
        /// 右键菜单
        /// </summary>
        private static readonly ContextMenuStrip Menu = new ContextMenuStrip();

        /// <summary>
        /// 显示表格
        /// </summary>
        private static DataGridView _dgView;

        /// <summary>
        /// 搜索行数显示限定
        /// </summary>
        [Param("最大搜索行显示", "搜索行数显示限定", "日志")]
        public static int SearchLineLimit = 500;

        /// <summary>
        /// 缓存行数显示限定
        /// </summary>
        [Param("最大缓存行显示", "缓存行数显示限定")]
        public static int BufferLineLimit = 100;

        /// <summary>
        /// 延迟写入
        /// </summary>
        [Param("延迟写入(ms)", "延迟指定时间，避免频繁写入", "日志")]
        public static int DelayToSave = 500;


        /// <summary>
        /// 用于显示的表格
        /// </summary>
        public static DataGridView DgViewer
        {
            get { return _dgView; }
            set
            {
                _dgView = value;
                if (_dgView == null) return;
                if (Table == null)
                {
                    Table = new DataTable();
                    Table.Columns.Add("时间", typeof(string));
                    Table.Columns.Add("类型", typeof(string));
                    Table.Columns.Add("内容", typeof(string));
                }

                _dgView.DataSource = Table;
                _dgView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                _dgView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                _dgView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                _dgView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                _dgView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                _dgView.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                _dgView.Columns[0].Width = 100;
                _dgView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
                _dgView.GridColor = SystemColors.ControlLight;
                _dgView.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
                _dgView.ReadOnly = false;
                _dgView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                _dgView.MultiSelect = false;
                _dgView.RowHeadersVisible = false;
                _dgView.AllowUserToAddRows = false;
                _dgView.AllowUserToOrderColumns = false;
                _dgView.AllowUserToResizeRows = true;
                _dgView.AllowUserToResizeColumns = true;
                _dgView.ScrollBars = ScrollBars.Vertical;
                _dgView.BackgroundColor = SystemColors.Control;

                ToolStripMenuItem tsmfilter =
                    new ToolStripMenuItem("搜素")
                    {
                        CheckOnClick = false,
                        Name = "filter"
                    };
                tsmfilter.Click += tsmfilter_Click;
                Menu.Items.Add(tsmfilter);

                ToolStripMenuItem tsmall = new ToolStripMenuItem(InfoType.All.GetDescription())
                {
                    CheckOnClick = false,
                    Name = Enum.GetName(typeof(InfoType), InfoType.All)
                };
                tsmall.Click += tsm_Click;
                Menu.Items.Add(tsmall);

                foreach (int v in Enum.GetValues(typeof(InfoType)))
                {
                    if (v == (int) InfoType.All || v == (int) InfoType.None) continue;
                    ToolStripMenuItem tsm =
                        new ToolStripMenuItem(((InfoType) v).GetDescription())
                        {
                            CheckOnClick = true,
                            Checked = true,
                            Name = Enum.GetName(typeof(InfoType), v)
                        };
                    tsm.Click += tsm_Click;
                    Menu.Items.Add(tsm);
                }

                _dgView.ContextMenuStrip = Menu;
                _dgView.CellPainting += DgvCellPainting;
                _dgView.CellClick += DgvSelectChanged;
            }
        }

        /// <summary>
        /// 接收报告计数
        /// </summary>
        public static int InCount;

        /// <summary>
        /// 发送成功计数
        /// </summary>
        public static int SendCount;

        /// <summary>
        /// 缓存剩余数
        /// </summary>
        public static int BufCount;

        /// <summary>
        /// 保留天数
        /// </summary>
        [Param("保留天数", "Log记录保留时间，超过时则删除，以节省存储空间", "日志")]
        public static int KeepDays = 30;

        /// <summary>
        /// 最近一次提取数据耗时(ms)
        /// </summary>
        public static int SelectTimeMs;

        /// <summary>
        /// 最近一次成功保存缓存数据耗时(ms)
        /// </summary>
        public static int SaveTimeMs;

        /// <summary>
        /// 数据更新时间戳
        /// </summary>
        public static int UpdateTimestamp;

        /// <summary>
        /// 显示缓存
        /// </summary>
        public static DataTable Table;

        /// <summary>
        /// 显示配置,请先配置好显示表格，再配置显示
        /// </summary>
        [Param("显示配置", "按位表示：错误0x01,警告0x02,信息0x04,调试0x08,操作0x10,参数0x20,产品0x40,状态0x80", "日志")]
        public static InfoType ShowCfg
        {
            get { return _showCfg; }
            set
            {
                foreach (ToolStripMenuItem item in Menu.Items)
                {
                    if (!item.CheckOnClick || item.Name == "All") continue;
                    foreach (int v in Enum.GetValues(typeof(InfoType)))
                    {
                        if (Enum.GetName(typeof(InfoType), v) == item.Name)
                        {
                            item.Checked = value.HasFlag((InfoType) v);
                            break;
                        }
                    }
                }
                _showCfg = value;
            }
        }
        private static InfoType _showCfg;

        /// <summary>
        /// 重复保存间隔
        /// </summary>
        [Param("重复保存间隔(ms)", "同样信息大于此间隔才会保存，避免刷屏，默认20000ms", "日志")]
        public static int IntvalSec = 20000;

        /// <summary>
        ///  SQL命令缓存数量1024
        /// </summary>
        [Param("命令缓存数", "SQL命令缓存数量,溢出则丢弃，默认1024", "日志")]
        public static int BufLen = 1024;

        /// <summary>
        /// 上次数据
        /// </summary>
        private static LogData _logDataTemp;

        /// <summary>
        /// 信息结构
        /// </summary>
        public struct LogData
        {
            /// <summary>
            /// 时间
            /// </summary>
            public DateTime Time;

            /// <summary>
            /// 类型
            /// </summary>
            public InfoType Type;

            /// <summary>
            /// 内容
            /// </summary>
            public string Text;

            /// <summary>
            /// 附加数据1
            /// </summary>
            public int Dat1;

            /// <summary>
            /// 附加数据2
            /// </summary>
            public int Dat2;
        }

        /// <summary>
        /// 信息类别
        /// </summary>
        [Flags]
        public enum InfoType
        {
            /// <summary>
            /// 禁用
            /// </summary>
            [Description("禁用")] None = 0,
            /// <summary>
            /// 全部
            /// </summary>
            [Description("全部")] All = 0xFFFF,
            /// <summary>
            /// 错误
            /// </summary>
            [Description("错误")] Error = 0x01,
            /// <summary>
            /// 警告
            /// </summary>
            [Description("警告")] Warning = 0x02,
            /// <summary>
            /// 信息
            /// </summary>
            [Description("信息")] Info = 0x04,
            /// <summary>
            /// 调试
            /// </summary>
            [Description("调试")] Debug = 0x08,
 
            /// <summary>
            /// 参数
            /// </summary>
            [Description("参数")] Param = 0x10,
            
            /// <summary>
            /// 工位1
            /// </summary>
            [Description("工位1")] ST1 = 0x20,
            /// <summary>
            /// 工位2
            /// </summary>
            [Description("工位2")] ST2 = 0x40,
            /// <summary>
            /// 工位3
            /// </summary>
            [Description("工位3")] ST3 = 0x80,
            /// <summary>
            /// 工位4
            /// </summary>
            [Description("工位4")] ST4 = 0x100,
            /// <summary>
            /// 工位5
            /// </summary>
            [Description("工位5")] ST5 = 0x200,
            /// <summary>
            /// 工位6
            /// </summary>
            [Description("工位6")] ST6 = 0x400,
            /// <summary>
            /// 工位7
            /// </summary>
            [Description("工位7")] ST7 = 0x800,
            /// <summary>
            /// 工位8
            /// </summary>
            [Description("工位8")] ST8 = 0x1000,
            /// <summary>
            /// 工位9
            /// </summary>
            [Description("工位9")] ST9 = 0x2000,
            /// <summary>
            /// 工位10
            /// </summary>
            [Description("工位10")] ST10 = 0x4000,

        };

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dgv">显示表格</param>
        /// <param name="showCfg">显示配置</param>
        /// <param name="savePath">保存路径，默认为运行目录下的log文件夹</param>
        /// <param name="dlyToSave">延迟保持ms</param>
        /// <param name="searchShowLine">搜索显示行数</param>
        /// <param name="keepDays">保持天数，自动删除</param>
        /// <param name="intvalSec">信息重复间隔，小于这个时间则丢弃</param>
        public static void Init(DataGridView dgv, InfoType showCfg, string savePath = "", int dlyToSave = 500, int searchShowLine = 500, int keepDays = 90, int intvalSec = 1000000)
        {
            DgViewer = dgv;
            ShowCfg = showCfg;
            DelayToSave = dlyToSave;
            SearchLineLimit = searchShowLine;
            KeepDays = keepDays;
            LogSavePath = savePath;
            DeleteLog();
        }
        #endregion

        #region Log数据处理

        /// <summary>
        /// 存数据库的
        /// </summary>
        private static readonly List<LogData> ListLog = new List<LogData>();

        /// <summary>
        /// 显示到DataView的
        /// </summary>
        private static readonly List<LogData> ListLogView = new List<LogData>();

        //缓存锁
        private static readonly object Lck = new object();

        //数据库锁
        private static readonly object DbLck = new object();

        //发送线程
        private static Task _tsk;
        //月的第几天
        private static int _day=-1;
        #region 写入操作
        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int Debug(string str, int sta = 0)
        {
            return Write(InfoType.Debug, str, sta);
        }
        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int Info(string str, int sta = 0)
        {
            return Write(InfoType.Info, str, sta);
        }
        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int Warning(string str, int sta = 0)
        {
            return Write(InfoType.Warning, str, sta);
        }
        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int Error(string str,int sta= 0)
        {
            return Write(InfoType.Error, str, sta);
        }     
        /// <summary>
        /// 参数变更
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int Param(string str, int sta = 0)
        {
            return Write(InfoType.Param, str, sta);
        }
       
        #endregion
        /// <summary>
        /// log写入缓存
        /// </summary>
        /// <param name="infoType">信息类型</param>
        /// <param name="info">信息内容</param>
        /// <param name="dat1">附件数据1</param>
        /// <param name="dat2">附件数据2</param>
        /// <returns>成功返回0，短时间内容重复-1，缓存满-2</returns>
        public static int Write(InfoType infoType, string info, int sta=0)
        {
            //if (sta == 1) infoType = InfoType.ST1;
            //else if (sta == 2) infoType = InfoType.ST2;
            //else if (sta == 3) infoType = InfoType.ST3;
            //else if (sta == 4) infoType = InfoType.ST4;
            //else if (sta == 5) infoType = InfoType.ST5;
            //else if (sta == 6) infoType = InfoType.ST6;
            //else if (sta == 7) infoType = InfoType.ST7;
            //else if (sta == 8) infoType = InfoType.ST8;
            //else if (sta == 9) infoType = InfoType.ST9;
            //else if (sta == 10) infoType = InfoType.ST10;
            int dat1 = sta;
            int dat2 = 0;
            int tick = Environment.TickCount;
            //一定时间内不能重复传同样内容
            if ((infoType == InfoType.Error || infoType == InfoType.Debug || infoType == InfoType.Info ||
                 infoType == InfoType.Warning) && _logDataTemp.Type == infoType &&
                Math.Abs(_logDataTemp.Time.Second - DateTime.Now.Second) < IntvalSec && _logDataTemp.Text == info)
            {

                return -1;
            }

            _logDataTemp.Time = DateTime.Now;
            _logDataTemp.Type = infoType;
            _logDataTemp.Text = info;

            //增加操作者
            if (infoType.HasFlag(InfoType.Param) /*|| infoType.HasFlag(InfoType.Operation)*/)
            {
                info = $"{info},{SysStatus.CurUser?.Name}";
            }

            //溢出
            int ret = 0;
            if (ListLog.Count >= BufLen)
            {
                
                //infoType = InfoType.Error;
                //info = "Log缓存溢出";
                dat1 = 0;
                dat2 = 0;
                ret = -2;
            }

            LogData log = new LogData
            {
                Type = infoType,
                Time = DateTime.Now,
                Text = info,
                Dat1 = dat1,
                Dat2 = dat2
            };

            //加入缓存
            lock (Lck)
            {
                if (ListLog.Count >= BufLen) ListLog.Clear();
                ListLog?.Add(log);
                InCount++;
                BufCount = BufLen - ListLog.Count;

                //显存---每次刷新，超过1024删掉最早的一条，，最大显示1024条信息
                if (ListLogView.Count >= BufLen) ListLogView.RemoveAt(0);
                ListLogView?.Add(log);

                //增加TXT文件保存 CH
                SaveLogInf($"{log.Time:hh:mm:ss}：{log.Type.GetDescription()}：{log.Text}",sta);
            }

            //未启写入任务则启动
            if (_tsk == null || _tsk.IsCompleted)
            {
                _tsk = new Task(xxxx);
                _tsk.Start();
            }

            SaveTimeMs = Environment.TickCount - tick;
            return ret;
        }

        //显示日志
        public static void xxxx()
        {
            ShowOnDgv(_dgView);
        }
        /// <summary>
        /// 发送数据到服务器，数据库
        /// </summary>
     

        /// <summary>
        /// 空间不足时，清除超出保留天数的Log文件
        /// </summary>
        /// <param name="keepDays">保留天数，默认用配置天数</param>
        /// <param name="freeSpaceLvl">保留空间(Gb)，小于此空间则自动删除</param>
        /// <returns>返回剩余空间(Gb)</returns>
        public static int DeleteLog(int keepDays = -1,int freeSpaceLvl = 10)
        {
            //计算剩余空间
            long freeSpace = 0;
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (_logSavePath.Contains(drive.Name))
                {
                    freeSpace = drive.TotalFreeSpace / (1024 * 1024 * 1024);
                    break;
                }
            }

            //文件夹不存在则创建
            if (!Directory.Exists(_logSavePath))
            {
                Directory.CreateDirectory(_logSavePath);
                return (int) freeSpace;
            }

            //清除文件
            lock (DbLck)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(_logSavePath);
                if (freeSpace >= freeSpaceLvl) return (int) freeSpace;
                foreach (FileInfo feInfo in dirInfo.GetFiles())
                {
                    if (feInfo.CreationTime < DateTime.Now.AddDays(-KeepDays))
                    {
                        Write(InfoType.Info, $"删除Log文件{feInfo.Name}");
                        feInfo.Delete();
                    }
                }
            }

            return (int) freeSpace;
        }

        #endregion

        #region 表格显示

        private static Form _from;
        private static string _strFileter = "";

        /// <summary>
        /// 显示委托定义
        /// </summary>
        /// <param name="displayer"></param>
        delegate void DisplayCallback(DataGridView displayer);

        /// <summary>
        /// 显示委托
        /// </summary>
        /// <param name="displayer"></param>
        public static void ShowOnDgv(DataGridView displayer)
        {
            

             if (_dgView == null || ListLogView.Count == 0) return;//|| Table.Rows.Count == 0

            int n = 0;
            while (displayer.IsHandleCreated == false)//获取一个值，该值指示控件是否有与它关联的句柄。如果已经为控件分配了句柄，则为 true；否则为 false。
            {
                //解决窗体关闭时出现“访问已释放句柄“的异常
                if (displayer.Disposing || displayer.IsDisposed)
                    return;
                Application.DoEvents();
                Thread.Sleep(10);
                if (n++ > 10) return;
            }

            //如果调用控件的线程和创建控件的线程不是同一个则为True
            if (displayer.InvokeRequired)// 获取一个值，该值指示调用方在对控件进行方法调用时是否必须调用 Invoke 方法，因为调用方位于创建控件所在的线程以外的线程中。
            {//  如果控件的 System.Windows.Forms.Control.Handle 是在与调用线程不同的线程上创建的（说明您必须通过 Invoke 方法对控件进行调用），则为
             //     true；否则为 false。
                DisplayCallback d = ShowOnDgv;
                displayer.BeginInvoke(d, new object[] {displayer});
            }
            else
            {
                if (Table == null)
                {
                    Table = new DataTable();
                    Table.Columns.Add("时间", typeof(string));
                    Table.Columns.Add("类型", typeof(string));
                    Table.Columns.Add("内容", typeof(string));
                }
                int cnt = ListLogView.Count;
                for (int i = 0; i < cnt; i++)
                {
                    if (ShowCfg.HasFlag(ListLogView[i].Type))
                    {
                        {
                            try
                            {
                                Table.Rows.Add(ListLogView[i].Time.ToString("HH:mm:ss.fff"),
                                    ListLogView[i].Type.GetDescription(),
                                    ListLogView[i].Text);
                                if (Table.Rows.Count > BufferLineLimit)
                                    Table.Rows.RemoveAt(0);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                throw;
                            }
                        }
                    }
                }
                lock (Lck)
                {
                    ListLogView.RemoveRange(0, cnt);
                }

                _dgView.DataSource = Table;
                if(DgViewer.RowCount>=1)
                {
                    _dgView.FirstDisplayedScrollingRowIndex = DgViewer.RowCount - 1;//出错
                }
                else
                {
                    //_dgView.FirstDisplayedScrollingRowIndex =0;//出错
                }
               
                _dgView.ClearSelection();
            }
        }

        /// <summary>
        /// 根据内容变底色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DgvCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var txt = _dgView.Rows[e.RowIndex].Cells[1]?.Value?.ToString();
                 switch (txt)
                {
                    case "错误":
                        _dgView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Pink;
                        break;
                    case "警告":
                        _dgView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Wheat;
                        break;
                    case "状态":
                        _dgView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCyan;
                        break;
                }

            }
        }

        #endregion

        #region 选择与统计

        /// <summary>
        /// 提取符合条件的数据
        /// </summary>
        /// <param name="dateTime">起始时间</param>
        /// <param name="minutesRang">提取份范围(前后分钟数)</param>
        /// <param name="typeCfg">信息类型</param>
        /// <param name="keyWord">关键词</param>
        /// <param name="limit">返回条数</param>
        /// <param name="offset">偏移条数</param>
        /// <returns></returns>
     
        #endregion

        #region 搜索   有问题不要用

        /// <summary>
        /// 菜单---搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void tsmfilter_Click(object sender, EventArgs e)
        {
            if (_from != null) return;

            FlowLayoutPanel flp1 = new FlowLayoutPanel {Dock = DockStyle.Fill};

            Label lb1 = new Label
            {
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Dock = DockStyle.None,
                Text = @"起  始:"
            };
            flp1.Controls.Add(lb1);

            DateTimePicker dtpFrom = new DateTimePicker
            {
                Name = "dtpFrom",
                Anchor = AnchorStyles.Left,
                Dock = DockStyle.None,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = @"yyyy-MM-dd HH:mm",
                ShowUpDown = true,
                Width = 200,
                Height = 34,
                Value = Convert.ToDateTime(DateTime.Now.Hour>8&& DateTime.Now.Hour < 20 ? DateTime.Now.ToString("yyyy-MM-dd 08:00:00"): DateTime.Now.ToString("yyyy-MM-dd 20:00:00"))
            };
            flp1.Controls.Add(dtpFrom);

            FlowLayoutPanel flp2 = new FlowLayoutPanel {Dock = DockStyle.Fill};

            Label lb2 = new Label
            {
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Dock = DockStyle.None,
                Text = @"结  束:"
            };
            flp2.Controls.Add(lb2);


            DateTimePicker dtpTo = new DateTimePicker
            {
                Name = "dtpTo",
                Anchor = AnchorStyles.Left,
                Dock = DockStyle.None,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = @"yyyy-MM-dd HH:mm",
                ShowUpDown = true,
                Width = 200,
                Height = 34
            };
            flp2.Controls.Add(dtpTo);

            FlowLayoutPanel flp3 = new FlowLayoutPanel {Dock = DockStyle.Fill};

            Label lbKeyword = new Label
            {
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Dock = DockStyle.None,
                Text = @"关键词:"
            };
            flp3.Controls.Add(lbKeyword);

            TextBox tbKeyword = new TextBox
            {
                Name = "tbKeyword",
                Anchor = AnchorStyles.Left,
                Dock = DockStyle.None,
                Width = 200
            };
            flp3.Controls.Add(tbKeyword);

            FlowLayoutPanel flp4 = new FlowLayoutPanel
            {
                Margin = new Padding(9, 3, 3, 3),
                RightToLeft = RightToLeft.No,
                Dock = DockStyle.Fill
            };

            Button btnFirst = new Button
            {
                Name = "btnFirst",
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Dock = DockStyle.None,
                Width = 40,
                Height = 30,
                Text = @"|<"
            };
            btnFirst.Click += btnNvigation_Click;
            flp4.Controls.Add(btnFirst);

            Button btnPre = new Button
            {
                Name = "btnPre",
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Dock = DockStyle.None,
                Width = 40,
                Height = 30,
                Text = @"<<"
            };
            btnPre.Click += btnNvigation_Click;
            flp4.Controls.Add(btnPre);

            Label lbNvigation = new Label
            {
                Text = @"0000/0000",
                Name = "lbNvigation",
                Tag = -1,
                AutoSize = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom,
                Dock = DockStyle.None,
                TextAlign = ContentAlignment.MiddleCenter,
                Width = 9 * 11,
            };
            flp4.Controls.Add(lbNvigation);

            Button btnNext = new Button
            {
                Name = "btnNext",
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Dock = DockStyle.None,
                Width = 40,
                Height = 30,
                Text = @">>"
            };
            btnNext.Click += btnNvigation_Click;
            flp4.Controls.Add(btnNext);

            Button btnLast = new Button
            {
                Name = "btnLast",
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Dock = DockStyle.None,
                Width = 40,
                Height = 30,
                Text = @">|"
            };
            btnLast.Click += btnNvigation_Click;
            flp4.Controls.Add(btnLast);

            FlowLayoutPanel flp5 = new FlowLayoutPanel
            {
                RightToLeft = RightToLeft.Yes,
                Dock = DockStyle.Fill
            };

            Button btnOk = new Button
            {
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Dock = DockStyle.None,
                Width = 100,
                Height = 40,
                Text = @"搜索"
            };
            btnOk.Click += btnOk_Click;
            flp5.Controls.Add(btnOk);

            Button btnCancel = new Button
            {
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Dock = DockStyle.None,
                Width = 80,
                Height = 40,
                Text = @"关闭"
            };
            btnCancel.Click += btnCancel_Click;
            flp5.Controls.Add(btnCancel);

            Button btnExport = new Button
            {
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Dock = DockStyle.None,
                Width = 90,
                Height = 40,
                Text = @"导出"
            };
            btnExport.Click += btnExport_Click;
            flp5.Controls.Add(btnExport);

            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Font = new Font(tlp.Font.FontFamily, 14);
            tlp.Dock = DockStyle.Fill;
            tlp.ColumnCount = 1;
            tlp.RowCount = 5;
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            tlp.Controls.Add(flp1);
            tlp.SetRow(flp1, 0);
            tlp.SetColumn(flp1, 0);

            tlp.Controls.Add(flp2);
            tlp.SetRow(flp2, 1);
            tlp.SetColumn(flp2, 0);

            tlp.Controls.Add(flp3);
            tlp.SetRow(flp3, 2);
            tlp.SetColumn(flp3, 0);

            tlp.Controls.Add(flp4);
            tlp.SetRow(flp4, 3);
            tlp.SetColumn(flp4, 0);

            tlp.Controls.Add(flp5);
            tlp.SetRow(flp5, 4);
            tlp.SetColumn(flp5, 0);

            _from = new Form
            {
                Name = "LogSearch",
                FormBorderStyle = FormBorderStyle.FixedSingle,
                Width = lb1.Width + dtpTo.Width + 40,
                Height = (dtpTo.Height + 30) * 5,
                Text = @"Log搜索",
                ShowIcon = false,
                MaximizeBox = false,
                MinimizeBox = false,
                TopMost = true,
                StartPosition = FormStartPosition.Manual,
                Location = Control.MousePosition
            };
            _from.Closed += FromClose;
            _from.Tag = 0;
            _from.Controls.Add(tlp);
            _from.Name = "";
            //_from.ShowDialog();
            _from.Show();
            tbKeyword.Focus();
        }

        /// <summary>
        /// 搜索导航
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void btnNvigation_Click(object sender, EventArgs e)
        {
            var f = ((Button) sender).FindForm();
            if (f == null) return;
            switch (((Button) sender).Name)
            {
                case "btnPre":
                    f.Tag = (int) f.Tag - SearchLineLimit;
                    if ((int) f.Tag < 0) f.Tag = 0;
                    break;
                case "btnNext":
                    f.Tag = (int) f.Tag + SearchLineLimit;
                    break;
                case "btnFirst":
                    f.Tag = 0;
                    break;
                case "btnLast":
                    f.Tag = -1;
                    break;
            }

            btnOk_Click(sender, e);
        }

        /// <summary>
        /// 搜搜
        /// </summary>
        /// <param name="dtFrom">起始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <param name="rouCnt">返回行数</param>
        /// <param name="offset">偏移行数</param>
        /// <param name="keyword">关键词</param>
        /// <param name="condition">SQL条件</param>
        /// <param name="limit">截取行数</param>
        /// <returns>搜索结果表格</returns>
      /*  public static DataTable Select(DateTime dtFrom, DateTime dtEnd, ref int rouCnt, ref int offset, string keyword,
            string condition = "true", int limit = 500)
        {
            if ((dtEnd - dtFrom).Days > 7)
            {
                MessageBox.Show(@"时间跨度不能超一个星期", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return new DataTable();
            }

            using (SQLiteConnection conn =
                new SQLiteConnection($@"data source={_logSavePath}{DateTime.Now:yyyy-MM-dd}.db"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    if(conn.State == ConnectionState.Closed)return new DataTable();
                    var sh = new SqLiteHelper(cmd);

                    //跨天
                    var strTable = "";
                    for (DateTime dateTemp = dtFrom; dateTemp <= dtEnd; dateTemp = dateTemp.AddDays(1))
                    {
                        //文件是否存在
                        var filename = $"{_logSavePath}{dateTemp:yyyy-MM-dd}.db";
                        if (!File.Exists(filename)) continue;
                        //表格是否存在
                        sh.AttachDatabase(filename, dateTemp.ToString("yyyy_MM_dd"));
                        var dt = sh.Select($"select* from `{dateTemp:yyyy_MM_dd}`.sqlite_master where type = 'table' and name = '{dateTemp:TyyyyMMdd}'");
                        if (dt?.Rows.Count > 0)
                        {
                            if (strTable != "") strTable += " UNION ALL ";
                            strTable += $"select * from 'T{dateTemp:yyyyMMdd}'";
                        }
                    }

                    //查询
                    if (strTable.Length > 0)
                    {
                        string select =
                            $"select count(*) from({strTable}) where time between '{dtFrom:yyyy-MM-dd HH:mm:ss}' and '{dtEnd:yyyy-MM-dd HH:mm:ss}' and info like '%{keyword}%' and {condition}";
                        var dt = sh.Select(select);
                        if (dt?.Rows.Count > 0)
                            rouCnt = Convert.ToInt32(dt.Rows[0][0].ToString());

                        if (offset == -1 || offset > rouCnt) offset = rouCnt - rouCnt % limit;
                        if (offset < 0) offset = 0;
                        select =
                            $"select time as 时间,type 类型,info 内容 from ({strTable}) where time between '{dtFrom:yyyy-MM-dd HH:mm:ss}' and '{dtEnd:yyyy-MM-dd HH:mm:ss}' and info like '%{keyword}%' and {condition} limit {limit} offset {offset}";
                        dt = sh.Select(select);
                        return dt;
                    }
                }
            }

            return new DataTable();
        }*/

        /// <summary>
        /// 关闭搜索框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void btnCancel_Click(object sender, EventArgs e)
        {
            _from?.Close();
            _from = null;
            Table.DefaultView.RowFilter = _strFileter;
        }

        /// <summary>
        /// 关闭搜索框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void FromClose(object sender, EventArgs e)
        {
            _from = null;
            Table.DefaultView.RowFilter = _strFileter;
        }

        /// <summary>
        /// 搜索按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void btnOk_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 导出搜索结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void btnExport_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 导出到CSV文件
        /// </summary>
        /// <param name="table"></param>
        /// <param name="file"></param>
        public static void DataTableToCsv(DataTable table, string file)
        {
            string title = "";
            FileStream fs = new FileStream(file, FileMode.Create);
            StreamWriter sw = new StreamWriter(new BufferedStream(fs), System.Text.Encoding.Default);
            for (int i = 0; i < table.Columns.Count; i++)
            {
                title += table.Columns[i].ColumnName + ",";
            }

            title = title.Substring(0, title.Length - 1) + "\n";
            sw.Write(title);
            foreach (DataRow row in table.Rows)
            {
                string line = "";
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    line += row[i] + ",";
                }

                line = line.Substring(0, line.Length - 1) + "\n";
                sw.Write(line);
            }

            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// 选择行指示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DgvSelectChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_from != null)
            {
                var ctl = _from.Controls.Find("lbNvigation", true);
                if (ctl.Length > 0)
                {
                    if ((int) ctl[0].Tag > 0)
                    {
                        ctl[0].Text = $@"{(int) _from.Tag + e.RowIndex + 1}/{(int) ctl[0].Tag}";
                    }
                    else
                    {
                        ctl[0].Text = $@"{(int) _from.Tag + e.RowIndex + 1}/{_dgView?.RowCount}";
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 菜单---显示配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void tsm_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem) sender).Name == "All")
            {
                ((ToolStripMenuItem) sender).Checked = !((ToolStripMenuItem) sender).Checked;
                foreach (ToolStripMenuItem item in Menu.Items)
                {
                    if (item.CheckOnClick)
                    {
                        item.Checked = ((ToolStripMenuItem) sender).Checked;
                    }
                }
            }

            _strFileter = "";
            foreach (ToolStripMenuItem item in Menu.Items)
            {
                if (!item.CheckOnClick) continue;//没选中时下一个
                foreach (int v in Enum.GetValues(typeof(InfoType)))
                {
                    if (Enum.GetName(typeof(InfoType), v) == item.Name)
                    {
                        if (item.Checked)
                        {
                            _showCfg = (InfoType) ((int) _showCfg | v);
                            if (_strFileter.Length == 0)
                            {
                                _strFileter += $"类型 = '{((InfoType) v).GetDescription()}'";
                            }
                            else
                            {
                                _strFileter += $" or 类型 = '{((InfoType) v).GetDescription()}'";
                            }
                        }
                        else
                        {
                            _showCfg = (InfoType) ((int) _showCfg & (~v));// 对应的位=0
                            var it= Menu.Items.Find(InfoType.All.ToString(),false);
                            if (it[0] != null)
                            {
                                ((ToolStripMenuItem)it[0]).Checked = false;
                            }
                        }

                        break;
                    }
                }
            }

            //无选择项目时，保持错误项目显示
            if (_strFileter == "")
            {
                _strFileter = $"类型 = '{InfoType.Error.GetDescription()}'";
                ToolStripItem[] it = Menu.Items.Find(InfoType.Error.ToString(), false);
                if (it.Length > 0) ((ToolStripMenuItem)it[0]).Checked = true;
                _showCfg = InfoType.Error;
            }

            if(_dgView?.DataSource!=null)
                ((DataTable)_dgView.DataSource).DefaultView.RowFilter = _strFileter;
            //Table.DefaultView.RowFilter = _strFileter;
        }
        /// <summary>
        /// 保持信息到log文件夹里面
        /// </summary>
        /// <param name="data"></param>
        private static void SaveLogInf(string data,int sta=0)
        {
            try
            {
                string path = $@"D:\log\logtxt\{sta.ToString()}\";
                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }
                StringBuilder DataColumn = new StringBuilder();
                StringBuilder DataLine = new StringBuilder();

                //列标题
                DataColumn.Append("时间,类型,信息");
                //行数据
                DataLine.Append(data);
                 
                string FilePath =  $"D:\\log\\logtxt\\{sta.ToString()}\\{DateTime.Now:yyyy-MM-dd}.txt";

                if (File.Exists(FilePath) == false)
                {
                    StreamWriter stream = new StreamWriter(FilePath, false, Encoding.UTF8);
                    stream.WriteLine(DataColumn);
                    stream.WriteLine(DataLine);
                    stream.Flush();
                    stream.Close();
                    stream.Dispose();
                }
                else
                {
                    StreamWriter stream = new StreamWriter(FilePath, true, Encoding.UTF8);
                    stream.WriteLine(DataLine);
                    stream.Flush();
                    stream.Close();
                    stream.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"保存log txt文档异常【{ex}】");
            }
        }
    }
}
