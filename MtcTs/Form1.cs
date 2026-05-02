using LibUsbDotNet;
using LibUsbDotNet.Main;
using System;
using System.Diagnostics;

namespace MtcTs
{
    public partial class Form1 : Form
    {
        /// <summary>　MTCを有効にするかどうか？ </summary>
        private bool mEnable = false;

        private bool mConnectOK = false;


        /// <summary>　MTCを有効にするかどうか？ のチェック </summary>
        private void chkEnable_CheckedChanged(object sender, EventArgs e)
        {
            mEnable = chkEnable.Checked;
            chkEnable.Text = (mEnable ? "有効" : "無効");
            if (mEnable) textBox1.Select();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            chkEnable.Checked = false;
            using (var form = new FormSettings() { Icon = this.Icon })
            {
                form.ShowDialog(this);
            }
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "MTC";
            this.Icon = Properties.Resources.p0b0;
            button1.BackgroundImage = Properties.Resources.p0b0.ToBitmap();
            button1.Enabled = false;
            button1.Text = "";
            label1.Text = "";

            Task.Run(() =>
            {
                int MtcType = -1;
                uint prevUint32 = 0;                            // 前回読み込み時バイト列 (32ビット)
                UsbEndpointReader? usbEndpointReader = null;    // USB の Reader
                UsbDevice? device = null;                       // USB の デバイス
                Image? imgErr = null;

                while (true)
                {
                    try
                    {
                        if (MtcType < 0)
                        {
                            // MtcList [0～4] のループ → MtcType を特定する
                            foreach (UsbRegistry reg in UsbDevice.AllDevices)
                            {
                                for (int i = 0; i < Common.MtcList.Length; i++)
                                {
                                    if (reg.Vid == Common.MtcList[i].Vid && reg.Pid == Common.MtcList[i].Pid && reg.Rev == Common.MtcList[i].Rev)
                                    {   // Vid　Pid　Rev　が見つかった

                                        // 接続を行う 
                                        if (reg.Open(out device) && device != null)
                                        {   // 接続成功
                                            usbEndpointReader = device.OpenEndpointReader(ReadEndpointID.Ep01);

                                            Invoke(() =>
                                            {
                                                Icon = Common.MtcList[i].icon;
                                                button1.BackgroundImage = Common.MtcList[i].img;
                                            });

                                            MtcType = i;
                                        }
                                        else
                                        {
                                            throw new Exception($"接続失敗\r\n{reg.Name}\r\n{UsbDevice.LastErrorString}");
                                        }

                                        break;
                                    }
                                }
                            }
                        }

                        if (MtcType < 0) throw new Exception("未接続");
                        if (usbEndpointReader == null) throw new Exception("未接続");


                        // 8バイトバッファへの読み込み
                        byte[] buff = new byte[8];  // 8バイトバッファ
                        int readLen = 0;
                        ErrorCode code = usbEndpointReader.Read(buff, 2000, out readLen);
                        //if (code != ErrorCode.IoTimedOut) continue;
                        if (readLen <= 0 || code != ErrorCode.None) throw new Exception($"接続エラー{code}");

                        uint tmp = (readLen > 0) ? ((uint)buff[1] << 00 | (uint)buff[2] << 08 | (uint)buff[3] << 16 | (uint)buff[4] << 24) : 0;


                        if (tmp == 0 || tmp == prevUint32) continue;

                        MTC.Read(MtcType, tmp);
                        OnKeyDownUp();

                        Invoke(() =>
                        {
                            label1.Text = MTC.ToText();
                            button1.Enabled = true;
                        });


                        mConnectOK = true;
                        prevUint32 = tmp;
                    }
                    catch (Exception ex)
                    {
                        try { if (imgErr == null) imgErr = Properties.Resources.p0b0.ToBitmap(); } catch (Exception) { }
                        try { device?.Close(); } catch { }
                        try { usbEndpointReader?.Dispose(); } catch { }
                        device = null;
                        usbEndpointReader = null;
                        prevUint32 = 0x00;

                        if (MtcType >= 0)
                        {
                            Invoke(() =>
                            {
                                Icon = Properties.Resources.p0b0;
                                button1.BackgroundImage = imgErr;
                                button1.Enabled = false;
                                label1.Text = ex.Message;
                            });
                        }

                        MtcType = -1;
                        mConnectOK = false;
                    }
                }
            });
        }


        /// <summary> ボタンの状態 </summary>        
        List<int> pushedKeys = [];

