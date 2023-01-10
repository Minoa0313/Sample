using NationalInstruments.Visa;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace GetWaveformSample
{
    public partial class Form1 : Form
    {
        private const int RecordLength = 125000;
        private float yzero, yoff, ymult, xincr = 0;
        
        ResourceManager rm;
        MessageBasedSession TekScope;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                rm = new ResourceManager();

                // �����Ŏ����̃I�V����USB ID�ɕύX����
                TekScope = (MessageBasedSession)rm.Open("USB0::0x0699::0x03A4::C042015::INSTR");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private void button_send_Click(object sender, EventArgs e)
        {
            string send = textBox_send.Text;

            if (send == "")
            {
                return;
            }

            textBox_receive.Text = "";
            TekScope.RawIO.Write(send);

            if (send.Contains("?"))
            {
                string receive = TekScope.RawIO.ReadString();
                textBox_receive.Text = receive;
            }
        }

        private void button_get_waveform_Click(object sender, EventArgs e)
        {
            /* �擾���e�t�H�[�}�b�g
             * ��F#125000.......LF
             */
            int bufferSize = RecordLength + RecordLength.ToString().Length + 3;
            int ch = 1;

            GetParam(RecordLength, ch);

            TekScope.RawIO.Write("curve?");
            byte[] buffer = TekScope.RawIO.Read(bufferSize);
            Debug.WriteLine(buffer.Length);

            // �擪��LF�����������f�[�^�� rawwave �ɕۑ�����
            byte[] rawwave = new byte[RecordLength];
            
            for (int j = RecordLength.ToString().Length + 2; j < buffer.Length - 1; j++)
            {
                rawwave[j - RecordLength.ToString().Length - 2] = buffer[j];
            }

            // �擾�����f�[�^��d���f�[�^�ɕϊ�����
            float[] wave = new float[RecordLength];
            float[] timepoint = new float[RecordLength];

            for (int j = 0; j < rawwave.Length; j++)
            {
                timepoint[j] = j * xincr;
                wave[j] = (rawwave[j] - yoff) * ymult + yzero;
            }

            // 125000�� �f�[�^�͑������āAexcel�Ń`���[�g�ɂȂ�Ȃ��̂ŁA
            // 100���ɕ��ς���
            float[] wave2 = new float[RecordLength];
            float[] timepoint2 = new float[RecordLength];
            int zoom = 100;

            wave2 = new float[rawwave.Length / zoom];
            timepoint2 = new float[rawwave.Length / zoom];

            for (int j = 0; j < rawwave.Length; j += zoom)
            {
                timepoint2[j / zoom] = timepoint.Skip(j).Take(zoom).Average() + xincr * zoom / 2;
                wave2[j / zoom] = wave.Skip(j).Take(zoom).Average();
            }

            // write waveform to a csv file
            System.IO.StreamWriter file = new System.IO.StreamWriter(string.Format("test-CH{0}-{1}.csv", ch, DateTime.Now.ToString("yyMMdd-HHmmss")));
            file.WriteLine("S,V");
            for (int j = 0; j < wave2.Length; j++)
            {
                file.WriteLine(string.Format("{0},{1}", timepoint2[j], wave2[j]));
            }

            file.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            TekScope.Dispose();
            rm.Dispose();
        }

        /// <summary>
        /// curve?�ԐM�f�[�^����Volt�ɕύX����K�v�ȃp�����[�^���擾
        /// </summary>
        /// <param name="length"></param>
        /// <param name="ch"></param>
        /// <returns></returns>
        private bool GetParam(int length, int ch)
        {
            string response;

            ymult = 0;
            yoff = 0;
            yzero = 0;
            xincr = 0;

            try
            {
                TekScope.RawIO.Write("data:sou ch" + ch.ToString());             
                TekScope.RawIO.Write(":header off");
                TekScope.RawIO.Write("wfmoutpre:composition composite_yt"); // composition
                TekScope.RawIO.Write("data:resolution full"); // resolution

                TekScope.RawIO.Write("data:start 1");
                TekScope.RawIO.Write("horizontal:acqlength?");
                string temp = TekScope.RawIO.ReadString().Trim();
                TekScope.RawIO.Write("data:stop " + temp.ToString());
                TekScope.RawIO.Write("data:width 1"); // 
                TekScope.RawIO.Write("wfmoutpre:enc rpb"); // data format

                TekScope.RawIO.Write("wfmoutpre:ymult?");
                response = TekScope.RawIO.ReadString();
                ymult = float.Parse(response);
                Debug.WriteLine("ymult = " + response);

                TekScope.RawIO.Write("wfmoutpre:yzero?");
                response = TekScope.RawIO.ReadString();
                yzero = float.Parse(response);
                Debug.WriteLine("yzero = " + response);

                TekScope.RawIO.Write("wfmoutpre:yoff?");
                response = TekScope.RawIO.ReadString();
                yoff = float.Parse(response);
                Debug.WriteLine("yoff = " + response);

                TekScope.RawIO.Write("wfmoutpre:xincr?");
                response = TekScope.RawIO.ReadString();
                xincr = float.Parse(response);
                Debug.WriteLine("xincr = " + response);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
    }
}