using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientNetwork
{
    public partial class Form1 : Form
    {
       

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                s.Connect("127.0.0.1", 8005);
                if (s.Connected)
                {
                    this.BackColor = Color.Green;
                    String strSend = textBox1.Text;
                    s.Send(Encoding.Unicode.GetBytes(strSend));
                    byte[] buffer = new byte[1024];
                    int l = 0;
                    do
                    {
                        l = s.Receive(buffer);
                        button1.Text += Encoding.Unicode.GetString(buffer, 0, l);
                    } while (l > 0);
                }
                else
                {
                    MessageBox.Show("Error!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
