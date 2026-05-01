using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MtcTs.Properties;

namespace MtcTs
{
    internal static class Common
    {
        /// <summary> 標準のURL （JR東日本TSを起動）</summary>
        internal const string GAME_URL_DEF = @"steam://rungameid/2111630";

        internal readonly static (int Vid, int Pid, int Rev, int Max, int Min, bool Kiha, Icon icon, Image img)[] MtcList =
        [   // Vid    Pid      Rev   Max  Min Kiha   icon              img
            (0x0AE4 , 0x0101 , 0400 , 4 , -7, false ,Resources.p4b7  , Resources.p4b7 .ToBitmap()  ),   // 0 : P4  B7 (非常を含めて-7)
            (0x0AE4 , 0x0101 , 0300 , 4 , -8, false ,Resources.p4b8  , Resources.p4b8 .ToBitmap()  ),   // 1 : P4  B8 (非常を含めて-8)
            (0x1C06 , 0x77A7 , 0202 , 5 , -6, true  ,Resources.p5b6  , Resources.p5b6 .ToBitmap()  ),   // 2 : P5  B6 (非常を含めて-6) → キハ５４向け
            (0x0AE4 , 0x0101 , 0800 , 5 , -8, false ,Resources.p5b8  , Resources.p5b8 .ToBitmap()  ),   // 3 : P5  B8 (非常を含めて-8)
            (0x0AE4 , 0x0101 , 0000 , 13, -8, false ,Resources.p13b8 , Resources.p13b8.ToBitmap()  ),   // 4 : P13 B8 (非常を含めて-8)
        ];


        internal static Dictionary<string, int> ButtonSettings = [];

        private static string SettingsKey(int mtcType, int mtcBtn, bool select, bool start) => $"{mtcType}{mtcBtn}{(select ? 1 : 0)}{(start ? 1 : 0)}";

        private static int GetSetting(int mtcType, int mtcBtn, bool select, bool start)
        {
            string key = SettingsKey(mtcType, mtcBtn, select, start);
            return (ButtonSettings.ContainsKey(key) ? ButtonSettings[key] : 0 );
        }

        private static void SetSetting(int mtcType, int mtcBtn, bool select, bool start, int arg) => ButtonSettings[SettingsKey(mtcType, mtcBtn, select, start)] = arg;

    }


    internal enum eKeyTypes
    {
        Space_ATS確認 = 0x08,
        Enter_電笛 = 0x0d,
        Back_空笛 = 0x08,
        Shift_視点 = 0x10,
        Esc_ポーズ = 0x1B,
        Sp_ATS確認 = 0x20,
        左 = 0x25,
        上 = 0x26,
        右 = 0x27,
        下 = 0x28,
        B_ブザー = 'B',
        C_運転台 = 'C',
        D_抑速1 = 'D',
        E_EBリセット = 'E',
        G_勾配起動 = 'G',
        H_ミュージックホーン切替 = 'H',
        I_インチング = 'I',
        T_Task切替 = 'T',
        U_復帰非常 = 'U',
        W_定速 = 'W',
        X_警報持続 = 'X',
        Y_復帰常用 = 'Y',


        非常 = -1,
        最大加速 = -2,
        WinG_ゲームバー = -3,
        WinAltR_録画 = -4,
    }


    /// <summary> MTCのボタンのID A～D 、 ATS 、十字キー （Start Select を除く）</summary>
    internal enum eMtcButton
    {
        A = 0, A強 = 1, B = 2, C = 3, D = 4,
        ATS = 5,
        上 = 6, 下 = 7, 左 = 8, 右 = 9,
        CNT = 10
    }


}
