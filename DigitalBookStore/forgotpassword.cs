using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;

namespace DigitalBookStore
{
    public partial class forgotpassword : Form
    

    {
        string otp;
         SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\BCA Sem 5\Project\project\DigitalBookStore\DigitalBookStore\Database1.mdf;Integrated Security=True");
             public forgotpassword()
        {
            InitializeComponent();
        }

        private void btnlog_Click(object sender, EventArgs e)
        {
           
        }

       
        private void button3_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();

        }

        private void forgotpassword_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnOTP_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select *from login where email='" + txtEmail.Text + "'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Random r = new Random();
                    otp = r.Next(9999).ToString();
//                   MessageBox.Show(otp);

                    MailMessage msg = new MailMessage();
                    string from = "naynodedra@gmail.com";
                    string pass = "qvktpqxuaiirdoey";
                    string msgBody = "your OTP is :"+otp+".";
                    string subject = "Digital book shop";
                    msg.To.Add(txtEmail.Text);
                    msg.From = new MailAddress(from);
                    msg.Body = msgBody;
                    msg.Subject = subject;

                    SmtpClient client = new SmtpClient("smtp.gmail.com");
                    client.EnableSsl = true;
                    client.Port = 587;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Credentials = new NetworkCredential(from, pass);
                    client.Send(msg);
                    MessageBox.Show("OTP SEND.....");
                }
                else
                {
                    MessageBox.Show("please try again with registered email");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOTP.Text == otp)
                {
                    panelpwd.Enabled = true;
                    panelEmail.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Invalid OTP");
                    panelpwd.Enabled = false;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtpwd.Text != txtpwd2.Text)
                {
                    MessageBox.Show("password doesn't match");

                }
                else
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("forgot_sp",conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("email",txtEmail.Text);
                    cmd.Parameters.Add("pwd",txtpwd2.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("password reset successfully..");
                }
            }
            catch   (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }


                


        }
    }

