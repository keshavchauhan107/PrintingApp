using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrintingApp
{
    
    static class Program
    {
        public static DateTime AppStartDate = DateTime.Today;
        public static string  connectionString = "Server=DESKTOP-K5H17TC\\MSSQLSERVER01;Database=PrintBarcode;Trusted_Connection=True;TrustServerCertificate=True";
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    public static class Message
    {
        public static void Custom(string title, string message, Point location)
        {
            Form msgForm = new Form();
            msgForm.StartPosition = FormStartPosition.Manual;
            msgForm.Location = location;
            msgForm.Size = new Size(320, 160);
            msgForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            msgForm.Text = title;
            msgForm.MaximizeBox = false;
            msgForm.MinimizeBox = false;
            msgForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            msgForm.ShowInTaskbar = false;

            Label lbl = new Label();
            lbl.Text = message;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Font = new Font("Segoe UI", 12F);
            lbl.Size = new Size(msgForm.ClientSize.Width - 20, 80);
            lbl.Location = new Point(10, 10);
            msgForm.Controls.Add(lbl);

            Button btnOK = new Button();
            btnOK.Text = "OK";
            btnOK.Size = new Size(80, 28);
            btnOK.Location = new Point((msgForm.ClientSize.Width - btnOK.Width) / 2, msgForm.ClientSize.Height - btnOK.Height - 2);
            btnOK.Click += (s, args) => msgForm.Close();
            msgForm.Controls.Add(btnOK);

            msgForm.ShowDialog();
        }
    }
}
