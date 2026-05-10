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
            chkEnable.Font = new Font("BIZ UDゴシック", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 128);
            chkEnable.ForeColor = Color.Snow;
            chkEnable.Location = new Point(167, 28);
            chkEnable.Margin = new Padding(6, 6, 6, 6);
            chkEnable.Name = "chkEnable";
            chkEnable.Size = new Size(71, 25);
            chkEnable.TabIndex = 0;
            chkEnable.Text = "無効";
            chkEnable.UseVisualStyleBackColor = false;
            chkEnable.CheckedChanged += chkEnable_CheckedChanged;
            // 
            // button1
            // 
            button1.BackColor = Color.White;
            button1.BackgroundImageLayout = ImageLayout.Zoom;
            button1.Location = new Point(17, 21);
            button1.Margin = new Padding(3, 6, 3, 6);
            button1.Name = "button1";
            button1.Size = new Size(113, 117);
            button1.TabIndex = 1;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(17, 345);
            label1.Name = "label1";
            label1.Size = new Size(96, 27);
            label1.TabIndex = 2;
            label1.Text = "label1";
            // 
            // buttonSettings
            // 
            buttonSettings.BackgroundImageLayout = ImageLayout.Zoom;
            buttonSettings.Location = new Point(17, 149);
            buttonSettings.Margin = new Padding(3, 6, 3, 6);
            buttonSettings.Name = "buttonSettings";
            buttonSettings.Size = new Size(113, 117);
            buttonSettings.TabIndex = 4;
            buttonSettings.Text = "設定";
            buttonSettings.UseVisualStyleBackColor = true;
            buttonSettings.Click += buttonSettings_Click;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Location = new Point(167, 75);
            textBox1.Margin = new Padding(3, 6, 3, 6);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(453, 246);
            textBox1.TabIndex = 5;
            // 
            // buttonClose
            // 
            buttonClose.BackgroundImageLayout = ImageLayout.Zoom;
            buttonClose.Location = new Point(507, 23);
            buttonClose.Margin = new Padding(3, 6, 3, 6);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(113, 43);
            buttonClose.TabIndex = 4;
            buttonClose.Text = "終了";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(14F, 27F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGreen;
            ClientSize = new Size(636, 398);
            ControlBox = false;
            Controls.Add(textBox1);
            Controls.Add(buttonClose);
            Controls.Add(buttonSettings);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(chkEnable);
            Font = new Font("BIZ UDゴシック", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(6, 6, 6, 6);
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
