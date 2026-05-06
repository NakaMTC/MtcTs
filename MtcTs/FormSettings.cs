using System.Drawing;
using System.Reflection;

namespace MtcTs
{
    public partial class FormSettings : Form
    {
        internal ComboBox[] ComboList => [
                 A, A強, B,      C,      D,      ATS,      上,      左,      右,      下, 
              selA,   selB,   selC,   selD,   selATS,   sel上,   sel左,   sel右,   sel下,
            startA, startB, startC, startD, startATS, start上, start左, start右, start下,
            SelStart
        ];

        public FormSettings()
        {
            InitializeComponent();
        }

        /// <summary> キャンセルボタン </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e) => this.Close();

        private void FormSettings_Load(object sender, EventArgs e)
        {
            Program.GetIconVer(out string ver, out Icon? icon, out Bitmap? iconBitmap);
            Icon = icon;
            Text = $"MtcTs 設定 - ver.{ver}";
            

            foreach (ComboBox comboBox in ComboList)
            {
                InitComboBox(comboBox);
            }

            LoadComboBox();
        }


        static List<string> m_List = [];

        private void InitComboBox(ComboBox comboBox)
        {
            if (m_List.Count <= 0)
            {
                m_List.Add("--");
                foreach (eKeyTypes keyType in Enum.GetValues(typeof(eKeyTypes)))
                {
                    m_List.Add(keyType.ToString());
                }
            }

            comboBox.DataSource = m_List.ToArray();

        }

        private static string i2s(int i)
        {
            if (i == 0) return "--";

            if (Enum.IsDefined(typeof(eKeyTypes), i))
            {
                return ((eKeyTypes)i).ToString();
            }

            return "--";
        }

        private static int s2i(string s)
        {
            if (string.IsNullOrEmpty(s) || s == "--")
                return 0;

            if (Enum.TryParse(typeof(eKeyTypes), s, out var result))
            {
                return (int)result;
            }

            return 0;
        }

        private void LoadComboBox()
        {
            A.Text  = i2s(Settings.instance.a);
            A強.Text  = i2s(Settings.instance.aa);
            B.Text  = i2s(Settings.instance.b);
            C.Text  = i2s(Settings.instance.c);
            D.Text  = i2s(Settings.instance.d);
            ATS.Text  = i2s(Settings.instance.ats);
            上.Text  = i2s(Settings.instance.up);
            下.Text  = i2s(Settings.instance.down);
            左.Text  = i2s(Settings.instance.left);
            右.Text  = i2s(Settings.instance.right);

            selA.Text  = i2s(Settings.instance.selA);
            selB.Text  = i2s(Settings.instance.selB);
            selC.Text  = i2s(Settings.instance.selC);
            selD.Text  = i2s(Settings.instance.selD);
            selATS.Text  = i2s(Settings.instance.selATS);
            sel上.Text  = i2s(Settings.instance.selUP);
            sel下.Text  = i2s(Settings.instance.selDown);
            sel左.Text  = i2s(Settings.instance.selLeft);
            sel右.Text  = i2s(Settings.instance.selRight);

            startA.Text  = i2s(Settings.instance.startA);
            startB.Text  = i2s(Settings.instance.startB);
            startC.Text  = i2s(Settings.instance.startC);
            startD.Text  = i2s(Settings.instance.startD);
            startATS.Text  = i2s(Settings.instance.startATS);
            start上.Text  = i2s(Settings.instance.startUp);
            start下.Text  = i2s(Settings.instance.startDown);
            start左.Text  = i2s(Settings.instance.startLeft);
            start右.Text  = i2s(Settings.instance.startRight);

            SelStart.Text  = i2s(Settings.instance.SelStart);
        }

        private void buttonInit_Click(object sender, EventArgs e)
        {
            Settings.instance.Init();
            LoadComboBox();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Settings.instance.a = s2i(A.Text);
            Settings.instance.aa = s2i(A強.Text);
            Settings.instance.b = s2i(B.Text);
            Settings.instance.c = s2i(C.Text);
            Settings.instance.d = s2i(D.Text);
            Settings.instance.ats = s2i(ATS.Text);
            Settings.instance.up = s2i(上.Text);
            Settings.instance.down = s2i(下.Text);
            Settings.instance.left = s2i(左.Text);
            Settings.instance.right = s2i(右.Text);

            Settings.instance.selA = s2i(selA.Text);
            Settings.instance.selB = s2i(selB.Text);
            Settings.instance.selC = s2i(selC.Text);
            Settings.instance.selD = s2i(selD.Text);
            Settings.instance.selATS = s2i(selATS.Text);
            Settings.instance.selUP = s2i(sel上.Text);
            Settings.instance.selDown = s2i(sel下.Text);
            Settings.instance.selLeft = s2i(sel左.Text);
            Settings.instance.selRight = s2i(sel右.Text);

            Settings.instance.startA = s2i(startA.Text);
            Settings.instance.startB = s2i(startB.Text);
            Settings.instance.startC = s2i(startC.Text);
            Settings.instance.startD = s2i(startD.Text);
            Settings.instance.startATS = s2i(startATS.Text);
            Settings.instance.startUp = s2i(start上.Text);
            Settings.instance.startDown = s2i(start下.Text);
            Settings.instance.startLeft = s2i(start左.Text);
            Settings.instance.startRight = s2i(start右.Text);

            Settings.instance.SelStart = s2i(SelStart.Text);

            Settings.Save(Settings.instance);

            this.Close();
        }


    }
}
