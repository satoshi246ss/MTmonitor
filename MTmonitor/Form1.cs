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
        public Byte  id;        //unsigned char  Soft ID
        public Byte  obs;       //unsigned char  観測中:1/0
        public Byte  save;      //unsigned char  保存中:1/0
        public Int32 diskspace; // HDD残容量(MB)
    }

    public partial class Form1 : Form
    {
        //状態を表す定数
        const int OFF = 0;
        const int ON  = 1;

        int mmFsiUdpPortMTmonitor = 24415;
        private BackgroundWorker worker_udp;
        public Form1()
        {
            InitializeComponent();

            worker_udp = new BackgroundWorker();
            worker_udp.WorkerReportsProgress = true;
            worker_udp.WorkerSupportsCancellation = true;
            worker_udp.DoWork += new DoWorkEventHandler(worker_udp_DoWork);
            worker_udp.ProgressChanged += new ProgressChangedEventHandler(worker_udp_ProgressChanged);
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
            switch(kmd3.id)
            {
                case 0: //AFP
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
                    break;
                case 1: //KV1000MT2
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
                    break;
                case 2: //KV1000SpCam
                    label = label_KV1000SpCam;
                    timer_KV1000SpCam.Stop(); timer_KV1000SpCam.Start();
                    label.Text = "KV1000SpCam\n" + (kmd3.diskspace).ToString() + "GB";
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
                    break;
                case 3: //FSI2
                    label = label_FSI2;
                    timer_FSI2.Stop(); timer_FSI2.Start();
                    label.Text = "FSI2\n" + (kmd3.diskspace).ToString() + "GB";
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
                    break;
                case 4: //MT3Fine
                    label = label_MT3Fine;
                    timer_MT3Fine.Stop(); timer_MT3Fine.Start();
                    label.Text = "MT3_Fine\n" + (kmd3.diskspace).ToString() + "GB";
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
                    break;
                case 5: //MT3IDS
                    timer_MT3IDS.Stop(); timer_MT3IDS.Start();
                    label_MT3IDS.Text="MT3_IDS\n"+ (kmd3.diskspace).ToString() +"GB";
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
                    break;
                case 6: //PictureViewer
                    label = label_PictureViewer;
                    timer_PictureViewer.Stop(); timer_PictureViewer.Start();
                    label.Text = "PictureViewer\n" + (kmd3.diskspace).ToString() + "GB";
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
                    break;
                case 7: //MT3Wide
                    timer_MT3Wide.Stop(); timer_MT3Wide.Start();
                    label_MT3Wide.Text = "MT3_Wide\n" + (kmd3.diskspace).ToString() + "GB";
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

        #region Timer
        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("D:\\usr\\python\\dist\\MT3FileMove.exe");
            //System.Diagnostics.Process.Start("mt3fm.bat");

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
            label_PictureViewer.Image = System.Drawing.Image.FromFile(@"Gray_button.png");
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

        private void timer_FSI2_Tick(object sender, EventArgs e)
        {
            label_FSI2.Image = System.Drawing.Image.FromFile(@"Gray_button.png");
        }
        private void timer_MT3Wide_Tick(object sender, EventArgs e)
        {
            label_MT3Wide.Image = System.Drawing.Image.FromFile(@"Gray_button.png");
        }

        #endregion

    }
}
