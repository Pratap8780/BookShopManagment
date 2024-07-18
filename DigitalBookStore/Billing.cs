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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
namespace DigitalBookStore
{
    public partial class Billing : UserControl
    {
        SqlConnection conn = new SqlConnection(DataConn.str);
        public Billing()
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
        private void Billing_Load(object sender, EventArgs e)
        {

        }
        
        public void search()
        {
            SqlDataAdapter da = new SqlDataAdapter("select sell.sId as ID,sell.cus as [Customer Name],bookstock.bookname as [Book Name],Bookstock.Price,sell.qty as [Quantity] ,sell.total as [Total] from Bookstock join sell on Bookstock.Id=sell.bid where cus like '%" + txtunm2.Text + "%'", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void btns_Click(object sender, EventArgs e)
        {
            dataGridView1.Width = this.Width;
            search();
        }
       
           
        private void btnpdf_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {

                SqlDataAdapter da = new SqlDataAdapter("select sell.sId as ID,sell.cus as [Customer Name],bookstock.bookname as [Book Name],Bookstock.Price,sell.qty as [Quantity] ,sell.total as [Total] from Bookstock join sell on Bookstock.Id=sell.bid where cus like '%" + txtunm2.Text + "%'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                int n = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    n += int.Parse(dt.Rows[i][5].ToString());
                }
             
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Output.pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            PdfPTable pdfTable = new PdfPTable(dataGridView1.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dataGridView1.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfTable.AddCell(cell.Value.ToString());
                                }
                            }

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                pdfDoc.Add(new Paragraph("Digital book shop"));
                                pdfDoc.Add(new Paragraph(" "));
                                pdfDoc.Add(new Paragraph(" "));
                                pdfDoc.Add(pdfTable);
                                pdfDoc.Add(new Paragraph("Total:"+n));
                                pdfDoc.Close();
                                stream.Close();
                            }

                            MessageBox.Show("Data Exported Successfully !!!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record To Export !!!", "Info");
            }
        }


    }
}
