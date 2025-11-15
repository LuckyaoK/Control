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

namespace CXPro001.controls
{
    public partial class hardwarContr : UserControl
    {
        public hardwarContr()
        {
            InitializeComponent();
        }
        PLC_jienshi plckey;
        s7com s71200;
        test7631 TS7631;
        Hanslaser hanslase;
        visioncam visionc;
        barcode_delijie delijie;
        barcode_jienshi jienshi;
        test7631 ts9803;
        /// <summary>
        /// 初始化，增加硬件，设备不同要跟着变换
        /// </summary>
        public void init()
        {
          //  if (plckey == null)   plckey = hardware.PLC_KEY;
           // if (s71200 == null) s71200 = hardware.s7_1200;
           // if (TS7631 == null) TS7631 = hardware.Tes7631;
            if (hanslase == null) hanslase = hardware.hanslaser1;
            if (visionc == null) visionc = hardware.viscam;
            if (delijie == null) delijie = hardware.delijie1;
            if (jienshi == null) jienshi = hardware.jienshi1;
          //  if (ts9803 == null) ts9803 = hardware.test9803;
            if (dgv.Rows.Count == 0)
            {
                dgv.Rows.Add("1", plckey.mdescrible, plckey.MIP, plckey.Mport.ToString(), plckey.IsConnected ? "连接就绪" : "未连接");
                dgv.Rows.Add("2", s71200.mdescrible, s71200.MIP, s71200.Mport.ToString(), s71200.IsConnected ? "连接就绪" : "未连接");
                dgv.Rows.Add("3", TS7631.mdescrible, TS7631.MyPortName, TS7631.MyBaudRate.ToString(), TS7631.IsConnected ? "连接就绪" : "未连接");
                dgv.Rows.Add("4", hanslase.mdescrible, hanslase.MyPortName, hanslase.MyBaudRate.ToString(), plckey.IsConnected ? "连接就绪" : "未连接");
                dgv.Rows.Add("5", visionc.mdescrible, visionc.MIP, visionc.Mport.ToString(), visionc.IsConnected ? "连接就绪" : "未连接");
                dgv.Rows.Add("6", delijie.mdescrible, delijie.MIP, delijie.Mport.ToString(), delijie.IsConnected ? "连接就绪" : "未连接");
                dgv.Rows.Add("7", jienshi.mdescrible, jienshi.MIP, jienshi.Mport.ToString(), jienshi.IsConnected ? "连接就绪" : "未连接");
                dgv.Rows.Add("8", ts9803.mdescrible, ts9803.MyPortName, ts9803.MyBaudRate.ToString(), ts9803.IsConnected ? "连接就绪" : "未连接");
            }
            else
            {
                dgv.Rows[0].Cells[0].Value = 1;
                dgv.Rows[0].Cells[1].Value = plckey.mdescrible;
                dgv.Rows[0].Cells[2].Value = plckey.MIP;
                dgv.Rows[0].Cells[3].Value = plckey.Mport.ToString();
                dgv.Rows[0].Cells[4].Value = plckey.IsConnected ? "连接就绪" : "未连接";
                dgv.Rows[1].Cells[0].Value = 2;
                dgv.Rows[1].Cells[1].Value = s71200.mdescrible;
                dgv.Rows[1].Cells[2].Value = s71200.MIP;
                dgv.Rows[1].Cells[3].Value = s71200.Mport.ToString();
                dgv.Rows[1].Cells[4].Value = s71200.IsConnected ? "连接就绪" : "未连接";
                dgv.Rows[2].Cells[0].Value = 3;
                dgv.Rows[2].Cells[1].Value = TS7631.mdescrible;
                dgv.Rows[2].Cells[2].Value = TS7631.MyPortName;
                dgv.Rows[2].Cells[3].Value = TS7631.MyBaudRate.ToString();
                dgv.Rows[2].Cells[4].Value = TS7631.IsConnected ? "连接就绪" : "未连接";
                dgv.Rows[3].Cells[0].Value = 4;
                dgv.Rows[3].Cells[1].Value = hanslase.mdescrible;
                dgv.Rows[3].Cells[2].Value = hanslase.MyPortName;
                dgv.Rows[3].Cells[3].Value = hanslase.MyBaudRate.ToString();
                dgv.Rows[3].Cells[4].Value = hanslase.IsConnected ? "连接就绪" : "未连接";
                dgv.Rows[4].Cells[0].Value = 5;
                dgv.Rows[4].Cells[1].Value = visionc.mdescrible;
                dgv.Rows[4].Cells[2].Value = visionc.MIP;
                dgv.Rows[4].Cells[3].Value = visionc.Mport.ToString();
                dgv.Rows[4].Cells[4].Value = visionc.IsConnected ? "连接就绪" : "未连接";
                dgv.Rows[5].Cells[0].Value = 6;
                dgv.Rows[5].Cells[1].Value = delijie.mdescrible;
                dgv.Rows[5].Cells[2].Value = delijie.MIP;
                dgv.Rows[5].Cells[3].Value = delijie.Mport.ToString();
                dgv.Rows[5].Cells[4].Value = delijie.IsConnected ? "连接就绪" : "未连接";
                dgv.Rows[6].Cells[0].Value = 7;
                dgv.Rows[6].Cells[1].Value = jienshi.mdescrible;
                dgv.Rows[6].Cells[2].Value = jienshi.MIP;
                dgv.Rows[6].Cells[3].Value = jienshi.Mport.ToString();
                dgv.Rows[6].Cells[4].Value = jienshi.IsConnected ? "连接就绪" : "未连接";
                dgv.Rows[7].Cells[0].Value = 8;
                dgv.Rows[7].Cells[1].Value = ts9803.mdescrible;
                dgv.Rows[7].Cells[2].Value = ts9803.MyPortName;
                dgv.Rows[7].Cells[3].Value = ts9803.MyBaudRate.ToString();
                dgv.Rows[7].Cells[4].Value = ts9803.IsConnected ? "连接就绪" : "未连接";


            }
            timer1.Enabled = true;
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv.Enabled = false;
            if (dgv.Columns[e.ColumnIndex].HeaderText.Equals("连接"))
            {
                if (e.RowIndex == 0)//基恩士PLC
                {
                    if (plckey.connect() == EmRes.Succeed) MessageBox.Show($"{plckey.mdescrible}连接成功！");
                    else MessageBox.Show($"{plckey.mdescrible}连接失败！");
                }
                if (e.RowIndex == 1) //西门子S7-1200
                {
                    if (s71200.connect() == EmRes.Succeed) MessageBox.Show($"{s71200.mdescrible}连接成功！");
                    else MessageBox.Show($"{s71200.mdescrible}连接失败！");
                }
                if (e.RowIndex == 2) //7631耐压仪
                {
                    if (TS7631.connect() == EmRes.Succeed) MessageBox.Show($"{TS7631.mdescrible}连接成功！");
                    else MessageBox.Show($"{TS7631.mdescrible}连接失败！");
                }
                if (e.RowIndex == 3) //激光刻字机
                {
                    if (hanslase.connect() == EmRes.Succeed) MessageBox.Show($"{hanslase.mdescrible}连接成功！");
                    else MessageBox.Show($"{hanslase.mdescrible}连接失败！");
                }
                if (e.RowIndex == 4) //视觉主机
                {
                    if (visionc.connect() == EmRes.Succeed) MessageBox.Show($"{visionc.mdescrible}连接成功！");
                    else MessageBox.Show($"{visionc.mdescrible}连接失败！");
                }
                if (e.RowIndex == 5) //得利捷扫码枪
                {
                    if (delijie.connect() == EmRes.Succeed) MessageBox.Show($"{delijie.mdescrible}连接成功！");
                    else MessageBox.Show($"{delijie.mdescrible}连接失败！");
                }
                if (e.RowIndex == 6) //基恩士扫码枪
                {
                    if (jienshi.connect() == EmRes.Succeed) MessageBox.Show($"{jienshi.mdescrible}连接成功！");
                    else MessageBox.Show($"{jienshi.mdescrible}连接失败！");
                }


            }
            else if(dgv.Columns[e.ColumnIndex].HeaderText.Equals("断开连接"))
            {
                if (e.RowIndex == 0)//基恩士PLC
                {
                    plckey.DisConnet();
                }
                if (e.RowIndex == 1) //西门子S7-1200
                {
                    s71200.DisConnet();
                    
                }
                if (e.RowIndex == 2) //7631耐压仪
                {
                    TS7631.DisConnet();
                    
                }
                if (e.RowIndex == 3) //激光刻字机
                {
                    hanslase.DisConnet();
             
                }
                if (e.RowIndex == 4) //视觉主机
                {
                    visionc.DisConnet();
                }
                if (e.RowIndex == 5) //得利捷扫码枪
                {
                    delijie.DisConnet();
                    
                }
                if (e.RowIndex == 6) //得利捷扫码枪
                {
                    jienshi.DisConnet();

                }





            }
            dgv.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(SysStatus.Status!= SysStatus.EmSysSta.Run)
            {
                init();
            }

        }
    }
}
