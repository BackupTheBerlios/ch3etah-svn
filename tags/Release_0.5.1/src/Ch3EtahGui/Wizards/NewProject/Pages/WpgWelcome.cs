using Ch3Etah.Gui.Widgets.Wizard;

namespace Ch3Etah.Gui.Wizards.NewProject.Pages {
    /// <summary>
    /// Summary description for Welcome.
    /// </summary>
    public class WpgWelcome : WizardPage {
        private Ch3Etah.Gui.Widgets.Wizard.InfoContainer infoWelcome;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;

        private void InitializeComponent() {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WpgWelcome));
            this.infoWelcome = new Ch3Etah.Gui.Widgets.Wizard.InfoContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.infoWelcome.SuspendLayout();
            this.SuspendLayout();
            // 
            // infoWelcome
            // 
            this.infoWelcome.BackColor = System.Drawing.Color.White;
            this.infoWelcome.Controls.Add(this.label3);
            this.infoWelcome.Controls.Add(this.label2);
            this.infoWelcome.Controls.Add(this.label1);
            this.infoWelcome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoWelcome.Image = ((System.Drawing.Image)(resources.GetObject("infoWelcome.Image")));
            this.infoWelcome.Location = new System.Drawing.Point(0, 0);
            this.infoWelcome.Name = "infoWelcome";
            this.infoWelcome.PageTitle = "Welcome to the New Project Wizard";
            this.infoWelcome.Size = new System.Drawing.Size(608, 360);
            this.infoWelcome.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(176, 336);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "To continue, click Next.";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(200, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(392, 88);
            this.label2.TabIndex = 9;
            this.label2.Text = "• Set up a new project, importing the required templates and configuring the most" +
                " common generator commands.\r\n\r\n• Extract metadata from a database, generating au" +
                "tomatically most of the entities\' definitions - including indexes and links.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(176, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "This wizard helps you:";
            // 
            // Welcome
            // 
            this.Controls.Add(this.infoWelcome);
            this.Name = "Welcome";
            this.Size = new System.Drawing.Size(608, 360);
            this.infoWelcome.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    
        public WpgWelcome() {
            //
            // TODO: Add constructor logic here
            //
            InitializeComponent();
        }
    }
}