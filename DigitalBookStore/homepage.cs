using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DigitalBookStore
{
    public partial class homepage : Form
    {
        int panelwidth;
        bool iscollapsed;

        public homepage()
        {
            InitializeComponent();
            timerTime.Start();
            panelwidth = panelLeft.Width;
            iscollapsed = false;
            home_uc uch = new home_uc();
            addcontrolstopanel(uch);

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (iscollapsed)
            {
                panelLeft.Width = panelLeft.Width + 10;
                if (panelLeft.Width >= panelwidth)
                {
                    timer1.Stop();
                    iscollapsed = false;
                    this.Refresh();
                }
            }
            else
            {
                panelLeft.Width = panelLeft.Width - 10;
                if (panelLeft.Width <= 59)
                {
                    timer1.Stop();
                    iscollapsed = true;
                    this.Refresh();
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void movesidepanel(Control btn)
        {
            panelSide.Top = btn.Top;
            panelSide.Height = btn.Height;
        }
        private void addcontrolstopanel(Control c)
        {
            c.Dock = DockStyle.Fill;
            panelControls.Controls.Clear();
            panelControls.Controls.Add(c);

        }
      

     
        private void btnhome_Click(object sender, EventArgs e)
        {
            
            movesidepanel(btnhome);
            home_uc uch = new home_uc();
            addcontrolstopanel(uch);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            movesidepanel(btn2);
            pagestock pg = new pagestock();
            addcontrolstopanel(pg);

        }
        private void btn3_Click(object sender, EventArgs e)
        {
            movesidepanel(btn3);
            viewstock vv = new viewstock();
            addcontrolstopanel(vv);

        }

        private void btn4_Click(object sender, EventArgs e)
        {
            movesidepanel(btn4);
            booksell bs = new booksell();
            addcontrolstopanel(bs);
        }

       

        private void btn6_Click(object sender, EventArgs e)
        {
            movesidepanel(btn6); SellView sv = new SellView();
            addcontrolstopanel(sv);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if (btnexit.Visible && btnchng.Visible)
            {
                btnexit.Visible = false;
                btnchng.Visible = false;
            }
            else
            {
                btnexit.Visible = true;
                btnchng.Visible = true;
                
            }movesidepanel(btn7);
        }

        private void timerTime_Tick(object sender, EventArgs e)
        {
            labelTime.Text = "Time:" + DateTime.Now.ToLongTimeString();


        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Show();
        }

        private void btnchng_Click(object sender, EventArgs e)
        {
            movesidepanel(btnchng);
            Change_Password pwd = new Change_Password();
            addcontrolstopanel(pwd);
        }

        

        private void panelControls_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
