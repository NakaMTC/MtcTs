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
        internal readonly static (string Name, int Vid, int Pid, int Rev, int Max, int Min, bool Kiha, bool Sitetsu, Icon icon, Image img)[] MtcList =
        [   // Name    Vid      Pid      Rev   Max  Min Kiha   Sitetsu  icon             img
            ("P4P6"  , 0x0AE4 , 0x0101 , 0400 , 4 , -7, false , true  , Resources.p4b6  , Resources.p4b6 .ToBitmap()  ),   // 0 : P4  B7 (非常を含めて-7)
            ("P4P7"  , 0x0AE4 , 0x0101 , 0300 , 4 , -8, false , true  , Resources.p4b7  , Resources.p4b7 .ToBitmap()  ),   // 1 : P4  B8 (非常を含めて-8)
            ("P5P5"  , 0x1C06 , 0x77A7 , 0202 , 5 , -6, true  , false , Resources.p5b5  , Resources.p5b5 .ToBitmap()  ),   // 2 : P5  B6 (非常を含めて-6) → キハ５４向け
            ("P5P7"  , 0x0AE4 , 0x0101 , 0800 , 5 , -8, false , false , Resources.p5b7  , Resources.p5b7 .ToBitmap()  ),   // 3 : P5  B8 (非常を含めて-8)
            ("P13P7" , 0x0AE4 , 0x0101 , 0000 , 13, -8, false , false , Resources.p13b7 , Resources.p13b7.ToBitmap()  ),   // 4 : P13 B8 (非常を含めて-8)
        ];


        public static Settings settings;

        static Common()
        {
            settings = Settings.Load();
        }

        /// <summary> Windows ・ ALTキー </summary>
        internal const int VK_LWindows = 0x5B, VK_Alt = 0xA4;
        
    }

    /// <summary> Select・Startボタンの状態</summary>
    internal enum eSelectStartType { Non, Sel, Start, Both }

    internal enum eKeyTypes
    {
        Space_ATS確認 = 0x20,
        Enter_電笛 = 0x0D,
        Back_空笛 = 0x08,
        Shift_視点 = 0x10,
        Esc_ポーズ = 0x1B,        
        左 = 0x25,
        上 = 0x26,
        右 = 0x27,
        下 = 0x28,
        B_ブザー = 'B',
        C_運転台 = 'C',
        D_抑速1 = 'D',
        E_EBリセット = 'E',
        G_勾配起動 = 'G',
        H_ホーン切替 = 'H',
        I_インチング = 'I',
        T_Task切替 = 'T',
        U_復帰非常 = 'U',
        V_HUD表示 = 'V',
        W_定速 = 'W',
        X_警報持続 = 'X',
        Y_復帰常用 = 'Y',

        Win = 0x5B,
        Alt = 0xA4,

        非常 = -1,
        WinG_ゲームバー = -2,
        WinAltR_録画 = -3,
        EB_Bブザー = -4,
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
