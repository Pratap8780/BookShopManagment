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
    public partial class pagestock : UserControl
    {
        SqlConnection conn = new SqlConnection(DataConn.str);
        public pagestock()
        {
            InitializeComponent();
        }

        private void pagestock_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtunm_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int p, q;
                if (txtprice.Text == null)
                {
                    p = 1;
                }
                else
                {
                    p = int.Parse(txtprice.Text);
                }
                if (txtQ.Text == null)
                {
                    q = 1;
                }
                else

                    q = int.Parse(txtQ.Text);
                int tot = p * q;
                txttotal.Text = tot.ToString();
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.ToString());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnlog_Click(object sender, EventArgs e)
        {   
            try
            {
                if (txtID.Text != "" && txtbookname.Text != "" && txtauthor.Text != "" && txtdescr.Text != "" && txtlagn.Text != "" && txtedition.Text != "" && txtprice.Text != "" && comboBox1.Text != "" && txtQ.Text != "" && txttotal.Text != "")
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Bookstock_p", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("ID", txtID.Text.ToString());
                    cmd.Parameters.Add("BookType", comboBox1.Text);
                    cmd.Parameters.Add("bookname", txtbookname.Text);
                    cmd.Parameters.Add("Author", txtauthor.Text);
                    cmd.Parameters.Add("descr", txtdescr.Text);
                    cmd.Parameters.Add("lang", txtlagn.Text);
                    cmd.Parameters.Add("Edition", txtedition.Text);
                    cmd.Parameters.Add("Price", txtprice.Text.ToString());
                    cmd.Parameters.Add("Quantity", txtQ.Text.ToString());
                    cmd.Parameters.Add("Total", txttotal.Text.ToString());
                    cmd.Parameters.Add("act", "ins");
                    cmd.ExecuteNonQuery();
                    conn.Close();
                   
                    
                    MessageBox.Show("registered successfully", "project", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("all information is required", "project", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception exc)
            {

                MessageBox.Show(exc.ToString(), "project", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtID.Text="";
            txtprice.Text = "";
            txttotal.Text = "";
            txtbookname.Text = "";
            txtauthor.Text = "";
            comboBox1.Text = "";
            txtQ.Text = "";

        }

        private void txtQ_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int p, q;
                if (txtprice.Text == null)
                {
                    p = 1;
                }
                else
                {
                    p = int.Parse(txtprice.Text);
                }
                if (txtQ.Text == null)
                {
                    q = 1;
                }
                else

                    q = int.Parse(txtQ.Text);
                int tot = p * q;
                txttotal.Text = tot.ToString();
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.ToString());
            }
                  }

        private void txtQ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8)

                e.Handled = false;
            else
                e.Handled = true;
            
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8)

                e.Handled = false;
            else
                e.Handled = true;
            
        }
    }
}
