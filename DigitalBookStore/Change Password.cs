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
    public partial class Change_Password : UserControl
    {
        SqlConnection conn = new SqlConnection(DataConn.str);
        public Change_Password()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnlog_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtold.Clear();
            txtnew.Clear();
            txtnew2.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
              try
            {
                if (string.IsNullOrEmpty(txtold.Text) || string.IsNullOrWhiteSpace(txtold.Text) || string.IsNullOrEmpty(txtnew.Text) || string.IsNullOrWhiteSpace(txtnew.Text) || string.IsNullOrEmpty(txtnew2.Text) || string.IsNullOrWhiteSpace(txtnew2.Text))
                {
                    if (txtnew.Text == "")
                        MessageBox.Show("please enter new password");

                    if (txtold.Text == "")
                        MessageBox.Show("please enter old password");

                    if (txtnew2.Text == "")
                        MessageBox.Show("please re-type new password");
                }
                else
                {
                    string qr = "select * from login where pwd='" + txtold.Text + "' ";
                    DataSet ds = new DataSet();
                    SqlDataAdapter adpt = new SqlDataAdapter(qr, conn);
                    adpt.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (txtnew.Text == txtnew2.Text)
                        {

                            conn.Open();
                            string str = "update login set pwd='" + txtnew.Text + "'";
                            SqlCommand cmd = new SqlCommand(str, conn);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Your password has been changed!!", "password changed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            txtnew.Text = txtold.Text = txtnew2.Text = "";
                            conn.Close();
                        }
                        else
                        {
                            MessageBox.Show("Your password doesnt match", "password mismatched", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Wronng password"); 
                        MessageBox.Show("you have entered wrong old padssword", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        

        }

        private void Change_Password_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                txtnew.UseSystemPasswordChar = false;
            else
                txtnew.UseSystemPasswordChar = true;

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                txtnew2.UseSystemPasswordChar = false;
            else
                txtnew2.UseSystemPasswordChar = true;

        }
    }
}
