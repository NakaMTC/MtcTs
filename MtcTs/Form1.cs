using System.Diagnostics;

namespace MtcTs
{
    public partial class Form1 : Form
    {

        /// <summary> 終了ボタン </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary> 終了の有効・無効の切替 </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(chkEnable.Checked)
            {
                // 有効にチェック → チェックを外す。 終了は無効
                chkEnable.Checked = false;
                e.Cancel = true;
            }
            else
            {
                e.Cancel = !FormYesNo.Show(this, "終了します。よろしいですか？" , "修了確認");
            }
        }




        /// <summary>　MTCを有効にするかどうか？ </summary>
        private bool m_Enable = false;

        /// <summary>　接続OKかどうか？ </summary>
        private bool m_ConnectOK = false;

        private string m_LavelText = "";



        /// <summary>　MTCを有効にするかどうか？ のチェック </summary>
        private void chkEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (m_Enable == true && chkEnable.Checked == false)
            {
                if (FormYesNo.Show(this, "有効 → 無効 に切り替えます。よろしいですか？", "切替確認") == false)
                {
                    chkEnable.Checked = true;
                    return;
                }
            }

            m_Enable = chkEnable.Checked;
            chkEnable.Text = (m_Enable ? "有効" : "無効");
            if (m_Enable) textBox1.Select();

            OnKeyUpAll();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            if (chkEnable.Checked)
            {
                chkEnable.Checked = false;
            }
            else
            {
                using (var form = new FormSettings() { Icon = this.Icon })
                {
                    form.ShowDialog(this);
                }
            }
        }



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "MTC";
            label1.Text = "";

            Task.Run(() =>
            {
                while (true)
                {
                    bool res = Usb.Read();
                    if (res) res = MTC.Read();

                    string str = Usb.ToText() + " " + MTC.ToText();

                    if (m_Enable == false && str != m_LavelText)
                    {
                        Invoke(() => label1.Text = str);
                        m_LavelText = str;
                    }


                    if (Usb.m_Uint32 == 0x00) Thread.Sleep(5000);
                }
            });
        }


        /// <summary> ボタンの状態 </summary>        
        List<int> pushedKeys = [];

        private void OnKeyDownUp()
        {
            List<int> nowKeys = [];

            if (MTC.m_SelStartMode == eSelectStartType.Non)
            {
                if (MTC.a) nowKeys.Add(Settings.instance.a);
                if (MTC.aa) nowKeys.Add(Settings.instance.aa);
                if (MTC.b) nowKeys.Add(Settings.instance.b);
                if (MTC.c) nowKeys.Add(Settings.instance.c);
                if (MTC.d) nowKeys.Add(Settings.instance.d);
                if (MTC.ats) nowKeys.Add(Settings.instance.ats);
                if (MTC.up) nowKeys.Add(Settings.instance.up);
                if (MTC.down) nowKeys.Add(Settings.instance.down);
                if (MTC.left) nowKeys.Add(Settings.instance.left);
                if (MTC.right) nowKeys.Add(Settings.instance.right);
            }
            else if (MTC.m_SelStartMode == eSelectStartType.Sel)
            {
                if (MTC.a) nowKeys.Add(Settings.instance.selA);
                if (MTC.b) nowKeys.Add(Settings.instance.selB);
                if (MTC.c) nowKeys.Add(Settings.instance.selC);
                if (MTC.d) nowKeys.Add(Settings.instance.selD);
                if (MTC.ats) nowKeys.Add(Settings.instance.selATS);
                if (MTC.up) nowKeys.Add(Settings.instance.selUP);
                if (MTC.down) nowKeys.Add(Settings.instance.selDown);
                if (MTC.left) nowKeys.Add(Settings.instance.selLeft);
                if (MTC.right) nowKeys.Add(Settings.instance.selRight);
            }
            else if (MTC.m_SelStartMode == eSelectStartType.Start)
            {
                if (MTC.a) nowKeys.Add(Settings.instance.startA);
                if (MTC.b) nowKeys.Add(Settings.instance.startB);
                if (MTC.c) nowKeys.Add(Settings.instance.startC);
                if (MTC.d) nowKeys.Add(Settings.instance.startD);
                if (MTC.ats) nowKeys.Add(Settings.instance.startATS);
                if (MTC.up) nowKeys.Add(Settings.instance.startUp);
                if (MTC.down) nowKeys.Add(Settings.instance.startDown);
                if (MTC.left) nowKeys.Add(Settings.instance.startLeft);
                if (MTC.right) nowKeys.Add(Settings.instance.startRight);
            }

            if (MTC.selStartBoth) nowKeys.Add(Settings.instance.SelStart);


            if (nowKeys.Contains((int)eKeyTypes.WinAltR_録画))
            {
                nowKeys.Add((int)eKeyTypes.Win);
                nowKeys.Add((int)eKeyTypes.Alt);
                nowKeys.Add('R');
            }

            if (nowKeys.Contains((int)eKeyTypes.WinG_ゲームバー))
            {
                nowKeys.Add((int)eKeyTypes.Win);
                nowKeys.Add('G');
            }

            if (nowKeys.Contains((int)eKeyTypes.EB_Bブザー))
            {
                //if(Co.)
            }

            nowKeys = nowKeys.Where(x => x != 0).Distinct().ToList();

            if (nowKeys.Count == 0 && pushedKeys.Count == 0) return;

            IEnumerable<int> downs = nowKeys.Where(x => pushedKeys.Contains(x) == false);
            OnKeyDown(downs);

            IEnumerable<int> ups = pushedKeys.Where(x => nowKeys.Contains(x) == false);
            OnKeyUp(ups);

            pushedKeys = nowKeys;
        }

        private void OnKeyUpAll()
        {
            OnKeyUp(pushedKeys);
            pushedKeys.Clear();
        }

        private void OnKeyDown(IEnumerable<int> keys)
        {
            if (keys.Count() <= 0) return;
            foreach (int key in keys)
            {

                Debug.WriteLine($"OnKeyDown {key} , {m_Enable}");
            }


            ShowKeys("+", keys);
        }



        private void OnKeyUp(IEnumerable<int> keys)
        {
            if (keys.Count() <= 0) return;
            foreach (int key in keys)
            {
                Debug.WriteLine($"OnKeyUp {key} , {m_Enable}");
            }
            ShowKeys("-", keys);
        }


        private void ShowKeys(string text, IEnumerable<int> keys)
        {
            //if (m_Enable) return;

            string tmp = "";
            foreach (int key in keys)
            {
                tmp += text;
                try
                {
                    eKeyTypes type = (eKeyTypes)Enum.ToObject(typeof(eKeyTypes), key);
                    tmp += $" {type}";
                }
                catch { }

                if (key >= (int)'A' && key <= (int)'Z') tmp += $" ({(char)key})\r\n";
                else tmp += $" ({key})\r\n";
            }


            Invoke(() =>
            {
                textBox2.Text += tmp;

                textBox2.SelectionStart = textBox2.Text.Length;

                textBox2.ScrollToCaret();
            });

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (chkEnable.Checked == false)
            {
                chkEnable.Checked = true;
                return;
            }

            if (m_Enable == false || m_ConnectOK == false) return;


            MessageBox.Show("aa");
        }


    }
}
