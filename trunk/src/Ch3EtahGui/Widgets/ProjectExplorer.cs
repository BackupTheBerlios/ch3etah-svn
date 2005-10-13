using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI;

namespace Ch3Etah.Gui.Widgets {
	public class ProjectExplorer : DockContent {
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnRefresh;
		
		private System.Windows.Forms.TreeView tvwProject;
		
		public ProjectExplorer() {
			InitializeComponent();
		}

		private void InitializeComponent() {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ProjectExplorer));
			this.tvwProject = new System.Windows.Forms.TreeView();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tvwProject
			// 
			this.tvwProject.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvwProject.HideSelection = false;
			this.tvwProject.ImageIndex = -1;
			this.tvwProject.Location = new System.Drawing.Point(0, 0);
			this.tvwProject.Name = "tvwProject";
			this.tvwProject.SelectedImageIndex = -1;
			this.tvwProject.Size = new System.Drawing.Size(292, 266);
			this.tvwProject.TabIndex = 26;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnRefresh);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(292, 24);
			this.panel1.TabIndex = 27;
			this.panel1.Visible = false;
			// 
			// btnRefresh
			// 
			this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
			this.btnRefresh.Location = new System.Drawing.Point(0, 0);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(24, 23);
			this.btnRefresh.TabIndex = 0;
			// 
			// ProjectExplorer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.tvwProject);
			this.HideOnClose = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ProjectExplorer";
			this.Text = "Project Explorer";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		public System.Windows.Forms.TreeView TvwProject {
			get { return tvwProject; }
		}
	}
}