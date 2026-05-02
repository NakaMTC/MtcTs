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
            SuspendLayout();
            // 
            // chkEnable
            // 
            chkEnable.AutoSize = true;
            chkEnable.Location = new Point(94, 12);
            chkEnable.Margin = new Padding(4, 3, 4, 3);
            chkEnable.Name = "chkEnable";
            chkEnable.Size = new Size(56, 19);
            chkEnable.TabIndex = 0;
            chkEnable.Text = "無効";
            chkEnable.UseVisualStyleBackColor = true;
            chkEnable.CheckedChanged += chkEnable_CheckedChanged;
            // 
            // button1
            // 
            button1.BackgroundImageLayout = ImageLayout.Zoom;
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(75, 65);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 158);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 2;
            label1.Text = "label1";
            // 
            // buttonSettings
            // 
            buttonSettings.BackgroundImageLayout = ImageLayout.Zoom;
            buttonSettings.Location = new Point(380, 39);
            buttonSettings.Name = "buttonSettings";
            buttonSettings.Size = new Size(75, 65);
            buttonSettings.TabIndex = 4;
            buttonSettings.Text = "設定";
            buttonSettings.UseVisualStyleBackColor = true;
            buttonSettings.Click += buttonSettings_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(506, 182);
            Controls.Add(buttonSettings);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(chkEnable);
            Font = new Font("BIZ UDPゴシック", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox chkEnable;
        private Button button1;
        private Label label1;
        private Button buttonSettings;
    }
}
