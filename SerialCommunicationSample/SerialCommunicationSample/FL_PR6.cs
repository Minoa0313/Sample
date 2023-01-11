using System.Diagnostics;
using System.IO.Ports;

namespace CLib_FL_PR6
{
    public class FL_PR6
    {
        private const string EndChar = "[CR][LF]";

        /// <summary>
        /// 通信ポート
        /// </summary>
        private SerialPort port1;

        /// <summary>
        /// 受信キュー
        /// </summary>
        private Queue<string> received = new Queue<string>();

        public static Action<string> AddLogAction = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FL_PR6()
        {
            port1 = new SerialPort();
        }

        /// <summary>
        /// 通信初期化
        /// </summary>
        /// <param name="portname"></param>
        public void Init(string portname)
        {
            port1.BaudRate = 9600;
            port1.Parity = Parity.None;
            port1.DataBits = 8;
            port1.StopBits = StopBits.One;
            port1.PortName = portname;
            port1.NewLine = ">";    // 書き込み機は>を使う
            port1.DataReceived += new SerialDataReceivedEventHandler(Port1_DataReceived);
        }

        /// <summary>
        /// comport 状態
        /// </summary>
        /// <returns>true: open false : close</returns>
        public bool IsOpen()
        {
            return port1.IsOpen;
        }

        public bool Open()
        {
            try
            {
                if (!port1.IsOpen)
                {
                    port1.Open();
                }

                return true;
            }
            catch (Exception ex)
            {
                AddLogAction(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// シリアルポートを閉じる
        /// </summary>
        public void Close()
        {
            port1.Close();
        }

        public void Dispose()
        {
            port1.DataReceived -= new SerialDataReceivedEventHandler(Port1_DataReceived);
            port1.Dispose();
        }

 
        /// <summary>
        /// 送信共通関数
        /// </summary>
        /// <param name="msg">送信データ</param>
        /// <returns>送信結果</returns>
        private bool Send(string msg)
        {
            if (!port1.IsOpen)
            {
                return false;
            }

            byte[] cmddata = System.Text.Encoding.ASCII.GetBytes(ControlCharReplace(msg + EndChar));
            port1.Write(cmddata, 0, cmddata.Length);
            AddLogAction("Send : " + msg);
            return true;
        }

        /// <summary>
        /// 送信後受信
        /// </summary>
        /// <param name="msg">送信内容</param>
        /// <param name="reply">受信内容</param>
        /// <param name="timeout">タイムアウト時間</param>
        /// <returns>true : 受信OK </returns>
        public bool SendRead(string msg, ref string reply, int timeout = 2000)
        {
            if (!port1.IsOpen)
            {
                return false;
            }

            byte[] cmddata = System.Text.Encoding.ASCII.GetBytes(ControlCharReplace(msg + EndChar));
            string dbgStr = System.Text.Encoding.ASCII.GetString(cmddata);
            port1.Write(cmddata, 0, cmddata.Length);
            AddLogAction("Send : " + msg);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds <= timeout)
            {
                if (GetReply(ref reply))
                {
                    break;
                }
            }

            bool ret = sw.ElapsedMilliseconds <= timeout;

            if (!ret)
            {
                AddLogAction("Received timeout.");
            }

            return ret;
        }

        /// <summary>
        /// 受信
        /// </summary>
        /// <param name="reply">受信内容</param>
        /// <returns>true : 受信OK</returns>
        private bool GetReply(ref string reply)
        {
            Thread.Sleep(1);

            if (received.Count == 0)
            {
                return false;
            }

            reply = received.Peek();
            received.Dequeue();
            return true;
        }

        private void Port1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string str = port1.ReadLine();
            received.Enqueue(str);
            // AddLogAction("Received : " + str);
        }

        private string ControlCharReplace(string data)
        {
            Dictionary<string, char> chrList = ControlCharList();
            foreach (string key in chrList.Keys)
            {
                data = data.Replace(key, chrList[key].ToString());
            }

            return data;
        }

        private Dictionary<string, char> ControlCharList()
        {
            Dictionary<string, char> ctr = new Dictionary<string, char>();
            ctr.Add("[NUL]", '\u0000');
            ctr.Add("[SOH]", '\u0001');
            ctr.Add("[STX]", '\u0002');
            ctr.Add("[ETX]", '\u0003');
            ctr.Add("[EOT]", '\u0004');
            ctr.Add("[ENQ]", '\u0005');
            ctr.Add("[ACK]", '\u0006');
            ctr.Add("[BEL]", '\u0007');
            ctr.Add("[BS]", '\u0008');
            ctr.Add("[HT]", '\u0009');
            ctr.Add("[LF]", '\u000A');
            ctr.Add("[VT]", '\u000B');
            ctr.Add("[FF]", '\u000C');
            ctr.Add("[CR]", '\u000D');
            ctr.Add("[SO]", '\u000E');
            ctr.Add("[SI]", '\u000F');
            ctr.Add("[DLE]", '\u0010');
            ctr.Add("[DC1]", '\u0011');
            ctr.Add("[DC2]", '\u0012');
            ctr.Add("[DC3]", '\u0013');
            ctr.Add("[DC4]", '\u0014');
            ctr.Add("[NAK]", '\u0015');
            ctr.Add("[SYN]", '\u0016');
            ctr.Add("[ETB]", '\u0017');
            ctr.Add("[CAN]", '\u0018');
            ctr.Add("[EM]", '\u0019');
            ctr.Add("[SUB]", '\u001A');
            ctr.Add("[ESC]", '\u001B');
            ctr.Add("[FS]", '\u001C');
            ctr.Add("[GS]", '\u001D');
            ctr.Add("[RS]", '\u001E');
            ctr.Add("[US]", '\u001F');
            ctr.Add("[DEL]", '\u007F');
            ctr.Add("[0]", '\u0000');
            ctr.Add("[1]", '\u0001');
            ctr.Add("[2]", '\u0002');
            ctr.Add("[3]", '\u0003');
            ctr.Add("[4]", '\u0004');
            ctr.Add("[5]", '\u0005');
            ctr.Add("[6]", '\u0006');
            ctr.Add("[7]", '\u0007');
            return ctr;
        }
    }
}