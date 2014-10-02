using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace Snap2Share
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void NewSnapshot()
        {
            ScreenCapture sc = new ScreenCapture();
            Image img = sc.CaptureScreen();
            this.pictureBox1.Image = img;
            img.Save("capture.png", ImageFormat.Png);
            //MessageBox.Show("File is saved to disk");

            // Read file data
            FileStream fs = new FileStream("capture.png", FileMode.Open, FileAccess.Read);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, data.Length);
            fs.Close();

            // Generate post objects
            Dictionary<string, object> postParameters = new Dictionary<string, object>();
            postParameters.Add("filename", "capture.png");
            postParameters.Add("fileformat", "png");
            postParameters.Add("mfile", new FIleUpload.FileParameter(data, "capture.png", "image/png"));

            // Create request and receive response
            string postURL = "http://www.snap2share.nl/apiupload";
            string userAgent = "Snap2Share .NET 4.5";
            HttpWebResponse webResponse = FIleUpload.MultipartFormDataPost(postURL, userAgent, postParameters);

            // Process response
            try
            {
                StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
                string fullResponse = responseReader.ReadToEnd();
                webResponse.Close();

                new Result(fullResponse).Show();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Couldn't connect to server");
                //webResponse.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewSnapshot();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox1().Show();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewSnapshot();
        }
    }
}
