using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MtcTs
{
    internal static class MTC
    {
        /// <summary> 前進後退(1前/0中/-1後) 、 最大段数、最小段数（非常を含む）、現在の段数 </summary>
        internal static int fr, max, min, val;

        /// <summary> 現在の Select・Startボタンの同時押し状態 </summary>
        internal static eSelectStartType selStartMode = eSelectStartType.Non;

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
        /// <param name="mtcType">MTCの種類</param>
        internal static void Read(int mtcType, uint bytes)
        {
            max = Common.MtcList[mtcType].Max;
            min = Common.MtcList[mtcType].Min;



            int _val, _fr;
            if (max == 13)  // P13～Bxの場合
            {
                _val = (int)(bytes & 0x1F) + min - 1;

                _fr = (int)((bytes & 0xE0) >> 4);
                if (_fr == 0) _fr = 0;
                else if (_fr == 4) _fr = -1;
                if (_fr == 8) _fr = 1;
            }
            else if (min == -6)  // P5～B5 (非常を含めてB6) の場合
            {
                _val = (int)(bytes & 0x0F) + min - 1;

                _fr = (int)(bytes & 0x30);
                if (_fr == 0) _fr = 0;
                else if (_fr == 16) _fr = -1;
                if (_fr == 32) _fr = 1;
            }
            else
            {
                _val = (int)(bytes & 0x0F) + min - 1;

                _fr = (int)((bytes & 0xF0) >> 4);
                if (_fr == 0) _fr = 0;
                else if (_fr == 4) _fr = -1;
                if (_fr == 8) _fr = 1;

            }
            if (_val >= min && _val <= max) { val = _val; fr = _fr; }
            else return;

            bool _a = CheckBit(bytes, bitA), _aa = CheckBit(bytes, bitAA), _b = CheckBit(bytes, bitB), _c = CheckBit(bytes, bitC), _d = CheckBit(bytes, bitD)
               , _ats = CheckBit(bytes, bitATS)
               , _up = CheckBit(bytes, bitUP), _down = CheckBit(bytes, bitDOWN), _left = CheckBit(bytes, bitLEFT), _right = CheckBit(bytes, bitRIGHT)
               , _select = CheckBit(bytes, bitSelect), _start = CheckBit(bytes, bitStart);

            // Select・Start以外は 全OFFのチェック
            bool allOff = !(_a || _aa || _b || _c || _d || _ats || _up || _down || _left || right);

            if (_ats)
            {
                _ats = CheckBit(bytes, bitATS);
            }


            // allOffの場合のみ 現在のモードの変更を許可する
            if (allOff)
            {
                if (_select && _start) selStartMode = eSelectStartType.Both;
                else if (_select) selStartMode = eSelectStartType.Sel;
                else if (_start) selStartMode = eSelectStartType.Start;
                else selStartMode = eSelectStartType.Non;
            }

            // 同時押しモードが一致している場合のみ、ボタンのONを許可する
            if ((_select == true && _start == false && selStartMode == eSelectStartType.Sel) ||
                (_select == false && _start == true && selStartMode == eSelectStartType.Start) ||
                (_select == false && _start == false && selStartMode == eSelectStartType.Non))
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

            selStartBoth = (selStartMode == eSelectStartType.Both && _select && _start);


            if (selStartMode == eSelectStartType.Sel || selStartMode == eSelectStartType.Start)
            {
                a = (a || aa);
                aa = false;
            }

            Debug.WriteLine(ToText());
        }

        internal static string ToText()
        {
            string text = $"({min + 1}～{max}) ";

            if (fr >= -1 && fr <= 1) text += new[] { "後 ", "中 ", "前 " }[fr + 1] + $"{val} ";

            if (selStartMode == eSelectStartType.Sel) text += "[select] ";
            if (selStartMode == eSelectStartType.Start) text += "|start> ";
            if (selStartBoth) text += "|select|start> ";

            if (a) text += "A "; if (aa) text += "A強 "; if (b) text += "B "; if (c) text += "C "; if (d) text += "D ";
            if (ats) text += "ATS "; if (up) text += "↑ "; if (down) text += "↓ "; if (left) text += "← "; if (right) text += "→ ";

            return text.Trim();
        }

        private static bool CheckBit(uint bits, uint mask) => ((bits & mask) == mask);

    }
}
