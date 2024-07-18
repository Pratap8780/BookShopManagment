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
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(DataConn.str);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtunm.Text == "" || txtpwd.Text == "") MessageBox.Show("please enter username or password.", "login form");
                else
                {
                    SqlDataAdapter da = new SqlDataAdapter("select * from login where unm='" + txtunm.Text + " ' and pwd='" + txtpwd.Text + "'", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("welcome.", "login form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        homepage f = new homepage();
                        this.Hide();
                        f.lblunm.Text = txtunm.Text;
                        f.Show();
                    }
                    else

                        MessageBox.Show("incorrect user name or password.", "login form", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString(), "login form");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you want to exit form", "exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                txtpwd.UseSystemPasswordChar = false;
            else
                txtpwd.UseSystemPasswordChar = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            forgotpassword f1 = new forgotpassword();
            this.Hide();
            f1.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}


