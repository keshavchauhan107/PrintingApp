using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing.PDF417.Internal;

namespace PrintingApp.Forms
{
    public partial class generateSticker: Form
    {
        public class Sticker
        {
            public int StickerID { get; set; }
            public string Plant { get; set; }
            public string Line { get; set; }
            public string ProductName { get; set; }
            public string Category { get; set; }
        }
        private BindingList<Sticker> currentStickers = new BindingList<Sticker>();

        List<string> products = new List<string>();
        public int unit = 0, unitPerMono = 0, monoPerPlan = 0, remainingUnit=0, remainingMono=0, remainingPlan = 0,stickerID=0;
        string plant = "", line = "", productName = "",height="",width="";
        List<string> productInfo=new List<string>();
        string mrp = "", description1 = "", description2 = "", description3 = "", weight = "", weightUnit = "";
        public generateSticker()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.generateSticker_Load);
            comboBox3.IntegralHeight = false;      // allow custom DropDownHeight
            comboBox3.MaxDropDownItems = 10;       // (optional) set an upper cap if you want
            comboBox3.TextChanged += comboBox3_TextChanged;
        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            string text = comboBox3.Text;

            // Suspend painting on the ComboBox
            comboBox3.BeginUpdate();
            comboBox3.Items.Clear();

            // Determine which list to show
            var filtered = string.IsNullOrWhiteSpace(text)
                ? products
                : products
                    .Where(item => item.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

            comboBox3.Items.AddRange(filtered.ToArray());

            // Re-open the drop-down so user sees the filtered list
            comboBox3.DroppedDown = true;

            // Keep the caret at the end so typing continues smoothly
            comboBox3.SelectionStart = text.Length;
            comboBox3.SelectionLength = 0;

            // Resume painting
            comboBox3.EndUpdate();
        }

