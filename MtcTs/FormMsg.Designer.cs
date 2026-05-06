namespace MtcTs
{
    partial class FormMsg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelMsg = new Label();
            buttonYes = new Button();
            buttonNo = new Button();
            SuspendLayout();
            // 
            // labelMsg
            // 
            labelMsg.AutoSize = true;
            labelMsg.Location = new Point(16, 11);
            labelMsg.Margin = new Padding(4, 0, 4, 0);
            labelMsg.Name = "labelMsg";
            labelMsg.Size = new Size(367, 25);
            labelMsg.TabIndex = 0;
            labelMsg.Text = "有効 → 無効 に切り替えます。よろしいですか？";
            // 
            // buttonYes
            // 
            buttonYes.Location = new Point(16, 55);
            buttonYes.Margin = new Padding(4, 4, 4, 4);
            buttonYes.Name = "buttonYes";
            buttonYes.Size = new Size(138, 42);
            buttonYes.TabIndex = 1;
            buttonYes.TabStop = false;
            buttonYes.Text = "はい　Yes";
            buttonYes.UseVisualStyleBackColor = true;
            buttonYes.Click += buttonYes_Click;
            // 
            // buttonNo
            // 
            buttonNo.Location = new Point(177, 55);
            buttonNo.Margin = new Padding(4, 4, 4, 4);
            buttonNo.Name = "buttonNo";
            buttonNo.Size = new Size(138, 42);
            buttonNo.TabIndex = 1;
            buttonNo.TabStop = false;
            buttonNo.Text = "いいえ No";
            buttonNo.UseVisualStyleBackColor = true;
            buttonNo.Click += buttonNo_Click;
            // 
            // FormMsg
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 119);
            ControlBox = false;
            Controls.Add(buttonNo);
            Controls.Add(buttonYes);
            Controls.Add(labelMsg);
            Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 5, 4, 5);
            Name = "FormYesNo";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FormYesNo";
            Load += FormYesNo_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelMsg;
        private Button buttonYes;
        private Button buttonNo;
    }
}