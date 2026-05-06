using LibUsbDotNet;
using LibUsbDotNet.Main;

namespace MtcTs
{

    /// <summary> USBの接続を管理する </summary>
    internal static class Usb
    {
        internal static readonly Cassette[] m_CassetteList =
        [
            new Cassette{Name="P4P6"  , Vid=0x0AE4 , Pid=0x0101 , Rev=0400 , Max=4  , Min=-7, Kiha=false , Shitetsu=true  },
            new Cassette{Name="P4P7"  , Vid=0x0AE4 , Pid=0x0101 , Rev=0300 , Max=4  , Min=-8, Kiha=false , Shitetsu=true  },
            new Cassette{Name="P5P5"  , Vid=0x1C06 , Pid=0x77A7 , Rev=0202 , Max=5  , Min=-6, Kiha=true  , Shitetsu=false },
            new Cassette{Name="P5P7"  , Vid=0x0AE4 , Pid=0x0101 , Rev=0800 , Max=5  , Min=-8, Kiha=false , Shitetsu=false },
            new Cassette{Name="P13P7" , Vid=0x0AE4 , Pid=0x0101 , Rev=0000 , Max=13 , Min=-8, Kiha=false , Shitetsu=false },
        ];


        /// <summary> MTCの段数カセット </summary>
        internal class Cassette
        {
            /// <summary> カードリッジ名 </summary>
            internal string Name="";

            /// <summary> USB接続用の Vid Pid Rev 、 ノッチ段数の最大（＋）と最小（マイナス、非常段を含む）</summary>
            internal int Vid, Pid, Rev, Max = 0, Min = 0;

            /// <summary> キハ54用、私鉄用か？</summary>
            internal bool Kiha, Shitetsu;
        }

        /// <summary> 現在接続中の段数カセット名 </summary>
        internal static string m_CassetteName = "";

        /// <summary> 現在接続中のMTCの最大段数（＋）、最小段数（マイナス、非常を含む）</summary>
        internal static int m_Max = 0, m_Min = 0;

        /// <summary> キハ54用、私鉄用か？</summary>
        internal static bool m_Kiha = false, m_Shitetsu = true;

        /// <summary> 現在のUSBの読み込み値(32ビット)</summary>
        internal static uint m_Uint32 = 0;

        /// <summary> 接続OKか？ </summary>
        internal static bool m_ConnectOK = false;


        /// <summary> 現在のエラー </summary>
        internal static Exception? m_Error = null;

        /// <summary> usb の Reader </summary>
        private static UsbEndpointReader? m_Reader = null;

        /// <summary> usb の デバイス </summary>
        private static UsbDevice? m_Device = null;


        internal static string ToText() => $"{m_CassetteName} 0x{m_Uint32:X8} {(m_Error?.Message ?? "")}";

        internal static bool Read()
        {
            try
            {
                if (m_Reader == null)
                {
                    // 接続前に既存のデバイスはクローズする
                    try { m_Device?.Close(); } catch { }
                    m_Device = null;

                    // MtcList [0～4] のループ → m_MtcTypeIndex を特定する
                    foreach (UsbRegistry reg in UsbDevice.AllDevices)
                    {
                        foreach (Cassette cassette in m_CassetteList)
                        {
                            if (reg.Vid == cassette.Vid && reg.Pid == cassette.Pid && reg.Rev == cassette.Rev)
                            {

                                if (reg.Open(out m_Device) && m_Device != null) // デバイスをOpen
                                {
                                    m_Reader = m_Device.OpenEndpointReader(ReadEndpointID.Ep01);
                                    m_CassetteName = cassette.Name;
                                    m_Max = cassette.Max;
                                    m_Min = cassette.Min;
                                    m_Kiha = cassette.Kiha;
                                    m_Shitetsu = cassette.Shitetsu;
                                }

                                // Open失敗時
                                if (m_Reader == null) throw new Exception($"接続失敗 {cassette.Name} {reg.Name} {UsbDevice.LastErrorString}");

                                break;
                            }
                        }  // foreach (Cassette cassette in m_CassetteList)

                        if (m_Reader != null) break;
                    } // foreach (UsbRegistry reg in UsbDevice.AllDevices)
                }


                if (m_Reader == null) throw new Exception("未接続");


                // 8バイトバッファへの読み込み
                byte[] buff = new byte[8];  // 8バイトバッファ
                int readLen = 0;
                ErrorCode code = m_Reader.Read(buff, 2000, out readLen);


                if (code == ErrorCode.IoTimedOut)
                {
                    // タイムアウトの場合
                    m_ConnectOK = false;
                    return false;   
                }

                // 接続エラーの場合
                if (readLen <= 0 || code != ErrorCode.None) throw new Exception($"接続エラー{code}");

                uint tmp = (readLen > 0) ? ((uint)buff[1] << 00 | (uint)buff[2] << 08 | (uint)buff[3] << 16 | (uint)buff[4] << 24) : 0;
                m_Error = null;
                m_ConnectOK = (tmp != 0x00);



                if (tmp == 0 || tmp == m_Uint32) return false;

                m_Uint32 = tmp;
                return true;
            }
            catch (Exception ex)
            {
                m_ConnectOK = false;

                MtcKeyMouse.OnKeyClear();

                try { m_Device?.Close(); } catch { }
                try { m_Reader?.Dispose(); } catch { }
                m_Device = null;
                m_Reader = null;

                m_Error = ex;

                if (m_Uint32 == 0) return false;
                m_Uint32 = 0;
                return true;
            }
        }
    }



}
