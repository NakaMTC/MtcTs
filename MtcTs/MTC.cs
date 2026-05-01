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
        internal static int FR, Max, Min, Val;

        /// <summary> エラー段数かどうか？ </summary>
        internal static bool isErrVal => (Val < Min || Val > Max);

        /// <summary> ボタンのON-OFF (Start・Select以外) </summary>
        private static bool[] BtnOn = new bool[(int)eMtcButton.CNT];

        /// <summary> Start・SelectのON-OF </summary>
        private static bool Start, Select;

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
            Max = Common.MtcList[mtcType].Max;
            Min = Common.MtcList[mtcType].Min;

            if (Max == 13)  // P13～Bxの場合
            {
                int fr = (int)((bytes & 0xE0) >> 4);
                if (fr == 0) FR = 0;
                else if (fr == 4) FR = -1;
                if (fr == 8) FR = 1;

                Val = (int)(bytes & 0x1F) + Min - 1;
            }
            else if (Min == -6)  // P5～B5 (非常を含めてB6) の場合
            {
                int fr = (int)(bytes & 0x30);
                if (fr == 0) FR = 0;
                else if (fr == 16) FR = -1;
                if (fr == 32) FR = 1;
                
                Val = (int)(bytes & 0x0F) + Min - 1;
            }
            else
            {
                int fr = (int)((bytes & 0xF0) >> 4);
                if (fr == 0) FR = 0;
                else if (fr == 4) FR = -1;
                if (fr == 8) FR = 1;

                Val = (int)(bytes & 0x0F) + Min - 1;
            }

            BtnOn[(int)eMtcButton.A] = CheckBit(bytes, bitA);
            BtnOn[(int)eMtcButton.A強] = CheckBit(bytes, bitAA);
            BtnOn[(int)eMtcButton.B] = CheckBit(bytes, bitB);
            BtnOn[(int)eMtcButton.C] = CheckBit(bytes, bitC);
            BtnOn[(int)eMtcButton.D] = CheckBit(bytes, bitD);

            BtnOn[(int)eMtcButton.ATS] = CheckBit(bytes, bitATS);
            Start = CheckBit(bytes, bitStart);
            Select = CheckBit(bytes, bitSelect);

            BtnOn[(int)eMtcButton.上] = CheckBit(bytes, bitUP);
            BtnOn[(int)eMtcButton.下] = CheckBit(bytes, bitDOWN);
            BtnOn[(int)eMtcButton.左] = CheckBit(bytes, bitLEFT);
            BtnOn[(int)eMtcButton.右] = CheckBit(bytes, bitRIGHT);

            Debug.WriteLine(ToText());
        }

        internal static string ToText()
        {
            string text = $"({Min+1}～{Max}) ";

            if (isErrVal == false && FR >= -1 && FR <= 1) text += new[] { "後 ", "中 ", "前 " }[FR + 1];
            if (isErrVal == false) text += $"{Val} ";

            for (int i = 0; i < (int)eMtcButton.CNT; i++)
            {
                if (BtnOn[i]) text += $"{(eMtcButton)i} ";
            }

            if (Select) text += $"[Select] ";
            if (Start) text += $"|Start> ";

            return text;
        }

        private static bool CheckBit(uint bits, uint mask) => ((bits & mask) == mask);

    }
}
