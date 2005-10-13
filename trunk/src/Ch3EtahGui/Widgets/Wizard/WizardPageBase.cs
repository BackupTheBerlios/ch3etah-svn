using System.ComponentModel;

namespace Ch3Etah.Gui.Widgets.Wizard {
    public class WizardPageBase : WizardPage {
        private Header header;
        private IContainer components = null;

        public WizardPageBase() {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof (WizardPageBase));
            this.header = new Ch3Etah.Gui.Widgets.Wizard.Header();
            this.SuspendLayout();
            // 
            // header
            // 
            this.header.BackColor = System.Drawing.SystemColors.Control;
            this.header.CausesValidation = false;
            this.header.Description = "Page Description";
            this.header.Dock = System.Windows.Forms.DockStyle.Top;
            this.header.Image = ((System.Drawing.Image) (resources.GetObject("header.Image")));
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(616, 64);
            this.header.TabIndex = 1;
            this.header.Title = "Page Title";
            // 
            // WizardPageBase
            // 
            this.Controls.Add(this.header);
            this.Name = "WizardPageBase";
            this.Size = new System.Drawing.Size(616, 366);
            this.ResumeLayout(false);

        }

        #endregion

        public string Title {
            get { return header.Title; }
            set { header.Title = value; }
        }

        public string Description {
            get { return header.Description; }
            set { header.Description = value; }
        }
    }
}