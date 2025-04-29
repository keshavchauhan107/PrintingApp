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

namespace PrintingApp
{
    public partial class addPlan: Form
    {
        List<string> products = new List<string>();

        public addPlan()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.addPlan_Load);
            comboBox3.TextChanged += comboBox3_TextChanged;

        }
        private void addPlan_Load(object sender, EventArgs e)
        {
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
            //for(int i = 0; i < products.Count; i++)
            //{
            //    comboBox3.Items.Add(products[i]);
            //}
            comboBox3.Items.AddRange(products.ToArray());
            dateTimePicker1.Value = DateTime.Today;
            LoadPlans(dateTimePicker1.Value.Date);

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


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LoadPlans(dateTimePicker1.Value.Date);
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
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (e.g., Backspace), and digits only
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Block the key
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null || comboBox3.SelectedItem == null || textBox1.Text == "")
                {
                    Message.Custom(Text, "Please fill all fields", new Point(620, 80));
                    return;
                }
                string plant = comboBox1.SelectedItem.ToString();
                string line = comboBox2.SelectedItem.ToString();
                string productName = comboBox3.SelectedItem.ToString();
                int unit = Convert.ToInt32(textBox1.Text);
                if (unit == 0)
                {
                    Message.Custom(Text, "Unit Cannot be Zero", new Point(620, 80));
                    return;
                }
                int unitPerMono = 0, monoPerPlan = 0;
                if (textBox2.Text != "")
                {
                    unitPerMono = Convert.ToInt32(textBox2.Text);
                    if (unit % unitPerMono != 0)
                    {
                        Message.Custom("Error", "unitPerMono must be a factor of unit ",new Point(620,80));
                        return;
                    }
                    if (textBox3.Text != "")
                    {
                        monoPerPlan = Convert.ToInt32(textBox3.Text);
                        if (unitPerMono%monoPerPlan!= 0)
                        {
                            Message.Custom("Error", "monoPerPlan must be a factor of unitPerMono ", new Point(620, 80));
                            return;
                        }
                    }
                }
               
                using (SqlConnection conn = new SqlConnection(Program.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("addPlanData", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@plant", plant);
                        cmd.Parameters.AddWithValue("@line", line);
                        cmd.Parameters.AddWithValue("@productName", productName);
                        cmd.Parameters.AddWithValue("@unit", unit);
                        cmd.Parameters.AddWithValue("@unitPerMono", unitPerMono);
                        cmd.Parameters.AddWithValue("@monoPerPlan", monoPerPlan);
                        try
                        {
                            conn.Open();
                            string result = (string)cmd.ExecuteScalar();
                            if (result != "success")
                            {
                                Message.Custom("Insert Status", result, new Point(620, 80));
                                return;
                            }
                            else
                            {
                                dateTimePicker1.Value = DateTime.Today;
                                LoadPlans(dateTimePicker1.Value.Date);
                                Message.Custom("Message","Successfully Added", new Point(620, 80));
                            }
                        }
                        catch (Exception ex)
                        {
                            Message.Custom("Insert Status", ex.Message, new Point(620, 80));
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Message.Custom("Error", ex.Message, new Point(620, 80));
            }
            
        }

        private void LoadPlans(DateTime selectedDate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Program.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetPlanData", conn))
                    {

                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@selectedDate",selectedDate);
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
                                    dr["productName"],
                                    dr["unit"],
                                    dr["unitPerMono"],
                                    dr["monoPerPlan"]
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
            catch (Exception ex)
            {
                Message.Custom("Error", ex.Message, new Point(620, 80));
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
    }
}
