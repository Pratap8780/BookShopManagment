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
    public partial class viewstock : UserControl
    {
        SqlConnection conn = new SqlConnection(DataConn.str);
        public viewstock()
        {
            InitializeComponent();
        }

        public void display()
        {
            SqlDataAdapter da = new SqlDataAdapter("select *from Bookstock", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 150;
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].Width = 50;
            dataGridView1.Columns[8].Width = 50;
            dataGridView1.Columns[9].Width = 63;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void viewstock_Load(object sender, EventArgs e)
        {
            display();

        }

       
        private void txtunm_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
               
                if (MessageBox.Show("do you want to delete this record?", "project", MessageBoxButtons.YesNo, MessageBoxIcon.Information)== DialogResult.Yes)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("bookstock_p", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("id", dataGridView1.SelectedCells[0].Value.ToString());
                    cmd.Parameters.Add("act" , "del");
                    cmd.ExecuteNonQuery();
                    conn.Close();
                  
                    display();
                      MessageBox.Show("delete successfully", "project", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
            }
            catch  (Exception exc)
            {
               MessageBox.Show(exc.ToString(), "project", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        
    

        

        private void btns_Click(object sender, EventArgs e)
        {
            search();
        }

        public void search()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Bookstock where bookname like '%"+txtunm.Text+"%'",conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

    }



}

