namespace PowerPointRemote.AddIn
{
    partial class Ribbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Ribbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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
            this.tab1 = this.Factory.CreateRibbonTab();
            this.GroupSlideShowRemote = this.Factory.CreateRibbonGroup();
            this.BtnOpen = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.GroupSlideShowRemote.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.ControlId.OfficeId = "TabSlideShow";
            this.tab1.Groups.Add(this.GroupSlideShowRemote);
            this.tab1.Label = "TabSlideShow";
            this.tab1.Name = "tab1";
            // 
            // GroupSlideShowRemote
            // 
            this.GroupSlideShowRemote.Items.Add(this.BtnOpen);
            this.GroupSlideShowRemote.Label = "Slide Show Remote";
            this.GroupSlideShowRemote.Name = "GroupSlideShowRemote";
            // 
            // BtnOpen
            // 
            this.BtnOpen.Label = "Open Remote";
            this.BtnOpen.Name = "BtnOpen";
            this.BtnOpen.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.BtnOpen_Click);
            // 
            // Ribbon
            // 
            this.Name = "Ribbon";
            this.RibbonType = "Microsoft.PowerPoint.Presentation";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.GroupSlideShowRemote.ResumeLayout(false);
            this.GroupSlideShowRemote.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup GroupSlideShowRemote;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BtnOpen;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon Ribbon
        {
            get { return this.GetRibbon<Ribbon>(); }
        }
    }
}
