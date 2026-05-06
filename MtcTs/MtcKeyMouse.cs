using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MtcTs
{
    internal static class MtcKeyMouse
    {
        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        private const uint KEYEVENTF_KEYDOWN = 0x0000;
        private const uint KEYEVENTF_KEYUPDOWN = 0x0001;
        private const uint KEYEVENTF_KEYUP = 0x0002;
        private const byte VK_UP = 0x26;
        private const byte VK_DOWN = 0x28;

        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const int MOUSEEVENTF_WHEEL = 0x0800;

        /// <summary> 左Windowsキー、左Altキー </summary>
        private const int VK_Win = 0x5B, VK_Alt = 0xA4;

        /// <summary> 現在押されているキー一覧 </summary>
        private static List<int> m_Keys = [];

        /// <summary> 非常ブレーキボタンのON-OFF </summary>
        internal static bool m_非常 = false;


        /// <summary> キーの Down と Up を制御する</summary>
        internal static void OnKeyDownUp()
        {
            try
            {
                // 現在押されているキーの一覧を取得
                List<int> nowKeys = [];

                if (MTC.m_SelStartMode == eSelectStartType.Non)
                {   // Selectボタン・Startボタンなし
                    if (MTC.a) nowKeys.Add(Settings.instance.a);
                    if (MTC.aa) nowKeys.Add(Settings.instance.aa);
                    if (MTC.b) nowKeys.Add(Settings.instance.b);
                    if (MTC.c) nowKeys.Add(Settings.instance.c);
                    if (MTC.d) nowKeys.Add(Settings.instance.d);
                    if (MTC.ats) nowKeys.Add(Settings.instance.ats);
                    if (MTC.up) nowKeys.Add(Settings.instance.up);
                    if (MTC.down) nowKeys.Add(Settings.instance.down);
                    if (MTC.left) nowKeys.Add(Settings.instance.left);
                    if (MTC.right) nowKeys.Add(Settings.instance.right);
                }
                else if (MTC.m_SelStartMode == eSelectStartType.Sel)
                {   // Selectボタン同時押し
                    if (MTC.a) nowKeys.Add(Settings.instance.selA);
                    if (MTC.b) nowKeys.Add(Settings.instance.selB);
                    if (MTC.c) nowKeys.Add(Settings.instance.selC);
                    if (MTC.d) nowKeys.Add(Settings.instance.selD);
                    if (MTC.ats) nowKeys.Add(Settings.instance.selATS);
                    if (MTC.up) nowKeys.Add(Settings.instance.selUP);
                    if (MTC.down) nowKeys.Add(Settings.instance.selDown);
                    if (MTC.left) nowKeys.Add(Settings.instance.selLeft);
                    if (MTC.right) nowKeys.Add(Settings.instance.selRight);
                }
                else if (MTC.m_SelStartMode == eSelectStartType.Start)
                {   // Startボタン同時押し
                    if (MTC.a) nowKeys.Add(Settings.instance.startA);
                    if (MTC.b) nowKeys.Add(Settings.instance.startB);
                    if (MTC.c) nowKeys.Add(Settings.instance.startC);
                    if (MTC.d) nowKeys.Add(Settings.instance.startD);
                    if (MTC.ats) nowKeys.Add(Settings.instance.startATS);
                    if (MTC.up) nowKeys.Add(Settings.instance.startUp);
                    if (MTC.down) nowKeys.Add(Settings.instance.startDown);
                    if (MTC.left) nowKeys.Add(Settings.instance.startLeft);
                    if (MTC.right) nowKeys.Add(Settings.instance.startRight);
                }

                // Select＋Startボタン同時押し
                if (MTC.selStartBoth) nowKeys.Add(Settings.instance.SelStart);


                m_非常 = nowKeys.Contains((int)eKeyTypes.非常);


                nowKeys = nowKeys.Where(x => x != 0).Distinct().ToList();

                if (nowKeys.Count == 0 && m_Keys.Count == 0) return;

                KeyUpDown(nowKeys);

                m_Keys = nowKeys;
            }
            catch { }
        }

        internal static void OnKeyClear()
        {
            try
            {
                if (m_Keys.Count > 0) KeyUpDown(null);
            }
            catch { }
        }

        private static void KeyUpDown(List<int>? keys)
         {
            IEnumerable<int>? downs, ups;    // Downしたキー、Upしたキーの一覧

            if (keys != null)
            {
                downs = keys.Where(_ => m_Keys.Contains(_) == false);
                ups = m_Keys.Where(_ => keys.Contains(_) == false);
            }
            else
            {
                downs = null; 
                ups = m_Keys;
            }

            if(downs != null)
            {
                foreach (int key in downs)
                {
                    keyDown(key);
                    m_Keys.Add(key);
                }
            }

            if (ups != null)
            {
                foreach (int key in ups)
                {
                    keyUp(key);
                    m_Keys.Remove(key);
                }
            }
        }


        static void keyDown(int key)
        {
            if (key == (int)eKeyTypes.EB_Bブザー) key = (Usb.m_Shitetsu ? (int)eKeyTypes.B_ブザー : (int)eKeyTypes.E_EBリセット);

            if (key > 0)
            {
                keybd_event((byte)key, 0, KEYEVENTF_KEYDOWN, 0);
            }
            else if (key == (int)eKeyTypes.WinG_ゲームバー)
            {
                keybd_event((byte)VK_Win, 0, KEYEVENTF_KEYDOWN, 0);
                keybd_event((byte)'G', 0, KEYEVENTF_KEYDOWN, 0);
            }
            else if (key == (int)eKeyTypes.WinAltR_録画)
            {
                keybd_event((byte)VK_Win, 0, KEYEVENTF_KEYDOWN, 0);
                keybd_event((byte)VK_Alt, 0, KEYEVENTF_KEYDOWN, 0);
                keybd_event((byte)'R', 0, KEYEVENTF_KEYDOWN, 0);
            }
        }

        static void keyUp(int key)
        {
            if (key == (int)eKeyTypes.EB_Bブザー) key = (Usb.m_Shitetsu ? (int)eKeyTypes.B_ブザー : (int)eKeyTypes.E_EBリセット);

            if (key > 0)
            {
                keybd_event((byte)key, 0, KEYEVENTF_KEYUP, 0);
            }
            else if (key == (int)eKeyTypes.WinG_ゲームバー)
            {
                keybd_event((byte)'G', 0, KEYEVENTF_KEYUP, 0);
                keybd_event((byte)VK_Win, 0, KEYEVENTF_KEYUP, 0);
            }
            else if (key == (int)eKeyTypes.WinAltR_録画)
            {
                keybd_event((byte)'R', 0, KEYEVENTF_KEYUP, 0);
                keybd_event((byte)VK_Alt, 0, KEYEVENTF_KEYUP, 0);
                keybd_event((byte)VK_Win, 0, KEYEVENTF_KEYUP, 0);
            }
        }


        private static int m_Fr = 0;

        /// <summary> 前進後退の切替 </summary>
        internal static void OnKeyFR()
        {
            // キハ54対応 の場合、↑↓（前後切替）の代わりに RF（直変切替）とする
            byte FR_UP = (Usb.m_Kiha ? (byte)'R' : VK_UP);
            byte FR_DOWN = (Usb.m_Kiha ? (byte)'F' : VK_DOWN);

            if (MTC.fr > 0 && m_Fr <= 0)
            {   // 前進に切り替えた場合 ↑↑
                keybd_event(FR_UP, 0, KEYEVENTF_KEYUPDOWN, 0);
                Thread.Sleep(10);
                keybd_event(FR_UP, 0, KEYEVENTF_KEYUPDOWN, 0);
            }
            else if (MTC.fr < 0 && m_Fr >= 0)
            {   // 後退に切り替えた場合 ↓↓
                keybd_event(FR_DOWN, 0, KEYEVENTF_KEYUPDOWN, 0);
                Thread.Sleep(10);
                keybd_event(FR_DOWN, 0, KEYEVENTF_KEYUPDOWN, 0);
            }
            else if (MTC.fr == 0 && m_Fr > 0)
            {   // 前進→中立に切り替えた場合 ↓
                keybd_event(FR_DOWN, 0, KEYEVENTF_KEYUPDOWN, 0);
            }
            else if (MTC.fr == 0 && m_Fr < 0)
            {   // 後退→中立に切り替えた場合 ↑
                keybd_event(FR_UP, 0, KEYEVENTF_KEYUPDOWN, 0);
            }
            m_Fr = MTC.fr;
        }


        private static int m_Val = 0;

        internal static void OnValChange()
        {
            // 変更後の値
            int mtcVal = MTC.val;   
            if (Usb.m_Kiha)
            {
                if (MTC.val == 0 || MTC.val == -1) mtcVal = 0;      // B0 B1 → B0 (緩め)
                if (MTC.val == -2 || MTC.val == -3) mtcVal = -1;    // B2 B3 → B1 (重なり)
                if (MTC.val == -4 || MTC.val == -5) mtcVal = -2;    // B4 B5 → B2 (制動)
                if (MTC.val <= -6) mtcVal = -3;                     // B9（非常） → B3 (非常)
            }

            if(MTC.val == Usb.m_Min)
            {
                if (m_非常) mtcVal--;           // 非常ブレーキボタン のとき、現在の段数を -8 → -9 に変更
                if (m_Val <= mtcVal) return;    // (変更前:-9 <= 変更後:-8～-9)  or (変更前:-8 <= 変更後:-8) は除外
            }

            // 非常ブレーキの段数
            int val非常;
            if (Usb.m_Kiha) val非常 = -3;
            else if (Usb.m_Shitetsu) val非常 = -8;
            else val非常 = -9;

            // 段数の変化量
            int move;
            if (mtcVal == val非常) move = -30;
            else if (mtcVal == Usb.m_Max) move = 30;
            else move = mtcVal - m_Val;


            if (MTC.val == 0)
            {
                mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
                Thread.Sleep(10);
                mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                Thread.Sleep(10);
                mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
                Thread.Sleep(10);
                mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
            }
            else if (move != 0)
            {
                mouse_event(MOUSEEVENTF_WHEEL, 0, 0, -move * 120, 0);
            }

            m_Val = mtcVal;

        }
    }
}