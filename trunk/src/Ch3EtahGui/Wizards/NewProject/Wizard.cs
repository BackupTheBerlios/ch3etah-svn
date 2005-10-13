using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

using ADODB;
using ADOX;

using Ch3Etah.Gui.Widgets;
using Ch3Etah.Gui.Widgets.Wizard;
using Ch3Etah.Gui.Wizards.NewProject.Pages;
using Ch3Etah.Metadata.OREntities;
using Ch3tah.DatabaseWizard.UI.Forms;
using Ch3Etah.Core.ProjectLib;

namespace Ch3Etah.Gui.Wizards.NewProject {
    public class Wizard : WizardBase {
        private DataSourceCollection dataSources;

        #region Designer generated code

        private Header header1;
        private Ch3Etah.Gui.Widgets.Wizard.WizardPage wpgDataSource;
        private System.Windows.Forms.ListBox lstDataSources;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdNewDataSource;
        private System.Windows.Forms.Button cmdEdit;
        private System.Windows.Forms.Button cmdDelete;
        private Ch3Etah.Gui.Widgets.Wizard.Header header2;
        private Ch3Etah.Gui.Widgets.Wizard.WizardPage wpgTables;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckedListBox lstTables;
        private Ch3Etah.Gui.Widgets.Wizard.WizardPage wpgProcess;
        private Ch3Etah.Gui.Widgets.Wizard.Header header3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label7;
        private Ch3Etah.Gui.Widgets.Wizard.WizardPage wizardPage1;
        private Ch3Etah.Gui.Wizards.NewProject.Pages.WpgWelcome wpgWelcome;
        private Ch3Etah.Gui.Widgets.Wizard.InfoContainer infoWelcome;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private IContainer components = null;

