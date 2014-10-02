using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Snap2Share
{
    public partial class Result : Form
    {
        public Result(string url)
        {
            InitializeComponent();

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Response));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(url));
            Response obj = (Response)ser.ReadObject(stream);

            LinkLabel.Link link2 = new LinkLabel.Link();
            link2.LinkData = obj.Bericht;
            linkLabel2.Links.Add(link2);

            Clipboard.SetText(obj.Bericht);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }
    }

    public class Response
    {
        public string Bericht { get; set; }
    }
}