        private void generateSticker_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel4.Visible = false;
            LoadProducts();
            comboBox1.Items.Add("Plant 1");
            comboBox1.Items.Add("Plant 2");
            comboBox1.Items.Add("Plant 3");
            comboBox1.Items.Add("Plant 4");
            comboBox2.Items.Add("Line 1");
            comboBox2.Items.Add("Line 2");
            comboBox2.Items.Add("Line 3");
            comboBox2.Items.Add("Line 4");
            comboBox2.Items.Add("Line 5");
            comboBox2.Items.Add("Line 6");
            comboBox2.Items.Add("Line 7");
            comboBox2.Items.Add("Line 8");
            comboBox2.Items.Add("Line 9");
            comboBox2.Items.Add("Line 10");
            for (int i = 0; i < products.Count; i++)
            {
                comboBox3.Items.Add(products[i]);
            }

        }

        private void LoadProducts()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Program.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetProducts", conn))
                    {

                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            // 3. Fill a DataTable
                            DataTable dt = new DataTable();
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(dt);
                            }


                            products.Clear();
                            // Example: pulling from a DataTable or any collection
                            foreach (DataRow dr in dt.Rows)
                            {
                                products.Add(dr["productName"].ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            Message.Custom("Insert Status", ex.Message, new Point(300, 80));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Message.Custom("Error", ex.Message, new Point(620, 80));
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem == null || comboBox2.SelectedItem==null || comboBox3.SelectedItem == null)
                {
                    Message.Custom("Invalid Field", "All Field Required", new Point(620, 80));
                    return;
                }
                using (SqlConnection conn = new SqlConnection(Program.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("fetchPlanData", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@plant", comboBox1.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@line", comboBox2.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@productName", comboBox3.SelectedItem.ToString());
                        try
                        {
                            conn.Open();
                            using (var reader = cmd.ExecuteReader())
                            {
                                // 1) First result: message
                                string message = null;
                                if (reader.Read())  
                                    message = reader.GetString(0);

                                if (reader.NextResult())
                                {
                                    while (reader.Read())
                                    {
                                        unit = reader.GetInt32(reader.GetOrdinal("unit"));
                                        unitPerMono = reader.GetInt32(reader.GetOrdinal("unitPerMono"));
                                        monoPerPlan = reader.GetInt32(reader.GetOrdinal("monoPerPlan"));
                                    }
                                }
                                if (reader.NextResult())
                                {
                                    while (reader.Read())
                                    {
                                        remainingUnit = reader.GetInt32(reader.GetOrdinal("remaningUnit"));
                                        remainingMono = reader.GetInt32(reader.GetOrdinal("remainingMono"));
                                        remainingPlan = reader.GetInt32(reader.GetOrdinal("remainingPlan"));
                                    }
                                }
                                if (message == "success")
                                {
                                    showPanel();
                                      
                                }
                                else Message.Custom("Insert Status", message, new Point(620, 80));

                            }
                        }
                        catch (Exception ex)
                        {
                            Message.Custom("Insert Status", ex.Message, new Point(620, 80));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Message.Custom("Error", ex.Message, new Point(620, 80));
            }

        }
        private void showPanel()
        {
            plant = comboBox1.SelectedItem.ToString();
            line = comboBox2.SelectedItem.ToString();
            productName = comboBox3.SelectedItem.ToString();
            panel1.Visible = true;
            textBox1.Text = unit.ToString();
            textBox2.Text = unitPerMono.ToString();
            textBox3.Text = monoPerPlan.ToString();
            if (remainingUnit == 0)
            {
                panel3.Visible = false;
            }
            else
            {
                label4.Visible = false;
                textBox4.Text = remainingUnit.ToString();
                textBox5.Text = remainingMono.ToString();
                textBox6.Text = remainingPlan.ToString();
            }
            getStickerData();
        }
        private string CreateZPL()
        {
            string email = "abcd@gmail.com";
            int rowSize = Convert.ToInt32(height) / 14;
            int colSize = Convert.ToInt32(width) / 3;
            int startPosition = rowSize / 4;
            int txtSize = rowSize / 2 + startPosition;
            int imageStart = 0;
            if (Convert.ToInt32(width) > 400)
            {
                imageStart = startPosition;
            }
            if (Convert.ToInt32(height) > 500)
            {
                imageStart = rowSize * 3 + startPosition;
            }
            return "^XA\n" +                                      // Start of label
          "^PW" + width + "\n" +                                      // Set print width
          "^LL" + height + "\n" +                                       // Set label length
          "^LH0,0\n" +                                      // Label home position

          // --- Outer Rectangle and Table Lines ---
          "^FO0,0^GB" + width + "," + height + ",3^FS\n" +
          "^FO0," + rowSize * 1 + "^GB" + width + ",3,3^FS\n" +
          "^FO0," + rowSize * 2 + "^GB" + width + ",3,3^FS\n" +
          "^FO0," + rowSize * 3 + "^GB" + width + ",3,3^FS\n" +
          "^FO0," + rowSize * 4 + "^GB" + width + ",3,3^FS\n" +
          "^FO0," + rowSize * 5 + "^GB" + width + ",3,3^FS\n" +
          "^FO" + colSize + "," + rowSize * 6 + "^GB" + width + ",3,3^FS\n" +
          "^FO" + colSize + "," + rowSize * 7 + "," + "^GB" + width + ",3,3^FS\n" +
          "^FO0," + rowSize * 8 + "^GB" + width + ",3,3^FS\n" +
          "^FO" + colSize + "," + rowSize * 9 + "^GB" + width + ",3,3^FS\n" +
          "^FO" + colSize + "," + rowSize * 10 + "^GB" + width + ",3,3^FS\n" +
          "^FO" + colSize + "," + rowSize * 11 + "^GB" + width + ",3,3^FS\n" +
          "^FO" + colSize + "," + rowSize * 12 + "^GB" + width + ",3,3^FS\n" +
          "^FO" + colSize + "," + rowSize * 13 + "^GB" + width + ",3,3^FS\n" +

          // --- Vertical Dividers ---
          "^FO" + colSize * 2 + ",0^GB3," + rowSize * 4 + ",3^FS\n" +
          "^FO" + colSize + "," + rowSize * 5 + "^GB3," + (Convert.ToInt32(height) - (rowSize * 5)) + ",3^FS\n" +
          "^FO" + colSize * 2 + "," + rowSize * 10 + "^GB3," + (rowSize * 2) + ",3^FS\n" +
          "^FO" + colSize * 2 + "," + rowSize * 12 + "^GB3," + (Convert.ToInt32(height) - (rowSize * 12)) + ",3^FS\n" +
          "^FO" + (colSize * 2 + colSize / 2) + "," + rowSize * 13 + "^GB3," + (Convert.ToInt32(height) - (rowSize * 13)) + ",3^FS\n" +

          // --- Text Fields ---
          "^FO" + startPosition + "," + startPosition + "^A0N," + txtSize + "," + txtSize + "^FDProduct Genric Name " + productName + "^FS\n" +
          "^FO" + (colSize * 2 + startPosition) + "," + startPosition + "^A0N," + txtSize + "," + txtSize + "^FDProduct Quantity^FS\n" +
          "^FO" + startPosition + "," + (rowSize + startPosition) + "^A0N," + txtSize + "," + txtSize + "^FDProduct Description Line 1 "+description1+"^FS\n" +
          "^FO" + startPosition + "," + (rowSize * 2 + startPosition) + "^A0N," + txtSize + "," + txtSize + "^FDProduct Description Line 2 "+description2+"^FS\n" +
          "^FO" + startPosition + "," + (rowSize * 3 + startPosition) + "^A0N," + txtSize + "," + txtSize + "^FDProduct Description Line 3 "+description3+"^FS\n" +
          "^FO" + startPosition + "," + (rowSize * 4 + startPosition) + "^A0N," + txtSize + "," + txtSize + "^FDDisclaimer 1^FS\n" +
          "^FO" + (colSize + startPosition) + "," + (rowSize * 5 + startPosition) + "^A0N," + txtSize + "," + txtSize + "^FDCompany Name^FS\n" +
          "^FO" + (colSize + startPosition) + "," + (rowSize * 6 + startPosition) + "^A0N," + txtSize + "," + txtSize + "^FDAddress^FS\n" +
          "^FO" + (colSize + startPosition) + "," + (rowSize * 7 + startPosition) + "^A0N," + txtSize + "," + txtSize + "^FDDisclaimer 2^FS\n" +
          "^FO" + (colSize + startPosition) + "," + (rowSize * 8 + startPosition) + "^A0N," + txtSize + "," + txtSize + "^FDCustomer Care Executive^FS\n" +
          "^FO" + (colSize + startPosition) + "," + (rowSize * 9 + startPosition) + "^A0N," + txtSize + "," + txtSize + "^FDCC Address^FS\n" +
          "^FO" + (colSize + startPosition) + "," + (rowSize * 10 + startPosition) + "^A0N," + txtSize + "," + txtSize + "^FDCustomer Care NUmber^FS\n" +
          "^FO" + (colSize + startPosition) + "," + (rowSize * 11 + startPosition) + "^A0N," + txtSize + "," + txtSize + "^FDEmail: " + email + "^FS\n" +
          "^FO" + (colSize + startPosition) + "," + (rowSize * 12 + startPosition) + "^A0N," + txtSize + "," + txtSize + "^FDMRP " + mrp + "^FS\n" +
          "^FO" + (colSize + startPosition) + "," + (rowSize * 13 + startPosition) + "^A0N," + txtSize + "," + txtSize + "^FDManufacturing Month/Year^FS\n" +
          "^FO" + (colSize * 2 + colSize / 3) + "," + (rowSize * 12 + startPosition) + "^A0N," + txtSize + "," + txtSize + "^FDMRP Fixed Text^FS\n" +
          "^FO" + (colSize * 2 + startPosition) + "," + (rowSize * 13 + startPosition) + "^A0N," + txtSize + "," + txtSize + "^FDWeight "+weight+"^FS\n" +
          "^FO" + (colSize * 2 + colSize / 2 + startPosition) + "," + (rowSize * 13 + startPosition) + "^A0N," + txtSize + "," + txtSize + "^FDWeight Unit "+weightUnit+"^FS\n" +

          // --- Barcode ---
          "^FO" + (startPosition + 20) + "," + (rowSize * 9 + 2 * startPosition) + "^BY1,3,120\n" +
          "^BCN," + (rowSize * 2 + startPosition) + ",Y,N,N\n" +
          "^FD" +stickerID + "^FS\n" +

          // --- Image ---  
          "^FO" + (startPosition + 20) + "," + (rowSize * 5 + startPosition) + "^GFA," + (rowSize * 120) + "," + (rowSize * 100) + "," + 60 + ",I08,0038J0C,007K0C,006K0C,00E01801C,70F3084F8,FCFF007FB8,FF7F003FF8,FFBF803FF8,7FDF001FE8,6IF800FD8,67FFE01FF,3B7FE01FE,1JF01F8,03IF81C,001FFC1C,I0FFE1C,I0FFE0C,:020FFE,020FFCF6,040FFDFFN03IFC,0447FDFFM07KFC,0447FF7EL07MF8,0C43FB3FK03NFE,0C6173FFJ01KFE07FF,0C2137FFJ07IFEJ01FC,0C3017FFI03IFCL03E,0C1803F8001IFCN0F,060E01F801IFCO038,07J0F00IFCP01C,038K0IFER04,03EI01IFES02,00NF,007LF,001KF,I01FFC," + "^FS\n^XZ\n";                                           // End of label

        }
        private void button2_Click(object sender, EventArgs e)
        {
            currentStickers = new BindingList<Sticker>();
            try
            {
                int val = unit;
                if (unitPerMono != 0) val = unitPerMono;
                getStickerHeightWidth("Unit");
                for (int i = 1; i <= val; i++)
                {
                    addSticker("Unit");

                }
                remainingUnit -= val;
                if (unitPerMono != 0)
                {
                    getStickerHeightWidth("Mono");
                    addSticker("Mono");
                    remainingMono--;
                }
                if (monoPerPlan!=0 && remainingMono % monoPerPlan == 0)
                {
                    getStickerHeightWidth("Plan");
                    addSticker("Plan");
                    remainingPlan--;
                }
                updateRemainingSticker();
                if (remainingUnit == 0)
                {
                    panel3.Visible = false;
                    label4.Visible = true;
                    panel4.Visible = false;
                }
                else
                {
                    showPanel();
                    
                    Message.Custom("Insert Status", "Batch Generated Successfully", new Point(620, 80));
                }
                panel4.Visible = true;
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = currentStickers;
            }
            catch (Exception ex)
            {
                Message.Custom("Error", ex.Message, new Point(620, 80));
            }
            
        }

        private void addSticker(string category)
        {
            string message = "";
            stickerID =0;
            try
            {
                using (SqlConnection conn = new SqlConnection(Program.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("InsertStickerData", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@plant", plant);
                        cmd.Parameters.AddWithValue("@line", line);
                        cmd.Parameters.AddWithValue("@productName", productName);
                        cmd.Parameters.AddWithValue("@category", category);
                        
                        try
                        {
                            conn.Open();
                            using (var reader = cmd.ExecuteReader())
                            {
                                reader.Read();
                                message = reader.GetString(0);         // "success" or error
                                stickerID = reader.GetInt32(1);          // new ID

                            }
                            if (message == "success")
                            {
                                updateSticker(stickerID);
                                currentStickers.Add(new Sticker
                                {
                                    StickerID = stickerID,
                                    Plant = plant,
                                    Line = line,
                                    ProductName = productName,
                                    Category = category
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            Message.Custom("Insert Status", ex.Message, new Point(620, 80));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Message.Custom("Error", ex.Message, new Point(620, 80));
            }
            
        }
        private void getStickerData()
        {

            using (SqlConnection conn = new SqlConnection(Program.connectionString))
            using (SqlCommand cmd = new SqlCommand("fetchSingleProduct", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productName", productName);

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string message = reader.GetString(reader.GetOrdinal("message"));
                            if (message != "success")
                            {
                                Message.Custom("Error", message, new Point(620, 80));
                            }
                        }

                        if (reader.NextResult() && reader.Read())
                        { 
                            mrp = reader["mrp"].ToString();
                            description1 = reader["description1"].ToString();
                            description2= reader["description2"].ToString();
                            description3 = reader["description3"].ToString();
                            weight= reader["weight"].ToString();
                            weightUnit= reader["weightUnit"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Message.Custom("Fetch Error", ex.Message, new Point(620, 80));
                }
            }
        }
        private void updateSticker(int stickerID)
        {
            string stickerValue = CreateZPL();
            string message = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(Program.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateStickerData", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@stickerID", stickerID);
                        cmd.Parameters.AddWithValue("@sticker", stickerValue);
                        cmd.Parameters.AddWithValue("@barcode", stickerID.ToString());

                        try
                        {
                            conn.Open();
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    message = reader.GetString(0);
                                }
                            }

                            if (message != "success") Message.Custom("Insert Status", message, new Point(620, 80));

                        }
                        catch (Exception ex)
                        {
                            Message.Custom("Insert Status", ex.Message, new Point(620, 80));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Message.Custom("Error", ex.Message, new Point(620, 80));
            }
        }
        private void updateRemainingSticker()
        {
            using (SqlConnection conn = new SqlConnection(Program.connectionString))
            using (SqlCommand cmd = new SqlCommand("updateRemainingPlanData", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productName", productName);
                cmd.Parameters.AddWithValue("@plant", plant);
                cmd.Parameters.AddWithValue("@line",line);
                cmd.Parameters.AddWithValue("@remainingUnit",remainingUnit);
                cmd.Parameters.AddWithValue("@remainingMono",remainingMono);
                cmd.Parameters.AddWithValue("@remainingPlan",remainingPlan);
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string message = reader.GetString(reader.GetOrdinal("message"));
                            if (message != "success")
                            {
                                Message.Custom("Error", message, new Point(620, 80));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Message.Custom("Fetch Error", ex.Message, new Point(620, 80));
                }
            }
        }

        private void getStickerHeightWidth(String category)
        {
            using (SqlConnection conn = new SqlConnection(Program.connectionString))
            using (SqlCommand cmd = new SqlCommand("fetchStickerHeightWidth", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@plant",plant);
                cmd.Parameters.AddWithValue("@line", line);
                cmd.Parameters.AddWithValue("@category",category);

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string message = reader.GetString(reader.GetOrdinal("message"));
                            if (message != "success")
                            {
                                Message.Custom("Error", message, new Point(620, 80));
                            }
                        }

                        if (reader.NextResult() && reader.Read())
                        {
                            height = reader["height"].ToString();
                            width = reader["width"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Message.Custom("Fetch Error", ex.Message, new Point(620, 80));
                }
            }
        }
    }
}
