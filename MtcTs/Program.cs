using System.Diagnostics;
using System.Drawing;
using System.Reflection;

namespace MtcTs
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            
            string key = "MTC_TS_EXE";
            using var mutex = new Mutex(true, key, out bool createdNew);

            if (createdNew)
            {
                Application.Run(new Form1());
            }
            else
            {
                Application.Run(new FormMsg(null, "エラー：多重起動禁止です", "エラー", false));
            }
        }


        private static string? m_Ver;
        private static Icon? m_Icon;
        private static Bitmap? m_IconBitmap;

        /// <summary> 現在のExeのバージョンとアイコンを取得する </summary>
        /// <returns></returns>
        internal static void GetIconVer(out string ver , out Icon? icon, out Bitmap? iconBitmap)
        {
            if(m_Ver == null)
            {
                Assembly? assembly = Assembly.GetExecutingAssembly();
                Version? version = assembly?.GetName().Version;
                string? path = Process.GetCurrentProcess().MainModule?.FileName;


                m_Ver = version?.ToString() ?? "";
                m_Icon = (path == null ? null : Icon.ExtractAssociatedIcon(path));
                m_IconBitmap = m_Icon?.ToBitmap();
            }

            ver = m_Ver;
            icon = m_Icon;
            iconBitmap = m_IconBitmap;
        }
    }
}