        private void OnKeyDownUp()
        {
            List<int> nowKeys = [];
            nowKeys.Clear();

            if (MTC.selStartMode == eSelectStartType.Non)
            {
                if (MTC.a) nowKeys.Add(Common.settings.a);
                if (MTC.aa) nowKeys.Add(Common.settings.aa);
                if (MTC.b) nowKeys.Add(Common.settings.b);
                if (MTC.c) nowKeys.Add(Common.settings.c);
                if (MTC.d) nowKeys.Add(Common.settings.d);
                if (MTC.ats) nowKeys.Add(Common.settings.ats);
                if (MTC.up) nowKeys.Add(Common.settings.up);
                if (MTC.down) nowKeys.Add(Common.settings.down);
                if (MTC.left) nowKeys.Add(Common.settings.left);
                if (MTC.right) nowKeys.Add(Common.settings.right);
            }
            else if (MTC.selStartMode == eSelectStartType.Sel)
            {
                if (MTC.a) nowKeys.Add(Common.settings.selA);
                if (MTC.b) nowKeys.Add(Common.settings.selB);
                if (MTC.c) nowKeys.Add(Common.settings.selC);
                if (MTC.d) nowKeys.Add(Common.settings.selD);
                if (MTC.ats) nowKeys.Add(Common.settings.selATS);
                if (MTC.up) nowKeys.Add(Common.settings.selUP);
                if (MTC.down) nowKeys.Add(Common.settings.selDown);
                if (MTC.left) nowKeys.Add(Common.settings.selLeft);
                if (MTC.right) nowKeys.Add(Common.settings.selRight);
            }
            else if (MTC.selStartMode == eSelectStartType.Start)
            {
                if (MTC.a) nowKeys.Add(Common.settings.startA);
                if (MTC.b) nowKeys.Add(Common.settings.startB);
                if (MTC.c) nowKeys.Add(Common.settings.startC);
                if (MTC.d) nowKeys.Add(Common.settings.startD);
                if (MTC.ats) nowKeys.Add(Common.settings.startATS);
                if (MTC.up) nowKeys.Add(Common.settings.startUp);
                if (MTC.down) nowKeys.Add(Common.settings.startDown);
                if (MTC.left) nowKeys.Add(Common.settings.startLeft);
                if (MTC.right) nowKeys.Add(Common.settings.startRight);
            }

            if (MTC.selStartBoth) nowKeys.Add(Common.settings.SelStart);


            if(nowKeys.Contains((int)eKeyTypes.WinAltR_録画))
            {
                nowKeys.Add((int)eKeyTypes.Win);
                nowKeys.Add((int)eKeyTypes.Alt);
                nowKeys.Add('R');
            }

            if (nowKeys.Contains((int)eKeyTypes.WinG_ゲームバー))
            {
                nowKeys.Add((int)eKeyTypes.Win);
                nowKeys.Add('G');
            }

            nowKeys = nowKeys.Where(x => x != 0).Distinct().ToList();

            


            if (nowKeys.Count == 0 && pushedKeys.Count == 0) return;



            IEnumerable<int> downs = nowKeys.Where(x => pushedKeys.Contains(x) == false);
            OnKeyDown(downs);

            IEnumerable<int> ups = pushedKeys.Where(x => nowKeys.Contains(x) == false);
            OnKeyUp(ups);

            pushedKeys = nowKeys;
        }



        private void OnKeyDown(IEnumerable<int> keys)
        {
            if (keys.Count() <= 0) return;
            foreach (int key in keys)
            {

                Debug.WriteLine($"OnKeyDown {key} , {mEnable}");
            }


            ShowKeys("－", keys);
        }



        private void OnKeyUp(IEnumerable<int> keys)
        {
            if (keys.Count() <= 0) return;
            foreach (int key in keys)
            {
                Debug.WriteLine($"OnKeyUp {key} , {mEnable}");
            }
            ShowKeys("＋", keys);
        }


        private void ShowKeys(string text, IEnumerable<int> keys)
        {
            if (mEnable) return;

            string tmp = "";
            foreach (int key in keys)
            {
                tmp += text;
                try
                {
                    eKeyTypes type = (eKeyTypes)Enum.ToObject(typeof(eKeyTypes), key);
                    tmp += $" {type}";
                }
                catch { }

                if (key >= (int)'A' && key <= (int)'Z') tmp += $" ({(char)key})\r\n";
                else tmp += $" ({key})\r\n";
            }

            Invoke(() =>
            {
                textBox2.Text += tmp;

                
                textBox2.SelectionStart = textBox2.Text.Length;

                textBox2.ScrollToCaret();
            });

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
