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
            chkEnable = new CheckBox();
            button1 = new Button();
            label1 = new Label();
            buttonSettings = new Button();
            textBox1 = new TextBox();
            buttonClose = new Button();
            SuspendLayout();
            // 
            // chkEnable
            // 
            chkEnable.AutoSize = true;
            chkEnable.Location = new Point(119, 22);
            chkEnable.Margin = new Padding(4);
            chkEnable.Name = "chkEnable";
            chkEnable.Size = new Size(66, 23);
            chkEnable.TabIndex = 0;
            chkEnable.Text = "無効";
            chkEnable.UseVisualStyleBackColor = true;
            chkEnable.CheckedChanged += chkEnable_CheckedChanged;
            // 
            // button1
            // 
            button1.BackgroundImageLayout = ImageLayout.Zoom;
            button1.Location = new Point(12, 15);
            button1.Margin = new Padding(2, 4, 2, 4);
            button1.Name = "button1";
            button1.Size = new Size(81, 82);
            button1.TabIndex = 1;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 243);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(69, 19);
            label1.TabIndex = 2;
            label1.Text = "label1";
            // 
            // buttonSettings
            // 
            buttonSettings.BackgroundImageLayout = ImageLayout.Zoom;
            buttonSettings.Location = new Point(12, 105);
            buttonSettings.Margin = new Padding(2, 4, 2, 4);
            buttonSettings.Name = "buttonSettings";
            buttonSettings.Size = new Size(81, 82);
            buttonSettings.TabIndex = 4;
            buttonSettings.Text = "設定";
            buttonSettings.UseVisualStyleBackColor = true;
            buttonSettings.Click += buttonSettings_Click;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Location = new Point(119, 53);
            textBox1.Margin = new Padding(2, 4, 2, 4);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(291, 174);
            textBox1.TabIndex = 5;
            // 
            // buttonClose
            // 
            buttonClose.BackgroundImageLayout = ImageLayout.Zoom;
            buttonClose.Location = new Point(329, 15);
            buttonClose.Margin = new Padding(2, 4, 2, 4);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(81, 30);
            buttonClose.TabIndex = 4;
            buttonClose.Text = "終了";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(424, 280);
            ControlBox = false;
            Controls.Add(textBox1);
            Controls.Add(buttonClose);
            Controls.Add(buttonSettings);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(chkEnable);
            Font = new Font("BIZ UDゴシック", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
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
        private Button buttonClose;
    }
}
