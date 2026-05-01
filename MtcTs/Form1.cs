using LibUsbDotNet;
using LibUsbDotNet.Main;
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


                        if (MTC.isErrVal == false) Invoke(() =>
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

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {



            //eKeyTypes? eVal = KeySetting.GetComboValue(comboBox1);
            //int intNum = (eVal != null ? (int)eVal : 0);
            //char c = ((intNum >= 'A' && intNum <= 'Z') ? (char)(intNum) : '-');
        }
    }
}
