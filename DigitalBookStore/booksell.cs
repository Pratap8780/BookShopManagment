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
    public partial class booksell : UserControl
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\BCA Sem 5\Project\project\DigitalBookStore\DigitalBookStore\Database1.mdf;Integrated Security=True");

        public booksell()
        {
            InitializeComponent();
        }

        private void booksale_Load(object sender, EventArgs e)
        {
            bindType();
        }
        public void bindType()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select bookType from BookStock", conn);
                DataTable dt = new DataTable();
                combotype.Items.Clear();
                da.Fill(dt);
                combotype.Items.Add("---Select Book Type---");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    combotype.Items.Add(dt.Rows[i][0].ToString());
                }
                combotype.SelectedIndex = 0;
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.ToString());
            }
        }
        public void bindName()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select bookname from BookStock where BookType='" + combotype.SelectedItem.ToString() + "'", conn);
                DataTable dt = new DataTable();
                comboname.Items.Clear();
                da.Fill(dt);
                comboname.Items.Add("---Select Book Name---");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboname.Items.Add(dt.Rows[i][0].ToString());
                }
                comboname.SelectedIndex = 0;
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.ToString());
            }
        }

        private void combotype_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindName();
        }
        static int Id;
        private void comboname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from BookStock where BookType='" + combotype.SelectedItem.ToString() + "' and bookname='" + comboname.SelectedItem.ToString() + "'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Id = int.Parse(dt.Rows[0][0].ToString());
                txtauthor.Text = dt.Rows[0][3].ToString();
                txtdescr.Text = dt.Rows[0][4].ToString();
                txtlagn.Text = dt.Rows[0][5].ToString();
                txtedition.Text = dt.Rows[0][6].ToString();
                txtprice.Text = dt.Rows[0][7].ToString();

            }
            catch (Exception exc)
            {

            }
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
                if (txtQty.Text == null)
                {
                    q = 1;
                }
                else

                    q = int.Parse(txtQty.Text);
                int discp = int.Parse(txtdisunt.Text);
                int disct = (p * q) * discp / 100;
                int tot = (p * q) - disct; ;

                txttotal.Text = tot.ToString();
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.ToString());
            }
        }

        private void btnlog_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtQty.Text != "" && txttotal.Text != "" && txtcus.Text != "")
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select Quantity from BookStock where BookType='" + combotype.SelectedItem.ToString() + "' and bookname='" + comboname.SelectedItem.ToString() + "'", conn);
                    int qty = int.Parse(cmd.ExecuteScalar().ToString());
                    conn.Close();
                    if (qty < int.Parse(txtQty.Text))
                    {
                        MessageBox.Show("insufficient stock");
                    }
                    else
                    {
                        conn.Open();
                        cmd = new SqlCommand("stockMinus_sp", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("qty", txtQty.Text);
                        cmd.Parameters.Add("id", Id);
                        cmd.ExecuteNonQuery();
                        conn.Close();


                        conn.Open();
                        cmd = new SqlCommand("sell_p", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("date", DateTime.Now.ToString());
                        cmd.Parameters.Add("qty", txtQty.Text);
                        cmd.Parameters.Add("cus", txtcus.Text);
                        cmd.Parameters.Add("total", txttotal.Text);
                        cmd.Parameters.Add("bid", Id);
                        cmd.Parameters.Add("act", "ins");
                        cmd.ExecuteNonQuery();
                        conn.Close();



                        MessageBox.Show("registered successfully", "project", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
            txtauthor.Text = "";
            txtdescr.Text = "";
            txtdisunt.Text = "";
            txtedition.Text = "";
            txtlagn.Text = "";
            txtprice.Text = "";
            txtQty.Text = "";
            txttotal.Text = "";
            comboname.Text = "";
            combotype.Text = "";
        }

        private void txtcus_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtedition_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtdisunt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
