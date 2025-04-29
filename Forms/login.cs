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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PrintingApp
{
    public partial class login: Form
    {
        public login()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.login_Load);
        }
        private void login_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            // 3) Pass the list into Form1
            var form1 = new Form1
            {
                // Assume Form1 has a public property like:
                //   public List<string> Plants { get; set; }
                //Plants = {"Plant1","Plant2"}
            };
            form1.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            string email = textBox6.Text;
            string password = textBox7.Text;
            if (email == "" && password == "")
            {
                Message.Custom("Insert Status", "Please Enter Email and Password", new Point(300, 80));
                return;
            }
            else if (email == "")
            {
                Message.Custom("Insert Status", "Please Enter Email", new Point(300, 80));
                return;
            }
            else if (password == "")
            {
                Message.Custom("Insert Status", "Please Enter Password", new Point(300, 80));
                return;
            }
            string connectionString = "Server=DESKTOP-K5H17TC\\MSSQLSERVER01;Database=PrintBarcode;Trusted_Connection=True;TrustServerCertificate=True"; // Replace with your actual connection string

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("fetchUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // 1) Read the login message
                        if (reader.Read())
                        {
                            var result = reader["message"].ToString();
                            Message.Custom("Login Status", result, new Point(300, 80));

                            // If invalid credentials, we can bail out early
                            if (!string.Equals(result, "success", StringComparison.OrdinalIgnoreCase))
                                return;
                        }

                        // 2) Move to the second result set (the plants list)
                        List<string> plantNames = new List<string>();
                        if (reader.NextResult())
                        {
                            // Loop through all rows of the second result set
                            while (reader.Read())
                            {
                                // Adjust the column name to match your SELECT: "plantName"
                                plantNames.Add(reader["plantName"].ToString());
                            }
                        }

                        // 3) Pass the list into Form1
                        var form1 = new Form1
                        {
                            // Assume Form1 has a public property like:
                            //   public List<string> Plants { get; set; }
                            //Plants = plantNames
                        };
                        form1.Show();
                    }
                }
                catch (Exception ex)
                {
                    Message.Custom("Error", ex.Message, new Point(300, 80));
                }
            }

        }
    }
}
