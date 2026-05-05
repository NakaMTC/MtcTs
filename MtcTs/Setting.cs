using Microsoft.VisualBasic;
using MtcTs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MtcTs
{
    public class Settings
    {
        public string GameUrl = "";

        public int SelStart;

        public int a, aa, b, c, d, ats, up, down, left, right;
        public int selA, selB, selC, selD, selATS, selUP, selDown, selLeft, selRight;
        public int startA, startB, startC, startD, startATS, startUp, startDown, startLeft, startRight;
                
        internal void Init()
        {
            GameUrl = @"steam://rungameid/2111630";

            a = (int)eKeyTypes.Enter_電笛;
            aa = (int)eKeyTypes.Back_空笛;
            b = (int)(eKeyTypes.EB_Bブザー);
            c = (int)eKeyTypes.W_定速;
            d = (int)eKeyTypes.D_抑速1;
            ats = (int)eKeyTypes.Space_ATS確認;
            up = (int)eKeyTypes.上;
            down = (int)eKeyTypes.下;
            left = (int)eKeyTypes.左;
            right = (int)eKeyTypes.右;

            selA = (int)eKeyTypes.C_運転台;
            selB = (int)eKeyTypes.B_ブザー;
            selC = (int)eKeyTypes.非常;
            selD = (int)eKeyTypes.V_HUD表示;
            selATS = (int)eKeyTypes.X_警報持続;
            selUP = (int)eKeyTypes.I_インチング;
            selDown = (int)eKeyTypes.G_勾配起動;
            selLeft = (int)eKeyTypes.Shift_視点;
            selRight = (int)eKeyTypes.T_Task切替;

            startA = (int)eKeyTypes.Y_復帰常用;
            startB = (int)eKeyTypes.U_復帰非常;
            startC = (int)eKeyTypes.WinG_ゲームバー;
            startD = (int)eKeyTypes.WinAltR_録画;
            startATS = (int)eKeyTypes.H_ホーン切替;
            startUp = (int)eKeyTypes.I_インチング;
            startDown = (int)eKeyTypes.G_勾配起動;
            startLeft = (int)eKeyTypes.Shift_視点;
            startRight = (int)eKeyTypes.T_Task切替;

            SelStart = (int)eKeyTypes.Esc_ポーズ;
        }

        [JsonIgnore]
        internal static Settings instance;

        private static string JsonFilePath() => Application.ExecutablePath + ".settings.json";




        static Settings()
        {
            Settings? settings = null;


            try
            {
                // JSONデータの読込
                string path = JsonFilePath();
                string json = File.Exists(path) ? File.ReadAllText(JsonFilePath(), Encoding.UTF8) : "";

                if (json != null)
                {
                    var options = new JsonSerializerOptions { IncludeFields = true, };
                    settings = JsonSerializer.Deserialize<Settings>(json, options);
                }
            }
            catch
            {
                settings = null;
            }

            if(settings==null)
            {
                settings = new();
                settings.Init();
            }

            instance = settings;
        }


        internal static void Save(Settings? settings)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    IncludeFields = true,
                    WriteIndented = true,
                };

                string json = JsonSerializer.Serialize(settings, options);
                File.WriteAllText(JsonFilePath(), json, Encoding.UTF8);
            }
            catch (Exception) { }
        }
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

}
