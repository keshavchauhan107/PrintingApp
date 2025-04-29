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
    public partial class addProduct: Form
    {
        public addProduct()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
            this.Load += new System.EventHandler(this.addProduct_Load);
        }
        private void addProduct_Load(object sender, EventArgs e)
        {
            LoadProductsIntoGrid();

        }
        private void LoadProductsIntoGrid()
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

                            dataGridView1.Rows.Clear();

                            // Example: pulling from a DataTable or any collection
                            foreach (DataRow dr in dt.Rows)
                            {
                                dataGridView1.Rows.Add(
                                    dr["productName"],
                                    dr["description1"],
                                    dr["description2"],
                                    dr["description3"],
                                    dr["mrp"],
                                    dr["weight"],
                                    dr["weightUnit"]
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
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(productGenericName.Text) ||
                    string.IsNullOrWhiteSpace(mrp.Text) ||
                    string.IsNullOrWhiteSpace(weight.Text) ||
                    weightUnit.SelectedItem == null)
                {
                    Message.Custom("Message", "Please fill all fields", new Point(620, 80));
                    return;
                }
                string productName = productGenericName.Text.ToString();
                string descrip1 = description1.Text.ToString();
                string descrip2 = description2.Text.ToString();
                string descrip3 = description3.Text.ToString();
                int productMRP = Convert.ToInt32(mrp.Text.ToString());
                int productWeight = Convert.ToInt32(weight.Text.ToString());
                string productweightUnit = weightUnit.SelectedItem.ToString();




                using (SqlConnection conn = new SqlConnection(Program.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("addProduct", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@productName", productName);
                        cmd.Parameters.AddWithValue("@description1", descrip1);
                        cmd.Parameters.AddWithValue("@description2", descrip2);
                        cmd.Parameters.AddWithValue("@description3", descrip3);
                        cmd.Parameters.AddWithValue("@mrp", productMRP);
                        cmd.Parameters.AddWithValue("@weight", productWeight);
                        cmd.Parameters.AddWithValue("@weightUnit", productweightUnit);

                        try
                        {
                            conn.Open();
                            string result = (string)cmd.ExecuteScalar(); // Gets the returned message
                                                                         //MessageBox.Show(result, "Insert Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadProductsIntoGrid();
                            Message.Custom("Insert Status", result, new Point(300, 80));
                        }
                        catch (Exception ex)
                        {
                            Message.Custom("Insert Status", ex.Message, new Point(300, 80));
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Message.Custom("Error", ex.Message, new Point(620, 80));
            }

            
        }
    }
}
