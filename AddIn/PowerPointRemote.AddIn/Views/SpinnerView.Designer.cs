namespace PowerPointRemote.AddIn.Views
{
    partial class SpinnerView
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
            this.pictureBoxSpinner = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxSpinner
            // 
            this.pictureBoxSpinner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxSpinner.Image = global::PowerPointRemote.AddIn.Properties.Resources.spinner;
            this.pictureBoxSpinner.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxSpinner.Name = "pictureBoxSpinner";
            this.pictureBoxSpinner.Size = new System.Drawing.Size(450, 950);
            this.pictureBoxSpinner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxSpinner.TabIndex = 0;
            this.pictureBoxSpinner.TabStop = false;
            // 
            // SpinnerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBoxSpinner);
            this.Name = "SpinnerView";
            this.Size = new System.Drawing.Size(450, 950);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpinner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxSpinner;
    }
}
