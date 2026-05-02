using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MtcTs
{
    public partial class FormSettings : Form
    {
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
            foreach (Panel panel in new[] { panel1, panel2, panel3 })
            {

                foreach (Control control in panel.Controls)
                {
                    ComboBox? comboBox = control as ComboBox;
                    if (comboBox != null) InitComboBox(comboBox);
                }
            }

            InitComboBox(SelStart);

            LoadComboBox();
        }

        public class ValTxt
        {
            public int Val { get; set; }
            public string Txt { get; set; }

            public ValTxt(int val, string txt)
            {
                Val = val;
                Txt = txt;
            }
        }

        static List<ValTxt> m_List = [];

        private void InitComboBox(ComboBox comboBox)
        {

            if (m_List.Count <= 0)
            {
                m_List.Add(new(0, "--"));
                foreach (eKeyTypes keyType in Enum.GetValues(typeof(eKeyTypes)))
                {
                    m_List.Add(new((int)keyType, keyType.ToString()));
                }
            }

            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.DataSource = m_List.ToArray();
            comboBox.DisplayMember = "txt";
            comboBox.ValueMember = "val";
        }

        private void LoadComboBox()
        {
            A.SelectedValue = Common.settings.a;
            A強.SelectedValue = Common.settings.aa;
            B.SelectedValue = Common.settings.b;
            C.SelectedValue = Common.settings.c;
            D.SelectedValue = Common.settings.d;
            ATS.SelectedValue = Common.settings.ats;
            上.SelectedValue = Common.settings.up;
            下.SelectedValue = Common.settings.down;
            左.SelectedValue = Common.settings.left;
            右.SelectedValue = Common.settings.right;

            selA.SelectedValue = Common.settings.selA;
            selB.SelectedValue = Common.settings.selB;
            selC.SelectedValue = Common.settings.selC;
            selD.SelectedValue = Common.settings.selD;
            selATS.SelectedValue = Common.settings.selATS;
            sel上.SelectedValue = Common.settings.selUP;
            sel下.SelectedValue = Common.settings.selDown;
            sel左.SelectedValue = Common.settings.selLeft;
            sel右.SelectedValue = Common.settings.selRight;

            startA.SelectedValue = Common.settings.startA;
            startB.SelectedValue = Common.settings.startB;
            startC.SelectedValue = Common.settings.startC;
            startD.SelectedValue = Common.settings.startD;
            startATS.SelectedValue = Common.settings.startATS;
            start上.SelectedValue = Common.settings.startUp;
            start下.SelectedValue = Common.settings.startDown;
            start左.SelectedValue = Common.settings.startLeft;
            start右.SelectedValue = Common.settings.startRight;

            SelStart.SelectedValue = Common.settings.SelStart;
        }

        private void buttonInit_Click(object sender, EventArgs e)
        {
            Common.settings.Init();
            LoadComboBox();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Common.settings.a = A.SelectedValue as int? ?? 0;
            Common.settings.aa = A強.SelectedValue as int? ?? 0;
            Common.settings.b = B.SelectedValue as int? ?? 0;
            Common.settings.c = C.SelectedValue as int? ?? 0;
            Common.settings.d = D.SelectedValue as int? ?? 0;
            Common.settings.ats = ATS.SelectedValue as int? ?? 0;
            Common.settings.up = 上.SelectedValue as int? ?? 0;
            Common.settings.down = 下.SelectedValue as int? ?? 0;
            Common.settings.left = 左.SelectedValue as int? ?? 0;
            Common.settings.right = 右.SelectedValue as int? ?? 0;

            Common.settings.selA = selA.SelectedValue as int? ?? 0;
            Common.settings.selB = selB.SelectedValue as int? ?? 0;
            Common.settings.selC = selC.SelectedValue as int? ?? 0;
            Common.settings.selD = selD.SelectedValue as int? ?? 0;
            Common.settings.selATS = selATS.SelectedValue as int? ?? 0;
            Common.settings.selUP = sel上.SelectedValue as int? ?? 0;
            Common.settings.selDown = sel下.SelectedValue as int? ?? 0;
            Common.settings.selLeft = sel左.SelectedValue as int? ?? 0;
            Common.settings.selRight = sel右.SelectedValue as int? ?? 0;

            Common.settings.startA = startA.SelectedValue as int? ?? 0;
            Common.settings.startB = startB.SelectedValue as int? ?? 0;
            Common.settings.startC = startC.SelectedValue as int? ?? 0;
            Common.settings.startD = startD.SelectedValue as int? ?? 0;
            Common.settings.startATS = startATS.SelectedValue as int? ?? 0;
            Common.settings.startUp = start上.SelectedValue as int? ?? 0;
            Common.settings.startDown = start下.SelectedValue as int? ?? 0;
            Common.settings.startLeft = start左.SelectedValue as int? ?? 0;
            Common.settings.startRight = start右.SelectedValue as int? ?? 0;

            Common.settings.SelStart = SelStart.SelectedValue as int? ?? 0;

            Settings.Save(Common.settings);

            this.Close();
        }


    }
}
