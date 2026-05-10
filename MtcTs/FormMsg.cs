namespace MtcTs
{
    /// <summary>
    /// メッセージボックス （親フォーム中央に表示、YesNo または エラー）
    /// </summary>
    public partial class FormMsg : Form
    {
        internal FormMsg(Form? parent, string msg, string title, bool bYesNo)
        {
            InitializeComponent();


            Icon = parent?.Icon;
            Text = title;
            labelMsg.Text = msg;

            buttonYes.Text = (bYesNo) ? "はい (Yes)" : "OK";
            buttonNo.Text = "いいえ (No)";
            buttonNo.Visible = bYesNo;

            labelMsg.ForeColor = (bYesNo ? Color.Black : Color.Red);
        }


        public static bool Show(Form? parent, string msg, string title, bool bYesNo)
        {
            using var dlg = new FormMsg(parent, msg, title, bYesNo);
            return dlg.ShowDialog(parent) == DialogResult.Yes;
        }


        private void FormYesNo_Load(object sender, EventArgs e)
        {
            Program.GetIconVer(out string ver, out Icon? icon, out Bitmap? iconBitmap);
            Icon = icon;
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }
    }
}
