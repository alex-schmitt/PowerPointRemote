namespace PowerPointRemote.AddIn.Views
{
    partial class ChannelView
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
            this.lblManualConnect = new System.Windows.Forms.Label();
            this.txtClientAddress = new System.Windows.Forms.TextBox();
            this.lblNavTo2 = new System.Windows.Forms.Label();
            this.lblEnterId = new System.Windows.Forms.Label();
            this.channelId = new System.Windows.Forms.TextBox();
            this.lblMonitor = new System.Windows.Forms.Label();
            this.lblRemoteConnections = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStatusValue = new System.Windows.Forms.Label();
            this.lblRemoteConnectionsValue = new System.Windows.Forms.Label();
            this.btnEndRemote = new System.Windows.Forms.Button();
            this.PanelManualConnect = new System.Windows.Forms.Panel();
            this.dividerManualConnect = new System.Windows.Forms.Label();
            this.PanelMonitor = new System.Windows.Forms.Panel();
            this.listBoxConnections = new System.Windows.Forms.ListBox();
            this.PanelControls = new System.Windows.Forms.Panel();
            this.PanelMain = new System.Windows.Forms.Panel();
            this.PanelQuickConnect = new System.Windows.Forms.Panel();
            this.dividerQuickConnect = new System.Windows.Forms.Label();
            this.lblQuickConnect = new System.Windows.Forms.Label();
            this.lblNavTo1 = new System.Windows.Forms.Label();
            this.txtChannelUri = new System.Windows.Forms.TextBox();
            this.btnCopyUri = new System.Windows.Forms.Button();
            this.PanelLoading = new System.Windows.Forms.Panel();
            this.pictureBoxSpinner = new System.Windows.Forms.PictureBox();
            this.PanelManualConnect.SuspendLayout();
            this.PanelMonitor.SuspendLayout();
            this.PanelControls.SuspendLayout();
            this.PanelMain.SuspendLayout();
            this.PanelQuickConnect.SuspendLayout();
            this.PanelLoading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // lblManualConnect
            // 
            this.lblManualConnect.AutoSize = true;
            this.lblManualConnect.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblManualConnect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(71)))), ((int)(((byte)(42)))));
            this.lblManualConnect.Location = new System.Drawing.Point(6, 25);
            this.lblManualConnect.Name = "lblManualConnect";
            this.lblManualConnect.Size = new System.Drawing.Size(191, 32);
            this.lblManualConnect.TabIndex = 4;
            this.lblManualConnect.Text = "Manual Connect";
            // 
            // txtClientAddress
            // 
            this.txtClientAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.txtClientAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtClientAddress.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClientAddress.Location = new System.Drawing.Point(12, 106);
            this.txtClientAddress.Name = "txtClientAddress";
            this.txtClientAddress.ReadOnly = true;
            this.txtClientAddress.Size = new System.Drawing.Size(311, 27);
            this.txtClientAddress.TabIndex = 6;
            this.txtClientAddress.Text = "[ Web Client ]";
            // 
            // lblNavTo2
            // 
            this.lblNavTo2.AutoSize = true;
            this.lblNavTo2.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNavTo2.Location = new System.Drawing.Point(6, 70);
            this.lblNavTo2.Name = "lblNavTo2";
            this.lblNavTo2.Size = new System.Drawing.Size(134, 30);
            this.lblNavTo2.TabIndex = 5;
            this.lblNavTo2.Text = "Navigate to:";
            // 
            // lblEnterId
            // 
            this.lblEnterId.AutoSize = true;
            this.lblEnterId.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnterId.Location = new System.Drawing.Point(6, 152);
            this.lblEnterId.Name = "lblEnterId";
            this.lblEnterId.Size = new System.Drawing.Size(205, 30);
            this.lblEnterId.TabIndex = 7;
            this.lblEnterId.Text = "Enter slide show id:";
            // 
            // channelId
            // 
            this.channelId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.channelId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.channelId.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.channelId.Location = new System.Drawing.Point(12, 188);
            this.channelId.Name = "channelId";
            this.channelId.ReadOnly = true;
            this.channelId.Size = new System.Drawing.Size(311, 27);
            this.channelId.TabIndex = 8;
            this.channelId.Text = "[ Channel ID ]";
            // 
            // lblMonitor
            // 
            this.lblMonitor.AutoSize = true;
            this.lblMonitor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonitor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(71)))), ((int)(((byte)(42)))));
            this.lblMonitor.Location = new System.Drawing.Point(6, 25);
            this.lblMonitor.Name = "lblMonitor";
            this.lblMonitor.Size = new System.Drawing.Size(101, 32);
            this.lblMonitor.TabIndex = 9;
            this.lblMonitor.Text = "Monitor";
            // 
            // lblRemoteConnections
            // 
            this.lblRemoteConnections.AutoSize = true;
            this.lblRemoteConnections.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemoteConnections.Location = new System.Drawing.Point(6, 112);
            this.lblRemoteConnections.Name = "lblRemoteConnections";
            this.lblRemoteConnections.Size = new System.Drawing.Size(225, 30);
            this.lblRemoteConnections.TabIndex = 10;
            this.lblRemoteConnections.Text = "Remote Connections:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(6, 70);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(84, 30);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "Status: ";
            // 
            // lblStatusValue
            // 
            this.lblStatusValue.AutoSize = true;
            this.lblStatusValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusValue.Location = new System.Drawing.Point(81, 71);
            this.lblStatusValue.Name = "lblStatusValue";
            this.lblStatusValue.Size = new System.Drawing.Size(187, 28);
            this.lblStatusValue.TabIndex = 12;
            this.lblStatusValue.Text = "[ ConnectionStatus ]";
            // 
            // lblRemoteConnectionsValue
            // 
            this.lblRemoteConnectionsValue.AutoSize = true;
            this.lblRemoteConnectionsValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemoteConnectionsValue.Location = new System.Drawing.Point(228, 113);
            this.lblRemoteConnectionsValue.Name = "lblRemoteConnectionsValue";
            this.lblRemoteConnectionsValue.Size = new System.Drawing.Size(87, 28);
            this.lblRemoteConnectionsValue.TabIndex = 14;
            this.lblRemoteConnectionsValue.Text = "[ Count ]";
            // 
            // btnEndRemote
            // 
            this.btnEndRemote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEndRemote.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEndRemote.Location = new System.Drawing.Point(25, 20);
            this.btnEndRemote.Name = "btnEndRemote";
            this.btnEndRemote.Size = new System.Drawing.Size(400, 48);
            this.btnEndRemote.TabIndex = 15;
            this.btnEndRemote.Text = "End Remote";
            this.btnEndRemote.UseVisualStyleBackColor = true;
            this.btnEndRemote.Click += new System.EventHandler(this.EndRemoteClick);
            // 
            // PanelManualConnect
            // 
            this.PanelManualConnect.Controls.Add(this.dividerManualConnect);
            this.PanelManualConnect.Controls.Add(this.lblManualConnect);
            this.PanelManualConnect.Controls.Add(this.lblNavTo2);
            this.PanelManualConnect.Controls.Add(this.txtClientAddress);
            this.PanelManualConnect.Controls.Add(this.lblEnterId);
            this.PanelManualConnect.Controls.Add(this.channelId);
            this.PanelManualConnect.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelManualConnect.Location = new System.Drawing.Point(0, 165);
            this.PanelManualConnect.Name = "PanelManualConnect";
            this.PanelManualConnect.Size = new System.Drawing.Size(450, 250);
            this.PanelManualConnect.TabIndex = 18;
            // 
            // dividerManualConnect
            // 
            this.dividerManualConnect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dividerManualConnect.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dividerManualConnect.Location = new System.Drawing.Point(0, 249);
            this.dividerManualConnect.Name = "dividerManualConnect";
            this.dividerManualConnect.Size = new System.Drawing.Size(450, 1);
            this.dividerManualConnect.TabIndex = 9;
            this.dividerManualConnect.UseCompatibleTextRendering = true;
            // 
            // PanelMonitor
            // 
            this.PanelMonitor.AutoSize = true;
            this.PanelMonitor.Controls.Add(this.listBoxConnections);
            this.PanelMonitor.Controls.Add(this.lblMonitor);
            this.PanelMonitor.Controls.Add(this.lblStatusValue);
            this.PanelMonitor.Controls.Add(this.lblStatus);
            this.PanelMonitor.Controls.Add(this.lblRemoteConnectionsValue);
            this.PanelMonitor.Controls.Add(this.lblRemoteConnections);
            this.PanelMonitor.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelMonitor.Location = new System.Drawing.Point(0, 415);
            this.PanelMonitor.Name = "PanelMonitor";
            this.PanelMonitor.Size = new System.Drawing.Size(450, 235);
            this.PanelMonitor.TabIndex = 19;
            // 
            // listBoxConnections
            // 
            this.listBoxConnections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxConnections.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.listBoxConnections.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxConnections.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxConnections.ItemHeight = 28;
            this.listBoxConnections.Location = new System.Drawing.Point(11, 148);
            this.listBoxConnections.Name = "listBoxConnections";
            this.listBoxConnections.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxConnections.Size = new System.Drawing.Size(429, 84);
            this.listBoxConnections.Sorted = true;
            this.listBoxConnections.TabIndex = 15;
            // 
            // PanelControls
            // 
            this.PanelControls.Controls.Add(this.btnEndRemote);
            this.PanelControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelControls.Location = new System.Drawing.Point(0, 868);
            this.PanelControls.Name = "PanelControls";
            this.PanelControls.Size = new System.Drawing.Size(450, 82);
            this.PanelControls.TabIndex = 20;
            // 
            // PanelMain
            // 
            this.PanelMain.AutoScroll = true;
            this.PanelMain.Controls.Add(this.PanelMonitor);
            this.PanelMain.Controls.Add(this.PanelManualConnect);
            this.PanelMain.Controls.Add(this.PanelQuickConnect);
            this.PanelMain.Controls.Add(this.PanelControls);
            this.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMain.Location = new System.Drawing.Point(0, 0);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.Size = new System.Drawing.Size(450, 950);
            this.PanelMain.TabIndex = 21;
            // 
            // PanelQuickConnect
            // 
            this.PanelQuickConnect.Controls.Add(this.dividerQuickConnect);
            this.PanelQuickConnect.Controls.Add(this.lblQuickConnect);
            this.PanelQuickConnect.Controls.Add(this.lblNavTo1);
            this.PanelQuickConnect.Controls.Add(this.txtChannelUri);
            this.PanelQuickConnect.Controls.Add(this.btnCopyUri);
            this.PanelQuickConnect.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelQuickConnect.Location = new System.Drawing.Point(0, 0);
            this.PanelQuickConnect.Name = "PanelQuickConnect";
            this.PanelQuickConnect.Size = new System.Drawing.Size(450, 165);
            this.PanelQuickConnect.TabIndex = 17;
            // 
            // dividerQuickConnect
            // 
            this.dividerQuickConnect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dividerQuickConnect.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dividerQuickConnect.Location = new System.Drawing.Point(0, 164);
            this.dividerQuickConnect.Name = "dividerQuickConnect";
            this.dividerQuickConnect.Size = new System.Drawing.Size(450, 1);
            this.dividerQuickConnect.TabIndex = 4;
            this.dividerQuickConnect.UseCompatibleTextRendering = true;
            // 
            // lblQuickConnect
            // 
            this.lblQuickConnect.AutoSize = true;
            this.lblQuickConnect.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuickConnect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(71)))), ((int)(((byte)(42)))));
            this.lblQuickConnect.Location = new System.Drawing.Point(6, 25);
            this.lblQuickConnect.Name = "lblQuickConnect";
            this.lblQuickConnect.Size = new System.Drawing.Size(172, 32);
            this.lblQuickConnect.TabIndex = 3;
            this.lblQuickConnect.Text = "Quick Connect";
            // 
            // lblNavTo1
            // 
            this.lblNavTo1.AutoSize = true;
            this.lblNavTo1.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNavTo1.Location = new System.Drawing.Point(6, 70);
            this.lblNavTo1.Name = "lblNavTo1";
            this.lblNavTo1.Size = new System.Drawing.Size(134, 30);
            this.lblNavTo1.TabIndex = 0;
            this.lblNavTo1.Text = "Navigate to:";
            // 
            // txtChannelUri
            // 
            this.txtChannelUri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.txtChannelUri.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtChannelUri.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChannelUri.Location = new System.Drawing.Point(12, 106);
            this.txtChannelUri.Name = "txtChannelUri";
            this.txtChannelUri.ReadOnly = true;
            this.txtChannelUri.Size = new System.Drawing.Size(320, 27);
            this.txtChannelUri.TabIndex = 1;
            this.txtChannelUri.Text = "[ Remote URI ]";
            // 
            // btnCopyUri
            // 
            this.btnCopyUri.Location = new System.Drawing.Point(335, 104);
            this.btnCopyUri.Name = "btnCopyUri";
            this.btnCopyUri.Size = new System.Drawing.Size(75, 35);
            this.btnCopyUri.TabIndex = 2;
            this.btnCopyUri.Text = "Copy";
            this.btnCopyUri.UseVisualStyleBackColor = true;
            this.btnCopyUri.Click += new System.EventHandler(this.UriCopyClick);
            // 
            // PanelLoading
            // 
            this.PanelLoading.Controls.Add(this.pictureBoxSpinner);
            this.PanelLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelLoading.Location = new System.Drawing.Point(0, 0);
            this.PanelLoading.Name = "PanelLoading";
            this.PanelLoading.Size = new System.Drawing.Size(450, 950);
            this.PanelLoading.TabIndex = 21;
            // 
            // pictureBoxSpinner
            // 
            this.pictureBoxSpinner.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.pictureBoxSpinner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxSpinner.Image = global::PowerPointRemote.AddIn.Properties.Resources.spinner;
            this.pictureBoxSpinner.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxSpinner.Name = "pictureBoxSpinner";
            this.pictureBoxSpinner.Size = new System.Drawing.Size(450, 950);
            this.pictureBoxSpinner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxSpinner.TabIndex = 0;
            this.pictureBoxSpinner.TabStop = false;
            // 
            // ChannelView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PanelMain);
            this.Controls.Add(this.PanelLoading);
            this.Name = "ChannelView";
            this.Size = new System.Drawing.Size(450, 950);
            this.PanelManualConnect.ResumeLayout(false);
            this.PanelManualConnect.PerformLayout();
            this.PanelMonitor.ResumeLayout(false);
            this.PanelMonitor.PerformLayout();
            this.PanelControls.ResumeLayout(false);
            this.PanelMain.ResumeLayout(false);
            this.PanelMain.PerformLayout();
            this.PanelQuickConnect.ResumeLayout(false);
            this.PanelQuickConnect.PerformLayout();
            this.PanelLoading.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpinner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblManualConnect;
        private System.Windows.Forms.TextBox txtClientAddress;
        private System.Windows.Forms.Label lblNavTo2;
        private System.Windows.Forms.Label lblEnterId;
        private System.Windows.Forms.TextBox channelId;
        private System.Windows.Forms.Label lblMonitor;
        private System.Windows.Forms.Label lblRemoteConnections;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblStatusValue;
        private System.Windows.Forms.Label lblRemoteConnectionsValue;
        private System.Windows.Forms.Button btnEndRemote;
        private System.Windows.Forms.Panel PanelManualConnect;
        private System.Windows.Forms.Panel PanelMonitor;
        private System.Windows.Forms.Panel PanelControls;
        private System.Windows.Forms.Panel PanelMain;
        private System.Windows.Forms.Panel PanelLoading;
        private System.Windows.Forms.PictureBox pictureBoxSpinner;
        private System.Windows.Forms.Label dividerManualConnect;
        private System.Windows.Forms.Panel PanelQuickConnect;
        private System.Windows.Forms.Label dividerQuickConnect;
        private System.Windows.Forms.Label lblQuickConnect;
        private System.Windows.Forms.Label lblNavTo1;
        private System.Windows.Forms.TextBox txtChannelUri;
        private System.Windows.Forms.Button btnCopyUri;
        private System.Windows.Forms.ListBox listBoxConnections;
    }
}
