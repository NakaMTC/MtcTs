namespace MtcTs
{
    public partial class FormYesNo : Form
    {

        public static bool Show(Form parent, string msg, string title)
        {
            using (var dlg = new FormYesNo { })
            {
                dlg.Icon = parent.Icon;
                dlg.Text = title;
                dlg.labelMsg.Text = msg;
                return dlg.ShowDialog(parent) == DialogResult.Yes;
            }
        }

        private FormYesNo()
        {
            InitializeComponent();
        }

        private void FormYesNo_Load(object sender, EventArgs e)
        {

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