        public Wizard() {
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof (Wizard));
            this.wpgDataSource = new Ch3Etah.Gui.Widgets.Wizard.WizardPage();
            this.lstDataSources = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.header1 = new Ch3Etah.Gui.Widgets.Wizard.Header();
            this.cmdNewDataSource = new System.Windows.Forms.Button();
            this.cmdEdit = new System.Windows.Forms.Button();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.wpgTables = new Ch3Etah.Gui.Widgets.Wizard.WizardPage();
            this.label5 = new System.Windows.Forms.Label();
            this.lstTables = new System.Windows.Forms.CheckedListBox();
            this.header2 = new Ch3Etah.Gui.Widgets.Wizard.Header();
            this.wpgProcess = new Ch3Etah.Gui.Widgets.Wizard.WizardPage();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label6 = new System.Windows.Forms.Label();
            this.header3 = new Ch3Etah.Gui.Widgets.Wizard.Header();
            this.label7 = new System.Windows.Forms.Label();
            this.wizardPage1 = new Ch3Etah.Gui.Widgets.Wizard.WizardPage();
            this.wpgWelcome = new WpgWelcome();
            this.infoWelcome = new Ch3Etah.Gui.Widgets.Wizard.InfoContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.wizardControl1.SuspendLayout();
            this.wpgDataSource.SuspendLayout();
            this.wpgTables.SuspendLayout();
            this.wpgProcess.SuspendLayout();
            this.wpgWelcome.SuspendLayout();
            this.infoWelcome.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.Controls.Add(this.wpgWelcome);
            this.wizardControl1.Controls.Add(this.wpgDataSource);
            this.wizardControl1.Controls.Add(this.wpgTables);
            this.wizardControl1.Controls.Add(this.wpgProcess);
            this.wizardControl1.Controls.Add(this.wizardPage1);
            this.wizardControl1.Controls.Add(this.wpgWelcome);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Pages.AddRange(new Ch3Etah.Gui.Widgets.Wizard.WizardPage[] {
                this.wpgWelcome,
                this.wpgDataSource,
                this.wpgTables,
                this.wpgProcess,
                this.wizardPage1
            });
            this.wizardControl1.Load += new System.EventHandler(this.wizardControl1_Load);
            // 
            // wpgDataSource
            // 
            this.wpgDataSource.Controls.Add(this.lstDataSources);
            this.wpgDataSource.Controls.Add(this.label4);
            this.wpgDataSource.Controls.Add(this.header1);
            this.wpgDataSource.Controls.Add(this.cmdNewDataSource);
            this.wpgDataSource.Controls.Add(this.cmdEdit);
            this.wpgDataSource.Controls.Add(this.cmdDelete);
            this.wpgDataSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wpgDataSource.IsFinishPage = false;
            this.wpgDataSource.Location = new System.Drawing.Point(0, 0);
            this.wpgDataSource.Name = "wpgDataSource";
            this.wpgDataSource.Size = new System.Drawing.Size(616, 366);
            this.wpgDataSource.TabIndex = 2;
            this.wpgDataSource.ShowFromBack += new System.EventHandler(this.wpgDataSource_Show);
            this.wpgDataSource.ShowFromNext += new System.EventHandler(this.wpgDataSource_Show);
            // 
            // lstDataSources
            // 
            this.lstDataSources.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
            this.lstDataSources.Location = new System.Drawing.Point(32, 100);
            this.lstDataSources.Name = "lstDataSources";
            this.lstDataSources.Size = new System.Drawing.Size(432, 251);
            this.lstDataSources.TabIndex = 5;
            this.lstDataSources.DoubleClick += new System.EventHandler(this.List_DoubleClick);
            this.lstDataSources.SelectedIndexChanged += new System.EventHandler(this.lstDataSources_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
            this.label4.Location = new System.Drawing.Point(32, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(212, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Which data source would you like to use?";
            // 
            // header1
            // 
            this.header1.BackColor = System.Drawing.SystemColors.Control;
            this.header1.CausesValidation = false;
            this.header1.Description = "You must select a data source (a set of database connection details) before proce" +
                "eding.";
            this.header1.Dock = System.Windows.Forms.DockStyle.Top;
            this.header1.Image = ((System.Drawing.Image) (resources.GetObject("header1.Image")));
            this.header1.Location = new System.Drawing.Point(0, 0);
            this.header1.Name = "header1";
            this.header1.Size = new System.Drawing.Size(616, 64);
            this.header1.TabIndex = 0;
            this.header1.Title = "Connect to data source";
            // 
            // cmdNewDataSource
            // 
            this.cmdNewDataSource.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
            this.cmdNewDataSource.Location = new System.Drawing.Point(472, 100);
            this.cmdNewDataSource.Name = "cmdNewDataSource";
            this.cmdNewDataSource.Size = new System.Drawing.Size(84, 23);
            this.cmdNewDataSource.TabIndex = 4;
            this.cmdNewDataSource.Text = "&Build new...";
            this.cmdNewDataSource.Click += new System.EventHandler(this.cmdNewDataSource_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Enabled = false;
            this.cmdEdit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
            this.cmdEdit.Location = new System.Drawing.Point(472, 132);
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Size = new System.Drawing.Size(84, 23);
            this.cmdEdit.TabIndex = 4;
            this.cmdEdit.Text = "&Edit...";
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Enabled = false;
            this.cmdDelete.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
            this.cmdDelete.Location = new System.Drawing.Point(472, 164);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(84, 23);
            this.cmdDelete.TabIndex = 4;
            this.cmdDelete.Text = "&Delete...";
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // wpgTables
            // 
            this.wpgTables.Controls.Add(this.label5);
            this.wpgTables.Controls.Add(this.lstTables);
            this.wpgTables.Controls.Add(this.header2);
            this.wpgTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wpgTables.IsFinishPage = false;
            this.wpgTables.Location = new System.Drawing.Point(0, 0);
            this.wpgTables.Name = "wpgTables";
            this.wpgTables.Size = new System.Drawing.Size(616, 366);
            this.wpgTables.TabIndex = 3;
            this.wpgTables.Paint += new System.Windows.Forms.PaintEventHandler(this.wpgTables_Paint);
            this.wpgTables.ShowFromNext += new System.EventHandler(this.wpgTables_ShowFromNext);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
            this.label5.Location = new System.Drawing.Point(32, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(231, 17);
            this.label5.TabIndex = 3;
            this.label5.Text = "&Check the desired tables from the list  below:";
            // 
            // lstTables
            // 
            this.lstTables.Location = new System.Drawing.Point(32, 100);
            this.lstTables.Name = "lstTables";
            this.lstTables.Size = new System.Drawing.Size(432, 244);
            this.lstTables.Sorted = true;
            this.lstTables.TabIndex = 2;
            this.lstTables.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstTables_ItemCheck);
            // 
            // header2
            // 
            this.header2.BackColor = System.Drawing.SystemColors.Control;
            this.header2.CausesValidation = false;
            this.header2.Description = "Select all the tables that should be processed by the wizard.";
            this.header2.Dock = System.Windows.Forms.DockStyle.Top;
            this.header2.Image = ((System.Drawing.Image) (resources.GetObject("header2.Image")));
            this.header2.Location = new System.Drawing.Point(0, 0);
            this.header2.Name = "header2";
            this.header2.Size = new System.Drawing.Size(616, 64);
            this.header2.TabIndex = 1;
            this.header2.Title = "Select tables";
            // 
            // wpgProcess
            // 
            this.wpgProcess.Controls.Add(this.progressBar1);
            this.wpgProcess.Controls.Add(this.label6);
            this.wpgProcess.Controls.Add(this.header3);
            this.wpgProcess.Controls.Add(this.label7);
            this.wpgProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wpgProcess.IsFinishPage = false;
            this.wpgProcess.Location = new System.Drawing.Point(0, 0);
            this.wpgProcess.Name = "wpgProcess";
            this.wpgProcess.Size = new System.Drawing.Size(616, 366);
            this.wpgProcess.TabIndex = 4;
            this.wpgProcess.ShowFromBack += new System.EventHandler(this.wpgProcess_ShowFromBack);
            this.wpgProcess.ShowFromNext += new System.EventHandler(this.wpgProcess_ShowFromNext);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(88, 216);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(464, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(371, 17);
            this.label6.TabIndex = 3;
            this.label6.Text = "Please wait while the wizard gathers information from the selected tables";
            // 
            // header3
            // 
            this.header3.BackColor = System.Drawing.SystemColors.Control;
            this.header3.CausesValidation = false;
            this.header3.Description = "Now the wizard will gather information from the selected tables.";
            this.header3.Dock = System.Windows.Forms.DockStyle.Top;
            this.header3.Image = ((System.Drawing.Image) (resources.GetObject("header3.Image")));
            this.header3.Location = new System.Drawing.Point(0, 0);
            this.header3.Name = "header3";
            this.header3.Size = new System.Drawing.Size(616, 64);
            this.header3.TabIndex = 2;
            this.header3.Title = "Processing tables";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(88, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(464, 48);
            this.label7.TabIndex = 3;
            this.label7.Text = "Table: ";
            // 
            // wizardPage1
            // 
            this.wizardPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage1.IsFinishPage = false;
            this.wizardPage1.Location = new System.Drawing.Point(0, 0);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(616, 366);
            this.wizardPage1.TabIndex = 5;
            // 
            // wpgWelcome
            // 
            this.wpgWelcome.Controls.Add(this.infoWelcome);
            this.wpgWelcome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wpgWelcome.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
            this.wpgWelcome.IsFinishPage = false;
            this.wpgWelcome.Location = new System.Drawing.Point(0, 0);
            this.wpgWelcome.Name = "wpgWelcome";
            this.wpgWelcome.Size = new System.Drawing.Size(616, 414);
            this.wpgWelcome.TabIndex = 1;
            // 
            // infoWelcome
            // 
            this.infoWelcome.BackColor = System.Drawing.Color.White;
            this.infoWelcome.Controls.Add(this.label3);
            this.infoWelcome.Controls.Add(this.label2);
            this.infoWelcome.Controls.Add(this.label1);
            this.infoWelcome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoWelcome.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
            this.infoWelcome.Image = ((System.Drawing.Image) (resources.GetObject("infoWelcome.Image")));
            this.infoWelcome.Location = new System.Drawing.Point(0, 0);
            this.infoWelcome.Name = "infoWelcome";
            this.infoWelcome.PageTitle = "Welcome to the New Project Wizard";
            this.infoWelcome.Size = new System.Drawing.Size(616, 414);
            this.infoWelcome.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
            this.label3.Location = new System.Drawing.Point(176, 336);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 16);
            this.label3.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
            this.label2.Location = new System.Drawing.Point(200, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(392, 88);
            this.label2.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
            this.label1.Location = new System.Drawing.Point(176, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 16);
            this.label1.TabIndex = 8;
            // 
            // Wizard
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(616, 414);
            this.Name = "Wizard";
            this.Load += new System.EventHandler(this.Wizard_Load);
            this.wizardControl1.ResumeLayout(false);
            this.wpgDataSource.ResumeLayout(false);
            this.wpgTables.ResumeLayout(false);
            this.wpgProcess.ResumeLayout(false);
            this.wpgWelcome.ResumeLayout(false);
            this.infoWelcome.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

		[Obsolete("This should be pulling from the currently loaded project.")]
        private void Wizard_Load(object sender, EventArgs e) {
            // Inicializa os controles
            //dataSources = new DataSourceCollection("C:\\DataSources.xml");
			dataSources = new DataSourceCollection();
            lstDataSources.DataSource = dataSources;
        }

        private void wpgDataSource_Show(object sender, EventArgs e) {
            // Verifica se há algum data source definido
            int items = lstDataSources.Items.Count;
            lstDataSources.Enabled = (items > 0);
            if (items > 0) {
                // Focaliza a seleção de existentes
                if (lstDataSources.SelectedIndex == -1)
                    lstDataSources.SelectedIndex = 0;
                lstDataSources.Focus();
            }
            else {
                // Focaliza a seleção de novo
                cmdNewDataSource.Focus();
            }
        }

        private void cmdNewDataSource_Click(object sender, EventArgs e) {
            DataSourceEditor editor = new DataSourceEditor();
            DataSource ds = editor.newDataSource();
            if (ds != null) {
                dataSources.Add(ds);
                Rebind(lstDataSources);
                lstDataSources.SelectedItem = ds;
                lstDataSources.Focus();
                //dataSources.Save();
            }
        }

        private void lstDataSources_SelectedIndexChanged(object sender, EventArgs e) {
            bool enabled = lstDataSources.SelectedIndex > -1;
//            cmdEdit.Enabled = enabled;
            cmdDelete.Enabled = enabled;
            wizardControl1.NextEnabled = enabled;
        }

        private void cmdDelete_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Do you really want to delete that data source?", "Confirmation",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                dataSources.Remove((DataSource) lstDataSources.SelectedItem);
                Rebind(lstDataSources);
            }
        }

        private void Rebind(ListControl listControl) {
            CurrencyManager cm = (CurrencyManager) listControl.BindingContext[listControl.DataSource];
            cm.Refresh();
        }

        public void cmdEdit_Click(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        private void wpgTables_ShowFromNext(object sender, EventArgs e) {
            // Obtém uma conexão ao banco de dados
            DataSource ds = (DataSource) lstDataSources.SelectedItem;
            IDbConnection cn = ds.GetConnection();
            Connection adocn = new Connection();
            adocn.Open(cn.ConnectionString, "", "", 0);

            // Conecta ao banco de dados
            Catalog cat = new Catalog();
            cat.ActiveConnection = adocn;

            // Enumera as tabelas
            foreach (Table tbl in cat.Tables) {
                if (tbl.Type.Equals("TABLE"))
                    lstTables.Items.Add(tbl.Name);
            }

            lstTables_ItemCheck(lstTables, new ItemCheckEventArgs(lstTables.SelectedIndex, CheckState.Unchecked, CheckState.Unchecked));
        }

        private void wpgTables_Paint(object sender, PaintEventArgs e) {}

        private void List_DoubleClick(object sender, EventArgs e) {
            wizardControl1.Next();
        }

        private void wpgProcess_ShowFromNext(object sender, EventArgs e) {}

        private void wpgProcess_ShowFromBack(object sender, EventArgs e) {
            wizardControl1.Back();
        }

        private void lstTables_ItemCheck(object sender, ItemCheckEventArgs e) {
            wizardControl1.NextEnabled = (lstTables.CheckedItems.Count > 0);
        }

        private void wizardControl1_Load(object sender, EventArgs e) {}
    }
}