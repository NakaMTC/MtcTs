namespace MtcTs
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            chkEnable = new CheckBox();
            button1 = new Button();
            label1 = new Label();
            buttonSettings = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            buttonClose = new Button();
            SuspendLayout();
            // 
            // chkEnable
            // 
            chkEnable.AutoSize = true;
            chkEnable.Location = new Point(95, 12);
            chkEnable.Name = "chkEnable";
            chkEnable.Size = new Size(56, 19);
            chkEnable.TabIndex = 0;
            chkEnable.Text = "無効";
            chkEnable.UseVisualStyleBackColor = true;
            chkEnable.CheckedChanged += chkEnable_CheckedChanged;
            // 
            // button1
            // 
            button1.BackgroundImage = Properties.Resources.MtcTs001;
            button1.BackgroundImageLayout = ImageLayout.Zoom;
            button1.Location = new Point(10, 12);
            button1.Margin = new Padding(2, 3, 2, 3);
            button1.Name = "button1";
            button1.Size = new Size(65, 65);
            button1.TabIndex = 1;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 204);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 2;
            label1.Text = "label1";
            // 
            // buttonSettings
            // 
            buttonSettings.BackgroundImageLayout = ImageLayout.Zoom;
            buttonSettings.Location = new Point(10, 83);
            buttonSettings.Margin = new Padding(2, 3, 2, 3);
            buttonSettings.Name = "buttonSettings";
            buttonSettings.Size = new Size(65, 65);
            buttonSettings.TabIndex = 4;
            buttonSettings.Text = "設定";
            buttonSettings.UseVisualStyleBackColor = true;
            buttonSettings.Click += buttonSettings_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(95, 34);
            textBox1.Margin = new Padding(2, 3, 2, 3);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(174, 159);
            textBox1.TabIndex = 5;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(297, 64);
            textBox2.Margin = new Padding(2, 3, 2, 3);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.ScrollBars = ScrollBars.Vertical;
            textBox2.Size = new Size(229, 129);
            textBox2.TabIndex = 5;
            // 
            // buttonClose
            // 
            buttonClose.BackgroundImageLayout = ImageLayout.Zoom;
            buttonClose.Location = new Point(461, 12);
            buttonClose.Margin = new Padding(2, 3, 2, 3);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(65, 31);
            buttonClose.TabIndex = 4;
            buttonClose.Text = "終了";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(541, 259);
            ControlBox = false;
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(buttonClose);
            Controls.Add(buttonSettings);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(chkEnable);
            Font = new Font("BIZ UDゴシック", 11.25F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox chkEnable;
        private Button button1;
        private Label label1;
        private Button buttonSettings;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button buttonClose;
    }
}
