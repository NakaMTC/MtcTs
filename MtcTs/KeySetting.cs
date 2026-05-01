using Microsoft.VisualBasic;
using MtcTs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MtcTs
{
    /*


    public class JsonData
    {
        public string Start_Select = eKeyTypes.Esc_ポーズ.ToString();
        public string GameURL = @"steam://rungameid/2111630";


        public JsonDataSub2 P4B7 , P4B8 , P5B6 , P5B8 , P13B8;


        public JsonData()
        {
            P4B7 = new();
        }

        //public JsonDataSub2 P4B7 = new(
        //    new(A: eKeyTypes.Enter_電笛,
        //        A強: eKeyTypes.Back_空笛,
        //        B: eKeyTypes.E_EBリセット,
        //        C: eKeyTypes.D_抑速1,
        //        D: eKeyTypes.W_定速,
        //        ATS: eKeyTypes.Space_ATS確認,
        //        上: eKeyTypes.上,
        //        下: eKeyTypes.上,
        //        右: eKeyTypes.右,
        //        左: eKeyTypes.左),
        //    new(A: eKeyTypes.Enter_電笛,
        //        A強: eKeyTypes.Back_空笛,
        //        B: eKeyTypes.E_EBリセット,
        //        C: eKeyTypes.D_抑速1,
        //        D: eKeyTypes.W_定速,
        //        ATS: eKeyTypes.Space_ATS確認,
        //        上: eKeyTypes.上,
        //        下: eKeyTypes.上,
        //        右: eKeyTypes.右,
        //        左: eKeyTypes.左),
        //    new(A: eKeyTypes.Enter_電笛,
        //        A強: eKeyTypes.Back_空笛,
        //        B: eKeyTypes.E_EBリセット,
        //        C: eKeyTypes.D_抑速1,
        //        D: eKeyTypes.W_定速,
        //        ATS: eKeyTypes.Space_ATS確認,
        //        上: eKeyTypes.上,
        //        下: eKeyTypes.上,
        //        右: eKeyTypes.右,
        //        左: eKeyTypes.左)
        //);


        //public JsonDataSub P4B8 = new JsonDataSub { };
        //public JsonDataSub P5B6 = new JsonDataSub { };
        //public JsonDataSub P5B8 = new JsonDataSub { };
        //public JsonDataSub P13B8 = new JsonDataSub { };


    }



    internal class KeySetting
    {
        internal static void InitCombobox(ComboBox comboBox)
        {
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            comboBox.Items.Clear();
            comboBox.Items.Add("");

            //コンボボックスに列挙型の文字列を入れておく
            foreach (eKeyTypes a in Enum.GetValues(typeof(eKeyTypes)))
            {
                comboBox.Items.Add(a.ToString());
            }

        }

        internal static eKeyTypes? GetComboValue(ComboBox comboBox)
        {
            try
            {
                string txt = comboBox.Text;
                if (txt == "") return null;

                return (Enum.TryParse(typeof(eKeyTypes), txt, out object? eKey) ? eKey as eKeyTypes? : null);
            }
            catch
            {
                return null;
            }
        }




        internal const int VK_LWIN = 0x5B;
        internal const int VK_Alt = 0x12;


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





    public class JsonDataSub1 { 
    
        public string A, A強, B , C , D , ATS , 上 , 下 , 右 , 左 ;

        internal JsonDataSub1(eKeyTypes? A, eKeyTypes? A強, eKeyTypes? B , eKeyTypes? C , eKeyTypes? D , eKeyTypes? ATS , eKeyTypes? 上 , eKeyTypes? 下 , eKeyTypes? 右 , eKeyTypes? 左 )
        {
            this.A = A?.ToString() ?? "";
            this.A強 = A強?.ToString() ?? "";
            this.B = B?.ToString() ?? "";
            this.C = C?.ToString() ?? "";
            this.D = D?.ToString() ?? "";
            this.ATS = ATS?.ToString() ?? "";
            this.上 = 上?.ToString() ?? "";
            this.下 = 下?.ToString() ?? "";
            this.右 = 右?.ToString() ?? "";
            this.左 = 左?.ToString() ?? "";
        }

        internal JsonDataSub1(JsonDataSub1 arg)
        {
            this.A = arg.A;
            this.A強 = arg.A強;
            this.B = arg.B;
            this.C = arg.C;
            this.D = arg.D;
            this.ATS = arg.ATS;
            this.上 = arg.上;
            this.下 = arg.下;
            this.右 = arg.右;
            this.左 = arg.左;
        }
    }


    public class JsonDataSub2
    {
        public JsonDataSub1 single, select, start;
        internal JsonDataSub2(JsonDataSub1 single, JsonDataSub1 select, JsonDataSub1 start)
        {
            this.single = single;
            this.select = select;
            this.start = start;
        }

        internal JsonDataSub2(JsonDataSub2 arg)
        {
            this.single = new(arg.single);
            this.select = new(arg.select);
            this.start = new(arg.start);
        }
    }

    */

    

}