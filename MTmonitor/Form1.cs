using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using NLog;

namespace MTmonitor
{
    // C++ 構造体のマーシャリング
    [StructLayout(LayoutKind.Sequential)]
    public struct FSI_DATA
    {
        public UInt16 id;    // unsigned short
        public Byte cam_id; //unsigned char
        public Byte fsi_pos;
        public Byte cmd;
        public Byte wdt;
        public Int16 mag;
        public Double t;
        public Single az;
        public Single alt;
        public Single vaz;
        public Single valt;
        public Single az_c;
        public Single alt_c;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MT_MONITOR_DATA
    {
        public Byte  id;        // unsigned char  Soft ID
        public Byte  obs;       // unsigned char  観測停止:0 それ以外frame No.
        public Byte  save;      // unsigned char  保存中:1/0
        public Int32 diskspace; // HDD残容量(MB)
    }

    public struct MT_Soft_Status
    {
        public int id;            // ID ラベル番号
        public string soft_name;  // IP address
        public string pc_name;    // PC name
        public PC_STATE pc_state; // PCの状態
    }
    
    public struct MT_PC_DATA
    {
        public int    id;         // ID ラベル番号
        public string ip;         // IP address
        public string pc_name;    // PC name
        public PC_STATE pc_state; // PCの状態
    }

    public enum PC_STATE
    {
        OK,
        NG
    }

    public enum MT_MON_ID
    {
        AFP,         //0
        KV1000MT2,   //1
        KV1000SpCam, //2
        MT3LrSpcam,  //3
        MT3Wide,     //4
        reserve5,
        reserve6,
        MT3NUV,       //7
        MT3Fine,      //8
        reserve9,
        MT2Wide,      //10
        MT2Echelle,   //11
        MT3SF,        //12
        KV1000SpCam2, //13
        FSI2,         //14
        MT3NIR=15,    //15
        MT3analog=20,
        PictureViewer=102,
    }

    public partial class Form1 : Form
    {
        // NLog
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        public static void NLogInfo(string message)
        {
            Logger.Info(message);
        }
        public static void NLogFatal(string message)
        {
            Logger.Fatal(message);
        }

        //状態を表す定数
        const int OFF = 0;
        const int ON  = 1;

        //Pingオブジェクト
        System.Net.NetworkInformation.Ping mainPing = null;

        int mmFsiUdpPortMTmonitor = 24415;
        private BackgroundWorker worker_udp, worker_ping;
        public const int pc_max_number = 10;
        MT_PC_DATA [] ping_pc_data = new MT_PC_DATA[pc_max_number] ;
        int ping_id     = 1;
        int ping_id_max = 6;

        public const int soft_max_number = 32;
        PC_STATE[] soft_alive_check = new PC_STATE[soft_max_number];
        Byte[] pre_Obs_Num = new Byte[soft_max_number];

        string WakeOnLanSoft = "C:\\Tool\\MagicSend.exe";

        public Form1()
        {
            InitializeComponent();

            worker_udp = new BackgroundWorker();
            worker_udp.WorkerReportsProgress = true;
            worker_udp.WorkerSupportsCancellation = true;
            worker_udp.DoWork += new DoWorkEventHandler(worker_udp_DoWork);
            worker_udp.ProgressChanged += new ProgressChangedEventHandler(worker_udp_ProgressChanged);

            // ping用
            worker_ping = new BackgroundWorker();
            worker_ping.WorkerReportsProgress = true;
            worker_ping.WorkerSupportsCancellation = true;
            worker_ping.DoWork += new DoWorkEventHandler(worker_ping_DoWork);
            worker_ping.ProgressChanged += new ProgressChangedEventHandler(worker_ping_ProgressChanged);

            NLogInfo("Start.");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.worker_udp.RunWorkerAsync();
            button1.Image = System.Drawing.Image.FromFile(@"Green_button.png");
            //DriveInfo cDrive = new DriveInfo("C");
            //MessageBox.Show(cDrive.TotalFreeSpace.ToString());

        }

