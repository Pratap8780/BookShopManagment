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
    public partial class SellView : UserControl
    {
        SqlConnection conn = new SqlConnection(DataConn.str);
        public SellView()
        {
            InitializeComponent();
        }
        public void display()
        {
            SqlDataAdapter da = new SqlDataAdapter("select sell.sId as ID,sell.cus as [Customer Name],bookstock.bookname as [Book Name],bookstock.booktype as [Book Type],Bookstock.Author,sell.date as [Date],Bookstock.Price,sell.qty as [Quantity] ,sell.total as [Total] from Bookstock join sell on Bookstock.Id=sell.bid", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void SellView_Load(object sender, EventArgs e)
        {
            dataGridView1.Width=this.Width;
            display();
        }

        private void txtunm_TextChanged(object sender, EventArgs e)
        {

        }

        private void btns_Click(object sender, EventArgs e)
        {
            search();
        }
        public void search()
        {
            SqlDataAdapter da = new SqlDataAdapter("select sell.sId as ID,sell.cus as [Customer Name],bookstock.bookname as [Book Name],bookstock.booktype as [Book Type],Bookstock.Author,Bookstock.Price,sell.qty as [Quantity] ,sell.total as [Total] from Bookstock join sell on Bookstock.Id=sell.bid where cus like '%" + txtunm2.Text + "%'", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Width = 0;
            dataGridView1.Columns[1].Width = 250;
            dataGridView1.Columns[2].Width = 250;
            dataGridView1.Columns[3].Width = 200;
            dataGridView1.Columns[4].Width = 200;
            dataGridView1.Columns[5].Width = 150;
            dataGridView1.Columns[6].Width = 120;
            dataGridView1.Columns[7].Width = 80;
            dataGridView1.Columns[8].Width = 40;
            dataGridView1.Columns[9].Width = 60;

            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


            try
            {

                if (MessageBox.Show("do you want to delete this record?", "project", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sell_p", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("sid", dataGridView1.SelectedCells[0].Value.ToString());
                    cmd.Parameters.Add("act", "del");
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    display();
                    MessageBox.Show("delete successfully", "project", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString(), "project", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
