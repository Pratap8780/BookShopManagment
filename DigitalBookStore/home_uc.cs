using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DigitalBookStore
{
    public partial class home_uc : UserControl
    {
        SqlConnection conn = new SqlConnection(DataConn.str);
        public home_uc()
        {
            InitializeComponent();
        }
        private int imageno = 1;

        private void loadimage()
        {
            if (imageno == 11)
            {
                imageno = 1;
            }
            pictureBox4.ImageLocation = string.Format(@"Images\{0}.jpeg", imageno);
            imageno++;
        }



       
      
        

        private void home_uc_Load(object sender, EventArgs e)
        {
            
            setstock();
            setsell();
            setmoney();
        }
        public void setstock()
        {
            SqlDataAdapter da = new SqlDataAdapter("select sum(Quantity) from Bookstock ", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            lblstk.Text = dt.Rows[0][0].ToString();
        }
        public void setsell()
         {
            SqlDataAdapter da = new SqlDataAdapter("select sum(qty) from sell ", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            lblsold.Text = dt.Rows[0][0].ToString();
        }
        public void setmoney()
        {
            SqlDataAdapter da = new SqlDataAdapter("select sum(total) from sell ", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            lblmo.Text = dt.Rows[0][0].ToString();
        }

        private void lblsold_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            loadimage();
        }
    }
}
