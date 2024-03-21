using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace _123123Client
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private string localIp = "192.168.0.105";
        private string remoteIp;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            remoteIp = textBox3.Text;
            client = new TcpClient(remoteIp, 12345);
            stream = client.GetStream();

            Thread receiveThread = new Thread(ReceiveMessages);
            receiveThread.Start();
        }

        private void ReceiveMessages()
        {
            while (true)
            {
                byte[] data = new byte[1024];
                int bytesRead = stream.Read(data, 0, data.Length);
                string message = Encoding.UTF8.GetString(data, 0, bytesRead);

                Invoke((MethodInvoker)delegate
                {
                    textBox2.Text += $"{remoteIp}: " + message + Environment.NewLine;
                });
            }
        }
        

     

        private void button2_Click(object sender, EventArgs e)
        {
            string message = textBox1.Text; 

            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);

            string historyFileName = $"{localIp}_{remoteIp}_chat_history.txt";


            textBox2.Text += $"{localIp}:" + message + Environment.NewLine;
        }
    }
}
