namespace PowerPointRemote.AddIn.Views
{
    partial class ChannelEndedView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblChannelEnded = new System.Windows.Forms.Label();
            this.btnNewRemote = new System.Windows.Forms.Button();
            this.btnCloseAddIn = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblChannelEnded
            // 
            this.lblChannelEnded.AutoSize = true;
            this.lblChannelEnded.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChannelEnded.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(71)))), ((int)(((byte)(42)))));
            this.lblChannelEnded.Location = new System.Drawing.Point(6, 25);
            this.lblChannelEnded.Name = "lblChannelEnded";
            this.lblChannelEnded.Size = new System.Drawing.Size(171, 32);
            this.lblChannelEnded.TabIndex = 4;
            this.lblChannelEnded.Text = "Remote Ended";
            // 
            // btnNewRemote
            // 
            this.btnNewRemote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewRemote.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewRemote.Location = new System.Drawing.Point(25, 100);
            this.btnNewRemote.Name = "btnNewRemote";
            this.btnNewRemote.Size = new System.Drawing.Size(403, 48);
            this.btnNewRemote.TabIndex = 17;
            this.btnNewRemote.Text = "Start New Remote";
            this.btnNewRemote.UseVisualStyleBackColor = true;
            this.btnNewRemote.Click += new System.EventHandler(this.OnNewRemoteClick);
            // 
            // btnCloseAddIn
            // 
            this.btnCloseAddIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseAddIn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseAddIn.Location = new System.Drawing.Point(25, 170);
            this.btnCloseAddIn.Name = "btnCloseAddIn";
            this.btnCloseAddIn.Size = new System.Drawing.Size(403, 48);
            this.btnCloseAddIn.TabIndex = 18;
            this.btnCloseAddIn.Text = "Close Add-In";
            this.btnCloseAddIn.UseVisualStyleBackColor = true;
            this.btnCloseAddIn.Click += new System.EventHandler(this.OnCloseAddInClick);
            // 
            // panelMain
            // 
            this.panelMain.AutoScroll = true;
            this.panelMain.Controls.Add(this.lblChannelEnded);
            this.panelMain.Controls.Add(this.btnNewRemote);
            this.panelMain.Controls.Add(this.btnCloseAddIn);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(450, 950);
            this.panelMain.TabIndex = 19;
            // 
            // ChannelEndedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Name = "ChannelEndedView";
            this.Size = new System.Drawing.Size(450, 950);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblChannelEnded;
        private System.Windows.Forms.Button btnNewRemote;
        private System.Windows.Forms.Button btnCloseAddIn;
        private System.Windows.Forms.Panel panelMain;
    }
}
