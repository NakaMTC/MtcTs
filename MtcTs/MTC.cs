namespace MtcTs
{
    internal static class MTC
    {
        /// <summary> 前進後退(1前/0中/-1後) 、 現在の段数（＋ or 0 or マイナス）</summary>
        internal static int fr, val;

        /// <summary> 現在の Select・Startボタンの同時押し状態 </summary>
        internal static eSelectStartType m_SelStartMode = eSelectStartType.Non;

        /// <summary> 現在の Select・Start 以外のボタンの状態 </summary>
        internal static bool a, aa, b, c, d, ats, up, down, left, right;

        /// <summary> Select・Start の同時押しの状態 </summary>
        internal static bool selStartBoth;

        /// <summary> ボタンのビットフィールド </summary>
        private const uint bitA = 0x0400, bitAA = 0x0c00, bitB = 0x1000, bitC = 0x2000, bitD = 0x0200;

        /// <summary> ATS , スタート , Select ボタンのビットフィールド </summary>
        private const uint bitATS = 0x0100, bitStart = 0x00010000, bitSelect = 0x00020000;

        /// <summary> 上下左右 ボタンのビットフィールド </summary>
        private const uint bitUP = 0x00040000, bitDOWN = 0x00080000, bitLEFT = 0x00100000, bitRIGHT = 0x00200000;


        /// <summary> USBから送られてきたバイト列を読み込む</summary>        
        internal static bool Read()
        {
            int _val, _fr;
            if (Usb.m_Max == 13)  // P13～Bxの場合
            {
                _val = (int)(Usb.m_Uint32 & 0x1F) + Usb.m_Min - 1;

                _fr = (int)((Usb.m_Uint32 & 0xE0) >> 4);
                if (_fr == 0) _fr = 0;
                else if (_fr == 4) _fr = -1;
                if (_fr == 8) _fr = 1;
            }
            else if (Usb.m_Min == -6)  // P5～B5 (非常を含めてB6) の場合
            {
                _val = (int)(Usb.m_Uint32 & 0x0F) + Usb.m_Min - 1;

                _fr = (int)(Usb.m_Uint32 & 0x30);
                if (_fr == 0) _fr = 0;
                else if (_fr == 16) _fr = -1;
                if (_fr == 32) _fr = 1;
            }
            else
            {
                _val = (int)(Usb.m_Uint32 & 0x0F) + Usb.m_Min - 1;

                _fr = (int)((Usb.m_Uint32 & 0xF0) >> 4);
                if (_fr == 0) _fr = 0;
                else if (_fr == 4) _fr = -1;
                if (_fr == 8) _fr = 1;
            }
            if (_val >= Usb.m_Min && _val <= Usb.m_Max) { val = _val; fr = _fr; }
            else return false;


            bool _a = CheckBit(Usb.m_Uint32, bitA), _aa = CheckBit(Usb.m_Uint32, bitAA)
               , _b = CheckBit(Usb.m_Uint32, bitB), _c = CheckBit(Usb.m_Uint32, bitC), _d = CheckBit(Usb.m_Uint32, bitD)
               , _ats = CheckBit(Usb.m_Uint32, bitATS)
               , _up = CheckBit(Usb.m_Uint32, bitUP), _down = CheckBit(Usb.m_Uint32, bitDOWN)
               , _left = CheckBit(Usb.m_Uint32, bitLEFT), _right = CheckBit(Usb.m_Uint32, bitRIGHT)
               , _select = CheckBit(Usb.m_Uint32, bitSelect), _start = CheckBit(Usb.m_Uint32, bitStart);

            // Select・Start以外は 全OFFのチェック
            bool allOff = !(_a || _aa || _b || _c || _d || _ats || _up || _down || _left || right);

            // allOffの場合のみ 現在のモードの変更を許可する
            if (allOff)
            {
                if (_select && _start) m_SelStartMode = eSelectStartType.Both;
                else if (_select) m_SelStartMode = eSelectStartType.Sel;
                else if (_start) m_SelStartMode = eSelectStartType.Start;
                else m_SelStartMode = eSelectStartType.Non;
            }

            // 同時押しモードが一致している場合のみ、ボタンのONを許可する
            if ((_select == true && _start == false && m_SelStartMode == eSelectStartType.Sel) ||
                (_select == false && _start == true && m_SelStartMode == eSelectStartType.Start) ||
                (_select == false && _start == false && m_SelStartMode == eSelectStartType.Non))
            {
                a = _a; aa = _aa; b = _b; c = _c; d = _d;
                ats = _ats;
                up = _up; down = _down; left = _left; right = _right;                
            }
            else
            {
                if (!_a) a = false; if (!_aa) aa = false; if (!_b) b = false; if (!_c) c = false; if (!_d) d = false;
                if (!_ats) ats = false;
                if (!_up) up = false; if (!_down) down = false; if (!_left) left = false; if (!_right) right = false;             
            }

            selStartBoth = (m_SelStartMode == eSelectStartType.Both && _select && _start);


            if (m_SelStartMode == eSelectStartType.Sel || m_SelStartMode == eSelectStartType.Start)
            {
                a = (a || aa);
                aa = false;
            }

            return true;
        }

        internal static string ToText()
        {
            string text = "";

            if (fr >= -1 && fr <= 1) text += new[] { "後 ", "中 ", "前 " }[fr + 1] + $"{val} ";

            if (m_SelStartMode == eSelectStartType.Sel) text += "[select] ";
            if (m_SelStartMode == eSelectStartType.Start) text += "|start> ";
            if (selStartBoth) text += "|select|start> ";

            if (a) text += "A "; if (aa) text += "A強 "; if (b) text += "B "; if (c) text += "C "; if (d) text += "D ";
            if (ats) text += "ATS "; if (up) text += "↑ "; if (down) text += "↓ "; if (left) text += "← "; if (right) text += "→ ";

            return text.Trim();
        }

        private static bool CheckBit(uint bits, uint mask) => ((bits & mask) == mask);

    }
}
