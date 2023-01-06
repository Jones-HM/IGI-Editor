
namespace IGIEditor
{
    partial class DialogMsgBox
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
            this.dialogBoxPanelMover = new System.Windows.Forms.Panel();
            this.dialogBoxPanel = new System.Windows.Forms.Panel();
            this.dialogInputBoxTxt = new System.Windows.Forms.TextBox();
            this.dialogBoxBtnYes = new System.Windows.Forms.Button();
            this.dialogBoxMsg = new System.Windows.Forms.Label();
            this.dialogBoxBtnNo = new System.Windows.Forms.Button();
            this.dialogBoxTitle = new System.Windows.Forms.Label();
            this.dialogBoxPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dialogBoxPanelMover
            // 
            this.dialogBoxPanelMover.BackColor = System.Drawing.SystemColors.HotTrack;
            this.dialogBoxPanelMover.Dock = System.Windows.Forms.DockStyle.Top;
            this.dialogBoxPanelMover.Location = new System.Drawing.Point(0, 0);
            this.dialogBoxPanelMover.Name = "dialogBoxPanelMover";
            this.dialogBoxPanelMover.Size = new System.Drawing.Size(451, 8);
            this.dialogBoxPanelMover.TabIndex = 8;
            this.dialogBoxPanelMover.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dialogBoxPanel_MouseDown);
            // 
            // dialogBoxPanel
            // 
            this.dialogBoxPanel.AutoSize = true;
            this.dialogBoxPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dialogBoxPanel.BackColor = System.Drawing.SystemColors.Control;
            this.dialogBoxPanel.Controls.Add(this.dialogInputBoxTxt);
            this.dialogBoxPanel.Controls.Add(this.dialogBoxBtnYes);
            this.dialogBoxPanel.Controls.Add(this.dialogBoxMsg);
            this.dialogBoxPanel.Controls.Add(this.dialogBoxBtnNo);
            this.dialogBoxPanel.Controls.Add(this.dialogBoxTitle);
            this.dialogBoxPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dialogBoxPanel.Location = new System.Drawing.Point(0, 8);
            this.dialogBoxPanel.Name = "dialogBoxPanel";
            this.dialogBoxPanel.Padding = new System.Windows.Forms.Padding(15, 15, 15, 65);
            this.dialogBoxPanel.Size = new System.Drawing.Size(451, 144);
            this.dialogBoxPanel.TabIndex = 10;
            // 
            // dialogInputBoxTxt
            // 
            this.dialogInputBoxTxt.BackColor = System.Drawing.SystemColors.Control;
            this.dialogInputBoxTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dialogInputBoxTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.dialogInputBoxTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.dialogInputBoxTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dialogInputBoxTxt.HideSelection = false;
            this.dialogInputBoxTxt.Location = new System.Drawing.Point(13, 47);
            this.dialogInputBoxTxt.Margin = new System.Windows.Forms.Padding(4);
            this.dialogInputBoxTxt.Name = "dialogInputBoxTxt";
            this.dialogInputBoxTxt.Size = new System.Drawing.Size(426, 30);
            this.dialogInputBoxTxt.TabIndex = 16;
            this.dialogInputBoxTxt.Visible = false;
            // 
            // dialogBoxBtnYes
            // 
            this.dialogBoxBtnYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dialogBoxBtnYes.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.dialogBoxBtnYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dialogBoxBtnYes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F);
            this.dialogBoxBtnYes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dialogBoxBtnYes.Location = new System.Drawing.Point(229, 101);
            this.dialogBoxBtnYes.Name = "dialogBoxBtnYes";
            this.dialogBoxBtnYes.Size = new System.Drawing.Size(102, 30);
            this.dialogBoxBtnYes.TabIndex = 14;
            this.dialogBoxBtnYes.Text = "YES";
            this.dialogBoxBtnYes.UseVisualStyleBackColor = true;
            this.dialogBoxBtnYes.Click += new System.EventHandler(this.dialogBoxBtnYes_Click);
            // 
            // dialogBoxMsg
            // 
            this.dialogBoxMsg.AutoSize = true;
            this.dialogBoxMsg.BackColor = System.Drawing.SystemColors.Control;
            this.dialogBoxMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dialogBoxMsg.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dialogBoxMsg.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dialogBoxMsg.Location = new System.Drawing.Point(15, 44);
            this.dialogBoxMsg.MaximumSize = new System.Drawing.Size(480, 0);
            this.dialogBoxMsg.Name = "dialogBoxMsg";
            this.dialogBoxMsg.Size = new System.Drawing.Size(232, 22);
            this.dialogBoxMsg.TabIndex = 11;
            this.dialogBoxMsg.Text = "Content of the message";
            // 
            // dialogBoxBtnNo
            // 
            this.dialogBoxBtnNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dialogBoxBtnNo.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.dialogBoxBtnNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dialogBoxBtnNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F);
            this.dialogBoxBtnNo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dialogBoxBtnNo.Location = new System.Drawing.Point(337, 101);
            this.dialogBoxBtnNo.Name = "dialogBoxBtnNo";
            this.dialogBoxBtnNo.Size = new System.Drawing.Size(102, 30);
            this.dialogBoxBtnNo.TabIndex = 12;
            this.dialogBoxBtnNo.Text = "OK";
            this.dialogBoxBtnNo.UseVisualStyleBackColor = true;
            this.dialogBoxBtnNo.Click += new System.EventHandler(this.dialogBoxBtnNo_Click);
            // 
            // dialogBoxTitle
            // 
            this.dialogBoxTitle.AutoSize = true;
            this.dialogBoxTitle.BackColor = System.Drawing.Color.Transparent;
            this.dialogBoxTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.dialogBoxTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dialogBoxTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dialogBoxTitle.Location = new System.Drawing.Point(15, 15);
            this.dialogBoxTitle.Name = "dialogBoxTitle";
            this.dialogBoxTitle.Size = new System.Drawing.Size(66, 29);
            this.dialogBoxTitle.TabIndex = 10;
            this.dialogBoxTitle.Text = "Title";
            // 
            // DialogMsgBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(451, 152);
            this.Controls.Add(this.dialogBoxPanel);
            this.Controls.Add(this.dialogBoxPanelMover);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(199, 91);
            this.Name = "DialogMsgBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MsgBox";
            this.TopMost = true;
            this.dialogBoxPanel.ResumeLayout(false);
            this.dialogBoxPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel dialogBoxPanelMover;
        private System.Windows.Forms.Panel dialogBoxPanel;
        public System.Windows.Forms.Label dialogBoxMsg;
        public System.Windows.Forms.Label dialogBoxTitle;
        private System.Windows.Forms.Button dialogBoxBtnNo;
        private System.Windows.Forms.Button dialogBoxBtnYes;
        private System.Windows.Forms.TextBox dialogInputBoxTxt;
    }
}