        #region UDP
        // 別スレッド処理（UDP） //IP 192.168.1.211
        private void worker_udp_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = (BackgroundWorker)sender;

            //バインドするローカルポート番号
            int localPort = mmFsiUdpPortMTmonitor;
            System.Net.Sockets.UdpClient udpc = null; ;
            try
            {
                udpc = new System.Net.Sockets.UdpClient(localPort);
            }
            catch (Exception ex)
            {
                MessageBox.Show("worker_udp initializing failed. ("+ex.Message+")");
            }


            //文字コードを指定する
            System.Text.Encoding enc = System.Text.Encoding.UTF8;
            //データを送信するリモートホストとポート番号
            string remoteHost = "localhost";
            //string remoteHost = "192.168.1.206";
            int remotePort = localPort;
            //送信するデータを読み込む
            string sendMsg = "test送信するデータ";
            byte[] sendBytes = enc.GetBytes(sendMsg);

            //リモートホストを指定してデータを送信する
            udpc.Send(sendBytes, sendBytes.Length, remoteHost, remotePort);

            //データ　ID:short  soft:RUN/STOP  Obs:ON/OFF  Save:ON/OFF  Disk space:int
            string str;
            MT_MONITOR_DATA kmd3 = new MT_MONITOR_DATA();
            int size = Marshal.SizeOf(kmd3);

            //データを受信する
            System.Net.IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, localPort);
            while (bw.CancellationPending == false)
            {
                byte[] rcvBytes = udpc.Receive(ref remoteEP);
                if (rcvBytes.Length == size)
                {
                    kmd3 = ToStruct(rcvBytes);
                    bw.ReportProgress(0, kmd3);
                }
                else
                {
                    string rcvMsg = enc.GetString(rcvBytes);
                    str = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "受信したデータ:[" + rcvMsg + "]\n";
                    this.Invoke(new dlgSetString(ShowText), new object[] { textBox1, str });
                }

                str = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") +" IP:"+ remoteEP.Address + " Port:" + remoteEP.Port + " Size:" + rcvBytes.Length;
                this.Invoke(new dlgSetString(ShowLabel), new object[] { label_msg, str });
            }

