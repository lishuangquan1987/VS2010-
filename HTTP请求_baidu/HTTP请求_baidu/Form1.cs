using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace HTTP请求_baidu
{
    public partial class Form1 : Form
    {
        string url = "http://www.baidu.com";
        string txt_header = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string result = HTTPGet(url);
            MessageBox.Show(result);
            MessageBox.Show(txt_header);
        }
        public string HTTPGet(string Url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "GET";
            request.ProtocolVersion = new Version(1, 1);
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            foreach (var item in response.Headers)
            {
                this.txt_header += item.ToString() + ";" + response.GetResponseHeader(item.ToString()) + System.Environment.NewLine;

            }
            File.WriteAllText("test.HTML", txt_header);
            if (response.ContentLength <= 0)
            {
                return null;

            }
            byte[] bytes;
            using (Stream stream = response.GetResponseStream())
            {
                int totalLength = (int)response.ContentLength;
                int numBytesRead = 0;
                bytes = new byte[totalLength + 1024];
                while (numBytesRead < totalLength)//循环读取response.GetResponseStream()的值，每次最大长度为1024，
                {
                    int num = stream.Read(bytes, numBytesRead, 1024);
                    if (num == 0)
                        break;
                    numBytesRead += num;
                }
            }
            string content = Encoding.UTF8.GetString(bytes);
            File.AppendAllText("test.HTML", content);
            return content;

        }
    }
}
