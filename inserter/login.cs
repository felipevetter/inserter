using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inserter
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {

        }
        private string getHWID()
        {
            var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
            ManagementObjectCollection mbsList = mbs.Get();
            string id = "";
            foreach (ManagementObject mo in mbsList)
            {
                id = mo["ProcessorId"].ToString();
                break;
            }
            return id;
        }

        public string strVar = "";
        public string strVar2 = "";

        private string MakeCall(string email, string pass)
        {
            string url = "http://fvsolutions.herokuapp.com/testUser";
            var request = (HttpWebRequest)WebRequest.Create(url);

            var postData = "email=" + Uri.EscapeDataString(email);
            postData += "&senha=" + Uri.EscapeDataString(pass);
            postData += "&nome=" + Uri.EscapeDataString(Environment.UserName);
            postData += "&hwid=" + Uri.EscapeDataString(getHWID());
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var response = MakeCall(guna2TextBox1.Text, guna2TextBox2.Text);
            if(response == "logado")
            {
                strVar = guna2TextBox1.Text;
                strVar2 = guna2TextBox2.Text;
                Form1 f1 = new Form1(this);
                f1.Show();
                this.Hide();
            }
            else if(response == "incorreto")
            {
                MessageBox.Show("Login ou senha incorretos!");
            } 
            else if(response == "pedidoacesso")
            {
                MessageBox.Show("Foi enviado uma verificação para você pelo site, por favor, verifique sua conta!");
            }
        }
    }
}
