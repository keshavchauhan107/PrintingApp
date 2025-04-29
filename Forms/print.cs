using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrintingApp.Forms
{
    public partial class print: Form
    {
        public print()
        {
            InitializeComponent();
        }
        private void PrintSticker(string zplCode, string name, string category)
        {
            byte[] zpl = Encoding.UTF8.GetBytes(zplCode);

            // adjust print density (8dpmm), label width (4 inches), label height (6 inches), and label index (0) as necessary
            var request = (HttpWebRequest)WebRequest.Create("http://api.labelary.com/v1/printers/24dpmm/labels/0.99x0.41/0/");
            if (category == "Mono")
            {
                request = (HttpWebRequest)WebRequest.Create("http://api.labelary.com/v1/printers/24dpmm/labels/1.32x0.58/0/");
            }
            if (category == "Plan")
            {
                request = (HttpWebRequest)WebRequest.Create("http://api.labelary.com/v1/printers/24dpmm/labels/1.65x0.75/0/");
            }
            request.Method = "POST";
            //request.Accept = "application/pdf"; // omit this line to get PNG images back
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = zpl.Length;

            var requestStream = request.GetRequestStream();
            requestStream.Write(zpl, 0, zpl.Length);
            requestStream.Close();

            try
            {
                string today = DateTime.Now.ToString("yyyy-MM-dd");
                Directory.CreateDirectory("Barcodes/" + today);
                string filePath = Path.Combine("Barcodes/" + today, name + ".png");
                var response = (HttpWebResponse)request.GetResponse();
                var responseStream = response.GetResponseStream();
                var fileStream = System.IO.File.Create(filePath); // change file name for PNG images
                responseStream.CopyTo(fileStream);
                responseStream.Close();
                fileStream.Close();
            }
            catch (WebException e)
            {
                Console.WriteLine("Error: {0}", e.Status);
            }
        }

        
    }
}
