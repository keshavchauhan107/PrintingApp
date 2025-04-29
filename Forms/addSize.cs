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

namespace PrintingApp.Forms
{
    public partial class addSize: Form
    {

        public addSize()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.addSize_Load);
        }
        private void addSize_Load(object sender, EventArgs e)
        {
            // Load data into the DataGridView
            barcodeData(sender, e);
            checkedComboBox1.AddItem("Plant 1");
            checkedComboBox1.AddItem("Plant 2");
            checkedComboBox1.AddItem("Plant 3");
            checkedComboBox1.AddItem("Plant 4");
            checkedComboBox2.AddItem("Line 1");
            checkedComboBox2.AddItem("Line 2");
            checkedComboBox2.AddItem("Line 3");
            checkedComboBox2.AddItem("Line 4");
            checkedComboBox2.AddItem("Line 5");
            checkedComboBox2.AddItem("Line 6");
            checkedComboBox2.AddItem("Line 7");
            checkedComboBox2.AddItem("Line 8");
            checkedComboBox2.AddItem("Line 9");
            checkedComboBox2.AddItem("Line 10");
            checkedComboBox3.AddItem("Unit");
            checkedComboBox3.AddItem("Mono");
            checkedComboBox3.AddItem("Plan");
        }
        public class CategoryMeasurement
        {
            public string Category { get; set; }
            public string Height { get; set; }
            public string Width { get; set; }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (e.g., Backspace), and digits only
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Block the key
            }
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (e.g., Backspace), and digits only
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Block the key
            }
        }
        private void barcodeData(object sender, EventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(Program.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetBarcodePrints", conn))
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

                        dataGridView1.Rows.Clear();

                        // Example: pulling from a DataTable or any collection
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridView1.Rows.Add(
                                dr["plant"],
                                dr["line"],
                                dr["category"],
                                dr["height"],
                                dr["width"]
                            );
                        }
                    }
                    catch (Exception ex)
                    {
                        Message.Custom("Insert Status", ex.Message, new Point(300, 80));
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                List<object> selectedPlant = checkedComboBox1.GetSelectedItems();
                List<object> selectedLine = checkedComboBox2.GetSelectedItems();
                List<object> selectedCategory = checkedComboBox3.GetSelectedItems();


                // Check if textBox1 is empty
                if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || selectedPlant.Count < 1 || selectedLine.Count < 1 || selectedCategory.Count < 1)
                {
                    Message.Custom("Insert Status", "Please fill all fields", new Point(520, 80));
                    return;
                }


                int height = Convert.ToInt32(textBox1.Text);
                int width = Convert.ToInt32(textBox2.Text);
                for (int i = 0; i < selectedPlant.Count; i++)
                {
                    for (int j = 0; j < selectedLine.Count; j++)
                    {
                        for (int k = 0; k < selectedCategory.Count; k++)
                        {
                            using (SqlConnection conn = new SqlConnection(Program.connectionString))
                            {
                                using (SqlCommand cmd = new SqlCommand("checkStickerSize", conn))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@plant", selectedPlant[i]);
                                    cmd.Parameters.AddWithValue("@category", selectedCategory[k]);
                                    cmd.Parameters.AddWithValue("@line", selectedLine[j]);
                                    try
                                    {
                                        conn.Open();
                                        string result = (string)cmd.ExecuteScalar();
                                        if (result != "success")
                                        {
                                            Message.Custom("Insert Status", result, new Point(620, 80));
                                            return;
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        Message.Custom("Insert Status", ex.Message, new Point(620, 80));
                                    }
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < selectedPlant.Count; i++)
                {
                    for (int j = 0; j < selectedLine.Count; j++)
                    {
                        for (int k = 0; k < selectedCategory.Count; k++)
                        {
                            using (SqlConnection conn = new SqlConnection(Program.connectionString))
                            {
                                using (SqlCommand cmd = new SqlCommand("insertStickerSize", conn))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@plant", selectedPlant[i]);
                                    cmd.Parameters.AddWithValue("@category", selectedCategory[k]);
                                    cmd.Parameters.AddWithValue("@line", selectedLine[j]);
                                    cmd.Parameters.AddWithValue("@height", height);
                                    cmd.Parameters.AddWithValue("@width", width);

                                    try
                                    {
                                        conn.Open();
                                        string result = (string)cmd.ExecuteScalar();
                                        Message.Custom("Insert Status", result, new Point(620, 80));

                                    }
                                    catch (Exception ex)
                                    {
                                        Message.Custom("Insert Status", ex.Message, new Point(620, 80));
                                    }
                                }
                            }
                        }
                    }
                }
                barcodeData(sender, e);
            }
            catch(Exception ex)
            {
                Message.Custom("Error", ex.Message, new Point(620, 80));
            }
            

        }

    }
}
