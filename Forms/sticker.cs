using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;   // Make sure you have ZXing.Rendering in your references

namespace PrintingApp
{
    public partial class sticker : Form
    {
        private readonly string _categoryFilter;
        private DateTime _currentDate;

        public sticker() : this(null) { }
        public sticker(string category)
        {
            InitializeComponent();
            _categoryFilter = category;
            dataGridView1.AllowUserToAddRows = false;
            // Ensure the 3rd column is an image column (if not set in designer)
            if (!(dataGridView1.Columns[3] is DataGridViewImageColumn))
            {
                var imgCol = new DataGridViewImageColumn
                {
                    Name = "BarcodeImage",
                    HeaderText = "Barcode"
                };
                dataGridView1.Columns.RemoveAt(3);
                dataGridView1.Columns.Insert(3, imgCol);
                imgCol.DefaultCellStyle.Padding = new Padding(5, 5, 5, 5);
            }
            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
            _currentDate = Program.AppStartDate;
            dateTimePicker1.Value = _currentDate;
        }
        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(dataGridView1.Columns[e.ColumnIndex].Name == "status")
            {
                string statusValue = e.Value?.ToString() ?? "";
                Color celColor = Color.White;
                if (statusValue == "Pending...")
                    celColor = Color.IndianRed;
                else if (statusValue == "Scanned")
                    celColor = Color.MediumSeaGreen;
                // else leave it White (or whatever your default)
                // Apply it (and keep it under selection)
                e.CellStyle.BackColor = celColor;
                e.CellStyle.SelectionBackColor = celColor;
            }
            // Only care about the “category” column
            if (dataGridView1.Columns[e.ColumnIndex].Name != "category")
                return;

            // Grab the text
            var catValue = e.Value?.ToString() ?? "";

            // Decide color with if/else
            Color cellColor = Color.White;
            if (catValue == "Unit")
                cellColor = Color.MediumSeaGreen;
            else if (catValue == "Mono")
                cellColor = Color.CornflowerBlue;
            else if (catValue == "Plan")
                cellColor = Color.Salmon;
            // else leave it White (or whatever your default)
            // Apply it (and keep it under selection)
            e.CellStyle.BackColor = cellColor;
            e.CellStyle.SelectionBackColor = cellColor;
        }

        private void sticker_Load(object sender, EventArgs e)
        {
            getStickerData(_categoryFilter,_currentDate);
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // TODO: call your data‑load here
            Program.AppStartDate = dateTimePicker1.Value;
            _currentDate = dateTimePicker1.Value;
            getStickerData(_categoryFilter, _currentDate);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string barRead = textBox1.Text.Trim();

            if (barRead.Length < 6) return;

            if (int.TryParse(barRead, out int stickerID))
            {
                UpdateStickerStatus(stickerID);
                getStickerData(_categoryFilter, _currentDate);  // Refresh the grid after update
                textBox1.Clear(); // Optional: Clear the box after scan
            }
            else
            {
                MessageBox.Show("Invalid barcode format.");
            }
        }
        private void getStickerData(string category, DateTime selectedDate)
        {
            const string connectionString =
                "Server=DESKTOP-K5H17TC\\MSSQLSERVER01;" +
                "Database=PrintBarcode;" +
                "Trusted_Connection=True;" +
                "TrustServerCertificate=True";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("fetchStickerData", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                // If category is null, pass DBNull.Value
                cmd.Parameters.AddWithValue(
                    "@category",
                    (object)category ?? DBNull.Value
                );
                cmd.Parameters.AddWithValue("@date",selectedDate);

                try
                {
                    var dt = new DataTable();
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    dataGridView1.Rows.Clear();
                    foreach (DataRow dr in dt.Rows)
                    {
                        string productName = dr["productName"]?.ToString() ?? "";
                        string barcodeText = dr["barcode"]?.ToString() ?? "";
                        string cat = dr["category"]?.ToString() ?? "";
                        bool scanned = dr["status"]?.ToString() == "True";
                        string statusText = scanned ? "Scanned" : "Pending...";

                        // generate barcode image
                        Image barcodeImg = GenerateBarcodeImage(barcodeText);

                        // 1) Make a new DataGridViewRow and create the right number of cells
                        var row = new DataGridViewRow();
                        row.CreateCells(dataGridView1);

                        // 2) Fill in columns 0-5
                        row.Cells[0].Value = dr["stickerID"];
                        row.Cells[1].Value = productName;
                        row.Cells[2].Value = cat;
                        row.Cells[3].Value = barcodeImg;
                        row.Cells[4].Value = statusText;

                        // 3) Conditionally add either the button or a “Done” text cell
                        // prepare a placeholder for the Action cell

                        // 3) add the row into the grid
                        dataGridView1.Rows.Add(row);

                        // assume column-index 2 is your category

                       
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Error fetching stickers",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void UpdateStickerStatus(int stickerID)
        {
            string connectionString = "Server=DESKTOP-K5H17TC\\MSSQLSERVER01;Database=PrintBarcode;Trusted_Connection=True;TrustServerCertificate=True"; // Replace with your actual connection string

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateStickerStatus", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@stickerID", stickerID);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Message.Custom("Insert Status", ex.Message, new Point(300, 80));
                    }
                }
            }
            
        }


        private Image GenerateBarcodeImage(string text, int width = 180, int height = 60)
        {
            var writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Width = width,
                    Height = height,
                    Margin = 10,
                    PureBarcode = true
                }
            };

            var pixelData = writer.Write(text);

            // Create bitmap and copy pixel data
            var bmp = new Bitmap(pixelData.Width, pixelData.Height,
                                 System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            var data = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                System.Drawing.Imaging.ImageLockMode.WriteOnly,
                bmp.PixelFormat
            );
            System.Runtime.InteropServices.Marshal.Copy(
                pixelData.Pixels, 0, data.Scan0, pixelData.Pixels.Length
            );
            bmp.UnlockBits(data);

            return bmp;
        }

    }
}
