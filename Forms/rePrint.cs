using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PrintingApp.Form1;

namespace PrintingApp
{
    public partial class rePrint: Form
    {
        public string sticker = "";
        public string productName = "";
        public string category = "";
        public rePrint()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string barRead = textBox1.Text.Trim();


            if (int.TryParse(barRead, out int stickerID))
            {
                string connectionString = "Server=DESKTOP-K5H17TC\\MSSQLSERVER01;Database=PrintBarcode;Trusted_Connection=True;TrustServerCertificate=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("fetchSingleSticker", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@stickerID", stickerID);

                        try
                        {
                            conn.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                string message = "";
                                // Read the first result set (the message)
                                if (reader.Read())
                                {
                                    message = reader["message"].ToString();
                                }

                                // Check if there is a second result set for the category data.
                                if (reader.NextResult()&& reader.Read())
                                {
                                    sticker= reader["sticker"].ToString();
                                    productName= reader["productName"].ToString();
                                    category= reader["category"].ToString();
                                }
                                if (message == "success")
                                {
                                    PrintSticker(sticker, productName, category);
                                }
                                else Message.Custom("Insert Status", message, new Point(300, 80));
                            }
                        }
                        catch (Exception ex)
                        {
                            // Handle exceptions as needed
                            Message.Custom("Insert Status", ex.Message, new Point(300, 80));
                        }
                    }
                }
            }
            else
            {
                Message.Custom("Insert Status", "Invalid Barcode format.", new Point(300, 80));
            }
        }
        private void PrintSticker(string zplCode, string name, string category)
        {
            byte[] zpl = Encoding.UTF8.GetBytes(zplCode);
            var request = (HttpWebRequest)WebRequest.Create("http://api.labelary.com/v1/printers/8dpmm/labels/4x2.5/0/");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = zpl.Length;

            using (var reqStream = request.GetRequestStream())
                reqStream.Write(zpl, 0, zpl.Length);

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                using (var respStream = response.GetResponseStream())
                {
                    // *** HERE: load the image directly ***
                    Image img = Image.FromStream(respStream);

                    // *** ASSIGN to your PictureBox ***
                    pictureBox1.Image?.Dispose();
                    pictureBox1.Image = img;
                }
            }
            catch (WebException ex)
            {
                Message.Custom("Print Error", $"Error fetching label: {ex.Message}", new Point(620, 80));
            }
        }


    }
}
