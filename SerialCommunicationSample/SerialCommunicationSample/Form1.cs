using System.Diagnostics;

namespace SerialCommunicationSample
{
    public partial class Form1 : Form
    {
        CLib_FL_PR6.FL_PR6 port;
        public Form1()
        {
            InitializeComponent();

            CLib_FL_PR6.FL_PR6.AddLogAction = LogAdd;
            port = new CLib_FL_PR6.FL_PR6();
            port.Init("COM5");
            port.Open();
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            string reply = "";
            string send = textBox_send.Text;
            if (send == "")
            {
                return;
            }

            if (!port.SendRead(send, ref reply))
            {
                LogAdd("timeout");
            }
            else
            {
                LogAdd("Get Msg : " + reply);
            }
        }

        private void LogAdd(string log)
        {
            Invoke(new Action(() => 
            {
                textBox_receive.AppendText(log + "\r\n");
            }));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            port.Close();
        }
    }
}