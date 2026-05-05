namespace MtcTs
{
    partial class FormYesNo
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
            labelMsg.Location = new Point(12, 9);
            labelMsg.Name = "labelMsg";
            labelMsg.Size = new Size(288, 20);
            labelMsg.TabIndex = 0;
            labelMsg.Text = "有効 → 無効 に切り替えます。よろしいですか？";
            // 
            // buttonYes
            // 
            buttonYes.Location = new Point(12, 44);
            buttonYes.Name = "buttonYes";
            buttonYes.Size = new Size(100, 34);
            buttonYes.TabIndex = 1;
            buttonYes.TabStop = false;
            buttonYes.Text = "はい　Yes";
            buttonYes.UseVisualStyleBackColor = true;
            buttonYes.Click += buttonYes_Click;
            // 
            // buttonNo
            // 
            buttonNo.Location = new Point(129, 44);
            buttonNo.Name = "buttonNo";
            buttonNo.Size = new Size(100, 34);
            buttonNo.TabIndex = 1;
            buttonNo.TabStop = false;
            buttonNo.Text = "いいえ No";
            buttonNo.UseVisualStyleBackColor = true;
            buttonNo.Click += buttonNo_Click;
            // 
            // FormYesNo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(316, 95);
            ControlBox = false;
            Controls.Add(buttonNo);
            Controls.Add(buttonYes);
            Controls.Add(labelMsg);
            Font = new Font("Yu Gothic UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
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