namespace PowerPointRemote.AddIn.Views
{
    partial class ErrorView
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
            this.PanelMain = new System.Windows.Forms.Panel();
            this.PanelControls = new System.Windows.Forms.Panel();
            this.btnRetry = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.PanelQuickConnect = new System.Windows.Forms.Panel();
            this.lblError = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.PanelMain.SuspendLayout();
            this.PanelControls.SuspendLayout();
            this.PanelQuickConnect.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelMain
            // 
            this.PanelMain.AutoScroll = true;
            this.PanelMain.Controls.Add(this.PanelControls);
            this.PanelMain.Controls.Add(this.PanelQuickConnect);
            this.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMain.Location = new System.Drawing.Point(0, 0);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.Size = new System.Drawing.Size(450, 950);
            this.PanelMain.TabIndex = 22;
            // 
            // PanelControls
            // 
            this.PanelControls.Controls.Add(this.btnRetry);
            this.PanelControls.Controls.Add(this.btnClose);
            this.PanelControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelControls.Location = new System.Drawing.Point(0, 123);
            this.PanelControls.Name = "PanelControls";
            this.PanelControls.Size = new System.Drawing.Size(450, 82);
            this.PanelControls.TabIndex = 20;
            // 
            // btnRetry
            // 
            this.btnRetry.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRetry.Location = new System.Drawing.Point(22, 24);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Size = new System.Drawing.Size(180, 48);
            this.btnRetry.TabIndex = 16;
            this.btnRetry.Text = "Retry";
            this.btnRetry.UseVisualStyleBackColor = true;
            this.btnRetry.Click += new System.EventHandler(this.OnRetryClick);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(233, 24);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(180, 48);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.OnCloseClick);
            // 
            // PanelQuickConnect
            // 
            this.PanelQuickConnect.Controls.Add(this.lblError);
            this.PanelQuickConnect.Controls.Add(this.lblMessage);
            this.PanelQuickConnect.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelQuickConnect.Location = new System.Drawing.Point(0, 0);
            this.PanelQuickConnect.Name = "PanelQuickConnect";
            this.PanelQuickConnect.Size = new System.Drawing.Size(450, 123);
            this.PanelQuickConnect.TabIndex = 17;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(71)))), ((int)(((byte)(42)))));
            this.lblError.Location = new System.Drawing.Point(6, 25);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(195, 32);
            this.lblError.TabIndex = 3;
            this.lblError.Text = "Connection Error";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(6, 70);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(158, 28);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "[ Error Message ]";
            // 
            // ErrorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PanelMain);
            this.Name = "ErrorView";
            this.Size = new System.Drawing.Size(450, 950);
            this.PanelMain.ResumeLayout(false);
            this.PanelControls.ResumeLayout(false);
            this.PanelQuickConnect.ResumeLayout(false);
            this.PanelQuickConnect.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelMain;
        private System.Windows.Forms.Panel PanelControls;
        private System.Windows.Forms.Button btnRetry;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel PanelQuickConnect;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblMessage;
    }
}
