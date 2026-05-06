using System.Diagnostics;
using System.Reflection;

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
                e.Cancel = !FormMsg.Show(this, "終了します。よろしいですか？", "修了確認", true);
            }
        }




        /// <summary>　MTCを有効にするかどうか？ </summary>
        private bool m_Enable = false;


        private string m_LabelText = "";



        /// <summary>　MTCを有効にするかどうか？ のチェック </summary>
        private void chkEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (m_Enable == true && chkEnable.Checked == false)
            {
                if (FormMsg.Show(this, "有効 → 無効 に切り替えます。よろしいですか？", "切替確認", true) == false)
                {
                    chkEnable.Checked = true;
                    return;
                }
            }

            m_Enable = chkEnable.Checked;
            chkEnable.Text = (m_Enable ? "有効" : "無効");
            if (m_Enable) textBox1.Select();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            if (chkEnable.Checked)
            {
                chkEnable.Checked = false;
            }
            else
            {
                using var form = new FormSettings() { Icon = this.Icon };
                form.ShowDialog(this);
            }
        }



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Program.GetIconVer(out string ver, out Icon? icon, out Bitmap? iconBitmap);
            Icon = icon;
            this.button1.BackgroundImage = iconBitmap;
            Text = $"MtcTs - ver.{ver}";


            label1.Text = "";

            Task.Run(() =>
            {
                while (true)
                {
                    bool res = Usb.Read();
                    if (res) res = MTC.Read();

                    if(res)
                    {
                        if(Usb.m_Uint32 != 0x00 &&  m_Enable)
                        {
                            MtcKeyMouse.OnKeyDownUp();
                            MtcKeyMouse.OnKeyFR();
                            MtcKeyMouse.OnValChange();
                        }
                        else
                        {
                            MtcKeyMouse.OnKeyClear();
                        }
                    }

                    if (m_Enable == false)
                    {
                        string str = Usb.ToText() + " " + MTC.ToText();

                        if (str != m_LabelText)
                        {
                            Invoke(() => label1.Text = str);
                            m_LabelText = str;
                        }
                    }

                    if (Usb.m_Uint32 == 0x00 ) Thread.Sleep(5000);
                    else if (m_Enable == false) Thread.Sleep(100);
                    
                }
            });
        }


        private void button1_Click(object sender, EventArgs e)
        {

            textBox1.Select();
            if (chkEnable.Checked == false)
            {
                chkEnable.Checked = true;
                return;
            }

            if (m_Enable == false || Usb.m_ConnectOK == false) return;


            var info = new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = Settings.instance.GameUrl,
            };
            Process.Start(info);

            
                        
        }


    }
}