            //UDP接続を終了
            udpc.Close();
        }
        //メインスレッドでの処理
        private void worker_udp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // 画面表示
            MT_MONITOR_DATA kmd3 = (MT_MONITOR_DATA)e.UserState;
            string s = string.Format("[ID:{0} Obs:{1} Save:{2} Disk:{3}MB]\n", kmd3.id, kmd3.obs, kmd3.save, kmd3.diskspace);
            textBox1.Text = s;
            Label label = null;
            switch (kmd3.id)
            {
                case (int)MT_MON_ID.AFP: //AFP
                    label = label_AFP;
                    timer_AFP.Stop(); timer_AFP.Start();
                    label.Text = "AFP\n" + (kmd3.diskspace).ToString() + "GB";
                    if (kmd3.obs == 0)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Orange_button.png");
                        label.ForeColor = Color.Black;
                    }
                    else if (kmd3.obs == 1)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Green_button.png");
                        label.ForeColor = Color.Black;
                    }
                    else
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Green_button.png");
                        label.ForeColor = Color.Red;
                    }
                    if (kmd3.diskspace <= 10)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Red_button.png");
                        label.ForeColor = Color.Black;
                    }
                    soft_alive_check[0] = PC_STATE.OK;
                    break;
                case (int)MT_MON_ID.KV1000MT2: //KV1000MT2
                    label = label_KV1000MT2;
                    timer_KV1000MT2.Stop(); timer_KV1000MT2.Start();
                    label.Text = "KV1000MT2\n" + (kmd3.diskspace).ToString() + "GB";
                    if (kmd3.obs == 0)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Orange_button.png");
                        label.ForeColor = Color.Black;
                    }
                    else if (kmd3.obs == 1)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Green_button.png");
                        label.ForeColor = Color.Black;
                    }
                    else
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Green_button.png");
                        label.ForeColor = Color.Red;
                    }
                    if (kmd3.diskspace <= 10)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Red_button.png");
                        label.ForeColor = Color.Black;
                    }
                    soft_alive_check[1] = PC_STATE.OK;
                    break;
                case (int)MT_MON_ID.KV1000SpCam: //KV1000SpCam
                    label = label_KV1000SpCam;
                    timer_KV1000SpCam.Stop(); timer_KV1000SpCam.Start();
                    label.Text = "KV1000SpCam\n"  + (kmd3.diskspace).ToString() + "GB";
                    if (kmd3.obs == 0)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Orange_button.png");
                        label.ForeColor = Color.Black;
                    }
                    else if (kmd3.obs == 1)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Green_button.png");
                        label.ForeColor = Color.Black;
                    }
                    else 
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Green_button.png");
                        label.ForeColor = Color.Red;
                    }
                    if (kmd3.diskspace <= 10)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Red_button.png");
                        label.ForeColor = Color.Black;
                    }
                    soft_alive_check[2] = PC_STATE.OK;
                    break;
                case (int)MT_MON_ID.KV1000SpCam2:
                    label = label_KV1000SpCam2;
                    timer_KV1000SpCam2.Stop(); timer_KV1000SpCam2.Start();
                    label.Text = "KV1000SpCam2\n" + (kmd3.diskspace).ToString() + "GB\n" + (kmd3.obs).ToString();
                    if (kmd3.obs == 0)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Orange_button.png");
                        label.ForeColor = Color.Black;
                    }
                    else if (kmd3.obs == 1)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Green_button.png");
                        label.ForeColor = Color.Black;
                    }
                    else
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Green_button.png");
                        label.ForeColor = Color.Red;
                    }
                    if (kmd3.diskspace <= 10)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Red_button.png");
                        label.ForeColor = Color.Black;
                    }
                    soft_alive_check[(int)MT_MON_ID.KV1000SpCam2] = PC_STATE.OK;
                    break;
                case (int)MT_MON_ID.MT3NUV:
                    label = label_NUV;
                    timer_MT3NUV.Stop(); timer_MT3NUV.Start();
                    label.Text = "MT3_NUV\n" + (kmd3.diskspace).ToString() + "GB\n" + (kmd3.obs).ToString();
                    if (kmd3.obs == 0)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Orange_button.png");
                        label.ForeColor = Color.Black;
                    }
                    else if (kmd3.obs == 1)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Green_button.png");
                        label.ForeColor = Color.Black;
                    }
                    else
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Green_button.png");
                        label.ForeColor = Color.Red;
                    }
                    if (kmd3.diskspace <= 10)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Red_button.png");
                        label.ForeColor = Color.Black;
                    }
                    soft_alive_check[3] = PC_STATE.OK;
                    break;
                case (int)MT_MON_ID.MT3Fine:
                    label = label_MT3Fine;
                    timer_MT3Fine.Stop(); timer_MT3Fine.Start();
                    label.Text = "MT3_Fine\n" + (kmd3.diskspace).ToString() + "GB\n" + (kmd3.obs).ToString();
                    if (kmd3.obs == 0)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Orange_button.png");
                        label.ForeColor = Color.Black;
                    }
                    else if (kmd3.obs == 1)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Green_button.png");
                        label.ForeColor = Color.Black;
                    }
                    else
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Green_button.png");
                        label.ForeColor = Color.Red;
                    }
                    if (kmd3.diskspace <= 10)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Red_button.png");
                        label.ForeColor = Color.Black;
                    }
                    soft_alive_check[4] = PC_STATE.OK;
                    break;
                case (int)MT_MON_ID.MT3LrSpcam:
                    timer_MT3IDS.Stop(); timer_MT3IDS.Start();
                    label_MT3IDS.Text = "MT3_LrSpcam\n" + (kmd3.diskspace).ToString() + "GB\n" + (kmd3.obs).ToString();
                    if (kmd3.obs == 0)
                    {
                        label_MT3IDS.Image = System.Drawing.Image.FromFile(@"Orange_button.png");
                        label_MT3IDS.ForeColor = Color.Black;
                    }
                    else if (kmd3.obs == 1)
                    {
                        label_MT3IDS.Image = System.Drawing.Image.FromFile(@"Green_button.png");
                        label_MT3IDS.ForeColor = Color.Black;
                    }
                    else
                    {
                        label_MT3IDS.Image = System.Drawing.Image.FromFile(@"Green_button.png");
                        label_MT3IDS.ForeColor = Color.Red;
                    }
                    if (kmd3.diskspace <= 10)
                    {
                        label_MT3IDS.Image = System.Drawing.Image.FromFile(@"Red_button.png");
                        label_MT3IDS.ForeColor = Color.Black;
                    }
                    soft_alive_check[5] = PC_STATE.OK;
                    break;
                case (int)MT_MON_ID.MT3Wide: //MT3Wide
                    timer_MT3Wide.Stop(); timer_MT3Wide.Start();
                    label_MT3Wide.Text = "MT3_Wide\n" + (kmd3.diskspace).ToString() + "GB\n" + (kmd3.obs).ToString();
                    if (kmd3.obs == 0)
                    {
                        label_MT3Wide.Image = System.Drawing.Image.FromFile(@"Orange_button.png");
                        label_MT3Wide.ForeColor = Color.Black;
                    }
                    else if (kmd3.obs == 1)
                    {
                        label_MT3Wide.Image = System.Drawing.Image.FromFile(@"Green_button.png");
                        label_MT3Wide.ForeColor = Color.Black;
                    }
                    else
                    {
                        label_MT3Wide.Image = System.Drawing.Image.FromFile(@"Green_button.png");
                        label_MT3Wide.ForeColor = Color.Red;
                    }
                    if (kmd3.diskspace <= 10)
                    {
                        label_MT3Wide.Image = System.Drawing.Image.FromFile(@"Red_button.png");
                        label_MT3Wide.ForeColor = Color.Black;
                    }
                    soft_alive_check[7] = PC_STATE.OK;
                    break;

                case (int)MT_MON_ID.MT3SF:
                    label = label_SF;
                    timer_PictureViewer.Stop(); timer_PictureViewer.Start();
                    label.Text = "MT3SF\n" + (kmd3.diskspace).ToString() + "GB\n" + (kmd3.obs).ToString();
                    if (kmd3.obs == 0)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Orange_button.png");
                        label.ForeColor = Color.Black;
                    }
                    else if (kmd3.obs == 1)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Green_button.png");
                        label.ForeColor = Color.Black;
                    }
                    else
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Green_button.png");
                        label.ForeColor = Color.Red;
                    }
                    if (kmd3.diskspace <= 10)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Red_button.png");
                        label.ForeColor = Color.Black;
                    }
                    soft_alive_check[(int)MT_MON_ID.MT3SF] = PC_STATE.OK;
                    break;


                case (int)MT_MON_ID.MT3NIR: //15
                    label = label_NIR;
                    timer_PictureViewer.Stop(); timer_PictureViewer.Start();
                    label.Text = "MT3NIR\n" + (kmd3.diskspace).ToString() + "GB\n" + (kmd3.obs).ToString();
                    if (kmd3.obs == 0)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Orange_button.png");
                        label.ForeColor = Color.Black;
                    }
                    else if (kmd3.obs == 1)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Green_button.png");
                        label.ForeColor = Color.Black;
                    }
                    else
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Green_button.png");
                        label.ForeColor = Color.Red;
                    }
                    if (kmd3.diskspace <= 10)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Red_button.png");
                        label.ForeColor = Color.Black;
                    }
                    soft_alive_check[(int)MT_MON_ID.MT3NIR] = PC_STATE.OK;
                    break;

                case (int)MT_MON_ID.MT2Wide:
                    label = label_MT2Wide;
                    timer_PictureViewer.Stop(); timer_PictureViewer.Start();
                    label.Text = "MT3 Analog\n" + (kmd3.diskspace).ToString() + "GB\n" + (kmd3.obs).ToString();
                    if (kmd3.obs == 0)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Orange_button.png");
                        label.ForeColor = Color.Black;
                    }
                    else if (kmd3.obs == 1)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Green_button.png");
                        label.ForeColor = Color.Black;
                    }
                    else
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Green_button.png");
                        label.ForeColor = Color.Red;
                    }
                    if (kmd3.diskspace <= 10)
                    {
                        label.Image = System.Drawing.Image.FromFile(@"Red_button.png");
                        label.ForeColor = Color.Black;
                    }
                    soft_alive_check[(int)MT_MON_ID.MT3analog] = PC_STATE.OK;
                    break;

            }
        }
        static byte[] ToBytes(MT_MONITOR_DATA obj)
        {
            int size = Marshal.SizeOf(typeof(MT_MONITOR_DATA));
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(obj, ptr, false);
            byte[] bytes = new byte[size];
            Marshal.Copy(ptr, bytes, 0, size);
            Marshal.FreeHGlobal(ptr);
            return bytes;
        }

        public static MT_MONITOR_DATA ToStruct(byte[] bytes)
        {
            GCHandle gch = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            MT_MONITOR_DATA result = (MT_MONITOR_DATA)Marshal.PtrToStructure(gch.AddrOfPinnedObject(), typeof(MT_MONITOR_DATA));
            gch.Free();
            return result;
        }
        //現在の時刻の表示と、タイマーの表示に使用されるデリゲート
        delegate void dlgSetString(object lbl, string text);
        //ボタンのカラー変更に使用されるデリゲート
        delegate void dlgSetColor(object lbl, int state);

        //デリゲートで別スレッドから呼ばれてラベルに現在の時間又は
        //ストップウオッチの時間を表示する
        private void ShowText(object sender, string str)
        {
            TextBox rtb = (TextBox)sender;　//objectをキャストする
            rtb.Text = str;
        }
        private void ShowLabel(object sender, string str)
        {
            Label rtb = (Label)sender;　//objectをキャストする
            rtb.Text = str;
        }
        private void SetColor(object sender, int sta)
        {
            Button rtb = (Button)sender;　//objectをキャストする
            if (sta == ON)
            {
                rtb.BackColor = Color.Red;
            }
            else if (sta == OFF)
            {
                rtb.BackColor = Color.FromKnownColor(KnownColor.Control);
            }
        }

        #endregion
        #region Ping
        void worker_ping_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            MT_PC_DATA mt_pc_data = (MT_PC_DATA)e.Argument;

            //Pingオブジェクトの作成
            if (mainPing == null)
            {
                mainPing = new System.Net.NetworkInformation.Ping();
            }
            //Pingを送信する
            System.Net.NetworkInformation.PingReply reply = mainPing.Send(mt_pc_data.ip);
            //結果を取得
            string st;
            if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
            {
                mt_pc_data.pc_state = PC_STATE.OK;
                st = string.Format("Reply from {0}:bytes={1} time={2}ms TTL={3}",
                    reply.Address, reply.Buffer.Length,
                    reply.RoundtripTime, reply.Options.Ttl);
            }
            else
            {
                mt_pc_data.pc_state = PC_STATE.NG;
                st = string.Format("Ping送信に失敗。({0})", reply.Status);
            }
            //string str = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + " IP:" + remoteEP.Address + " Port:" + remoteEP.Port + " Size:" + rcvBytes.Length;
            this.Invoke(new dlgSetString(ShowLabel), new object[] { label1, st });
            //mainPing.Dispose();

            bw.ReportProgress(0, mt_pc_data);
            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
        }
                //メインスレッドでの処理
        private void worker_ping_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // 画面表示
            ping_pc_data[ping_id] = (MT_PC_DATA)e.UserState;
            MT_PC_DATA mpd = (MT_PC_DATA)e.UserState;

            switch (ping_id)
            {
                case 1: //SC440
                    if (mpd.pc_state == PC_STATE.NG)
                    {
                        panel1.BackColor = Color.Red;
                    }
                    else
                    {
                        panel1.BackColor = SystemColors.Control;
                    }
                    break;
                case 2: //I5-3450
                    if (mpd.pc_state == PC_STATE.NG)
                    {
                        groupBox1.BackColor = Color.Red;
                    }
                    else
                    {
                        groupBox1.BackColor = SystemColors.Control;
                    }
                    break;
                case 3: //TX100S3
                    if (mpd.pc_state == PC_STATE.NG)
                    {
                        label5.BackColor = Color.Red;
                    }
                    else
                    {
                        label5.BackColor = SystemColors.Control;
                    }
                    break;
                case 4: //TX100S3-B
                    if (mpd.pc_state == PC_STATE.NG)
                    {
                        label4.BackColor = Color.Red;
                    }
                    else
                    {
                        label4.BackColor = SystemColors.Control;
                    }
                    break;
                case 5: //MJ34LL
                    if (mpd.pc_state == PC_STATE.NG)
                    {
                        label6.BackColor = Color.Red;
                    }
                    else
                    {
                        label6.BackColor = SystemColors.Control;
                    }
                    break;
                case 6: //HP6200SFF1
                    if (mpd.pc_state == PC_STATE.NG)
                    {
                        label2.BackColor = Color.Red;
                    }
                    else
                    {
                        label2.BackColor = SystemColors.Control;
                    }
                    break;
                case 7: //KV1000 (MT3)
                    if (mpd.pc_state == PC_STATE.NG)
                    {
                        label7.BackColor = Color.Red;
                    }
                    else
                    {
                        label7.BackColor = SystemColors.Control;
                    }
                    break;
/*                case 8: //KV1000SpCam-2
                    if (mpd.pc_state == PC_STATE.NG)
                    {
                        label8.BackColor = Color.Red;
                    }
                    else
                    {
                        label8.BackColor = SystemColors.Control;
                    }
                    break;
                    */
            }
            if (++ping_id > ping_id_max )
            {
                ping_id = 1;
            }
        }
            
        #endregion

        #region Timer
        private void button1_Click(object sender, EventArgs e)
        {
            //string st = "D:\\usr\\python\\dist\\MT3FileMove.exe";
            string st = "C:\\Users\\root\\Documents\\Visual Studio 2015\\Projects\\MT3FileMove\\MT3FileMove\\dist\\MT3FileMove.exe";
            if (checkBoxUseDate.Checked)
            {
                string st_param = dateTimePicker1.Value.Year.ToString() + " " + dateTimePicker1.Value.Month.ToString() + " " + dateTimePicker1.Value.Day.ToString()+" "+numericUpDown1.Value.ToString();
                System.Diagnostics.Process.Start(st,st_param);
                //System.Diagnostics.Process.Start("mt3fm.bat");
            }
            else
            {
                //org
                //System.Diagnostics.Process.Start(st);

                string st_param = DateTime.Today.ToString("yyyy MM dd ") + "2";
                System.Diagnostics.Process.Start(st, st_param);
            }

            //button1.Image = System.Drawing.Image.FromFile(@"Red_button.png");
            //label_KV1000MT2.Image  = System.Drawing.Image.FromFile(@"Orange_button.png");
        }

        private void timer_MT3IDS_Tick(object sender, EventArgs e)
        {
            label_MT3IDS.Image = System.Drawing.Image.FromFile(@"Gray_button.png");
        }

        private void timer_MT3Fine_Tick(object sender, EventArgs e)
        {
            label_MT3Fine.Image = System.Drawing.Image.FromFile(@"Gray_button.png");
        }

        private void timer_PictureViewer_Tick(object sender, EventArgs e)
        {
            label_SF.Image = System.Drawing.Image.FromFile(@"Gray_button.png");
        }

        private void timer_AFP_Tick(object sender, EventArgs e)
        {
            label_AFP.Image = System.Drawing.Image.FromFile(@"Gray_button.png");
        }

        private void timer_KV1000MT2_Tick(object sender, EventArgs e)
        {
            label_KV1000MT2.Image = System.Drawing.Image.FromFile(@"Gray_button.png");
        }

        private void timer_KV1000SpCam_Tick(object sender, EventArgs e)
        {
            label_KV1000SpCam.Image = System.Drawing.Image.FromFile(@"Gray_button.png");
        }
 
        private void timer_KV1000SpCam2_Tick(object sender, EventArgs e)
        {
            label_KV1000SpCam2.Image = System.Drawing.Image.FromFile(@"Gray_button.png");
        }

        private void timer_MT3NUV_Tick(object sender, EventArgs e)
        {
            label_NUV.Image = System.Drawing.Image.FromFile(@"Gray_button.png");
        }
        private void timer_MT3Wide_Tick(object sender, EventArgs e)
        {
            label_MT3Wide.Image = System.Drawing.Image.FromFile(@"Gray_button.png");
        }

        private void timer_NIR_Tick(object sender, EventArgs e)
        {
            if (soft_alive_check[(int)MT_MON_ID.MT3NIR] == PC_STATE.NG)
            {
                label_NIR.Image = System.Drawing.Image.FromFile(@"Gray_button.png");
            }
            soft_alive_check[(int)MT_MON_ID.MT3NIR] = PC_STATE.NG;

            label_NIR.Image = System.Drawing.Image.FromFile(@"Gray_button.png");
        }
        private void timer_MT2Wide_Tick(object sender, EventArgs e)
        {
            label_MT2Wide.Image = System.Drawing.Image.FromFile(@"Gray_button.png");
        }
        private void timer_MT2Echelle_Tick(object sender, EventArgs e)
        {
            label_MT2Echelle.Image = System.Drawing.Image.FromFile(@"Gray_button.png");
        }
        #endregion


        private void button2_Click(object sender, EventArgs e)
        {
            //button2.Enabled = false;
            MT_PC_DATA mpd;
            mpd.id = 1;
            mpd.ip = "192.168.1.206";
            mpd.pc_name = "SC440";
            mpd.pc_state = PC_STATE.NG;
            worker_ping.RunWorkerAsync(mpd);
        }

        private void timerPcCheck_Tick(object sender, EventArgs e)
        {
            MT_PC_DATA mpd;
            mpd.id = 1;
            mpd.ip = "192.168.1.206";
            mpd.pc_name = "SC440";
            mpd.pc_state = PC_STATE.NG;
            ping_pc_data[1] = mpd;

            mpd.id = 2;
            mpd.ip = "192.168.1.204";
            mpd.pc_name = "I5-3450";
            ping_pc_data[2] = mpd;

            mpd.id = 3;
            mpd.ip = "192.168.1.201";
            mpd.pc_name = "TX100S3";
            ping_pc_data[3] = mpd;

            mpd.id = 4;
            mpd.ip = "192.168.1.212";
            mpd.pc_name = "TX100S3-B";
            ping_pc_data[4] = mpd;

            mpd.id = 5;
            mpd.ip = "192.168.1.214";
            mpd.pc_name = "MJ34LL";
            ping_pc_data[5] = mpd;

            mpd.id = 6;
            mpd.ip = "192.168.1.216";
            mpd.pc_name = "HP6200SFF1";
            ping_pc_data[6] = mpd;

            mpd.id = 7;
            mpd.ip = "192.168.1.10";
            mpd.pc_name = "KV1000-1";
            ping_pc_data[7] = mpd;

            worker_ping.RunWorkerAsync(ping_pc_data[ping_id]);
        }

        private void label_NIR_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(WakeOnLanSoft,"2C-41-38-AF-89-2B");
            label1.Text = "MagicSend.exe HP6200SFF";
        }

        private void label_MT3IDS_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(WakeOnLanSoft, "00-19-99-D5-AD-01");
            label1.Text = "MagicSend.exe TX100S3";
        }

        private void label_MT3Wide_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(WakeOnLanSoft, "00-19-99-D5-AD-01");
            label1.Text = "MagicSend.exe TX100S3";
        }

        private void label_AnalogCamera_Click(object sender, EventArgs e)
        {

        }
        // MJ34LL
        private void label6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(WakeOnLanSoft, "44-8A-5B-72-01-8A");
            label1.Text = "MagicSend.exe MJ34LL";
        }

        private void label2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(WakeOnLanSoft, "2C-41-38-AF-89-2B");
            label1.Text = "MagicSend.exe HP6200SFF";
        }

        private void label4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(WakeOnLanSoft, "90-1B-0E-0D-69-48");
            label1.Text = "MagicSend.exe TX100S3-B";
        }

        private void label_HP6300_1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(WakeOnLanSoft, "A0-D3-C1-32-6A-54"); //HP6300SFF-1
            label1.Text = "MagicSend.exe HP6300SFF-1";
        }

        private void label_HP6300_2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(WakeOnLanSoft, "74-46-A0-B2-07-0C"); //HP6300SFF-2
            label1.Text = "MagicSend.exe HP6300SFF-2";
        }

        private void label_HP6300_3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(WakeOnLanSoft, "B4-B5-2F-CD-FF-E1"); //HP6300SFF-3
            label1.Text = "MagicSend.exe HP6300SFF-3";
        }


        private void label5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(WakeOnLanSoft, "00-19-99-D5-AD-01"); //TX100S3
            label1.Text = "MagicSend.exe TX100S3";
        }

        private void timer1Min_Tick(object sender, EventArgs e)
        {
            //J:ドライブの情報を取得する
            System.IO.DriveInfo drive = new System.IO.DriveInfo("J");
            //ドライブの準備ができているか調べる
            if (drive.IsReady)
            {
                int hdd_space = (int)( drive.AvailableFreeSpace / (1024 * 1024 * 1024) );
                label_hdd_info.Text= hdd_space.ToString() + " GB" ;

                // 100GB以下なら赤色
                if( hdd_space < 100 )
                {
                    label_hdd_info.ForeColor = Color.Red;
                } else
                {
                    label_hdd_info.ForeColor = SystemColors.WindowText ;
                }
            }

            DateTime dt = DateTime.Now ;
            if (dt.Minute == 0)
            {
                if (checkBoxUseDate.Checked) checkBoxUseDate.Checked = false;
                button1_Click(sender, e);
            }
        }

        private void label_all_pc_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p0 = System.Diagnostics.Process.Start(WakeOnLanSoft, "44-8A-5B-72-01-8A"); //MJ34LL
            System.Diagnostics.Process p1 = System.Diagnostics.Process.Start(WakeOnLanSoft, "00-19-99-D5-AD-01"); //TX100S3
            System.Diagnostics.Process p2 = System.Diagnostics.Process.Start(WakeOnLanSoft, "90-1B-0E-0D-69-48"); //TX100S3-B
            System.Diagnostics.Process p3 = System.Diagnostics.Process.Start(WakeOnLanSoft, "2C-41-38-AF-89-2B"); //HP6200SFF
            System.Diagnostics.Process p4 = System.Diagnostics.Process.Start(WakeOnLanSoft, "A0-D3-C1-32-6A-54"); //HP6300SFF-1 ID9 300mm IMX174C
            System.Diagnostics.Process p5 = System.Diagnostics.Process.Start(WakeOnLanSoft, "74-46-A0-B2-07-0C"); //HP6300SFF-2 Riku PC
            System.Diagnostics.Process p6 = System.Diagnostics.Process.Start(WakeOnLanSoft, "B4-B5-2F-CD-FF-E1"); //HP6300SFF-3 ID1 FishEye2 IMX174M
            System.Diagnostics.Process p7 = System.Diagnostics.Process.Start(WakeOnLanSoft, "00-19-99-D5-AD-01"); //TX100S3
            System.Diagnostics.Process p8 = System.Diagnostics.Process.Start(WakeOnLanSoft, "BC-5F-F4-44-81-98"); //I5 - 3450
        }

    }
}
