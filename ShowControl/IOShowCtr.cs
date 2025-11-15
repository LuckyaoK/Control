using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CXPro001.myclass;
namespace CXPro001.ShowControl
{
    public partial class IOShowCtr : UserControl
    {
        public IOShowCtr()
        {
            InitializeComponent();
        }

        private void TM_SYS_Tick(object sender, EventArgs e)
        {
            #region P1
            ///p1状态
            if (hardware.my_io.m_output[0,8])            
                P1_BTN_b1.BackColor = Color.Red;
            else  
                P1_BTN_b1.BackColor = SystemColors.Control;
           
            if (hardware.my_io.m_output[0, 9])            
                P1_BTN_b2.BackColor = Color.Red;            
            else            
                P1_BTN_b2.BackColor = SystemColors.Control;
            

            if (hardware.my_io.m_output[0, 10])            
                P1_BTN_b3.BackColor = Color.Red;            
            else            
                P1_BTN_b3.BackColor = SystemColors.Control;           

            if (hardware.my_io.m_output[0,11])            
                P1_BTN_b4.BackColor = Color.Red;            
            else            
                P1_BTN_b4.BackColor = SystemColors.Control;            

            if (hardware.my_io.m_output[0, 12])            
                P1_BTN_b5.BackColor = Color.Red;            
            else            
                P1_BTN_b5.BackColor = SystemColors.Control;         

            if (hardware.my_io.m_output[0, 13])
                 P1_BTN_b6.BackColor = Color.Red;           
            else            
                P1_BTN_b6.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[0, 14])
                P1_BTN_b7.BackColor = Color.Red;
            else
                P1_BTN_b7.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[0, 15])
                P1_BTN_b8.BackColor = Color.Red;
            else
                P1_BTN_b8.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[0, 8])
                P1_LBL_l1.BackColor = Color.Red;
            else
                P1_LBL_l1.BackColor = SystemColors.Control;
            if (hardware.my_io.m_input[0, 9])
                P1_LBL_l2.BackColor = Color.Red;
            else
                P1_LBL_l2.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[0, 10])
                P1_LBL_l3.BackColor = Color.Red;
            else
                P1_LBL_l3.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[0, 11])
                P1_LBL_l4.BackColor = Color.Red;
            else
                P1_LBL_l4.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[0, 12])
                P1_LBL_l5.BackColor = Color.Red;
            else
                P1_LBL_l5.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[0, 13])
                P1_LBL_l6.BackColor = Color.Red;
            else
                P1_LBL_l6.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[0, 14])
                P1_LBL_l7.BackColor = Color.Red;
            else
                P1_LBL_l7.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[0, 15])
                P1_LBL_l8.BackColor = Color.Red;
            else
                P1_LBL_l8.BackColor = SystemColors.Control;
            ////
            #endregion

            #region P2
            if (hardware.my_io.m_output[1, 0])
                P2_BTN_b1.BackColor = Color.Red;
            else
                P2_BTN_b1.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[1, 1])
                P2_BTN_b2.BackColor = Color.Red;
            else
                P2_BTN_b2.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[1, 1])
                P2_LBL_l1.BackColor = Color.Red;
            else
                P2_LBL_l1.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[1, 0])
                P2_LBL_l2.BackColor = Color.Red;
            else
                P2_LBL_l2.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[1, 2])
                P2_LBL_l3.BackColor = Color.Red;
            else
                P2_LBL_l3.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[1, 3])
                P2_LBL_l4.BackColor = Color.Red;
            else
                P2_LBL_l4.BackColor = SystemColors.Control;
            #endregion

            #region P3
            if (hardware.my_io.m_output[1, 6])
                P3_BTN_b1.BackColor = Color.Red;
            else
                P3_BTN_b1.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[1, 7])
                P3_BTN_b2.BackColor = Color.Red;
            else
                P3_BTN_b2.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[1, 8])
                P3_BTN_b3.BackColor = Color.Red;
            else
                P3_BTN_b3.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[1, 9])
                P3_BTN_b4.BackColor = Color.Red;
            else
                P3_BTN_b4.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[1, 8])
                P3_LBL_l1.BackColor = Color.Red;
            else
                P3_LBL_l1.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[1, 9])
                P3_LBL_l2.BackColor = Color.Red;
            else
                P3_LBL_l2.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[1, 10])
                P3_LBL_l3.BackColor = Color.Red;
            else
                P3_LBL_l3.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[1, 11])
                P3_LBL_l4.BackColor = Color.Red;
            else
                P3_LBL_l4.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[4, 8])
                P3_LBL_l5.BackColor = Color.Red;
            else
                P3_LBL_l5.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[4, 9])
                P3_LBL_l6.BackColor = Color.Red;
            else
                P3_LBL_l6.BackColor = SystemColors.Control;
            #endregion

            #region P4
            if (hardware.my_io.m_output[1, 10])
                P4_BTN_b1.BackColor = Color.Red;
            else
                P4_BTN_b1.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[1, 11])
                P4_BTN_b2.BackColor = Color.Red;
            else
                P4_BTN_b2.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[1, 12])
                P4_BTN_b3.BackColor = Color.Red;
            else
                P4_BTN_b3.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[1, 13])
                P4_BTN_b4.BackColor = Color.Red;
            else
                P4_BTN_b4.BackColor = SystemColors.Control;


            if (hardware.my_io.m_input[1, 12])
                P4_LBL_l1.BackColor = Color.Red;
            else
                P4_LBL_l1.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[1, 13])
                P4_LBL_l2.BackColor = Color.Red;
            else
                P4_LBL_l2.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[1, 14])
                P4_LBL_l3.BackColor = Color.Red;
            else
                P4_LBL_l3.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[1, 15])
                P4_LBL_l4.BackColor = Color.Red;
            else
                P4_LBL_l4.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[4, 10])
                P4_LBL_l5.BackColor = Color.Red;
            else
                P4_LBL_l5.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[4, 11])
                P4_LBL_l6.BackColor = Color.Red;
            else
                P4_LBL_l6.BackColor = SystemColors.Control;
            #endregion

            #region P5
            if (hardware.my_io.m_output[1, 14])
                P5_BTN_b1.BackColor = Color.Red;
            else
                P5_BTN_b1.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[1, 15])
                P5_BTN_b2.BackColor = Color.Red;
            else
                P5_BTN_b2.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[2, 0])
                P5_BTN_b3.BackColor = Color.Red;
            else
                P5_BTN_b3.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[2, 1])
                P5_BTN_b4.BackColor = Color.Red;
            else
                P5_BTN_b4.BackColor = SystemColors.Control;


            if (hardware.my_io.m_input[2, 0])
                P5_LBL_l1.BackColor = Color.Red;
            else
                P5_LBL_l1.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[2, 1])
                P5_LBL_l2.BackColor = Color.Red;
            else
                P5_LBL_l2.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[2, 2])
                P5_LBL_l3.BackColor = Color.Red;
            else
                P5_LBL_l3.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[2, 3])
                P5_LBL_l4.BackColor = Color.Red;
            else
                P5_LBL_l4.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[4, 12])
                P5_LBL_l5.BackColor = Color.Red;
            else
                P5_LBL_l5.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[4, 13])
                P5_LBL_l6.BackColor = Color.Red;
            else
                P5_LBL_l6.BackColor = SystemColors.Control;
            #endregion

            #region P6
            

            if (hardware.my_io.m_input[4, 14])
                P6_LBL_l1.BackColor = Color.Red;
            else
                P6_LBL_l1.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[4, 15])
                P6_LBL_l2.BackColor = Color.Red;
            else
                P6_LBL_l2.BackColor = SystemColors.Control;
            #endregion

            #region P7
            if (hardware.my_io.m_output[2, 2])
                P7_BTN_b1.BackColor = Color.Red;
            else
                P7_BTN_b1.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[2, 3])
                P7_BTN_b2.BackColor = Color.Red;
            else
                P7_BTN_b2.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[2, 4])
                P7_BTN_b3.BackColor = Color.Red;
            else
                P7_BTN_b3.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[2, 5])
                P7_BTN_b4.BackColor = Color.Red;
            else
                P7_BTN_b4.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[2,6])
                P7_BTN_b5.BackColor = Color.Red;
            else
                P7_BTN_b5.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[2, 7])
                P7_BTN_b6.BackColor = Color.Red;
            else
                P7_BTN_b6.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[2, 13])
                P7_LBL_l1.BackColor = Color.Red;
            else
                P7_LBL_l1.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[2, 14])
                P7_LBL_l2.BackColor = Color.Red;
            else
                P7_LBL_l2.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[2, 9])
                P7_LBL_l3.BackColor = Color.Red;
            else
                P7_LBL_l3.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[2, 10])
                P7_LBL_l4.BackColor = Color.Red;
            else
                P7_LBL_l4.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[2, 11])
                P7_LBL_l5.BackColor = Color.Red;
            else
                P7_LBL_l5.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[2, 12])
                P7_LBL_l6.BackColor = Color.Red;
            else
                P7_LBL_l6.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[4, 16])
                P7_LBL_l7.BackColor = Color.Red;
            else
                P7_LBL_l7.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[4, 17])
                P7_LBL_l8.BackColor = Color.Red;
            else
                P7_LBL_l8.BackColor = SystemColors.Control;
            #endregion

            #region P8
            if (hardware.my_io.m_output[1, 2])
                P8_BTN_b1.BackColor = Color.Red;
            else
                P8_BTN_b1.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[1, 3])
                P8_BTN_b2.BackColor = Color.Red;
            else
                P8_BTN_b2.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[1, 4])
                P8_BTN_b3.BackColor = Color.Red;
            else
                P8_BTN_b3.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[1, 5])
                P8_BTN_b4.BackColor = Color.Red;
            else
                P8_BTN_b4.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[3, 0])
                P8_BTN_b5.BackColor = Color.Red;
            else
                P8_BTN_b5.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[3, 1])
                P8_BTN_b6.BackColor = Color.Red;
            else
                P8_BTN_b6.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[3, 2])
                P8_BTN_b7.BackColor = Color.Red;
            else
                P8_BTN_b7.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[3, 3])
                P8_BTN_b8.BackColor = Color.Red;
            else
                P8_BTN_b8.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[3, 4])
                P8_BTN_b9.BackColor = Color.Red;
            else
                P8_BTN_b9.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[3, 5])
                P8_BTN_b10.BackColor = Color.Red;
            else
                P8_BTN_b10.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[3, 6])
                P8_BTN_b11.BackColor = Color.Red;
            else
                P8_BTN_b11.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[3, 7])
                P8_BTN_b12.BackColor = Color.Red;
            else
                P8_BTN_b12.BackColor = SystemColors.Control;

           /*if (hardware.my_io.m_output[3, 8])
                P8_BTN_b13.BackColor = Color.Red;
            else
                P8_BTN_b13.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[3, 9])
                P8_BTN_b14.BackColor = Color.Red;
            else
                P8_BTN_b14.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[3, 10])
                P8_BTN_b15.BackColor = Color.Red;
            else
                P8_BTN_b15.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[3, 11])
                P8_BTN_b16.BackColor = Color.Red;
            else
                P8_BTN_b16.BackColor = SystemColors.Control;
            /////
           */
            if (hardware.my_io.m_input[1, 4])
                P8_LBL_l1.BackColor = Color.Red;
            else
                P8_LBL_l1.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[1, 5])
                P8_LBL_l2.BackColor = Color.Red;
            else
                P8_LBL_l2.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[1, 6])
                P8_LBL_l3.BackColor = Color.Red;
            else
                P8_LBL_l3.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[1, 7])
                P8_LBL_l4.BackColor = Color.Red;
            else
                P8_LBL_l4.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[3,8])
                P8_LBL_l5.BackColor = Color.Red;
            else
                P8_LBL_l5.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[3, 9])
                P8_LBL_l6.BackColor = Color.Red;
            else
                P8_LBL_l6.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[3, 10])
                P8_LBL_l7.BackColor = Color.Red;
            else
                P8_LBL_l7.BackColor = SystemColors.Control;

       

            if (hardware.my_io.m_input[3, 11])
                P8_LBL_l9.BackColor = Color.Red;
            else
                P8_LBL_l9.BackColor = SystemColors.Control;


            if (hardware.my_io.m_input[3, 12])
                P8_LBL_l11.BackColor = Color.Red;
            else
                P8_LBL_l11.BackColor = SystemColors.Control;

          

            if (hardware.my_io.m_input[3, 13])
                P8_LBL_l13.BackColor = Color.Red;
            else
                P8_LBL_l13.BackColor = SystemColors.Control;

            

            if (hardware.my_io.m_input[3, 14])
                P8_LBL_l15.BackColor = Color.Red;
            else
                P8_LBL_l15.BackColor = SystemColors.Control;

           

            if (hardware.my_io.m_input[3, 15])
                P8_LBL_l17.BackColor = Color.Red;
            else
                P8_LBL_l17.BackColor = SystemColors.Control;

           

            if (hardware.my_io.m_input[4, 0])
                P8_LBL_l19.BackColor = Color.Red;
            else
                P8_LBL_l19.BackColor = SystemColors.Control;

          

            if (hardware.my_io.m_input[4, 1])
                P8_LBL_l21.BackColor = Color.Red;
            else
                P8_LBL_l21.BackColor = SystemColors.Control;

        

            if (hardware.my_io.m_input[4, 2])
                P8_LBL_l23.BackColor = Color.Red;
            else
                P8_LBL_l23.BackColor = SystemColors.Control;
 

            if (hardware.my_io.m_input[4, 3])
                P8_LBL_l25.BackColor = Color.Red;
            else
                P8_LBL_l25.BackColor = SystemColors.Control;
 


            #endregion

            #region P9
            if (hardware.my_io.m_input[4, 18])
                P9_LBL_l1.BackColor = Color.Red;
            else
                P9_LBL_l1.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[4, 19])
                P9_LBL_l2.BackColor = Color.Red;
            else
                P9_LBL_l2.BackColor = SystemColors.Control;

            #endregion

            #region P10
            /* if (hardware.my_io.m_input[4, 18])
                 P10_LBL_l1.BackColor = Color.Red;
             else
                 P10_LBL_l1.BackColor = SystemColors.Control;

             if (hardware.my_io.m_input[4, 19])
                 P10_LBL_l2.BackColor = Color.Red;
             else
                 P10_LBL_l2.BackColor = SystemColors.Control;

             if (hardware.my_io.m_input[4, 20])
                 P10_LBL_l3.BackColor = Color.Red;
             else
                 P10_LBL_l3.BackColor = SystemColors.Control;

             if (hardware.my_io.m_input[4, 21])
                 P10_LBL_l4.BackColor = Color.Red;
             else
                 P10_LBL_l4.BackColor = SystemColors.Control;
            */
            #endregion


            #region P11


            if (hardware.my_io.m_output[3, 8])
                P11_BTN_b4.BackColor = Color.Red;
            else
                P11_BTN_b4.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[3, 9])
                P11_BTN_b2.BackColor = Color.Red;
            else
                P11_BTN_b2.BackColor = SystemColors.Control;


            if (hardware.my_io.m_output[3, 10])
            {
                P11_BTN_b5.BackColor = Color.Red;
                P11_BTN_b6.BackColor = SystemColors.Control;
            }
            else
            {
                P11_BTN_b5.BackColor = SystemColors.Control;
                P11_BTN_b6.BackColor = Color.Red;
            }
            if (hardware.my_io.m_output[3, 11])
            {
                P11_BTN_b7.BackColor = Color.Red;
                P11_BTN_b8.BackColor = SystemColors.Control;
            }
            else
            {
                P11_BTN_b7.BackColor = SystemColors.Control;
                P11_BTN_b8.BackColor = Color.Red;
            }

            if (hardware.my_io.m_output[3, 12])
                P11_BTN_b3.BackColor = Color.Red;
            else
                P11_BTN_b3.BackColor = SystemColors.Control;

            if (hardware.my_io.m_output[3, 13])
                P11_BTN_b1.BackColor = Color.Red;
            else
                P11_BTN_b1.BackColor = SystemColors.Control;


            if (hardware.my_io.m_input[3, 0])
                 P11_LBL_l1.BackColor = Color.Red;
             else
                 P11_LBL_l1.BackColor = SystemColors.Control;

             if (hardware.my_io.m_input[3, 1])
                 P11_LBL_l2.BackColor = Color.Red;
             else
                 P11_LBL_l2.BackColor = SystemColors.Control;

             if (hardware.my_io.m_input[3, 2])
                 P11_LBL_l3.BackColor = Color.Red;
             else
                 P11_LBL_l3.BackColor = SystemColors.Control;

             if (hardware.my_io.m_input[3, 3])
                 P11_LBL_l4.BackColor = Color.Red;
             else
                 P11_LBL_l4.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[3, 5])
                 P11_LBL_l5.BackColor = Color.Red;
             else
                 P11_LBL_l5.BackColor = SystemColors.Control;

            if (hardware.my_io.m_input[3, 4])
                 P11_LBL_l6.BackColor = Color.Red;
             else
                 P11_LBL_l6.BackColor = SystemColors.Control;
             
            #endregion
        }

        private void P1_BTN_b1_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(0, 8, 1);
            hardware.my_io.Set_Do(0, 9, 0);
        }

        private void P1_BTN_b2_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(0, 8, 0);
            hardware.my_io.Set_Do(0, 9, 1);
        }

        private void P1_BTN_b3_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(0, 10, 1);
            hardware.my_io.Set_Do(0, 11, 0);
        }

        private void P1_BTN_b4_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(0, 10, 0);
            hardware.my_io.Set_Do(0, 11, 1);
        }

        private void P1_BTN_b5_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(0, 12, 1);
            hardware.my_io.Set_Do(0, 13, 0);
        }

        private void P1_BTN_b6_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(0, 12, 0);
            hardware.my_io.Set_Do(0, 13, 1);
        }

        private void P2_BTN_b1_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(1, 0, 1);
            hardware.my_io.Set_Do(1, 1, 0);
        }

        private void P2_BTN_b2_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(1, 0, 0);
            hardware.my_io.Set_Do(1, 1, 1);
        }

        private void P4_BTN_b1_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(1, 10, 1);
            hardware.my_io.Set_Do(1, 11, 0);
        }

        private void P4_BTN_b2_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(1, 10, 0);
            hardware.my_io.Set_Do(1, 11, 1);
        }

        private void P4_BTN_b3_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(1, 12, 1);
            hardware.my_io.Set_Do(1, 13, 0);
        }

        private void P4_BTN_b4_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(1, 12, 0);
            hardware.my_io.Set_Do(1, 13, 1);
        }

        private void P3_BTN_b1_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(1, 6, 1);
            hardware.my_io.Set_Do(1, 7, 0);
        }

        private void P3_BTN_b2_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(1, 6, 0);
            hardware.my_io.Set_Do(1, 7, 1);
        }

        private void P3_BTN_b3_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(1, 8, 1);
            hardware.my_io.Set_Do(1, 9, 0);
        }

        private void P3_BTN_b4_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(1, 8, 0);
            hardware.my_io.Set_Do(1, 9, 1);
        }

        private void P5_BTN_b1_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(1, 14, 1);
            hardware.my_io.Set_Do(1, 15, 0);
        }

        private void P5_BTN_b2_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(1, 14, 0);
            hardware.my_io.Set_Do(1, 15, 1);
        }

        private void P5_BTN_b3_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(2, 0, 1);
            hardware.my_io.Set_Do(2, 1, 0);
        }

        private void P5_BTN_b4_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(2, 0, 0);
            hardware.my_io.Set_Do(2, 1, 1);
        }

        private void P7_BTN_b1_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(2, 2, 1);
            hardware.my_io.Set_Do(2, 3, 0);
        }

        private void P7_BTN_b2_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(2, 2, 0);
            hardware.my_io.Set_Do(2, 3, 1);
        }

        private void P7_BTN_b3_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(2, 4, 1);
            hardware.my_io.Set_Do(2, 5, 0);
        }

        private void P7_BTN_b4_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(2, 4, 0);
            hardware.my_io.Set_Do(2, 5, 1);
        }

        private void P8_BTN_b1_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(1, 2, 1);
            hardware.my_io.Set_Do(1, 3, 0);
        }

        private void P8_BTN_b2_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(1, 2, 0);
            hardware.my_io.Set_Do(1, 3, 1);
        }

        private void P8_BTN_b5_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(3, 0, 1);
            hardware.my_io.Set_Do(3, 1, 0);
        }

        private void P8_BTN_b6_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(3, 0, 0);
            hardware.my_io.Set_Do(3, 1, 1);
        }

        private void P8_BTN_b7_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(3, 2, 1);
            hardware.my_io.Set_Do(3, 3, 0);
        }

        private void P8_BTN_b8_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(3, 2, 0);
            hardware.my_io.Set_Do(3, 3, 1);
        }

        private void P8_BTN_b9_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(3, 4, 1);
            hardware.my_io.Set_Do(3, 5, 0);
        }

        private void P8_BTN_b10_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(3, 4, 0);
            hardware.my_io.Set_Do(3, 5, 1);
        }

        private void P8_BTN_b11_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(3, 6, 1);
            hardware.my_io.Set_Do(3, 7, 0);
        }

        private void P8_BTN_b12_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(3, 6, 0);
            hardware.my_io.Set_Do(3, 7, 1);
        }

        private void P8_BTN_b13_Click(object sender, EventArgs e)
        {
            
        }

        private void P8_BTN_b14_Click(object sender, EventArgs e)
        {
             
        }

        private void P8_BTN_b15_Click(object sender, EventArgs e)
        {
           
        }

        private void P8_BTN_b16_Click(object sender, EventArgs e)
        {
         
        }

        private void P8_BTN_b3_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(1, 4, 1);
            hardware.my_io.Set_Do(1, 5, 0);
        }

        private void P8_BTN_b4_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(1, 4, 0);
            hardware.my_io.Set_Do(1, 5, 1);
        }

        private void P7_BTN_b5_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(2, 6, 1);
            hardware.my_io.Set_Do(2, 7, 0);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(2, 6, 0);
            hardware.my_io.Set_Do(2, 7, 1);
        }

        private void P1_BTN_b7_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(0, 14, 1);
            hardware.my_io.Set_Do(0, 15, 0);
        }

        private void P1_BTN_b8_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(0, 14, 0);
            hardware.my_io.Set_Do(0, 15, 1);
        }

        private void P11_BTN_b1_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(3, 13, 1);
            hardware.my_io.Set_Do(3, 9, 0);
        }

        private void P11_BTN_b2_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(3, 13, 0);
            hardware.my_io.Set_Do(3, 9, 1);
        }

        private void P11_BTN_b3_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(3, 12, 1);
            hardware.my_io.Set_Do(3, 8, 0);
        }

        private void P11_BTN_b4_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(3, 12, 0);
            hardware.my_io.Set_Do(3, 8, 1);
        }

        private void P11_BTN_b5_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(3, 10, 1);
            
        }

        private void P11_BTN_b6_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(3, 10, 0);
           
        }

        private void P11_BTN_b7_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(3, 11, 1);
        }

        private void P11_BTN_b8_Click(object sender, EventArgs e)
        {
            hardware.my_io.Set_Do(3, 11, 0);
        }
    }
}
